Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinDataSource
'Imports Infragistics.Win.UltraWinTabControl
Imports System.Data.OleDb
Imports AxFPSpreadADO

Friend Class frmPO_GST
    Inherits System.Windows.Forms.Form
    Dim RsPOMain As ADODB.Recordset ''ADODB.Recordset		
    Dim RsPODetail As ADODB.Recordset ''ADODB.Recordset		
    Dim RsPOExp As ADODB.Recordset
    Dim RsPOAnnex As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection		

    Dim mSearchKey As String
    Dim cntSearchRow As Long
    Dim cntSearchCol As Long

    Const conChunkSize As Short = 100
    Dim Ctrl As Object
    Dim Ctrl1 As System.Windows.Forms.Control
    Dim PicNm As Object
    Dim StrTempPic As String
    Dim Isize As Object
    Dim nHand As Short
    Dim lngImgSiz As Integer
    Dim lngOffset As Integer

    Dim Chunk() As Byte

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean
    Dim mAccountCode As String
    Dim pRound As Double

    Private Const ConRowHeight As Short = 30
    Private Const ColAnnexDesc As Short = 1
    Dim mSearchStartRow As Integer

    Dim pShowCalc As Boolean
    Dim pmyMenu As String

    Private Const ColWoDesc As Short = 1
    Private Const ColItemCode As Short = 2
    Private Const ColItemName As Short = 3
    Private Const ColHSN As Short = 4
    Private Const ColIdenty As Short = 5
    Private Const ColItemUOM As Short = 6
    Private Const ColQty As Short = 7
    Private Const ColRMRate As Short = 8
    Private Const ColRMDRWRate As Short = 9
    Private Const ColLastPORate As Short = 10
    Private Const ColItemRate As Short = 11
    Private Const ColItemDisc As Short = 12
    Private Const ColGross As Short = 13
    Private Const ColGross_Prev As Short = 14
    Private Const ColPO_WEF As Short = 15
    Private Const ColPrevPO_WEF As Short = 16
    Private Const ColIsTentativeRate As Short = 17
    Private Const ColRemarks As Short = 18
    Private Const ColFreightCost As Short = 19
    Private Const ColVolumeDiscount As Short = 20
    Private Const ColCGSTPer As Short = 21
    Private Const ColCGSTAmount As Short = 22
    Private Const ColSGSTPer As Short = 23
    Private Const ColSGSTAmount As Short = 24
    Private Const ColIGSTPer As Short = 25
    Private Const ColIGSTAmount As Short = 26
    Private Const ColLandedCost As Short = 27
    Private Const ColAcctPostName As Short = 28
    Private Const ColQtyInKgs As Short = 29
    Private Const ColRateInKgs As Short = 30
    Private Const ColPrintStatus As Short = 31
    Private Const ColReprocess As Short = 32
    Private Const ColOutWardCode As Short = 33
    Private Const ColOutWardName As Short = 34
    Private Const ColAssetsNo As Short = 35
    Private Const ColStatus As Short = 36
    Private Const ColQtyRecd As Short = 37



    Private Const ColRO As Short = 1
    Private Const ColExpName As Short = 2
    Private Const ColExpPercent As Short = 3
    Private Const ColExpAmt As Short = 4
    Private Const ColExpSTCode As Short = 5
    Private Const ColExpAddDeduct As Short = 6
    Private Const ColExpIdent As Short = 7
    Private Const ColTaxable As Short = 8
    Private Const ColExciseable As Short = 9
    Private Const ColExpCalcOn As Short = 10
    Dim mAmendStatus As Boolean
    Dim mAuthorised As Boolean
    Dim mAuthorisedPrint As Boolean
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Private Sub cboGSTStatus_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboGSTStatus.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboGSTStatus_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboGSTStatus.SelectedIndexChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub ChkCapital_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCapital.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub ChkActivate_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkActivate.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkDevelopment_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkDevelopment.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub ChkPrintAllItem_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkPrintAllItem.CheckStateChanged
        On Error GoTo ErrPart
        Dim CntRow As Integer
        Dim mPrice As Double
        Dim mDisc As Double
        Dim mCurrAmount As Double
        Dim mPrevAmount As Double
        Dim mPOWEF As String
        Dim mPrevWEF As String

        With SprdMain
            For CntRow = 1 To .MaxRows
                .Row = CntRow
                .Col = ColItemRate
                mPrice = Val(.Text)

                .Col = ColItemDisc
                mDisc = Val(.Text)

                mCurrAmount = mPrice - System.Math.Round((mPrice * mDisc) / 100, 4)

                .Col = ColGross_Prev
                mPrevAmount = Val(.Text)

                .Col = ColPO_WEF
                mPOWEF = VB6.Format(.Text, "DD/MM/YYYY")

                .Col = ColPrevPO_WEF
                mPrevWEF = VB6.Format(.Text, "DD/MM/YYYY")

                If VB.Left(lblBookType.Text, 1) = "W" Or VB.Left(lblBookType.Text, 1) = "R" Then
                    .Col = ColWoDesc
                Else
                    .Col = ColItemCode
                End If

                If Trim(.Text) <> "" Then
                    If ChkPrintAllItem.CheckState = System.Windows.Forms.CheckState.Checked Then
                        .Col = ColPrintStatus
                        .Value = CStr(System.Windows.Forms.CheckState.Checked)
                    Else
                        .Col = ColPrintStatus
                        .Value = IIf(mCurrAmount = mPrevAmount And mPOWEF = mPrevWEF, System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
                    End If
                End If
            Next
        End With


        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub chkPrintApp_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkPrintApp.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkShipTo_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkShipTo.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        If chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtShippedTo.Enabled = False
            cmdSearchShippedTo.Enabled = False

            TxtShipTo.Enabled = False
            cmdShipToSearch.Enabled = False
        Else
            txtShippedTo.Enabled = True
            cmdSearchShippedTo.Enabled = True
            TxtShipTo.Enabled = True
            cmdShipToSearch.Enabled = True
        End If
    End Sub

    Private Sub chkStatus_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkStatus.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkTCAvailable_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkTCAvailable.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)

        cmdTC.Enabled = IIf(chkTCAvailable.CheckState = System.Windows.Forms.CheckState.Unchecked, False, True)

    End Sub

    Private Sub chkTPRAvailable_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkTPRAvailable.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        cmdTPRI.Enabled = IIf(chkTPRAvailable.CheckState = System.Windows.Forms.CheckState.Unchecked, False, True) 'cmdTC.Enabled = IIf(chkTCAvailable.Value = vbUnchecked, False, True)			
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            pShowCalc = True
            SprdMain.Enabled = True
            SprdExp.Enabled = True
            SprdAnnex.Enabled = True
            txtPONo.Enabled = False
            cmdSearchPO.Enabled = False
            cmdSearchAmend.Enabled = False
            txtPrevPONo.Enabled = True
            cmdSearchPrevPO.Enabled = True
            cmdUpdateCosting.Enabled = True
        Else
            ADDMode = False
            MODIFYMode = False
            If RsPOMain.EOF = False Then RsPOMain.MoveFirst()
            Show1()
            txtPONo.Enabled = True
            cmdSearchPO.Enabled = True
            cmdSearchAmend.Enabled = True
            txtPrevPONo.Enabled = False
            cmdSearchPrevPO.Enabled = False
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
        'Resume			
    End Sub

    Private Sub cmdAmend_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAmend.Click

        On Error GoTo ERR1
        Dim mPONo As Double
        Dim I As Integer
        Dim pCurrRate As Double
        Dim mPrice As Double
        Dim mDisc As Double
        Dim mItemCode As String
        Dim mHSNCode As String
        Dim pCGSTPer As Double
        Dim pSGSTPer As Double
        Dim pIGSTPer As Double

        Dim mLocal As String
        Dim mPartyGSTNo As String

        Dim mPurchaseInvTypeCode As Double

        Dim mInvTypeDesc As String


        '     lblBookType.text			
        '     Right(lblBookType, 1)			

        'If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
        '    If lblBookType.Text = "PC" Then
        '        MsgInformation("You Cann't be Amend for Closed Order")
        '        'MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols)
        '        Exit Sub
        '    End If
        'End If


        mPONo = Val(txtPONo.Text)

        If mPONo = 0 Then
            MsgInformation("Please Select PO.")
            Exit Sub
        End If

        Call txtAmendNo_Validating(txtAmendNo, New System.ComponentModel.CancelEventArgs(True)) '' txtPONO_Validate(True)			

        If CheckUnPostedPO(mPONo) = True Then
            txtPONo.Enabled = True
            cmdSearchPO.Enabled = True
            cmdSearchAmend.Enabled = True
            cmdSearchAmend.Focus()
            Exit Sub
        End If

        txtAmendNo.Text = CStr(GetMaxAmendNo(mPONo))
        txtAmendDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        ChkActivate.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked
        txtRecdDate.Text = ""
        chkRecdAcct.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkPrintApp.CheckState = System.Windows.Forms.CheckState.Unchecked

        chkApprovedWO_TC.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkTCAvailable.Enabled = False

        chkTCAvailable.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkTCAvailable.Enabled = True
        txtTCPath.Text = ""
        cmdTC.Enabled = True

        chkTPRAvailable.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkTPRAvailable.Enabled = True
        txtTPRPath.Text = ""
        cmdTPRI.Enabled = True

        txtSupplierName.Enabled = False
        cmdsearch.Enabled = False

        txtDivision.Enabled = IIf(Trim(txtDivision.Text) = "", True, False)
        cmdDivSearch.Enabled = IIf(Trim(txtDivision.Text) = "", True, False)

        mAmendStatus = True
        cmdAmend.Enabled = False

        ADDMode = True
        MODIFYMode = False
        'If lblBookType.Text = "PC" Then
        '    SprdMain.Enabled = True    '' False Sandeep 15/05/2022     '' IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)
        'Else
        SprdMain.Enabled = True
        'End If

        mLocal = "N"
        mPartyGSTNo = ""
        mLocal = GetPartyBusinessDetail(Trim(txtCode.Text), Trim(txtBillTo.Text), "WITHIN_STATE")
        mPartyGSTNo = GetPartyBusinessDetail(Trim(txtCode.Text), Trim(txtBillTo.Text), "GST_RGN_NO")

        'mLocal = "N"
        'If MainClass.ValidateWithMasterTable(Trim(txtCode.Text), "SUPP_CUST_CODE", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mLocal = MasterNo
        'End If

        'mPartyGSTNo = ""
        'If MainClass.ValidateWithMasterTable(Trim(txtCode.Text), "SUPP_CUST_CODE", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mPartyGSTNo = MasterNo
        'End If

        If Val(txtAmendNo.Text) <> 0 Then
            For I = 1 To SprdMain.MaxRows - 1
                SprdMain.Row = I

                SprdMain.Col = ColItemCode
                mItemCode = Trim(SprdMain.Text)



                SprdMain.Col = ColHSN
                If Trim(SprdMain.Text) = "" Then
                    mHSNCode = GetHSNCode(mItemCode)
                    SprdMain.Text = mHSNCode
                Else
                    mHSNCode = Trim(SprdMain.Text)
                End If

                If GetHSNDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, VB.Left(cboGSTStatus.Text, 1), mPartyGSTNo) = False Then GoTo ERR1

                SprdMain.Col = ColCGSTPer
                SprdMain.Text = VB6.Format(pCGSTPer, "0.00")

                SprdMain.Col = ColSGSTPer
                SprdMain.Text = VB6.Format(pSGSTPer, "0.00")

                SprdMain.Col = ColIGSTPer
                SprdMain.Text = VB6.Format(pIGSTPer, "0.00")

                SprdMain.Col = ColAcctPostName
                If Trim(SprdMain.Text) = "" Then
                    mPurchaseInvTypeCode = CDbl(GetItemPurchaseInvoiceType(mItemCode))
                    mInvTypeDesc = ""
                    If MainClass.ValidateWithMasterTable(mPurchaseInvTypeCode, "CODE", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P'") = True Then
                        mInvTypeDesc = MasterNo
                    End If

                    SprdMain.Col = ColAcctPostName
                    SprdMain.Text = Trim(mInvTypeDesc)
                End If
            Next
        End If

        If lblBookType.Text = "PO" Or (lblBookType.Text = "JC") Then
            If Val(txtAmendNo.Text) <> 0 Then
                For I = 1 To SprdMain.MaxRows - 1
                    SprdMain.Row = I

                    SprdMain.Col = ColItemRate
                    mPrice = Val(SprdMain.Text)

                    SprdMain.Col = ColItemDisc
                    mDisc = Val(SprdMain.Text)

                    pCurrRate = mPrice - System.Math.Round((mPrice * mDisc) / 100, 4)

                    SprdMain.Col = ColGross_Prev
                    SprdMain.Text = VB6.Format(pCurrRate, "0.0000")
                Next
            End If
        End If

        SprdExp.Enabled = True
        SprdAnnex.Enabled = True
        txtPONo.Enabled = False
        cmdSearchPO.Enabled = False
        cmdSearchAmend.Enabled = False
        cmdUpdateCosting.Enabled = True

        'If lblBookType.Text = "PC" Then
        '    'MsgInformation("You Cann't be Amend for Closed Order")
        '    If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
        '        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols)
        '    Else

        '    End If

        '    'Exit Sub
        'End If

        MainClass.ButtonStatus(Me, XRIGHT, RsPOMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ERR1:

    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        On Error Resume Next
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If ValidateBranchLocking((txtAmendDate.Text)) = True Then
            Exit Sub
        End If

        If ValidateBookLocking(PubDBCn, CInt(ConLockPO), txtPODate.Text) = True Then
            Exit Sub
        End If

        If ValidateAccountLocking(PubDBCn, (txtPODate.Text), (txtSupplierName.Text)) = True Then
            Exit Sub
        End If

        If MainClass.GetUserCanModify((txtAmendDate.Text)) = False Then
            MsgBox("You Have Not Rights to delete back P.O.", MsgBoxStyle.Information)
            Exit Sub
        End If

        If chkStatus.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Posted PO Cann't be Deleted")
            Exit Sub
        End If

        If txtPONo.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If Not RsPOMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.			
                PubDBCn.Cancel()
                PubDBCn.BeginTrans()

                If InsertIntoDelAudit(PubDBCn, "PUR_PURCHASE_HDR", (txtPONo.Text), RsPOMain, "PO NO", "D") = False Then GoTo DelErrPart

                If InsertIntoDeleteTrn(PubDBCn, "PUR_PURCHASE_HDR", "MKEY", (lblMkey.Text)) = False Then GoTo DelErrPart

                PubDBCn.Execute("Delete from PUR_POCONS_IND_TRN Where MKEY='" & lblMkey.Text & "'")
                PubDBCn.Execute("Delete from PUR_PURCHASE_EXP Where MKEY='" & lblMkey.Text & "'")
                PubDBCn.Execute("Delete from FIN_DNCN_AMEND Where POMKEY='" & lblMkey.Text & "' AND IS_DNCN_MADE='N'")
                PubDBCn.Execute("Delete from PUR_PURCHASE_ANNEX Where MKEY='" & lblMkey.Text & "'")
                PubDBCn.Execute("DELETE FROM PUR_PURCHASE_DET WHERE MKEY=" & Val(lblMkey.Text) & "")
                PubDBCn.Execute("DELETE FROM PUR_PURCHASE_HDR WHERE MKEY=" & Val(lblMkey.Text) & " AND PUR_TYPE='" & VB.Left(lblBookType.Text, 1) & "' AND ORDER_TYPE='" & VB.Right(lblBookType.Text, 1) & "'")

                PubDBCn.CommitTrans()
                RsPOMain.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Cancel()
        RsPOMain.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsPOMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
            SprdExp.Enabled = True
            SprdAnnex.Enabled = True
            txtPONo.Enabled = False
            cmdSearchPO.Enabled = False
            cmdSearchAmend.Enabled = False
            pShowCalc = True
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdOwner_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOwner.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If ADDMode = True Then
            SqlStr = SqlStr & " AND STATUS='O'"
        End If
        If MainClass.SearchGridMaster((txtOwner.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            txtOwner.Text = AcName
            txtOwner_Validating(txtOwner, New System.ComponentModel.CancelEventArgs(True))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdPaySearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPaySearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mIsMSMESupplier As String

        If MainClass.ValidateWithMasterTable(txtSupplierName.Text, "SUPP_CUST_NAME", "SME_REGD", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SME_REGD='Y'") = True Then
            mIsMSMESupplier = "Y"
        Else
            mIsMSMESupplier = "N"
        End If



        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If mIsMSMESupplier = "Y" Then
            SqlStr = SqlStr & " AND FOR_MSME='Y'"
        End If
        If MainClass.SearchGridMaster((txtPayment.Text), "FIN_PAYTERM_MST", "PAY_TERM_DESC", "PAY_TERM_CODE", , , SqlStr) = True Then
            txtPayment.Text = AcName1
            txtPayment_Validating(txtPayment, New System.ComponentModel.CancelEventArgs(False))
            txtPayment.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportonPO(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportonPO(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportonPO(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""
        Dim SqlStr As String = ""
        Dim mRptFileName As String
        Dim mVNo As String
        Dim mItemCodeWisePrint As Boolean
        Dim mPrintSubReport As Boolean

        Dim CntRow As Integer
        Dim mItemCode As String
        Dim mProductType As String
        Dim mDraftPrint As Boolean

        Dim mCheckBOPPO As Boolean
        Dim mPOWEF As String

        mCheckBOPPO = False

        With SprdMain
            For CntRow = 1 To .MaxRows - 1
                .Row = CntRow
                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)
                mProductType = GetProductionType(mItemCode)

                If CheckTCRequired(mItemCode) = True And chkTCAvailable.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                    If chkApprovedWO_TC.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                        MsgInformation("TC Required for Such Item, So PO can't be Print.")
                        Exit Sub
                    End If
                    If chkApprovedWO_TC.CheckState = System.Windows.Forms.CheckState.Checked And chkTPRAvailable.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                        MsgInformation("If TC is not available than Third Party Report is must, So PO can't be Print.")
                        Exit Sub
                    End If
                End If
            Next
        End With


        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        SqlStr = ""
        mSubTitle = ""
        mPrintSubReport = False

        If mAuthorisedPrint = False Then
            MsgInformation("You have no Authorisation rights to print PO, so cann't be print PO.")
            Exit Sub
        End If
        mItemCodeWisePrint = True
        mDraftPrint = False

        'If lblBookType.Text = "WC" Or lblBookType.Text = "RC" Then
        '    mItemCodeWisePrint = True
        '    mDraftPrint = IIf(chkPrintApp.CheckState = System.Windows.Forms.CheckState.Unchecked, True, False)
        'Else
        '    If RsCompany.Fields("PO_PRINT_APP_REQ").Value = "Y" Then
        '        If chkPrintApp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
        '            frmPrintPO.optPrintType(0).Checked = True
        '            frmPrintPO.optPrintType(1).Enabled = False
        '        Else
        '            frmPrintPO.optPrintType(1).Checked = True
        '            frmPrintPO.optPrintType(0).Enabled = False
        '        End If
        '    End If

        '    frmPrintPO.ShowDialog()

        '    If G_PrintLedg = False Then
        '        Exit Sub
        '    End If

        '    mDraftPrint = IIf(frmPrintPO.optPrintType(0).Checked = True, True, False)
        '    mItemCodeWisePrint = IIf(frmPrintPO.OptPrint(0).Checked = True, True, False)
        'End If


        'mDraftPrint = False
        'If mDraftPrint = True Then
        '    mTitle = "Draft "
        'Else
        '    mTitle = ""
        'End If

        If VB.Left(lblBookType.Text, 1) = "J" Then
            mTitle = mTitle & "Jobwork Order"
        ElseIf VB.Left(lblBookType.Text, 1) = "P" Then
            mTitle = mTitle & "Purchase Order"
        ElseIf VB.Left(lblBookType.Text, 1) = "R" Then
            mTitle = mTitle & "Purchase Order"
        ElseIf VB.Left(lblBookType.Text, 1) = "W" Then
            mTitle = mTitle & "Service Purchase Order"
        ElseIf VB.Left(lblBookType.Text, 1) = "L" Then
            mTitle = mTitle & "Assets Under Lease"
        End If

        If RsCompany.Fields("DIV_AS_LOCATION").Value = "N" Then
            If chkCapital.CheckState = System.Windows.Forms.CheckState.Checked Then
                mTitle = mTitle & " - CAPITAL"
            End If

            If chkDevelopment.CheckState = System.Windows.Forms.CheckState.Checked Then
                mTitle = mTitle & " (DEVELOPMENT)"
            End If

            If VB.Right(lblBookType.Text, 1) = "O" Then
                mSubTitle = "(OPEN)"
            ElseIf VB.Right(lblBookType.Text, 1) = "C" Then
                If VB.Left(lblBookType.Text, 1) = "R" Then
                    mSubTitle = "(PROJECT)"
                Else
                    If VB.Left(lblBookType.Text, 1) = "P" Then
                        mSubTitle = "(CLOSE)"
                    End If
                End If
            End If


            If Val(txtDivision.Text) <> 1 Then
                mSubTitle = mSubTitle & " - " & Trim(lblDivision.Text)
            End If
        End If

        If Val(txtAmendNo.Text) > 0 Then
            mSubTitle = mSubTitle & "-AMENDMENT"
        End If

        mSubTitle = mSubTitle & IIf(mDraftPrint = True, "(Approval Pending)", "")

        Call MainClass.ClearCRptFormulas(Report1)

        If InserIntoTemp(mItemCodeWisePrint) = False Then GoTo ERR1

        If lblBookType.Text = "WC" Or lblBookType.Text = "RC" Then
            Call SelectQryForWO(SqlStr)
            If RsCompany.Fields("DIV_AS_LOCATION").Value = "Y" Then
                mRptFileName = "WO_PRN_GST_LOC.rpt"
            Else
                mRptFileName = "WO_PRN_GST.rpt"
            End If

            mPrintSubReport = True
        Else
            Call SelectQryForPO(SqlStr, mItemCodeWisePrint)
            If RsCompany.Fields("DIV_AS_LOCATION").Value = "Y" Then
                mRptFileName = IIf(mItemCodeWisePrint = True, "PO_PRN_GST_LOC.rpt", "PO_TECHWISE_GST_LOC.RPT")
            Else
                If lblRMPO.Text = "R" Then
                    mRptFileName = "PO_RMPRN_GST.rpt"
                    mPrintSubReport = True
                Else
                    mRptFileName = IIf(mItemCodeWisePrint = True, "PO_PRN_GST.rpt", "PO_TECHWISE_GST.RPT")
                    mPrintSubReport = True
                End If
            End If

        End If

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName, False, mPrintSubReport)

        If RsCompany.Fields("DIV_AS_LOCATION").Value = "N" Then
            If MsgQuestion("Are You want to Print Terms & Condition Page. ...") = CStr(MsgBoxResult.Yes) Then
                Report1.Reset()
                MainClass.ClearCRptFormulas(Report1)
                'mTitle = "Purchase Order AMENDMENT" & IIf(mDraftPrint = True, "(Approval Pending)", "")
                'mSubTitle = IIf(Val(txtDivision.Text) = 1, "", lblDivision.Text)
                'Call InsertPrintDummy(IIf(mItemCodeWisePrint = True, False, True))
                'SqlStr = MainClass.FetchFromTempData(SqlStr, IIf(mItemCodeWisePrint = True, "SUBROW", "FIELD11"))
                mRptFileName = "POTERMS_PRN_GST.rpt"

                Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName, False, False)
            End If


            If VB.Right(lblBookType.Text, 1) = "O" And Val(txtAmendNo.Text) > 0 Then
                If MsgQuestion("Are You want to Print Amendment Covering Letter. ...") = CStr(MsgBoxResult.Yes) Then
                    Report1.Reset()
                    MainClass.ClearCRptFormulas(Report1)
                    mTitle = "Purchase Order AMENDMENT" & IIf(mDraftPrint = True, "(Approval Pending)", "")
                    mSubTitle = IIf(Val(txtDivision.Text) = 1, "", lblDivision.Text)
                    Call InsertPrintDummy(IIf(mItemCodeWisePrint = True, False, True))
                    SqlStr = MainClass.FetchFromTempData(SqlStr, IIf(mItemCodeWisePrint = True, "SUBROW", "FIELD11"))
                    If RsCompany.Fields("DIV_AS_LOCATION").Value = "Y" Then
                        mRptFileName = "POAmend_LOC.rpt"
                    Else
                        mRptFileName = "POAmend.rpt"
                    End If
                    Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName, True, False)
                End If
            End If

            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
                If MsgQuestion("Are You want to Print Supplier Wise Item Comparison. ...") = CStr(MsgBoxResult.Yes) Then
                    Report1.Reset()
                    MainClass.ClearCRptFormulas(Report1)
                    mTitle = "Comparison Sheet"
                    Call InsertPrintDummyForComparision()
                    SqlStr = MainClass.FetchFromTempData(SqlStr, "FIELD6")
                    mRptFileName = "POComparison.rpt"
                    Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName, True, False)
                End If
            End If

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            With SprdMain
                For CntRow = 1 To .MaxRows - 1
                    .Row = CntRow
                    .Col = ColItemCode
                    mItemCode = MainClass.AllowSingleQuote(.Text)

                    .Col = ColPO_WEF
                    If lblBookType.Text = "PC" Then
                        mPOWEF = VB6.Format(Trim(txtWEF.Text), "DD/MM/YYYY")
                    Else
                        mPOWEF = VB6.Format(Trim(.Text), "DD/MM/YYYY")
                    End If

                    If GetCostingRequired(mItemCode) = True Then
                        If MsgQuestion("Are You want to Print Costing Sheet. ...") = CStr(MsgBoxResult.Yes) Then
                            Call ReportOnCosting(mItemCode, mPOWEF, Crystal.DestinationConstants.crptToWindow)
                        End If
                    End If
                Next
            End With
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        'frmPrintPO.Close()
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'frmPrintPO.Close()
    End Sub

    Private Sub ReportOnCosting(ByRef nItemCode As String, ByRef nWEF As String, ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim SqlStr1 As String
        Dim SqlStr2 As String
        Dim SqlStr3 As String
        Dim SqlStr4 As String
        Dim SqlStr5 As String
        Dim SqlStr6 As String
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""

        Dim SubSqlStr1 As String
        Dim SubSqlStr2 As String
        Dim RsTemp1 As ADODB.Recordset = Nothing
        Dim RSTemp2 As ADODB.Recordset
        Dim mSuppCustCode As String
        Dim mItemCode As String
        Dim nMkey As String
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = " SELECT IH.MKEY" & vbCrLf _
            & " FROM PRD_BOP_COST_HDR IH" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND IH.ITEM_CODE='" & MainClass.AllowSingleQuote(nItemCode) & "' " & vbCrLf _
            & " AND IH.WEF=TO_DATE('" & VB6.Format(nWEF, "DD/MMM/YYYY") & "','DD-MON-YYYY') "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            nMkey = IIf(IsDBNull(RsTemp.Fields("mKey").Value), "", RsTemp.Fields("mKey").Value)
        Else
            MsgInformation("Costing is not Available, Please check the Costing")
            Exit Sub
        End If

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        mTitle = "BOP Costing"

        SqlStr = " SELECT IH.*, ID.*, CMST.SUPP_CUST_NAME, INVMST.ITEM_SHORT_DESC, " & vbCrLf & " PRODMST.MTRL_DESC, PRODMST.MTRL_DENSITY, PREP.EMP_NAME AS PREP_BY, APP.EMP_NAME AS APP_BY" & vbCrLf & " FROM PRD_BOP_COST_HDR IH, PRD_BOP_COST_DET ID, FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST," & vbCrLf & " PRD_MTRL_MST PRODMST, PAY_EMPLOYEE_MST PREP, PAY_EMPLOYEE_MST APP " & vbCrLf & " WHERE IH.MKEY=ID.MKEY(+) " & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND ID.COMPANY_CODE=PRODMST.COMPANY_CODE(+) AND ID.RM_CODE=PRODMST.MTRL_CODE(+) " & vbCrLf & " AND IH.COMPANY_CODE=PREP.COMPANY_CODE(+) " & vbCrLf & " AND IH.PREPARED_BY=PREP.EMP_CODE(+) " & vbCrLf & " AND IH.COMPANY_CODE=APP.COMPANY_CODE(+) " & vbCrLf & " AND IH.APP_EMP_CODE=APP.EMP_CODE(+) " & vbCrLf & " AND IH.MKEY='" & MainClass.AllowSingleQuote(nMkey) & "' "

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\BOPCosting.rpt"
        SetCrpt(Report1, Mode, 1, mTitle, mSubTitle, True)
        Report1.SQLQuery = SqlStr
        Report1.WindowShowGroupTree = False


        SqlStr2 = " SELECT * FROM PRD_BOP_PART_DET " & vbCrLf & " WHERE " & vbCrLf & " MKEY='" & MainClass.AllowSingleQuote(nMkey) & "' " & vbCrLf & " ORDER BY SUBROWNO "

        Report1.SubreportToChange = Report1.GetNthSubreportName(0)
        Report1.Connect = STRRptConn
        Report1.SQLQuery = SqlStr2

        SqlStr3 = " SELECT * FROM PRD_BOP_PROCESS1_DET OPR, PRD_OPR_MST OPRMST,  INV_ITEM_MST INVMST" & vbCrLf & " WHERE OPR.MKEY='" & MainClass.AllowSingleQuote(nMkey) & "'" & vbCrLf & " AND OPR.COMPANY_CODE=OPRMST.COMPANY_CODE AND OPR.OPR_CODE=OPRMST.OPR_CODE AND OPR.COMPANY_CODE=INVMST.COMPANY_CODE AND OPR.MACHINE_ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " ORDER BY SUBROWNO "

        Report1.SubreportToChange = Report1.GetNthSubreportName(1)
        Report1.Connect = STRRptConn
        Report1.SQLQuery = SqlStr3

        SqlStr4 = " SELECT * FROM PRD_BOP_PROCESS2_DET " & vbCrLf & " WHERE MKEY='" & MainClass.AllowSingleQuote(nMkey) & "' " & vbCrLf & " ORDER BY SUBROWNO "

        Report1.SubreportToChange = Report1.GetNthSubreportName(2)
        Report1.Connect = STRRptConn
        Report1.SQLQuery = SqlStr4


        SqlStr5 = " SELECT * FROM PRD_BOP_OPERATION_DET OPR, PRD_OPR_MST OPRMST" & vbCrLf & " WHERE OPR.MKEY='" & MainClass.AllowSingleQuote(nMkey) & "' AND OPR.COMPANY_CODE=OPRMST.COMPANY_CODE AND OPR.OPR_CODE=OPRMST.OPR_CODE" & vbCrLf & " ORDER BY OPR.SUBROWNO "

        Report1.SubreportToChange = Report1.GetNthSubreportName(3)
        Report1.Connect = STRRptConn
        Report1.SQLQuery = SqlStr5


        SqlStr6 = " SELECT * FROM PRD_BOP_EXP_COST_DET COSTEXP, PRD_COSTINGEXP_MST EXPMST " & vbCrLf & " WHERE COSTEXP.MKEY='" & MainClass.AllowSingleQuote(nMkey) & "' AND COSTEXP.COMPANY_CODE=EXPMST.COMPANY_CODE AND COSTEXP.EXP_CODE=EXPMST.CODE" & vbCrLf & " ORDER BY SUBROWNO "

        Report1.SubreportToChange = Report1.GetNthSubreportName(4)
        Report1.Connect = STRRptConn
        Report1.SQLQuery = SqlStr6


        Report1.SubreportToChange = ""

        Report1.Action = 1



        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        '    Resume			
    End Sub

    Private Function InsertPrintDummy(ByRef pIsGroupWise As Boolean) As Boolean


        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim CntRow As Integer
        Dim cntSubRow As Integer
        Dim mItemCode As String
        Dim mItemName As String
        Dim mItemUOM As String = ""
        Dim mGross As Double
        Dim mGross_Prev As Double

        Dim mSuppName As String = ""
        Dim mSuppAddress As String = ""
        Dim mSuppCity As String = ""
        Dim mSuppState As String = ""
        Dim mSuppCode As String = ""
        Dim mPONo As String = ""
        Dim mPODate As String = ""
        Dim AmmedNo As String = ""
        Dim WEFDate As String = ""
        Dim mPrice As Double
        Dim mDisc As Double
        Dim mECCNo As String = ""
        Dim mCSTNo As String = ""
        Dim mTINNo As String = ""
        Dim mDescStr As String = ""
        Dim mPrevWEF As String = ""
        Dim mPOWEF As String = ""
        Dim mHSNCode As String = ""

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM Temp_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        mSuppName = Trim(UCase(txtSupplierName.Text))
        mSuppCode = Trim(txtCode.Text)
        mPONo = Trim(txtPONo.Text)
        mPODate = Trim(txtPODate.Text)
        AmmedNo = Trim(txtAmendNo.Text)
        WEFDate = Trim(txtWEF.Text)


        SqlStr = "SELECT * FROM FIN_SUPP_CUST_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & Trim(txtCode.Text) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mSuppAddress = UCase(IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_ADDR").Value), "", RsTemp.Fields("SUPP_CUST_ADDR").Value))
            mSuppCity = UCase(IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value))
            mSuppState = UCase(IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_STATE").Value), "", RsTemp.Fields("SUPP_CUST_STATE").Value))
            mSuppState = UCase(mSuppState & " - " & IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_PIN").Value), "", RsTemp.Fields("SUPP_CUST_PIN").Value))
            mECCNo = UCase(IIf(IsDBNull(RsTemp.Fields("ECC_NO").Value), "", RsTemp.Fields("ECC_NO").Value))
            mCSTNo = UCase(IIf(IsDBNull(RsTemp.Fields("CST_NO").Value), "", RsTemp.Fields("CST_NO").Value))
            mTINNo = UCase(IIf(IsDBNull(RsTemp.Fields("ACCOUNT_CODE").Value), "", RsTemp.Fields("ACCOUNT_CODE").Value))
        End If

        SqlStr = ""
        With SprdMain
            For CntRow = 1 To .MaxRows
                .Row = CntRow

                .Col = ColItemCode
                mItemCode = Trim(.Text)

                If pIsGroupWise = False Then
                    .Col = ColItemName
                    mItemName = Trim(.Text)
                    mItemCode = mItemCode & New String(" ", 10 - Len(mItemCode))
                    mItemName = mItemCode & Trim(.Text)
                    .Col = ColIsTentativeRate
                    If Trim(.Value) = CStr(System.Windows.Forms.CheckState.Checked) Then
                        mItemName = mItemName & " (Tentative Rate)"
                    End If
                Else
                    mItemName = GetSubCategoryName(mItemCode)
                End If

                If pIsGroupWise = False Then
                    .Col = ColRemarks
                    If Trim(.Text) <> "" Then
                        mItemName = mItemName & "(" & Trim(.Text) & ")"
                    End If
                End If

                .Col = ColItemUOM
                mItemUOM = .Text

                .Col = ColHSN
                mHSNCode = .Text

                .Col = ColItemRate
                mPrice = Val(.Text)

                .Col = ColItemDisc
                mDisc = Val(.Text)

                mGross = mPrice - System.Math.Round((mPrice * mDisc) / 100, 4)

                .Col = ColGross_Prev
                mGross_Prev = Val(.Text)

                .Col = ColPO_WEF
                mPOWEF = VB6.Format(.Text, "DD/MM/YYYY")

                .Col = ColPrevPO_WEF
                mPrevWEF = VB6.Format(.Text, "DD/MM/YYYY")

                If pIsGroupWise = True Then
                    If InStr(1, mDescStr, mItemName & ",") > 0 Then
                        GoTo NextRow
                    End If
                End If

                .Col = ColPrintStatus
                If Val(CStr(mGross_Prev)) <> Val(CStr(mGross)) Or mPOWEF <> mPrevWEF Then
                    cntSubRow = cntSubRow + 1
                    SqlStr = "Insert into Temp_PrintDummyData (UserID,SubRow, " & vbCrLf _
                        & " Field1,Field2,Field3,Field4,Field5," & vbCrLf _
                        & " Field6,Field7,Field8,Field9,Field10,Field11, Field12, " & vbCrLf _
                        & " Field13,Field14, Field15, Field16, Field17, Field18, Field19, Field20, Field21, Field22,Field23) Values (" & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                        & " " & cntSubRow & ", " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mSuppName) & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mSuppAddress) & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mSuppCity) & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mSuppState) & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mSuppCode) & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mPONo) & "', " & vbCrLf _
                        & " '" & VB6.Format(mPODate, "DD/MM/YYYY") & "', " & vbCrLf _
                        & " '" & AmmedNo & "','" & VB6.Format(WEFDate, "DD/MM/YYYY") & "','" & MainClass.AllowSingleQuote(mItemCode) & "','" & MainClass.AllowSingleQuote(mItemName) & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mItemUOM) & "'," & vbCrLf _
                        & " '" & IIf(mGross_Prev = 0, "-", VB6.Format(mGross_Prev, "0.000")) & "', '" & VB6.Format(mGross, "0.000") & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mECCNo) & "', '" & MainClass.AllowSingleQuote(mCSTNo) & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mTINNo) & "','" & MainClass.AllowSingleQuote(txtRemarks.Text) & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(lblPaymentTerms.Text) & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(txtExcise.Text) & "', '', '" & MainClass.AllowSingleQuote(txtPacking.Text) & "','" & MainClass.AllowSingleQuote(mHSNCode) & "') "

                    PubDBCn.Execute(SqlStr)

                    If mDescStr = "" Then
                        mDescStr = mItemName
                    Else
                        mDescStr = mDescStr & ", " & mItemName
                    End If
                    mDescStr = mDescStr & ","
                    '                End If			
                End If

NextRow:
            Next
        End With
        PubDBCn.CommitTrans()
        InsertPrintDummy = True
        Exit Function
ERR1:
        '    Resume			
        MsgInformation(Err.Description)
        InsertPrintDummy = False
        PubDBCn.RollbackTrans()
    End Function


    Private Function InsertPrintDummyForComparision() As Boolean


        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mItemCode As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM Temp_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = ""
        With SprdMain
            For CntRow = 1 To .MaxRows
                .Row = CntRow

                .Col = ColItemCode
                mItemCode = Trim(.Text)

                If mItemCode <> "" Then
                    SqlStr = "Insert into Temp_PrintDummyData (UserID,SubRow, " & vbCrLf _
                        & " Field1,Field2,Field3,Field4,Field5," & vbCrLf _
                        & " Field6,Field7,Field8,Field9,Field10) " & vbCrLf _
                        & " SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "', " & CntRow & ", " & vbCrLf _
                        & " IH.AUTO_KEY_PO, IH.AMEND_NO, IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME," & vbCrLf _
                        & " ID.ITEM_CODE, IMST.ITEM_SHORT_DESC, ID.ITEM_UOM, ID.ITEM_PRICE, ID.ITEM_DIS_PER, TO_CHAR(ID.PO_WEF_DATE,'DD/MM/YYYY')" & vbCrLf _
                        & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID, FIN_SUPP_CUST_MST CMST, INV_ITEM_MST IMST" & vbCrLf _
                        & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
                        & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
                        & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
                        & " AND IH.COMPANY_CODE=IMST.COMPANY_CODE" & vbCrLf _
                        & " AND ID.ITEM_CODE=IMST.ITEM_CODE" & vbCrLf _
                        & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND PO_STATUS='Y' AND PO_CLOSED='N' AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"


                    PubDBCn.Execute(SqlStr)

                End If

NextRow:
            Next
        End With
        PubDBCn.CommitTrans()
        InsertPrintDummyForComparision = True
        Exit Function
ERR1:
        '    Resume			
        MsgInformation(Err.Description)
        InsertPrintDummyForComparision = False
        PubDBCn.RollbackTrans()
    End Function
    Private Sub ReportonPOAnnex(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""
        Dim SqlStr As String = ""
        Dim mRptFileName As String
        Dim mVNo As String

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        SqlStr = ""
        mSubTitle = ""


        mTitle = "Annexure"
        Call SelectQryForAnnex(SqlStr)
        mRptFileName = "PO_ANNEX.rpt"

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName, True, False)

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Function InserIntoTemp(ByRef pItemCodeWisePrint As Boolean) As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim CntRow As Integer
        Dim mPrintStaus As String
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mWOItemDesc As String
        Dim mTechDesc As String
        Dim mDrwNo As String
        Dim mDrwRNo As String = ""
        Dim mHSNCode As String = ""

        Dim mCGSTPer As Double = 0
        Dim mSGSTPer As Double = 0
        Dim mIGSTPer As Double = 0
        Dim mCGSTAmount As Double = 0
        Dim mSGSTAmount As Double = 0
        Dim mIGSTAmount As Double = 0


        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM Temp_PO_PRN NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        If lblRMPO.Text = "R" Then
            SprdMain.Row = 1

            SprdMain.Col = ColItemUOM
            mItemCode = Trim(SprdMain.Text)

            SprdMain.Col = ColHSN
            mHSNCode = Trim(SprdMain.Text)

            SprdMain.Col = ColCGSTPer
            mCGSTPer = Val(SprdMain.Text)

            SprdMain.Col = ColCGSTAmount
            mCGSTAmount = Val(mCGSTPer) * Val(txtRMQty.Text) * Val(txtRMRate.Text) * 0.01

            SprdMain.Col = ColSGSTPer
            mSGSTPer = Val(SprdMain.Text)

            SprdMain.Col = ColSGSTAmount
            mSGSTAmount = Val(mSGSTPer) * Val(txtRMQty.Text) * Val(txtRMRate.Text) * 0.01

            SprdMain.Col = ColIGSTPer
            mIGSTPer = Val(SprdMain.Text)

            SprdMain.Col = ColIGSTAmount
            mIGSTAmount = Val(mIGSTPer) * Val(txtRMQty.Text) * Val(txtRMRate.Text) * 0.01


            SqlStr = "Insert into Temp_PO_PRN (UserID, " & vbCrLf _
                & " ITEM_CODE, ITEM_SHORT_DESC, PRINT_STATUS, ITEM_TECH_DESC,HSNCODE," & vbCrLf _
                & " RM_CGST_PER, RM_SGST_PER, RM_IGST_PER, " & vbCrLf _
                & " RM_CGST_AMOUNT, RM_SGST_AMOUNT, RM_IGST_AMOUNT, SUBROWNO " & vbCrLf _
                & " ) Values (" & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                & " '" & mItemCode & "', '" & MainClass.AllowSingleQuote(txtRMDesc.Text) & "', " & vbCrLf _
                & " 'Y','','" & mHSNCode & "'," & vbCrLf _
                & " " & mCGSTPer & ", " & mSGSTPer & ", " & mIGSTPer & ", " & vbCrLf _
                & " " & mCGSTAmount & ", " & mSGSTAmount & ", " & mIGSTAmount & ", 1 " & vbCrLf _
                & " ) "


            PubDBCn.Execute(SqlStr)

            PubDBCn.CommitTrans()
            InserIntoTemp = True
            Exit Function
        End If

        SqlStr = ""
        With SprdMain
            For CntRow = 1 To .MaxRows - 1
                .Row = CntRow

                .Col = ColItemCode
                mItemCode = Replace(.Text, "'", "''")
                mItemCode = IIf(Trim(mItemCode) = "", -1, mItemCode)

                .Col = ColItemName
                If pItemCodeWisePrint = True Then
                    mItemDesc = Trim(.Text)

                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_TECH_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mTechDesc = Trim(MasterNo)
                    Else
                        mTechDesc = ""
                    End If


                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "DRAWING_NO", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mDrwNo = Trim(MasterNo)
                    Else
                        mDrwNo = ""
                    End If

                    If mDrwNo <> "" Then
                        If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "DRW_REVNO", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mDrwRNo = Trim(MasterNo)
                        Else
                            mDrwRNo = ""
                        End If
                    End If


                    mItemDesc = mItemDesc & IIf(mDrwNo = "", "", " DWG NO : " & mDrwNo) & IIf(mDrwNo = "", "", IIf(mDrwRNo = "", "", " - " & mDrwRNo))

                    .Col = ColHSN
                    mHSNCode = Trim(.Text)

                Else
                    mItemDesc = GetSubCategoryName(mItemCode)
                    mTechDesc = ""

                    mHSNCode = ""
                End If
                mItemDesc = Replace(mItemDesc, "'", "''")

                .Col = ColWoDesc
                mWOItemDesc = Replace(.Text, "'", "''")

                If VB.Left(lblBookType.Text, 1) = "W" Or VB.Left(lblBookType.Text, 1) = "R" Then
                    mItemDesc = mWOItemDesc
                End If

                .Col = ColPrintStatus
                mPrintStaus = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")


                SqlStr = "Insert into Temp_PO_PRN (UserID, " & vbCrLf _
                    & " ITEM_CODE, ITEM_SHORT_DESC, PRINT_STATUS, ITEM_TECH_DESC,HSNCODE,SUBROWNO) Values (" & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                    & " '" & mItemCode & "', '" & mItemDesc & "', " & vbCrLf _
                    & " '" & mPrintStaus & "','" & MainClass.AllowSingleQuote(mTechDesc) & "','" & mHSNCode & "'," & CntRow & ") "

                PubDBCn.Execute(SqlStr)
NextRow:
            Next
        End With
        PubDBCn.CommitTrans()
        InserIntoTemp = True
        Exit Function
ERR1:
        PubDBCn.RollbackTrans()
        MsgInformation(Err.Description)
        InserIntoTemp = False
    End Function

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String, ByRef ISAnnexPrint As Boolean, ByRef pPrintSubReport As Boolean)
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
        Dim mShipToAddress As String = ""
        Dim mShipToCity As String = ""
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
        Dim mShipContactNo As String = ""

        'If UCase(mRptFileName) = "PO_PRN_UNIT1.RPT" Then
        '    SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, True, pmyMenu)
        'Else
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        'End If


        If chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked Then
            mShipToName = txtSupplierName.Text
            mShipToLocation = txtBillTo.Text
        Else
            mShipToName = txtShippedTo.Text
            mShipToLocation = TxtShipTo.Text
        End If

        'mPartyGSTNo = GetPartyBusinessDetail(Trim(txtCode.Text), Trim(txtBillTo.Text), "GST_RGN_NO")

        'SqlStr = " SELECT SUPP_CUST_NAME, SUPP_CUST_ADDR, SUPP_CUST_CITY, SUPP_CUST_STATE,  " & vbCrLf _
        '    & " SUPP_CUST_PIN, GST_RGN_NO" & vbCrLf _
        '    & " FROM FIN_SUPP_CUST_MST " & vbCrLf _
        '    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        '    & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(mShipToName) & "'"

        If RsCompany.Fields("DIV_AS_LOCATION").Value = "Y" Then
            SqlStr = "SELECT * FROM INV_DIVISION_MST" & vbCrLf _
                   & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                   & " AND DIV_CODE='" & MainClass.AllowSingleQuote(txtDivision.Text) & "'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempShip, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTempShip.EOF = False Then

                mShipLocName = IIf(IsDBNull(RsTempShip.Fields("DIV_DESC").Value), "", RsTempShip.Fields("DIV_DESC").Value)
                mShipLocAddress = IIf(IsDBNull(RsTempShip.Fields("DIV_ADDRESS").Value), "", RsTempShip.Fields("DIV_ADDRESS").Value)
                mShipLocCity = IIf(IsDBNull(RsTempShip.Fields("DIV_CITY").Value), "", RsTempShip.Fields("DIV_CITY").Value)
                mShipLocCity = mShipLocCity & " " & IIf(IsDBNull(RsTempShip.Fields("DIV_PINCODE").Value), "", RsTempShip.Fields("DIV_PINCODE").Value)
                mShipLocState = IIf(IsDBNull(RsTempShip.Fields("DIV_STATE").Value), "", RsTempShip.Fields("DIV_STATE").Value)
                mShipContactNo = IIf(IsDBNull(RsTempShip.Fields("DIV_CONTACTNO").Value), "", RsTempShip.Fields("DIV_CONTACTNO").Value)
                mShipLocStateCode = GetStateCode(mShipLocState)

                mPONo = IIf(IsDBNull(RsTempShip.Fields("DIV_ALIAS").Value), "", "" & RsTempShip.Fields("DIV_ALIAS").Value)

            End If

            MainClass.AssignCRptFormulas(Report1, "mShipLocName=""" & mShipLocName & """")
            MainClass.AssignCRptFormulas(Report1, "mShipLocAddress=""" & mShipLocAddress & """")
            MainClass.AssignCRptFormulas(Report1, "mShipLocCity=""" & mShipLocCity & """")
            MainClass.AssignCRptFormulas(Report1, "mShipLocState=""" & mShipLocState & """")
            MainClass.AssignCRptFormulas(Report1, "mShipLocStateCode=""" & mShipLocStateCode & """")
            MainClass.AssignCRptFormulas(Report1, "mShipContactNo=""" & mShipContactNo & """")


            mRegdAddress = IIf(IsDBNull(RsCompany.Fields("REGD_ADDR1").Value), "", "Regd. Office : " & RsCompany.Fields("REGD_ADDR1").Value)
            mRegdAddress = mRegdAddress & IIf(IsDBNull(RsCompany.Fields("REGD_ADDR2").Value), "", " " & RsCompany.Fields("REGD_ADDR2").Value)

            mRegdCity = IIf(IsDBNull(RsCompany.Fields("REGD_CITY").Value), "", RsCompany.Fields("REGD_CITY").Value)
            mRegdCity = mRegdCity & IIf(IsDBNull(RsCompany.Fields("REGD_STATE").Value), "", ", " & RsCompany.Fields("REGD_STATE").Value)

            mRegdCity = mRegdCity & IIf(IsDBNull(RsCompany.Fields("REGD_PIN").Value), "", ", " & RsCompany.Fields("REGD_PIN").Value)

            '
            mRegdPhone = IIf(IsDBNull(RsCompany.Fields("COMPANY_MAILID").Value), "", "Email : " & RsCompany.Fields("COMPANY_MAILID").Value)

            mRegdPhone = mRegdPhone & IIf(IsDBNull(RsCompany.Fields("REGD_PHONE").Value), "", "       Mobile : " & RsCompany.Fields("REGD_PHONE").Value)

            MainClass.AssignCRptFormulas(Report1, "RegdOfficeAddress=""" & mRegdAddress & """")
            MainClass.AssignCRptFormulas(Report1, "RegdCompanyCity=""" & mRegdCity & """")
            MainClass.AssignCRptFormulas(Report1, "CompanyEMail=""" & mRegdPhone & """")

            mCompanyAdd1 = IIf(IsDBNull(RsCompany.Fields("COMPANY_ADDR").Value), "", RsCompany.Fields("COMPANY_ADDR").Value)
            mCompanyCity = IIf(IsDBNull(RsCompany.Fields("COMPANY_CITY").Value), "", RsCompany.Fields("COMPANY_CITY").Value)
            mCompanyCity = mCompanyCity & IIf(IsDBNull(RsCompany.Fields("COMPANY_STATE").Value), "", ", " & RsCompany.Fields("COMPANY_STATE").Value)
            mCompanyCity = mCompanyCity & IIf(IsDBNull(RsCompany.Fields("COMPANY_PIN").Value), "", "- " & RsCompany.Fields("COMPANY_PIN").Value)

            mCompanyPhone = IIf(IsDBNull(RsCompany.Fields("COMPANY_PHONE").Value), "", "M :" & RsCompany.Fields("COMPANY_PHONE").Value)

            MainClass.AssignCRptFormulas(Report1, "CompanyAdd1=""" & mCompanyAdd1 & """")
            MainClass.AssignCRptFormulas(Report1, "CompanyCity=""" & mCompanyCity & """")
            MainClass.AssignCRptFormulas(Report1, "CompanyPhone=""" & mCompanyPhone & """")

            MainClass.AssignCRptFormulas(Report1, "CompanyAlias=""" & IIf(IsDBNull(RsCompany.Fields("COMPANY_SHORTNAME").Value), "", "- " & RsCompany.Fields("COMPANY_SHORTNAME").Value) & """")

            mFyearFrom = VB6.Format(RsCompany.Fields("START_DATE").Value, "YY")
            mFyearTo = VB6.Format(RsCompany.Fields("END_DATE").Value, "YY")
            mPONo = mFyearFrom & "-" & mFyearTo & "/" & mPONo & "/" & VB6.Format(Mid(txtPONo.Text, 1, Len(txtPONo.Text) - 6), "000000")
            MainClass.AssignCRptFormulas(Report1, "PONO=""" & mPONo & """")
        Else
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
        End If

        SqlStr = "SELECT A.*, B.SUPP_CUST_NAME FROM FIN_SUPP_CUST_BUSINESS_MST A, FIN_SUPP_CUST_MST B" & vbCrLf _
            & " Where A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND A.COMPANY_CODE=B.COMPANY_CODE AND A.SUPP_CUST_CODE=B.SUPP_CUST_CODE" & vbCrLf _
            & " AND B.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(mShipToName) & "' AND A.LOCATION_ID='" & MainClass.AllowSingleQuote(mShipToLocation) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempShip, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTempShip.EOF = False Then
            mShipToName = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_NAME").Value), "", RsTempShip.Fields("SUPP_CUST_NAME").Value)
            mShipToAddress = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_ADDR").Value), "", RsTempShip.Fields("SUPP_CUST_ADDR").Value)
            mShipToAddress = Replace(mShipToAddress, vbCrLf, "")
            mShipToCity = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_CITY").Value), "", RsTempShip.Fields("SUPP_CUST_CITY").Value)
            mShipToCity = mShipToCity & " " & IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_PIN").Value), "", RsTempShip.Fields("SUPP_CUST_PIN").Value)
            mShipToState = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_STATE").Value), "", RsTempShip.Fields("SUPP_CUST_STATE").Value)
            mShipToStateCode = GetStateCode(mShipToState)
            mShipToGSTN = IIf(IsDBNull(RsTempShip.Fields("GST_RGN_NO").Value), "", RsTempShip.Fields("GST_RGN_NO").Value)
        End If

        If ISAnnexPrint = False Then
            MainClass.AssignCRptFormulas(Report1, "mShipToName=""" & mShipToName & """")
            MainClass.AssignCRptFormulas(Report1, "mShipToAddress=""" & mShipToAddress & """")
            MainClass.AssignCRptFormulas(Report1, "mShipToCity=""" & mShipToCity & """")
            MainClass.AssignCRptFormulas(Report1, "mShipToGSTN=""" & mShipToGSTN & """")

            MainClass.AssignCRptFormulas(Report1, "mShipToState=""" & mShipToState & """")
            '    MainClass.AssignCRptFormulas Report1, "mShipToStateCode=""" & mShipToStateCode & """"			

            '    MainClass.AssignCRptFormulas Report1, "mStateName=""" & mStateName & """"			
            '    MainClass.AssignCRptFormulas Report1, "mStateCode=""" & mStateCode & """"			
            '    MainClass.AssignCRptFormulas Report1, "mPlaceofSupply=""" & mPlaceofSupply & """"			
        End If


        Dim mDeliveryToName As String = ""
        Dim mDeliveryToAddress As String = ""
        Dim mDeliveryGSTNo As String = ""
        Dim mDeliveryCity As String = ""
        Dim mDeliveryPin As String = ""
        Dim mDeliveryState As String = ""

        SqlStr = "SELECT A.*, B.SUPP_CUST_NAME FROM FIN_SUPP_CUST_BUSINESS_MST A, FIN_SUPP_CUST_MST B" & vbCrLf _
            & " Where A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND A.COMPANY_CODE=B.COMPANY_CODE AND A.SUPP_CUST_CODE=B.SUPP_CUST_CODE" & vbCrLf _
            & " AND B.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtDeliveryTo.Text) & "' AND A.LOCATION_ID='" & MainClass.AllowSingleQuote(TxtDeliveryToLoc.Text) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempShip, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTempShip.EOF = False Then
            mDeliveryToName = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_NAME").Value), "", RsTempShip.Fields("SUPP_CUST_NAME").Value)
            mDeliveryToAddress = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_ADDR").Value), "", RsTempShip.Fields("SUPP_CUST_ADDR").Value)
            mDeliveryToAddress = Replace(mDeliveryToAddress, vbCrLf, "")

            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 116 Then
                mDeliveryCity = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_CITY").Value), "", RsTempShip.Fields("SUPP_CUST_CITY").Value)
                mDeliveryPin = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_PIN").Value), "", RsTempShip.Fields("SUPP_CUST_PIN").Value)
                mDeliveryState = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_STATE").Value), "", RsTempShip.Fields("SUPP_CUST_STATE").Value)
                'mShipToStateCode = GetStateCode(mShipToState)
                'mShipToGSTN = IIf(IsDBNull(RsTempShip.Fields("GST_RGN_NO").Value), "", RsTempShip.Fields("GST_RGN_NO").Value)
                mDeliveryGSTNo = IIf(IsDBNull(RsTempShip.Fields("GST_RGN_NO").Value), "", RsTempShip.Fields("GST_RGN_NO").Value)
            Else
                mDeliveryToAddress = mDeliveryToAddress & ", " & IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_CITY").Value), "", RsTempShip.Fields("SUPP_CUST_CITY").Value)
                mDeliveryToAddress = mDeliveryToAddress & " " & IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_PIN").Value), "", RsTempShip.Fields("SUPP_CUST_PIN").Value)
                mDeliveryToAddress = mDeliveryToAddress & " " & IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_STATE").Value), "", RsTempShip.Fields("SUPP_CUST_STATE").Value)
                'mShipToStateCode = GetStateCode(mShipToState)
                'mShipToGSTN = IIf(IsDBNull(RsTempShip.Fields("GST_RGN_NO").Value), "", RsTempShip.Fields("GST_RGN_NO").Value)
                mDeliveryGSTNo = IIf(IsDBNull(RsTempShip.Fields("GST_RGN_NO").Value), "", RsTempShip.Fields("GST_RGN_NO").Value)
                If mDeliveryGSTNo <> "" Then
                    mDeliveryToAddress = mDeliveryToAddress & " GSTIN : " & mDeliveryGSTNo
                End If
            End If


        End If

        If ISAnnexPrint = False Then
            MainClass.AssignCRptFormulas(Report1, "mDeliveryToName=""" & mDeliveryToName & """")
            MainClass.AssignCRptFormulas(Report1, "mDeliveryToAddress=""" & mDeliveryToAddress & """")
            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 116 Then
                Dim mFYear As String
                Dim mPreparedBy As String
                Dim mCheckedBy As String

                mFYear = Mid(txtPONo.Text, Len(txtPONo.Text) - 3, 2) & "-" & Val(Mid(txtPONo.Text, Len(txtPONo.Text) - 3, 2)) + 1
                MainClass.AssignCRptFormulas(Report1, "mDeliveryToCity=""" & mDeliveryCity & """")
                MainClass.AssignCRptFormulas(Report1, "mDeliveryToPin=""" & mDeliveryPin & """")
                MainClass.AssignCRptFormulas(Report1, "mDeliveryToState=""" & mDeliveryState & """")
                MainClass.AssignCRptFormulas(Report1, "mDeliveryToGSTNo=""" & mDeliveryGSTNo & """")
                MainClass.AssignCRptFormulas(Report1, "mFYear=""" & mFYear & """")

                If MainClass.ValidateWithMasterTable(lblAddUser.Text, "USER_ID", "EMP_NAME", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mPreparedBy = MasterNo
                Else
                    mPreparedBy = lblAddUser.Text
                End If

                If MainClass.ValidateWithMasterTable(lblModUser.Text, "USER_ID", "EMP_NAME", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mCheckedBy = MasterNo
                Else
                    mCheckedBy = lblModUser.Text
                End If

                MainClass.AssignCRptFormulas(Report1, "PreparedBy=""" & mPreparedBy & """")
                MainClass.AssignCRptFormulas(Report1, "CheckedBy=""" & mCheckedBy & """")

            End If


        End If

        If RsCompany.Fields("PO_PREPRINT").Value = "N" And ISAnnexPrint = False Then
            MainClass.AssignCRptFormulas(Report1, "TINNo=""" & IIf(IsDBNull(RsCompany.Fields("TINNO").Value), "", RsCompany.Fields("TINNO").Value) & """")
            MainClass.AssignCRptFormulas(Report1, "ExciseRegnNo=""" & IIf(IsDBNull(RsCompany.Fields("CENT_EXC_RGN_NO").Value), "", RsCompany.Fields("CENT_EXC_RGN_NO").Value) & """")
            MainClass.AssignCRptFormulas(Report1, "ECCNo=""" & IIf(IsDBNull(RsCompany.Fields("ECC_NO").Value), "", RsCompany.Fields("ECC_NO").Value) & """")
            MainClass.AssignCRptFormulas(Report1, "Division=""" & IIf(IsDBNull(RsCompany.Fields("EXCISE_DIV").Value), "", RsCompany.Fields("EXCISE_DIV").Value) & """")
            MainClass.AssignCRptFormulas(Report1, "Range=""" & IIf(IsDBNull(RsCompany.Fields("EXCISE_RANGE").Value), "", RsCompany.Fields("EXCISE_RANGE").Value) & """")
            MainClass.AssignCRptFormulas(Report1, "PANNo=""" & IIf(IsDBNull(RsCompany.Fields("PAN_NO").Value), "", RsCompany.Fields("PAN_NO").Value) & """")
        End If


        MainClass.AssignCRptFormulas(Report1, "COMPANYGSTIN=""" & IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value) & """")
        MainClass.AssignCRptFormulas(Report1, "COMPANYCIN=""" & IIf(IsDBNull(RsCompany.Fields("CIN_NO").Value), "", RsCompany.Fields("CIN_NO").Value) & """")

        If (VB.Left(lblBookType.Text, 1) = "W" Or VB.Left(lblBookType.Text, 1) = "J") And ISAnnexPrint = False Then
            mService = Trim(txtServProvided.Text)
            MainClass.AssignCRptFormulas(Report1, "SERVPROD=""" & mService & """")
        End If

        If ISAnnexPrint = False Then
            mItemValue = 0
            With SprdMain
                For CntRow = 1 To .MaxRows - 1
                    .Row = CntRow
                    If VB.Left(lblBookType.Text, 1) = "W" Or VB.Left(lblBookType.Text, 1) = "R" Then
                        .Col = ColWoDesc
                    Else
                        .Col = ColItemCode
                    End If

                    If Trim(.Text) <> "" Then
                        .Col = ColPrintStatus
                        If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                            .Col = ColGross
                            mItemValue = mItemValue + Val(.Text)
                        End If
                    End If
                Next
            End With

            '        mItemValue = mItemValue + Val(lblCGSTValue.text) + Val(lblSGSTValue.text) + Val(lblIGSTValue.text) + Val(lblTotOtherExp.text)			
        End If

        '    SprdMain.Row = 0			
        '    SprdMain.Col = ColItemRate			
        '    mCurr = SprdMain.Text			
        '    MainClass.AssignCRptFormulas Report1, "PriceTitle=""" & mCurr & """"			



        Report1.ReportFileName = PubReportFolderPath & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        If pPrintSubReport = True Then
            Dim mGSTPer As Double

            If lblRMPO.Text = "R" Then
                SprdMain.Row = 1

                mItemValue = Val(txtRMQty.Text) * Val(txtRMRate.Text)

                SprdMain.Col = ColCGSTPer
                mItemValue = mItemValue + (Val(SprdMain.Text) * Val(txtRMQty.Text) * Val(txtRMRate.Text) * 0.01)

                SprdMain.Col = ColSGSTPer
                mItemValue = mItemValue + (Val(SprdMain.Text) * Val(txtRMQty.Text) * Val(txtRMRate.Text) * 0.01)

                SprdMain.Col = ColIGSTPer
                mItemValue = mItemValue + (Val(SprdMain.Text) * Val(txtRMQty.Text) * Val(txtRMRate.Text) * 0.01)

            Else
                With SprdExp
                    For CntRow = 1 To .MaxRows - 1
                        .Row = CntRow
                        .Col = ColExpAmt
                        mItemValue = mItemValue + Val(.Text)

                    Next
                End With
            End If

            Dim pSqlStr As String
            Dim RsTemp As ADODB.Recordset = Nothing
            Dim mMajorCurr As String = ""
            Dim mMinorCurr As String = ""
            Dim mCurrency As String = ""

            If MainClass.ValidateWithMasterTable((txtCode.Text), "SUPP_CUST_CODE", "CURRENCYNAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mCurrency = MasterNo
            End If

            pSqlStr = " SELECT CURR_DESC, MINOR_CURR " & vbCrLf _
                & " FROM FIN_CURRENCY_MST" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND CURR_DESC='" & MainClass.AllowSingleQuote(mCurrency) & "'"

            MainClass.UOpenRecordSet(pSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                mMajorCurr = IIf(IsDBNull(RsTemp.Fields("CURR_DESC").Value), "", RsTemp.Fields("CURR_DESC").Value)
                mMinorCurr = IIf(IsDBNull(RsTemp.Fields("MINOR_CURR").Value), "", RsTemp.Fields("MINOR_CURR").Value)
            End If

            'mAmountInword = MainClass.RupeesConversion(CDbl(mItemValue))
            mAmountInword = MainClass.RupeesIntoForigenCurr(CDbl(mItemValue), mMajorCurr, mMinorCurr)


            MainClass.AssignCRptFormulas(Report1, "AmountInWord=""" & mAmountInword & """")
            MainClass.AssignCRptFormulas(Report1, "NetAmount=""" & VB6.Format(mItemValue, "0.00") & """")


            SqlStrSub = " SELECT MKEY, SUBROWNO, EXPPERCENT, AMOUNT, COMPANY_CODE, NAME" & vbCrLf _
                & " FROM PUR_PURCHASE_EXP, FIN_INTERFACE_MST " & vbCrLf _
                & " WHERE PUR_PURCHASE_EXP.EXPCODE = FIN_INTERFACE_MST.CODE" & vbCrLf _
                & " AND PUR_PURCHASE_EXP.MKEY='" & MainClass.AllowSingleQuote(lblMkey.Text) & "'" & vbCrLf _
                & " AND FIN_INTERFACE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

            SqlStrSub = SqlStrSub & vbCrLf & " AND GST_ENABLED='Y'"

            '        If PubGSTApplicable = True Then			
            '            SqlStrSub = SqlStrSub & vbCrLf & " AND GST_ENABLED='Y'"			
            '        Else			
            '            SqlStrSub = SqlStrSub & vbCrLf & " AND GST_ENABLED='N'"			
            '        End If			

            SqlStrSub = SqlStrSub & vbCrLf & " ORDER BY SUBROWNO"

            Report1.SubreportToChange = Report1.GetNthSubreportName(0)
            Report1.Connect = STRRptConn
            Report1.SQLQuery = SqlStrSub

            Report1.SubreportToChange = ""
        End If

        'Dim prt As Printer			
        'If PubUniversalPrinter = "Y" And mMode = Crystal.DestinationConstants.crptToPrinter Then			
        '    For Each prt In Printers			
        '        If UCase(prt.DeviceName) = UCase("Universal Printer") Then			
        '            Printer = prt			

        '            Report1.PrinterName = prt.DeviceName			
        '            Report1.PrinterDriver = prt.DriverName			
        '            Report1.PrinterPort = prt.Port			
        '            Exit For			
        '        End If			
        '    Next prt			
        'End If			

        Report1.Action = 1
        Report1.Reset()
        'Report1.Dispose
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call CalcTots()
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        If Update1() = True Then
            ADDMode = False
            MODIFYMode = False
            txtAmendNo_Validating(txtAmendNo, New System.ComponentModel.CancelEventArgs(False)) '' txtPONO_Validate False			
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
    Private Function Update1() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mPONo As Double
        Dim mPurType As String
        Dim mOrderType As String
        Dim mStatus As String
        Dim mActivate As String
        Dim mAmendNo As Integer
        Dim mRecdAcct As String
        Dim mModvatable As String
        Dim mSTRefundable As String
        Dim mCapital As String
        Dim mSACCode As String
        Dim mOwnerCode As String = ""
        Dim mPostingDetail As Integer
        Dim mGSTApplicable As String
        Dim mShippedToSame As String
        Dim mShippedToCode As String = ""
        Dim mReverseCharge As String
        Dim mDevelopment As String
        Dim mApprovedWO_TC As String
        Dim mTCAvailable As String
        Dim mTPRAvailable As String
        Dim mTCFilename As String
        Dim mTRFileName As String
        Dim mDeliveryToCode As String = ""
        Dim mShipToLoc As String = ""

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()


        If MainClass.ValidateWithMasterTable((txtCode.Text), "SUPP_CUST_CODE", "SUPP_CUST_CODE", "FIN_SUPP_CUST_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            If UpdateSuppMst((txtCode.Text)) = False Then GoTo ErrPart
        End If

        If lblBookType.Text = "LC" Then
            If MainClass.ValidateWithMasterTable((txtOwner.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mOwnerCode = MasterNo
            End If
        Else
            mOwnerCode = ""
        End If

        If lblBookType.Text = "LC" Then
            If optPostingDetails(0).Checked = True Then
                mPostingDetail = 1
            ElseIf optPostingDetails(1).Checked = True Then
                mPostingDetail = 2
            ElseIf optPostingDetails(2).Checked = True Then
                mPostingDetail = 3
            End If
        Else
            mPostingDetail = 0
        End If

        mPurType = VB.Left(lblBookType.Text, 1)
        mOrderType = VB.Right(lblBookType.Text, 1)
        mStatus = IIf(chkStatus.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mActivate = IIf(ChkActivate.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mRecdAcct = IIf(chkRecdAcct.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")


        mApprovedWO_TC = IIf(chkApprovedWO_TC.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mTCAvailable = IIf(chkTCAvailable.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mTPRAvailable = IIf(chkTPRAvailable.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mTCFilename = IIf(mTCAvailable = "Y", ExtractFileName((txtTCPath.Text)), "")
        mTRFileName = IIf(mTPRAvailable = "Y", ExtractFileName((txtTPRPath.Text)), "")


        mModvatable = "N"
        mSTRefundable = "N"
        mGSTApplicable = VB.Left(cboGSTStatus.Text, 1)

        mCapital = IIf(chkCapital.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mDevelopment = IIf(chkDevelopment.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mShippedToSame = IIf(chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        If mShippedToSame = "Y" Then
            mShippedToCode = MainClass.AllowSingleQuote(txtCode.Text)
            mShipToLoc = txtBillTo.Text
        Else
            If MainClass.ValidateWithMasterTable(txtShippedTo.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mShippedToCode = MasterNo
            End If
            mShipToLoc = TxtShipTo.Text
        End If

        If Trim(txtDeliveryTo.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtDeliveryTo.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDeliveryToCode = MasterNo
            End If
        End If

        SqlStr = ""
        mPONo = Val(txtPONo.Text)
        If Val(txtPONo.Text) = 0 Then
            mPONo = AutoGenPONoSeq()
        End If
        txtPONo.Text = CStr(mPONo)

        mAmendNo = Val(txtAmendNo.Text)

        txtAmendNo.Text = CStr(Val(CStr(mAmendNo)))

        If MainClass.ValidateWithMasterTable(Trim(txtServProvided.Text), "HSN_DESC", "HSN_CODE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
            mSACCode = CStr(Val(MasterNo))
        Else
            mSACCode = ""
        End If


        If ADDMode = True Then
            lblMkey.Text = mPONo & VB6.Format(mAmendNo, "000")
            SqlStr = " INSERT INTO PUR_PURCHASE_HDR ( " & vbCrLf _
                & "  MKEY, AUTO_KEY_PO,  COMPANY_CODE," & vbCrLf _
                & "  PUR_TYPE,  ORDER_TYPE," & vbCrLf _
                & "  PUR_ORD_DATE, SUPP_CUST_CODE," & vbCrLf _
                & "  AMEND_NO, AMEND_DATE," & vbCrLf _
                & "  PAYDAYS, EXCHANGERATE, REMARKS," & vbCrLf _
                & "  DELIVERY, EXCISE_OTHERS," & vbCrLf _
                & "  PAYMENT_CODE, MODE_DESPATCH," & vbCrLf _
                & "  INSPECTION, PACKING_FORWARDING," & vbCrLf _
                & "  INSURANCE, OTHERS_COND1," & vbCrLf _
                & "  OTHERS_COND2, PO_STATUS," & vbCrLf _
                & "  SALETAX_PER, EXCISE_PER," & vbCrLf _
                & "  AMEND_WEF_DATE, PO_CLOSED,PREV_PO_NO, " & vbCrLf _
                & "  RECD_AC_FLAG, RECD_PO_DATE, " & vbCrLf _
                & "  ADDUSER, ADDDATE, MODUSER, MODDATE," & vbCrLf _
                & " UPDATE_FROM,DIV_CODE,ISMODVATABLE, " & vbCrLf _
                & " ISSTREFUNDABLE,ISGSTAPPLICABLE, ISCAPITAL, " & vbCrLf _
                & " SAC_CODE, NAV_PO_NO, OWNER_CODE, ACCTPOST_DETAIL, " & vbCrLf _
                & " TOTALGSTVALUE, OTHEREXPVALUE, SHIPPED_TO_SAMEPARTY, " & vbCrLf _
                & " SHIPPED_TO_PARTY_CODE, ISGSTENABLE_PO, " & vbCrLf _
                & " TOTCGST_AMOUNT, TOTSGST_AMOUNT, TOTIGST_AMOUNT, IS_DEVELOPMENT," & vbCrLf _
                & " TC_AVAILABLE, TC_FILE_PATH, TPRI_AVAILABLE, TPRI_FILE_PATH, APPROVAL_WO_TC,BILL_TO_LOC_ID,SHIP_TO_LOC_ID,ISRM_PO," & vbCrLf _
                & " RM_DESC, RM_QTY, RM_RATE, DELIVERY_TO,DELIVERY_TO_LOC_ID) "

            SqlStr = SqlStr & vbCrLf & " VALUES ( " & vbCrLf _
                & " " & Val(lblMkey.Text) & "," & mPONo & ", " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf _
                & " '" & mPurType & "', '" & mOrderType & "', " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtPODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtCode.Text) & "', " & vbCrLf _
                & " " & Val(CStr(mAmendNo)) & ", " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtAmendDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " " & Val(txtPaymentDays.Text) & ", " & Val(TxtExchangeRate.Text) & ", "

            SqlStr = SqlStr & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtDelivery.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtExcise.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtPayment.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtDespMode.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtInspection.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtPacking.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtInsurance.Text) & "', " & vbCrLf _
                & " '', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtOthCond2.Text) & "', " & vbCrLf _
                & " '" & mStatus & "'," & Val(lblSTPercentage.Text) & "," & Val(lblEDPercentage.Text) & "," & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & mActivate & "','" & txtPrevPONo.Text & "'," & vbCrLf _
                & " '" & mRecdAcct & "', TO_DATE('" & VB6.Format(txtRecdDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')," & vbCrLf _
                & " '','','N'," & Val(txtDivision.Text) & ",'" & mModvatable & "'," & vbCrLf _
                & " '" & mSTRefundable & "','" & mGSTApplicable & "', '" & mCapital & "','" & mSACCode & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtOldERPNo.Text) & "','" & MainClass.AllowSingleQuote(mOwnerCode) & "', " & mPostingDetail & "," & vbCrLf _
                & " " & Val(lblCGSTValue.Text) + Val(lblSGSTValue.Text) + Val(lblIGSTValue.Text) & "," & Val(lblTotOtherExp.Text) & "," & vbCrLf _
                & " '" & mShippedToSame & "','" & MainClass.AllowSingleQuote(mShippedToCode) & "','Y', " & vbCrLf _
                & " " & Val(lblCGSTValue.Text) & "," & Val(lblSGSTValue.Text) & "," & Val(lblIGSTValue.Text) & ", " & vbCrLf _
                & " '" & mDevelopment & "','" & mTCAvailable & "','" & mTCFilename & "','" & mTPRAvailable & "','" & mTRFileName & "', '" & mApprovedWO_TC & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtBillTo.Text) & "','" & MainClass.AllowSingleQuote(mShipToLoc) & "','" & IIf(lblRMPO.Text = "R", "Y", "N") & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtRMDesc.Text) & "'," & Val(txtRMQty.Text) & "," & Val(txtRMRate.Text) & ",'" & MainClass.AllowSingleQuote(mDeliveryToCode) & "','" & MainClass.AllowSingleQuote(TxtDeliveryToLoc.Text) & "')" '
        End If


        If MODIFYMode = True Then
            SqlStr = " UPDATE PUR_PURCHASE_HDR SET " & vbCrLf _
                & " AUTO_KEY_PO=" & mPONo & ", ISRM_PO= '" & IIf(lblRMPO.Text = "R", "Y", "N") & "'," & vbCrLf _
                & " PUR_TYPE='" & mPurType & "', ORDER_TYPE='" & mOrderType & "', " & vbCrLf _
                & " PUR_ORD_DATE=TO_DATE('" & VB6.Format(txtPODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "', " & vbCrLf _
                & " DELIVERY_TO = '" & MainClass.AllowSingleQuote(mDeliveryToCode) & "', DELIVERY_TO_LOC_ID='" & MainClass.AllowSingleQuote(TxtDeliveryToLoc.Text) & "'," & vbCrLf _
                & " OWNER_CODE='" & MainClass.AllowSingleQuote(mOwnerCode) & "', " & vbCrLf _
                & " AMEND_NO=" & Val(CStr(mAmendNo)) & ", " & vbCrLf _
                & " AMEND_DATE=TO_DATE('" & VB6.Format(txtAmendDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " PAYDAYS=" & Val(txtPaymentDays.Text) & ", UPDATE_FROM='N', ACCTPOST_DETAIL=" & mPostingDetail & "," & vbCrLf _
                & " TC_AVAILABLE = '" & mTCAvailable & "',  " & vbCrLf & " TC_FILE_PATH = '" & mTCFilename & "',  " & vbCrLf _
                & " TPRI_AVAILABLE = '" & mTPRAvailable & "',  " & vbCrLf _
                & " BILL_TO_LOC_ID='" & MainClass.AllowSingleQuote((txtBillTo.Text)) & "', " & vbCrLf _
                & " SHIP_TO_LOC_ID='" & MainClass.AllowSingleQuote((mShipToLoc)) & "', " & vbCrLf _
                & " TPRI_FILE_PATH = '" & mTRFileName & "', APPROVAL_WO_TC= '" & mApprovedWO_TC & "'," & vbCrLf _
                & " RM_DESC='" & MainClass.AllowSingleQuote(txtRMDesc.Text) & "', RM_QTY=" & Val(txtRMQty.Text) & ", RM_RATE=" & Val(txtRMRate.Text) & ","

            SqlStr = SqlStr & vbCrLf _
                & " EXCHANGERATE= " & Val(TxtExchangeRate.Text) & ", " & vbCrLf _
                & " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                & " DELIVERY='" & MainClass.AllowSingleQuote(txtDelivery.Text) & "', " & vbCrLf _
                & " EXCISE_OTHERS='" & MainClass.AllowSingleQuote(txtExcise.Text) & "', " & vbCrLf _
                & " PAYMENT_CODE='" & MainClass.AllowSingleQuote(txtPayment.Text) & "', " & vbCrLf _
                & " MODE_DESPATCH='" & MainClass.AllowSingleQuote(txtDespMode.Text) & "', " & vbCrLf _
                & " INSPECTION='" & MainClass.AllowSingleQuote(txtInspection.Text) & "', " & vbCrLf _
                & " PACKING_FORWARDING='" & MainClass.AllowSingleQuote(txtPacking.Text) & "', " & vbCrLf _
                & " INSURANCE='" & MainClass.AllowSingleQuote(txtInsurance.Text) & "', " & vbCrLf _
                & " OTHERS_COND1='', " & vbCrLf & " OTHERS_COND2='" & MainClass.AllowSingleQuote(txtOthCond2.Text) & "', " & vbCrLf _
                & " SALETAX_PER=" & Val(lblSTPercentage.Text) & ", EXCISE_PER=" & Val(lblEDPercentage.Text) & ", " & vbCrLf _
                & " PO_STATUS='" & mStatus & "', PO_CLOSED='" & mActivate & "', " & vbCrLf _
                & " PREV_PO_NO='" & txtPrevPONo.Text & "',DIV_CODE=" & Val(txtDivision.Text) & "," & vbCrLf _
                & " ISMODVATABLE='" & mModvatable & "',ISSTREFUNDABLE='" & mSTRefundable & "', ISCAPITAL='" & mCapital & "'," & vbCrLf _
                & " ISGSTAPPLICABLE='" & mGSTApplicable & "', "

            SqlStr = SqlStr & vbCrLf _
                & " TOTALGSTVALUE=" & Val(lblCGSTValue.Text) + Val(lblSGSTValue.Text) + Val(lblIGSTValue.Text) & ", " & vbCrLf _
                & " TOTCGST_AMOUNT=" & Val(lblCGSTValue.Text) & ", " & vbCrLf _
                & " TOTSGST_AMOUNT=" & Val(lblSGSTValue.Text) & ", " & vbCrLf _
                & " TOTIGST_AMOUNT=" & Val(lblIGSTValue.Text) & ", " & vbCrLf _
                & " OTHEREXPVALUE=" & Val(lblTotOtherExp.Text) & ", " & vbCrLf _
                & " SHIPPED_TO_SAMEPARTY='" & mShippedToSame & "',SHIPPED_TO_PARTY_CODE='" & MainClass.AllowSingleQuote(mShippedToCode) & "', " & vbCrLf _
                & " AMEND_WEF_DATE=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " RECD_AC_FLAG='" & mRecdAcct & "', RECD_PO_DATE=TO_DATE('" & VB6.Format(txtRecdDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " SAC_CODE='" & mSACCode & "'," & vbCrLf _
                & " NAV_PO_NO='" & MainClass.AllowSingleQuote(txtOldERPNo.Text) & "', IS_DEVELOPMENT='" & mDevelopment & "'," & vbCrLf _
                & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND MKEY =" & Val(lblMkey.Text) & ""
        End If


        PubDBCn.Execute(SqlStr)
        If UpdateDetail1() = False Then GoTo ErrPart
        '    If ADDMode = True Then			
        'If UpdateBlobData((lblmKey.Text)) = False Then GoTo ErrPart
        '    End If			
        If UpdateIndent() = False Then GoTo ErrPart
        Update1 = True
        PubDBCn.CommitTrans()
        txtPONo.Text = CStr(mPONo)
        Exit Function
ErrPart:
        '    Resume			
        Update1 = False
        MsgInformation(Err.Description)
        PubDBCn.RollbackTrans()
        RsPOMain.Requery()
        RsPODetail.Requery()
        RsPOExp.Requery()
        RsPOAnnex.Requery()
        MsgBox(Err.Description)
        ''Resume			
    End Function
    Private Function UpdateBlobData(ByRef pMkey As String) As Boolean
        On Error GoTo UpdateBlobData
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing


        Dim mTCFilename As String
        Dim mTCExt As String

        Dim mTPRFilename As String
        Dim mTPRExt As String

        Dim tmpTCFileName As String = ""
        Dim tmpTPIFileName As String = ""

        '    CopyBlobFileintoTempFile			

        PubDBCnBlob.Errors.Clear()
        PubDBCnBlob.BeginTrans()

        SqlStr = "Delete From  PUR_PURCHASE_TC_TRN WHERE MKEY=" & Val(lblMkey.Text) & ""
        PubDBCnBlob.Execute(SqlStr)

        If chkTCAvailable.CheckState = System.Windows.Forms.CheckState.Unchecked And chkTPRAvailable.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            UpdateBlobData = True
            Exit Function
        End If

        mTCFilename = ExtractFileName((txtTCPath.Text))
        mTCExt = GetExtensionName((txtTCPath.Text))

        mTPRFilename = ExtractFileName((txtTPRPath.Text))
        mTPRExt = GetExtensionName((txtTPRPath.Text))

        SqlStr = " SELECT MKEY, TC_DOC_DESC, TC_DOC_EXT, TPR_DOC_DESC, TPR_DOC_EXT " & vbCrLf & " FROM PUR_PURCHASE_TC_TRN " & vbCrLf & " WHERE MKEY = '" & pMkey & "'"


        RsTemp = New ADODB.Recordset
        With RsTemp
            .CursorType = ADODB.CursorTypeEnum.adOpenKeyset
            .LockType = ADODB.LockTypeEnum.adLockOptimistic
            .Open(SqlStr, PubDBCnBlob)
            '      .Delete adAffectAll			
            .AddNew(New Object() {"MKEY", "TC_DOC_DESC", "TC_DOC_EXT", "TPR_DOC_DESC", "TPR_DOC_EXT"}, New Object() {pMkey, mTCFilename, mTCExt, mTPRFilename, mTPRExt})
            .Close()
        End With

        If mTCFilename <> "" Then
            '        mFileName = VB6.Format(GetServerDate, "DDMMYYYY") & VB6.Format(GetServerTimeWithSecond, "HHMMSS") & "_TC." & RsTemp("TC_DOC_EXT").Value			
            tmpTCFileName = PubDomainUserDesktopPath & "\" & VB6.Format(GetServerDate, "DDMMYYYY") & VB6.Format(GetServerTimeWithSecond, "HHMMSS") & "_TC." & mTCExt ''& mTCFilename ''& "." & mExt   ''mLocalPath  lblPhotoFileName.text  '' GetTempPath & ExtractFileName(fullFileName)			
            '        tmpTCFileName = App.path & "\Temp\" & VB6.Format(GetServerDate, "DDMMYYYY") & VB6.Format(GetServerTimeWithSecond, "HHMMSS") & "_TC." & mTCExt			

            '        if the tmp file exists, delete it			
            If Len(Dir(tmpTCFileName)) > 0 Then
                Kill(tmpTCFileName)
            End If
            Call FileCopy(txtTCPath.Text, tmpTCFileName)
        End If

        If mTPRFilename <> "" Then
            tmpTPIFileName = PubDomainUserDesktopPath & "\" & VB6.Format(GetServerDate, "DDMMYYYY") & VB6.Format(GetServerTimeWithSecond, "HHMMSS") & "_TPR." & mTPRExt ''& mTCFilename ''& "." & mExt   ''mLocalPath  lblPhotoFileName.text  '' GetTempPath & ExtractFileName(fullFileName)			
            '        tmpTCFileName = App.path & "\Temp\" & VB6.Format(GetServerDate, "DDMMYYYY") & VB6.Format(GetServerTimeWithSecond, "HHMMSS") & "_TPR." & mTPRExt			
            '        tmpTPIFileName = PubDomainUserDesktopPath & "\" & mTPRFilename ''& "." & mExt   ''lblPhotoFileName.text  '' GetTempPath & ExtractFileName(fullFileName)			
            'if the tmp file exists, delete it			
            If Len(Dir(tmpTPIFileName)) > 0 Then
                Kill(tmpTPIFileName)
            End If
            Call FileCopy(txtTPRPath.Text, tmpTPIFileName)
        End If




        'Now that our record is inserted, update it with the file from disk			
        RsTemp = Nothing
        RsTemp = New ADODB.Recordset
        Dim st As ADODB.Stream
        RsTemp.Open(" SELECT TC_BLOB_DATA, TPR_BLOB_DATA" & vbCrLf & " FROM PUR_PURCHASE_TC_TRN WHERE MKEY = '" & pMkey & "'", PubDBCnBlob, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

        st = New ADODB.Stream
        st.Type = ADODB.StreamTypeEnum.adTypeBinary
        st.Open()

        If tmpTCFileName <> "" Then
            st.LoadFromFile((tmpTCFileName))
            RsTemp.Fields("TC_BLOB_DATA").Value = st.Read
        End If

        If tmpTPIFileName <> "" Then
            st.LoadFromFile((tmpTPIFileName))
            RsTemp.Fields("TPR_BLOB_DATA").Value = st.Read
        End If

        RsTemp.Update()

        'Now delete the temp file we created			

        If tmpTCFileName <> "" Then
            Kill((tmpTCFileName))
        End If

        If tmpTPIFileName <> "" Then
            Kill((tmpTPIFileName))
        End If

        PubDBCnBlob.CommitTrans()
        UpdateBlobData = True

        Exit Function
UpdateBlobData:
        UpdateBlobData = False
        PubDBCnBlob.RollbackTrans()
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume			
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

    Private Function UpdateSuppCustDet(ByRef xSuppCustCode As String, ByRef xItemCode As String, ByRef xRate As Double, ByRef xDisc As Double, ByRef xType As String) As Boolean

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
                SqlStr = " INSERT INTO FIN_SUPP_CUST_DET ( " & vbCrLf & " COMPANY_CODE , SUPP_CUST_CODE, " & vbCrLf & " ITEM_CODE, ITEM_RATE, " & vbCrLf & " DISC_PER, TRN_TYPE) "
                SqlStr = SqlStr & vbCrLf & " VALUES ( " & vbCrLf & " '" & RsCompany.Fields("COMPANY_CODE").Value & "','" & MainClass.AllowSingleQuote(xSuppCustCode) & "', " & vbCrLf & " '" & xItemCode & "'," & xRate & ", " & vbCrLf & " " & xDisc & ",'" & xType & "') "

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


    Private Function UpdateIndent() As Boolean

        On Error GoTo UpdateIndentErr1
        Dim RsTempIndent As ADODB.Recordset = Nothing
        Dim ii As Integer
        Dim SqlStr As String = ""
        Dim mPoSerialNo As Integer
        Dim mItemCode As String
        Dim mKey As String
        Dim mSerialNo As Double
        Dim mIndentQty As Double

        If DeleteIndent(PubDBCn, (lblMkey.Text)) = False Then GoTo UpdateIndentErr1

        SqlStr = "SELECT * " & vbCrLf _
            & "FROM TEMP_PUR_POCONS_IND_TRN " & vbCrLf _
            & "WHERE UserId='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf _
            & "ORDER BY AUTO_KEY_INDENT,ITEM_CODE,SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempIndent, ADODB.LockTypeEnum.adLockReadOnly)

        With RsTempIndent
            If .EOF = False Then
                Do While Not .EOF
                    SqlStr = ""
                    ii = ii + 1
                    mItemCode = IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                    mKey = IIf(IsDBNull(.Fields("AUTO_KEY_INDENT").Value), "", .Fields("AUTO_KEY_INDENT").Value)
                    mSerialNo = IIf(IsDBNull(.Fields("SERIAL_NO_INDENT").Value), 0, .Fields("SERIAL_NO_INDENT").Value)
                    mIndentQty = IIf(IsDBNull(.Fields("INDENT_QTY").Value), 0, .Fields("INDENT_QTY").Value)

                    If CheckItemInGrid(mItemCode, mPoSerialNo) = True Then
                        If UpdateIndentIntoTable(Val(lblMkey.Text), Val(txtPONo.Text), mPoSerialNo, mKey, mSerialNo, mIndentQty, Trim(mItemCode)) = False Then GoTo UpdateIndentErr1
                    End If
                    .MoveNext()
                Loop
            End If
        End With
        UpdateIndent = True
        Exit Function
UpdateIndentErr1:
        UpdateIndent = False
        If Err.Description <> "" Then
            MsgBox(Err.Description)
        End If
    End Function
    Public Function UpdateIndentIntoTable(ByRef pMkey As Double, ByRef pPONO As Double, ByRef pPOSerialNo As Integer, ByRef pIndentNo As Double, ByRef pSerialNo As Integer, ByRef pIndentQty As Double, ByRef pItemCode As String) As Boolean
        On Error GoTo UpDateIndentErr
        Dim SqlStr As String = ""
        SqlStr = ""

        SqlStr = "INSERT INTO PUR_POCONS_IND_TRN (" & vbCrLf _
            & " MKEY,AUTO_KEY_PO, SERIAL_NO, " & vbCrLf _
            & " AUTO_KEY_INDENT, SERIAL_NO_INDENT, " & vbCrLf _
            & " INDENT_QTY, ITEM_CODE) VALUES ( " & vbCrLf _
            & " " & Val(CStr(pMkey)) & "," & Val(CStr(pPONO)) & ", " & Val(CStr(pPOSerialNo)) & "," & vbCrLf _
            & " " & Val(CStr(pIndentNo)) & ", " & Val(CStr(pSerialNo)) & "," & vbCrLf _
            & " " & Val(CStr(pIndentQty)) & ", '" & MainClass.AllowSingleQuote(pItemCode) & "')"

        PubDBCn.Execute(SqlStr)

        UpdateIndentIntoTable = True
        Exit Function
UpDateIndentErr:
        UpdateIndentIntoTable = False
        If Err.Number = -2147217900 Then
            MsgBox("Indent No Can Not Be Duplicate", MsgBoxStyle.Information)
            Exit Function
        End If
        MsgBox(Err.Description)
    End Function
    Private Function CheckItemInGrid(ByRef mItemCode As String, ByRef mPoSerialNo As Integer) As Boolean
        On Error GoTo CheckERR
        Dim I As Integer

        CheckItemInGrid = False

        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I
                .Col = ColItemCode
                If UCase(Trim(mItemCode)) = UCase(Trim(.Text)) Then
                    .Row = I
                    .Col = ColQty
                    If Val(.Text) > 0 Then
                        mPoSerialNo = I
                        CheckItemInGrid = True
                        Exit Function
                    End If
                End If
            Next
        End With
        Exit Function
CheckERR:
        CheckItemInGrid = False
    End Function

    Public Function DeleteIndent(ByRef pDBCn As ADODB.Connection, ByRef pMkey As String) As Boolean
        Dim SqlStr As String = ""
        On Error GoTo DeleteIndentErr
        SqlStr = ""
        SqlStr = "DELETE FROM PUR_POCONS_IND_TRN  " & vbCrLf & " WHERE AUTO_KEY_PO=" & Val(txtPONo.Text) & " "
        pDBCn.Execute(SqlStr)
        DeleteIndent = True
        Exit Function
DeleteIndentErr:
        MsgInformation(Err.Description)
        DeleteIndent = False
    End Function
    Private Sub CalcTots()
        On Error GoTo ERR1
        Dim mDiscount As Double
        Dim mGross As Double
        Dim mQty As Double
        Dim mRate As Double
        Dim mItemAmount As Double
        Dim mItemValue As Double
        Dim mTotQty As Double
        Dim j As Integer
        Dim I As Integer
        Dim mItemCode As String
        Dim xStr As String
        Dim mExpPercent As Double
        Dim mNetAccessAmt As Double
        Dim mTaxableAmount As Double
        Dim mShortage As Double
        Dim mTotItemAmount As Double
        Dim pTotExportExp As Double
        Dim pTotOthers As Double
        Dim pTotCustomDuty As Double
        Dim pTotCustomDutyExport As Double
        Dim pTotMSC As Double
        Dim pTotDiscount As Double
        Dim pTotRO As Double
        Dim pTotTCS As Double
        Dim mTotExp As Double
        Dim pTCSPer As Double
        Dim pTotGST As Double
        Dim pTotOtherExp As Double
        Dim mIGSTPer As Double
        Dim mSGSTPer As Double
        Dim mCGSTPer As Double
        Dim mIGSTAmount As Double
        Dim mSGSTAmount As Double
        Dim mCGSTAmount As Double
        Dim mLandedCost As Double
        Dim mNetIGSTAmount As Double
        Dim mNetSGSTAmount As Double
        Dim mNetCGSTAmount As Double
        Dim mNetGSTAmount As Double
        Dim mFreightper As Double
        Dim mFreightAmount As Double
        Dim pTotVODDiscount As Double
        Dim mFreightCost As Double

        Dim mExpName As String
        Dim mIsTaxable As String
        Dim mOtherTaxableAmount As Double
        Dim mGSTableAmount As Double

        Dim mMaxCGST As Double = 0
        Dim mMaxSGST As Double = 0
        Dim mMaxIGST As Double = 0

        pRound = 0
        mQty = 0
        mRate = 0
        mItemAmount = 0
        mTotItemAmount = 0
        mTotExp = 0
        mNetCGSTAmount = 0
        mNetSGSTAmount = 0
        mNetIGSTAmount = 0
        mNetGSTAmount = 0
        mOtherTaxableAmount = 0

        With SprdExp
            For I = 1 To SprdExp.MaxRows
                .Row = I
                .Col = ColExpName
                mExpName = Trim(.Text)

                If MainClass.ValidateWithMasterTable(mExpName, "NAME", "TAXABLE", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GST_ENABLED='Y' AND TAXABLE='Y'") = True Then
                    mIsTaxable = MasterNo
                Else
                    mIsTaxable = "N"
                End If

                If mIsTaxable = "Y" Then
                    .Col = ColExpAmt
                    mOtherTaxableAmount = mOtherTaxableAmount + CDbl(VB6.Format(Val(.Text), "0.00"))
                End If
            Next
        End With

        With SprdMain
            j = .MaxRows
            For I = 1 To j
                .Row = I
                .Col = 0
                If .Text = "Del" Then GoTo DontCalc

                If VB.Left(lblBookType.Text, 1) = "W" Or VB.Left(lblBookType.Text, 1) = "R" Then
                    .Col = ColWoDesc
                    If .Text = "" Then GoTo DontCalc
                Else
                    .Col = ColItemCode
                    If .Text = "" Then GoTo DontCalc
                    mItemCode = .Text
                End If

                .Col = ColQty
                mQty = Val(.Text)

                mTotQty = mTotQty + mQty

                If lblRMPO.Text = "R" Then
                    .Col = ColRMRate
                    mRate = Val(.Text)

                    .Col = ColRMDRWRate
                    mRate = mRate + Val(.Text)
                Else
                    .Col = ColItemRate
                    mRate = Val(.Text)
                End If

                .Col = ColItemRate
                .Text = CStr(mRate)

                .Col = ColItemDisc
                mDiscount = Val(.Text)
                .Text = CStr(mDiscount)

                mGross = mRate - (mRate * mDiscount * 0.01)

                .Col = ColGross
                .Text = CStr(mQty * mGross)

                mItemAmount = mQty * mGross '- mDiscount			
                mTotItemAmount = mTotItemAmount + mItemAmount

                .Col = ColCGSTPer
                mMaxCGST = IIf(Val(.Text) > mMaxCGST, Val(.Text), mMaxCGST)

                .Col = ColSGSTPer
                mMaxSGST = IIf(Val(.Text) > mMaxSGST, Val(.Text), mMaxSGST)

                .Col = ColIGSTPer
                mMaxIGST = IIf(Val(.Text) > mMaxIGST, Val(.Text), mMaxIGST)

DontCalc:
            Next I
        End With

        mDiscount = 0
        mNetAccessAmt = Val(CStr(mTotItemAmount))
        mTaxableAmount = Val(CStr(mTotItemAmount + mOtherTaxableAmount))

        With SprdMain
            j = .MaxRows
            For I = 1 To j
                .Row = I
                .Col = 0
                If .Text = "Del" Then GoTo DontCalc1

                If VB.Left(lblBookType.Text, 1) = "W" Or VB.Left(lblBookType.Text, 1) = "R" Then
                    .Col = ColWoDesc
                    If .Text = "" Then GoTo DontCalc1
                Else
                    .Col = ColItemCode
                    If .Text = "" Then GoTo DontCalc1
                    mItemCode = .Text
                End If

                .Col = ColQty
                mQty = Val(.Text)


                .Col = ColItemRate
                mRate = Val(.Text)

                .Col = ColItemDisc
                mDiscount = Val(.Text)

                mGross = mRate - (mRate * mDiscount * 0.01)


                mItemAmount = mQty * mGross '- mDiscount			

                .Col = ColCGSTPer
                mCGSTPer = Val(.Text)

                .Col = ColSGSTPer
                mSGSTPer = Val(.Text)

                .Col = ColIGSTPer
                mIGSTPer = Val(.Text)

                .Col = ColFreightCost
                mFreightCost = Val(.Text)

                If mTotItemAmount = 0 Then
                    mGSTableAmount = 0
                Else
                    If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
                        mGSTableAmount = mItemAmount
                    Else
                        mGSTableAmount = mItemAmount + CDbl(VB6.Format(mOtherTaxableAmount * mItemAmount / mTotItemAmount, "0.00")) '' VB6.Format(Val(.Text), "0.00")	
                    End If

                End If


                mCGSTAmount = CDbl(VB6.Format(mGSTableAmount * mCGSTPer * 0.01, "0.00"))
                mSGSTAmount = CDbl(VB6.Format(mGSTableAmount * mSGSTPer * 0.01, "0.00"))
                mIGSTAmount = CDbl(VB6.Format(mGSTableAmount * mIGSTPer * 0.01, "0.00"))


                mNetCGSTAmount = mNetCGSTAmount + mCGSTAmount
                mNetSGSTAmount = mNetSGSTAmount + mSGSTAmount
                mNetIGSTAmount = mNetIGSTAmount + mIGSTAmount

                mNetGSTAmount = mNetGSTAmount + mCGSTAmount + mSGSTAmount + mIGSTAmount

                mLandedCost = mGross ''+ IIf(chkGSTApplicable.Value = vbChecked, 0, mGross * mCGSTPer * 0.01)			

                .Col = ColCGSTAmount
                .Text = VB6.Format(mCGSTAmount, "0.00")

                .Col = ColSGSTAmount
                .Text = VB6.Format(mSGSTAmount, "0.00")

                .Col = ColIGSTAmount
                .Text = VB6.Format(mIGSTAmount, "0.00")

                .Col = ColLandedCost
                .Text = VB6.Format(mLandedCost, "0.00")

DontCalc1:
            Next I
        End With


        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
            mTaxableAmount = mTaxableAmount + mOtherTaxableAmount
            mCGSTAmount = CDbl(VB6.Format(mOtherTaxableAmount * mMaxCGST * 0.01, "0.00"))
            mSGSTAmount = CDbl(VB6.Format(mOtherTaxableAmount * mMaxSGST * 0.01, "0.00"))
            mIGSTAmount = CDbl(VB6.Format(mOtherTaxableAmount * mMaxIGST * 0.01, "0.00"))


            mNetCGSTAmount = mNetCGSTAmount + mCGSTAmount
            mNetSGSTAmount = mNetSGSTAmount + mSGSTAmount
            mNetIGSTAmount = mNetIGSTAmount + mIGSTAmount

            mNetGSTAmount = mNetGSTAmount + mCGSTAmount + mSGSTAmount + mIGSTAmount

        Else
            'mGSTableAmount = mItemAmount + CDbl(VB6.Format(mOtherTaxableAmount * mItemAmount / mTotItemAmount, "0.00")) '' VB6.Format(Val(.Text), "0.00")	
        End If


        Call BillExpensesCalcTots_GST(SprdExp, (txtAmendDate.Text), mNetAccessAmt, mTotItemAmount, mTaxableAmount, mIGSTPer, mSGSTPer, mCGSTPer, mNetIGSTAmount, mNetSGSTAmount, mNetCGSTAmount, pTotExportExp, mFreightper, mFreightAmount, pTotOthers, pTotCustomDutyExport, pTotCustomDuty, pTotMSC, pTotDiscount, pTotVODDiscount, pTotRO, pTotTCS, mTotExp, pTCSPer, "PO")


        lblTotItemValue.Text = VB6.Format(mTotItemAmount, "#0.00")
        '    lblGSTValue.text = VB6.Format(mIGSTAmount + mSGSTAmount + mCGSTAmount, "#0.00")			
        lblCGSTValue.Text = VB6.Format(mNetCGSTAmount, "#0.00")
        lblSGSTValue.Text = VB6.Format(mNetSGSTAmount, "#0.00")
        lblIGSTValue.Text = VB6.Format(mNetIGSTAmount, "#0.00")
        lblTotOtherExp.Text = VB6.Format(mTotExp, "#0.00")
        lblNetAmount.Text = VB6.Format(mTotExp + mTotItemAmount + mNetGSTAmount, "#0.00")
        lblTotFreight.Text = VB6.Format(mFreightAmount, "#0.00")
        lblTotCharges.Text = CStr(0) ''VB6.Format(mRO, "#0.00")			
        lblTotExpAmt.Text = VB6.Format(mTotExp, "#0.00")
        lblTotTaxableAmt.Text = VB6.Format(mTaxableAmount, "#0.00")

        lblRO.Text = VB6.Format(pTotRO, "#0.00")
        lblDiscount.Text = VB6.Format(pTotDiscount, "#0.00")
        '    lblSurcharge.text = VB6.Format(pTotSurcharge, "#0.00")			
        lblMSC.Text = VB6.Format(pTotMSC, "#0.00")
        lblTotQty.Text = VB6.Format(mTotQty, "#0.00")


        '    Call CheckPORate			

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
        Dim mStartingChk As Double
        Dim mMaxNo As String
        mAutoGen = 1

        'mStartingChk = CDbl(50000 & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"))


        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_PO)  " & vbCrLf _
            & " FROM PUR_PURCHASE_HDR " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_PO,LENGTH(AUTO_KEY_PO)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAutoGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsAutoGen
            If .EOF = False Then
                mMaxNo = IIf(IsDBNull(RsAutoGen.Fields(0).Value), 0, RsAutoGen.Fields(0).Value)
                If mMaxNo > 0 Then
                    mAutoGen = Mid(mMaxNo, 1, Len(mMaxNo) - 6)
                    mAutoGen = mAutoGen + 1
                Else
                    mAutoGen = 1
                End If
            End If
        End With

        AutoGenPONoSeq = CDbl(mAutoGen & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"))
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
        Dim mItemUOM As String = ""
        Dim mQty As Double
        Dim mRate As Double
        Dim mQtyInKgs As Double
        Dim mRateInKgs As Double
        Dim mDisc As Double
        Dim mGross As Double
        Dim mQtyRecd As Double
        Dim mRemarks As String
        Dim mWODesc As String
        Dim mStatus As String
        Dim xUpdate As Boolean
        Dim mPOWEFDate As String
        Dim mIsTentativeRate As String
        Dim mFreightCost As Double
        Dim mVolumeDiscount As Double
        Dim mCGSTPer As String
        Dim mSGSTPer As String
        Dim mIGSTPer As String

        Dim mCGSTAmount As String
        Dim mSGSTAmount As String
        Dim mIGSTAmount As String

        Dim mAcctPostCode As String = ""
        Dim mAcctPostName As String = ""
        Dim mLandedCost As Double
        Dim mOutWardCode As String = ""
        Dim mHSNCode As String = ""
        Dim mISReprocess As String
        Dim mRMRate As Double
        Dim mRMDRWRate As Double
        Dim mAssetsNo As String

        SqlStr = "Delete From  PUR_PURCHASE_DET " & vbCrLf _
            & " Where " & vbCrLf _
            & " MKEY=" & Val(lblMkey.Text) & ""

        PubDBCn.Execute(SqlStr)

        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I
                xUpdate = False

                .Col = ColWoDesc
                mWODesc = MainClass.AllowSingleQuote(.Text)

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColOutWardCode
                mOutWardCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColItemUOM
                mItemUOM = MainClass.AllowSingleQuote(.Text)

                .Col = ColQty
                mQty = Val(.Text)

                If lblRMPO.Text = "R" Then
                    .Col = ColRMRate
                    mRMRate = Val(.Text)

                    .Col = ColRMDRWRate
                    mRMDRWRate = Val(.Text)
                Else
                    mRMRate = 0
                    mRMDRWRate = 0
                End If
                .Col = ColItemRate
                mRate = Val(.Text)

                .Col = ColQtyInKgs
                mQtyInKgs = Val(.Text)

                .Col = ColRateInKgs
                mRateInKgs = Val(.Text)

                .Col = ColItemDisc
                mDisc = Val(.Text)

                .Col = ColGross
                mGross = Val(.Text)

                .Col = ColRemarks
                mRemarks = Trim(.Text) '' MainClass.AllowSingleQuote(.Text)			

                .Col = ColStatus
                mStatus = IIf(SprdMain.Value = System.Windows.Forms.CheckState.Checked, "Y", "N") '' IIf(Trim(.Text) = "", "N", Trim(.Text))      ''"N"

                .Col = ColFreightCost
                mFreightCost = Val(.Text)

                .Col = ColVolumeDiscount
                mVolumeDiscount = Val(.Text)

                .Col = ColCGSTPer
                mCGSTPer = Val(.Text)

                .Col = ColSGSTPer
                mSGSTPer = Val(.Text)

                .Col = ColIGSTPer
                mIGSTPer = Val(.Text)

                .Col = ColCGSTAmount
                mCGSTAmount = Val(.Text)

                .Col = ColSGSTAmount
                mSGSTAmount = Val(.Text)

                .Col = ColIGSTAmount
                mIGSTAmount = Val(.Text)

                .Col = ColLandedCost
                mLandedCost = Val(.Text)

                .Col = ColAcctPostName
                mAcctPostName = Trim(.Text)

                If MainClass.ValidateWithMasterTable(mAcctPostName, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P'") = True Then
                    mAcctPostCode = MasterNo
                End If

                .Col = ColIsTentativeRate
                mIsTentativeRate = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                .Col = ColReprocess
                mISReprocess = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                .Col = ColQtyRecd
                mQtyRecd = Val(.Text)

                .Col = ColQtyInKgs
                mQtyInKgs = Val(.Text)

                .Col = ColRateInKgs
                mRateInKgs = Val(.Text)

                .Col = ColHSN
                mHSNCode = .Text

                .Col = ColAssetsNo
                mAssetsNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColPO_WEF
                If lblBookType.Text = "PO" Or (lblBookType.Text = "JC" And mQty = 0) Then
                    If Trim(.Text) = "" Or Not IsDate(.Text) Then
                        mPOWEFDate = VB6.Format(txtWEF.Text, "DD/MM/YYYY")
                    Else
                        mPOWEFDate = VB6.Format(.Text, "DD/MM/YYYY")
                    End If
                Else
                    mPOWEFDate = VB6.Format(txtWEF.Text, "DD/MM/YYYY")
                End If

                SqlStr = ""

                If VB.Right(lblBookType.Text, 1) = "O" Or VB.Left(lblBookType.Text, 1) = "W" Or VB.Left(lblBookType.Text, 1) = "R" Or VB.Left(lblBookType.Text, 1) = "J" Then
                    If mRate > 0 Then
                        xUpdate = True
                    End If
                Else
                    If mQty > 0 Or lblRMPO.Text = "R" Then
                        xUpdate = True
                    End If
                End If

                If (mItemCode <> "" Or mWODesc <> "") And xUpdate = True And mRate > 0 Then
                    SqlStr = " INSERT INTO PUR_PURCHASE_DET ( " & vbCrLf _
                        & " MKEY,SERIAL_NO,ITEM_CODE, " & vbCrLf _
                        & " ITEM_UOM, ITEM_QTY,ITEM_PRICE, " & vbCrLf _
                        & " ITEM_DIS_PER, GROSS_AMT,ITEM_RECD_QTY, " & vbCrLf _
                        & " REMARKS, WO_DESCRIPTION, " & vbCrLf _
                        & " PO_ITEM_STATUS, COMPANY_CODE, PO_WEF_DATE, " & vbCrLf _
                        & " IS_TENTATIVE_RATE,FREIGHT_COST, " & vbCrLf _
                        & " CGST_PER, SGST_PER, IGST_PER, " & vbCrLf _
                        & " CGST_AMOUNT, SGST_AMOUNT, IGST_AMOUNT, " & vbCrLf _
                        & " ACCOUNT_POSTING_CODE,ITEM_LANDED_COST,VOLUME_DISCOUNT, ITEM_QTY_IN_KGS, " & vbCrLf _
                        & " ITEM_PRICE_IN_KGS,OUTWARD_ITEM_CODE,HSN_CODE,IS_REPROCESS, RM_ITEM_RATE, RM_DRAWING_RATE,ASSETS_NO) "

                    SqlStr = SqlStr & vbCrLf _
                        & " VALUES ( " & vbCrLf _
                        & " " & Val(lblMkey.Text) & "," & I & ", " & vbCrLf _
                        & " '" & mItemCode & "','" & mItemUOM & "', " & vbCrLf _
                        & " " & mQty & ", " & mRate & ", " & vbCrLf _
                        & " " & mDisc & "," & mGross & "," & mQtyRecd & ", " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mRemarks) & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mWODesc) & "'," & vbCrLf _
                        & " '" & mStatus & "'," & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                        & " TO_DATE('" & VB6.Format(mPOWEFDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & mIsTentativeRate & "'," & mFreightCost & ", " & vbCrLf _
                        & " " & mCGSTPer & "," & mSGSTPer & "," & mIGSTPer & "," & vbCrLf _
                        & " " & mCGSTAmount & "," & mSGSTAmount & "," & mIGSTAmount & "," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mAcctPostCode) & "'," & mLandedCost & "," & mVolumeDiscount & ", " & vbCrLf _
                        & " " & mQtyInKgs & ", " & mRateInKgs & ",'" & MainClass.AllowSingleQuote(mOutWardCode) & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mHSNCode) & "','" & MainClass.AllowSingleQuote(mISReprocess) & "'," & mRMRate & "," & mRMDRWRate & ",'" & mAssetsNo & "') "

                    PubDBCn.Execute(SqlStr)

                    If UpdateSuppCustDet((txtCode.Text), mItemCode, mRate, mDisc, "P") = False Then GoTo UpdateDetail1
                End If
            Next
        End With

        UpdateDetail1 = UpdatePOExp1()
        UpdateDetail1 = UpdatePOAnnex()

        UpdateDetail1 = True

        Exit Function
UpdateDetail1:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume			
    End Function

    Private Sub cmdAnnexPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAnnexPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportonPOAnnex(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = " SELECT CMST.SUPP_CUST_NAME, CMST.SUPP_CUST_CODE, CMST.LOCATION_ID, CMST.SUPP_CUST_ADDR, CMST.SUPP_CUST_CITY, CMST.SUPP_CUST_STATE" & vbCrLf _
                & " FROM FIN_SUPP_CUST_MST CMST1, FIN_SUPP_CUST_BUSINESS_MST CMST" & vbCrLf _
                & " WHERE CMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CMST1.SUPP_CUST_TYPE IN ('S','C')" & vbCrLf _
                & " AND CMST1.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
                & " AND CMST1.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _

        'SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"
        If ADDMode = True Then
            SqlStr = SqlStr & " AND CMST1.STATUS='O'"
        End If
        If MainClass.SearchGridMasterBySQL2((txtSupplierName.Text), SqlStr) = True Then
            txtSupplierName.Text = AcName
            txtBillTo.Text = AcName2
            txtSupplierName_Validating(txtSupplierName, New System.ComponentModel.CancelEventArgs(True))
            '        If TxtRemarks.Enabled = True Then TxtRemarks.SetFocus			
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

        If Trim(txtPONo.Text) = "" Then
            Exit Sub
        End If

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND PUR_TYPE='" & VB.Left(lblBookType.Text, 1) & "'" & vbCrLf _
            & " AND ORDER_TYPE='" & VB.Right(lblBookType.Text, 1) & "'" & vbCrLf & " AND AUTO_KEY_PO=" & Val(txtPONo.Text) & ""

        If MainClass.SearchGridMaster("", "PUR_PURCHASE_HDR", "trim(TO_CHAR(AMEND_NO,'000'))", "AMEND_DATE", , , SqlStr) = True Then
            txtAmendNo.Text = AcName
            txtAmendDate.Text = AcName1
            txtAmendNo_Validating(txtAmendNo, New System.ComponentModel.CancelEventArgs(False)) ''txtPONO_Validate False			
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

        mSearchItem = Trim(txtSearchItem.Text)
        Dim counter As Short
        With SprdMain
            counter = mSearchStartRow
            For I = counter To .MaxRows
                .Row = I

                .Col = ColItemCode
                mFindItemName = Trim(.Text)

                '            If mSearchItem = mFindItemName Then			
                If InStr(1, mFindItemName, mSearchItem, CompareMethod.Text) > 0 Then
                    MainClass.SetFocusToCell(SprdMain, I, ColItemCode)
                    mSearchStartRow = I + 1
                    GoTo NextRec
                End If

                .Col = ColItemName
                mFindItemName = Trim(.Text)

                '            If mSearchItem = mFindItemName Then			
                If InStr(1, mFindItemName, mSearchItem, CompareMethod.Text) > 0 Then
                    MainClass.SetFocusToCell(SprdMain, I, ColItemCode)
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

    Private Sub cmdSearchPO_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchPO.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = " SELECT AUTO_KEY_PO, AMEND_NO, PUR_ORD_DATE, NAV_PO_NO, CMST.SUPP_CUST_NAME, IH.SUPP_CUST_CODE, CMST.SUPP_CUST_ADDR, CMST.SUPP_CUST_CITY, CMST.SUPP_CUST_STATE" & vbCrLf _
                & " FROM PUR_PURCHASE_HDR IH, FIN_SUPP_CUST_BUSINESS_MST CMST" & vbCrLf _
                & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
                & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
                & " AND IH.BILL_TO_LOC_ID=CMST.LOCATION_ID" & vbCrLf _
                & " AND ISGSTENABLE_PO='Y'"


        SqlStr = SqlStr & vbCrLf & " AND ISRM_PO='" & IIf(lblRMPO.Text = "R", "Y", "N") & "'"

        SqlStr = SqlStr & vbCrLf & " AND PUR_TYPE='" & VB.Left(lblBookType.Text, 1) & "'" & vbCrLf & " AND ORDER_TYPE='" & VB.Right(lblBookType.Text, 1) & "'"

        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtPONo.Text = AcName
            txtAmendNo.Text = AcName1
            'txtBillTo.Text = 1
            txtAmendNo_Validating(txtAmendNo, New System.ComponentModel.CancelEventArgs(False)) '' txtPONO_Validate False			
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdSearchPrevPO_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchPrevPO.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = " SELECT IH.AUTO_KEY_PO, IH.AMEND_NO, IH.PUR_ORD_DATE, CMST.SUPP_CUST_NAME, IH.SUPP_CUST_CODE, CMST.SUPP_CUST_ADDR, CMST.SUPP_CUST_CITY, CMST.SUPP_CUST_STATE" & vbCrLf _
                & " FROM PUR_PURCHASE_HDR IH, FIN_SUPP_CUST_BUSINESS_MST CMST" & vbCrLf _
                & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
                & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
                & " AND IH.BILL_TO_LOC_ID=CMST.LOCATION_ID" & vbCrLf _
                & " AND IH.ISGSTENABLE_PO='Y'"

        SqlStr = SqlStr & vbCrLf _
            & " AND IH.ISRM_PO='" & IIf(lblRMPO.Text = "R", "Y", "N") & "'"

        SqlStr = SqlStr & vbCrLf _
            & " AND IH.PO_STATUS='Y' AND IH.PO_CLOSED='N'" & vbCrLf _
            & " AND IH.PUR_TYPE='" & VB.Left(lblBookType.Text, 1) & "'" & vbCrLf _
            & " AND IH.ORDER_TYPE='" & VB.Right(lblBookType.Text, 1) & "'"

        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtPrevPONo.Text = AcName & "-" & AcName1
            txtPrevPONo_Validating(txtPrevPONo, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Sub cmdSearchShippedTo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchShippedTo.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"
        If ADDMode = True Then
            SqlStr = SqlStr & " AND STATUS='O'"
        End If
        If MainClass.SearchGridMaster((txtShippedTo.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            txtShippedTo.Text = AcName
            txtShippedTo_Validating(txtShippedTo, New System.ComponentModel.CancelEventArgs(True))
            '        If TxtRemarks.Enabled = True Then TxtRemarks.SetFocus			
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdSearchDeliveryTo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchDeliveryTo.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"
        If ADDMode = True Then
            SqlStr = SqlStr & " AND STATUS='O'"
        End If
        If MainClass.SearchGridMaster((txtDeliveryTo.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            txtDeliveryTo.Text = AcName
            txtDeliveryTo_Validating(txtDeliveryTo, New System.ComponentModel.CancelEventArgs(True))
            '        If TxtRemarks.Enabled = True Then TxtRemarks.SetFocus			
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdServProvided_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdServProvided.Click
        Call SearchProvidedMaster()
    End Sub

    Private Sub cmdTC_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdTC.Click
        On Error GoTo IconFileErr
        Dim mFilename As String

        cdgFilePathOpen.Filter = "(*.bmp;*.ico;*.gif;*.jpg;*.pdf)/*.bmp;*.ico;*.gif;*.jpg;*.pdf"
        cdgFilePathOpen.ShowDialog()

        'assign the image file name to the fileName variable			
        mFilename = cdgFilePathOpen.FileName

        'if the file name is valid, load the image in the image control on the form			
        '    If mFilename <> "" Then			
        '        Set ImagePhoto.Picture = LoadPicture(mFilename)  ''temp  comments sandeep			
        '    End If			

        txtTCPath.Text = mFilename
        Exit Sub
        ' DataChanged			
IconFileErr:
        'If cdgFilePath.CancelError = True Then MsgInformation("Cancelled by user")			
    End Sub

    Private Sub cmdTCShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdTCShow.Click

        'ShellExecute(Me.Handle.ToInt32, "Open", txtTCPath.Text, vbNullString, vbNullString, SW_SHOWNORMAL)			


        'Dim SqlStr As String = ""			
        'Dim RsTemp As ADODB.Recordset=Nothing= Nothing=Nothing=NOthing			
        '			
        '			
        '    lngImgSiz = 0			
        '    lngOffset = 0			
        '    'The BLOB Method			
        '			
        ''Open the temporary file to save the BLOB to			
        '			
        ''Read the binary data into the byte variable array			
        '			
        '    SqlStr = " SELECT MKEY, TC_DOC_DESC, TC_DOC_EXT, TPR_DOC_DESC, TPR_DOC_EXT, TC_BLOB_DATA, TPR_BLOB_DATA " & vbCrLf _			
        ''            & " FROM PUR_PURCHASE_TC_TRN " & vbCrLf _			
        ''            & " WHERE MKEY = '" & lblMkey.text & "'"			
        '			
        '    Set RsTemp = New ADODB.Recordset			
        '			
        '    With RsTemp			
        '        .CursorType = adOpenKeyset			
        '        .LockType = adLockOptimistic			
        '        .Open SqlStr, PubDBCnBlob			
        '        If RsTemp.EOF = False Then			
        ''            cdgFilePath.ShowSave           ''.ShowOpen			
        ''            StrTempPic = cdgFilePath.FileName			
        '			
        '			
        '            Dim sTempDir As String			
        '            On Error Resume Next			
        '			
        '            StrTempPic = PubDomainUserDesktopPath & "\" & VB6.Format(PubCurrDate, "DDMMYYYY") & VB6.Format(GetServerTime, "HHMM") & "_TC." & RsTemp("TC_DOC_EXT").Value       ''RsTemp("TC_DOC_DESC").Value			
        '            If Len(Dir(StrTempPic)) > 0 Then			
        '               Kill StrTempPic			
        '            End If			
        '			
        '            lngImgSiz = RsTemp("TC_BLOB_DATA").ActualSize			
        '			
        '            nHand = FreeFile			
        '            Open StrTempPic For Binary As #nHand			
        '			
        '            Do While lngOffset < lngImgSiz			
        '                Chunk() = RsTemp("TC_BLOB_DATA").GetChunk(conChunkSize)			
        '               Put #nHand, , Chunk()			
        '               lngOffset = lngOffset + conChunkSize			
        '            Loop			
        '        End If			
        '      .Close			
        '    End With			
        '			
        '    Close #nHand			


    End Sub

    Private Sub cmdTPRI_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdTPRI.Click
        On Error GoTo IconFileErr
        Dim mFilename As String

        cdgFilePathOpen.Filter = "(*.bmp;*.ico;*.gif;*.jpg;*.pdf)/*.bmp;*.ico;*.gif;*.jpg;*.pdf"
        cdgFilePathOpen.ShowDialog()

        'assign the image file name to the fileName variable			
        mFilename = cdgFilePathOpen.FileName

        'if the file name is valid, load the image in the image control on the form			
        '    If mFilename <> "" Then			
        '        Set ImagePhoto.Picture = LoadPicture(mFilename)  ''temp  comments sandeep			
        '    End If			

        txtTPRPath.Text = mFilename
        Exit Sub
        ' DataChanged			
IconFileErr:
        'If cdgFilePath.CancelError = True Then MsgInformation("Cancelled by user")			
    End Sub


    Private Sub cmdTPShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdTPShow.Click

        'ShellExecute(Me.Handle.ToInt32, "Open", txtTPRPath.Text, vbNullString, vbNullString, SW_SHOWNORMAL)			


        'Dim SqlStr As String = ""			
        'Dim RsTemp As ADODB.Recordset=Nothing= Nothing=Nothing=NOthing			
        '			
        '			
        '    lngImgSiz = 0			
        '    lngOffset = 0			
        '    'The BLOB Method			
        '			
        ''Open the temporary file to save the BLOB to			
        '			
        ''Read the binary data into the byte variable array			
        '			
        '    SqlStr = " SELECT MKEY, TC_DOC_DESC, TC_DOC_EXT, TPR_DOC_DESC, TPR_DOC_EXT, TC_BLOB_DATA, TPR_BLOB_DATA " & vbCrLf _			
        ''            & " FROM PUR_PURCHASE_TC_TRN " & vbCrLf _			
        ''            & " WHERE MKEY = '" & lblMkey.text & "'"			
        '			
        '    Set RsTemp = New ADODB.Recordset			
        '			
        '    With RsTemp			
        '        .CursorType = adOpenKeyset			
        '        .LockType = adLockOptimistic			
        '        .Open SqlStr, PubDBCnBlob			
        '        If RsTemp.EOF = False Then			
        ''            cdgFilePath.ShowSave           ''.ShowOpen			
        ''            StrTempPic = cdgFilePath.FileName			
        '			
        '			
        '            Dim sTempDir As String			
        '            On Error Resume Next			
        ''            sTempDir = CurDir    'Remember the current active directory			
        ''            cdgFilePath.DialogTitle = "Select a directory" 'titlebar			
        ''            cdgFilePath.InitDir = mLocalPath       '' "D:\" ''App.path 'start dir, might be "C:\" or so also			
        '            cdgFilePath.FileName = "Select a Directory"  'Something in filenamebox			
        ''            cdgFilePath.flags = cdlOFNNoValidate + cdlOFNHideReadOnly			
        ''            cdgFilePath.filter = "Directories|*.~#~" 'set files-filter to show dirs only  ''cdgFilePath.filter = "(*.bmp;*.ico;*.gif;*.jpg)/*.bmp;*.ico;*.gif;*.jpg"			
        ''            cdgFilePath.CancelError = True 'allow escape key/cancel			
        ''            cdgFilePath.ShowSave   'show the dialog screen			
        ''			
        ''            If err <> 32755 Then    ' User didn't chose Cancel.			
        ''                StrTempPic = CurDir			
        ''            End If			
        ''			
        ''            ChDir sTempDir  'restore path to what it was at entering			
        '			
        '			
        '            StrTempPic = mLocalPath & "\" & VB6.Format(PubCurrDate, "DDMMYYYY") & VB6.Format(GetServerTime, "HHMM") & "_TPR." & RsTemp("TPR_DOC_EXT").Value       ''RsTemp("TC_DOC_DESC").Value			
        '            If Len(Dir(StrTempPic)) > 0 Then			
        ''               If MsgQuestion("File Already Exists.Want to overright? ") = vbNo Then			
        ''                    Exit Sub			
        ''                End If			
        '               Kill StrTempPic			
        '            End If			
        '			
        '            lngImgSiz = RsTemp("TPR_BLOB_DATA").ActualSize			
        '			
        '            nHand = FreeFile			
        '            Open StrTempPic For Binary As #nHand			
        '			
        '            Do While lngOffset < lngImgSiz			
        '                Chunk() = RsTemp("TPR_BLOB_DATA").GetChunk(conChunkSize)			
        '               Put #nHand, , Chunk()			
        '               lngOffset = lngOffset + conChunkSize			
        '            Loop			
        '        End If			
        '      .Close			
        '    End With			
        '			
        '    Close #nHand			
        '			
        '    ShellExecute Me.hWnd, "Open", StrTempPic, vbNullString, vbNullString, SW_SHOWNORMAL			
        '			
        ''    Open StrTempPic For Output As #nHand			
        '    ''Open tSendFileName For Output As #iFreeFile			
        '			
        ''    Dim wsh As Object			
        ''    Set wsh = CreateObject("wscript.shell")			
        '    wsh.Run "open.exe """ & StrTempPic & """"   '""" & strDestination & """ /y /r", 1, True			
        ''    'Application.ScreenUpdating = False			
        ''    'Application.DisplayAlerts = False			
        ''    Set wsh = Nothing			
        '			
        '			
        '			
        ''After loading the image, get rid of the temporary file			
        ''      ImagePhoto1.Picture = LoadPicture(StrTempPic)			
        ''      Kill StrTempPic			
    End Sub

    Private Sub cmdUpdateCosting_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdUpdateCosting.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mDate As String = ""

        SqlStr = " SELECT DISTINCT WEF, AMEND_NO , A.ITEM_CODE, ITEM_SHORT_DESC" & vbCrLf _
                        & " FROM PRD_BOP_COST_HDR A, INV_ITEM_MST B" & vbCrLf _
                        & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND A.SUPP_CUST_CODE = '" & MainClass.AllowSingleQuote(txtCode.Text) & "'" & vbCrLf _
                        & " AND A.COMPANY_CODE = B.COMPANY_CODE" & vbCrLf _
                        & " AND A.ITEM_CODE = B.ITEM_CODE" & vbCrLf _
                        & " ORDER BY A.ITEM_CODE, ITEM_SHORT_DESC,AMEND_NO, WEF"


        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            mDate = AcName
            If UpdateCosting(mDate) = False Then GoTo ErrPart
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'SqlStr = " Select WEF, NET_COST " & vbCrLf _
        '                & " FROM PRD_BOP_COST_HDR" & vbCrLf _
        '                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        '                & " And SUPP_CUST_CODE = '" & MainClass.AllowSingleQuote(mSupplierCode) & "'" & vbCrLf _
        '                & " AND ITEM_CODE = '" & MainClass.AllowSingleQuote(mItemCode) & "'" & vbCrLf _
        '                & " AND AMEND_NO = ( " & vbCrLf _
        '                & " SELECT MAX(AMEND_NO) " & vbCrLf _
        '                & " FROM PRD_BOP_COST_HDR" & vbCrLf _
        '                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        '                & " AND SUPP_CUST_CODE = '" & MainClass.AllowSingleQuote(mSupplierCode) & "'" & vbCrLf _
        '                & " AND ITEM_CODE = '" & MainClass.AllowSingleQuote(mItemCode) & "')"

        'MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


        'Dim I As Integer
        'Dim SqlStr As String = ""
        'Dim RsTemp As ADODB.Recordset = Nothing
        'Dim mItemCode As String
        'Dim mSupplierCode As String

        'Dim mWef As String
        'Dim mNetCost As Double
        'Dim mItemCategory As String
        'Dim mCostingReq As Boolean

        ''    Left(lblBookType.text, 1) = "P"			
        'If VB.Left(lblBookType.Text, 1) <> "P" Then Exit Sub 'If lblBookType.text <> "PO" Then Exit Sub			

        'mSupplierCode = Trim(txtCode.Text)
        'With SprdMain
        '    For I = 1 To .MaxRows
        '        .Row = I
        '        .Col = ColItemCode
        '        mItemCode = Trim(.Text)

        '        mItemCategory = GetProductionType(mItemCode)
        '        mCostingReq = GetCostingRequired(mItemCode)

        '        If mCostingReq = True Then

        '            '                .Col = ColPO_WEF			
        '            '                mPrevWEF = VB6.Format(Trim(.Text), "DD/MM/YYYY")			

        '            mWef = ""
        '            mNetCost = 0
        '            SqlStr = " SELECT WEF, NET_COST " & vbCrLf _
        '                & " FROM PRD_BOP_COST_HDR" & vbCrLf _
        '                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        '                & " AND SUPP_CUST_CODE = '" & MainClass.AllowSingleQuote(mSupplierCode) & "'" & vbCrLf _
        '                & " AND ITEM_CODE = '" & MainClass.AllowSingleQuote(mItemCode) & "'" & vbCrLf _
        '                & " AND AMEND_NO = ( " & vbCrLf _
        '                & " SELECT MAX(AMEND_NO) " & vbCrLf _
        '                & " FROM PRD_BOP_COST_HDR" & vbCrLf _
        '                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        '                & " AND SUPP_CUST_CODE = '" & MainClass.AllowSingleQuote(mSupplierCode) & "'" & vbCrLf _
        '                & " AND ITEM_CODE = '" & MainClass.AllowSingleQuote(mItemCode) & "')"

        '            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        '            If RsTemp.EOF = False Then
        '                mWef = VB6.Format(IIf(IsDBNull(RsTemp.Fields("WEF").Value), "", RsTemp.Fields("WEF").Value), "DD/MM/YYYY")
        '                mNetCost = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("NET_COST").Value), 0, RsTemp.Fields("NET_COST").Value), "0.0000"))
        '            End If

        '            .Row = I

        '            .Col = ColItemRate
        '            .Text = VB6.Format(mNetCost, "0.0000")

        '            .Col = ColPO_WEF
        '            .Text = VB6.Format(mWef, "DD/MM/YYYY")
        '        End If
        '    Next
        'End With

    End Sub
    Private Function UpdateCosting(ByRef mDate As String) As Boolean

        Dim I As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mItemCode As String
        Dim mSupplierCode As String

        Dim mWef As String
        Dim mNetCost As Double
        Dim mItemCategory As String
        Dim mCostingReq As Boolean


        If VB.Left(lblBookType.Text, 1) <> "P" Then UpdateCosting = True : Exit Function 'If lblBookType.text <> "PO" Then Exit Sub			

        mSupplierCode = Trim(txtCode.Text)
        With SprdMain
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                mItemCategory = GetProductionType(mItemCode)
                mCostingReq = GetCostingRequired(mItemCode)

                If mCostingReq = True Then
                    mWef = ""
                    mNetCost = 0
                    SqlStr = " SELECT WEF, NET_COST " & vbCrLf _
                        & " FROM PRD_BOP_COST_HDR" & vbCrLf _
                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND SUPP_CUST_CODE = '" & MainClass.AllowSingleQuote(mSupplierCode) & "'" & vbCrLf _
                        & " AND ITEM_CODE = '" & MainClass.AllowSingleQuote(mItemCode) & "'" & vbCrLf _
                        & " AND WEF = TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                    '           & " AND AMEND_NO = ( " & vbCrLf _
                    '& " SELECT MIN(AMEND_NO) " & vbCrLf _
                    '& " FROM PRD_BOP_COST_HDR" & vbCrLf _
                    '& " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    '& " AND SUPP_CUST_CODE = '" & MainClass.AllowSingleQuote(mSupplierCode) & "'" & vbCrLf _
                    '& " AND ITEM_CODE = '" & MainClass.AllowSingleQuote(mItemCode) & "')" & vbCrLf _

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                    If RsTemp.EOF = False Then
                        mWef = VB6.Format(IIf(IsDBNull(RsTemp.Fields("WEF").Value), "", RsTemp.Fields("WEF").Value), "DD/MM/YYYY")
                        mNetCost = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("NET_COST").Value), 0, RsTemp.Fields("NET_COST").Value), "0.0000"))

                        .Row = I

                        .Col = ColItemRate
                        .Text = VB6.Format(mNetCost, "0.0000")

                        .Col = ColPO_WEF
                        .Text = VB6.Format(mWef, "DD/MM/YYYY")
                    End If

                End If
            Next
        End With

    End Function
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
        MainClass.ButtonStatus(Me, XRIGHT, RsPOMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Public Sub frmPO_GST_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'If lblBookType.Text = "PO" Then
        '    Me.Text = "Purchase Order - Open"
        'ElseIf lblBookType.Text = "PC" Then
        '    Me.Text = "Purchase Order - Close"
        'ElseIf lblBookType.Text = "RC" Then
        '    Me.Text = "Purchase Order - Project"
        'ElseIf lblBookType.Text = "JC" Then
        '    Me.Text = "Job Work Order"
        'ElseIf lblBookType.Text = "WC" Then
        '    Me.Text = "Service Purchase Order"
        'ElseIf lblBookType.Text = "LC" Then
        '    Me.Text = "Assets Under Lease (Purchase Order)"
        'End If

        SqlStr = "Select * From PUR_PURCHASE_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPOMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From PUR_PURCHASE_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPODetail, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From PUR_PURCHASE_ANNEX WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPOAnnex, ADODB.LockTypeEnum.adLockReadOnly)


        cboGSTStatus.Items.Clear()
        cboGSTStatus.Items.Add("GST Refund")
        cboGSTStatus.Items.Add("Reverse Charge")
        cboGSTStatus.Items.Add("Exempt")
        cboGSTStatus.Items.Add("Non-GST")
        cboGSTStatus.Items.Add("Ineligible")
        cboGSTStatus.Items.Add("Composit")

        cboGSTStatus.SelectedIndex = -1

        Call AssignGrid(False)

        SetTextLengths()
        Clear1()
        If CmdAdd.Enabled = True Then cmdAdd_Click(CmdAdd, New System.EventArgs())
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim i As Integer
        Dim inti As Integer

        oledbCnn = New OleDbConnection(StrConn)
        SqlStr = ""


        SqlStr = " Select " & vbCrLf _
            & " A.MKEY As MKEY, A.AUTO_KEY_PO As PO_NO1,CONCAT(SUBSTR(A.AUTO_KEY_PO,0,LENGTH(A.AUTO_KEY_PO)-6),CONCAT('-',SUBSTR(A.AUTO_KEY_PO,LENGTH(A.AUTO_KEY_PO)-5,LENGTH(A.AUTO_KEY_PO)))) as PO_NO, TO_CHAR(A.PUR_ORD_DATE,'DD/MM/YYYY') AS PO_DATE, " & vbCrLf _
            & " A.AMEND_NO, TO_CHAR(A.AMEND_DATE,'DD/MM/YYYY') AS AMEND_DATE,  " & vbCrLf _
            & " TO_CHAR(A.AMEND_WEF_DATE,'DD/MM/YYYY') AS WEF, B.SUPP_CUST_NAME AS NAME, " & vbCrLf _
            & " A.PAYDAYS, A.REMARKS, A.DELIVERY , A.EXCISE_OTHERS, " & vbCrLf _
            & " A.PAYMENT_CODE, A.MODE_DESPATCH, A.INSPECTION, " & vbCrLf _
            & " A.PACKING_FORWARDING, A.INSURANCE, A.OTHERS_COND1, " & vbCrLf _
            & " A.OTHERS_COND2, A.PO_STATUS " & vbCrLf _
            & " FROM PUR_PURCHASE_HDR A, FIN_SUPP_CUST_MST B " & vbCrLf _
            & " WHERE A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf _
            & " AND A.SUPP_CUST_CODE=B.SUPP_CUST_CODE " & vbCrLf _
            & " AND A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        SqlStr = SqlStr & vbCrLf & " AND ISRM_PO='" & IIf(lblRMPO.Text = "R", "Y", "N") & "'"

        SqlStr = SqlStr & vbCrLf _
            & " AND PUR_TYPE='" & VB.Left(lblBookType.Text, 1) & "'" & vbCrLf _
            & " AND ORDER_TYPE='" & VB.Right(lblBookType.Text, 1) & "' AND ISGSTENABLE_PO='Y'"

        SqlStr = SqlStr & vbCrLf _
            & " AND A.MKEY = (SELECT MAX(MKEY) FROM PUR_PURCHASE_HDR WHERE AUTO_KEY_PO=A.AUTO_KEY_PO)"

        SqlStr = SqlStr & " ORDER BY SUBSTR(AUTO_KEY_PO,LENGTH(AUTO_KEY_PO)-5,4) DESC,A.AUTO_KEY_PO DESC,A.AMEND_NO DESC"

        'MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        'FormatSprdView()
        ClearGroupFromUltraGrid(UltraGrid1)
        ClearFilterFromUltraGrid(UltraGrid1)

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

            'SqlStr = " Select " & vbCrLf _
            '& " A.MKEY As MKEY, A.AUTO_KEY_PO As PO_NO, TO_CHAR(A.PUR_ORD_DATE,'DD/MM/YYYY') AS PO_DATE, " & vbCrLf _
            '& " A.AMEND_NO, TO_CHAR(A.AMEND_DATE,'DD/MM/YYYY') AS AMEND_DATE,  " & vbCrLf _
            '& " TO_CHAR(A.AMEND_WEF_DATE,'DD/MM/YYYY') AS WEF, B.SUPP_CUST_NAME AS NAME, " & vbCrLf _
            '& " A.PAYDAYS, A.REMARKS, A.DELIVERY , A.EXCISE_OTHERS, " & vbCrLf _
            '& " A.PAYMENT_CODE, A.MODE_DESPATCH, A.INSPECTION, " & vbCrLf _
            '& " A.PACKING_FORWARDING, A.INSURANCE, A.OTHERS_COND1, " & vbCrLf _
            '& " A.OTHERS_COND2, A.PO_STATUS" & vbCrLf _


            'UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Key = "Locked"
            UltraGrid1.DisplayLayout.Bands(0).Columns(0).Header.Caption = "MKey"
            UltraGrid1.DisplayLayout.Bands(0).Columns(1).Header.Caption = "PO No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(2).Header.Caption = "PO No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(3).Header.Caption = "PO Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(4).Header.Caption = "Amend No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(5).Header.Caption = "Amend Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(6).Header.Caption = "Amend WEF Date"

            UltraGrid1.DisplayLayout.Bands(0).Columns(7).Header.Caption = "Supplier Name"
            UltraGrid1.DisplayLayout.Bands(0).Columns(8).Header.Caption = "Pay Days"
            UltraGrid1.DisplayLayout.Bands(0).Columns(9).Header.Caption = "Remarks"
            UltraGrid1.DisplayLayout.Bands(0).Columns(10).Header.Caption = "Others"
            UltraGrid1.DisplayLayout.Bands(0).Columns(11).Header.Caption = "Payment Code"

            UltraGrid1.DisplayLayout.Bands(0).Columns(12).Header.Caption = "Mode of Despatch"
            UltraGrid1.DisplayLayout.Bands(0).Columns(13).Header.Caption = "Inspection"
            UltraGrid1.DisplayLayout.Bands(0).Columns(14).Header.Caption = "Packing & Forwarding"
            UltraGrid1.DisplayLayout.Bands(0).Columns(15).Header.Caption = "Insurance"
            UltraGrid1.DisplayLayout.Bands(0).Columns(16).Header.Caption = "Other Condition 1"
            UltraGrid1.DisplayLayout.Bands(0).Columns(17).Header.Caption = "Other Condition 2"
            UltraGrid1.DisplayLayout.Bands(0).Columns(18).Header.Caption = "Status"


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
            UltraGrid1.DisplayLayout.Bands(0).Columns(3).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(4).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(5).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(6).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(7).Width = 250
            UltraGrid1.DisplayLayout.Bands(0).Columns(8).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(9).Width = 200
            UltraGrid1.DisplayLayout.Bands(0).Columns(10).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(11).Width = 100


            'UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.MFGQty).MaskInput = "99999"

            Me.UltraGrid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True
            Me.UltraGrid1.DisplayLayout.Override.RowSelectorNumberStyle = RowSelectorNumberStyle.RowIndex

            UltraGrid1.DisplayLayout.Bands(0).Columns(1).Hidden = True
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

    Private Sub frmPO_GST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmPO_GST_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection			
        'PvtDBCn.Open StrConn	


        CurrFormHeight = 7245
        CurrFormWidth = 11355
        Call SetMainFormCordinate(Me)
        pmyMenu = myMenu
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, pmyMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)


        mAuthorisedPrint = IIf(InStr(1, XRIGHT, "P") > 0, True, False)
        mAuthorised = IIf(InStr(1, XRIGHT, "S") > 0, True, False)
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
        txtPONo.Text = ""
        txtPODate.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        txtAmendNo.Text = CStr(0)
        txtAmendDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtWEF.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkStatus.Enabled = False
        ChkActivate.CheckState = System.Windows.Forms.CheckState.Unchecked
        ChkActivate.Enabled = False
        chkPrintApp.CheckState = System.Windows.Forms.CheckState.Unchecked

        txtSupplierName.Text = ""

        txtDivision.Text = ""
        lblDivision.Text = ""
        txtDivision.Enabled = True

        chkCapital.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkDevelopment.CheckState = System.Windows.Forms.CheckState.Unchecked
        cboGSTStatus.SelectedIndex = -1
        cboGSTStatus.Enabled = True
        txtOldERPNo.Text = ""

        txtCode.Text = ""
        txtCode.Enabled = True
        txtSupplierName.Enabled = True

        txtOwner.Text = ""

        chkShipTo.Enabled = True
        chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked
        txtShippedTo.Enabled = False
        cmdSearchShippedTo.Enabled = False

        lblCGSTValue.Text = "0.00"
        lblSGSTValue.Text = "0.00"
        lblIGSTValue.Text = "0.00"
        lblTotOtherExp.Text = "0.00"
        txtShippedTo.Text = ""
        txtBillTo.Text = ""
        TxtShipTo.Text = ""

        txtDeliveryTo.Text = ""
        TxtDeliveryToLoc.Text = ""

        cmdsearch.Enabled = True
        SprdMain.Enabled = True
        SprdAnnex.Enabled = True
        TxtExchangeRate.Text = "1.000"

        txtExcise.Text = ""
        txtDespMode.Text = ""
        txtPacking.Text = ""
        txtPayment.Text = ""
        txtDelivery.Text = ""
        txtInspection.Text = "At out Works"
        txtInsurance.Text = ""
        txtOthCond2.Text = ""
        lblPaymentTerms.Text = ""
        txtPaymentDays.Text = ""
        txtAmendNo.Enabled = False
        txtAmendDate.Enabled = False

        txtServProvided.Text = ""
        lblAddUser.Text = ""
        lblAddDate.Text = ""
        lblModUser.Text = ""
        lblModDate.Text = ""

        chkApprovedWO_TC.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkApprovedWO_TC.Enabled = False

        chkTCAvailable.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkTCAvailable.Enabled = True
        txtTCPath.Text = ""
        cmdTC.Enabled = False

        chkTPRAvailable.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkTPRAvailable.Enabled = True
        txtTPRPath.Text = ""
        cmdTPRI.Enabled = False


        txtIndentNo.Text = ""
        txtIndentNo.Enabled = IIf(lblBookType.Text = "PC", True, False)

        txtPrevPONo.Text = ""
        txtRemarks.Text = ""

        chkRecdAcct.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkRecdAcct.Enabled = False

        txtRecdDate.Text = ""
        txtRecdDate.Enabled = False

        txtPrevPONo.Enabled = True
        cmdSearchPrevPO.Enabled = True

        txtAnnexTitle.Text = ""

        If lblBookType.Text = "LC" Then
            txtOwner.Enabled = True
            cmdOwner.Enabled = True

            txtOwner.Visible = True
            cmdOwner.Visible = True

            FraPostingDetails.Enabled = True
            FraPostingDetails.Visible = True
        Else
            txtOwner.Enabled = False
            cmdOwner.Enabled = False

            txtOwner.Visible = True
            cmdOwner.Visible = True

            FraPostingDetails.Enabled = False
            FraPostingDetails.Visible = False
        End If

        txtRMDesc.Text = ""
        txtRMQty.Text = ""
        txtRMRate.Text = ""

        If lblRMPO.Text = "R" Then
            lblRMDesc.Visible = True
            lblRMQty.Visible = True
            lblRMRate.Visible = True

            txtRMDesc.Visible = True
            txtRMQty.Visible = True
            txtRMRate.Visible = True
            cmdGetData.Visible = True
            cmdGetData.Enabled = True
        Else
            lblRMDesc.Visible = False
            lblRMQty.Visible = False
            lblRMRate.Visible = False

            txtRMDesc.Visible = False
            txtRMQty.Visible = False
            txtRMRate.Visible = False
            cmdGetData.Visible = False
        End If

        cmdUpdateCosting.Enabled = False

        optPostingDetails(0).Checked = False
        optPostingDetails(1).Checked = False
        optPostingDetails(2).Checked = False

        TabMain.SelectedIndex = 0

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 116 Then
            TabMain.TabPages.Remove(_TabMain_TabPage2)
            TabMain.TabPages.Remove(_TabMain_TabPage3)
            TabMain.TabPages.Remove(_TabMain_TabPage4)

            ''tabControl1.TabPages.Remove(tabPage1);
        End If
        Call DelTemp_Indent()

        mAmendStatus = False
        cmdAmend.Enabled = IIf(InStr(1, XRIGHT, "M") > 0, True, False) '' True			
        ChkPrintAllItem.CheckState = System.Windows.Forms.CheckState.Unchecked


        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)

        MainClass.ClearGrid(SprdAnnex, ConRowHeight)
        FormatSprdAnnex(-1)

        MainClass.ClearGrid(SprdExp)
        Call FillSprdExp()
        pShowCalc = False

        Call AutoCompleteSearch("FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", " STATUS='O' AND SUPP_CUST_TYPE IN ('S','C')", txtSupplierName)
        Call AutoCompleteSearch("FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", " STATUS='O' AND SUPP_CUST_TYPE IN ('S','C')", txtShippedTo)
        'Call AutoCompleteSearch("FIN_PAYTERM_MST", "PAY_TERM_CODE", "", txtPayment)
        Call AutoCompleteSearch("GEN_HSN_MST", "HSN_DESC", " CODETYPE='S'", txtServProvided)

        MainClass.ButtonStatus(Me, XRIGHT, RsPOMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume			
    End Sub
    Private Sub FillSprdExp()

        On Error GoTo ERR1
        Dim mLocal As String
        Dim SqlStr As String = ""

        Dim RS As ADODB.Recordset = Nothing
        Dim I As Integer

        MainClass.ClearGrid(SprdExp)
        pShowCalc = False
        If Trim(txtSupplierName.Text) <> "" Then
            mLocal = GetPartyBusinessDetail(txtCode.Text, Trim(txtBillTo.Text), "WITHIN_STATE")
            'If MainClass.ValidateWithMasterTable(txtSupplierName.Text, "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mLocal = IIf(MasterNo = "Y", "L", "C")
            'Else
            '    mLocal = ""
            'End If
        Else
            mLocal = ""
        End If

        SqlStr = "Select * From FIN_INTERFACE_MST " & vbCrLf & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND (Type='P' OR Type='B') "

        SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"

        SqlStr = SqlStr & vbCrLf & " Order By PrintSequence"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            I = 0
            Do While Not RS.EOF
                I = I + 1

                SprdExp.Row = I

                SprdExp.Col = ColRO
                SprdExp.Value = IIf(RS.Fields("ROUNDOFF").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                SprdExp.Col = ColExpName
                SprdExp.Text = RS.Fields("Name").Value

                SprdExp.Col = ColExpPercent
                If ADDMode = True Then
                    SprdExp.Text = Str(IIf(IsDBNull(RS.Fields("DefaultPercent").Value), 0, Str(RS.Fields("DefaultPercent").Value)))
                Else
                    SprdExp.Text = ""
                End If

                SprdExp.Col = ColExpAmt
                SprdExp.Text = "0"

                SprdExp.Col = ColExpSTCode
                SprdExp.Text = CStr(Val(IIf(IsDBNull(RS.Fields("CODE").Value), -1, RS.Fields("CODE").Value)))

                SprdExp.Col = ColExpAddDeduct
                SprdExp.Text = IIf(RS.Fields("Add_Ded").Value = "A", "A", "D")

                SprdExp.Col = ColExpIdent
                SprdExp.Text = IIf(IsDBNull(RS.Fields("Identification").Value), "OTR", RS.Fields("Identification").Value)
                If SprdExp.Text = "DAM" Then MainClass.ProtectCell(SprdExp, I, I, 1, SprdExp.MaxCols)

                SprdExp.Col = ColTaxable
                SprdExp.Text = IIf(IsDBNull(RS.Fields("Taxable").Value), "N", RS.Fields("Taxable").Value)

                SprdExp.Col = ColExciseable
                SprdExp.Text = IIf(IsDBNull(RS.Fields("Exciseable").Value), "N", RS.Fields("Exciseable").Value)

                If RS.Fields("Identification").Value = "ST" Then
                    If RS.Fields("STTYPE").Value = mLocal Then
                        SprdExp.RowHidden = False
                    Else
                        SprdExp.RowHidden = True
                    End If
                End If

                RS.MoveNext()

                If RS.EOF = False Then
                    SprdExp.MaxRows = SprdExp.MaxRows + 1
                End If
            Loop
        End If
        pShowCalc = True
        FormatSprdExp(-1)
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume			
    End Sub

    Private Sub lblGSTValue_Change()

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub lblTotOtherExp_Change()

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdAnnex_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdAnnex.Change

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdAnnex_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdAnnex.ClickEvent

        If eventArgs.col = 0 And eventArgs.row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdAnnex, eventArgs.row, ColAnnexDesc)
            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub SprdAnnex_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdAnnex.KeyUpEvent
        If eventArgs.keyCode = System.Windows.Forms.Keys.Tab Or eventArgs.keyCode = System.Windows.Forms.Keys.Enter Then
            Call SprdAnnex_LeaveCell(SprdAnnex, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(SprdAnnex.ActiveCol, SprdAnnex.ActiveRow, SprdAnnex.ActiveCol, SprdAnnex.ActiveRow, False))
        End If
    End Sub

    Private Sub SprdAnnex_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdAnnex.LeaveCell

        On Error GoTo ErrPart
        Dim xAnnexDesc As String

        If eventArgs.newRow = -1 Then Exit Sub

        Select Case eventArgs.col
            Case ColAnnexDesc
                SprdAnnex.Row = SprdAnnex.ActiveRow

                SprdAnnex.Col = ColAnnexDesc
                xAnnexDesc = SprdAnnex.Text

                If xAnnexDesc <> "" Then
                    MainClass.AddBlankSprdRow(SprdAnnex, ColAnnexDesc, ConRowHeight)
                Else
                    MainClass.SetFocusToCell(SprdAnnex, eventArgs.row, ColAnnexDesc)
                End If
        End Select

        FormatSprdAnnex(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdExp_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdExp.Change

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdExp_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdExp.LeaveCell

        On Error GoTo ErrPart
        Static ESCol As Object
        Static ESRow As Integer
        Static m_Exp As Object
        Static mIDENT As String
        Static m_Amt As Object
        Static m_ExpPercent As Double
        Static m_xp As Object
        Static m_xpn As String
        Static p_DebitAmt As Double
        Static p_Amt As Double
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

        ESCol = eventArgs.col
        ESRow = eventArgs.row
        Select Case eventArgs.col
            Case 1 'Exp.Name			
                If eventArgs.newRow >= ESRow Or eventArgs.newRow = -1 Then
                    SprdExp.Row = ESRow

                    SprdExp.Col = 1
                    m_Exp = MainClass.AllowSingleQuote(SprdExp.Text)

                    If SprdExp.Text = "" Then Exit Sub
                    If m_Exp <> "" Then Exit Sub

                    SprdExp.Col = ColExpIdent
                    mIDENT = SprdExp.Text

                    SqlStr = "Select * From FIN_INTERFACE_MST Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND Name= '" & m_Exp & "'"
                    SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
                    If RS.EOF = True Then
                        ESCol = 1
                        GoTo ErrPart
                    Else
                        If mIDENT = "ST" Then
                            SprdExp.Col = 2
                            SprdExp.Text = CStr(0)
                        End If
                        If RS.EOF = False Then
                            SprdExp.Row = ESRow
                            SprdExp.Col = 4
                            SprdExp.Text = IIf(RS.Fields("Add_Ded").Value = "A", "A", "D")
                        End If
                        SprdExp.Col = 1
                        If SprdExp.Text <> "" Then
                            If SprdExp.MaxRows = ESRow Then
                                MainClass.AddBlankSprdRow(SprdExp, ColExpName)
                                FormatSprdExp((SprdExp.MaxRows))
                            End If
                        End If
                    End If
                End If

            Case 2 'Exp. %			
                If eventArgs.newRow >= ESRow Or eventArgs.newRow = -1 Then
                    SprdExp.Row = ESRow
                    SprdExp.Col = 1
                    If SprdExp.Text = "" Then Exit Sub
                    '               mExp = SprdExp.Text			
                    m_xpn = SprdExp.Text
                    SprdExp.Col = 2
                    SprdExp.Row = ESRow
                    m_ExpPercent = Val(SprdExp.Value)
                    If m_ExpPercent = 0 Then
                        Exit Sub
                    Else
                        SprdExp.Col = ColExpIdent
                        mIDENT = SprdExp.Text

                        If mIDENT = "ST" Or mIDENT = "ED" Or mIDENT = "RO" Then
                            Call CalcTots()
                            Exit Sub
                        End If
                        SprdExp.Row = ESRow
                        SprdExp.Col = 3
                        If MainClass.ValidateWithMasterTable(m_xpn, "Name", "RoundOff", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
                            If MasterNo = True Then
                                SprdExp.Text = VB6.Format((CDbl(m_ExpPercent) * CDbl(lblTotItemValue.Text)) / 100, "0")
                            Else
                                SprdExp.Text = VB6.Format((CDbl(m_ExpPercent) * CDbl(lblTotItemValue.Text)) / 100, "0.00")
                            End If
                        End If
                    End If
                Else
                    ESCol = 2
                    ESRow = eventArgs.newRow
                    GoTo ErrPart
                End If

        End Select
        'Call DistributeExpInMainGrid			
        Call CalcTots()
        Exit Sub
ErrPart:
        SprdExp.Col = ESCol
        SprdExp.Col2 = ESCol
        SprdExp.Row = ESRow
        SprdExp.Row2 = ESRow
        SprdExp.BlockMode = True
        SprdExp.Action = 0
        SprdExp.BlockMode = False
        SprdExp.Focus()
    End Sub

    Private Sub SprdExp_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdExp.ClickEvent

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim cntCol As Integer

        With SprdMain
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = ColWoDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsPODetail.Fields("WO_DESCRIPTION").DefinedSize
            .TypeEditMultiLine = True
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE

            .set_ColWidth(.Col, 25)
            If VB.Left(lblBookType.Text, 1) = "W" Or VB.Left(lblBookType.Text, 1) = "R" Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = ColOutWardCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsPODetail.Fields("ITEM_CODE").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 8)
            If VB.Left(lblBookType.Text, 1) = "J" Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If
            .TypeEditMultiLine = False

            .Col = ColOutWardName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = MainClass.SetMaxLength("Item_Short_Desc", "INV_ITEM_MST", PubDBCn)
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 25)
            If VB.Left(lblBookType.Text, 1) = "J" Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If
            .TypeEditMultiLine = False

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsPODetail.Fields("ITEM_CODE").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 8)

            'If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
            If VB.Left(lblBookType.Text, 1) = "R" Then ''VB.Left(lblBookType.Text, 1) = "W" Or
                .ColHidden = True
            Else
                .ColHidden = False
            End If
            'Else
            '    If VB.Left(lblBookType.Text, 1) = "W" Or VB.Left(lblBookType.Text, 1) = "R" Then ''
            '        .ColHidden = True
            '    Else
            '        .ColHidden = False
            '    End If
            'End If

            .TypeEditMultiLine = False

            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = MainClass.SetMaxLength("Item_Short_Desc", "INV_ITEM_MST", PubDBCn)
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            If VB.Right(lblBookType.Text, 1) = "O" Then
                .set_ColWidth(.Col, 22)
            Else
                .set_ColWidth(.Col, 22)
            End If
            'If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
            If VB.Left(lblBookType.Text, 1) = "R" Then ''VB.Left(lblBookType.Text, 1) = "W" Or
                    .ColHidden = True
                Else
                    .ColHidden = False
                End If
                'Else
                '    If VB.Left(lblBookType.Text, 1) = "W" Or VB.Left(lblBookType.Text, 1) = "R" Then ''
                '        .ColHidden = True
                '    Else
                '        .ColHidden = False
                '    End If
                'End If

                .TypeEditMultiLine = False
            .ColsFrozen = ColItemName

            .Col = ColIdenty
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = MainClass.SetMaxLength("Item_Short_Desc", "INV_ITEM_MST", PubDBCn)
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            If VB.Right(lblBookType.Text, 1) = "O" Then
                .set_ColWidth(.Col, 10)
            Else
                .set_ColWidth(.Col, 10)
            End If
            If VB.Left(lblBookType.Text, 1) = "W" Or VB.Left(lblBookType.Text, 1) = "R" Then
                .ColHidden = True
            Else
                .ColHidden = False
            End If

            .Col = ColItemUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ALPHANUMERIC
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = RsPODetail.Fields("ITEM_UOM").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(.Col, 4)
            If VB.Left(lblBookType.Text, 1) = "R" Then ''Left(lblBookType.text, 1) = "W" Or			
                .ColHidden = True
            Else
                .ColHidden = False
            End If

            .Col = ColLastPORate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMax = CDbl("999999.99")
            .TypeFloatMin = CDbl("-999999.99")
            .set_ColWidth(ColLastPORate, 6)
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC


            .Col = ColQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
                .TypeFloatMax = CDbl("999999999.9999")
                .TypeFloatMin = CDbl("-999999999.9999")
                .TypeFloatDecimalPlaces = 4
            Else
                .TypeFloatMax = CDbl("999999999.99")
                .TypeFloatMin = CDbl("-999999999.99")
                .TypeFloatDecimalPlaces = 3
            End If

            .TypeEditLen = RsPODetail.Fields("ITEM_QTY").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColQty, 8)
            If VB.Right(lblBookType.Text, 1) = "O" Then
                .ColHidden = True
            Else
                .ColHidden = False
            End If


            .Col = ColRMRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeFloatDecimalPlaces = 4
            .TypeEditLen = RsPODetail.Fields("RM_ITEM_RATE").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 9)
            .ColHidden = IIf(lblRMPO.Text = "R", False, True)

            .Col = ColRMDRWRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeFloatDecimalPlaces = 4
            .TypeEditLen = RsPODetail.Fields("RM_DRAWING_RATE").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 9)
            .ColHidden = IIf(lblRMPO.Text = "R", False, True)

            .Col = ColItemRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeFloatDecimalPlaces = 4
            .TypeEditLen = RsPODetail.Fields("ITEM_PRICE").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 9)

            .Col = ColQtyInKgs
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsPODetail.Fields("ITEM_QTY_IN_KGS").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColQtyInKgs, 7)
            'If VB.Right(lblBookType.Text, 1) = "O" Then
            .ColHidden = True
            'Else
            '    .ColHidden = False
            'End If

            .Col = ColRateInKgs
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeFloatDecimalPlaces = 4
            .TypeEditLen = RsPODetail.Fields("ITEM_PRICE_IN_KGS").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 9)
            .ColHidden = True

            .Col = ColItemDisc
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99.999")
            .TypeFloatMin = CDbl("-99.999")
            .TypeFloatDecimalPlaces = 4
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColItemDisc, 7)

            .Col = ColGross
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeFloatDecimalPlaces = 4
            .TypeEditLen = RsPODetail.Fields("GROSS_AMT").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 10)
            If VB.Right(lblBookType.Text, 1) = "O" Then
                .ColHidden = True
            Else
                .ColHidden = False
            End If

            .Col = ColGross_Prev
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeFloatDecimalPlaces = 4
            .TypeEditLen = RsPODetail.Fields("GROSS_AMT").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 9)
            .ColHidden = True

            .Col = ColPO_WEF
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeDateFormat = SS_CELL_DATE_FORMAT_DDMMYY
            .set_ColWidth(.Col, 8)
            .ColHidden = IIf(VB.Right(lblBookType.Text, 1) = "O" Or lblBookType.Text = "JC", False, True)

            ''Or (lblBookType.text = "JC" And mQty = 0)			

            .Col = ColPrevPO_WEF
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeDateFormat = SS_CELL_DATE_FORMAT_DDMMYY
            .set_ColWidth(.Col, 8)
            .ColHidden = True

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsPODetail.Fields("REMARKS").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            '        If Right(lblBookType.text, 1) = "O" Then			
            '            .ColWidth(.Col) = 18			
            '        Else			
            .set_ColWidth(.Col, 15)
            '        End If			



            .Col = ColAssetsNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsPODetail.Fields("ASSETS_NO").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            '        If Right(lblBookType.text, 1) = "O" Then			
            '            .ColWidth(.Col) = 18			
            '        Else			
            .set_ColWidth(.Col, 20)

            .Col = ColQtyRecd
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsPODetail.Fields("ITEM_RECD_QTY").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 7)
            '        If Right(lblBookType.text, 1) = "O" Then			
            .ColHidden = True
            '        Else			
            '            .ColHidden = False			
            '        End If			

            .Col = ColIsTentativeRate
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(.Col, 5)
            '        If Right(lblBookType.text, 1) = "O" Then			
            .ColHidden = False
            '        Else			
            '            .ColHidden = False			
            '        End If			

            .Col = ColFreightCost
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeFloatDecimalPlaces = 3
            .TypeEditLen = RsPODetail.Fields("FREIGHT_COST").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 7)

            .Col = ColVolumeDiscount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeFloatDecimalPlaces = 3
            .TypeEditLen = RsPODetail.Fields("VOLUME_DISCOUNT").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 7)

            .Col = ColCGSTPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMin = CDbl("-99.99")
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColCGSTPer, 5)

            .Col = ColSGSTPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMin = CDbl("-99.99")
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColSGSTPer, 5)

            .Col = ColIGSTPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMin = CDbl("-99.99")
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColIGSTPer, 5)

            .Col = ColCGSTAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeFloatDecimalPlaces = 3
            .TypeEditLen = RsPODetail.Fields("CGST_AMOUNT").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColCGSTAmount, 7)

            .Col = ColSGSTAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeFloatDecimalPlaces = 3
            .TypeEditLen = RsPODetail.Fields("SGST_AMOUNT").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColSGSTAmount, 7)

            .Col = ColIGSTAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeFloatDecimalPlaces = 3
            .TypeEditLen = RsPODetail.Fields("SGST_AMOUNT").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColIGSTAmount, 7)

            .Col = ColLandedCost
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeFloatDecimalPlaces = 3
            .TypeEditLen = RsPODetail.Fields("ITEM_LANDED_COST").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColLandedCost, 7)
            .ColHidden = True

            .Col = ColAcctPostName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = MainClass.SetMaxLength("NAME", "FIN_INVTYPE_MST", PubDBCn)
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColAcctPostName, 20)

            .Col = ColStatus
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(.Col, 5)
            '        If Right(lblBookType.text, 1) = "O" Then			
            .ColHidden = False
            '        Else			
            '            .ColHidden = False			
            '        End If			

            .Col = ColPrintStatus
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(.Col, 8)
            '        .Value = vbChecked			
            .ColHidden = False


            .Col = ColReprocess
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(.Col, 8)
            '        .Value = vbChecked			
            .ColHidden = IIf(VB.Left(lblBookType.Text, 1) = "J", False, True)

            If VB.Left(lblBookType.Text, 1) = "W" Then

                'If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
                MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemName, ColItemName)
                    MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColIdenty, ColItemUOM)
                    'Else
                    '    MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemName, ColItemName)
                    '    MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColIdenty, ColIdenty)
                    'End If
                Else
                MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemName, ColItemName)
                MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColIdenty, ColItemUOM)
            End If
            '18/09/2024
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColLastPORate, ColLastPORate)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColStatus, ColQtyRecd)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColGross, ColGross)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColGross_Prev, ColGross_Prev)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPrevPO_WEF, ColPrevPO_WEF)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColCGSTPer, ColLandedCost)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPrintStatus, ColPrintStatus)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColOutWardName, ColOutWardName)

            If lblRMPO.Text = "R" Then
                MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColRMDRWRate, ColItemRate)
            End If

            MainClass.SetSpreadColor(SprdMain, Arow)

            Call SetCurrency()
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume			
    End Sub

    Private Sub FormatSprdAnnex(ByRef Arow As Integer)

        On Error GoTo ERR1

        With SprdAnnex
            .set_RowHeight(-1, ConRowHeight * 2.5)
            .Row = Arow

            .Col = ColAnnexDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsPOAnnex.Fields("DESCRIPTION").DefinedSize
            .TypeEditMultiLine = True
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE

            MainClass.UnProtectCell(SprdAnnex, 1, SprdMain.MaxRows, ColAnnexDesc, ColAnnexDesc)
            MainClass.SetSpreadColor(SprdAnnex, Arow)
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
        '    .set_ColWidth(1, 1400)
        '    .set_ColWidth(2, 1200)
        '    .set_ColWidth(3, 1000)
        '    .set_ColWidth(4, 800)
        '    .set_ColWidth(5, 1000)
        '    .set_ColWidth(6, 1000)
        '    .set_ColWidth(7, 3500)
        '    .set_ColWidth(8, 800)
        '    .set_ColWidth(9, 2000)
        '    .set_ColWidth(10, 2000)
        '    .set_ColWidth(11, 2000)
        '    .set_ColWidth(12, 1200)
        '    .ColsFrozen = 2

        '    .Col = 1
        '    .ColHidden = True

        '    MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
        '    MainClass.SetSpreadColor(SprdView, -1)
        '    .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle			
        '    MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        'End With
    End Sub
    Private Sub SetTextLengths()

        On Error GoTo ERR1

        txtPONo.MaxLength = RsPOMain.Fields("AUTO_KEY_PO").Precision
        txtPODate.MaxLength = RsPOMain.Fields("PUR_ORD_DATE").DefinedSize - 6
        txtRemarks.MaxLength = RsPOMain.Fields("REMARKS").DefinedSize
        txtOldERPNo.MaxLength = RsPOMain.Fields("NAV_PO_NO").DefinedSize


        txtAmendNo.MaxLength = RsPOMain.Fields("AMEND_NO").Precision
        txtAmendDate.MaxLength = RsPOMain.Fields("AMEND_DATE").DefinedSize - 6
        txtWEF.MaxLength = RsPOMain.Fields("AMEND_WEF_DATE").DefinedSize - 6
        txtPaymentDays.MaxLength = RsPOMain.Fields("PAYDAYS").Precision

        TxtExchangeRate.MaxLength = RsPOMain.Fields("ExchangeRate").Precision

        txtSupplierName.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
        txtShippedTo.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
        txtCode.MaxLength = RsPOMain.Fields("SUPP_CUST_CODE").DefinedSize
        txtOwner.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)

        txtDivision.MaxLength = RsPOMain.Fields("DIV_CODE").DefinedSize


        txtExcise.MaxLength = RsPOMain.Fields("EXCISE_OTHERS").DefinedSize
        txtDespMode.MaxLength = RsPOMain.Fields("MODE_DESPATCH").DefinedSize
        txtPacking.MaxLength = RsPOMain.Fields("PACKING_FORWARDING").DefinedSize

        txtPayment.MaxLength = RsPOMain.Fields("PAYMENT_CODE").DefinedSize
        txtDelivery.MaxLength = RsPOMain.Fields("DELIVERY").DefinedSize
        txtInspection.MaxLength = RsPOMain.Fields("INSPECTION").DefinedSize
        txtInsurance.MaxLength = RsPOMain.Fields("INSURANCE").DefinedSize
        txtOthCond2.MaxLength = RsPOMain.Fields("OTHERS_COND2").DefinedSize

        txtAnnexTitle.MaxLength = RsPOAnnex.Fields("ANNEX_TITLE").DefinedSize

        txtServProvided.MaxLength = MainClass.SetMaxLength("HSN_DESC", "GEN_HSN_MST", PubDBCn)

        txtBillTo.MaxLength = RsPOMain.Fields("BILL_TO_LOC_ID").DefinedSize
        TxtShipTo.MaxLength = RsPOMain.Fields("SHIP_TO_LOC_ID").DefinedSize

        txtDeliveryTo.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
        TxtDeliveryToLoc.MaxLength = RsPOMain.Fields("DELIVERY_TO_LOC_ID").DefinedSize

        txtRMDesc.MaxLength = RsPOMain.Fields("RM_DESC").DefinedSize
        txtRMQty.MaxLength = RsPOMain.Fields("RM_QTY").Precision
        txtRMRate.MaxLength = RsPOMain.Fields("RM_RATE").Precision


        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume			
    End Sub

    Private Function FieldsVarification() As Boolean

        On Error GoTo err_Renamed
        Dim mItemCode As String
        Dim OutwardItemCode As String = ""
        Dim mProductType As String
        Dim mQty As Double
        Dim pProdType As String
        Dim mPOWEFCheck As String
        Dim mPOWEF As String
        Dim mCheckPOWEF As Boolean
        Dim mSaveRights As String
        Dim mItemRate As Double
        Dim mItemDisc As Double

        Dim pPervRate As Double
        Dim pCurrRate As Double
        Dim mPrice As Double
        Dim mDisc As Double

        Dim I As Integer
        Dim mIsApproved As String
        Dim pPONO As Double
        Dim mItemCategory As String
        Dim mItemUOM As String = ""
        Dim mItemStock As Double
        Dim mIsCapitalCheck As String
        Dim mIsItemCapital As String
        Dim mAcctPostName As String
        Dim mFirstAcctPostName As String
        Dim pISGSTRegd As String
        Dim mLocal As String
        Dim mPartyGSTNo As String
        Dim mHSNCode As String
        Dim mSAC As String
        'Dim mServCode As String			
        Dim pCGSTPer As Double
        Dim pSGSTPer As Double
        Dim pIGSTPer As Double
        Dim mGSTClass As String = ""
        Dim mWithInCountry As String
        Dim SqlStr As String = ""

        Dim mItemWEF As String
        Dim mPORate As Double
        Dim mNetCost As Double
        Dim mWef As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mPODetails As String = ""
        Dim mItemQty As Double
        Dim mPrevItemQty As Double
        Dim mShipToCode As String = ""

        Dim mShipQty As Double
        Dim mShipRate As Double
        Dim mCostingReq As Boolean
        Dim mFileSize As Double

        FieldsVarification = True

        'If CDate(txtWEF.Text) < CDate(PubGSTApplicableDate) Then
        '    MsgBox("Now GST Applicable, So cann't be Save in Old Format.", MsgBoxStyle.Information)
        '    FieldsVarification = False
        '    Exit Function
        'End If


        If MainClass.GetUserCanModify((txtAmendDate.Text)) = False Then
            MsgBox("You Have Not Rights to change back P.O.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        If ValidateBranchLocking((txtAmendDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If

        If ValidateBookLocking(PubDBCn, CInt(ConLockPO), txtAmendDate.Text) = True Then
            FieldsVarification = False
            Exit Function
        End If

        If ValidateAccountLocking(PubDBCn, (txtAmendDate.Text), (txtSupplierName.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If

        If MODIFYMode = True And chkStatus.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Posted PO Cann't be Modified")
            FieldsVarification = False
            Exit Function
        End If

        '    If RsCompany!PO_LOCK = "N" Then			
        If chkPrintApp.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Print Approval is done, So PO Cann't be Modified")
            FieldsVarification = False
            Exit Function
        End If
        '    End If			

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsPOMain.EOF = True Then Exit Function

        If MODIFYMode = True And Trim(txtPONo.Text) = "" Then
            MsgInformation("PO No. is Blank")
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
                If txtPODate.Enabled = True Then txtPODate.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If Trim(txtAmendDate.Text) <> "" Then
            If IsDate(txtAmendDate.Text) = False Then
                MsgInformation(" Invalid PO Amend Date. Cannot Save")
                If txtAmendDate.Enabled = True Then txtAmendDate.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If Trim(txtWEF.Text) <> "" Then
            If IsDate(txtWEF.Text) = False Then
                MsgInformation(" Invalid PO Date. Cannot Save")
                txtPODate.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If CDate(txtPODate.Text) > CDate(txtAmendDate.Text) Then
            MsgInformation(" Amend Date Cann't be less than PO Date. Cannot Save")
            FieldsVarification = False
            Exit Function
        End If
        '    If CVDate(txtAmendDate.Text) > CVDate(txtWEF.Text) Then			
        '        MsgInformation " WEF Date Cann't be less than Amend Date. Cannot Save"			
        '        FieldsVarification = False			
        '        Exit Function			
        '    End If			

        If Trim(txtSupplierName.Text) = "" Then
            MsgInformation("Supplier Name is Blank. Cannot Save")
            If txtSupplierName.Enabled = True Then txtSupplierName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtDivision.Text) = "" Then
            MsgInformation("Division is Blank. Cannot Save")
            If txtDivision.Enabled = True Then txtDivision.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable((lblDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invalid txtDivision Name. Cannot Save")
            If txtDivision.Enabled = True Then txtDivision.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtInspection.Text) = "" Then
            TabMain.SelectedIndex = 1
            MsgInformation("Inspection is Blank. Cannot Save")
            If txtInspection.Enabled = True Then txtInspection.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable((txtSupplierName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtCode.Text = MasterNo
        Else
            MsgInformation("Invalid Supplier Name. Cannot Save")
            If txtSupplierName.Enabled = True Then txtSupplierName.Focus()
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


        If MainClass.ValidateWithMasterTable((txtSupplierName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = False Then
            MsgInformation("Supplier Name is not a Supplier or Customer Category. Cannot Save")
            If txtSupplierName.Enabled = True Then txtSupplierName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable((txtSupplierName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='C'") = True Then
            MsgInformation("Supplier Account is Closed. Cannot Save")
            If txtSupplierName.Enabled = True Then txtSupplierName.Focus()
            FieldsVarification = False
            Exit Function
        End If


        If MainClass.ValidateWithMasterTable((txtSupplierName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STOP_PO='Y'") = True Then
            MsgBox("PO Lock for Such Supplier, So cann't be saved", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtSupplierName.Enabled = True Then txtSupplierName.Focus()
            Exit Function
        End If


        pISGSTRegd = "N"
        If MainClass.ValidateWithMasterTable((txtSupplierName.Text), "SUPP_CUST_NAME", "GST_REGD", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            pISGSTRegd = MasterNo
        End If

        If cboGSTStatus.SelectedIndex = -1 Then
            MsgBox("Please select GST Status", MsgBoxStyle.Information)
            If cboGSTStatus.Enabled = True Then cboGSTStatus.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If RsCompany.Fields("DIV_AS_LOCATION").Value = "N" Then
            If pISGSTRegd = "Y" And VB.Left(cboGSTStatus.Text, 1) = "R" Then
                MsgBox("Supplier is registered, please select the GST Refund.", MsgBoxStyle.Information)
                ' txtSupplier.SetFocus			
                FieldsVarification = False
                Exit Function
            End If

            If VB.Left(cboGSTStatus.Text, 1) <> "N" Then
                If pISGSTRegd = "N" And VB.Left(cboGSTStatus.Text, 1) <> "R" Then
                    MsgBox("Supplier is not registered, please select the Reverse Charge.", MsgBoxStyle.Information)
                    ' txtSupplier.SetFocus			
                    FieldsVarification = False
                    Exit Function
                End If
            End If

            If pISGSTRegd = "E" And VB.Left(cboGSTStatus.Text, 1) <> "E" Then
                MsgBox("GST Exempted Supplier, please select the GST Exempted.", MsgBoxStyle.Information)
                ' txtSupplier.SetFocus			
                FieldsVarification = False
                Exit Function
            End If

            If pISGSTRegd = "C" And VB.Left(cboGSTStatus.Text, 1) <> "C" Then
                MsgBox("Supplier is Composit Dealer, please select the Composit.", MsgBoxStyle.Information)
                ' txtSupplier.SetFocus			
                FieldsVarification = False
                Exit Function
            ElseIf pISGSTRegd <> "C" And VB.Left(cboGSTStatus.Text, 1) = "C" Then
                MsgBox("Supplier is not a Composit Dealer, please unselect the Composit.", MsgBoxStyle.Information)
                ' txtSupplier.SetFocus			
                FieldsVarification = False
                Exit Function
            End If

            mWithInCountry = "Y"
            If MainClass.ValidateWithMasterTable((txtSupplierName.Text), "SUPP_CUST_NAME", "WITHIN_COUNTRY", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mWithInCountry = MasterNo
            End If


            If pISGSTRegd <> "Y" And VB.Right(lblBookType.Text, 1) = "O" Then
                MsgInformation("Supplier is not Registered in GST, So Cann't be Prepare Open Purchase Order for such Supplier.")
                FieldsVarification = False
                Exit Function
            End If
        End If

        If chkShipTo.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtShippedTo.Text) = "" Then
                MsgInformation("Please Select Shipped To Supplier Name. Cannot Save")
                FieldsVarification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((txtShippedTo.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invalid Shipped To Supplier Name. Cannot Save")
                If txtShippedTo.Enabled = True Then txtShippedTo.Focus()
                FieldsVarification = False
                Exit Function
            Else
                mShipToCode = MasterNo
            End If
            If Trim(TxtShipTo.Text) = "" Then
                MsgInformation("Ship To is blank. Cannot Save")
                TxtShipTo.Focus()
                FieldsVarification = False
                Exit Function
            Else
                If MainClass.ValidateWithMasterTable(TxtShipTo.Text, "LOCATION_ID", "LOCATION_ID", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mShipToCode) & "'") = False Then
                    MsgBox("Invalid Location Id for such Customer.", MsgBoxStyle.Information)
                    TxtShipTo.Focus()
                    FieldsVarification = False
                End If
            End If
        End If

        If txtDeliveryTo.Text = "" Then
            TxtDeliveryToLoc.Text = ""
        Else
            Dim mDeliveryToCode As String
            If MainClass.ValidateWithMasterTable((txtDeliveryTo.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invalid Delivery To Supplier Name. Cannot Save")
                If txtDeliveryTo.Enabled = True Then txtDeliveryTo.Focus()
                FieldsVarification = False
                Exit Function
            Else
                mDeliveryToCode = MasterNo
            End If
            If Trim(TxtDeliveryToLoc.Text) = "" Then
                MsgInformation("Delivery To is blank. Cannot Save")
                TxtDeliveryToLoc.Focus()
                FieldsVarification = False
                Exit Function
            Else
                If MainClass.ValidateWithMasterTable(TxtDeliveryToLoc.Text, "LOCATION_ID", "LOCATION_ID", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mDeliveryToCode) & "'") = False Then
                    MsgBox("Invalid Delivery Location Id for such Customer.", MsgBoxStyle.Information)
                    TxtDeliveryToLoc.Focus()
                    FieldsVarification = False
                End If
            End If
        End If

        mLocal = "N"
        mPartyGSTNo = ""
        mLocal = GetPartyBusinessDetail(Trim(txtCode.Text), Trim(txtBillTo.Text), "WITHIN_STATE")
        mPartyGSTNo = GetPartyBusinessDetail(Trim(txtCode.Text), Trim(txtBillTo.Text), "GST_RGN_NO")

        'mLocal = "N"
        'If MainClass.ValidateWithMasterTable(Trim(txtCode.Text), "SUPP_CUST_CODE", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mLocal = MasterNo
        'End If

        'mPartyGSTNo = ""
        'If MainClass.ValidateWithMasterTable(Trim(txtCode.Text), "SUPP_CUST_CODE", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mPartyGSTNo = MasterNo
        'End If


        If lblBookType.Text = "LC" Then
            TabMain.SelectedIndex = 3
            If Trim(txtOwner.Text) = "" Then
                MsgInformation("Please select Owner Name. Cannot Save")
                If txtOwner.Enabled = True Then txtOwner.Focus()
                FieldsVarification = False
                Exit Function
            End If

            If MainClass.ValidateWithMasterTable((txtOwner.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invalid Owner Name. Cannot Save")
                If txtOwner.Enabled = True Then txtOwner.Focus()
                FieldsVarification = False
                Exit Function
            End If

            If optPostingDetails(0).Checked = False And optPostingDetails(1).Checked = False And optPostingDetails(2).Checked = False Then
                MsgInformation("Please Select Posting Details. Cannot Save")
                FieldsVarification = False
                Exit Function
            End If
        End If

        '    If PubSuperUser = "U" Then			
        If Val(txtPONo.Text) <> 0 Then
            pPONO = CDbl(Mid(txtPONo.Text, 1, Len(txtPONo.Text) - 6))
        End If
        '    End If			

        mIsApproved = "N"
        'If VB.Right(lblBookType.Text, 1) = "O" Then
        '    If MainClass.ValidateWithMasterTable((txtCode.Text), "SUPP_CUST_CODE", "IS_APPROVED", "FIN_SUPP_CUST_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mIsApproved = IIf(IsDBNull(MasterNo) Or MasterNo = "N", "N", "Y")
        '    End If
        '    If mIsApproved = "N" Then
        '        MsgInformation("Vendor is not Approved so cann't be generate Open Order. ")
        '        '            If txtSupplierName.Enabled = True Then txtSupplierName.SetFocus			
        '        FieldsVarification = False
        '        Exit Function
        '    End If
        'End If

        If VB.Right(lblBookType.Text, 1) = "O" Then
            If CheckPreviousPOExists((txtCode.Text), Trim(txtPONo.Text), mShipToCode, "O") = True Then
                FieldsVarification = False
                Exit Function
            End If
        ElseIf VB.Right(lblBookType.Text, 1) = "C" Then
            If CheckPreviousPOExists((txtCode.Text), Trim(txtPONo.Text), mShipToCode, "O") = True Then
                FieldsVarification = False
                Exit Function
            End If

            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 And mAuthorised = False Then
                If CheckPreviousPOExists((txtCode.Text), Trim(txtPONo.Text), mShipToCode, "C") = True Then
                    FieldsVarification = False
                    Exit Function
                End If
            End If

        ElseIf VB.Right(lblBookType.Text, 1) = "J" Then
            If CheckPreviousPOExists((txtCode.Text), Trim(txtPONo.Text), mShipToCode, "C") = True Then
                FieldsVarification = False
                Exit Function
            End If
        End If

        If Trim(txtPayment.Text) = "" Then
            MsgInformation("Payment Terms Cann't be Blank.")
            FieldsVarification = False
            Exit Function
        End If

        Dim mIsMSMESupplier As String
        Dim xSqlStr As String
        Dim mErrorMsg As String

        If MainClass.ValidateWithMasterTable(txtSupplierName.Text, "SUPP_CUST_NAME", "SME_REGD", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SME_REGD='Y'") = True Then
            mIsMSMESupplier = "Y"
            mErrorMsg = "Invalid Payment Code for MSME Supplier"
        Else
            mIsMSMESupplier = "N"
            mErrorMsg = "Invalid Payment Code"
        End If

        xSqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If mIsMSMESupplier = "Y" Then
            xSqlStr = xSqlStr & " AND FOR_MSME='Y'"
        End If

        If MainClass.ValidateWithMasterTable((txtPayment.Text), "PAY_TERM_CODE", "PAY_TERM_DESC", "FIN_PAYTERM_MST", PubDBCn, MasterNo, , xSqlStr) = True Then
            lblPaymentTerms.Text = MasterNo
        Else
            MsgBox(mErrorMsg, MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        If CheckPreviousMonthPendingOrder(mPODetails) = True Then
            MsgInformation("Please Closed or Approve Last Month Purchase / Service Purchase Order, Pending PO/s : " & mPODetails)
            FieldsVarification = False
            Exit Function
        End If

        If VB.Left(lblBookType.Text, 1) = "W" Then
            If MainClass.ValidDataInGrid(SprdMain, ColWoDesc, "S", "Please Check Service Purchase Order Description.") = False Then FieldsVarification = False : Exit Function

            'If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then

            If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "Please Check Item Code.") = False Then FieldsVarification = False : Exit Function
                If MainClass.ValidDataInGrid(SprdMain, ColItemName, "S", "Please Check Item Description.") = False Then FieldsVarification = False : Exit Function
                'End If
                If MainClass.ValidDataInGrid(SprdMain, ColItemUOM, "S", "Please Check Unit.") = False Then FieldsVarification = False : Exit Function

        ElseIf VB.Left(lblBookType.Text, 1) = "R" Then
            If MainClass.ValidDataInGrid(SprdMain, ColWoDesc, "S", "Please Check Project Order Description.") = False Then FieldsVarification = False : Exit Function
        Else
            If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "Please Check Item Code.") = False Then FieldsVarification = False : Exit Function
            If MainClass.ValidDataInGrid(SprdMain, ColItemName, "S", "Please Check Item Description.") = False Then FieldsVarification = False : Exit Function
            If MainClass.ValidDataInGrid(SprdMain, ColItemUOM, "S", "Please Check Unit.") = False Then FieldsVarification = False : Exit Function

            '        If CheckItemRateFromCosting = False Then			
            '            FieldsVarification = False			
            '            Exit Function			
            '        End If			
        End If



        ''If Left(cboGSTStatus.Text, 1) <> "E" Then			

        mIsCapitalCheck = IIf(chkCapital.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")


        SprdMain.Row = 1
        SprdMain.Col = ColAcctPostName
        mFirstAcctPostName = Trim(UCase(SprdMain.Text))
        Dim xWoDesc As String = ""
        Dim mPOItemRate As Double
        Dim mPOItemDisc As Double
        Dim mItemMRP As Double
        Dim mItemMRPDisc As Double


        For I = 1 To SprdMain.MaxRows - 1
            SprdMain.Row = I
            SprdMain.Col = ColQty
            mQty = Val(SprdMain.Text)


            SprdMain.Col = ColItemCode
            mItemCode = Trim(UCase(SprdMain.Text))

            SprdMain.Col = ColItemRate
            mPOItemRate = Val(SprdMain.Text)

            SprdMain.Col = ColItemDisc
            mPOItemDisc = Val(SprdMain.Text)

            mPOItemRate = mPOItemRate - (mPOItemRate * mPOItemDisc * 0.01)

            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 116 Then
                mItemMRP = GetMRPRate(txtWEF.Text, "RATE", mItemCode, "L")
                mItemMRPDisc = GetMRPRate(txtWEF.Text, "RATE_DISC", mItemCode, "L")
                mItemMRP = mItemMRP - (mItemMRP * mItemMRPDisc * 0.01)

                If mItemMRP > 0 Then
                    If mPOItemRate > mItemMRP Then
                        MsgInformation("Item PO rate Cann't be Greater Than MRP rate.")
                        MainClass.SetFocusToCell(SprdMain, I, ColItemCode)
                        FieldsVarification = False
                        Exit Function
                    End If
                End If
            End If

            If VB.Left(lblBookType.Text, 1) = "W" Then

                If mItemCode <> "" Then
                    If CheckDuplicateItem(mItemCode) = True Then
                        MainClass.SetFocusToCell(SprdMain, I, ColItemCode)
                        FieldsVarification = False
                        Exit Function
                    End If
                End If

                SprdMain.Col = ColWoDesc
                xWoDesc = SprdMain.Text
                If xWoDesc <> "" Then
                    If CheckDuplicateItemDesc(xWoDesc) = True Then
                        MainClass.SetFocusToCell(SprdMain, I, ColWoDesc)
                        FieldsVarification = False
                        Exit Function
                    End If
                End If

                If mItemCode <> "" Then
                    pProdType = GetProductionType(mItemCode)
                    If pProdType = "S" Then

                    Else
                        If mQty <= 0 Then
                            MsgInformation("Please Enter The Qty.")
                            MainClass.SetFocusToCell(SprdMain, I, ColQty)
                            FieldsVarification = False
                            Exit Function

                        End If
                    End If
                End If

            Else
                If mItemCode <> "" Then
                    If CheckDuplicateItem(mItemCode) = True Then
                        MainClass.SetFocusToCell(SprdMain, I, ColItemCode)
                        FieldsVarification = False
                        Exit Function
                    End If
                End If
            End If

            SprdMain.Col = ColItemCode
            mProductType = GetProductionType(mItemCode)

            SprdMain.Row = I
            SprdMain.Col = ColAssetsNo
            If mIsCapitalCheck = "Y" And RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 And Trim(SprdMain.Text) = "" Then
                MsgInformation("Please Enter the Assets No. Cannot Save")
                MainClass.SetFocusToCell(SprdMain, I, ColAssetsNo)
                FieldsVarification = False
                Exit Function
            End If

            SprdMain.Col = ColItemCode
            'If chkShipTo.CheckState = System.Windows.Forms.CheckState.Unchecked And (mProductType = "R" Or mProductType = "D" Or mProductType = "3") Then
            '    SprdMain.Col = ColItemUOM
            '    If Trim(SprdMain.Text) = "KGS" Or Trim(SprdMain.Text) = "TON" Or Trim(SprdMain.Text) = "MT" Then
            '        If Trim(SprdMain.Text) = "KGS" Then
            '            SprdMain.Row = I
            '            SprdMain.Col = ColQty
            '            mShipQty = Val(SprdMain.Text)

            '            SprdMain.Col = ColItemRate
            '            mShipRate = Val(SprdMain.Text)

            '            SprdMain.Col = ColQtyInKgs
            '            SprdMain.Text = VB6.Format(mShipQty, "0.000")

            '            SprdMain.Col = ColRateInKgs
            '            SprdMain.Text = VB6.Format(mShipRate, "0.0000")


            '        ElseIf Trim(SprdMain.Text) = "TON" Or Trim(SprdMain.Text) = "MT" Then
            '            SprdMain.Row = I
            '            SprdMain.Col = ColQty
            '            mShipQty = Val(SprdMain.Text)

            '            SprdMain.Col = ColItemRate
            '            mShipRate = Val(SprdMain.Text)

            '            SprdMain.Col = ColQtyInKgs
            '            SprdMain.Text = VB6.Format(mShipQty * 1000, "0.000")

            '            SprdMain.Col = ColRateInKgs
            '            SprdMain.Text = VB6.Format(mShipRate / 1000, "0.0000")
            '        End If
            '    Else
            '        SprdMain.Row = I
            '        SprdMain.Col = ColQty
            '        mShipQty = Val(SprdMain.Text)

            '        SprdMain.Col = ColQtyInKgs

            '        If mShipQty > 0 And Val(SprdMain.Text) = 0 Then
            '            MsgBox("Please enter the Qty in KGS.", MsgBoxStyle.Information)
            '            FieldsVarification = False
            '            Exit Function
            '        End If

            '        SprdMain.Col = ColItemRate
            '        mShipRate = Val(SprdMain.Text)

            '        SprdMain.Col = ColRateInKgs
            '        If mShipRate > 0 And Val(SprdMain.Text) = 0 Then
            '            MsgBox("Please enter the Rate in KGS.", MsgBoxStyle.Information)
            '            FieldsVarification = False
            '            Exit Function
            '        End If
            '    End If
            'End If
            If chkDevelopment.CheckState = System.Windows.Forms.CheckState.Checked Then
                If VB.Right(lblBookType.Text, 1) = "O" Then
                    MsgBox("Only Closed Order will be Generate in Development PO.", MsgBoxStyle.Information)
                    FieldsVarification = False
                    Exit Function
                End If

                If mProductType <> "D" Then
                    MsgBox("Please Select the Development Category Item Only.", MsgBoxStyle.Information)
                    FieldsVarification = False
                    Exit Function
                End If
            Else
                If mProductType = "D" Then
                    MsgBox("Can't be Select Development Category Item in Regular PO.", MsgBoxStyle.Information)
                    FieldsVarification = False
                    Exit Function
                End If
            End If

            '        If PubUserID = "G0416" Or PubUserID = "000617" Then			
            '        Else			
            If mProductType = "D" Then
                SprdMain.Row = I
                SprdMain.Col = ColQty
                mItemQty = Val(SprdMain.Text)
                If Val(SprdMain.Text) > 500 Then
                    MsgBox("Qty cann't be more than 500 units for Development items.", MsgBoxStyle.Information)
                    FieldsVarification = False
                    Exit Function
                End If

                mPrevItemQty = mItemQty + GetItemQty(mItemCode)
                If Val(CStr(mPrevItemQty)) > 500 Then
                    MsgBox("Qty cann't be more than 500 units for Development items.", MsgBoxStyle.Information)
                    FieldsVarification = False
                    Exit Function
                End If
            End If


            If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS='I'") = True Then
                MsgInformation("Item Status is Closed, So cann't be Saved. [" & mItemCode & "]")
                MainClass.SetFocusToCell(SprdMain, I, ColItemCode)
                FieldsVarification = False
                Exit Function
            End If

            If VB.Left(lblBookType.Text, 1) = "W" Then

            Else

                mGSTClass = "0"
                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "GST_ITEMCLASS", "INV_ITEM_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mGSTClass = MasterNo
                End If

                If RsCompany.Fields("DIV_AS_LOCATION").Value = "N" Then
                    If VB.Left(cboGSTStatus.Text, 1) = "G" And mGSTClass <> "0" Then
                        If mGSTClass = "1" Then
                            MsgInformation("Item is NON GST, So that cann't be select GST Refund.")
                        Else
                            MsgInformation("Item is GST Exempt, So that cann't be select GST Refund.")
                        End If
                        FieldsVarification = False
                        Exit Function
                    End If


                    If (VB.Left(cboGSTStatus.Text, 1) = "N" And mGSTClass <> "1") Or (VB.Left(cboGSTStatus.Text, 1) <> "N" And mGSTClass = "1") Then
                        MsgInformation("Item is NON GST, So that please select NON GST.")
                        FieldsVarification = False
                        Exit Function
                    End If

                    'If mIsCapitalCheck = "Y" And mIsItemCapital = "N" Then
                    '    MsgInformation("Item Category is not Capital of Item Code [" & mItemCode & "]. Please UnClick on Capital.")
                    '    MainClass.SetFocusToCell(SprdMain, I, ColItemCode)
                    '    FieldsVarification = False
                    '    Exit Function
                    'ElseIf mIsCapitalCheck = "N" And mIsItemCapital = "Y" Then
                    '    MsgInformation("Item Category is Capital of Item Code [" & mItemCode & "]. Please Click on Capital.")
                    '    MainClass.SetFocusToCell(SprdMain, I, ColItemCode)
                    '    FieldsVarification = False
                    '    Exit Function
                    'End If
                End If
            End If

            mIsItemCapital = GetProductionType(mItemCode)
            mIsItemCapital = IIf(mIsItemCapital = "A", "Y", "N")

            SprdMain.Row = I
            SprdMain.Col = ColAcctPostName
            If Trim(UCase(SprdMain.Text)) = "" Then
                SprdMain.Text = mFirstAcctPostName
            End If
            mAcctPostName = Trim(UCase(SprdMain.Text))

            If mQty > 0 Then
                If mAcctPostName = "" Then
                    MsgInformation("Account Post Name Cann't be Blank.")
                    MainClass.SetFocusToCell(SprdMain, I, ColAcctPostName)
                    FieldsVarification = False
                    Exit Function
                Else
                    If MainClass.ValidateWithMasterTable(mAcctPostName, "NAME", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P' AND ISFIXASSETS ='" & mIsCapitalCheck & "'") = False Then
                        MsgInformation("Invaild Account Post Name.")
                        MainClass.SetFocusToCell(SprdMain, I, ColAcctPostName)
                        FieldsVarification = False
                        Exit Function
                    End If
                End If
            End If
            pCGSTPer = 0
            pSGSTPer = 0
            pIGSTPer = 0

            If VB.Left(lblBookType.Text, 1) = "W" Or VB.Left(lblBookType.Text, 1) = "J" Then ''Or Left(lblBookType.text, 1) = "R"			
                mSAC = ""
                If MainClass.ValidateWithMasterTable(txtServProvided.Text, "HSN_DESC", "HSN_CODE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
                    mSAC = MasterNo
                End If


                If mSAC = "" Then
                    MsgInformation("Invalid Service Provider.")
                    FieldsVarification = False
                    Exit Function
                Else
                    If GetSACDetails(mSAC, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, mPartyGSTNo, VB.Left(cboGSTStatus.Text, 1)) = False Then GoTo err_Renamed
                End If

                If VB.Left(cboGSTStatus.Text, 1) = "E" Or VB.Left(cboGSTStatus.Text, 1) = "C" Or VB.Left(cboGSTStatus.Text, 1) = "N" Then
                Else
                    If mLocal = "Y" Then
                        If pCGSTPer = 0 Then
                            MsgInformation("CGST % is not Defined for Service Code : " & mSAC)
                            FieldsVarification = False
                            Exit Function
                        End If

                        If pSGSTPer = 0 Then
                            MsgInformation("SGST % is not Defined for Service Code : " & mSAC)
                            FieldsVarification = False
                            Exit Function
                        End If

                    Else
                        If pIGSTPer = 0 Then
                            MsgInformation("IGST % is not Defined for Service Code : " & mSAC)
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If
                End If

                SprdMain.Row = I

                SprdMain.Col = ColHSN
                SprdMain.Text = mSAC

                SprdMain.Col = ColCGSTPer
                SprdMain.Text = VB6.Format(pCGSTPer, "0.00")

                SprdMain.Col = ColSGSTPer
                SprdMain.Text = VB6.Format(pSGSTPer, "0.00")

                SprdMain.Col = ColIGSTPer
                SprdMain.Text = VB6.Format(pIGSTPer, "0.00")
            Else
                SprdMain.Row = I
                SprdMain.Col = ColHSN
                mHSNCode = Trim(UCase(SprdMain.Text))
                If mGSTClass = "0" Then
                    If mHSNCode = "" Then
                        MsgInformation("HSN Cann't be Blank.")
                        FieldsVarification = False
                        Exit Function
                    Else
                        If GetHSNDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, VB.Left(cboGSTStatus.Text, 1), mPartyGSTNo) = False Then GoTo err_Renamed
                    End If

                    If VB.Left(cboGSTStatus.Text, 1) = "E" Or VB.Left(cboGSTStatus.Text, 1) = "C" Or VB.Left(cboGSTStatus.Text, 1) = "N" Then

                    Else
                        If mLocal = "Y" Then
                            If pCGSTPer = 0 Then
                                MsgInformation("CGST % is not Defined for Item Code : " & mItemCode)
                                FieldsVarification = False
                                MainClass.SetFocusToCell(SprdMain, I, ColItemCode)
                                Exit Function
                            End If

                            If pSGSTPer = 0 Then
                                MsgInformation("SGST % is not Defined for Item Code : " & mItemCode)
                                FieldsVarification = False
                                MainClass.SetFocusToCell(SprdMain, I, ColItemCode)
                                Exit Function
                            End If
                        Else
                            If pIGSTPer = 0 Then
                                MsgInformation("IGST % is not Defined for Item Code : " & mItemCode)
                                FieldsVarification = False
                                MainClass.SetFocusToCell(SprdMain, I, ColItemCode)
                                Exit Function
                            End If
                        End If
                    End If
                    '                End If			
                End If

                SprdMain.Row = I
                SprdMain.Col = ColCGSTPer
                SprdMain.Text = VB6.Format(pCGSTPer, "0.00")

                SprdMain.Col = ColSGSTPer
                SprdMain.Text = VB6.Format(pSGSTPer, "0.00")

                SprdMain.Col = ColIGSTPer
                SprdMain.Text = VB6.Format(pIGSTPer, "0.00")

            End If

            'If VB.Left(lblBookType.Text, 1) = "J" Then
            '    SprdMain.Col = ColOutWardCode
            '    OutwardItemCode = Trim(SprdMain.Text)
            '    If OutwardItemCode = "" Then
            '        MsgInformation("Outward Item is must for Jobwork Order")
            '        MainClass.SetFocusToCell(SprdMain, I, ColOutWardCode)
            '        FieldsVarification = False
            '        Exit Function
            '    End If
            '    If MainClass.ValidateWithMasterTable(OutwardItemCode, "ITEM_CODE", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            '        MsgInformation("Outward Item is not Exists or Status is Closed, So cann't be Saved. [" & OutwardItemCode & "]")
            '        MainClass.SetFocusToCell(SprdMain, I, ColOutWardCode)
            '        FieldsVarification = False
            '        Exit Function
            '    End If
            '    SprdMain.Row = I
            '    SprdMain.Col = ColItemCode
            '    mItemCode = Trim(SprdMain.Text)

            '    If ValidateOutwardCode(mItemCode, OutwardItemCode) = False Then
            '        MsgInformation("Not a Valid Outward Item Code : [" & OutwardItemCode & "] for Product Code : " & mItemCode)
            '        MainClass.SetFocusToCell(SprdMain, I, ColOutWardCode)
            '        FieldsVarification = False
            '        Exit Function
            '    End If
            'End If
        Next

        ''24.11.2003			

        If VB.Left(lblBookType.Text, 1) = "P" Or VB.Left(lblBookType.Text, 1) = "J" Then
            For I = 1 To SprdMain.MaxRows - 1
                SprdMain.Row = I
                SprdMain.Col = ColItemCode
                mItemCode = Trim(UCase(SprdMain.Text))

                SprdMain.Col = ColItemUOM
                mItemUOM = Trim(UCase(SprdMain.Text))

                mItemCategory = GetProductionType(mItemCode)

                If VB.Left(lblBookType.Text, 1) = "P" Then
                    If VB.Right(lblBookType.Text, 1) = "O" Then
                        If mItemCategory = "D" Then
                            MsgInformation("Please check Item Category of Item Code - " & mItemCode & ". Item Category is Defined Development BOP/RM. Cann't made Open Order.")
                            MainClass.SetFocusToCell(SprdMain, I, ColItemCode)
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If

                    'If CheckBOMItem(mItemCode) = True Then ''Or mItemCategory = "R"			
                    '    If CheckItemExistsInBOM(mItemCode) = False Then
                    '        MsgInformation("Item Code - " & mItemCode & " is not Defined in any BOM. So Purchase Order not made.")
                    '        MainClass.SetFocusToCell(SprdMain, I, ColItemCode)
                    '        FieldsVarification = False
                    '        Exit Function

                    '    End If
                    'End If
                    If mItemCategory = "D" Then
                        mItemStock = GetBalanceStockQty(mItemCode, VB6.Format(PubCurrDate, "DD/MM/YYYY"), mItemUOM, "STR", "ST", "", ConWH, Val(txtDivision.Text))

                        If mItemStock > 0 Then
                            MsgInformation("Stock is Available for Item Code - " & mItemCode & "(Development Category). So Purchase Order not made.")
                            MainClass.SetFocusToCell(SprdMain, I, ColItemCode)
                            FieldsVarification = False
                            Exit Function
                        End If

                    End If
                Else
                    'If CheckItemConsumptionExists(mItemCode, "") = False Then
                    '    MsgInformation("Item Code - " & mItemCode & " Consumption not Defined. So Jobwork Order not made.")
                    '    MainClass.SetFocusToCell(SprdMain, I, ColItemCode)
                    '    FieldsVarification = False
                    '    Exit Function
                    'End If
                End If
            Next
        End If
        Dim mFileExt As String
        Dim mRateCount As Long = 0
        Dim mQtyAvailable As Boolean = False

        If lblBookType.Text = "PC" Then
            For I = 1 To SprdMain.MaxRows - 1
                SprdMain.Row = I
                SprdMain.Col = ColItemCode
                mItemCode = Trim(UCase(SprdMain.Text))

                SprdMain.Col = ColQty
                mQty = Val(SprdMain.Text)
                If mQty > 0 Then
                    mQtyAvailable = True
                End If

                SprdMain.Col = ColItemRate
                mItemRate = Val(SprdMain.Text)
                mRateCount = mRateCount + IIf(mItemRate > 0, 1, 0)

                SprdMain.Col = ColItemDisc
                mItemDisc = Val(SprdMain.Text)

                If CheckTCRequired(mItemCode) = True Then
                    If chkTCAvailable.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                        If chkApprovedWO_TC.CheckState = System.Windows.Forms.CheckState.Checked And chkTPRAvailable.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                            MsgInformation("If TC is not available than Third Party Report is must.")
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If

                    If chkTCAvailable.CheckState = System.Windows.Forms.CheckState.Checked And txtTCPath.Text = "" Then
                        MsgInformation("Please upload TC.")
                        FieldsVarification = False
                        Exit Function
                    End If
                    If chkTPRAvailable.CheckState = System.Windows.Forms.CheckState.Checked And txtTPRPath.Text = "" Then
                        MsgInformation("Please upload Third Party Report.")
                        FieldsVarification = False
                        Exit Function
                    End If

                    If txtTCPath.Text <> "" Then

                        mFileExt = GetExtensionName((txtTCPath.Text))
                        If UCase(mFileExt) = "PDF" Or UCase(mFileExt) = "JPG" Or UCase(mFileExt) = "BMP" Or UCase(mFileExt) = "PNG" Then

                        Else
                            MsgInformation("Please select only pdf,jpg,bmp & png files.")
                            FieldsVarification = False
                            Exit Function
                        End If

                        If FILEExists((txtTCPath.Text)) Then
                            mFileSize = FileLen(txtTCPath.Text)
                            mFileSize = mFileSize * 0.001
                            If mFileSize > 300 Then
                                MsgInformation("File Size can't be greater than 300kb.")
                                FieldsVarification = False
                                Exit Function
                            End If
                            '                        MsgBox F.Size 'displays size of file			
                        Else
                            MsgInformation("File not Found, Please select the vaild file.")
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If

                    If txtTPRPath.Text <> "" Then

                        mFileExt = GetExtensionName((txtTPRPath.Text))
                        If UCase(mFileExt) = "PDF" Or UCase(mFileExt) = "JPG" Or UCase(mFileExt) = "BMP" Or UCase(mFileExt) = "PNG" Then

                        Else
                            MsgInformation("Please select only pdf,jpg,bmp & png files.")
                            FieldsVarification = False
                            Exit Function
                        End If

                        If FILEExists((txtTPRPath.Text)) Then
                            mFileSize = FileLen(txtTPRPath.Text)
                            mFileSize = mFileSize * 0.001
                            If mFileSize > 300 Then
                                MsgInformation("File Size can't be greater than 300kb.")
                                FieldsVarification = False
                                Exit Function
                            End If
                            '                        MsgBox F.Size 'displays size of file			
                        Else
                            MsgInformation("File not Found, Please select the vaild file.")
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If
                End If



                SprdMain.Col = ColQty
                mQty = Val(SprdMain.Text)


                If CheckIndentItem(mItemCode) = True Then
                    If mQty > 0 Then
                        If IndentDetailExists(mItemCode, I, mQty) = False Then
                            MsgInformation("Please Check Indent Qty.")
                            MainClass.SetFocusToCell(SprdMain, I, ColQty)
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If
                Else
                    If CheckQuotationRequiredItem(mItemCode) = True Then
                        If IndentDetailExists(mItemCode, I, mQty) = False Then
                            MsgInformation("Please Check Indent Qty.")
                            MainClass.SetFocusToCell(SprdMain, I, ColQty)
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If
                End If

                If CheckQuotationRequiredItem(mItemCode) = True Then
                    If QuotationDetailExists(mItemCode, I, mItemRate, mItemDisc) = False Then
                        MsgInformation("Please Check Item Rate with Quotation Rate")
                        MainClass.SetFocusToCell(SprdMain, I, ColItemRate)
                        FieldsVarification = False
                        Exit Function
                    End If
                End If

                ''MAX ORDER QTY.......			
                '            If CheckBOMItem(mItemCode) = True Then			
                '			
                '            End If			

                'If NonApprovedItemExists(mItemCode) = False Then
                '    MsgInformation("Item is Not Approved for last 2 Month. So you cann't raised PO. ")
                '    MainClass.SetFocusToCell(SprdMain, I, ColItemCode)
                '    FieldsVarification = False
                '    Exit Function
                'End If
            Next
            If mRateCount = 0 Then
                MsgInformation("Nothing to Save.")
                FieldsVarification = False
                Exit Function
            End If
            mCheckPOWEF = True
        ElseIf lblBookType.Text = "PO" Or (lblBookType.Text = "JC" And mQty = 0) Then
            If Val(txtAmendNo.Text) <> 0 Then
                mCheckPOWEF = False
                mPOWEF = VB6.Format(txtWEF.Text, "DD/MM/YYYY")
                For I = 1 To SprdMain.MaxRows - 1
                    SprdMain.Row = I


                    SprdMain.Col = ColItemCode
                    mItemCode = Trim(UCase(SprdMain.Text))

                    If CheckTCRequired(mItemCode) = True Then
                        MsgInformation("You can't be made TC Item's Open Order.")
                        FieldsVarification = False
                        Exit Function
                    End If

                    SprdMain.Col = ColPO_WEF
                    SprdMain.Text = VB6.Format(IIf(SprdMain.Text = "", mPOWEF, SprdMain.Text), "DD/MM/YYYY")
                    mPOWEFCheck = VB6.Format(SprdMain.Text, "DD/MM/YYYY")

                    SprdMain.Col = ColItemRate
                    mPrice = Val(SprdMain.Text)

                    SprdMain.Col = ColItemDisc
                    mDisc = Val(SprdMain.Text)

                    pCurrRate = mPrice - System.Math.Round((mPrice * mDisc) / 100, 4)

                    SprdMain.Col = ColGross_Prev
                    pPervRate = Val(SprdMain.Text)


                    mCostingReq = GetCostingRequired(mItemCode)

                    If mCostingReq = False Then
                        If Val(CStr(pCurrRate)) <> Val(CStr(pPervRate)) Then
                            If CDate(mPOWEF) <> CDate(mPOWEFCheck) Then
                                '                        If MsgQuestion("WEF Date is not Match with WEF Date in Detail Part. " & vbCrLf & " Are You want to UpDate All WEF Date in Detail Which Rate has changed Only. ...") = vbNo Then			
                                '                            FieldsVarification = False			
                                '                            Exit Function			
                                '                        Else			
                                Call UpdateWEFInDetail(I)
                                '                        End If			
                            End If
                        End If
                    End If

                    SprdMain.Row = I
                    SprdMain.Col = ColPO_WEF
                    mPOWEFCheck = VB6.Format(SprdMain.Text, "DD/MM/YYYY")

                    If CDate(mPOWEF) = CDate(mPOWEFCheck) Then
                        mCheckPOWEF = True
                    End If
                Next
                If mCheckPOWEF = False Then
                    MsgInformation("Please Check WEF Date in Detail Part.")
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        End If

        If RsCompany.Fields("MAX_PO_ITEMS").Value > 0 And VB.Left(lblBookType.Text, 1) = "P" Then
            If Val(CStr(SprdMain.MaxRows)) - 1 > RsCompany.Fields("MAX_PO_ITEMS").Value Then
                MsgInformation("You cann't be select more than " & Val(RsCompany.Fields("MAX_PO_ITEMS").Value) & " in a PO.")
                FieldsVarification = False
                Exit Function
            End If
        End If

        mCostingReq = False

        If VB.Left(lblBookType.Text, 1) = "P" Then ''RsCompany!PO_LOCK = "Y" And			
            With SprdMain
                For I = 1 To .MaxRows
                    .Row = I
                    .Col = ColItemCode
                    mItemCode = Trim(.Text)

                    mItemCategory = GetProductionType(mItemCode)
                    mCostingReq = GetCostingRequired(mItemCode)

                    If mCostingReq = True Then
                        .Col = ColPO_WEF
                        If lblBookType.Text = "PC" Then
                            mItemWEF = VB6.Format(Trim(txtWEF.Text), "DD/MM/YYYY")
                        Else
                            mItemWEF = VB6.Format(Trim(.Text), "DD/MM/YYYY")
                        End If

                        .Col = ColItemRate
                        mPORate = CDbl(VB6.Format(Val(.Text), "0.0000"))

                        mWef = ""
                        mNetCost = 0

                        ''SqlStr = " SELECT WEF, NET_COST " & vbCrLf _
                        ''    & " FROM PRD_BOP_COST_HDR" & vbCrLf _
                        ''    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        ''    & " AND SUPP_CUST_CODE = '" & MainClass.AllowSingleQuote(txtCode.Text) & "'" & vbCrLf _
                        ''    & " AND ITEM_CODE = '" & MainClass.AllowSingleQuote(mItemCode) & "'" & vbCrLf _
                        ''    & " AND WEF < TO_DATE('" & VB6.Format(mItemWEF, "DD/MM/YYYY") & "','DD-MON-YYYY')"

                        ''MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


                        SqlStr = " SELECT WEF, NET_COST " & vbCrLf _
                            & " FROM PRD_BOP_COST_HDR" & vbCrLf _
                            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                            & " AND SUPP_CUST_CODE = '" & MainClass.AllowSingleQuote(txtCode.Text) & "'" & vbCrLf _
                            & " AND ITEM_CODE = '" & MainClass.AllowSingleQuote(mItemCode) & "'" & vbCrLf _
                            & " AND WEF = TO_DATE('" & VB6.Format(mItemWEF, "DD/MM/YYYY") & "','DD-MON-YYYY')"
                        '
                        '& " AND AMEND_NO = ( " & vbCrLf & " SELECT MAX(AMEND_NO) " & vbCrLf _
                        '& " FROM PRD_BOP_COST_HDR" & vbCrLf _
                        '& " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        '& " AND SUPP_CUST_CODE = '" & MainClass.AllowSingleQuote(txtCode.Text) & "'" & vbCrLf _
                        '& " AND ITEM_CODE = '" & MainClass.AllowSingleQuote(mItemCode) & "')"

                        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                        If RsTemp.EOF = False Then
                            mWef = VB6.Format(IIf(IsDBNull(RsTemp.Fields("WEF").Value), "", RsTemp.Fields("WEF").Value), "DD/MM/YYYY")
                            mNetCost = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("NET_COST").Value), 0, RsTemp.Fields("NET_COST").Value), "0.0000"))
                        End If

                        If mWef = "" Then
                            MsgInformation("Vendor Costing is not update for Item Code : " & mItemCode & ".")
                            FieldsVarification = False
                            Exit Function
                        Else
                            If CDate(mItemWEF) <> CDate(mWef) Then
                                MsgInformation("Please WEF Date is not match with Vendor Costing of item Code : " & mItemCode)
                                FieldsVarification = False
                                Exit Function
                            End If

                            If Val(CStr(mPORate)) <> Val(CStr(mNetCost)) Then
                                MsgInformation("Please Item rate is not match with Vendor Costing. Costing Rate is " & mNetCost & " For Item Code : " & mItemCode & "")
                                FieldsVarification = False
                                Exit Function
                            End If
                        End If
                    End If

                Next
            End With
        End If

        If VB.Left(lblBookType.Text, 1) = "W" Or VB.Left(lblBookType.Text, 1) = "J" Then
            If VB.Left(cboGSTStatus.Text, 1) = "C" Or VB.Left(cboGSTStatus.Text, 1) = "N" Then
            Else
                If Trim(txtServProvided.Text) = "" Then
                    MsgBox("Please Select The Service., So cann't be Saved.", MsgBoxStyle.Information)
                    FieldsVarification = False
                    Exit Function
                End If

                If MainClass.ValidateWithMasterTable(Trim(txtServProvided.Text), "HSN_DESC", "HSN_CODE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = False Then
                    MsgBox("Service Provided is not defined in Master, So cann't be Saved.", MsgBoxStyle.Information)
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        End If

        Call CalcTots()

        If lblRMPO.Text = "R" Then
            If Val(txtRMQty.Text) <> Val(lblTotQty.Text) Then
                MsgBox("Total Qty Should be Match with RM Qty, So cann't be Saved.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
            For I = 1 To SprdMain.MaxRows - 1
                SprdMain.Row = I

                SprdMain.Col = ColItemCode
                mItemCode = Trim(UCase(SprdMain.Text))

                If Trim(mItemCode) <> "" Then
                    SprdMain.Col = ColRMRate
                    If Val(txtRMRate.Text) <> Val(SprdMain.Text) Then
                        MsgBox("Item Rate Should be Match with RM Rate, So cann't be Saved.", MsgBoxStyle.Information)
                        FieldsVarification = False
                        Exit Function
                    End If

                End If
            Next

        End If

        If lblBookType.Text = "PC" Then
            If mQtyAvailable = False Then
                MsgBox("Please Check Quantity., So cann't be Saved.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
            'If MainClass.ValidDataInGrid(SprdMain, ColQty, "N", "Please Check Quantity.") = False Then FieldsVarification = False
        End If
        '    If MainClass.ValidDataInGrid(SprdMain, ColItemRate, "N", "Please Check Item Price") = False Then FieldsVarification = False			

        Exit Function
err_Renamed:
        FieldsVarification = False
        MsgBox(Err.Description)
        'Resume			
    End Function
    Private Function GetItemQty(ByRef pItemCode As String) As Double

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing

        GetItemQty = 0

        mSqlStr = "SELECT SUM(ITEM_QTY) AS ITEM_QTY " & vbCrLf _
            & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID" & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        If Val(txtPONo.Text) > 0 Then
            mSqlStr = mSqlStr & vbCrLf & " AND IH.AUTO_KEY_PO<>" & Val(txtPONo.Text) & ""
        End If

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetItemQty = IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)
        End If

        Exit Function
ErrPart:
        GetItemQty = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function CheckPreviousMonthPendingOrder(ByRef mPODetails As String) As Boolean

        On Error GoTo err_Renamed
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim pMonthStartDate As String


        '    If CDate(PubCurrDate) < CDate("01/10/2018") Then			
        CheckPreviousMonthPendingOrder = False
        Exit Function
        '    End If			

        mPODetails = ""

        pMonthStartDate = "01/" & VB6.Format(PubCurrDate, "MM/YYYY")

        SqlStr = "SELECT IH.AUTO_KEY_PO, TO_CHAR(IH.AMEND_WEF_DATE,'DD/MM/YYYY') AS AMEND_WEF_DATE ,AMEND_NO " & vbCrLf _
            & " FROM PUR_PURCHASE_HDR IH" & vbCrLf _
            & " WHERE IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND IH.ORDER_TYPE='" & VB.Left(lblBookType.Text, 1) & "'" & vbCrLf _
            & " AND IH.PUR_TYPE='" & VB.Right(lblBookType.Text, 1) & "'" & vbCrLf & " AND IH.PO_STATUS='N' AND IH.PO_CLOSED='N'"

        SqlStr = SqlStr & vbCrLf & " AND MKEY NOT IN (" & vbCrLf _
            & " SELECT MKEY FROM GEN_PO_UNLOCK " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TILL_DATE>=TO_DATE('" & VB6.Format(PubCurrDate, "DD/MM/YYYY") & "','DD-MON-YYYY'))"


        '    If RsCompany.fields("FYEAR").value < ConOPENPO_CONTINOUS_YEAR Then			
        '        SqlStr = SqlStr & vbCrLf & " AND SUBSTR(AUTO_KEY_PO,LENGTH(AUTO_KEY_PO)-5,4)=" & RsCompany.fields("FYEAR").value & ""			
        '    Else			
        '        SqlStr = SqlStr & vbCrLf & " AND SUBSTR(AUTO_KEY_PO,LENGTH(AUTO_KEY_PO)-5,4)>=" & ConOPENPO_CONTINOUS_YEAR & ""			
        '    End If			

        SqlStr = SqlStr & vbCrLf & " AND ISGSTENABLE_PO='Y'"


        SqlStr = SqlStr & vbCrLf & " AND AMEND_DATE<TO_DATE('" & VB6.Format(pMonthStartDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " ORDER BY IH.AUTO_KEY_PO, IH.AMEND_WEF_DATE, AMEND_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mPODetails = IIf(mPODetails = "", "", mPODetails & ", " & vbCrLf & IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_PO").Value), "", RsTemp.Fields("AUTO_KEY_PO").Value))
            CheckPreviousMonthPendingOrder = True
        Else
            CheckPreviousMonthPendingOrder = False
        End If

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        'Resume			
    End Function

    Private Function IndentDetailExists(ByRef nItemCode As String, ByRef mSerialNo As Integer, ByRef mQty As Double) As Boolean

        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing '' ADODB.Recordset			

        SqlStr = "SELECT TRN.ITEM_CODE,SUM(TRN.INDENT_QTY) AS INDENT_QTY" & vbCrLf _
            & " FROM TEMP_PUR_POCONS_IND_TRN TRN, PUR_INDENT_HDR IH" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND TRN.USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf _
            & " AND TRN.AUTO_KEY_INDENT=IH.AUTO_KEY_INDENT" & vbCrLf _
            & " AND TRN.ITEM_CODE='" & Trim(nItemCode) & "'" & vbCrLf _
            & " AND IH.DIV_CODE=" & Val(txtDivision.Text) & "" & vbCrLf _
            & " GROUP BY TRN.ITEM_CODE "

        ''& " AND SERIAL_NO=" & mSerialNo & "" & vbCrLf _			
        '			
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If Val(RsTemp.Fields("INDENT_QTY").Value) = mQty Then
                IndentDetailExists = True
            Else
                IndentDetailExists = False
            End If
        Else
            IndentDetailExists = False
        End If
    End Function

    Private Function QuotationDetailExists(ByRef nItemCode As String, ByRef mSerialNo As Integer, ByRef mItemRate As Double, ByRef mItemDisc As Double) As Boolean

        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing '' ADODB.Recordset			

        Dim RsTempQ As ADODB.Recordset = Nothing '' ADODB.Recordset		
        Dim mItemCode As String
        Dim mIndent As Double
        Dim mCheckPrice As Double = 0
        Dim mCheckDiscount As Double = 0
        Dim mPOPrice As Double = 0
        Dim mSupplierCode As String = ""

        mSupplierCode = Trim(txtCode.Text)
        QuotationDetailExists = False

        SqlStr = "SELECT DISTINCT TRN.ITEM_CODE, TRN.AUTO_KEY_INDENT" & vbCrLf _
            & " FROM TEMP_PUR_POCONS_IND_TRN TRN, PUR_INDENT_HDR IH" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND TRN.USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf _
            & " AND TRN.AUTO_KEY_INDENT=IH.AUTO_KEY_INDENT" & vbCrLf _
            & " AND TRN.ITEM_CODE='" & Trim(nItemCode) & "'" & vbCrLf _
            & " AND IH.DIV_CODE=" & Val(txtDivision.Text) & ""
        '			
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mItemCode = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)
            mIndent = IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_INDENT").Value), 0, RsTemp.Fields("AUTO_KEY_INDENT").Value)

            SqlStr = " SELECT * FROM" & vbCrLf _
                        & " PUR_QUOTATION_HDR IH, PUR_QUOTATION_DET ID" & vbCrLf _
                        & " WHERE IH.AUTO_KEY_QUOT=ID.AUTO_KEY_QUOT AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND ID.AUTO_KEY_INDENT=" & Val(mIndent) & "" & vbCrLf _
                        & " AND IH.SUPP_CUST_CODE='" & Trim(mSupplierCode) & "'" & vbCrLf _
                        & " AND ID.ITEM_CODE ='" & Trim(mItemCode) & "' AND ID.QUOTATION_APP='Y'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                mCheckPrice = VB6.Format(IIf(IsDBNull(RsTemp.Fields("ITEM_PRICE").Value), 0, RsTemp.Fields("ITEM_PRICE").Value), "0.000")
                mCheckDiscount = VB6.Format(IIf(IsDBNull(RsTemp.Fields("DISCOUNT").Value), 0, RsTemp.Fields("DISCOUNT").Value), "0.000")

                mCheckPrice = mCheckPrice - (mCheckPrice * mCheckDiscount * 0.01)
                mPOPrice = mItemRate - (mItemRate * mItemDisc * 0.01)

                If mCheckPrice = mPOPrice Then
                    QuotationDetailExists = True
                Else
                    QuotationDetailExists = False
                End If
            End If

        Else
            QuotationDetailExists = False
        End If
    End Function
    Private Function NonApprovedItemExists(ByRef nItemCode As String) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing '' ADODB.Recordset			
        Dim mCheckDate As String

        If MainClass.ValidateWithMasterTable(nItemCode, "ITEM_CODE", "ITEM_APPROVED", "FIN_SUPP_CUST_DET", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote((txtCode.Text)) & "'") = True Then
            If MasterNo = "Y" Then
                NonApprovedItemExists = True
                Exit Function
            End If
        End If

        mCheckDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, -2, CDate(txtPODate.Text)))

        SqlStr = "SELECT SUBCATMST.IS_APPROVAL " & vbCrLf & " FROM INV_ITEM_MST INVMST, INV_SUBCATEGORY_MST SUBCATMST " & vbCrLf & " WHERE INVMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND INVMST.COMPANY_CODE=SUBCATMST.COMPANY_CODE " & vbCrLf & " AND INVMST.CATEGORY_CODE=SUBCATMST.CATEGORY_CODE " & vbCrLf & " AND INVMST.SUBCATEGORY_CODE=SUBCATMST.SUBCATEGORY_CODE " & vbCrLf & " AND INVMST.ITEM_CODE='" & Trim(nItemCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If RsTemp.Fields("IS_APPROVAL").Value = "N" Then
                NonApprovedItemExists = True
                Exit Function
            End If
        Else
            NonApprovedItemExists = True
            Exit Function
        End If

        SqlStr = "SELECT COUNT(1) AS CNT" & vbCrLf & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.SUPP_CUST_CODE='" & Trim(txtCode.Text) & "' " & vbCrLf & " AND ID.ITEM_CODE='" & Trim(nItemCode) & "' AND IH.PUR_TYPE='P' AND IH.ORDER_TYPE='C' AND IH.PO_STATUS='Y'" & vbCrLf & " AND PUR_ORD_DATE<TO_DATE('" & VB6.Format(mCheckDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If RsTemp.Fields("CNT").Value <= 0 Then
                NonApprovedItemExists = True
            Else
                NonApprovedItemExists = False
            End If
        Else
            NonApprovedItemExists = True
        End If
        Exit Function
ErrPart:
        NonApprovedItemExists = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Sub frmPO_GST_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        Me.Hide()
        Me.Dispose()
        Me.Close()
        RsPOMain.Close()
        'RsOpOuts.Close			
    End Sub


    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Function CheckDuplicateItem(ByRef mItemCode As String) As Boolean

        On Error GoTo ERR1
        Dim I As Integer
        Dim mItemRept As Integer
        Dim pCheckItemCode As String

        If mItemCode = "" Then CheckDuplicateItem = True : Exit Function
        With SprdMain
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColItemCode
                pCheckItemCode = Trim(SprdMain.Text)

                If VB.Left(lblBookType.Text, 1) = "J" Then
                    SprdMain.Col = ColOutWardCode
                    pCheckItemCode = pCheckItemCode & "-" & Trim(SprdMain.Text)
                End If

                If UCase(pCheckItemCode) = UCase(Trim(mItemCode)) Then
                    mItemRept = mItemRept + 1
                    If mItemRept > 1 Then
                        CheckDuplicateItem = True
                        MsgInformation("Duplicate Item Code")
                        'MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColItemCode)
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
        'With SprdMain
        '    '        SprdMain_LeaveCell .ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False			
        'End With
    End Sub


    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim SqlStr As String = ""
        Dim xHSNCode As String = ""
        Dim pItemCode As String
        Dim RsTemp As ADODB.Recordset

        If eventArgs.row = 0 And eventArgs.col = ColItemCode And SprdMain.Enabled = True Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode

                '', CONNECT_BY_ROOT  IH.PRODUCT_CODE AS TOP_PRODUCT_CODE,  CONNECT_BY_ROOT  HMST.ITEM_SHORT_DESC AS TOP_PRODUCT_NAME, LEVEL
                '',CONNECT_BY_ROOT  IH.PRODUCT_CODE AS TOP_PRODUCT_CODE,  CONNECT_BY_ROOT  HMST.ITEM_SHORT_DESC AS TOP_PRODUCT_NAME

                '                Select Case IH.PRODUCT_CODE, HMST.ITEM_SHORT_DESC, IH.DEPT_CODE, IH.RM_CODE, RMMST.ITEM_SHORT_DESC,
                'CONNECT_BY_ROOT HMST.ITEM_SHORT_DESC AS TOP_PRODUCT_NAME
                'From VW_PRD_BOM_TRN IH, INV_ITEM_MST HMST, INV_ITEM_MST RMMST
                ' Where IH.COMPANY_CODE = 1
                ' And IH.COMPANY_CODE=HMST.COMPANY_CODE
                ' And IH.PRODUCT_CODE=HMST.ITEM_CODE
                ' And IH.COMPANY_CODE=RMMST.COMPANY_CODE
                ' And IH.RM_CODE=RMMST.ITEM_CODE
                'CONNECT BY NOCYCLE PRIOR IH.RM_CODE =  IH.PRODUCT_CODE ''CONNECT_BY_ROOT HMST.ITEM_SHORT_DESC AS FINAL_PRODUCT_NAME

                If VB.Left(lblBookType.Text, 1) = "J" Then
                    SqlStr = " SELECT DISTINCT IH.PRODUCT_CODE, HMST.ITEM_SHORT_DESC AS PRODUCT_NAME, IH.DEPT_CODE, IH.RM_CODE AS CHILD_CODE, " & vbCrLf _
                            & " RMMST.ITEM_SHORT_DESC AS CHILD_NAME " & vbCrLf _
                            & " FROM VW_PRD_BOM_TRN IH, INV_ITEM_MST HMST, INV_ITEM_MST RMMST" & vbCrLf _
                            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                            & " AND IH.COMPANY_CODE=HMST.COMPANY_CODE" & vbCrLf _
                            & " AND IH.PRODUCT_CODE=HMST.ITEM_CODE" & vbCrLf _
                            & " AND IH.COMPANY_CODE=RMMST.COMPANY_CODE" & vbCrLf _
                            & " AND IH.RM_CODE=RMMST.ITEM_CODE" & vbCrLf _
                            & " CONNECT BY NOCYCLE PRIOR  IH.RM_CODE = IH.PRODUCT_CODE"


                    SqlStr = SqlStr & vbCrLf & " UNION ALL "

                    SqlStr = SqlStr & vbCrLf & " SELECT DISTINCT RMMST.ITEM_CODE, RMMST.ITEM_SHORT_DESC AS PRODUCT_NAME, '' AS DEPT_CODE, HMST.ITEM_CODE AS PARENT_CODE, " & vbCrLf _
                            & " HMST.ITEM_SHORT_DESC AS PARENT_NAME " & vbCrLf _
                            & " FROM INV_ITEM_MST HMST, INV_ITEM_MST RMMST" & vbCrLf _
                            & " WHERE RMMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                            & " AND RMMST.COMPANY_CODE=HMST.COMPANY_CODE" & vbCrLf _
                            & " AND TRIM(RMMST.PARENT_CODE)=TRIM(HMST.ITEM_CODE)"

                    '& vbCrLf _
                    '        & " ORDER BY RMMST.ITEM_SHORT_DESC"

                    '& vbCrLf _
                    '           & " --CONNECT BY PRIOR IH.COMPANY_CODE||TRIM(IH.RM_CODE) = IH.COMPANY_CODE||TRIM(IH.PRODUCT_CODE)"

                    'SqlStr = GetSearchOutwardItem(pItemCode)
                    ''If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "", "ITEM_CODE") = True Then			
                    If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                        .Row = .ActiveRow
                        .Col = ColItemCode
                        .Text = Trim(AcName)

                        .Col = ColItemName
                        .Text = Trim(AcName1)
                    End If
                Else
                    SqlStr = GetSearchItem("Y")
                    If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", "CUSTOMER_PART_NO", , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS='A' ") = True Then
                        'If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                        .Row = .ActiveRow
                        .Col = ColItemCode
                        .Text = Trim(AcName)
                        .Col = ColItemName
                        .Text = Trim(AcName1)
                    End If
                    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
                End If
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColOutWardCode And SprdMain.Enabled = True Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode
                pItemCode = Trim(.Text)

                If pItemCode = "" Then Exit Sub

                .Col = ColOutWardCode

                SqlStr = " SELECT DISTINCT IH.RM_CODE, HMST.ITEM_SHORT_DESC, CONNECT_BY_ROOT  IH.PRODUCT_CODE AS TOP_PRODUCT_CODE,  CONNECT_BY_ROOT  PMST.ITEM_SHORT_DESC AS TOP_PRODUCT_NAME " & vbCrLf _
                        & " FROM VW_PRD_BOM_TRN IH, INV_ITEM_MST HMST, INV_ITEM_MST PMST" & vbCrLf _
                        & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND IH.COMPANY_CODE=HMST.COMPANY_CODE" & vbCrLf _
                        & " AND IH.RM_CODE=HMST.ITEM_CODE" & vbCrLf _
                        & " AND IH.COMPANY_CODE=PMST.COMPANY_CODE" & vbCrLf _
                        & " AND IH.PRODUCT_CODE=PMST.ITEM_CODE" & vbCrLf _
                        & " START WITH IH.PRODUCT_CODE = '" & Trim(pItemCode) & "'" & vbCrLf _
                        & " CONNECT BY PRIOR  IH.RM_CODE = IH.PRODUCT_CODE"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = True Then
                    SqlStr = " SELECT DISTINCT HMST.ITEM_CODE, HMST.ITEM_SHORT_DESC AS PRODUCT_NAME, '' AS DEPT_CODE, RMMST.ITEM_CODE AS CHILD_CODE, " & vbCrLf _
                            & " RMMST.ITEM_SHORT_DESC AS CHILD_NAME " & vbCrLf _
                            & " FROM INV_ITEM_MST HMST, INV_ITEM_MST RMMST" & vbCrLf _
                            & " WHERE RMMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                            & " AND RMMST.COMPANY_CODE=HMST.COMPANY_CODE" & vbCrLf _
                            & " AND TRIM(RMMST.PARENT_CODE)=TRIM(HMST.ITEM_CODE) AND RMMST.ITEM_CODE = '" & Trim(pItemCode) & "'" & vbCrLf _
                            & " ORDER BY HMST.ITEM_SHORT_DESC"
                End If

                RsTemp.Close()
                RsTemp = Nothing

                'SqlStr = GetSearchOutwardItem(pItemCode)
                ''If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "", "ITEM_CODE") = True Then			
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColOutWardCode
                    .Text = Trim(AcName)

                    .Col = ColOutWardName
                    .Text = Trim(AcName1)
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColOutWardCode)
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColItemName And SprdMain.Enabled = True Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemName
                SqlStr = GetSearchItem("N")
                If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", "CUSTOMER_PART_NO", , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS='A' ") = True Then
                    'If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColItemName
                    .Text = Trim(AcName)
                    .Col = ColItemCode
                    .Text = Trim(AcName1)
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColHSN Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColHSN
                If MainClass.SearchGridMaster(.Text, "GEN_HSN_MST", "HSN_CODE", "HSN_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='" & IIf(VB.Left(lblBookType.Text, 1) = "W" Or VB.Left(lblBookType.Text, 1) = "J", "S", "G") & "' ") = True Then     ''AND CODETYPE='" & iif(VB.Left(lblBookType.Text, 1) = "W" Or VB.Left(lblBookType.Text, 1) = "J" ,'S','G') & "'  'VB.Left(lblBookType.Text, 1) = "W" Or VB.Left(lblBookType.Text, 1) = "J" 
                    .Row = .ActiveRow
                    .Col = ColHSN
                    .Text = AcName
                    xHSNCode = Trim(.Text)

                    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColHSN)
                End If

            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColAcctPostName Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColAcctPostName
                MainClass.SearchGridMaster(.Text, "FIN_INVTYPE_MST", "NAME", , , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P'")
                .Row = .ActiveRow
                .Col = ColAcctPostName
                .Text = AcName

                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColAcctPostName)
            End With
        End If
        '    If mAmendStatus = True Or (txtAmendNo.Text) > 0 Then			
        '        Exit Sub			
        '    End If			

        If lblRMPO.Text = "R" And MODIFYMode = True Then
        Else
            If eventArgs.col = 0 And eventArgs.row > 0 And (ADDMode = True Or MODIFYMode = True) Then
                MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColItemName)
                MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
            End If
        End If

    End Sub
    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short


        mCol = SprdMain.ActiveCol
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemName Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemName, 0))

        If eventArgs.keyCode = System.Windows.Forms.Keys.F2 And mCol = ColItemName Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F2 And mCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemName, 0))

        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColOutWardCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColOutWardCode, 0))

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
        Dim xIDesc As String
        Dim xAcctPostName As String
        Dim xWoDesc As String
        Dim mCheckItemCode As String
        Dim xReProcess As String

        If eventArgs.newRow = -1 Then Exit Sub

        If Val(txtDivision.Text) = 0 Then
            MsgInformation("Please Select Division First.")
            If txtDivision.Enabled = True Then txtDivision.Focus()
            Exit Sub
        End If

        Select Case eventArgs.col
            Case ColWoDesc
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColWoDesc
                xWoDesc = SprdMain.Text
                If xWoDesc = "" Then GoTo CalcPart

                If CheckDuplicateItemDesc(xWoDesc) = False Then
                    If FillServiceGSTData((SprdMain.ActiveRow)) = False Then
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColWoDesc)
                    End If
                End If
            Case ColItemCode
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColItemCode
                xICode = SprdMain.Text
                If xICode = "" Then GoTo CalcPart

                mCheckItemCode = Trim(xICode)

                If VB.Left(lblBookType.Text, 1) = "J" Then
                    SprdMain.Col = ColOutWardCode
                    mCheckItemCode = mCheckItemCode & "-" & Trim(SprdMain.Text)
                End If

                If GetValidItem(xICode) = True Then
                    If CheckDuplicateItem(mCheckItemCode) = False Then
                        If FillGridRow(xICode, ColItemCode) = False Then Exit Sub
                        '                    FormatSprdMain Row			
                        '                MainClass.SetFocusToCell SprdMain, Row, ColItemRate		
                    Else
                        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
                    End If
                Else
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColItemCode)
                End If
            Case ColOutWardCode
                If VB.Left(lblBookType.Text, 1) = "J" Then
                    SprdMain.Row = SprdMain.ActiveRow

                    SprdMain.Col = ColItemCode
                    xICode = SprdMain.Text

                    SprdMain.Col = ColReprocess
                    xReProcess = IIf(SprdMain.Value = System.Windows.Forms.CheckState.Checked, "Y", "N")

                    SprdMain.Col = ColOutWardCode
                    If SprdMain.Text = "" Then GoTo CalcPart

                    If SprdMain.Text <> "" Then
                        If MainClass.ValidateWithMasterTable(SprdMain.Text, "ITEM_CODE", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ") = False Then            'AND CODETYPE='" & VB.Left(cboInvType.Text, 1) & "'
                            MsgInformation("Invaild Outward Item CODE.")
                            MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColOutWardCode)
                            Exit Sub
                        End If
                    End If

                    If ValidateOutwardCode(xICode, Trim(SprdMain.Text), xReProcess) = False Then
                        MsgInformation("Invaild Outward Item CODE.")
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColOutWardCode)
                        Exit Sub
                    End If

                    mCheckItemCode = Trim(xICode)

                    If VB.Left(lblBookType.Text, 1) = "J" Then
                        SprdMain.Col = ColOutWardCode
                        mCheckItemCode = mCheckItemCode & "-" & Trim(SprdMain.Text)
                    End If

                    If GetValidItem(xICode) = True Then
                        If CheckDuplicateItem(mCheckItemCode) = True Then
                            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColOutWardCode)
                        End If
                    Else
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColItemCode)
                    End If

                End If
            Case ColHSN
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColItemCode
                xICode = SprdMain.Text
                If xICode = "" Then Exit Sub

                SprdMain.Col = ColHSN
                'If SprdMain.Text = "" Then Exit Sub

                If SprdMain.Text = "" Then
                    If MainClass.ValidateWithMasterTable(xICode, "ITEM_CODE", "HSN_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ") = True Then            'AND CODETYPE='" & VB.Left(cboInvType.Text, 1) & "'
                        SprdMain.Col = ColHSN
                        SprdMain.Text = MasterNo
                    End If
                End If

                SprdMain.Col = ColHSN
                If MainClass.ValidateWithMasterTable(SprdMain.Text, "HSN_CODE", "HSN_CODE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ") = False Then            'AND CODETYPE='" & VB.Left(cboInvType.Text, 1) & "'
                    MsgInformation("Invaild HSN CODE.")
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColHSN)
                    Exit Sub
                End If


                If FillGridRow(xICode, ColHSN) = False Then Exit Sub
            Case ColQty
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColItemCode
                xICode = SprdMain.Text
                If xICode = "" Then GoTo CalcPart
                SprdMain.Col = ColItemName
                xIDesc = SprdMain.Text

                SprdMain.Col = ColQty
                If Val(SprdMain.Text) = 0 Then GoTo CalcPart

                If CheckIndentItem(xICode) = True Then
                    If CheckItemQty() = True Then
                        '               If Right(lblBookType.text, 1) = "PC" Then			
                        If lblBookType.Text = "PC" Then
                            SprdMain.Col = ColQty
                            Call ShowFrmItemIndent((SprdMain.ActiveRow), (lblMkey.Text), xICode, xIDesc, Val(SprdMain.Text), Val(txtDivision.Text))
                        End If
                    End If
                End If
            Case ColItemRate, ColRMRate
                If CheckItemRate() = True Then
                    If VB.Left(lblBookType.Text, 1) = "W" Or VB.Left(lblBookType.Text, 1) = "R" Then
                        MainClass.AddBlankSprdRow(SprdMain, ColWoDesc, ConRowHeight)
                    Else
                        MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                    End If
                    FormatSprdMain(-1)
                End If
            Case ColAcctPostName
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColItemCode
                xICode = SprdMain.Text
                If xICode = "" Then GoTo CalcPart
                SprdMain.Col = ColAcctPostName
                xAcctPostName = SprdMain.Text

                If xAcctPostName = "" Then GoTo CalcPart

                If MainClass.ValidateWithMasterTable(xAcctPostName, "NAME", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P'") = False Then
                    MsgInformation("Invaild Account Post Name.")
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColAcctPostName)
                    Exit Sub
                End If

        End Select
CalcPart:

        Call CalcTots()

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function CheckDuplicateItemDesc(ByRef mItemDesc As String) As Boolean

        On Error GoTo ERR1
        Dim I As Integer
        Dim mItemRept As Integer

        If mItemDesc = "" Then CheckDuplicateItemDesc = True : Exit Function
        With SprdMain
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColWoDesc
                If UCase(Trim(.Text)) = UCase(Trim(mItemDesc)) Then
                    mItemRept = mItemRept + 1
                    If mItemRept > 1 Then
                        CheckDuplicateItemDesc = True
                        MsgInformation("Duplicate Item Description")
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColWoDesc)
                        Exit Function
                    End If
                End If
            Next
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function FillServiceGSTData(ByRef pRow As Integer) As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        'Dim mReverseChargeApp As String			
        Dim mLocal As String
        Dim CntRow As Integer
        Dim mCGSTPer As Double
        Dim mSGSTPer As Double
        Dim mIGSTPer As Double
        'Dim mServCode As String			
        Dim mSACCode As String
        Dim mPartyGSTNo As String
        If Trim(txtServProvided.Text) = "" Then FillServiceGSTData = True : Exit Function

        mLocal = "N"
        mPartyGSTNo = ""
        mLocal = GetPartyBusinessDetail(Trim(txtCode.Text), Trim(txtBillTo.Text), "WITHIN_STATE")
        mPartyGSTNo = GetPartyBusinessDetail(Trim(txtCode.Text), Trim(txtBillTo.Text), "GST_RGN_NO")

        'mLocal = "N"
        'If MainClass.ValidateWithMasterTable(Trim(txtCode.Text), "SUPP_CUST_CODE", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mLocal = MasterNo
        'End If

        'mPartyGSTNo = ""
        'If MainClass.ValidateWithMasterTable(Trim(txtCode.Text), "SUPP_CUST_CODE", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mPartyGSTNo = MasterNo
        'End If

        SqlStr = " SELECT HSN_CODE, HSN_DESC" & vbCrLf & " FROM GEN_HSN_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND HSN_DESC='" & MainClass.AllowSingleQuote((txtServProvided.Text)) & "' AND CODETYPE='S'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = True Then
            MsgInformation("Please Select Valid Service Provided")
            FillServiceGSTData = False
            Exit Function
        Else
            '        mServCode = IIf(IsNull(RsTemp!Code), "", RsTemp!Code)			
            mSACCode = IIf(IsDBNull(RsTemp.Fields("HSN_CODE").Value), "", RsTemp.Fields("HSN_CODE").Value)
            If GetSACDetails(mSACCode, mCGSTPer, mSGSTPer, mIGSTPer, mLocal, mPartyGSTNo, VB.Left(cboGSTStatus.Text, 1)) = False Then GoTo ERR1

            If VB.Left(lblBookType.Text, 1) = "W" Or VB.Left(lblBookType.Text, 1) = "J" Then
                With SprdMain
                    .Row = pRow
                    .Col = ColHSN
                    .Text = mSACCode

                    .Col = ColCGSTPer
                    .Text = VB6.Format(mCGSTPer, "0.00")

                    .Col = ColSGSTPer
                    .Text = VB6.Format(mSGSTPer, "0.00")

                    .Col = ColIGSTPer
                    .Text = VB6.Format(mIGSTPer, "0.00")

                End With
            End If
        End If
        FillServiceGSTData = True
        Exit Function
ERR1:
        FillServiceGSTData = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub ShowFrmItemIndent(ByRef mActiveRow As Integer, ByRef mRefNo As String, ByRef mItemCode As String, ByRef mItemDesc As String, ByRef mQty As Double, ByRef mDivisionCode As Double)
        On Error GoTo ErrPart
        Dim I As Integer
        'Me.lblPOType.Text = "False"
        ConPOIndentDetail = False

        'FrmPOItemIndent.MdiParent = Me.MdiParent
        With FrmPOItemIndent

            .LblPONo.Text = mRefNo
            .LblItemCode.Text = mItemCode
            .lblPOQty.Text = CStr(mQty)
            .lblItemDesc.Text = mItemCode & " : " & mItemDesc
            .lblDivisionCode.Text = CStr(mDivisionCode)
            .lblPORowNo.Text = CStr(mActiveRow)
            .ShowDialog()
            '        .FormatSprdMain -1			

        End With
        If ConPOIndentDetail = True Then
            SprdMain.Row = mActiveRow
            SprdMain.Col = ColQty
            SprdMain.Text = CStr(Val(FrmPOItemIndent.lblPOQty.Text))
            FrmPOItemIndent.Close()
        End If
        Exit Sub
ErrPart:
        If Err.Number = 400 Then Resume Next
        MsgBox(Err.Description)
    End Sub

    Private Function CheckItemQty() As Boolean
        On Error GoTo ERR1

        CheckItemQty = True
        Exit Function

        If VB.Right(lblBookType.Text, 1) = "O" Then
            CheckItemQty = True
            Exit Function
        End If
        With SprdMain
            .Row = .ActiveRow
            .Col = ColItemCode
            If Trim(.Text) = "" Then Exit Function

            .Col = ColQty
            If Val(.Text) > 0 Then
                CheckItemQty = True
            Else
                '            MsgInformation "Please Check the Qty."			
                '            MainClass.SetFocusToCell SprdMain, .ActiveRow, ColQty			
            End If
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function CheckItemRate() As Boolean

        On Error GoTo ERR1
        Dim mItemCode As String
        Dim mItemRate As Double

        If lblBookType.Text = "PC" Then
            CheckItemRate = True
            Exit Function
        End If

        With SprdMain
            .Row = .ActiveRow

            If VB.Left(lblBookType.Text, 1) = "W" Or VB.Left(lblBookType.Text, 1) = "R" Then
                .Col = ColWoDesc
            Else
                .Col = ColItemCode
            End If
            mItemCode = Trim(.Text)

            If mItemCode = "" Then Exit Function

            .Col = ColItemRate
            mItemRate = Val(.Text)
            If mItemRate > 0 Then
                '            If Left(lblBookType.text, 1) = "W" Or Left(lblBookType.text, 1) = "R" Then			
                '                CheckItemRate = True			
                '            Else			
                '                If GetItemRateFromCosting(mItemCode, mItemRate) = True Then			
                CheckItemRate = True
                '                End If			
                '            End If			
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
        Dim mPurchaseInvTypeCode As String
        Dim pCGSTPer As Double
        Dim pSGSTPer As Double
        Dim pIGSTPer As Double
        Dim mInvTypeDesc As String
        Dim mLocal As String
        Dim mPartyGSTNo As String
        Dim mLastSuppCustName As String = ""
        Dim mLastMrrDate As String = ""

        If mItemCode = "" Then Exit Function
        If Trim(txtCode.Text) = "" Then Exit Function

        '    WITHIN_COUNTRY			

        mLocal = "N"
        mPartyGSTNo = ""
        mLocal = GetPartyBusinessDetail(Trim(txtCode.Text), Trim(txtBillTo.Text), "WITHIN_STATE")
        mPartyGSTNo = GetPartyBusinessDetail(Trim(txtCode.Text), Trim(txtBillTo.Text), "GST_RGN_NO")

        'If MainClass.ValidateWithMasterTable(Trim(txtCode.Text), "SUPP_CUST_CODE", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mLocal = MasterNo
        'End If

        'mPartyGSTNo = ""
        'If MainClass.ValidateWithMasterTable(Trim(txtCode.Text), "SUPP_CUST_CODE", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mPartyGSTNo = MasterNo
        'End If

        SqlStr = ""
        SqlStr = " Select INVMST.ITEM_CODE,INVMST.ITEM_SHORT_DESC,DECODE('" & VB.Left(lblBookType.Text, 1) & "','J',INVMST.ITEM_JW_UOM,INVMST.PURCHASE_UOM) AS PURCHASE_UOM, IDENT_MARK," & vbCrLf _
            & " ID.ITEM_RATE,  ID.DISC_PER, INVMST.CATEGORY_CODE, CMST.SALEINVTYPECODE, CMST.PURCHASEINVTYPECODE, INVMST.HSN_CODE " & vbCrLf _
            & " FROM FIN_SUPP_CUST_DET ID, INV_ITEM_MST INVMST, INV_GENERAL_MST CMST" & vbCrLf _
            & " WHERE ID.COMPANY_CODE=INVMST.COMPANY_CODE AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf _
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

                SprdMain.Col = ColItemUOM
                SprdMain.Text = IIf(IsDBNull(.Fields("PURCHASE_UOM").Value), "", .Fields("PURCHASE_UOM").Value)


                SprdMain.Col = ColHSN
                If Trim(SprdMain.Text) = "" Then
                    SprdMain.Text = IIf(IsDBNull(.Fields("HSN_CODE").Value), "", .Fields("HSN_CODE").Value)
                    mHSNCode = IIf(IsDBNull(.Fields("HSN_CODE").Value), "", .Fields("HSN_CODE").Value)
                Else
                    mHSNCode = Trim(SprdMain.Text)
                End If

                mLastSuppCustName = ""
                mLastMrrDate = ""

                ''18/09/2024
                SprdMain.Col = ColLastPORate
                SprdMain.Text = GetLastPORate(Trim(mItemCode), mLastSuppCustName, mLastMrrDate)

                'SprdMain.Col = ColLastPurDate
                'SprdMain.Text = VB6.Format(mLastMrrDate, "dd/MM/yyyy")

                'SprdMain.Col = ColLastSupplier
                'SprdMain.Text = MainClass.AllowSingleQuote(mLastSuppCustName)


                SprdMain.Col = ColIdenty
                SprdMain.Text = IIf(IsDBNull(.Fields("IDENT_MARK").Value), "", .Fields("IDENT_MARK").Value)

                mPurchaseInvTypeCode = IIf(IsDBNull(.Fields("PURCHASEINVTYPECODE").Value), "", .Fields("PURCHASEINVTYPECODE").Value)


                If GetHSNDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, VB.Left(cboGSTStatus.Text, 1), mPartyGSTNo) = False Then GoTo ERR1

                SprdMain.Col = ColCGSTPer
                SprdMain.Text = VB6.Format(pCGSTPer, "0.00")

                SprdMain.Col = ColSGSTPer
                SprdMain.Text = VB6.Format(pSGSTPer, "0.00")

                SprdMain.Col = ColIGSTPer
                SprdMain.Text = VB6.Format(pIGSTPer, "0.00")

                SprdMain.Col = ColAcctPostName
                If Trim(SprdMain.Text) = "" Then
                    mInvTypeDesc = ""
                    If MainClass.ValidateWithMasterTable(mPurchaseInvTypeCode, "CODE", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P'") = True Then
                        mInvTypeDesc = MasterNo
                    End If

                    SprdMain.Col = ColAcctPostName
                    SprdMain.Text = Trim(mInvTypeDesc)
                End If

                SprdMain.Col = ColItemRate
                If Val(SprdMain.Text) = 0 Then
                    SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_RATE").Value), "", .Fields("ITEM_RATE").Value)))


                    SprdMain.Col = ColItemDisc
                    If Val(SprdMain.Text) = 0 Then
                        SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("DISC_PER").Value), "", .Fields("DISC_PER").Value)))
                    End If
                End If
            End With
            FillGridRow = True
        Else
            SqlStr = " Select INVMST.ITEM_CODE,INVMST.ITEM_SHORT_DESC,DECODE('" & VB.Left(lblBookType.Text, 1) & "','J',INVMST.ITEM_JW_UOM,INVMST.PURCHASE_UOM) AS PURCHASE_UOM, INVMST.IDENT_MARK, INVMST.CATEGORY_CODE, CMST.SALEINVTYPECODE, CMST.PURCHASEINVTYPECODE, INVMST.HSN_CODE " & vbCrLf _
                & " FROM INV_ITEM_MST INVMST, INV_GENERAL_MST CMST" & vbCrLf _
                & " WHERE INVMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND INVMST.COMPANY_CODE=CMST.COMPANY_CODE AND INVMST.CATEGORY_CODE=CMST.GEN_CODE" & vbCrLf _
                & " AND INVMST.ITEM_CODE='" & Trim(mItemCode) & "'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
            If RsMisc.EOF = False Then
                SprdMain.Row = SprdMain.ActiveRow
                With RsMisc

                    SprdMain.Col = ColItemName
                    SprdMain.Text = IIf(IsDBNull(.Fields("Item_Short_Desc").Value), "", .Fields("Item_Short_Desc").Value)

                    SprdMain.Col = ColHSN
                    'SprdMain.Text = IIf(IsDBNull(.Fields("HSN_CODE").Value), "", .Fields("HSN_CODE").Value)
                    If Trim(SprdMain.Text) = "" Then
                        SprdMain.Text = IIf(IsDBNull(.Fields("HSN_CODE").Value), "", .Fields("HSN_CODE").Value)
                        mHSNCode = IIf(IsDBNull(.Fields("HSN_CODE").Value), "", .Fields("HSN_CODE").Value)
                    Else
                        mHSNCode = Trim(SprdMain.Text)
                    End If

                    SprdMain.Col = ColIdenty
                    SprdMain.Text = IIf(IsDBNull(.Fields("IDENT_MARK").Value), "", .Fields("IDENT_MARK").Value)

                    SprdMain.Col = ColItemUOM
                    SprdMain.Text = IIf(IsDBNull(.Fields("PURCHASE_UOM").Value), "", .Fields("PURCHASE_UOM").Value)

                    mPurchaseInvTypeCode = IIf(IsDBNull(.Fields("PURCHASEINVTYPECODE").Value), "", .Fields("PURCHASEINVTYPECODE").Value)
                    'mHSNCode = IIf(IsDBNull(.Fields("HSN_CODE").Value), "", .Fields("HSN_CODE").Value)

                    If GetHSNDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, VB.Left(cboGSTStatus.Text, 1), mPartyGSTNo) = False Then GoTo ERR1

                    SprdMain.Col = ColCGSTPer
                    SprdMain.Text = VB6.Format(pCGSTPer, "0.00")

                    SprdMain.Col = ColSGSTPer
                    SprdMain.Text = VB6.Format(pSGSTPer, "0.00")

                    SprdMain.Col = ColIGSTPer
                    SprdMain.Text = VB6.Format(pIGSTPer, "0.00")

                    SprdMain.Col = ColAcctPostName
                    If Trim(SprdMain.Text) = "" Then
                        mInvTypeDesc = ""
                        If MainClass.ValidateWithMasterTable(mPurchaseInvTypeCode, "CODE", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P'") = True Then
                            mInvTypeDesc = MasterNo
                        End If

                        SprdMain.Col = ColAcctPostName
                        SprdMain.Text = Trim(mInvTypeDesc)
                    End If

                End With
                FillGridRow = True
            Else
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, pCol)
                FillGridRow = False
            End If
        End If

        If lblRMPO.Text = "R" Then
            Dim mRMDRWRate As Double
            SprdMain.Row = SprdMain.ActiveRow
            SprdMain.Col = ColRMDRWRate
            mRMDRWRate = GetRMDrawingRate(mItemCode)

            SprdMain.Text = VB6.Format(mRMDRWRate, "0.00")
        End If

        Exit Function
ERR1:
        '    Resume			
        FillGridRow = False
        MsgBox(Err.Description)
    End Function
    Private Sub UltraGrid1_DoubleClick(sender As Object, e As EventArgs) Handles UltraGrid1.DoubleClick

        Dim mPONo As String
        Dim mAmendNo As String

        Dim mRow As UltraGridRow

        If Me.UltraGrid1.ActiveRow.Index < 0 Then Exit Sub
        mRow = Me.UltraGrid1.Rows(Me.UltraGrid1.ActiveRow.Index)

        mPONo = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(1))
        mAmendNo = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(3))

        txtPONo.Text = CStr(Val(mPONo))
        txtAmendNo.Text = CStr(Val(mAmendNo))

        txtAmendNo_Validating(txtAmendNo, New System.ComponentModel.CancelEventArgs(False)) ''txtPONO_Validate False	
        CmdView_Click(CmdView, New System.EventArgs())

    End Sub
    'Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent)
    '    SprdView.Row = SprdView.ActiveRow

    '    SprdView.Col = 2
    '    txtPONo.Text = SprdView.Text

    '    SprdView.Col = 4
    '    txtAmendNo.Text = SprdView.Text

    '    txtAmendNo_Validating(txtAmendNo, New System.ComponentModel.CancelEventArgs(False)) ''txtPONO_Validate False			
    '    CmdView_Click(CmdView, New System.EventArgs())
    'End Sub
    'Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent)
    '    If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdMain.ActiveCol, SprdMain.ActiveRow))
    'End Sub

    Private Sub txtAmendDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAmendDate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAmendDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAmendDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtAmendDate.Text) = "" Then GoTo EventExitSub

        If IsDate(txtAmendDate.Text) = False Then
            MsgInformation(" Invalid Date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
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


    Public Sub txtAmendNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAmendNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMkey As String = ""
        Dim mPONo As Double
        Dim SqlStr As String = ""


        If Trim(txtPONo.Text) = "" Then GoTo EventExitSub

        If Len(txtPONo.Text) < 6 Then
            txtPONo.Text = Val(txtPONo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        mPONo = Val(txtPONo.Text)

        If MODIFYMode = True And RsPOMain.BOF = False Then xMkey = RsPOMain.Fields("mKey").Value

        SqlStr = "SELECT * FROM PUR_PURCHASE_HDR " & vbCrLf _
            & " WHERE AUTO_KEY_PO='" & MainClass.AllowSingleQuote(UCase(CStr(mPONo))) & "'" & vbCrLf _
            & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ISGSTENABLE_PO='Y'"

        SqlStr = SqlStr & vbCrLf & " AND ISRM_PO='" & IIf(lblRMPO.Text = "R", "Y", "N") & "'"

        SqlStr = SqlStr & vbCrLf & " AND PUR_TYPE='" & VB.Left(lblBookType.Text, 1) & "'" & vbCrLf & " AND ORDER_TYPE='" & VB.Right(lblBookType.Text, 1) & "'"


        SqlStr = SqlStr & vbCrLf & " AND AMEND_NO=" & Val(txtAmendNo.Text) & ""


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPOMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsPOMain.EOF = False Then
            Clear1()
            Show1()
        Else
            SqlStr = "SELECT * FROM PUR_PURCHASE_HDR " & " WHERE AUTO_KEY_PO='" & MainClass.AllowSingleQuote(UCase(CStr(mPONo))) & "'" & vbCrLf _
                & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ISGSTENABLE_PO='Y'"

            SqlStr = SqlStr & vbCrLf & " AND ISRM_PO='" & IIf(lblRMPO.Text = "R", "Y", "N") & "'"

            SqlStr = SqlStr & vbCrLf & " AND PUR_TYPE='" & VB.Left(lblBookType.Text, 1) & "'" & vbCrLf & " AND ORDER_TYPE='" & VB.Right(lblBookType.Text, 1) & "'"

            SqlStr = SqlStr & vbCrLf & " AND AMEND_NO IN (" & vbCrLf & " SELECT MAX(AMEND_NO) FROM PUR_PURCHASE_HDR " & " WHERE AUTO_KEY_PO='" & MainClass.AllowSingleQuote(UCase(CStr(mPONo))) & "'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ISGSTENABLE_PO='Y'"

            SqlStr = SqlStr & vbCrLf & " AND PUR_TYPE='" & VB.Left(lblBookType.Text, 1) & "'" & vbCrLf & " AND ORDER_TYPE='" & VB.Right(lblBookType.Text, 1) & "')"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPOMain, ADODB.LockTypeEnum.adLockReadOnly)
            If RsPOMain.EOF = False Then
                Clear1()
                Show1()
            Else

                If ADDMode = False And MODIFYMode = False Then
                    MsgBox("No Such PO No. Click, Add for New", MsgBoxStyle.Information)
                    txtAmendNo.Text = CStr(0)
                    '                Cancel = True			
                ElseIf MODIFYMode = True Then
                    SqlStr = "SELECT * FROM PUR_PURCHASE_HDR WHERE MKEY=" & Val(xMkey) & " AND ISGSTENABLE_PO='Y'"

                    SqlStr = SqlStr & vbCrLf & " AND ISRM_PO='" & IIf(lblRMPO.Text = "R", "Y", "N") & "'"

                    SqlStr = SqlStr & vbCrLf & " AND PUR_TYPE='" & VB.Left(lblBookType.Text, 1) & "'" & vbCrLf & " AND ORDER_TYPE='" & VB.Right(lblBookType.Text, 1) & "'"

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPOMain, ADODB.LockTypeEnum.adLockReadOnly)
                End If
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtAnnexTitle_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAnnexTitle.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAnnexTitle_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAnnexTitle.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDelivery.Text)
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
            txtSupplierName.Text = MasterNo
            txtCode.Enabled = False
        Else
            MsgBox("Name Does Not Exist In Master, Click Add To Add In Master", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDelivery_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDelivery.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDelivery_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDelivery.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDelivery.Text)
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


    Private Sub txtDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDivision.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDivision_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDivision.DoubleClick
        cmdDivSearch_Click(cmdDivSearch, New System.EventArgs())
    End Sub


    Private Sub txtDivision_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDivision.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDivision.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtDivision_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDivision.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdDivSearch_Click(cmdDivSearch, New System.EventArgs())
    End Sub


    Private Sub txtDivision_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDivision.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtDivision.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtDivision.Text), "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            lblDivision.Text = MasterNo
        Else
            MsgBox("Invalid Division Code.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cmdDivSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDivSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtDivision.Text), "INV_DIVISION_MST", "DIV_CODE", "DIV_DESC", , , SqlStr) = True Then
            txtDivision.Text = AcName
            txtDivision_Validating(txtDivision, New System.ComponentModel.CancelEventArgs(False))
            txtDivision.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub TxtExchangeRate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtExchangeRate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtExchangeRate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtExchangeRate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtExcise_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtExcise.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtExcise_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtExcise.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtExcise.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtIndentNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtIndentNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtIndentNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtIndentNo.DoubleClick
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        'SqlStr = " SELECT IH.AUTO_KEY_INDENT, IH.INDENT_DATE, ID.ITEM_CODE, IMST.ITEM_SHORT_DESC, DEPT.DEPT_DESC,REQ_QTY,ITEM_PURPOSE  " & vbCrLf _
        '        & " FROM PUR_INDENT_HDR IH,  PUR_INDENT_DET ID, INV_ITEM_MST IMST, PAY_DEPT_MST DEPT" & vbCrLf _
        '        & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        '        & " And IH.AUTO_KEY_INDENT=ID.AUTO_KEY_INDENT" & vbCrLf _
        '        & " And IH.COMPANY_CODE=IMST.COMPANY_CODE" & vbCrLf _
        '        & " And ID.ITEM_CODE=IMST.ITEM_CODE" & vbCrLf _
        '        & " And IH.COMPANY_CODE=DEPT.COMPANY_CODE" & vbCrLf _
        '        & " And IH.DEPT_CODE=DEPT.DEPT_CODE" & vbCrLf _
        '        & " And SUBSTR(IH.AUTO_KEY_INDENT,LENGTH(IH.AUTO_KEY_INDENT)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
        '        & " And IH.APP_EMP_CODE Is Not NULL And IH.APPROVAL_STATUS='Y'"


        'SqlStr = " SELECT IH.AUTO_KEY_INDENT, IH.INDENT_DATE, ID.ITEM_CODE, IMST.ITEM_SHORT_DESC, DEPT.DEPT_DESC, " & vbCrLf _
        '    & " TO_CHAR(REQ_QTY-SUM(NVL(INDENT_QTY,0))) AS BAL_QTY" & vbCrLf _
        '    & " FROM PUR_INDENT_HDR IH,PUR_INDENT_DET ID,PUR_POCONS_IND_TRN POD, INV_ITEM_MST IMST, PAY_DEPT_MST DEPT " & vbCrLf _
        '    & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        '    & " And IH.AUTO_KEY_INDENT=ID.AUTO_KEY_INDENT" & vbCrLf _
        '    & " And IH.COMPANY_CODE=IMST.COMPANY_CODE" & vbCrLf _
        '    & " And ID.ITEM_CODE=IMST.ITEM_CODE" & vbCrLf _
        '    & " And IH.COMPANY_CODE=DEPT.COMPANY_CODE" & vbCrLf _
        '    & " And IH.DEPT_CODE=DEPT.DEPT_CODE" & vbCrLf _
        '    & " And ID.AUTO_KEY_INDENT=POD.AUTO_KEY_INDENT(+)" & vbCrLf _
        '    & " And ID.ITEM_CODE=POD.ITEM_CODE(+)" & vbCrLf _
        '    & " And DIV_CODE=" & Val(txtDivision.Text) & "" & vbCrLf _
        '    & " And APP_EMP_CODE Is Not NULL And APPROVAL_STATUS='Y' AND INDENT_STATUS='N'" & vbCrLf _
        '    & " GROUP BY IH.AUTO_KEY_INDENT, IH.INDENT_DATE, ID.ITEM_CODE, " & vbCrLf _
        '    & " IMST.ITEM_SHORT_DESC, DEPT.DEPT_DESC,REQ_QTY " & vbCrLf _
        '    & " HAVING REQ_QTY-SUM(NVL(INDENT_QTY,0))>0"

        SqlStr = " SELECT IH.AUTO_KEY_INDENT, IH.INDENT_DATE, ID.ITEM_CODE, IMST.ITEM_SHORT_DESC, DEPT.DEPT_DESC, "

        'SqlStr = SqlStr & vbCrLf _
        '    & " MAX(NVL((SELECT MAX(SUPP_CUST_NAME) FROM PUR_QUOTATION_HDR QIH, PUR_QUOTATION_DET QID, FIN_SUPP_CUST_MST CMST" & vbCrLf _
        '    & " WHERE QIH.AUTO_KEY_QUOT=QID.AUTO_KEY_QUOT" & vbCrLf _
        '    & " AND QIH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
        '    & " AND QIH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
        '    & " AND QID.AUTO_KEY_INDENT=IH.AUTO_KEY_INDENT" & vbCrLf _
        '    & " AND QID.ITEM_CODE = ID.ITEM_CODE AND ID.QUOTATION_APP='Y'" & vbCrLf _
        '    & " ),'')) AS SUPP_NAME,"

        'SqlStr = " SELECT * FROM" & vbCrLf _
        '                & " PUR_QUOTATION_HDR IH, PUR_QUOTATION_DET ID" & vbCrLf _
        '                & " WHERE IH.AUTO_KEY_QUOT=ID.AUTO_KEY_QUOT AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        '                & " AND ID.AUTO_KEY_INDENT=" & Val(mIndent) & "" & vbCrLf _
        '                & " AND IH.SUPP_CUST_CODE='" & Trim(mSupplierCode) & "'" & vbCrLf _
        '                & " AND ID.ITEM_CODE ='" & Trim(mItemCode) & "' AND ID.QUOTATION_APP='Y'"

        SqlStr = SqlStr & vbCrLf _
            & " MAX(NVL(SUPP_CUST_NAME,'')) AS SUPP_CUST_NAME,"

        SqlStr = SqlStr & vbCrLf _
            & " TO_CHAR(REQ_QTY-SUM(NVL(INDENT_QTY,0))) As BAL_QTY" & vbCrLf _
            & " FROM PUR_INDENT_HDR IH,PUR_INDENT_DET ID, PUR_QUOTATION_HDR QIH, PUR_QUOTATION_DET QID, FIN_SUPP_CUST_MST CMST," & vbCrLf _
            & " PUR_POCONS_IND_TRN POD, " & vbCrLf _
            & " INV_ITEM_MST IMST, PAY_DEPT_MST DEPT, INV_GENERAL_MST GMST" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " And IH.AUTO_KEY_INDENT=ID.AUTO_KEY_INDENT" & vbCrLf _
            & " And IH.COMPANY_CODE=IMST.COMPANY_CODE" & vbCrLf _
            & " And ID.ITEM_CODE=IMST.ITEM_CODE" & vbCrLf _
            & " And IH.COMPANY_CODE=DEPT.COMPANY_CODE" & vbCrLf _
            & " And IH.DEPT_CODE=DEPT.DEPT_CODE" & vbCrLf _
            & " And IMST.COMPANY_CODE=GMST.COMPANY_CODE" & vbCrLf _
            & " And IMST.CATEGORY_CODE=GMST.GEN_CODE And GEN_TYPE='C'" & vbCrLf _
            & " AND QID.AUTO_KEY_QUOT=QIH.AUTO_KEY_QUOT(+)" & vbCrLf _
            & " AND QIH.COMPANY_CODE=CMST.COMPANY_CODE(+)" & vbCrLf _
            & " AND QIH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE(+) " & vbCrLf _
            & " AND IH.AUTO_KEY_INDENT=QID.AUTO_KEY_INDENT(+)" & vbCrLf _
            & " AND ID.ITEM_CODE=QID.ITEM_CODE(+) AND DECODE(IS_QUOTATION_REQ,'Y',QID.QUOTATION_APP(+),'1')=DECODE(IS_QUOTATION_REQ,'Y','Y','1')" & vbCrLf _
            & " And ID.AUTO_KEY_INDENT=POD.AUTO_KEY_INDENT(+)" & vbCrLf _
            & " And ID.ITEM_CODE=POD.ITEM_CODE(+)" & vbCrLf _
            & " And DIV_CODE=" & Val(txtDivision.Text) & "" & vbCrLf _
            & " And APP_EMP_CODE Is Not NULL And APPROVAL_STATUS='Y' AND INDENT_STATUS='N'"

        If Trim(txtSupplierName.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND SUPP_CUST_NAME='" & txtSupplierName.Text & "'"
        End If

        SqlStr = SqlStr & vbCrLf _
            & " GROUP BY IH.AUTO_KEY_INDENT, IH.INDENT_DATE, ID.ITEM_CODE, " & vbCrLf _
            & " IMST.ITEM_SHORT_DESC, DEPT.DEPT_DESC,REQ_QTY " & vbCrLf _
            & " HAVING REQ_QTY-SUM(NVL(INDENT_QTY,0))>0"



        If MainClass.SearchGridMasterBySQL2((txtIndentNo.Text), SqlStr) = True Then
            txtIndentNo.Text = AcName
            txtSupplierName.Text = AcName5
            txtSupplierName_Validating(txtSupplierName, New System.ComponentModel.CancelEventArgs(False))
            txtIndentNo_Validating(txtIndentNo, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Sub txtIndentNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtIndentNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtIndentNo_DoubleClick(txtIndentNo, New System.EventArgs())
    End Sub

    Private Sub txtIndentNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtIndentNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsIndent As ADODB.Recordset = Nothing
        Dim mMaxRow As Integer
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mQuotationRequired As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mHSNCode As String
        Dim pCGSTPer As Double
        Dim pSGSTPer As Double
        Dim pIGSTPer As Double
        Dim mLocal As String
        Dim mPartyGSTNo As String


        mLocal = "N"
        mPartyGSTNo = ""
        mLocal = GetPartyBusinessDetail(Trim(txtCode.Text), Trim(txtBillTo.Text), "WITHIN_STATE")
        mPartyGSTNo = GetPartyBusinessDetail(Trim(txtCode.Text), Trim(txtBillTo.Text), "GST_RGN_NO")

        If Val(txtIndentNo.Text) = 0 Then GoTo EventExitSub

        SqlStr = " SELECT ID.ITEM_CODE, ID.ITEM_UOM, ID.REQ_QTY FROM" & vbCrLf _
            & " PUR_INDENT_HDR IH, PUR_INDENT_DET ID, PUR_POCONS_IND_TRN POD" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " And IH.AUTO_KEY_INDENT = ID.AUTO_KEY_INDENT " & vbCrLf _
            & " And ID.AUTO_KEY_INDENT = POD.AUTO_KEY_INDENT(+)" & vbCrLf _
            & " And ID.ITEM_CODE = POD.ITEM_CODE(+)" & vbCrLf _
            & " And IH.AUTO_KEY_INDENT=" & Val(txtIndentNo.Text) & " " & vbCrLf _
            & " And APP_EMP_CODE Is Not NULL And APPROVAL_STATUS='Y' AND ID.INDENT_STATUS='N'"

        SqlStr = SqlStr & vbCrLf _
            & " GROUP BY ID.ITEM_CODE, ID.ITEM_UOM, ID.REQ_QTY " & vbCrLf _
            & " HAVING REQ_QTY-SUM(NVL(INDENT_QTY,0))>0"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsIndent, ADODB.LockTypeEnum.adLockReadOnly)

        If RsIndent.EOF = False Then
            Do While Not RsIndent.EOF
                mItemCode = IIf(IsDBNull(RsIndent.Fields("ITEM_CODE").Value), "", RsIndent.Fields("ITEM_CODE").Value)

                If CheckQuotationRequiredItem(mItemCode) = True Then
                    SqlStr = " SELECT A.AUTO_KEY_QUOT FROM" & vbCrLf _
                           & " PUR_QUOTATION_HDR A, PUR_QUOTATION_DET B" & vbCrLf _
                           & " WHERE A.AUTO_KEY_QUOT=B.AUTO_KEY_QUOT AND A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                           & " AND B.AUTO_KEY_INDENT=" & Val(txtIndentNo.Text) & "" & vbCrLf _
                           & " AND B.ITEM_CODE ='" & Trim(mItemCode) & "' AND B.QUOTATION_APP='Y'"

                    If Trim(txtCode.Text) <> "" Then
                        SqlStr = SqlStr & vbCrLf & " AND A.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'"
                    End If
                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                    If RsTemp.EOF = True Then
                        GoTo NEXTROW
                    End If
                End If
                '
                If CheckAlreadyINGrid(mItemCode) = False Then
                    mMaxRow = GetMaxRow()
                    SprdMain.MaxRows = mMaxRow + 1
                    SprdMain.Row = mMaxRow
                    SprdMain.Col = ColItemCode
                    SprdMain.Text = mItemCode

                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mItemDesc = MasterNo
                    Else
                        mItemDesc = ""
                    End If

                    SprdMain.Col = ColItemName
                    SprdMain.Text = mItemDesc

                    SprdMain.Col = ColItemUOM
                    SprdMain.Text = IIf(IsDBNull(RsIndent.Fields("ITEM_UOM").Value), "", RsIndent.Fields("ITEM_UOM").Value)

                    SprdMain.Col = ColQty
                    If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 115 Then
                        SprdMain.Text = "0.00"
                    Else
                        SprdMain.Text = VB6.Format(IIf(IsDBNull(RsIndent.Fields("REQ_QTY").Value), 0, RsIndent.Fields("REQ_QTY").Value), "0.000")
                    End If

                    'mQuotationRequired = CheckQuotationRequiredItem(mItemCode)

                    'If mQuotationRequired = "Y" Then

                    SqlStr = " SELECT * FROM" & vbCrLf _
                        & " PUR_QUOTATION_DET" & vbCrLf _
                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND AUTO_KEY_INDENT=" & Val(txtIndentNo.Text) & "" & vbCrLf _
                        & " AND ITEM_CODE ='" & Trim(mItemCode) & "' AND QUOTATION_APP='Y'"

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                    If RsTemp.EOF = False Then


                        SprdMain.Col = ColItemRate
                        SprdMain.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("ITEM_PRICE").Value), 0, RsTemp.Fields("ITEM_PRICE").Value), "0.0000")
                        '
                        SprdMain.Col = ColItemDisc
                        SprdMain.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("DISCOUNT").Value), 0, RsTemp.Fields("DISCOUNT").Value), "0.0000")

                    End If


                    mHSNCode = GetHSNCode(Trim(mItemCode))
                    SprdMain.Col = ColHSN
                    SprdMain.Text = mHSNCode

                    If GetHSNDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, VB.Left(cboGSTStatus.Text, 1), mPartyGSTNo) = False Then GoTo ErrPart

                    SprdMain.Col = ColCGSTPer
                    SprdMain.Text = VB6.Format(pCGSTPer, "0.00")

                    SprdMain.Col = ColSGSTPer
                    SprdMain.Text = VB6.Format(pSGSTPer, "0.00")

                    SprdMain.Col = ColIGSTPer
                    SprdMain.Text = VB6.Format(pIGSTPer, "0.00")

                    'End If
                End If
NEXTROW:
                RsIndent.MoveNext()
            Loop
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Function CheckAlreadyINGrid(ByRef pItemCode As String) As Boolean
        On Error GoTo ErrPart
        Dim mRow As Integer
        Dim mCol As Integer
        Dim mCheckItemCode As String
        ''mMaxRow = GetMaxRow()			
        CheckAlreadyINGrid = False
        If pItemCode = "" Then CheckAlreadyINGrid = True : Exit Function
        With SprdMain
            For mRow = 1 To .MaxRows
                .Row = mRow
                .Col = ColItemCode
                mCheckItemCode = UCase(Trim(.Text))
                If UCase(Trim(pItemCode)) = mCheckItemCode Then
                    CheckAlreadyINGrid = True
                End If
            Next
        End With
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function GetMaxRow() As Integer
        On Error GoTo ErrPart
        Dim mRow As Integer
        Dim mRowCount As Integer

        With SprdMain
            For mRow = 1 To .MaxRows
                .Row = mRow
                .Col = ColItemCode
                If UCase(Trim(.Text)) = "" Then
                    mRowCount = mRow
                End If
            Next
        End With

        GetMaxRow = mRowCount
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
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

    Private Sub txtOwner_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOwner.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtOwner_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOwner.DoubleClick
        cmdOwner_Click(cmdOwner, New System.EventArgs())
    End Sub
    Private Sub txtOwner_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOwner.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtOwner.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtOwner_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtOwner.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdOwner_Click(cmdOwner, New System.EventArgs())
    End Sub

    Private Sub txtOwner_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtOwner.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1

        If Trim(txtOwner.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtOwner.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Invalid Owner Name.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
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

    Private Sub txtPONo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPONo.DoubleClick
        cmdSearchPO_Click(cmdSearchPO, New System.EventArgs())
    End Sub

    Private Sub txtPONo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPONo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchPO_Click(cmdSearchPO, New System.EventArgs())
    End Sub

    Private Sub txtPrevPONo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPrevPONo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPrevPONo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPrevPONo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtPrevPONo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPrevPONo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim mPrevPONo As Double
        Dim mPrevAmendPONo As Double
        Dim mPOS As Integer

        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        If Trim(txtPrevPONo.Text) = "" Then GoTo EventExitSub
        mPOS = InStr(1, txtPrevPONo.Text, "-")

        If mPOS = 0 Then
            txtPrevPONo.Text = Val(txtPrevPONo.Text) & "-0"
            mPOS = InStr(1, txtPrevPONo.Text, "-")
        End If

        mPrevPONo = CDbl(Mid(txtPrevPONo.Text, 1, mPOS - 1))
        mPrevAmendPONo = CDbl(Mid(txtPrevPONo.Text, mPOS + 1))


        '    If LblBookCode.text = "Y" Then			
        '        If CheckUnPostedPO(Val(mPONo)) = True Then			
        '            txtPONo.Enabled = True			
        '            txtPONo.SetFocus			
        '            MsgInformation "Please Post First UnPosted PO - " & mPONo			
        '            Cancel = True			
        '            Exit Sub			
        '        End If			
        '    End If			

        SqlStr = "SELECT * FROM PUR_PURCHASE_HDR " & " WHERE AUTO_KEY_PO='" & MainClass.AllowSingleQuote(UCase(CStr(mPrevPONo))) & "'" & vbCrLf _
            & " AND " & vbCrLf & " PUR_TYPE='" & VB.Left(lblBookType.Text, 1) & "'" & vbCrLf _
            & " AND ORDER_TYPE='" & VB.Right(lblBookType.Text, 1) & "' AND MKEY = " 'PO_CLOSED='N'			

        '    If Trim(mPrevAmendPONo) <> "" Then			
        '        SqlStr = SqlStr & vbCrLf & " AND AMEND_NO=" & Val(mPrevAmendPONo) & ""			
        '    End If			

        SqlStr = SqlStr & vbCrLf & " (SELECT MAX(MKEY) FROM PUR_PURCHASE_HDR WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AUTO_KEY_PO='" & MainClass.AllowSingleQuote(UCase(CStr(mPrevPONo))) & "' AND PO_CLOSED='N' AND PO_STATUS='Y')"

        '    SqlStr = SqlStr & vbCrLf & " AND PO_STATUS='Y'"			

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            Clear1()
            Call PrevPoShow1(RsTemp)
            txtWEF.Focus()
        Else
            MsgBox("Invalid PO No.", MsgBoxStyle.Information)
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
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
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        'Dim mReverseChargeApp As String			
        Dim mLocal As String
        Dim CntRow As Integer
        Dim mCGSTPer As Double
        Dim mSGSTPer As Double
        Dim mIGSTPer As Double
        'Dim mServCode As String			
        Dim mSACCode As String
        Dim mPartyGSTNo As String

        If Trim(txtServProvided.Text) = "" Then GoTo EventExitSub

        mLocal = "N"
        mPartyGSTNo = ""
        mLocal = GetPartyBusinessDetail(Trim(txtCode.Text), Trim(txtBillTo.Text), "WITHIN_STATE")
        mPartyGSTNo = GetPartyBusinessDetail(Trim(txtCode.Text), Trim(txtBillTo.Text), "GST_RGN_NO")

        'If MainClass.ValidateWithMasterTable(Trim(txtCode.Text), "SUPP_CUST_CODE", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mLocal = MasterNo
        'End If

        'mPartyGSTNo = ""
        'If MainClass.ValidateWithMasterTable(Trim(txtCode.Text), "SUPP_CUST_CODE", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mPartyGSTNo = MasterNo
        'End If

        SqlStr = " SELECT HSN_CODE, HSN_DESC" & vbCrLf & " FROM GEN_HSN_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND HSN_DESC='" & MainClass.AllowSingleQuote((txtServProvided.Text)) & "' AND CODETYPE='S'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = True Then
            MsgInformation("Please Select Valid Service Provided")
            Cancel = True
            GoTo EventExitSub
        Else
            '        mServCode = IIf(IsNull(RsTemp!Code), "", RsTemp!Code)			
            mSACCode = IIf(IsDBNull(RsTemp.Fields("HSN_CODE").Value), "", RsTemp.Fields("HSN_CODE").Value)
            If GetSACDetails(mSACCode, mCGSTPer, mSGSTPer, mIGSTPer, mLocal, mPartyGSTNo, VB.Left(cboGSTStatus.Text, 1)) = False Then GoTo ERR1

            If VB.Left(lblBookType.Text, 1) = "W" Or VB.Left(lblBookType.Text, 1) = "J" Then
                With SprdMain
                    For CntRow = 1 To .MaxRows - 1
                        .Row = CntRow
                        .Col = ColHSN
                        .Text = mSACCode

                        .Col = ColCGSTPer
                        .Text = VB6.Format(mCGSTPer, "0.00")

                        .Col = ColIGSTPer
                        .Text = VB6.Format(mIGSTPer, "0.00")

                        .Col = ColSGSTPer
                        .Text = VB6.Format(mSGSTPer, "0.00")
                    Next
                End With
            End If
            CalcTots()
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

    Private Sub txtShippedTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtShippedTo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtShippedTo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtShippedTo.DoubleClick
        cmdSearchShippedTo_Click(cmdSearchShippedTo, New System.EventArgs())
    End Sub


    Private Sub txtShippedTo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtShippedTo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtShippedTo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtShippedTo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtShippedTo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchShippedTo_Click(cmdSearchShippedTo, New System.EventArgs())
    End Sub

    Private Sub txtShippedTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtShippedTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim xAcctCode As String


        If Trim(txtShippedTo.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtShippedTo.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = False Then
            MsgBox("Invalid Shipped to Supplier Name.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtDeliveryTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDeliveryTo.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtDeliveryTo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDeliveryTo.DoubleClick
        cmdSearchDeliveryTo_Click(cmdSearchDeliveryTo, New System.EventArgs())
    End Sub

    Private Sub txtDeliveryTo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDeliveryTo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDeliveryTo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtDeliveryTo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDeliveryTo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchDeliveryTo_Click(cmdSearchDeliveryTo, New System.EventArgs())
    End Sub

    Private Sub txtDeliveryTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDeliveryTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim xAcctCode As String


        If Trim(txtDeliveryTo.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtDeliveryTo.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = False Then
            MsgBox("Invalid Delivery to Supplier Name.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSupplierName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplierName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtSupplierName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplierName.DoubleClick
        cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Sub txtSupplierName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSupplierName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSupplierName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSupplierName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSupplierName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Sub txtSupplierName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSupplierName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim xAcctCode As String
        Dim mIsApproved As String

        ''21-03-2006 'SK			
        '    Call DelTemp_Indent			

        If Trim(txtSupplierName.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtSupplierName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
            xAcctCode = MasterNo
            txtCode.Text = xAcctCode
            txtCode.Enabled = False
        Else
            MsgBox("Invalid Supplier Name.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

        If Trim(txtBillTo.Text) = "" Then
            txtBillTo.Text = GetDefaultLocation(xAcctCode)
        End If
        mIsApproved = "N"
        'If VB.Right(lblBookType.Text, 1) = "O" Then
        '    If MainClass.ValidateWithMasterTable((txtCode.Text), "SUPP_CUST_CODE", "IS_APPROVED", "FIN_SUPP_CUST_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mIsApproved = IIf(IsDBNull(MasterNo) Or MasterNo = "N", "N", "Y")
        '    End If
        '    If mIsApproved = "N" Then
        '        MsgInformation("Vendor is not Approved. ")
        '        Cancel = True
        '        If txtSupplierName.Enabled = True Then txtSupplierName.Focus()
        '        GoTo EventExitSub
        '    End If
        'End If

        If VB.Left(lblBookType.Text, 1) = "W" Then

        Else
            If ADDMode = True Then
                If lblBookType.Text = "PC" And Trim(txtIndentNo.Text) <> "" Then
                Else
                    '        If MsgQuestion("Populate Data From Supplier Detail ...") = vbYes Then			
                    Call FillItemFromSuppCustDetail()
                    '          txtRemarks.SetFocus			
                    '        Else			
                    txtRemarks.Focus()
                    '        End If			
                End If
            End If
        End If
        Call FillSprdExp()
        Call SetCurrency()
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
        Dim mHSNCode As String
        Dim mPartyGSTNo As String

        Dim mLocal As String
        Dim pCGSTPer As Double
        Dim pSGSTPer As Double
        Dim pIGSTPer As Double
        Dim mItemCode As String

        MainClass.ClearGrid(SprdMain, ConRowHeight)
        txtExcise.Text = ""
        txtDespMode.Text = ""
        txtPacking.Text = ""

        txtPayment.Text = ""
        txtDelivery.Text = ""
        txtInspection.Text = "At Our Works"
        txtInsurance.Text = ""
        txtOthCond2.Text = ""
        lblPaymentTerms.Text = ""

        mLocal = "N"
        mPartyGSTNo = ""
        mLocal = GetPartyBusinessDetail(Trim(txtCode.Text), Trim(txtBillTo.Text), "WITHIN_STATE")
        mPartyGSTNo = GetPartyBusinessDetail(Trim(txtCode.Text), Trim(txtBillTo.Text), "GST_RGN_NO")

        'If MainClass.ValidateWithMasterTable(Trim(txtCode.Text), "SUPP_CUST_CODE", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mLocal = MasterNo
        'End If

        'mPartyGSTNo = ""
        'If MainClass.ValidateWithMasterTable(Trim(txtCode.Text), "SUPP_CUST_CODE", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mPartyGSTNo = MasterNo
        'End If

        SqlStr = " SELECT IH.PAYMENT_CODE, IH.DELIVERY, IH.EXCISE_OTHERS, " & vbCrLf _
            & " IH.MODE_DESPATCH, IH.INSPECTION, IH.PACKING_FORWARDING, " & vbCrLf _
            & " IH.INSURANCE, IH.OTHERS_COND1, IH.OTHERS_COND2, " & vbCrLf _
            & " ID.ITEM_CODE,  DECODE('" & VB.Left(lblBookType.Text, 1) & "','J',INVMST.ITEM_JW_UOM,INVMST.PURCHASE_UOM) AS PURCHASE_UOM, INVMST.ITEM_SHORT_DESC, INVMST.IDENT_MARK," & vbCrLf _
            & " ID.ITEM_RATE,  ID.DISC_PER " & vbCrLf _
            & " FROM FIN_SUPP_CUST_HDR IH, FIN_SUPP_CUST_DET ID, INV_ITEM_MST INVMST" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=ID.COMPANY_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=ID.SUPP_CUST_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
            & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'" ''& vbCrLf |            & " "			

        If VB.Left(lblBookType.Text, 1) = "J" Then
            SqlStr = SqlStr & vbCrLf & " AND TRN_TYPE='J'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND TRN_TYPE='P'"
        End If

        If lblRMPO.Text = "R" Then
            SqlStr = SqlStr & vbCrLf _
                    & " AND ID.ITEM_CODE IN (SELECT  DISTINCT ID.ITEM_CODE FROM PUR_RM_DWG_RATE_HDR A, PUR_RM_DWG_RATE_DET B" & vbCrLf _
                    & " WHERE A.MKEY=B.MKEY" & vbCrLf _
                    & " AND A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND A.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "' " & vbCrLf _
                    & " AND A.BILL_TO_LOC_ID='" & MainClass.AllowSingleQuote(txtBillTo.Text) & "'" & vbCrLf _
                    & " AND PO_STATUS='Y')"
        End If



        'If VB.Right(lblBookType.Text, 1) = "O" Then
        '    SqlStr = SqlStr & vbCrLf & " AND ID.ITEM_APPROVED='Y'"
        'End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY ID.ITEM_CODE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        I = 1
        If RsTemp.EOF = False Then

            txtExcise.Text = IIf(IsDBNull(RsTemp.Fields("EXCISE_OTHERS").Value), "", RsTemp.Fields("EXCISE_OTHERS").Value)
            txtDespMode.Text = IIf(IsDBNull(RsTemp.Fields("MODE_DESPATCH").Value), "", RsTemp.Fields("MODE_DESPATCH").Value)
            txtPacking.Text = IIf(IsDBNull(RsTemp.Fields("PACKING_FORWARDING").Value), "", RsTemp.Fields("PACKING_FORWARDING").Value)

            txtPayment.Text = IIf(IsDBNull(RsTemp.Fields("PAYMENT_CODE").Value), "", RsTemp.Fields("PAYMENT_CODE").Value)
            txtDelivery.Text = IIf(IsDBNull(RsTemp.Fields("DELIVERY").Value), "", RsTemp.Fields("DELIVERY").Value)
            txtInspection.Text = IIf(IsDBNull(RsTemp.Fields("INSPECTION").Value), "At Our Works", RsTemp.Fields("INSPECTION").Value)
            txtInsurance.Text = IIf(IsDBNull(RsTemp.Fields("INSURANCE").Value), "", RsTemp.Fields("INSURANCE").Value)
            txtOthCond2.Text = IIf(IsDBNull(RsTemp.Fields("OTHERS_COND2").Value), "", RsTemp.Fields("OTHERS_COND2").Value)

            If MainClass.ValidateWithMasterTable(txtPayment.Text, "PAY_TERM_CODE", "PAY_TERM_DESC", "FIN_PAYTERM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblPaymentTerms.Text = MasterNo
            End If

            If MsgQuestion("Populate Data From Supplier Detail ...") = CStr(MsgBoxResult.Yes) Then
                With SprdMain
                    Do While Not RsTemp.EOF
                        .Row = I
                        .Col = ColItemCode
                        .Text = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)
                        mItemCode = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)

                        .Col = ColItemName
                        .Text = IIf(IsDBNull(RsTemp.Fields("Item_Short_Desc").Value), "", RsTemp.Fields("Item_Short_Desc").Value)

                        mHSNCode = GetHSNCode(IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))
                        .Col = ColHSN
                        .Text = mHSNCode

                        .Col = ColIdenty
                        .Text = IIf(IsDBNull(RsTemp.Fields("IDENT_MARK").Value), "", RsTemp.Fields("IDENT_MARK").Value)

                        .Col = ColItemUOM
                        .Text = IIf(IsDBNull(RsTemp.Fields("PURCHASE_UOM").Value), "", RsTemp.Fields("PURCHASE_UOM").Value)

                        .Col = ColQty
                        .Text = CStr(0)

                        If lblRMPO.Text = "R" Then
                            .Col = ColRMRate
                            .Text = CStr(Val(txtRMRate.Text))

                            .Col = ColRMDRWRate
                            .Text = GetRMDrawingRate(mItemCode)
                        Else
                            '.Col = ColRMRate
                            '.Text = "0.00"

                            .Col = ColRMDRWRate
                            .Text = "0.00"

                            .Col = ColItemRate
                            .Text = CStr(Val(IIf(IsDBNull(RsTemp.Fields("ITEM_RATE").Value), "", RsTemp.Fields("ITEM_RATE").Value)))

                            .Col = ColItemDisc
                            .Text = CStr(Val(IIf(IsDBNull(RsTemp.Fields("DISC_PER").Value), "", RsTemp.Fields("DISC_PER").Value)))

                        End If

                        If GetHSNDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, VB.Left(cboGSTStatus.Text, 1), mPartyGSTNo) = False Then GoTo ErrPart

                        SprdMain.Col = ColCGSTPer
                        SprdMain.Text = VB6.Format(pCGSTPer, "0.00")

                        SprdMain.Col = ColSGSTPer
                        SprdMain.Text = VB6.Format(pSGSTPer, "0.00")

                        SprdMain.Col = ColIGSTPer
                        SprdMain.Text = VB6.Format(pIGSTPer, "0.00")

                        I = I + 1
                        .MaxRows = I
                        RsTemp.MoveNext()
                    Loop
                End With
            End If
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
        Dim mAddMode As Boolean
        Dim mSACCode As String
        Dim mOwnerName As String = ""
        Dim mOwnerCode As String
        Dim mPostDetail As Integer
        Dim mShippedToCode As String = ""
        Dim mShippedToName As String = ""
        Dim mGSTStatus As String

        Clear1()
        pShowCalc = False
        If Not RsPOMain.EOF Then

            lblMkey.Text = IIf(IsDBNull(RsPOMain.Fields("MKEY").Value), "", RsPOMain.Fields("MKEY").Value)
            txtPONo.Text = IIf(IsDBNull(RsPOMain.Fields("AUTO_KEY_PO").Value), "", RsPOMain.Fields("AUTO_KEY_PO").Value)
            txtPODate.Text = VB6.Format(IIf(IsDBNull(RsPOMain.Fields("PUR_ORD_DATE").Value), "", RsPOMain.Fields("PUR_ORD_DATE").Value), "DD/MM/YYYY")
            txtPrevPONo.Text = IIf(IsDBNull(RsPOMain.Fields("PREV_PO_NO").Value), "", RsPOMain.Fields("PREV_PO_NO").Value)

            '        chkStatus.Enabled = IIf(RsPOMain!PO_STATUS = "Y", False, True)			

            '        If RsPOMain!PO_STATUS = "Y" Then			
            '            txtAmendNo.Text = Val(IIf(IsNull(RsPOMain.Fields("AMEND_NO").Value), 0, RsPOMain.Fields("AMEND_NO").Value)) + 1			
            '            txtAmendDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")			
            '        Else			


            '        End If			

            TxtExchangeRate.Text = VB6.Format(IIf(IsDBNull(RsPOMain.Fields("ExchangeRate").Value), "1", RsPOMain.Fields("ExchangeRate").Value), "0.000")

            txtWEF.Text = VB6.Format(IIf(IsDBNull(RsPOMain.Fields("AMEND_WEF_DATE").Value), "", RsPOMain.Fields("AMEND_WEF_DATE").Value), "DD/MM/YYYY")

            txtDivision.Text = IIf(IsDBNull(RsPOMain.Fields("DIV_CODE").Value), "", RsPOMain.Fields("DIV_CODE").Value)

            If MainClass.ValidateWithMasterTable((txtDivision.Text), "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblDivision.Text = MasterNo
            End If

            ChkActivate.CheckState = System.Windows.Forms.CheckState.Unchecked
            txtAmendNo.Text = IIf(IsDBNull(RsPOMain.Fields("AMEND_NO").Value), 0, RsPOMain.Fields("AMEND_NO").Value)
            txtAmendDate.Text = VB6.Format(IIf(IsDBNull(RsPOMain.Fields("AMEND_DATE").Value), "", RsPOMain.Fields("AMEND_DATE").Value), "DD/MM/YYYY")
            chkStatus.CheckState = IIf(RsPOMain.Fields("PO_STATUS").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            ChkActivate.CheckState = IIf(RsPOMain.Fields("PO_CLOSED").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkPrintApp.CheckState = IIf(RsPOMain.Fields("PO_PRINT_APP").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            chkCapital.CheckState = IIf(RsPOMain.Fields("ISCAPITAL").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkDevelopment.CheckState = IIf(RsPOMain.Fields("IS_DEVELOPMENT").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            cmdAmend.Enabled = IIf(RsPOMain.Fields("PO_CLOSED").Value = "Y", False, True)

            chkRecdAcct.CheckState = IIf(RsPOMain.Fields("RECD_AC_FLAG").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            txtRecdDate.Text = VB6.Format(IIf(IsDBNull(RsPOMain.Fields("RECD_PO_DATE").Value), "", RsPOMain.Fields("RECD_PO_DATE").Value), "DD/MM/YYYY")

            txtPaymentDays.Text = IIf(IsDBNull(RsPOMain.Fields("PAYDAYS").Value), "", RsPOMain.Fields("PAYDAYS").Value)

            chkApprovedWO_TC.CheckState = IIf(RsPOMain.Fields("APPROVAL_WO_TC").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            chkTCAvailable.CheckState = IIf(RsPOMain.Fields("TC_AVAILABLE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkTCAvailable.Enabled = IIf(RsPOMain.Fields("TC_AVAILABLE").Value = "Y", False, True)
            txtTCPath.Text = IIf(IsDBNull(RsPOMain.Fields("TC_FILE_PATH").Value), "", RsPOMain.Fields("TC_FILE_PATH").Value)
            cmdTC.Enabled = IIf(RsPOMain.Fields("TC_AVAILABLE").Value = "Y", False, True)
            txtOldERPNo.Text = IIf(IsDBNull(RsPOMain.Fields("NAV_PO_NO").Value), "", RsPOMain.Fields("NAV_PO_NO").Value)


            chkTPRAvailable.CheckState = IIf(RsPOMain.Fields("TPRI_AVAILABLE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkTPRAvailable.Enabled = IIf(RsPOMain.Fields("TPRI_AVAILABLE").Value = "Y", False, True)
            txtTPRPath.Text = IIf(IsDBNull(RsPOMain.Fields("TPRI_FILE_PATH").Value), "", RsPOMain.Fields("TPRI_FILE_PATH").Value)
            cmdTPRI.Enabled = IIf(RsPOMain.Fields("TPRI_AVAILABLE").Value = "Y", False, True)


            mAccountCode = IIf(IsDBNull(RsPOMain.Fields("SUPP_CUST_CODE").Value), -1, RsPOMain.Fields("SUPP_CUST_CODE").Value)
            If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountName = MasterNo
            End If

            txtSupplierName.Text = mAccountName
            txtCode.Text = Trim(IIf(IsDBNull(RsPOMain.Fields("SUPP_CUST_CODE").Value), "", RsPOMain.Fields("SUPP_CUST_CODE").Value))
            txtCode.Enabled = False
            txtSupplierName.Enabled = True
            cmdsearch.Enabled = True

            txtBillTo.Text = IIf(IsDBNull(RsPOMain.Fields("BILL_TO_LOC_ID").Value), "", RsPOMain.Fields("BILL_TO_LOC_ID").Value)
            TxtShipTo.Text = IIf(IsDBNull(RsPOMain.Fields("SHIP_TO_LOC_ID").Value), "", RsPOMain.Fields("SHIP_TO_LOC_ID").Value)

            mOwnerCode = IIf(IsDBNull(RsPOMain.Fields("OWNER_CODE").Value), -1, RsPOMain.Fields("OWNER_CODE").Value)
            If MainClass.ValidateWithMasterTable(mOwnerCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mOwnerName = MasterNo
            End If

            txtOwner.Text = mOwnerName

            mPostDetail = Val(IIf(IsDBNull(RsPOMain.Fields("ACCTPOST_DETAIL").Value), 0, RsPOMain.Fields("ACCTPOST_DETAIL").Value))
            optPostingDetails(0).Checked = False
            optPostingDetails(1).Checked = False
            optPostingDetails(2).Checked = False

            If mPostDetail = 1 Then
                optPostingDetails(0).Checked = True
            ElseIf mPostDetail = 2 Then
                optPostingDetails(1).Checked = True
            ElseIf mPostDetail = 2 Then
                optPostingDetails(2).Checked = True
            End If

            txtRemarks.Text = IIf(IsDBNull(RsPOMain.Fields("REMARKS").Value), "", RsPOMain.Fields("REMARKS").Value)

            txtExcise.Text = IIf(IsDBNull(RsPOMain.Fields("EXCISE_OTHERS").Value), "", RsPOMain.Fields("EXCISE_OTHERS").Value)
            txtDespMode.Text = IIf(IsDBNull(RsPOMain.Fields("MODE_DESPATCH").Value), "", RsPOMain.Fields("MODE_DESPATCH").Value)
            txtPacking.Text = IIf(IsDBNull(RsPOMain.Fields("PACKING_FORWARDING").Value), "", RsPOMain.Fields("PACKING_FORWARDING").Value)

            txtPayment.Text = IIf(IsDBNull(RsPOMain.Fields("PAYMENT_CODE").Value), "", RsPOMain.Fields("PAYMENT_CODE").Value)
            txtDelivery.Text = IIf(IsDBNull(RsPOMain.Fields("DELIVERY").Value), "", RsPOMain.Fields("DELIVERY").Value)
            txtInspection.Text = IIf(IsDBNull(RsPOMain.Fields("INSPECTION").Value), "At Our Works", RsPOMain.Fields("INSPECTION").Value)
            txtInsurance.Text = IIf(IsDBNull(RsPOMain.Fields("INSURANCE").Value), "", RsPOMain.Fields("INSURANCE").Value)
            txtOthCond2.Text = IIf(IsDBNull(RsPOMain.Fields("OTHERS_COND2").Value), "", RsPOMain.Fields("OTHERS_COND2").Value)

            lblAddUser.Text = IIf(IsDBNull(RsPOMain.Fields("ADDUSER").Value), "", RsPOMain.Fields("ADDUSER").Value)
            lblAddDate.Text = VB6.Format(IIf(IsDBNull(RsPOMain.Fields("ADDDATE").Value), "", RsPOMain.Fields("ADDDATE").Value), "DD/MM/YYYY")
            lblModUser.Text = IIf(IsDBNull(RsPOMain.Fields("MODUSER").Value), "", RsPOMain.Fields("MODUSER").Value)
            lblModDate.Text = VB6.Format(IIf(IsDBNull(RsPOMain.Fields("MODDATE").Value), "", RsPOMain.Fields("MODDATE").Value), "DD/MM/YYYY")

            If MainClass.ValidateWithMasterTable((txtPayment.Text), "PAY_TERM_CODE", "PAY_TERM_DESC", "FIN_PAYTERM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblPaymentTerms.Text = MasterNo
            End If

            mSACCode = IIf(IsDBNull(RsPOMain.Fields("SAC_CODE").Value), "", RsPOMain.Fields("SAC_CODE").Value)
            If MainClass.ValidateWithMasterTable(mSACCode, "HSN_CODE", "HSN_DESC", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
                txtServProvided.Text = Trim(MasterNo)
            Else
                txtServProvided.Text = ""
            End If
            mGSTStatus = IIf(IsDBNull(RsPOMain.Fields("ISGSTAPPLICABLE").Value), "", RsPOMain.Fields("ISGSTAPPLICABLE").Value)

            If mGSTStatus = "G" Then
                cboGSTStatus.SelectedIndex = 0
            ElseIf mGSTStatus = "R" Then
                cboGSTStatus.SelectedIndex = 1
            ElseIf mGSTStatus = "E" Then
                cboGSTStatus.SelectedIndex = 2
            ElseIf mGSTStatus = "N" Then
                cboGSTStatus.SelectedIndex = 3
            ElseIf mGSTStatus = "I" Then
                cboGSTStatus.SelectedIndex = 4
            ElseIf mGSTStatus = "C" Then
                cboGSTStatus.SelectedIndex = 5
            End If
            cboGSTStatus.Enabled = False

            lblCGSTValue.Text = VB6.Format(IIf(IsDBNull(RsPOMain.Fields("TOTCGST_AMOUNT").Value), "0", RsPOMain.Fields("TOTCGST_AMOUNT").Value), "0.00")
            lblSGSTValue.Text = VB6.Format(IIf(IsDBNull(RsPOMain.Fields("TOTSGST_AMOUNT").Value), "0", RsPOMain.Fields("TOTSGST_AMOUNT").Value), "0.00")
            lblIGSTValue.Text = VB6.Format(IIf(IsDBNull(RsPOMain.Fields("TOTIGST_AMOUNT").Value), "0", RsPOMain.Fields("TOTIGST_AMOUNT").Value), "0.00")

            lblTotOtherExp.Text = VB6.Format(IIf(IsDBNull(RsPOMain.Fields("OTHEREXPVALUE").Value), "0", RsPOMain.Fields("OTHEREXPVALUE").Value), "0.00")
            chkShipTo.CheckState = IIf(RsPOMain.Fields("SHIPPED_TO_SAMEPARTY").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            mShippedToCode = IIf(IsDBNull(RsPOMain.Fields("SHIPPED_TO_PARTY_CODE").Value), -1, RsPOMain.Fields("SHIPPED_TO_PARTY_CODE").Value)
            If MainClass.ValidateWithMasterTable(mShippedToCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mShippedToName = MasterNo
            End If

            txtShippedTo.Text = mShippedToName

            If chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked Then
                TxtShipTo.Enabled = False
                cmdShipToSearch.Enabled = False
            Else
                TxtShipTo.Enabled = True
                cmdShipToSearch.Enabled = True
            End If

            Dim mDeliveryToCode As String = ""
            Dim mDeliveryToName As String = ""

            mDeliveryToCode = IIf(IsDBNull(RsPOMain.Fields("DELIVERY_TO").Value), "", RsPOMain.Fields("DELIVERY_TO").Value)
            TxtDeliveryToLoc.Text = ""
            If mDeliveryToCode <> "" Then
                If MainClass.ValidateWithMasterTable(mDeliveryToCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDeliveryToName = MasterNo
                End If

                txtDeliveryTo.Text = mDeliveryToName

                TxtDeliveryToLoc.Text = IIf(IsDBNull(RsPOMain.Fields("DELIVERY_TO_LOC_ID").Value), "", RsPOMain.Fields("DELIVERY_TO_LOC_ID").Value)

            End If


            txtPrevPONo.Enabled = False
            cmdSearchPrevPO.Enabled = False

            If lblRMPO.Text = "R" Then
                txtRMDesc.Text = IIf(IsDBNull(RsPOMain.Fields("RM_DESC").Value), "", RsPOMain.Fields("RM_DESC").Value)
                txtRMQty.Text = Val(IIf(IsDBNull(RsPOMain.Fields("RM_QTY").Value), 0, RsPOMain.Fields("RM_QTY").Value))
                txtRMRate.Text = Val(IIf(IsDBNull(RsPOMain.Fields("RM_RATE").Value), 0, RsPOMain.Fields("RM_RATE").Value))
            Else
                txtRMDesc.Text = ""
                txtRMQty.Text = ""
                txtRMRate.Text = ""
            End If

            Call ShowDetail1()
            Call ShowExp1((lblMkey.Text))
            '        Call SprdExp_LeaveCell(ColExpAmt, 1, 1, 1, True)			
            Call ShowIndent()
            'Call ShowBlobFile()
            Call SetCurrency()
        End If
        FormatSprdMain(-1)
        Call CalcTots()
        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = True
        SprdExp.Enabled = False
        SprdAnnex.Enabled = False
        txtPONo.Enabled = True
        cmdSearchPO.Enabled = True
        cmdSearchAmend.Enabled = True
        pShowCalc = True
        If VB.Left(lblBookType.Text, 1) = "W" Then
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemName, ColItemName)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColIdenty, ColIdenty)
        Else
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemName, ColItemName)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColIdenty, ColItemUOM)
        End If
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColStatus, ColQtyRecd)

        If lblRMPO.Text = "R" Then
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColRMDRWRate, ColItemRate)
        End If

        MainClass.ButtonStatus(Me, XRIGHT, RsPOMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume			
    End Sub
    Private Sub ShowBlobFile()
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim sTempDir As String
        Dim mFilename As String

        lngImgSiz = 0
        lngOffset = 0


        SqlStr = " SELECT MKEY, TC_DOC_DESC, TC_DOC_EXT, TPR_DOC_DESC, TPR_DOC_EXT, TC_BLOB_DATA, TPR_BLOB_DATA " & vbCrLf & " FROM PUR_PURCHASE_TC_TRN " & vbCrLf & " WHERE MKEY = '" & lblMkey.Text & "'"

        RsTemp = New ADODB.Recordset

        With RsTemp
            .CursorType = ADODB.CursorTypeEnum.adOpenKeyset
            .LockType = ADODB.LockTypeEnum.adLockOptimistic
            .Open(SqlStr, PubDBCnBlob)
            If RsTemp.EOF = False Then
                '            mFileName = IIf(IsNull(RsTemp("TC_DOC_DESC").Value), "", RsTemp("TC_DOC_DESC").Value)			
                mFilename = VB6.Format(GetServerDate, "DDMMYYYY") & VB6.Format(GetServerTimeWithSecond, "HHMMSS") & "_TC." & RsTemp.Fields("TC_DOC_EXT").Value
                StrTempPic = ""
                If mFilename <> "" Then

                    StrTempPic = PubDomainUserDesktopPath & "\" & mFilename ''"_TC." & RsTemp("TC_DOC_EXT").Value  ''VB6.Format(GetServerDate, "DDMMYYYY") & VB6.Format(GetServerTimeWithSecond, "HHMMSS") &     ''RsTemp("TC_DOC_DESC").Value			
                    '                StrTempPic = App.path & "\Temp\" & mFilename			
                    If Len(Dir(StrTempPic)) > 0 Then
                        Kill(StrTempPic)
                    End If

                    lngImgSiz = RsTemp.Fields("TC_BLOB_DATA").ActualSize

                    If lngImgSiz = 0 Then
                        StrTempPic = ""
                    Else
                        nHand = FreeFile()
                        FileOpen(nHand, StrTempPic, OpenMode.Binary)

                        Do While lngOffset < lngImgSiz
                            Chunk = RsTemp.Fields("TC_BLOB_DATA").GetChunk(conChunkSize)
                            FilePut(nHand, Chunk)
                            lngOffset = lngOffset + conChunkSize
                        Loop
                        FileClose(nHand)
                    End If
                End If
            End If
            .Close()
        End With



        txtTCPath.Text = StrTempPic

        ' Second File			


        lngImgSiz = 0
        lngOffset = 0
        StrTempPic = ""

        SqlStr = " SELECT MKEY, TC_DOC_DESC, TC_DOC_EXT, TPR_DOC_DESC, TPR_DOC_EXT, TC_BLOB_DATA, TPR_BLOB_DATA " & vbCrLf & " FROM PUR_PURCHASE_TC_TRN " & vbCrLf & " WHERE MKEY = '" & lblMkey.Text & "'"

        RsTemp = New ADODB.Recordset

        With RsTemp
            .CursorType = ADODB.CursorTypeEnum.adOpenKeyset
            .LockType = ADODB.LockTypeEnum.adLockOptimistic
            .Open(SqlStr, PubDBCnBlob)
            If RsTemp.EOF = False Then
                '            mFileName = IIf(IsNull(RsTemp("TPR_DOC_DESC").Value), "", RsTemp("TPR_DOC_DESC").Value)			
                mFilename = VB6.Format(GetServerDate, "DDMMYYYY") & VB6.Format(GetServerTimeWithSecond, "HHMMSS") & "_TPR." & RsTemp.Fields("TC_DOC_EXT").Value
                StrTempPic = ""
                If mFilename <> "" Then
                    StrTempPic = PubDomainUserDesktopPath & "\" & mFilename ''& "_TPR." & RsTemp("TPR_DOC_EXT").Value       ''RsTemp("TC_DOC_DESC").Value			
                    '                StrTempPic = App.path & "\Temp\" & mFilename			
                    If Len(Dir(StrTempPic)) > 0 Then
                        Kill(StrTempPic)
                    End If

                    lngImgSiz = RsTemp.Fields("TPR_BLOB_DATA").ActualSize

                    If lngImgSiz = 0 Then
                        StrTempPic = ""
                    Else

                        nHand = FreeFile()
                        FileOpen(nHand, StrTempPic, OpenMode.Binary)

                        Do While lngOffset < lngImgSiz
                            Chunk = RsTemp.Fields("TC_BLOB_DATA").GetChunk(conChunkSize)
                            FilePut(nHand, Chunk)
                            lngOffset = lngOffset + conChunkSize
                        Loop
                        FileClose(nHand)
                    End If
                End If
            End If
            .Close()
        End With

        txtTPRPath.Text = StrTempPic


        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume			
    End Sub
    Private Sub PrevPoShow1(ByRef mRsTemp As ADODB.Recordset)

        On Error GoTo ShowErrPart
        Dim mAccountName As String = ""

        Clear1()
        If Not mRsTemp.EOF Then

            lblMkey.Text = ""
            txtPrevPONo.Text = IIf(IsDBNull(mRsTemp.Fields("AUTO_KEY_PO").Value), "", mRsTemp.Fields("AUTO_KEY_PO").Value)
            '        txtPODate.Text = VB6.Format(IIf(IsNull(mRsTemp.Fields("PUR_ORD_DATE").Value), "", mRsTemp.Fields("PUR_ORD_DATE").Value), "DD/MM/YYYY")			
            '        chkStatus.Enabled = IIf(mRsTemp!PO_STATUS = "Y", False, True)			

            '        If mRsTemp!PO_STATUS = "Y" Then			
            '            txtAmendNo.Text = Val(IIf(IsNull(mRsTemp.Fields("AMEND_NO").Value), 0, mRsTemp.Fields("AMEND_NO").Value)) + 1			
            '            txtAmendDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")			
            '        Else			
            txtPrevPONo.Text = txtPrevPONo.Text & "-" & IIf(IsDBNull(mRsTemp.Fields("AMEND_NO").Value), 0, mRsTemp.Fields("AMEND_NO").Value)
            '        txtAmendDate.Text = VB6.Format(IIf(IsNull(mRsTemp.Fields("AMEND_DATE").Value), "", mRsTemp.Fields("AMEND_DATE").Value), "DD/MM/YYYY")			
            '        End If			

            txtWEF.Text = VB6.Format(IIf(IsDBNull(mRsTemp.Fields("AMEND_WEF_DATE").Value), "", mRsTemp.Fields("AMEND_WEF_DATE").Value), "DD/MM/YYYY")

            '        If LblBookCode.text = "Y" Then			
            '            chkStatus.Value = vbUnchecked			
            '        Else			
            '            chkStatus.Value = IIf(mRsTemp!PO_STATUS = "Y", vbChecked, vbUnchecked)			
            '        End If			

            txtPaymentDays.Text = IIf(IsDBNull(mRsTemp.Fields("PAYDAYS").Value), "", mRsTemp.Fields("PAYDAYS").Value)

            mAccountCode = IIf(IsDBNull(mRsTemp.Fields("SUPP_CUST_CODE").Value), -1, mRsTemp.Fields("SUPP_CUST_CODE").Value)

            If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountName = MasterNo
            End If

            txtSupplierName.Text = mAccountName
            txtCode.Text = Trim(IIf(IsDBNull(mRsTemp.Fields("SUPP_CUST_CODE").Value), "", mRsTemp.Fields("SUPP_CUST_CODE").Value))
            txtCode.Enabled = False
            txtSupplierName.Enabled = False
            cmdsearch.Enabled = False

            txtDivision.Text = IIf(IsDBNull(mRsTemp.Fields("DIV_CODE").Value), "", mRsTemp.Fields("DIV_CODE").Value)

            If MainClass.ValidateWithMasterTable((txtDivision.Text), "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblDivision.Text = MasterNo
            End If

            txtExcise.Text = IIf(IsDBNull(mRsTemp.Fields("EXCISE_OTHERS").Value), "", mRsTemp.Fields("EXCISE_OTHERS").Value)
            txtDespMode.Text = IIf(IsDBNull(mRsTemp.Fields("MODE_DESPATCH").Value), "", mRsTemp.Fields("MODE_DESPATCH").Value)
            txtPacking.Text = IIf(IsDBNull(mRsTemp.Fields("PACKING_FORWARDING").Value), "", mRsTemp.Fields("PACKING_FORWARDING").Value)

            txtPayment.Text = IIf(IsDBNull(mRsTemp.Fields("PAYMENT_CODE").Value), "", mRsTemp.Fields("PAYMENT_CODE").Value)
            txtDelivery.Text = IIf(IsDBNull(mRsTemp.Fields("DELIVERY").Value), "", mRsTemp.Fields("DELIVERY").Value)
            txtInspection.Text = IIf(IsDBNull(mRsTemp.Fields("INSPECTION").Value), "At Our Works", mRsTemp.Fields("INSPECTION").Value)
            txtInsurance.Text = IIf(IsDBNull(mRsTemp.Fields("INSURANCE").Value), "", mRsTemp.Fields("INSURANCE").Value)
            txtOthCond2.Text = IIf(IsDBNull(mRsTemp.Fields("OTHERS_COND2").Value), "", mRsTemp.Fields("OTHERS_COND2").Value)

            If MainClass.ValidateWithMasterTable((txtPayment.Text), "PAY_TERM_CODE", "PAY_TERM_DESC", "FIN_PAYTERM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblPaymentTerms.Text = MasterNo
            End If

            '        If LblBookCode.text = "Y" Then			
            '            chkStatus.Value = vbUnchecked			
            '        Else			
            '            ChkActivate.Value = IIf(mRsTemp!PO_CLOSED = "Y", vbChecked, vbUnchecked)			
            '        End If			
            chkPrintApp.CheckState = System.Windows.Forms.CheckState.Unchecked

            Call ShowPrevPoDetail1((mRsTemp.Fields("MKEY").Value))
            Call ShowExp1((mRsTemp.Fields("MKEY").Value))
            Call SprdExp_LeaveCell(SprdExp, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColExpAmt, 1, 1, 1, True))
        End If

        Call CalcTots()

        If VB.Left(lblBookType.Text, 1) = "W" Then
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemName, ColItemName)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColIdenty, ColIdenty)
        Else
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemName, ColItemName)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColIdenty, ColItemUOM)
        End If
        If lblRMPO.Text = "R" Then
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColRMDRWRate, ColItemRate)
        End If
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume			
    End Sub
    Private Sub ShowExp1(ByRef mMKEY As String)

        On Error GoTo ERR1
        Dim I As Integer
        Dim pExpId As String
        Dim SqlStr As String = ""

        Call FillSprdExp()

        pShowCalc = False
        SqlStr = ""
        SqlStr = "Select PUR_PURCHASE_EXP.EXPCODE,PUR_PURCHASE_EXP.EXPPERCENT, " & vbCrLf & " PUR_PURCHASE_EXP.AMOUNT, " & vbCrLf & " FIN_INTERFACE_MST.Name as Name,FIN_INTERFACE_MST.Code, " & vbCrLf & " Identification,Add_Ded,Taxable,Exciseable,CalcOn,RO " & vbCrLf & " From PUR_PURCHASE_EXP,FIN_INTERFACE_MST " & vbCrLf & " WHERE " & vbCrLf & " FIN_INTERFACE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PUR_PURCHASE_EXP.ExpCode=FIN_INTERFACE_MST.Code " & vbCrLf & " AND PUR_PURCHASE_EXP.MKEY='" & mMKEY & "'"

        SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"
        '    If PubGSTApplicable = True Then			
        '        SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"			
        '    Else			
        '        SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='N'"			
        '    End If			

        SqlStr = SqlStr & vbCrLf & " ORDER BY SUBROWNO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPOExp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsPOExp.EOF = False Then
            RsPOExp.MoveFirst()
            With SprdExp
                Do While Not RsPOExp.EOF
                    For I = 1 To .MaxRows
                        .Row = I
                        '			
                        '                    .Col = ColExpIdent			
                        '                    pExpId = Trim(.Text)			
                        '			
                        '                    If pExpId = "ST" Then			
                        '			
                        '                    End If			
                        .Col = ColExpName
                        If .Text = RsPOExp.Fields("Name").Value Then Exit For
                    Next I

                    .Col = ColExpPercent 'Exp. %			
                    .Text = CStr(Val(IIf(IsDBNull(RsPOExp.Fields("ExpPercent").Value), "", RsPOExp.Fields("ExpPercent").Value)))

                    .Col = ColExpAmt
                    If RsPOExp.Fields("Identification").Value = "RO" Then '30.10.2001   ''Allow '-' if exp. is ropund off			
                        .Text = CStr(Val(IIf(IsDBNull(RsPOExp.Fields("Amount").Value), "", RsPOExp.Fields("Amount").Value)))
                    Else
                        .Text = CStr(System.Math.Abs(Val(IIf(IsDBNull(RsPOExp.Fields("Amount").Value), "", RsPOExp.Fields("Amount").Value))))
                    End If

                    .Col = ColExpSTCode
                    .Text = CStr(Val(IIf(IsDBNull(RsPOExp.Fields("CODE").Value), 0, RsPOExp.Fields("CODE").Value)))

                    .Col = ColExpAddDeduct 'ExpFlag			
                    .Text = IIf(RsPOExp.Fields("Add_Ded").Value = "A", "A", "D")

                    .Col = ColExpIdent
                    .Text = IIf(IsDBNull(RsPOExp.Fields("Identification").Value), "", RsPOExp.Fields("Identification").Value)
                    If .Text = "RO" Then 'round off			
                        .Col = ColExpAmt
                        pRound = Val(.Text)
                    End If

                    SprdExp.Col = ColTaxable
                    SprdExp.Text = IIf(IsDBNull(RsPOExp.Fields("Taxable").Value), "N", RsPOExp.Fields("Taxable").Value)

                    SprdExp.Col = ColExciseable
                    SprdExp.Text = IIf(IsDBNull(RsPOExp.Fields("Exciseable").Value), "N", RsPOExp.Fields("Exciseable").Value)

                    SprdExp.Col = ColExpCalcOn
                    SprdExp.Text = CStr(Val(IIf(IsDBNull(RsPOExp.Fields("CalcOn").Value), "", RsPOExp.Fields("CalcOn").Value)))

                    .Col = ColRO
                    .Value = IIf(RsPOExp.Fields("RO").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)


                    RsPOExp.MoveNext()
                Loop
            End With
        End If
        pShowCalc = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)

    End Sub
    Private Sub ShowIndent()

        On Error GoTo ShowIndentErr
        Dim RsIndent As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mITEM_CODE As String
        Call DelTemp_Indent()

        SqlStr = ""

        SqlStr = "SELECT * " & vbCrLf & " FROM PUR_POCONS_IND_TRN " & " WHERE MKEY=" & Val(lblMkey.Text) & " " & vbCrLf & " ORDER BY AUTO_KEY_PO,ITEM_CODE,SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsIndent, ADODB.LockTypeEnum.adLockReadOnly)

        With RsIndent
            If .EOF = False Then
                Do While Not .EOF
                    mITEM_CODE = IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                    SqlStr = ""
                    SqlStr = "INSERT INTO TEMP_PUR_POCONS_IND_TRN " & vbCrLf & " ( USERID, SERIAL_NO, " & vbCrLf & " AUTO_KEY_INDENT, SERIAL_NO_INDENT, " & vbCrLf & " INDENT_QTY, ITEM_CODE) VALUES ( " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " " & Val(.Fields("SERIAL_NO").Value) & ", " & vbCrLf & " " & Val(.Fields("AUTO_KEY_INDENT").Value) & ", " & vbCrLf & " " & Val(.Fields("SERIAL_NO_INDENT").Value) & ", " & vbCrLf & " " & Val(.Fields("INDENT_QTY").Value) & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(mITEM_CODE) & "')"
                    PubDBCn.Execute(SqlStr)
                    .MoveNext()
                Loop
            End If
        End With
        Exit Sub
ShowIndentErr:
        MsgBox(Err.Description)
        '    Resume			
    End Sub
    Private Sub DelTemp_Indent(Optional ByRef mRefNo As Double = 0, Optional ByRef mItemCode As String = "")

        Dim SqlStr As String = ""

        SqlStr = ""
        SqlStr = "DELETE FROM TEMP_PUR_POCONS_IND_TRN " & vbCrLf _
            & "WHERE UserId='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"

        If mRefNo <> 0 And mItemCode <> "" Then
            SqlStr = SqlStr & "AND AUTO_KEY_PO=" & Val(CStr(txtPONo.Text)) & "" & vbCrLf _
                & "AND ITEM_CODE='" & MainClass.AllowSingleQuote(UCase(mItemCode)) & "' "
        End If
        PubDBCn.Execute(SqlStr)
    End Sub
    Private Function GetLastPORate(ByRef pItemCode As String, ByRef mLastSuppCustName As String, ByRef mLastMrrDate As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mPONo As Double
        Dim mLastSuppCustCode As String

        GetLastPORate = 0
        mLastSuppCustCode = ""
        mLastSuppCustName = ""
        mLastMrrDate = ""

        SqlStr = " SELECT IH.MRR_DATE, ID.REF_PO_NO, IH.SUPP_CUST_CODE" & vbCrLf _
            & " FROM INV_GATE_HDR IH, INV_GATE_DET ID" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.AUTO_KEY_MRR=ID.AUTO_KEY_MRR" & vbCrLf _
            & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "' AND IH.REF_TYPE='P'" & vbCrLf _
            & " ORDER BY IH.MRR_DATE DESC"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)

        If RsTemp.EOF = False Then
            mLastSuppCustCode = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value)
            If MainClass.ValidateWithMasterTable(mLastSuppCustCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mLastSuppCustName = MasterNo
            End If


            mLastMrrDate = IIf(IsDBNull(RsTemp.Fields("MRR_DATE").Value), "", RsTemp.Fields("MRR_DATE").Value)
            mPONo = IIf(IsDBNull(RsTemp.Fields("REF_PO_NO").Value), -1, RsTemp.Fields("REF_PO_NO").Value)
        Else
            GetLastPORate = 0
            Exit Function
        End If

        SqlStr = " SELECT NVL(ITEM_PRICE,0) - ROUND((NVL(ITEM_PRICE,0) * ITEM_DIS_PER)/100,2) AS PO_RATE" & vbCrLf _
            & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.MKEY=ID.MKEY" & vbCrLf _
            & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
            & " AND IH.PO_STATUS='Y' AND IH.PUR_TYPE='P' AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mLastSuppCustCode) & "'" & vbCrLf _
            & " AND IH.AUTO_KEY_PO=" & mPONo & "" & vbCrLf _
            & " AND AMEND_WEF_DATE=(" & vbCrLf _
            & " SELECT MAX(AMEND_WEF_DATE) " & vbCrLf _
            & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.MKEY=ID.MKEY" & vbCrLf _
            & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
            & " AND IH.AUTO_KEY_PO=" & mPONo & "" & vbCrLf _
            & " AND IH.PO_STATUS='Y' AND IH.PUR_TYPE='P' AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mLastSuppCustCode) & "'" & vbCrLf _
            & " )"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)

        If RsTemp.EOF = False Then
            GetLastPORate = IIf(IsDBNull(RsTemp.Fields("PO_RATE").Value), 0, RsTemp.Fields("PO_RATE").Value)
        End If
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number))
    End Function

    Private Sub ShowDetail1()

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mIdenty As String
        Dim mWODesc As String
        Dim mCurrValue As Double
        Dim mPrevValue As Double
        Dim mPrice As Double
        Dim mDisc As Double
        Dim mPOWEFDate As String
        Dim mPrevPOWEFDate As String
        Dim mInvTypeCode As String
        Dim mInvTypeDesc As String
        Dim mHSNCode As String
        Dim mISReProcess As String
        Dim mLastSuppCustName As String = ""
        Dim mLastMrrDate As String = ""

        MainClass.ClearGrid(SprdMain, ConRowHeight)

        Call AutoCompleteSearch("PUR_PURCHASE_DET", "ITEM_CODE", " MKEY=" & Val(lblMkey.Text) & "", txtSearchItem)

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf _
            & " FROM PUR_PURCHASE_DET " & vbCrLf _
            & " Where " & vbCrLf _
            & " MKEY=" & Val(lblMkey.Text) & "" & vbCrLf _
            & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPODetail, ADODB.LockTypeEnum.adLockReadOnly)

        With RsPODetail
            If .EOF = True Then Exit Sub
            '        FormatSprdMain -1			
            I = 1
            '        .MoveFirst			

            Do While Not .EOF

                SprdMain.Row = I

                SprdMain.Col = ColWoDesc
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("WO_DESCRIPTION").Value), "", .Fields("WO_DESCRIPTION").Value))
                mWODesc = Trim(IIf(IsDBNull(.Fields("WO_DESCRIPTION").Value), "", .Fields("WO_DESCRIPTION").Value))

                SprdMain.Col = ColItemCode
                'If mWODesc = "" Then
                mItemCode = Trim(IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))
                'Else
                '    mItemCode = ""
                'End If
                '            If mItemCode = "C00010" Then MsgBox "OK"			
                SprdMain.Text = mItemCode

                SprdMain.Col = ColItemName
                If mItemCode <> "" Then
                    MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                    mItemDesc = MasterNo
                Else
                    mItemDesc = ""
                End If

                SprdMain.Text = mItemDesc

                SprdMain.Col = ColOutWardCode
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("OUTWARD_ITEM_CODE").Value), "", .Fields("OUTWARD_ITEM_CODE").Value))

                mItemDesc = ""
                SprdMain.Col = ColOutWardCode
                If Trim(SprdMain.Text) <> "" Then
                    MainClass.ValidateWithMasterTable(Trim(SprdMain.Text), "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                    mItemDesc = MasterNo
                Else
                    mItemDesc = ""
                End If

                SprdMain.Col = ColOutWardName
                SprdMain.Text = mItemDesc

                SprdMain.Col = ColHSN



                If VB.Left(lblBookType.Text, 1) = "W" Or VB.Left(lblBookType.Text, 1) = "J" Then
                    mHSNCode = GetSACCode((txtServProvided.Text))
                Else
                    mHSNCode = Trim(IIf(IsDBNull(.Fields("HSN_CODE").Value), "", .Fields("HSN_CODE").Value))

                    If mHSNCode = "" Then
                        mHSNCode = GetHSNCode(mItemCode)
                    End If

                End If

                SprdMain.Text = mHSNCode

                SprdMain.Col = ColIdenty
                MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "IDENT_MARK", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mIdenty = MasterNo
                SprdMain.Text = mIdenty

                SprdMain.Col = ColItemUOM
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value))

                ''18/09/2024
                SprdMain.Col = ColLastPORate
                SprdMain.Text = GetLastPORate(mItemCode, mLastSuppCustName, mLastMrrDate)

                'SprdMain.Col = ColLastPurDate
                'SprdMain.Text = VB6.Format(mLastMrrDate, "dd/MM/yyyy")          ''= CDate(mLastMrrDate).ToString("dd/MM/yyyy")

                'SprdMain.Col = ColLastSupplier
                'SprdMain.Text = MainClass.AllowSingleQuote(mLastSuppCustName)

                SprdMain.Col = ColQty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_QTY").Value), 0, .Fields("ITEM_QTY").Value)))

                If lblRMPO.Text = "R" Then
                    SprdMain.Col = ColRMRate
                    SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("RM_ITEM_RATE").Value), 0, .Fields("RM_ITEM_RATE").Value)))

                    SprdMain.Col = ColRMDRWRate
                    SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("RM_DRAWING_RATE").Value), 0, .Fields("RM_DRAWING_RATE").Value)))

                Else
                    SprdMain.Col = ColRMRate
                    SprdMain.Text = "0.00"

                    SprdMain.Col = ColRMDRWRate
                    SprdMain.Text = "0.00"
                End If

                SprdMain.Col = ColItemRate
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_PRICE").Value), 0, .Fields("ITEM_PRICE").Value)))
                mPrice = Val(IIf(IsDBNull(.Fields("ITEM_PRICE").Value), 0, .Fields("ITEM_PRICE").Value))

                SprdMain.Col = ColQtyInKgs
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_QTY_IN_KGS").Value), 0, .Fields("ITEM_QTY_IN_KGS").Value)))

                SprdMain.Col = ColRateInKgs
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_PRICE_IN_KGS").Value), 0, .Fields("ITEM_PRICE_IN_KGS").Value)))


                SprdMain.Col = ColItemDisc
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_DIS_PER").Value), 0, .Fields("ITEM_DIS_PER").Value)))
                mDisc = Val(IIf(IsDBNull(.Fields("ITEM_DIS_PER").Value), 0, .Fields("ITEM_DIS_PER").Value))

                SprdMain.Col = ColGross
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("GROSS_AMT").Value), 0, .Fields("GROSS_AMT").Value)))

                SprdMain.Col = ColFreightCost
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("FREIGHT_COST").Value), 0, .Fields("FREIGHT_COST").Value)))

                SprdMain.Col = ColVolumeDiscount
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("VOLUME_DISCOUNT").Value), 0, .Fields("VOLUME_DISCOUNT").Value)))

                SprdMain.Col = ColCGSTPer
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("CGST_PER").Value), 0, .Fields("CGST_PER").Value)))

                SprdMain.Col = ColSGSTPer
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("SGST_PER").Value), 0, .Fields("SGST_PER").Value)))

                SprdMain.Col = ColIGSTPer
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("IGST_PER").Value), 0, .Fields("IGST_PER").Value)))

                ''NVL(ITEM_PRICE,0) - ROUND((NVL(ITEM_PRICE,0) * ITEM_DIS_PER)/100,2)) AS TOTRate			

                mCurrValue = mPrice - System.Math.Round((mPrice * mDisc) / 100, 4)

                SprdMain.Col = ColGross_Prev
                If Val(txtAmendNo.Text) = 0 Then
                    SprdMain.Text = "0"
                Else
                    SprdMain.Text = VB6.Format(GetPreviousItemGross(mItemCode, mWODesc), "0.0000")
                End If
                mPrevValue = Val(SprdMain.Text)

                SprdMain.Col = ColRemarks
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value))

                mPOWEFDate = Trim(IIf(IsDBNull(.Fields("PO_WEF_DATE").Value), "", .Fields("PO_WEF_DATE").Value))
                If mPOWEFDate = "" Then
                    mPOWEFDate = VB6.Format(txtWEF.Text, "DD/MM/YYYY")
                End If
                SprdMain.Col = ColPO_WEF
                SprdMain.Text = VB6.Format(mPOWEFDate, "DD/MM/YYYY")

                SprdMain.Col = ColPrevPO_WEF
                If Val(txtAmendNo.Text) = 0 Then
                    mPrevPOWEFDate = ""
                Else
                    mPrevPOWEFDate = VB6.Format(GetPreviousItemWEFDate(mItemCode, mWODesc))
                End If
                SprdMain.Text = VB6.Format(mPrevPOWEFDate, "DD/MM/YYYY")

                SprdMain.Col = ColQtyRecd
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_RECD_QTY").Value), 0, .Fields("ITEM_RECD_QTY").Value)))

                SprdMain.Col = ColStatus
                SprdMain.Value = IIf(.Fields("PO_ITEM_STATUS").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                SprdMain.Col = ColIsTentativeRate
                SprdMain.Value = IIf(.Fields("IS_TENTATIVE_RATE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                SprdMain.Col = ColPrintStatus
                SprdMain.Value = IIf(Val(CStr(mCurrValue)) = Val(CStr(mPrevValue)) And mPOWEFDate = mPrevPOWEFDate, System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)

                SprdMain.Col = ColReprocess
                SprdMain.Value = IIf(.Fields("IS_REPROCESS").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)


                mInvTypeCode = Trim(IIf(IsDBNull(.Fields("ACCOUNT_POSTING_CODE").Value), "", .Fields("ACCOUNT_POSTING_CODE").Value))
                mInvTypeDesc = ""

                If MainClass.ValidateWithMasterTable(mInvTypeCode, "CODE", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P'") = True Then
                    mInvTypeDesc = MasterNo
                End If

                SprdMain.Col = ColAcctPostName
                SprdMain.Value = mInvTypeDesc

                SprdMain.Col = ColAssetsNo
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("ASSETS_NO").Value), "", .Fields("ASSETS_NO").Value))

                .MoveNext()

                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf _
            & " FROM PUR_PURCHASE_ANNEX " & vbCrLf _
            & " Where " & vbCrLf _
            & " MKEY=" & Val(lblMkey.Text) & "" & vbCrLf _
            & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPOAnnex, ADODB.LockTypeEnum.adLockReadOnly)

        With RsPOAnnex
            If .EOF = True Then Exit Sub
            I = 1

            txtAnnexTitle.Text = Trim(IIf(IsDBNull(.Fields("ANNEX_TITLE").Value), "", .Fields("ANNEX_TITLE").Value))

            Do While Not .EOF

                SprdAnnex.Row = I

                SprdAnnex.Col = ColAnnexDesc
                SprdAnnex.Text = Trim(IIf(IsDBNull(.Fields("DESCRIPTION").Value), "", .Fields("DESCRIPTION").Value))

                .MoveNext()

                I = I + 1
                SprdAnnex.MaxRows = I
            Loop
        End With

        Call FormatSprdMain(-1)
        FormatSprdAnnex(-1)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''   Resume			
    End Sub
    Private Sub ShowPrevPoDetail1(ByRef mPONo As Double)

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mIdenty As String
        Dim mPurchaseInvTypeCode As String
        Dim mHSNCode As String
        Dim pCGSTPer As Double
        Dim pSGSTPer As Double
        Dim pIGSTPer As Double
        Dim mLocal As String
        Dim mInvTypeDesc As String
        Dim mPartyGSTNo As String

        mLocal = "N"
        mPartyGSTNo = ""
        mLocal = GetPartyBusinessDetail(Trim(txtCode.Text), Trim(txtBillTo.Text), "WITHIN_STATE")
        mPartyGSTNo = GetPartyBusinessDetail(Trim(txtCode.Text), Trim(txtBillTo.Text), "GST_RGN_NO")

        'mLocal = "N"
        'If MainClass.ValidateWithMasterTable(Trim(txtCode.Text), "SUPP_CUST_CODE", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mLocal = MasterNo
        'End If

        'mPartyGSTNo = ""
        'If MainClass.ValidateWithMasterTable(Trim(txtCode.Text), "SUPP_CUST_CODE", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mPartyGSTNo = MasterNo
        'End If

        SqlStr = ""
        '    SqlStr = " SELECT * " & vbCrLf _			
        ''            & " FROM PUR_PURCHASE_DET " & vbCrLf _			
        ''            & " Where " & vbCrLf _			
        ''            & " MKEY=" & Val(mPONo) & "" & vbCrLf _			
        ''            & " Order By SERIAL_NO"			

        SqlStr = " Select ID.*, INVMST.ITEM_SHORT_DESC,DECODE('" & VB.Left(lblBookType.Text, 1) & "','J',INVMST.ITEM_JW_UOM,INVMST.PURCHASE_UOM) AS PURCHASE_UOM, IDENT_MARK," & vbCrLf _
            & " INVMST.CATEGORY_CODE, CMST.SALEINVTYPECODE, CMST.PURCHASEINVTYPECODE, INVMST.HSN_CODE " & vbCrLf _
            & " FROM PUR_PURCHASE_DET ID, INV_ITEM_MST INVMST, INV_GENERAL_MST CMST" & vbCrLf _
            & " WHERE ID.COMPANY_CODE=INVMST.COMPANY_CODE AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf _
            & " AND INVMST.COMPANY_CODE=CMST.COMPANY_CODE AND INVMST.CATEGORY_CODE=CMST.GEN_CODE" & vbCrLf _
            & " AND ID.MKEY=" & Val(CStr(mPONo)) & "" & vbCrLf _
            & " Order By ID.SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPODetail, ADODB.LockTypeEnum.adLockReadOnly)

        With RsPODetail
            If .EOF = True Then Exit Sub
            '        FormatSprdMain -1			
            I = 1
            '        .MoveFirst			

            Do While Not .EOF

                SprdMain.Row = I

                SprdMain.Col = ColWoDesc
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("WO_DESCRIPTION").Value), "", .Fields("WO_DESCRIPTION").Value))

                SprdMain.Col = ColItemCode
                mItemCode = Trim(IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))
                SprdMain.Text = mItemCode

                SprdMain.Col = ColItemName
                MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mItemDesc = MasterNo
                SprdMain.Text = mItemDesc

                SprdMain.Col = ColIdenty
                MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "IDENT_MARK", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mIdenty = MasterNo
                SprdMain.Text = mIdenty

                SprdMain.Col = ColHSN
                SprdMain.Text = IIf(IsDBNull(.Fields("HSN_CODE").Value), "", .Fields("HSN_CODE").Value)

                mPurchaseInvTypeCode = IIf(IsDBNull(.Fields("PURCHASEINVTYPECODE").Value), "", .Fields("PURCHASEINVTYPECODE").Value)
                mHSNCode = IIf(IsDBNull(.Fields("HSN_CODE").Value), "", .Fields("HSN_CODE").Value)

                If GetHSNDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, VB.Left(cboGSTStatus.Text, 1), mPartyGSTNo) = False Then GoTo ERR1

                SprdMain.Col = ColCGSTPer
                SprdMain.Text = VB6.Format(pCGSTPer, "0.00")

                SprdMain.Col = ColSGSTPer
                SprdMain.Text = VB6.Format(pSGSTPer, "0.00")

                SprdMain.Col = ColIGSTPer
                SprdMain.Text = VB6.Format(pIGSTPer, "0.00")

                SprdMain.Col = ColAcctPostName
                If Trim(SprdMain.Text) = "" Then
                    mInvTypeDesc = ""
                    If MainClass.ValidateWithMasterTable(mPurchaseInvTypeCode, "CODE", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P'") = True Then
                        mInvTypeDesc = MasterNo
                    End If

                    SprdMain.Col = ColAcctPostName
                    SprdMain.Text = Trim(mInvTypeDesc)
                End If

                SprdMain.Col = ColItemUOM
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value))

                SprdMain.Col = ColQty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_QTY").Value), 0, .Fields("ITEM_QTY").Value)))

                If lblRMPO.Text = "R" Then
                    SprdMain.Col = ColRMRate
                    SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("RM_ITEM_RATE").Value), 0, .Fields("RM_ITEM_RATE").Value)))

                    SprdMain.Col = ColRMDRWRate
                    SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("RM_DRAWING_RATE").Value), 0, .Fields("RM_DRAWING_RATE").Value)))

                Else
                    SprdMain.Col = ColRMRate
                    SprdMain.Text = "0.00"

                    SprdMain.Col = ColRMDRWRate
                    SprdMain.Text = "0.00"
                End If

                SprdMain.Col = ColItemRate
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_PRICE").Value), 0, .Fields("ITEM_PRICE").Value)))

                SprdMain.Col = ColQtyInKgs
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_QTY_IN_KGS").Value), 0, .Fields("ITEM_QTY_IN_KGS").Value)))

                SprdMain.Col = ColRateInKgs
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_PRICE_IN_KGS").Value), 0, .Fields("ITEM_PRICE_IN_KGS").Value)))


                SprdMain.Col = ColItemDisc
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_DIS_PER").Value), 0, .Fields("ITEM_DIS_PER").Value)))

                SprdMain.Col = ColGross
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("GROSS_AMT").Value), 0, .Fields("GROSS_AMT").Value)))

                SprdMain.Col = ColRemarks
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value))

                SprdMain.Col = ColFreightCost
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("FREIGHT_COST").Value), "", .Fields("FREIGHT_COST").Value))

                SprdMain.Col = ColVolumeDiscount
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("VOLUME_DISCOUNT").Value), "", .Fields("VOLUME_DISCOUNT").Value))

                SprdMain.Col = ColQtyRecd
                SprdMain.Text = CStr(0) ''Val(IIf(IsNull(.Fields("ITEM_RECD_QTY").Value), 0, .Fields("ITEM_RECD_QTY").Value))			

                SprdMain.Col = ColStatus
                SprdMain.Value = CStr(System.Windows.Forms.CheckState.Unchecked) '' IIf(!PO_ITEM_STATUS = "Y", vbChecked, vbUnchecked)			

                SprdMain.Col = ColIsTentativeRate
                SprdMain.Value = CStr(System.Windows.Forms.CheckState.Unchecked)

                SprdMain.Col = ColReprocess
                SprdMain.Value = IIf(.Fields("IS_REPROCESS").Value = "Y", CStr(System.Windows.Forms.CheckState.Checked), CStr(System.Windows.Forms.CheckState.Unchecked))

                .MoveNext()

                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM PUR_PURCHASE_ANNEX " & vbCrLf & " Where " & vbCrLf & " MKEY=" & Val(CStr(mPONo)) & "" & vbCrLf & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPOAnnex, ADODB.LockTypeEnum.adLockReadOnly)

        With RsPOAnnex
            If .EOF = True Then Exit Sub
            I = 1

            txtAnnexTitle.Text = Trim(IIf(IsDBNull(.Fields("ANNEX_TITLE").Value), "", .Fields("ANNEX_TITLE").Value))

            Do While Not .EOF

                SprdAnnex.Row = I

                SprdAnnex.Col = ColAnnexDesc
                SprdAnnex.Text = Trim(IIf(IsDBNull(.Fields("DESCRIPTION").Value), "", .Fields("DESCRIPTION").Value))

                .MoveNext()

                I = I + 1
                SprdAnnex.MaxRows = I
            Loop
            FormatSprdAnnex(-1)
        End With

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume			
    End Sub
    Private Sub txtOthCond2_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOthCond2.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOthCond2_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOthCond2.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtOthCond2.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPacking_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPacking.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPacking_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPacking.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPacking.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPayment_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPayment.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPayment_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPayment.DoubleClick
        cmdPaySearch_Click(cmdPaySearch, New System.EventArgs())
    End Sub


    Private Sub txtPayment_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPayment.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPayment.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPayment_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPayment.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdPaySearch_Click(cmdPaySearch, New System.EventArgs())
    End Sub


    Private Sub txtPayment_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPayment.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim xAcctCode As String
        Dim mIsMSMESupplier As String
        Dim mErrorMsg As String
        If Trim(txtPayment.Text) = "" Then GoTo EventExitSub


        If MainClass.ValidateWithMasterTable(txtSupplierName.Text, "SUPP_CUST_NAME", "SME_REGD", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SME_REGD='Y'") = True Then
            mIsMSMESupplier = "Y"
            mErrorMsg = "Invalid Payment Code for MSME Supplier"
        Else
            mIsMSMESupplier = "N"
            mErrorMsg = "Invalid Payment Code."
        End If

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If mIsMSMESupplier = "Y" Then
            SqlStr = SqlStr & " AND FOR_MSME='Y'"
        End If

        If MainClass.ValidateWithMasterTable((txtPayment.Text), "PAY_TERM_CODE", "PAY_TERM_DESC", "FIN_PAYTERM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            lblPaymentTerms.Text = MasterNo
        Else
            MsgBox(mErrorMsg, MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtPaymentDays_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPaymentDays.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPaymentDays_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPaymentDays.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtPODate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPODate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPONo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPONo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtPONo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPONo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Public Sub txtPONO_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPONo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMkey As String = ""
        Dim mPONo As Double
        Dim SqlStr As String = ""


        If Trim(txtPONo.Text) = "" Then GoTo EventExitSub

        If Len(txtPONo.Text) < 6 Then
            txtPONo.Text = Val(txtPONo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        mPONo = Val(txtPONo.Text)

        If MODIFYMode = True And RsPOMain.BOF = False Then xMkey = RsPOMain.Fields("mKey").Value

        SqlStr = "SELECT * FROM PUR_PURCHASE_HDR " & " WHERE AUTO_KEY_PO='" & MainClass.AllowSingleQuote(UCase(CStr(mPONo))) & "'" & vbCrLf _
            & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ISGSTENABLE_PO='Y' "

        SqlStr = SqlStr & vbCrLf & " AND ISRM_PO='" & IIf(lblRMPO.Text = "R", "Y", "N") & "'"
        SqlStr = SqlStr & vbCrLf & " AND PUR_TYPE='" & VB.Left(lblBookType.Text, 1) & "'" & vbCrLf _
            & " AND ORDER_TYPE='" & VB.Right(lblBookType.Text, 1) & "' "

        SqlStr = SqlStr & vbCrLf & " AND AMEND_NO = (" & vbCrLf & " SELECT MAX(AMEND_NO) AS AMEND_NO FROM PUR_PURCHASE_HDR" & vbCrLf _
            & " WHERE AUTO_KEY_PO='" & MainClass.AllowSingleQuote(UCase(CStr(mPONo))) & "' AND ISGSTENABLE_PO='Y'" & vbCrLf _
            & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ")"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPOMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsPOMain.EOF = False Then
            Clear1()
            Show1()
        Else
            SqlStr = "SELECT * FROM PUR_PURCHASE_HDR " & " WHERE AUTO_KEY_PO='" & MainClass.AllowSingleQuote(UCase(CStr(mPONo))) & "'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ISGSTENABLE_PO='Y'"

            SqlStr = SqlStr & vbCrLf & " AND ISRM_PO='" & IIf(lblRMPO.Text = "R", "Y", "N") & "'"
            SqlStr = SqlStr & vbCrLf & " AND PUR_TYPE='" & VB.Left(lblBookType.Text, 1) & "'" & vbCrLf & " AND ORDER_TYPE='" & VB.Right(lblBookType.Text, 1) & "' "

            SqlStr = SqlStr & vbCrLf & " AND AMEND_NO IN (" & vbCrLf & " SELECT MAX(AMEND_NO) FROM PUR_PURCHASE_HDR " & vbCrLf & " WHERE AUTO_KEY_PO='" & MainClass.AllowSingleQuote(UCase(CStr(mPONo))) & "'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ISGSTENABLE_PO='Y'"


            SqlStr = SqlStr & vbCrLf & " AND PUR_TYPE='" & VB.Left(lblBookType.Text, 1) & "'" & vbCrLf & " AND ORDER_TYPE='" & VB.Right(lblBookType.Text, 1) & "' )"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPOMain, ADODB.LockTypeEnum.adLockReadOnly)
            If RsPOMain.EOF = False Then
                Clear1()
                Show1()
            Else

                If ADDMode = False And MODIFYMode = False Then
                    MsgBox("No Such PO No. Click, Add for New", MsgBoxStyle.Information)
                    txtAmendNo.Text = CStr(0)
                    Cancel = True
                ElseIf MODIFYMode = True Then
                    SqlStr = "SELECT * FROM PUR_PURCHASE_HDR WHERE MKEY=" & Val(xMkey) & " AND ISGSTENABLE_PO='Y'"

                    SqlStr = SqlStr & vbCrLf & " AND ISRM_PO='" & IIf(lblRMPO.Text = "R", "Y", "N") & "'"

                    SqlStr = SqlStr & vbCrLf & " AND PUR_TYPE='" & VB.Left(lblBookType.Text, 1) & "'" & vbCrLf & " AND ORDER_TYPE='" & VB.Right(lblBookType.Text, 1) & "' "

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPOMain, ADODB.LockTypeEnum.adLockReadOnly)
                End If
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
    Private Sub txtRMDesc_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRMDesc.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtRMDesc_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRMDesc.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtRMDesc.Text)
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
            mSqlStr = "SELECT A.ITEM_CODE,A.ITEM_SHORT_DESC,CUSTOMER_PART_NO "
        Else
            mSqlStr = "SELECT A.ITEM_SHORT_DESC,A.ITEM_CODE,CUSTOMER_PART_NO "
        End If

        'If VB.Right(lblBookType.Text, 1) = "O" Then
        '    mSqlStr = mSqlStr & vbCrLf & " FROM INV_ITEM_MST A, FIN_SUPP_CUST_DET B" & vbCrLf & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf & " AND A.ITEM_CODE=B.ITEM_CODE " & vbCrLf & " AND B.SUPP_CUST_CODE='" & xSuppCode & "' AND ITEM_APPROVED='Y'"
        'Else
        mSqlStr = mSqlStr & vbCrLf & " FROM INV_ITEM_MST A" & vbCrLf _
            & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS='A'"
        'End If

        If mByCode = "Y" Then
            mSqlStr = mSqlStr & vbCrLf & "ORDER BY A.ITEM_CODE "
        Else
            mSqlStr = mSqlStr & vbCrLf & "ORDER BY A.ITEM_SHORT_DESC"
        End If

        'mSqlStr = mSqlStr & vbCrLf & " FETCH FIRST 500 ROWS ONLY"

        GetSearchItem = mSqlStr
        Exit Function
ErrPart:
        GetSearchItem = ""

    End Function
    Private Function GetSearchOutwardItem(nItemCode As String) As String
        On Error GoTo ErrPart
        Dim mSqlStr As String
        'Dim nItemCode As String

        'SprdMain.Row = pRow
        'SprdMain.Col = ColItemCode
        'nItemCode
        mSqlStr = "SELECT A.RM_CODE,  B.ITEM_SHORT_DESC, B.ISSUE_UOM" & vbCrLf _
            & " FROM VW_PRD_BOM_TRN A, INV_ITEM_MST B" & vbCrLf _
            & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf & " AND A.RM_CODE=B.ITEM_CODE "

        mSqlStr = mSqlStr & vbCrLf _
            & " START WITH  TRIM(A.RM_CODE) || '-' || A.COMPANY_CODE='" & MainClass.AllowSingleQuote(nItemCode) & "-" & RsCompany.Fields("COMPANY_CODE").Value & "'" & vbCrLf _
            & " CONNECT BY NOCYCLE (TRIM(A.PRODUCT_CODE) || A.COMPANY_CODE) || ' '=PRIOR TRIM(A.RM_CODE) || A.COMPANY_CODE || ' '"

        GetSearchOutwardItem = mSqlStr
        Exit Function
ErrPart:
        GetSearchOutwardItem = ""

    End Function

    Private Function ValidateOutwardCode(nItemCode As String, OutwardItemCode As String, pIsReProcess As String) As Boolean
        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCheckItemCode As String

        ValidateOutwardCode = False

        If Trim(nItemCode) = Trim(OutwardItemCode) Then
            ValidateOutwardCode = True
            Exit Function
        End If

        mSqlStr = "SELECT DISTINCT A.RM_CODE" & vbCrLf _
            & " FROM VW_PRD_BOM_TRN A" & vbCrLf _
            & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If pIsReProcess = "Y" Then
            mSqlStr = mSqlStr & vbCrLf _
                & " START WITH  TRIM(A.RM_CODE) || '-' || A.COMPANY_CODE='" & MainClass.AllowSingleQuote(nItemCode) & "-" & RsCompany.Fields("COMPANY_CODE").Value & "'" & vbCrLf _
                & " CONNECT BY NOCYCLE (TRIM(A.RM_CODE) || A.COMPANY_CODE) || ' '=PRIOR TRIM(A.PRODUCT_CODE) || A.COMPANY_CODE || ' '"

        Else
            mSqlStr = mSqlStr & vbCrLf _
                & " START WITH  TRIM(A.PRODUCT_CODE) || '-' || A.COMPANY_CODE='" & MainClass.AllowSingleQuote(nItemCode) & "-" & RsCompany.Fields("COMPANY_CODE").Value & "'" & vbCrLf _
                & " CONNECT BY NOCYCLE (TRIM(A.PRODUCT_CODE) || A.COMPANY_CODE) || ' '=PRIOR TRIM(A.RM_CODE) || A.COMPANY_CODE || ' '"

        End If

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        Do While RsTemp.EOF = False
            mCheckItemCode = IIf(IsDBNull(RsTemp.Fields("RM_CODE").Value), "", RsTemp.Fields("RM_CODE").Value)
            If Trim(mCheckItemCode) = Trim(OutwardItemCode) Then
                ValidateOutwardCode = True
                Exit Function
            End If
            RsTemp.MoveNext()
        Loop

        mSqlStr = " SELECT DISTINCT ITEM_CODE, PARENT_CODE " & vbCrLf _
                & " FROM INV_ITEM_MST" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND (ITEM_CODE='" & MainClass.AllowSingleQuote(nItemCode) & "' OR PARENT_CODE='" & MainClass.AllowSingleQuote(nItemCode) & "')"

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            ValidateOutwardCode = True
            Exit Function
        End If

        Exit Function
ErrPart:
        ValidateOutwardCode = False

    End Function

    Private Function GetValidItem(ByRef pItemCode As String) As Boolean

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim xSuppCode As String
        Dim RsTemp As ADODB.Recordset = Nothing

        xSuppCode = IIf(Trim(txtCode.Text) = "", "-1", Trim(txtCode.Text))


        'mSqlStr = "SELECT B.ITEM_CODE, B.ITEM_APPROVED " & vbCrLf _
        '    & " FROM INV_ITEM_MST A, FIN_SUPP_CUST_DET B" & vbCrLf _
        '    & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        '    & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf _
        '    & " AND A.ITEM_CODE=B.ITEM_CODE " & vbCrLf _
        '    & " AND B.SUPP_CUST_CODE='" & xSuppCode & "'" & vbCrLf & " AND B.ITEM_CODE='" & Trim(pItemCode) & "'"
        'MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        'If RsTemp.EOF = False Then
        '    GetValidItem = True
        '    If RsTemp.Fields("ITEM_APPROVED").Value = "N" And VB.Right(lblBookType.Text, 1) = "O" Then
        '        MsgInformation("Item is Not Approved for such Supplier.")
        '        GetValidItem = False
        '    End If
        'Else
        '    If VB.Right(lblBookType.Text, 1) = "O" Then
        '        MsgInformation("Please Check Item In Supplier Customer Detail Master.")
        '        GetValidItem = False
        '    Else
        If MainClass.ValidateWithMasterTable(Trim(pItemCode), "ITEM_CODE", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            GetValidItem = True
        Else
            MsgInformation("Please Check Item.")
            GetValidItem = False
        End If
        '    End If
        'End If

        Exit Function
ErrPart:
        GetValidItem = False
    End Function
    Private Function SelectQryForPO(ByRef mSqlStr As String, ByRef pItemCodeWisePrint As Boolean) As String

        ''SELECT CLAUSE...			

        If lblRMPO.Text = "R" Then
            mSqlStr = " SELECT " & vbCrLf & " IH.*, TEMP_PO.*,"
        Else
            mSqlStr = " SELECT " & vbCrLf & " IH.*, ID.*,TEMP_PO.*,"
        End If


        mSqlStr = mSqlStr & " CMST.SUPP_CUST_NAME, " & vbCrLf _
             & " BCMST.*"

        ''FROM CLAUSE...			


        If lblRMPO.Text = "R" Then
            mSqlStr = mSqlStr & vbCrLf & " FROM PUR_PURCHASE_HDR IH,  " & vbCrLf _
                    & " FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_BUSINESS_MST BCMST, FIN_PAYTERM_MST PAYMST, Temp_PO_PRN TEMP_PO"
        Else
            mSqlStr = mSqlStr & vbCrLf & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID, " & vbCrLf _
                    & " FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_BUSINESS_MST BCMST, FIN_PAYTERM_MST PAYMST, Temp_PO_PRN TEMP_PO"
        End If

        'SqlStr = "SELECT A.*, B.SUPP_CUST_NAME FROM FIN_SUPP_CUST_BUSINESS_MST A, FIN_SUPP_CUST_MST B" & vbCrLf _
        '    & " Where A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND A.COMPANY_CODE=B.COMPANY_CODE AND A.SUPP_CUST_CODE=B.SUPP_CUST_CODE" & vbCrLf _
        '    & " AND B.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(mShipToName) & "' AND A.LOCATION_ID='" & MainClass.AllowSingleQuote(TxtShipTo.Text) & "'"


        'WHERE CLAUSE...	

        If lblRMPO.Text = "R" Then
            mSqlStr = mSqlStr & vbCrLf _
                & " WHERE " & vbCrLf _
                & " IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
                & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
                & " AND CMST.COMPANY_CODE=BCMST.COMPANY_CODE" & vbCrLf _
                & " AND CMST.SUPP_CUST_CODE=BCMST.SUPP_CUST_CODE AND BCMST.LOCATION_ID='" & Trim(txtBillTo.Text) & "'" & vbCrLf _
                & " AND IH.COMPANY_CODE=PAYMST.COMPANY_CODE(+)" & vbCrLf _
                & " AND IH.PAYMENT_CODE=PAYMST.PAY_TERM_CODE(+)" & vbCrLf _
                & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND IH.AUTO_KEY_PO=" & Val(txtPONo.Text) & "" & vbCrLf _
                & " AND IH.AMEND_NO=" & Val(txtAmendNo.Text) & "" & vbCrLf _
                & " AND IH.RM_DESC=TEMP_PO.ITEM_SHORT_DESC" & vbCrLf _
                & " AND TEMP_PO.USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        Else
            mSqlStr = mSqlStr & vbCrLf _
                & " WHERE " & vbCrLf _
                & " IH.MKEY=ID.MKEY" & vbCrLf _
                & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
                & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
                & " AND CMST.COMPANY_CODE=BCMST.COMPANY_CODE" & vbCrLf _
                & " AND CMST.SUPP_CUST_CODE=BCMST.SUPP_CUST_CODE AND BCMST.LOCATION_ID='" & Trim(txtBillTo.Text) & "'" & vbCrLf _
                & " AND IH.COMPANY_CODE=PAYMST.COMPANY_CODE(+)" & vbCrLf _
                & " AND IH.PAYMENT_CODE=PAYMST.PAY_TERM_CODE(+)" & vbCrLf _
                & " AND ID.ITEM_CODE=TEMP_PO.ITEM_CODE" & vbCrLf _
                & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND IH.AUTO_KEY_PO=" & Val(txtPONo.Text) & "" & vbCrLf _
                & " AND IH.AMEND_NO=" & Val(txtAmendNo.Text) & "" & vbCrLf _
                & " AND TEMP_PO.USERID='" & MainClass.AllowSingleQuote(PubUserID) & "' AND TEMP_PO.PRINT_STATUS='Y'"
        End If

        '' AND ID.SERIAL_NO=TEMP_PO.SUBROWNO

        'mSqlStr = mSqlStr & vbCrLf _
        '    & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
        '    & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
        '    & " AND CMST.COMPANY_CODE=BCMST.COMPANY_CODE" & vbCrLf _
        '    & " AND CMST.SUPP_CUST_CODE=BCMST.SUPP_CUST_CODE AND BCMST.LOCATION_ID='" & Trim(txtBillTo.Text) & "'" & vbCrLf _
        '    & " AND IH.COMPANY_CODE=PAYMST.COMPANY_CODE(+)" & vbCrLf _
        '    & " AND IH.PAYMENT_CODE=PAYMST.PAY_TERM_CODE(+)" & vbCrLf _
        '    & " AND ID.ITEM_CODE=TEMP_PO.ITEM_CODE" & vbCrLf _
        '    & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        '    & " AND IH.AUTO_KEY_PO=" & Val(txtPONo.Text) & "" & vbCrLf _
        '    & " AND IH.AMEND_NO=" & Val(txtAmendNo.Text) & "" & vbCrLf _
        '    & " AND TEMP_PO.USERID='" & MainClass.AllowSingleQuote(PubUserID) & "' AND TEMP_PO.PRINT_STATUS='Y'"
        'End If

        'ORDER CLAUSE...			

        If lblRMPO.Text = "R" Then
        Else
            If pItemCodeWisePrint = True Then
                mSqlStr = mSqlStr & vbCrLf & "ORDER BY ID.SERIAL_NO"
            Else
                mSqlStr = mSqlStr & vbCrLf & "ORDER BY TEMP_PO.ITEM_SHORT_DESC"
            End If
        End If


        SelectQryForPO = mSqlStr
    End Function

    Private Function SelectQryForAnnex(ByRef mSqlStr As String) As String

        ''SELECT CLAUSE...			

        mSqlStr = " SELECT " & vbCrLf & " IH.*, ANNEX.*, "

        mSqlStr = mSqlStr & " CMST.SUPP_CUST_NAME, CMST.SUPP_CUST_ADDR, " & vbCrLf & " CMST.SUPP_CUST_CITY, CMST.SUPP_CUST_STATE, " & vbCrLf & " CMST.SUPP_CUST_PIN, CMST.SUPP_CUST_PHONE, " & vbCrLf & " CMST.SUPP_CUST_FAXNO, CMST.SUPP_CUST_MAILID, " & vbCrLf & " CMST.SUPP_CUST_MOBILE, CMST.CST_NO, " & vbCrLf & " CMST.LST_NO, CMST.PAN_NO, " & vbCrLf & " CMST.EXCISE_DIV, CMST.EXCISE_RANGE, " & vbCrLf & " CMST.CENT_EXC_RGN_NO, CMST.ECC_NO, " & vbCrLf & " CMST.SUPP_CUST_REMARKS, CMST.WITHIN_STATE, " & vbCrLf & " CMST.WITHIN_DISTT, CMST.COMMISIONER_RATE, " & vbCrLf & " CMST.REGD_DEALER, CMST.DATE_OF_APPROVAL, WITHIN_STATE"

        ''FROM CLAUSE...			
        mSqlStr = mSqlStr & vbCrLf & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_ANNEX ANNEX, " & vbCrLf & " FIN_SUPP_CUST_MST CMST, FIN_PAYTERM_MST PAYMST "

        ''WHERE CLAUSE...			
        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.MKEY=ANNEX.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND IH.COMPANY_CODE=PAYMST.COMPANY_CODE(+)" & vbCrLf & " AND IH.PAYMENT_CODE=PAYMST.PAY_TERM_CODE(+)" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=" & Val(lblMkey.Text) & ""
        ''ORDER CLAUSE...			

        mSqlStr = mSqlStr & vbCrLf & "ORDER BY ANNEX.SERIAL_NO"

        SelectQryForAnnex = mSqlStr
    End Function
    Private Function SelectQryForWO(ByRef mSqlStr As String) As String

        ''SELECT CLAUSE...			

        mSqlStr = " SELECT " & vbCrLf & " IH.*, ID.*, TEMP_PO.*,"

        mSqlStr = mSqlStr & " CMST.SUPP_CUST_NAME, CMST.SUPP_CUST_ADDR, " & vbCrLf _
            & " CMST.SUPP_CUST_CITY, CMST.SUPP_CUST_STATE, " & vbCrLf _
            & " CMST.SUPP_CUST_PIN, CMST.SUPP_CUST_PHONE, " & vbCrLf _
            & " CMST.SUPP_CUST_FAXNO, CMST.SUPP_CUST_MAILID, " & vbCrLf _
            & " CMST.SUPP_CUST_MOBILE, CMST.CST_NO, " & vbCrLf _
            & " CMST.LST_NO, CMST.PAN_NO, " & vbCrLf _
            & " CMST.EXCISE_DIV, CMST.EXCISE_RANGE, " & vbCrLf _
            & " CMST.CENT_EXC_RGN_NO, CMST.ECC_NO, " & vbCrLf _
            & " CMST.SUPP_CUST_REMARKS, CMST.WITHIN_STATE, " & vbCrLf _
            & " CMST.WITHIN_DISTT, CMST.COMMISIONER_RATE, " & vbCrLf & " CMST.REGD_DEALER, CMST.DATE_OF_APPROVAL, WITHIN_STATE"

        ''FROM CLAUSE...			
        mSqlStr = mSqlStr & vbCrLf & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID, " & vbCrLf _
            & " FIN_SUPP_CUST_MST CMST, FIN_PAYTERM_MST PAYMST,Temp_PO_PRN TEMP_PO "

        ''WHERE CLAUSE...			
        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND IH.COMPANY_CODE=PAYMST.COMPANY_CODE(+)" & vbCrLf & " AND IH.PAYMENT_CODE=PAYMST.PAY_TERM_CODE(+)" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_PO=" & Val(txtPONo.Text) & "" & vbCrLf & " AND IH.AMEND_NO=" & Val(txtAmendNo.Text) & "" & vbCrLf & " AND ID.WO_DESCRIPTION=TEMP_PO.ITEM_SHORT_DESC" & vbCrLf & " AND TEMP_PO.USERID='" & MainClass.AllowSingleQuote(PubUserID) & "' AND TEMP_PO.PRINT_STATUS='Y'"


        ''ORDER CLAUSE...			

        mSqlStr = mSqlStr & vbCrLf & "ORDER BY ID.SERIAL_NO"

        SelectQryForWO = mSqlStr
    End Function
    Private Sub TxtWef_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtWEF.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtWEF.Text) = "" Then GoTo EventExitSub

        If IsDate(txtWEF.Text) = False Then
            MsgInformation(" Invalid Date.")
            Cancel = True
        End If

        If CDate(txtWEF.Text) < CDate(PubGSTApplicableDate) Then
            MsgInformation("WEF Date Should be Greater than GST Applicable date.")
            Cancel = True
        End If

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub SprdExp_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdExp.ButtonClicked
        If pShowCalc = True Then ''If FormActive = True Then			
            SprdExp.Col = ColExpAmt
            SprdExp.Row = eventArgs.row
            If Val(SprdExp.Text) <> 0 Then
                Call CalcTots()
            End If
        End If
    End Sub
    Private Function UpdatePOExp1() As Boolean
        On Error GoTo UpdateSaleExpErr1
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mExpCode As Integer
        Dim mPercent As Double
        Dim mExpAmount As Double
        Dim m_AD As String
        Dim mCalcOn As Double
        Dim mRO As String
        Dim mDebitAmount As String

        PubDBCn.Execute("Delete From PUR_PURCHASE_EXP Where MKEY='" & lblMkey.Text & "'")
        With SprdExp
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColExpName
                If MainClass.ValidateWithMasterTable(.Text, "Name", "Code", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mExpCode = MasterNo
                Else
                    mExpCode = -1
                End If

                .Col = ColExpPercent
                mPercent = Val(.Text)

                .Col = ColExpAmt
                mExpAmount = Val(.Text)

                SprdExp.Col = ColExpAddDeduct
                m_AD = SprdExp.Text
                If m_AD = "D" Then
                    mExpAmount = mExpAmount * -1
                End If

                SprdExp.Col = ColExpCalcOn
                mCalcOn = Val(.Text)


                .Col = ColRO
                mRO = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                SqlStr = ""
                If mCalcOn <> 0 Or mExpAmount <> 0 Or mPercent <> 0 Then
                    SqlStr = "Insert Into  PUR_PURCHASE_EXP (MKEY,SUBROWNO, " & vbCrLf & " EXPCODE,EXPPERCENT,AMOUNT,CalcOn,RO) " & vbCrLf & " Values (" & Val(lblMkey.Text) & "," & I & ", " & vbCrLf & " " & mExpCode & "," & mPercent & "," & mExpAmount & ", " & vbCrLf & " " & mCalcOn & ",'" & mRO & "')"
                    PubDBCn.Execute(SqlStr)
                End If
            Next I
        End With
        UpdatePOExp1 = True

        Exit Function
UpdateSaleExpErr1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdatePOExp1 = False
    End Function

    Private Function UpdatePOAnnex() As Boolean

        On Error GoTo UpdateSaleExpErr1
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mDesc As String

        SqlStr = "Delete From  PUR_PURCHASE_ANNEX " & vbCrLf & " Where " & vbCrLf & " MKEY=" & Val(lblMkey.Text) & ""

        PubDBCn.Execute(SqlStr)

        With SprdAnnex
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColAnnexDesc
                mDesc = MainClass.AllowSingleQuote(.Text)

                SqlStr = ""
                If Trim(mDesc) <> "" Then
                    SqlStr = "Insert Into  PUR_PURCHASE_ANNEX ( " & vbCrLf & " MKEY, SERIAL_NO, " & vbCrLf & " ANNEX_TITLE, DESCRIPTION ) " & vbCrLf & " Values ( " & vbCrLf & " " & Val(lblMkey.Text) & ", " & I & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtAnnexTitle.Text) & "', " & vbCrLf & " '" & mDesc & "')"

                    PubDBCn.Execute(SqlStr)
                End If
            Next I
        End With
        UpdatePOAnnex = True

        Exit Function
UpdateSaleExpErr1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdatePOAnnex = False
    End Function

    Private Sub FormatSprdExp(ByRef Arow As Integer)

        On Error GoTo ERR1
        pShowCalc = False
        With SprdExp
            .Row = Arow
            .set_RowHeight(Arow, 10)

            .Col = ColExpName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColExpName, 22)

            .Col = ColExpPercent
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatMin = 0.0#
            .TypeFloatMax = 99.99
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeEditMultiLine = False

            .Col = ColExpAmt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatMin = -99999999999.99
            .TypeFloatMax = 99999999999.99
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(ColExpAmt, 8)
            .TypeEditMultiLine = False

            .Col = ColExpSTCode
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 0
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMin = CDbl("-9999999999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .ColHidden = True

            .Col = ColExpAddDeduct 'ExpFlag (For Add or Deduct) Hidden Column			
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .ColHidden = True

            .Col = ColExpIdent
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .ColHidden = True

            .Col = ColTaxable
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .ColHidden = True

            .Col = ColExciseable
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .ColHidden = True

            SprdExp.Col = ColExpCalcOn
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatMin = -99999999999.99
            .TypeFloatMax = 99999999999.99
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeEditMultiLine = False
            .ColHidden = True

            .Col = ColRO
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColRO, 2)
            '.Value = vbUnchecked			

            MainClass.ProtectCell(SprdExp, 1, .MaxRows, ColExpName, ColExpName)


        End With
        pShowCalc = True
        MainClass.SetSpreadColor(SprdExp, Arow)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume			
    End Sub

    Private Function GetMaxAmendNo(ByRef pPONO As Double) As Integer

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = " SELECT MAX(AMEND_NO) AS AMEND_NO" & vbCrLf & " FROM PUR_PURCHASE_HDR" & vbCrLf & " WHERE AUTO_KEY_PO=" & Val(CStr(pPONO)) & "" '& vbCrLf |        & " AND PO_STATUS='Y' " & vbCrLf |        & " AND PO_CLOSED='N' "			

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

    Private Function CheckUnPostedPO(ByRef pPONO As Double) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing


        CheckUnPostedPO = False

        SqlStr = " SELECT Count(1) AS CNTPO" & vbCrLf & " FROM PUR_PURCHASE_HDR" & vbCrLf & " WHERE AUTO_KEY_PO=" & Val(CStr(pPONO)) & "" & vbCrLf & " AND PO_STATUS='N' " '& vbCrLf |        & " AND PO_CLOSED='N' "			

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If IsDBNull(RsTemp.Fields("CNTPO").Value) Or RsTemp.Fields("CNTPO").Value < 1 Then
                CheckUnPostedPO = False
            Else
                MsgInformation("There are " & RsTemp.Fields("CNTPO").Value & " UnPosted PO. So Please Post UnPosted PO - " & pPONO)
                CheckUnPostedPO = True
            End If
        Else
            CheckUnPostedPO = False
        End If

        Exit Function
ErrPart:
        CheckUnPostedPO = True
    End Function


    Private Function CheckPreviousPOExists(ByRef pSupplierCode As String, ByRef pPONO As String, ByRef mShipToCode As String, pPOOrderType As String) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xPoNo As Double
        Dim CntRow As Integer
        Dim pItemCode As String
        Dim pOutwardCode As String

        CheckPreviousPOExists = False

        If Trim(pPONO) = "" Then
            '        xPoNo = VB6.Format(RsCompany.fields("FYEAR").value, "0000") & VB6.Format(RsCompany.fields("COMPANY_CODE").value, "00")			
            xPoNo = -1
        Else
            xPoNo = Val(pPONO)
        End If


        With SprdMain
            For CntRow = 1 To .MaxRows - 1
                .Row = CntRow
                .Col = ColItemCode
                pItemCode = Trim(.Text)

                pOutwardCode = ""
                If VB.Left(lblBookType.Text, 1) = "J" Then
                    .Col = ColOutWardCode
                    pOutwardCode = Trim(.Text)
                End If

                SqlStr = "SELECT DISTINCT AUTO_KEY_PO " & vbCrLf _
                    & " FROM PUR_PURCHASE_HDR IH,PUR_PURCHASE_DET ID " & vbCrLf _
                    & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
                    & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

                SqlStr = SqlStr & vbCrLf & " AND ISGSTENABLE_PO='Y'"

                SqlStr = SqlStr & vbCrLf _
                    & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pSupplierCode) & "'" & vbCrLf _
                    & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
                    & " AND IH.PUR_TYPE='" & VB.Left(lblBookType.Text, 1) & "'" & vbCrLf _
                    & " AND IH.ORDER_TYPE='" & pPOOrderType & "' AND PO_CLOSED='N' "

                If pPOOrderType = "C" And VB.Left(lblBookType.Text, 1) = "P" Then
                    'SqlStr = SqlStr & vbCrLf _
                    '    & " AND ID.ITEM_QTY-GETMRRQTYFORPO(IH.Company_Code, IH.AUTO_KEY_PO, IH.SUPP_CUST_CODE, ID.ITEM_CODE) > 0 " Then

                    SqlStr = SqlStr & vbCrLf _
                        & " AND AUTO_KEY_PO<>" & xPoNo & ""

                    SqlStr = SqlStr & vbCrLf _
                            & " And PO_ITEM_STATUS ='N' AND ID.ITEM_QTY -NVL(GETMRRQTYFORPO(IH.COMPANY_CODE, IH.AUTO_KEY_PO, IH.SUPP_CUST_CODE, ID.ITEM_CODE), 0)>0"

                End If
                ''NVL(GetMRRQty(IH.COMPANY_CODE, IH.PUR_TYPE, IH.AUTO_KEY_PO, IH.PUR_ORD_DATE, IH.SUPP_CUST_CODE, ID.ITEM_CODE), 0)

                If pOutwardCode <> "" Then
                    SqlStr = SqlStr & vbCrLf _
                    & " AND ID.OUTWARD_ITEM_CODE='" & MainClass.AllowSingleQuote(pOutwardCode) & "'"
                End If

                If chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked Then
                    SqlStr = SqlStr & vbCrLf & " AND SHIPPED_TO_SAMEPARTY='Y'"
                Else
                    SqlStr = SqlStr & vbCrLf & " AND SHIPPED_TO_PARTY_CODE='" & mShipToCode & "'"
                End If

                SqlStr = SqlStr & vbCrLf & " AND DIV_CODE=" & Val(txtDivision.Text) & ""

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTemp.EOF = False Then
                    If RsTemp.Fields("AUTO_KEY_PO").Value <> xPoNo Then
                        MsgInformation("Item Code : " & pItemCode & " Already made. Against  PO No. : " & RsTemp.Fields("AUTO_KEY_PO").Value)
                        CheckPreviousPOExists = True
                        Exit Function
                    End If
                End If
            Next
        End With
        Exit Function
ErrPart:
        CheckPreviousPOExists = True
    End Function

    Private Function CheckItemRateFromCosting() As Boolean

        On Error GoTo ErrPart
        Dim CntRow As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim pItemCode As String
        Dim pItemRate As Double
        Dim pItemDisc As Double
        Dim mCostingReq As String
        Dim mCostingRate As String
        Dim mPrevRate As Double
        Dim xSupplierCode As String = ""

        CheckItemRateFromCosting = False

        If RsCompany.Fields("PO_LOCK").Value = "N" Then
            CheckItemRateFromCosting = True
            Exit Function
        End If

        If chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked Then
            xSupplierCode = Trim(txtCode.Text)
        Else
            If MainClass.ValidateWithMasterTable((txtShippedTo.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                xSupplierCode = MasterNo
            End If
        End If

        With SprdMain
            For CntRow = 1 To .MaxRows
                .Row = CntRow
                .Col = ColItemCode
                pItemCode = Trim(.Text)

                If GetCostingRequired(pItemCode) = True Then
                    .Col = ColItemRate
                    pItemRate = Val(.Text)

                    .Col = ColItemDisc
                    pItemDisc = Val(.Text)

                    pItemRate = pItemRate - (pItemDisc * 0.01 * pItemRate)

                    .Col = ColGross_Prev
                    If Val(txtAmendNo.Text) = 0 Then
                        mPrevRate = 0
                    Else
                        mPrevRate = CDbl(VB6.Format(GetPreviousItemGross(pItemCode, ""), "0.0000"))
                    End If


                    If mPrevRate = pItemRate Then GoTo NextRow

                    '                SqlStr = "SELECT COSTING_REQ " & vbCrLf _			
                    ''                        & " FROM FIN_SUPP_CUST_DET " & vbCrLf _			
                    ''                        & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _			
                    ''                        & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xSupplierCode) & "'" & vbCrLf _			
                    ''                        & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"			
                    '			
                    '                MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly			
                    '                If RsTemp.EOF = False Then			
                    '                    mCostingReq = IIf(IsNull(RsTemp!COSTING_REQ), "N", RsTemp!COSTING_REQ)			
                    '                    If mCostingReq = "Y" Then			
                    SqlStr = "SELECT ID.RATE " & vbCrLf & " FROM PRD_VENDOR_COST_HDR IH, PRD_VENDOR_COST_DET ID" & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=ID.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=ID.SUPP_CUST_CODE " & vbCrLf & " AND IH.WEF_DATE=ID.WEF_DATE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xSupplierCode) & "'" & vbCrLf & " AND IH.WEF_DATE=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf & " AND IH.CANCELLED='N'"

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                    If RsTemp.EOF = False Then
                        mCostingRate = IIf(IsDBNull(RsTemp.Fields("Rate").Value), 0, RsTemp.Fields("Rate").Value)

                        If VB6.Format(mCostingRate, "0.00") = VB6.Format(pItemRate, "0.00") Then 'If CDbl(mCostingRate) = pItemRate Then  'VB6.Format(mNetCost, "0.00")
                            GoTo NextRow
                        Else
                            MsgInformation("Item Code " & pItemCode & " - Costing Rate Rs. " & mCostingRate & " is not match with PO Rate. Rs. " & pItemRate)
                            MainClass.SetFocusToCell(SprdMain, CntRow, ColItemRate)
                            CheckItemRateFromCosting = False
                            Exit Function
                        End If
                    Else
                        MsgInformation("Please Entered Costing Rate in Costing Module.")
                        MainClass.SetFocusToCell(SprdMain, CntRow, ColItemRate)
                        CheckItemRateFromCosting = False
                        Exit Function
                    End If

                    '                   End If			
                    '                End If			
                End If
NextRow:
            Next
        End With
        CheckItemRateFromCosting = True
        Exit Function
ErrPart:
        CheckItemRateFromCosting = False
    End Function
    Private Sub SetCurrency()
        On Error GoTo ErrPart
        Dim mCurr As String = ""
        If MainClass.ValidateWithMasterTable((txtCode.Text), "SUPP_CUST_CODE", "CURRENCYNAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCurr = MasterNo
        End If


        SprdMain.Row = 0
        SprdMain.Col = ColItemRate
        SprdMain.Text = "Price" & vbNewLine & "(" & mCurr & ")"
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function GetRMDrawingRate(ByRef pItemCode As String) As Double

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mSupplierCode As String

        GetRMDrawingRate = 0

        mSqlStr = "SELECT NVL(ID.ITEM_PRICE,0) AS ITEM_PRICE " & vbCrLf _
            & " FROM PUR_RM_DWG_RATE_HDR IH, PUR_RM_DWG_RATE_DET ID" & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "' " & vbCrLf _
            & " AND IH.BILL_TO_LOC_ID='" & MainClass.AllowSingleQuote(txtBillTo.Text) & "'" & vbCrLf _
            & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "' AND PO_STATUS='Y'"

        mSqlStr = mSqlStr & vbCrLf _
            & " AND IH.MKEY IN (SELECT MAX(A.MKEY) FROM PUR_RM_DWG_RATE_HDR A, PUR_RM_DWG_RATE_DET B" & vbCrLf _
            & " WHERE A.MKEY=B.MKEY" & vbCrLf _
            & " AND A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND A.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "' " & vbCrLf _
            & " AND A.BILL_TO_LOC_ID='" & MainClass.AllowSingleQuote(txtBillTo.Text) & "'" & vbCrLf _
            & " AND B.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "' AND PO_STATUS='Y'" & vbCrLf _
            & " AND A.AMEND_WEF_DATE <=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " )"


        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetRMDrawingRate = IIf(IsDBNull(RsTemp.Fields("ITEM_PRICE").Value), 0, RsTemp.Fields("ITEM_PRICE").Value)
        End If

        Exit Function
ErrPart:
        GetRMDrawingRate = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function GetPreviousItemGross(ByRef pItemCode As String, ByRef pWODesc As String) As Double

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing

        GetPreviousItemGross = 0

        mSqlStr = "SELECT (NVL(ID.ITEM_PRICE,0) - ROUND((NVL(ID.ITEM_PRICE,0) * ID.ITEM_DIS_PER)/100,4)) AS GROSS_AMT " & vbCrLf & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID" & vbCrLf & " WHERE IH.MKEy=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        mSqlStr = mSqlStr & vbCrLf & " AND IH.AUTO_KEY_PO=" & Val(txtPONo.Text) & "" & vbCrLf & " AND IH.PUR_TYPE='" & VB.Left(lblBookType.Text, 1) & "'" & vbCrLf & " AND IH.ORDER_TYPE='" & VB.Right(lblBookType.Text, 1) & "'"

        If VB.Left(lblBookType.Text, 1) = "W" Or VB.Left(lblBookType.Text, 1) = "R" Then
            mSqlStr = mSqlStr & vbCrLf & " AND ID.WO_DESCRIPTION='" & MainClass.AllowSingleQuote(pWODesc) & "'"
        Else
            If Trim(pItemCode) <> "" Then
                mSqlStr = mSqlStr & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"
            End If
        End If

        If Trim(txtAmendNo.Text) <> "" Then
            mSqlStr = mSqlStr & vbCrLf & " AND IH.AMEND_NO=" & Val(txtAmendNo.Text) - 1 & ""
        End If

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetPreviousItemGross = IIf(IsDBNull(RsTemp.Fields("GROSS_AMT").Value), 0, RsTemp.Fields("GROSS_AMT").Value)
        End If

        Exit Function
ErrPart:
        GetPreviousItemGross = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function GetPreviousItemWEFDate(ByRef pItemCode As String, ByRef pWODesc As String) As String

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing

        GetPreviousItemWEFDate = ""

        mSqlStr = "SELECT PO_WEF_DATE " & vbCrLf & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID" & vbCrLf & " WHERE IH.MKEy=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "


        mSqlStr = mSqlStr & vbCrLf & " AND IH.AUTO_KEY_PO=" & Val(txtPONo.Text) & "" & vbCrLf & " AND IH.PUR_TYPE='" & VB.Left(lblBookType.Text, 1) & "'" & vbCrLf & " AND IH.ORDER_TYPE='" & VB.Right(lblBookType.Text, 1) & "'"

        If VB.Left(lblBookType.Text, 1) = "W" Then
            mSqlStr = mSqlStr & vbCrLf & " AND ID.WO_DESCRIPTION='" & MainClass.AllowSingleQuote(pWODesc) & "'"
        Else
            If Trim(pItemCode) <> "" Then
                mSqlStr = mSqlStr & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"
            End If
        End If

        If Trim(txtAmendNo.Text) <> "" Then
            mSqlStr = mSqlStr & vbCrLf & " AND IH.AMEND_NO=" & Val(txtAmendNo.Text) - 1 & ""
        End If

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetPreviousItemWEFDate = IIf(IsDBNull(RsTemp.Fields("PO_WEF_DATE").Value), "", RsTemp.Fields("PO_WEF_DATE").Value)
        End If

        Exit Function
ErrPart:
        GetPreviousItemWEFDate = ""
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function


    Private Sub UpdateWEFInDetail(ByRef pRowCnt As Integer)
        On Error GoTo err_Renamed
        Dim mItemCode As String
        Dim pPervRate As Double
        Dim pCurrRate As Double
        Dim mPrice As Double
        Dim mDisc As Double

        Dim I As Integer

        '        For I = 1 To SprdMain.MaxRows - 1	
        SprdMain.Row = pRowCnt


        SprdMain.Col = ColItemCode
        mItemCode = Trim(UCase(SprdMain.Text))

        SprdMain.Col = ColItemRate
        mPrice = Val(SprdMain.Text)

        SprdMain.Col = ColItemDisc
        mDisc = Val(SprdMain.Text)

        pCurrRate = mPrice - System.Math.Round((mPrice * mDisc) / 100, 4)

        SprdMain.Col = ColGross_Prev
        pPervRate = Val(SprdMain.Text)

        If Val(CStr(pCurrRate)) <> Val(CStr(pPervRate)) Then
            SprdMain.Col = ColPO_WEF
            SprdMain.Text = VB6.Format(txtWEF.Text, "DD/MM/YYYY")
        End If
        '        Next	
        Exit Sub
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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

        If MainClass.ValidateWithMasterTable(txtBillTo.Text, "LOCATION_ID", "LOCATION_ID", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xAcctCode) & "'") = False Then
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
            txtBillTo_Validating(txtBillTo, New System.ComponentModel.CancelEventArgs(False))
            If txtBillTo.Enabled = True Then txtBillTo.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub TxtShipTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtShipTo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub TxtShipTo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtShipTo.DoubleClick
        cmdShipToSearch_Click(cmdShipToSearch, New System.EventArgs())
    End Sub
    Private Sub TxtShipTo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtShipTo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtShipTo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub TxtShipTo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtShipTo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdShipToSearch_Click(cmdShipToSearch, New System.EventArgs())
    End Sub
    Private Sub TxtShipTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtShipTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim xAcctCode As String

        If Trim(txtShippedTo.Text) = "" Then GoTo EventExitSub
        If Trim(TxtShipTo.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(Trim(txtShippedTo.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
            xAcctCode = MasterNo
        Else
            MsgBox("Invalid Shipped Customer Name.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

        If MainClass.ValidateWithMasterTable(TxtShipTo.Text, "LOCATION_ID", "LOCATION_ID", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xAcctCode) & "'") = False Then
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

        If Trim(txtShippedTo.Text) = "" Then
            MsgInformation("Please select the Customer First")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(txtShippedTo.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
            xAcctCode = MasterNo
        End If

        SqlStr = "SELECT LOCATION_ID, SUPP_CUST_ADDR, SUPP_CUST_CITY, SUPP_CUST_STATE, GST_RGN_NO " & vbCrLf _
                & " FROM FIN_SUPP_CUST_BUSINESS_MST" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " And SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xAcctCode) & "'"

        If MainClass.SearchGridMasterBySQL2((TxtShipTo.Text), SqlStr) = True Then
            TxtShipTo.Text = AcName
            TxtShipTo_Validating(TxtShipTo, New System.ComponentModel.CancelEventArgs(False))
            If TxtShipTo.Enabled = True Then TxtShipTo.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtDeliveryToLoc_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtDeliveryToLoc.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtDeliveryToLoc_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtDeliveryToLoc.DoubleClick
        cmdDeliveryToLocSearch_Click(cmdDeliveryToLocSearch, New System.EventArgs())
    End Sub
    Private Sub txtDeliveryToLoc_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtDeliveryToLoc.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtDeliveryToLoc.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtDeliveryToLoc_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtDeliveryToLoc.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdDeliveryToLocSearch_Click(cmdDeliveryToLocSearch, New System.EventArgs())
    End Sub
    Private Sub txtDeliveryToLoc_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtDeliveryToLoc.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim xAcctCode As String

        If Trim(txtDeliveryTo.Text) = "" Then GoTo EventExitSub
        If Trim(TxtDeliveryToLoc.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(Trim(txtDeliveryTo.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
            xAcctCode = MasterNo
        Else
            MsgBox("Invalid Delivery To Supplier Name.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

        If MainClass.ValidateWithMasterTable(TxtDeliveryToLoc.Text, "LOCATION_ID", "LOCATION_ID", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xAcctCode) & "'") = False Then
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

    Private Sub cmdDeliveryToLocSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDeliveryToLocSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        Dim xAcctCode As String = ""

        If Trim(txtDeliveryTo.Text) = "" Then
            MsgInformation("Please select the Delivery Supplier First")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(txtDeliveryTo.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
            xAcctCode = MasterNo
        End If

        SqlStr = "SELECT LOCATION_ID, SUPP_CUST_ADDR, SUPP_CUST_CITY, SUPP_CUST_STATE, GST_RGN_NO " & vbCrLf _
                & " FROM FIN_SUPP_CUST_BUSINESS_MST" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " And SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xAcctCode) & "'"

        If MainClass.SearchGridMasterBySQL2((TxtDeliveryToLoc.Text), SqlStr) = True Then
            TxtDeliveryToLoc.Text = AcName
            txtDeliveryToLoc_Validating(TxtDeliveryToLoc, New System.ComponentModel.CancelEventArgs(False))
            If TxtDeliveryToLoc.Enabled = True Then TxtDeliveryToLoc.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtOldERPNo_TextChanged(sender As Object, e As EventArgs) Handles txtOldERPNo.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtOldERPNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOldERPNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtOldERPNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmPO_GST_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 350, mReFormWidth - 350, mReFormWidth))
        fraAccounts.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        Frame1.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        UltraGrid1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        FraTrn.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))

        TabMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        CurrFormWidth = mReFormWidth

        'MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdMain_KeyPressEvent(sender As Object, e As _DSpreadEvents_KeyPressEvent) Handles SprdMain.KeyPressEvent
        If FormActive = False Then Exit Sub
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

    Private Sub cmdInterUnitSO_Click(sender As Object, e As EventArgs) Handles cmdInterUnitSO.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim xAcctCode As String = ""
        Dim mInterUnitCompanyCode As Integer
        Dim mCurrentUnitAccountCode As String
        Dim mSOMKey As Double
        If txtSupplierName.Text = "" Then MsgInformation("Please Select the Inter Unit Name") : Exit Sub

        If VB.Left(lblBookType.Text, 1) <> "P" Then Exit Sub

        If MainClass.ValidateWithMasterTable(Trim(txtSupplierName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND INTER_UNIT='Y'") = True Then
            xAcctCode = MasterNo
        Else
            MsgInformation("Supplier is Not Inter Unit.")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(txtSupplierName.Text), "SUPP_CUST_NAME", "INTERUNIT_COMPANY_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND INTER_UNIT='Y'") = True Then
            mInterUnitCompanyCode = Val(MasterNo)
        Else
            MsgInformation("Supplier is Not Inter Unit.")
            Exit Sub
        End If

        If mInterUnitCompanyCode <= 0 Then
            MsgInformation("Supplier Not Link with any Units.")
            Exit Sub
        End If

        mCurrentUnitAccountCode = IIf(IsDBNull(RsCompany.Fields("COMP_AC_CODE").Value), "", RsCompany.Fields("COMP_AC_CODE").Value)

        If mCurrentUnitAccountCode = "" Then
            MsgInformation("Unit Not Link with Account Name.")
            Exit Sub
        End If



        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
            SqlStr = " SELECT " & vbCrLf _
                   & " A.MKEY AS SO_NO, A.SO_DATE AS SO_DATE, " & vbCrLf _
                   & " A.CUST_PO_NO AS PO_NO, IH.BILLNO, IH.INVOICE_DATE AS INVOICE_DATE, " & vbCrLf _
                   & " A.AMEND_NO, A.AMEND_DATE, " & vbCrLf _
                   & " A.AMEND_WEF_FROM AS WEF,A.SUPP_CUST_CODE, B.SUPP_CUST_NAME AS NAME, " & vbCrLf _
                   & " C.PART_NO, C.ITEM_CODE, D.ITEM_SHORT_DESC, C.SO_QTY, C.ITEM_PRICE"

            SqlStr = SqlStr & vbCrLf _
                        & " FROM DSP_SALEORDER_HDR A, DSP_SALEORDER_DET C, FIN_INVOICE_HDR IH, FIN_SUPP_CUST_MST B, INV_ITEM_MST D " & vbCrLf _
                        & " WHERE A.MKEY=C.MKEY AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf _
                        & " AND A.SUPP_CUST_CODE=B.SUPP_CUST_CODE " & vbCrLf _
                        & " AND A.COMPANY_CODE=IH.COMPANY_CODE " & vbCrLf _
                        & " AND A.AUTO_KEY_SO=IH.OUR_AUTO_KEY_SO " & vbCrLf _
                        & " AND A.COMPANY_CODE=D.COMPANY_CODE " & vbCrLf _
                        & " AND C.ITEM_CODE=D.ITEM_CODE " & vbCrLf _
                        & " AND ISGSTENABLE_PO='Y'"

            SqlStr = SqlStr & vbCrLf _
                        & " AND A.COMPANY_CODE=" & mInterUnitCompanyCode & " AND A.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mCurrentUnitAccountCode) & "'"

            SqlStr = SqlStr & vbCrLf & " AND A.SO_STATUS='O' AND SO_APPROVED='Y'"
        Else

            SqlStr = " SELECT " & vbCrLf _
                & " A.MKEY AS SO_NO, A.SO_DATE AS SO_DATE, " & vbCrLf _
                & " A.CUST_PO_NO AS PO_NO, A.CUST_PO_DATE AS PO_DATE, " & vbCrLf _
                & " A.AMEND_NO, A.AMEND_DATE, " & vbCrLf _
                & " A.AMEND_WEF_FROM AS WEF,A.SUPP_CUST_CODE, B.SUPP_CUST_NAME AS NAME, " & vbCrLf _
                & " C.PART_NO, C.ITEM_CODE, D.ITEM_SHORT_DESC, C.SO_QTY, C.ITEM_PRICE"

            SqlStr = SqlStr & vbCrLf _
                & " FROM DSP_SALEORDER_HDR A, DSP_SALEORDER_DET C, FIN_SUPP_CUST_MST B, INV_ITEM_MST D " & vbCrLf _
                & " WHERE A.MKEY=C.MKEY AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf _
                & " AND A.SUPP_CUST_CODE=B.SUPP_CUST_CODE " & vbCrLf _
                & " AND A.COMPANY_CODE=D.COMPANY_CODE " & vbCrLf _
                & " AND C.ITEM_CODE=D.ITEM_CODE " & vbCrLf _
                & " AND ISGSTENABLE_PO='Y'"

            SqlStr = SqlStr & vbCrLf _
                & " AND A.COMPANY_CODE=" & mInterUnitCompanyCode & " AND A.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mCurrentUnitAccountCode) & "'"

            SqlStr = SqlStr & vbCrLf & " AND A.SO_STATUS='O' AND SO_APPROVED='Y'"

            If RsCompany.Fields("IS_WAREHOUSE").Value = "Y" Then   ''IS_WAREHOUSE

                SqlStr = SqlStr & vbCrLf & " UNION ALL "

                SqlStr = SqlStr & vbCrLf _
                        & " SELECT " & vbCrLf _
                        & " A.MKEY AS SO_NO, A.SO_DATE AS SO_DATE, " & vbCrLf _
                        & " A.CUST_PO_NO AS PO_NO, A.CUST_PO_DATE AS PO_DATE, " & vbCrLf _
                        & " A.AMEND_NO, A.AMEND_DATE, " & vbCrLf _
                        & " A.AMEND_WEF_FROM AS WEF,A.SUPP_CUST_CODE, B.SUPP_CUST_NAME AS NAME, " & vbCrLf _
                        & " C.PART_NO, C.ITEM_CODE, D.ITEM_SHORT_DESC, C.SO_QTY, C.ITEM_PRICE"

                SqlStr = SqlStr & vbCrLf _
                        & " FROM DSP_SALEORDER_HDR A, DSP_SALEORDER_DET C, FIN_SUPP_CUST_MST B, INV_ITEM_MST D " & vbCrLf _
                        & " WHERE A.MKEY=C.MKEY AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf _
                        & " AND A.SUPP_CUST_CODE=B.SUPP_CUST_CODE " & vbCrLf _
                        & " AND A.COMPANY_CODE=D.COMPANY_CODE " & vbCrLf _
                        & " AND C.ITEM_CODE=D.ITEM_CODE " & vbCrLf _
                        & " AND ISGSTENABLE_PO='Y'"

                SqlStr = SqlStr & vbCrLf _
                    & " AND A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

                SqlStr = SqlStr & vbCrLf & " AND A.SO_STATUS='O' AND SO_APPROVED='Y'"

            End If
        End If

        SqlStr = SqlStr & " ORDER BY 1,5"



        If MainClass.SearchGridMasterBySQL2((TxtShipTo.Text), SqlStr) = True Then
            mSOMKey = AcName
        End If

        If Val(mSOMKey) > 0 Then
            Call ShowFromSO(mSOMKey)
            'End If
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub ShowFromSO(ByRef mSOMKey As Double)

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

        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)

        SqlStr = "SELECT * FROM DSP_SALEORDER_DET" & vbCrLf _
                & " WHERE MKEY=" & mSOMKey & " ORDER BY SERIAL_NO"

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

                    .Col = ColItemUOM
                    .Text = Trim(IIf(IsDBNull(RsTemp.Fields("UOM_CODE").Value), "", RsTemp.Fields("UOM_CODE").Value))

                    .Col = ColHSN
                    .Text = Trim(IIf(IsDBNull(RsTemp.Fields("HSN_CODE").Value), "", RsTemp.Fields("HSN_CODE").Value))

                    .Col = ColQty
                    .Text = Trim(IIf(IsDBNull(RsTemp.Fields("SO_QTY").Value), "", RsTemp.Fields("SO_QTY").Value))

                    .Col = ColItemRate
                    .Text = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_PRICE").Value), "", RsTemp.Fields("ITEM_PRICE").Value))

                    '.Col = ColGross
                    '.Text = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_AMT").Value), "", RsTemp.Fields("ITEM_AMT").Value))

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
    'Private Sub SprdMain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles SprdMain.KeyPress
    '    If FormActive = False Then Exit Sub
    '    Dim KeyAscii As Short = Asc(e.KeyChar)

    '    If KeyAscii = 6 Then
    '        SprdMain.Row = 1
    '        SprdMain.Row2 = SprdMain.MaxRows
    '        SprdMain.Col = 1
    '        SprdMain.Col2 = SprdMain.MaxCols '' SprdMain.ActiveCol
    '        SprdMain.BlockMode = True
    '        SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
    '        SprdMain.BlockMode = False

    '        mSearchKey = ""
    '        cntSearchRow = 1
    '        cntSearchCol = 1
    '        mSearchKey = InputBox("Search :", "Search", mSearchKey)
    '        If MainClass.SearchIntoFullGrid(SprdMain, ColItemCode, mSearchKey, cntSearchRow, cntSearchCol) = True Then

    '            SprdMain.Row = cntSearchRow
    '            SprdMain.Row2 = cntSearchRow
    '            SprdMain.Col = 1
    '            SprdMain.Col2 = SprdMain.MaxCols
    '            SprdMain.BlockMode = True
    '            SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFF80)
    '            SprdMain.BlockMode = False

    '            MainClass.SetFocusToCell(SprdMain, cntSearchRow, ColItemRate)
    '            cntSearchRow = cntSearchRow + 1
    '            cntSearchCol = cntSearchCol + 1
    '        End If
    '    End If
    'End Sub
    Private Sub txtRMRate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRMRate.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRMRate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRMRate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub cmdGetData_Click(sender As Object, e As EventArgs) Handles cmdGetData.Click

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim xAcctCode As String
        Dim mIsApproved As String

        ''21-03-2006 'SK			
        '    Call DelTemp_Indent			

        If Trim(txtSupplierName.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtSupplierName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
            xAcctCode = MasterNo
            txtCode.Text = xAcctCode
            txtCode.Enabled = False
        Else
            MsgBox("Invalid Supplier Name.", MsgBoxStyle.Information)
            GoTo EventExitSub
        End If

        If Trim(txtBillTo.Text) = "" Then
            txtBillTo.Text = GetDefaultLocation(xAcctCode)
        End If
        mIsApproved = "N"

        If Val(txtRMRate.Text) <> 0 Then
            Call FillItemFromSuppCustDetail()

            Call FillSprdExp()
            Call SetCurrency()
            cmdGetData.Enabled = False
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:

    End Sub
End Class
