Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmDNRejReg
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    ''Dim PvtDBCn As ADODB.Connection
    Dim mAccountCode As String
    Private Const RowHeight As Short = 15

    Private Const ColMKEY As Short = 1
    Private Const ColVNo As Short = 2
    Private Const ColVDate As Short = 3
    Private Const ColMRRType As Short = 4
    Private Const ColMRRNo As Short = 5
    Private Const ColBillNo As Short = 6
    Private Const ColBillDate As Short = 7
    Private Const ColPartyCode As Short = 8
    Private Const ColPartyName As Short = 9
    Private Const ColItemCode As Short = 10
    Private Const ColItemName As Short = 11
    Private Const ColDrQty As Short = 12
    Private Const ColRate As Short = 13
    Private Const ColCrQty As Short = 14
    Private Const ColInvQty As Short = 15
    Private Const ColBalQty As Short = 16
    Private Const ColBalAmount As Short = 17
    Private Const ColBEDAmount As Short = 18
    Private Const ColCessAmount As Short = 19
    Private Const ColSHECAmount As Short = 20
    Private Const ColAEDAmount As Short = 21
    Private Const ColSTAmount As Short = 22
    Private Const ColRemarks As Short = 23

    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer

    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboRejType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboRejType.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboRejType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboRejType.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkALL.CheckStateChanged
        If ChkALL.CheckState = System.Windows.Forms.CheckState.Checked Then
            TxtAccount.Enabled = False
        Else
            TxtAccount.Enabled = True
        End If
        Call PrintStatus(False)
    End Sub

    Private Sub chkDRAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkDRAll.CheckStateChanged
        If chkDRAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtDRNo.Enabled = False
        Else
            txtDRNo.Enabled = True
        End If
        Call PrintStatus(False)
    End Sub


    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub
    Private Sub SearchAccounts()

        On Error GoTo ERR1
        MainClass.SearchGridMaster(TxtAccount.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND (SUPP_CUST_TYPE IN ('C','S'))")
        If AcName <> "" Then
            TxtAccount.Text = AcName
        End If
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForRejection(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        Dim mMKey As String
        Dim mVNo As String
        Dim mVdate As String
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mPartyCode As String
        Dim mPartyName As String
        Dim mItemCode As String
        Dim mItemName As String
        Dim mDrQty As Double
        Dim mRate As Double
        Dim mCRQty As Double
        Dim mInvQty As Double
        Dim mBalQty As Double
        Dim mBalAmount As Double
        Dim mRemarks As String
        Dim cntRow As Integer
        Dim mGTotal As Double
        Dim mSaleBillNo As String
        Dim mMRRNO As Double
        Dim mCreditNo As String
        Dim mMRRType As String
        Dim mBEDAmount As Double
        Dim mCESSAmount As Double
        Dim mSHECAmount As Double
        Dim mAEDAmount As Double
        Dim mSTAmount As Double
        Dim mItemValue As Double

        If FieldsVerification = False Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        MainClass.ClearGrid(SprdMain, RowHeight)
        LblTotalAmt.Text = "0.00"
        FormatSprdMain()

        If OptWise(0).Checked Then
            SqlStr = ShowRejection
        Else
            SqlStr = ShowRejectionMRR
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        cntRow = 1

        If Not RsTemp.EOF Then
            With SprdMain
                Do While Not RsTemp.EOF

                    mSaleBillNo = ""
                    mCreditNo = ""

                    mMKey = IIf(IsDbNull(RsTemp.Fields("mKey").Value), "", RsTemp.Fields("mKey").Value)
                    mVNo = IIf(IsDbNull(RsTemp.Fields("VNO").Value), "", RsTemp.Fields("VNO").Value)
                    mVdate = IIf(IsDbNull(RsTemp.Fields("VDATE").Value), "", RsTemp.Fields("VDATE").Value)
                    mMRRNO = IIf(IsDbNull(RsTemp.Fields("MRR_REF_NO").Value), -1, RsTemp.Fields("MRR_REF_NO").Value)
                    mBillNo = IIf(IsDbNull(RsTemp.Fields("SUPP_REF_NO").Value), "", RsTemp.Fields("SUPP_REF_NO").Value)
                    mBillDate = IIf(IsDbNull(RsTemp.Fields("SUPP_REF_DATE").Value), "", RsTemp.Fields("SUPP_REF_DATE").Value)
                    mPartyCode = IIf(IsDbNull(RsTemp.Fields("DEBITACCOUNTCODE").Value), "", RsTemp.Fields("DEBITACCOUNTCODE").Value)
                    mPartyName = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
                    mItemCode = Trim(IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))
                    mItemName = IIf(IsDbNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value)
                    mDrQty = IIf(IsDbNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value)
                    mRate = IIf(IsDbNull(RsTemp.Fields("ITEM_RATE").Value), 0, RsTemp.Fields("ITEM_RATE").Value)
                    mCRQty = IIf(IsDbNull(RsTemp.Fields("CR_QTY").Value), 0, RsTemp.Fields("CR_QTY").Value) ''GetCRQty(mMRRNO, mPartyCode, mItemCode, mCreditNo)
                    mInvQty = IIf(IsDbNull(RsTemp.Fields("INV_QTY").Value), 0, RsTemp.Fields("INV_QTY").Value) '' GetSaleQty(mMkey, mPartyCode, mMRRNO, mItemCode, mSaleBillNo)
                    mItemValue = IIf(IsDbNull(RsTemp.Fields("ITEMVALUE").Value), 0, RsTemp.Fields("ITEMVALUE").Value)
                    mCreditNo = GetCRNo(mMRRNO, mPartyCode, mItemCode)
                    mSaleBillNo = GetSaleNo(mMKey, mVdate, mPartyCode, mMRRNO, mItemCode)

                    mBalQty = mDrQty - mCRQty - mInvQty
                    mBalAmount = CDbl(VB6.Format(mRate * mBalQty, "0.00"))
                    mRemarks = mSaleBillNo & IIf(mCreditNo = "", "", IIf(mSaleBillNo = "", "", ",") & mCreditNo)

                    If optStatus(1).Checked = True And mBalQty = 0 Then GoTo NextRec
                    If optStatus(2).Checked = True And mBalQty <> 0 Then GoTo NextRec

                    .Row = cntRow
                    .Col = ColMKEY
                    .Text = mMKey

                    .Col = ColMRRNo
                    .Text = CStr(mMRRNO)

                    .Col = ColMRRType
                    mMRRType = GetMrrRefNo(mMRRNO)

                    If mMRRType = "R" Then
                        mMRRType = "RGP"
                    ElseIf mMRRType = "P" Then
                        mMRRType = "PO"
                    ElseIf mMRRType = "I" Then
                        mMRRType = "SR"
                    ElseIf mMRRType = "C" Then
                        mMRRType = "CASH"
                    ElseIf mMRRType = "J" Then
                        mMRRType = "JobWork"
                    ElseIf mMRRType = "F" Then
                        mMRRType = "FOC"
                    ElseIf mMRRType = "1" Then
                        mMRRType = "J/W REJ"
                    ElseIf mMRRType = "2" Then
                        mMRRType = "SR WR"
                    End If
                    .Text = mMRRType

                    .Col = ColVNo
                    .Text = mVNo

                    .Col = ColVDate
                    .Text = mVdate

                    .Col = ColBillNo
                    .Text = mBillNo

                    .Col = ColBillDate
                    .Text = mBillDate

                    .Col = ColPartyCode
                    .Text = mPartyCode

                    .Col = ColPartyName
                    .Text = mPartyName

                    .Col = ColItemCode
                    .Text = mItemCode

                    .Col = ColItemName
                    .Text = mItemName

                    .Col = ColDrQty
                    .Text = CStr(mDrQty)

                    .Col = ColRate
                    .Text = CStr(mRate)

                    .Col = ColCrQty
                    .Text = CStr(mCRQty)

                    .Col = ColInvQty
                    .Text = CStr(mInvQty)

                    .Col = ColBalQty
                    .Text = CStr(mBalQty)

                    .Col = ColBalAmount
                    .Text = CStr(mBalAmount)

                    mBEDAmount = GetExpAmount(mMKey, mItemValue, mBalAmount, "ED")
                    .Col = ColBEDAmount
                    .Text = CStr(mBEDAmount)

                    mCESSAmount = GetExpAmount(mMKey, mItemValue, mBalAmount, "EDU")
                    .Col = ColCessAmount
                    .Text = CStr(mCESSAmount)

                    mSHECAmount = GetExpAmount(mMKey, mItemValue, mBalAmount, "SHC")
                    .Col = ColSHECAmount
                    .Text = CStr(mSHECAmount)

                    mSHECAmount = GetExpAmount(mMKey, mItemValue, mBalAmount, "ADE")
                    .Col = ColAEDAmount
                    .Text = CStr(mAEDAmount)

                    mSTAmount = GetExpAmount(mMKey, mItemValue, mBalAmount, "ST")
                    mSTAmount = mSTAmount + GetExpAmount(mMKey, mItemValue, mBalAmount, "SUR")
                    .Col = ColSTAmount
                    .Text = CStr(mSTAmount)


                    .Col = ColRemarks
                    .Text = mRemarks

                    cntRow = cntRow + 1
                    .MaxRows = cntRow

                    mGTotal = mGTotal + mBalAmount

NextRec:
                    RsTemp.MoveNext()
                Loop
            End With
        End If


        LblTotalAmt.Text = VB6.Format(mGTotal, "0.00")
        '    FormatSprdMain
        Call PrintStatus(True)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '    Resume
    End Sub
    Public Function GetExpAmount(ByRef mKey As String, ByRef mItemValue As Double, ByRef mBalAmount As Double, ByRef pExpId As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mExpAmount As Double

        If mBalAmount <= 0 Then
            GetExpAmount = 0
            Exit Function
        End If

        If mItemValue <= 0 Then
            GetExpAmount = 0
            Exit Function
        End If

        SqlStr = "SELECT SUM(FIN_DNCN_EXP.AMOUNT) AS AMOUNT" & vbCrLf & " FROM FIN_DNCN_EXP,FIN_INTERFACE_MST " & vbCrLf & " WHERE " & vbCrLf & " FIN_INTERFACE_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FIN_DNCN_EXP.ExpCode=FIN_INTERFACE_MST.Code " & vbCrLf & " AND FIN_DNCN_EXP.Mkey='" & mKey & "'" & vbCrLf & " AND FIN_INTERFACE_MST.IDENTIFICATION='" & pExpId & "'" & vbCrLf & " ORDER BY SUBROWNO"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsTemp.EOF Then
            mExpAmount = IIf(IsDbNull(RsTemp.Fields("Amount").Value), 0, RsTemp.Fields("Amount").Value)
        End If

        GetExpAmount = mExpAmount * mBalAmount / mItemValue

        Exit Function
ErrPart:
        GetExpAmount = 0
    End Function

    Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        Dim mDRNo As String

        If MainClass.ChkIsdateF(txtDateFrom) = False Then txtDateFrom.Focus() : Exit Function
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then txtDateFrom.Focus()
        If MainClass.ChkIsdateF(txtDateTo) = False Then txtDateTo.Focus() : Exit Function
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then txtDateTo.Focus()

        If ChkALL.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If CDbl(Trim(TxtAccount.Text)) = -1 Then
                MsgInformation("Please Select Account")
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
            Else
                MsgInformation("Please Select Valid Account")
                Exit Function
            End If
        End If

        If chkDRAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If CDbl(Trim(txtDRNo.Text)) = -1 Then
                MsgInformation("Please Select Debit Note No.")
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((txtDRNo.Text), "VNO", "VNO", "FIN_DNCN_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "") = True Then
                mDRNo = MasterNo
            Else
                MsgInformation("Please Select Valid Debit Note No.")
                Exit Function
            End If
        End If

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Public Sub frmDNRejReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmDNRejReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        Dim SqlStr As String
        Dim RS As ADODB.Recordset

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''''Set PvtDBCn = New ADODB.Connection
        ''''PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)


        Call PrintStatus(True)
        txtDateFrom.Text = RsCompany.Fields("START_DATE").Value
        txtDateTo.Text = CStr(RunDate)
        ChkALL.CheckState = System.Windows.Forms.CheckState.Checked
        TxtAccount.Enabled = False

        chkDRAll.CheckState = System.Windows.Forms.CheckState.Checked
        txtDRNo.Enabled = False
        LblTotalAmt.Text = "0.00"

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

        cboRejType.Items.Clear()
        cboRejType.Items.Add("Both")
        cboRejType.Items.Add("QC Rejection")
        cboRejType.Items.Add("Line Rejection")
        cboRejType.SelectedIndex = 0

        Call frmDNRejReg_Activated(eventSender, eventArgs)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmDNRejReg_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))
        Frame4.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function ShowRejection() As String

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mDivisionCode As Double

        SqlStr = "SELECT IH.MKEY, IH.VNO, IH.VDATE, MRR_REF_NO," & vbCrLf & " ID.SUPP_REF_NO, ID.SUPP_REF_DATE, " & vbCrLf & " IH.DEBITACCOUNTCODE, ACM.SUPP_CUST_NAME," & vbCrLf & " ID.ITEM_CODE, INVMST.ITEM_SHORT_DESC, IH.ITEMVALUE," & vbCrLf & " DECODE(INVMST.ISSUE_UOM,ID.ITEM_UOM,1,INVMST.UOM_FACTOR) * ID.ITEM_QTY AS ITEM_QTY, " & vbCrLf & " ID.ITEM_RATE/DECODE(INVMST.ISSUE_UOM,ID.ITEM_UOM,1,INVMST.UOM_FACTOR) AS ITEM_RATE,"

        SqlStr = SqlStr & vbCrLf & " GETREJDESPATCHQTY (IH.COMPANY_CODE, IH.MKEY,IH.DEBITACCOUNTCODE,ID.MRR_REF_NO,ID.ITEM_CODE) AS INV_QTY, "
        SqlStr = SqlStr & vbCrLf & " GETREJCREDITQTY (IH.COMPANY_CODE, IH.DEBITACCOUNTCODE,ID.MRR_REF_NO,ID.ITEM_CODE,DECODE(INVMST.ISSUE_UOM,ID.ITEM_UOM,1,INVMST.UOM_FACTOR)) CR_QTY"

        '& " TO_CHAR(SUM(DECODE(INVMST.ISSUE_UOM,ID.ITEM_UOM,1,INVMST.UOM_FACTOR) * ID.REJECTED_QTY)) AS REJECTED_QTY, ID.ITEM_RATE "

        SqlStr = SqlStr & vbCrLf & " FROM FIN_DNCN_HDR IH, FIN_DNCN_DET ID, " & vbCrLf & " FIN_SUPP_CUST_MST ACM,INV_ITEM_MST INVMST "

        SqlStr = SqlStr & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.Company_Code=ACM.Company_Code " & vbCrLf & " AND IH.DEBITACCOUNTCODE=ACM.SUPP_CUST_CODE " & vbCrLf & " AND ID.Company_Code=INVMST.Company_Code " & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.BOOKCODE=" & ConDebitNoteBookCode & " AND IH.BOOKTYPE='E' AND IH.DNCNTYPE='R' AND CANCELLED='N' AND APPROVED='Y'"

        If cboDivision.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            SqlStr = SqlStr & vbCrLf & " AND IH.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
        End If

        If cboRejType.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & " AND IH.DNCNFROM='M'"
        ElseIf cboRejType.SelectedIndex = 2 Then
            SqlStr = SqlStr & vbCrLf & " AND IH.DNCNFROM='S'"
        End If


        If ChkALL.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(TxtAccount.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " And ACM.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(UCase(TxtAccount.Text)) & "'"
        End If

        If chkDRAll.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtDRNo.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " And IH.VNO='" & MainClass.AllowSingleQuote(UCase(txtDRNo.Text)) & "'"
        End If

        If optType(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & "AND ACM.WITHIN_STATE='Y'"
        ElseIf optType(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & "AND ACM.WITHIN_STATE='N'"
        End If

        If optStatus(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND (DECODE(INVMST.ISSUE_UOM,ID.ITEM_UOM,1,INVMST.UOM_FACTOR) * ID.ITEM_QTY)<> "
            SqlStr = SqlStr & vbCrLf & " GETREJDESPATCHQTY (IH.COMPANY_CODE, IH.MKEY,IH.DEBITACCOUNTCODE,ID.MRR_REF_NO,ID.ITEM_CODE) "
            SqlStr = SqlStr & vbCrLf & " + GETREJCREDITQTY (IH.COMPANY_CODE, IH.DEBITACCOUNTCODE,ID.MRR_REF_NO,ID.ITEM_CODE,DECODE(INVMST.ISSUE_UOM,ID.ITEM_UOM,1,INVMST.UOM_FACTOR)) "
        ElseIf optStatus(2).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND (DECODE(INVMST.ISSUE_UOM,ID.ITEM_UOM,1,INVMST.UOM_FACTOR) * ID.ITEM_QTY)= "
            SqlStr = SqlStr & vbCrLf & " GETREJDESPATCHQTY (IH.COMPANY_CODE, IH.MKEY,IH.DEBITACCOUNTCODE,ID.MRR_REF_NO,ID.ITEM_CODE) "
            SqlStr = SqlStr & vbCrLf & " + GETREJCREDITQTY (IH.COMPANY_CODE, IH.DEBITACCOUNTCODE,ID.MRR_REF_NO,ID.ITEM_CODE,DECODE(INVMST.ISSUE_UOM,ID.ITEM_UOM,1,INVMST.UOM_FACTOR)) "
        End If

        If txtDateFrom.Text <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If txtDateTo.Text <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If


        If OptOrderBY(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " ORDER BY IH.VNO,IH.VDATE"
        Else
            SqlStr = SqlStr & vbCrLf & " ORDER BY ACM.SUPP_CUST_NAME,IH.VNO,IH.VDATE"
        End If

        ShowRejection = SqlStr

        Exit Function
ErrPart:
        MsgInformation(Err.Description)
    End Function

    Private Function ShowRejectionMRR() As String

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mDivisionCode As Double


        ''SUM(DECODE(INVMST.ISSUE_UOM,ID.ITEM_UOM,1,INVMST.UOM_FACTOR) * ID.ITEM_QTY)

        SqlStr = "SELECT '' AS MKEY, '' AS VNO, '' AS VDATE, MRR_REF_NO," & vbCrLf & " ID.SUPP_REF_NO, ID.SUPP_REF_DATE, " & vbCrLf & " IH.DEBITACCOUNTCODE, ACM.SUPP_CUST_NAME," & vbCrLf & " ID.ITEM_CODE, INVMST.ITEM_SHORT_DESC, 0 AS ITEMVALUE," & vbCrLf & " GETREJDEBITQTY(IH.COMPANY_CODE, IH.DEBITACCOUNTCODE,ID.MRR_REF_NO,ID.ITEM_CODE,DECODE(INVMST.ISSUE_UOM,ID.ITEM_UOM,1,INVMST.UOM_FACTOR)) AS ITEM_QTY, " & vbCrLf & " MAX(ID.ITEM_RATE/DECODE(INVMST.ISSUE_UOM,ID.ITEM_UOM,1,INVMST.UOM_FACTOR)) AS ITEM_RATE,"

        SqlStr = SqlStr & vbCrLf & " GETREJDESPATCHQTY_NEW (IH.COMPANY_CODE,IH.DEBITACCOUNTCODE,ID.MRR_REF_NO,ID.ITEM_CODE) AS INV_QTY, "
        SqlStr = SqlStr & vbCrLf & " GETREJCREDITQTY (IH.COMPANY_CODE, IH.DEBITACCOUNTCODE,ID.MRR_REF_NO,ID.ITEM_CODE,DECODE(INVMST.ISSUE_UOM,ID.ITEM_UOM,1,INVMST.UOM_FACTOR)) CR_QTY"

        '& " TO_CHAR(SUM(DECODE(INVMST.ISSUE_UOM,ID.ITEM_UOM,1,INVMST.UOM_FACTOR) * ID.REJECTED_QTY)) AS REJECTED_QTY, ID.ITEM_RATE "

        SqlStr = SqlStr & vbCrLf & " FROM FIN_DNCN_HDR IH, FIN_DNCN_DET ID, " & vbCrLf & " FIN_SUPP_CUST_MST ACM,INV_ITEM_MST INVMST "

        SqlStr = SqlStr & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.Company_Code=ACM.Company_Code " & vbCrLf & " AND IH.DEBITACCOUNTCODE=ACM.SUPP_CUST_CODE " & vbCrLf & " AND ID.Company_Code=INVMST.Company_Code " & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.BOOKCODE=" & ConDebitNoteBookCode & " AND IH.BOOKTYPE='E' AND IH.DNCNTYPE='R' AND CANCELLED='N' AND APPROVED='Y'"

        If cboDivision.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            SqlStr = SqlStr & vbCrLf & " AND IH.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
        End If

        If cboRejType.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & " AND IH.DNCNFROM='M'"
        ElseIf cboRejType.SelectedIndex = 2 Then
            SqlStr = SqlStr & vbCrLf & " AND IH.DNCNFROM='S'"
        End If


        If ChkALL.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(TxtAccount.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " And ACM.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(UCase(TxtAccount.Text)) & "'"
        End If

        If chkDRAll.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtDRNo.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " And IH.VNO='" & MainClass.AllowSingleQuote(UCase(txtDRNo.Text)) & "'"
        End If

        If optType(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & "AND ACM.WITHIN_STATE='Y'"
        ElseIf optType(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & "AND ACM.WITHIN_STATE='N'"
        End If

        If optStatus(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND GETREJDEBITQTY(IH.COMPANY_CODE, IH.DEBITACCOUNTCODE,ID.MRR_REF_NO,ID.ITEM_CODE,DECODE(INVMST.ISSUE_UOM,ID.ITEM_UOM,1,INVMST.UOM_FACTOR))<> "
            SqlStr = SqlStr & vbCrLf & " GETREJDESPATCHQTY_NEW (IH.COMPANY_CODE, IH.DEBITACCOUNTCODE,ID.MRR_REF_NO,ID.ITEM_CODE) "
            SqlStr = SqlStr & vbCrLf & " + GETREJCREDITQTY (IH.COMPANY_CODE, IH.DEBITACCOUNTCODE,ID.MRR_REF_NO,ID.ITEM_CODE,DECODE(INVMST.ISSUE_UOM,ID.ITEM_UOM,1,INVMST.UOM_FACTOR)) "
        ElseIf optStatus(2).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND GETREJDEBITQTY(IH.COMPANY_CODE, IH.DEBITACCOUNTCODE,ID.MRR_REF_NO,ID.ITEM_CODE,DECODE(INVMST.ISSUE_UOM,ID.ITEM_UOM,1,INVMST.UOM_FACTOR))= "
            SqlStr = SqlStr & vbCrLf & " GETREJDESPATCHQTY_NEW (IH.COMPANY_CODE, IH.DEBITACCOUNTCODE,ID.MRR_REF_NO,ID.ITEM_CODE) "
            SqlStr = SqlStr & vbCrLf & " + GETREJCREDITQTY (IH.COMPANY_CODE, IH.DEBITACCOUNTCODE,ID.MRR_REF_NO,ID.ITEM_CODE,DECODE(INVMST.ISSUE_UOM,ID.ITEM_UOM,1,INVMST.UOM_FACTOR)) "
        End If

        If txtDateFrom.Text <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND IH.VDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If txtDateTo.Text <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND IH.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        SqlStr = SqlStr & vbCrLf & " GROUP BY " & vbCrLf & " IH.COMPANY_CODE, MRR_REF_NO, ID.SUPP_REF_NO, ID.SUPP_REF_DATE, ACM.SUPP_CUST_NAME, " & vbCrLf & " IH.DEBITACCOUNTCODE, ID.ITEM_CODE, INVMST.ITEM_SHORT_DESC,DECODE(INVMST.ISSUE_UOM,ID.ITEM_UOM,1,INVMST.UOM_FACTOR)"


        If OptOrderBY(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " ORDER BY MRR_REF_NO"
        Else
            SqlStr = SqlStr & vbCrLf & " ORDER BY ACM.SUPP_CUST_NAME,MRR_REF_NO"
        End If



        ShowRejectionMRR = SqlStr

        Exit Function
ErrPart:
        MsgInformation(Err.Description)
    End Function
    Private Function GetSaleQty(ByRef pMKey As String, ByRef pSupplierCode As String, ByRef pMRRNo As Double, ByRef pItemCode As String, ByRef pInvNo As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsSale As ADODB.Recordset

        pInvNo = ""
        GetSaleQty = 0

        SqlStr = "SELECT SUM(PACKED_QTY) AS PACKED_QTY, IH.BILLNO " & vbCrLf & " FROM " & vbCrLf & " FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, " & vbCrLf & " DSP_DESPATCH_HDR DH, DSP_DESPATCH_DET DD" & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf & " AND DH.AUTO_KEY_DESP=DD.AUTO_KEY_DESP" & vbCrLf & " AND IH.AUTO_KEY_DESP=DH.AUTO_KEY_DESP" & vbCrLf & " AND ID.ITEM_CODE=DD.ITEM_CODE AND ID.SUBROWNO=DD.SERIAL_NO" & vbCrLf & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.SUPP_CUST_CODE='" & pSupplierCode & "'" & vbCrLf & " AND DH.AUTO_KEY_SO='" & pMKey & "'" & vbCrLf & " AND ID.ITEM_CODE='" & Trim(pItemCode) & "'" & vbCrLf & " AND DD.MRR_REF_NO=" & pMRRNo & "" & vbCrLf & " AND DH.DESP_TYPE IN ('Q','L') AND IH.CANCELLED='N'"


        'AND DD.OUR_REF_DATE='" & vb6.Format(pBillDate, "DD-MMM-YYYY") & "'


        SqlStr = SqlStr & vbCrLf & " GROUP BY IH.BILLNO"

        SqlStr = SqlStr & vbCrLf & " ORDER BY IH.BILLNO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSale, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsSale.EOF Then
            Do While Not RsSale.EOF
                pInvNo = pInvNo & IIf(pInvNo = "", "", ", ") & IIf(IsDbNull(RsSale.Fields("BILLNO").Value), "", RsSale.Fields("BILLNO").Value)
                GetSaleQty = GetSaleQty + IIf(IsDbNull(RsSale.Fields("PACKED_QTY").Value), 0, RsSale.Fields("PACKED_QTY").Value)
                RsSale.MoveNext()
            Loop
        End If

        Exit Function
ErrPart:
        MsgInformation(Err.Description)
    End Function

    Private Function GetSaleNo(ByRef pMKey As String, ByRef pDate As String, ByRef pSupplierCode As String, ByRef pMRRNo As Double, ByRef pItemCode As String) As String

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsSale As ADODB.Recordset

        GetSaleNo = ""

        If OptWise(1).Checked Then
            GoTo NextRow
        End If

        If CDate(pDate) >= CDate(PubGSTApplicableDate) Then
            SqlStr = "SELECT DISTINCT DH.AUTO_KEY_DESP AS BILLNO " & vbCrLf & " FROM " & vbCrLf & " DSP_DESPATCH_HDR DH, DSP_DESPATCH_DET DD" & vbCrLf & " WHERE DH.AUTO_KEY_DESP=DD.AUTO_KEY_DESP" & vbCrLf & " AND DH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND DH.SUPP_CUST_CODE='" & pSupplierCode & "'" & vbCrLf & " AND DD.MRR_REF_NO=" & pMRRNo & "" & vbCrLf & " AND DD.ITEM_CODE='" & Trim(pItemCode) & "'" & vbCrLf & " AND DH.DESP_TYPE IN ('Q','L') AND DH.DESP_STATUS=1"

            SqlStr = SqlStr & vbCrLf & " ORDER BY DH.AUTO_KEY_DESP"
        Else
NextRow:
            SqlStr = "SELECT DISTINCT IH.BILLNO " & vbCrLf & " FROM " & vbCrLf & " FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, " & vbCrLf & " DSP_DESPATCH_HDR DH, DSP_DESPATCH_DET DD" & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf & " AND DH.AUTO_KEY_DESP=DD.AUTO_KEY_DESP" & vbCrLf & " AND IH.AUTO_KEY_DESP=DH.AUTO_KEY_DESP" & vbCrLf & " AND ID.ITEM_CODE=DD.ITEM_CODE AND ID.SUBROWNO=DD.SERIAL_NO" & vbCrLf & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.SUPP_CUST_CODE='" & pSupplierCode & "'" & vbCrLf & " AND DH.AUTO_KEY_SO='" & pMKey & "'" & vbCrLf & " AND ID.ITEM_CODE='" & Trim(pItemCode) & "'" & vbCrLf & " AND DD.MRR_REF_NO=" & pMRRNo & "" & vbCrLf & " AND DH.DESP_TYPE IN ('Q','L') AND IH.CANCELLED='N'"

            SqlStr = SqlStr & vbCrLf & " ORDER BY IH.BILLNO"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSale, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsSale.EOF Then
            Do While Not RsSale.EOF
                GetSaleNo = GetSaleNo & IIf(GetSaleNo = "", "", ", ") & IIf(IsDbNull(RsSale.Fields("BILLNO").Value), "", RsSale.Fields("BILLNO").Value)
                RsSale.MoveNext()
            Loop
        End If

        Exit Function
ErrPart:
        MsgInformation(Err.Description)
    End Function

    Private Function GetCRQty(ByRef pMRRNo As Double, ByRef pSupplierCode As String, ByRef pItemCode As String, ByRef mCNNo As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsSale As ADODB.Recordset

        mCNNo = ""
        GetCRQty = 0

        SqlStr = "SELECT IH.VNO, BOOKCODE, SUM(DECODE(INVMST.ISSUE_UOM,ID.ITEM_UOM,1,INVMST.UOM_FACTOR) * ID.ITEM_QTY) AS QTY " & vbCrLf & " FROM " & vbCrLf & " FIN_DNCN_HDR IH, FIN_DNCN_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND ID.MRR_REF_NO=" & pMRRNo & "" & vbCrLf & " AND IH.CREDITACCOUNTCODE='" & pSupplierCode & "'" & vbCrLf & " AND ID.ITEM_CODE='" & Trim(pItemCode) & "'" & vbCrLf & " AND IH.BOOKCODE=" & ConCreditNoteBookCode & " AND IH.DNCNTYPE='R' AND CANCELLED='N' AND APPROVED='Y'"

        ''AND IH.DNCNFROM IN ('M','R')

        SqlStr = SqlStr & vbCrLf & " GROUP BY IH.VNO, BOOKCODE "

        SqlStr = SqlStr & vbCrLf & " ORDER BY IH.VNO, BOOKCODE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSale, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsSale.EOF Then
            Do While Not RsSale.EOF
                mCNNo = mCNNo & IIf(mCNNo = "", "", ", ") & IIf(IsDbNull(RsSale.Fields("VNO").Value), "", RsSale.Fields("VNO").Value)

                GetCRQty = GetCRQty + IIf(IsDbNull(RsSale.Fields("QTY").Value), 0, RsSale.Fields("QTY").Value)
                RsSale.MoveNext()
            Loop
        End If
        Exit Function
ErrPart:
        MsgInformation(Err.Description)
    End Function

    Private Function GetCRNo(ByRef pMRRNo As Double, ByRef pSupplierCode As String, ByRef pItemCode As String) As String

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsSale As ADODB.Recordset

        GetCRNo = ""
        ''(IH.CREDITACCOUNTCODE='" & pSupplierCode & "'

        SqlStr = "SELECT DISTINCT IH.VNO" & vbCrLf & " FROM " & vbCrLf & " FIN_DNCN_HDR IH, FIN_DNCN_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND ID.MRR_REF_NO=" & pMRRNo & "" & vbCrLf & " AND " & vbCrLf & " ID.ITEM_CODE='" & Trim(pItemCode) & "'" & vbCrLf & " AND IH.DNCNTYPE='R' AND CANCELLED='N' AND APPROVED='Y'"

        If OptWise(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND IH.BOOKCODE=" & ConCreditNoteBookCode & " "
        Else
            '            SqlStr = SqlStr & vbCrLf & " AND IH.BOOKCODE=" & ConCreditNoteBookCode & " "
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY IH.VNO DESC"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSale, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsSale.EOF Then
            Do While Not RsSale.EOF
                GetCRNo = GetCRNo & IIf(GetCRNo = "", "", ", ") & IIf(IsDbNull(RsSale.Fields("VNO").Value), "", RsSale.Fields("VNO").Value)
                RsSale.MoveNext()
            Loop
        End If
        Exit Function
ErrPart:
        MsgInformation(Err.Description)
    End Function


    Private Sub FormatSprdMain()

        Dim I As Integer

        With SprdMain
            .MaxCols = ColRemarks
            .set_RowHeight(0, RowHeight * 1.5)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1


            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColMKEY, 8)
            .ColHidden = True

            .Col = ColVNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColVNo, 10.5)
            .ColHidden = IIf(OptWise(0).Checked = True, False, True)

            .Col = ColVDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColVDate, 10.5)
            .ColHidden = IIf(OptWise(0).Checked = True, False, True)

            .Col = ColBillNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColBillNo, 9)

            .Col = ColBillDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColBillDate, 9)

            .Col = ColPartyCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColPartyCode, 9)

            .Col = ColPartyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyName, 27)
            .ColsFrozen = ColPartyName

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColItemCode, 9)

            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemName, 20)

            For I = ColDrQty To ColSTAmount
                .Col = I
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("99999999999.99")
                .TypeFloatMin = CDbl("-99999999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(I, 8)
            Next


            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColRemarks, 8)


            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            SprdMain.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            '    SprdMain.GridColor = &HC00000
        End With
        WriteColHeadings()
    End Sub
    Private Sub WriteColHeadings()
        With SprdMain
            .Row = 0

            .Col = ColMKEY
            .Text = "MKey"

            .Col = ColVNo
            .Text = "Voucher No."

            .Col = ColVDate
            .Text = "Voucher Date"

            .Col = ColMRRNo
            .Text = "MRR No."

            .Col = ColMRRType
            .Text = "MRR Ref Type"

            .Col = ColBillNo
            .Text = "Bill No."

            .Col = ColBillDate
            .Text = "Bill Date"

            .Col = ColPartyCode
            .Text = "Party Code"

            .Col = ColPartyName
            .Text = "Party Name"

            .Col = ColItemCode
            .Text = "Item Code"

            .Col = ColItemName
            .Text = "Item Description"

            .Col = ColDrQty
            .Text = "Debit Note Qty"

            .Col = ColRate
            .Text = "Debit Note Rate"

            .Col = ColCrQty
            .Text = "Credit Note Qty"

            .Col = ColInvQty
            .Text = "INV Qty"

            .Col = ColBalQty
            .Text = "Balance Qty"

            .Col = ColBalAmount
            .Text = "Item Balance Value"

            .Col = ColBEDAmount
            .Text = "BED Amount"

            .Col = ColCessAmount
            .Text = "Cess Amount"

            .Col = ColSHECAmount
            .Text = "SHEC Amount"

            .Col = ColAEDAmount
            .Text = "AED Amount"

            .Col = ColSTAmount
            .Text = "Sales Tax Amount"

            .Col = ColRemarks
            .Text = "Remarks"

        End With
    End Sub
    Private Sub frmDNRejReg_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub
    Private Sub txtAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub TxtAccount_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.DoubleClick
        SearchAccounts()
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
        Dim SqlStr As String
        On Error GoTo ERR1
        If TxtAccount.Text = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            TxtAccount.Text = UCase(Trim(TxtAccount.Text))
        Else
            MsgInformation("No Such Account in Account Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForRejection(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportForRejection(ByRef Mode As Crystal.DestinationConstants)


        On Error GoTo ERR1
        Dim All As Boolean
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String
        Dim PrintStatus As Boolean
        Dim mReportFileName As String


        If TxtAccount.Text = "" Then PrintStatus = False Else PrintStatus = True


        SqlStr = ""



        '''''Select Record for print...

        If MainClass.FillPrintDummyDataFromSprd(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols, PubDBCn) = False Then GoTo ERR1

        SqlStr = MainClass.FetchFromTempData(SqlStr, "")

        mTitle = "Rejection Debit Note Status Register"

        If Trim(cboDivision.Text) <> "ALL" Then
            mTitle = mTitle & "[" & cboDivision.Text & "]"
        End If

        If optStatus(1).Checked = True Then
            mTitle = mTitle & " (Pending)"
        ElseIf optStatus(2).Checked = True Then
            mTitle = mTitle & " (Complete)"
        End If

        mSubTitle = "From: " & VB6.Format(txtDateFrom.Text, "DD MMM, YYYY") & " To: " & VB6.Format(txtDateTo.Text, "DD MMM, YYYY")


        mReportFileName = "DNRegStatusReg.Rpt"

        Call ShowReport(SqlStr, mReportFileName, Mode, mTitle, mSubTitle)

        Exit Sub
ERR1:
        If Err.Number = 32755 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        Else
            MsgInformation(Err.Description)
        End If
        '    Resume
    End Sub


    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub
    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtdateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateFrom) = False Then
            txtDateFrom.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then
            txtDateFrom.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateTo) = False Then
            txtDateTo.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then
            txtDateTo.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDRNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDRNo.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtDRNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDRNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDRNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
End Class
