Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmSRQCEntry
    Inherits System.Windows.Forms.Form
    Dim RsSRMain As ADODB.Recordset
    Dim RsSRDetail As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection	
    Dim pQCDate As String
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim pXRIGHT As String
    Dim xMyMenu As String

    Dim FormActive As Boolean
    Dim mCurRowNo As Integer


    Dim mSupplierCode As String


    Private Const ConRowHeight As Short = 12

    Private Const ColPONo As Short = 1
    Private Const ColPODate As Short = 2
    Private Const ColItemCode As Short = 3
    Private Const ColItemDesc As Short = 4
    Private Const ColUnit As Short = 5
    Private Const ColBillQty As Short = 6
    Private Const ColReceivedQty As Short = 7
    Private Const ColAcceptQty As Short = 8
    Private Const ColStockType As Short = 9
    Private Const ColReason As Short = 10
    Private Const ColAction As Short = 11
    Private Const ColRectfiedQty As Short = 12
    Private Const ColScrapQty As Short = 13
    Private Const ColDeptCode As Short = 14
    Private Const ColCompleteDate As Short = 15
    Private Const ColSupplier As Short = 16
    Private Const ColBOPItem As Short = 17

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer


    Private Sub chkQC_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkQC.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click

        On Error GoTo AddErr
        If cmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()

            SprdMain.Enabled = True

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
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
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

        mLockBookCode = CInt(ConLockMRRQC)


        If ValidateBookLocking(PubDBCn, mLockBookCode, txtMRRDate.Text) = True Then
            Exit Sub
        End If

        If ValidateAccountLocking(PubDBCn, txtBillDate.Text, (txtSupplier.Text), mSupplierCode) = True Then
            Exit Sub
        End If

        If PubSuperUser <> "S" Then
            MsgInformation("You have no Rigths to Delete MRR.")
            Exit Sub
        End If

        If Trim(txtRefNo.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub


        If CheckBillPayment(mSupplierCode, (txtBillNo.Text), "B", (txtBillDate.Text)) = True Then Exit Sub

        If Not RsSRMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.	
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "PRD_SALERETURN_HDR", (txtRefNo.Text), RsSRMain, "AUTO_KEY_REF") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "PRD_SALERETURN_HDR", "AUTO_KEY_REF", (lblMKey.Text)) = False Then GoTo DelErrPart

                If DeleteCRTRN(PubDBCn, ConStockRefType_MRR, (txtRefNo.Text)) = False Then GoTo DelErrPart

                '            If DeleteStockTRN(PubDBCn, ConStockRefType_MRR, txtMRRNo.Text) = False Then GoTo DelErrPart	

                PubDBCn.Execute("Delete from PRD_SALERETURN_DET Where AUTO_KEY_REF=" & Val(lblMKey.Text) & "")
                PubDBCn.Execute("Delete from PRD_SALERETURN_HDR Where AUTO_KEY_REF=" & Val(lblMKey.Text) & "")



                PubDBCn.CommitTrans()
                RsSRMain.Requery() ''.Refresh	
                RsSRDetail.Requery() ''.Refresh	

                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans() ''	
        RsSRMain.Requery() ''.Refresh	
        RsSRDetail.Requery() ''.Refresh	

        If Err.Description <> "" Then
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        '        Resume	
    End Sub


    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdModify.Click

        On Error GoTo ModifyErr

        If cmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsSRMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True

            txtMRRNo.Enabled = False
            cmdMRRSearch.Enabled = False

            txtRefNo.Enabled = False
            cmdSearchRef.Enabled = False
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


        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_MRR,LENGTH(AUTO_KEY_MRR)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND QC_STATUS='N' AND MRR_STATUS='N' AND REF_TYPE IN ('I','2')" & vbCrLf _
            & " AND AUTO_KEY_MRR NOT IN ( " & vbCrLf _
            & " SELECT AUTO_KEY_MRR FROM PRD_SALERETURN_HDR" & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & ")" & vbCrLf _
            & " AND DIV_CODE IN ( " & vbCrLf _
            & " SELECT DIV_CODE FROM INV_DIVISION_MST" & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " AND IS_WAREHOUSE_DIV='N')"

        If MainClass.SearchGridMaster(txtMRRNo.Text, "INV_GATE_HDR", "AUTO_KEY_MRR", "MRR_DATE", "SUPP_CUST_CODE", , SqlStr) = True Then
            txtMRRNo.Text = AcName
            'TxtMRRNo_Validate(False)
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
    Private Sub ReportONMRR(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim mTitle As String
        Dim mSubTitle As String
        Dim SqlStr As String = ""
        Dim mRptFileName As String
        Dim mVNo As String

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        SqlStr = ""

        Call MainClass.ClearCRptFormulas(Report1)

        Call SelectQryForMRR(SqlStr)


        mTitle = "Material Receipt Report - Sales Return"
        mSubTitle = ""
        mRptFileName = "MRR.rpt"

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Function SelectQryForMRR(ByRef mSqlStr As String) As String

        ''SELECT CLAUSE...	

        mSqlStr = " SELECT " & vbCrLf & " IH.*, ID.*,INVMST.ITEM_SHORT_DESC,"

        mSqlStr = mSqlStr & " CMST.SUPP_CUST_NAME, CMST.SUPP_CUST_ADDR, " & vbCrLf _
            & " CMST.SUPP_CUST_CITY, CMST.SUPP_CUST_STATE, " & vbCrLf _
            & " CMST.SUPP_CUST_PIN, CMST.SUPP_CUST_PHONE, " & vbCrLf _
            & " CMST.SUPP_CUST_FAXNO, CMST.SUPP_CUST_MAILID, " & vbCrLf _
            & " CMST.SUPP_CUST_MOBILE, CMST.CST_NO, " & vbCrLf _
            & " CMST.LST_NO, PREBY.EMP_NAME"

        ''FROM CLAUSE...	
        mSqlStr = mSqlStr & vbCrLf _
        & " FROM INV_GATE_HDR IH, INV_GATE_DET ID, " & vbCrLf _
        & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST,PAY_EMPLOYEE_MST PREBY"

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then
            mSqlStr = mSqlStr & vbCrLf & ", INV_GENERAL_MST GMST"
        End If

        ''WHERE CLAUSE...	
        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf _
            & " CMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND INVMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.AUTO_KEY_MRR=ID.AUTO_KEY_MRR" & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND ID.COMPANY_CODE=PREBY.COMPANY_CODE(+)" & vbCrLf _
            & " AND ID.QC_EMP_CODE=PREBY.EMP_CODE(+)" & vbCrLf _
            & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & "" ''& vbCrLf |            & " AND IH.QC_STATUS='Y'"	

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 101 Then

            mSqlStr = mSqlStr & vbCrLf _
               & " AND IH.COMPANY_CODE=GMST.COMPANY_CODE" & vbCrLf _
               & " AND INVMST.CATEGORY_CODE=GMST.GEN_CODE AND GMST.GEN_TYPE='C'" & vbCrLf

        End If

        ''ORDER CLAUSE...	

        mSqlStr = mSqlStr & vbCrLf & "ORDER BY ID.SERIAL_NO"

        SelectQryForMRR = mSqlStr
    End Function


    Private Sub cmdResetMRR_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdResetMRR.Click

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsResetGateMain As ADODB.Recordset

        SqlStr = " SELECT * FROM INV_GATE_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_MRR,LENGTH(AUTO_KEY_MRR)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsResetGateMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsResetGateMain.EOF = False Then
            Call ShowResetMRREntry(RsResetGateMain)
        Else
            MsgBox("No Such Gate Entry.", MsgBoxStyle.Information)
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub ShowResetMRREntry(ByRef mRsGate As ADODB.Recordset)

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim mMode As String
        Dim mDivisionCode As Integer
        Dim mShippedToCode As String
        Dim mShippedToName As String

        With mRsGate
            If Not .EOF Then

                '            mDivision = ""	
                txtMRRNo.Text = IIf(IsDbNull(.Fields("AUTO_KEY_MRR").Value), "", .Fields("AUTO_KEY_MRR").Value)
                txtMRRDate.Text = VB6.Format(IIf(IsDbNull(.Fields("MRR_DATE").Value), "", .Fields("MRR_DATE").Value), "DD/MM/YYYY")

                mSupplierCode = .Fields("SUPP_CUST_CODE").Value
                If MainClass.ValidateWithMasterTable(.Fields("SUPP_CUST_CODE").Value, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    TxtSupplier.Text = MasterNo
                End If


                lblRefType.Text = IIf(IsDbNull(.Fields("REF_TYPE").Value), "", .Fields("REF_TYPE").Value)

                txtBillNo.Text = IIf(IsDbNull(.Fields("BILL_NO").Value), "", .Fields("BILL_NO").Value)
                txtBillDate.Text = VB6.Format(IIf(IsDbNull(.Fields("BILL_DATE").Value), "", .Fields("BILL_DATE").Value), "DD/MM/YYYY")

                txtRemarks.Text = "" '' IIf(IsNull(!REMARKS), "", !REMARKS)	
                txtEmp.Text = ""

                lblDivisionCode.Text = IIf(IsDbNull(.Fields("DIV_CODE").Value), "", .Fields("DIV_CODE").Value)
                mDivisionCode = IIf(IsDbNull(.Fields("DIV_CODE").Value), "", .Fields("DIV_CODE").Value)


                lblDivisionName.Text = ""
                If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    lblDivisionName.text = MasterNo
                End If

                MainClass.ClearGrid(SprdMain)
                Call FormatSprdMain(-1)

                Call ShowDetailFromMRREntry((txtMRRNo.Text), (.Fields("DIV_CODE").Value))
            End If
        End With

        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume	
    End Sub
    Private Sub ShowDetailFromMRREntry(ByRef mMKEY As String, ByRef mDivisionCode As Double)

        On Error GoTo ERR1
        Dim i As Integer
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
        Dim RsGateDetail As ADODB.Recordset
        Dim mQCEmpCode As String
        Dim pSupplierCode As String


        pSupplierCode = ""

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM INV_GATE_DET " & vbCrLf & " Where AUTO_KEY_MRR=" & Val(mMKEY) & "" & vbCrLf & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGateDetail, ADODB.LockTypeEnum.adLockReadOnly)

        With RsGateDetail
            If .EOF = True Then Exit Sub
            i = 1
            .MoveFirst()

            Do While Not .EOF

                SprdMain.Row = i

                SprdMain.Col = ColPONo
                mRefPoNo = (IIf(IsDbNull(.Fields("REF_AUTO_KEY_NO").Value), -1, .Fields("REF_AUTO_KEY_NO").Value))
                SprdMain.Text = mRefPoNo

                SprdMain.Col = ColPODate
                SprdMain.Text = IIf(IsDbNull(.Fields("REF_DATE").Value), "", .Fields("REF_DATE").Value)

                SprdMain.Col = ColItemCode
                mItemCode = Trim(IIf(IsDbNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))
                SprdMain.Text = Trim(mItemCode)

                SprdMain.Col = ColItemDesc
                MainClass.ValidateWithMasterTable(Trim(mItemCode), "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mItemDesc = MasterNo
                SprdMain.Text = mItemDesc

                SprdMain.Col = ColUnit
                SprdMain.Text = IIf(IsDbNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)

                SprdMain.Row = i
                SprdMain.Col = ColBillQty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("BILL_QTY").Value), 0, .Fields("BILL_QTY").Value)))

                SprdMain.Col = ColReceivedQty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("RECEIVED_QTY").Value), 0, .Fields("RECEIVED_QTY").Value)))

                SprdMain.Col = ColAcceptQty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("LOT_ACCEPT").Value), 0, .Fields("LOT_ACCEPT").Value)))

                '            SprdMain.Col = ColApprovedQty	
                '            SprdMain.Text = Val(IIf(IsNull(!APPROVED_QTY), 0, !APPROVED_QTY))	

                SprdMain.Col = ColStockType
                SprdMain.Text = "QC"

                SprdMain.Col = ColDeptCode
                SprdMain.Text = ""

                .MoveNext()

                i = i + 1
                SprdMain.MaxRows = i
            Loop
        End With
        FormatSprdMain(-1)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume	
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If


        If UpdateMain1 = True Then
            ADDMode = False
            MODIFYMode = False
            txtRefNo_Validating(txtRefNo, New System.ComponentModel.CancelEventArgs(False))

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

    Private Sub cmdSearchRef_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchRef.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUBSTR(AUTO_KEY_REF,LENGTH(AUTO_KEY_REF)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        If MainClass.SearchGridMaster(txtRefNo.Text, "PRD_SALERETURN_HDR", "AUTO_KEY_REF", "REF_DATE", , , SqlStr) = True Then
            txtRefNo.Text = AcName
            'txtRefNo_Validate(False)
            txtRefNo_Validating(txtRefNo, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim xIName As String
        Dim xPoNo As String
        Dim SqlStr As String = ""
        Dim xSuppCode As String
        Dim xRefNo As String
        Dim xRGPCode As String
        Dim xItemCode As String
        Dim mCT3No As Integer
        Dim mFromMRRDate As String
        Dim mSupplierName As String
        Dim mBOPItem As String

        SprdMain.Row = SprdMain.ActiveRow
        SprdMain.Col = ColPONo
        xRefNo = Trim(SprdMain.Text)


        If eventArgs.row = 0 And eventArgs.col = ColStockType Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColStockType
                If MainClass.SearchGridMaster(.Text, "INV_TYPE_MST", "STOCK_TYPE_CODE", "STOCK_TYPE_DESC", , , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND STOCK_TYPE_CODE='CR'") = True Then
                    .Row = .ActiveRow
                    .Col = ColStockType
                    .Text = AcName
                End If
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColSupplier Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColSupplier
                If MainClass.SearchGridMaster(.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
                    .Row = .ActiveRow
                    .Col = ColSupplier
                    .Text = AcName
                End If
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColDeptCode Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColSupplier
                If MainClass.SearchGridMaster(.Text, "PAY_DEPT_MST", "DEPT_CODE", "DEPT_DESC", , , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
                    .Row = .ActiveRow
                    .Col = ColDeptCode
                    .Text = AcName
                End If
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColBOPItem Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColSupplier
                mSupplierName = Trim(.Text)

                .Col = ColBOPItem
                mBOPItem = Trim(.Text)

                SqlStr = " SELECT INVMST.ITEM_SHORT_DESC, INVMST.ITEM_CODE" & vbCrLf & " FROM FIN_SUPP_CUST_MST CMST, FIN_SUPP_CUST_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE CMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CMST.COMPANY_CODE = ID.COMPANY_CODE" & vbCrLf & " AND CMST.SUPP_CUST_CODE = ID.SUPP_CUST_CODE" & vbCrLf & " AND ID.COMPANY_CODE = INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE = INVMST.ITEM_CODE"

                If mSupplierName <> "" Then
                    SqlStr = SqlStr & vbCrLf & " AND CMST.SUPP_CUST_NAME = '" & MainClass.AllowSingleQuote(mSupplierName) & "'"
                End If

                SqlStr = SqlStr & vbCrLf & " ORDER BY 1"

                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColBOPItem
                    .Text = AcName
                End If
            End With
        End If


        '    If eventArgs.Col = 0 And eventArgs.Row > 0 Then	
        '        SprdMain.Row = Row	
        '        SprdMain.Col = ColPONo	
        '        If Row < SprdMain.MaxRows And (ADDMode = True Or MODIFYMode = True) Then	
        '            Dim mPONo As String	
        '            Dim mItemCode As String	
        '            Dim DelStatus As Boolean	
        '	
        '            mPONo = SprdMain.Text	
        '	
        '            SprdMain.Col = ColItemCode	
        '            mItemCode = SprdMain.Text	
        '	
        '            MainClass.DeleteSprdRow SprdMain, Row, ColPONo, DelStatus	
        '            FormatSprdMain -1	
        '            MainClass.SaveStatus Me, ADDMode, MODIFYMode	
        '        End If	
        '    End If	
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        Dim xPoNo As String
        Dim xICode As String
        Dim mQty As Double
        Dim mAcceptQty As Double

        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xRGPItemCode As String
        Dim mRow As Integer
        Dim mDivisionCode As Double
        Dim xSqlStr As String

        If eventArgs.NewRow = -1 Then Exit Sub

        SprdMain.Row = eventArgs.row
        SprdMain.Col = ColPONo
        xPoNo = SprdMain.Text
        mRow = eventArgs.row
        Select Case eventArgs.col

            Case ColBillQty
                SprdMain.Row = mRow
                SprdMain.Col = ColPONo
                xPoNo = SprdMain.Text


                SprdMain.Col = ColItemCode
                xICode = SprdMain.Text
                If xICode = "" Then Exit Sub



            Case ColReceivedQty
                SprdMain.Row = mRow
                SprdMain.Col = ColPONo
                xPoNo = SprdMain.Text

                SprdMain.Col = ColItemCode
                xICode = SprdMain.Text
                If xICode = "" Then Exit Sub


            Case ColAcceptQty
                SprdMain.Row = mRow
                SprdMain.Col = ColReceivedQty
                mQty = Val(SprdMain.Text)


                SprdMain.Col = ColAcceptQty
                mAcceptQty = Val(SprdMain.Text)

            Case ColStockType
                SprdMain.Row = mRow
                SprdMain.Col = ColStockType
                If Trim(SprdMain.Text) = "" Then Exit Sub

                If MainClass.ValidateWithMasterTable(SprdMain.Text, "STOCK_TYPE_CODE", "STOCK_TYPE_DESC", "INV_TYPE_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STOCK_TYPE_CODE IN ('QC','CR')") = False Then
                    MsgInformation("Invalid Stock Type")
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColStockType)
                    eventArgs.cancel = True
                    Exit Sub
                End If

            Case ColDeptCode
                SprdMain.Row = mRow
                SprdMain.Col = ColDeptCode
                If Trim(SprdMain.Text) = "" Then Exit Sub

                If MainClass.ValidateWithMasterTable(SprdMain.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                    MsgInformation("Invalid Dept Code")
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColDeptCode)
                    eventArgs.cancel = True
                    Exit Sub
                End If

            Case ColSupplier
                SprdMain.Row = mRow
                SprdMain.Col = ColSupplier
                If Trim(SprdMain.Text) = "" Then Exit Sub

                If MainClass.ValidateWithMasterTable(SprdMain.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                    MsgBox("Either Item Code in Invalid or not Active.", MsgBoxStyle.Information)
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColBOPItem)
                    eventArgs.cancel = True
                End If
            Case ColBOPItem

                SprdMain.Row = mRow
                SprdMain.Col = ColBOPItem
                If Trim(SprdMain.Text) = "" Then Exit Sub

                If MainClass.ValidateWithMasterTable(SprdMain.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                    MsgBox("Either Item Code in Invalid or not Active.", MsgBoxStyle.Information)
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColBOPItem)
                    eventArgs.cancel = True
                End If

        End Select

        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Function FillGridRow(ByRef mPoNO As String, ByRef mItemCode As String, ByRef mOutItemCode As String, ByRef mDivisionCode As Double) As Boolean

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""


        If mItemCode = "" Then Exit Function

        SqlStr = ""
        SqlStr = " SELECT ITEM_CODE,ITEM_SHORT_DESC AS NAME," & vbCrLf _
             & " PURCHASE_UOM " & vbCrLf _
             & " FROM INV_ITEM_MST " & vbCrLf _
             & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
             & " AND Item_Code='" & MainClass.AllowSingleQuote(UCase(mItemCode)) & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMisc.EOF = False Then
            SprdMain.Row = SprdMain.Row
            With RsMisc
                SprdMain.Col = ColItemCode
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))

                SprdMain.Col = ColItemDesc
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("Name").Value), "", .Fields("Name").Value))

                SprdMain.Col = ColUnit
                SprdMain.Text = IIf(IsDbNull(.Fields("PURCHASE_UOM").Value), "", .Fields("PURCHASE_UOM").Value)

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
    Private Sub SprdMain_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles SprdMain.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim mCancel As Boolean
        mCancel = False
        With SprdMain
            SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, mCancel))
            Cancel = mCancel
        End With
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        With SprdView
            .Row = eventArgs.Row

            .Col = 1
            txtRefNo.Text = CStr(Val(.Text))

            txtRefNo_Validating(txtRefNo, New System.ComponentModel.CancelEventArgs(False))
            CmdView_Click(CmdView, New System.EventArgs())
        End With
    End Sub
    Private Sub txtBillDate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBillDate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.CheckDateKey(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEmp_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmp.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtEmp_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmp.DoubleClick
        Call cmdSearchEmp_Click(cmdSearchEmp, New System.EventArgs())
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchEmp_Click(cmdSearchEmp, New System.EventArgs())
    End Sub

    Private Sub txtEmp_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmp.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        If Trim(txtEmp.Text) = "" Then GoTo EventExitSub

        If ADDMode = True Then
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_LEAVE_DATE IS NULL "
        Else
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If

        txtEmp.Text = VB6.Format(txtEmp.Text, "000000")
        If MainClass.ValidateWithMasterTable(txtEmp.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            lblEmp.text = MasterNo
        Else
            MsgInformation("Invalid Employee Code")
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cmdSearchEmp_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchEmp.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_LEAVE_DATE IS NULL "
        If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            txtEmp.Text = AcName1
            lblEmp.text = AcName
            If txtEmp.Enabled = True Then txtEmp.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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
        cmdMRRSearch_Click(cmdMRRSearch, New System.EventArgs())
    End Sub

    Private Sub TxtMRRNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMRRNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtRefDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRefDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRefDate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRefDate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.CheckDateKey(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRefDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRefDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If Not IsDate(txtRefDate.Text) Then
            MsgInformation("Invaild Date.")
            Cancel = True
            GoTo EventExitSub
        End If

        If FYChk((txtRefDate.Text)) = False Then
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRefNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRefNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRefNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRefNo.DoubleClick
        cmdSearchRef_Click(cmdSearchRef, New System.EventArgs())
    End Sub

    Private Sub txtRefNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRefNo.KeyPress
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
        Dim mMRRNo As String
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing


        If Trim(txtMRRNo.Text) = "" Then GoTo EventExitSub

        If Len(txtMRRNo.Text) < 6 Then
            txtMRRNo.Text = Val(txtMRRNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        mMRRNo = Trim(txtMRRNo.Text)

        If ADDMode = False Then
            txtMRRNo.Text = ""
            MsgBox("Please click add first.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

        SqlStr = " SELECT * FROM INV_GATE_HDR " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_MRR,LENGTH(AUTO_KEY_MRR)-5,4)=" & RsCompany.Fields("FYEAR").Value & "  AND REF_TYPE IN ('I','2')" & vbCrLf _
            & " AND AUTO_KEY_MRR=" & Val(mMRRNo) & " " & vbCrLf _
            & " AND AUTO_KEY_MRR NOT IN ( " & vbCrLf _
            & " SELECT AUTO_KEY_MRR FROM PRD_SALERETURN_HDR" & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & ")" & vbCrLf _
            & " AND DIV_CODE IN ( " & vbCrLf _
            & " SELECT DIV_CODE FROM INV_DIVISION_MST" & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " AND IS_WAREHOUSE_DIV='N')"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Clear1()
            Call ShowFromMRR(RsTemp)
        Else
            MsgBox("Invalid MRR No.", MsgBoxStyle.Information)
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Public Sub txtRefNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRefNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMkey As String = ""
        Dim mMRRNo As String
        Dim SqlStr As String = ""

        If Trim(txtRefNo.Text) = "" Then GoTo EventExitSub

        If Len(txtRefNo.Text) < 6 Then
            txtRefNo.Text = Val(txtRefNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        If MODIFYMode = True And RsSRMain.EOF = False Then xMkey = RsSRMain.Fields("AUTO_KEY_MRR").Value
        mMRRNo = Trim(txtRefNo.Text)

        SqlStr = " SELECT * FROM PRD_SALERETURN_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_REF,LENGTH(AUTO_KEY_REF)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_REF=" & Val(mMRRNo) & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSRMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsSRMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Ref, Use Generate Ref Option To add", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM PRD_SALERETURN_HDR " & " WHERE AUTO_KEY_REF=" & Val(xMkey) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSRMain, ADODB.LockTypeEnum.adLockReadOnly)
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
        Dim SqlStr As String = ""

        Dim mVNoSeq As Double
        Dim mSuppCustCode As String
        Dim mQCStatus As String
        Dim mEntryDate As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mEntryDate = VB6.Format(GetServerDate, "DD-MMM-YYYY") & " " & GetServerTime

        mSuppCustCode = "-1"
        If MainClass.ValidateWithMasterTable(TxtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSuppCustCode = MasterNo
        Else
            mSuppCustCode = -1
            MsgBox("Supplier Does Not Exist In Master", vbInformation)
            GoTo ErrPart
        End If

        mQCStatus = IIf(chkQC.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        If Val(txtRefNo.Text) = 0 Then
            mVNoSeq = CDbl(AutoGenSeqNo())
        Else
            mVNoSeq = Val(txtRefNo.Text)
        End If

        txtRefNo.Text = CStr(Val(CStr(mVNoSeq)))

        SqlStr = ""

        If ADDMode = True Then
            lblMKey.Text = CStr(mVNoSeq)
            SqlStr = "INSERT INTO PRD_SALERETURN_HDR( " & vbCrLf & " COMPANY_CODE, AUTO_KEY_MRR, MRR_DATE," & vbCrLf & " AUTO_KEY_REF, REF_DATE, EMP_CODE," & vbCrLf & " REMARKS, QC_DONE, MRR_REF_TYPE, " & vbCrLf & " ADDUSER, ADDDATE, MODUSER, MODDATE ) "

            SqlStr = SqlStr & vbCrLf _
                & " VALUES(" & RsCompany.Fields("COMPANY_CODE").Value & ", " & Val(txtMRRNo.Text) & ", TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " " & Val(mVNoSeq) & ", TO_DATE('" & VB6.Format(txtRefDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(txtEmp.Text) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(TxtRemarks.Text) & "','" & mQCStatus & "', '" & lblRefType.Text & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & mEntryDate & "','DD-MON-YYYY HH24:MI'),'','')"


        ElseIf MODIFYMode = True Then
            SqlStr = ""
            SqlStr = "UPDATE PRD_SALERETURN_HDR SET " & vbCrLf _
                & " AUTO_KEY_MRR =" & Val(txtMRRNo.Text) & " ," & vbCrLf _
                & " MRR_DATE=TO_DATE('" & VB6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " AUTO_KEY_REF =" & Val(mVNoSeq) & " ," & vbCrLf _
                & " REF_DATE=TO_DATE('" & VB6.Format(txtRefDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " EMP_CODE='" & MainClass.AllowSingleQuote(txtEmp.Text) & "'," & vbCrLf _
                & " REMARKS='" & MainClass.AllowSingleQuote(TxtRemarks.Text) & "'," & vbCrLf _
                & " QC_DONE = '" & mQCStatus & "', MRR_REF_TYPE= '" & lblRefType.Text & "', " & vbCrLf _
                & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " WHERE AUTO_KEY_REF =" & Val(LblMkey.Text) & ""
        End If

        PubDBCn.Execute(SqlStr)

        If UpdateDetail1(mVNoSeq) = False Then GoTo ErrPart

        If chkQC.Enabled = True Then
            SqlStr = " UPDATE INV_GATE_HDR SET QC_STATUS = '" & mQCStatus & "'," & vbCrLf _
                & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND AUTO_KEY_MRR =" & Val(txtMRRNo.Text) & ""

            PubDBCn.Execute(SqlStr)
        End If

        UpdateMain1 = True
        PubDBCn.CommitTrans()

        Exit Function
ErrPart:
        UpdateMain1 = False
        PubDBCn.RollbackTrans() ''	
        RsSRMain.Requery() ''.Refresh	
        RsSRDetail.Requery() ''.Refresh	
        If Err.Description = "" Then Exit Function
        If Err.Number = -2147217900 Then
            ErrorMsg("Duplicate Invoice No. Generated, Save Again", "Duplicate", MsgBoxStyle.Critical)
        Else
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        '    Resume	
    End Function
    Private Function AutoGenSeqNo() As String

        On Error GoTo AutoGenSeqNoErr
        Dim RsSRMainGen As ADODB.Recordset
        Dim mNewSeqNo As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mStartingSNo As Double
        Dim mMaxValue As String

        SqlStr = ""
        mStartingSNo = 1

        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_REF)  " & vbCrLf & " FROM PRD_SALERETURN_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_REF,LENGTH(AUTO_KEY_REF)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSRMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsSRMainGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mMaxValue = .Fields(0).Value
                    mNewSeqNo = CInt(Mid(mMaxValue, 1, Len(mMaxValue) - 6))
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

    Private Function UpdateDetail1(ByRef pRefAutoKeyNo As Double) As Boolean

        On Error GoTo UpdateDetail1Err
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim i As Integer
        Dim j As Integer
        Dim mPoNO As String
        Dim mPODate As String
        Dim mItemCode As String
        Dim mUnit As String
        Dim mStockType As String
        Dim mBillQty As Double
        Dim mRecdQty As Double
        Dim mApprovedQty As Double
        Dim mAcceptQty As Double
        Dim mReason As String
        Dim mActionTaken As String
        Dim mCompleteDate As String
        Dim mQCDate As String
        Dim mItemRate As Double
        Dim mItemCost As Double
        Dim pDivisionCode As Double
        Dim mSupplier As String
        Dim mSupplierCode As String
        Dim mBOPName As String
        Dim mBOPCode As String

        Dim mScrapQty As Double
        Dim mRectfiedQty As Double
        Dim mDeptCode As String
        Dim mDeptDesc As String

        pQCDate = VB6.Format(PubCurrDate, "DD/MM/YYYY") '' RunDate	
        If CDate(pQCDate) > CDate(RsCompany.Fields("END_DATE").Value) Then
            pQCDate = VB6.Format(RsCompany.Fields("END_DATE").Value, "DD/MM/YYYY")
        End If

        PubDBCn.Execute("Delete From PRD_SALERETURN_DET Where AUTO_KEY_REF='" & lblMKey.Text & "'")



        '    If chkQC.Enabled = True Then	
        If DeleteStockTRN(PubDBCn, ConStockRefType_MRR, (txtMRRNo.Text)) = False Then GoTo UpdateDetail1Err
        If DeleteCRTRN(PubDBCn, ConStockRefType_MRR, (txtRefNo.Text)) = False Then GoTo UpdateDetail1Err
        '    End If	

        If Trim(txtSupplier.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(TxtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSupplierCode = MasterNo
            End If
        End If

        With SprdMain
            i = 0
            For j = 1 To .MaxRows - 1
                .Row = j
                i = i + 1


                .Col = ColPONo
                mPoNO = .Text

                .Col = ColPODate
                mPODate = MainClass.AllowSingleQuote(.Text)

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColUnit
                mUnit = MainClass.AllowSingleQuote(.Text)

                .Col = ColStockType
                mStockType = MainClass.AllowSingleQuote(.Text)


                .Col = ColBillQty
                mBillQty = Val(.Text)

                .Col = ColReceivedQty
                mRecdQty = Val(.Text)

                .Col = ColAcceptQty
                mAcceptQty = Val(.Text)

                .Col = ColReason
                mReason = Trim(.Text)

                .Col = ColAction
                mActionTaken = Trim(.Text)

                .Col = ColRectfiedQty
                mRectfiedQty = Val(.Text)

                .Col = ColScrapQty
                mScrapQty = Val(.Text)


                .Col = ColCompleteDate
                mCompleteDate = Trim(.Text)

                '	
                '	
                '            .Col = colSupplier	
                '            mSupplier = Trim(.Text)	
                '            mSupplierCode = ""	



                .Col = ColDeptCode
                mDeptCode = MainClass.AllowSingleQuote(.Text)
                mDeptDesc = ""

                If mDeptCode <> "" Then
                    If MainClass.ValidateWithMasterTable(mDeptCode, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mDeptDesc = MasterNo
                    End If
                End If


                .Col = ColBOPItem
                mBOPName = Trim(.Text)
                mBOPCode = ""

                If mBOPName <> "" Then
                    If MainClass.ValidateWithMasterTable(mBOPName, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mBOPCode = MasterNo
                    End If
                End If

                SqlStr = ""

                SqlStr = " INSERT INTO PRD_SALERETURN_DET ( " & vbCrLf _
                    & " COMPANY_CODE, AUTO_KEY_REF, SERIAL_NO, " & vbCrLf _
                    & " REF_AUTO_KEY_NO, REF_DATE, ITEM_CODE," & vbCrLf _
                    & " ITEM_UOM, STOCK_TYPE, BILL_QTY," & vbCrLf _
                    & " RECEIVED_QTY, APPROVED_QTY, LOT_ACCEPT, " & vbCrLf _
                    & " REASON, ACTION_TAKEN, COMPLETION_DATE, BOP_SUPP_CODE, BOP_ITEM_CODE, RECTIFIED_QTY, SCRAP_QTY,DEPT_CODE ) "


                SqlStr = SqlStr & vbCrLf & " VALUES (" & RsCompany.Fields("COMPANY_CODE").Value & ", '" & pRefAutoKeyNo & "'," & i & "," & vbCrLf _
                    & " " & mPoNO & ", TO_DATE('" & VB6.Format(mPODate, "DD/MMM/YYYY") & "','DD-MON-YYYY'), '" & mItemCode & "', " & vbCrLf _
                    & " '" & mUnit & "','" & mStockType & "'," & mBillQty & ", " & vbCrLf _
                    & " " & mRecdQty & ", " & mAcceptQty & ", " & mAcceptQty & "," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(mReason) & "', '" & MainClass.AllowSingleQuote(mActionTaken) & "'," & vbCrLf _
                    & " TO_DATE('" & VB6.Format(mCompleteDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(mSupplierCode) & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(mBOPCode) & "'," & Val(CStr(mRectfiedQty)) & "," & mScrapQty & ", '" & mDeptCode & "') "

                PubDBCn.Execute(SqlStr)

                mQCDate = VB6.Format(txtRefDate.Text, "DD/MM/YYYY")

                SqlStr = " UPDATE INV_GATE_DET SET  " & vbCrLf _
                    & " STOCK_TYPE = '" & mStockType & "'," & vbCrLf _
                    & " APPROVED_QTY = " & mAcceptQty & "," & vbCrLf _
                    & " LOT_ACCEPT = " & mAcceptQty & "," & vbCrLf _
                    & " REJECTED_QTY = " & mRecdQty - mAcceptQty & "," & vbCrLf _
                    & " SHORTAGE_QTY = " & mBillQty - mRecdQty & ", " & vbCrLf _
                    & " QC_EMP_CODE = '" & MainClass.AllowSingleQuote(txtEmp.Text) & "', " & vbCrLf _
                    & " MRR_QCDATE = TO_DATE('" & VB6.Format(mQCDate, "DD/MMM/YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                    & " WHERE AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & "" & vbCrLf _
                    & " AND SERIAL_NO=" & Val(CStr(i)) & "" & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"

                PubDBCn.Execute(SqlStr)

                mItemRate = GetRate(Val(mPoNO), mItemCode)
                mItemCost = mItemRate * mRectfiedQty

                If CDate(txtRefDate.Text) <= CDate("20/05/2018") Then
                    If UpdateStockTRN(PubDBCn, ConStockRefType_MRR, (txtMRRNo.Text), i, (txtMRRDate.Text), (txtMRRDate.Text), "CR", mItemCode, mUnit, CStr(-1), mRectfiedQty + mScrapQty, 0, "I", mItemRate, mItemCost, "", "", "STR", "", "", "N", "From : " & txtSupplier.Text, mSupplierCode, ConWH, CDbl(lblDivisionCode.Text), (lblRefType.Text), "") = False Then GoTo UpdateDetail1Err

                Else
                    If UpdateStockTRN(PubDBCn, ConStockRefType_MRR, (txtMRRNo.Text), i, (txtMRRDate.Text), (txtMRRDate.Text), "WC", mItemCode, mUnit, CStr(-1), mRectfiedQty, 0, "I", mItemRate, mItemCost, "", "", "STR", "", "", "N", "From : " & txtSupplier.Text, mSupplierCode, ConWH, CDbl(lblDivisionCode.Text), (lblRefType.Text), "") = False Then GoTo UpdateDetail1Err

                    mItemCost = mItemRate * mScrapQty

                    If UpdateStockTRN(PubDBCn, ConStockRefType_MRR, (txtMRRNo.Text), i, (txtMRRDate.Text), (txtMRRDate.Text), "SR", mItemCode, mUnit, CStr(-1), mScrapQty, 0, "I", mItemRate, mItemCost, "", "", "STR", "", "", "N", "From : " & txtSupplier.Text, mSupplierCode, ConWH, CDbl(lblDivisionCode.Text), (lblRefType.Text), "") = False Then GoTo UpdateDetail1Err


                    If UpdateStockTRN(PubDBCn, ConStockRefType_MRR, (txtMRRNo.Text), i * 1000, (txtMRRDate.Text), (txtMRRDate.Text), "WC", mItemCode, mUnit, CStr(-1), mRectfiedQty, 0, "O", mItemRate, mItemCost, "", "", "STR", "", "", "N", "To : " & mDeptDesc & " -" & ConStockRefType_MRR, mSupplierCode, ConWH, CDbl(lblDivisionCode.Text), (lblRefType.Text), "") = False Then GoTo UpdateDetail1Err


                    If UpdateStockTRN(PubDBCn, ConStockRefType_MRR, (txtMRRNo.Text), i * 1000, (txtMRRDate.Text), (txtMRRDate.Text), "SR", mItemCode, mUnit, CStr(-1), mScrapQty, 0, "O", mItemRate, mItemCost, "", "", "STR", "", "", "N", "To : " & mDeptDesc & " -" & ConStockRefType_MRR, mSupplierCode, ConWH, CDbl(lblDivisionCode.Text), (lblRefType.Text), "") = False Then GoTo UpdateDetail1Err


                    If UpdateStockTRN(PubDBCn, ConStockRefType_MRR, (txtMRRNo.Text), i * 1000, (txtMRRDate.Text), (txtMRRDate.Text), "WC", mItemCode, mUnit, CStr(-1), mRectfiedQty, 0, "I", mItemRate, mItemCost, "", "", mDeptCode, "", "", "N", "FROM STORE -" & ConStockRefType_MRR, "-1", ConPH, CDbl(lblDivisionCode.Text), (lblRefType.Text), "") = False Then GoTo UpdateDetail1Err

                    mItemCost = mItemRate * mScrapQty

                    If UpdateStockTRN(PubDBCn, ConStockRefType_MRR, (txtMRRNo.Text), i * 1000, (txtMRRDate.Text), (txtMRRDate.Text), "SR", mItemCode, mUnit, CStr(-1), mScrapQty, 0, "I", mItemRate, mItemCost, "", "", mDeptCode, "", "", "N", "FROM STORE -" & ConStockRefType_MRR, "-1", ConPH, CDbl(lblDivisionCode.Text), (lblRefType.Text), "") = False Then GoTo UpdateDetail1Err


                    If UpdateCRTRN(PubDBCn, CDbl(txtRefNo.Text), (txtRefDate.Text), ConStockRefType_MRR, mSupplierCode, (txtMRRNo.Text), (txtMRRDate.Text), CStr(Val(mPoNO)), mPODate, mItemCode, mRectfiedQty, mUnit, mItemRate, "WC", "I", mCompleteDate, Val(lblDivisionCode.Text), mDeptCode) = False Then GoTo UpdateDetail1Err


                    If UpdateCRTRN(PubDBCn, CDbl(txtRefNo.Text), (txtRefDate.Text), ConStockRefType_MRR, mSupplierCode, (txtMRRNo.Text), (txtMRRDate.Text), CStr(Val(mPoNO)), mPODate, mItemCode, mScrapQty, mUnit, mItemRate, "SR", "I", mCompleteDate, Val(lblDivisionCode.Text), mDeptCode) = False Then GoTo UpdateDetail1Err
                End If


            Next
        End With

        UpdateDetail1 = True
        Exit Function
UpdateDetail1Err:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume	
    End Function
    Private Function GetRate(ByRef xPoNo As Double, ByRef xItemCode As String) As Double

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsPO As ADODB.Recordset

        GetRate = 0
        SqlStr = "SELECT MAX(POD.ITEM_RATE) AS ITEM_RATE " & vbCrLf & " FROM FIN_INVOICE_HDR POM,FIN_INVOICE_DET POD" & vbCrLf & " WHERE POM.MKEY = POD.MKEY" & vbCrLf & " And POM.AUTO_KEY_INVOICE=" & Val(CStr(xPoNo)) & "" & vbCrLf & " And POM.CANCELLED='N' AND POD.ITEM_CODE='" & Trim(xItemCode) & "'" & vbCrLf
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPO, ADODB.LockTypeEnum.adLockReadOnly)

        If RsPO.EOF = False Then
            GetRate = IIf(IsDbNull(RsPO.Fields("ITEM_RATE").Value), 0, RsPO.Fields("ITEM_RATE").Value)
        End If
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim i As Integer
        Dim mLockBookCode As Integer
        Dim mAllItemQCDone As Boolean
        Dim mBillQty As Double
        Dim mReceivedQty As Double
        Dim mAcceptQty As Double
        Dim mEmpCode As String
        Dim mStockType As String
        Dim mRectfiedQty As Double
        Dim mScrapQty As Double
        Dim mItemCode As String
        Dim mDeptCode As String
        Dim mMaxTargetLimit As String

        FieldsVarification = True

        If MainClass.ValidateWithMasterTable(TxtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Supplier Does Not Exist In Master", vbInformation)
            'txtSupplier.SetFocus
            FieldsVarification = False
            Exit Function
        Else
            mSupplierCode = MasterNo
        End If

        If ValidateBranchLocking((txtMRRDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If
        If ValidateAccountLocking(PubDBCn, txtMRRDate.Text, (txtSupplier.Text), mSupplierCode) = True Then
            FieldsVarification = False
            Exit Function
        End If

        mLockBookCode = CInt(ConLockMRRQC)

        If ValidateBookLocking(PubDBCn, mLockBookCode, txtMRRDate.Text) = True Then
            FieldsVarification = False
            Exit Function
        End If

        If ADDMode = True Then
            If chkQC.Enabled = True Then
                If CDate(txtMRRDate.Text) < CDate(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -7, PubCurrDate)) Then
                    If ValidateMRRApproval(PubDBCn, Val(txtMRRNo.Text)) = False Then
                        MsgBox("MRR is More than One Week so that MRR Lock. For Unlock Contact Administrator with Plant Head Approval.", MsgBoxStyle.Information)
                        FieldsVarification = False
                        Exit Function
                    End If
                End If
            End If
        End If

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsSRMain.EOF = True Then Exit Function

        If txtMRRNo.Text = "" Then
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

        If MODIFYMode = True And txtRefNo.Text = "" Then
            MsgInformation("Ref No. is Blank")
            FieldsVarification = False
            Exit Function
        End If

        If txtRefDate.Text = "" Then
            MsgBox("VDate is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtRefDate.Focus()
            Exit Function
        ElseIf FYChk((txtRefDate.Text)) = False Then
            FieldsVarification = False
            If txtRefDate.Enabled = True Then txtRefDate.Focus()
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

        If Trim(txtEmp.Text) = "" Then
            MsgBox("Employee Name is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtEmp.Focus()
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable(txtEmp.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Employee Does Not Exist In Master", vbInformation)
            FieldsVarification = False
            Exit Function
        End If

        mAllItemQCDone = True
        With SprdMain
            For i = 1 To .MaxRows - 1
                .Row = i
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                If mItemCode <> "" Then
                    .Col = ColDeptCode
                    mDeptCode = Trim(.Text)

                    .Col = ColBillQty
                    mBillQty = Val(.Text)

                    .Col = ColReceivedQty
                    mReceivedQty = Val(.Text)

                    .Col = ColAcceptQty
                    mAcceptQty = Val(.Text)

                    .Col = ColRectfiedQty
                    mRectfiedQty = Val(.Text)

                    .Col = ColScrapQty
                    mScrapQty = Val(.Text)

                    If lblRefType.Text = "2" And mRectfiedQty > 0 Then
                        MsgBox("Warranty Sale Retrn Cann't be Rectify", MsgBoxStyle.Information)
                        FieldsVarification = False
                        Exit Function
                    End If

                    If mReceivedQty > mBillQty Then
                        MsgBox("Received Qty Cann't be Greater Than Bill Qty.", MsgBoxStyle.Information)
                        FieldsVarification = False
                        Exit Function
                    End If

                    If mAcceptQty <> mReceivedQty Then
                        MsgBox("Accepted Qty must be equal to Received Qty.", MsgBoxStyle.Information)
                        FieldsVarification = False
                        Exit Function
                    End If

                    If mAcceptQty <> mRectfiedQty + mScrapQty Then
                        MsgBox("Accepted Qty must be equal to Rectified Qty and Scrap Qty.", MsgBoxStyle.Information)
                        FieldsVarification = False
                        Exit Function
                    End If

                    .Col = ColStockType
                    If chkQC.CheckState = System.Windows.Forms.CheckState.Checked Then
                        .Text = "CR"
                    Else
                        If .Text = "QC" Or .Text = "CR" Then
                            If .Text = "QC" Then
                                mAllItemQCDone = False
                            End If
                        Else
                            MsgBox("Stock Type Must be QC or CR.", MsgBoxStyle.Information)
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If
                    mStockType = Trim(.Text)

                    .Col = ColReason
                    If .Text = "" Then
                        MsgBox("Reason of Return is must.", MsgBoxStyle.Information)
                        FieldsVarification = False
                        Exit Function
                    End If

                    '                If mStockType = "CR" Then	
                    .Col = ColAction
                    If .Text = "" Then
                        MsgBox("Action Taken is must.", MsgBoxStyle.Information)
                        FieldsVarification = False
                        Exit Function
                    End If

                    .Col = ColCompleteDate
                    If Trim(.Text) = "" Then
                        MsgBox("Completion Date is must.", MsgBoxStyle.Information)
                        FieldsVarification = False
                        Exit Function
                    End If
                    '                End If	

                    .Col = ColCompleteDate
                    If Trim(.Text) <> "" Then
                        If Not IsDate(.Text) Then
                            MsgBox("Completion Date is not Valid.", MsgBoxStyle.Information)
                            FieldsVarification = False
                            Exit Function
                        End If

                        If CDate(.Text) < CDate(txtMRRDate.Text) Then
                            MsgBox("Completion Date cann't be less than MRR date.", MsgBoxStyle.Information)
                            FieldsVarification = False
                            Exit Function
                        End If

                        If CDate(.Text) < CDate(txtRefDate.Text) Then
                            MsgBox("Completion Date cann't be less than MRR date.", MsgBoxStyle.Information)
                            FieldsVarification = False
                            Exit Function
                        End If

                        mMaxTargetLimit = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, 15, CDate(txtRefDate.Text)))
                        If CDate(.Text) > CDate(mMaxTargetLimit) Then
                            MsgBox("Completion Target Date cann't be more than 15 days from MRR Date.", MsgBoxStyle.Information)
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If

                    If ValidateDept(mItemCode, mDeptCode) = False Then
                        If mDeptCode <> "NPD" Then
                            MsgBox("Please check dept, Not as valid Sequence for this product.", MsgBoxStyle.Information)
                            FieldsVarification = False
                            Exit Function
                        End If
                    End If

                End If
            Next
        End With

        If mAllItemQCDone = True Then
            chkQC.CheckState = System.Windows.Forms.CheckState.Checked
        End If

        If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "ItemCode Is Blank.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColBillQty, "N", "Please Check Bill Quantity.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColStockType, "S", "Please Check Stock Type.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColDeptCode, "S", "Please Check Dept Code.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColReason, "S", "Please Reason of Return.") = False Then FieldsVarification = False : Exit Function


        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume	
    End Function


    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Public Sub frmSRQCEntry_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Sales Return QC Entry"

        SqlStr = ""
        SqlStr = "Select * from PRD_SALERETURN_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSRMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from PRD_SALERETURN_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSRDetail, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)
        Call SetTextLengths()
        mSupplierCode = CStr(-1)

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

        MainClass.ClearGrid(SprdView)

        SqlStr = "Select IH.AUTO_KEY_REF as REF_No,TO_CHAR(IH.REF_DATE,'DD-MM-YYYY') as REF_Date,GR.AUTO_KEY_MRR as MRR_No," & vbCrLf _
            & " TO_CHAR(GR.MRR_DATE,'DD-MM-YYYY') as MRR_Date, " & vbCrLf _
            & " AC.SUPP_CUST_NAME AS SupplierName " & vbCrLf _
            & " FROM PRD_SALERETURN_HDR IH, INV_GATE_HDR GR, FIN_SUPP_CUST_MST AC " & vbCrLf _
            & " WHERE GR.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUBSTR(AUTO_KEY_REF,LENGTH(AUTO_KEY_REF)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND IH.COMPANY_CODE=GR.COMPANY_CODE AND IH.AUTO_KEY_MRR=GR.AUTO_KEY_MRR" & vbCrLf _
            & " AND GR.COMPANY_CODE=AC.COMPANY_CODE " & vbCrLf _
            & " AND GR.SUPP_CUST_CODE=AC.SUPP_CUST_CODE " & vbCrLf & " Order by AUTO_KEY_REF"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
AssignGridErr:
        MsgBox(Err.Description, MsgBoxStyle.Information)
        'Resume	
    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1

            .set_RowHeight(0, 600)

            .set_ColWidth(0, 600)

            .set_ColWidth(1, 1200)
            .set_ColWidth(2, 1200)
            .set_ColWidth(3, 1200)
            .set_ColWidth(4, 1200)
            .set_ColWidth(5, 4500)
            .set_ColWidth(6, 1200)

            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            SprdView.set_RowHeight(-1, 300)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1


        With SprdMain
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)

            .Col = ColPONo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsSRDetail.Fields("REF_AUTO_KEY_NO").Precision ''	
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


            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsSRDetail.Fields("ITEM_CODE").DefinedSize ''	
            .set_ColWidth(ColItemCode, 8)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn)
            .ColsFrozen = ColItemDesc
            .Col = ColUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeEditLen = RsSRDetail.Fields("ITEM_UOM").DefinedSize ''	
            .set_ColWidth(ColUnit, 4)
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
            .TypeEditLen = RsSRDetail.Fields("STOCK_TYPE").DefinedSize ''	
            .set_ColWidth(ColStockType, 5)

            .Col = ColDeptCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsSRDetail.Fields("DEPT_CODE").DefinedSize ''	
            .set_ColWidth(ColDeptCode, 5)


            .Col = ColAcceptQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.999")
            .TypeFloatMin = CDbl("-99999999999.999")
            .set_ColWidth(ColAcceptQty, 9)

            .Col = ColRectfiedQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.999")
            .TypeFloatMin = CDbl("-99999999999.999")
            .set_ColWidth(ColRectfiedQty, 9)

            .Col = ColScrapQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.999")
            .TypeFloatMin = CDbl("-99999999999.999")
            .set_ColWidth(ColScrapQty, 9)


            .Col = ColReason
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsSRDetail.Fields("REASON").DefinedSize ''	
            .set_ColWidth(ColReason, 15)

            .Col = ColAction
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsSRDetail.Fields("ACTION_TAKEN").DefinedSize ''	
            .set_ColWidth(ColAction, 15)


            .Col = ColCompleteDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeEditCharSet = SS_CELL_DATE_FORMAT_DDMMYY
            .TypeDateFormat = FPSpreadADO.TypeDateFormatConstants.TypeDateFormatDDMMYY
            '        .TypeEditLen = RsSRDetail.Fields("ITEM_CODE").DefinedSize           ''	
            .set_ColWidth(ColCompleteDate, 6)

        End With

        If chkQC.Enabled = True Then
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPONo, ColReceivedQty)
        Else
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColPONo, ColReason)
        End If

        '    MainClass.ProtectCell SprdMain, 1, SprdMain.MaxRows, ColApprovedQty, ColApprovedQty	

        MainClass.SetSpreadColor(SprdMain, Arow)

        Exit Sub
ERR1:
        '    Resume	
        If Err.Number = -2147418113 Then RsSRDetail.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Function ValidateDept(ByRef mProductCode As String, ByRef mCheckDeptCode As String) As Boolean

        On Error GoTo LedgError
        Dim RsShow As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mDeptCode As String
        Dim xProductCode As String
        Dim xPrdCode As String
        Dim mProductSeqDept As String

        ValidateDept = False

        If RsCompany.Fields("StockBalCheck").Value = "N" Then
            ValidateDept = True
            Exit Function
        End If
        xProductCode = mProductCode

        If GetProductSeqNo(mProductCode, mCheckDeptCode, (txtRefDate.Text)) > 0 Then
            ValidateDept = True
            Exit Function
        End If

        SqlStr = "SELECT * FROM PRD_NEWBOM_HDR IH " & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        If RsShow.EOF = True Then
            ValidateDept = True
            Exit Function
        End If

CheckNext:
        SqlStr = " SELECT DISTINCT DEPT_CODE " & vbCrLf & " FROM VW_PRD_BOM_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='O'"

        SqlStr = SqlStr & vbCrLf & " START WITH  TRIM(PRODUCT_CODE) || '-' || COMPANY_CODE='" & MainClass.AllowSingleQuote(xProductCode) & "-" & RsCompany.Fields("COMPANY_CODE").Value & "'" & vbCrLf & " CONNECT BY PRIOR (TRIM(RM_CODE) || COMPANY_CODE || ' ')=TRIM(PRODUCT_CODE) || COMPANY_CODE || ' '"

        SqlStr = SqlStr & vbCrLf & " ORDER SIBLINGS BY DEPT_CODE"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)


        If Not RsShow.EOF Then
            Do While Not RsShow.EOF
                mDeptCode = Trim(IIf(IsDbNull(RsShow.Fields("DEPT_CODE").Value), "", RsShow.Fields("DEPT_CODE").Value))
                If mCheckDeptCode = mDeptCode Then
                    ValidateDept = True
                    Exit Do
                End If
                RsShow.MoveNext()
            Loop
        Else
            xPrdCode = GetMainItemCode(xProductCode)
            If xProductCode <> xPrdCode Then
                xProductCode = xPrdCode
                GoTo CheckNext
            End If
        End If

        RsShow = Nothing
        Exit Function
LedgError:
        ValidateDept = False
        ''    Resume	
        MsgInformation(Err.Description)
    End Function

    Private Sub SetTextLengths()
        On Error GoTo ERR1
        With RsSRMain

            txtRefNo.Maxlength = .Fields("AUTO_KEY_REF").Precision
            txtRefDate.Maxlength = 10

            txtMRRNo.Maxlength = .Fields("AUTO_KEY_MRR").Precision
            txtMRRDate.Maxlength = 10
            txtEmp.Maxlength = .Fields("EMP_CODE").DefinedSize

            '        TxtSupplier.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)	
            '        txtBillNo.MaxLength = .Fields("BILL_NO").DefinedSize	
            '        txtBillDate.MaxLength = 10	

            txtRemarks.Maxlength = .Fields("REMARKS").DefinedSize


        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Show1()

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim mRefType As String
        Dim mDivision As String
        Dim mDivisionCode As Double
        Dim SqlStr As String = ""

        With RsSRMain
            If Not .EOF Then

                lblMKey.Text = .Fields("AUTO_KEY_REF").Value
                txtMRRNo.Text = IIf(IsDbNull(.Fields("AUTO_KEY_MRR").Value), "", .Fields("AUTO_KEY_MRR").Value)
                txtMRRDate.Text = VB6.Format(IIf(IsDbNull(.Fields("MRR_DATE").Value), "", .Fields("MRR_DATE").Value), "DD/MM/YYYY")

                SqlStr = "SELECT SUPP_CUST_CODE,DIV_CODE,REF_TYPE,BILL_NO,BILL_DATE FROM INV_GATE_HDR WHERE AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & ""
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)

                If RsMisc.EOF = False Then
                    mDivisionCode = IIf(IsDbNull(RsMisc.Fields("DIV_CODE").Value), "", RsMisc.Fields("DIV_CODE").Value)
                    lblDivisionCode.Text = IIf(IsDbNull(RsMisc.Fields("DIV_CODE").Value), "", RsMisc.Fields("DIV_CODE").Value)

                    mRefType = IIf(IsDbNull(RsMisc.Fields("REF_TYPE").Value), "", RsMisc.Fields("REF_TYPE").Value)
                    lblRefType.Text = IIf(IsDbNull(RsMisc.Fields("REF_TYPE").Value), "", RsMisc.Fields("REF_TYPE").Value)

                    mSupplierCode = IIf(IsDbNull(RsMisc.Fields("SUPP_CUST_CODE").Value), "", RsMisc.Fields("SUPP_CUST_CODE").Value)

                    txtBillNo.Text = IIf(IsDbNull(RsMisc.Fields("BILL_NO").Value), "", RsMisc.Fields("BILL_NO").Value)
                    txtBillDate.Text = VB6.Format(IIf(IsDbNull(RsMisc.Fields("BILL_DATE").Value), "", RsMisc.Fields("BILL_DATE").Value), "DD/MM/YYYY")

                End If

                txtRefNo.Text = IIf(IsDbNull(.Fields("AUTO_KEY_REF").Value), "", .Fields("AUTO_KEY_REF").Value)
                txtRefDate.Text = VB6.Format(IIf(IsDbNull(.Fields("REF_DATE").Value), "", .Fields("REF_DATE").Value), "DD/MM/YYYY")

                lblEntryDate.Text = VB6.Format(IIf(IsDbNull(.Fields("ADDDATE").Value), "", .Fields("ADDDATE").Value), "DD/MM/YYYY HH:MM")

                If MainClass.ValidateWithMasterTable(.Fields("EMP_CODE").Value, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    lblEmp.Text = MasterNo
                End If
                txtEmp.Text = .Fields("EMP_CODE").Value

                txtSupplier.Text = ""
                If MainClass.ValidateWithMasterTable(mSupplierCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtSupplier.Text = MasterNo
                End If

                lblDivisionName.Text = ""
                If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    lblDivisionName.Text = MasterNo
                End If

                txtRemarks.Text = IIf(IsDbNull(.Fields("Remarks").Value), "", .Fields("Remarks").Value)

                chkQC.CheckState = IIf(.Fields("QC_DONE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkQC.Enabled = IIf(.Fields("QC_DONE").Value = "N", True, False)

                cmdResetMRR.Enabled = False
                If PubSuperUser = "S" Then
                    cmdResetMRR.Enabled = True
                End If


                txtMRRNo.Enabled = False
                cmdMRRSearch.Enabled = False

                Call ShowDetail1((lblMKey.Text))

            End If
        End With
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsSRMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        SprdMain.Enabled = True

        txtRefNo.Enabled = True
        cmdSearchRef.Enabled = True

        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume	
    End Sub
    Private Sub ShowFromMRR(ByRef mRsGate As ADODB.Recordset)
        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim mMode As String
        Dim mDivisionCode As Double


        With mRsGate
            If Not .EOF Then

                txtMRRNo.Text = IIf(IsDbNull(.Fields("AUTO_KEY_MRR").Value), "", .Fields("AUTO_KEY_MRR").Value)
                txtMRRDate.Text = VB6.Format(IIf(IsDbNull(.Fields("MRR_DATE").Value), "", .Fields("MRR_DATE").Value), "DD/MM/YYYY")

                mSupplierCode = .Fields("SUPP_CUST_CODE").Value
                If MainClass.ValidateWithMasterTable(.Fields("SUPP_CUST_CODE").Value, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    TxtSupplier.Text = MasterNo
                End If

                lblRefType.Text = IIf(IsDbNull(.Fields("REF_TYPE").Value), "", .Fields("REF_TYPE").Value)

                txtBillNo.Text = IIf(IsDbNull(.Fields("BILL_NO").Value), "", .Fields("BILL_NO").Value)
                txtBillDate.Text = VB6.Format(IIf(IsDbNull(.Fields("BILL_DATE").Value), "", .Fields("BILL_DATE").Value), "DD/MM/YYYY")

                txtRemarks.Text = "" '' IIf(IsNull(!REMARKS), "", !REMARKS)	
                txtEmp.Text = ""

                lblDivisionCode.Text = IIf(IsDbNull(.Fields("DIV_CODE").Value), "", .Fields("DIV_CODE").Value)
                mDivisionCode = IIf(IsDbNull(.Fields("DIV_CODE").Value), "", .Fields("DIV_CODE").Value)


                lblDivisionName.Text = ""
                If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    lblDivisionName.text = MasterNo
                End If

                Call ShowDetailFromMRR((txtMRRNo.Text))
            End If
        End With
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume	
    End Sub


    Private Sub ShowDetail1(ByRef mMKEY As String)

        On Error GoTo ERR1
        Dim i As Integer
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
        Dim mBopSuppCode As String
        Dim mBopItemCode As String
        Dim mBopSuppName As String
        Dim mBopItemName As String


        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM PRD_SALERETURN_DET " & vbCrLf & " Where AUTO_KEY_REF=" & Val(mMKEY) & "" & vbCrLf & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSRDetail, ADODB.LockTypeEnum.adLockReadOnly)

        With RsSRDetail
            If .EOF = True Then Exit Sub
            i = 1
            .MoveFirst()

            Do While Not .EOF

                SprdMain.Row = i

                SprdMain.Col = ColPONo
                mRefPoNo = (IIf(IsDbNull(.Fields("REF_AUTO_KEY_NO").Value), -1, .Fields("REF_AUTO_KEY_NO").Value))
                SprdMain.Text = mRefPoNo

                SprdMain.Col = ColPODate
                SprdMain.Text = IIf(IsDbNull(.Fields("REF_DATE").Value), "", .Fields("REF_DATE").Value)

                SprdMain.Col = ColItemCode
                mItemCode = Trim(IIf(IsDbNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))
                SprdMain.Text = Trim(mItemCode)

                SprdMain.Col = ColItemDesc
                mItemDesc = ""
                If MainClass.ValidateWithMasterTable(Trim(mItemCode), "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mItemDesc = MasterNo
                End If

                SprdMain.Text = mItemDesc

                SprdMain.Col = ColUnit
                SprdMain.Text = IIf(IsDbNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)

                SprdMain.Row = i
                SprdMain.Col = ColBillQty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("BILL_QTY").Value), 0, .Fields("BILL_QTY").Value)))

                SprdMain.Col = ColReceivedQty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("RECEIVED_QTY").Value), 0, .Fields("RECEIVED_QTY").Value)))

                SprdMain.Col = ColStockType
                SprdMain.Text = IIf(IsDbNull(.Fields("STOCK_TYPE").Value), "", .Fields("STOCK_TYPE").Value)

                SprdMain.Col = ColDeptCode
                SprdMain.Text = IIf(IsDbNull(.Fields("DEPT_CODE").Value), "", .Fields("DEPT_CODE").Value)

                SprdMain.Col = ColAcceptQty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("LOT_ACCEPT").Value), 0, .Fields("LOT_ACCEPT").Value)))

                SprdMain.Col = ColRectfiedQty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("RECTIFIED_QTY").Value), 0, .Fields("RECTIFIED_QTY").Value)))

                SprdMain.Col = ColScrapQty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("SCRAP_QTY").Value), 0, .Fields("SCRAP_QTY").Value)))


                '            SprdMain.Col = ColApprovedQty	
                '            SprdMain.Text = Val(IIf(IsNull(!APPROVED_QTY), 0, !APPROVED_QTY))	

                SprdMain.Col = ColReason
                SprdMain.Text = IIf(IsDbNull(.Fields("reason").Value), "", .Fields("reason").Value)

                SprdMain.Col = ColAction
                SprdMain.Text = IIf(IsDbNull(.Fields("ACTION_TAKEN").Value), "", .Fields("ACTION_TAKEN").Value)

                SprdMain.Col = ColCompleteDate
                SprdMain.Text = VB6.Format(IIf(IsDbNull(.Fields("COMPLETION_DATE").Value), "", .Fields("COMPLETION_DATE").Value), "DD/MM/YYYY")


                mBopSuppCode = Trim(IIf(IsDbNull(.Fields("BOP_SUPP_CODE").Value), "", .Fields("BOP_SUPP_CODE").Value))
                mBopItemCode = Trim(IIf(IsDbNull(.Fields("BOP_ITEM_CODE").Value), "", .Fields("BOP_ITEM_CODE").Value))
                mBopSuppName = ""
                mBopItemName = ""

                If mBopSuppCode <> "" Then
                    If MainClass.ValidateWithMasterTable(mBopSuppCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mBopSuppName = MasterNo
                    End If
                End If

                If mBopItemCode <> "" Then
                    If MainClass.ValidateWithMasterTable(mBopItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mBopItemName = MasterNo
                    End If
                End If

                SprdMain.Col = ColSupplier
                SprdMain.Text = mBopSuppName

                SprdMain.Col = ColBOPItem
                SprdMain.Text = mBopItemName

                .MoveNext()

                i = i + 1
                SprdMain.MaxRows = i
            Loop
        End With
        FormatSprdMain(-1)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume	
    End Sub

    Private Sub ShowDetailFromMRR(ByRef mMKEY As String)

        On Error GoTo ERR1
        Dim i As Integer
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
        Dim RsGateDetail As ADODB.Recordset
        Dim mQCEmpCode As String

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM INV_GATE_DET " & vbCrLf & " Where AUTO_KEY_MRR=" & Val(mMKEY) & "" & vbCrLf & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGateDetail, ADODB.LockTypeEnum.adLockReadOnly)

        With RsGateDetail
            If .EOF = True Then Exit Sub
            i = 1
            .MoveFirst()

            Do While Not .EOF

                SprdMain.Row = i

                SprdMain.Col = ColPONo
                mRefPoNo = (IIf(IsDbNull(.Fields("REF_AUTO_KEY_NO").Value), -1, .Fields("REF_AUTO_KEY_NO").Value))
                SprdMain.Text = mRefPoNo

                SprdMain.Col = ColPODate
                SprdMain.Text = IIf(IsDbNull(.Fields("REF_DATE").Value), "", .Fields("REF_DATE").Value)

                SprdMain.Col = ColItemCode
                mItemCode = Trim(IIf(IsDbNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))
                SprdMain.Text = Trim(mItemCode)

                SprdMain.Col = ColItemDesc
                MainClass.ValidateWithMasterTable(Trim(mItemCode), "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mItemDesc = MasterNo
                SprdMain.Text = mItemDesc

                SprdMain.Col = ColUnit
                SprdMain.Text = IIf(IsDbNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)

                SprdMain.Row = i
                SprdMain.Col = ColBillQty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("BILL_QTY").Value), 0, .Fields("BILL_QTY").Value)))

                SprdMain.Col = ColReceivedQty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("RECEIVED_QTY").Value), 0, .Fields("RECEIVED_QTY").Value)))

                SprdMain.Col = ColAcceptQty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("LOT_ACCEPT").Value), 0, .Fields("LOT_ACCEPT").Value)))

                '            SprdMain.Col = ColApprovedQty	
                '            SprdMain.Text = Val(IIf(IsNull(!APPROVED_QTY), 0, !APPROVED_QTY))	

                SprdMain.Col = ColStockType
                SprdMain.Text = "QC"

                '            SprdMain.Col = ColReason	
                '            SprdMain.Text = IIf(IsNull(!REMARKS), "", !REMARKS)	

                '            mQCEmpCode = GetQCEmpCode(mItemCode)	
                '            SprdMain.Row = I	
                '            SprdMain.Col = ColQCEMP	
                '            SprdMain.Text = mQCEmpCode	

                .MoveNext()

                i = i + 1
                SprdMain.MaxRows = i
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
            FormatSprdView()
            SprdView.Focus()
            FraFront.SendToBack()
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraFront.BringToFront()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsSRMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub Clear1()

        Dim mCurrentDateFY As Integer

        lblMKey.Text = ""
        lblRefType.Text = ""
        lblDivisionCode.Text = ""
        lblDivisionName.Text = ""

        mSupplierCode = CStr(-1)
        txtMRRNo.Text = ""
        txtMRRDate.Text = ""

        mCurrentDateFY = GetCurrentFYNo(PubDBCn, VB6.Format(PubCurrDate, "DD/MM/YYYY"))
        txtRefNo.Text = ""
        If mCurrentDateFY = RsCompany.Fields("FYEAR").Value Then
            txtRefDate.Text = VB6.Format(PubCurrDate, "DD/MM/YYYY")
            txtRefDate.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)
        Else
            txtRefDate.Text = VB6.Format(RsCompany.Fields("END_DATE").Value, "DD/MM/YYYY")
            txtRefDate.Enabled = True
        End If

        lblEntryDate.Text = VB6.Format(PubCurrDate, "DD/MM/YYYY") & " " & GetServerTime
        txtSupplier.Text = ""
        txtBillNo.Text = ""
        txtBillDate.Text = "" 'Format(RunDate, "DD/MM/YYYY")	

        txtRemarks.Text = ""
        txtEmp.Text = ""
        lblEmp.Text = ""

        txtMRRNo.Enabled = True
        cmdMRRSearch.Enabled = True
        txtRefNo.Enabled = False
        cmdSearchRef.Enabled = False


        txtBillNo.Enabled = False
        txtBillDate.Enabled = False
        txtSupplier.Enabled = False


        chkQC.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkQC.Enabled = True
        cmdResetMRR.Enabled = False

        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)

        MainClass.ButtonStatus(Me, XRIGHT, RsSRMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub frmSRQCEntry_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
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

    Private Sub frmSRQCEntry_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection	
        'PvtDBCn.Open StrConn	

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        pXRIGHT = XRIGHT
        xMyMenu = myMenu
        MainClass.RightsToButton(Me, XRIGHT)

        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Top = 0
        Me.Left = 0
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        'Me.Height = VB6.TwipsToPixelsY(7245) '8000	
        'Me.Width = VB6.TwipsToPixelsX(11355) '11900	


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = False
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
        Dim mPoNO As Double

        mCol = SprdMain.ActiveCol

        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColStockType Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColStockType, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColDeptCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColDeptCode, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColSupplier Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColSupplier, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColBOPItem Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColBOPItem, 0))

        SprdMain.Refresh()

    End Sub

    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        With SprdMain
            '        SprdMain_LeaveCell .ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False	
        End With
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
        KeyAscii = MainClass.UpperCase(KeyAscii, txtBillNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSupplier_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtSupplier.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtSupplier_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtSupplier.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSupplier.Text)
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

        KeyAscii = MainClass.UpperCase(KeyAscii, txtRemarks.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String)
        On Error GoTo ErrPart
        Dim mAmountInword As String


        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)


        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\" & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        Report1.Action = 1
        Report1.Reset()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Sub frmSRQCEntry_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        'SprdView.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))

        'FraFront.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11379.7, 749)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 210, mReFormWidth - 210, mReFormWidth))
        SprdView.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 210, mReFormWidth - 210, mReFormWidth))
        FraFront.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)



        CurrFormWidth = mReFormWidth

        'MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
End Class
