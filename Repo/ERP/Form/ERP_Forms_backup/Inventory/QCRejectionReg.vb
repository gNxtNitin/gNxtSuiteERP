Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmQCRejectionReg
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    ''Dim PvtDBCn As ADODB.Connection
    Dim mAccountCode As String
    Private Const RowHeight As Short = 15

    Private Const ColGRDate As Short = 1
    Private Const ColGRNo As Short = 2
    Private Const ColBillNo As Short = 3
    Private Const ColPartyName As Short = 4
    Private Const ColItemName As Short = 5
    Private Const ColQty As Short = 6
    Private Const ColReQty As Short = 7
    Private Const ColDRQty As Short = 8
    Private Const ColInvQty As Short = 9
    Private Const ColReofferNo As Short = 10
    Private Const ColDNNo As Short = 11
    Private Const ColInvNo As Short = 12
    Private Const ColRate As Short = 13
    Private Const ColAmount As Short = 14
    Private Const colRemarks As Short = 15
    Private Const ColPDIRFlag As Short = 16
    Private Const ColStatus As Short = 17


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

    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkALL.CheckStateChanged
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            TxtAccount.Enabled = False
        Else
            TxtAccount.Enabled = True
        End If
        Call PrintStatus(False)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
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
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntRow As Integer
        Dim mQty As Double
        Dim mReQty As Double
        Dim mRate As Double
        Dim mAmount As Double
        Dim mGTotal As Double
        Dim mPurBillNO As String
        Dim mMRRNo As Double
        Dim mSupplierCode As String
        Dim mSupplierName As String = ""
        Dim mItemCode As String
        Dim mItemName As String
        Dim mBillNo As String = ""
        Dim mDNNo As String = ""
        Dim mMRRDate As String
        Dim mDRQty As Double
        Dim mInvQty As Double
        Dim mReofferNo As String = ""
        Dim mBalQty As Double
        Dim mShow As Boolean
        Dim mShowBill As Boolean
        Dim mRemarks As String
        Dim mPDIRFlag As String

        If FieldsVerification = False Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        MainClass.ClearGrid(SprdMain, RowHeight)
        LblTotalAmt.Text = "0.00"
        FormatSprdMain()

        SqlStr = ShowRejection
        '    MainClass.AssignDataInSprd SqlStr, AData1, StrConn, "Y"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        cntRow = 1

        If Not RsTemp.EOF Then
            With SprdMain
                Do While Not RsTemp.EOF


                    mShow = False
                    mMRRNo = IIf(IsDbNull(RsTemp.Fields("AUTO_KEY_MRR").Value), "", RsTemp.Fields("AUTO_KEY_MRR").Value)
                    mPurBillNO = IIf(IsDbNull(RsTemp.Fields("BILL_NO").Value), "", RsTemp.Fields("BILL_NO").Value)
                    mMRRDate = IIf(IsDbNull(RsTemp.Fields("MRR_DATE").Value), "", RsTemp.Fields("MRR_DATE").Value)
                    mSupplierCode = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value)
                    mSupplierName = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
                    mItemCode = IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)
                    mItemName = IIf(IsDbNull(RsTemp.Fields("Item_Short_Desc").Value), "", RsTemp.Fields("Item_Short_Desc").Value)
                    mRemarks = IIf(IsDbNull(RsTemp.Fields("Remarks").Value), "", RsTemp.Fields("Remarks").Value)
                    mPDIRFlag = IIf(IsDbNull(RsTemp.Fields("PDIR_FLAG").Value), "", RsTemp.Fields("PDIR_FLAG").Value)


                    mQty = Val(IIf(IsDbNull(RsTemp.Fields("REJECTED_QTY").Value), 0, RsTemp.Fields("REJECTED_QTY").Value))
                    mReQty = CDbl("0.00")
                    mDRQty = CDbl("0.00")
                    mInvQty = CDbl("0.00")

                    mRate = Val(IIf(IsDbNull(RsTemp.Fields("ITEM_RATE").Value), 0, RsTemp.Fields("ITEM_RATE").Value))

                    If GetReOfferDetail(mMRRNo, mSupplierCode, mItemCode, mReofferNo, mReQty) = False Then GoTo ErrPart
                    If GetDRDetail(mMRRNo, mSupplierCode, mItemCode, mDNNo, mDRQty) = False Then GoTo ErrPart
                    If GetSaleDetail(mMRRNo, mMRRDate, mSupplierCode, mItemCode, mBillNo, mInvQty) = False Then GoTo ErrPart

                    mBalQty = mQty - mReQty - mDRQty

                    If optDNStatus(0).Checked = True Then
                        mShow = True
                    ElseIf optDNStatus(1).Checked = True Then
                        mShow = IIf(mBalQty > 0, True, False)
                    ElseIf optDNStatus(2).Checked = True Then
                        mShow = IIf(mBalQty <= 0 And mDRQty > 0, True, False)
                    End If

                    If optINVStatus(0).Checked = True Then
                        mShowBill = True
                    ElseIf optINVStatus(1).Checked = True Then
                        mShowBill = IIf(mDRQty <> mInvQty, True, False) ''IIf(mBillNo = "", True, False)
                    ElseIf optINVStatus(2).Checked = True Then
                        mShowBill = IIf(mDRQty = mInvQty And mInvQty > 0, True, False)
                    End If

                    If mShow = True And mShowBill = True Then
                        .Row = cntRow

                        .Col = ColGRNo
                        .Text = Str(mMRRNo)

                        .Col = ColGRDate
                        .Text = VB6.Format(mMRRDate, "DD/MM/YYYY")

                        .Col = ColBillNo
                        .Text = mPurBillNO

                        .Col = ColPartyName
                        .Text = mSupplierName

                        .Col = ColItemName
                        .Text = mItemName

                        .Col = ColQty
                        .Text = VB6.Format(mQty, "0.00")

                        .Col = ColReQty
                        .Text = VB6.Format(mReQty, "0.00")

                        .Col = ColDRQty
                        .Text = VB6.Format(mDRQty, "0.00")

                        .Col = ColInvQty
                        .Text = VB6.Format(mInvQty, "0.00")

                        .Col = ColReofferNo
                        .Text = mReofferNo

                        .Col = ColDNNo
                        .Text = mDNNo

                        .Col = ColInvNo
                        .Text = mBillNo

                        .Col = ColRate
                        .Text = VB6.Format(mRate, "0.00")

                        .Col = ColAmount
                        mAmount = mBalQty * mRate
                        .Text = VB6.Format(mAmount, "0.00")
                        mGTotal = mGTotal + CDbl(VB6.Format(mAmount, "0.00"))

                        .Col = colRemarks
                        .Text = mRemarks

                        .Col = ColPDIRFlag
                        .Text = mPDIRFlag

                        'ColPDIRFlag , DECODE(PDIR_FLAG,'Y','YES','NO') AS PDIR_FLAG, mRemarks,mPDIRFlag

                        .Col = ColStatus
                        If mBalQty > 0 Then
                            .Text = "NO"
                        Else
                            .Text = IIf(mDRQty = mInvQty, "YES", "NO")
                        End If

                        cntRow = cntRow + 1
                        .MaxRows = cntRow
                    End If
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
    Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtDateFrom) = False Then txtDateFrom.Focus() : Exit Function
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then txtDateFrom.Focus()
        If MainClass.ChkIsdateF(txtDateTo) = False Then txtDateTo.Focus() : Exit Function
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then txtDateTo.Focus()

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(TxtAccount.Text) = "" Then
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
        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Public Sub frmQCRejectionReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmQCRejectionReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        ''Me.Width = VB6.TwipsToPixelsX(11355)


        Call PrintStatus(True)
        txtDateFrom.Text = RsCompany.Fields("Start_Date").Value
        txtDateTo.Text = CStr(RunDate)
        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtAccount.Enabled = False

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

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmQCRejectionReg_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
        Dim SqlStr As String = ""
        Dim mDivisionCode As Double

        SqlStr = "SELECT IH.MRR_DATE, IH.AUTO_KEY_MRR, IH.BILL_NO," & vbCrLf & " ACM.SUPP_CUST_NAME, INVMST.ITEM_SHORT_DESC, " & vbCrLf & " ACM.SUPP_CUST_CODE, INVMST.ITEM_CODE, " & vbCrLf & " TO_CHAR(SUM(DECODE(INVMST.ISSUE_UOM,ID.ITEM_UOM,1,INVMST.UOM_FACTOR) * ID.REJECTED_QTY)) AS REJECTED_QTY, ID.ITEM_RATE, " & vbCrLf & " ID.REMARKS, DECODE(ID.PDIR_FLAG,'Y','YES','NO') AS PDIR_FLAG " & vbCrLf & " FROM " & vbCrLf & " INV_GATE_HDR IH, INV_GATE_DET ID," & vbCrLf & " FIN_SUPP_CUST_MST ACM,INV_ITEM_MST INVMST " & vbCrLf & " WHERE IH.AUTO_KEY_MRR=ID.AUTO_KEY_MRR" & vbCrLf & " AND IH.Company_Code=ACM.Company_Code " & vbCrLf & " AND IH.SUPP_CUST_CODE=ACM.SUPP_CUST_CODE " & vbCrLf & " AND ID.Company_Code=INVMST.Company_Code " & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND MRR_STATUS='N' "

        If cboDivision.SelectedIndex > 0 Then
            If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionCode = CDbl(Trim(MasterNo))
            End If
            SqlStr = SqlStr & vbCrLf & " AND IH.DIV_CODE=" & Val(CStr(mDivisionCode)) & ""
        End If


        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(TxtAccount.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " And ACM.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(UCase(TxtAccount.Text)) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND ID.REJECTED_QTY>0 "

        If txtDateFrom.Text <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND IH.MRR_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If txtDateTo.Text <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND IH.MRR_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        SqlStr = SqlStr & vbCrLf & " GROUP BY IH.MRR_DATE, IH.AUTO_KEY_MRR, IH.BILL_NO," & vbCrLf & " ACM.SUPP_CUST_NAME, INVMST.ITEM_SHORT_DESC,ACM.SUPP_CUST_CODE, INVMST.ITEM_CODE,ITEM_RATE, ID.REMARKS, ID.PDIR_FLAG "

        If OptOrderBy(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " ORDER BY IH.MRR_DATE,IH.AUTO_KEY_MRR"
        Else
            SqlStr = SqlStr & vbCrLf & " ORDER BY ACM.SUPP_CUST_NAME,IH.BILL_NO,IH.MRR_DATE,IH.AUTO_KEY_MRR"
        End If

        ShowRejection = SqlStr

        Exit Function
ErrPart:
        MsgInformation(Err.Description)
    End Function

    Private Function GetSaleDetail(ByRef pMRRNo As Double, ByRef pMRRDate As String, ByRef pSupplierCode As String, ByRef pItemCode As String, ByRef mBillNo As String, ByRef mInvQty As Double) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsSale As ADODB.Recordset = Nothing

        GetSaleDetail = False
        mBillNo = ""
        mInvQty = 0

        If CDate(pMRRDate) >= CDate(PubGSTApplicableDate) Then
            SqlStr = "SELECT SUM(PACKED_QTY) AS PACKED_QTY, DH.AUTO_KEY_DESP AS BILLNO " & vbCrLf & " FROM " & vbCrLf & " DSP_DESPATCH_HDR DH, DSP_DESPATCH_DET DD" & vbCrLf & " WHERE DH.AUTO_KEY_DESP=DD.AUTO_KEY_DESP" & vbCrLf & " AND DH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND DH.SUPP_CUST_CODE='" & pSupplierCode & "'" & vbCrLf & " AND DD.MRR_REF_NO=" & pMRRNo & "" & vbCrLf & " AND DD.ITEM_CODE='" & Trim(pItemCode) & "'" & vbCrLf & " AND DH.DESP_TYPE IN ('Q','L') AND DH.DESP_STATUS=1"

            SqlStr = SqlStr & vbCrLf & " GROUP BY DH.AUTO_KEY_DESP"

            SqlStr = SqlStr & vbCrLf & " ORDER BY DH.AUTO_KEY_DESP"
        Else
            SqlStr = "SELECT SUM(PACKED_QTY) AS PACKED_QTY, IH.BILLNO " & vbCrLf & " FROM " & vbCrLf & " FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID, " & vbCrLf & " DSP_DESPATCH_HDR DH, DSP_DESPATCH_DET DD" & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf & " AND DH.AUTO_KEY_DESP=DD.AUTO_KEY_DESP" & vbCrLf & " AND IH.AUTO_KEY_DESP=DH.AUTO_KEY_DESP" & vbCrLf & " AND ID.ITEM_CODE=DD.ITEM_CODE" & vbCrLf & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.SUPP_CUST_CODE='" & pSupplierCode & "'" & vbCrLf & " AND DD.MRR_REF_NO=" & pMRRNo & "" & vbCrLf & " AND ID.ITEM_CODE='" & Trim(pItemCode) & "'" & vbCrLf & " AND DH.DESP_TYPE IN ('Q','L') AND IH.CANCELLED='N'"

            SqlStr = SqlStr & vbCrLf & " GROUP BY IH.BILLNO"

            SqlStr = SqlStr & vbCrLf & " ORDER BY IH.BILLNO"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSale, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsSale.EOF Then
            Do While Not RsSale.EOF
                mBillNo = mBillNo & IIf(mBillNo = "", "", ", ") & IIf(IsDbNull(RsSale.Fields("BILLNO").Value), "", RsSale.Fields("BILLNO").Value)
                mInvQty = mInvQty + IIf(IsDbNull(RsSale.Fields("PACKED_QTY").Value), 0, RsSale.Fields("PACKED_QTY").Value)
                RsSale.MoveNext()
            Loop
        Else
            mBillNo = ""
            mInvQty = 0
        End If
        GetSaleDetail = True

        Exit Function
ErrPart:
        MsgInformation(Err.Description)
    End Function
    Private Function GetDRDetail(ByRef pMRRNo As Double, ByRef pSupplierCode As String, ByRef pItemCode As String, ByRef mDNNo As String, ByRef mDNQty As Double) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsSale As ADODB.Recordset = Nothing

        GetDRDetail = False
        mDNNo = ""
        mDNQty = 0

        SqlStr = "SELECT IH.VNO, BOOKCODE, SUM(DECODE(INVMST.ISSUE_UOM,ID.ITEM_UOM,1,INVMST.UOM_FACTOR) * ID.ITEM_QTY) AS QTY " & vbCrLf & " FROM " & vbCrLf & " FIN_DNCN_HDR IH, FIN_DNCN_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND ID.MRR_REF_NO=" & pMRRNo & "" & vbCrLf & " AND IH.DEBITACCOUNTCODE='" & pSupplierCode & "'" & vbCrLf & " AND ID.ITEM_CODE='" & Trim(pItemCode) & "'" & vbCrLf & " AND IH.DNCNTYPE='R' AND CANCELLED='N' AND IH.DNCNFROM IN ('M','R') AND APPROVED='Y'"

        SqlStr = SqlStr & vbCrLf & " GROUP BY IH.VNO, BOOKCODE "

        SqlStr = SqlStr & vbCrLf & " ORDER BY IH.VNO, BOOKCODE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSale, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsSale.EOF Then
            Do While Not RsSale.EOF
                mDNNo = mDNNo & IIf(mDNNo = "", "", ", ") & IIf(IsDbNull(RsSale.Fields("VNO").Value), "", RsSale.Fields("VNO").Value)

                mDNQty = mDNQty + (IIf(RsSale.Fields("BOOKCODE").Value = ConDebitNoteBookCode, 1, -1) * IIf(IsDbNull(RsSale.Fields("QTY").Value), 0, RsSale.Fields("QTY").Value))
                RsSale.MoveNext()
            Loop
        Else
            mDNNo = ""
            mDNQty = 0
        End If

        SqlStr = "SELECT IH.VNO, BOOKCODE, SUM(DECODE(INVMST.ISSUE_UOM,ID.ITEM_UOM,1,INVMST.UOM_FACTOR) * ID.ITEM_QTY) AS QTY " & vbCrLf & " FROM " & vbCrLf & " FIN_DNCN_HDR IH, FIN_DNCN_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND ID.MRR_REF_NO=" & pMRRNo & "" & vbCrLf & " AND IH.CREDITACCOUNTCODE='" & pSupplierCode & "'" & vbCrLf & " AND ID.ITEM_CODE='" & Trim(pItemCode) & "'" & vbCrLf & " AND IH.DNCNTYPE='R' AND CANCELLED='N' AND IH.DNCNFROM IN ('M','R')"

        SqlStr = SqlStr & vbCrLf & " GROUP BY IH.VNO, BOOKCODE "

        SqlStr = SqlStr & vbCrLf & " ORDER BY IH.VNO, BOOKCODE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSale, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsSale.EOF Then
            Do While Not RsSale.EOF
                mDNNo = mDNNo & IIf(mDNNo = "", "", ", ") & IIf(IsDbNull(RsSale.Fields("VNO").Value), "", RsSale.Fields("VNO").Value)

                mDNQty = mDNQty + (IIf(RsSale.Fields("BOOKCODE").Value = ConDebitNoteBookCode, 1, -1) * IIf(IsDbNull(RsSale.Fields("QTY").Value), 0, RsSale.Fields("QTY").Value))
                RsSale.MoveNext()
            Loop
        End If

        GetDRDetail = True

        Exit Function
ErrPart:
        MsgInformation(Err.Description)
    End Function

    Private Function GetReOfferDetail(ByRef pMRRNo As Double, ByRef pSupplierCode As String, ByRef pItemCode As String, ByRef mReofferNo As String, ByRef mReQty As Double) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsSale As ADODB.Recordset = Nothing
        Dim mRefNo As String

        GetReOfferDetail = False
        mReofferNo = ""
        mReQty = 0

        SqlStr = "SELECT IH.AUTO_KEY_REF, SUM(DECODE(INVMST.ISSUE_UOM,ID.ITEM_UOM,1,INVMST.UOM_FACTOR) * ID.LOT_ACC_RWK) AS QTY " & vbCrLf & " FROM " & vbCrLf & " INV_REOFFER_HDR IH, INV_REOFFER_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.AUTO_KEY_REF=ID.AUTO_KEY_REF" & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.AUTO_KEY_MRR=" & pMRRNo & "" & vbCrLf & " AND IH.SUPP_CUST_CODE='" & pSupplierCode & "'" & vbCrLf & " AND ID.ITEM_CODE='" & Trim(pItemCode) & "' AND CANCELLED_STATUS='N' AND IS_POSTED='Y'"

        SqlStr = SqlStr & vbCrLf & " GROUP BY IH.AUTO_KEY_REF "

        SqlStr = SqlStr & vbCrLf & " ORDER BY IH.AUTO_KEY_REF"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSale, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsSale.EOF Then
            Do While Not RsSale.EOF
                mRefNo = IIf(IsDbNull(RsSale.Fields("AUTO_KEY_REF").Value), "", RsSale.Fields("AUTO_KEY_REF").Value)
                mRefNo = Mid(mRefNo, 1, Len(mRefNo) - 6)
                mReofferNo = mReofferNo & IIf(mReofferNo = "", "", ", ") & mRefNo
                mReQty = mReQty + IIf(IsDbNull(RsSale.Fields("QTY").Value), 0, RsSale.Fields("QTY").Value)
                RsSale.MoveNext()
            Loop
        Else
            mReofferNo = ""
            mReQty = 0
        End If
        GetReOfferDetail = True

        Exit Function
ErrPart:
        MsgInformation(Err.Description)
    End Function
    Private Sub FormatSprdMain()

        Dim I As Integer

        With SprdMain
            .MaxCols = ColStatus
            .set_RowHeight(0, RowHeight * 1.5)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColGRDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColGRDate, 8)

            .Col = ColGRNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColGRNo, 10.5)

            .Col = ColBillNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColBillNo, 9)

            .Col = ColPartyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyName, 27)
            .ColsFrozen = ColPartyName

            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemName, 20)

            For I = ColQty To ColInvQty
                .Col = I
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("99999999999.99")
                .TypeFloatMin = CDbl("-99999999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(I, 8)
            Next

            .Col = ColReofferNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColReofferNo, 8)

            .Col = ColDNNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColDNNo, 8)

            .Col = ColInvNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColInvNo, 9)

            For I = ColRate To ColAmount
                .Col = I
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("99999999999.99")
                .TypeFloatMin = CDbl("-99999999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(I, 8)
            Next

            .Col = colRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(colRemarks, 15)

            .Col = ColPDIRFlag
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPDIRFlag, 8)


            .Col = ColStatus
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColStatus, 8)

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

            .Col = ColGRDate
            .Text = "MRR Date"

            .Col = ColGRNo
            .Text = "MRR No."

            .Col = ColBillNo
            .Text = "Bill No."

            .Col = ColPartyName
            .Text = "Party Name"

            .Col = ColItemName
            .Text = "Item Description"

            .Col = ColQty
            .Text = "Rejected Qty"

            .Col = ColReQty
            .Text = "Re-Offer Qty"

            .Col = ColDRQty
            .Text = "DR Qty"

            .Col = ColInvQty
            .Text = "INV Qty"

            .Col = ColReofferNo
            .Text = "Re-Offer No."

            .Col = ColDNNo
            .Text = "DN/CN No."

            .Col = ColInvNo
            .Text = "Sale Bill No"

            .Col = ColRate
            .Text = "Item Rate"

            .Col = ColAmount
            .Text = "Item Value"

            .Col = colRemarks
            .Text = "Remarks"

            .Col = ColPDIRFlag
            .Text = "PDIR Flag"

            .Col = ColStatus
            .Text = "Status"

        End With
    End Sub
    Private Sub frmQCRejectionReg_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
    End Sub

    Private Sub optINVStatus_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optINVStatus.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optINVStatus.GetIndex(eventSender)
            Call PrintStatus(False)
        End If
    End Sub

    Private Sub optDNStatus_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optDNStatus.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optDNStatus.GetIndex(eventSender)
            Call PrintStatus(False)
        End If
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
        Dim SqlStr As String = ""
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
        Dim SqlStr As String = ""
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""
        Dim PrintStatus As Boolean
        Dim mReportFileName As String


        If TxtAccount.Text = "" Then PrintStatus = False Else PrintStatus = True


        SqlStr = ""

        If InsertPrintDummy = False Then GoTo ERR1


        'Select Record for print...

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        mTitle = "Rejection Register"

        If optDNStatus(1).Checked = True Then
            mTitle = mTitle & " (Debit Note Not Made)"
        ElseIf optDNStatus(2).Checked = True Then
            mTitle = mTitle & " (Debit Note Made)"
        End If

        If optINVStatus(1).Checked = True Then
            mTitle = mTitle & " (Invoice Not Made)"
        ElseIf optINVStatus(2).Checked = True Then
            mTitle = mTitle & " (Invoice Made)"
        End If

        If Trim(cboDivision.Text) <> "ALL" Then
            mTitle = mTitle & "[" & cboDivision.Text & "]"
        End If

        mSubTitle = "From: " & VB6.Format(txtDateFrom.Text, "DD MMM, YYYY") & " To: " & VB6.Format(txtDateTo.Text, "DD MMM, YYYY")

        If OptOrderBy(0).Checked = True And chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            mReportFileName = "RejectionReg.Rpt"
        Else
            mReportFileName = "RejectionReg_Name.Rpt"
        End If
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
    Private Function InsertPrintDummy() As Boolean


        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mGRDate As String
        Dim mGRNo As String
        Dim mBillNo As String
        Dim mPartyName As String
        Dim mItemName As String
        Dim mQty As String
        Dim mReQty As String
        Dim mDRQty As String
        Dim mInvQty As String
        Dim mReofferNo As String
        Dim mDNNo As String
        Dim mInvNo As String
        Dim mRate As String
        Dim mAmount As String
        Dim mStatus As String

        Dim mRemarks As String
        Dim mPDIRFlag As String

        InsertPrintDummy = False
        PubDBCn.Errors.Clear()

        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM Temp_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)



        SqlStr = ""
        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow

                .Col = ColGRDate
                mGRDate = VB6.Format(.Text, "DD/MM/YYYY")

                .Col = ColGRNo
                mGRNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColBillNo
                mBillNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColPartyName
                mPartyName = MainClass.AllowSingleQuote(.Text)

                .Col = ColItemName
                mItemName = MainClass.AllowSingleQuote(.Text)

                .Col = ColQty
                mQty = VB6.Format(.Text, "0.00")

                .Col = ColReQty
                mReQty = VB6.Format(.Text, "0.00")

                .Col = ColDRQty
                mDRQty = VB6.Format(.Text, "0.00")

                .Col = ColInvQty
                mInvQty = VB6.Format(.Text, "0.00")

                .Col = ColReofferNo
                mReofferNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColDNNo
                mDNNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColInvNo
                mInvNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColRate
                mRate = VB6.Format(.Text, "0.00")

                .Col = ColAmount
                mAmount = VB6.Format(.Text, "0.00")

                .Col = ColStatus
                mStatus = MainClass.AllowSingleQuote(.Text)

                .Col = colRemarks
                mRemarks = MainClass.AllowSingleQuote(.Text)

                .Col = ColPDIRFlag
                mPDIRFlag = MainClass.AllowSingleQuote(.Text)

                SqlStr = " Insert into Temp_PrintDummyData ( " & vbCrLf & " UserID, SubRow, " & vbCrLf & " Field1, Field2, Field3, " & vbCrLf & " Field4, Field5, Field6, " & vbCrLf & " Field7, Field8, Field9, " & vbCrLf & " Field10, Field11, Field12, Field13, Field14, Field15, Field16, Field17) Values (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & cntRow & ", " & vbCrLf & " '" & mGRDate & "', '" & mGRNo & "', " & vbCrLf & " '" & mPartyName & "', '" & mItemName & "', " & vbCrLf & " '" & mQty & "', '" & mReQty & "', " & vbCrLf & " '" & mDRQty & "', '" & mInvQty & "', " & vbCrLf & " '" & mReofferNo & "', '" & mDNNo & "', " & vbCrLf & " '" & mInvNo & "', '" & mRate & "', " & vbCrLf & " '" & mAmount & "', '" & mBillNo & "', '" & mStatus & "','" & mRemarks & "','" & mPDIRFlag & "') "


                PubDBCn.Execute(SqlStr)
NextRow:
            Next
        End With
        PubDBCn.CommitTrans()
        InsertPrintDummy = True
        Exit Function
ERR1:
        PubDBCn.RollbackTrans()
        InsertPrintDummy = False
        MsgInformation(Err.Description)
    End Function
    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String


        mSqlStr = " SELECT * " & " FROM Temp_PrintDummyData PRINTDUMMYDATA " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY SUBROW"

        FetchRecordForReport = mSqlStr

    End Function

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = PubReportFolderPath & mRPTName
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
End Class
