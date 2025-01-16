Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class FrmRGPSlip
    Inherits System.Windows.Forms.Form
    Dim RsReqMain As ADODB.Recordset ''Recordset
    Dim RsReqDetail As ADODB.Recordset ''Recordset
    ''Private PvtDBCn As ADODB.Connection

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim xMyMenu As String

    Dim FormActive As Boolean

    Private Const ConRowHeight As Short = 12

    Private Const ColItemCode As Short = 1
    Private Const ColItemName As Short = 2
    Private Const ColItemDesc As Short = 3
    Private Const ColUom As Short = 4
    Private Const ColStockType As Short = 5
    Private Const ColLotNo As Short = 6
    Private Const ColHeatNo As Short = 7
    Private Const ColBatchNo As Short = 8
    Private Const ColStockBal As Short = 9
    Private Const ColF4No As Short = 10
    Private Const ColF4Stock As Short = 11
    Private Const ColQtyKGs As Short = 12
    Private Const ColQty As Short = 13
    Private Const ColRemarks As Short = 14
    Private Const ColIncomingItemCode As Short = 15
    Private Const ColJobOrderNo As Short = 16
    Private Const ColItemDetail As Short = 17

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub cboGatePasstype_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboGatePasstype.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboPurpose_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboPurpose.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboPurpose_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboPurpose.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkPaintF4_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkPaintF4.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click

        On Error GoTo AddErr
        If cmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            SprdMain.Enabled = True
            txtSlipno.Enabled = False
            cmdSearch.Enabled = False

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
    Private Sub FillCboStatus()

        On Error GoTo FillERR
        Dim RsFormType As ADODB.Recordset

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

        cboDivision.Text = GetDefaultDivision()        'cboDivision.SelectedIndex = -1



        '    SqlStr = "Select DISTINCT FORMTYPE From FIN_INTERFACE_MST " & vbCrLf _
        ''            & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND IDENTIFICATION='ST'"
        '
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsFormType, adLockReadOnly
        cboStatus.Items.Clear()
        cboStatus.Items.Add(("Pending"))
        cboStatus.Items.Add(("Completed"))
        cboStatus.Items.Add(("Closed"))

        cboGatePasstype.Items.Clear()
        cboGatePasstype.Items.Add(("RGP"))
        cboGatePasstype.Items.Add(("NRGP"))

        cboGatePasstype.SelectedIndex = 0

        cboPurpose.Items.Clear()
        cboPurpose.Items.Add("A : None")
        cboPurpose.Items.Add("B : Jobwork")
        cboPurpose.Items.Add("C : Repair / Refill / Work Order")
        cboPurpose.Items.Add("D : Tool Trial")
        cboPurpose.Items.Add("E : Preparation of Tool/Die/Jigs/Fixture")
        cboPurpose.Items.Add("F : Testing / Trial")
        cboPurpose.Items.Add("G : Trolley / Bins")
        cboPurpose.Items.Add("H : FOC - Under Warranty / Re-Repair")
        cboPurpose.Items.Add("I : Fitting into any M/c coming to the company")
        cboPurpose.SelectedIndex = -1

        '    If RsFormType.EOF = False Then
        '        Do While Not RsFormType.EOF
        '            If Not IsNull(RsFormType!FORMTYPE) Then
        '                CboFormType.AddItem RsFormType!FORMTYPE
        '            End If
        '            RsFormType.MoveNext
        '        Loop
        '    End If
        Exit Sub
FillERR:
        MsgBox(Err.Description)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDelete.Click
        On Error GoTo DelErrPart

        Dim mItemCode As String

        If ValidateBranchLocking((txtDate.Text)) = True Then
            Exit Sub
        End If

        If ValidateBookLocking(PubDBCn, CInt(ConLockGatePassReq), txtDate.Text) = True Then
            Exit Sub
        End If


        If Trim(txtSlipno.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub

        '    If chkissue.Value = vbChecked Then
        '        MsgInformation "Issue Completed, Cann't be Deleted"
        '        Exit Sub
        '    End If
        If cboStatus.SelectedIndex = 1 Then
            MsgInformation("GatePass already Generated")
            Exit Sub
        End If

        If Not RsReqMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()

                If InsertIntoDelAudit(PubDBCn, "INV_RGP_SLIP_HDR", (txtSlipno.Text), RsReqMain, "AUTO_KEY_RGPSLIP", "D") = False Then GoTo DelErrPart

                If InsertIntoDeleteTrn(PubDBCn, "INV_RGP_SLIP_HDR", "AUTO_KEY_RGPSLIP", (txtSlipno.Text)) = False Then GoTo DelErrPart

                '            If DeleteStockTRN(PubDBCn, txtDate.Text, txtSlipno.Text, txtDate.Text, "ISSUE") = False Then GoTo DelErrPart
                If DeleteOutRGPDetail(PubDBCn, Val(txtSlipno.Text)) = False Then GoTo DelErrPart

                PubDBCn.Execute("Delete from INV_RGP_SLIP_DET Where AUTO_KEY_RGPSLIP=" & Val(txtSlipno.Text) & "")
                PubDBCn.Execute("Delete from INV_RGP_SLIP_HDR Where AUTO_KEY_RGPSLIP=" & Val(txtSlipno.Text) & "")

                PubDBCn.CommitTrans()
                RsReqMain.Requery() ''.Refresh
                RsReqDetail.Requery() ''.Refresh
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans() ''
        RsReqMain.Requery() ''.Refresh
        RsReqDetail.Requery() ''.Refresh
        If Err.Description <> "" Then
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
    End Sub
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdModify.Click

        On Error GoTo ModifyErr
        If PubSuperUser <> "S" Then
            If VB.Left(cboStatus.Text, 1) = "C" Then
                MsgInformation("GatePass Made For this Requistion Note, so Cann't be Modified")
                Exit Sub
            End If
        End If

        If cboGatePasstype.Text = "NRGP" Then
            If chkNRGPApproved.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("NRGP Approved, so Cann't be Modified")
                Exit Sub
            End If
        End If

        If cmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsReqMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
            txtSlipno.Enabled = False
            cmdSearch.Enabled = False
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
        Call ReportonRgp_Nrgp(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportonRgp_Nrgp(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportonRgp_Nrgp(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""
        Dim SqlStr As String = ""
        Dim mRptFileName As String

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        SqlStr = ""

        Call MainClass.ClearCRptFormulas(Report1)


        Call SelectQryForRgp_Nrgp(SqlStr)


        mTitle = "RGP-NRGP Requisition Slip"
        mSubTitle = ""
        mRptFileName = "Rgp_NrgpReq.rpt"

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)



        Exit Sub
ERR1:
        frmPrintRGP_F4.Close()
        MsgInformation(Err.Description)
    End Sub
    Private Function SelectQryForRgp_Nrgp(ByRef mSqlStr As String) As String

        ''SELECT CLAUSE...

        mSqlStr = " SELECT " & vbCrLf & " IH.*, ID.*,INVMST.ITEM_SHORT_DESC, FIN_SUPP_CUST_BUSINESS_MST.*, PREBY.EMP_NAME"

        'mSqlStr = mSqlStr & " CMST.SUPP_CUST_NAME, CMST.SUPP_CUST_ADDR, " & vbCrLf _
        '    & " CMST.SUPP_CUST_CITY, CMST.SUPP_CUST_STATE, " & vbCrLf _
        '    & " CMST.SUPP_CUST_PIN, CMST.SUPP_CUST_PHONE, " & vbCrLf _
        '    & " CMST.SUPP_CUST_FAXNO, CMST.SUPP_CUST_MAILID, " & vbCrLf _
        '    & " CMST.SUPP_CUST_MOBILE, CMST.CST_NO, " & vbCrLf _
        '    & " CMST.LST_NO, PREBY.EMP_NAME"

        ''FROM CLAUSE...
        mSqlStr = mSqlStr & vbCrLf _
            & " FROM INV_RGP_SLIP_HDR IH, INV_RGP_SLIP_DET ID, " & vbCrLf _
            & " FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_BUSINESS_MST, INV_ITEM_MST INVMST,PAY_EMPLOYEE_MST PREBY"

        ''WHERE CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf _
            & " IH.AUTO_KEY_RGPSLIP=ID.AUTO_KEY_RGPSLIP" & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND CMST.COMPANY_CODE=FIN_SUPP_CUST_BUSINESS_MST.COMPANY_CODE" & vbCrLf _
            & " AND CMST.SUPP_CUST_CODE=FIN_SUPP_CUST_BUSINESS_MST.SUPP_CUST_CODE AND FIN_SUPP_CUST_BUSINESS_MST.LOCATION_ID='" & Trim(txtBillTo.Text) & "'" & vbCrLf _
            & " AND IH.COMPANY_CODE=PREBY.COMPANY_CODE(+)" & vbCrLf _
            & " AND IH.AUTH_GIVEN_BY=PREBY.EMP_CODE(+)" & vbCrLf _
            & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND ID.FROM_ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.AUTO_KEY_RGPSLIP=" & Val(txtSlipno.Text) & ""

        ''ORDER CLAUSE...

        mSqlStr = mSqlStr & vbCrLf & "ORDER BY ID.SERIAL_NO"

        SelectQryForRgp_Nrgp = mSqlStr
    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String)

        On Error GoTo ErrPart
        Dim mAmountInword As String
        Dim mPrepareBy As String
        Dim mResponsibleBy As String
        Dim mAuthorityBy As String

        If RsCompany.Fields("COMPANY_CODE").Value = 16 Then
            SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, True, xMyMenu)
        Else
            SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        End If

        MainClass.AssignCRptFormulas(Report1, "PrepareBy=""" & txtPrepareBy.Text & """")
        MainClass.AssignCRptFormulas(Report1, "ResponsibleBy=""" & txtEmpName.Text & """")
        MainClass.AssignCRptFormulas(Report1, "AuthorityBy=""" & txtAuthorityName.Text & """")

        Report1.ReportFileName = PubReportFolderPath & mRptFileName

        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        Report1.Action = 1
        Report1.Reset()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If UpdateMain1() = True Then
            ADDMode = False
            MODIFYMode = False
            txtSlipNo_Validating(txtSlipno, New System.ComponentModel.CancelEventArgs(False))
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

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtSlipno.Text), "INV_RGP_SLIP_HDR", "AUTO_KEY_RGPSLIP", "RGP_SLIP_DATE", , , SqlStr) = True Then
            txtSlipno.Text = AcName
            txtSlipNo_Validating(txtSlipno, New System.ComponentModel.CancelEventArgs(False))
            If txtSlipno.Enabled = True Then txtSlipno.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdDeptSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDeptSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtDept.Text), "PAY_DEPT_MST", "DEPT_CODE", "DEPT_DESC", , , SqlStr) = True Then
            txtDept.Text = AcName
            txtDeptName.Text = AcName1
            '            txtDept_Validate False
            If txtDept.Enabled = True Then txtDept.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub CmdSearchMRR_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchMRR.Click

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mMaxRow As Integer
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mItemUOM As String = ""
        Dim mDivisionCode As Double
        Dim mLotNo As String
        Dim mJobOrderNo As String
        Dim mFactor As Double
        Dim mIssueUOM As String = ""
        Dim mHeatNo As String

        If ADDMode = False Then Exit Sub
        MainClass.ClearGrid(SprdMain)

        If Val(txtMRRSearch.Text) = 0 Then Exit Sub

        If Len(txtMRRSearch.Text) < 6 Then
            txtMRRSearch.Text = Val(txtMRRSearch.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        If Trim(txtSuppcode.Text) = "" Then
            If txtSuppcode.Enabled = True Then txtSuppcode.Focus()
            MsgInformation("Please Select Supplier Code.")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(txtSuppcode.Text), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invalid Supplier Code.")
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

        If UCase(cboGatePasstype.Text) = "NRGP" Then
            cboPurpose.SelectedIndex = 0
            'If cboPurpose.Text = "" Then
            '    If cboPurpose.Enabled = True Then cboPurpose.Focus()
            '    MsgInformation("Please Select Purpose for.")
            '    Exit Sub
            'End If
            'If cboPurpose.SelectedIndex <= 2 Then
            '    If cboPurpose.Enabled = True Then cboPurpose.Focus()
            '    MsgInformation("Cann't be Select None / Jobwork / Work Order Purpose for RGP.")
            '    Exit Sub
            'End If
        Else
            If cboPurpose.Text = "" Then
                If cboPurpose.Enabled = True Then cboPurpose.Focus()
                MsgInformation("Please Select Purpose for.")
                Exit Sub
            End If
            If cboPurpose.SelectedIndex = 0 Then
                If cboPurpose.Enabled = True Then cboPurpose.Focus()
                MsgInformation("Cann't be Select None Purpose for RGP.")
                Exit Sub
            End If
        End If

        SqlStr = " SELECT ID.ITEM_CODE,ID.ITEM_UOM, SUM(APPROVED_QTY) AS APPROVED_QTY  FROM" & vbCrLf _
            & " INV_GATE_HDR IH, INV_GATE_DET ID" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.AUTO_KEY_MRR = ID.AUTO_KEY_MRR " & vbCrLf _
            & " AND IH.AUTO_KEY_MRR=" & Val(txtMRRSearch.Text) & "" & vbCrLf _
            & " GROUP BY ID.ITEM_CODE,ID.ITEM_UOM " & vbCrLf _
            & " ORDER BY ID.ITEM_CODE,ID.ITEM_UOM "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While Not RsTemp.EOF
                mItemCode = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)
                If CheckAlreadyINGrid(mItemCode) = False Then
                    mMaxRow = GetMaxRow()
                    SprdMain.MaxRows = mMaxRow + 1
                    SprdMain.Row = mMaxRow
                    SprdMain.Col = ColItemCode
                    SprdMain.Text = mItemCode
                    mItemUOM = IIf(IsDBNull(RsTemp.Fields("ITEM_UOM").Value), "", RsTemp.Fields("ITEM_UOM").Value)

                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mItemDesc = MasterNo
                    Else
                        mItemDesc = ""
                    End If

                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mIssueUOM = MasterNo
                    End If

                    mFactor = 1
                    If Trim(mItemUOM) <> Trim(mIssueUOM) Then
                        If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "UOM_FACTOR", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mFactor = Val(MasterNo)
                        End If
                    End If

                    SprdMain.Col = ColItemName
                    SprdMain.Text = mItemDesc

                    SprdMain.Col = ColUom
                    SprdMain.Text = mIssueUOM

                    SprdMain.Col = ColStockType
                    SprdMain.Text = "ST"

                    SprdMain.Col = ColHeatNo
                    mHeatNo = Trim(SprdMain.Text)

                    SprdMain.Col = ColStockBal
                    'If Mid(cboPurpose.Text, 1, 1) = "B" And ADDMode = True And Trim(txtDept.Text) <> "STR" Then
                    '    SprdMain.Text = CStr(GetBalanceStockQty(mItemCode, (txtDate.Text), mIssueUOM, Trim(txtDept.Text), "ST", "", ConPH, mDivisionCode))
                    'Else
                    SprdMain.Text = CStr(GetBalanceStockQty(mItemCode, (txtDate.Text), mIssueUOM, "STR", "ST", "", ConWH, mDivisionCode,,,,, mHeatNo))
                    'End If


                    mJobOrderNo = GetJobOrderNo(mItemCode, Trim(txtSuppcode.Text), mDivisionCode)

                    SprdMain.Col = ColJobOrderNo
                    SprdMain.Text = mJobOrderNo

                    SprdMain.Col = ColQty
                    SprdMain.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("APPROVED_QTY").Value), "", RsTemp.Fields("APPROVED_QTY").Value * mFactor), "0.0000")

                End If
                RsTemp.MoveNext()
            Loop
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

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

    Private Sub cmdSupplierSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSupplierSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        'SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"

        'If ADDMode = True Then
        '    SqlStr = SqlStr & "  AND STATUS='O'"
        'End If

        'If MainClass.SearchGridMaster("", "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
        '    txtSuppcode.Text = AcName1
        '    txtSuppName.Text = AcName
        '    txtSuppcode_Validating(txtSuppcode, New System.ComponentModel.CancelEventArgs(False))
        '    If txtSuppcode.Enabled = True Then txtSuppcode.Focus()
        'End If

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & "  AND SUPP_CUST_CODE NOT IN ('" & IIf(IsDBNull(RsCompany.Fields("COMP_AC_CODE").Value), "", RsCompany.Fields("COMP_AC_CODE").Value) & "')"  '' AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.SearchGridMaster("", "FIN_SUPP_CUST_BUSINESS_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", "LOCATION_ID", "SUPP_CUST_ADDR || SUPP_CUST_CITY || SUPP_CUST_STATE", SqlStr) = True Then
            txtSuppName.Text = AcName
            txtSuppcode.Text = AcName1
            txtBillTo.Text = AcName2
            txtAddress.Text = AcName3
            txtSuppcode_Validating(txtSuppcode, New System.ComponentModel.CancelEventArgs(False))
        End If


        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdAuthSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAuthSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" '' AND RGP_AUTH='Y'"

        If MainClass.ValidateWithMasterTable(txtAuthority.Text, "USER_ID", "EMP_NAME", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtAuthority.Text = AcName
            txtAuthorityName.Text = AcName1
            txtAuthority_Validating(txtAuthority, New System.ComponentModel.CancelEventArgs(False))
            If txtAuthority.Enabled = True Then txtAuthority.Focus()
        End If

        'If ADDMode = True Then
        '    SqlStr = SqlStr & " AND (EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE='')"
        'Else
        '    SqlStr = SqlStr & vbCrLf & " AND (EMP_LEAVE_DATE IS NULL OR (EMP_LEAVE_DATE >= TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')))"
        'End If

        'If MainClass.SearchGridMaster((txtAuthority.Text), "PAY_EMPLOYEE_MST", "EMP_CODE", "EMP_NAME", , , SqlStr) = True Then
        '    txtAuthority.Text = AcName
        '    txtAuthorityName.Text = AcName1
        '    txtAuthority_Validating(txtAuthority, New System.ComponentModel.CancelEventArgs(False))
        '    If txtAuthority.Enabled = True Then txtAuthority.Focus()
        'End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub FrmRGPSlip_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub optMaterial_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optMaterial.CheckedChanged
        'If eventSender.Checked Then
        '    Dim Index As Short = optMaterial.GetIndex(eventSender)

        '    MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        'End If
    End Sub

    Private Sub SprdMain_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdMain.ButtonClicked
        Call ShowFormOutDetail(eventArgs.col, eventArgs.row)
    End Sub
    Private Sub ShowFormOutDetail(ByRef pCol As Integer, ByRef pRow As Integer)

        'Dim I As Integer
        'Dim SqlStr As String = ""
        'Dim RsTemp As ADODB.Recordset = Nothing
        Dim mItemCode As String
        'Dim mItemName As String
        Dim mQty As String

        With SprdMain
            .Row = pRow

            .Col = ColItemCode
            mItemCode = .Text

            .Col = ColQty
            mQty = .Text
        End With
        If mItemCode = "" Then Exit Sub
        If Trim(txtDate.Text) = "" Then Exit Sub

        If Trim(mItemCode) = "" Then
            MsgInformation("Please Enter Valid Item Code.")
            Exit Sub
        End If

        If CDbl(Trim(mQty)) = 0 Then
            MsgInformation("Please Enter Item Qty.")
            Exit Sub
        End If
        ConRGPSlipDetail = False
        'Me.lblDetail.Text = "False"

        With FrmRGPOutDetail
            .LblAddMode.Text = CStr(ADDMode)
            .LblModifyMode.Text = CStr(MODIFYMode)
            .lblItemCode.Text = mItemCode
            .lblMainActiveRow.Text = CStr(pRow)
            .lblOutQty.Text = VB6.Format(mQty, "0.0000")
            .lblRGPDate.Text = VB6.Format(txtDate.Text, "DD/MM/YYYY")
            .ShowDialog()
        End With

        If ConRGPSlipDetail = True Then
            MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
            FrmRGPOutDetail.Close()
        End If

    End Sub
    Private Function GetFinishedJobCode(ByRef xItemCode As String) As String

        On Error GoTo ErrPart
        Dim SqlStr1 As String
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mFCode As String

        GetFinishedJobCode = "'" & Trim(xItemCode) & "'"
        SqlStr1 = "SELECT DISTINCT IH.PRODUCT_CODE "


        SqlStr = SqlStr1 & vbCrLf & " FROM  " & vbCrLf & " PRD_OUTBOM_HDR IH,PRD_OUTBOM_DET ID " & vbCrLf & " WHERE IH.MKEY = ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ID.ITEM_CODE='" & xItemCode & "'"

        SqlStr = SqlStr & vbCrLf & " UNION " & SqlStr1 & vbCrLf & " FROM  " & vbCrLf & " PRD_OUTBOM_HDR IH,PRD_OUTBOM_ALTER_DET ID " & vbCrLf & " WHERE IH.MKEY = ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ID.ALTER_ITEM_CODE='" & xItemCode & "'"

        SqlStr = SqlStr & vbCrLf & " ORDER BY 1 "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mFCode = "'" & Trim(IIf(IsDBNull(RsTemp.Fields("PRODUCT_CODE").Value), "", RsTemp.Fields("PRODUCT_CODE").Value)) & "'"
                If GetFinishedJobCode = "" Then
                    GetFinishedJobCode = mFCode
                Else
                    GetFinishedJobCode = GetFinishedJobCode & "," & mFCode
                End If
                RsTemp.MoveNext()
            Loop
        End If

        Exit Function
ErrPart:
        GetFinishedJobCode = ""
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function GetJobOrderNo(ByRef mItemCode As String, ByRef xSuppCode As String, ByRef mDivisionCode As Double) As String

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mFCode As String
        Dim mFinishedJobCode As String

        GetJobOrderNo = ""
        SqlStr = " SELECT DISTINCT IH.AUTO_KEY_PO  As AUTO_KEY_PO , IH.PUR_ORD_DATE, ID.PO_WEF_DATE,"

        If cboPurpose.SelectedIndex = 1 Then
            SqlStr = SqlStr & vbCrLf & " ID.ITEM_CODE "
        Else
            SqlStr = SqlStr & vbCrLf & " ID.ITEM_CODE, WO_DESCRIPTION "
        End If

        SqlStr = SqlStr & vbCrLf & " FROM PUR_PURCHASE_HDR IH,PUR_PURCHASE_DET ID" & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If cboPurpose.SelectedIndex = 1 Then
            If mItemCode <> "" Then
                mFinishedJobCode = GetFinishedJobCode(mItemCode)
                SqlStr = SqlStr & vbCrLf & " AND ID.ITEM_CODE IN (" & mFinishedJobCode & ",'" & mItemCode & "')"
            End If
            SqlStr = SqlStr & vbCrLf & " AND PUR_TYPE='J'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND ID.ITEM_CODE = '" & mItemCode & "'"
            SqlStr = SqlStr & vbCrLf & " AND PUR_TYPE='W'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND SUPP_CUST_CODE='" & xSuppCode & "'"
        SqlStr = SqlStr & vbCrLf & " AND DIV_CODE=" & mDivisionCode & ""


        If IsDate(txtDate.Text) Then
            SqlStr = SqlStr & vbCrLf & " AND ID.PO_WEF_DATE<=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        If ADDMode = True Then
            SqlStr = SqlStr & vbCrLf & " AND IH.PO_STATUS='Y' AND IH.PO_CLOSED='N' AND ID.PO_ITEM_STATUS='N'"
        End If

        If PubGSTApplicable = True Then
            SqlStr = SqlStr & vbCrLf & " AND ID.PO_WEF_DATE>=TO_DATE('" & VB6.Format(PubGSTApplicableDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY TO_NUMBER(IH.AUTO_KEY_PO),IH.PUR_ORD_DATE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetJobOrderNo = IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_PO").Value), "", RsTemp.Fields("AUTO_KEY_PO").Value)
        End If

        Exit Function
ErrPart:
        GetJobOrderNo = ""
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function GetInItemCode(ByRef xItemCode As String) As String

        On Error GoTo ErrPart
        Dim SqlStr1 As String
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mFCode As String

        '    SqlStr = ""

        GetInItemCode = "'" & Trim(xItemCode) & "'"
        SqlStr = "SELECT DISTINCT IN_ITEM_CODE " & vbCrLf & " FROM TEMP_RGP_OUT_DET" & vbCrLf & " WHERE UserID ='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf & " AND COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ITEM_CODE = '" & xItemCode & "'"

        SqlStr = SqlStr & vbCrLf & " ORDER BY 1 "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mFCode = "'" & Trim(IIf(IsDBNull(RsTemp.Fields("IN_ITEM_CODE").Value), "", RsTemp.Fields("IN_ITEM_CODE").Value)) & "'"
                If GetInItemCode = "" Then
                    GetInItemCode = mFCode
                Else
                    GetInItemCode = GetInItemCode & "," & mFCode
                End If
                RsTemp.MoveNext()
            Loop
        End If

        Exit Function
ErrPart:
        GetInItemCode = ""
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim xIName As String
        Dim SqlStr As String = ""
        Dim xIncomingItemCode As String
        Dim xICode As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim pMainItemCode As String
        Dim mBookType As String
        Dim mGatepassNo As Double
        Dim mInConUnit As Double
        Dim mOutConUnit As Double
        Dim mIsManyIn As Boolean
        Dim mLotNo As String
        Dim mUOM As String = ""
        Dim mStockType As String = ""
        Dim mItemCode As String
        Dim xSuppCode As String
        Dim mDivisionCode As Double
        Dim xPoNo As String
        Dim mFinishedJobCode As String

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        If eventArgs.row = 0 And eventArgs.col = ColItemCode Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode

                If VB.Left(cboPurpose.Text, 1) = "B" Then

                    SqlStr = " SELECT DISTINCT ID.OUTWARD_ITEM_CODE ITEM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, IH.AUTO_KEY_PO  As AUTO_KEY_PO , IH.PUR_ORD_DATE, ID.PO_WEF_DATE"

                    SqlStr = SqlStr & vbCrLf _
                        & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID, INV_ITEM_MST INVMST" & vbCrLf _
                        & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
                        & " AND IH.Company_Code=INVMST.Company_Code" & vbCrLf _
                        & " AND ID.OUTWARD_ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf _
                        & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & ""

                    SqlStr = SqlStr & vbCrLf & " AND IH.PUR_TYPE='J'"

                    If Trim(txtSuppName.Text) <> "" Then
                        If MainClass.ValidateWithMasterTable(txtSuppName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE NOT IN ('" & IIf(IsDBNull(RsCompany.Fields("COMP_AC_CODE").Value), "", RsCompany.Fields("COMP_AC_CODE").Value) & "')") = True Then
                            xSuppCode = MasterNo
                            SqlStr = SqlStr & vbCrLf & " AND IH.SUPP_CUST_CODE='" & xSuppCode & "'"
                        End If
                    End If

                    SqlStr = SqlStr & vbCrLf & " AND IH.BILL_TO_LOC_ID='" & txtBillTo.Text & "'"

                    SqlStr = SqlStr & vbCrLf & " AND IH.DIV_CODE=" & mDivisionCode & ""

                    If IsDate(txtDate.Text) Then
                        SqlStr = SqlStr & vbCrLf & " AND ID.PO_WEF_DATE<=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                    End If

                    If ADDMode = True Then
                        SqlStr = SqlStr & vbCrLf & " AND IH.PO_STATUS='Y' AND IH.PO_CLOSED='N'" '' AND ID.PO_ITEM_STATUS='N'"
                    End If

                    'SqlStr = SqlStr & vbCrLf & " ORDER BY TO_NUMBER(IH.AUTO_KEY_PO),IH.PUR_ORD_DATE"

                    If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
                        .Row = .ActiveRow
                        .Col = ColItemCode
                        .Text = Trim(AcName)

                        .Col = ColJobOrderNo
                        .Text = AcName3
                    End If

                Else
                    If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", "CUSTOMER_PART_NO", , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS='A'") = True Then
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

                If VB.Left(cboPurpose.Text, 1) = "B" Then

                    SqlStr = " SELECT DISTINCT INVMST.ITEM_SHORT_DESC, ID.OUTWARD_ITEM_CODE ITEM_CODE, INVMST.CUSTOMER_PART_NO, IH.AUTO_KEY_PO  As AUTO_KEY_PO , IH.PUR_ORD_DATE, ID.PO_WEF_DATE"

                    SqlStr = SqlStr & vbCrLf _
                        & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID, INV_ITEM_MST INVMST" & vbCrLf _
                        & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
                        & " AND IH.Company_Code=INVMST.Company_Code" & vbCrLf _
                        & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf _
                        & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & ""

                    SqlStr = SqlStr & vbCrLf & " AND IH.PUR_TYPE='J'"

                    If Trim(txtSuppName.Text) <> "" Then
                        If MainClass.ValidateWithMasterTable(txtSuppName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE NOT IN ('" & IIf(IsDBNull(RsCompany.Fields("COMP_AC_CODE").Value), "", RsCompany.Fields("COMP_AC_CODE").Value) & "')") = True Then
                            xSuppCode = MasterNo
                            SqlStr = SqlStr & vbCrLf & " AND IH.SUPP_CUST_CODE='" & xSuppCode & "'"
                        End If
                    End If

                    SqlStr = SqlStr & vbCrLf & " AND IH.BILL_TO_LOC_ID='" & txtBillTo.Text & "'"

                    SqlStr = SqlStr & vbCrLf & " AND IH.DIV_CODE=" & mDivisionCode & ""

                    If IsDate(txtDate.Text) Then
                        SqlStr = SqlStr & vbCrLf & " AND ID.PO_WEF_DATE<=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                    End If

                    If ADDMode = True Then
                        SqlStr = SqlStr & vbCrLf & " AND IH.PO_STATUS='Y' AND IH.PO_CLOSED='N' AND ID.PO_ITEM_STATUS='N'"
                    End If

                    'SqlStr = SqlStr & vbCrLf & " ORDER BY TO_NUMBER(IH.AUTO_KEY_PO),IH.PUR_ORD_DATE"

                    If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
                        .Row = .ActiveRow
                        .Col = ColItemCode
                        .Text = Trim(AcName1)

                        .Col = ColJobOrderNo
                        .Text = AcName3
                    End If

                Else
                    If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
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

        If eventArgs.row = 0 And eventArgs.col = ColIncomingItemCode And VB.Left(cboPurpose.Text, 1) = "B" Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                If mItemCode = "" Then Exit Sub

                .Col = ColIncomingItemCode
                xIncomingItemCode = .Text

                SqlStr = " SELECT DISTINCT ID.ITEM_CODE, INVMST.ITEM_SHORT_DESC, IH.AUTO_KEY_PO  As AUTO_KEY_PO , IH.PUR_ORD_DATE, ID.PO_WEF_DATE"

                SqlStr = SqlStr & vbCrLf _
                    & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID, INV_ITEM_MST INVMST" & vbCrLf _
                    & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
                    & " AND IH.Company_Code=INVMST.Company_Code" & vbCrLf _
                    & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf _
                    & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & ""

                SqlStr = SqlStr & vbCrLf & " AND ID.OUTWARD_ITEM_CODE = '" & MainClass.AllowSingleQuote(mItemCode) & "'"

                SqlStr = SqlStr & vbCrLf & " AND IH.PUR_TYPE='J'"

                If Trim(txtSuppName.Text) <> "" Then
                    If MainClass.ValidateWithMasterTable(txtSuppName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE NOT IN ('" & IIf(IsDBNull(RsCompany.Fields("COMP_AC_CODE").Value), "", RsCompany.Fields("COMP_AC_CODE").Value) & "')") = True Then
                        xSuppCode = MasterNo
                        SqlStr = SqlStr & vbCrLf & " AND IH.SUPP_CUST_CODE='" & xSuppCode & "'"
                    End If
                End If

                SqlStr = SqlStr & vbCrLf & " AND IH.BILL_TO_LOC_ID='" & txtBillTo.Text & "'"

                SqlStr = SqlStr & vbCrLf & " AND IH.DIV_CODE=" & mDivisionCode & ""


                If IsDate(txtDate.Text) Then
                    SqlStr = SqlStr & vbCrLf & " AND ID.PO_WEF_DATE<=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                End If

                If ADDMode = True Then
                    SqlStr = SqlStr & vbCrLf & " AND IH.PO_STATUS='Y' AND IH.PO_CLOSED='N' AND ID.PO_ITEM_STATUS='N'"
                End If

                'SqlStr = SqlStr & vbCrLf & " ORDER BY TO_NUMBER(IH.AUTO_KEY_PO),IH.PUR_ORD_DATE"

                If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColIncomingItemCode
                    .Text = Trim(AcName)

                    .Col = ColJobOrderNo
                    .Text = AcName2
                End If

                .Row = .ActiveRow
                .Col = ColIncomingItemCode
                xIncomingItemCode = Trim(.Text)

                If xIncomingItemCode <> "" Then
                    If MainClass.ValidateWithMasterTable(xIncomingItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColIncomingItemCode)
                    End If
                End If
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColJobOrderNo Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColJobOrderNo
                xPoNo = Trim(SprdMain.Text)


                SqlStr = " SELECT DISTINCT IH.AUTO_KEY_PO  As AUTO_KEY_PO , IH.PUR_ORD_DATE, ID.PO_WEF_DATE,"

                If cboPurpose.SelectedIndex = 1 Then
                    SqlStr = SqlStr & vbCrLf & " ID.ITEM_CODE,  INVMST.ITEM_SHORT_DESC "
                Else
                    SqlStr = SqlStr & vbCrLf & " ID.ITEM_CODE,  INVMST.ITEM_SHORT_DESC ,WO_DESCRIPTION "
                End If

                SqlStr = SqlStr & vbCrLf & " FROM PUR_PURCHASE_HDR IH,PUR_PURCHASE_DET ID"

                If cboPurpose.SelectedIndex = 1 Then
                    SqlStr = SqlStr & vbCrLf & " , INV_ITEM_MST INVMST"
                Else
                    SqlStr = SqlStr & vbCrLf & " , INV_ITEM_MST INVMST"
                End If

                SqlStr = SqlStr & vbCrLf _
                    & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
                    & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & ""

                If cboPurpose.SelectedIndex = 1 Then
                    SqlStr = SqlStr & vbCrLf _
                            & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf _
                            & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & ""

                    If mItemCode <> "" Then
                        SqlStr = SqlStr & vbCrLf & " AND ID.OUTWARD_ITEM_CODE = '" & MainClass.AllowSingleQuote(mItemCode) & "'"
                    End If
                    SqlStr = SqlStr & vbCrLf & " AND PUR_TYPE='J'"
                Else

                    If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
                        SqlStr = SqlStr & vbCrLf _
                            & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf _
                            & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & ""

                        If mItemCode <> "" Then
                            SqlStr = SqlStr & vbCrLf & " AND ID.ITEM_CODE = '" & MainClass.AllowSingleQuote(mItemCode) & "'"
                        End If

                    Else
                        SqlStr = SqlStr & vbCrLf _
                            & " AND ID.ITEM_CODE=INVMST.ITEM_CODE(+)" & vbCrLf _
                            & " AND IH.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & ""
                    End If



                    SqlStr = SqlStr & vbCrLf & " AND PUR_TYPE='W'"
                End If

                If Trim(txtSuppName.Text) <> "" Then
                    If MainClass.ValidateWithMasterTable(txtSuppName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE NOT IN ('" & IIf(IsDBNull(RsCompany.Fields("COMP_AC_CODE").Value), "", RsCompany.Fields("COMP_AC_CODE").Value) & "')") = True Then
                        xSuppCode = MasterNo
                        SqlStr = SqlStr & vbCrLf & " AND SUPP_CUST_CODE='" & xSuppCode & "'"
                    End If
                End If


                SqlStr = SqlStr & vbCrLf & " AND BILL_TO_LOC_ID='" & txtBillTo.Text & "'"
                SqlStr = SqlStr & vbCrLf & " AND DIV_CODE=" & mDivisionCode & ""


                If IsDate(txtDate.Text) Then
                    SqlStr = SqlStr & vbCrLf & " AND ID.PO_WEF_DATE<=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                End If

                If PubGSTApplicable = True Then
                    SqlStr = SqlStr & vbCrLf & " AND ID.PO_WEF_DATE>=TO_DATE('" & VB6.Format(PubGSTApplicableDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
                End If

                If ADDMode = True Then
                    SqlStr = SqlStr & vbCrLf & " AND IH.PO_STATUS='Y' AND IH.PO_CLOSED='N' AND ID.PO_ITEM_STATUS='N'"
                End If

                If Val(xPoNo) > 0 Then
                    SqlStr = SqlStr & vbCrLf & " AND IH.AUTO_KEY_PO Like '" & xPoNo & "%'"
                End If

                'SqlStr = SqlStr & vbCrLf & " ORDER BY TO_NUMBER(IH.AUTO_KEY_PO),IH.PUR_ORD_DATE"

                If MainClass.SearchGridMasterBySQL2(xPoNo, SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColJobOrderNo
                    .Text = AcName

                    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColJobOrderNo)
                End If
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColLotNo Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode
                xICode = Trim(.Text)

                .Col = ColLotNo
                mLotNo = Trim(.Text)


                .Col = ColUom
                mUOM = Trim(.Text)

                .Col = ColStockType
                mStockType = Trim(.Text)

                .Col = ColLotNo
                SqlStr = GetItemLotWiseQry(xICode, (txtDate.Text), mUOM, "STR", mStockType, mLotNo, ConWH)
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColLotNo
                    .Text = Trim(AcName1)
                End If

                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColLotNo)
            End With
        End If
        If eventArgs.row = 0 And eventArgs.col = ColBatchNo Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode
                xICode = Trim(.Text)

                .Col = ColBatchNo
                mLotNo = Trim(.Text)

                .Col = ColUom
                mUOM = Trim(.Text)

                .Col = ColStockType
                mStockType = Trim(.Text)

                .Col = ColBatchNo
                SqlStr = GetItemLotWiseQry(xICode, (txtDate.Text), mUOM, "STR", mStockType, mLotNo, ConWH)
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColBatchNo
                    .Text = Trim(AcName1)
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColBatchNo)
            End With
        End If
        If eventArgs.row = 0 And eventArgs.col = ColHeatNo Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode
                xICode = Trim(.Text)

                .Col = ColHeatNo
                mLotNo = Trim(.Text)

                .Col = ColUom
                mUOM = Trim(.Text)

                .Col = ColStockType
                mStockType = Trim(.Text)

                .Col = ColHeatNo
                SqlStr = GetItemHeatWiseQry(xICode, (txtDate.Text), mUOM, "STR", mStockType, mLotNo, ConWH)
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColHeatNo
                    .Text = Trim(AcName1)
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColHeatNo)
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColStockType Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColStockType
                If MainClass.SearchGridMaster(.Text, "INV_TYPE_MST", "STOCK_TYPE_CODE", "STOCK_TYPE_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow
                    .Col = ColStockType
                    .Text = AcName
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColStockType)
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColF4No Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode
                xICode = Trim(.Text)
                If xICode = "" Then Exit Sub
                mBookType = IIf(chkPaintF4.CheckState = System.Windows.Forms.CheckState.Checked, "G", "P")

                If chkPaintF4.CheckState = System.Windows.Forms.CheckState.Checked Then
                    pMainItemCode = "('" & xICode & "')"
                    mIsManyIn = False
                Else
                    pMainItemCode = GetInJobworkItem(xICode, Trim(txtDate.Text), mInConUnit, mIsManyIn)

                    If pMainItemCode = "" Then
                        pMainItemCode = "('" & xICode & "')"
                    Else
                        pMainItemCode = "('" & xICode & "'," & pMainItemCode & ")"
                    End If

                    mOutConUnit = 1
                End If

                If mIsManyIn = False Then
                    SqlStr = " SELECT PARTY_F4NO, TO_CHAR(SUM(DECODE(ITEM_IO,'I',1,-1)*ITEM_QTY)) AS BALQTY " & vbCrLf & " FROM DSP_PAINT57F4_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND (ITEM_CODE IN " & pMainItemCode & " OR SUB_ITEM_CODE IN " & pMainItemCode & ")" & vbCrLf & " AND SUPP_CUST_CODE='" & Trim(txtSuppcode.Text) & "' AND BOOKTYPE<>'" & mBookType & "' " & vbCrLf & " AND ISSCRAP='N'"

                    If Val(txtSlipno.Text) <> 0 Then
                        If MainClass.ValidateWithMasterTable(txtSlipno.Text, "REQ_NO", "AUTO_KEY_PASSNO", "INV_GATEPASS_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            mGatepassNo = MasterNo
                        End If
                    End If

                    If Val(CStr(mGatepassNo)) <> 0 Then
                        SqlStr = SqlStr & vbCrLf & " AND BILL_NO<>'" & mGatepassNo & "'"
                    End If

                    SqlStr = SqlStr & vbCrLf & " GROUP BY PARTY_F4NO " & vbCrLf & " HAVING SUM(DECODE(ITEM_IO,'I',1,-1)*ITEM_QTY)>0"

                    .Col = ColF4No
                    If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                        .Row = .ActiveRow
                        .Col = ColF4No
                        .Text = AcName
                    End If
                End If
            End With
        End If

        If eventArgs.col = 0 And eventArgs.row > 0 Then '***ROW DEL. OPTION NOT REQ IN INVOICE
            SprdMain.Row = eventArgs.row
            SprdMain.Col = ColItemCode
            If CheckOutDetailExists() = False Then
                If eventArgs.row < SprdMain.MaxRows And (ADDMode = True Or MODIFYMode = True) Then
                    MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColItemCode)
                    MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
                    FormatSprdMain(eventArgs.row)
                End If
            End If
        End If

    End Sub

    Private Function GetSearchItem(ByRef mByCode As String) As String
        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim xSuppCode As String

        xSuppCode = IIf(Trim(txtSuppcode.Text) = "", "-1", Trim(txtSuppcode.Text))

        If mByCode = "Y" Then
            mSqlStr = "SELECT B.ITEM_CODE,A.ITEM_SHORT_DESC "
        Else
            mSqlStr = "SELECT A.ITEM_SHORT_DESC,B.ITEM_CODE "
        End If

        mSqlStr = mSqlStr & vbCrLf & " FROM INV_ITEM_MST A, FIN_SUPP_CUST_DET B" & vbCrLf & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf & " AND A.ITEM_CODE=B.ITEM_CODE " & vbCrLf & " AND B.SUPP_CUST_CODE='" & xSuppCode & "'"
        GetSearchItem = mSqlStr
        Exit Function
ErrPart:
        GetSearchItem = ""

    End Function

    Private Sub SprdMain_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdMain.KeyDownEvent

        Dim mActiveCol As Integer

        mActiveCol = SprdMain.ActiveCol

        If eventArgs.keyCode = System.Windows.Forms.Keys.Return Or eventArgs.keyCode = System.Windows.Forms.Keys.Tab Then
            If mActiveCol = ColQty Then
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColQty
                If Val(SprdMain.Text) <> 0 Then
                    If SprdMain.MaxRows = SprdMain.ActiveRow Then
                        MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                        '                    FormatSprdMain SprdMain.MaxRows
                    End If
                End If
                '            SprdMain.Row = SprdMain.MaxRows
            End If
        ElseIf eventArgs.keyCode = System.Windows.Forms.Keys.F1 Then
            If mActiveCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
            If mActiveCol = ColItemName Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemName, 0))
            If mActiveCol = ColJobOrderNo Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColJobOrderNo, 0))
        End If
        '    KeyCode = 9999
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim mBalQty As Double
        Dim mQtyKgs As Double
        Dim mItemWeight As Double
        Dim mIssueQty As Double
        Dim mItemCode As String
        Dim mIncomingItemCode As String = ""
        Dim mLotNo As String
        Dim mItemUOM As String = ""
        Dim mStkType As String
        Dim xICode As String
        Dim xF4No As String
        Dim pMainItemCode As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim xF4Stock As Double
        Dim xQty As Double
        Dim mInConUnit As Double
        Dim mOutConUnit As Double
        Dim mIsManyIn As Boolean
        Dim mDivisionCode As Double
        Dim xPoNo As String
        Dim mTableName As String
        Dim mHeatNo As String

        If eventArgs.newRow = -1 Then Exit Sub

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        SprdMain.Row = eventArgs.row '' SprdMain.ActiveRow

        Select Case eventArgs.col
            Case ColItemCode
                SprdMain.Col = ColItemCode
                SprdMain.Text = Trim(SprdMain.Text)
                If Trim(SprdMain.Text) = "" Then Exit Sub

                If FillItemDescFromItemCode(Trim(SprdMain.Text)) = True Then
                    If DuplicateItem(ColItemCode, eventArgs.row) = True Then
                        eventArgs.cancel = True
                        Exit Sub
                    Else
                        FormatSprdMain(-1)
                        'MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColStockType)

                        '                MainClass.SetFocusToCell SprdMain, SprdMain.ActiveRow, ColStockType
                    End If

                Else
                    MsgInformation("Please Check Item Code")
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColItemCode)
                    eventArgs.cancel = True
                    Exit Sub
                End If
                '        Case ColItemCode
                '            SprdMain.Row = SprdMain.ActiveRow
                '
                '            SprdMain.Col = ColItemCode
                '            xICode = SprdMain.Text
                '            If xICode = "" Then Exit Sub
                '
                '            If GetValidItem(xICode) = True Then
                '                If CheckDuplicateItem(xICode) = False Then
                '                    If FillGridRow(xICode) = False Then Exit Sub
                ''                    FormatSprdMain Row
                '    '                MainClass.SetFocusToCell SprdMain, Row, ColItemRate
                '                End If
                '            Else
                '                MainClass.SetFocusToCell SprdMain, Row, ColItemCode
                '            End If

                '        Case ColItemName
                '            SprdMain.Col = ColItemCode
                '            Call FillItemDescFromItemDesc(SprdMain.Text)
                '            If DuplicateItem = False Then
                '            End If
            Case ColQtyKGs
                '            If RsCompany!StockBalCheck = "Y" Then
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColItemCode
                mItemCode = Trim(SprdMain.Text)

                mItemWeight = 0
                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_WEIGHT", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mItemWeight = Val(MasterNo)
                End If

                SprdMain.Col = ColQtyKGs
                mQtyKgs = Val(SprdMain.Text)

                SprdMain.Col = ColQty
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

                SprdMain.Col = ColQtyKGs
                SprdMain.Text = mQtyKgs

                SprdMain.Col = ColQty
                SprdMain.Text = xQty

                'If RsCompany.Fields("STOCKBALCHECK").Value = "Y" Then
                '    If xQty > mBalQty Then
                '        MsgInformation("You have not enough Stock.")
                '        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColQty)
                '        eventArgs.cancel = True
                '        Exit Sub
                '    End If

                '    If CDbl(xF4No) <> 0 Then
                '        If xQty > xF4Stock Then
                '            MsgInformation("You have not enough F4 Stock.")
                '            MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColQty)
                '            eventArgs.cancel = True
                '            Exit Sub
                '        End If
                '    End If
                'End If
                'MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                'FormatSprdMain(-1)
                '             End If
            Case ColQty
                '            If RsCompany!StockBalCheck = "Y" Then
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColItemCode
                mItemCode = Trim(SprdMain.Text)

                mItemWeight = 0
                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_WEIGHT", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mItemWeight = Val(MasterNo)
                End If

                SprdMain.Col = ColStockType
                mStkType = Trim(SprdMain.Text)

                SprdMain.Col = ColStockBal
                mBalQty = Val(SprdMain.Text)

                SprdMain.Col = ColF4No
                xF4No = CStr(Val(SprdMain.Text))

                SprdMain.Col = ColF4Stock
                xF4Stock = Val(SprdMain.Text)

                SprdMain.Col = ColQtyKGs
                mQtyKgs = Val(SprdMain.Text)

                SprdMain.Col = ColQty
                xQty = Val(SprdMain.Text)


                'If mQtyKgs <> 0 And xQty = 0 Then
                '    If mItemWeight > 0 Then
                '        xQty = Int(mQtyKgs * 1000 / mItemWeight)
                '    End If
                'End If

                'If mQtyKgs = 0 And xQty <> 0 Then
                '    If mItemWeight > 0 Then
                mQtyKgs = VB6.Format(xQty * mItemWeight / 1000, "0.00")
                '    End If
                'End If

                SprdMain.Col = ColQtyKGs
                SprdMain.Text = mQtyKgs

                'SprdMain.Col = ColQty
                'SprdMain.Text = xQty

                If RsCompany.Fields("STOCKBALCHECK").Value = "Y" Then
                    If xQty > mBalQty Then
                        MsgInformation("You have not enough Stock.")
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColQty)
                        eventArgs.cancel = True
                        Exit Sub
                    End If

                    If CDbl(xF4No) <> 0 Then
                        If xQty > xF4Stock Then
                            MsgInformation("You have not enough F4 Stock.")
                            MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColQty)
                            eventArgs.cancel = True
                            Exit Sub
                        End If
                    End If
                End If
                MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                FormatSprdMain(-1)
                '             End If
            Case ColIncomingItemCode

                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColIncomingItemCode
                mIncomingItemCode = Trim(SprdMain.Text)

                If mIncomingItemCode <> "" Then
                    If MainClass.ValidateWithMasterTable(mIncomingItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                        MsgInformation("Invalid Incoming Item Code.")
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColIncomingItemCode)
                        eventArgs.cancel = True
                        Exit Sub
                    End If
                End If
                If DuplicateItem(ColIncomingItemCode, eventArgs.row) = True Then
                    eventArgs.cancel = True
                    Exit Sub
                End If
            Case ColLotNo, ColHeatNo
                If DuplicateItem(ColLotNo, eventArgs.row) = False Then
                    SprdMain.Row = SprdMain.ActiveRow

                    SprdMain.Col = ColItemCode
                    mItemCode = Trim(SprdMain.Text)

                    SprdMain.Col = ColUom
                    mItemUOM = Trim(SprdMain.Text)

                    SprdMain.Col = ColLotNo
                    mLotNo = Trim(SprdMain.Text)

                    SprdMain.Col = ColHeatNo
                    mHeatNo = Trim(SprdMain.Text)

                    If mLotNo <> "" Then
                        mTableName = ConInventoryTable
                        If MainClass.ValidateWithMasterTable(mLotNo, "BATCH_NO", "BATCH_NO", mTableName, PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND ITEM_CODE='" & mItemCode & "'") = False Then
                            MsgInformation("Invalid Lot No")
                            MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColLotNo)
                            Exit Sub
                        End If
                    End If

                    SprdMain.Col = ColStockType
                    mStkType = Trim(SprdMain.Text)
                    If mStkType = "" Then Exit Sub

                    SprdMain.Col = ColStockBal
                    'If Mid(cboPurpose.Text, 1, 1) = "B" And ADDMode = True And Trim(txtDept.Text) <> "STR" Then
                    '    SprdMain.Text = CStr(GetBalanceStockQty(mItemCode, (txtDate.Text), mItemUOM, Trim(txtDept.Text), mStkType, mLotNo, ConPH, mDivisionCode))
                    'Else
                    SprdMain.Text = CStr(GetBalanceStockQty(mItemCode, (txtDate.Text), mItemUOM, "STR", mStkType, mLotNo, ConWH, mDivisionCode,,,,, mHeatNo))
                    'End If
                Else
                    eventArgs.cancel = True
                    Exit Sub
                End If
            Case ColStockType
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColItemCode
                mItemCode = Trim(SprdMain.Text)

                SprdMain.Col = ColUom
                mItemUOM = Trim(SprdMain.Text)

                SprdMain.Col = ColLotNo
                mLotNo = Trim(SprdMain.Text)

                SprdMain.Col = ColHeatNo
                mHeatNo = Trim(SprdMain.Text)

                SprdMain.Col = ColStockType
                mStkType = Trim(SprdMain.Text)
                If mStkType = "" Then Exit Sub

                If MainClass.ValidateWithMasterTable(SprdMain.Text, "STOCK_TYPE_CODE", "STOCK_TYPE_DESC", "INV_TYPE_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                    MsgInformation("InValid Stock Type")
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColStockType)
                    eventArgs.cancel = True
                    Exit Sub
                Else
                    SprdMain.Col = ColStockBal
                    'If Mid(cboPurpose.Text, 1, 1) = "B" And ADDMode = True And Trim(txtDept.Text) <> "STR" Then
                    '    SprdMain.Text = CStr(GetBalanceStockQty(mItemCode, (txtDate.Text), mItemUOM, Trim(txtDept.Text), mStkType, mLotNo, ConPH, mDivisionCode))
                    'Else
                    SprdMain.Text = CStr(GetBalanceStockQty(mItemCode, (txtDate.Text), mItemUOM, "STR", mStkType, mLotNo, ConWH, mDivisionCode,,,,, mHeatNo))
                    'End If

                End If
            Case ColF4No

                SprdMain.Col = ColItemCode
                xICode = SprdMain.Text
                If xICode = "" Then Exit Sub

                SprdMain.Col = ColF4No
                xF4No = Trim(SprdMain.Text)
                If xF4No = "" Then Exit Sub

                pMainItemCode = GetInJobworkItem(xICode, Trim(txtDate.Text), mInConUnit, mIsManyIn)

                If pMainItemCode = "" Then
                    pMainItemCode = "('" & xICode & "')"
                Else
                    pMainItemCode = "('" & xICode & "'," & pMainItemCode & ")"
                End If

                mOutConUnit = 1

                If mIsManyIn = False Then
                    If FillREFDetail(pMainItemCode, xF4No, (SprdMain.ActiveRow), True) = False Then Exit Sub
                End If

            Case ColJobOrderNo
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColJobOrderNo
                xPoNo = Trim(SprdMain.Text)

                If VB.Left(cboPurpose.Text, 1) = "B" Then
                    If MainClass.ValidateWithMasterTable(xPoNo, "AUTO_KEY_PO", "PUR_ORD_DATE", "PUR_PURCHASE_HDR", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DIV_CODE=" & mDivisionCode & " AND PUR_TYPE ='J' AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtSuppcode.Text) & "' AND PO_STATUS='Y' AND PO_CLOSED='N' AND AMEND_WEF_DATE<=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')") = False Then
                        If xPoNo <> "" Then
                            MsgInformation("Invalid Ref No for Such Supplier")
                            MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColJobOrderNo)
                            eventArgs.cancel = True
                        End If
                    End If
                End If

                If VB.Left(cboPurpose.Text, 1) = "C" Then
                    If MainClass.ValidateWithMasterTable(xPoNo, "AUTO_KEY_PO", "PUR_ORD_DATE", "PUR_PURCHASE_HDR", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DIV_CODE=" & mDivisionCode & " AND PUR_TYPE= 'W' AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtSuppcode.Text) & "' AND PO_STATUS='Y' AND PO_CLOSED='N' AND AMEND_WEF_DATE<=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')") = False Then
                        If xPoNo <> "" Then
                            MsgInformation("Invalid Ref No for Such Supplier")
                            MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColJobOrderNo)
                            eventArgs.cancel = True
                        End If
                    End If
                End If

        End Select
        '    FormatSprdMain -1
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Function FillREFDetail(ByRef pItemCode As String, ByRef pRefNo As String, ByRef mRow As Integer, ByRef Cancel As Boolean) As Boolean

        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mGatepassNo As Double

        If Val(pRefNo) = 0 Then Exit Function

        If Val(txtSlipno.Text) <> 0 Then
            If MainClass.ValidateWithMasterTable((txtSlipno.Text), "REQ_NO", "AUTO_KEY_PASSNO", "INV_GATEPASS_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mGatepassNo = MasterNo
            End If
        End If

        SqlStr = " SELECT SUM(DECODE(ITEM_IO,'I',1,-1)*ITEM_QTY) AS ITEMQTY " & vbCrLf & " FROM DSP_PAINT57F4_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRIM(PARTY_F4NO)='" & MainClass.AllowSingleQuote(Trim(pRefNo)) & "'" & vbCrLf & " AND ITEM_CODE IN " & pItemCode & " AND ISSCRAP='N'"

        If Val(CStr(mGatepassNo)) <> 0 Then
            SqlStr = SqlStr & vbCrLf & " AND BILL_NO<>'" & mGatepassNo & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " HAVING SUM(DECODE(ITEM_IO,'I',1,-1)*ITEM_QTY)>0"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            SprdMain.Row = mRow
            With RsTemp
                SprdMain.Col = ColF4Stock
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("ITEMQTY").Value), "", .Fields("ITEMQTY").Value)))
            End With
            FillREFDetail = True
        Else
            If Cancel = True Then
                MsgInformation("Either Invalid 57F4 No or Invalid Item Code for This Item")
                MainClass.SetFocusToCell(SprdMain, mRow, ColF4No)
                FillREFDetail = False
            Else
                FillREFDetail = True
            End If
        End If

        Exit Function
ERR1:
        FillREFDetail = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function GetValidItem(ByRef pItemCode As String) As Boolean

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim xSuppCode As String
        Dim RsTemp As ADODB.Recordset = Nothing

        xSuppCode = IIf(Trim(txtSuppcode.Text) = "", "-1", Trim(txtSuppcode.Text))


        mSqlStr = "SELECT B.ITEM_CODE " & vbCrLf & " FROM INV_ITEM_MST A, FIN_SUPP_CUST_DET B" & vbCrLf & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf & " AND A.ITEM_CODE=B.ITEM_CODE " & vbCrLf & " AND B.SUPP_CUST_CODE='" & xSuppCode & "'" & vbCrLf & " AND B.ITEM_CODE LIKE '" & pItemCode & "%'"
        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetValidItem = True
        Else
            MsgInformation("Please Check Item.")
            GetValidItem = False
        End If

        Exit Function
ErrPart:
        GetValidItem = False
    End Function
    Private Function CheckDuplicateItem(ByRef mItemCode As String) As Boolean

        On Error GoTo ERR1
        Dim I As Integer
        Dim mItemRept As Integer

        If mItemCode = "" Then CheckDuplicateItem = True : Exit Function
        With SprdMain
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColItemCode
                If UCase(Trim(.Text)) = UCase(Trim(mItemCode)) Then
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

    Private Function FillGridRow(ByRef mItemCode As String) As Boolean

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        If mItemCode = "" Then Exit Function

        SqlStr = ""
        SqlStr = " Select INVMST.ITEM_CODE,INVMST.ITEM_SHORT_DESC,INVMST.PURCHASE_UOM, " & vbCrLf & " ID.ITEM_RATE,  ID.DISC_PER,CUSTOMER_PART_NO,ITEM_COLOR " & vbCrLf & " FROM FIN_SUPP_CUST_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND ID.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtSuppcode.Text) & "'" & vbCrLf & " AND INVMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND INVMST.ITEM_CODE='" & Trim(mItemCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMisc.EOF = False Then
            SprdMain.Row = SprdMain.ActiveRow
            With RsMisc

                SprdMain.Col = ColItemName
                SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_SHORT_DESC").Value), "", .Fields("ITEM_SHORT_DESC").Value)

                SprdMain.Col = ColUom
                SprdMain.Text = IIf(IsDBNull(.Fields("PURCHASE_UOM").Value), "", .Fields("PURCHASE_UOM").Value)

                MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                FormatSprdMain(-1)

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

    Private Function DuplicateItem(ByRef pCol As Integer, ByRef pRow As Integer) As Boolean

        Dim CntRow As Integer
        Dim mCount As Byte
        Dim mCheckItemCode As String
        Dim mItemCode As String
        Dim mLotNo As String
        Dim mCheckLotNo As String
        Dim mF4No As String
        Dim mCheckF4No As String
        Dim mCheckHeatNo As String
        Dim mInwardCode As String
        Dim mCheckInwardCode As String
        Dim mHeatNo As String

        Dim mIncomingCodeConsider As Boolean = False


        'If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
        mIncomingCodeConsider = True
        'Else
        '    mIncomingCodeConsider = False
        'End If
        'If RsCompany.Fields("COMPANY_CODE").Value = 9 Then
        '    DuplicateItem = False
        '    Exit Function
        'End If

        With SprdMain
            .Row = pRow
            .Col = ColItemCode
            mCheckItemCode = Trim(UCase(.Text))

            .Col = ColHeatNo
            mCheckHeatNo = Trim(UCase(.Text))

            .Col = ColBatchNo
            mCheckLotNo = Trim(UCase(.Text))

            .Col = ColF4No
            mCheckF4No = Trim(UCase(.Text))

            .Col = ColIncomingItemCode
            mCheckInwardCode = IIf(mIncomingCodeConsider = True, Trim(UCase(.Text)), "")

            For CntRow = 1 To .MaxRows
                .Row = CntRow
                .Col = ColItemCode
                mItemCode = Trim(UCase(.Text))

                .Col = ColHeatNo
                mHeatNo = Trim(UCase(.Text))

                .Col = ColBatchNo
                mLotNo = Trim(UCase(.Text))

                .Col = ColF4No
                mF4No = Trim(UCase(.Text))

                .Col = ColIncomingItemCode
                mInwardCode = IIf(mIncomingCodeConsider = True, Trim(UCase(.Text)), "")

                If mCheckItemCode <> "" Then
                    If (mItemCode & ":" & mHeatNo & ":" & mLotNo & ":" & mF4No & ":" & mInwardCode = mCheckItemCode & ":" & mCheckHeatNo & ":" & mCheckLotNo & ":" & mCheckF4No & ":" & mCheckInwardCode) Then
                        mCount = mCount + 1
                    End If
                End If


                '            If (mItemCode = mCheckItemCode And mCheckItemCode <> "") Then
                '                mCount = mCount + 1
                '            End If

                If mCount > 1 Then
                    DuplicateItem = True
                    MsgInformation("Duplicate Item : " & mCheckItemCode)
                    MainClass.SetFocusToCell(SprdMain, pRow, pCol)
                    Exit Function
                End If
            Next
        End With
    End Function
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

    Private Function FillItemDescFromItemCode(ByRef pItemCode As String) As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        FillItemDescFromItemCode = False
        If Trim(pItemCode) = "" Then Exit Function
        With SprdMain
            SqlStr = "SELECT ITEM_SHORT_DESC,ISSUE_UOM " & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND LTRIM(RTRIM(ITEM_CODE))='" & MainClass.AllowSingleQuote(pItemCode) & "'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                .Row = .ActiveRow
                .Col = ColItemName
                .Text = IIf(IsDBNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value)

                .Col = ColUom
                .Text = IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)

                .Col = ColStockType
                .Text = IIf(Trim(.Text) = "", "ST", Trim(.Text))
                FillItemDescFromItemCode = True
            End If
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Sub FillItemDescFromItemDesc(ByRef pItemDesc As String)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        If Trim(pItemDesc) = "" Then Exit Sub
        With SprdMain
            SqlStr = "SELECT ITEM_CODE,CUSTOMER_PART_NO,ISSUE_UOM " & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND LTRIM(RTRIM(ITEM_SHORT_DESC))='" & MainClass.AllowSingleQuote(pItemDesc) & "'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                .Row = .ActiveRow
                .Col = ColItemCode
                .Text = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)

                '            .Col = ColPartNo
                '            .Text = IIf(IsNull(RsTemp!CUSTOMER_PART_NO), "", RsTemp!CUSTOMER_PART_NO)

                .Col = ColUom
                .Text = IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)

            Else
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColQty)
            End If
        End With
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub SprdMain_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles SprdMain.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        'Dim mCancel As Boolean
        '    mCancel = True
        '    With SprdMain
        '        SprdMain_LeaveCell .ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, mCancel
        '        Cancel = mCancel
        '    End With
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub SprdView_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdView.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        With SprdView
            .Row = eventArgs.row
            .Col = 1
            txtSlipno.Text = .Text
            txtSlipNo_Validating(txtSlipno, New System.ComponentModel.CancelEventArgs(False))
            If txtSlipno.Enabled = True Then txtSlipno.Focus()
            CmdView_Click(CmdView, New System.EventArgs())
        End With
    End Sub
    Private Function AutoGenSeqNo() As String

        On Error GoTo AutoGenSeqNoErr
        Dim RsMRRMainGen As ADODB.Recordset = Nothing
        Dim mNewSeqNo As Integer
        Dim SqlStr As String = ""
        Dim mMaxValue As String
        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_RGPSLIP)  " & vbCrLf & " FROM INV_RGP_SLIP_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_RGPSLIP,LENGTH(AUTO_KEY_RGPSLIP)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMRRMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsMRRMainGen
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
        AutoGenSeqNo = mNewSeqNo & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        Exit Function
AutoGenSeqNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function UpdateMain1() As Boolean
        On Error GoTo ErrPart
        Dim mReqnum As String = ""
        Dim SqlStr As String = ""
        Dim mVNoSeq As Double
        Dim IsPaintF4 As String
        Dim mStatus As String
        Dim mMaterial As String
        Dim mGatepasstype As String
        Dim mDivisionCode As Double
        Dim mPurpose As String
        Dim ISNRGPApproved As String = "N"
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()



        '    mItemCode = -1
        '    If MainClass.ValidateWithMasterTable(txtSlipNo.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mItemCode = MasterNo
        '    Else
        '        mItemCode = -1
        '        MsgBox "Item Does Not Exist In Master", vbInformation
        '        GoTo ErrPart
        '    End If
        '


        'mMaterial = IIf(optMaterial(0).Checked = True, "INV", "PRD")
        IsPaintF4 = IIf(chkPaintF4.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        ISNRGPApproved = IIf(chkNRGPApproved.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mPurpose = VB.Left(cboPurpose.Text, 1)

        If MainClass.ValidateWithMasterTable((cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Val(MasterNo)
        End If

        If cboGatePasstype.Text = "RGP" Then
            mGatepasstype = "R"
        ElseIf cboGatePasstype.Text = "NRGP" Then
            mGatepasstype = "N"
        Else
            mGatepasstype = "G"
        End If

        If cboStatus.SelectedIndex = 0 Then
            mStatus = "N"
        ElseIf cboStatus.SelectedIndex = 1 Then
            mStatus = "Y"
        Else
            mStatus = "C"
        End If


        If Val(txtSlipno.Text) = 0 Then
            mVNoSeq = CDbl(AutoGenSeqNo())
        Else
            mVNoSeq = Val(txtSlipno.Text)
        End If

        txtSlipno.Text = CStr(Val(CStr(mVNoSeq)))


        SqlStr = ""
        If ADDMode = True Then
            lblMKey.Text = CStr(mVNoSeq)
            SqlStr = "INSERT INTO INV_RGP_SLIP_HDR (" & vbCrLf & " AUTO_KEY_RGPSLIP, COMPANY_CODE, " & vbCrLf & " RGP_SLIP_DATE, DEPT_CODE, " & vbCrLf & " EMP_CODE, AUTH_GIVEN_BY, RGP_SLIP_STATUS, BEARER_NAME, " & vbCrLf & " SUPP_CUST_CODE, EXP_RTN_DATE, INPUT_FROM_FLAG, VEHICLE_NO, " & vbCrLf & " INWARD_ITEM_CODE, INWARD_ITEM_QTY, " & vbCrLf & " ADDUSER, ADDDATE, MODUSER, MODDATE,ISPAINTF4,GATEPASS_TYPE,DIV_CODE,PURPOSE,BILL_TO_LOC_ID,NRGP_APPROVED)" & vbCrLf & " VALUES( " & vbCrLf & " " & Val(CStr(mVNoSeq)) & "," & vbCrLf & " " & RsCompany.Fields("Company_Code").Value & "," & vbCrLf & " TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & MainClass.AllowSingleQuote((txtDept.Text)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtEmp.Text)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtAuthority.Text)) & "', " & vbCrLf & " '" & mStatus & "', '" & MainClass.AllowSingleQuote((txtBearername.Text)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtSuppcode.Text)) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(txtReturnDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mMaterial & "', '" & MainClass.AllowSingleQuote((txtVehicle.Text)) & "'," & vbCrLf _
                & " '', 0, " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','', " & vbCrLf & " '" & IsPaintF4 & "','" & mGatepasstype & "'," & mDivisionCode & ",'" & mPurpose & "','" & MainClass.AllowSingleQuote(txtBillTo.Text) & "','N')"

        ElseIf MODIFYMode = True Then
            SqlStr = ""
            SqlStr = "UPDATE INV_RGP_SLIP_HDR SET RGP_SLIP_DATE=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " DEPT_CODE='" & MainClass.AllowSingleQuote((txtDept.Text)) & "', " & vbCrLf & " EMP_CODE ='" & MainClass.AllowSingleQuote((txtEmp.Text)) & "', " & vbCrLf & " BEARER_NAME ='" & MainClass.AllowSingleQuote((txtBearername.Text)) & "'," & vbCrLf & " SUPP_CUST_CODE ='" & MainClass.AllowSingleQuote((txtSuppcode.Text)) & "'," & vbCrLf & " AUTH_GIVEN_BY  ='" & MainClass.AllowSingleQuote((txtAuthority.Text)) & "'," & vbCrLf & " INPUT_FROM_FLAG ='" & mMaterial & "', " & vbCrLf & " RGP_SLIP_STATUS ='" & mStatus & "'," & vbCrLf & " VEHICLE_NO  ='" & MainClass.AllowSingleQuote((txtVehicle.Text)) & "'," & vbCrLf & " EXP_RTN_DATE  =TO_DATE('" & VB6.Format(txtReturnDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " ISPAINTF4='" & IsPaintF4 & "', GATEPASS_TYPE='" & mGatepasstype & "',DIV_CODE=" & mDivisionCode & "," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                & " BILL_TO_LOC_ID='" & MainClass.AllowSingleQuote((txtBillTo.Text)) & "',NRGP_APPROVED= '" & ISNRGPApproved & "'," & vbCrLf _
                & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),PURPOSE='" & mPurpose & "' " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND AUTO_KEY_RGPSLIP =" & Val(lblMKey.Text) & ""
        End If

        PubDBCn.Execute(SqlStr)

        If UpdateDetail1(CStr(Val(CStr(mVNoSeq))), mDivisionCode) = False Then GoTo ErrPart
        'If UpdateOutDetail = False Then GoTo ErrPart

        UpdateMain1 = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        UpdateMain1 = False
        PubDBCn.RollbackTrans() ''
        If ADDMode = True Then
            txtSlipno.Text = ""
        End If

        RsReqMain.Requery() ''.Refresh
        RsReqDetail.Requery() ''.Refresh
        If Err.Description = "" Then Exit Function
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Function
    Private Function UpdateDetail1(ByRef pVnoseq As String, ByRef mDivisionCode As Double) As Boolean

        On Error GoTo UpdateDetail1Err
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mUOM As String = ""
        Dim mHeatNo As String
        Dim mBatchNo As String
        Dim mStockType As String = ""
        Dim mQty As Double
        Dim mRemarks As String
        Dim mF4No As Double
        Dim mLotNo As String
        Dim mJobOrderNo As Double
        Dim mIncomingItemCode As String
        Dim mStockQty As Double
        Dim mQtyKgs As Double

        If DeleteOutRGPDetail(PubDBCn, Val(lblMKey.Text)) = False Then GoTo UpdateDetail1Err

        SqlStr = " Delete From INV_RGP_SLIP_DET " & vbCrLf & " WHERE AUTO_KEY_RGPSLIP=" & Val(lblMKey.Text) & ""
        PubDBCn.Execute(SqlStr)

        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColItemDesc
                mItemDesc = Val(.Text)

                .Col = ColUom
                mUOM = MainClass.AllowSingleQuote(.Text)

                .Col = ColHeatNo
                mHeatNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColBatchNo
                mBatchNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColStockType
                mStockType = MainClass.AllowSingleQuote(.Text)

                .Col = ColLotNo
                mLotNo = Trim(.Text)

                .Col = ColQty
                mQty = Val(.Text)

                .Col = ColQtyKGs
                mQtyKgs = Val(.Text)

                .Col = ColF4No
                mF4No = Val(.Text)

                .Col = ColRemarks
                mRemarks = MainClass.AllowSingleQuote(.Text)

                .Col = ColIncomingItemCode
                mIncomingItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColJobOrderNo
                mJobOrderNo = Val(.Text)

                If mItemCode <> "" And mQty > 0 Then
                    SqlStr = " INSERT INTO INV_RGP_SLIP_DET (AUTO_KEY_RGPSLIP,SERIAL_NO,FROM_ITEM_CODE,FROM_ITEM_UOM," & vbCrLf _
                            & " REMARKS_PURPOSE,STOCK_TYPE,ITEM_QTY, ITEM_QTY_KGS, F4No, LOT_NO, AUTO_KEY_WO,HEAT_NO,BATCH_NO,INWARD_ITEM_CODE,COMPANY_CODE) " & vbCrLf
                    SqlStr = SqlStr & " VALUES (" & Val(lblMKey.Text) & ", " & I & "," & vbCrLf _
                            & "'" & MainClass.AllowSingleQuote(mItemCode) & "', " & vbCrLf _
                            & "'" & MainClass.AllowSingleQuote(mUOM) & "', " & vbCrLf _
                            & "'" & MainClass.AllowSingleQuote(mRemarks) & "', " & vbCrLf _
                            & "'" & MainClass.AllowSingleQuote(mStockType) & "'," & vbCrLf _
                            & " " & mQty & ", " & mQtyKgs & "," & IIf(mF4No = 0, "Null", mF4No) & "," & vbCrLf _
                            & "'" & mLotNo & "'," & mJobOrderNo & ",'" & mHeatNo & "','" & mBatchNo & "','" & mIncomingItemCode & "'," & RsCompany.Fields("Company_Code").Value & ") "
                    PubDBCn.Execute(SqlStr)

                    '                If UpdateStockTRN(PubDBCn, txtDate.Text, txtSlipno.Text, txtDate.Text, _
                    ''                                 2, 1, mItemCode, mStockType, "ISSUE", "", "", txtDept.Text, "", 0, mIssueQty, "N") = False Then GoTo UpdateDetail1Err

                End If
            Next
        End With

        'Auto SRN slip Not required

        'If Mid(cboPurpose.Text, 1, 1) = "B" And ADDMode = True And Trim(txtDept.Text) <> "STR" Then
        '    Dim mSRNNo As Double
        '    Dim mTariffCode As String
        '    Dim mProdType As String

        '    mSRNNo = CDbl(AutoGenSRNSeqNo())
        '    SqlStr = "INSERT INTO INV_SRN_HDR (" & vbCrLf _
        '        & " AUTO_KEY_SRN, COMPANY_CODE, SRN_DATE, " & vbCrLf _
        '        & " DEPT_CODE, EMP_CODE, COST_CENTER_CODE, OPR_CODE,REMARKS, PRD_FLOOR,  " & vbCrLf _
        '        & " ACTIONTAKEN, STATUS, BOOKTYPE, BOOKSUBTYPE," & vbCrLf _
        '        & " ADDUSER,ADDDATE,MODUSER,MODDATE,DIV_CODE,CLOSED_SRN)" & vbCrLf _
        '        & " VALUES( " & vbCrLf _
        '        & " " & Val(CStr(mSRNNo)) & ", " & RsCompany.Fields("Company_Code").Value & ", TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
        '        & " '" & MainClass.AllowSingleQuote(txtDept.Text) & "', '" & MainClass.AllowSingleQuote((txtEmp.Text)) & "', " & vbCrLf _
        '        & " '001',  '', 'FROM RGP SLIP', 'Y', " & vbCrLf & " '', 'Y', " & vbCrLf _
        '        & " 'P', 'O', " & vbCrLf _
        '        & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'',''," & mDivisionCode & ",'N')"

        '    PubDBCn.Execute(SqlStr)

        '    With SprdMain
        '        For I = 1 To .MaxRows - 1
        '            .Row = I

        '            .Col = ColItemCode
        '            mItemCode = Trim(MainClass.AllowSingleQuote(.Text))

        '            MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "TARIFF_CODE", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
        '            mTariffCode = MasterNo

        '            .Col = ColUom
        '            mUOM = MainClass.AllowSingleQuote(.Text)

        '            .Col = ColStockType
        '            mStockType = MainClass.AllowSingleQuote(.Text)

        '            .Col = ColLotNo
        '            mLotNo = Trim(.Text)

        '            .Col = ColStockBal
        '            mStockQty = Val(.Text)

        '            .Col = ColQty
        '            mQty = Val(.Text)

        '            If RsCompany.Fields("STOCKBALCHECK").Value = "Y" Then
        '                mQty = IIf(mStockQty <mQty, mStockQty, mQty)
        '            End If

        '            .Col = ColRemarks
        '            mRemarks = MainClass.AllowSingleQuote(.Text)

        '            mProdType = GetProductionType(mItemCode)

        '            SqlStr = ""

        '            If mItemCode <> "" And mQty > 0 Then
        '                SqlStr = " INSERT INTO INV_SRN_DET ( " & vbCrLf & " AUTO_KEY_SRN,SERIAL_NO,ITEM_CODE,ITEM_UOM,RTN_QTY," & vbCrLf & " SUPP_CUST_CODE,FROM_STOCK_TYPE,TO_STOCK_TYPE,REMARKS,COMPANY_CODE, LOT_NO) "
        '                SqlStr = SqlStr & vbCrLf & " VALUES (" & Val(mSRNNo) & ", " & I & "," & vbCrLf _
        '                    & " '" & MainClass.AllowSingleQuote(mItemCode) & "', " & vbCrLf _
        '                    & " '" & MainClass.AllowSingleQuote(mUOM) & "', " & vbCrLf _
        '                    & " " & mQty & ", " & vbCrLf _
        '                    & " '" & MainClass.AllowSingleQuote(txtSuppcode.Text) & "', " & vbCrLf _
        '                    & " '" & MainClass.AllowSingleQuote(mStockType) & "', " & vbCrLf _
        '                    & " '" & MainClass.AllowSingleQuote(mStockType) & "', " & vbCrLf _
        '                    & " ''," & RsCompany.Fields("COMPANY_CODE").Value & ",'" & mLotNo & "') "

        '                PubDBCn.Execute(SqlStr)
        '            End If


        '            If UpdateStockTRN(PubDBCn, ConStockRefType_SRN, CStr(mSRNNo), I, (txtDate.Text), (txtDate.Text), mStockType, mItemCode, mUOM, mLotNo, mQty,
        '                              0, "O", 0, 0, "", "", (txtDept.Text), "STR", "", "N", UCase("To : STORE (ST STOCK)"), txtSuppcode.Text, ConPH,
        '                              mDivisionCode, "P", "") = False Then GoTo UpdateDetail1Err


        '            If UpdateStockTRN(PubDBCn, ConStockRefType_SRN, CStr(mSRNNo), I, (txtDate.Text), (txtDate.Text), mStockType, mItemCode, mUOM, mLotNo, mQty,
        '                              0, "I", 0, 0, "", "", "STR", (txtDept.Text), "", "N", UCase("From : " & txtDeptName.Text & " (" & mStockType & " STOCK )"), txtSuppcode.Text, ConWH,
        '                              mDivisionCode, "P", "") = False Then GoTo UpdateDetail1Err

        '        Next
        '    End With

        'End If

        UpdateDetail1 = True
        Exit Function
UpdateDetail1Err:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Function
    Private Function AutoGenSRNSeqNo() As String

        On Error GoTo AutoGenSRNSeqNoErr
        Dim RsMRRMainGen As ADODB.Recordset = Nothing
        Dim mNewSeqNo As Integer
        Dim SqlStr As String = ""
        Dim mMaxValue As String

        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_SRN)  " & vbCrLf _
            & " FROM INV_SRN_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_SRN,LENGTH(AUTO_KEY_SRN)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMRRMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsMRRMainGen
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
        AutoGenSRNSeqNo = mNewSeqNo & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        Exit Function
AutoGenSRNSeqNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Public Function DeleteOutRGPDetail(ByRef pDBCn As ADODB.Connection, ByRef pMkey As Double) As Boolean
        Dim SqlStr As String = ""
        On Error GoTo DeleteDSDailyDetailErr

        SqlStr = ""
        SqlStr = "DELETE FROM INV_RGP_OUT_DET  " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AUTO_KEY_RGPSLIP=" & Val(CStr(pMkey)) & " "
        pDBCn.Execute(SqlStr)

        DeleteOutRGPDetail = True
        Exit Function

DeleteDSDailyDetailErr:
        MsgInformation(Err.Description)
        DeleteOutRGPDetail = False
    End Function
    Private Function FieldsVarification() As Boolean

        On Error GoTo err_Renamed
        Dim mRow As Integer
        Dim mSTTaxcount As Integer
        Dim mIsAuthorisedUser As String
        Dim mLotNoRequied As String
        Dim mItemCode As String
        Dim mInWardItemCode As String
        Dim mBalQty As Double
        Dim xF4No As Double
        Dim xF4Stock As Double
        Dim xQty As Double
        Dim mIncomingItemCode As String
        Dim mStockType As String = ""
        Dim mLotNo As String
        Dim mUOM As String = ""
        Dim SqlStr As String = ""
        Dim mDivisionCode As Double
        Dim xPoNo As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCount As Integer
        Dim mPendRGPQty As Double
        Dim mAgeDays As Double
        Dim mPOQty As Double
        Dim mPONo As Double
        Dim mReqQty As Double
        Dim mSendRGPQty As Double
        Dim mFinishedJobCode As String
        Dim mTableName As String
        Dim mHeatNo As String
        Dim mJWUOM As String

        Dim mInterUnitCompanyCode As Long

        Dim mPartyGSTNo As String
        Dim mSameGSTNo As String

        FieldsVarification = True

        mPartyGSTNo = ""
        mPartyGSTNo = GetPartyBusinessDetail(Trim(txtSuppcode.Text), Trim(txtBillTo.Text), "GST_RGN_NO")
        mSameGSTNo = IIf(mPartyGSTNo = IIf(IsDBNull(RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "", RsCompany.Fields("COMPANY_GST_RGN_NO").Value), "Y", "N")

        If mSameGSTNo = "Y" Then
            Dim mRGPNo As String
            Dim mTillDate As String
            mTillDate = DateAdd("d", -2, txtDate.Text)

            SqlStr = "SELECT AUTO_KEY_PASSNO " & vbCrLf _
               & " FROM INV_GATEPASS_HDR" & vbCrLf _
               & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtSuppcode.Text) & "'" & vbCrLf _
               & " AND IS_GATENTRY_MADE='N' " & vbCrLf _
               & " AND GATEPASS_DATE >= TO_DATE('" & VB6.Format(RsCompany.Fields("Start_Date").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
               & " AND GATEPASS_DATE <= TO_DATE('" & VB6.Format(mTillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                Do While RsTemp.EOF = False
                    mRGPNo = IIf(mRGPNo = "", "", mRGPNo & ", ") & IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_PASSNO").Value), "", RsTemp.Fields("AUTO_KEY_PASSNO").Value)
                    RsTemp.MoveNext()
                Loop
                MsgInformation("Following RGP (" & mRGPNo & ") are pending for more than 24 Hours, so Cann't be save.")
                FieldsVarification = False
                Exit Function
            End If
        End If



        If ValidateBranchLocking((txtDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If

        If ValidateBookLocking(PubDBCn, CInt(ConLockGatePassReq), txtDate.Text) = True Then
            FieldsVarification = False
            Exit Function
        End If

        If ValidateDeptRight(PubUserID, Trim(txtDept.Text), UCase(Trim(txtDeptName.Text))) = False Then
            FieldsVarification = False
            Exit Function
        End If

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            FieldsVarification = False
            Exit Function
        End If

        If UCase(cboGatePasstype.Text) = "NRGP" Then
            'cboPurpose.SelectedIndex = 0
            If cboPurpose.Text = "" Then
                If cboPurpose.Enabled = True Then cboPurpose.Focus()
                MsgInformation("Please Select Purpose For.")
                FieldsVarification = False
                Exit Function
            End If
            If cboPurpose.SelectedIndex <= 2 Then
                If cboPurpose.Enabled = True Then cboPurpose.Focus()
                MsgInformation("Cann't be Select None / Jobwork / Work Order Purpose for RGP.")
                FieldsVarification = False
                Exit Function
            End If
        Else
            If cboPurpose.Text = "" Then
                If cboPurpose.Enabled = True Then cboPurpose.Focus()
                MsgInformation("Please Select Purpose for.")
                FieldsVarification = False
                Exit Function
            End If
            If cboPurpose.SelectedIndex = 0 Then
                If cboPurpose.Enabled = True Then cboPurpose.Focus()
                MsgInformation("Cann't be Select None Purpose for RGP.")
                FieldsVarification = False
                Exit Function
            End If
        End If

        If MainClass.ValidateWithMasterTable((cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Val(MasterNo)
        Else
            MsgInformation("Invalid Division Name.")
            FieldsVarification = False
            Exit Function
        End If

        If ValidateDivisionRight(PubUserID, mDivisionCode, UCase(Trim(cboDivision.Text))) = False Then
            FieldsVarification = False
            Exit Function
        End If

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsReqMain.EOF = True Then Exit Function

        'If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Or RsCompany.Fields("COMPANY_CODE").Value = 20 Then
        '    mIsAuthorisedUser = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)

        '    If InStr(1, mIsAuthorisedUser, "S") = 0 Then
        '        If UCase(cboGatePasstype.Text) = "NRGP" Then
        '            MsgBox("You are not Authorised to send Material in NRGP.", MsgBoxStyle.Information)
        '            FieldsVarification = False
        '            Exit Function
        '        End If
        '    End If
        'End If


        If MODIFYMode = True And txtSlipno.Text = "" Then
            MsgInformation("Requisition No. Cann't Blank")
            FieldsVarification = False
            Exit Function
        End If

        If txtDate.Text = "" Then
            MsgBox("Date is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtDate.Focus()
            Exit Function
        ElseIf FYChk((txtDate.Text)) = False Then
            FieldsVarification = False
            If txtDate.Enabled = True Then txtDate.Focus()
            Exit Function
        End If

        If CDate(txtDate.Text) > CDate(PubCurrDate) Then
            MsgInformation("Invaild Date. Date cann't be Greater Then Current date.")
            If txtDate.Enabled Then txtDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If VB.Left(cboGatePasstype.Text, 1) = "N" Then
            txtReturnDate.Text = VB6.Format(txtDate.Text, "DD/MM/YYYY")
        End If

        If MODIFYMode = True Then
            If MainClass.ValidateWithMasterTable(txtSlipno.Text, "REQ_NO", "AUTO_KEY_PASSNO", "INV_GATEPASS_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                MsgBox("Gate Pass (" & MasterNo & ") had Made Against this Requistion Note, So Cann't be Changed", MsgBoxStyle.Information)
                FieldsVarification = False
                Exit Function
            End If
        End If

        If txtReturnDate.Text = "" And (VB.Left(cboGatePasstype.Text, 1) = "R" Or VB.Left(cboGatePasstype.Text, 1) = "G") Then
            MsgBox("Return Date is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtReturnDate.Enabled = True Then txtReturnDate.Focus()
            Exit Function
        ElseIf Not IsDate(txtReturnDate.Text) Then
            FieldsVarification = False
            If txtReturnDate.Enabled = True Then txtReturnDate.Focus()
            Exit Function
        End If

        If CDate(txtDate.Text) > CDate(txtReturnDate.Text) Then
            MsgBox("Return Date Cann't be Less than Req. Date", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtReturnDate.Enabled = True Then txtReturnDate.Focus()
            Exit Function
        End If

        If txtSuppcode.Text = "" Then
            MsgBox("Supplier code is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtSuppcode.Focus()
            Exit Function
        End If

        If txtAuthority.Text = "" Then
            MsgBox("Authority code is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtAuthority.Focus()
            Exit Function
        End If

        If txtDept.Text = "" Then
            MsgBox("Department code is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtDept.Focus()
            Exit Function
        End If
        If txtEmp.Text = "" Then
            MsgBox("Responsible Person Code is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtEmp.Focus()
            Exit Function
        End If

        If txtBearername.Text = "" Then
            MsgBox("Bearer Name is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtBearername.Focus()
            Exit Function
        End If


        'SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND (EMP_LEAVE_DATE IS NULL OR (EMP_LEAVE_DATE >= TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')))"

        'SqlStr = SqlStr & vbCrLf & " AND EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'"
        'SqlStr = SqlStr & vbCrLf & " AND EMP_CATG IN ('D','G','P','S')"

        'If MainClass.ValidateWithMasterTable(txtEmp.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
        '    MsgInformation("Employee is not in Such Dept. Please Check.")
        '    FieldsVarification = False
        '    Exit Function
        'End If

        If ADDMode = True Then
            If MainClass.ValidateWithMasterTable(txtSuppcode.Text, "SUPP_CUST_CODE", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='C'") = True Then
                MsgBox("Supplier / Customer Master is Closed, So cann't be saved", MsgBoxStyle.Information)
                FieldsVarification = False
                If txtSuppcode.Enabled = True Then txtSuppcode.Focus()
                Exit Function
            End If

            If MainClass.ValidateWithMasterTable(txtSuppcode.Text, "SUPP_CUST_CODE", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STOP_RGP='Y'") = True Then
                MsgBox("Supplier / Customer Master is Closed, So cann't be saved", MsgBoxStyle.Information)
                FieldsVarification = False
                If txtSuppcode.Enabled = True Then txtSuppcode.Focus()
                Exit Function
            End If

        End If

        If MainClass.ValidateWithMasterTable(txtSuppName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')  AND SUPP_CUST_CODE NOT IN ('" & IIf(IsDBNull(RsCompany.Fields("COMP_AC_CODE").Value), "", RsCompany.Fields("COMP_AC_CODE").Value) & "')") = False Then
            MsgInformation("Supplier Name is not a Supplier or Customer Category. Cannot Save")
            If txtSuppName.Enabled = True Then txtSuppName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtBillTo.Text) = "" Then
            MsgInformation("Bill To is blank. Cannot Save")
            txtBillTo.Focus()
            FieldsVarification = False
            Exit Function
        Else
            If MainClass.ValidateWithMasterTable(txtBillTo.Text, "LOCATION_ID", "LOCATION_ID", "FIN_SUPP_CUST_BUSINESS_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtSuppName.Text) & "'") = False Then
                MsgBox("Invalid Location Id for such Customer.", MsgBoxStyle.Information)
                txtBillTo.Focus()
                FieldsVarification = False
            End If
        End If

        If CheckStockQty(SprdMain, ColStockBal, ColQty, ColItemCode, ColStockType, True) = False Then
            FieldsVarification = False
            Exit Function
        End If

        If VB.Left(cboPurpose.Text, 1) = "D" Then
            If MsgQuestion("You Select RGP Purpose is Tool Trial. So that Account Payment will not be made for this RGP. Want to Continue..") = CStr(MsgBoxResult.No) Then
                FieldsVarification = False
                Exit Function
            End If
        ElseIf VB.Left(cboPurpose.Text, 1) = "F" Then
            If MsgQuestion("You Select RGP Purpose is Testing / Trial. So that Account Payment will not be made for this RGP. Want to Continue..") = CStr(MsgBoxResult.No) Then
                FieldsVarification = False
                Exit Function
            End If
        ElseIf VB.Left(cboPurpose.Text, 1) = "G" Then
            If MsgQuestion("You Select RGP Purpose is Trolley / Bins. So that Account Payment will not be made for this RGP. Want to Continue..") = CStr(MsgBoxResult.No) Then
                FieldsVarification = False
                Exit Function
            End If
        ElseIf VB.Left(cboPurpose.Text, 1) = "H" Then
            If MsgQuestion("You Select RGP Purpose is FOC - Under Warranty / Re-Repair. So that Account Payment will not be made for this RGP. Want to Continue..") = CStr(MsgBoxResult.No) Then
                FieldsVarification = False
                Exit Function
            End If
        End If

        With SprdMain
            For mRow = 1 To .MaxRows
                .Row = mRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColLotNo
                mLotNo = Trim(.Text)

                .Col = ColHeatNo
                mHeatNo = Trim(.Text)

                .Col = ColUom
                mUOM = Trim(.Text)

                .Col = ColStockType
                mStockType = Trim(.Text)

                .Col = ColJobOrderNo
                mPONo = Val(.Text)

                .Col = ColQty
                mReqQty = Val(.Text)

                If mItemCode <> "" Then
                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_JW_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mJWUOM = MasterNo
                    End If

                    If Trim(UCase(mJWUOM)) <> Trim(UCase(mUOM)) And VB.Left(cboPurpose.Text, 1) = "B" Then
                        .Col = ColQtyKGs
                        If Val(.Text) = 0 Then
                            MsgInformation("Please Enter the Qty in Kgs Also.")
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If

                    If mLotNo <> "" Then
                        mTableName = ConInventoryTable
                        If MainClass.ValidateWithMasterTable(mLotNo, "BATCH_NO", "BATCH_NO", mTableName, PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND ITEM_CODE='" & mItemCode & "'") = False Then
                            MsgInformation("Invalid Lot No")
                            MainClass.SetFocusToCell(SprdMain, mRow, ColLotNo)
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If

                    If VB.Left(Trim(cboGatePasstype.Text), 1) = "R" Then
                        If VB.Left(cboPurpose.Text, 1) = "G" Then

                        Else

                            If (VB.Left(cboPurpose.Text, 1) = "B" Or VB.Left(cboPurpose.Text, 1) = "C") Then
                                If VB.Left(cboPurpose.Text, 1) = "B" Then
                                    SprdMain.Col = ColIncomingItemCode
                                    mFinishedJobCode = Trim(SprdMain.Text)

                                Else
                                    mFinishedJobCode = ""
                                End If
                                If ValidatePurchaseOrder(mFinishedJobCode, mPONo, IIf(VB.Left(cboPurpose.Text, 1) = "B", "J", "W")) = False Then
                                    MsgInformation("Please Select Valid Job Order Or Work Order for Item Code : " & mItemCode)
                                    MainClass.SetFocusToCell(SprdMain, mRow, ColJobOrderNo)
                                    FieldsVarification = False
                                    Exit Function
                                End If

                                If mPONo > 0 Then
                                    mPOQty = GetPOQty(mFinishedJobCode, mPONo, mUOM)
                                    If mPOQty > 0 Then
                                        mSendRGPQty = IIf(cboStatus.SelectedIndex = 0, mReqQty, 0) + GetRGPQty(mItemCode, mPONo, "O", "")
                                        If mPOQty < mSendRGPQty Then
                                            MsgInformation("You cann't be send more than Job Order Qty. Job Order Qty is " & mPOQty & " & You Send For RGP Qty " & mSendRGPQty & ".")
                                            MainClass.SetFocusToCell(SprdMain, mRow, ColQty)
                                            FieldsVarification = False
                                            Exit Function
                                        End If
                                    End If
                                    If CheckINItemPOQty(mRow, mItemCode, mPONo) = False Then
                                        FieldsVarification = False
                                        Exit Function
                                    End If
                                Else
                                    MsgInformation("Please Select Valid Job Order Or Work Order for Item Code : " & mItemCode)
                                    MainClass.SetFocusToCell(SprdMain, mRow, ColJobOrderNo)
                                    FieldsVarification = False
                                    Exit Function
                                End If
                            End If

                            mPendRGPQty = GetPendingRGPQty(mItemCode, (txtDate.Text), Trim(txtSuppcode.Text), mAgeDays)

                            'If RsCompany.Fields("STOCKBALCHECK").Value = "Y" Then
                            '    If mPendRGPQty > 0 Then
                            '        If mAgeDays > 30 Then
                            '            If CheckRGPApproval(mItemCode, (txtDate.Text), Trim(txtSuppcode.Text), "P", mReqQty, Val(txtSlipno.Text)) = False Then
                            '                MsgInformation("RGP already Pending for Item Code - " & mItemCode & " for such Supplier from " & mAgeDays & " Days. Approval Need from Plant Head.")
                            '                MainClass.SetFocusToCell(SprdMain, mRow, ColItemCode)
                            '                FieldsVarification = False
                            '                Exit Function
                            '            End If
                            '        ElseIf mAgeDays <= 30 And mAgeDays > 15 Then
                            '            If CheckRGPApproval(mItemCode, (txtDate.Text), Trim(txtSuppcode.Text), "D", mReqQty, Val(txtSlipno.Text)) = False Then
                            '                MsgInformation("RGP already Pending for Item Code - " & mItemCode & " for such Supplier from " & mAgeDays & " Days. Approval Need from Department Head.")
                            '                MainClass.SetFocusToCell(SprdMain, mRow, ColItemCode)
                            '                FieldsVarification = False
                            '                Exit Function
                            '            End If
                            '        End If
                            '    End If
                            'End If
                        End If
                    End If
NextRow:
                    .Col = ColStockBal
                    .Text = CStr(GetBalanceStockQty(mItemCode, (txtDate.Text), mUOM, "STR", mStockType, mLotNo, ConWH, mDivisionCode,,,,, mHeatNo))

                    If ADDMode = True Then
                        If MainClass.ValidateWithMasterTable(Trim(mItemCode), "ITEM_CODE", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS='I'") = True Then
                            MsgInformation("Item Status is Closed, So cann't be Saved. [" & Trim(mItemCode) & "]")
                            MainClass.SetFocusToCell(SprdMain, mRow, ColItemCode)
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If

                    mLotNoRequied = "N"
                    If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mLotNoRequied = MasterNo
                    End If
                    If mLotNoRequied = "Y" Then
                        .Col = ColLotNo
                        If Trim(.Text) = "" Then
                            MsgInformation("Lot No. Must For Such Item.")
                            FieldsVarification = False
                            MainClass.SetFocusToCell(SprdMain, mRow, ColLotNo)
                            Exit Function
                        End If
                    End If

                    .Col = ColStockType
                    If Trim(.Text) = "FG" Then
                        MsgBox("You cann't be send FG Stock through RGP/NRGP.", MsgBoxStyle.Information)
                        FieldsVarification = False
                        MainClass.SetFocusToCell(SprdMain, mRow, ColStockType)
                        Exit Function
                    End If

                    If Trim(.Text) = "QC" Or Trim(.Text) = "CR" Or Trim(.Text) = "WC" Then
                        MsgBox("You cann't be send Material Without QC / CR / WC.", MsgBoxStyle.Information)
                        FieldsVarification = False
                        MainClass.SetFocusToCell(SprdMain, mRow, ColStockType)
                        Exit Function
                    End If



                    .Col = ColStockBal
                    mBalQty = Val(SprdMain.Text)

                    .Col = ColF4No
                    xF4No = Val(SprdMain.Text)

                    .Col = ColF4Stock
                    xF4Stock = Val(SprdMain.Text)

                    .Col = ColQty
                    xQty = Val(SprdMain.Text)

                    .Col = ColIncomingItemCode
                    mIncomingItemCode = Trim(.Text)

                    .Col = ColJobOrderNo
                    xPoNo = Trim(.Text)
                    If RsCompany.Fields("STOCKBALCHECK").Value = "Y" Then
                        If xQty > mBalQty Then
                            MsgInformation("You have not enough Stock.")
                            MainClass.SetFocusToCell(SprdMain, mRow, ColQty)
                            FieldsVarification = False
                            Exit Function
                        End If

                        If xF4No <> 0 Then
                            If xQty > xF4Stock Then
                                MsgInformation("You have not enough F4 Stock.")
                                MainClass.SetFocusToCell(SprdMain, mRow, ColQty)
                                FieldsVarification = False
                                Exit Function
                            End If
                        End If
                    End If

                    If VB.Left(cboPurpose.Text, 1) = "B" Then
                        If Trim(mIncomingItemCode) = "" Then
                            MsgInformation("Please Select In Coming Item Code for such Item")
                            MainClass.SetFocusToCell(SprdMain, mRow, ColIncomingItemCode)
                            FieldsVarification = False
                            Exit Function
                        End If

                        If Trim(mIncomingItemCode) <> "" Then
                            If MainClass.ValidateWithMasterTable(mIncomingItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                                MsgInformation("Invalid Item Code")
                                MainClass.SetFocusToCell(SprdMain, mRow, ColIncomingItemCode)
                                FieldsVarification = False
                                Exit Function
                            End If
                        End If

                        If xPoNo = "" Or xPoNo = "0" Then
                            MsgInformation("Please Select Jobwork Order for Such Item")
                            MainClass.SetFocusToCell(SprdMain, mRow, ColJobOrderNo)
                            FieldsVarification = False
                            Exit Function
                        End If
                        If xPoNo <> "" Then
                            If MainClass.ValidateWithMasterTable(xPoNo, "AUTO_KEY_PO", "PUR_ORD_DATE", "PUR_PURCHASE_HDR", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DIV_CODE=" & mDivisionCode & " AND PUR_TYPE ='J' AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtSuppcode.Text) & "' AND PO_STATUS='Y' AND PO_CLOSED='N' AND AMEND_WEF_DATE<=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')") = False Then
                                MsgInformation("Invalid Ref No for Such Supplier")
                                MainClass.SetFocusToCell(SprdMain, mRow, ColJobOrderNo)
                                FieldsVarification = False
                                Exit Function
                            End If
                        End If
                    Else
                        If Trim(mIncomingItemCode) <> "" Then
                            MsgInformation("Please remove InComing Item Code for such Item")
                            MainClass.SetFocusToCell(SprdMain, mRow, ColIncomingItemCode)
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If

                    If VB.Left(cboPurpose.Text, 1) = "C" Then
                        'If xPoNo = "" Then
                        '    MsgInformation("Please Select WorkOrder Order for Such Item")
                        '    MainClass.SetFocusToCell(SprdMain, mRow, ColJobOrderNo)
                        '    FieldsVarification = False
                        '    Exit Function
                        'End If
                        If xPoNo = "" Then
                            MsgInformation("Please Select WorkOrder Order for Such Item")
                            MainClass.SetFocusToCell(SprdMain, mRow, ColJobOrderNo)
                            FieldsVarification = False
                            Exit Function
                        Else
                            If MainClass.ValidateWithMasterTable(xPoNo, "AUTO_KEY_PO", "PUR_ORD_DATE", "PUR_PURCHASE_HDR", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DIV_CODE=" & mDivisionCode & " AND PUR_TYPE= 'W' AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtSuppcode.Text) & "' AND PO_STATUS='Y' AND PO_CLOSED='N' AND AMEND_WEF_DATE<=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')") = False Then
                                MsgInformation("Invalid Ref No for Such Supplier")
                                MainClass.SetFocusToCell(SprdMain, mRow, ColJobOrderNo)
                                FieldsVarification = False
                                Exit Function
                            End If
                        End If

                    End If


                    'If VB.Left(cboPurpose.Text, 1) = "B" Then

                    '    SqlStr = " SELECT COUNT(1) AS CNT" & vbCrLf & " FROM TEMP_RGP_OUT_DET " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'" & vbCrLf & " AND TRN_SERIAL_NO=" & Val(CStr(mRow)) & ""

                    '    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                    '    If RsTemp.EOF = False Then
                    '        mCount = IIf(IsDbNull(RsTemp.Fields("cnt").Value), 0, RsTemp.Fields("cnt").Value)
                    '        If mCount < 1 Then
                    '            MsgInformation("Please define Consumption for Item : " & mItemCode)
                    '            MainClass.SetFocusToCell(SprdMain, mRow, ColItemDetail)
                    '            FieldsVarification = False
                    '            Exit Function
                    '        End If
                    '    Else
                    '        MsgInformation("Please define Consumption for Item : " & mItemCode)
                    '        MainClass.SetFocusToCell(SprdMain, mRow, ColItemDetail)
                    '        FieldsVarification = False
                    '        Exit Function
                    '    End If
                    'End If

                    If DuplicateItem(ColItemCode, mRow) = True Then
                        FieldsVarification = False
                        Exit Function
                    End If

NextRow1:
                End If
            Next
        End With


        If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "ItemCode Is Blank.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColQty, "N", "Please Check Quantity.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColStockType, "S", "Please Check Stock Type.") = False Then FieldsVarification = False : Exit Function

        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Function
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Public Sub FrmRGPSlip_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor


        SqlStr = ""
        SqlStr = "Select * from INV_RGP_SLIP_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsReqMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from INV_RGP_SLIP_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsReqDetail, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)
        '    Call SetTextLengths
        Clear1()
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

        SqlStr = ""

        ''SELECT CLAUSE...

        SqlStr = " SELECT  AUTO_KEY_RGPSLIP AS REFNO, RGP_SLIP_DATE AS REFDATE, " & vbCrLf _
            & " SUPP_CUST_NAME, DEPT_CODE, BEARER_NAME, " & vbCrLf & " AUTH_GIVEN_BY, IH.EMP_CODE, " & vbCrLf & " CASE WHEN RGP_SLIP_STATUS='N' THEN 'Pending' " & vbCrLf & " WHEN RGP_SLIP_STATUS='Y' THEN 'Completed' ELSE 'Not Completed' END AS STATUS," & vbCrLf & " EXP_RTN_DATE AS RTN_TIME, VEHICLE_NO "


        ''FROM CLAUSE...

        SqlStr = SqlStr & vbCrLf & " FROM INV_RGP_SLIP_HDR IH,FIN_SUPP_CUST_MST CMST "

        ''WHERE CLAUSE...

        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND SUBSTR(AUTO_KEY_RGPSLIP,LENGTH(AUTO_KEY_RGPSLIP)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        ''ORDER BY CLAUSE...

        SqlStr = SqlStr & vbCrLf & " Order by AUTO_KEY_RGPSLIP"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
AssignGridErr:
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1

            .set_RowHeight(0, 400)

            .set_ColWidth(0, 600)
            .set_ColWidth(1, 1000)
            .set_ColWidth(2, 1000)
            .set_ColWidth(3, 2500)
            .set_ColWidth(4, 600)
            .set_ColWidth(5, 1500)
            .set_ColWidth(6, 900)
            .set_ColWidth(7, 900)
            .set_ColWidth(8, 1500)
            .set_ColWidth(9, 1000)
            .set_ColWidth(10, 1500)


            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            SprdView.set_RowHeight(-1, 300)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        With SprdMain
            .set_RowHeight(0, ConRowHeight * 2.5)
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight * 1.5)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsReqDetail.Fields("FROM_ITEM_CODE").DefinedSize ''
            .set_ColWidth(ColItemCode, 8)

            .Col = ColIncomingItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsReqDetail.Fields("INWARD_ITEM_CODE").DefinedSize ''
            .set_ColWidth(ColIncomingItemCode, 8)

            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(ColItemName, 22)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("WO_DESCRIPTION", "PUR_PURCHASE_DET", PubDBCn)
            .set_ColWidth(ColItemDesc, 30)
            .ColHidden = IIf(VB.Left(cboGatePasstype.Text, 1) = "R", False, True)

            .Col = ColUom
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("FROM_ITEM_UOM", "INV_RGP_SLIP_DET", PubDBCn)
            .set_ColWidth(ColUom, 4)

            .Col = ColLotNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = RsReqDetail.Fields("LOT_NO").DefinedSize '' MainClass.SetMaxLength("LOT_NO", "INV_GATE_DET", PubDBCn)
            .set_ColWidth(ColLotNo, 6)
            .ColHidden = True
            .ColsFrozen = ColUom

            .Col = ColHeatNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = RsReqDetail.Fields("HEAT_NO").DefinedSize
            .set_ColWidth(ColHeatNo, 10)
            .ColHidden = IIf(RsCompany.Fields("HEATNO_HIDE").Value = "N", False, True)

            .Col = ColBatchNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsReqDetail.Fields("BATCH_NO").DefinedSize
            .set_ColWidth(ColBatchNo, 9)

            .Col = ColStockType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = MainClass.SetMaxLength("STOCK_TYPE", "INV_RGP_SLIP_DET", PubDBCn)
            .set_ColWidth(ColStockType, 5)

            .Col = ColStockBal
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColStockBal, 10)

            .Col = ColF4No
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = MainClass.SetMaxLength("F4NO", "INV_RGP_SLIP_DET", PubDBCn)
            .set_ColWidth(ColF4No, 7)

            .Col = ColF4Stock
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColF4Stock, 7)

            .Col = ColQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColQty, 9)

            .Col = ColQtyKGs
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColQtyKGs, 9)
            .ColHidden = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 102 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 114, False, True)

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = MainClass.SetMaxLength("REMARKS_PURPOSE", "INV_RGP_SLIP_DET", PubDBCn)
            .set_ColWidth(ColRemarks, 8)

            .Col = ColJobOrderNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = MainClass.SetMaxLength("AUTO_KEY_WO", "INV_RGP_SLIP_DET", PubDBCn)
            .set_ColWidth(ColJobOrderNo, 10)

            .Col = ColItemDetail
            .CellType = SS_CELL_TYPE_BUTTON
            '.Lock = False
            .TypeButtonText = "Details"
            .TypeButtonAlign = SS_CELL_BUTTON_ALIGN_LEFT
            .set_ColWidth(ColItemDetail, 6)
            .ColHidden = True

        End With
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemName, ColUom)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColStockBal, ColStockBal)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColF4Stock, ColF4Stock)

        'If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 114 Then
        'MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColQtyKGs, ColQtyKGs)
        'End If

        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then RsReqDetail.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        With RsReqMain
            txtDate.MaxLength = 10
            txtSlipno.MaxLength = .Fields("AUTO_KEY_RGPSLIP").Precision
            txtDept.MaxLength = .Fields("DEPT_CODE").DefinedSize
            txtEmp.MaxLength = .Fields("EMP_CODE").DefinedSize
            txtAuthority.MaxLength = .Fields("AUTH_GIVEN_BY").DefinedSize
            txtSuppcode.MaxLength = .Fields("SUPP_CUST_CODE").DefinedSize
            txtBearername.MaxLength = .Fields("BEARER_NAME").DefinedSize
            txtVehicle.MaxLength = .Fields("VEHICLE_NO").DefinedSize

            txtBillTo.MaxLength = .Fields("BILL_TO_LOC_ID").DefinedSize

            'txtQty.Maxlength = .Fields("INWARD_ITEM_QTY").Precision
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Show1()

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim mDivisionCode As Double
        Dim mDivisionDesc As String

        With RsReqMain
            If Not .EOF Then
                txtSlipno.Enabled = False
                lblMKey.Text = .Fields("AUTO_KEY_RGPSLIP").Value

                txtSlipno.Text = IIf(IsDBNull(.Fields("AUTO_KEY_RGPSLIP").Value), 0, .Fields("AUTO_KEY_RGPSLIP").Value)
                txtDate.Text = VB6.Format(IIf(IsDBNull(.Fields("RGP_SLIP_DATE").Value), "", .Fields("RGP_SLIP_DATE").Value), "DD/MM/YYYY")
                txtDept.Text = IIf(IsDBNull(.Fields("DEPT_CODE").Value), "", .Fields("DEPT_CODE").Value)
                txtEmp.Text = IIf(IsDBNull(.Fields("EMP_CODE").Value), "", .Fields("EMP_CODE").Value)
                txtPrepareBy.Text = IIf(IsDBNull(.Fields("ADDUSER").Value), "", .Fields("ADDUSER").Value)
                txtSuppcode.Text = IIf(IsDBNull(.Fields("SUPP_CUST_CODE").Value), "", .Fields("SUPP_CUST_CODE").Value)


                Call txtSuppcode_Validating(txtSuppcode, New System.ComponentModel.CancelEventArgs(True))


                txtSuppcode.Enabled = True ''False
                cmdSupplierSearch.Enabled = True ''False
                txtBearername.Text = IIf(IsDBNull(.Fields("BEARER_NAME").Value), "", .Fields("BEARER_NAME").Value)
                'cboStatus.Text = IIf(IsDbNull(.Fields("RGP_SLIP_STATUS").Value), "", .Fields("RGP_SLIP_STATUS").Value)
                txtVehicle.Text = IIf(IsDBNull(.Fields("VEHICLE_NO").Value), "", .Fields("VEHICLE_NO").Value)
                txtReturnDate.Text = IIf(IsDBNull(.Fields("EXP_RTN_DATE").Value), "", .Fields("EXP_RTN_DATE").Value)
                txtAuthority.Text = IIf(IsDBNull(.Fields("AUTH_GIVEN_BY").Value), "", .Fields("AUTH_GIVEN_BY").Value)

                chkPaintF4.CheckState = IIf(.Fields("IsPaintF4").Value = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
                chkNRGPApproved.CheckState = IIf(.Fields("NRGP_APPROVED").Value = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)

                If .Fields("PURPOSE").Value = "A" Then
                    cboPurpose.SelectedIndex = 0
                ElseIf .Fields("PURPOSE").Value = "B" Then
                    cboPurpose.SelectedIndex = 1
                ElseIf .Fields("PURPOSE").Value = "C" Then
                    cboPurpose.SelectedIndex = 2
                ElseIf .Fields("PURPOSE").Value = "D" Then
                    cboPurpose.SelectedIndex = 3
                ElseIf .Fields("PURPOSE").Value = "E" Then
                    cboPurpose.SelectedIndex = 4
                ElseIf .Fields("PURPOSE").Value = "F" Then
                    cboPurpose.SelectedIndex = 5
                ElseIf .Fields("PURPOSE").Value = "G" Then
                    cboPurpose.SelectedIndex = 6
                ElseIf .Fields("PURPOSE").Value = "H" Then
                    cboPurpose.SelectedIndex = 7
                ElseIf .Fields("PURPOSE").Value = "I" Then
                    cboPurpose.SelectedIndex = 8
                Else
                    cboPurpose.SelectedIndex = 9
                End If

                'If .Fields("INPUT_FROM_FLAG").Value = "INV" Then
                '    optMaterial(0).Checked = True
                'Else
                '    optMaterial(1).Checked = True
                'End If

                If .Fields("GATEPASS_TYPE").Value = "G" Then
                    cboGatePasstype.SelectedIndex = 2
                ElseIf .Fields("GATEPASS_TYPE").Value = "N" Then
                    cboGatePasstype.SelectedIndex = 1
                Else
                    cboGatePasstype.SelectedIndex = 0
                End If

                If .Fields("RGP_SLIP_STATUS").Value = "N" Then
                    cboStatus.SelectedIndex = 0
                    'cmdItemCode.Enabled = False ''True
                    'txtItemCode.Enabled = False ''True
                    'txtQty.Enabled = False ''True
                ElseIf .Fields("RGP_SLIP_STATUS").Value = "Y" Then
                    cboStatus.SelectedIndex = 1
                    'cmdItemCode.Enabled = False
                    'txtItemCode.Enabled = False
                    'txtQty.Enabled = False
                Else
                    cboStatus.SelectedIndex = 2
                    'cmdItemCode.Enabled = False
                    'txtItemCode.Enabled = False
                    'txtQty.Enabled = False
                End If

                If MainClass.ValidateWithMasterTable((txtDept.Text), "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtDeptName.Text = MasterNo
                End If

                'If MainClass.ValidateWithMasterTable((txtAuthority.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '    txtAuthorityName.Text = MasterNo
                'End If

                If MainClass.ValidateWithMasterTable(txtAuthority.Text, "USER_ID", "EMP_NAME", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtAuthorityName.Text = MasterNo
                End If

                'If MainClass.ValidateWithMasterTable((txtEmp.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '    txtEmpName.Text = MasterNo
                'End If

                If MainClass.ValidateWithMasterTable(txtEmp.Text, "USER_ID", "EMP_NAME", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtEmpName.Text = MasterNo
                End If

                If MainClass.ValidateWithMasterTable((txtSuppcode.Text), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
                    txtSuppName.Text = MasterNo
                End If

                mDivisionCode = IIf(IsDBNull(.Fields("DIV_CODE").Value), -1, .Fields("DIV_CODE").Value)

                If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionDesc = Trim(MasterNo)
                    cboDivision.Text = mDivisionDesc
                End If

                txtBillTo.Text = IIf(IsDBNull(RsReqMain.Fields("BILL_TO_LOC_ID").Value), "", RsReqMain.Fields("BILL_TO_LOC_ID").Value)
                txtBillTo.Enabled = True

                lblAddUser.Text = IIf(IsDBNull(.Fields("ADDUSER").Value), "", .Fields("ADDUSER").Value)
                lblAddDate.Text = VB6.Format(IIf(IsDBNull(.Fields("ADDDATE").Value), "", .Fields("ADDDATE").Value), "dd/MM/yyyy")
                lblModUser.Text = IIf(IsDBNull(.Fields("MODUSER").Value), "", .Fields("MODUSER").Value)
                lblModDate.Text = VB6.Format(IIf(IsDBNull(.Fields("MODDATE").Value), "", .Fields("MODDATE").Value), "dd/MM/yyyy")

                'txtQty.Text = IIf(IsDbNull(.Fields("INWARD_ITEM_QTY").Value) Or .Fields("INWARD_ITEM_QTY").Value = 0, "", .Fields("INWARD_ITEM_QTY").Value)


                Call ShowDetail1(.Fields("AUTO_KEY_RGPSLIP").Value, mDivisionCode)
                Call ShowOutDetail()
            End If
        End With
        ADDMode = False
        MODIFYMode = False

        MainClass.ButtonStatus(Me, XRIGHT, RsReqMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

        '    FormatSprdMain -1

        SprdMain.Enabled = True
        txtSlipno.Enabled = True
        cmdSearch.Enabled = True
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub
    Private Sub ShowOutDetail()

        On Error GoTo ShowSerialNoErr
        Dim RsSRLNo As ADODB.Recordset
        Dim SqlStr As String = ""

        Call DelTemp_OUTDetail()

        SqlStr = ""
        SqlStr = "INSERT INTO TEMP_RGP_OUT_DET ( " & vbCrLf & " USERID, COMPANY_CODE, AUTO_KEY_RGPSLIP, TRN_SERIAL_NO, " & vbCrLf & " SERIAL_NO, ITEM_CODE, IN_ITEM_CODE, " & vbCrLf & " GROSS_WT, NET_WT,SCRAP_WT,IN_QTY,TOTAL_IN_WT )" & vbCrLf & " SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "', COMPANY_CODE, " & vbCrLf & " AUTO_KEY_RGPSLIP, TRN_SERIAL_NO, " & vbCrLf & " SERIAL_NO, ITEM_CODE, IN_ITEM_CODE, " & vbCrLf & " GROSS_WT, NET_WT,SCRAP_WT,IN_QTY,TOTAL_IN_WT " & vbCrLf & " FROM INV_RGP_OUT_DET " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND AUTO_KEY_RGPSLIP=" & Val(lblMKey.Text) & " " & vbCrLf & " ORDER BY SERIAL_NO"

        PubDBCn.Execute(SqlStr)

        Exit Sub
ShowSerialNoErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub DelTemp_OUTDetail(Optional ByRef mRefNo As String = "", Optional ByRef mItemCode As String = "")

        Dim SqlStr As String = ""

        SqlStr = ""
        SqlStr = "DELETE FROM TEMP_RGP_OUT_DET " & "WHERE UserId='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"

        If mRefNo <> "" And mItemCode <> "" Then
            SqlStr = SqlStr & "AND AUTO_KEY_RGPSLIP=" & Val(mRefNo) & "' " & "AND ITEM_CODE='" & MainClass.AllowSingleQuote(UCase(mItemCode)) & "' "
        End If
        PubDBCn.Execute(SqlStr)


    End Sub

    Private Function CheckOutDetailExists(Optional ByRef mRefNo As String = "", Optional ByRef mItemCode As String = "") As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = ""
        CheckOutDetailExists = False
        SqlStr = "SELECT * FROM TEMP_RGP_OUT_DET " & "WHERE UserId='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"

        If mRefNo <> "" And mItemCode <> "" Then
            SqlStr = SqlStr & "AND AUTO_KEY_RGPSLIP=" & Val(mRefNo) & "' " & "AND ITEM_CODE='" & MainClass.AllowSingleQuote(UCase(mItemCode)) & "' "
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            CheckOutDetailExists = True
        End If

        Exit Function
ErrPart:
        CheckOutDetailExists = True
    End Function


    Private Function CheckINItemPOQty(ByRef mRow As Integer, ByRef mItemCode As String, ByRef mPONo As Double) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mInItemCode As String
        Dim mPOQty As Double
        Dim mSendRGPQty As Double
        Dim mReqQty As Double

        SqlStr = ""

        CheckINItemPOQty = True

        SqlStr = " SELECT " & vbCrLf & " SERIAL_NO, ITEM_CODE, IN_ITEM_CODE, " & vbCrLf & " IN_QTY, TOTAL_IN_WT  " & vbCrLf & " FROM TEMP_RGP_OUT_DET " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf _
            & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'" & vbCrLf & " AND TRN_SERIAL_NO=" & Val(CStr(mRow)) & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mInItemCode = Trim(IIf(IsDBNull(RsTemp.Fields("IN_ITEM_CODE").Value), "", RsTemp.Fields("IN_ITEM_CODE").Value))
            mReqQty = IIf(IsDBNull(RsTemp.Fields("IN_QTY").Value), 0, RsTemp.Fields("IN_QTY").Value)
            If mPONo > 0 And (VB.Left(cboPurpose.Text, 1) = "B" Or VB.Left(cboPurpose.Text, 1) = "C") Then
                mPOQty = GetPOQty(mInItemCode, mPONo, "")
                If mPOQty > 0 Then
                    mSendRGPQty = IIf(cboStatus.SelectedIndex = 0, mReqQty, 0) + GetRGPQty(mItemCode, mPONo, "I", mInItemCode)
                    If mPOQty < mSendRGPQty Then
                        MsgInformation("You cann't be send more than Job Order Qty. Job Order Qty is " & mPOQty & " & You Already Send For RGP Qty " & mSendRGPQty & ".")
                        MainClass.SetFocusToCell(SprdMain, mRow, ColQty)
                        CheckINItemPOQty = False
                        Exit Function
                    End If
                End If
            End If
        End If

        Exit Function
ErrPart:
        CheckINItemPOQty = True
    End Function

    Private Function UpdateOutDetail() As Boolean
        On Error GoTo UpdateErr1
        Dim RsTemp_SRLNo As ADODB.Recordset
        Dim ii As Integer
        Dim SqlStr As String = ""
        Dim mItemCode As String


        With SprdMain
            For ii = 1 To .MaxRows - 1
                .Row = ii
                .Col = ColItemCode
                mItemCode = Trim(.Text)
                SqlStr = "INSERT INTO INV_RGP_OUT_DET (" & vbCrLf & " COMPANY_CODE, AUTO_KEY_RGPSLIP, TRN_SERIAL_NO, " & vbCrLf & " SERIAL_NO, ITEM_CODE, IN_ITEM_CODE, " & vbCrLf & " GROSS_WT, NET_WT,SCRAP_WT,IN_QTY,TOTAL_IN_WT )" & vbCrLf & " SELECT " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " " & Val(txtSlipno.Text) & ", TRN_SERIAL_NO, " & vbCrLf & " SERIAL_NO, ITEM_CODE, IN_ITEM_CODE, " & vbCrLf & " GROSS_WT, NET_WT,SCRAP_WT,IN_QTY,TOTAL_IN_WT  " & vbCrLf & " FROM TEMP_RGP_OUT_DET " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'" & vbCrLf & " AND TRN_SERIAL_NO=" & Val(CStr(ii)) & ""

                PubDBCn.Execute(SqlStr)
            Next
        End With

        UpdateOutDetail = True
        Exit Function
UpdateErr1:
        UpdateOutDetail = False
        If Err.Description <> "" Then
            MsgBox(Err.Description)
        End If
    End Function

    Private Sub ShowDetail1(ByVal pReqNum As Double, ByVal mDivisionCode As Double)

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mItemUOM As String = ""
        Dim mStkType As String
        Dim mQty As Double
        Dim mF4No As String
        Dim mRemarks As String
        Dim pMainItemCode As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mInConUnit As Double
        Dim mOutConUnit As Double
        Dim mIsManyIn As Boolean
        Dim mLotNo As String
        Dim mJobOrderNo As String
        Dim mHeatNo As String


        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM INV_RGP_SLIP_DET  " & vbCrLf & " Where AUTO_KEY_RGPSLIP = " & Val(CStr(pReqNum)) & "" & vbCrLf & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsReqDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsReqDetail
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            I = 1
            .MoveFirst()

            Do While Not .EOF

                SprdMain.Row = I

                SprdMain.Col = ColItemCode
                mItemCode = IIf(IsDBNull(.Fields("FROM_ITEM_CODE").Value), "", .Fields("FROM_ITEM_CODE").Value)
                SprdMain.Text = mItemCode

                SprdMain.Col = ColItemName
                MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mItemDesc = MasterNo
                SprdMain.Text = mItemDesc

                ''mukul
                SprdMain.Col = ColItemDesc
                If String.IsNullOrEmpty(mItemCode) Then
                    mItemDesc = ""
                Else

                    mItemDesc = GetItemDescription(mItemCode)
                End If
                SprdMain.Text = mItemDesc

                SprdMain.Col = ColUom
                mItemUOM = IIf(IsDBNull(.Fields("FROM_ITEM_UOM").Value), "", .Fields("FROM_ITEM_UOM").Value)
                SprdMain.Text = mItemUOM

                SprdMain.Col = ColLotNo
                mLotNo = IIf(IsDBNull(.Fields("LOT_NO").Value), "", .Fields("LOT_NO").Value)
                SprdMain.Text = mLotNo
                'mLotNo = IIf(IsDbNull(.Fields("LOT_NO").Value), "", .Fields("LOT_NO").Value)

                SprdMain.Col = ColHeatNo
                SprdMain.Text = IIf(IsDBNull(.Fields("HEAT_NO").Value), "", .Fields("HEAT_NO").Value)
                mHeatNo = IIf(IsDBNull(.Fields("HEAT_NO").Value), "", .Fields("HEAT_NO").Value)

                SprdMain.Col = ColBatchNo
                SprdMain.Text = IIf(IsDBNull(.Fields("BATCH_NO").Value), "", .Fields("BATCH_NO").Value)

                SprdMain.Col = ColStockType
                mStkType = IIf(IsDBNull(.Fields("STOCK_TYPE").Value), "", .Fields("STOCK_TYPE").Value)
                SprdMain.Text = mStkType

                SprdMain.Col = ColStockBal
                SprdMain.Text = CStr(GetBalanceStockQty(mItemCode, (txtDate.Text), mItemUOM, "STR", mStkType, mLotNo, ConWH, mDivisionCode,,,,, mHeatNo))

                SprdMain.Col = ColQty
                mQty = IIf(IsDBNull(.Fields("ITEM_QTY").Value), 0, .Fields("ITEM_QTY").Value)
                SprdMain.Text = VB6.Format(mQty, "0.000")

                SprdMain.Col = ColQtyKGs
                SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_QTY_KGS").Value), 0, .Fields("ITEM_QTY_KGS").Value)

                SprdMain.Col = ColF4No
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("F4No").Value), "", .Fields("F4No").Value))
                mF4No = Trim(SprdMain.Text)

                SprdMain.Col = ColRemarks
                mRemarks = IIf(IsDBNull(.Fields("REMARKS_PURPOSE").Value), "", .Fields("REMARKS_PURPOSE").Value)
                SprdMain.Text = mRemarks

                SprdMain.Col = ColIncomingItemCode
                SprdMain.Text = IIf(IsDBNull(.Fields("INWARD_ITEM_CODE").Value), "", .Fields("INWARD_ITEM_CODE").Value)

                SprdMain.Col = ColJobOrderNo
                mJobOrderNo = IIf(IsDBNull(.Fields("AUTO_KEY_WO").Value), "", .Fields("AUTO_KEY_WO").Value)
                SprdMain.Text = IIf(mJobOrderNo = "0", "", mJobOrderNo)

                If Val(mF4No) <> 0 Then
                    If mItemCode = "" Then Exit Sub
                    If mF4No = "" Then Exit Sub

                    pMainItemCode = GetInJobworkItem(mItemCode, Trim(txtDate.Text), mInConUnit, mIsManyIn)

                    If pMainItemCode = "" Then
                        pMainItemCode = "('" & mItemCode & "')"
                    Else
                        pMainItemCode = "('" & mItemCode & "'," & pMainItemCode & ")"
                    End If

                    mOutConUnit = 1

                    If mIsManyIn = False Then
                        If FillREFDetail(pMainItemCode, mF4No, I, False) = False Then Exit Sub
                    End If
                    pMainItemCode = ""
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
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        AdoDCMain.Refresh
            FormatSprdView()
            SprdView.Focus()
            FraFront.SendToBack()
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraFront.BringToFront()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsReqMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub Clear1()


        lblMKey.Text = ""
        cboGatePasstype.SelectedIndex = 0
        cboDivision.Text = GetDefaultDivision()        '

        txtDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtSlipno.Text = ""
        txtDept.Text = ""
        txtEmp.Text = PubUserID
        txtEmpName.Text = ""
        If MainClass.ValidateWithMasterTable(txtEmp.Text, "USER_ID", "EMP_NAME", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtEmpName.Text = MasterNo
        End If
        txtPrepareBy.Text = PubUserID
        txtPrepareBy.Enabled = False
        txtSuppcode.Text = ""
        txtBearername.Text = ""
        cboStatus.SelectedIndex = 0
        txtReturnDate.Text = ""
        txtVehicle.Text = ""
        txtAuthority.Text = PubUserID
        txtSuppName.Text = ""
        'txtAuthorityName.Text = PubUserID
        If MainClass.ValidateWithMasterTable(txtAuthority.Text, "USER_ID", "EMP_NAME", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtAuthorityName.Text = MasterNo
        End If

        txtDeptName.Text = ""
        'optMaterial(0).Checked = True
        txtSuppcode.Enabled = True
        cmdSupplierSearch.Enabled = True
        cboStatus.Enabled = False
        chkPaintF4.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkNRGPApproved.CheckState = System.Windows.Forms.CheckState.Unchecked
        'txtItemCode.Text = ""
        'txtItemDesc.Text = ""
        'txtQty.Text = ""
        txtAddress.Text = ""
        'cmdItemCode.Enabled = False ''True
        'txtItemCode.Enabled = False ''True
        'txtQty.Enabled = False ''True

        txtBillTo.Text = ""
        txtBillTo.Enabled = False

        lblAddUser.Text = ""
        lblAddDate.Text = ""
        lblModUser.Text = ""
        lblModDate.Text = ""

        '    chkissue.Enabled = False
        Call DelTemp_OUTDetail()
        txtDate.Enabled = IIf(PubATHUSER = True, True, False)
        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsReqMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub FrmRGPSlip_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub FrmRGPSlip_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Public Sub FrmRGPSlip_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        xMyMenu = myMenu
        MainClass.RightsToButton(Me, XRIGHT)

        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False

        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        ''Me.Width = VB6.TwipsToPixelsX(11355)

        'AdoDCMain.Visible = False
        FillCboStatus()
        txtSlipno.Enabled = True
        FormActive = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol

        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemName Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemName, 0))

        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColIncomingItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColIncomingItemCode, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColJobOrderNo Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColJobOrderNo, 0))
        SprdMain.Refresh()
    End Sub
    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        With SprdMain
            '        SprdMain_LeaveCell .ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False
        End With

    End Sub
    Private Sub txtAuthority_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAuthority.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtAuthority_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAuthority.DoubleClick
        Call cmdAuthSearch_Click(cmdAuthSearch, New System.EventArgs())
    End Sub
    Private Sub txtAuthority_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAuthority.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtAuthority.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtAuthority_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtAuthority.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtAuthority_DoubleClick(txtAuthority, New System.EventArgs())
    End Sub

    Private Sub txtAuthority_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAuthority.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        If Trim(txtAuthority.Text) = "" Then GoTo EventExitSub

        'txtAuthority.Text = VB6.Format(txtAuthority.Text, "000000")

        If MainClass.ValidateWithMasterTable((txtAuthority.Text), "USER_ID", "EMP_NAME", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtAuthorityName.Text = MasterNo
        Else
            MsgInformation("Invalid USER ID")
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtBearername_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBearername.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBearername_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBearername.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtBearername.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEmp_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmp.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtEmp_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmp.DoubleClick
        Call cmdEmpSearch_Click(cmdEmpSearch, New System.EventArgs())
    End Sub

    Private Sub txtEmp_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEmp.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtEmp.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEmp_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtEmp.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtEmp_DoubleClick(txtEmp, New System.EventArgs())
    End Sub

    Private Sub txtEmp_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmp.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        If Trim(txtEmp.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtEmp.Text, "USER_ID", "EMP_NAME", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtEmpName.Text = MasterNo
        Else
            MsgInformation("Invalid USer ID")
            Cancel = True
        End If

        'txtEmp.Text = VB6.Format(txtEmp.Text, "000000")
        'SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND (EMP_LEAVE_DATE IS NULL OR (EMP_LEAVE_DATE >=TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')))"

        'SqlStr = SqlStr & vbCrLf & " AND EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'"

        'SqlStr = SqlStr & vbCrLf & " AND EMP_CATG IN ('D','G','P','S')"

        'If MainClass.ValidateWithMasterTable(txtEmp.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = True Then
        '    txtEmpName.Text = MasterNo
        'Else
        '    MsgInformation("Invalid Employee Code")
        '    Cancel = True
        'End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub cmdEmpSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdEmpSearch.Click

        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster(txtEmp.Text, "ATH_PASSWORD_MST", "USER_ID", "EMP_NAME", , , SqlStr) = True Then
            txtEmp.Text = AcName
            txtEmpName.Text = AcName1
            txtEmp_Validating(txtEmp, New System.ComponentModel.CancelEventArgs(False))
            If txtEmp.Enabled = True Then txtEmp.Focus()
        End If

        'If ADDMode = True Then
        '    SqlStr = SqlStr & " AND (EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE='')"
        'Else
        '    SqlStr = SqlStr & vbCrLf & " AND (EMP_LEAVE_DATE IS NULL OR (EMP_LEAVE_DATE >= TO_DATE('" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')))"
        'End If

        'SqlStr = SqlStr & vbCrLf & " AND EMP_DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'"

        'SqlStr = SqlStr & vbCrLf & " AND EMP_CATG IN ('D','G','P','S')"

        'If MainClass.SearchGridMaster(txtEmp.Text, "PAY_EMPLOYEE_MST", "EMP_CODE", "EMP_NAME", , , SqlStr) = True Then
        '    txtEmp.Text = AcName
        '    txtEmpName.Text = AcName1
        '    txtEmp_Validating(txtEmp, New System.ComponentModel.CancelEventArgs(False))
        '    If txtEmp.Enabled = True Then txtEmp.Focus()
        'End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtReturnDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtReturnDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtReturnDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtReturnDate.Text) Then
            MsgInformation("Invalid Return Date")
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSuppcode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSuppcode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSuppcode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSuppcode.DoubleClick
        Call cmdSupplierSearch_Click(cmdSupplierSearch, New System.EventArgs())
    End Sub

    Private Sub txtSuppcode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSuppcode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSuppcode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSuppcode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSuppcode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtSuppcode_DoubleClick(txtSuppcode, New System.EventArgs())
    End Sub

    Private Sub txtSuppcode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSuppcode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mAddress As String

        If Trim(txtSuppcode.Text) = "" Then GoTo EventExitSub

        SqlStr = " SELECT SUPP_CUST_NAME, SUPP_CUST_ADDR, SUPP_CUST_CITY, " & vbCrLf _
            & " SUPP_CUST_STATE, SUPP_CUST_PIN " & vbCrLf _
            & " FROM FIN_SUPP_CUST_MST " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUPP_CUST_TYPE IN ('S','C')  AND SUPP_CUST_CODE NOT IN ('" & IIf(IsDBNull(RsCompany.Fields("COMP_AC_CODE").Value), "", RsCompany.Fields("COMP_AC_CODE").Value) & "')" & vbCrLf _
            & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote((txtSuppcode.Text)) & "'"

        If ADDMode = True Then
            SqlStr = SqlStr & "  AND STATUS='O'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            txtSuppName.Text = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
        Else
            MsgInformation("Invalid Supplier Code")
            txtSuppName.Text = ""
            txtAddress.Text = ""
            Cancel = True
        End If

        If Trim(txtBillTo.Text) = "" Then
            txtBillTo.Text = GetDefaultLocation(txtSuppcode.Text)
        End If

        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtDate.Text) Then
            MsgInformation("Invalid Date")
            Cancel = True
            GoTo EventExitSub
        End If

        If CDate(txtDate.Text) > CDate(PubCurrDate) Then
            MsgInformation("Invaild Date. Date cann't be Greater Then Current date.")
            If txtDate.Enabled Then txtDate.Focus()
            Cancel = True
            GoTo EventExitSub
        End If

        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtDept_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.DoubleClick
        Call cmdDeptSearch_Click(cmdDeptSearch, New System.EventArgs())
    End Sub

    Private Sub txtDept_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDept.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDept.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDept_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDept.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then TxtDept_DoubleClick(TxtDept, New System.EventArgs())
    End Sub

    Private Sub txtDept_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDept.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        If Trim(txtDept.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable((txtDept.Text), "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtDeptName.Text = MasterNo
        Else
            MsgInformation("Invalid Depatment Code")
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtReturnDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtReturnDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtSlipNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSlipno.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtSlipNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSlipno.DoubleClick
        Call cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Sub txtSlipNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSlipno.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSlipno.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSlipNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSlipno.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Public Sub txtSlipNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSlipno.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mReqnum As String = ""

        If Trim(txtSlipno.Text) = "" Then GoTo EventExitSub

        If Len(txtSlipno.Text) < 6 Then
            txtSlipno.Text = Val(txtSlipno.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        If MODIFYMode = True And RsReqMain.EOF = False Then mReqnum = RsReqMain.Fields("AUTO_KEY_RGPSLIP").Value

        SqlStr = "Select * From INV_RGP_SLIP_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND LTRIM(RTRIM(AUTO_KEY_RGPSLIP))=" & Val(txtSlipno.Text) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsReqMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsReqMain.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Item Consumption, Use Generate Item Consumption Option To add", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * From INV_RGP_SLIP_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " LTRIM(RTRIM(AUTO_KEY_ISS))=" & Val(mReqnum) & ""

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsReqMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtBillTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBillTo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtBillTo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBillTo.DoubleClick
        Call BillToSearch()
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call BillToSearch()
    End Sub
    Private Sub txtBillTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBillTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim xAcctCode As String
        Dim RsTemp As ADODB.Recordset
        Dim mAddress As String

        If Trim(txtSuppcode.Text) = "" Then GoTo EventExitSub
        If Trim(txtBillTo.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(Trim(txtSuppcode.Text), "SUPP_CUST_CODE", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
            xAcctCode = MasterNo
        Else
            MsgBox("Invalid Customer Name.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If
        ''SUPP_CUST_TYPE IN ('S','C')

        SqlStr = " SELECT SUPP_CUST_NAME, SUPP_CUST_ADDR, SUPP_CUST_CITY, " & vbCrLf _
            & " SUPP_CUST_STATE, SUPP_CUST_PIN " & vbCrLf _
            & " FROM FIN_SUPP_CUST_BUSINESS_MST " & vbCrLf _
            & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND " & vbCrLf _
            & " SUPP_CUST_CODE='" & MainClass.AllowSingleQuote((txtSuppcode.Text)) & "'" & vbCrLf _
            & " AND LOCATION_ID='" & MainClass.AllowSingleQuote((txtBillTo.Text)) & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            mAddress = Trim(IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_ADDR").Value), "", RsTemp.Fields("SUPP_CUST_ADDR").Value))
            mAddress = mAddress & ", " & IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value)
            mAddress = mAddress & ", " & IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_STATE").Value), "", RsTemp.Fields("SUPP_CUST_STATE").Value)

            txtAddress.Text = mAddress
        Else
            MsgInformation("Invalid Location Id for such Customer")

            txtAddress.Text = ""
            Cancel = True
        End If


        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub BillToSearch()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        Dim xAcctCode As String = ""

        If Trim(txtSuppcode.Text) = "" Then
            MsgInformation("Please select the Customer First")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(txtSuppcode.Text), "SUPP_CUST_CODE", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
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

    Private Sub FrmRGPSlip_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 210, mReFormWidth - 210, mReFormWidth))
        'UltraGrid2.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 210, mReFormWidth - 210, mReFormWidth))
        Frame6.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        FraFront.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        SprdView.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))

        CurrFormWidth = mReFormWidth

        'MainClass.SetSpreadColor(SprdMain, -1)
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

    Private Sub cboGatePasstype_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboGatePasstype.SelectedIndexChanged
        If FormActive = False Then Exit Sub
        Call FormatSprdMain(-1)
    End Sub
End Class
