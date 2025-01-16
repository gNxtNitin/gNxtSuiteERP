'Option Strict Off
'Option Explicit On
'Imports VB = Microsoft.VisualBasic
''Imports Microsoft.VisualBasic.PowerPacks.Printing.Compatibility.VB6
'Imports Microsoft.VisualBasic.Compatibility
'Imports Infragistics.Shared
'Imports Infragistics.Win
'Imports Infragistics.Win.UltraWinGrid
'Imports Infragistics.Win.UltraWinDataSource
''Imports Infragistics.Win.UltraWinTabControl
'Imports System.Data.OleDb
'Imports System.Drawing
'Imports System.Drawing.Printing
Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
''Imports Newtonsoft.Json
Imports System.Xml
'Imports System.Web.Script.Serialization
Imports System.Xml.Linq

Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Imports QRCoder
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Drawing.Color
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinDataSource
'Imports Infragistics.Win.UltraWinTabControl
Imports System.Data.OleDb
Imports System.Drawing
Imports System.Drawing.Printing

Friend Class FrmPurchaseGST
    Inherits System.Windows.Forms.Form
    'Dim ResizeForm As New Resizer
    Private Enum TerrorCorretion
        QualityLow
        QualityMedium
        QualityStandard
        QualityHigh
    End Enum
    Dim AccessCnn As New ADODB.Connection
    Dim RsPurchMain As ADODB.Recordset ''Recordset
    Dim RsPurchDetail As ADODB.Recordset ''Recordset
    Dim RsPurchExp As ADODB.Recordset ''Recordset
    Dim RsPurchPrn As ADODB.Recordset ''Recordset
    'Private PvtDBCn As ADODB.Connection
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean
    Dim mCurRowNo As Integer
    'Dim SqlStr As String=""
    Dim mSupplierCode As String
    Dim pRound As Double
    Dim pShowCalc As Boolean
    Private Const mBookType As String = "P"
    ''Private Const mBookSubType = "C"
    Dim mBookSubType As String
    Private Const ConRowHeight As Short = 12
    Dim mAddUser As String
    Dim mAddDate As String
    Dim mModUser As String
    Dim mModDate As String
    'Private JB As JsonBag
    Dim pProcessKey As Double
    Private Const ColItemCode As Short = 1
    Private Const ColPartNo As Short = 2
    Private Const ColItemDesc As Short = 3
    Private Const ColHSN As Short = 4
    Private Const ColAcceptedQty As Short = 5
    Private Const ColShortageQty As Short = 6
    Private Const ColRejectedQty As Short = 7
    Private Const ColPORate As Short = 8
    Private Const ColVolDiscRate As Short = 9
    Private Const ColUnit As Short = 10
    Private Const ColQty As Short = 11
    Private Const ColRate As Short = 12
    Private Const ColAmount As Short = 13
    Private Const ColTaxableAmount As Short = 14
    Private Const ColCGSTPer As Short = 15
    Private Const ColCGSTAmount As Short = 16
    Private Const ColSGSTPer As Short = 17
    Private Const ColSGSTAmount As Short = 18
    Private Const ColIGSTPer As Short = 19
    Private Const ColIGSTAmount As Short = 20
    Private Const ColInvType As Short = 21
    Private Const ColPONo As Short = 22
    Private Const ColShowPO As Short = 23

    Private Const ColPayBillNo As Short = 1
    Private Const ColPayBillDate As Short = 2
    Private Const ColPayBillAmount As Short = 3
    Private Const ColPayBalAmount As Short = 4
    Private Const ColPayBalDC As Short = 5
    Private Const ColPayPaymentAmt As Short = 6



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
    Private Const ColExpDebitAmt As Short = 11
    Dim pDnCnNo As String
    Dim mDNCnNO As Integer
    Dim pTempDNCNSeq As Double
    Dim mIsAuthorisedUser As Boolean
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboGSTStatus_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboGSTStatus.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboGSTStatus_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboGSTStatus.SelectedIndexChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboGSTStatus_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles cboGSTStatus.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mGSTType As String
        Dim mItemCode As String
        Dim mHSNCode As String
        Dim pCGSTPer As Double
        Dim pSGSTPer As Double
        Dim pIGSTPer As Double
        Dim mLocal As String
        Dim mPartyGSTNo As String
        Dim mAccountCode As String

        If MODIFYMode = True Then

            If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = Trim(MasterNo)
            End If

            '        If MsgQuestion("Want to Refresh GST % ? ") = vbYes Then
            MsgInformation("GST % will also reset.")
            mLocal = "N"
            If Trim(txtSupplier.Text) <> "" Then
                mLocal = GetPartyBusinessDetail(Trim(mAccountCode), Trim(txtBillTo.Text), "WITHIN_STATE")
            End If
            mPartyGSTNo = ""
            'If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            '    mPartyGSTNo = MasterNo
            'End If
            mPartyGSTNo = GetPartyBusinessDetail(mAccountCode, Trim(txtBillTo.Text), "GST_RGN_NO")
            mGSTType = VB.Left(cboGSTStatus.Text, 1)
            If mGSTType = "E" Or mGSTType = "N" Or mGSTType = "C" Then
                For cntRow = 1 To SprdMain.MaxRows - 1
                    SprdMain.Row = cntRow
                    SprdMain.Col = ColCGSTPer
                    SprdMain.Text = "0.00"
                    SprdMain.Col = ColSGSTPer
                    SprdMain.Text = "0.00"
                    SprdMain.Col = ColIGSTPer
                    SprdMain.Text = "0.00"
                Next
            Else
                With SprdMain
                    For cntRow = 1 To SprdMain.MaxRows - 1
                        .Row = cntRow
                        .Col = ColItemCode
                        mItemCode = Trim(.Text)

                        If ADDMode = True Then
                            If lblPurchaseType.Text = "J" Or lblPurchaseType.Text = "R" Then
                                mHSNCode = GetSACCode((txtServProvided.Text))
                                If GetSACDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, mPartyGSTNo, VB.Left(cboGSTStatus.Text, 1)) = False Then GoTo ErrPart
                            Else

                                SprdMain.Row = cntRow
                                SprdMain.Col = ColHSN
                                If Trim(.Text) = "" Then
                                    mHSNCode = GetHSNCode(mItemCode)
                                Else
                                    mHSNCode = Trim(.Text)
                                End If
                                If GetHSNDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, VB.Left(cboGSTStatus.Text, 1), mPartyGSTNo) = False Then GoTo ErrPart
                            End If
                            .Col = ColHSN
                            .Text = mHSNCode
                            .Col = ColCGSTPer
                            .Text = VB6.Format(pCGSTPer, "0.00")
                            .Col = ColSGSTPer
                            .Text = VB6.Format(pSGSTPer, "0.00")
                            .Col = ColIGSTPer
                            .Text = VB6.Format(pIGSTPer, "0.00")
                        End If
                    Next
                End With
            End If
            '        End If
        End If
        Call CalcTots()
        GoTo EventExitSub
ErrPart:
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cboInvType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboInvType.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboInvType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboInvType.SelectedIndexChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboInvType_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles cboInvType.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        'Dim pMKey As String
        If Trim(cboInvType.Text) = "" Then GoTo EventExitSub
        txtDebitAccount.Text = GetDebitNameOfInvType(Trim(cboInvType.Text), "Y")
        If ADDMode = True Then
            Call FillExpFromPartyExp()
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub FillExpFromPartyExp()
        On Error GoTo ERR1
        Dim RS As ADODB.Recordset = Nothing
        Dim xAcctCode As String
        Dim xTrnCode As Double
        Dim I As Integer
        Dim mLocal As String
        Dim SqlStr As String = ""
        Dim mRO As String
        Exit Sub
        If Trim(txtSupplier.Text) = "" Then Exit Sub
        If Trim(cboInvType.Text) = "" Then Exit Sub

        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xAcctCode = MasterNo
        Else
            xAcctCode = "-1"
        End If

        If Trim(txtSupplier.Text) <> "" Then
            If Trim(txtSupplier.Text) <> "" Then
                mLocal = GetPartyBusinessDetail(Trim(xAcctCode), Trim(txtBillTo.Text), "WITHIN_STATE")
                mLocal = IIf(MasterNo = "Y", "L", "C")
            End If

            'If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            '    mLocal = IIf(MasterNo = "Y", "L", "C")
            'Else
            '    mLocal = ""
            'End If
        Else
            mLocal = ""
        End If

        If MainClass.ValidateWithMasterTable((cboInvType.Text), "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P'") = True Then
            xTrnCode = MasterNo
        Else
            xTrnCode = CDbl("-1")
        End If
        SqlStr = "Select IH.*, ID.PERCENT,ID.RO FROM " & vbCrLf & " FIN_INTERFACE_MST IH, FIN_PARTY_INTERFACE_MST ID  " & vbCrLf & " Where IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=ID.COMPANY_CODE(+) " & vbCrLf & " AND IH.CODE=ID.EXPCODE(+) " & vbCrLf & " AND ID.SUPP_CUST_CODE='" & xAcctCode & "'" & vbCrLf & " AND ID.TRNTYPE='" & xTrnCode & "'" & vbCrLf & " AND (IH.Type='P' OR IH.Type='B')  " & vbCrLf & " AND ID.CATEGORY='P' ORDER BY IH.PrintSequence"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        If RS.EOF = False Then
            MainClass.ClearGrid(SprdExp)
            I = 0
            Do While Not RS.EOF
                I = I + 1
                SprdExp.Row = I
                SprdExp.Col = ColExpName
                SprdExp.Text = RS.Fields("Name").Value
                mRO = IIf(IsDBNull(RS.Fields("RO").Value), "N", RS.Fields("RO").Value)
                SprdExp.Col = ColRO
                SprdExp.Value = IIf(mRO = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                SprdExp.Col = ColExpPercent
                SprdExp.Text = Str(IIf(IsDBNull(RS.Fields("PERCENT").Value), 0, Str(RS.Fields("PERCENT").Value)))
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
        FormatSprdExp(-1)
        Call CalcTots()
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        ''Resume
    End Sub
    Private Sub chkCancelled_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCancelled.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub ChkCapital_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkCapital.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        Call CalcTots()
    End Sub
    Private Sub chkCreditRC_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCreditRC.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkESI_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkESI.CheckStateChanged
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        If chkESI.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtESIRate.Enabled = True
            txtESIDeductOn.Enabled = True
            If Val(txtESIRate.Text) = 0 Then
                SqlStr = "SELECT ESI_PER FROM FIN_SUPP_CUST_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtSupplier.Text) & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTemp.EOF = False Then
                    txtESIRate.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("ESI_PER").Value), 0, RsTemp.Fields("ESI_PER").Value), "0.000")
                End If
            End If
        Else
            txtESIRate.Enabled = False
            txtESIDeductOn.Enabled = False
            txtESIRate.Text = CStr(0)
        End If
        txtESIRate.Text = VB6.Format(txtESIRate.Text, "0.000")
        CalcTots()
    End Sub
    Private Sub ChkESIRO_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkESIRO.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        CalcTots()
    End Sub
    Private Sub chkFinalPost_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkFinalPost.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkFOC_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkFOC.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkRejection_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkRejection.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub ChkSTDS_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkSTDS.CheckStateChanged
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        If ChkSTDS.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtSTDSRate.Enabled = True
            txtSTDSDeductOn.Enabled = True
            If Val(txtSTDSRate.Text) = 0 Then
                SqlStr = "SELECT STDS_PER FROM FIN_SUPP_CUST_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtSupplier.Text) & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTemp.EOF = False Then
                    txtSTDSRate.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("STDS_PER").Value), 0, RsTemp.Fields("STDS_PER").Value), "0.000")
                End If
            End If
        Else
            txtSTDSRate.Enabled = False
            txtSTDSDeductOn.Enabled = False
            txtSTDSRate.Text = CStr(0)
        End If
        txtSTDSRate.Text = VB6.Format(txtSTDSRate.Text, "0.000")
        CalcTots()
    End Sub
    Private Sub CheckPORate()
        Dim mCntRow As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mITEM_CODE As String
        Dim mTaxableRate As Double
        Dim mPONo As Double
        Dim mGetREfType As String
        Dim mExchangeRate As Double
        mTaxableRate = 0
        mExchangeRate = 0
        mGetREfType = GetMrrRefNo(Val(txtMRRNo.Text))
        With SprdMain
            For mCntRow = 1 To .MaxRows - 1
                .Row = mCntRow
                .Col = ColPONo
                mPONo = Val(.Text)
                .Col = ColItemCode
                mITEM_CODE = Trim(.Text)
                If mGetREfType = "I" Or mGetREfType = "2" Or mGetREfType = "3" Then
                    If MainClass.ValidateWithMasterTable((txtMRRNo.Text), "AUTO_KEY_MRR", "REF_AUTO_KEY_NO", "INV_GATE_DET", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mPONo = MasterNo
                    Else
                        mPONo = CDbl("-1")
                    End If
                    SqlStr = "SELECT GetSALEITEMPRICE(" & Val(CStr(mPONo)) & ",'','','" & mITEM_CODE & "') AS PORATE, 0 as VOL_DISCRATE  FROM DUAL"
                ElseIf mGetREfType = "P" Then
                    SqlStr = "SELECT GetITEMPRICE_NEW(1,1,TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & Val(CStr(mPONo)) & ",'" & mITEM_CODE & "') AS PORATE, " & vbCrLf & " GetVOL_DISC_ITEM(1,1,TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & Val(CStr(mPONo)) & ", '" & mITEM_CODE & "') AS VOL_DISCRATE FROM DUAL"
                ElseIf mGetREfType = "R" Then
                    SqlStr = "SELECT GetITEMJWRate(" & RsCompany.Fields("COMPANY_CODE").Value & ",1,TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & Val(CStr(mPONo)) & "," & Val(txtMRRNo.Text) & ",'" & mITEM_CODE & "'," & mCntRow & ") AS PORATE, 0 as VOL_DISCRATE  FROM DUAL"
                Else
                    SqlStr = "SELECT 0 AS PORATE, 0 as VOL_DISCRATE  FROM DUAL"
                End If
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTemp.EOF = False Then
                    If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "PORATEEDITABLE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        If MasterNo = "N" Then
                            '                        If mGetREfType = "P" Then
                            '                            mExchangeRate = GetExchangeRate(mPONo)
                            '                        Else
                            '                            mExchangeRate = 1
                            '                        End If
                            .Col = ColPORate
                            .Text = CStr(Val(IIf(IsDBNull(RsTemp.Fields("PORATE").Value), 0, RsTemp.Fields("PORATE").Value))) ''* mExchangeRate
                            .Col = ColVolDiscRate
                            .Text = CStr(Val(IIf(IsDBNull(RsTemp.Fields("VOL_DISCRATE").Value), 0, RsTemp.Fields("VOL_DISCRATE").Value))) ''* mExchangeRate
                        End If
                    End If
                End If
            Next
        End With
    End Sub
    Private Sub ChkSTDSRO_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkSTDSRO.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        CalcTots()
    End Sub
    Private Sub chkTDS_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkTDS.CheckStateChanged
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        If chkTDS.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtTDSRate.Enabled = True
            txtTDSDeductOn.Enabled = True
            txtSection.Enabled = True

            If lblPurchaseSeqType.Text = "1" Or lblPurchaseSeqType.Text = "3" Or lblPurchaseSeqType.Text = "8" Then
                SqlStr = "SELECT NAME, TDS_DEFAULT_PER FROM TDS_SECTION_MST " & vbCrLf _
                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND TDS_ON='P'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTemp.EOF = False Then
                    If Trim(txtSection.Text) = "" Then
                        txtSection.Text = IIf(IsDBNull(RsTemp.Fields("NAME").Value), "", RsTemp.Fields("NAME").Value)
                    End If
                    If Val(txtTDSRate.Text) = 0 Then
                        txtTDSRate.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("TDS_DEFAULT_PER").Value), 0, RsTemp.Fields("TDS_DEFAULT_PER").Value), "0.000")
                    End If
                End If
            Else
                SqlStr = "SELECT NAME, TDS_DEFAULT_PER FROM TDS_SECTION_MST " & vbCrLf _
                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND NAME='" & MainClass.AllowSingleQuote(txtSection.Text) & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTemp.EOF = False Then
                    If Val(txtTDSRate.Text) = 0 Then
                        txtTDSRate.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("TDS_DEFAULT_PER").Value), 0, RsTemp.Fields("TDS_DEFAULT_PER").Value), "0.000")
                    End If
                End If
            End If
            'If Val(txtTDSRate.Text) = 0 Then
            '    SqlStr = "SELECT TDS_PER FROM FIN_SUPP_CUST_MST " & vbCrLf _
            '        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            '        & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtSupplier.Text) & "'"
            '    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            '    If RsTemp.EOF = False Then
            '        txtTDSRate.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("TDS_PER").Value), 0, RsTemp.Fields("TDS_PER").Value), "0.000")
            '    End If
            'End If
        Else
            txtTDSDeductOn.Enabled = False
            txtSection.Enabled = False
            txtTDSRate.Enabled = False
            txtTDSRate.Text = CStr(0)
        End If
        txtTDSRate.Text = VB6.Format(txtTDSRate.Text, "0.000")
        CalcTots()
    End Sub
    Private Sub ChkTDSRO_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkTDSRO.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        CalcTots()
    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click
        On Error GoTo AddErr
        If cmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            SprdMain.Enabled = True
            SprdExp.Enabled = True
            txtVNo.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A" Or mIsAuthorisedUser = True, True, False)
            txtModvatNo.Enabled = False
            txtServNo.Enabled = False
            txtMRRNo.Enabled = True
            CmdSearchMRR.Enabled = True
            'If RsCompany.Fields("PURCHASE_POSTINGTYPE").Value = "B" Then
            '    If Val(LblBookCode.Text) = ConModvatBookCode Then
            '        cboInvType.Enabled = False
            '    Else
            cboInvType.Enabled = True
            '    End If
            'Else
            '    cboInvType.Enabled = False
            'End If
            If txtMRRNo.Enabled = True Then txtMRRNo.Focus()
            pShowCalc = True
        Else
            cmdAdd.Text = ConCmdAddCaption
            ADDMode = False
            MODIFYMode = False
            MainClass.ClearGrid(SprdMain)
            Call FormatSprdMain(-1)
            Show1()
        End If
        Exit Sub
AddErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDelete.Click
        On Error GoTo DelErrPart
        Dim xDCNo As String
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mBookCode As Integer
        Dim mLockBookCode As Integer
        'If PubUserID = "G0416" Then
        'Else
        '    Exit Sub
        'End If
        '    If CheckVoucherDateLock(txtVDate.Text, pMaxDate) = True Then
        '         MsgInformation "Working Company Been Locked till Date : " & pMaxDate & vbCrLf _
        ''                    & "So Unable to Save or Delete. Contact your system administrator."
        '        FieldsVarification = False
        '        Exit Function
        '    End If
        If chkCreditRC.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgBox("Final Credit is Done, So that cann't be Deleted.", MsgBoxStyle.Information)
            Exit Sub
        End If
        If ValidateBranchLocking((txtVDate.Text)) = True Then
            Exit Sub
        End If
        If CDbl(LblBookCode.Text) = ConModvatBookCode Then
            mLockBookCode = CInt(ConLockModvat)
            If ValidateBookLocking(PubDBCn, mLockBookCode, txtVDate.Text) = True Then
                Exit Sub
            End If
        ElseIf CDbl(LblBookCode.Text) = ConPurchaseBookCode And lblSeprateGST.Text = "Y" Then
            mLockBookCode = CInt(ConLockPurchase)
            If ValidateBookLocking(PubDBCn, mLockBookCode, txtVDate.Text) = True Then
                Exit Sub
            End If
        ElseIf CDbl(LblBookCode.Text) = ConPurchaseBookCode And lblSeprateGST.Text = "N" Then
            mLockBookCode = CInt(ConLockModvat)
            If ValidateBookLocking(PubDBCn, mLockBookCode, txtVDate.Text) = True Then
                Exit Sub
            End If
            mLockBookCode = CInt(ConLockPurchase)
            If ValidateBookLocking(PubDBCn, mLockBookCode, txtVDate.Text) = True Then
                Exit Sub
            End If
        End If
        If ValidateAccountLocking(PubDBCn, txtVDate.Text, (txtSupplier.Text), mSupplierCode) = True Then
            Exit Sub
        End If
        If Trim(txtBillNo.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If CDbl(LblBookCode.Text) = ConPurchaseBookCode Then
            If MainClass.GetUserCanModify((txtVDate.Text)) = False Then
                MsgBox("You Have Not Rights to delete back Voucher", MsgBoxStyle.Information)
                Exit Sub
            End If
        End If
        If CDbl(LblBookCode.Text) = ConModvatBookCode Then
            If chkFinalPost.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("Final Bill Post Cann't be Deleted")
                Exit Sub
            End If
        End If
        If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Cancelled Bill Cann't be Deleted.")
            Exit Sub
        End If
        If CheckBillPayment(mSupplierCode, (txtBillNo.Text), "B", (txtBillDate.Text)) = True Then Exit Sub
        If CheckDebitNoteExsits(Val(txtMRRNo.Text)) = True Then Exit Sub
        If Not RsPurchMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()

                If InsertIntoDelAudit(PubDBCn, "FIN_PURCHASE_HDR", (LblMKey.Text), RsPurchMain, "MKEY", "D") = False Then GoTo DelErrPart
                If InsertIntoDelAudit(PubDBCn, "FIN_PURCHASE_DET", (LblMKey.Text), RsPurchDetail, "MKEY", "D") = False Then GoTo DelErrPart
                If InsertIntoDelAudit(PubDBCn, "FIN_PURCHASE_EXP", (LblMKey.Text), RsPurchExp, "MKEY", "D") = False Then GoTo DelErrPart


                If InsertIntoDeleteTrn(PubDBCn, "FIN_PURCHASE_HDR", "MKEY", (LblMKey.Text)) = False Then GoTo DelErrPart

                PubDBCn.Execute("Delete from FIN_PURCHASE_VNO_MST Where Mkey='" & LblMKey.Text & "'")
                If CDbl(LblBookCode.Text) = ConModvatBookCode Then
                    SqlStr = "UPDATE INV_GATE_HDR SET GST_STATUS='N',"
                Else
                    SqlStr = "UPDATE INV_GATE_HDR SET MRR_FINAL_FLAG='N',"
                End If
                SqlStr = SqlStr & vbCrLf & " UPDATE_FROM='N'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf
                SqlStr = SqlStr & vbCrLf & " WHERE AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & " " & vbCrLf & " AND MRR_DATE=TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND Company_Code=" & RsCompany.Fields("Company_Code").Value & ""
                PubDBCn.Execute(SqlStr)
                'If CDbl(LblBookCode.Text) = ConPurchaseBookCode Then
                PubDBCn.Execute("DELETE FROM FIN_PURCHASE_TRN WHERE MKey='" & LblMKey.Text & "' AND BookCode=" & ConPurchaseBookCode & "")
                PubDBCn.Execute("DELETE FROM FIN_POSTED_TRN WHERE MKey='" & LblMKey.Text & "' AND BookType='" & mBookType & "'")        '' AND BookSubType='" & mBookSubType & "'
                PubDBCn.Execute("Delete From FIN_GST_POST_TRN Where Mkey='" & LblMKey.Text & "' AND BookType='" & UCase(mBookType) & "' AND BOOKCODE='" & ConPurchaseBookCode & "'")
                PubDBCn.Execute("Delete from FIN_PURCHASE_EXP Where Mkey='" & LblMKey.Text & "'")
                PubDBCn.Execute("Delete from FIN_PURCHASE_DET Where Mkey='" & LblMKey.Text & "'")
                PubDBCn.Execute("Delete from FIN_PURCHASE_VNO_MST Where Mkey='" & LblMKey.Text & "'")
                PubDBCn.Execute("DELETE FROM FIN_PURCHASE_HDR WHERE MKey='" & LblMKey.Text & "' ")
                PubDBCn.Execute("DELETE FROM FIN_GST_SEQ_MST " & vbCrLf & " WHERE MKEY= '" & LblMKey.Text & "'" & vbCrLf & " AND COMPANY_CODE = " & RsCompany.Fields("Company_Code").Value & "" & vbCrLf & " AND FYEAR = " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND BOOKCODE = '" & LblBookCode.Text & "'" & vbCrLf & " AND BOOKTYPE = '" & mBookType & "'")

                'ElseIf CDbl(LblBookCode.Text) = ConModvatBookCode Then
                '                If chkGSTRefund.Value = vbChecked And chkFinalPost.Value = vbUnchecked Then
                '                    PubDBCn.Execute "Delete From FIN_GST_POST_TRN Where Mkey='" & lblMKey.text & "' AND BookType='" & UCase(mBookType) & "'"
                '                    PubDBCn.Execute "Delete from FIN_PURCHASE_EXP Where Mkey='" & lblMKey.text & "'"
                '                    PubDBCn.Execute "Delete from FIN_PURCHASE_DET Where Mkey='" & lblMKey.text & "'"
                '                    PubDBCn.Execute "Delete from FIN_PURCHASE_TRN Where Mkey='" & lblMKey.text & "'"
                '                    PubDBCn.Execute "Delete from FIN_PURCHASE_VNO_MST Where Mkey='" & lblMKey.text & "'"
                '                    PubDBCn.Execute "DELETE FROM FIN_PURCHASE_HDR WHERE MKey='" & lblMKey.text & "' "
                '
                '                    PubDBCn.Execute "DELETE FROM FIN_GST_SEQ_MST " & vbCrLf _
                ''                        & " WHERE MKEY= '" & lblMKey.text & "'" & vbCrLf _
                ''                        & " AND COMPANY_CODE = " & RsCompany.Fields("Company_Code").Value & "" & vbCrLf _
                ''                        & " AND FYEAR = " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                ''                        & " AND BOOKCODE = '" & LblBookCode.text & "'" & vbCrLf _
                ''                        & " AND BOOKTYPE = '" & mBookType & "'"
                '
                '                End If
                'End If
                PubDBCn.CommitTrans()
                RsPurchMain.Requery() ''.Refresh
                RsPurchDetail.Requery() ''.Refresh
                RsPurchExp.Requery() ''.Refresh
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        '    Resume
        PubDBCn.RollbackTrans() ''
        RsPurchMain.Requery() ''.Refresh
        RsPurchDetail.Requery() ''.Refresh
        RsPurchExp.Requery() ''.Refresh
        If Err.Description <> "" Then
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        '        Resume
    End Sub
    Private Sub cmdeInvoice_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdeInvoice.Click
        On Error GoTo ErrPart
        Dim mMkey As String
        Dim meInvoiceApp As String
        If ADDMode = True Or MODIFYMode = True Then
            Exit Sub
        End If
        meInvoiceApp = IIf(PubUserID = "EINV", "Y", IIf(IsDBNull(RsCompany.Fields("E_INVOICE_APP").Value), "N", RsCompany.Fields("E_INVOICE_APP").Value))
        If meInvoiceApp = "N" Then Exit Sub
        If lblPurchaseSeqType.Text <> "2" Then
            Exit Sub
        End If
        mMkey = Trim(LblMKey.Text)
        If Trim(txtIRNNo.Text) = "" Then
            If WebRequestGenerateIRN(mMkey) = False Then Exit Sub
        Else
            MsgInformation("IRN Already generated.")
            Exit Sub
        End If
        Exit Sub
ErrPart:
        MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub
    Public Function WebRequestGenerateIRN(ByRef pMKey As String) As Boolean
        'On Error GoTo ErrPart
        'Dim url As String
        'Dim mGSTIN As String
        'Dim mTaxSch As String
        'Dim mVersion As String
        'Dim mIrn As String
        'Dim mTran_Catg As String
        'Dim mTran_RegRev As String
        'Dim mTran_Typ As String
        'Dim mTran_EcmTrn As String
        'Dim mTran_EcmGstin As String
        'Dim mDoc_Typ As String
        'Dim mDOC_NO As String
        'Dim mDoc_Dt As String
        'Dim mBillFrom_Gstin As String
        'Dim mBillFrom_TrdNm As String
        'Dim mBillFrom_Bno As String
        'Dim mBillFrom_Bnm As String
        'Dim mBillFrom_Flno As String
        'Dim mBillFrom_Loc As String
        'Dim mBillFrom_Dst As String
        'Dim mBillFrom_Pin As String
        'Dim mBillFrom_Stcd As String
        'Dim mBillFrom_Ph As String
        'Dim mBillFrom_Em As String
        'Dim mBillTo_Gstin As String
        'Dim mBillTo_TrdNm As String
        'Dim mBillTo_Bno As String
        'Dim mBillTo_Bnm As String
        'Dim mBillTo_Flno As String
        'Dim mBillTo_Loc As String
        'Dim mBillTo_Dst As String
        'Dim mBillTo_Pin As String
        'Dim mBillTo_Stcd As String
        'Dim mBillTo_Ph As String
        'Dim mBillTo_Em As String
        'Dim mToPlace As String
        'Dim mItem_PrdNm As String
        'Dim mItem_PrdDesc As String
        'Dim mItem_HsnCd As String
        'Dim mItem_Barcde As String
        'Dim mItem_Qty As Double
        'Dim mItem_FreeQty As Double
        'Dim mItem_Unit As String
        'Dim mItem_UnitPrice As Double
        'Dim mItem_TotAmt As Double
        'Dim mItem_Discount As Double
        'Dim mItem_OthChrg As Double
        'Dim mItem_AssAmt As Double
        'Dim mItem_CgstRt As Double
        'Dim mItem_SgstRt As Double
        'Dim mItem_IgstRt As Double
        'Dim mItem_CgstAmt As Double
        'Dim mItem_SgstAmt As Double
        'Dim mItem_IgstAmt As Double
        'Dim mItem_CesRt As Double
        'Dim mItem_CesNonAdval As Double
        'Dim mItem_StateCes As Double
        'Dim mItem_TotItemVal As Double
        'Dim mItem_Bch_Nm As String
        'Dim mItem_Bch_ExpDt As String
        'Dim mItem_Bch_WrDt As String
        'Dim mVal_AssVal As Double
        'Dim mVal_CgstVal As Double
        'Dim mVal_SgstVal As Double
        'Dim mVal_IgstVal As Double
        'Dim mVal_CesVal As Double
        'Dim mVal_StCesVal As Double
        'Dim mVal_CesNonAdVal As Double
        'Dim mVal_Disc As Double
        'Dim mVal_OthChrg As Double
        'Dim mVal_TotInvVal As Double
        'Dim mPay_Nam As String
        'Dim mPay_Mode As String
        'Dim mPay_PayTerm As String
        'Dim mPay_PayInstr As String
        'Dim mPay_CrDay As String
        'Dim mPay_BalAmt As Double
        'Dim mPay_PayDueDt As String
        'Dim mRef_InvRmk As String
        'Dim mRef_InvStDt As String
        'Dim mRef_InvEndDt As String
        ''Dim mTran_EcmGstin As String
        'Dim mDoc_OrgInvNo As String
        'Dim mShipFrom_Gstin As String
        'Dim mShipFrom_TrdNm As String
        'Dim mShipFrom_Loc As String
        'Dim mShipFrom_Pin As String
        'Dim mShipFrom_Stcd As String
        'Dim mShipFrom_Bno As String
        'Dim mShipFrom_Bnm As String
        'Dim mShipFrom_Flno As String
        'Dim mShipFrom_Dst As String
        'Dim mShipFrom_Ph As String
        'Dim mShipFrom_Em As String
        'Dim mStateName As String
        'Dim mShipTo_Gstin As String
        'Dim mShipTo_TrdNm As String
        'Dim mShipTo_Loc As String
        'Dim mShipTo_Pin As String
        'Dim mShipTo_Stcd As String
        'Dim mShipTo_Bno As String
        'Dim mShipTo_Bnm As String
        'Dim mShipTo_Flno As String
        'Dim mShipTo_Dst As String
        'Dim mShipTo_Ph As String
        'Dim mShipTo_Em As String
        'Dim mPay_FinInsBr As String
        'Dim mPay_CrTrn As String
        'Dim mPay_DirDr As String
        'Dim mPay_AcctDet As String
        'Dim mRef_PrecInvNo As String
        'Dim mRef_PrecInvDt As String
        'Dim mRef_RecAdvRef As String
        'Dim mRef_TendRef As String
        'Dim mRef_ContrRef As String
        'Dim mRef_ExtRef As String
        'Dim mRef_ProjRef As String
        'Dim mRef_PORef As String
        'Dim mExp_ExpCat As String
        'Dim mExp_WthPay As String
        'Dim mExp_InvForCur As String
        'Dim mExp_ForCur As String
        'Dim mExp_CntCode As String
        'Dim mExp_ShipBNo As String
        'Dim mExp_ShipBDt As String
        'Dim mExp_Port As String
        'Dim mGetQRImg As String
        'Dim mGetSignedInvoice As String
        'Dim mCDKey As String
        'Dim mEInvUserName As String
        'Dim mEInvPassword As String
        'Dim mEFUserName As String
        'Dim mEFPassword As String
        'Dim pStateName As String
        'Dim pStateCode As String
        'Dim cntRow As Integer
        'Dim mSqlStr As String
        'Dim RsTemp As ADODB.Recordset = Nothing
        'Dim mBody As String
        'Dim mResponseId As String
        'Dim mResponseIdStr As String
        'Dim url1 As String
        'Dim WebRequestGen As String
        'Dim pStaus As String
        'Dim mIRNNo As String
        'Dim mIRNAckNo As String
        'Dim mIRNAckDate As String
        'Dim pError As String
        'Dim mSignedQRCode As String
        'Dim mSignedInvoice As String
        ''Dim pUserId As String
        'Dim mBMPFileName As String
        'Dim pResponseText As String
        'Dim SqlStr As String=""
        'If GeteInvoiceSetupContents(url, "G", mCDKey, mEFUserName, mEFPassword, mEInvUserName, mEInvPassword) = False Then GoTo ErrPart
        ''    url = "http://einvsandbox.webtel.in/v1.03/GenIRN"
        ''    mCDKey = "1000687"
        ''    mEInvUserName = "06AAACH0118F2Z9"       ''"06AAACW3775F013"
        ''    mEInvPassword = "Admin!23"
        ''    mEFUserName = "29AAACW3775F000"
        ''    mEFPassword = "Admin!23.."
        '      '22/10/2021 Dim http As MSXML2.XMLHTTP60 '' MSXML.xmlhttp
        '      '22/10/2021 http = CreateObject("MSXML2.ServerXMLHTTP")
        'mGSTIN = IIf(IsDbNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
        'mTaxSch = "GST"
        'mVersion = "1.0"
        'mIrn = ""
        ''    If lblInvoiceSeq.text = 6 Then
        ''        mTran_Catg = "EXPWP"
        ''    Else
        'mTran_Catg = "B2B"
        ''    End If
        'mTran_RegRev = "N"
        'If chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked Then
        'mTran_Typ = "REG"
        'ElseIf chkShipTo.CheckState = System.Windows.Forms.CheckState.Unchecked Then 
        'mTran_Typ = "SHP"
        'End If
        'mTran_EcmTrn = "N"
        'mTran_EcmGstin = ""
        'mDoc_Typ = "CRN"
        'mDOC_NO = Trim(txtVNoPrefix.Text) & Trim(txtVno.Text)
        'mDoc_Dt = VB6.Format(TxtVDate.Text, "DD/MM/YYYY")
        'mDoc_OrgInvNo = ""
        'mBillFrom_Gstin = IIf(IsDbNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
        'mBillFrom_TrdNm = IIf(IsDbNull(RsCompany.Fields("Company_Name").Value), "", RsCompany.Fields("Company_Name").Value)
        'mBillFrom_Bno = IIf(IsDbNull(RsCompany.Fields("COMPANY_ADDR").Value), "", RsCompany.Fields("COMPANY_ADDR").Value)
        'mBillFrom_Bnm = ""
        'mBillFrom_Flno = ""
        'mBillFrom_Loc = IIf(IsDbNull(RsCompany.Fields("COMPANY_CITY").Value), "", RsCompany.Fields("COMPANY_CITY").Value)
        'mBillFrom_Dst = ""
        'mBillFrom_Pin = IIf(IsDbNull(RsCompany.Fields("COMPANY_PIN").Value), "", RsCompany.Fields("COMPANY_PIN").Value)
        'pStateName = IIf(IsDbNull(RsCompany.Fields("COMPANY_STATE").Value), "", RsCompany.Fields("COMPANY_STATE").Value)
        'pStateCode = GetStateCode(pStateName)
        'mBillFrom_Stcd = pStateCode
        'mBillFrom_Ph = ""
        'mBillFrom_Em = ""
        'mSqlStr = " SELECT SUPP_CUST_ADDR, SUPP_CUST_CITY,SUPP_CUST_STATE,SUPP_CUST_PIN,GST_RGN_NO" & vbCrLf & " FROM FIN_SUPP_CUST_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote((txtSupplier.Text)) & "'"
        'MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        'If RsTemp.EOF = False Then
        'mBillTo_TrdNm = Trim(txtSupplier.Text)
        'mBillTo_Bno = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_ADDR").Value), "", RsTemp.Fields("SUPP_CUST_ADDR").Value)
        'mBillTo_Bnm = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value)
        'mBillTo_Flno = ""
        'mBillTo_Loc = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value)
        'mBillTo_Dst = ""
        'mBillTo_Ph = ""
        'mBillTo_Em = ""
        'mToPlace = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_STATE").Value), "", RsTemp.Fields("SUPP_CUST_STATE").Value)
        ''        If lblInvoiceSeq.text = 6 Then
        ''            mBillTo_Gstin = "URP"
        ''            mBillTo_Pin = "999999"
        ''            mBillTo_Stcd = 99
        ''        Else
        'mBillTo_Gstin = IIf(IsDbNull(RsTemp.Fields("GST_RGN_NO").Value), "", RsTemp.Fields("GST_RGN_NO").Value)
        'mBillTo_Pin = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_PIN").Value), "", RsTemp.Fields("SUPP_CUST_PIN").Value)
        'mBillTo_Stcd = GetStateCode(mToPlace)
        ''        End If
        'Else
        'MsgInformation("Invalid Customer Name, Please Select Valid Customer Name.")
        'WebRequestGenerateIRN = False
        'http = Nothing
        'Exit Function
        'End If
        ''    If chkDespatchFrom.Value = vbChecked Then
        ''        mSqlStr = " SELECT SUPP_CUST_NAME, SUPP_CUST_ADDR, SUPP_CUST_CITY,SUPP_CUST_STATE,SUPP_CUST_PIN,GST_RGN_NO" & vbCrLf _
        ' ''                & " FROM FIN_SUPP_CUST_MST" & vbCrLf _
        ' ''                & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
        ' ''                & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtShippedFrom.Text) & "'"
        ''
        ''        MainClass.UOpenRecordSet mSqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly
        ''
        ''        If RsTemp.EOF = False Then
        ''            mShipFrom_Gstin = IIf(IsNull(RsTemp!GST_RGN_NO), "", RsTemp!GST_RGN_NO)
        ''            mShipFrom_TrdNm = Trim(IIf(IsNull(RsTemp!SUPP_CUST_NAME), "", RsTemp!SUPP_CUST_NAME))
        ''            mShipFrom_Loc = IIf(IsNull(RsTemp!SUPP_CUST_CITY), "", RsTemp!SUPP_CUST_CITY)
        ''            mShipFrom_Pin = IIf(IsNull(RsTemp!SUPP_CUST_PIN), "", RsTemp!SUPP_CUST_PIN)
        ''            mStateName = IIf(IsNull(RsTemp!SUPP_CUST_STATE), "", RsTemp!SUPP_CUST_STATE)
        ''            mShipFrom_Stcd = GetStateCode(mStateName)
        ''            mShipFrom_Bno = IIf(IsNull(RsTemp!SUPP_CUST_ADDR), "", RsTemp!SUPP_CUST_ADDR)
        ''            mShipFrom_Bnm = IIf(IsNull(RsTemp!SUPP_CUST_CITY), "", RsTemp!SUPP_CUST_CITY)
        ''            mShipFrom_Flno = ""
        ''            mShipFrom_Dst = ""
        ''            mShipFrom_Ph = ""
        ''            mShipFrom_Em = ""
        ''        Else
        ''            MsgInformation "Invalid Shipped From Customer Name, Please Select Valid Shipped From Customer Name."
        ''            WebRequestGenerateIRN = False
        ''            Set http = Nothing
        ''            Exit Function
        ''        End If
        ''    Else
        'mShipFrom_Gstin = ""
        'mShipFrom_TrdNm = ""
        'mShipFrom_Loc = ""
        'mShipFrom_Pin = ""
        'mShipFrom_Stcd = ""
        'mShipFrom_Bno = ""
        'mShipFrom_Bnm = ""
        'mShipFrom_Flno = ""
        'mShipFrom_Dst = ""
        'mShipFrom_Ph = ""
        'mShipFrom_Em = ""
        ''    End If
        'If chkShipTo.CheckState = System.Windows.Forms.CheckState.Unchecked Then
        'mSqlStr = " SELECT SUPP_CUST_NAME,SUPP_CUST_ADDR, SUPP_CUST_CITY,SUPP_CUST_STATE,SUPP_CUST_PIN,GST_RGN_NO" & vbCrLf & " FROM FIN_SUPP_CUST_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtShippedTo.Text) & "'"
        'MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        'If RsTemp.EOF = False Then
        'mShipTo_TrdNm = Trim(IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value))
        'mShipTo_Loc = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value)
        'mShipTo_Bno = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_ADDR").Value), "", RsTemp.Fields("SUPP_CUST_ADDR").Value)
        'mShipTo_Bnm = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value)
        'mShipTo_Flno = ""
        'mShipTo_Dst = ""
        'mShipTo_Ph = ""
        'mShipTo_Em = ""
        ''            If lblInvoiceSeq.text = 6 Then
        ''                mShipTo_Gstin = "URP"
        ''                mShipTo_Pin = "999999"
        ''                mShipTo_Stcd = 99
        ''            Else
        'mShipTo_Gstin = IIf(IsDbNull(RsTemp.Fields("GST_RGN_NO").Value), "", RsTemp.Fields("GST_RGN_NO").Value)
        'mShipTo_Pin = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_PIN").Value), "", RsTemp.Fields("SUPP_CUST_PIN").Value)
        'mStateName = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_STATE").Value), "", RsTemp.Fields("SUPP_CUST_STATE").Value)
        'mShipTo_Stcd = GetStateCode(mStateName)
        ''            End If
        'Else
        'MsgInformation("Invalid Shipped to Customer Name, Please Select Valid Shipped To Customer Name.")
        'WebRequestGenerateIRN = False
        'http = Nothing
        'Exit Function
        'End If
        'Else
        'mShipTo_Gstin = ""
        'mShipTo_TrdNm = ""
        'mShipTo_Loc = ""
        'mShipTo_Pin = ""
        'mShipTo_Stcd = ""
        'mShipTo_Bno = ""
        'mShipTo_Bnm = ""
        'mShipTo_Flno = ""
        'mShipTo_Dst = ""
        'mShipTo_Ph = ""
        'mShipTo_Em = ""
        'End If
        'mVal_AssVal = Val(lblTotTaxableAmt.Text)
        'mVal_CgstVal = Val(lblTotCGSTAmount.Text)
        'mVal_SgstVal = Val(lblTotSGSTAmount.Text)
        'mVal_IgstVal = Val(lblTotIGSTAmount.Text)
        'mVal_CesVal = 0
        'mVal_StCesVal = 0
        'mVal_CesNonAdVal = 0
        'mVal_TotInvVal = Val(lblNetAmount.Text)
        'mVal_OthChrg = CDbl(VB6.Format(mVal_TotInvVal - (mVal_AssVal + mVal_CgstVal + mVal_SgstVal + mVal_IgstVal + Val(lblMSC.Text)), "0.00")) 'Val(lblTotExpAmt.text)  ''
        ''    mVal_OthChrg = Format(mVal_TotInvVal - (Val(lblTotItemValue.text) + mVal_CgstVal + mVal_SgstVal + mVal_IgstVal + Val(lblRO.text)), "0.00")
        ''    mVal_OthChrg = Val(lblMSC.text)
        'mVal_Disc = Val(lblMSC.Text) * -1
        ''    If mVal_OthChrg < 0 Then
        ''        mVal_Disc = Format(mVal_OthChrg * -1, "0.00")
        ''        mVal_OthChrg = Format(mVal_AssVal - Val(lblTotItemValue.text), "0.00")
        ''    Else
        ''        mVal_Disc = 0
        ''        mVal_OthChrg = Format(mVal_OthChrg + mVal_AssVal - Val(lblTotItemValue.text), "0.00")
        ''    End If
        ''pInvoiceValue = Format(lblNetAmount.text, "0.00")
        ''    pTaxableValue = Format(lblTaxableAmount.text, "0.00")
        ''
        ''    pCGSTValue = Format(lblCGSTAmt.text, "0.00")
        ''    pSGSTValue = Format(lblSGSTAmt.text, "0.00")
        ''    pIGSTValue = Format(lblIGSTAmt.text, "0.00")
        ''
        ''    pOtherValue = Format(pInvoiceValue - (pTaxableValue + pCGSTValue + pSGSTValue + pIGSTValue), "0.00")
        ''
        'mPay_Nam = ""
        'mPay_Mode = ""
        'mPay_PayTerm = ""
        'mPay_PayInstr = ""
        'mPay_CrDay = ""
        'mPay_BalAmt = 0
        'mPay_PayDueDt = ""
        'mRef_InvRmk = ""
        'mRef_InvStDt = ""
        'mRef_InvEndDt = ""
        'mTran_EcmGstin = ""
        'mPay_FinInsBr = ""
        'mPay_CrTrn = ""
        'mPay_DirDr = ""
        'mPay_AcctDet = ""
        'mRef_PrecInvNo = ""
        'mRef_PrecInvDt = ""
        'mRef_RecAdvRef = ""
        'mRef_TendRef = ""
        'mRef_ContrRef = ""
        'mRef_ExtRef = ""
        'mRef_ProjRef = ""
        'mRef_PORef = ""
        'mExp_ExpCat = ""
        'mExp_WthPay = ""
        'mExp_InvForCur = ""
        ''    If lblInvoiceSeq.text = 6 Then
        ''        mExp_ShipBNo = Trim(txtShippingNo.Text)
        ''        mExp_ShipBDt = Format(txtShippingDate.Text, "DD/MM/YYYY")
        ''        mExp_Port = Trim(txtPortCode.Text)
        ''
        ''        If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "CURRENCY_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
        ''            mExp_ForCur = MasterNo
        ''        End If
        ''
        ''        If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_NAME", "COUNTRY_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
        ''            mExp_CntCode = MasterNo
        ''        End If
        ''
        ''    Else
        'mExp_ShipBNo = ""
        'mExp_ShipBDt = ""
        'mExp_Port = ""
        'mExp_ForCur = ""
        'mExp_CntCode = ""
        ''    End If
        'mGetQRImg = "0" ''0 for text , 1 for Image
        'mGetSignedInvoice = "0" ''1 - Signed Json of Invoice will be return, 0 - will not return signed Invoice.
        'http.Open("POST", url, False)
        'http.setRequestHeader("Content-Type", "application/json")
        'With JB
        '.Clear()
        '.IsArray_Renamed = False 'Actually the default after Clear.
        'With .AddNewObject("Push_Data_List")
        'With .AddNewArray("Data") ''With .AddNewArray("Push_Data_List")
        'For cntRow = 1 To SprdMain.MaxRows - 1
        'With .AddNewObject()
        '.Item("Gstin") = mGSTIN
        '.Item("Version") = mVersion
        '.Item("Irn") = mIrn
        '.Item("Tran_TaxSch") = mTaxSch 'check
        '.Item("Tran_SupTyp") = mTran_Catg ''  .Item("Tran_Catg")
        '.Item("Tran_RegRev") = mTran_RegRev
        '.Item("Tran_Typ") = mTran_Typ
        ''                        .Item("Tran_EcmTrn") = mTran_EcmTrn  'check
        '.Item("Tran_EcmGstin") = mTran_EcmGstin
        '.Item("Tran_IgstOnIntra") = "N" ''Y- indicates the supply is intra state but chargeable to IGST
        '.Item("Doc_Typ") = mDoc_Typ
        '.Item("DOC_NO") = mDOC_NO
        '.Item("Doc_Dt") = mDoc_Dt
        '.Item("BillFrom_Gstin") = mBillFrom_Gstin
        '.Item("BillFrom_LglNm") = mBillFrom_TrdNm
        '.Item("BillFrom_TrdNm") = mBillFrom_TrdNm
        '.Item("BillFrom_Addr1") = mBillFrom_Bno
        '.Item("BillFrom_Addr2") = mBillFrom_Bnm
        ''                        .Item("BillFrom_Flno") = mBillFrom_Flno
        '.Item("BillFrom_Loc") = mBillFrom_Loc
        ''                        .Item("BillFrom_Dst") = mBillFrom_Dst
        '.Item("BillFrom_Pin") = mBillFrom_Pin
        '.Item("BillFrom_Stcd") = mBillFrom_Stcd
        '.Item("BillFrom_Ph") = mBillFrom_Ph
        '.Item("BillFrom_Em") = mBillFrom_Em
        '.Item("BillTo_Gstin") = mBillTo_Gstin
        '.Item("BillTo_LglNm") = mBillTo_TrdNm
        '.Item("BillTo_TrdNm") = mBillTo_TrdNm
        '.Item("BillTo_Pos") = mBillTo_Stcd
        '.Item("BillTo_Addr1") = mBillTo_Bno
        '.Item("BillTo_Addr2") = mBillTo_Bnm
        ''                        .Item("BillTo_Flno") = mBillTo_Flno
        '.Item("BillTo_Loc") = mBillTo_Loc
        ''                        .Item("BillTo_Dst") = mBillTo_Dst
        '.Item("BillTo_Pin") = mBillTo_Pin
        '.Item("BillTo_Stcd") = mBillTo_Stcd
        '.Item("BillTo_Ph") = mBillTo_Ph
        '.Item("BillTo_Em") = mBillTo_Em
        'SprdMain.Row = cntRow
        'SprdMain.Col = ColItemCode
        'SprdMain.Col = ColItemDesc
        'mItem_PrdNm = Trim(SprdMain.Text)
        'mItem_PrdDesc = Trim(SprdMain.Text)
        'SprdMain.Col = ColHSN
        'mItem_HsnCd = Trim(SprdMain.Text)
        'mItem_Barcde = ""
        'SprdMain.Col = ColQty
        'mItem_Qty = CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))
        'mItem_FreeQty = 0
        'SprdMain.Col = ColUnit
        'mItem_Unit = Trim(SprdMain.Text)
        'SprdMain.Col = ColRate
        'mItem_UnitPrice = CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))
        'SprdMain.Col = ColTaxableAmount ''ColAmount
        'mItem_TotAmt = CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))
        'SprdMain.Col = ColTaxableAmount
        'mItem_AssAmt = CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))
        'mItem_Discount = 0
        ''                        If Val(lblTotItemValue.text) <> 0 Then
        ''                            mItem_Discount = Format(mVal_Disc * mItem_TotAmt / Val(lblTotItemValue.text), "0.00")
        ''                        End If
        'mItem_OthChrg = mItem_AssAmt - mItem_TotAmt
        'mItem_OthChrg = CDbl(VB6.Format(mItem_OthChrg, "0.00"))
        ''                        mItem_OthChrg = mItem_OthChrg - mItem_Discount
        ''                        If mItem_OthChrg < 0 Then
        ''                            mItem_Discount = mItem_OthChrg
        ''                            mItem_OthChrg = 0
        ''                        Else
        ''                            mItem_Discount = 0
        ''                            mItem_OthChrg = mItem_OthChrg
        ''                        End If
        ''                     = ""
        ''                    mItem_TotItemVal = ""
        ''                    mItem_Bch_Nm = ""
        ''                    mItem_Bch_ExpDt = ""
        ''                    mItem_Bch_WrDt = ""
        'SprdMain.Col = ColSGSTPer
        'mItem_SgstRt = CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))
        'SprdMain.Col = ColCGSTPer
        'mItem_CgstRt = CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))
        'SprdMain.Col = ColIGSTPer
        'mItem_IgstRt = CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))
        'SprdMain.Col = ColSGSTAmount
        'mItem_SgstAmt = CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))
        'SprdMain.Col = ColCGSTAmount
        'mItem_CgstAmt = CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))
        'SprdMain.Col = ColIGSTAmount
        'mItem_IgstAmt = CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))
        'mItem_CesRt = 0
        'mItem_CesNonAdval = 0
        'mItem_StateCes = 0
        'mItem_TotItemVal = mItem_TotAmt + mItem_SgstAmt + mItem_CgstAmt + mItem_IgstAmt + mItem_CesNonAdval + mItem_StateCes + mItem_OthChrg ''- mItem_Discount '' mItem_AssAmt 30/09' (mItem_AssAmt * ((100 + mItem_SgstRt + mItem_CgstRt + mItem_IgstRt + mItem_CesRt + mItem_StateCes) * 0.01)) + mItem_CesNonAdval
        'mItem_TotItemVal = CDbl(VB6.Format(mItem_TotItemVal, "0.00"))
        '.Item("Item_SlNo") = cntRow
        ''                        .Item("Item_PrdNm") = mItem_PrdNm  '' Not required
        '.Item("Item_PrdDesc") = mItem_PrdDesc
        '.Item("Item_IsServc") = "N" '' IIf(lblInvoiceSeq.text = 2, "Y", "N")
        '.Item("Item_HsnCd") = mItem_HsnCd
        '.Item("Item_Barcde") = mItem_Barcde
        '.Item("Item_Qty") = mItem_Qty
        '.Item("Item_FreeQty") = mItem_FreeQty
        '.Item("Item_Unit") = mItem_Unit
        '.Item("Item_UnitPrice") = mItem_UnitPrice
        '.Item("Item_TotAmt") = mItem_TotAmt
        '.Item("Item_Discount") = mItem_Discount
        '.Item("Item_PreTaxVal") = mItem_TotAmt
        '.Item("Item_AssAmt") = mItem_AssAmt
        '.Item("Item_GstRt") = mItem_CgstRt + mItem_SgstRt + mItem_IgstRt
        '.Item("Item_IgstAmt") = mItem_IgstAmt
        '.Item("Item_CgstAmt") = mItem_CgstAmt
        '.Item("Item_SgstAmt") = mItem_SgstAmt
        '.Item("Item_CesRt") = mItem_CesRt
        '.Item("Item_CesAmt") = ""
        '.Item("Item_CesNonAdvlAmt") = mItem_CesNonAdval
        '.Item("Item_StateCesRt") = ""
        '.Item("Item_StateCesAmt") = ""
        '.Item("Item_StateCesNonAdvlAmt") = ""
        '.Item("Item_OthChrg") = mItem_OthChrg
        '.Item("Item_TotItemVal") = mItem_TotItemVal
        '.Item("Item_OrdLineRef") = ""
        '.Item("Item_OrgCntry") = ""
        '.Item("Item_PrdSlNo") = ""
        '.Item("Item_Attrib_Nm") = ""
        '.Item("Item_Attrib_Val") = ""
        '.Item("Item_Bch_Nm") = mItem_Bch_Nm
        '.Item("Item_Bch_ExpDt") = mItem_Bch_ExpDt
        '.Item("Item_Bch_WrDt") = mItem_Bch_WrDt
        '.Item("Val_AssVal") = mVal_AssVal
        '.Item("Val_CgstVal") = mVal_CgstVal
        '.Item("Val_SgstVal") = mVal_SgstVal
        '.Item("Val_IgstVal") = mVal_IgstVal
        '.Item("Val_CesVal") = mVal_CesVal
        '.Item("Val_StCesVal") = mVal_StCesVal
        ''                        .Item("Val_CesNonAdVal") = mVal_CesNonAdVal
        '.Item("Val_Discount") = mVal_Disc
        '.Item("Val_OthChrg") = mVal_OthChrg
        '.Item("Val_RndOffAmt") = VB6.Format(Val(lblRO.Text), "0.00")
        '.Item("Val_TotInvVal") = mVal_TotInvVal
        '.Item("Val_TotInvValFc") = ""
        '.Item("Pay_Nm") = mPay_Nam
        '.Item("Pay_AcctDet") = mPay_AcctDet
        '.Item("Pay_Mode") = mPay_Mode
        '.Item("Pay_FinInsBr") = mPay_FinInsBr
        '.Item("Pay_PayTerm") = mPay_PayTerm
        '.Item("Pay_PayInstr") = mPay_PayInstr
        '.Item("Pay_CrTrn") = mPay_CrTrn
        '.Item("Pay_DirDr") = mPay_DirDr
        '.Item("Pay_CrDay") = mPay_CrDay
        '.Item("Pay_PaidAmt") = ""
        '.Item("Pay_BalAmt") = mPay_BalAmt
        '.Item("Pay_PaymtDue") = mPay_PayDueDt
        '.Item("Ref_InvRmk") = mRef_InvRmk
        '.Item("Ref_InvStDt") = mRef_InvStDt
        '.Item("Ref_InvEndDt") = mRef_InvEndDt
        '.Item("Doc_OrgInvNo") = mDoc_OrgInvNo
        '.Item("ShipFrom_Gstin") = mShipFrom_Gstin
        ''                        .Item("ShipFrom_TrdNm") = mShipFrom_TrdNm
        '.Item("ShipFrom_Nm") = mShipFrom_TrdNm
        '
        '.Item("ShipFrom_Addr1") = mShipFrom_Bno
        '.Item("ShipFrom_Addr2") = mShipFrom_Bnm
        '.Item("ShipFrom_Loc") = mShipFrom_Loc
        '.Item("ShipFrom_Pin") = mShipFrom_Pin
        '.Item("ShipFrom_Stcd") = mShipFrom_Stcd
        ''                        .Item("ShipFrom_Bno") = mShipFrom_Bno
        ''                        .Item("ShipFrom_Bnm") = mShipFrom_Bnm
        ''                        .Item("ShipFrom_Flno") = mShipFrom_Flno
        ''                        .Item("ShipFrom_Dst") = mShipFrom_Dst
        ''                        .Item("ShipFrom_Ph") = mShipFrom_Ph
        ''                        .Item("ShipFrom_Em") = mShipFrom_Em
        '.Item("ShipTo_Gstin") = mShipTo_Gstin
        '.Item("ShipTo_LglNm") = mShipTo_TrdNm
        '.Item("ShipTo_TrdNm") = mShipTo_TrdNm
        '.Item("ShipTo_Addr1") = mShipTo_Bno
        '.Item("ShipTo_Addr2") = mShipTo_Loc
        '.Item("ShipTo_Loc") = mShipTo_Loc
        '.Item("ShipTo_Pin") = mShipTo_Pin
        '.Item("ShipTo_Stcd") = mShipTo_Stcd
        ''                        .Item("ShipTo_Bno") = mShipTo_Bno
        ''                        .Item("ShipTo_Bnm") = mShipTo_Bnm
        ''                        .Item("ShipTo_Flno") = mShipTo_Flno
        ''                        .Item("ShipTo_Dst") = mShipTo_Dst
        ''                        .Item("ShipTo_Ph") = mShipTo_Ph
        ''                        .Item("ShipTo_Em") = mShipTo_Em
        '.Item("Ref_PrecDoc_InvNo") = mRef_PrecInvNo
        '.Item("Ref_PrecDoc_InvDt") = mRef_PrecInvDt
        '.Item("Ref_PrecDoc_OthRefNo") = ""
        '.Item("Ref_Contr_RecAdvRefr") = mRef_RecAdvRef
        '.Item("Ref_Contr_RecAdvDt") = ""
        '.Item("Ref_Contr_TendRefr") = mRef_TendRef
        '.Item("Ref_Contr_ContrRefr") = mRef_ContrRef
        ''                        .Item("Ref_ExtRef") = mRef_ExtRef
        '.Item("Ref_Contr_ExtRefr") = mRef_ProjRef
        '.Item("Ref_Contr_ProjRefr") = ""
        '.Item("Ref_Contr_PORefr") = mRef_PORef
        '.Item("Ref_Contr_PORefDt") = ""
        '.Item("AddlDoc_Url") = ""
        '.Item("AddlDoc_Docs") = ""
        '.Item("AddlDoc_Info") = ""
        ''                        .Item("Exp_ExpCat") = mExp_ExpCat
        ''                        .Item("Exp_WthPay") = mExp_WthPay
        ''                        .Item("Exp_InvForCur") = mExp_InvForCur
        '.Item("Exp_ForCur") = mExp_ForCur
        '.Item("Exp_CntCode") = mExp_CntCode
        '.Item("Exp_ShipBNo") = mExp_ShipBNo
        '.Item("Exp_ShipBDt") = mExp_ShipBDt
        '.Item("Exp_Port") = mExp_Port
        ''                        .Item("GetQRImg") = mGetQRImg       ''29/09/2020
        ''                        .Item("GetSignedInvoice") = mGetSignedInvoice ''29/09/2020
        '.Item("CDKey") = mCDKey
        '.Item("EInvUserName") = mEInvUserName
        '.Item("EInvPassword") = mEInvPassword
        '.Item("EFUserName") = mEFUserName
        '.Item("EFPassword") = mEFPassword
        'End With
        'Next 
        'End With
        'End With
        'mBody = .JSON
        'End With
        '' shipToGSTIN String GSTIN of  Ship-To shipToTradeName String Trade Name of  Ship-To dispatchFromGSTIN String GSTIN of Dispatch-From dispatchFromTradeName String Trade Name of Dispatch-From IsBillFromShipFromSame String Required, 0 for Different BillFrom and ShipFrom, 1 for Same BillFrom and ShipFrom IsBillToShipToSame String Required, 0 for Different BillTo and ShipTo, 1 for Same BillTo and ShipTo IsGSTINSEZ
        'http.Send(mBody)
        'pResponseText = http.responseText
        ''    pResponseText = Replace(pResponseText, "\", "")
        'pResponseText = Replace(pResponseText, "[", "")
        'pResponseText = Replace(pResponseText, "]", "")
        ''    pResponseText = Mid(pResponseText, 2, Len(pResponseText) - 2)
        'Dim JsonTest As Object
        'Dim SB As New cStringBuilder
        'Dim c As Object
        'Dim I As Integer
        'JsonTest = JSON.parse(pResponseText)
        'pStaus = JsonTest.Item("Status")
        'If pStaus = "1" Then
        'mIRNNo = JsonTest.Item("Irn")
        'mIRNAckNo = JsonTest.Item("AckNo")
        'mIRNAckDate = JsonTest.Item("AckDate") 'JsonTest.Item("elements").Item(mResponseId).Item("ewayBillDate")
        'mSignedQRCode = JsonTest.Item("SignedQRCode")
        'mSignedInvoice = JsonTest.Item("SignedInvoice")
        'txtIRNNo.Text = Trim(mIRNNo)
        'txteInvAckNo.Text = Trim(mIRNAckNo)
        'txteInvAckDate.Text = VB6.Format(mIRNAckDate, "DD/MM/YYYY HH:MM")
        'PubDBCn.Errors.Clear()
        'PubDBCn.BeginTrans()
        'SqlStr = ""
        'SqlStr = "UPDATE FIN_PURCHASE_HDR SET " & vbCrLf & " IRN_NO ='" & Trim(txtIRNNo.Text) & "'," & vbCrLf & " IRN_ACK_NO ='" & Trim(txteInvAckNo.Text) & "'," & vbCrLf & " IRN_ACK_DATE =TO_DATE('" & VB6.Format(txteInvAckDate.Text, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI')" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MKEY ='" & pMKey & "'"
        'PubDBCn.Execute(SqlStr)
        'PubDBCn.CommitTrans()
        ''        mBMPFileName = mPubBarCodePath & "\" & Trim(txtVNoPrefix.Text) & Trim(txtVno.Text) & ".bmp"
        ''
        ''        If GererateQRCodeImage(mBMPFileName, mSignedQRCode) = False Then GoTo ErrPart
        ''
        ''        If UpdateQRCODE(LblMKey.text, mBMPFileName) = False Then GoTo ErrPart
        'End If
        'If pStaus = "0" Then
        'pError = JsonTest.Item("ErrorMessage") ''JsonTest.Item("errors").Item(1).Item("description") & "," & JsonTest.Item("errors").Item(1).Item("message")    ''Item("items").Item(1).Item("url")
        'MsgInformation(pError)
        'WebRequestGenerateIRN = False
        'http = Nothing
        'Exit Function
        'End If
        'WebRequestGenerateIRN = True
        'http = Nothing
        ''    Set httpGen = Nothing
        'Exit Function
        'ErrPart: 
        ''    Resume
        'WebRequestGenerateIRN = False
        'http = Nothing
        'MsgBox(Err.Description)
        'PubDBCn.RollbackTrans()
    End Function
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdModify.Click
        On Error GoTo ModifyErr
        If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Cancelled Bill Cann't be Modified")
            Exit Sub
        End If

        If PubUserID <> "G0416" Then
            If Trim(txtIRNNo.Text) <> "" Then
                MsgInformation("IRN No Made against this Voucher So cann't be Modified.")
                Exit Sub
            End If
        End If


        If chkFinalPost.CheckState = System.Windows.Forms.CheckState.Checked And mIsAuthorisedUser = False Then
            MsgInformation("Final Bill Post Cann't be Modified")
            Exit Sub
        End If

        If cmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsPurchMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
            SprdExp.Enabled = True
            txtMRRNo.Enabled = False
            CmdSearchMRR.Enabled = False
            txtMRRDate.Enabled = False
            txtVNo.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A" Or mIsAuthorisedUser = True, True, False)
            txtModvatNo.Enabled = False
            txtServNo.Enabled = False
            pShowCalc = True
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdPostingHead_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPostingHead.Click
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntRow As Integer
        FraPostingDtl.Visible = Not FraPostingDtl.Visible
        If FraPostingDtl.Visible = True Then
            FraPostingDtl.BringToFront()
            MainClass.ClearGrid(SprdPostingDetail)
            SqlStr = " SELECT ACM.SUPP_CUST_NAME, " & vbCrLf & " ABS(SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))) AS AMOUNT, " & vbCrLf & " CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))<=0 THEN 'Cr' ELSE 'Dr' END AS DC "
            SqlStr = SqlStr & vbCrLf & " FROM FIN_POSTED_TRN TRN , FIN_SUPP_CUST_MST ACM " & vbCrLf & " WHERE  " & vbCrLf & " TRN.Company_Code=ACM.Company_Code " & vbCrLf & " AND TRN.ACCOUNTCODE=ACM.SUPP_CUST_CODE "
            SqlStr = SqlStr & vbCrLf & " AND TRN.Company_Code = " & RsCompany.Fields("Company_Code").Value & "" & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND BOOKTYPE ='" & mBookType & "'" & vbCrLf & " AND BOOKSUBTYPE = '" & mBookSubType & "'" & vbCrLf & " AND TRN.MKEY='" & LblMKey.Text & "'"
            SqlStr = SqlStr & vbCrLf & " GROUP BY ACM.SUPP_CUST_NAME"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            cntRow = 1
            If RsTemp.EOF = False Then
                Do While RsTemp.EOF = False
                    SprdPostingDetail.Row = cntRow
                    SprdPostingDetail.Col = 1
                    SprdPostingDetail.Text = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
                    SprdPostingDetail.Col = 2
                    SprdPostingDetail.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("Amount").Value), "0.00", RsTemp.Fields("Amount").Value), "0.00")
                    SprdPostingDetail.Col = 3
                    SprdPostingDetail.Text = IIf(IsDBNull(RsTemp.Fields("DC").Value), "", RsTemp.Fields("DC").Value)
                    RsTemp.MoveNext()
                    If RsTemp.EOF = False Then
                        cntRow = cntRow + 1
                        SprdPostingDetail.MaxRows = cntRow
                    End If
                Loop
            End If
            FraPostingDtl.BringToFront()
            Call FormatSprdPostingDetail(-1)
        End If
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnPurchase(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnPurchase(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportOnPurchase(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim mTitle As String
        Dim mSubTitle As String
        Dim SqlStr As String = ""
        Dim mRptFileName As String
        Dim mVNo As String
        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)
        SqlStr = ""
        Call SelectQryForVoucher(SqlStr)
        If lblPurchaseSeqType.Text = "2" Then
            mTitle = "Credit Note (Sale Rejection)"
            mRptFileName = "SaleReturn.rpt"
            mSubTitle = ""
            Dim mPDFPrint As Boolean = False
            Dim mPrePrint As String = ""
            Dim mInvoicePrintType As String = ""
            Call ShowExcisePDFReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName, True, mInvoicePrintType, mPDFPrint, mPrePrint)
        Else
            mTitle = "Purchase Voucher"
            mRptFileName = "PurchaseGST.rpt"
            mSubTitle = ""
            Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName, "N")
        End If



        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub ShowExcisePDFReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String, ByRef IsSubReport As Boolean, ByRef mInvoicePrintType As String, ByVal mPDF As Boolean, mPrePrint As String)

        On Error GoTo ErrPart
        Dim RsTemp As New ADODB.Recordset
        Dim RS As New ADODB.Recordset

        Dim fPath As String
        Dim mBillNoStr As String

        Dim mAmountInword As String
        Dim SqlStrSub As String
        Dim mDutyInword As String
        Dim SqlStr As String = ""

        Dim RsTempShip As ADODB.Recordset = Nothing
        Dim mNetAmount As Double
        Dim mNetDuty As Double
        Dim mPrepTime As String = ""
        Dim mRemovalTime As String = ""
        Dim mManuAVInWord As String
        Dim mManuCessInWord As String
        Dim mManuEDInWord As String
        Dim mManuHCessInWord As String
        Dim mDealerDetail As String
        Dim mDealerAddress As String
        Dim mManuAddress As String
        Dim mSO As Double
        Dim mPayTerms As String
        Dim mBalPayTerms As String
        Dim mJurisdiction As String
        Dim mShipToSameParty As String
        Dim mShipToCode As String

        Dim mShipToName As String = ""
        Dim mShipToAddress As String = ""
        Dim mShipToCity As String = ""
        Dim mShipToGSTN As String = ""
        Dim mCompanyDetail As String = ""
        Dim mCompanyeMail As String = ""
        Dim mCompanyWebSite As String = ""
        Dim mShipToState As String = ""
        Dim mShipToStateCode As String = ""
        Dim mStateName As String = ""
        Dim mStateCode As String = ""
        Dim mWithInState As String = ""
        Dim mWithInCountry As String = ""
        Dim mPlaceofSupply As String = ""
        Dim mExpHeading As String
        Dim mLUT As String
        Dim mCustomerCode As String
        Dim pBarCodeString As String

        Dim mShipFromOtherThan As String
        Dim mShipFromCode As String
        Dim mShipFromName As String
        Dim mShipFromAddress As String
        Dim mShipFromCity As String
        Dim mShipFromState As String
        Dim mShipFromStateCode As String
        Dim mShipFromGSTN As String
        Dim mExWork As String
        Dim path As String
        Dim mCurrency As String
        Dim mRateTitle As String
        Dim mAmountTitle As String
        Dim mShipLocation As String
        Dim mHour As String = ""
        Dim mMin As String = ""
        Dim mShipToPAN As String = ""
        Dim mShipToPhoneNo As String
        Dim mShipToMailID As String

        Dim CrReport As New CrystalDecisions.CrystalReports.Engine.ReportDocument  ' Report Name 
        Dim CrExportOptions As ExportOptions
        Dim CrDiskFileDestinationOptions As New DiskFileDestinationOptions

        Dim CrFormatTypeOptions As New PdfRtfWordFormatOptions

        Dim mEPCGNo As String
        Dim mEPCGDate As String
        Dim mShipToPIN As String

        mRptFileName = PubReportFolderPath & mRptFileName
        CrReport.Load(mRptFileName)


        SqlStrSub = " SELECT * " & vbCrLf _
            & " FROM FIN_PURCHASE_EXP, FIN_PURCHASE_HDR, FIN_INTERFACE_MST " & vbCrLf _
            & " WHERE FIN_PURCHASE_EXP.MKEY = FIN_PURCHASE_HDR.MKEY " & vbCrLf _
            & " AND FIN_PURCHASE_EXP.EXPCODE = FIN_INTERFACE_MST.CODE" & vbCrLf _
            & " AND FIN_PURCHASE_HDR.COMPANY_CODE = FIN_INTERFACE_MST.COMPANY_CODE" & vbCrLf _
            & " And FIN_PURCHASE_EXP.MKEY='" & MainClass.AllowSingleQuote(LblMKey.Text) & "'" & vbCrLf _
            & " AND FIN_INTERFACE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AMOUNT<>0"

        SqlStrSub = SqlStrSub & vbCrLf & " AND GST_ENABLED='Y'"

        SqlStrSub = SqlStrSub & vbCrLf & " ORDER BY SUBROWNO"

        Call Connect_MainReport_To_Database_11(CrReport)        '', mSqlStr
        Call Connect_SubReport_To_Database_11(CrReport, "PurExp")      '

        CrReport.RecordSelectionFormula = "{IH.COMPANY_CODE} = " & RsCompany.Fields("COMPANY_CODE").Value & " AND {IH.MKEY} = '" & MainClass.AllowSingleQuote(LblMKey.Text) & "' AND {IH.FYEAR} = '" & RsCompany.Fields("FYEAR").Value & "'"

        ''And IH.BOOKTYPE='" & mBookType & "' AND IH.BOOKSUBTYPE='" & mBookSubType & "' AND IH.ISFINALPOST='Y' AND IH.BILL_TO_LOC_ID='" & txtBillTo.Text & "'"


        ClearCRpt11Formulas(CrReport)
        CrReport.ReportOptions.EnableSaveDataWithReport = False
        SetCompanyReport11(CrReport, 1, mTitle, mSubTitle)
        'CrReport.VerifyDatabase()   .VerifyOnEveryPrint = True '' blnVerifyOnEveryPrint		
        CrReport.Refresh()

        mStateName = ""
        mStateCode = ""

        If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCustomerCode = MasterNo
        End If

        mStateName = GetPartyBusinessDetail(Trim(mCustomerCode), Trim(txtBillTo.Text), "SUPP_CUST_STATE")
        mStateCode = GetStateCode(mStateName)

        mWithInState = GetPartyBusinessDetail(Trim(mCustomerCode), Trim(txtBillTo.Text), "WITHIN_STATE")


        If mWithInState = "N" Then
            If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "WITHIN_COUNTRY", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mWithInCountry = MasterNo
            End If
        End If

        mPlaceofSupply = VB6.Format(mStateCode, "00") & "-" & mStateName ''IIf(mWithInState = "Y", "INTRA STATE", IIf(mWithInCountry = "Y", "INTER STATE", "EXPORT"))							



        SqlStr = " SELECT * " & vbCrLf _
            & " FROM FIN_PURCHASE_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND MKEY='" & MainClass.AllowSingleQuote(LblMKey.Text) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mNetAmount = IIf(IsDBNull(RsTemp.Fields("NETVALUE").Value), 0, RsTemp.Fields("NETVALUE").Value)
            mNetDuty = IIf(IsDBNull(RsTemp.Fields("TOTCGST_AMOUNT").Value), 0, RsTemp.Fields("TOTCGST_AMOUNT").Value)
            mNetDuty = mNetDuty + IIf(IsDBNull(RsTemp.Fields("TOTSGST_AMOUNT").Value), 0, RsTemp.Fields("TOTSGST_AMOUNT").Value)
            mNetDuty = mNetDuty + IIf(IsDBNull(RsTemp.Fields("TOTIGST_AMOUNT").Value), 0, RsTemp.Fields("TOTIGST_AMOUNT").Value)


            mShipToSameParty = IIf(IsDBNull(RsTemp.Fields("SHIPPED_TO_SAMEPARTY").Value), "Y", RsTemp.Fields("SHIPPED_TO_SAMEPARTY").Value)
            mShipToCode = IIf(IsDBNull(RsTemp.Fields("SHIPPED_TO_PARTY_CODE").Value), "", RsTemp.Fields("SHIPPED_TO_PARTY_CODE").Value)
            mExWork = "N"   ''IIf(IsDBNull(RsTemp.Fields("IS_SHIPPTO_EX_WORK").Value), "N", RsTemp.Fields("IS_SHIPPTO_EX_WORK").Value)

            mPrepTime = "" ''VB6.Format(IIf(IsDBNull(RsTemp.Fields("INV_PREP_TIME").Value), "", RsTemp.Fields("INV_PREP_TIME").Value), "HH:MM")
            mRemovalTime = "" ' VB6.Format(IIf(IsDBNull(RsTemp.Fields("REMOVAL_TIME").Value), "", RsTemp.Fields("REMOVAL_TIME").Value), "HH:MM")
            mSO = -1 'IIf(IsDBNull(RsTemp.Fields("OUR_AUTO_KEY_SO").Value), "", RsTemp.Fields("OUR_AUTO_KEY_SO").Value)

            mHour = "" ' HoursInText(VB.Left(mRemovalTime, 2))
            mMin = "" 'MinInText(VB.Right(mRemovalTime, 2))

            mHour = "" ' mHour & " " & mMin

            If mExWork = "Y" Then ''mShipToSameParty						
                mShipToName = "Ex Work"
                mShipToAddress = ""
                mShipToCity = ""
                mShipToGSTN = ""
                mShipToState = ""
                mShipToStateCode = ""
            Else
                If mShipToSameParty = "Y" Then
                    mShipToCode = mCustomerCode
                    mShipLocation = Trim(txtBillTo.Text)
                Else
                    mShipToCode = IIf(IsDBNull(RsTemp.Fields("SHIPPED_TO_PARTY_CODE").Value), "", RsTemp.Fields("SHIPPED_TO_PARTY_CODE").Value)
                    mShipLocation = Trim(TxtShipTo.Text)
                End If

                SqlStr = "SELECT A.*, B.SUPP_CUST_NAME,PAN_NO FROM FIN_SUPP_CUST_BUSINESS_MST A, FIN_SUPP_CUST_MST B" & vbCrLf _
                    & " Where A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND A.COMPANY_CODE=B.COMPANY_CODE AND A.SUPP_CUST_CODE=B.SUPP_CUST_CODE" & vbCrLf _
                    & " AND B.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mShipToCode) & "' AND A.LOCATION_ID='" & MainClass.AllowSingleQuote(mShipLocation) & "'"


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

                    mShipToPIN = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_PIN").Value), "", RsTempShip.Fields("SUPP_CUST_PIN").Value)
                    mShipToState = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_STATE").Value), "", RsTempShip.Fields("SUPP_CUST_STATE").Value)
                    mShipToStateCode = GetStateCode(mShipToState)
                    mShipToGSTN = IIf(IsDBNull(RsTempShip.Fields("GST_RGN_NO").Value), "", RsTempShip.Fields("GST_RGN_NO").Value)

                    mShipToPAN = ""

                    If MainClass.ValidateWithMasterTable(mShipToName, "SUPP_CUST_NAME", "PAN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mShipToPAN = MasterNo
                    End If

                    mShipToPhoneNo = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_PHONE").Value), "", RsTempShip.Fields("SUPP_CUST_PHONE").Value) ' , 
                    mShipToMailID = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_MAILID").Value), "", RsTempShip.Fields("SUPP_CUST_MAILID").Value)

                End If
            End If


            mShipFromOtherThan = "N" ' IIf(IsDBNull(RsTemp.Fields("IS_DESP_OTHERTHAN_BILL").Value), "N", RsTemp.Fields("IS_DESP_OTHERTHAN_BILL").Value)
            mShipFromCode = "" '' IIf(IsDBNull(RsTemp.Fields("SHIPPED_FROM_PARTY_CODE").Value), "", RsTemp.Fields("SHIPPED_FROM_PARTY_CODE").Value)

            mShipFromName = ""
            mShipFromAddress = ""
            mShipFromAddress = ""
            mShipFromCity = ""
            mShipFromCity = ""
            mShipFromState = ""
            mShipFromStateCode = ""
            mShipFromGSTN = ""

            If mShipFromOtherThan = "Y" Then
                SqlStr = " SELECT SUPP_CUST_NAME, SUPP_CUST_ADDR, SUPP_CUST_CITY, SUPP_CUST_STATE,  " & vbCrLf & " SUPP_CUST_PIN, GST_RGN_NO" & vbCrLf & " FROM FIN_SUPP_CUST_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mShipFromCode) & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempShip, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTempShip.EOF = False Then
                    mShipFromName = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_NAME").Value), "", RsTempShip.Fields("SUPP_CUST_NAME").Value)
                    mShipFromAddress = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_ADDR").Value), "", RsTempShip.Fields("SUPP_CUST_ADDR").Value)
                    mShipFromAddress = Replace(mShipFromAddress, vbCrLf, "")


                    mShipFromCity = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_CITY").Value), "", RsTempShip.Fields("SUPP_CUST_CITY").Value)
                    mShipFromCity = mShipFromCity & " " & IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_PIN").Value), "", RsTempShip.Fields("SUPP_CUST_PIN").Value)

                    mShipFromState = IIf(IsDBNull(RsTempShip.Fields("SUPP_CUST_STATE").Value), "", RsTempShip.Fields("SUPP_CUST_STATE").Value)
                    mShipFromStateCode = GetStateCode(mShipFromState)
                    mShipFromGSTN = IIf(IsDBNull(RsTempShip.Fields("GST_RGN_NO").Value), "", RsTempShip.Fields("GST_RGN_NO").Value)

                    ''Despatch From ...				


                    AssignCRpt11Formulas(CrReport, "ShipFromName", "'" & mShipFromName & "'")
                    AssignCRpt11Formulas(CrReport, "ShipFromAddress", "'" & mShipFromAddress & "'")
                    AssignCRpt11Formulas(CrReport, "ShipFromCity", "'" & mShipFromCity & "'")
                    AssignCRpt11Formulas(CrReport, "ShipFromState", "'" & mShipFromState & "'")

                End If
            End If

        End If

        AssignCRpt11Formulas(CrReport, "InvoicePrintType", "'" & mInvoicePrintType & "'")
        AssignCRpt11Formulas(CrReport, "CompanyCity", "'" & IIf(IsDBNull(RsCompany.Fields("COMPANY_CITY").Value), "", RsCompany.Fields("COMPANY_CITY").Value) & "'")
        AssignCRpt11Formulas(CrReport, "CompanyGSTIN", "'" & IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value) & "'")

        ''-------------
        AssignCRpt11Formulas(CrReport, "CompanyAddressNew", "'" & IIf(IsDBNull(RsCompany.Fields("COMPANY_ADDR").Value), "", RsCompany.Fields("COMPANY_ADDR").Value) & "'")
        AssignCRpt11Formulas(CrReport, "CompanyPin", "'" & IIf(IsDBNull(RsCompany.Fields("COMPANY_PIN").Value), "", RsCompany.Fields("COMPANY_PIN").Value) & "'")
        AssignCRpt11Formulas(CrReport, "CompanyState", "'" & IIf(IsDBNull(RsCompany.Fields("COMPANY_STATE").Value), "", RsCompany.Fields("COMPANY_STATE").Value) & "'")
        AssignCRpt11Formulas(CrReport, "CompanyPhone", "'" & IIf(IsDBNull(RsCompany.Fields("COMPANY_PHONE").Value), "", RsCompany.Fields("COMPANY_PHONE").Value) & "'")
        AssignCRpt11Formulas(CrReport, "CompanyFax", "'" & IIf(IsDBNull(RsCompany.Fields("COMPANY_FAXNO").Value), "", RsCompany.Fields("COMPANY_FAXNO").Value) & "'")
        AssignCRpt11Formulas(CrReport, "CompanyEmail", "'" & IIf(IsDBNull(RsCompany.Fields("COMPANY_MAILID").Value), "", RsCompany.Fields("COMPANY_MAILID").Value) & "'")
        AssignCRpt11Formulas(CrReport, "CompanyWeb", "'" & IIf(IsDBNull(RsCompany.Fields("WEBSITE").Value), "", RsCompany.Fields("WEBSITE").Value) & "'")
        AssignCRpt11Formulas(CrReport, "CompanyPAN", "'" & IIf(IsDBNull(RsCompany.Fields("PAN_NO").Value), "", RsCompany.Fields("PAN_NO").Value) & "'")
        Dim mCompanyStateCode As String = GetStateCode(IIf(IsDBNull(RsCompany.Fields("COMPANY_STATE").Value), "", RsCompany.Fields("COMPANY_STATE").Value) & "")
        AssignCRpt11Formulas(CrReport, "CompanyStateCode", "'" & mCompanyStateCode & "'")
        ''---------------
        mJurisdiction = "All Disputes Subject to " & IIf(IsDBNull(RsCompany.Fields("JURISDICTION").Value), "", RsCompany.Fields("JURISDICTION").Value) & " Jurisdiction."

        AssignCRpt11Formulas(CrReport, "COMPANYTINNo", "'" & IIf(IsDBNull(RsCompany.Fields("TINNO").Value), "", RsCompany.Fields("TINNO").Value) & "'")
        AssignCRpt11Formulas(CrReport, "COMPANYCINNo", "'" & IIf(IsDBNull(RsCompany.Fields("CIN_NO").Value), "", RsCompany.Fields("CIN_NO").Value) & "'")

        mCompanyeMail = IIf(IsDBNull(RsCompany.Fields("COMPANY_MAILID").Value), "", "e-mail : " & RsCompany.Fields("COMPANY_MAILID").Value)
        mCompanyWebSite = IIf(IsDBNull(RsCompany.Fields("WEBSITE").Value), "", "WebSite : " & RsCompany.Fields("WEBSITE").Value)
        mCompanyDetail = mCompanyeMail & ", " & mCompanyWebSite

        AssignCRpt11Formulas(CrReport, "COMPANYDETAIL", "'" & mCompanyDetail & "'")
        AssignCRpt11Formulas(CrReport, "PrepTime", "'" & mPrepTime & "'")
        AssignCRpt11Formulas(CrReport, "RemovalTime", "'" & mRemovalTime & "'")
        AssignCRpt11Formulas(CrReport, "RemovalTimeInWord", "'" & mHour & "'")
        AssignCRpt11Formulas(CrReport, "ShipToPAN", "'" & mShipToPAN & "'")



        AssignCRpt11Formulas(CrReport, "Jurisdiction", "'" & mJurisdiction & "'")
        AssignCRpt11Formulas(CrReport, "mShipToName", "'" & mShipToName & "'")
        AssignCRpt11Formulas(CrReport, "mShipToAddress", "'" & mShipToAddress & "'")
        AssignCRpt11Formulas(CrReport, "mShipToCity", "'" & mShipToCity & "'")
        AssignCRpt11Formulas(CrReport, "mShipToGSTN", "'" & mShipToGSTN & "'")
        AssignCRpt11Formulas(CrReport, "mShipToState", "'" & mShipToState & "'")
        AssignCRpt11Formulas(CrReport, "mShipToStateCode", "'" & mShipToStateCode & "'")
        AssignCRpt11Formulas(CrReport, "mStateName", "'" & mStateName & "'")
        AssignCRpt11Formulas(CrReport, "mStateCode", "'" & mStateCode & "'")
        AssignCRpt11Formulas(CrReport, "mPlaceofSupply", "'" & mPlaceofSupply & "'")
        AssignCRpt11Formulas(CrReport, "mServiceName", "'" & Trim(txtServProvided.Text) & "'")

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
            AssignCRpt11Formulas(CrReport, "mShipToPIN", "'" & mShipToPIN & "'")
            AssignCRpt11Formulas(CrReport, "mShipToPhoneNo", "'" & mShipToPhoneNo & "'")
            AssignCRpt11Formulas(CrReport, "mShipToMailID", "'" & mShipToMailID & "'")

            If UCase(Mid(mRptFileName, Len(mRptFileName) - 6)) = "_A3.RPT" Then
                AssignCRpt11Formulas(CrReport, "PrePrint", "'" & mPrePrint & "'")
            End If
        End If


        'mPayTerms = ""

        If IsSubReport = True Then

            If mNetAmount = 0 Then
                mAmountInword = " Zero Only"
            Else
                mAmountInword = MainClass.RupeesConversion(mNetAmount) & " Only"
            End If

            mDutyInword = MainClass.RupeesConversion(mNetDuty) & " Only"

            If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
                AssignCRpt11Formulas(CrReport, "AmountInWord", "'Rs. Zero'")
                AssignCRpt11Formulas(CrReport, "DutyInword", "'Rs. Zero'")
                AssignCRpt11Formulas(CrReport, "NetAmount", "'0.00'")
            Else
                AssignCRpt11Formulas(CrReport, "AmountInWord", "'" & mAmountInword & "'")
                AssignCRpt11Formulas(CrReport, "NetAmount", "'" & VB6.Format(mNetAmount, "0.00") & "'")
                AssignCRpt11Formulas(CrReport, "DutyInword", "'" & mDutyInword & "'")
            End If
        End If


        Dim mBMPFileName As String = ""
        mBillNoStr = Trim(txtCReditNoteNo.Text)
        mBillNoStr = Replace(mBillNoStr, "/", "_")
        mBillNoStr = Replace(mBillNoStr, "\", "_")
        mBMPFileName = RefreshQRCode(LblMKey.Text, mBillNoStr, txtIRNNo.Text)

        If Not FILEExists(mBMPFileName) Then
            mBMPFileName = ""
        End If

        AssignCRpt11Formulas(CrReport, "PicLocation", "'" & mBMPFileName & "'")

        Dim mClientLogoPath As String = ""
        If Not FILEExists(PubClientLogoPath) Then
            mClientLogoPath = ""
        Else
            mClientLogoPath = PubClientLogoPath
        End If
        AssignCRpt11Formulas(CrReport, "CompanyLogo", "'" & mClientLogoPath & "'")

        If mPDF = True Then
            Dim pOutPutFileName As String = ""
            mBillNoStr = Trim(txtCReditNoteNo.Text)
            mBillNoStr = Replace(mBillNoStr, "/", "_")
            mBillNoStr = Replace(mBillNoStr, "\", "_")

            fPath = mPubBarCodePath & "\TaxInvoice_" & RsCompany.Fields("COMPANY_CODE").Value & mBillNoStr & ".pdf"
            pOutPutFileName = mPubBarCodePath & "\TaxInvoice_DS_" & RsCompany.Fields("COMPANY_CODE").Value & mBillNoStr & ".pdf"

            'FrmInvoiceViewer.CrystalReportViewer1.ReportSource = Nothing
            'FrmInvoiceViewer.CrystalReportViewer1.ReportSource = CrReport
            'FrmInvoiceViewer.CrystalReportViewer1.Show()

            CrDiskFileDestinationOptions.DiskFileName = fPath
            CrExportOptions = CrReport.ExportOptions

            With CrExportOptions
                .ExportDestinationType = ExportDestinationType.DiskFile
                .ExportFormatType = ExportFormatType.PortableDocFormat
                .DestinationOptions = CrDiskFileDestinationOptions
                .FormatOptions = CrFormatTypeOptions
            End With
            CrReport.Export()

            If FILEExists(fPath) Then
                If frmPrintInvCopy.optShow(1).Checked = True Then
                    Process.Start("explorer.exe", fPath)
                End If
            End If

            If frmPrintInvCopy.optShow(2).Checked = True Then

                ''My test

                'Dim mSignerName As String
                Dim mPrintDigitalSign As String
                mPrintDigitalSign = "Authorised Signatory" ''"For " & RsCompany.Fields("PRINT_COMPANY_NAME").Value  ''"Authorised Signatory"
                'mSignerName = GetDigitalSignName(PubUserID)
                'If mSignerName <> "" Then
                If SignPdf(fPath, pOutPutFileName, mPrintDigitalSign) = False Then Exit Sub

                If FILEExists(pOutPutFileName) Then
                    Process.Start("explorer.exe", pOutPutFileName)
                End If
                'End If
            End If
        Else
            If mMode = Crystal.DestinationConstants.crptToWindow Then
                FrmInvoiceViewer.CrystalReportViewer1.ReportSource = Nothing
                'FrmInvoiceViewer.CrystalReportViewer1.DataBindings()
                FrmInvoiceViewer.CrystalReportViewer1.ReportSource = CrReport
                FrmInvoiceViewer.CrystalReportViewer1.Show()
                FrmInvoiceViewer.MdiParent = Me.MdiParent
                FrmInvoiceViewer.CrystalReportViewer1.ShowGroupTreeButton = False
                FrmInvoiceViewer.CrystalReportViewer1.DisplayGroupTree = False
                FrmInvoiceViewer.Dock = DockStyle.Fill
                FrmInvoiceViewer.Show()
            Else

                'CrReport.PrintToPrinter(1, False, 1, 99)

                'For Each prt In PrinterSettings.InstalledPrinters       ''Printers
                '    If UCase(prt) = UCase("Universal Printer") Then
                '        CrReport.PrintOptions.PrinterName = prt.DeviceName
                '        Exit For
                '    End If
                'Next
                Dim settings As PrinterSettings = New PrinterSettings()
                For Each printer As String In PrinterSettings.InstalledPrinters

                    If settings.IsDefaultPrinter Then
                        settings.PrinterName = printer
                        Exit For
                    End If
                Next

                CrReport.PrintToPrinter(1, False, 1, 99)
                CrReport.Dispose()
            End If
        End If


        Exit Sub
ErrPart:
        'Resume		
        CrReport.Dispose()
        MsgBox(Err.Description)
    End Sub
    Private Function SelectQryForVoucher(ByRef mSqlStr As String) As String
        ''SELECT CLAUSE...
        mSqlStr = " SELECT " & vbCrLf & " IH.*, ID.*, CMST.SUPP_CUST_NAME "
        ''FROM CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID, FIN_SUPP_CUST_MST CMST , FIN_SUPP_CUST_BUSINESS_MST BMST, GEN_COMPANY_MST GMST" ' & vbCrLf |
        ''WHERE CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " WHERE IH.COMPANY_CODE=GMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.MKEY=ID.MKEY " & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND IH.COMPANY_CODE=BMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=BMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND IH.BILL_TO_LOC_ID=BMST.LOCATION_ID" & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND IH.MKEY='" & LblMKey.Text & "'" & vbCrLf _
            & " AND IH.BOOKTYPE='" & mBookType & "'" & vbCrLf _
            & " AND IH.BOOKSUBTYPE='" & mBookSubType & "'" & vbCrLf _
            & " AND IH.ISFINALPOST='Y' AND IH.BILL_TO_LOC_ID='" & txtBillTo.Text & "'"
        ''ORDER CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & "ORDER BY ID.SUBROWNO"
        SelectQryForVoucher = mSqlStr
    End Function
    Private Sub cmdQRCode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdQRCode.Click
        'On Error GoTo ErrPart
        'Dim url As String
        'Dim mGSTIN As String
        'Dim mIrn As String
        'Dim mGetQRImg As String
        'Dim mGetSignedInvoice As String
        'Dim mCDKey As String
        'Dim mEInvUserName As String
        'Dim mEInvPassword As String
        'Dim mEFUserName As String
        'Dim mEFPassword As String
        'Dim mBody As String
        'Dim mResponseId As String
        'Dim mResponseIdStr As String
        'Dim url1 As String
        'Dim WebRequestGen As String
        'Dim pStaus As String
        'Dim mIRNNo As String
        'Dim mSignedInvoice As String
        'Dim mSignedQRCode As String
        'Dim pError As String
        ''Dim pBranchId As String
        ''Dim pTokenId As String
        ''Dim pUserId As String
        'Dim mBMPFileName As String
        'Dim mFilePath As String
        'Dim pResponseText As String
        'If Trim(txtIRNNo.Text) = "" Then Exit Sub
        'If GeteInvoiceSetupContents(url, "P", mCDKey, mEFUserName, mEFPassword, mEInvUserName, mEInvPassword) = False Then GoTo ErrPart
        ''    url = "http://EinvSandbox.webtel.in/v1.0/GetEInvoiceByIRN"
        ''    mCDKey = "1000687"
        ''    mEInvUserName = "06AAACW3775F013"
        ''    mEInvPassword = "Admin!23"
        ''    mEFUserName = "29AAACW3775F000"
        ''    mEFPassword = "Admin!23.."
        '        '22/10/2021 Dim http As MSXML2.XMLHTTP60 '' MSXML.xmlhttp
        '        '22/10/2021 http = CreateObject("MSXML2.ServerXMLHTTP")
        'mGSTIN = IIf(IsDbNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
        'mIRNNo = Trim(txtIRNNo.Text)
        ''    mGetQRImg = "0"      ''0 for text , 1 for Image
        ''    mGetSignedInvoice = "0"  ''1 - Signed Json of Invoice will be return, 0 - will not return signed Invoice.
        'http.Open("POST", url, False)
        'http.setRequestHeader("Content-Type", "application/json")
        'With JB
        '.Clear()
        '.IsArray_Renamed = False 'Actually the default after Clear.
        '.Item("Irn") = mIRNNo
        '.Item("GSTIN") = mGSTIN
        '.Item("CDKey") = mCDKey
        '.Item("EInvUserName") = mEInvUserName
        '.Item("EInvPassword") = mEInvPassword
        '.Item("EFUserName") = mEFUserName
        '.Item("EFPassword") = mEFPassword
        'mBody = .JSON
        'End With
        'http.Send(mBody)
        'pResponseText = http.responseText
        ''    pResponseText = Replace(pResponseText, "\", "")
        'pResponseText = Replace(pResponseText, "[", "")
        'pResponseText = Replace(pResponseText, "]", "")
        ''    pResponseText = Mid(pResponseText, 2, Len(pResponseText) - 2)
        'Dim JsonTest As Object
        'JsonTest = JSON.parse(pResponseText)
        'pStaus = JsonTest.Item("Status")
        'If pStaus = "1" Then
        'mFilePath = JsonTest.Item("File") ''http.responseText
        '            'If mFilePath <> "" Then
        '            'ShellExecute(Me.Handle.ToInt32, "open", mFilePath, vbNullString, vbNullString, SW_SHOWNORMAL)
        '            'End If
        'End If
        'If pStaus = "0" Then
        'pError = JsonTest.Item("ErrorMessage") ''JsonTest.Item("errors").Item(1).Item("description") & "," & JsonTest.Item("errors").Item(1).Item("message")    ''Item("items").Item(1).Item("url")
        'MsgInformation(pError)
        'http = Nothing
        'Exit Sub
        'End If
        'http = Nothing
        ''    Set httpGen = Nothing
        'Exit Sub
        'ErrPart: 
        ''    Resume
        'http = Nothing
        'MsgBox(Err.Description)
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If RsCompany.Fields("PURCHASE_POSTINGTYPE").Value = "B" Then
            txtDebitAccount.Text = GetDebitNameOfInvType(Trim(cboInvType.Text), "Y")
        End If
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        Call CalcTots()
        pDnCnNo = ""
        If UpdateMain1() = True Then
            ADDMode = False
            MODIFYMode = False
            ''TxtVNo_Validate False
            If CDbl(LblBookCode.Text) = ConModvatBookCode Then
                '            TxtMODVATNo_Validate False
            ElseIf CDbl(LblBookCode.Text) = ConPurchaseBookCode Then
                txtVNo_Validating(txtVNo, New System.ComponentModel.CancelEventArgs(False))
            End If
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
    Private Sub CmdSearchMRR_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearchMRR.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "SELECT IH.AUTO_KEY_MRR, IH.MRR_DATE, IH.BILL_NO, IH.BILL_DATE, IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, IH.VEHICLE" & vbCrLf _
            & " FROM INV_GATE_HDR IH, FIN_SUPP_CUST_MST CMST" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND MRR_FINAL_FLAG='N' " & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE"

        If lblPurchaseType.Text = "J" Or lblPurchaseType.Text = "R" Then
            SqlStr = SqlStr & vbCrLf & " AND IH.REF_TYPE='R'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND IH.REF_TYPE<>'R'"
        End If
        If lblPurchaseSeqType.Text = "9" Then
            SqlStr = SqlStr & vbCrLf & " AND IH.UNDER_CHALLAN='Y'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND IH.UNDER_CHALLAN='N'"
        End If
        If chkRejection.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " AND IH.REF_TYPE IN ('I','1','2','3')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND IH.REF_TYPE NOT IN ('I','1','2','3')"
        End If
        If CDbl(LblBookCode.Text) = ConModvatBookCode Then
            SqlStr = SqlStr & vbCrLf & " AND IH.GST_STATUS='N'"
        ElseIf CDbl(LblBookCode.Text) = ConPurchaseBookCode Then
            SqlStr = SqlStr & vbCrLf & " AND IH.SEND_AC_FLAG='Y'"
        End If

        If MainClass.SearchGridMasterBySQL2((txtMRRNo.Text), SqlStr) = True Then  ''If MainClass.SearchGridMaster((txtMRRNo.Text), "INV_GATE_HDR", "AUTO_KEY_MRR", "MRR_DATE", , , SqlStr) = True Then
            txtMRRNo.Text = AcName
            txtMRRNo_Validating(txtMRRNo, New System.ComponentModel.CancelEventArgs(False))
            If cboInvType.Enabled = True Then cboInvType.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdResetMRR_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdResetMRR.Click
        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        If Trim(txtMRRNo.Text) = "" Then Exit Sub
        SqlStr = " SELECT * FROM INV_GATE_HDR " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & "" ''& vbCrLf |              & " And SUBSTR(AUTO_KEY_MRR, LENGTH(AUTO_KEY_MRR) - 5, 4) = " & RsCompany.fields("FYEAR").value & ""
        If lblPurchaseType.Text = "J" Or lblPurchaseType.Text = "R" Then
            SqlStr = SqlStr & vbCrLf & " AND REF_TYPE='R'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND REF_TYPE<>'R'"
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            If ResetMRRMain(RsTemp) = False Then
                Exit Sub
            End If
        Else
            ErrorMsg("Either InValid MRR No. OR Not Send to Account.", "", MsgBoxStyle.Critical)
        End If
        FormatSprdMain(-1)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SprdExp_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdExp.ButtonClicked
        If pShowCalc = True Then '' If FormActive = True Then
            SprdExp.Col = ColExpAmt
            SprdExp.Row = eventArgs.row
            If Val(SprdExp.Text) <> 0 Then
                Call CalcTots()
            End If
        End If
    End Sub
    Private Sub SprdMain_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdMain.ButtonClicked
        Dim mPONo As String
        SprdMain.Row = eventArgs.row
        SprdMain.Col = ColPONo
        mPONo = SprdMain.Text
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportonPO(Crystal.DestinationConstants.crptToWindow, mPONo)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent
        Dim xIName As String = ""
        Dim SqlStr As String = ""
        Dim xHSNCode As String = ""


        If eventArgs.row = 0 And eventArgs.col = ColHSN Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColHSN
                If MainClass.SearchGridMaster(.Text, "GEN_HSN_MST", "HSN_CODE", "HSN_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='" & IIf(VB.Left(lblPurchaseType.Text, 1) = "J", "S", "G") & "' ") = True Then     ''AND CODETYPE='" & iif(VB.Left(lblBookType.Text, 1) = "W" Or VB.Left(lblBookType.Text, 1) = "J" ,'S','G') & "'  'VB.Left(lblBookType.Text, 1) = "W" Or VB.Left(lblBookType.Text, 1) = "J" 
                    .Row = .ActiveRow
                    .Col = ColHSN
                    .Text = AcName
                    xHSNCode = Trim(.Text)

                    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColHSN)
                End If

            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColInvType Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColInvType
                If MainClass.SearchGridMaster(.Text, "FIN_INVTYPE_MST", "NAME", , , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P'") = True Then
                    .Row = .ActiveRow
                    .Col = ColInvType
                    .Text = AcName
                    '                MainClass.SetFocusToCell SprdMain, SprdMain.ActiveRow, ColInvType
                End If
            End With
        End If
        Exit Sub
        If eventArgs.row = 0 And eventArgs.col = ColItemCode Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode
                '            If mainclass.SearchMaster(.Text, "vwITEM", "ITEMCODE", SqlStr) = True Then
                '                .Row = .ActiveRow
                '                .Col = ColItemCode
                '                .Text = AcName
                '            End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            End With
        End If
        'If eventArgs.row = 0 And eventArgs.col = ColItemDesc Then
        '    With SprdMain
        '        .Row = .ActiveRow
        '        .Col = ColItemDesc
        '        xIName = .Text
        '        .Text = ""
        '        '            If mainclass.SearchMaster(.Text, "vwITEM", "Name", SqlStr) = True Then
        '        '                .Row = .ActiveRow
        '        '                .Col = ColItemDesc
        '        '                .Text = AcName
        '        '            Else
        '        '                .Row = .ActiveRow
        '        '                .Col = ColItemDesc
        '        '                .Text = xIName
        '        '            End If
        '        MainClass.ValidateWithMasterTable(.Text, "Name", "ItemCode", "Item", PubDBCn, MasterNo)
        '        .Row = .ActiveRow
        '        .Col = ColItemCode
        '        .Text = MasterNo
        '        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
        '    End With
        'End If


        '    If eventArgs.Col = 0 And eventArgs.Row > 0 Then    '***ROW DEL. OPTION NOT REQ IN INVOICE
        '        SprdMain.Row = Row
        '        SprdMain.Col = ColSONo
        '        If Row < SprdMain.MaxRows And (ADDMode = True Or MODIFYMode = True) Then
        '            mainclass.DeleteSprdRow SprdMain, Row, ColSONo
        '            mainclass.SaveStatus Me, ADDMode, MODIFYMode
        '            FormatSprdMain Row
        ''            Call DistributeExpInMainGrid
        ''            Call CalcTots
        '        End If
        '    End If
        Call CalcTots()
    End Sub
    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell
        On Error GoTo ErrPart
        Dim RIBBONSGroup As Boolean
        Dim xSoNo As String
        Dim xICode As String
        If eventArgs.newRow = -1 Then Exit Sub
        SprdMain.Row = SprdMain.ActiveRow
        Select Case eventArgs.col
            Case ColQty
                If CheckQty() = True Then
                    MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                    FormatSprdMain((SprdMain.MaxRows))
                End If
            Case ColRate
                Call CheckRate()
            Case ColInvType
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColInvType
                If Trim(SprdMain.Text) <> "" Then
                    If MainClass.ValidateWithMasterTable(Trim(SprdMain.Text), "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                        MsgBox("Invoice Name Does Not Exist In Master", MsgBoxStyle.Information)
                        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColInvType)
                        eventArgs.cancel = True
                    End If
                End If
            Case ColHSN
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColItemCode
                xICode = SprdMain.Text
                If xICode = "" Then Exit Sub

                SprdMain.Col = ColHSN
                If SprdMain.Text = "" Then Exit Sub

                If SprdMain.Text <> "" Then
                    If MainClass.ValidateWithMasterTable(SprdMain.Text, "HSN_CODE", "HSN_CODE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ") = False Then            'AND CODETYPE='" & VB.Left(cboInvType.Text, 1) & "'
                        MsgInformation("Invaild HSN CODE.")
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColHSN)
                        Exit Sub
                    End If
                End If
        End Select
        Call CalcTots()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub CheckRate()
        On Error GoTo ERR1
        With SprdMain
            .Row = .ActiveRow
            .Col = ColItemCode
            If Trim(.Text) = "" Then Exit Sub
            .Col = ColRate
            If Val(.Text) <= 0 Then
                MsgInformation("Please Enter the Rate.")
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColRate)
            End If
        End With
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
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
    Private Sub SprdMain_TextTipFetch(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_TextTipFetchEvent) Handles SprdMain.TextTipFetch
        If eventArgs.row = 0 Then Exit Sub
        SprdMain.Row = eventArgs.row
        SprdMain.Col = ColItemDesc
        eventArgs.tipText = SprdMain.Text
        eventArgs.showTip = True
    End Sub
    Private Sub UltraGrid1_DoubleClick(sender As Object, e As EventArgs) Handles UltraGrid1.DoubleClick

        Dim mBillType As String
        Dim mBillNoPrefix As String
        Dim mBillNo As String
        Dim mBillNoSuffix As String
        Dim mRow As UltraGridRow
        Dim mCol As UltraGridColumn
        Dim mVDate As String

        If Me.UltraGrid1.ActiveRow.Index < 0 Then Exit Sub
        mRow = Me.UltraGrid1.Rows(Me.UltraGrid1.ActiveRow.Index)
        mCol = Me.UltraGrid1.DisplayLayout.Bands(0).Columns(1)

        mBillType = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(0))
        mBillNoPrefix = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(1))
        mBillNo = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(2))       ''ultrow.SetCellValue(m_udtColumns.EntryNo, dtRow.Item("EntryNo"))
        mBillNoSuffix = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(3))

        mVDate = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(7))


        cboInvType.Text = Trim(mBillType)

        txtVNoPrefix.Text = mBillNoPrefix

        txtVNo.Text = VB6.Format(mBillNo, "00000")

        txtVNoSuffix.Text = mBillNoSuffix

        txtVDate.Text = VB6.Format(mVDate, "DD/MM/YYYY")
        'txtModvatNo.Text = VB6.Format(.Text, "00000")
        'txtModvatDate.Text = VB6.Format(.Text, "DD/MM/YYYY")
        'ChkCapital.CheckState = IIf(VB.Left(.Text, 1) = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

        If CDbl(LblBookCode.Text) = ConModvatBookCode Then
            '            TxtMODVATNo_Validate False
        ElseIf CDbl(LblBookCode.Text) = ConPurchaseBookCode Then
            txtVNo_Validating(txtVNo, New System.ComponentModel.CancelEventArgs(False))
        End If
        CmdView_Click(CmdView, New System.EventArgs())


    End Sub
    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent)
        'With SprdView
        '    If eventArgs.row = 0 Then Exit Sub
        '    .Row = eventArgs.row
        '    .Col = 1
        '    If Trim(.Text) = "" Then
        '        cboInvType.SelectedIndex = -1
        '    Else
        '        cboInvType.Text = Trim(.Text)
        '    End If
        '    .Col = 2
        '    txtVNoPrefix.Text = .Text
        '    .Col = 3
        '    txtVNo.Text = VB6.Format(.Text, "00000")
        '    .Col = 4
        '    txtVNoSuffix.Text = .Text
        '    .Col = 6
        '    txtVDate.Text = VB6.Format(.Text, "DD/MM/YYYY")
        '    .Col = 7
        '    txtModvatNo.Text = VB6.Format(.Text, "00000")
        '    .Col = 8
        '    txtModvatDate.Text = VB6.Format(.Text, "DD/MM/YYYY")
        '    .Col = 21
        '    ChkCapital.CheckState = IIf(VB.Left(.Text, 1) = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        '    If CDbl(LblBookCode.Text) = ConModvatBookCode Then
        '        '            TxtMODVATNo_Validate False
        '    ElseIf CDbl(LblBookCode.Text) = ConPurchaseBookCode Then
        '        txtVNo_Validating(txtVNo, New System.ComponentModel.CancelEventArgs(False))
        '    End If
        '    CmdView_Click(CmdView, New System.EventArgs())
        'End With
    End Sub
    Private Sub txtAdvAdjust_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAdvAdjust.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcAdvTots()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtAdvBal_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAdvBal.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtAdvBal_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAdvBal.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtAdvAdjust_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAdvAdjust.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtAdvAdjust_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAdvAdjust.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtAdvCGST_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAdvCGST.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtAdvCGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAdvCGST.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtAdvCGST_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAdvCGST.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcAdvTots()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtAdvIGST_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAdvIGST.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcAdvTots()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtAdvSGST_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAdvSGST.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtAdvSGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAdvSGST.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtAdvIGST_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAdvIGST.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtAdvIGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAdvIGST.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtAdvDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAdvDate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtAdvSGST_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAdvSGST.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcAdvTots()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtAdvVNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAdvVNo.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SearchAdvanceVNo()
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mDivisionCode As Double
        Dim xSupplierCode As Double
        Dim mVNo As String
        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If
        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xSupplierCode = MasterNo
        End If
        mVNo = ""
        If Val(CStr(Val(txtVNo.Text))) > 0 Then
            mVNo = Trim(Trim(txtVNoPrefix.Text) & VB6.Format(Val(txtVNo.Text), "00000") & Trim(txtVNoSuffix.Text))
        End If
        SqlStr = " SELECT VNO, VDATE, SUM(NETVALUE) AS NETVALUE FROM ("
        SqlStr = SqlStr & vbCrLf & " SELECT VNO, VDATE, SUM(NETVALUE) AS NETVALUE " & vbCrLf & " FROM FIN_ADVANCE_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & xSupplierCode & "' AND BOOKTYPE='AP'" & vbCrLf & " AND DIV_CODE = " & mDivisionCode & "" & vbCrLf & " AND VDATE <= TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        SqlStr = SqlStr & vbCrLf & " GROUP BY VNO, VDATE"
        SqlStr = SqlStr & vbCrLf & " UNION "
        SqlStr = SqlStr & vbCrLf & " SELECT ADV_VNO AS VNO, ADV_VDATE AS VDATE, SUM(ADV_ADJUSTED_AMT*-1) AS ADV_ADJUSTED_AMT " & vbCrLf & " FROM FIN_PURCHASE_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & xSupplierCode & "'" & vbCrLf & " AND DIV_CODE = " & mDivisionCode & "" & vbCrLf & " AND VDATE <= TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        If mVNo <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND FYEAR || VNO <> " & RsCompany.Fields("FYEAR").Value & " || '" & mVNo & "'"
        End If
        SqlStr = SqlStr & vbCrLf & " GROUP BY ADV_VNO, ADV_VDATE HAVING SUM(ADV_ADJUSTED_AMT)<>0"
        SqlStr = SqlStr & vbCrLf & ") GROUP BY VNO, VDATE HAVING SUM(NETVALUE)>0"
        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtAdvVNo.Text = AcName
            txtAdvVNo_Validating(txtAdvVNo, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub txtAdvVNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAdvVNo.DoubleClick
        Call SearchAdvanceVNo()
    End Sub
    Private Sub txtAdvVNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtAdvVNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchAdvanceVNo()
    End Sub
    Private Sub txtAdvVNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAdvVNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mDivisionCode As Double
        Dim mSupplierCode As String
        If txtAdvVNo.Text = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If
        mSupplierCode = "-1"
        If MainClass.ValidateWithMasterTable(Trim(txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSupplierCode = Trim(MasterNo)
        End If
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & mSupplierCode & "' AND DIV_CODE = " & mDivisionCode & " AND BOOKTYPE='AP'"
        If MainClass.ValidateWithMasterTable((txtAdvVNo.Text), "VNO", "VDATE", "FIN_ADVANCE_HDR", PubDBCn, MasterNo, , SqlStr) = True Then
            txtAdvDate.Text = VB6.Format(MasterNo, "DD/MM/YYYY")
        Else
            MsgInformation("No Such Advance Voucher")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtBEDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBEDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtBEDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtBEDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtBENo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBENo.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtBENo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBENo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtBENo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtBEAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBEAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtBEDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBEDate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtBEAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBEAmount.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtESIAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtESIAmount.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtESIAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtESIAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtESIAmount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtESIAmount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        CalcTots()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtESIDeductOn_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtESIDeductOn.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtESIDeductOn_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtESIDeductOn.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtESIDeductOn_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtESIDeductOn.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtESIDeductOn.Text = VB6.Format(txtESIDeductOn.Text, "0.00")
        CalcTots()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtESIRate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtESIRate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtESIRate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtESIRate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtESIRate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtESIRate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtESIRate.Text = VB6.Format(txtESIRate.Text, "0.00")
        CalcTots()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtItemAdvAdjust_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemAdvAdjust.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtItemAdvAdjust_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItemAdvAdjust.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtItemAdvAdjust_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemAdvAdjust.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcAdvTots()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtModvatDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtModvatDate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtModvatDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtModvatDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtModvatDate.Text) = "" Then
            MsgBox("Modvat Date Cann't be Blank", MsgBoxStyle.Information)
            GoTo EventExitSub
        End If
        If Not IsDate(txtModvatDate.Text) Then
            MsgBox("Invalid Modvat Date", MsgBoxStyle.Information)
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtModvatNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtModvatNo.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtModvatNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtModvatNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub SearchProvidedMaster()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'"
        If MainClass.SearchGridMaster((txtServProvided.Text), "GEN_HSN_MST", "HSN_DESC", "HSN_CODE", "IGST_PER", , SqlStr) = True Then
            txtServProvided.Text = AcName
            txtServProvided_Validating(txtServProvided, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtPaymentDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPaymentdate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtPaymentDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPaymentdate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtPaymentdate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtPaymentdate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtPortCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPortCode.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtPortCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPortCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtServDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtServDate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtServDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtServDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtServDate.Text) = "" Then
            MsgBox("Service Tax Claim Date Cann't be Blank", MsgBoxStyle.Information)
            GoTo EventExitSub
        End If
        If Not IsDate(txtServDate.Text) Then
            MsgBox("Invalid Modvat Date", MsgBoxStyle.Information)
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtServiceOn_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtServiceOn.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtServiceOn_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtServiceOn.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtServiceOn_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtServiceOn.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        CalcTots()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtProviderPer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProviderPer.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtProviderPer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtProviderPer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtRecipientPer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRecipientPer.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtRecipientPer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRecipientPer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtServiceTaxAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtServiceTaxAmount.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtServiceTaxAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtServiceTaxAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtServiceTaxAmount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtServiceTaxAmount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        CalcTots()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtServiceTaxPer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtServiceTaxPer.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtServiceTaxPer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtServiceTaxPer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtServiceTaxPer_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtServiceTaxPer.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        CalcTots()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtServNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtServNo.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtServNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtServNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtServNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtServNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim xMkey As String = ""
        Dim mSERVNo As String
        Dim mCapital As String
        Dim SqlStr As String = ""
        If Val(txtServNo.Text) = 0 Then GoTo EventExitSub
        txtServNo.Text = VB6.Format(Val(txtServNo.Text), "00000")
        mCapital = IIf(ChkCapital.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        If MODIFYMode = True And RsPurchMain.EOF = False Then xMkey = RsPurchMain.Fields("mKey").Value
        mSERVNo = Trim(txtServNo.Text)
        SqlStr = " SELECT * FROM FIN_PURCHASE_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND SERVNO='" & MainClass.AllowSingleQuote(mSERVNo) & "' AND ISPLA='N' AND ISSERVTAX_POST='N'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPurchMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsPurchMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Voucher, Use Generate Voucher Option To add", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM FIN_PURCHASE_HDR " & " WHERE Mkey='" & MainClass.AllowSingleQuote(xMkey) & "' "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPurchMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
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
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mReverseChargeApp As String
        'Dim mReverseChargePer As Double
        'Dim mServiceTaxOn As Double
        Dim mLocal As String
        Dim mHSNCode As String
        Dim pCGSTPer As Double
        Dim pSGSTPer As Double
        Dim pIGSTPer As Double
        Dim cntRow As Integer
        Dim mPartyGSTNo As String
        Dim xAcctCode As String

        If Trim(txtServProvided.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xAcctCode = Trim(MasterNo)
        End If

        mLocal = "N"
        If Trim(txtSupplier.Text) <> "" Then
            'If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            '    mLocal = Trim(MasterNo)
            'End If
            If Trim(txtSupplier.Text) <> "" Then
                mLocal = GetPartyBusinessDetail(Trim(xAcctCode), Trim(txtBillTo.Text), "WITHIN_STATE")
            End If
        End If
        mPartyGSTNo = ""
        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPartyGSTNo = MasterNo
        End If
        txtProviderPer.Text = "0.00"
        txtRecipientPer.Text = "0.00"
        SqlStr = " SELECT HSN_CODE, HSN_DESC, CGST_PER, SGST_PER, IGST_PER" & vbCrLf & " REVERSE_CHARGE_APP, GST_APP" & vbCrLf & " FROM GEN_HSN_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND HSN_DESC='" & MainClass.AllowSingleQuote((txtServProvided.Text)) & "' AND CODETYPE='S'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            mReverseChargeApp = IIf(IsDBNull(RsTemp.Fields("REVERSE_CHARGE_APP").Value), "N", RsTemp.Fields("REVERSE_CHARGE_APP").Value)
            mHSNCode = IIf(IsDBNull(RsTemp.Fields("HSN_CODE").Value), "", RsTemp.Fields("HSN_CODE").Value)
            pCGSTPer = 0
            pSGSTPer = 0
            pIGSTPer = 0
            If lblPurchaseType.Text <> "G" Then
                If GetSACDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, mPartyGSTNo, VB.Left(cboGSTStatus.Text, 1)) = False Then GoTo ERR1
                For cntRow = 1 To SprdMain.MaxRows - 1
                    SprdMain.Row = cntRow
                    SprdMain.Col = ColHSN
                    SprdMain.Text = mHSNCode
                    SprdMain.Col = ColCGSTPer
                    SprdMain.Text = VB6.Format(pCGSTPer, "0.00")
                    SprdMain.Col = ColSGSTPer
                    SprdMain.Text = VB6.Format(pSGSTPer, "0.00")
                    SprdMain.Col = ColIGSTPer
                    SprdMain.Text = VB6.Format(pIGSTPer, "0.00")
                Next
                CalcTots()
            End If
        Else
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
    Private Sub txtSTDSAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSTDSAmount.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtSTDSAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSTDSAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSTDSAmount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSTDSAmount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        CalcTots()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtSTDSDeductOn_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSTDSDeductOn.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtSTDSDeductOn_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSTDSDeductOn.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSTDSDeductOn_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSTDSDeductOn.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtSTDSDeductOn.Text = VB6.Format(txtSTDSDeductOn.Text, "0.00")
        CalcTots()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtSTDSRate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSTDSRate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtSTDSRate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSTDSRate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSTDSRate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSTDSRate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtSTDSRate.Text = VB6.Format(txtSTDSRate.Text, "0.00")
        CalcTots()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtTariff_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTariff.DoubleClick
        SearchTariff()
    End Sub
    Private Sub txtTariff_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtTariff.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchTariff()
    End Sub
    Private Sub txtTariff_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTariff.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        If Trim(txtTariff.Text) = "" Then GoTo EventExitSub
        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable((txtTariff.Text), "TARRIF_CODE", "TARRIF_DESC", "FIN_TARRIF_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            ErrorMsg("Please Enter Vaild Tariff.", "", MsgBoxStyle.Critical)
            Cancel = True
        Else
            txtItemType.Text = MasterNo
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtTDSAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTDSAmount.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtTDSAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTDSAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtTDSAmount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTDSAmount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        CalcTots()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtTDSDeductOn_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTDSDeductOn.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtTDSDeductOn_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTDSDeductOn.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtTDSDeductOn_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTDSDeductOn.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtTDSDeductOn.Text = VB6.Format(txtTDSDeductOn.Text, "0.00")
        CalcTots()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtTdsRate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTDSRate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtTdsRate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTDSRate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtTdsRate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTDSRate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtTDSRate.Text = VB6.Format(txtTDSRate.Text, "0.000")
        CalcTots()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtVDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVDate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtVDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtVDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtVDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtVDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If
        If FYChk((txtVDate.Text)) = False Then
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Public Sub txtVNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtVNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim xMkey As String = ""
        Dim mVNo As String
        Dim SqlStr As String = ""
        If Trim(txtVNo.Text) = "" Then GoTo EventExitSub
        txtVNo.Text = VB6.Format(Val(txtVNo.Text), "00000")
        If MODIFYMode = True And RsPurchMain.EOF = False Then xMkey = RsPurchMain.Fields("mKey").Value
        mVNo = Trim(Trim(txtVNoPrefix.Text) & Trim(txtVNo.Text) & Trim(txtVNoSuffix.Text))
        SqlStr = " SELECT * FROM FIN_PURCHASE_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND VNo='" & MainClass.AllowSingleQuote(mVNo) & "' " & vbCrLf & " AND BookCode=" & Val(LblBookCode.Text) & " " & vbCrLf & " AND BookType='" & mBookType & "' "
        SqlStr = SqlStr & vbCrLf & " AND VDATE>=TO_DATE('" & VB6.Format(PubGSTApplicableDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        SqlStr = SqlStr & vbCrLf & " AND PURCHASE_TYPE='" & lblPurchaseType.Text & "'"
        If RsCompany.Fields("FYEAR").Value >= 2018 Then
            SqlStr = SqlStr & vbCrLf & " AND PURCHASESEQTYPE=" & Val(lblPurchaseSeqType.Text) & ""
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPurchMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsPurchMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Voucher, Use Generate Voucher Option To add", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM FIN_PURCHASE_HDR " & " WHERE Mkey='" & MainClass.AllowSingleQuote(xMkey) & "' "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPurchMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Function UpdateMain1() As Boolean
        On Error GoTo ErrPart
        Dim I As Short
        Dim SqlStr As String = ""
        Dim nMkey As String
        Dim mTRNType As String
        Dim mTRNTypeName As String
        Dim mVNoSeq As Double
        Dim mVNo As String
        Dim mSuppCustCode As String
        Dim mModvatSuppCode As String
        Dim mAccountCode As String
        Dim mFREIGHTCHARGES As String
        Dim mEXEMPT_NOTIF_NO As String
        Dim mItemValue As Double
        Dim mTOTSTAMT As Double
        Dim mTOTCHARGES As Double
        Dim mTotEDAmount As Double
        Dim mTotEDUAmount As Double
        Dim mTOTEXPAMT As Double
        Dim mNETVALUE As Double
        Dim mTotQty As Double
        Dim mLSTCST As String
        Dim mWITHFORM As String
        Dim mPRINTED As String
        Dim mCancelled As String
        Dim mIsRegdNo As String
        Dim mSTType As String
        Dim mBookCode As Integer
        Dim mStartingNo As Double
        Dim mSTPERCENT As Double
        Dim mTOTFREIGHT As Double
        Dim mEDPERCENT As Double
        Dim mEDUPERCENT As Double
        Dim mSHECPercent As Double
        Dim mTOTTAXABLEAMOUNT As Double
        Dim mIsGSTRefund As String
        Dim mSRBillNo As String
        Dim mSRBillDate As String

        Dim mSRBillNo1 As String
        Dim mSRBillDate1 As String
        Dim pNewVNO As String

        Dim mRO As Double
        Dim mSURAmount As Double
        Dim mMSC As Double
        Dim mTotDiscount As Double
        Dim mREJECTION As String
        Dim pDueDate As String
        Dim mModvatNo As Integer
        Dim mSERVNo As Integer
        Dim mSTCLAIMNo As Integer
        Dim mCapital As String
        Dim mNarration As String = ""
        Dim mISMODVAT As String
        Dim mIsServClaim As String
        Dim mISSTREFUND As String
        Dim mISCSTREFUND As String
        Dim mFinalPost As String
        Dim mItemType As String
        Dim cntRow As Integer
        Dim xShortageQty As Double
        Dim xRejectedQty As Double
        Dim mPreviousRJ As Double
        Dim mAlreadyRejQty As Double
        Dim mDNCNQty As Double
        Dim xDebitAmt As Double
        Dim xPORate As Double
        Dim xRate As Double
        Dim xRateDiffDN As Double
        Dim xRateDiffCN As Double
        Dim mISTDSDEDUCT As String
        Dim mISESIDEDUCT As String
        Dim mISSTDSDEDUCT As String
        Dim mPDIRItem As Integer
        Dim PDIRAmount As Double
        Dim mDNCNCreated As Boolean
        Dim xExpDiffDN As Boolean
        Dim mFormRecdCode As Integer
        Dim mFormDueCode As Integer
        Dim mISFOC As String
        Dim mIsSuppBill As String
        Dim mServTax_Repost As String
        Dim mApproved As String
        Dim mItemCode As String
        Dim mModvatType As Integer
        Dim mISFixAssets As String
        Dim mItemDesc As String
        Dim mModvatAmount As Double
        Dim mLocal As String
        Dim mDivisionCode As Double
        Dim mServiceCode As String
        Dim RsPostSRTrn As ADODB.Recordset = Nothing
        Dim xItemValue As Double
        Dim xTOTEXPAMT As Double
        Dim xTotED As Double
        Dim xTotST As Double
        Dim xModvatAmount As Double
        Dim xCESSAmount As Double
        Dim xSHECAmount As Double
        Dim xServiceAmount As Double
        Dim xEDUAmount As Double
        Dim xSHEC As Double
        Dim xSTClaimAmount As Double
        Dim xNETVALUE As Double
        Dim xSurOnVat As Double
        Dim xSurcharge As Double
        Dim mFirstRow As Boolean
        Dim mSubRowNo As Integer
        Dim mGSTNo As Double
        Dim mTotGSTAmount As Double
        Dim mShipTo As String
        Dim mShipToCode As String = ""
        Dim mNetExpAmount As Double
        Dim mSaleBillNoPrefix As String
        Dim mSaleBillNoSeq As Double
        Dim mSaleBillNo As String
        Dim mSaleBillDate As String
        Dim mNewGSTNo As Boolean
        Dim mPartyGSTNo As String
        Dim mCompanyGSTNo As String
        Dim mSACCode As String
        Dim mItemCGST As Double
        Dim mItemSGST As Double
        Dim mItemIGST As Double
        Dim mRejCreditNoteNo As String
        Dim mShipToLoc As String
        Dim pJVTMKey As String
        Dim pSectionCode As Long
        Dim mMannualAdjustment As String
        Dim mBalanceAmount As Double
        Dim mDeliveryToCode As String = ""
        Dim mDeliveryToLoc As String = ""

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        mNewGSTNo = False
        mCompanyGSTNo = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
        mPartyGSTNo = ""
        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPartyGSTNo = MasterNo
        End If
        If ADDMode = True And chkFOC.CheckState = System.Windows.Forms.CheckState.Unchecked And chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked And CDbl(LblBookCode.Text) = ConPurchaseBookCode Then
            pTempDNCNSeq = MainClass.AutoGenRowNo("TEMP_FIN_DNCN_DET", "RowNo", PubDBCn)
            If UpdateTempDNCNTable(pTempDNCNSeq, IIf(mCompanyGSTNo = mPartyGSTNo, "Y", "N")) = False Then GoTo ErrPart
        End If
        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If
        mFormRecdCode = -1
        mFormDueCode = -1
        If CDbl(LblBookCode.Text) = ConModvatBookCode Then
            If MainClass.ValidateWithMasterTable(cboInvType.Text, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mTRNType = MasterNo
            Else
                mTRNType = CStr(-1)
            End If
            '        Left(cboGSTStatus.Text, 1)="G"
        Else

            'If RsCompany.Fields("PURCHASE_POSTINGTYPE").Value = "B" Then
            '    If MainClass.ValidateWithMasterTable(cboInvType.Text, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            '        mTRNType = MasterNo
            '    Else
            '        mTRNType = CStr(-1)
            '        MsgBox("INVOICE TYPE Does Not Exist In Master", MsgBoxStyle.Information)
            '        GoTo ErrPart
            '    End If
            'Else
            '    mTRNType = CStr(-1)
            'End If

            SprdMain.Row = 1
            SprdMain.Col = ColInvType
            mTRNTypeName = Trim(SprdMain.Text)
            If MainClass.ValidateWithMasterTable(mTRNTypeName, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mTRNType = MasterNo
            Else
                mTRNType = CStr(-1)
                MsgBox("INVOICE TYPE Does Not Exist In Master", MsgBoxStyle.Information)
                GoTo ErrPart
            End If
            mAccountCode = GetDebitNameOfInvType(mTRNTypeName, "N")
        End If
        If CDbl(LblBookCode.Text) = ConPurchaseBookCode Then
            mFinalPost = "Y"
            chkFinalPost.CheckState = System.Windows.Forms.CheckState.Checked
        Else
            mFinalPost = "N"
            chkFinalPost.CheckState = System.Windows.Forms.CheckState.Unchecked
        End If
        mSuppCustCode = CStr(-1)
        If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSuppCustCode = MasterNo
        Else
            mSuppCustCode = CStr(-1)
            MsgBox("Customer Does Not Exist In Master", MsgBoxStyle.Information)
            GoTo ErrPart
        End If

        pSectionCode = -1

        If Trim(txtSection.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtSection.Text, "NAME", "CODE", "TDS_SECTION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                pSectionCode = MasterNo
            End If
        End If

        mLocal = "N"
        If Trim(txtSupplier.Text) <> "" Then
            mLocal = GetPartyBusinessDetail(Trim(mSuppCustCode), Trim(txtBillTo.Text), "WITHIN_STATE")
        End If

        'If MainClass.ValidateWithMasterTable(mSuppCustCode, "SUPP_CUST_CODE", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mLocal = Trim(MasterNo)
        'Else
        '    mLocal = "N"
        'End If
        '*********
        mModvatSuppCode = CStr(-1)
        '*************
        '    If LblBookCode.text = ConModvatBookCode Or LblBookCode.text = ConServiceClaimBookCode Or LblBookCode.text = ConSTClaimBookCode Then

        'If RsCompany.Fields("PURCHASE_POSTINGTYPE").Value = "B" Then
        '    If Trim(txtDebitAccount.Text) = "" Then
        '        mAccountCode = "-1"
        '    Else
        '        If MainClass.ValidateWithMasterTable(txtDebitAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '            mAccountCode = MasterNo
        '        Else
        '            mAccountCode = "-1"
        '            MsgBox("Credit Account Does Not Exist In Master", MsgBoxStyle.Information)
        '            GoTo ErrPart
        '        End If
        '    End If
        'Else
        '    mAccountCode = "-1"
        'End If
        mFREIGHTCHARGES = IIf(OptFreight(0).Checked = True, "To Pay", "Paid")
        mEXEMPT_NOTIF_NO = ""
        mBookCode = CInt(LblBookCode.Text)
        If CDbl(LblBookCode.Text) = ConModvatBookCode Then
            mBookSubType = "E"
        Else
            If RsCompany.Fields("PURCHASE_POSTINGTYPE").Value = "B" Then
                If chkRejection.CheckState = System.Windows.Forms.CheckState.Checked Then
                    mBookSubType = "R"
                Else
                    If MainClass.ValidateWithMasterTable(mTRNTypeName, "NAME", "IDENTIFICATION", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
                        mBookSubType = MasterNo
                    Else
                        mBookSubType = CStr(-1)
                    End If
                End If
            Else
                If chkRejection.CheckState = System.Windows.Forms.CheckState.Checked Then
                    mBookSubType = "R"
                Else
                    mBookSubType = "E"
                End If
            End If
        End If
        mItemValue = Val(lblTotItemValue.Text)
        mTOTSTAMT = 0
        mTOTCHARGES = Val(lblTotCharges.Text)
        mTotEDAmount = 0
        mTOTEXPAMT = Val(lblTotExpAmt.Text)
        mNETVALUE = Val(lblNetAmount.Text)
        mSTPERCENT = Val(lblSTPercentage.Text)
        mTOTFREIGHT = Val(lblTotFreight.Text)
        mEDPERCENT = Val(lblEDPercentage.Text)
        mEDUPERCENT = Val(lblEDUPercent.Text)
        mSHECPercent = 0
        mTOTTAXABLEAMOUNT = Val(lblTotTaxableAmt.Text)
        mRO = Val(lblRO.Text)
        mTotDiscount = Val(lblDiscount.Text)
        mSURAmount = Val(lblSurcharge.Text)
        mMSC = Val(lblMSC.Text)
        mTotQty = Val(lblTotQty.Text)
        mLSTCST = ""
        mWITHFORM = ""
        mPRINTED = "N"
        mCancelled = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mIsRegdNo = "N"
        mREJECTION = IIf(chkRejection.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mCapital = IIf(ChkCapital.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mISMODVAT = "N"
        mIsGSTRefund = VB.Left(cboGSTStatus.Text, 1)
        mIsServClaim = "N"
        mIsServClaim = "N"
        mISSTREFUND = "N"
        mISCSTREFUND = "N"
        mISTDSDEDUCT = IIf(chkTDS.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mISESIDEDUCT = IIf(chkESI.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mISSTDSDEDUCT = IIf(ChkSTDS.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mServTax_Repost = "N"
        mISFOC = IIf(chkFOC.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mIsSuppBill = "N"
        mSTType = "0"
        mTotGSTAmount = Val(lblTotCGSTAmount.Text) + Val(lblTotSGSTAmount.Text) + Val(lblTotIGSTAmount.Text)
        If CDbl(LblBookCode.Text) = ConModvatBookCode Then
            If VB.Left(cboGSTStatus.Text, 1) = "G" Then
                mStartingNo = 1
                If Trim(lblGSTClaimNo.Text) = "" Or Val(lblGSTClaimNo.Text) = 0 Then
                    mGSTNo = CDbl(AutoGenSeqGSTNo())
                    mNewGSTNo = True
                Else
                    mGSTNo = Val(lblGSTClaimNo.Text)
                End If
            End If
        End If
        If CDbl(LblBookCode.Text) = ConPurchaseBookCode And lblSeprateGST.Text = "Y" Then
            mGSTNo = Val(lblGSTClaimNo.Text)
            If Trim(txtVNo.Text) = "" Then
                If RsCompany.Fields("FYEAR").Value >= 2018 Then
                    mVNoSeq = CDbl(AutoGenSeqBillNoNew("VNOSEQ", mBookType, mBookSubType, mStartingNo, mDivisionCode))
                Else
                    mVNoSeq = CDbl(AutoGenSeqBillNo("VNOSEQ", mBookType, mBookSubType, mStartingNo, mDivisionCode))
                End If
            Else
                mVNoSeq = Val(txtVNo.Text)
            End If
        ElseIf CDbl(LblBookCode.Text) = ConPurchaseBookCode And lblSeprateGST.Text = "N" Then
            If VB.Left(cboGSTStatus.Text, 1) = "G" And (Val(txtTotCGSTRefund.Text) + Val(txtTotSGSTRefund.Text) + Val(txtTotIGSTRefund.Text) > 0) Then
                mStartingNo = 1
                If Trim(lblGSTClaimNo.Text) = "" Or Val(lblGSTClaimNo.Text) = 0 Then
                    mGSTNo = CDbl(AutoGenSeqGSTNo())
                    mNewGSTNo = True
                Else
                    mGSTNo = Val(lblGSTClaimNo.Text)
                End If
            End If
            If Trim(txtVNo.Text) = "" Then
                If RsCompany.Fields("FYEAR").Value >= 2018 Then
                    mVNoSeq = AutoGenSeqBillNoNew("VNOSEQ", mBookType, mBookSubType, mStartingNo, mDivisionCode)
                Else
                    mVNoSeq = AutoGenSeqBillNo("VNOSEQ", mBookType, mBookSubType, mStartingNo, mDivisionCode)
                End If
            Else
                mVNoSeq = Val(txtVNo.Text)
            End If
        End If
        mModvatNo = 0
        txtVNo.Text = IIf(mVNoSeq = -1 Or mVNoSeq = 0, "", VB6.Format(Val(CStr(mVNoSeq)), "00000"))
        lblGSTClaimNo.Text = VB6.Format(Val(CStr(mGSTNo)), "00000")
        txtServNo.Text = VB6.Format(Val(CStr(mSERVNo)), "00000")
        If CheckValidVDate(mVNoSeq) = False Then GoTo ErrPart : 
        txtNarration.Text = GetNarration()

        If Trim(txtVNo.Text) = "" Then
            MsgInformation("Please Check Voucher No.")
            GoTo ErrPart
        End If
        If mVNoSeq = -1 Or mVNoSeq = 0 Then
            mVNo = "-1"
        Else
            mVNo = Trim(Trim(txtVNoPrefix.Text) & VB6.Format(Val(CStr(mVNoSeq)), "00000") & Trim(txtVNoSuffix.Text))
        End If
        If lblPurchaseSeqType.Text = 2 Then       ''If chkRejection.CheckState = System.Windows.Forms.CheckState.Checked Then
            If Trim(txtCReditNoteNo.Text) = "" Then
                txtCReditNoteNo.Text = AutoCreditNoteNo()
            End If
            mRejCreditNoteNo = Trim(txtCReditNoteNo.Text)
            'If CDate(txtVDate.Text) >= CDate("01/07/2017") And CDate(txtVDate.Text) <= CDate("31/07/2017") Then
            '    mRejCreditNoteNo = mVNo
            'Else
            '    If RsCompany.Fields("FYEAR").Value >= 2020 Then
            '        mRejCreditNoteNo = mVNo
            '    Else
            '        mRejCreditNoteNo = Trim(Trim(txtVNoPrefix.Text) & VB6.Format(IIf(IsDBNull(RsCompany.Fields("INVOICE_PREFIX").Value), "", RsCompany.Fields("INVOICE_PREFIX").Value), "00") & VB6.Format(Val(CStr(mVNoSeq)), "00000") & Trim(txtVNoSuffix.Text))
            '    End If
            'End If
        Else
            mRejCreditNoteNo = ""
        End If

        SqlStr = ""
        If VB.Left(cboGSTStatus.Text, 1) = "G" Then
            mModvatType = 1
        Else
            mModvatType = 0
        End If
        If MainClass.ValidateWithMasterTable(Trim(txtServProvided.Text), "HSN_DESC", "HSN_CODE", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
            mSACCode = Trim(MasterNo)
        Else
            mSACCode = ""
        End If
        mShipTo = IIf(chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        If mShipTo = "Y" Then
            mShipToCode = mSuppCustCode
            mShipToLoc = txtBillTo.Text
        Else
            If MainClass.ValidateWithMasterTable(txtShippedTo.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mShipToCode = MasterNo
            End If
            mShipToLoc = TxtShipTo.Text
        End If
        If VB.Left(cboGSTStatus.Text, 1) = "R" Then
            '        If ADDMode = True Then
            '            mSaleBillNoPrefix = "S"
            '            mSaleBillNoSeq = AutoGenSeqSaleBillNo(lblPurchaseType.text)
            '            mSaleBillNo = mSaleBillNoPrefix & vb6.Format(mSaleBillNoSeq, "00000000")
            '            mSaleBillDate = Format(TxtVDate.Text, "DD/MM/YYYY")
            '        Else
            mSaleBillNoPrefix = "S"
            mSaleBillNoSeq = Val(lblSaleBillNoSeq.Text)
            mSaleBillNo = lblSaleBillNo.Text
            mSaleBillDate = VB6.Format(lblSaleBillDate.Text, "DD/MM/YYYY")
            '        End If
        Else
            mSaleBillNoPrefix = ""
            mSaleBillNoSeq = 0
            mSaleBillNo = ""
            mSaleBillDate = ""
        End If
        mServiceCode = CStr(-1)

        If txtDeliveryTo.Text = "" Then
            mDeliveryToCode = ""
            mDeliveryToLoc = ""
        Else
            If MainClass.ValidateWithMasterTable((txtDeliveryTo.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDeliveryToCode = MasterNo
            End If
            mDeliveryToLoc = txtDeliveryToLoc.Text
        End If

        If MODIFYMode = True Then
            If InsertIntoDelAudit(PubDBCn, "FIN_PURCHASE_HDR", (LblMKey.Text), RsPurchMain, "MKEY", "M") = False Then GoTo ErrPart
            If InsertIntoDelAudit(PubDBCn, "FIN_PURCHASE_DET", (LblMKey.Text), RsPurchDetail, "MKEY", "M") = False Then GoTo ErrPart
            If InsertIntoDelAudit(PubDBCn, "FIN_PURCHASE_EXP", (LblMKey.Text), RsPurchExp, "MKEY", "M") = False Then GoTo ErrPart
        End If

        If ADDMode = True Then
            mCurRowNo = MainClass.AutoGenRowNo("FIN_PURCHASE_HDR", "RowNo", PubDBCn)
            nMkey = RsCompany.Fields("COMPANY_CODE").Value & RsCompany.Fields("FYEAR").Value & mCurRowNo
            LblMKey.Text = nMkey
            SqlStr = "INSERT INTO FIN_PURCHASE_HDR( " & vbCrLf _
                & " MKEY, COMPANY_CODE, FYEAR, ROWNO," & vbCrLf _
                & " TRNTYPE, VNOPREFIX, VNOSEQ, VNOSUFFIX, VNO, VDATE, " & vbCrLf _
                & " BILLNO, INVOICE_DATE, AUTO_KEY_MRR, MRRDATE," & vbCrLf _
                & " CUSTREFNO, CUSTREFDATE, SUPP_CUST_CODE, MODVAT_SUPP_CODE, ACCOUNTCODE," & vbCrLf _
                & " ST_38_NO, DUEDAYSFROM, DUEDAYSTO, DESPATCHMODE," & vbCrLf _
                & " DOCSTHROUGH, VEHICLENO, CARRIERS, FREIGHTCHARGES," & vbCrLf _
                & " TARIFFHEADING, EXEMPT_NOTIF_NO," & vbCrLf _
                & " BOOKCODE, BOOKTYPE, BOOKSUBTYPE, " & vbCrLf _
                & " REMARKS, ITEMDESC, ITEMVALUE," & vbCrLf _
                & " TOTSTAMT, TOTCHARGES, " & vbCrLf _
                & " TOTEDAMOUNT, TOTEXPAMT, NETVALUE, TOTQTY,  " & vbCrLf _
                & " STTYPE, STFORMCODE, STFORMNAME, STFORMNO, " & vbCrLf _
                & " STFORMDATE, STDUEFORMCODE, STDUEFORMNAME, STDUEFORMNO, STDUEFORMDATE," & vbCrLf _
                & " ISREGDNO, LSTCST, WITHFORM, " & vbCrLf _
                & " CANCELLED, REJECTION,  NARRATION,  " & vbCrLf _
                & " STPERCENT,TOTFREIGHT,EDPERCENT,TOTTAXABLEAMOUNT,  " & vbCrLf _
                & " TOTSURCHARGEAMT, TOTDISCAMOUNT, TOTMSCAMOUNT,TOTRO,  " & vbCrLf _
                & " MODVATNO, MODVATDATE, MODVATPER, MODVATAMOUNT, " & vbCrLf _
                & " STCLAIMNO, STCLAIMDATE, STCLAIMPER, STCLAIMAMOUNT,ISCAPITAL, PAYMENTDATE, " & vbCrLf _
                & " ISMODVAT,ISSTREFUND, ISCSTREFUND, ISFINALPOST,ISTDSDEDUCT,TDSPER,TDSAMOUNT, " & vbCrLf _
                & " ISESIDEDUCT,ESIPER,ESIAMOUNT, ISSTDSDEDUCT,STDSPER,STDSAMOUNT," & vbCrLf _
                & " TDS_DEDUCT_ON, STDS_DEDUCT_ON, ESI_DEDUCT_ON, " & vbCrLf _
                & " ADDUSER, ADDDATE, MODUSER, MODDATE, "

            SqlStr = SqlStr & vbCrLf & " MODVATItemValue, " & vbCrLf _
                & " TOTEDUPERCENT,TOTEDUAMOUNT," & vbCrLf _
                & " CESSABLEAMOUNT,CESSPER,CESSAMOUNT," & vbCrLf _
                & " ISFOC,ISSUPPBILL,"

            SqlStr = SqlStr & vbCrLf _
                & " TOTSERVICEPERCENT,TOTSERVICEAMOUNT, " & vbCrLf _
                & " SERVNO, SERVDATE, " & vbCrLf _
                & " ISSERVCLAIM, " & vbCrLf _
                & " SERVCLAIMPERCENT, SERVICECLAIMAMOUNT, ISSERVTAX_POST,SERV_PROV, "

            SqlStr = SqlStr & vbCrLf _
                & " SHECMODVATPER,SHECMODVATAMOUNT, SHECPERCENT, SHECAMOUNT, " & vbCrLf _
                & " ADEMODVATPER,ADEMODVATAMOUNT, ADEAMOUNT,UPDATE_FROM,MODVAT_TYPE,SUR_VATCLAIMAMOUNT,DIV_CODE," & vbCrLf _
                & " SAC_CODE, SERVICE_ON_AMT, SERV_PROVIDER_PER, " & vbCrLf _
                & " SERV_RECIPIENT_PER,SERVICE_TAX_PER,SERVICE_TAX_AMOUNT,KK_CESS_PER,KK_CESS_AMOUNT, " & vbCrLf _
                & " ISGSTAPPLICABLE, GST_CLAIM_NO, GST_CLAIM_DATE, " & vbCrLf _
                & " TOTALGSTVALUE, TOTCGST_REFUNDAMT, TOTSGST_REFUNDAMT, " & vbCrLf _
                & " TOTIGST_REFUNDAMT, TOTCGST_AMOUNT, TOTSGST_AMOUNT, " & vbCrLf _
                & " TOTIGST_AMOUNT, SHIPPED_TO_SAMEPARTY, SHIPPED_TO_PARTY_CODE, " & vbCrLf _
                & " PURCHASE_TYPE, " & vbCrLf _
                & " ADV_VNO, ADV_VDATE, ADV_ADJUSTED_AMT, " & vbCrLf _
                & " ADV_CGST_AMT, ADV_SGST_AMT, ADV_IGST_AMT,ADV_ITEM_AMT, PORT_CODE, REJ_CREDITNOTE, " & vbCrLf _
                & " BE_NO, BE_DATE, BE_AMOUNT, PURCHASESEQTYPE,BILL_TO_LOC_ID, SHIP_TO_LOC_ID,SECTION_CODE,CUSTOMER_REF_NO,DELIVERY_TO,DELIVERY_TO_LOC_ID " & vbCrLf _
                & " )"

            SqlStr = SqlStr & vbCrLf _
                & " VALUES('" & nMkey & "'," & RsCompany.Fields("Company_Code").Value & "," & RsCompany.Fields("FYEAR").Value & ", " & mCurRowNo & ", " & vbCrLf & " " & Val(mTRNType) & ", '" & MainClass.AllowSingleQuote(txtVNoPrefix.Text) & "', " & vbCrLf & " " & mVNoSeq & ", '" & MainClass.AllowSingleQuote(txtVNoSuffix.Text) & "', '" & MainClass.AllowSingleQuote(mVNo) & "',TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtBillNo.Text) & "',TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " " & Val(txtMRRNo.Text) & ", TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtPONo.Text) & "',TO_DATE('" & VB6.Format(txtPODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & mSuppCustCode & "', '" & mModvatSuppCode & "', '" & mAccountCode & "','', " & vbCrLf & " " & Val(txtCreditDays(0).Text) & ", " & Val(txtCreditDays(1).Text) & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtMode.Text) & "', '" & MainClass.AllowSingleQuote(txtDocsThru.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtVehicle.Text) & "', '" & MainClass.AllowSingleQuote(txtCarriers.Text) & "', '" & mFREIGHTCHARGES & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtTariff.Text) & "', '" & mEXEMPT_NOTIF_NO & "', " & vbCrLf & " '" & mBookCode & "', '" & mBookType & "', '" & mBookSubType & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', '" & MainClass.AllowSingleQuote(txtItemType.Text) & "', " & mItemValue & ", " & vbCrLf & " " & mTOTSTAMT & ", " & mTOTCHARGES & ", " & mTotEDAmount & ", " & vbCrLf & " " & mTOTEXPAMT & ", " & mNETVALUE & ", " & mTotQty & ", " & vbCrLf & " '" & mSTType & "'," & mFormRecdCode & ",'','', '', " & vbCrLf & " " & mFormDueCode & ",'','', '', " & vbCrLf & " '" & mIsRegdNo & "', '" & mLSTCST & "', " & vbCrLf & " '" & mWITHFORM & "', " & vbCrLf & " '" & mCancelled & "', '" & mREJECTION & "', '" & MainClass.AllowSingleQuote(txtNarration.Text) & "',  " & vbCrLf & "" & mSTPERCENT & "," & mTOTFREIGHT & "," & mEDPERCENT & "," & mTOTTAXABLEAMOUNT & "," & vbCrLf & "" & mSURAmount & "," & mTotDiscount & "," & mMSC & "," & mRO & ", "

            SqlStr = SqlStr & vbCrLf _
                & " '" & mModvatNo & "', TO_DATE('" & VB6.Format(txtModvatDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), 0,0, " & vbCrLf _
                & " '" & mSTCLAIMNo & "','',0,0, '" & mCapital & "'," & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtPaymentdate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') , " & vbCrLf _
                & " '" & mISMODVAT & "','" & mISSTREFUND & "','" & mISCSTREFUND & "', '" & mFinalPost & "'," & vbCrLf _
                & " '" & mISTDSDEDUCT & "'," & Val(txtTDSRate.Text) & ", " & Val(txtTDSAmount.Text) & ", " & vbCrLf _
                & " '" & mISESIDEDUCT & "'," & Val(txtESIRate.Text) & ", " & Val(txtESIAmount.Text) & ", " & vbCrLf _
                & " '" & mISSTDSDEDUCT & "'," & Val(txtSTDSRate.Text) & ", " & Val(txtSTDSAmount.Text) & ", " & vbCrLf _
                & " '" & Val(txtTDSDeductOn.Text) & "'," & Val(txtSTDSDeductOn.Text) & ", " & Val(txtESIDeductOn.Text) & ", " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')," & vbCrLf _
                & " '','',0," & Val(lblEDUPercent.Text) & ",0," & Val(lblCESSableAmount.Text) & ",0,0, " & vbCrLf _
                & " '" & mISFOC & "','" & mIsSuppBill & "',"

            SqlStr = SqlStr & vbCrLf _
                & " " & Val(lblServicePercentage.Text) & "," & vbCrLf _
                & " 0," & vbCrLf & " '" & mSERVNo & "', TO_DATE('" & VB6.Format(txtServDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " '" & mIsServClaim & "', " & vbCrLf & " 0, 0," & vbCrLf _
                & " '" & mServTax_Repost & "','" & MainClass.AllowSingleQuote(txtServProvided.Text) & "',"

            SqlStr = SqlStr & vbCrLf _
                & " 0, " & vbCrLf & " 0, " & vbCrLf & " 0, " & vbCrLf _
                & " 0, " & vbCrLf & " 0, " & vbCrLf & " 0, " & vbCrLf _
                & " 0, 'N','" & mModvatType & "',0," & mDivisionCode & "," & vbCrLf _
                & " '" & mSACCode & "', " & Val(txtServiceOn.Text) & ", " & Val(txtProviderPer.Text) & ", " & Val(txtRecipientPer.Text) & ", " & vbCrLf _
                & " " & Val(txtServiceTaxPer.Text) & "," & Val(txtServiceTaxAmount.Text) & ",0,0, " & vbCrLf _
                & " '" & mIsGSTRefund & "', " & Val(CStr(mGSTNo)) & ", TO_DATE('" & VB6.Format(lblGSTClaimDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " " & Val(CStr(mTotGSTAmount)) & ", " & Val(txtTotCGSTRefund.Text) & ", " & Val(txtTotSGSTRefund.Text) & ", " & vbCrLf _
                & " " & Val(txtTotIGSTRefund.Text) & ", " & Val(lblTotCGSTAmount.Text) & ", " & Val(lblTotSGSTAmount.Text) & "," & vbCrLf _
                & " " & Val(lblTotIGSTAmount.Text) & ",'" & mShipTo & "', '" & mShipToCode & "', " & vbCrLf _
                & " '" & lblPurchaseType.Text & "'," & vbCrLf _
                & " '" & Trim(txtAdvVNo.Text) & "', " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtAdvDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & Val(txtAdvAdjust.Text) & ", " & vbCrLf _
                & " " & Val(txtAdvCGST.Text) & ", " & Val(txtAdvSGST.Text) & ", " & Val(txtAdvIGST.Text) & ", " & Val(txtItemAdvAdjust.Text) & ", " & vbCrLf _
                & " '" & Trim(txtPortCode.Text) & "','" & mRejCreditNoteNo & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtBENo.Text) & "',TO_DATE('" & VB6.Format(txtBEDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & Val(txtBEAmount.Text) & " ," & Val(lblPurchaseSeqType.Text) & ", '" & MainClass.AllowSingleQuote(txtBillTo.Text) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(mShipToLoc) & "'," & IIf(pSectionCode = -1, "NULL", pSectionCode) & ", '" & MainClass.AllowSingleQuote(txtCustomerRefNo.Text) & "','" & MainClass.AllowSingleQuote(mDeliveryToCode) & "','" & MainClass.AllowSingleQuote(mDeliveryToLoc) & "')"

        ElseIf MODIFYMode = True Then
            SqlStr = ""
            SqlStr = "UPDATE FIN_PURCHASE_HDR SET " & vbCrLf & " VNOPREFIX = '" & MainClass.AllowSingleQuote(txtVNoPrefix.Text) & "'," & vbCrLf & " VNOSEQ= " & mVNoSeq & ", TRNTYPE=" & Val(mTRNType) & "," & vbCrLf & " VNOSUFFIX= '" & MainClass.AllowSingleQuote(txtVNoSuffix.Text) & "'," & vbCrLf & " VNO= '" & MainClass.AllowSingleQuote(mVNo) & "'," & vbCrLf & " VDATE= TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " BILLNO= '" & MainClass.AllowSingleQuote(txtBillNo.Text) & "'," & vbCrLf & " INVOICE_DATE= TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " AUTO_KEY_MRR= " & Val(txtMRRNo.Text) & "," & vbCrLf & " MRRDATE= TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " CUSTREFNO='" & MainClass.AllowSingleQuote(txtPONo.Text) & "', " & vbCrLf & " CUSTREFDATE= TO_DATE('" & VB6.Format(txtPODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " SUPP_CUST_CODE= '" & mSuppCustCode & "'," & vbCrLf & " MODVAT_SUPP_CODE= '" & mModvatSuppCode & "'," & vbCrLf & " ACCOUNTCODE= '" & mAccountCode & "'," & vbCrLf & " ST_38_NO= '', PORT_CODE='" & Trim(txtPortCode.Text) & "',"
            SqlStr = SqlStr & vbCrLf & " DUEDAYSFROM= " & Val(txtCreditDays(0).Text) & "," & vbCrLf & " DUEDAYSTO= " & Val(txtCreditDays(1).Text) & ", " & vbCrLf & " DESPATCHMODE= '" & MainClass.AllowSingleQuote(txtMode.Text) & "', " & vbCrLf & " DOCSTHROUGH= '" & MainClass.AllowSingleQuote(txtDocsThru.Text) & "'," & vbCrLf & " VEHICLENO= '" & MainClass.AllowSingleQuote(txtVehicle.Text) & "', " & vbCrLf & " CARRIERS=  '" & MainClass.AllowSingleQuote(txtCarriers.Text) & "'," & vbCrLf & " FREIGHTCHARGES= '" & mFREIGHTCHARGES & "', " & vbCrLf & " TARIFFHEADING= '" & MainClass.AllowSingleQuote(txtTariff.Text) & "'," & vbCrLf & " EXEMPT_NOTIF_NO= '" & MainClass.AllowSingleQuote(mEXEMPT_NOTIF_NO) & "',"
            SqlStr = SqlStr & vbCrLf & " BOOKCODE= " & mBookCode & "," & vbCrLf & " BOOKTYPE= '" & mBookType & "'," & vbCrLf & " BOOKSUBTYPE= '" & mBookSubType & "'," & vbCrLf & " REMARKS= '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "'," & vbCrLf & " ITEMDESC= '" & MainClass.AllowSingleQuote(txtItemType.Text) & "'," & vbCrLf & " ITEMVALUE= " & mItemValue & "," & vbCrLf & " TOTSTAMT= " & mTOTSTAMT & "," & vbCrLf & " TOTCHARGES= " & mTOTCHARGES & "," & vbCrLf & " TOTEDAMOUNT= " & mTotEDAmount & "," & vbCrLf & " TOTEXPAMT= " & mTOTEXPAMT & "," & vbCrLf & " NETVALUE= " & mNETVALUE & "," & vbCrLf & " TOTQTY= " & mTotQty & "," & vbCrLf & " STTYPE= '" & mSTType & "'," & vbCrLf & " REJECTION='" & mREJECTION & "'," & vbCrLf & " STFORMCODE= " & mFormRecdCode & "," & vbCrLf & " STFORMNAME= ''," & vbCrLf & " STFORMNO= ''," & vbCrLf & " STFORMDATE='', " & vbCrLf & " STDUEFORMCODE= " & mFormDueCode & "," & vbCrLf & " STDUEFORMNAME= ''," & vbCrLf & " STDUEFORMNO= ''," & vbCrLf & " STDUEFORMDATE= '',"
            SqlStr = SqlStr & vbCrLf & " STPERCENT=" & mSTPERCENT & "," & vbCrLf & " TOTFREIGHT=" & mTOTFREIGHT & "," & vbCrLf & " EDPERCENT=" & mEDPERCENT & "," & vbCrLf & " TOTTAXABLEAMOUNT=" & mTOTTAXABLEAMOUNT & "," & vbCrLf & " ISREGDNO= '" & mIsRegdNo & "'," & vbCrLf & " LSTCST= '" & mLSTCST & "', " & vbCrLf & " WITHFORM= '" & mWITHFORM & "'," & vbCrLf & " CANCELLED= '" & mCancelled & "'," & vbCrLf & " NARRATION= '" & MainClass.AllowSingleQuote(txtNarration.Text) & "'," & vbCrLf & " TOTSURCHARGEAMT=" & mSURAmount & ", " & vbCrLf & " TOTDISCAMOUNT=" & mTotDiscount & ", " & vbCrLf & " TOTMSCAMOUNT=" & mMSC & ", TotRO=" & mRO & "," & vbCrLf & " MODVATNO='" & mModvatNo & "', " & vbCrLf & " MODVATDATE=TO_DATE('" & VB6.Format(txtModvatDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " MODVATPER=0, " & vbCrLf & " MODVATAMOUNT=0, " & vbCrLf & " TOTEDUPERCENT=" & Val(lblEDUPercent.Text) & ", " & vbCrLf & " TOTEDUAMOUNT=0, " & vbCrLf & " CESSABLEAMOUNT=" & Val(lblCESSableAmount.Text) & "," & vbCrLf & " CESSPER=0, " & vbCrLf & " CESSAMOUNT=0, " & vbCrLf & " TDS_DEDUCT_ON=" & Val(txtTDSDeductOn.Text) & ", " & vbCrLf & " ISTDSDEDUCT='" & mISTDSDEDUCT & "'," & vbCrLf & " TDSPER=" & Val(txtTDSRate.Text) & ", TDSAMOUNT=" & Val(txtTDSAmount.Text) & ", "
            SqlStr = SqlStr & vbCrLf & " MODVATItemValue=0," & vbCrLf & " ESI_DEDUCT_ON=" & Val(txtESIDeductOn.Text) & ", " & vbCrLf & " ISESIDEDUCT='" & mISESIDEDUCT & "'," & vbCrLf & " ESIPER=" & Val(txtESIRate.Text) & ", " & vbCrLf & " ESIAMOUNT=" & Val(txtESIAmount.Text) & ", " & vbCrLf & " STDS_DEDUCT_ON=" & Val(txtSTDSDeductOn.Text) & ", " & vbCrLf & " ISSTDSDEDUCT='" & mISSTDSDEDUCT & "'," & vbCrLf & " STDSPER=" & Val(txtSTDSRate.Text) & ", " & vbCrLf & " STDSAMOUNT=" & Val(txtSTDSAmount.Text) & ", " & vbCrLf & " STCLAIMNO='" & mSTCLAIMNo & "', " & vbCrLf & " STCLAIMDATE='', " & vbCrLf & " STCLAIMPER=0, " & vbCrLf & " STCLAIMAMOUNT=0, " & vbCrLf & " ISCAPITAL='" & mCapital & "', PAYMENTDATE=TO_DATE('" & VB6.Format(txtPaymentdate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " ISMODVAT='" & mISMODVAT & "',ISSTREFUND='" & mISSTREFUND & "', " & vbCrLf & " ISCSTREFUND='" & mISCSTREFUND & "', ISFINALPOST='" & mFinalPost & "', " & vbCrLf & " ISFOC='" & mISFOC & "',ISSUPPBILL='" & mIsSuppBill & "', "
            SqlStr = SqlStr & vbCrLf & " TOTSERVICEPERCENT=" & Val(lblServicePercentage.Text) & ", " & vbCrLf & " TOTSERVICEAMOUNT=0, " & vbCrLf & " SERVNO='" & mSERVNo & "', " & vbCrLf & " SERVDATE=TO_DATE('" & VB6.Format(txtServDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " ISSERVCLAIM='" & mIsServClaim & "', " & vbCrLf & " SERVCLAIMPERCENT=0, " & vbCrLf & " SERVICECLAIMAMOUNT=0, " & vbCrLf & " ISSERVTAX_POST='" & mServTax_Repost & "'," & vbCrLf & " SERV_PROV='" & MainClass.AllowSingleQuote(txtServProvided.Text) & "',"
            SqlStr = SqlStr & vbCrLf & " SHECMODVATPER=0, " & vbCrLf & " SHECMODVATAMOUNT=0, " & vbCrLf & " SHECPERCENT=0," & vbCrLf & " SHECAMOUNT=0," & vbCrLf & " ADEMODVATPER=0, " & vbCrLf & " ADEMODVATAMOUNT=0, " & vbCrLf & " ADEAMOUNT=0," & vbCrLf & " UPDATE_FROM='N',MODVAT_TYPE='" & mModvatType & "',SUR_VATCLAIMAMOUNT= 0,"
            SqlStr = SqlStr & vbCrLf & " SAC_CODE='" & mSACCode & "'," & vbCrLf & " SERVICE_ON_AMT=" & Val(txtServiceOn.Text) & "," & vbCrLf & " SERV_PROVIDER_PER=" & Val(txtProviderPer.Text) & "," & vbCrLf & " SERV_RECIPIENT_PER=" & Val(txtRecipientPer.Text) & "," & vbCrLf & " SERVICE_TAX_PER=" & Val(txtServiceTaxPer.Text) & "," & vbCrLf & " SERVICE_TAX_AMOUNT=" & Val(txtServiceTaxAmount.Text) & "," & vbCrLf & " KK_CESS_PER=0," & vbCrLf & " KK_CESS_AMOUNT=0,"
            SqlStr = SqlStr & vbCrLf & " ISGSTAPPLICABLE='" & mIsGSTRefund & "', " & vbCrLf & " GST_CLAIM_NO=" & Val(CStr(mGSTNo)) & ",  " & vbCrLf & " GST_CLAIM_DATE=TO_DATE('" & VB6.Format(lblGSTClaimDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " GST_CLAIM='" & IIf(chkGSTClaim.CheckState = System.Windows.Forms.CheckState.Unchecked, "N", lblClaimStatus.Text) & "', " & vbCrLf & " GST_CLAIM_NEW_NO=" & Val(txtModvatNo.Text) & ",  " & vbCrLf & " GST_CLAIM_NEW_DATE=TO_DATE('" & VB6.Format(txtModvatDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " TOTALGSTVALUE=" & Val(CStr(mTotGSTAmount)) & ",  " & vbCrLf & " TOTCGST_REFUNDAMT=" & Val(txtTotCGSTRefund.Text) & ",  " & vbCrLf & " TOTSGST_REFUNDAMT=" & Val(txtTotSGSTRefund.Text) & ", " & vbCrLf & " TOTIGST_REFUNDAMT=" & Val(txtTotIGSTRefund.Text) & ",  " & vbCrLf & " TOTCGST_AMOUNT=" & Val(lblTotCGSTAmount.Text) & ",  " & vbCrLf & " TOTSGST_AMOUNT=" & Val(lblTotSGSTAmount.Text) & ", " & vbCrLf & " TOTIGST_AMOUNT=" & Val(lblTotIGSTAmount.Text) & ",  " & vbCrLf & " SHIPPED_TO_SAMEPARTY='" & mShipTo & "',  " & vbCrLf & " SHIPPED_TO_PARTY_CODE='" & mShipToCode & "', " & vbCrLf

            SqlStr = SqlStr & vbCrLf _
                & " SECTION_CODE=" & IIf(pSectionCode = -1, "NULL", pSectionCode) & ", PURCHASE_TYPE= '" & lblPurchaseType.Text & "'," & vbCrLf _
                & " ADV_VNO = '" & Trim(txtAdvVNo.Text) & "'," & vbCrLf _
                & " ADV_VDATE = TO_DATE('" & VB6.Format(txtAdvDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " ADV_ADJUSTED_AMT = " & Val(txtAdvAdjust.Text) & ", " & vbCrLf _
                & " ADV_CGST_AMT = " & Val(txtAdvCGST.Text) & ", " & vbCrLf _
                & " ADV_SGST_AMT = " & Val(txtAdvSGST.Text) & ", " & vbCrLf _
                & " ADV_IGST_AMT = " & Val(txtAdvIGST.Text) & ", " & vbCrLf _
                & " ADV_ITEM_AMT = " & Val(txtItemAdvAdjust.Text) & ", " & vbCrLf _
                & " REJ_CREDITNOTE = '" & mRejCreditNoteNo & "'," & vbCrLf _
                & " BE_NO = '" & MainClass.AllowSingleQuote(txtBENo.Text) & "',  " & vbCrLf _
                & " BE_DATE = TO_DATE('" & VB6.Format(txtBEDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " BE_AMOUNT = " & Val(txtBEAmount.Text) & ", PURCHASESEQTYPE=" & Val(lblPurchaseSeqType.Text) & "," & vbCrLf _
                & " CUSTOMER_REF_NO = '" & MainClass.AllowSingleQuote(txtCustomerRefNo.Text) & "',"



            SqlStr = SqlStr & vbCrLf _
                & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),DIV_CODE=" & mDivisionCode & ",BILL_TO_LOC_ID='" & MainClass.AllowSingleQuote(txtBillTo.Text) & "', SHIP_TO_LOC_ID='" & MainClass.AllowSingleQuote(mShipToLoc) & "',DELIVERY_TO='" & MainClass.AllowSingleQuote(mDeliveryToCode) & "',DELIVERY_TO_LOC_ID = '" & MainClass.AllowSingleQuote(mDeliveryToLoc) & "' " & vbCrLf _
                & " WHERE Mkey ='" & MainClass.AllowSingleQuote(LblMKey.Text) & "'"
        End If
        PubDBCn.Execute(SqlStr)

        If FinancePVNOMST((LblMKey.Text), Trim(txtVNoPrefix.Text) & Trim(txtVNo.Text), VB6.Format(txtVDate.Text, "DD-MMM-YYYY"), IIf(chkFinalPost.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N"), Val(txtMRRNo.Text)) = False Then GoTo ErrPart

        If UpdateDetail1(mNarration, mAccountCode, mVNo, mSuppCustCode, mShipTo, mShipToCode, mDivisionCode, mSaleBillNo, mSaleBillDate) = False Then GoTo ErrPart



        If UpdateMRRMain((txtMRRNo.Text)) = False Then GoTo ErrPart

        If VB.Left(cboGSTStatus.Text, 1) = "G" And mNewGSTNo = True And (Val(lblTotCGSTAmount.Text) + Val(lblTotSGSTAmount.Text) + Val(lblTotIGSTAmount.Text) > 0) Then ''chkCancelled.Value = vbUnchecked
            If UpdateGSTSeqMaster(PubDBCn, (LblMKey.Text), LblBookCode.Text, mBookType, mBookSubType, mGSTNo, VB6.Format(txtModvatDate.Text, "DD-MMM-YYYY"), mCapital, "N", "G") = False Then GoTo ErrPart
        End If
        If DeletePrevious() = False Then GoTo ErrPart
        pDueDate = txtPaymentdate.Text
        If CDbl(LblBookCode.Text) = ConPurchaseBookCode And chkFOC.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            ''tobecheck
            If RsCompany.Fields("PURCHASE_POSTINGTYPE").Value = "B" Then
                If MainClass.ValidateWithMasterTable(cboInvType.Text, "NAME", "ITEMTYPE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mItemType = MasterNo
                End If
            Else
                SprdMain.Row = 1
                SprdMain.Col = ColItemCode
                mItemCode = Trim(SprdMain.Text)
                mAccountCode = GetItemAccountCode(mItemCode)
                If mAccountCode = "-1" Then MsgBox("Account Code not Defined For Item Code : " & mItemCode) : GoTo ErrPart
                mItemType = GetItemType(mItemCode)
            End If
            mDNCnNO = 0
            mDNCNCreated = False
            xExpDiffDN = False

            If ADDMode = True And chkFOC.CheckState = System.Windows.Forms.CheckState.Unchecked Then

                Dim pDebitNoteNo As String = ""
                Dim pDebitNoteDate As String = ""

                If CheckDebitNote(pDebitNoteNo, pDebitNoteDate, "S") = False Then
                    If IsDebitNoteDeduct(pTempDNCNSeq, VB.Left(ConDebitNote, 1), "S", mVNo, mSuppCustCode, mDivisionCode) = False Then GoTo ErrPart
                End If

                If RsCompany.Fields("REJECTION_DN").Value = "Y" Then
                    If CheckDebitNote(pDebitNoteNo, pDebitNoteDate, "R") = False Then
                        If IsDebitNoteDeduct(pTempDNCNSeq, VB.Left(ConDebitNote, 1), "R", mVNo, mSuppCustCode, mDivisionCode) = False Then GoTo ErrPart
                    End If
                End If

                If lblPurchaseSeqType.Text = "2" Or lblPurchaseSeqType.Text = "8" Then
                    Else
                        If IsDebitNoteDeduct(pTempDNCNSeq, VB.Left(ConDebitNote, 1), "P", mVNo, mSuppCustCode, mDivisionCode) = False Then GoTo ErrPart
                        If IsDebitNoteDeduct(pTempDNCNSeq, VB.Left(ConCreditNote, 1), "P", mVNo, mSuppCustCode, mDivisionCode) = False Then GoTo ErrPart
                    End If

                    If IsDebitNoteDeduct(pTempDNCNSeq, VB.Left(ConDebitNote, 1), "V", mVNo, mSuppCustCode, mDivisionCode) = False Then GoTo ErrPart
                    If IsDebitNoteDeduct(pTempDNCNSeq, VB.Left(ConDebitNote, 1), "O", mVNo, mSuppCustCode, mDivisionCode) = False Then GoTo ErrPart
                    mAccountCode = GetDebitNameOfInvType(Trim(cboInvType.Text), "N")

                    mPDIRItem = GetPDIRItem(Val(txtMRRNo.Text))
                    If mPDIRItem > 0 Then
                        PDIRAmount = mPDIRItem * Val(IIf(IsDBNull(RsCompany.Fields("PDIR_AMOUNT").Value), 0, RsCompany.Fields("PDIR_AMOUNT").Value))
                        If CDbl(lblPurchaseSeqType.Text) = 2 Or CDbl(lblPurchaseSeqType.Text) = 8 Then
                            PDIRAmount = 0
                        End If
                        If PDIRAmount > 0 Then
                            If IsDBNull(RsCompany.Fields("PDIR_CreditAcct").Value) Then
                                MsgBox("PDIR Credit Account Missing, Please Call Administrator....")
                                GoTo ErrPart
                            End If
                            If mDNCNCreated = True Then
                                mDNCnNO = mDNCnNO + 1
                            Else
                                mDNCnNO = 0
                            End If
                            mDNCNCreated = True
                            If UpdateDnCnMain(mVNo, (txtVDate.Text), Trim(txtBillNo.Text), Trim(txtBillDate.Text), Trim(txtMRRNo.Text), (txtMRRDate.Text), (txtPONo.Text), (txtPODate.Text), Val(txtCreditDays(0).Text), Val(txtCreditDays(1).Text), Trim(txtItemType.Text), "O", mCancelled, ConDebitNoteBookCode, VB.Left(ConDebitNote, 1), VB.Right(ConDebitNote, 1), mSuppCustCode, RsCompany.Fields("PDIR_CreditAcct").Value, (txtPaymentdate.Text), "Y", mDNCnNO, False, mDivisionCode, cntRow, PDIRAmount) = False Then GoTo ErrPart
                        End If
                    End If
                End If
            End If
        If CDbl(LblBookCode.Text) = ConPurchaseBookCode Then
            If lblPurchaseSeqType.Text = "2" Or lblPurchaseSeqType.Text = "8" Then     '' mBookSubType = "R" Then
                If Trim(txtPONo.Text) = "" Then
                    mSRBillNo = txtBillNo.Text
                    mSRBillDate = txtBillDate.Text
                Else
                    mSRBillNo = txtBillNo.Text
                    mSRBillDate = txtBillDate.Text
                End If
                Dim mRow As Long



                mMannualAdjustment = IIf(IsDBNull(RsCompany.Fields("MANNUAL_BILL_ADJUST").Value), "N", RsCompany.Fields("MANNUAL_BILL_ADJUST").Value)
                mRow = SprdPaymentDetail.MaxRows
                mBalanceAmount = Val(lblNetAmount.Text)
                If mMannualAdjustment = "Y" Then

                    If mCompanyGSTNo = mPartyGSTNo Then
                        mNetExpAmount = Val(lblTotExpAmt.Text)
                        mItemCGST = 0
                        mItemSGST = 0
                        mItemIGST = 0
                    Else
                        mItemCGST = Val(lblTotCGSTAmount.Text)
                        mItemSGST = Val(lblTotSGSTAmount.Text)
                        mItemIGST = Val(lblTotIGSTAmount.Text)

                        If VB.Left(cboGSTStatus.Text, 1) = "I" Then     ''VB.Left(cboGSTStatus.Text, 1) = "G" Or 
                            mNetExpAmount = Val(lblTotExpAmt.Text) + Val(lblTotCGSTAmount.Text) + Val(lblTotIGSTAmount.Text) + Val(lblTotSGSTAmount.Text)
                        Else
                            mNetExpAmount = Val(lblTotExpAmt.Text)
                        End If
                    End If
                    mFirstRow = True

                    'With SprdPaymentDetail
                    '    For mRow = 1 To SprdPaymentDetail.MaxRows - 1
                    '        .Row = mRow



                    '        .Col = ColPayBillNo
                    '        mSRBillNo = Trim(.Text)

                    '        .Col = ColPayBillDate
                    '        mSRBillDate = Trim(.Text)

                    '        .Col = ColPayPaymentAmt
                    '        xNETVALUE = Val(.Text)
                    '        mBalanceAmount = mBalanceAmount - xNETVALUE

                    '        If SaleReturnPostTRNGSTNew(PubDBCn, (LblMKey.Text), mRow, (LblBookCode.Text), mBookType, mBookSubType, mSubRowNo, mVNo,
                    '            (txtVDate.Text), mSRBillNo, mSRBillDate, mTRNType, mSuppCustCode, mAccountCode, Val(CStr(xNETVALUE)),
                    '            IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, True, False),
                    '            IIf(chkFOC.CheckState = System.Windows.Forms.CheckState.Checked, True, False), pDueDate,
                    '            IIf(chkRejection.CheckState = System.Windows.Forms.CheckState.Checked, True, False), VB.Left(mNarration, 254), (txtRemarks.Text),
                    '            Val(mNetExpAmount), mItemCGST, mItemSGST, mItemIGST, IIf(mIsGSTRefund = "G", "Y", "N"),
                    '            (txtBillNo.Text), (txtBillDate.Text), (txtMRRDate.Text), Val(lblTotItemValue.Text), ADDMode, mAddUser, mAddDate, mLocal, mDivisionCode,
                    '            mFirstRow, txtBillTo.Text, Val(lblPurchaseSeqType.Text)) = False Then GoTo ErrPart

                    '        mFirstRow = False
                    '    Next
                    'End With
                    If mBalanceAmount <> 0 Then
                        xNETVALUE = mBalanceAmount
                        mSRBillNo = mVNo
                        mSRBillDate = txtVDate.Text

                        If SaleReturnPostTRNGSTNew(PubDBCn, (LblMKey.Text), mRow, (LblBookCode.Text), mBookType, mBookSubType, mSubRowNo, mVNo,
                                (txtVDate.Text), mSRBillNo, mSRBillDate, mTRNType, mSuppCustCode, mAccountCode, Val(CStr(xNETVALUE)),
                                IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, True, False),
                                IIf(chkFOC.CheckState = System.Windows.Forms.CheckState.Checked, True, False), pDueDate,
                                IIf(chkRejection.CheckState = System.Windows.Forms.CheckState.Checked, True, False), VB.Left(mNarration, 254), (txtRemarks.Text),
                                Val(mNetExpAmount), mItemCGST, mItemSGST, mItemIGST, IIf(mIsGSTRefund = "G", "Y", "N"),
                                (txtBillNo.Text), (txtBillDate.Text), (txtMRRDate.Text), Val(lblTotItemValue.Text), ADDMode, mAddUser, mAddDate, mLocal, mDivisionCode,
                                mFirstRow, txtBillTo.Text, Val(lblPurchaseSeqType.Text)) = False Then GoTo ErrPart
                    End If

                Else

                    SqlStr = "SELECT  CUST_REF_NO, CUST_REF_DATE, " & vbCrLf _
                                & " SUM(ITEM_AMT) AS ITEM_AMT, SUM(CGST_AMOUNT) AS CGST_AMOUNT,SUM(SGST_AMOUNT) AS SGST_AMOUNT, SUM(IGST_AMOUNT) AS IGST_AMOUNT" & vbCrLf _
                                & " FROM FIN_PURCHASE_DET " & vbCrLf _
                                & " WHERE MKEY = '" & (LblMKey.Text) & "'" & vbCrLf _
                                & " GROUP BY CUST_REF_NO, CUST_REF_DATE"

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPostSRTrn, ADODB.LockTypeEnum.adLockReadOnly)
                    If RsPostSRTrn.EOF = False Then
                        mFirstRow = True
                        mSubRowNo = 0
                        Do While RsPostSRTrn.EOF = False
                            mSubRowNo = mSubRowNo + 1

                            mSRBillNo = IIf(IsDBNull(RsPostSRTrn.Fields("CUST_REF_NO").Value), "", RsPostSRTrn.Fields("CUST_REF_NO").Value)
                            mSRBillDate = IIf(IsDBNull(RsPostSRTrn.Fields("CUST_REF_DATE").Value), "", RsPostSRTrn.Fields("CUST_REF_DATE").Value) '' GetSaleInvoiceDate(1, 0, mSRBillNo, "", PubDBCn) '


                            xItemValue = IIf(IsDBNull(RsPostSRTrn.Fields("ITEM_AMT").Value), 0, RsPostSRTrn.Fields("ITEM_AMT").Value)
                            xTOTEXPAMT = 0
                            xTotED = 0
                            xTotST = 0
                            xModvatAmount = 0
                            xCESSAmount = 0
                            xSHECAmount = 0
                            xServiceAmount = 0
                            xEDUAmount = 0
                            xSHECAmount = 0
                            xSTClaimAmount = 0
                            xNETVALUE = 0
                            xSurOnVat = 0
                            xSurcharge = 0
                            mItemCGST = 0
                            mItemSGST = 0
                            mItemIGST = 0
                            If mItemValue <> 0 Then
                                xTOTEXPAMT = Val(lblTotExpAmt.Text) * xItemValue / mItemValue
                                xTotED = 0
                                xTotST = 0
                                xModvatAmount = 0
                                xCESSAmount = 0
                                xSHECAmount = 0
                                xServiceAmount = 0
                                xEDUAmount = 0
                                xSHEC = 0
                                xSTClaimAmount = 0
                                xSurOnVat = 0
                                xSurcharge = 0
                                xNETVALUE = Val(CStr(mNETVALUE)) * xItemValue / mItemValue
                                If mCompanyGSTNo = mPartyGSTNo Then
                                    mItemCGST = 0
                                    mItemSGST = 0
                                    mItemIGST = 0
                                Else
                                    mItemCGST = IIf(IsDBNull(RsPostSRTrn.Fields("CGST_AMOUNT").Value), 0, RsPostSRTrn.Fields("CGST_AMOUNT").Value) 'Val(txtTotCGSTRefund.Text) * xItemValue / mItemValue
                                    mItemSGST = IIf(IsDBNull(RsPostSRTrn.Fields("SGST_AMOUNT").Value), 0, RsPostSRTrn.Fields("SGST_AMOUNT").Value) 'Val(txtTotSGSTRefund.Text) * xItemValue / mItemValue
                                    mItemIGST = IIf(IsDBNull(RsPostSRTrn.Fields("IGST_AMOUNT").Value), 0, RsPostSRTrn.Fields("IGST_AMOUNT").Value) 'Val(txtTotIGSTRefund.Text) * xItemValue / mItemValue
                                End If
                                If VB.Left(cboGSTStatus.Text, 1) = "I" Then
                                    xTOTEXPAMT = Val(CStr(xTOTEXPAMT)) + Val(CStr(mItemCGST)) + Val(CStr(mItemSGST)) + Val(CStr(mItemIGST))
                                Else
                                    xTOTEXPAMT = Val(CStr(xTOTEXPAMT))
                                End If
                            End If
                            mSRBillNo = IIf(mSRBillNo = "", txtBillNo.Text, mSRBillNo)
                            mSRBillDate = IIf(mSRBillDate = "", txtBillDate.Text, mSRBillDate)

                            mSRBillNo1 = txtBillNo.Text & IIf(txtCustomerRefNo.Text = "", "", "/" & txtCustomerRefNo.Text)
                            mSRBillDate1 = txtBillDate.Text

                            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
                                pNewVNO = IIf(txtCustomerRefNo.Text = "", mVNo, txtCustomerRefNo.Text)
                                mSRBillNo = IIf(txtCustomerRefNo.Text = "", mSRBillNo, txtCustomerRefNo.Text)
                            Else
                                pNewVNO = mVNo
                            End If

                            If SaleReturnPostTRNGST(PubDBCn, (LblMKey.Text), mCurRowNo, (LblBookCode.Text), mBookType, mBookSubType, mSubRowNo, pNewVNO,
                                                        (txtVDate.Text), mSRBillNo, mSRBillDate, mTRNType, mSuppCustCode, mAccountCode, Val(CStr(xNETVALUE)),
                                                        IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, True, False),
                                                        IIf(chkFOC.CheckState = System.Windows.Forms.CheckState.Checked, True, False), pDueDate,
                                                        IIf(chkRejection.CheckState = System.Windows.Forms.CheckState.Checked, True, False), VB.Left(mNarration, 254),
                                                        (txtRemarks.Text), Val(CStr(xTOTEXPAMT)), Val(CStr(mItemCGST)), Val(CStr(mItemSGST)), Val(CStr(mItemIGST)),
                                                        IIf(mIsGSTRefund = "G", "Y", "N"), mSRBillNo1, mSRBillDate1, (txtMRRDate.Text), Val(CStr(xItemValue)),
                                                        ADDMode, mAddUser, mAddDate, mLocal, mDivisionCode, mFirstRow, txtBillTo.Text, Val(lblPurchaseSeqType.Text)) = False Then GoTo ErrPart
                            RsPostSRTrn.MoveNext()
                            mFirstRow = False
                        Loop
                    End If
                End If
            Else
                If mCompanyGSTNo = mPartyGSTNo Then
                    mNetExpAmount = Val(lblTotExpAmt.Text)
                Else
                    If VB.Left(cboGSTStatus.Text, 1) = "G" Or VB.Left(cboGSTStatus.Text, 1) = "I" Then
                        mNetExpAmount = Val(lblTotExpAmt.Text) + Val(lblTotCGSTAmount.Text) + Val(lblTotIGSTAmount.Text) + Val(lblTotSGSTAmount.Text)
                    Else
                        mNetExpAmount = Val(lblTotExpAmt.Text)
                    End If
                End If

                If PurchasePostTRNGST(PubDBCn, (LblMKey.Text), mCurRowNo, (LblBookCode.Text), mBookType, mBookSubType, mVNo, (txtVDate.Text), (txtBillNo.Text), (txtBillDate.Text), mTRNType, mSuppCustCode, mAccountCode, Val(CStr(mItemValue)), Val(lblNetAmount.Text), IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, True, False), IIf(chkFOC.CheckState = System.Windows.Forms.CheckState.Checked, True, False), pDueDate, VB.Left(mNarration, 254), (txtRemarks.Text), mNetExpAmount, IIf(mIsGSTRefund = "G", "Y", "N"), Val(txtTotCGSTRefund.Text), Val(txtTotSGSTRefund.Text), Val(txtTotIGSTRefund.Text), (txtMRRDate.Text), ADDMode, mAddUser, mAddDate, mDivisionCode, IIf(mIsGSTRefund = "R", "Y", "N"), Val(lblTotCGSTAmount.Text) + Val(lblTotIGSTAmount.Text) + Val(lblTotSGSTAmount.Text), Val(lblTotCGSTAmount.Text), Val(lblTotSGSTAmount.Text), Val(lblTotIGSTAmount.Text), mSaleBillNo, mSaleBillDate, txtBillTo.Text) = False Then GoTo ErrPart
            End If
            'If ADDMode = True Then

            If IIf(IsDBNull(RsCompany.Fields("MANNUAL_BILL_ADJUST").Value), "N", RsCompany.Fields("MANNUAL_BILL_ADJUST").Value) = "Y" Then
                If UpdatePaymentDetail1(PubDBCn, (LblMKey.Text), mCurRowNo, (LblBookCode.Text), mBookType, mBookSubType, mVNo, (txtVDate.Text), (txtBillNo.Text), (txtBillDate.Text), mTRNType, mSuppCustCode, mAccountCode, Val(CStr(mItemValue)), Val(lblNetAmount.Text), IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, True, False), IIf(chkFOC.CheckState = System.Windows.Forms.CheckState.Checked, True, False), pDueDate, VB.Left(mNarration, 254), (txtRemarks.Text), mNetExpAmount, IIf(mIsGSTRefund = "G", "Y", "N"), Val(txtTotCGSTRefund.Text), Val(txtTotSGSTRefund.Text), Val(txtTotIGSTRefund.Text), (txtMRRDate.Text), ADDMode, mAddUser, mAddDate, mDivisionCode, IIf(mIsGSTRefund = "R", "Y", "N"), Val(lblTotCGSTAmount.Text) + Val(lblTotIGSTAmount.Text) + Val(lblTotSGSTAmount.Text), Val(lblTotCGSTAmount.Text), Val(lblTotSGSTAmount.Text), Val(lblTotIGSTAmount.Text), mSaleBillNo, mSaleBillDate, txtBillTo.Text) = False Then GoTo ErrPart
            End If

            If chkTDS.CheckState = System.Windows.Forms.CheckState.Checked Or chkESI.CheckState = System.Windows.Forms.CheckState.Checked Or ChkSTDS.CheckState = System.Windows.Forms.CheckState.Checked Then
                    pJVTMKey = lblJVTMKey.Text
                    If Val(txtTDSAmount.Text) + Val(txtESIAmount.Text) + Val(txtSTDSAmount.Text) > 0 Then
                        If UpdateTDSVoucher(mDivisionCode, pJVTMKey) = False Then GoTo ErrPart
                        SqlStr = "UPDATE FIN_PURCHASE_HDR SET JVNO='" & txtJVVNO.Text & "', " & vbCrLf _
                            & " JVT_MKEY='" & pJVTMKey & "'," & vbCrLf _
                            & " UPDATE_FROM='N'," & vbCrLf _
                            & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
                            & " WHERE MKEY='" & LblMKey.Text & "'"
                        PubDBCn.Execute(SqlStr)
                    End If
                End If
                'End If

                If MODIFYMode = True And chkFinalPost.CheckState = System.Windows.Forms.CheckState.Checked Then
                    SqlStr = "UPDATE FIN_DNCN_HDR SET PURVNO='" & Trim(mVNo) & "', PURVDATE=TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " UPDATE_FROM='N'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & " AND DNCNFROM IN ('P','M')"
                    PubDBCn.Execute(SqlStr)
                    SqlStr = " UPDATE FIN_DNCN_DET SET PURMKEY='" & LblMKey.Text & "', PURVNO='" & Trim(mVNo) & "', PURVDATE=TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND MRR_REF_NO=" & Val(txtMRRNo.Text) & " AND MKEY IN (" & vbCrLf & " SELECT MKEY FROM FIN_DNCN_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DNCNFROM IN ('P','M'))"
                    PubDBCn.Execute(SqlStr)
                    SqlStr = "UPDATE FIN_DNCN_HDR SET " & vbCrLf & " UPDATE_FROM='N'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DNCNFROM IN ('P','M') AND MKEY IN (" & vbCrLf & " SELECT MKEY FROM FIN_DNCN_DET" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND MRR_REF_NO=" & Val(txtMRRNo.Text) & ")"
                    PubDBCn.Execute(SqlStr)
                End If
            End If
            PubDBCn.CommitTrans()
        UpdateMain1 = True
        If Trim(pDnCnNo) <> "" Then
            MsgBox(pDnCnNo & " Created. ", MsgBoxStyle.Information)
        End If
        If ADDMode = True And Trim(txtJVVNO.Text) <> "" Then
            MsgBox("TDS Journal Voucher No. " & txtJVVNO.Text & " Created. ", MsgBoxStyle.Information)
        End If
        Exit Function
ErrPart:
        '    Resume
        UpdateMain1 = False
        PubDBCn.RollbackTrans() ''
        RsPurchMain.Requery() ''.Refresh
        RsPurchDetail.Requery() ''.Refresh
        If ADDMode = True Then
            txtVNo.Text = ""
        End If
        If Err.Description = "" Then Exit Function
        If Err.Number = -2147217900 Then
            ErrorMsg("Duplicate Invoice No. Generated, Save Again", "Duplicate", MsgBoxStyle.Critical)
        Else
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        '    Resume
    End Function
    Public Function AutoCreditNoteNo() As String

        On Error GoTo AutoGenSeqNoErr
        Dim RsMRRMainGen As ADODB.Recordset = Nothing
        Dim mNewSeqNo As Integer
        Dim SqlStr As String = ""
        Dim mMaxValue As String
        Dim mPreFix As String
        Dim mPrefixLen As Long

        mPreFix = GetDocumentPrefix("S", "R")

        mPrefixLen = IIf(Trim(mPreFix) = "", 0, Len(Trim(mPreFix)))
        SqlStr = ""
        ''select BILLNO, NVL(LENGTH(BILLNOPREFIX),0), LENGTH(BILLNO),SUBSTR(REJ_CREDITNOTE,NVL(LENGTH(BILLNOPREFIX),0)+1,LENGTH(REJ_CREDITNOTE)-NVL(LENGTH(BILLNOPREFIX),0)),


        'SqlStr = "SELECT Max(TO_NUMBER(SUBSTR(REJ_CREDITNOTE," & mPrefixLen + 1 & ",LENGTH(REJ_CREDITNOTE)-" & mPrefixLen & "))) AS MaxNo " & vbCrLf _
        '    & " FROM FIN_PURCHASE_HDR " & vbCrLf _
        '    & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
        '    & " AND FYEAR =" & RsCompany.Fields("FYEAR").Value & ""

        'SqlStr = SqlStr & vbCrLf & " AND PURCHASESEQTYPE=2"

        'If mPreFix <> "" Then
        '    SqlStr = SqlStr & vbCrLf & " AND SUBSTR(REJ_CREDITNOTE,1," & mPrefixLen & ")='" & mPreFix & "'"
        'End If

        SqlStr = "SELECT MAX(MaxNo)  AS MaxNo FROM ("

        SqlStr = SqlStr & vbCrLf _
            & " Select Max(TO_NUMBER(SUBSTR(REJ_CREDITNOTE," & mPrefixLen + 1 & ",LENGTH(REJ_CREDITNOTE)-" & mPrefixLen & "))) As MaxNo " & vbCrLf _
            & " FROM FIN_PURCHASE_HDR " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " And FYEAR =" & RsCompany.Fields("FYEAR").Value & ""

        SqlStr = SqlStr & vbCrLf & " And PURCHASESEQTYPE=2"

        If mPreFix <> "" Then
            SqlStr = SqlStr & vbCrLf & " And SUBSTR(REJ_CREDITNOTE,1," & mPrefixLen & ")='" & mPreFix & "'"
        End If

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 103 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
            SqlStr = SqlStr & vbCrLf & " UNION ALL"

            SqlStr = SqlStr & vbCrLf _
                & "SELECT Max(TO_NUMBER(SUBSTR(PARTY_DNCN_NO," & mPrefixLen + 1 & ",LENGTH(PARTY_DNCN_NO)-" & mPrefixLen & "))) AS MaxNo " & vbCrLf _
                & " FROM FIN_SUPP_SALE_HDR " & vbCrLf _
                & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND FYEAR =" & RsCompany.Fields("FYEAR").Value & ""

            If mPreFix <> "" Then
                SqlStr = SqlStr & vbCrLf & " AND SUBSTR(PARTY_DNCN_NO,1," & mPrefixLen & ")='" & mPreFix & "'"
            End If
        End If

        SqlStr = SqlStr & vbCrLf & " )"

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
        'mNewSeqNo = CDbl(VB6.Format(mNewSeqNo, ConBillFormat))
        If mPreFix = "" Then
            AutoCreditNoteNo = mNewSeqNo
        Else
            AutoCreditNoteNo = mPreFix & VB6.Format(mNewSeqNo, ConBillFormat)
        End If
        ''& VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        Exit Function
AutoGenSeqNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function UpdateTempDNCNTable(ByRef pTempDNCNSeq As Double, ByRef pSameGSTNo As String) As Boolean
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mSqlStr As String
        Dim mBookType As String
        Dim mSubBookType As String
        Dim mSubRowNo As Integer
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mItemQty As Double
        Dim mItemUOM As String
        Dim mItemRate As Double
        Dim xItemRate As Double
        Dim mItemAmount As Double
        Dim mItemED As Double
        Dim mItemST As Double
        Dim mMRRRefNo As Double
        Dim mMRRDate As String
        Dim mSuppRefNo As String
        Dim mSuppRefDate As String
        Dim mSuppPoNo As String
        Dim mPORate As Double
        Dim mMrrRefType As String
        Dim mExpName As String
        Dim mEDAmount As Double
        Dim mEDPer As Double
        Dim mSTAmount As Double
        Dim mSTPer As Double
        Dim mAssessableValue As Double
        Dim mTaxableValue As Double
        Dim mAccountCode As String = ""

        Dim mExpAccountCode As String = ""

        Dim I As Integer
        Dim mCGSTPer As Double
        Dim mSGSTPer As Double
        Dim mIGSTPer As Double
        Dim mCGSTAmount As Double
        Dim mSGSTAmount As Double
        Dim mIGSTAmount As Double
        Dim mHSNCode As String
        Dim mOtherDebitAmount As Double

        PubDBCn.Execute("DELETE FROM TEMP_FIN_DNCN_DET Where AUTO_GEN_REFNO=" & pTempDNCNSeq & "")
        mEDAmount = 0
        mSTAmount = 0
        mAssessableValue = Val(lblTotItemValue.Text)
        mTaxableValue = Val(lblTotItemValue.Text)
        mOtherDebitAmount = 0
        For I = 1 To SprdExp.MaxRows
            SprdExp.Row = I
            SprdExp.Col = ColExpName
            mExpName = Trim(SprdExp.Text)
            If MainClass.ValidateWithMasterTable(mExpName, "Name", "Code", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EXCISEABLE='Y'") = True Then
                SprdExp.Row = I
                SprdExp.Col = ColExpAmt
                mAssessableValue = mAssessableValue + Val(SprdExp.Text)
            End If
            If MainClass.ValidateWithMasterTable(mExpName, "Name", "Code", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TAXABLE='Y'") = True Then
                SprdExp.Row = I
                SprdExp.Col = ColExpAmt
                mTaxableValue = mTaxableValue + Val(SprdExp.Text)
            End If
            If MainClass.ValidateWithMasterTable(mExpName, "Name", "Code", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IDENTIFICATION='ED'") = True Then
                SprdExp.Row = I
                SprdExp.Col = ColExpAmt
                mEDAmount = mEDAmount + Val(SprdExp.Text)
            End If
            If MainClass.ValidateWithMasterTable(mExpName, "Name", "Code", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IDENTIFICATION='ST'") = True Then
                SprdExp.Row = I
                SprdExp.Col = ColExpAmt
                mSTAmount = mSTAmount + Val(SprdExp.Text)
            End If
            SprdExp.Row = I
            SprdExp.Col = ColExpDebitAmt
            mOtherDebitAmount = mOtherDebitAmount + IIf(Val(SprdExp.Text) > 0, Val(SprdExp.Text), 0)

            If Val(SprdExp.Text) > 0 And mExpAccountCode = "" Then

                If MainClass.ValidateWithMasterTable(mExpName, "Name", "PURCHASEPOSTCODE", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    If Trim(MasterNo) <> "" Then
                        mExpAccountCode = Trim(MasterNo)
                    End If
                End If
            End If
        Next

        If Val(CStr(mAssessableValue)) <> 0 Then
            mEDPer = CDbl(VB6.Format(mEDAmount * 100 / Val(CStr(mAssessableValue)), "0.00"))
        End If
        If Val(CStr(mTaxableValue)) <> 0 Then
            mSTPer = CDbl(VB6.Format(mSTAmount * 100 / Val(CStr(mTaxableValue)), "0.00"))
        End If
        mSubRowNo = 0
        mMRRRefNo = Val(txtMRRNo.Text)
        mMRRDate = VB6.Format(txtMRRDate.Text, "DD/MM/YYYY")
        mSuppRefNo = Trim(txtBillNo.Text)
        mSuppRefDate = VB6.Format(txtBillDate.Text, "DD/MM/YYYY")
        mMrrRefType = GetMrrRefNo(Val(txtMRRNo.Text))
        With SprdMain
            mBookType = VB.Left(ConDebitNote, 1)
            mSubBookType = "S"
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)
                .Col = ColItemDesc
                mItemDesc = Trim(.Text)
                .Col = ColHSN
                mHSNCode = Trim(.Text)
                .Col = ColShortageQty
                mItemQty = Val(.Text)
                .Col = ColUnit
                mItemUOM = Trim(.Text)
                .Col = ColRate
                mItemRate = Val(.Text)
                mItemAmount = CDbl(VB6.Format(mItemQty * mItemRate, "0.0000"))
                .Col = ColPONo
                mSuppPoNo = Trim(.Text)
                .Col = ColPORate
                mPORate = Val(.Text)
                ''GST Not Required .. 27/10/2018
                '            mCGSTPer = 0
                '            mSGSTPer = 0
                '            mIGSTPer = 0
                '            mCGSTAmount = 0
                '            mSGSTAmount = 0
                '            mIGSTAmount = 0
                '' Reverse Comments as on 05/11/2018
                If pSameGSTNo = "Y" Then
                    mCGSTPer = 0
                    mSGSTPer = 0
                    mIGSTPer = 0
                    mCGSTAmount = 0
                    mSGSTAmount = 0
                    mIGSTAmount = 0
                Else
                    If VB.Left(cboGSTStatus.Text, 1) = "G" Or VB.Left(cboGSTStatus.Text, 1) = "I" Then
                        .Col = ColCGSTPer
                        mCGSTPer = Val(.Text)
                        .Col = ColSGSTPer
                        mSGSTPer = Val(.Text)
                        .Col = ColIGSTPer
                        mIGSTPer = Val(.Text)
                        mCGSTAmount = System.Math.Round(Val(CStr(mItemAmount * mCGSTPer * 0.01)), 2)
                        mSGSTAmount = System.Math.Round(Val(CStr(mItemAmount * mSGSTPer * 0.01)), 2)
                        mIGSTAmount = System.Math.Round(Val(CStr(mItemAmount * mIGSTPer * 0.01)), 2)
                    Else
                        mCGSTPer = 0
                        mSGSTPer = 0
                        mIGSTPer = 0
                        mCGSTAmount = 0
                        mSGSTAmount = 0
                        mIGSTAmount = 0
                    End If
                End If
                If mItemQty > 0 Then
                    .Col = ColInvType
                    If Trim(.Text) = "" Then
                        mAccountCode = GetDebitNameOfInvType(Trim(cboInvType.Text), "N")
                    Else
                        mAccountCode = GetDebitNameOfInvType(Trim(.Text), "N")
                    End If
                    mItemED = mItemAmount * mEDPer * 0.01
                    mItemST = mItemAmount * mSTPer * 0.01
                    mSubRowNo = mSubRowNo + 1

                    mSqlStr = " INSERT INTO TEMP_FIN_DNCN_DET " & vbCrLf _
                        & " ( " & vbCrLf _
                        & " AUTO_GEN_REFNO, DNCN_BOOKTYPE, DNCN_BOOKSUBTYPE, " & vbCrLf _
                        & " SUBROWNO, ITEM_CODE, ITEM_DESC, " & vbCrLf _
                        & " ITEM_QTY, ITEM_UOM, ITEM_RATE, " & vbCrLf _
                        & " ITEM_AMT, ITEM_ED, ITEM_ST, " & vbCrLf _
                        & " MRR_REF_NO, MRR_REF_DATE, SUPP_REF_NO, " & vbCrLf _
                        & " SUPP_REF_DATE, REF_PO_NO, COMPANY_CODE, " & vbCrLf _
                        & " PURMKEY, PURVNO, PURVDATE, " & vbCrLf _
                        & " DNCN_REF_NO, DNCN_REF_DATE, PO_RATE, " & vbCrLf _
                        & " MRR_REF_TYPE, ACCOUNT_POST_CODE, " & vbCrLf _
                        & " CGST_PER, SGST_PER, IGST_PER, " & vbCrLf _
                        & " CGST_AMOUNT, SGST_AMOUNT, IGST_AMOUNT, HSNCODE " & vbCrLf _
                        & " ) VALUES ("

                    mSqlStr = mSqlStr & vbCrLf _
                        & " " & pTempDNCNSeq & ", '" & mBookType & "', '" & mSubBookType & "', " & vbCrLf _
                        & " " & mSubRowNo & ", '" & mItemCode & "', '" & MainClass.AllowSingleQuote(mItemDesc) & "', " & vbCrLf _
                        & " " & mItemQty & ", '" & mItemUOM & "', " & mItemRate & ", " & vbCrLf _
                        & " " & mItemAmount & ", " & mItemED & ", " & mItemST & ", " & vbCrLf _
                        & " " & mMRRRefNo & ", To_DATE('" & VB6.Format(mMRRDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(mSuppRefNo) & "'," & vbCrLf _
                        & " TO_DATE('" & VB6.Format(mSuppRefDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & mSuppPoNo & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                        & " '', '', ''," & vbCrLf _
                        & " '', '', " & mPORate & ", " & vbCrLf _
                        & " '" & mMrrRefType & "','" & mAccountCode & "', " & vbCrLf _
                        & " " & mCGSTPer & ", " & mSGSTPer & ", " & mIGSTPer & ", " & vbCrLf _
                        & " " & mCGSTAmount & ", " & mSGSTAmount & ", " & mIGSTAmount & ",'" & mHSNCode & "' " & vbCrLf _
                        & " ) "

                    PubDBCn.Execute(mSqlStr)
                End If
            Next
        End With
        With SprdMain
            mBookType = VB.Left(ConDebitNote, 1)
            mSubBookType = "R"
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)
                .Col = ColItemDesc
                mItemDesc = Trim(.Text)
                .Col = ColHSN
                mHSNCode = Trim(.Text)
                .Col = ColRejectedQty
                mItemQty = Val(.Text)
                .Col = ColUnit
                mItemUOM = Trim(.Text)
                .Col = ColRate
                mItemRate = Val(.Text)
                mItemAmount = CDbl(VB6.Format(mItemQty * mItemRate, "0.0000"))
                .Col = ColPONo
                mSuppPoNo = Trim(.Text)
                .Col = ColPORate
                mPORate = Val(.Text)
                If pSameGSTNo = "Y" Then
                    mCGSTPer = 0
                    mSGSTPer = 0
                    mIGSTPer = 0
                    mCGSTAmount = 0
                    mSGSTAmount = 0
                    mIGSTAmount = 0
                Else
                    If VB.Left(cboGSTStatus.Text, 1) = "G" Or VB.Left(cboGSTStatus.Text, 1) = "I" Then
                        .Col = ColCGSTPer
                        mCGSTPer = Val(.Text)
                        .Col = ColSGSTPer
                        mSGSTPer = Val(.Text)
                        .Col = ColIGSTPer
                        mIGSTPer = Val(.Text)
                        mCGSTAmount = System.Math.Round(Val(CStr(mItemAmount * mCGSTPer * 0.01)), 2)
                        mSGSTAmount = System.Math.Round(Val(CStr(mItemAmount * mSGSTPer * 0.01)), 2)
                        mIGSTAmount = System.Math.Round(Val(CStr(mItemAmount * mIGSTPer * 0.01)), 2)
                    Else
                        mCGSTPer = 0
                        mSGSTPer = 0
                        mIGSTPer = 0
                        mCGSTAmount = 0
                        mSGSTAmount = 0
                        mIGSTAmount = 0
                    End If
                End If
                If mItemQty > 0 Then
                    .Col = ColInvType
                    If Trim(.Text) = "" Then
                        mAccountCode = GetDebitNameOfInvType(Trim(cboInvType.Text), "N")
                    Else
                        mAccountCode = GetDebitNameOfInvType(Trim(.Text), "N")
                    End If
                    mItemED = mItemAmount * mEDPer * 0.01
                    mItemST = mItemAmount * mSTPer * 0.01
                    mSubRowNo = mSubRowNo + 1
                    mSqlStr = " INSERT INTO TEMP_FIN_DNCN_DET " & vbCrLf _
                        & " ( " & vbCrLf _
                        & " AUTO_GEN_REFNO, DNCN_BOOKTYPE, DNCN_BOOKSUBTYPE, " & vbCrLf _
                        & " SUBROWNO, ITEM_CODE, ITEM_DESC, " & vbCrLf _
                        & " ITEM_QTY, ITEM_UOM, ITEM_RATE, " & vbCrLf _
                        & " ITEM_AMT, ITEM_ED, ITEM_ST, " & vbCrLf _
                        & " MRR_REF_NO, MRR_REF_DATE, SUPP_REF_NO, " & vbCrLf _
                        & " SUPP_REF_DATE, REF_PO_NO, COMPANY_CODE, " & vbCrLf _
                        & " PURMKEY, PURVNO, PURVDATE, " & vbCrLf _
                        & " DNCN_REF_NO, DNCN_REF_DATE, PO_RATE, " & vbCrLf _
                        & " MRR_REF_TYPE, ACCOUNT_POST_CODE," & vbCrLf _
                        & " CGST_PER, SGST_PER, IGST_PER, " & vbCrLf _
                        & " CGST_AMOUNT, SGST_AMOUNT, IGST_AMOUNT, HSNCODE " & vbCrLf _
                        & " ) VALUES ("
                    mSqlStr = mSqlStr & vbCrLf & " " & pTempDNCNSeq & ", '" & mBookType & "', '" & mSubBookType & "', " & vbCrLf & " " & mSubRowNo & ", '" & mItemCode & "', '" & MainClass.AllowSingleQuote(mItemDesc) & "', " & vbCrLf & " " & mItemQty & ", '" & mItemUOM & "', " & mItemRate & ", " & vbCrLf & " " & mItemAmount & ", " & mItemED & ", " & mItemST & ", " & vbCrLf & " " & mMRRRefNo & ", TO_DATE('" & VB6.Format(mMRRDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(mSuppRefNo) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(mSuppRefDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & mSuppPoNo & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " '', '', ''," & vbCrLf & " '', '', " & mPORate & ", " & vbCrLf & " '" & mMrrRefType & "','" & mAccountCode & "'," & vbCrLf & " " & mCGSTPer & ", " & mSGSTPer & ", " & mIGSTPer & ", " & vbCrLf & " " & mCGSTAmount & ", " & mSGSTAmount & ", " & mIGSTAmount & ",'" & mHSNCode & "' " & vbCrLf & " ) "
                    PubDBCn.Execute(mSqlStr)
                End If
            Next
        End With
        With SprdMain
            mSubBookType = "P"
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)
                .Col = ColItemDesc
                mItemDesc = Trim(.Text)
                .Col = ColHSN
                mHSNCode = Trim(.Text)
                .Col = ColQty
                mItemQty = Val(.Text)
                .Col = ColShortageQty
                mItemQty = mItemQty - Val(.Text)
                .Col = ColRejectedQty
                mItemQty = mItemQty - Val(.Text)
                .Col = ColUnit
                mItemUOM = Trim(.Text)
                .Col = ColRate
                mItemRate = Val(.Text)
                .Col = ColPONo
                mSuppPoNo = Trim(.Text)
                .Col = ColPORate
                mPORate = Val(.Text)
                ''GST Not Required .. 27/10/2018
                mCGSTPer = 0
                mSGSTPer = 0
                mIGSTPer = 0
                mCGSTAmount = 0
                mSGSTAmount = 0
                mIGSTAmount = 0
                If pSameGSTNo = "Y" Then
                    mCGSTPer = 0
                    mSGSTPer = 0
                    mIGSTPer = 0
                    mCGSTAmount = 0
                    mSGSTAmount = 0
                    mIGSTAmount = 0
                Else
                    If VB.Left(cboGSTStatus.Text, 1) = "G" Or VB.Left(cboGSTStatus.Text, 1) = "I" Then
                        .Col = ColCGSTPer
                        mCGSTPer = Val(.Text)
                        .Col = ColSGSTPer
                        mSGSTPer = Val(.Text)
                        .Col = ColIGSTPer
                        mIGSTPer = Val(.Text)
                        mCGSTAmount = System.Math.Round(Val(CStr(mItemAmount * mCGSTPer * 0.01)), 2)
                        mSGSTAmount = System.Math.Round(Val(CStr(mItemAmount * mSGSTPer * 0.01)), 2)
                        mIGSTAmount = System.Math.Round(Val(CStr(mItemAmount * mIGSTPer * 0.01)), 2)
                    Else
                        mCGSTPer = 0
                        mSGSTPer = 0
                        mIGSTPer = 0
                        mCGSTAmount = 0
                        mSGSTAmount = 0
                        mIGSTAmount = 0
                    End If
                End If
                If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 103 Then
                    mPORate = VB6.Format(mPORate, "0.00")
                    mItemRate = VB6.Format(mItemRate, "0.00")
                End If

                If mPORate - mItemRate <> 0 And Val(mItemQty * System.Math.Abs(mPORate - mItemRate)) > 1 Then
                    mItemRate = mPORate - mItemRate
                    If mItemRate > 0 Then
                        mBookType = VB.Left(ConCreditNote, 1)
                    Else
                        mBookType = VB.Left(ConDebitNote, 1)
                        mItemRate = mItemRate * -1
                    End If
                    mItemAmount = CDbl(VB6.Format(mItemQty * mItemRate, "0.0000"))
                    .Col = ColInvType
                    If Trim(.Text) = "" Then
                        mAccountCode = GetDebitNameOfInvType(Trim(cboInvType.Text), "N")
                    Else
                        mAccountCode = GetDebitNameOfInvType(Trim(.Text), "N")
                    End If
                    mItemED = mItemAmount * mEDPer * 0.01
                    mItemST = mItemAmount * mSTPer * 0.01
                    mCGSTAmount = System.Math.Round(Val(CStr(mItemAmount * mCGSTPer * 0.01)), 2)
                    mSGSTAmount = System.Math.Round(Val(CStr(mItemAmount * mSGSTPer * 0.01)), 2)
                    mIGSTAmount = System.Math.Round(Val(CStr(mItemAmount * mIGSTPer * 0.01)), 2)
                    mSubRowNo = mSubRowNo + 1
                    mSqlStr = " INSERT INTO TEMP_FIN_DNCN_DET " & vbCrLf _
                        & " ( " & vbCrLf _
                        & " AUTO_GEN_REFNO, DNCN_BOOKTYPE, DNCN_BOOKSUBTYPE, " & vbCrLf _
                        & " SUBROWNO, ITEM_CODE, ITEM_DESC, " & vbCrLf _
                        & " ITEM_QTY, ITEM_UOM, ITEM_RATE, " & vbCrLf _
                        & " ITEM_AMT, ITEM_ED, ITEM_ST, " & vbCrLf _
                        & " MRR_REF_NO, MRR_REF_DATE, SUPP_REF_NO, " & vbCrLf _
                        & " SUPP_REF_DATE, REF_PO_NO, COMPANY_CODE, " & vbCrLf _
                        & " PURMKEY, PURVNO, PURVDATE, " & vbCrLf _
                        & " DNCN_REF_NO, DNCN_REF_DATE, PO_RATE, " & vbCrLf _
                        & " MRR_REF_TYPE, ACCOUNT_POST_CODE, " & vbCrLf _
                        & " CGST_PER, SGST_PER, IGST_PER, " & vbCrLf _
                        & " CGST_AMOUNT, SGST_AMOUNT, IGST_AMOUNT, HSNCODE " & vbCrLf _
                        & " ) VALUES ("

                    mSqlStr = mSqlStr & vbCrLf _
                        & " " & pTempDNCNSeq & ", '" & mBookType & "', '" & mSubBookType & "', " & vbCrLf _
                        & " " & mSubRowNo & ", '" & mItemCode & "', '" & MainClass.AllowSingleQuote(mItemDesc) & "', " & vbCrLf _
                        & " " & mItemQty & ", '" & mItemUOM & "', " & mItemRate & ", " & vbCrLf _
                        & " " & mItemAmount & ", " & mItemED & ", " & mItemST & ", " & vbCrLf _
                        & " " & mMRRRefNo & ", TO_DATE('" & VB6.Format(mMRRDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(mSuppRefNo) & "'," & vbCrLf _
                        & " TO_DATE('" & VB6.Format(mSuppRefDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & mSuppPoNo & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                        & " '', '', ''," & vbCrLf _
                        & " '', '', " & mPORate & ", " & vbCrLf & " '" & mMrrRefType & "','" & mAccountCode & "'," & vbCrLf _
                        & " " & mCGSTPer & ", " & mSGSTPer & ", " & mIGSTPer & ", " & vbCrLf _
                        & " " & mCGSTAmount & ", " & mSGSTAmount & ", " & mIGSTAmount & ",'" & mHSNCode & "' " & vbCrLf _
                        & " ) "

                    PubDBCn.Execute(mSqlStr)
                End If
            Next
        End With
        With SprdMain
            mBookType = VB.Left(ConDebitNote, 1)
            mSubBookType = "V"
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)
                .Col = ColItemDesc
                mItemDesc = Trim(.Text)
                .Col = ColHSN
                mHSNCode = Trim(.Text)
                .Col = ColQty
                mItemQty = Val(.Text)
                .Col = ColShortageQty
                mItemQty = mItemQty - Val(.Text)
                .Col = ColRejectedQty
                mItemQty = mItemQty - Val(.Text)
                .Col = ColUnit
                mItemUOM = Trim(.Text)
                .Col = ColVolDiscRate
                mItemRate = Val(.Text)
                .Col = ColPONo
                mSuppPoNo = Trim(.Text)
                .Col = ColPORate
                mPORate = Val(.Text)
                ''GST Not Required .. 27/10/2018
                mCGSTPer = 0
                mSGSTPer = 0
                mIGSTPer = 0
                mCGSTAmount = 0
                mSGSTAmount = 0
                mIGSTAmount = 0
                If pSameGSTNo = "Y" Then
                    mCGSTPer = 0
                    mSGSTPer = 0
                    mIGSTPer = 0
                    mCGSTAmount = 0
                    mSGSTAmount = 0
                    mIGSTAmount = 0
                Else

                    mCGSTPer = 0
                    mSGSTPer = 0
                    mIGSTPer = 0
                    mCGSTAmount = 0
                    mSGSTAmount = 0
                    mIGSTAmount = 0

                End If
                If (mItemQty * mItemRate) > 0 Then
                    mItemAmount = CDbl(VB6.Format(mItemQty * mItemRate, "0.0000"))
                    .Col = ColInvType
                    If Trim(.Text) = "" Then
                        mAccountCode = GetDebitNameOfInvType(Trim(cboInvType.Text), "N")
                    Else
                        mAccountCode = GetDebitNameOfInvType(Trim(.Text), "N")
                    End If
                    mItemED = mItemAmount * mEDPer * 0.01
                    mItemST = mItemAmount * mSTPer * 0.01
                    '
                    mCGSTAmount = System.Math.Round(Val(CStr(mItemAmount * mCGSTPer * 0.01)), 2)
                    mSGSTAmount = System.Math.Round(Val(CStr(mItemAmount * mSGSTPer * 0.01)), 2)
                    mIGSTAmount = System.Math.Round(Val(CStr(mItemAmount * mIGSTPer * 0.01)), 2)
                    mSubRowNo = mSubRowNo + 1
                    mSqlStr = " INSERT INTO TEMP_FIN_DNCN_DET " & vbCrLf & " ( " & vbCrLf & " AUTO_GEN_REFNO, DNCN_BOOKTYPE, DNCN_BOOKSUBTYPE, " & vbCrLf & " SUBROWNO, ITEM_CODE, ITEM_DESC, " & vbCrLf & " ITEM_QTY, ITEM_UOM, ITEM_RATE, " & vbCrLf & " ITEM_AMT, ITEM_ED, ITEM_ST, " & vbCrLf & " MRR_REF_NO, MRR_REF_DATE, SUPP_REF_NO, " & vbCrLf & " SUPP_REF_DATE, REF_PO_NO, COMPANY_CODE, " & vbCrLf & " PURMKEY, PURVNO, PURVDATE, " & vbCrLf & " DNCN_REF_NO, DNCN_REF_DATE, PO_RATE, " & vbCrLf & " MRR_REF_TYPE, ACCOUNT_POST_CODE, " & vbCrLf & " CGST_PER, SGST_PER, IGST_PER, " & vbCrLf & " CGST_AMOUNT, SGST_AMOUNT, IGST_AMOUNT, HSNCODE " & vbCrLf & " ) VALUES ("
                    mSqlStr = mSqlStr & vbCrLf & " " & pTempDNCNSeq & ", '" & mBookType & "', '" & mSubBookType & "', " & vbCrLf & " " & mSubRowNo & ", '" & mItemCode & "', '" & MainClass.AllowSingleQuote(mItemDesc) & "', " & vbCrLf & " " & mItemQty & ", '" & mItemUOM & "', " & mItemRate & ", " & vbCrLf & " " & mItemAmount & ", " & mItemED & ", " & mItemST & ", " & vbCrLf & " " & mMRRRefNo & ", TO_DATE('" & VB6.Format(mMRRDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(mSuppRefNo) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(mSuppRefDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & mSuppPoNo & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " '', '', ''," & vbCrLf & " '', '', " & mPORate & ", " & vbCrLf & " '" & mMrrRefType & "','" & mAccountCode & "'," & vbCrLf & " " & mCGSTPer & ", " & mSGSTPer & ", " & mIGSTPer & ", " & vbCrLf & " " & mCGSTAmount & ", " & mSGSTAmount & ", " & mIGSTAmount & ",'" & mHSNCode & "' " & vbCrLf & " ) "
                    PubDBCn.Execute(mSqlStr)
                End If
            Next
        End With
        If Val(CStr(mOtherDebitAmount)) > 0 Then

            With SprdMain
                mBookType = VB.Left(ConDebitNote, 1)
                mSubBookType = "O"
                For cntRow = 1 To .MaxRows - 1
                    .Row = cntRow
                    .Col = ColItemCode
                    mItemCode = Trim(.Text)
                    .Col = ColItemDesc
                    mItemDesc = Trim(.Text)
                    .Col = ColHSN
                    mHSNCode = Trim(.Text)
                    .Col = ColQty
                    mItemQty = Val(.Text)
                    .Col = ColUnit
                    mItemUOM = Trim(.Text)
                    .Col = ColRate
                    xItemRate = Val(.Text)
                    .Col = ColPONo
                    mSuppPoNo = Trim(.Text)
                    .Col = ColPORate
                    mPORate = Val(.Text)
                    ''GST Not Required .. 27/10/2018
                    mCGSTPer = 0
                    mSGSTPer = 0
                    mIGSTPer = 0
                    mCGSTAmount = 0
                    mSGSTAmount = 0
                    mIGSTAmount = 0
                    If pSameGSTNo = "Y" Then
                        mCGSTPer = 0
                        mSGSTPer = 0
                        mIGSTPer = 0
                        mCGSTAmount = 0
                        mSGSTAmount = 0
                        mIGSTAmount = 0
                    Else
                        mCGSTPer = 0
                        mSGSTPer = 0
                        mIGSTPer = 0
                        mCGSTAmount = 0
                        mSGSTAmount = 0
                        mIGSTAmount = 0
                    End If
                    If (mItemQty * xItemRate) > 0 Then
                        '                mItemAmount = Format(mItemQty * mItemRate, "0.0000")
                        If Val(lblTotItemValue.Text) > 0 Then
                            mItemAmount = CDbl(VB6.Format((mItemQty * xItemRate) * mOtherDebitAmount / Val(lblTotItemValue.Text), "0.0000"))
                            mItemRate = CDbl(VB6.Format(mItemAmount / mItemQty, "0.0000"))
                        Else
                            mItemAmount = 0
                            mItemRate = 0
                        End If
                        .Col = ColInvType
                        If Trim(.Text) = "" Then
                            mAccountCode = GetDebitNameOfInvType(Trim(cboInvType.Text), "N")
                        Else
                            mAccountCode = GetDebitNameOfInvType(Trim(.Text), "N")
                        End If

                        mAccountCode = IIf(mExpAccountCode = "", mAccountCode, mExpAccountCode)

                        mItemED = mItemAmount * mEDPer * 0.01
                        mItemST = mItemAmount * mSTPer * 0.01
                        '
                        mCGSTAmount = System.Math.Round(Val(CStr(mItemAmount * mCGSTPer * 0.01)), 2)
                        mSGSTAmount = System.Math.Round(Val(CStr(mItemAmount * mSGSTPer * 0.01)), 2)
                        mIGSTAmount = System.Math.Round(Val(CStr(mItemAmount * mIGSTPer * 0.01)), 2)
                        mSubRowNo = mSubRowNo + 1
                        mSqlStr = " INSERT INTO TEMP_FIN_DNCN_DET " & vbCrLf _
                            & " ( " & vbCrLf & " AUTO_GEN_REFNO, DNCN_BOOKTYPE, DNCN_BOOKSUBTYPE, " & vbCrLf _
                            & " SUBROWNO, ITEM_CODE, ITEM_DESC, " & vbCrLf & " ITEM_QTY, ITEM_UOM, ITEM_RATE, " & vbCrLf _
                            & " ITEM_AMT, ITEM_ED, ITEM_ST, " & vbCrLf & " MRR_REF_NO, MRR_REF_DATE, SUPP_REF_NO, " & vbCrLf _
                            & " SUPP_REF_DATE, REF_PO_NO, COMPANY_CODE, " & vbCrLf & " PURMKEY, PURVNO, PURVDATE, " & vbCrLf _
                            & " DNCN_REF_NO, DNCN_REF_DATE, PO_RATE, " & vbCrLf _
                            & " MRR_REF_TYPE, ACCOUNT_POST_CODE, " & vbCrLf _
                            & " CGST_PER, SGST_PER, IGST_PER, " & vbCrLf _
                            & " CGST_AMOUNT, SGST_AMOUNT, IGST_AMOUNT, HSNCODE " & vbCrLf _
                            & " ) VALUES ("

                        mSqlStr = mSqlStr & vbCrLf _
                            & " " & pTempDNCNSeq & ", '" & mBookType & "', '" & mSubBookType & "', " & vbCrLf _
                            & " " & mSubRowNo & ", '" & mItemCode & "', '" & MainClass.AllowSingleQuote(mItemDesc) & "', " & vbCrLf _
                            & " " & mItemQty & ", '" & mItemUOM & "', " & mItemRate & ", " & vbCrLf _
                            & " " & mItemAmount & ", " & mItemED & ", " & mItemST & ", " & vbCrLf _
                            & " " & mMRRRefNo & ", TO_DATE('" & VB6.Format(mMRRDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(mSuppRefNo) & "'," & vbCrLf _
                            & " TO_DATE('" & VB6.Format(mSuppRefDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & mSuppPoNo & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                            & " '', '', ''," & vbCrLf _
                            & " '', '', " & mPORate & ", " & vbCrLf _
                            & " '" & mMrrRefType & "','" & mAccountCode & "'," & vbCrLf & " " & mCGSTPer & ", " & mSGSTPer & ", " & mIGSTPer & ", " & vbCrLf & " " & mCGSTAmount & ", " & mSGSTAmount & ", " & mIGSTAmount & ",'" & mHSNCode & "' " & vbCrLf & " ) "

                        PubDBCn.Execute(mSqlStr)
                    End If
                Next
            End With
        End If
        UpdateTempDNCNTable = True
        Exit Function
ErrPart:
        UpdateTempDNCNTable = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function
    Private Function IsDebitNoteDeduct(ByRef pTempDNCNSeq As Double, ByRef pBookType As String, ByRef pBookSubType As String, ByRef mVNo As String, ByRef mSuppCustCode As String, ByRef mDivisionCode As Double) As Boolean
        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mApproved As String
        Dim mAccountCode As String
        Dim mDNCnNO As Integer
        Dim mMsg As String = ""
        Dim mDebitCode As String
        Dim mCreditCode As String
        IsDebitNoteDeduct = False


        mApproved = IIf(IsDBNull(RsCompany.Fields("Shortage_DN_APP").Value), "N", RsCompany.Fields("Shortage_DN_APP").Value)
        If pBookSubType = "S" Then
            mApproved = IIf(IsDBNull(RsCompany.Fields("Shortage_DN_APP").Value), "N", RsCompany.Fields("Shortage_DN_APP").Value)
            mMsg = "Are You Want to Approved Debit Note For Shortage."
        ElseIf pBookSubType = "R" Then
            mApproved = IIf(IsDBNull(RsCompany.Fields("Rejection_DN_APP").Value), "N", RsCompany.Fields("Rejection_DN_APP").Value)
            mMsg = "Are You Want to Approved Debit Note For Rejection."
            If RsCompany.Fields("REJECTION_DN").Value = "N" Then
                IsDebitNoteDeduct = True
                Exit Function
            End If
        ElseIf pBookSubType = "P" Then
            If pBookType = VB.Left(ConDebitNote, 1) Then
                mApproved = IIf(IsDBNull(RsCompany.Fields("RATE_Diff_DN_APP").Value), "N", RsCompany.Fields("RATE_Diff_DN_APP").Value)
                mMsg = "Are You Want to Approved Debit Note For PO Rate Diff."
            Else
                If RsCompany.Fields("RATE_Diff_CN").Value = "N" Then
                    IsDebitNoteDeduct = True
                    Exit Function
                End If
                mApproved = IIf(IsDBNull(RsCompany.Fields("RATE_Diff_CN_APP").Value), "N", RsCompany.Fields("RATE_Diff_CN_APP").Value)
                mMsg = "Are You Want to Approved Credit Note For PO Rate Diff."
            End If
        ElseIf pBookSubType = "V" Then
            mApproved = "Y"
            mMsg = "Are You Want to Approved Debit Note For Volume Discount."
        ElseIf pBookSubType = "O" Then
            mApproved = "Y"
            mMsg = "Are You Want to Approved Debit Note For Other."
        End If

        mSqlStr = "SELECT ACCOUNT_POST_CODE FROM TEMP_FIN_DNCN_DET " & vbCrLf _
            & " WHERE AUTO_GEN_REFNO=" & pTempDNCNSeq & " " & vbCrLf _
            & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND DNCN_BOOKTYPE='" & pBookType & "'" & vbCrLf _
            & " AND DNCN_BOOKSUBTYPE='" & pBookSubType & "'" & vbCrLf _
            & " GROUP BY ACCOUNT_POST_CODE"
        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            If mApproved = "N" Then
                If pBookType = VB.Left(ConCreditNote, 1) Then
                    mApproved = "N"
                Else
                    If MsgQuestion(mMsg) = CStr(MsgBoxResult.No) Then
                        mApproved = "N"
                    Else
                        mApproved = "Y"
                    End If
                End If
            End If
            Do While RsTemp.EOF = False
                mAccountCode = IIf(IsDBNull(RsTemp.Fields("ACCOUNT_POST_CODE").Value), "", RsTemp.Fields("ACCOUNT_POST_CODE").Value)
                If pBookType = VB.Left(ConDebitNote, 1) Then
                    mDebitCode = mSuppCustCode
                    mCreditCode = mAccountCode
                Else
                    mDebitCode = mAccountCode
                    mCreditCode = mSuppCustCode
                End If
                '
                If UpdateNewDnCnMain(mVNo, (txtVDate.Text), Trim(txtBillNo.Text), Trim(txtBillDate.Text), Trim(txtMRRNo.Text), (txtMRRDate.Text), (txtPONo.Text), (txtPODate.Text), Val(txtCreditDays(0).Text), Val(txtCreditDays(1).Text), Trim(txtItemType.Text), pBookSubType, "N", IIf(pBookType = VB.Left(ConDebitNote, 1), ConDebitNoteBookCode, ConCreditNoteBookCode), VB.Left(IIf(pBookType = VB.Left(ConDebitNote, 1), ConDebitNote, ConCreditNote), 1), VB.Right(IIf(pBookType = VB.Left(ConDebitNote, 1), ConDebitNote, ConCreditNote), 1), mDebitCode, mCreditCode, (txtPaymentdate.Text), mApproved, pTempDNCNSeq, False, mDivisionCode, mAccountCode) = False Then GoTo ErrPart
                RsTemp.MoveNext()
            Loop
        End If

        IsDebitNoteDeduct = True
        Exit Function
ErrPart:
        IsDebitNoteDeduct = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function
    Private Function UpdateNewDnCnMain(ByRef xPURVNO As String, ByRef xPURVDate As String, ByRef xBillNo As String,
                                       ByRef xBillDate As String, ByRef xMRRNo As String, ByRef xMRRDate As String, ByRef xPoNo As String,
                                       ByRef xPODate As String, ByRef xCreditDays1 As Integer, ByRef xCreditDays2 As Integer,
                                       ByRef xItemDesc As String, ByRef xDnCnType As String, ByRef xCancelled As String, ByRef xBookCode As Integer,
                                       ByRef xBookType As String, ByRef xBookSubType As String, ByRef xDebitAccountCode As String, ByRef xCreditAccountCode As String,
                                       ByRef xPayDate As String, ByRef xApproved As String, ByRef pTempDNCNSeq As Double, ByRef pExpDiffDN As Boolean,
                                       ByRef mDivisionCode As Double, ByRef xAccountCode As String, Optional ByRef xAmount As Double = 0) As Boolean
        On Error GoTo ErrPart
        Dim xMkey As String = ""
        Dim xCurRowNo As Integer
        Dim SqlStr As String = ""
        Dim xVNoPrefix As String
        Dim xVTYPE As String
        Dim xVNoSeq As Double
        Dim xVNoSuffix As String
        Dim xVNo As String = ""
        Dim xVDate As String = ""
        Dim xItemValue As Double
        Dim xSTPERCENT As Double
        Dim xTOTSTAMT As Double
        Dim xTOTFREIGHT As Double
        Dim xTOTCHARGES As Double
        Dim xEDPERCENT As Double
        Dim xTotEDAmount As Double
        Dim xSURAmount As Double
        Dim xTotDiscount As Double
        Dim xMSC As Double
        Dim xRO As Double
        Dim xTOTEXPAMT As Double
        Dim xTOTTAXABLEAMOUNT As Double
        Dim xNETVALUE As Double
        Dim xTotQty As Double
        Dim xNarration As String
        Dim xReason As String = ""
        Dim nBookCode As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mPDIRItem As String
        Dim mExpAmount As Double
        Dim mDNFROM As String
        'Dim RsTemp As ADODB.Recordset = Nothing
        Dim xCessAmt As Double
        Dim xSHECessAmt As Double
        Dim xTotServiceAmt As Double
        Dim xSTClaimNo As String
        Dim xSTClaimPer As Double
        Dim xSTClaimAmount As Double
        Dim xSURVATClaimAmount As Double
        Dim xSTClaimDate As String
        Dim xISSTRefund As String
        Dim xTOTSURCHARGEAMT As Double
        Dim xTOTVATCLAIMAMT As Double
        Dim xISCSTRefund As String
        Dim pInsertRow As Boolean
        Dim xIsGST As String
        Dim xCGSTRefundAMT As Double
        Dim xSGSTRefundAMT As Double
        Dim xIGSTRefundAMT As Double
        Dim mPartyDNCNNo As String
        Dim mPartyDNCNDate As String
        Dim mPartyDNCNRcdDate As String
        Dim mIsDNCNIssue As String
        Dim pSuppCustCode As String
        Dim pAccountCode As String
        Dim mSubRow As Integer
        Dim mItemCode As String
        Dim mQty As Double
        Dim mUnit As String
        Dim mRate As Double
        Dim mAmount As Double
        Dim mCGSTPer As Double
        Dim mSGSTPer As Double
        Dim mIGSTPer As Double
        Dim mCGSTAmount As Double
        Dim mSGSTAmount As Double
        Dim mIGSTAmount As Double
        Dim mHSNCode As String
        Dim mItemDesc As String
        Dim mPartyGSTNo As String
        Dim mCompanyGSTNo As String
        Dim pDNSeqType As Integer
        mCompanyGSTNo = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
        mPartyGSTNo = ""
        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPartyGSTNo = MasterNo
        End If
        If xCancelled = "Y" Then UpdateNewDnCnMain = True : Exit Function
        If xDnCnType = "R" Then
            xVTYPE = IIf(xBookCode = ConDebitNoteBookCode, "DR", "CR")
            mDNFROM = "M"
        Else
            xVTYPE = IIf(xBookCode = ConDebitNoteBookCode, "DN", "CN")
            mDNFROM = "P"
        End If
        xSTClaimNo = ""
        xSTClaimPer = 0
        xSTClaimAmount = 0
        xSTClaimDate = ""
        xSURVATClaimAmount = 0
        xISSTRefund = "N"
        xISCSTRefund = "N"
        If mPartyGSTNo = mCompanyGSTNo Then
            xIsGST = "W"
        ElseIf xBookSubType = "P" Or xBookSubType = "V" Or xBookSubType = "O" Then
            'xIsGST = "W"
            If VB.Left(cboGSTStatus.Text, 1) = "G" Then
                'xIsGST = IIf(IsDBNull(RsCompany.Fields("INVOICE_PREFIX").Value), "", RsCompany.Fields("INVOICE_PREFIX").Value)
                If xDnCnType = "R" Then
                    xIsGST = IIf(IsDBNull(RsCompany.Fields("Rejection_With_GST").Value), "N", RsCompany.Fields("Rejection_With_GST").Value)
                    xIsGST = IIf(xIsGST = "Y", "G", "W")
                ElseIf xDnCnType = "S" Then
                    xIsGST = IIf(IsDBNull(RsCompany.Fields("Shortage_With_GST").Value), "N", RsCompany.Fields("Shortage_With_GST").Value)
                    xIsGST = IIf(xIsGST = "Y", "G", "W")
                ElseIf xDnCnType = "P" Or xDnCnType = "V" Or xDnCnType = "D" Or xDnCnType = "A" Or xDnCnType = "O" Then
                    If xBookCode = ConDebitNoteBookCode Then
                        xIsGST = IIf(IsDBNull(RsCompany.Fields("RateDiff_DR_With_GST").Value), "N", RsCompany.Fields("RateDiff_DR_With_GST").Value)
                    Else
                        xIsGST = IIf(IsDBNull(RsCompany.Fields("RateDiff_CR_With_GST").Value), "N", RsCompany.Fields("RateDiff_CR_With_GST").Value)
                    End If
                    xIsGST = IIf(xIsGST = "Y", "G", "W")
                End If
            ElseIf VB.Left(cboGSTStatus.Text, 1) = "I" Then
                xIsGST = VB.Left(cboGSTStatus.Text, 1)
            Else
                xIsGST = "W"
            End If
        Else
            If VB.Left(cboGSTStatus.Text, 1) = "G" Then
                If xDnCnType = "R" Then
                    xIsGST = IIf(IsDBNull(RsCompany.Fields("Rejection_With_GST").Value), "N", RsCompany.Fields("Rejection_With_GST").Value)
                    xIsGST = IIf(xIsGST = "Y", "G", "W")
                ElseIf xDnCnType = "S" Then
                    xIsGST = IIf(IsDBNull(RsCompany.Fields("Shortage_With_GST").Value), "N", RsCompany.Fields("Shortage_With_GST").Value)
                    xIsGST = IIf(xIsGST = "Y", "G", "W")
                ElseIf xDnCnType = "P" Or xDnCnType = "V" Or xDnCnType = "D" Or xDnCnType = "A" Or xDnCnType = "O" Then
                    If xBookCode = ConDebitNoteBookCode Then
                        xIsGST = IIf(IsDBNull(RsCompany.Fields("RateDiff_DR_With_GST").Value), "N", RsCompany.Fields("RateDiff_DR_With_GST").Value)
                    Else
                        xIsGST = IIf(IsDBNull(RsCompany.Fields("RateDiff_CR_With_GST").Value), "N", RsCompany.Fields("RateDiff_CR_With_GST").Value)
                    End If
                    xIsGST = IIf(xIsGST = "Y", "G", "W")
                End If
            ElseIf VB.Left(cboGSTStatus.Text, 1) = "I" Then
                xIsGST = VB.Left(cboGSTStatus.Text, 1)
            Else
                xIsGST = "W"
            End If
        End If
        xVNoPrefix = VB6.Format(IIf(IsDBNull(RsCompany.Fields("INVOICE_PREFIX").Value), "", RsCompany.Fields("INVOICE_PREFIX").Value), "00")
        xVNoSuffix = ""
        xItemValue = 0
        xSTPERCENT = 0
        xTOTSTAMT = 0
        xTOTFREIGHT = 0
        xTOTCHARGES = 0
        xEDPERCENT = 0
        xTotEDAmount = 0
        xSURAmount = 0
        xTotDiscount = 0
        xMSC = 0
        xRO = 0
        xTOTEXPAMT = 0
        xTOTTAXABLEAMOUNT = 0
        xNETVALUE = 0
        xTotQty = 0
        xNarration = ""
        xTOTSURCHARGEAMT = 0
        xTOTVATCLAIMAMT = 0
        mPartyDNCNNo = ""
        mPartyDNCNDate = ""
        mPartyDNCNRcdDate = ""
        mIsDNCNIssue = "N"

        If xDnCnType = "R" Then
            xReason = "REJECTION"
            pDNSeqType = 4
        ElseIf xDnCnType = "S" Then
            xReason = "SHORTAGE"
            'mPartyDNCNNo = xVNo
            'mPartyDNCNDate = xVDate
            'mPartyDNCNRcdDate = xVDate
            mIsDNCNIssue = "Y"
            pDNSeqType = 1
        ElseIf xDnCnType = "P" Then
            xReason = "RATE DIFF"
            pDNSeqType = 2
        ElseIf xDnCnType = "V" Then
            xReason = "VOLUME DISCOUNT"
            pDNSeqType = 5
        ElseIf xDnCnType = "D" Then
            xReason = "DISCOUNT"
            pDNSeqType = 6
        ElseIf xDnCnType = "A" Then
            xReason = "AMENDED PO RATE DIFF"
            pDNSeqType = 3
        ElseIf xDnCnType = "O" Then
            xNETVALUE = xAmount
            xReason = "OTHERS" ' "PDIR NOT RECEVIED."
            xNarration = "OTHERS" '"PDIR NOT RECEVIED."
            pDNSeqType = 7
            '        SqlStr = "SELECT ITEM_CODE " & vbCrLf _
            ''                & " FROM INV_GATE_DET " & vbCrLf _
            ''                & " WHERE " & vbCrLf _
            ''                & " AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & " AND PDIR_FLAG='N'"
            '
            '        MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly
            '        If RsTemp.EOF = False Then
            '            Do While Not RsTemp.EOF
            '                mPDIRItem = IIf(mPDIRItem = "", "", mPDIRItem & ",") & IIf(IsNull(RsTemp!ITEM_CODE), "", RsTemp!ITEM_CODE)
            '                RsTemp.MoveNext
            '            Loop
            '        xNarration = xNarration & " AGT. ITEM CODE  : " & mPDIRItem & " ( Rs. 200/- each)"
            '        End If
        End If

        xVNoSeq = CDbl(AutoGenDNCNNo("VNOSEQ", xBookCode, xVTYPE, pDNSeqType))
        xVNo = Trim(xVTYPE) & Trim(xVNoPrefix) & VB6.Format(Val(CStr(xVNoSeq)), "00000") & Trim(xVNoSuffix)
        xVDate = txtVDate.Text
        ''xVNoSeq = xDNCNNO + AutoGenDNCNNo("VNOSEQ", xBookCode, xVTYPE)
        SqlStr = ""

        If ADDMode = True Then
            '        If xDnCnType <> "O" Then
            If UpdateNewDNCNDetail1(xBookType, xDnCnType, xMkey, xVTYPE, xPURVNO, xPURVDate, xAccountCode, "Y", pInsertRow) = False Then GoTo ErrPart
            '        End If
            If pInsertRow = False Then
                UpdateNewDnCnMain = True
                Exit Function
            End If
            xCurRowNo = MainClass.AutoGenRowNo("FIN_DNCN_HDR", "RowNo", PubDBCn)
            xMkey = RsCompany.Fields("COMPANY_CODE").Value & RsCompany.Fields("FYEAR").Value & xCurRowNo

            SqlStr = "INSERT INTO FIN_DNCN_HDR( " & vbCrLf _
                & " MKEY, COMPANY_CODE, FYEAR, ROWNO, " & vbCrLf _
                & " VNOPREFIX, VTYPE,VNOSEQ, VNOSUFFIX, " & vbCrLf _
                & " VNO, VDATE, BILLNO, INVOICE_DATE, " & vbCrLf _
                & " DEBITACCOUNTCODE, CREDITACCOUNTCODE, DUEDAYSFROM, DUEDAYSTO, " & vbCrLf _
                & " BOOKCODE, BookType, BOOKSUBTYPE, REMARKS,  " & vbCrLf _
                & " ITEMDESC, REASON, ITEMVALUE, STPERCENT,  " & vbCrLf _
                & " TOTSTAMT, TOTFREIGHT, TOTCHARGES, EDPERCENT,  " & vbCrLf _
                & " TOTEDAMOUNT, TOTSURCHARGEAMT, TOTDISCAMOUNT, TOTMSCAMOUNT, " & vbCrLf & " TOTRO, TOTEXPAMT, TOTTAXABLEAMOUNT, NETVALUE, " & vbCrLf _
                & " TOTQTY, CANCELLED, NARRATION, DNCNTYPE, APPROVED, PAYDATE, DNCNFROM, " & vbCrLf & " PURVNO, PURVDATE, AUTO_KEY_MRR, MRRDATE, " & vbCrLf _
                & " CUSTREFNO, CUSTREFDATE, MODVATNO, MODVATDATE, " & vbCrLf _
                & " MODVATPER, MODVATAMOUNT, STCLAIMNO, STCLAIMPER, " & vbCrLf _
                & " STCLAIMAMOUNT, STCLAIMDATE, ISMODVAT, ISSTREFUND, " & vbCrLf & " ISDESPATCHED, SALEINVOICENO, SALEINVOICEDATE, " & vbCrLf _
                & " ADDUSER, ADDDATE, MODUSER, MODDATE,ISCSTREFUND, " & vbCrLf & " UPDATE_FROM, SUR_VATCLAIMAMOUNT,DIV_CODE, " & vbCrLf _
                & " NETCGST_AMOUNT, NETSGST_AMOUNT, NETIGST_AMOUNT," & vbCrLf & " ISGSTREFUND , GST_NO, GST_DATE, CGST_REFUNDAMOUNT, " & vbCrLf _
                & " SGST_REFUNDAMOUNT, IGST_REFUNDAMOUNT, " & vbCrLf _
                & " PARTY_DNCN_NO, PARTY_DNCN_DATE, PARTY_DNCN_RECDDATE, ISDNCN_ISSUE,DNCNSEQTYPE,BILL_TO_LOC_ID, SHIP_TO_LOC_ID ) "

            SqlStr = SqlStr & vbCrLf & " VALUES('" & xMkey & "'," & RsCompany.Fields("Company_Code").Value & "," & RsCompany.Fields("FYEAR").Value & ", " & xCurRowNo & ", " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(xVNoPrefix) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(xVTYPE) & "', " & vbCrLf _
                & " " & xVNoSeq & ", '" & MainClass.AllowSingleQuote(xVNoSuffix) & "', '" & MainClass.AllowSingleQuote(xVNo) & "',TO_DATE('" & VB6.Format(xVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(xBillNo) & "', TO_DATE('" & VB6.Format(xBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & xDebitAccountCode & "','" & xCreditAccountCode & "', " & Val(CStr(xCreditDays1)) & ", " & Val(CStr(xCreditDays2)) & ", " & vbCrLf & " '" & xBookCode & "', '" & xBookType & "', '" & xBookSubType & "', ''," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(xItemDesc) & "', '" & MainClass.AllowSingleQuote(xReason) & "', " & vbCrLf _
                & " " & xItemValue & ", " & xSTPERCENT & ", " & xTOTSTAMT & ", " & xTOTFREIGHT & ", " & xTOTCHARGES & "," & vbCrLf _
                & " " & xEDPERCENT & ", " & xTotEDAmount & ", " & xSURAmount & ", " & xTotDiscount & "," & xMSC & ", " & vbCrLf _
                & " " & xRO & ", " & xTOTEXPAMT & ", " & xTOTTAXABLEAMOUNT & ", " & xNETVALUE & ", " & vbCrLf _
                & " " & xTotQty & ", '" & xCancelled & "', '" & MainClass.AllowSingleQuote(xNarration) & "', " & vbCrLf _
                & " '" & xDnCnType & "', '" & xApproved & "', TO_DATE('" & VB6.Format(xPayDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & mDNFROM & "', " & vbCrLf _
                & " '" & xPURVNO & "', TO_DATE('" & VB6.Format(xPURVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & xMRRNo & "', TO_DATE('" & VB6.Format(xMRRDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " '', '', '', '', " & vbCrLf _
                & " 100, 0, '" & xSTClaimNo & "', " & xSTClaimPer & ", " & vbCrLf _
                & " " & xSTClaimAmount & ", TO_DATE('" & VB6.Format(xSTClaimDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), 'N', '" & xISSTRefund & "'," & vbCrLf _
                & " 'N','',''," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'',''," & vbCrLf _
                & " '" & xISCSTRefund & "','N'," & Val(CStr(xSURVATClaimAmount)) & "," & mDivisionCode & ",0,0,0," & vbCrLf _
                & " '" & xIsGST & "', '' , '',0,0,0," & vbCrLf _
                & " '" & mPartyDNCNNo & "', TO_DATE('" & VB6.Format(mPartyDNCNDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), TO_DATE('" & VB6.Format(mPartyDNCNRcdDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & mIsDNCNIssue & "'," & pDNSeqType & ",'" & MainClass.AllowSingleQuote(txtBillTo.Text) & "','" & MainClass.AllowSingleQuote(TxtShipTo.Text) & "')"
        End If
        PubDBCn.Execute(SqlStr)

        UpdateNewDnCnMain = True
        '    If pExpDiffDN = True Then
        '        If UpdateDNCNExp1(xMkey) = False Then GoTo ErrPart
        '    End If
        '    If xDnCnType = "O" Then
        '        If UpdateDNCNPDIRExp1(xMkey, xAmount) = False Then GoTo ErrPart
        '    Else
        If UpdateNewDNCNDetail1(xBookType, xDnCnType, xMkey, xVTYPE, xPURVNO, xPURVDate, xAccountCode, "N", pInsertRow) = False Then GoTo ErrPart
        '    End If
        '    If (xDnCnType = "P" Or xDnCnType = "R" Or xDnCnType = "S" Or xDnCnType = "V") Then             'And pExpDiffDN = False
        If UpdateDNCNRateDiffExp1(xDnCnType, xMkey, pExpDiffDN) = False Then GoTo ErrPart
        '    End If
        If xApproved = "Y" Then
            If UpdateDNCNHDRAPP(xMkey, xDnCnType) = False Then GoTo ErrPart
            nBookCode = CStr(xBookCode)
            SqlStr = "SELECT NETVALUE, TOTEXPAMT, TOTEDAMOUNT,TOTSTAMT, " & vbCrLf _
                & " TOTSURCHARGEAMT,SUR_VATCLAIMAMOUNT, " & vbCrLf _
                & " NETCGST_AMOUNT, NETSGST_AMOUNT, NETIGST_AMOUNT, " & vbCrLf _
                & " CGST_REFUNDAMOUNT, SGST_REFUNDAMOUNT, IGST_REFUNDAMOUNT" & vbCrLf _
                & " FROM FIN_DNCN_HDR" & vbCrLf & " WHERE " & vbCrLf _
                & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                & " AND MKEY='" & xMkey & "'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                xAmount = IIf(IsDBNull(RsTemp.Fields("NETVALUE").Value), 0, RsTemp.Fields("NETVALUE").Value)
                mExpAmount = IIf(IsDBNull(RsTemp.Fields("TOTEXPAMT").Value), 0, RsTemp.Fields("TOTEXPAMT").Value)
                xTotEDAmount = IIf(IsDBNull(RsTemp.Fields("TOTEDAMOUNT").Value), 0, RsTemp.Fields("TOTEDAMOUNT").Value)
                xTOTSTAMT = IIf(IsDBNull(RsTemp.Fields("TOTSTAMT").Value), 0, RsTemp.Fields("TOTSTAMT").Value)
                xTOTSURCHARGEAMT = IIf(IsDBNull(RsTemp.Fields("TOTSURCHARGEAMT").Value), 0, RsTemp.Fields("TOTSURCHARGEAMT").Value)
                xTOTVATCLAIMAMT = IIf(IsDBNull(RsTemp.Fields("SUR_VATCLAIMAMOUNT").Value), 0, RsTemp.Fields("SUR_VATCLAIMAMOUNT").Value)
                '            xCGSTAMT = IIf(IsNull(RsTemp!NETCGST_AMOUNT), 0, RsTemp!NETCGST_AMOUNT)
                '            xSGSTAMT = IIf(IsNull(RsTemp!NETSGST_AMOUNT), 0, RsTemp!NETSGST_AMOUNT)
                '            xIGSTAMT = IIf(IsNull(RsTemp!NETIGST_AMOUNT), 0, RsTemp!NETIGST_AMOUNT)
                xCGSTRefundAMT = IIf(IsDBNull(RsTemp.Fields("CGST_REFUNDAMOUNT").Value), 0, RsTemp.Fields("CGST_REFUNDAMOUNT").Value)
                xSGSTRefundAMT = IIf(IsDBNull(RsTemp.Fields("SGST_REFUNDAMOUNT").Value), 0, RsTemp.Fields("SGST_REFUNDAMOUNT").Value)
                xIGSTRefundAMT = IIf(IsDBNull(RsTemp.Fields("IGST_REFUNDAMOUNT").Value), 0, RsTemp.Fields("IGST_REFUNDAMOUNT").Value)
                If Trim(mCompanyGSTNo) = Trim(mPartyGSTNo) Then
                    xAmount = xAmount - xCGSTRefundAMT - xSGSTRefundAMT - xIGSTRefundAMT
                    xCGSTRefundAMT = 0
                    xSGSTRefundAMT = 0
                    xIGSTRefundAMT = 0
                End If
            End If
            pSuppCustCode = IIf(CDbl(nBookCode) = ConDebitNoteBookCode, xDebitAccountCode, xCreditAccountCode)
            pAccountCode = IIf(CDbl(nBookCode) = ConDebitNoteBookCode, xCreditAccountCode, xDebitAccountCode)

            If DNCNPostTRNGST(PubDBCn, xMkey, xCurRowNo, nBookCode, xBookType, xBookSubType, xVTYPE, xVNo, xVDate, xBillNo, xBillDate, xDebitAccountCode, xCreditAccountCode, Val(CStr(xAmount)), IIf(xCancelled = "Y", True, False), xPayDate, "", xReason, mExpAmount, ADDMode, mAddUser, mAddDate, mDivisionCode, IIf(xIsGST = "G", IIf(Trim(mCompanyGSTNo) = Trim(mPartyGSTNo), "N", "Y"), "N"), xCGSTRefundAMT, xSGSTRefundAMT, xIGSTRefundAMT, xDnCnType, txtBillTo.Text) = False Then GoTo ErrPart

            SqlStr = " SELECT SUBROWNO, ITEM_CODE,ITEM_QTY,ITEM_UOM,HSNCODE, ITEM_RATE, ITEM_AMT, " & vbCrLf & " CGST_PER,SGST_PER,IGST_PER, " & vbCrLf & " CGST_AMOUNT,SGST_AMOUNT,IGST_AMOUNT " & vbCrLf & " FROM FIN_DNCN_DET" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND MKEY='" & xMkey & "'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                Do While RsTemp.EOF = False
                    mSubRow = IIf(IsDBNull(RsTemp.Fields("SUBROWNO").Value), 0, RsTemp.Fields("SUBROWNO").Value)
                    mItemCode = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)
                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mItemDesc = MasterNo
                    Else
                        mItemDesc = ""
                    End If
                    mHSNCode = IIf(IsDBNull(RsTemp.Fields("HSNCODE").Value), "", RsTemp.Fields("HSNCODE").Value)
                    mQty = IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)
                    mUnit = IIf(IsDBNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value)
                    mRate = IIf(IsDBNull(RsTemp.Fields("ITEM_RATE").Value), 0, RsTemp.Fields("ITEM_RATE").Value)
                    mAmount = IIf(IsDBNull(RsTemp.Fields("ITEM_AMT").Value), 0, RsTemp.Fields("ITEM_AMT").Value)
                    mCGSTPer = IIf(IsDBNull(RsTemp.Fields("CGST_PER").Value), 0, RsTemp.Fields("CGST_PER").Value)
                    mSGSTPer = IIf(IsDBNull(RsTemp.Fields("SGST_PER").Value), 0, RsTemp.Fields("SGST_PER").Value)
                    mIGSTPer = IIf(IsDBNull(RsTemp.Fields("IGST_PER").Value), 0, RsTemp.Fields("IGST_PER").Value)
                    mCGSTAmount = IIf(IsDBNull(RsTemp.Fields("CGST_AMOUNT").Value), 0, RsTemp.Fields("CGST_AMOUNT").Value)
                    mSGSTAmount = IIf(IsDBNull(RsTemp.Fields("SGST_AMOUNT").Value), 0, RsTemp.Fields("SGST_AMOUNT").Value)
                    mIGSTAmount = IIf(IsDBNull(RsTemp.Fields("IGST_AMOUNT").Value), 0, RsTemp.Fields("IGST_AMOUNT").Value)
                    If Trim(mCompanyGSTNo) = Trim(mPartyGSTNo) Then
                    Else
                        If VB.Left(cboGSTStatus.Text, 1) = "G" Then
                            If UpdateGSTTRN(PubDBCn, xMkey, nBookCode, xBookType, xBookSubType, xVNo, VB6.Format(xVDate, "DD-MMM-YYYY"), Trim(xBillNo), VB6.Format(xBillDate, "DD-MMM-YYYY"), "", "", pSuppCustCode, pAccountCode, "Y", pSuppCustCode, mSubRow, mItemCode, mQty, mUnit, mRate, mAmount, mAmount, 0, mCGSTPer, mSGSTPer, mIGSTPer, mCGSTAmount, mSGSTAmount, mIGSTAmount, mCGSTAmount, mSGSTAmount, mIGSTAmount, mDivisionCode, mHSNCode, mItemDesc, "", "N", xDnCnType, IIf(lblPurchaseType.Text = "G", "G", "S"), IIf(VB.Left(cboGSTStatus.Text, 1) = "R", "Y", "N"), IIf(xBookCode = ConDebitNoteBookCode, "D", "C"), (lblGSTClaimDate.Text), "N") = False Then GoTo ErrPart
                        End If
                    End If
                    RsTemp.MoveNext()
                Loop
            End If
        End If
        pDnCnNo = IIf(pDnCnNo = "", "", pDnCnNo & ", ") & xVNo
        Exit Function
ErrPart:
        UpdateNewDnCnMain = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function
    Private Function UpdateNewDNCNDetail1(ByRef pBookType As String, ByRef pDnCnType As String, ByRef xKey As String, ByRef pVType As String, ByRef xPURVNO As String, ByRef xPURVDate As String, ByRef pAccountCode As String, ByRef mOnlyCheck As String, ByRef pInsertRow As Boolean) As Boolean
        On Error GoTo UpdateDetail1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mQty As Double
        Dim mUnit As String
        Dim mRate As Double
        Dim mPORate As Double
        Dim mAmount As Double
        Dim mPONo As String
        Dim mMrrRefType As String
        Dim mFactor As Double
        Dim pItemEDAmount As Double
        Dim pItemSTAmount As Double
        Dim mEDPer As Double
        Dim mEDAmount As Double
        Dim mItemValue As Double
        Dim mExpCode As Double
        Dim mExpName As String
        Dim mEDPerNos As Double
        Dim mCGSTPer As Double
        Dim mSGSTPer As Double
        Dim mIGSTPer As Double
        Dim mCGSTAmount As Double
        Dim mSGSTAmount As Double
        Dim mIGSTAmount As Double
        Dim mHSNCode As String
        pInsertRow = False
        PubDBCn.Execute("Delete From FIN_DNCN_DET Where Mkey='" & xKey & "'")
        SqlStr = ""
        SqlStr = "SELECT ITEM_CODE, HSNCODE, ITEM_DESC, SUM(ITEM_QTY) AS ITEM_QTY," & vbCrLf _
            & " ITEM_UOM, ITEM_RATE, SUM(ITEM_ED) AS ITEM_ED, SUM(ITEM_ST) AS ITEM_ST," & vbCrLf _
            & " MRR_REF_NO, MRR_REF_DATE, SUPP_REF_NO, SUPP_REF_DATE, " & vbCrLf _
            & " REF_PO_NO, PO_RATE, MRR_REF_TYPE," & vbCrLf _
            & " CGST_PER, SGST_PER, IGST_PER, " & vbCrLf _
            & " SUM(CGST_AMOUNT) AS CGST_AMOUNT, SUM(SGST_AMOUNT) AS SGST_AMOUNT, SUM(IGST_AMOUNT) AS IGST_AMOUNT" & vbCrLf _
            & " FROM TEMP_FIN_DNCN_DET " & vbCrLf _
            & " WHERE AUTO_GEN_REFNO=" & pTempDNCNSeq & " " & vbCrLf _
            & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND DNCN_BOOKTYPE='" & pBookType & "'" & vbCrLf _
            & " AND DNCN_BOOKSUBTYPE='" & pDnCnType & "'" & vbCrLf _
            & " AND ACCOUNT_POST_CODE='" & pAccountCode & "'" & vbCrLf _
            & " GROUP BY ITEM_CODE, HSNCODE, ITEM_DESC, " & vbCrLf _
            & " ITEM_UOM, ITEM_RATE," & vbCrLf _
            & " MRR_REF_NO, MRR_REF_DATE, SUPP_REF_NO, SUPP_REF_DATE, " & vbCrLf _
            & " REF_PO_NO, PO_RATE, MRR_REF_TYPE,CGST_PER, SGST_PER, IGST_PER"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        I = 1
        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mItemCode = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)
                mHSNCode = IIf(IsDBNull(RsTemp.Fields("HSNCODE").Value), "", RsTemp.Fields("HSNCODE").Value)
                mItemDesc = IIf(IsDBNull(RsTemp.Fields("ITEM_DESC").Value), "", RsTemp.Fields("ITEM_DESC").Value)
                mUnit = IIf(IsDBNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value)
                mPONo = IIf(IsDBNull(RsTemp.Fields("REF_PO_NO").Value), "", RsTemp.Fields("REF_PO_NO").Value)
                mQty = IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)
                mRate = IIf(IsDBNull(RsTemp.Fields("ITEM_RATE").Value), 0, RsTemp.Fields("ITEM_RATE").Value)
                mPORate = IIf(IsDBNull(RsTemp.Fields("PO_RATE").Value), 0, RsTemp.Fields("PO_RATE").Value)
                mMrrRefType = IIf(IsDBNull(RsTemp.Fields("MRR_REF_TYPE").Value), "", RsTemp.Fields("MRR_REF_TYPE").Value)
                mEDPerNos = 0
                mCGSTPer = IIf(IsDBNull(RsTemp.Fields("CGST_PER").Value), 0, RsTemp.Fields("CGST_PER").Value)
                mSGSTPer = IIf(IsDBNull(RsTemp.Fields("SGST_PER").Value), 0, RsTemp.Fields("SGST_PER").Value)
                mIGSTPer = IIf(IsDBNull(RsTemp.Fields("IGST_PER").Value), 0, RsTemp.Fields("IGST_PER").Value)
                mCGSTAmount = System.Math.Round(IIf(IsDBNull(RsTemp.Fields("CGST_AMOUNT").Value), 0, RsTemp.Fields("CGST_AMOUNT").Value), 2)
                mSGSTAmount = System.Math.Round(IIf(IsDBNull(RsTemp.Fields("SGST_AMOUNT").Value), 0, RsTemp.Fields("SGST_AMOUNT").Value), 2)
                mIGSTAmount = System.Math.Round(IIf(IsDBNull(RsTemp.Fields("IGST_AMOUNT").Value), 0, RsTemp.Fields("IGST_AMOUNT").Value), 2)
                If mQty <> 0 Then
                    mEDPerNos = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ITEM_ED").Value), 0, RsTemp.Fields("ITEM_ED").Value) / mQty, "0.00"))
                End If
                If pDnCnType = "R" Then
                    SqlStr = "SELECT DECODE(ISSUE_UOM,'" & mUnit & "',1,UOM_FACTOR) AS UOM_FACTOR,ISSUE_UOM FROM INV_ITEM_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
                    mFactor = 1
                    If RsMisc.EOF = False Then
                        mUnit = IIf(IsDBNull(RsMisc.Fields("ISSUE_UOM").Value), "", RsMisc.Fields("ISSUE_UOM").Value)
                        mFactor = IIf(IsDBNull(RsMisc.Fields("UOM_FACTOR").Value), 1, RsMisc.Fields("UOM_FACTOR").Value)
                    End If

                    mQty = mQty * mFactor
                    mQty = mQty - GetDebitQty(Val(txtMRRNo.Text), mItemCode, pDnCnType)

                    mQty = IIf(mQty < 0, 0, mQty)
                    '                If pDnCnType = "R" And chkModvat.Value = vbUnchecked Then
                    '                    mRate = Format(mRate + mEDPerNos, "0.0000")
                    '                End If
                    mRate = mRate / mFactor
                ElseIf pDnCnType = "S" Then
                    mQty = mQty - GetDebitQty(Val(txtMRRNo.Text), mItemCode, pDnCnType)
                ElseIf pDnCnType = "P" Then
                    If mPORate <> 0 Then
                        If pBookType = VB.Left(ConDebitNote, 1) Then
                            mRate = System.Math.Abs(mRate) '' Abs(IIf(mRate - mPORate <= 0, 0, mRate - mPORate))
                        ElseIf pBookType = VB.Left(ConCreditNote, 1) Then
                            mRate = System.Math.Abs(mRate) '' Abs(IIf(mPORate - mRate <= 0, 0, mPORate - mRate))
                        Else
                            mRate = 0
                        End If
                    Else
                        mRate = 0
                    End If
                End If
                mAmount = mQty * mRate
                mAmount = CDbl(VB6.Format(mAmount, "0.00"))
                pItemEDAmount = 0
                pItemSTAmount = 0
                If mItemCode <> "" And mAmount <> 0 Then
                    I = I + 1
                    If mOnlyCheck = "N" Then
                        SqlStr = " INSERT INTO FIN_DNCN_DET ( " & vbCrLf _
                            & " MKEY , SUBROWNO, " & vbCrLf _
                            & " ITEM_CODE , HSNCODE, ITEM_DESC, ITEM_QTY, " & vbCrLf _
                            & " ITEM_UOM , ITEM_RATE, ITEM_AMT," & vbCrLf _
                            & " MRR_REF_NO,MRR_REF_DATE,SUPP_REF_NO," & vbCrLf _
                            & " SUPP_REF_DATE, REF_PO_NO, COMPANY_CODE, " & vbCrLf _
                            & " PURMKEY, " & vbCrLf _
                            & " PURVNO, PURVDATE, " & vbCrLf _
                            & " DNCN_REF_NO, DNCN_REF_DATE, " & vbCrLf _
                            & " PO_RATE, MRR_REF_TYPE,ITEM_ED, ITEM_ST, " & vbCrLf _
                            & " CGST_PER, SGST_PER, IGST_PER, " & vbCrLf _
                            & " CGST_AMOUNT, SGST_AMOUNT, IGST_AMOUNT" & vbCrLf _
                            & " ) "

                        SqlStr = SqlStr & vbCrLf _
                            & " VALUES ('" & xKey & "'," & I & ", " & vbCrLf _
                            & " '" & mItemCode & "','" & mHSNCode & "', '" & MainClass.AllowSingleQuote(mItemDesc) & "'," & mQty & ", " & vbCrLf _
                            & " '" & mUnit & "'," & mRate & "," & mAmount & "," & vbCrLf _
                            & " " & Val(txtMRRNo.Text) & ",TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtBillNo.Text) & "', " & vbCrLf _
                            & " TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                            & " '" & mPONo & "'," & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                            & " '" & LblMKey.Text & "'," & vbCrLf _
                            & " '" & xPURVNO & "',TO_DATE('" & VB6.Format(xPURVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                            & " '" & xPURVNO & "',TO_DATE('" & VB6.Format(xPURVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                            & " " & mPORate & ", '" & mMrrRefType & "'," & Val(CStr(pItemEDAmount)) & ", " & Val(CStr(pItemSTAmount)) & "," & vbCrLf _
                            & " " & mCGSTPer & ", " & mSGSTPer & ", " & mIGSTPer & ", " & vbCrLf _
                            & " " & mCGSTAmount & ", " & mSGSTAmount & ", " & mIGSTAmount & " " & vbCrLf _
                            & " ) "

                        PubDBCn.Execute(SqlStr)
                    End If
                    pInsertRow = True
                End If
                RsTemp.MoveNext()
            Loop
        End If
        UpdateNewDNCNDetail1 = True
        Exit Function
UpdateDetail1:
        UpdateNewDNCNDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function
    Private Function GetDebitQty(ByRef pMRRNo As Double, ByRef pItemCode As String, ByRef pDnCnType As String) As Double
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        If pDnCnType = "R" Then
            SqlStr = "SELECT SUM(DECODE(INVMST.ISSUE_UOM,ID.ITEM_UOM,1,INVMST.UOM_FACTOR) * ID.ITEM_QTY * DECODE(IH.BOOKSUBTYPE,'D',1,-1)) AS QTY "
        Else
            SqlStr = "SELECT SUM(ID.ITEM_QTY * DECODE(IH.BOOKSUBTYPE,'D',1,-1)) AS QTY "
        End If

        SqlStr = SqlStr & vbCrLf & " FROM " & vbCrLf _
            & " FIN_DNCN_HDR IH, FIN_DNCN_DET ID, INV_ITEM_MST INVMST" & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
            & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf _
            & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND ID.MRR_REF_NO=" & pMRRNo & "" & vbCrLf & " AND ID.ITEM_CODE='" & Trim(pItemCode) & "'" & vbCrLf _
            & " AND IH.DNCNTYPE='" & pDnCnType & "' AND CANCELLED='N'  AND APPROVED='Y'" ''('M','R','S')


        SqlStr = SqlStr & vbCrLf _
            & " AND IH.PURVNO <> '" & Trim(Trim(txtVNoPrefix.Text) & VB6.Format(Val(CStr(txtVNo.Text)), "00000") & Trim(txtVNoSuffix.Text)) & "' "

        If pDnCnType = "R" Then
            SqlStr = SqlStr & vbCrLf & " AND IH.DNCNFROM IN ('M')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND IH.DNCNFROM IN ('P')"
        End If

        '    If LblBookCode.text = ConCreditNoteBookCode Then
        '        SqlStr = SqlStr & vbCrLf & " AND IH.ISDESPATCHED='Y'"
        '    End If
        If Trim(LblMKey.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND IH.MKEY<>'" & LblMKey.Text & "'"
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetDebitQty = IIf(IsDBNull(RsTemp.Fields("QTY").Value), 0, RsTemp.Fields("QTY").Value)
        End If
        Exit Function
ErrPart:
        GetDebitQty = 0
    End Function
    Private Function UpdateTDSVoucher(ByRef mDivisionCode As Double, ByRef pJVTMKey As String) As Boolean
        On Error GoTo ErrPart
        Dim mBookCode As String
        Dim SqlStr As String = ""
        Dim mAccountCode As String
        Dim mDrCr As String
        Dim mVAmount As Double
        Dim mVnoStr As String
        Dim mVType As String
        Dim mVNoPrefix As String
        Dim mVNoSuffix As String
        Dim mBookType As String
        Dim mBookSubType As String
        Dim mVNo As String
        Dim mCancelled As String
        Dim pRowNo As Integer
        Dim CurJVMKey As String = ""
        mBookType = VB.Left(ConJournal, 1)
        mBookSubType = VB.Right(ConJournal, 1)
        Dim pAddMode As Boolean
        mVType = "JVT"

        If pJVTMKey = "" Then
            mVNo = GenJVVno(mVType)
            mVNoPrefix = GenPrefixVNo(txtVDate.Text)
            mVNoSuffix = ""
            mVnoStr = mVType & mVNoPrefix & mVNo & mVNoSuffix
            txtJVVNO.Text = mVnoStr
            pAddMode = True
        Else
            mVnoStr = txtJVVNO.Text
            pAddMode = False
        End If

        mCancelled = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mBookCode = CStr(ConJournalBookCode)
        If pAddMode = True Then
            pRowNo = MainClass.AutoGenRowNo("FIN_VOUCHER_HDR", "RowNo", PubDBCn)
            CurJVMKey = VB6.Format(RsCompany.Fields("COMPANY_CODE").Value) & VB6.Format(RsCompany.Fields("FYEAR").Value) & VB6.Format(pRowNo)
            pJVTMKey = CurJVMKey
            SqlStr = " INSERT INTO FIN_VOUCHER_HDR ( " & vbCrLf _
                & " Mkey, COMPANY_CODE, " & vbCrLf _
                & " FYEAR,RowNo, VType, VNoPrefix, VNoSeq, VNoSuffix, " & vbCrLf _
                & " Vno, Vdate, BookType,BookSubType, " & vbCrLf _
                & " BookCode, Narration, CANCELLED, " & vbCrLf _
                & " AddUser, AddDate, ModUser, ModDate,UPDATE_FROM,EXPDATE,IS_REVERSAL_MADE, IS_REVERSAL_VOUCHER, REVERSAL_MKEY ) VALUES ( " & vbCrLf _
                & " '" & CurJVMKey & "'," & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf _
                & " " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf _
                & " " & pRowNo & ", " & vbCrLf & " '" & mVType & "', '" & mVNoPrefix & "', " & vbCrLf & " " & Val(mVNo) & ", '" & mVNoSuffix & "', '" & mVnoStr & "', " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & mBookType & "', '" & mBookSubType & "', " & vbCrLf _
                & " '" & mBookCode & "', '" & MainClass.AllowSingleQuote("") & "', '" & mCancelled & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','','N'," & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'N','N','')"
        Else                ''If MODIFYMode = True Then
            SqlStr = "UPDATE FIN_VOUCHER_HDR SET " & vbCrLf _
                & " Vdate=TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " EXPDATE=TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " Narration='" & MainClass.AllowSingleQuote(txtNarration.Text) & "', " & vbCrLf _
                & " CANCELLED='" & mCancelled & "', " & vbCrLf _
                & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
                & " Where Mkey='" & pJVTMKey & "'"
        End If

        'SqlStr = "UPDATE FIN_VOUCHER_HDR SET " & vbCrLf _
        '        & " Vdate=TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
        '        & " EXPDATE=TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
        '        & " VType= '" & mVType & "'," & vbCrLf _
        '        & " VnoPrefix='" & mVNoPrefix & "', " & vbCrLf _
        '        & " VnoSeq=" & Val(mVNo) & ", " & vbCrLf _
        '        & " VnoSuffix='" & mVNoSuffix & "', " & vbCrLf _
        '        & " Vno='" & mVnoStr & "', " & vbCrLf _
        '        & " BookCode='" & mBookCode & "', " & vbCrLf _
        '        & " Narration='" & MainClass.AllowSingleQuote(txtNarration.Text) & "', " & vbCrLf _
        '        & " CANCELLED='" & mCancelled & "', " & vbCrLf _
        '        & " BookType='" & mBookType & "', " & vbCrLf _
        '        & " BookSubType='" & mBookSubType & "', " & vbCrLf _
        '        & " UPDATE_FROM='N'," & vbCrLf _
        '        & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
        '        & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
        '        & " Where Mkey='" & pJVTMKey & "'"

        PubDBCn.Execute(SqlStr)

        If UpdateJVDetail(pJVTMKey, pRowNo, mBookCode, mVType, mVnoStr, (txtVDate.Text), "", PubDBCn, mDivisionCode) = False Then GoTo ErrPart

        If chkTDS.CheckState = System.Windows.Forms.CheckState.Checked Then
            If UpdateTDSCreditDetail(pJVTMKey, mVnoStr, mBookType, mBookSubType, pAddMode) = False Then GoTo ErrPart
        End If
        '    txtVno.Text = mVNo
        UpdateTDSVoucher = True
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateTDSVoucher = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function GenJVVno(ByRef xBookType As String) As String
        On Error GoTo ERR1
        Dim mVNo1 As String
        Dim SqlStr2 As String
        Dim SqlStr As String = ""
        Dim mBookType As String
        Dim mBookSubType As String
        ''    Call GenPrefixVNo
        ''
        GenJVVno = ""
        mBookType = VB.Left(ConJournal, 1)
        mBookSubType = VB.Right(ConJournal, 1)
        'If ADDMode = True Then
        SqlStr = "SELECT MAX(VNOSeq) From FIN_VOUCHER_HDR WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND BookType='" & mBookType & "'" & vbCrLf _
            & " AND BookSubType='" & mBookSubType & "'" & vbCrLf _
            & " AND VTYPE='" & MainClass.AllowSingleQuote(xBookType) & "'"

        If RsCompany.Fields("CBJVoucherSeq").Value = "D" Then
            SqlStr = SqlStr & vbCrLf & " AND VDATE=TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf RsCompany.Fields("CBJVoucherSeq").Value = "M" Then
            SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(VDATE,'MMMYYYY')=TO_CHAR('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','MMMYYYY')"
        ElseIf RsCompany.Fields("CBJVoucherSeq").Value = "Y" Then

        End If

        GenJVVno = VB6.Format(MainClass.AutoGenVNo(SqlStr, PubDBCn), "00000")
        'End If
        Exit Function
ERR1:
        ErrorMsg(Err.Description)
        'Resume
    End Function
    Private Function UpdateJVDetail(ByRef mMkey As String, ByRef mRowNo As Integer, ByRef mBookCode As String, ByRef mVType As String, ByRef mVNo As String, ByRef mVDate As String, ByRef pNarration As String, ByRef pDBCn As ADODB.Connection, ByRef mDivisionCode As Double) As Boolean
        On Error GoTo ErrDetail
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mAccountName As String
        Dim mAccountCode As String = ""
        Dim mChequeNo As String
        Dim mChqDate As String
        Dim mAmount As Double
        Dim mCCCode As String
        Dim mDeptCode As String
        Dim mEmpCode As String
        Dim mExpCode As String
        Dim mIBRNo As String
        Dim mDC As String
        Dim mRemarks As String
        Dim mPRRowNo As Integer
        Dim mBookType As String
        Dim mBookSubType As String
        Dim mClearDate As String
        Dim mParticulars As String
        mBookType = VB.Left(ConJournal, 1)
        mBookSubType = VB.Right(ConJournal, 1)
        SqlStr = "Delete From FIN_TEMPBILL_TRN Where UserId='" & MainClass.AllowSingleQuote(PubUserID) & "' AND TEMPMKEY=" & pProcessKey & ""
        pDBCn.Execute(SqlStr)
        SqlStr = "Delete From FIN_BILLDETAILS_TRN Where Mkey='" & mMkey & "'"
        pDBCn.Execute(SqlStr)
        SqlStr = "Delete From FIN_VOUCHER_DET Where Mkey='" & mMkey & "'"
        pDBCn.Execute(SqlStr)
        mRemarks = " agt Bill No(s) " & txtBillNo.Text & " Dt. " & txtBillDate.Text
        SqlStr = "DELETE FROM FIN_POSTED_TRN  WHERE " & vbCrLf & " MKEY ='" & mMkey & "' " & vbCrLf & " AND BookType='" & mBookType & "'" & vbCrLf & " AND BooksubType='" & mBookSubType & "'"
        pDBCn.Execute(SqlStr)
        '    Call InsertTempBill(mAccountCode, mAmount, mRemarks)
        '******SUPPLIER ACCOUNT POSTING
        mAccountName = txtSupplier.Text
        If mAccountName <> "" Then
            mPRRowNo = 1
            mDC = "D"
            mAccountCode = IIf(MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", pDBCn, mAccountCode, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True, mAccountCode, -1)
            mAmount = Val(txtTDSAmount.Text) + Val(txtESIAmount.Text) + Val(txtSTDSAmount.Text)
            mParticulars = "Bill No : " & txtBillNo.Text

            If Val(txtTDSAmount.Text) > 0 Then
                mParticulars = mParticulars & " (TDS DEDUCT ON RS. " & VB6.Format(txtTDSDeductOn.Text, "0.000") & " @ " & VB6.Format(txtTDSRate.Text, "0.000") & "%)"
            End If

            If Val(txtESIAmount.Text) > 0 Then
                mParticulars = mParticulars & " (ESI DEDUCT ON RS. " & VB6.Format(txtESIDeductOn.Text, "0.000") & " @ " & VB6.Format(txtESIRate.Text, "0.000") & "%)"
            End If

            If Val(txtSTDSAmount.Text) > 0 Then
                mParticulars = mParticulars & " (STDS DEDUCT ON RS. " & VB6.Format(txtSTDSDeductOn.Text, "0.000") & " @ " & VB6.Format(txtSTDSRate.Text, "0.000") & "%)"
            End If

            mChequeNo = ""
            mChqDate = ""
            mCCCode = "-1"
            mDeptCode = "-1"
            mEmpCode = "-1"
            mExpCode = "-1"
            mIBRNo = "-1"
            mClearDate = ""
            I = 1
            SqlStr = "INSERT INTO FIN_VOUCHER_DET ( " & vbCrLf & " COMPANYCODE, MKey,PRROWNO,SubRowNo,DC,AccountCode, " & vbCrLf & " ChequeNo,ChqDate,CostCCode, " & vbCrLf & " DeptCode,EmpCode,EXP_CODE,IBRNo,Amount,ClearDate,PARTICULARS,DIV_CODE )" & vbCrLf & " VALUES ( " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " '" & mMkey & "', " & mPRRowNo & ", " & vbCrLf & " " & I & ",'" & mDC & "', '" & mAccountCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mChequeNo) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(mChqDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mCCCode & "', '" & mDeptCode & "', '" & mEmpCode & "','" & mExpCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mIBRNo) & "'," & mAmount & "," & vbCrLf & " TO_DATE('" & VB6.Format(mClearDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(mParticulars) & "'," & mDivisionCode & " )"
            PubDBCn.Execute(SqlStr)
            If UpdateSuppPRDetail(pDBCn, mMkey, I, mPRRowNo, mAccountCode, mBookCode, mVType, mBookType, mBookSubType, mVNo, mVDate, mDC, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mClearDate, "N", mParticulars, mAmount, mRemarks, mDivisionCode) = False Then GoTo ErrDetail
        End If
        '******TDS ACCOUNT POSTING
        mPRRowNo = 2
        mDC = "C"
        mAccountCode = GetTDSAccountCode(txtSection.Text)       ''' IIf(IsDBNull(RsCompany.Fields("TDSCREDITACCOUNT").Value), "-1", RsCompany.Fields("TDSCREDITACCOUNT").Value)
        If Trim(mAccountCode) = "" Then
            MsgInformation("TDS Head Not Defined.")
            UpdateJVDetail = False
            Exit Function
        End If
        mParticulars = ""
        mParticulars = "Bill No : " & txtBillNo.Text & " (TDS DEDUCT ON RS. " & VB6.Format(txtTDSDeductOn.Text, "0.000") & " @ " & VB6.Format(txtTDSRate.Text, "0.000") & "%)"
        mAmount = Val(txtTDSAmount.Text)
        mChequeNo = ""
        mChqDate = ""
        mCCCode = "-1"
        mDeptCode = "-1"
        mEmpCode = "-1"
        mExpCode = "-1"
        mIBRNo = "-1"
        mClearDate = ""
        I = 2
        If mAmount > 0 Then
            SqlStr = "INSERT INTO FIN_VOUCHER_DET ( " & vbCrLf & " COMPANYCODE, MKey,PRROWNO,SubRowNo,DC,AccountCode, " & vbCrLf & " ChequeNo,ChqDate,CostCCode, " & vbCrLf & " DeptCode,EmpCode,EXP_CODE,IBRNo,Amount,ClearDate,PARTICULARS,DIV_CODE )" & vbCrLf & " VALUES ( " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " '" & mMkey & "', " & mPRRowNo & ", " & vbCrLf & " " & I & ",'" & mDC & "', '" & mAccountCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mChequeNo) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(mChqDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mCCCode & "', '" & mDeptCode & "', '" & mEmpCode & "', '" & mExpCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mIBRNo) & "'," & mAmount & "," & vbCrLf & " TO_DATE('" & VB6.Format(mClearDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(mParticulars) & "'," & mDivisionCode & " )"
            PubDBCn.Execute(SqlStr)
            If UpdatePRDetail(pDBCn, mMkey, I, mPRRowNo, mAccountCode, mBookCode, mVType, mBookType, mBookSubType, mVNo, mVDate, mDC, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mClearDate, "N", mParticulars, "", IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N"), mBookType, mBookSubType, (txtMRRDate.Text), ADDMode, mAddUser, mAddDate, mDivisionCode, pProcessKey) = False Then GoTo ErrDetail
        End If
        '******ESI ACCOUNT POSTING
        mPRRowNo = 3
        mDC = "C"
        mAccountCode = IIf(IsDBNull(RsCompany.Fields("ESICREDITACCOUNT").Value), "-1", RsCompany.Fields("ESICREDITACCOUNT").Value)
        mParticulars = "Bill No : " & txtBillNo.Text & " (ESI DEDUCT ON RS. " & VB6.Format(txtESIDeductOn.Text, "0.000") & " @ " & VB6.Format(txtESIRate.Text, "0.000") & "%)"
        mAmount = Val(txtESIAmount.Text)
        mChequeNo = ""
        mChqDate = ""
        mCCCode = "-1"
        mDeptCode = "-1"
        mEmpCode = "-1"
        mExpCode = "-1"
        mIBRNo = "-1"
        mClearDate = ""
        I = 3
        If mAmount > 0 Then
            SqlStr = "INSERT INTO FIN_VOUCHER_DET ( " & vbCrLf & " COMPANYCODE, MKey,PRROWNO,SubRowNo,DC,AccountCode, " & vbCrLf & " ChequeNo,ChqDate,CostCCode, " & vbCrLf & " DeptCode,EmpCode,EXP_CODE,IBRNo,Amount,ClearDate,PARTICULARS,DIV_CODE )" & vbCrLf & " VALUES ( " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " '" & mMkey & "', " & mPRRowNo & ", " & vbCrLf & " " & I & ",'" & mDC & "', '" & mAccountCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mChequeNo) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(mChqDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mCCCode & "', '" & mDeptCode & "', '" & mEmpCode & "', '" & mExpCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mIBRNo) & "'," & mAmount & "," & vbCrLf & " TO_DATE('" & VB6.Format(mClearDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(mParticulars) & "'," & mDivisionCode & " )"
            PubDBCn.Execute(SqlStr)
            If UpdatePRDetail(pDBCn, mMkey, I, mPRRowNo, mAccountCode, mBookCode, mVType, mBookType, mBookSubType, mVNo, mVDate, mDC, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mClearDate, "N", mParticulars, "", IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N"), mBookType, mBookSubType, (txtMRRDate.Text), ADDMode, mAddUser, mAddDate, mDivisionCode, pProcessKey) = False Then GoTo ErrDetail
        End If
        '******STDS ACCOUNT POSTING
        mPRRowNo = 4
        mDC = "C"
        mAccountCode = IIf(IsDBNull(RsCompany.Fields("STDSCREDITACCOUNT").Value), "-1", RsCompany.Fields("STDSCREDITACCOUNT").Value)
        mParticulars = "Bill No : " & txtBillNo.Text & " (STDS DEDUCT ON RS. " & VB6.Format(txtSTDSDeductOn.Text, "0.000") & " @ " & VB6.Format(txtSTDSRate.Text, "0.000") & "%)"
        mAmount = Val(txtSTDSAmount.Text)
        mChequeNo = ""
        mChqDate = ""
        mCCCode = "-1"
        mDeptCode = "-1"
        mEmpCode = "-1"
        mExpCode = "-1"
        mIBRNo = "-1"
        mClearDate = ""
        I = 4
        If mAmount > 0 Then
            SqlStr = "INSERT INTO FIN_VOUCHER_DET ( " & vbCrLf & " COMPANYCODE, MKey,PRROWNO,SubRowNo,DC,AccountCode, " & vbCrLf & " ChequeNo,ChqDate,CostCCode, " & vbCrLf & " DeptCode,EmpCode,EXP_CODE,IBRNo,Amount,ClearDate,PARTICULARS,DIV_CODE )" & vbCrLf & " VALUES ( " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " '" & mMkey & "', " & mPRRowNo & ", " & vbCrLf & " " & I & ",'" & mDC & "', '" & mAccountCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mChequeNo) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(mChqDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mCCCode & "', '" & mDeptCode & "', '" & mEmpCode & "', '" & mExpCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(mIBRNo) & "'," & mAmount & "," & vbCrLf & " TO_DATE('" & VB6.Format(mClearDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(mParticulars) & "'," & mDivisionCode & " )"
            PubDBCn.Execute(SqlStr)
            If UpdatePRDetail(pDBCn, mMkey, I, mPRRowNo, mAccountCode, mBookCode, mVType, mBookType, mBookSubType, mVNo, mVDate, mDC, mAmount, mChequeNo, mChqDate, mCCCode, mDeptCode, mEmpCode, mExpCode, mIBRNo, mClearDate, "N", mParticulars, "", IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N"), mBookType, mBookSubType, (txtMRRDate.Text), ADDMode, mAddUser, mAddDate, mDivisionCode, pProcessKey) = False Then GoTo ErrDetail
        End If
        UpdateJVDetail = True
        Exit Function
ErrDetail:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateJVDetail = False
        ''Resume
    End Function
    Public Function UpdateSuppPRDetail(ByRef pDBCn As ADODB.Connection, ByRef pMKey As String, ByRef pRowNo As Integer, ByRef pTRNDtlSubRow As Integer, ByRef pAccountCode As String, ByRef pBookCode As String, ByRef pVType As String, ByRef pBookType As String, ByRef pBookSubType As String, ByRef pVNo As String, ByRef pVDate As String, ByRef pTrnDC As String, ByRef pTrnAmount As Double, ByRef pChequeNo As String, ByRef pChqDate As String, ByRef pCostCCode As String, ByRef pDeptCode As String, ByRef pEmpCode As String, ByRef pExpCode As String, ByRef pIBRNo As String, ByRef pClearDate As String, ByRef pLocked As String, ByRef pNarration As String, ByRef xAmount As Double, ByRef xRemarks As String, ByRef mDivisionCode As Double) As Boolean
        On Error GoTo ErrDetail
        Dim RsTempPRDetail As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim pTRNType As String
        Dim pBillNo As String
        Dim pBillDate As String
        Dim pBillAmount As Double
        Dim pBillDC As String
        Dim pAmount As Double
        Dim pDC As String
        Dim pBillType As String
        Dim pSubRowNo As Integer
        Dim pRemarks As String
        Dim pDueDate As String

        Dim mAccountCode As String = "-1"
        pSubRowNo = 1000 * pRowNo
        pSubRowNo = pSubRowNo + 1
        pTRNType = "T"
        pBillNo = txtBillNo.Text
        pBillDate = txtBillDate.Text
        pBillAmount = Val(lblNetAmount.Text)
        pBillDC = "C"
        pAmount = xAmount
        pDC = "D"
        pRemarks = xRemarks
        pDueDate = txtPaymentdate.Text
        If GetAccountBalancingMethod(pAccountCode, True) = "D" Then
            SqlStr = "INSERT INTO FIN_BILLDETAILS_TRN ( " & vbCrLf _
                & " MKey, TRNDtlSubRowNo ,SubRowNo," & vbCrLf _
                & " AccountCode, TrnType, BillNo, BillDate," & vbCrLf _
                & " BillAmount,BillDc, Amount,Dc,REMARKS,DUEDATE,BILL_TO_LOC_ID,COMPANY_CODE,BILL_COMPANY_CODE,BOOKTYPE ) VALUES ( " & vbCrLf _
                & " '" & pMKey & "', " & pTRNDtlSubRow & "," & pSubRowNo & ", " & vbCrLf _
                & " '" & pAccountCode & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(UCase(pTRNType)) & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(pBillNo) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(pBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & pBillAmount & ", '" & pBillDC & "', " & pAmount & ", '" & pDC & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(pRemarks) & "',TO_DATE('" & VB6.Format(pDueDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') ,'" & MainClass.AllowSingleQuote(txtBillTo.Text) & "'," & RsCompany.Fields("COMPANY_CODE").Value & "," & RsCompany.Fields("COMPANY_CODE").Value & ",'" & pBookType & "')"
            pDBCn.Execute(SqlStr)
        End If
        If pTRNType = "N" Then
            pBillType = "B"
        ElseIf pTRNType = "B" Or pTRNType = "O" Or pTRNType = "A" Then
            pBillType = "P"
        Else
            pBillType = pTRNType
        End If
        mAccountCode = IIf(MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_Name", "SUPP_CUST_Code", "FIN_SUPP_CUST_MST", pDBCn, mAccountCode, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True, mAccountCode, "-1")

        If UpdateTRN(pDBCn, pMKey, pTRNDtlSubRow, pSubRowNo, pBookCode, pVType, pBookType, pBookSubType, pAccountCode, pVNo, pVDate, pBillNo, pBillDate, pAmount, pDC, pTRNType, pChequeNo, pChqDate, pCostCCode, pDeptCode, pEmpCode, pExpCode, pDueDate, pIBRNo, pBillType, pClearDate, pLocked, pNarration, pRemarks, (txtMRRDate.Text), ADDMode, mAddUser, mAddDate, mDivisionCode, txtBillTo.Text) = False Then GoTo ErrDetail
        UpdateSuppPRDetail = True
        Exit Function
ErrDetail:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateSuppPRDetail = False
        'Resume
    End Function
    Private Function UpdateTDSCreditDetail(ByRef pMKey As String, ByRef pVNoStr As String, ByRef pBookType As String, ByRef pBookSubType As String, ByRef pAddMode As Boolean) As Boolean
        On Error GoTo UpdateError
        Dim SqlStr As String = ""
        Dim mTDSAccountCode As String
        Dim mExempted As String
        Dim mSectionCode As Integer
        Dim mAmountPaid As Double
        Dim mTdsRate As Double
        Dim mPartyName As String
        Dim mPartyCode As String
        Dim xAddMode As Boolean
        SqlStr = ""
        'SqlStr = "DELETE FROM TDS_TRN WHERE MKey= '" & pMKey & "'"
        'PubDBCn.Execute(SqlStr)
        If chkTDS.CheckState = System.Windows.Forms.CheckState.Unchecked Or chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
            UpdateTDSCreditDetail = True
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable(pMKey, "MKEY", "MKEY", "TDS_TRN", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xAddMode = False
        Else
            xAddMode = True
        End If

        mTDSAccountCode = GetTDSAccountCode(txtSection.Text)       '' IIf(IsDBNull(RsCompany.Fields("TDSCREDITACCOUNT").Value), "", RsCompany.Fields("TDSCREDITACCOUNT").Value)
        If mTDSAccountCode = "" Then
            ErrorMsg("TDS ACCOUNT Code not Defined into System Pref.", "", MsgBoxStyle.Critical)
            UpdateTDSCreditDetail = False
        End If
        mPartyName = Trim(txtSupplier.Text)
        If MainClass.ValidateWithMasterTable(mPartyName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPartyCode = MasterNo
        Else
            mPartyCode = "-1"
        End If
        'If MainClass.ValidateWithMasterTable(mPartyCode, "SUPP_CUST_CODE", "SECTIONCODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mSectionCode = MasterNo
        'Else
        '    mSectionCode = CInt("-1")
        'End If

        mSectionCode = -1

        If Trim(txtSection.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtSection.Text, "NAME", "CODE", "TDS_SECTION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSectionCode = MasterNo
            End If
        End If

        mAmountPaid = Val(CStr(CDbl(txtTDSDeductOn.Text)))
        mTdsRate = Val(txtTDSRate.Text)
        mExempted = "N"
        If xAddMode = True Then
            SqlStr = "INSERT INTO TDS_TRN ( MKEY, COMPANY_CODE, " & vbCrLf _
                & " FYEAR, ROWNO, SUBROWNO, VNO,VDATE, " & vbCrLf _
                & " BOOKCODE, BOOKTYPE, BOOKSUBTYPE, ACCOUNTCODE, " & vbCrLf _
                & " PARTYCODE,PARTYNAME, AMOUNTPAID, SECTIONCODE, " & vbCrLf _
                & " TDSRATE, ISEXEPTED, EXEPTIONCNO, " & vbCrLf _
                & " TDSAMOUNT, ADDUSER, ADDDATE, MODUSER, MODDATE,UPDATE_FROM) VALUES ( "
            SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(Trim(pMKey)) & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " 1,1,'" & MainClass.AllowSingleQuote(pVNoStr) & "', TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & -1 & ",'" & pBookType & "', '" & pBookSubType & "', " & vbCrLf & " '" & mTDSAccountCode & "', '" & MainClass.AllowSingleQuote(mPartyCode) & "', '" & MainClass.AllowSingleQuote(mPartyName) & "', " & vbCrLf & " " & Val(CStr(mAmountPaid)) & "," & mSectionCode & "," & Val(CStr(mTdsRate)) & ", " & vbCrLf & " '" & mExempted & "','', " & vbCrLf & " " & Val(txtTDSAmount.Text) & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','','N')"
        Else
            SqlStr = " UPDATE TDS_TRN SET " & vbCrLf _
                & " VDATE=TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " ACCOUNTCODE='" & mTDSAccountCode & "', " & vbCrLf _
                & " PARTYCODE='" & MainClass.AllowSingleQuote(mPartyCode) & "', PARTYNAME='" & MainClass.AllowSingleQuote(mPartyName) & "', " & vbCrLf _
                & " VNO='" & MainClass.AllowSingleQuote(pVNoStr) & "', " & vbCrLf _
                & " AMOUNTPAID=" & Val(CStr(mAmountPaid)) & ", " & vbCrLf _
                & " SECTIONCODE=" & mSectionCode & "," & vbCrLf _
                & " TDSRATE=" & Val(CStr(mTdsRate)) & ", " & vbCrLf _
                & " ISEXEPTED='" & mExempted & "', " & vbCrLf _
                & " EXEPTIONCNO='', " & vbCrLf _
                & " TDSAMOUNT=" & Val(txtTDSAmount.Text) & ", UPDATE_FROM='N'," & vbCrLf _
                & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')" & vbCrLf _
                & " WHERE MKey= '" & pMKey & "'"
        End If
        PubDBCn.Execute(SqlStr)
        UpdateTDSCreditDetail = True
        Exit Function
UpdateError:
        UpdateTDSCreditDetail = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Function
    Private Function UpdateDnCnMain(ByRef xPURVNO As String, ByRef xPURVDate As String, ByRef xBillNo As String, ByRef xBillDate As String, ByRef xMRRNo As String, ByRef xMRRDate As String, ByRef xPoNo As String, ByRef xPODate As String, ByRef xCreditDays1 As Integer, ByRef xCreditDays2 As Integer, ByRef xItemDesc As String, ByRef xDnCnType As String, ByRef xCancelled As String, ByRef xBookCode As Integer, ByRef xBookType As String, ByRef xBookSubType As String, ByRef xDebitAccountCode As String, ByRef xCreditAccountCode As String, ByRef xPayDate As String, ByRef xApproved As String, ByRef xDNCNNO As Integer, ByRef pExpDiffDN As Boolean, ByRef mDivisionCode As Double, ByRef cntRow As Integer, Optional ByRef xAmount As Double = 0) As Boolean
        On Error GoTo ErrPart
        Dim xMkey As String = ""
        Dim xCurRowNo As Integer
        Dim SqlStr As String = ""
        Dim xVNoPrefix As String
        Dim xVTYPE As String
        Dim xVNoSeq As Double
        Dim xVNoSuffix As String
        Dim xVNo As String
        Dim xVDate As String
        Dim xItemValue As Double
        Dim xSTPERCENT As Double
        Dim xTOTSTAMT As Double
        Dim xTOTFREIGHT As Double
        Dim xTOTCHARGES As Double
        Dim xEDPERCENT As Double
        Dim xTotEDAmount As Double
        Dim xSURAmount As Double
        Dim xTotDiscount As Double
        Dim xMSC As Double
        Dim xRO As Double
        Dim xTOTEXPAMT As Double
        Dim xTOTTAXABLEAMOUNT As Double
        Dim xNETVALUE As Double
        Dim xTotQty As Double
        Dim xNarration As String
        Dim xReason As String = ""
        Dim nBookCode As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mPDIRItem As String = ""
        Dim mExpAmount As Double
        Dim mDNFROM As String
        'Dim RsTemp As ADODB.Recordset = Nothing
        Dim xCessAmt As Double
        Dim xSHECessAmt As Double
        Dim xTotServiceAmt As Double
        Dim xSTClaimNo As String
        Dim xSTClaimPer As Double
        Dim xSTClaimAmount As Double
        Dim xSTClaimDate As String
        Dim xISSTRefund As String
        Dim xSURVATClaimAmount As Double
        Dim xTOTSURCHARGEAMT As Double
        Dim xTOTVATCLAIMAMT As Double
        Dim pSuppCustCode As String
        Dim pAccountCode As String
        Dim mSubRow As Integer
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mHSNCode As String
        Dim mQty As Double
        Dim mUnit As String
        Dim mRate As Double
        Dim mAmount As Double
        Dim mCGSTPer As Double
        Dim mSGSTPer As Double
        Dim mIGSTPer As Double
        Dim mCGSTAmount As Double
        Dim mSGSTAmount As Double
        Dim mIGSTAmount As Double
        Dim pDNSeqType As Integer
        If xCancelled = "Y" Then UpdateDnCnMain = True : Exit Function
        If xDnCnType = "R" Then
            xVTYPE = IIf(xBookCode = ConDebitNoteBookCode, "DR", "CR")
            mDNFROM = "M"
        Else
            xVTYPE = IIf(xBookCode = ConDebitNoteBookCode, "DN", "CN")
            mDNFROM = "P"
        End If
        xSTClaimNo = ""
        xSTClaimPer = 0
        xSTClaimAmount = 0
        xSTClaimDate = ""
        xISSTRefund = "N"
        xSURVATClaimAmount = 0
        xItemValue = 0
        xSTPERCENT = 0
        xTOTSTAMT = 0
        xTOTFREIGHT = 0
        xTOTCHARGES = 0
        xEDPERCENT = 0
        xTotEDAmount = 0
        xSURAmount = 0
        xTotDiscount = 0
        xMSC = 0
        xRO = 0
        xTOTEXPAMT = 0
        xTOTTAXABLEAMOUNT = 0
        xNETVALUE = 0
        xTotQty = 0
        xNarration = ""
        xTOTSURCHARGEAMT = 0
        xTOTVATCLAIMAMT = 0
        If xDnCnType = "R" Then
            xReason = "REJECTION"
            pDNSeqType = 4
        ElseIf xDnCnType = "S" Then
            xReason = "SHORTAGE"
            pDNSeqType = 1
        ElseIf xDnCnType = "P" Then
            xReason = "RATE DIFF"
            pDNSeqType = 2
        ElseIf xDnCnType = "V" Then
            xReason = "VOLUME DISCOUNT"
            pDNSeqType = 5
        ElseIf xDnCnType = "O" Then
            xNETVALUE = xAmount
            xReason = "PDIR NOT RECEVIED."
            xNarration = "PDIR NOT RECEVIED."
            pDNSeqType = 7

            SqlStr = "SELECT ITEM_CODE " & vbCrLf _
                & " FROM INV_GATE_DET " & vbCrLf _
                & " WHERE " & vbCrLf _
                & " AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & " AND PDIR_FLAG='N'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                Do While Not RsTemp.EOF
                    mPDIRItem = IIf(mPDIRItem = "", "", mPDIRItem & ",") & IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)
                    RsTemp.MoveNext()
                Loop
                xNarration = xNarration & " AGT. ITEM CODE  : " & mPDIRItem & " ( Rs. 200/- each)"
            End If
        End If
        xVNoSeq = CDbl(AutoGenDNCNNo("VNOSEQ", xBookCode, xVTYPE, pDNSeqType))
        ''xVNoSeq = xDNCNNO + AutoGenDNCNNo("VNOSEQ", xBookCode, xVTYPE)
        xVNoPrefix = VB6.Format(IIf(IsDBNull(RsCompany.Fields("INVOICE_PREFIX").Value), "", RsCompany.Fields("INVOICE_PREFIX").Value), "00")
        xVNoSuffix = ""
        xVNo = Trim(xVTYPE) & Trim(xVNoPrefix) & VB6.Format(Val(CStr(xVNoSeq)), "00000") & Trim(xVNoSuffix)
        xVDate = txtVDate.Text
        SqlStr = ""
        If ADDMode = True Then
            xCurRowNo = MainClass.AutoGenRowNo("FIN_DNCN_HDR", "RowNo", PubDBCn)
            xMkey = RsCompany.Fields("COMPANY_CODE").Value & RsCompany.Fields("FYEAR").Value & xCurRowNo
            SqlStr = "INSERT INTO FIN_DNCN_HDR( " & vbCrLf _
                & " MKEY, COMPANY_CODE, FYEAR, ROWNO, " & vbCrLf _
                & " VNOPREFIX, VTYPE,VNOSEQ, VNOSUFFIX, " & vbCrLf _
                & " VNO, VDATE, BILLNO, INVOICE_DATE, " & vbCrLf _
                & " DEBITACCOUNTCODE, CREDITACCOUNTCODE, DUEDAYSFROM, DUEDAYSTO, " & vbCrLf _
                & " BOOKCODE, BookType, BOOKSUBTYPE, REMARKS,  " & vbCrLf _
                & " ITEMDESC, REASON, ITEMVALUE, STPERCENT,  " & vbCrLf _
                & " TOTSTAMT, TOTFREIGHT, TOTCHARGES, EDPERCENT,  " & vbCrLf _
                & " TOTEDAMOUNT, TOTSURCHARGEAMT, TOTDISCAMOUNT, TOTMSCAMOUNT, " & vbCrLf _
                & " TOTRO, TOTEXPAMT, TOTTAXABLEAMOUNT, NETVALUE, " & vbCrLf _
                & " TOTQTY, CANCELLED, NARRATION, DNCNTYPE, APPROVED, PAYDATE, DNCNFROM, " & vbCrLf _
                & " PURVNO, PURVDATE, AUTO_KEY_MRR, MRRDATE, " & vbCrLf _
                & " CUSTREFNO, CUSTREFDATE, MODVATNO, MODVATDATE, " & vbCrLf _
                & " MODVATPER, MODVATAMOUNT, STCLAIMNO, STCLAIMPER, " & vbCrLf _
                & " STCLAIMAMOUNT, STCLAIMDATE, ISMODVAT, ISSTREFUND, " & vbCrLf _
                & " ISDESPATCHED, SALEINVOICENO, SALEINVOICEDATE, " & vbCrLf _
                & " ADDUSER, ADDDATE, MODUSER, MODDATE,UPDATE_FROM,SUR_VATCLAIMAMOUNT, DIV_CODE, ISGSTREFUND, DNCNSEQTYPE,BILL_TO_LOC_ID, SHIP_TO_LOC_ID) "

            SqlStr = SqlStr & vbCrLf _
                & " VALUES('" & xMkey & "'," & RsCompany.Fields("Company_Code").Value & "," & RsCompany.Fields("FYEAR").Value & ", " & xCurRowNo & ", " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(xVNoPrefix) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(xVTYPE) & "', " & vbCrLf _
                & " " & xVNoSeq & ", '" & MainClass.AllowSingleQuote(xVNoSuffix) & "', '" & MainClass.AllowSingleQuote(xVNo) & "',TO_DATE('" & VB6.Format(xVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(xBillNo) & "', TO_DATE('" & VB6.Format(xBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & xDebitAccountCode & "','" & xCreditAccountCode & "', " & Val(CStr(xCreditDays1)) & ", " & Val(CStr(xCreditDays2)) & ", " & vbCrLf _
                & " '" & xBookCode & "', '" & xBookType & "', '" & xBookSubType & "', ''," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(xItemDesc) & "', '" & MainClass.AllowSingleQuote(xReason) & "', " & vbCrLf _
                & " " & xItemValue & ", " & xSTPERCENT & ", " & xTOTSTAMT & ", " & xTOTFREIGHT & ", " & xTOTCHARGES & "," & vbCrLf _
                & " " & xEDPERCENT & ", " & xTotEDAmount & ", " & xSURAmount & ", " & xTotDiscount & "," & xMSC & ", " & vbCrLf _
                & " " & xRO & ", " & xTOTEXPAMT & ", " & xTOTTAXABLEAMOUNT & ", " & xNETVALUE & ", " & vbCrLf _
                & " " & xTotQty & ", '" & xCancelled & "', '" & MainClass.AllowSingleQuote(xNarration) & "', " & vbCrLf _
                & " '" & xDnCnType & "', '" & xApproved & "', TO_DATE('" & VB6.Format(xPayDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & mDNFROM & "', " & vbCrLf _
                & " '" & xPURVNO & "', TO_DATE('" & VB6.Format(xPURVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & xMRRNo & "', TO_DATE('" & VB6.Format(xMRRDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " '', '', '', '', " & vbCrLf _
                & " 100, 0, '" & xSTClaimNo & "', " & xSTClaimPer & ", " & vbCrLf _
                & " " & xSTClaimAmount & ", TO_DATE('" & VB6.Format(xSTClaimDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), 'N', '" & xISSTRefund & "'," & vbCrLf _
                & " 'N','',''," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','','N'," & Val(CStr(xSURVATClaimAmount)) & "," & mDivisionCode & "," & vbCrLf _
                & " '" & IIf(VB.Left(cboGSTStatus.Text, 1) = "R", "Y", "N") & "'," & pDNSeqType & ",'" & MainClass.AllowSingleQuote(txtBillTo.Text) & "','" & MainClass.AllowSingleQuote(TxtShipTo.Text) & "')"

        End If
        PubDBCn.Execute(SqlStr)
        UpdateDnCnMain = True
        If xDnCnType = "O" Then
            If UpdateDNCNPDIRExp1(xMkey, xAmount) = False Then GoTo ErrPart
        Else
            If UpdateDNCNDetail1(xDnCnType, xMkey, xVTYPE, xPURVNO, xPURVDate, cntRow) = False Then GoTo ErrPart
        End If
        If (xDnCnType = "P" Or xDnCnType = "R" Or xDnCnType = "S" Or xDnCnType = "V") Then 'And pExpDiffDN = False
            If UpdateDNCNRateDiffExp1(xDnCnType, xMkey, pExpDiffDN) = False Then GoTo ErrPart
        End If
        If xApproved = "Y" Then
            If UpdateDNCNHDRAPP(xMkey, xDnCnType) = False Then GoTo ErrPart
            nBookCode = CStr(xBookCode)
            SqlStr = "SELECT NETVALUE, TOTEXPAMT, TOTEDAMOUNT,TOTSTAMT,TOTSURCHARGEAMT,SUR_VATCLAIMAMOUNT " & vbCrLf & " FROM FIN_DNCN_HDR" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND MKEY='" & xMkey & "'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                xAmount = IIf(IsDBNull(RsTemp.Fields("NETVALUE").Value), 0, RsTemp.Fields("NETVALUE").Value)
                mExpAmount = IIf(IsDBNull(RsTemp.Fields("TOTEXPAMT").Value), 0, RsTemp.Fields("TOTEXPAMT").Value)
                xTotEDAmount = IIf(IsDBNull(RsTemp.Fields("TOTEDAMOUNT").Value), 0, RsTemp.Fields("TOTEDAMOUNT").Value)
                xTOTSTAMT = IIf(IsDBNull(RsTemp.Fields("TOTSTAMT").Value), 0, RsTemp.Fields("TOTSTAMT").Value)
                xTOTSURCHARGEAMT = IIf(IsDBNull(RsTemp.Fields("TOTSURCHARGEAMT").Value), 0, RsTemp.Fields("TOTSURCHARGEAMT").Value)
                xTOTVATCLAIMAMT = IIf(IsDBNull(RsTemp.Fields("SUR_VATCLAIMAMOUNT").Value), 0, RsTemp.Fields("SUR_VATCLAIMAMOUNT").Value)
            End If
            SqlStr = "Select IX.AMOUNT,IDENTIFICATION " & vbCrLf & " From FIN_DNCN_EXP IX,FIN_INTERFACE_MST IMST" & vbCrLf & " Where " & vbCrLf & " IMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND " & vbCrLf & " IX.ExpCode=IMST.Code " & vbCrLf & " AND IX.Mkey='" & xMkey & "'" & vbCrLf & " AND IDENTIFICATION IN ('EDU','SER','SHC')"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                Do While RsTemp.EOF = False
                    If RsTemp.Fields("Identification").Value = "EDU" Then
                        xCessAmt = IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
                    End If
                    If RsTemp.Fields("Identification").Value = "SER" Then
                        xTotServiceAmt = IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
                    End If
                    If RsTemp.Fields("Identification").Value = "SHC" Then
                        xSHECessAmt = IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
                    End If
                    RsTemp.MoveNext()
                Loop
            End If

            pSuppCustCode = IIf(CDbl(nBookCode) = ConDebitNoteBookCode, xDebitAccountCode, xCreditAccountCode)
            pAccountCode = IIf(CDbl(nBookCode) = ConDebitNoteBookCode, xCreditAccountCode, xDebitAccountCode)

            If DNCNPostTRNGST(PubDBCn, xMkey, xCurRowNo, nBookCode, xBookType, xBookSubType, xVTYPE, xVNo, xVDate, xBillNo, xBillDate, xDebitAccountCode, xCreditAccountCode, Val(CStr(xAmount)), IIf(xCancelled = "Y", True, False), xPayDate, "", xReason, Val(CStr(mExpAmount)), ADDMode, mAddUser, mAddDate, mDivisionCode, "N", 0, 0, 0, xDnCnType, txtBillTo.Text) = False Then GoTo ErrPart

            SqlStr = " SELECT SUBROWNO, ITEM_CODE, HSNCODE, ITEM_QTY,ITEM_UOM, ITEM_RATE, ITEM_AMT, " & vbCrLf & " CGST_PER,SGST_PER,IGST_PER, " & vbCrLf & " CGST_AMOUNT,SGST_AMOUNT,IGST_AMOUNT " & vbCrLf & " FROM FIN_DNCN_DET" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND MKEY='" & xMkey & "'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                Do While RsTemp.EOF = False
                    mSubRow = IIf(IsDBNull(RsTemp.Fields("SUBROWNO").Value), 0, RsTemp.Fields("SUBROWNO").Value)
                    mItemCode = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)
                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "INV_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mItemDesc = MasterNo
                    Else
                        mItemDesc = ""
                    End If
                    mHSNCode = IIf(IsDBNull(RsTemp.Fields("HSNCODE").Value), "", RsTemp.Fields("HSNCODE").Value)
                    mQty = IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)
                    mUnit = IIf(IsDBNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value)
                    mRate = IIf(IsDBNull(RsTemp.Fields("ITEM_RATE").Value), 0, RsTemp.Fields("ITEM_RATE").Value)
                    mAmount = IIf(IsDBNull(RsTemp.Fields("ITEM_AMT").Value), 0, RsTemp.Fields("ITEM_AMT").Value)
                    mCGSTPer = IIf(IsDBNull(RsTemp.Fields("CGST_PER").Value), 0, RsTemp.Fields("CGST_PER").Value)
                    mSGSTPer = IIf(IsDBNull(RsTemp.Fields("SGST_PER").Value), 0, RsTemp.Fields("SGST_PER").Value)
                    mIGSTPer = IIf(IsDBNull(RsTemp.Fields("IGST_PER").Value), 0, RsTemp.Fields("IGST_PER").Value)
                    mCGSTAmount = IIf(IsDBNull(RsTemp.Fields("CGST_AMOUNT").Value), 0, RsTemp.Fields("CGST_AMOUNT").Value)
                    mSGSTAmount = IIf(IsDBNull(RsTemp.Fields("SGST_AMOUNT").Value), 0, RsTemp.Fields("SGST_AMOUNT").Value)
                    mIGSTAmount = IIf(IsDBNull(RsTemp.Fields("IGST_AMOUNT").Value), 0, RsTemp.Fields("IGST_AMOUNT").Value)
                    If VB.Left(cboGSTStatus.Text, 1) = "G" Then
                        If UpdateGSTTRN(PubDBCn, xMkey, nBookCode, xBookType, xBookSubType, xVNo, VB6.Format(xVDate, "DD-MMM-YYYY"), Trim(xBillNo), VB6.Format(xBillDate, "DD-MMM-YYYY"), "", "", pSuppCustCode, pAccountCode, "Y", pSuppCustCode, mSubRow, mItemCode, mQty, mUnit, mRate, mAmount, mAmount, 0, mCGSTPer, mSGSTPer, mIGSTPer, mCGSTAmount, mSGSTAmount, mIGSTAmount, mCGSTAmount, mSGSTAmount, mIGSTAmount, mDivisionCode, mHSNCode, mItemDesc, "", "N", xDnCnType, IIf(lblPurchaseType.Text = "G", "G", "S"), IIf(VB.Left(cboGSTStatus.Text, 1) = "R", "Y", "N"), IIf(xBookCode = ConDebitNoteBookCode, "D", "C"), (lblGSTClaimDate.Text), "N") = False Then GoTo ErrPart
                    End If
                    RsTemp.MoveNext()
                Loop
            End If
        End If
        pDnCnNo = IIf(pDnCnNo = "", "", pDnCnNo & ", ") & xVNo
        Exit Function
ErrPart:
        UpdateDnCnMain = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function
    Private Function UpdateDNCNExp1(ByRef xKey As String) As Boolean
        On Error GoTo UpdateSaleExpErr1
        Dim I As Integer
        Dim mExpCode As Integer
        Dim mPercent As Double
        Dim mExpAmount As Double
        Dim m_AD As String
        Dim mCalcOn As Double
        Dim mRO As String
        Dim SqlStr As String = ""
        PubDBCn.Execute("Delete From FIN_DNCN_EXP Where Mkey='" & xKey & "'")
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
                .Col = ColExpDebitAmt
                mExpAmount = Val(.Text)
                SprdExp.Col = ColExpAddDeduct
                m_AD = SprdExp.Text
                If m_AD = "D" Then
                    '                mExpAmount = mExpAmount * -1
                End If
                SprdExp.Col = ColExpCalcOn
                mCalcOn = Val(.Text)
                .Col = ColRO
                mRO = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")
                SqlStr = ""
                If mExpAmount <> 0 Then
                    SqlStr = "Insert Into  FIN_DNCN_EXP (MKEY,SUBROWNO, " & vbCrLf & " EXPCODE,EXPPERCENT,AMOUNT,CalcOn,RO) " & vbCrLf & " Values ('" & xKey & "'," & I & ", " & vbCrLf & " " & mExpCode & ",0," & mExpAmount & ", " & vbCrLf & " " & mExpAmount & ",'N')"
                    PubDBCn.Execute(SqlStr)
                End If
            Next I
        End With
        UpdateDNCNExp1 = True
        Exit Function
UpdateSaleExpErr1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateDNCNExp1 = False
    End Function
    Private Function UpdateDNCNRateDiffExp1(ByRef pDnCnType As String, ByRef xKey As String, ByRef pExpDiffDN As Boolean) As Boolean
        On Error GoTo UpdateSaleExpErr1
        Dim I As Integer
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mItemValue As Double
        Dim mExpCode As Integer
        Dim mPercent As Double
        Dim mExpAmount As Double
        Dim m_AD As String
        Dim mCalcOn As Double
        Dim mRO As String
        Dim SqlStr As String = ""
        Dim mSql As String
        Dim mIdentification As String = ""
        Dim mTaxableAmount As Double
        Dim mCESSableAmount As Double
        Dim mTaxAmount As Double
        PubDBCn.Execute("Delete From FIN_DNCN_EXP Where Mkey='" & xKey & "'")
        SqlStr = "SELECT SUM(ITEM_AMT) AS ITEM_AMT FROM FIN_DNCN_DET Where Mkey='" & xKey & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            mItemValue = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("ITEM_AMT").Value), 0, RsTemp.Fields("ITEM_AMT").Value), "0.00"))
        End If
        mTaxableAmount = mItemValue
        With SprdExp
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColExpName
                mSql = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
                If MainClass.ValidateWithMasterTable(.Text, "Name", "IDENTIFICATION", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , mSql) = True Then
                    mIdentification = MasterNo
                End If
                If pDnCnType = "P" Or pDnCnType = "V" Then
                    mSql = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STTYPE='C'"
                    If MainClass.ValidateWithMasterTable(.Text, "Name", "Code", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , mSql) = True Then
                        mExpCode = MasterNo
                    Else
                        mExpCode = -1
                    End If
                ElseIf pDnCnType = "S" Then
                    If mIdentification = "ST" Then
                        mSql = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND  STTYPE='C'"
                        If MainClass.ValidateWithMasterTable(.Text, "Name", "Code", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , mSql) = True Then
                            mExpCode = MasterNo
                        Else
                            mExpCode = -1
                        End If
                    Else
                        mSql = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
                        If MainClass.ValidateWithMasterTable(.Text, "Name", "Code", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , mSql) = True Then
                            mExpCode = MasterNo
                        Else
                            mExpCode = -1
                        End If
                    End If
                Else
                    mSql = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND (Type='S' OR Type='B')"
                    If mIdentification = "ED" Then
                    Else
                        If MainClass.ValidateWithMasterTable(.Text, "Name", "Code", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , mSql) = True Then
                            mExpCode = MasterNo
                        Else
                            mExpCode = -1
                        End If
                    End If
                End If
                .Col = ColExpPercent
                mPercent = Val(.Text)
                .Col = ColExpAmt
                If Val(.Text) <> 0 And mPercent = 0 Then
                    If Val(lblTotItemValue.Text) = 0 Then
                        mExpAmount = 0
                    Else
                        mExpAmount = mItemValue * Val(.Text) / Val(lblTotItemValue.Text)
                    End If
                    Select Case mIdentification
                        Case "ED"
                            mCESSableAmount = mExpAmount
                            mTaxableAmount = mTaxableAmount + IIf(mExpCode = -1, 0, mExpAmount) 'mTaxableAmount + mExpAmount
                        Case "EDU"
                            mExpAmount = mCESSableAmount * mPercent / 100
                            mTaxableAmount = mTaxableAmount + IIf(mExpCode = -1, 0, mExpAmount) 'mTaxableAmount + mExpAmount
                        Case "SHC"
                            mExpAmount = mCESSableAmount * mPercent / 100
                            mTaxableAmount = mTaxableAmount + IIf(mExpCode = -1, 0, mExpAmount)
                        Case "SER"
                            mCESSableAmount = mExpAmount
                            mTaxableAmount = mItemValue + mExpAmount
                        Case "ST"
                            mExpAmount = mTaxableAmount * mPercent / 100
                            mTaxAmount = mTaxableAmount * mPercent / 100
                        Case "SUR"
                            mExpAmount = mTaxAmount * mPercent / 100
                        Case Else
                            mExpAmount = mTaxableAmount * mPercent / 100
                    End Select
                ElseIf Val(.Text) <> 0 And mPercent <> 0 Then
                    '                mExpAmount = mItemValue * mPercent / 100
                    Select Case mIdentification
                        Case "ED"
                            mExpAmount = mItemValue * mPercent / 100
                            mCESSableAmount = mExpAmount
                            mTaxableAmount = mTaxableAmount + IIf(mExpCode = -1, 0, mExpAmount) 'mTaxableAmount + mExpAmount
                        Case "EDU"
                            mExpAmount = mCESSableAmount * mPercent / 100
                            mTaxableAmount = mTaxableAmount + IIf(mExpCode = -1, 0, mExpAmount) 'mTaxableAmount + mExpAmount
                        Case "SHC"
                            mExpAmount = mCESSableAmount * mPercent / 100
                            mTaxableAmount = mTaxableAmount + IIf(mExpCode = -1, 0, mExpAmount)
                        Case "SER"
                            mExpAmount = mItemValue * mPercent / 100
                            mCESSableAmount = mExpAmount
                            mTaxableAmount = mItemValue + mExpAmount
                        Case "ST"
                            mExpAmount = mTaxableAmount * mPercent / 100
                            mTaxAmount = mTaxableAmount * mPercent / 100
                        Case "SUR"
                            mExpAmount = mTaxAmount * mPercent / 100
                        Case Else
                            mExpAmount = mTaxableAmount * mPercent / 100
                    End Select
                End If
                SprdExp.Col = ColExpAddDeduct
                m_AD = SprdExp.Text
                If m_AD = "D" Then
                    '                mExpAmount = mExpAmount * -1
                End If
                SprdExp.Col = ColExpCalcOn
                mCalcOn = Val(.Text)
                .Col = ColRO
                mRO = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")
                If pExpDiffDN = True Then
                    mPercent = 0
                    .Col = ColExpDebitAmt
                    If Val(.Text) <> 0 Then
                        mExpAmount = mExpAmount + Val(.Text)
                        .Col = ColExpName
                        If MainClass.ValidateWithMasterTable(.Text, "Name", "Code", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mExpCode = MasterNo
                        Else
                            mExpCode = -1
                        End If
                    End If
                End If
                If mRO = "Y" Then
                    mExpAmount = System.Math.Round(mExpAmount, 0)
                End If
                SqlStr = ""
                If mExpAmount <> 0 And mExpCode <> -1 Then
                    SqlStr = "Insert Into  FIN_DNCN_EXP (MKEY,SUBROWNO, " & vbCrLf & " EXPCODE,EXPPERCENT,AMOUNT,CalcOn,RO) " & vbCrLf & " Values ('" & xKey & "'," & I & ", " & vbCrLf & " " & mExpCode & "," & mPercent & "," & mExpAmount & ", " & vbCrLf & " " & mItemValue & ",'" & mRO & "')"
                    PubDBCn.Execute(SqlStr)
                End If
                mExpAmount = 0
            Next I
        End With
        UpdateDNCNRateDiffExp1 = True
        Exit Function
UpdateSaleExpErr1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateDNCNRateDiffExp1 = False
    End Function
    Private Function UpdateDNCNPDIRExp1(ByRef xKey As String, ByRef xAmount As Double) As Boolean
        On Error GoTo UpdateSaleExpErr1
        Dim I As Integer
        Dim mExpCode As Integer
        Dim mPercent As Double
        Dim mExpAmount As Double
        Dim m_AD As String
        Dim mCalcOn As Double
        Dim mRO As String
        Dim SqlStr As String = ""
        PubDBCn.Execute("Delete From FIN_DNCN_EXP Where Mkey='" & xKey & "'")
        If IsDBNull(RsCompany.Fields("PDIR_ACCOUNT").Value) Then
            MsgBox("PDIR Account Missing, Please Call Administrator....")
            UpdateDNCNPDIRExp1 = False
        End If
        mExpCode = IIf(IsDBNull(RsCompany.Fields("PDIR_ACCOUNT").Value), "-1", RsCompany.Fields("PDIR_ACCOUNT").Value)
        mPercent = 0
        mExpAmount = xAmount
        mCalcOn = xAmount
        mRO = "N"
        SqlStr = ""
        If mCalcOn <> 0 Or mExpAmount <> 0 Then
            SqlStr = "Insert Into  FIN_DNCN_EXP (MKEY,SUBROWNO, " & vbCrLf & " EXPCODE,EXPPERCENT,AMOUNT,CalcOn,RO) " & vbCrLf & " Values ('" & xKey & "',1, " & vbCrLf & " " & mExpCode & "," & mPercent & "," & mExpAmount & ", " & vbCrLf & " " & mCalcOn & ",'" & mRO & "')"
            PubDBCn.Execute(SqlStr)
        End If
        UpdateDNCNPDIRExp1 = True
        Exit Function
UpdateSaleExpErr1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateDNCNPDIRExp1 = False
    End Function
    Private Function CheckValidVDate(ByRef pBillNoSeq As Double) As Boolean
        On Error GoTo CheckERR
        Dim SqlStr As String = ""
        Dim mRsCheck1 As ADODB.Recordset = Nothing
        Dim mRsCheck2 As ADODB.Recordset = Nothing
        Dim mBackBillDate As String = ""
        Dim mMaxInvStrfNo As Integer
        CheckValidVDate = True


        If txtBillNo.Text = "000001" Then Exit Function

        SqlStr = "SELECT MAX(INVOICE_DATE)" & vbCrLf _
            & " FROM FIN_INVOICE_HDR " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND BOOKCode = " & Val(LblBookCode.Text) & " " & vbCrLf _
            & " AND BookType='" & mBookType & "' " & vbCrLf _
            & " AND BookSubType='" & mBookSubType & "' " & vbCrLf _
            & " AND BillNoSeq<" & Val(CStr(pBillNoSeq)) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsCheck2, ADODB.LockTypeEnum.adLockReadOnly)
        If mRsCheck2.EOF = False Then
            mBackBillDate = IIf(IsDBNull(mRsCheck2.Fields(0).Value), mBackBillDate, mRsCheck2.Fields(0).Value)
        End If

        SqlStr = "SELECT MIN(INVOICE_DATE)" & vbCrLf _
            & " FROM FIN_INVOICE_HDR " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND BOOKCode = " & Val(LblBookCode.Text) & " " & vbCrLf _
            & " AND BookType='" & mBookType & "' " & vbCrLf _
            & " AND BookSubType='" & mBookSubType & "' " & vbCrLf _
            & " AND BillNoSeq>" & Val(CStr(pBillNoSeq)) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsCheck1, ADODB.LockTypeEnum.adLockReadOnly)
        If mRsCheck1.EOF = False And Not IsDBNull(mRsCheck1.Fields(0).Value) And mRsCheck2.EOF = False And Not IsDBNull(mRsCheck2.Fields(0).Value) Then
            If CDate(txtBillDate.Text) > CDate(mRsCheck1.Fields(0).Value) Then
                MsgBox("Bill Date Is Greater Than The BillDate Of Next InvoiceNo.")
                CheckValidVDate = False
            ElseIf CDate(txtBillDate.Text) < CDate(mRsCheck2.Fields(0).Value) Then
                MsgBox("Bill Date Is Less Than The BillDate Of Previous InvoiceNo.")
                CheckValidVDate = False
            End If
        ElseIf mRsCheck1.EOF = False And Not IsDBNull(mRsCheck1.Fields(0).Value) Then
            If CDate(txtBillDate.Text) > CDate(mRsCheck1.Fields(0).Value) Then
                MsgBox("Bill Date Is Greater Than The BillDate Of Next InvoiceNo.")
                CheckValidVDate = False
            End If
        ElseIf mRsCheck2.EOF = False And Not IsDBNull(mRsCheck2.Fields(0).Value) Then
            If CDate(txtBillDate.Text) < CDate(mRsCheck2.Fields(0).Value) Then
                MsgBox("Bill Date Is Less Than The BillDate Of Previous InvoiceNo.")
                CheckValidVDate = False
            End If
        End If
        Exit Function
CheckERR:
        CheckValidVDate = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function CheckVoucherDateLock(ByRef pVDate As String, ByRef pMaxDate As String) As Boolean
        On Error GoTo CheckERR
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mLastBillDate As String
        mLastBillDate = RsCompany.Fields("START_DATE").Value
        CheckVoucherDateLock = False
        pMaxDate = ""
        SqlStr = "SELECT MAX(VDATE) AS VDATE" & vbCrLf & " FROM FIN_PURCHASE_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            mLastBillDate = IIf(IsDBNull(RsTemp.Fields("VDATE").Value), mLastBillDate, RsTemp.Fields("VDATE").Value)
        End If
        pMaxDate = mLastBillDate
        If CDate(mLastBillDate) > CDate(pVDate) Then
            CheckVoucherDateLock = True
        End If
        Exit Function
CheckERR:
        CheckVoucherDateLock = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function AutoGenSeqBillNo(ByRef mFieldName As String, ByRef mBookType As String, ByRef mBookSubType As String, ByRef pStartingNo As Integer, ByRef mDivisionCode As Double) As String
        On Error GoTo AutoGenSeqBillNoErr
        Dim RsPurchMainGen As ADODB.Recordset = Nothing
        Dim mNewSeqBillNo As Double
        Dim SqlStr As String = ""
        Dim mSeparateSeries As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mNO As Double
        SqlStr = ""
        If lblPurchaseType.Text = "G" Then
            If chkRejection.CheckState = System.Windows.Forms.CheckState.Checked Then
                pStartingNo = 50001
            Else
                pStartingNo = 1
            End If
        ElseIf lblPurchaseType.Text = "J" Then
            pStartingNo = 70001
        ElseIf lblPurchaseType.Text = "W" Or lblPurchaseType.Text = "R" Then
            pStartingNo = 90001
        End If

        SqlStr = ""
        SqlStr = "SELECT Max(" & mFieldName & ")  FROM FIN_PURCHASE_HDR " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
            & " AND BookType='" & mBookType & "'"
        If mFieldName = "VNOSEQ" Then
            If lblPurchaseType.Text = "G" Then
                If chkRejection.CheckState = System.Windows.Forms.CheckState.Checked Then
                    SqlStr = SqlStr & vbCrLf & "AND BOOKSUBTYPE='R'"
                Else
                    SqlStr = SqlStr & vbCrLf & "AND BOOKSUBTYPE<>'R'"
                End If
                SqlStr = SqlStr & vbCrLf & " AND (FIN_PURCHASE_HDR.PURCHASE_TYPE= 'G' OR FIN_PURCHASE_HDR.PURCHASE_TYPE= '' OR FIN_PURCHASE_HDR.PURCHASE_TYPE IS NULL)"
            ElseIf lblPurchaseType.Text = "W" Or lblPurchaseType.Text = "R" Then
                SqlStr = SqlStr & vbCrLf & "AND PURCHASE_TYPE IN ('W','R')"
            Else
                SqlStr = SqlStr & vbCrLf & "AND PURCHASE_TYPE='" & lblPurchaseType.Text & "'"
            End If
        ElseIf mFieldName = "GST_CLAIM_NO" Then
            SqlStr = SqlStr & vbCrLf & " AND MODVAT_TYPE =1 AND ISGSTAPPLICABLE ='Y'"
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPurchMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsPurchMainGen
            If .EOF = False Then
                mNO = IIf(IsDBNull(.Fields(0).Value), 0, .Fields(0).Value)
                If mNO <= 0 Then
                    mNewSeqBillNo = pStartingNo
                ElseIf Not IsDBNull(.Fields(0).Value) Then
                    mNewSeqBillNo = .Fields(0).Value + 1
                Else
                    mNewSeqBillNo = pStartingNo
                End If
            Else
                mNewSeqBillNo = pStartingNo
            End If
        End With
        AutoGenSeqBillNo = CStr(mNewSeqBillNo)
        Exit Function
AutoGenSeqBillNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function AutoGenSeqBillNoNew(ByRef mFieldName As String, ByRef mBookType As String, ByRef mBookSubType As String, ByRef pStartingNo As Double, ByRef mDivisionCode As Double) As String
        On Error GoTo AutoGenSeqBillNoErr
        Dim RsPurchMainGen As ADODB.Recordset = Nothing
        Dim mNewSeqBillNo As Double
        Dim SqlStr As String = ""
        Dim mSeparateSeries As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xFYear As Integer
        Dim mMaxVNo As Double

        SqlStr = ""
        xFYear = CInt(VB6.Format(RsCompany.Fields("START_DATE").Value, "YY"))

        pStartingNo = IIf(pStartingNo <= 0, 1, pStartingNo)

        pStartingNo = CDbl(xFYear & VB6.Format(IIf(IsDBNull(RsCompany.Fields("INVOICE_PREFIX").Value), "", RsCompany.Fields("INVOICE_PREFIX").Value), "00") & Val(lblPurchaseSeqType.Text) & VB6.Format(pStartingNo, "00000"))

        SqlStr = ""
        SqlStr = "SELECT Max(" & mFieldName & ")  FROM FIN_PURCHASE_HDR " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
            & " AND BookType='" & mBookType & "'"

        If mFieldName = "VNOSEQ" Then
            SqlStr = SqlStr & vbCrLf & "AND PURCHASESEQTYPE='" & lblPurchaseSeqType.Text & "'"
        ElseIf mFieldName = "GST_CLAIM_NO" Then
            SqlStr = SqlStr & vbCrLf & " AND MODVAT_TYPE =1 AND ISGSTAPPLICABLE ='Y'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPurchMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsPurchMainGen
            If .EOF = False Then
                mMaxVNo = IIf(IsDBNull(.Fields(0).Value), 0, .Fields(0).Value)
                If mMaxVNo <= 0 Then
                    mNewSeqBillNo = pStartingNo
                ElseIf Not IsDBNull(.Fields(0).Value) Then
                    mNewSeqBillNo = .Fields(0).Value + 1
                Else
                    mNewSeqBillNo = pStartingNo
                End If
            Else
                mNewSeqBillNo = pStartingNo
            End If
        End With
        AutoGenSeqBillNoNew = CStr(mNewSeqBillNo)
        Exit Function
AutoGenSeqBillNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function AutoGenDNCNNo(ByRef mFieldName As String, ByRef pBookCode As Integer, ByRef pVType As String, ByRef pDNSeqType As Integer) As String
        On Error GoTo AutoGenNoErr
        Dim RsGen As ADODB.Recordset = Nothing
        Dim mNewDNCNNo As Double
        Dim SqlStr As String = ""
        Dim mStartingNo As Double
        Dim xFYear As Integer
        Dim mNO As Double

        SqlStr = ""
        xFYear = CInt(VB6.Format(RsCompany.Fields("START_DATE").Value, "YY"))
        mStartingNo = 1

        mStartingNo = CDbl(xFYear & Val(CStr(pDNSeqType)) & VB6.Format(mStartingNo, "00000"))

        SqlStr = "SELECT Max(" & mFieldName & ")  FROM FIN_DNCN_HDR " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
            & " AND BookCode='" & pBookCode & "' AND VType='" & pVType & "'"

        SqlStr = SqlStr & vbCrLf & " AND DNCNSEQTYPE=" & Val(CStr(pDNSeqType)) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsGen
            If .EOF = False Then
                mNO = IIf(IsDBNull(.Fields(0).Value), 0, .Fields(0).Value)
                If mNO <= 0 Then
                    mNewDNCNNo = mStartingNo
                ElseIf Not IsDBNull(.Fields(0).Value) Then
                    mNewDNCNNo = .Fields(0).Value + 1
                Else
                    mNewDNCNNo = mStartingNo
                End If
            Else
                mNewDNCNNo = mStartingNo
            End If
        End With
        AutoGenDNCNNo = CStr(mNewDNCNNo)
        Exit Function
AutoGenNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function UpdateMRRMain(ByRef pMRRNo As String) As Boolean
        On Error GoTo UpdateDCErr
        Dim xMRRNo As Double
        Dim SqlStr As String = ""
        xMRRNo = Val(txtMRRNo.Text)
        SqlStr = ""
        SqlStr = "UPDATE INV_GATE_HDR SET "
        If CDbl(LblBookCode.Text) = ConModvatBookCode Then
            If chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                SqlStr = SqlStr & vbCrLf & " GST_STATUS='Y',"
            Else
                SqlStr = SqlStr & vbCrLf & " GST_STATUS='N',"
            End If
        ElseIf CDbl(LblBookCode.Text) = ConPurchaseBookCode Then
            If chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                SqlStr = SqlStr & vbCrLf & " MRR_FINAL_FLAG='Y',"
            Else
                SqlStr = SqlStr & vbCrLf & " MRR_FINAL_FLAG='N',"
            End If
        End If
        SqlStr = SqlStr & vbCrLf & " UPDATE_FROM='N'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') "
        SqlStr = SqlStr & vbCrLf & " WHERE AUTO_KEY_MRR=" & Val(CStr(xMRRNo)) & " " & vbCrLf & " AND MRR_DATE=TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND Company_Code=" & RsCompany.Fields("Company_Code").Value & ""
        PubDBCn.Execute(SqlStr)
        UpdateMRRMain = True
        Exit Function
UpdateDCErr:
        UpdateMRRMain = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function DeletePrevious() As Boolean
        On Error GoTo UpdateDCErr
        Dim SqlStr As String = ""
        DeletePrevious = True
        If Trim(lblPMKey.Text) = "" Then Exit Function
        SqlStr = "DELETE FROM FIN_PURCHASE_EXP WHERE MKEY='" & lblPMKey.Text & "'"
        PubDBCn.Execute(SqlStr)
        SqlStr = "DELETE FROM FIN_PURCHASE_DET WHERE MKEY='" & lblPMKey.Text & "'"
        PubDBCn.Execute(SqlStr)
        '    Sqlstr = "DELETE FROM FIN_PURCHASE_TRN WHERE MKEY='" & lblPMKey.text & "'"
        '    PubDBCn.Execute Sqlstr
        SqlStr = "Delete From FIN_GST_POST_TRN Where Mkey='" & lblPMKey.Text & "' AND BookType='" & UCase(mBookType) & "'"
        PubDBCn.Execute(SqlStr)
        SqlStr = "DELETE FROM FIN_PURCHASE_VNO_MST WHERE MKEY='" & lblPMKey.Text & "'"
        PubDBCn.Execute(SqlStr)
        SqlStr = "DELETE FROM FIN_PURCHASE_HDR WHERE MKEY='" & lblPMKey.Text & "'"
        PubDBCn.Execute(SqlStr)
        Exit Function
UpdateDCErr:
        DeletePrevious = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function UpdateDetail1(ByRef xNarration As String, ByRef pAccountCode As String, ByRef pVNo As String, ByRef pSuppCustCode As String, ByRef pShipToSameParty As String, ByRef pShipToSuppCustCode As String, ByRef pDivCode As Double, ByRef pSaleBillNo As String, ByRef pSaleBillDate As String) As Boolean
        On Error GoTo UpdateDetail1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mItemCode As String
        Dim mPartNo As String
        Dim mItemDesc As String
        Dim mQty As Double
        Dim mUnit As String
        Dim mRate As Double
        Dim mAmount As Double
        Dim mExicseableAmt As Double
        Dim mCessableAmt As Double
        Dim mSTableAmt As Double
        Dim mShortageQty As Double
        Dim mRejectQty As Double
        Dim mCESSAmt As Double
        Dim mServiceAmt As Double
        Dim mTotExicseableAmt As Double
        Dim mTotSTableAmt As Double
        Dim mPONo As String
        Dim mTotCessableAmt As Double
        Dim mSHECAmt As Double
        Dim mEDRate As Double
        Dim xIsCancelled As Boolean
        Dim xIsFOC As Boolean
        Dim xIsModvat As String
        Dim xISSTRefund As String
        Dim xISCSTRefund As String
        Dim mIsJobWork As String
        Dim mIsSaleReturn As String
        Dim mApprovedQty As Double
        Dim mOtherAmount As Double
        Dim mHSNCode As String
        Dim pInvType As String
        Dim mInvTypeCode As Double
        Dim mDebitAccountCode As String = ""
        Dim mPODate As String = ""
        Dim mCGSTPer As Double
        Dim mSGSTPer As Double
        Dim mIGSTPer As Double
        Dim mCGSTAmount As Double
        Dim mSGSTAmount As Double
        Dim mIGSTAmount As Double
        Dim mPOS As String
        Dim mState As String
        Dim mRefType As String = ""
        Dim xSuppCustCode As String
        Dim mGSTableAmount As Double
        Dim mItemAdvCGST As Double
        Dim mItemAdvSGST As Double
        Dim mItemAdvIGST As Double
        Dim mPartyGSTNo As String
        Dim mCompanyGSTNo As String

        mCompanyGSTNo = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
        mPartyGSTNo = ""
        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPartyGSTNo = MasterNo
        End If
        xIsCancelled = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, True, False)
        xIsFOC = IIf(chkFOC.CheckState = System.Windows.Forms.CheckState.Checked, True, False)
        xIsModvat = "N" ''IIf(chkModvat.Value = vbChecked, "Y", "N")
        xISSTRefund = "N"
        xISCSTRefund = "N"
        If MainClass.ValidateWithMasterTable((cboInvType.Text), "NAME", "IDENTIFICATION ", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IDENTIFICATION ='J'") = True Then
            mIsJobWork = "Y"
        Else
            mIsJobWork = "N"
        End If
        'If MainClass.ValidateWithMasterTable(cboInvType.Text, "NAME", "ISSALERETURN ", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ISSALERETURN ='Y' AND CATEGORY='P'") = True Then
        If chkRejection.CheckState = System.Windows.Forms.CheckState.Checked Then
            mIsSaleReturn = "Y"
        Else
            mIsSaleReturn = "N"
        End If
        If MainClass.ValidateWithMasterTable((txtMRRNo.Text), "AUTO_KEY_MRR", "REF_TYPE", "INV_GATE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mRefType = MasterNo
        End If
        mTotExicseableAmt = 0
        mTotSTableAmt = 0
        mTotCessableAmt = 0
        PubDBCn.Execute("Delete From FIN_GST_POST_TRN Where Mkey='" & LblMKey.Text & "' AND BookType='" & UCase(mBookType) & "'")
        PubDBCn.Execute("DELETE FROM FIN_PURCHASE_TRN WHERE MKEY='" & LblMKey.Text & "'")
        PubDBCn.Execute("Delete From FIN_PURCHASE_DET Where Mkey='" & LblMKey.Text & "'")

        mPOS = ""
        If pShipToSameParty = "N" Then
            If MainClass.ValidateWithMasterTable(pShipToSuppCustCode, "SUPP_CUST_CODE", "SUPP_CUST_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
                mState = MasterNo
                If MainClass.ValidateWithMasterTable(mState, "NAME", "STATE_CODE", "GEN_STATE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
                    mPOS = MasterNo
                End If
            End If
        End If

        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I
                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)
                .Col = ColPartNo
                mPartNo = MainClass.AllowSingleQuote(.Text)
                .Col = ColItemDesc
                mItemDesc = MainClass.AllowSingleQuote(.Text)
                xNarration = xNarration & IIf(xNarration = "", "", ", ") & mItemDesc
                .Col = ColHSN
                mHSNCode = Trim(.Text)
                .Col = ColQty
                mQty = Val(.Text)
                .Col = ColShortageQty
                mShortageQty = Val(.Text)
                .Col = ColRejectedQty
                mRejectQty = Val(.Text)
                .Col = ColUnit
                mUnit = MainClass.AllowSingleQuote(.Text)
                .Col = ColRate
                mRate = Val(.Text)
                mEDRate = 0
                .Col = ColAmount
                mAmount = Val(.Text)
                .Col = ColTaxableAmount
                mGSTableAmount = Val(.Text)

                .Col = ColPONo
                mPONo = Trim(.Text)
                If mIsSaleReturn = "Y" Then
                    If Val(mPONo) < 0 Then
                        mPODate = txtBillDate.Text
                    Else
                        mPODate = GetSaleInvoiceDate(I, Val(txtMRRNo.Text), mPONo, mItemCode, PubDBCn) 'GetSaleInvoiceDate(1, 0, mPONo, "", PubDBCn) '
                    End If

                    If IsDate(mPODate) Then
                    Else
                        If mPODate = "" And RsCompany.Fields("StockBalCheck").Value = "Y" Then
                            If PubSuperUser = "S" Then
                                mPODate = txtBillDate.Text
                            Else
                                MsgInformation("Sale Invoice No is not Valid of Item Code : " & mItemCode)
                                UpdateDetail1 = False
                                Exit Function
                            End If

                        Else
                            mPODate = txtBillDate.Text
                        End If
                    End If

                End If

                If mTotExicseableAmt = 0 Then
                    mExicseableAmt = 0
                    mCessableAmt = 0
                Else
                    mExicseableAmt = 0 ' Format((Val(lblTotED.text) * mAmount) / mTotExicseableAmt, "0.00")
                    mCessableAmt = 0 ' mExicseableAmt
                End If
                If Val(lblTotItemValue.Text) = 0 Then
                    mServiceAmt = 0
                Else
                    mServiceAmt = 0 '  Format((Val(lblServiceAmount.text) * mAmount) / Val(lblTotItemValue.text), "0.00")
                    mCessableAmt = 0 '  mCessableAmt + mServiceAmt
                End If
                If mTotCessableAmt = 0 Then
                    mCESSAmt = 0
                Else
                    mCESSAmt = 0 'Format((Val(lblEDUAmount.text) * mCessableAmt) / mTotCessableAmt, "0.00")
                End If
                If mTotCessableAmt = 0 Then
                    mSHECAmt = 0
                Else
                    mSHECAmt = 0 ' Format((Val(lblSHEC.text) * mCessableAmt) / mTotCessableAmt, "0.00")
                End If
                If mTotSTableAmt = 0 Then
                    mSTableAmt = 0
                Else
                    mSTableAmt = 0 '  Format((Val(lblTotST.text) * (mAmount + mExicseableAmt + mCESSAmt)) / mTotSTableAmt, "0.00")
                End If
                .Col = ColCGSTPer
                mCGSTPer = Val(.Text)
                .Col = ColSGSTPer
                mSGSTPer = Val(.Text)
                .Col = ColIGSTPer
                mIGSTPer = Val(.Text)
                .Col = ColCGSTAmount
                mCGSTAmount = Val(.Text)
                If Val(txtTotCGSTRefund.Text) = 0 Then
                    mItemAdvCGST = 0
                Else
                    mItemAdvCGST = mCGSTAmount * Val(txtAdvCGST.Text) / Val(txtTotCGSTRefund.Text)
                End If
                .Col = ColSGSTAmount
                mSGSTAmount = Val(.Text)
                If Val(txtTotSGSTRefund.Text) = 0 Then
                    mItemAdvSGST = 0
                Else
                    mItemAdvSGST = mSGSTAmount * Val(txtAdvSGST.Text) / Val(txtTotSGSTRefund.Text)
                End If
                .Col = ColIGSTAmount
                mIGSTAmount = Val(.Text)
                If Val(txtTotIGSTRefund.Text) = 0 Then
                    mItemAdvIGST = 0
                Else
                    mItemAdvIGST = mIGSTAmount * Val(txtAdvIGST.Text) / Val(txtTotIGSTRefund.Text)
                End If
                If CDbl(LblBookCode.Text) = ConPurchaseBookCode Then
                    .Col = ColInvType
                    pInvType = Trim(.Text)
                    If pInvType = "" Then
                        pInvType = "" ''Trim(cboInvType.Text)
                        mDebitAccountCode = pAccountCode
                    Else
                        mDebitAccountCode = GetDebitNameOfInvType(pInvType, "N")
                        If chkFinalPost.CheckState = System.Windows.Forms.CheckState.Checked Then
                            If mDebitAccountCode = "-1" Then MsgBox("Account Code not Defined For Item Code : " & mItemCode) : GoTo UpdateDetail1
                        End If
                    End If
                    If MainClass.ValidateWithMasterTable(pInvType, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mInvTypeCode = MasterNo
                    Else
                        MsgBox("Invoice Type Does Not Exist In Master", MsgBoxStyle.Information)
                        GoTo UpdateDetail1
                    End If
                End If
                SqlStr = ""
                If mItemCode <> "" And mQty > 0 Then
                    SqlStr = " INSERT INTO FIN_PURCHASE_DET ( " & vbCrLf & " MKEY , SUBROWNO, " & vbCrLf & " ITEM_CODE , ITEM_DESC, CUSTOMER_PART_NO, HSNCODE, ITEM_QTY, " & vbCrLf & " ITEM_UOM , ITEM_RATE, ITEM_AMT, " & vbCrLf & " ITEM_ED, ITEM_ST, ITEM_CESS, SHORTAGE_QTY,REJECTED_QTY," & vbCrLf & " CUST_REF_NO, CUST_REF_DATE, COMPANY_CODE,ITEM_SHEC, " & vbCrLf & " PUR_ACCOUNT_CODE,ITEM_ED_PER,ITEM_TRNTYPE, " & vbCrLf & " CGST_PER, SGST_PER, IGST_PER, " & vbCrLf & " CGST_AMOUNT, SGST_AMOUNT, IGST_AMOUNT,GSTABLE_AMT ) "
                    SqlStr = SqlStr & vbCrLf & " VALUES ('" & LblMKey.Text & "'," & I & ", " & vbCrLf & " '" & mItemCode & "','" & MainClass.AllowSingleQuote(mItemDesc) & "','" & mPartNo & "','" & mHSNCode & "', " & mQty & ", " & vbCrLf & " '" & mUnit & "'," & mRate & "," & mAmount & ", " & vbCrLf & " " & mExicseableAmt & ", " & mSTableAmt & ", " & vbCrLf & " " & mCESSAmt & "," & mShortageQty & "," & mRejectQty & ", " & vbCrLf & " '" & mPONo & "',TO_DATE('" & VB6.Format(mPODate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & RsCompany.Fields("COMPANY_CODE").Value & ", " & Val(CStr(mSHECAmt)) & "," & vbCrLf & " '" & MainClass.AllowSingleQuote(mDebitAccountCode) & "'," & Val(CStr(mEDRate)) & ", " & mInvTypeCode & ", " & vbCrLf & " " & mCGSTPer & "," & mSGSTPer & "," & mIGSTPer & "," & vbCrLf & " " & mCGSTAmount & "," & mSGSTAmount & "," & mIGSTAmount & "," & mGSTableAmount & ") "
                    PubDBCn.Execute(SqlStr)
                    mApprovedQty = mQty - mShortageQty - mRejectQty
                    If chkFinalPost.CheckState = System.Windows.Forms.CheckState.Checked And mApprovedQty > 0 And mIsJobWork = "N" And mIsSaleReturn = "N" Then
                        If FinancePurchaseTRN((LblMKey.Text), xIsCancelled, xIsFOC, Val(txtMRRNo.Text), VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY"), Trim(txtBillNo.Text), VB6.Format(txtBillDate.Text, "DD-MMM-YYYY"), Trim(txtVNoPrefix.Text) & Trim(txtVNo.Text), VB6.Format(txtVDate.Text, "DD-MMM-YYYY"), CInt(LblBookCode.Text), xIsModvat, xISSTRefund, xISCSTRefund, I, mItemCode, mUnit, mApprovedQty, mRate, Val(CStr(mExicseableAmt)), mCESSAmt, mSHECAmt, mSTableAmt, mOtherAmount) = False Then GoTo UpdateDetail1
                    End If
                    If chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                        If VB.Left(cboGSTStatus.Text, 1) = "G" And Trim(mPartyGSTNo) <> Trim(mCompanyGSTNo) Then
                            If UpdateGSTTRN(PubDBCn, (LblMKey.Text), LblBookCode.Text, mBookType, mBookSubType, pVNo, VB6.Format(txtVDate.Text, "DD-MMM-YYYY"), Trim(txtBillNo.Text), VB6.Format(txtBillDate.Text, "DD-MMM-YYYY"), "", "", pSuppCustCode, pAccountCode, pShipToSameParty, pShipToSuppCustCode, I, mItemCode, mQty, mUnit, mRate, mQty * mRate, mGSTableAmount, 0, mCGSTPer, mSGSTPer, mIGSTPer, mCGSTAmount, mSGSTAmount, mIGSTAmount, mCGSTAmount, mSGSTAmount, mIGSTAmount, pDivCode, mHSNCode, Trim(mItemDesc), mPOS, "N", mRefType, IIf(lblPurchaseType.Text = "G", "G", "S"), IIf(VB.Left(cboGSTStatus.Text, 1) = "R", "Y", "N"), "C", (lblGSTClaimDate.Text), "N") = False Then GoTo UpdateDetail1
                            '                    ElseIf Left(cboGSTStatus.Text, 1) = "R" Then
                            '                        xSuppCustCode = IIf(IsNull(RsCompany!COMPANY_ACCTCODE), -1, RsCompany!COMPANY_ACCTCODE)
                            '                        If UpdateGSTTRN(PubDBCn, LblMKey.text, LblBookCode, mBookType, mBookSubType, _
                            ''                                        pVNo, Format(TxtVDate.Text, "DD-MMM-YYYY"), Trim(pSaleBillNo), Format(pSaleBillDate, "DD-MMM-YYYY"), "", "", _
                            ''                                        xSuppCustCode, pAccountCode, pShipToSameParty, pShipToSuppCustCode, _
                            ''                                        I, mItemCode, mQty, mUnit, mRate, _
                            ''                                        mQty * mRate, mGSTableAmount, 0, mCGSTPer, mSGSTPer, mIGSTPer, _
                            ''                                        mCGSTAmount, mSGSTAmount, mIGSTAmount, _
                            ''                                        mCGSTAmount, mSGSTAmount, mIGSTAmount, pDivCode, mHSNCode, Trim(mItemDesc), mPOS, "N", _
                            ''                                        mRefType, IIf(lblPurchaseType.text = "G", "G", "S"), "Y", "D", pSaleBillDate, "N" _
                            ''                                        ) = False Then GoTo UpdateDetail1:
                        End If
                    End If
                End If
            Next
        End With
        UpdateDetail1 = True
        UpdateDetail1 = UpdatePurchaseExp1()
        Exit Function
UpdateDetail1:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function
    Private Function GetNarration() As String
        On Error GoTo UpdateDetail1
        Dim I As Integer
        Dim mItemDesc As String
        Dim xNarration As String = ""
        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I
                .Col = ColItemDesc
                mItemDesc = MainClass.AllowSingleQuote(.Text)
                xNarration = xNarration & IIf(xNarration = "", "", ", ") & mItemDesc
            Next
        End With
        xNarration = IIf(xNarration = "", "", IIf(mBookSubType = "J", " ( JobWork of :", " ( Cost of :")) & xNarration & IIf(xNarration = "", "", " )")
        GetNarration = VB.Left(xNarration, 250)
        Exit Function
UpdateDetail1:
        GetNarration = ""
    End Function
    Private Function GetBillBalanceAmt(ByRef pSuppCode As String, ByRef pBillNo As String) As Double
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mSql As String
        mSql = " Sum(AMOUNT*DECODE(DC,'D',1,-1))"
        SqlStr = "SELECT " & vbCrLf & "" & mSql & " AS AMOUNT" & vbCrLf & " FROM FIN_POSTED_TRN "
        SqlStr = SqlStr & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND  ACCOUNTCODE='" & MainClass.AllowSingleQuote(Trim(pSuppCode)) & "'" & vbCrLf & "AND BillNo='" & MainClass.AllowSingleQuote(UCase(Trim(pBillNo))) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetBillBalanceAmt = IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
        Else
            GetBillBalanceAmt = 0
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetBillBalanceAmt = 0
    End Function
    Private Function UpdateDNCNDetail1(ByRef pDnCnType As String, ByRef xKey As String, ByRef pVType As String, ByRef xPURVNO As String, ByRef xPURVDate As String, ByRef cntRow As Integer) As Boolean
        On Error GoTo UpdateDetail1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mQty As Double
        Dim mUnit As String
        Dim mRate As Double
        Dim mPORate As Double
        Dim mAmount As Double
        Dim mPONo As String
        Dim mMrrRefType As String
        Dim pItemEDAmount As Double
        Dim pItemSTAmount As Double
        Dim mFactor As Double
        Dim mEDPer As Double
        Dim mEDAmount As Double
        Dim mItemValue As Double
        Dim mExpCode As Double
        Dim mExpName As String
        Dim mHSNCode As String
        mEDPer = 0
        If pDnCnType = "R" Then
            mEDAmount = 0
            mItemValue = Val(lblTotItemValue.Text)
            For I = 1 To SprdExp.MaxRows
                SprdExp.Row = I
                SprdExp.Col = ColExpName
                mExpName = Trim(SprdExp.Text)
                If MainClass.ValidateWithMasterTable(mExpName, "Name", "Code", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IDENTIFICATION='ED'") = True Then
                    SprdExp.Row = I
                    SprdExp.Col = ColExpAmt
                    mEDAmount = mEDAmount + Val(SprdExp.Text)
                End If
            Next
            If mItemValue <> 0 Then
                mEDPer = CDbl(VB6.Format(mEDAmount * 100 / mItemValue, "0.00"))
            End If
        End If
        PubDBCn.Execute("Delete From FIN_DNCN_DET Where Mkey='" & xKey & "'")
        With SprdMain
            For I = cntRow To cntRow '' .MaxRows - 1
                .Row = I
                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)
                .Col = ColItemDesc
                mItemDesc = MainClass.AllowSingleQuote(.Text)
                .Col = ColUnit
                mUnit = MainClass.AllowSingleQuote(.Text)
                .Col = ColHSN
                mHSNCode = MainClass.AllowSingleQuote(.Text)
                .Col = ColPONo
                mPONo = MainClass.AllowSingleQuote(.Text)
                If pDnCnType = "R" Then
                    SqlStr = "SELECT DECODE(ISSUE_UOM,'" & mUnit & "',1,UOM_FACTOR) AS UOM_FACTOR,ISSUE_UOM FROM INV_ITEM_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
                    mFactor = 1
                    If RsMisc.EOF = False Then
                        mUnit = IIf(IsDBNull(RsMisc.Fields("ISSUE_UOM").Value), "", RsMisc.Fields("ISSUE_UOM").Value)
                        mFactor = IIf(IsDBNull(RsMisc.Fields("UOM_FACTOR").Value), "", RsMisc.Fields("UOM_FACTOR").Value)
                    End If
                    .Col = ColRejectedQty
                    mQty = Val(.Text) * mFactor
                    mQty = mQty - GetDebitQty(Val(txtMRRNo.Text), mItemCode, pDnCnType)
                    .Col = ColRate
                    mRate = Val(.Text) / mFactor
                    If pDnCnType = "R" Then
                        mRate = CDbl(VB6.Format(mRate + (mRate * mEDPer * 0.01), "0.0000"))
                    End If
                    .Col = ColPORate
                    mPORate = Val(.Text) / mFactor
                ElseIf pDnCnType = "S" Then
                    .Col = ColShortageQty
                    mQty = Val(.Text)
                    mQty = mQty - GetDebitQty(Val(txtMRRNo.Text), mItemCode, pDnCnType)
                    .Col = ColRate
                    mRate = Val(.Text)
                ElseIf pDnCnType = "V" Then
                    .Col = ColQty
                    mQty = Val(.Text)
                    .Col = ColRate
                    mRate = Val(.Text)
                ElseIf pDnCnType = "P" Then
                    .Col = ColQty
                    mQty = Val(.Text)
                    .Col = ColRejectedQty
                    mQty = mQty - Val(.Text)
                    .Col = ColShortageQty
                    mQty = mQty - Val(.Text)
                    .Col = ColPORate
                    mPORate = Val(.Text)
                    .Col = ColRate
                    mRate = Val(.Text)
                    If mPORate <> 0 Then
                        If pVType = "DN" Then
                            mRate = IIf(mRate - mPORate <= 0, 0, mRate - mPORate)
                        Else
                            mRate = IIf(mPORate - mRate <= 0, 0, mPORate - mRate)
                        End If
                    Else
                        mRate = 0
                    End If
                End If
                mAmount = mQty * mRate
                mAmount = CDbl(VB6.Format(mAmount, "0.00"))
                If Val(lblTotItemValue.Text) = 0 Then
                    pItemEDAmount = 0
                    pItemSTAmount = 0
                Else
                    pItemEDAmount = 0 '  Val(lblTotED.text) * mAmount / Val(lblTotItemValue.text)
                    pItemSTAmount = 0 '  Val(lblTotST.text) * mAmount / Val(lblTotItemValue.text)
                End If
                mMrrRefType = GetMrrRefNo(Val(txtMRRNo.Text))
                SqlStr = ""
                If mItemCode <> "" And mAmount <> 0 Then
                    SqlStr = " INSERT INTO FIN_DNCN_DET ( " & vbCrLf & " MKEY , SUBROWNO, " & vbCrLf & " ITEM_CODE , ITEM_DESC, HSNCODE, ITEM_QTY, " & vbCrLf & " ITEM_UOM , ITEM_RATE, ITEM_AMT," & vbCrLf & " MRR_REF_NO,MRR_REF_DATE,SUPP_REF_NO," & vbCrLf & " SUPP_REF_DATE, REF_PO_NO, COMPANY_CODE, " & vbCrLf & " PURMKEY, " & vbCrLf & " PURVNO, PURVDATE, " & vbCrLf & " DNCN_REF_NO, DNCN_REF_DATE, " & vbCrLf & " PO_RATE, MRR_REF_TYPE,ITEM_ED, ITEM_ST " & vbCrLf & " ) "
                    SqlStr = SqlStr & vbCrLf & " VALUES ('" & xKey & "'," & I & ", " & vbCrLf & " '" & mItemCode & "','" & MainClass.AllowSingleQuote(mItemDesc) & "','" & mHSNCode & "', " & mQty & ", " & vbCrLf & " '" & mUnit & "'," & mRate & "," & mAmount & "," & vbCrLf & " " & Val(txtMRRNo.Text) & ",TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & MainClass.AllowSingleQuote(txtBillNo.Text) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & mPONo & "'," & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " '" & LblMKey.Text & "'," & vbCrLf & " '" & xPURVNO & "',TO_DATE('" & VB6.Format(xPURVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & xPURVNO & "',TO_DATE('" & VB6.Format(xPURVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " " & mPORate & ", '" & mMrrRefType & "', " & Val(CStr(pItemEDAmount)) & ", " & Val(CStr(pItemSTAmount)) & "" & vbCrLf & " ) "
                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        UpdateDNCNDetail1 = True
        Exit Function
UpdateDetail1:
        UpdateDNCNDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function
    Private Function UpdateDNCNHDRAPP(ByRef xKey As String, ByRef pDnCnType As String) As Boolean
        On Error GoTo UpdateDetail1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim xItemValue As Double
        Dim xSTPERCENT As Double
        Dim xTOTSTAMT As Double
        Dim xTOTFREIGHT As Double
        Dim xTOTCHARGES As Double
        Dim xEDPERCENT As Double
        Dim xTotEDAmount As Double
        Dim xSURAmount As Double
        Dim xTotDiscount As Double
        Dim xMSC As Double
        Dim xRO As Double
        Dim xTOTEXPAMT As Double
        Dim xTOTTAXABLEAMOUNT As Double
        Dim xNETVALUE As Double
        Dim xTotQty As Double
        Dim xVatSurAmt As Double
        Dim xCGSTPer As Double
        Dim xSGSTPer As Double
        Dim xIGSTPer As Double
        Dim xCGSTAmount As Double
        Dim xSGSTAmount As Double
        Dim xIGSTAmount As Double
        Dim xCGSTRefundAmount As Double
        Dim xSGSTRefundAmount As Double
        Dim xIGSTRefundAmount As Double
        SqlStr = "SELECT SUM(ITEM_AMT) AS ITEM_AMT FROM FIN_DNCN_DET Where Mkey='" & xKey & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMisc.EOF = False Then
            xItemValue = CDbl(VB6.Format(IIf(IsDBNull(RsMisc.Fields("ITEM_AMT").Value), 0, RsMisc.Fields("ITEM_AMT").Value), "0.00"))
        End If
        Call CalcTotsDNCN(xKey, pDnCnType, xItemValue, xTOTFREIGHT, xTOTCHARGES, xTotDiscount, xMSC, xRO, xTOTEXPAMT, xNETVALUE, xTotQty, xCGSTPer, xSGSTPer, xIGSTPer, xCGSTAmount, xSGSTAmount, xIGSTAmount, xCGSTRefundAmount, xSGSTRefundAmount, xIGSTRefundAmount)
        SqlStr = "UPDATE FIN_DNCN_HDR SET " & vbCrLf & " ITEMVALUE=" & xItemValue & ", " & vbCrLf & " STPERCENT=" & xSTPERCENT & ",  " & vbCrLf & " TOTSTAMT=" & xTOTSTAMT & ", " & vbCrLf & " TOTFREIGHT=" & xTOTFREIGHT & ", " & vbCrLf & " TOTCHARGES=" & xTOTCHARGES & ", " & vbCrLf & " EDPERCENT=" & xEDPERCENT & ",  " & vbCrLf & " TOTEDAMOUNT=" & xTotEDAmount & ", " & vbCrLf & " TOTSURCHARGEAMT=" & xSURAmount & ", " & vbCrLf & " TOTDISCAMOUNT=" & xTotDiscount & ", " & vbCrLf & " TOTMSCAMOUNT=" & xMSC & ", " & vbCrLf & " TOTRO=" & xRO & ", " & vbCrLf & " TOTEXPAMT=" & xTOTEXPAMT & ", " & vbCrLf & " TOTTAXABLEAMOUNT=" & xTOTTAXABLEAMOUNT & ", " & vbCrLf & " SUR_VATCLAIMAMOUNT=" & xVatSurAmt & ", " & vbCrLf & " NETVALUE=" & xNETVALUE & ", UPDATE_FROM='N'," & vbCrLf & " TOTQTY=" & xTotQty & "," & vbCrLf & " NETCGST_AMOUNT=" & xCGSTAmount & ", " & vbCrLf & " NETSGST_AMOUNT=" & xSGSTAmount & ", " & vbCrLf & " NETIGST_AMOUNT=" & xIGSTAmount & ", " & vbCrLf & " CGST_REFUNDAMOUNT=" & xCGSTRefundAmount & ", " & vbCrLf & " SGST_REFUNDAMOUNT=" & xSGSTRefundAmount & ", " & vbCrLf & " IGST_REFUNDAMOUNT=" & xIGSTRefundAmount & " " & vbCrLf & " Where Mkey='" & xKey & "'"
        PubDBCn.Execute(SqlStr)
        UpdateDNCNHDRAPP = True
        Exit Function
UpdateDetail1:
        UpdateDNCNHDRAPP = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function
    Private Function UpdatePurchaseExp1() As Boolean
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
        PubDBCn.Execute("Delete From FIN_PURCHASE_EXP Where Mkey='" & LblMKey.Text & "'")
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
                .Col = ColExpDebitAmt
                mDebitAmount = CStr(Val(.Text))
                .Col = ColRO
                mRO = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")
                SqlStr = ""
                If mCalcOn <> 0 Or mExpAmount <> 0 Then
                    SqlStr = "Insert Into  FIN_PURCHASE_EXP (MKEY,SUBROWNO, " & vbCrLf & " EXPCODE,EXPPERCENT,AMOUNT,CalcOn,RO,DebitAmount) " & vbCrLf & " Values ('" & LblMKey.Text & "'," & I & ", " & vbCrLf & " " & mExpCode & "," & mPercent & "," & mExpAmount & ", " & vbCrLf & " " & mCalcOn & ",'" & mRO & "'," & mDebitAmount & ")"
                    PubDBCn.Execute(SqlStr)
                End If
            Next I
        End With
        UpdatePurchaseExp1 = True
        Exit Function
UpdateSaleExpErr1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdatePurchaseExp1 = False
    End Function
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim mRow As Integer
        Dim mSTTaxcount As Integer
        Dim cntRow As Integer
        Dim xShortageQty As Double
        Dim xRejectedQty As Double
        Dim xPORate As Double
        Dim xVolDiscRate As Double
        Dim xRate As Double
        Dim xRateDiffDN As Double
        Dim xVolDiscRateDN As Double
        Dim xRateDiffCN As Double
        Dim mExciseDutyAmt As Double
        Dim mServiceTaxAmt As Double
        Dim mEDUAmt As Double
        Dim mSHECAmt As Double
        Dim mSalesTaxAmount As Double
        Dim mWithInState As String
        Dim mWithCountry As String = ""
        Dim xPoNo As String
        Dim mPORateZero As Boolean
        Dim mLockBookCode As Integer
        Dim mAgtPO As Boolean
        Dim mSalesTaxReq As String
        Dim mSectionCode As String
        Dim mPANNo As String
        Dim mRefType As String = ""
        Dim mIsSaleReturn As String = ""
        Dim mItemType As String = ""
        Dim mIsItemCapital As String
        Dim mCapitalInvType As String
        Dim mItemCode As String
        Dim mHeadType As String
        Dim mInterUnit As String = ""
        'Dim mAlreadyRejQty As Double
        Dim pDebitNoteNo As String = ""
        Dim pDebitNoteDate As String = ""
        Dim mItemClassification As String
        Dim mAcctPostName As String
        Dim xSuppCode As String
        Dim mPartyGSTNo As String
        Dim mCompanyGSTNo As String
        Dim pMaxDate As String
        Dim mGSTClass As String
        Dim mItemExempted As Boolean
        Dim mPurpose As String
        Dim mShippFromSameBillFrom As String
        Dim pErrorMsg As String = ""
        mAgtPO = False
        FieldsVarification = True
        If ValidateBranchLocking((txtVDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If
        If CDbl(LblBookCode.Text) = ConModvatBookCode Then
            mLockBookCode = CInt(ConLockModvat)
            If CheckVoucherUnLockApproval(PubDBCn, MainClass.AllowSingleQuote(Trim(txtVNoPrefix.Text)) & Trim(txtVNo.Text), (txtVDate.Text), "") = False Then
                If ValidateBookLocking(PubDBCn, mLockBookCode, txtVDate.Text) = True Then
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        ElseIf CDbl(LblBookCode.Text) = ConPurchaseBookCode And lblSeprateGST.Text = "Y" Then
            mLockBookCode = CInt(ConLockPurchase)
            If CheckVoucherUnLockApproval(PubDBCn, MainClass.AllowSingleQuote(Trim(txtVNoPrefix.Text)) & Trim(txtVNo.Text), (txtVDate.Text), "") = False Then
                If ValidateBookLocking(PubDBCn, mLockBookCode, txtVDate.Text) = True Then
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        ElseIf CDbl(LblBookCode.Text) = ConPurchaseBookCode And lblSeprateGST.Text = "N" Then
            mLockBookCode = CInt(ConLockModvat)
            If CheckVoucherUnLockApproval(PubDBCn, MainClass.AllowSingleQuote(Trim(txtVNoPrefix.Text)) & Trim(txtVNo.Text), (txtVDate.Text), "") = False Then
                If ValidateBookLocking(PubDBCn, mLockBookCode, txtVDate.Text) = True Then
                    FieldsVarification = False
                    Exit Function
                End If
            End If
            mLockBookCode = CInt(ConLockPurchase)
            If CheckVoucherUnLockApproval(PubDBCn, MainClass.AllowSingleQuote(Trim(txtVNoPrefix.Text)) & Trim(txtVNo.Text), (txtVDate.Text), "") = False Then
                If ValidateBookLocking(PubDBCn, mLockBookCode, txtVDate.Text) = True Then
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        End If
        If ValidateAccountLocking(PubDBCn, txtVDate.Text, (txtSupplier.Text), mSupplierCode) = True Then
            FieldsVarification = False
            Exit Function
        End If
        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsPurchMain.EOF = True Then Exit Function
        If CDbl(LblBookCode.Text) = ConPurchaseBookCode Then
            If MainClass.GetUserCanModify(txtVDate.Text) = False Then
                MsgBox("You Have Not Rights to Add or Modify back Voucher", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
        End If

        If CDate(VB6.Format(txtVDate.Text, "DD/MM/YYYY")) > CDate(VB6.Format(PubCurrDate, "DD/MM/YYYY")) Then
            MsgInformation("Voucher Date is Greater Than Current Date. Cannot Save")
            txtVDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If PubUserID = "G0416" Or mIsAuthorisedUser = True Then
        Else
            If chkGSTClaim.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("GST Claim is Taken, So that cann't be Save.")
                FieldsVarification = False
                Exit Function
            End If
            '        If CheckVoucherDateLock(txtVDate.Text, pMaxDate) = True Then
            '             MsgInformation "Working Company has been locked till date : " & pMaxDate & vbCrLf _
            ''                        & "So Unable to Save or Delete. Contact your system administrator."
            '            FieldsVarification = False
            '            Exit Function
            '        End If
        End If
        If MODIFYMode = True And txtVNo.Text = "" Then
            MsgInformation("Voucher No. is Blank")
            FieldsVarification = False
            Exit Function
        End If
        If txtVDate.Text = "" Then
            MsgBox("VDate is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtVDate.Focus()
            Exit Function
        ElseIf FYChk((txtVDate.Text)) = False Then
            FieldsVarification = False
            If txtVDate.Enabled = True Then txtVDate.Focus()
            Exit Function
        End If
        If txtBillNo.Text = "" Then
            MsgInformation("Bill No. is Blank")
            FieldsVarification = False
            Exit Function
        End If
        If txtMRRNo.Text = "" Then
            MsgBox("DCNo is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtMRRNo.Focus()
            Exit Function
        End If
        If txtMRRDate.Text = "" Then
            MsgBox("DCDate is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtMRRDate.Focus()
            Exit Function
            '    ElseIf FYChk(txtMRRDate.Text) = False Then
            '        FieldsVarification = False
            '        txtMRRDate.SetFocus
            '        Exit Function
        End If
        If txtBillDate.Text = "" Then
            MsgBox("BillDate is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtBillDate.Focus()
            Exit Function
        ElseIf Not IsDate(txtBillDate.Text) Then
            MsgBox("Invalid Bill Date", MsgBoxStyle.Information)
            FieldsVarification = False
            txtBillDate.Focus()
            Exit Function
        End If
        If CDate(txtVDate.Text) < CDate(txtMRRDate.Text) Then
            MsgBox("VDate Can Not be Less Than MRRDate.")
            FieldsVarification = False
            txtVDate.Focus()
            Exit Function
        End If
        If CDate(txtVDate.Text) < CDate(txtBillDate.Text) Then
            MsgBox("VDate Can Not be Less Than BillDate.")
            FieldsVarification = False
            If txtBillDate.Enabled = True Then txtBillDate.Focus()
            Exit Function
        End If
        'If chkShipTo.CheckState = System.Windows.Forms.CheckState.Unchecked Then
        '    MsgBox("Bill from & Ship from is different, please save in Ship-to-ship-to Form. ", MsgBoxStyle.Information)
        '    FieldsVarification = False
        '    Exit Function
        'End If
        If Trim(txtSupplier.Text) = "" Then
            MsgBox("Supplier Cannot Be Blank", MsgBoxStyle.Information)
            ' txtSupplier.SetFocus
            FieldsVarification = False
            Exit Function
        End If
        If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Supplier Does Not Exist In Master", MsgBoxStyle.Information)
            'txtSupplier.SetFocus
            FieldsVarification = False
            Exit Function
        Else
            xSuppCode = MasterNo
        End If
        If (CDate(txtMRRDate.Text) < CDate(PubGSTApplicableDate)) Or (CDate(txtBillDate.Text) < CDate(PubGSTApplicableDate)) Then
            MsgInformation("Bill Date Cann't be less Than 01/07/2017, So cann't be Saved in GST.")
            FieldsVarification = False
            Exit Function
        End If
        mCompanyGSTNo = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
        mPartyGSTNo = ""
        If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPartyGSTNo = MasterNo
        End If
        '
        '    If Trim(txtModvatSupp.Text) = "" Then
        '        txtModvatSupp.Text = txtSupplier.Text
        ''        MsgBox "Modvat Supplier Cannot Be Blank", vbInformation
        ''       ' txtSupplier.SetFocus
        ''        FieldsVarification = False
        ''        Exit Function
        '    End If
        '
        '     If MainClass.ValidateWithMasterTable(txtModvatSupp.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
        '        MsgBox "Modvat Supplier Does Not Exist In Master", vbInformation
        '        'txtSupplier.SetFocus
        '        FieldsVarification = False
        '        Exit Function
        '    End If
        If ValidateBillNo((txtBillNo.Text), pErrorMsg) = False Then
            MsgInformation(pErrorMsg)
            FieldsVarification = False
            Exit Function
        End If
        If DuplicateBillNo(xSuppCode, (LblMKey.Text)) = True Then
            MsgBox("Duplicate Bill for this Supplier", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If
        If lblPurchaseSeqType.Text = "9" Then
            If CDate(txtBillDate.Text) > CDate(txtBillDate.Text) Then
                MsgInformation("Bill Date Cann't be greater Than VDate.")
                FieldsVarification = False
                Exit Function
            End If
        End If
        If CDbl(LblBookCode.Text) = ConModvatBookCode Then
            If Trim(txtTariff.Text) = "" Then
                MsgBox("Tariff Heading Cannot Be Blank", MsgBoxStyle.Information)
                txtTariff.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If
        If chkRejection.CheckState = System.Windows.Forms.CheckState.Checked Then
            If CheckCRStockType(mItemType) = False Then
                MsgBox("Please Check Stock Type in MRR. Stock Type should be 'CR' For Prduction or 'ST' for BOP / RM.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
        End If

        Dim mJVTVDate As String

        If Trim(txtJVVNO.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(lblJVTMKey.Text, "MKEY", "VDATE", "FIN_VOUCHER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mJVTVDate = MasterNo
                If CDate(mJVTVDate) <> CDate(txtVDate.Text) Then
                    MsgBox("Cann't be Change Voucher Date. JVT Voucher has been Made.", MsgBoxStyle.Information)
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        End If
        Dim mAccountCode As String
        Dim mAccountName As String

        mItemType = CheckItemType()
        If CDbl(LblBookCode.Text) = ConPurchaseBookCode Then
            If Trim(cboInvType.Text) <> "" Then ''RsCompany.Fields("PURCHASE_POSTINGTYPE").Value = "B" Then
                If MainClass.ValidateWithMasterTable(cboInvType.Text, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                    MsgBox("INVOICE TYPE Does Not Exist In Master", MsgBoxStyle.Information)
                    cboInvType.Focus()
                    FieldsVarification = False
                    Exit Function
                End If


                If MainClass.ValidateWithMasterTable(cboInvType.Text, "NAME", "ISST_REQ", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mSalesTaxReq = MasterNo
                End If
                mAccountName = txtDebitAccount.Text
            Else
                Dim mTRNType As String

                SprdMain.Row = 1
                SprdMain.Col = ColInvType
                Dim mTRNTypeName As String = Trim(SprdMain.Text)
                If MainClass.ValidateWithMasterTable(mTRNTypeName, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mTRNType = MasterNo
                Else
                    mTRNType = CStr(-1)
                    MsgBox("INVOICE TYPE Does Not Exist In Master", MsgBoxStyle.Information)
                    FieldsVarification = False
                    Exit Function
                End If

                If MainClass.ValidateWithMasterTable(mTRNTypeName, "NAME", "ISST_REQ", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mSalesTaxReq = MasterNo
                End If
                mAccountCode = GetDebitNameOfInvType(mTRNTypeName, "N")

                If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                    MsgBox("Debit Account Does Not Exist In Master", MsgBoxStyle.Information)
                    'txtDebitAccount.SetFocus
                    FieldsVarification = False
                    Exit Function
                Else
                    mAccountName = MasterNo
                End If

            End If
            mWithInState = "Y"
            'If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            '    mWithInState = IIf(IsDBNull(MasterNo), "Y", MasterNo)
            'End If

            If Trim(txtSupplier.Text) <> "" Then
                mWithInState = GetPartyBusinessDetail(Trim(xSuppCode), Trim(txtBillTo.Text), "WITHIN_STATE")
            End If

            mWithCountry = "Y"
            If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "WITHIN_COUNTRY", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mWithCountry = IIf(IsDBNull(MasterNo), "Y", MasterNo)
            End If
            If RsCompany.Fields("PURCHASE_POSTINGTYPE").Value = "B" Then
                If MainClass.ValidateWithMasterTable(mAccountName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                    MsgBox("Debit Account Does Not Exist In Master", MsgBoxStyle.Information)
                    'txtDebitAccount.SetFocus
                    FieldsVarification = False
                    Exit Function
                End If
            End If
            '        If Trim(txtItemType.Text) = "" Then
            '            MsgBox "Item Type is Blank", vbInformation
            '            FieldsVarification = False
            '            txtItemType.SetFocus
            '            Exit Function
            '        End If
            mShippFromSameBillFrom = "Y"
            If MainClass.ValidateWithMasterTable(txtMRRNo.Text, "AUTO_KEY_MRR", "SHIPPED_TO_SAMEPARTY", "INV_GATE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mShippFromSameBillFrom = Trim(MasterNo)
            End If
            'If mShippFromSameBillFrom = "N" Then
            '    MsgBox("Bill From & Ship From is not Same, So this voucher could not be save in this format.", MsgBoxStyle.Information)
            '    FieldsVarification = False
            '    Exit Function
            'End If
            If MainClass.ValidateWithMasterTable(txtMRRNo.Text, "AUTO_KEY_MRR", "REF_TYPE", "INV_GATE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mRefType = MasterNo
                If mRefType = "P" Then
                    mAgtPO = True
                Else
                    mAgtPO = False
                End If

                If mRefType = "F" Or mRefType = "C" Then
                    MsgBox("MRR Made Agt. FOC or Cash Purchase, So cann't be Save.", MsgBoxStyle.Information)
                    FieldsVarification = False
                    Exit Function
                End If
            End If
            If (lblPurchaseType.Text = "J" Or lblPurchaseType.Text = "R") And mRefType <> "R" Then
                MsgBox("MRR not Made Agt. RGP, So cann't be Save.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
            If (lblPurchaseType.Text = "J" Or lblPurchaseType.Text = "R") And mRefType = "R" Then
            Else
                If mRefType = "R" Then
                    MsgBox("MRR Made Agt. RGP, So cann't be Save.", MsgBoxStyle.Information)
                    FieldsVarification = False
                    Exit Function
                End If
            End If
            '        If lblPurchaseType.text = "R" And mRefType <> "R" Then
            '            MsgBox "MRR not Made Agt. RGP, So cann't be Save.", vbInformation
            '            FieldsVarification = False
            '            Exit Function
            '        End If
            '
            '
            '        If lblPurchaseType.text <> "R" And mRefType = "R" Then
            '            MsgBox "MRR Made Agt. RGP, So cann't be Save.", vbInformation
            '            FieldsVarification = False
            '            Exit Function
            '        End If
            'If MainClass.ValidateWithMasterTable(cboInvType.Text, "NAME", "ISSALERETURN", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            '    mIsSaleReturn = MasterNo
            'End If
            'If mItemType = "B" Or mItemType = "R" Then
            '    If mIsSaleReturn = "Y" Then
            '        MsgBox("Invaild Invoice Type. BOP/RM Item Cann't be in Sales Return.", MsgBoxStyle.Information)
            '        FieldsVarification = False
            '        Exit Function
            '    End If
            'Else
            '    If mIsSaleReturn = "Y" Then
            '        If mRefType = "I" Or mRefType = "2" Then
            '            If chkRejection.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            '                MsgBox("Please Select Agt D3 Check.", MsgBoxStyle.Information)
            '                FieldsVarification = False
            '                Exit Function
            '            End If
            '        Else
            '            If PubUserID = "G0416" Then
            '            Else
            '                MsgBox("Invaild Invoice Type. MRR Not made is Sales Return.", MsgBoxStyle.Information)
            '                FieldsVarification = False
            '                Exit Function
            '            End If
            '        End If
            '    End If
            '    If mIsSaleReturn = "N" And (mRefType = "I" Or mRefType = "2") Then
            '        MsgBox("Invaild Invoice Type. MRR Not made is Sales Return.", MsgBoxStyle.Information)
            '        FieldsVarification = False
            '        Exit Function
            '    End If
            'End If
        End If
        If Trim(lblSaleBillNo.Text) <> "" Then
            MsgBox("Reverse Charge Sale Bill is Generated, So Cann't be Save.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If
        If txtPaymentdate.Text = "" Then
            MsgBox("Payment Date is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtPaymentdate.Focus()
            Exit Function
        ElseIf Not IsDate(txtPaymentdate.Text) Then
            MsgBox("Invalid Payment Date", MsgBoxStyle.Information)
            FieldsVarification = False
            txtPaymentdate.Focus()
            Exit Function
        End If
        If chkCreditRC.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgBox("Final Credit on Reverse Charge is Done, So that cann't be Modify.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If
        If CDbl(LblBookCode.Text) = ConModvatBookCode And VB.Left(cboGSTStatus.Text, 1) <> "G" Then
            MsgBox("Please check GST Check.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If
        '    If Trim(mPartyGSTNo) = Trim(mCompanyGSTNo) Then
        ''        If (Val(lblTotCGSTAmount.text) + Val(lblTotSGSTAmount.text) + Val(lblTotIGSTAmount.text)) > 0 Then
        ''            MsgBox "GST Amount Should be Zero.", vbInformation
        ''            FieldsVarification = False
        ''            Exit Function
        ''        End If
        '    Else
        '        If Left(cboGSTStatus.Text, 1) = "G" Or Left(cboGSTStatus.Text, 1) = "R" Then
        '            If (Val(lblTotCGSTAmount.text) + Val(lblTotSGSTAmount.text) + Val(lblTotIGSTAmount.text)) = 0 Then
        '                MsgBox "GST Amount Cann't be Zero.", vbInformation
        '                FieldsVarification = False
        '                Exit Function
        '            End If
        '        Else
        '            If (Val(lblTotCGSTAmount.text) + Val(lblTotSGSTAmount.text) + Val(lblTotIGSTAmount.text)) > 0 Then
        '                MsgBox "GST Amount Should not be Zero.", vbInformation
        '                FieldsVarification = False
        '                Exit Function
        '            End If
        '        End If
        '    End If
        '
        If Val(txtTDSRate.Text) > 100 Then
            MsgBox("TDS RATE Cann't be greater than 100.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If
        If Val(txtESIRate.Text) > 100 Then
            MsgBox("ESI RATE Cann't be greater than 100.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If
        If Val(txtSTDSRate.Text) > 100 Then
            MsgBox("STDS RATE Cann't be greater than 100.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If
        If CDbl(LblBookCode.Text) = ConPurchaseBookCode Then
            mWithInState = "Y"
            'If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            '    mWithInState = IIf(IsDBNull(MasterNo), "Y", MasterNo)
            'End If
            If Trim(txtSupplier.Text) <> "" Then
                mWithInState = GetPartyBusinessDetail(Trim(xSuppCode), Trim(txtBillTo.Text), "WITHIN_STATE")
            End If
            '        If Trim(txtItemType.Text) = "" Then
            '            MsgBox "Item Type is Blank", vbInformation
            '            FieldsVarification = False
            '            txtItemType.SetFocus
            '            Exit Function
            '        End If
        End If


        If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "ItemCode Is Blank.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColQty, "N", "Please Check Quantity.") = False Then FieldsVarification = False : Exit Function
        '    If RsCompany!ISEOU = "Y" And Val(txtModvatAmount.Text) <> 0 Then
        '        If MainClass.ValidDataInGrid(SprdMain, ColEDRate, "N", "Please Check Excise Duty Percentage.") = False Then FieldsVarification = False: Exit Function
        '    End If
        Dim mGSTRegd As String = ""
        If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "GST_REGD", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mGSTRegd = IIf(IsDBNull(MasterNo), "N", MasterNo)
        End If
        If cboGSTStatus.SelectedIndex = -1 Then
            MsgBox("Please select GST Status", MsgBoxStyle.Information)
            If cboGSTStatus.Enabled = True Then cboGSTStatus.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If mGSTRegd = "C" And VB.Left(cboGSTStatus.Text, 1) <> "C" Then
            MsgBox("Supplier is registered in Composit Tax, please select the Composit.", MsgBoxStyle.Information)
            ' txtSupplier.SetFocus
            FieldsVarification = False
            Exit Function
        End If
        If mGSTRegd <> "C" And VB.Left(cboGSTStatus.Text, 1) = "C" Then
            MsgBox("Supplier is not registered in Composit Tax, please unselect the Composit.", MsgBoxStyle.Information)
            ' txtSupplier.SetFocus
            FieldsVarification = False
            Exit Function
        End If
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
        ElseIf VB.Left(cboGSTStatus.Text, 1) = "I" Or VB.Left(cboGSTStatus.Text, 1) = "C" Or VB.Left(cboGSTStatus.Text, 1) = "E" Then
            mItemExempted = True
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
                        mItemExempted = True
                    Else
                        mItemExempted = False
                        Exit For
                    End If
                Next
            End With
            If mWithCountry = "N" Then
                If VB.Left(cboGSTStatus.Text, 1) = "G" Or VB.Left(cboGSTStatus.Text, 1) = "I" Then
                    If Trim(txtPortCode.Text) = "" Then
                        MsgBox("Please Enter the Port Code.", MsgBoxStyle.Information)
                        FieldsVarification = False
                        Exit Function
                    End If
                    If Trim(txtBENo.Text) = "" Then
                        MsgBox("Please Enter the Bill Of Entry No.", MsgBoxStyle.Information)
                        FieldsVarification = False
                        Exit Function
                    End If
                    If Trim(txtBEDate.Text) = "" Then
                        MsgBox("Please Enter the Bill Of Entry date.", MsgBoxStyle.Information)
                        FieldsVarification = False
                        Exit Function
                    End If
                    If Trim(txtBEAmount.Text) = "" Then
                        MsgBox("Please Enter the Bill Of Entry Amount.", MsgBoxStyle.Information)
                        FieldsVarification = False
                        Exit Function
                    End If
                End If
            ElseIf mItemExempted = False Then
                If mGSTRegd = "Y" And VB.Left(cboGSTStatus.Text, 1) <> "G" Then
                    MsgBox("Supplier is registered, please select the GST Refund.", MsgBoxStyle.Information)
                    ' txtSupplier.SetFocus
                    FieldsVarification = False
                    Exit Function
                End If
                If mGSTRegd = "N" And VB.Left(cboGSTStatus.Text, 1) <> "R" Then
                    MsgBox("Supplier is not registered, please select the Reverse Charge.", MsgBoxStyle.Information)
                    ' txtSupplier.SetFocus
                    FieldsVarification = False
                    Exit Function
                End If
                If mGSTRegd = "E" And VB.Left(cboGSTStatus.Text, 1) <> "E" Then
                    MsgBox("GST Exempted Supplier, please select the GST Exempted.", MsgBoxStyle.Information)
                    ' txtSupplier.SetFocus
                    FieldsVarification = False
                    Exit Function
                End If
                If Trim(mPartyGSTNo) = Trim(mCompanyGSTNo) Then
                    '        If (Val(lblTotCGSTAmount.text) + Val(lblTotSGSTAmount.text) + Val(lblTotIGSTAmount.text)) > 0 Then
                    '            MsgBox "GST Amount Should be Zero.", vbInformation
                    '            FieldsVarification = False
                    '            Exit Function
                    '        End If
                Else
                    If VB.Left(cboGSTStatus.Text, 1) = "G" Or VB.Left(cboGSTStatus.Text, 1) = "R" Or VB.Left(cboGSTStatus.Text, 1) = "I" Then
                        If (Val(lblTotCGSTAmount.Text) + Val(lblTotSGSTAmount.Text) + Val(lblTotIGSTAmount.Text)) = 0 Then
                            MsgBox("GST Amount Cann't be Zero.", MsgBoxStyle.Information)
                            FieldsVarification = False
                            Exit Function
                        End If
                    Else
                        If (Val(lblTotCGSTAmount.Text) + Val(lblTotSGSTAmount.Text) + Val(lblTotIGSTAmount.Text)) > 0 Then
                            MsgBox("GST Amount Should be Zero.", MsgBoxStyle.Information)
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If
                End If
            End If
        End If
        '    If LblBookCode.text = ConPurchaseBookCode And chkGSTRefund.Value = vbUnchecked And (Val(lblTotCGSTAmount.text) + Val(lblTotSGSTAmount.text) + Val(lblTotIGSTAmount.text)) <> 0 Then
        '        If MsgQuestion("You have not Check in GST. You Want to Continue ...") = vbNo Then
        '            FieldsVarification = False
        '            Exit Function
        '        End If
        '    End If
        '    If LblBookCode.text = ConPurchaseBookCode Then
        '        If (Val(lblTotCGSTAmount.text) + Val(lblTotSGSTAmount.text) + Val(lblTotIGSTAmount.text)) <> (Val(txtTotCGSTRefund.Text) + Val(txtTotSGSTRefund.Text) + Val(txtTotIGSTRefund.Text)) Then
        '            If MsgQuestion("GST Amount And Refund Amount Not Match. You Want to Continue ...") = vbNo Then
        '                FieldsVarification = False
        '                Exit Function
        '            End If
        '        End If
        '    End If
        If CDbl(LblBookCode.Text) = ConPurchaseBookCode And chkFOC.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mPORateZero = False
            mCapitalInvType = "N"
            If MainClass.ValidateWithMasterTable(Trim(cboInvType.Text), "NAME", "ISFIXASSETS", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mCapitalInvType = Trim(IIf(IsDBNull(MasterNo), "N", MasterNo))
            End If
            If ChkCapital.CheckState = System.Windows.Forms.CheckState.Checked And mCapitalInvType = "N" Then
                If MsgQuestion("Invoice Type is not Capital but checked in Capital. You Want to Continue ...") = CStr(MsgBoxResult.No) Then
                    FieldsVarification = False
                    Exit Function
                End If
            ElseIf ChkCapital.CheckState = System.Windows.Forms.CheckState.Unchecked And mCapitalInvType = "Y" Then
                If MsgQuestion("Invoice Type is Capital but not checked in Capital. You Want to Continue ...") = CStr(MsgBoxResult.No) Then
                    FieldsVarification = False
                    Exit Function
                End If
            End If
            With SprdMain
                For cntRow = 1 To .MaxRows - 1
                    .Row = cntRow
                    .Col = ColItemCode
                    mItemCode = Trim(.Text)
                    mItemClassification = ""
                    If MainClass.ValidateWithMasterTable(Trim(mItemCode), "ITEM_CODE", "ITEM_CLASSIFICATION", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mItemClassification = Trim(IIf(IsDBNull(MasterNo), "", MasterNo))
                    End If
                    If mItemClassification = "3" Then
                        .Col = ColPORate
                        xPORate = Val(.Text)
                        .Col = ColRate
                        xRate = Val(.Text)
                        If xPORate <> xRate Then
                            MsgBox("Diesel Rate in Not Match with PO please Check. Cann't be Save.", MsgBoxStyle.Information)
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If
                    mIsItemCapital = GetProductionType(mItemCode)
                    mIsItemCapital = IIf(mIsItemCapital = "A", "Y", "N")
                    If ChkCapital.CheckState = System.Windows.Forms.CheckState.Checked And mIsItemCapital = "N" Then
                        If MsgQuestion("Item Category is not Capital of Item Code [" & mItemCode & "]. You Want to Continue ...") = CStr(MsgBoxResult.No) Then
                            FieldsVarification = False
                            Exit Function
                        End If
                    ElseIf ChkCapital.CheckState = System.Windows.Forms.CheckState.Unchecked And mIsItemCapital = "Y" Then
                        If MsgQuestion("Item Category is Capital of Item Code [" & mItemCode & "]. You Want to Continue ...") = CStr(MsgBoxResult.No) Then
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If
                    .Col = ColShortageQty
                    xShortageQty = xShortageQty + Val(.Text)

                    .Col = ColRejectedQty
                    xRejectedQty = xRejectedQty + Val(.Text)

                    .Col = ColPORate
                    xPORate = Val(.Text)

                    .Col = ColVolDiscRate
                    xVolDiscRate = Val(.Text)

                    .Col = ColPONo
                    xPoNo = CStr(Val(.Text))

                    .Col = ColRate
                    xRate = Val(.Text)

                    If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 103 Then
                        xRate = VB6.Format(xRate, "0.00")
                        xPORate = VB6.Format(xPORate, "0.00")
                    End If

                    If ADDMode = True Then
                        If Val(xPoNo) > 0 And xPORate <> 0 Then
                            If xPORate - xRate <0 Then
                                xRateDiffDN= xRateDiffDN + 1
                            ElseIf xPORate - xRate > 0 Then
                                xRateDiffCN = xRateDiffCN + 1
                            End If
                        End If
                        If Val(xPoNo) > 0 And xVolDiscRate > 0 Then
                            xVolDiscRateDN = xVolDiscRateDN + 1
                        End If
                        If Val(xPoNo) > 0 And xPORate = 0 Then
                            mPORateZero = True
                        End If
                    End If
                    If mAgtPO = True Then
                        If CheckAmount(Val(xPoNo)) = False Then
                            '                        MsgBox "Purchase Amount Cann't be Greater Than PO Amount", vbInformation
                            FieldsVarification = False
                            MainClass.SetFocusToCell(SprdMain, cntRow, ColRate)
                            Exit Function
                        End If
                    End If
                    SprdMain.Row = cntRow
                    SprdMain.Col = ColInvType
                    mAcctPostName = Trim(UCase(SprdMain.Text))      ''IIf(Trim(UCase(SprdMain.Text)) = "", Trim(cboInvType.Text), Trim(UCase(SprdMain.Text)))
                    If mAcctPostName = "" Then
                        MsgInformation("Account Post Name Cann't be Blank.")
                        MainClass.SetFocusToCell(SprdMain, cntRow, ColInvType)
                        FieldsVarification = False
                        Exit Function
                    Else
                        If MainClass.ValidateWithMasterTable(mAcctPostName, "NAME", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P'") = False Then
                            MsgInformation("Invaild Account Post Name.")
                            MainClass.SetFocusToCell(SprdMain, cntRow, ColInvType)
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If
                    Dim mItemGSTClass As String = "0"

                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "GST_ITEMCLASS", "INV_ITEM_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mItemGSTClass = MasterNo
                    End If

                    If mItemGSTClass = "0" Then  ''1 - Non GSt, 2 Exempt
                        SprdMain.Row = cntRow
                        SprdMain.Col = ColHSN
                        If Trim(UCase(SprdMain.Text)) = "" Then
                            MsgInformation("HSN Cann't be Blank.")
                            '                        MainClass.SetFocusToCell SprdMain, I, ColAcctPostName
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If
                    'If VB.Left(cboGSTStatus.Text, 1) = "E" Or VB.Left(cboGSTStatus.Text, 1) = "N" Or VB.Left(cboGSTStatus.Text, 1) = "C" Then
                    '    SprdMain.Row = cntRow
                    '    SprdMain.Col = ColCGSTPer
                    '    SprdMain.Text = "0.00"
                    '    SprdMain.Col = ColSGSTPer
                    '    SprdMain.Text = "0.00"
                    '    SprdMain.Col = ColIGSTPer
                    '    SprdMain.Text = "0.00"
                    '    SprdMain.Col = ColCGSTAmount
                    '    SprdMain.Text = "0.00"
                    '    SprdMain.Col = ColSGSTAmount
                    '    SprdMain.Text = "0.00"
                    '    SprdMain.Col = ColIGSTAmount
                    '    SprdMain.Text = "0.00"
                    'Else
                    '    SprdMain.Row = cntRow
                    '    SprdMain.Col = ColHSN
                    '    If Trim(UCase(SprdMain.Text)) = "" Then
                    '        MsgInformation("HSN Cann't be Blank.")
                    '        '                        MainClass.SetFocusToCell SprdMain, I, ColAcctPostName
                    '        FieldsVarification = False
                    '        Exit Function
                    '    End If
                    'End If
                    If mRefType = "R" And xRate > 0.0001 Then
                        mPurpose = ""
                        If GetValidRGPPurpose(Val(xPoNo), mPurpose) = False Then
                            If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "INTER_UNIT", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                mInterUnit = IIf(IsDBNull(MasterNo), "Y", MasterNo)
                            End If
                            If mInterUnit = "Y" Then
                                If MsgQuestion("RGP Purpose is FOC OR Trail. You Want to Continue ...") = CStr(MsgBoxResult.No) Then
                                    FieldsVarification = False
                                    MainClass.SetFocusToCell(SprdMain, cntRow, ColRate)
                                    Exit Function
                                End If
                            Else
                                MsgBox("RGP Purpose is FOC OR Trail, So Can't be post in Account. RGP NO : " & xPoNo)
                                FieldsVarification = False
                                MainClass.SetFocusToCell(SprdMain, cntRow, ColRate)
                                Exit Function
                            End If
                        Else
                            If lblPurchaseType.Text = "J" And mPurpose <> "B" Then
                                MsgBox("RGP Purpose is not Jobwork, Please check RGP NO : " & xPoNo)
                                FieldsVarification = False
                                Exit Function
                            ElseIf lblPurchaseType.Text = "W" And mPurpose = "B" Then
                                MsgBox("RGP Purpose is not Work Order, Please check RGP NO : " & xPoNo)
                                FieldsVarification = False
                                Exit Function
                            End If
                        End If
                    End If
                Next
            End With
            If mPORateZero = True Then
                If MsgQuestion("Purchase Order rate is Zero. You Want to Continue ...") = CStr(MsgBoxResult.No) Then
                    FieldsVarification = False
                    Exit Function
                End If
            End If
            If RsCompany.Fields("REJECTION_DN").Value = "Y" Then
                If xRejectedQty > 0 And ADDMode = True Then
                    If MsgQuestion("Debit Note Will be Generate of Rejection Qty. You Want to Continue ...") = CStr(MsgBoxResult.No) Then
                        FieldsVarification = False
                        Exit Function
                    Else
                        If CheckDebitNote(pDebitNoteNo, pDebitNoteDate, "R") = True Then
                            If MsgQuestion("Debit Note Already Deduct for this party for such bill. Debit Note No : " & pDebitNoteNo & " - " & pDebitNoteDate & ". You Want to Continue ...") = CStr(MsgBoxResult.No) Then
                                FieldsVarification = False
                                Exit Function
                            End If
                        End If
                    End If
                End If
            End If
            If xShortageQty > 0 And ADDMode = True Then
                If MsgQuestion("Debit Note Will be Generate of Shortage Qty. You Want to Continue ...") = CStr(MsgBoxResult.No) Then
                    FieldsVarification = False
                    Exit Function
                Else
                    If CheckDebitNote(pDebitNoteNo, pDebitNoteDate, "S") = True Then
                        If MsgQuestion("Debit Note Already Deduct for this party for such bill. Debit Note No : " & pDebitNoteNo & " - " & pDebitNoteDate & ". You Want to Continue ...") = CStr(MsgBoxResult.No) Then
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If
                End If
            End If

            Dim mPaymentAmount As Double = 0
            Dim mBalanceAdjPayment As Double = 0
            Dim mBillAdjNo As String

            With SprdPaymentDetail
                For cntRow = 1 To .MaxRows - 1
                    .Row = cntRow

                    .Col = ColPayBillNo
                    mBillAdjNo = Trim(.Text)
                    If mBillAdjNo <> "" Then
                        .Col = ColPayPaymentAmt
                        mPaymentAmount = mPaymentAmount + Val(.Text)

                        .Col = ColPayBalAmount
                        mBalanceAdjPayment = Val(.Text)

                        .Col = ColPayBalDC
                        mBalanceAdjPayment = mBalanceAdjPayment * IIf(Mid(.Text, 1, 1) = "D", 1, -1)

                        .Col = ColPayPaymentAmt
                        If mBalanceAdjPayment < Val(.Text) Then
                            MsgBox("There is no Balance Amount for Adjust. Bill No : " & mBillAdjNo, MsgBoxStyle.Information)
                            FieldsVarification = False
                            Exit Function
                        End If

                    End If
                Next
            End With

            'Private Const ColPayBalAmount As Short = 4
            'Private Const ColPayBalDC As Short = 5
            'Private Const ColPayPaymentAmt As Short = 6

            If mPaymentAmount > Val(lblNetAmount.Text) Then
                MsgBox("Payment Cann't be greater than Bill Amount", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If

            If lblPurchaseSeqType.Text = "2" Or lblPurchaseSeqType.Text = "8" Then

            Else
                If xRateDiffDN >= 1 And ADDMode = True Then
                    If MsgQuestion("Debit Note Will be Generate of Rate Diff. You Want to Continue ...") = CStr(MsgBoxResult.No) Then
                        FieldsVarification = False
                        Exit Function
                    End If
                End If

                If xRateDiffCN >= 1 And ADDMode = True Then
                    If MsgQuestion("Credit Note Will be Generate of Rate Diff. You Want to Continue ...") = CStr(MsgBoxResult.No) Then
                        FieldsVarification = False
                        Exit Function
                    End If
                End If
            End If

            If xVolDiscRateDN >= 1 And ADDMode = True Then
                If MsgQuestion("Debit Note Will be Generate of Volume Discount. You Want to Continue ...") = CStr(MsgBoxResult.No) Then
                    FieldsVarification = False
                    Exit Function
                End If
            End If

            'If ADDMode = True Then
            If chkTDS.CheckState = System.Windows.Forms.CheckState.Checked And Val(txtTDSAmount.Text) = 0 Then
                MsgBox("Please Check TDS Amount.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If

            If chkTDS.CheckState = System.Windows.Forms.CheckState.Checked And Trim(txtSection.Text) = "" Then
                MsgBox("Please Check TDS Section.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If

            If chkTDS.CheckState = System.Windows.Forms.CheckState.Checked Then
                If MainClass.ValidateWithMasterTable(txtSection.Text, "NAME", "CODE", "TDS_SECTION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                    MsgBox("Please Check TDS Section.", MsgBoxStyle.Information)
                    FieldsVarification = False
                    Exit Function
                End If
            End If

            If chkESI.CheckState = System.Windows.Forms.CheckState.Checked And Val(txtESIAmount.Text) = 0 Then
                MsgBox("Please Check ESI Amount.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
            If ChkSTDS.CheckState = System.Windows.Forms.CheckState.Checked And Val(txtSTDSAmount.Text) = 0 Then
                MsgBox("Please Check STDS Amount.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
            'End If
        End If
        If CDbl(LblBookCode.Text) = ConPurchaseBookCode And chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
            If VB.Left(cboGSTStatus.Text, 1) <> "E" Then
                MsgBox("Cann't be Cancelled.(First You Deleted GST Claim.)", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
        End If
        If lblPurchaseType.Text = "J" Or lblPurchaseType.Text = "R" Then
            If Trim(txtServProvided.Text) = "" Then
                MsgBox("Service Provided Cann't be Blank", MsgBoxStyle.Information)
                If txtServProvided.Enabled Then txtServProvided.Focus()
                FieldsVarification = False
                Exit Function
            End If
        Else
            If Trim(txtServProvided.Text) <> "" Then
                MsgBox("You Select Service Provided for Goods.", MsgBoxStyle.Information)
                If txtServProvided.Enabled Then txtServProvided.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If
        If MainClass.ValidateWithMasterTable(Trim(txtDebitAccount.Text), "SUPP_CUST_NAME", "HEADTYPE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mHeadType = Trim(MasterNo)
        End If
        If lblPurchaseType.Text = "J" Or lblPurchaseType.Text = "R" Then ''If mHeadType = "4" Then
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
            '        If Val(txtServiceOn.Text) = 0 Then
            '            MsgBox "Please Enter the Service Tax On, So cann't be Saved.", vbInformation
            '            FieldsVarification = False
            '            Exit Function
            '        End If
            '
            '        If Val(txtServiceTaxPer.Text) = 0 Then
            '            MsgBox "Please Enter the Service Tax Per, So cann't be Saved.", vbInformation
            '            FieldsVarification = False
            '            Exit Function
            '        End If
            '
            '        If Val(txtProviderPer.Text) + Val(txtRecipientPer.Text) <> 100 Then
            '            MsgBox "Provider & Recipient Service Percent is not Equal to 100, So cann't be Saved.", vbInformation
            '            FieldsVarification = False
            '            Exit Function
            '        End If
        End If
        'If ADDMode = True Then
        If chkTDS.CheckState = System.Windows.Forms.CheckState.Checked Then
            If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "PAN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mPANNo = MasterNo
            Else
                mPANNo = ""
            End If
            If Trim(mPANNo) = "" Then
                MsgBox("PAN NO is not defined in Master, So cann't be Saved.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
            'If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "SECTIONCODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            '    mSectionCode = MasterNo
            'Else
            '    mSectionCode = ""
            'End If
            'If Trim(mSectionCode) = "" Or Trim(mSectionCode) = "-1" Then
            '    MsgBox("TDS Section not defined in Master, So cann't be Saved.", MsgBoxStyle.Information)
            '    FieldsVarification = False
            '    Exit Function
            'End If
        End If
        'End If
        Dim mTDSRequired As String = "N"
        Dim mPurchaseTDSApp As Boolean = False
        Dim mTDSNotRequired As String = "N"

        If lblPurchaseSeqType.Text = "1" Or lblPurchaseSeqType.Text = "3" Or lblPurchaseSeqType.Text = "8" Then
            If lblPurchaseSeqType.Text = "8" And RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then
                mPurchaseTDSApp = False
            Else
                If CheckFullLotRejection() = True Then
                    mPurchaseTDSApp = False
                Else
                    mPurchaseTDSApp = True
                End If
            End If
        End If

        If ADDMode = True And mPurchaseTDSApp = True Then

            Dim mTurnOver As Double
            Dim mWithinCountry As String = "Y"
            Dim pInterUnit As String = "N"

            If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "INTER_UNIT", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND INTER_UNIT='N'") = True Then
                pInterUnit = IIf(IsDBNull(MasterNo), "Y", MasterNo)
            Else
                pInterUnit = "Y"
            End If

            If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "WITHIN_COUNTRY", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mWithinCountry = IIf(IsDBNull(MasterNo), "Y", MasterNo)
            End If

            If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "TDS_NOT_UNDER_194Q", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mTDSNotRequired = IIf(IsDBNull(MasterNo), "N", MasterNo)
            End If

            If mWithinCountry = "Y" And pInterUnit = "N" And mTDSNotRequired = "N" Then
                If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "TDS_UNDER_194Q", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mTDSRequired = IIf(IsDBNull(MasterNo), "N", MasterNo)
                End If

                If Val(txtTDSAmount.Text) = 0 Then


                    If mTDSRequired = "N" Then
                        If CDate(txtVDate.Text) >= CDate("01/10/2022") Then
                            mTurnOver = GetPANWiseTurnOver(PubDBCn, txtSupplier.Text)
                            If ADDMode = True Then
                                mTurnOver = mTurnOver + Val(lblTotItemValue.Text)
                            End If
                            If mTurnOver >= 5000000 Then
                                MsgBox("Trunover Exceed from 50 Lakhs, Please deduct the TDS under section 194 Q.", vbInformation)
                                FieldsVarification = False
                                Exit Function
                            End If
                        End If
                    ElseIf mTDSRequired = "Y" Then
                        MsgBox("Please deduct the TDS under section 194 Q.", vbInformation)
                        FieldsVarification = False
                        Exit Function
                    End If
                End If
            End If
        End If

        If VB.Left(cboGSTStatus.Text, 1) = "E" Or VB.Left(cboGSTStatus.Text, 1) = "N" Or VB.Left(cboGSTStatus.Text, 1) = "C" Then
            Call CalcTots()
        End If

        If mBookSubType = "R" Then
            If Val(txtAdvAdjust.Text) > 0 Then
                MsgBox("Advance Balance cann't adjust with Sales Return.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
        End If
        If Val(txtAdvBal.Text) > 0 And Val(txtAdvAdjust.Text) = 0 Then
            If MsgQuestion("Party has advance Payment, Want to adjust with this voucher.") = CStr(MsgBoxResult.Yes) Then
                FieldsVarification = False
                Exit Function
            End If
        End If
        If Val(txtAdvBal.Text) > 0 Then
            If Val(txtAdvBal.Text) < Val(txtAdvAdjust.Text) Then
                MsgBox("Advance Balance is Less than Advnace Adjusted, So cann't be Saved.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
        End If
        Exit Function
err_Renamed:
        FieldsVarification = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Function
    Private Function DuplicateBillNo(ByRef pSuppCode As String, ByRef pMKey As String) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mBillFyear As Integer
        Dim mCount As Integer
        Dim mAcctBillFYear As Integer
        DuplicateBillNo = False
        mCount = 0 ''AND FYEAR=" & RsCompany.fields("FYEAR").value & "
        SqlStr = "SELECT BILLNO, BILLDATE  " & vbCrLf & " FROM FIN_POSTED_TRN" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND BILLTYPE='B'" & vbCrLf & " AND ACCOUNTCODE='" & pSuppCode & "' AND LOCATION_ID='" & txtBillTo.Text & "'" & vbCrLf & " AND BILLNO='" & Trim(txtBillNo.Text) & "'"
        SqlStr = SqlStr & vbCrLf & " AND BOOKCODE NOT IN (-10,-2)"
        If ADDMode = True Then
        Else
            SqlStr = SqlStr & vbCrLf & " AND MKEY<>'" & pMKey & "'"
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        mAcctBillFYear = GetCurrentFYNo(PubDBCn, (txtBillDate.Text))
        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mBillNo = IIf(IsDBNull(RsTemp.Fields("BILLNO").Value), "", RsTemp.Fields("BILLNO").Value)
                mBillDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("BILLDATE").Value), "", RsTemp.Fields("BILLDATE").Value), "DD/MM/YYYY")
                If CDate(txtBillDate.Text) = CDate(mBillDate) Then
                    DuplicateBillNo = True
                    Exit Function
                Else
                    mBillFyear = GetCurrentFYNo(PubDBCn, mBillDate)
                    If mAcctBillFYear = mBillFyear Then
                        mCount = mCount + 1
                    End If
                End If
                RsTemp.MoveNext()
            Loop
        End If
        If mCount > 0 Then
            DuplicateBillNo = True
            Exit Function
        End If
        Exit Function
ErrPart:
        DuplicateBillNo = False
    End Function
    Private Function CheckDebitNote(ByRef pDebitNoteNo As String, ByRef pDebitNoteDate As String, ByRef pDnCnType As String) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mSql As String
        CheckDebitNote = False

        pDebitNoteNo = ""
        pDebitNoteDate = ""

        SqlStr = "SELECT VNO, VDATE " & vbCrLf & " FROM FIN_DNCN_HDR IH, FIN_DNCN_DET ID"

        SqlStr = SqlStr & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.MKEY=ID.MKEY " & vbCrLf _
            & " AND ID.MRR_REF_NO=" & Val(txtMRRNo.Text) & "" & vbCrLf _
            & " AND IH.DNCNTYPE='" & pDnCnType _
            & "' AND IH.APPROVED='Y' AND IH.CANCELLED='N'"

        If pDnCnType = "R" Then
            SqlStr = SqlStr & vbCrLf & " AND IH.DNCNFROM IN ('M')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND IH.DNCNFROM IN ('P')"
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                pDebitNoteNo = IIf(pDebitNoteNo = "", "", pDebitNoteNo & ",") & IIf(IsDBNull(RsTemp.Fields("VNO").Value), "", RsTemp.Fields("VNO").Value)
                pDebitNoteDate = IIf(pDebitNoteDate = "", "", pDebitNoteDate & ",") & IIf(IsDBNull(RsTemp.Fields("VDATE").Value), "", RsTemp.Fields("VDATE").Value)
                RsTemp.MoveNext()
            Loop
            CheckDebitNote = True
        Else
            pDebitNoteNo = ""
            pDebitNoteDate = ""
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        CheckDebitNote = False
    End Function
    Private Function CheckAmount(ByRef pPONO As Double) As Boolean
        On Error GoTo ErrPart1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTotalAmount As Double
        Dim mPurchaseAmount As Double
        Dim mIsProjectPO As Boolean
        Dim cntRow As Integer
        Dim mPOAmount As Double
        mIsProjectPO = False
        If Val(txtMRRNo.Text) <> 0 Then
            If MainClass.ValidateWithMasterTable(pPONO, "AUTO_KEY_PO", "PUR_TYPE", "PUR_PURCHASE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                If MasterNo = "R" Then
                    mIsProjectPO = True
                End If
            End If
        End If
        If mIsProjectPO = False Then CheckAmount = True : Exit Function
        mTotalAmount = 0
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColPONo
                If Val(.Text) = pPONO Then
                    .Col = ColAmount
                    mTotalAmount = mTotalAmount + Val(.Text)
                End If
            Next
        End With
        SqlStr = "SELECT SUM(ID.ITEM_AMT) AS AMOUNT" & vbCrLf & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.ISFINALPOST='Y'" & vbCrLf & " AND ID.CUST_REF_NO='" & pPONO & "'"
        '    If Val(txtMRRNo.Text) <> 0 Then
        '        SqlStr = SqlStr & vbCrLf & " AND IH.AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & ""
        '    End If
        If LblMKey.Text <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND IH.MKEY<>'" & LblMKey.Text & "'"
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            mPurchaseAmount = IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
        End If
        mPurchaseAmount = mPurchaseAmount + mTotalAmount
        SqlStr = "SELECT SUM(ID.GROSS_AMT) AS AMOUNT" & vbCrLf & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.AUTO_KEY_PO=" & pPONO & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            mPOAmount = IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
        End If
        If mPOAmount < mPurchaseAmount Then
            MsgInformation("Purchase Amount (Rs." & mPurchaseAmount & ") Cann't be exceed Than PO Amount (Rs." & mPOAmount & ").")
            '        MainClass.SetFocusToCell SprdMain, Row, ColRate
            CheckAmount = False
        Else
            CheckAmount = True
        End If
        Exit Function
ErrPart1:
        CheckAmount = False
    End Function
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Public Sub FrmPurchaseGST_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        FormActive = True

        SqlStr = ""
        SqlStr = "Select * from FIN_PURCHASE_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPurchMain, ADODB.LockTypeEnum.adLockReadOnly)
        SqlStr = ""
        SqlStr = "Select * from FIN_PURCHASE_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPurchDetail, ADODB.LockTypeEnum.adLockReadOnly)
        SqlStr = ""
        SqlStr = "Select * from FIN_PURCHASE_EXP Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPurchExp, ADODB.LockTypeEnum.adLockReadOnly)
        Call AssignGrid(False)
        Call SetTextLengths()
        mSupplierCode = CStr(-1)
        If Val(LblBookCode.Text) = ConModvatBookCode Then
            'cboInvType.Enabled = False
            'cboInvType.Visible = False
            'lblInvType.Visible = False
            txtTotCGSTRefund.Enabled = True
            txtTotSGSTRefund.Enabled = True
            txtTotIGSTRefund.Enabled = True
            chkRejection.Enabled = True
        Else
            'cboInvType.Enabled = False
            'cboInvType.Visible = True
            'lblInvType.Visible = True
            txtTotCGSTRefund.Enabled = False
            txtTotSGSTRefund.Enabled = False
            txtTotIGSTRefund.Enabled = False
            chkRejection.Enabled = False
        End If
        If RsCompany.Fields("PURCHASE_POSTINGTYPE").Value = "I" Then
            cboInvType.Enabled = False
            '        cboInvType.Visible = True
        End If
        If myMenu = UCase("mnuPurchaseSRInv") Or myMenu = UCase("mnuPurchaseSR") Then
            chkRejection.CheckState = System.Windows.Forms.CheckState.Checked
        End If
        cboGSTStatus.Items.Clear()
        cboGSTStatus.Items.Add("GST Input")
        cboGSTStatus.Items.Add("Reverse Charge")
        cboGSTStatus.Items.Add("Exempt")
        cboGSTStatus.Items.Add("Non-GST")
        cboGSTStatus.Items.Add("Ineligible")
        cboGSTStatus.Items.Add("Composit")
        cboGSTStatus.SelectedIndex = -1
        FillCboSaleType()
        'JB = New JsonBag
        'JB.Whitespace = System.Windows.Forms.CheckState.Checked
        If cmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ''Resume
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)
        On Error GoTo AssignGridErr
        Dim SqlStr As String = ""
        SqlStr = ""

        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim sql As String
        Dim i As Integer
        Dim inti As Integer

        oledbCnn = New OleDbConnection(StrConn)

        'MainClass.ClearGrid(SprdView)

        SqlStr = "SELECT FIN_INVTYPE_MST.NAME AS INVOICE_TYPE," & vbCrLf & " VNOPREFIX, TO_CHAR(VNOSEQ),VNOSUFFIX, " & vbCrLf & " VNO,VDATE, "

        If CDbl(LblBookCode.Text) = ConPurchaseBookCode Then
            SqlStr = SqlStr & vbCrLf & " VNO AS VNO,VDATE, "
        ElseIf CDbl(LblBookCode.Text) = ConModvatBookCode Then
            SqlStr = SqlStr & vbCrLf & " TO_CHAR(DECODE(GST_CLAIM_NO,-1,'',GST_CLAIM_NO),'00000') AS GST_CLAIM_NO,GST_CLAIM_DATE, "
        End If

        If lblPurchaseSeqType.Text = "2" Then
            SqlStr = SqlStr & vbCrLf & " REJ_CREDITNOTE AS OUR_CREDIT_NOTE_NO, "
        Else
            SqlStr = SqlStr & vbCrLf & " '' AS OUR_CREDIT_NOTE_NO, "
        End If

        SqlStr = SqlStr & vbCrLf & " BILLNO, INVOICE_DATE  AS BILLDATE, " & vbCrLf & " AUTO_KEY_MRR AS MRRNO, MRRDATE, " & vbCrLf & " A.SUPP_CUST_NAME AS SUPPLIER, B.SUPP_CUST_NAME AS CREDIT_ACCOUNT, " & vbCrLf & " ITEMDESC, TARIFFHEADING AS TARIFF,ITEMVALUE,"
        SqlStr = SqlStr & vbCrLf _
            & "TOTCGST_REFUNDAMT AS CGSTAMT,TOTSGST_REFUNDAMT AS SGSTAMT,TOTIGST_REFUNDAMT AS IGSTAMT, NETVALUE,DECODE(ISCAPITAL,'Y','YES','NO') AS ISCAPITAL,DECODE(REJECTION,'Y','YES','NO') AS AGTD3,DECODE(ISFINALPOST,'Y','YES','NO') AS FINALPOST "

        SqlStr = SqlStr & vbCrLf & " FROM " & vbCrLf _
            & " FIN_PURCHASE_HDR, FIN_INVTYPE_MST, FIN_SUPP_CUST_MST A, FIN_SUPP_CUST_MST B " & vbCrLf _
            & " WHERE FIN_PURCHASE_HDR.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " And FIN_PURCHASE_HDR.FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
            & " AND FIN_PURCHASE_HDR.COMPANY_CODE=FIN_INVTYPE_MST.COMPANY_CODE(+) " & vbCrLf _
            & " AND FIN_PURCHASE_HDR.TRNTYPE=FIN_INVTYPE_MST.CODE(+) " & vbCrLf _
            & " AND FIN_PURCHASE_HDR.COMPANY_CODE=A.COMPANY_CODE " & vbCrLf _
            & " AND FIN_PURCHASE_HDR.SUPP_CUST_CODE=A.SUPP_CUST_CODE " & vbCrLf _
            & " AND FIN_PURCHASE_HDR.COMPANY_CODE=B.COMPANY_CODE(+) " & vbCrLf _
            & " AND FIN_PURCHASE_HDR.ACCOUNTCODE=B.SUPP_CUST_CODE(+) AND FIN_PURCHASE_HDR.PURCHASE_TYPE= '" & lblPurchaseType.Text & "'"

        If Val(lblPurchaseSeqType.Text) = 2 Then
        Else
            SqlStr = SqlStr & vbCrLf & " AND SHIPPED_TO_SAMEPARTY='Y'"
        End If

        If chkRejection.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " AND REJECTION='Y'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND REJECTION='N'"
        End If
        If RsCompany.Fields("FYEAR").Value >= 2018 Then
            SqlStr = SqlStr & vbCrLf & " AND PURCHASESEQTYPE= '" & lblPurchaseSeqType.Text & "'"
        End If
        If CDbl(LblBookCode.Text) = ConModvatBookCode Then
            SqlStr = SqlStr & vbCrLf & " AND ISGSTAPPLICABLE='Y' AND AUTO_KEY_MRR<>-1"
            SqlStr = SqlStr & vbCrLf & " Order by GST_CLAIM_DATE,GST_CLAIM_NO"
        ElseIf CDbl(LblBookCode.Text) = ConPurchaseBookCode Then
            'SqlStr = SqlStr & vbCrLf & " AND ISFINALPOST='Y'" ''AND TRNTYPE<>-1     '' AND AUTO_KEY_MRR<>-1
            SqlStr = SqlStr & vbCrLf & " AND VDATE >= TO_DATE('" & VB6.Format(PubGSTApplicableDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            SqlStr = SqlStr & vbCrLf & " Order by FIN_PURCHASE_HDR.VDATE, FIN_PURCHASE_HDR.VNO"
        End If

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

        'MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
AssignGridErr:
        MsgBox(Err.Description, MsgBoxStyle.Information)
        'Resume
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
            UltraGrid1.DisplayLayout.Bands(0).Columns(0).Header.Caption = "Invoice Type"
            UltraGrid1.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Bill No Prefix"
            UltraGrid1.DisplayLayout.Bands(0).Columns(2).Header.Caption = "Bill Seq No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(3).Header.Caption = "Bill No Suffix"
            UltraGrid1.DisplayLayout.Bands(0).Columns(4).Header.Caption = "VNo"
            UltraGrid1.DisplayLayout.Bands(0).Columns(5).Header.Caption = "VDate"
            UltraGrid1.DisplayLayout.Bands(0).Columns(6).Header.Caption = "VNo"
            UltraGrid1.DisplayLayout.Bands(0).Columns(7).Header.Caption = "VDate"


            UltraGrid1.DisplayLayout.Bands(0).Columns(8).Header.Caption = "Our Credit Note No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(9).Header.Caption = "Bill No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(10).Header.Caption = "Bill Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(11).Header.Caption = "MRR No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(12).Header.Caption = "MRR Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(13).Header.Caption = "Supplier Name"
            UltraGrid1.DisplayLayout.Bands(0).Columns(14).Header.Caption = "Account Name"
            UltraGrid1.DisplayLayout.Bands(0).Columns(15).Header.Caption = "Item Desc"

            UltraGrid1.DisplayLayout.Bands(0).Columns(16).Header.Caption = "Tariff Heading"
            UltraGrid1.DisplayLayout.Bands(0).Columns(17).Header.Caption = "Item Value"
            UltraGrid1.DisplayLayout.Bands(0).Columns(18).Header.Caption = "CGST Refund Amount"
            UltraGrid1.DisplayLayout.Bands(0).Columns(19).Header.Caption = "SGST Refund Amount"
            UltraGrid1.DisplayLayout.Bands(0).Columns(20).Header.Caption = "IGST Refund Amount"
            UltraGrid1.DisplayLayout.Bands(0).Columns(21).Header.Caption = "Net Value"
            UltraGrid1.DisplayLayout.Bands(0).Columns(22).Header.Caption = "IS Capital"
            UltraGrid1.DisplayLayout.Bands(0).Columns(23).Header.Caption = "AGT D3"


            ''enable/disable the columns
            For inti = 0 To UltraGrid1.DisplayLayout.Bands(0).Columns.Count - 1
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).CellActivation = Activation.NoEdit  ''  .AllowEdit
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).Header.Appearance.TextHAlign = HAlign.Center

                ''UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.SubDepartmentName).Style = UltraWinGrid.ColumnStyle.DropDown
                '' UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.Qty).Style = UltraWinGrid.ColumnStyle.DoubleNonNegative
                ''UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.SubDepartmentName).EditorComponent = cmbDepartment
            Next

            For inti = 17 To 21
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).Style = UltraWinGrid.ColumnStyle.Double
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).CellAppearance.TextHAlign = HAlign.Right
            Next

            ''for hidden
            UltraGrid1.DisplayLayout.Bands(0).Columns(1).Hidden = True
            UltraGrid1.DisplayLayout.Bands(0).Columns(2).Hidden = True
            UltraGrid1.DisplayLayout.Bands(0).Columns(3).Hidden = True
            'UltraGrid1.DisplayLayout.Bands(0).Columns(15).Hidden = True

            ' to define width of the columns
            UltraGrid1.DisplayLayout.Bands(0).Columns(0).Width = 250
            UltraGrid1.DisplayLayout.Bands(0).Columns(1).Width = 60
            UltraGrid1.DisplayLayout.Bands(0).Columns(2).Width = 60
            UltraGrid1.DisplayLayout.Bands(0).Columns(3).Width = 60
            UltraGrid1.DisplayLayout.Bands(0).Columns(4).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(5).Width = 75
            UltraGrid1.DisplayLayout.Bands(0).Columns(6).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(7).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(8).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(9).Width = 80

            UltraGrid1.DisplayLayout.Bands(0).Columns(10).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(11).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(12).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(13).Width = 250
            UltraGrid1.DisplayLayout.Bands(0).Columns(14).Width = 250
            UltraGrid1.DisplayLayout.Bands(0).Columns(15).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(16).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(17).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(18).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(19).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(20).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(21).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(22).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(23).Width = 90


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
    Private Sub FormatSprdView()
        Dim cntCol As Integer
        'With SprdView
        '    .Row = -1
        '    .set_RowHeight(0, 600)
        '    .set_ColWidth(0, 600)
        '    .set_ColWidth(1, 0)
        '    .set_ColWidth(2, 0)
        '    .set_ColWidth(3, 0)
        '    .set_ColWidth(4, 0)
        '    .set_ColWidth(5, 0)
        '    .set_ColWidth(6, 0)
        '    .set_ColWidth(7, 1200)
        '    .set_ColWidth(8, 1300)
        '    .set_ColWidth(9, 1200)
        '    .set_ColWidth(10, 1300)
        '    .set_ColWidth(11, 1200)
        '    .set_ColWidth(12, 1200)
        '    .set_ColWidth(13, 2000)
        '    .set_ColWidth(14, 2000)
        '    .set_ColWidth(15, 1200)
        '    .set_ColWidth(16, 1200)
        '    .set_ColWidth(17, 1200)
        '    .set_ColWidth(18, 1200)
        '    .set_ColWidth(19, 1200)
        '    .set_ColWidth(20, 1200)
        '    .set_ColWidth(21, 800)
        '    .set_ColWidth(22, 800)
        '    For cntCol = 17 To 20
        '        .Col = cntCol
        '        .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
        '    Next
        '    .ColsFrozen = 8
        '    MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
        '    MainClass.SetSpreadColor(SprdView, -1)
        '    SprdView.set_RowHeight(-1, 300)
        '    .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
        '    MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        'End With
    End Sub
    Private Sub FormatSprdExp(ByRef Arow As Integer)
        On Error GoTo ERR1
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
            .TypeFloatDecimalPlaces = 3
            .TypeFloatDecimalChar = Asc(".")
            .TypeEditMultiLine = False

            .Col = ColExpAmt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatMin = -99999999999.999
            .TypeFloatMax = 99999999999.999
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
            .Col = ColExpDebitAmt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatMin = -99999999999.99
            .TypeFloatMax = 99999999999.99
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(ColExpDebitAmt, 8)
            .TypeEditMultiLine = False
            .Col = ColRO
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColRO, 2)
            '.Value = vbUnchecked
            MainClass.UnProtectCell(SprdExp, 1, .MaxRows, 1, ColExpDebitAmt)
            If ADDMode = True Then
                '            MainClass.UnProtectCell SprdExp, 1, .MaxRows, ColExpDebitAmt, ColExpDebitAmt
            Else
                MainClass.ProtectCell(SprdExp, 1, .MaxRows, ColExpDebitAmt, ColExpDebitAmt)
            End If
            MainClass.ProtectCell(SprdExp, 1, .MaxRows, ColExpName, ColExpName)
        End With
        MainClass.SetSpreadColor(SprdExp, Arow)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)
        On Error GoTo ERR1
        pShowCalc = False
        With SprdMain
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsPurchDetail.Fields("ITEM_CODE").DefinedSize ''
            .set_ColWidth(ColItemCode, 8)

            .Col = ColHSN
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = MainClass.SetMaxLength("HSN_CODE", "GEN_HSN_MST", PubDBCn) ''
            .set_ColWidth(ColHSN, 8)
            .Col = ColInvType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("NAME", "FIN_INVTYPE_MST", PubDBCn)
            .set_ColWidth(ColInvType, 25)
            .Col = ColPartNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsPurchDetail.Fields("CUSTOMER_PART_NO").DefinedSize
            .ColHidden = True
            .ColsFrozen = ColItemDesc
            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = RsPurchDetail.Fields("Item_Desc").DefinedSize ''
            .set_ColWidth(ColItemDesc, 15)
            .Col = ColAcceptedQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .ColHidden = True
            .Col = ColShortageQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColShortageQty, 7)
            .Col = ColRejectedQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColRejectedQty, 7)
            .Col = ColPORate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColPORate, 7)
            .Col = ColVolDiscRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColVolDiscRate, 6)
            .Col = ColUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = RsPurchDetail.Fields("ITEM_UOM").DefinedSize ''
            .set_ColWidth(ColUnit, 4)
            .Col = ColQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColQty, 8)
            .Col = ColRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.9999")
            .TypeFloatMin = CDbl("-999999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColRate, 8)
            .Col = ColAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .set_ColWidth(ColAmount, 8)
            .Col = ColTaxableAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .set_ColWidth(ColTaxableAmount, 8)
            .Col = ColCGSTPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMin = CDbl("-99.99")
            .set_ColWidth(ColCGSTPer, 5)
            .Col = ColCGSTAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .set_ColWidth(ColCGSTAmount, 8)
            .Col = ColSGSTPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMin = CDbl("-99.99")
            .set_ColWidth(ColSGSTPer, 5)
            .Col = ColSGSTAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .set_ColWidth(ColSGSTAmount, 8)
            .Col = ColIGSTPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99.99")
            .TypeFloatMin = CDbl("-99.99")
            .set_ColWidth(ColIGSTPer, 5)
            .Col = ColIGSTAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .set_ColWidth(ColIGSTAmount, 8)
            .Col = ColPONo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsPurchDetail.Fields("CUST_REF_NO").DefinedSize ''
            .set_ColWidth(ColPONo, 9)

            .Col = ColShowPO
            .CellType = SS_CELL_TYPE_BUTTON
            '.Lock = False
            .TypeButtonText = "Show"
            .TypeButtonAlign = SS_CELL_BUTTON_ALIGN_LEFT
            .set_ColWidth(ColShowPO, 5)
        End With
        '    If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "PORATEEDITABLE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
        MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, ColQty, ColQty)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColItemDesc)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColAcceptedQty, ColUnit)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColAmount, ColTaxableAmount)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColCGSTPer, ColIGSTAmount)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPONo, ColPONo)


        If MainClass.ValidateWithMasterTable(txtMRRNo.Text, "AUTO_KEY_MRR", "REF_TYPE", "INV_GATE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            If MasterNo = "R" Or MasterNo = "J" Then
                '                MainClass.ProtectCell SprdMain, 1, SprdMain.MaxRows, ColQty, ColQty
            Else
                MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColQty, ColQty)
            End If
        End If
        '    End If
        pShowCalc = True
        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then RsPurchDetail.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Private Sub FormatSprdPostingDetail(ByRef Arow As Integer)
        On Error GoTo ERR1
        With SprdPostingDetail
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)
            .Col = 1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(1, 30)
            .Col = 2
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(2, 12)
            .Col = 3
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditMultiLine = True
            .set_ColWidth(3, 5)
        End With
        MainClass.ProtectCell(SprdPostingDetail, 1, SprdPostingDetail.MaxRows, 1, 3)
        MainClass.SetSpreadColor(SprdPostingDetail, Arow)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        With RsPurchMain
            txtVNo.MaxLength = .Fields("Vno").DefinedSize ''
            txtVNoPrefix.MaxLength = .Fields("VNoPrefix").DefinedSize ''
            txtVNoSuffix.MaxLength = .Fields("VNoSuffix").DefinedSize ''
            txtVDate.MaxLength = 10
            txtModvatNo.MaxLength = .Fields("GST_CLAIM_NEW_NO").DefinedSize
            txtModvatDate.MaxLength = 10

            txtCReditNoteNo.MaxLength = .Fields("REJ_CREDITNOTE").DefinedSize
            txtCustomerRefNo.MaxLength = .Fields("CUSTOMER_REF_NO").DefinedSize

            txtBENo.MaxLength = .Fields("BE_NO").DefinedSize ''
            txtBEDate.MaxLength = 10
            txtBEAmount.MaxLength = .Fields("BE_AMOUNT").Precision
            txtTotCGSTRefund.MaxLength = .Fields("TOTCGST_REFUNDAMT").Precision
            txtTotSGSTRefund.MaxLength = .Fields("TOTSGST_REFUNDAMT").Precision
            txtTotIGSTRefund.MaxLength = .Fields("TOTIGST_REFUNDAMT").Precision
            txtMRRNo.MaxLength = .Fields("AUTO_KEY_MRR").Precision ''
            txtMRRDate.MaxLength = 10
            txtBillNo.MaxLength = .Fields("BillNo").Precision ''
            txtBillDate.MaxLength = 10
            txtSupplier.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            '        txtModvatSupp.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            txtDebitAccount.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            txtShippedTo.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            txtCreditDays(0).MaxLength = .Fields("DUEDAYSFROM").Precision ''
            txtCreditDays(1).MaxLength = .Fields("DUEDAYSTO").Precision ''
            txtTariff.MaxLength = .Fields("TARIFFHEADING").DefinedSize ''
            txtItemType.MaxLength = .Fields("ItemDesc").DefinedSize ''
            txtRemarks.MaxLength = .Fields("Remarks").DefinedSize ''
            txtPortCode.MaxLength = .Fields("PORT_CODE").DefinedSize
            txtNarration.MaxLength = .Fields("NARRATION").DefinedSize ''
            txtCarriers.MaxLength = .Fields("CARRIERS").DefinedSize ''
            txtVehicle.MaxLength = .Fields("VehicleNo").DefinedSize ''
            txtDocsThru.MaxLength = .Fields("DocsThrough").DefinedSize ''
            txtMode.MaxLength = .Fields("DespatchMode").DefinedSize ''
            txtTDSRate.MaxLength = .Fields("TDSPer").Precision ''
            txtTDSAmount.MaxLength = .Fields("TDSAMOUNT").Precision ''
            txtESIRate.MaxLength = .Fields("ESIPER").Precision ''
            txtESIAmount.MaxLength = .Fields("ESIAMOUNT").Precision ''
            txtSTDSRate.MaxLength = .Fields("STDSPER").Precision ''
            txtSTDSAmount.MaxLength = .Fields("STDSAMOUNT").Precision ''
            txtJVVNO.MaxLength = .Fields("JVNO").DefinedSize ''
            'lblJVTMKey.MaxLength = .Fields("JVT_MKEY").DefinedSize ''
            txtServProvided.MaxLength = .Fields("SERV_PROV").DefinedSize ''
            txtServiceOn.MaxLength = .Fields("SERVICE_ON_AMT").Precision
            txtProviderPer.MaxLength = .Fields("SERV_PROVIDER_PER").Precision
            txtRecipientPer.MaxLength = .Fields("SERV_RECIPIENT_PER").Precision
            txtServiceTaxPer.MaxLength = .Fields("SERVICE_TAX_PER").Precision
            txtServiceTaxAmount.MaxLength = .Fields("SERVICE_TAX_AMOUNT").Precision
            txtAdvVNo.MaxLength = .Fields("ADV_VNO").DefinedSize
            txtAdvDate.MaxLength = .Fields("ADV_VDATE").DefinedSize
            txtItemAdvAdjust.MaxLength = .Fields("ADV_ITEM_AMT").Precision
            txtAdvAdjust.MaxLength = .Fields("ADV_ADJUSTED_AMT").Precision
            txtAdvCGST.MaxLength = .Fields("ADV_CGST_AMT").Precision
            txtAdvSGST.MaxLength = .Fields("ADV_SGST_AMT").Precision
            txtAdvIGST.MaxLength = .Fields("ADV_IGST_AMT").Precision
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Show1()
        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim mCustRefNo As String
        Dim mDivisionCode As Double
        Dim mDivisionDesc As String
        Dim mSACCode As String
        Dim mGSTStatus As String
        Dim mVNo As String
        Dim mBalCGST As Double
        Dim mBalSGST As Double
        Dim mBalIGST As Double
        Dim pSectionCode As Long
        Dim mSupplierCode As String
        Dim mDeliveryToCode As String = ""
        Dim mDeliveryToName As String = ""

        Clear1()
        With RsPurchMain
            If Not .EOF Then
                ADDMode = False
                MODIFYMode = False
                LblMKey.Text = .Fields("MKey").Value
                lblPMKey.Text = ""
                txtVNoPrefix.Text = IIf(IsDBNull(.Fields("VNOPREFIX").Value), "", .Fields("VNOPREFIX").Value)
                txtVNo.Text = VB6.Format(IIf(IsDBNull(.Fields("VNOSEQ").Value), "", .Fields("VNOSEQ").Value), "00000")
                txtVNoSuffix.Text = IIf(IsDBNull(.Fields("VNOSUFFIX").Value), "", .Fields("VNOSUFFIX").Value)
                txtVDate.Text = VB6.Format(IIf(IsDBNull(.Fields("VDate").Value), "", .Fields("VDate").Value), "DD/MM/YYYY")
                lblGSTClaimNo.Text = IIf(IsDBNull(.Fields("GST_CLAIM_NO").Value), "", .Fields("GST_CLAIM_NO").Value)
                lblGSTClaimDate.Text = VB6.Format(IIf(IsDBNull(.Fields("GST_CLAIM_DATE").Value), "", .Fields("GST_CLAIM_DATE").Value), "DD/MM/YYYY")
                txtModvatNo.Text = IIf(IsDBNull(.Fields("GST_CLAIM_NEW_NO").Value), "", .Fields("GST_CLAIM_NEW_NO").Value)
                txtModvatDate.Text = VB6.Format(IIf(IsDBNull(.Fields("GST_CLAIM_NEW_DATE").Value), "", .Fields("GST_CLAIM_NEW_DATE").Value), "DD/MM/YYYY")

                txtCReditNoteNo.Text = IIf(IsDBNull(.Fields("REJ_CREDITNOTE").Value), "", .Fields("REJ_CREDITNOTE").Value)
                txtCustomerRefNo.Text = IIf(IsDBNull(.Fields("CUSTOMER_REF_NO").Value), "", .Fields("CUSTOMER_REF_NO").Value)


                chkGSTClaim.CheckState = IIf(.Fields("GST_CLAIM").Value = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
                lblClaimStatus.Text = IIf(IsDBNull(.Fields("GST_CLAIM").Value), "N", .Fields("GST_CLAIM").Value)
                '            chkCreditRC.Value = IIf(.Fields("GST_RC_CLAIM").Value = "Y", vbChecked, vbUnchecked)
                '
                '            If chkCreditRC.Value = vbChecked Then
                '                txtModvatNo.Text = Format(IIf(IsNull(.Fields("GST_CLAIM_RC_NO").Value), "", .Fields("GST_CLAIM_RC_NO").Value), "00000")
                '                txtModvatDate.Text = Format(IIf(IsNull(.Fields("GST_CLAIM_RC_DATE").Value), "", .Fields("GST_CLAIM_RC_DATE").Value), "DD/MM/YYYY")
                '            Else
                '                txtModvatNo.Text = Format(IIf(IsNull(.Fields("GST_CLAIM_NO").Value), "", .Fields("GST_CLAIM_NO").Value), "00000")
                '                txtModvatDate.Text = Format(IIf(IsNull(.Fields("GST_CLAIM_DATE").Value), "", .Fields("GST_CLAIM_DATE").Value), "DD/MM/YYYY")
                '            End If
                '
                mVNo = Trim(Trim(txtVNoPrefix.Text) & VB6.Format(Val(txtVNo.Text), "00000") & Trim(txtVNoSuffix.Text))
                '            lblSaleBillNoSeq.text = Format(IIf(IsNull(.Fields("SALEBILLNOSEQ").Value), "", .Fields("SALEBILLNOSEQ").Value), "00000000")
                '            lblSaleBillNo.text = IIf(IsNull(.Fields("SALEBILL_NO").Value), "", .Fields("SALEBILL_NO").Value)
                '            lblSaleBillDate.text = Format(IIf(IsNull(.Fields("SALEBILLDATE").Value), "", .Fields("SALEBILLDATE").Value), "DD/MM/YYYY")
                lblPurchaseVNo.Text = VB6.Format(IIf(IsDBNull(.Fields("VNOSEQ").Value), "", .Fields("VNOSEQ").Value), "00000")
                lblPurchaseSeqType.Text = IIf(IsDBNull(.Fields("PURCHASESEQTYPE").Value), 0, .Fields("PURCHASESEQTYPE").Value)

                mGSTStatus = IIf(IsDBNull(.Fields("ISGSTAPPLICABLE").Value), "E", .Fields("ISGSTAPPLICABLE").Value) ''IIf(.Fields("ISGSTAPPLICABLE").Value = "Y", vbChecked, vbUnchecked)
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
                Else
                    cboGSTStatus.SelectedIndex = 5
                End If
                cboGSTStatus.Enabled = IIf(chkGSTClaim.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
                txtTotCGSTRefund.Text = VB6.Format(IIf(IsDBNull(.Fields("TOTCGST_REFUNDAMT").Value), "", .Fields("TOTCGST_REFUNDAMT").Value), "0.00")
                txtTotSGSTRefund.Text = VB6.Format(IIf(IsDBNull(.Fields("TOTSGST_REFUNDAMT").Value), "", .Fields("TOTSGST_REFUNDAMT").Value), "0.00")
                txtTotIGSTRefund.Text = VB6.Format(IIf(IsDBNull(.Fields("TOTIGST_REFUNDAMT").Value), "", .Fields("TOTIGST_REFUNDAMT").Value), "0.00")
                txtServNo.Text = VB6.Format(IIf(IsDBNull(.Fields("SERVNo").Value), "", .Fields("SERVNo").Value), "00000")
                txtServDate.Text = VB6.Format(IIf(IsDBNull(.Fields("SERVDate").Value), "", .Fields("SERVDate").Value), "DD/MM/YYYY")
                txtMRRNo.Text = IIf(IsDBNull(.Fields("AUTO_KEY_MRR").Value), "", .Fields("AUTO_KEY_MRR").Value)
                txtMRRDate.Text = IIf(IsDBNull(.Fields("MRRDATE").Value), "", .Fields("MRRDATE").Value)
                txtPONo.Text = IIf(IsDBNull(.Fields("CUSTREFNO").Value), "", .Fields("CUSTREFNO").Value)
                txtPODate.Text = IIf(IsDBNull(.Fields("CUSTREFDATE").Value), "", .Fields("CUSTREFDATE").Value)
                'If RsCompany.Fields("PURCHASE_POSTINGTYPE").Value = "B" Then
                If MainClass.ValidateWithMasterTable((.Fields("TRNTYPE").Value), "CODE", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    cboInvType.Text = MasterNo
                End If
                mBookSubType = IIf(IsDBNull(.Fields("BOOKSUBTYPE").Value), "", .Fields("BOOKSUBTYPE").Value)
                '                If chkRejection.Value = vbChecked Then
                '                    mBookSubType = "R"
                '                Else
                '                    If MainClass.ValidateWithMasterTable(cboInvType.Text, "NAME", "IDENTIFICATION", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") Then
                '                        mBookSubType = MasterNo
                '                    Else
                '                        mBookSubType = -1
                '                    End If
                '                End If
                'Else
                '    mBookSubType = IIf(IsDBNull(.Fields("BOOKSUBTYPE").Value), "", .Fields("BOOKSUBTYPE").Value)
                'End If
                txtBillNo.Text = IIf(IsDBNull(.Fields("BILLNO").Value), "", .Fields("BILLNO").Value)
                txtBillDate.Text = VB6.Format(IIf(IsDBNull(.Fields("INVOICE_DATE").Value), "", .Fields("INVOICE_DATE").Value), "DD/MM/YYYY")
                mSupplierCode = IIf(IsDBNull(.Fields("SUPP_CUST_CODE").Value), "", .Fields("SUPP_CUST_CODE").Value)
                If MainClass.ValidateWithMasterTable((.Fields("SUPP_CUST_CODE").Value), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtSupplier.Text = MasterNo
                End If
                chkShipTo.CheckState = IIf(.Fields("SHIPPED_TO_SAMEPARTY").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                If chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked Then
                    txtShippedTo.Text = txtSupplier.Text
                Else
                    If MainClass.ValidateWithMasterTable((.Fields("SHIPPED_TO_PARTY_CODE").Value), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        txtShippedTo.Text = MasterNo
                    End If
                End If
                '            If MainClass.ValidateWithMasterTable(.Fields("MODVAT_SUPP_CODE").Value, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '                txtModvatSupp.Text = MasterNo
                '            End If
                If RsCompany.Fields("PURCHASE_POSTINGTYPE").Value = "B" Then
                    If MainClass.ValidateWithMasterTable((.Fields("ACCOUNTCODE").Value), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        txtDebitAccount.Text = MasterNo
                    End If
                Else
                    txtDebitAccount.Text = ""
                End If
                txtCreditDays(0).Text = IIf(IsDBNull(.Fields("DUEDAYSFROM").Value), "", .Fields("DUEDAYSFROM").Value)
                txtCreditDays(1).Text = IIf(IsDBNull(.Fields("DUEDAYSTO").Value), "", .Fields("DUEDAYSTO").Value)
                chkCancelled.CheckState = IIf(.Fields("CANCELLED").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                '            If LblBookCode.text = ConPurchaseBookCode Then
                '                chkCancelled.Enabled = IIf(.Fields("CANCELLED").Value = "Y", False, True)
                '            Else
                '                chkCancelled.Enabled = False
                '            End If
                chkRejection.CheckState = IIf(.Fields("REJECTION").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                ChkCapital.CheckState = IIf(.Fields("ISCAPITAL").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                txtIRNNo.Text = IIf(IsDBNull(.Fields("IRN_NO").Value), "", .Fields("IRN_NO").Value)
                txteInvAckNo.Text = IIf(IsDBNull(.Fields("IRN_ACK_NO").Value), "", .Fields("IRN_ACK_NO").Value)
                txteInvAckDate.Text = VB6.Format(IIf(IsDBNull(.Fields("IRN_ACK_DATE").Value), "", .Fields("IRN_ACK_DATE").Value), "DD/MM/YYYY HH:MM")
                If Trim(txtIRNNo.Text) = "" Then
                    cmdeInvoice.Enabled = IIf(PubUserID = "EINV", True, IIf(RsCompany.Fields("E_INVOICE_APP").Value = "Y", True, False))
                Else
                    cmdeInvoice.Enabled = False
                End If
                lblTotQty.Text = VB6.Format(IIf(IsDBNull(.Fields("TOTQTY").Value), 0, .Fields("TOTQTY").Value), "0.00")
                lblTotItemValue.Text = VB6.Format(IIf(IsDBNull(.Fields("ITEMVALUE").Value), 0, .Fields("ITEMVALUE").Value), "0.00")
                lblTotCGSTAmount.Text = VB6.Format(IIf(IsDBNull(.Fields("TOTCGST_AMOUNT").Value), "", .Fields("TOTCGST_AMOUNT").Value), "0.00")
                lblTotSGSTAmount.Text = VB6.Format(IIf(IsDBNull(.Fields("TOTSGST_AMOUNT").Value), "", .Fields("TOTSGST_AMOUNT").Value), "0.00")
                lblTotIGSTAmount.Text = VB6.Format(IIf(IsDBNull(.Fields("TOTIGST_AMOUNT").Value), "", .Fields("TOTIGST_AMOUNT").Value), "0.00")
                lblNetAmount.Text = VB6.Format(IIf(IsDBNull(.Fields("NETVALUE").Value), 0, .Fields("NETVALUE").Value), "0.00")
                txtTariff.Text = IIf(IsDBNull(.Fields("TARIFFHEADING").Value), "", .Fields("TARIFFHEADING").Value)
                txtItemType.Text = IIf(IsDBNull(.Fields("ITEMDESC").Value), "", .Fields("ITEMDESC").Value)
                txtRemarks.Text = IIf(IsDBNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)
                txtBENo.Text = IIf(IsDBNull(.Fields("BE_NO").Value), "", .Fields("BE_NO").Value)
                txtBEDate.Text = VB6.Format(IIf(IsDBNull(.Fields("BE_DATE").Value), "", .Fields("BE_DATE").Value), "DD/MM/YYYY")
                txtBEAmount.Text = VB6.Format(IIf(IsDBNull(.Fields("BE_AMOUNT").Value), 0, .Fields("BE_AMOUNT").Value), "0.00")
                txtPortCode.Text = IIf(IsDBNull(.Fields("PORT_CODE").Value), "", .Fields("PORT_CODE").Value)
                txtNarration.Text = IIf(IsDBNull(.Fields("NARRATION").Value), "", .Fields("NARRATION").Value)
                txtCarriers.Text = IIf(IsDBNull(.Fields("CARRIERS").Value), "", .Fields("CARRIERS").Value)
                txtVehicle.Text = IIf(IsDBNull(.Fields("VEHICLENO").Value), "", .Fields("VEHICLENO").Value)
                txtDocsThru.Text = IIf(IsDBNull(.Fields("DOCSTHROUGH").Value), "", .Fields("DOCSTHROUGH").Value)
                txtMode.Text = IIf(IsDBNull(.Fields("DESPATCHMODE").Value), "", .Fields("DESPATCHMODE").Value)
                txtPaymentdate.Text = IIf(IsDBNull(.Fields("PAYMENTDATE").Value), "", .Fields("PAYMENTDATE").Value)
                chkTDS.CheckState = IIf(.Fields("ISTDSDEDUCT").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkTDS.Enabled = IIf(.Fields("ISTDSDEDUCT").Value = "Y", False, True)
                txtTDSRate.Text = VB6.Format(IIf(IsDBNull(.Fields("TDSPer").Value), "", .Fields("TDSPer").Value), "0.000")
                txtTDSAmount.Text = VB6.Format(IIf(IsDBNull(.Fields("TDSAMOUNT").Value), "", .Fields("TDSAMOUNT").Value), "0.00")
                chkESI.CheckState = IIf(.Fields("ISESIDEDUCT").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkESI.Enabled = IIf(.Fields("ISESIDEDUCT").Value = "Y", False, True)
                txtESIRate.Text = VB6.Format(IIf(IsDBNull(.Fields("ESIPer").Value), "", .Fields("ESIPer").Value), "0.000")
                txtESIAmount.Text = VB6.Format(IIf(IsDBNull(.Fields("ESIAMOUNT").Value), "", .Fields("ESIAMOUNT").Value), "0.00")
                ChkSTDS.CheckState = IIf(.Fields("ISSTDSDEDUCT").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                ChkSTDS.Enabled = IIf(.Fields("ISSTDSDEDUCT").Value = "Y", False, True)
                txtSTDSRate.Text = VB6.Format(IIf(IsDBNull(.Fields("STDSPer").Value), "", .Fields("STDSPer").Value), "0.000")
                txtSTDSAmount.Text = VB6.Format(IIf(IsDBNull(.Fields("STDSAMOUNT").Value), "", .Fields("STDSAMOUNT").Value), "0.00")
                txtTDSDeductOn.Text = VB6.Format(IIf(IsDBNull(.Fields("TDS_DEDUCT_ON").Value), "", .Fields("TDS_DEDUCT_ON").Value), "0.00")
                txtSTDSDeductOn.Text = VB6.Format(IIf(IsDBNull(.Fields("STDS_DEDUCT_ON").Value), "", .Fields("STDS_DEDUCT_ON").Value), "0.00")
                txtESIDeductOn.Text = VB6.Format(IIf(IsDBNull(.Fields("ESI_DEDUCT_ON").Value), "", .Fields("ESI_DEDUCT_ON").Value), "0.00")
                txtJVVNO.Text = IIf(IsDBNull(.Fields("JVNO").Value), "", .Fields("JVNO").Value)
                lblJVTMKey.Text = IIf(IsDBNull(.Fields("JVT_MKEY").Value), "", .Fields("JVT_MKEY").Value)

                If lblJVTMKey.Text <> "" Then
                    If MainClass.ValidateWithMasterTable((lblJVTMKey.Text), "MKEY", "VNO", "FIN_VOUCHER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        txtJVVNO.Text = Trim(MasterNo)
                    End If
                End If

                pSectionCode = IIf(IsDBNull(.Fields("SECTION_CODE").Value), -1, .Fields("SECTION_CODE").Value)

                If pSectionCode > 0 Then
                    If MainClass.ValidateWithMasterTable(pSectionCode, "CODE", "NAME", "TDS_SECTION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        txtSection.Text = MasterNo
                    End If
                End If


                OptFreight(0).Checked = True
                OptFreight(1).Checked = False
                chkFinalPost.CheckState = IIf(.Fields("ISFINALPOST").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkFOC.CheckState = IIf(.Fields("ISFOC").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkFOC.Enabled = IIf(.Fields("ISFOC").Value = "Y", True, False)
                If MainClass.ValidateWithMasterTable((txtMRRNo.Text), "AUTO_KEY_MRR", "REF_TYPE", "INV_GATE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mCustRefNo = MasterNo
                Else
                    mCustRefNo = "-1"
                End If
                '            txtBalAmount.Text = GetBillBalanceAmt(.Fields("SUPP_CUST_CODE").Value, txtBillNo.Text)
                mSACCode = IIf(IsDBNull(.Fields("SAC_CODE").Value), "", .Fields("SAC_CODE").Value)
                If MainClass.ValidateWithMasterTable(mSACCode, "HSN_CODE", "HSN_DESC", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
                    txtServProvided.Text = Trim(MasterNo)
                Else
                    txtServProvided.Text = ""
                End If
                txtServiceOn.Text = VB6.Format(IIf(IsDBNull(.Fields("SERVICE_ON_AMT").Value), 0, .Fields("SERVICE_ON_AMT").Value), "0.00")
                txtProviderPer.Text = VB6.Format(IIf(IsDBNull(.Fields("SERV_PROVIDER_PER").Value), 0, .Fields("SERV_PROVIDER_PER").Value), "0.00")
                txtRecipientPer.Text = VB6.Format(IIf(IsDBNull(.Fields("SERV_RECIPIENT_PER").Value), 0, .Fields("SERV_RECIPIENT_PER").Value), "0.00")
                txtServiceTaxPer.Text = VB6.Format(IIf(IsDBNull(.Fields("SERVICE_TAX_PER").Value), 0, .Fields("SERVICE_TAX_PER").Value), "0.00")
                txtServiceTaxAmount.Text = VB6.Format(IIf(IsDBNull(.Fields("SERVICE_TAX_AMOUNT").Value), 0, .Fields("SERVICE_TAX_AMOUNT").Value), "0.00")
                mDivisionCode = IIf(IsDBNull(.Fields("DIV_CODE").Value), -1, .Fields("DIV_CODE").Value)
                If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionDesc = Trim(MasterNo)
                    cboDivision.Text = mDivisionDesc
                End If
                cboDivision.Enabled = False
                txtAdvVNo.Text = IIf(IsDBNull(.Fields("ADV_VNO").Value), "", .Fields("ADV_VNO").Value)
                txtAdvDate.Text = IIf(IsDBNull(.Fields("ADV_VDATE").Value), "", .Fields("ADV_VDATE").Value)
                txtAdvBal.Text = CStr(GetBalancePaymentAmount((.Fields("SUPP_CUST_CODE").Value), txtBillDate.Text, mVNo, (txtVDate.Text), mDivisionCode, "AP", mBalCGST, mBalSGST, mBalIGST))
                txtAdvBal.Text = VB6.Format(Val(txtAdvBal.Text) + mBalCGST + mBalSGST + mBalIGST, "0.00")
                '            txtAdvCGSTBal.Text = Format(mBalCGST, "0.00")
                '            txtAdvSGSTBal.Text = Format(mBalSGST, "0.00")
                '            txtAdvIGSTBal.Text = Format(mBalIGST, "0.00")
                txtItemAdvAdjust.Text = VB6.Format(IIf(IsDBNull(.Fields("ADV_ITEM_AMT").Value), 0, .Fields("ADV_ITEM_AMT").Value), "0.00")
                txtAdvAdjust.Text = VB6.Format(IIf(IsDBNull(.Fields("ADV_ADJUSTED_AMT").Value), 0, .Fields("ADV_ADJUSTED_AMT").Value), "0.00")
                txtAdvCGST.Text = VB6.Format(IIf(IsDBNull(.Fields("ADV_CGST_AMT").Value), 0, .Fields("ADV_CGST_AMT").Value), "0.00")
                txtAdvSGST.Text = VB6.Format(IIf(IsDBNull(.Fields("ADV_SGST_AMT").Value), 0, .Fields("ADV_SGST_AMT").Value), "0.00")
                txtAdvIGST.Text = VB6.Format(IIf(IsDBNull(.Fields("ADV_IGST_AMT").Value), 0, .Fields("ADV_IGST_AMT").Value), "0.00")
                chkRejection.Enabled = False
                ChkCapital.Enabled = False

                txtBillTo.Text = IIf(IsDBNull(.Fields("BILL_TO_LOC_ID").Value), "", .Fields("BILL_TO_LOC_ID").Value)
                TxtShipTo.Text = IIf(IsDBNull(.Fields("SHIP_TO_LOC_ID").Value), "", .Fields("SHIP_TO_LOC_ID").Value)

                mDeliveryToCode = IIf(IsDBNull(.Fields("DELIVERY_TO").Value), "", .Fields("DELIVERY_TO").Value)
                mDeliveryToName = ""

                If mDeliveryToCode = "" Then
                    txtDeliveryTo.Text = ""

                    txtDeliveryToLoc.Text = ""
                Else
                    If MainClass.ValidateWithMasterTable(mDeliveryToCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mDeliveryToName = MasterNo
                    End If
                    ',
                    txtDeliveryTo.Text = mDeliveryToName

                    txtDeliveryToLoc.Text = IIf(IsDBNull(.Fields("DELIVERY_TO_LOC_ID").Value), "", .Fields("DELIVERY_TO_LOC_ID").Value)
                End If

                mAddUser = IIf(IsDBNull(.Fields("ADDUSER").Value), "", .Fields("ADDUSER").Value)
                mAddDate = VB6.Format(IIf(IsDBNull(.Fields("ADDDATE").Value), "", .Fields("ADDDATE").Value), "DD/MM/YYYY")
                mModUser = IIf(IsDBNull(.Fields("MODUSER").Value), "", .Fields("MODUSER").Value)
                mModDate = VB6.Format(IIf(IsDBNull(.Fields("MODDATE").Value), "", .Fields("MODDATE").Value), "DD/MM/YYYY")
                '            cmdResetMRR.Enabled = True
                Call ShowDetail1((LblMKey.Text), mCustRefNo)
                Call ShowPaymentDetail1((LblMKey.Text), mSupplierCode)
                Call ShowExp1((LblMKey.Text))
                Call SprdExp_LeaveCell(SprdExp, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColExpAmt, 1, 1, 1, True))
                ''Call CalcTots
            End If
        End With
        txtVNo.Enabled = True
        '    chkModvat.Enabled = False
        '    chkSTRefund.Enabled = False
        chkRejection.Enabled = False
        ChkCapital.Enabled = False
        If lblPurchaseSeqType.Text = "9" Then
            txtBillNo.Enabled = False
            txtBillDate.Enabled = False
        End If
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsPurchMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        FormatSprdMain(-1)
        If PubUserID <> "G0416" Then
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColRate, ColRate)
        End If
        SprdMain.Enabled = True
        SprdExp.Enabled = True
        txtMRRNo.Enabled = False
        CmdSearchMRR.Enabled = False
        txtMRRDate.Enabled = False
        'If RsCompany.Fields("PURCHASE_POSTINGTYPE").Value = "B" Then
        '    If Val(LblBookCode.Text) = ConModvatBookCode Or Val(LblBookCode.Text) = ConSTClaimBookCode Or Val(LblBookCode.Text) = ConCSTClaimBookCode Or Val(LblBookCode.Text) = ConServiceClaimBookCode Then
        '        cboInvType.Enabled = False
        '    Else
        '        cboInvType.Enabled = MainClass.GetUserCanModify(txtVDate.Text) ''IIf(PubUserLevel = 1 Or PubUserLevel = 2, True, False)
        '    End If
        'Else
        cboInvType.Enabled = False
        'End If
        'Call CalcTots()
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Private Function ShowFromExcise1(ByRef mRsDC As ADODB.Recordset) As Boolean
        On Error GoTo ERR1
        Dim mCustRefNo As String
        Dim mFormCode As String
        Dim mDivisionCode As Double
        Dim mDivisionDesc As String
        Dim mGSTStatus As String
        Dim mBalCGST As Double
        Dim mBalSGST As Double
        Dim mBalIGST As Double
        With mRsDC
            If Not .EOF Then
                '            txtVNoPrefix.Text = IIf(IsNull(.Fields("VNOPREFIX").Value), "", .Fields("VNOPREFIX").Value)
                '
                '            If .Fields("VNOSEQ").Value = -1 Then
                '
                '            Else
                '                txtVNo.Text = Format(IIf(IsNull(.Fields("VNOSEQ").Value), "", .Fields("VNOSEQ").Value), "00000")
                '            End If
                '            txtVNoSuffix.Text = IIf(IsNull(.Fields("VNOSUFFIX").Value), "", .Fields("VNOSUFFIX").Value)
                '            txtVDate.Text = Format(IIf(IsNull(.Fields("VDate").Value), "", .Fields("VDate").Value), "DD/MM/YYYY")
                txtMRRNo.Text = IIf(IsDBNull(.Fields("AUTO_KEY_MRR").Value), "", .Fields("AUTO_KEY_MRR").Value)
                txtMRRDate.Text = IIf(IsDBNull(.Fields("MRRDATE").Value), "", .Fields("MRRDATE").Value)
                If .Fields("ISFINALPOST").Value = "Y" Then
                    MsgInformation("Account Entry (P" & VB6.Format(IIf(IsDBNull(.Fields("VNOSEQ").Value) Or .Fields("VNOSEQ").Value = "-1", "", .Fields("VNOSEQ").Value), "00000") & ") Already made Against This MRR")
                    ShowFromExcise1 = False
                    Exit Function
                End If
                If MainClass.ValidateWithMasterTable((txtMRRNo.Text), "AUTO_KEY_MRR", "MRR_FINAL_FLAG", "INV_GATE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    If MasterNo = "Y" Then
                        MsgInformation("Please Check This MRR Made FOC")
                        ShowFromExcise1 = False
                        Exit Function
                    End If
                End If
                If CDbl(LblBookCode.Text) = ConModvatBookCode Then
                    If .Fields("ISGSTAPPLICABLE").Value = "Y" Then
                        MsgInformation("GST Entry (" & VB6.Format(IIf(IsDBNull(.Fields("GST_CLAIM_NO").Value) Or .Fields("GST_CLAIM_NO").Value = "-1", "", .Fields("GST_CLAIM_NO").Value), "00000") & ") Already made Against This MRR")
                        ShowFromExcise1 = False
                        Exit Function
                    End If
                End If
                lblPMKey.Text = .Fields("MKey").Value
                LblMKey.Text = ""
                lblPurchaseVNo.Text = VB6.Format(IIf(IsDBNull(.Fields("VNOSEQ").Value) Or .Fields("VNOSEQ").Value = "-1", "", .Fields("VNOSEQ").Value), "00000")
                mGSTStatus = IIf(.Fields("ISGSTAPPLICABLE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                If mGSTStatus = "G" Then
                    cboGSTStatus.SelectedIndex = 0
                ElseIf mGSTStatus = "R" Then
                    cboGSTStatus.SelectedIndex = 1
                ElseIf mGSTStatus = "E" Then
                    cboGSTStatus.SelectedIndex = 2
                ElseIf mGSTStatus = "N" Then
                    cboGSTStatus.SelectedIndex = 3
                Else
                    cboGSTStatus.SelectedIndex = 4
                End If
                cboGSTStatus.Enabled = False
                chkCreditRC.CheckState = IIf(.Fields("GST_RC_CLAIM").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                '            If chkCreditRC.Value = vbChecked Then
                '                txtModvatNo.Text = Format(IIf(IsNull(.Fields("GST_CLAIM_RC_NO").Value), "", .Fields("GST_CLAIM_RC_NO").Value), "00000")
                '                txtModvatDate.Text = Format(IIf(IsNull(.Fields("GST_CLAIM_RC_DATE").Value), "", .Fields("GST_CLAIM_RC_DATE").Value), "DD/MM/YYYY")
                '            Else
                '                txtModvatNo.Text = Format(IIf(IsNull(.Fields("GST_CLAIM_NO").Value), "", .Fields("GST_CLAIM_NO").Value), "00000")
                '                txtModvatDate.Text = Format(IIf(IsNull(.Fields("GST_CLAIM_DATE").Value), "", .Fields("GST_CLAIM_DATE").Value), "DD/MM/YYYY")
                '            End If
                If Trim(txtModvatNo.Text) <> "" Then
                    ChkCapital.Enabled = False
                End If
                txtTotCGSTRefund.Text = VB6.Format(IIf(IsDBNull(.Fields("TOTCGST_REFUNDAMT").Value), "", .Fields("TOTCGST_REFUNDAMT").Value), "0.00")
                txtTotSGSTRefund.Text = VB6.Format(IIf(IsDBNull(.Fields("TOTSGST_REFUNDAMT").Value), "", .Fields("TOTSGST_REFUNDAMT").Value), "0.00")
                txtTotIGSTRefund.Text = VB6.Format(IIf(IsDBNull(.Fields("TOTIGST_REFUNDAMT").Value), "", .Fields("TOTIGST_REFUNDAMT").Value), "0.00")
                lblTotCGSTAmount.Text = IIf(IsDBNull(.Fields("TOTCGST_AMOUNT").Value), "", .Fields("TOTCGST_AMOUNT").Value)
                lblTotSGSTAmount.Text = IIf(IsDBNull(.Fields("TOTSGST_AMOUNT").Value), "", .Fields("TOTSGST_AMOUNT").Value)
                lblTotIGSTAmount.Text = IIf(IsDBNull(.Fields("TOTIGST_AMOUNT").Value), "", .Fields("TOTIGST_AMOUNT").Value)
                txtServNo.Text = VB6.Format(IIf(IsDBNull(.Fields("SERVNo").Value), "", .Fields("SERVNo").Value), "00000")
                txtServDate.Text = VB6.Format(IIf(IsDBNull(.Fields("SERVDate").Value), "", .Fields("SERVDate").Value), "DD/MM/YYYY")
                lblServicePercentage.Text = VB6.Format(IIf(IsDBNull(.Fields("TOTSERVICEPERCENT").Value), 0, .Fields("TOTSERVICEPERCENT").Value), "0.00")
                txtPONo.Text = IIf(IsDBNull(.Fields("CUSTREFNO").Value), "", .Fields("CUSTREFNO").Value)
                txtPODate.Text = IIf(IsDBNull(.Fields("CUSTREFDATE").Value), "", .Fields("CUSTREFDATE").Value)
                txtBillNo.Text = IIf(IsDBNull(.Fields("BILLNO").Value), "", .Fields("BILLNO").Value)
                txtBillDate.Text = VB6.Format(IIf(IsDBNull(.Fields("INVOICE_DATE").Value), "", .Fields("INVOICE_DATE").Value), "DD/MM/YYYY")
                mSupplierCode = IIf(IsDBNull(.Fields("SUPP_CUST_CODE").Value), -1, .Fields("SUPP_CUST_CODE").Value) 'DEEPAK 10_09_2004
                If MainClass.ValidateWithMasterTable((.Fields("SUPP_CUST_CODE").Value), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtSupplier.Text = MasterNo
                End If
                If MainClass.ValidateWithMasterTable((.Fields("ACCOUNTCODE").Value), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtDebitAccount.Text = MasterNo
                End If
                chkShipTo.CheckState = IIf(.Fields("SHIPPED_TO_SAMEPARTY").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                If MainClass.ValidateWithMasterTable((.Fields("SHIPPED_TO_PARTY_CODE").Value), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtShippedTo.Text = MasterNo
                End If

                txtBillTo.Text = IIf(IsDBNull(.Fields("BILL_TO_LOC_ID").Value), "", .Fields("BILL_TO_LOC_ID").Value)
                TxtShipTo.Text = IIf(IsDBNull(.Fields("SHIP_TO_LOC_ID").Value), "", .Fields("SHIP_TO_LOC_ID").Value)


                txtCreditDays(0).Text = IIf(IsDBNull(.Fields("DUEDAYSFROM").Value), "", .Fields("DUEDAYSFROM").Value)
                txtCreditDays(1).Text = IIf(IsDBNull(.Fields("DUEDAYSTO").Value), "", .Fields("DUEDAYSTO").Value)
                chkCancelled.CheckState = IIf(.Fields("CANCELLED").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                '            If LblBookCode.text = ConPurchaseBookCode Then
                '                chkCancelled.Enabled = IIf(.Fields("CANCELLED").Value = "Y", False, True)
                '            Else
                '                chkCancelled.Enabled = False
                '            End If
                chkRejection.CheckState = IIf(.Fields("REJECTION").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                ChkCapital.CheckState = IIf(.Fields("ISCAPITAL").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                lblTotQty.Text = VB6.Format(IIf(IsDBNull(.Fields("TOTQTY").Value), 0, .Fields("TOTQTY").Value), "0.00")
                lblTotItemValue.Text = VB6.Format(IIf(IsDBNull(.Fields("ITEMVALUE").Value), 0, .Fields("ITEMVALUE").Value), "0.00")
                lblNetAmount.Text = VB6.Format(IIf(IsDBNull(.Fields("NETVALUE").Value), 0, .Fields("NETVALUE").Value), "0.00")
                txtTariff.Text = IIf(IsDBNull(.Fields("TARIFFHEADING").Value), "", .Fields("TARIFFHEADING").Value)
                txtItemType.Text = IIf(IsDBNull(.Fields("ITEMDESC").Value), "", .Fields("ITEMDESC").Value)
                txtRemarks.Text = IIf(IsDBNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)
                txtNarration.Text = IIf(IsDBNull(.Fields("NARRATION").Value), "", .Fields("NARRATION").Value)
                txtCarriers.Text = IIf(IsDBNull(.Fields("CARRIERS").Value), "", .Fields("CARRIERS").Value)
                txtVehicle.Text = IIf(IsDBNull(.Fields("VEHICLENO").Value), "", .Fields("VEHICLENO").Value)
                txtDocsThru.Text = IIf(IsDBNull(.Fields("DOCSTHROUGH").Value), "", .Fields("DOCSTHROUGH").Value)
                txtMode.Text = IIf(IsDBNull(.Fields("DESPATCHMODE").Value), "", .Fields("DESPATCHMODE").Value)
                OptFreight(0).Checked = True
                OptFreight(1).Checked = False
                mDivisionCode = IIf(IsDBNull(.Fields("DIV_CODE").Value), -1, .Fields("DIV_CODE").Value)
                If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionDesc = Trim(MasterNo)
                    cboDivision.Text = mDivisionDesc
                End If
                cboDivision.Enabled = False
                chkFinalPost.CheckState = IIf(.Fields("ISFINALPOST").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                If MainClass.ValidateWithMasterTable((txtMRRNo.Text), "AUTO_KEY_MRR", "REF_TYPE", "INV_GATE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mCustRefNo = MasterNo
                Else
                    mCustRefNo = "-1"
                End If
                txtAdvBal.Text = CStr(GetBalancePaymentAmount(mSupplierCode, txtBillDate.Text, "", "", mDivisionCode, "AP", mBalCGST, mBalSGST, mBalIGST))
                '            txtAdvCGSTBal.Text = Format(mBalCGST, "0.00")
                '            txtAdvSGSTBal.Text = Format(mBalSGST, "0.00")
                '            txtAdvIGSTBal.Text = Format(mBalIGST, "0.00")
                cmdResetMRR.Enabled = True
                Call ShowDetail1((.Fields("mKey").Value), mCustRefNo)
                Call ShowExp1((.Fields("mKey").Value))
                Call SprdExp_LeaveCell(SprdExp, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColExpAmt, 1, 1, 1, True))
                ''Call CalcTots
            End If
        End With
        ShowFromExcise1 = True
        FormatSprdMain(-1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Function
ERR1:
        ShowFromExcise1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Function
    Private Sub ShowMRRExp1(ByRef mMkey As String)
        On Error GoTo ERR1
        Dim I As Integer
        Dim pExpId As String
        Dim SqlStr As String = ""

        pShowCalc = False
        SqlStr = ""

        SqlStr = "Select INV_GATE_EXP.EXPCODE, INV_GATE_EXP.EXPPERCENT, " & vbCrLf _
            & " INV_GATE_EXP.AMOUNT, " & vbCrLf _
            & " FIN_INTERFACE_MST.Name as Name,FIN_INTERFACE_MST.Code, " & vbCrLf _
            & " Identification,Add_Ded,Taxable,Exciseable,CalcOn,RO,DebitAmount " & vbCrLf _
            & " From INV_GATE_EXP,FIN_INTERFACE_MST " & vbCrLf _
            & " Where " & vbCrLf _
            & " FIN_INTERFACE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND INV_GATE_EXP.ExpCode=FIN_INTERFACE_MST.Code " & vbCrLf _
            & " AND INV_GATE_EXP.Mkey='" & mMkey & "'"

        If PubGSTApplicable = True Then
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='N'"
        End If
        SqlStr = SqlStr & vbCrLf & " ORDER BY SUBROWNO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPurchExp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsPurchExp.EOF = False Then
            RsPurchExp.MoveFirst()
            With SprdExp
                Do While Not RsPurchExp.EOF
                    For I = 1 To .MaxRows
                        .Row = I
                        .Col = ColExpName
                        If .Text = RsPurchExp.Fields("Name").Value Then Exit For
                    Next I
                    .Col = ColExpPercent 'Exp. %
                    .Text = CStr(Val(IIf(IsDBNull(RsPurchExp.Fields("ExpPercent").Value), "", RsPurchExp.Fields("ExpPercent").Value)))
                    .Col = ColExpAmt
                    If RsPurchExp.Fields("Identification").Value = "RO" Then '30.10.2001   ''Allow '-' if exp. is ropund off
                        .Text = CStr(Val(IIf(IsDBNull(RsPurchExp.Fields("Amount").Value), "", RsPurchExp.Fields("Amount").Value)))
                    Else
                        .Text = CStr(System.Math.Abs(Val(IIf(IsDBNull(RsPurchExp.Fields("Amount").Value), "", RsPurchExp.Fields("Amount").Value))))
                    End If
                    .Col = ColExpSTCode
                    .Text = CStr(Val(IIf(IsDBNull(RsPurchExp.Fields("CODE").Value), 0, RsPurchExp.Fields("CODE").Value)))
                    .Col = ColExpAddDeduct 'ExpFlag
                    .Text = IIf(RsPurchExp.Fields("Add_Ded").Value = "A", "A", "D")
                    .Col = ColExpIdent
                    .Text = IIf(IsDBNull(RsPurchExp.Fields("Identification").Value), "", RsPurchExp.Fields("Identification").Value)
                    If .Text = "RO" Then 'round off
                        .Col = ColExpAmt
                        pRound = Val(.Text)
                    End If
                    SprdExp.Col = ColTaxable
                    SprdExp.Text = IIf(IsDBNull(RsPurchExp.Fields("Taxable").Value), "N", RsPurchExp.Fields("Taxable").Value)
                    SprdExp.Col = ColExciseable
                    SprdExp.Text = IIf(IsDBNull(RsPurchExp.Fields("Exciseable").Value), "N", RsPurchExp.Fields("Exciseable").Value)
                    SprdExp.Col = ColExpCalcOn
                    SprdExp.Text = CStr(Val(IIf(IsDBNull(RsPurchExp.Fields("CalcOn").Value), "", RsPurchExp.Fields("CalcOn").Value)))
                    .Col = ColExpDebitAmt
                    .Text = CStr(Val(IIf(IsDBNull(RsPurchExp.Fields("DebitAmount").Value), "", RsPurchExp.Fields("DebitAmount").Value)))
                    .Col = ColRO
                    .Value = IIf(RsPurchExp.Fields("RO").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                    RsPurchExp.MoveNext()
                Loop
            End With
            '    Else
            '        If ADDMode = True Then
            '            Call FillExpFromPartyExp
            '        End If
        End If
        pShowCalc = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub ShowExp1(ByRef mMkey As String)
        On Error GoTo ERR1
        Dim I As Integer
        Dim pExpId As String
        Dim SqlStr As String = ""
        Call FillSprdExp()
        pShowCalc = False
        SqlStr = ""
        SqlStr = "Select FIN_PURCHASE_EXP.EXPCODE,FIN_PURCHASE_EXP.EXPPERCENT, " & vbCrLf & " FIN_PURCHASE_EXP.AMOUNT, " & vbCrLf & " FIN_INTERFACE_MST.Name as Name,FIN_INTERFACE_MST.Code, " & vbCrLf & " Identification,Add_Ded,Taxable,Exciseable,CalcOn,RO,DebitAmount " & vbCrLf & " From FIN_PURCHASE_EXP,FIN_INTERFACE_MST " & vbCrLf & " Where " & vbCrLf & " FIN_INTERFACE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FIN_PURCHASE_EXP.ExpCode=FIN_INTERFACE_MST.Code " & vbCrLf & " AND FIN_PURCHASE_EXP.Mkey='" & mMkey & "'"
        If PubGSTApplicable = True Then
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='N'"
        End If
        SqlStr = SqlStr & vbCrLf & " ORDER BY SUBROWNO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPurchExp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsPurchExp.EOF = False Then
            RsPurchExp.MoveFirst()
            With SprdExp
                Do While Not RsPurchExp.EOF
                    For I = 1 To .MaxRows
                        .Row = I
                        .Col = ColExpName
                        If .Text = RsPurchExp.Fields("Name").Value Then Exit For
                    Next I
                    .Col = ColExpPercent 'Exp. %
                    .Text = CStr(Val(IIf(IsDBNull(RsPurchExp.Fields("ExpPercent").Value), "", RsPurchExp.Fields("ExpPercent").Value)))
                    .Col = ColExpAmt
                    If RsPurchExp.Fields("Identification").Value = "RO" Then '30.10.2001   ''Allow '-' if exp. is ropund off
                        .Text = CStr(Val(IIf(IsDBNull(RsPurchExp.Fields("Amount").Value), "", RsPurchExp.Fields("Amount").Value)))
                    Else
                        .Text = CStr(System.Math.Abs(Val(IIf(IsDBNull(RsPurchExp.Fields("Amount").Value), "", RsPurchExp.Fields("Amount").Value))))
                    End If
                    .Col = ColExpSTCode
                    .Text = CStr(Val(IIf(IsDBNull(RsPurchExp.Fields("CODE").Value), 0, RsPurchExp.Fields("CODE").Value)))
                    .Col = ColExpAddDeduct 'ExpFlag
                    .Text = IIf(RsPurchExp.Fields("Add_Ded").Value = "A", "A", "D")
                    .Col = ColExpIdent
                    .Text = IIf(IsDBNull(RsPurchExp.Fields("Identification").Value), "", RsPurchExp.Fields("Identification").Value)
                    If .Text = "RO" Then 'round off
                        .Col = ColExpAmt
                        pRound = Val(.Text)
                    End If
                    SprdExp.Col = ColTaxable
                    SprdExp.Text = IIf(IsDBNull(RsPurchExp.Fields("Taxable").Value), "N", RsPurchExp.Fields("Taxable").Value)
                    SprdExp.Col = ColExciseable
                    SprdExp.Text = IIf(IsDBNull(RsPurchExp.Fields("Exciseable").Value), "N", RsPurchExp.Fields("Exciseable").Value)
                    SprdExp.Col = ColExpCalcOn
                    SprdExp.Text = CStr(Val(IIf(IsDBNull(RsPurchExp.Fields("CalcOn").Value), "", RsPurchExp.Fields("CalcOn").Value)))
                    .Col = ColExpDebitAmt
                    .Text = CStr(Val(IIf(IsDBNull(RsPurchExp.Fields("DebitAmount").Value), "", RsPurchExp.Fields("DebitAmount").Value)))
                    .Col = ColRO
                    .Value = IIf(RsPurchExp.Fields("RO").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                    RsPurchExp.MoveNext()
                Loop
            End With
            '    Else
            '        If ADDMode = True Then
            '            Call FillExpFromPartyExp
            '        End If
        End If
        pShowCalc = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub ShowDetail1(ByRef mMkey As String, ByRef mCustRefType As String)
        On Error GoTo ERR1
        Dim I As Integer
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mShortageQty As Double
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mPartNo As String
        Dim SqlStr As String = ""
        Dim mBillNo As Double
        Dim mReOffer As Double
        Dim mRejQty As Double
        Dim mHSNCode As String
        Dim pCGSTPer As Double
        Dim pSGSTPer As Double
        Dim pIGSTPer As Double
        Dim mPORate As Double
        'Dim mHSNCode As String

        SqlStr = ""
        SqlStr = " SELECT FIN_PURCHASE_DET.*, "
        If mCustRefType = "I" Or mCustRefType = "2" Or mCustRefType = "3" Then
            If MainClass.ValidateWithMasterTable((txtMRRNo.Text), "AUTO_KEY_MRR", "REF_PO_NO", "INV_GATE_DET", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mBillNo = MasterNo
            Else
                mBillNo = -1
            End If
            mBillNo = IIf(Val(CStr(mBillNo)) = 0, -1, mBillNo)
            SqlStr = SqlStr & " GetSALEITEMPRICE(" & mBillNo & ",CUST_REF_NO, '" & mSupplierCode & "',ITEM_CODE) AS PORATE, "
            SqlStr = SqlStr & " 0 AS VOL_DISCRATE "
        ElseIf mCustRefType = "P" Then
            SqlStr = SqlStr & " GetITEMPRICE_NEW(1,1,TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),CUST_REF_NO, ITEM_CODE) AS PORATE, "
            SqlStr = SqlStr & " GetVOL_DISC_ITEM(1,1,TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),CUST_REF_NO, ITEM_CODE) AS VOL_DISCRATE "
        ElseIf mCustRefType = "R" Then
            SqlStr = SqlStr & " GetITEMJWRate(" & RsCompany.Fields("COMPANY_CODE").Value & ",1,TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),CUST_REF_NO, " & Val(txtMRRNo.Text) & ", ITEM_CODE,SUBROWNO) AS PORATE, "
            SqlStr = SqlStr & " 0 AS VOL_DISCRATE "
        Else
            SqlStr = SqlStr & " 0 AS PORATE, "
            SqlStr = SqlStr & " 0 AS VOL_DISCRATE "
        End If
        SqlStr = SqlStr & vbCrLf & " FROM FIN_PURCHASE_DET " & vbCrLf & " Where Mkey='" & mMkey & "'" & vbCrLf & " Order By SubRowNo"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPurchDetail, ADODB.LockTypeEnum.adLockReadOnly)

        With RsPurchDetail
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            I = 1
            .MoveFirst()
            Do While Not .EOF
                SprdMain.Row = I
                SprdMain.Col = ColItemCode
                mItemCode = IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)


                SprdMain.Text = mItemCode
                SprdMain.Col = ColItemDesc
                MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mItemDesc = MasterNo
                SprdMain.Text = mItemDesc ''IIf(IsNull(.Fields("ITEM_DESC").Value), "", .Fields("ITEM_DESC").Value)
                SprdMain.Col = ColPartNo
                MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "CUSTOMER_PART_NO", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mPartNo = MasterNo
                SprdMain.Text = mPartNo ''IIf(IsNull(.Fields("CUSTOMER_PART_NO").Value), "", .Fields("CUSTOMER_PART_NO").Value)
                'If lblPurchaseType.Text = "G" Then
                '    If mCustRefType = "P" Then
                mHSNCode = IIf(IsDBNull(.Fields("HSNCODE").Value), "", .Fields("HSNCODE").Value)
                '    Else
                '        mHSNCode = GetHSNCode(mItemCode)
                '    End If
                'Else
                '    mHSNCode = GetSACCode((txtServProvided.Text))
                'End If
                SprdMain.Col = ColHSN
                SprdMain.Text = mHSNCode
                SprdMain.Col = ColQty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_QTY").Value), 0, .Fields("ITEM_QTY").Value)))
                SprdMain.Col = ColUnit
                SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)

                SprdMain.Col = ColPORate
                mPORate = Val(IIf(IsDBNull(.Fields("PORATE").Value), 0, .Fields("PORATE").Value))
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("PORATE").Value), 0, .Fields("PORATE").Value)))

                SprdMain.Col = ColVolDiscRate
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("VOL_DISCRATE").Value), 0, .Fields("VOL_DISCRATE").Value)))
                SprdMain.Col = ColRate
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_RATE").Value), 0, .Fields("ITEM_RATE").Value)))
                SprdMain.Col = ColAmount
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_AMT").Value), 0, .Fields("ITEM_AMT").Value)))
                SprdMain.Col = ColTaxableAmount
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("GSTABLE_AMT").Value), 0, .Fields("GSTABLE_AMT").Value)))
                SprdMain.Col = ColPONo
                SprdMain.Text = CStr(IIf(IsDBNull(.Fields("CUST_REF_NO").Value), "", .Fields("CUST_REF_NO").Value))

                SprdMain.Col = ColCGSTPer
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("CGST_PER").Value), 0, .Fields("CGST_PER").Value)))
                SprdMain.Col = ColSGSTPer
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("SGST_PER").Value), 0, .Fields("SGST_PER").Value)))
                SprdMain.Col = ColIGSTPer
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("IGST_PER").Value), 0, .Fields("IGST_PER").Value)))

                SprdMain.Col = ColInvType
                If IsDBNull(.Fields("ITEM_TRNTYPE").Value) = True Then
                    SprdMain.Text = ""  ''Trim(cboInvType.Text)
                Else
                    If MainClass.ValidateWithMasterTable(.Fields("ITEM_TRNTYPE").Value, "CODE", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        SprdMain.Text = MasterNo
                    Else
                        SprdMain.Text = ""  'Trim(cboInvType.Text)
                    End If
                End If

                SqlStr = ""
                SqlStr = " SELECT RECEIVED_QTY,SHORTAGE_QTY,REJECTED_QTY,ITEM_RATE, " & vbCrLf _
                    & " GETREOFFERQTY(" & RsCompany.Fields("COMPANY_CODE").Value & ",AUTO_KEY_MRR,MRR_DATE,SUPP_CUST_CODE,ITEM_CODE) AS REOFFER " & vbCrLf _
                    & " FROM INV_GATE_DET " & vbCrLf _
                    & " Where AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & "" & vbCrLf _
                    & " AND ITEM_CODE='" & RsPurchDetail.Fields("ITEM_CODE").Value & "' and SERIAL_NO=" & Val(CStr(I)) & ""

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTemp.EOF = False Then
                    SprdMain.Col = ColAcceptedQty
                    SprdMain.Text = CStr(Val(IIf(IsDBNull(RsTemp.Fields("RECEIVED_QTY").Value), 0, RsTemp.Fields("RECEIVED_QTY").Value)))
                    SprdMain.Col = ColShortageQty
                    SprdMain.Text = CStr(Val(IIf(IsDBNull(RsTemp.Fields("SHORTAGE_QTY").Value), 0, RsTemp.Fields("SHORTAGE_QTY").Value)))
                    mShortageQty = Val(SprdMain.Text)
                    SprdMain.Col = ColRejectedQty
                    mReOffer = IIf(IsDBNull(RsTemp.Fields("REOFFER").Value), 0, RsTemp.Fields("REOFFER").Value)
                    mRejQty = IIf(IsDBNull(RsTemp.Fields("REJECTED_QTY").Value), 0, RsTemp.Fields("REJECTED_QTY").Value)
                    SprdMain.Text = CStr(Val(CStr(mRejQty))) ''Val(mRejQty - mReOffer)

                End If

                .MoveNext()
                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With
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
            FormatSprdView()
            UltraGrid1.Focus()
            FraFront.SendToBack()
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraFront.BringToFront()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsPurchMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub CalcTots()
        On Error GoTo ERR1
        Dim mQty As Double
        Dim mRate As Double
        Dim mItemAmount As Double
        Dim mTotQty As Double
        Dim j As Integer
        Dim I As Integer
        Dim mItemCode As String
        Dim xStr As String
        Dim mExpPercent As Double
        Dim mNetAccessAmt As Double
        Dim mShortage As Double
        Dim mTotItemAmount As Double
        Dim pTotOthers As Double
        Dim pTotMSC As Double
        Dim pTotDiscount As Double
        Dim pTotRO As Double
        Dim pTotTCS As Double
        Dim mTotExp As Double
        Dim pCGSTPer As Double
        Dim pSGSTPer As Double
        Dim pIGSTPer As Double
        Dim pTCSPer As Double
        Dim mGSTableAmount As Double
        Dim mCGSTAmount As Double
        Dim mSGSTAmount As Double
        Dim mIGSTAmount As Double
        Dim pTotCGSTAmount As Double
        Dim pTotSGSTAmount As Double
        Dim pTotIGSTAmount As Double
        Dim pTotCGSTRefundAmount As Double
        Dim pTotSGSTRefundAmount As Double
        Dim pTotIGSTRefundAmount As Double
        Dim mExpName As String
        Dim mIsTaxable As String
        Dim mOtherTaxableAmount As Double
        Dim mTotTaxableItemAmount As Double
        Dim mPartyGSTNo As String
        Dim mCompanyGSTNo As String
        Dim mLocal As String = ""
        Dim mSuppCustCode As String = ""
        Dim mHSNCode As String

        Dim mMaxCGST As Double = 0
        Dim mMaxSGST As Double = 0
        Dim mMaxIGST As Double = 0
        Dim mAddDeduct As String = "A"


        If FormActive = False Then Exit Sub

        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSuppCustCode = MasterNo
        End If

        mCompanyGSTNo = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
        'mPartyGSTNo = ""
        'If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '    mPartyGSTNo = MasterNo
        'End If

        mLocal = "N"
        mPartyGSTNo = ""
        mLocal = GetPartyBusinessDetail(Trim(mSuppCustCode), Trim(txtBillTo.Text), "WITHIN_STATE")
        mPartyGSTNo = GetPartyBusinessDetail(Trim(mSuppCustCode), Trim(txtBillTo.Text), "GST_RGN_NO")

        pRound = 0
        mQty = 0
        mRate = 0
        '    mST = 0
        mItemAmount = 0
        mTotItemAmount = 0
        mOtherTaxableAmount = 0
        mTotExp = 0
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

                    'mAddDeduct
                    If MainClass.ValidateWithMasterTable(mExpName, "NAME", "ADD_DED", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GST_ENABLED='Y'") = True Then
                        mAddDeduct = MasterNo
                    Else
                        mAddDeduct = "A"
                    End If

                    mOtherTaxableAmount = mOtherTaxableAmount + (IIf(mAddDeduct = "A", 1, -1) * CDbl(VB6.Format(.Text, "0.00")))
                End If
            Next
        End With
        With SprdMain
            j = .MaxRows
            For I = 1 To j
                .Row = I
                .Col = 0
                If .Text = "Del" Then GoTo DontCalc

                .Col = ColItemCode
                If .Text = "" Then GoTo DontCalc
                mItemCode = .Text

                .Col = ColQty
                mQty = Val(.Text)

                .Col = ColShortageQty
                If Val(.Text) >= 0 Then
                    mShortage = mQty - Val(.Text)
                Else
                    mShortage = mQty
                End If

                mTotQty = mTotQty + mQty

                .Col = ColRate
                mRate = Val(.Text)
                .Text = CStr(mRate)

                .Col = ColAmount
                .Text = VB6.Format(mQty * mRate, "0.00")
                mTotItemAmount = mTotItemAmount + CDbl(VB6.Format(mQty * mRate, "0.00"))

                .Col = ColHSN
                'mHSNCode = Trim(.Text)
                'If GetHSNDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, VB.Left(cboGSTStatus.Text, 1), mPartyGSTNo) = False Then GoTo ERR1

                'If ADDMode = True Then
                If lblPurchaseType.Text = "J" Or lblPurchaseType.Text = "R" Then
                        mHSNCode = GetSACCode(txtServProvided.Text)
                        If GetSACDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, mPartyGSTNo, VB.Left(cboGSTStatus.Text, 1)) = False Then GoTo ERR1
                    Else
                        mHSNCode = Trim(.Text)
                        If GetHSNDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, Mid(cboGSTStatus.Text, 1, 1), mPartyGSTNo) = False Then GoTo ERR1
                    End If

                    SprdMain.Col = ColCGSTPer
                    SprdMain.Text = VB6.Format(pCGSTPer, "0.00")
                    mMaxCGST = IIf(Val(SprdMain.Text) > mMaxCGST, Val(SprdMain.Text), mMaxCGST)

                    SprdMain.Col = ColSGSTPer
                    SprdMain.Text = VB6.Format(pSGSTPer, "0.00")
                    mMaxSGST = IIf(Val(SprdMain.Text) > mMaxSGST, Val(SprdMain.Text), mMaxSGST)

                    SprdMain.Col = ColIGSTPer
                    SprdMain.Text = VB6.Format(pIGSTPer, "0.00")
                    mMaxIGST = IIf(Val(SprdMain.Text) > mMaxIGST, Val(SprdMain.Text), mMaxIGST)

                'End If




DontCalc:
            Next I
        End With
        mTotTaxableItemAmount = mTotItemAmount + mOtherTaxableAmount
        With SprdMain
            j = .MaxRows
            For I = 1 To j
                .Row = I
                .Col = 0
                If .Text = "Del" Then GoTo DontCalc1
                .Col = ColItemCode
                If .Text = "" Then GoTo DontCalc1
                mItemCode = .Text
                .Col = ColQty
                mQty = Val(.Text)
                .Col = ColShortageQty
                If Val(.Text) >= 0 Then
                    mShortage = mQty - Val(.Text)
                Else
                    mShortage = mQty
                End If
                '            mTotQty = mTotQty + mQty
                .Col = ColRate
                mRate = Val(.Text)

                .Col = ColCGSTPer
                pCGSTPer = Val(.Text)

                .Col = ColSGSTPer
                pSGSTPer = Val(.Text)

                .Col = ColIGSTPer
                pIGSTPer = Val(.Text)

                .Col = ColAmount
                mItemAmount = CDbl(VB6.Format(mQty * mRate, "0.00"))

                .Col = ColTaxableAmount
                If mTotItemAmount = 0 Then
                    mGSTableAmount = 0
                Else
                    'If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
                    mGSTableAmount = mItemAmount + CDbl(VB6.Format((mOtherTaxableAmount * mItemAmount / mTotItemAmount), "0.00"))
                    'Else
                    'mGSTableAmount = mItemAmount + CDbl(VB6.Format(mOtherTaxableAmount * mItemAmount / mTotItemAmount, "0.00")) '' VB6.Format(Val(.Text), "0.00")	
                    'End If
                    'mGSTableAmount = mItemAmount + CDbl(VB6.Format(mOtherTaxableAmount * mItemAmount / mTotItemAmount, "0.00")) '' Format(Val(.Text), "0.00")
                End If

                .Text = VB6.Format(Val(CStr(mGSTableAmount)), "0.00")
                If mTotItemAmount = 0 Then
                    mCGSTAmount = 0
                    mSGSTAmount = 0
                    mIGSTAmount = 0
                Else
                    mCGSTAmount = CDbl(VB6.Format(mItemAmount * pCGSTPer * 0.01, "0.00")) + CDbl(VB6.Format((mOtherTaxableAmount * mItemAmount / mTotItemAmount) * mMaxCGST * 0.01, "0.00"))
                    mSGSTAmount = CDbl(VB6.Format(mItemAmount * pSGSTPer * 0.01, "0.00")) + CDbl(VB6.Format((mOtherTaxableAmount * mItemAmount / mTotItemAmount) * mMaxSGST * 0.01, "0.00"))
                    mIGSTAmount = CDbl(VB6.Format(mItemAmount * pIGSTPer * 0.01, "0.00")) + CDbl(VB6.Format((mOtherTaxableAmount * mItemAmount / mTotItemAmount) * mMaxIGST * 0.01, "0.00"))

                End If

                .Col = ColCGSTAmount
                .Text = VB6.Format(mCGSTAmount, "0.00")
                .Col = ColSGSTAmount
                .Text = VB6.Format(mSGSTAmount, "0.00")
                .Col = ColIGSTAmount
                .Text = VB6.Format(mIGSTAmount, "0.00")
                pTotCGSTAmount = pTotCGSTAmount + mCGSTAmount
                pTotSGSTAmount = pTotSGSTAmount + mSGSTAmount
                pTotIGSTAmount = pTotIGSTAmount + mIGSTAmount

                If mCompanyGSTNo = mPartyGSTNo Then
                    pTotCGSTRefundAmount = 0
                    pTotSGSTRefundAmount = 0
                    pTotIGSTRefundAmount = 0
                ElseIf VB.Left(cboGSTStatus.Text, 1) = "G" Then
                    pTotCGSTRefundAmount = pTotCGSTRefundAmount + mCGSTAmount ''Format(mQty * mRate * pCGSTPer * 0.01, "0.00")
                    pTotSGSTRefundAmount = pTotSGSTRefundAmount + mSGSTAmount ''Format(mQty * mRate * pSGSTPer * 0.01, "0.00")
                    pTotIGSTRefundAmount = pTotIGSTRefundAmount + mIGSTAmount ''Format(mQty * mRate * pIGSTPer * 0.01, "0.00")
                Else
                    pTotCGSTRefundAmount = 0
                    pTotSGSTRefundAmount = 0
                    pTotIGSTRefundAmount = 0
                End If
DontCalc1:
            Next I
        End With

        'If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
        'mGSTableAmount = mGSTableAmount + mOtherTaxableAmount
        'mCGSTAmount = CDbl(VB6.Format(mOtherTaxableAmount * mMaxCGST * 0.01, "0.00"))
        'mSGSTAmount = CDbl(VB6.Format(mOtherTaxableAmount * mMaxSGST * 0.01, "0.00"))
        'mIGSTAmount = CDbl(VB6.Format(mOtherTaxableAmount * mMaxIGST * 0.01, "0.00"))

        mCGSTAmount = 0
        mSGSTAmount = 0
        mIGSTAmount = 0

        If mCompanyGSTNo = mPartyGSTNo Then
            pTotCGSTRefundAmount = 0
            pTotSGSTRefundAmount = 0
            pTotIGSTRefundAmount = 0
        ElseIf VB.Left(cboGSTStatus.Text, 1) = "G" Then
            pTotCGSTRefundAmount = pTotCGSTRefundAmount + mCGSTAmount ''Format(mQty * mRate * pCGSTPer * 0.01, "0.00")
            pTotSGSTRefundAmount = pTotSGSTRefundAmount + mSGSTAmount ''Format(mQty * mRate * pSGSTPer * 0.01, "0.00")
            pTotIGSTRefundAmount = pTotIGSTRefundAmount + mIGSTAmount ''Format(mQty * mRate * pIGSTPer * 0.01, "0.00")
        Else
            pTotCGSTRefundAmount = 0
            pTotSGSTRefundAmount = 0
            pTotIGSTRefundAmount = 0
        End If

        pTotCGSTAmount = pTotCGSTAmount + mCGSTAmount
        pTotSGSTAmount = pTotSGSTAmount + mSGSTAmount
        pTotIGSTAmount = pTotIGSTAmount + mIGSTAmount

        'mNetGSTAmount = mNetGSTAmount + mCGSTAmount + mSGSTAmount + mIGSTAmount

        'Else
        '    'mGSTableAmount = mItemAmount + CDbl(VB6.Format(mOtherTaxableAmount * mItemAmount / mTotItemAmount, "0.00")) '' VB6.Format(Val(.Text), "0.00")	
        'End If


        pTotDiscount = 0
        pTotRO = 0
        pTotTCS = 0
        mTotExp = 0
        mNetAccessAmt = Val(CStr(mTotItemAmount + mOtherTaxableAmount))
        Call BillExpensesCalcTots_GST(SprdExp, (txtBillDate.Text), mNetAccessAmt, mTotItemAmount, mTotItemAmount, 0, 0, 0, pTotIGSTAmount, pTotSGSTAmount, pTotCGSTAmount, 0, 0, 0, pTotOthers, 0, 0, pTotMSC, pTotDiscount, 0, pTotRO, pTotTCS, mTotExp, pTCSPer, "PA")
        lblTotItemValue.Text = VB6.Format(mTotItemAmount, "#0.00")
        If mCompanyGSTNo = mPartyGSTNo Then
            lblNetAmount.Text = VB6.Format(mTotItemAmount + mTotExp, "#0.00")
        Else
            If VB.Left(cboGSTStatus.Text, 1) = "G" Or VB.Left(cboGSTStatus.Text, 1) = "I" Then
                lblNetAmount.Text = VB6.Format(mTotItemAmount + pTotCGSTAmount + pTotSGSTAmount + pTotIGSTAmount + mTotExp, "#0.00")
            Else
                lblNetAmount.Text = VB6.Format(mTotItemAmount + mTotExp, "#0.00")
            End If
        End If
        lblTotTaxableAmt.Text = VB6.Format(Val(CStr(mTotItemAmount + mOtherTaxableAmount)), "#0.00")
        lblTotFreight.Text = CStr(0) ''Format(pTotOthers, "#0.00")
        lblTotCharges.Text = VB6.Format(pTotOthers, "#0.00") ''Format(mRO, "#0.00")
        lblTotExpAmt.Text = VB6.Format(mTotExp, "#0.00")
        lblRO.Text = VB6.Format(pTotRO, "#0.00")
        lblDiscount.Text = VB6.Format(pTotDiscount, "#0.00")
        lblMSC.Text = VB6.Format(pTotMSC, "#0.00")
        lblTotQty.Text = VB6.Format(mTotQty, "#0.00")
        lblTotCGSTAmount.Text = VB6.Format(pTotCGSTAmount, "#0.00")
        lblTotSGSTAmount.Text = VB6.Format(pTotSGSTAmount, "#0.00")
        lblTotIGSTAmount.Text = VB6.Format(pTotIGSTAmount, "#0.00")
        If VB.Left(cboGSTStatus.Text, 1) = "G" Then 'If chkFinalPost.Value = vbUnchecked Then
            txtTotCGSTRefund.Text = VB6.Format(pTotCGSTRefundAmount, "#0.00")
            txtTotSGSTRefund.Text = VB6.Format(pTotSGSTRefundAmount, "#0.00")
            txtTotIGSTRefund.Text = VB6.Format(pTotIGSTRefundAmount, "#0.00")
        Else
            txtTotCGSTRefund.Text = VB6.Format(0, "#0.00")
            txtTotSGSTRefund.Text = VB6.Format(0, "#0.00")
            txtTotIGSTRefund.Text = VB6.Format(0, "#0.00")
        End If

        If chkTDS.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtTDSDeductOn.Text = VB6.Format(IIf(Val(txtTDSDeductOn.Text) = 0, lblTotItemValue.Text, txtTDSDeductOn.Text), "#0.00")
        Else
            txtTDSDeductOn.Text = VB6.Format(lblTotItemValue.Text, "#0.00")
        End If

        If chkESI.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtESIDeductOn.Text = VB6.Format(IIf(Val(txtESIDeductOn.Text) = 0, lblNetAmount.Text, txtESIDeductOn.Text), "#0.00")
        Else
            txtESIDeductOn.Text = VB6.Format(lblNetAmount.Text, "#0.00")
        End If
        If ChkSTDS.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtSTDSDeductOn.Text = VB6.Format(IIf(Val(txtSTDSDeductOn.Text) = 0, lblNetAmount.Text, txtSTDSDeductOn.Text), "#0.00")
        Else
            txtSTDSDeductOn.Text = VB6.Format(lblNetAmount.Text, "#0.00")
        End If

        If chkTDS.CheckState = System.Windows.Forms.CheckState.Checked Then
            If CheckFullLotRejection() = True Then
                txtTDSAmount.Text = "0.00"
                chkTDS.CheckState = System.Windows.Forms.CheckState.Unchecked
            Else
                txtTDSAmount.Text = VB6.Format(Val(txtTDSRate.Text) * Val(txtTDSDeductOn.Text) / 100, "0.00")
            End If

            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
                txtTDSAmount.Text = IIf(Val(txtTDSAmount.Text) > Int(txtTDSAmount.Text), Int(txtTDSAmount.Text) + 1, Val(txtTDSAmount.Text))
            Else
                If ChkTDSRO.CheckState = System.Windows.Forms.CheckState.Checked Then
                    txtTDSAmount.Text = VB6.Format(System.Math.Round(Val(txtTDSAmount.Text), 0), "0.00")
                    If Val(txtTDSRate.Text) > 0 And Val(txtTDSDeductOn.Text) > 0 And Val(txtTDSAmount.Text) <= 1 Then
                        txtTDSAmount.Text = 1
                    End If
                End If
            End If
        Else
            txtTDSAmount.Text = "0.00"
        End If


        If chkESI.CheckState = System.Windows.Forms.CheckState.Checked Then
            If ChkESIRO.CheckState = System.Windows.Forms.CheckState.Checked Then
                txtESIAmount.Text = VB6.Format(System.Math.Round(Val(txtESIRate.Text) * Val(txtESIDeductOn.Text) / 100, 0), "0.00")
            Else
                txtESIAmount.Text = VB6.Format(Val(txtESIRate.Text) * Val(txtESIDeductOn.Text) / 100, "0.00")
            End If
        Else
            txtESIAmount.Text = "0.00"
        End If
        If ChkSTDS.CheckState = System.Windows.Forms.CheckState.Checked Then
            If ChkSTDSRO.CheckState = System.Windows.Forms.CheckState.Checked Then
                txtSTDSAmount.Text = VB6.Format(System.Math.Round(Val(txtSTDSRate.Text) * Val(txtSTDSDeductOn.Text) / 100, 0), "0.00")
            Else
                txtSTDSAmount.Text = VB6.Format(Val(txtSTDSRate.Text) * Val(txtSTDSDeductOn.Text) / 100, "0.00")
            End If
        Else
            txtSTDSAmount.Text = "0.00"
        End If
        txtServiceTaxAmount.Text = VB6.Format(System.Math.Round(Val(txtServiceOn.Text) * Val(txtServiceTaxPer.Text) * 0.01, 0), "0.00")
        Call CheckPORate()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Private Sub CalcAdvTots()
        On Error GoTo ERR1
        Dim mNetAdvanceAmount As Double
        txtItemAdvAdjust.Text = VB6.Format(txtItemAdvAdjust.Text, "0.00")
        mNetAdvanceAmount = Val(txtItemAdvAdjust.Text)
        txtAdvCGST.Text = VB6.Format(txtAdvCGST.Text, "0.00")
        mNetAdvanceAmount = mNetAdvanceAmount + Val(txtAdvCGST.Text)
        txtAdvSGST.Text = VB6.Format(txtAdvSGST.Text, "0.00")
        mNetAdvanceAmount = mNetAdvanceAmount + Val(txtAdvSGST.Text)
        txtAdvIGST.Text = VB6.Format(txtAdvIGST.Text, "0.00")
        mNetAdvanceAmount = mNetAdvanceAmount + Val(txtAdvIGST.Text)
        txtAdvAdjust.Text = VB6.Format(mNetAdvanceAmount, "0.00")
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Private Sub CalcTotsDNCN(ByRef pMKey As String, ByRef pDnCnType As String, ByRef xItemValue As Double, ByRef xTOTFREIGHT As Double, ByRef xTOTCHARGES As Double, ByRef xTotDiscount As Double, ByRef xMSC As Double, ByRef xRO As Double, ByRef xTOTEXPAMT As Double, ByRef xNETVALUE As Double, ByRef xTotQty As Double, ByRef xCGSTPer As Double, ByRef xSGSTPer As Double, ByRef xIGSTPer As Double, ByRef xCGSTAmount As Double, ByRef xSGSTAmount As Double, ByRef xIGSTAmount As Double, ByRef xCGSTRefundAmount As Double, ByRef xSGSTRefundAmount As Double, ByRef xIGSTRefundAmount As Double)
        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mQty As Double
        Dim mRate As Double
        Dim mDiscount As Double
        Dim mItemAmount As Double
        Dim mItemValue As Double
        Dim mTotQty As Double
        Dim mTotItemAmount As Double
        Dim mTotExp As Double
        Dim mTotDiscount As Double
        Dim j As Integer
        Dim I As Integer
        Dim mST As Decimal
        Dim mItemCode As String
        Dim xStr As String
        Dim mExpPercent As Double
        Dim mNetAccessAmt As Double
        Dim mOTRCharges As Double
        Dim mRO As Double
        Dim mExp As Double
        Dim mRoType As String
        Dim mExpAddDeduct As String
        Dim mMSC As Double
        Dim mExpCode As Integer
        Dim mCGSTPer As Double
        Dim mSGSTPer As Double
        Dim mIGSTPer As Double
        Dim mCGSTAmount As Double
        Dim mSGSTAmount As Double
        Dim mIGSTAmount As Double
        Dim mExpAmount As Double
        pRound = 0
        mQty = 0
        mRate = 0
        mItemAmount = 0
        mTotItemAmount = 0
        mItemValue = 0
        mTotExp = 0
        SqlStr = "SELECT * FROM FIN_DNCN_DET WHERE MKEY='" & pMKey & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMisc.EOF = False Then
            Do While Not RsMisc.EOF
                mItemCode = IIf(IsDBNull(RsMisc.Fields("ITEM_CODE").Value), "", RsMisc.Fields("ITEM_CODE").Value)
                If mItemCode = "" Then GoTo DontCalc
                mQty = IIf(IsDBNull(RsMisc.Fields("ITEM_QTY").Value), "", RsMisc.Fields("ITEM_QTY").Value)
                mTotQty = mTotQty + mQty
                mRate = IIf(IsDBNull(RsMisc.Fields("ITEM_RATE").Value), "", RsMisc.Fields("ITEM_RATE").Value)
                mItemAmount = IIf(IsDBNull(RsMisc.Fields("ITEM_AMT").Value), "", RsMisc.Fields("ITEM_AMT").Value)
                mTotItemAmount = mTotItemAmount + mItemAmount
                mItemValue = CDbl(VB6.Format(mItemAmount, "0.00"))
                mCGSTAmount = mCGSTAmount + IIf(IsDBNull(RsMisc.Fields("CGST_AMOUNT").Value), 0, RsMisc.Fields("CGST_AMOUNT").Value)
                mSGSTAmount = mSGSTAmount + IIf(IsDBNull(RsMisc.Fields("SGST_AMOUNT").Value), 0, RsMisc.Fields("SGST_AMOUNT").Value)
                mIGSTAmount = mIGSTAmount + IIf(IsDBNull(RsMisc.Fields("IGST_AMOUNT").Value), 0, RsMisc.Fields("IGST_AMOUNT").Value)
DontCalc:
                RsMisc.MoveNext()
            Loop
        End If
        mNetAccessAmt = Val(CStr(mTotItemAmount))
        SqlStr = "SELECT EXP.MKEY ,EXP.SUBROWNO, EXP.EXPCODE, EXP.EXPPERCENT, " & vbCrLf & " EXP.AMOUNT, EXP.CALCON, EXP.RO,  " & vbCrLf & " IMST.IDENTIFICATION,ADD_DED,EXCISEABLE,TAXABLE,CESSABLE " & vbCrLf & " FROM FIN_DNCN_EXP EXP, FIN_INTERFACE_MST IMST" & vbCrLf & " WHERE EXP.MKEY='" & pMKey & "'" & vbCrLf & " AND IMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EXP.EXPCODE=IMST.CODE" & vbCrLf
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMisc.EOF = False Then
            Do While Not RsMisc.EOF
                mRoType = IIf(IsDBNull(RsMisc.Fields("RO").Value), "N", RsMisc.Fields("RO").Value)
                xStr = IIf(IsDBNull(RsMisc.Fields("Identification").Value), "", RsMisc.Fields("Identification").Value)
                mExpPercent = IIf(IsDBNull(RsMisc.Fields("EXPPERCENT").Value), "0", RsMisc.Fields("EXPPERCENT").Value)
                mExpAddDeduct = IIf(IsDBNull(RsMisc.Fields("ADD_DED").Value), "A", RsMisc.Fields("ADD_DED").Value)
                mExpCode = IIf(IsDBNull(RsMisc.Fields("EXPCODE").Value), "-1", RsMisc.Fields("EXPCODE").Value)
                mExpAmount = IIf(IsDBNull(RsMisc.Fields("Amount").Value), "0", RsMisc.Fields("Amount").Value)
                Select Case xStr
                    Case "DOB"
                        '                    If mExpPercent <> 0 Then
                        mDiscount = mExpAmount
                        If mRoType = "Y" Then
                            mDiscount = System.Math.Round(Val(CStr(mExpAmount)), 0)
                        End If
                        '                    End If
                        mTotDiscount = mTotDiscount + (mDiscount * IIf(mExpAddDeduct = "D", -1, 1))
                        mNetAccessAmt = Val(CStr(mNetAccessAmt)) - Val(CStr(mDiscount))
                        mExp = mDiscount
                    Case "MSC"
                        mMSC = mMSC + (Val(CStr(mExpAmount)) * IIf(mExpAddDeduct = "D", -1, 1))
                        mExp = Val(CStr(mExpAmount))
                    Case "OTR", "FRO", "TOL"
                        mOTRCharges = mOTRCharges + (Val(CStr(mExpAmount)) * IIf(mExpAddDeduct = "D", -1, 1))
                        mExp = Val(CStr(mExpAmount))
                    Case "RO"
                        mRO = mRO + (Val(CStr(mExpAmount)) * IIf(mExpAddDeduct = "D", -1, 1))
                        mExp = Val(CStr(mExpAmount))
                End Select
                If xStr = "RO" Then
                    mTotExp = mTotExp + mExp
                Else
                    mTotExp = mTotExp + IIf(mExpAddDeduct = "D", -mExp, mExp)
                End If
                mExp = 0
DontCalc1:
                RsMisc.MoveNext()
            Loop
        End If
        xItemValue = CDbl(VB6.Format(mTotItemAmount, "#0.00"))
        xCGSTAmount = CDbl(VB6.Format(mCGSTAmount, "#0.00")) ' mCGSTAmount
        xSGSTAmount = CDbl(VB6.Format(mSGSTAmount, "#0.00")) ' mSGSTAmount
        xIGSTAmount = CDbl(VB6.Format(mIGSTAmount, "#0.00")) ' mIGSTAmount
        '    If pDnCnType = "R" Then
        xCGSTRefundAmount = CDbl(VB6.Format(mCGSTAmount, "#0.00"))
        xSGSTRefundAmount = CDbl(VB6.Format(mSGSTAmount, "#0.00"))
        xIGSTRefundAmount = CDbl(VB6.Format(mIGSTAmount, "#0.00"))
        '    End If
        xNETVALUE = CDbl(VB6.Format(System.Math.Abs(mTotExp + xCGSTAmount + xSGSTAmount + xIGSTAmount + mTotItemAmount), "#0.00"))
        xTOTFREIGHT = CDbl(VB6.Format(mOTRCharges, "#0.00"))
        xTOTCHARGES = 0 ''Format(mRO, "#0.00")
        xTOTEXPAMT = CDbl(VB6.Format(mTotExp, "#0.00"))
        xRO = CDbl(VB6.Format(mRO, "#0.00"))
        xTotDiscount = CDbl(VB6.Format(mTotDiscount, "#0.00"))
        xMSC = CDbl(VB6.Format(mMSC, "#0.00"))
        xTotQty = CDbl(VB6.Format(mTotQty, "#0.00"))
        Exit Sub
ERR1:
        ''Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Private Sub Clear1()
        pShowCalc = False
        LblMKey.Text = ""
        lblPMKey.Text = ""
        mSupplierCode = CStr(-1)
        lblPurchaseVNo.Text = ""

        '    mAuthSign = ""
        '    txtMRRNoPrefix.Text = RsCompany!Alias & vb6.Format(RsCompany!FYNO, "00")
        '    txtMRRNo.Text = ""
        '    txtMRRDate.Text = ""
        '    txtBillNoPrefix.Text = RsCompany!Alias & vb6.Format(RsCompany!FYNO, "00")
        '    txtBillNo.Text = ""
        '    txtBillNoSuffix.Text = IIf(LblBookCode.text = "-7", "E", "")
        '    txtBillDate.Text = Format(RunDate, "DD/MM/YYYY")
        '    TxtBillTm.Text = GetServerTime
        '    txtSupplier.Text = ""
        SSTab1.SelectedIndex = 0
        SSTabLevies.SelectedIndex = 0
        txtVNo.Text = ""
        txtVNoPrefix.Text = mBookType
        lblSaleBillNoSeq.Text = ""
        lblSaleBillNo.Text = ""
        lblSaleBillDate.Text = ""
        lblClaimStatus.Text = ""
        txtVNoSuffix.Text = ""
        If CDbl(LblBookCode.Text) = ConPurchaseBookCode Then
            If Not IsDate(txtVDate.Text) Then
                txtVDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
            End If
            '        chkCancelled.Enabled = True
        Else
            txtVDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
            '        chkCancelled.Enabled = False
        End If
        txtMRRNo.Text = ""
        txtMRRDate.Text = ""

        '4-07-2003 Commit on Mukesh Demand....
        cboInvType.SelectedIndex = -1
        txtBillNo.Text = ""
        txtBillDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtSupplier.Text = ""

        '    txtModvatSupp.Text = ""
        txtShippedTo.Text = ""
        txtShippedTo.Enabled = False
        chkShipTo.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkShipTo.Enabled = False
        txtDebitAccount.Text = ""
        txtCreditDays(0).Text = ""
        txtCreditDays(1).Text = ""
        txtPONo.Text = ""
        txtPODate.Text = ""
        chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkCancelled.Enabled = False
        ChkCapital.CheckState = System.Windows.Forms.CheckState.Unchecked
        cboGSTStatus.SelectedIndex = -1
        cboGSTStatus.Enabled = True
        txtTariff.Text = ""
        txtBENo.Text = ""
        txtBEDate.Text = ""
        txtBEAmount.Text = "0.00"
        txtPortCode.Text = ""
        txtRemarks.Text = ""
        txtNarration.Text = ""
        txtCarriers.Text = ""
        txtVehicle.Text = ""
        txtDocsThru.Text = ""
        txtMode.Text = ""
        OptFreight(0).Checked = True
        OptFreight(1).Checked = False
        lblGSTClaimNo.Text = ""
        lblGSTClaimDate.Text = ""
        txtModvatNo.Text = ""
        txtModvatDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        txtCReditNoteNo.Text = ""
        txtCustomerRefNo.Text = ""
        txtCustomerRefNo.Enabled = True
        chkGSTClaim.CheckState = System.Windows.Forms.CheckState.Unchecked
        txtModvatNo.Enabled = False
        txtModvatDate.Enabled = False
        chkGSTClaim.Enabled = False
        cboDivision.SelectedIndex = -1
        cboDivision.Enabled = False
        txtServNo.Text = ""
        txtServDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        '    chkRejection.Value = vbUnchecked
        chkRejection.Enabled = False
        lblTotItemValue.Text = VB6.Format(0, "#0.00")
        lblTotQty.Text = "0.00"
        lblTotCGSTAmount.Text = "0.00"
        lblTotSGSTAmount.Text = "0.00"
        lblTotIGSTAmount.Text = "0.00"
        lblTotOtherExp.Text = "0.00"
        txtTotCGSTRefund.Text = "0.00"
        txtTotSGSTRefund.Text = "0.00"
        txtTotIGSTRefund.Text = "0.00"
        lblNetAmount.Text = VB6.Format(0, "#0.00")
        lblTotFreight.Text = VB6.Format(0, "#0.00")
        lblTotCharges.Text = VB6.Format(0, "#0.00")
        lblTotExpAmt.Text = VB6.Format(0, "#0.00")
        lblTotTaxableAmt.Text = VB6.Format(0, "#0.00")
        lblRO.Text = VB6.Format(0, "#0.00")
        lblDiscount.Text = VB6.Format(0, "#0.00")
        lblSurcharge.Text = VB6.Format(0, "#0.00")
        lblMSC.Text = VB6.Format(0, "#0.00")
        txtPaymentdate.Text = ""
        chkCreditRC.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkFinalPost.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkFOC.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkFOC.Enabled = False
        txtPaymentdate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        chkTDS.CheckState = System.Windows.Forms.CheckState.Unchecked
        txtTDSRate.Text = "0.000"
        txtTDSAmount.Text = "0.00"
        chkTDS.Enabled = True
        chkESI.CheckState = System.Windows.Forms.CheckState.Unchecked
        txtESIRate.Text = "0.000"
        txtESIAmount.Text = "0.00"
        chkESI.Enabled = True
        ChkSTDS.CheckState = System.Windows.Forms.CheckState.Unchecked
        txtSTDSRate.Text = "0.000"
        txtSTDSAmount.Text = "0.00"
        ChkSTDS.Enabled = True
        txtJVVNO.Text = ""
        lblJVTMKey.Text = ""
        txtTDSDeductOn.Text = "0.00"
        txtESIDeductOn.Text = "0.00"
        txtSTDSDeductOn.Text = "0.00"
        txtSection.Text = ""

        ChkTDSRO.CheckState = System.Windows.Forms.CheckState.Checked
        ChkESIRO.CheckState = System.Windows.Forms.CheckState.Checked
        ChkSTDSRO.CheckState = System.Windows.Forms.CheckState.Checked
        FraServiceTax.Enabled = IIf(CDbl(LblBookCode.Text) = ConPurchaseBookCode, True, False)
        '    txtBalAmount.Text = "0.00"
        txtServProvided.Text = ""
        txtServiceOn.Text = ""
        txtProviderPer.Text = ""
        txtRecipientPer.Text = ""
        txtServiceTaxPer.Text = ""
        txtServiceTaxAmount.Text = ""
        txtAdvVNo.Text = ""
        txtAdvDate.Text = ""
        txtAdvBal.Text = ""
        txtItemAdvAdjust.Text = ""
        txtAdvAdjust.Text = ""
        txtAdvCGST.Text = ""
        txtAdvSGST.Text = ""
        txtAdvIGST.Text = ""
        cmdResetMRR.Enabled = False
        lblDiffAmt.Text = "0.00"

        txtShippedTo.Text = ""
        TxtShipTo.Text = ""
        txtDeliveryTo.Text = ""
        txtDeliveryToLoc.Text = ""

        'If lblPurchaseSeqType.Text = "2" Or lblPurchaseSeqType.Text = "8" Then
        fraAdvance.Visible = True
        fraPayment.Visible = True
        'Else
        '    fraAdvance.Visible = True
        '    fraPayment.Visible = False
        'End If

        SprdPaymentDetail.Enabled = IIf(IIf(IsDBNull(RsCompany.Fields("MANNUAL_BILL_ADJUST").Value), "N", RsCompany.Fields("MANNUAL_BILL_ADJUST").Value) = "Y", True, False)
        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)
        MainClass.ClearGrid(SprdExp)
        Call FillSprdExp()
        FraPostingDtl.Visible = False

        MainClass.ClearGrid(SprdPaymentDetail)
        Call FormatSprdPaymentDetail(-1, False)

        MainClass.ClearGrid(SprdPostingDetail)
        Call FormatSprdPostingDetail(-1)
        If lblPurchaseSeqType.Text = "9" Then
            txtBillNo.Enabled = True
            txtBillDate.Enabled = True
        End If
        txtBillTo.Text = ""
        TxtShipTo.Text = ""
        txtBillTo.Enabled = False
        TxtShipTo.Enabled = False
        mAddUser = ""
        mAddDate = ""
        mModUser = ""
        mModDate = ""
        pProcessKey = MainClass.AutoGenRowNo("TEMP_DSP_DESPATCH", "RowNo", PubDBCn)
        ChkCapital.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsPurchMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        pShowCalc = True
    End Sub
    Private Sub FillSprdExp()
        On Error GoTo ERR1
        Dim mLocal As String
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing
        Dim I As Integer
        Dim xSuppCode As String
        pShowCalc = False
        MainClass.ClearGrid(SprdExp)

        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xSuppCode = Trim(MasterNo)
        End If

        If Trim(txtSupplier.Text) <> "" Then
            mLocal = ""
            If Trim(txtSupplier.Text) <> "" Then
                mLocal = GetPartyBusinessDetail(Trim(xSuppCode), Trim(txtBillTo.Text), "WITHIN_STATE")
            End If
            'If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            '    mLocal = IIf(MasterNo = "Y", "L", "C")
            'Else
            '    mLocal = ""
            'End If
        Else
            mLocal = ""
        End If
        SqlStr = "Select * From FIN_INTERFACE_MST " & vbCrLf & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND (Type='P' OR Type='B') "
        If PubGSTApplicable = True Then
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='N'"
        End If
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

    Private Sub FrmPurchaseGST_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub
    Private Sub FrmPurchaseGST_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub FrmPurchaseGST_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub FrmPurchaseGST_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo ErrPart
        Dim x As Boolean
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        CurrFormHeight = 7245
        CurrFormWidth = 11355

        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        '    If InStr(1, XRIGHT, "D", vbTextCompare) > 1 Then
        '        chkCancelled.Enabled = True
        '    Else
        '        chkCancelled.Enabled = False
        '    End If

        mIsAuthorisedUser = False
        If InStr(1, XRIGHT, "S", CompareMethod.Text) > 0 Then
            mIsAuthorisedUser = True
        End If

        txtVNoPrefix.Text = mBookType
        SprdMain.Enabled = True
        SprdExp.Enabled = True
        txtVNo.Enabled = True
        txtModvatNo.Enabled = False
        '    txtStClaimNo.Enabled = False
        txtServNo.Enabled = False
        txtMRRNo.Enabled = True
        CmdSearchMRR.Enabled = True
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7755) '8000
        'Me.Width = VB6.TwipsToPixelsX(11355) '11900
        SSTab1.SelectedIndex = 0
        'AdoDCMain.Visible = False
        txtSupplier.Enabled = False
        txtBillDate.Enabled = False
        cboDivision.Items.Clear()
        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If
        cboDivision.SelectedIndex = -1
        ' Control displays text tips aligned to pointer with focus
        SprdMain.TextTip = FPSpreadADO.TextTipConstants.TextTipFloatingFocusOnly
        ' Control displays text tips after 250 milliseconds
        SprdMain.TextTipDelay = 250
        ' Text tip displays custom font and colors
        ' Background is yellow, RGB(255, 255, 0)
        ' Foreground is dark blue, RGB(0, 0, 128)
        x = SprdMain.SetTextTipAppearance("Arial", CShort("10"), False, False, &HFFFF, &H800000)
        'ResizeForm.FindAllControls(Me)
        FormActive = False

        Call FrmPurchaseGST_Activated(eventSender, eventArgs)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub OptFreight_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptFreight.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptFreight.GetIndex(eventSender)
            MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        End If
    End Sub
    Private Sub SprdExp_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdExp.Change
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
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
                    SqlStr = "Select * From FIN_INTERFACE_MST Where COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " AND Name= '" & m_Exp & "'"
                    If PubGSTApplicable = True Then
                        SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"
                    Else
                        SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='N'"
                    End If
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
            Case ColExpDebitAmt
                If eventArgs.newRow = -1 Then Exit Sub
                SprdExp.Row = ESRow
                SprdExp.Col = ColExpAmt
                p_Amt = Val(SprdExp.Text)
                SprdExp.Col = ColExpDebitAmt
                p_DebitAmt = Val(SprdExp.Text)
                If p_Amt < p_DebitAmt And p_DebitAmt <> 0 Then
                    MsgBox("Debit Amount Cann't be Greater Than Exp Amount.", MsgBoxStyle.Information)
                    Call MainClass.SetFocusToCell(SprdExp, ESRow, ColExpDebitAmt)
                    '                    Exit Sub
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
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol
        '    If KeyCode = vbKeyF1 And mCol = ColItemCode Then SprdMain_Click ColItemCode, 0
        '    If KeyCode = vbKeyF1 And mCol = ColItemDesc Then SprdMain_Click ColItemDesc, 0
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColHSN Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColHSN, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColInvType Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColInvType, 0))
        SprdMain.Refresh()
    End Sub
    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        '    With SprdMain
        '        SprdMain_LeaveCell .ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False
        '    End With
    End Sub
    Private Sub txtBillDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBillDate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtBillNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBillNo.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtBillNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBillNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCarriers_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCarriers.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtCarriers_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCarriers.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtCarriers.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtDebitAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDebitAccount.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtDebitAccount_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDebitAccount.DoubleClick
        On Error GoTo ErrPart
        If MainClass.SearchGridMaster((txtDebitAccount.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtDebitAccount.Text = AcName
            'txtMRRNo_Validate False
            txtDebitAccount.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtDebitAccount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDebitAccount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtDebitAccount.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtDebitAccount_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDebitAccount.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtDebitAccount_DoubleClick(txtDebitAccount, New System.EventArgs())
    End Sub
    Private Sub txtDebitAccount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDebitAccount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        If Trim(txtDebitAccount.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable((txtDebitAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            Cancel = True
            ErrorMsg("Please Enter the Valid Credit Account.", "", MsgBoxStyle.Critical)
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtCreditDays_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCreditDays.TextChanged
        Dim Index As Short = txtCreditDays.GetIndex(eventSender)
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtCreditDays_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCreditDays.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        Dim Index As Short = txtCreditDays.GetIndex(eventSender)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSupplier_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplier.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtSupplier_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSupplier.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtSupplier.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtMRRDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMRRDate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtMRRNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMRRNo.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtMRRNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMRRNo.DoubleClick
        CmdSearchMRR_Click(CmdSearchMRR, New System.EventArgs())
    End Sub
    Private Sub txtMRRNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMRRNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtMRRNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtMRRNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then CmdSearchMRR_Click(CmdSearchMRR, New System.EventArgs())
    End Sub
    Private Sub txtMRRNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMRRNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        If Trim(txtMRRNo.Text) = "" Then GoTo EventExitSub
        If txtMRRNo.Enabled = False Then GoTo EventExitSub
        If Len(txtMRRNo.Text) < 6 Then
            txtMRRNo.Text = Val(txtMRRNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If
        SqlStr = " SELECT * FROM FIN_PURCHASE_HDR " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & " AND CANCELLED='N'" '' AND ISFINALPOST='N'"
        If chkRejection.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " AND REJECTION='Y'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND REJECTION='N'"
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            If CDbl(LblBookCode.Text) = ConPurchaseBookCode Then
                If SendMrrToAccount() = False Then
                    MsgBox("MRR not Send By Store.", MsgBoxStyle.Critical)
                    Cancel = True
                    GoTo EventExitSub
                End If
            End If
            Clear1()
            If ShowFromExcise1(RsTemp) = False Then
                Cancel = True
                GoTo EventExitSub
            End If
        Else
            SqlStr = " SELECT * FROM INV_GATE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & "" ''& vbCrLf |                 & " And SUBSTR(AUTO_KEY_MRR, LENGTH(AUTO_KEY_MRR) - 5, 4) = " & RsCompany.fields("FYEAR").value & ""
            If lblPurchaseType.Text = "J" Or lblPurchaseType.Text = "R" Then
                SqlStr = SqlStr & vbCrLf & " AND REF_TYPE='R'"
            Else
                SqlStr = SqlStr & vbCrLf & " AND REF_TYPE<>'R'"
            End If
            If chkRejection.CheckState = System.Windows.Forms.CheckState.Checked Then
                SqlStr = SqlStr & vbCrLf & " AND REF_TYPE IN ('I','1','2','3')"
            Else
                SqlStr = SqlStr & vbCrLf & " AND REF_TYPE NOT IN ('I','1','2','3')"
            End If
            If CDbl(LblBookCode.Text) = ConModvatBookCode Then
                SqlStr = SqlStr & vbCrLf & " AND GST_STATUS='N'"
            ElseIf CDbl(LblBookCode.Text) = ConPurchaseBookCode Then
                SqlStr = SqlStr & vbCrLf & " AND SEND_AC_FLAG='Y'"
            End If
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                Clear1()
                '            If RsTemp.Fields("MRR_FINAL_FLAG").value = "Y" Then
                '                ErrorMsg "Please Enter Vaild MRR No.", "", vbCritical
                '                Cancel = True
                '            End If
                If ShowFromMRRMain(RsTemp) = False Then
                    Cancel = True
                    GoTo EventExitSub
                End If
            Else
                ErrorMsg("Either InValid MRR No. OR Not Send to Account.", "", MsgBoxStyle.Critical)
                Cancel = True
            End If
        End If
        '    If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "PORATEEDITABLE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
        '        If MasterNo = "Y" Then
        '            MainClass.UnProtectCell SprdMain, 1, SprdMain.MaxRows, ColPORate, ColPORate
        '            MainClass.ProtectCell SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColRejectedQty
        '            MainClass.ProtectCell SprdMain, 1, SprdMain.MaxRows, ColUnit, ColUnit
        '            MainClass.ProtectCell SprdMain, 1, SprdMain.MaxRows, ColAmount, ColAmount
        '        End If
        '    End If
        '    If ADDMode = True Then
        '        Call FillExpFromPartyExp
        '    End If
        FormatSprdMain(-1)
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtDocsThru_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDocsThru.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtDocsThru_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDocsThru.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtDocsThru.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtItemType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemType.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtItemType_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItemType.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtItemType.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtMode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMode.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtMode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtMode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtNarration_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNarration.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtNarration_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNarration.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtNarration.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtPODate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPODate.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtPONo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPONo.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtTariff_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTariff.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtTariff_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTariff.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtVehicle_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVehicle.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Function ShowFromMRRMain(ByRef mRsDC As ADODB.Recordset) As Boolean
        On Error GoTo ErrPart
        Dim mFormCode As Integer
        Dim mDivisionCode As Double
        Dim mDivisionDesc As String
        Dim mShipTo As String
        Dim mShipToCode As String
        Dim mGSTType As String
        Dim pServName As String = ""
        Dim mMrrRefType As String
        Dim mIsGSTReg As String = ""
        Dim mBalCGST As Double
        Dim mBalSGST As Double
        Dim mBalIGST As Double
        Dim pMRR As String
        Dim mDeliveryToCode As String
        Dim mDeliveryToName As String

        txtMRRNo.Text = IIf(IsDBNull(mRsDC.Fields("AUTO_KEY_MRR").Value), 0, mRsDC.Fields("AUTO_KEY_MRR").Value)
        If mRsDC.Fields("MRR_FINAL_FLAG").Value = "Y" Then
            MsgInformation("Account Entry Already made Against This MRR")
            ShowFromMRRMain = False
            Exit Function
        End If
        txtMRRDate.Text = IIf(IsDBNull(mRsDC.Fields("MRR_DATE").Value), "", mRsDC.Fields("MRR_DATE").Value)
        If MainClass.ValidateWithMasterTable((mRsDC.Fields("SUPP_CUST_CODE").Value), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtSupplier.Text = MasterNo
            mSupplierCode = Trim(mRsDC.Fields("SUPP_CUST_CODE").Value)
        End If
        '    txtModvatSupp.Text = Trim(txtSupplier.Text)
        txtBillNo.Text = IIf(IsDBNull(mRsDC.Fields("BILL_NO").Value), "", mRsDC.Fields("BILL_NO").Value)
        txtBillDate.Text = IIf(IsDBNull(mRsDC.Fields("BILL_DATE").Value), "", mRsDC.Fields("BILL_DATE").Value)
        txtVehicle.Text = IIf(IsDBNull(mRsDC.Fields("VEHICLE").Value), "", mRsDC.Fields("VEHICLE").Value)
        txtMode.Text = IIf(IsDBNull(mRsDC.Fields("TRANSPORT_MODE").Value), "", mRsDC.Fields("TRANSPORT_MODE").Value)

        txtBillTo.Text = IIf(IsDBNull(mRsDC.Fields("BILL_TO_LOC_ID").Value), "", mRsDC.Fields("BILL_TO_LOC_ID").Value)
        TxtShipTo.Text = IIf(IsDBNull(mRsDC.Fields("SHIP_TO_LOC_ID").Value), "", mRsDC.Fields("SHIP_TO_LOC_ID").Value)

        mMrrRefType = mRsDC.Fields("REF_TYPE").Value
        '    If mRsDC.Fields("REF_TYPE").Value = "P" Or mRsDC.Fields("REF_TYPE").Value = "I" Or mRsDC.Fields("REF_TYPE").Value = "2" Then
        '        txtPONo.Text = IIf(IsNull(mRsDC.Fields("REF_AUTO_KEY_NO").Value), "", mRsDC.Fields("REF_AUTO_KEY_NO").Value)
        '        If mRsDC.Fields("REF_TYPE").Value = "I" Or mRsDC.Fields("REF_TYPE").Value = "2" Then
        '            If Trim(txtPONo.Text) <> "" Then
        '                txtPONo.Text = "S" & vb6.Format(Mid(txtPONo.Text, 1, Len(txtPONo.Text) - 6), "00000")
        '            End If
        '        End If
        '
        '        txtPODate.Text = IIf(IsNull(mRsDC.Fields("REF_DATE").Value), "", mRsDC.Fields("REF_DATE").Value)
        '    Else
        '        txtPONo.Text = IIf(IsNull(mRsDC.Fields("PO_NO").Value), "", mRsDC.Fields("PO_NO").Value)
        '        txtPODate.Text = IIf(IsNull(mRsDC.Fields("PO_DATE").Value), "", mRsDC.Fields("PO_DATE").Value)
        '    End If
        txtRemarks.Text = IIf(IsDBNull(mRsDC.Fields("REMARKS").Value), "", mRsDC.Fields("REMARKS").Value)
        mDivisionCode = IIf(IsDBNull(mRsDC.Fields("DIV_CODE").Value), -1, mRsDC.Fields("DIV_CODE").Value)
        If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionDesc = Trim(MasterNo)
            cboDivision.Text = mDivisionDesc
        End If
        cboDivision.Enabled = False
        Call FillCreditDays(mSupplierCode)
        mShipTo = "N"
        mShipToCode = ""
        mGSTType = "E"
        If GetShipToFromPO(Val(txtMRRNo.Text), mMrrRefType, mGSTType, mShipTo, mShipToCode, pServName) = False Then GoTo ErrPart
        If mMrrRefType = "I" Or mMrrRefType = "1" Or mMrrRefType = "2" Or mMrrRefType = "3" Then
            mShipTo = "Y"
            mShipToCode = mSupplierCode
            cboGSTStatus.SelectedIndex = 0
        ElseIf mMrrRefType = "R" Then
            If MainClass.ValidateWithMasterTable((mRsDC.Fields("SUPP_CUST_CODE").Value), "SUPP_CUST_CODE", "GST_REGD", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mIsGSTReg = MasterNo
            End If
            cboGSTStatus.SelectedIndex = IIf(mIsGSTReg = "Y", 0, 1)
        Else
            If mGSTType = "G" Then
                cboGSTStatus.SelectedIndex = 0
            ElseIf mGSTType = "R" Then
                cboGSTStatus.SelectedIndex = 1
            ElseIf mGSTType = "E" Then
                cboGSTStatus.SelectedIndex = 2
            ElseIf mGSTType = "N" Then
                cboGSTStatus.SelectedIndex = 3
            ElseIf mGSTType = "C" Then
                cboGSTStatus.SelectedIndex = 5
            Else
                cboGSTStatus.SelectedIndex = 4
            End If
        End If
        '
        cboGSTStatus.Enabled = True
        chkShipTo.CheckState = IIf(mShipTo = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        txtServProvided.Text = pServName
        If mShipTo = "Y" Then
            mShipToCode = mSupplierCode
        End If
        If MainClass.ValidateWithMasterTable(mShipToCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtShippedTo.Text = MasterNo
        End If

        mDeliveryToCode = IIf(IsDBNull(mRsDC.Fields("DELIVERY_TO").Value), "", mRsDC.Fields("DELIVERY_TO").Value)
        mDeliveryToName = ""

        If mDeliveryToCode = "" Then
            txtDeliveryTo.Text = ""

            txtDeliveryToLoc.Text = ""
        Else
            If MainClass.ValidateWithMasterTable(mDeliveryToCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDeliveryToName = MasterNo
            End If
            ',
            txtDeliveryTo.Text = mDeliveryToName

            txtDeliveryToLoc.Text = IIf(IsDBNull(mRsDC.Fields("DELIVERY_TO_LOC_ID").Value), "", mRsDC.Fields("DELIVERY_TO_LOC_ID").Value)
        End If

        txtAdvBal.Text = CStr(GetBalancePaymentAmount(mSupplierCode, txtBillDate.Text, "", "", mDivisionCode, "AP", mBalCGST, mBalSGST, mBalIGST))
        '    txtAdvCGSTBal.Text = Format(mBalCGST, "0.00")
        '    txtAdvSGSTBal.Text = Format(mBalSGST, "0.00")
        '    txtAdvIGSTBal.Text = Format(mBalIGST, "0.00")
        If ShowFromMRRDetail((mRsDC.Fields("AUTO_KEY_MRR").Value), mSupplierCode, (mRsDC.Fields("REF_TYPE").Value)) = False Then GoTo ErrPart
        Call FillSprdExp()
        pMRR = mRsDC.Fields("AUTO_KEY_MRR").Value
        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
        Else
            Call ShowMRRExp1(pMRR)
        End If

        CalcTots()
        ShowFromMRRMain = True
        Exit Function
ErrPart:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ShowFromMRRMain = False
    End Function
    Private Function ResetMRRMain(ByRef mRsDC As ADODB.Recordset) As Boolean
        On Error GoTo ErrPart
        txtMRRDate.Text = IIf(IsDBNull(mRsDC.Fields("MRR_DATE").Value), "", mRsDC.Fields("MRR_DATE").Value)
        If MainClass.ValidateWithMasterTable((mRsDC.Fields("SUPP_CUST_CODE").Value), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtSupplier.Text = MasterNo
            mSupplierCode = Trim(mRsDC.Fields("SUPP_CUST_CODE").Value)
        End If
        txtBillNo.Text = IIf(IsDBNull(mRsDC.Fields("BILL_NO").Value), "", mRsDC.Fields("BILL_NO").Value)
        txtBillDate.Text = IIf(IsDBNull(mRsDC.Fields("BILL_DATE").Value), "", mRsDC.Fields("BILL_DATE").Value)

        txtBillTo.Text = IIf(IsDBNull(mRsDC.Fields("BILL_TO_LOC_ID").Value), "", mRsDC.Fields("BILL_TO_LOC_ID").Value)
        TxtShipTo.Text = IIf(IsDBNull(mRsDC.Fields("SHIP_TO_LOC_ID").Value), "", mRsDC.Fields("SHIP_TO_LOC_ID").Value)


        txtMode.Text = IIf(IsDBNull(mRsDC.Fields("TRANSPORT_MODE").Value), "", mRsDC.Fields("TRANSPORT_MODE").Value)
        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)
        MainClass.ClearGrid(SprdExp)
        If ShowFromMRRDetail((mRsDC.Fields("AUTO_KEY_MRR").Value), mSupplierCode, (mRsDC.Fields("REF_TYPE").Value)) = False Then GoTo ErrPart
        Call FillSprdExp()
        CalcTots()
        ResetMRRMain = True
        Exit Function
ErrPart:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ResetMRRMain = False
    End Function
    Private Function ShowFromMRRDetail(ByRef mDCNo As String, ByRef pCustomerCode As String, ByRef xRefType As String) As Boolean
        On Error GoTo ErrPart
        Dim RsDc As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim cntRow As Integer
        Dim mItemCode As String
        Dim mRGPItemCode As String
        Dim mRate As Double
        Dim mQty As Double
        Dim mTariff As String = ""
        Dim mTariffDesc As String = ""
        Dim RejectQty As Double
        Dim ReOfferQty As Double
        Dim mPONo As Double
        Dim mExchangeRate As Double
        Dim mRateExp As Double
        Dim mWorkOrderNo As Double
        Dim mOurAutoSaleKey As String
        Dim mOurSaleInvoiceNo As String
        Dim mOurSaleInvoiceDate As String
        Dim pCGSTPer As Double
        Dim pSGSTPer As Double
        Dim pIGSTPer As Double
        Dim mHSNCode As String
        Dim mLocal As String
        Dim mInvTypeCode As Double
        Dim mPartyGSTNo As String
        Dim xSuppCode As String

        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            xSuppCode = Trim(MasterNo)
        End If

        mLocal = "N"
        If Trim(pCustomerCode) <> "" Then
            'If MainClass.ValidateWithMasterTable(pCustomerCode, "SUPP_CUST_CODE", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            '    mLocal = Trim(MasterNo)
            'End If
            If Trim(txtSupplier.Text) <> "" Then
                mLocal = GetPartyBusinessDetail(Trim(xSuppCode), Trim(txtBillTo.Text), "WITHIN_STATE")
            End If
        End If
        mPartyGSTNo = ""
        If MainClass.ValidateWithMasterTable(pCustomerCode, "SUPP_CUST_CODE", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPartyGSTNo = MasterNo
        End If
        SqlStr = "SELECT INV_GATE_DET.*, "

        SqlStr = SqlStr & vbCrLf & "GETREOFFERQTY(" & RsCompany.Fields("COMPANY_CODE").Value & ",AUTO_KEY_MRR,MRR_DATE,SUPP_CUST_CODE,ITEM_CODE) AS REOFFER , "

        If xRefType = "I" Or xRefType = "2" Then
            SqlStr = SqlStr & vbCrLf & " GetSALEITEMPRICE(REF_PO_NO,'','" & pCustomerCode & "',ITEM_CODE) AS PORATE, "
            SqlStr = SqlStr & " 0 AS VOL_DISCRATE "
        ElseIf xRefType = "P" Then
            SqlStr = SqlStr & vbCrLf & " GetITEMPRICE_NEW(1,1,TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),REF_PO_NO,ITEM_CODE) AS PORATE, "
            SqlStr = SqlStr & " GetVOL_DISC_ITEM(1,1,TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),REF_PO_NO, ITEM_CODE) AS VOL_DISCRATE "
        ElseIf xRefType = "R" Then
            SqlStr = SqlStr & vbCrLf & " GetITEMJWRate(" & RsCompany.Fields("COMPANY_CODE").Value & ",1,TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),REF_PO_NO,AUTO_KEY_MRR,ITEM_CODE,SERIAL_NO) AS PORATE, "
            SqlStr = SqlStr & " 0 AS VOL_DISCRATE "
        Else
            SqlStr = SqlStr & vbCrLf & " 0 AS PORATE, "
            SqlStr = SqlStr & " 0 AS VOL_DISCRATE "
        End If

        SqlStr = SqlStr & vbCrLf & " FROM INV_GATE_DET " & vbCrLf & " WHERE AUTO_KEY_MRR=" & Val(mDCNo) & "" & vbCrLf & " ORDER BY SERIAL_NO "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDc, ADODB.LockTypeEnum.adLockReadOnly)

        With SprdMain
            cntRow = 1
            If RsDc.EOF = False Then
                Do While Not RsDc.EOF
                    .Row = cntRow
                    .Col = ColItemCode
                    .Text = IIf(IsDBNull(RsDc.Fields("ITEM_CODE").Value), "", RsDc.Fields("ITEM_CODE").Value)
                    mItemCode = IIf(IsDBNull(RsDc.Fields("ITEM_CODE").Value), "", RsDc.Fields("ITEM_CODE").Value)
                    mRGPItemCode = IIf(IsDBNull(RsDc.Fields("RGP_ITEM_CODE").Value), "", RsDc.Fields("RGP_ITEM_CODE").Value)
                    .Col = ColItemDesc
                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        .Text = MasterNo
                    End If
                    .Col = ColPartNo
                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "CUSTOMER_PART_NO", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        .Text = MasterNo
                    End If
                    .Col = ColAcceptedQty
                    .Text = CStr(Val(IIf(IsDBNull(RsDc.Fields("RECEIVED_QTY").Value), "", RsDc.Fields("RECEIVED_QTY").Value)))
                    .Col = ColShortageQty
                    .Text = CStr(Val(IIf(IsDBNull(RsDc.Fields("SHORTAGE_QTY").Value), "", RsDc.Fields("SHORTAGE_QTY").Value)))
                    .Col = ColRejectedQty
                    ReOfferQty = IIf(IsDBNull(RsDc.Fields("REOFFER").Value), "", RsDc.Fields("REOFFER").Value)
                    RejectQty = IIf(IsDBNull(RsDc.Fields("REJECTED_QTY").Value), "", RsDc.Fields("REJECTED_QTY").Value)
                    .Text = CStr(Val(CStr(RejectQty))) ' Val(RejectQty - ReOfferQty)
                    If xRefType = "P" Then
                        mPONo = IIf(IsDBNull(RsDc.Fields("REF_PO_NO").Value), "", RsDc.Fields("REF_PO_NO").Value)
                        mExchangeRate = GetExchangeRate(mPONo)
                    Else
                        mExchangeRate = 1
                    End If
                    mRateExp = 0

                    .Col = ColPORate
                    .Text = Val(IIf(IsDBNull(RsDc.Fields("PORATE").Value), "", RsDc.Fields("PORATE").Value)) + mRateExp ''* mExchangeRate

                    .Col = ColVolDiscRate
                    .Text = CStr(Val(IIf(IsDBNull(RsDc.Fields("VOL_DISCRATE").Value), "", RsDc.Fields("VOL_DISCRATE").Value)))

                    .Col = ColUnit
                    .Text = IIf(IsDBNull(RsDc.Fields("ITEM_UOM").Value), "", RsDc.Fields("ITEM_UOM").Value)

                    .Col = ColQty
                    mQty = IIf(IsDBNull(RsDc.Fields("BILL_QTY").Value), "", RsDc.Fields("BILL_QTY").Value)

                    .Text = CStr(mQty)
                    .Col = ColRate
                    mRate = IIf(IsDBNull(RsDc.Fields("ITEM_RATE").Value), "", RsDc.Fields("ITEM_RATE").Value) ''* mExchangeRate
                    .Text = CStr(mRate)

                    .Col = ColAmount
                    .Text = VB6.Format(mQty * mRate, "0.00")




                    If lblPurchaseType.Text = "J" Or lblPurchaseType.Text = "R" Then
                        mHSNCode = GetSACCode((txtServProvided.Text))
                        If GetSACDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, mPartyGSTNo, VB.Left(cboGSTStatus.Text, 1)) = False Then GoTo ErrPart
                    Else
                        mHSNCode = ""
                        If xRefType = "P" Then
                            .Col = ColPONo
                            mOurAutoSaleKey = IIf(IsDBNull(RsDc.Fields("REF_PO_NO").Value), -1, RsDc.Fields("REF_PO_NO").Value)
                            mHSNCode = GetHSNFromPurchaseOrder(mItemCode, mOurAutoSaleKey)
                        End If
                        If mHSNCode = "" Then
                            mHSNCode = GetHSNCode(mItemCode)
                        End If
                        If GetHSNDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, VB.Left(cboGSTStatus.Text, 1), mPartyGSTNo) = False Then GoTo ErrPart
                    End If

                    SprdMain.Col = ColHSN
                    SprdMain.Text = mHSNCode
                    SprdMain.Col = ColCGSTPer
                    SprdMain.Text = VB6.Format(pCGSTPer, "0.00")
                    SprdMain.Col = ColSGSTPer
                    SprdMain.Text = VB6.Format(pSGSTPer, "0.00")
                    SprdMain.Col = ColIGSTPer
                    SprdMain.Text = VB6.Format(pIGSTPer, "0.00")
                    .Col = ColPONo
                    .Text = CStr(IIf(IsDBNull(RsDc.Fields("REF_PO_NO").Value), "", RsDc.Fields("REF_PO_NO").Value))
                    mOurAutoSaleKey = IIf(IsDBNull(RsDc.Fields("REF_PO_NO").Value), -1, RsDc.Fields("REF_PO_NO").Value)
                    If xRefType = "I" Or xRefType = "1" Or xRefType = "2" Or xRefType = "3" Then
                        '                    If Trim(.Text) <> "" Then
                        '                        .Text = "S" & vb6.Format(Mid(.Text, 1, Len(.Text) - 6), "00000")
                        '                    End If
                        mOurSaleInvoiceNo = ""
                        If MainClass.ValidateWithMasterTable(mOurAutoSaleKey, "AUTO_KEY_INVOICE", "BILLNO", "FIN_INVOICE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mOurSaleInvoiceNo = MasterNo
                            .Text = mOurSaleInvoiceNo
                        End If

                    ElseIf xRefType = "R" Then
                        mWorkOrderNo = -1
                        If MainClass.ValidateWithMasterTable(mOurAutoSaleKey, "AUTO_KEY_PASSNO", "AUTO_KEY_WO", "INV_GATEPASS_DET", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mRGPItemCode) & "'") = True Then
                            mWorkOrderNo = MasterNo
                        End If
                    End If
                    If xRefType = "P" Then
                        mInvTypeCode = GetPOInvType(Val(mOurAutoSaleKey), mItemCode)
                        SprdMain.Col = ColInvType
                        If mInvTypeCode = -1 Then
                            SprdMain.Text = ""  ''Trim(cboInvType.Text)
                        Else
                            If MainClass.ValidateWithMasterTable(mInvTypeCode, "CODE", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                SprdMain.Text = MasterNo
                                If cboInvType.SelectedIndex = -1 Then cboInvType.Text = SprdMain.Text
                            Else
                                SprdMain.Text = Trim(cboInvType.Text)
                            End If
                        End If
                    ElseIf xRefType = "R" Then
                        mInvTypeCode = GetPOInvType(mWorkOrderNo, mItemCode)
                        SprdMain.Col = ColInvType
                        If mInvTypeCode = -1 Then
                            SprdMain.Text = Trim(cboInvType.Text)
                        Else
                            If MainClass.ValidateWithMasterTable(mInvTypeCode, "CODE", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                SprdMain.Text = MasterNo
                                If cboInvType.SelectedIndex = -1 Then cboInvType.Text = SprdMain.Text
                            Else
                                SprdMain.Text = Trim(cboInvType.Text)
                            End If
                        End If
                    End If
                    '                txtPODate.Text = IIf(IsNull(mRsDC.Fields("REF_DATE").Value), "", mRsDC.Fields("REF_DATE").Value)
                    If Trim(txtTariff.Text) = "" Then
                        If GetTariffHeading(mItemCode, mTariff, mTariffDesc) = True Then
                            txtTariff.Text = mTariff
                            txtItemType.Text = mTariffDesc
                        End If
                    End If
                    RsDc.MoveNext()
                    cntRow = cntRow + 1
                    .MaxRows = .MaxRows + 1
                Loop
            End If
        End With
        FormatSprdMain(-1)
        ShowFromMRRDetail = True
        Exit Function
ErrPart:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ShowFromMRRDetail = False
    End Function

    Private Sub FillCboSaleType()
        On Error GoTo ErrPart
        Dim RsSaleType As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        cboInvType.Items.Clear()
        'mm.lblPurchaseType.text="J"
        SqlStr = "SELECT NAME FROM FIN_INVTYPE_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND CATEGORY='P' "

        If lblPurchaseType.Text = "J" Then
            SqlStr = SqlStr & vbCrLf & " AND IDENTIFICATION ='J'"
        ElseIf lblPurchaseType.Text = "R" Then
            SqlStr = SqlStr & vbCrLf & " AND IDENTIFICATION ='W'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND IDENTIFICATION NOT IN ('W','J')"
        End If
        SqlStr = SqlStr & vbCrLf & " ORDER BY NAME "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSaleType, ADODB.LockTypeEnum.adLockReadOnly)
        If RsSaleType.EOF = False Then
            Do While Not RsSaleType.EOF
                cboInvType.Items.Add(RsSaleType.Fields("NAME").Value)
                RsSaleType.MoveNext()
            Loop
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtVehicle_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVehicle.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtVehicle.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtVNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVNo.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtVNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub FillCreditDays(ByRef mSupplierCode As String)
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mPayDate As String
        Dim mPayDay As Integer
        Dim mPayDay2 As Integer
        If Trim(txtPONo.Text) = "" Or Val(txtPONo.Text) = 0 Then
            SqlStr = "SELECT FROM_DAYS, TO_DAYS FROM " & vbCrLf & " FIN_SUPP_CUST_HDR A, FIN_PAYTERM_MST B" & vbCrLf & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf & " AND A.PAYMENT_CODE=B.PAY_TERM_CODE AND SUPP_CUST_CODE='" & mSupplierCode & "'"
        Else
            SqlStr = "SELECT FROM_DAYS, TO_DAYS FROM " & vbCrLf & " PUR_PURCHASE_HDR A, FIN_PAYTERM_MST B" & vbCrLf & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf & " AND A.PAYMENT_CODE=B.PAY_TERM_CODE AND AUTO_KEY_PO='" & txtPONo.Text & "'"
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)
        If RsTemp.EOF = False Then
            txtCreditDays(0).Text = IIf(IsDBNull(RsTemp.Fields("FROM_DAYS").Value), 0, RsTemp.Fields("FROM_DAYS").Value)
            txtCreditDays(1).Text = IIf(IsDBNull(RsTemp.Fields("TO_DAYS").Value), 0, RsTemp.Fields("TO_DAYS").Value)
        Else
            SqlStr = "SELECT FROM_DAYS, TO_DAYS FROM " & vbCrLf & " FIN_SUPP_CUST_HDR A, FIN_PAYTERM_MST B" & vbCrLf & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf & " AND A.PAYMENT_CODE=B.PAY_TERM_CODE AND SUPP_CUST_CODE='" & mSupplierCode & "'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)
            If RsTemp.EOF = False Then
                txtCreditDays(0).Text = IIf(IsDBNull(RsTemp.Fields("FROM_DAYS").Value), 0, RsTemp.Fields("FROM_DAYS").Value)
                txtCreditDays(1).Text = IIf(IsDBNull(RsTemp.Fields("TO_DAYS").Value), 0, RsTemp.Fields("TO_DAYS").Value)
            End If
        End If
        ''Temp.. Comment.. (paydate from po terms....)
        '    If MainClass.ValidateWithMasterTable(mSupplierCode, "SUPP_CUST_CODE", "PAIDDAY", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mPayDay = Val(IIf(IsNull(MasterNo), 0, MasterNo))
        '    Else
        '        mPayDay = 0
        '    End If
        '    If mPayDay = 0 Then
        txtPaymentdate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, Val(txtCreditDays(0).Text), CDate(txtBillDate.Text)))
        '    Else
        '        mPayDate = DateAdd("D", Val(txtCreditDays(0).Text), CDate(txtBillDate.Text))
        '        If mPayDay >= Day(mPayDate) Then
        '            txtPaymentdate.Text = Format(mPayDay, "00") & "/" & vb6.Format(Month(mPayDate), "00") & "/" & vb6.Format(Year(mPayDate), "0000")
        '        Else
        '            If Val(txtCreditDays(0).Text) = Val(txtCreditDays(1).Text) Then
        '                mPayDate = DateAdd("M", 1, mPayDate)
        '                txtPaymentdate.Text = Format(mPayDay, "00") & "/" & vb6.Format(Month(mPayDate), "00") & "/" & vb6.Format(Year(mPayDate), "0000")
        '            Else
        '                mPayDate = Format(mPayDay, "00") & "/" & vb6.Format(Month(mPayDate), "00") & "/" & vb6.Format(Year(mPayDate), "0000")
        '                mPayDate = DateAdd("D", Val(txtCreditDays(1).Text) - Val(txtCreditDays(0).Text), mPayDate)
        '                    txtPaymentdate.Text = mPayDate      ''Format(mPayDay2, "00") & "/" & vb6.Format(Month(mPayDate), "00") & "/" & vb6.Format(Year(mPayDate), "0000")
        '            End If
        '        End If
        '    End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtItemType_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemType.DoubleClick
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster((txtItemType.Text), "FIN_ITEMTYPE_MST", "NAME", , , , SqlStr) = True Then
            txtItemType.Text = AcName
            If txtItemType.Enabled = True Then txtItemType.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtItemType_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtItemType.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtItemType_DoubleClick(txtItemType, New System.EventArgs())
    End Sub
    Private Sub txtVehicle_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVehicle.DoubleClick
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster((txtVehicle.Text), "FIN_Vehicle_MST", "NAME", , , , SqlStr) = True Then
            txtVehicle.Text = AcName
            If txtVehicle.Enabled = True Then txtVehicle.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtVehicle_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtVehicle.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtVehicle_DoubleClick(txtVehicle, New System.EventArgs())
    End Sub
    Private Sub InsertTempBill(ByRef mAccountCode As String, ByRef mAmount As Double, ByRef mRemarks As String)
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        '    PubDBCn.BeginTrans
        SqlStr = "Insert Into FIN_TEMPBILL_TRN  ( " & vbCrLf & " UserId, TRNDTLSUBROWNO, SUBROWNO, " & vbCrLf & " ACCOUNTCODE, BILLNO, BILLDATE, " & vbCrLf & " BILLAMOUNT, BILLDC, TRNTYPE, " & vbCrLf & " Amount, DC, BOOKTYPE, REMARKS,  " & vbCrLf & " OldAmount, OldDC, OldBillNo, OldPayType,DUEDATE,TEMPMKEY ) VALUES (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "' , 1, 1, " & vbCrLf & " '" & mAccountCode & "','" & MainClass.AllowSingleQuote(txtBillNo.Text) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(txtBillDate.Text, "DD/MMM/YYYY") & "')," & vbCrLf & " " & Val(lblNetAmount.Text) & ", 'C', 'B', " & vbCrLf & " " & mAmount & ", 'D', '" & ConJournal & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mRemarks) & "', '','','',''," & vbCrLf & " TO_DATE('" & VB6.Format(txtPaymentdate.Text, "DD/MMM/YYYY") & "')," & pProcessKey & ")"
        PubDBCn.Execute(SqlStr)
        '    PubDBCn.CommitTrans
        Exit Sub
ErrPart:
        '    PubDBCn.RollbackTrans
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchTariff()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster((txtTariff.Text), "FIN_TARRIF_MST", "TARRIF_CODE", "TARRIF_DESC", , , SqlStr) = True Then
            txtTariff.Text = AcName
            txtItemType.Text = AcName1
            '        txtTariff_Validate False
            If txtTariff.Enabled = True Then txtTariff.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function GetPDIRItem(ByRef xMRRNo As Double) As Integer
        On Error GoTo ErrPart1
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        mSqlStr = "SELECT COUNT(1) AS CNTPDIR FROM INV_GATE_DET " & vbCrLf & " WHERE AUTO_KEY_MRR=" & xMRRNo & " AND PDIR_FLAG='N'"
        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetPDIRItem = IIf(IsDBNull(RsTemp.Fields("CNTPDIR").Value), 0, RsTemp.Fields("CNTPDIR").Value)
        End If
        Exit Function
ErrPart1:
        GetPDIRItem = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function GetShipToFromPO(ByRef xMRRNo As Double, ByRef mMrrRefType As String, ByRef mGSTType As String, ByRef mShipTo As String, ByRef mShipToCode As String, ByRef pServName As String) As Boolean
        On Error GoTo ErrPart1
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim pSACCode As String
        If mMrrRefType = "P" Then
            pServName = ""
            '        mSqlStr = "SELECT ISGSTAPPLICABLE,SHIPPED_TO_SAMEPARTY, SHIPPED_TO_PARTY_CODE, SERVICE_CODE " & vbCrLf _
            ''                & " FROM INV_GATE_DET ID, PUR_PURCHASE_HDR PH " & vbCrLf _
            ''                & " WHERE ID.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
            ''                & " AND ID.AUTO_KEY_MRR=" & xMRRNo & " " & vbCrLf _
            ''                & " AND ID.COMPANY_CODE=PH.COMPANY_CODE" & vbCrLf _
            ''                & " AND ID.REF_PO_NO=PH.AUTO_KEY_PO " & vbCrLf _
            ''                & " AND PH.PO_STATUS='Y' AND ID.REF_TYPE='P'" & vbCrLf _
            ''                & " AND MKEY IN ( " & vbCrLf _
            ''                & " SELECT MAX(MKEY) FROM PUR_PURCHASE_HDR PO " & vbCrLf _
            ''                & " WHERE PO.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
            ''                & " AND PO.AUTO_KEY_PO=PH.AUTO_KEY_PO AND PO.PO_STATUS='Y'" & vbCrLf _
            ''                & " AND PO.AMEND_WEF_DATE<='" & vb6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "')"
            mSqlStr = "SELECT ISGSTAPPLICABLE,SHIPPED_TO_SAMEPARTY, SHIPPED_TO_PARTY_CODE " & vbCrLf _
                & " FROM INV_GATE_DET ID, PUR_PURCHASE_HDR PH " & vbCrLf _
                & " WHERE ID.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND ID.AUTO_KEY_MRR=" & xMRRNo & " " & vbCrLf _
                & " AND ID.COMPANY_CODE=PH.COMPANY_CODE" & vbCrLf _
                & " AND ID.REF_PO_NO=PH.AUTO_KEY_PO " & vbCrLf _
                & " AND PH.PO_STATUS='Y' AND ID.REF_TYPE='P'" & vbCrLf _
                & " AND MKEY IN ( " & vbCrLf & " SELECT MAX(MKEY) FROM INV_GATE_DET PD, PUR_PURCHASE_HDR PO " & vbCrLf _
                & " WHERE PO.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND PD.AUTO_KEY_MRR=" & xMRRNo & " " & vbCrLf _
                & " AND PD.COMPANY_CODE=PO.COMPANY_CODE" & vbCrLf _
                & " AND PD.REF_PO_NO=PO.AUTO_KEY_PO " & vbCrLf _
                & " AND PO.PO_STATUS='Y'" & vbCrLf _
                & " AND PO.AMEND_WEF_DATE<=To_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

            MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                mGSTType = IIf(IsDBNull(RsTemp.Fields("ISGSTAPPLICABLE").Value), "E", RsTemp.Fields("ISGSTAPPLICABLE").Value)
                mShipTo = IIf(IsDBNull(RsTemp.Fields("SHIPPED_TO_SAMEPARTY").Value), "N", RsTemp.Fields("SHIPPED_TO_SAMEPARTY").Value)
                mShipToCode = IIf(IsDBNull(RsTemp.Fields("SHIPPED_TO_PARTY_CODE").Value), "", RsTemp.Fields("SHIPPED_TO_PARTY_CODE").Value)
                '            pServCode = IIf(IsNull(RsTemp!SERVICE_CODE), "", RsTemp!SERVICE_CODE)
                '
                '            If pServCode <> "" Then
                '                If MainClass.ValidateWithMasterTable(pServCode, "CODE", "NAME", "FIN_SERVPROV_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '                    pServName = Trim(MasterNo)
                '                End If
                '            End If
            End If
        ElseIf mMrrRefType = "R" Then
            pServName = ""
            mSqlStr = "SELECT GST_APP AS ISGSTAPPLICABLE,'Y' AS SHIPPED_TO_SAMEPARTY, PH.SUPP_CUST_CODE AS SHIPPED_TO_PARTY_CODE, SAC_CODE " & vbCrLf _
                & " FROM INV_GATE_DET ID, INV_GATEPASS_HDR PH, INV_GATEPASS_DET PD " & vbCrLf _
                & " WHERE ID.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND ID.AUTO_KEY_MRR=" & xMRRNo & " " & vbCrLf _
                & " AND ID.COMPANY_CODE=PD.COMPANY_CODE" & vbCrLf & " AND ID.REF_PO_NO=PD.AUTO_KEY_PASSNO " & vbCrLf _
                & " AND ID.RGP_ITEM_CODE=PD.ITEM_CODE AND PH.AUTO_KEY_PASSNO=PD.AUTO_KEY_PASSNO" & vbCrLf _
                & " AND PD.AUTO_KEY_WO IN ( " & vbCrLf _
                & " SELECT MAX(AUTO_KEY_PO) FROM PUR_PURCHASE_HDR PO " & vbCrLf _
                & " WHERE PO.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND PO.AUTO_KEY_PO=PD.AUTO_KEY_WO AND PO.PO_STATUS='Y'" & vbCrLf _
                & " AND PO.AMEND_WEF_DATE<=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
            MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                mGSTType = IIf(IsDBNull(RsTemp.Fields("ISGSTAPPLICABLE").Value), "E", RsTemp.Fields("ISGSTAPPLICABLE").Value)
                mShipTo = IIf(IsDBNull(RsTemp.Fields("SHIPPED_TO_SAMEPARTY").Value), "N", RsTemp.Fields("SHIPPED_TO_SAMEPARTY").Value)
                mShipToCode = IIf(IsDBNull(RsTemp.Fields("SHIPPED_TO_PARTY_CODE").Value), "", RsTemp.Fields("SHIPPED_TO_PARTY_CODE").Value)
                pSACCode = IIf(IsDBNull(RsTemp.Fields("SAC_CODE").Value), "", RsTemp.Fields("SAC_CODE").Value)
                If pSACCode <> "" Then
                    If MainClass.ValidateWithMasterTable(pSACCode, "HSN_CODE", "HSN_DESC", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'") = True Then
                        pServName = Trim(MasterNo)
                    End If
                End If
            Else
                mShipTo = "Y"
            End If
        End If
        GetShipToFromPO = True
        Exit Function
ErrPart1:
        '    Resume
        GetShipToFromPO = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub ReportonPO(ByRef Mode As Crystal.DestinationConstants, ByRef mPONo As String)
        On Error GoTo ERR1
        Dim mTitle As String
        Dim mSubTitle As String
        Dim SqlStr As String = ""
        Dim mRptFileName As String
        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)
        SqlStr = ""
        mSubTitle = ""
        Call MainClass.ClearCRptFormulas(Report1)
        '    Call InsertForPO(mPONo)
        SqlStr = ""
        Call SelectQryForPO(SqlStr, mPONo)
        '    SqlStr = FetchRecordForReport(SqlStr)
        mTitle = "PURCHASE ORDER"
        mRptFileName = "PO_View.rpt" ''mRptFileName = "PO.rpt"
        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName, "Y")
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Function SelectQryForPO(ByRef mSqlStr As String, ByRef pPONO As String) As String
        On Error GoTo ErrPart
        Dim mSuppCode As String

        ''SELECT CLAUSE...
        mSqlStr = " SELECT " & vbCrLf & " IH.*, ID.*,"
        mSqlStr = mSqlStr & " CMST.SUPP_CUST_NAME, CMST.SUPP_CUST_ADDR, " & vbCrLf & " CMST.SUPP_CUST_CITY, CMST.SUPP_CUST_STATE, " & vbCrLf & " CMST.SUPP_CUST_PIN, CMST.SUPP_CUST_PHONE, " & vbCrLf & " CMST.SUPP_CUST_FAXNO, CMST.SUPP_CUST_MAILID, " & vbCrLf & " CMST.SUPP_CUST_MOBILE, CMST.CST_NO, " & vbCrLf & " CMST.LST_NO, CMST.PAN_NO, " & vbCrLf & " CMST.EXCISE_DIV, CMST.EXCISE_RANGE, " & vbCrLf & " CMST.CENT_EXC_RGN_NO, CMST.ECC_NO, " & vbCrLf & " CMST.SUPP_CUST_REMARKS, CMST.WITHIN_STATE, " & vbCrLf & " CMST.WITHIN_DISTT, CMST.COMMISIONER_RATE, " & vbCrLf & " CMST.REGD_DEALER, CMST.DATE_OF_APPROVAL, WITHIN_STATE"
        ''FROM CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID, " & vbCrLf & " FIN_SUPP_CUST_MST CMST, FIN_PAYTERM_MST PAYMST, INV_ITEM_MST IMST"
        ''WHERE CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND IH.COMPANY_CODE=PAYMST.COMPANY_CODE(+)" & vbCrLf & " AND IH.PAYMENT_CODE=PAYMST.PAY_TERM_CODE(+)" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        mSqlStr = mSqlStr & vbCrLf & " AND IMST.COMPANY_CODE(+)=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND ID.ITEM_CODE=IMST.ITEM_CODE(+) AND PO_STATUS='Y'"
        If pPONO = "" Then
            If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSuppCode = MasterNo
            Else
                mSuppCode = "-1"
            End If
            mSqlStr = mSqlStr & vbCrLf & " AND IH.SUPP_CUST_CODE='" & mSuppCode & "'"
            If RsCompany.Fields("FYEAR").Value < ConOPENPO_CONTINOUS_YEAR Then
                mSqlStr = mSqlStr & vbCrLf & " AND SUBSTR(IH.AUTO_KEY_PO,LENGTH(IH.AUTO_KEY_PO)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
            Else
                mSqlStr = mSqlStr & vbCrLf & " AND SUBSTR(IH.AUTO_KEY_PO,LENGTH(IH.AUTO_KEY_PO)-5,4)>=" & ConOPENPO_CONTINOUS_YEAR & ""
            End If
        Else
            mSqlStr = mSqlStr & vbCrLf & " AND IH.AUTO_KEY_PO=" & Val(pPONO) & ""
        End If
        ''ORDER CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & "ORDER BY IH.MKEY, ID.SERIAL_NO"
        SelectQryForPO = mSqlStr
        Exit Function
        SelectQryForPO = ""
ErrPart:
    End Function
    Private Sub InsertForPO(ByRef mPONo As String)
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mSqlStr As String
        Dim mSuppCode As String
        Dim mRefType As String = ""
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        If MainClass.ValidateWithMasterTable((txtMRRNo.Text), "AUTO_KEY_MRR", "REF_TYPE", "INV_GATE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mRefType = MasterNo
        End If
        If mRefType <> "P" Then
            mPONo = ""
        End If
        SqlStr = "DELETE FROM Temp_PO NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)
        SqlStr = ""
        mSqlStr = ""
        ''SELECT CLAUSE...
        mSqlStr = " SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " IH.AUTO_KEY_PO, IH.COMPANY_CODE, " & vbCrLf & " IH.PUR_TYPE, IH.ORDER_TYPE, " & vbCrLf & " IH.PUR_ORD_DATE, IH.SUPP_CUST_CODE, " & vbCrLf & " IH.AMEND_NO, IH.AMEND_DATE, " & vbCrLf & " IH.REMARKS, WO_DESCRIPTION," & vbCrLf & " 'DELIVERY : ' || IH.DELIVERY || ' EXCISE : ' || IH.EXCISE_OTHERS || ' PAYMENT : ' || IH.PAYMENT_CODE || ' DESPATCH MODE : ' || IH.MODE_DESPATCH || ' INSPECTION : ' || IH.INSPECTION || ' PACKING & FORWARDING : ' || IH.PACKING_FORWARDING || ' INSURANCE : ' || IH.INSURANCE || ' OTHER TERMS1 : ' || IH.OTHERS_COND1 || ' OTHER TERMS2 : ' || IH.OTHERS_COND2 , " & vbCrLf & " ID.PO_WEF_DATE, " & vbCrLf
        mSqlStr = mSqlStr & " ID.ITEM_CODE, " & vbCrLf & " ID.ITEM_UOM, ID.ITEM_QTY, " & vbCrLf & " ID.ITEM_PRICE, ID.ITEM_DIS_PER, ID.ITEM_QTY*ID.ITEM_PRICE, " & vbCrLf & " ITEM_SHORT_DESC, SUPP_CUST_NAME "
        ''FROM CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID," & vbCrLf & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST IMST"
        ''WHERE CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IMST.COMPANY_CODE(+)=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If Trim(mPONo) = "" Then
            If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSuppCode = MasterNo
            Else
                mSuppCode = "-1"
            End If
            mSqlStr = mSqlStr & vbCrLf & " AND IH.SUPP_CUST_CODE='" & mSuppCode & "'"
            mSqlStr = mSqlStr & vbCrLf & " AND SUBSTR(IH.AUTO_KEY_PO,LENGTH(IH.AUTO_KEY_PO)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
        Else
            mSqlStr = mSqlStr & vbCrLf & " AND IH.AUTO_KEY_PO=" & Val(mPONo) & ""
        End If
        mSqlStr = mSqlStr & vbCrLf & " AND IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND ID.ITEM_CODE=IMST.ITEM_CODE(+) AND PO_STATUS='Y'"
        ''& " AND IH.COMPANY_CODE=IMST.COMPANY_CODE " & vbCrLf _
        '
        ''ORDER CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & "ORDER BY IH.AUTO_KEY_PO,IH.AMEND_NO"
        SqlStr = " INSERT INTO TEMP_PO (" & vbCrLf & " USERID, AUTO_KEY_PO, COMPANY_CODE, " & vbCrLf & " PUR_TYPE, ORDER_TYPE, PUR_ORD_DATE, " & vbCrLf & " SUPP_CUST_CODE, AMEND_NO, AMEND_DATE, " & vbCrLf & " REMARKS, WO_DESCRIPTION," & vbCrLf & " CONDITIONS_CHG, " & vbCrLf & " AMEND_WEF_DATE, " & vbCrLf & " ITEM_CODE, ITEM_UOM, ITEM_QTY, " & vbCrLf & " ITEM_PRICE, ITEM_DIS_PER, GROSS_AMT, ITEM_SHORT_DESC, SUPP_CUST_NAME ) " & mSqlStr
        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        Exit Sub
ERR1:
        PubDBCn.RollbackTrans()
        MsgInformation(Err.Description)
    End Sub
    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String
        mSqlStr = mSqlStr & "SELECT * " & " FROM Temp_PO " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY AUTO_KEY_PO,AMEND_NO"
        FetchRecordForReport = mSqlStr
    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String, ByRef pIsPO As String)
        'Dim Printer As New Printer
        On Error GoTo ErrPart
        Dim mAmountInword As String
        Dim SqlStrSub As String = ""
        Dim mStateName As String
        Dim mStateCode As String = ""
        Dim mGSTStatus As String
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.ReportFileName = PubReportFolderPath & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False
        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_STATE", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND LOCATION_ID='" & txtBillTo.Text & "'") = True Then
            mStateName = MasterNo
            mStateCode = GetStateCode(mStateName)
        End If
        If mRptFileName = "PO_View.rpt" Then
        Else
            MainClass.AssignCRptFormulas(Report1, "COMPANYCINNo=""" & IIf(IsDBNull(RsCompany.Fields("CIN_NO").Value), "", RsCompany.Fields("CIN_NO").Value) & """")
            MainClass.AssignCRptFormulas(Report1, "CompanyGSTIN=""" & IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value) & """")
            MainClass.AssignCRptFormulas(Report1, "mStateCode=""" & mStateCode & """")
            '        If Left(cboGSTStatus.Text, 1) = "G" Then
            '            mGSTStatus = "GST Refund"
            '        ElseIf Left(cboGSTStatus.Text, 1) = "E" Then
            '            mGSTStatus = "Exempted"
            '        ElseIf Left(cboGSTStatus.Text, 1) = "R" Then
            '            mGSTStatus = "Reverse Charge"
            '        ElseIf Left(cboGSTStatus.Text, 1) = "N" Then
            '            mGSTStatus = "Non - GST"
            '        ElseIf Left(cboGSTStatus.Text, 1) = "I" Then
            '            mGSTStatus = "Ineligible"
            '        ElseIf Left(cboGSTStatus.Text, 1) = "C" Then
            '            mGSTStatus = "Composit"
            '        End If
            '        MainClass.AssignCRptFormulas Report1, "GSTStatus=""" & mGSTStatus & """"
        End If
        If pIsPO = "Y" Then
            Report1.SubreportToChange = Report1.GetNthSubreportName(0)
            Report1.Connect = STRRptConn
            Report1.SQLQuery = SqlStrSub
            Report1.SubreportToChange = ""
        Else
            mAmountInword = MainClass.RupeesConversion(CDbl(IIf(Val(lblNetAmount.Text) = 0, 0, lblNetAmount.Text)))
            If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
                MainClass.AssignCRptFormulas(Report1, "AmountInWord=""Rs. Zero""")
                MainClass.AssignCRptFormulas(Report1, "NetAmount=""0.00""")
            Else
                MainClass.AssignCRptFormulas(Report1, "AmountInWord=""" & mAmountInword & """")
                MainClass.AssignCRptFormulas(Report1, "NetAmount=""" & lblNetAmount.Text & """")
            End If
            SqlStrSub = " SELECT MKEY, SUBROWNO, EXPPERCENT, AMOUNT, COMPANY_CODE, NAME" & vbCrLf & " FROM FIN_PURCHASE_EXP, FIN_INTERFACE_MST " & vbCrLf & " WHERE FIN_PURCHASE_EXP.EXPCODE = FIN_INTERFACE_MST.CODE" & vbCrLf & " AND FIN_PURCHASE_EXP.MKEY='" & MainClass.AllowSingleQuote(LblMKey.Text) & "'" & vbCrLf & " AND FIN_INTERFACE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " "
            If CDate(txtVDate.Text) >= CDate(PubGSTApplicableDate) Then 'Change on 29/010/2017 before If CDate(txtVDate.Text) < CDate(PubGSTApplicableDate) Then
                SqlStrSub = SqlStrSub & vbCrLf & " AND GST_ENABLED='Y'"
            Else
                SqlStrSub = SqlStrSub & vbCrLf & " AND GST_ENABLED='N'"
            End If
            SqlStrSub = SqlStrSub & vbCrLf & " ORDER BY SUBROWNO"
            Report1.SubreportToChange = Report1.GetNthSubreportName(0)
            Report1.Connect = STRRptConn
            Report1.SQLQuery = SqlStrSub

            If lblPurchaseSeqType.Text <> "2" Then
                '        Report1.SubreportToChange = ""
                SqlStrSub = " SELECT ACM.SUPP_CUST_NAME, " & vbCrLf & " ABS(SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))) AS AMOUNT, " & vbCrLf & " CASE WHEN SUM(TRN.AMOUNT*DECODE(TRN.DC,'D',1,-1))<=0 THEN 'Cr' ELSE 'Dr' END AS DC " & vbCrLf & " FROM FIN_POSTED_TRN TRN , FIN_SUPP_CUST_MST ACM " & vbCrLf & " WHERE  " & vbCrLf & " TRN.Company_Code=ACM.Company_Code " & vbCrLf & " AND TRN.ACCOUNTCODE=ACM.SUPP_CUST_CODE " & vbCrLf & " AND TRN.Company_Code = " & RsCompany.Fields("Company_Code").Value & "" & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND BOOKTYPE ='" & mBookType & "'" & vbCrLf & " AND BOOKSUBTYPE = '" & mBookSubType & "'" & vbCrLf & " AND TRN.MKEY='" & LblMKey.Text & "'" & vbCrLf & " GROUP BY ACM.SUPP_CUST_NAME"
                Report1.SubreportToChange = Report1.GetNthSubreportName(1)
                Report1.Connect = STRRptConn
                Report1.SQLQuery = SqlStrSub
                Report1.SubreportToChange = ""
            End If

        End If
        'Dim prt As Printer
        'If PubUniversalPrinter = "Y" And mMode = Crystal.DestinationConstants.crptToPrinter Then
        'ForEach prt In Printers
        'If UCase(prt.DeviceName) = UCase("Universal Printer") Then
        'Printer = prt
        'Report1.PrinterName = prt.DeviceName
        'Report1.PrinterDriver = prt.DriverName
        'Report1.PrinterPort = prt.Port
        'Exit For
        'End If
        'Next prt
        'End If
        Report1.Action = 1
        Report1.ReportFileName = ""
        Report1.Reset()
        Exit Sub
ErrPart:
        '    Resume
        MsgBox(Err.Description)
    End Sub
    Private Function SendMrrToAccount() As Boolean
        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        SqlStr = " SELECT * FROM INV_GATE_HDR " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & "" & vbCrLf & " AND SEND_AC_FLAG='Y'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            SendMrrToAccount = True
        Else
            SendMrrToAccount = False
        End If
        Exit Function
ErrPart:
        SendMrrToAccount = False
    End Function
    Private Function CheckCRStockType(ByRef mItemType As String) As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mItemCode As String
        Dim mStockType As String
        CheckCRStockType = True
        SqlStr = " SELECT ITEM_CODE, STOCK_TYPE " & vbCrLf & " FROM  INV_GATE_DET" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & ""
        '            & vbCrLf _
        ''            & " AND STOCK_TYPE<>'CR'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        Do While RsTemp.EOF = False
            If RsTemp.EOF = True Then
                mItemCode = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)
                mItemType = GetProductionType(mItemCode)
                mStockType = IIf(IsDBNull(RsTemp.Fields("STOCK_TYPE").Value), "", RsTemp.Fields("STOCK_TYPE").Value)
                If mItemType = "R" Or mItemType = "B" Then
                    If mStockType <> "ST" Then
                        CheckCRStockType = False
                    End If
                Else
                    If mStockType <> "CR" Then
                        CheckCRStockType = False
                    End If
                End If
            End If
            RsTemp.MoveNext()
        Loop
        Exit Function
ErrPart:
        CheckCRStockType = False
    End Function
    Private Function CheckItemType() As String
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mItemCode As String
        CheckItemType = ""
        SqlStr = " SELECT ITEM_CODE, STOCK_TYPE " & vbCrLf & " FROM  INV_GATE_DET" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        '    Do While RsTemp.EOF = False
        If RsTemp.EOF = False Then
            mItemCode = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))
            CheckItemType = GetProductionType(mItemCode)
        End If
        '        RsTemp.MoveNext
        '    Loop
        Exit Function
ErrPart:
        CheckItemType = ""
    End Function
    Private Function GetPOInvType(ByRef pPONO As Double, ByRef pItemCode As String) As Double
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mItemCode As String
        GetPOInvType = -1

        SqlStr = " SELECT ACCOUNT_POSTING_CODE " & vbCrLf _
            & " FROM  PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.MKEY=ID.MKEY" & vbCrLf _
            & " AND IH.PO_STATUS='Y' AND IH.PO_CLOSED='N'" & vbCrLf _
            & " AND AUTO_KEY_PO=" & Val(CStr(pPONO)) & "" & vbCrLf _
            & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            GetPOInvType = CDbl(Trim(IIf(IsDBNull(RsTemp.Fields("ACCOUNT_POSTING_CODE").Value), -1, RsTemp.Fields("ACCOUNT_POSTING_CODE").Value)))
        End If
        Exit Function
ErrPart:
        GetPOInvType = -1
    End Function

    Private Function GetInvoiceExp(ByRef pPONO As Double, ByRef pItemRate As Double) As Double
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTotalExpAmount As Double
        Dim mADDDeduct As String
        Dim mItemValue As Double
        Dim mNETVALUE As Double
        SqlStr = "SELECT IH.ITEMVALUE, IH.NETVALUE, IE.AMOUNT, IMST.IDENTIFICATION, IMST.ADD_DED " & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_EXP IE, FIN_INTERFACE_MST IMST" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=IE.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=IMST.COMPANY_CODE " & vbCrLf & " AND IE.EXPCODE=IMST.CODE " & vbCrLf & " AND IH.AUTO_KEY_INVOICE =" & pPONO & "" & vbCrLf & " AND IH.REF_DESP_TYPE<>'U'" & vbCrLf & " ORDER BY IMST.PRINTSEQUENCE"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            mItemValue = IIf(IsDbNull(RsTemp.Fields("ITEMVALUE").Value), 0, RsTemp.Fields("ITEMVALUE").Value)
            mNETVALUE = IIf(IsDbNull(RsTemp.Fields("NETVALUE").Value), 0, RsTemp.Fields("NETVALUE").Value)
            Do While RsTemp.EOF
                If RsTemp.Fields("Identification").Value = "ST" Then GoTo NextCalc
                mADDDeduct = IIf(IsDbNull(RsTemp.Fields("ADD_DED").Value), 0, RsTemp.Fields("ADD_DED").Value)
                mTotalExpAmount = mTotalExpAmount + (IIf(IsDbNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value) * IIf(mADDDeduct = "D", -1, 1))
                RsTemp.MoveNext()
            Loop
        End If
NextCalc:
        If mItemValue = 0 Then
            GetInvoiceExp = 0
        Else
            GetInvoiceExp = CDbl(VB6.Format(mTotalExpAmount * pItemRate / mItemValue, "0.0000"))
        End If
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GetInvoiceExp = 0
    End Function
    Private Function GetPreviousRJQty(ByRef pcntRow As Integer, ByRef pItemCode As String) As Double
        On Error GoTo ErrPart
        Dim cntRow As Short
        Dim mItemCode As String
        Dim mRejectedQty As Double
        mRejectedQty = 0
        If pcntRow - 1 <= 0 Then GetPreviousRJQty = 0 : Exit Function
        With SprdMain
            For cntRow = 1 To pcntRow - 1
                .Row = cntRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)
                If Trim(mItemCode) = Trim(pItemCode) Then
                    .Col = ColRejectedQty
                    mRejectedQty = mRejectedQty + Val(.Text)
                End If
            Next
        End With
        GetPreviousRJQty = mRejectedQty
        Exit Function
ErrPart:
        GetPreviousRJQty = 0
        '    Resume
    End Function
    Private Sub FrmPurchaseGST_Resize(sender As Object, e As System.EventArgs) Handles Me.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 210, mReFormWidth - 210, mReFormWidth))
        FraFront.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        Frame6.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        SSTab1.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        UltraGrid1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtSection_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSection.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtSection_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSection.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtSection.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSection_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSection.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        If Trim(txtSection.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtSection.Text, "NAME", "CODE", "TDS_SECTION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invalid Section Code.")
                eventArgs.Cancel = True
            End If
        End If

        SqlStr = "SELECT NAME, TDS_DEFAULT_PER FROM TDS_SECTION_MST " & vbCrLf _
                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND NAME='" & MainClass.AllowSingleQuote(txtSection.Text) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            If Val(txtTDSRate.Text) = 0 Then
                txtTDSRate.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("TDS_DEFAULT_PER").Value), 0, RsTemp.Fields("TDS_DEFAULT_PER").Value), "0.000")
            End If
        End If

    End Sub
    Private Sub SearchSection()
        On Error GoTo ErrPart
        Dim SqlStr As String

        SqlStr = " SELECT TDSSECTION.NAME  AS NAME" & vbCrLf _
                & " From TDS_Section_MST TDSSECTION " & vbCrLf _
                & " Where TDSSECTION.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchBySQL(SqlStr, "NAME") = True Then
            txtSection.Text = Trim(AcName)
            txtSection_Validating(txtSection, New System.ComponentModel.CancelEventArgs(False)) ''_Validate False
        End If
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Sub txtSection_KeyUp(sender As Object, e As KeyEventArgs) Handles txtSection.KeyUp
        Dim KeyCode As Short = e.KeyCode
        Dim Shift As Short = e.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchSection()
    End Sub

    Private Sub txtSection_DoubleClick(sender As Object, e As EventArgs) Handles txtSection.DoubleClick
        Call SearchSection()
    End Sub
    Private Sub SprdPaymentDetail_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdPaymentDetail.Change
        MainClass.SaveStatus(frmAtrn.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdPaymentDetail_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdPaymentDetail.ClickEvent

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mCompanyCode As Long
        Dim mShortName As String
        Dim mSupplierCode As String

        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSupplierCode = MasterNo
        Else
            mSupplierCode = "-1"
        End If
        Select Case eventArgs.col
            Case 0
                If eventArgs.row > 0 Then
                    MainClass.DeleteSprdRow(SprdPaymentDetail, eventArgs.row, ColPayBillNo)
                    MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
                    CalcTots()
                End If
            Case ColPayBillNo
                If eventArgs.row = 0 Then
                    SearchBill(mSupplierCode)
                End If
        End Select
        Exit Sub
ERR1:
        ''Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SprdPaymentDetail_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdPaymentDetail.KeyDownEvent

        Dim mPayType As String
        Dim mActiveCol As Integer
        Dim mActiveRow As Integer
        Dim mSupplierCode As String

        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSupplierCode = MasterNo
        Else
            mSupplierCode = "-1"
        End If

        mActiveCol = SprdPaymentDetail.ActiveCol
        mActiveRow = SprdPaymentDetail.ActiveRow

        If eventArgs.keyCode = System.Windows.Forms.Keys.Return Or eventArgs.keyCode = System.Windows.Forms.Keys.Tab Then
            If mActiveCol = ColPayPaymentAmt Then
                SprdPaymentDetail.Row = SprdPaymentDetail.ActiveRow
                SprdPaymentDetail.Col = ColPayPaymentAmt
                If Val(SprdPaymentDetail.Text) <> 0 Then
                    If SprdPaymentDetail.MaxRows = SprdPaymentDetail.ActiveRow Then
                        MainClass.AddBlankSprdRow(SprdPaymentDetail, ColPayBillNo, ConRowHeight)
                        FormatSprdPaymentDetail((SprdPaymentDetail.MaxRows), False)
                        MainClass.SetFocusToCell(SprdPaymentDetail, mActiveRow, ColPayPaymentAmt)
                    End If
                End If

            End If
        ElseIf eventArgs.keyCode = System.Windows.Forms.Keys.F1 Then
            If SprdPaymentDetail.ActiveCol = ColPayBillNo Then SearchBill(mSupplierCode)
        End If
        eventArgs.keyCode = 9999
    End Sub
    Private Sub SprdPaymentDetail_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdPaymentDetail.LeaveCell

        On Error GoTo ERR1

        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

        Dim mPayType As String
        Dim mBillNo As String
        Dim mAmount As Double
        Dim mBillDate As String
        Dim mDueDays As Double
        Dim mPayCode As String
        Dim mPONo As String
        Dim mAccountCode As String = ""
        Dim mPrevBillAmount As Double
        Dim mCurrBillAmount As Double
        Dim mPOAmount As Double
        Dim mCompanyCode As Long
        Dim mCurrCompanyCode As Long
        Dim mBillCompanyName As String
        Dim mSupplierCode As String

        If eventArgs.newRow = -1 Then Exit Sub

        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSupplierCode = MasterNo
        Else
            mSupplierCode = "-1"
        End If

        SprdPaymentDetail.Row = eventArgs.row

        SprdPaymentDetail.Col = ColPayBillNo
        mBillNo = SprdPaymentDetail.Text

        SprdPaymentDetail.Col = ColPayBillDate
        mBillDate = SprdPaymentDetail.Text


        mCompanyCode = RsCompany.Fields("COMPANY_CODE").Value   ''GetCompanyCode(mBillNo, mBillDate, lblAccountCode.Text)       ' IIf(Val(SprdPaymentDetail.Text) <= 0, RsCompany.Fields("COMPANY_CODE").Value, Val(SprdPaymentDetail.Text))

        Dim mAccountName As String
        Select Case eventArgs.col

            Case ColPayBillNo

                If DuplicatePayBillNo() = False Then
                    If CheckBillNo(mSupplierCode) = True Then

                    End If
                    SprdPaymentDetail.Row = eventArgs.row

                    SprdPaymentDetail.Col = ColPayBillNo
                    mBillNo = SprdPaymentDetail.Text

                    '-------- FILLING BILL AMT TO AMT COL

                    SprdPaymentDetail.Col = ColPayBalAmount
                    mAmount = Val(SprdPaymentDetail.Text)
                    SprdPaymentDetail.Col = ColPayPaymentAmt
                    If Val(SprdPaymentDetail.Text) = 0 Then
                        SprdPaymentDetail.Text = IIf(Val(lblDiffAmt.Text) >= mAmount, mAmount, Val(lblDiffAmt.Text))
                    End If
                    '                MainClass.SetFocusToCell SprdPaymentDetail, Row, ColPayPaymentAmt
                    '                SprdPaymentDetail.Col = ColPayType
                End If
            Case ColPayBillDate
                SprdPaymentDetail.Row = eventArgs.row

                If DuplicatePayBillNo() = False Then
                    If CheckBillNo(mSupplierCode) = True Then

                    End If
                    If mPayType = "N" Then
                        SprdPaymentDetail.Row = eventArgs.row
                        SprdPaymentDetail.Col = ColPayBillDate
                        mBillDate = SprdPaymentDetail.Text

                        SprdPaymentDetail.Col = ColPayPaymentAmt
                        If Val(SprdPaymentDetail.Text) = 0 Then SprdPaymentDetail.Text = CStr(Val(lblDiffAmt.Text))
                    End If
                End If
            Case ColPayPaymentAmt
                SprdPaymentDetail.Row = eventArgs.row        ''SprdPaymentDetail.ActiveRow
                SprdPaymentDetail.Col = ColPayBillNo
                mBillNo = SprdPaymentDetail.Text
                SprdPaymentDetail.Col = ColPayPaymentAmt

                If CheckPayAmount() = False Then
                    MainClass.SetFocusToCell(SprdPaymentDetail, eventArgs.row, ColPayPaymentAmt)
                    Exit Sub
                End If

            Case ColPayBalDC
                SprdPaymentDetail.Col = ColPayBalDC
                SprdPaymentDetail.Row = eventArgs.row
                If UCase(SprdPaymentDetail.Text) = "DR" Or UCase(SprdPaymentDetail.Text) = "D" Then
                    SprdPaymentDetail.Text = "Dr"
                    Exit Sub
                ElseIf UCase(SprdPaymentDetail.Text) = "CR" Or UCase(SprdPaymentDetail.Text) = "C" Then
                    SprdPaymentDetail.Text = "Cr"
                    Exit Sub
                Else
                    eventArgs.col = ColPayBalDC
                    SprdPaymentDetail.Text = "Dr"
                    Exit Sub
                End If
                '            If Row <> NewRow Then CheckForEqualAmount

        End Select
        CalcTotsPayment()
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume
    End Sub
    Private Function DuplicatePayBillNo() As Boolean

        Dim cntRow As Integer
        Dim mCount As Byte
        Dim mCheckBillNo As String
        Dim mBillNo As String
        Dim mFYear As Integer

        With SprdPaymentDetail
            .Row = .ActiveRow
            .Col = ColPayBillNo
            mCheckBillNo = Trim(UCase(.Text))

            .Col = ColPayBillDate
            If Trim(.Text) <> "" Then
                If Month(CDate(VB6.Format(.Text, "DD/MM/YYYY"))) >= 4 And Month(CDate(VB6.Format(.Text, "DD/MM/YYYY"))) <= 12 Then
                    mFYear = CInt(VB6.Format(.Text, "YYYY"))
                Else
                    mFYear = CDbl(VB6.Format(.Text, "YYYY")) - 1
                End If
            End If

            mCheckBillNo = mCheckBillNo & ":" & VB6.Format(mFYear, "0000")

            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColPayBillNo
                mBillNo = Trim(UCase(.Text))

                .Col = ColPayBillDate
                If Trim(.Text) <> "" Then
                    If Month(CDate(VB6.Format(.Text, "DD/MM/YYYY"))) >= 4 And Month(CDate(VB6.Format(.Text, "DD/MM/YYYY"))) <= 12 Then
                        mFYear = CInt(VB6.Format(.Text, "YYYY"))
                    Else
                        mFYear = CDbl(VB6.Format(.Text, "YYYY")) - 1
                    End If
                End If
                mBillNo = mBillNo & ":" & VB6.Format(mFYear, "0000")

                If (mBillNo = mCheckBillNo And mCheckBillNo <> "") Then
                    mCount = mCount + 1
                End If

                If mCount > 1 Then
                    DuplicatePayBillNo = True
                    MainClass.SetFocusToCell(SprdPaymentDetail, .ActiveRow, ColPayBillNo, "Duplicate Bill No. : " & Mid(mCheckBillNo, 2))
                    Exit Function
                End If
            Next
        End With
    End Function
    Private Function CheckBillNo(ByRef pSupplierCode As String) As Boolean
        On Error GoTo ERR1
        Dim RS As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        Dim mPayType As String
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mBillAmount As Double
        Dim mDC As String
        Dim mPaymentAmt As Double

        Dim mBalance As Double
        Dim mRow As Integer
        Dim cntRow As Integer
        Dim mOldAmount As Double

        With SprdPaymentDetail
            mRow = .ActiveRow
            .Row = mRow
            .Col = ColPayBillNo
            mBillNo = Trim(.Text)

            If mBillNo = "" Then
                .Row = mRow
                .Col = ColPayBillNo
                .Text = ""

                .Col = ColPayBillDate
                .Text = ""

                .Col = ColPayBillAmount
                .Text = "0.00"

                .Col = ColPayBalAmount
                .Text = "0.00"

                .Col = ColPayPaymentAmt
                .Text = "0.00"

                CheckBillNo = True
                Exit Function
            End If



            .Col = ColPayBillDate
            mBillDate = .Text

            Call GetBalanceAmount(mRow, pSupplierCode, mBillNo, mBillDate, "B")
            'Call PickUpBillPayment("B", mBillNo, mOldAmount, "D")

        End With
        CheckBillNo = True
        Exit Function
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub FormatSprdPaymentDetail(ByRef Arow As Integer, ByRef mFromPopulate As Boolean)

        On Error GoTo ErrPart
        Dim RsTRN As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        SqlStr = "SELECT * FROM FIN_POSTED_TRN WHERE 1=2"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTRN, ADODB.LockTypeEnum.adLockReadOnly)

        With SprdPaymentDetail
            .set_RowHeight(Arow, ConRowHeight)
            .Row = Arow

            .Col = 0
            .set_ColWidth(0, 3)

            .Col = ColPayBillNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsTRN.Fields("BillNo").DefinedSize ''
            .set_ColWidth(.Col, 12)

            .ColsFrozen = ColPayBillNo


            .Col = ColPayBillDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeEditCharSet = SS_CELL_DATE_FORMAT_DDMMYY
            .TypeDateFormat = FPSpreadADO.TypeDateFormatConstants.TypeDateFormatDDMMYY
            .set_ColWidth(.Col, 8)


            .Col = ColPayBillAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(.Col, 8)


            .Col = ColPayBalAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(.Col, 8.25)

            .Col = ColPayBalDC
            .CellType = SS_CELL_TYPE_EDIT
            If mFromPopulate = False Then
                .Text = "Cr"    ''IIf(VB.Left(lblDC.Text, 1) = "C", "Cr", "Dr")
            End If
            .set_ColWidth(.Col, 3)

            .Col = ColPayPaymentAmt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("0")
            .TypeFloatMax = CDbl("9999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(.Col, 8.5)

            .Row = Arow
            MainClass.ProtectCell(SprdPaymentDetail, 1, .MaxRows, ColPayBillDate, ColPayBalDC)
            'MainClass.ProtectCell(SprdPaymentDetail, 1, .MaxRows, ColCompanyCode, ColCompanyCode)
            MainClass.SetSpreadColor(SprdPaymentDetail, Arow)


        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Private Sub GetBalanceAmount(ByRef pRow As Integer, ByRef pAccountCode As String, ByRef pBillNo As String, ByRef pBillDate As String, ByRef pPayType As String)

        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mBalance As Double
        Dim mActBillAmount As Double
        Dim mBillAmount As Double
        Dim mPaymentAmt As Double
        Dim mDueDays As Double
        Dim mBillDate As String
        Dim mPayCode As String
        Dim mBillDC As String
        Dim mDivCode As Double
        Dim mLocCode As String
        Dim mCompanyCode As Long
        Dim mCompanyName As String
        Dim mVNO As String


        mVNO = Trim(txtVNoPrefix.Text) & Trim(txtVNo.Text)

        SqlStr = " Select Company_Code,BillNo, BillDate,MAX(EXPDATE) AS DueDate , " & vbCrLf _
            & " SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount) AS BillAMT, " & vbCrLf _
            & " SUM(DECODE(BILLTYPE,'B',0,1)*DECODE(DC,'D',1,-1)*Amount) AS PayAMT " & vbCrLf _
            & " FROM FIN_POSTED_TRN  "

        SqlStr = SqlStr & vbCrLf _
            & " WHERE " & vbCrLf _
            & " FYEAR =" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND ACCOUNTCODE = '" & MainClass.AllowSingleQuote(pAccountCode) & "'"

        'If RsCompany.Fields("AC_PR_AUTO_JV").Value = "Y" Then
        'Else
        SqlStr = SqlStr & vbCrLf & " AND Company_Code=" & RsCompany.Fields("Company_Code").Value & ""
        'End If
        SqlStr = SqlStr & vbCrLf & " AND BillNo='" & MainClass.AllowSingleQuote(pBillNo) & "'"

        SqlStr = SqlStr & vbCrLf & " AND VNo<>'" & MainClass.AllowSingleQuote(mVNO) & "'"

        ''18-03-2010  ''Check New Bill Also.....
        If pPayType = "N" Then
            SqlStr = SqlStr & vbCrLf & " AND BillDate>=TO_DATE('" & VB6.Format(RsCompany.Fields("START_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND BillDate<=TO_DATE('" & VB6.Format(RsCompany.Fields("END_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            If pBillDate <> "" Then
                SqlStr = SqlStr & vbCrLf & " AND BillDate=TO_DATE('" & VB6.Format(pBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If
        End If

        SqlStr = SqlStr & vbCrLf & " GROUP BY Company_Code,BillNo, BillDate " & vbCrLf & " ORDER BY BillNo, BillDate,ABS(SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount))-SUM(DECODE(BILLTYPE,'B',0,1)*DECODE(DC,'D',1,-1)*Amount) DESC"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        With SprdPaymentDetail
            .Row = pRow
            If RsTemp.EOF = False Then


                mCompanyCode = RsCompany.Fields("COMPANY_CODE").Value

                .Col = ColPayBillDate
                mBillDate = IIf(IsDBNull(RsTemp.Fields("BillDate").Value), "", VB6.Format(RsTemp.Fields("BillDate").Value, "DD/MM/YYYY"))
                .Text = IIf(IsDBNull(RsTemp.Fields("BillDate").Value), "", VB6.Format(RsTemp.Fields("BillDate").Value, "DD/MM/YYYY"))

                .Col = ColPayBillAmount
                mActBillAmount = GetBillAmount(pAccountCode, pBillNo, mBillDate, Val(IIf(IsDBNull(RsTemp.Fields("BILLAMT").Value), 0, RsTemp.Fields("BILLAMT").Value)))
                mBillAmount = Val(IIf(IsDBNull(RsTemp.Fields("BILLAMT").Value), 0, RsTemp.Fields("BILLAMT").Value))
                .Text = Str(System.Math.Abs(mActBillAmount))

                '.Col = ColBillAmountDC
                '.Text = IIf(mActBillAmount >= 0, "Dr", "Cr")
                'mBillDC = IIf(mBillAmount >= 0, "Dr", "Cr")

                .Col = ColPayBalAmount
                'mPaymentAmt = Val(.Text)
                mPaymentAmt = Val(IIf(IsDBNull(RsTemp.Fields("PAYAMT").Value), 0, RsTemp.Fields("PAYAMT").Value))
                mBalance = mBillAmount + mPaymentAmt
                .Text = Str(System.Math.Abs(mBalance))
                '.Text = Str(Abs(mBalance) + Abs(mPRAmount))

                .Col = ColPayBalDC
                If mBalance = 0 Then
                    .Text = mBillDC
                Else
                    .Text = IIf(mBalance > 0, "Dr", "Cr")
                End If

                '********************
                .Row = .MaxRows
                .Row2 = .MaxRows
                .Col = 1
                .Col2 = .MaxCols
                .BlockMode = True
                .BackColor = System.Drawing.ColorTranslator.FromOle(IIf(UCase(mBillDC) = "CR", &H8000000F, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White))) ''&H80FF80
                .BlockMode = False
                '********************
            End If
        End With
    End Sub
    Private Function GetBillAmount(ByRef xAccountCode As String, ByRef xBillNo As String, ByRef xBillDate As String, ByRef xBillAmount As Double) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCheck As Integer
        Dim mBillYear As Integer


        mBillYear = GetCurrentFYNo(PubDBCn, xBillDate)
        If mBillYear = RsCompany.Fields("FYEAR").Value Then
            GetBillAmount = xBillAmount
            Exit Function
        End If

        mCheck = 1

NextSearch:
        GetBillAmount = 0
        SqlStr = " Select SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount) AS BillAMT " & vbCrLf _
            & " FROM FIN_POSTED_TRN  " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " ACCOUNTCODE = '" & MainClass.AllowSingleQuote(xAccountCode) & "'"

        'If RsCompany.Fields("AC_PR_AUTO_JV").Value = "Y" Then
        'Else
        SqlStr = SqlStr & vbCrLf & " AND Company_Code=" & RsCompany.Fields("Company_Code").Value & ""
        'End If
        If mCheck = 1 Then
            SqlStr = SqlStr & vbCrLf & " AND BOOKTYPE<>'O'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND BOOKTYPE='O'"
        End If

        '    SqlStr = SqlStr & vbCrLf & " AND DIV_CODE =" & Val(lblDivisionCode.text) & ""
        SqlStr = SqlStr & vbCrLf & " AND BillNo='" & MainClass.AllowSingleQuote(xBillNo) & "'"
        SqlStr = SqlStr & vbCrLf & " AND BillDate=TO_DATE('" & VB6.Format(xBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetBillAmount = IIf(IsDBNull(RsTemp.Fields("BillAMT").Value), 0, RsTemp.Fields("BillAMT").Value)
        Else
            If mCheck = 2 Then
                GetBillAmount = 0
            Else
                '            mBillYear = GetCurrentFYNo(PubDBCn, xBillDate)
                If mBillYear = RsCompany.Fields("FYEAR").Value Then
                    GetBillAmount = 0
                Else
                    mCheck = 2
                    GoTo NextSearch
                End If
            End If
        End If
        Exit Function
ErrPart:
        GetBillAmount = 0
    End Function
    Private Sub SearchBill(ByRef pSupplierCode As String)

        'Dim I As Integer
        Dim SqlStr As String = ""
        Dim mBillAmtStr As String
        Dim mADVAmtStr As String
        Dim mDNAmtStr As String
        Dim mCNAmtStr As String
        Dim mTDSAmtStr As String
        Dim mPayAmtStr As String
        Dim mBalAmtStr As String
        Dim mTrnTypeStr As String

        mBillAmtStr = "SUM(DECODE(BILLTYPE,'B',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mADVAmtStr = "SUM(DECODE(BILLTYPE,'A',1,DECODE(BILLTYPE,'O',1,0))*DECODE(DC,'D',1,-1)*Amount)"
        mDNAmtStr = "SUM(DECODE(BILLTYPE,'D',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mCNAmtStr = "SUM(DECODE(BILLTYPE,'C',1,0)*DECODE(DC,'D',1,-1)*Amount)"
        mTDSAmtStr = "SUM(DECODE(BILLTYPE,'T',1,0)*DECODE(DC,'D',1,-1)*Amount)"

        mPayAmtStr = "SUM(DECODE(BILLTYPE,'P',1,0)*DECODE(DC,'D',1,-1)*Amount)"

        ''mBalAmtStr = "ABS(" & mBillAmtStr & ")-ABS(" & mPayAmtStr & ")"
        mBalAmtStr = "" & mBillAmtStr & " + " & mADVAmtStr & " + " & mDNAmtStr & " + " & mTDSAmtStr & " +" & mCNAmtStr & " + " & mPayAmtStr & ""


        mTrnTypeStr = " CASE WHEN TRNTYPE='N' OR TRNTYPE='B' THEN 'BILL' " & vbCrLf _
            & " WHEN TRNTYPE= 'O' THEN 'ON ACCOUNT' " & vbCrLf _
            & " WHEN TRNTYPE='A' THEN 'ADVANCE' " & vbCrLf _
            & " WHEN TRNTYPE='T' THEN 'TDS' " & vbCrLf _
            & " WHEN TRNTYPE='D' THEN 'D/N' ELSE 'C/N' END"

        SqlStr = " Select BillNo, BillDate, LOCATION_ID," & vbCrLf _
            & " TO_CHAR(ABS(" & mBillAmtStr & ")) AS BillAMT, " & vbCrLf _
            & " CASE WHEN " & mBillAmtStr & " >=0 THEn 'DR' ELSE 'CR' END AS DC , " & vbCrLf _
            & " TO_CHAR(ABS(" & mADVAmtStr & ")) AS ADV, " & vbCrLf _
            & " TO_CHAR(ABS(" & mDNAmtStr & ")) AS DNOTE, " & vbCrLf _
            & " TO_CHAR(ABS(" & mCNAmtStr & ")) AS CNOTE, " & vbCrLf _
            & " TO_CHAR(ABS(" & mTDSAmtStr & ")) AS TDS, " & vbCrLf _
            & " TO_CHAR(ABS(" & mBalAmtStr & ")) AS BALANCE, " & vbCrLf _
            & " CASE WHEN " & mBalAmtStr & " >=0 THEn 'DR' ELSE 'CR' END AS DC, MAX(DUEDATE) AS DUEDATE,COMPANY_CODE  " & vbCrLf _
            & " FROM FIN_POSTED_TRN  " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND AccountCode = '" & pSupplierCode & "'"      '' AND TRNTYPE='B'"

        'If RsCompany.Fields("AC_PR_AUTO_JV").Value = "Y" Then
        'Else
        SqlStr = SqlStr & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        'End If
        '    SqlStr = SqlStr & vbCrLf & " AND DIV_CODE =" & Val(lblDivisionCode.text) & ""

        SqlStr = SqlStr & vbCrLf & " AND LOCATION_ID='" & txtBillTo.Text & "'"

        SqlStr = SqlStr & vbCrLf & " GROUP BY  BillDate, BillNo,COMPANY_CODE,LOCATION_ID" & vbCrLf _
            & " HAVING " & mBalAmtStr & " <>0 " & vbCrLf _
            & " ORDER BY BillDate, BillNo "

        MainClass.SearchGridMasterBySQL("", SqlStr)

        If AcName <> "" Then
            SprdPaymentDetail.Row = SprdPaymentDetail.ActiveRow
            SprdPaymentDetail.Col = ColPayBillNo
            SprdPaymentDetail.Text = AcName
            SprdPaymentDetail.Col = ColPayBillDate
            SprdPaymentDetail.Text = AcName1
            MainClass.SetFocusToCell(SprdPaymentDetail, SprdPaymentDetail.ActiveRow, ColPayBillNo)
        End If
        Exit Sub

    End Sub
    Private Function CheckPayAmount() As Boolean
        Dim mDC As String
        Dim mBalance As Double
        Dim mBalanceDC As String
        Dim mOldAmount As Double
        Dim mOldDC As String
        Dim mNetBalance As Double
        Dim mCurrAmount As Double

        With SprdPaymentDetail

            .Col = ColPayBalDC
            mBalanceDC = VB.Left(.Text, 1)

            .Col = ColPayBalAmount
            mBalance = Val(.Text) * IIf(mBalanceDC = "D", 1, -1)

            mNetBalance = mBalance + mOldAmount

            mDC = mBalanceDC

            .Col = ColAmount
            mCurrAmount = Val(.Text) * IIf(mDC = "D", -1, 1)

            If System.Math.Abs(mCurrAmount) > System.Math.Abs(mNetBalance) Then
                ErrorMsg("Amount Exceeds", "", MsgBoxStyle.Critical)
                CheckPayAmount = False
            Else
                CheckPayAmount = True
            End If


        End With
    End Function
    Private Sub CalcTotsPayment()
        On Error GoTo ErrSprdTotal
        Dim mDAmt As Double
        Dim mCAmt As Double
        Dim mNetAmt As Double
        Dim MTotalAmt As Double
        Dim cntRow As Integer
        Dim mDC As String
        Dim mDrCr As String = ""

        With SprdPaymentDetail
            For cntRow = 1 To .MaxRows - 1 Step 1
                .Row = cntRow


                .Col = ColPayBalDC
                mDC = VB.Left(.Text, 1)

                .Col = ColPayPaymentAmt
                If mDC = "D" Then
                    mDAmt = mDAmt + Val(.Value)
                Else
                    mCAmt = mCAmt + Val(.Value)
                End If

                mNetAmt = System.Math.Abs(mCAmt - mDAmt)

NextRow:
            Next cntRow
        End With


        lblDiffAmt.Text = Val(lblNetAmount.Text) - Val(mNetAmt)

ErrSprdTotal:
    End Sub
    Private Function UpdatePaymentDetail1(ByRef pDBCn As ADODB.Connection, ByRef pMKey As String, ByRef pCurrRowNo As Integer, ByRef pBookCode As String, ByRef pBookType As String, ByRef pBookSubType As String, ByRef pVNo As String, ByRef pVDate As String, ByRef pBillNo As String, ByRef pBillDate As String, ByRef pTRNType As String, ByRef pSupplierCode As String, ByRef pAccountCode As String, ByRef pItemValue As Double, ByRef pNetBillValue As Double, ByRef pCancel As Boolean, ByRef pFOC As Boolean, ByRef pDueDate As String, ByRef pNarration As String, ByRef pRemarks As String,
                    ByRef pExpAmount As Double, ByRef pISGSTRefund As String, ByRef pCGSTRefundAmount As Double, ByRef pSGSTRefundAmount As Double, ByRef pIGSTRefundAmount As Double, ByRef pMRRDate As String, ByRef pAddMode As Boolean, ByRef pAddUser As String, ByRef pAddDate As String, ByRef mDivisionCode As Double, ByRef pReverseCharge As String, ByRef pReverseTaxAmount As Double, ByRef pReverseCGST As Double, ByRef pReverseSGST As Double, ByRef pReverseIGST As Double, ByRef pSaleBillNo As String,
                    ByRef pSaleBillDate As String, ByRef pLoactionID As String) As Boolean



        On Error GoTo UpdatePaymentDetail1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mPayBillNo As String
        Dim mPayBillDate As String
        Dim mPayBillAmount As Double
        Dim mPayBalDC As String
        Dim mPayPaymentAmt As Double
        Dim mTotPayPaymentAmt As Double
        Dim mSubRowNo As Long

        PubDBCn.Execute("Delete From FIN_PURBILLDETAILS_TRN Where Mkey='" & LblMKey.Text & "' AND BookType='" & UCase(mBookType) & "'")

        pDBCn.Execute("DELETE FROM FIN_POSTED_TRN WHERE MKey='" & LblMKey.Text & "' AND BookType='" & UCase(pBookType) & "' AND BookCode='" & UCase(pBookCode) & "' AND BILLTYPE='P'")

        mTotPayPaymentAmt = 0
        If SprdPaymentDetail.MaxRows = 1 Then
            UpdatePaymentDetail1 = True
            Exit Function
        End If

        mSubRowNo = 1000

        With SprdPaymentDetail
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColPayBillNo
                mPayBillNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColPayBillDate
                mPayBillDate = VB6.Format(.Text, "DD/MM/YYYY")

                .Col = ColPayBillAmount
                mPayBillAmount = Val(.Text)

                .Col = ColPayBalDC
                mPayBalDC = Mid(.Text, 1, 1)

                .Col = ColPayPaymentAmt
                mPayPaymentAmt = Val(.Text)

                mTotPayPaymentAmt = mTotPayPaymentAmt + mPayPaymentAmt

                SqlStr = ""
                If mPayBillNo <> "" And mPayPaymentAmt > 0 Then
                    SqlStr = " INSERT INTO FIN_PURBILLDETAILS_TRN (COMPANY_CODE, " & vbCrLf _
                        & " MKEY , SUBROWNO, " & vbCrLf _
                        & " ACCOUNTCODE , BILLNO, BILLDATE, BILLAMOUNT, BILLDC, " & vbCrLf _
                        & " AMOUNT , DC, BOOKTYPE) "

                    SqlStr = SqlStr & vbCrLf _
                        & " VALUES (" & RsCompany.Fields("COMPANY_CODE").Value & ",'" & pMKey & "'," & I & ", " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(pSupplierCode) & "','" & mPayBillNo & "',TO_DATE('" & VB6.Format(mPayBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & mPayBillAmount & ", " & vbCrLf _
                        & " '" & mPayBalDC & "'," & mPayPaymentAmt & ",'" & mPayBalDC & "','" & UCase(mBookType) & "') "

                    PubDBCn.Execute(SqlStr)

                    'If lblPurchaseSeqType.Text = "2" Or lblPurchaseSeqType.Text = "8" Then
                    'Else
                    If UpdateTRN(pDBCn, pMKey, pCurrRowNo, mSubRowNo + I, pBookCode, "P", pBookType, pBookSubType, pSupplierCode,
                         pVNo, pVDate, mPayBillNo, mPayBillDate, mPayPaymentAmt, "C", "O", "", "",
                         CStr(-1), CStr(-1), CStr(-1), CStr(-1), pDueDate, "", "P", "", "",
                         pNarration, pRemarks, pMRRDate, pAddMode, pAddUser, pAddDate, mDivisionCode, pLoactionID) = False Then GoTo UpdatePaymentDetail1
                    'End If


                End If
            Next
        End With

        'If lblPurchaseSeqType.Text = "2" Or lblPurchaseSeqType.Text = "8" Then
        'Else
        If UpdateTRN(pDBCn, pMKey, pCurrRowNo, mSubRowNo + I, pBookCode, "P", pBookType, pBookSubType, pSupplierCode,
                         pVNo, pVDate, pBillNo, pBillDate, mTotPayPaymentAmt, "D", "B", "", "",
                         CStr(-1), CStr(-1), CStr(-1), CStr(-1), pDueDate, "", "P", "", "",
                         pNarration, pRemarks, pMRRDate, pAddMode, pAddUser, pAddDate, mDivisionCode, pLoactionID) = False Then GoTo UpdatePaymentDetail1
        'End If


        UpdatePaymentDetail1 = True
        Exit Function
UpdatePaymentDetail1:
        UpdatePaymentDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function
    Private Sub ShowPaymentDetail1(ByRef mMkey As String, ByRef mSupplierCode As String)
        On Error GoTo ERR1
        Dim I As Integer
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim SqlStr As String = ""
        Dim mBillNo As String
        Dim mBillDate As String

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf _
            & " FROM FIN_PURBILLDETAILS_TRN " & vbCrLf _
            & " Where Mkey='" & mMkey & "' AND BookType='" & UCase(mBookType) & "'" & vbCrLf _
            & " Order By SubRowNo"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        With RsTemp
            If .EOF = True Then Exit Sub
            FormatSprdPaymentDetail(-1, False)
            I = 1
            .MoveFirst()
            Do While Not .EOF
                SprdPaymentDetail.Row = I
                SprdPaymentDetail.Col = ColPayBillNo
                SprdPaymentDetail.Text = IIf(IsDBNull(.Fields("BILLNO").Value), "", .Fields("BILLNO").Value)
                mBillNo = IIf(IsDBNull(.Fields("BILLNO").Value), "", .Fields("BILLNO").Value)

                SprdPaymentDetail.Col = ColPayBillDate
                SprdPaymentDetail.Text = VB6.Format(IIf(IsDBNull(.Fields("BILLDATE").Value), "", .Fields("BILLDATE").Value), "DD/MM/YYYY")
                mBillDate = VB6.Format(IIf(IsDBNull(.Fields("BILLDATE").Value), "", .Fields("BILLDATE").Value), "DD/MM/YYYY")

                SprdPaymentDetail.Col = ColPayBillAmount
                SprdPaymentDetail.Text = CStr(Val(IIf(IsDBNull(.Fields("BILLAMOUNT").Value), 0, .Fields("BILLAMOUNT").Value)))

                'SprdPaymentDetail.Col = ColPayBalAmount
                'SprdPaymentDetail.Text = CStr(Val(IIf(IsDBNull(.Fields("IGST_PER").Value), 0, .Fields("IGST_PER").Value)))

                SprdPaymentDetail.Col = ColPayBalDC
                SprdPaymentDetail.Text = IIf(IsDBNull(.Fields("DC").Value), "D", .Fields("DC").Value)

                SprdPaymentDetail.Col = ColPayPaymentAmt
                SprdPaymentDetail.Text = CStr(Val(IIf(IsDBNull(.Fields("AMOUNT").Value), 0, .Fields("AMOUNT").Value)))

                Call GetBalanceAmount(I, (mSupplierCode), mBillNo, mBillDate, "B")

                .MoveNext()
                I = I + 1
                SprdPaymentDetail.MaxRows = I
            Loop
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume
    End Sub

    Private Sub CmdUpdatePayment_Click(sender As Object, e As EventArgs) Handles CmdUpdatePayment.Click
        On Error GoTo ErrPart
        Dim mMannualAdjustment As String
        Dim mSRBillNo As String
        Dim mSRBillDate As String
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim RsPostSRTrn As ADODB.Recordset
        Dim mRow As Long
        Dim mBalanceAmount As Double
        Dim mPartyGSTNo As String
        Dim mCompanyGSTNo As String

        Dim nMkey As String
        Dim mTRNType As String
        Dim mVNoSeq As Double
        Dim mVNo As String
        Dim mSuppCustCode As String
        Dim mModvatSuppCode As String
        Dim mAccountCode As String
        Dim mFREIGHTCHARGES As String
        Dim mEXEMPT_NOTIF_NO As String
        Dim mItemValue As Double
        Dim mTOTSTAMT As Double
        Dim mTOTCHARGES As Double
        Dim mTotEDAmount As Double
        Dim mTotEDUAmount As Double
        Dim mTOTEXPAMT As Double
        Dim mNETVALUE As Double
        Dim mTotQty As Double
        Dim mLSTCST As String
        Dim mWITHFORM As String
        Dim mPRINTED As String
        Dim mCancelled As String
        Dim mIsRegdNo As String
        Dim mSTType As String
        Dim mBookCode As Integer
        Dim mStartingNo As Double
        Dim mSTPERCENT As Double
        Dim mTOTFREIGHT As Double
        Dim mEDPERCENT As Double
        Dim mEDUPERCENT As Double
        Dim mSHECPercent As Double
        Dim mTOTTAXABLEAMOUNT As Double
        Dim mIsGSTRefund As String
        Dim mRO As Double
        Dim mSURAmount As Double
        Dim mMSC As Double
        Dim mTotDiscount As Double
        Dim mREJECTION As String
        Dim pDueDate As String
        Dim mModvatNo As Integer
        Dim mSERVNo As Integer
        Dim mSTCLAIMNo As Integer
        Dim mCapital As String
        Dim mNarration As String = ""
        Dim mISMODVAT As String
        Dim mIsServClaim As String
        Dim mISSTREFUND As String
        Dim mISCSTREFUND As String
        Dim mFinalPost As String
        Dim mItemType As String
        Dim cntRow As Integer
        Dim xShortageQty As Double
        Dim xRejectedQty As Double
        Dim mPreviousRJ As Double
        Dim mAlreadyRejQty As Double
        Dim mDNCNQty As Double
        Dim xDebitAmt As Double
        Dim xPORate As Double
        Dim xRate As Double
        Dim xRateDiffDN As Double
        Dim xRateDiffCN As Double
        Dim mISTDSDEDUCT As String
        Dim mISESIDEDUCT As String
        Dim mISSTDSDEDUCT As String
        Dim mPDIRItem As Integer
        Dim PDIRAmount As Double
        Dim mDNCNCreated As Boolean
        Dim xExpDiffDN As Boolean
        Dim mFormRecdCode As Integer
        Dim mFormDueCode As Integer
        Dim mISFOC As String
        Dim mIsSuppBill As String
        Dim mServTax_Repost As String
        Dim mApproved As String
        Dim mItemCode As String
        Dim mModvatType As Integer
        Dim mISFixAssets As String
        Dim mItemDesc As String
        Dim mModvatAmount As Double
        Dim mLocal As String
        Dim mDivisionCode As Double
        Dim xItemValue As Double
        Dim xTOTEXPAMT As Double

        Dim xNETVALUE As Double

        Dim mFirstRow As Boolean
        Dim mSubRowNo As Integer
        Dim mGSTNo As Double
        Dim mTotGSTAmount As Double
        Dim mShipTo As String
        Dim mShipToCode As String = ""
        Dim mNetExpAmount As Double
        Dim mSaleBillNoPrefix As String
        Dim mSaleBillNoSeq As Double
        Dim mSaleBillNo As String
        Dim mSaleBillDate As String
        Dim mNewGSTNo As Boolean

        Dim mSACCode As String
        Dim mItemCGST As Double
        Dim mItemSGST As Double
        Dim mItemIGST As Double
        'Dim mBookType As String
        Dim mAlias As String
        If SprdPaymentDetail.MaxRows <= 1 Then Exit Sub

        If ADDMode = True Or MODIFYMode = True Then Exit Sub

        'If lblPurchaseSeqType.Text = "2" Or lblPurchaseSeqType.Text = "8" Then
        'Else
        '    Exit Sub
        'End If

        With SprdPaymentDetail
            For I = 1 To .MaxRows - 1
                .Row = I
                .Col = ColPayBillDate
                If Not IsDate(CDate(.Text)) Then
                    MsgInformation("Invalid Bill Date.")
                    Exit Sub
                End If

            Next
        End With


        mMannualAdjustment = IIf(IsDBNull(RsCompany.Fields("MANNUAL_BILL_ADJUST").Value), "N", RsCompany.Fields("MANNUAL_BILL_ADJUST").Value)
        If mMannualAdjustment = "N" Then Exit Sub

        If Trim(txtPONo.Text) = "" Then
            mSRBillNo = txtBillNo.Text
            mSRBillDate = txtBillDate.Text
        Else
            mSRBillNo = txtBillNo.Text
            mSRBillDate = txtBillDate.Text
        End If

        mCompanyGSTNo = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)
        mPartyGSTNo = ""
        If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "GST_RGN_NO", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mPartyGSTNo = MasterNo
        End If
        mNarration = ""

        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I
                .Col = ColItemDesc
                mItemDesc = MainClass.AllowSingleQuote(.Text)
                mNarration = mNarration & IIf(mNarration = "", "", ", ") & mItemDesc

            Next
        End With

        mNarration = IIf(mNarration = "", "", IIf(mBookSubType = "J", " ( JobWork of :", " ( Cost of :")) & mNarration & IIf(mNarration = "", "", " )")



        If lblPurchaseSeqType.Text = "2" Or lblPurchaseSeqType.Text = "8" Then     '' mBookSubType = "R" Then



            SqlStr = "SELECT * FROM FIN_PURCHASE_HDR WHERE MKEY='" & LblMKey.Text & "' AND PURCHASESEQTYPE=" & Val(lblPurchaseSeqType.Text) & ""
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                mRow = SprdPaymentDetail.MaxRows
                mBalanceAmount = IIf(IsDBNull(RsTemp.Fields("NETVALUE").Value), 0, RsTemp.Fields("NETVALUE").Value) '' Val(lblNetAmount.Text)
                mTRNType = IIf(IsDBNull(RsTemp.Fields("TRNTYPE").Value), "-1", RsTemp.Fields("TRNTYPE").Value)
                mSuppCustCode = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value)
                mAccountCode = IIf(IsDBNull(RsTemp.Fields("ACCOUNTCODE").Value), "-1", RsTemp.Fields("ACCOUNTCODE").Value)
                mIsGSTRefund = IIf(IsDBNull(RsTemp.Fields("ISGSTAPPLICABLE").Value), "G", RsTemp.Fields("ISGSTAPPLICABLE").Value)

                'mBookType = IIf(IsDBNull(RsTemp.Fields("BOOOKTYPE").Value), "", RsTemp.Fields("BOOOKTYPE").Value)
                mBookSubType = IIf(IsDBNull(RsTemp.Fields("BOOKSUBTYPE").Value), "", RsTemp.Fields("BOOKSUBTYPE").Value)
                mSubRowNo = 0      'IIf(IsDBNull(RsTemp.Fields("ISGSTAPPLICABLE").Value), "G", RsTemp.Fields("ISGSTAPPLICABLE").Value)
                mVNo = IIf(IsDBNull(RsTemp.Fields("VNO").Value), "", RsTemp.Fields("VNO").Value)
                pDueDate = txtPaymentdate.Text
                mLocal = "N"
                mLocal = GetPartyBusinessDetail(Trim(mSuppCustCode), Trim(txtBillTo.Text), "WITHIN_STATE")

                mItemValue = IIf(IsDBNull(RsTemp.Fields("ITEMVALUE").Value), 0, RsTemp.Fields("ITEMVALUE").Value) '' Val(lblNetAmount.Text)

                mDivisionCode = IIf(IsDBNull(RsTemp.Fields("DIV_CODE").Value), "G", RsTemp.Fields("DIV_CODE").Value)

                'If UpdatePaymentDetail1(mSuppCustCode, mDivisionCode) = False Then GoTo ErrPart

                If MainClass.ValidateWithMasterTable(mTRNType, "Code", "Alias", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAlias = MasterNo & "-"
                Else
                    mAlias = ""
                End If

                If mBookSubType = "W" Then
                    If chkFOC.CheckState = System.Windows.Forms.CheckState.Checked Then
                        mNarration = "Bill No : " & txtBillNo.Text & " (Cancelled)"
                    Else
                        mNarration = "Bill No : " & txtBillNo.Text & mNarration
                    End If
                Else
                    If chkFOC.CheckState = System.Windows.Forms.CheckState.Checked Then
                        mNarration = "Bill No : " & mAlias & txtBillNo.Text & " (Cancelled)"
                    Else
                        mNarration = "Bill No : " & mAlias & txtBillNo.Text & mNarration
                    End If
                End If

                If UpdatePaymentDetail1(PubDBCn, (LblMKey.Text), mCurRowNo, (LblBookCode.Text), mBookType, mBookSubType, mVNo, (txtVDate.Text), (txtBillNo.Text), (txtBillDate.Text), mTRNType, mSuppCustCode, mAccountCode, Val(CStr(mItemValue)), Val(lblNetAmount.Text), IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, True, False), IIf(chkFOC.CheckState = System.Windows.Forms.CheckState.Checked, True, False), pDueDate, VB.Left(mNarration, 254), (txtRemarks.Text), mNetExpAmount, IIf(mIsGSTRefund = "G", "Y", "N"), Val(txtTotCGSTRefund.Text), Val(txtTotSGSTRefund.Text), Val(txtTotIGSTRefund.Text), (txtMRRDate.Text), ADDMode, mAddUser, mAddDate, mDivisionCode, IIf(mIsGSTRefund = "R", "Y", "N"), Val(lblTotCGSTAmount.Text) + Val(lblTotIGSTAmount.Text) + Val(lblTotSGSTAmount.Text), Val(lblTotCGSTAmount.Text), Val(lblTotSGSTAmount.Text), Val(lblTotIGSTAmount.Text), mSaleBillNo, mSaleBillDate, txtBillTo.Text) = False Then GoTo ErrPart

                If mMannualAdjustment = "Y" Then

                    If mCompanyGSTNo = mPartyGSTNo Then
                        mNetExpAmount = IIf(IsDBNull(RsTemp.Fields("TOTEXPAMT").Value), 0, RsTemp.Fields("TOTEXPAMT").Value) '         '' Val(lblTotExpAmt.Text)
                        mItemCGST = 0
                        mItemSGST = 0
                        mItemIGST = 0
                    Else
                        mItemCGST = IIf(IsDBNull(RsTemp.Fields("TOTCGST_AMOUNT").Value), 0, RsTemp.Fields("TOTCGST_AMOUNT").Value) '      ''Val(lblTotCGSTAmount.Text)
                        mItemSGST = IIf(IsDBNull(RsTemp.Fields("TOTSGST_AMOUNT").Value), 0, RsTemp.Fields("TOTSGST_AMOUNT").Value) 'Val(lblTotSGSTAmount.Text)
                        mItemIGST = IIf(IsDBNull(RsTemp.Fields("TOTIGST_AMOUNT").Value), 0, RsTemp.Fields("TOTIGST_AMOUNT").Value) ' Val(lblTotIGSTAmount.Text)

                        If VB.Left(cboGSTStatus.Text, 1) = "I" Then     ''VB.Left(cboGSTStatus.Text, 1) = "G" Or 
                            mNetExpAmount = Val(mNetExpAmount) + Val(mItemCGST) + Val(mItemSGST) + Val(mItemIGST)  ' Val(lblTotExpAmt.Text) + Val(lblTotCGSTAmount.Text) + Val(lblTotIGSTAmount.Text) + Val(lblTotSGSTAmount.Text)
                        Else
                            mNetExpAmount = IIf(IsDBNull(RsTemp.Fields("TOTEXPAMT").Value), 0, RsTemp.Fields("TOTEXPAMT").Value)      ' Val(lblTotExpAmt.Text)
                        End If
                    End If
                    mFirstRow = True

                    With SprdPaymentDetail
                        For mRow = 1 To SprdPaymentDetail.MaxRows - 1
                            .Row = mRow



                            .Col = ColPayBillNo
                            mSRBillNo = Trim(.Text)

                            .Col = ColPayBillDate
                            mSRBillDate = Trim(.Text)

                            .Col = ColPayPaymentAmt
                            xNETVALUE = Val(.Text)
                            mBalanceAmount = mBalanceAmount - xNETVALUE

                            mSubRowNo = mSubRowNo + 1

                            If SaleReturnPostTRNGSTNew(PubDBCn, (LblMKey.Text), mRow, (LblBookCode.Text), mBookType, mBookSubType, mSubRowNo, mVNo,
                                (txtVDate.Text), mSRBillNo, mSRBillDate, mTRNType, mSuppCustCode, mAccountCode, Val(CStr(xNETVALUE)),
                                IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, True, False),
                                IIf(chkFOC.CheckState = System.Windows.Forms.CheckState.Checked, True, False), pDueDate,
                                IIf(chkRejection.CheckState = System.Windows.Forms.CheckState.Checked, True, False), VB.Left(mNarration, 254), (txtRemarks.Text),
                                Val(mNetExpAmount), mItemCGST, mItemSGST, mItemIGST, IIf(mIsGSTRefund = "G", "Y", "N"),
                                txtBillNo.Text, "", (txtMRRDate.Text), Val(mItemValue), ADDMode, mAddUser, mAddDate, mLocal, mDivisionCode,
                                mFirstRow, txtBillTo.Text, Val(lblPurchaseSeqType.Text)) = False Then GoTo ErrPart

                            mFirstRow = False
                        Next
                    End With
                    If mBalanceAmount <> 0 Then
                        xNETVALUE = mBalanceAmount
                        mSRBillNo = mVNo
                        mSRBillDate = txtVDate.Text

                        mSubRowNo = mSubRowNo + 1

                        If SaleReturnPostTRNGSTNew(PubDBCn, (LblMKey.Text), mRow, (LblBookCode.Text), mBookType, mBookSubType, mSubRowNo, mVNo,
                                (txtVDate.Text), mSRBillNo, mSRBillDate, mTRNType, mSuppCustCode, mAccountCode, Val(CStr(xNETVALUE)),
                                IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, True, False),
                                IIf(chkFOC.CheckState = System.Windows.Forms.CheckState.Checked, True, False), pDueDate,
                                IIf(chkRejection.CheckState = System.Windows.Forms.CheckState.Checked, True, False), VB.Left(mNarration, 254), (txtRemarks.Text),
                                Val(mNetExpAmount), mItemCGST, mItemSGST, mItemIGST, IIf(mIsGSTRefund = "G", "Y", "N"),
                               txtBillNo.Text, "", (txtMRRDate.Text), Val(mItemValue), ADDMode, mAddUser, mAddDate, mLocal, mDivisionCode,
                                mFirstRow, txtBillTo.Text, Val(lblPurchaseSeqType.Text)) = False Then GoTo ErrPart
                    End If
                End If

            End If
        Else
            SqlStr = "SELECT * FROM FIN_PURCHASE_HDR WHERE MKEY='" & LblMKey.Text & "' AND PURCHASESEQTYPE=" & Val(lblPurchaseSeqType.Text) & ""
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                mRow = SprdPaymentDetail.MaxRows
                mBalanceAmount = IIf(IsDBNull(RsTemp.Fields("NETVALUE").Value), 0, RsTemp.Fields("NETVALUE").Value) '' Val(lblNetAmount.Text)
                mTRNType = IIf(IsDBNull(RsTemp.Fields("TRNTYPE").Value), "-1", RsTemp.Fields("TRNTYPE").Value)
                mSuppCustCode = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value)
                mAccountCode = IIf(IsDBNull(RsTemp.Fields("ACCOUNTCODE").Value), "-1", RsTemp.Fields("ACCOUNTCODE").Value)
                mIsGSTRefund = IIf(IsDBNull(RsTemp.Fields("ISGSTAPPLICABLE").Value), "G", RsTemp.Fields("ISGSTAPPLICABLE").Value)

                'mBookType = IIf(IsDBNull(RsTemp.Fields("BOOOKTYPE").Value), "", RsTemp.Fields("BOOOKTYPE").Value)
                mBookSubType = IIf(IsDBNull(RsTemp.Fields("BOOKSUBTYPE").Value), "", RsTemp.Fields("BOOKSUBTYPE").Value)

                If MainClass.ValidateWithMasterTable(mTRNType, "Code", "Alias", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mAlias = MasterNo & "-"
                Else
                    mAlias = ""
                End If

                If mBookSubType = "W" Then
                    If chkFOC.CheckState = System.Windows.Forms.CheckState.Checked Then
                        mNarration = "Bill No : " & txtBillNo.Text & " (Cancelled)"
                    Else
                        mNarration = "Bill No : " & txtBillNo.Text & mNarration
                    End If
                Else
                    If chkFOC.CheckState = System.Windows.Forms.CheckState.Checked Then
                        mNarration = "Bill No : " & mAlias & txtBillNo.Text & " (Cancelled)"
                    Else
                        mNarration = "Bill No : " & mAlias & txtBillNo.Text & mNarration
                    End If
                End If

                mSubRowNo = 0      'IIf(IsDBNull(RsTemp.Fields("ISGSTAPPLICABLE").Value), "G", RsTemp.Fields("ISGSTAPPLICABLE").Value)
                mVNo = IIf(IsDBNull(RsTemp.Fields("VNO").Value), "", RsTemp.Fields("VNO").Value)
                pDueDate = txtPaymentdate.Text
                mLocal = "N"
                mLocal = GetPartyBusinessDetail(Trim(mSuppCustCode), Trim(txtBillTo.Text), "WITHIN_STATE")

                mItemValue = IIf(IsDBNull(RsTemp.Fields("ITEMVALUE").Value), 0, RsTemp.Fields("ITEMVALUE").Value) '' Val(lblNetAmount.Text)

                mDivisionCode = IIf(IsDBNull(RsTemp.Fields("DIV_CODE").Value), "G", RsTemp.Fields("DIV_CODE").Value)
                If UpdatePaymentDetail1(PubDBCn, (LblMKey.Text), mCurRowNo, (LblBookCode.Text), mBookType, mBookSubType, mVNo, (txtVDate.Text), (txtBillNo.Text), (txtBillDate.Text), mTRNType, mSuppCustCode, mAccountCode, Val(CStr(mItemValue)), Val(lblNetAmount.Text), IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, True, False), IIf(chkFOC.CheckState = System.Windows.Forms.CheckState.Checked, True, False), pDueDate, VB.Left(mNarration, 254), (txtRemarks.Text), mNetExpAmount, IIf(mIsGSTRefund = "G", "Y", "N"), Val(txtTotCGSTRefund.Text), Val(txtTotSGSTRefund.Text), Val(txtTotIGSTRefund.Text), (txtMRRDate.Text), ADDMode, mAddUser, mAddDate, mDivisionCode, IIf(mIsGSTRefund = "R", "Y", "N"), Val(lblTotCGSTAmount.Text) + Val(lblTotIGSTAmount.Text) + Val(lblTotSGSTAmount.Text), Val(lblTotCGSTAmount.Text), Val(lblTotSGSTAmount.Text), Val(lblTotIGSTAmount.Text), mSaleBillNo, mSaleBillDate, txtBillTo.Text) = False Then GoTo ErrPart
            End If
        End If
        MsgInformation("Payment Saved.")
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function CheckFullLotRejection() As Boolean
        On Error GoTo ERR1
        Dim mAcceptQty As Double

        Dim cntRow As Long

        CheckFullLotRejection = True

        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColAcceptedQty
                mAcceptQty = Val(.Text)

                If mAcceptQty > 0 Then
                    CheckFullLotRejection = False
                    Exit Function
                End If
            Next
        End With

        Exit Function
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function

End Class
