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

Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Imports System.Data.OleDb
Imports System.ComponentModel

Imports System.Drawing
Imports System.Drawing.Printing

Imports System.Data
Imports System.IO
Imports System.Configuration


Friend Class FrmGateEntry
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
    Private Const ColItemPartNo As Short = 7
    Private Const ColHSNCode As Short = 8
    Private Const ColUnit As Short = 9
    Private Const ColHeatNo As Short = 10
    Private Const ColBatchNo As Short = 11
    Private Const ColPOQty As Short = 12
    Private Const ColBalQty As Short = 13
    Private Const ColBillQty As Short = 14
    Private Const ColPORate As Short = 15
    Private Const ColRate As Short = 16
    Private Const ColAmount As Short = 17
    Private Const ColItemCost As Short = 18
    Private Const ColQtyInKgs As Short = 19
    Private Const ColRemarks As Short = 20



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

    Private Function GetAutoIssueFromIndent(ByRef mPONo As String, ByRef mItemCode As String, ByRef mCheckedField As String, Optional ByRef mDeptCode As String = "") As String

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

    Private Function ValidatePO(ByRef mPONo As String, ByRef mDivisionCode As Double) As Boolean

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
            SqlStr = "SELECT AUTO_KEY_PO, PO_STATUS AS CLOSED,SUPP_CUST_CODE  from PUR_PURCHASE_HDR WHERE " & vbCrLf & " AUTO_KEY_PO=" & Val(mPONo) & "" & vbCrLf & " AND Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " ''& vbCrLf |                & " AND SUBSTR(AUTO_KEY_PO,LENGTH(AUTO_KEY_PO)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
        ElseIf mRefType = "I" Or mRefType = "2" Or mRefType = "3" Then
            SqlStr = ""
        ElseIf mRefType = "R" Then
            SqlStr = ""
            'ElseIf mRefType = "4" Then
            '    SqlStr = "SELECT MKEY,AUTO_KEY_INVOICE, STATUS AS CLOSED,SUPP_CUST_CODE  from FIN_INVOICE_HDR WHERE " & vbCrLf & " AUTO_KEY_INVOICE=" & Val(mPONo) & "" & vbCrLf & " AND Company_Code<>" & RsCompany.Fields("COMPANY_CODE").Value & " "
        End If

        If CDbl(mSupplierCode) <> -1 Then
            SqlStr = SqlStr & vbCrLf & " AND TRIM(SUPP_CUST_CODE)='" & MainClass.AllowSingleQuote(mSupplierCode) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND DIV_CODE=" & mDivisionCode & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        If RS.EOF = True Then
            ValidatePO = False
            If CDbl(mSupplierCode) = -1 Then MsgInformation("Either No Such PONo.(s) Or " & ErrMsg & " , Press F1 On PoNo.(s) For Help... ")
            If CDbl(mSupplierCode) <> -1 Then MsgInformation("PO No(s) Not Belong to Same Supplier")
        Else

            If RS.Fields("CLOSED").Value = "N" Then ValidatePO = False : MsgInformation("This PO Marked As CloseOrder, So Can Not Be Used For Further Transaction.")
            mSupplierCode = RS.Fields("SUPP_CUST_CODE").Value
        End If
        Exit Function
ERR1:
        ValidatePO = False
        MsgBox(Err.Description)
    End Function
    Private Function ValidateRGP(ByRef mPONo As String) As Boolean

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
    Private Function ValidateInvoice(ByRef mPONo As String) As Boolean

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


        SqlStr = "SELECT AUTO_KEY_INVOICE,CANCELLED AS CLOSED,SUPP_CUST_CODE" & vbCrLf & " FROM FIN_INVOICE_HDR WHERE " & vbCrLf & " AUTO_KEY_INVOICE='" & mPONo & "'" & vbCrLf & " AND Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " '' & vbCrLf |            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""


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

        MainClass.ButtonStatus(Me, XRIGHT, RsMRRMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
        Call FormatSprdMain(-1)
        '    MainClass.ClearGrid SprdExp
        '    Call FillSprdExp
        MainClass.ButtonStatus(Me, XRIGHT, RsMRRMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

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
        '            SqlStr = "SELECT GetITEMPRICE(TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & Val(txtPONo.Text) & ",'" & mITEM_CODE & "') AS PORATE  FROM DUAL"
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

    Private Sub chkQC_Click()

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkMRRMade_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkMRRMade.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkShipTo_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkShipTo.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
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

    Private Sub chkUnderChallan_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkUnderChallan.CheckStateChanged

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
        Else
            xAcctCode = MasterNo
        End If

        TxtShipTo.Text = GetDefaultLocation(xAcctCode)

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


    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click

        On Error GoTo AddErr
        If cmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            pShowCalc = True
            SprdMain.Enabled = True
            SprdExp.Enabled = True
            txtMRRNo.Enabled = True ' IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)
            txtMRRDate.Enabled = True ' IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)
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
        Dim pMRRMade As String

        If ValidateBranchLocking((txtBillDate.Text)) = True Then
            Exit Sub
        End If

        '    If lblBookType.text = "Q" Then
        '        mLockBookCode = ConLockMRRQC
        '    Else
        '        mLockBookCode = ConLockMRREntry
        '    End If

        If ValidateBookLocking(PubDBCn, mLockBookCode, txtMRRDate.Text) = True Then
            Exit Sub
        End If

        If ValidateAccountLocking(PubDBCn, txtBillDate.Text, (TxtSupplier.Text), mSupplierCode) = True Then
            Exit Sub
        End If

        pMRRMade = "N"
        If MainClass.ValidateWithMasterTable((txtMRRNo.Text), "AUTO_KEY_GATE", "MRR_MADE", "INV_GATEENTRY_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            pMRRMade = Trim(MasterNo)
        End If

        If pMRRMade = "Y" Then
            MsgInformation("MRR Made against this MRR, so Cann't be Deleted")
            Exit Sub
        End If


        If Trim(txtMRRNo.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub

        'delpart:
        If Not RsMRRMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "INV_GATEENTRY_HDR", (txtMRRNo.Text), RsMRRMain, "MRRNo") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "INV_GATEENTRY_HDR", "AUTO_KEY_GATE", (LblMkey.Text)) = False Then GoTo DelErrPart

                PubDBCn.Execute("DELETE FROM INV_RGP_REG_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND BILL_NO='" & txtBillNo.Text & "' AND  BILL_DATE =TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND  SUPP_CUST_CODE='" & mSupplierCode & "'  AND BOOKTYPE='M' AND ITEM_IO='I'")

                PubDBCn.Execute("Delete from INV_GATEENTRY_EXP Where AUTO_KEY_GATE='" & LblMkey.Text & "'")
                PubDBCn.Execute("Delete from INV_GATEENTRY_DET Where AUTO_KEY_GATE=" & Val(LblMkey.Text) & "")
                PubDBCn.Execute("Delete from INV_GATEENTRY_HDR Where AUTO_KEY_GATE=" & Val(LblMkey.Text) & "")

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

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdModify.Click

        On Error GoTo ModifyErr
        Dim pMRRMade As String

        If PubUserID <> "G0416" Then
            If PubSuperUser = "S" Or PubSuperUser = "A" Then
                If CheckOtherTransMade() = True Then
                    MsgInformation("Other Transaction Made, so Gate Entry Cann't be Modified")
                    Exit Sub
                End If
            Else
                pMRRMade = "N"
                If MainClass.ValidateWithMasterTable((txtMRRNo.Text), "AUTO_KEY_GATE", "MRR_MADE", "INV_GATEENTRY_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    pMRRMade = Trim(MasterNo)
                End If

                If pMRRMade = "Y" Then
                    MsgInformation("MRR Made, so Gate Entry Cann't be Modified")
                    Exit Sub
                End If
            End If
        End If

        TxtSupplier.Enabled = True
        txtBillTo.Enabled = True
        cmdsearch.Enabled = True
        cboRefType.Enabled = True
        cboDivision.Enabled = False ''iif(PubSuperUser="Y",true, False
        Frame1.Enabled = True
        txtMRRDate.Enabled = True

        If cmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsMRRMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
            SprdExp.Enabled = True
            txtMRRNo.Enabled = False
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
    Private Sub ReportONMRR(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""
        Dim SqlStr As String = ""
        Dim mRptFileName As String
        Dim mVNo As String

        frmPrintInvoice.OptInvoice.Text = "Gate Entry"
        frmPrintInvoice.OptInvoiceAnnex.Text = "GRN Label"
        frmPrintInvoice.OptInvoiceAnnex.Enabled = True
        frmPrintInvoice.OptInvoiceAnnex.Visible = True
        frmPrintInvoice.optSubsidiaryChallan.Enabled = False
        frmPrintInvoice.optSubsidiaryChallan.Visible = False
        frmPrintInvoice.FraF4.Enabled = False
        frmPrintInvoice.FraF4.Visible = False
        frmPrintInvoice.Opt4.Visible = False
        frmPrintInvoice.ShowDialog()





        If G_PrintLedg = False Then
            frmPrintInvoice.Close()
            Exit Sub
        End If


        If frmPrintInvoice.OptInvoice.Checked = True Then
            Report1.Reset()
            MainClass.ClearCRptFormulas(Report1)

            SqlStr = ""

            Call MainClass.ClearCRptFormulas(Report1)

            Call SelectQryForMRR(SqlStr)

            mTitle = "Material Gate Entry"
            mSubTitle = ""
            mRptFileName = "MGE.rpt"

            Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)
        Else
            mTitle = "Material Gate Entry"
            mSubTitle = ""
            mRptFileName = "GRNLABEL.rpt"
            Call ShowGRNLabelReport("", Mode, mTitle, mSubTitle, mRptFileName, False, False, "N")
        End If



        frmPrintInvoice.Close()

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub ReportONDiscrepancy(ByRef Mode As Crystal.DestinationConstants)

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

        mSqlStr = " SELECT " & vbCrLf & " IH.*, ID.*,INVMST.ITEM_SHORT_DESC, BCMST.*,  PREBY.EMP_NAME "

        'mSqlStr = mSqlStr & " CMST.SUPP_CUST_NAME, CMST.SUPP_CUST_ADDR, " & vbCrLf _
        '    & " CMST.SUPP_CUST_CITY, CMST.SUPP_CUST_STATE, " & vbCrLf _
        '    & " CMST.SUPP_CUST_PIN, CMST.SUPP_CUST_PHONE, " & vbCrLf _
        '    & " CMST.SUPP_CUST_FAXNO, CMST.SUPP_CUST_MAILID, " & vbCrLf _
        '    & " CMST.SUPP_CUST_MOBILE, CMST.CST_NO, " & vbCrLf _
        '    & " CMST.LST_NO, PREBY.EMP_NAME"

        ''FROM CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " FROM INV_GATEENTRY_HDR IH, INV_GATEENTRY_DET ID, " & vbCrLf _
            & " FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_BUSINESS_MST BCMST, INV_ITEM_MST INVMST"
        ''WHERE CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf _
            & " CMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND INVMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.AUTO_KEY_GATE=ID.AUTO_KEY_GATE" & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND CMST.COMPANY_CODE=BCMST.COMPANY_CODE" & vbCrLf _
            & " AND CMST.SUPP_CUST_CODE=BCMST.SUPP_CUST_CODE AND LOCATION_ID='" & Trim(txtBillTo.Text) & "'" & vbCrLf _
            & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.AUTO_KEY_GATE=" & Val(txtMRRNo.Text) & ""


        ''ORDER CLAUSE...

        mSqlStr = mSqlStr & vbCrLf & "ORDER BY ID.SERIAL_NO"

        SelectQryForMRR = mSqlStr
    End Function


    Private Function SelectQryForDiscrepancy(ByRef mSqlStr As String) As String

        ''SELECT CLAUSE...

        mSqlStr = " SELECT " & vbCrLf & " IH.*, ID.*,INVMST.ITEM_SHORT_DESC,"

        mSqlStr = mSqlStr & " CMST.SUPP_CUST_NAME, CMST.SUPP_CUST_ADDR, " & vbCrLf & " CMST.SUPP_CUST_CITY, CMST.SUPP_CUST_STATE, " & vbCrLf & " CMST.SUPP_CUST_PIN, CMST.SUPP_CUST_PHONE, " & vbCrLf & " CMST.SUPP_CUST_FAXNO, CMST.SUPP_CUST_MAILID, " & vbCrLf & " CMST.SUPP_CUST_MOBILE, CMST.CST_NO, " & vbCrLf & " CMST.LST_NO"

        ''FROM CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " FROM INV_DESCRP_HDR IH, INV_DESCRP_DET ID, " & vbCrLf & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST, INV_GATEENTRY_HDR GATE "

        ''WHERE CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf & " GATE.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND INVMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_DESCRP=ID.AUTO_KEY_DESCRP" & vbCrLf & " AND IH.COMPANY_CODE=GATE.COMPANY_CODE" & vbCrLf & " AND IH.AUTO_KEY_GATE=GATE.AUTO_KEY_GATE" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_GATE=" & Val(txtMRRNo.Text) & ""


        ''ORDER CLAUSE...

        mSqlStr = mSqlStr & vbCrLf & "ORDER BY ID.SERIAL_NO"

        SelectQryForDiscrepancy = mSqlStr
    End Function

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If PubUserID = "G0416" Then
            If MsgQuestion("You Want to Fields Varification ...") = CStr(MsgBoxResult.No) Then
                GoTo NextLine
            End If
        End If

        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

NextLine:

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
    Private Sub cmdSavePrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSavePrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""  '' & vbCrLf & " AND SUPP_CUST_TYPE IN ('S','C')"

        'If ADDMode = True Then
        '    SqlStr = SqlStr & "  AND STATUS='O'"
        'End If

        If MainClass.SearchGridMaster((TxtSupplier.Text), "FIN_SUPP_CUST_BUSINESS_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", "LOCATION_ID", "SUPP_CUST_ADDR||SUPP_CUST_CITY", SqlStr) = True Then
            TxtSupplier.Text = AcName
            txtsupplier_Validating(TxtSupplier, New System.ComponentModel.CancelEventArgs(False))

            txtBillTo.Text = AcName2
            txtBillTo_Validating(txtBillTo, New System.ComponentModel.CancelEventArgs(False))

        End If
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
            'ElseIf mCheckString = "4" Then
            '    cboRefType.SelectedIndex = 9
        End If

        mString = Mid(mString, InStr(1, mString, mSeprator) + 1)
        mCheckString = Mid(mString, 1, InStr(1, mString, mSeprator) - 1)

        If mRefNo = "D" Then
            mCheckString = GetPOFromDs(mCheckString)
            VB6.SetItemString(CboPONo, 0, mCheckString)
        Else
            VB6.SetItemString(CboPONo, 0, mCheckString)
        End If
        mPONo = mCheckString

        If mRefNo = "F" Then Exit Sub

        If mRefNo = "R" Then
            If ValidateRGP(mPONo) = False Then Exit Sub
        ElseIf mRefNo = "I" Or mRefNo = "2" Or mRefNo = "3" Or mRefNo = "1" Then
            If ValidateInvoice(mPONo) = False Then Exit Sub
        ElseIf mRefNo = "P" Then
            If ValidatePO(mPONo, mDivisionCode) = False Then Exit Sub
            '    Else
            '        mPONo = mPONo & VB6.Format(RsCompany!FYNO, "00")
        End If

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
        lblTotCGST.Text = VB6.Format(Val(mCheckString), "0.00")

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
        lblTotSGST.Text = VB6.Format(Val(mCheckString), "0.00")

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

                .Col = ColRate
                .Text = CStr(mRate)

                .Row = CntRow
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColPONo, .Row, ColItemCode, .Row, True))
                .Row = CntRow
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColItemCode, .Row, ColBillQty, .Row, True))
                .Row = CntRow

                CntRow = CntRow + 1
                If Trim(mPONo) <> "" Then
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
                    .Text = CStr(Val(lblTotCGST.Text))
                    mEDFlag = True
                End If

                If mSTID = "ST" And mSTFlag = False Then
                    .Col = ColExpPercent
                    .Text = CStr(Val(lblSTPercentage.Text))

                    .Col = ColExpAmt
                    .Text = CStr(Val(lblTotSGST.Text))
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
        'Dim mCT3No As Integer
        'Dim mFromMRRDate As String
        Dim mDivisionCode As Double
        Dim mSchdDate As String
        Dim xRefDate As String
        Dim xMinInvoiceDate As String
        Dim RsTemp As ADODB.Recordset = Nothing

        mSchdDate = "01/" & VB6.Format(txtMRRDate.Text, "MM/YYYY")

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        If Trim(TxtSupplier.Text) <> "" Then
            If MainClass.ValidateWithMasterTable((TxtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                xSuppCode = MasterNo
                SqlStr = SqlStr & vbCrLf & " AND  POMain.SUPP_CUST_CODE='" & xSuppCode & "'"
            End If
        End If

        If VB.Left(cboRefType.Text, 1) = "F" Or VB.Left(cboRefType.Text, 1) = "C" Or VB.Left(cboRefType.Text, 1) = "J" Then ''Or Left(cboRefType, 1) = "1"
            mWithOutOrder = True
        ElseIf VB.Left(cboRefType.Text, 1) = "I" Then
            SprdMain.Row = SprdMain.ActiveRow

            SprdMain.Col = ColPONo
            xRefNo = Trim(SprdMain.Text)

            SprdMain.Col = ColPODate
            xRefDate = Trim(SprdMain.Text)

            mWithOutOrder = False
            If xRefDate = "" Then

            Else
                SqlStr = "SELECT MIN(INVOICE_DATE) INVOICE_DATE " & vbCrLf _
                        & " FROM FIN_INVOICE_HDR" & vbCrLf _
                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                xMinInvoiceDate = ""
                If RsTemp.EOF = False Then
                    xMinInvoiceDate = IIf(IsDBNull(RsTemp.Fields("INVOICE_DATE").Value), "", RsTemp.Fields("INVOICE_DATE").Value)
                End If

                If xMinInvoiceDate = "" Then
                    mWithOutOrder = True
                ElseIf CDate(xRefDate) < CDate(xMinInvoiceDate) Then
                    mWithOutOrder = True
                End If
            End If


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
                    'SqlStr = "SELECT DISTINCT POMain.AUTO_KEY_PO  As AUTO_KEY_PO , POMain.PUR_ORD_DATE, PODetail.PO_WEF_DATE " & vbCrLf & " FROM PUR_PURCHASE_HDR POMain,PUR_PURCHASE_DET PODetail" & vbCrLf & " WHERE POMain.MKEY=PODetail.MKEY" & vbCrLf & " AND POMain.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND PUR_TYPE IN ('P','R','L')"

                    SqlStr = "SELECT DISTINCT POMain.AUTO_KEY_PO , " & vbCrLf _
                        & " POMain.PUR_ORD_DATE, PODetail.PO_WEF_DATE, PODetail.ITEM_CODE, INV.ITEM_SHORT_DESC, " & vbCrLf _
                        & " CASE WHEN ORDER_TYPE='C' " & vbCrLf _
                        & " THEN PODetail.ITEM_QTY-GETMRRQTYFORPO(POMain.Company_Code, POMain.AUTO_KEY_PO, POMain.SUPP_CUST_CODE, PODetail.ITEM_CODE) ELSE " & vbCrLf _
                        & " GetSupplierMonScheduleQty(POMain.Company_Code, POMain.AUTO_KEY_PO, POMain.SUPP_CUST_CODE, PODetail.ITEM_CODE,TO_DATE('" & VB6.Format(mSchdDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))-GETMRRQTYFORPO(POMain.Company_Code, POMain.AUTO_KEY_PO, POMain.SUPP_CUST_CODE, PODetail.ITEM_CODE) END AS BAL_QTY,ITEM_PRICE,NAV_PO_NO AS OLD_ERP_PO,GROUP_ITEM_CODE " & vbCrLf _
                        & " FROM PUR_PURCHASE_HDR POMain,PUR_PURCHASE_DET PODetail, INV_ITEM_MST INV" & vbCrLf _
                        & " WHERE POMain.MKEY=PODetail.MKEY " & vbCrLf _
                        & " And POMain.Company_Code=INV.Company_Code(+) And PODetail.ITEM_CODE=INV.ITEM_CODE(+)" & vbCrLf _
                        & " And POMain.BILL_TO_LOC_ID='" & Trim(txtBillTo.Text) & "'" & vbCrLf _
                        & " And POMain.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " And PUR_TYPE IN ('P','R','L')"

                    If IsDate(txtBillDate.Text) Then
                        SqlStr = SqlStr & vbCrLf & " AND NVL(PODetail.PO_WEF_DATE,POMain.AMEND_WEF_DATE)<=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                    End If

                    If RsCompany.Fields("PO_IN_GE").Value = "Y" Then
                        If ADDMode = True Then
                            SqlStr = SqlStr & vbCrLf & " AND POMain.PO_STATUS='Y' AND POMain.PO_CLOSED='N'"
                        End If
                    End If

                    ''AND SUBSTR(POMain.AUTO_KEY_PO,LENGTH(POMain.AUTO_KEY_PO)-5,4)=" & RsCompany.Fields("FYEAR").Value

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

                    SqlStr = SqlStr & vbCrLf & " AND POMain.AUTO_KEY_PO Like '" & xPoNo & "%'"

                    'SqlStr = SqlStr & vbCrLf & " AND POMain.AUTO_KEY_PO Like '" & xPoNo & "%'" & vbCrLf & " ORDER BY TO_NUMBER(POMain.AUTO_KEY_PO),POMain.PUR_ORD_DATE "

                Case "R"

                    'SqlStr = "SELECT DISTINCT RGP_NO,  OUTWARD_ITEM_CODE AS ITEM_CODE, RGP_DATE, TO_CHAR(SUM(DECODE(ITEM_IO,'O',1,-1) * RGP_QTY)) AS Balance,F4NO" & vbCrLf _
                    '& " FROM INV_RGP_REG_TRN" & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " "

                    SqlStr = "SELECT DISTINCT TRN.RGP_NO, TRN.CHALLAN_NO, TRN.OUTWARD_ITEM_CODE AS ITEM_CODE, INVMST.ITEM_SHORT_DESC," & vbCrLf _
                            & " TO_CHAR(SUM(DECODE(TRN.ITEM_IO,'O',1,0) * TRN.RGP_QTY)) AS RGP_QTY, TO_CHAR(TRN.RGP_DATE,'DD/MM/YYYY') AS RGP_DATE, " & vbCrLf _
                            & " TO_CHAR(SUM(DECODE(TRN.ITEM_IO,'O',1,-1) * TRN.RGP_QTY)) AS Balance" & vbCrLf _
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

                    SqlStr = SqlStr & vbCrLf & " AND RGP_NO Like '" & xPoNo & "%'"

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

                    SqlStr = SqlStr & vbCrLf & " GROUP BY TRN.RGP_NO,  TRN.OUTWARD_ITEM_CODE, INVMST.ITEM_SHORT_DESC,RGP_DATE,CHALLAN_NO "

                    SqlStr = SqlStr & vbCrLf & " ORDER BY RGP_DATE, RGP_NO "

                Case "I", "1", "2", "3"
                    SqlStr = "SELECT DISTINCT IH.AUTO_KEY_INVOICE ,IH.INVOICE_DATE, ID.ITEM_CODE, ID.ITEM_QTY, ID.ITEM_DESC, ID.CUSTOMER_PART_NO,BILLNO" & vbCrLf _
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

                    SqlStr = SqlStr & vbCrLf & " AND IH.AUTO_KEY_INVOICE Like '" & xPoNo & "%'"

                    'SqlStr = SqlStr & vbCrLf & " AND IH.AUTO_KEY_INVOICE Like '" & xPoNo & "%'" & vbCrLf & " ORDER BY IH.INVOICE_DATE, IH.AUTO_KEY_INVOICE, ID.ITEM_CODE "
            End Select

            If SqlStr <> "" Then
                With SprdMain
                    .Row = .ActiveRow
                    .Col = ColPONo
                    If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then ''If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                        .Row = .ActiveRow
                        .Col = ColPONo
                        .Text = AcName

                        If VB.Left(cboRefType.Text, 1) = "R" Then
                            .Col = ColRGPItemCode
                            .Text = AcName2
                        Else
                            .Col = ColPODate
                            .Text = AcName1
                        End If


                        If VB.Left(cboRefType.Text, 1) = "R" Then
                            .Col = ColPODate
                            .Text = AcName5

                            '.Col = ColRGPQty
                            '.Text = AcName3
                            SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColPONo, .ActiveRow, ColPONo, .ActiveRow, True))
                            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
                        ElseIf VB.Left(cboRefType.Text, 1) = "P" Then
                            .Col = ColItemCode
                            .Text = AcName3

                            .Col = ColItemName
                            .Text = AcName4

                            '.Col = ColItemPartNo
                            '.Text = AcName5

                            SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColItemCode, .ActiveRow, ColItemCode, .ActiveRow, True))
                            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColBillQty)
                        Else
                            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColPONo)
                        End If


                    End If
                End With
            End If
        End If

        If eventArgs.row = 0 And eventArgs.col = ColItemCode Then
            With SprdMain
                .Row = .ActiveRow

                .Col = ColItemCode
                If mWithOutOrder = True Or mIsProjectPO = True Then
                    If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", "CUSTOMER_PART_NO", , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS='A'") = True Then
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
                    If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", "CUSTOMER_PART_NO", , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS='A'") = True Then
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
                If MainClass.ValidateWithMasterTable(.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow
                    .Col = ColItemCode
                    .Text = Trim(MasterNo)
                    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
                End If
            End With
        End If

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

        CalcTots()
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        Dim xPoNo As String
        Dim xICode As String
        'Dim mQty As Double
        'Dim mAcceptQty As Double
        'Dim mItemClassType As String
        'Dim mLotNoRequied As String
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xRGPItemCode As String
        Dim mRow As Integer
        Dim mDivisionCode As Double
        Dim pSupplierCode As String = ""

        Dim mPORate As Double
        Dim mBillRate As Double

        Dim mItemCode As String
        Dim mItemWeight As Double
        Dim mQtyKgs As Double
        Dim xQty As Double

        If eventArgs.newRow = -1 Then Exit Sub

        'Call UpdateTempFile()

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

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
            If MainClass.ValidateWithMasterTable(xPoNo, "AUTO_KEY_PO", "PUR_TYPE", "PUR_PURCHASE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DIV_CODE=" & mDivisionCode & " And BILL_TO_LOC_ID ='" & Trim(txtBillTo.Text) & "' AND PUR_TYPE IN ('P','R','L') ") = True Then  ' And BILL_TO_LOC_ID ='" & Trim(txtBillTo.Text) & "'
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

        Select Case eventArgs.col
            Case ColPONo
                SprdMain.Row = mRow
                SprdMain.Col = ColPONo
                xPoNo = SprdMain.Text
                If mWithOutOrder = False Then
                    If VB.Left(cboRefType.Text, 1) = "P" Then
                        If MainClass.ValidateWithMasterTable(xPoNo, "AUTO_KEY_PO", "PUR_ORD_DATE", "PUR_PURCHASE_HDR", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DIV_CODE=" & mDivisionCode & " And BILL_TO_LOC_ID ='" & Trim(txtBillTo.Text) & "' AND PUR_TYPE IN ('P','R','L') AND SUPP_CUST_CODE='" & pSupplierCode & "'") = False Then
                            If xPoNo <> "" Then
                                MsgInformation("Invalid Ref No for Such Supplier")
                                MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColPONo)
                                eventArgs.cancel = True
                            End If
                        Else
                            'If MainClass.ValidateWithMasterTable(xPoNo, "AUTO_KEY_PO", "PUR_ORD_DATE", "PUR_PURCHASE_HDR", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DIV_CODE=" & mDivisionCode & " And BILL_TO_LOC_ID ='" & Trim(txtBillTo.Text) & "' AND PUR_TYPE IN ('P','R','L') AND SUPP_CUST_CODE='" & pSupplierCode & "' AND AMEND_WEF_DATE<=TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')") = False Then

                            'Else
                            SprdMain.Col = ColPODate
                            SprdMain.Text = MasterNo
                            'End If
                        End If
                    ElseIf VB.Left(cboRefType.Text, 1) = "R" Then

                        If Val(xPoNo) = 0 Then
                            Exit Sub
                        End If

                        If MainClass.ValidateWithMasterTable(xPoNo, "AUTO_KEY_PASSNO", "AUTO_KEY_PASSNO", "INV_GATEPASS_HDR", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & pSupplierCode & "' AND DIV_CODE=" & mDivisionCode & " And BILL_TO_LOC_ID ='" & Trim(txtBillTo.Text) & "'") = False Then
                            MsgInformation("Invalid Ref No for Such Supplier")
                            MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColPONo)
                            eventArgs.cancel = True
                            Exit Sub
                        End If
                        '                    SprdMain.Col = ColRGPItemCode
                        '                    xRGPItemCode = SprdMain.Text



                        SqlStr = "SELECT RGP_NO, RGP_DATE, OUTWARD_ITEM_CODE AS ITEM_CODE" & vbCrLf _
                            & " FROM INV_RGP_REG_TRN" & vbCrLf _
                            & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                            & " AND SUPP_CUST_CODE='" & pSupplierCode & "'" & vbCrLf _
                            & " AND RGP_NO = " & xPoNo & " AND ITEM_IO='O'"
                        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                        If RsTemp.EOF = False Then
                            SprdMain.Row = mRow
                            SprdMain.Col = ColPODate
                            SprdMain.Text = IIf(IsDBNull(RsTemp.Fields("RGP_DATE").Value), "", RsTemp.Fields("RGP_DATE").Value)
                            '                        SprdMain.Col = ColRGPItemCode
                            '                        SprdMain.Text = IIf(IsNull(RsTemp!ITEM_CODE), "", RsTemp!ITEM_CODE)
                        Else
                            MsgInformation("Either invalid RGP No Or Date is not Match for Such Supplier")
                            MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColPONo)
                            eventArgs.cancel = True
                        End If

                    ElseIf VB.Left(cboRefType.Text, 1) = "I" Or VB.Left(cboRefType.Text, 1) = "1" Or VB.Left(cboRefType.Text, 1) = "2" Or VB.Left(cboRefType.Text, 1) = "3" Then
                        If Mid(RsCompany.Fields("COMPANY_NAME").Value, 1, 3) = "KAY" Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("StockBalCheck").Value = "N" Then
                        Else
                            If MainClass.ValidateWithMasterTable(xPoNo, "AUTO_KEY_INVOICE", "INVOICE_DATE", "FIN_INVOICE_HDR", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " And BILL_TO_LOC_ID ='" & Trim(txtBillTo.Text) & "' AND (SUPP_CUST_CODE='" & pSupplierCode & "' OR BUYER_CODE='" & pSupplierCode & "' OR CO_BUYER_CODE='" & pSupplierCode & "')") = False Then
                                MsgInformation("Invalid Ref No for Such Supplier")
                                MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColPONo)
                                eventArgs.cancel = True
                            Else
                                SprdMain.Col = ColPODate
                                SprdMain.Text = MasterNo
                            End If
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
                        If MainClass.ValidateWithMasterTable(xPoNo, "AUTO_KEY_PO", "PUR_ORD_DATE", "PUR_PURCHASE_HDR", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND PUR_TYPE IN ('P','R','L') AND DIV_CODE=" & mDivisionCode & " AND SUPP_CUST_CODE='" & pSupplierCode & "' AND AMEND_WEF_DATE<=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')") = False Then
                            MsgInformation("Invalid Ref No for Such Supplier")
                            MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColPONo)
                            eventArgs.cancel = True
                            Exit Sub
                        End If
                    End If
                ElseIf VB.Left(cboRefType.Text, 1) = "R" Then
                    If MainClass.ValidateWithMasterTable(xPoNo, "AUTO_KEY_PASSNO", "AUTO_KEY_PASSNO", "INV_GATEPASS_HDR", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & pSupplierCode & "' AND DIV_CODE=" & mDivisionCode & "") = False Then
                        MsgInformation("Invalid Ref No for Such Supplier")
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColPONo)
                        eventArgs.cancel = True
                        Exit Sub
                    End If
                ElseIf VB.Left(cboRefType.Text, 1) = "I" Or VB.Left(cboRefType.Text, 1) = "1" Or VB.Left(cboRefType.Text, 1) = "2" Or VB.Left(cboRefType.Text, 1) = "3" Then
                    'If RsCompany.Fields("StockBalCheck").Value = "Y" Then
                    If Mid(RsCompany.Fields("COMPANY_NAME").Value, 1, 3) = "KAY" Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("StockBalCheck").Value = "N" Then
                    Else
                        If MainClass.ValidateWithMasterTable(xPoNo, "AUTO_KEY_INVOICE", "INVOICE_DATE", "FIN_INVOICE_HDR", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND (SUPP_CUST_CODE='" & pSupplierCode & "' OR BUYER_CODE='" & pSupplierCode & "' OR CO_BUYER_CODE='" & pSupplierCode & "')") = False Then
                            MsgInformation("Invalid Ref No for Such Supplier")
                            MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColPONo)
                            eventArgs.cancel = True
                            Exit Sub
                        End If
                    End If

                End If

                SprdMain.Col = ColItemCode
                xICode = Trim(SprdMain.Text)
                If xICode = "" Then Exit Sub
                If MainClass.ValidateWithMasterTable(xICode, "Item_Code", "Item_Code", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS='A'") = True Then
                    If DuplicateItemCode() = False Then
                        SprdMain.Row = mRow
                        If FillGridRow(xPoNo, xICode, xRGPItemCode, mDivisionCode) = False Then Exit Sub
                        FormatSprdMain(eventArgs.row)
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColBillQty)
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



            Case ColQtyInKgs


                If VB.Left(cboRefType.Text, 1) = "R" Then
                    SprdMain.Row = mRow
                    SprdMain.Col = ColItemCode
                    mItemCode = Trim(SprdMain.Text)

                    mItemWeight = 0
                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_WEIGHT", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mItemWeight = Val(MasterNo)
                    End If

                    SprdMain.Col = ColQtyInKgs
                    mQtyKgs = Val(SprdMain.Text)

                    SprdMain.Col = ColBillQty
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

                    SprdMain.Col = ColQtyInKgs
                    SprdMain.Text = mQtyKgs

                    SprdMain.Col = ColBillQty
                    SprdMain.Text = xQty
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

                If VB.Left(cboRefType.Text, 1) = "R" Then
                    mItemWeight = 0
                    If MainClass.ValidateWithMasterTable(xICode, "ITEM_CODE", "ITEM_WEIGHT", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mItemWeight = Val(MasterNo)
                    End If

                    SprdMain.Col = ColQtyInKgs
                    mQtyKgs = Val(SprdMain.Text)

                    SprdMain.Col = ColBillQty
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

                    SprdMain.Col = ColQtyInKgs
                    SprdMain.Text = mQtyKgs

                    SprdMain.Col = ColBillQty
                    SprdMain.Text = xQty
                End If

                ''25-06-2007

                If PubSuperUser <> "S" Then
                    If VB.Left(cboRefType.Text, 1) = "P" Or VB.Left(cboRefType.Text, 1) = "I" Or VB.Left(cboRefType.Text, 1) = "1" Or VB.Left(cboRefType.Text, 1) = "R" Or VB.Left(cboRefType.Text, 1) = "2" Or VB.Left(cboRefType.Text, 1) = "3" Then
                        If CheckBillQty(ColBillQty, eventArgs.row) = True Then
                            MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                            FormatSprdMain(eventArgs.row)
                        Else
                            eventArgs.cancel = True
                            Exit Sub
                        End If
                    Else
                        MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                        FormatSprdMain(eventArgs.row)
                    End If
                Else
                    MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                    FormatSprdMain(eventArgs.row)
                End If


                '            If mWithOutOrder = True Then

                '                CboPONo.List(0) = xPoNo
                '            End If
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
        End Select
        Call CalcTots()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Function DuplicateItemCode() As Boolean

        Dim CntRow As Integer
        Dim mCount As Byte
        Dim mCheckItemCode As String
        Dim mItemCode As String
        Dim mPONo As Double
        Dim xCheckCode As String
        Dim mRGPCode As String
        'Dim mCheckRGPCode As String

        With SprdMain
            .Row = .ActiveRow
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
    Private Function DuplicateLotNo(ByRef pLotNo As String, ByRef pItemCode As String) As Boolean

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
    Private Function FillGridRow(ByRef mPONo As String, ByRef mItemCode As String, ByRef mOutItemCode As String, ByRef mDivisionCode As Double) As Boolean

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
        SqlStr = " SELECT Item_Code,ITEM_SHORT_DESC AS NAME, CUSTOMER_PART_NO, HSN_CODE," & vbCrLf _
            & " PURCHASE_UOM " & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND Item_Code='" & MainClass.AllowSingleQuote(UCase(mItemCode)) & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMisc.EOF = False Then
            SprdMain.Row = SprdMain.Row
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
                        SprdMain.Col = ColItemCode
                        SprdMain.Text = Trim(IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))

                        SprdMain.Col = ColItemName
                        SprdMain.Text = Trim(IIf(IsDBNull(.Fields("Name").Value), "", .Fields("Name").Value))

                        SprdMain.Col = ColItemPartNo
                        SprdMain.Text = Trim(IIf(IsDBNull(.Fields("CUSTOMER_PART_NO").Value), "", .Fields("CUSTOMER_PART_NO").Value))

                        SprdMain.Col = ColHSNCode
                        SprdMain.Text = Trim(IIf(IsDBNull(.Fields("HSN_CODE").Value), "", .Fields("HSN_CODE").Value))

                        SprdMain.Col = ColUnit
                        SprdMain.Text = IIf(IsDBNull(.Fields("PURCHASE_UOM").Value), "", .Fields("PURCHASE_UOM").Value)

                        SprdMain.Col = ColPOQty
                        SprdMain.Text = IIf(Val(SprdMain.Text) = 0, 0, SprdMain.Text)

                        SprdMain.Col = ColBalQty
                        SprdMain.Text = IIf(Val(SprdMain.Text) = 0, 0, SprdMain.Text)
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
    Private Function GetQCEmpCode(ByRef pItemCode As String) As String

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

        SqlStr = " SELECT EMP_CODE FROM INV_ITEM_MST INVMST, INV_QCEMP_MST SMST " & vbCrLf & " WHERE INVMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND INVMST.COMPANY_CODE=SMST.COMPANY_CODE " & vbCrLf & " AND INVMST.CATEGORY_CODE=SMST.CATEGORY_CODE " & vbCrLf & " AND INVMST.SUBCATEGORY_CODE=SMST.SUBCATEGORY_CODE " & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf & " AND SMST.DIV_CODE=" & mDivisionCode & ""

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

            .Col = ColBillQty
            mQty = Val(.Text)

            .Col = ColRate
            mRate = Val(.Text)

            mAmount = mQty * mRate

        End With

        mMRRNO = Val(txtMRRNo.Text)

        SqlStr = "SELECT SUM(BILL_QTY*ITEM_RATE) AS AMOUNT" & vbCrLf & " FROM INV_GATEENTRY_DET " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND REF_PO_NO=" & mPONo & ""

        If mMRRNO <> 0 Then
            SqlStr = SqlStr & vbCrLf & " AND AUTO_KEY_GATE<>" & mMRRNO & ""
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

    Private Function CheckBillQty(ByVal Col As Integer, ByVal Row As Integer) As Boolean

        On Error GoTo ERR1
        Dim mPOQty As Double
        Dim mBalQty As Double
        Dim mEXQty As Double
        Dim mItemCode As String
        With SprdMain

            If mWithOutOrder = True Then CheckBillQty = True : Exit Function
            If mIsProjectPO = True Then CheckBillQty = True : Exit Function


            If RsCompany.Fields("StockBalCheck").Value = "N" Or VB.Left(cboRefType.Text, 1) = "I" Or VB.Left(cboRefType.Text, 1) = "3" Then
                CheckBillQty = True
                Exit Function
            End If
            '    CheckBillQty = True: Exit Function

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

            mEXQty = (mBalQty * IIf(IsDBNull(RsCompany.Fields("GRExcessPer").Value), 0, RsCompany.Fields("GRExcessPer").Value) / 100)

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
    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent)
        'With SprdView
        '    .Row = eventArgs.row

        '    .Col = 2
        '    txtMRRNo.Text = CStr(Val(.Text))

        '    TxtMRRNo_Validating(txtMRRNo, New System.ComponentModel.CancelEventArgs(False))
        '    CmdView_Click(CmdView, New System.EventArgs())
        'End With
    End Sub
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

        pTempUpdate = False

        If CheckRefDate(mDivisionCode) = False Then
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtBillNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBillNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim pErrorMsg As String = ""
        If ValidateBillNo(txtBillNo.Text, pErrorMsg) = False Then
            MsgInformation(pErrorMsg)
            Cancel = True
            GoTo EventExitSub
        End If
        pTempUpdate = False
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDocsThru_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDocsThru.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
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

    Private Sub TxtMRRNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMRRNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
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

        If MODIFYMode = True And RsMRRMain.EOF = False Then xMkey = RsMRRMain.Fields("AUTO_KEY_GATE").Value
        mMRRNO = Trim(txtMRRNo.Text)

        SqlStr = " SELECT * FROM INV_GATEENTRY_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_GATE,LENGTH(AUTO_KEY_GATE)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_GATE=" & Val(mMRRNO) & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMRRMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsMRRMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such MRR, Use Generate MRR Option To add", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM INV_GATEENTRY_HDR " & " WHERE AUTO_KEY_GATE=" & Val(xMkey) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMRRMain, ADODB.LockTypeEnum.adLockReadOnly)
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
        Dim mVNoSeq As Double
        Dim mSuppCustCode As String
        Dim mTotQty As Double
        Dim mCancelled As String
        Dim mPONOs As String = ""
        Dim mMRRMade As String
        Dim mEntryDate As String
        Dim CntRow As Integer
        Dim mItemCode As String
        Dim mCatCode As String = ""
        Dim mFreightType As Integer
        Dim mPONo As String
        Dim mShippedToSame As String
        Dim mShippedToCode As String = ""
        Dim mDivisionCode As Double
        Dim mUnderChallan As String
        Dim mTCAvailable As String
        Dim mTPRAvailable As String
        Dim mTCFilename As String
        Dim mTRFileName As String
        Dim mInterUnitCode As Long
        Dim mDeliveryToCode As String = ""
        Dim mDeliveryToLoc As String = ""

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mEntryDate = VB6.Format(PubCurrDate, "DD-MMM-YYYY") & " " & GetServerTime()

        mSuppCustCode = CStr(-1)
        If MainClass.ValidateWithMasterTable((TxtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSuppCustCode = MasterNo
        Else
            mSuppCustCode = CStr(-1)
            MsgBox("Supplier Does Not Exist In Master", MsgBoxStyle.Information)
            GoTo ErrPart
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

        mMRRMade = IIf(chkMRRMade.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        If Val(txtMRRNo.Text) = 0 Then
            mVNoSeq = CDbl(AutoGenSeqNo(mDivisionCode))
        Else
            mVNoSeq = Val(txtMRRNo.Text)
        End If

        txtMRRNo.Text = CStr(Val(CStr(mVNoSeq)))

        If CheckValidVDate(mVNoSeq, mDivisionCode) = False Then GoTo ErrPart

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
            TxtShipTo.Text = txtBillTo.Text
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

        mTCAvailable = IIf(chkTCAvailable.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mTPRAvailable = IIf(chkTPRAvailable.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mTCFilename = IIf(mTCAvailable = "Y", ExtractFileName((txtTCPath.Text)), "")
        mTRFileName = IIf(mTPRAvailable = "Y", ExtractFileName((txtTPRPath.Text)), "")

        If ADDMode = True Then
            LblMkey.Text = CStr(mVNoSeq)
            SqlStr = "INSERT INTO INV_GATEENTRY_HDR( " & vbCrLf _
                & " COMPANY_CODE, AUTO_KEY_GATE, GATE_DATE," & vbCrLf _
                & " SUPP_CUST_CODE, BILL_NO, BILL_DATE," & vbCrLf _
                & " REF_DOC_NO, REF_DOC_DATE, REF_TYPE," & vbCrLf _
                & " REF_AUTO_KEY_NO, REF_DATE, PO_NO," & vbCrLf _
                & " PO_DATE, NO_ST38, TRANSPORT_MODE," & vbCrLf _
                & " REMARKS, PRE_EMP_CODE, FREIGHT_CHARGES," & vbCrLf _
                & " ASSESS_AMT, EXCISE_PER, EXCISE_AMT," & vbCrLf _
                & " DISCOUNT_PER, DISCOUNT_AMT, TAXABLE_AMT," & vbCrLf _
                & " SALETAX_PER, SALETAX_AMT, FREIGHT_AMT," & vbCrLf _
                & " INVOICE_AMT, FORM_DETAILS, MRR_MADE," & vbCrLf _
                & " ITEM_DETAILS, " & vbCrLf & " TOTEDUPERCENT,TOTEDUAMOUNT," & vbCrLf _
                & " ADDUSER, ADDDATE, MODUSER, MODDATE,UPDATE_FROM, " & vbCrLf _
                & " FREIGHT_TYPE, MODE_TYPE, DOCS_THRU, VEHICLE, GRNO, GRDATE, " & vbCrLf _
                & " DIV_CODE,SHIPPED_TO_SAMEPARTY, SHIPPED_TO_PARTY_CODE,PARTY_EWAYBILLNO, UNDER_CHALLAN, " & vbCrLf _
                & " TC_AVAILABLE, TC_FILE_PATH, TPRI_AVAILABLE, TPRI_FILE_PATH,BILL_TO_LOC_ID,SHIP_TO_LOC_ID,OLD_ERP_NO, OLD_ERP_DATE,DELIVERY_TO,DELIVERY_TO_LOC_ID) "


            SqlStr = SqlStr & vbCrLf _
                & " VALUES(" & RsCompany.Fields("COMPANY_CODE").Value & ", " & Val(CStr(mVNoSeq)) & ", TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(mSuppCustCode) & "', '" & MainClass.AllowSingleQuote((txtBillNo.Text)) & "', TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " '','','" & VB.Left(cboRefType.Text, 1) & "'," & vbCrLf _
                & " '" & mPONOs & "','','', " & vbCrLf _
                & " '', '" & Trim(txtST38No.Text) & "', '" & MainClass.AllowSingleQuote((TxtTransporter.Text)) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((TxtRemarks.Text)) & "', '" & MainClass.AllowSingleQuote(PubUserID) & "', '" & MainClass.AllowSingleQuote((txtFreight.Text)) & "', " & vbCrLf _
                & " " & Val(lblTotItemValue.Text) & ", " & Val(lblEDPercentage.Text) & ", " & Val(lblTotCGST.Text) & "," & vbCrLf _
                & " 0," & Val(lblDiscount.Text) & "," & Val(lblTotTaxableAmt.Text) & ", " & vbCrLf _
                & " " & Val(lblSTPercentage.Text) & "," & Val(lblTotSGST.Text) & "," & Val(lblTotFreight.Text) & "," & vbCrLf _
                & " " & Val(lblNetAmount.Text) & ",'" & MainClass.AllowSingleQuote((txtFormDetail.Text)) & "','" & mMRRMade & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((TxtItemDesc.Text)) & "', " & vbCrLf _
                & " " & Val(lblEDUPercent.Text) & ", " & Val(lblEDUAmount.Text) & ", " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & mEntryDate & "','DD-MON-YYYY HH24:MI'),'','','H'," & vbCrLf _
                & " " & mFreightType & ", '" & MainClass.AllowSingleQuote((cboMode.Text)) & "', '" & MainClass.AllowSingleQuote((txtDocsThru.Text)) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtVehicle.Text)) & "', '" & MainClass.AllowSingleQuote((txtGRNo.Text)) & "'," & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtGRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & mDivisionCode & ",'" & mShippedToSame & "','" & mShippedToCode & "'," & Val(txtEwayBillNo.Text) & ",'" & mUnderChallan & "'," & vbCrLf _
                & " '" & mTCAvailable & "','" & mTCFilename & "','" & mTPRAvailable & "','" & mTRFileName & "','" & MainClass.AllowSingleQuote(txtBillTo.Text) & "','" & MainClass.AllowSingleQuote(TxtShipTo.Text) & "','" & MainClass.AllowSingleQuote(txtOldERPNo.Text) & "', TO_DATE('" & VB6.Format(txtOldERPDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(mDeliveryToCode) & "','" & MainClass.AllowSingleQuote(mDeliveryToLoc) & "')"

        ElseIf MODIFYMode = True Then
            SqlStr = ""
            SqlStr = "UPDATE INV_GATEENTRY_HDR SET " & vbCrLf & " AUTO_KEY_GATE =" & Val(CStr(mVNoSeq)) & " ," & vbCrLf & " GATE_DATE=TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSuppCustCode) & "'," & vbCrLf & " BILL_NO='" & MainClass.AllowSingleQuote((txtBillNo.Text)) & "'," & vbCrLf & " BILL_DATE=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " REF_DOC_NO=" & Val(CboPONo.Text) & "," & vbCrLf & " REF_DOC_DATE=''," & vbCrLf & " REF_TYPE='" & VB.Left(cboRefType.Text, 1) & "'," & vbCrLf & " REF_AUTO_KEY_NO='" & mPONOs & "', PARTY_EWAYBILLNO= " & Val(txtEwayBillNo.Text) & "," & vbCrLf & " REF_DATE=''," & vbCrLf & " PO_NO=''," & vbCrLf & " PO_DATE='',UNDER_CHALLAN='" & mUnderChallan & "'," & vbCrLf & " NO_ST38='" & Trim(txtST38No.Text) & "'," & vbCrLf & " TRANSPORT_MODE='" & MainClass.AllowSingleQuote((TxtTransporter.Text)) & "'," & vbCrLf & " REMARKS='" & MainClass.AllowSingleQuote((TxtRemarks.Text)) & "',DIV_CODE=" & mDivisionCode & "," & vbCrLf & " TC_AVAILABLE = '" & mTCAvailable & "',  " & vbCrLf & " TC_FILE_PATH = '" & mTCFilename & "',  " & vbCrLf & " TPRI_AVAILABLE = '" & mTPRAvailable & "',  " & vbCrLf & " TPRI_FILE_PATH = '" & mTRFileName & "', "

            SqlStr = SqlStr & vbCrLf & " FREIGHT_CHARGES= '" & MainClass.AllowSingleQuote(txtFreight.Text) & "'," & vbCrLf & " ASSESS_AMT= " & Val(lblTotItemValue.Text) & ", " & vbCrLf & " EXCISE_PER= " & Val(lblEDPercentage.Text) & ", " & vbCrLf & " EXCISE_AMT= " & Val(lblTotCGST.Text) & "," & vbCrLf & " DISCOUNT_PER= 0, " & vbCrLf & " DISCOUNT_AMT= " & Val(lblDiscount.Text) & "," & vbCrLf & " TAXABLE_AMT= " & Val(lblTotTaxableAmt.Text) & ", " & vbCrLf & " SALETAX_PER= " & Val(lblSTPercentage.Text) & "," & vbCrLf & " SALETAX_AMT= " & Val(lblTotSGST.Text) & "," & vbCrLf & " FREIGHT_AMT= " & Val(lblTotFreight.Text) & "," & vbCrLf & " INVOICE_AMT= " & Val(lblNetAmount.Text) & "," & vbCrLf & " FORM_DETAILS='" & MainClass.AllowSingleQuote(txtFormDetail.Text) & "'," & vbCrLf & " ITEM_DETAILS='" & MainClass.AllowSingleQuote(TxtItemDesc.Text) & "', " & vbCrLf & " TOTEDUPERCENT= " & Val(lblEDUPercent.Text) & ", " & vbCrLf & " TOTEDUAMOUNT= " & Val(lblEDUAmount.Text) & ", UPDATE_FROM='H'," & vbCrLf & " FREIGHT_TYPE=" & mFreightType & ", " & vbCrLf & " MODE_TYPE='" & MainClass.AllowSingleQuote(cboMode.Text) & "', " & vbCrLf & " DOCS_THRU='" & MainClass.AllowSingleQuote(txtDocsThru.Text) & "', " & vbCrLf & " VEHICLE='" & MainClass.AllowSingleQuote(txtVehicle.Text) & "', " & vbCrLf & " GRNO='" & MainClass.AllowSingleQuote(txtGRNo.Text) & "', " & vbCrLf _
                & " GRDATE=TO_DATE('" & VB6.Format(txtGRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " SHIPPED_TO_SAMEPARTY='" & mShippedToSame & "', SHIPPED_TO_PARTY_CODE='" & mShippedToCode & "', " & vbCrLf _
                & " BILL_TO_LOC_ID='" & MainClass.AllowSingleQuote((txtBillTo.Text)) & "', " & vbCrLf _
                & " SHIP_TO_LOC_ID='" & MainClass.AllowSingleQuote((TxtShipTo.Text)) & "', OLD_ERP_NO='" & MainClass.AllowSingleQuote(txtOldERPNo.Text) & "', OLD_ERP_DATE=TO_DATE('" & VB6.Format(txtOldERPDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),DELIVERY_TO='" & MainClass.AllowSingleQuote(mDeliveryToCode) & "',DELIVERY_TO_LOC_ID = '" & MainClass.AllowSingleQuote(mDeliveryToLoc) & "' " & vbCrLf _
                & " WHERE AUTO_KEY_GATE ='" & MainClass.AllowSingleQuote(LblMkey.Text) & "'"
        End If

        ''MRR_MADE = '" & mMRRMade & "',

        PubDBCn.Execute(SqlStr)

        '' Update Misc Gate Entry

        SqlStr = "UPDATE INV_MISC_GATE_HDR  SET " & vbCrLf _
            & " GATE_ENTRY_MADE ='Y'," & vbCrLf _
            & " AUTO_KEY_GATE =" & Val(CStr(mVNoSeq)) & " ," & vbCrLf _
            & " GATE_DATE=TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSuppCustCode) & "'" & vbCrLf _
            & " AND BILL_NO='" & MainClass.AllowSingleQuote(txtBillNo.Text) & "'" & vbCrLf _
            & " AND BILL_DATE=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        PubDBCn.Execute(SqlStr)

        If UpdateDetail1(mSuppCustCode, Val(CboPONo.Text), mDivisionCode) = False Then GoTo ErrPart

        If UpdateBlobData((LblMkey.Text)) = False Then GoTo ErrPart

        If cboRefType.SelectedIndex = 2 Then
            If UpdateSRTRN() = False Then GoTo ErrPart
        End If

        If MainClass.ValidateWithMasterTable(Trim(TxtSupplier.Text), "SUPP_CUST_NAME", "INTERUNIT_COMPANY_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND INTER_UNIT='Y'") = True Then
            'If VB.Left(cboRefType.Text, 1) = "J" Then
            mInterUnitCode = Val(MasterNo)

            SqlStr = "UPDATE INV_GATEPASS_HDR SET IS_GATENTRY_MADE='Y' WHERE AUTO_KEY_PASSNO=" & Val(txtBillNo.Text) & " " & vbCrLf _
                & " AND COMPANY_CODE = " & mInterUnitCode & ""

            '' & " (SELECT COMPANY_CODE FROM FIN_PRINT_MST WHERE COMP_AC_CODE='" & mSuppCustCode & "')"

            PubDBCn.Execute(SqlStr)

            'Else
            SqlStr = "UPDATE FIN_INVOICE_HDR SET IS_GATENTRY_MADE='Y' WHERE BILLNO='" & Trim(txtBillNo.Text) & "' " & vbCrLf _
                 & " AND COMPANY_CODE = " & mInterUnitCode & ""

            '' & " (SELECT COMPANY_CODE FROM FIN_PRINT_MST WHERE COMP_AC_CODE='" & mSuppCustCode & "')"

            'End If
            PubDBCn.Execute(SqlStr)
        End If

        UpdateMain1 = True
        PubDBCn.CommitTrans()

        Exit Function
ErrPart:
        UpdateMain1 = False
        PubDBCn.RollbackTrans() ''
        RsMRRMain.Requery() ''.Refresh
        RsMRRDetail.Requery() ''.Refresh
        If Err.Description = "" Then Exit Function
        'If Err.Number = -2147217900 Then
        '    ErrorMsg("Duplicate Invoice No. Generated, Save Again", "Duplicate", MsgBoxStyle.Critical)
        'Else
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'End If
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

        UpdateBlobData = True

        Exit Function

        '    CopyBlobFileintoTempFile

        PubDBCnBlob.Errors.Clear()
        PubDBCnBlob.BeginTrans()

        SqlStr = "Delete From  INV_GATEENTRY_TC_TRN WHERE MKEY=" & Val(LblMkey.Text) & ""
        PubDBCnBlob.Execute(SqlStr)

        If chkTCAvailable.CheckState = System.Windows.Forms.CheckState.Unchecked And chkTPRAvailable.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            UpdateBlobData = True
            Exit Function
        End If

        mTCFilename = ExtractFileName((txtTCPath.Text))
        mTCExt = GetExtensionName((txtTCPath.Text))

        mTPRFilename = ExtractFileName((txtTPRPath.Text))
        mTPRExt = GetExtensionName((txtTPRPath.Text))

        SqlStr = " SELECT MKEY, TC_DOC_DESC, TC_DOC_EXT, TPR_DOC_DESC, TPR_DOC_EXT " & vbCrLf & " FROM INV_GATEENTRY_TC_TRN " & vbCrLf & " WHERE MKEY = '" & pMkey & "'"


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
        RsTemp.Open(" SELECT TC_BLOB_DATA, TPR_BLOB_DATA" & vbCrLf & " FROM INV_GATEENTRY_TC_TRN WHERE MKEY = '" & pMkey & "'", PubDBCnBlob, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic)

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
    Private Function UpdateSRTRN() As Boolean

        On Error GoTo UpdateDetail1Err
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing


        SqlStr = " SELECT * " & vbCrLf & " FROM INV_SALEREJECTION_TRN " & vbCrLf & " WHERE AUTO_KEY_REF=" & Val(txtMRRNo.Text) & "" & vbCrLf & " AND REF_TYPE='G'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = True Then
            SqlStr = " INSERT INTO INV_SALEREJECTION_TRN ( " & vbCrLf & " AUTO_KEY_REF, REF_TYPE, MAIL_SEND,IS_MODIFIED) " & vbCrLf & " VALUES (" & Val(txtMRRNo.Text) & ",'G','N','N') "
        Else
            SqlStr = " UPDATE INV_SALEREJECTION_TRN SET IS_MODIFIED='Y', MAIL_SEND='N'" & vbCrLf & " WHERE AUTO_KEY_REF=" & Val(txtMRRNo.Text) & " AND REF_TYPE='G'"
        End If

        PubDBCn.Execute(SqlStr)

        UpdateSRTRN = True
        Exit Function
UpdateDetail1Err:
        UpdateSRTRN = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function
    Private Function CheckValidVDate(ByRef pMRRNoSeq As Double, ByRef mDivisionCode As Double) As Object

        On Error GoTo CheckERR
        Dim SqlStr As String = ""
        Dim mRsCheck1 As ADODB.Recordset = Nothing
        Dim mRsCheck2 As ADODB.Recordset = Nothing
        Dim mBackBillDate As String = ""
        Dim mMaxInvStrfNo As Integer
        Dim mSeparateSeries As String
        Dim RsTemp As ADODB.Recordset = Nothing

        CheckValidVDate = True

        If RsCompany.Fields("StockBalCheck").Value = "N" Or MODIFYMode = True Then
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

        SqlStr = "SELECT MAX(GATE_DATE)" & vbCrLf & " FROM INV_GATEENTRY_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_GATE,LENGTH(AUTO_KEY_GATE)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_GATE<" & Val(CStr(pMRRNoSeq)) & ""

        If mSeparateSeries = "Y" Then
            SqlStr = SqlStr & vbCrLf & " AND DIV_CODE=" & mDivisionCode & ""
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsCheck2, ADODB.LockTypeEnum.adLockReadOnly)

        If mRsCheck2.EOF = False Then
            mBackBillDate = IIf(IsDBNull(mRsCheck2.Fields(0).Value), mBackBillDate, mRsCheck2.Fields(0).Value)
        End If

        SqlStr = "SELECT MIN(GATE_DATE)" & " FROM INV_GATEENTRY_HDR " & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_GATE,LENGTH(AUTO_KEY_GATE)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_GATE>" & Val(CStr(pMRRNoSeq)) & ""

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
    Private Function AutoGenSeqNo(ByRef pDivision As Double) As String

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
        SqlStr = "SELECT Max(AUTO_KEY_GATE)  " & vbCrLf _
            & " FROM INV_GATEENTRY_HDR " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_GATE,LENGTH(AUTO_KEY_GATE)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        If mSeparateSeries = "Y" Then
            SqlStr = SqlStr & vbCrLf & " AND DIV_CODE=" & pDivision & ""
        End If

        'If CDate(txtMRRDate.Text) <= CDate("30/06/2022") Then
        '    SqlStr = SqlStr & vbCrLf & " AND GATE_DATE<=TO_DATE('" & VB6.Format("30/06/2022", "DD-MMM-YYYY") & "','DD-MON-YYYY')"
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

    Private Function UpdateDetail1(ByRef pSupplierCode As String, ByRef pRefAutoKeyNo As Double, ByRef pDivisionCode As Double) As Boolean

        On Error GoTo UpdateDetail1Err
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim j As Integer
        Dim mPONo As String
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mUnit As String
        Dim mHeatNo As String
        Dim mBatchNo As String
        Dim mBillQty As Double
        Dim mQtyInKgs As Double
        Dim mItemRate As Double
        Dim mItemCost As Double
        Dim mInvQty As Double

        Dim mRecord As Boolean
        Dim mPODate As String
        Dim mRGPItemCode As String
        Dim mRemarks As String
        Dim mItemLock As Boolean

        Dim mOutwardF4No As Double
        Dim mOutwardF4Date As String = ""
        Dim mExpDate As String = ""
        Dim mCheckF4 As Boolean
        Dim mRGPQty As Double

        mRecord = False

        PubDBCn.Execute("Delete From INV_GATEENTRY_DET Where AUTO_KEY_GATE='" & LblMkey.Text & "'")
        '    PubDBCn.Execute "DELETE FROM INV_RGP_REG_TRN WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND REF_NO='" & lblMkey.text & "'  AND BOOKTYPE='M' AND ITEM_IO='I'"

        If chkMRRMade.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            PubDBCn.Execute("DELETE FROM INV_RGP_REG_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND BILL_NO='" & txtBillNo.Text & "' AND  BILL_DATE =TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND  SUPP_CUST_CODE='" & pSupplierCode & "'  AND BOOKTYPE='M' AND ITEM_IO='I'")
        End If


        With SprdMain
            I = 0
            For j = 1 To .MaxRows - 1
                .Row = j
                I = I + 1
                mItemLock = False

                .Col = ColPONo
                mPONo = IIf(Val(.Text) = 0, "-1" & VB6.Format(RsCompany.Fields("FYEAR").Value, "00"), .Text)

                .Col = ColPODate
                mPODate = MainClass.AllowSingleQuote(.Text)

                .Col = ColRGPItemCode
                mRGPItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                If GetItemLocking(mItemCode) = True Then
                    mItemLock = True
                End If

                .Col = ColItemDesc
                mItemDesc = Val(.Text)

                .Col = ColUnit
                mUnit = MainClass.AllowSingleQuote(.Text)

                .Col = ColHeatNo
                mHeatNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColBatchNo
                mBatchNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColBillQty
                mBillQty = Val(.Text)

                .Col = ColQtyInKgs
                mQtyInKgs = Val(.Text)

                .Col = ColRate
                mItemRate = Val(.Text)

                .Col = ColRemarks
                mRemarks = Trim(.Text)

                SqlStr = ""

                If mItemCode <> "" And mBillQty > 0 Then
                    SqlStr = " INSERT INTO INV_GATEENTRY_DET ( AUTO_KEY_GATE, SERIAL_NO, ITEM_CODE,ITEM_UOM, BILL_QTY," & vbCrLf _
                            & " REMARKS, ITEM_RATE, GATE_DATE, COMPANY_CODE,SUPP_CUST_CODE, REF_TYPE, REF_AUTO_KEY_NO," & vbCrLf _
                            & " ITEM_COST,REF_PO_NO, RGP_ITEM_CODE, REF_DATE,ITEM_QTY_IN_KGS,BATCH_NO,HEAT_NO) " & vbCrLf _
                            & " VALUES ('" & LblMkey.Text & "'," & I & ",'" & mItemCode & "', '" & mUnit & "'," & vbCrLf _
                            & " " & mBillQty & ", '" & MainClass.AllowSingleQuote(mRemarks) & "'," & mItemRate & "," & vbCrLf _
                            & " TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " " & RsCompany.Fields("COMPANY_CODE").Value & ", '" & pSupplierCode & "'," & vbCrLf _
                            & "'" & VB.Left(cboRefType.Text, 1) & "','" & mPONo & "', " & mItemCost & ",'" & mPONo & "'," & vbCrLf _
                            & "'" & mRGPItemCode & "',TO_DATE('" & VB6.Format(mPODate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                            & " " & mQtyInKgs & ",'" & mBatchNo & "','" & mHeatNo & "') "

                    PubDBCn.Execute(SqlStr)

                    '                If Left(cboRefType.Text, 1) = "R" Then
                    '                    Call GetF4detailFromRGP(mPONo, mCheckF4, mOutwardF4No, mOutwardF4Date, mExpDate)
                    '
                    '                    If UpdateRGP_TRN(PubDBCn, mPONo, VB6.Format(mPODate, "DD/MM/YYYY"), _
                    ''                            txtMRRNo.Text, VB6.Format(txtMRRDate.Text, "DD/MM/YYYY"), _
                    ''                            pSupplierCode, mOutwardF4No, VB6.Format(mOutwardF4Date, "DD/MM/YYYY"), _
                    ''                            txtBillNo.Text, txtBillDate.Text, _
                    ''                            Trim(mRGPItemCode), mItemCode, mRGPQty, mRecdQty, "I", I, "M", mExpDate) = False Then GoTo UpdateDetail1Err
                    '                End If
                    '

                    mRecord = True

                    If chkMRRMade.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                        If VB.Left(cboRefType.Text, 1) = "R" Then
                            Call GetF4detailFromRGP(mPONo, mCheckF4, mOutwardF4No, mOutwardF4Date, mExpDate)

                            If UpdateRGP_TRN(PubDBCn, mPONo, VB6.Format(mPODate, "DD/MM/YYYY"), CDbl(txtMRRNo.Text), VB6.Format(txtMRRDate.Text, "DD/MM/YYYY"), pSupplierCode, mOutwardF4No, VB6.Format(mOutwardF4Date, "DD/MM/YYYY"), (txtBillNo.Text), (txtBillDate.Text), Trim(mRGPItemCode), mItemCode, mRGPQty, mBillQty, "I", I, "M", mExpDate, txtBillTo.Text) = False Then GoTo UpdateDetail1Err
                        End If
                    End If
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

        PubDBCn.Execute("Delete From INV_GATEENTRY_EXP Where AUTO_KEY_GATE='" & LblMkey.Text & "'")
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
                    SqlStr = "Insert Into  INV_GATEENTRY_EXP (AUTO_KEY_GATE,SERIAL_NO, " & vbCrLf & " EXPCODE,EXPPERCENT,AMOUNT,CalcOn,RO) " & vbCrLf & " Values ('" & LblMkey.Text & "'," & I & ", " & vbCrLf & " " & mExpCode & "," & mPercent & "," & mExpAmount & ", " & vbCrLf & " " & mCalcOn & ",'" & mRO & "')"
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
        Dim mLotNoRequied As String
        Dim mValidQCUser As Boolean
        Dim mLockBookCode As Integer
        Dim mQCDate As String
        Dim mQCEmp As String
        Dim mStockType As String = ""
        Dim ItemCode As String
        Dim mMaxStockQty As Double
        Dim mStockQty As Double
        Dim mItemUOM As String = ""
        Dim mAcceptQty As Double
        Dim mDivisionCode As Double
        Dim mCntRGPPaidType As Integer
        Dim mCntRGPFOCType As Integer
        Dim mRGPPurpose As Boolean
        Dim mProdType As String
        Dim mItemCategory As String
        Dim mStockLockQty As Double
        Dim mMaxLevelQty As Double
        Dim mMaxLevelCheck As Boolean
        Dim mMRRQty As Double
        Dim pMRRNo As Double
        Dim mBillNo As String
        Dim mIsFGInvoice As Boolean
        Dim meBillNoApp As String
        Dim meBillNoAppDate As String
        Dim xUnit As String
        Dim xBillQty As Double
        Dim mFileSize As Double
        Dim mPORate As Double
        Dim mBillRate As Double
        Dim mBillFromSupplier As String
        Dim mHSNCode As String
        Dim mPOHSNCode As String
        Dim mPartyGSTNo As String
        Dim mSameGSTNo As String
        Dim mInterUnit As String
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


        If MainClass.ValidateWithMasterTable(Trim(TxtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND INTER_UNIT='Y'") = True Then
            mInterUnit = "Y"
        Else
            mInterUnit = "N"
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
        '        TxtSupplier.SetFocus
        '        FieldsVarification = False
        '        Exit Function
        '    Else
        '        mBillFromSupplier = MasterNo
        '    End If
        'End If


        pMRRNo = -1
        If MainClass.ValidateWithMasterTable((txtMRRNo.Text), "AUTO_KEY_GATE", "MRR_NO", "INV_GATEENTRY_HDR", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            pMRRNo = Val(MasterNo)
        End If

        If ValidateBranchLocking((txtMRRDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If
        If ValidateAccountLocking(PubDBCn, txtMRRDate.Text, (TxtSupplier.Text), mSupplierCode) = True Then
            FieldsVarification = False
            Exit Function
        End If

        '    If lblBookType.text = "Q" Then
        '        mLockBookCode = ConLockMRRQC
        '    Else
        '        mLockBookCode = ConLockMRREntry
        '    End If

        If ValidateBookLocking(PubDBCn, mLockBookCode, txtMRRDate.Text) = True Then
            FieldsVarification = False
            Exit Function
        End If

        If ADDMode = True Then
            If MainClass.ValidateWithMasterTable(mSupplierCode, "SUPP_CUST_CODE", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STOP_MRR='Y'") = True Then
                MsgBox("MRR Cann't Be Made for Such Customer, So cann't be saved", MsgBoxStyle.Information)
                FieldsVarification = False
                If TxtSupplier.Enabled = True Then TxtSupplier.Focus()
                Exit Function
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

        If (txtOldERPDate.Text) = "" Then
        Else
            If Not IsDate(txtOldERPDate.Text) Then
                MsgBox("Invalid OLD ERP Date", MsgBoxStyle.Information)
                FieldsVarification = False
                txtOldERPDate.Focus()
                Exit Function
            End If
        End If


        'If PubSuperUser <> "S" Then
        '    If ADDMode = True Then
        '        If DateDiff(Microsoft.VisualBasic.DateInterval.Month, CDate(txtBillDate.Text), PubCurrDate) > 12 Then
        '            MsgBox("Bill Date is more than 12 Month old, So cann't be Save. Please contact Administrator.", MsgBoxStyle.Information)
        '            If txtBillDate.Enabled Then txtBillDate.Focus()
        '            FieldsVarification = False
        '            Exit Function
        '        End If
        '    End If
        '    If CDate(txtBillDate.Text) > CDate(PubCurrDate) Then
        '        MsgBox("Bill Date Cann't be future Date, So cann't be Save. Please contact Administrator.", MsgBoxStyle.Information)
        '        If txtBillDate.Enabled Then txtBillDate.Focus()
        '        FieldsVarification = False
        '        Exit Function
        '    End If
        'End If

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

        If ValidateDivisionRight(PubUserID, mDivisionCode, UCase(Trim(cboDivision.Text))) = False Then
            FieldsVarification = False
            Exit Function
        End If

        If CheckRefDate(mDivisionCode) = False Then
            FieldsVarification = False
            Exit Function
        End If


        '    If PubSuperUser = "U" Then
        '        If CDate(txtBillDate.Text) < CDate(RsCompany!Start_Date - 365) Then
        '            MsgBox "Invalid BillDate."
        '            FieldsVarification = False
        '            If txtBillDate.Enabled = True Then txtBillDate.SetFocus
        '            Exit Function
        '        End If
        '    End If

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

        If mAuthorised = False Then
            If mSameGSTNo = "N" Then
                If VB.Left(cboRefType.Text, 1) = "F" Then
                    MsgBox("You Have No Right To Entered FOC Invoice.")
                    FieldsVarification = False
                    Exit Function
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
        If MainClass.ValidateWithMasterTable((TxtSupplier.Text), "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Supplier Does Not Exist In Master", MsgBoxStyle.Information)
            'txtSupplier.SetFocus
            FieldsVarification = False
            Exit Function
        Else
            'mWithInState = IIf(IsDBNull(MasterNo), "Y", MasterNo)
        End If

        If Trim(txtBillTo.Text) = "" Then
            MsgInformation("Bill To is blank. Cannot Save")
            txtBillTo.Focus()
            FieldsVarification = False
            Exit Function
        Else
            If MainClass.ValidateWithMasterTable(txtBillTo.Text, "LOCATION_ID", "WITHIN_STATE", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(TxtSupplier.Text) & "'") = False Then
                MsgBox("Invalid Location Id for such Customer.", MsgBoxStyle.Information)
                txtBillTo.Focus()
                FieldsVarification = False
            Else
                mWithInState = GetPartyBusinessDetail(TxtSupplier.Text, Trim(txtBillTo.Text), "WITHIN_STATE")
                'mWithInState = IIf(IsDBNull(MasterNo), "Y", MasterNo)
            End If
        End If

        'sk21-11-2005
        '    If Val(CDbl(lblNetAmount.text)) >= 10000 And mWithInState = "N" Then

        'If CDate(txtMRRDate.Text) < CDate(PubGSTApplicableDate) And CDate(txtBillDate.Text) >= CDate(PubGSTApplicableDate) Then
        '    If DateDiff(Microsoft.VisualBasic.DateInterval.Month, CDate(txtMRRDate.Text), CDate(txtBillDate.Text)) > 1 Then
        '        MsgBox("Bill Date is more than 1 Month After GST Applicable, So cann't be Save. Please contact Administrator.", MsgBoxStyle.Information)
        '        If txtBillDate.Enabled Then txtBillDate.Focus()
        '        FieldsVarification = False
        '        Exit Function
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
            If Trim(TxtShipTo.Text) = "" Then
                MsgInformation("Ship To is blank. Cannot Save")
                TxtShipTo.Focus()
                FieldsVarification = False
                Exit Function
            Else
                If MainClass.ValidateWithMasterTable(TxtShipTo.Text, "LOCATION_ID", "LOCATION_ID", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtShippedTo.Text) & "'") = False Then
                    MsgBox("Invalid Location Id for such Customer.", MsgBoxStyle.Information)
                    TxtShipTo.Focus()
                    FieldsVarification = False
                End If
            End If
        End If

        Dim mDeliveryToCode As String = ""

        If txtDeliveryTo.Text = "" Then
            txtDeliveryToLoc.Text = ""
        Else
            'If MainClass.ValidateWithMasterTable((txtDeliveryTo.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            '    MsgBox("Supplier (Delivery To) Does Not Exist In Master", MsgBoxStyle.Information)
            '    'txtSupplier.SetFocus						
            '    FieldsVarification = False
            '    Exit Function
            'Else
            '    mDeliveryToCode = MasterNo
            '    If MainClass.ValidateWithMasterTable((txtDeliveryToLoc.Text), "LOCATION_ID", "LOCATION_ID", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & mDeliveryToCode & "'") = False Then
            '        MsgBox("Supplier (Delivery To LOcation) Does Not Exist In Master", MsgBoxStyle.Information)
            '        'txtSupplier.SetFocus						
            '        FieldsVarification = False
            '        Exit Function
            '    End If

            'End If
            If MainClass.ValidateWithMasterTable((txtDeliveryTo.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invalid Delivery To Supplier Name. Cannot Save")
                If txtDeliveryTo.Enabled = True Then txtDeliveryTo.Focus()
                FieldsVarification = False
                Exit Function
            End If
            If Trim(txtDeliveryToLoc.Text) = "" Then
                MsgInformation("Delivery To Location is blank. Cannot Save")
                txtDeliveryToLoc.Focus()
                FieldsVarification = False
                Exit Function
            Else
                If MainClass.ValidateWithMasterTable(txtDeliveryToLoc.Text, "LOCATION_ID", "LOCATION_ID", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtDeliveryTo.Text) & "'") = False Then
                    MsgBox("Invalid Location Id for such Delivery.", MsgBoxStyle.Information)
                    txtDeliveryToLoc.Focus()
                    FieldsVarification = False
                End If
            End If
        End If

        If ADDMode = True Then
            'If CheckBillToShipFrom(mSupplierCode) = False Then
            '    '        MsgInformation "Invalid Shipped To Supplier Name. Cannot Save"
            '    FieldsVarification = False
            '    Exit Function
            'End If
        End If


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
                    '                If CDate(meBillNoAppDate) >= CDate(txtBillDate.Text) Then
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

        ''------------------------------------------
        ''
        Dim mInterUnitCompanyCode As Long
        Dim mCurrentUnitAccountCode As String
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        If VB.Left(cboRefType.Text, 1) = "I" Or VB.Left(cboRefType.Text, 1) = "3" Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
        Else
            If MainClass.ValidateWithMasterTable(Trim(TxtSupplier.Text), "SUPP_CUST_NAME", "INTERUNIT_COMPANY_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND INTER_UNIT='Y'") = True Then

                mInterUnitCompanyCode = Val(MasterNo)

                mCurrentUnitAccountCode = IIf(IsDBNull(RsCompany.Fields("COMP_AC_CODE").Value), "", RsCompany.Fields("COMP_AC_CODE").Value)

                If mInterUnitCompanyCode > 0 And mCurrentUnitAccountCode <> "" Then
                    If VB.Left(cboRefType.Text, 1) = "J" Or VB.Left(cboRefType.Text, 1) = "R" Or VB.Left(cboRefType.Text, 1) = "F" Then
                        SqlStr = "SELECT AUTO_KEY_PASSNO,GATEPASS_DATE,E_BILLWAYNO,VEHICLE_NO, TRIM(CHALLAN_PREFIX||GATEPASS_NO) AS GATEPASS_NO " & vbCrLf _
                            & " FROM INV_GATEPASS_HDR" & vbCrLf _
                            & " WHERE COMPANY_CODE=" & mInterUnitCompanyCode & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mCurrentUnitAccountCode) & "'" & vbCrLf _
                            & " AND (AUTO_KEY_PASSNO=" & Val(txtBillNo.Text) & " OR TRIM(CHALLAN_PREFIX||GATEPASS_NO)='" & txtBillNo.Text & "')"

                        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                        If RsTemp.EOF = False Then
                            GoTo ExitCond
                            'Else
                            '    MsgInformation("Invalid Bill No.")
                            '    FieldsVarification = False
                            '    Exit Function
                        End If
                    End If

                    SqlStr = "SELECT BILLNO,INVOICE_DATE,CUST_PO_NO,CUST_PO_DATE,E_BILLWAYNO,VEHICLENO " & vbCrLf _
                            & " FROM FIN_INVOICE_HDR" & vbCrLf _
                            & " WHERE COMPANY_CODE=" & mInterUnitCompanyCode & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mCurrentUnitAccountCode) & "'" & vbCrLf _
                            & " AND CANCELLED='N'"

                    If VB.Left(cboRefType.Text, 1) = "P" Then
                        SqlStr = SqlStr & vbCrLf & " AND REF_DESP_TYPE IN ('P','G','F','Q','L','S')"
                    ElseIf VB.Left(cboRefType.Text, 1) = "I" Or VB.Left(cboRefType.Text, 1) = "3" Then
                        SqlStr = SqlStr & vbCrLf & " AND REF_DESP_TYPE IN ('S','Q','L')"
                    ElseIf VB.Left(cboRefType.Text, 1) = "R" Then
                        SqlStr = SqlStr & vbCrLf & " AND REF_DESP_TYPE IN ('J')"
                    End If

                    SqlStr = SqlStr & vbCrLf & " AND BILLNO='" & txtBillNo.Text & "'"

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                    If RsTemp.EOF = True Then
                        MsgInformation("Invalid Bill No.")
                        FieldsVarification = False
                        Exit Function
                    End If

                End If
            End If
        End If

ExitCond:


        ''-----------------------------------------------
        'If Val(txtEwayBillNo.Text) > 0 Then
        '    If WebRequestFetch((txtEwayBillNo.Text)) = True Then

        '    Else
        '        '                MsgInformation "eWay Bill No is Blank, So cann't be Save."
        '        '                FieldsVarification = False
        '        '                txtEwayBillNo.SetFocus
        '        '                Exit Function
        '    End If
        'End If

        If CheckRowCount() = False Then
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "ItemCode Is Blank.") = False Then FieldsVarification = False : Exit Function
        'If MainClass.ValidDataInGrid(SprdMain, ColBillQty, "N", "Please Check Bill Quantity.") = False Then FieldsVarification = False : Exit Function

        mValidQCUser = False
        With SprdMain
            For mRow = 1 To .MaxRows
                .Row = mRow
                .Col = ColItemCode
                If Trim(.Text) = "" Then GoTo NextRow

                ''ERP_CUSTOMER_ID IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102, False, True)
                If (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 And (VB.Left(cboRefType.Text, 1) = "I" Or VB.Left(cboRefType.Text, 1) = "3")) Or RsCompany.Fields("StockBalCheck").Value = "N" Then
                    .Col = ColPODate
                    .Text = IIf(Trim(.Text) = "", txtMRRDate.Text, .Text)
                ElseIf VB.Left(cboRefType.Text, 1) = "F" Then
                    .Col = ColPODate
                    .Text = IIf(Trim(.Text) = "", txtMRRDate.Text, .Text)
                ElseIf RsCompany.Fields("StockBalCheck").Value = "Y" Or VB.Left(cboRefType.Text, 1) = "I" Then

                    If VB.Left(cboRefType.Text, 1) = "I" Then
                        .Col = ColPONo

                        If Val(.Text) < 0 Then
                            .Col = ColPODate
                            .Text = txtMRRDate.Text
                        End If
                    End If
                    If VB.Left(cboRefType.Text, 1) <> "J" Then
                        .Col = ColPODate
                        If Not IsDate(.Text) Then
                            MsgInformation("Please enter the Ref Date.")
                            FormatSprdMain(mRow)
                            MainClass.SetFocusToCell(SprdMain, mRow, ColPONo)
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If
                End If

                .Col = ColItemCode
                mItemCode = Trim(.Text)
                mProdType = GetProductionType(mItemCode)

                Dim mRGPItemCode As String
                Dim mPONo As String
                Dim mRecdQty As Double
                Dim mBalanceQty As Double
                'Dim ii As Integer
                Dim mRGPBalanceQty As Double
                Dim mConsQty As Double
                'Dim mRejQty As Double
                'Dim mVNo As String = ""
                Dim mBillQty As Double

                .Col = ColPONo
                mPONo = .Text

                .Col = ColBillQty
                mBillQty = Val(.Text)

                'If GetOutJobworkManyItem(Trim(.Text), Trim(txtMRRDate.Text)) = False And VB.Left(cboRefType.Text, 1) = "R" Then
                '    .Row = mRow

                '    .Col = ColRGPItemCode
                '    mRGPItemCode = Trim(.Text)

                '    .Col = ColBalQty
                '    mBalanceQty = Val(.Text)


                '    mRGPBalanceQty = CalcRGPBalanceQty(Val(mPONo), mRGPItemCode, mSupplierCode)

                '    mRecdQty = 0

                '    For ii = 1 To .MaxRows
                '        .Row = ii
                '        .Col = ColPONo
                '        If CDbl(mPONo) = Val(.Text) Then
                '            .Col = ColRGPItemCode
                '            If mRGPItemCode = Trim(.Text) Then
                '                .Col = ColItemCode
                '                mConsQty = GetConsQty(mRGPItemCode, Trim(.Text))
                '                .Col = ColBillQty
                '                mRecdQty = mRecdQty + (Val(.Text) * mConsQty)
                '            End If
                '        End If
                '    Next

                '    If mRGPBalanceQty < mRecdQty Then
                '        MsgInformation("RGP [" & mPONo & "] Balance Qty is Less than Received Qty, So cann't be Saved. [" & mRGPItemCode & "]")
                '        MainClass.SetFocusToCell(SprdMain, mRow, ColItemCode)
                '        FieldsVarification = False
                '        Exit Function
                '    End If
                'Else
                If ValidateRefNo(mItemCode, mBillFromSupplier, mPONo, mDivisionCode) = False Then
                    MsgBox("Invalid PO Ref., So cann't be saved. Ref No [" & mPONo & "] & Item Code [ " & mItemCode & "]", MsgBoxStyle.Information)
                    FieldsVarification = False
                    Exit Function
                End If
                'End If

                If VB.Left(cboRefType.Text, 1) = "P" Then ''Or VB.Left(cboRefType.Text, 1) = "I" Or VB.Left(cboRefType.Text, 1) = "2" Or VB.Left(cboRefType.Text, 1) = "3" Then
                    .Col = ColPORate
                    mPORate = Val(SprdMain.Text)

                    If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 And mBillQty > 0 Then
                        mPOHSNCode = GetPOHSNCode(mItemCode, mBillFromSupplier, mPONo, mDivisionCode, Trim(txtMRRDate.Text))

                        .Col = ColHSNCode
                        mHSNCode = Trim(SprdMain.Text)

                        If mPOHSNCode <> mHSNCode Then
                            MsgInformation("For Item Code :" & mItemCode & ", Bill HSN Code (" & mHSNCode & ") is not Match with PO HSN Code : " & mPOHSNCode)
                            MainClass.SetFocusToCell(SprdMain, mRow, ColHSNCode)
                            FieldsVarification = False
                            Exit Function
                        End If

                    End If

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

                If PubInvLevelAPPUser = "N" Then
                    If VB.Left(cboRefType.Text, 1) = "F" Or VB.Left(cboRefType.Text, 1) = "C" Then
                        mProdType = GetProductionType(mItemCode)

                        '                    If mProdType = "B" Or mProdType = "R" Or mProdType = "I" Or mProdType = "P" Then
                        If mProdType = "1" Or mProdType = "2" Then

                        Else
                            MsgInformation("Cann't be save in FOC or Cash.")
                            FieldsVarification = False
                            Exit Function

                            '                        If CheckOpenOrder(mItemCode, mDivisionCode) = True Then
                            '                            MsgInformation "Open Order Made for this Supplier for Item Code : " & mItemCode & ". So cann't be save in FOC or Cash"
                            '                            FieldsVarification = False
                            '                            Exit Function
                            '                        End If
                        End If
                    End If
                End If

                If VB.Left(cboRefType.Text, 1) = "R" Then
                    .Col = ColPONo
                    mRGPPurpose = GetValidRGPPurpose(Val(.Text), "")
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


                If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 And VB.Left(cboRefType.Text, 1) = "I" Then
                ElseIf RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 And VB.Left(cboRefType.Text, 1) = "3" Then
                ElseIf VB.Left(cboRefType.Text, 1) = "I" Or VB.Left(cboRefType.Text, 1) = "3" Then
                    If RsCompany.Fields("StockBalCheck").Value = "Y" Then
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
                End If


                .Col = ColBillQty
                If Val(.Text) > 0 Then
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

                    .Col = ColUnit
                    xUnit = Trim(.Text)

                    Dim mJWUOM As String = ""
                    If VB.Left(cboRefType.Text, 1) = "R" Then
                        If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_JW_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mJWUOM = MasterNo
                        End If

                        If Trim(UCase(mJWUOM)) <> Trim(UCase(xUnit)) Then
                            .Col = ColQtyInKgs
                            If Val(.Text) = 0 Then
                                MsgInformation("Please Enter the Qty in Kgs Also.")
                                FieldsVarification = False
                                Exit Function
                            End If
                        End If
                    End If

                    If ADDMode = True Then
                        If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS='I'") = True Then
                            MsgInformation("Item Status is Closed, So cann't be Saved. [" & mItemCode & "]")
                            MainClass.SetFocusToCell(SprdMain, mRow, ColItemCode)
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If

                    ''28-07-2011 ''Check at the time of save.  '
                    If PubSuperUser = "U" And mInterUnit = "N" Then
                        If VB.Left(cboRefType.Text, 1) = "F" Or VB.Left(cboRefType.Text, 1) = "C" Then
                            If CheckOpenOrder(mItemCode, mDivisionCode) = True Then
                                MsgInformation("Open Order Made for this Supplier for Item Code : " & mItemCode & ". So cann't be save in FOC or Cash")
                                FieldsVarification = False
                                Exit Function
                            End If
                        End If

                    End If


                    If VB.Left(cboRefType.Text, 1) = "P" Then
                        If CheckBillQty(ColBillQty, mRow) = False Then
                            FieldsVarification = False
                            Exit Function
                        End If
                    ElseIf VB.Left(cboRefType.Text, 1) = "I" Or VB.Left(cboRefType.Text, 1) = "1" Or VB.Left(cboRefType.Text, 1) = "R" Or VB.Left(cboRefType.Text, 1) = "2" Or VB.Left(cboRefType.Text, 1) = "3" Then
                        '                    If PubSuperUser = "U" Then
                        If (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104) And VB.Left(cboRefType.Text, 1) = "I" Then
                        ElseIf RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 And VB.Left(cboRefType.Text, 1) = "3" Then
                        ElseIf CheckBillQty(ColBillQty, mRow) = False Then
                            FieldsVarification = False
                            Exit Function
                        End If
                        '                    End If
                    End If

                    If Trim(mItemCode) <> "" Then
                        If CheckItemLock(mItemCode, "M", mSupplierCode) = True Then
                            MsgInformation("Gate Entry Lock For Item Code : " & mItemCode & ". So cann't be made Gate Entry for this Item.")
                            FieldsVarification = False
                            Call MainClass.SetFocusToCell(SprdMain, mRow, ColBillQty)
                            Exit Function
                        End If

                        mMaxLevelCheck = CheckMaxLevel(mItemCode)

                        If mMaxLevelCheck = True Then
                            mMaxLevelQty = GetInventoryLevelQty(mItemCode, "MAXIMUM_QTY")
                            mMRRQty = GetMRRItemQty(mItemCode)
                            mItemCategory = GetProductionType(mItemCode)


                            If VB.Left(cboRefType.Text, 1) = "F" Or VB.Left(cboRefType.Text, 1) = "C" Or VB.Left(cboRefType.Text, 1) = "P" Then
                                If mMaxLevelQty <= 0 Then
                                    MsgInformation("Please define the Max Level Qty For Item Code : " & mItemCode & ". Cann't be made Gate Entry for this Item.")
                                    FieldsVarification = False
                                    Call MainClass.SetFocusToCell(SprdMain, mRow, ColBillQty)
                                    Exit Function
                                End If

                                'If mMaxLevelQty > 0 Then
                                If mMRRQty > mMaxLevelQty Then
                                    If CheckMaxLevelApproval(mItemCode, (txtMRRDate.Text), mSupplierCode, mMRRQty) = False Then
                                        MsgInformation("Gate Entry Qty cann't be More than Max Level Qty (" & mMaxLevelQty & ") For Item Code : " & mItemCode & ". So cann't be made Gate Entry for this Item.")
                                        FieldsVarification = False
                                        Call MainClass.SetFocusToCell(SprdMain, mRow, ColBillQty)
                                        Exit Function
                                    End If
                                End If
                                'End If

                                mStockQty = GetBalanceStockQty(mItemCode, (txtMRRDate.Text), mItemUOM, "STR", "", "", ConWH, mDivisionCode)
                                mStockQty = mStockQty + GetPendingGateEntryQty(mItemCode, (txtMRRDate.Text), mItemUOM, mDivisionCode, Val(txtMRRNo.Text))

                                mStockLockQty = GetInventoryLevelQty(mItemCode, "STOCK_LOCK_QTY")
                                mStockQty = mStockQty - mStockLockQty
                                mStockQty = IIf(mStockQty < 0, 0, mStockQty)


                                If mStockQty + mMRRQty > mMaxLevelQty Then
                                    If CheckMaxLevelApproval(mItemCode, (txtMRRDate.Text), mSupplierCode, mMRRQty) = False Then
                                        MsgInformation("You already cross the Max Level (" & mMaxLevelQty & ")" & " for Item Code : " & mItemCode & ". Stock Qty is (" & mStockQty & "). Approval is Must for Entry.")
                                        FieldsVarification = False
                                        Exit Function
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
NextRow:
            Next
        End With




        If Trim(cboMode.Text) = "" Then
            MsgBox("Please Enter Mode Type.", MsgBoxStyle.Information)
            SSTab1.SelectedIndex = 1
            cboMode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If OptFreight(0).Checked = True Then
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

        Dim mFileExt As String
        If VB.Left(cboRefType.Text, 1) = "P" Then
            For CntRow = 1 To SprdMain.MaxRows - 1
                SprdMain.Row = CntRow
                SprdMain.Col = ColItemCode
                mItemCode = Trim(UCase(SprdMain.Text))

                If CheckTCRequired(mItemCode) = True Then
                    If chkTCAvailable.CheckState = System.Windows.Forms.CheckState.Unchecked And chkTPRAvailable.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                        MsgInformation("If TC is not available than Third Party Report is must.")
                        FieldsVarification = False
                        Exit Function
                    End If

                    '                If chkTPRAvailable.Value = vbUnchecked Then
                    '                    MsgInformation "Please upload Third Party Inspection is required."
                    '                    FieldsVarification = False
                    '                    Exit Function
                    '                End If

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
            Next
        End If

        If mCntRGPPaidType > 0 And mCntRGPFOCType > 0 Then
            If MsgQuestion("You are making Paid and FOC purpose RGP Together in this Gate Entry. Are you want to Continue....") = CStr(MsgBoxResult.No) Then
                FieldsVarification = False
                Exit Function
            End If
        End If

        Exit Function
err_Renamed:
        FieldsVarification = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function
    Private Function ValidateRefNo(ByVal mItemCode As String, ByVal mSupplierCode As String, ByVal mRefNo As String, ByVal mDivisionCode As Double) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        ValidateRefNo = False

        If VB.Left(cboRefType.Text, 1) = "F" Or VB.Left(cboRefType.Text, 1) = "C" Or VB.Left(cboRefType.Text, 1) = "J" Or VB.Left(cboRefType.Text, 1) = "R" Then ''Or Left(cboRefType, 1) = "1"						
            ValidateRefNo = True
            Exit Function
        End If

        If VB.Left(cboRefType.Text, 1) = "I" And mRefNo < 0 Then ''Or Left(cboRefType, 1) = "1"						
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
                SqlStr = "SELECT DISTINCT IH.AUTO_KEY_INVOICE ,IH.INVOICE_DATE, ID.ITEM_CODE, ID.ITEM_QTY, ID.ITEM_DESC " & vbCrLf _
                    & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID" & vbCrLf _
                    & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
                    & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " "

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
    Private Function CheckRowCount() As Boolean
        On Error GoTo ERR1
        Dim I As Integer
        Dim mItemCode As String
        Dim mQty As Double
        Dim mRowCount As Integer
        Dim mTotQty As Double

        mRowCount = 0
        mTotQty = 0
        CheckRowCount = True

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColBillQty
                mQty = Val(.Text)
                mTotQty = mTotQty + mQty

                If mItemCode <> "" And mQty > 0 Then
                    mRowCount = mRowCount + 1
                End If
            Next
        End With

        If mTotQty = 0 Then
            CheckRowCount = False
            MsgInformation("Nothing To Save.")
            Exit Function
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
        CheckRowCount = False
    End Function

    Private Function CheckBillToShipFrom(ByRef mPartyCode As String) As Boolean

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

            mSqlStr = "SELECT IH.SHIPPED_TO_SAMEPARTY, IH.SHIPPED_TO_PARTY_CODE, SUPP_CUST_CODE " & vbCrLf & " FROM PUR_PURCHASE_HDR IH " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_PO=" & xPoNo & " " & vbCrLf & " AND IH.PO_STATUS='Y' AND IH.PO_CLOSED='N'"

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
    Private Function GetMRRItemQty(ByRef mItemCode As String) As Double

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

                .Col = ColUnit
                mPurchaseUOM = Trim(.Text)

                If Trim(UCase(mCheckItemCode)) = Trim(UCase(mItemCode)) Then
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



    Private Function NonApprovedItemExists(ByRef nItemCode As String, ByRef pSuppCode As String) As Boolean

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

    Public Sub FrmGateEntry_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "GATE Entry"

        SqlStr = ""
        SqlStr = "Select * from INV_GATEENTRY_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMRRMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from INV_GATEENTRY_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMRRDetail, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from INV_GATEENTRY_EXP Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMRRExp, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)
        Call SetTextLengths()
        mSupplierCode = CStr(-1)

        cmdAdd.Visible = True
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

        SqlStr = "Select REF_TYPE,GR.AUTO_KEY_GATE as GATE_No1,CONCAT(SUBSTR(GR.AUTO_KEY_GATE,0,LENGTH(GR.AUTO_KEY_GATE)-6),CONCAT('-',SUBSTR(GR.AUTO_KEY_GATE,LENGTH(GR.AUTO_KEY_GATE)-5,LENGTH(GR.AUTO_KEY_GATE)))) as GATE_No," & vbCrLf & " TO_CHAR(GR.GATE_DATE,'DD-MM-YYYY') as GATE_DATE, " & vbCrLf & " AC.SUPP_CUST_NAME AS SupplierName, " & vbCrLf & " GR.BILL_NO, " & vbCrLf & " TO_CHAR(GR.BILL_DATE,'DD-MM-YYYY') AS BillDate " & vbCrLf & " FROM INV_GATEENTRY_HDR GR,FIN_SUPP_CUST_MST AC " & vbCrLf & " WHERE " & vbCrLf & " GR.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUBSTR(AUTO_KEY_GATE,LENGTH(AUTO_KEY_GATE)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND GR.COMPANY_CODE=AC.COMPANY_CODE " & vbCrLf & " AND GR.SUPP_CUST_CODE=AC.SUPP_CUST_CODE " & vbCrLf & " Order by AUTO_KEY_GATE DESC"

        'MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))

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


        'FormatSprdView()
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
            UltraGrid1.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Gate Entry No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(2).Header.Caption = "Gate Entry No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(3).Header.Caption = "Gate Entry Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(4).Header.Caption = "Supplier Name"
            UltraGrid1.DisplayLayout.Bands(0).Columns(5).Header.Caption = "Bill No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(6).Header.Caption = "Bill Date"

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
            UltraGrid1.DisplayLayout.Bands(0).Columns(4).Width = 350
            UltraGrid1.DisplayLayout.Bands(0).Columns(5).Width = 125
            UltraGrid1.DisplayLayout.Bands(0).Columns(6).Width = 100


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
    'Private Sub FormatSprdView()

    '    With SprdView
    '        .Row = -1

    '        .set_RowHeight(0, 600)

    '        .set_ColWidth(0, 600)

    '        .set_ColWidth(1, 600)
    '        .set_ColWidth(2, 1200)
    '        .set_ColWidth(3, 1200)
    '        .set_ColWidth(4, 4500)
    '        .set_ColWidth(5, 1200)
    '        .set_ColWidth(6, 1200)

    '        MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
    '        MainClass.SetSpreadColor(SprdView, -1)
    '        SprdView.set_RowHeight(-1, 300)
    '        .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
    '        MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
    '    End With
    'End Sub
    Private Sub FormatSprdExp(ByRef Arow As Integer)

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
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1

        pShowCalc = False
        With SprdMain
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)

            .Col = ColPONo
            .CellType = SS_CELL_TYPE_EDIT
            '.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            '.TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            '.TypeEditMultiLine = False
            '.CellType = SS_CELL_TYPE_INTEGER
            ''.TypeFloatDecimalPlaces = 0
            ''.TypeFloatDecimalChar = Asc(".")
            ''.TypeFloatMax = CDbl("9999999999999")
            ''.TypeFloatMin = CDbl("-9999999999999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeEditLen = RsMRRMain.Fields("REF_AUTO_KEY_NO").Precision ''
            '        .ColHidden = True
            .set_ColWidth(ColPONo, 10)

            .Col = ColPODate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = 10 ''
            .set_ColWidth(ColPODate, 8)

            .Col = ColRGPItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsMRRDetail.Fields("ITEM_CODE").Precision ''

            .set_ColWidth(ColRGPItemCode, 8)

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
            .set_ColWidth(ColItemName, 25)
            .ColsFrozen = ColItemName

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("WO_DESCRIPTION", "PUR_PURCHASE_DET", PubDBCn)
            .set_ColWidth(ColItemDesc, 35)
            .ColsFrozen = ColItemDesc
            .ColHidden = IIf(VB.Left(cboRefType.Text, 1) = "R", False, True)

            .Col = ColItemPartNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("CUSTOMER_PART_NO", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(ColItemPartNo, 10)

            .Col = ColHSNCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("HSN_CODe", "INV_ITEM_MST", PubDBCn)

            .Col = ColUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = RsMRRDetail.Fields("ITEM_UOM").DefinedSize ''
            .set_ColWidth(ColUnit, 4)

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
            .set_ColWidth(ColBatchNo, 9)

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

            .Col = ColBillQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColBillQty, 9) ''ITEM_QTY_IN_KGS

            .Col = ColQtyInKgs
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColQtyInKgs, 9) ''
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106, False, True)

            .Col = ColPORate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = "999999999.9999"
            .TypeFloatMin = "-999999999.9999"
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColPORate, 9)


            .Col = ColRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.9999")
            .TypeFloatMin = CDbl("-999999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColRate, 9)

            .Col = ColAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .set_ColWidth(ColAmount, 9)

            .Col = ColItemCost
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .ColHidden = True
        End With

        MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols)

        If RsCompany.Fields("StockBalCheck").Value = "N" Or VB.Left(cboRefType.Text, 1) = "I" Then
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColRGPItemCode, ColRGPItemCode)
        Else
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPODate, ColRGPItemCode)
        End If

        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPORate, ColPORate)

        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPOQty, ColBalQty)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColAmount, ColAmount)

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemName, ColItemPartNo)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColUnit, ColUnit)
        Else
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemName, ColUnit)
        End If

        MainClass.SetSpreadColor(SprdMain, Arow)
        pShowCalc = True
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then RsMRRDetail.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SetTextLengths()

        On Error GoTo ERR1
        With RsMRRMain

            txtMRRNo.MaxLength = .Fields("AUTO_KEY_GATE").Precision
            txtMRRDate.MaxLength = 10
            TxtSupplier.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            txtBillNo.MaxLength = .Fields("BILL_NO").DefinedSize
            txtBillDate.MaxLength = 10

            txtOldERPNo.MaxLength = .Fields("OLD_ERP_NO").DefinedSize
            txtOldERPDate.MaxLength = 10

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

            txtShippedTo.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)

            txtDeliveryTo.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            txtDeliveryToLoc.MaxLength = MainClass.SetMaxLength("LOCATION_ID", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn)

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
        Dim mDeliveryToCode As String
        Dim mDeliveryToName As String

        pShowCalc = False
        With RsMRRMain
            If Not .EOF Then
                LblMkey.Text = .Fields("AUTO_KEY_GATE").Value


                txtMRRNo.Text = IIf(IsDBNull(.Fields("AUTO_KEY_GATE").Value), "", .Fields("AUTO_KEY_GATE").Value)
                txtMRRDate.Text = VB6.Format(IIf(IsDBNull(.Fields("GATE_DATE").Value), "", .Fields("GATE_DATE").Value), "DD/MM/YYYY")

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

                txtOldERPNo.Text = IIf(IsDBNull(.Fields("OLD_ERP_NO").Value), "", .Fields("OLD_ERP_NO").Value)
                txtOldERPDate.Text = VB6.Format(IIf(IsDBNull(.Fields("OLD_ERP_DATE").Value), "", .Fields("OLD_ERP_DATE").Value), "DD/MM/YYYY")


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

                chkMRRMade.CheckState = IIf(.Fields("MRR_MADE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                OptFreight(0).Checked = IIf(.Fields("FREIGHT_TYPE").Value = 0, True, False)
                OptFreight(1).Checked = IIf(.Fields("FREIGHT_TYPE").Value = 1, True, False)

                mMode = IIf(IsDBNull(.Fields("MODE_TYPE").Value), "", .Fields("MODE_TYPE").Value)
                cboMode.SelectedIndex = Val(VB.Left(mMode, 1)) - 1

                txtDocsThru.Text = IIf(IsDBNull(.Fields("DOCS_THRU").Value), "", .Fields("DOCS_THRU").Value)
                txtVehicle.Text = IIf(IsDBNull(.Fields("VEHICLE").Value), "", .Fields("VEHICLE").Value)
                txtGRNo.Text = IIf(IsDBNull(.Fields("GRNO").Value), "", .Fields("GRNO").Value)
                txtGRDate.Text = IIf(IsDBNull(.Fields("GRDATE").Value), "", .Fields("GRDATE").Value)

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

                TxtShipTo.Enabled = False
                txtBillTo.Enabled = False
                cmdBillToSearch.Enabled = False
                cmdShipToSearch.Enabled = False


                chkTCAvailable.CheckState = IIf(.Fields("TC_AVAILABLE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkTCAvailable.Enabled = False
                txtTCPath.Text = IIf(IsDBNull(.Fields("TC_FILE_PATH").Value), "", .Fields("TC_FILE_PATH").Value)
                cmdTC.Enabled = False


                chkTPRAvailable.CheckState = IIf(.Fields("TPRI_AVAILABLE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkTPRAvailable.Enabled = IIf(.Fields("TPRI_AVAILABLE").Value = "Y", False, True)
                txtTPRPath.Text = IIf(IsDBNull(.Fields("TPRI_FILE_PATH").Value), "", .Fields("TPRI_FILE_PATH").Value)
                cmdTPRI.Enabled = IIf(.Fields("TPRI_AVAILABLE").Value = "Y", False, True)


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



                FillCboPONo((LblMkey.Text))
                CboPONo.Enabled = True
                cboRefType.Enabled = False
                cboDivision.Enabled = False

                Call ShowDetail1((LblMkey.Text), .Fields("REF_TYPE").Value, Val(.Fields("DIV_CODE").Value))
                Call ShowExp1((LblMkey.Text))
                Call SprdExp_LeaveCell(SprdExp, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColExpAmt, 1, 1, 1, True))
                Call ShowBlobFile()
                '            Call CalcTots
                TxtSupplier.Enabled = False
                cmdsearch.Enabled = False
            End If
        End With
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsMRRMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        SprdMain.Enabled = True
        SprdExp.Enabled = True
        txtMRRNo.Enabled = True

        pShowCalc = True
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub

    Private Sub ShowBlobFileFromPO(ByRef mPNO As Double)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim sTempDir As String
        Dim mFilename As String
        Dim mKey As String
        Dim mPOMKey As Double

        lngImgSiz = 0
        lngOffset = 0
        SqlStr = " SELECT MAX(IH.MKEY) AS MKEY" & vbCrLf & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID" & vbCrLf & " WHERE IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY AND IH.COMPANY_CODE=ID.COMPANY_CODE " & vbCrLf & " AND IH.AUTO_KEY_PO =" & Val(CStr(mPNO)) & "" & vbCrLf & " And IH.PO_STATUS='Y'" & vbCrLf & " AND ID.PO_WEF_DATE<=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND ID.PO_WEF_DATE>=TO_DATE('" & VB6.Format(PubGSTApplicableDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mPOMKey = IIf(IsDBNull(RsTemp.Fields("mKey").Value), -1, RsTemp.Fields("mKey").Value)
        Else
            Exit Sub
        End If


        SqlStr = " SELECT MKEY, TC_DOC_DESC, TC_DOC_EXT, TPR_DOC_DESC, TPR_DOC_EXT, TC_BLOB_DATA, TPR_BLOB_DATA " & vbCrLf & " FROM PUR_PURCHASE_TC_TRN " & vbCrLf & " WHERE MKEY = " & mPOMKey & ""


        '            (" & vbCrLf _
        ''            & " SELECT MAX(IH.MKEY) " & vbCrLf _
        ''            & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID" & vbCrLf _
        ''            & " WHERE IH.Company_Code=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
        ''            & " AND IH.MKEY=ID.MKEY AND IH.COMPANY_CODE=ID.COMPANY_CODE " & vbCrLf _
        ''            & " AND IH.AUTO_KEY_PO =" & Val(mPNO) & "" & vbCrLf _
        ''            & " And IH.PO_STATUS='Y'" & vbCrLf _
        ''            & " AND ID.PO_WEF_DATE<=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
        ''            & " AND ID.PO_WEF_DATE>=TO_DATE('" & VB6.Format(PubGSTApplicableDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"


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
            chkTCAvailable.Enabled = False
            cmdTC.Enabled = False
        End If
        ' Second File


        lngImgSiz = 0
        lngOffset = 0
        StrTempPic = ""

        SqlStr = " SELECT MKEY, TC_DOC_DESC, TC_DOC_EXT, TPR_DOC_DESC, TPR_DOC_EXT, TC_BLOB_DATA, TPR_BLOB_DATA " & vbCrLf & " FROM PUR_PURCHASE_TC_TRN " & vbCrLf & " WHERE MKEY = " & mPOMKey & ""


        '            (" & vbCrLf _
        ''            & " SELECT MAX(IH.MKEY) " & vbCrLf _
        ''            & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID" & vbCrLf _
        ''            & " WHERE IH.Company_Code=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
        ''            & " AND IH.MKEY=ID.MKEY AND IH.COMPANY_CODE=ID.COMPANY_CODE " & vbCrLf _
        ''            & " AND IH.AUTO_KEY_PO =" & Val(mPNO) & "" & vbCrLf _
        ''            & " And IH.PO_STATUS='Y'" & vbCrLf _
        ''            & " AND ID.PO_WEF_DATE<=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
        ''            & " AND ID.PO_WEF_DATE>=TO_DATE('" & VB6.Format(PubGSTApplicableDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

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
        If txtTPRPath.Text = "" Then
            chkTPRAvailable.CheckState = System.Windows.Forms.CheckState.Unchecked
            chkTPRAvailable.Enabled = True
            cmdTPRI.Enabled = True
        Else
            chkTPRAvailable.CheckState = System.Windows.Forms.CheckState.Checked
            chkTPRAvailable.Enabled = False
            cmdTPRI.Enabled = False
        End If

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume
    End Sub

    Private Sub ShowBlobFile()
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim sTempDir As String
        Dim mFilename As String

        lngImgSiz = 0
        lngOffset = 0

        SqlStr = " SELECT MKEY, TC_DOC_DESC, TC_DOC_EXT, TPR_DOC_DESC, TPR_DOC_EXT, TC_BLOB_DATA, TPR_BLOB_DATA " & vbCrLf & " FROM INV_GATEENTRY_TC_TRN " & vbCrLf & " WHERE MKEY = '" & LblMkey.Text & "'"

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

        ' Second File


        lngImgSiz = 0
        lngOffset = 0
        StrTempPic = ""

        SqlStr = " SELECT MKEY, TC_DOC_DESC, TC_DOC_EXT, TPR_DOC_DESC, TPR_DOC_EXT, TC_BLOB_DATA, TPR_BLOB_DATA " & vbCrLf & " FROM INV_GATEENTRY_TC_TRN " & vbCrLf & " WHERE MKEY = '" & LblMkey.Text & "'"

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


        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume
    End Sub

    Private Sub FillCboPONo(ByRef mMKEY As String)

        On Error GoTo FillERR
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        SqlStr = ""
        SqlStr = " SELECT Distinct GRD.REF_AUTO_KEY_NO " & vbCrLf & " FROM INV_GATEENTRY_HDR GRD" & vbCrLf & " WHERE AUTO_KEY_GATE='" & UCase(mMKEY) & "'"

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
    Private Sub ShowExp1(ByRef mMKEY As String)

        On Error GoTo ERR1
        Dim I As Integer
        Dim pExpId As String
        Dim SqlStr As String = ""

        Call FillSprdExp()
        pShowCalc = False
        SqlStr = ""
        SqlStr = "Select INV_GATEENTRY_EXP.EXPCODE,INV_GATEENTRY_EXP.EXPPERCENT, " & vbCrLf & " INV_GATEENTRY_EXP.AMOUNT, " & vbCrLf & " FIN_INTERFACE_MST.Name as Name,FIN_INTERFACE_MST.Code, " & vbCrLf & " Identification,Add_Ded,Taxable,Exciseable,CalcOn,RO,DebitAmount " & vbCrLf & " From INV_GATEENTRY_EXP,FIN_INTERFACE_MST " & vbCrLf & " Where " & vbCrLf & " FIN_INTERFACE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND INV_GATEENTRY_EXP.ExpCode=FIN_INTERFACE_MST.Code " & vbCrLf & " AND INV_GATEENTRY_EXP.AUTO_KEY_GATE='" & mMKEY & "'"

        If PubGSTApplicable = True Then
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='Y'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND GST_ENABLED='N'"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY SERIAL_NO"

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
        Dim mRefPoNoStr As String
        Dim mRGPItemCode As String
        Dim mOpenOrder As Boolean
        Dim pSupplierCode As String = ""
        Dim mHSNCode As String
        Dim mItemPartNo As String

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

        SqlStr = ""
        SqlStr = " SELECT ID.*, "

        If pRefType = "I" Or pRefType = "2" Or pRefType = "3" Then
            SqlStr = SqlStr & " GetSALEITEMPRICE(REF_AUTO_KEY_NO,REF_AUTO_KEY_NO, '" & pSupplierCode & "',ITEM_CODE) AS PORATE "
        ElseIf pRefType = "P" Then
            SqlStr = SqlStr & " GetITEMPRICE_NEW(1,1,TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),REF_AUTO_KEY_NO, ITEM_CODE) AS PORATE "
        Else
            SqlStr = SqlStr & " 0 AS PORATE "
        End If

        SqlStr = SqlStr & vbCrLf _
            & " FROM INV_GATEENTRY_DET ID" & vbCrLf _
            & " Where AUTO_KEY_GATE=" & Val(mMKEY) & "" & vbCrLf _
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
                    mRefPoNoStr = CStr(mRefPoNo)
                Else
                    mRefPoNo = Val(IIf(IsDBNull(.Fields("REF_AUTO_KEY_NO").Value), -1, .Fields("REF_AUTO_KEY_NO").Value))
                    mRefPoNoStr = IIf(IsDBNull(.Fields("REF_AUTO_KEY_NO").Value), -1, .Fields("REF_AUTO_KEY_NO").Value)
                End If

                '            mRefPoNo = Val(IIf(IsNull(!REF_PO_NO), -1, !REF_PO_NO))

                SprdMain.Text = mRefPoNoStr ''mRefPoNo

                SprdMain.Col = ColPODate
                SprdMain.Text = IIf(IsDBNull(.Fields("REF_DATE").Value), "", .Fields("REF_DATE").Value)

                SprdMain.Col = ColRGPItemCode
                mRGPItemCode = Trim(IIf(IsDBNull(.Fields("RGP_ITEM_CODE").Value), "", .Fields("RGP_ITEM_CODE").Value))
                SprdMain.Text = Trim(mRGPItemCode)

                SprdMain.Col = ColItemCode
                mItemCode = Trim(IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))
                SprdMain.Text = Trim(mItemCode)

                SprdMain.Col = ColItemName
                mItemName = ""
                If MainClass.ValidateWithMasterTable(Trim(mItemCode), "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mItemName = MasterNo
                End If
                SprdMain.Text = mItemName

                'mukul'
                SprdMain.Col = ColItemDesc
                If String.IsNullOrEmpty(mItemCode) Then
                    mItemDesc = ""
                Else

                    mItemDesc = GetItemDescription(mItemCode)
                End If
                SprdMain.Text = mItemDesc

                SprdMain.Col = ColItemPartNo
                mItemPartNo = ""
                If MainClass.ValidateWithMasterTable(Trim(mItemCode), "Item_Code", "CUSTOMER_PART_NO", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mItemPartNo = MasterNo
                End If

                SprdMain.Text = mItemPartNo


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

                'MainClass.ValidateWithMasterTable(Trim(mItemCode), "Item_Code", "HSN_CODE", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                'SprdMain.Text = MasterNo

                SprdMain.Col = ColUnit
                SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)

                SprdMain.Col = ColHeatNo
                SprdMain.Text = IIf(IsDBNull(.Fields("HEAT_NO").Value), "", .Fields("HEAT_NO").Value)

                SprdMain.Col = ColBatchNo
                SprdMain.Text = IIf(IsDBNull(.Fields("BATCH_NO").Value), "", .Fields("BATCH_NO").Value)

                If .Fields("REF_TYPE").Value = "R" Or .Fields("REF_TYPE").Value = "I" Or .Fields("REF_TYPE").Value = "1" Or .Fields("REF_TYPE").Value = "2" Or .Fields("REF_TYPE").Value = "3" Then
                    Call CollectPOData(.Fields("REF_TYPE").Value, mRefPoNoStr, Trim(mItemCode), Trim(mRGPItemCode), I, mDivisionCode)
                Else
                    mPOQty = CalcPOQty(pSupplierCode, mRefPoNo, mItemCode, pRefType, mOpenOrder, mDivisionCode)

                    SprdMain.Row = I
                    SprdMain.Col = ColPOQty
                    SprdMain.Text = VB6.Format(mPOQty, "0.00")

                    mRecdQty = CalcRecvQty(mRefPoNo, mItemCode, mSupplierCode, mOpenOrder)
                    mBalQty = mPOQty - mRecdQty ''+ Val(IIf(IsNull(!RECEIVED_QTY), 0, !RECEIVED_QTY))

                    SprdMain.Row = I
                    SprdMain.Col = ColBalQty
                    SprdMain.Text = VB6.Format(mBalQty, "0.0000")

                    SprdMain.Col = ColPORate
                    SprdMain.Text = Val(IIf(IsDBNull(.Fields("PORATE").Value), 0, .Fields("PORATE").Value))
                End If

                SprdMain.Row = I
                SprdMain.Col = ColBillQty
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("BILL_QTY").Value), 0, .Fields("BILL_QTY").Value)))

                SprdMain.Col = ColRate
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_RATE").Value), 0, .Fields("ITEM_RATE").Value)))

                SprdMain.Col = ColItemCost
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_COST").Value), 0, .Fields("ITEM_COST").Value)))

                SprdMain.Col = ColQtyInKgs
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEM_QTY_IN_KGS").Value), 0, .Fields("ITEM_QTY_IN_KGS").Value)))

                SprdMain.Col = ColRemarks
                SprdMain.Text = IIf(IsDBNull(.Fields("Remarks").Value), "", .Fields("Remarks").Value)

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

                .Col = ColBillQty
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

    Private Sub CalcExpTots(ByRef mTotAmt As Double)
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
        lblTotSGST.Text = VB6.Format(mTotSGST, "#0.00")
        lblTotCGST.Text = VB6.Format(mTotCGST, "#0.00")
        lblTotIGST.Text = VB6.Format(mTotIGST, "#0.00")
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
        '        On Error GoTo ERR1

        '        Dim mNetAccessAmt As Double
        '        Dim mExciseableAmount As Double
        '        Dim mTaxableAmount As Double
        '        Dim mModvatableAmount As Double
        '        Dim mTotModvatableAmount As Double
        '        Dim mTotServiceableAmount As Double
        '        Dim mTotSTRefundableAmt As Double
        '        'Dim mShortage As Double
        '        Dim mCEDCessAble As Double
        '        Dim mADDCessAble As Double
        '        Dim mCESSableAmount As Double
        '        Dim mTotItemAmount As Double
        '        Dim pTotExciseDuty As Double
        '        Dim pTotEduCess As Double
        '        Dim pTotSHECess As Double
        '        Dim pTotADE As Double
        '        Dim pTotExportExp As Double
        '        Dim pTotOthers As Double
        '        Dim pTotSalesTax As Double
        '        Dim pTotSurcharge As Double
        '        Dim pTotCustomDuty As Double
        '        Dim pTotAddCess As Double
        '        Dim pTotCustomDutyExport As Double
        '        Dim pTotCustomDutyCess As Double
        '        Dim pTotMSC As Double
        '        Dim pTotDiscount As Double
        '        Dim pTotServiceTax As Double
        '        Dim pTotRO As Double
        '        Dim pTotTCS As Double
        '        Dim mTotExp As Double
        '        Dim pEDPer As Double
        '        Dim pSTPer As Double
        '        Dim pServPer As Double
        '        Dim pCessPer As Double
        '        Dim pSHECPer As Double
        '        Dim pTCSPer As Double
        '        Dim pTotKKCAmount As Double

        '        Dim mTotIGST As Double
        '        Dim mTotSGST As Double
        '        Dim mTotCGST As Double

        '        mNetAccessAmt = Val(CStr(mTotAmt))
        '        mExciseableAmount = Val(lblTotItemValue.Text)
        '        mTaxableAmount = Val(lblTotItemValue.Text)



        '        'Call BillExpensesCalcTots(SprdExp, (txtMRRDate.Text), False, mNetAccessAmt, mExciseableAmount, mTaxableAmount, mCEDCessAble, mADDCessAble, mCESSableAmount, mTotItemAmount, pTotExciseDuty, pTotEduCess, pTotSHECess, pTotADE, pTotExportExp, pTotOthers, pTotSalesTax, pTotSurcharge, pTotCustomDuty, pTotAddCess, pTotCustomDutyExport, pTotCustomDutyCess, pTotMSC, pTotDiscount, pTotServiceTax, pTotRO, pTotTCS, mTotExp, pEDPer, pSTPer, pServPer, pCessPer, pSHECPer, pTCSPer, "MRR", mNetAccessAmt, pTotKKCAmount)

        '        Call BillExpensesCalcTots_GST(SprdExp, txtMRRDate.Text, mNetAccessAmt, mTotItemAmount, mTaxableAmount,
        '                                0, 0, 0, mTotIGST, mTotSGST, mTotCGST, pTotExportExp, 0, 0, pTotOthers,
        '                                pTotCustomDutyExport, pTotCustomDuty, pTotMSC, pTotDiscount,
        '                                0, pTotRO, pTotTCS, mTotExp, pTCSPer, "S")

        '        lblTotItemValue.Text = VB6.Format(mTotAmt, "#0.00")
        '        lblTotIGST.Text = VB6.Format(mTotIGST, "#0.00")
        '        lblTotSGST.Text = VB6.Format(mTotSGST, "#0.00")
        '        lblTotCGST.Text = VB6.Format(mTotCGST, "#0.00")
        '        lblEDUAmount.Text = VB6.Format(pTotEduCess, "#0.00")
        '        lblNetAmount.Text = VB6.Format(mTotExp + mTotAmt, "#0.00")
        '        lblTotFreight.Text = VB6.Format(pTotOthers, "#0.00")
        '        lblTotCharges.Text = CStr(0) ''VB6.Format(mRO, "#0.00")
        '        lblTotExpAmt.Text = VB6.Format(mTotExp, "#0.00")
        '        lblTotTaxableAmt.Text = VB6.Format(mTaxableAmount, "#0.00")

        '        lblRO.Text = VB6.Format(pTotRO, "#0.00")
        '        lblDiscount.Text = VB6.Format(pTotDiscount, "#0.00")
        '        lblSurcharge.Text = VB6.Format(pTotSurcharge, "#0.00")
        '        lblMSC.Text = VB6.Format(pTotMSC, "#0.00")
        '        '    lblTotQty.text = VB6.Format(mTotQty, "#0.00")

        '        Call CalcLandedCost()

        '        Exit Sub
        'ERR1:
        '        ''Resume
        '        If Err.Number = 6 Then Resume Next 'OverFlow
        '        MsgInformation(Err.Description)

    End Sub

    Private Sub Clear1()

        pShowCalc = False
        pTempUpdate = False
        LblMkey.Text = ""
        CboPONo.Enabled = True
        cboRefType.Enabled = True

        cboDivision.Enabled = True
        cboDivision.Text = GetDefaultDivision()        '-1  cboDivision.SelectedIndex = -1

        SSTab1.SelectedIndex = 0

        mSupplierCode = CStr(-1)
        txtMRRNo.Text = ""
        txtMRRDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        lblEntryDate.Text = VB6.Format(PubCurrDate, "DD/MM/YYYY") & " " & GetServerTime()

        fraFreight.Enabled = True

        cboRefType.SelectedIndex = 0
        CboPONo.Text = ""
        CboPONo.Items.Clear()
        TxtSupplier.Text = ""
        txtBillNo.Text = ""
        txtBillDate.Text = "" 'VB6.Format(RunDate, "DD/MM/YYYY")

        txtOldERPNo.Text = ""
        txtOldERPDate.Text = ""

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

        txtMRRDate.Enabled = True 'False
        txtBillDate.Enabled = True
        TxtSupplier.Enabled = True
        cmdsearch.Enabled = True

        chkTCAvailable.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkTCAvailable.Enabled = False
        txtTCPath.Text = ""
        cmdTC.Enabled = False

        chkTPRAvailable.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkTPRAvailable.Enabled = True
        txtTPRPath.Text = ""
        cmdTPRI.Enabled = False


        chkMRRMade.CheckState = System.Windows.Forms.CheckState.Unchecked

        lblAddUser.Text = ""
        lblAddDate.Text = ""
        lblModUser.Text = ""
        lblModDate.Text = ""

        txtShippedTo.Text = ""
        TxtShipTo.Text = ""
        txtDeliveryTo.Text = ""
        txtDeliveryToLoc.Text = ""

        chkShipTo.Enabled = True
        chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked
        txtShippedTo.Enabled = False
        cmdSearchShippedTo.Enabled = False

        chkUnderChallan.Enabled = True
        chkUnderChallan.CheckState = System.Windows.Forms.CheckState.Unchecked

        lblTotQty.Text = VB6.Format(0, "#0.00")
        lblTotItemValue.Text = VB6.Format(0, "#0.00")
        lblTotCGST.Text = VB6.Format(0, "#0.00")
        lblTotIGST.Text = VB6.Format(0, "#0.00")
        lblTotSGST.Text = VB6.Format(0, "#0.00")
        lblEDUAmount.Text = VB6.Format(0, "#0.00")
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

        txtBillTo.Text = ""
        TxtShipTo.Text = ""


        txtBillTo.Enabled = True
        TxtShipTo.Enabled = False
        cmdBillToSearch.Enabled = True
        cmdShipToSearch.Enabled = False

        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)
        MainClass.ClearGrid(SprdExp)
        Call FillSprdExp()
        pShowCalc = True
        MainClass.ButtonStatus(Me, XRIGHT, RsMRRMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub FillSprdExp()

        On Error GoTo ERR1
        Dim mLocal As String
        Dim SqlStr As String = ""

        Dim RS As ADODB.Recordset = Nothing
        Dim I As Integer
        pShowCalc = False
        MainClass.ClearGrid(SprdExp)

        If Trim(TxtSupplier.Text) <> "" Then
            mLocal = GetPartyBusinessDetail(TxtSupplier.Text, Trim(txtBillTo.Text), "WITHIN_STATE")
            'If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "WITHIN_STATE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mLocal = IIf(mLocal = "Y", "L", "C")
            'Else
            '    mLocal = ""
            'End If
        Else
            mLocal = ""
        End If

        SqlStr = "Select * From FIN_INTERFACE_MST " & vbCrLf & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " And (Type='P' OR Type='B') "

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
        FormatSprdExp(-1)
        pShowCalc = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume
    End Sub
    Private Sub FrmGateEntry_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
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

    Private Sub FrmGateEntry_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        pXRIGHT = XRIGHT
        xMyMenu = myMenu
        MainClass.RightsToButton(Me, XRIGHT)

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

    Private Sub FillCombo()

        On Error GoTo FillErr2
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

        'cboDivision.Text = GetDefaultDivision()        '

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
        cboRefType.Items.Add("4 - Inter Unit Purchase")

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
    Private Sub txtOldERPDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOldERPDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOldERPNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOldERPNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOldERPNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOldERPNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        '    KeyAscii = MainClass.SetNumericField(KeyAscii)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtOldERPNo.Text, "Y", "N")
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


    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String

        mSqlStr = mSqlStr & "SELECT * " & " FROM Temp_PO " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY AUTO_KEY_PO,AMEND_NO"

        FetchRecordForReport = mSqlStr

    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String)
        'Dim Printer As New Printer
        On Error GoTo ErrPart
        Dim mAmountInword As String


        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)


        Report1.ReportFileName = PubReportFolderPath & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        'Dim prt As Printer
        'If PubUniversalPrinter = "Y" And mMode = Crystal.DestinationConstants.crptToPrinter Then
        '    'For Each prt In Printers
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
    Private Function CollectPOData(ByVal xRefType As String, ByRef xPoNo As String, ByRef xItemCode As String, ByRef xOutItemCode As String, ByRef mRowNo As Integer, ByRef mDivisionCode As Double) As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsPO As ADODB.Recordset = Nothing
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xFYNo As Integer
        Dim jj As Integer
        Dim mSprdRowNo As Integer
        Dim mInwardItemCode As String
        'Dim RsTemp As ADODB.Recordset = Nothing
        Dim mDeliveryToCode As String = ""
        Dim mDeliveryToName As String = ""

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

            'And POM.PO_STATUS='Y'
            'And IH.PO_STATUS='Y'

            Case "P"

                SqlStr = " SELECT POM.*, " & vbCrLf & " POD.*, (NVL(ITEM_PRICE,0) - ROUND((NVL(ITEM_PRICE,0) * ITEM_DIS_PER)/100,2)) AS I_RATE, " & vbCrLf _
                    & " AC.SUPP_CUST_NAME as SuppName, POD.REMARKS AS REMARKS_DETAILS " & vbCrLf _
                    & " FROM PUR_PURCHASE_HDR POM,PUR_PURCHASE_DET POD,FIN_SUPP_CUST_MST AC " & vbCrLf _
                    & " WHERE POM.MKEY = POD.MKEY " & vbCrLf _
                    & " And POM.Company_Code = AC.Company_Code " & vbCrLf _
                    & " And POM.SUPP_CUST_CODE = AC.SUPP_CUST_CODE " & vbCrLf _
                    & " And POM.AUTO_KEY_PO=" & Val(xPoNo) & " " & vbCrLf _
                    & " And POM.SUPP_CUST_CODE='" & pSupplierCode & "' " & vbCrLf _
                    & " And POM.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " And POD.ITEM_CODE='" & Trim(xItemCode) & "' "

                If RsCompany.Fields("PO_IN_GE").Value = "Y" Then
                    SqlStr = SqlStr & vbCrLf & " And POM.PO_STATUS='Y'"
                End If

                SqlStr = SqlStr & vbCrLf _
                    & " AND POM.DIV_CODE=" & mDivisionCode & "" & vbCrLf _
                    & " AND POM.MKEY = ( " & vbCrLf _
                    & " SELECT MAX(IH.MKEY) " & vbCrLf _
                    & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID" & vbCrLf _
                    & " WHERE IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.AUTO_KEY_PO =" & Val(xPoNo) & ""

                If RsCompany.Fields("PO_IN_GE").Value = "Y" Then
                    SqlStr = SqlStr & vbCrLf & " And IH.PO_STATUS='Y'"
                End If

                SqlStr = SqlStr & vbCrLf _
                    & " AND IH.DIV_CODE=" & mDivisionCode & " And ID.ITEM_CODE='" & Trim(xItemCode) & "'" & vbCrLf _
                    & " AND ID.PO_WEF_DATE<=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


                'SqlStr = SqlStr & vbCrLf & " AND ID.PO_WEF_DATE>=TO_DATE('" & VB6.Format(PubGSTApplicableDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"



                '                If PubGSTApplicable = True Then
                '                    SqlStr = SqlStr & vbCrLf & " AND ID.PO_WEF_DATE>=TO_DATE('" & VB6.Format(PubGSTApplicableDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                '                End If

                SqlStr = SqlStr & ")"


                SqlStr = SqlStr & vbCrLf & " ORDER BY POD.SERIAL_NO"

            Case "R"

                If xItemCode = xOutItemCode Then
                    mOutConUnit = 1
                Else


                    Dim xWONo As Double
                    Dim mIsReprocess As String
                    Dim RsProcessTemp As ADODB.Recordset

                    xWONo = -1
                    If MainClass.ValidateWithMasterTable(xPoNo, "AUTO_KEY_PASSNO", "AUTO_KEY_WO", "INV_GATEPASS_DET", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE='" & xOutItemCode & "'") = True Then
                        xWONo = MasterNo
                    End If

                    SqlStr = " SELECT DISTINCT IS_REPROCESS " & vbCrLf _
                        & " FROM PUR_PURCHASE_HDR POM, PUR_PURCHASE_DET POD " & vbCrLf _
                        & " WHERE POM.MKEY = POD.MKEY " & vbCrLf _
                        & " And POM.AUTO_KEY_PO=" & Val(xWONo) & " " & vbCrLf _
                        & " And POM.SUPP_CUST_CODE='" & pSupplierCode & "' " & vbCrLf _
                        & " And POM.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                        & " And POD.ITEM_CODE='" & Trim(xItemCode) & "' "

                    SqlStr = SqlStr & vbCrLf _
                        & " AND POM.DIV_CODE=" & mDivisionCode & "" & vbCrLf _
                        & " AND POM.MKEY = ( " & vbCrLf _
                        & " SELECT MAX(IH.MKEY) " & vbCrLf _
                        & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID" & vbCrLf _
                        & " WHERE IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.AUTO_KEY_PO =" & Val(xWONo) & ""


                    SqlStr = SqlStr & vbCrLf _
                        & " AND IH.DIV_CODE=" & mDivisionCode & " And ID.ITEM_CODE='" & Trim(xItemCode) & "'" & vbCrLf _
                        & " AND ID.PO_WEF_DATE<=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                    SqlStr = SqlStr & ")"


                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProcessTemp, ADODB.LockTypeEnum.adLockReadOnly)

                    mIsReprocess = "N"

                    If RsProcessTemp.EOF = False Then
                        mIsReprocess = Trim(IIf(IsDBNull(RsProcessTemp.Fields("IS_REPROCESS").Value), "N", RsProcessTemp.Fields("IS_REPROCESS").Value))
                    End If


                    SqlStr = "SELECT A.RM_CODE AS ITEM_CODE,  B.ITEM_SHORT_DESC, B.ISSUE_UOM, (NVL(A.STD_QTY,0) + NVL(A.GROSS_WT_SCRAP,0)) AS STD_QTY" & vbCrLf _
                            & " FROM VW_PRD_BOM_TRN A, INV_ITEM_MST B" & vbCrLf _
                            & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                            & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf & " AND A.RM_CODE=B.ITEM_CODE "

                    If mIsReprocess = "Y" Then
                        SqlStr = SqlStr & vbCrLf _
                            & " START WITH  TRIM(A.RM_CODE) || '-' || A.COMPANY_CODE='" & MainClass.AllowSingleQuote(xItemCode) & "-" & RsCompany.Fields("COMPANY_CODE").Value & "'" & vbCrLf _
                            & " CONNECT BY NOCYCLE (TRIM(A.RM_CODE) || A.COMPANY_CODE) || ' '=PRIOR TRIM(A.PRODUCT_CODE) || A.COMPANY_CODE || ' '"
                    Else
                        SqlStr = SqlStr & vbCrLf _
                            & " START WITH  TRIM(A.PRODUCT_CODE) || '-' || A.COMPANY_CODE='" & MainClass.AllowSingleQuote(xItemCode) & "-" & RsCompany.Fields("COMPANY_CODE").Value & "'" & vbCrLf _
                            & " CONNECT BY NOCYCLE (TRIM(A.PRODUCT_CODE) || A.COMPANY_CODE) || ' '=PRIOR TRIM(A.RM_CODE) || A.COMPANY_CODE || ' '"
                    End If





                    'SqlStr = "SELECT IH.MKEY, IH.PRODUCT_CODE , ID.RM_CODE AS ITEM_CODE,(STD_QTY+GROSS_WT_SCRAP) as ITEM_QTY " & vbCrLf _
                    '        & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID " & vbCrLf _
                    '        & " WHERE " & vbCrLf _
                    '        & " IH.MKEY=ID.MKEY" & vbCrLf _
                    '        & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    '        & " AND IH.PRODUCT_CODE='" & xItemCode & "'"

                    'SqlStr = SqlStr & vbCrLf _
                    '        & " AND IH.WEF = (" & vbCrLf _
                    '        & " SELECT MAX(WEF) " & vbCrLf _
                    '        & " FROM PRD_NEWBOM_HDR " & vbCrLf _
                    '        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    '        & " AND PRODUCT_CODE='" & xItemCode & "'" & vbCrLf _
                    '        & " AND WEF<=TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                    'If ADDMode = True Then
                    '    SqlStr = SqlStr & " AND STATUS='O')"
                    'Else
                    '    SqlStr = SqlStr & ")"
                    'End If

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                    If RsTemp.EOF = True Then
                        SqlStr = " SELECT DISTINCT RMMST.ITEM_CODE, RMMST.ITEM_SHORT_DESC, RMMST.ISSUE_UOM,  " & vbCrLf _
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
                            mCheckOutItem = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), 0, RsTemp.Fields("ITEM_CODE").Value))
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
                            SqlStr = " SELECT DISTINCT RMMST.ITEM_CODE, RMMST.ITEM_SHORT_DESC, RMMST.ISSUE_UOM,  " & vbCrLf _
                                & " 1 AS STD_QTY" & vbCrLf _
                                & " FROM INV_ITEM_MST HMST, INV_ITEM_MST RMMST" & vbCrLf _
                                & " WHERE RMMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                                & " AND RMMST.COMPANY_CODE=HMST.COMPANY_CODE" & vbCrLf _
                                & " AND TRIM(RMMST.PARENT_CODE)=TRIM(HMST.ITEM_CODE) AND RMMST.ITEM_CODE = '" & Trim(xItemCode) & "'"

                            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                            If RsTemp.EOF = False Then
                                mOutConUnit = 1
                            Else
                                MsgInformation("Please check BOM of Item Code : " & xItemCode)
                                CollectPOData = False
                                Exit Function
                            End If

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

                SqlStr = " SELECT POM.*, " & vbCrLf _
                    & " POD.AUTO_KEY_PASSNO,POD.SERIAL_NO,POD.ITEM_CODE,POD.ITEM_UOM,POD.STOCK_TYPE,POD.ITEM_QTY, " & vbCrLf _
                    & " POD.RTN_QTY,POD.REMARKS,POD.ITEM_RATE ORATE,"


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

                ''             & "     --And SPD.PO_WEF_DATE <=  POD.GATEPASS_DATE " & vbCrLf _

                SqlStr = SqlStr & vbCrLf _
                    & " POD.GATEPASS_DATE,POD.COMPANY_CODE,POD.DEPT_CODE, " & vbCrLf _
                    & " POD.INPUT_FROM_FLAG,POD.GATEPASS_TYPE,POD.REQ_NO,POD.REQ_EMP_CODE,POD.OUTWARD_57F4NO, " & vbCrLf _
                    & " POD.GATEPASS_NO,POD.SUPP_CUST_CODE,POD.F4NO,POD.LOT_NO,POD.AUTO_KEY_WO,POD.HSN_CODE, " & vbCrLf _
                    & " POD.AMOUNT,POD.CGST_PER,POD.SGST_PER,POD.IGST_PER, POD.CGST_AMOUNT,POD.SGST_AMOUNT, " & vbCrLf _
                    & " POD.IGST_AMOUNT,POD.BATCH_NO,POD.HEAT_NO, CASE WHEN '" & xItemCode & "' = '" & xOutItemCode & "' THEN  POD.ITEM_CODE ELSE POD.INWARD_ITEM_CODE END AS INWARD_ITEM_CODE," & vbCrLf _
                    & " AC.SUPP_CUST_NAME as SuppName " & vbCrLf _
                    & " FROM INV_GATEPASS_HDR POM,INV_GATEPASS_DET POD,FIN_SUPP_CUST_MST AC " & vbCrLf _
                    & " WHERE POM.AUTO_KEY_PASSNO = POD.AUTO_KEY_PASSNO " & vbCrLf _
                    & " And POM.Company_Code = AC.Company_Code " & vbCrLf _
                    & " And POM.SUPP_CUST_CODE = AC.SUPP_CUST_CODE " & vbCrLf _
                    & " And POM.AUTO_KEY_PASSNO=" & Val(xPoNo) & " " & vbCrLf _
                    & " And POM.SUPP_CUST_CODE='" & pSupplierCode & "' " & vbCrLf _
                    & " And POM.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " and POM.GATEPASS_STATUS='N' AND POD.ITEM_CODE = '" & Trim(xOutItemCode) & "'" & vbCrLf _
                    & " order by POD.SERIAL_NO"

            Case "I", "1", "2", "3"
                SqlStr = " SELECT POM.INVOICE_DATE, " & vbCrLf _
                    & " POD.ITEM_CODE, POD.ITEM_UOM, SUM(POD.ITEM_QTY) As ITEM_QTY, MAX(POD.ITEM_RATE) AS ITEM_RATE, " & vbCrLf _
                    & " AC.SUPP_CUST_NAME as SuppName " & vbCrLf _
                    & " FROM FIN_INVOICE_HDR POM,FIN_INVOICE_DET POD,FIN_SUPP_CUST_MST AC " & vbCrLf _
                    & " WHERE POM.MKEY = POD.MKEY " & vbCrLf _
                    & " And POM.Company_Code = AC.Company_Code " & vbCrLf _
                    & " And POM.SUPP_CUST_CODE = AC.SUPP_CUST_CODE " & vbCrLf _
                    & " And POM.AUTO_KEY_INVOICE='" & xPoNo & "' " & vbCrLf _
                    & " And (POM.SUPP_CUST_CODE='" & pSupplierCode & "' OR POM.BUYER_CODE='" & pSupplierCode & "' OR POM.CO_BUYER_CODE='" & pSupplierCode & "')" & vbCrLf _
                    & " And POM.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " and POM.CANCELLED='N' AND POD.ITEM_CODE='" & Trim(xItemCode) & "'" & vbCrLf _
                    & " GROUP BY POM.INVOICE_DATE,POD.ITEM_CODE,POD.ITEM_UOM,AC.SUPP_CUST_NAME"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = True Then
                    SqlStr = " SELECT '' AS INVOICE_DATE, " & vbCrLf _
                            & " INVMST.ITEM_CODE, INVMST.ISSUE_UOM AS ITEM_UOM, 0 As ITEM_QTY,0 AS ITEM_RATE, " & vbCrLf _
                            & " '' as SuppName " & vbCrLf _
                            & " FROM INV_ITEM_MST INVMST " & vbCrLf _
                            & " WHERE INVMST.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                            & " and INVMST.ITEM_CODE='" & Trim(xItemCode) & "'"
                End If
            Case Else
                CollectPOData = True
                Exit Function
        End Select

        If SqlStr = "" Then Exit Function

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPO, ADODB.LockTypeEnum.adLockReadOnly)

        If RsPO.EOF = False Then
            If VB.Left(cboRefType.Text, 1) = "R" Then
                If mMultiItemCode = False Then
                    mInwardItemCode = Trim(IIf(IsDBNull(RsPO.Fields("INWARD_ITEM_CODE").Value), "", RsPO.Fields("INWARD_ITEM_CODE").Value))
                    'If mInwardItemCode <> "" Then
                    '    FillInwardRGPDetailPart(RsPO, Val(xPoNo), mRowNo)
                    'Else
                    '    '                FillPODetailPart RsPO, Val(xPoNo), mRowNo

                    FillRGPDetailPart(RsPO, Val(xPoNo), mRowNo, Trim(xItemCode), Trim(xOutItemCode), mInConUnit, mOutConUnit, pSupplierCode)

                    'End If
                End If
            Else
                If VB.Left(cboRefType.Text, 1) = "P" Then
                    If Trim(txtDeliveryTo.Text) = "" Then
                        mDeliveryToCode = IIf(IsDBNull(RsPO.Fields("DELIVERY_TO").Value), "", RsPO.Fields("DELIVERY_TO").Value)
                        txtDeliveryToLoc.Text = ""
                        If mDeliveryToCode <> "" Then
                            If MainClass.ValidateWithMasterTable(mDeliveryToCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                mDeliveryToName = MasterNo
                            End If

                            txtDeliveryTo.Text = mDeliveryToName

                            txtDeliveryToLoc.Text = IIf(IsDBNull(RsPO.Fields("DELIVERY_TO_LOC_ID").Value), "", RsPO.Fields("DELIVERY_TO_LOC_ID").Value)

                        End If
                    End If
                End If
                FillPODetailPart(RsPO, Val(xPoNo), mRowNo)
            End If
            CollectPOData = True
        Else
            If RsCompany.Fields("StockBalCheck").Value = "N" Or VB.Left(cboRefType.Text, 1) = "I" Or VB.Left(cboRefType.Text, 1) = "3" Then
                CollectPOData = True
            Else
                CollectPOData = False
            End If

        End If
        CalcTots()
        Exit Function
ERR1:
        MsgInformation(Err.Description)

        CollectPOData = False
    End Function

    Private Function CheckOpenOrder(ByRef pItemCode As String, ByRef mDivisionCode As Double) As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing


        SqlStr = ""

        SqlStr = " SELECT ID.ITEM_CODE " & vbCrLf _
            & " FROM PUR_PURCHASE_HDR IH,PUR_PURCHASE_DET ID" & vbCrLf _
            & " WHERE IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IH.MKEY = ID.MKEY " & vbCrLf _
            & " And IH.SUPP_CUST_CODE='" & mSupplierCode & "' " & vbCrLf _
            & " And ID.ITEM_CODE='" & Trim(pItemCode) & "' "

        If RsCompany.Fields("PO_IN_GE").Value = "Y" Then
            SqlStr = SqlStr & vbCrLf & " And IH.PO_STATUS='Y'"
        End If

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
    Private Sub FillPODetailPart(ByRef RsPO As ADODB.Recordset, ByRef xPoNo As Double, ByRef SprdRowNo As Integer)

        On Error GoTo ERR1
        Dim mOrdQty As Object
        Dim mRecvQty As Double
        Dim mItemCode As String
        Dim mItemName As String
        Dim mItemPartNo As String
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
        Dim mPONO As Double

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
                If RsCompany.Fields("StockBalCheck").Value = "Y" Then
                    .Text = IIf(IsDBNull(RsPO.Fields("INVOICE_DATE").Value), "", RsPO.Fields("INVOICE_DATE").Value)
                Else
                    .Text = IIf(IsDBNull(RsPO.Fields("INVOICE_DATE").Value), .Text, RsPO.Fields("INVOICE_DATE").Value)
                End If
            Else
                .Text = IIf(IsDBNull(RsPO.Fields("PUR_ORD_DATE").Value), "", RsPO.Fields("PUR_ORD_DATE").Value)
            End If

            .Col = ColItemCode
            mItemCode = Trim(IIf(IsDBNull(RsPO.Fields("ITEM_CODE").Value), "", RsPO.Fields("ITEM_CODE").Value))
            .Text = mItemCode

            mItemName = ""
            .Col = ColItemName
            MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
            mItemName = MasterNo
            .Text = mItemName

            mItemPartNo = ""
            .Col = ColItemPartNo
            MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "CUSTOMER_PART_NO", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
            mItemPartNo = MasterNo
            .Text = mItemPartNo

            .Col = ColHSNCode
            mHSNCode = ""
            If VB.Left(cboRefType.Text, 1) = "P" Then
                mHSNCode = ""
                .Col = ColPONo
                mPONO = Trim(.Text)
                If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
                Else
                    mHSNCode = GetHSNFromPurchaseOrder(mItemCode, mPONO)
                End If

                .Col = ColHSNCode
                .Text = mHSNCode
            End If
            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
            Else
                If mHSNCode = "" Then
                    MainClass.ValidateWithMasterTable(Trim(mItemCode), "Item_Code", "HSN_CODE", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                    .Text = MasterNo
                End If
            End If

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
            mRecdQty = CalcRecvQty(xPoNo, RsPO.Fields("ITEM_CODE").Value, mSupplierCode, mOpenOrder)
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

            .Col = ColRemarks
            If VB.Left(cboRefType.Text, 1) = "P" Then
                .Text = IIf(IsDBNull(RsPO.Fields("REMARKS_DETAILS").Value), "", RsPO.Fields("REMARKS_DETAILS").Value)
            End If

            'If SprdRowNo = 1 Then
            '    If CheckTCRequired(mItemCode) = True Then ''If GetCostingRequired(mItemCode) = False Then
            '        Call ShowBlobFileFromPO(xPoNo)
            '    End If
            'End If

        End With



        '    FormatSprdMain -1
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        '    Resume
    End Sub

    Private Sub FillInwardRGPDetailPart(ByRef RsPO As ADODB.Recordset, ByRef mRefNo As Double, ByRef SprdRowNo As Integer)

        On Error GoTo ERR1
        Dim mOrdQty As Object
        Dim mRecvQty As Double
        Dim mItemCode As String
        Dim mItemName As String
        Dim mItemUOM As String = ""
        Dim mRGPCode As String

        Dim mPOQty As Double
        Dim mRecdQty As Double
        Dim mBalQty As Double
        Dim mJWRate As Double
        Dim pSupplierCode As String = "-1"
        Dim mPONo As String
        Dim mPORate As Double
        Dim mPurType As String
        Dim pMkey As Double
        Dim mItemPartNo As String

        If RsPO.EOF Then Exit Sub
        RsPO.MoveFirst()

        MainClass.ValidateWithMasterTable(Trim(TxtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
        pSupplierCode = MasterNo

        With SprdMain

            .Row = SprdRowNo

            .Col = ColItemCode
            mItemCode = Trim(IIf(IsDBNull(RsPO.Fields("INWARD_ITEM_CODE").Value), "", RsPO.Fields("INWARD_ITEM_CODE").Value))
            .Text = mItemCode

            .Col = ColRGPItemCode
            mRGPCode = Trim(.Text)

            .Col = ColItemName
            MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
            mItemName = MasterNo
            .Text = mItemName

            mItemPartNo = ""
            .Col = ColItemPartNo
            MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
            mItemPartNo = MasterNo
            .Text = mItemPartNo

            .Col = ColHSNCode
            MainClass.ValidateWithMasterTable(Trim(mItemCode), "Item_Code", "HSN_CODE", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
            .Text = MasterNo

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

            .Col = ColPORate
            If VB.Left(cboRefType.Text, 1) = "R" And mRGPCode <> "" Then

                mPONo = IIf(IsDBNull(RsPO.Fields("AUTO_KEY_WO").Value), 0, RsPO.Fields("AUTO_KEY_WO").Value)

                If mItemCode = mRGPCode Then
                    mPORate = 0
                Else
                    If MainClass.ValidateWithMasterTable(mPONo, "AUTO_KEY_PO", "PUR_TYPE", "PUR_PURCHASE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & pSupplierCode & "' AND PO_STATUS='Y' AND PO_CLOSED='N'") = True Then
                        mPurType = Trim(MasterNo)
                    End If
                    If mPurType = "J" Then
                        If MainClass.ValidateWithMasterTable(mPONo, "AUTO_KEY_PO", "MKEY", "PUR_PURCHASE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & pSupplierCode & "' AND PO_STATUS='Y' AND PO_CLOSED='N'") = True Then
                            pMkey = IIf(IsDBNull(MasterNo) Or MasterNo = 0, -1, MasterNo)
                            If MainClass.ValidateWithMasterTable(pMkey, "MKEY", "(NVL(ITEM_PRICE,0) - ROUND((NVL(ITEM_PRICE,0) * ITEM_DIS_PER)/100,2))", "PUR_PURCHASE_DET", PubDBCn, MasterNo, , "MKEY=" & pMkey & " AND ITEM_CODE='" & mItemCode & "'") = True Then
                                mPORate = IIf(IsDBNull(MasterNo) Or MasterNo = 0, RsPO.Fields("ITEM_RATE").Value, MasterNo)
                                .Text = IIf(IsDBNull(MasterNo) Or MasterNo = 0, RsPO.Fields("ITEM_RATE").Value, MasterNo)
                            Else
                                .Text = Val(IIf(IsDBNull(RsPO.Fields("ITEM_RATE").Value), 0, RsPO.Fields("ITEM_RATE").Value))
                            End If
                        End If
                    Else
                        .Text = Val(IIf(IsDBNull(RsPO.Fields("ITEM_RATE").Value), 0, RsPO.Fields("ITEM_RATE").Value))
                    End If
                End If
            ElseIf VB.Left(cboRefType.Text, 1) = "I" Or VB.Left(cboRefType.Text, 1) = "1" Or VB.Left(cboRefType.Text, 1) = "2" Or VB.Left(cboRefType.Text, 1) = "3" Then
                .Text = CStr(Val(IIf(IsDBNull(RsPO.Fields("ITEM_RATE").Value), 0, RsPO.Fields("ITEM_RATE").Value)))
            Else
                .Text = CStr(Val(IIf(IsDBNull(RsPO.Fields("ITEM_PRICE").Value), 0, RsPO.Fields("ITEM_PRICE").Value)))
            End If

            .Col = ColRate
            If VB.Left(cboRefType.Text, 1) = "R" Then
                .Text = mPORate
            ElseIf VB.Left(cboRefType.Text, 1) = "I" Or VB.Left(cboRefType.Text, 1) = "1" Or VB.Left(cboRefType.Text, 1) = "2" Or VB.Left(cboRefType.Text, 1) = "3" Then
                .Text = CStr(Val(IIf(IsDBNull(RsPO.Fields("ITEM_RATE").Value), 0, RsPO.Fields("ITEM_RATE").Value)))
            Else
                .Text = CStr(Val(IIf(IsDBNull(RsPO.Fields("ITEM_PRICE").Value), 0, RsPO.Fields("ITEM_PRICE").Value)))
            End If


            '          MainClass.AddBlankSprdRow SprdMain, ColItemCode, ConRowHeight
        End With
        '        FormatSprdMain -1
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        '    Resume
    End Sub

    Private Sub FillRGPDetailPart(ByRef RsPO As ADODB.Recordset, ByRef mRefNo As Double, ByRef SprdRowNo As Integer, ByRef xInItemCode As String, ByRef xOutItemCode As String, ByRef xInConUnit As Double, ByRef xOutConUnit As Double, ByRef pSupplierCode As String)


        On Error GoTo ERR1
        Dim mOrdQty As Object
        Dim mRecvQty As Double
        Dim mOutItemCode As String
        Dim mItemName As String
        Dim mItemUOM As String = ""
        Dim mCheckUOM As String
        Dim mPOQty As Double
        Dim mRecdQty As Double
        Dim mBalQty As Double
        Dim mRejQty As Double

        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mQCEmpCode As String
        Dim mPONo As Double
        Dim pMkey As Double
        Dim mPurType As String = ""
        Dim mItemPartNo As String
        If RsPO.EOF Then Exit Sub
        RsPO.MoveFirst()
        mOutItemCode = xOutItemCode 'Trim(IIf(IsNull(RsPO!ITEM_CODE), "", RsPO!ITEM_CODE))

        With SprdMain

            .Row = SprdRowNo

            .Col = ColItemCode
            .Text = xInItemCode

            .Col = ColItemName
            MainClass.ValidateWithMasterTable(xInItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
            mItemName = MasterNo
            .Text = mItemName

            mItemPartNo = ""
            .Col = ColItemPartNo
            MainClass.ValidateWithMasterTable(xInItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
            mItemPartNo = MasterNo
            .Text = mItemPartNo

            .Col = ColHSNCode
            MainClass.ValidateWithMasterTable(Trim(xInItemCode), "Item_Code", "HSN_CODE", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
            .Text = MasterNo

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
                mRecdQty = CalcRecvQty(mRefNo, xInItemCode, mSupplierCode, False)
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

            mPONo = IIf(IsDBNull(RsPO.Fields("AUTO_KEY_WO").Value), 0, RsPO.Fields("AUTO_KEY_WO").Value)

            .Col = ColPORate
            If xInItemCode = xOutItemCode Then
                If MainClass.ValidateWithMasterTable(mPONo, "AUTO_KEY_PO", "PUR_TYPE", "PUR_PURCHASE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & pSupplierCode & "' AND PO_STATUS='Y' AND PO_CLOSED='N'") = True Then
                    mPurType = Trim(MasterNo)
                End If
                If mPurType = "W" Then
                    If MainClass.ValidateWithMasterTable(mPONo, "AUTO_KEY_PO", "MKEY", "PUR_PURCHASE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & pSupplierCode & "' AND PO_STATUS='Y' AND PO_CLOSED='N'") = True Then
                        pMkey = IIf(IsDBNull(MasterNo) Or MasterNo = 0, -1, MasterNo)
                        If MainClass.ValidateWithMasterTable(pMkey, "MKEY", "(NVL(ITEM_PRICE,0) - ROUND((NVL(ITEM_PRICE,0) * ITEM_DIS_PER)/100,2))", "PUR_PURCHASE_DET", PubDBCn, MasterNo, , "MKEY=" & pMkey & "") = True Then
                            .Text = IIf(IsDBNull(MasterNo) Or MasterNo = 0, RsPO.Fields("ITEM_RATE").Value, MasterNo)
                        Else
                            .Text = Val(IIf(IsDBNull(RsPO.Fields("ITEM_RATE").Value), 0, RsPO.Fields("ITEM_RATE").Value))
                        End If
                    End If
                Else
                    .Text = Val(IIf(IsDBNull(RsPO.Fields("ITEM_RATE").Value), 0, RsPO.Fields("ITEM_RATE").Value))
                End If
            ElseIf MainClass.ValidateWithMasterTable(mPONo, "AUTO_KEY_PO", "MKEY", "PUR_PURCHASE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & pSupplierCode & "' AND PO_STATUS='Y' AND PO_CLOSED='N'") = True Then

                pMkey = IIf(IsDBNull(MasterNo) Or MasterNo = 0, -1, MasterNo)

                If MainClass.ValidateWithMasterTable(xInItemCode, "ITEM_CODE", "(NVL(ITEM_PRICE,0) - ROUND((NVL(ITEM_PRICE,0) * ITEM_DIS_PER)/100,2))", "PUR_PURCHASE_DET", PubDBCn, MasterNo, , "MKEY=" & pMkey & "") = True Then
                    .Text = IIf(IsDBNull(MasterNo) Or MasterNo = 0, RsPO.Fields("ITEM_RATE").Value, MasterNo)
                Else
                    .Text = Val(IIf(IsDBNull(RsPO.Fields("ITEM_RATE").Value), 0, RsPO.Fields("ITEM_RATE").Value))
                End If
            ElseIf MainClass.ValidateWithMasterTable(xInItemCode, "Item_Code", "ITEM_RATE", "FIN_SUPP_CUST_DET", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & pSupplierCode & "'") = True Then
                .Text = IIf(IsDBNull(MasterNo) Or MasterNo = 0, RsPO.Fields("ITEM_RATE").Value, MasterNo)
            Else
                .Text = Val(IIf(IsDBNull(RsPO.Fields("ITEM_RATE").Value), 0, RsPO.Fields("ITEM_RATE").Value)) / IIf(xInConUnit = 0, 1, xInConUnit)
            End If


            .Col = ColRate
            If xInItemCode = xOutItemCode Then
                .Text = CStr(Val(IIf(IsDBNull(RsPO.Fields("ITEM_RATE").Value), 0, RsPO.Fields("ITEM_RATE").Value)))
            ElseIf MainClass.ValidateWithMasterTable(mPONo, "AUTO_KEY_PO", "MKEY", "PUR_PURCHASE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & pSupplierCode & "' AND PO_STATUS='Y' AND PO_CLOSED='N'") = True Then

                pMkey = IIf(IsDBNull(MasterNo) Or MasterNo = 0, -1, MasterNo)

                If MainClass.ValidateWithMasterTable(xInItemCode, "ITEM_CODE", "(NVL(ITEM_PRICE,0) - ROUND((NVL(ITEM_PRICE,0) * ITEM_DIS_PER)/100,2))", "PUR_PURCHASE_DET", PubDBCn, MasterNo, , "MKEY=" & pMkey & "") = True Then
                    .Text = IIf(IsDBNull(MasterNo) Or MasterNo = 0, RsPO.Fields("ITEM_RATE").Value, MasterNo)
                Else
                    .Text = CStr(Val(IIf(IsDBNull(RsPO.Fields("ITEM_RATE").Value), 0, RsPO.Fields("ITEM_RATE").Value)))
                End If
            ElseIf MainClass.ValidateWithMasterTable(xInItemCode, "Item_Code", "ITEM_RATE", "FIN_SUPP_CUST_DET", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & pSupplierCode & "'") = True Then
                .Text = IIf(IsDBNull(MasterNo) Or MasterNo = 0, RsPO.Fields("ITEM_RATE").Value, MasterNo)
            Else
                .Text = CStr(Val(IIf(IsDBNull(RsPO.Fields("ITEM_RATE").Value), 0, RsPO.Fields("ITEM_RATE").Value)) / IIf(xInConUnit = 0, 1, xInConUnit))
            End If

            '
            '          MainClass.AddBlankSprdRow SprdMain, ColItemCode, ConRowHeight
        End With
        '        FormatSprdMain -1
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        '    Resume
    End Sub
    Private Function CalcDSQty(ByVal pSupplierCode As String, ByVal pPONO As Double, ByVal pItemCode As String) As Double

        On Error GoTo ErrPart
        Dim mSchdDate As String
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mLastDate As String
        Dim mFieldName As String

        mSchdDate = "01/" & VB6.Format(txtMRRDate.Text, "MM") & "/" & VB6.Format(txtMRRDate.Text, "YYYY")
        mLastDate = MainClass.LastDay(Month(CDate(txtMRRDate.Text)), Year(CDate(txtMRRDate.Text))) & "/" & VB6.Format(txtMRRDate.Text, "MM") & "/" & VB6.Format(txtMRRDate.Text, "YYYY")

        If RsCompany.Fields("WEEKLY_SCHD").Value = "N" Then
            mFieldName = "TOTAL_QTY"
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

        SqlStr = "SELECT SCHLD_DATE," & mFieldName & " AS TOTAL_QTY " & vbCrLf _
            & " FROM PUR_DELV_SCHLD_HDR DSMain, PUR_DELV_SCHLD_DET DSDetail" & vbCrLf _
            & " WHERE DSMain.COMPANY_CODE=Dsdetail.COMPANY_CODE AND DSMain.AUTO_KEY_DELV=Dsdetail.AUTO_KEY_DELV" & vbCrLf _
            & " AND DSMain.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUPP_CUST_CODE='" & pSupplierCode & "'" & vbCrLf _
            & " AND ITEM_CODE='" & pItemCode & "' AND POST_FLAG='Y'" & vbCrLf _
            & " AND SCHLD_DATE >= TO_DATE('" & VB6.Format(mSchdDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND SCHLD_DATE <= TO_DATE('" & VB6.Format(mLastDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        'If CDate(txtMRRDate.Text) >= CDate(PubGSTApplicableDate) And CDate(txtBillDate.Text) < CDate(PubGSTApplicableDate) Then

        'Else
        SqlStr = SqlStr & vbCrLf & " AND AUTO_KEY_PO=" & Val(pPONO) & ""
        'End If

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

    Private Function GetScheduleNo(ByRef pSupplierCode As String, ByRef pDSNo As Double) As Double

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

    Private Function CalcPOQty(ByRef pSupplierCode As String, ByRef pPONO As Double, ByRef pItemCode As String, ByRef pRefType As String, ByRef pOpenOrder As Boolean, ByRef mDivisionCode As Double) As Double

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
            'And IH.PO_STATUS='Y'

            SqlStr = SqlStr & vbCrLf & " AND POMain.MKEY = ( " & vbCrLf & " SELECT MAX(IH.MKEY) " & vbCrLf & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID" & vbCrLf & " WHERE IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY AND IH.COMPANY_CODE=ID.COMPANY_CODE " & vbCrLf & " AND IH.AUTO_KEY_PO =" & Val(CStr(pPONO)) & ""

            If RsCompany.Fields("PO_IN_GE").Value = "Y" Then
                SqlStr = SqlStr & vbCrLf & " And IH.PO_STATUS='Y'"
            End If

            SqlStr = SqlStr & vbCrLf & " AND IH.DIV_CODE=" & mDivisionCode & " And ID.ITEM_CODE='" & Trim(pItemCode) & "'" & vbCrLf & " AND ID.PO_WEF_DATE<=TO_DATE('" & VB6.Format(txtBillDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            '        If PubGSTApplicable = True Then
            '            SqlStr = SqlStr & vbCrLf & " AND ID.PO_WEF_DATE>=TO_DATE('" & VB6.Format(PubGSTApplicableDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            '        End If

            If CDate(txtMRRDate.Text) < CDate(PubGSTApplicableDate) And CDate(txtBillDate.Text) < CDate(PubGSTApplicableDate) Then

            ElseIf CDate(txtMRRDate.Text) < CDate(PubGSTApplicableDate) And CDate(txtBillDate.Text) >= CDate(PubGSTApplicableDate) Then

            ElseIf CDate(txtMRRDate.Text) >= CDate(PubGSTApplicableDate) And CDate(txtBillDate.Text) < CDate(PubGSTApplicableDate) Then

            Else
                SqlStr = SqlStr & vbCrLf & " AND ID.PO_WEF_DATE>=TO_DATE('" & VB6.Format(PubGSTApplicableDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If

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
    Private Function CalcRecvQty(ByVal CurrPONo As Double, ByVal CurrItemCode As String, ByVal pSupplierCode As String, ByVal pOpenOrder As Boolean) As Double

        On Error GoTo CalcRecvQtyErr
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim xPoNo As String
        Dim xFYNo As Integer
        Dim SqlStr As String = ""
        Dim CurrMrrNo As Double
        Dim xSchldDate As String
        Dim mLastDayOfMonth As String

        '    xSchldDate = "01/" & VB6.Format(txtMRRDate, "MM") & "/" & VB6.Format(txtMRRDate, "YYYY")
        '    mLastDayOfMonth = MainClass.LastDay(Month(txtMRRDate.Text), Year(txtMRRDate.Text)) & "/" & VB6.Format(txtMRRDate, "MM") & "/" & VB6.Format(txtMRRDate, "YYYY")

        If RsCompany.Fields("WEEKLY_SCHD").Value = "N" Then
            xSchldDate = "01/" & VB6.Format(txtMRRDate.Text, "MM") & "/" & VB6.Format(txtMRRDate.Text, "YYYY")
            mLastDayOfMonth = MainClass.LastDay(Month(CDate(txtMRRDate.Text)), Year(CDate(txtMRRDate.Text))) & "/" & VB6.Format(txtMRRDate.Text, "MM") & "/" & VB6.Format(txtMRRDate.Text, "YYYY")
        Else
            '        xSchldDate = GetFirstDayInWeek(txtMRRDate.Text)
            '        mLastDayOfMonth = GetLastDayInWeek(txtMRRDate.Text)
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

        If mWithOutOrder = True Then CalcRecvQty = 0.0# : Exit Function

        CurrMrrNo = IIf(Trim(txtMRRNo.Text) = "", -1, Val(txtMRRNo.Text))

        SqlStr = ""

        ''-DECODE(QC_STATUS,'Y',REJECTED_QTY,0)
        ''
        '
        '
        '    SqlStr = "SELECT SUM(ID.RECEIVED_QTY) AS RECDQTY " & vbCrLf _
        ''            & " FROM INV_GATEENTRY_HDR IH, INV_GATEENTRY_DET ID WHERE " & vbCrLf _
        ''            & " IH.AUTO_KEY_GATE=ID.AUTO_KEY_GATE" & vbCrLf _
        ''            & " AND IH.Company_Code=" & RsCompany.fields("COMPANY_CODE").value & " AND SUBSTR(IH.AUTO_KEY_GATE,LENGTH(IH.AUTO_KEY_GATE)-5,4)=" & RsCompany.fields("FYEAR").value & "" & vbCrLf _
        ''            & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(UCase(pSupplierCode)) & "' " & vbCrLf _
        ''            & " AND ID.REF_AUTO_KEY_NO=" & Val(CurrPONo) & " "


        SqlStr = "SELECT SUM(ID.RECEIVED_QTY) AS RECDQTY " & vbCrLf _
            & " FROM INV_GATE_DET ID WHERE " & vbCrLf _
            & " ID.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUBSTR(ID.AUTO_KEY_MRR,LENGTH(ID.AUTO_KEY_MRR)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND TRIM(ID.SUPP_CUST_CODE)='" & MainClass.AllowSingleQuote(UCase(pSupplierCode)) & "' " ''& vbCrLf |            & " "

        '    If chkShipTo.Value = vbChecked Then   ''Not Required  .  11/06/2020  SK  PO must required in both cases
        SqlStr = SqlStr & "  AND ID.REF_AUTO_KEY_NO=" & Val(CStr(CurrPONo)) & " "
        '    End If

        If VB.Left(cboRefType.Text, 1) = "R" Then
            SqlStr = SqlStr & vbCrLf & " AND ID.RGP_ITEM_CODE='" & MainClass.AllowSingleQuote(CurrItemCode) & "' "
        Else
            SqlStr = SqlStr & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(CurrItemCode) & "' "
        End If

        If VB.Left(cboRefType.Text, 1) <> "P" Then
            SqlStr = SqlStr & vbCrLf & " AND ID.REF_TYPE='" & VB.Left(cboRefType.Text, 1) & "'"
        End If
        '
        '    If CurrMrrNo <> "-1" Then
        '        SqlStr = SqlStr & vbCrLf & " AND ID.AUTO_KEY_GATE<>" & Val(CurrMrrNo) & ""
        '    End If

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


        SqlStr = "SELECT SUM(ID.BILL_QTY) AS RECDQTY " & vbCrLf _
            & " FROM  INV_GATEENTRY_HDR IH, INV_GATEENTRY_DET ID WHERE IH.AUTO_KEY_GATE=ID.AUTO_KEY_GATE" & vbCrLf _
            & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUBSTR(ID.AUTO_KEY_GATE,LENGTH(ID.AUTO_KEY_GATE)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND TRIM(IH.SUPP_CUST_CODE)='" & MainClass.AllowSingleQuote(UCase(pSupplierCode)) & "' AND IH.MRR_MADE='N'" ''& vbCrLf |            & " "

        '    If chkShipTo.Value = vbChecked Then   ''Not Required  .  11/06/2020  SK  PO must required in both cases
        SqlStr = SqlStr & "  AND ID.REF_AUTO_KEY_NO=" & Val(CStr(CurrPONo)) & " "
        '    End If

        If VB.Left(cboRefType.Text, 1) = "R" Then
            SqlStr = SqlStr & vbCrLf & " AND ID.RGP_ITEM_CODE='" & MainClass.AllowSingleQuote(CurrItemCode) & "' "
        Else
            SqlStr = SqlStr & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(CurrItemCode) & "' "
        End If

        If Val(txtMRRNo.Text) > 0 Then
            SqlStr = SqlStr & vbCrLf & " AND IH.AUTO_KEY_GATE<> " & Val(txtMRRNo.Text) & ""
        End If

        If VB.Left(cboRefType.Text, 1) <> "P" Then
            SqlStr = SqlStr & vbCrLf & " AND ID.REF_TYPE='" & VB.Left(cboRefType.Text, 1) & "'"
        End If
        '
        '    If CurrMrrNo <> "-1" Then
        '        SqlStr = SqlStr & vbCrLf & " AND ID.AUTO_KEY_GATE<>" & Val(CurrMrrNo) & ""
        '    End If

        If VB.Left(cboRefType.Text, 1) = "P" And pOpenOrder = True Then
            If xSchldDate <> "" Then 'DEEPAK  IF PO HAS MORE THAN ONE DLV SCHLD OF SAME ITEM , IT WAS CONSID PREV MRR QTY ALSO 01/05/2004
                SqlStr = SqlStr & vbCrLf & " AND IH.GATE_DATE>=TO_DATE('" & VB6.Format(xSchldDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND IH.GATE_DATE<=TO_DATE('" & VB6.Format(mLastDayOfMonth, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            End If
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMisc.EOF = False Then
            CalcRecvQty = CalcRecvQty + Val(IIf(IsDBNull(RsMisc.Fields(0).Value), 0, RsMisc.Fields(0).Value))
        End If

        Exit Function
CalcRecvQtyErr:
        CalcRecvQty = 0.0#
        MsgBox(Err.Description)
    End Function

    Private Function CalcRGPRecvQty(ByRef CurrPONo As Double, ByRef CurrItemCode As String, ByRef pSupplierCode As String) As Double

        On Error GoTo CalcRecvQtyErr
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim xPoNo As String
        Dim xFYNo As Integer
        Dim SqlStr As String = ""
        Dim CurrMrrNo As Double

        If mWithOutOrder = True Then CalcRGPRecvQty = 0.0# : Exit Function

        CurrMrrNo = IIf(Trim(txtMRRNo.Text) = "", -1, Val(txtMRRNo.Text))

        SqlStr = ""

        SqlStr = "SELECT SUM(DECODE(ITEM_IO,'O',0,1)*TRN.RGP_QTY) AS RECDQTY " & vbCrLf _
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
            CalcRGPRecvQty = Val(IIf(IsDBNull(RsMisc.Fields(0).Value), 0, RsMisc.Fields(0).Value))
        Else
            CalcRGPRecvQty = 0.0#
        End If
        Exit Function
CalcRecvQtyErr:
        CalcRGPRecvQty = 0.0#
        MsgBox(Err.Description)
    End Function

    Private Function CalcRecvRGPREJQty(ByRef CurrPONo As Double, ByRef OutItemCode As String, ByRef pSupplierCode As String) As Double

        On Error GoTo CalcRecvQtyErr
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim xPoNo As String
        Dim xFYNo As Integer
        Dim SqlStr As String = ""
        Dim CurrMrrNo As Double

        If mWithOutOrder = True Then CalcRecvRGPREJQty = 0.0# : Exit Function

        CurrMrrNo = IIf(Trim(txtMRRNo.Text) = "", -1, Val(txtMRRNo.Text))

        SqlStr = ""

        SqlStr = "SELECT SUM(ID.RECEIVED_QTY) AS RECDQTY " & vbCrLf & " FROM INV_GATEENTRY_HDR IH, INV_GATEENTRY_DET ID WHERE " & vbCrLf & " IH.AUTO_KEY_GATE=ID.AUTO_KEY_GATE" & vbCrLf & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(UCase(pSupplierCode)) & "' " & vbCrLf & " AND ID.REF_AUTO_KEY_NO=" & Val(CStr(CurrPONo)) & " " & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(OutItemCode) & "' "

        If VB.Left(cboRefType.Text, 1) <> "P" Then
            SqlStr = SqlStr & vbCrLf & " AND IH.REF_TYPE='" & VB.Left(cboRefType.Text, 1) & "'"
        End If

        If CurrMrrNo <> CDbl("-1") Then
            SqlStr = SqlStr & vbCrLf & " AND IH.AUTO_KEY_GATE<>" & Val(CStr(CurrMrrNo)) & ""
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
        Else
            mSupplierCode = "-1"
            Cancel = True
        End If

        txtBillTo.Text = IIf(txtBillTo.Text = "", GetDefaultLocation(mSupplierCode), txtBillTo.Text)

        Call FillSprdExp()
        pTempUpdate = False
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Function GetPOFromDs(ByRef xDSNo As String) As String

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

    Private Sub GetF4detailFromRGP(ByRef mPONo As Double, ByRef mCheckF4 As Boolean, ByRef mOutwardF4No As Double, ByRef mOutwardF4Date As String, ByRef mExpDate As String)

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing

        mCheckF4 = False
        mOutwardF4No = CDbl("0")
        mOutwardF4Date = ""

        mSqlStr = " SELECT OUTWARD_57F4NO,GATEPASS_DATE,EXP_RTN_DATE " & vbCrLf & " FROM INV_GATEPASS_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_PASSNO=" & mPONo & ""

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

    Private Function SelectQuery(ByRef xRefType As String, ByRef xRefNo As String, ByRef xIsItemCode As Boolean, ByRef mDivisionCode As Double, Optional ByRef pRGPItemCode As String = "") As String
        On Error GoTo ErrPart
        Dim mSuppCode As String
        Dim SelectQuery1 As String

        If xIsItemCode = True Then
            SelectQuery = "SELECT DISTINCT INVMST.ITEM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO,'OUT' AS RGP_IO "
        Else
            SelectQuery = "SELECT DISTINCT INVMST.ITEM_SHORT_DESC, INVMST.ITEM_CODE, INVMST.CUSTOMER_PART_NO,'OUT' AS RGP_IO  "
        End If

        'IH.PO_STATUS='Y'

        Select Case xRefType
            Case "P"
                SelectQuery = SelectQuery & vbCrLf & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY" & vbCrLf & " AND ID.Company_Code=INVMST.Company_Code" & vbCrLf & " AND ID.ITEM_Code=INVMST.ITEM_Code" & vbCrLf & " AND IH.DIV_CODE=" & mDivisionCode & " AND PO_ITEM_STATUS='N' " & vbCrLf & " AND IH.AUTO_KEY_PO=" & Val(xRefNo) & ""

                If RsCompany.Fields("PO_IN_GE").Value = "Y" Then
                    SelectQuery = SelectQuery & vbCrLf & " And IH.PO_STATUS='Y'"
                End If

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
                    SelectQuery1 = "SELECT DISTINCT INVMST.ITEM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO,'IN' AS RGP_IO"
                Else
                    SelectQuery1 = "SELECT DISTINCT INVMST.ITEM_SHORT_DESC, INVMST.ITEM_CODE, INVMST.CUSTOMER_PART_NO,'IN' AS RGP_IO"
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
                    & " AND IH.GATEPASS_TYPE ='R'  "


                SelectQuery = SelectQuery & vbCrLf _
                    & " UNION " & SelectQuery1 & vbCrLf _
                    & " FROM  " & vbCrLf _
                    & " PRD_NEWBOM_HDR IH,PRD_NEWBOM_DET ID, " & vbCrLf _
                    & " INV_ITEM_MST INVMST" & vbCrLf _
                    & " WHERE IH.MKEY = ID.MKEY " & vbCrLf _
                    & " AND IH.COMPANY_CODE = INVMST.COMPANY_CODE " & vbCrLf _
                    & " AND ID.PRODUCT_CODE = INVMST.ITEM_CODE " & vbCrLf _
                    & " AND IH.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND ID.RM_CODE='" & pRGPItemCode & "'"

                'SelectQuery = SelectQuery & vbCrLf & " UNION " & SelectQuery1 & vbCrLf & " FROM  " & vbCrLf & " PRD_OUTBOM_HDR IH,PRD_OUTBOM_ALTER_DET ID, " & vbCrLf & " INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY = ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE = INVMST.COMPANY_CODE " & vbCrLf & " AND ID.PRODUCT_CODE = INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ID.ALTER_ITEM_CODE='" & pRGPItemCode & "'"

                'AND INVMST.COMPANY_CODE = " & RsCompany.fields("COMPANY_CODE").value & "

            Case "I", "1", "2", "3"


                If MainClass.ValidateWithMasterTable(TxtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mSuppCode = MasterNo
                Else
                    mSuppCode = "-1"
                End If

                SelectQuery = SelectQuery & vbCrLf _
                    & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, INV_ITEM_MST INVMST" & vbCrLf _
                    & " WHERE IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND INVMST.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND IH.MKEY=ID.MKEY" & vbCrLf _
                    & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSuppCode) & "'" & vbCrLf _
                    & " AND IH.Company_Code=INVMST.Company_Code" & vbCrLf _
                    & " AND ID.ITEM_Code=INVMST.ITEM_Code" & vbCrLf _
                    & " AND IH.AUTO_KEY_INVOICE='" & xRefNo & "'"


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

    Private Function DuplicateBillNo(ByRef pSuppCode As String) As Boolean

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
        SqlStr = "SELECT BILL_NO " & vbCrLf _
            & " FROM INV_GATEENTRY_HDR" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_GATE,LENGTH(AUTO_KEY_GATE)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND TRIM(SUPP_CUST_CODE)='" & pSuppCode & "' AND BILL_NO='" & Trim(txtBillNo.Text) & "'" & vbCrLf _
            & " AND AUTO_KEY_GATE<>" & mMRRNO & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            DuplicateBillNo = True
        End If

        Exit Function
ErrPart:
        DuplicateBillNo = False
    End Function


    Private Function AutoGenIssueSeqNo() As Double

        On Error GoTo AutoGenIssueSeqNoErr
        Dim RsMainGen As ADODB.Recordset = Nothing
        Dim mNewSeqNo As Double
        Dim SqlStr As String = ""
        Dim mMaxValue As String

        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_ISS)  " & vbCrLf & " FROM INV_ISSUE_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_ISS,LENGTH(AUTO_KEY_ISS)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsMainGen
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
        AutoGenIssueSeqNo = CDbl(mNewSeqNo & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"))
        Exit Function
AutoGenIssueSeqNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function



    Public Function CheckRefDate(ByRef mDivisionCode As Double) As Boolean
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
        '        MsgInformation("Invalid Ship from Name.")
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
                        If Mid(RsCompany.Fields("COMPANY_NAME").Value, 1, 3) = "KAY" Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("StockBalCheck").Value = "N" Then
                        Else
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

    Private Function GetCostC(ByRef pDeptCode As String) As String

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

    Private Function CheckOtherTransMade() As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mMRRNO As Double
        Dim pMRRMade As String

        CheckOtherTransMade = False

        pMRRMade = "N"
        If MainClass.ValidateWithMasterTable((txtMRRNo.Text), "AUTO_KEY_GATE", "MRR_MADE", "INV_GATEENTRY_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            pMRRMade = Trim(MasterNo)
        End If

        If pMRRMade = "N" Then
            CheckOtherTransMade = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable((txtMRRNo.Text), "AUTO_KEY_GATE", "MRR_NO", "INV_GATEENTRY_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mMRRNO = Val(MasterNo)
        End If
        If MainClass.ValidateWithMasterTable(mMRRNO, "AUTO_KEY_MRR", "VNO", "FIN_PURCHASE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            CheckOtherTransMade = True
            Exit Function
        End If
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function


    Private Function GetItemLocking(ByRef pItemCode As String) As Boolean

        On Error GoTo ErrPart
        Dim mSupplierCode As String = ""
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing

        If MainClass.ValidateWithMasterTable((TxtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSupplierCode = MasterNo
        End If

        mSqlStr = "SELECT ITEM_CODE " & vbCrLf & " FROM INV_SCHD_LOCK_DET ID" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSupplierCode) & "'" & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf & " AND DATE_FROM<=TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND DATE_TO>=TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

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

        Exit Sub

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

                    .Col = ColRate
                    .Text = Trim(IIf(IsDBNull(RSDetail.Fields("ITEM_RATE").Value), "", RSDetail.Fields("ITEM_RATE").Value))

                    .Col = ColAmount
                    .Text = Trim(IIf(IsDBNull(RSDetail.Fields("ITEM_AMT").Value), "", RSDetail.Fields("ITEM_AMT").Value))

                    .Col = ColItemCost
                    .Text = Trim(IIf(IsDBNull(RSDetail.Fields("ITEM_AMT").Value), "", RSDetail.Fields("ITEM_AMT").Value))

                    CntRow = CntRow + 1
                    .MaxRows = CntRow
                    RSDetail.MoveNext()
                Loop
            End If
        End With

        FormatSprdMain(-1)

        Call CalcTots()

        Exit Sub
ErrPart:
        ''Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtVehicle_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVehicle.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
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
    Private Sub txtBillTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBillTo.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
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
    Private Sub cmdBillToSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdBillToSearch.Click
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
    Private Sub txtShipTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtShipTo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtShipTo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtShipTo.DoubleClick
        cmdShipToSearch_Click(cmdShipToSearch, New System.EventArgs())
    End Sub
    Private Sub txtShipTo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtShipTo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtShipTo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtShipTo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtShipTo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdShipToSearch_Click(cmdShipToSearch, New System.EventArgs())
    End Sub
    Private Sub txtShipTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtShipTo.Validating
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
            txtShipTo_Validating(TxtShipTo, New System.ComponentModel.CancelEventArgs(False))
            If TxtShipTo.Enabled = True Then TxtShipTo.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdInterUnitBill_Click(sender As Object, e As EventArgs) Handles cmdInterUnitBill.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim xAcctCode As String = ""
        Dim mInterUnitCompanyCode As Integer
        Dim mCurrentUnitAccountCode As String

        If TxtSupplier.Text = "" Then MsgInformation("Please Select the Inter Unit Supplier Name") : Exit Sub
        If cboRefType.Text = "" Then MsgInformation("Please Select the Ref Type") : Exit Sub

        If MainClass.ValidateWithMasterTable(Trim(TxtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND INTER_UNIT='Y'") = True Then
            xAcctCode = MasterNo
        Else
            MsgInformation("Supplier is Not Inter Unit.")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(TxtSupplier.Text), "SUPP_CUST_NAME", "INTERUNIT_COMPANY_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND INTER_UNIT='Y'") = True Then
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

        If VB.Left(cboRefType.Text, 1) = "J" Or VB.Left(cboRefType.Text, 1) = "F" Then
            SqlStr = "SELECT AUTO_KEY_PASSNO,GATEPASS_DATE,E_BILLWAYNO,VEHICLE_NO, TRIM(CHALLAN_PREFIX||GATEPASS_NO) AS GATEPASS_NO " & vbCrLf _
                & " FROM INV_GATEPASS_HDR" & vbCrLf _
                & " WHERE COMPANY_CODE=" & mInterUnitCompanyCode & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mCurrentUnitAccountCode) & "'" & vbCrLf _
                & " AND IS_GATENTRY_MADE='N'"
        ElseIf VB.Left(cboRefType.Text, 1) = "R" Then
            SqlStr = "SELECT TO_CHAR(AUTO_KEY_PASSNO) AS AUTO_KEY_PASSNO,GATEPASS_DATE,E_BILLWAYNO,VEHICLE_NO, TRIM(CHALLAN_PREFIX||GATEPASS_NO) AS GATEPASS_NO " & vbCrLf _
                      & " FROM INV_GATEPASS_HDR" & vbCrLf _
                      & " WHERE COMPANY_CODE=" & mInterUnitCompanyCode & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mCurrentUnitAccountCode) & "'" & vbCrLf _
                      & " AND IS_GATENTRY_MADE='N'"

            SqlStr = SqlStr & vbCrLf & " UNION ALL " & vbCrLf _
                    & "SELECT BILLNO As AUTO_KEY_PASSNO,INVOICE_DATE AS GATEPASS_DATE,E_BILLWAYNO,VEHICLENO ,BILLNO AS GATEPASS_NO" & vbCrLf _
                    & " FROM FIN_INVOICE_HDR" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & mInterUnitCompanyCode & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mCurrentUnitAccountCode) & "'" & vbCrLf _
                    & " AND IS_GATENTRY_MADE='N' AND CANCELLED='N' AND REF_DESP_TYPE IN ('J')"



        Else
            SqlStr = "SELECT BILLNO,INVOICE_DATE,CUST_PO_NO,CUST_PO_DATE,E_BILLWAYNO,VEHICLENO " & vbCrLf _
                & " FROM FIN_INVOICE_HDR" & vbCrLf _
                & " WHERE COMPANY_CODE=" & mInterUnitCompanyCode & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mCurrentUnitAccountCode) & "'" & vbCrLf _
                & " AND IS_GATENTRY_MADE='N' AND CANCELLED='N'"

            If VB.Left(cboRefType.Text, 1) = "P" Then
                SqlStr = SqlStr & vbCrLf & " AND REF_DESP_TYPE IN ('P','G','F','S')"
            ElseIf VB.Left(cboRefType.Text, 1) = "I" Or VB.Left(cboRefType.Text, 1) = "3" Then
                SqlStr = SqlStr & vbCrLf & " AND REF_DESP_TYPE IN ('S','Q','L')"
            ElseIf VB.Left(cboRefType.Text, 1) = "R" Then
                SqlStr = SqlStr & vbCrLf & " AND REF_DESP_TYPE IN ('J')"
            End If
        End If

        If MainClass.SearchGridMasterBySQL2((TxtShipTo.Text), SqlStr) = True Then
            txtBillNo.Text = AcName
            txtBillDate.Text = VB6.Format(AcName1, "DD/MM/YYYY")
            If VB.Left(cboRefType.Text, 1) = "J" Or VB.Left(cboRefType.Text, 1) = "F" Then
                txtEwayBillNo.Text = AcName2
            ElseIf VB.Left(cboRefType.Text, 1) = "R" Then
                txtEwayBillNo.Text = AcName2
            Else
                txtEwayBillNo.Text = AcName4
            End If
        End If

        If Trim(txtBillNo.Text) <> "" Then
            'If VB.Left(cboRefType.Text, 1) = "J" Then
            '    'Call ShowFromRGP(txtBillNo.Text, txtBillDate.Text, mInterUnitCompanyCode)
            'Else
            Call ShowFromInvoice(mInterUnitCompanyCode, mCurrentUnitAccountCode)
            'End If
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub ShowFromInvoice(ByRef mInterUnitCompanyCode As Integer, ByRef mCurrentUnitAccountCode As String)

        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsTempDet As ADODB.Recordset = Nothing
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
        Dim mCustomerPartNo As String
        Dim mDocType As String = ""
        Dim mRGPItemCode As String

        If VB.Left(cboRefType.Text, 1) = "J" Or VB.Left(cboRefType.Text, 1) = "F" Then
            SqlStr = "SELECT AUTO_KEY_PASSNO AS MKEY, AUTO_KEY_PASSNO AS BILL_NO, GATEPASS_DATE AS BILL_DATE, E_BILLWAYNO AS PARTY_EWAYBILLNO, '' AS ITEM_DETAILS," & vbCrLf _
                & " VEHICLE_NO AS VEHICLE, CARRIERS AS TRANSPORT_MODE,GRNO,GRDATE,GRAMOUNT, '' AS CUST_PO_NO, '' AS CUST_PO_DATE, TRIM(CHALLAN_PREFIX||GATEPASS_NO) AS GATEPASS_NO" & vbCrLf _
                & " FROM INV_GATEPASS_HDR" & vbCrLf _
                & " WHERE COMPANY_CODE=" & mInterUnitCompanyCode & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mCurrentUnitAccountCode) & "'" & vbCrLf _
                & " AND IS_GATENTRY_MADE='N'"

            SqlStr = SqlStr & vbCrLf & " AND AUTO_KEY_PASSNO='" & txtBillNo.Text & "'"
        ElseIf VB.Left(cboRefType.Text, 1) = "R" Then
            SqlStr = "SELECT TO_CHAR(AUTO_KEY_PASSNO) AS MKEY, TO_CHAR(AUTO_KEY_PASSNO) AS BILL_NO, GATEPASS_DATE AS BILL_DATE, E_BILLWAYNO AS PARTY_EWAYBILLNO, '' AS ITEM_DETAILS," & vbCrLf _
                & " VEHICLE_NO AS VEHICLE, CARRIERS AS TRANSPORT_MODE,GRNO,GRDATE,GRAMOUNT, '' AS CUST_PO_NO, NULL AS CUST_PO_DATE, TRIM(CHALLAN_PREFIX||GATEPASS_NO) AS GATEPASS_NO,'R' AS DOCTYPE" & vbCrLf _
                & " FROM INV_GATEPASS_HDR" & vbCrLf _
                & " WHERE COMPANY_CODE=" & mInterUnitCompanyCode & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mCurrentUnitAccountCode) & "'" & vbCrLf _
                & " AND IS_GATENTRY_MADE='N'"

            SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(AUTO_KEY_PASSNO)='" & txtBillNo.Text & "'"

            SqlStr = SqlStr & vbCrLf & " UNION ALL"

            SqlStr = SqlStr & vbCrLf & "SELECT MKEY, BILLNO AS BILL_NO, INVOICE_DATE AS BILL_DATE, E_BILLWAYNO AS PARTY_EWAYBILLNO, ITEMDESC AS ITEM_DETAILS, " & vbCrLf _
               & " VEHICLENO AS VEHICLE, CARRIERS AS TRANSPORT_MODE,GRNO,GRDATE,GRAMOUNT, CUST_PO_NO, CUST_PO_DATE,'' AS GATEPASS_NO,'I' AS DOCTYPE" & vbCrLf _
               & " FROM FIN_INVOICE_HDR" & vbCrLf _
               & " WHERE COMPANY_CODE=" & mInterUnitCompanyCode & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mCurrentUnitAccountCode) & "'" & vbCrLf _
               & " AND IS_GATENTRY_MADE='N' AND CANCELLED='N' AND REF_DESP_TYPE IN ('J')"

            SqlStr = SqlStr & vbCrLf & " AND BILLNO='" & txtBillNo.Text & "'"
        Else
            SqlStr = "SELECT MKEY, BILLNO AS BILL_NO, INVOICE_DATE AS BILL_DATE, E_BILLWAYNO AS PARTY_EWAYBILLNO, ITEMDESC AS ITEM_DETAILS, " & vbCrLf _
                & " VEHICLENO AS VEHICLE, CARRIERS AS TRANSPORT_MODE,GRNO,GRDATE,GRAMOUNT, CUST_PO_NO, CUST_PO_DATE,'' AS GATEPASS_NO" & vbCrLf _
                & " FROM FIN_INVOICE_HDR" & vbCrLf _
                & " WHERE COMPANY_CODE=" & mInterUnitCompanyCode & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mCurrentUnitAccountCode) & "'" & vbCrLf _
                & " AND IS_GATENTRY_MADE='N' AND CANCELLED='N'"

            If VB.Left(cboRefType.Text, 1) = "P" Then
                SqlStr = SqlStr & vbCrLf & " AND REF_DESP_TYPE IN ('P','G','F','S')"
            ElseIf VB.Left(cboRefType.Text, 1) = "I" Or VB.Left(cboRefType.Text, 1) = "3" Then
                SqlStr = SqlStr & vbCrLf & " AND REF_DESP_TYPE IN ('S','Q','L')"
            ElseIf VB.Left(cboRefType.Text, 1) = "R" Then
                SqlStr = SqlStr & vbCrLf & " AND REF_DESP_TYPE IN ('J')"
            End If

            SqlStr = SqlStr & vbCrLf & " AND BILLNO='" & txtBillNo.Text & "'"

        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


        With RsTemp
            If Not .EOF Then
                mSaleMKey = IIf(IsDBNull(.Fields("MKEY").Value), "", .Fields("MKEY").Value)
                txtBillNo.Text = IIf(IsDBNull(.Fields("BILL_NO").Value), "", .Fields("BILL_NO").Value)
                txtBillDate.Text = VB6.Format(IIf(IsDBNull(.Fields("BILL_DATE").Value), "", .Fields("BILL_DATE").Value), "DD/MM/YYYY")
                txtEwayBillNo.Text = IIf(IsDBNull(.Fields("PARTY_EWAYBILLNO").Value), "", .Fields("PARTY_EWAYBILLNO").Value)
                TxtItemDesc.Text = IIf(IsDBNull(.Fields("ITEM_DETAILS").Value), "", .Fields("ITEM_DETAILS").Value)
                TxtTransporter.Text = IIf(IsDBNull(.Fields("TRANSPORT_MODE").Value), "", .Fields("TRANSPORT_MODE").Value)
                txtFreight.Text = IIf(IsDBNull(.Fields("GRAMOUNT").Value), "", .Fields("GRAMOUNT").Value)
                'TxtRemarks.Text = IIf(IsDBNull(.Fields("Remarks").Value), "", .Fields("Remarks").Value)
                txtOldERPNo.Text = IIf(IsDBNull(.Fields("GATEPASS_NO").Value), "", .Fields("GATEPASS_NO").Value)
                ''

                mPONo = IIf(IsDBNull(.Fields("CUST_PO_NO").Value), "", .Fields("CUST_PO_NO").Value)
                mPODate = VB6.Format(IIf(IsDBNull(.Fields("CUST_PO_DATE").Value), "", .Fields("CUST_PO_DATE").Value), "DD/MM/YYYY")

                'OptFreight(0).Checked = IIf(.Fields("FREIGHT_TYPE").Value = 0, True, False)
                'OptFreight(1).Checked = IIf(.Fields("FREIGHT_TYPE").Value = 1, True, False)

                'mMode = IIf(IsDBNull(.Fields("MODE_TYPE").Value), "", .Fields("MODE_TYPE").Value)
                'cboMode.SelectedIndex = Val(VB.Left(mMode, 1)) - 1

                'txtDocsThru.Text = IIf(IsDBNull(.Fields("DOCS_THRU").Value), "", .Fields("DOCS_THRU").Value)
                txtVehicle.Text = IIf(IsDBNull(.Fields("VEHICLE").Value), "", .Fields("VEHICLE").Value)
                txtGRNo.Text = IIf(IsDBNull(.Fields("GRNO").Value), "", .Fields("GRNO").Value)
                txtGRDate.Text = IIf(IsDBNull(.Fields("GRDATE").Value), "", .Fields("GRDATE").Value)


                'chkShipTo.CheckState = IIf(.Fields("SHIPPED_TO_SAMEPARTY").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                'mShippedToCode = IIf(IsDBNull(.Fields("SHIPPED_TO_PARTY_CODE").Value), -1, .Fields("SHIPPED_TO_PARTY_CODE").Value)
                'mShippedToName = ""
                'If MainClass.ValidateWithMasterTable(mShippedToCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '    mShippedToName = MasterNo
                'End If

                'txtShippedTo.Text = mShippedToName

                'txtBillTo.Text = IIf(IsDBNull(.Fields("BILL_TO_LOC_ID").Value), "", .Fields("BILL_TO_LOC_ID").Value)
                'TxtShipTo.Text = IIf(IsDBNull(.Fields("SHIP_TO_LOC_ID").Value), "", .Fields("SHIP_TO_LOC_ID").Value)

                If VB.Left(cboRefType.Text, 1) = "R" Then
                    mDocType = IIf(IsDBNull(.Fields("DOCTYPE").Value), "R", .Fields("DOCTYPE").Value)
                Else
                    mDocType = ""
                End If

            End If
        End With

        ''Detail Part.....

        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)

        If VB.Left(cboRefType.Text, 1) = "J" Then
            SqlStr = "SELECT ID.AUTO_KEY_PASSNO, ID.SERIAL_NO, ID.ITEM_CODE, ID.ITEM_UOM, ID.ITEM_QTY, ID.ITEM_RATE, ID.HSN_CODE," & vbCrLf _
                & " ID.AMOUNT AS ITEM_AMT, ID.BATCH_NO, ID.HEAT_NO, IMST.CUSTOMER_PART_NO FROM INV_GATEPASS_DET ID, INV_ITEM_MST IMST" & vbCrLf _
                & " WHERE ID.COMPANY_CODE= IMST.COMPANY_CODE AND ID.ITEM_CODE=IMST.ITEM_CODE AND ID.AUTO_KEY_PASSNO='" & mSaleMKey & "'"
        ElseIf VB.Left(cboRefType.Text, 1) = "R" Then
            If mDocType = "R" Then
                SqlStr = "SELECT ID.AUTO_KEY_PASSNO,ID.SERIAL_NO,ID.ITEM_CODE,ID.ITEM_UOM,ID.ITEM_QTY,ID.ITEM_RATE,ID.HSN_CODE," & vbCrLf _
                   & " ID.AMOUNT AS ITEM_AMT,ID.BATCH_NO,ID.HEAT_NO, IMST.CUSTOMER_PART_NO FROM INV_GATEPASS_DET ID, INV_ITEM_MST IMST" & vbCrLf _
                   & " WHERE ID.COMPANY_CODE= IMST.COMPANY_CODE AND ID.ITEM_CODE=IMST.ITEM_CODE AND TO_CHAR(AUTO_KEY_PASSNO)='" & mSaleMKey & "'"

            Else
                SqlStr = "SELECT GH.BILL_NO OUR_AUTO_KEY_SO, GH.BILL_DATE OUR_SO_DATE, " & vbCrLf _
                    & " ID.ITEM_CODE, ID.ITEM_UOM, ID.HSNCODE AS HSN_CODE, ID.ITEM_QTY, ID.ITEM_RATE, ID.ITEM_AMT,IMST.CUSTOMER_PART_NO " & vbCrLf _
                    & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, INV_GATE_HDR GH, INV_ITEM_MST IMST" & vbCrLf _
                    & " WHERE IH.MKEY=ID.MKEY And IH.MKEY='" & mSaleMKey & "' " & vbCrLf _
                    & " AND IH.COMPANY_CODE=GH.COMPANY_CODE And ID.OUR_REF_NO=GH.AUTO_KEY_MRR" & vbCrLf _
                    & " AND IH.COMPANY_CODE= IMST.COMPANY_CODE AND ID.ITEM_CODE=IMST.ITEM_CODE"
            End If
        Else
            SqlStr = "Select IH.OUR_AUTO_KEY_SO, IH.OUR_SO_DATE, " & vbCrLf _
                & " ID.ITEM_CODE, ID.ITEM_UOM, ID.HSNCODE As HSN_CODE, ID.ITEM_QTY, ID.ITEM_RATE, ID.ITEM_AMT, CUSTOMER_PART_NO CUSTOMER_PART_NO " & vbCrLf _
                & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID" & vbCrLf _
                & " WHERE IH.MKEY=ID.MKEY And IH.MKEY='" & mSaleMKey & "'"
        End If


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempDet, ADODB.LockTypeEnum.adLockReadOnly)

        CntRow = 1
        With SprdMain
            If RsTempDet.EOF = False Then
                Do While RsTempDet.EOF = False
                    .Row = CntRow
                    .Col = ColPONo
                    If VB.Left(cboRefType.Text, 1) = "J" Then
                        .Text = "-1" & RsCompany.Fields("FYEAR").Value
                    ElseIf VB.Left(cboRefType.Text, 1) = "R" Then
                        If mDocType = "R" Then
                            .Text = Trim(IIf(IsDBNull(RsTempDet.Fields("AUTO_KEY_PASSNO").Value), "", RsTempDet.Fields("AUTO_KEY_PASSNO").Value))
                        Else
                            .Text = Trim(IIf(IsDBNull(RsTempDet.Fields("OUR_AUTO_KEY_SO").Value), "", RsTempDet.Fields("OUR_AUTO_KEY_SO").Value))
                        End If

                    Else
                        .Text = IIf(Val(mPONo) = 0, "", Val(mPONo))
                    End If

                    .Col = ColRGPItemCode
                    .Text = ""


                    .Col = ColPODate
                    If VB.Left(cboRefType.Text, 1) = "J" Then
                        .Text = VB6.Format(RunDate, "DD/MM/YYYY")
                    ElseIf VB.Left(cboRefType.Text, 1) = "R" Then
                        If mDocType = "R" Then
                            .Text = VB6.Format(txtBillDate.Text, "DD/MM/YYYY") '' VB6.Format(IIf(IsDBNull(RsTempDet.Fields("OUR_REF_DATE").Value), "", RsTempDet.Fields("OUR_REF_DATE").Value), "DD/MM/YYYY")
                        Else
                            .Text = VB6.Format(IIf(IsDBNull(RsTempDet.Fields("OUR_SO_DATE").Value), "", RsTempDet.Fields("OUR_SO_DATE").Value), "DD/MM/YYYY")
                        End If

                        mRGPItemCode = ""
                        ''Sandeep Kandwal
                        If mDocType = "R" Then
                            mRGPItemCode = Trim(IIf(IsDBNull(RsTempDet.Fields("CUSTOMER_PART_NO").Value), "", RsTempDet.Fields("CUSTOMER_PART_NO").Value))
                        Else
                            mItemCode = Trim(IIf(IsDBNull(RsTempDet.Fields("CUSTOMER_PART_NO").Value), "", RsTempDet.Fields("CUSTOMER_PART_NO").Value))

                            mRGPItemCode = GetRGPItemCode(mItemCode, txtMRRDate.Text)
                            '.Text = Trim(IIf(IsDBNull(RsTempDet.Fields("OUR_AUTO_KEY_SO").Value), "", RsTempDet.Fields("OUR_AUTO_KEY_SO").Value))
                        End If

                        .Col = ColRGPItemCode
                        .Text = mRGPItemCode

                    Else
                        .Text = VB6.Format(mPODate, "DD/MM/YYYY")
                    End If





                    If CheckConsolidatedMaster("INV_ITEM_MST") = True Or (RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 And (RsCompany.Fields("COMPANY_CODE").Value = 4 Or RsCompany.Fields("COMPANY_CODE").Value = 5)) Then
                        .Col = ColItemCode
                        .Text = Trim(IIf(IsDBNull(RsTempDet.Fields("ITEM_CODE").Value), "", RsTempDet.Fields("ITEM_CODE").Value))
                        mItemCode = Trim(IIf(IsDBNull(RsTempDet.Fields("ITEM_CODE").Value), "", RsTempDet.Fields("ITEM_CODE").Value))

                        .Col = ColItemName
                        If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            .Text = Trim(MasterNo)
                        Else
                            .Text = ""
                        End If

                        .Col = ColUnit
                        .Text = Trim(IIf(IsDBNull(RsTempDet.Fields("ITEM_UOM").Value), "", RsTempDet.Fields("ITEM_UOM").Value))

                        .Col = ColHSNCode
                        .Text = Trim(IIf(IsDBNull(RsTempDet.Fields("HSN_CODE").Value), "", RsTempDet.Fields("HSN_CODE").Value))
                    Else

                        If VB.Left(cboRefType.Text, 1) = "J" Then
                            mCustomerPartNo = Trim(IIf(IsDBNull(RsTempDet.Fields("ITEM_CODE").Value), "", RsTempDet.Fields("ITEM_CODE").Value))
                            If MainClass.ValidateWithMasterTable(mCustomerPartNo, "CUSTOMER_PART_NO", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                mItemCode = Trim(MasterNo)
                            Else
                                mItemCode = ""
                            End If
                        Else
                            mItemCode = Trim(IIf(IsDBNull(RsTempDet.Fields("CUSTOMER_PART_NO").Value), "", RsTempDet.Fields("CUSTOMER_PART_NO").Value))
                        End If

                        .Col = ColItemCode
                        .Text = Trim(mItemCode)

                        .Col = ColItemName
                        If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            .Text = Trim(MasterNo)
                        Else
                            .Text = ""
                        End If

                        .Col = ColUnit
                        If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "PURCHASE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            .Text = Trim(MasterNo)
                        Else
                            .Text = ""
                        End If

                        .Col = ColHSNCode
                        If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "HSN_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            .Text = Trim(MasterNo)
                        Else
                            .Text = ""
                        End If

                        .Col = ColItemPartNo
                        .Text = mCustomerPartNo
                        'If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "CUSTOMER_PART_NO", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        '    .Text = Trim(MasterNo)
                        'Else
                        '    .Text = ""
                        'End If

                    End If





                    .Col = ColPOQty
                    .Text = "0.00"

                    .Col = ColBalQty
                    .Text = "0.00"

                    .Col = ColBillQty
                    .Text = Trim(IIf(IsDBNull(RsTempDet.Fields("ITEM_QTY").Value), "", RsTempDet.Fields("ITEM_QTY").Value))

                    .Col = ColRate
                    .Text = Trim(IIf(IsDBNull(RsTempDet.Fields("ITEM_RATE").Value), "", RsTempDet.Fields("ITEM_RATE").Value))

                    .Col = ColAmount
                    .Text = Trim(IIf(IsDBNull(RsTempDet.Fields("ITEM_AMT").Value), "", RsTempDet.Fields("ITEM_AMT").Value))

                    .Col = ColItemCost
                    .Text = Trim(IIf(IsDBNull(RsTempDet.Fields("ITEM_AMT").Value), "", RsTempDet.Fields("ITEM_AMT").Value))

                    CntRow = CntRow + 1
                    .MaxRows = CntRow
                    RsTempDet.MoveNext()
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

    Private Sub txtOldERPDate_Validating(sender As Object, e As CancelEventArgs) Handles txtOldERPDate.Validating
        Dim Cancel As Boolean = e.Cancel

        If (txtOldERPDate.Text) = "" Then Exit Sub

        If Not IsDate(txtOldERPDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If

EventExitSub:
        e.Cancel = Cancel
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

    Private Sub CboPONo_KeyUp(sender As Object, e As KeyEventArgs) Handles CboPONo.KeyUp
        If e.KeyCode = System.Windows.Forms.Keys.F1 Then SearchPONo()
    End Sub

    Private Sub CboPONo_DoubleClick(sender As Object, e As EventArgs) Handles CboPONo.DoubleClick
        SearchPONo()
    End Sub

    Private Sub CboPONo_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles CboPONo.MouseDoubleClick
        SearchPONo()
    End Sub

    Private Sub FrmGateEntry_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 210, mReFormWidth - 210, mReFormWidth))
        FraFront.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        Frasprd.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        SSTab1.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        UltraGrid1.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750) ''VB6.TwipsToPixelsX(IIf(mReFormWidth > 210, mReFormWidth - 210, mReFormWidth))
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub ShowGRNLabelReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String, ByRef IsSubReport As Boolean, ByVal mPDF As Boolean, mPrePrint As String)

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

        CrReport.RecordSelectionFormula = "{IH.COMPANY_CODE} = " & RsCompany.Fields("COMPANY_CODE").Value & " AND {IH.AUTO_KEY_GATE} = " & Val(txtMRRNo.Text) & ""

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
    Private Sub txtDeliveryTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDeliveryTo.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
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
    Private Sub txtDeliveryToLoc_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDeliveryToLoc.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtDeliveryToLoc_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDeliveryToLoc.DoubleClick
        cmdDeliveryToLocSearch_Click(cmdDeliveryToLocSearch, New System.EventArgs())
    End Sub
    Private Sub txtDeliveryToLoc_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDeliveryToLoc.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDeliveryToLoc.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtDeliveryToLoc_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDeliveryToLoc.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdDeliveryToLocSearch_Click(cmdDeliveryToLocSearch, New System.EventArgs())
    End Sub
    Private Sub txtDeliveryToLoc_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDeliveryToLoc.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim xAcctCode As String

        If Trim(txtDeliveryTo.Text) = "" Then GoTo EventExitSub
        If Trim(txtDeliveryToLoc.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(Trim(txtDeliveryTo.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
            xAcctCode = MasterNo
        Else
            MsgBox("Invalid Delivery To Supplier Name.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

        If MainClass.ValidateWithMasterTable(txtDeliveryToLoc.Text, "LOCATION_ID", "LOCATION_ID", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(xAcctCode) & "'") = False Then
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

        If MainClass.SearchGridMasterBySQL2((txtDeliveryToLoc.Text), SqlStr) = True Then
            txtDeliveryToLoc.Text = AcName
            txtDeliveryToLoc_Validating(txtDeliveryToLoc, New System.ComponentModel.CancelEventArgs(False))
            If txtDeliveryToLoc.Enabled = True Then txtDeliveryToLoc.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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
