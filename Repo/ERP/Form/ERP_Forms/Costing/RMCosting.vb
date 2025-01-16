Option Strict Off
Option Explicit On
Imports System.ComponentModel
Imports Microsoft.VisualBasic.Compatibility
Friend Class FrmRMCosting
    Inherits System.Windows.Forms.Form

    Dim RsCostMain As ADODB.Recordset
    Dim RsCostDetail As ADODB.Recordset
    Dim RsPartDetail As ADODB.Recordset
    Dim RsProcess1Detail As ADODB.Recordset
    Dim RsProcess2Detail As ADODB.Recordset
    Dim RsOprnDetail As ADODB.Recordset
    Dim RsConsDetail As ADODB.Recordset

    'Dim PvtDBCn As ADODB.Connection					

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean
    Dim mIsShowing As Boolean

    Private Const ConRowHeight As Short = 12

    Private Const ColMannualCalc As Short = 1
    Private Const ColRMDesc As Short = 2
    Private Const ColRMRate As Short = 3
    Private Const ColRMUOM As Short = 4
    Private Const ColRMThick As Short = 5
    Private Const ColRMLenth As Short = 6
    Private Const ColRMWidth As Short = 7
    Private Const ColRMDiaMeter As Short = 8
    Private Const ColWtPerStrip As Short = 9
    Private Const ColQtyPerStrip As Short = 10
    Private Const ColWtPerPc As Short = 11
    Private Const ColRMCost As Short = 12
    Private Const ColNetWt As Short = 13
    Private Const ColScrapWt As Short = 14
    Private Const ColScrapRate As Short = 15
    Private Const ColScrapCost As Short = 16
    Private Const ColNetRMCost As Short = 17


    Private Const ColPartDesc As Short = 1
    Private Const ColPartNo As Short = 2
    Private Const ColPartUOM As Short = 3
    Private Const ColPartQty As Short = 4
    Private Const ColPartRate As Short = 5
    Private Const ColPartCost As Short = 6

    Private Const ColProcess1 As Short = 1
    Private Const ColMachCode1 As Short = 2
    Private Const ColMachDesc1 As Short = 3
    Private Const ColMachTonnes As Short = 4
    Private Const ColStroke1 As Short = 5
    Private Const ColRate1 As Short = 6
    Private Const ColCost1 As Short = 7

    Private Const ColProcess2 As Short = 1
    Private Const ColPlantNo2 As Short = 2
    Private Const ColSurface2 As Short = 3
    Private Const ColRate2 As Short = 4
    Private Const ColCost2 As Short = 5

    Private Const ColOPRCode As Short = 1
    Private Const ColOprDesc As Short = 2
    Private Const ColOprUOM As Short = 3
    Private Const ColOprQty As Short = 4
    Private Const ColOPRRate As Short = 5
    Private Const ColOprCost As Short = 6

    Private Const ColExpName As Short = 1
    Private Const ColExpPercent As Short = 2
    Private Const ColExpAmt As Short = 3
    Private Const ColExpCalc As Short = 4
    Private Const ColExpAddDed As Short = 5
    Private Const ColExpCode As Short = 6
    Private Const ColExpRemarks As Short = 7

    Dim mAmendStatus As Boolean
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo AssignGridErr
        Dim SqlStr As String
        SqlStr = ""
        SqlStr = "SELECT DECODE(IH.STATUS,'O','OPEN','CLOSE') AS STATUS, " & vbCrLf _
            & " IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, " & vbCrLf _
            & " IH.ITEM_CODE, IMST.ITEM_SHORT_DESC, IMST.ISSUE_UOM, " & vbCrLf _
            & " TO_CHAR(IH.WEF,'DD/MM/YYYY') AS WEF, IH.AMEND_NO, " & vbCrLf _
            & " TO_CHAR(IH.NET_COST,'0.0000') AS NET_COST, IH.PREPARED_BY, IH.APP_EMP_CODE " & vbCrLf _
            & " FROM PRD_BOP_COST_HDR IH, INV_ITEM_MST IMST, FIN_SUPP_CUST_MST CMST " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " IH.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.COMPANY_CODE = IMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.ITEM_CODE = IMST.ITEM_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE = CMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE = CMST.SUPP_CUST_CODE " & vbCrLf _
            & " ORDER BY CMST.SUPP_CUST_NAME,IH.ITEM_CODE, IH.WEF "

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()

        Exit Sub
AssignGridErr:
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 12)
            .set_ColWidth(0, 4)
            .set_ColWidth(1, 6)
            .set_ColWidth(2, 8)
            .set_ColWidth(3, 25)
            .set_ColWidth(4, 8)
            .set_ColWidth(5, 8)
            .set_ColWidth(6, 8)
            .set_ColWidth(7, 8)
            .set_ColWidth(8, 8)
            .set_ColWidth(9, 8)

            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        FieldsVarification = True
        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False

            Exit Function
        End If
        If MODIFYMode = True And RsCostMain.EOF = True Then Exit Function

        If MODIFYMode = True And chkStatus.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Closed Costing Cann't be Modified")
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtSupplierCode.Text) = "" Then
            MsgBox("Supplier Code is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtSupplierCode.Enabled = True Then txtSupplierCode.Focus()
            Exit Function
        End If

        If Trim(txtItemCode.Text) = "" Then
            MsgBox("Product Code is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtItemCode.Enabled = True Then txtItemCode.Focus()
            Exit Function
        End If

        If Trim(txtWEF.Text) = "" Then
            MsgBox("W.E.F is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtWEF.Enabled = True Then txtWEF.Focus()
            Exit Function
        End If

        If Trim(txtItemDesc.Text) = "" Then
            MsgBox("Product Desc is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtItemDesc.Enabled = True Then txtItemDesc.Focus()
            Exit Function
        End If

        If Trim(txtUnit.Text) = "" Then
            MsgBox("Unit is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtUnit.Enabled = True Then txtUnit.Focus()
            Exit Function
        End If
        If Trim(txtPreparedBy.Text) = "" Then
            MsgBox("Prepared By is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtPreparedBy.Focus()
            Exit Function
        End If


        If chkV2V.CheckState = System.Windows.Forms.CheckState.Unchecked Then

            If ValidateWEFDate() = False Then
                MsgInformation("WEF Date Cann't be Less than Previous WEF.")
                FieldsVarification = False
                If txtWEF.Enabled Then txtWEF.Focus()
                Exit Function
            End If
            If MainClass.ValidDataInGrid(SprdMain, ColRMDesc, "S", "Item Name Is Blank") = False Then FieldsVarification = False : Exit Function
            If MainClass.ValidDataInGrid(SprdMain, ColRMRate, "N", "Please Check Rate") = False Then FieldsVarification = False : Exit Function
            If MainClass.ValidDataInGrid(SprdMain, ColRMUOM, "S", "Please Check Unit") = False Then FieldsVarification = False : Exit Function

            If MainClass.ValidDataInGrid(SprdMain, ColWtPerStrip, "N", "Please Check Wt per Strip") = False Then FieldsVarification = False : Exit Function
            If MainClass.ValidDataInGrid(SprdMain, ColQtyPerStrip, "N", "Please Check Qty per Strip") = False Then FieldsVarification = False : Exit Function

        End If


        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function

    Private Sub chkStatus_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkStatus.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkV2V_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkV2V.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        If chkV2V.CheckState = System.Windows.Forms.CheckState.Checked Then ''txtNetBOPCost.va					
            txtNetBOPCost.Enabled = True
        Else
            txtNetBOPCost.Enabled = False
        End If
    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()

            txtSupplierCode.Enabled = True
            cmdSearchSupplier.Enabled = True

            txtItemCode.Enabled = True
            cmdSearchItemCode.Enabled = True
            cmdSearchWEF.Enabled = True
            SprdMain.Enabled = True
            SprdPart.Enabled = True
            SprdProcess1.Enabled = True
            SprdProcess2.Enabled = True
            SprdMainOperation.Enabled = True
            SprdCostingExp.Enabled = True
            txtCopyFrom.Enabled = True
            cmdSearchCopy.Enabled = True
        Else
            CmdAdd.Text = ConCmdAddCaption
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
            txtCopyFrom.Enabled = False
            cmdSearchCopy.Enabled = False
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdAmend_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAmend.Click

        Dim mItemCode As String
        Dim mSupplierCode As String
        Dim i As Integer

        mSupplierCode = Trim(txtSupplierCode.Text)

        If mSupplierCode = "" Then
            MsgInformation("Please Select Supplier")
            Exit Sub
        End If

        Call txtSupplierCode_Validating(txtSupplierCode, New System.ComponentModel.CancelEventArgs(True))

        mItemCode = Trim(txtItemCode.Text)

        If mItemCode = "" Then
            MsgInformation("Please Select Item")
            Exit Sub
        End If

        Call txtItemCode_Validating(txtItemCode, New System.ComponentModel.CancelEventArgs(True))

        txtAmendNo.Text = CStr(GetMaxAmendNo(mSupplierCode, mItemCode))
        chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked


        mAmendStatus = True
        cmdAmend.Enabled = False
        txtWEF.Enabled = True
        SprdMain.Enabled = True
        SprdPart.Enabled = True
        SprdProcess1.Enabled = True
        SprdProcess2.Enabled = True
        SprdMainOperation.Enabled = True
        SprdCostingExp.Enabled = True
        chkV2V.Enabled = False

        ADDMode = True
        MODIFYMode = False

        MainClass.ButtonStatus(Me, XRIGHT, RsCostMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Function GetMaxAmendNo(ByRef pSupplierCode As String, ByRef pItemCode As String) As Integer

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        SqlStr = " SELECT MAX(AMEND_NO) AS AMEND_NO" & vbCrLf _
        & " FROM PRD_BOP_COST_HDR" & vbCrLf _
        & " WHERE " & vbCrLf _
        & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        & " AND SUPP_CUST_CODE ='" & MainClass.AllowSingleQuote(pSupplierCode) & "'" & vbCrLf _
        & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

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
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click

        On Error GoTo DelErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset


        If chkStatus.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Closed Costing Cann't be Deleted")
            Exit Sub
        End If

        If Trim(txtSupplierCode.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If Trim(txtItemCode.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub

        mSqlStr = "SELECT IH.AUTO_KEY_PO, IH.AMEND_NO  " & vbCrLf & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID" & vbCrLf & " WHERE IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtSupplierCode.Text) & "'" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(txtItemCode.Text) & "'" & vbCrLf & " AND ID.PO_WEF_DATE=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            MsgInformation("Po Made Agt this Costing, So cann't be deleted. PO NO :" & IIf(IsDBNull(RsTemp.Fields("AUTO_KEY_PO").Value), "", RsTemp.Fields("AUTO_KEY_PO").Value))
            Exit Sub
        End If

        If Not RsCostMain.EOF Then
            If MsgQuestion("Want to Delete ?") = CStr(MsgBoxResult.Yes) Then
                PubDBCn.Cancel()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "PRD_BOP_COST_HDR", (txtItemCode.Text), RsCostMain) = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "PRD_BOP_COST_HDR", "MKEY", (lblMKey.Text)) = False Then GoTo DelErrPart

                '            If DelBOMOperationOnRM = False Then GoTo DelErrPart:					
                PubDBCn.Execute("DELETE FROM PRD_BOP_OPERATION_DET WHERE UPPER(LTRIM(RTRIM(Mkey)))='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")
                PubDBCn.Execute("DELETE FROM PRD_BOP_EXP_COST_DET WHERE UPPER(LTRIM(RTRIM(Mkey)))='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")

                PubDBCn.Execute("DELETE FROM PRD_BOP_PROCESS1_DET  WHERE UPPER(LTRIM(RTRIM(Mkey)))='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")
                PubDBCn.Execute("DELETE FROM PRD_BOP_PROCESS2_DET  WHERE UPPER(LTRIM(RTRIM(Mkey)))='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")
                PubDBCn.Execute("DELETE FROM PRD_BOP_PART_DET WHERE UPPER(LTRIM(RTRIM(Mkey)))='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")
                PubDBCn.Execute("DELETE FROM PRD_BOP_COST_DET WHERE UPPER(LTRIM(RTRIM(Mkey)))='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")
                PubDBCn.Execute("DELETE FROM PRD_BOP_COST_HDR  WHERE UPPER(LTRIM(RTRIM(Mkey)))='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")

                If Val(txtAmendNo.Text) > 0 Then
                    If UpdatePreviousCost((txtSupplierCode.Text), (txtItemCode.Text), Val(txtAmendNo.Text), "O") = False Then GoTo DelErrPart
                End If

                PubDBCn.CommitTrans()
                RsCostMain.Requery()
                RsCostDetail.Requery()
                RsPartDetail.Requery()

                RsProcess1Detail.Requery()
                RsProcess2Detail.Requery()
                RsOprnDetail.Requery()
                RsConsDetail.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        RsCostMain.Requery()
        RsCostDetail.Requery()
        RsPartDetail.Requery()
        RsProcess1Detail.Requery()
        RsProcess2Detail.Requery()
        RsOprnDetail.Requery()
        RsConsDetail.Requery()

        PubDBCn.Cancel()
        MsgBox(Err.Description)
    End Sub
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsCostMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

            txtSupplierCode.Enabled = False
            cmdSearchSupplier.Enabled = False

            txtItemCode.Enabled = False
            cmdSearchItemCode.Enabled = False
            SprdMain.Enabled = True
            SprdPart.Enabled = True
            SprdProcess1.Enabled = True
            SprdProcess2.Enabled = True
            SprdMainOperation.Enabled = True
            SprdCostingExp.Enabled = True
        Else
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
        '    Resume					
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnCosting((lblMKey.Text), Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnCosting((lblMKey.Text), Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportOnCosting(ByRef nMkey As String, ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim SqlStr1 As String
        Dim SqlStr2 As String
        Dim SqlStr3 As String
        Dim SqlStr4 As String
        Dim SqlStr5 As String
        Dim SqlStr6 As String
        Dim mTitle As String
        Dim mSubTitle As String

        Dim SubSqlStr1 As String
        Dim SubSqlStr2 As String
        Dim RsTemp1 As ADODB.Recordset
        Dim RsTemp2 As ADODB.Recordset
        Dim mSuppCustCode As String
        Dim mItemCode As String

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
            txtSupplierCode_Validating(txtSupplierCode, New System.ComponentModel.CancelEventArgs(False))
            txtCopyFrom.Enabled = False
            cmdSearchCopy.Enabled = False
            If CmdAdd.Enabled = True And CmdAdd.Visible = True Then CmdAdd.Focus()
        Else
            MsgInformation("Record not saved")
            GoTo ErrorHandler
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        If Err.Description = "" Then Exit Sub
        MsgBox(Err.Description)
        '    Resume					
    End Sub
    Private Sub cmdSearchAppBy_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchAppBy.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If ADDMode = True Then
            SqlStr = SqlStr & " AND EMP_LEAVE_DATE IS NULL "
        End If

        If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            txtApprovedBy.Text = AcName1
            lblApprovedBy.Text = AcName
        End If

    End Sub

    Private Sub cmdSearchCopy_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchCopy.Click
        Dim mSqlStr As String

        mSqlStr = " SELECT IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, IH.ITEM_CODE, IH.WEF " & vbCrLf & " FROM PRD_BOP_COST_HDR IH, FIN_SUPP_CUST_MST CMST " & vbCrLf & " WHERE IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND IH.ITEM_CODE='" & Trim(txtItemCode.Text) & "'" & vbCrLf & " AND IH.STATUS='O'"

        mSqlStr = mSqlStr & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMasterBySQL2("", mSqlStr) = True Then
            txtCopyFrom.Text = AcName
            If txtCopyFrom.Enabled = True Then txtCopyFrom.Focus()
        End If
    End Sub

    Private Sub cmdSearchItemCode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchItemCode.Click
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS = 'A'"

        If MainClass.SearchGridMaster("", "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr) = True Then
            txtItemCode.Text = AcName1
            txtItemDesc.Text = AcName
            If txtItemCode.Enabled = True Then txtItemCode.Focus()
        End If
    End Sub
    Private Sub cmdSearchPrepBy_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchPrepBy.Click
        On Error GoTo SrchERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If ADDMode = True Then
            SqlStr = SqlStr & " AND EMP_LEAVE_DATE IS NULL "
        End If

        If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            txtPreparedBy.Text = AcName1
            lblPreparedBy.Text = AcName
        End If
        Exit Sub
SrchERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchSupplier_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchSupplier.Click
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE = 'S'"

        If MainClass.SearchGridMaster("", "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            txtSupplierCode.Text = AcName1
            txtSupplierName.Text = AcName
            If txtSupplierCode.Enabled = True Then txtSupplierCode.Focus()
        End If
    End Sub

    Private Sub cmdSearchWEF_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchWEF.Click
        Dim mSqlStr As String

        If Trim(txtSupplierCode.Text) = "" Then
            MsgInformation("Please Select the Supplier Name.")
            Exit Sub
        End If

        mSqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If Trim(txtSupplierCode.Text) <> "" Then
            mSqlStr = mSqlStr & " AND SUPP_CUST_CODE='" & Trim(txtSupplierCode.Text) & "'"
        End If

        If Trim(txtItemCode.Text) <> "" Then
            mSqlStr = mSqlStr & " AND ITEM_CODE='" & Trim(txtItemCode.Text) & "'"
        End If

        If MainClass.SearchGridMaster("", "PRD_BOP_COST_HDR", "WEF", "ITEM_CODE", "", "", mSqlStr) = True Then
            txtWEF.Text = Format(AcName, "DD/MM/YYYY")
            txtItemCode.Text = AcName1
            If txtWEF.Enabled = True Then txtWEF.Focus()
        End If
    End Sub
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Private Sub FrmRMCosting_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String
        If FormActive = True Then Exit Sub
        Me.Text = "BOP Items Costing"

        SqlStr = ""
        SqlStr = "Select * from PRD_BOP_COST_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCostMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from PRD_BOP_COST_DET  Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCostDetail, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from PRD_BOP_PART_DET  Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPartDetail, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from PRD_BOP_PROCESS1_DET  Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProcess1Detail, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from PRD_BOP_PROCESS2_DET  Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProcess2Detail, ADODB.LockTypeEnum.adLockReadOnly)


        SqlStr = ""
        SqlStr = "Select * from PRD_BOP_OPERATION_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOprnDetail, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from PRD_BOP_EXP_COST_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsConsDetail, ADODB.LockTypeEnum.adLockReadOnly)

        AssignGrid(False)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        SetTextLengths()

        If CmdAdd.Enabled = True Then cmdAdd_Click(CmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub FrmRMCosting_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub FrmRMCosting_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub FrmRMCosting_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        Dim mDocNo As String
        Dim mDateOrg As String
        Dim mRevNo As String
        Dim mDateRev As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection					
        'PvtDBCn.Open StrConn					

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        '    MainClass.SetReportDocDetail myMenu, PubDBCn, mDocNo, mDateOrg, mRevNo, mDateRev					
        '    lblDocNo.text = mDocNo					
        '    lblDateOrig.text = Format(mDateOrg, "DD/MM/YYYY					
        '    lblRevNo.text = mRevNo					
        '    lblDateRev.text = Format(mDateRev, "DD/MM/YYYY					

        CurrFormHeight = 7245
        CurrFormWidth = 11355

        ADDMode = False
        MODIFYMode = False
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7590)					
        'Me.Width = VB6.TwipsToPixelsX(11385)					

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        With RsCostMain
            txtSupplierCode.MaxLength = .Fields("SUPP_CUST_CODE").DefinedSize
            txtItemCode.MaxLength = .Fields("ITEM_CODE").DefinedSize
            txtWEF.MaxLength = 10
            TxtRemarks.MaxLength = .Fields("REMARKS").DefinedSize
            txtPreparedBy.MaxLength = .Fields("PREPARED_BY").DefinedSize
            txtApprovedBy.MaxLength = .Fields("APP_EMP_CODE").DefinedSize

            txtToolCost.MaxLength = .Fields("TOOL_COST").Precision
            txtToolQty.MaxLength = .Fields("TOOL_QTY").Precision
            txtToolCostPerPc.MaxLength = .Fields("TOOL_COST_PER_PC").Precision

        End With
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        '    Resume					
    End Sub
    Private Sub MakeEnableDesableField(ByRef mMode As Boolean)
        txtSupplierCode.Enabled = mMode
        txtItemCode.Enabled = mMode
        cmdSearchItemCode.Enabled = mMode
        txtWEF.Enabled = mMode

        txtPreparedBy.Enabled = mMode
        cmdSearchPrepBy.Enabled = mMode
        txtItemDesc.Enabled = False
        txtUnit.Enabled = False

        txtCustPartNo.Enabled = False
        txtAmendNo.Enabled = False
    End Sub
    Private Sub FrmRMCosting_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False

        RsCostMain.Close()
        RsCostDetail.Close()
        RsPartDetail.Close()
        RsProcess1Detail.Close()
        RsProcess2Detail.Close()
        RsOprnDetail.Close()
        RsConsDetail.Close()

        'PvtDBCn.Close					
        RsCostMain = Nothing
        RsCostDetail = Nothing
        RsPartDetail = Nothing
        RsProcess1Detail = Nothing
        RsProcess2Detail = Nothing
        RsOprnDetail = Nothing
        RsConsDetail = Nothing

        'Set PvtDBCn = Nothing					
    End Sub
    Private Sub Clear1()

        lblMKey.Text = ""
        txtSupplierCode.Text = ""
        txtSupplierName.Text = ""

        txtCopyFrom.Text = ""
        txtItemCode.Text = ""
        txtItemDesc.Text = ""
        txtUnit.Text = ""
        txtWEF.Text = ""

        txtCustPartNo.Text = ""
        txtAmendNo.Text = "0"
        TxtRemarks.Text = ""
        txtPreparedBy.Text = ""
        lblPreparedBy.Text = ""
        txtApprovedBy.Text = ""
        lblApprovedBy.Text = ""

        txtGrossCost.Text = "0.00"
        txtScrapCost.Text = "0.00"
        txtNetCost.Text = "0.00"

        txtStdPartCost.Text = "0.00"
        txtProcessCost_A.Text = "0.00"
        txtProcessCost_B.Text = "0.00"

        txtNetBOPCost.Text = "0.00"
        txtOpeartionCost.Text = "0.00"
        txtOtherCost.Text = "0.00"

        txtGrossWt.Text = "0.00"
        txtScrapWt.Text = "0.00"
        txtNetWt.Text = "0.00"

        txtToolCost.Text = "0.00"
        txtToolQty.Text = "0.00"
        txtToolCostPerPc.Text = "0.00"

        chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkV2V.CheckState = System.Windows.Forms.CheckState.Unchecked
        txtNetBOPCost.Enabled = False
        chkV2V.Enabled = True

        MainClass.ClearGrid(SprdMain)
        FormatSprdMain(-1)

        MainClass.ClearGrid(SprdPart)
        FormatSprdPart(-1)

        MainClass.ClearGrid(SprdProcess1)
        FormatSprdProcess1(-1)

        MainClass.ClearGrid(SprdProcess2)
        FormatSprdProcess2(-1)

        MainClass.ClearGrid(SprdMainOperation)
        FormatSprdMainOperation(-1)

        MainClass.ClearGrid(SprdCostingExp)
        Call FillCostExp()
        FormatSprdCost(-1)

        mAmendStatus = False
        cmdAmend.Enabled = True



        SSTab1.SelectedIndex = 0
        Call MakeEnableDesableField(True)

        MainClass.ButtonStatus(Me, XRIGHT, RsCostMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub FormatSprdMain(ByRef mRow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer

        With SprdMain
            .Row = mRow
            .set_RowHeight(0, ConRowHeight * 2.5)
            .set_RowHeight(mRow, ConRowHeight)

            .Col = ColMannualCalc
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER

            .Col = ColRMDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = 255
            .set_ColWidth(.Col, 25)
            .ColsFrozen = ColRMDesc

            .Col = ColRMRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 3
            .TypeFloatMax = CDbl("9999999.999")
            .TypeFloatMin = CDbl("-9999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 8)

            .Col = ColRMUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = RsCostDetail.Fields("ISSUE_UOM").DefinedSize
            .set_ColWidth(.Col, 5)

            For cntCol = ColRMThick To ColRMDiaMeter
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatDecimalPlaces = 3
                .TypeFloatMax = CDbl("9999999.999")
                .TypeFloatMin = CDbl("-9999999.999")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(.Col, 8)
            Next

            For cntCol = ColWtPerStrip To ColNetRMCost
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatDecimalPlaces = 3
                .TypeFloatMax = CDbl("9999999.999")
                .TypeFloatMin = CDbl("-9999999.999")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(.Col, 8)
            Next

        End With

        '    MainClass.ProtectCell SprdMain, 1, SprdMain.MaxRows, ColRMDesc, ColRMDesc					
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColRMRate, ColRMUOM)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColScrapRate, ColScrapRate)

        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColWtPerStrip, ColWtPerStrip)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColWtPerPc, ColRMCost)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColScrapWt, ColScrapWt)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColScrapCost, ColNetRMCost)

        Call LockSprdMain()

        MainClass.SetSpreadColor(SprdMain, mRow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then
            RsCostDetail.Requery()
            '        Resume					
        End If
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub

    Private Sub LockSprdMain()

        On Error GoTo ERR1
        Dim i As Integer
        Dim mMannualCalc As Integer

        With SprdMain
            MainClass.UnProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)

            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColRMRate, ColRMUOM)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColScrapRate, ColScrapRate)

            '    MainClass.ProtectCell SprdMain, 1, SprdMain.MaxRows, ColWtPerStrip, ColWtPerStrip					
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColWtPerPc, ColRMCost)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColScrapWt, ColScrapWt)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColScrapCost, ColNetRMCost)



            For i = 1 To .MaxRows
                .Row = i
                .Col = ColMannualCalc
                mMannualCalc = CInt(.Value)
                If mMannualCalc = System.Windows.Forms.CheckState.Unchecked Then
                    .Row = i
                    .Row2 = i
                    .Col = ColWtPerStrip
                    .Col2 = ColWtPerStrip
                    .BlockMode = True
                    .Protect = True
                    .Lock = True
                    .BlockMode = False
                End If
            Next
        End With

        Exit Sub
ERR1:
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub FormatSprdPart(ByRef mRow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer

        With SprdPart
            .Row = mRow
            .set_RowHeight(0, ConRowHeight * 2.5)
            .set_RowHeight(mRow, ConRowHeight)


            .Col = ColPartDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = RsPartDetail.Fields("PART_DESC").DefinedSize
            .set_ColWidth(.Col, 25)

            .Col = ColPartNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsPartDetail.Fields("PART_NO").DefinedSize
            .set_ColWidth(.Col, 15)

            .Col = ColPartUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = RsPartDetail.Fields("PART_UOM").DefinedSize
            .set_ColWidth(.Col, 8)

            For cntCol = ColPartQty To ColPartCost
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMax = CDbl("9999999.99")
                .TypeFloatMin = CDbl("-9999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(.Col, 10)
            Next

        End With

        MainClass.ProtectCell(SprdPart, 1, SprdPart.MaxRows, ColPartCost, ColPartCost)
        '    MainClass.UnProtectCell SprdMain, 1, SprdPart.MaxRows, ColRMUOM, ColRMUOM					

        MainClass.SetSpreadColor(SprdPart, mRow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then
            RsPartDetail.Requery()
            '        Resume					
        End If
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub FormatSprdProcess1(ByRef mRow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer

        With SprdProcess1
            .Row = mRow
            .set_RowHeight(0, ConRowHeight * 2.5)
            .set_RowHeight(mRow, ConRowHeight)

            .Col = ColProcess1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("OPR_DESC", "PRD_OPR_MST", PubDBCn)
            .set_ColWidth(.Col, 30)

            .Col = ColMachCode1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsProcess1Detail.Fields("MACHINE_ITEM_CODE").DefinedSize
            .set_ColWidth(.Col, 7)
            .ColHidden = True


            .Col = ColMachDesc1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(.Col, 25)

            For cntCol = ColMachTonnes To ColCost1
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMax = CDbl("9999999.99")
                .TypeFloatMin = CDbl("-9999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(.Col, 8)
            Next

        End With

        'MainClass.ProtectCell(SprdProcess1, 1, SprdProcess1.MaxRows, ColMachDesc1, ColMachDesc1)
        MainClass.ProtectCell(SprdProcess1, 1, SprdProcess1.MaxRows, ColCost1, ColCost1)
        '    MainClass.UnProtectCell SprdMain, 1, SprdProcess1.MaxRows, ColRMUOM, ColRMUOM					

        MainClass.SetSpreadColor(SprdProcess1, mRow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then
            RsProcess1Detail.Requery()
            '        Resume					
        End If
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub FormatSprdMainOperation(ByRef mRow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer

        With SprdMainOperation
            .Row = mRow
            .set_RowHeight(0, ConRowHeight * 2.5)
            .set_RowHeight(mRow, ConRowHeight)

            .Col = ColOPRCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("OPR_CODE", "PRD_OPR_MST", PubDBCn)
            .set_ColWidth(.Col, 10)

            .Col = ColOprDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("OPR_DESC", "PRD_OPR_MST", PubDBCn)
            .set_ColWidth(.Col, 25)

            .Col = ColOprUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = RsOprnDetail.Fields("OPR_UOM").DefinedSize
            .set_ColWidth(.Col, 5)

            For cntCol = ColOprQty To ColOprCost
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMax = CDbl("9999999.99")
                .TypeFloatMin = CDbl("-9999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(.Col, 8)
            Next

        End With

        MainClass.ProtectCell(SprdMainOperation, 1, SprdMainOperation.MaxRows, ColOprDesc, ColOprDesc)
        MainClass.ProtectCell(SprdMainOperation, 1, SprdMainOperation.MaxRows, ColOprCost, ColOprCost)

        MainClass.SetSpreadColor(SprdMainOperation, mRow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then
            RsProcess1Detail.Requery()
            '        Resume					
        End If
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub

    Private Sub FormatSprdProcess2(ByRef mRow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer

        With SprdProcess2
            .Row = mRow
            .set_RowHeight(0, ConRowHeight * 2.5)
            .set_RowHeight(mRow, ConRowHeight)


            .Col = ColProcess2
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = RsProcess2Detail.Fields("PROCESS_DESC").DefinedSize
            .set_ColWidth(.Col, 30)

            .Col = ColPlantNo2
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsProcess2Detail.Fields("PLANT_NO").DefinedSize
            .set_ColWidth(.Col, 30)


            For cntCol = ColSurface2 To ColCost2
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMax = CDbl("9999999.99")
                .TypeFloatMin = CDbl("-9999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(.Col, 8)
            Next

        End With

        MainClass.ProtectCell(SprdProcess2, 1, SprdProcess2.MaxRows, ColCost2, ColCost2)
        '    MainClass.UnProtectCell SprdMain, 1, SprdProcess2.MaxRows, ColRMUOM, ColRMUOM					

        MainClass.SetSpreadColor(SprdProcess2, mRow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then
            RsProcess2Detail.Requery()
            '        Resume					
        End If
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub Show1()

        On Error GoTo ERR1

        With RsCostMain
            If Not .EOF Then
                ADDMode = False
                MODIFYMode = False
                lblMKey.Text = .Fields("mKey").Value
                mIsShowing = True

                txtSupplierCode.Text = IIf(IsDBNull(.Fields("SUPP_CUST_CODE").Value), "", .Fields("SUPP_CUST_CODE").Value)

                If MainClass.ValidateWithMasterTable(txtSupplierCode.Text, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIn_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='S'") = True Then
                    txtSupplierName.Text = MasterNo
                End If



                txtItemCode.Text = IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)

                If MainClass.ValidateWithMasterTable(txtItemCode.Text, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtItemDesc.Text = MasterNo
                End If

                If MainClass.ValidateWithMasterTable(txtItemCode.Text, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtUnit.Text = MasterNo
                End If


                txtWEF.Text = IIf(IsDBNull(.Fields("WEF").Value), "", .Fields("WEF").Value)

                '            If MainClass.ValidateWithMasterTable(txtItemCode.Text, "ITEM_CODE", "ITEM_MODEL", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then					
                '                txtModelNo.Text = MasterNo					
                '            End If					
                '					
                If MainClass.ValidateWithMasterTable(txtItemCode.Text, "ITEM_CODE", "CUSTOMER_PART_NO", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtCustPartNo.Text = MasterNo
                End If

                txtAmendNo.Text = IIf(IsDBNull(.Fields("AMEND_NO").Value), "", .Fields("AMEND_NO").Value)
                TxtRemarks.Text = IIf(IsDBNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)

                txtPreparedBy.Text = IIf(IsDBNull(.Fields("PREPARED_BY").Value), "", .Fields("PREPARED_BY").Value)
                txtPreparedBy_Validating(txtPreparedBy, New System.ComponentModel.CancelEventArgs(False))

                txtApprovedBy.Text = IIf(IsDBNull(.Fields("APP_EMP_CODE").Value), "", .Fields("APP_EMP_CODE").Value)
                txtApprovedBy_Validating(txtApprovedBy, New System.ComponentModel.CancelEventArgs(False))

                chkStatus.CheckState = IIf(.Fields("Status").Value = "O", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
                '            chkStatus.Enabled = IIf(!Status = "C", False, True)					

                chkV2V.CheckState = IIf(.Fields("V2V_SUPPLIER").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkV2V.Enabled = False
                txtNetBOPCost.Enabled = IIf(chkV2V.CheckState = System.Windows.Forms.CheckState.Checked, True, False)

                txtGrossCost.Text = IIf(IsDBNull(.Fields("RM_GROSSCOST").Value), "0", .Fields("RM_GROSSCOST").Value)
                txtScrapCost.Text = IIf(IsDBNull(.Fields("SCRAP_COST").Value), "0", .Fields("SCRAP_COST").Value)
                txtNetCost.Text = IIf(IsDBNull(.Fields("RM_NETCOST").Value), "0", .Fields("RM_NETCOST").Value)

                txtStdPartCost.Text = IIf(IsDBNull(.Fields("PART_COST").Value), "0", .Fields("PART_COST").Value)
                txtProcessCost_A.Text = IIf(IsDBNull(.Fields("PROCESS_A_COST").Value), "0", .Fields("PROCESS_A_COST").Value)
                txtProcessCost_B.Text = IIf(IsDBNull(.Fields("PROCESS_B_COST").Value), "0", .Fields("PROCESS_B_COST").Value)

                txtNetBOPCost.Text = IIf(IsDBNull(.Fields("NET_COST").Value), "0", .Fields("NET_COST").Value)
                txtOpeartionCost.Text = IIf(IsDBNull(.Fields("OPR_COST").Value), "0", .Fields("OPR_COST").Value)
                txtOtherCost.Text = IIf(IsDBNull(.Fields("OTHERCHARGES").Value), "0", .Fields("OTHERCHARGES").Value)

                txtGrossWt.Text = IIf(IsDBNull(.Fields("ITEM_GROSS_WT").Value), "0", .Fields("ITEM_GROSS_WT").Value)
                txtScrapWt.Text = IIf(IsDBNull(.Fields("ITEM_SCRAP_WT").Value), "0", .Fields("ITEM_SCRAP_WT").Value)
                txtNetWt.Text = IIf(IsDBNull(.Fields("ITEM_NET_WT").Value), "0", .Fields("ITEM_NET_WT").Value)

                txtToolCost.Text = IIf(IsDBNull(.Fields("TOOL_COST").Value), "0", .Fields("TOOL_COST").Value)
                txtToolQty.Text = IIf(IsDBNull(.Fields("TOOL_QTY").Value), "0", .Fields("TOOL_QTY").Value)
                txtToolCostPerPc.Text = IIf(IsDBNull(.Fields("TOOL_COST_PER_PC").Value), "0", .Fields("TOOL_COST_PER_PC").Value)

                cmdAmend.Enabled = IIf(.Fields("Status").Value = "C", False, True)

                Dim mRoundingType As String
                mRoundingType = IIf(IsDBNull(.Fields("ROUNDING_TYPE").Value), "N", .Fields("ROUNDING_TYPE").Value)
                If mRoundingType = "N" Then
                    optNone.Checked = True
                ElseIf mRoundingType = "U" Then
                    optRoundingUP.Checked = True
                Else
                    optRoundingDown.Checked = True
                End If

                txtDigit.Text = IIf(IsDBNull(.Fields("ROUNDING_DIGIT").Value), "N", .Fields("ROUNDING_DIGIT").Value)

                SSTab1.SelectedIndex = 0
                Call ShowDetail1((lblMKey.Text))

                SSTab1.SelectedIndex = 1
                Call ShowPartDetail1((lblMKey.Text))

                SSTab1.SelectedIndex = 2
                Call ShowProcess1Detail1((lblMKey.Text))

                SSTab1.SelectedIndex = 3
                Call ShowProcess2Detail1((lblMKey.Text))

                SSTab1.SelectedIndex = 4
                Call ShowOprDetail1((lblMKey.Text))

                SSTab1.SelectedIndex = 5
                Call ShowCostExpDetail1((lblMKey.Text))

                Call AutoCalc()
                SSTab1.SelectedIndex = 0
                Call MakeEnableDesableField(False)
                mIsShowing = False
            End If
        End With
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsCostMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        SprdMain.Enabled = True
        SprdPart.Enabled = False
        SprdProcess1.Enabled = False
        SprdProcess2.Enabled = False

        SprdMainOperation.Enabled = False
        SprdCostingExp.Enabled = False
        '    txtDeptCode.Enabled = False					
        '    cmdSearchDept.Enabled = False					

        txtSupplierCode.Enabled = False
        cmdSearchSupplier.Enabled = False

        txtItemCode.Enabled = False
        cmdSearchItemCode.Enabled = False
        cmdSearchWEF.Enabled = False
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
        '    Resume					
    End Sub
    Private Sub ShowCopy1(ByRef pRs As ADODB.Recordset)
        On Error GoTo ERR1
        Dim pMKey As String
        With pRs
            If Not .EOF Then
                pMKey = .Fields("mKey").Value
                txtToolCost.Text = IIf(IsDBNull(.Fields("TOOL_COST").Value), "0", .Fields("TOOL_COST").Value)
                txtToolQty.Text = IIf(IsDBNull(.Fields("TOOL_QTY").Value), "0", .Fields("TOOL_QTY").Value)
                txtToolCostPerPc.Text = IIf(IsDBNull(.Fields("TOOL_COST_PER_PC").Value), "0", .Fields("TOOL_COST_PER_PC").Value)

                Call ShowDetail1(pMKey)
                Call ShowPartDetail1(pMKey)
                Call ShowProcess1Detail1(pMKey)
                Call ShowProcess2Detail1(pMKey)

                Call ShowOprDetail1(pMKey)
                Call ShowCostExpDetail1(pMKey)

                Call AutoCalc()
                'Call MakeEnableDesableField(False)					
                mIsShowing = False
            End If
        End With
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
        '    Resume					
    End Sub
    Private Sub FillCostExp()

        On Error GoTo ERR1
        Dim RS As ADODB.Recordset
        Dim i As Integer
        Dim SqlStr As String

        MainClass.ClearGrid(SprdCostingExp)

        SqlStr = "Select * From PRD_COSTINGEXP_MST " & vbCrLf _
            & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND CATEGORY='B'"

        SqlStr = SqlStr & vbCrLf & "Order By PrintSequence"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            i = 0
            Do While Not RS.EOF
                i = i + 1

                SprdCostingExp.Row = i

                SprdCostingExp.Col = ColExpCode
                SprdCostingExp.Text = CStr(RS.Fields("Code").Value)

                SprdCostingExp.Col = ColExpName
                SprdCostingExp.Text = RS.Fields("Name").Value

                SprdCostingExp.Col = ColExpCalc
                SprdCostingExp.Text = RS.Fields("CALCULATION").Value

                SprdCostingExp.Col = ColExpAddDed
                SprdCostingExp.Text = RS.Fields("ADD_DED").Value


                SprdCostingExp.Col = ColExpPercent
                If ADDMode = True Then
                    SprdCostingExp.Text = Str(IIf(IsDBNull(RS.Fields("DefaultPercent").Value), 0, Str(RS.Fields("DefaultPercent").Value)))
                Else
                    SprdCostingExp.Text = ""
                End If

                SprdCostingExp.Col = ColExpAmt
                SprdCostingExp.Text = "0"

                '            SprdCostingExp.Col = ColExpAddDeduct					
                '            SprdCostingExp.Text = IIf(RS.Fields("Add_Ded").Value = "A", "A", "D")					
                '					
                '            SprdCostingExp.Col = ColExpIdent					
                '            SprdCostingExp.Text = IIf(IsNull(RS.Fields("Identification").Value), "OTR", RS.Fields("Identification").Value)					
                '            mIdentification = IIf(IsNull(RS.Fields("Identification").Value), "OTR", RS.Fields("Identification").Value)					

                RS.MoveNext()

                '            If RS.EOF = False Then					
                SprdCostingExp.MaxRows = SprdCostingExp.MaxRows + 1
                '            End If					
            Loop
        End If

        FormatSprdCost(-1)
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        '    Resume					
    End Sub

    Private Sub FormatSprdCost(ByRef mRow As Integer)

        On Error GoTo ERR1
        With SprdCostingExp
            .Row = mRow
            .set_RowHeight(0, ConRowHeight * 2.5)
            .set_RowHeight(mRow, ConRowHeight)

            .Col = ColExpName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = MainClass.SetMaxLength("NAME", "PRD_COSTINGEXP_MST", PubDBCn)
            .set_ColWidth(.Col, 25)

            .Col = ColExpCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsConsDetail.Fields("EXP_CODE").DefinedSize
            .set_ColWidth(.Col, 5)
            .ColHidden = True

            .Col = ColExpAddDed
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            '        .TypeEditLen = RsCostDetail.Fields("EXP_DESC").DefinedSize					
            .set_ColWidth(.Col, 5)
            .ColHidden = True

            .Col = ColExpPercent
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 3
            .TypeFloatMax = CDbl("9999999.999")
            .TypeFloatMin = CDbl("-9999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC

            .Col = ColExpAmt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 3
            .TypeFloatMax = CDbl("9999999.999")
            .TypeFloatMin = CDbl("-9999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC

            .Col = ColExpCalc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            '        .TypeEditLen = RsCostDetail.Fields("EXP_REMARKS").DefinedSize					
            .set_ColWidth(.Col, 10)
            .ColHidden = True

            .Col = ColExpRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = RsConsDetail.Fields("EXP_REMARKS").DefinedSize
            .set_ColWidth(.Col, 10)

        End With
        MainClass.ProtectCell(SprdCostingExp, 1, SprdCostingExp.MaxRows, ColExpName, ColExpName)
        MainClass.ProtectCell(SprdCostingExp, 1, SprdCostingExp.MaxRows, ColExpAddDed, ColExpCode)
        MainClass.ProtectCell(SprdCostingExp, 1, SprdCostingExp.MaxRows, ColExpCalc, ColExpCalc)

        MainClass.SetSpreadColor(SprdCostingExp, mRow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then
            RsConsDetail.Requery()
            '        Resume					
        End If
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub

    Private Sub ShowCostExpDetail1(ByRef pMKey As String)

        On Error GoTo ERR1
        Dim i As Integer
        Dim SqlStr As String
        Dim mItemCode As String
        Dim mCheckCode As String

        SqlStr = ""

        For i = 1 To SprdCostingExp.MaxRows

            SprdCostingExp.Row = i

            SprdCostingExp.Col = ColExpCode
            mCheckCode = Trim(SprdCostingExp.Text)

            If mCheckCode <> "" Then
                SqlStr = " SELECT * FROM PRD_BOP_EXP_COST_DET " & vbCrLf & " WHERE Mkey='" & MainClass.AllowSingleQuote(pMKey) & "'" & vbCrLf & " AND EXP_CODE='" & mCheckCode & " '"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsConsDetail, ADODB.LockTypeEnum.adLockReadOnly)


                If Not RsConsDetail.EOF Then

                    SprdCostingExp.Row = i

                    SprdCostingExp.Col = ColExpPercent
                    SprdCostingExp.Text = VB6.Format(IIf(IsDBNull(RsConsDetail.Fields("EXP_PERCENT").Value), 0, RsConsDetail.Fields("EXP_PERCENT").Value), "0.00")

                    SprdCostingExp.Col = ColExpAmt
                    SprdCostingExp.Text = VB6.Format(IIf(IsDBNull(RsConsDetail.Fields("EXP_AMOUNT").Value), 0, RsConsDetail.Fields("EXP_AMOUNT").Value), "0.000")

                    SprdCostingExp.Col = ColExpRemarks
                    SprdCostingExp.Text = IIf(IsDBNull(RsConsDetail.Fields("EXP_REMARKS").Value), "", RsConsDetail.Fields("EXP_REMARKS").Value)
                End If
            End If
        Next
        Exit Sub
ERR1:
        '    Resume					
        MsgBox(Err.Description)
    End Sub
    Private Function UpdateCostExpDetail1() As Boolean

        On Error GoTo UpdateDetail1
        Dim SqlStr As String
        Dim i As Integer
        Dim mExpCode As Double
        Dim mExpName As String
        Dim mAmt As Double
        Dim mRemarks As String
        Dim mExpPer As Double


        PubDBCn.Execute(" DELETE FROM PRD_BOP_EXP_COST_DET " & vbCrLf & " WHERE MKEY='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "' ")

        With SprdCostingExp
            For i = 1 To .MaxRows - 1
                .Row = i

                .Col = ColExpName
                mExpName = MainClass.AllowSingleQuote(.Text)

                If MainClass.ValidateWithMasterTable(mExpName, "NAME", "CODE", "PRD_COSTINGEXP_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND CATEGORY='B'") = True Then
                    mExpCode = Val(MasterNo)
                Else
                    mExpCode = -1
                End If


                .Col = ColExpPercent
                mExpPer = Val(.Text)

                .Col = ColExpAmt
                mAmt = Val(.Text)

                .Col = ColExpRemarks
                mRemarks = MainClass.AllowSingleQuote(.Text)

                SqlStr = ""
                If Val(mExpCode) > 0 Then

                    SqlStr = " INSERT INTO  PRD_BOP_EXP_COST_DET (" & vbCrLf _
                        & " MKEY, COMPANY_CODE, SUPP_CUST_CODE, ITEM_CODE, " & vbCrLf _
                        & " SUBROWNO, EXP_CODE, EXP_PERCENT, EXP_AMOUNT, " & vbCrLf _
                        & " EXP_REMARKS " & vbCrLf _
                        & " ) VALUES ( "

                    SqlStr = SqlStr & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(lblMKey.Text) & "', " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(txtSupplierCode.Text) & "', '" & MainClass.AllowSingleQuote(txtItemCode.Text) & "', " & vbCrLf _
                        & " " & i & ", '" & mExpCode & "'," & mExpPer & ", " & mAmt & ", " & vbCrLf _
                        & " '" & mRemarks & "' )"

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With

        UpdateCostExpDetail1 = True
        Exit Function
UpdateDetail1:
        MsgBox(Err.Description)
        UpdateCostExpDetail1 = False
    End Function

    Private Sub SprdCostingExp_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdCostingExp.ClickEvent

        If eventArgs.col = 0 And eventArgs.row > 0 Then
            MainClass.DeleteSprdRow(SprdCostingExp, eventArgs.row, ColExpName)
        End If
        Call AutoCalc()
    End Sub
    Private Sub SprdCostingExp_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdCostingExp.LeaveCell

        On Error GoTo ErrPart
        Dim xDesc As String
        Dim xDeptCode As String
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset

        If eventArgs.newRow = -1 Then Exit Sub

        SprdCostingExp.Row = SprdCostingExp.ActiveRow

        Select Case eventArgs.col
            Case ColExpAmt
                SprdCostingExp.Row = SprdCostingExp.ActiveRow
                SprdCostingExp.Col = ColExpName

                If Trim(SprdCostingExp.Text) <> "" Then
                    SprdCostingExp.Col = ColExpAmt

                    If Val(SprdCostingExp.Text) <> 0 Then
                        MainClass.AddBlankSprdRow(SprdCostingExp, ColExpAmt, ConRowHeight)
                        FormatSprdCost((SprdCostingExp.MaxRows))
                    End If
                End If
        End Select
        Call AutoCalc()
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Function AutoCostExpCalc() As Double
        On Error GoTo AuERR
        Dim i As Integer
        Dim mExpCode As String
        Dim mAddDeduct As String
        Dim mCalcOn As String
        Dim j As Integer
        Dim mExpPercent As Double
        Dim mExpCost As Double
        Dim mExpCalcOn As Double

        AutoCostExpCalc = 0
        With SprdCostingExp
            For i = 1 To .MaxRows
                mExpCalcOn = 0
                mExpCost = 0

                .Row = i


                .Col = ColExpCode
                mExpCode = Trim(.Text)
                If mExpCode = "" Then GoTo NextLoop

                .Col = ColExpPercent
                mExpPercent = Val(.Text)

                .Col = ColExpAddDed
                mAddDeduct = Trim(.Text)

                .Col = ColExpCalc
                mCalcOn = Trim(.Text)

                If mExpPercent = 0 Then
                    .Col = ColExpAmt
                    mExpCost = Val(.Text)
                Else
                    For j = 0 To 5
                        '                    mStrFound = InStr(1, mCalcOn, I & ",")					
                        If InStr(1, mCalcOn, j & ",") > 0 Then
                            If j = 0 Then
                                mExpCalcOn = mExpCalcOn + Val(txtNetCost.Text)
                            ElseIf j = 1 Then
                                mExpCalcOn = mExpCalcOn + Val(txtStdPartCost.Text)
                            ElseIf j = 2 Then
                                mExpCalcOn = mExpCalcOn + Val(txtProcessCost_A.Text)
                            ElseIf j = 3 Then
                                mExpCalcOn = mExpCalcOn + Val(txtProcessCost_B.Text)
                            ElseIf j = 4 Then
                                mExpCalcOn = mExpCalcOn + Val(txtOpeartionCost.Text)
                            ElseIf j = 5 Then
                                mExpCalcOn = 0
                            End If
                        End If
                    Next
                    mExpCost = CDbl(VB6.Format(mExpCalcOn * mExpPercent * 0.01, "0.000"))
                    .Col = ColExpAmt
                    .Text = CStr(mExpCost)
                End If

                AutoCostExpCalc = AutoCostExpCalc + mExpCost
NextLoop:
            Next
        End With

        Exit Function
AuERR:
        '    Resume					
        MsgBox(Err.Description)
    End Function
    Private Function Update1() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim nMkey As String
        Dim mRowNo As Integer
        Dim mStatus As String
        Dim mV2V As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mStatus = IIf(chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked, "O", "C")
        mV2V = IIf(chkV2V.CheckState = System.Windows.Forms.CheckState.Unchecked, "N", "Y")

        Dim mRoundingType As String
        mRoundingType = IIf(optNone.Checked = True, "N", IIf(optRoundingUP.Checked = True, "U", "D"))

        SqlStr = ""
        If ADDMode = True Then
            mRowNo = MainClass.AutoGenRowNo("PRD_BOP_COST_HDR", "RowNo", PubDBCn)
            nMkey = VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00") & mRowNo & UCase(Trim(txtSupplierCode.Text)) & UCase(Trim(txtItemCode.Text)) & VB6.Format(txtWEF.Text, "YYYYMMDD")
            lblMKey.Text = nMkey

            SqlStr = " INSERT INTO PRD_BOP_COST_HDR ( " & vbCrLf _
                & " MKEY, COMPANY_CODE, ROWNO, " & vbCrLf _
                & " SUPP_CUST_CODE, ITEM_CODE, WEF, AMEND_NO, " & vbCrLf _
                & " RM_GROSSCOST, SCRAP_COST, RM_NETCOST, " & vbCrLf _
                & " PART_COST, PROCESS_A_COST, PROCESS_B_COST, " & vbCrLf _
                & " NET_COST, REMARKS, PREPARED_BY, " & vbCrLf _
                & " TOOL_COST, TOOL_QTY, TOOL_COST_PER_PC, " & vbCrLf _
                & " APP_EMP_CODE, STATUS, ADDUSER, " & vbCrLf _
                & " ADDDATE, MODUSER, MODDATE," & vbCrLf _
                & " RM_GROSSWT, RM_NETWT, OTHERCHARGES, OPR_COST, " & vbCrLf _
                & " ITEM_GROSS_WT, ITEM_SCRAP_WT, ITEM_NET_WT,V2V_SUPPLIER,ROUNDING_TYPE,ROUNDING_DIGIT" & vbCrLf _
                & " ) VALUES( "

            SqlStr = SqlStr & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(nMkey) & "'," & RsCompany.Fields("COMPANY_CODE").Value & ", " & mRowNo & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtSupplierCode.Text) & "', '" & MainClass.AllowSingleQuote(txtItemCode.Text) & "', TO_DATE('" & VB6.Format(txtWEF.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY'), " & Val(txtAmendNo.Text) & ", " & vbCrLf & " " & Val(txtGrossCost.Text) & ", " & Val(txtScrapCost.Text) & ", " & Val(txtNetCost.Text) & "," & vbCrLf & " " & Val(txtStdPartCost.Text) & ", " & Val(txtProcessCost_A.Text) & ", " & Val(txtProcessCost_B.Text) & "," & vbCrLf & " " & Val(txtNetBOPCost.Text) & ", '" & MainClass.AllowSingleQuote(TxtRemarks.Text) & "', '" & MainClass.AllowSingleQuote(txtPreparedBy.Text) & "', " & vbCrLf & " " & Val(txtToolCost.Text) & ", " & Val(txtToolQty.Text) & ", " & Val(txtToolCostPerPc.Text) & "," & vbCrLf & " '" & MainClass.AllowSingleQuote(txtApprovedBy.Text) & "', '" & mStatus & "', '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','', " & vbCrLf & " 0, 0, " & Val(txtOtherCost.Text) & ", " & Val(txtOpeartionCost.Text) & ", " & vbCrLf _
                & " " & Val(txtGrossWt.Text) & ", " & Val(txtScrapWt.Text) & ", " & Val(txtNetWt.Text) & ",'" & mV2V & "'," & vbCrLf _
                & " '" & mRoundingType & "', " & Val(txtDigit.Text) & ")"

        ElseIf MODIFYMode = True Then
            SqlStr = ""
            SqlStr = " UPDATE PRD_BOP_COST_HDR SET " & vbCrLf _
                & " SUPP_CUST_CODE = '" & MainClass.AllowSingleQuote(txtSupplierCode.Text) & "', " & vbCrLf _
                & " ITEM_CODE = '" & MainClass.AllowSingleQuote(txtItemCode.Text) & "', " & vbCrLf _
                & " WEF = TO_DATE('" & VB6.Format(txtWEF.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " AMEND_NO = " & Val(txtAmendNo.Text) & ", " & vbCrLf _
                & " RM_GROSSCOST = " & Val(txtGrossCost.Text) & ", " & vbCrLf _
                & " SCRAP_COST = " & Val(txtScrapCost.Text) & ", " & vbCrLf _
                & " RM_NETCOST = " & Val(txtNetCost.Text) & ", ROUNDING_TYPE='" & mRoundingType & "' , ROUNDING_DIGIT= " & Val(txtDigit.Text) & ", " & vbCrLf _
                & " TOOL_COST = " & Val(txtToolCost.Text) & ", " & vbCrLf & " TOOL_QTY = " & Val(txtToolQty.Text) & ", " & vbCrLf & " TOOL_COST_PER_PC = " & Val(txtToolCostPerPc.Text) & ", " & vbCrLf & " PART_COST = " & Val(txtStdPartCost.Text) & ", " & vbCrLf & " PROCESS_A_COST = " & Val(txtProcessCost_A.Text) & ", " & vbCrLf & " PROCESS_B_COST = " & Val(txtProcessCost_B.Text) & ", " & vbCrLf & " NET_COST = " & Val(txtNetBOPCost.Text) & ", " & vbCrLf & " REMARKS = '" & MainClass.AllowSingleQuote(TxtRemarks.Text) & "', " & vbCrLf & " PREPARED_BY = '" & MainClass.AllowSingleQuote(txtPreparedBy.Text) & "', " & vbCrLf & " APP_EMP_CODE = '" & MainClass.AllowSingleQuote(txtApprovedBy.Text) & "', " & vbCrLf & " STATUS = '" & mStatus & "', " & vbCrLf & " OTHERCHARGES=" & Val(txtOtherCost.Text) & ", OPR_COST=" & Val(txtOpeartionCost.Text) & ", " & vbCrLf & " ITEM_GROSS_WT=" & Val(txtGrossWt.Text) & ", ITEM_SCRAP_WT=" & Val(txtScrapWt.Text) & ", ITEM_NET_WT=" & Val(txtNetWt.Text) & "," & vbCrLf & " MODUSER = '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " MODDATE = TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), V2V_SUPPLIER='" & mV2V & "'" & vbCrLf & " WHERE Mkey ='" & MainClass.AllowSingleQuote(lblMKey.Text) & "'"

        End If



        PubDBCn.Execute(SqlStr)


        If UpdateDetail1() = False Then GoTo ErrPart


        If UpdatePartDetail1() = False Then GoTo ErrPart

        If UpdateProcess1Detail1() = False Then GoTo ErrPart
        If UpdateProcess2Detail1() = False Then GoTo ErrPart

        If UpdateOperationDetail1() = False Then GoTo ErrPart
        If UpdateCostExpDetail1() = False Then GoTo ErrPart

        If Val(txtAmendNo.Text) > 0 Then
            If UpdatePreviousCost((txtSupplierCode.Text), (txtItemCode.Text), Val(txtAmendNo.Text), "C") = False Then GoTo ErrPart
        End If


        '    If UpdateBOMOperationOnMP = False Then GoTo ErrPart					
        '    If UpdateBOMExp = False Then GoTo ErrPart					
        '    If mIsBOM = False Then					
        '        If UpdateBOMFinalCost = False Then GoTo ErrPart					
        '        If UpdateBOMMadeFlag(True) = False Then GoTo ErrPart					
        '    End If					
        Update1 = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsCostMain.Requery()
        RsCostDetail.Requery()
        RsPartDetail.Requery()
        RsProcess1Detail.Requery()
        RsProcess2Detail.Requery()
        RsOprnDetail.Requery()
        RsConsDetail.Requery()
        If Err.Description = "" Then Exit Function
        MsgBox(Err.Description)
        '    Resume					
    End Function
    Private Function UpdatePreviousCost(ByRef pSupplierCode As String, ByRef pItemCode As String, ByRef pAmendNo As Integer, ByRef pPreviousStatus As String) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String

        SqlStr = ""
        SqlStr = " UPDATE PRD_BOP_COST_HDR SET " & vbCrLf & " STATUS = '" & pPreviousStatus & "', " & vbCrLf & " MODUSER = '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " MODDATE = TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AMEND_NO = " & pAmendNo - 1 & "" & vbCrLf & " AND SUPP_CUST_CODE ='" & MainClass.AllowSingleQuote(pSupplierCode) & "'" & vbCrLf & " AND ITEM_CODE ='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        PubDBCn.Execute(SqlStr)

        UpdatePreviousCost = True

        Exit Function
ErrPart:
        UpdatePreviousCost = False
        If Err.Description = "" Then Exit Function
        MsgBox(Err.Description)
        '    Resume					
    End Function
    Private Sub ViewGrid()

        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        ADataMain.Refresh					
            FormatSprdView()
            SprdView.Refresh()

            SprdView.Focus()
            SprdView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            SprdView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsCostMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Function CheckQty(ByRef pSprd As Object, ByVal Col As Integer, ByVal Row As Integer) As Boolean

        On Error GoTo ERR1
        With pSprd
            .Row = Row
            .Col = Col
            If Val(.Text) > 0 Then
                CheckQty = True
            Else
                MainClass.SetFocusToCell(pSprd, Row, Col)
            End If
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function CheckUnit(ByRef pSprd As Object, ByVal Col As Integer, ByVal Row As Integer) As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE = 'U' "
        With pSprd
            .Row = Row
            .Col = Col
            If MainClass.ValidateWithMasterTable(.Text, "GEN_CODE", "GEN_DESC", "INV_GENERAL_MST", PubDBCn, MasterNo,  , SqlStr) = True Then
                CheckUnit = True
            Else
                MainClass.SetFocusToCell(pSprd, Row, Col)
            End If
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Sub SprdPart_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdPart.Change

        With SprdPart
            SprdPart_LeaveCell(SprdPart, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdPart_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdPart.ClickEvent

        If eventArgs.col = 0 And eventArgs.row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdPart, eventArgs.row, ColPartDesc, True)
            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
        Call AutoCalc()
    End Sub


    Private Sub SprdPart_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdPart.LeaveCell

        On Error GoTo ErrPart

        If eventArgs.newRow = -1 Then Exit Sub
        SprdPart.Row = eventArgs.row
        SprdPart.Col = ColPartDesc
        If Trim(SprdPart.Text) = "" Then Exit Sub

        Select Case eventArgs.col
'        Case ColRMCode					
'            SprdMain.Row = SprdMain.ActiveRow					
'					
'            SprdMain.Col = ColRMCode					
'            mRMCode = Trim(SprdMain.Text)					
'					
'            If Trim(txtItemCode.Text) = Trim(SprdMain.Text) Then					
'                MainClass.setfocusToCell SprdMain, SprdMain.ActiveRow, ColRMCode					
'            Else					
'                If CheckDuplicateItem(mRMCode) = False Then					
'                    SprdMain.Row = SprdMain.ActiveRow					
'                    SprdMain.Col = ColRMCode					
'                    Call FillGridRow(SprdMain.Text)					
'                Else					
'                    MainClass.setfocusToCell SprdMain, SprdMain.ActiveRow, ColRMCode					
'                End If					
'            End If					
            Case ColPartQty
                If CheckQty(SprdPart, eventArgs.col, eventArgs.row) = True Then
                    MainClass.AddBlankSprdRow(SprdPart, ColPartDesc, ConRowHeight)
                    FormatSprdPart((SprdPart.MaxRows))
                End If
        End Select

        Call AutoCalc()
        FormatSprdPart(-1)
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Sub SprdPart_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdPart.Leave
        With SprdPart
            SprdPart_LeaveCell(SprdPart, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub

    Private Sub SprdProcess1_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdProcess1.Change

        With SprdProcess1
            SprdProcess1_LeaveCell(SprdProcess1, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdProcess1_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdProcess1.ClickEvent

        Dim SqlStr As String
        Dim mMachineDesc As String
        Dim mOperationDesc As String

        'If eventArgs.row = 0 And eventArgs.col = ColMachCode1 Then
        '    With SprdProcess1
        '        SqlStr = "SELECT DISTINCT A.ITEM_CODE, A.ITEM_SHORT_DESC " & vbCrLf & " FROM INV_ITEM_MST A, MAN_MACHINE_MST B" & vbCrLf & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND A.ITEM_STATUS = 'A' " & vbCrLf & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf & " AND A.ITEM_CODE=B.MACHINE_ITEM_CODE " & vbCrLf & " ORDER BY A.ITEM_CODE "
        '        .Row = .ActiveRow
        '        .Col = ColMachCode1
        '        If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
        '            .Row = .ActiveRow

        '            .Col = ColMachCode1
        '            .Text = AcName

        '            .Col = ColMachDesc1
        '            .Text = AcName1
        '        End If
        '    End With
        'End If

        'If eventArgs.row = 0 And eventArgs.col = ColMachDesc1 Then
        '    With SprdProcess1
        '        SqlStr = "SELECT DISTINCT A.ITEM_SHORT_DESC, A.ITEM_CODE " & vbCrLf & " FROM INV_ITEM_MST A, MAN_MACHINE_MST B" & vbCrLf & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND A.ITEM_STATUS = 'A' " & vbCrLf & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf & " AND A.ITEM_CODE=B.MACHINE_ITEM_CODE " & vbCrLf & " ORDER BY A.ITEM_CODE "

        '        .Row = .ActiveRow

        '        .Col = ColMachDesc1
        '        mMachineDesc = .Text

        '        .Text = ""
        '        If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
        '            .Row = .ActiveRow

        '            .Col = ColMachDesc1
        '            .Text = AcName

        '            .Col = ColMachCode1
        '            .Text = AcName1
        '        Else
        '            .Row = .ActiveRow

        '            .Col = ColMachDesc1
        '            .Text = mMachineDesc
        '        End If
        '    End With
        'End If

        If eventArgs.row = 0 And eventArgs.col = ColProcess1 Then
            With SprdProcess1
                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  "

                .Row = .ActiveRow

                .Col = ColProcess1
                mOperationDesc = .Text

                .Text = ""
                If MainClass.SearchGridMaster(mOperationDesc, "PRD_OPR_MST", "OPR_DESC", "OPR_CODE", , , SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColProcess1
                    .Text = AcName

                Else
                    .Row = .ActiveRow

                    .Col = ColProcess1
                    .Text = mMachineDesc
                End If
                Call SprdProcess1_LeaveCell(SprdProcess1, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColProcess1, SprdProcess1.ActiveRow, ColProcess1, SprdProcess1.ActiveRow, False))
            End With
        End If

        If eventArgs.col = 0 And eventArgs.row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdProcess1, eventArgs.row, ColProcess1, True)
            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
        Call AutoCalc()
    End Sub
    Private Sub SprdProcess1_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdProcess1.KeyUpEvent
        Dim mCol As Short
        mCol = SprdProcess1.ActiveCol
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColProcess1 Then SprdProcess1_ClickEvent(SprdProcess1, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColProcess1, 0))
        'If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColMachCode1 Then SprdProcess1_ClickEvent(SprdProcess1, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColMachCode1, 0))
        'If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColMachDesc1 Then SprdProcess1_ClickEvent(SprdProcess1, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColMachDesc1, 0))
    End Sub
    Private Sub SprdProcess1_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdProcess1.LeaveCell

        On Error GoTo ErrPart
        Dim mMachineCode As String
        Dim mMCDesc As String
        Dim mOPNDesc As String
        Dim mCheckCode As String

        If eventArgs.newRow = -1 Then Exit Sub
        SprdProcess1.Row = eventArgs.row
        SprdProcess1.Col = ColProcess1
        If Trim(SprdProcess1.Text) = "" Then Exit Sub

        Select Case eventArgs.col
            Case ColProcess1
                SprdProcess1.Row = SprdProcess1.ActiveRow
                SprdProcess1.Col = ColProcess1
                mOPNDesc = MainClass.AllowSingleQuote(SprdProcess1.Text)

                If MainClass.ValidateWithMasterTable(mOPNDesc, "OPR_DESC", "OPR_CODE", "PRD_OPR_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                    MsgInformation("Invalid Process Code.")
                    MainClass.SetFocusToCell(SprdProcess1, SprdProcess1.ActiveRow, ColProcess1)
                    Exit Sub
                Else
                    MainClass.AddBlankSprdRow(SprdProcess1, ColProcess1, ConRowHeight)
                    FormatSprdProcess1((SprdProcess1.MaxRows))
                End If

            'Case ColMachCode1
            '    SprdProcess1.Row = SprdProcess1.ActiveRow

            '    SprdProcess1.Col = ColProcess1
            '    mCheckCode = UCase(Trim(SprdProcess1.Text))

            '    SprdProcess1.Col = ColMachCode1
            '    mCheckCode = mCheckCode & "|" & UCase(Trim(SprdProcess1.Text))
            '    mMachineCode = Trim(SprdProcess1.Text)

            '    If Trim(txtItemCode.Text) = Trim(SprdProcess1.Text) Then
            '        MainClass.SetFocusToCell(SprdProcess1, SprdProcess1.ActiveRow, ColMachCode1)
            '    Else
            '        If CheckDuplicateOperation(mCheckCode, SprdProcess1) = False Then
            '            SprdProcess1.Row = SprdProcess1.ActiveRow
            '            SprdProcess1.Col = ColMachCode1

            '            mMachineCode = Trim(SprdProcess1.Text)
            '            If Trim(mMachineCode) <> "" Then
            '                If MainClass.ValidateWithMasterTable(mMachineCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            '                    mMCDesc = MasterNo


            '                    SprdProcess1.Col = ColMachDesc1
            '                    SprdProcess1.Text = mMCDesc

            '                    MainClass.AddBlankSprdRow(SprdProcess1, ColMachCode1, ConRowHeight)
            '                    FormatSprdProcess1((SprdProcess1.MaxRows))
            '                Else
            '                    MsgInformation("Invalid Machine Code.")
            '                    MainClass.SetFocusToCell(SprdProcess1, SprdProcess1.ActiveRow, ColMachCode1)
            '                    Exit Sub
            '                End If
            '            End If
            '        Else
            '            MainClass.SetFocusToCell(SprdProcess1, SprdProcess1.ActiveRow, ColMachCode1)
            '        End If
            '    End If
            Case ColRate1
                If CheckQty(SprdProcess1, eventArgs.col, eventArgs.row) = True Then
                    MainClass.AddBlankSprdRow(SprdProcess1, ColProcess1, ConRowHeight)
                    FormatSprdProcess1((SprdProcess1.MaxRows))
                End If

                '        Case ColLengthRM					
                '            Call FillStripWidth					
                '        Case ColWidthRM					
                '            Call FillStripWidth					
                '        Case ColThicknessRM					
                '            Call FillStripWidth					
                '        Case ColMtrlCode					
                '            SprdProcess1.Row = SprdProcess1.ActiveRow					
                '            SprdProcess1.Col = ColMtrlCode					
                '            Call FillMtrlRow(SprdProcess1.Text)					
        End Select
        Call AutoCalc()
        FormatSprdProcess1(-1)
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub SprdProcess1_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdProcess1.Leave
        With SprdProcess1
            SprdProcess1_LeaveCell(SprdProcess1, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub
    Private Sub SprdProcess2_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdProcess2.Change

        With SprdProcess2
            SprdProcess2_LeaveCell(SprdProcess2, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdProcess2_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdProcess2.ClickEvent

        Dim SqlStr As String


        '    If Row = 0 And Col = ColProcess1 Then					
        '        With SprdProcess2					
        '            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  "					
        '					
        '            .Row = .ActiveRow					
        '					
        '            .Col = ColProcess1					
        '            mOperationDesc = .Text					
        '					
        '            .Text = ""					
        '            If MainClass.SearchGridMaster(.Text, "PRD_OPR_MST", "OPR_DESC", "OPR_CODE", , , SqlStr) = True Then					
        '                .Row = .ActiveRow					
        '					
        '                .Col = ColProcess1					
        '                .Text = AcName					
        '					
        '            Else					
        '                .Row = .ActiveRow					
        '					
        '                .Col = ColProcess1					
        '                .Text = mMachineDesc					
        '            End If					
        '            Call SprdProcess2_LeaveCell(ColProcess1, SprdProcess2.ActiveRow, ColProcess1, SprdProcess2.ActiveRow, False)					
        '        End With					
        '    End If					

        If eventArgs.col = 0 And eventArgs.row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdProcess2, eventArgs.row, ColProcess2, True)
            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
        Call AutoCalc()
    End Sub

    Private Sub SprdProcess2_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdProcess2.KeyUpEvent
        Dim mCol As Short
        mCol = SprdProcess2.ActiveCol
        'If KeyCode = vbKeyF1 And mCol = ColProcess1 Then SprdProcess2_Click ColProcess1, 0					
    End Sub

    Private Sub SprdProcess2_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdProcess2.LeaveCell

        On Error GoTo ErrPart
        Dim mProcess As String


        If eventArgs.newRow = -1 Then Exit Sub
        SprdProcess2.Row = eventArgs.row
        SprdProcess2.Col = ColRMDesc
        If Trim(SprdProcess2.Text) = "" Then Exit Sub

        Select Case eventArgs.col
            Case ColProcess2
                SprdProcess2.Row = SprdProcess2.ActiveRow

                SprdProcess2.Col = ColProcess2
                mProcess = Trim(SprdProcess2.Text)


                If CheckDuplicateItem(mProcess, ColProcess2, SprdProcess2) = False Then
                    SprdProcess2.Row = SprdProcess2.ActiveRow
                    SprdProcess2.Col = ColProcess2

                    mProcess = Trim(SprdProcess2.Text)

                    MainClass.AddBlankSprdRow(SprdProcess2, ColProcess2, ConRowHeight)
                    FormatSprdProcess2((SprdProcess2.MaxRows))

                Else
                    MainClass.SetFocusToCell(SprdProcess2, SprdProcess2.ActiveRow, ColProcess2)
                End If

            Case ColRate1
                If CheckQty(SprdProcess2, eventArgs.col, eventArgs.row) = True Then
                    MainClass.AddBlankSprdRow(SprdProcess2, ColProcess2, ConRowHeight)
                    FormatSprdProcess2((SprdProcess2.MaxRows))
                End If
        End Select
        Call AutoCalc()
        FormatSprdProcess2(-1)
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub SprdProcess2_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdProcess2.Leave
        With SprdProcess2
            SprdProcess2_LeaveCell(SprdProcess2, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub
    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick

        SprdView.Row = SprdView.ActiveRow

        SprdView.Col = 2
        txtSupplierCode.Text = Trim(SprdView.Text)

        SprdView.Col = 4
        txtItemCode.Text = Trim(SprdView.Text)

        SprdView.Col = 7
        txtWEF.Text = VB6.Format(SprdView.Text, "DD/MM/YYYY")

        txtItemCode_Validating(txtItemCode, New System.ComponentModel.CancelEventArgs(False))
        ViewGrid()
    End Sub
    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub
    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        With SprdMain
            SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim SqlStr As String
        Dim mRMName As String


        '    If Row = 0 And Col = ColRMCode Then					
        '        With SprdMain					
        '            SqlStr = "SELECT ITEM_CODE, ITEM_SHORT_DESC, ISSUE_UOM " & vbCrLf _					
        ''                    & " FROM INV_ITEM_MST " & vbCrLf _					
        ''                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS = 'A' " & vbCrLf _					
        ''                    & " ORDER BY ITEM_CODE "					
        '            .Row = .ActiveRow					
        '            .Col = ColRMCode					
        '            If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then					
        '                .Row = .ActiveRow					
        '					
        '                .Col = ColRMCode					
        '                .Text = AcName					
        '					
        '                .Col = ColRMDesc					
        '                .Text = AcName1					
        '					
        '                .Col = ColRMCode					
        '                Call FillGridRow(SprdMain.Text)					
        '            End If					
        '        End With					
        '    End If					
        '					
        '    If Row = 0 And Col = ColRMDesc Then					
        '        With SprdMain					
        '            SqlStr = "SELECT ITEM_SHORT_DESC, ITEM_CODE, ISSUE_UOM " & vbCrLf _					
        ''                    & " FROM INV_ITEM_MST " & vbCrLf _					
        ''                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS = 'A' " & vbCrLf _					
        ''                    & " ORDER BY IITEM_SHORT_DESC "					
        '					
        '            .Row = .ActiveRow					
        '					
        '            .Col = ColRMDesc					
        '            mRMName = .Text					
        '					
        '            .Text = ""					
        '            If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then					
        '                .Row = .ActiveRow					
        '					
        '                .Col = ColRMDesc					
        '                .Text = AcName					
        '					
        '                .Col = ColRMCode					
        '                .Text = AcName1					
        '            Else					
        '                .Row = .ActiveRow					
        '					
        '                .Col = ColRMDesc					
        '                .Text = mRMName					
        '            End If					
        '            .Col = ColRMCode					
        '            Call FillGridRow(SprdMain.Text)					
        '        End With					
        '    End If					

        '    If Row = 0 And Col = ColMtrlCode Then					
        '        With SprdMain					
        '            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "					
        '            .Row = .ActiveRow					
        '            .Col = ColMtrlCode					
        '            If MainClass.SearchGridMaster(.Text, "PRD_MTRL_MST", "MTRL_CODE", "MTRL_DESC", "MTRL_DENSITY", , SqlStr) = True Then					
        '                .Row = .ActiveRow					
        '					
        '                .Col = ColMtrlCode					
        '                .Text = AcName					
        '					
        '                .Col = ColMtrlDesc					
        '                .Text = AcName1					
        '            End If					
        '            Call SprdMain_LeaveCell(ColMtrlCode, SprdMain.ActiveRow, ColMtrlCode, SprdMain.ActiveRow, False)					
        '        End With					
        '    End If					

        If eventArgs.row = 0 And eventArgs.col = ColRMDesc Then
            With SprdMain
                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  "

                .Row = .ActiveRow

                .Col = ColRMDesc
                mRMName = .Text

                .Text = ""
                If MainClass.SearchGridMaster(.Text, "PRD_MTRL_MST", "MTRL_DESC", "MTRL_CODE", "MTRL_DENSITY", , SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColRMDesc
                    .Text = AcName

                    '                .Col = ColMtrlCode					
                    '                .Text = AcName1					
                Else
                    .Row = .ActiveRow

                    .Col = ColRMDesc
                    .Text = mRMName
                End If
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColRMDesc, SprdMain.ActiveRow, ColRMDesc, SprdMain.ActiveRow, False))
            End With
        End If

        If eventArgs.col = 0 And eventArgs.row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColRMDesc, True)
            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
        Call AutoCalc()
    End Sub
    Private Sub FillGridRow(ByRef mItemCode As String)

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim RsMisc As ADODB.Recordset
        Dim mSizeCode As Integer
        Dim mRate As Double
        Dim mCode As String
        Dim mDesc As String

        If Trim(mItemCode) = "" Then Exit Sub

        SqlStr = " SELECT MTRL_DESC,GRADE_UOM " & vbCrLf & " FROM PRD_MTRL_MST " & vbCrLf & " WHERE MTRL_DESC='" & MainClass.AllowSingleQuote(mItemCode) & "' " & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsMisc.EOF Then
            SprdMain.Row = SprdMain.ActiveRow
            With RsMisc
                SprdMain.Col = ColRMDesc
                SprdMain.Text = IIf(IsDBNull(.Fields("MTRL_DESC").Value), "", .Fields("MTRL_DESC").Value)
                mDesc = IIf(IsDBNull(.Fields("MTRL_DESC").Value), "", .Fields("MTRL_DESC").Value)
                mCode = ""
                If MainClass.ValidateWithMasterTable(mDesc, "MTRL_DESC", "MTRL_CODE", "PRD_MTRL_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mCode = Trim(MasterNo)
                End If

                SprdMain.Col = ColRMUOM
                SprdMain.Text = IIf(IsDBNull(.Fields("GRADE_UOM").Value), "", .Fields("GRADE_UOM").Value)

                mRate = GetLastestRate(mCode, "RM")
                SprdMain.Col = ColRMRate
                SprdMain.Text = VB6.Format(mRate, "0.000")

                mRate = GetLastestRate(mCode, "SC")
                SprdMain.Col = ColScrapRate
                SprdMain.Text = VB6.Format(mRate, "0.000")

            End With
        Else
            MsgInformation("Invalid Material Name")
            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColRMDesc)
        End If
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub

    Private Function GetLastestRate(ByRef mRMCode As String, ByRef pType As String) As Double


        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        GetLastestRate = 0
        SqlStr = ""

        If pType = "RM" Then
            SqlStr = " SELECT  RATE AS RATE "
        Else
            SqlStr = " SELECT  SCRAP_RATE AS RATE "
        End If

        SqlStr = SqlStr & vbCrLf _
        & " FROM PRD_RM_GRADE_RATE_DET" & vbCrLf _
        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        & " AND GRADE_CODE ='" & MainClass.AllowSingleQuote(mRMCode) & "'" & vbCrLf _
        & " AND SUPP_CUST_CODE ='" & MainClass.AllowSingleQuote(txtSupplierCode.Text) & "'" & vbCrLf _
        & " AND WEF_DATE =TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"



        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetLastestRate = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("Rate").Value), 0, RsTemp.Fields("Rate").Value), "0.00"))
        End If
        Exit Function
ErrPart:
        GetLastestRate = 0
        If Err.Description = "" Then Exit Function
        MsgBox(Err.Description)
        '    Resume					
    End Function
    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        'Dim mCol As Integer					
        '    mCol = SprdMain.ActiveCol					
        '    If KeyCode = vbKeyF1 And mCol = ColRMCode Then SprdMain_Click ColRMCode, 0					
        '    If KeyCode = vbKeyF1 And mCol = ColRMDesc Then SprdMain_Click ColRMDesc, 0					
    End Sub
    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim mRMCode As String

        If eventArgs.newRow = -1 Then Exit Sub
        SprdMain.Row = eventArgs.row
        SprdMain.Col = ColRMDesc
        If Trim(SprdMain.Text) = "" Then Exit Sub

        Select Case eventArgs.col
            Case ColMannualCalc
                Call LockSprdMain()
            Case ColRMDesc
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColRMDesc
                mRMCode = Trim(SprdMain.Text)

                If Trim(txtItemCode.Text) = Trim(SprdMain.Text) Then
                    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColRMDesc)
                Else
                    '                If CheckDuplicateItem(mRMCode, ColRMDesc, SprdMain) = False Then					
                    SprdMain.Row = SprdMain.ActiveRow
                    SprdMain.Col = ColRMDesc
                    Call FillGridRow((SprdMain.Text))
                    MainClass.AddBlankSprdRow(SprdMain, ColRMDesc, ConRowHeight)
                    FormatSprdMain((SprdMain.MaxRows))
                    '                Else					
                    '                    MainClass.setfocusToCell SprdMain, SprdMain.ActiveRow, ColRMDesc					
                    '                End If					
                End If
            Case ColRMRate
                If CheckQty(SprdMain, eventArgs.col, eventArgs.row) = True Then
                    MainClass.AddBlankSprdRow(SprdMain, ColRMDesc, ConRowHeight)
                    FormatSprdMain((SprdMain.MaxRows))
                End If

                '        Case ColLengthRM					
                '            Call FillStripWidth					
                '        Case ColWidthRM					
                '            Call FillStripWidth					
                '        Case ColThicknessRM					
                '            Call FillStripWidth					
                '        Case ColMtrlCode					
                '            SprdMain.Row = SprdMain.ActiveRow					
                '            SprdMain.Col = ColMtrlCode					
                '            Call FillMtrlRow(SprdMain.Text)					
        End Select
        FormatSprdMain(-1)
        Call AutoCalc()

        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Function CheckDuplicateItem(ByRef pCheckCode As String, ByRef pCol As Integer, ByRef pSprd As AxFPSpreadADO.AxfpSpread) As Boolean
        On Error GoTo ERR1
        Dim i As Integer
        Dim mItemRept As Integer

        If Trim(pCheckCode) = "" Then CheckDuplicateItem = False : Exit Function
        With pSprd
            For i = 1 To .MaxRows
                .Row = i
                .Col = pCol
                If UCase(Trim(.Text)) = UCase(Trim(pCheckCode)) Then
                    mItemRept = mItemRept + 1
                    If mItemRept > 1 Then
                        MsgInformation("Duplication Item.")
                        CheckDuplicateItem = True
                        Exit Function
                    End If
                End If
            Next
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function CheckDuplicateOperation(ByRef pCheckItem As String, ByRef pSprd As AxFPSpreadADO.AxfpSpread) As Boolean
        On Error GoTo ERR1
        Dim i As Integer
        Dim mItemRept As Integer
        Dim mItem As String

        If Trim(pCheckItem) = "" Then CheckDuplicateOperation = False : Exit Function

        With pSprd
            For i = 1 To .MaxRows
                .Row = i
                .Col = ColProcess1
                mItem = UCase(Trim(.Text))

                .Col = ColMachCode1
                mItem = mItem & "|" & UCase(Trim(.Text))

                If UCase(Trim(mItem)) = UCase(Trim(pCheckItem)) Then
                    mItemRept = mItemRept + 1
                    If mItemRept > 1 Then
                        MsgInformation("Duplication Process & Machine Code.")
                        CheckDuplicateOperation = True
                        Exit Function
                    End If
                End If
            Next
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub AutoCalc()
        On Error GoTo AuERR
        Dim i As Integer
        Dim mRMCode As String
        Dim mRMDesc As String
        Dim mRMRate As Double
        Dim mRMUOM As String
        Dim mRMThick As Double
        Dim mRMLenth As Double
        Dim mRMWidth As Double
        Dim mRMDiaMeter As Double
        Dim mWtPerStrip As Double
        Dim mQtyPerStrip As Double
        Dim mWtPerPc As Double
        Dim mRMCost As Double
        Dim mNetWt As Double
        Dim mScrapWt As Double
        Dim mScrapRate As Double
        Dim mScrapCost As Double
        Dim mNetRMCost As Double

        Dim mTotalGrossCost As Double
        Dim mTotalScrapCost As Double
        Dim mTotalNetCost As Double
        Dim mTotalPartCost As Double
        Dim mTotalProcessACost As Double
        Dim mTotalProcessBCost As Double
        Dim mTotalNetBOPCost As Double


        Dim mPartDesc As String
        Dim mPartNo As String
        Dim mPartUOM As String
        Dim mPartQty As Double
        Dim mPartRate As Double
        Dim mPartCost As Double

        Dim mProcess1Stroke As Double
        Dim mProcess1Rate As Double
        Dim mProcess1Cost As Double

        Dim mProcess2Surface As Double
        Dim mProcess2Rate As Double
        Dim mProcess2Cost As Double

        Dim mTotalGrossWt As Double
        Dim mTotalScrapWt As Double
        Dim mTotalNetWt As Double

        Dim mOprQty As Double
        Dim mOprRate As Double
        Dim mOprCost As Double
        Dim mTotalOprCost As Double
        Dim mTotExpAmount As Double
        Dim mDensity As Double
        Dim mMaterialType As String
        Dim mMannualCalc As String

        If chkV2V.CheckState = System.Windows.Forms.CheckState.Checked Then
            Exit Sub
        End If


        With SprdMain
            For i = 1 To .MaxRows
                .Row = i

                .Col = ColMannualCalc
                mMannualCalc = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                .Col = ColRMDesc
                If Trim(.Text) = "" Then GoTo NextLoop
                mRMDesc = Trim(.Text)

                mDensity = 0 '7.86					

                If MainClass.ValidateWithMasterTable(mRMDesc, "MTRL_DESC", "MTRL_DENSITY", "PRD_MTRL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDensity = Val(MasterNo)
                End If

                If MainClass.ValidateWithMasterTable(mRMDesc, "MTRL_DESC", "MTRL_TYPE", "PRD_MTRL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mMaterialType = Trim(MasterNo)
                End If

                .Col = ColRMRate
                mRMRate = Val(.Text)

                .Col = ColRMThick
                mRMThick = Val(.Text)

                .Col = ColRMLenth
                mRMLenth = Val(.Text)

                .Col = ColRMWidth
                mRMWidth = Val(.Text)

                .Col = ColRMDiaMeter
                mRMDiaMeter = Val(.Text)

                If mMannualCalc = "N" Then
                    If mRMThick <> 0 And mRMLenth <> 0 And mRMWidth <> 0 Then ''Sheet					
                        mWtPerStrip = CDbl(VB6.Format(mRMThick * mRMLenth * mRMWidth * mDensity / (1000), "0.000")) ''IN Grams					
                    ElseIf mRMThick <> 0 And mRMLenth <> 0 And mRMDiaMeter <> 0 Then
                        If mMaterialType = "ROUND PIPE" Then
                            mDensity = 0.024653
                            mWtPerStrip = CDbl(VB6.Format((mRMDiaMeter - mRMThick) * mRMThick * mRMLenth * mDensity, "0.0000")) ''IN Grams					
                        ElseIf mMaterialType = "ROD" Then
                            mWtPerStrip = CDbl(VB6.Format(mRMDiaMeter * mRMDiaMeter * (3.14 / 4) * 7.85 * mRMLenth / 1000, "0.000")) ''IN Grams					
                        Else
                            mWtPerStrip = CDbl(VB6.Format(3.14 * (mRMDiaMeter - mRMThick) * mRMLenth * mDensity, "0.000")) ''IN Grams					
                        End If
                    ElseIf mRMLenth <> 0 And mRMDiaMeter <> 0 Then
                        mWtPerStrip = CDbl(VB6.Format((3.14 / 4) * (mRMDiaMeter * mRMDiaMeter) * mRMLenth * mDensity / (1000), "0.000")) ''IN Grams					
                    End If

                    .Col = ColWtPerStrip
                    .Text = VB6.Format(mWtPerStrip, "0.000")
                Else
                    .Col = ColWtPerStrip
                    mWtPerStrip = CDbl(VB6.Format(Val(.Text), "0.000"))
                End If

                ''mWtPerStrip = mWtPerStrip * 1000 ''IN Grams ''24/12/2016					


                .Col = ColQtyPerStrip
                mQtyPerStrip = Val(.Text)

                If mQtyPerStrip = 0 Then GoTo NextLoop

                .Col = ColWtPerPc
                mWtPerPc = CDbl(VB6.Format(mWtPerStrip / mQtyPerStrip, "0.000"))
                .Text = VB6.Format(mWtPerPc, "0.000")

                .Col = ColRMCost
                mRMCost = CDbl(VB6.Format(mWtPerPc * mRMRate, "0.000"))
                mRMCost = mRMCost / 1000 ''In KGS					
                .Text = VB6.Format(mRMCost, "0.000")

                .Col = ColNetWt
                mNetWt = Val(.Text)

                .Col = ColScrapWt
                mScrapWt = CDbl(VB6.Format(mWtPerPc - mNetWt, "0.000"))
                .Text = VB6.Format(mScrapWt, "0.000")

                .Col = ColScrapRate
                mScrapRate = Val(.Text)

                .Col = ColScrapCost
                mScrapCost = CDbl(VB6.Format(mScrapWt * mScrapRate, "0.000"))
                mScrapCost = mScrapCost / 1000 ''In KGS					
                .Text = VB6.Format(mScrapCost, "0.000")

                .Col = ColNetRMCost
                mNetRMCost = CDbl(VB6.Format(mRMCost - mScrapCost, "0.000"))
                .Text = VB6.Format(mNetRMCost, "0.000")

                mTotalGrossCost = mTotalGrossCost + mRMCost
                mTotalScrapCost = mTotalScrapCost + mScrapCost
                mTotalNetCost = mTotalNetCost + mNetRMCost
                mTotalGrossWt = mTotalGrossWt + mWtPerPc
                mTotalScrapWt = mTotalScrapWt + mScrapWt
                mTotalNetWt = mTotalNetWt + mNetWt

NextLoop:
            Next
        End With


        With SprdPart
            For i = 1 To .MaxRows
                .Row = i

                .Col = ColPartDesc
                If Trim(.Text) = "" Then GoTo NextPartLoop

                .Col = ColPartQty
                mPartQty = Val(.Text)

                .Col = ColPartRate
                mPartRate = Val(.Text)

                .Col = ColPartCost
                mPartCost = CDbl(VB6.Format(mPartQty * mPartRate, "0.000"))
                .Text = VB6.Format(mPartCost, "0.000")

                mTotalPartCost = mTotalPartCost + mPartCost
NextPartLoop:
            Next
        End With


        With SprdProcess1
            For i = 1 To .MaxRows
                .Row = i

                .Col = ColProcess1
                If Trim(.Text) = "" Then GoTo NextProcessALoop

                .Col = ColStroke1
                mProcess1Stroke = Val(.Text)

                .Col = ColRate1
                mProcess1Rate = Val(.Text)

                .Col = ColCost1
                mProcess1Cost = CDbl(VB6.Format(mProcess1Stroke * mProcess1Rate, "0.000"))
                .Text = VB6.Format(mProcess1Cost, "0.000")

                mTotalProcessACost = mTotalProcessACost + mProcess1Cost
NextProcessALoop:
            Next
        End With


        With SprdProcess2
            For i = 1 To .MaxRows
                .Row = i

                .Col = ColProcess2
                If Trim(.Text) = "" Then GoTo NextProcessBLoop

                .Col = ColSurface2
                mProcess2Surface = Val(.Text)

                .Col = ColRate2
                mProcess2Rate = Val(.Text)

                .Col = ColCost2
                mProcess2Cost = CDbl(VB6.Format(mProcess2Surface * mProcess2Rate, "0.000"))
                .Text = VB6.Format(mProcess2Cost, "0.000")

                mTotalProcessBCost = mTotalProcessBCost + mProcess2Cost
NextProcessBLoop:
            Next
        End With


        With SprdMainOperation
            For i = 1 To .MaxRows
                .Row = i

                .Col = ColOPRCode
                If Trim(.Text) = "" Then GoTo NextOprLoop

                .Col = ColOprQty
                mOprQty = Val(.Text)

                .Col = ColOPRRate
                mOprRate = Val(.Text)

                .Col = ColOprCost
                mOprCost = CDbl(VB6.Format(mOprQty * mOprRate, "0.000"))
                .Text = VB6.Format(mOprCost, "0.000")

                mTotalOprCost = mTotalOprCost + mOprCost
NextOprLoop:
            Next
        End With


        txtGrossCost.Text = VB6.Format(mTotalGrossCost, "0.000")
        txtScrapCost.Text = VB6.Format(mTotalScrapCost, "0.000")
        txtNetCost.Text = VB6.Format(mTotalNetCost, "0.000")
        txtStdPartCost.Text = VB6.Format(mTotalPartCost, "0.000")
        txtProcessCost_A.Text = VB6.Format(mTotalProcessACost, "0.000")
        txtProcessCost_B.Text = VB6.Format(mTotalProcessBCost, "0.000")

        txtOpeartionCost.Text = VB6.Format(mTotalOprCost, "0.000")


        mTotExpAmount = AutoCostExpCalc()
        txtOtherCost.Text = VB6.Format(mTotExpAmount, "0.000")

        If Val(txtToolQty.Text) <> 0 Then
            txtToolCostPerPc.Text = VB6.Format(Val(txtToolCost.Text) / Val(txtToolQty.Text), "0.000")
        End If

        mTotalNetBOPCost = mTotalNetCost + mTotalPartCost + mTotalProcessACost + mTotalProcessBCost + mTotalOprCost + mTotExpAmount + Val(txtToolCostPerPc.Text)

        Dim mRounding As Integer
        mRounding = Val(txtDigit.Text)      'VB6.Format(mSeqNo, mFormat)

        If optNone.Checked = True Then
            txtNetBOPCost.Text = VB6.Format(mTotalNetBOPCost, "0.000")
        ElseIf optRoundingUP.Checked = True Then
            'Math.Ceiling(value * (10 ^ decimals)) / (10 ^ decimals)
            'txtNetBOPCost.Text = Math.Roundup(mTotalNetBOPCost, mRounding) 'Math.Round(2.56666666, 2)
            txtNetBOPCost.Text = Math.Ceiling(mTotalNetBOPCost * (10 ^ mRounding)) / (10 ^ mRounding)
        Else
            'theRounded = Math.Sign(theNumber) * Math.Floor(Math.Abs(theNumber) * 100) / 100.0
            txtNetBOPCost.Text = Math.Sign(mTotalNetBOPCost) * Math.Floor(Math.Abs(mTotalNetBOPCost * (10 ^ mRounding))) / (10 ^ mRounding) 'Math.Sign(mTotalNetBOPCost) * Math.Floor(Math.Abs(mTotalNetBOPCost) * 100) / 100.0
        End If


        txtGrossWt.Text = VB6.Format(mTotalGrossWt, "0.000")
        txtScrapWt.Text = VB6.Format(mTotalScrapWt, "0.000")
        txtNetWt.Text = VB6.Format(mTotalNetWt, "0.000")

        Exit Sub
AuERR:
        '    Resume					
        MsgBox(Err.Description)
    End Sub

    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        With SprdMain
            SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub
    Private Sub SprdMainOperation_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMainOperation.Change

        With SprdMainOperation
            SprdMainOperation_LeaveCell(SprdMainOperation, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdMainOperation_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMainOperation.ClickEvent

        Dim SqlStr As String

        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If eventArgs.row = 0 And eventArgs.col = ColOPRCode Then
            With SprdMainOperation
                .Row = .ActiveRow

                .Col = ColOPRCode
                If MainClass.SearchGridMaster(.Text, "PRD_OPR_MST", "OPR_CODE", "OPR_DESC", "", "", SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColOPRCode
                    .Text = Trim(AcName)

                    .Col = ColOprDesc
                    .Text = Trim(AcName1)
                End If
                Call SprdMainOperation_LeaveCell(SprdMainOperation, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColOPRCode, .ActiveRow, ColOPRCode, .ActiveRow, False))
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColOprDesc Then
            With SprdMainOperation
                .Row = .ActiveRow

                .Col = ColOprDesc
                If MainClass.SearchGridMaster("", "PRD_OPR_MST", "OPR_DESC", "OPR_CODE", "", "", SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColOPRCode
                    .Text = Trim(AcName1)

                    .Col = ColOprDesc
                    .Text = Trim(AcName)
                End If
                Call SprdMainOperation_LeaveCell(SprdMainOperation, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColOPRCode, .ActiveRow, ColOPRCode, .ActiveRow, False))
            End With
        End If

        '    If Row = 0 And Col = ColOPRUnit Then					
        '        With SprdMainOperation					
        '            'SqlStr = " UPPER(ItemType)='RAW' "					
        '            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE = 'U' "					
        '					
        '            .Row = .ActiveRow					
        '            .Col = ColOPRUnit					
        '            If MainClass.SearchGridMaster(.Text, "INV_GENERAL_MST", "GEN_CODE", "GEN_DESC", , , SqlStr) = True Then					
        '                .Row = .ActiveRow					
        '					
        '                .Col = ColOPRUnit					
        '                .Text = AcName					
        '            End If					
        '        End With					
        '    End If					
        If eventArgs.col = 0 And eventArgs.row > 0 Then
            MainClass.DeleteSprdRow(SprdMainOperation, eventArgs.row, ColOPRCode)
        End If
        Call AutoCalc()
    End Sub

    Private Sub SprdMainOperation_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMainOperation.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMainOperation.ActiveCol
        '    If KeyCode = vbKeyF1 And mCol = ColOPRCode Then SprdMainOperation_Click ColOPRCode, 0					
        '    If KeyCode = vbKeyF1 And mCol = ColOPRDesc Then SprdMainOperation_Click ColOPRDesc, 0					
        '    If KeyCode = vbKeyF1 And mCol = ColOPRUnit Then SprdMainOperation_Click ColOPRUnit, 0					
    End Sub

    Private Sub SprdMainOperation_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMainOperation.LeaveCell

        On Error GoTo ErrPart
        Dim xOPRCode As String

        If eventArgs.newRow = -1 Then Exit Sub

        SprdMainOperation.Row = SprdMainOperation.ActiveRow

        SprdMainOperation.Col = ColOPRCode
        xOPRCode = Trim(SprdMainOperation.Text)
        If xOPRCode = "" Then Exit Sub

        Select Case eventArgs.col
            Case ColOPRCode
                SprdMainOperation.Col = ColOPRCode
                xOPRCode = Trim(SprdMainOperation.Text)
                If xOPRCode = "" Then Exit Sub

                If MainClass.ValidateWithMasterTable(xOPRCode, "OPR_CODE", "OPR_DESC", "PRD_OPR_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    Call CheckDuplicasy(xOPRCode)
                Else
                    MsgInformation("Invalid Operation.")
                    eventArgs.cancel = True
                    Exit Sub
                End If

            Case ColOprQty
                If CheckQty(SprdMainOperation, eventArgs.col, eventArgs.row) = True Then
                    MainClass.AddBlankSprdRow(SprdMainOperation, ColOPRCode, ConRowHeight)
                    FormatSprdMainOperation((SprdMainOperation.MaxRows))
                End If
                '        Case ColOPRUnit					
                '            SprdMainOperation.Row = SprdMainOperation.ActiveRow					
                '            SprdMainOperation.Col = ColOPRUnit					
                '            If Trim(SprdMainOperation.Text) <> "" Then Call CheckUnit(SprdMainOperation, ColOPRUnit, SprdMainOperation.ActiveRow)					
        End Select
        Call AutoCalc()
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function CheckDuplicasy(ByRef pOPRCode As String) As Boolean

        On Error GoTo ERR1
        Dim i As Integer
        Dim xOPRCode As String
        Dim mItemRept As Integer

        If pOPRCode = "" Then CheckDuplicasy = False : Exit Function
        With SprdMainOperation
            For i = 1 To .MaxRows
                .Row = i

                .Col = ColOPRCode
                xOPRCode = .Text

                If UCase(Trim(xOPRCode)) = UCase(Trim(pOPRCode)) Then
                    mItemRept = mItemRept + 1
                    If mItemRept > 1 Then
                        CheckDuplicasy = True
                        MsgInformation("Duplicate Operation.")
                        MainClass.SetFocusToCell(SprdMainOperation, .ActiveRow, .ActiveCol)
                        Exit Function
                    End If
                End If
            Next
        End With
        Exit Function
ERR1:
        CheckDuplicasy = False
        MsgInformation(Err.Description)
    End Function
    Private Sub SprdMainOperation_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMainOperation.Leave
        With SprdMainOperation
            SprdMainOperation_LeaveCell(SprdMainOperation, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
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


    Private Sub txtApprovedBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtApprovedBy.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtApprovedBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtApprovedBy.DoubleClick
        Call cmdSearchAppBy_Click(cmdSearchAppBy, New System.EventArgs())
    End Sub

    Private Sub txtApprovedBy_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtApprovedBy.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtApprovedBy.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtApprovedBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtApprovedBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchAppBy_Click(cmdSearchAppBy, New System.EventArgs())
    End Sub

    Private Sub txtApprovedBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtApprovedBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim SqlStr As String
        If Trim(txtApprovedBy.Text) = "" Then GoTo EventExitSub
        txtApprovedBy.Text = VB6.Format(Trim(txtApprovedBy.Text), "000000")

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        If ADDMode = True Then
            SqlStr = SqlStr & " AND EMP_LEAVE_DATE IS NULL "
        End If

        If MainClass.ValidateWithMasterTable(txtApprovedBy.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("EMPLOYEE Does Not Exist In Master.")
            Cancel = True
        Else
            lblApprovedBy.Text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCopyFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCopyFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim RsTempCostMain As ADODB.Recordset
        Dim SqlStr As String
        Dim mProcessType As Integer

        If ADDMode = False Then GoTo EventExitSub
        If Trim(txtCopyFrom.Text) = "" Or Trim(txtItemCode.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtCopyFrom.Text, "SUPP_CUST_CODE", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            SqlStr = " SELECT * FROM PRD_BOP_COST_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCopyFrom.Text) & "' " & vbCrLf _
            & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(txtItemCode.Text) & "' " & vbCrLf _
            & " AND STATUS='O'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTempCostMain, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTempCostMain.EOF = False Then
                MainClass.ClearGrid(SprdMain)
                FormatSprdMain(-1)

                MainClass.ClearGrid(SprdPart)
                FormatSprdPart(-1)

                MainClass.ClearGrid(SprdProcess1)
                FormatSprdProcess1(-1)

                MainClass.ClearGrid(SprdProcess2)
                FormatSprdProcess2(-1)

                MainClass.ClearGrid(SprdMainOperation)
                FormatSprdMainOperation(-1)

                MainClass.ClearGrid(SprdCostingExp)
                Call FillCostExp()
                FormatSprdCost(-1)

                SSTab1.SelectedIndex = 0

                Call ShowCopy1(RsTempCostMain)
            Else
                MsgBox("Invaild Item Code.", MsgBoxStyle.Information)
                Cancel = True
                GoTo EventExitSub
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCustPartNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustPartNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCustPartNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCustPartNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCustPartNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtGrossCost_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGrossCost.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtGrossCost_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtGrossCost.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtItemDesc_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemDesc.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtItemDesc_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItemDesc.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtItemDesc.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtNetBOPCost_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNetBOPCost.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNetBOPCost_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNetBOPCost.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtNetCost_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNetCost.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNetCost_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNetCost.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtPreparedBy_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPreparedBy.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPreparedBy.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtProcessCost_A_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProcessCost_A.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtProcessCost_A_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtProcessCost_A.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtProcessCost_B_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProcessCost_B.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtProcessCost_B_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtProcessCost_B.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtRemarks_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtRemarks.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtRemarks.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtScrapCost_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtScrapCost.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtScrapCost_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtScrapCost.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtStdPartCost_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtStdPartCost.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtStdPartCost_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtStdPartCost.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtSupplierCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplierCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSupplierCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSupplierCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSupplierCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSupplierCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSupplierCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchSupplier_Click(cmdSearchSupplier, New System.EventArgs())
    End Sub

    Private Sub txtSupplierCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSupplierCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim mRs As ADODB.Recordset
        Dim SqlStr As String
        Dim xMkey As String
        If Trim(txtSupplierCode.Text) = "" Then GoTo EventExitSub


        SqlStr = " SELECT SUPP_CUST_CODE,SUPP_CUST_NAME  " & vbCrLf & " FROM FIN_SUPP_CUST_MST " & vbCrLf & " WHERE SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtSupplierCode.Text) & "'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE = 'S' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRs, ADODB.LockTypeEnum.adLockReadOnly)

        If Not mRs.EOF Then
            txtSupplierName.Text = IIf(IsDBNull(mRs.Fields("SUPP_CUST_NAME").Value), "", mRs.Fields("SUPP_CUST_NAME").Value)
        Else
            txtSupplierName.Text = ""
            MsgBox("Either Not In Master Or Not A Supplier Master.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

        If Trim(txtWEF.Text) = "" Then
            SqlStr = " SELECT MAX(WEF) AS WEF " & vbCrLf & " FROM PRD_BOP_COST_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & Trim(txtSupplierCode.Text) & "'" & vbCrLf & " AND ITEM_CODE='" & Trim(txtItemCode.Text) & "'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRs, ADODB.LockTypeEnum.adLockReadOnly)

            If Not mRs.EOF Then
                txtWEF.Text = VB6.Format(IIf(IsDBNull(mRs.Fields("WEF").Value), "", mRs.Fields("WEF").Value), "DD/MM/YYYY")
            End If
        End If

        '    If mIsShowing = False Then If ShowRecord = False Then Cancel = True					
        Call ShowRecord()
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
        Resume
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSupplierName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplierName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtSupplierName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSupplierName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSupplierName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtToolCost_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtToolCost.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        '    If Val(txtToolQty.Text) <> 0 Then					
        '        txtToolCostPerPc.Text = Format(Val(txtToolCost.Text) / Val(txtToolQty.Text), "0.00")					
        '    End If					

        AutoCalc()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtToolCostPerPc_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtToolCostPerPc.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtToolCostPerPc_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtToolCostPerPc.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtToolQty_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtToolQty.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtToolQty_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtToolQty.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtToolCost_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtToolCost.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtToolCost_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtToolCost.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtToolQty_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtToolQty.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        '    If Val(txtToolQty.Text) <> 0 Then					
        '        txtToolCostPerPc.Text = Format(Val(txtToolCost.Text) / Val(txtToolQty.Text), "0.00")					
        '    End If					
        AutoCalc()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtUnit_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtUnit.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtItemCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtItemCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItemCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtItemCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtItemCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtItemCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchItemCode_Click(cmdSearchItemCode, New System.EventArgs())
    End Sub

    Private Sub txtItemCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim mRs As ADODB.Recordset
        Dim SqlStr As String
        Dim xMkey As String
        If Trim(txtItemCode.Text) = "" Then GoTo EventExitSub


        SqlStr = " SELECT ITEM_SHORT_DESC,ISSUE_UOM,ITEM_MODEL,CUSTOMER_PART_NO " & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE ITEM_CODE='" & MainClass.AllowSingleQuote(txtItemCode.Text) & "'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS = 'A' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRs, ADODB.LockTypeEnum.adLockReadOnly)

        If Not mRs.EOF Then
            txtItemDesc.Text = IIf(IsDBNull(mRs.Fields("ITEM_SHORT_DESC").Value), "", mRs.Fields("ITEM_SHORT_DESC").Value)
            txtUnit.Text = IIf(IsDBNull(mRs.Fields("ISSUE_UOM").Value), "", mRs.Fields("ISSUE_UOM").Value)
            '        txtModelNo.Text = IIf(IsNull(mRs!ITEM_MODEL), "", mRs!ITEM_MODEL)					
            txtCustPartNo.Text = IIf(IsDBNull(mRs.Fields("CUSTOMER_PART_NO").Value), "", mRs.Fields("CUSTOMER_PART_NO").Value)
        Else
            txtItemDesc.Text = ""
            txtUnit.Text = ""
            '        txtModelNo.Text = ""					
            txtCustPartNo.Text = ""
            MsgBox("Either Not In Master Or Not A BOP Item.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

        If Trim(txtWEF.Text) = "" Then
            SqlStr = " SELECT MAX(WEF) AS WEF " & vbCrLf & " FROM PRD_BOP_COST_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & Trim(txtSupplierCode.Text) & "'" & vbCrLf & " AND ITEM_CODE='" & Trim(txtItemCode.Text) & "'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRs, ADODB.LockTypeEnum.adLockReadOnly)

            If Not mRs.EOF Then
                txtWEF.Text = VB6.Format(IIf(IsDBNull(mRs.Fields("WEF").Value), "", mRs.Fields("WEF").Value), "DD/MM/YYYY")
            End If
        End If

        '    If mIsShowing = False Then If ShowRecord = False Then Cancel = True					
        Call ShowRecord()
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
        '    Resume					
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPreparedBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPreparedBy.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPreparedBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPreparedBy.DoubleClick
        Call cmdSearchPrepBy_Click(cmdSearchPrepBy, New System.EventArgs())
    End Sub

    Private Sub txtPreparedBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPreparedBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchPrepBy_Click(cmdSearchPrepBy, New System.EventArgs())
    End Sub

    Private Sub txtPreparedBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPreparedBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValEMP
        Dim SqlStr As String
        If Trim(txtPreparedBy.Text) = "" Then GoTo EventExitSub
        txtPreparedBy.Text = VB6.Format(Trim(txtPreparedBy.Text), "000000")
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If ADDMode = True Then
            SqlStr = SqlStr & " AND EMP_LEAVE_DATE IS NULL "
        End If

        If MainClass.ValidateWithMasterTable(txtPreparedBy.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            If txtPreparedBy.Enabled = True Then
                MsgBox("Employee Does Not Exist In Master.")
                Cancel = True
            End If

        Else
            lblPreparedBy.Text = MasterNo
        End If
        GoTo EventExitSub
ValEMP:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtRemarks.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtUnit_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtUnit.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtUnit.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtWEF_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWEF.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtWEF_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtWEF.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchWEF_Click(cmdSearchWEF, New System.EventArgs())
    End Sub

    Private Sub txtWEF_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtWEF.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        '    If mIsShowing = False Then If ShowRecord = False Then Cancel = True					
        Call ShowRecord()
        eventArgs.Cancel = Cancel
    End Sub

    Private Function ShowRecord() As Boolean

        On Error GoTo ERR1
        Dim mRs As ADODB.Recordset
        Dim SqlStr As String
        Dim xMkey As String

        ShowRecord = True

        If Trim(txtWEF.Text) = "" Or Trim(txtItemCode.Text) = "" Or Trim(txtSupplierCode.Text) = "" Then Exit Function

        If IsDate(txtWEF.Text) = False Then
            MsgBox("Invalid Date")
            ShowRecord = False
        Else
            If MODIFYMode = True And RsCostMain.EOF = False Then xMkey = RsCostMain.Fields("mKey").Value
            SqlStr = " SELECT * FROM PRD_BOP_COST_HDR " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtSupplierCode.Text) & "' " & vbCrLf _
                    & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(txtItemCode.Text) & "' " & vbCrLf _
                    & " AND WEF=TO_DATE('" & VB6.Format((txtWEF.Text), "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCostMain, ADODB.LockTypeEnum.adLockReadOnly)
            If RsCostMain.EOF = False Then
                Clear1()
                Show1()
            Else
                If ADDMode = False And MODIFYMode = False Then
                    MsgBox("Costing Not Made For This Item In This Month. Click Add For New.", MsgBoxStyle.Information)
                    ShowRecord = False
                ElseIf MODIFYMode = True Then
                    SqlStr = "SELECT * FROM PRD_BOP_COST_HDR " & vbCrLf & " WHERE UPPER(LTRIM(RTRIM(MKey)))='" & MainClass.AllowSingleQuote(UCase(xMkey)) & "' " & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCostMain, ADODB.LockTypeEnum.adLockReadOnly)
                End If
            End If
        End If
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function ValidateWEFDate() As Boolean

        On Error GoTo ERR1
        Dim mRs As ADODB.Recordset
        Dim SqlStr As String
        Dim xWEF As String

        ValidateWEFDate = True

        If Val(txtAmendNo.Text) = 0 Then Exit Function

        SqlStr = " SELECT MAX(WEF) AS WEF FROM PRD_BOP_COST_HDR " & vbCrLf _
        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtSupplierCode.Text) & "' " & vbCrLf _
        & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(txtItemCode.Text) & "' " & vbCrLf _
        & " AND AMEND_NO<" & Val(txtAmendNo.Text) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRs, ADODB.LockTypeEnum.adLockReadOnly)
        If mRs.EOF = False Then
            xWEF = IIf(IsDBNull(mRs.Fields("WEF").Value), "", mRs.Fields("WEF").Value)
        End If

        If CDate(txtWEF.Text) < CDate(xWEF) Then
            ValidateWEFDate = False
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function UpdateDetail1() As Boolean

        On Error GoTo UpdateDetail1
        Dim SqlStr As String
        Dim i As Integer
        Dim mRMCode As String
        Dim mRMDesc As String
        Dim mRMRate As Double
        Dim mRMUOM As String
        Dim mRMThick As Double
        Dim mRMLenth As Double
        Dim mRMWidth As Double
        Dim mRMDiaMeter As Double
        Dim mWtPerStrip As Double
        Dim mQtyPerStrip As Double
        Dim mWtPerPc As Double
        Dim mRMCost As Double
        Dim mNetWt As Double
        Dim mScrapWt As Double
        Dim mScrapRate As Double
        Dim mScrapCost As Double
        Dim mNetRMCost As Double
        Dim mMannualCalc As String

        PubDBCn.Execute(" DELETE FROM PRD_BOP_COST_DET " & vbCrLf & " WHERE MKEY='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")

        With SprdMain
            For i = 1 To .MaxRows - 1
                .Row = i

                .Col = ColMannualCalc
                If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                    mMannualCalc = "Y"
                ElseIf .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                    mMannualCalc = "N"
                End If

                .Col = ColRMDesc
                mRMDesc = MainClass.AllowSingleQuote(.Text)

                mRMCode = ""
                If MainClass.ValidateWithMasterTable(mRMDesc, "MTRL_DESC", "MTRL_CODE", "PRD_MTRL_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mRMCode = MasterNo
                End If

                .Col = ColRMRate
                mRMRate = Val(.Text)

                .Col = ColRMUOM
                mRMUOM = MainClass.AllowSingleQuote(.Text)

                .Col = ColRMThick
                mRMThick = Val(.Text)

                .Col = ColRMLenth
                mRMLenth = Val(.Text)

                .Col = ColRMWidth
                mRMWidth = Val(.Text)

                .Col = ColRMDiaMeter
                mRMDiaMeter = Val(.Text)

                .Col = ColWtPerStrip
                mWtPerStrip = Val(.Text)

                .Col = ColQtyPerStrip
                mQtyPerStrip = Val(.Text)

                .Col = ColWtPerPc
                mWtPerPc = Val(.Text)

                .Col = ColRMCost
                mRMCost = Val(.Text)

                .Col = ColNetWt
                mNetWt = Val(.Text)

                .Col = ColScrapWt
                mScrapWt = Val(.Text)

                .Col = ColScrapRate
                mScrapRate = Val(.Text)

                .Col = ColScrapCost
                mScrapCost = Val(.Text)

                .Col = ColNetRMCost
                mNetRMCost = Val(.Text)

                SqlStr = ""
                If Trim(mRMDesc) <> "" Then

                    SqlStr = " INSERT INTO  PRD_BOP_COST_DET (" & vbCrLf & " MKEY, COMPANY_CODE, SUPP_CUST_CODE, ITEM_CODE, " & vbCrLf & " SUBROWNO, MANNUAL_CALC, RM_CODE, ISSUE_UOM, " & vbCrLf & " RATE_PCS, THICKNESS_RM, LENGTH_RM, " & vbCrLf & " WIDTH_RM, DIAMETER_RM, WT_PER_STRIP, " & vbCrLf & " QTY_PER_STRIP, GROSS_WT_PCS, COST_PCS, " & vbCrLf & " NET_WT_PCS, GROSS_WT_SCRAP, RATE_SCRAP, " & vbCrLf & " COST_SCRAP, NET_COST_PCS ) VALUES ( "


                    SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(lblMKey.Text) & "'," & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ",'" & MainClass.AllowSingleQuote(txtSupplierCode.Text) & "', '" & MainClass.AllowSingleQuote(txtItemCode.Text) & "', " & vbCrLf & " " & i & ", '" & mMannualCalc & "', '" & mRMCode & "', '" & mRMUOM & "', " & vbCrLf & " " & mRMRate & ", " & mRMThick & ", " & mRMLenth & ", " & vbCrLf & " " & mRMWidth & ", " & mRMDiaMeter & ", " & mWtPerStrip & ", " & vbCrLf & " " & mQtyPerStrip & ", " & mWtPerPc & ", " & mRMCost & ", " & vbCrLf & " " & mNetWt & ", " & mScrapWt & ", " & mScrapRate & ", " & vbCrLf & " " & mScrapCost & ", " & mNetRMCost & ")"

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        UpdateDetail1 = True
        Exit Function
UpdateDetail1:
        '    Resume					
        MsgBox(Err.Description)
        UpdateDetail1 = False
    End Function
    Private Function UpdatePartDetail1() As Boolean

        On Error GoTo UpdateDetail1
        Dim SqlStr As String
        Dim i As Integer
        Dim mPartDesc As String
        Dim mPartNo As String
        Dim mPartUOM As String
        Dim mPartQty As Double
        Dim mPartRate As Double
        Dim mPartCost As Double


        PubDBCn.Execute(" DELETE FROM PRD_BOP_PART_DET " & vbCrLf & " WHERE MKEY='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")

        With SprdPart
            For i = 1 To .MaxRows - 1
                .Row = i

                .Col = ColPartDesc
                mPartDesc = MainClass.AllowSingleQuote(.Text)

                .Col = ColPartNo
                mPartNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColPartUOM
                mPartUOM = MainClass.AllowSingleQuote(.Text)

                .Col = ColPartQty
                mPartQty = Val(.Text)

                .Col = ColPartRate
                mPartRate = Val(.Text)

                .Col = ColPartCost
                mPartCost = Val(.Text)

                SqlStr = ""
                If Trim(mPartDesc) <> "" Then

                    SqlStr = " INSERT INTO  PRD_BOP_PART_DET (" & vbCrLf & " MKEY, COMPANY_CODE, SUPP_CUST_CODE, ITEM_CODE, " & vbCrLf & " SUBROWNO, PART_DESC, PART_NO, " & vbCrLf & " PART_UOM, PART_QTY, PART_RATE, " & vbCrLf & " PART_COST ) VALUES ( "

                    SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(lblMKey.Text) & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtSupplierCode.Text) & "', '" & MainClass.AllowSingleQuote(txtItemCode.Text) & "', " & vbCrLf & " " & i & ", '" & mPartDesc & "', '" & mPartNo & "', " & vbCrLf & " '" & mPartUOM & "', " & mPartQty & ", " & mPartRate & ", " & vbCrLf & " " & mPartCost & ")"

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        UpdatePartDetail1 = True
        Exit Function
UpdateDetail1:
        MsgBox(Err.Description)
        UpdatePartDetail1 = False
    End Function

    Private Function UpdateProcess1Detail1() As Boolean

        On Error GoTo UpdateDetail1
        Dim SqlStr As String
        Dim i As Integer
        Dim mOPNCode As String
        Dim mOPNDesc As String
        Dim mMCCode As String
        Dim mStorke As String
        Dim mRate As Double
        Dim mCost As Double
        Dim mMachTonnes As Double
        Dim mMCDesc As String

        PubDBCn.Execute(" DELETE FROM PRD_BOP_PROCESS1_DET " & vbCrLf & " WHERE MKEY='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")

        With SprdProcess1
            For i = 1 To .MaxRows - 1
                .Row = i

                .Col = ColProcess1
                mOPNDesc = MainClass.AllowSingleQuote(.Text)

                If MainClass.ValidateWithMasterTable(mOPNDesc, "OPR_DESC", "OPR_CODE", "PRD_OPR_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mOPNCode = MasterNo
                Else
                    MsgInformation("Invalid Process.")
                    mOPNCode = "-1"
                    UpdateProcess1Detail1 = False
                    Exit Function
                End If

                .Col = ColMachCode1
                mMCCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColMachDesc1
                mMCDesc = MainClass.AllowSingleQuote(.Text)

                .Col = ColMachTonnes
                mMachTonnes = Val(.Text)

                .Col = ColStroke1
                mStorke = CStr(Val(.Text))

                .Col = ColRate1
                mRate = Val(.Text)

                mCost = CDbl(VB6.Format(CDbl(mStorke) * mRate, "0.00"))

                SqlStr = ""
                If Trim(mOPNDesc) <> "" Then

                    SqlStr = " INSERT INTO  PRD_BOP_PROCESS1_DET (" & vbCrLf & " MKEY, COMPANY_CODE,SUPP_CUST_CODE, ITEM_CODE, MACHINE_TONN," & vbCrLf _
                        & " SUBROWNO, OPR_CODE, MACHINE_ITEM_CODE, MACHINE_DESC," & vbCrLf _
                        & " STORKE, RATE, COST ) VALUES ( "

                    SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(lblMKey.Text) & "', " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(txtSupplierCode.Text) & "', '" & MainClass.AllowSingleQuote(txtItemCode.Text) & "',  " & mMachTonnes & "," & vbCrLf _
                        & " " & i & ", '" & mOPNCode & "', '" & mMCCode & "','" & mMCDesc & "'," & vbCrLf _
                        & " " & mStorke & ", " & mRate & ", " & mCost & ")"

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        UpdateProcess1Detail1 = True
        Exit Function
UpdateDetail1:
        MsgBox(Err.Description)
        UpdateProcess1Detail1 = False
    End Function

    Private Function UpdateOperationDetail1() As Boolean

        On Error GoTo UpdateDetail1
        Dim SqlStr As String
        Dim i As Integer
        Dim mOPNCode As String
        Dim mOPNDesc As String
        Dim mQty As Double
        Dim mRate As Double
        Dim mCost As Double
        Dim mUOM As String

        PubDBCn.Execute(" DELETE FROM PRD_BOP_OPERATION_DET " & vbCrLf & " WHERE MKEY='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")


        With SprdMainOperation
            For i = 1 To .MaxRows - 1
                .Row = i

                .Col = ColOPRCode
                mOPNCode = MainClass.AllowSingleQuote(.Text)

                '            If MainClass.ValidateWithMasterTable(mOPNDesc, "OPR_DESC", "OPR_CODE", "PRD_OPR_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then					
                '                mOPNCode = MasterNo					
                '            Else					
                '                MsgInformation "Invalid Process."					
                '                mOPNCode = "-1"					
                '                UpdateProcess1Detail1 = False					
                '                Exit Function					
                '            End If					
                '	
                .Col = ColOprUOM
                mUOM = Trim(.Text)

                .Col = ColOprQty
                mQty = Val(.Text)

                .Col = ColOPRRate
                mRate = Val(.Text)

                mCost = CDbl(VB6.Format(mQty * mRate, "0.00"))

                SqlStr = ""
                If Trim(mOPNCode) <> "" Then

                    SqlStr = " INSERT INTO  PRD_BOP_OPERATION_DET (" & vbCrLf _
                        & " MKEY, COMPANY_CODE, SUPP_CUST_CODE, ITEM_CODE, " & vbCrLf _
                        & " SUBROWNO, OPR_CODE, OPR_UOM, OPR_QTY, " & vbCrLf _
                        & " OPR_RATE, OPR_COST ) VALUES ( "

                    SqlStr = SqlStr & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(lblMKey.Text) & "', " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(txtSupplierCode.Text) & "', '" & MainClass.AllowSingleQuote(txtItemCode.Text) & "', " & vbCrLf _
                        & " " & i & ", '" & mOPNCode & "', '" & mUOM & "', " & vbCrLf _
                        & " " & mQty & ", " & mRate & ", " & mCost & ")"

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With


        UpdateOperationDetail1 = True
        Exit Function
UpdateDetail1:
        MsgBox(Err.Description)
        UpdateOperationDetail1 = False
    End Function
    Private Function UpdateProcess2Detail1() As Boolean

        On Error GoTo UpdateDetail1
        Dim SqlStr As String
        Dim i As Integer
        Dim mProcess As String
        Dim mPlantNo As String
        Dim mSurface As String
        Dim mRate As String
        Dim mCost As Double


        PubDBCn.Execute(" DELETE FROM PRD_BOP_PROCESS2_DET " & vbCrLf & " WHERE MKEY='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")

        With SprdProcess2
            For i = 1 To .MaxRows - 1
                .Row = i

                .Col = ColProcess2
                mProcess = MainClass.AllowSingleQuote(.Text)

                .Col = ColPlantNo2
                mPlantNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColSurface2
                mSurface = CStr(Val(.Text))

                .Col = ColRate2
                mRate = CStr(Val(.Text))

                mCost = CDbl(VB6.Format(CDbl(mSurface) * CDbl(mRate), "0.00"))

                SqlStr = ""
                If Trim(mProcess) <> "" Then

                    SqlStr = " INSERT INTO  PRD_BOP_PROCESS2_DET (" & vbCrLf & " MKEY, COMPANY_CODE, SUPP_CUST_CODE, ITEM_CODE, " & vbCrLf & " SUBROWNO, PROCESS_DESC, PLANT_NO, " & vbCrLf & " SURFACE, RATE, COST ) VALUES ( "


                    SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(lblMKey.Text) & "', " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " '" & MainClass.AllowSingleQuote(txtSupplierCode.Text) & "', '" & MainClass.AllowSingleQuote(txtItemCode.Text) & "', " & vbCrLf & " " & i & ", '" & mProcess & "', '" & mPlantNo & "', " & vbCrLf & " " & mSurface & ", " & mRate & ", " & mCost & ")"

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        UpdateProcess2Detail1 = True
        Exit Function
UpdateDetail1:
        MsgBox(Err.Description)
        UpdateProcess2Detail1 = False
    End Function
    Private Sub ShowDetail1(ByRef pMKey As String)

        On Error GoTo ERR1
        Dim i As Integer
        Dim SqlStr As String
        Dim mRMDesc As String
        Dim mRMCode As String

        SqlStr = ""
        SqlStr = " SELECT * FROM PRD_BOP_COST_DET " & vbCrLf & " WHERE Mkey='" & MainClass.AllowSingleQuote(pMKey) & "'" & " ORDER BY SubRowNo"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCostDetail, ADODB.LockTypeEnum.adLockReadOnly)

        With RsCostDetail
            If Not .EOF Then
                If .EOF = True Then Exit Sub
                SprdMain.MaxRows = MainClass.GetMaxRecord("PRD_BOP_COST_DET", PubDBCn, " Mkey='" & MainClass.AllowSingleQuote(pMKey) & "'") + 1
                FormatSprdPart(-1)
                i = 0
                .MoveFirst()

                Do While Not .EOF
                    i = i + 1
                    SprdMain.Row = i

                    SprdMain.Col = ColMannualCalc
                    If .Fields("MANNUAL_CALC").Value = "Y" Then
                        SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked)
                    ElseIf .Fields("MANNUAL_CALC").Value = "N" Then
                        SprdMain.Value = CStr(System.Windows.Forms.CheckState.Unchecked)
                    End If

                    mRMCode = IIf(IsDBNull(.Fields("RM_CODE").Value), "", .Fields("RM_CODE").Value)
                    mRMDesc = ""
                    If MainClass.ValidateWithMasterTable(mRMCode, "MTRL_CODE", "MTRL_DESC", "PRD_MTRL_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mRMDesc = MasterNo
                    End If

                    SprdMain.Col = ColRMDesc
                    SprdMain.Text = Trim(mRMDesc)

                    SprdMain.Col = ColRMUOM
                    SprdMain.Text = IIf(IsDBNull(.Fields("ISSUE_UOM").Value), "", .Fields("ISSUE_UOM").Value)

                    SprdMain.Col = ColRMRate
                    SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("RATE_PCS").Value), 0, .Fields("RATE_PCS").Value), "0.00")

                    SprdMain.Col = ColRMThick
                    SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("THICKNESS_RM").Value), 0, .Fields("THICKNESS_RM").Value), "0.000")

                    SprdMain.Col = ColRMLenth
                    SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("LENGTH_RM").Value), 0, .Fields("LENGTH_RM").Value), "0.000")

                    SprdMain.Col = ColRMWidth
                    SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("WIDTH_RM").Value), 0, .Fields("WIDTH_RM").Value), "0.000")

                    SprdMain.Col = ColRMDiaMeter
                    SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("DIAMETER_RM").Value), 0, .Fields("DIAMETER_RM").Value), "0.000")

                    SprdMain.Col = ColWtPerStrip
                    SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("WT_PER_STRIP").Value), 0, .Fields("WT_PER_STRIP").Value), "0.000")

                    SprdMain.Col = ColQtyPerStrip
                    SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("QTY_PER_STRIP").Value), 0, .Fields("QTY_PER_STRIP").Value), "0.000")

                    SprdMain.Col = ColWtPerPc
                    SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("GROSS_WT_PCS").Value), 0, .Fields("GROSS_WT_PCS").Value), "0.000")

                    SprdMain.Col = ColRMCost
                    SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("COST_PCS").Value), 0, .Fields("COST_PCS").Value), "0.00")

                    SprdMain.Col = ColNetWt
                    SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("NET_WT_PCS").Value), 0, .Fields("NET_WT_PCS").Value), "0.000")

                    SprdMain.Col = ColScrapWt
                    SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("GROSS_WT_SCRAP").Value), 0, .Fields("GROSS_WT_SCRAP").Value), "0.000")

                    SprdMain.Col = ColScrapRate
                    SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("RATE_SCRAP").Value), 0, .Fields("RATE_SCRAP").Value), "0.00")

                    SprdMain.Col = ColScrapCost
                    SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("COST_SCRAP").Value), 0, .Fields("COST_SCRAP").Value), "0.00")

                    SprdMain.Col = ColNetRMCost
                    SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("NET_COST_PCS").Value), 0, .Fields("NET_COST_PCS").Value), "0.00")

                    .MoveNext()
                Loop
            End If
        End With
        Exit Sub
ERR1:
        '    Resume					
        MsgBox(Err.Description)
    End Sub
    Private Sub ShowPartDetail1(ByRef pMKey As String)

        On Error GoTo ERR1
        Dim i As Integer
        Dim SqlStr As String

        SqlStr = ""
        SqlStr = " SELECT * FROM PRD_BOP_PART_DET " & vbCrLf & " WHERE Mkey='" & MainClass.AllowSingleQuote(pMKey) & "'" & " ORDER BY SubRowNo"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPartDetail, ADODB.LockTypeEnum.adLockReadOnly)

        With RsPartDetail
            If Not .EOF Then
                If .EOF = True Then Exit Sub
                SprdPart.MaxRows = MainClass.GetMaxRecord("PRD_BOP_PART_DET", PubDBCn, " Mkey='" & MainClass.AllowSingleQuote(pMKey) & "'") + 1
                FormatSprdPart(-1)
                i = 0
                .MoveFirst()
                Do While Not .EOF
                    i = i + 1
                    SprdPart.Row = i

                    SprdPart.Col = ColPartDesc
                    SprdPart.Text = IIf(IsDBNull(.Fields("PART_DESC").Value), "", .Fields("PART_DESC").Value)

                    SprdPart.Col = ColPartNo
                    SprdPart.Text = IIf(IsDBNull(.Fields("PART_NO").Value), "", .Fields("PART_NO").Value)

                    SprdPart.Col = ColPartUOM
                    SprdPart.Text = IIf(IsDBNull(.Fields("PART_UOM").Value), "", .Fields("PART_UOM").Value)

                    SprdPart.Col = ColPartQty
                    SprdPart.Text = VB6.Format(IIf(IsDBNull(.Fields("PART_QTY").Value), 0, .Fields("PART_QTY").Value), "0.00")

                    SprdPart.Col = ColPartRate
                    SprdPart.Text = VB6.Format(IIf(IsDBNull(.Fields("PART_RATE").Value), 0, .Fields("PART_RATE").Value), "0.00")

                    SprdPart.Col = ColPartCost
                    SprdPart.Text = VB6.Format(IIf(IsDBNull(.Fields("PART_COST").Value), 0, .Fields("PART_COST").Value), "0.00")

                    .MoveNext()
                Loop
            End If
        End With
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub

    Private Sub ShowProcess1Detail1(ByRef pMKey As String)

        On Error GoTo ERR1
        Dim i As Integer
        Dim SqlStr As String
        Dim mOPNCode As String
        Dim mOPNDesc As String
        Dim mMCCode As String
        Dim mMCDesc As String


        SqlStr = ""
        SqlStr = " SELECT * FROM PRD_BOP_PROCESS1_DET " & vbCrLf & " WHERE Mkey='" & MainClass.AllowSingleQuote(pMKey) & "'" & " ORDER BY SubRowNo"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProcess1Detail, ADODB.LockTypeEnum.adLockReadOnly)

        With RsProcess1Detail
            If Not .EOF Then
                If .EOF = True Then Exit Sub
                SprdProcess1.MaxRows = MainClass.GetMaxRecord("PRD_BOP_PROCESS1_DET", PubDBCn, " Mkey='" & MainClass.AllowSingleQuote(pMKey) & "'") + 1
                FormatSprdProcess1(-1)
                i = 0
                .MoveFirst()
                Do While Not .EOF
                    i = i + 1
                    SprdProcess1.Row = i

                    mOPNCode = IIf(IsDBNull(.Fields("OPR_CODE").Value), "", .Fields("OPR_CODE").Value)
                    If MainClass.ValidateWithMasterTable(mOPNCode, "OPR_CODE", "OPR_DESC", "PRD_OPR_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mOPNDesc = MasterNo
                    End If

                    SprdProcess1.Col = ColProcess1
                    SprdProcess1.Text = Trim(mOPNDesc)

                    mMCCode = IIf(IsDBNull(.Fields("MACHINE_ITEM_CODE").Value), "", .Fields("MACHINE_ITEM_CODE").Value)
                    If MainClass.ValidateWithMasterTable(mMCCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mMCDesc = MasterNo
                    Else
                        mMCDesc = IIf(IsDBNull(.Fields("MACHINE_DESC").Value), "", .Fields("MACHINE_DESC").Value)
                    End If

                    SprdProcess1.Col = ColMachCode1
                    SprdProcess1.Text = mMCCode

                    SprdProcess1.Col = ColMachDesc1
                    SprdProcess1.Text = mMCDesc

                    SprdProcess1.Col = ColMachTonnes
                    SprdProcess1.Text = VB6.Format(IIf(IsDBNull(.Fields("MACHINE_TONN").Value), 0, .Fields("MACHINE_TONN").Value), "0.00")


                    SprdProcess1.Col = ColStroke1
                    SprdProcess1.Text = VB6.Format(IIf(IsDBNull(.Fields("STORKE").Value), 0, .Fields("STORKE").Value), "0.00")

                    SprdProcess1.Col = ColRate1
                    SprdProcess1.Text = VB6.Format(IIf(IsDBNull(.Fields("Rate").Value), 0, .Fields("Rate").Value), "0.00")

                    SprdProcess1.Col = ColCost1
                    SprdProcess1.Text = VB6.Format(IIf(IsDBNull(.Fields("COST").Value), 0, .Fields("COST").Value), "0.00")

                    .MoveNext()
                Loop
            End If
        End With
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub

    Private Sub ShowOprDetail1(ByRef pMKey As String)

        On Error GoTo ERR1
        Dim i As Integer
        Dim SqlStr As String
        Dim mOPNCode As String
        Dim mOPNDesc As String
        Dim mOPNUOM As String
        Dim mQty As Double
        Dim mRate As Double

        SqlStr = ""
        SqlStr = " SELECT * FROM PRD_BOP_OPERATION_DET " & vbCrLf & " WHERE Mkey='" & MainClass.AllowSingleQuote(pMKey) & "'" & " ORDER BY SubRowNo"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOprnDetail, ADODB.LockTypeEnum.adLockReadOnly)

        With RsOprnDetail
            If Not .EOF Then
                If .EOF = True Then Exit Sub
                SprdMainOperation.MaxRows = MainClass.GetMaxRecord("PRD_BOP_OPERATION_DET", PubDBCn, " Mkey='" & MainClass.AllowSingleQuote(pMKey) & "'") + 1
                FormatSprdMainOperation(-1)
                i = 0
                .MoveFirst()
                Do While Not .EOF
                    i = i + 1
                    SprdMainOperation.Row = i

                    mOPNCode = IIf(IsDBNull(.Fields("OPR_CODE").Value), "", .Fields("OPR_CODE").Value)
                    If MainClass.ValidateWithMasterTable(mOPNCode, "OPR_CODE", "OPR_DESC", "PRD_OPR_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mOPNDesc = MasterNo
                    End If
                    SprdMainOperation.Col = ColOPRCode
                    SprdMainOperation.Text = Trim(mOPNCode)

                    SprdMainOperation.Col = ColOprDesc
                    SprdMainOperation.Text = Trim(mOPNDesc)

                    mOPNUOM = IIf(IsDBNull(.Fields("OPR_UOM").Value), "", .Fields("OPR_UOM").Value)

                    SprdMainOperation.Col = ColOprUOM
                    SprdMainOperation.Text = Trim(mOPNUOM)

                    SprdMainOperation.Col = ColOprQty
                    SprdMainOperation.Text = VB6.Format(IIf(IsDBNull(.Fields("OPR_QTY").Value), 0, .Fields("OPR_QTY").Value), "0.00")

                    SprdMainOperation.Col = ColOPRRate
                    SprdMainOperation.Text = VB6.Format(IIf(IsDBNull(.Fields("OPR_RATE").Value), 0, .Fields("OPR_RATE").Value), "0.00")

                    SprdMainOperation.Col = ColOprCost
                    SprdMainOperation.Text = VB6.Format(IIf(IsDBNull(.Fields("OPR_COST").Value), 0, .Fields("OPR_COST").Value), "0.00")

                    .MoveNext()
                Loop
            End If
        End With
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Private Sub ShowProcess2Detail1(ByRef pMKey As String)

        On Error GoTo ERR1
        Dim i As Integer
        Dim SqlStr As String

        SqlStr = ""
        SqlStr = " Select * FROM PRD_BOP_PROCESS2_DET " & vbCrLf & " WHERE Mkey='" & MainClass.AllowSingleQuote(pMKey) & "'" & " ORDER BY SubRowNo"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProcess2Detail, ADODB.LockTypeEnum.adLockReadOnly)

        With RsProcess2Detail
            If Not .EOF Then
                If .EOF = True Then Exit Sub
                SprdProcess2.MaxRows = MainClass.GetMaxRecord("PRD_BOP_PROCESS2_DET", PubDBCn, " Mkey='" & MainClass.AllowSingleQuote(pMKey) & "'") + 1
                FormatSprdProcess2(-1)
                i = 0
                .MoveFirst()
                Do While Not .EOF
                    i = i + 1
                    SprdProcess2.Row = i

                    SprdProcess2.Col = ColProcess2
                    SprdProcess2.Text = IIf(IsDBNull(.Fields("PROCESS_DESC").Value), "", .Fields("PROCESS_DESC").Value)

                    SprdProcess2.Col = ColPlantNo2
                    SprdProcess2.Text = IIf(IsDBNull(.Fields("PLANT_NO").Value), "", .Fields("PLANT_NO").Value)

                    SprdProcess2.Col = ColSurface2
                    SprdProcess2.Text = VB6.Format(IIf(IsDBNull(.Fields("SURFACE").Value), 0, .Fields("SURFACE").Value), "0.00")

                    SprdProcess2.Col = ColRate2
                    SprdProcess2.Text = VB6.Format(IIf(IsDBNull(.Fields("Rate").Value), 0, .Fields("Rate").Value), "0.00")

                    SprdProcess2.Col = ColCost2
                    SprdProcess2.Text = VB6.Format(IIf(IsDBNull(.Fields("COST").Value), 0, .Fields("COST").Value), "0.00")

                    .MoveNext()
                Loop
            End If
        End With
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub

    Private Sub FrmRMCosting_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 210, mReFormWidth - 210, mReFormWidth))
        'UltraGrid2.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 210, mReFormWidth - 210, mReFormWidth))
        SSTab1.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        fraBase.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11394.9, 750)
        SprdView.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))

        CurrFormWidth = mReFormWidth

        'MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtDigit_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDigit.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDigit_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDigit.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub optNone_CheckedChanged(sender As Object, e As EventArgs) Handles optNone.CheckedChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        If FormActive = True Then
            AutoCalc()
        End If

    End Sub

    Private Sub optRoundingDown_CheckedChanged(sender As Object, e As EventArgs) Handles optRoundingDown.CheckedChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        If FormActive = True Then
            AutoCalc()
        End If

    End Sub

    Private Sub optRoundingUP_CheckedChanged(sender As Object, e As EventArgs) Handles optRoundingUP.CheckedChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        If FormActive = True Then
            AutoCalc()
        End If

    End Sub

    Private Sub txtDigit_Validating(sender As Object, e As CancelEventArgs) Handles txtDigit.Validating
        If FormActive = True Then
            AutoCalc()
        End If
    End Sub
End Class
