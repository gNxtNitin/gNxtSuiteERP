Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports AxFPSpreadADO
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinDataSource
'Imports Infragistics.Win.UltraWinTabControl
Imports System.Data.OleDb

Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Imports System.ComponentModel

Imports System.Drawing
Imports System.Drawing.Printing

Imports System.Data
Imports System.IO
Imports System.Configuration


Friend Class FrmMRR
    Inherits System.Windows.Forms.Form
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

    Dim RsMRRMain As ADODB.Recordset
    Dim RsMRRDetail As ADODB.Recordset
    Dim RsMRRExp As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection					
    Dim pQCDate As String
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim pXRIGHT As String
    Dim xMyMenu As String

    Dim FormActive As Boolean
    Dim mCurRowNo As Integer
    'Private JB As JsonBag						

    Dim mSupplierCode As String
    Dim pRound As Double
    Dim mWithOutOrder As Boolean
    Dim mIsProjectPO As Boolean

    Private Const mBookType As String = "G"
    Private Const mBookSubType As String = "R"

    Private Const ConRowHeight As Short = 12

    Private Const ColPONo As Short = 1
    Private Const ColPODate As Short = 2
    Private Const ColRGPItemCode As Short = 3
    Private Const ColItemCode As Short = 4
    Private Const ColItemName As Short = 5
    Private Const ColItemDesc As Short = 6
    Private Const ColHSNCode As Short = 7
    Private Const ColHeatNo As Short = 8
    Private Const ColBatchNo As Short = 9
    Private Const ColUnit As Short = 10
    Private Const ColPOQty As Short = 11
    Private Const ColBalQty As Short = 12
    Private Const ColBillQty As Short = 13
    Private Const ColReceivedQty As Short = 14
    Private Const ColAcceptQty As Short = 15
    Private Const ColApprovedQty As Short = 16
    Private Const ColShortQty As Short = 17
    Private Const ColRejQty As Short = 18
    Private Const ColDevQty As Short = 19
    Private Const ColSeg As Short = 20
    Private Const ColRework As Short = 21
    Private Const ColConvQty As Short = 22
    Private Const ColStockType As Short = 23
    Private Const ColPORate = 24
    Private Const ColRate As Short = 25
    Private Const ColAmount As Short = 26
    Private Const ColItemCost As Short = 27
    Private Const ColQCEMP As Short = 28
    Private Const ColCT3No As Short = 29
    Private Const ColPCNo As Short = 30
    Private Const ColQtyInKgs As Short = 31
    Private Const ColRecdQtyInKgs As Short = 32
    Private Const ColRemarks As Short = 33
    Private Const ColPDIRFlag As Short = 34
    Private Const ColSchdRtnFlag As Short = 35
    Private Const ColQCDate As Short = 36


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

    Dim pDnCnNo As String
    Dim mDNCnNO As Integer

    Dim pShowCalc As Boolean
    Dim pTempUpdate As Boolean
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mAuthorised As Boolean

    Private Function GetAutoIssueFromIndent(ByVal mPONo As String, ByVal mItemCode As String, ByVal mCheckedField As String, Optional ByVal mDeptCode As String = "") As String

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mIndentNo As Double

        GetAutoIssueFromIndent = "N"
        SqlStr = " SELECT AUTO_KEY_INDENT " & vbCrLf & " FROM PUR_POCONS_IND_TRN " & vbCrLf & " WHERE AUTO_KEY_PO = " & Val(mPONo) & "" & vbCrLf & " AND ITEM_CODE ='" & MainClass.AllowSingleQuote(mItemCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = True Then
            GetAutoIssueFromIndent = "N"
        Else
            mIndentNo = Val(IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_INDENT").Value), 0, RsTemp.Fields("AUTO_KEY_INDENT").Value))
            If MainClass.ValidateWithMasterTable(mIndentNo, "AUTO_KEY_INDENT", mCheckedField, "PUR_INDENT_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & mCheckedField & "='Y'") = True Then
                GetAutoIssueFromIndent = "Y"
            End If
            If MainClass.ValidateWithMasterTable(mIndentNo, "AUTO_KEY_INDENT", "DEPT_CODE", "PUR_INDENT_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & mCheckedField & "='Y'") = True Then
                mDeptCode = Trim(MasterNo)
            End If
        End If

        Exit Function
ErrPart:
        GetAutoIssueFromIndent = "N"
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function ValidatePO(ByVal mPONo As String, ByVal mDivisionCode As Double) As Boolean

        On Error GoTo ERR1
        Dim RS As ADODB.Recordset = Nothing
        Dim mRefType As String
        Dim xPoNo As String
        Dim SqlStr As String = ""
        Dim ErrMsg As String = ""

        If mPONo = "" Then Exit Function
        ValidatePO = True
        SqlStr = ""
        mRefType = VB.Left(cboRefType.Text, 1)

        If mRefType = "F" Or mRefType = "C" Then Exit Function

        If Trim(TxtSupplier.Text) = "" Then
            mSupplierCode = "-1"
        ElseIf MainClass.ValidateWithMasterTable((TxtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSupplierCode = MasterNo
        End If

        If mRefType = "P" Or mRefType = "J" Or mRefType = "1" Then
            SqlStr = "SELECT AUTO_KEY_PO,PO_STATUS AS CLOSED,SUPP_CUST_CODE,DIV_CODE  from PUR_PURCHASE_HDR WHERE " & vbCrLf & " AUTO_KEY_PO=" & Val(mPONo) & "" & vbCrLf & " AND Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND PO_STATUS='Y'" ''& vbCrLf |                & " AND SUBSTR(AUTO_KEY_PO,LENGTH(AUTO_KEY_PO)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""			

        ElseIf mRefType = "I" Or mRefType = "2" Or mRefType = "3" Then
            SqlStr = ""
        ElseIf mRefType = "R" Then
            SqlStr = ""
        End If

        If CDbl(mSupplierCode) <> -1 Then
            SqlStr = SqlStr & vbCrLf & " AND TRIM(SUPP_CUST_CODE)='" & MainClass.AllowSingleQuote(mSupplierCode) & "'"
        End If

        '    SqlStr = SqlStr & vbCrLf & " AND DIV_CODE=" & mDivisionCode & ""				

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        If RS.EOF = True Then
            ValidatePO = False
            If CDbl(mSupplierCode) = -1 Then MsgInformation("Either No Such PONo.(s) Or " & ErrMsg & " , Press F1 On PoNo.(s) For Help... ")
            If CDbl(mSupplierCode) <> -1 Then MsgInformation("PO No(s) Not Belong to Same Supplier")
        Else

            If RS.Fields("CLOSED").Value = "N" Then ValidatePO = False : MsgInformation("This PO Marked As Close Order, So Can Not Be Used For Further Transaction.")
            mSupplierCode = IIf(IsDBNull(RS.Fields("SUPP_CUST_CODE").Value), "", RS.Fields("SUPP_CUST_CODE").Value)
            mDivisionCode = IIf(IsDBNull(RS.Fields("DIV_CODE").Value), -1, RS.Fields("DIV_CODE").Value)
        End If
        Exit Function
ERR1:
        ValidatePO = False
        MsgBox(Err.Description)
    End Function
    Private Function ValidateRGP(ByVal mPONo As String) As Boolean

        On Error GoTo ERR1
        Dim RS As ADODB.Recordset = Nothing
        Dim mRefType As String
        Dim xPoNo As String
        Dim SqlStr As String = ""
        Dim ErrMsg As String = ""

        If mPONo = "" Then Exit Function
        ValidateRGP = True
        SqlStr = ""

        If Trim(TxtSupplier.Text) = "" Then
            mSupplierCode = "-1"
        ElseIf MainClass.ValidateWithMasterTable((TxtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSupplierCode = MasterNo
        End If


        SqlStr = "SELECT AUTO_KEY_PASSNO,GATEPASS_STATUS AS CLOSED,SUPP_CUST_CODE  from INV_GATEPASS_HDR WHERE " & vbCrLf & " AUTO_KEY_PASSNO=" & Val(mPONo) & "" & vbCrLf & " AND Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " ''& vbCrLf |            & " AND SUBSTR(AUTO_KEY_PASSNO,LENGTH(AUTO_KEY_PASSNO)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""				


        If CDbl(mSupplierCode) <> -1 Then
            SqlStr = SqlStr & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSupplierCode) & "'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        If RS.EOF = True Then
            ValidateRGP = False
            If CDbl(mSupplierCode) = -1 Then MsgInformation("Either No Such RGP No.(s) Or " & ErrMsg & " , Press F1 On PoNo.(s) For Help... ")
            If CDbl(mSupplierCode) <> -1 Then MsgInformation("RGP No(s) Not Belong to Same Supplier")
        Else

            If RS.Fields("CLOSED").Value = "Y" Then ValidateRGP = False : MsgInformation("This RGP had been Completed, So Can Not Be Used For Further Transaction.")
            If RS.Fields("CLOSED").Value = "C" Then ValidateRGP = False : MsgInformation("This RGP Marked As Closed, So Can Not Be Used For Further Transaction.")

            mSupplierCode = RS.Fields("SUPP_CUST_CODE").Value
        End If
        Exit Function
ERR1:
        ValidateRGP = False
        MsgBox(Err.Description)
    End Function
    Private Function ValidateInvoice(ByVal mPONo As String) As Boolean

        On Error GoTo ERR1
        Dim RS As ADODB.Recordset = Nothing
        Dim mRefType As String
        Dim xPoNo As String
        Dim SqlStr As String = ""
        Dim ErrMsg As String = ""

        If mPONo = "" Then Exit Function
        ValidateInvoice = True
        SqlStr = ""

        If Trim(TxtSupplier.Text) = "" Then
            mSupplierCode = "-1"
        ElseIf MainClass.ValidateWithMasterTable((TxtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSupplierCode = MasterNo
        End If


        SqlStr = "SELECT AUTO_KEY_INVOICE,CANCELLED AS CLOSED,SUPP_CUST_CODE" & vbCrLf & " FROM FIN_INVOICE_HDR WHERE " & vbCrLf & " AUTO_KEY_INVOICE='" & (mPONo) & "'" & vbCrLf & " AND Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " '' & vbCrLf |            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""				


        If CDbl(mSupplierCode) <> -1 Then
            SqlStr = SqlStr & vbCrLf & " AND (SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSupplierCode) & "' OR BUYER_CODE='" & MainClass.AllowSingleQuote(mSupplierCode) & "' OR CO_BUYER_CODE='" & MainClass.AllowSingleQuote(mSupplierCode) & "')"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        If RS.EOF = True Then
            ValidateInvoice = False
            If CDbl(mSupplierCode) = -1 Then MsgInformation("Either No Such Invoice No.(s) Or " & ErrMsg & " , Press F1 On PoNo.(s) For Help... ")
            If CDbl(mSupplierCode) <> -1 Then MsgInformation("Invoice No(s) Not Belong to Same Supplier")
        Else

            If RS.Fields("CLOSED").Value = "Y" Then ValidateInvoice = False : MsgInformation("This Invoice Marked As Cancelled, So Can Not Be Used For Further Transaction.")
            mSupplierCode = RS.Fields("SUPP_CUST_CODE").Value
        End If
        Exit Function
ERR1:
        ValidateInvoice = False
        MsgBox(Err.Description)
    End Function
    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged

        '    MainClass.ButtonStatus Me, XRIGHT, RsMRRMain, ADDMode, MODIFYMode, True				
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub cboMode_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboMode.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboRefType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboRefType.TextChanged

        If VB.Left(cboRefType.Text, 1) = "F" Or VB.Left(cboRefType.Text, 1) = "C" Or VB.Left(cboRefType.Text, 1) = "J" Then ''Or Left(cboRefType, 1) = "1"				
            mWithOutOrder = True
            CboPONo.Enabled = False
        Else
            mWithOutOrder = False
            CboPONo.Enabled = True
        End If
        FormatSprdMain(-1)
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboRefType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboRefType.SelectedIndexChanged

        If VB.Left(cboRefType.Text, 1) = "F" Or VB.Left(cboRefType.Text, 1) = "C" Or VB.Left(cboRefType.Text, 1) = "J" Then ''Or Left(cboRefType, 1) = "1"				
            mWithOutOrder = True
            CboPONo.Enabled = False
        Else
            mWithOutOrder = False
            CboPONo.Enabled = True
        End If

        '    MainClass.ClearGrid SprdMain				
        '    Call FormatSprdMain(-1)				
        '    MainClass.ClearGrid SprdExp				
        '    Call FillSprdExp				
        MainClass.ButtonStatus(Me, XRIGHT, RsMRRMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

    End Sub

    Private Sub cboRefType_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles cboRefType.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        pTempUpdate = False
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub chkCancelled_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCancelled.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub CheckPORate()
        'Dim mCntRow As Long				
        'Dim SqlStr As String = ""				
        'Dim RsTemp As ADODB.Recordset=Nothing				
        'Dim mITEM_CODE As String				
        'Dim mTaxableRate As Double				
        'Dim mSTPer As Double				
        'Dim mEDPer As Double				
        '				
        '    If Val(lblEDPercentage) <> 0 Then				
        '        mEDPer = Val(lblEDPercentage)				
        '    Else				
        '        mEDPer = Val(lblTotED) * 100 / IIf(Val(lblTotItemValue) = 0, 1, Val(lblTotItemValue))				
        '    End If				
        '				
        '    If Val(lblSTPercentage) <> 0 Then				
        '        mSTPer = Val(lblSTPercentage)				
        '    Else				
        '        mSTPer = Val(lblTotST) * 100 / IIf(Val(lblTotTaxableAmt) = 0, 1, Val(lblTotTaxableAmt))				
        '    End If				
        '				
        '    With SprdMain				
        '        For mCntRow = 1 To .MaxRows - 1				
        '            .Row = mCntRow				
        '            .Col = ColItemCode				
        '            mITEM_CODE = Trim(.Text)				
        '				
        '            SqlStr = "SELECT GetITEMPRICE(TO_DATE(TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))," & Val(txtPONo.Text) & ",'" & mITEM_CODE & "') AS PORATE  FROM DUAL"				
        '            MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly				
        '				
        '            If RsTemp.EOF = False Then				
        '                .Col = ColPORate				
        '                .Text = Val(IIf(IsNull(RsTemp!PORATE), 0, RsTemp!PORATE))				
        '            End If				
        '        Next				
        '    End With				
        '				
        '    With SprdMain				
        '        For mCntRow = 1 To .MaxRows - 1				
        '            .Row = mCntRow				
        '            .Col = ColPORate				
        '            If chkIncludingED.Value = vbChecked Then				
        ''                mTaxableRate = Val(.Text) - (Val(.Text) * Val(mEDPer) * 0.01)				
        '                mTaxableRate = (Val(.Text) * 100) / (Val(mEDPer) + 100)				
        '            Else				
        '                mTaxableRate = Val(.Text)				
        '            End If				
        '				
        '            If chkSTInclude.Value = vbChecked Then				
        ''                .Text = Val(mTaxableRate) - (Val(mTaxableRate) * Val(mSTPer) * 0.01)				
        '                .Text = (Val(mTaxableRate) * 100) / (Val(mSTPer) + 100)				
        '            Else				
        '                .Text = mTaxableRate				
        '            End If				
        '        Next				
        '    End With				


    End Sub

    Private Sub chkQC_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkQC.CheckStateChanged


        Dim cntRow As Long
        Dim mInterUnit As String = "N"

        mInterUnit = "N"
        If MainClass.ValidateWithMasterTable((TxtSupplier.Text), "SUPP_CUST_NAME", "INTER_UNIT", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mInterUnit = MasterNo
        End If

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)

        If chkQC.Enabled = False Then Exit Sub

        If chkQC.CheckState = System.Windows.Forms.CheckState.Checked Then
            With SprdMain
                For cntRow = 1 To .MaxRows - 1
                    .Row = cntRow
                    .Col = ColStockType

                    If VB.Left(cboRefType.Text, 1) = "P" Then
                        .Text = IIf(Trim(.Text) = "QC", "ST", .Text)
                    ElseIf VB.Left(cboRefType.Text, 1) = "J" Then
                        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 And mInterUnit = "Y" Then
                            .Text = IIf(Trim(.Text) = "QC", "ST", .Text)
                        Else
                            .Text = IIf(Trim(.Text) = "QC", "CS", .Text)
                        End If

                    ElseIf VB.Left(cboRefType.Text, 1) = "3" Then
                        .Text = IIf(Trim(.Text) = "QC", "ST", .Text)
                    ElseIf VB.Left(cboRefType.Text, 1) = "I" Then
                        .Text = IIf(Trim(.Text) = "QC", "CR", .Text)
                    Else
                        .Text = IIf(Trim(.Text) = "QC", "ST", .Text)
                    End If
                Next
            End With
        Else
            With SprdMain
                For cntRow = 1 To .MaxRows - 1
                    .Row = cntRow
                    .Col = ColStockType
                    .Text = "QC"
                Next
            End With
        End If
    End Sub

    Private Sub chkUnderChallan_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkUnderChallan.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click

        On Error GoTo AddErr
        If cmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            pShowCalc = True
            SprdMain.Enabled = True
            SprdExp.Enabled = True
            txtMRRNo.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)
            txtMRRDate.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)
            cmdMRRSearch.Enabled = False
            If cboRefType.Enabled = True Then cboRefType.Focus()
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

        If ValidateBranchLocking((txtBillDate.Text)) = True Then
            Exit Sub
        End If

        If lblBookType.Text = "Q" Then
            mLockBookCode = CInt(ConLockMRRQC)
        Else
            mLockBookCode = CInt(ConLockMRREntry)
        End If

        If ValidateBookLocking(PubDBCn, mLockBookCode, txtMRRDate.Text) = True Then
            Exit Sub
        End If

        If ValidateAccountLocking(PubDBCn, txtBillDate.Text, (TxtSupplier.Text), mSupplierCode) = True Then
            Exit Sub
        End If

        If lblBookType.Text = "Q" Then
            MsgInformation("You have no Rigths to Delete MRR.")
            Exit Sub
        End If

        '    GoTo delpart				

        If lblBookType.Text = "G" Then
            If chkQC.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("QC Made against this MRR, so Cann't be Deleted")
                Exit Sub
            End If
        End If



        If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Cancelled Bill Cann't be Modified")
            Exit Sub
        End If

        If chkBillPassing.CheckState = System.Windows.Forms.CheckState.Checked Or chkMrrSend.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Final Bill Post / Send To Account Cann't be Modified")
            Exit Sub
        End If

        If Trim(txtMRRNo.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub

        If chkBillPassing.CheckState = System.Windows.Forms.CheckState.Checked Or chkMrrSend.CheckState = System.Windows.Forms.CheckState.Checked Or chkQC.CheckState = System.Windows.Forms.CheckState.Checked Or chkExciseStatus.CheckState = System.Windows.Forms.CheckState.Checked Or chkGSTStatus.CheckState = System.Windows.Forms.CheckState.Checked Or chkSTStatus.CheckState = System.Windows.Forms.CheckState.Checked Or chkServiceTaxClaim.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Transaction Made Against This MRR So Cann't be Deleted")
            Exit Sub
        End If


        If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Cancelled Bill Cann't be Deleted.")
            Exit Sub
        End If

        If CheckBillPayment(mSupplierCode, (txtBillNo.Text), "B", (txtBillDate.Text)) = True Then Exit Sub
        'delpart:				
        If Not RsMRRMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.			
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "INV_GATE_HDR", (txtMRRNo.Text), RsMRRMain, "MRRNo") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "INV_GATE_HDR", "AUTO_KEY_MRR", (LblMkey.Text)) = False Then GoTo DelErrPart

                If DeleteStockTRN(PubDBCn, ConStockRefType_MRR, (txtMRRNo.Text)) = False Then GoTo DelErrPart
                If DeletePaintStockTRN(PubDBCn, ConStockRefType_MRR, (txtMRRNo.Text)) = False Then GoTo DelErrPart

                'PubDBCn.Execute("DELETE FROM INV_RGP_REG_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND REF_NO='" & LblMkey.Text & "'  AND BOOKTYPE='M' AND ITEM_IO='I'")

                PubDBCn.Execute("DELETE FROM INV_RGP_REG_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND BILL_NO='" & txtBillNo.Text & "' AND  BILL_DATE =TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND  SUPP_CUST_CODE='" & mSupplierCode & "'  AND BOOKTYPE='M' AND ITEM_IO='I'")

                PubDBCn.Execute("Delete From FIN_CT_TRN Where Mkey='" & LblMkey.Text & "' AND BOOKTYPE='P' AND BOOKSUBTYPE='I'")
                PubDBCn.Execute("Delete From FIN_PC_TRN Where Mkey='" & LblMkey.Text & "' AND BOOKTYPE='P' AND BOOKSUBTYPE='I'")
                PubDBCn.Execute("Delete from INV_GATE_EXP Where Mkey='" & LblMkey.Text & "'")
                PubDBCn.Execute("Delete from INV_GATE_DET Where AUTO_KEY_MRR=" & Val(LblMkey.Text) & "")
                PubDBCn.Execute("Delete from INV_GATE_HDR Where AUTO_KEY_MRR=" & Val(LblMkey.Text) & "")

                If RsCompany.Fields("MRR_AGT_GE").Value = "Y" Then
                    PubDBCn.Execute("UPDATE INV_GATEENTRY_HDR SET MRR_MADE='N', MRR_NO='',MRRDATE='' WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AUTO_KEY_GATE ='" & MainClass.AllowSingleQuote((txtGateNo.Text)) & "'")
                End If

                PubDBCn.CommitTrans()
                RsMRRMain.Requery() ''.Refresh		
                RsMRRDetail.Requery() ''.Refresh		
                RsMRRExp.Requery() ''.Refresh		
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans() ''				
        RsMRRMain.Requery() ''.Refresh				
        RsMRRDetail.Requery() ''.Refresh				
        RsMRRExp.Requery() ''.Refresh				
        If Err.Description <> "" Then
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        '        Resume				
    End Sub

    Private Sub cmdDetail_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDetail.Click
        FraDetail.Visible = Not FraDetail.Visible
    End Sub
    Private Sub cmdGateSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdGateSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        If ADDMode = False Then Exit Sub

        SqlStr = " SELECT AUTO_KEY_GATE, GATE_DATE, IH.SUPP_CUST_CODE, SUPP_CUST_NAME,BILL_NO,BILL_DATE" & vbCrLf _
                & " FROM INV_GATEENTRY_HDR IH, FIN_SUPP_CUST_MST CMST" & vbCrLf _
                & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND SUBSTR(AUTO_KEY_GATE,LENGTH(AUTO_KEY_GATE)-5,4)=" & RsCompany.Fields("FYEAR").Value & " AND MRR_MADE='N'" & vbCrLf _
                & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
                & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE ORDER BY GATE_DATE DESC;"

        'If MainClass.SearchGridMaster((txtGateNo.Text), "INV_GATEENTRY_HDR", "AUTO_KEY_GATE", "GATE_DATE", "SUPP_CUST_CODE", , SqlStr) = True Then
        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtGateNo.Text = AcName
            txtGateNo_Validating(txtGateNo, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdModify.Click

        On Error GoTo ModifyErr


        If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Cancelled Bill Cann't be Modified")
            Exit Sub
        End If

        If PubUserID <> "G0416" Then
            If chkQC.CheckState = System.Windows.Forms.CheckState.Checked And chkQC.Enabled = False Then
                If chkBillPassing.CheckState = System.Windows.Forms.CheckState.Checked Or chkMrrSend.CheckState = System.Windows.Forms.CheckState.Checked Or chkExciseStatus.CheckState = System.Windows.Forms.CheckState.Checked Or chkGSTStatus.CheckState = System.Windows.Forms.CheckState.Checked Or chkSTStatus.CheckState = System.Windows.Forms.CheckState.Checked Then
                    MsgInformation("Final Bill Post / Send To Account Cann't be Modified")
                    Exit Sub
                End If
            End If

            '        If lblBookType.text = "G" Then			
            If PubSuperUser = "S" Then

            Else
                If chkQC.CheckState = System.Windows.Forms.CheckState.Checked And chkQC.Enabled = False Then
                    MsgInformation("QC Made agt this MRR, so Cann't be Modified")
                    Exit Sub
                Else
                    If InStr(1, pXRIGHT, "S") > 0 Or VB.Left(cboRefType.Text, 1) = "J" Then
                        TxtSupplier.Enabled = True
                        cmdsearch.Enabled = True
                        cboRefType.Enabled = True
                        cboDivision.Enabled = True
                        Frame1.Enabled = True
                    End If
                End If
            End If
            '        End If			
        End If


        If PubSuperUser = "S" Then
            TxtSupplier.Enabled = True
            cmdsearch.Enabled = True
            cboRefType.Enabled = True
            cboDivision.Enabled = False
            Frame1.Enabled = True
            txtMRRDate.Enabled = True
        End If


        If cmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsMRRMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
            SprdExp.Enabled = True
            txtMRRNo.Enabled = False
            cmdMRRSearch.Enabled = False
            pShowCalc = True
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

    Private Sub cmdMRRSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdMRRSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        If lblBookType.Text = "G" Then Exit Sub

        'SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUBSTR(AUTO_KEY_MRR,LENGTH(AUTO_KEY_MRR)-5,4)=" & RsCompany.Fields("FYEAR").Value & " AND QC_STATUS='N' AND MRR_STATUS='N'"

        'If MainClass.SearchGridMaster((txtMRRNo.Text), "INV_GATE_HDR", "AUTO_KEY_MRR", "MRR_DATE", "SUPP_CUST_CODE", , SqlStr) = True Then

        SqlStr = " SELECT AUTO_KEY_MRR, MRR_DATE, IH.SUPP_CUST_CODE, SUPP_CUST_NAME,BILL_NO,BILL_DATE" & vbCrLf _
                & " FROM INV_GATE_HDR IH, FIN_SUPP_CUST_MST CMST" & vbCrLf _
                & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND SUBSTR(AUTO_KEY_MRR,LENGTH(AUTO_KEY_MRR)-5,4)=" & RsCompany.Fields("FYEAR").Value & " AND QC_STATUS='N' AND MRR_STATUS='N'" & vbCrLf _
                & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
                & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE"

        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtMRRNo.Text = AcName
            TxtMRRNo_Validating(txtMRRNo, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportONMRR(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportONMRR(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportONMRR(ByVal Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""
        Dim SqlStr As String = ""
        Dim mRptFileName As String
        Dim mVNo As String


        frmPrintMRR.ShowDialog()

        If G_PrintLedg = False Then
            Exit Sub
        End If

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        SqlStr = ""

        Call MainClass.ClearCRptFormulas(Report1)

        Call SelectQryForMRR(SqlStr)

        If frmPrintMRR.optMRR.Checked = True Then
            mTitle = "Material Receipt Report"
            mSubTitle = ""

            mRptFileName = "MRR.rpt"
        Else
            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Then

                If FillPrintDummyDataForLabel() = False Then GoTo ERR1

                mTitle = "Material Gate Entry"
                mSubTitle = ""
                mRptFileName = "MRRLABEL.rpt"
                Call ShowMRRLabelReport("", Mode, mTitle, mSubTitle, mRptFileName, False, False, "N")
                frmPrintMRR.Hide()
                frmPrintMRR.Close()
                frmPrintMRR.Dispose()
                Exit Sub
            Else
                mTitle = "Identification Tag"
                mSubTitle = ""

                mRptFileName = "MRRTag.rpt"
            End If

        End If



        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)

        frmPrintMRR.Hide()
        frmPrintMRR.Close()
        frmPrintMRR.Dispose()
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        frmPrintMRR.Hide()
        frmPrintMRR.Close()
        frmPrintMRR.Dispose()
    End Sub
    Private Sub ShowMRRLabelReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String, ByRef IsSubReport As Boolean, ByVal mPDF As Boolean, mPrePrint As String)

        On Error GoTo ErrPart
        Dim RsTemp As New ADODB.Recordset
        Dim RS As New ADODB.Recordset

        Dim fPath As String

        Dim SqlStrSub As String
        Dim SqlStr As String = ""


        Dim CrReport As New CrystalDecisions.CrystalReports.Engine.ReportDocument  ' Report Name 
        Dim CrExportOptions As ExportOptions
        Dim CrDiskFileDestinationOptions As New DiskFileDestinationOptions

        Dim CrFormatTypeOptions As New PdfRtfWordFormatOptions

        mRptFileName = PubReportFolderPath & mRptFileName

        CrReport.Load(mRptFileName)

        Call Connect_MainReport_To_Database_11(CrReport)        '', mSqlStr

        'CrReport.RecordSelectionFormula = "{IH.COMPANY_CODE} = " & RsCompany.Fields("COMPANY_CODE").Value & " AND {IH.AUTO_KEY_MRR} = " & Val(txtMRRNo.Text) & ""

        CrReport.RecordSelectionFormula = "{PRINTDUMMYDATA.USERID} = '" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"

        ClearCRpt11Formulas(CrReport)
        CrReport.ReportOptions.EnableSaveDataWithReport = False
        SetCompanyReport11(CrReport, 1, mTitle, mSubTitle)
        'CrReport.VerifyDatabase()   .VerifyOnEveryPrint = True '' blnVerifyOnEveryPrint		
        CrReport.Refresh()



        If mPDF = True Then
            'Dim pOutPutFileName As String = ""

            'fPath = mPubBarCodePath & "\TaxInvoice_" & RsCompany.Fields("COMPANY_CODE").Value & mBillNoStr & ".pdf"
            'pOutPutFileName = mPubBarCodePath & "\TaxInvoice_DS_" & RsCompany.Fields("COMPANY_CODE").Value & mBillNoStr & ".pdf"

            ''FrmInvoiceViewer.CrystalReportViewer1.ReportSource = Nothing
            ''FrmInvoiceViewer.CrystalReportViewer1.ReportSource = CrReport
            ''FrmInvoiceViewer.CrystalReportViewer1.Show()

            'CrDiskFileDestinationOptions.DiskFileName = fPath
            'CrExportOptions = CrReport.ExportOptions

            'With CrExportOptions
            '    .ExportDestinationType = ExportDestinationType.DiskFile
            '    .ExportFormatType = ExportFormatType.PortableDocFormat
            '    .DestinationOptions = CrDiskFileDestinationOptions
            '    .FormatOptions = CrFormatTypeOptions
            'End With
            'CrReport.Export()

            'If FILEExists(fPath) Then
            '    If frmPrintInvCopy.optShow(1).Checked = True Then
            '        Process.Start("explorer.exe", fPath)
            '    End If
            'End If

            'If frmPrintInvCopy.optShow(2).Checked = True Then

            '    ''My test

            '    'Dim mSignerName As String
            '    Dim mPrintDigitalSign As String
            '    mPrintDigitalSign = "Authorised Signatory" ''"For " & RsCompany.Fields("PRINT_COMPANY_NAME").Value  ''"Authorised Signatory"
            '    'mSignerName = GetDigitalSignName(PubUserID)
            '    'If mSignerName <> "" Then
            '    If SignPdf(fPath, pOutPutFileName, mPrintDigitalSign) = False Then Exit Sub

            '    If FILEExists(pOutPutFileName) Then
            '        Process.Start("explorer.exe", pOutPutFileName)
            '    End If
            '    'End If
            'End If
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
    Private Function FillPrintDummyDataForLabel() As Boolean
        '' This procedure fills the Grid Data into PrintDummy table for printing...
        On Error GoTo PrintDummyErr

        Dim RSPrintDummy As ADODB.Recordset
        Dim FieldCnt As Short
        Dim RowNum As Short
        Dim FieldNum As Short
        Dim SqlStr As String = ""

        Dim mItemName As String
        Dim mItemCode As String
        Dim mBillQty As Double
        Dim mLOTNo As Double
        Dim mAcceptedQty As Double
        Dim mRejectedQty As Double
        Dim mLOTQty As Double
        Dim I As Long

        Dim mBalAcceptedQty As Double
        Dim mBalRejQty As Double
        Dim mBalBillQty As Double
        Dim mPackingQty As Double
        Dim xLotNo As String

        PubDBCn.Errors.Clear()

        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_PrintDummyData WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        FieldCnt = 0
        With SprdMain
            For RowNum = 1 To .MaxRows - 1
                .Row = RowNum
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColItemName
                mItemName = Trim(.Text)

                .Col = ColBillQty
                mBillQty = Val(.Text)

                mPackingQty = 1

                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "PACK_STD", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mPackingQty = Val(MasterNo)
                End If

                mPackingQty = IIf(mPackingQty <= 1, mBillQty, mPackingQty)

                ''mPackingQty = IIf(mPackingQty = 1, 1, mPackingQty)

                mLOTNo = mBillQty / mPackingQty
                mLOTNo = If(mLOTNo > Int(mLOTNo), Int(mLOTNo) + 1, mLOTNo)

                .Col = ColAcceptQty
                mAcceptedQty = Val(.Text)

                .Col = ColRejQty
                mRejectedQty = Val(.Text)

                mBalBillQty = mBillQty
                mBalAcceptedQty = mAcceptedQty
                mBalRejQty = mRejectedQty

                For I = 1 To mLOTNo

                    'mBillQty = IIf(mBalBillQty > mPackingQty, mPackingQty, mBalBillQty)
                    mAcceptedQty = IIf(mBalAcceptedQty > mPackingQty, mPackingQty, mBalAcceptedQty)
                    mRejectedQty = IIf(mBalRejQty > mPackingQty, mPackingQty, mBalRejQty)

                    'mBalBillQty = IIf(mBalBillQty - mBillQty > 0, mBalBillQty - mBillQty, 0)
                    mBalAcceptedQty = IIf(mBalAcceptedQty - mAcceptedQty > 0, mBalAcceptedQty - mAcceptedQty, 0)
                    mBalRejQty = IIf(mBalRejQty - mRejectedQty > 0, mBalRejQty - mRejectedQty, 0)

                    xLotNo = I & "/" & mLOTNo

                    FieldCnt = FieldCnt + 1

                    SqlStr = " INSERT INTO TEMP_PRINTDUMMYDATA (USERID, SUBROW, " & vbCrLf _
                        & " FIELD1, FIELD2,FIELD3,FIELD4,FIELD5,FIELD6,FIELD7,FIELD8,FIELD9,FIELD10,FIELD11) " & vbCrLf _
                        & " VALUES (" & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & FieldCnt & ", " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(TxtSupplier.Text) & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mItemName) & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mItemCode) & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(txtBillNo.Text) & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(txtBillDate.Text) & "'," & vbCrLf _
                        & " '" & mBillQty & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(xLotNo) & "'," & vbCrLf _
                        & " '" & mAcceptedQty & "'," & vbCrLf _
                        & " '" & mRejectedQty & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(txtMRRNo.Text) & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(txtMRRDate.Text) & "'" & vbCrLf _
                        & " ) "

                    PubDBCn.Execute(SqlStr)
                Next
            Next
        End With


        PubDBCn.CommitTrans()
        FillPrintDummyDataForLabel = True

        Exit Function
PrintDummyErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        FillPrintDummyDataForLabel = False
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Sub ReportONDiscrepancy(ByVal Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""
        Dim SqlStr As String = ""
        Dim mRptFileName As String
        Dim mVNo As String

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        SqlStr = ""

        Call MainClass.ClearCRptFormulas(Report1)

        Call SelectQryForDiscrepancy(SqlStr)


        mTitle = "Discrepancy Report"
        mSubTitle = ""
        mRptFileName = "Discrepancy.rpt"

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Function SelectQryForMRR(ByRef mSqlStr As String) As String

        ''SELECT CLAUSE...				

        mSqlStr = " SELECT " & vbCrLf & " IH.*, ID.*,INVMST.ITEM_SHORT_DESC, BCMST.*, PREBY.EMP_NAME"

        'mSqlStr = mSqlStr & " CMST.SUPP_CUST_NAME, CMST.SUPP_CUST_ADDR, " & vbCrLf _
        '    & " CMST.SUPP_CUST_CITY, CMST.SUPP_CUST_STATE, " & vbCrLf _
        '    & " CMST.SUPP_CUST_PIN, CMST.SUPP_CUST_PHONE, " & vbCrLf _
        '    & " CMST.SUPP_CUST_FAXNO, CMST.SUPP_CUST_MAILID, " & vbCrLf _
        '    & " CMST.SUPP_CUST_MOBILE, CMST.CST_NO, " & vbCrLf & " CMST.LST_NO, PREBY.EMP_NAME"

        ''FROM CLAUSE...				
        mSqlStr = mSqlStr & vbCrLf & " FROM INV_GATE_HDR IH, INV_GATE_DET ID, " & vbCrLf _
            & " FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_BUSINESS_MST BCMST, INV_ITEM_MST INVMST,PAY_EMPLOYEE_MST PREBY"

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
            mSqlStr = mSqlStr & vbCrLf & ", INV_GENERAL_MST GMST"
        End If

        ''WHERE CLAUSE...				
        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf _
            & " CMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " And INVMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " And IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " And IH.AUTO_KEY_MRR=ID.AUTO_KEY_MRR" & vbCrLf _
            & " And IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " And IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " And CMST.COMPANY_CODE=BCMST.COMPANY_CODE" & vbCrLf _
            & " And CMST.SUPP_CUST_CODE=BCMST.SUPP_CUST_CODE And BCMST.LOCATION_ID='" & Trim(txtBillTo.Text) & "'"

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then

            mSqlStr = mSqlStr & vbCrLf _
               & " AND IH.COMPANY_CODE=GMST.COMPANY_CODE" & vbCrLf _
               & " AND INVMST.CATEGORY_CODE=GMST.GEN_CODE AND GMST.GEN_TYPE='C'" & vbCrLf

        End If



        mSqlStr = mSqlStr & vbCrLf _
            & " AND ID.COMPANY_CODE=PREBY.COMPANY_CODE(+)" & vbCrLf _
            & " AND ID.QC_EMP_CODE=PREBY.EMP_CODE(+)" & vbCrLf _
            & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & "" ''& vbCrLf |            & " AND IH.QC_STATUS='Y'"				


        ''ORDER CLAUSE...				

        mSqlStr = mSqlStr & vbCrLf & "ORDER BY ID.SERIAL_NO"

        SelectQryForMRR = mSqlStr
    End Function


    Private Function SelectQryForDiscrepancy(ByVal mSqlStr As String) As String

        ''SELECT CLAUSE...				

        mSqlStr = " SELECT " & vbCrLf & " IH.*, ID.*,INVMST.ITEM_SHORT_DESC,"

        mSqlStr = mSqlStr & " CMST.SUPP_CUST_NAME, CMST.SUPP_CUST_ADDR, " & vbCrLf & " CMST.SUPP_CUST_CITY, CMST.SUPP_CUST_STATE, " & vbCrLf & " CMST.SUPP_CUST_PIN, CMST.SUPP_CUST_PHONE, " & vbCrLf & " CMST.SUPP_CUST_FAXNO, CMST.SUPP_CUST_MAILID, " & vbCrLf & " CMST.SUPP_CUST_MOBILE, CMST.CST_NO, " & vbCrLf & " CMST.LST_NO"

        ''FROM CLAUSE...				
        mSqlStr = mSqlStr & vbCrLf & " FROM INV_DESCRP_HDR IH, INV_DESCRP_DET ID, " & vbCrLf & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST, INV_GATE_HDR GATE "

        ''WHERE CLAUSE...				
        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf & " GATE.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND INVMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_DESCRP=ID.AUTO_KEY_DESCRP" & vbCrLf & " AND IH.COMPANY_CODE=GATE.COMPANY_CODE" & vbCrLf & " AND IH.AUTO_KEY_MRR=GATE.AUTO_KEY_MRR" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & ""


        ''ORDER CLAUSE...				

        mSqlStr = mSqlStr & vbCrLf & "ORDER BY ID.SERIAL_NO"

        SelectQryForDiscrepancy = mSqlStr
    End Function

    Private Sub cmdResetMRR_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdResetMRR.Click

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsResetGateMain As ADODB.Recordset = Nothing

        SqlStr = " SELECT * FROM INV_GATEENTRY_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_GATE,LENGTH(AUTO_KEY_GATE)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_GATE=" & Val(txtGateNo.Text) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsResetGateMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsResetGateMain.EOF = False Then
            Call ShowResetGateEntry(RsResetGateMain)
        Else
            MsgBox("No Such Gate Entry.", MsgBoxStyle.Information)
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler
        Dim mRefNo As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'If PubUserID <> "G0416" Then
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        'End If

        If VB.Left(cboRefType.Text, 1) = "I" And MODIFYMode = True Then
            If MainClass.ValidateWithMasterTable((txtMRRNo.Text), "AUTO_KEY_MRR", "AUTO_KEY_REF", "PRD_SALERETURN_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mRefNo = MasterNo
                MsgBox("QC Already done for this Rejection MRR, Agt Ref No : " & mRefNo & ", So cann't be Modify", MsgBoxStyle.Information)
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                Exit Sub
            End If
        End If

        Call CalcTots()
        pDnCnNo = ""

        If UpdateMain1() = True Then
            ADDMode = False
            MODIFYMode = False
            TxtMRRNo_Validating(txtMRRNo, New System.ComponentModel.CancelEventArgs(False))

            If cmdAdd.Enabled = True And cmdAdd.Visible = True Then cmdAdd.Focus()
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
    Private Sub cmdDispcrepancy_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDispcrepancy.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If chkDNote.CheckState = System.Windows.Forms.CheckState.Checked Then
            Call ReportONDiscrepancy(Crystal.DestinationConstants.crptToWindow)
        Else
            MsgInformation("Nothing to print.")
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub txtBillTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBillTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim xAcctCode As String

        If Trim(TxtSupplier.Text) = "" Then GoTo EventExitSub
        If Trim(txtBillTo.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(Trim(TxtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
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
    Private Sub cmdBillToSearch()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        Dim xAcctCode As String = ""

        If Trim(TxtSupplier.Text) = "" Then
            MsgInformation("Please select the Customer First")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(TxtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
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
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" ''& vbCrLf & " AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.SearchGridMaster((TxtSupplier.Text), "FIN_SUPP_CUST_BUSINESS_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", "LOCATION_ID", "SUPP_CUST_ADDR||SUPP_CUST_CITY", SqlStr) = True Then
            TxtSupplier.Text = AcName
            txtsupplier_Validating(TxtSupplier, New System.ComponentModel.CancelEventArgs(False))

            txtBillTo.Text = AcName2
            txtBillTo_Validating(txtBillTo, New System.ComponentModel.CancelEventArgs(False))

        End If

        'If ADDMode = True Then
        '    SqlStr = SqlStr & "  AND STATUS='O'"
        'End If

        'If MainClass.SearchGridMaster((TxtSupplier.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
        '    TxtSupplier.Text = AcName
        '    txtsupplier_Validating(TxtSupplier, New System.ComponentModel.CancelEventArgs(False))
        'End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        Dim mString As String = ""
        Dim mCheckString As String
        Dim mSeprator As String
        Dim mRefNo As String
        Dim CntRow As Integer
        Dim mPONo As String
        Dim mItemCode As String
        Dim mCustItemCode As String
        Dim mQty As Double
        Dim mRate As Double
        Dim mCheckPONO As String
        Dim mCheckItemCode As String
        Dim mSTID As String
        Dim mEDFlag As Boolean
        Dim mSTFlag As Boolean
        Dim mDiscountFlag As Boolean
        Dim mFreightFlag As Boolean
        Dim mBillDate As Date
        Dim mBarCodeNo As Integer

        Dim RsMisc As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mQCEmpCode As String
        Dim mDivisionCode As Double

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        mSeprator = "#"
        mString = Trim(txtScanning.Text)

        If mString = "" Then Exit Sub

        '    mString = Mid(mString, InStr(1, mString, mSeprator) + 1)				
        '    mCheckString = Mid(mString, 1, InStr(1, mString, mSeprator) - 1)				
        '    mBarCodeNo = Mid(mString, 1, InStr(1, mString, mSeprator) - 1)          ''mCheckString				
        '				
        '    If mBarCodeNo = 0 Then				
        '        Clear1				
        '    End If				

        '    mString = Mid(mString, InStr(1, mString, mSeprator) + 1)				
        mCheckString = Mid(mString, 1, InStr(1, mString, mSeprator) - 1)
        mSupplierCode = mCheckString ''Mid(mString, 1, InStr(1, mString, mSeprator) - 1)				
        If MainClass.ValidateWithMasterTable(mSupplierCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANy_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
            TxtSupplier.Text = MasterNo
            '    Else			
            '        MsgInformation "Invalid Supplier Name"			
            '        Exit Sub			
        End If

        txtsupplier_Validating(TxtSupplier, New System.ComponentModel.CancelEventArgs(True))

        mString = Mid(mString, InStr(1, mString, mSeprator) + 1)
        mCheckString = Mid(mString, 1, InStr(1, mString, mSeprator) - 1)
        mRefNo = mCheckString

        If mRefNo = "" Then
            MsgInformation("Invalid Ref. Type")
            Exit Sub
        End If

        If mCheckString = "P" Or mCheckString = "D" Then
            cboRefType.SelectedIndex = 0
        ElseIf mCheckString = "J" Then
            cboRefType.SelectedIndex = 1
        ElseIf mCheckString = "I" Then
            cboRefType.SelectedIndex = 2
        ElseIf mCheckString = "F" Then
            cboRefType.SelectedIndex = 3
        ElseIf mCheckString = "R" Then
            cboRefType.SelectedIndex = 4
        ElseIf mCheckString = "C" Then
            cboRefType.SelectedIndex = 5
        ElseIf mCheckString = "1" Then
            cboRefType.SelectedIndex = 6
        ElseIf mCheckString = "2" Then
            cboRefType.SelectedIndex = 7
        ElseIf mCheckString = "3" Then
            cboRefType.SelectedIndex = 8
        End If

        mString = Mid(mString, InStr(1, mString, mSeprator) + 1)
        mCheckString = Mid(mString, 1, InStr(1, mString, mSeprator) - 1)

        If cboRefType.SelectedIndex = 1 Then
            mPONo = "-1"
        Else
            If mRefNo = "D" Then
                mCheckString = GetPOFromDs(mCheckString)
                VB6.SetItemString(CboPONo, 0, mCheckString)
            Else
                VB6.SetItemString(CboPONo, 0, mCheckString)
            End If
            mPONo = mCheckString
        End If

        If mRefNo = "F" Then Exit Sub

        If mRefNo = "R" Then
            If ValidateRGP(mPONo) = False Then Exit Sub
        ElseIf mRefNo = "I" Or mRefNo = "1" Or mRefNo = "2" Or mRefNo = "3" Then
            If ValidateInvoice(mPONo) = False Then Exit Sub
        ElseIf mRefNo = "P" Then
            If ValidatePO(mPONo, mDivisionCode) = False Then Exit Sub
            '    Else			
            '        mPONo = mPONo & VB6.Format(RsCompany!FYNO, "00")			
        End If

        If MainClass.ValidateWithMasterTable(Val(CStr(mDivisionCode)), "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            cboDivision.Text = Trim(MasterNo)
        End If

        '    If MainClass.ValidateWithMasterTable(mSupplierCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANy_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then				
        '        txtSupplier.Text = MasterNo				
        '    Else				
        '        MsgInformation "Invalid Supplier Name"				
        '        Exit Sub				
        '    End If				

        mString = Mid(mString, InStr(1, mString, mSeprator) + 1)
        mCheckString = Mid(mString, 1, InStr(1, mString, mSeprator) - 1)
        txtBillNo.Text = UCase(Trim(mCheckString))

        mString = Mid(mString, InStr(1, mString, mSeprator) + 1)
        mCheckString = Mid(mString, 1, InStr(1, mString, mSeprator) - 1)
        If Not IsDate(mCheckString) Then
            MsgInformation("Invalid Bill Date")
            Exit Sub
        End If
        mCheckString = VB6.Format(UCase(mCheckString), "DD/MM/YYYY")
        txtBillDate.Text = VB6.Format(mCheckString, "DD/MM/YYYY")

        mString = Mid(mString, InStr(1, mString, mSeprator) + 1)
        mCheckString = Mid(mString, 1, InStr(1, mString, mSeprator) - 1)
        txtST38No.Text = UCase(Trim(mCheckString))

        mString = Mid(mString, InStr(1, mString, mSeprator) + 1)
        mCheckString = Mid(mString, 1, InStr(1, mString, mSeprator) - 1)
        TxtTransporter.Text = UCase(Trim(mCheckString))

        mString = Mid(mString, InStr(1, mString, mSeprator) + 1)
        mCheckString = Mid(mString, 1, InStr(1, mString, mSeprator) - 1)
        lblTotItemValue.Text = VB6.Format(Val(mCheckString), "0.00")

        mString = Mid(mString, InStr(1, mString, mSeprator) + 1)
        mCheckString = Mid(mString, 1, InStr(1, mString, mSeprator) - 1)
        lblEDPercentage.Text = VB6.Format(Val(mCheckString), "0.00")

        mString = Mid(mString, InStr(1, mString, mSeprator) + 1)
        mCheckString = Mid(mString, 1, InStr(1, mString, mSeprator) - 1)
        lblCGST.Text = VB6.Format(Val(mCheckString), "0.00")

        mString = Mid(mString, InStr(1, mString, mSeprator) + 1)
        mCheckString = Mid(mString, 1, InStr(1, mString, mSeprator) - 1)

        mString = Mid(mString, InStr(1, mString, mSeprator) + 1)
        mCheckString = Mid(mString, 1, InStr(1, mString, mSeprator) - 1)
        lblDiscount.Text = VB6.Format(Val(mCheckString), "0.00")

        mString = Mid(mString, InStr(1, mString, mSeprator) + 1)
        mCheckString = Mid(mString, 1, InStr(1, mString, mSeprator) - 1)
        lblTotTaxableAmt.Text = VB6.Format(Val(mCheckString), "0.00")

        mString = Mid(mString, InStr(1, mString, mSeprator) + 1)
        mCheckString = Mid(mString, 1, InStr(1, mString, mSeprator) - 1)
        lblSTPercentage.Text = VB6.Format(Val(mCheckString), "0.00")

        mString = Mid(mString, InStr(1, mString, mSeprator) + 1)
        mCheckString = Mid(mString, 1, InStr(1, mString, mSeprator) - 1)
        lblSGST.Text = VB6.Format(Val(mCheckString), "0.00")

        mString = Mid(mString, InStr(1, mString, mSeprator) + 1)
        mCheckString = Mid(mString, 1, InStr(1, mString, mSeprator) - 1)
        lblTotFreight.Text = VB6.Format(Val(mCheckString), "0.00")

        mString = Mid(mString, InStr(1, mString, mSeprator) + 1)
        mCheckString = Mid(mString, 1, InStr(1, mString, mSeprator) - 1)
        lblNetAmount.Text = VB6.Format(Val(mCheckString), "0.00")

        mString = Mid(mString, InStr(1, mString, mSeprator) + 1)
        mCheckString = Mid(mString, 1, InStr(1, mString, mSeprator) - 1)
        txtFreight.Text = UCase(Trim(mCheckString))

        mString = Mid(mString, InStr(1, mString, mSeprator) + 1)
        mCheckString = Mid(mString, 1, InStr(1, mString, mSeprator) - 1)
        txtFormDetail.Text = UCase(Trim(mCheckString))

        mString = Mid(mString, InStr(1, mString, mSeprator) + 1)
        mCheckString = Mid(mString, 1, InStr(1, mString, mSeprator) - 1)
        txtVehicle.Text = UCase(Trim(mCheckString))

        mString = Mid(mString, InStr(1, mString, mSeprator) + 1)
        mCheckString = Mid(mString, 1, InStr(1, mString, mSeprator) - 1)
        txtDocsThru.Text = UCase(Trim(mCheckString))

        CntRow = IIf(mBarCodeNo = 0, 1, SprdMain.MaxRows)

        Do While Len(mString) > 0
            With SprdMain
                If InStr(1, mString, mSeprator) = 0 Then
                    Exit Do
                End If

                mString = Mid(mString, InStr(1, mString, mSeprator) + 1)
                mCheckString = Mid(mString, 1, InStr(1, mString, mSeprator) - 1)
                mCustItemCode = UCase(Trim(mCheckString))

                If MainClass.ValidateWithMasterTable(mCustItemCode, "CUSTOMER_PART_NO", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mItemCode = MasterNo
                Else
                    mItemCode = Trim(mCustItemCode)
                End If

                mString = Mid(mString, InStr(1, mString, mSeprator) + 1)
                If InStr(1, mString, mSeprator) > 0 Then
                    mCheckString = Mid(mString, 1, InStr(1, mString, mSeprator) - 1)
                Else
                    mCheckString = mString
                End If
                mQty = Val(mCheckString)

                mString = Mid(mString, InStr(1, mString, mSeprator) + 1)
                If InStr(1, mString, mSeprator) > 0 Then
                    mCheckString = Mid(mString, 1, InStr(1, mString, mSeprator) - 1)
                Else
                    mCheckString = mString
                End If
                mRate = Val(mCheckString)

                .Row = CntRow
                .Col = ColPONo
                .Text = mPONo
                '            Call SprdMain_LeaveCell(ColPONo, cntRow, ColItemCode, cntRow, True)		

                .Row = CntRow
                .Col = ColItemCode
                .Text = mItemCode
                '            Call SprdMain_LeaveCell(ColItemCode, cntRow, ColBillQty, cntRow, True)		

                .Row = CntRow
                .Col = ColBillQty
                .Text = CStr(mQty)
                '            Call SprdMain_LeaveCell(ColBillQty, cntRow, ColReceivedQty, cntRow, True)		

                .Row = CntRow
                .Col = ColReceivedQty
                .Text = CStr(mQty)

                .Row = CntRow
                .Col = ColAcceptQty
                .Text = CStr(mQty)

                .Col = ColRate
                .Text = CStr(mRate)

                .Col = ColStockType
                .Text = "QC"


                mQCEmpCode = GetQCEmpCode(mItemCode)
                .Row = CntRow
                .Col = ColQCEMP
                .Text = mQCEmpCode

                .Row = CntRow
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColPONo, CntRow, ColItemCode, CntRow, True))
                .Row = CntRow
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColItemCode, CntRow, ColBillQty, CntRow, True))
                .Row = CntRow
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColBillQty, CntRow, ColReceivedQty, CntRow, True))
                .Row = CntRow

                '            SqlStr = " SELECT Item_Code,ITEM_SHORT_DESC AS NAME," & vbCrLf _		
                ''                    & " PURCHASE_UOM " & vbCrLf _		
                ''                    & " FROM INV_ITEM_MST " & vbCrLf _		
                ''                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _		
                ''                    & " AND Item_Code='" & MainClass.AllowSingleQuote(UCase(mItemCode)) & "' "		
                '		
                '            MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsMisc, adLockReadOnly		
                '            If RsMisc.EOF = False Then		
                '                .Row = cntRow		
                '                .Col = ColItemName		
                '                .Text = Trim(IIf(IsNull(RsMisc!Name), "", RsMisc!Name))		
                '		
                '                .Col = ColUnit		
                '                .Text = IIf(IsNull(RsMisc!PURCHASE_UOM), "", RsMisc!PURCHASE_UOM)		
                '		
                '                .Col = ColPOQty		
                '                .Text = 0		
                '		
                '                .Col = ColBalQty		
                '                .Text = 0		
                '		
                '                .Col = ColStockType		
                '                .Text = "QC"		
                '		
                '                mQCEmpCode = GetQCEmpCode(mItemCode)		
                '                .Col = ColQCEmp		
                '                .Text = mQCEmpCode		
                '            End If		
                CntRow = CntRow + 1
                If Trim(mPONo) <> "" Or cboRefType.SelectedIndex = 1 Then
                    MainClass.AddBlankSprdRow(SprdMain, ColPONo, ConRowHeight)
                End If
                '            .MaxRows = .MaxRows + 1		
            End With
        Loop

        With SprdExp
            mEDFlag = False
            mSTFlag = False
            mDiscountFlag = False
            mFreightFlag = False
            For CntRow = 1 To .MaxRows
                .Row = CntRow
                If .RowHidden = True Then GoTo NextcntRow

                .Col = ColExpIdent
                mSTID = UCase(Trim(.Text))

                If mSTID = "ED" And mEDFlag = False Then
                    .Col = ColExpPercent
                    .Text = CStr(Val(lblEDPercentage.Text))

                    .Col = ColExpAmt
                    .Text = CStr(Val(lblCGST.Text))
                    mEDFlag = True
                End If

                If mSTID = "ST" And mSTFlag = False Then
                    .Col = ColExpPercent
                    .Text = CStr(Val(lblSTPercentage.Text))

                    .Col = ColExpAmt
                    .Text = CStr(Val(lblSGST.Text))
                    mSTFlag = True
                End If

NextcntRow:
            Next
        End With

        txtScanning.Text = ""
        Exit Sub
ErrPart:
        ErrorMsg(CStr(Err.Number), Err.Description, MsgBoxStyle.Critical)
        '    Clear				
        '    Resume				
    End Sub


    Private Sub OptFreight_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptFreight.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptFreight.GetIndex(eventSender)

            MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        End If
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

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim xIName As String
        Dim xPoNo As String
        Dim SqlStr As String = ""
        Dim xSuppCode As String
        Dim xRefNo As String
        Dim xRGPCode As String
        Dim xItemCode As String = ""
        Dim mCT3No As Integer
        Dim mFromMRRDate As String
        Dim mDivisionCode As Double

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        If VB.Left(cboRefType.Text, 1) = "F" Or VB.Left(cboRefType.Text, 1) = "C" Or VB.Left(cboRefType.Text, 1) = "J" Then ''Or Left(cboRefType, 1) = "1"						
            mWithOutOrder = True
        Else
            mWithOutOrder = False
        End If

        SprdMain.Row = SprdMain.ActiveRow
        SprdMain.Col = ColPONo
        xRefNo = Trim(SprdMain.Text)

        If VB.Left(cboRefType.Text, 1) = "P" And xRefNo <> "" Then
            If MainClass.ValidateWithMasterTable(xRefNo, "AUTO_KEY_PO", "PUR_TYPE", "PUR_PURCHASE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DIV_CODE=" & mDivisionCode & " AND PUR_TYPE IN ('P','R','L')") = True Then
                If MasterNo = "R" Then
                    mIsProjectPO = True
                Else
                    mIsProjectPO = False
                End If
            End If
        Else
            mIsProjectPO = False
        End If

        If eventArgs.row = 0 And eventArgs.col = ColPONo Then
            SprdMain.Row = SprdMain.ActiveRow
            SprdMain.Col = ColPONo
            xPoNo = Trim(SprdMain.Text)

            Select Case VB.Left(cboRefType.Text, 1)
                Case "P"
                    SqlStr = "SELECT DISTINCT POMain.AUTO_KEY_PO  As AUTO_KEY_PO , POMain.PUR_ORD_DATE, PODetail.PO_WEF_DATE " & vbCrLf _
                        & " FROM PUR_PURCHASE_HDR POMain,PUR_PURCHASE_DET PODetail" & vbCrLf _
                        & " WHERE POMain.MKEY=PODetail.MKEY" & vbCrLf _
                        & " AND POMain.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND PUR_TYPE IN ('P','R','L')"

                    ''AND SUBSTR(POMain.AUTO_KEY_PO,LENGTH(POMain.AUTO_KEY_PO)-5,4)=" & RsCompany.Fields("FYEAR").Value						

                    If Trim(TxtSupplier.Text) <> "" Then
                        If MainClass.ValidateWithMasterTable((TxtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            xSuppCode = MasterNo
                            SqlStr = SqlStr & vbCrLf & " AND SUPP_CUST_CODE='" & xSuppCode & "'"
                        End If
                    End If

                    SqlStr = SqlStr & vbCrLf & " AND DIV_CODE=" & mDivisionCode & ""

                    If RsCompany.Fields("FYEAR").Value < ConOPENPO_CONTINOUS_YEAR Then
                        SqlStr = SqlStr & vbCrLf & " AND POMain.ORDER_TYPE NOT IN " & vbCrLf & " CASE WHEN SUBSTR(POMain.AUTO_KEY_PO,LENGTH(POMain.AUTO_KEY_PO)-5,4)<" & RsCompany.Fields("FYEAR").Value & " THEN ('O') ELSE ('-1') END"
                    Else
                        SqlStr = SqlStr & vbCrLf & " AND SUBSTR(POMain.AUTO_KEY_PO,LENGTH(POMain.AUTO_KEY_PO)-5,4)>= " & vbCrLf & " CASE WHEN POMain.ORDER_TYPE='O' THEN " & ConOPENPO_CONTINOUS_YEAR & " ELSE 1 END"
                    End If

                    If IsDate(txtMRRDate.Text) Then
                        SqlStr = SqlStr & vbCrLf & " AND PODetail.PO_WEF_DATE<=TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                    End If

                    If ADDMode = True Then
                        SqlStr = SqlStr & vbCrLf & " AND POMain.PO_STATUS='Y' AND POMain.PO_CLOSED='N'"
                    End If

                    If CDate(txtMRRDate.Text) < CDate(PubGSTApplicableDate) And CDate(txtBillDate.Text) < CDate(PubGSTApplicableDate) Then

                    ElseIf CDate(txtMRRDate.Text) < CDate(PubGSTApplicableDate) And CDate(txtBillDate.Text) >= CDate(PubGSTApplicableDate) Then

                    ElseIf CDate(txtMRRDate.Text) >= CDate(PubGSTApplicableDate) And CDate(txtBillDate.Text) < CDate(PubGSTApplicableDate) Then

                    Else
                        SqlStr = SqlStr & vbCrLf & " AND PODetail.PO_WEF_DATE>=TO_DATE('" & VB6.Format(PubGSTApplicableDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                    End If

                    SqlStr = SqlStr & vbCrLf & "  AND PO_ITEM_STATUS='N' " & vbCrLf & " AND POMain.AUTO_KEY_PO Like '" & xPoNo & "%'"

                    '                SqlStr = SqlStr & vbCrLf & " AND POMain.PO_STATUS='Y' AND PO_ITEM_STATUS='N' " & vbCrLf _						
                    ''                        & " AND POMain.AUTO_KEY_PO Like '" & xPoNo & "%'" & vbCrLf _						
                    ''                        & " AND PODetail.PO_WEF_DATE = ("						
                    '						
                    '                SqlStr = SqlStr & vbCrLf & " SELECT MAX(ID.PO_WEF_DATE) " & vbCrLf _						
                    ''                        & " FROM PUR_PURCHASE_HDR IH,PUR_PURCHASE_DET ID" & vbCrLf _						
                    ''                        & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _						
                    ''                        & " AND IH.Company_Code=" & RsCompany.fields("COMPANY_CODE").value & " AND PUR_TYPE IN ('P','R','L')" & vbCrLf _						
                    ''                        & " AND IH.PO_STATUS='Y' AND PO_ITEM_STATUS='N' AND IH.AUTO_KEY_PO Like '" & xPoNo & "%' "						
                    '						
                    '                If Trim(txtSupplier.Text) <> "" Then						
                    '                    If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then						
                    '                        xSuppCode = MasterNo						
                    '                        SqlStr = SqlStr & vbCrLf & " AND IH.SUPP_CUST_CODE='" & xSuppCode & "'"						
                    '                    End If						
                    '                End If						
                    '						
                    '                SqlStr = SqlStr & vbCrLf & " AND IH.DIV_CODE=" & mDivisionCode & ""						
                    '						
                    '                If IsDate(txtMRRDate) Then						
                    '                    SqlStr = SqlStr & vbCrLf & " AND PODetail.PO_WEF_DATE<='" & VB6.Format(txtMRRDate, "DD-MMM-YYYY") & "'"						
                    '                End If						
                    '						
                    '                SqlStr = SqlStr & ")"						

                    SqlStr = SqlStr & vbCrLf & " ORDER BY TO_NUMBER(POMain.AUTO_KEY_PO),POMain.PUR_ORD_DATE"

                Case "R"

                    SqlStr = "SELECT DISTINCT RGP_NO,  OUTWARD_ITEM_CODE AS ITEM_CODE, RGP_DATE, TO_CHAR(SUM(DECODE(ITEM_IO,'O',1,-1) * RGP_QTY)) AS Balance,F4NO"


                    SqlStr = SqlStr & vbCrLf _
                        & " FROM INV_RGP_REG_TRN" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

                    If Trim(TxtSupplier.Text) <> "" Then
                        If MainClass.ValidateWithMasterTable((TxtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            xSuppCode = MasterNo
                            SqlStr = SqlStr & vbCrLf & " AND SUPP_CUST_CODE='" & xSuppCode & "'"
                        End If
                    End If

                    SqlStr = SqlStr & vbCrLf & " AND BILL_TO_LOC_ID='" & MainClass.AllowSingleQuote(txtBillTo.Text) & "'"

                    '                SqlStr = SqlStr & vbCrLf & " AND DIV_CODE=" & mDivisionCode & ""						

                    SqlStr = SqlStr & vbCrLf & " AND RGP_NO Like '" & xPoNo & "%'"

                    'If Val(txtMRRNo.Text) <> 0 Then
                    '    SqlStr = SqlStr & vbCrLf & " AND REF_NO<>" & Val(txtMRRNo.Text) & ""
                    'End If

                    SqlStr = SqlStr & vbCrLf & " AND REF_NO NOT IN (" & vbCrLf _
                            & " SELECT REF_NO FROM INV_RGP_REG_TRN " & vbCrLf _
                            & " WHERE " & vbCrLf _
                            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(UCase(xSuppCode)) & "'" & vbCrLf _
                            & " AND BILL_NO='" & txtBillNo.Text & "' AND  BILL_DATE =TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND BOOKTYPE='M' AND ITEM_IO='I')"


                    If IsDate(txtMRRDate.Text) Then
                        SqlStr = SqlStr & vbCrLf & " AND RGP_DATE<=TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                    End If


                    SqlStr = SqlStr & vbCrLf & " HAVING SUM(DECODE(ITEM_IO,'O',1,-1) * RGP_QTY)>0 "

                    SqlStr = SqlStr & vbCrLf & " GROUP BY RGP_NO, RGP_DATE, OUTWARD_ITEM_CODE,F4NO "

                    SqlStr = SqlStr & vbCrLf & " ORDER BY OUTWARD_ITEM_CODE, RGP_NO "

                Case "I", "1", "2", "3"
                    SqlStr = "SELECT DISTINCT IH.AUTO_KEY_INVOICE ,TO_CHAR(IH.INVOICE_DATE,'DD/MM/YYYY') AS INVOICE_DATE, ID.ITEM_CODE, ID.ITEM_QTY, ID.ITEM_DESC " & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID" & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IH.INVOICESEQTYPE<>9" ''& vbCrLf |                        & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" |						
                    If Trim(TxtSupplier.Text) <> "" Then
                        If MainClass.ValidateWithMasterTable((TxtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            xSuppCode = MasterNo
                            SqlStr = SqlStr & vbCrLf & " AND IH.SUPP_CUST_CODE='" & xSuppCode & "'"
                        End If
                    End If

                    SqlStr = SqlStr & vbCrLf & " AND IH.DIV_CODE=" & mDivisionCode & ""

                    If IsDate(txtMRRDate.Text) Then
                        mFromMRRDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -365, CDate(txtMRRDate.Text)))
                        SqlStr = SqlStr & vbCrLf & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(mFromMRRDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                        SqlStr = SqlStr & vbCrLf & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                    End If

                    SqlStr = SqlStr & vbCrLf & " AND IH.AUTO_KEY_INVOICE Like '" & xPoNo & "%'" & vbCrLf & " ORDER BY TO_CHAR(IH.INVOICE_DATE,'DD/MM/YYYY') , IH.AUTO_KEY_INVOICE, ID.ITEM_CODE "
            End Select

            If SqlStr <> "" Then
                With SprdMain
                    .Row = .ActiveRow
                    .Col = ColPONo
                    If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                        .Row = .ActiveRow
                        .Col = ColPONo
                        .Text = AcName

                        If VB.Left(cboRefType.Text, 1) = "R" Then
                            .Col = ColRGPItemCode
                        Else
                            .Col = ColPODate
                        End If
                        .Text = AcName1

                        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColPONo)
                    End If
                End With
            End If
        End If

        If eventArgs.row = 0 And eventArgs.col = ColItemCode Then
            With SprdMain
                .Row = .ActiveRow

                .Col = ColItemCode
                If mWithOutOrder = True Or mIsProjectPO = True Then
                    If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS='A'") = True Then
                        .Row = .ActiveRow
                        .Col = ColItemCode
                        .Text = Trim(AcName)
                    End If

                Else
                    .Col = ColPONo
                    xRefNo = Trim(.Text)

                    .Col = ColRGPItemCode
                    xRGPCode = Trim(.Text)

                    .Row = .ActiveRow
                    .Col = ColItemCode

                    SqlStr = SelectQuery(VB.Left(cboRefType.Text, 1), xRefNo, True, mDivisionCode, xRGPCode)

                    If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                        .Row = .ActiveRow
                        .Col = ColItemCode
                        .Text = Trim(AcName)
                    End If
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColItemName Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemName
                xIName = .Text
                .Text = ""
                If mWithOutOrder = True Or mIsProjectPO = True Then
                    If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS='A'") = True Then
                        .Row = .ActiveRow
                        .Col = ColItemName
                        .Text = AcName
                    Else
                        .Row = .ActiveRow
                        .Col = ColItemName
                        .Text = xIName
                    End If

                Else
                    .Col = ColPONo
                    xRefNo = Trim(.Text)

                    .Col = ColRGPItemCode
                    xRGPCode = Trim(.Text)

                    .Row = .ActiveRow
                    .Col = ColItemCode

                    SqlStr = SelectQuery(VB.Left(cboRefType.Text, 1), xRefNo, False, mDivisionCode, xRGPCode)


                    If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                        .Row = .ActiveRow
                        .Col = ColItemName
                        .Text = Trim(AcName)
                    Else
                        .Row = .ActiveRow
                        .Col = ColItemName
                        .Text = xIName
                    End If

                End If
                MainClass.ValidateWithMasterTable(.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                .Row = .ActiveRow
                .Col = ColItemCode
                .Text = Trim(MasterNo)
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColStockType Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColStockType
                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

                If lblBookType.Text = "Q" Then
                    SqlStr = SqlStr & " AND STOCK_TYPE_CODE IN ('CS','CR','FC','QC','SC','ST')"
                Else
                    SqlStr = SqlStr & " AND STOCK_TYPE_CODE='QC'"
                End If

                If MainClass.SearchGridMaster(.Text, "INV_TYPE_MST", "STOCK_TYPE_CODE", "STOCK_TYPE_DESC", , , SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColStockType
                    .Text = AcName
                End If
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColQCEMP Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColQCEMP

                If MainClass.SearchGridMaster(.Text, "PAY_EMPLOYEE_MST", "EMP_CODE", "EMP_NAME", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_LEAVE_DATE IS NULL AND EMP_STOP_SALARY='N'") = True Then
                    .Row = .ActiveRow
                    .Col = ColQCEMP
                    .Text = AcName
                End If
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColCT3No Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode
                xItemCode = Trim(.Text)

                .Col = ColCT3No
                mCT3No = Val(.Text)

                SqlStr = " SELECT CT_NO, CT_DATE," & vbCrLf & " TRN.ITEM_CODE, " & vbCrLf & " TO_CHAR(SUM(DECODE(BOOKSUBTYPE,'O',1,-1)*ITEM_QTY)) AS BalQty" & vbCrLf & " FROM FIN_CT_TRN TRN " & vbCrLf & " WHERE " & vbCrLf & " TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND BOOKTYPE='P'"

                If Val(txtMRRNo.Text) <> 0 Then
                    SqlStr = SqlStr & vbCrLf & "AND TRN.MKEY <> '" & MainClass.AllowSingleQuote(txtMRRNo.Text) & "'"
                End If

                If Val(.Text) <> 0 Then
                    SqlStr = SqlStr & vbCrLf & " AND CT_NO=" & Val(.Text) & " "
                End If

                If Trim(TxtSupplier.Text) <> "" Then
                    If MainClass.ValidateWithMasterTable(TxtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        xSuppCode = MasterNo
                        SqlStr = SqlStr & vbCrLf & " AND TRN.SUPP_CUST_CODE='" & xSuppCode & "'"
                    End If
                End If

                SqlStr = SqlStr & vbCrLf & "AND TRN.ITEM_CODE = '" & xItemCode & "'"

                SqlStr = SqlStr & vbCrLf & " GROUP BY CT_NO, CT_DATE, TRN.ITEM_CODE"
                SqlStr = SqlStr & vbCrLf & " HAVING SUM(DECODE(BOOKSUBTYPE,'O',1,-1)*ITEM_QTY)<>0"
                SqlStr = SqlStr & vbCrLf & " ORDER BY CT_NO, CT_DATE, ITEM_CODE "


                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColCT3No
                    .Text = CStr(Val(AcName))
                End If

            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColPCNo Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode
                xItemCode = Trim(.Text)

                .Col = ColPCNo
                mCT3No = Val(.Text)

                SqlStr = " SELECT PC_NO, PC_DATE," & vbCrLf & " TRN.ITEM_CODE, " & vbCrLf & " TO_CHAR(SUM(DECODE(BOOKSUBTYPE,'O',1,-1)*ITEM_QTY)) AS BalQty" & vbCrLf & " FROM FIN_PC_TRN TRN " & vbCrLf & " WHERE " & vbCrLf & " TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND BOOKTYPE='P'"

                If Val(txtMRRNo.Text) <> 0 Then
                    SqlStr = SqlStr & vbCrLf & "AND TRN.MKEY <> '" & MainClass.AllowSingleQuote(txtMRRNo.Text) & "'"
                End If

                If Val(.Text) <> 0 Then
                    SqlStr = SqlStr & vbCrLf & " AND PC_NO=" & Val(.Text) & " "
                End If

                If Trim(TxtSupplier.Text) <> "" Then
                    If MainClass.ValidateWithMasterTable(TxtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        xSuppCode = MasterNo
                        SqlStr = SqlStr & vbCrLf & " AND TRN.SUPP_CUST_CODE='" & xSuppCode & "'"
                    End If
                End If

                SqlStr = SqlStr & vbCrLf & "AND TRN.ITEM_CODE = '" & xItemCode & "'"

                SqlStr = SqlStr & vbCrLf & " GROUP BY PC_NO, PC_DATE, TRN.ITEM_CODE"
                SqlStr = SqlStr & vbCrLf & " HAVING SUM(DECODE(BOOKSUBTYPE,'O',1,-1)*ITEM_QTY)<>0"
                SqlStr = SqlStr & vbCrLf & " ORDER BY PC_NO, PC_DATE, ITEM_CODE "


                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColPCNo
                    .Text = CStr(Val(AcName))
                End If

            End With
        End If
        If RsCompany.Fields("MRR_AGT_GE").Value = "N" Then
            Dim mPONo As String
            Dim mItemCode As String
            Dim DelStatus As Boolean
            If eventArgs.col = 0 And eventArgs.row > 0 Then
                SprdMain.Row = eventArgs.row
                SprdMain.Col = ColPONo
                If eventArgs.row < SprdMain.MaxRows And (ADDMode = True Or MODIFYMode = True) Then

                    mPONo = SprdMain.Text

                    SprdMain.Col = ColItemCode
                    mItemCode = SprdMain.Text

                    MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColPONo, DelStatus)
                    FormatSprdMain(-1)
                    MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
                End If
            End If
        End If

        CalcTots()
    End Sub
    Private Function ValidateRefNo(ByVal mItemCode As String, ByVal mSupplierCode As String, ByVal mRefNo As String, ByVal mDivisionCode As Double) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        ValidateRefNo = False

        If VB.Left(cboRefType.Text, 1) = "F" Or VB.Left(cboRefType.Text, 1) = "C" Or VB.Left(cboRefType.Text, 1) = "J" Or VB.Left(cboRefType.Text, 1) = "R" Then ''Or Left(cboRefType, 1) = "1"						
            ValidateRefNo = True
            Exit Function
        End If

        If VB.Left(cboRefType.Text, 1) = "I" And Val(mRefNo) < 0 Then ''Or Left(cboRefType, 1) = "1"						
            ValidateRefNo = True
            Exit Function
        End If

        Select Case VB.Left(cboRefType.Text, 1)
            Case "P"
                mIsProjectPO = False
                If Val(mRefNo) > 0 Then
                    If MainClass.ValidateWithMasterTable(mRefNo, "AUTO_KEY_PO", "PUR_TYPE", "PUR_PURCHASE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND PUR_TYPE IN ('P','R','L')") = True Then
                        If MasterNo = "R" Then
                            mIsProjectPO = True
                            ValidateRefNo = True
                            Exit Function
                        End If
                    End If
                End If

                SqlStr = "SELECT DISTINCT POMain.AUTO_KEY_PO  As AUTO_KEY_PO , POMain.PUR_ORD_DATE, PODetail.PO_WEF_DATE " & vbCrLf & " FROM PUR_PURCHASE_HDR POMain,PUR_PURCHASE_DET PODetail" & vbCrLf & " WHERE POMain.MKEY=PODetail.MKEY" & vbCrLf & " AND POMain.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND PUR_TYPE IN ('P','R','L')" & vbCrLf & " AND SUPP_CUST_CODE='" & mSupplierCode & "'" & vbCrLf & " AND DIV_CODE=" & mDivisionCode & ""

                If RsCompany.Fields("FYEAR").Value < ConOPENPO_CONTINOUS_YEAR Then
                    SqlStr = SqlStr & vbCrLf & " AND POMain.ORDER_TYPE NOT IN " & vbCrLf & " CASE WHEN SUBSTR(POMain.AUTO_KEY_PO,LENGTH(POMain.AUTO_KEY_PO)-5,4)<" & RsCompany.Fields("FYEAR").Value & " THEN ('O') ELSE ('-1') END"
                Else
                    SqlStr = SqlStr & vbCrLf & " AND SUBSTR(POMain.AUTO_KEY_PO,LENGTH(POMain.AUTO_KEY_PO)-5,4)>= " & vbCrLf & " CASE WHEN POMain.ORDER_TYPE='O' THEN " & ConOPENPO_CONTINOUS_YEAR & " ELSE 1 END"
                End If

                If IsDate(txtMRRDate.Text) Then
                    SqlStr = SqlStr & vbCrLf & " AND PODetail.PO_WEF_DATE<=TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                End If

                If CDate(txtMRRDate.Text) < CDate(PubGSTApplicableDate) And CDate(txtBillDate.Text) < CDate(PubGSTApplicableDate) Then

                ElseIf CDate(txtMRRDate.Text) < CDate(PubGSTApplicableDate) And CDate(txtBillDate.Text) >= CDate(PubGSTApplicableDate) Then

                ElseIf CDate(txtMRRDate.Text) >= CDate(PubGSTApplicableDate) And CDate(txtBillDate.Text) < CDate(PubGSTApplicableDate) Then

                Else
                    SqlStr = SqlStr & vbCrLf & " AND PODetail.PO_WEF_DATE>=TO_DATE('" & VB6.Format(PubGSTApplicableDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                End If

                '                If PubGSTApplicable = True Then						
                '                    SqlStr = SqlStr & vbCrLf & " AND PODetail.PO_WEF_DATE>='" & VB6.Format(PubGSTApplicableDate, "DD-MMM-YYYY") & "'"						
                '                End If						

                If ADDMode = True Then
                    SqlStr = SqlStr & vbCrLf & " AND POMain.PO_CLOSED='N'"
                End If

                SqlStr = SqlStr & vbCrLf & " AND POMain.PO_STATUS='Y'"

                SqlStr = SqlStr & vbCrLf & " AND PODetail.ITEM_CODE='" & mItemCode & "'"

                SqlStr = SqlStr & vbCrLf & "  AND PO_ITEM_STATUS='N' " & vbCrLf & " AND POMain.AUTO_KEY_PO = '" & mRefNo & "'"

            Case "R"

                SqlStr = "SELECT DISTINCT RGP_NO,  OUTWARD_ITEM_CODE AS ITEM_CODE, RGP_DATE, TO_CHAR(SUM(DECODE(ITEM_IO,'O',1,-1) * RGP_QTY)) AS Balance,F4NO" & vbCrLf & " FROM INV_RGP_REG_TRN" & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUPP_CUST_CODE='" & mSupplierCode & "'"

                SqlStr = SqlStr & vbCrLf & " AND RGP_NO = '" & mRefNo & "'"

                SqlStr = SqlStr & vbCrLf & " AND OUTWARD_ITEM_CODE='" & mItemCode & "'"

                'If Val(txtMRRNo.Text) <> 0 Then
                '    SqlStr = SqlStr & vbCrLf & " AND REF_NO<>" & Val(txtMRRNo.Text) & ""
                'End If

                SqlStr = SqlStr & vbCrLf & " AND REF_NO NOT IN (" & vbCrLf _
                        & " SELECT REF_NO FROM INV_RGP_REG_TRN " & vbCrLf _
                        & " WHERE " & vbCrLf _
                        & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(UCase(mSupplierCode)) & "'" & vbCrLf _
                        & " AND BILL_NO='" & txtBillNo.Text & "' AND  BILL_DATE =TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND BOOKTYPE='M' AND ITEM_IO='I')"


                If IsDate(txtMRRDate.Text) Then
                    SqlStr = SqlStr & vbCrLf & " AND RGP_DATE<=TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                End If

                SqlStr = SqlStr & vbCrLf & " HAVING SUM(DECODE(ITEM_IO,'O',1,-1) * RGP_QTY)>0 "

                SqlStr = SqlStr & vbCrLf & " GROUP BY RGP_NO, RGP_DATE, OUTWARD_ITEM_CODE,F4NO "

            Case "I", "1", "2", "3"
                If RsCompany.Fields("StockBalCheck").Value = "N" Then
                    ValidateRefNo = True
                    Exit Function
                End If
                SqlStr = "SELECT DISTINCT IH.AUTO_KEY_INVOICE ,IH.INVOICE_DATE, ID.ITEM_CODE, ID.ITEM_QTY, ID.ITEM_DESC " & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID" & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " "

                SqlStr = SqlStr & vbCrLf & " AND ID.ITEM_CODE='" & mItemCode & "'"

                If CDbl(Mid(mRefNo, Len(mRefNo) - 5, 4)) >= 2012 Then
                    SqlStr = SqlStr & vbCrLf & " AND IH.DIV_CODE='" & mDivisionCode & "'"
                Else
                    If PubSuperUser <> "S" Then
                        SqlStr = SqlStr & vbCrLf & " AND IH.DIV_CODE='" & mDivisionCode & "'"
                    End If
                End If


                SqlStr = SqlStr & vbCrLf & " AND IH.SUPP_CUST_CODE='" & mSupplierCode & "'"

                If IsDate(txtMRRDate.Text) Then
                    SqlStr = SqlStr & vbCrLf & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                End If

                SqlStr = SqlStr & vbCrLf & " AND IH.AUTO_KEY_INVOICE = '" & mRefNo & "'"
        End Select

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            ValidateRefNo = True
            Exit Function
        End If
        Exit Function
ErrPart:

    End Function
    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        Dim xPoNo As String
        Dim xICode As String
        Dim mQty As Double
        Dim mAcceptQty As Double
        Dim mItemClassType As String
        Dim mLotNoRequied As String
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xRGPItemCode As String
        Dim mRow As Integer
        Dim mDivisionCode As Double
        Dim xSqlStr As String
        Dim mBillFromSupplier As String = ""
        Dim mPORate As Double
        Dim mBillRate As Double

        Dim mItemWeight As Double
        Dim mQtyKgs As Double
        Dim xQty As Double

        If eventArgs.newRow = -1 Then Exit Sub
        Call UpdateTempFile()

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        If Not IsDate(txtBillDate.Text) Then
            MsgInformation("Please enter Bill Date First.")
            If txtBillDate.Enabled = True Then txtBillDate.Focus()
            Exit Sub
        End If

        If VB.Left(cboRefType.Text, 1) = "F" Or VB.Left(cboRefType.Text, 1) = "J" Or VB.Left(cboRefType.Text, 1) = "C" Then ''Or Left(cboRefType.Text, 1) = "1"						
            mWithOutOrder = True
        Else
            mWithOutOrder = False
        End If

        SprdMain.Row = eventArgs.row
        SprdMain.Col = ColPONo
        xPoNo = SprdMain.Text

        If VB.Left(cboRefType.Text, 1) = "P" And xPoNo <> "" Then
            If MainClass.ValidateWithMasterTable(xPoNo, "AUTO_KEY_PO", "PUR_TYPE", "PUR_PURCHASE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DIV_CODE=" & mDivisionCode & " AND PUR_TYPE IN ('P','R','L')") = True Then
                If MasterNo = "R" Then
                    mIsProjectPO = True
                Else
                    mIsProjectPO = False
                End If
            End If
        Else
            mIsProjectPO = False
        End If

        mRow = eventArgs.row
        SprdMain.Row = mRow
        If mWithOutOrder = False Then
            SprdMain.Col = ColItemCode
            If Trim(SprdMain.Text) <> "" Then
                SprdMain.Col = ColPONo
                If SprdMain.Text = "" Then
                    '                MsgInformation "Please Select Valid Ref No for Such Supplier"						
                    '                MainClass.SetFocusToCell SprdMain, Row, ColItemCode						
                    '                Exit Sub						
                    SprdMain.Col = ColItemCode
                    SprdMain.Text = ""
                End If
            Else
                SprdMain.Col = ColPONo
                If SprdMain.Text = "" Then Exit Sub
            End If
        Else
            SprdMain.Col = ColItemCode
            If SprdMain.Text = "" Then Exit Sub
        End If

        '    SprdMain.Col = ColPONo						
        '    If UCase(Trim(SprdMain.Text)) = "" Or UCase(Trim(SprdMain.Text)) = "-1" & VB6.Format(RsCompany.fields("FYEAR").value, "00") Then						
        '        SprdMain.Col = ColPONo						
        '        SprdMain.Text = "-1" & VB6.Format(RsCompany.fields("FYEAR").value, "00")						
        '        mWithOutOrder = True						
        '    Else						
        '        mWithOutOrder = False						
        '    End If						

        '    SprdMain.Col = ColPONo						
        '    If mWithOutOrder = True Then						
        '        SprdMain.Col = ColPONo						
        '        SprdMain.Text = "-1" & VB6.Format(RsCompany.fields("FYEAR").value, "00")						
        '    End If						


        'If chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked Then
        mBillFromSupplier = mSupplierCode
        'Else
        '    If MainClass.ValidateWithMasterTable((txtShippedTo.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mBillFromSupplier = MasterNo
        '    End If
        'End If

        Select Case eventArgs.col
            Case ColPONo
                SprdMain.Row = mRow
                SprdMain.Col = ColPONo
                xPoNo = SprdMain.Text
                If mWithOutOrder = False Then
                    If VB.Left(cboRefType.Text, 1) = "P" Then
                        If MainClass.ValidateWithMasterTable(xPoNo, "AUTO_KEY_PO", "PUR_ORD_DATE", "PUR_PURCHASE_HDR", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DIV_CODE=" & mDivisionCode & " AND PUR_TYPE IN ('P','R','L') AND SUPP_CUST_CODE='" & mBillFromSupplier & "' AND AMEND_WEF_DATE<=TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')") = False Then
                            If xPoNo <> "" Then
                                MsgInformation("Invalid Ref No for Such Supplier")
                                MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColPONo)
                                eventArgs.cancel = True
                            End If
                        Else
                            SprdMain.Col = ColPODate
                            SprdMain.Text = MasterNo
                        End If
                    ElseIf VB.Left(cboRefType.Text, 1) = "R" Then

                        If Val(xPoNo) = 0 Then
                            Exit Sub
                        End If

                        If MainClass.ValidateWithMasterTable(xPoNo, "AUTO_KEY_PASSNO", "AUTO_KEY_PASSNO", "INV_GATEPASS_HDR", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & mSupplierCode & "' AND DIV_CODE=" & mDivisionCode & "") = False Then
                            MsgInformation("Invalid Ref No for Such Supplier")
                            MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColPONo)
                            eventArgs.cancel = True
                            Exit Sub
                        End If
                        '                    SprdMain.Col = ColRGPItemCode						
                        '                    xRGPItemCode = SprdMain.Text						



                        SqlStr = "SELECT RGP_NO, RGP_DATE, OUTWARD_ITEM_CODE AS ITEM_CODE" & vbCrLf & " FROM INV_RGP_REG_TRN" & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUPP_CUST_CODE='" & mSupplierCode & "'" & vbCrLf & " AND RGP_NO = " & xPoNo & " AND ITEM_IO='O'"
                        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                        If RsTemp.EOF = False Then
                            SprdMain.Row = mRow
                            SprdMain.Col = ColPODate
                            SprdMain.Text = IIf(IsDBNull(RsTemp.Fields("RGP_DATE").Value), "", RsTemp.Fields("RGP_DATE").Value)
                            '                        SprdMain.Col = ColRGPItemCode						
                            '                        SprdMain.Text = IIf(IsNull(RsTemp!ITEM_CODE), "", RsTemp!ITEM_CODE)						
                        Else
                            MsgInformation("Invalid Ref No for Such Supplier")
                            MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColPONo)
                            eventArgs.cancel = True
                        End If

                    ElseIf VB.Left(cboRefType.Text, 1) = "I" Or VB.Left(cboRefType.Text, 1) = "1" Or VB.Left(cboRefType.Text, 1) = "2" Or VB.Left(cboRefType.Text, 1) = "3" Then
                        If CDbl(Mid(xPoNo, Len(xPoNo) - 5, 4)) >= 2012 Then
                            xSqlStr = "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DIV_CODE=" & mDivisionCode & " AND (SUPP_CUST_CODE='" & mSupplierCode & "' OR BUYER_CODE='" & mSupplierCode & "' OR CO_BUYER_CODE='" & mSupplierCode & "')"
                        Else
                            If PubSuperUser <> "S" Then
                                xSqlStr = "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DIV_CODE=" & mDivisionCode & " AND (SUPP_CUST_CODE='" & mSupplierCode & "' OR BUYER_CODE='" & mSupplierCode & "' OR CO_BUYER_CODE='" & mSupplierCode & "')"
                            Else
                                xSqlStr = "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND (SUPP_CUST_CODE='" & mSupplierCode & "' OR BUYER_CODE='" & mSupplierCode & "' OR CO_BUYER_CODE='" & mSupplierCode & "')"
                            End If
                        End If
                        If MainClass.ValidateWithMasterTable(xPoNo, "AUTO_KEY_INVOICE", "INVOICE_DATE", "FIN_INVOICE_HDR", PubDBCn, MasterNo, , xSqlStr) = False Then
                            MsgInformation("Invalid Ref No for Such Supplier")
                            MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColPONo)
                            eventArgs.cancel = True
                        Else
                            SprdMain.Col = ColPODate
                            SprdMain.Text = MasterNo
                        End If
                    End If
                End If
            Case ColItemCode
                SprdMain.Row = mRow

                SprdMain.Col = ColPONo
                xPoNo = SprdMain.Text

                SprdMain.Col = ColRGPItemCode
                xRGPItemCode = Trim(SprdMain.Text)

                If VB.Left(cboRefType.Text, 1) = "P" Then
                    If mIsProjectPO = False Then
                        If MainClass.ValidateWithMasterTable(xPoNo, "AUTO_KEY_PO", "PUR_ORD_DATE", "PUR_PURCHASE_HDR", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND PUR_TYPE IN ('P','R','L') AND DIV_CODE=" & mDivisionCode & " AND SUPP_CUST_CODE='" & mBillFromSupplier & "' AND AMEND_WEF_DATE<=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')") = False Then
                            MsgInformation("Invalid Ref No for Such Supplier")
                            MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColPONo)
                            eventArgs.cancel = True
                            Exit Sub
                        End If
                    End If
                ElseIf VB.Left(cboRefType.Text, 1) = "R" Then
                    If MainClass.ValidateWithMasterTable(xPoNo, "AUTO_KEY_PASSNO", "AUTO_KEY_PASSNO", "INV_GATEPASS_HDR", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & mBillFromSupplier & "' AND DIV_CODE=" & mDivisionCode & "") = False Then
                        MsgInformation("Invalid Ref No for Such Supplier")
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColPONo)
                        eventArgs.cancel = True
                        Exit Sub
                    End If
                ElseIf VB.Left(cboRefType.Text, 1) = "I" Or VB.Left(cboRefType.Text, 1) = "1" Or VB.Left(cboRefType.Text, 1) = "2" Or VB.Left(cboRefType.Text, 1) = "3" Then
                    If CDbl(Mid(xPoNo, Len(xPoNo) - 5, 4)) >= 2012 Then
                        xSqlStr = "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DIV_CODE=" & mDivisionCode & " AND (SUPP_CUST_CODE='" & mSupplierCode & "' OR BUYER_CODE='" & mSupplierCode & "' OR CO_BUYER_CODE='" & mSupplierCode & "')"
                    Else
                        If PubSuperUser <> "S" Then
                            xSqlStr = "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DIV_CODE=" & mDivisionCode & " AND (SUPP_CUST_CODE='" & mSupplierCode & "' OR BUYER_CODE='" & mSupplierCode & "' OR CO_BUYER_CODE='" & mSupplierCode & "')"
                        Else
                            xSqlStr = "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND (SUPP_CUST_CODE='" & mSupplierCode & "' OR BUYER_CODE='" & mSupplierCode & "' OR CO_BUYER_CODE='" & mSupplierCode & "')"
                        End If
                    End If
                    If MainClass.ValidateWithMasterTable(xPoNo, "AUTO_KEY_INVOICE", "INVOICE_DATE", "FIN_INVOICE_HDR", PubDBCn, MasterNo, , xSqlStr) = False Then
                        MsgInformation("Invalid Ref No for Such Supplier")
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColPONo)
                        eventArgs.cancel = True
                        Exit Sub
                    End If
                End If

                SprdMain.Col = ColItemCode
                xICode = Trim(SprdMain.Text)
                If xICode = "" Then Exit Sub
                If MainClass.ValidateWithMasterTable(xICode, "Item_Code", "Item_Code", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS='A'") = True Then
                    If DuplicateItemCode(mRow) = False Then
                        SprdMain.Row = mRow
                        If FillGridRow(xPoNo, xICode, mRow, xRGPItemCode, mDivisionCode) = False Then Exit Sub
                        FormatSprdMain(eventArgs.row)
                        If lblBookType.Text = "Q" Then
                            MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColAcceptQty)
                        Else
                            MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColBillQty)
                        End If
                    Else
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColItemCode)
                        eventArgs.cancel = True
                    End If
                Else
                    MsgBox("Either Item Code in Invalid or not Active.", MsgBoxStyle.Information)
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColItemCode)
                    eventArgs.cancel = True
                End If

            Case ColItemName
                SprdMain.Row = mRow
                SprdMain.Col = ColItemName
                If MainClass.ValidateWithMasterTable(SprdMain.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS='A'") = False Then
                    MsgBox("Either Item Code in Invalid or not Active.", MsgBoxStyle.Information)
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColItemCode)
                    eventArgs.cancel = True
                End If

            Case ColBillQty
                SprdMain.Row = mRow
                SprdMain.Col = ColPONo
                xPoNo = SprdMain.Text
                If mWithOutOrder = False Then
                    If xPoNo = "" Then Exit Sub
                End If

                SprdMain.Col = ColItemCode
                xICode = SprdMain.Text
                If xICode = "" Then Exit Sub

                ''25-06-2007						

                If PubSuperUser <> "S" Then
                    If VB.Left(cboRefType.Text, 1) = "P" Or VB.Left(cboRefType.Text, 1) = "I" Or VB.Left(cboRefType.Text, 1) = "1" Or VB.Left(cboRefType.Text, 1) = "R" Or VB.Left(cboRefType.Text, 1) = "2" Or VB.Left(cboRefType.Text, 1) = "3" Then
                        If CheckBillQty(ColBillQty, eventArgs.row) = True Then
                            SprdMain.Col = ColReceivedQty
                            mQty = Val(SprdMain.Text)
                            MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                            FormatSprdMain(eventArgs.row)
                        Else
                            eventArgs.cancel = True
                            Exit Sub
                        End If
                    Else
                        SprdMain.Col = ColReceivedQty
                        mQty = Val(SprdMain.Text)
                        MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                        FormatSprdMain(eventArgs.row)
                    End If
                Else
                    SprdMain.Col = ColReceivedQty
                    mQty = Val(SprdMain.Text)
                    MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                    FormatSprdMain(eventArgs.row)
                End If


                '            If mWithOutOrder = True Then						

                '                CboPONo.List(0) = xPoNo						
                '            End If						
            Case ColRecdQtyInKgs


                If VB.Left(cboRefType.Text, 1) = "R" Then
                    SprdMain.Row = mRow
                    SprdMain.Col = ColItemCode
                    xICode = Trim(SprdMain.Text)

                    mItemWeight = 0
                    If MainClass.ValidateWithMasterTable(xICode, "ITEM_CODE", "ITEM_WEIGHT", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mItemWeight = Val(MasterNo)
                    End If

                    SprdMain.Col = ColRecdQtyInKgs
                    mQtyKgs = Val(SprdMain.Text)

                    SprdMain.Col = ColReceivedQty
                    xQty = Val(SprdMain.Text)

                    If mQtyKgs <> 0 And xQty = 0 Then
                        If mItemWeight > 0 Then
                            xQty = Int(mQtyKgs * 1000 / mItemWeight)
                        End If
                    End If

                    If mQtyKgs = 0 And xQty <> 0 Then
                        If mItemWeight > 0 Then
                            mQtyKgs = VB6.Format(xQty * mItemWeight / 1000, "0.00")
                        End If
                    End If

                    SprdMain.Col = ColRecdQtyInKgs
                    SprdMain.Text = mQtyKgs

                    SprdMain.Col = ColReceivedQty
                    SprdMain.Text = xQty
                End If
            Case ColReceivedQty
                SprdMain.Row = mRow
                SprdMain.Col = ColPONo
                xPoNo = SprdMain.Text
                If mWithOutOrder = False Then
                    If xPoNo = "" Then Exit Sub
                End If

                SprdMain.Col = ColItemCode
                xICode = SprdMain.Text
                If xICode = "" Then Exit Sub

                If MainClass.ValidateWithMasterTable(xICode, "ITEM_CODE", "ITEM_CLASSIFICATION", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mItemClassType = IIf(IsDBNull(MasterNo), "B", MasterNo)
                Else
                    mItemClassType = "B"
                End If

                If CheckQty(eventArgs.col, eventArgs.row) = True Then
                    SprdMain.Col = ColReceivedQty
                    mQty = Val(SprdMain.Text)

                    If mItemClassType = "D" Then
                        If mQty > 100 Then
                            MsgInformation("Development Item cann't be received More than 100 unit. ")
                            MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColReceivedQty)
                            eventArgs.cancel = True
                            Exit Sub
                        End If
                    End If

                    SprdMain.Col = ColAcceptQty
                    If lblBookType.Text = "G" And chkQC.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                        SprdMain.Col = ColAcceptQty
                        SprdMain.Text = CStr(mQty)
                    Else
                        If Val(SprdMain.Text) = 0 Then
                            SprdMain.Col = ColAcceptQty
                            SprdMain.Text = CStr(mQty)
                        End If
                    End If

                    If VB.Left(cboRefType.Text, 1) = "R" Then
                        SprdMain.Row = mRow
                        SprdMain.Col = ColItemCode
                        xICode = Trim(SprdMain.Text)

                        mItemWeight = 0
                        If MainClass.ValidateWithMasterTable(xICode, "ITEM_CODE", "ITEM_WEIGHT", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mItemWeight = Val(MasterNo)
                        End If

                        SprdMain.Col = ColRecdQtyInKgs
                        mQtyKgs = Val(SprdMain.Text)

                        SprdMain.Col = ColReceivedQty
                        xQty = Val(SprdMain.Text)

                        If mQtyKgs <> 0 And xQty = 0 Then
                            If mItemWeight > 0 Then
                                xQty = Int(mQtyKgs * 1000 / mItemWeight)
                            End If
                        End If

                        If mQtyKgs = 0 And xQty <> 0 Then
                            If mItemWeight > 0 Then
                                mQtyKgs = VB6.Format(xQty * mItemWeight / 1000, "0.00")
                            End If
                        End If

                        SprdMain.Col = ColRecdQtyInKgs
                        SprdMain.Text = mQtyKgs

                        SprdMain.Col = ColReceivedQty
                        SprdMain.Text = xQty
                    End If
                Else
                    eventArgs.cancel = True
                    Exit Sub
                End If

                If mWithOutOrder = True Then
                    MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                    VB6.SetItemString(CboPONo, 0, xPoNo)
                End If

            Case ColAcceptQty
                SprdMain.Row = mRow
                SprdMain.Col = ColReceivedQty
                mQty = Val(SprdMain.Text)


                SprdMain.Col = ColAcceptQty
                mAcceptQty = Val(SprdMain.Text)

                If CheckBillQty(ColAcceptQty, eventArgs.row) = True Then
                    SprdMain.Col = ColReceivedQty
                    mQty = Val(SprdMain.Text)
                    MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                    FormatSprdMain(eventArgs.row)
                Else
                    eventArgs.cancel = True
                    Call MainClass.SetFocusToCell(SprdMain, mRow, ColReceivedQty)
                    Exit Sub
                End If


                If CheckApprovedQty(mRow) = False Then
                    MsgInformation("Accepted Qty Cann't be Less Than (Rework + Segregated + Deviation) Qty")
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColAcceptQty)
                    eventArgs.cancel = True
                    Exit Sub
                Else
                    If mQty >= mAcceptQty Then
                        SprdMain.Col = ColRejQty
                        SprdMain.Text = CStr(mQty - mAcceptQty)

                        MainClass.AddBlankSprdRow(SprdMain, ColPONo, ConRowHeight)
                        FormatSprdMain(eventArgs.row)
                        '                MainClass.SetFocusToCell SprdMain, Row, ColDiscount						
                    Else
                        MsgInformation("Accepted Qty Cann't be Greater Than Recieved Qty")
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColAcceptQty)
                        eventArgs.cancel = True
                        Exit Sub
                    End If
                End If
            Case ColDevQty
                If CheckApprovedQty(mRow) = False Then
                    MsgInformation("(Rework + Segregated + Deviation) Qty Cann't be Greater Than Accepted Qty")
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColDevQty)
                    eventArgs.cancel = True
                    Exit Sub
                End If
            Case ColSeg
                If CheckApprovedQty(mRow) = False Then
                    MsgInformation("(Rework + Segregated + Deviation) Qty Cann't be Greater Than Accepted Qty")
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColSeg)
                    eventArgs.cancel = True
                    Exit Sub
                End If
            Case ColRework
                If CheckApprovedQty(mRow) = False Then
                    MsgInformation("(Rework + Segregated + Deviation) Qty Cann't be Greater Than Accepted Qty")
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColRework)
                    eventArgs.cancel = True
                    Exit Sub
                End If
            Case ColConvQty
                If CheckApprovedQty(mRow) = False Then
                    MsgInformation("(Rework + Segregated + Deviation) Qty Cann't be Greater Than Accepted Qty")
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColConvQty)
                    eventArgs.cancel = True
                    Exit Sub
                End If
            Case ColRate
                ''Project PO						
                If mIsProjectPO = True Then
                    If CheckRate(eventArgs.col, eventArgs.row) = False Then
                        eventArgs.cancel = True
                        Exit Sub
                    End If
                Else
                    If VB.Left(cboRefType.Text, 1) = "P" Or VB.Left(cboRefType.Text, 1) = "I" Or VB.Left(cboRefType.Text, 1) = "2" Or VB.Left(cboRefType.Text, 1) = "3" Then
                        SprdMain.Row = mRow
                        SprdMain.Col = ColItemCode
                        xICode = Trim(SprdMain.Text)
                        If xICode = "" Then Exit Sub

                        SprdMain.Col = ColPORate
                        mPORate = Val(SprdMain.Text)

                        SprdMain.Col = ColRate
                        mBillRate = Val(SprdMain.Text)

                        If RsCompany.Fields("CHECK_PO_RATE").Value = "Y" Then
                            If mPORate <> mBillRate Then
                                MsgInformation("Bill Rate is not Match with PO Rate.")
                                MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColRate)
                                eventArgs.cancel = True
                                Exit Sub
                            End If
                        End If
                    End If
                End If
            Case ColStockType
                SprdMain.Row = mRow
                SprdMain.Col = ColStockType
                If Trim(SprdMain.Text) = "" Then Exit Sub

                If MainClass.ValidateWithMasterTable(SprdMain.Text, "STOCK_TYPE_CODE", "STOCK_TYPE_DESC", "INV_TYPE_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                    MsgInformation("InValid Stock Type")
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColStockType)
                    eventArgs.cancel = True
                    Exit Sub
                Else
                    If Trim(SprdMain.Text) = "RJ" Then
                        MsgInformation("You Cann't Select 'RJ' Stock Type.")
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColStockType)
                        eventArgs.cancel = True
                        Exit Sub
                    End If
                    If RsCompany.Fields("COMPANY_CODE").Value <> 9 Then
                        If chkUnderChallan.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                            If (VB.Left(cboRefType.Text, 1) = "J") And Trim(SprdMain.Text) = "ST" Then ''Or Left(cboRefType.Text, 1) = "1"						
                                MsgInformation("You Cann't Select 'ST' Stock Type for Jobwork Item.")
                                MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColStockType)
                                eventArgs.cancel = True
                                Exit Sub
                            End If
                        End If
                    End If
                End If
            Case ColQCEMP
                SprdMain.Row = mRow
                SprdMain.Col = ColQCEMP
                If Trim(SprdMain.Text) = "" Then Exit Sub
                'SprdMain.Text = VB6.Format(SprdMain.Text, "000000")
                If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 114 Then

                Else
                    If MainClass.ValidateWithMasterTable(SprdMain.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                        MsgInformation("InValid QC Employee")
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColQCEMP)
                        eventArgs.cancel = True
                        Exit Sub
                    End If
                End If

            Case ColBatchNo
                If lblBookType.Text = "Q" Then
                    SprdMain.Row = mRow
                    SprdMain.Col = ColItemCode
                    xICode = Trim(SprdMain.Text)
                    If xICode = "" Then Exit Sub

                    mLotNoRequied = "N"
                    If MainClass.ValidateWithMasterTable(xICode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mLotNoRequied = MasterNo
                    End If
                    If mLotNoRequied = "Y" Then
                        SprdMain.Col = ColBatchNo
                        If Trim(SprdMain.Text) = "" Then
                            MsgInformation("Lot No. Must For Such Item.")
                            MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColBatchNo)
                            eventArgs.cancel = True
                            Exit Sub
                        End If

                        If DuplicateLotNo(Trim(SprdMain.Text), xICode) = True Then
                            MsgInformation("Lot No. already Exists For Such Item.")
                            MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColBatchNo)
                            eventArgs.cancel = True
                            Exit Sub
                        End If

                    End If
                End If
        End Select
        Call CalcApprovedQty(eventArgs.row)
        Call CalcTots()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Function DuplicateItemCode(ByVal pRowNo As Integer) As Boolean

        Dim CntRow As Integer
        Dim mCount As Byte
        Dim mCheckItemCode As String
        Dim mItemCode As String
        Dim mPONo As Double
        Dim xCheckCode As String
        Dim mRGPCode As String
        'Dim mCheckRGPCode As String						

        With SprdMain
            .Row = pRowNo
            .Col = ColPONo
            mCheckItemCode = CStr(Val(.Text))

            .Col = ColItemCode
            mCheckItemCode = mCheckItemCode & Trim(UCase(.Text))

            If VB.Left(cboRefType.Text, 1) = "R" Then
                .Col = ColRGPItemCode
                mCheckItemCode = mCheckItemCode & Trim(UCase(.Text))
            End If

            For CntRow = 1 To .MaxRows
                .Row = CntRow

                .Col = ColPONo
                mPONo = Val(.Text)

                .Col = ColItemCode
                mItemCode = Trim(UCase(.Text))

                xCheckCode = mPONo & mItemCode

                If VB.Left(cboRefType.Text, 1) = "R" Then
                    .Col = ColRGPItemCode
                    mRGPCode = Trim(UCase(.Text))
                    xCheckCode = xCheckCode & mRGPCode
                End If

                If (xCheckCode = mCheckItemCode And mCheckItemCode <> "") Then
                    mCount = mCount + 1
                End If

                If mCount > 1 Then
                    DuplicateItemCode = True
                    MsgInformation("Duplicate Item : " & mItemCode & " For PoNo : " & mPONo)
                    MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColItemCode)
                    Exit Function
                End If
            Next
        End With
    End Function

    Private Function DuplicateLotNo(ByVal pLotNo As String, ByVal pItemCode As String) As Boolean

        Dim mRefNo As Double
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        DuplicateLotNo = False
        mRefNo = Val(txtMRRNo.Text)

        SqlStr = " SELECT LOT_NO " & vbCrLf & " FROM INV_PAINT_STOCK_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ITEM_CODE='" & pItemCode & "'" & vbCrLf & " AND LOT_NO='" & pLotNo & "' AND ITEM_IO='I'"

        If mRefNo <> 0 Then
            SqlStr = SqlStr & vbCrLf & " AND REF_NO<>" & mRefNo & "" '' & vbCrLf |                & " AND REF_TYPE='" & ConStockRefType_MRR & "'"						
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            DuplicateLotNo = True
        End If

    End Function
    Private Function FillGridRow(ByVal mPONo As String, ByVal mItemCode As String, ByVal pRowNo As Integer, ByVal mOutItemCode As String, ByVal mDivisionCode As Double) As Boolean

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim mOrdQty As Object
        Dim mRecvQty As Double
        Dim xPoNo As String
        Dim xFYNo As Integer
        Dim xSupplierCode As Integer
        Dim mOrderSno As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mQCEmpCode As String

        If mItemCode = "" Then Exit Function

        If VB.Left(cboRefType.Text, 1) = "P" And mPONo <> "" Then
            If MainClass.ValidateWithMasterTable(mPONo, "AUTO_KEY_PO", "PUR_TYPE", "PUR_PURCHASE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DIV_CODE=" & mDivisionCode & " AND PUR_TYPE IN ('P','R','L')") = True Then
                If MasterNo = "R" Then
                    mIsProjectPO = True
                Else
                    mIsProjectPO = False
                End If
            End If
        Else
            mIsProjectPO = False
        End If

        SqlStr = ""
        SqlStr = " SELECT Item_Code,ITEM_SHORT_DESC AS NAME," & vbCrLf & " PURCHASE_UOM, HSN_CODE " & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND Item_Code='" & MainClass.AllowSingleQuote(UCase(mItemCode)) & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMisc.EOF = False Then
            SprdMain.Row = pRowNo
            With RsMisc

                If mIsProjectPO = True Then
                    GoTo NextLoop
                Else
                    If CollectPOData(VB.Left(cboRefType.Text, 1), mPONo, mItemCode, mOutItemCode, (SprdMain.Row), mDivisionCode) = False Then
                        MsgInformation("Invalid Item Code for PONo " & mPONo)
                        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
                        FillGridRow = False
                        Exit Function
                    End If
                End If
                SprdMain.Row = SprdMain.ActiveRow
                Select Case VB.Left(cboRefType.Text, 1)
                    Case "P", "I", "1", "2", "3"

                    Case "R"
                        If GetOutJobworkManyItem(mItemCode, Trim(txtMRRDate.Text)) = True Then
                            GoTo NextLoop
                        End If
                    Case Else
NextLoop:
                        SprdMain.Row = pRowNo
                        SprdMain.Col = ColItemCode
                        SprdMain.Text = Trim(IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))

                        SprdMain.Col = ColItemName
                        SprdMain.Text = Trim(IIf(IsDBNull(.Fields("Name").Value), "", .Fields("Name").Value))

                        SprdMain.Col = ColHSNCode
                        SprdMain.Text = Trim(IIf(IsDBNull(.Fields("HSN_CODE").Value), "", .Fields("HSN_CODE").Value))

                        SprdMain.Col = ColUnit
                        SprdMain.Text = IIf(IsDBNull(.Fields("PURCHASE_UOM").Value), "", .Fields("PURCHASE_UOM").Value)



                        SprdMain.Col = ColBatchNo
                        '            SprdMain.text= IIf(IsNull(!ITEM_CODE), "", !ITEM_CODE)						

                        SprdMain.Col = ColPOQty
                        SprdMain.Text = IIf(Val(SprdMain.Text) = 0, 0, SprdMain.Text)

                        SprdMain.Col = ColBalQty
                        SprdMain.Text = IIf(Val(SprdMain.Text) = 0, 0, SprdMain.Text)

                        SprdMain.Col = ColStockType
                        If lblBookType.Text = "G" Then
                            If GetAutoQC(SprdMain.Text) = False Then
                                SprdMain.Text = "ST"
                            Else
                                SprdMain.Text = IIf(Trim(SprdMain.Text) = "", "QC", SprdMain.Text)
                            End If
                        Else
                            SprdMain.Text = IIf(Trim(SprdMain.Text) = "", "ST", SprdMain.Text)
                        End If

                        'SprdMain.Col = ColStockType
                        'If lblBookType.Text = "G" Then
                        '    SprdMain.Text = IIf(Trim(SprdMain.Text) = "", "QC", SprdMain.Text)
                        'Else
                        '    SprdMain.Text = IIf(Trim(SprdMain.Text) = "", "ST", SprdMain.Text)
                        'End If

                        '                    SqlStr = " SELECT QC_EMP_CODE FROM INV_ITEM_MST INVMST, INV_SUBCATEGORY_MST SMST " & vbCrLf _						
                        ''                            & " WHERE INVMST.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _						
                        ''                            & " AND INVMST.COMPANY_CODE=SMST.COMPANY_CODE " & vbCrLf _						
                        ''                            & " AND INVMST.CATEGORY_CODE=SMST.CATEGORY_CODE " & vbCrLf _						
                        ''                            & " AND INVMST.SUBCATEGORY_CODE=SMST.SUBCATEGORY_CODE " & vbCrLf _						
                        ''                            & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"						
                        '						
                        '                    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly						
                        '						
                        '                    If RsTemp.EOF = False Then						
                        '                        mQCEmpCode = IIf(IsNull(RsTemp!QC_EMP_CODE), "", RsTemp!QC_EMP_CODE)						
                        '                    End If						
                        mQCEmpCode = GetQCEmpCode(mItemCode)
                        SprdMain.Col = ColQCEMP
                        SprdMain.Text = VB6.Format(IIf(Trim(SprdMain.Text) = "", mQCEmpCode, Trim(SprdMain.Text)), "000000")

                End Select


            End With
            FillGridRow = True
        Else
            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            FillGridRow = False
        End If

        Exit Function
ERR1:
        FillGridRow = False
        MsgBox(Err.Description)
    End Function
    Private Function GetQCEmpCode(ByVal pItemCode As String) As String

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mDivisionCode As Double

        GetQCEmpCode = ""
        If Trim(cboDivision.Text) = "" Then
            MsgInformation("Please Select Division.")
            Exit Function
        End If
        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If



        SqlStr = " SELECT SMST.EMP_CODE FROM INV_ITEM_MST INVMST, INV_QCEMP_MST SMST, PAY_EMPLOYEE_MST EMP" & vbCrLf & " WHERE INVMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND INVMST.COMPANY_CODE=SMST.COMPANY_CODE " & vbCrLf & " AND INVMST.CATEGORY_CODE=SMST.CATEGORY_CODE " & vbCrLf & " AND INVMST.SUBCATEGORY_CODE=SMST.SUBCATEGORY_CODE " & vbCrLf & " AND SMST.COMPANY_CODE=EMP.COMPANY_CODE " & vbCrLf & " AND SMST.EMP_CODE=EMP.EMP_CODE " & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf & " AND SMST.DIV_CODE=" & mDivisionCode & ""

        SqlStr = SqlStr & vbCrLf & " AND EMP_DOJ <=TO_DATE(TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) " & vbCrLf & " AND (EMP_LEAVE_DATE >TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') OR EMP_LEAVE_DATE IS NULL) "


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetQCEmpCode = IIf(IsDBNull(RsTemp.Fields("EMP_CODE").Value), "", RsTemp.Fields("EMP_CODE").Value)
        End If


        Exit Function
ERR1:
        GetQCEmpCode = ""
        MsgBox(Err.Description)
    End Function
    Private Function CheckRate(ByVal Col As Integer, ByVal Row As Integer) As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mAmount As Double
        Dim mQty As Double
        Dim mRate As Double
        Dim mPONo As Double
        Dim mMRRNO As Double
        Dim mMRRAmount As Double
        Dim mPOAmount As Double

        With SprdMain

            .Row = Row
            .Col = ColPONo
            If Trim(.Text) = "" Then Exit Function
            mPONo = Val(.Text)

            .Col = ColReceivedQty
            mQty = Val(.Text)

            .Col = ColRate
            mRate = Val(.Text)

            mAmount = mQty * mRate

        End With

        mMRRNO = Val(txtMRRNo.Text)

        SqlStr = "SELECT SUM(APPROVED_QTY*ITEM_RATE) AS AMOUNT" & vbCrLf & " FROM INV_GATE_DET " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND REF_PO_NO=" & mPONo & ""

        If mMRRNO <> 0 Then
            SqlStr = SqlStr & vbCrLf & " AND AUTO_KEY_MRR<>" & mMRRNO & ""
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mMRRAmount = IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
        End If

        mMRRAmount = mMRRAmount + mAmount

        SqlStr = "SELECT SUM(ID.GROSS_AMT) AS AMOUNT" & vbCrLf & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.AUTO_KEY_PO=" & mPONo & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mPOAmount = IIf(IsDBNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
        End If

        If mPOAmount < mMRRAmount Then
            MsgInformation("MRR Amount Cann't be more Than PO Amount.")
            MainClass.SetFocusToCell(SprdMain, Row, ColRate)
            CheckRate = False
        Else
            CheckRate = True
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
        CheckRate = False
    End Function
    Private Function CheckApprovedQty(ByVal pRow As Integer) As Boolean
        On Error GoTo ERR1


        Dim mBillQty As Double
        Dim mReceivedQty As Double
        Dim mAcceptQty As Double
        Dim mApprovedQty As Double
        Dim mShortQty As Double
        Dim mRejQty As Double
        Dim mDevQty As Double
        Dim mSeg As Double
        Dim mRework As Double
        Dim mConvQty As Double

        CheckApprovedQty = True
        With SprdMain

            .Row = pRow

            .Col = ColAcceptQty
            mAcceptQty = Val(.Text)

            .Col = ColDevQty
            mDevQty = Val(.Text)

            .Col = ColSeg
            mSeg = Val(.Text)

            .Col = ColRework
            mRework = Val(.Text)

            .Col = ColConvQty
            mConvQty = Val(.Text)

            If mAcceptQty < mDevQty + mSeg + mRework + mConvQty Then
                CheckApprovedQty = False
            End If

        End With


        Exit Function
ERR1:
        MsgInformation(Err.Description)
        CheckApprovedQty = False
    End Function
    Private Function CheckQty(ByVal Col As Integer, ByVal Row As Integer) As Boolean

        On Error GoTo ERR1
        Dim mPOQty As Double
        Dim mBillQty As Double
        Dim mEXQty As Double
        With SprdMain

            'sk   '25-10-2004						
            '    If mWithOutOrder = True Then CheckQty = True: Exit Function						

            .Row = Row
            .Col = ColPOQty
            mPOQty = Val(.Text)

            .Col = ColBillQty
            mBillQty = Val(.Text)

            mEXQty = mBillQty * IIf(IsDBNull(RsCompany.Fields("GRExcessPer").Value), 0, RsCompany.Fields("GRExcessPer").Value) / 100
            .Col = ColReceivedQty

            If Val(.Text) > mBillQty Then
                MsgInformation("Receipt Qty can not be greater than Bill Qty") ' & RsCompany!GRExcessPer & "%"						
                MainClass.SetFocusToCell(SprdMain, Row, Col)
                CheckQty = False
            Else
                If Val(.Text) > mBillQty + mEXQty Then
                    MsgInformation("Receipt Qty can not be greater than PO Qty") ' & RsCompany!GRExcessPer & "%"						
                    MainClass.SetFocusToCell(SprdMain, Row, Col)
                    CheckQty = False
                Else
                    CheckQty = True
                End If
            End If
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function CheckBillQty(ByVal Col As Integer, ByVal Row As Integer) As Boolean

        On Error GoTo ERR1
        Dim mPOQty As Double
        Dim mBalQty As Double
        Dim mEXQty As Double
        Dim mItemCode As String
        With SprdMain

            If mWithOutOrder = True Then CheckBillQty = True : Exit Function
            If mIsProjectPO = True Then CheckBillQty = True : Exit Function

            If RsCompany.Fields("StockBalCheck").Value = "N" And VB.Left(cboRefType.Text, 1) = "I" Then
                CheckBillQty = True
                Exit Function
            End If

            If VB.Left(cboRefType.Text, 1) = "I" Then
                .Row = Row
                .Col = ColPONo
                If Val(.Text) < 0 Then
                    CheckBillQty = True
                    Exit Function
                End If
            End If

            .Row = Row
            .Col = ColItemCode
            mItemCode = Trim(.Text)

            If VB.Left(cboRefType.Text, 1) = "R" Then
                If GetOutJobworkManyItem(mItemCode, Trim(txtMRRDate.Text)) = True Then
                    CheckBillQty = True : Exit Function
                End If
            End If

            If GetItemLocking(mItemCode) = True Then CheckBillQty = False : Exit Function

            '    CheckBillQty = True						
            '    Exit Function						
            '						
            .Col = ColPOQty
            mPOQty = Val(.Text)

            .Col = ColBalQty
            mBalQty = Val(.Text)

            mEXQty = 0
            If GetProductionType(mItemCode) = "R" Then
                If mBalQty > 0 Then
                    mEXQty = (mBalQty * IIf(IsDBNull(RsCompany.Fields("GRExcessPer").Value), 0, RsCompany.Fields("GRExcessPer").Value) / 100)
                Else
                    mEXQty = 0
                End If
            End If

            .Col = Col
            '    .Col = ColAcceptQty						
            If Val(.Text) = 0 Then CheckBillQty = True : Exit Function

            If Val(.Text) > mBalQty + mEXQty Then
                MsgInformation("Qty can not be greater than Balance Qty (" & mBalQty & ") : " & mItemCode) ' & RsCompany!GRExcessPer & "%"						
                MainClass.SetFocusToCell(SprdMain, Row, Col)
                CheckBillQty = False
            Else

                CheckBillQty = True
            End If
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function GetMRRItemQty(ByVal mItemCode As String) As Double

        On Error GoTo ERR1
        Dim CntRow As Integer
        Dim mCheckItemCode As String
        Dim mPurchaseUOM As String = ""
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mIssueUOM As String = ""
        Dim mFactor As Double
        GetMRRItemQty = 0

        With SprdMain
            For CntRow = 1 To .MaxRows
                .Row = CntRow
                .Col = ColItemCode
                mCheckItemCode = Trim(.Text)

                If Trim(UCase(mCheckItemCode)) = Trim(UCase(mItemCode)) Then
                    .Col = ColUnit
                    mPurchaseUOM = Trim(.Text)

                    .Col = ColBillQty
                    GetMRRItemQty = GetMRRItemQty + Val(.Text)
                End If
            Next
        End With

        If GetMRRItemQty <> 0 Then

            SqlStr = " SELECT ISSUE_UOM, PURCHASE_UOM, UOM_FACTOR FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                mIssueUOM = Trim(IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value))
                '            mPurchaseUOM = IIf(IsNull(RsTemp!PURCHASE_UOM), "", RsTemp!PURCHASE_UOM)						
                mFactor = IIf(IsDBNull(RsTemp.Fields("UOM_FACTOR").Value) Or RsTemp.Fields("UOM_FACTOR").Value = 0, 1, RsTemp.Fields("UOM_FACTOR").Value)

                If mPurchaseUOM = mIssueUOM Then

                Else
                    GetMRRItemQty = GetMRRItemQty * mFactor
                End If
            End If
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub SprdMain_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles SprdMain.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim mCancel As Boolean
        mCancel = False
        'With SprdMain
        '    SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, mCancel))
        '    Cancel = mCancel
        'End With
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub UltraGrid1_DoubleClick(sender As Object, e As EventArgs) Handles UltraGrid1.DoubleClick

        Dim mMRRNo As String

        Dim mRow As UltraGridRow

        If Me.UltraGrid1.ActiveRow.Index < 0 Then Exit Sub
        mRow = Me.UltraGrid1.Rows(Me.UltraGrid1.ActiveRow.Index)

        mMRRNo = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(1))

        txtMRRNo.Text = CStr(Val(mMRRNo))

        TxtMRRNo_Validating(txtMRRNo, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())

    End Sub
    'Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent)
    '    With SprdView
    '        .Row = eventArgs.row

    '        .Col = 2
    '        txtMRRNo.Text = CStr(Val(.Text))

    '        TxtMRRNo_Validating(txtMRRNo, New System.ComponentModel.CancelEventArgs(False))
    '        CmdView_Click(CmdView, New System.EventArgs())
    '    End With
    'End Sub

    Private Sub txtBillDate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBillDate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.CheckDateKey(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtBillDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBillDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim mEntryDate As String
        Dim mDivisionCode As Double

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            GoTo EventExitSub
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        If Trim(txtBillDate.Text) = "" Then
            GoTo EventExitSub
        End If


        If Not IsDate(txtBillDate.Text) Then
            MsgInformation("Invaild Bill Date.")
            Cancel = True
            GoTo EventExitSub
        End If

        If CDate(txtBillDate.Text) > CDate(txtMRRDate.Text) Then
            MsgInformation("Bill Date cann't be greater than MRR Date.")
            Cancel = True
            GoTo EventExitSub
        End If

        If Trim(TxtSupplier.Text) = "" Then
            MsgInformation("Please Select Supplier Name First.")
            txtBillDate.Text = ""
            Cancel = False
            If TxtSupplier.Enabled = True Then TxtSupplier.Focus()
            GoTo EventExitSub
        End If

        If CheckRefDate(mDivisionCode) = False Then
            Cancel = True
            GoTo EventExitSub
        End If

        '        If ADDMode = False Then Exit Sub						
        '        If Left(cboRefType.Text, 1) <> "P" Then Exit Sub						
        '						
        '        PubDBCn.Errors.Clear						
        '        PubDBCn.BeginTrans						
        '						
        '						
        '        mEntryDate = VB6.Format(GetServerDate, "DD-MMM-YYYY") & " " & GetServerTime						
        '						
        '        mSqlStr = " INSERT INTO TEMP_INV_GATE_TRN " & vbCrLf _						
        ''                & " (COMPANY_CODE, MRR_DATE, SUPP_CUST_NAME, " & vbCrLf _						
        ''                & " BILL_NO, BILL_DATE, ADDUSER, ADDDATE) VALUES (" & vbCrLf _						
        ''                & " " & RsCompany.fields("COMPANY_CODE").value & ", TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _						
        ''                & " '" & MainClass.AllowSingleQuote(TxtSupplier.Text) & "', '" & MainClass.AllowSingleQuote(txtBillNo.Text) & "'," & vbCrLf _						
        ''                & " '" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "'," & vbCrLf _						
        ''                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & mEntryDate & "','DD-MON-YYYY HH24:MI'))"						
        '						
        '        PubDBCn.Execute mSqlStr						
        '						
        '						
        '        PubDBCn.CommitTrans						

        pTempUpdate = False
        GoTo EventExitSub
ErrPart:
        '    PubDBCn.RollbackTrans						
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub UpdateTempFile()
        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim mEntryDate As String

        If pTempUpdate = True Then Exit Sub

        If ADDMode = False Then Exit Sub
        If VB.Left(cboRefType.Text, 1) <> "P" Then Exit Sub

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()


        mEntryDate = VB6.Format(GetServerDate, "DD-MMM-YYYY") & " " & GetServerTime()

        mSqlStr = " INSERT INTO TEMP_INV_GATE_TRN " & vbCrLf & " (COMPANY_CODE, MRR_DATE, SUPP_CUST_NAME, " & vbCrLf & " BILL_NO, BILL_DATE, ADDUSER, ADDDATE) VALUES (" & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & MainClass.AllowSingleQuote((TxtSupplier.Text)) & "', '" & MainClass.AllowSingleQuote((txtBillNo.Text)) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & mEntryDate & "','DD-MON-YYYY HH24:MI'))"

        PubDBCn.Execute(mSqlStr)


        PubDBCn.CommitTrans()
        pTempUpdate = True

        Exit Sub
ErrPart:
        PubDBCn.RollbackTrans()
    End Sub
    Private Sub txtBillNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBillNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim mEntryDate As String
        Dim pErrorMsg As String = ""
        If ValidateBillNo((txtBillNo.Text), pErrorMsg) = False Then
            MsgInformation(pErrorMsg)
            Cancel = True
            GoTo EventExitSub
        End If

        '        If ADDMode = False Then Exit Sub						
        '        If Left(cboRefType.Text, 1) <> "P" Then Exit Sub						
        '						
        '        PubDBCn.Errors.Clear						
        '        PubDBCn.BeginTrans						
        '						
        '						
        '        mEntryDate = VB6.Format(GetServerDate, "DD-MMM-YYYY") & " " & GetServerTime						
        '						
        '        mSqlStr = " INSERT INTO TEMP_INV_GATE_TRN " & vbCrLf _						
        ''                & " (COMPANY_CODE, MRR_DATE, SUPP_CUST_NAME, " & vbCrLf _						
        ''                & " BILL_NO, BILL_DATE, ADDUSER, ADDDATE) VALUES (" & vbCrLf _						
        ''                & " " & RsCompany.fields("COMPANY_CODE").value & ", TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _						
        ''                & " '" & MainClass.AllowSingleQuote(TxtSupplier.Text) & "', '" & MainClass.AllowSingleQuote(txtBillNo.Text) & "'," & vbCrLf _						
        ''                & " '" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "'," & vbCrLf _						
        ''                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & mEntryDate & "','DD-MON-YYYY HH24:MI'))"						
        '						
        '        PubDBCn.Execute mSqlStr						
        '						
        '						
        '        PubDBCn.CommitTrans						

        pTempUpdate = False
        GoTo EventExitSub
ErrPart:
        '    PubDBCn.RollbackTrans						
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDocsThru_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDocsThru.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtEwayBillNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEwayBillNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtEwayBillNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEwayBillNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtFormDetail_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFormDetail.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtFormDetail_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFormDetail.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtFormDetail.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtFreight_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFreight.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtFreight_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFreight.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtGateNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGateNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtGateNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGateNo.DoubleClick
        If lblBookType.Text = "G" Then cmdGateSearch_Click(cmdGateSearch, New System.EventArgs())
    End Sub

    Private Sub txtGateNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtGateNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtGateNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtGateNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdMRRSearch_Click(cmdMRRSearch, New System.EventArgs())
    End Sub

    Public Sub txtGateNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtGateNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsGateMain As ADODB.Recordset = Nothing

        If Trim(txtGateNo.Text) = "" Then GoTo EventExitSub

        If Len(txtGateNo.Text) < 6 Then
            txtGateNo.Text = Val(txtGateNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        SqlStr = " SELECT * FROM INV_GATEENTRY_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_GATE,LENGTH(AUTO_KEY_GATE)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_GATE=" & Val(txtGateNo.Text) & " AND MRR_MADE='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGateMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsGateMain.EOF = False Then
            Clear1()
            Call ShowFromGateEntry(RsGateMain)
        Else
            MsgBox("No Such Gate Entry.", MsgBoxStyle.Information)
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub TxtGRDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGRDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtGRDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtGRDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtGRDate.Text) = "" Then
            GoTo EventExitSub
        End If


        If Not IsDate(txtGRDate.Text) Then
            MsgInformation("Invaild GR Date.")
            Cancel = True
            GoTo EventExitSub
        End If

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub TxtGRNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGRNo.TextChanged

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

    Private Sub chkShipTo_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkShipTo.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        If chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtShippedTo.Enabled = False
            cmdSearchShippedTo.Enabled = False
        Else

            If RsCompany.Fields("MRR_AGT_GE").Value = "Y" Then
                txtShippedTo.Enabled = False
                cmdSearchShippedTo.Enabled = False
            Else
                txtShippedTo.Enabled = True
                cmdSearchShippedTo.Enabled = True
            End If
        End If

    End Sub

    Private Sub txtShippedTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtShippedTo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
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


    Private Sub txtTripDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTripDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTripDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTripDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtTripDate.Text) = "" Then
            GoTo EventExitSub
        End If


        If Not IsDate(txtTripDate.Text) Then
            MsgInformation("Invalid Trip Date.")
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtTripNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTripNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTripNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTripNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtTripNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtVehicle_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVehicle.DoubleClick
        SearchVehicleMaster()
    End Sub

    Private Sub txtVehicle_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVehicle.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtVehicle.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtGRNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtGRNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtGRNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtGRDate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtGRDate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtGRDate.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtItemDesc_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtItemDesc.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtItemDesc_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtItemDesc.DoubleClick
        SearchItemDesc()
    End Sub


    Private Sub txtItemDesc_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtItemDesc.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtItemDesc.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtItemDesc_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtItemDesc.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchItemDesc()
    End Sub

    Private Sub txtItemDesc_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtItemDesc.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(TxtItemDesc.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable((TxtItemDesc.Text), "NAME", "NAME", "FIN_ITEMTYPE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgInformation("Please Press F1 OR Double Click For Valid Item Desc.")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub cboMode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboMode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMRRDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMRRDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMRRDate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMRRDate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.CheckDateKey(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtMRRDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMRRDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If Not IsDate(txtMRRDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If

        If FYChk((txtMRRDate.Text)) = False Then
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub TxtMRRNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMRRNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtMRRNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMRRNo.DoubleClick
        If lblBookType.Text = "Q" Then cmdMRRSearch_Click(cmdMRRSearch, New System.EventArgs())
    End Sub

    Private Sub TxtMRRNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMRRNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub TxtMRRNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtMRRNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdMRRSearch_Click(cmdMRRSearch, New System.EventArgs())
    End Sub

    Public Sub TxtMRRNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMRRNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMkey As String = ""
        Dim mMRRNO As String
        Dim SqlStr As String = ""

        If Trim(txtMRRNo.Text) = "" Then GoTo EventExitSub

        If Len(txtMRRNo.Text) < 6 Then
            txtMRRNo.Text = Val(txtMRRNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        If MODIFYMode = True And RsMRRMain.EOF = False Then xMkey = RsMRRMain.Fields("AUTO_KEY_MRR").Value
        mMRRNO = Trim(txtMRRNo.Text)

        SqlStr = " SELECT * FROM INV_GATE_HDR " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_MRR,LENGTH(AUTO_KEY_MRR)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND AUTO_KEY_MRR=" & Val(mMRRNO) & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMRRMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsMRRMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such MRR, Use Generate MRR Option To add", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM INV_GATE_HDR " & " WHERE AUTO_KEY_MRR=" & Val(xMkey) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMRRMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Function CheckAutoQC() As String
        On Error GoTo ErrPart
        Dim cntRow As Long
        Dim mAutoQC As Boolean

        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColStockType

                .Col = ColItemCode
                mAutoQC = GetAutoQC(.Text)

                If mAutoQC = False Then
                    .Col = ColStockType
                    If Trim(.Text) = "QC" Then
                        CheckAutoQC = "N"
                        Exit Function
                    End If
                End If
            Next
            CheckAutoQC = "Y"
        End With
        Exit Function
ErrPart:
        CheckAutoQC = "N"
    End Function

    Private Function UpdateMain1() As Boolean

        On Error GoTo ErrPart
        Dim I As Short
        Dim SqlStr As String = ""
        Dim nMkey As String
        Dim mVNoSeq As Double
        Dim mSuppCustCode As String
        Dim mTotQty As Double
        Dim mCancelled As String
        Dim mPONOs As String = ""
        Dim mQCStatus As String
        Dim mEntryDate As String
        Dim mIssued As String
        Dim mIssueNoteNo As String = ""
        Dim mJobWorkItems As String
        Dim CntRow As Integer
        Dim mItemCode As String
        Dim mCatCode As String = ""
        Dim mAutoIssue As String
        Dim mFreightType As Integer
        Dim xAutoIssueCheck As Boolean
        Dim mPONo As String

        Dim mIssueAsPerIndent As String
        Dim mIssueSSAsPerIndent As String
        Dim mDivisionCode As Double
        Dim mItemClassification As String
        Dim mShippedToSame As String
        Dim mShippedToCode As String = ""
        Dim mUnderChallan As String
        Dim mInterUnit As String = "N"
        Dim mDeliveryToCode As String = ""
        Dim mDeliveryToLoc As String = ""

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mEntryDate = VB6.Format(GetServerDate, "DD-MMM-YYYY") & " " & GetServerTime()

        xAutoIssueCheck = CheckAutoIssue(VB6.Format(PubCurrDate, "DD/MM/YYYY"), "")

        mSuppCustCode = CStr(-1)
        If MainClass.ValidateWithMasterTable((TxtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSuppCustCode = MasterNo
        Else
            mSuppCustCode = CStr(-1)
            MsgBox("Supplier Does Not Exist In Master", MsgBoxStyle.Information)
            GoTo ErrPart
        End If

        mInterUnit = "N"
        If MainClass.ValidateWithMasterTable((TxtSupplier.Text), "SUPP_CUST_NAME", "INTER_UNIT", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mInterUnit = MasterNo
        End If

        For I = 0 To CboPONo.Items.Count - 1
            mPONOs = IIf(mPONOs = "", mPONOs & VB6.GetItemString(CboPONo, I), mPONOs & "," & VB6.GetItemString(CboPONo, I))
        Next


        If MainClass.ValidateWithMasterTable((cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Val(MasterNo)
        Else
            mDivisionCode = -1
            MsgBox("Division Does Not Exist In Master", MsgBoxStyle.Information)
            GoTo ErrPart
        End If

        mCancelled = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mQCStatus = IIf(chkQC.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        If lblBookType.Text = "G" And lblSaleReturn.Text = "Y" Then
        Else
            If mQCStatus = "N" Then
                If lblBookType.Text = "Q" Then
                    mQCStatus = CheckStockType()
                Else
                    mQCStatus = CheckAutoQC()
                End If

                chkQC.CheckState = IIf(CheckAutoQC() = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            End If
        End If
        If lblBookType.Text = "Q" Then
            mQCStatus = CheckStockType()
        End If


        If Val(txtMRRNo.Text) = 0 Then
            mVNoSeq = CDbl(AutoGenSeqNo(mDivisionCode))
        Else
            mVNoSeq = Val(txtMRRNo.Text)
        End If

        txtMRRNo.Text = CStr(Val(CStr(mVNoSeq)))

        If lblBookType.Text <> "Q" Then
            If CheckValidVDate(mVNoSeq, mDivisionCode) = False Then GoTo ErrPart
        End If

        SqlStr = ""

        If OptFreight(0).Checked = True Then
            mFreightType = 0
        Else
            mFreightType = 1
        End If


        mShippedToSame = IIf(chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mUnderChallan = IIf(chkUnderChallan.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        If mShippedToSame = "Y" Then
            mShippedToCode = mSuppCustCode
        Else
            If MainClass.ValidateWithMasterTable((txtShippedTo.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mShippedToCode = MasterNo
            End If
        End If

        If txtDeliveryTo.Text = "" Then
            mDeliveryToCode = ""
            mDeliveryToLoc = ""
        Else
            If MainClass.ValidateWithMasterTable((txtDeliveryTo.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDeliveryToCode = MasterNo
            End If
            mDeliveryToLoc = txtDeliveryToLoc.Text
        End If

        If ADDMode = True Then
            LblMkey.Text = CStr(mVNoSeq)
            SqlStr = "INSERT INTO INV_GATE_HDR( " & vbCrLf & " COMPANY_CODE, AUTO_KEY_MRR, MRR_DATE," & vbCrLf & " SUPP_CUST_CODE, BILL_NO, BILL_DATE," & vbCrLf & " REF_DOC_NO, REF_DOC_DATE, REF_TYPE," & vbCrLf & " REF_AUTO_KEY_NO, REF_DATE, PO_NO," & vbCrLf & " PO_DATE, NO_ST38, TRANSPORT_MODE," & vbCrLf & " REMARKS, PRE_EMP_CODE, FREIGHT_CHARGES," & vbCrLf & " ASSESS_AMT, EXCISE_PER, EXCISE_AMT," & vbCrLf & " DISCOUNT_PER, DISCOUNT_AMT, TAXABLE_AMT," & vbCrLf & " SALETAX_PER, SALETAX_AMT, FREIGHT_AMT," & vbCrLf & " INVOICE_AMT, FORM_DETAILS, QC_STATUS," & vbCrLf & " EXCISE_STATUS, SERV_STATUS, SALETAX_STATUS, MRR_FINAL_FLAG," & vbCrLf & " DESCR_FLAG, PACK_MAT_FLAG, CHALLAN_MADE," & vbCrLf & " SCHLD_RTN_FLAG, MRR_STATUS, ITEM_DETAILS, " & vbCrLf & " QC_DATE, SEND_AC_FLAG, SEND_AC_DATE,TOTEDUPERCENT,TOTEDUAMOUNT, IS_ISSUED," & vbCrLf & " ADDUSER, ADDDATE, MODUSER, MODDATE,UPDATE_FROM, " & vbCrLf & " FREIGHT_TYPE, MODE_TYPE, DOCS_THRU, VEHICLE," & vbCrLf _
                & " GRNO, GRDATE, DIV_CODE, TRIP_NO, TRIP_DATE, GATE_ENTRY, GATEDATE,SHIPPED_TO_SAMEPARTY, SHIPPED_TO_PARTY_CODE,PARTY_EWAYBILLNO,UNDER_CHALLAN,BILL_TO_LOC_ID,SHIP_TO_LOC_ID,DELIVERY_TO,DELIVERY_TO_LOC_ID ) "


            SqlStr = SqlStr & vbCrLf & " VALUES(" & RsCompany.Fields("COMPANY_CODE").Value & ", " & Val(CStr(mVNoSeq)) & ", TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & MainClass.AllowSingleQuote(mSuppCustCode) & "', '" & MainClass.AllowSingleQuote((txtBillNo.Text)) & "', TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '','','" & VB.Left(cboRefType.Text, 1) & "'," & vbCrLf & " '" & mPONOs & "','','', " & vbCrLf & " '', '" & Trim(txtST38No.Text) & "', '" & MainClass.AllowSingleQuote((TxtTransporter.Text)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote((TxtRemarks.Text)) & "', '" & MainClass.AllowSingleQuote(PubUserID) & "', '" & MainClass.AllowSingleQuote((txtFreight.Text)) & "', " & vbCrLf & " " & Val(lblTotItemValue.Text) & ", " & Val(lblEDPercentage.Text) & ", " & Val(lblCGST.Text) & "," & vbCrLf & " 0," & Val(lblDiscount.Text) & "," & Val(lblTotTaxableAmt.Text) & ", " & vbCrLf & " " & Val(lblSTPercentage.Text) & "," & Val(lblSGST.Text) & "," & Val(lblTotFreight.Text) & "," & vbCrLf & "  " & Val(lblNetAmount.Text) & ",'" & MainClass.AllowSingleQuote((txtFormDetail.Text)) & "','" & mQCStatus & "'," & vbCrLf & " 'N','N','N','N', " & vbCrLf & " 'N','N','N', " & vbCrLf & " 'N','" & mCancelled & "', '" & MainClass.AllowSingleQuote((TxtItemDesc.Text)) & "', " & vbCrLf & " '','N',''," & Val(lblEDUPercent.Text) & ", " & Val(lblEDUAmount.Text) & ", 'N', " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & mEntryDate & "','DD-MON-YYYY HH24:MI'),'','','H'," & vbCrLf & " " & mFreightType & ", '" & MainClass.AllowSingleQuote((cboMode.Text)) & "', '" & MainClass.AllowSingleQuote((txtDocsThru.Text)) & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote((txtVehicle.Text)) & "', '" & MainClass.AllowSingleQuote((txtGRNo.Text)) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(txtGRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & mDivisionCode & "," & vbCrLf & " '" & MainClass.AllowSingleQuote((txtTripNo.Text)) & "',TO_DATE('" & VB6.Format(txtTripDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " " & Val(txtGateNo.Text) & " , TO_DATE('" & VB6.Format(txtGateDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & mShippedToSame & "','" & mShippedToCode & "'," & Val(txtEwayBillNo.Text) & ",'" & mUnderChallan & "','" & MainClass.AllowSingleQuote(txtBillTo.Text) & "','" & MainClass.AllowSingleQuote(TxtShipTo.Text) & "','" & MainClass.AllowSingleQuote(mDeliveryToCode) & "','" & MainClass.AllowSingleQuote(mDeliveryToLoc) & "')"

        ElseIf MODIFYMode = True Then
            SqlStr = ""
            SqlStr = "UPDATE INV_GATE_HDR SET " & vbCrLf & " AUTO_KEY_MRR =" & Val(CStr(mVNoSeq)) & " ," & vbCrLf & " MRR_DATE=TO_DATE(TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))," & vbCrLf & " SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSuppCustCode) & "'," & vbCrLf & " BILL_NO='" & MainClass.AllowSingleQuote((txtBillNo.Text)) & "'," & vbCrLf & " BILL_DATE=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " REF_DOC_NO=" & Val(CboPONo.Text) & "," & vbCrLf & " REF_DOC_DATE=''," & vbCrLf & " REF_TYPE='" & VB.Left(cboRefType.Text, 1) & "'," & vbCrLf & " REF_AUTO_KEY_NO='" & mPONOs & "'," & vbCrLf & " REF_DATE=''," & vbCrLf & " PO_NO=''," & vbCrLf & " PO_DATE='', UNDER_CHALLAN='" & mUnderChallan & "'," & vbCrLf & " NO_ST38='" & Trim(txtST38No.Text) & "', PARTY_EWAYBILLNO=" & Val(txtEwayBillNo.Text) & "," & vbCrLf & " TRANSPORT_MODE='" & MainClass.AllowSingleQuote((TxtTransporter.Text)) & "'," & vbCrLf & " REMARKS='" & MainClass.AllowSingleQuote((TxtRemarks.Text)) & "',DIV_CODE=" & mDivisionCode & ","

            SqlStr = SqlStr & vbCrLf & " QC_STATUS = '" & mQCStatus & "',FREIGHT_CHARGES= '" & MainClass.AllowSingleQuote(txtFreight.Text) & "'," & vbCrLf & " ASSESS_AMT= " & Val(lblTotItemValue.Text) & ", " & vbCrLf & " EXCISE_PER= " & Val(lblEDPercentage.Text) & ", " & vbCrLf & " EXCISE_AMT= " & Val(lblCGST.Text) & "," & vbCrLf & " DISCOUNT_PER= 0, " & vbCrLf & " DISCOUNT_AMT= " & Val(lblDiscount.Text) & "," & vbCrLf & " TAXABLE_AMT= " & Val(lblTotTaxableAmt.Text) & ", " & vbCrLf & " SALETAX_PER= " & Val(lblSTPercentage.Text) & "," & vbCrLf & " SALETAX_AMT= " & Val(lblSGST.Text) & "," & vbCrLf & " FREIGHT_AMT= " & Val(lblTotFreight.Text) & "," & vbCrLf & " INVOICE_AMT= " & Val(lblNetAmount.Text) & ",MRR_STATUS='" & mCancelled & "'," & vbCrLf & " FORM_DETAILS='" & MainClass.AllowSingleQuote(txtFormDetail.Text) & "'," & vbCrLf & " ITEM_DETAILS='" & MainClass.AllowSingleQuote(TxtItemDesc.Text) & "', " & vbCrLf & " TOTEDUPERCENT= " & Val(lblEDUPercent.Text) & ", " & vbCrLf & " TOTEDUAMOUNT= " & Val(lblEDUAmount.Text) & ", UPDATE_FROM='H'," & vbCrLf & " FREIGHT_TYPE=" & mFreightType & ", " & vbCrLf & " MODE_TYPE='" & MainClass.AllowSingleQuote(cboMode.Text) & "', " & vbCrLf & " DOCS_THRU='" & MainClass.AllowSingleQuote(txtDocsThru.Text) & "', " & vbCrLf & " VEHICLE='" & MainClass.AllowSingleQuote(txtVehicle.Text) & "', " & vbCrLf & " GRNO='" & MainClass.AllowSingleQuote(txtGRNo.Text) & "', " & vbCrLf & " GRDATE=TO_DATE('" & VB6.Format(txtGRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " GATE_ENTRY=" & Val(txtGateNo.Text) & " , GATEDATE=TO_DATE('" & VB6.Format(txtGateDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),"


            SqlStr = SqlStr & vbCrLf & " TRIP_NO='" & MainClass.AllowSingleQuote(txtTripNo.Text) & "', " & vbCrLf _
                & " BILL_TO_LOC_ID='" & MainClass.AllowSingleQuote((txtBillTo.Text)) & "', " & vbCrLf _
                & " SHIP_TO_LOC_ID='" & MainClass.AllowSingleQuote((TxtShipTo.Text)) & "', " & vbCrLf _
                & " TRIP_DATE=TO_DATE('" & VB6.Format(txtTripDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " SHIPPED_TO_SAMEPARTY='" & mShippedToSame & "', SHIPPED_TO_PARTY_CODE='" & mShippedToCode & "', " & vbCrLf _
                & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),DELIVERY_TO='" & MainClass.AllowSingleQuote(mDeliveryToCode) & "',DELIVERY_TO_LOC_ID = '" & MainClass.AllowSingleQuote(mDeliveryToLoc) & "' " & vbCrLf _
                & " WHERE AUTO_KEY_MRR ='" & MainClass.AllowSingleQuote(LblMkey.Text) & "'"
        End If

        PubDBCn.Execute(SqlStr)
        If UpdateDetail1(IIf(chkRejRtn.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N"), mSuppCustCode, Val(CboPONo.Text), mDivisionCode) = False Then GoTo ErrPart

        If lblBookType.Text = "G" And RsCompany.Fields("MRR_AGT_GE").Value = "N" And cboRefType.SelectedIndex = 2 Then
            If UpdateSRTRN() = False Then GoTo ErrPart
        End If

        '    If chkDNote.Value = vbUnchecked Then						
        If UpdateDiscrepancyNote(mSuppCustCode) = False Then GoTo ErrPart
        '    End If						


        mJobWorkItems = "N"
        mIssueAsPerIndent = "N"
        mIssueSSAsPerIndent = "N"

        If RsMRRMain.EOF = False Then
            mIssued = IIf(IsDBNull(RsMRRMain.Fields("IS_ISSUED").Value), "N", RsMRRMain.Fields("IS_ISSUED").Value)
        Else
            mIssued = "N"
        End If

        If lblBookType.Text = "Q" And chkQC.Enabled = True And mQCStatus = "Y" And mIssued = "N" Then
            For CntRow = 1 To SprdMain.MaxRows - 1

                mAutoIssue = "N"
                mIssueAsPerIndent = "N"
                mIssueSSAsPerIndent = "N"
                mJobWorkItems = "N"

                SprdMain.Row = CntRow
                SprdMain.Col = ColPONo
                mPONo = Trim(SprdMain.Text)

                SprdMain.Col = ColItemCode
                mItemCode = Trim(SprdMain.Text)

                SprdMain.Col = ColStockType
                If Trim(SprdMain.Text) = "CS" Then

                    If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 And mInterUnit = "Y" Then
                        SprdMain.Col = ColStockType
                        SprdMain.Text = "ST"
                        mJobWorkItems = "N"
                    Else
                        If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "CATEGORY_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mCatCode = MasterNo
                        Else
                            mCatCode = "-1"
                        End If

                        If MainClass.ValidateWithMasterTable(mCatCode, "GEN_CODE", "PRD_TYPE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND PRD_TYPE='J'") = True Then
                            mJobWorkItems = "Y"
                            If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "AUTO_INDENT", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AUTO_INDENT='Y'") = True Then
                                mJobWorkItems = MasterNo
                            Else
                                mJobWorkItems = "N"
                            End If
                            '                Exit For						
                        Else
                            mJobWorkItems = "N"
                        End If
                    End If
                Else
                    If VB.Left(cboRefType.Text, 1) = "P" Then
                        If GetProductionType(mItemCode) = "G" Or GetProductionType(mItemCode) = "T" Or GetProductionType(mItemCode) = "A" Then
                            If mIssueAsPerIndent = "N" Then
                                mIssueAsPerIndent = GetAutoIssueFromIndent(mPONo, mItemCode, "AUTO_ISSUE")
                            End If

                            If mIssueSSAsPerIndent = "N" Then
                                mIssueSSAsPerIndent = GetAutoIssueFromIndent(mPONo, mItemCode, "AUTO_SS_ISSUE")
                            End If
                        End If

                    ElseIf VB.Left(cboRefType.Text, 1) = "R" Then
                        If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "AUTO_INDENT", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mAutoIssue = MasterNo
                        Else
                            mAutoIssue = "N"
                        End If
                    End If


                End If
                '    Next						

                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_CLASSIFICATION", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mItemClassification = MasterNo
                    If Trim(mItemClassification) = "3" Or Trim(mItemClassification) = "2" Then
                        mAutoIssue = "N"
                        mIssueAsPerIndent = "N"
                        mIssueSSAsPerIndent = "N"
                        mJobWorkItems = "N"
                    End If
                End If

                '    If xAutoIssueCheck = False Then						
                If (mJobWorkItems = "Y" Or mAutoIssue = "Y" Or mIssueAsPerIndent = "Y" Or mIssueSSAsPerIndent = "Y") Then
                    If mIssueAsPerIndent = "Y" Or mIssueSSAsPerIndent = "Y" Then
                        If UpdateIssueNoteMain(mIssueNoteNo, "P", xAutoIssueCheck, CntRow, mIssueSSAsPerIndent) = False Then GoTo ErrPart
                    Else
                        If VB.Left(cboRefType.Text, 1) = "R" Then
                            If UpdateIssueNoteMain(mIssueNoteNo, "R", xAutoIssueCheck, CntRow, "") = False Then GoTo ErrPart
                        Else
                            If UpdateIssueNoteMain(mIssueNoteNo, "J", xAutoIssueCheck, CntRow, "") = False Then GoTo ErrPart
                        End If
                    End If
                End If
            Next

            SqlStr = " UPDATE INV_GATE_HDR SET IS_ISSUED='Y', UPDATE_FROM='H'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_MRR ='" & MainClass.AllowSingleQuote(LblMkey.Text) & "'"

            PubDBCn.Execute(SqlStr)
        End If

        If lblBookType.Text = "G" And RsCompany.Fields("MRR_AGT_GE").Value = "Y" Then 'And ADDMode = True						

            SqlStr = " UPDATE INV_GATEENTRY_HDR SET MRR_MADE='Y', MRR_NO=" & Val(txtMRRNo.Text) & "," & vbCrLf & " MRRDATE=TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_gate ='" & MainClass.AllowSingleQuote(txtGateNo.Text) & "'"

            PubDBCn.Execute(SqlStr)

        End If

        If Mid(cboRefType.Text, 1, 1) = "J" Then
            'If lblBookType.Text = "Q" And chkQC.Enabled = True And mQCStatus = "Y" And mIssued = "N" Then
            If Update57Main1(mSuppCustCode) = False Then

            End If
            'End If
        End If

        Dim mSameGSTNo As String
        Dim mPartyGSTNo As String

        mPartyGSTNo = ""
        mPartyGSTNo = GetPartyBusinessDetail(Trim(mSupplierCode), Trim(txtBillTo.Text), "GST_RGN_NO")
        mSameGSTNo = IIf(mPartyGSTNo = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "Y", "N")

        If RsCompany.Fields("IS_POST_DC_IN_LEDGER").Value = "N" And mSameGSTNo = "Y" Then
            SqlStr = " UPDATE INV_GATE_HDR SET MRR_FINAL_FLAG='Y' WHERE AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & ""
            PubDBCn.Execute(SqlStr)
        End If


        UpdateMain1 = True
        PubDBCn.CommitTrans()

        If mIssueNoteNo <> "" Then
            MsgBox("Issue Note no is " & mIssueNoteNo)
        End If

        Exit Function
ErrPart:
        UpdateMain1 = False
        PubDBCn.RollbackTrans() ''						
        RsMRRMain.Requery() ''.Refresh						
        RsMRRDetail.Requery() ''.Refresh						
        If Err.Description = "" Then Exit Function
        If Err.Number = -2147217900 Then
            ErrorMsg("Duplicate Invoice No. Generated, Save Again", "Duplicate", MsgBoxStyle.Critical)
        Else
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        '    Resume						
    End Function
    Private Function AutoGen57No(ByRef mBookType As String, ByRef mBookSubType As String) As String

        On Error GoTo AutoGenSeqBillNoErr
        Dim RsSaleMainGen As ADODB.Recordset
        Dim mNewSeqBillNo As Double
        Dim SqlStr As String

        SqlStr = ""


        SqlStr = "SELECT Max(VNO)  FROM DSP_PAINT57F4_HDR " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
            & " AND BookType='" & mBookType & "' AND BookSubType='" & mBookSubType & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSaleMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsSaleMainGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mNewSeqBillNo = .Fields(0).Value + 1
                Else
                    mNewSeqBillNo = 1
                End If
            Else
                mNewSeqBillNo = 1
            End If
        End With
        AutoGen57No = CStr(mNewSeqBillNo)

        Exit Function
AutoGenSeqBillNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function Update57Main1(ByRef mSuppCustCode As String) As Boolean

        On Error GoTo ErrPart
        Dim i As Short
        Dim nMkey As String
        Dim mTRNType As String
        Dim mAutoKeyNo As Double
        Dim mBillNoSeq As Integer
        Dim mBillNo As String
        'Dim mAccountCode As String
        Dim mNETVALUE As Double
        Dim mTotQty As Double
        Dim mStatus As String
        Dim mREJECTION As String
        Dim lBookType As String
        Dim lBookSubType As String
        Dim SqlStr As String
        Dim mDuration As String




        mNETVALUE = Val(lblTotItemValue.Text)
        mTotQty = Val(lblTotQty.Text)
        mStatus = "O"
        mREJECTION = "N"
        lBookType = "D"
        lBookSubType = "I"
        mDuration = DateAdd("d", 180, txtBillDate.Text)

        nMkey = ""
        If MainClass.ValidateWithMasterTable((txtMRRNo.Text), "AUTO_KEY_MRR", "MKEY", "DSP_PAINT57F4_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND BOOKTYPE='" & lBookType & "' AND BOOKSUBTYPE='" & lBookSubType & "'") = True Then
            nMkey = MasterNo
        End If


        If nMkey = "" Then

            mBillNoSeq = CInt(AutoGen57No(lBookType, lBookSubType))
            mBillNo = CStr(Val(CStr(mBillNoSeq)))

            mCurRowNo = MainClass.AutoGenRowNo("DSP_PAINT57F4_HDR", "RowNo", PubDBCn)
            nMkey = RsCompany.Fields("COMPANY_CODE").Value & RsCompany.Fields("FYEAR").Value & mCurRowNo


            SqlStr = "INSERT INTO DSP_PAINT57F4_HDR (" & " MKEY, COMPANY_CODE, FYEAR, " & vbCrLf _
                & " ROWNO, BookType, BOOKSUBTYPE, " & vbCrLf _
                & " AUTO_KEY_MRR,  MRR_DATE, VNO, " & vbCrLf _
                & " VDATE,  SUPP_CUST_CODE,  BILL_NO, " & vbCrLf _
                & " BILL_DATE,  PARTY_F4NO,  PARTY_F4DATE, " & vbCrLf _
                & " ISSUE_DATE,  NATURE, EXPECTED_DATE, " & vbCrLf _
                & " DESPATCH_NO, DESPATCH_DATE, NETVALUE, " & vbCrLf _
                & " TOTQTY, STATUS, ADDUSER, ADDDATE, " & vbCrLf _
                & " MODUSER, MODDATE,ISREJECTION )"

            SqlStr = SqlStr & vbCrLf _
                & " VALUES('" & nMkey & "'," & RsCompany.Fields("Company_Code").Value & "," & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf _
                & " " & mCurRowNo & ", '" & lBookType & "', '" & lBookSubType & "'," & vbCrLf _
                & " " & Val(txtMRRNo.Text) & ", TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & mBillNoSeq & "'," & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(mSuppCustCode) & "', '" & MainClass.AllowSingleQuote(txtBillNo.Text) & "'," & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & Val(txtMRRNo.Text) & "', TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '-',TO_DATE('" & VB6.Format(mDuration, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '','', " & vbCrLf _
                & " " & mNETVALUE & ", " & mTotQty & ", '" & mStatus & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','','" & mREJECTION & "')"

        Else
            SqlStr = ""
            SqlStr = "UPDATE DSP_PAINT57F4_HDR SET " & vbCrLf _
                & " BOOKTYPE= '" & lBookType & "'," & vbCrLf _
                & " BOOKSUBTYPE= '" & lBookSubType & "'," & vbCrLf _
                & " AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & ", " & vbCrLf _
                & " MRR_DATE=TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " VNO='" & mBillNoSeq & "', " & vbCrLf _
                & " VDATE=TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSuppCustCode) & "', " & vbCrLf _
                & " BILL_NO='" & MainClass.AllowSingleQuote(txtBillNo.Text) & "', " & vbCrLf _
                & " BILL_DATE=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " PARTY_F4NO='" & Val(txtMRRNo.Text) & "', " & vbCrLf _
                & " PARTY_F4DATE=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " ISSUE_DATE=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " NATURE='-', " & vbCrLf _
                & " EXPECTED_DATE=TO_DATE('" & VB6.Format(mDuration, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " DESPATCH_NO='', " & vbCrLf _
                & " DESPATCH_DATE='', " & vbCrLf _
                & " STATUS='" & mStatus & "'," & vbCrLf _
                & " NETVALUE=" & mNETVALUE & ", " & vbCrLf _
                & " TOTQTY=" & mTotQty & ", " & vbCrLf _
                & " ISREJECTION='" & mREJECTION & "'," & vbCrLf _
                & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " WHERE AUTO_KEY_MRR =" & Val(nMkey) & ""
        End If

        PubDBCn.Execute(SqlStr)

        Dim p57F4No As String = Val(txtMRRNo.Text)
        Dim p57F4Date As String = txtBillDate.Text
        Dim pVDate As String = txtMRRDate.Text

        If Update57Detail1(nMkey, lBookType, lBookSubType, mSuppCustCode, p57F4No, p57F4Date, pVDate) = False Then GoTo ErrPart

        Update57Main1 = True


        Exit Function
ErrPart:
        Update57Main1 = False
        If Err.Description = "" Then Exit Function
        If Err.Number = -2147217900 Then
            ErrorMsg("Duplicate Invoice No. Generated, Save Again", "Duplicate", MsgBoxStyle.Critical)
        Else
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        ''Resume
    End Function
    Private Function Update57Detail1(ByRef pMKey As String, ByRef lBookType As String, ByRef lBookSubType As String,
                                     ByRef mSuppCustCode As String, ByRef p57F4No As String, ByRef p57F4Date As String, ByRef pVDate As String) As Boolean

        On Error GoTo Update57Detail1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim i As Integer
        Dim mItemCode As String
        Dim mTariff As String
        Dim mQty As Double
        Dim mUnit As String
        Dim mRate As Double
        Dim mAmount As Double
        Dim SqlStr As String



        PubDBCn.Execute("Delete From DSP_PAINT57F4_DET Where Mkey='" & pMKey & "'")
        PubDBCn.Execute("Delete From DSP_PAINT57F4_TRN Where Mkey='" & pMKey & "'" & vbCrLf _
                        & " AND BOOKTYPE='" & lBookType & "' AND BOOKSUBTYPE='" & lBookSubType & "' AND TRNTYPE='O'")


        With SprdMain
            For i = 1 To .MaxRows - 1
                .Row = i

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColReceivedQty
                mQty = Val(.Text)

                .Col = ColUnit
                mUnit = MainClass.AllowSingleQuote(.Text)

                .Col = ColRate
                mRate = Val(.Text)

                .Col = ColAmount
                mAmount = Val(.Text)

                mTariff = ""

                SqlStr = ""

                If mItemCode <> "" And mQty <> 0 Then
                    SqlStr = " INSERT INTO DSP_PAINT57F4_DET ( " & vbCrLf _
                        & " MKEY , SUBROWNO, " & vbCrLf _
                        & " ITEM_CODE , ITEM_QTY, " & vbCrLf _
                        & " ITEM_UOM , ITEM_RATE, ITEM_AMT, " & vbCrLf _
                        & " ITEM_TARIFF, COMPANY_CODE) "

                    SqlStr = SqlStr & vbCrLf _
                        & " VALUES ('" & pMKey & "', " & i & ", " & vbCrLf & " '" & mItemCode & "'," & mQty & ", " & vbCrLf _
                        & " '" & mUnit & "'," & mRate & "," & mAmount & ", " & vbCrLf & " '" & mTariff & "'," & RsCompany.Fields("COMPANY_CODE").Value & " ) "

                    PubDBCn.Execute(SqlStr)

                    If UpdatePaintDetail(PubDBCn, pMKey, lBookType, lBookSubType, mSuppCustCode, Trim(p57F4No), p57F4Date,
                                         (txtBillNo.Text), (txtBillDate.Text), mItemCode, mQty, "I", i, "O", pVDate) = False Then GoTo Update57Detail1
                End If
            Next
        End With
        Update57Detail1 = True
        Exit Function
Update57Detail1:
        Update57Detail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Function

    Private Function UpdateSRTRN() As Boolean

        On Error GoTo UpdateDetail1Err
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing


        SqlStr = " SELECT AUTO_KEY_REF " & vbCrLf & " FROM INV_SALEREJECTION_TRN " & vbCrLf & " WHERE AUTO_KEY_REF=" & Val(txtMRRNo.Text) & "" & vbCrLf & " AND REF_TYPE='M'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = True Then
            SqlStr = " INSERT INTO INV_SALEREJECTION_TRN ( " & vbCrLf & " AUTO_KEY_REF, REF_TYPE, MAIL_SEND,IS_MODIFIED) " & vbCrLf & " VALUES (" & Val(txtMRRNo.Text) & ",'M','N','N') "

        Else
            SqlStr = " UPDATE INV_SALEREJECTION_TRN SET IS_MODIFIED='Y', MAIL_SEND='N'" & vbCrLf & " WHERE AUTO_KEY_REF=" & Val(txtMRRNo.Text) & " AND REF_TYPE='M'"

        End If

        PubDBCn.Execute(SqlStr)

        UpdateSRTRN = True
        Exit Function
UpdateDetail1Err:
        UpdateSRTRN = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume						
    End Function

    Private Function CheckValidVDate(ByVal pMRRNoSeq As Double, ByVal mDivisionCode As Double) As Object

        On Error GoTo CheckERR
        Dim SqlStr As String = ""
        Dim mRsCheck1 As ADODB.Recordset = Nothing
        Dim mRsCheck2 As ADODB.Recordset = Nothing
        Dim mBackBillDate As String = ""
        Dim mMaxInvStrfNo As Integer
        Dim mSeparateSeries As String
        Dim RsTemp As ADODB.Recordset = Nothing

        CheckValidVDate = True

        If RsCompany.Fields("StockBalCheck").Value = "N" Then
            Exit Function
        End If

        If CDate(txtMRRDate.Text) <= CDate("31/07/2022") Then
            Exit Function
        End If


        If txtMRRNo.Text = 1 & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00") Then Exit Function

        '    SqlStr = "SELECT SEPARATE_MRR_SERIES, MRR_SERIES " & vbCrLf _						
        ''            & " FROM INV_DIVISION_MST " & vbCrLf _						
        ''            & " WHERE Company_Code=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _						
        ''            & " AND DIV_CODE=" & mDivisionCode & ""						
        '						
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly						
        '						
        '						
        '    If RsTemp.EOF = False Then						
        '        mSeparateSeries = IIf(IsNull(RsTemp!SEPARATE_MRR_SERIES), "N", RsTemp!SEPARATE_MRR_SERIES)						
        '    End If						

        mSeparateSeries = IIf(IsDBNull(RsCompany.Fields("SEPARATE_MRR_SERIES").Value), "N", RsCompany.Fields("SEPARATE_MRR_SERIES").Value)

        SqlStr = "SELECT MAX(MRR_DATE)" & vbCrLf & " FROM INV_GATE_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_MRR,LENGTH(AUTO_KEY_MRR)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_MRR<" & Val(CStr(pMRRNoSeq)) & ""

        If mSeparateSeries = "Y" Then
            SqlStr = SqlStr & vbCrLf & " AND DIV_CODE=" & mDivisionCode & ""
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsCheck2, ADODB.LockTypeEnum.adLockReadOnly)

        If mRsCheck2.EOF = False Then
            mBackBillDate = IIf(IsDBNull(mRsCheck2.Fields(0).Value), mBackBillDate, mRsCheck2.Fields(0).Value)
        End If

        SqlStr = "SELECT MIN(MRR_DATE)" & " FROM INV_GATE_HDR " & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_MRR,LENGTH(AUTO_KEY_MRR)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_MRR>" & Val(CStr(pMRRNoSeq)) & ""

        If mSeparateSeries = "Y" Then
            SqlStr = SqlStr & vbCrLf & " AND DIV_CODE=" & mDivisionCode & ""
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsCheck1, ADODB.LockTypeEnum.adLockReadOnly)

        If mRsCheck1.EOF = False And Not IsDBNull(mRsCheck1.Fields(0).Value) And mRsCheck2.EOF = False And Not IsDBNull(mRsCheck2.Fields(0).Value) Then
            If CDate(txtMRRDate.Text) > CDate(mRsCheck1.Fields(0).Value) Then
                MsgBox("MRR Date Is Greater Than The MRR Date Of Next MRR No.")
                CheckValidVDate = False
            ElseIf CDate(txtMRRDate.Text) < CDate(mRsCheck2.Fields(0).Value) Then
                MsgBox("MRR Date Is Less Than The MRR Date Of Previous MRR No.")
                CheckValidVDate = False
            End If
        ElseIf mRsCheck1.EOF = False And Not IsDBNull(mRsCheck1.Fields(0).Value) Then
            If CDate(txtMRRDate.Text) > CDate(mRsCheck1.Fields(0).Value) Then
                MsgBox("MRR Date Is Greater Than The MRR Date Of Next MRR No.")
                CheckValidVDate = False
            End If
        ElseIf mRsCheck2.EOF = False And Not IsDBNull(mRsCheck2.Fields(0).Value) Then
            If CDate(txtMRRDate.Text) < CDate(mRsCheck2.Fields(0).Value) Then
                MsgBox("MRR Date Is Less Than The MRR Date Of Previous MRR No.")
                CheckValidVDate = False
            End If
        End If

        Exit Function
CheckERR:
        ''Resume						
        CheckValidVDate = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function CheckPendingGateEntry(ByVal mDivisionCode As Double) As Object

        On Error GoTo CheckERR
        Dim SqlStr As String = ""
        Dim mSeparateSeries As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCheckDate As String

        'If RsCompany.Fields("StockBalCheck").Value = "N" Then
        CheckPendingGateEntry = True
        Exit Function
        'End If
        CheckPendingGateEntry = False
        mCheckDate = "01/" & VB6.Format(txtMRRDate.Text, "MM/YYYY")


        '    mSeparateSeries = IIf(IsNull(RsCompany!SEPARATE_MRR_SERIES), "N", RsCompany!SEPARATE_MRR_SERIES)						

        SqlStr = "SELECT AUTO_KEY_GATE" & vbCrLf & " FROM INV_GATEENTRY_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_GATE,LENGTH(AUTO_KEY_GATE)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND MRR_MADE='N'" & vbCrLf & " AND GATE_DATE < TO_DATE('" & VB6.Format(mCheckDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        SqlStr = SqlStr & vbCrLf & " AND AUTO_KEY_GATE NOT IN (" & vbCrLf & " SELECT AUTO_KEY_GATE FROM INV_GATEENTRY_UNLOCK_TRN " & vbCrLf & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " )"

        '    If mSeparateSeries = "Y" Then						
        '        SqlStr = SqlStr & vbCrLf & " AND DIV_CODE=" & mDivisionCode & ""						
        '    End If						

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            CheckPendingGateEntry = False
        Else
            CheckPendingGateEntry = True
        End If

        Exit Function
CheckERR:
        ''Resume						
        CheckPendingGateEntry = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function


    Private Function GetGateEntryPendingHour() As Integer

        On Error GoTo CheckERR
        Dim SqlStr As String = ""
        Dim mSeparateSeries As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCurrentDate As String
        Dim mGateEntryDate As String = ""

        Dim mHolidays As Integer

        GetGateEntryPendingHour = 0
        mHolidays = 0

        mCurrentDate = lblEntryDate.Text

        SqlStr = "SELECT TO_CHAR(ADDDATE,'DD/MM/YYYY HH24:MI') AS GATE_DATE" & vbCrLf & " FROM INV_GATEENTRY_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND AUTO_KEY_GATE = " & Val(txtGateNo.Text) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mGateEntryDate = IIf(IsDBNull(RsTemp.Fields("GATE_DATE").Value), "", RsTemp.Fields("GATE_DATE").Value)
        End If

        SqlStr = " SELECT COUNT(1) AS HOLIDAYCNT " & vbCrLf & " FROM PAY_HOLIDAY_MST WHERE " & vbCrLf & " COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND HOLIDAY_DATE>=TO_DATE('" & VB6.Format(mGateEntryDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND HOLIDAY_DATE<=TO_DATE('" & VB6.Format(mCurrentDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            mHolidays = (IIf(IsDBNull(RsTemp.Fields("HOLIDAYCNT").Value), 0, RsTemp.Fields("HOLIDAYCNT").Value)) * 24
        End If

        GetGateEntryPendingHour = DateDiff(Microsoft.VisualBasic.DateInterval.Hour, CDate(mGateEntryDate), CDate(mCurrentDate)) - mHolidays

        Exit Function
CheckERR:
        ''Resume						
        GetGateEntryPendingHour = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function


    Private Function CheckReofferMade(ByVal mItemCode As String, ByVal pReofferNo As String) As Boolean

        On Error GoTo CheckERR
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        CheckReofferMade = False
        pReofferNo = ""
        SqlStr = "SELECT IH.AUTO_KEY_REF" & vbCrLf & " FROM INV_REOFFER_HDR IH, INV_REOFFER_DET ID " & vbCrLf & " WHERE IH.Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND IH.AUTO_KEY_REF=ID.AUTO_KEY_REF" & vbCrLf & " AND IH.AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & "" & vbCrLf & " AND ID.ITEM_CODE = '" & MainClass.AllowSingleQuote(mItemCode) & "'" & vbCrLf & " AND IH.CANCELLED_STATUS='N'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            pReofferNo = IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_REF").Value), "", RsTemp.Fields("AUTO_KEY_REF").Value)
            CheckReofferMade = True
        Else
            CheckReofferMade = False
        End If

        Exit Function
CheckERR:
        ''Resume						
        CheckReofferMade = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function


    Private Function CheckDNCNMade(ByVal mItemCode As String, ByVal mItemQty As Double, ByVal mVNo As String) As Object

        On Error GoTo CheckERR
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xValue As Double


        CheckDNCNMade = False
        mVNo = ""

        SqlStr = "SELECT SUM(ITEM_QTY) AS ITEM_QTY, VNO AS VNO" & vbCrLf & " FROM FIN_DNCN_HDR IH, FIN_DNCN_DET ID" & vbCrLf & " WHERE IH.Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND IH.MKEY=ID.MKEY" & vbCrLf & " AND ID.MRR_REF_NO=" & Val(txtMRRNo.Text) & "" & vbCrLf & " AND ID.ITEM_CODE = '" & MainClass.AllowSingleQuote(mItemCode) & "' AND IH.CANCELLED='N' AND APPROVED='Y' AND DNCNFROM='M'" & vbCrLf & " HAVING SUM(ITEM_QTY)<>0 GROUP BY VNO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        xValue = 0
        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                xValue = xValue + IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)
                If mVNo = "" Then
                    mVNo = IIf(IsDBNull(RsTemp.Fields("VNO").Value), "", RsTemp.Fields("VNO").Value)
                Else
                    mVNo = mVNo & ", " & IIf(IsDBNull(RsTemp.Fields("VNO").Value), "", RsTemp.Fields("VNO").Value)
                End If
                RsTemp.MoveNext()
            Loop
            If xValue = mItemQty Then
                CheckDNCNMade = False
            Else
                CheckDNCNMade = True
            End If
        Else
            CheckDNCNMade = False
        End If

        Exit Function
CheckERR:
        'Resume						
        CheckDNCNMade = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function AutoGenSeqNo(ByVal pDivision As Double) As String

        On Error GoTo AutoGenSeqNoErr
        Dim RsMRRMainGen As ADODB.Recordset = Nothing
        Dim mNewSeqNo As Double
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mStartingSNo As Double
        Dim mSeparateSeries As String
        Dim mMaxValue As String

        SqlStr = ""
        mStartingSNo = 1
        mSeparateSeries = IIf(IsDBNull(RsCompany.Fields("SEPARATE_MRR_SERIES").Value), "N", RsCompany.Fields("SEPARATE_MRR_SERIES").Value)

        SqlStr = "SELECT MRR_SERIES " & vbCrLf & " FROM INV_DIVISION_MST " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND DIV_CODE=" & pDivision & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


        If RsTemp.EOF = False Then
            '        mSeparateSeries = IIf(IsNull(RsTemp!SEPARATE_MRR_SERIES), "N", RsTemp!SEPARATE_MRR_SERIES)						
            If mSeparateSeries = "Y" Then
                mStartingSNo = IIf(IsDBNull(RsTemp.Fields("MRR_SERIES").Value), 1, RsTemp.Fields("MRR_SERIES").Value)
                mStartingSNo = IIf(mStartingSNo = 0, 1, mStartingSNo)
            End If
        End If



        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_MRR)  " & vbCrLf & " FROM INV_GATE_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_MRR,LENGTH(AUTO_KEY_MRR)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        If mSeparateSeries = "Y" Then
            SqlStr = SqlStr & vbCrLf & " AND DIV_CODE=" & pDivision & ""
        End If


        'If CDate(txtMRRDate.Text) <= CDate("30/06/2022") Then
        '    SqlStr = SqlStr & vbCrLf & " AND MRR_DATE<=TO_DATE('" & VB6.Format("30/06/2022", "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        'End If



        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMRRMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsMRRMainGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mMaxValue = .Fields(0).Value
                    mNewSeqNo = CDbl(Mid(mMaxValue, 1, Len(mMaxValue) - 6))
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

    Private Function AutoDNoteNo() As String

        On Error GoTo AutoGenSeqNoErr
        Dim RsMRRMainGen As ADODB.Recordset = Nothing
        Dim mNewSeqNo As Double
        Dim SqlStr As String = ""
        Dim mMaxValue As String

        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_DESCRP)  " & vbCrLf & " FROM INV_DESCRP_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_DESCRP,LENGTH(AUTO_KEY_DESCRP)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMRRMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsMRRMainGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mMaxValue = .Fields(0).Value
                    mNewSeqNo = CDbl(Mid(mMaxValue, 1, Len(mMaxValue) - 6))
                    mNewSeqNo = mNewSeqNo + 1
                Else
                    mNewSeqNo = 1
                End If
            End If
        End With
        AutoDNoteNo = mNewSeqNo & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        Exit Function
AutoGenSeqNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function UpdateDetail1(ByVal pRejStatus As String, ByVal pSupplierCode As String, ByVal pRefAutoKeyNo As Double, ByVal pDivisionCode As Double) As Boolean

        On Error GoTo UpdateDetail1Err
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim j As Integer
        Dim mPONo As Double
        Dim mPONoRef As String
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mUnit As String
        Dim mStockType As String = ""
        Dim mBillQty As Double
        Dim mQtyInKgs As Double
        Dim mRecdQtyInKgs As Double
        Dim mRecdQty As Double
        Dim mShortQty As Double
        Dim mApprovedQty As Double
        Dim mRejQty As Double
        Dim mAcceptQty As Double
        Dim mDevQty As Double
        Dim mSeg As Double
        Dim mRework As Double
        Dim mQCEmp As String
        Dim mItemRate As Double
        Dim mConvQty As Double
        Dim mSchdRtnFlag As String
        Dim mPDIRFlag As String
        Dim mItemCost As Double
        Dim mBatchNo As String
        Dim mInvQty As Double

        Dim mOutwardF4No As Double
        Dim mOutwardF4Date As String = ""
        Dim mExpDate As String = ""
        Dim mCheckF4 As Boolean
        Dim mMRRQCDate As String
        Dim mItemQCDate As String
        Dim mRecord As Boolean
        Dim mQCDate As String
        Dim mItemClassType As String
        'Dim mLotNo As String
        Dim mPODate As String
        Dim mRGPQty As Double
        Dim mRGPItemCode As String
        Dim mRemarks As String
        Dim mCT3No As Double
        Dim mPCNo As Double
        'Dim mItemLock As Boolean
        Dim xPurchaseCost As Double
        Dim xLandedCost As Double
        Dim mHeatNo As String
        Dim mBillFromSupplier As String = ""
        Dim mAutoQC As Boolean = False
        Dim mInterUnitCompanyCode As Long
        Dim mWOItemDesc As String

        Dim mPartyGSTNo As String
        Dim mSameGSTNo As String
        Dim mIsSampling As Boolean
        Dim mInterUnit As String = "N"
        Dim mPORate As Double = 0
        Dim mAssets As String
        If MainClass.ValidateWithMasterTable((TxtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSupplierCode = MasterNo
        End If


        mInterUnit = "N"
        If MainClass.ValidateWithMasterTable((TxtSupplier.Text), "SUPP_CUST_NAME", "INTER_UNIT", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mInterUnit = MasterNo
        End If


        mPartyGSTNo = ""
        mPartyGSTNo = GetPartyBusinessDetail(Trim(mSupplierCode), Trim(txtBillTo.Text), "GST_RGN_NO")
        mSameGSTNo = IIf(mPartyGSTNo = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "Y", "N")

        mIsSampling = False
        If mSameGSTNo = "Y" And VB.Left(cboRefType.Text, 1) = "F" Then
            mInterUnitCompanyCode = -1
            If MainClass.ValidateWithMasterTable(Trim(TxtSupplier.Text), "SUPP_CUST_NAME", "INTERUNIT_COMPANY_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND INTER_UNIT='Y'") = True Then
                mInterUnitCompanyCode = Val(MasterNo)
            End If

            SqlStr = "SELECT PURPOSE " & vbCrLf _
               & " FROM INV_GATEPASS_HDR" & vbCrLf _
               & " WHERE COMPANY_CODE=" & mInterUnitCompanyCode & " " & vbCrLf _
               & " AND AUTO_KEY_PASSNO='" & txtBillNo.Text & "' AND PURPOSE='F'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                mIsSampling = True
            End If
        End If

        ''AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSupplierCode) & "'" & vbCrLf _



        mRecord = False
        pQCDate = VB6.Format(PubCurrDate, "DD/MM/YYYY") '' RunDate						
        If CDate(pQCDate) > CDate(RsCompany.Fields("END_DATE").Value) Then
            pQCDate = VB6.Format(RsCompany.Fields("END_DATE").Value, "DD/MM/YYYY")
        End If

        'If chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked Then
        mBillFromSupplier = pSupplierCode
        'Else
        '    If MainClass.ValidateWithMasterTable((txtShippedTo.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mBillFromSupplier = MasterNo
        '    End If
        'End If

        PubDBCn.Execute("Delete From INV_GATE_DET Where AUTO_KEY_MRR='" & LblMkey.Text & "'")

        '    If lblBookType.text = "G" Then						
        '        If DeleteStockTRN(PubDBCn, ConStockRefType_QC, txtMRRNo.Text) = False Then GoTo UpdateDetail1Err						
        '    Else						
        If DeleteStockTRN(PubDBCn, ConStockRefType_MRR, (txtMRRNo.Text)) = False Then GoTo UpdateDetail1Err
        '    End If						

        If DeletePaintStockTRN(PubDBCn, ConStockRefType_MRR, (txtMRRNo.Text)) = False Then GoTo UpdateDetail1Err

        ''Delete as per Bill No & Date
        PubDBCn.Execute("DELETE FROM INV_RGP_REG_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND BILL_NO='" & txtBillNo.Text & "' AND  BILL_DATE =TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND  SUPP_CUST_CODE='" & pSupplierCode & "'  AND BOOKTYPE='M' AND ITEM_IO='I'")

        'PubDBCn.Execute("DELETE FROM INV_RGP_REG_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND REF_NO='" & LblMkey.Text & "'  AND BOOKTYPE='M' AND ITEM_IO='I'")

        PubDBCn.Execute("Delete From FIN_CT_TRN Where Mkey='" & LblMkey.Text & "' AND BOOKTYPE='P' AND BOOKSUBTYPE='I'")
        PubDBCn.Execute("Delete From FIN_PC_TRN Where Mkey='" & LblMkey.Text & "' AND BOOKTYPE='P' AND BOOKSUBTYPE='I'")
        With SprdMain
            I = 0
            For j = 1 To .MaxRows - 1
                .Row = j
                I = I + 1
                'mItemLock = False

                .Col = ColPONo
                mPONo = IIf(Val(.Text) = 0, "-1" & VB6.Format(RsCompany.Fields("FYEAR").Value, "00"), Val(.Text))
                mPONoRef = IIf(Val(.Text) = 0, "-1" & VB6.Format(RsCompany.Fields("FYEAR").Value, "00"), .Text)

                .Col = ColPODate
                mPODate = MainClass.AllowSingleQuote(.Text)
                If VB.Left(cboRefType.Text, 1) = "R" Then
                    If MainClass.ValidateWithMasterTable(mPONo, "AUTO_KEY_PASSNO", "GATEPASS_DATE", "INV_GATEPASS_HDR", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & pSupplierCode & "'") = True Then
                        mPODate = MasterNo
                    End If
                End If

                .Col = ColRGPItemCode
                mRGPItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColItemDesc
                mItemDesc = Val(.Text)

                'If GetItemLocking(mItemCode) = True Then
                '    mItemLock = True
                'End If

                .Col = ColUnit
                mUnit = MainClass.AllowSingleQuote(.Text)

                .Col = ColStockType
                If lblBookType.Text = "G" And lblSaleReturn.Text = "Y" Then
                    mStockType = MainClass.AllowSingleQuote(.Text)
                Else
                    'If mItemLock = True Then
                    '    mStockType = "ST"
                    'Else
                    mStockType = MainClass.AllowSingleQuote(.Text)
                    mAutoQC = GetAutoQC(mItemCode)
                    If mAutoQC = True Then
                        mStockType = IIf(mStockType = "QC", IIf(VB.Left(cboRefType.Text, 1) = "J", "CS", "ST"), mStockType)
                        .Col = ColQCEMP
                        .Text = IIf(Trim(.Text) = "", PubUserEMPCode, .Text)

                    End If
                    'End If
                End If

                If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 And mInterUnit = "Y" And mStockType = "CS" Then
                    mStockType = "ST"
                End If

                .Col = ColHeatNo
                mHeatNo = UCase(Trim(.Text))

                .Col = ColBatchNo
                mBatchNo = Trim(.Text)
                'If PubGSTApplicable = True Then
                '    If Trim(.Text) = "" Then
                '        mBatchNo = LblMkey.Text ''Mid(LblMkey.text, 1, Len(LblMkey.text) - 6)						
                '    Else
                '        mBatchNo = Trim(.Text)
                '    End If
                'Else
                '    If Trim(.Text) = "" Then
                '        mBatchNo = Mid(LblMkey.Text, 1, Len(LblMkey.Text) - 6)
                '    Else
                '        mBatchNo = Trim(.Text)
                '    End If
                'End If

                '.Col = ColLotNo
                'If GetProductionType(mItemCode) = "R" Then
                '    mLotNo = mBatchNo
                'Else
                '    mLotNo = Trim(.Text) 'mBatchNo '						
                'End If

                .Col = ColBillQty
                mBillQty = Val(.Text)

                .Col = ColQtyInKgs
                mQtyInKgs = Val(.Text)

                .Col = ColRecdQtyInKgs
                mRecdQtyInKgs = Val(.Text)

                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_CLASSIFICATION", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mItemClassType = IIf(IsDBNull(MasterNo), "B", MasterNo)
                Else
                    mItemClassType = "B"
                End If

                .Col = ColReceivedQty
                mRecdQty = Val(.Text)
                'If mItemClassType = "D" Then
                '    If mRecdQty > 100 Then
                '        MsgInformation("Development Item cann't be received More than 100 unit. ")
                '        MainClass.SetFocusToCell(SprdMain, I, ColReceivedQty)
                '        GoTo UpdateDetail1Err
                '        UpdateDetail1 = False
                '    End If
                'End If

                .Col = ColShortQty
                'If mItemLock = True Then
                mShortQty = System.Math.Abs(mBillQty - mRecdQty)
                'Else

                '    mShortQty = Val(.Text)
                'End If

                .Col = ColApprovedQty
                If mAutoQC = True Then
                    mApprovedQty = mRecdQty
                Else
                    mApprovedQty = Val(.Text)
                End If

                .Col = ColRejQty
                'If mAutoQC = True Then
                '    mRejQty = 0
                'Else
                '    mRejQty = mRecdQty - mApprovedQty      ''Val(.Text)
                'End If
                mRejQty = mRecdQty - mApprovedQty

                .Col = ColAcceptQty
                'If mAutoQC = True Then
                '    mAcceptQty = mRecdQty
                'Else
                '    mAcceptQty = Val(.Text)
                'End If
                mAcceptQty = mApprovedQty

                .Col = ColDevQty
                mDevQty = Val(.Text)

                .Col = ColSeg
                mSeg = Val(.Text)

                .Col = ColRework
                mRework = Val(.Text)

                .Col = ColQCEMP
                mQCEmp = MainClass.AllowSingleQuote(.Text)

                .Col = ColCT3No
                mCT3No = Val(.Text)

                .Col = ColPCNo
                mPCNo = Val(.Text)

                '.Col = ColPORate
                'mPORate = Val(.Text)

                If VB.Left(cboRefType.Text, 1) = "P" Then
                    xPurchaseCost = 0
                    xLandedCost = 0
                    If GetLatestItemCostFromPO(mItemCode, xPurchaseCost, xLandedCost, VB6.Format(txtMRRDate.Text, "DD/MM/YYYY"), "ST", mBillFromSupplier, mUnit, 1, , mPONo) = False Then GoTo UpdateDetail1Err

                    .Col = ColRate
                    .Text = VB6.Format(xPurchaseCost, "0.0000")

                    .Col = ColItemCost
                    .Text = VB6.Format(xLandedCost, "0.0000")

                End If

                .Col = ColRate
                mItemRate = Val(.Text)

                .Col = ColItemCost
                mItemCost = Val(.Text)

                .Col = ColConvQty
                mConvQty = Val(.Text)

                .Col = ColSchdRtnFlag
                mSchdRtnFlag = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                .Col = ColPDIRFlag
                mPDIRFlag = IIf(ADDMode = True, "Y", IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N"))


                .Col = ColQCDate
                If mStockType = "QC" Then ' SK 05-02-2004 txtMRRDate.Text)						
                    mMRRQCDate = ""
                    mQCDate = VB6.Format(txtMRRDate.Text, "DD/MM/YYYY")
                Else
                    mMRRQCDate = IIf(IsDate(.Text), .Text, pQCDate)
                    mQCDate = mMRRQCDate
                    If CDate(txtMRRDate.Text) > CDate(mQCDate) Then
                        mMRRQCDate = txtMRRDate.Text
                        mQCDate = txtMRRDate.Text
                    End If
                End If

                'Not req. ''SK						
                '            mQCDate = VB6.Format(txtMRRDate.Text, "DD/MM/YYYY")						
                '            mMRRQCDate = VB6.Format(txtMRRDate.Text, "DD/MM/YYYY")						

                .Col = ColRemarks
                mRemarks = Trim(.Text)

                .Col = ColItemDesc
                mWOItemDesc = Trim(.Text)

                SqlStr = ""

                If mItemCode <> "" And mBillQty > 0 Then
                    SqlStr = " INSERT INTO INV_GATE_DET ( " & vbCrLf _
                        & " AUTO_KEY_MRR, SERIAL_NO, ITEM_CODE," & vbCrLf _
                        & " ITEM_UOM, STOCK_TYPE, BILL_QTY," & vbCrLf _
                        & " RECEIVED_QTY, SHORTAGE_QTY, APPROVED_QTY," & vbCrLf _
                        & " REJECTED_QTY, LOT_ACCEPT, LOT_ACCEPT_DEV," & vbCrLf _
                        & " LOT_ACC_SEG, LOT_ACC_RWK, QC_EMP_CODE," & vbCrLf _
                        & " REMARKS, ITEM_RATE, CONV_QTY," & vbCrLf _
                        & " REJ_RTN_STATUS, MRR_DATE, COMPANY_CODE," & vbCrLf _
                        & " SUPP_CUST_CODE, REF_TYPE, REF_AUTO_KEY_NO," & vbCrLf _
                        & " SCHLD_RTN_FLAG, MRR_QCDATE, PDIR_FLAG," & vbCrLf _
                        & " ITEM_COST,BATCH_NO,  REF_PO_NO, RGP_ITEM_CODE, REF_DATE,CT3_NO,PC_NO,HEAT_NO,ITEM_QTY_IN_KGS,ITEM_RECDQTY_IN_KGS,ITEM_DESCRIPTION ) "

                    SqlStr = SqlStr & vbCrLf & " VALUES ('" & LblMkey.Text & "'," & I & ",'" & mItemCode & "', " & vbCrLf _
                        & " '" & mUnit & "','" & mStockType & "'," & mBillQty & ", " & vbCrLf _
                        & " " & mRecdQty & ", " & mShortQty & ", " & mApprovedQty & "," & vbCrLf _
                        & " " & mRejQty & ", " & mAcceptQty & ", " & mDevQty & ", " & vbCrLf _
                        & " " & mSeg & ", " & mRework & ", '" & mQCEmp & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mRemarks) & "'," & mItemRate & ", " & mConvQty & "," & vbCrLf _
                        & " '" & pRejStatus & "',TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                        & " '" & pSupplierCode & "','" & VB.Left(cboRefType.Text, 1) & "'," & mPONoRef & ", " & vbCrLf _
                        & " '" & mSchdRtnFlag & "', TO_DATE('" & VB6.Format(mMRRQCDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                        & " '" & mPDIRFlag & "'," & mItemCost & ",'" & mBatchNo & "', " & mPONoRef & "," & vbCrLf _
                        & " '" & mRGPItemCode & "',TO_DATE('" & VB6.Format(mPODate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & mCT3No & "," & mPCNo & "," & vbCrLf _
                        & " '" & mHeatNo & "', " & mQtyInKgs & "," & mRecdQtyInKgs & ",'" & MainClass.AllowSingleQuote(mWOItemDesc) & "') "

                    PubDBCn.Execute(SqlStr)

                    If UpdateMRRHistory(PubDBCn, Val(LblMkey.Text), I, mItemCode, mUnit, mStockType, mBillQty, mRecdQty, mShortQty, mApprovedQty, mRejQty, mAcceptQty, mDevQty, mSeg, mRework, mQCEmp, mRemarks, mItemRate, mConvQty, pRejStatus, (txtMRRDate.Text), RsCompany.Fields("COMPANY_CODE").Value, pSupplierCode, VB.Left(cboRefType.Text, 1), mPONo, mSchdRtnFlag, mMRRQCDate, mPDIRFlag, mItemCost, mBatchNo, mRGPItemCode, mPODate, mCT3No, mPCNo, (txtBillNo.Text), (txtBillDate.Text)) = False Then GoTo UpdateDetail1Err

                    If lblBookType.Text = "G" And chkQC.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                        mStockType = IIf(chkQC.CheckState = System.Windows.Forms.CheckState.Unchecked, "QC", mStockType)
                        mInvQty = mRecdQty
                    Else
                        If mStockType = "RJ" Then
                            mStockType = "ST"
                        End If
                        mInvQty = mAcceptQty
                    End If

                    If mIsSampling = True Then
                        mStockType = "SC"
                    End If

                    'If mItemLock = True Then
                    '    mStockType = "ST"
                    '    mInvQty = 0
                    'End If

                    If VB.Left(cboRefType.Text, 1) = "R" Then
                        Call GetF4detailFromRGP(mPONo, mCheckF4, mOutwardF4No, mOutwardF4Date, mExpDate)

                        If UpdateRGP_TRN(PubDBCn, mPONo, VB6.Format(mPODate, "DD/MM/YYYY"), CDbl(txtMRRNo.Text), VB6.Format(txtMRRDate.Text, "DD/MM/YYYY"), pSupplierCode, mOutwardF4No, VB6.Format(mOutwardF4Date, "DD/MM/YYYY"), (txtBillNo.Text), (txtBillDate.Text), Trim(mRGPItemCode), mItemCode, mRGPQty, mRecdQty, "I", I, "M", mExpDate, txtBillTo.Text) = False Then GoTo UpdateDetail1Err
                    End If

                    ''15-05-2011						

                    'mQCDate = VB6.Format(txtMRRDate.Text, "DD/MM/YYYY")

                    If CDate(txtMRRDate.Text) > CDate("30/06/2022") Then
                        If UpdateStockTRN(PubDBCn, ConStockRefType_MRR, (txtMRRNo.Text), I, (txtMRRDate.Text), (txtMRRDate.Text), mStockType, mItemCode, mUnit, mBatchNo, mInvQty, mRejQty, "I", mItemRate, mItemCost, "", "", "STR", "", "", IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N"), "From : " & TxtSupplier.Text, pSupplierCode, ConWH, pDivisionCode, VB.Left(cboRefType.Text, 1), "",, mHeatNo, 0, mPONo) = False Then GoTo UpdateDetail1Err
                    End If


                    '                If Left(cboRefType.Text, 1) = "J" or Left(cboRefType.Text, 1) = "1" Then						
                    '                        If UpdateStockTRN(PubDBCn, ConStockRefType_MRR, txtMRRNo.Text, I, txtMRRDate.Text, mQCDate, _						
                    ''                                mStockType, mItemCode, mUnit, mBatchNo, mInvQty, mRejQty, "O", mItemRate, mItemCost, "", "", "STR", "", "", IIf(chkCancelled.Value = vbChecked, "Y", "N"), "From : " & TxtSupplier.Text, pSupplierCode, ConJW) = False Then GoTo UpdateDetail1Err						
                    '                End If						

                    ''not required... 25/11/2016						
                    '                If Val(mLotNo) <> 0 Then						
                    '                        If UpdatePaintStockTRN(PubDBCn, ConStockRefType_MRR, txtMRRNo.Text, I, txtMRRDate.Text, _						
                    ''                                mStockType, mItemCode, mUnit, mBatchNo, VB6.Format(mLotNo), mInvQty, mRejQty, "I", IIf(chkCancelled.Value = vbChecked, "Y", "N"), "From : " & txtSupplier.Text) = False Then GoTo UpdateDetail1Err						
                    '                End If						

                    If Val(CStr(mCT3No)) <> 0 Then
                        If UpdateCT3TRN(PubDBCn, (txtMRRNo.Text), "P", "I", pSupplierCode, mCT3No, "", Str(CDbl(txtBillNo.Text)), (txtBillDate.Text), mItemCode, mUnit, mInvQty, mItemCost) = False Then GoTo UpdateDetail1Err
                    End If

                    If Val(CStr(mPCNo)) <> 0 Then
                        If UpdatePCTRN(PubDBCn, (txtMRRNo.Text), "P", "I", pSupplierCode, mPCNo, "", (txtBillNo.Text), (txtBillDate.Text), mItemCode, mUnit, mInvQty, mItemCost) = False Then GoTo UpdateDetail1Err
                    End If

                    mRecord = True
                End If
            Next
        End With
        If mRecord = False Then
            MsgInformation("Nothing to Save.")
            UpdateDetail1 = False
            Exit Function
        End If

        UpdateDetail1 = True
        UpdateDetail1 = UpdateExp1()
        Exit Function
UpdateDetail1Err:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume						
    End Function
    Private Function CheckShortQty() As Boolean

        On Error GoTo UpdateDetail1Err
        Dim SqlStr As String = ""
        Dim I As Integer

        Dim mItemCode As String
        Dim mBillQty As Double
        Dim mRecdQty As Double

        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColBillQty
                mBillQty = Val(.Text)

                .Col = ColReceivedQty
                mRecdQty = Val(.Text)

                If mBillQty > mRecdQty Then
                    CheckShortQty = True
                    Exit Function
                End If
            Next
        End With

        CheckShortQty = False
        Exit Function
UpdateDetail1Err:
        CheckShortQty = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume						
    End Function

    Private Function UpdateDiscrepancyNote(ByVal pSupplierCode As String) As Boolean

        On Error GoTo UpdateDetail1Err
        Dim SqlStr As String = ""
        Dim I As Integer

        Dim mItemCode As String
        Dim mUnit As String
        Dim mBillQty As Double
        Dim mRecdQty As Double
        Dim mShortQty As Double
        Dim mAutoKey As Double
        Dim mRemarks As String

        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = "DELETE FROM INV_DESCRP_DET WHERE AUTO_KEY_DESCRP IN ( " & vbCrLf & " SELECT AUTO_KEY_DESCRP FROM " & vbCrLf & " INV_DESCRP_HDR " & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & ")"

        PubDBCn.Execute(SqlStr)


        If CheckShortQty() = False Then
            SqlStr = " DELETE FROM " & vbCrLf & " INV_DESCRP_HDR " & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & ""

            PubDBCn.Execute(SqlStr)

            UpdateDiscrepancyNote = True
            Exit Function
        End If

        SqlStr = " SELECT * FROM " & vbCrLf & " INV_DESCRP_HDR " & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = True Then
            mAutoKey = CDbl(AutoDNoteNo())

            SqlStr = " INSERT INTO INV_DESCRP_HDR ( " & vbCrLf & " AUTO_KEY_DESCRP, COMPANY_CODE, DESCRP_DATE," & vbCrLf & " SUPP_CUST_CODE, AUTO_KEY_MRR) "

            SqlStr = SqlStr & vbCrLf & " VALUES (" & mAutoKey & "," & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & pSupplierCode & "', " & txtMRRNo.Text & ") "
        Else
            mAutoKey = IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_DESCRP").Value), "", RsTemp.Fields("AUTO_KEY_DESCRP").Value)


            SqlStr = " UPDATE INV_DESCRP_HDR SET " & vbCrLf & " DESCRP_DATE=TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " SUPP_CUST_CODE='" & pSupplierCode & "'" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_MRR = " & Val(txtMRRNo.Text) & " " & vbCrLf & " AND AUTO_KEY_DESCRP=" & mAutoKey & ""
        End If
        PubDBCn.Execute(SqlStr)

        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColUnit
                mUnit = MainClass.AllowSingleQuote(.Text)

                .Col = ColBillQty
                mBillQty = Val(.Text)

                .Col = ColReceivedQty
                mRecdQty = Val(.Text)

                mShortQty = CDbl(VB6.Format(mBillQty - mRecdQty, "0.0000"))

                If mShortQty > 0 Then
                    mRemarks = "SHORT QTY " & mShortQty & " " & mUnit

                    SqlStr = ""

                    If mItemCode <> "" And mShortQty > 0 Then
                        SqlStr = " INSERT INTO INV_DESCRP_DET ( " & vbCrLf & " AUTO_KEY_DESCRP, SERIAL_NO, ITEM_CODE," & vbCrLf & " BILL_QTY, REC_QTY, REMARKS) "

                        SqlStr = SqlStr & vbCrLf & " VALUES (" & mAutoKey & "," & I & ",'" & mItemCode & "', " & vbCrLf & " " & mBillQty & ", " & mRecdQty & ",'" & mRemarks & "') "

                        PubDBCn.Execute(SqlStr)

                    End If
                End If
            Next
        End With

        SqlStr = " UPDATE INV_GATE_HDR SET " & vbCrLf & " DESCR_FLAG='Y', UPDATE_FROM='H'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE= " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_MRR=" & txtMRRNo.Text & " "

        PubDBCn.Execute(SqlStr)

        UpdateDiscrepancyNote = True
        Exit Function
UpdateDetail1Err:
        UpdateDiscrepancyNote = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume						
    End Function
    Private Function UpdateExp1() As Boolean
        On Error GoTo UpdateSaleExpErr1
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mExpCode As Integer
        Dim mPercent As Double
        Dim mExpAmount As Double
        Dim m_AD As String
        Dim mCalcOn As Double
        Dim mRO As String

        PubDBCn.Execute("Delete From INV_GATE_EXP Where Mkey='" & LblMkey.Text & "'")
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
                If mCalcOn <> 0 Or mExpAmount <> 0 Then
                    SqlStr = "Insert Into  INV_GATE_EXP (MKEY,SUBROWNO, " & vbCrLf & " EXPCODE,EXPPERCENT,AMOUNT,CalcOn,RO) " & vbCrLf & " Values ('" & LblMkey.Text & "'," & I & ", " & vbCrLf & " " & mExpCode & "," & mPercent & "," & mExpAmount & ", " & vbCrLf & " " & mCalcOn & ",'" & mRO & "')"
                    PubDBCn.Execute(SqlStr)
                End If
            Next I
        End With
        UpdateExp1 = True
        Exit Function
UpdateSaleExpErr1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateExp1 = False
    End Function

    Private Function FieldsVarification() As Boolean

        On Error GoTo err_Renamed
        Dim mRow As Integer
        Dim mSTTaxcount As Integer
        Dim CntRow As Integer
        Dim xShortageQty As Double
        Dim xRejectedQty As Double
        Dim xPORate As Double
        Dim xRate As Double
        Dim xRateDiffDN As Double
        Dim xRateDiffCN As Double
        Dim mExciseDutyAmt As Double
        Dim mSalesTaxAmount As Double
        Dim mWithInState As String
        Dim mItemCode As String
        Dim mItemGSTClass As String = ""
        Dim mBillNo As String
        Dim pReofferNo As String = ""
        Dim mLotNoRequied As String
        Dim mHeatNo As String
        Dim mValidQCUser As Boolean
        Dim mLockBookCode As Integer
        Dim mQCDate As String
        Dim mQCEmp As String
        Dim mStockType As String = ""
        Dim mMaxStockQty As Double
        Dim mStockQty As Double
        Dim mStockLockQty As Double
        Dim mItemUOM As String = ""
        Dim mAcceptQty As Double
        Dim mDivisionCode As Double

        Dim mRGPItemCode As String
        Dim mPONo As String
        Dim mRecdQty As Double
        Dim mBalanceQty As Double
        Dim ii As Integer
        Dim mRGPBalanceQty As Double
        Dim mConsQty As Double
        Dim mRejQty As Double
        Dim mVNo As String = ""
        Dim mProdType As String
        Dim mBillQty As Double
        Dim mCntRGPPaidType As Integer
        Dim mCntRGPFOCType As Integer
        Dim mRGPPurpose As Boolean
        Dim mItemCategory As String

        Dim mMaxLevelQty As Double
        Dim mMRRQty As Double
        Dim mActualQCEmp As String
        Dim mQCAllowDays As Integer
        Dim mIsFGInvoice As Boolean
        Dim mBillFromSupplier As String
        Dim meBillNoApp As String
        Dim meBillNoAppDate As String
        Dim xUnit As String
        Dim xBillQty As Double

        Dim mPORate As Double
        Dim mBillRate As Double

        Dim mPartyGSTNo As String
        Dim mSameGSTNo As String

        mCntRGPPaidType = 0
        mCntRGPFOCType = 0
        mRGPPurpose = False

        FieldsVarification = True


        If MainClass.ValidateWithMasterTable((TxtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Supplier Does Not Exist In Master", MsgBoxStyle.Information)
            'txtSupplier.SetFocus						
            FieldsVarification = False
            Exit Function
        Else
            mSupplierCode = MasterNo
        End If

        mPartyGSTNo = ""
        mPartyGSTNo = GetPartyBusinessDetail(Trim(mSupplierCode), Trim(txtBillTo.Text), "GST_RGN_NO")
        mSameGSTNo = IIf(mPartyGSTNo = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "Y", "N")


        If MainClass.ValidateWithMasterTable((TxtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = False Then
            MsgInformation("Supplier Name is not a Supplier or Customer Category. Cannot Save")
            If TxtSupplier.Enabled = True Then TxtSupplier.Focus()
            FieldsVarification = False
            Exit Function
        End If

        'If chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked Then
        mBillFromSupplier = mSupplierCode
        'Else
        '    If MainClass.ValidateWithMasterTable((txtShippedTo.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
        '        MsgBox("Supplier (Bill To) Does Not Exist In Master", MsgBoxStyle.Information)
        '        'txtSupplier.SetFocus						
        '        FieldsVarification = False
        '        Exit Function
        '    Else
        '        mBillFromSupplier = MasterNo
        '    End If
        'End If

        If ADDMode = True Then
            If CheckBillToShipFrom(mSupplierCode) = False Then
                '        MsgInformation "Invalid Shipped To Supplier Name. Cannot Save"						
                FieldsVarification = False
                Exit Function
            End If
        End If

        If ValidateBranchLocking((txtMRRDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If
        If ValidateAccountLocking(PubDBCn, txtMRRDate.Text, (TxtSupplier.Text), mSupplierCode) = True Then
            FieldsVarification = False
            Exit Function
        End If

        If lblBookType.Text = "Q" Then
            mLockBookCode = CInt(ConLockMRRQC)
        Else
            mLockBookCode = CInt(ConLockMRREntry)
        End If

        If ValidateBookLocking(PubDBCn, mLockBookCode, txtMRRDate.Text) = True Then
            FieldsVarification = False
            Exit Function
        End If

        If ADDMode = False Then
            If chkQC.Enabled = True Then
                If RsCompany.Fields("STOCKBALCHECK").Value = "Y" Then
                    mQCAllowDays = IIf(IsDBNull(RsCompany.Fields("QC_ALLOW_DAYS").Value), 7, RsCompany.Fields("QC_ALLOW_DAYS").Value)
                    If CDate(txtMRRDate.Text) < CDate(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1 * mQCAllowDays, PubCurrDate)) Then
                        If ValidateMRRApproval(PubDBCn, Val(txtMRRNo.Text)) = False Then
                            MsgBox("MRR is More than " & mQCAllowDays & " days so that MRR Lock. For Unlock Contact Administrator with Plant Head Approval.", MsgBoxStyle.Information)
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If
                End If
            End If
        End If

        If ADDMode = True Then
            If VB.Left(cboRefType.Text, 1) = "P" Or VB.Left(cboRefType.Text, 1) = "C" Or VB.Left(cboRefType.Text, 1) = "F" Then
                If MainClass.ValidateWithMasterTable(mSupplierCode, "SUPP_CUST_CODE", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STOP_MRR='Y'") = True Then
                    MsgBox("MRR Cann't Be Made for Such Customer, So cann't be saved", MsgBoxStyle.Information)
                    FieldsVarification = False
                    If TxtSupplier.Enabled = True Then TxtSupplier.Focus()
                    Exit Function
                End If
            End If
        End If

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsMRRMain.EOF = True Then Exit Function

        If MODIFYMode = True And txtMRRNo.Text = "" Then
            MsgInformation("MRR No. is Blank")
            FieldsVarification = False
            Exit Function
        End If

        If txtMRRDate.Text = "" Then
            MsgBox("VDate is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtMRRDate.Focus()
            Exit Function
        ElseIf FYChk((txtMRRDate.Text)) = False Then
            FieldsVarification = False
            If txtMRRDate.Enabled = True Then txtMRRDate.Focus()
            Exit Function
        End If

        '    If PubGSTApplicable = False Then						
        '        If CDate(txtMRRDate.Text) >= CDate(PubGSTApplicableDate) Then						
        '            MsgBox "Please Check the MRR Date.", vbInformation						
        '            FieldsVarification = False						
        '            Exit Function						
        '        End If						
        '    Else						
        '        If CDate(txtMRRDate.Text) < CDate(PubGSTApplicableDate) Then						
        '            MsgBox "Please Check the MRR Date.", vbInformation						
        '            FieldsVarification = False						
        '            Exit Function						
        '        End If						
        '    End If						


        If Trim(txtBillNo.Text) = "" Then
            MsgInformation("Bill No. is Blank")
            FieldsVarification = False
            txtBillNo.Focus()
            Exit Function
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

        If CDate(txtBillDate.Text) > CDate(txtMRRDate.Text) Then
            MsgBox("Bill Date Cann't be greater than MRR Date.", MsgBoxStyle.Information)
            FieldsVarification = False
            txtBillDate.Focus()
            Exit Function
        End If

        If PubSuperUser <> "S" Then
            If ADDMode = True Then
                If DateDiff(Microsoft.VisualBasic.DateInterval.Month, CDate(txtBillDate.Text), PubCurrDate) > 12 Then
                    MsgBox("Bill Date is more than 12 Month old, So cann't be Save. Please contact Administrator.", MsgBoxStyle.Information)
                    If txtBillDate.Enabled Then txtBillDate.Focus()
                    FieldsVarification = False
                    Exit Function
                End If
            End If
            If CDate(txtBillDate.Text) > CDate(PubCurrDate) Then
                MsgBox("Bill Date Cann't be future Date, So cann't be Save. Please contact Administrator.", MsgBoxStyle.Information)
                If txtBillDate.Enabled Then txtBillDate.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If CDate(txtMRRDate.Text) < CDate(PubGSTApplicableDate) And CDate(txtBillDate.Text) >= CDate(PubGSTApplicableDate) Then
            If DateDiff(Microsoft.VisualBasic.DateInterval.Month, CDate(txtMRRDate.Text), CDate(txtBillDate.Text)) > 1 Then
                MsgBox("Bill Date is more than 1 Month After GST Applicable, So cann't be Save. Please contact Administrator.", MsgBoxStyle.Information)
                If txtBillDate.Enabled Then txtBillDate.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If Trim(cboDivision.Text) = "" Then
            MsgBox("Division Cannot Be Blank", MsgBoxStyle.Information)
            ' txtSupplier.SetFocus						
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable((cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Division Does Not Exist In Master", MsgBoxStyle.Information)
            'txtSupplier.SetFocus						
            FieldsVarification = False
            Exit Function
        Else
            mDivisionCode = Val(MasterNo)
        End If


        If RsCompany.Fields("MRR_AGT_GE").Value = "Y" Then
            If txtGateNo.Text = "" Then
                MsgInformation("Gate No. is Blank")
                FieldsVarification = False
                Exit Function
            End If

            'If ADDMode = True Then
            '    If lblBookType.Text = "G" And CDate(PubCurrDate) >= CDate("05/01/2015") Then
            '        If GetGateEntryPendingHour() > 48 Then
            '            If ValidateMRRApprovalAfter48Hours(PubDBCn, Val(txtGateNo.Text)) = False Then
            '                MsgBox("Gate is Pending from More than 48 hours, so that Gate Entry is Lock. For Unlock Contact Administrator with Plant Head Approval.", MsgBoxStyle.Information)
            '                FieldsVarification = False
            '                Exit Function
            '            End If
            '        End If
            '    End If
            'End If

            'Temp..						
            If lblBookType.Text = "G" Then
                If CheckPendingGateEntry(mDivisionCode) = False Then
                    MsgInformation("Previous Month Gate Entries are Pending. Please Clear first Pending Gate Entries.")
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        End If


        If lblBookType.Text = "G" Then
            If ValidateDivisionRight(PubUserID, mDivisionCode, UCase(Trim(cboDivision.Text))) = False Then
                FieldsVarification = False
                Exit Function
            End If
        Else
            If ValidateDivisionRight(PubUserID, mDivisionCode, UCase(Trim(cboDivision.Text))) = False Then
                FieldsVarification = False
                Exit Function
            End If
        End If

        If RsCompany.Fields("MRR_AGT_GE").Value = "Y" Then
            If Val(txtGateNo.Text) = 0 Then
                MsgBox("Please Entered the Valid Gate Entry No.")
                FieldsVarification = False
                Exit Function
            End If

            If ADDMode = True Then
                If MainClass.ValidateWithMasterTable((txtGateNo.Text), "AUTO_KEY_GATE", "AUTO_KEY_GATE", "INV_GATEENTRY_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND MRR_MADE='N'") = False Then
                    MsgBox("Please Entered the Valid Gate Entry No.")
                    FieldsVarification = False
                    Exit Function
                End If
            Else
                If MainClass.ValidateWithMasterTable((txtGateNo.Text), "AUTO_KEY_GATE", "AUTO_KEY_GATE", "INV_GATEENTRY_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                    MsgBox("Please Entered the Valid Gate Entry No.")
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        End If

        If CheckRefDate(mDivisionCode) = False Then
            FieldsVarification = False
            Exit Function
        End If


        If PubSuperUser = "U" Then
            Dim mCheckDate As String
            mCheckDate = RsCompany.Fields("Start_Date").Value
            mCheckDate = DateAdd("YYYY", -1, mCheckDate)
            If CDate(txtBillDate.Text) < CDate(mCheckDate) Then
                MsgBox("Invalid BillDate.")
                FieldsVarification = False
                If txtBillDate.Enabled = True Then txtBillDate.Focus()
                Exit Function
            End If
        End If

        If CDate(txtMRRDate.Text) < CDate(txtBillDate.Text) Then
            MsgBox("VDate Can Not be Less Than BillDate.")
            FieldsVarification = False
            If txtBillDate.Enabled = True Then txtBillDate.Focus()
            Exit Function
        End If

        '    If CDate(txtMRRDate.Text) < CDate(txtBillDate.Text) Then						
        '        MsgBox "VDate Can Not be Less Than BillDate."						
        '        FieldsVarification = False						
        '        If txtBillDate.Enabled = True Then txtBillDate.SetFocus						
        '        Exit Function						
        '    End If						


        If Trim(TxtSupplier.Text) = "" Then
            MsgBox("Supplier Cannot Be Blank", MsgBoxStyle.Information)
            ' txtSupplier.SetFocus						
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtBillTo.Text) = "" Then
            MsgBox("Supplier Location Cannot Be Blank", MsgBoxStyle.Information)
            ' txtSupplier.SetFocus						
            FieldsVarification = False
            Exit Function
        End If

        If lblBookType.Text = "Q" And (VB.Left(cboRefType.Text, 1) = "I" Or VB.Left(cboRefType.Text, 1) = "2") Then
            If CheckWareHouseDivision(mDivisionCode) = "N" Then
                MsgBox("Cann't be done Sale Return QC From here. Please contact to System Administrator.")
                FieldsVarification = False
                Exit Function
            End If
        End If

        If mAuthorised = False Then
            If mSameGSTNo = "N" Then
                If lblBookType.Text <> "Q" Then
                    If VB.Left(cboRefType.Text, 1) = "F" Then
                        MsgBox("You Have No Right To Entered FOC Invoice.")
                        FieldsVarification = False
                        Exit Function
                    End If
                End If
            End If
        End If


        If DuplicateBillNo(mSupplierCode) = True Then
            MsgBox("Duplicate Bill No for Such Supplier.", MsgBoxStyle.Information)
            If txtBillNo.Enabled = True Then txtBillNo.Focus()
            FieldsVarification = False
            Exit Function
        End If

        mWithInState = "Y"

        mWithInState = GetPartyBusinessDetail(Trim(TxtSupplier.Text), Trim(txtBillTo.Text), "WITHIN_STATE")
        'If MainClass.ValidateWithMasterTable((TxtSupplier.Text), "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
        '    MsgBox("Supplier Does Not Exist In Master", MsgBoxStyle.Information)
        '    'txtSupplier.SetFocus						
        '    FieldsVarification = False
        '    Exit Function
        'Else
        '    mWithInState = IIf(IsDbNull(MasterNo), "Y", MasterNo)
        'End If

        'sk21-11-2005						
        '    If Val(CDbl(lblNetAmount.text)) >= 10000 And mWithInState = "N" Then						


        If RsCompany.Fields("EWAYBILLAPP").Value = "Y" Then
            meBillNoApp = "Y"
        Else
            meBillNoApp = IIf(mWithInState = "Y", "N", "Y")
        End If


        meBillNoAppDate = "01/06/2018"

        SprdMain.Row = 1
        SprdMain.Col = ColItemCode
        mItemCode = Trim(SprdMain.Text)

        If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "GST_ITEMCLASS", "INV_ITEM_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mItemGSTClass = MasterNo
        End If

        If VB.Left(cboMode.Text, 1) = "1" Or VB.Left(cboMode.Text, 1) = "7" Then
        Else
            If Val(CStr(CDbl(lblNetAmount.Text))) >= 50000 And meBillNoApp = "Y" And mItemGSTClass = "0" Then
                If CDate(meBillNoAppDate) <= CDate(txtBillDate.Text) Then
                    If Val(txtEwayBillNo.Text) <= 0 Then
                        '                If MsgQuestion("eWay Bill No is Blank. You Want to Continue ...") = vbNo Then						
                        MsgInformation("eWay Bill No is Blank, So cann't be Save.")
                        FieldsVarification = False
                        txtEwayBillNo.Focus()
                        Exit Function
                        '                End If						
                    End If
                End If
            End If
        End If

        'If Val(txtEwayBillNo.Text) > 0 Then						
        '    If WebRequestFetch((txtEwayBillNo.Text)) = True Then						

        '    Else						
        '        '                MsgInformation "eWay Bill No is Blank, So cann't be Save."						
        '        '                FieldsVarification = False						
        '        '                txtEwayBillNo.SetFocus						
        '        '                Exit Function						
        '    End If						
        'End If						


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
            End If
        End If

        If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "ItemCode Is Blank.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColBillQty, "N", "Please Check Bill Quantity.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColStockType, "S", "Please Check Stock Type.") = False Then FieldsVarification = False : Exit Function

        If lblBookType.Text = "G" And cboRefType.SelectedIndex = 2 And lblSaleReturn.Text = "N" Then
            MsgBox("You have not right to save such ref type MRR.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        If lblSaleReturn.Text = "Y" Then
            If cboRefType.SelectedIndex <> 2 Then
                MsgBox("You have not right to save such ref type MRR.", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
        End If

        mValidQCUser = False
        With SprdMain
            For mRow = 1 To .MaxRows
                .Row = mRow
                .Col = ColItemCode
                If Trim(.Text) = "" Then GoTo NextRow

                .Col = ColPONo
                mPONo = .Text

                If VB.Left(cboRefType.Text, 1) = "R" Then
                    mRGPPurpose = GetValidRGPPurpose(Val(mPONo), "")
                    If mRGPPurpose = True Then
                        mCntRGPPaidType = mCntRGPPaidType + 1
                    Else
                        mCntRGPFOCType = mCntRGPFOCType + 1
                    End If

                    '                If mCntRGPPaidType > 0 And mCntRGPFOCType > 0 Then						
                    '                    MsgBox "Please make Paid or FOC purpose RGP in Separate MRR.", vbInformation						
                    '                    FieldsVarification = False						
                    '                    Exit Function						
                    '                End If						
                End If

                .Col = ColItemCode
                mItemCode = Trim(.Text)
                mProdType = GetProductionType(mItemCode)


                If GetOutJobworkManyItem(Trim(.Text), Trim(txtMRRDate.Text)) = False And VB.Left(cboRefType.Text, 1) = "R" Then
                    .Row = mRow

                    .Col = ColRGPItemCode
                    mRGPItemCode = Trim(.Text)

                    .Col = ColBalQty
                    mBalanceQty = Val(.Text)


                    mRGPBalanceQty = CalcRGPBalanceQty(Val(mPONo), mRGPItemCode, mSupplierCode)
                    '                mRecdQty = VB6.Format(xInConUnit * mRecdQty / xOutConUnit, "0.0000")						

                    mRecdQty = 0

                    For ii = 1 To .MaxRows
                        .Row = ii
                        .Col = ColPONo
                        If CDbl(mPONo) = Val(.Text) Then
                            .Col = ColRGPItemCode
                            If mRGPItemCode = Trim(.Text) Then
                                .Col = ColItemCode
                                mConsQty = GetConsQty(mRGPItemCode, Trim(.Text))
                                .Col = ColReceivedQty
                                mRecdQty = mRecdQty + (Val(.Text) * mConsQty)
                            End If
                        End If
                    Next

                    If mRGPBalanceQty < mRecdQty Then
                        MsgInformation("RGP [" & mPONo & "] Balance Qty is Less than Received Qty, So cann't be Saved. [" & mRGPItemCode & "]")
                        MainClass.SetFocusToCell(SprdMain, mRow, ColItemCode)
                        FieldsVarification = False
                        Exit Function
                    End If
                Else
                    If ValidateRefNo(mItemCode, mBillFromSupplier, mPONo, mDivisionCode) = False Then
                        MsgBox("Invalid PO Ref., So cann't be saved. Ref No [" & mPONo & "] & Item Code [ " & mItemCode & "]", MsgBoxStyle.Information)
                        FieldsVarification = False
                        Exit Function
                    End If
                End If

                .Row = mRow

                .Col = ColBillQty
                If Val(.Text) > 0 And VB.Left(cboRefType.Text, 1) = "R" Then
                    Dim mJWUOM As String

                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_JW_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mJWUOM = MasterNo
                    End If

                    .Col = ColUnit
                    xUnit = Trim(.Text)

                    If Trim(UCase(mJWUOM)) <> Trim(UCase(xUnit)) Then
                        .Col = ColQtyInKgs
                        If Val(.Text) = 0 Then
                            MsgInformation("Please Enter the Qty in Kgs Also.")
                            FieldsVarification = False
                            Exit Function
                        End If
                        .Col = ColRecdQtyInKgs
                        If Val(.Text) = 0 Then
                            MsgInformation("Please Enter the Recd Qty in Kgs Also.")
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If
                End If

                'If chkShipTo.CheckState = System.Windows.Forms.CheckState.Unchecked And (mProdType = "R" Or mProdType = "D" Or mProdType = "3") Then
                '    .Col = ColUnit
                '    xUnit = Trim(.Text)
                '    If xUnit = "KGS" Or xUnit = "TON" Or xUnit = "MT" Then
                '        .Col = ColBillQty
                '        xBillQty = Val(.Text)
                '        If xUnit = "KGS" Then
                '            .Col = ColQtyInKgs
                '            .Text = CStr(Val(CStr(xBillQty)))
                '        Else
                '            .Col = ColQtyInKgs
                '            .Text = CStr(Val(CStr(xBillQty * 1000)))
                '        End If
                '    Else
                '        .Col = ColQtyInKgs
                '        If Val(.Text) = 0 Then
                '            MsgInformation("Please Enter the Qty in Kgs Also.")
                '            FieldsVarification = False
                '            Exit Function
                '        End If
                '    End If
                'End If
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColRejQty
                mRejQty = Val(.Text)

                '						
                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS='I'") = True Then
                    MsgInformation("Item Status is Closed, So cann't be Saved. [" & mItemCode & "]")
                    MainClass.SetFocusToCell(SprdMain, mRow, ColItemCode)
                    FieldsVarification = False
                    Exit Function
                End If
                '                Else						
                If ADDMode = False Then
                    If CheckReofferMade(mItemCode, pReofferNo) = True Then
                        If PubSuperUser = "S" Then
                            If MsgQuestion("Reoffer No : " & pReofferNo & " is Made, So cann't be Modify. [" & mItemCode & "]. Do you still want to Process? ") = CStr(MsgBoxResult.No) Then
                                MainClass.SetFocusToCell(SprdMain, mRow, ColItemCode)
                                FieldsVarification = False
                                Exit Function
                            End If
                        Else
                            MsgInformation("Reoffer No : " & pReofferNo & " is Made, So cann't be Modify. [" & mItemCode & "]")
                            MainClass.SetFocusToCell(SprdMain, mRow, ColItemCode)
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If

                    If CheckDNCNMade(mItemCode, mRejQty, mVNo) = True Then
                        MsgInformation("Debit Note Made (" & mVNo & "), So cann't be Modify. [" & mItemCode & "]")
                        MainClass.SetFocusToCell(SprdMain, mRow, ColItemCode)
                        FieldsVarification = False
                        Exit Function
                    End If

                End If

                ''28-07-2011 ''Check at the time of save.  ' PubSuperUser = "U" And						

                If PubInvLevelAPPUser = "N" Then
                    If lblBookType.Text = "G" Then
                        If VB.Left(cboRefType.Text, 1) = "F" Or VB.Left(cboRefType.Text, 1) = "C" Then
                            mProdType = GetProductionType(mItemCode)

                            If mProdType = "1" Or mProdType = "2" Then

                            Else
                                MsgInformation("Cann't be save in FOC or Cash.")
                                FieldsVarification = False
                                Exit Function
                            End If
                            '                            If CheckOpenOrder(mItemCode, mDivisionCode) = True Then						
                            '                                MsgInformation "Open Order Made for this Supplier for Item Code : " & mItemCode & ". So cann't be save in FOC or Cash"						
                            '                                FieldsVarification = False						
                            '                                Exit Function						
                            '                            End If						
                        End If
                    End If
                End If

                .Col = ColReceivedQty
                If Val(.Text) <= 0 Then
                    MsgInformation("Received Qty Cann't not be Zero.")
                    FieldsVarification = False
                    Exit Function
                End If

                If CheckBillQty(ColReceivedQty, mRow) = False Then
                    '                        MainClass.SetFocusToCell SprdMain, mRow, ColBillQty						
                    FieldsVarification = False
                    Exit Function
                End If

                .Col = ColQCEMP
                mQCEmp = Trim(.Text)

                .Col = ColStockType
                mStockType = Trim(.Text)

                If VB.Left(cboRefType.Text, 1) = "P" Then
                    If lblBookType.Text = "Q" Then
                        If CheckBillQty(ColAcceptQty, mRow) = False Then
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If
                ElseIf VB.Left(cboRefType.Text, 1) = "I" Or VB.Left(cboRefType.Text, 1) = "1" Or VB.Left(cboRefType.Text, 1) = "2" Or VB.Left(cboRefType.Text, 1) = "R" Or VB.Left(cboRefType.Text, 1) = "3" Then
                    '                    If PubSuperUser = "U" Then						
                    If CheckBillQty(ColAcceptQty, mRow) = False Then
                        FieldsVarification = False
                        Exit Function
                    End If
                    '                    End If						
                End If

                If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 114 Then
                    If mQCEmp = "" Then
                        mQCEmp = PubUserEMPCode
                    End If
                Else
                    If mQCEmp = "" Then
                        MsgInformation("Please Check QC EMP.")
                        MainClass.SetFocusToCell(SprdMain, mRow, ColQCEMP)
                        FieldsVarification = False
                        Exit Function
                    End If

                    'mQCEmp = VB6.Format(mQCEmp, "000000")
                    If MainClass.ValidateWithMasterTable(mQCEmp, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                        MsgInformation("InValid QC Employee")
                        MainClass.SetFocusToCell(SprdMain, mRow, ColQCEMP)
                        FieldsVarification = False
                        Exit Function
                    End If

                    .Col = ColQCDate
                    mQCDate = Trim(.Text)

                    If lblBookType.Text = "Q" And mQCDate = "" And mStockType <> "QC" And PubUserEMPCode <> mQCEmp And PubATHUSER = False Then
                        MsgBox("You are not Valid User to done QC. For Item Code : " & mItemCode, MsgBoxStyle.Information)
                        FieldsVarification = False
                        Exit Function
                    End If
                End If


                '                If lblBookType.text = "Q" And mValidQCUser = False And PubUserEMPCode <> "" Then						
                '                If lblBookType.text = "Q" And PubUserEMPCode <> "" Then						
                '                    If PubUserEMPCode = mQCEMP Then						
                '                        mValidQCUser = True						
                '                    End If						
                '                End If						
                'End If  ''Sandeep

                '            If CheckValidStockType(mRow, ColStockType, ColItemCode, SprdMain) = False Then						
                '                MsgInformation "InValid Stock Type. Please Check"						
                '                MainClass.SetFocusToCell SprdMain, mRow, ColStockType						
                '                FieldsVarification = False						
                '                Exit Function						
                '            End If						

                If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 And VB.Left(cboRefType.Text, 1) = "I" Then
                ElseIf RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 And VB.Left(cboRefType.Text, 1) = "3" Then
                ElseIf VB.Left(cboRefType.Text, 1) = "I" Or VB.Left(cboRefType.Text, 1) = "3" Then
                    .Col = ColPONo
                    mBillNo = .Text
                    mIsFGInvoice = IsFGInvoice(mBillNo)
                    If mIsFGInvoice = True And VB.Left(cboRefType.Text, 1) = "3" Then
                        MsgInformation("It is FG Sale, So Please Select Valid Ref Type (FG Invoice Return).")
                        FieldsVarification = False
                        Exit Function
                    ElseIf IsFGInvoice(mBillNo) = False And VB.Left(cboRefType.Text, 1) = "I" Then
                        MsgInformation("It is RM/BOP Sale, So Please Select Valid Ref Type (RM/BOP Return).")
                        FieldsVarification = False
                        Exit Function
                    End If

                End If

                If lblBookType.Text = "Q" Then
                    .Col = ColItemCode
                    mItemCode = Trim(.Text)

                    ''above code						
                    '                If Left(cboRefType.Text, 1) = "P" Then						
                    '                    SprdMain.Col = ColAcceptQty						
                    '                    mAcceptQty = Val(SprdMain.Text)						
                    '						
                    '                    If CheckBillQty(ColAcceptQty, mRow) = True Then						
                    '                        MsgInformation "Accepted Qty Cann't be Greater Than Balance PO Qty."						
                    '                        FieldsVarification = False						
                    '                        MainClass.SetFocusToCell SprdMain, mRow, ColAcceptQty						
                    '                        Exit Function						
                    '                    End If						
                    '                End If						

                    mLotNoRequied = "N"
                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mLotNoRequied = MasterNo
                    End If
                    If mLotNoRequied = "Y" Then
                        .Col = ColBatchNo
                        If Trim(.Text) = "" Then
                            MsgInformation("Lot No. Must For Such Item.")
                            FieldsVarification = False
                            MainClass.SetFocusToCell(SprdMain, mRow, ColBatchNo)
                            Exit Function
                        End If
                    End If

                    mHeatNo = "N"
                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "HEAT_NO_REQ", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mHeatNo = MasterNo
                    End If
                    If mHeatNo = "Y" Then
                        .Col = ColHeatNo
                        If Trim(.Text) = "" Then
                            MsgInformation("Heat No. Is Must For Such Item.")
                            FieldsVarification = False
                            MainClass.SetFocusToCell(SprdMain, mRow, ColHeatNo)
                            Exit Function
                        End If
                    Else
                        .Col = ColHeatNo
                        If Trim(.Text) <> "" Then
                            MsgInformation("Heat No. is not activated for Such Item.")
                            FieldsVarification = False
                            MainClass.SetFocusToCell(SprdMain, mRow, ColHeatNo)
                            Exit Function
                        End If
                    End If

                    .Col = ColStockType
                    If Trim(.Text) = "ST" Or Trim(.Text) = "CS" Or Trim(.Text) = "CR" Or Trim(.Text) = "FC" Or Trim(.Text) = "QC" Or Trim(.Text) = "SC" Then

                    Else
                        MsgInformation("Invalid Stock Type.")
                        FieldsVarification = False
                        '                    MainClass.SetFocusToCell SprdMain, mRow, ColStockType						
                        Exit Function
                    End If

                    If VB.Left(cboRefType.Text, 1) = "3" Then
                        .Col = ColStockType
                        If Trim(.Text) = "ST" Or Trim(.Text) = "RJ" Then

                        Else
                            MsgInformation("Stock Type Must Be ST / RJ for Invoice Rejection.")
                            FieldsVarification = False
                            '                        MainClass.SetFocusToCell SprdMain, mRow, ColStockType						
                            Exit Function
                        End If
                    ElseIf VB.Left(cboRefType.Text, 1) = "I" Or VB.Left(cboRefType.Text, 1) = "2" Then

                        .Col = ColPONo
                        mBillNo = .Text
                        If IsFGInvoice(mBillNo) = True Then
                            .Col = ColStockType
                            If Trim(.Text) <> "CR" Then
                                MsgInformation("Stock Type Must Be CR for Invoice Rejection.")
                                FieldsVarification = False
                                '                                MainClass.SetFocusToCell SprdMain, mRow, ColStockType						
                                Exit Function
                            End If
                        Else
                            .Col = ColStockType
                            If Trim(.Text) <> "ST" Then
                                '                            If Trim(.Text) <> "CR" Then						
                                MsgInformation("Stock Type Must Be ST for Invoice Rejection.")
                                FieldsVarification = False
                                '                                    MainClass.SetFocusToCell SprdMain, mRow, ColStockType						
                                Exit Function
                                '                            End If						
                            End If
                        End If

                    End If

                    If VB.Left(cboRefType.Text, 1) = "P" Or VB.Left(cboRefType.Text, 1) = "C" Or VB.Left(cboRefType.Text, 1) = "F" Then
                        .Col = ColStockType

                        If Trim(.Text) = "ST" Or Trim(.Text) = "QC" Then

                        Else
                            MsgInformation("Stock Type Must Be ST Or QC.")
                            FieldsVarification = False
                            '                        MainClass.SetFocusToCell SprdMain, mRow, ColStockType						
                            Exit Function
                        End If

                    End If

                    Dim mInterUnit As String = "N"

                    mInterUnit = "N"
                    If MainClass.ValidateWithMasterTable((TxtSupplier.Text), "SUPP_CUST_NAME", "INTER_UNIT", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mInterUnit = MasterNo
                    End If

                    If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 And mInterUnit = "Y" Then

                    Else
                        If VB.Left(cboRefType.Text, 1) = "J" Or VB.Left(cboRefType.Text, 1) = "1" Then
                            .Col = ColStockType
                            'If chkUnderChallan.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                            If Trim(.Text) <> "CS" Then
                                MsgInformation("Stock Type Must Be CS.")
                                FieldsVarification = False
                                '                        MainClass.SetFocusToCell SprdMain, mRow, ColStockType						
                                Exit Function
                            End If
                            'Else
                            '    If Trim(.Text) <> "ST" Then
                            '        MsgInformation("Stock Type Must Be ST.")
                            '        FieldsVarification = False
                            '        '                        MainClass.SetFocusToCell SprdMain, mRow, ColStockType						
                            '        Exit Function
                            '    End If
                            'End If
                        End If
                    End If

                    If VB.Left(cboRefType.Text, 1) <> "R" Then
                        If VB.Left(cboRefType.Text, 1) <> "J" And VB.Left(cboRefType.Text, 1) <> "1" Then
                            If NonApprovedItemExists(mItemCode, mSupplierCode) = False Then
                                If MsgQuestion("Item Code (" & mItemCode & ") is Not Approved from this Supplier. Do you still want to pass the material ? ") = CStr(MsgBoxResult.No) Then
                                    MainClass.SetFocusToCell(SprdMain, mRow, ColItemCode)
                                    FieldsVarification = False
                                    Exit Function
                                End If
                            End If
                        End If
                    End If
                    '            Else						


                    '                If Left(cboRefType.Text, 1) = "J" Then						
                    '                    .Col = ColItemCode						
                    '                    mItemCode = Trim(.Text)						
                    '						
                    '                    .Col = ColUnit						
                    '                    mItemUOM = Trim(.Text)						
                    '						
                    '                    .Col = ColStockType						
                    '                    mStockType = Trim(.Text)						
                    '						
                    '						
                    '                    mMaxStockQty = 0						
                    '                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "MAXIMUM_QTY", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then						
                    '                        mMaxStockQty = MasterNo						
                    '                    End If						
                    '                    If mMaxStockQty > 0 Then						
                    '                        mStockQty = GetBalanceStockQty(mItemCode, txtMRRDate.Text, mItemUOM, "STR", "", "", ConWH)						
                    '                        mStockQty = mStockQty + GetBalanceStockQty(mItemCode, txtMRRDate.Text, mItemUOM, "", "", "", ConPH)						
                    '                        If mMaxStockQty < mStockQty Then						
                    '                            If MsgQuestion("Stock Qty of Item Code : " & mItemCode & " is " & mStockQty & " " & mItemUOM & " is higher than Maximum Level. Do you still want to continue.. ? ") = vbNo Then						
                    '                                MainClass.SetFocusToCell SprdMain, mRow, ColItemCode						
                    '                                FieldsVarification = False						
                    '                                Exit Function						
                    '                            End If						
                    '                        End If						
                    '                    End If						
                    '                End If						
                End If
NextRow:
            Next
        End With

        '    If lblBookType.text = "Q" And mValidQCUser = False And PubATHUSER = False Then						
        '        MsgBox "You are not Valid User to done QC.", vbInformation						
        '        FieldsVarification = False						
        '        Exit Function						
        '    End If						

        With SprdMain
            For mRow = 1 To .MaxRows
                .Row = mRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                If Trim(mItemCode) <> "" Then

                    If lblBookType.Text = "G" Then
                        If VB.Left(cboRefType.Text, 1) = "P" Or VB.Left(cboRefType.Text, 1) = "I" Or VB.Left(cboRefType.Text, 1) = "2" Or VB.Left(cboRefType.Text, 1) = "3" Then
                            .Col = ColPORate
                            mPORate = Val(SprdMain.Text)

                            .Col = ColRate
                            mBillRate = Val(SprdMain.Text)

                            If RsCompany.Fields("CHECK_PO_RATE").Value = "Y" Then
                                If mPORate <> mBillRate Then
                                    MsgInformation("For Item Code :" & mItemCode & ", Bill Rate is not Match with PO Rate.")
                                    MainClass.SetFocusToCell(SprdMain, mRow, ColRate)
                                    FieldsVarification = False
                                    Exit Function
                                End If
                            End If
                        End If
                        If CheckItemLock(mItemCode, "M", mSupplierCode) = True Then
                            MsgInformation("MRR Lock For Item Code : " & mItemCode & ". So cann't be made MRR for this Item.")
                            FieldsVarification = False
                            Call MainClass.SetFocusToCell(SprdMain, mRow, ColAcceptQty)
                            Exit Function
                        End If

                        'mMaxLevelQty = GetInventoryLevelQty(mItemCode, "MAXIMUM_QTY")
                        'mMRRQty = GetMRRItemQty(mItemCode)

                        'mItemCategory = GetProductionType(mItemCode)

                        'If (mItemCategory = "P" Or mItemCategory = "R" Or mItemCategory = "B" Or mItemCategory = "I" Or mItemCategory = "D" Or mItemCategory = "3") Then
                        '    If VB.Left(cboRefType.Text, 1) = "F" Or VB.Left(cboRefType.Text, 1) = "C" Or VB.Left(cboRefType.Text, 1) = "P" Then
                        '        If mMaxLevelQty > 0 Then
                        '            If mMRRQty > mMaxLevelQty Then
                        '                If CheckMaxLevelApproval(mItemCode, (txtMRRDate.Text), mSupplierCode, mMRRQty) = False Then
                        '                    MsgInformation("MRR Qty cann't be More than Max Level Qty (" & mMaxLevelQty & "). For Item Code : " & mItemCode & ". So cann't be made MRR Entry for this Item.")
                        '                    FieldsVarification = False
                        '                    Call MainClass.SetFocusToCell(SprdMain, mRow, ColAcceptQty)
                        '                    Exit Function
                        '                End If
                        '            End If
                        '        End If
                        '    End If
                        'End If

                        'If VB.Left(cboRefType.Text, 1) = "F" Or VB.Left(cboRefType.Text, 1) = "C" Or VB.Left(cboRefType.Text, 1) = "P" Then
                        '    If CheckItemLock(mItemCode, "O") = True Or CheckMaxLevel(mItemCode) Then
                        '        '                                    Or (RsCompany!BOP_MAX_LEVEL = "Y" And (mItemCategory = "P" Or mItemCategory = "R" Or mItemCategory = "B" Or mItemCategory = "I" Or mItemCategory = "D" Or mItemCategory = "3")) _						
                        '        ''                                    Or (RsCompany!CONS_MAX_LEVEL = "Y" And (mItemCategory = "G" Or mItemCategory = "C")) _						
                        '        ''                                    Or (RsCompany!MAINT_MAX_LEVEL = "Y" And mItemCategory = "M") _						
                        '        ''                                    Or (RsCompany!RM_MAX_LEVEL = "Y" And mItemCategory = "R") Then						
                        '        '						
                        '        mStockQty = GetBalanceStockQty(mItemCode, (txtMRRDate.Text), mItemUOM, "STR", "", "X", ConWH, mDivisionCode, "MRR", Val(txtMRRNo.Text))
                        '        mStockLockQty = GetInventoryLevelQty(mItemCode, "STOCK_LOCK_QTY")
                        '        mStockQty = mStockQty - mStockLockQty
                        '        mStockQty = IIf(mStockQty < 0, 0, mStockQty)

                        '        '                        mMaxLevelQty = mMaxLevelQty  ''* 3						

                        '        If mStockQty + mMRRQty > mMaxLevelQty Then
                        '            If CheckMaxLevelApproval(mItemCode, (txtMRRDate.Text), mSupplierCode, mMRRQty) = False Then
                        '                MsgInformation("You already cross the Max Level (" & mMaxLevelQty & ")" & " for Item Code : " & mItemCode & ". Stock Qty is (" & mStockQty & "). Approval is Must for Entry.")
                        '                FieldsVarification = False
                        '                Exit Function
                        '            End If
                        '        End If
                        '    End If
                        'End If
                    End If
                    .Row = mRow
                    .Col = ColBillQty
                    mBillQty = Val(.Text)

                    .Col = ColReceivedQty
                    mRecdQty = Val(.Text)

                    .Col = ColAcceptQty
                    mAcceptQty = Val(.Text)

                    .Col = ColUnit
                    mItemUOM = Trim(.Text)

                    If lblBookType.Text = "G" Then
                        If mBillQty < mRecdQty Then
                            If PubSuperUser = "U" Then
                                MsgInformation("Received Qty is greater than Bill Qty For Item Code : " & mItemCode & ".")
                                FieldsVarification = False
                                Call MainClass.SetFocusToCell(SprdMain, mRow, ColAcceptQty)
                                Exit Function
                            Else
                                If MsgQuestion("Received Qty is greater than Bill Qty For Item Code : " & mItemCode & ". Are you want to Continue....") = CStr(MsgBoxResult.No) Then
                                    FieldsVarification = False
                                    Call MainClass.SetFocusToCell(SprdMain, mRow, ColReceivedQty)
                                    Exit Function
                                End If
                            End If
                        ElseIf mBillQty <> mRecdQty Then
                            If MsgQuestion("Received Qty is not match with Bill Qty For Item Code : " & mItemCode & ". Are you want to Continue....") = CStr(MsgBoxResult.No) Then
                                FieldsVarification = False
                                Call MainClass.SetFocusToCell(SprdMain, mRow, ColReceivedQty)
                                Exit Function
                            End If
                        End If
                    Else
                        If mRecdQty < mAcceptQty Then
                            '                        If MsgQuestion("Accepted Qty is Less than Received Qty For Item Code : " & mItemCode & ". Are you want to Continue....") = vbNo Then						
                            MsgInformation("Accepted Qty is greater than Received Qty For Item Code : " & mItemCode & ".")
                            FieldsVarification = False
                            Call MainClass.SetFocusToCell(SprdMain, mRow, ColAcceptQty)
                            Exit Function
                            '                        End If						
                        ElseIf mRecdQty <> mAcceptQty Then
                            If MsgQuestion("Accepted Qty is not match with Received Qty For Item Code : " & mItemCode & ". Are you want to Continue....") = CStr(MsgBoxResult.No) Then
                                FieldsVarification = False
                                Call MainClass.SetFocusToCell(SprdMain, mRow, ColAcceptQty)
                                Exit Function
                            End If
                        End If
                    End If
                End If
            Next
        End With


        If Trim(cboMode.Text) = "" And lblBookType.Text = "G" Then
            MsgBox("Please Enter Mode Type.", MsgBoxStyle.Information)
            SSTab1.SelectedIndex = 1
            cboMode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If OptFreight(0).Checked = True And lblBookType.Text = "G" Then
            If Trim(TxtTransporter.Text) = "" Then
                MsgBox("Please Enter Transporter Name.", MsgBoxStyle.Information)
                SSTab1.SelectedIndex = 1
                TxtTransporter.Focus()
                FieldsVarification = False
                Exit Function
            End If

            If Trim(txtVehicle.Text) = "" Then
                MsgBox("Please Enter Vehicle No.", MsgBoxStyle.Information)
                SSTab1.SelectedIndex = 1
                txtVehicle.Focus()
                FieldsVarification = False
                Exit Function
            End If

            If Trim(txtGRNo.Text) = "" Then
                MsgBox("Please Enter GR No.", MsgBoxStyle.Information)
                SSTab1.SelectedIndex = 1
                txtGRNo.Focus()
                FieldsVarification = False
                Exit Function
            End If

            If Trim(txtGRDate.Text) = "" Then
                MsgBox("Please Enter GR Date.", MsgBoxStyle.Information)
                SSTab1.SelectedIndex = 1
                txtGRDate.Focus()
                FieldsVarification = False
                Exit Function
            End If
            If Trim(txtFreight.Text) = "" Then
                MsgBox("Please Enter Freight Amount.", MsgBoxStyle.Information)
                SSTab1.SelectedIndex = 1
                txtFreight.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If lblBookType.Text = "Q" Then

            With SprdMain
                For mRow = 1 To .MaxRows
                    .Row = mRow
                    .Col = ColItemCode
                    mItemCode = Trim(.Text)

                    .Col = ColQCEMP
                    mQCEmp = Trim(.Text)

                    .Col = ColStockType
                    mStockType = Trim(.Text)

                    mActualQCEmp = GetItemQCEmp(mItemCode)

                    If mActualQCEmp <> "" Then
                        If Trim(mQCEmp) <> Trim(mActualQCEmp) And mStockType <> "QC" Then
                            MsgBox("You are not Valid User to done QC for Item Code : " & mItemCode, MsgBoxStyle.Information)
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If
                Next
            End With

        End If

        If mCntRGPPaidType > 0 And mCntRGPFOCType > 0 Then
            If MsgQuestion("You are making Paid and FOC purpose RGP Together in this MRR. Are you want to Continue....") = CStr(MsgBoxResult.No) Then
                FieldsVarification = False
                Exit Function
            End If
        End If

        Exit Function
err_Renamed:
        Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        FieldsVarification = False
        '    Resume						
    End Function
    Private Function CheckBillToShipFrom(ByVal mPartyCode As String) As Boolean

        On Error GoTo ErrPart1
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xPoNo As Double
        Dim mShipTo As String
        Dim mShipToCode As String

        Dim mMRRShipTo As String
        Dim mMRRShipToCode As String
        Dim CntRow As Integer

        If VB.Left(cboRefType.Text, 1) <> "P" Then
            CheckBillToShipFrom = True
            Exit Function
        End If

        CheckBillToShipFrom = False

        If MainClass.ValidateWithMasterTable((txtShippedTo.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mMRRShipToCode = MasterNo
        End If

        mMRRShipTo = IIf(chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        For CntRow = 1 To SprdMain.MaxRows - 1
            mShipTo = "Y"
            mShipToCode = ""
            SprdMain.Row = CntRow
            SprdMain.Col = ColPONo
            xPoNo = Val(SprdMain.Text)

            mSqlStr = "SELECT IH.SHIPPED_TO_SAMEPARTY, IH.SHIPPED_TO_PARTY_CODE " & vbCrLf & " FROM PUR_PURCHASE_HDR IH " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_PO=" & xPoNo & " " & vbCrLf & " AND IH.PO_STATUS='Y'  AND IH.PO_CLOSED='N'"

            MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                mShipTo = IIf(IsDBNull(RsTemp.Fields("SHIPPED_TO_SAMEPARTY").Value), "N", RsTemp.Fields("SHIPPED_TO_SAMEPARTY").Value)
                mShipToCode = IIf(IsDBNull(RsTemp.Fields("SHIPPED_TO_PARTY_CODE").Value), "", RsTemp.Fields("SHIPPED_TO_PARTY_CODE").Value)
            End If

            If mMRRShipTo <> mShipTo Then
                MsgInformation("Ship To Condition is not match with PO")
                CheckBillToShipFrom = False
                Exit Function
            End If

            If mMRRShipTo = "N" Then
                If mPartyCode <> mShipToCode Then
                    MsgInformation("Ship From Party is not match with PO")
                    CheckBillToShipFrom = False
                    Exit Function
                End If
            End If
        Next

        CheckBillToShipFrom = True
        Exit Function
ErrPart1:
        '    Resume						
        CheckBillToShipFrom = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function NonApprovedItemExists(ByVal nItemCode As String, ByVal pSuppCode As String) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing '' ADODB.Recordset						

        NonApprovedItemExists = False

        If MainClass.ValidateWithMasterTable(nItemCode, "ITEM_CODE", "ITEM_APPROVED", "FIN_SUPP_CUST_DET", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pSuppCode) & "'") = True Then
            If MasterNo = "Y" Then
                NonApprovedItemExists = True
                Exit Function
            End If
        End If

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
        Exit Function
ErrPart:
        NonApprovedItemExists = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Public Sub FrmMRR_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If lblBookType.Text = "Q" Then
            Me.Text = "MRR - QC Entry"
        Else
            Me.Text = "MRR Entry"
        End If

        SqlStr = ""
        SqlStr = "Select * from INV_GATE_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMRRMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from INV_GATE_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMRRDetail, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from INV_GATE_EXP Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMRRExp, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)
        Call SetTextLengths()
        mSupplierCode = CStr(-1)

        If lblBookType.Text = "Q" Then
            cmdAdd.Visible = False
            cmdMRRSearch.Visible = True
            SSTab1.Height = VB6.TwipsToPixelsY(4035)
            SprdMain.Height = VB6.TwipsToPixelsY(4035 - 400)
            SSTab1.SelectedIndex = 0
            MainClass.ClearGrid(SprdMain)
            Call FormatSprdMain(-1)
            Clear1()
        Else
            cmdAdd.Visible = True
            cmdMRRSearch.Visible = False
            If cmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        End If


        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ''Resume						
    End Sub
    Private Sub AssignGrid(ByVal mRefresh As Boolean)

        On Error GoTo AssignGridErr
        Dim SqlStr As String = ""
        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim sql As String
        Dim i As Integer
        Dim inti As Integer

        oledbCnn = New OleDbConnection(StrConn)
        SqlStr = ""

        'MainClass.ClearGrid(SprdView)

        SqlStr = "Select REF_TYPE,GR.AUTO_KEY_MRR as MRR_No1,CONCAT(SUBSTR(AUTO_KEY_MRR,0,LENGTH(AUTO_KEY_MRR)-6),CONCAT('-',SUBSTR(AUTO_KEY_MRR,LENGTH(AUTO_KEY_MRR)-5,LENGTH(AUTO_KEY_MRR)))) AS MRR_NO," & vbCrLf _
            & " TO_CHAR(GR.MRR_DATE,'DD-MM-YYYY') as MRR_Date, " & vbCrLf _
            & " GR.GATE_ENTRY as GATE_ENTRY, TO_CHAR(GR.GATEDATE,'DD-MM-YYYY') as GATEDATE, " & vbCrLf _
            & " AC.SUPP_CUST_NAME AS SupplierName, " & vbCrLf & " GR.BILL_NO, " & vbCrLf & " TO_CHAR(GR.BILL_DATE,'DD-MM-YYYY') AS BillDate,GR.QC_STATUS " & vbCrLf & " FROM INV_GATE_HDR GR,FIN_SUPP_CUST_MST AC " & vbCrLf & " WHERE " & vbCrLf & " GR.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUBSTR(AUTO_KEY_MRR,LENGTH(AUTO_KEY_MRR)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND GR.COMPANY_CODE=AC.COMPANY_CODE " & vbCrLf & " AND GR.SUPP_CUST_CODE=AC.SUPP_CUST_CODE " & vbCrLf & " Order by AUTO_KEY_MRR DESC"

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
            UltraGrid1.DisplayLayout.Bands(0).Columns(0).Header.Caption = "Ref Type"
            UltraGrid1.DisplayLayout.Bands(0).Columns(1).Header.Caption = "MRR No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(2).Header.Caption = "MRR No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(3).Header.Caption = "MRR Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(4).Header.Caption = "Gate Entry No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(5).Header.Caption = "Gate Entry Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(6).Header.Caption = "Supplier Name"
            UltraGrid1.DisplayLayout.Bands(0).Columns(7).Header.Caption = "Bill No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(8).Header.Caption = "Bill Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(9).Header.Caption = "QC Done"


            ''enable/disable the columns
            For inti = 0 To UltraGrid1.DisplayLayout.Bands(0).Columns.Count - 1
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).CellActivation = Activation.NoEdit  ''  .AllowEdit
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).Header.Appearance.TextHAlign = HAlign.Center

                ''UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.SubDepartmentName).Style = UltraWinGrid.ColumnStyle.DropDown
                '' UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.Qty).Style = UltraWinGrid.ColumnStyle.DoubleNonNegative
                ''UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.SubDepartmentName).EditorComponent = cmbDepartment
            Next

            ' to define width of the columns
            UltraGrid1.DisplayLayout.Bands(0).Columns(0).Width = 50
            UltraGrid1.DisplayLayout.Bands(0).Columns(1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(2).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(3).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(4).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(5).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(6).Width = 350
            UltraGrid1.DisplayLayout.Bands(0).Columns(7).Width = 125
            UltraGrid1.DisplayLayout.Bands(0).Columns(8).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(9).Width = 100


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
    Private Sub FormatSprdView()

        'With SprdView
        '    .Row = -1

        '    .set_RowHeight(0, 600)

        '    .set_ColWidth(0, 600)

        '    .set_ColWidth(1, 600)
        '    .set_ColWidth(2, 1200)
        '    .set_ColWidth(3, 1200)
        '    .set_ColWidth(4, 4500)
        '    .set_ColWidth(5, 1200)
        '    .set_ColWidth(6, 1200)

        '    MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
        '    MainClass.SetSpreadColor(SprdView, -1)
        '    SprdView.set_RowHeight(-1, 300)
        '    .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
        '    MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        'End With
    End Sub
    Private Sub FormatSprdExp(ByVal Arow As Integer)

        On Error GoTo ERR1
        With SprdExp
            .Row = Arow
            .set_RowHeight(Arow, 10)

            .Col = ColExpName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(.Col, 20)

            .Col = ColExpPercent
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatMin = 0.0#
            .TypeFloatMax = 99.99
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeEditMultiLine = False
            .set_ColWidth(.Col, 6)

            .Col = ColExpAmt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatMin = -99999999999.99
            .TypeFloatMax = 99999999999.99
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .set_ColWidth(ColExpAmt, 8)
            .TypeEditMultiLine = False
            .set_ColWidth(.Col, 10)

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
        MainClass.SetSpreadColor(SprdExp, Arow)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume						
    End Sub
    Private Sub FormatSprdMain(ByVal Arow As Integer)

        On Error GoTo ERR1

        pShowCalc = False
        With SprdMain
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)

            .Col = ColPONo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsMRRMain.Fields("REF_AUTO_KEY_NO").Precision ''						
            '        .ColHidden = True						
            .set_ColWidth(ColPONo, 10)

            .Col = ColPODate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = 10 ''						
            If lblBookType.Text = "Q" Then
                .ColHidden = True
            Else
                .ColHidden = False
            End If
            .set_ColWidth(ColPODate, 8)

            .Col = ColRGPItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsMRRDetail.Fields("ITEM_CODE").DefinedSize ''						
            If lblBookType.Text = "Q" Then
                .ColHidden = True
            Else
                .ColHidden = False
            End If

            .set_ColWidth(ColRGPItemCode, 6)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsMRRDetail.Fields("ITEM_CODE").DefinedSize ''						
            .set_ColWidth(ColItemCode, 8)

            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(ColItemName, 18)
            .ColsFrozen = ColItemName

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("WO_DESCRIPTION", "PUR_PURCHASE_DET", PubDBCn)
            .set_ColWidth(ColItemDesc, 30)
            .ColsFrozen = ColItemDesc
            .ColHidden = IIf(VB.Left(cboRefType.Text, 1) = "R", False, True)

            .Col = ColHSNCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("HSN_CODE", "INV_ITEM_MST", PubDBCn)

            .Col = ColHeatNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = RsMRRDetail.Fields("HEAT_NO").DefinedSize
            .set_ColWidth(ColHeatNo, 10)
            .ColHidden = IIf(RsCompany.Fields("HEATNO_HIDE").Value = "N", False, True)

            .Col = ColBatchNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsMRRDetail.Fields("BATCH_NO").DefinedSize ''						
            '        .CellType = SS_CELL_TYPE_FLOAT						
            '        .TypeFloatDecimalPlaces = 0						
            ''        .TypeFloatDecimalChar = Asc(".")						
            '        .TypeFloatMax = "99999999999"						
            '        .TypeFloatMin = "-99999999999"						
            '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC						
            .set_ColWidth(ColBatchNo, 9)
            '        If lblBookType.text = "Q" Then						
            '           .ColHidden = True
            '        Else						
            '            .ColHidden = False						
            '        End If						
            '.ColHidden = True

            .ColHidden = IIf(RsCompany.Fields("BATCHNO_HIDE").Value = "N", False, True)

            '.Col = ColLotNo
            '.CellType = SS_CELL_TYPE_FLOAT
            '.TypeFloatDecimalPlaces = 0
            ''        .TypeFloatDecimalChar = Asc(".")						
            '.TypeFloatMax = CDbl("99999999999")
            '.TypeFloatMin = CDbl("-99999999999")
            '.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            ''        .CellType = SS_CELL_TYPE_INTEGER						
            ''        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC						
            '''        .CellType = SS_CELL_TYPE_EDIT						
            '''        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII						
            '''        .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE						
            '''        .TypeEditMultiLine = False						
            ''        .TypeEditLen = RsMRRDetail.Fields("LOT_NO").DefinedSize           ''						
            '.set_ColWidth(ColLotNo, 6)
            ''If lblBookType.Text = "Q" Then
            ''.ColHidden = False
            ''Else
            '.ColHidden = True
            ''End If

            .Col = ColUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = RsMRRDetail.Fields("ITEM_UOM").DefinedSize ''						
            .set_ColWidth(ColUnit, 4)
            If lblBookType.Text = "Q" Then
                .ColHidden = True
            Else
                .ColHidden = False
            End If

            .Col = ColPOQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColPOQty, 9)
            .ColHidden = True

            .Col = ColBalQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColBalQty, 9)
            If lblBookType.Text = "Q" Then
                .ColHidden = True
            Else
                .ColHidden = False
            End If

            .Col = ColBillQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColBillQty, 9)
            If lblBookType.Text = "Q" Then
                .ColHidden = True
            Else
                .ColHidden = False
            End If

            .Col = ColQtyInKgs
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColQtyInKgs, 9)
            'If lblBookType.Text = "Q" Then
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102, False, True)
            'Else
            '    .ColHidden = False
            'End If

            .Col = ColRecdQtyInKgs
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColRecdQtyInKgs, 9)
            'If lblBookType.Text = "Q" Then
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102, False, True)
            'Else
            '    .ColHidden = False
            'End If

            .Col = ColReceivedQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .set_ColWidth(ColReceivedQty, 9)
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC

            .Col = ColStockType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsMRRDetail.Fields("STOCK_TYPE").DefinedSize ''						
            .set_ColWidth(ColStockType, 5)

            .Col = ColPORate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = "999999999.9999"
            .TypeFloatMin = "-999999999.9999"
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColPORate, 9)
            If lblBookType.Text = "Q" Then
                .ColHidden = True
            Else
                .ColHidden = False
            End If

            .Col = ColRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.9999")
            .TypeFloatMin = CDbl("-999999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColRate, 9)
            If lblBookType.Text = "Q" Then
                .ColHidden = True
            Else
                .ColHidden = False
            End If

            .Col = ColAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .set_ColWidth(ColAmount, 9)
            If lblBookType.Text = "Q" Then
                .ColHidden = True
            Else
                .ColHidden = False
            End If

            .Col = ColItemCost
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .ColHidden = True

            .Col = ColAcceptQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.999")
            .TypeFloatMin = CDbl("-99999999999.999")
            .set_ColWidth(ColAcceptQty, 9)
            If lblBookType.Text = "Q" Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = ColApprovedQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.999")
            .TypeFloatMin = CDbl("-99999999999.999")
            .set_ColWidth(ColApprovedQty, 9)
            If lblBookType.Text = "Q" Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = ColShortQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.999")
            .TypeFloatMin = CDbl("-99999999999.999")
            .set_ColWidth(ColShortQty, 9)
            ''mukul
            'If lblBookType.Text = "Q" Then
            '    .ColHidden = False
            'Else
            '    .ColHidden = True
            'End If

            .Col = ColRejQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.999")
            .TypeFloatMin = CDbl("-99999999999.999")
            .set_ColWidth(ColRejQty, 9)
            If lblBookType.Text = "Q" Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = ColDevQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.999")
            .TypeFloatMin = CDbl("-99999999999.999")
            .set_ColWidth(ColDevQty, 9)
            If lblBookType.Text = "Q" Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = ColSeg
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.999")
            .TypeFloatMin = CDbl("-99999999999.999")
            .set_ColWidth(ColSeg, 9)
            If lblBookType.Text = "Q" Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = ColRework
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.999")
            .TypeFloatMin = CDbl("-99999999999.999")
            .set_ColWidth(ColRework, 9)
            If lblBookType.Text = "Q" Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = ColConvQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.999")
            .TypeFloatMin = CDbl("-99999999999.999")
            .set_ColWidth(ColConvQty, 9)
            .ColHidden = True

            .Col = ColQCEMP
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = RsMRRDetail.Fields("QC_EMP_CODE").DefinedSize ''						
            .set_ColWidth(ColQCEMP, 6)

            .Col = ColCT3No
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeEditLen = RsMRRDetail.Fields("CT3_NO").DefinedSize
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColCT3No, 9)
            .ColHidden = True

            .Col = ColPCNo
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeEditLen = RsMRRDetail.Fields("PC_NO").DefinedSize
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColPCNo, 9)
            .ColHidden = True

            .Col = ColSchdRtnFlag
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(.Col, 8.5)
            '        .Value = vbUnchecked						
            .ColHidden = True

            .Col = ColQCDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = 10
            .set_ColWidth(ColQCDate, 9)
            .ColHidden = True

            .Col = ColPDIRFlag
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(.Col, 8.5)
            '        .Value = vbUnchecked						
            If lblBookType.Text = "Q" Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If
        End With

        MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols)

        ''    If mWithOutOrder = False Then						
        ''        MainClass.ProtectCell SprdMain, 1, SprdMain.MaxRows, ColItemName, ColBalQty						
        ''    Else						
        'MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemName, ColHSNCode)
        ''MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColBatchNo, ColBatchNo)
        'MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColUnit, ColBalQty)
        ''    End If						

        If lblBookType.Text = "Q" Then
            'MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, ColHeatNo, ColBatchNo)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPONo, ColHSNCode)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColUnit, ColUnit)
            '        MainClass.ProtectCell SprdMain, 1, SprdMain.MaxRows, ColUnit, ColPOQty						
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColBillQty, ColReceivedQty)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColQtyInKgs, ColRecdQtyInKgs)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColQCEMP, ColQCEMP)
        Else
            'MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, ColBillQty, ColReceivedQty)
            MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, ColHeatNo, ColBatchNo)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColStockType, ColStockType)
            'MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, ColHeatNo, ColHeatNo)
        End If

        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemName, ColHSNCode)
        'MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColHeatNo, ColBatchNo)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPORate, ColPORate)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColUnit, ColBalQty)

        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColApprovedQty, ColRejQty)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColConvQty, ColConvQty)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColAmount, ColAmount)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColQCDate, ColQCDate)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPODate, ColRGPItemCode)

        If RsCompany.Fields("MRR_AGT_GE").Value = "Y" Then
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPONo, ColHSNCode)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColUnit, ColBillQty)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColQtyInKgs, ColQtyInKgs)
        End If
        'MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, ColHeatNo, ColBatchNo)

        MainClass.SetSpreadColor(SprdMain, Arow)
        pShowCalc = True
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then RsMRRDetail.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume						
    End Sub
    Private Sub SetTextLengths()

        On Error GoTo ERR1
        With RsMRRMain

            txtMRRNo.MaxLength = .Fields("AUTO_KEY_MRR").Precision
            txtMRRDate.MaxLength = 10
            TxtSupplier.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            txtBillNo.MaxLength = .Fields("BILL_NO").DefinedSize
            txtBillDate.MaxLength = 10
            txtST38No.MaxLength = .Fields("NO_ST38").DefinedSize
            txtEwayBillNo.MaxLength = .Fields("PARTY_EWAYBILLNO").DefinedSize
            TxtItemDesc.MaxLength = .Fields("ITEM_DETAILS").DefinedSize
            TxtTransporter.MaxLength = .Fields("TRANSPORT_MODE").DefinedSize
            txtFreight.MaxLength = .Fields("FREIGHT_CHARGES").DefinedSize
            TxtRemarks.MaxLength = .Fields("REMARKS").DefinedSize
            txtFormDetail.MaxLength = .Fields("FORM_DETAILS").DefinedSize

            txtDocsThru.MaxLength = .Fields("DOCS_THRU").DefinedSize
            txtVehicle.MaxLength = .Fields("VEHICLE").DefinedSize
            txtGRNo.MaxLength = .Fields("GRNO").DefinedSize
            txtGRDate.MaxLength = 10
            txtTripNo.MaxLength = .Fields("TRIP_NO").DefinedSize
            txtTripDate.MaxLength = 10

            txtShippedTo.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)

        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Show1()

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim mMode As String
        Dim mDivision As String
        Dim mShippedToCode As String
        Dim mShippedToName As String
        Dim mDeliveryToCode As String = ""
        Dim mDeliveryToName As String = ""

        pShowCalc = False
        With RsMRRMain
            If Not .EOF Then
                LblMkey.Text = .Fields("AUTO_KEY_MRR").Value

                If RsCompany.Fields("MRR_AGT_GE").Value = "Y" Then
                    FillGateEntryNo(CDbl(LblMkey.Text))
                End If
                txtMRRNo.Text = IIf(IsDBNull(.Fields("AUTO_KEY_MRR").Value), "", .Fields("AUTO_KEY_MRR").Value)
                txtMRRDate.Text = VB6.Format(IIf(IsDBNull(.Fields("MRR_DATE").Value), "", .Fields("MRR_DATE").Value), "DD/MM/YYYY")

                lblEntryDate.Text = VB6.Format(IIf(IsDBNull(.Fields("ADDDATE").Value), "", .Fields("ADDDATE").Value), "DD/MM/YYYY HH:MM")

                If MainClass.ValidateWithMasterTable((.Fields("SUPP_CUST_CODE").Value), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    TxtSupplier.Text = MasterNo
                End If
                mSupplierCode = .Fields("SUPP_CUST_CODE").Value

                mDivision = ""
                If MainClass.ValidateWithMasterTable((.Fields("DIV_CODE").Value), "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivision = MasterNo
                End If
                cboDivision.Text = mDivision


                txtBillNo.Text = IIf(IsDBNull(.Fields("BILL_NO").Value), "", .Fields("BILL_NO").Value)
                txtBillDate.Text = VB6.Format(IIf(IsDBNull(.Fields("BILL_DATE").Value), "", .Fields("BILL_DATE").Value), "DD/MM/YYYY")
                txtST38No.Text = IIf(IsDBNull(.Fields("NO_ST38").Value), "", .Fields("NO_ST38").Value)
                txtEwayBillNo.Text = IIf(IsDBNull(.Fields("PARTY_EWAYBILLNO").Value), "", .Fields("PARTY_EWAYBILLNO").Value)
                TxtItemDesc.Text = IIf(IsDBNull(.Fields("ITEM_DETAILS").Value), "", .Fields("ITEM_DETAILS").Value)
                TxtTransporter.Text = IIf(IsDBNull(.Fields("TRANSPORT_MODE").Value), "", .Fields("TRANSPORT_MODE").Value)
                txtFreight.Text = IIf(IsDBNull(.Fields("FREIGHT_CHARGES").Value), "", .Fields("FREIGHT_CHARGES").Value)
                TxtRemarks.Text = IIf(IsDBNull(.Fields("Remarks").Value), "", .Fields("Remarks").Value)
                txtFormDetail.Text = IIf(IsDBNull(.Fields("FORM_DETAILS").Value), "", .Fields("FORM_DETAILS").Value)

                If .Fields("REF_TYPE").Value = "P" Or .Fields("REF_TYPE").Value = "D" Then
                    cboRefType.SelectedIndex = 0
                ElseIf .Fields("REF_TYPE").Value = "D" Then
                    cboRefType.SelectedIndex = 0
                ElseIf .Fields("REF_TYPE").Value = "J" Then
                    cboRefType.SelectedIndex = 1
                ElseIf .Fields("REF_TYPE").Value = "I" Then
                    cboRefType.SelectedIndex = 2
                ElseIf .Fields("REF_TYPE").Value = "F" Then
                    cboRefType.SelectedIndex = 3
                ElseIf .Fields("REF_TYPE").Value = "R" Then
                    cboRefType.SelectedIndex = 4
                ElseIf .Fields("REF_TYPE").Value = "C" Then
                    cboRefType.SelectedIndex = 5
                ElseIf .Fields("REF_TYPE").Value = "1" Then
                    cboRefType.SelectedIndex = 6
                ElseIf .Fields("REF_TYPE").Value = "2" Then
                    cboRefType.SelectedIndex = 7
                ElseIf .Fields("REF_TYPE").Value = "3" Then
                    cboRefType.SelectedIndex = 8
                End If

                chkMrrSend.CheckState = IIf(.Fields("SEND_AC_FLAG").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                txtSendDate.Text = VB6.Format(IIf(IsDBNull(.Fields("SEND_AC_DATE").Value), "", .Fields("SEND_AC_DATE").Value), "DD/MM/YYYY")
                chkPacking.CheckState = IIf(.Fields("PACK_MAT_FLAG").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                If .Fields("MRR_FINAL_FLAG").Value = "Y" Then
                    chkFOC.CheckState = IIf(.Fields("REF_TYPE").Value = "F" Or .Fields("REF_TYPE").Value = "J" Or .Fields("REF_TYPE").Value = "1", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                Else
                    chkFOC.CheckState = System.Windows.Forms.CheckState.Unchecked
                End If

                chkScheRej.CheckState = IIf(.Fields("SCHLD_RTN_FLAG").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkDNote.CheckState = IIf(.Fields("DESCR_FLAG").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                chkQC.CheckState = IIf(.Fields("QC_STATUS").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                If lblBookType.Text = "G" Then
                    chkQC.Enabled = False
                Else
                    chkQC.Enabled = IIf(.Fields("QC_STATUS").Value = "N", True, False)
                End If


                OptFreight(0).Checked = IIf(.Fields("FREIGHT_TYPE").Value = 0, True, False)
                OptFreight(1).Checked = IIf(.Fields("FREIGHT_TYPE").Value = 1, True, False)

                mMode = IIf(IsDBNull(.Fields("MODE_TYPE").Value), "", .Fields("MODE_TYPE").Value)
                cboMode.SelectedIndex = Val(VB.Left(mMode, 1)) - 1

                txtDocsThru.Text = IIf(IsDBNull(.Fields("DOCS_THRU").Value), "", .Fields("DOCS_THRU").Value)
                txtVehicle.Text = IIf(IsDBNull(.Fields("VEHICLE").Value), "", .Fields("VEHICLE").Value)
                txtGRNo.Text = IIf(IsDBNull(.Fields("GRNO").Value), "", .Fields("GRNO").Value)
                txtGRDate.Text = IIf(IsDBNull(.Fields("GRDATE").Value), "", .Fields("GRDATE").Value)

                txtTripNo.Text = IIf(IsDBNull(.Fields("TRIP_NO").Value), "", .Fields("TRIP_NO").Value)
                txtTripDate.Text = IIf(IsDBNull(.Fields("TRIP_DATE").Value), "", .Fields("TRIP_DATE").Value)

                chkExciseStatus.CheckState = IIf(.Fields("EXCISE_STATUS").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkGSTStatus.CheckState = IIf(.Fields("GST_STATUS").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkServiceTaxClaim.CheckState = IIf(.Fields("SERV_STATUS").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkSTStatus.CheckState = IIf(.Fields("SALETAX_STATUS").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkBillPassing.CheckState = IIf(.Fields("MRR_FINAL_FLAG").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkCancelled.CheckState = IIf(.Fields("MRR_STATUS").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkPrimiumFreight.CheckState = IIf(.Fields("PREMIUM_FRIGHT").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                cmdResetMRR.Enabled = False
                If PubUserID = "G0416" Then
                    cmdResetMRR.Enabled = True
                Else
                    If PubSuperUser = "S" Or PubSuperUser = "A" Then
                        If chkExciseStatus.CheckState = System.Windows.Forms.CheckState.Unchecked And chkGSTStatus.CheckState = System.Windows.Forms.CheckState.Unchecked And chkSTStatus.CheckState = System.Windows.Forms.CheckState.Unchecked And chkBillPassing.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                            cmdResetMRR.Enabled = True
                        End If
                    End If
                End If

                lblAddUser.Text = IIf(IsDBNull(.Fields("ADDUSER").Value), "", .Fields("ADDUSER").Value)
                lblAddDate.Text = VB6.Format(IIf(IsDBNull(.Fields("ADDDATE").Value), "", .Fields("ADDDATE").Value), "DD/MM/YYYY")
                lblModUser.Text = IIf(IsDBNull(.Fields("MODUSER").Value), "", .Fields("MODUSER").Value)
                lblModDate.Text = VB6.Format(IIf(IsDBNull(.Fields("MODDATE").Value), "", .Fields("MODDATE").Value), "DD/MM/YYYY")


                chkShipTo.CheckState = IIf(.Fields("SHIPPED_TO_SAMEPARTY").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkUnderChallan.CheckState = IIf(.Fields("UNDER_CHALLAN").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                mShippedToCode = IIf(IsDBNull(.Fields("SHIPPED_TO_PARTY_CODE").Value), -1, .Fields("SHIPPED_TO_PARTY_CODE").Value)
                mShippedToName = ""
                If MainClass.ValidateWithMasterTable(mShippedToCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mShippedToName = MasterNo
                End If

                txtShippedTo.Text = mShippedToName

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

                FillCboPONo((LblMkey.Text), "N")
                CboPONo.Enabled = False
                cboRefType.Enabled = False
                cboDivision.Enabled = False

                If PubSuperUser = "S" Or PubSuperUser = "A" Then
                    If chkExciseStatus.CheckState = System.Windows.Forms.CheckState.Unchecked And chkGSTStatus.CheckState = System.Windows.Forms.CheckState.Unchecked And chkSTStatus.CheckState = System.Windows.Forms.CheckState.Unchecked And chkBillPassing.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                        cboDivision.Enabled = True
                    End If
                End If

                txtGateNo.Enabled = False
                cmdGateSearch.Enabled = False

                Call ShowDetail1((LblMkey.Text), .Fields("REF_TYPE").Value, Val(.Fields("DIV_CODE").Value))
                Call ShowExp1((LblMkey.Text))
                Call SprdExp_LeaveCell(SprdExp, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColExpAmt, 1, 1, 1, True))
                Call ShowBlobFile()
                '            Call CalcTots						
                TxtSupplier.Enabled = False
                cmdsearch.Enabled = False
                cmdDispcrepancy.Enabled = IIf(chkDNote.CheckState = System.Windows.Forms.CheckState.Checked, True, False)
            End If
        End With
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsMRRMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        SprdMain.Enabled = True
        SprdExp.Enabled = True
        txtMRRNo.Enabled = True
        cmdMRRSearch.Enabled = True
        pShowCalc = True
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
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

        SqlStr = " SELECT MKEY, TC_DOC_DESC, TC_DOC_EXT, TPR_DOC_DESC, TPR_DOC_EXT, TC_BLOB_DATA, TPR_BLOB_DATA " & vbCrLf & " FROM INV_GATEENTRY_TC_TRN " & vbCrLf & " WHERE MKEY = '" & txtGateNo.Text & "'"

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

                    '                StrTempPic = PubDomainUserDesktopPath & "\" & mFilename    ''"_TC." & RsTemp("TC_DOC_EXT").Value  ''VB6.Format(GetServerDate, "DDMMYYYY") & VB6.Format(GetServerTimeWithSecond, "HHMMSS") &     ''RsTemp("TC_DOC_DESC").Value						
                    StrTempPic = My.Application.Info.DirectoryPath & "\Temp\" & mFilename
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
        If StrTempPic <> "" Then
            chkTCAvailable.CheckState = System.Windows.Forms.CheckState.Checked
        End If

        ' Second File						


        lngImgSiz = 0
        lngOffset = 0
        StrTempPic = ""

        SqlStr = " SELECT MKEY, TC_DOC_DESC, TC_DOC_EXT, TPR_DOC_DESC, TPR_DOC_EXT, TC_BLOB_DATA, TPR_BLOB_DATA " & vbCrLf & " FROM INV_GATEENTRY_TC_TRN " & vbCrLf & " WHERE MKEY = '" & txtGateNo.Text & "'"

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
                    '                StrTempPic = PubDomainUserDesktopPath & "\" & VB6.Format(GetServerDate, "DDMMYYYY") & VB6.Format(GetServerTimeWithSecond, "HHMMSS") & mFilename       ''& "_TPR." & RsTemp("TPR_DOC_EXT").Value       ''RsTemp("TC_DOC_DESC").Value						
                    StrTempPic = My.Application.Info.DirectoryPath & "\Temp\" & mFilename
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
        If StrTempPic <> "" Then
            chkTPRAvailable.CheckState = System.Windows.Forms.CheckState.Checked
        End If


        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume						
    End Sub
    Private Sub FillGateEntryNo(ByVal pMRRNo As Double)

        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        SqlStr = "SELECT AUTO_KEY_GATE,GATE_DATE FROM INV_GATEENTRY_HDR WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND MRR_NO=" & pMRRNo & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            txtGateNo.Text = IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_GATE").Value), "", RsTemp.Fields("AUTO_KEY_GATE").Value)
            txtGateDate.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("GATE_DATE").Value), "", RsTemp.Fields("GATE_DATE").Value), "DD/MM/YYYY")
        End If
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume						
    End Sub
    Private Sub ShowFromGateEntry(ByVal mRsGate As ADODB.Recordset)
        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim mMode As String
        Dim mDivision As String
        Dim mShippedToCode As String
        Dim mShippedToName As String
        Dim mDeliveryToCode As String
        Dim mDeliveryToName As String

        pShowCalc = False
        With mRsGate
            If Not .EOF Then

                txtGateNo.Text = IIf(IsDBNull(.Fields("AUTO_KEY_GATE").Value), "", .Fields("AUTO_KEY_GATE").Value)
                txtGateDate.Text = VB6.Format(IIf(IsDBNull(.Fields("GATE_DATE").Value), "", .Fields("GATE_DATE").Value), "DD/MM/YYYY")

                If MainClass.ValidateWithMasterTable((.Fields("SUPP_CUST_CODE").Value), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    TxtSupplier.Text = MasterNo
                End If
                mSupplierCode = .Fields("SUPP_CUST_CODE").Value

                mDivision = ""
                If MainClass.ValidateWithMasterTable((.Fields("DIV_CODE").Value), "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivision = MasterNo
                End If
                cboDivision.Text = mDivision


                txtBillNo.Text = IIf(IsDBNull(.Fields("BILL_NO").Value), "", .Fields("BILL_NO").Value)
                txtBillDate.Text = VB6.Format(IIf(IsDBNull(.Fields("BILL_DATE").Value), "", .Fields("BILL_DATE").Value), "DD/MM/YYYY")
                txtST38No.Text = IIf(IsDBNull(.Fields("NO_ST38").Value), "", .Fields("NO_ST38").Value)
                txtEwayBillNo.Text = IIf(IsDBNull(.Fields("PARTY_EWAYBILLNO").Value), "", .Fields("PARTY_EWAYBILLNO").Value)
                TxtItemDesc.Text = IIf(IsDBNull(.Fields("ITEM_DETAILS").Value), "", .Fields("ITEM_DETAILS").Value)
                TxtTransporter.Text = IIf(IsDBNull(.Fields("TRANSPORT_MODE").Value), "", .Fields("TRANSPORT_MODE").Value)
                txtFreight.Text = IIf(IsDBNull(.Fields("FREIGHT_CHARGES").Value), "", .Fields("FREIGHT_CHARGES").Value)
                TxtRemarks.Text = IIf(IsDBNull(.Fields("Remarks").Value), "", .Fields("Remarks").Value)
                txtFormDetail.Text = IIf(IsDBNull(.Fields("FORM_DETAILS").Value), "", .Fields("FORM_DETAILS").Value)

                If .Fields("REF_TYPE").Value = "P" Or .Fields("REF_TYPE").Value = "D" Then
                    cboRefType.SelectedIndex = 0
                ElseIf .Fields("REF_TYPE").Value = "D" Then
                    cboRefType.SelectedIndex = 0
                ElseIf .Fields("REF_TYPE").Value = "J" Then
                    cboRefType.SelectedIndex = 1
                ElseIf .Fields("REF_TYPE").Value = "I" Then
                    cboRefType.SelectedIndex = 2
                ElseIf .Fields("REF_TYPE").Value = "F" Then
                    cboRefType.SelectedIndex = 3
                ElseIf .Fields("REF_TYPE").Value = "R" Then
                    cboRefType.SelectedIndex = 4
                ElseIf .Fields("REF_TYPE").Value = "C" Then
                    cboRefType.SelectedIndex = 5
                ElseIf .Fields("REF_TYPE").Value = "1" Then
                    cboRefType.SelectedIndex = 6
                ElseIf .Fields("REF_TYPE").Value = "2" Then
                    cboRefType.SelectedIndex = 7
                ElseIf .Fields("REF_TYPE").Value = "3" Then
                    cboRefType.SelectedIndex = 8
                End If

                OptFreight(0).Checked = IIf(.Fields("FREIGHT_TYPE").Value = 0, True, False)
                OptFreight(1).Checked = IIf(.Fields("FREIGHT_TYPE").Value = 1, True, False)

                mMode = IIf(IsDBNull(.Fields("MODE_TYPE").Value), "", .Fields("MODE_TYPE").Value)
                cboMode.SelectedIndex = Val(VB.Left(mMode, 1)) - 1

                txtDocsThru.Text = IIf(IsDBNull(.Fields("DOCS_THRU").Value), "", .Fields("DOCS_THRU").Value)
                txtVehicle.Text = IIf(IsDBNull(.Fields("VEHICLE").Value), "", .Fields("VEHICLE").Value)
                txtGRNo.Text = IIf(IsDBNull(.Fields("GRNO").Value), "", .Fields("GRNO").Value)
                txtGRDate.Text = IIf(IsDBNull(.Fields("GRDATE").Value), "", .Fields("GRDATE").Value)

                FillCboPONo((txtGateNo.Text), "Y")

                chkShipTo.CheckState = IIf(.Fields("SHIPPED_TO_SAMEPARTY").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkUnderChallan.CheckState = IIf(.Fields("UNDER_CHALLAN").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                mShippedToCode = IIf(IsDBNull(.Fields("SHIPPED_TO_PARTY_CODE").Value), -1, .Fields("SHIPPED_TO_PARTY_CODE").Value)
                mShippedToName = ""
                If MainClass.ValidateWithMasterTable(mShippedToCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mShippedToName = MasterNo
                End If

                txtShippedTo.Text = mShippedToName

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

                Call ShowDetailFromGateEntry((txtGateNo.Text), .Fields("REF_TYPE").Value, (.Fields("DIV_CODE").Value))
                Call ShowExpFromGate((txtGateNo.Text))
                Call SprdExp_LeaveCell(SprdExp, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColExpAmt, 1, 1, 1, True))
            End If
        End With
        pShowCalc = True
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume						
    End Sub


    Private Sub ShowResetGateEntry(ByVal mRsGate As ADODB.Recordset)

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim mMode As String
        Dim mDivision As String
        Dim mShippedToCode As String
        Dim mShippedToName As String

        With mRsGate
            If Not .EOF Then

                If MainClass.ValidateWithMasterTable((.Fields("SUPP_CUST_CODE").Value), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    TxtSupplier.Text = MasterNo
                End If
                mSupplierCode = .Fields("SUPP_CUST_CODE").Value

                mDivision = ""
                If MainClass.ValidateWithMasterTable((.Fields("DIV_CODE").Value), "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivision = MasterNo
                End If
                cboDivision.Text = mDivision


                txtBillNo.Text = IIf(IsDBNull(.Fields("BILL_NO").Value), "", .Fields("BILL_NO").Value)
                txtBillDate.Text = VB6.Format(IIf(IsDBNull(.Fields("BILL_DATE").Value), "", .Fields("BILL_DATE").Value), "DD/MM/YYYY")
                txtST38No.Text = IIf(IsDBNull(.Fields("NO_ST38").Value), "", .Fields("NO_ST38").Value)
                txtEwayBillNo.Text = IIf(IsDBNull(.Fields("PARTY_EWAYBILLNO").Value), "", .Fields("PARTY_EWAYBILLNO").Value)
                TxtItemDesc.Text = IIf(IsDBNull(.Fields("ITEM_DETAILS").Value), "", .Fields("ITEM_DETAILS").Value)
                TxtTransporter.Text = IIf(IsDBNull(.Fields("TRANSPORT_MODE").Value), "", .Fields("TRANSPORT_MODE").Value)
                txtFreight.Text = IIf(IsDBNull(.Fields("FREIGHT_CHARGES").Value), "", .Fields("FREIGHT_CHARGES").Value)
                TxtRemarks.Text = IIf(IsDBNull(.Fields("Remarks").Value), "", .Fields("Remarks").Value)
                txtFormDetail.Text = IIf(IsDBNull(.Fields("FORM_DETAILS").Value), "", .Fields("FORM_DETAILS").Value)

                If .Fields("REF_TYPE").Value = "P" Or .Fields("REF_TYPE").Value = "D" Then
                    cboRefType.SelectedIndex = 0
                ElseIf .Fields("REF_TYPE").Value = "D" Then
                    cboRefType.SelectedIndex = 0
                ElseIf .Fields("REF_TYPE").Value = "J" Then
                    cboRefType.SelectedIndex = 1
                ElseIf .Fields("REF_TYPE").Value = "I" Then
                    cboRefType.SelectedIndex = 2
                ElseIf .Fields("REF_TYPE").Value = "F" Then
                    cboRefType.SelectedIndex = 3
                ElseIf .Fields("REF_TYPE").Value = "R" Then
                    cboRefType.SelectedIndex = 4
                ElseIf .Fields("REF_TYPE").Value = "C" Then
                    cboRefType.SelectedIndex = 5
                ElseIf .Fields("REF_TYPE").Value = "1" Then
                    cboRefType.SelectedIndex = 6
                ElseIf .Fields("REF_TYPE").Value = "2" Then
                    cboRefType.SelectedIndex = 7
                ElseIf .Fields("REF_TYPE").Value = "3" Then
                    cboRefType.SelectedIndex = 8
                End If

                OptFreight(0).Checked = IIf(.Fields("FREIGHT_TYPE").Value = 0, True, False)
                OptFreight(1).Checked = IIf(.Fields("FREIGHT_TYPE").Value = 1, True, False)

                mMode = IIf(IsDBNull(.Fields("MODE_TYPE").Value), "", .Fields("MODE_TYPE").Value)
                cboMode.SelectedIndex = Val(VB.Left(mMode, 1)) - 1

                txtDocsThru.Text = IIf(IsDBNull(.Fields("DOCS_THRU").Value), "", .Fields("DOCS_THRU").Value)
                txtVehicle.Text = IIf(IsDBNull(.Fields("VEHICLE").Value), "", .Fields("VEHICLE").Value)
                txtGRNo.Text = IIf(IsDBNull(.Fields("GRNO").Value), "", .Fields("GRNO").Value)
                txtGRDate.Text = IIf(IsDBNull(.Fields("GRDATE").Value), "", .Fields("GRDATE").Value)
                chkQC.CheckState = System.Windows.Forms.CheckState.Unchecked
                FillCboPONo((txtGateNo.Text), "Y")

                chkShipTo.CheckState = IIf(.Fields("SHIPPED_TO_SAMEPARTY").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkUnderChallan.CheckState = IIf(.Fields("UNDER_CHALLAN").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                mShippedToCode = IIf(IsDBNull(.Fields("SHIPPED_TO_PARTY_CODE").Value), -1, .Fields("SHIPPED_TO_PARTY_CODE").Value)
                mShippedToName = ""
                If MainClass.ValidateWithMasterTable(mShippedToCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mShippedToName = MasterNo
                End If

                txtShippedTo.Text = mShippedToName

                txtBillTo.Text = IIf(IsDBNull(.Fields("BILL_TO_LOC_ID").Value), "", .Fields("BILL_TO_LOC_ID").Value)
                TxtShipTo.Text = IIf(IsDBNull(.Fields("SHIP_TO_LOC_ID").Value), "", .Fields("SHIP_TO_LOC_ID").Value)

                MainClass.ClearGrid(SprdMain)
                Call FormatSprdMain(-1)

                Call ShowDetailFromGateEntry((txtGateNo.Text), .Fields("REF_TYPE").Value, (.Fields("DIV_CODE").Value))
                Call ShowExpFromGate((txtGateNo.Text))
                Call SprdExp_LeaveCell(SprdExp, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColExpAmt, 1, 1, 1, True))
            End If
        End With
        pShowCalc = True
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume						
    End Sub
    Private Sub FillCboPONo(ByVal mMKEY As String, ByVal mFromGateEntry As String)

        On Error GoTo FillERR
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        SqlStr = ""

        If mFromGateEntry = "N" Then
            SqlStr = " SELECT Distinct GRD.REF_AUTO_KEY_NO " & vbCrLf & " FROM INV_GATE_HDR GRD" & vbCrLf & " WHERE AUTO_KEY_MRR='" & UCase(mMKEY) & "'"
        Else
            SqlStr = " SELECT Distinct GRD.REF_AUTO_KEY_NO " & vbCrLf & " FROM INV_GATEENTRY_HDR GRD" & vbCrLf & " WHERE AUTO_KEY_GATE='" & UCase(mMKEY) & "'"

        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        CboPONo.Items.Clear()
        If Not RsMisc.EOF Then
            Do While Not RsMisc.EOF
                CboPONo.Items.Add(IIf(IsDBNull(RsMisc.Fields("REF_AUTO_KEY_NO").Value), "", RsMisc.Fields("REF_AUTO_KEY_NO").Value))
                RsMisc.MoveNext()
            Loop
        End If
        Exit Sub
FillERR:
        MsgBox(Err.Description)
    End Sub
    Private Sub ShowExp1(ByVal mMKEY As String)

        On Error GoTo ERR1
        Dim I As Integer
        Dim pExpId As String
        Dim SqlStr As String = ""

        Call FillSprdExp()
        pShowCalc = False
        SqlStr = ""
        SqlStr = "Select INV_GATE_EXP.EXPCODE,INV_GATE_EXP.EXPPERCENT, " & vbCrLf & " INV_GATE_EXP.AMOUNT, " & vbCrLf & " FIN_INTERFACE_MST.Name as Name,FIN_INTERFACE_MST.Code, " & vbCrLf & " Identification,Add_Ded,Taxable,Exciseable,CalcOn,RO,DebitAmount " & vbCrLf & " From INV_GATE_EXP,FIN_INTERFACE_MST " & vbCrLf & " Where " & vbCrLf & " FIN_INTERFACE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND INV_GATE_EXP.ExpCode=FIN_INTERFACE_MST.Code " & vbCrLf & " AND INV_GATE_EXP.Mkey='" & mMKEY & "'"

        If CDate(txtMRRDate.Text) >= CDate(PubGSTApplicableDate) Then ' If PubGSTApplicable = True Then						
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='N'"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY SUBROWNO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMRRExp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMRRExp.EOF = False Then
            RsMRRExp.MoveFirst()
            With SprdExp
                Do While Not RsMRRExp.EOF
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
                        If .Text = RsMRRExp.Fields("Name").Value Then Exit For
                    Next I

                    .Col = ColExpPercent 'Exp. %						
                    .Text = CStr(Val(IIf(IsDBNull(RsMRRExp.Fields("ExpPercent").Value), "", RsMRRExp.Fields("ExpPercent").Value)))

                    .Col = ColExpAmt
                    If RsMRRExp.Fields("Identification").Value = "RO" Then '30.10.2001   ''Allow '-' if exp. is ropund off						
                        .Text = CStr(Val(IIf(IsDBNull(RsMRRExp.Fields("Amount").Value), "", RsMRRExp.Fields("Amount").Value)))
                    Else
                        .Text = CStr(System.Math.Abs(Val(IIf(IsDBNull(RsMRRExp.Fields("Amount").Value), "", RsMRRExp.Fields("Amount").Value))))
                    End If

                    .Col = ColExpSTCode
                    .Text = CStr(Val(IIf(IsDBNull(RsMRRExp.Fields("CODE").Value), 0, RsMRRExp.Fields("CODE").Value)))

                    .Col = ColExpAddDeduct 'ExpFlag						
                    .Text = IIf(RsMRRExp.Fields("Add_Ded").Value = "A", "A", "D")

                    .Col = ColExpIdent
                    .Text = IIf(IsDBNull(RsMRRExp.Fields("Identification").Value), "", RsMRRExp.Fields("Identification").Value)
                    If .Text = "RO" Then 'round off						
                        .Col = ColExpAmt
                        pRound = Val(.Text)
                    End If

                    SprdExp.Col = ColTaxable
                    SprdExp.Text = IIf(IsDBNull(RsMRRExp.Fields("Taxable").Value), "N", RsMRRExp.Fields("Taxable").Value)

                    SprdExp.Col = ColExciseable
                    SprdExp.Text = IIf(IsDBNull(RsMRRExp.Fields("Exciseable").Value), "N", RsMRRExp.Fields("Exciseable").Value)

                    SprdExp.Col = ColExpCalcOn
                    SprdExp.Text = CStr(Val(IIf(IsDBNull(RsMRRExp.Fields("CalcOn").Value), "", RsMRRExp.Fields("CalcOn").Value)))

                    .Col = ColRO
                    .Value = IIf(RsMRRExp.Fields("RO").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)


                    RsMRRExp.MoveNext()
                Loop
            End With
        End If
        pShowCalc = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)

    End Sub
    Private Sub ShowExpFromGate(ByVal mMKEY As String)

        On Error GoTo ERR1
        Dim I As Integer
        Dim pExpId As String
        Dim SqlStr As String = ""
        Dim RsGateExp As ADODB.Recordset = Nothing

        Call FillSprdExp()
        pShowCalc = False
        SqlStr = ""
        SqlStr = "Select INV_GATEENTRY_EXP.EXPCODE,INV_GATEENTRY_EXP.EXPPERCENT, " & vbCrLf & " INV_GATEENTRY_EXP.AMOUNT, " & vbCrLf & " FIN_INTERFACE_MST.Name as Name,FIN_INTERFACE_MST.Code, " & vbCrLf & " Identification,Add_Ded,Taxable,Exciseable,CalcOn,RO,DebitAmount " & vbCrLf & " From INV_GATEENTRY_EXP,FIN_INTERFACE_MST " & vbCrLf & " Where " & vbCrLf & " FIN_INTERFACE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND INV_GATEENTRY_EXP.ExpCode=FIN_INTERFACE_MST.Code " & vbCrLf & " AND INV_GATEENTRY_EXP.AUTO_KEY_GATE='" & mMKEY & "'"

        If CDate(txtMRRDate.Text) >= CDate(PubGSTApplicableDate) Then 'If PubGSTApplicable = True Then						
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='N'"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGateExp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsGateExp.EOF = False Then
            RsGateExp.MoveFirst()
            With SprdExp
                Do While Not RsGateExp.EOF
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
                        If .Text = RsGateExp.Fields("Name").Value Then Exit For
                    Next I

                    .Col = ColExpPercent 'Exp. %						
                    .Text = CStr(Val(IIf(IsDBNull(RsGateExp.Fields("ExpPercent").Value), "", RsGateExp.Fields("ExpPercent").Value)))

                    .Col = ColExpAmt
                    If RsGateExp.Fields("Identification").Value = "RO" Then '30.10.2001   ''Allow '-' if exp. is ropund off						
                        .Text = CStr(Val(IIf(IsDBNull(RsGateExp.Fields("Amount").Value), "", RsGateExp.Fields("Amount").Value)))
                    Else
                        .Text = CStr(System.Math.Abs(Val(IIf(IsDBNull(RsGateExp.Fields("Amount").Value), "", RsGateExp.Fields("Amount").Value))))
                    End If

                    .Col = ColExpSTCode
                    .Text = CStr(Val(IIf(IsDBNull(RsGateExp.Fields("CODE").Value), 0, RsGateExp.Fields("CODE").Value)))

                    .Col = ColExpAddDeduct 'ExpFlag						
                    .Text = IIf(RsGateExp.Fields("Add_Ded").Value = "A", "A", "D")

                    .Col = ColExpIdent
                    .Text = IIf(IsDBNull(RsGateExp.Fields("Identification").Value), "", RsGateExp.Fields("Identification").Value)
                    If .Text = "RO" Then 'round off						
                        .Col = ColExpAmt
                        pRound = Val(.Text)
                    End If

                    SprdExp.Col = ColTaxable
                    SprdExp.Text = IIf(IsDBNull(RsGateExp.Fields("Taxable").Value), "N", RsGateExp.Fields("Taxable").Value)

                    SprdExp.Col = ColExciseable
                    SprdExp.Text = IIf(IsDBNull(RsGateExp.Fields("Exciseable").Value), "N", RsGateExp.Fields("Exciseable").Value)

                    SprdExp.Col = ColExpCalcOn
                    SprdExp.Text = CStr(Val(IIf(IsDBNull(RsGateExp.Fields("CalcOn").Value), "", RsGateExp.Fields("CalcOn").Value)))

                    .Col = ColRO
                    .Value = IIf(RsGateExp.Fields("RO").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)


                    RsGateExp.MoveNext()
                Loop
            End With
        End If
        pShowCalc = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)

    End Sub

    Private Sub ShowDetail1(ByVal mMKEY As String, ByVal pRefType As String, ByVal mDivisionCode As Double)

        On Error GoTo ERR1
        Dim I As Integer
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mShortageQty As Double
        Dim mItemCode As String
        Dim mItemName As String
        Dim mItemDesc As String
        Dim mPartNo As String
        Dim SqlStr As String = ""
        Dim mPOQty As Double
        Dim mRecdQty As Double
        Dim mBalQty As Double
        Dim mRefPoNo As Double
        Dim mRefInvoiceNo As String
        Dim mRGPItemCode As String
        Dim mOpenOrder As Boolean
        Dim pSupplierCode As String = ""
        Dim mHSNCode As String

        pSupplierCode = ""

        'If chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked Then
        If Trim(TxtSupplier.Text) <> "" Then
            If MainClass.ValidateWithMasterTable((TxtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                pSupplierCode = MasterNo
            End If
        End If
        'Else
        '    If Trim(txtShippedTo.Text) <> "" Then
        '        If MainClass.ValidateWithMasterTable((txtShippedTo.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '            pSupplierCode = MasterNo
        '        End If
        '    End If
        'End If

        SqlStr = ""
        'SqlStr = " SELECT * " & vbCrLf & " FROM INV_GATE_DET " & vbCrLf & " Where AUTO_KEY_MRR=" & Val(mMKEY) & "" & vbCrLf & " Order By SERIAL_NO"

        SqlStr = " SELECT ID.*, "

        If pRefType = "I" Or pRefType = "2" Or pRefType = "3" Then
            SqlStr = SqlStr & " GetSALEITEMPRICE(REF_AUTO_KEY_NO,REF_AUTO_KEY_NO, '" & pSupplierCode & "',ITEM_CODE) AS PORATE "
        ElseIf pRefType = "P" Then
            SqlStr = SqlStr & " GetITEMPRICE_NEW(1,1,TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),REF_AUTO_KEY_NO, ITEM_CODE) AS PORATE "
        Else
            SqlStr = SqlStr & " GetITEMJWRate_New(" & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & ",ID.MRR_DATE,REF_AUTO_KEY_NO, AUTO_KEY_MRR,ITEM_CODE,RGP_ITEM_CODE,SERIAL_NO) AS PORATE "
        End If

        SqlStr = SqlStr & vbCrLf _
            & " FROM INV_GATE_DET ID" & vbCrLf _
            & " Where AUTO_KEY_MRR=" & Val(mMKEY) & "" & vbCrLf _
            & " Order By SERIAL_NO"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMRRDetail, ADODB.LockTypeEnum.adLockReadOnly)

        With RsMRRDetail
            If .EOF = True Then Exit Sub
            I = 1
            .MoveFirst()

            Do While Not .EOF

                SprdMain.Row = I

                SprdMain.Col = ColPONo
                If pRefType = "D" Then
                    mRefPoNo = GetScheduleNo(pSupplierCode, .Fields("REF_AUTO_KEY_NO").Value)
                    mRefInvoiceNo = CStr(mRefPoNo)
                Else
                    mRefPoNo = Val(IIf(IsDBNull(.Fields("REF_AUTO_KEY_NO").Value), -1, .Fields("REF_AUTO_KEY_NO").Value))
                    mRefInvoiceNo = IIf(IsDBNull(.Fields("REF_AUTO_KEY_NO").Value), -1, .Fields("REF_AUTO_KEY_NO").Value)
                End If

                '            mRefPoNo = Val(IIf(IsNull(!REF_PO_NO), -1, !REF_PO_NO))						

                SprdMain.Text = mRefInvoiceNo 'mRefPoNo						

                SprdMain.Col = ColPODate
                SprdMain.Text = IIf(IsDBNull(.Fields("REF_DATE").Value), "", .Fields("REF_DATE").Value)

                SprdMain.Col = ColRGPItemCode
                mRGPItemCode = Trim(IIf(IsDBNull(.Fields("RGP_ITEM_CODE").Value), "", .Fields("RGP_ITEM_CODE").Value))
                SprdMain.Text = Trim(mRGPItemCode)

                SprdMain.Col = ColItemCode
                mItemCode = Trim(IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))
                SprdMain.Text = Trim(mItemCode)

                SprdMain.Col = ColItemName
                MainClass.ValidateWithMasterTable(Trim(mItemCode), "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mItemName = MasterNo
                SprdMain.Text = mItemName



                SprdMain.Col = ColHSNCode
                mHSNCode = ""
                If VB.Left(cboRefType.Text, 1) = "P" Then
                    mHSNCode = ""
                    mHSNCode = GetHSNFromPurchaseOrder(mItemCode, mRefPoNo)
                    SprdMain.Col = ColHSNCode
                    SprdMain.Text = mHSNCode
                End If
                If mHSNCode = "" Then
                    MainClass.ValidateWithMasterTable(Trim(mItemCode), "Item_Code", "HSN_CODE", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                    SprdMain.Text = MasterNo
                End If

                'SprdMain.Col = ColHSNCode
                'MainClass.ValidateWithMasterTable(Trim(mItemCode), "Item_Code", "HSN_CODE", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                'SprdMain.Text = MasterNo

                SprdMain.Col = ColHeatNo
                SprdMain.Text = IIf(IsDBNull(.Fields("HEAT_NO").Value), "", .Fields("HEAT_NO").Value)

                SprdMain.Col = ColBatchNo
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("BATCH_NO").Value), "", .Fields("BATCH_NO").Value))

                'SprdMain.Col = ColLotNo
                'SprdMain.Text = Trim(IIf(IsDBNull(.Fields("LOT_NO").Value), "", .Fields("LOT_NO").Value))

                SprdMain.Col = ColUnit
                SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)

                If .Fields("REF_TYPE").Value = "R" Or .Fields("REF_TYPE").Value = "I" Or .Fields("REF_TYPE").Value = "1" Or .Fields("REF_TYPE").Value = "2" Or .Fields("REF_TYPE").Value = "3" Then
                    Call CollectPOData(.Fields("REF_TYPE").Value, mRefInvoiceNo, Trim(mItemCode), Trim(mRGPItemCode), I, mDivisionCode)
                    ''mukul
                    mItemDesc = ""
                    SprdMain.Col = ColItemDesc
                    If mItemCode <> "" And .Fields("REF_TYPE").Value = "R" Then
                        mItemDesc = GetItemDescription(mRefPoNo, mRGPItemCode)
                    End If
                    SprdMain.Text = mItemDesc
                Else
                    mPOQty = CalcPOQty(pSupplierCode, mRefPoNo, Trim(.Fields("ITEM_CODE").Value), .Fields("REF_TYPE").Value, mOpenOrder, mDivisionCode)

                    SprdMain.Row = I
                    SprdMain.Col = ColPOQty
                    SprdMain.Text = VB6.Format(mPOQty, "0.00")

                    mRecdQty = CalcRecvQty(CStr(Val(CStr(mRefPoNo))), .Fields("ITEM_CODE").Value, pSupplierCode, mOpenOrder)
                    mBalQty = mPOQty - mRecdQty ''+ Val(IIf(IsNull(!RECEIVED_QTY), 0, !RECEIVED_QTY))						

                    SprdMain.Row = I
                    SprdMain.Col = ColBalQty
                    SprdMain.Text = VB6.Format(mBalQty, "0.0000")
                End If

                SprdMain.Row = I
                SprdMain.Col = ColBillQty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("BILL_QTY").Value), 0, .Fields("BILL_QTY").Value)))

                SprdMain.Col = ColReceivedQty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("RECEIVED_QTY").Value), 0, .Fields("RECEIVED_QTY").Value)))

                If Val(IIf(IsDBNull(.Fields("PORATE").Value), 0, .Fields("PORATE").Value)) > 0 Then
                    SprdMain.Col = ColPORate
                    SprdMain.Text = Val(IIf(IsDBNull(.Fields("PORATE").Value), 0, .Fields("PORATE").Value))
                End If

                SprdMain.Col = ColRate
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_RATE").Value), 0, .Fields("ITEM_RATE").Value)))

                SprdMain.Col = ColItemCost
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_COST").Value), 0, .Fields("ITEM_COST").Value)))

                SprdMain.Col = ColStockType
                SprdMain.Text = IIf(IsDBNull(.Fields("STOCK_TYPE").Value), "", .Fields("STOCK_TYPE").Value)

                SprdMain.Col = ColAcceptQty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("LOT_ACCEPT").Value), 0, .Fields("LOT_ACCEPT").Value)))

                SprdMain.Col = ColApprovedQty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("APPROVED_QTY").Value), 0, .Fields("APPROVED_QTY").Value)))

                SprdMain.Col = ColShortQty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("SHORTAGE_QTY").Value), 0, .Fields("SHORTAGE_QTY").Value)))

                SprdMain.Col = ColRejQty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("REJECTED_QTY").Value), 0, .Fields("REJECTED_QTY").Value)))

                SprdMain.Col = ColDevQty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("LOT_ACCEPT_DEV").Value), 0, .Fields("LOT_ACCEPT_DEV").Value)))

                SprdMain.Col = ColQtyInKgs
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_QTY_IN_KGS").Value), 0, .Fields("ITEM_QTY_IN_KGS").Value)))

                SprdMain.Col = ColRecdQtyInKgs
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_RECDQTY_IN_KGS").Value), 0, .Fields("ITEM_RECDQTY_IN_KGS").Value)))

                SprdMain.Col = ColSeg
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("LOT_ACC_SEG").Value), 0, .Fields("LOT_ACC_SEG").Value)))

                SprdMain.Col = ColRework
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("LOT_ACC_RWK").Value), 0, .Fields("LOT_ACC_RWK").Value)))

                SprdMain.Col = ColConvQty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("CONV_QTY").Value), 0, .Fields("CONV_QTY").Value)))

                SprdMain.Col = ColQCEMP
                SprdMain.Text = IIf(IsDBNull(.Fields("QC_EMP_CODE").Value), "", .Fields("QC_EMP_CODE").Value)

                SprdMain.Col = ColCT3No
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("CT3_NO").Value), "", .Fields("CT3_NO").Value)))

                SprdMain.Col = ColPCNo
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("PC_NO").Value), "", .Fields("PC_NO").Value)))

                SprdMain.Col = ColRemarks
                SprdMain.Text = IIf(IsDBNull(.Fields("Remarks").Value), "", .Fields("Remarks").Value)

                SprdMain.Col = ColQCDate
                SprdMain.Text = IIf(IsDBNull(.Fields("MRR_QCDATE").Value), "", .Fields("MRR_QCDATE").Value)

                SprdMain.Col = ColPDIRFlag
                SprdMain.Value = IIf(.Fields("PDIR_FLAG").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                SprdMain.Col = ColSchdRtnFlag
                SprdMain.Value = IIf(.Fields("SCHLD_RTN_FLAG").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

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

    Private Sub ShowDetailFromGateEntry(ByVal mMKEY As String, ByVal pRefType As String, ByVal mDivisionCode As Double)

        On Error GoTo ERR1
        Dim I As Integer
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mShortageQty As Double
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mPartNo As String
        Dim SqlStr As String = ""
        Dim mPOQty As Double
        Dim mRecdQty As Double
        Dim mBalQty As Double
        Dim mRefPoNo As String
        Dim mRGPItemCode As String
        Dim mOpenOrder As Boolean
        Dim RsGateDetail As ADODB.Recordset = Nothing
        Dim mQCEmpCode As String
        Dim pSupplierCode As String = ""
        Dim mHSNCode As String


        pSupplierCode = ""

        'If chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked Then
        If Trim(TxtSupplier.Text) <> "" Then
            If MainClass.ValidateWithMasterTable((TxtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                pSupplierCode = MasterNo
            End If
        End If
        'Else
        '    If Trim(txtShippedTo.Text) <> "" Then
        '        If MainClass.ValidateWithMasterTable((txtShippedTo.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '            pSupplierCode = MasterNo
        '        End If
        '    End If
        'End If

        SqlStr = ""
        'SqlStr = " SELECT * " & vbCrLf & " FROM INV_GATEENTRY_DET " & vbCrLf & " Where AUTO_KEY_GATE=" & Val(mMKEY) & "" & vbCrLf & " Order By SERIAL_NO"

        SqlStr = " SELECT ID.*, "

        If pRefType = "I" Or pRefType = "2" Or pRefType = "3" Then
            SqlStr = SqlStr & " GetSALEITEMPRICE(REF_AUTO_KEY_NO,REF_AUTO_KEY_NO, '" & pSupplierCode & "',ITEM_CODE) AS PORATE "
        ElseIf pRefType = "P" Then
            SqlStr = SqlStr & " GetITEMPRICE_NEW(1,1,TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),REF_AUTO_KEY_NO, ITEM_CODE) AS PORATE "
        Else
            SqlStr = SqlStr & " GetITEMJWRate_New(" & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & ",ID.GATE_DATE,REF_AUTO_KEY_NO, AUTO_KEY_GATE,ITEM_CODE,RGP_ITEM_CODE,SERIAL_NO) AS PORATE "
        End If


        ''
        SqlStr = SqlStr & vbCrLf _
            & " FROM INV_GATEENTRY_DET ID" & vbCrLf _
            & " Where AUTO_KEY_GATE=" & Val(mMKEY) & "" & vbCrLf _
            & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGateDetail, ADODB.LockTypeEnum.adLockReadOnly)

        With RsGateDetail
            If .EOF = True Then Exit Sub
            I = 1
            .MoveFirst()

            Do While Not .EOF

                SprdMain.Row = I

                SprdMain.Col = ColPONo
                If pRefType = "D" Then
                    mRefPoNo = CStr(GetScheduleNo(pSupplierCode, .Fields("REF_AUTO_KEY_NO").Value))
                Else
                    mRefPoNo = IIf(IsDBNull(.Fields("REF_AUTO_KEY_NO").Value), -1, .Fields("REF_AUTO_KEY_NO").Value)
                End If

                '            mRefPoNo = Val(IIf(IsNull(!REF_PO_NO), -1, !REF_PO_NO))						

                SprdMain.Text = mRefPoNo

                SprdMain.Col = ColPODate
                SprdMain.Text = IIf(IsDBNull(.Fields("REF_DATE").Value), "", .Fields("REF_DATE").Value)

                SprdMain.Col = ColRGPItemCode
                mRGPItemCode = Trim(IIf(IsDBNull(.Fields("RGP_ITEM_CODE").Value), "", .Fields("RGP_ITEM_CODE").Value))
                SprdMain.Text = Trim(mRGPItemCode)

                SprdMain.Col = ColItemCode
                mItemCode = Trim(IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))
                SprdMain.Text = Trim(mItemCode)

                SprdMain.Col = ColItemName
                MainClass.ValidateWithMasterTable(Trim(mItemCode), "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mItemDesc = MasterNo
                SprdMain.Text = mItemDesc

                SprdMain.Col = ColHSNCode
                mHSNCode = ""
                If VB.Left(cboRefType.Text, 1) = "P" Then
                    mHSNCode = ""
                    mHSNCode = GetHSNFromPurchaseOrder(mItemCode, mRefPoNo)
                    SprdMain.Col = ColHSNCode
                    SprdMain.Text = mHSNCode
                End If
                If mHSNCode = "" Then
                    If MainClass.ValidateWithMasterTable(Trim(mItemCode), "Item_Code", "HSN_CODE", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        SprdMain.Text = MasterNo
                    Else
                        SprdMain.Text = ""
                    End If

                End If
                'MainClass.ValidateWithMasterTable(Trim(mItemCode), "Item_Code", "HSN_Code", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                'SprdMain.Text = MasterNo

                SprdMain.Col = ColUnit
                SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)

                SprdMain.Col = ColBatchNo
                SprdMain.Text = IIf(IsDBNull(.Fields("BATCH_NO").Value), "", .Fields("BATCH_NO").Value)

                SprdMain.Col = ColHeatNo
                SprdMain.Text = IIf(IsDBNull(.Fields("HEAT_NO").Value), "", .Fields("HEAT_NO").Value)

                If .Fields("REF_TYPE").Value = "R" Or .Fields("REF_TYPE").Value = "I" Or .Fields("REF_TYPE").Value = "1" Or .Fields("REF_TYPE").Value = "2" Or .Fields("REF_TYPE").Value = "3" Then
                    Call CollectPOData(.Fields("REF_TYPE").Value, mRefPoNo, Trim(mItemCode), Trim(mRGPItemCode), I, mDivisionCode)
                Else
                    mPOQty = CalcPOQty(pSupplierCode, Val(mRefPoNo), Trim(.Fields("ITEM_CODE").Value), .Fields("REF_TYPE").Value, mOpenOrder, mDivisionCode)

                    SprdMain.Row = I
                    SprdMain.Col = ColPOQty
                    SprdMain.Text = VB6.Format(mPOQty, "0.00")

                    mRecdQty = CalcRecvQty(CStr(Val(mRefPoNo)), .Fields("ITEM_CODE").Value, mSupplierCode, mOpenOrder)
                    mBalQty = mPOQty - mRecdQty ''+ Val(IIf(IsNull(!RECEIVED_QTY), 0, !RECEIVED_QTY))						

                    SprdMain.Row = I
                    SprdMain.Col = ColBalQty
                    SprdMain.Text = VB6.Format(mBalQty, "0.0000")
                End If

                SprdMain.Row = I
                SprdMain.Col = ColBillQty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("BILL_QTY").Value), 0, .Fields("BILL_QTY").Value)))

                ''mukul billed qty = Received Qty
                'SprdMain.Col = ColReceivedQty
                'SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("BILL_QTY").Value), 0, .Fields("BILL_QTY").Value)))

                SprdMain.Col = ColPORate
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("PORATE").Value), 0, .Fields("PORATE").Value))) ''Val(IIf(IsNull(!PORATE), 0, !PORATE))

                SprdMain.Col = ColRate
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_RATE").Value), 0, .Fields("ITEM_RATE").Value)))

                SprdMain.Col = ColItemCost
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_COST").Value), 0, .Fields("ITEM_COST").Value)))

                SprdMain.Col = ColStockType
                If GetAutoQC(mItemCode) = True Then
                    SprdMain.Text = "ST"
                Else
                    SprdMain.Text = "QC"
                End If

                SprdMain.Col = ColRemarks
                SprdMain.Text = IIf(IsDBNull(.Fields("Remarks").Value), "", .Fields("Remarks").Value)

                SprdMain.Col = ColQtyInKgs
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_QTY_IN_KGS").Value), 0, .Fields("ITEM_QTY_IN_KGS").Value)))

                mQCEmpCode = GetQCEmpCode(mItemCode)
                If mQCEmpCode <> "" Then
                    SprdMain.Row = I
                    SprdMain.Col = ColQCEMP
                    SprdMain.Text = mQCEmpCode
                End If

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
            AssignGrid(True)
            '        AdataItem.Refresh						
            'FormatSprdView()
            'SprdView.Focus()
            FraFront.SendToBack()
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraFront.BringToFront()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsMRRMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub CalcTots()
        On Error GoTo ERR1

        Dim CntRow As Integer

        Dim mQty As Double
        Dim mRate As Double
        Dim mDiscount As Double
        Dim mItemValue As Double

        Dim mTaxableExpValue As Double
        Dim mTotalValue As Double

        Dim mSalesTax As String
        Dim mSTPERCENT As Double
        Dim mST As Double
        Dim mTotalST As Double

        Dim mAmount As Double

        Dim mTotAmt As Double
        Dim mTotQty As Double
        Dim mTotExp As Double
        Dim mNetAmt As Double

        With SprdMain
            For CntRow = 1 To .MaxRows
                .Row = CntRow

                '            .Col = ColPONo						
                '            If .Text = "" Then GoTo DontCalc						

                .Col = ColItemCode
                If .Text = "" Then GoTo DontCalc

                .Col = ColReceivedQty
                mQty = Val(.Text)

                .Col = ColRate
                mRate = Val(.Text)

                mItemValue = (mQty * mRate)

                .Col = ColAmount
                .Text = CStr(mItemValue)

                mTotAmt = mTotAmt + CDbl(VB6.Format(mItemValue, "0.00"))
                mTotQty = mTotQty + mQty
DontCalc:
            Next CntRow
        End With



        '    mTotExp = lblTotExpValue.text						
        '    mNetAmt = mTotAmt + mTotExp						
        lblTotItemValue.Text = VB6.Format(mTotAmt, "0.00")
        lblTotQty.Text = VB6.Format(mTotQty, "0.00")
        Call CalcExpTots(mTotAmt)
        Exit Sub
ERR1:
        'Resume						
        If Err.Number = 6 Then Resume Next 'OverFlow						
        MsgInformation(Err.Description)
    End Sub

    Private Sub CalcLandedCost()
        Dim ii As Integer

        Dim mItemAmount As Double
        Dim mItemValue As Double
        Dim mExpAmount As Double
        Dim mItemCost As Double
        Dim mQty As Double
        Dim mRate As Double

        On Error GoTo ERR1
        mItemAmount = CalcItemAmount()
        mExpAmount = Val(lblTotExpAmt.Text)

        With SprdMain
            For ii = 1 To .MaxRows - 1
                .Row = ii
                .Col = ColAmount
                mItemValue = Val(.Text)

                If mItemAmount = 0 Then
                    mItemCost = 0
                Else
                    mItemCost = mExpAmount * mItemValue / mItemAmount
                End If

                .Col = ColBillQty
                mQty = Val(.Text)

                .Col = ColRate
                mRate = Val(.Text)

                .Col = ColItemCost
                If mQty > 0 Then
                    .Text = CStr(mRate + (mItemCost / mQty))
                Else
                    .Text = CStr(0)
                End If

            Next ii
        End With
        Exit Sub
ERR1:
        ''Resume						
        MsgInformation(Err.Description)
    End Sub
    Function CalcItemAmount() As Double
        Dim ii As Integer

        On Error GoTo ERR1
        CalcItemAmount = 0
        With SprdMain
            For ii = 1 To .MaxRows - 1
                .Row = ii
                .Col = ColAmount
                CalcItemAmount = CalcItemAmount + Val(.Text)

                ''            .Col = ColSTAmt						
                ''            CalcItemAmount = CalcItemAmount + Val(.Text)						
            Next ii
        End With

        Exit Function
ERR1:
        'Resume						
        MsgInformation(Err.Description)
    End Function
    Private Sub CalcApprovedQty(ByVal pRow As Object)
        On Error GoTo ERR1
        Dim mBillQty As Double
        Dim mReceivedQty As Double
        Dim mAcceptQty As Double
        Dim mApprovedQty As Double
        Dim mShortQty As Double
        Dim mRejQty As Double
        Dim mDevQty As Double
        Dim mSeg As Double
        Dim mRework As Double
        Dim mConvQty As Double

        With SprdMain

            .Row = pRow

            .Col = ColBillQty
            mBillQty = Val(.Text)

            .Col = ColReceivedQty
            mReceivedQty = Val(.Text)

            .Col = ColAcceptQty
            mAcceptQty = Val(.Text)

            .Col = ColDevQty
            mDevQty = Val(.Text)

            .Col = ColSeg
            mSeg = Val(.Text)

            .Col = ColRework
            mRework = Val(.Text)

            .Col = ColConvQty
            mConvQty = Val(.Text)

            mApprovedQty = mAcceptQty - mDevQty - mSeg - mRework - mConvQty

            .Col = ColApprovedQty
            .Text = CStr(mApprovedQty)

            .Col = ColShortQty
            '            If mBillQty < mReceivedQty Then						
            .Text = CStr(mBillQty - mReceivedQty)
            '            Else						
            '                .Text = 0						
            '            End If						

            .Col = ColRejQty
            .Text = CStr(mReceivedQty - mAcceptQty)

        End With


        Exit Sub
ERR1:
        'Resume						
        If Err.Number = 6 Then Resume Next 'OverFlow						
        MsgInformation(Err.Description)
    End Sub
    Private Sub CalcExpTots(ByVal mTotAmt As Double)
        On Error GoTo ERR1

        Dim mNetAccessAmt As Double
        Dim mExciseableAmount As Double
        Dim mTaxableAmount As Double
        Dim mModvatableAmount As Double
        Dim mTotModvatableAmount As Double
        Dim mTotServiceableAmount As Double
        Dim mTotSTRefundableAmt As Double
        'Dim mShortage As Double						
        Dim mCEDCessAble As Double
        Dim mADDCessAble As Double
        Dim mCESSableAmount As Double
        Dim mTotItemAmount As Double
        Dim pTotExciseDuty As Double
        Dim pTotEduCess As Double
        Dim pTotSHECess As Double
        Dim pTotADE As Double
        Dim pTotExportExp As Double
        Dim pTotOthers As Double
        Dim pTotSalesTax As Double
        Dim pTotSurcharge As Double
        Dim pTotCustomDuty As Double
        Dim pTotAddCess As Double
        Dim pTotCustomDutyExport As Double
        Dim pTotCustomDutyCess As Double
        Dim pTotMSC As Double
        Dim pTotDiscount As Double
        Dim pTotServiceTax As Double
        Dim pTotRO As Double
        Dim pTotTCS As Double
        Dim mTotExp As Double
        Dim pEDPer As Double
        Dim pSTPer As Double
        Dim pServPer As Double
        Dim pCessPer As Double
        Dim pSHECPer As Double
        Dim pTCSPer As Double
        Dim pTotKKCAmount As Double

        Dim mTotIGST As Double
        Dim mTotSGST As Double
        Dim mTotCGST As Double
        Dim mItemValue As String

        Dim mHSNCode As String
        Dim pCGSTPer As Double
        Dim pSGSTPer As Double
        Dim pIGSTPer As Double
        Dim mPartyGSTNo As String
        Dim mCompanyGSTNo As String
        Dim mSupplierCode As String

        Dim mLocal As String

        '

        If MainClass.ValidateWithMasterTable((TxtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
            mSupplierCode = MasterNo
        Else
            Exit Sub
        End If

        mLocal = GetPartyBusinessDetail(Trim(mSupplierCode), Trim(txtBillTo.Text), "WITHIN_STATE")
        mPartyGSTNo = GetPartyBusinessDetail(Trim(mSupplierCode), Trim(txtBillTo.Text), "GST_RGN_NO")

        mCompanyGSTNo = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value)


        mNetAccessAmt = Val(CStr(mTotAmt))
        mExciseableAmount = Val(lblTotItemValue.Text)
        mTaxableAmount = Val(lblTotItemValue.Text)

        'Call BillExpensesCalcTots(SprdExp, (txtMRRDate.Text), False, mNetAccessAmt, mExciseableAmount, mTaxableAmount, mCEDCessAble, mADDCessAble, mCESSableAmount, mTotItemAmount, pTotExciseDuty, pTotEduCess, pTotSHECess, pTotADE, pTotExportExp, pTotOthers, pTotSalesTax, pTotSurcharge, pTotCustomDuty, pTotAddCess, pTotCustomDutyExport, pTotCustomDutyCess, pTotMSC, pTotDiscount, pTotServiceTax, pTotRO, pTotTCS, mTotExp, pEDPer, pSTPer, pServPer, pCessPer, pSHECPer, pTCSPer, "MRR", mNetAccessAmt, pTotKKCAmount)

        Dim mExpName As String
        Dim mExpAddDeduct As String
        Dim mIsTaxable As String
        Dim mOtherTaxableAmount As Double
        Dim mTotTaxableItemAmount As Double
        Dim j As Long

        With SprdExp
            For I = 1 To SprdExp.MaxRows
                .Row = I
                .Col = ColExpName
                mExpName = Trim(.Text)

                .Col = ColExpAddDeduct
                mExpAddDeduct = Trim(.Text)

                If MainClass.ValidateWithMasterTable(mExpName, "NAME", "TAXABLE", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GST_ENABLED='Y' AND TAXABLE='Y'") = True Then
                    mIsTaxable = MasterNo
                Else
                    mIsTaxable = "N"
                End If

                If mIsTaxable = "Y" Then
                    .Col = ColExpAmt
                    mOtherTaxableAmount = mOtherTaxableAmount + (CDbl(VB6.Format(.Text, "0.00")) * IIf(mExpAddDeduct = "D", -1, 1))
                End If
            Next
        End With


        With SprdMain
            j = .MaxRows
            For I = 1 To j
                .Row = I
                .Col = 0
                If .Text = "Del" Then GoTo DontCalc1

                .Col = ColItemCode
                If .Text = "" Then GoTo DontCalc1

                .Col = ColAmount
                mItemValue = Val(.Text)

                mTotItemAmount = mTotItemAmount + CDbl(VB6.Format(mItemValue, "0.00"))
DontCalc1:
            Next I
        End With

        mTotTaxableItemAmount = mTotItemAmount + mOtherTaxableAmount

        With SprdMain
            For CntRow = 1 To .MaxRows
                .Row = CntRow

                '            .Col = ColPONo						
                '            If .Text = "" Then GoTo DontCalc						

                .Col = ColItemCode
                If .Text = "" Then GoTo DontCalc

                .Col = ColHSNCode
                mHSNCode = Trim(.Text)

                .Col = ColAmount
                mItemValue = Val(.Text)


                If mTotItemAmount = 0 Then
                    mTaxableAmount = 0
                Else
                    mTaxableAmount = mItemValue + CDbl(VB6.Format(mOtherTaxableAmount * mItemValue / mTotItemAmount, "0.00"))
                End If


                If GetHSNDetails(mHSNCode, pCGSTPer, pSGSTPer, pIGSTPer, mLocal, "0", mPartyGSTNo) = False Then GoTo ERR1

                mTotCGST = mTotCGST + (mTaxableAmount * pCGSTPer / 100)
                mTotSGST = mTotSGST + (mTaxableAmount * pSGSTPer / 100)
                mTotIGST = mTotIGST + (mTaxableAmount * pIGSTPer / 100)

DontCalc:
            Next CntRow
        End With



        Call BillExpensesCalcTots_GST(SprdExp, txtMRRDate.Text, mNetAccessAmt, mTotItemAmount, mTaxableAmount,
                                0, 0, 0, mTotIGST, mTotSGST, mTotCGST, pTotExportExp, 0, 0, pTotOthers,
                                pTotCustomDutyExport, pTotCustomDuty, pTotMSC, pTotDiscount,
                                0, pTotRO, pTotTCS, mTotExp, pTCSPer, "S")

        'lblTotItemValue.Text = VB6.Format(mTotAmt, "#0.00")
        'lblTotIGST.Text = VB6.Format(mTotIGST, "#0.00")
        'lblTotSGST.Text = VB6.Format(mTotSGST, "#0.00")
        'lblTotCGST.Text = VB6.Format(mTotCGST, "#0.00")
        'lblEDUAmount.Text = VB6.Format(pTotEduCess, "#0.00")
        'lblNetAmount.Text = VB6.Format(mTotExp + mTotAmt, "#0.00")
        'lblTotFreight.Text = VB6.Format(pTotOthers, "#0.00")
        'lblTotCharges.Text = CStr(0) ''VB6.Format(mRO, "#0.00")
        'lblTotExpAmt.Text = VB6.Format(mTotExp, "#0.00")
        'lblTotTaxableAmt.Text = VB6.Format(mTaxableAmount, "#0.00")


        lblTotItemValue.Text = VB6.Format(mTotAmt, "#0.00")
        lblSGST.Text = VB6.Format(mTotSGST, "#0.00")
        lblCGST.Text = VB6.Format(mTotCGST, "#0.00")
        lblIGST.Text = VB6.Format(mTotIGST, "#0.00")
        lblNetAmount.Text = VB6.Format(mTotExp + mTotAmt + mTotSGST + mTotCGST + mTotIGST, "#0.00")
        lblTotFreight.Text = VB6.Format(pTotOthers, "#0.00")
        lblTotCharges.Text = CStr(0) ''VB6.Format(mRO, "#0.00")						
        lblTotExpAmt.Text = VB6.Format(mTotExp, "#0.00")
        lblTotTaxableAmt.Text = VB6.Format(mTaxableAmount, "#0.00")

        lblRO.Text = VB6.Format(pTotRO, "#0.00")
        lblDiscount.Text = VB6.Format(pTotDiscount, "#0.00")
        lblSurcharge.Text = VB6.Format(pTotSurcharge, "#0.00")
        lblMSC.Text = VB6.Format(pTotMSC, "#0.00")
        '    lblTotQty.text = VB6.Format(mTotQty, "#0.00")						

        Call CalcLandedCost()

        Exit Sub
ERR1:
        ''Resume						
        If Err.Number = 6 Then Resume Next 'OverFlow						
        MsgInformation(Err.Description)

    End Sub
    Private Function CalcDSQty(ByVal pSupplierCode As String, ByVal pPONO As Double, ByVal pItemCode As String) As Double

        On Error GoTo ErrPart
        Dim mSchdDate As String
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mLastDate As String
        Dim mFieldName As String
        Dim mCategoryType As String

        mSchdDate = "01/" & VB6.Format(txtMRRDate.Text, "MM") & "/" & VB6.Format(txtMRRDate.Text, "YYYY")
        mLastDate = MainClass.LastDay(Month(CDate(txtMRRDate.Text)), Year(CDate(txtMRRDate.Text))) & "/" & VB6.Format(txtMRRDate.Text, "MM") & "/" & VB6.Format(txtMRRDate.Text, "YYYY")

        '    mSchdDate = GetFirstDayInWeek(txtMRRDate.Text)						
        '    mLastDate = GetLastDayInWeek(txtMRRDate.Text)						

        If RsCompany.Fields("WEEKLY_SCHD").Value = "N" Then
            mFieldName = "TOTAL_QTY"
        Else
            mCategoryType = GetProductionType(pItemCode)
            If mCategoryType = "G" Or mCategoryType = "C" Or mCategoryType = "T" Or mCategoryType = "A" Then
                If VB.Day(CDate(txtMRRDate.Text)) < 8 Then
                    mFieldName = "WEEK1_QTY"
                ElseIf VB.Day(CDate(txtMRRDate.Text)) < 15 Then
                    mFieldName = "WEEK1_QTY+WEEK2_QTY"
                ElseIf VB.Day(CDate(txtMRRDate.Text)) < 22 Then
                    mFieldName = "WEEK1_QTY+WEEK2_QTY+WEEK3_QTY"
                ElseIf VB.Day(CDate(txtMRRDate.Text)) < 29 Then
                    mFieldName = "WEEK1_QTY+WEEK2_QTY+WEEK3_QTY+WEEK4_QTY"
                Else
                    mFieldName = "WEEK1_QTY+WEEK2_QTY+WEEK3_QTY+WEEK4_QTY+WEEK5_QTY"
                End If
            Else
                If VB.Day(CDate(txtMRRDate.Text)) < 8 Then
                    mFieldName = "WEEK1_QTY"
                ElseIf VB.Day(CDate(txtMRRDate.Text)) < 15 Then
                    mFieldName = "WEEK2_QTY"
                ElseIf VB.Day(CDate(txtMRRDate.Text)) < 22 Then
                    mFieldName = "WEEK3_QTY"
                ElseIf VB.Day(CDate(txtMRRDate.Text)) < 29 Then
                    mFieldName = "WEEK4_QTY"
                Else
                    mFieldName = "WEEK5_QTY"
                End If

            End If
        End If

        SqlStr = "SELECT SCHLD_DATE," & mFieldName & " AS TOTAL_QTY " & vbCrLf & " FROM PUR_DELV_SCHLD_HDR DSMain, PUR_DELV_SCHLD_DET DSDetail" & vbCrLf & " WHERE DSMain.COMPANY_CODE=Dsdetail.COMPANY_CODE AND DSMain.AUTO_KEY_DELV=Dsdetail.AUTO_KEY_DELV" & vbCrLf & " AND DSMain.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & pSupplierCode & "'" & vbCrLf & " AND ITEM_CODE='" & pItemCode & "' AND POST_FLAG='Y'" & vbCrLf & " AND " & vbCrLf & " SCHLD_DATE >= TO_DATE('" & VB6.Format(mSchdDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND SCHLD_DATE <= TO_DATE('" & VB6.Format(mLastDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        If CDate(txtMRRDate.Text) >= CDate(PubGSTApplicableDate) And CDate(txtBillDate.Text) < CDate(PubGSTApplicableDate) Then

        Else
            SqlStr = SqlStr & vbCrLf & " AND AUTO_KEY_PO=" & Val(CStr(pPONO)) & ""
        End If


        ''AND SCHLD_STATUS='N'						
        ''SCHLD_STATUS='N' means Open....						
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            CalcDSQty = IIf(IsDBNull(RsTemp.Fields("TOTAL_QTY").Value), 0, RsTemp.Fields("TOTAL_QTY").Value)
        End If
        Exit Function
ErrPart:
        CalcDSQty = 0
    End Function

    Private Function GetScheduleNo(ByVal pSupplierCode As String, ByVal pDSNo As Double) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = "SELECT AUTO_KEY_PO " & vbCrLf & " FROM PUR_DELV_SCHLD_HDR DSMain" & vbCrLf & " WHERE DSMain.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & pSupplierCode & "'" & vbCrLf & " AND AUTO_KEY_DELV=" & Val(CStr(pDSNo)) & ""

        ''SCHLD_STATUS='N' means Open....						
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetScheduleNo = IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_PO").Value), 0, RsTemp.Fields("AUTO_KEY_PO").Value)
        End If
        Exit Function
ErrPart:
        GetScheduleNo = -1
    End Function
    Private Function GetPOFromDs(ByVal xDSNo As String) As String

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = "SELECT AUTO_KEY_PO FROM PUR_DELV_SCHLD_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_DELV=" & Val(xDSNo) & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


        If RsTemp.EOF = False Then
            GetPOFromDs = IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_PO").Value), "", RsTemp.Fields("AUTO_KEY_PO").Value)
        Else
            GetPOFromDs = ""
        End If
        Exit Function
ErrPart:
        GetPOFromDs = ""
    End Function

    Private Sub txtTransporter_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtTransporter.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtTransporter_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtTransporter.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtTransporter.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub SearchItemDesc()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((TxtItemDesc.Text), "FIN_ITEMTYPE_MST", "NAME", , , , SqlStr) = True Then
            TxtItemDesc.Text = AcName
            If TxtItemDesc.Enabled = True Then TxtItemDesc.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Function CheckStockType() As String
        On Error GoTo ErrPart
        Dim CntRow As Integer

        With SprdMain
            For CntRow = 1 To .MaxRows - 1
                .Row = CntRow
                .Col = ColStockType

                If Trim(.Text) = "QC" Then
                    CheckStockType = "N"
                    Exit Function
                End If
            Next
            CheckStockType = "Y"
            pQCDate = VB6.Format(RunDate, "DD/MM/YYYY")
        End With
        Exit Function
ErrPart:
        CheckStockType = "N"
    End Function

    Private Sub GetF4detailFromRGP(ByVal mPONo As Double, ByVal mCheckF4 As Boolean, ByRef mOutwardF4No As Double, ByRef mOutwardF4Date As String, ByVal mExpDate As String)

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing

        mCheckF4 = False
        mOutwardF4No = CDbl("0")
        mOutwardF4Date = ""

        mSqlStr = " SELECT OUTWARD_57F4NO,GATEPASS_DATE,EXP_RTN_DATE " & vbCrLf _
                & " FROM INV_GATEPASS_HDR " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND AUTO_KEY_PASSNO=" & mPONo & ""

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mOutwardF4No = IIf(IsDBNull(RsTemp.Fields("OUTWARD_57F4NO").Value), "0", RsTemp.Fields("OUTWARD_57F4NO").Value)
            mOutwardF4Date = VB6.Format(IIf(IsDBNull(RsTemp.Fields("GATEPASS_DATE").Value), "", RsTemp.Fields("GATEPASS_DATE").Value), "DD/MM/YYYY")
            mExpDate = VB6.Format(IIf(IsDBNull(RsTemp.Fields("EXP_RTN_DATE").Value), "", RsTemp.Fields("EXP_RTN_DATE").Value), "DD/MM/YYYY")
            If Val(CStr(mOutwardF4No)) = 0 Then
                mCheckF4 = False
            Else
                mCheckF4 = True
            End If
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function SelectQuery(ByVal xRefType As String, ByVal xRefNo As String, ByVal xIsItemCode As Boolean, ByVal mDivisionCode As Double, Optional ByVal pRGPItemCode As String = "") As String
        On Error GoTo ErrPart
        Dim mSuppCode As String
        Dim SelectQuery1 As String

        If xIsItemCode = True Then
            SelectQuery = "SELECT DISTINCT INVMST.ITEM_CODE, INVMST.ITEM_SHORT_DESC "
        Else
            SelectQuery = "SELECT DISTINCT INVMST.ITEM_SHORT_DESC, INVMST.ITEM_CODE "
        End If

        Select Case xRefType
            Case "P"
                SelectQuery = SelectQuery & vbCrLf _
                    & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID, INV_ITEM_MST INVMST" & vbCrLf _
                    & " WHERE IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND IH.MKEY=ID.MKEY" & vbCrLf _
                    & " AND ID.Company_Code=INVMST.Company_Code" & vbCrLf _
                    & " AND ID.ITEM_Code=INVMST.ITEM_Code" & vbCrLf _
                    & " AND IH.PO_STATUS='Y' AND IH.DIV_CODE=" & mDivisionCode & " AND PO_ITEM_STATUS='N' " & vbCrLf _
                    & " AND IH.AUTO_KEY_PO=" & Val(xRefNo) & ""

            Case "R"

                SelectQuery = SelectQuery & vbCrLf _
                    & " FROM INV_GATEPASS_HDR IH, INV_GATEPASS_DET ID, INV_ITEM_MST INVMST" & vbCrLf _
                    & " WHERE IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND IH.AUTO_KEY_PASSNO = ID.AUTO_KEY_PASSNO " & vbCrLf _
                    & " AND INVMST.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND IH.Company_Code=INVMST.Company_Code" & vbCrLf _
                    & " AND ID.ITEM_Code=INVMST.ITEM_Code" & vbCrLf _
                    & " AND ID.ITEM_CODE='" & pRGPItemCode & "' AND IH.DIV_CODE=" & mDivisionCode & "" & vbCrLf _
                    & " AND IH.GATEPASS_TYPE ='R'  " & vbCrLf & " AND IH.AUTO_KEY_PASSNO=" & Val(xRefNo) & ""

                If xIsItemCode = True Then
                    SelectQuery1 = "SELECT DISTINCT INVMST.ITEM_CODE, INVMST.ITEM_SHORT_DESC "
                Else
                    SelectQuery1 = "SELECT DISTINCT INVMST.ITEM_SHORT_DESC, INVMST.ITEM_CODE "
                End If

                SelectQuery = SelectQuery & vbCrLf _
                    & " UNION " & SelectQuery1 & vbCrLf _
                    & " FROM INV_GATEPASS_HDR IH, INV_GATEPASS_DET ID, INV_ITEM_MST INVMST" & vbCrLf _
                    & " WHERE IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND IH.AUTO_KEY_PASSNO = ID.AUTO_KEY_PASSNO " & vbCrLf _
                    & " AND INVMST.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND IH.Company_Code=INVMST.Company_Code" & vbCrLf _
                    & " AND ID.INWARD_ITEM_CODE=INVMST.ITEM_Code" & vbCrLf _
                    & " AND ID.ITEM_CODE='" & pRGPItemCode & "' AND IH.DIV_CODE=" & mDivisionCode & "" & vbCrLf _
                    & " AND IH.GATEPASS_TYPE ='R'  " & vbCrLf _
                    & " AND IH.AUTO_KEY_PASSNO=" & Val(xRefNo) & ""


                '& " FROM  " & vbCrLf & " PRD_OUTBOM_HDR IH,PRD_OUTBOM_DET ID, " & vbCrLf & " INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY = ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE = INVMST.COMPANY_CODE " & vbCrLf & " AND ID.PRODUCT_CODE = INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ID.ITEM_CODE='" & pRGPItemCode & "'"
                '						
                '             SelectQuery = SelectQuery & vbCrLf _						
                ''                    & " AND IH.WEF = (" & vbCrLf _						
                ''                    & " SELECT MAX(WEF) " & vbCrLf _						
                ''                    & " FROM PRD_OUTBOM_HDR " & vbCrLf _						
                ''                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _						
                ''                    & " AND PRODUCT_CODE='" & xItemCode & "'" & vbCrLf _						
                ''                    & " AND WEF<=TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"						


                'SelectQuery = SelectQuery & vbCrLf & " UNION " & SelectQuery1 & vbCrLf & " FROM  " & vbCrLf & " PRD_OUTBOM_HDR IH,PRD_OUTBOM_ALTER_DET ID, " & vbCrLf & " INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY = ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE = INVMST.COMPANY_CODE " & vbCrLf & " AND ID.PRODUCT_CODE = INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ID.ALTER_ITEM_CODE='" & pRGPItemCode & "'"

                'AND INVMST.COMPANY_CODE = " & RsCompany.fields("COMPANY_CODE").value & "						

            Case "I", "1", "2", "3"
                SelectQuery = SelectQuery & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND INVMST.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.Company_Code=INVMST.Company_Code" & vbCrLf & " AND ID.ITEM_Code=INVMST.ITEM_Code" & vbCrLf & " AND IH.AUTO_KEY_INVOICE='" & xRefNo & "'"
                '        Case "J"						
                '						
                '            If MainClass.ValidateWithMasterTable(TxtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then						
                '                mSuppCode = MasterNo						
                '            Else						
                '                mSuppCode = "-1"						
                '            End If						
                '						
                '            SelectQuery = SelectQuery & vbCrLf _						
                ''                            & " FROM FIN_SUPP_CUST_DET ID,INV_ITEM_MST INVMST " & vbCrLf _						
                ''                            & " WHERE " & vbCrLf _						
                ''                            & " ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _						
                ''                            & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf _						
                ''                            & " AND ID.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _						
                ''                            & " AND SUPP_CUST_CODE='" & mSuppCode & "' AND TRN_TYPE='J'"						


        End Select

        SelectQuery = SelectQuery & vbCrLf & " ORDER BY 1 "

        Exit Function
ErrPart:
        SelectQuery = ""
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function DuplicateBillNo(ByVal pSuppCode As String) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mMRRNO As Double

        If Trim(txtMRRNo.Text) = "" Then
            mMRRNO = -1
        Else
            mMRRNO = Val(txtMRRNo.Text)
        End If

        DuplicateBillNo = False
        SqlStr = "SELECT BILL_NO " & vbCrLf & " FROM INV_GATE_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUBSTR(AUTO_KEY_MRR,LENGTH(AUTO_KEY_MRR)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND TRIM(SUPP_CUST_CODE)='" & pSuppCode & "'  AND BILL_NO='" & Trim(txtBillNo.Text) & "'" & vbCrLf & " AND AUTO_KEY_MRR<>" & mMRRNO & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            DuplicateBillNo = True
        End If

        Exit Function
ErrPart:
        DuplicateBillNo = False
    End Function

    Private Function UpdateIssueNoteMain(ByVal pIssueNoteNoStr As String, ByVal mRefType As String, ByVal pAutoIssueCheck As Boolean, ByVal pcntRow As Integer, Optional ByVal mIsSubStore As String = "") As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mIssueSeq As Double
        Dim mStatus As String
        Dim mEntryDate As String
        Dim CntRow As Integer
        Dim mItemCode As String
        Dim mStockType As String = ""
        Dim mQCEmpCode As String
        Dim mPurchaseQty As Double
        Dim mIssueQty As Double
        Dim mPurchaseUOM As String = ""
        Dim mUOM As String = ""
        Dim mDeptCode As String = ""
        Dim mFactor As Double
        Dim mLotNoRequied As String
        Dim mDeptDesc As String = ""
        Dim mCostC As String
        Dim mPONo As String
        Dim mProd_Type As String
        Dim mIssueFor As String
        Dim mDivisionCode As Double
        Dim mHeatNo As String

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        If mRefType = "J" Or mRefType = "1" Then
            If pAutoIssueCheck = True Then
                UpdateIssueNoteMain = True
                Exit Function
            End If
        End If

        With SprdMain
            For CntRow = pcntRow To pcntRow ''.MaxRows - 1						

                .Row = CntRow

                .Col = ColPONo
                mPONo = Trim(.Text)

                .Col = ColItemCode
                mItemCode = Trim(.Text)

                mProd_Type = GetProductionType(mItemCode)

                .Col = ColHeatNo
                mHeatNo = Trim(UCase(.Text))

                .Col = ColStockType
                mStockType = Trim(.Text)


                If mRefType = "J" Or mRefType = "1" Then
                    If mStockType <> "CS" Then GoTo NextRecd
                ElseIf mRefType = "R" Then
                    If (mStockType <> "ST" And mStockType <> "CS") Then GoTo NextRecd
                Else
                    mDeptCode = ""
                    If mStockType <> "ST" Then GoTo NextRecd
                    If mIsSubStore = "Y" Then
                        If GetAutoIssueFromIndent(mPONo, mItemCode, "AUTO_SS_ISSUE", mDeptCode) = "N" Then GoTo NextRecd
                    Else
                        If GetAutoIssueFromIndent(mPONo, mItemCode, "AUTO_ISSUE", mDeptCode) = "N" Then GoTo NextRecd
                        If pAutoIssueCheck = True Then
                            If IsProductionItem(mItemCode) = True Then GoTo NextRecd
                        End If
                    End If
                End If

                .Col = ColQCEMP
                mQCEmpCode = Trim(.Text)

                .Col = ColUnit
                mPurchaseUOM = Trim(.Text)

                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mUOM = MasterNo
                End If

                .Col = ColApprovedQty
                mPurchaseQty = Val(.Text)

                If mPurchaseUOM = mUOM Then
                    mIssueQty = mPurchaseQty
                Else
                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "UOM_FACTOR", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mFactor = MasterNo
                    End If
                    mIssueQty = mPurchaseQty * mFactor
                End If

                If mRefType = "J" Or mRefType = "1" Or mRefType = "R" Then
                    mDeptCode = GetDeptFromBOM(mItemCode)
                Else
                    '                mDeptCode = GetDeptFromIndent(mItemCode)						
                End If

                If mItemCode <> "" And mDeptCode <> "" And mQCEmpCode <> "" And mIssueQty <> 0 Then
                    If mDeptCode = "" Then
                        If mRefType = "J" Or mRefType = "1" Or mRefType = "R" Then
                            If MsgQuestion("B.O.M. Not defined for Item Code (" & mItemCode & "). Do you still want to continue except this item code. ? ") = CStr(MsgBoxResult.No) Then
                                UpdateIssueNoteMain = False
                                Exit Function
                            End If
                        End If
                    End If

                    mIssueSeq = AutoGenIssueSeqNo()

                    If pIssueNoteNoStr = "" Then
                        pIssueNoteNoStr = Str(mIssueSeq)
                    Else
                        pIssueNoteNoStr = pIssueNoteNoStr & "," & Str(mIssueSeq)
                    End If

                    mEntryDate = VB6.Format(PubCurrDate, "DD-MMM-YYYY") & " " & GetServerTime()

                    If MainClass.ValidateWithMasterTable(mDeptCode, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mDeptDesc = MasterNo
                    End If

                    mCostC = GetCostC(mDeptCode)

                    SqlStr = ""
                    If mIsSubStore = "Y" Then
                        mIssueFor = "S"
                    Else
                        If mProd_Type = "P" Or mProd_Type = "J" Or mProd_Type = "1" Or mProd_Type = "C" Or mProd_Type = "R" Or mProd_Type = "3" Then
                            mIssueFor = "P"
                        Else
                            mIssueFor = "G"
                        End If
                    End If

                    SqlStr = "INSERT INTO INV_ISSUE_HDR (" & vbCrLf & " AUTO_KEY_ISS, " & vbCrLf & " COMPANY_CODE, " & vbCrLf & " ISSUE_DATE, " & vbCrLf & " DEPT_CODE, " & vbCrLf & " EMP_CODE, REMARKS, COST_CENTER_CODE, DAILY_PLAN_NO, " & vbCrLf & " SHIFT_CODE,ISSUE_STATUS, ISSUE_FOR, " & vbCrLf & " ADDUSER,ADDDATE,MODUSER,MODDATE,DIV_CODE)" & vbCrLf & " VALUES( "

                    SqlStr = SqlStr & vbCrLf & " " & Val(CStr(mIssueSeq)) & "," & vbCrLf & " " & RsCompany.Fields("Company_Code").Value & "," & vbCrLf & " TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')," & vbCrLf & " '" & MainClass.AllowSingleQuote(mDeptCode) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mQCEmpCode) & "', " & vbCrLf & " '', " & vbCrLf & " '" & mCostC & "', " & vbCrLf & " 0, " & vbCrLf & " 'A', " & vbCrLf & " 'Y', '" & mIssueFor & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " TO_DATE('" & mEntryDate & "','DD-MON-YYYY HH24:MI'),'',''," & mDivisionCode & ")"

                    PubDBCn.Execute(SqlStr)


                    SqlStr = " INSERT INTO INV_ISSUE_DET ( " & vbCrLf & " AUTO_KEY_ISS,SERIAL_NO,ITEM_CODE,ITEM_UOM,REMARKS," & vbCrLf & " FROM_STOCK_TYPE,DEMAND_QTY,ISSUE_QTY, COMPANY_CODE) "

                    SqlStr = SqlStr & vbCrLf & " VALUES (" & Val(CStr(mIssueSeq)) & ",1," & vbCrLf & " '" & MainClass.AllowSingleQuote(mItemCode) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mUOM) & "', " & vbCrLf & " '', '" & MainClass.AllowSingleQuote(mStockType) & "', " & vbCrLf & " " & mIssueQty & "," & mIssueQty & ", " & RsCompany.Fields("COMPANY_CODE").Value & ") "
                    PubDBCn.Execute(SqlStr)

                    If UpdateStockTRN(PubDBCn, ConStockRefType_ISS, Str(mIssueSeq), 1, VB6.Format(PubCurrDate, "DD/MM/YYYY"), VB6.Format(PubCurrDate, "DD/MM/YYYY"), mStockType, mItemCode, mUOM, CStr(-1), mIssueQty, 0, "O", 0, 0, "", "", "STR", mDeptCode, "", "N", "To : " & mDeptDesc, "-1", ConWH, mDivisionCode, VB.Left(cboRefType.Text, 1), "",, mHeatNo) = False Then GoTo ErrPart

                    If mProd_Type = "P" Or mProd_Type = "J" Or mProd_Type = "1" Or mProd_Type = "C" Or mProd_Type = "R" Or mProd_Type = "B" Or mProd_Type = "I" Or mProd_Type = "3" Then
                        If UpdateStockTRN(PubDBCn, ConStockRefType_ISS, Str(mIssueSeq), 1, VB6.Format(PubCurrDate, "DD/MM/YYYY"), VB6.Format(PubCurrDate, "DD/MM/YYYY"), mStockType, mItemCode, mUOM, CStr(-1), mIssueQty, 0, "I", 0, 0, "", "", mDeptCode, mDeptCode, "", "N", "From : STORE TO :" & mDeptDesc, "-1", ConPH, mDivisionCode, VB.Left(cboRefType.Text, 1), "",, mHeatNo) = False Then GoTo ErrPart
                    End If

                    If mIsSubStore = "Y" Then
                        If UpdateStockTRN(PubDBCn, ConStockRefType_ISS, Str(mIssueSeq), 1, VB6.Format(PubCurrDate, "DD/MM/YYYY"), VB6.Format(PubCurrDate, "DD/MM/YYYY"), mStockType, mItemCode, mUOM, CStr(-1), mIssueQty, 0, "I", 0, 0, "", "", mDeptCode, "STR", "", "N", "From : STORE TO : " & mDeptCode, "-1", ConSH, mDivisionCode, VB.Left(cboRefType.Text, 1), "",, mHeatNo) = False Then GoTo ErrPart
                    End If

                    mLotNoRequied = "N"
                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mLotNoRequied = MasterNo
                    End If

                    If mLotNoRequied = "Y" Then
                        If UpdateLotInPaintStock(1, Str(mIssueSeq), VB6.Format(PubCurrDate, "DD/MM/YYYY"), mItemCode, mUOM, mIssueQty, mDeptDesc) = False Then GoTo ErrPart
                    End If
                End If
NextRecd:
            Next
        End With

        UpdateIssueNoteMain = True
        Exit Function
ErrPart:
        UpdateIssueNoteMain = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function GetDeptFromBOM(ByVal pItemCode As String) As String

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = ""
        '						
        '    SqlStr = "SELECT DEPT_CODE " & vbCrLf _						
        ''            & " FROM PRD_NEWBOM_DET " & vbCrLf _						
        ''            & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _						
        ''            & " AND RM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"        '' & vbCrLf _						
        ''            & " AND  SERIAL_NO=1"						

        SqlStr = " SELECT ID.DEPT_CODE FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID" & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND RM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf & " AND IH.STATUS='O' "

        '    If Trim(txtWEF.Text) <> "" Then						
        '        SqlStr = SqlStr & vbCrLf & " AND WEF='" & VB6.Format((txtWEF.Text), "DD-MMM-YYYY") & "'"						
        '    Else						
        '        SqlStr = SqlStr & vbCrLf _						
        ''            & " AND WEF = (" & vbCrLf _						
        ''            & " SELECT MAX(WEF) AS WEF " & vbCrLf _						
        ''            & " FROM PRD_NEWBOM_HDR " & vbCrLf _						
        ''            & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _						
        ''            & " AND PRODUCT_CODE='" & Trim(txtProductCode.Text) & "'" & vbCrLf _						
        ''            & " AND BOM_TYPE='" & lblType.text & "')"						
        '    End If						

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetDeptFromBOM = IIf(IsDBNull(RsTemp.Fields("DEPT_CODE").Value), "", RsTemp.Fields("DEPT_CODE").Value)
        Else
            GetDeptFromBOM = ""
        End If

        Exit Function
ErrPart:
        GetDeptFromBOM = ""
    End Function
    Private Function AutoGenIssueSeqNo() As Double

        On Error GoTo AutoGenIssueSeqNoErr
        Dim RsMainGen As ADODB.Recordset = Nothing
        Dim mNewSeqNo As Integer
        Dim SqlStr As String = ""
        Dim mMaxValue As String

        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_ISS)  " & vbCrLf & " FROM INV_ISSUE_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_ISS,LENGTH(AUTO_KEY_ISS)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsMainGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mMaxValue = .Fields(0).Value
                    mNewSeqNo = CInt(Mid(mMaxValue, 1, Len(mMaxValue) - 6))
                    mNewSeqNo = mNewSeqNo + 1
                Else
                    mNewSeqNo = 1
                End If
            End If
        End With
        AutoGenIssueSeqNo = CDbl(mNewSeqNo & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"))
        Exit Function
AutoGenIssueSeqNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function



    Public Function CheckRefDate(ByVal mDivisionCode As Double) As Boolean
        On Error GoTo ErrPart
        Dim mSupplierCode As String
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim CntRow As Integer
        Dim mRefNo As String

        If Trim(TxtSupplier.Text) = "" Then
            MsgInformation("Please Select Supplier Name First.")
            CheckRefDate = False
            Exit Function
        End If

        'If chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked Then
        If MainClass.ValidateWithMasterTable((TxtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invalid Supplier Name.")
            CheckRefDate = False
            Exit Function
        Else
            mSupplierCode = MasterNo
        End If
        'Else
        '    If MainClass.ValidateWithMasterTable((txtShippedTo.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
        '        MsgInformation("Invalid Supplier Name.")
        '        CheckRefDate = False
        '        Exit Function
        '    Else
        '        mSupplierCode = MasterNo
        '    End If

        'End If

        With SprdMain
            For CntRow = 1 To .MaxRows
                .Row = CntRow
                .Col = ColPONo
                mRefNo = Trim(SprdMain.Text)

                If mRefNo <> "" Then
                    If VB.Left(cboRefType.Text, 1) = "P" Then
                        If MainClass.ValidateWithMasterTable(mRefNo, "AUTO_KEY_PO", "PUR_ORD_DATE", "PUR_PURCHASE_HDR", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & mSupplierCode & "' AND PUR_TYPE IN ('P','R','L') AND DIV_CODE=" & mDivisionCode & " AND AMEND_WEF_DATE<=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')") = False Then
                            MsgInformation("Invalid Bill Date / Ref No for Such Supplier")
                            CheckRefDate = False
                            Exit Function
                        End If

                    ElseIf VB.Left(cboRefType.Text, 1) = "R" Then
                        If MainClass.ValidateWithMasterTable(mRefNo, "AUTO_KEY_PASSNO", "AUTO_KEY_PASSNO", "INV_GATEPASS_HDR", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & mSupplierCode & "' AND DIV_CODE=" & mDivisionCode & " AND GATEPASS_DATE<=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')") = False Then
                            MsgInformation("Invalid Bill Date / Ref No for Such Supplier")
                            CheckRefDate = False
                            Exit Function
                        End If
                    ElseIf VB.Left(cboRefType.Text, 1) = "I" Or VB.Left(cboRefType.Text, 1) = "1" Or VB.Left(cboRefType.Text, 1) = "2" Or VB.Left(cboRefType.Text, 1) = "3" Then
                        If RsCompany.Fields("StockBalCheck").Value = "Y" And mRefNo > 0 Then
                            If MainClass.ValidateWithMasterTable(mRefNo, "AUTO_KEY_INVOICE", "INVOICE_DATE", "FIN_INVOICE_HDR", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND (SUPP_CUST_CODE='" & mSupplierCode & "' OR BUYER_CODE='" & mSupplierCode & "' OR CO_BUYER_CODE='" & mSupplierCode & "') AND INVOICE_DATE<=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')") = False Then
                                MsgInformation("Invalid Bill Date / Ref Nofor Such Supplier")
                                CheckRefDate = False
                                Exit Function
                            End If
                        End If
                    End If
                End If
            Next
        End With
        CheckRefDate = True
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        CheckRefDate = False
    End Function
    Private Function GetCostC(ByVal pDeptCode As String) As String

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        GetCostC = "001"

        SqlStr = " SELECT IH.CC_CODE " & vbCrLf & " FROM FIN_CCENTER_HDR IH, FIN_CCENTER_DET ID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=ID.COMPANY_CODE AND IH.CC_CODE=ID.CC_CODE" & vbCrLf & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(pDeptCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetCostC = IIf(IsDBNull(RsTemp.Fields("CC_CODE").Value), "001", RsTemp.Fields("CC_CODE").Value)
        End If
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function GetItemLocking(ByVal pItemCode As String) As Boolean

        On Error GoTo ErrPart
        Dim mSupplierCode As String = ""
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing

        If MainClass.ValidateWithMasterTable((TxtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSupplierCode = MasterNo
        End If

        mSqlStr = "SELECT ITEM_CODE " & vbCrLf _
            & " FROM INV_SCHD_LOCK_DET ID" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSupplierCode) & "'" & vbCrLf _
            & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
            & " AND DATE_FROM<=TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND DATE_TO>=TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = True Then
            GetItemLocking = False
        Else
            GetItemLocking = True
        End If

        Exit Function
ErrPart:
        GetItemLocking = False
    End Function

    Private Sub DataFromERPInvoice()

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsMain As ADODB.Recordset = Nothing
        Dim RSDetail As ADODB.Recordset = Nothing
        Dim RSExp As ADODB.Recordset
        Dim mSaleMKey As String
        Dim mExpCode As String
        Dim mCheckExpName As String
        Dim mExpName As String
        Dim mItemCode As String
        Dim CntRow As Integer

        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)
        MainClass.ClearGrid(SprdExp)
        Call FillSprdExp()

        If MainClass.ValidateWithMasterTable(Trim(txtScanning.Text), "BILLNO", "MKEY", "KJ.FIN_INVOICE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=2 AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "") = True Then
            mSaleMKey = MasterNo
        Else
            MsgBox("Sale Invoice Not Found.")
            Exit Sub
        End If

        ''Main Part.....						
        SqlStr = "SELECT * FROM KJ.FIN_INVOICE_HDR " & vbCrLf & " WHERE COMPANY_CODE=2 AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND MKEY='" & mSaleMKey & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsMain.EOF = False Then
            cboRefType.SelectedIndex = 3
            TxtSupplier.Text = "KAY JAY AUTO LIMITED"
            txtBillNo.Text = IIf(IsDBNull(RsMain.Fields("BILLNO").Value), "", RsMain.Fields("BILLNO").Value)
            txtBillDate.Text = VB6.Format(IIf(IsDBNull(RsMain.Fields("INVOICE_DATE").Value), "", RsMain.Fields("INVOICE_DATE").Value), "DD/MM/YYYY")
            txtST38No.Text = IIf(IsDBNull(RsMain.Fields("ST_38_NO").Value), "", RsMain.Fields("ST_38_NO").Value)
            TxtItemDesc.Text = IIf(IsDBNull(RsMain.Fields("ITEMDESC").Value), "", RsMain.Fields("ITEMDESC").Value)
            TxtTransporter.Text = ""
            txtFreight.Text = ""
            TxtRemarks.Text = ""
            txtFormDetail.Text = ""
            txtScanning.Text = ""
        End If

        If MainClass.ValidateWithMasterTable(TxtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSupplierCode = MasterNo
        End If

        Call FillSprdExp()

        ''Detail Part.....						
        SqlStr = "SELECT * FROM KJ.FIN_INVOICE_DET " & vbCrLf & " WHERE MKEY='" & mSaleMKey & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSDetail, ADODB.LockTypeEnum.adLockReadOnly)

        CntRow = 1
        With SprdMain
            If RSDetail.EOF = False Then
                Do While RSDetail.EOF = False
                    .Row = CntRow
                    .Col = ColPONo
                    .Text = "-1" & RsCompany.Fields("FYEAR").Value

                    .Col = ColPODate
                    .Text = VB6.Format(RunDate, "DD/MM/YYYY")

                    .Col = ColRGPItemCode
                    .Text = ""

                    .Col = ColItemCode
                    mItemCode = Trim(IIf(IsDBNull(RSDetail.Fields("ITEM_CODE").Value), "", RSDetail.Fields("ITEM_CODE").Value))
                    .Text = mItemCode

                    .Col = ColItemName
                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        .Text = Trim(MasterNo)
                    Else
                        .Text = ""
                    End If

                    .Col = ColUnit
                    .Text = Trim(IIf(IsDBNull(RSDetail.Fields("ITEM_UOM").Value), "", RSDetail.Fields("ITEM_UOM").Value))

                    .Col = ColPOQty
                    .Text = "0.00"

                    .Col = ColBalQty
                    .Text = "0.00"

                    .Col = ColBillQty
                    .Text = Trim(IIf(IsDBNull(RSDetail.Fields("ITEM_QTY").Value), "", RSDetail.Fields("ITEM_QTY").Value))

                    .Col = ColReceivedQty
                    .Text = Trim(IIf(IsDBNull(RSDetail.Fields("ITEM_QTY").Value), "", RSDetail.Fields("ITEM_QTY").Value))

                    .Col = ColAcceptQty
                    .Text = Trim(IIf(IsDBNull(RSDetail.Fields("ITEM_QTY").Value), "", RSDetail.Fields("ITEM_QTY").Value))

                    .Col = ColApprovedQty
                    .Text = Trim(IIf(IsDBNull(RSDetail.Fields("ITEM_QTY").Value), "", RSDetail.Fields("ITEM_QTY").Value))

                    .Col = ColShortQty
                    .Text = "0.00"

                    .Col = ColRejQty
                    .Text = "0.00"

                    .Col = ColDevQty
                    .Text = "0.00"

                    .Col = ColSeg
                    .Text = "0.00"

                    .Col = ColRework
                    .Text = "0.00"

                    .Col = ColConvQty
                    .Text = "0.00"

                    .Col = ColStockType
                    .Text = "ST"

                    .Col = ColRate
                    .Text = Trim(IIf(IsDBNull(RSDetail.Fields("ITEM_RATE").Value), "", RSDetail.Fields("ITEM_RATE").Value))

                    .Col = ColAmount
                    .Text = Trim(IIf(IsDBNull(RSDetail.Fields("ITEM_AMT").Value), "", RSDetail.Fields("ITEM_AMT").Value))

                    .Col = ColItemCost
                    .Text = Trim(IIf(IsDBNull(RSDetail.Fields("ITEM_AMT").Value), "", RSDetail.Fields("ITEM_AMT").Value))

                    .Col = ColQCEMP
                    .Text = GetQCEmpCode(mItemCode)

                    .Col = ColPDIRFlag
                    .Value = CStr(System.Windows.Forms.CheckState.Checked)

                    .Col = ColSchdRtnFlag
                    .Value = CStr(System.Windows.Forms.CheckState.Unchecked)

                    .Col = ColQCDate
                    .Text = ""

                    CntRow = CntRow + 1
                    .MaxRows = CntRow
                    RSDetail.MoveNext()
                Loop
            End If
        End With

        FormatSprdMain(-1)

        ''Exp Part.....						
        '    SqlStr = "SELECT * FROM ERP.FIN_INVOICE_EXP " & vbCrLf _						
        ''            & " WHERE MKEY='" & mSaleMKey & "'"						
        '						
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RSExp, adLockReadOnly						
        '						
        '    With SprdExp						
        '        If RSExp.EOF = False Then						
        '            For cntRow = 1 To .MaxRows						
        '                RSExp.MoveFirst						
        '                .Row = cntRow						
        '                .Col = ColExpName						
        '                mCheckExpName = Trim(.Text)						
        '                Do While RSExp.EOF = False						
        '                    mExpCode = Trim(IIf(IsNull(RSExp!EXPCODE), "", RSExp!EXPCODE))						
        '						
        '                    If MainClass.ValidateWithMasterTable(mExpCode, "CODE", "NAME", "FIN_INTERFACE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then						
        '                        mExpName = Trim(MasterNo)						
        '                    End If						
        '						
        '                    If mCheckExpName = mExpName Then						
        '                        .Col = ColExpPercent						
        '                        .Text = IIf(IsNull(RSExp!EXPPERCENT), 0, RSExp!EXPPERCENT)						
        '						
        '                        .Col = ColExpAmt						
        '                        .Text = IIf(IsNull(RSExp!AMOUNT), 0, RSExp!AMOUNT)						
        '						
        '                        Exit Do						
        '                    End If						
        '                    RSExp.MoveNext						
        '                Loop						
        '            Next						
        '        End If						
        '    End With						

        Call CalcTots()

        Exit Sub
ErrPart:
        ''Resume						
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtVehicle_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVehicle.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtVehicle_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtVehicle.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchVehicleMaster()
    End Sub
    Private Sub SearchVehicleMaster()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtVehicle.Text), "FIN_VEHICLE_MST", "NAME", "CODE", , , SqlStr) = True Then
            txtVehicle.Text = AcName
            txtVehicle_Validating(txtVehicle, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtVehicle_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtVehicle.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtVehicle.Text) = "" Then GoTo EventExitSub

        If CDbl(VB.Left(cboMode.Text, 1)) = 2 Then
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

            If MainClass.ValidateWithMasterTable((txtVehicle.Text), "NAME", "TRANSPORTER_NAME", "FIN_VEHICLE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
                MsgInformation("Invalid Vehicle No")
                Cancel = True
            Else
                TxtTransporter.Text = MasterNo
            End If
        End If

        If CDbl(VB.Left(cboMode.Text, 1)) = 4 Then
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

            If MainClass.ValidateWithMasterTable((txtVehicle.Text), "NAME", "TRANSPORTER_NAME", "FIN_VEHICLE_MST", PubDBCn, MasterNo, , SqlStr) = True Then
                TxtTransporter.Text = MasterNo
            End If
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtsupplier_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtSupplier.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(TxtSupplier.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_TYPE IN ('S','C')"

        If ADDMode = True Then
            SqlStr = SqlStr & "  AND STATUS='O'"
        End If

        If MainClass.ValidateWithMasterTable((TxtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            mSupplierCode = MasterNo

            If txtBillTo.Text = "" Then
                txtBillTo.Text = GetDefaultLocation(mSupplierCode)
            End If

        Else
            mSupplierCode = "-1"
            Cancel = True
        End If

        Call FillSprdExp()
        pTempUpdate = False
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Function CalcRGPRecvQty(ByVal CurrPONo As Double, ByVal CurrItemCode As String, ByVal pSupplierCode As String) As Double

        On Error GoTo CalcRecvQtyErr
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim xPoNo As String
        Dim xFYNo As Integer
        Dim SqlStr As String = ""
        Dim CurrMrrNo As Double

        If mWithOutOrder = True Then CalcRGPRecvQty = 0.0# : Exit Function

        CurrMrrNo = IIf(Trim(txtMRRNo.Text) = "", -1, Val(txtMRRNo.Text))

        SqlStr = ""

        SqlStr = "SELECT SUM(DECODE(ITEM_IO,'O',0,1)*TRN.RGP_QTY) AS RECDQTY " & vbCrLf & " FROM INV_RGP_REG_TRN TRN WHERE " & vbCrLf & " TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND TRN.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(UCase(pSupplierCode)) & "' " & vbCrLf & " AND TRN.RGP_NO=" & Val(CStr(CurrPONo)) & " " & vbCrLf & " AND TRN.OUTWARD_ITEM_CODE='" & MainClass.AllowSingleQuote(CurrItemCode) & "' "

        'If CurrMrrNo <> CDbl("-1") Then
        '    SqlStr = SqlStr & vbCrLf & " AND TRN.REF_NO<>" & Val(CStr(CurrMrrNo)) & ""
        'End If


        SqlStr = SqlStr & vbCrLf & " AND TRN.REF_NO NOT IN (" & vbCrLf _
                & " SELECT REF_NO FROM INV_RGP_REG_TRN " & vbCrLf _
                & " WHERE " & vbCrLf _
                & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(UCase(pSupplierCode)) & "'" & vbCrLf _
                & " AND BILL_NO='" & txtBillNo.Text & "' AND  BILL_DATE =TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND BOOKTYPE='M' AND ITEM_IO='I')"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMisc.EOF = False Then
            CalcRGPRecvQty = Val(IIf(IsDBNull(RsMisc.Fields(0).Value), 0, RsMisc.Fields(0).Value))
        Else
            CalcRGPRecvQty = 0.0#
        End If
        Exit Function
CalcRecvQtyErr:
        CalcRGPRecvQty = 0.0#
        MsgBox(Err.Description)
    End Function

    Private Function CalcRGPBalanceQty(ByVal CurrPONo As Double, ByVal CurrItemCode As String, ByVal pSupplierCode As String) As Double

        On Error GoTo CalcRecvQtyErr
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim xPoNo As String
        Dim xFYNo As Integer
        Dim SqlStr As String = ""
        Dim CurrMrrNo As Double

        If mWithOutOrder = True Then CalcRGPBalanceQty = 0.0# : Exit Function

        CurrMrrNo = IIf(Trim(txtMRRNo.Text) = "", -1, Val(txtMRRNo.Text))

        SqlStr = ""

        SqlStr = "SELECT SUM(DECODE(ITEM_IO,'O',1,-1)*TRN.RGP_QTY) AS RECDQTY " & vbCrLf _
            & " FROM INV_RGP_REG_TRN TRN WHERE " & vbCrLf _
            & " TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND TRN.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(UCase(pSupplierCode)) & "' " & vbCrLf _
            & " AND TRN.RGP_NO=" & Val(CStr(CurrPONo)) & " " & vbCrLf _
            & " AND TRN.OUTWARD_ITEM_CODE='" & MainClass.AllowSingleQuote(CurrItemCode) & "' "

        'If CurrMrrNo <> CDbl("-1") Then
        '    SqlStr = SqlStr & vbCrLf & " AND TRN.REF_NO<>" & Val(CStr(CurrMrrNo)) & ""
        'End If

        SqlStr = SqlStr & vbCrLf & " AND TRN.REF_NO NOT IN (" & vbCrLf _
                & " SELECT REF_NO FROM INV_RGP_REG_TRN " & vbCrLf _
                & " WHERE " & vbCrLf _
                & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(UCase(pSupplierCode)) & "'" & vbCrLf _
                & " AND BILL_NO='" & txtBillNo.Text & "' AND  BILL_DATE =TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND BOOKTYPE='M' AND ITEM_IO='I')"



        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMisc.EOF = False Then
            CalcRGPBalanceQty = Val(IIf(IsDBNull(RsMisc.Fields(0).Value), 0, RsMisc.Fields(0).Value))
        Else
            CalcRGPBalanceQty = 0.0#
        End If
        Exit Function
CalcRecvQtyErr:
        CalcRGPBalanceQty = 0.0#
        MsgBox(Err.Description)
    End Function

    Private Function CalcRecvRGPREJQty(ByVal CurrPONo As Double, ByVal OutItemCode As String, ByVal pSupplierCode As String) As Double

        On Error GoTo CalcRecvQtyErr
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim xPoNo As String
        Dim xFYNo As Integer
        Dim SqlStr As String = ""
        Dim CurrMrrNo As Double

        If mWithOutOrder = True Then CalcRecvRGPREJQty = 0.0# : Exit Function

        CurrMrrNo = IIf(Trim(txtMRRNo.Text) = "", -1, Val(txtMRRNo.Text))

        SqlStr = ""

        SqlStr = "SELECT SUM(ID.RECEIVED_QTY) AS RECDQTY " & vbCrLf & " FROM INV_GATE_HDR IH, INV_GATE_DET ID WHERE " & vbCrLf & " IH.AUTO_KEY_MRR=ID.AUTO_KEY_MRR" & vbCrLf & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(UCase(pSupplierCode)) & "' " & vbCrLf & " AND ID.REF_AUTO_KEY_NO=" & Val(CStr(CurrPONo)) & " " & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(OutItemCode) & "' "

        If VB.Left(cboRefType.Text, 1) <> "P" Then
            SqlStr = SqlStr & vbCrLf & " AND IH.REF_TYPE='" & VB.Left(cboRefType.Text, 1) & "'"
        End If

        If CurrMrrNo <> CDbl("-1") Then
            SqlStr = SqlStr & vbCrLf & " AND IH.AUTO_KEY_MRR<>" & Val(CStr(CurrMrrNo)) & ""
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMisc.EOF = False Then
            CalcRecvRGPREJQty = Val(IIf(IsDBNull(RsMisc.Fields(0).Value), 0, RsMisc.Fields(0).Value))
        Else
            CalcRecvRGPREJQty = 0.0#
        End If
        Exit Function
CalcRecvQtyErr:
        CalcRecvRGPREJQty = 0.0#
        MsgBox(Err.Description)
    End Function
    Private Sub txtSupplier_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtSupplier.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Function CalcPOQty(ByVal pSupplierCode As String, ByVal pPONO As Double, ByVal pItemCode As String, ByVal pRefType As String, ByRef pOpenOrder As Boolean, ByVal mDivisionCode As Double) As Double

        On Error GoTo ErrPart
        Dim mSchdDate As String
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing


        mSchdDate = "01/" & VB6.Format(txtMRRDate.Text, "MM") & "/" & VB6.Format(txtMRRDate.Text, "YYYY")
        pOpenOrder = False

        If pRefType = "P" Then
            SqlStr = "SELECT ITEM_QTY,ORDER_TYPE " & vbCrLf & " FROM PUR_PURCHASE_HDR POMain, PUR_PURCHASE_DET PODetail" & vbCrLf & " WHERE POMain.MKEY=PODetail.MKEY AND POMain.COMPANY_CODE=PODetail.COMPANY_CODE " & vbCrLf & " AND POMain.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & pSupplierCode & "'" & vbCrLf & " AND PODetail.ITEM_CODE='" & Trim(pItemCode) & "'" & vbCrLf & " AND POMain.AUTO_KEY_PO=" & Val(CStr(pPONO)) & ""

            SqlStr = SqlStr & vbCrLf & " AND POMain.DIV_CODE=" & mDivisionCode & ""

            '        SqlStr = SqlStr & vbCrLf & " AND POMain.PO_STATUS='Y' AND POMain.PO_CLOSED='N'"						

            SqlStr = SqlStr & vbCrLf & " AND POMain.MKEY = ( " & vbCrLf & " SELECT MAX(IH.MKEY) " & vbCrLf & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID" & vbCrLf & " WHERE IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY AND IH.COMPANY_CODE=ID.COMPANY_CODE " & vbCrLf & " AND IH.AUTO_KEY_PO =" & Val(CStr(pPONO)) & " And IH.PO_STATUS='Y' AND IH.DIV_CODE=" & mDivisionCode & " And ID.ITEM_CODE='" & Trim(pItemCode) & "'" & vbCrLf & " AND ID.PO_WEF_DATE<=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            If CDate(txtMRRDate.Text) < CDate(PubGSTApplicableDate) And CDate(txtBillDate.Text) < CDate(PubGSTApplicableDate) Then

            ElseIf CDate(txtMRRDate.Text) < CDate(PubGSTApplicableDate) And CDate(txtBillDate.Text) >= CDate(PubGSTApplicableDate) Then

            ElseIf CDate(txtMRRDate.Text) >= CDate(PubGSTApplicableDate) And CDate(txtBillDate.Text) < CDate(PubGSTApplicableDate) Then

            Else
                SqlStr = SqlStr & vbCrLf & " AND ID.PO_WEF_DATE>=TO_DATE('" & VB6.Format(PubGSTApplicableDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If

            '        If PubGSTApplicable = True Then						
            '            SqlStr = SqlStr & vbCrLf & " AND ID.PO_WEF_DATE>='" & VB6.Format(PubGSTApplicableDate, "DD-MMM-YYYY") & "'"						
            '        End If						

            SqlStr = SqlStr & ")"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                If RsTemp.Fields("ORDER_TYPE").Value = "C" Then
                    CalcPOQty = IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)
                Else
                    CalcPOQty = CalcDSQty(pSupplierCode, pPONO, pItemCode)
                    pOpenOrder = True
                End If
            End If
        ElseIf pRefType = "R" Then
            SqlStr = "SELECT ITEM_QTY" & vbCrLf & " FROM INV_GATEPASS_HDR POMain, INV_GATEPASS_DET PODetail" & vbCrLf & " WHERE POMain.AUTO_KEY_PASSNO=PODetail.AUTO_KEY_PASSNO" & vbCrLf & " AND POMain.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND POMain.SUPP_CUST_CODE='" & pSupplierCode & "'" & vbCrLf & " AND PODetail.ITEM_CODE='" & Trim(pItemCode) & "'" & vbCrLf & " AND PODetail.AUTO_KEY_PASSNO=" & Val(CStr(pPONO)) & ""

            SqlStr = SqlStr & vbCrLf & " AND POMain.DIV_CODE=" & mDivisionCode & ""

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                CalcPOQty = IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)
            End If
        End If

        Exit Function
ErrPart:
        CalcPOQty = 0
    End Function
    Private Function CalcRecvQty(ByVal CurrPONo As String, ByVal CurrItemCode As String, ByVal pSupplierCode As String, ByVal pOpenOrder As Boolean) As Double

        On Error GoTo CalcRecvQtyErr
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim xPoNo As String
        Dim xFYNo As Integer
        Dim SqlStr As String = ""
        Dim CurrMrrNo As Double
        Dim xSchldDate As String
        Dim mLastDayOfMonth As String
        Dim mCategoryType As String

        If RsCompany.Fields("WEEKLY_SCHD").Value = "N" Then
            xSchldDate = "01/" & VB6.Format(txtMRRDate.Text, "MM") & "/" & VB6.Format(txtMRRDate.Text, "YYYY")
            mLastDayOfMonth = MainClass.LastDay(Month(CDate(txtMRRDate.Text)), Year(CDate(txtMRRDate.Text))) & "/" & VB6.Format(txtMRRDate.Text, "MM") & "/" & VB6.Format(txtMRRDate.Text, "YYYY")
        Else
            '        xSchldDate = GetFirstDayInWeek(txtMRRDate.Text)						
            '        mLastDayOfMonth = GetLastDayInWeek(txtMRRDate.Text)						
            mCategoryType = GetProductionType(CurrItemCode)
            If mCategoryType = "G" Or mCategoryType = "C" Or mCategoryType = "T" Or mCategoryType = "A" Then
                xSchldDate = "01/" & VB6.Format(txtMRRDate.Text, "MM") & "/" & VB6.Format(txtMRRDate.Text, "YYYY")
                mLastDayOfMonth = MainClass.LastDay(Month(CDate(txtMRRDate.Text)), Year(CDate(txtMRRDate.Text))) & "/" & VB6.Format(txtMRRDate.Text, "MM") & "/" & VB6.Format(txtMRRDate.Text, "YYYY")
            Else

                If VB.Day(CDate(txtMRRDate.Text)) < 8 Then
                    xSchldDate = "01/" & VB6.Format(txtMRRDate.Text, "MM") & "/" & VB6.Format(txtMRRDate.Text, "YYYY")
                    mLastDayOfMonth = "07/" & VB6.Format(txtMRRDate.Text, "MM") & "/" & VB6.Format(txtMRRDate.Text, "YYYY")
                ElseIf VB.Day(CDate(txtMRRDate.Text)) < 15 Then
                    xSchldDate = "08/" & VB6.Format(txtMRRDate.Text, "MM") & "/" & VB6.Format(txtMRRDate.Text, "YYYY")
                    mLastDayOfMonth = "14/" & VB6.Format(txtMRRDate.Text, "MM") & "/" & VB6.Format(txtMRRDate.Text, "YYYY")
                ElseIf VB.Day(CDate(txtMRRDate.Text)) < 22 Then
                    xSchldDate = "15/" & VB6.Format(txtMRRDate.Text, "MM") & "/" & VB6.Format(txtMRRDate.Text, "YYYY")
                    mLastDayOfMonth = "21/" & VB6.Format(txtMRRDate.Text, "MM") & "/" & VB6.Format(txtMRRDate.Text, "YYYY")
                ElseIf VB.Day(CDate(txtMRRDate.Text)) < 29 Then
                    xSchldDate = "22/" & VB6.Format(txtMRRDate.Text, "MM") & "/" & VB6.Format(txtMRRDate.Text, "YYYY")
                    mLastDayOfMonth = "28/" & VB6.Format(txtMRRDate.Text, "MM") & "/" & VB6.Format(txtMRRDate.Text, "YYYY")
                Else
                    xSchldDate = "29/" & VB6.Format(txtMRRDate.Text, "MM") & "/" & VB6.Format(txtMRRDate.Text, "YYYY")
                    mLastDayOfMonth = MainClass.LastDay(Month(CDate(txtMRRDate.Text)), Year(CDate(txtMRRDate.Text))) & "/" & VB6.Format(txtMRRDate.Text, "MM") & "/" & VB6.Format(txtMRRDate.Text, "YYYY")
                End If
            End If
        End If

        If mWithOutOrder = True Then CalcRecvQty = 0.0# : Exit Function

        CurrMrrNo = IIf(Trim(txtMRRNo.Text) = "", -1, Val(txtMRRNo.Text))

        SqlStr = ""

        ''-DECODE(QC_STATUS,'Y',REJECTED_QTY,0)						
        ''						
        '						
        '						
        '    SqlStr = "SELECT SUM(ID.RECEIVED_QTY) AS RECDQTY " & vbCrLf _						
        ''            & " FROM INV_GATE_HDR IH, INV_GATE_DET ID WHERE " & vbCrLf _						
        ''            & " IH.AUTO_KEY_MRR=ID.AUTO_KEY_MRR" & vbCrLf _						
        ''            & " AND IH.Company_Code=" & RsCompany.fields("COMPANY_CODE").value & " AND SUBSTR(IH.AUTO_KEY_MRR,LENGTH(IH.AUTO_KEY_MRR)-5,4)=" & RsCompany.fields("FYEAR").value & "" & vbCrLf _						
        ''            & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(UCase(pSupplierCode)) & "' " & vbCrLf _						
        ''            & " AND ID.REF_AUTO_KEY_NO=" & Val(CurrPONo) & " "						

        SqlStr = "SELECT SUM(ID.RECEIVED_QTY) AS RECDQTY " & vbCrLf & " FROM INV_GATE_DET ID WHERE " & vbCrLf & " ID.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUBSTR(ID.AUTO_KEY_MRR,LENGTH(ID.AUTO_KEY_MRR)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND TRIM(ID.SUPP_CUST_CODE)='" & MainClass.AllowSingleQuote(UCase(pSupplierCode)) & "' " '' & vbCrLf |            & " AND ID.REF_AUTO_KEY_NO=" & Val(CurrPONo) & " "						

        '    If chkShipTo.Value = vbChecked Then        ''Not Required  .  11/06/2020  SK PO must required in both cases						
        SqlStr = SqlStr & "  AND ID.REF_AUTO_KEY_NO=" & CurrPONo & " "
        '    End If						

        If VB.Left(cboRefType.Text, 1) = "R" Then
            SqlStr = SqlStr & vbCrLf & " AND ID.RGP_ITEM_CODE='" & MainClass.AllowSingleQuote(CurrItemCode) & "' "
        Else
            SqlStr = SqlStr & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(CurrItemCode) & "' "
        End If

        If VB.Left(cboRefType.Text, 1) <> "P" Then
            SqlStr = SqlStr & vbCrLf & " AND ID.REF_TYPE='" & VB.Left(cboRefType.Text, 1) & "'"
        End If

        If CurrMrrNo <> CDbl("-1") Then
            SqlStr = SqlStr & vbCrLf & " AND ID.AUTO_KEY_MRR<>" & Val(CStr(CurrMrrNo)) & ""
        End If

        If VB.Left(cboRefType.Text, 1) = "P" And pOpenOrder = True Then
            If xSchldDate <> "" Then 'DEEPAK  IF PO HAS MORE THAN ONE DLV SCHLD OF SAME ITEM , IT WAS CONSID PREV MRR QTY ALSO 01/05/2004						
                SqlStr = SqlStr & vbCrLf & " AND ID.MRR_DATE>=TO_DATE('" & VB6.Format(xSchldDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND ID.MRR_DATE<=TO_DATE('" & VB6.Format(mLastDayOfMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMisc.EOF = False Then
            CalcRecvQty = Val(IIf(IsDBNull(RsMisc.Fields(0).Value), 0, RsMisc.Fields(0).Value))
        Else
            CalcRecvQty = 0.0#
        End If
        Exit Function
CalcRecvQtyErr:
        CalcRecvQty = 0.0#
        MsgBox(Err.Description)
    End Function

    Private Sub FillRGPDetailPart(ByVal RsPO As ADODB.Recordset, ByVal mRefNo As Double, ByVal SprdRowNo As Integer, ByVal xInItemCode As String, ByVal xOutItemCode As String, ByVal xInConUnit As Double, ByVal xOutConUnit As Double, ByVal pSupplierCode As String)


        On Error GoTo ERR1
        Dim mOrdQty As Object
        Dim mRecvQty As Double
        Dim mOutItemCode As String
        Dim mItemDesc As String
        Dim mItemUOM As String = ""
        Dim mCheckUOM As String
        Dim mPOQty As Double
        Dim mRecdQty As Double
        Dim mBalQty As Double
        Dim mRejQty As Double

        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mQCEmpCode As String
        Dim mBatchNo As String

        If RsPO.EOF Then Exit Sub
        RsPO.MoveFirst()
        mOutItemCode = xOutItemCode 'Trim(IIf(IsNull(RsPO!ITEM_CODE), "", RsPO!ITEM_CODE))						

        With SprdMain

            .Row = SprdRowNo

            .Col = ColItemCode
            .Text = xInItemCode

            .Col = ColItemName
            MainClass.ValidateWithMasterTable(xInItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
            mItemDesc = MasterNo
            .Text = mItemDesc

            .Col = ColHSNCode
            MainClass.ValidateWithMasterTable(xInItemCode, "Item_Code", "HSN_CODE", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
            .Text = MasterNo


            If VB.Left(cboRefType.Text, 1) = "R" Then
                mBatchNo = GetBatchNo(mRefNo, xOutItemCode, pSupplierCode)

                .Col = ColBatchNo
                .Text = mBatchNo
            Else

            End If
            '            .text= IIf(IsNull(!ITEM_CODE), "", !ITEM_CODE)						

            .Col = ColUnit
            If xInItemCode = xOutItemCode Then
                mItemUOM = IIf(IsDBNull(RsPO.Fields("ITEM_UOM").Value), "", RsPO.Fields("ITEM_UOM").Value)
            Else
                If MainClass.ValidateWithMasterTable(xInItemCode, "Item_Code", "ISSUE_UOM", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mItemUOM = MasterNo
                End If
            End If

            .Text = mItemUOM

            .Col = ColPOQty
            If xInItemCode = xOutItemCode Then
                mPOQty = IIf(IsDBNull(RsPO.Fields("ITEM_QTY").Value), "", RsPO.Fields("ITEM_QTY").Value)
            Else
                mPOQty = CDbl(VB6.Format(xInConUnit * IIf(IsDBNull(RsPO.Fields("ITEM_QTY").Value), "", RsPO.Fields("ITEM_QTY").Value) / xOutConUnit, "0.0000"))
            End If

            .Text = CStr(mPOQty)

            If VB.Left(cboRefType.Text, 1) = "R" Then
                mRecdQty = CalcRGPRecvQty(mRefNo, xOutItemCode, mSupplierCode)
            Else
                mRecdQty = CalcRecvQty(Str(mRefNo), xInItemCode, mSupplierCode, False)
            End If

            If xInItemCode <> xOutItemCode Then
                mRecdQty = CDbl(VB6.Format(xInConUnit * mRecdQty / xOutConUnit, "0.0000"))
            End If

            If VB.Left(cboRefType.Text, 1) = "R" Then
                '            If xInItemCode = xOutItemCode Then						
                mRejQty = 0
                '            Else						
                '                mRejQty = CalcRecvRGPREJQty(mRefNo, mOutItemCode, mSupplierCode)						
                '            End If						
            Else
                mRejQty = 0
            End If

            '          If xInItemCode = xInItemCode Then						
            '            mRejQty = xInConUnit * mRejQty / xOutConUnit						
            '          End If						

            mBalQty = mPOQty - (mRecdQty + mRejQty)
            .Col = ColBalQty
            .Text = CStr(mBalQty)

            .Col = ColPORate
            If xInItemCode = xOutItemCode Then
                .Text = CStr(Val(IIf(IsDBNull(RsPO.Fields("ITEM_RATE").Value), 0, RsPO.Fields("ITEM_RATE").Value))) ' Val(IIf(IsNull(RsPO!ITEM_RATE), 0, RsPO!ITEM_RATE))
                'ElseIf MainClass.ValidateWithMasterTable(xInItemCode, "Item_Code", "ITEM_RATE", "FIN_SUPP_CUST_DET", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & pSupplierCode & "'") = True Then
                '    .Text = IIf(IsDBNull(MasterNo) Or MasterNo = 0, RsPO.Fields("ITEM_RATE").Value, MasterNo)
            Else
                SprdMain.Text = CStr(Val(IIf(IsDBNull(RsPO.Fields("ITEM_RATE").Value), 0, RsPO.Fields("ITEM_RATE").Value)) / IIf(xInConUnit = 0, 1, xInConUnit))
            End If

            .Col = ColRate
            If xInItemCode = xOutItemCode Then
                .Text = CStr(Val(IIf(IsDBNull(RsPO.Fields("ITEM_RATE").Value), 0, RsPO.Fields("ITEM_RATE").Value)))
                'ElseIf MainClass.ValidateWithMasterTable(xInItemCode, "Item_Code", "ITEM_RATE", "FIN_SUPP_CUST_DET", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & pSupplierCode & "'") = True Then
                '    .Text = IIf(IsDBNull(MasterNo) Or MasterNo = 0, RsPO.Fields("ITEM_RATE").Value, MasterNo)
            Else
                .Text = CStr(Val(IIf(IsDBNull(RsPO.Fields("ITEM_RATE").Value), 0, RsPO.Fields("ITEM_RATE").Value)) / IIf(xInConUnit = 0, 1, xInConUnit))
            End If

            '          If Left(cboRefType.Text, 1) = "R" Or Left(cboRefType.Text, 1) = "I" Then						
            '              .Text = Val(IIf(IsNull(RsPO!ITEM_RATE), 0, RsPO!ITEM_RATE))						
            '          Else						
            '              .Text = Val(IIf(IsNull(RsPO!ITEM_PRICE), 0, RsPO!ITEM_PRICE))						
            '          End If						

            .Col = ColStockType
            If lblBookType.Text = "G" Then
                .Text = IIf(Trim(.Text) = "", "QC", Trim(.Text))
            Else
                .Text = IIf(Trim(.Text) = "", "ST", Trim(.Text))
            End If

            '        mQCEmpCode = IIf(IsNull(RsPO!AUTH_EMP_CODE), "", RsPO!AUTH_EMP_CODE)						
            '						
            '        SprdMain.Col = ColQCEmp						
            '        SprdMain.Text = IIf(Trim(SprdMain.Text) = "", mQCEmpCode, Trim(SprdMain.Text))						



            mQCEmpCode = GetQCEmpCode(xInItemCode)

            SprdMain.Col = ColQCEMP
            SprdMain.Text = IIf(Trim(SprdMain.Text) = "", mQCEmpCode, Trim(SprdMain.Text))


            '						
            '          MainClass.AddBlankSprdRow SprdMain, ColItemCode, ConRowHeight						
        End With
        '        FormatSprdMain -1						
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        '    Resume						
    End Sub
    Private Function GetBatchNo(ByVal mRefNo As Double, ByVal xOutItemCode As String, ByVal pSupplierCode As String) As String


        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xFGBatchNoReq As String

        If MainClass.ValidateWithMasterTable(xOutItemCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DSP_RPT_FLAG='Y'") = True Then
            GetBatchNo = "-1"
            xFGBatchNoReq = "Y"
        Else
            xFGBatchNoReq = "N"
            GetBatchNo = ""
            Exit Function
        End If

        SqlStr = "SELECT LOT_NO" & vbCrLf & " FROM INV_GATEPASS_HDR IH, INV_GATEPASS_DET ID" & vbCrLf & " WHERE IH.AUTO_KEY_PASSNO=ID.AUTO_KEY_PASSNO" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.SUPP_CUST_CODE='" & pSupplierCode & "'" & vbCrLf & " AND ID.ITEM_CODE='" & Trim(xOutItemCode) & "'" & vbCrLf & " AND IH.AUTO_KEY_PASSNO=" & Val(CStr(mRefNo)) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetBatchNo = IIf(IsDBNull(RsTemp.Fields("LOT_NO").Value), "-1", RsTemp.Fields("LOT_NO").Value)
        End If

        Exit Function
ERR1:
        MsgBox(Err.Description)
        '    Resume						
    End Function

    Private Sub FillPODetailPart(ByVal RsPO As ADODB.Recordset, ByVal xPoNo As String, ByVal SprdRowNo As Integer)

        On Error GoTo ERR1
        Dim mOrdQty As Object
        Dim mRecvQty As Double
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mPOQty As Double
        Dim mRecdQty As Double
        Dim mBalQty As Double
        Dim mRefNo As Double
        Dim mOpenOrder As Boolean
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mQCEmpCode As String
        Dim pSupplierCode As String = ""
        Dim mHSNCode As String
        Dim mPONo As Double

        pSupplierCode = ""

        'If chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked Then
        If Trim(TxtSupplier.Text) <> "" Then
            If MainClass.ValidateWithMasterTable((TxtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                pSupplierCode = MasterNo
            End If
        End If
        'Else
        '    If Trim(txtShippedTo.Text) <> "" Then
        '        If MainClass.ValidateWithMasterTable((txtShippedTo.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '            pSupplierCode = MasterNo
        '        End If
        '    End If
        'End If

        If RsPO.EOF Then Exit Sub
        RsPO.MoveFirst()
        mOpenOrder = False

        With SprdMain
            .Row = SprdRowNo

            .Col = ColPODate
            If VB.Left(cboRefType.Text, 1) = "I" Or VB.Left(cboRefType.Text, 1) = "1" Or VB.Left(cboRefType.Text, 1) = "2" Or VB.Left(cboRefType.Text, 1) = "3" Then
                .Text = IIf(IsDBNull(RsPO.Fields("INVOICE_DATE").Value), "", RsPO.Fields("INVOICE_DATE").Value)
            Else
                .Text = IIf(IsDBNull(RsPO.Fields("PUR_ORD_DATE").Value), "", RsPO.Fields("PUR_ORD_DATE").Value)
            End If

            .Col = ColItemCode
            mItemCode = Trim(IIf(IsDBNull(RsPO.Fields("ITEM_CODE").Value), "", RsPO.Fields("ITEM_CODE").Value))
            .Text = mItemCode

            .Col = ColItemName
            MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
            mItemDesc = MasterNo
            .Text = mItemDesc

            .Col = ColHSNCode
            mHSNCode = ""
            If VB.Left(cboRefType.Text, 1) = "P" Then
                mHSNCode = ""
                .Col = ColPONo
                mPONo = Trim(.Text)
                mHSNCode = GetHSNFromPurchaseOrder(mItemCode, mPONo)
                .Col = ColHSNCode
                .Text = mHSNCode
            End If
            If mHSNCode = "" Then
                MainClass.ValidateWithMasterTable(Trim(mItemCode), "Item_Code", "HSN_CODE", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                .Text = MasterNo
            End If

            '.Col = ColHSNCode
            'MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "HSN_Code", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
            '.Text = MasterNo

            .Col = ColUnit
            .Text = IIf(IsDBNull(RsPO.Fields("ITEM_UOM").Value), "", RsPO.Fields("ITEM_UOM").Value)

            .Col = ColPOQty
            If VB.Left(cboRefType.Text, 1) = "R" Or VB.Left(cboRefType.Text, 1) = "I" Or VB.Left(cboRefType.Text, 1) = "1" Or VB.Left(cboRefType.Text, 1) = "2" Or VB.Left(cboRefType.Text, 1) = "3" Then
                mPOQty = IIf(IsDBNull(RsPO.Fields("ITEM_QTY").Value), 0, RsPO.Fields("ITEM_QTY").Value)
            Else
                If RsPO.Fields("ORDER_TYPE").Value = "O" Then
                    mPOQty = CalcDSQty(pSupplierCode, RsPO.Fields("AUTO_KEY_PO").Value, RsPO.Fields("ITEM_CODE").Value)
                    mOpenOrder = True
                Else
                    mPOQty = IIf(IsDBNull(RsPO.Fields("ITEM_QTY").Value), "", RsPO.Fields("ITEM_QTY").Value)
                End If
            End If

            .Text = CStr(mPOQty)
            mRecdQty = CalcRecvQty(xPoNo, RsPO.Fields("ITEM_CODE").Value, pSupplierCode, mOpenOrder)
            mBalQty = mPOQty - mRecdQty


            .Col = ColBalQty
            .Text = CStr(mBalQty)

            .Col = ColPORate
            If VB.Left(cboRefType.Text, 1) = "R" Or VB.Left(cboRefType.Text, 1) = "I" Or VB.Left(cboRefType.Text, 1) = "1" Or VB.Left(cboRefType.Text, 1) = "2" Or VB.Left(cboRefType.Text, 1) = "3" Then
                .Text = CStr(Val(IIf(IsDBNull(RsPO.Fields("ITEM_RATE").Value), 0, RsPO.Fields("ITEM_RATE").Value)))
            Else
                .Text = CStr(Val(IIf(IsDBNull(RsPO.Fields("I_RATE").Value), 0, RsPO.Fields("I_RATE").Value)))
            End If

            .Col = ColRate
            If VB.Left(cboRefType.Text, 1) = "R" Or VB.Left(cboRefType.Text, 1) = "I" Or VB.Left(cboRefType.Text, 1) = "1" Or VB.Left(cboRefType.Text, 1) = "2" Or VB.Left(cboRefType.Text, 1) = "3" Then
                .Text = CStr(Val(IIf(IsDBNull(RsPO.Fields("ITEM_RATE").Value), 0, RsPO.Fields("ITEM_RATE").Value)))
            Else
                .Text = CStr(Val(IIf(IsDBNull(RsPO.Fields("I_RATE").Value), 0, RsPO.Fields("I_RATE").Value)))
            End If

            .Col = ColStockType
            If lblBookType.Text = "G" Then
                .Text = IIf(Trim(.Text) = "", "QC", Trim(.Text))
            Else
                .Text = IIf(Trim(.Text) = "", "ST", Trim(.Text))
            End If

            '        mSqlStr = " SELECT QC_EMP_CODE FROM INV_ITEM_MST INVMST, INV_SUBCATEGORY_MST SMST " & vbCrLf _						
            ''                & " WHERE INVMST.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _						
            ''                & " AND INVMST.COMPANY_CODE=SMST.COMPANY_CODE " & vbCrLf _						
            ''                & " AND INVMST.CATEGORY_CODE=SMST.CATEGORY_CODE " & vbCrLf _						
            ''                & " AND INVMST.SUBCATEGORY_CODE=SMST.SUBCATEGORY_CODE " & vbCrLf _						
            ''                & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"						
            '						
            '        MainClass.UOpenRecordSet mSqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly						
            '						
            '        If RsTemp.EOF = False Then						
            '            mQCEmpCode = IIf(IsNull(RsTemp!QC_EMP_CODE), "", RsTemp!QC_EMP_CODE)						
            '        End If						

            mQCEmpCode = GetQCEmpCode(mItemCode)
            .Col = ColQCEMP
            .Text = IIf(Trim(.Text) = "", mQCEmpCode, Trim(.Text))

        End With
        '    FormatSprdMain -1						
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        '    Resume						
    End Sub

    Private Sub FillInwardRGPDetailPart(ByVal RsPO As ADODB.Recordset, ByVal mRefNo As Double, ByVal SprdRowNo As Integer)

        On Error GoTo ERR1
        Dim mOrdQty As Object
        Dim mRecvQty As Double
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mItemUOM As String = ""

        Dim mPOQty As Double
        Dim mRecdQty As Double
        Dim mBalQty As Double

        If RsPO.EOF Then Exit Sub
        RsPO.MoveFirst()

        With SprdMain

            .Row = SprdRowNo

            .Col = ColItemCode
            mItemCode = Trim(IIf(IsDBNull(RsPO.Fields("INWARD_ITEM_CODE").Value), "", RsPO.Fields("INWARD_ITEM_CODE").Value))
            .Text = mItemCode

            .Col = ColItemName
            MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
            mItemDesc = MasterNo
            .Text = mItemDesc

            .Col = ColHSNCode
            MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "HSN_CODE", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
            .Text = MasterNo

            .Col = ColBatchNo
            '            .text= IIf(IsNull(!ITEM_CODE), "", !ITEM_CODE)						

            .Col = ColUnit
            MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "PURCHASE_UOM", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
            mItemUOM = MasterNo
            .Text = mItemUOM

            .Col = ColPOQty
            mPOQty = IIf(IsDBNull(RsPO.Fields("ITEM_QTY").Value), "", RsPO.Fields("ITEM_QTY").Value)  '' IIf(IsDbNull(RsPO.Fields("INWARD_ITEM_QTY").Value), "", RsPO.Fields("INWARD_ITEM_QTY").Value)

            .Text = CStr(mPOQty)
            mRecdQty = CalcRecvQty(mRefNo, RsPO.Fields("INWARD_ITEM_CODE").Value, mSupplierCode, False)
            mBalQty = mPOQty - mRecdQty
            .Col = ColBalQty
            .Text = CStr(mBalQty)

            'mPOQty = IIf(IsDBNull(RsPO.Fields("INWARD_ITEM_QTY").Value), "", RsPO.Fields("INWARD_ITEM_QTY").Value)


            '.Text = CStr(mPOQty)
            'mRecdQty = CalcRecvQty(Str(mRefNo), RsPO.Fields("INWARD_ITEM_CODE").Value, mSupplierCode, False)
            'mBalQty = mPOQty - mRecdQty
            '.Col = ColBalQty
            '.Text = CStr(mBalQty)

            .Col = ColRate
            If VB.Left(cboRefType.Text, 1) = "R" Or VB.Left(cboRefType.Text, 1) = "I" Or VB.Left(cboRefType.Text, 1) = "1" Or VB.Left(cboRefType.Text, 1) = "2" Or VB.Left(cboRefType.Text, 1) = "3" Then
                .Text = CStr(Val(IIf(IsDBNull(RsPO.Fields("ITEM_RATE").Value), 0, RsPO.Fields("ITEM_RATE").Value)))
            Else
                .Text = CStr(Val(IIf(IsDBNull(RsPO.Fields("ITEM_PRICE").Value), 0, RsPO.Fields("ITEM_PRICE").Value)))
            End If

            .Col = ColStockType
            If lblBookType.Text = "G" Then
                .Text = IIf(Trim(.Text) = "", "QC", Trim(.Text))
            Else
                .Text = IIf(Trim(.Text) = "", "ST", Trim(.Text))
            End If

            '          MainClass.AddBlankSprdRow SprdMain, ColItemCode, ConRowHeight						
        End With
        '        FormatSprdMain -1						
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        '    Resume						
    End Sub

    Private Function GetConsQty(ByVal xOutItemCode As String, ByVal xItemCode As String) As Double

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        'Dim RsPO As ADODB.Recordset						
        'Dim xFYNo As Long						
        'Dim jj As Long						
        'Dim mSprdRowNo As Long						
        'Dim mInwardItemCode As String						
        Dim RsTemp As ADODB.Recordset = Nothing
        '						
        'Dim mInConUnit As Double						
        'Dim mOutConUnit As Double						
        '						
        'Dim mMultiItemCode As Boolean						
        'Dim mMKey As String						
        'Dim mCheckOutItem As String						

        SqlStr = ""

        GetConsQty = 0
        If xOutItemCode = "" Then GetConsQty = 0 : Exit Function
        If xItemCode = "" Then GetConsQty = 0 : Exit Function

        If xItemCode = xOutItemCode Then
            GetConsQty = 1
        Else
            SqlStr = "SELECT ITEM_CODE, ITEM_QTY " & vbCrLf & " FROM PRD_OUTBOM_DET " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE='" & xItemCode & "'" & vbCrLf & " AND ITEM_CODE='" & xOutItemCode & "'" & vbCrLf & " AND MKEY IN (SELECT MKEY FROM PRD_OUTBOM_HDR WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND PRODUCT_CODE='" & xItemCode & "' AND STATUS='O')"


            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                GetConsQty = IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)
            Else
                SqlStr = "SELECT ALTER_ITEM_CODE, ALTER_ITEM_QTY " & vbCrLf & " FROM PRD_OUTBOM_ALTER_DET " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE='" & xItemCode & "'" & vbCrLf & " AND ALTER_ITEM_CODE='" & xOutItemCode & "'" & vbCrLf & " AND MKEY IN (SELECT MKEY FROM PRD_OUTBOM_HDR WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND PRODUCT_CODE='" & xItemCode & "' AND STATUS='O')"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTemp.EOF = False Then
                    GetConsQty = IIf(IsDBNull(RsTemp.Fields("ALTER_ITEM_QTY").Value), 0, RsTemp.Fields("ALTER_ITEM_QTY").Value)
                End If
            End If
        End If
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        GetConsQty = 0
    End Function
    Private Function CheckOpenOrder(ByVal pItemCode As String, ByVal mDivisionCode As Double) As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing


        SqlStr = ""

        SqlStr = " SELECT ID.ITEM_CODE " & vbCrLf & " FROM PUR_PURCHASE_HDR IH,PUR_PURCHASE_DET ID" & vbCrLf & " WHERE IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IH.MKEY = ID.MKEY " & vbCrLf & " And IH.SUPP_CUST_CODE='" & mSupplierCode & "' " & vbCrLf & " And ID.ITEM_CODE='" & Trim(pItemCode) & "' " & vbCrLf & " And IH.PO_STATUS='Y' "

        SqlStr = SqlStr & vbCrLf & " AND DIV_CODE=" & mDivisionCode & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            CheckOpenOrder = True
        Else
            CheckOpenOrder = False
        End If
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        CheckOpenOrder = False
    End Function
    Private Function CollectPOData(ByVal xRefType As String, ByVal xPoNo As String, ByVal xItemCode As String, ByVal xOutItemCode As String, ByVal mRowNo As Integer, ByVal mDivisionCode As Double) As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsPO As ADODB.Recordset = Nothing
        Dim xFYNo As Integer
        Dim jj As Integer
        Dim mSprdRowNo As Integer
        Dim mInwardItemCode As String
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim mInConUnit As Double
        Dim mOutConUnit As Double

        Dim mMultiItemCode As Boolean
        Dim mMKEY As String = ""
        Dim mCheckOutItem As String = ""
        Dim pSupplierCode As String = ""

        SqlStr = ""

        pSupplierCode = ""

        'If chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked Then
        If Trim(TxtSupplier.Text) <> "" Then
            If MainClass.ValidateWithMasterTable((TxtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                pSupplierCode = MasterNo
            End If
        End If
        'Else
        '    If Trim(txtShippedTo.Text) <> "" Then
        '        If MainClass.ValidateWithMasterTable((txtShippedTo.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '            pSupplierCode = MasterNo
        '        End If
        '    End If
        'End If

        Select Case xRefType

            Case "P"
                SqlStr = " SELECT POM.*, " & vbCrLf & " POD.*, (NVL(ITEM_PRICE,0) - ROUND((NVL(ITEM_PRICE,0) * ITEM_DIS_PER)/100,2)) AS I_RATE, " & vbCrLf & " AC.SUPP_CUST_NAME as SuppName " & vbCrLf & " FROM PUR_PURCHASE_HDR POM,PUR_PURCHASE_DET POD,FIN_SUPP_CUST_MST AC " & vbCrLf & " WHERE POM.MKEY = POD.MKEY " & vbCrLf & " And POM.Company_Code = AC.Company_Code " & vbCrLf & " And POM.SUPP_CUST_CODE = AC.SUPP_CUST_CODE " & vbCrLf & " And POM.AUTO_KEY_PO=" & Val(xPoNo) & " " & vbCrLf & " And POM.SUPP_CUST_CODE='" & pSupplierCode & "' " & vbCrLf & " And POM.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " And POD.ITEM_CODE='" & Trim(xItemCode) & "' " & vbCrLf & " And POM.PO_STATUS='Y' AND POM.DIV_CODE=" & mDivisionCode & "" & vbCrLf & " AND POM.MKEY = ( " & vbCrLf & " SELECT MAX(IH.MKEY) " & vbCrLf & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID" & vbCrLf & " WHERE IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.AUTO_KEY_PO =" & Val(xPoNo) & " And IH.PO_STATUS='Y' AND IH.DIV_CODE=" & mDivisionCode & " And ID.ITEM_CODE='" & Trim(xItemCode) & "'" & vbCrLf & " AND ID.PO_WEF_DATE<=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                If CDate(txtMRRDate.Text) < CDate(PubGSTApplicableDate) And CDate(txtBillDate.Text) < CDate(PubGSTApplicableDate) Then

                ElseIf CDate(txtMRRDate.Text) < CDate(PubGSTApplicableDate) And CDate(txtBillDate.Text) >= CDate(PubGSTApplicableDate) Then

                ElseIf CDate(txtMRRDate.Text) >= CDate(PubGSTApplicableDate) And CDate(txtBillDate.Text) < CDate(PubGSTApplicableDate) Then

                Else
                    SqlStr = SqlStr & vbCrLf & " AND ID.PO_WEF_DATE>=TO_DATE('" & VB6.Format(PubGSTApplicableDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                End If

                '            If PubGSTApplicable = True Then						
                '                SqlStr = SqlStr & vbCrLf & " AND ID.PO_WEF_DATE>='" & VB6.Format(PubGSTApplicableDate, "DD-MMM-YYYY") & "'"						
                '            End If						

                SqlStr = SqlStr & ")"

                SqlStr = SqlStr & vbCrLf & " ORDER BY POD.SERIAL_NO"

            Case "R"

                Dim mWONo As Double
                Dim mIsReProcess As String

                If MainClass.ValidateWithMasterTable(xPoNo, "AUTO_KEY_PASSNO", "AUTO_KEY_WO", "INV_GATEPASS_DET", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE='" & xOutItemCode & "' AND INWARD_ITEM_CODE='" & xItemCode & "'") = True Then
                    mWONo = MasterNo
                End If

                mIsReProcess = GetItemReProcess(mWONo, xItemCode, pSupplierCode, txtBillDate.Text)

                If xItemCode = xOutItemCode Then
                    mOutConUnit = 1
                Else
                    'SqlStr = "SELECT IH.MKEY, IH.PRODUCT_CODE, ID.ITEM_CODE,ITEM_QTY " & vbCrLf & " FROM PRD_OUTBOM_HDR IH, PRD_OUTBOM_DET ID " & vbCrLf & " WHERE " & vbCrLf & " IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & xItemCode & "'"

                    'SqlStr = SqlStr & vbCrLf & " AND IH.WEF = (" & vbCrLf & " SELECT MAX(WEF) " & vbCrLf & " FROM PRD_OUTBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE='" & xItemCode & "'" & vbCrLf & " AND WEF<=TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                    'If ADDMode = True Then
                    '    SqlStr = SqlStr & " AND STATUS='O')"
                    'Else
                    '    SqlStr = SqlStr & ")"
                    'End If


                    SqlStr = "SELECT A.RM_CODE AS ITEM_CODE,  B.ITEM_SHORT_DESC, B.ISSUE_UOM, (NVL(A.STD_QTY,0) + NVL(A.GROSS_WT_SCRAP,0)) AS STD_QTY" & vbCrLf _
                            & " FROM VW_PRD_BOM_TRN A, INV_ITEM_MST B" & vbCrLf _
                            & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                            & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf & " AND A.RM_CODE=B.ITEM_CODE "

                    If mIsReProcess = "N" Then
                        SqlStr = SqlStr & vbCrLf _
                            & " START WITH  TRIM(A.PRODUCT_CODE) || '-' || A.COMPANY_CODE='" & MainClass.AllowSingleQuote(xItemCode) & "-" & RsCompany.Fields("COMPANY_CODE").Value & "'" & vbCrLf _
                            & " CONNECT BY NOCYCLE (TRIM(A.PRODUCT_CODE) || A.COMPANY_CODE) || ' '=PRIOR TRIM(A.RM_CODE) || A.COMPANY_CODE || ' '"

                    Else
                        SqlStr = SqlStr & vbCrLf _
                            & " START WITH  TRIM(A.RM_CODE) || '-' || A.COMPANY_CODE='" & MainClass.AllowSingleQuote(xItemCode) & "-" & RsCompany.Fields("COMPANY_CODE").Value & "'" & vbCrLf _
                            & " CONNECT BY NOCYCLE (TRIM(A.RM_CODE) || A.COMPANY_CODE) || ' '=PRIOR TRIM(A.PRODUCT_CODE) || A.COMPANY_CODE || ' '"


                        'SqlStr = SqlStr & vbCrLf _
                        '    & " START WITH TRIM(RM_CODE) || '-' || TRN.COMPANY_CODE ='" & MainClass.AllowSingleQuote(xItemCode) & "-" & RsCompany.Fields("COMPANY_CODE").Value & "'" & vbCrLf _
                        '    & " CONNECT BY PRIOR (TRIM(PRODUCT_CODE) || COMPANY_CODE || ' ')= (TRIM(RM_CODE) || COMPANY_CODE || ' ')" & vbCrLf _
                        '    & " ORDER SIBLINGS BY RM_CODE"
                    End If

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                    If RsTemp.EOF = True Then
                        SqlStr = " SELECT DISTINCT RMMST.PARENT_CODE ITEM_CODE, RMMST.ITEM_SHORT_DESC, RMMST.ISSUE_UOM,  " & vbCrLf _
                            & " 1 AS STD_QTY" & vbCrLf _
                            & " FROM INV_ITEM_MST HMST, INV_ITEM_MST RMMST" & vbCrLf _
                            & " WHERE RMMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                            & " AND RMMST.COMPANY_CODE=HMST.COMPANY_CODE" & vbCrLf _
                            & " AND TRIM(RMMST.PARENT_CODE)=TRIM(HMST.ITEM_CODE) AND RMMST.ITEM_CODE = '" & Trim(xItemCode) & "'"

                        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                    End If

                    mMultiItemCode = False
                    mInConUnit = 1
                    Dim mBOMFound As Boolean = False
                    If RsTemp.EOF = False Then
                        Do While RsTemp.EOF = False
                            'mMKEY = IIf(IsDBNull(RsTemp.Fields("mKey").Value), "", RsTemp.Fields("mKey").Value)
                            mCheckOutItem = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))
                            If Trim(mCheckOutItem) = Trim(xOutItemCode) Then
                                mOutConUnit = Val(IIf(IsDBNull(RsTemp.Fields("STD_QTY").Value), 0, RsTemp.Fields("STD_QTY").Value))
                                If mOutConUnit = 0 Then
                                    MsgInformation("Please check BOM of Item Code : " & xItemCode)
                                    CollectPOData = False
                                    Exit Function
                                End If
                                mBOMFound = True
                                Exit Do
                            End If
                            RsTemp.MoveNext()
                            'If RsTemp.EOF = False Then
                            '    mMultiItemCode = True
                            '    Exit Do
                            'End If

                        Loop
                        If mBOMFound = False Then
                            MsgInformation("Please check BOM of Item Code : " & xItemCode)
                            CollectPOData = False
                            Exit Function
                        End If
                        RsTemp.MoveFirst()
                    Else
                        If xItemCode = xOutItemCode Then
                            mOutConUnit = 1
                        Else
                            CollectPOData = False
                            Exit Function
                        End If
                    End If

                    'If mMultiItemCode = False Then
                    '    If mCheckOutItem <> "" Then
                    '        If Trim(mCheckOutItem) = Trim(xOutItemCode) Then
                    '            mOutConUnit = IIf(IsDBNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)
                    '        Else
                    '            SqlStr = "SELECT ALTER_ITEM_CODE, ALTER_ITEM_QTY " & vbCrLf & " FROM PRD_OUTBOM_ALTER_DET " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MKEY='" & mMKEY & "'" & vbCrLf & " AND PRODUCT_CODE='" & xItemCode & "'" & vbCrLf & " AND ALTER_ITEM_CODE='" & xOutItemCode & "'"

                    '            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                    '            If RsTemp.EOF = False Then
                    '                mOutConUnit = IIf(IsDBNull(RsTemp.Fields("ALTER_ITEM_QTY").Value), 0, RsTemp.Fields("ALTER_ITEM_QTY").Value)
                    '            Else
                    '                CollectPOData = False
                    '                Exit Function
                    '            End If
                    '        End If
                    '    End If
                    'Else
                    '    CollectPOData = True
                    '    Exit Function
                    'End If
                End If

                SqlStr = " SELECT POD.INWARD_ITEM_CODE, POD.ITEM_UOM, SUM(ITEM_QTY) AS ITEM_QTY,  " & vbCrLf _
                    & " MAX(ITEM_RATE) AS ORATE,"


                SqlStr = SqlStr & vbCrLf _
                    & " NVL((SELECT MAX(((NVL(ITEM_PRICE, 0) - ROUND((NVL(ITEM_PRICE,0) * ITEM_DIS_PER)/100,4))) * EXCHANGERATE) " & vbCrLf _
                    & " From PUR_PURCHASE_HDR PH, PUR_PURCHASE_DET PD " & vbCrLf _
                    & " Where PH.COMPANY_CODE = PD.COMPANY_CODE And PH.MKEY = PD.MKEY And PD.ITEM_CODE = POD.INWARD_ITEM_CODE " & vbCrLf _
                    & " And PD.MKEY =(SELECT MAX(SPH.MKEY) " & vbCrLf _
                    & " From PUR_PURCHASE_HDR SPH, PUR_PURCHASE_DET SPD " & vbCrLf _
                    & " Where SPH.MKEY = SPD.MKEY " & vbCrLf _
                    & " And SPH.AUTO_KEY_PO = POD.AUTO_KEY_WO " & vbCrLf _
                    & " And SPD.ITEM_CODE= POD.INWARD_ITEM_CODE " & vbCrLf _
                    & " And PO_STATUS='Y')), POD.ITEM_RATE) AS ITEM_RATE, "

                '' & "     --And SPD.PO_WEF_DATE <=  POD.GATEPASS_DATE " & vbCrLf _

                SqlStr = SqlStr & vbCrLf _
                    & " AC.SUPP_CUST_NAME as SuppName " & vbCrLf _
                    & " FROM INV_GATEPASS_HDR POM,INV_GATEPASS_DET POD,FIN_SUPP_CUST_MST AC " & vbCrLf _
                    & " WHERE POM.AUTO_KEY_PASSNO = POD.AUTO_KEY_PASSNO " & vbCrLf & " And POM.Company_Code = AC.Company_Code " & vbCrLf _
                    & " And POM.SUPP_CUST_CODE = AC.SUPP_CUST_CODE " & vbCrLf & " And POM.AUTO_KEY_PASSNO=" & Val(xPoNo) & " " & vbCrLf _
                    & " And POM.SUPP_CUST_CODE='" & pSupplierCode & "' " & vbCrLf & " And POM.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " and POM.GATEPASS_STATUS='N' AND POD.ITEM_CODE = '" & Trim(xOutItemCode) & "'" & vbCrLf & " GROUP BY POD.INWARD_ITEM_CODE,POD.ITEM_UOM,AC.SUPP_CUST_NAME, POD.AUTO_KEY_WO,POD.ITEM_RATE"

            Case "I", "1", "2", "3"
                SqlStr = " SELECT POM.INVOICE_DATE, " & vbCrLf & " POD.ITEM_CODE, POD.ITEM_UOM, SUM(POD.ITEM_QTY) As ITEM_QTY, MAX(POD.ITEM_RATE) AS ITEM_RATE, " & vbCrLf & " AC.SUPP_CUST_NAME as SuppName " & vbCrLf & " FROM FIN_INVOICE_HDR POM,FIN_INVOICE_DET POD,FIN_SUPP_CUST_MST AC " & vbCrLf & " WHERE POM.MKEY = POD.MKEY " & vbCrLf & " And POM.Company_Code = AC.Company_Code " & vbCrLf & " And POM.SUPP_CUST_CODE = AC.SUPP_CUST_CODE " & vbCrLf & " And POM.AUTO_KEY_INVOICE='" & xPoNo & "' " & vbCrLf & " And (POM.SUPP_CUST_CODE='" & pSupplierCode & "' OR POM.BUYER_CODE='" & pSupplierCode & "' OR POM.CO_BUYER_CODE='" & pSupplierCode & "')" & vbCrLf & " And POM.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " and POM.CANCELLED='N' AND POD.ITEM_CODE='" & Trim(xItemCode) & "'" & vbCrLf & " GROUP BY POM.INVOICE_DATE,POD.ITEM_CODE,POD.ITEM_UOM,AC.SUPP_CUST_NAME"
            Case Else
                CollectPOData = True
                Exit Function
        End Select

        If SqlStr = "" Then Exit Function

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPO, ADODB.LockTypeEnum.adLockReadOnly)

        If RsPO.EOF = False Then
            If VB.Left(cboRefType.Text, 1) = "R" Then
                If mMultiItemCode = False Then
                    mInwardItemCode = IIf(IsDBNull(RsPO.Fields("INWARD_ITEM_CODE").Value), "", RsPO.Fields("INWARD_ITEM_CODE").Value)
                    'If mInwardItemCode <> "" Then
                    '    FillInwardRGPDetailPart(RsPO, Val(xPoNo), mRowNo)
                    'Else
                    '    '                FillPODetailPart RsPO, Val(xPoNo), mRowNo						
                    FillRGPDetailPart(RsPO, Val(xPoNo), mRowNo, Trim(xItemCode), Trim(xOutItemCode), mInConUnit, mOutConUnit, pSupplierCode)
                    'End If
                End If
            Else
                FillPODetailPart(RsPO, CStr(Val(xPoNo)), mRowNo)
            End If
            CollectPOData = True
        Else
            CollectPOData = False
        End If
        CalcTots()
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        CollectPOData = False
    End Function

    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String

        mSqlStr = mSqlStr & "SELECT * " & " FROM Temp_PO " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY AUTO_KEY_PO,AMEND_NO"

        FetchRecordForReport = mSqlStr

    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByVal mMode As Crystal.DestinationConstants, ByVal mTitle As String, ByVal mSubTitle As String, ByVal mRptFileName As String)
        'Dim Printer As New Printer						
        On Error GoTo ErrPart
        Dim mAmountInword As String


        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        Report1.ReportFileName = PubReportFolderPath & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        'Dim prt As Printer						
        'If PubUniversalPrinter = "Y" And mMode = Crystal.DestinationConstants.crptToPrinter Then						
        '    For Each prt In Printers						
        '        If UCase(prt.DeviceName) = UCase("Universal Printer") Then						
        '            Printer = prt						

        '            Report1.PrinterName = prt.DeviceName						
        '            Report1.PrinterDriver = prt.DriverName						
        '            Report1.PrinterPort = prt.Port						
        '            Report1.PrinterSelect()						
        '            Exit For						
        '        End If						
        '    Next prt						
        'End If						

        Report1.Action = 1
        Report1.Reset()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub txtSupplier_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtSupplier.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSupplier_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtSupplier.DoubleClick
        cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub

    Private Sub txtSupplier_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtSupplier.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtSupplier.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtRemarks.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtRemarks_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtRemarks.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtRemarks.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtST38No_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtST38No.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtST38No_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtST38No.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtST38No.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        'With SprdMain
        '    '        SprdMain_LeaveCell .ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False						
        'End With
    End Sub
    Private Sub txtBillDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBillDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBillNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBillNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBillNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBillNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        '    KeyAscii = MainClass.SetNumericField(KeyAscii)						
        KeyAscii = MainClass.UpperCase(KeyAscii, txtBillNo.Text, "Y", "N")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtScanning_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtScanning.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtScanning_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtScanning.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtScanning.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub SprdExp_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdExp.ClickEvent

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        Dim mPONo As Double

        mCol = SprdMain.ActiveCol

        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemName Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemName, 0))

        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColPONo Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColPONo, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColStockType Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColStockType, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColQCEMP Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColQCEMP, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColCT3No Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColCT3No, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColPCNo Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColPCNo, 0))

        SprdMain.Refresh()



        mCol = SprdMain.ActiveCol
        If eventArgs.keyCode = System.Windows.Forms.Keys.F3 And mCol = ColPONo And SprdMain.ActiveRow > 1 And VB.Left(cboRefType.Text, 1) <> "R" Then
            SprdMain.Row = SprdMain.ActiveRow - 1
            SprdMain.Col = ColPONo
            mPONo = Val(SprdMain.Text)

            SprdMain.Row = SprdMain.ActiveRow
            SprdMain.Col = ColPONo
            SprdMain.Text = CStr(mPONo)

        End If
        ''SprdMain_Click ColItemName, 0						

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

                    SqlStr = "Select * From FIN_INTERFACE_MST Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND Name= '" & m_Exp & "'"
                    If CDate(txtMRRDate.Text) >= CDate(PubGSTApplicableDate) Then ' If PubGSTApplicable = True Then						
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

        End Select
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
    Private Sub FillCombo()

        On Error GoTo FillErr2
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing



        cboDivision.Items.Clear()

        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        SqlStr = SqlStr & vbCrLf & " ORDER BY DIV_DESC"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboDivision.SelectedIndex = -1

        cboRefType.Items.Clear()
        cboRefType.Items.Add("Purchase Order")
        cboRefType.Items.Add("Job Work Order(3rd Party)")
        cboRefType.Items.Add("Invoice-Sale Return")
        cboRefType.Items.Add("Free of Cost")
        cboRefType.Items.Add("Returnable Gate Pass")
        cboRefType.Items.Add("Cash Purchase")
        cboRefType.Items.Add("1 - Job Work Rejection")
        cboRefType.Items.Add("2 - Sale Return Under Warranty")
        cboRefType.Items.Add("3 - Sale Return RM/BOP")


        cboMode.Items.Clear()
        cboMode.Items.Add("1. BY HAND")
        cboMode.Items.Add("2. BY COMPANY VEHICLE")
        cboMode.Items.Add("3. BY PARTY VEHICLE")
        cboMode.Items.Add("4. BY TRANSPOTER")
        cboMode.Items.Add("5. BY AIR")
        cboMode.Items.Add("6. BY CARGO")
        cboMode.Items.Add("7. BY COURIER / POSTAL")
        cboMode.SelectedIndex = -1

        Exit Sub
FillErr2:
        MsgBox(Err.Description)
    End Sub
    Private Sub FrmMRR_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection						
        'PvtDBCn.Open StrConn						

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        pXRIGHT = XRIGHT
        xMyMenu = myMenu
        MainClass.RightsToButton(Me, XRIGHT)

        '    If InStr(1, XRIGHT, "D", vbTextCompare) > 1 Then						
        '        chkCancelled.Enabled = True						
        '    Else						
        chkCancelled.Enabled = False
        '    End If						

        CurrFormHeight = 7245
        CurrFormWidth = 11355

        MainClass.SetControlsColor(Me)
        mAuthorised = IIf(InStr(1, XRIGHT, "S") > 0, True, False)

        ADDMode = False
        MODIFYMode = False
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245) '8000						
        ''Me.Width = VB6.TwipsToPixelsX(11355) '11900						


        Call FillCombo()

        'AdataItem.Visible = False
        FraDetail.Visible = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = False
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub FrmMRR_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    'Private Sub Form_KeyUp(KeyCode As Integer, Shift As Integer)						
    '    MainClass.DoFunctionKey Me, KeyCode						
    'End Sub						
    Private Sub FillSprdExp()

        On Error GoTo ERR1
        Dim mLocal As String
        Dim SqlStr As String = ""

        Dim RS As ADODB.Recordset = Nothing
        Dim I As Integer
        pShowCalc = False
        MainClass.ClearGrid(SprdExp)

        If Trim(TxtSupplier.Text) <> "" Then
            mLocal = GetPartyBusinessDetail(Trim(TxtSupplier.Text), Trim(txtBillTo.Text), "WITHIN_STATE")
            mLocal = IIf(mLocal = "Y", "L", "C")
            'If MainClass.ValidateWithMasterTable(TxtSupplier.Text, "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            '    mLocal = IIf(MasterNo = "Y", "L", "C")
            'Else
            '    mLocal = ""
            'End If
        Else
            mLocal = ""
        End If



        SqlStr = "Select * From FIN_INTERFACE_MST " & vbCrLf & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND (Type='P' OR Type='B') "

        If CDate(txtMRRDate.Text) >= CDate(PubGSTApplicableDate) Then 'If PubGSTApplicable = True Then						
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
        FormatSprdExp(-1)
        pShowCalc = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume						
    End Sub
    Private Sub Clear1()

        pShowCalc = False
        LblMkey.Text = ""
        CboPONo.Enabled = True
        cboRefType.Enabled = True

        cboDivision.Enabled = True
        cboDivision.SelectedIndex = -1

        SSTab1.SelectedIndex = 0

        mSupplierCode = CStr(-1)
        txtMRRNo.Text = ""
        txtMRRDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        lblEntryDate.Text = VB6.Format(PubCurrDate, "DD/MM/YYYY") & " " & GetServerTime()
        chkCancelled.Enabled = False
        fraFreight.Enabled = IIf(lblBookType.Text = "G", True, False)

        cboRefType.SelectedIndex = 0
        CboPONo.Items.Clear()
        chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked
        TxtSupplier.Text = ""
        txtBillTo.Text = ""
        txtShippedTo.Text = ""
        TxtShipTo.Text = ""
        txtBillNo.Text = ""
        txtBillDate.Text = "" 'VB6.Format(RunDate, "DD/MM/YYYY")						
        txtST38No.Text = ""
        txtEwayBillNo.Text = ""
        TxtItemDesc.Text = ""
        TxtTransporter.Text = ""
        txtFreight.Text = ""
        TxtRemarks.Text = ""
        txtFormDetail.Text = ""
        txtScanning.Text = ""

        OptFreight(0).Checked = True
        cboMode.SelectedIndex = -1

        txtDocsThru.Text = ""
        txtVehicle.Text = ""
        txtGRNo.Text = ""
        txtGRDate.Text = ""

        txtTripNo.Text = ""
        txtTripDate.Text = ""

        txtMRRDate.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)
        txtBillDate.Enabled = True
        TxtSupplier.Enabled = True
        cmdsearch.Enabled = True

        chkMrrSend.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkPacking.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkFOC.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkScheRej.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkDNote.CheckState = System.Windows.Forms.CheckState.Unchecked
        txtSendDate.Text = ""
        chkQC.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkExciseStatus.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkGSTStatus.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkSTStatus.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkServiceTaxClaim.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkBillPassing.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkRejRtn.CheckState = System.Windows.Forms.CheckState.Unchecked

        chkPrimiumFreight.Enabled = False
        chkPrimiumFreight.CheckState = System.Windows.Forms.CheckState.Unchecked

        lblAddUser.Text = ""
        lblAddDate.Text = ""
        lblModUser.Text = ""
        lblModDate.Text = ""

        lblTotQty.Text = VB6.Format(0, "#0.00")
        lblTotItemValue.Text = VB6.Format(0, "#0.00")
        lblCGST.Text = VB6.Format(0, "#0.00")
        lblEDUAmount.Text = VB6.Format(0, "#0.00")
        lblSGST.Text = VB6.Format(0, "#0.00")
        lblNetAmount.Text = VB6.Format(0, "#0.00")
        lblDiscount.Text = VB6.Format(0, "#0.00")
        lblSurcharge.Text = VB6.Format(0, "#0.00")
        lblRO.Text = VB6.Format(0, "#0.00")
        lblMSC.Text = VB6.Format(0, "#0.00")
        lblTotCharges.Text = VB6.Format(0, "#0.00")
        lblTotExpAmt.Text = VB6.Format(0, "#0.00")
        lblSTPercentage.Text = VB6.Format(0, "#0.00")
        lblEDPercentage.Text = VB6.Format(0, "#0.00")
        lblEDUPercent.Text = VB6.Format(0, "#0.00")
        lblTotFreight.Text = VB6.Format(0, "#0.00")
        lblTotTaxableAmt.Text = VB6.Format(0, "#0.00")
        pQCDate = ""
        mWithOutOrder = False
        FraDetail.Visible = False

        If lblBookType.Text = "Q" Then
            chkQC.Enabled = True
        End If

        txtGateNo.Text = ""
        txtGateDate.Text = ""
        cmdResetMRR.Enabled = False


        chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked
        txtShippedTo.Enabled = False
        cmdSearchShippedTo.Enabled = False
        chkUnderChallan.CheckState = System.Windows.Forms.CheckState.Unchecked

        txtShippedTo.Text = ""
        TxtShipTo.Text = ""
        txtDeliveryTo.Text = ""
        txtDeliveryToLoc.Text = ""

        chkTCAvailable.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkTCAvailable.Enabled = False
        txtTCPath.Text = ""
        cmdTC.Enabled = False

        chkTPRAvailable.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkTPRAvailable.Enabled = False
        txtTPRPath.Text = ""
        cmdTPRI.Enabled = False

        pTempUpdate = False

        If RsCompany.Fields("MRR_AGT_GE").Value = "Y" Then
            TxtSupplier.Enabled = False
            cmdsearch.Enabled = False
            txtBillNo.Enabled = False
            txtBillDate.Enabled = False
            txtST38No.Enabled = True
            cboRefType.Enabled = False
            cboDivision.Enabled = False
            txtGateNo.Enabled = IIf(lblBookType.Text = "Q", False, True)
            txtGateDate.Enabled = False
            cmdGateSearch.Enabled = IIf(lblBookType.Text = "Q", False, True)
            chkShipTo.Enabled = False
            txtShippedTo.Enabled = False
            chkUnderChallan.Enabled = False
            txtBillTo.Enabled = False
            TxtShipTo.Enabled = False
        Else
            txtGateNo.Enabled = False
            txtGateDate.Enabled = False
            cmdGateSearch.Enabled = False
            chkShipTo.Enabled = True
            chkUnderChallan.Enabled = True
            txtBillTo.Enabled = True
            TxtShipTo.Enabled = False
            cboRefType.Enabled = True
            FraPO.Enabled = True
        End If



        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)
        MainClass.ClearGrid(SprdExp)
        Call FillSprdExp()
        pShowCalc = True
        MainClass.ButtonStatus(Me, XRIGHT, RsMRRMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub FrmMRR_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 210, mReFormWidth - 210, mReFormWidth))
        FraFront.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        Frasprd.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        SSTab1.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        UltraGrid1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtBillTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBillTo.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtBillTo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBillTo.DoubleClick
        Call cmdBillToSearch()
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdBillToSearch()
    End Sub

    Private Sub CboPONo_KeyUp(sender As Object, e As KeyEventArgs) Handles CboPONo.KeyUp
        If e.KeyCode = System.Windows.Forms.Keys.F1 Then SearchPONo()
    End Sub

    Private Sub CboPONo_DoubleClick(sender As Object, e As EventArgs) Handles CboPONo.DoubleClick
        SearchPONo()
    End Sub

    Private Sub CboPONo_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles CboPONo.MouseDoubleClick
        SearchPONo()
    End Sub
    Private Sub SearchPONo()

        Dim xIName As String
        Dim xPoNo As String
        Dim SqlStr As String = ""
        Dim xSuppCode As String
        Dim xRefNo As String
        Dim xRGPCode As String
        Dim xItemCode As String = ""
        'Dim mCT3No As Integer
        'Dim mFromMRRDate As String
        Dim mDivisionCode As Double
        Dim mSchdDate As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim CntRow As Long
        Dim mCheckString As String = ""
        mSchdDate = "01/" & VB6.Format(txtMRRDate.Text, "MM/YYYY")

        If Trim(txtBillDate.Text) = "" Then
            If txtBillDate.Enabled = True Then txtBillDate.Focus()
            MsgInformation("Please Enter Bill Date.")
            Exit Sub
        End If

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        If VB.Left(cboRefType.Text, 1) = "F" Or VB.Left(cboRefType.Text, 1) = "C" Or VB.Left(cboRefType.Text, 1) = "J" Then ''Or Left(cboRefType, 1) = "1"
            mWithOutOrder = True
        Else
            mWithOutOrder = False
        End If

        xRefNo = Trim(CboPONo.Text)

        If VB.Left(cboRefType.Text, 1) = "P" And xRefNo <> "" Then
            If MainClass.ValidateWithMasterTable(xRefNo, "AUTO_KEY_PO", "PUR_TYPE", "PUR_PURCHASE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DIV_CODE=" & mDivisionCode & " AND PUR_TYPE IN ('P','R','L')") = True Then
                If MasterNo = "R" Then
                    mIsProjectPO = True
                Else
                    mIsProjectPO = False
                End If
            End If
        Else
            mIsProjectPO = False
        End If

        xPoNo = ""

        'If IsDate(txtMRRDate.Text) Then
        '    mFromMRRDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -365, CDate(txtMRRDate.Text)))
        'End If

        SqlStr = SearchPOQuery(mDivisionCode, mWithOutOrder, mIsProjectPO, mSchdDate, VB.Left(cboRefType.Text, 1), xPoNo)

        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            CboPONo.Text = AcName
            xPoNo = AcName
            SqlStr = SearchPOQuery(mDivisionCode, mWithOutOrder, mIsProjectPO, mSchdDate, VB.Left(cboRefType.Text, 1), xPoNo)
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                'MainClass.ClearGrid(SprdMain)
                'Call FormatSprdMain(-1)
                'MainClass.ClearGrid(SprdExp)
                'Call FillSprdExp()
                CntRow = SprdMain.MaxRows
                'With SprdMain
                '    For CntRow = 1 To .MaxRows

                '    Next
                'End With
                Do While RsTemp.EOF = False
                    With SprdMain
                        .Row = CntRow
                        .Col = ColPONo
                        .Text = IIf(IsDBNull(RsTemp.Fields("PONO").Value), "", RsTemp.Fields("PONO").Value)

                        If VB.Left(cboRefType.Text, 1) = "R" Then
                            .Col = ColRGPItemCode
                            .Text = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)
                        Else
                            .Col = ColPODate
                            .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("PODATE").Value), "", RsTemp.Fields("PODATE").Value), "DD/MM/YYYY")
                        End If
                        .Text = AcName1

                        If VB.Left(cboRefType.Text, 1) = "R" Then

                            .Col = ColPODate
                            .Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("RGP_DATE").Value), "", RsTemp.Fields("RGP_DATE").Value), "DD/MM/YYYY")

                            '.Col = ColRGPQty
                            '.Text = AcName3
                            SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColPONo, CntRow, ColPONo, CntRow, True))
                        ElseIf VB.Left(cboRefType.Text, 1) = "P" Then


                            mCheckString = xPoNo & IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)

                            If ItemCodeAlreadyExist(mCheckString) = True Then GoTo NextRed
                            .Col = ColItemCode
                            .Text = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)

                            .Col = ColItemName
                            .Text = IIf(IsDBNull(RsTemp.Fields("ITEM_DESC").Value), "", RsTemp.Fields("ITEM_DESC").Value)


                            SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColItemCode, CntRow, ColItemCode, CntRow, True))
                        Else

                            mCheckString = xPoNo & IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)
                            If ItemCodeAlreadyExist(mCheckString) = True Then GoTo NextRed
                            .Col = ColItemCode
                            .Text = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)

                            .Col = ColItemName
                            .Text = IIf(IsDBNull(RsTemp.Fields("ITEM_DESC").Value), "", RsTemp.Fields("ITEM_DESC").Value)

                            SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColItemCode, CntRow, ColItemCode, CntRow, True))
                        End If
                    End With
                    CntRow = CntRow + 1
                    SprdMain.MaxRows = CntRow
NextRed:
                    RsTemp.MoveNext()
                Loop
            End If
        End If
        MainClass.SetFocusToCell(SprdMain, 1, ColItemCode)
        CalcTots()
    End Sub
    Private Function SearchPOQuery(ByVal mDivisionCode As Double, ByVal mWithOutOrder As Boolean, ByVal mIsProjectPO As Boolean,
                                   ByVal mSchdDate As String, ByVal pRefType As String, ByVal pPONo As String) As String

        Dim SqlStr As String = ""
        Dim xSuppCode As String

        SearchPOQuery = ""


        Select Case VB.Left(cboRefType.Text, 1)
            Case "P"
                'AS PONO, AS PODATE, AS PO_WEF_DATE, AS ITEM_CODE,AS ITEM_DESC,AS BAL_QTY,AS OLD_ERP_PO, AS GROUP_ITEM_CODE

                SqlStr = "SELECT DISTINCT POMain.AUTO_KEY_PO AS PONO , " & vbCrLf _
                    & " POMain.PUR_ORD_DATE AS PODATE, PODetail.PO_WEF_DATE AS PO_WEF_DATE, PODetail.ITEM_CODE AS ITEM_CODE, INV.ITEM_SHORT_DESC AS ITEM_DESC, " & vbCrLf _
                    & " CASE WHEN ORDER_TYPE='C' " & vbCrLf _
                    & " THEN PODetail.ITEM_QTY-GETMRRQTYFORPO(POMain.Company_Code, POMain.AUTO_KEY_PO, POMain.SUPP_CUST_CODE, PODetail.ITEM_CODE) ELSE " & vbCrLf _
                    & " GetSupplierMonScheduleQty(POMain.Company_Code, POMain.AUTO_KEY_PO, POMain.SUPP_CUST_CODE, PODetail.ITEM_CODE,TO_DATE('" & VB6.Format(mSchdDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))-GETMRRQTYFORPO(POMain.Company_Code, POMain.AUTO_KEY_PO, POMain.SUPP_CUST_CODE, PODetail.ITEM_CODE) END AS BAL_QTY,ITEM_PRICE,NAV_PO_NO AS OLD_ERP_PO,GROUP_ITEM_CODE " & vbCrLf _
                    & " FROM PUR_PURCHASE_HDR POMain,PUR_PURCHASE_DET PODetail, INV_ITEM_MST INV" & vbCrLf _
                    & " WHERE POMain.MKEY=PODetail.MKEY " & vbCrLf _
                    & " And POMain.Company_Code=INV.Company_Code And PODetail.ITEM_CODE=INV.ITEM_CODE" & vbCrLf _
                    & " And POMain.BILL_TO_LOC_ID='" & Trim(txtBillTo.Text) & "'" & vbCrLf _
                    & " And POMain.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " And PUR_TYPE IN ('P','R','L')"

                If IsDate(txtBillDate.Text) Then
                    SqlStr = SqlStr & vbCrLf & " AND PODetail.PO_WEF_DATE<=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                End If

                If RsCompany.Fields("PO_IN_GE").Value = "Y" Then
                    If ADDMode = True Then
                        SqlStr = SqlStr & vbCrLf & " AND POMain.PO_STATUS='Y' AND POMain.PO_CLOSED='N'"
                    End If
                End If

                'If chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked Then
                If Trim(TxtSupplier.Text) <> "" Then
                    If MainClass.ValidateWithMasterTable((TxtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        xSuppCode = MasterNo
                        SqlStr = SqlStr & vbCrLf & " AND  POMain.SUPP_CUST_CODE='" & xSuppCode & "'"
                    End If
                End If
                'Else
                '    If Trim(txtShippedTo.Text) <> "" Then
                '        If MainClass.ValidateWithMasterTable((txtShippedTo.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '            xSuppCode = MasterNo
                '            SqlStr = SqlStr & vbCrLf & " AND  POMain.SUPP_CUST_CODE='" & xSuppCode & "'"
                '        End If
                '    End If
                'End If

                SqlStr = SqlStr & vbCrLf & " AND DIV_CODE=" & mDivisionCode & ""

                If RsCompany.Fields("FYEAR").Value < ConOPENPO_CONTINOUS_YEAR Then
                    SqlStr = SqlStr & vbCrLf & " AND POMain.ORDER_TYPE NOT IN " & vbCrLf & " CASE WHEN SUBSTR(POMain.AUTO_KEY_PO,LENGTH(POMain.AUTO_KEY_PO)-5,4)<" & RsCompany.Fields("FYEAR").Value & " THEN ('O') ELSE ('-1') END"
                Else
                    SqlStr = SqlStr & vbCrLf & " AND SUBSTR(POMain.AUTO_KEY_PO,LENGTH(POMain.AUTO_KEY_PO)-5,4)>= " & vbCrLf & " CASE WHEN POMain.ORDER_TYPE='O' THEN " & ConOPENPO_CONTINOUS_YEAR & " ELSE 1 END"
                End If
                'AND POMain.PO_STATUS='Y'

                If ADDMode = True Then
                    SqlStr = SqlStr & vbCrLf & " AND POMain.PO_CLOSED='N'"
                End If

                SqlStr = SqlStr & vbCrLf & " AND CASE WHEN ORDER_TYPE='C' THEN PODetail.ITEM_QTY-GETMRRQTYFORPO(POMain.Company_Code, POMain.AUTO_KEY_PO, POMain.SUPP_CUST_CODE, PODetail.ITEM_CODE) ELSE 1 END >0 "

                SqlStr = SqlStr & vbCrLf & "  AND PO_ITEM_STATUS='N' "

                If pPONo <> "" Then
                    SqlStr = SqlStr & vbCrLf & "  AND POMain.AUTO_KEY_PO = '" & pPONo & "'"
                End If

                SqlStr = SqlStr & vbCrLf & " ORDER BY TO_NUMBER(POMain.AUTO_KEY_PO),POMain.PUR_ORD_DATE "

            Case "R"

                SqlStr = "SELECT DISTINCT TRN.RGP_NO AS PONO,  TRN.OUTWARD_ITEM_CODE AS ITEM_CODE, INVMST.ITEM_SHORT_DESC AS ITEM_DESC," & vbCrLf _
                    & " TO_CHAR(SUM(DECODE(TRN.ITEM_IO,'O',1,0) * TRN.RGP_QTY)) AS RGP_QTY, TO_CHAR(TRN.RGP_DATE,'DD/MM/YYYY') AS RGP_DATE, " & vbCrLf _
                    & " TO_CHAR(SUM(DECODE(TRN.ITEM_IO,'O',1,-1) * TRN.RGP_QTY)) AS BAL_QTY," & vbCrLf _
                    & " TRN.F4NO " & vbCrLf _
                    & " FROM INV_RGP_REG_TRN TRN, INV_ITEM_MST INVMST" & vbCrLf _
                    & " WHERE TRN.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND TRN.Company_Code=INVMST.Company_Code " & vbCrLf _
                    & " AND TRN.OUTWARD_ITEM_CODE=INVMST.ITEM_CODE "

                If Trim(TxtSupplier.Text) <> "" Then
                    If MainClass.ValidateWithMasterTable((TxtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        xSuppCode = MasterNo
                        SqlStr = SqlStr & vbCrLf & " AND SUPP_CUST_CODE='" & xSuppCode & "'"
                    End If
                End If

                SqlStr = SqlStr & vbCrLf & " AND BILL_TO_LOC_ID='" & MainClass.AllowSingleQuote(txtBillTo.Text) & "'"

                '                SqlStr = SqlStr & vbCrLf & " AND DIV_CODE=" & mDivisionCode & ""

                'SqlStr = SqlStr & vbCrLf & " AND RGP_NO Like '" & pPONo & "%'"
                If pPONo <> "" Then
                    SqlStr = SqlStr & vbCrLf & "  AND RGP_NO = '" & pPONo & "'"
                End If

                'If Val(txtMRRNo.Text) <> 0 Then
                '    SqlStr = SqlStr & vbCrLf & " AND REF_NO<>" & Val(txtMRRNo.Text) & ""
                'End If

                SqlStr = SqlStr & vbCrLf & " AND TRN.REF_NO NOT IN (" & vbCrLf _
                        & " SELECT REF_NO FROM INV_RGP_REG_TRN " & vbCrLf _
                        & " WHERE " & vbCrLf _
                        & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(UCase(xSuppCode)) & "'" & vbCrLf _
                        & " AND BILL_NO='" & txtBillNo.Text & "' AND  BILL_DATE =TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND BOOKTYPE='M' AND ITEM_IO='I')"

                If IsDate(txtMRRDate.Text) Then
                    SqlStr = SqlStr & vbCrLf & " AND RGP_DATE<=TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                End If


                SqlStr = SqlStr & vbCrLf & " HAVING SUM(DECODE(ITEM_IO,'O',1,-1) * RGP_QTY)>0 "

                SqlStr = SqlStr & vbCrLf & " GROUP BY TRN.RGP_NO,  TRN.OUTWARD_ITEM_CODE, INVMST.ITEM_SHORT_DESC,RGP_DATE,F4NO "

                SqlStr = SqlStr & vbCrLf & " ORDER BY RGP_DATE, RGP_NO "

            Case "I", "1", "2", "3"


                SqlStr = "SELECT DISTINCT IH.AUTO_KEY_INVOICE AS PONO ,IH.INVOICE_DATE AS PO_WEF_DATE, ID.ITEM_CODE, ID.ITEM_QTY AS BAL_QTY, ID.ITEM_DESC, IH.BILLNO " & vbCrLf _
                    & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID" & vbCrLf _
                    & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
                    & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND IH.BILL_TO_LOC_ID='" & Trim(txtBillTo.Text) & "'" & vbCrLf

                If Trim(TxtSupplier.Text) <> "" Then
                    If MainClass.ValidateWithMasterTable((TxtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        xSuppCode = MasterNo
                        SqlStr = SqlStr & vbCrLf & " AND IH.SUPP_CUST_CODE='" & xSuppCode & "'"
                    End If
                End If

                If IsDate(txtMRRDate.Text) Then
                    'mFromMRRDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -365, CDate(txtMRRDate.Text)))
                    'SqlStr = SqlStr & vbCrLf & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(mFromMRRDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                    SqlStr = SqlStr & vbCrLf & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                End If

                If pPONo <> "" Then
                    SqlStr = SqlStr & vbCrLf & "  AND IH.AUTO_KEY_INVOICE = '" & pPONo & "'"
                End If

                SqlStr = SqlStr & vbCrLf & " ORDER BY IH.INVOICE_DATE, IH.AUTO_KEY_INVOICE, ID.ITEM_CODE "
        End Select

        SearchPOQuery = SqlStr
    End Function
    Private Function ItemCodeAlreadyExist(ByRef mCheckItemCode As String) As Boolean

        Dim CntRow As Integer
        Dim mCount As Byte
        'Dim mCheckItemCode As String
        Dim mItemCode As String
        Dim mPONo As Double
        Dim xCheckCode As String
        Dim mRGPCode As String
        'Dim mCheckRGPCode As String

        ItemCodeAlreadyExist = False
        With SprdMain
            mCount = 0
            For CntRow = 1 To .MaxRows
                .Row = CntRow

                .Col = ColPONo
                mPONo = Val(.Text)

                .Col = ColItemCode
                mItemCode = Trim(UCase(.Text))

                xCheckCode = mPONo & mItemCode

                If VB.Left(cboRefType.Text, 1) = "R" Then
                    .Col = ColRGPItemCode
                    mRGPCode = Trim(UCase(.Text))
                    xCheckCode = xCheckCode & mRGPCode
                End If

                If (xCheckCode = mCheckItemCode And mCheckItemCode <> "") Then
                    mCount = mCount + 1
                End If

                If mCount >= 1 Then
                    ItemCodeAlreadyExist = True
                    Exit Function
                End If
            Next
        End With
    End Function

    Public Function GetItemDescription(ByRef xRGPNo As Double, ByRef ItemCode As String) As String
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCatPreFix As String = "'"
        Dim mSubCatPreFix As String = ""
        Dim mDescription As String = ""
        Dim mItemPrefix As String = ""
        Dim mMaxCode As String = ""

        Dim mSuppCustCode As String = ""

        Dim mWONo As Double


        If MainClass.ValidateWithMasterTable(xRGPNo, "AUTO_KEY_PASSNO", "AUTO_KEY_WO", "INV_GATEPASS_DET", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE='" & ItemCode & "' AND ITEM_CODE='" & ItemCode & "'") = True Then
            mWONo = MasterNo
        End If


        SqlStr = "SELECT NVL(WO_DESCRIPTION, '') AS DESCRIPTION FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID " & vbCrLf _
            & " Where IH.MKEY=ID.MKEY " & vbCrLf _
            & " AND IH.AUTO_KEY_PO=" & mWONo & " " & vbCrLf _
            & " AND ITEM_CODE='" & ItemCode & "' AND IH.PO_CLOSED='N' and IH.PO_STATUS='Y' Order By SERIAL_NO"

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
