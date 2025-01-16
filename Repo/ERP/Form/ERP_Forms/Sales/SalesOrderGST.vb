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
Imports AxFPSpreadADO
'Imports Infragistics.Win.UltraWinTabControl
Friend Class frmSalesOrderGST
    Inherits System.Windows.Forms.Form
    Dim RsSOMain As ADODB.Recordset ''ADODB.Recordset
    Dim RsSODetail As ADODB.Recordset ''ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean
    Dim mAccountCode As String
    Dim mSearchKey As String
    Dim cntSearchRow As Long
    Dim cntSearchCol As Long

    Private Const ConRowHeight As Short = 14
    Dim pTempSeq As String

    Private Const ColItemCode As Short = 1
    Private Const ColPartNo As Short = 2
    Private Const ColItemName As Short = 3
    Private Const ColItemUOM As Short = 4
    Private Const ColHSNCode As Short = 5

    Private Const ColGlassDescription As Short = 6
    Private Const ColActualWidth As Short = 7
    Private Const ColActualHeight As Short = 8

    Private Const ColSize As Short = 9
    Private Const ColChargeableWidth As Short = 10
    Private Const ColChargeableHeight As Short = 11


    Private Const ColArea As Short = 12
    Private Const ColAreaRate As Short = 13


    Private Const ColModelNo As Short = 14
    Private Const ColDrawingNo As Short = 15
    Private Const ColItemSNo As Short = 16
    Private Const ColAddItemDesc As Short = 17
    Private Const ColCustStoreLoc As Short = 18
    Private Const ColPreviousItemRate As Short = 19
    Private Const ColPktQty As Short = 20
    Private Const ColItemQty As Short = 21
    Private Const ColItemDetail As Short = 22
    Private Const ColMRP As Short = 23
    Private Const ColItemDiscount As Short = 24
    Private Const ColTODDiscount As Short = 25
    Private Const ColOtherDiscount As Short = 26
    Private Const ColItemRate As Short = 27
    Private Const ColOtherCost As Short = 28
    Private Const ColFreightCost As Short = 29
    Private Const ColVariablePrice As Short = 30

    Private Const ColItemAmount As Short = 31
    Private Const ColPO_WEF As Short = 32
    Private Const ColValidQty As Short = 33
    Private Const ColValidDate As Short = 34
    Private Const ColMSPCost As Short = 35
    Private Const ColMSPCostAdd As Short = 36
    Private Const ColMTRCOST As Short = 37
    Private Const ColProcessCost As Short = 38
    Private Const ColCGSTPer As Short = 39
    Private Const ColSGSTPer As Short = 40
    Private Const ColIGSTPer As Short = 41

    Private Const ColCGSTAmount As Short = 42
    Private Const ColSGSTAmount As Short = 43
    Private Const ColIGSTAmount As Short = 44

    Private Const ColGrossAmount As Short = 45

    Private Const ColAccountName As Short = 46
    Private Const ColSOStatus As Short = 47
    Private Const colRemarks As Short = 48

    Dim FileDBCn As ADODB.Connection
    Dim mSearchStartRow As Integer

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer

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

    Private Sub chkExporterMerchant_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkExporterMerchant.CheckStateChanged
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
        txtStoreDetail.Enabled = True
        txtApplicant.Enabled = True
        txtShipCustomer.Enabled = IIf(PubUserID = "G0416", True, False)
        txtShipTo.Enabled = IIf(PubUserID = "G0416", True, False)


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
        Dim mStoreLoc As String

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
                    mRate = VB6.Format(Trim(IIf(IsDBNull(RsFile.Fields(3).Value), 0, RsFile.Fields(3).Value)), "0.0000")
                    mWEF = VB6.Format(Trim(IIf(IsDBNull(RsFile.Fields(4).Value), "", RsFile.Fields(4).Value)), "DD-MMM-YYYY")
                    mStoreLoc = Trim(IIf(IsDBNull(RsFile.Fields(5).Value), "", RsFile.Fields(5).Value))

                    'Item Code	Item Description	Item Part No	Item Rate	WEF	Store Loc


                    mChkItemCode = mChkItemCode & "-" & Trim(mStoreLoc)

                    With SprdMain
                        For cntRow = 1 To .MaxRows
                            .Row = cntRow
                            .Col = ColItemCode
                            mItemCode = Trim(.Text)

                            .Col = ColCustStoreLoc
                            mItemCode = mItemCode & "-" & Trim(.Text)

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

                If InsertIntoDelAudit(PubDBCn, "DSP_SALEORDER_HDR", (lblMkey.Text), RsSOMain, "MKEY", "D") = False Then GoTo DelErrPart
                If InsertIntoDelAudit(PubDBCn, "DSP_SALEORDER_DET", (lblMkey.Text), RsSODetail, "MKEY", "D") = False Then GoTo DelErrPart

                If InsertIntoDeleteTrn(PubDBCn, "DSP_SALEORDER_HDR", "MKEY", (lblMkey.Text)) = False Then GoTo DelErrPart

                PubDBCn.Execute("DELETE FROM DSP_SALEORDER_DET WHERE MKEY=" & Val(lblMkey.Text) & "")
                PubDBCn.Execute("DELETE FROM DSP_SALEORDER_HDR WHERE MKEY=" & Val(lblMkey.Text) & "")

                If DeleteDSDailyDetail(PubDBCn, Val(txtSONo.Text)) = False Then GoTo DelErrPart

                SqlStr = " UPDATE DSP_SALEORDER_HDR SET SO_STATUS='O', " & vbCrLf _
                    & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                    & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND AUTO_KEY_SO=" & Val(txtSONo.Text) & "" & vbCrLf _
                    & " AND AMEND_NO=" & Val(txtAmendNo.Text) - 1 & ""

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
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonPO(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonPO(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
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
    Private Sub cboReason_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboReason.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboReason_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboReason.SelectedIndexChanged
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
        Dim mProjectCode As Double
        Dim mSalePersonCode As String
        Dim mPaymentType As String
        Dim mExporterMerchant As String
        Dim mStoreDetail As String = ""
        Dim mApplicantDetail As String = ""

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mStatus = VB.Left(cboStatus.Text, 1)
        mPOType = VB.Left(cboPOType.Text, 1)

        mOrderType = VB.Left(cboOrderType.Text, 1)
        mApproved = IIf(chkApproved.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mDI = IIf(chkDI.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mExporterMerchant = IIf(chkExporterMerchant.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

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

        mStoreDetail = ""
        If MainClass.ValidateWithMasterTable((txtStoreDetail.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('C','S')") = True Then
            mStoreDetail = MasterNo
        End If



        mApplicantDetail = ""
        If MainClass.ValidateWithMasterTable((txtApplicant.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('C','S')") = True Then
            mApplicantDetail = MasterNo
        End If

        SqlStr = ""
        mSoNo = Val(txtSONo.Text)
        If Val(txtSONo.Text) = 0 Then
            mSoNo = AutoGenPONoSeq()
        End If

        mProjectCode = IIf(cboProjectName.Text = "", 0, cboProjectName.Value)
        mSalePersonCode = IIf(cboSalePersonName.Text = "", "", cboSalePersonName.Value)
        mPaymentType = IIf(cboPaymentType.Text = "", "", cboPaymentType.Value)

        If MODIFYMode = True Then
            If InsertIntoDelAudit(PubDBCn, "DSP_SALEORDER_HDR", (lblMkey.Text), RsSOMain, "MKEY", "M") = False Then GoTo ErrPart
            If InsertIntoDelAudit(PubDBCn, "DSP_SALEORDER_DET", (lblMkey.Text), RsSODetail, "MKEY", "M") = False Then GoTo ErrPart
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
                & " BILL_TO_LOC_ID, SHIP_TO_LOC_ID, SHIPPED_TO_PARTY_CODE, SHIPPED_TO_SAMEPARTY,DELIVERY_INSTRUCTION_REQ,PO_TYPE," & vbCrLf _
                & " VENDOR_CODE,SCHD_AGREEMENT_NO, SCHD_AGREEMENT_DATE," & vbCrLf _
                & " PROJECT_CODE, SALE_PERSON_CODE, PAYMENT_TYPE, CHEQUE_NO, PO_AMEND_REASON,EXPORTER_MERCHANT,AUTO_KEY_PI, PI_TYPE,SUPP_CUST_STORE_CODE,SUPP_CUST_APPLICANT_CODE) "

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
                & " '" & MainClass.AllowSingleQuote(txtBillTo.Text) & "','" & MainClass.AllowSingleQuote(txtShipTo.Text) & "','" & MainClass.AllowSingleQuote(mShipToCustCode) & "'," & vbCrLf _
                & "'" & MainClass.AllowSingleQuote(mShipToSameBillTo) & "','" & mDI & "','" & mPOType & "','" & MainClass.AllowSingleQuote(txtVendorCode.Text) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtScheduleAggNo.Text) & "',TO_DATE('" & VB6.Format(txtScheduleAggDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') , " & vbCrLf _
                & " " & IIf(Val(mProjectCode) = 0, "NULL", Val(mProjectCode)) & ", '" & MainClass.AllowSingleQuote(mSalePersonCode) & "', '" & MainClass.AllowSingleQuote(mPaymentType) & "', '" & MainClass.AllowSingleQuote(txtChqNo.Text) & "','" & cboReason.Text & "','" & mExporterMerchant & "'," & vbCrLf _
                & " '" & Trim(txtPINo.Text) & "' , '" & txtPIType.Text & "','" & MainClass.AllowSingleQuote(mStoreDetail) & "','" & MainClass.AllowSingleQuote(mApplicantDetail) & "')"
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
                    & " SHIPPED_TO_PARTY_CODE='" & MainClass.AllowSingleQuote(mShipToCustCode) & "',SHIPPED_TO_SAMEPARTY='" & MainClass.AllowSingleQuote(mShipToSameBillTo) & "',  " & vbCrLf _
                    & " PROJECT_CODE = " & IIf(Val(mProjectCode) = 0, "NULL", Val(mProjectCode)) & ", SALE_PERSON_CODE = '" & MainClass.AllowSingleQuote(mSalePersonCode) & "', PAYMENT_TYPE = '" & MainClass.AllowSingleQuote(mPaymentType) & "', CHEQUE_NO = '" & MainClass.AllowSingleQuote(txtChqNo.Text) & "', "


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
                    & " SO_STATUS='" & mStatus & "', SUPP_CUST_STORE_CODE='" & MainClass.AllowSingleQuote(mStoreDetail) & "', SUPP_CUST_APPLICANT_CODE='" & MainClass.AllowSingleQuote(mApplicantDetail) & "' ," & vbCrLf _
                    & " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "'," & vbCrLf _
                    & " BILL_TO_LOC_ID='" & MainClass.AllowSingleQuote(txtBillTo.Text) & "'," & vbCrLf _
                    & " SHIP_TO_LOC_ID='" & MainClass.AllowSingleQuote(txtShipTo.Text) & "'," & vbCrLf _
                    & " ORDER_TYPE='" & mOrderType & "', DELIVERY_INSTRUCTION_REQ='" & mDI & "',EXPORTER_MERCHANT='" & mExporterMerchant & "'," & vbCrLf _
                    & " GOODS_SERVICE='" & VB.Left(cboInvType.Text, 1) & "', SAC_CODE = '" & mSACCode & "', " & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', PO_AMEND_REASON='" & cboReason.Text & "', " & vbCrLf _
                    & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')," & vbCrLf _
                    & " EPCG_NO='" & MainClass.AllowSingleQuote(txtEPCGNo.Text) & "',EPCG_DATE=TO_DATE('" & VB6.Format(txtEPCGDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                    & " SCHD_AGREEMENT_NO='" & MainClass.AllowSingleQuote(txtScheduleAggNo.Text) & "',SCHD_AGREEMENT_DATE=TO_DATE('" & VB6.Format(txtScheduleAggDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                    & " AUTO_KEY_PI = '" & Trim(txtPINo.Text) & "' , PI_TYPE='" & txtPIType.Text & "'" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND MKEY =" & Val(lblMkey.Text) & ""
            End If
        End If

        PubDBCn.Execute(SqlStr)
        If UpdateDetail1() = False Then GoTo ErrPart

        If VB.Left(cboOrderType.Text, 1) = "C" Then
            If UpdateDailyDSDetail(mSoNo) = False Then GoTo ErrPart
        End If

        If lblAddItem.Text = "N" Then
            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then

            Else
                SqlStr = " UPDATE DSP_SALEORDER_HDR SET SO_STATUS='C', " & vbCrLf _
                       & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                       & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
                       & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                       & " AND AUTO_KEY_SO=" & mSoNo & "" & vbCrLf & " AND AMEND_NO=" & Val(txtAmendNo.Text) - 1 & "" & vbCrLf
                PubDBCn.Execute(SqlStr)
            End If

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
        Dim mItemRate As Double
        Dim mOtherCost As Double
        Dim mFreightCost As Double

        Dim mMRP As Double
        Dim mPrice As Double
        Dim mDisc As Double
        Dim mPackingStandard As Double
        Dim mItemCode As String
        Dim mPktQty As Double
        Dim I As Integer
        Dim j As Integer
        Dim mChargeableHeight As Double
        Dim mChargeableWidth As Double
        Dim mHeight As Double
        Dim mWidth As Double
        Dim mArea As Double
        Dim mAreaRate As Double
        Dim mRate As Double
        Dim mTotItemAmount As Double
        Dim mPreviousQty As Double

        Dim mItemAmount As Double = 0
        Dim mCGSTAmount As Double = 0
        Dim mSGSTAmount As Double = 0
        Dim mIGSTAmount As Double = 0
        Dim mGrossAmount As Double = 0
        Dim mCGSTPer As Double = 0
        Dim mSGSTPer As Double = 0
        Dim mIGSTPer As Double = 0
        Dim mTotGrossItemAmount As Double = 0

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
            With SprdMain
                j = .MaxRows - 1
                For I = 1 To j
                    .Row = I
                    mGrossAmt = 0

                    .Col = ColItemCode
                    mItemCode = Trim(.Text)

                    mPackingStandard = 0
                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "PACK_STD", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mPackingStandard = MasterNo
                    End If

                    .Col = ColPktQty
                    mPktQty = Val(.Text)

                    .Col = ColItemQty
                    'If mPackingStandard > 0 And Val(.Text) = 0 Then
                    '    mQty = mPackingStandard * mPktQty
                    'Else
                    mQty = Val(.Text)
                    'End If
                    '.Text = mQty

                    .Col = ColMRP
                    mMRP = Val(.Text)

                    If mMRP > 0 Then
                        .Col = ColItemDiscount
                        mDisc = Val(.Text)

                        mPrice = mMRP - (mDisc * 0.01 * mMRP)

                        .Col = ColTODDiscount
                        mDisc = Val(.Text)
                        mPrice = mPrice - (mDisc * 0.01 * mPrice)

                        .Col = ColOtherDiscount
                        mDisc = Val(.Text)
                        mPrice = mPrice - (mDisc * 0.01 * mPrice)

                        .Col = ColItemRate
                        .Text = mPrice
                    End If

                    .Col = ColActualHeight
                    mHeight = Val(.Text)

                    .Col = ColActualWidth
                    mWidth = Val(.Text)

                    If mHeight > 0 And mWidth > 0 Then
                        .Col = ColSize
                        .Text = mWidth & " x " & mHeight
                    End If

                    .Col = ColChargeableHeight
                    If Val(.Text) = 0 Then
                        mChargeableHeight = (mHeight + IIf((mHeight Mod 20) > 0, (20 - (mHeight Mod 20)), 0))
                        .Text = mChargeableHeight
                        mChargeableHeight = Val(.Text) / 1000
                    Else
                        mChargeableHeight = Val(.Text) / 1000
                    End If


                    .Col = ColChargeableWidth
                    If Val(.Text) = 0 Then
                        mChargeableWidth = (mWidth + IIf((mWidth Mod 20) > 0, (20 - (mWidth Mod 20)), 0))
                        .Text = mChargeableWidth
                        mChargeableWidth = Val(.Text) / 1000
                    Else
                        mChargeableWidth = Val(.Text) / 1000
                    End If

                    .Col = ColArea
                    mArea = VB6.Format(mChargeableHeight * mChargeableWidth, "0.0000")
                    .Text = VB6.Format(mArea, "0.0000")


                    .Col = ColAreaRate
                    mAreaRate = Val(.Text)

                    .Col = ColItemRate
                    If mAreaRate > 0 Then
                        .Text = VB6.Format(mArea * mAreaRate, "0.00")           ''Val(.Text)
                    End If
                Next I
            End With
        End If

        mTotGrossItemAmount = 0
        With SprdMain
            j = .MaxRows - 1
            For I = 1 To j
                .Row = I
                .Col = ColItemQty
                mQty = Val(.Text)

                .Col = ColItemRate
                mItemRate = Val(.Text)

                .Col = ColOtherCost
                mOtherCost = Val(.Text)

                .Col = ColFreightCost
                mFreightCost = Val(.Text)

                .Col = ColItemAmount
                If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 115 Then
                    .Text = VB6.Format(mQty * (mItemRate + mOtherCost + mFreightCost), "0.00")
                    mItemAmount = VB6.Format(SprdMain.Text, "0.00")
                    mTotItemAmount = mTotItemAmount + (mQty * (mItemRate + mOtherCost + mFreightCost))
                Else
                    .Text = VB6.Format(mQty * mItemRate, "0.00")
                    mItemAmount = VB6.Format(SprdMain.Text, "0.00")
                    mTotItemAmount = mTotItemAmount + (mQty * mItemRate)
                End If

                SprdMain.Col = ColCGSTPer
                mCGSTPer = Val(SprdMain.Text)

                SprdMain.Col = ColSGSTPer
                mSGSTPer = Val(SprdMain.Text)

                SprdMain.Col = ColIGSTPer
                mIGSTPer = Val(SprdMain.Text)


                mCGSTAmount = VB6.Format(mItemAmount * mCGSTPer * 0.01, "0.00")
                mSGSTAmount = VB6.Format(mItemAmount * mSGSTPer * 0.01, "0.00")
                mIGSTAmount = VB6.Format(mItemAmount * mIGSTPer * 0.01, "0.00")
                mGrossAmount = VB6.Format(mItemAmount + mCGSTAmount + mSGSTAmount + mIGSTAmount, "0.00")
                mTotGrossItemAmount = mTotGrossItemAmount + mGrossAmount

                SprdMain.Col = ColCGSTAmount
                SprdMain.Text = VB6.Format(mCGSTAmount, "0.00")

                SprdMain.Col = ColSGSTAmount
                SprdMain.Text = VB6.Format(mSGSTAmount, "0.00")

                SprdMain.Col = ColIGSTAmount
                SprdMain.Text = VB6.Format(mIGSTAmount, "0.00")

                SprdMain.Col = ColGrossAmount
                SprdMain.Text = VB6.Format(mGrossAmount, "0.00")

            Next
        End With

        lblTotItemValue.Text = Format(mTotItemAmount, "0.00")
        lblTotGrossValue.Text = Format(mTotGrossItemAmount, "0.00")

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value <> 104 Then Exit Sub

        mGrossAmt = 0

        With SprdMain
            j = .MaxRows
            For I = 1 To j
                .Row = I
                mGrossAmt = 0

                .Col = ColItemCode
                mItemCode = Trim(.Text)

                mPackingStandard = 1
                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "PACK_STD", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mPackingStandard = MasterNo
                End If
                mPackingStandard = IIf(mPackingStandard = 0, 1, mPackingStandard)

                .Col = ColPktQty
                mPktQty = Val(.Text)

                .Col = ColItemQty
                mPreviousQty = Val(.Text)

                If mPreviousQty = 0 Then
                    mQty = mPackingStandard * mPktQty
                Else
                    mQty = mPreviousQty
                End If

                .Text = mQty

                .Col = ColMRP
                mMRP = Val(.Text)

                If mMRP > 0 Then
                    .Col = ColItemDiscount
                    mDisc = Val(.Text)

                    mPrice = mMRP - (mDisc * 0.01 * mMRP)

                    .Col = ColTODDiscount
                    mDisc = Val(.Text)
                    mPrice = mPrice - (mDisc * 0.01 * mPrice)

                    .Col = ColOtherDiscount
                    mDisc = Val(.Text)
                    mPrice = mPrice - (mDisc * 0.01 * mPrice)

                    .Col = ColItemRate
                    .Text = mPrice
                End If

                .Col = ColChargeableHeight
                mHeight = Val(.Text) / 1000

                .Col = ColChargeableWidth
                mWidth = Val(.Text) / 1000

                .Col = ColArea
                mArea = VB6.Format(mHeight * mWidth, "0.0000")
                .Text = VB6.Format(mArea, "0.0000")

                .Col = ColActualHeight
                mHeight = Val(.Text)

                .Col = ColActualWidth
                mWidth = Val(.Text)

                .Col = ColSize
                .Text = mHeight & " x " & mWidth

            Next I
        End With



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
        Dim mItemQty As Double
        Dim mItemDiscount As Double
        Dim mTODDiscount As Double
        Dim mOtherDiscount As Double
        Dim mPktQty As Double
        Dim mSize As String
        Dim mModelNo As String
        Dim mDrawingNo As String
        Dim mGlassDescription As String

        Dim mActualHeight As Double
        Dim mActualWidth As Double
        Dim mChargeableHeight As Double
        Dim mChargeableWidth As Double
        Dim mArea As Double
        Dim mAreaRate As Double

        Dim mOtherCost As Double
        Dim mIsVariablePrice As String
        Dim mGrossAmount As Double

        Dim mCGSTAmount As Double
        Dim mSGSTAmount As Double
        Dim mIGSTAmount As Double

        If DeleteDSDailyDetail(PubDBCn, Val(txtSONo.Text)) = False Then GoTo UpdateDetail1

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



                .Col = ColSize
                mSize = MainClass.AllowSingleQuote(.Text)

                .Col = ColModelNo
                mModelNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColDrawingNo
                mDrawingNo = MainClass.AllowSingleQuote(.Text)

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
                mSOStatus = IIf(Trim(.Text) = "", "N", Trim(.Text))
                mSOStatus = IIf(Trim(mSOStatus) = "", "N", Trim(mSOStatus))

                .Col = colRemarks
                mRemarks = Trim(.Text)

                .Col = ColPktQty
                mPktQty = Val(.Text)

                .Col = ColItemQty
                mItemQty = Val(.Text)
                mValidQty = IIf(mItemQty > 0, mItemQty, mValidQty)

                .Col = ColItemDiscount
                mItemDiscount = Val(.Text)

                .Col = ColTODDiscount
                mTODDiscount = Val(.Text)

                .Col = ColOtherDiscount
                mOtherDiscount = Val(.Text)

                .Col = ColGlassDescription
                mGlassDescription = MainClass.AllowSingleQuote(.Text)

                .Col = ColActualHeight
                mActualHeight = Val(.Text)

                .Col = ColActualWidth
                mActualWidth = Val(.Text)

                .Col = ColChargeableHeight
                mChargeableHeight = Val(.Text)

                .Col = ColChargeableWidth
                mChargeableWidth = Val(.Text)

                .Col = ColArea
                mArea = Val(.Text)

                .Col = ColAreaRate
                mAreaRate = Val(.Text)


                .Col = ColOtherCost
                mOtherCost = Val(.Text)

                .Col = ColVariablePrice
                mIsVariablePrice = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                .Col = ColGrossAmount
                mGrossAmount = Val(.Text)


                .Col = ColCGSTAmount
                mCGSTAmount = Val(.Text)

                .Col = ColSGSTAmount
                mSGSTAmount = Val(.Text)

                .Col = ColIGSTAmount
                mIGSTAmount = Val(.Text)

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
                        & " HSN_CODE, REMARKS, SO_ITEM_STATUS, ITEM_SNO, MSP_COST_ADD, ADD_ITEM_DESCRIPTION, CUST_STORE_LOC," & vbCrLf _
                        & " SO_QTY, ITEM_DISC, TOD_DISC, OTH_DISC, PACK_QTY, ITEM_SIZE, ITEM_MODEL, ITEM_DRAWINGNO," & vbCrLf _
                        & " GLASS_DESC, ACTUAL_HEIGHT, ACTUAL_WIDTH," & vbCrLf _
                        & " CHARGEABLE_HEIGHT, CHARGEABLE_WIDTH, GLASS_AREA," & vbCrLf _
                        & " AREA_RATE, OTHER_CODE, IS_VAR_PRICE, GROSS_ITEMAMOUNT, CGST_AMOUNT, SGST_AMOUNT, IGST_AMOUNT" & vbCrLf _
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
                        & " '" & MainClass.AllowSingleQuote(mItemSNo) & "'," & mMSPCostAdd & ",'" & MainClass.AllowSingleQuote(mAddItemDesc) & "','" & MainClass.AllowSingleQuote(mCustStoreLoc) & "'," & vbCrLf _
                        & " " & Val(mItemQty) & "," & Val(mItemDiscount) & "," & Val(mTODDiscount) & "," & Val(mOtherDiscount) & "," & Val(mPktQty) & "," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mSize) & "', '" & MainClass.AllowSingleQuote(mModelNo) & "', '" & MainClass.AllowSingleQuote(mDrawingNo) & "'," & vbCrLf _
                        & " '" & mGlassDescription & "', " & mActualHeight & ", " & mActualWidth & ", " & vbCrLf _
                        & " " & mChargeableHeight & ", " & mChargeableWidth & ", " & mArea & "," & vbCrLf _
                        & " " & mAreaRate & "," & mOtherCost & ", '" & mIsVariablePrice & "', " & mGrossAmount & "," & vbCrLf _
                        & " " & mCGSTAmount & "," & mSGSTAmount & ", " & mIGSTAmount & "" & vbCrLf _
                        & " ) "

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
    Private Sub SearchCode()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " And SUPP_CUST_TYPE IN ('S','C')"

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

        cboReason.Items.Clear()
        cboReason.Items.Add("")
        cboReason.Items.Add("RM Price Change")
        cboReason.Items.Add("New Part")
        cboReason.Items.Add("DCN Change")
        cboReason.Items.Add("Process Amendment")
        cboReason.Items.Add("Others")

        FillCombo()

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
            & " A.AMEND_NO, A.AMEND_DATE,  " & vbCrLf _
            & " A.AMEND_WEF_FROM AS WEF,A.SUPP_CUST_CODE, B.SUPP_CUST_NAME AS NAME, " & vbCrLf _
            & " A.TYPE_OF_SALE, A.TRANSPORTER_DTL, A.MODE_OF_DELV, " & vbCrLf _
            & " DECODE(A.SO_STATUS,'O','Open','Closed') AS STATUS, " & vbCrLf _
            & " A.REMARKS, "

        SqlStr = SqlStr & vbCrLf _
            & " NVL((SELECT SUM(SO_QTY) FROM DSP_SALEORDER_DET WHERE MKEY=A.MKEY),0) AS SO_QTY, "

        SqlStr = SqlStr & vbCrLf _
            & " NVL((SELECT SUM(SO_QTY*ITEM_PRICE) FROM DSP_SALEORDER_DET WHERE MKEY=A.MKEY),0) AS SO_AMOUNT,AUTO_KEY_PI "

        SqlStr = SqlStr & vbCrLf _
            & " FROM DSP_SALEORDER_HDR A, FIN_SUPP_CUST_MST B " & vbCrLf _
            & " WHERE A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf _
            & " AND A.SUPP_CUST_CODE=B.SUPP_CUST_CODE " & vbCrLf _
            & " AND A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ISGSTENABLE_PO='Y'" ''& vbCrLf |            & " AND SUBSTR(AUTO_KEY_SO,LENGTH(AUTO_KEY_SO)-5,4)=" & RsCompany.fields("FYEAR").value & ""


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

            UltraGrid1.DisplayLayout.Bands(0).Columns(14).Header.Caption = "SO Qty"
            UltraGrid1.DisplayLayout.Bands(0).Columns(15).Header.Caption = "SO Amount"

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

            UltraGrid1.DisplayLayout.Bands(0).Columns(14).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(15).Width = 100

            'UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.MFGQty).MaskInput = "99999"

            Me.UltraGrid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True
            Me.UltraGrid1.DisplayLayout.Override.RowSelectorNumberStyle = RowSelectorNumberStyle.RowIndex

            UltraGrid1.DisplayLayout.Bands(0).Columns(14).Header.Appearance.TextHAlign = HAlign.Right
            UltraGrid1.DisplayLayout.Bands(0).Columns(15).Header.Appearance.TextHAlign = HAlign.Right

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

        CurrFormHeight = 7245
        CurrFormWidth = 11355

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
        chkExporterMerchant.CheckState = System.Windows.Forms.CheckState.Unchecked



        txtCustomerName.Text = ""
        txtShipCustomer.Text = ""
        txtStoreDetail.Text = ""
        txtApplicant.Text = ""
        txtCode.Text = ""
        txtCode.Enabled = True
        txtCustomerName.Enabled = True
        txtStoreDetail.Enabled = True
        txtApplicant.Enabled = True
        SprdMain.Enabled = True

        txtPONo.Text = ""
        txtPODate.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        txtEPCGNo.Text = ""
        txtEPCGDate.Text = ""
        txtScheduleAggNo.Text = ""
        txtScheduleAggDate.Text = ""
        txtCustAmendNo.Text = ""
        txtAmendNo.Text = ""
        txtAmendDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtWEF.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        cboStatus.SelectedIndex = 0
        cboPOType.SelectedIndex = 0
        cboReason.SelectedIndex = 0
        cboOrderType.SelectedIndex = IIf(lblType.Text = "O", 0, 1)
        txtRemarks.Text = ""
        txtPINo.Text = ""
        txtPIType.Text = ""


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

        chkShipTo.CheckState = CheckState.Checked
        chkShipTo.Enabled = True
        lblAddUser.Text = ""
        lblAddDate.Text = ""
        lblModUser.Text = ""
        lblModDate.Text = ""
        txtShipCustomer.Enabled = False

        txtShipTo.Enabled = False

        txtBillTo.Enabled = False

        lblTotItemValue.Text = ""
        lblTotGrossValue.Text = ""
        cboProjectName.Text = ""
        cboSalePersonName.Text = ""
        cboPaymentType.Text = ""

        FillComboCustomerName()

        pTempSeq = MainClass.AutoGenRowNo("DSP_DAILY_SCHLD_DET", "RowNo", PubDBCn)

        Call DelTemp_DailyDetail()

        cmdAmendExcel.Enabled = False
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)
        cboStatus.Enabled = False '' IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)
        cboOrderType.Enabled = False '' True

        txtChqNo.Text = ""

        FillCombo()

        MainClass.ButtonStatus(Me, XRIGHT, RsSOMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub
    Private Sub FillCombo()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        Dim RS As ADODB.Recordset
        Dim CntLst As Long

        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim ds1 As New DataSet
        Dim ds2 As New DataSet

        oledbCnn = New OleDbConnection(StrConn)

        oledbCnn.Open()

        SqlStr = "Select DISTINCT NAME, CODE  " & vbCrLf _
                 & " FROM DSP_PROJECT_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        oledbAdapter = New OleDbDataAdapter(SqlStr, oledbCnn)
        oledbAdapter.Fill(ds)

        ' Set the data source and data member to bind the grid.
        cboProjectName.DataSource = ds
        cboProjectName.DataMember = ""
        cboProjectName.DisplayMember = "NAME"
        cboProjectName.ValueMember = "CODE"

        cboProjectName.Appearance.FontData.SizeInPoints = 8.5
        cboProjectName.DisplayLayout.Bands(0).Columns(0).Header.Caption = "Project Name"
        cboProjectName.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Project Code"
        cboProjectName.DisplayLayout.Bands(0).Columns(0).Width = 300
        cboProjectName.DisplayLayout.Bands(0).Columns(1).Width = 35

        cboProjectName.DisplayLayout.Bands(0).Columns(1).Hidden = True

        cboProjectName.DisplayLayout.Appearance.FontData.SizeInPoints = 8.5
        cboProjectName.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList ''List       '' Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown

        oledbAdapter.Dispose()


        '-----

        cboSalePersonName.DataSource = Nothing

        'SqlStr = "Select DISTINCT EMP_NAME, EMP_CODE  " & vbCrLf _
        '         & " FROM PAY_EMPLOYEE_MST"         '' WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
            SqlStr = "Select DISTINCT NAME, CODE  " & vbCrLf _
                 & " FROM FIN_SALESPERSON_MST ORDER BY NAME"

        Else

            SqlStr = "Select DISTINCT EMP_NAME NAME, EMP_CODE CODE " & vbCrLf _
                     & " FROM PAY_EMPLOYEE_MST"         '' WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        End If

        oledbAdapter = New OleDbDataAdapter(SqlStr, oledbCnn)
        oledbAdapter.Fill(ds1)

        ' Set the data source and data member to bind the grid.
        cboSalePersonName.DataSource = ds1
        cboSalePersonName.DataMember = ""
        cboSalePersonName.DisplayMember = "NAME"
        cboSalePersonName.ValueMember = "CODE"

        cboSalePersonName.Appearance.FontData.SizeInPoints = 8.5
        cboSalePersonName.DisplayLayout.Bands(0).Columns(0).Header.Caption = "Sale Person Name"
        cboSalePersonName.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Sale Person Code"
        cboSalePersonName.DisplayLayout.Bands(0).Columns(0).Width = 350
        cboSalePersonName.DisplayLayout.Bands(0).Columns(1).Width = 100

        'cboSalePersonName.DisplayLayout.Bands(0).Columns(1).Hidden = True

        cboSalePersonName.DisplayLayout.Appearance.FontData.SizeInPoints = 8.5
        cboSalePersonName.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList ''List       '' Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown

        oledbAdapter.Dispose()


        '-----
        SqlStr = " Select 'CASH' AS PAY_TYPE FROM DUAL  " & vbCrLf _
                 & " UNION ALL " & vbCrLf _
                 & " Select 'CHEQUE' AS PAY_TYPE FROM DUAL" & vbCrLf _
                 & " UNION ALL" & vbCrLf _
                 & " Select 'DD' AS PAY_TYPE FROM DUAL" & vbCrLf _
                 & " UNION ALL" & vbCrLf _
                 & " Select 'PDC' AS PAY_TYPE FROM DUAL" & vbCrLf _
                 & " UNION ALL" & vbCrLf _
                 & " Select 'BANK TRANSFER' AS PAY_TYPE FROM DUAL" & vbCrLf _
                 & " UNION ALL" & vbCrLf _
                 & " Select 'LC' AS PAY_TYPE FROM DUAL" & vbCrLf _
                 & " UNION ALL" & vbCrLf _
                 & " Select 'OTHER' AS PAY_TYPE FROM DUAL" & vbCrLf _
                 & " UNION ALL" & vbCrLf _
                 & " Select 'ADVANCE' AS PAY_TYPE FROM DUAL" & vbCrLf _
                 & " UNION ALL" & vbCrLf _
                 & " Select 'CREDIT' AS PAY_TYPE FROM DUAL"

        oledbAdapter = New OleDbDataAdapter(SqlStr, oledbCnn)
        oledbAdapter.Fill(ds2)

        ' Set the data source and data member to bind the grid.
        cboPaymentType.DataSource = ds2
        cboPaymentType.DataMember = ""
        cboPaymentType.DisplayMember = "PAY_TYPE"
        cboPaymentType.ValueMember = "PAY_TYPE"

        cboPaymentType.Appearance.FontData.SizeInPoints = 8.5
        cboPaymentType.DisplayLayout.Bands(0).Columns(0).Header.Caption = "Payment Type"
        cboPaymentType.DisplayLayout.Bands(0).Columns(0).Width = 250

        'cboPaymentType.DisplayLayout.Bands(0).Columns(1).Hidden = True

        cboPaymentType.DisplayLayout.Appearance.FontData.SizeInPoints = 8.5
        cboPaymentType.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList ''List       '' Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown

        oledbAdapter.Dispose()
        '---

        oledbCnn.Close()

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mCntCol As Long
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset

        With SprdMain
            .set_RowHeight(-1, ConRowHeight * 1.5)
            .Row = Arow

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsSODetail.Fields("ITEM_CODE").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 8)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104, True, False)

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

            .Col = ColSize
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsSODetail.Fields("ITEM_SIZE").DefinedSize
            .TypeEditMultiLine = True
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 15)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)

            .Col = ColModelNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsSODetail.Fields("ITEM_MODEL").DefinedSize
            .TypeEditMultiLine = True
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 10)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)

            .Col = ColDrawingNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsSODetail.Fields("ITEM_DRAWINGNO").DefinedSize
            .TypeEditMultiLine = True
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 12)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)

            .Col = ColItemSNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsSODetail.Fields("ITEM_SNO").DefinedSize
            .TypeEditMultiLine = True
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 10)
            ''.ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, True, False)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, True, False)

            .Col = ColPartNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsSODetail.Fields("PART_NO").DefinedSize
            .TypeEditMultiLine = True
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104, 30, 20))
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 115, True, False)

            .Col = ColGlassDescription
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = RsSODetail.Fields("GLASS_DESC").DefinedSize ''				
            .set_ColWidth(.Col, 20)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)

            For cntCol = ColActualWidth To ColActualHeight
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.999")
                .TypeFloatMin = CDbl("-999999999.999")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(.Col, 7)
                .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)
            Next

            For cntCol = ColChargeableWidth To ColChargeableHeight
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.999")
                .TypeFloatMin = CDbl("-999999999.999")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(.Col, 7)
                .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)
            Next

            .Col = ColArea
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 9)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)

            .Col = ColAreaRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 9)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)

            '.ScrollBars = ScrollBarsConstants.ScrollBarsNone
            '.CellType = CellTypeConstants.CellTypeComboBox
            '.TypeComboBoxEditable = True
            '.TypeComboBoxAutoSearch = TypeComboAutoSearchConstants.TypeComboBoxAutoSearchMultipleChar
            '.set_ColWidth(.Col, IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104, 30, 20))
            '.EditModePermanent = True
            '.TypeComboBoxList = ""
            'mSqlStr = "SELECT DISTINCT CUSTOMER_PART_NO FROM INV_ITEM_MST WHERE COMPANY_CODE=1"
            '.TypeComboBoxList = ""

            'MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            ''.DataSource = Nothing
            '.DataSource = RsTemp.DataSource

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
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, True, False)


            .Col = ColCustStoreLoc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsSODetail.Fields("CUST_STORE_LOC").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 18)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 115, True, False)


            .Col = ColPreviousItemRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 115, 2, 4)
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsSODetail.Fields("ITEM_PRICE").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 8)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, True, False)

            .Col = ColPktQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 0
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsSODetail.Fields("PACK_QTY").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 7)
            If Mid(cboOrderType.Text, 1, 1) = "C" Then
                .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104, False, True)
            Else
                .ColHidden = True
            End If

            .Col = ColItemQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsSODetail.Fields("ITEM_PRICE").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 7)
            If Mid(cboOrderType.Text, 1, 1) = "C" Then
                .ColHidden = False 'IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)
            Else
                .ColHidden = True
            End If

            .Col = ColItemDetail
            .CellType = SS_CELL_TYPE_BUTTON
            '.Lock = False
            .TypeButtonText = "Details"
            .TypeButtonAlign = SS_CELL_BUTTON_ALIGN_LEFT
            .set_ColWidth(ColItemDetail, 4.5)
            If Mid(cboOrderType.Text, 1, 1) = "C" Then
                .ColHidden = False 'IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)
            Else
                .ColHidden = True
            End If

            .Col = ColMRP
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 115, 2, 4)
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsSODetail.Fields("ITEM_PRICE").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 8)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104, False, True)

            For mCntCol = ColItemDiscount To ColOtherDiscount
                .Col = mCntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.99")
                .TypeFloatMin = CDbl("-999999999.99")
                .TypeEditLen = RsSODetail.Fields("ITEM_PRICE").Precision
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(mCntCol, 7)
                .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104, False, True)
            Next

            .Col = ColItemRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 115, 2, 4)
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsSODetail.Fields("ITEM_PRICE").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 10)

            .Col = ColOtherCost
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsSODetail.Fields("OTHER_CODE").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 115, False, True)
            .set_ColWidth(.Col, 10)

            .Col = ColVariablePrice
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 115, False, True)
            .set_ColWidth(.Col, 6)

            .Col = ColItemAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 115, 2, 4)
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsSODetail.Fields("ITEM_PRICE").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 115, 12, 10))


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
            .set_ColWidth(.Col, IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 115, 10, 8))

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
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, True, False)

            .Col = ColValidDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeEditCharSet = SS_CELL_DATE_FORMAT_DDMMYY
            .TypeDateFormat = FPSpreadADO.TypeDateFormatConstants.TypeDateFormatDDMMYY
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, True, False)
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


            .Col = ColCGSTAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsSODetail.Fields("CGST_AMOUNT").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 9)

            .Col = ColSGSTAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsSODetail.Fields("SGST_AMOUNT").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 9)

            .Col = ColIGSTAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsSODetail.Fields("IGST_AMOUNT").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 9)

            .Col = ColGrossAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsSODetail.Fields("GROSS_ITEMAMOUNT").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 12)

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


            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
                MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColItemCode)
                MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemName, ColItemName)
                MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemUOM, ColHSNCode)
                MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPreviousItemRate, ColPreviousItemRate)
                MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColCGSTPer, ColIGSTPer)
                MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColSOStatus, ColSOStatus)
                MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemQty, ColItemQty)
                MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemAmount, ColItemAmount)
            Else
                MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPartNo, ColItemUOM)
                'MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPartNo, ColPartNo)
                MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPreviousItemRate, ColPreviousItemRate)
                MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColCGSTPer, ColIGSTPer)
                MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColSOStatus, ColSOStatus)
                If Mid(cboOrderType.Text, 1, 1) = "C" Then  ''If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
                    'MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemQty, ColItemQty)
                Else
                    MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemQty, ColItemQty)
                End If
                MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemAmount, ColItemAmount)
            End If

            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColArea, ColArea)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColSize, ColSize)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColCGSTAmount, ColGrossAmount)
            MainClass.SetSpreadColor(SprdMain, Arow)
        End With

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 115 Then
            SprdMain.Row = 0
            SprdMain.Col = ColItemSNo
            SprdMain.Text = "Unique No"
        End If


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

        txtStoreDetail.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
        txtApplicant.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)

        txtPONo.MaxLength = RsSOMain.Fields("CUST_PO_NO").DefinedSize
        txtPODate.MaxLength = 10

        txtEPCGNo.MaxLength = RsSOMain.Fields("EPCG_NO").DefinedSize
        txtEPCGDate.MaxLength = 10

        txtScheduleAggNo.MaxLength = RsSOMain.Fields("SCHD_AGREEMENT_NO").DefinedSize
        txtScheduleAggDate.MaxLength = 10

        txtCustAmendNo.MaxLength = RsSOMain.Fields("CUST_AMEND_NO").DefinedSize
        txtAmendNo.MaxLength = RsSOMain.Fields("AMEND_NO").DefinedSize
        txtAmendDate.MaxLength = 10
        txtWEF.MaxLength = 10
        txtRemarks.MaxLength = RsSOMain.Fields("REMARKS").DefinedSize

        txtPINo.MaxLength = RsSOMain.Fields("AUTO_KEY_PI").DefinedSize
        txtPIType.MaxLength = RsSOMain.Fields("PI_TYPE").DefinedSize

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
        txtChqNo.MaxLength = RsSOMain.Fields("CHEQUE_NO").DefinedSize
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
        Dim mMainItemCode As String
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
        Dim pCGSTPer As Double
        Dim pSGSTPer As Double
        Dim pIGSTPer As Double


        Dim mItemAmount As Double = 0
        Dim mCGSTAmount As Double = 0
        Dim mSGSTAmount As Double = 0
        Dim mIGSTAmount As Double = 0
        Dim mGrossAmount As Double = 0

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
            'If RsSOMain.Fields("SO_APPROVED").Value = "Y" Then
            '    MsgInformation("Approved Sale Order Cann't be Modified")
            '    FieldsVarification = False
            '    Exit Function
            'End If
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

        If Val(txtAmendNo.Text) > 0 And Trim(cboReason.Text) = "" Then
            MsgInformation("Please select the Reason. Cannot Save")
            cboReason.Focus()
            FieldsVarification = False
            Exit Function
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

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
            If Trim(cboSalePersonName.Text) = "" Then
                MsgInformation("Sale Person Name is Blank")
                TabMain.SelectedIndex = 1
                cboSalePersonName.Focus()
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

        Dim mMerchantExporter As String = "N"
        mMerchantExporter = IIf(chkExporterMerchant.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        If mMerchantExporter = "Y" Then
            If MainClass.ValidateWithMasterTable(Trim(txtCustomerName.Text), "SUPP_CUST_NAME", "TYPE_OF_SUPPLIER", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TYPE_OF_SUPPLIER= 'EXPORTER-MERCHANT'") = False Then
                MsgBox("Customer is not defined Merchant Exporter, Please define in Master.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
        End If

        Dim mItemRate As Double = 0
        Dim mProductMRP As Double = 0
        Dim mProductMRPDisc As Double = 0

        For I = 1 To SprdMain.MaxRows - 1
            SprdMain.Row = I
            SprdMain.Col = ColItemCode
            mItemCode = Trim(UCase(SprdMain.Text))


            If RsCompany.Fields("CHECK_MRP_SALEORDER").Value = "Y" Then
                SprdMain.Col = ColItemRate
                mItemRate = Trim(UCase(SprdMain.Text))

                mProductMRP = GetMRPRate((txtWEF.Text), "RATE", mItemCode, "L")
                mProductMRPDisc = GetMRPRate((txtWEF.Text), "RATE_DISC", mItemCode, "L")

                mProductMRP = mProductMRP - (mProductMRP * mProductMRPDisc * 0.01)

                If mProductMRP > 0 And mItemRate > 0 Then
                    If mItemRate < mProductMRP Then
                        MsgBox("Item Price (" & mItemRate & ") Cann't be Less than MRP (" & mProductMRP & ") for Item Code : " & mItemCode & "")
                        MainClass.SetFocusToCell(SprdMain, I, ColHSNCode)
                        FieldsVarification = False
                        Exit Function
                    End If
                End If
            End If

            If RsCompany.Fields("CHECK_BOM_SO").Value = "Y" Then
                Dim SqlStr As String = ""
                Dim pIsFGItem As Boolean = False
                Dim RsTemp As ADODB.Recordset = Nothing

                pIsFGItem = IsFGItem(mItemCode)
                mMainItemCode = GetMainItemCode(mItemCode)

                If pIsFGItem = True Then
                    SqlStr = " SELECT PRODUCT_CODE,IS_APPROVED " & vbCrLf _
                        & " FROM PRD_NEWBOM_HDR" & vbCrLf _
                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND STATUS='O'" & vbCrLf _
                        & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mMainItemCode) & "'"

                    SqlStr = SqlStr & vbCrLf _
                        & " AND WEF=( " & vbCrLf & " SELECT MAX(WEF) FROM PRD_NEWBOM_HDR " & vbCrLf _
                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                        & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(UCase(mMainItemCode)) & "') "

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                    If RsTemp.EOF = True Then
                        MsgInformation("Please Defined B.O.M. For Product Code : " & mItemCode & ". Cann't Be Saved")
                        FieldsVarification = False
                        '                MainClass.SetFocusToCell SprdMain, CntRow, ColItemCode	
                        Exit Function
                    Else
                        If RsTemp.Fields("IS_APPROVED").Value = "N" Then
                            MsgInformation("B.O.M. has not Approved for Product Code : " & mItemCode & ". Cann't Be Saved")
                            FieldsVarification = False
                            '                MainClass.SetFocusToCell SprdMain, CntRow, ColItemCode	
                            Exit Function
                        End If
                    End If
                End If
            End If

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


                If GetHSNDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, "0", mPartyGSTNo,,,, mMerchantExporter) = False Then GoTo err_Renamed

                SprdMain.Col = ColCGSTPer
                SprdMain.Text = VB6.Format(pCGSTPer, "0.00")

                SprdMain.Col = ColSGSTPer
                SprdMain.Text = VB6.Format(pSGSTPer, "0.00")

                SprdMain.Col = ColIGSTPer
                SprdMain.Text = VB6.Format(pIGSTPer, "0.00")


                SprdMain.Col = ColItemAmount
                mItemAmount = VB6.Format(SprdMain.Text, "0.00")

                mCGSTAmount = VB6.Format(mItemAmount * pCGSTPer, "0.00")
                mSGSTAmount = VB6.Format(mItemAmount * pSGSTPer, "0.00")
                mIGSTAmount = VB6.Format(mItemAmount * pIGSTPer, "0.00")
                mGrossAmount = VB6.Format(mItemAmount + mCGSTAmount + mSGSTAmount + mIGSTAmount, "0.00")

                SprdMain.Col = ColCGSTAmount
                SprdMain.Text = VB6.Format(mCGSTAmount, "0.00")

                SprdMain.Col = ColSGSTAmount
                SprdMain.Text = VB6.Format(mSGSTAmount, "0.00")

                SprdMain.Col = ColIGSTAmount
                SprdMain.Text = VB6.Format(mIGSTAmount, "0.00")

                SprdMain.Col = ColGrossAmount
                SprdMain.Text = VB6.Format(mGrossAmount, "0.00")

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

            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
                If txtCode.Text = "11265" Then

                Else
                    If mProdType = "P" Or mProdType = "I" Then
                        SprdMain.Col = ColActualWidth
                        If Val(SprdMain.Text) = 0 Then
                            MsgBox("Please Enter the Actual Width Size.")
                            FieldsVarification = False
                            Exit Function
                        End If
                        SprdMain.Col = ColActualHeight
                        If Val(SprdMain.Text) = 0 Then
                            MsgBox("Please Enter the Actual Height Size.")
                            FieldsVarification = False
                            Exit Function
                        End If
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
                mProdType = GetProductionType(mItemCode)
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

        If Mid(cboOrderType.Text, 1, 1) = "C" Then
            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
                If MainClass.ValidDataInGrid(SprdMain, ColPktQty, "N", "Please Check Packet Qty.") = False Then FieldsVarification = False
                'If MainClass.ValidDataInGrid(SprdMain, ColItemQty, "N", "Please Check Item Qty.") = False Then FieldsVarification = False
            ElseIf RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
                'If MainClass.ValidDataInGrid(SprdMain, ColItemQty, "N", "Please Check Item Qty.") = False Then FieldsVarification = False
            End If
            If MainClass.ValidDataInGrid(SprdMain, ColItemQty, "N", "Please Check Item Qty.") = False Then FieldsVarification = False
        End If

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

            .Col = ColSize
            mItemCode = mItemCode & "-" & UCase(.Text)

            .Col = ColModelNo
            mItemCode = mItemCode & "-" & UCase(.Text)

            For I = 1 To .MaxRows
                .Row = I
                .Col = ColItemCode
                mCheckItemCode = UCase(.Text)

                .Col = ColCustStoreLoc
                mCheckItemCode = mCheckItemCode & "-" & UCase(.Text)

                .Col = ColSize
                mCheckItemCode = mCheckItemCode & "-" & UCase(.Text)

                .Col = ColModelNo
                mCheckItemCode = mCheckItemCode & "-" & UCase(.Text)

                If UCase(mCheckItemCode) = UCase(mItemCode) Then
                    mItemRept = mItemRept + 1
                    If mItemRept > 1 Then
                        CheckDuplicateItem = True
                        MsgInformation("Duplicate Item Code : " & mCheckItemCode & " of Line No : " & I)
                        MainClass.SetFocusToCell(SprdMain, I, ColItemCode)
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
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mHSNDesc As String = ""
        Dim mCGSTPer As Double = 0
        Dim mSGSTPer As Double = 0
        Dim mIGSTPer As Double = 0

        If eventArgs.row = 0 And eventArgs.col = ColItemCode And SprdMain.Enabled = True Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode
                SqlStr = GetSearchItem("C")
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
                SqlStr = GetSearchItem("D")
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

        If eventArgs.row = 0 And eventArgs.col = ColPartNo And SprdMain.Enabled = True Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColPartNo
                SqlStr = GetSearchItem("P")
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColItemCode
                    .Text = Trim(AcName2)
                    .Col = ColItemName
                    .Text = Trim(AcName1)
                    .Col = ColItemUOM
                    .Text = Trim(AcName3)
                    .Col = ColHSNCode
                    .Text = Trim(AcName4)
                    .Col = ColPartNo
                    .Text = Trim(AcName)
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
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColPartNo Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColPartNo, 0))


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
            Case ColPartNo

                If RsCompany.Fields("ERP_CUSTOMER_ID").Value <> 104 Then Exit Sub

                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColPartNo
                If SprdMain.Text = "" Then Exit Sub

                xICode = ""
                If MainClass.ValidateWithMasterTable(Trim(SprdMain.Text), "CUSTOMER_PART_NO", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    xICode = MasterNo
                Else
                    MsgInformation("Invalid Part No.")
                    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColPartNo)
                    Exit Sub
                End If

                If xICode = "" Then Exit Sub
                SprdMain.Col = ColItemCode
                SprdMain.Text = xICode

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
        Dim pMRPRate As Double
        Dim mMerchantExporter As String = "N"
        If mItemCode = "" Then Exit Function
        Dim mItemAmount As Double = 0
        Dim mCGSTAmount As Double = 0
        Dim mSGSTAmount As Double = 0
        Dim mIGSTAmount As Double = 0
        Dim mGrossAmount As Double = 0
        Dim pMRPRateDisc As Double

        mLocal = GetPartyBusinessDetail(Trim(txtCode.Text), Trim(txtBillTo.Text), "WITHIN_STATE")
        mPartyGSTNo = GetPartyBusinessDetail(Trim(txtCode.Text), Trim(txtBillTo.Text), "GST_RGN_NO")


        mMerchantExporter = IIf(chkExporterMerchant.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")


        SqlStr = ""
        SqlStr = " Select INVMST.ITEM_CODE,INVMST.ITEM_SHORT_DESC,INVMST.ISSUE_UOM, " & vbCrLf _
            & " ID.ITEM_RATE,  ID.DISC_PER,ID.CUSTOMER_ITEM_NO , CUSTOMER_PART_NO,ITEM_COLOR," & vbCrLf _
            & " INVMST.CATEGORY_CODE, CMST.SALEINVTYPECODE, CMST.PURCHASEINVTYPECODE, INVMST.HSN_CODE, MAT_WIDTH , MAT_LEN" & vbCrLf _
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

                SprdMain.Col = ColActualWidth
                If Val(SprdMain.Text) = 0 Then
                    SprdMain.Text = IIf(IsDBNull(.Fields("MAT_WIDTH").Value), 0, .Fields("MAT_WIDTH").Value)
                End If

                SprdMain.Col = ColActualHeight
                If Val(SprdMain.Text) = 0 Then
                    SprdMain.Text = IIf(IsDBNull(.Fields("MAT_LEN").Value), 0, .Fields("MAT_LEN").Value)
                End If
                '' , 
                'SprdMain.Col = ColColor
                'SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_COLOR").Value), "", .Fields("ITEM_COLOR").Value)

                'If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
                '    SprdMain.Col = ColMRP
                '    If Val(SprdMain.Text) = 0 Then
                '        pMRPRate = 0
                '        If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "RATE", "INV_ITEM_RATE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '            pMRPRate = Val(MasterNo)
                '        End If
                '        SprdMain.Text = pMRPRate
                '    End If
                'End If

                If RsCompany.Fields("CHECK_MRP_SALEORDER").Value = "Y" Then
                    SprdMain.Col = ColMRP
                    pMRPRate = Val(SprdMain.Text)
                    If pMRPRate = 0 Then
                        pMRPRate = 0
                        pMRPRate = GetMRPRate((txtWEF.Text), "RATE", mItemCode, "L")
                        'If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "RATE", "INV_ITEM_RATE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        '    pMRPRate = Val(MasterNo)
                        'End If
                        SprdMain.Col = ColMRP
                        SprdMain.Text = pMRPRate

                        pMRPRateDisc = 0
                        pMRPRateDisc = GetMRPRate((txtWEF.Text), "RATE_DISC", mItemCode, "L")
                        SprdMain.Col = ColItemDiscount
                        SprdMain.Text = pMRPRateDisc
                    End If
                End If

                SprdMain.Col = ColPreviousItemRate
                SprdMain.Text = CStr(GetPreviousRate((txtCode.Text), Val(txtSONo.Text), Val(txtAmendNo.Text), mItemCode))


                SprdMain.Col = ColItemRate
                If Val(SprdMain.Text) = 0 Then
                    SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_RATE").Value), "", .Fields("ITEM_RATE").Value)))
                End If

                mSaleInvTypeCode = IIf(IsDBNull(.Fields("SALEINVTYPECODE").Value), "", .Fields("SALEINVTYPECODE").Value)


                If VB.Left(cboInvType.Text, 1) = "G" Then
                    If GetHSNDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, "0", mPartyGSTNo,,,, mMerchantExporter) = False Then GoTo ERR1
                Else
                    If GetSACDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, mPartyGSTNo, "G") = False Then GoTo ERR1
                End If

                SprdMain.Col = ColCGSTPer
                SprdMain.Text = VB6.Format(pCGSTPer, "0.00")

                SprdMain.Col = ColSGSTPer
                SprdMain.Text = VB6.Format(pSGSTPer, "0.00")

                SprdMain.Col = ColIGSTPer
                SprdMain.Text = VB6.Format(pIGSTPer, "0.00")

                SprdMain.Col = ColItemAmount
                mItemAmount = VB6.Format(SprdMain.Text, "0.00")

                mCGSTAmount = VB6.Format(mItemAmount * pCGSTPer, "0.00")
                mSGSTAmount = VB6.Format(mItemAmount * pSGSTPer, "0.00")
                mIGSTAmount = VB6.Format(mItemAmount * pIGSTPer, "0.00")
                mGrossAmount = VB6.Format(mItemAmount + mCGSTAmount + mSGSTAmount + mIGSTAmount, "0.00")

                SprdMain.Col = ColCGSTAmount
                SprdMain.Text = VB6.Format(mCGSTAmount, "0.00")

                SprdMain.Col = ColSGSTAmount
                SprdMain.Text = VB6.Format(mSGSTAmount, "0.00")

                SprdMain.Col = ColIGSTAmount
                SprdMain.Text = VB6.Format(mIGSTAmount, "0.00")

                SprdMain.Col = ColGrossAmount
                SprdMain.Text = VB6.Format(mGrossAmount, "0.00")

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
            & " INVMST.CATEGORY_CODE, CMST.SALEINVTYPECODE, CMST.PURCHASEINVTYPECODE, INVMST.HSN_CODE, MAT_WIDTH, MAT_LEN" & vbCrLf _
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

                    SprdMain.Col = ColActualWidth
                    If Val(SprdMain.Text) = 0 Then
                        SprdMain.Text = IIf(IsDBNull(.Fields("MAT_WIDTH").Value), 0, .Fields("MAT_WIDTH").Value)
                    End If

                    SprdMain.Col = ColActualHeight
                    If Val(SprdMain.Text) = 0 Then
                        SprdMain.Text = IIf(IsDBNull(.Fields("MAT_LEN").Value), 0, .Fields("MAT_LEN").Value)
                    End If

                    'SprdMain.Col = ColColor
                    'SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_COLOR").Value), "", .Fields("ITEM_COLOR").Value)

                    'If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
                    '    SprdMain.Col = ColMRP
                    '    If Val(SprdMain.Text) = 0 Then
                    '        pMRPRate = 0
                    '        If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "RATE", "INV_ITEM_RATE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    '            pMRPRate = Val(MasterNo)
                    '        End If
                    '        SprdMain.Text = pMRPRate
                    '    End If
                    'End If

                    If RsCompany.Fields("CHECK_MRP_SALEORDER").Value = "Y" Then
                        SprdMain.Col = ColMRP
                        pMRPRate = Val(SprdMain.Text)
                        If pMRPRate = 0 Then
                            pMRPRate = 0
                            pMRPRate = GetMRPRate((txtWEF.Text), "RATE", mItemCode, "L")
                            'If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "RATE", "INV_ITEM_RATE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            '    pMRPRate = Val(MasterNo)
                            'End If
                            SprdMain.Col = ColMRP
                            SprdMain.Text = pMRPRate

                            pMRPRateDisc = 0
                            pMRPRateDisc = GetMRPRate((txtWEF.Text), "RATE_DISC", mItemCode, "L")
                            SprdMain.Col = ColItemDiscount
                            SprdMain.Text = pMRPRateDisc
                        End If
                    End If

                    SprdMain.Col = ColPreviousItemRate
                    SprdMain.Text = CStr(GetPreviousRate((txtCode.Text), Val(txtSONo.Text), Val(txtAmendNo.Text), mItemCode))


                    SprdMain.Col = ColItemRate
                    If Val(SprdMain.Text) = 0 Then
                        SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_RATE").Value), "", .Fields("ITEM_RATE").Value)))
                    End If

                    mSaleInvTypeCode = IIf(IsDBNull(.Fields("SALEINVTYPECODE").Value), "", .Fields("SALEINVTYPECODE").Value)


                    If VB.Left(cboInvType.Text, 1) = "G" Then
                        If GetHSNDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, "0", mPartyGSTNo,,,, mMerchantExporter) = False Then GoTo ERR1
                    Else
                        If GetSACDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, mPartyGSTNo, "G") = False Then GoTo ERR1
                    End If

                    SprdMain.Col = ColCGSTPer
                    SprdMain.Text = VB6.Format(pCGSTPer, "0.00")

                    SprdMain.Col = ColSGSTPer
                    SprdMain.Text = VB6.Format(pSGSTPer, "0.00")

                    SprdMain.Col = ColIGSTPer
                    SprdMain.Text = VB6.Format(pIGSTPer, "0.00")

                    SprdMain.Col = ColItemAmount
                    mItemAmount = VB6.Format(SprdMain.Text, "0.00")

                    mCGSTAmount = VB6.Format(mItemAmount * pCGSTPer, "0.00")
                    mSGSTAmount = VB6.Format(mItemAmount * pSGSTPer, "0.00")
                    mIGSTAmount = VB6.Format(mItemAmount * pIGSTPer, "0.00")
                    mGrossAmount = VB6.Format(mItemAmount + mCGSTAmount + mSGSTAmount + mIGSTAmount, "0.00")

                    SprdMain.Col = ColCGSTAmount
                    SprdMain.Text = VB6.Format(mCGSTAmount, "0.00")

                    SprdMain.Col = ColSGSTAmount
                    SprdMain.Text = VB6.Format(mSGSTAmount, "0.00")

                    SprdMain.Col = ColIGSTAmount
                    SprdMain.Text = VB6.Format(mIGSTAmount, "0.00")

                    SprdMain.Col = ColGrossAmount
                    SprdMain.Text = VB6.Format(mGrossAmount, "0.00")

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

        'Call AutoCompleteSearch("FIN_SUPP_CUST_BUSINESS_MST ", "LOCATION_ID", "SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'", txtBillTo)



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
    Private Sub txtScheduleAggDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtScheduleAggDate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtScheduleAggDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtScheduleAggDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtScheduleAggDate.Text) = "" Then GoTo EventExitSub

        If IsDate(txtScheduleAggDate.Text) = False Then
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
    Private Sub txtScheduleAggNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtScheduleAggNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtScheduleAggNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtScheduleAggNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtScheduleAggNo.Text)
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

    Private Sub txtCustomerName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCustomerName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCustomerName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
        'FillComboCustomerName()
    End Sub
    Private Sub txtCustomerName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCustomerName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        'If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdsearch, New System.EventArgs())
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

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then ''BONTON
            If Trim(txtVendorCode.Text) = "" Then
                If MainClass.ValidateWithMasterTable((txtCustomerName.Text), "SUPP_CUST_NAME", "GROUP_UID", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
                    txtVendorCode.Text = MasterNo
                End If
            End If
        Else
            If Trim(txtVendorCode.Text) = "" Then
                If MainClass.ValidateWithMasterTable((txtCustomerName.Text), "SUPP_CUST_NAME", "VENDOR_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
                    txtVendorCode.Text = MasterNo
                End If
            End If
        End If

        If Trim(txtBillTo.Text) = "" Then
            txtBillTo.Text = GetDefaultLocation(xAcctCode)
        End If
        'txtShipTo.Text = GetDefaultLocation(xAcctCode)

        If txtBillTo.Text <> "" Then
            If MainClass.ValidateWithMasterTable(txtBillTo.Text, "LOCATION_ID", "SUPP_CUST_ADDR || ',' || SUPP_CUST_CITY || ',' || SUPP_CUST_STATE || ',' || ' GST NO :' || GST_RGN_NO", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'") = True Then
                txtAddress.Text = MasterNo
            End If
        Else
            txtAddress.Text = ""
        End If
        'Call AutoCompleteSearch("FIN_SUPP_CUST_BUSINESS_MST ", "LOCATION_ID", "SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xAcctCode) & "'", txtBillTo)
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
        'cmdsearchShipCust_Click(cmdsearchShipCust, New System.EventArgs())
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
        'If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearchShipCust_Click(cmdsearchShipCust, New System.EventArgs())
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
        If Trim(txtShipTo.Text) = "" Then
            txtShipTo.Text = GetDefaultLocation(xAcctCode)
        End If

        'Call AutoCompleteSearch("FIN_SUPP_CUST_BUSINESS_MST ", "LOCATION_ID", "SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xAcctCode) & "'", txtBillTo)
        'Call AutoCompleteSearch("FIN_SUPP_CUST_BUSINESS_MST ", "LOCATION_ID", "SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xAcctCode) & "'", txtShipTo)

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
        Dim mMerchantExporter As String = "N"

        mMerchantExporter = IIf(chkExporterMerchant.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        MainClass.ClearGrid(SprdMain, ConRowHeight)

        If Trim(txtBillTo.Text) = "" Then Exit Sub

        mLocal = GetPartyBusinessDetail(Trim(txtCode.Text), Trim(txtBillTo.Text), "WITHIN_STATE")
        mPartyGSTNo = GetPartyBusinessDetail(Trim(txtCode.Text), Trim(txtBillTo.Text), "GST_RGN_NO")

        SqlStr = " SELECT IH.PAYMENT_CODE, IH.DELIVERY, IH.EXCISE_OTHERS, " & vbCrLf _
            & " IH.MODE_DESPATCH, IH.INSPECTION, IH.PACKING_FORWARDING, " & vbCrLf _
            & " IH.INSURANCE, IH.OTHERS_COND1, IH.OTHERS_COND2, " & vbCrLf _
            & " ID.ITEM_CODE,  INVMST.PURCHASE_UOM, INVMST.ITEM_SHORT_DESC, " & vbCrLf _
            & " ID.ITEM_RATE,  ID.DISC_PER,ID.CUSTOMER_ITEM_NO AS CUSTOMER_PART_NO, INVMST.CUSTOMER_PART_NO AS CUSTOMER_PART_NO_ITEM, INVMST.ITEM_COLOR,MAT_WIDTH , MAT_LEN " & vbCrLf _
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

                    SprdMain.Col = ColActualWidth
                    SprdMain.Text = IIf(IsDBNull(RsTemp.Fields("MAT_WIDTH").Value), 0, RsTemp.Fields("MAT_WIDTH").Value)

                    SprdMain.Col = ColActualHeight
                    SprdMain.Text = IIf(IsDBNull(RsTemp.Fields("MAT_LEN").Value), 0, RsTemp.Fields("MAT_LEN").Value)

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
                        If GetHSNDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, "0", mPartyGSTNo,,,, mMerchantExporter) = False Then GoTo ErrPart
                    Else
                        If GetSACDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, mPartyGSTNo, "G") = False Then GoTo ErrPart
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


                mBillToShipToSame = Trim(IIf(IsDBNull(.Fields("SHIPPED_TO_SAMEPARTY").Value), "", .Fields("SHIPPED_TO_SAMEPARTY").Value))

                chkShipTo.CheckState = IIf(mBillToShipToSame = "Y", CheckState.Checked, CheckState.Unchecked)


                Dim mAccountStoreCode As String = IIf(IsDBNull(.Fields("SUPP_CUST_STORE_CODE").Value), -1, .Fields("SUPP_CUST_STORE_CODE").Value)
                txtStoreDetail.Text = ""
                If MainClass.ValidateWithMasterTable(mAccountStoreCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtStoreDetail.Text = MasterNo
                End If

                Dim mAppliciantCode As String = IIf(IsDBNull(.Fields("SUPP_CUST_APPLICANT_CODE").Value), -1, .Fields("SUPP_CUST_APPLICANT_CODE").Value)
                txtApplicant.Text = ""
                If MainClass.ValidateWithMasterTable(mAppliciantCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtApplicant.Text = MasterNo
                End If



                txtVendorCode.Text = IIf(IsDBNull(.Fields("VENDOR_CODE").Value), "", .Fields("VENDOR_CODE").Value)

                If mBillToShipToSame = "Y" Then
                    txtShipCustomer.Text = mAccountName
                    mShipAccountCode = mAccountCode

                    txtShipCustomer.Enabled = False
                    txtShipTo.Enabled = False
                Else
                    mShipAccountCode = IIf(IsDBNull(.Fields("SHIPPED_TO_PARTY_CODE").Value), -1, .Fields("SHIPPED_TO_PARTY_CODE").Value)
                    If MainClass.ValidateWithMasterTable(mShipAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mShipToAccountName = MasterNo
                    End If

                    txtShipCustomer.Text = mShipToAccountName

                    txtShipCustomer.Enabled = True
                    txtShipTo.Enabled = True
                End If

                txtBillTo.Text = IIf(IsDBNull(.Fields("BILL_TO_LOC_ID").Value), "", .Fields("BILL_TO_LOC_ID").Value)
                txtShipTo.Text = IIf(IsDBNull(.Fields("SHIP_TO_LOC_ID").Value), "", .Fields("SHIP_TO_LOC_ID").Value)

                If MainClass.ValidateWithMasterTable(txtBillTo.Text, "LOCATION_ID", "SUPP_CUST_ADDR || ',' || SUPP_CUST_CITY || ',' || SUPP_CUST_STATE || ',' || ' GST NO :' || GST_RGN_NO", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'") = True Then
                    txtAddress.Text = MasterNo
                End If

                txtPONo.Text = IIf(IsDBNull(.Fields("CUST_PO_NO").Value), "", .Fields("CUST_PO_NO").Value)
                txtPODate.Text = VB6.Format(IIf(IsDBNull(.Fields("CUST_PO_DATE").Value), "", .Fields("CUST_PO_DATE").Value), "DD/MM/YYYY")

                txtEPCGNo.Text = IIf(IsDBNull(.Fields("EPCG_NO").Value), "", .Fields("EPCG_NO").Value)
                txtEPCGDate.Text = VB6.Format(IIf(IsDBNull(.Fields("EPCG_DATE").Value), "", .Fields("EPCG_DATE").Value), "DD/MM/YYYY")

                txtScheduleAggNo.Text = IIf(IsDBNull(.Fields("SCHD_AGREEMENT_NO").Value), "", .Fields("SCHD_AGREEMENT_NO").Value)
                txtScheduleAggDate.Text = VB6.Format(IIf(IsDBNull(.Fields("SCHD_AGREEMENT_DATE").Value), "", .Fields("SCHD_AGREEMENT_DATE").Value), "DD/MM/YYYY")

                txtCustAmendNo.Text = IIf(IsDBNull(.Fields("CUST_AMEND_NO").Value), "", .Fields("CUST_AMEND_NO").Value)
                txtAmendNo.Text = IIf(IsDBNull(.Fields("AMEND_NO").Value), "", .Fields("AMEND_NO").Value)
                txtAmendDate.Text = VB6.Format(IIf(IsDBNull(.Fields("AMEND_DATE").Value), "", .Fields("AMEND_DATE").Value), "DD/MM/YYYY")
                txtWEF.Text = VB6.Format(IIf(IsDBNull(.Fields("AMEND_WEF_FROM").Value), "", .Fields("AMEND_WEF_FROM").Value), "DD/MM/YYYY")
                cboStatus.SelectedIndex = IIf(.Fields("SO_STATUS").Value = "O", 0, 1)
                cboPOType.SelectedIndex = IIf(.Fields("PO_TYPE").Value = "R", 0, IIf(.Fields("PO_TYPE").Value = "S", 1, 2))

                cboReason.Text = IIf(IsDBNull(.Fields("PO_AMEND_REASON").Value), "", .Fields("PO_AMEND_REASON").Value)

                cboStatus.Enabled = False       ''IIf(PubSuperUser = "U", False, IIf(.Fields("SO_STATUS").Value = "O", True, False))
                cmdAmend.Enabled = IIf(.Fields("SO_STATUS").Value = "C", False, True)

                chkApproved.CheckState = IIf(.Fields("SO_APPROVED").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                txtCode.Enabled = False
                txtCustomerName.Enabled = IIf(chkApproved.CheckState = System.Windows.Forms.CheckState.Unchecked, True, IIf(PubUserID = "G0416", True, False))

                chkDI.CheckState = IIf(.Fields("DELIVERY_INSTRUCTION_REQ").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkExporterMerchant.CheckState = IIf(.Fields("EXPORTER_MERCHANT").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                ''

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

                txtPINo.Text = IIf(IsDBNull(.Fields("AUTO_KEY_PI").Value), "", .Fields("AUTO_KEY_PI").Value)
                txtPIType.Text = IIf(IsDBNull(.Fields("PI_TYPE").Value), "", .Fields("PI_TYPE").Value)



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

                cboProjectName.Value = IIf(IsDBNull(.Fields("PROJECT_CODE").Value), "", .Fields("PROJECT_CODE").Value)
                cboSalePersonName.Value = IIf(IsDBNull(.Fields("SALE_PERSON_CODE").Value), "", .Fields("SALE_PERSON_CODE").Value)
                cboPaymentType.Value = IIf(IsDBNull(.Fields("PAYMENT_TYPE").Value), "", .Fields("PAYMENT_TYPE").Value)
                txtChqNo.Text = IIf(IsDBNull(.Fields("CHEQUE_NO").Value), "", .Fields("CHEQUE_NO").Value)

                lblAddUser.Text = IIf(IsDBNull(.Fields("ADDUSER").Value), "", .Fields("ADDUSER").Value)
                lblAddDate.Text = VB6.Format(IIf(IsDBNull(.Fields("ADDDATE").Value), "", .Fields("ADDDATE").Value), "DD/MM/YYYY")
                lblModUser.Text = IIf(IsDBNull(.Fields("MODUSER").Value), "", .Fields("MODUSER").Value)
                lblModDate.Text = VB6.Format(IIf(IsDBNull(.Fields("MODDATE").Value), "", .Fields("MODDATE").Value), "DD/MM/YYYY")


                Call ShowDetail1()

                Call ShowDSDailyDetail()
                'Call AutoCompleteSearch("FIN_SUPP_CUST_BUSINESS_MST ", "LOCATION_ID", "SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'", txtBillTo)
                'Call AutoCompleteSearch("FIN_SUPP_CUST_BUSINESS_MST ", "LOCATION_ID", "SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mShipAccountCode) & "'", txtShipTo)

            End With
        End If
        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = True
        txtSONo.Enabled = True
        cmdSearchAmend.Enabled = True


        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColItemCode)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemName, ColItemName)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemUOM, ColHSNCode)
        Else
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemName, ColItemUOM)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPartNo, ColPartNo)
        End If

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
        SqlStr = " SELECT * " & vbCrLf _
            & " FROM DSP_SALEORDER_DET " & vbCrLf _
            & " Where " & vbCrLf _
            & " MKEY=" & Val(lblMkey.Text) & "" & vbCrLf & " Order By SERIAL_NO"

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

                SprdMain.Col = ColSize
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("ITEM_SIZE").Value), "", .Fields("ITEM_SIZE").Value))

                SprdMain.Col = ColModelNo
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("ITEM_MODEL").Value), "", .Fields("ITEM_MODEL").Value))

                SprdMain.Col = ColDrawingNo
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("ITEM_DRAWINGNO").Value), "", .Fields("ITEM_DRAWINGNO").Value))


                '            mHSNCode = GetHSNCode(mItemCode)

                SprdMain.Col = ColHSNCode
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("HSN_CODE").Value), "", .Fields("HSN_CODE").Value))

                SprdMain.Col = ColGlassDescription
                SprdMain.Text = IIf(IsDBNull(.Fields("GLASS_DESC").Value), "", .Fields("GLASS_DESC").Value)

                SprdMain.Col = ColActualHeight
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ACTUAL_HEIGHT").Value), 0, .Fields("ACTUAL_HEIGHT").Value)))

                SprdMain.Col = ColActualWidth
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ACTUAL_WIDTH").Value), 0, .Fields("ACTUAL_WIDTH").Value)))

                SprdMain.Col = ColChargeableHeight
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("CHARGEABLE_HEIGHT").Value), 0, .Fields("CHARGEABLE_HEIGHT").Value)))

                SprdMain.Col = ColChargeableWidth
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("CHARGEABLE_WIDTH").Value), 0, .Fields("CHARGEABLE_WIDTH").Value)))

                SprdMain.Col = ColArea
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("GLASS_AREA").Value), 0, .Fields("GLASS_AREA").Value)))

                SprdMain.Col = ColAreaRate
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("AREA_RATE").Value), 0, .Fields("AREA_RATE").Value)))

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

                SprdMain.Col = ColPktQty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("PACK_QTY").Value), 0, .Fields("PACK_QTY").Value)))

                SprdMain.Col = ColItemQty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("SO_QTY").Value), 0, .Fields("SO_QTY").Value)))

                SprdMain.Col = ColItemDiscount
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_DISC").Value), 0, .Fields("ITEM_DISC").Value)))

                SprdMain.Col = ColTODDiscount
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("TOD_DISC").Value), 0, .Fields("TOD_DISC").Value)))

                SprdMain.Col = ColOtherDiscount
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("OTH_DISC").Value), 0, .Fields("OTH_DISC").Value)))

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



                SprdMain.Col = ColOtherCost
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("OTHER_CODE").Value), 0, .Fields("OTHER_CODE").Value)))

                SprdMain.Col = ColVariablePrice
                SprdMain.Value = IIf(.Fields("IS_VAR_PRICE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                SprdMain.Col = ColGrossAmount
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("GROSS_ITEMAMOUNT").Value), 0, .Fields("GROSS_ITEMAMOUNT").Value)))

                SprdMain.Col = ColCGSTAmount
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("CGST_AMOUNT").Value), 0, .Fields("CGST_AMOUNT").Value)))

                SprdMain.Col = ColSGSTAmount
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("SGST_AMOUNT").Value), 0, .Fields("SGST_AMOUNT").Value)))

                SprdMain.Col = ColIGSTAmount
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("IGST_AMOUNT").Value), 0, .Fields("IGST_AMOUNT").Value)))



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
        Call CalcTots()
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

        If mByCode = "C" Then
            mSqlStr = " SELECT A.ITEM_CODE,A.ITEM_SHORT_DESC, A.ISSUE_UOM, A.HSN_CODE, A.CUSTOMER_PART_NO"
        ElseIf mByCode = "D" Then
            mSqlStr = " SELECT A.ITEM_SHORT_DESC,A.ITEM_CODE, A.ISSUE_UOM, A.HSN_CODE, A.CUSTOMER_PART_NO"
        Else
            mSqlStr = " SELECT A.CUSTOMER_PART_NO,A.ITEM_SHORT_DESC,A.ITEM_CODE, A.ISSUE_UOM, A.HSN_CODE"
        End If

        mSqlStr = mSqlStr & vbCrLf & " "

        'mSqlStr = mSqlStr & vbCrLf _
        '    & " FROM INV_ITEM_MST A, FIN_SUPP_CUST_DET B" & vbCrLf _
        '    & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        '    & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf _
        '    & " AND A.ITEM_CODE=B.ITEM_CODE " & vbCrLf _
        '    & " AND B.SUPP_CUST_CODE='" & xSuppCode & "'"

        mSqlStr = mSqlStr & vbCrLf & " FROM INV_ITEM_MST A" & vbCrLf _
            & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND A.ITEM_STATUS='A'"

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
        'cmdBillToSearch_Click(cmdBillToSearch, New System.EventArgs())
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
        'If KeyCode = System.Windows.Forms.Keys.F1 Then cmdBillToSearch_Click(cmdBillToSearch, New System.EventArgs())
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

    Private Sub txtShipTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtShipTo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtShipTo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtShipTo.DoubleClick
        'cmdShipToSearch_Click(cmdShipToSearch, New System.EventArgs())
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
        'If KeyCode = System.Windows.Forms.Keys.F1 Then cmdShipToSearch_Click(cmdShipToSearch, New System.EventArgs())
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

    Private Sub chkShipTo_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkShipTo.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        If chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtShipCustomer.Enabled = False
            txtShipTo.Enabled = False
        Else
            txtShipCustomer.Enabled = True
            txtShipTo.Enabled = True
        End If
    End Sub

    Private Sub UltraGrid1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles UltraGrid1.KeyPress
        'If e.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdMain.ActiveCol, SprdMain.ActiveRow))
    End Sub

    Private Sub frmSalesOrderGST_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        If KeyAscii = 6 Then
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

                MainClass.SetFocusToCell(SprdMain, cntSearchRow, ColItemRate)
                cntSearchRow = cntSearchRow + 1
                cntSearchCol = cntSearchCol + 1
            End If
        End If
    End Sub
    Private Sub ReportonPO(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""
        Dim SqlStr As String = ""
        Dim mRptFileName As String
        Dim mVNo As String

        Dim CntRow As Integer
        Dim mItemCode As String

        Dim mPOWEF As String


        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        SqlStr = ""
        mSubTitle = ""

        mTitle = mTitle & "Sales Order"

        If Val(txtAmendNo.Text) > 0 Then
            mSubTitle = mSubTitle & "-AMENDMENT"
        End If

        Call MainClass.ClearCRptFormulas(Report1)


        Call SelectQryForPO(SqlStr)
        mRptFileName = "SALEORDER.rpt"


        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        'frmPrintPO.Close()
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'frmPrintPO.Close()
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String)
        'Dim Printer As New Printer			

        On Error GoTo ErrPart
        Dim mAmountInword As String
        Dim mCurr As String
        Dim CntRow As Integer
        Dim mItemValue As Double
        Dim SqlStrSub As String
        Dim mService As String
        Dim mShipToName As String = ""

        Dim SqlStr As String = ""
        Dim RsTempShip As ADODB.Recordset = Nothing
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim mShipToAddress As String = ""
        Dim mShipToCity As String = ""
        Dim mShipToPinCode As String = ""
        Dim mShipToState As String = ""
        Dim mShipToGSTN As String = ""
        Dim mShipToStateCode As String = ""
        Dim mShipToLocation As String = ""

        Dim mShipLocName As String = ""
        Dim mShipLocAddress As String = ""
        Dim mShipLocCity As String = ""
        Dim mShipLocState As String = ""
        Dim mShipLocStateCode As String = ""
        Dim mPONo As String
        Dim mFyearFrom As String
        Dim mFyearTo As String

        Dim mRegdAddress As String = ""
        Dim mRegdCity As String = ""
        Dim mRegdPhone As String = ""

        Dim mCompanyAdd1 As String = ""
        Dim mCompanyCity As String = ""
        Dim mCompanyPhone As String = ""
        Dim meMail As String = ""
        Dim mCompanyPAN As String = ""
        Dim mJurisdiction As String
        Dim mShipContactNo As String

        Dim mShipToPANno As String = ""
        Dim mShipToPhoneNo As String = ""
        Dim mShipToMailID As String = ""

        Dim mStoreName As String = ""
        Dim mStoreAddress As String = ""
        Dim mStoreCity As String = ""
        Dim mStoreState As String = ""
        Dim mStoreGSTN As String = ""

        Dim mApplicantName As String = ""
        Dim mApplicantAddress As String = ""
        Dim mApplicantCity As String = ""
        Dim mApplicantState As String = ""
        Dim mApplicantGSTN As String = ""

        'If UCase(mRptFileName) = "PO_PRN_UNIT1.RPT" Then
        '    SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, True, pmyMenu)
        'Else
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        'End If


        If chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked Then
            mShipToName = txtCustomerName.Text
            mShipToLocation = txtBillTo.Text
        Else
            mShipToName = txtShipCustomer.Text
            mShipToLocation = txtShipTo.Text
        End If

        'mPartyGSTNo = GetPartyBusinessDetail(Trim(txtCode.Text), Trim(txtBillTo.Text), "GST_RGN_NO")

        'SqlStr = " SELECT SUPP_CUST_NAME, SUPP_CUST_ADDR, SUPP_CUST_CITY, SUPP_CUST_STATE,  " & vbCrLf _
        '    & " SUPP_CUST_PIN, GST_RGN_NO" & vbCrLf _
        '    & " FROM FIN_SUPP_CUST_MST " & vbCrLf _
        '    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        '    & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(mShipToName) & "'"


        mCompanyPhone = IIf(IsDBNull(RsCompany.Fields("COMPANY_PHONE").Value), "", "Phone : " & RsCompany.Fields("COMPANY_PHONE").Value)
        mCompanyPhone = mCompanyPhone & IIf(IsDBNull(RsCompany.Fields("COMPANY_FAXNO").Value), "", " Fax : " & RsCompany.Fields("COMPANY_FAXNO").Value)

        meMail = IIf(IsDBNull(RsCompany.Fields("COMPANY_MAILID").Value), "", "EMail : " & RsCompany.Fields("COMPANY_MAILID").Value)
        meMail = meMail & IIf(IsDBNull(RsCompany.Fields("WEBSITE").Value), "", "  Web : " & RsCompany.Fields("WEBSITE").Value)

        mCompanyPAN = IIf(IsDBNull(RsCompany.Fields("PAN_NO").Value), "", RsCompany.Fields("PAN_NO").Value)
        mJurisdiction = IIf(IsDBNull(RsCompany.Fields("JURISDICTION").Value), "", RsCompany.Fields("JURISDICTION").Value)

        MainClass.AssignCRptFormulas(Report1, "CompanyPAN=""" & mCompanyPAN & """")
        MainClass.AssignCRptFormulas(Report1, "CompanyeMail=""" & meMail & """")
        MainClass.AssignCRptFormulas(Report1, "CompanyPhone=""" & mCompanyPhone & """")
        MainClass.AssignCRptFormulas(Report1, "Jurisdiction=""" & mJurisdiction & """")


        SqlStr = "SELECT A.*, B.SUPP_CUST_NAME,PAN_NO FROM FIN_SUPP_CUST_BUSINESS_MST A, FIN_SUPP_CUST_MST B" & vbCrLf _
            & " Where A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND A.COMPANY_CODE=B.COMPANY_CODE AND A.SUPP_CUST_CODE=B.SUPP_CUST_CODE" & vbCrLf _
            & " AND B.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(mShipToName) & "' AND A.LOCATION_ID='" & MainClass.AllowSingleQuote(mShipToLocation) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempShip, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTempShip.EOF = False Then
            mShipToName = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_NAME").Value), "", RsTempShip.Fields("SUPP_CUST_NAME").Value)
            mShipToAddress = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_ADDR").Value), "", RsTempShip.Fields("SUPP_CUST_ADDR").Value)
            mShipToAddress = Replace(mShipToAddress, vbCrLf, "")

            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
                mShipToCity = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_CITY").Value), "", RsTempShip.Fields("SUPP_CUST_CITY").Value)
            Else
                mShipToCity = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_CITY").Value), "", RsTempShip.Fields("SUPP_CUST_CITY").Value)
                mShipToCity = mShipToCity & " " & IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_PIN").Value), "", RsTempShip.Fields("SUPP_CUST_PIN").Value)
            End If

            mShipToPinCode = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_PIN").Value), "", RsTempShip.Fields("SUPP_CUST_PIN").Value)
            mShipToState = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_STATE").Value), "", RsTempShip.Fields("SUPP_CUST_STATE").Value)
            mShipToStateCode = GetStateCode(mShipToState)
            mShipToGSTN = IIf(IsDBNull(RsTempShip.Fields("GST_RGN_NO").Value), "", RsTempShip.Fields("GST_RGN_NO").Value)
            mShipToPANno = IIf(IsDBNull(RsTempShip.Fields("PAN_NO").Value), "", RsTempShip.Fields("PAN_NO").Value)
            mShipToPhoneNo = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_PHONE").Value), "", RsTempShip.Fields("SUPP_CUST_PHONE").Value) ' , 
            mShipToMailID = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_MAILID").Value), "", RsTempShip.Fields("SUPP_CUST_MAILID").Value)
        End If



        MainClass.AssignCRptFormulas(Report1, "mShipToName=""" & mShipToName & """")
        MainClass.AssignCRptFormulas(Report1, "mShipToAddress=""" & mShipToAddress & """")
        MainClass.AssignCRptFormulas(Report1, "mShipToCity=""" & mShipToCity & """")
        MainClass.AssignCRptFormulas(Report1, "mShipToGSTN=""" & mShipToGSTN & """")

        MainClass.AssignCRptFormulas(Report1, "mShipToState=""" & mShipToState & """")
        '    MainClass.AssignCRptFormulas Report1, "mShipToStateCode=""" & mShipToStateCode & """"			

        '    MainClass.AssignCRptFormulas Report1, "mStateName=""" & mStateName & """"			
        '    MainClass.AssignCRptFormulas Report1, "mStateCode=""" & mStateCode & """"			
        '    MainClass.AssignCRptFormulas Report1, "mPlaceofSupply=""" & mPlaceofSupply & """"			

        '
        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 115 Then
            If txtStoreDetail.Text <> "" Then
                SqlStr = "SELECT * FROM FIN_SUPP_CUST_MST " & vbCrLf _
                   & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                   & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtStoreDetail.Text) & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempShip, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTempShip.EOF = False Then
                    mStoreName = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_NAME").Value), "", RsTempShip.Fields("SUPP_CUST_NAME").Value)
                    mStoreAddress = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_ADDR").Value), "", RsTempShip.Fields("SUPP_CUST_ADDR").Value)
                    mStoreAddress = Replace(mStoreAddress, vbCrLf, "")

                    mStoreCity = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_CITY").Value), "", RsTempShip.Fields("SUPP_CUST_CITY").Value)
                    mStoreCity = mStoreCity & " " & IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_PIN").Value), "", RsTempShip.Fields("SUPP_CUST_PIN").Value)

                    mStoreState = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_STATE").Value), "", RsTempShip.Fields("SUPP_CUST_STATE").Value)
                    mStoreGSTN = IIf(IsDBNull(RsTempShip.Fields("GST_RGN_NO").Value), "", RsTempShip.Fields("GST_RGN_NO").Value)
                    'mStorePANno = IIf(IsDBNull(RsTempShip.Fields("PAN_NO").Value), "", RsTempShip.Fields("PAN_NO").Value)
                    'mStorePhoneNo = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_PHONE").Value), "", RsTempShip.Fields("SUPP_CUST_PHONE").Value) ' , 
                    'mStoreMailID = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_MAILID").Value), "", RsTempShip.Fields("SUPP_CUST_MAILID").Value)
                End If
            End If

            MainClass.AssignCRptFormulas(Report1, "mStoreName=""" & mStoreName & """")
            MainClass.AssignCRptFormulas(Report1, "mStoreAddress=""" & mStoreAddress & """")
            MainClass.AssignCRptFormulas(Report1, "mStoreCity=""" & mStoreCity & """")
            MainClass.AssignCRptFormulas(Report1, "mStoreState=""" & mStoreState & """")
            MainClass.AssignCRptFormulas(Report1, "mStoreGSTN=""" & mStoreGSTN & """")



            If txtApplicant.Text <> "" Then
                SqlStr = "SELECT * FROM FIN_SUPP_CUST_MST " & vbCrLf _
                   & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                   & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtApplicant.Text) & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempShip, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTempShip.EOF = False Then
                    mApplicantName = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_NAME").Value), "", RsTempShip.Fields("SUPP_CUST_NAME").Value)
                    mApplicantAddress = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_ADDR").Value), "", RsTempShip.Fields("SUPP_CUST_ADDR").Value)
                    mApplicantAddress = Replace(mApplicantAddress, vbCrLf, "")

                    mApplicantCity = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_CITY").Value), "", RsTempShip.Fields("SUPP_CUST_CITY").Value)
                    mApplicantCity = mApplicantCity & " " & IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_PIN").Value), "", RsTempShip.Fields("SUPP_CUST_PIN").Value)

                    mApplicantState = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_STATE").Value), "", RsTempShip.Fields("SUPP_CUST_STATE").Value)
                    mApplicantGSTN = IIf(IsDBNull(RsTempShip.Fields("GST_RGN_NO").Value), "", RsTempShip.Fields("GST_RGN_NO").Value)
                    'mApplicantPANno = IIf(IsDBNull(RsTempShip.Fields("PAN_NO").Value), "", RsTempShip.Fields("PAN_NO").Value)
                    'mApplicantPhoneNo = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_PHONE").Value), "", RsTempShip.Fields("SUPP_CUST_PHONE").Value) ' , 
                    'mApplicantMailID = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_MAILID").Value), "", RsTempShip.Fields("SUPP_CUST_MAILID").Value)
                End If
            End If


            MainClass.AssignCRptFormulas(Report1, "mApplicantName=""" & mApplicantName & """")
            MainClass.AssignCRptFormulas(Report1, "mApplicantAddress=""" & mApplicantAddress & """")
            MainClass.AssignCRptFormulas(Report1, "mApplicantCity=""" & mApplicantCity & """")
            MainClass.AssignCRptFormulas(Report1, "mApplicantState=""" & mApplicantState & """")
            MainClass.AssignCRptFormulas(Report1, "mApplicantGSTN=""" & mApplicantGSTN & """")


        End If


        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
            MainClass.AssignCRptFormulas(Report1, "TINNo=""" & mShipToStateCode & """")
            MainClass.AssignCRptFormulas(Report1, "ExciseRegnNo=""" & mShipToPinCode & """")
            MainClass.AssignCRptFormulas(Report1, "ECCNo=""" & mShipToPANno & """")
            MainClass.AssignCRptFormulas(Report1, "Division=""" & mShipToPhoneNo & """")
            MainClass.AssignCRptFormulas(Report1, "Range=""" & mShipToMailID & """")
            Dim mStateCode As String

            SqlStr = "SELECT A.*, B.SUPP_CUST_NAME FROM FIN_SUPP_CUST_BUSINESS_MST A, FIN_SUPP_CUST_MST B" & vbCrLf _
                & " Where A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND A.COMPANY_CODE=B.COMPANY_CODE AND A.SUPP_CUST_CODE=B.SUPP_CUST_CODE" & vbCrLf _
                & " AND B.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtCustomerName.Text) & "' AND A.LOCATION_ID='" & MainClass.AllowSingleQuote(txtBillTo.Text) & "'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            mStateCode = ""
            If RsTemp.EOF = False Then
                mStateCode = GetStateCode(IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_STATE").Value), "", RsTemp.Fields("SUPP_CUST_STATE").Value))
            End If

            MainClass.AssignCRptFormulas(Report1, "PANNo=""" & mStateCode & """")
        Else
            MainClass.AssignCRptFormulas(Report1, "TINNo=""" & IIf(IsDBNull(RsCompany.Fields("TINNO").Value), "", RsCompany.Fields("TINNO").Value) & """")
            MainClass.AssignCRptFormulas(Report1, "ExciseRegnNo=""" & IIf(IsDBNull(RsCompany.Fields("CENT_EXC_RGN_NO").Value), "", RsCompany.Fields("CENT_EXC_RGN_NO").Value) & """")
            MainClass.AssignCRptFormulas(Report1, "ECCNo=""" & IIf(IsDBNull(RsCompany.Fields("ECC_NO").Value), "", RsCompany.Fields("ECC_NO").Value) & """")
            MainClass.AssignCRptFormulas(Report1, "Division=""" & IIf(IsDBNull(RsCompany.Fields("EXCISE_DIV").Value), "", RsCompany.Fields("EXCISE_DIV").Value) & """")
            MainClass.AssignCRptFormulas(Report1, "Range=""" & IIf(IsDBNull(RsCompany.Fields("EXCISE_RANGE").Value), "", RsCompany.Fields("EXCISE_RANGE").Value) & """")
            MainClass.AssignCRptFormulas(Report1, "PANNo=""" & IIf(IsDBNull(RsCompany.Fields("PAN_NO").Value), "", RsCompany.Fields("PAN_NO").Value) & """")
        End If

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
            MainClass.AssignCRptFormulas(Report1, "SalePersonName=""" & cboSalePersonName.Text & """")
            MainClass.AssignCRptFormulas(Report1, "ProjectName=""" & cboProjectName.Text & """")
            MainClass.AssignCRptFormulas(Report1, "PrepareBy=""" & PubUserName & """")

        End If

        MainClass.AssignCRptFormulas(Report1, "COMPANYGSTIN=""" & IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value) & """")
        MainClass.AssignCRptFormulas(Report1, "COMPANYCIN=""" & IIf(IsDBNull(RsCompany.Fields("CIN_NO").Value), "", RsCompany.Fields("CIN_NO").Value) & """")

        mService = Trim(txtServProvided.Text)
        MainClass.AssignCRptFormulas(Report1, "SERVPROD=""" & mService & """")



        Report1.ReportFileName = PubReportFolderPath & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False


        Report1.Action = 1
        Report1.Reset()
        'Report1.Dispose
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Function SelectQryForPO(ByRef mSqlStr As String) As String

        ''SELECT CLAUSE...			

        mSqlStr = " SELECT " & vbCrLf _
            & " IH.*, ID.*,"

        mSqlStr = mSqlStr & " CMST.SUPP_CUST_NAME, " & vbCrLf _
             & " BCMST.*"

        ''FROM CLAUSE...			
        mSqlStr = mSqlStr & vbCrLf & " FROM DSP_SALEORDER_HDR IH, DSP_SALEORDER_DET ID, " & vbCrLf _
            & " FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_BUSINESS_MST BCMST, INV_ITEM_MST INVMST"

        ''WHERE CLAUSE...			
        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.MKEY=ID.MKEY" & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND CMST.COMPANY_CODE=BCMST.COMPANY_CODE" & vbCrLf _
            & " AND CMST.SUPP_CUST_CODE=BCMST.SUPP_CUST_CODE " & vbCrLf _
            & " AND IH.BILL_TO_LOC_ID=BCMST.LOCATION_ID " & vbCrLf _
            & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf _
            & " AND BCMST.LOCATION_ID='" & Trim(txtBillTo.Text) & "'" & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.AUTO_KEY_SO=" & Val(txtSONo.Text) & "" & vbCrLf _
            & " AND IH.AMEND_NO=" & Val(txtAmendNo.Text) & ""

        ''ORDER CLAUSE...			

        mSqlStr = mSqlStr & vbCrLf & "ORDER BY ID.SERIAL_NO"


        SelectQryForPO = mSqlStr
    End Function
    Private Sub cboProjectName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboProjectName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboProjectName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles cboProjectName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, cboProjectName.Text)  '' MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If

    End Sub
    Private Sub cboSalePersonName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboSalePersonName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboSalePersonName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles cboSalePersonName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, cboSalePersonName.Text)  '' MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub cboPaymentType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboPaymentType.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboPaymentType_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles cboPaymentType.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, cboPaymentType.Text)  '' MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtChqNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtChqNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtChqNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtChqNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtChqNo.Text)  '' MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub FillComboCustomerName()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        Dim RS As ADODB.Recordset
        Dim CntLst As Long

        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim dsShip As New DataSet
        Dim dsStore As New DataSet
        Dim dsApplicant As New DataSet

        oledbCnn = New OleDbConnection(StrConn)

        oledbCnn.Open()

        SqlStr = "Select SUPP_CUST_NAME, SUPP_CUST_CODE, LOCATION_ID, SUPP_CUST_ADDR, SUPP_CUST_CITY," & vbCrLf _
            & " SUPP_CUST_STATE, GST_RGN_NO  " & vbCrLf _
            & " FROM FIN_SUPP_CUST_BUSINESS_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "


        If Trim(txtCustomerName.Text) <> "" Then
            SqlStr = SqlStr & " AND SUPP_CUST_NAME Like '%" & txtCustomerName.Text & "%'"
        End If

        SqlStr = SqlStr & " ORDER BY SUPP_CUST_NAME"

        'SqlStr = SqlStr & vbCrLf & " FETCH FIRST 500 ROWS ONLY"

        oledbAdapter = New OleDbDataAdapter(SqlStr, oledbCnn)
        oledbAdapter.Fill(ds)

        ' Set the data source and data member to bind the grid.
        txtCustomerName.DataSource = ds
        txtCustomerName.DataMember = ""
        'cmbCompany.ValueMember = "COMPANY_CODE"
        'cmbCompany.DisplayMember = "Company Name"

        txtCustomerName.Appearance.FontData.SizeInPoints = 8.5

        txtCustomerName.DisplayLayout.Bands(0).Columns(0).Header.Caption = "Name"
        txtCustomerName.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Code"
        txtCustomerName.DisplayLayout.Bands(0).Columns(2).Header.Caption = "LocationID"
        txtCustomerName.DisplayLayout.Bands(0).Columns(3).Header.Caption = "Address"
        txtCustomerName.DisplayLayout.Bands(0).Columns(4).Header.Caption = "City"
        txtCustomerName.DisplayLayout.Bands(0).Columns(5).Header.Caption = "State"
        txtCustomerName.DisplayLayout.Bands(0).Columns(6).Header.Caption = "GST No"


        txtCustomerName.DisplayLayout.Bands(0).Columns(0).Width = 350
        txtCustomerName.DisplayLayout.Bands(0).Columns(1).Width = 100
        txtCustomerName.DisplayLayout.Bands(0).Columns(2).Width = 150
        txtCustomerName.DisplayLayout.Bands(0).Columns(3).Width = 80
        txtCustomerName.DisplayLayout.Bands(0).Columns(4).Width = 80
        txtCustomerName.DisplayLayout.Bands(0).Columns(5).Width = 80
        txtCustomerName.DisplayLayout.Bands(0).Columns(6).Width = 80

        txtCustomerName.DisplayLayout.Appearance.FontData.SizeInPoints = 8.5

        txtCustomerName.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown ''List       '' Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown

        oledbAdapter.Dispose()

        SqlStr = "Select SUPP_CUST_NAME, SUPP_CUST_CODE, LOCATION_ID, SUPP_CUST_ADDR, SUPP_CUST_CITY," & vbCrLf _
            & " SUPP_CUST_STATE, GST_RGN_NO  " & vbCrLf _
            & " FROM FIN_SUPP_CUST_BUSINESS_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If Trim(txtShipCustomer.Text) <> "" Then
            SqlStr = SqlStr & " AND SUPP_CUST_NAME Like '%" & txtShipCustomer.Text & "%'"
        End If

        SqlStr = SqlStr & " ORDER BY SUPP_CUST_NAME"

        'SqlStr = SqlStr & vbCrLf & " FETCH FIRST 500 ROWS ONLY"

        oledbAdapter = New OleDbDataAdapter(SqlStr, oledbCnn)
        oledbAdapter.Fill(dsShip)

        ' Set the data source and data member to bind the grid.
        txtShipCustomer.DataSource = dsShip
        txtShipCustomer.DataMember = ""
        'cmbCompany.ValueMember = "COMPANY_CODE"
        'cmbCompany.DisplayMember = "Company Name"

        txtShipCustomer.Appearance.FontData.SizeInPoints = 8.5

        txtShipCustomer.DisplayLayout.Bands(0).Columns(0).Header.Caption = "Name"
        txtShipCustomer.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Code"
        txtShipCustomer.DisplayLayout.Bands(0).Columns(2).Header.Caption = "LocationID"
        txtShipCustomer.DisplayLayout.Bands(0).Columns(3).Header.Caption = "Address"
        txtShipCustomer.DisplayLayout.Bands(0).Columns(4).Header.Caption = "City"
        txtShipCustomer.DisplayLayout.Bands(0).Columns(5).Header.Caption = "State"
        txtShipCustomer.DisplayLayout.Bands(0).Columns(6).Header.Caption = "GST No"

        txtShipCustomer.DisplayLayout.Bands(0).Columns(0).Width = 350
        txtShipCustomer.DisplayLayout.Bands(0).Columns(1).Width = 100
        txtShipCustomer.DisplayLayout.Bands(0).Columns(2).Width = 150
        txtShipCustomer.DisplayLayout.Bands(0).Columns(3).Width = 80
        txtShipCustomer.DisplayLayout.Bands(0).Columns(4).Width = 80
        txtShipCustomer.DisplayLayout.Bands(0).Columns(5).Width = 80
        txtShipCustomer.DisplayLayout.Bands(0).Columns(6).Width = 80

        txtShipCustomer.DisplayLayout.Appearance.FontData.SizeInPoints = 8.5

        txtShipCustomer.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown ''List       '' Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown

        oledbAdapter.Dispose()

        SqlStr = "Select SUPP_CUST_NAME, SUPP_CUST_CODE, LOCATION_ID, SUPP_CUST_ADDR, SUPP_CUST_CITY," & vbCrLf _
            & " SUPP_CUST_STATE, GST_RGN_NO  " & vbCrLf _
            & " FROM FIN_SUPP_CUST_BUSINESS_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If Trim(txtStoreDetail.Text) <> "" Then
            SqlStr = SqlStr & " AND SUPP_CUST_NAME Like '%" & txtStoreDetail.Text & "%'"
        End If

        SqlStr = SqlStr & " ORDER BY SUPP_CUST_NAME"

        'SqlStr = SqlStr & vbCrLf & " FETCH FIRST 500 ROWS ONLY"

        oledbAdapter = New OleDbDataAdapter(SqlStr, oledbCnn)
        oledbAdapter.Fill(dsStore)

        ' Set the data source and data member to bind the grid.
        txtStoreDetail.DataSource = dsStore
        txtStoreDetail.DataMember = ""
        'cmbCompany.ValueMember = "COMPANY_CODE"
        'cmbCompany.DisplayMember = "Company Name"

        txtStoreDetail.Appearance.FontData.SizeInPoints = 8.5

        txtStoreDetail.DisplayLayout.Bands(0).Columns(0).Header.Caption = "Name"
        txtStoreDetail.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Code"
        txtStoreDetail.DisplayLayout.Bands(0).Columns(2).Header.Caption = "LocationID"
        txtStoreDetail.DisplayLayout.Bands(0).Columns(3).Header.Caption = "Address"
        txtStoreDetail.DisplayLayout.Bands(0).Columns(4).Header.Caption = "City"
        txtStoreDetail.DisplayLayout.Bands(0).Columns(5).Header.Caption = "State"
        txtStoreDetail.DisplayLayout.Bands(0).Columns(6).Header.Caption = "GST No"

        txtStoreDetail.DisplayLayout.Bands(0).Columns(0).Width = 350
        txtStoreDetail.DisplayLayout.Bands(0).Columns(1).Width = 100
        txtStoreDetail.DisplayLayout.Bands(0).Columns(2).Width = 150
        txtStoreDetail.DisplayLayout.Bands(0).Columns(3).Width = 80
        txtStoreDetail.DisplayLayout.Bands(0).Columns(4).Width = 80
        txtStoreDetail.DisplayLayout.Bands(0).Columns(5).Width = 80
        txtStoreDetail.DisplayLayout.Bands(0).Columns(6).Width = 80

        txtStoreDetail.DisplayLayout.Appearance.FontData.SizeInPoints = 8.5

        txtStoreDetail.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown ''List       '' Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown

        oledbAdapter.Dispose()

        SqlStr = "Select SUPP_CUST_NAME, SUPP_CUST_CODE, LOCATION_ID, SUPP_CUST_ADDR, SUPP_CUST_CITY," & vbCrLf _
            & " SUPP_CUST_STATE, GST_RGN_NO  " & vbCrLf _
            & " FROM FIN_SUPP_CUST_BUSINESS_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If Trim(txtApplicant.Text) <> "" Then
            SqlStr = SqlStr & " AND SUPP_CUST_NAME Like '%" & txtApplicant.Text & "%'"
        End If

        SqlStr = SqlStr & " ORDER BY SUPP_CUST_NAME"

        'SqlStr = SqlStr & vbCrLf & " FETCH FIRST 500 ROWS ONLY"

        oledbAdapter = New OleDbDataAdapter(SqlStr, oledbCnn)
        oledbAdapter.Fill(dsApplicant)

        ' Set the data source and data member to bind the grid.
        txtApplicant.DataSource = dsApplicant
        txtApplicant.DataMember = ""
        'cmbCompany.ValueMember = "COMPANY_CODE"
        'cmbCompany.DisplayMember = "Company Name"

        txtApplicant.Appearance.FontData.SizeInPoints = 8.5

        txtApplicant.DisplayLayout.Bands(0).Columns(0).Header.Caption = "Name"
        txtApplicant.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Code"
        txtApplicant.DisplayLayout.Bands(0).Columns(2).Header.Caption = "LocationID"
        txtApplicant.DisplayLayout.Bands(0).Columns(3).Header.Caption = "Address"
        txtApplicant.DisplayLayout.Bands(0).Columns(4).Header.Caption = "City"
        txtApplicant.DisplayLayout.Bands(0).Columns(5).Header.Caption = "State"
        txtApplicant.DisplayLayout.Bands(0).Columns(6).Header.Caption = "GST No"

        txtApplicant.DisplayLayout.Bands(0).Columns(0).Width = 350
        txtApplicant.DisplayLayout.Bands(0).Columns(1).Width = 100
        txtApplicant.DisplayLayout.Bands(0).Columns(2).Width = 150
        txtApplicant.DisplayLayout.Bands(0).Columns(3).Width = 80
        txtApplicant.DisplayLayout.Bands(0).Columns(4).Width = 80
        txtApplicant.DisplayLayout.Bands(0).Columns(5).Width = 80
        txtApplicant.DisplayLayout.Bands(0).Columns(6).Width = 80

        txtApplicant.DisplayLayout.Appearance.FontData.SizeInPoints = 8.5

        txtApplicant.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown ''List       '' Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown

        oledbAdapter.Dispose()
        oledbCnn.Close()

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtCustomerName_InitializeLayout(sender As Object, e As InitializeLayoutEventArgs) Handles txtCustomerName.InitializeLayout, txtBillTo.InitializeLayout, txtShipCustomer.InitializeLayout, txtShipTo.InitializeLayout, txtStoreDetail.InitializeLayout, txtApplicant.InitializeLayout
        e.Layout.Override.AllowRowFiltering = DefaultableBoolean.True
        e.Layout.Override.FilterUIType = FilterUIType.FilterRow
        e.Layout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.ExternalSortSingle
        e.Layout.Override.HeaderClickAction = HeaderClickAction.SortSingle
    End Sub
    Private Sub txtShipCustomer_KeyDown(sender As Object, e As KeyEventArgs) Handles txtShipCustomer.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Down Then
            txtShipCustomer.PerformAction(UltraComboAction.Dropdown)
        End If
    End Sub
    Private Sub txtBillTo_KeyDown(sender As Object, e As KeyEventArgs) Handles txtBillTo.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Down Then
            txtBillTo.PerformAction(UltraComboAction.Dropdown)
        End If
    End Sub
    Private Sub txtCustomerName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCustomerName.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Down Then
            txtCustomerName.PerformAction(UltraComboAction.Dropdown)
        End If
    End Sub
    Private Sub txtStoreDetail_KeyDown(sender As Object, e As KeyEventArgs) Handles txtStoreDetail.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Down Then
            txtStoreDetail.PerformAction(UltraComboAction.Dropdown)
        End If
    End Sub

    Private Sub txtApplicant_KeyDown(sender As Object, e As KeyEventArgs) Handles txtApplicant.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Down Then
            txtApplicant.PerformAction(UltraComboAction.Dropdown)
        End If
    End Sub

    Private Sub txtShipTo_KeyDown(sender As Object, e As KeyEventArgs) Handles txtShipTo.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        ElseIf e.KeyCode = Keys.Down Then
            txtShipTo.PerformAction(UltraComboAction.Dropdown)
        End If
    End Sub

    Private Sub txtCustomerName_RowSelected(sender As Object, e As RowSelectedEventArgs) Handles txtCustomerName.RowSelected
        On Error GoTo ErrPart
        Dim mAddress As String

        If Trim(txtCustomerName.Text) = "" Then Exit Sub

        Call FillCboLocation("C")

        txtBillTo.Text = txtCustomerName.SelectedRow.Cells(2).Value     '
        mAddress = txtCustomerName.SelectedRow.Cells(3).Value
        mAddress = mAddress & ", " & txtCustomerName.SelectedRow.Cells(4).Value
        mAddress = mAddress & ", " & txtCustomerName.SelectedRow.Cells(5).Value
        mAddress = mAddress & ", GSTNo :" & txtCustomerName.SelectedRow.Cells(6).Value
        txtAddress.Text = mAddress
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, Err.Number)
    End Sub

    Private Sub txtStoreDetail_RowSelected(sender As Object, e As RowSelectedEventArgs) Handles txtStoreDetail.RowSelected
        On Error GoTo ErrPart
        Dim mAddress As String

        If Trim(txtStoreDetail.Text) = "" Then Exit Sub

        'Call FillCboLocation("C")

        'txtBillTo.Text = txtCustomerName.SelectedRow.Cells(2).Value     '
        'mAddress = txtCustomerName.SelectedRow.Cells(3).Value
        'mAddress = mAddress & ", " & txtCustomerName.SelectedRow.Cells(4).Value
        'mAddress = mAddress & ", " & txtCustomerName.SelectedRow.Cells(5).Value
        'mAddress = mAddress & ", GSTNo :" & txtCustomerName.SelectedRow.Cells(6).Value
        'txtAddress.Text = mAddress
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, Err.Number)
    End Sub
    '
    Private Sub txtApplicant_RowSelected(sender As Object, e As RowSelectedEventArgs) Handles txtApplicant.RowSelected
        On Error GoTo ErrPart
        Dim mAddress As String

        If Trim(txtApplicant.Text) = "" Then Exit Sub

        'Call FillCboLocation("C")

        'txtBillTo.Text = txtCustomerName.SelectedRow.Cells(2).Value     '
        'mAddress = txtCustomerName.SelectedRow.Cells(3).Value
        'mAddress = mAddress & ", " & txtCustomerName.SelectedRow.Cells(4).Value
        'mAddress = mAddress & ", " & txtCustomerName.SelectedRow.Cells(5).Value
        'mAddress = mAddress & ", GSTNo :" & txtCustomerName.SelectedRow.Cells(6).Value
        'txtAddress.Text = mAddress
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, Err.Number)
    End Sub
    Private Sub txtShipCustomer_RowSelected(sender As Object, e As RowSelectedEventArgs) Handles txtShipCustomer.RowSelected
        On Error GoTo ErrPart
        If Trim(txtShipCustomer.Text) = "" Then Exit Sub
        Dim mAddress As String
        Call FillCboLocation("S")
        txtShipTo.Text = txtShipCustomer.SelectedRow.Cells(2).Value
        'mAddress = txtShipCustomer.SelectedRow.Cells(3).Value
        'mAddress = mAddress & ", " & txtShipCustomer.SelectedRow.Cells(4).Value
        'mAddress = mAddress & ", " & txtShipCustomer.SelectedRow.Cells(5).Value
        'mAddress = mAddress & ", GSTNo :" & txtShipCustomer.SelectedRow.Cells(6).Value
        'txtAddress.Text = mAddress
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, Err.Number)
    End Sub
    Private Sub FillCboLocation(ByRef pType As String)
        On Error GoTo ErrPart
        Dim SqlStr As String
        'Dim CntLst As Long
        Dim xAcctCode As String = ""
        Dim mLocationID As String
        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet

        oledbCnn = New OleDbConnection(StrConn)

        xAcctCode = ""

        If pType = "C" Then
            If MainClass.ValidateWithMasterTable(txtCustomerName.Text.Trim, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                xAcctCode = MasterNo
            End If
            mLocationID = txtBillTo.Text
        Else
            If MainClass.ValidateWithMasterTable(txtShipCustomer.Text.Trim, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                xAcctCode = MasterNo
            End If
            mLocationID = txtShipTo.Text
        End If

        SqlStr = "SELECT LOCATION_ID, SUPP_CUST_ADDR, SUPP_CUST_CITY, SUPP_CUST_STATE, GST_RGN_NO " & vbCrLf _
                & " FROM FIN_SUPP_CUST_BUSINESS_MST" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " And SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xAcctCode) & "'"

        If mLocationID <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND LOCATION_ID='" & mLocationID & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY LOCATION_ID"

        oledbCnn.Open()
        oledbAdapter = New OleDbDataAdapter(SqlStr, oledbCnn)

        oledbAdapter.Fill(ds)

        If pType = "C" Then
            ' Set the data source and data member to bind the grid.
            txtBillTo.DataSource = ds
            txtBillTo.DataMember = ""

            'cboProcessTo.DisplayMember = "PROCESS_DESC"
            'cboProcessTo.ValueMember = "PROCESS_CODE"

            txtBillTo.DisplayLayout.Bands(0).Columns(0).Header.Caption = "LocationID"
            txtBillTo.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Address"
            txtBillTo.DisplayLayout.Bands(0).Columns(2).Header.Caption = "City"
            txtBillTo.DisplayLayout.Bands(0).Columns(3).Header.Caption = "State"
            txtBillTo.DisplayLayout.Bands(0).Columns(4).Header.Caption = "GST No"

            txtBillTo.DisplayLayout.Bands(0).Columns(0).Width = 150
            txtBillTo.DisplayLayout.Bands(0).Columns(1).Width = 250
            txtBillTo.DisplayLayout.Bands(0).Columns(2).Width = 100
            txtBillTo.DisplayLayout.Bands(0).Columns(3).Width = 100
            txtBillTo.DisplayLayout.Bands(0).Columns(4).Width = 100

            txtBillTo.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown ''List       '' Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown
        Else
            txtShipTo.DataSource = ds
            txtShipTo.DataMember = ""

            'cboProcessTo.DisplayMember = "PROCESS_DESC"
            'cboProcessTo.ValueMember = "PROCESS_CODE"

            txtShipTo.DisplayLayout.Bands(0).Columns(0).Header.Caption = "LocationID"
            txtShipTo.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Address"
            txtShipTo.DisplayLayout.Bands(0).Columns(2).Header.Caption = "City"
            txtShipTo.DisplayLayout.Bands(0).Columns(3).Header.Caption = "State"
            txtShipTo.DisplayLayout.Bands(0).Columns(4).Header.Caption = "GST No"

            txtShipTo.DisplayLayout.Bands(0).Columns(0).Width = 150
            txtShipTo.DisplayLayout.Bands(0).Columns(1).Width = 250
            txtShipTo.DisplayLayout.Bands(0).Columns(2).Width = 100
            txtShipTo.DisplayLayout.Bands(0).Columns(3).Width = 100
            txtShipTo.DisplayLayout.Bands(0).Columns(4).Width = 100

            txtShipTo.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown
        End If
        oledbAdapter.Dispose()
        oledbCnn.Close()

        Exit Sub
ErrPart:
    End Sub

    Private Sub frmSalesOrderGST_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 240, mReFormWidth - 240, mReFormWidth))
        fraAccounts.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        FraTrn.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        UltraGrid1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        CurrFormWidth = mReFormWidth

        fraTop1.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        _TabMain_TabPage0.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        _TabMain_TabPage1.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        TabMain.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        'FraTrn.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdPopulate_Click(sender As Object, e As EventArgs) Handles cmdPopulate.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim xAcctCode As String = ""
        Dim mBillNo As String
        Dim mMKey As String
        Dim mSearchKey As String = 1


        txtPINo.Text = ""
        txtPIType.Text = ""

        If txtCustomerName.Text = "" Then MsgInformation("Please Select Customer Name") : Exit Sub


        If MainClass.ValidateWithMasterTable(Trim(txtCustomerName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xAcctCode = MasterNo
        Else
            MsgInformation("Customer Not valid.")
            Exit Sub
        End If

        ''CUST_PO_NO, CUST_PO_DATE, 

        mSearchKey = InputBox("Press 1 for PI and 2 For PO :", "PI / PO", mSearchKey)

        If mSearchKey = "1" Then
            SqlStr = "SELECT DISTINCT IH.BILLNO, IH.INVOICE_DATE  AS BILLDATE, " & vbCrLf _
                & " IH.CUST_PO_NO AS PONO, IH.CUST_PO_DATE AS PODATE, " & vbCrLf _
                & " IH.BILL_TO_LOC_ID, IH.SHIPPED_TO_SAMEPARTY, IH.SHIPPED_TO_PARTY_CODE, IH.SHIP_TO_LOC_ID, " & vbCrLf _
                & " A.SUPP_CUST_NAME AS CUSTOMER, " & vbCrLf _
                & " IH.NETVALUE FROM " & vbCrLf _
                & " FIN_PRO_INVOICE_HDR IH, FIN_PRO_INVOICE_DET ID,FIN_SUPP_CUST_MST A " & vbCrLf _
                & " WHERE IH.AUTO_KEY_INVOICE=ID.AUTO_KEY_INVOICE" & vbCrLf _
                & " AND IH.COMPANY_CODE=A.COMPANY_CODE " & vbCrLf _
                & " AND IH.SUPP_CUST_CODE=A.SUPP_CUST_CODE "

            SqlStr = SqlStr & vbCrLf _
                & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xAcctCode) & "'"

            SqlStr = SqlStr & vbCrLf _
                & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " And IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

            SqlStr = SqlStr & vbCrLf _
                & " AND IH.BILLNO || ID.ITEM_CODE NOT IN (" & vbCrLf _
                & " SELECT DISTINCT SH.AUTO_KEY_PI || SD.ITEM_CODE FROM DSP_SALEORDER_HDR SH, DSP_SALEORDER_DET SD " & vbCrLf _
                & " WHERE SH.MKEY=SD.MKEY AND SH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND SH.SUPP_CUST_CODE=IH.SUPP_CUST_CODE AND SH.PI_TYPE='I' AND SH.AUTO_KEY_PI IS NOT NULL)"

            '' 

            SqlStr = SqlStr & vbCrLf _
                & " Order by BILLDATE,BillNo"

            Dim mShipAccountCode As String
            Dim mShipToAccountName As String

            If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
                mBillNo = AcName
                txtPONo.Text = AcName2
                txtPODate.Text = VB6.Format(AcName3, "DD/MM/YYYY")
                txtBillTo.Text = AcName4

                txtPINo.Text = mBillNo
                txtPIType.Text = "I"

                Dim mShipto As String = AcName5
                chkShipTo.CheckState = IIf(mShipto = "Y", CheckState.Checked, CheckState.Unchecked)
                mShipAccountCode = AcName6


                mShipAccountCode = AcName6
                mShipToAccountName = "'"
                If MainClass.ValidateWithMasterTable(mShipAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mShipToAccountName = MasterNo
                End If

                txtShipCustomer.Text = mShipToAccountName
                txtShipTo.Text = AcName7

                If mShipto = "Y" Then
                    txtShipCustomer.Enabled = False
                    txtShipTo.Enabled = False
                Else
                    txtShipCustomer.Enabled = True
                    txtShipTo.Enabled = True
                End If


                If MainClass.ValidateWithMasterTable(mBillNo, "BILLNO", "MKEY", "FIN_PRO_INVOICE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mMKey = MasterNo
                End If

            End If

            If Trim(mMKey) <> "" Then
                Call ShowFromPI(mMKey)
                'End If
            End If
        Else
            'SqlStr = "SELECT BILLNO, INVOICE_DATE  AS BILLDATE, " & vbCrLf _
            '    & " CUST_PO_NO AS PONO, CUST_PO_DATE AS PODATE, " & vbCrLf _
            '    & " BILL_TO_LOC_ID, SHIPPED_TO_SAMEPARTY, SHIPPED_TO_PARTY_CODE, SHIP_TO_LOC_ID, " & vbCrLf _
            '    & " A.SUPP_CUST_NAME AS CUSTOMER, " & vbCrLf _
            '    & " NETVALUE FROM " & vbCrLf _
            '    & " PUR_PURCHASE_HDR, FIN_SUPP_CUST_MST A " & vbCrLf _
            '    & " WHERE " & vbCrLf _
            '    & " FIN_PRO_INVOICE_HDR.COMPANY_CODE=A.COMPANY_CODE " & vbCrLf _
            '    & " AND FIN_PRO_INVOICE_HDR.SUPP_CUST_CODE=A.SUPP_CUST_CODE "

            'SqlStr = SqlStr & vbCrLf _
            '    & " AND PUR_PURCHASE_HDR.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xAcctCode) & "'"

            'SqlStr = SqlStr & vbCrLf _
            '    & " AND FIN_PRO_INVOICE_HDR.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            '    & " And FIN_PRO_INVOICE_HDR.FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

            'SqlStr = SqlStr & vbCrLf _
            '    & " Order by BILLDATE,BillNo"

            'Dim mShipAccountCode As String
            'Dim mShipToAccountName As String

            'If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            '    mBillNo = AcName
            '    txtPONo.Text = AcName2
            '    txtPODate.Text = VB6.Format(AcName3, "DD/MM/YYYY")
            '    txtBillTo.Text = AcName4

            '    Dim mShipto As String = AcName5
            '    chkShipTo.CheckState = IIf(mShipto = "Y", CheckState.Checked, CheckState.Unchecked)
            '    mShipAccountCode = AcName6


            '    mShipAccountCode = AcName6
            '    mShipToAccountName = "'"
            '    If MainClass.ValidateWithMasterTable(mShipAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            '        mShipToAccountName = MasterNo
            '    End If

            '    txtShipCustomer.Text = mShipToAccountName
            '    txtShipTo.Text = AcName7

            '    If mShipto = "Y" Then
            '        txtShipCustomer.Enabled = False
            '        txtShipTo.Enabled = False
            '    Else
            '        txtShipCustomer.Enabled = True
            '        txtShipTo.Enabled = True
            '    End If


            '    If MainClass.ValidateWithMasterTable(mBillNo, "BILLNO", "MKEY", "FIN_PRO_INVOICE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            '        mMKey = MasterNo
            '    End If
            'End If

            'If Trim(mMKey) <> "" Then
            '    Call ShowFromPI(mMKey)
            '    'End If
            'End If
        End If

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub ShowFromPI(ByRef pMKey As String)

        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim mMode As String
        Dim mDivision As String
        Dim mShippedToCode As String
        Dim mShippedToName As String
        Dim SqlStr As String
        Dim mSaleMKey As String
        Dim CntRow As Long
        Dim mPONo As String
        Dim mPODate As String
        Dim mItemCode As String
        Dim mRate As Double
        Dim mDiscRate As Double

        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)

        SqlStr = "SELECT * FROM  FIN_PRO_INVOICE_DET" & vbCrLf _
                & " WHERE MKEY='" & pMKey & "' "

        SqlStr = SqlStr & vbCrLf _
                & " AND ITEM_CODE NOT IN (" & vbCrLf _
                & " SELECT SD.ITEM_CODE FROM DSP_SALEORDER_HDR SH, DSP_SALEORDER_DET SD " & vbCrLf _
                & " WHERE SH.MKEY=SD.MKEY AND SH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND SH.AUTO_KEY_PI='" & pMKey & "')"

        SqlStr = SqlStr & vbCrLf _
                & " ORDER BY SUBROWNO"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        CntRow = 1
        With SprdMain
            If RsTemp.EOF = False Then
                Do While RsTemp.EOF = False
                    .Row = CntRow

                    .Col = ColItemCode
                    .Text = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))
                    mItemCode = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))

                    .Col = ColItemName
                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        .Text = Trim(MasterNo)
                    Else
                        .Text = ""
                    End If

                    .Col = ColPartNo
                    .Text = Trim(IIf(IsDBNull(RsTemp.Fields("CUSTOMER_PART_NO").Value), "", RsTemp.Fields("CUSTOMER_PART_NO").Value))

                    .Col = ColItemUOM
                    .Text = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value))

                    .Col = ColHSNCode
                    .Text = Trim(IIf(IsDBNull(RsTemp.Fields("HSNCODE").Value), "", RsTemp.Fields("HSNCODE").Value))

                    .Col = ColGlassDescription
                    .Text = Trim(IIf(IsDBNull(RsTemp.Fields("GLASS_DESC").Value), "", RsTemp.Fields("GLASS_DESC").Value))

                    .Col = ColActualWidth
                    .Text = IIf(IsDBNull(RsTemp.Fields("ACTUAL_WIDTH").Value), 0, RsTemp.Fields("ACTUAL_WIDTH").Value)

                    .Col = ColActualHeight
                    .Text = IIf(IsDBNull(RsTemp.Fields("ACTUAL_HEIGHT").Value), 0, RsTemp.Fields("ACTUAL_HEIGHT").Value)

                    .Col = ColChargeableWidth
                    .Text = Trim(IIf(IsDBNull(RsTemp.Fields("CHARGEABLE_WIDTH").Value), "", RsTemp.Fields("CHARGEABLE_WIDTH").Value))

                    .Col = ColChargeableHeight
                    .Text = Trim(IIf(IsDBNull(RsTemp.Fields("CHARGEABLE_HEIGHT").Value), "", RsTemp.Fields("CHARGEABLE_HEIGHT").Value))

                    '.Col = ColArea
                    '.Text = Trim(IIf(IsDBNull(RsTemp.Fields("a").Value), "", RsTemp.Fields("a").Value))

                    .Col = ColAreaRate
                    .Text = Trim(IIf(IsDBNull(RsTemp.Fields("AREA_RATE").Value), "", RsTemp.Fields("AREA_RATE").Value))

                    '.Col = ColModelNo
                    '.Text = Trim(IIf(IsDBNull(RsTemp.Fields("a").Value), "", RsTemp.Fields("a").Value))

                    '.Col = ColDrawingNo
                    '.Text = Trim(IIf(IsDBNull(RsTemp.Fields("a").Value), "", RsTemp.Fields("a").Value))

                    '.Col = ColItemSNo
                    '.Text = Trim(IIf(IsDBNull(RsTemp.Fields("a").Value), "", RsTemp.Fields("a").Value))

                    '.Col = ColAddItemDesc
                    '.Text = Trim(IIf(IsDBNull(RsTemp.Fields("a").Value), "", RsTemp.Fields("a").Value))

                    '.Col = ColCustStoreLoc
                    '.Text = Trim(IIf(IsDBNull(RsTemp.Fields("a").Value), "", RsTemp.Fields("a").Value))


                    .Col = ColPktQty
                    .Text = Trim(IIf(IsDBNull(RsTemp.Fields("PACK_QTY").Value), "", RsTemp.Fields("PACK_QTY").Value))

                    .Col = ColItemQty
                    .Text = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), "", RsTemp.Fields("ITEM_QTY").Value))

                    .Col = ColMRP
                    .Text = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_MRP").Value), "", RsTemp.Fields("ITEM_MRP").Value))

                    .Col = ColItemDiscount
                    .Text = Trim(IIf(IsDBNull(RsTemp.Fields("DISC_RATE").Value), "", RsTemp.Fields("DISC_RATE").Value))

                    'mRate = Val(IIf(IsDBNull(RsTemp.Fields("ITEM_RATE").Value), 0, RsTemp.Fields("ITEM_RATE").Value))
                    'mDiscRate = Val(IIf(IsDBNull(RsTemp.Fields("DISC_RATE").Value), 0, RsTemp.Fields("DISC_RATE").Value))

                    'mRate = mRate - (mRate * mDiscRate / 100)

                    .Col = ColItemRate
                    .Text = Val(IIf(IsDBNull(RsTemp.Fields("ITEM_RATE").Value), 0, RsTemp.Fields("ITEM_RATE").Value)) '' Trim(mRate)

                    .Col = ColCGSTPer
                    .Text = Trim(IIf(IsDBNull(RsTemp.Fields("CGST_PER").Value), "", RsTemp.Fields("CGST_PER").Value))

                    .Col = ColSGSTPer
                    .Text = Trim(IIf(IsDBNull(RsTemp.Fields("SGST_PER").Value), "", RsTemp.Fields("SGST_PER").Value))

                    .Col = ColIGSTPer
                    .Text = Trim(IIf(IsDBNull(RsTemp.Fields("IGST_PER").Value), "", RsTemp.Fields("IGST_PER").Value))


                    CntRow = CntRow + 1
                    .MaxRows = CntRow
                    RsTemp.MoveNext()
                Loop
            End If
        End With

        FormatSprdMain(-1)

        Call CalcTots()

        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub

    Private Sub SprdMain_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdMain.ButtonClicked
        If eventArgs.col = ColItemDetail And eventArgs.row > 0 Then
            Call ShowFormDSDailyDetail(eventArgs.col, eventArgs.row)
        End If

    End Sub
    Private Sub ShowFormDSDailyDetail(ByRef pCol As Integer, ByRef pRow As Integer)
        'Dim I As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        'Dim pDate As String
        Dim mItemCode As String
        'Dim mItemName As String
        'Dim mQty As String
        Dim mOrderType As String = ""
        Dim mSchdQty As Double
        Dim mStoreLoc As String = ""
        Dim mDeliveryInstruction As String = "N"
        ''txtOurSONo

        With SprdMain
            .Row = pRow

            .Col = ColItemCode
            mItemCode = .Text

            .Col = ColItemQty
            mSchdQty = Val(.Text)
        End With
        If mItemCode = "" Then Exit Sub

        mOrderType = "C"
        mDeliveryInstruction = "N"



        ConSaleDSDetail = False


        With FrmSalesDSDailyClosed
            .LblAddMode.Text = CStr(ADDMode)
            .LblModifyMode.Text = CStr(MODIFYMode)
            .LblTempSeq.Text = CStr(Val(pTempSeq))
            .lblPoNo.Text = CStr(Val(txtSONo.Text))
            .lblItemCode.Text = mItemCode
            .lblStoreLoc.Text = mStoreLoc
            .lblDI.Text = mDeliveryInstruction
            .lblSuppCode.Text = txtCode.Text
            .LblPODate.Text = VB6.Format(txtSODate.Text, "DD/MM/YYYY")
            .lblMainActiveRow.Text = CStr(pRow)
            .lblBookType.Text = "S"
            .ShowDialog()
        End With

        If ConSaleDSDetail = True Then
            With SprdMain
                .Row = pRow
                .Col = ColItemQty
                mSchdQty = Val(FrmSalesDSDailyClosed.lblPlanQty.Text)

                .Text = mSchdQty

                '.Col = ColWeek1Qty
                '.Text = CStr(Val(FrmSalesDSDailyClosed.lblWeek1.Text))
                '.Col = ColWeek2Qty
                '.Text = CStr(Val(FrmSalesDSDailyClosed.lblWeek2.Text))
                '.Col = ColWeek3Qty
                '.Text = CStr(Val(FrmSalesDSDailyClosed.lblWeek3.Text))
                '.Col = ColWeek4Qty
                '.Text = CStr(Val(FrmSalesDSDailyClosed.lblWeek4.Text))
                '.Col = ColWeek5Qty
                '.Text = CStr(Val(FrmSalesDSDailyClosed.lblWeek5.Text))
                FrmSalesDSDailyClosed.Close()
            End With
            'Call CalcTots()
        End If
    End Sub
    Private Sub ShowDSDailyDetail()

        On Error GoTo ShowSerialNoErr
        Dim RsSRLNo As ADODB.Recordset
        Dim SqlStr As String = ""

        Call DelTemp_DailyDetail()

        SqlStr = ""
        SqlStr = "INSERT INTO TEMP_DSP_DAILY_SCHLD_DET ( " & vbCrLf _
            & " UserId, TEMP_AUTO_KEY, AUTO_KEY_DELV, ITEM_CODE, " & vbCrLf _
            & " SERIAL_DATE, PLANNED_QTY, ACTUAL_QTY," & vbCrLf _
            & " DELV_CNT, SUPP_CUST_CODE,SCHLD_DATE,REQ_DATE,LOC_CODE,OD_NO,BOOKTYPE)" & vbCrLf _
            & " SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "', " & Val(pTempSeq) & ", " & vbCrLf _
            & " AUTO_KEY_DELV, ITEM_CODE," & vbCrLf _
            & " SERIAL_DATE, PLANNED_QTY, ACTUAL_QTY, " & vbCrLf _
            & " DELV_CNT , SUPP_CUST_CODE, SCHLD_DATE,REQ_DATE,LOC_CODE,OD_NO,BOOKTYPE " & vbCrLf _
            & " FROM DSP_DAILY_SCHLD_DET " & vbCrLf _
            & " WHERE AUTO_KEY_DELV=" & Val(txtSONo.Text) & " AND BOOKTYPE='S'" & vbCrLf _
            & " ORDER BY SERIAL_NO, SERIAL_DATE"

        PubDBCn.Execute(SqlStr)

        Exit Sub
ShowSerialNoErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub DelTemp_DailyDetail(Optional ByRef mRefNo As String = "", Optional ByRef mItemCode As String = "")

        Dim SqlStr As String = ""

        SqlStr = ""
        SqlStr = "DELETE FROM TEMP_DSP_DAILY_SCHLD_DET " & vbCrLf _
            & "WHERE UserId='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' AND BOOKTYPE='S'"

        SqlStr = SqlStr & vbCrLf _
            & " AND TEMP_AUTO_KEY=" & Val(pTempSeq) & " "

        If mRefNo <> "" And mItemCode <> "" Then
            SqlStr = SqlStr & vbCrLf _
                & " AND AUTO_KEY_DELV=" & Val(mRefNo) & "' " & vbCrLf _
                & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(UCase(mItemCode)) & "' "
        End If
        PubDBCn.Execute(SqlStr)
    End Sub
    Private Function CheckDSDetailExists(ByRef nItemCode As String, ByRef mStoreLoc As String, ByRef mSerialNo As Integer, ByRef mDSQty As Double) As Boolean

        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing '' ADODB.Recordset

        SqlStr = "SELECT SUM(PLANNED_QTY) AS PLANNED_QTY" & vbCrLf _
            & " FROM TEMP_DSP_DAILY_SCHLD_DET " & vbCrLf _
            & " WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf _
            & " AND TEMP_AUTO_KEY=" & Val(pTempSeq) & " " & vbCrLf _
            & " AND ITEM_CODE='" & Trim(nItemCode) & "' AND BOOKTYPE='S'"

        If mStoreLoc = "" Then
            SqlStr = SqlStr & vbCrLf & " AND (LOC_CODE='" & Trim(mStoreLoc) & "' OR LOC_CODE IS NULL)"
        Else
            SqlStr = SqlStr & vbCrLf & " AND LOC_CODE='" & Trim(mStoreLoc) & "'"
        End If

        SqlStr = SqlStr & vbCrLf _
            & " GROUP BY ITEM_CODE "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If Val(RsTemp.Fields("PLANNED_QTY").Value) = mDSQty Then
                CheckDSDetailExists = True
            Else
                CheckDSDetailExists = False
            End If
        Else
            CheckDSDetailExists = False
        End If
    End Function
    Private Function UpdateDailyDSDetail(ByRef mSoNo As Double) As Boolean
        On Error GoTo UpdateErr1
        Dim RsTemp_SRLNo As ADODB.Recordset
        Dim ii As Integer
        Dim SqlStr As String = ""
        Dim mItemCode As String
        Dim mStoreLoc As String
        Dim mItemQty As Double

        With SprdMain
            For ii = 1 To .MaxRows - 1
                .Row = ii
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColItemQty
                mItemQty = Val(.Text)

                mStoreLoc = ""
                If mItemQty > 0 Then
                    SqlStr = "SELECT " & vbCrLf & " " & Val(txtSONo.Text) & ", " & ii & ", ITEM_CODE, " & vbCrLf _
                            & " SERIAL_DATE, PLANNED_QTY, ACTUAL_QTY, " & vbCrLf _
                            & " DELV_CNT, SUPP_CUST_CODE, SCHLD_DATE,REQ_DATE,LOC_CODE,OD_NO,BOOKTYPE " & vbCrLf _
                            & " FROM TEMP_DSP_DAILY_SCHLD_DET " & vbCrLf _
                            & " WHERE USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf _
                            & " AND TEMP_AUTO_KEY=" & Val(pTempSeq) & " " & vbCrLf _
                            & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote((txtCode.Text)) & "'" & vbCrLf _
                            & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' AND BOOKTYPE='S'"

                    If mStoreLoc = "" Then
                        SqlStr = SqlStr & vbCrLf & " AND (LOC_CODE='" & MainClass.AllowSingleQuote(mStoreLoc) & "' OR LOC_CODE IS NULL)"
                    Else
                        SqlStr = SqlStr & vbCrLf & " AND LOC_CODE='" & MainClass.AllowSingleQuote(mStoreLoc) & "'"
                    End If

                    SqlStr = SqlStr & vbCrLf & " AND SCHLD_DATE=TO_DATE('" & VB6.Format(txtSODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp_SRLNo, ADODB.LockTypeEnum.adLockReadOnly)

                    If RsTemp_SRLNo.EOF = True Then
                        SqlStr = "INSERT INTO DSP_DAILY_SCHLD_DET (" & vbCrLf _
                           & " AUTO_KEY_DELV, SERIAL_NO, ITEM_CODE, " & vbCrLf _
                           & " SERIAL_DATE, PLANNED_QTY, ACTUAL_QTY, " & vbCrLf _
                           & " DELV_CNT, SUPP_CUST_CODE, SCHLD_DATE,REQ_DATE,LOC_CODE,OD_NO,BOOKTYPE ) VALUES (" & vbCrLf _
                           & " " & Val(mSoNo) & ", " & ii & ", '" & mItemCode & "', " & vbCrLf _
                           & " TO_DATE('" & VB6.Format(txtSODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & mItemQty & ", 0, " & vbCrLf _
                           & " 0, '" & txtCode.Text & "', TO_DATE('" & VB6.Format(txtSODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','','','S') "

                        PubDBCn.Execute(SqlStr)
                    Else
                        SqlStr = "INSERT INTO DSP_DAILY_SCHLD_DET (" & vbCrLf _
                            & " AUTO_KEY_DELV, SERIAL_NO, ITEM_CODE, " & vbCrLf _
                            & " SERIAL_DATE, PLANNED_QTY, ACTUAL_QTY, " & vbCrLf _
                            & " DELV_CNT, SUPP_CUST_CODE, SCHLD_DATE,REQ_DATE,LOC_CODE,OD_NO,BOOKTYPE )" & vbCrLf _
                            & " SELECT " & vbCrLf & " " & Val(mSoNo) & ", " & ii & ", ITEM_CODE, " & vbCrLf _
                            & " SERIAL_DATE, PLANNED_QTY, ACTUAL_QTY, " & vbCrLf _
                            & " DELV_CNT, SUPP_CUST_CODE, SCHLD_DATE,REQ_DATE,LOC_CODE,OD_NO,BOOKTYPE " & vbCrLf _
                            & " FROM TEMP_DSP_DAILY_SCHLD_DET " & vbCrLf _
                            & " WHERE USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf _
                            & " AND TEMP_AUTO_KEY=" & Val(pTempSeq) & " " & vbCrLf _
                            & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote((txtCode.Text)) & "'" & vbCrLf _
                            & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' AND BOOKTYPE='S'"

                        If mStoreLoc = "" Then
                            SqlStr = SqlStr & vbCrLf & " AND (LOC_CODE='" & MainClass.AllowSingleQuote(mStoreLoc) & "' OR LOC_CODE IS NULL)"
                        Else
                            SqlStr = SqlStr & vbCrLf & " AND LOC_CODE='" & MainClass.AllowSingleQuote(mStoreLoc) & "'"
                        End If

                        SqlStr = SqlStr & vbCrLf & " AND SCHLD_DATE=TO_DATE('" & VB6.Format(txtSODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                        PubDBCn.Execute(SqlStr)

                        'SqlStr = "INSERT INTO DSP_DAILY_SCHLD_LOG_DET (" & vbCrLf _
                        '        & " AUTO_KEY_DELV, AMEND_NO, SERIAL_NO, ITEM_CODE, " & vbCrLf _
                        '        & " SERIAL_DATE, PLANNED_QTY, LOC_CODE, OD_NO,BOOKTYPE,MODUSER, MODDATE)" & vbCrLf _
                        '        & " SELECT " & vbCrLf _
                        '        & " " & Val(txtSONo.Text) & ", " & VB6.Format(txtAmendNo.Text, "000") & ", " & ii & ", ITEM_CODE, " & vbCrLf _
                        '        & " SERIAL_DATE, PLANNED_QTY,LOC_CODE, OD_NO, BOOKTYPE,'" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "', TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')" & vbCrLf _
                        '        & " FROM TEMP_DSP_DAILY_SCHLD_DET " & vbCrLf _
                        '        & " WHERE USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf _
                        '        & " AND TEMP_AUTO_KEY=" & Val(pTempSeq) & " " & vbCrLf _
                        '        & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote((txtCode.Text)) & "'" & vbCrLf _
                        '        & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' AND BOOKTYPE='S'"

                        'If mStoreLoc = "" Then
                        '    SqlStr = SqlStr & vbCrLf & " AND (LOC_CODE='" & MainClass.AllowSingleQuote(mStoreLoc) & "' OR LOC_CODE IS NULL)"
                        'Else
                        '    SqlStr = SqlStr & vbCrLf & " AND LOC_CODE='" & MainClass.AllowSingleQuote(mStoreLoc) & "'"
                        'End If

                        'SqlStr = SqlStr & vbCrLf & " AND SCHLD_DATE=TO_DATE('" & VB6.Format(txtSODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                        'PubDBCn.Execute(SqlStr)
                    End If
                End If
            Next
        End With

        If Trim(pTempSeq) <> "" Then
            SqlStr = "DELETE FROM TEMP_DSP_DAILY_SCHLD_DET WHERE TEMP_AUTO_KEY=" & Val(pTempSeq) & ""
            PubDBCn.Execute(SqlStr)
        End If

        UpdateDailyDSDetail = True
        Exit Function
UpdateErr1:
        'Resume
        UpdateDailyDSDetail = False
        If Err.Description <> "" Then
            MsgBox(Err.Description)
        End If
    End Function
    Public Function DeleteDSDailyDetail(ByRef pDBCn As ADODB.Connection, ByRef pMKey As Double) As Boolean
        Dim SqlStr As String = ""
        On Error GoTo DeleteDSDailyDetailErr
        SqlStr = ""
        SqlStr = "DELETE FROM DSP_DAILY_SCHLD_DET  " & vbCrLf _
            & " WHERE AUTO_KEY_DELV=" & Val(CStr(pMKey)) & " AND BOOKTYPE='S'"
        pDBCn.Execute(SqlStr)

        SqlStr = "DELETE FROM DSP_DAILY_SCHLD_LOG_DET  " & vbCrLf _
            & " WHERE AUTO_KEY_DELV=" & Val(CStr(pMKey)) & " " & vbCrLf _
            & " AND AMEND_NO= " & Val(txtAmendNo.Text) & " AND BOOKTYPE='S' "
        pDBCn.Execute(SqlStr)

        DeleteDSDailyDetail = True
        Exit Function
DeleteDSDailyDetailErr:
        MsgInformation(Err.Description)
        DeleteDSDailyDetail = False
    End Function
    Private Sub txtStoreDetail_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtStoreDetail.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtStoreDetail_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtStoreDetail.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtStoreDetail.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
        'FillComboCustomerName()
    End Sub
    Private Sub txtStoreDetail_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtStoreDetail.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        'If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Sub txtStoreDetail_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtStoreDetail.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim xAcctCode As String

        If Trim(txtStoreDetail.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtStoreDetail.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then

        Else
            MsgBox("Invalid Store Details.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If


        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtApplicant_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtApplicant.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtApplicant_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtApplicant.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtApplicant.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
        'FillComboCustomerName()
    End Sub
    Private Sub txtApplicant_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtApplicant.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        'If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Sub txtApplicant_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtApplicant.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim xAcctCode As String

        If Trim(txtApplicant.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtApplicant.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then

        Else
            MsgBox("Invalid Applicant.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If


        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
