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

Imports System.Data
Imports System.IO
Imports System.Configuration
Imports System.Drawing.Color

Imports System.Drawing
Imports System.Drawing.Printing


Friend Class frmRM_DRW_MST
    Inherits System.Windows.Forms.Form
    Dim RsRMDrwMain As ADODB.Recordset ''ADODB.Recordset		
    Dim RsRMDrwDetail As ADODB.Recordset ''ADODB.Recordset		


    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean
    Dim mAccountCode As String
    Dim pRound As Double

    Private Const ConRowHeight As Short = 30

    Dim mSearchStartRow As Integer

    Dim pShowCalc As Boolean
    Dim pmyMenu As String


    Private Const ColItemCode As Short = 1
    Private Const ColItemName As Short = 2
    Private Const ColItemUOM As Short = 3
    Private Const ColHSN As Short = 4
    Private Const ColGross_Prev As Short = 5
    Private Const ColItemRate As Short = 6
    Private Const ColRemarks As Short = 7

    Dim mAmendStatus As Boolean
    Private Sub ChkActivate_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkActivate.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkStatus_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkStatus.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            pShowCalc = True
            SprdMain.Enabled = True
            txtPONo.Enabled = False
            cmdSearchPO.Enabled = False
            cmdSearchAmend.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            If RsRMDrwMain.EOF = False Then RsRMDrwMain.MoveFirst()
            Show1()
            txtPONo.Enabled = True
            cmdSearchPO.Enabled = True
            cmdSearchAmend.Enabled = True
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


        txtPONo.Enabled = False
        cmdSearchPO.Enabled = False
        cmdSearchAmend.Enabled = False

        MainClass.ButtonStatus(Me, XRIGHT, RsRMDrwMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
        If Not RsRMDrwMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.			
                PubDBCn.Cancel()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "PUR_RM_DWG_RATE_HDR", (txtPONo.Text), RsRMDrwMain, "PO NO") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "PUR_RM_DWG_RATE_HDR", "MKEY", (lblMkey.Text)) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM PUR_RM_DWG_RATE_DET WHERE MKEY=" & Val(lblMkey.Text) & "")
                PubDBCn.Execute("DELETE FROM PUR_RM_DWG_RATE_HDR WHERE MKEY=" & Val(lblMkey.Text) & "")

                PubDBCn.CommitTrans()
                RsRMDrwMain.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Cancel()
        RsRMDrwMain.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsRMDrwMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
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

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        SqlStr = ""
        mSubTitle = ""
        mPrintSubReport = False
        mTitle = "RM Drawing Rate"
        mSubTitle = Trim(lblDivision.Text)

        If Val(txtAmendNo.Text) > 0 Then
            mSubTitle = mSubTitle & "-AMENDMENT"
        End If

        mSubTitle = mSubTitle & IIf(mDraftPrint = True, "(Approval Pending)", "")

        Call MainClass.ClearCRptFormulas(Report1)

        mRptFileName = "RM_DRW_RATE_MST"


        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName, False, mPrintSubReport)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        'frmPrintPO.Close()
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'frmPrintPO.Close()
    End Sub
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
        Dim mShipContactNo As String


        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)



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
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
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

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()


        mStatus = IIf(chkStatus.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mActivate = IIf(ChkActivate.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")


        SqlStr = ""
        mPONo = Val(txtPONo.Text)
        If Val(txtPONo.Text) = 0 Then
            mPONo = AutoGenPONoSeq()
        End If
        txtPONo.Text = CStr(mPONo)

        mAmendNo = Val(txtAmendNo.Text)

        txtAmendNo.Text = CStr(Val(CStr(mAmendNo)))


        If ADDMode = True Then
            lblMkey.Text = mPONo & VB6.Format(mAmendNo, "000")
            SqlStr = " INSERT INTO PUR_RM_DWG_RATE_HDR ( " & vbCrLf _
                & "  MKEY, AUTO_KEY_PO,  COMPANY_CODE," & vbCrLf _
                & "  PUR_ORD_DATE, SUPP_CUST_CODE," & vbCrLf _
                & "  AMEND_NO, AMEND_DATE, EXCHANGERATE," & vbCrLf _
                & "  REMARKS, PO_STATUS, " & vbCrLf _
                & "  AMEND_WEF_DATE, PO_CLOSED," & vbCrLf _
                & "  ADDUSER, ADDDATE, MODUSER, MODDATE," & vbCrLf _
                & "  DIV_CODE ,BILL_TO_LOC_ID) "

            SqlStr = SqlStr & vbCrLf & " VALUES ( " & vbCrLf _
                & " " & Val(lblMkey.Text) & "," & mPONo & ", " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtPODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtCode.Text) & "', " & vbCrLf _
                & " " & Val(CStr(mAmendNo)) & ", " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtAmendDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " " & Val(TxtExchangeRate.Text) & ", "

            SqlStr = SqlStr & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                & " '" & mStatus & "',TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & mActivate & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')," & vbCrLf _
                & " '',''," & Val(txtDivision.Text) & ",'" & MainClass.AllowSingleQuote(txtBillTo.Text) & "')"
        End If


        If MODIFYMode = True Then
            SqlStr = " UPDATE PUR_RM_DWG_RATE_HDR SET " & vbCrLf _
                & " AUTO_KEY_PO=" & mPONo & ", " & vbCrLf _
                & " PUR_ORD_DATE=TO_DATE('" & VB6.Format(txtPODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "', " & vbCrLf _
                & " AMEND_NO=" & Val(CStr(mAmendNo)) & ", " & vbCrLf _
                & " AMEND_DATE=TO_DATE('" & VB6.Format(txtAmendDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " BILL_TO_LOC_ID='" & MainClass.AllowSingleQuote((txtBillTo.Text)) & "', "

            SqlStr = SqlStr & vbCrLf _
                & " EXCHANGERATE= " & Val(TxtExchangeRate.Text) & ", " & vbCrLf _
                & " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                & " PO_STATUS='" & mStatus & "', PO_CLOSED='" & mActivate & "', " & vbCrLf _
                & " DIV_CODE=" & Val(txtDivision.Text) & ","

            SqlStr = SqlStr & vbCrLf _
                & " AMEND_WEF_DATE=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND MKEY =" & Val(lblMkey.Text) & ""
        End If


        PubDBCn.Execute(SqlStr)
        If UpdateDetail1() = False Then GoTo ErrPart

        Update1 = True
        PubDBCn.CommitTrans()
        txtPONo.Text = CStr(mPONo)
        Exit Function
ErrPart:
        '    Resume			
        Update1 = False
        MsgInformation(Err.Description)
        PubDBCn.RollbackTrans()
        RsRMDrwMain.Requery()
        RsRMDrwDetail.Requery()

        MsgBox(Err.Description)
        ''Resume			
    End Function

    Private Function CheckItemInGrid(ByRef mItemCode As String, ByRef mPoSerialNo As Integer) As Boolean
        On Error GoTo CheckERR
        Dim I As Integer
        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I
                .Col = ColItemCode
                If UCase(Trim(mItemCode)) = UCase(Trim(.Text)) Then
                    mPoSerialNo = I
                    CheckItemInGrid = True
                    Exit Function
                End If
            Next
        End With
        Exit Function
CheckERR:
        CheckItemInGrid = False
    End Function

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
            & " FROM PUR_RM_DWG_RATE_HDR " & vbCrLf _
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

        SqlStr = "Delete From  PUR_RM_DWG_RATE_DET " & vbCrLf _
            & " Where " & vbCrLf _
            & " MKEY=" & Val(lblMkey.Text) & ""

        PubDBCn.Execute(SqlStr)

        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I
                xUpdate = False

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColItemUOM
                mItemUOM = MainClass.AllowSingleQuote(.Text)

                .Col = ColItemRate
                mRate = Val(.Text)

                .Col = ColRemarks
                mRemarks = Trim(.Text) '' MainClass.AllowSingleQuote(.Text)			

                .Col = ColHSN
                mHSNCode = .Text

                SqlStr = ""

                If mItemCode <> "" Then
                    SqlStr = " INSERT INTO PUR_RM_DWG_RATE_DET ( " & vbCrLf _
                        & " MKEY, SERIAL_NO, ITEM_CODE, " & vbCrLf _
                        & " ITEM_UOM, ITEM_PRICE, " & vbCrLf _
                        & " REMARKS, COMPANY_CODE, HSN_CODE) "

                    SqlStr = SqlStr & vbCrLf _
                        & " VALUES ( " & vbCrLf _
                        & " " & Val(lblMkey.Text) & "," & I & ", " & vbCrLf _
                        & " '" & mItemCode & "','" & mItemUOM & "', " & vbCrLf _
                        & " " & mRate & ", " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mRemarks) & "'," & vbCrLf _
                        & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mHSNCode) & "') "

                    PubDBCn.Execute(SqlStr)
                End If
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

        SqlStr = " SELECT CMST.SUPP_CUST_NAME, CMST.SUPP_CUST_CODE, CMST.LOCATION_ID, CMST.SUPP_CUST_ADDR, CMST.SUPP_CUST_CITY, CMST.SUPP_CUST_STATE" & vbCrLf _
                & " FROM FIN_SUPP_CUST_MST CMST1, FIN_SUPP_CUST_BUSINESS_MST CMST" & vbCrLf _
                & " WHERE CMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CMST1.SUPP_CUST_TYPE IN ('S','C')" & vbCrLf _
                & " AND CMST1.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
                & " AND CMST1.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf

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
            & " AND AUTO_KEY_PO=" & Val(txtPONo.Text) & ""

        If MainClass.SearchGridMaster("", "PUR_RM_DWG_RATE_HDR", "trim(TO_CHAR(AMEND_NO,'000'))", "AMEND_DATE", , , SqlStr) = True Then
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
                & " FROM PUR_RM_DWG_RATE_HDR IH, FIN_SUPP_CUST_BUSINESS_MST CMST" & vbCrLf _
                & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
                & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
                & " AND IH.BILL_TO_LOC_ID=CMST.LOCATION_ID"

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
            UltraGrid1.Refresh()
            UltraGrid1.Focus()
            UltraGrid1.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            UltraGrid1.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsRMDrwMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Public Sub frmRM_DRW_MST_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor


        SqlStr = "Select * From PUR_RM_DWG_RATE_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRMDrwMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From PUR_RM_DWG_RATE_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRMDrwDetail, ADODB.LockTypeEnum.adLockReadOnly)

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


        SqlStr = " SELECT " & vbCrLf _
            & " A.MKEY AS MKEY, A.AUTO_KEY_PO AS PO_NO, TO_CHAR(A.PUR_ORD_DATE,'DD/MM/YYYY') AS PO_DATE, " & vbCrLf _
            & " A.AMEND_NO, TO_CHAR(A.AMEND_DATE,'DD/MM/YYYY') AS AMEND_DATE,  " & vbCrLf _
            & " TO_CHAR(A.AMEND_WEF_DATE,'DD/MM/YYYY') AS WEF, B.SUPP_CUST_NAME AS NAME, BILL_TO_LOC_ID," & vbCrLf _
            & " A.PO_STATUS " & vbCrLf _
            & " FROM PUR_RM_DWG_RATE_HDR A, FIN_SUPP_CUST_MST B " & vbCrLf _
            & " WHERE A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf _
            & " AND A.SUPP_CUST_CODE=B.SUPP_CUST_CODE " & vbCrLf _
            & " AND A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        SqlStr = SqlStr & vbCrLf _
            & " AND A.MKEY = (SELECT MAX(MKEY) FROM PUR_RM_DWG_RATE_HDR WHERE AUTO_KEY_PO=A.AUTO_KEY_PO)"

        SqlStr = SqlStr & " ORDER BY SUBSTR(AUTO_KEY_PO,LENGTH(AUTO_KEY_PO)-5,4),A.AUTO_KEY_PO,A.AMEND_NO"

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
            UltraGrid1.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Ref No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(2).Header.Caption = "Ref Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(3).Header.Caption = "Amend No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(4).Header.Caption = "Amend Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(5).Header.Caption = "Amend WEF Date"

            UltraGrid1.DisplayLayout.Bands(0).Columns(6).Header.Caption = "Supplier Name"
            UltraGrid1.DisplayLayout.Bands(0).Columns(7).Header.Caption = "Supplier Location"
            UltraGrid1.DisplayLayout.Bands(0).Columns(8).Header.Caption = "Status"


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
            UltraGrid1.DisplayLayout.Bands(0).Columns(6).Width = 250
            UltraGrid1.DisplayLayout.Bands(0).Columns(7).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(8).Width = 80



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

    Private Sub frmRM_DRW_MST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmRM_DRW_MST_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection			
        'PvtDBCn.Open StrConn			
        Call SetMainFormCordinate(Me)
        pmyMenu = myMenu
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, pmyMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

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

        chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkStatus.Enabled = True
        ChkActivate.Enabled = False

        txtSupplierName.Text = ""

        txtDivision.Text = ""
        lblDivision.Text = ""
        txtDivision.Enabled = True


        txtCode.Text = ""
        txtCode.Enabled = True
        txtSupplierName.Enabled = True


        txtBillTo.Text = ""


        cmdsearch.Enabled = True
        SprdMain.Enabled = True

        TxtExchangeRate.Text = "1.000"

        txtAmendNo.Enabled = False
        txtAmendDate.Enabled = False
        txtRemarks.Text = ""


        mAmendStatus = False
        cmdAmend.Enabled = IIf(InStr(1, XRIGHT, "M") > 0, True, False) '' True			

        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)

        pShowCalc = False

        Call AutoCompleteSearch("FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", " STATUS='O' AND SUPP_CUST_TYPE IN ('S','C')", txtSupplierName)

        MainClass.ButtonStatus(Me, XRIGHT, RsRMDrwMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume			
    End Sub

    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim cntCol As Integer

        With SprdMain
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow


            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRMDrwDetail.Fields("ITEM_CODE").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 8)

            .TypeEditMultiLine = False

            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = MainClass.SetMaxLength("Item_Short_Desc", "INV_ITEM_MST", PubDBCn)
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 40)
            .ColsFrozen = ColItemName

            .Col = ColItemUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ALPHANUMERIC
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = RsRMDrwDetail.Fields("ITEM_UOM").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(.Col, 5)


            .Col = ColItemRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeFloatDecimalPlaces = 4
            .TypeEditLen = RsRMDrwDetail.Fields("ITEM_PRICE").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 10)


            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRMDrwDetail.Fields("REMARKS").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            '        If Right(lblBookType.text, 1) = "O" Then			
            '            .ColWidth(.Col) = 18			
            '        Else			
            .set_ColWidth(.Col, 15)
            '        End If			




            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemName, ColGross_Prev)
            MainClass.SetSpreadColor(SprdMain, Arow)

            Call SetCurrency()
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume			
    End Sub

    Private Sub SetTextLengths()

        On Error GoTo ERR1

        txtPONo.MaxLength = RsRMDrwMain.Fields("AUTO_KEY_PO").Precision
        txtPODate.MaxLength = RsRMDrwMain.Fields("PUR_ORD_DATE").DefinedSize - 6
        txtRemarks.MaxLength = RsRMDrwMain.Fields("REMARKS").DefinedSize

        txtAmendNo.MaxLength = RsRMDrwMain.Fields("AMEND_NO").Precision
        txtAmendDate.MaxLength = RsRMDrwMain.Fields("AMEND_DATE").DefinedSize - 6

        TxtExchangeRate.MaxLength = RsRMDrwMain.Fields("ExchangeRate").Precision

        txtSupplierName.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
        txtCode.MaxLength = RsRMDrwMain.Fields("SUPP_CUST_CODE").DefinedSize

        txtDivision.MaxLength = RsRMDrwMain.Fields("DIV_CODE").DefinedSize

        txtBillTo.MaxLength = RsRMDrwMain.Fields("BILL_TO_LOC_ID").DefinedSize

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
        Dim mPOWEFCheck As String
        Dim mPOWEF As String
        Dim mCheckPOWEF As Boolean
        Dim mSaveRights As String

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

        If MODIFYMode = True And chkStatus.CheckState = System.Windows.Forms.CheckState.Checked And chkStatus.Enabled = False Then
            MsgInformation("Posted PO Cann't be Modified")
            FieldsVarification = False
            Exit Function
        End If


        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsRMDrwMain.EOF = True Then Exit Function

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

        If Trim(txtWEF.Text) = "" Then
            MsgInformation(" WEF Date is empty. Cannot Save")
            txtWEF.Focus()
            FieldsVarification = False
            Exit Function
        Else
            If IsDate(txtWEF.Text) = False Then
                MsgInformation(" Invalid WEF Date. Cannot Save")
                txtWEF.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If CDate(txtPODate.Text) > CDate(txtAmendDate.Text) Then
            MsgInformation(" Amend Date Cann't be less than PO Date. Cannot Save")
            FieldsVarification = False
            Exit Function
        End If


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
        If Val(txtPONo.Text) <> 0 Then
            pPONO = CDbl(Mid(txtPONo.Text, 1, Len(txtPONo.Text) - 6))
        End If


        If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "Please Check Item Code.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColItemName, "S", "Please Check Item Description.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColItemUOM, "S", "Please Check Unit.") = False Then FieldsVarification = False : Exit Function


        If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS='I'") = True Then
            MsgInformation("Item Status is Closed, So cann't be Saved. [" & mItemCode & "]")
            MainClass.SetFocusToCell(SprdMain, I, ColItemCode)
            FieldsVarification = False
            Exit Function
        End If


        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        'Resume			
    End Function

    Private Sub frmRM_DRW_MST_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        Me.Hide()
        Me.Dispose()
        Me.Close()
        RsRMDrwMain.Close()
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


                If UCase(pCheckItemCode) = UCase(Trim(mItemCode)) Then
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

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim SqlStr As String = ""
        Dim xHSNCode As String = ""
        Dim pItemCode As String
        Dim RsTemp As ADODB.Recordset

        If eventArgs.row = 0 And eventArgs.col = ColItemCode And SprdMain.Enabled = True Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode

                SqlStr = GetSearchItem("Y")
                ''If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "", "ITEM_CODE") = True Then			
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColItemCode
                    .Text = Trim(AcName)
                    .Col = ColItemName
                    .Text = Trim(AcName1)
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)

            End With
        End If


        If eventArgs.row = 0 And eventArgs.col = ColItemName And SprdMain.Enabled = True Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemName
                SqlStr = GetSearchItem("N")
                ''If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "", "ITEM_CODE") = True Then			
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
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
                If MainClass.SearchGridMaster(.Text, "GEN_HSN_MST", "HSN_CODE", "HSN_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='G' ") = True Then     ''AND CODETYPE='" & iif(VB.Left(lblBookType.Text, 1) = "W" Or VB.Left(lblBookType.Text, 1) = "J" ,'S','G') & "'  'VB.Left(lblBookType.Text, 1) = "W" Or VB.Left(lblBookType.Text, 1) = "J" 
                    .Row = .ActiveRow
                    .Col = ColHSN
                    .Text = AcName
                    xHSNCode = Trim(.Text)

                    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColHSN)
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

        If eventArgs.keyCode = System.Windows.Forms.Keys.F2 And mCol = ColItemName Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F2 And mCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemName, 0))


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
            Case ColItemCode
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColItemCode
                xICode = SprdMain.Text
                If xICode = "" Then GoTo CalcPart

                mCheckItemCode = Trim(xICode)


                If GetValidItem(xICode) = True Then
                    If CheckDuplicateItem(mCheckItemCode) = False Then
                        If FillGridRow(xICode, ColItemCode) = False Then Exit Sub

                    End If
                Else
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColItemCode)
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

            Case ColItemRate
                If CheckItemRate() = True Then
                    MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                    FormatSprdMain(-1)
                End If

        End Select
CalcPart:

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Function CheckItemRate() As Boolean

        On Error GoTo ERR1
        Dim mItemCode As String
        Dim mItemRate As Double

        With SprdMain
            .Row = .ActiveRow

            .Col = ColItemCode

            mItemCode = Trim(.Text)

            If mItemCode = "" Then Exit Function

            .Col = ColItemRate
            mItemRate = Val(.Text)
            If mItemRate > 0 Then
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

        If mItemCode = "" Then Exit Function
        If Trim(txtCode.Text) = "" Then Exit Function

        '    WITHIN_COUNTRY			



        SqlStr = ""

        SqlStr = " Select INVMST.ITEM_CODE,INVMST.ITEM_SHORT_DESC,INVMST.PURCHASE_UOM, INVMST.IDENT_MARK, INVMST.CATEGORY_CODE, CMST.SALEINVTYPECODE, CMST.PURCHASEINVTYPECODE, INVMST.HSN_CODE " & vbCrLf _
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

                SprdMain.Col = ColItemUOM
                SprdMain.Text = IIf(IsDBNull(.Fields("PURCHASE_UOM").Value), "", .Fields("PURCHASE_UOM").Value)

            End With
            FillGridRow = True
        Else
            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, pCol)
            FillGridRow = False
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

        If MODIFYMode = True And RsRMDrwMain.BOF = False Then xMkey = RsRMDrwMain.Fields("mKey").Value

        SqlStr = "SELECT * FROM PUR_RM_DWG_RATE_HDR " & " WHERE AUTO_KEY_PO='" & MainClass.AllowSingleQuote(UCase(CStr(mPONo))) & "'" & vbCrLf _
            & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        SqlStr = SqlStr & vbCrLf & " AND AMEND_NO=" & Val(txtAmendNo.Text) & ""


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRMDrwMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsRMDrwMain.EOF = False Then
            Clear1()
            Show1()
        Else
            SqlStr = "SELECT * FROM PUR_RM_DWG_RATE_HDR " &
                " WHERE AUTO_KEY_PO='" & MainClass.AllowSingleQuote(UCase(CStr(mPONo))) & "'" & vbCrLf _
                & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""


            SqlStr = SqlStr & vbCrLf &
                " AND AMEND_NO IN (" & vbCrLf _
                & " SELECT MAX(AMEND_NO) FROM PUR_RM_DWG_RATE_HDR " & " WHERE AUTO_KEY_PO='" & MainClass.AllowSingleQuote(UCase(CStr(mPONo))) & "'" & vbCrLf _
                & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRMDrwMain, ADODB.LockTypeEnum.adLockReadOnly)
            If RsRMDrwMain.EOF = False Then
                Clear1()
                Show1()
            Else

                If ADDMode = False And MODIFYMode = False Then
                    MsgBox("No Such PO No. Click, Add for New", MsgBoxStyle.Information)
                    txtAmendNo.Text = CStr(0)
                    '                Cancel = True			
                ElseIf MODIFYMode = True Then
                    SqlStr = "SELECT * FROM PUR_RM_DWG_RATE_HDR WHERE MKEY=" & Val(xMkey) & ""
                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRMDrwMain, ADODB.LockTypeEnum.adLockReadOnly)
                End If
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
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

    Private Sub txtSearchItem_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSearchItem.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSearchItem.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
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

        Call SetCurrency()
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
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
        If Not RsRMDrwMain.EOF Then

            lblMkey.Text = IIf(IsDBNull(RsRMDrwMain.Fields("MKEY").Value), "", RsRMDrwMain.Fields("MKEY").Value)
            txtPONo.Text = IIf(IsDBNull(RsRMDrwMain.Fields("AUTO_KEY_PO").Value), "", RsRMDrwMain.Fields("AUTO_KEY_PO").Value)
            txtPODate.Text = VB6.Format(IIf(IsDBNull(RsRMDrwMain.Fields("PUR_ORD_DATE").Value), "", RsRMDrwMain.Fields("PUR_ORD_DATE").Value), "DD/MM/YYYY")
            TxtExchangeRate.Text = VB6.Format(IIf(IsDBNull(RsRMDrwMain.Fields("ExchangeRate").Value), "1", RsRMDrwMain.Fields("ExchangeRate").Value), "0.000")

            txtWEF.Text = VB6.Format(IIf(IsDBNull(RsRMDrwMain.Fields("AMEND_WEF_DATE").Value), "", RsRMDrwMain.Fields("AMEND_WEF_DATE").Value), "DD/MM/YYYY")

            txtDivision.Text = IIf(IsDBNull(RsRMDrwMain.Fields("DIV_CODE").Value), "", RsRMDrwMain.Fields("DIV_CODE").Value)

            If MainClass.ValidateWithMasterTable((txtDivision.Text), "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblDivision.Text = MasterNo
            End If

            ChkActivate.CheckState = System.Windows.Forms.CheckState.Unchecked
            txtAmendNo.Text = IIf(IsDBNull(RsRMDrwMain.Fields("AMEND_NO").Value), 0, RsRMDrwMain.Fields("AMEND_NO").Value)
            txtAmendDate.Text = VB6.Format(IIf(IsDBNull(RsRMDrwMain.Fields("AMEND_DATE").Value), "", RsRMDrwMain.Fields("AMEND_DATE").Value), "DD/MM/YYYY")
            chkStatus.CheckState = IIf(RsRMDrwMain.Fields("PO_STATUS").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            chkStatus.Enabled = IIf(RsRMDrwMain.Fields("PO_STATUS").Value = "Y", False, True)

            ChkActivate.CheckState = IIf(RsRMDrwMain.Fields("PO_CLOSED").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            cmdAmend.Enabled = IIf(RsRMDrwMain.Fields("PO_CLOSED").Value = "Y", False, True)

            mAccountCode = IIf(IsDBNull(RsRMDrwMain.Fields("SUPP_CUST_CODE").Value), -1, RsRMDrwMain.Fields("SUPP_CUST_CODE").Value)
            If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountName = MasterNo
            End If

            txtSupplierName.Text = mAccountName
            txtCode.Text = Trim(IIf(IsDBNull(RsRMDrwMain.Fields("SUPP_CUST_CODE").Value), "", RsRMDrwMain.Fields("SUPP_CUST_CODE").Value))
            txtCode.Enabled = False
            txtSupplierName.Enabled = True
            cmdsearch.Enabled = True

            txtBillTo.Text = IIf(IsDBNull(RsRMDrwMain.Fields("BILL_TO_LOC_ID").Value), "", RsRMDrwMain.Fields("BILL_TO_LOC_ID").Value)


            txtRemarks.Text = IIf(IsDBNull(RsRMDrwMain.Fields("REMARKS").Value), "", RsRMDrwMain.Fields("REMARKS").Value)

            Call ShowDetail1()

            Call SetCurrency()
        End If
        FormatSprdMain(-1)

        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = True

        txtPONo.Enabled = True
        cmdSearchPO.Enabled = True
        cmdSearchAmend.Enabled = True
        pShowCalc = True
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemName, ColGross_Prev)

        MainClass.ButtonStatus(Me, XRIGHT, RsRMDrwMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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

        MainClass.ClearGrid(SprdMain, ConRowHeight)

        Call AutoCompleteSearch("PUR_RM_DWG_RATE_DET", "ITEM_CODE", " MKEY=" & Val(lblMkey.Text) & "", txtSearchItem)

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf _
            & " FROM PUR_RM_DWG_RATE_DET " & vbCrLf _
            & " Where " & vbCrLf _
            & " MKEY=" & Val(lblMkey.Text) & "" & vbCrLf _
            & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRMDrwDetail, ADODB.LockTypeEnum.adLockReadOnly)

        With RsRMDrwDetail
            If .EOF = True Then Exit Sub
            '        FormatSprdMain -1			
            I = 1
            '        .MoveFirst			

            Do While Not .EOF

                SprdMain.Row = I


                SprdMain.Col = ColItemCode
                If mWODesc = "" Then
                    mItemCode = Trim(IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))
                Else
                    mItemCode = ""
                End If
                '            If mItemCode = "C00010" Then MsgBox "OK"			
                SprdMain.Text = mItemCode

                SprdMain.Col = ColItemName
                If mWODesc = "" Then
                    MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                    mItemDesc = MasterNo
                Else
                    mItemDesc = ""
                End If

                SprdMain.Text = mItemDesc

                SprdMain.Col = ColHSN
                mHSNCode = GetHSNCode(mItemCode)
                SprdMain.Text = mHSNCode


                SprdMain.Col = ColItemUOM
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value))

                SprdMain.Col = ColItemRate
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_PRICE").Value), 0, .Fields("ITEM_PRICE").Value)))
                mPrice = Val(IIf(IsDBNull(.Fields("ITEM_PRICE").Value), 0, .Fields("ITEM_PRICE").Value))

                SprdMain.Col = ColGross_Prev
                If Val(txtAmendNo.Text) = 0 Then
                    SprdMain.Text = "0"
                Else
                    SprdMain.Text = VB6.Format(GetPreviousItemGross(mItemCode, mWODesc), "0.0000")
                End If
                mPrevValue = Val(SprdMain.Text)

                SprdMain.Col = ColRemarks
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value))

                .MoveNext()

                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With

        Call FormatSprdMain(-1)

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''   Resume			
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

        If MODIFYMode = True And RsRMDrwMain.BOF = False Then xMkey = RsRMDrwMain.Fields("mKey").Value

        SqlStr = "SELECT * FROM PUR_RM_DWG_RATE_HDR " & " WHERE AUTO_KEY_PO='" & MainClass.AllowSingleQuote(UCase(CStr(mPONo))) & "'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        SqlStr = SqlStr & vbCrLf & " AND AMEND_NO = (" & vbCrLf & " SELECT MAX(AMEND_NO) AS AMEND_NO FROM PUR_RM_DWG_RATE_HDR" & vbCrLf & " WHERE AUTO_KEY_PO='" & MainClass.AllowSingleQuote(UCase(CStr(mPONo))) & "' " & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ")"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRMDrwMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsRMDrwMain.EOF = False Then
            Clear1()
            Show1()
        Else
            SqlStr = "SELECT * FROM PUR_RM_DWG_RATE_HDR " & " WHERE AUTO_KEY_PO='" & MainClass.AllowSingleQuote(UCase(CStr(mPONo))) & "'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""


            SqlStr = SqlStr & vbCrLf & " AND AMEND_NO IN (" & vbCrLf & " SELECT MAX(AMEND_NO) FROM PUR_RM_DWG_RATE_HDR " & vbCrLf & " WHERE AUTO_KEY_PO='" & MainClass.AllowSingleQuote(UCase(CStr(mPONo))) & "'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRMDrwMain, ADODB.LockTypeEnum.adLockReadOnly)
            If RsRMDrwMain.EOF = False Then
                Clear1()
                Show1()
            Else

                If ADDMode = False And MODIFYMode = False Then
                    MsgBox("No Such PO No. Click, Add for New", MsgBoxStyle.Information)
                    txtAmendNo.Text = CStr(0)
                    Cancel = True
                ElseIf MODIFYMode = True Then
                    SqlStr = "SELECT * FROM PUR_RM_DWG_RATE_HDR WHERE MKEY=" & Val(xMkey) & ""

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRMDrwMain, ADODB.LockTypeEnum.adLockReadOnly)
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


    Private Sub TxtWef_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWEF.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Function GetSearchItem(ByRef mByCode As String) As String
        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim xSuppCode As String

        xSuppCode = IIf(Trim(txtCode.Text) = "", "-1", Trim(txtCode.Text))

        If mByCode = "Y" Then
            mSqlStr = "SELECT A.ITEM_CODE,A.ITEM_SHORT_DESC "
        Else
            mSqlStr = "SELECT A.ITEM_SHORT_DESC,A.ITEM_CODE "
        End If

        'If VB.Right(lblBookType.Text, 1) = "O" Then
        '    mSqlStr = mSqlStr & vbCrLf & " FROM INV_ITEM_MST A, FIN_SUPP_CUST_DET B" & vbCrLf & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf & " AND A.ITEM_CODE=B.ITEM_CODE " & vbCrLf & " AND B.SUPP_CUST_CODE='" & xSuppCode & "' AND ITEM_APPROVED='Y'"
        'Else
        mSqlStr = mSqlStr & vbCrLf & " FROM INV_ITEM_MST A" & vbCrLf & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        'End If

        If mByCode = "Y" Then
            mSqlStr = mSqlStr & vbCrLf & "ORDER BY A.ITEM_CODE "
        Else
            mSqlStr = mSqlStr & vbCrLf & "ORDER BY A.ITEM_SHORT_DESC"
        End If

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

        mSqlStr = " SELECT " & vbCrLf & " IH.*, ID.*,TEMP_PO.*,"

        mSqlStr = mSqlStr & " CMST.SUPP_CUST_NAME, " & vbCrLf _
             & " BCMST.*"

        ''FROM CLAUSE...			
        mSqlStr = mSqlStr & vbCrLf & " FROM PUR_RM_DWG_RATE_HDR IH, PUR_RM_DWG_RATE_DET ID, " & vbCrLf _
            & " FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_BUSINESS_MST BCMST, FIN_PAYTERM_MST PAYMST, Temp_PO_PRN TEMP_PO"

        ''WHERE CLAUSE...			
        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.MKEY=ID.MKEY" & vbCrLf _
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

        ''ORDER CLAUSE...			

        If pItemCodeWisePrint = True Then
            mSqlStr = mSqlStr & vbCrLf & "ORDER BY ID.SERIAL_NO"
        Else
            mSqlStr = mSqlStr & vbCrLf & "ORDER BY TEMP_PO.ITEM_SHORT_DESC"
        End If

        SelectQryForPO = mSqlStr
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
    Private Function GetMaxAmendNo(ByRef pPONO As Double) As Integer

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = " SELECT MAX(AMEND_NO) AS AMEND_NO" & vbCrLf _
            & " FROM PUR_RM_DWG_RATE_HDR" & vbCrLf & " WHERE AUTO_KEY_PO=" & Val(CStr(pPONO)) & "" '& vbCrLf |        & " AND PO_STATUS='Y' " & vbCrLf |        & " AND PO_CLOSED='N' "			

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

        SqlStr = " SELECT Count(1) AS CNTPO" & vbCrLf & " FROM PUR_RM_DWG_RATE_HDR" & vbCrLf & " WHERE AUTO_KEY_PO=" & Val(CStr(pPONO)) & "" & vbCrLf & " AND PO_STATUS='N' " '& vbCrLf |        & " AND PO_CLOSED='N' "			

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

    Private Function GetPreviousItemGross(ByRef pItemCode As String, ByRef pWODesc As String) As Double

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing

        GetPreviousItemGross = 0

        mSqlStr = "SELECT NVL(ID.ITEM_PRICE,0) AS GROSS_AMT " & vbCrLf _
            & " FROM PUR_RM_DWG_RATE_HDR IH, PUR_RM_DWG_RATE_DET ID" & vbCrLf _
            & " WHERE IH.MKEy=ID.MKEY" & vbCrLf _
            & " And IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "


        ''- ROUND((NVL(ID.ITEM_PRICE,0) * ID.ITEM_DIS_PER)/100,4)) AS GROSS_AMT " & vbCrLf & " FROM PUR_RM_DWG_RATE_HDR IH, PUR_RM_DWG_RATE_DET ID" & vbCrLf & " WHERE IH.MKEy=ID.MKEY" & vbCrLf & " And IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        mSqlStr = mSqlStr & vbCrLf & " And IH.AUTO_KEY_PO=" & Val(txtPONo.Text) & "" & vbCrLf & ""


        If Trim(pItemCode) <> "" Then
            mSqlStr = mSqlStr & vbCrLf & " And ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"
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
    Private Sub cmdBillToSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
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

    Private Sub CmdPopFromFile_Click(sender As Object, e As EventArgs) Handles CmdPopFromFile.Click

        Try
            Dim strFilePath As String = ""
            Dim intflag As Integer
            CommonDialogOpen.FileName = ""

            intflag = CommonDialogOpen.ShowDialog()

            If intflag = 1 Then
                If CommonDialogOpen.FileName <> "" Then
                    strFilePath = CommonDialogOpen.FileName
                    'strfilename = CommonDialogOpen.SafeFileName
                    Call PopulateFromXLSFile(strFilePath)
                End If
            End If

        Catch ex As Exception

        End Try
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
NormalExit:

    End Sub
    Private Sub PopulateFromXLSFile(ByVal strXLSFile As String)

        On Error GoTo ErrPart
        Dim I As Integer
        Dim SqlStr As String = ""

        Dim xSqlStr As String
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsFile As ADODB.Recordset
        Dim FileConnStr As String


        Dim mItemCode As String
        Dim mItemPartNo As String
        Dim mItemDesc As String
        Dim mRate As Double
        Dim mUOM As String
        Dim mHSNCode As String

        Dim strTemp As String
        Dim strWkShName As String
        Dim strError As String = ""
        Dim CntRow As Long = 1


        MainClass.ClearGrid(SprdMain)
        FormatSprdMain(-1)

        Dim ErrorFile As System.IO.StreamWriter


        Dim FileName As String = Path.GetFileName(strXLSFile)
        Dim Extension As String = Path.GetExtension(strXLSFile)


        Dim conStr As String = ""
        Select Case UCase(Extension)
            Case ".XLS"
                'Excel 97-03 
                conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & strXLSFile & ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'"
                Exit Select
            Case ".XLSX"
                'Excel 07 
                conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & strXLSFile & ";Extended Properties='Excel 12.0 Xml;HDR=Yes'"
                Exit Select
        End Select

        conStr = String.Format(conStr, strXLSFile)    ''isHDR='Yes'

        Dim connExcel As New OleDbConnection(conStr)
        Dim cmdExcel As New OleDbCommand()
        Dim oda As New OleDbDataAdapter()
        Dim dt As New DataTable()

        cmdExcel.Connection = connExcel

        'Get the name of First Sheet 
        connExcel.Open()
        Dim dtExcelSchema As DataTable
        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
        Dim SheetName As String = dtExcelSchema.Rows(0)("TABLE_NAME").ToString()

        connExcel.Close()

        'Read Data from First Sheet 
        connExcel.Open()

        cmdExcel.CommandText = "SELECT * From [" & SheetName & "]"      '' ORDER BY 4 DESC
        oda.SelectCommand = cmdExcel
        oda.Fill(dt)
        connExcel.Close()

        For Each dtRow In dt.Rows




            mItemCode = UCase(Trim(IIf(IsDBNull(dtRow.Item(0)), "", dtRow.Item(0))))
            'If Trim(txtCustomerCode.Text) <> mCustomerCode Then GoTo NextRecord

            mRate = Val(IIf(IsDBNull(dtRow.Item(2)), 0, dtRow.Item(2)))

            'mItemDesc = UCase(Trim(IIf(IsDBNull(dtRow.Item(5)), "", dtRow.Item(5))))

            OpenLocalConnection()

            xSqlStr = " SELECT ITEM_SHORT_DESC, ITEM_CODE, ISSUE_UOM ,HSN_CODE" & vbCrLf _
                   & " FROM INV_ITEM_MST " & vbCrLf _
                   & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                   & " AND ITEM_CODE ='" & MainClass.AllowSingleQuote(mItemCode) & "'"

            MainClass.UOpenRecordSet(xSqlStr, LocalPubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                mItemCode = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))
                mItemDesc = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value))
                mUOM = Trim(IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value))
                mHSNCode = Trim(IIf(IsDBNull(RsTemp.Fields("HSN_CODE").Value), "", RsTemp.Fields("HSN_CODE").Value))
            Else
                GoTo NextRecord
            End If

            SprdMain.Row = SprdMain.MaxRows

            SprdMain.Col = ColItemCode
            SprdMain.Text = mItemCode

            SprdMain.Col = ColItemName
            SprdMain.Text = mItemDesc

            SprdMain.Col = ColItemUOM
            SprdMain.Text = mUOM

            SprdMain.Col = ColHSN
            SprdMain.Text = mHSNCode

            SprdMain.Col = ColItemRate
            SprdMain.Text = mRate


            SprdMain.MaxRows = SprdMain.MaxRows + 1
            CntRow = CntRow + 1

            RsTemp.Close()
            RsTemp = Nothing

            CloseLocalConnection()
NextRecord:

        Next

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '        Resume
    End Sub
End Class
