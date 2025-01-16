Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmFGCostingCustomerWise
    Inherits System.Windows.Forms.Form
    Dim RsFGCostMain As ADODB.Recordset
    Dim RsFGCostRMDet As ADODB.Recordset
    Dim RsFGCostBOPDet As ADODB.Recordset
    Dim RsFGCostPNTDet As ADODB.Recordset
    Dim RsFGCostOprDet As ADODB.Recordset
    Dim RsFGCostWeldDet As ADODB.Recordset
    Dim RsFGCostPackDet As ADODB.Recordset

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean
    Dim mIsShowing As Boolean

    Private Const ConRowHeight As Short = 12

    Dim mcntRow As Integer


    'Private Const ColRMNewItem = 1			
    'Private Const ColRMItemCode = 2			
    'Private Const ColRMItemDesc = 3			
    'Private Const ColRMGrossWt = 4			
    'Private Const ColRMItemRate = 5			
    'Private Const ColRMItemAmount = 6			
    'Private Const ColRMScrapWt = 7			
    'Private Const ColRMScrapRate = 8			
    'Private Const ColRMScrapAmount = 9			
    'Private Const ColRMNetWt = 10			
    'Private Const ColRMNetAmount = 11			
    'Private Const ColRMFreight = 12			
    'Private Const ColRMTotAmount = 13			
    'Private Const ColRMRemarks = 14			

    Private Const ColMannualCalc As Short = 1
    Private Const ColProductDesc As Short = 2
    Private Const ColRMDesc As Short = 3
    Private Const ColRMRate As Short = 4
    Private Const ColRMUOM As Short = 5
    Private Const ColRMThick As Short = 6
    Private Const ColRMLenth As Short = 7
    Private Const ColRMWidth As Short = 8
    Private Const ColRMDiaMeter As Short = 9
    Private Const ColWtPerStrip As Short = 10
    Private Const ColQtyPerStrip As Short = 11
    Private Const ColWtPerPc As Short = 12
    Private Const ColRMCost As Short = 13
    Private Const ColNetWt As Short = 14
    Private Const ColScrapWt As Short = 15
    Private Const ColScrapRate As Short = 16
    Private Const ColScrapCost As Short = 17
    Private Const ColNetRMCost As Short = 18


    Private Const ColBOPNewItem As Short = 1
    Private Const ColBOPItemCode As Short = 2
    Private Const ColBOPItemDesc As Short = 3
    Private Const ColBOPItemUOM As Short = 4
    Private Const ColBOPItemQty As Short = 5
    Private Const ColBOPItemRate As Short = 6
    Private Const ColBOPItemAmount As Short = 7
    Private Const ColBOPFreight As Short = 8
    Private Const ColBOPTotAmount As Short = 9
    Private Const ColBOPSubCosting As Short = 10
    Private Const ColBOPRemarks As Short = 11

    Private Const ColWeldDesc As Short = 1
    Private Const ColWeldType As Short = 2
    Private Const ColWeldUOM As Short = 3
    Private Const ColWeldQty As Short = 4
    Private Const ColWeldRate As Short = 5
    Private Const ColWeldAmount As Short = 6
    Private Const ColWeldRemarks As Short = 7



    Private Const ColOprOprCode As Short = 1
    Private Const ColOprOprDesc As Short = 2
    Private Const ColOprOprRate As Short = 3
    Private Const ColOprType As Short = 4
    Private Const ColOprRemarks As Short = 5

    Private Const ColPackPackDetail As Short = 1
    Private Const ColPackPackRate As Short = 2
    Private Const ColPackType As Short = 3
    Private Const ColPackRemarks As Short = 4

    Private Const ColPNTItemCode As Short = 1
    Private Const ColPNTItemDesc As Short = 2
    Private Const ColPNTItemUOM As Short = 3
    Private Const ColPNTItemQty As Short = 4
    Private Const ColPNTItemRate As Short = 5
    Private Const ColPNTItemAmount As Short = 6
    Private Const ColPNTRemarks As Short = 7

    Dim mAmendStatus As Boolean

    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo AssignGridErr
        Dim SqlStr As String
        SqlStr = ""

        SqlStr = "SELECT IH.PRODUCT_CODE, INVMST.ITEM_SHORT_DESC, TO_CHAR(WEF,'DD/MM/YYYY') AS WEF, " & vbCrLf & " TOT_SALE_PRICE, INVMST.CUSTOMER_PART_NO, IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME " & vbCrLf & " FROM PRD_CUST_FG_COST_HDR IH, FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND IH.PRODUCT_CODE=INVMST.ITEM_CODE " & vbCrLf & " ORDER BY IH.PRODUCT_CODE, CMST.SUPP_CUST_NAME "

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()

        Exit Sub
AssignGridErr:
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub

    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 24)
            .set_ColWidth(0, 5)
            .set_ColWidth(1, 10)
            .set_ColWidth(2, 25)
            .set_ColWidth(3, 8)
            .set_ColWidth(4, 10)
            .set_ColWidth(5, 12)
            .set_ColWidth(6, 12)
            .set_ColWidth(7, 24)
            .set_ColWidth(8, 12)
            .set_ColWidth(9, 12)

            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle			
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
        If MODIFYMode = True And RsFGCostMain.EOF = True Then Exit Function

        If MODIFYMode = True And chkStatus.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Closed Costing Cann't be Modified")
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtProductCode.Text) = "" Then
            MsgBox("Product Code is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtProductCode.Enabled = True Then txtProductCode.Focus()
            Exit Function
        End If

        If Trim(txtWEF.Text) = "" Then
            MsgBox("W.E.F is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtWEF.Enabled = True Then txtWEF.Focus()
            Exit Function
        End If

        If Trim(txtSuppCustCode.Text) = "" Then
            MsgBox("Customer is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtSuppCustCode.Enabled = True Then txtSuppCustCode.Focus()
            Exit Function
        End If

        If Trim(txtPrepBy.Text) = "" Then
            MsgBox("Prepared By is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtPrepBy.Focus()
            Exit Function
        End If

        If ADDMode = True And Val(txtAmendNo.Text) > 0 Then
            If CheckWEFDate(Trim(txtWEF.Text)) = False Then
                MsgBox("WEF. Date Cann't be Less or Equal Than Current WEF Date.", MsgBoxStyle.Information)
                FieldsVarification = False
                txtWEF.Focus()
                Exit Function
            End If
        End If

        '    If MainClass.ValidDataInGrid(SprdRM, ColRMNewItem, "S", "Dept Code Is Blank") = False Then FieldsVarification = False: Exit Function			
        '    If MainClass.ValidDataInGrid(SprdRM, ColRMItemCode, "S", "Item Code Is Blank") = False Then FieldsVarification = False: Exit Function			
        '    If MainClass.ValidDataInGrid(SprdRM, ColRMItemDesc, "S", "Item Name Is Blank") = False Then FieldsVarification = False: Exit Function			
        '    If MainClass.ValidDataInGrid(SprdRM, ColRMGrossWt, "N", "Please Check Std.Qty") = False Then FieldsVarification = False: Exit Function			
        '    If MainClass.ValidDataInGrid(SprdRM, ColQUnit, "S", "Please Check Unit") = False Then FieldsVarification = False: Exit Function			
        '			
        ''Weld Operation			
        '    If MainClass.ValidDataInGrid(SprdOpr, ColOprOprCode, "S", "Please Check Unit") = False Then FieldsVarification = False: Exit Function			
        '    If MainClass.ValidDataInGrid(SprdOpr, ColOprDeptCode, "S", "Please Check Dept") = False Then FieldsVarification = False: Exit Function			
        '    If MainClass.ValidDataInGrid(SprdOpr, ColWeldMcCode, "S", "Please Check Machine Code") = False Then FieldsVarification = False: Exit Function			
        '    If MainClass.ValidDataInGrid(SprdOpr, ColOprOprRate, "N", "Please Check Weld Rate") = False Then FieldsVarification = False: Exit Function			
        '    If MainClass.ValidDataInGrid(SprdOpr, ColWeldStroke, "N", "Please Check Weld Stroke") = False Then FieldsVarification = False: Exit Function			
        '			
        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function

    Private Function CheckWEFDate(ByRef pWEFDate As String) As Boolean

        On Error GoTo ErrorPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mCheckWEFDate As String

        CheckWEFDate = True

        SqlStr = " SELECT MAX(WEF) AS WEF " & vbCrLf _
        & " FROM PRD_CUST_FG_COST_HDR " & vbCrLf _
        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
        & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtSuppCustCode.Text) & "' " & vbCrLf _
        & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "' " & vbCrLf _
        & " AND AMEND_NO < " & Val(txtAmendNo.Text) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mCheckWEFDate = IIf(IsDBNull(RsTemp.Fields("WEF").Value), "", RsTemp.Fields("WEF").Value)
            If mCheckWEFDate <> "" Then
                If CDate(mCheckWEFDate) >= CDate(pWEFDate) Then
                    CheckWEFDate = False
                End If
            End If
        End If

        Exit Function
ErrorPart:
        CheckWEFDate = False
    End Function

    Private Sub chkStatus_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkStatus.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtProductCode.Enabled = True
            cmdSearchProdCode.Enabled = True
            cmdSearchWEF.Enabled = True
            SprdRM.Enabled = True
            SprdBOP.Enabled = True
            SprdOpr.Enabled = True
            SprdPlt.Enabled = True
            SprdPnt.Enabled = True
            SprdPdr.Enabled = True
            SprdPack.Enabled = True
        Else
            CmdAdd.Text = ConCmdAddCaption
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdAmend_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAmend.Click

        Dim mItemCode As String
        Dim i As Integer

        mItemCode = Trim(txtProductCode.Text)

        If mItemCode = "" Then
            MsgInformation("Please Select Item")
            Exit Sub
        End If

        Call txtProductCode_Validating(txtProductCode, New System.ComponentModel.CancelEventArgs(True))

        txtAmendNo.Text = CStr(GetMaxAmendNo(mItemCode))
        chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked

        mAmendStatus = True
        cmdAmend.Enabled = False
        txtWEF.Enabled = True
        SprdRM.Enabled = True
        SprdBOP.Enabled = True
        SprdOpr.Enabled = True
        SprdPlt.Enabled = True
        SprdPnt.Enabled = True
        SprdPdr.Enabled = True
        SprdPack.Enabled = True

        txtPrepBy.Text = ""
        lblPrepBy.Text = ""
        txtAppBy.Text = ""
        lblAppBy.Text = ""

        txtPrepBy.Enabled = True
        cmdSearchPrepBy.Enabled = True

        ADDMode = True
        MODIFYMode = False

        MainClass.ButtonStatus(Me, XRIGHT, RsFGCostMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Function GetMaxAmendNo(ByRef pItemCode As String) As Integer

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        SqlStr = " SELECT MAX(AMEND_NO) AS AMEND_NO" & vbCrLf _
        & " FROM PRD_CUST_FG_COST_HDR" & vbCrLf _
        & " WHERE " & vbCrLf _
        & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtSuppCustCode.Text) & "'" & vbCrLf _
        & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

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

        If chkStatus.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Closed Costing Cann't be Deleted")
            Exit Sub
        End If

        If Trim(txtProductCode.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If Not RsFGCostMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then
                PubDBCn.Cancel()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "PRD_CUST_FG_COST_HDR", (txtProductCode.Text), RsFGCostMain) = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "PRD_CUST_FG_COST_HDR", "MKEY", (lblMKey.Text)) = False Then GoTo DelErrPart

                PubDBCn.Execute("DELETE FROM PRD_CUST_FG_COST_PACK_DET WHERE UPPER(LTRIM(RTRIM(Mkey)))='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")
                PubDBCn.Execute("DELETE FROM PRD_CUST_FG_COST_OPR_DET WHERE UPPER(LTRIM(RTRIM(Mkey)))='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")
                PubDBCn.Execute("DELETE FROM PRD_CUST_FG_COST_PNT_DET WHERE UPPER(LTRIM(RTRIM(Mkey)))='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")
                PubDBCn.Execute("DELETE FROM PRD_CUST_FG_COST_CONVER_DET WHERE UPPER(LTRIM(RTRIM(Mkey)))='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")
                PubDBCn.Execute("DELETE FROM PRD_CUST_FG_COST_BOP_DET WHERE UPPER(LTRIM(RTRIM(Mkey)))='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")
                PubDBCn.Execute("DELETE FROM PRD_CUST_FG_COST_RM_DET WHERE UPPER(LTRIM(RTRIM(Mkey)))='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")
                PubDBCn.Execute("DELETE FROM PRD_CUST_FG_COST_HDR WHERE UPPER(LTRIM(RTRIM(Mkey)))='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")



                If Val(txtAmendNo.Text) > 0 Then
                    If UpdatePreviousCost((txtProductCode.Text), Val(txtAmendNo.Text), "O") = False Then GoTo DelErrPart
                End If

                PubDBCn.CommitTrans()
                RsFGCostMain.Requery()
                RsFGCostRMDet.Requery()
                RsFGCostBOPDet.Requery()
                RsFGCostOprDet.Requery()
                RsFGCostWeldDet.Requery()
                RsFGCostPackDet.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        RsFGCostMain.Requery()
        RsFGCostRMDet.Requery()
        RsFGCostBOPDet.Requery()
        RsFGCostOprDet.Requery()
        RsFGCostWeldDet.Requery()
        RsFGCostPackDet.Requery()
        PubDBCn.Cancel()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsFGCostMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            txtProductCode.Enabled = False
            cmdSearchProdCode.Enabled = False
            SprdRM.Enabled = True
            SprdBOP.Enabled = True
            SprdOpr.Enabled = True
            SprdPlt.Enabled = True
            SprdPnt.Enabled = True
            SprdPdr.Enabled = True
            SprdPack.Enabled = True
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

    Private Sub cmdPopulate_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPopulate.Click

        On Error GoTo ERR1
        Dim RsShow As ADODB.Recordset
        Dim SqlStr As String
        Dim mRMCode As String
        Dim i As Integer
        Dim mSrn As String
        Dim RsTemp As ADODB.Recordset
        Dim pSqlStr As String
        Dim mLevel As Integer


        If Trim(txtWEF.Text) = "" Then
            MsgInformation("Please Enter WEF Date.")
            txtWEF.Focus()
            Exit Sub
        End If

        MainClass.ClearGrid(SprdBOP)
        MainClass.ClearGrid(SprdOpr)

        FormatSprdBOP(-1)
        FormatSprdWeld(-1)
        FormatSprdOPR(-1)

        SqlStr = ""

        '''" & VB6.Format((PubCurrDate), "DD-MMM-YYYY") & "'			

        SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.RM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " ID.DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " ID.STD_QTY, ID.GROSS_WT_SCRAP, " & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM, INVMST.UOM_FACTOR"

        SqlStr = SqlStr & vbCrLf & " FROM PRD_NEWBOM_HDR IH,PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.RM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        SqlStr = SqlStr & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "' " & vbCrLf & " AND IH.WEF= (" & vbCrLf & " SELECT MAX(WEF) FROM PRD_NEWBOM_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "'" & vbCrLf & " AND WEF <= TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        SqlStr = SqlStr & vbCrLf & " ORDER BY IH.PRODUCT_CODE, ID.SUBROWNO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsShow.EOF Then
            '        pSqlStr = "SELECT ITEM_SHORT_DESC, CUSTOMER_PART_NO, ITEM_MODEL, ISSUE_UOM,ITEM_WLENGTH" & vbCrLf _			
            ''                & " FROM INV_ITEM_MST " & vbCrLf _			
            ''                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _			
            ''                & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(RsShow!PRODUCT_CODE) & "'"			
            '        MainClass.UOpenRecordSet pSqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly			
            '			
            '        If RsTemp.EOF = False Then			
            '            txtProductDesc.Text = IIf(IsNull(RsTemp!ITEM_SHORT_DESC), "", RsTemp!ITEM_SHORT_DESC)			
            '            txtCustPartNo.Text = IIf(IsNull(RsTemp!CUSTOMER_PART_NO), "", RsTemp!CUSTOMER_PART_NO)			
            '            txtUnit.Text = IIf(IsNull(RsTemp!ISSUE_UOM), "", RsTemp!ISSUE_UOM)			
            '            txtWeldLength.Text = IIf(IsNull(RsTemp!ITEM_WLENGTH), "0.000", RsTemp!ITEM_WLENGTH)			
            '        End If			

            Do While Not RsShow.EOF
                mcntRow = mcntRow + 1
                i = i + 1
                SprdBOP.Row = mcntRow

                mSrn = Str(i)

                mRMCode = Trim(IIf(IsDBNull(RsShow.Fields("RM_CODE").Value), "", RsShow.Fields("RM_CODE").Value))
                mLevel = 1
                Call FillGridCol(RsShow, mSrn, mLevel, Trim(txtProductCode.Text), Trim(txtProductCode.Text))

                '            SprdMain.MaxRows = SprdMain.MaxRows + 1			
                RsShow.MoveNext()
            Loop

        End If
        '    If ShowOperationRate(Trim(txtProductCode.Text)) = False Then GoTo ERR1			


        Call AutoCalc()
        FormatSprdBOP(-1)
        FormatSprdWeld(-1)
        FormatSprdOPR(-1)

        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub

    Private Sub FillGridCol(ByRef pRs As ADODB.Recordset, ByRef pSRNo As String, ByRef pLevel As Integer, ByRef pProductCode As String, ByRef pParentCode As String)
        On Error GoTo FillGERR
        Dim mDrgRevNo As String
        Dim mRMCode As String
        Dim mDeptCode As String

        Dim mItemRate As Double
        Dim xPurchaseCost As Double
        Dim xLandedCost As Double
        Dim pItemUOM As String
        Dim mFactor As Double
        Dim mStdQty As Double

        With SprdBOP
            mRMCode = IIf(IsDBNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value)
            mDeptCode = IIf(IsDBNull(pRs.Fields("DEPT_CODE").Value), "", pRs.Fields("DEPT_CODE").Value)

            '        If ShowOperationRate(mRMCode) = False Then GoTo FillGERR			
            '        If FillSurfaceData(mDeptCode) = False Then GoTo FillGERR			

            If mDeptCode = "J/W" Then
                '            If FillJWGrid(mRMCode) = False Then GoTo FillGERR			
            End If

            If CheckSubRecord(mRMCode) = True Then
                Call FillSubRecord(mRMCode, (txtWEF.Text), pSRNo, pLevel, pProductCode)
            Else
                .Row = .MaxRows

                '            .Col = ColBOPNewItem			
                '            .Value = vbUnchecked			
                '			
                .Col = ColBOPItemCode
                .Text = IIf(IsDBNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value)
                mRMCode = IIf(IsDBNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value)

                .Col = ColBOPItemDesc
                .Text = IIf(IsDBNull(pRs.Fields("ITEM_SHORT_DESC").Value), "", pRs.Fields("ITEM_SHORT_DESC").Value)

                .Col = ColBOPItemUOM
                pItemUOM = IIf(IsDBNull(pRs.Fields("ISSUE_UOM").Value), "", pRs.Fields("ISSUE_UOM").Value)
                .Text = IIf(IsDBNull(pRs.Fields("ISSUE_UOM").Value), "", pRs.Fields("ISSUE_UOM").Value)

                mStdQty = 1
                If mDeptCode <> "J/W" Then
                    If pItemUOM = "KGS" Then
                        mStdQty = 0.001
                    ElseIf pItemUOM = "TON" Or pItemUOM = "MT" Then
                        mStdQty = 0.001 * 0.001
                    Else

                    End If
                End If
                .Col = ColBOPItemQty
                .Text = CStr(Val(IIf(IsDBNull(pRs.Fields("STD_QTY").Value), 0, pRs.Fields("STD_QTY").Value)) * mStdQty)



                mFactor = IIf(IsDBNull(pRs.Fields("UOM_FACTOR").Value), "", pRs.Fields("UOM_FACTOR").Value)

                .Col = ColBOPItemRate
                If GetLatestItemCostFromPO(mRMCode, xPurchaseCost, xLandedCost, VB6.Format(PubCurrDate, "DD/MM/YYYY"), "ST", "", pItemUOM, mFactor) = False Then GoTo FillGERR
                mItemRate = xLandedCost
                SprdBOP.Text = CStr(mItemRate)

                '            .Col = ColBOPItemAmount			
                '            .Col = ColBOPFreight			
                '            .Col = ColBOPTotAmount			
                '            .Col = ColBOPSubCosting			
                '            .Col = ColBOPRemarks			


                '            .Col = ColDeptCode			
                '            .Text = mDeptCode			



                SprdBOP.MaxRows = SprdBOP.MaxRows + 1

            End If
        End With

        ''''    If pLevel > 1 Then			
        ''''        pRs.MoveNext			
        ''''        If pRs.EOF = False Then			
        ''''            mRMCode = IIf(IsNull(pRs!RM_CODE), "", pRs!RM_CODE)			
        ''''        Else			
        ''''            mRMCode = "-1"			
        ''''        End If			
        ''''    End If			

        '    Call FillSubAlterRecord(mRMCode, "", pSRNo, pLevel, pProductCode, mDeptCode, pParentCode)			
        '    Call FillSubRecord(mRMCode, txtWEF.Text, pSRNo, pLevel, pProductCode)			

        Exit Sub
FillGERR:
        '    Resume			
        MsgBox(Err.Description)
    End Sub

    Private Function ShowOperationRate(ByRef xProductCode As String) As Boolean

        On Error GoTo ERR1
        Dim mRs As ADODB.Recordset
        Dim SqlStr As String
        Dim mWef As String
        Dim RsOprRate As ADODB.Recordset
        Dim i As Integer
        Dim RsShow As ADODB.Recordset

        ShowOperationRate = True

        If Trim(xProductCode) = "" Then Exit Function



        SqlStr = " SELECT ID.* FROM PRD_OPR_RATE_HDR IH, PRD_OPR_RATE_DET ID " & vbCrLf _
        & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
        & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        & " AND IH.ITEM_CODE='" & MainClass.AllowSingleQuote(xProductCode) & "' "

        '    If Trim(mWef) <> "" Then			
        '        SqlStr = SqlStr & vbCrLf & " AND IH.WEF='" & VB6.Format((txtWEF.Text), "DD-MMM-YYYY") & "'"			
        '    Else			
        SqlStr = SqlStr & vbCrLf & " AND IH.WEF = (" & vbCrLf & " SELECT MAX(WEF) AS WEF " & vbCrLf & " FROM PRD_OPR_RATE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ITEM_CODE='" & Trim(xProductCode) & "'"

        SqlStr = SqlStr & vbCrLf & " AND WEF<=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        '    End If			

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOprRate, ADODB.LockTypeEnum.adLockReadOnly)
        '    I = 1			
        If RsOprRate.EOF = False Then
            With SprdOpr
                Do While Not RsOprRate.EOF
                    .Row = .MaxRows

                    .Col = ColOprOprCode
                    .Text = Trim(IIf(IsDBNull(RsOprRate.Fields("OPR_CODE").Value), "", RsOprRate.Fields("OPR_CODE").Value))

                    .Col = ColOprOprDesc
                    If MainClass.ValidateWithMasterTable(RsOprRate.Fields("OPR_CODE"), "OPR_CODE", "OPR_DESC", "PRD_OPR_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        .Text = MasterNo
                    End If

                    .Col = ColOprOprRate
                    .Text = VB6.Format(IIf(IsDBNull(RsOprRate.Fields("TOTAL_RATE").Value), 0, RsOprRate.Fields("TOTAL_RATE").Value) / 100, "0.000") '''+ Val(IIf(IsNull(RsOprRate!UNSKILLED_RATE), "", RsOprRate!UNSKILLED_RATE))			

                    RsOprRate.MoveNext()
                    '                I = I + 1			
                    .MaxRows = .MaxRows + 1
                Loop
            End With
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function CheckSubRecord(ByRef pProductCode As String) As Boolean


        On Error GoTo FillERR
        Dim SqlStr As String
        Dim RsShow As ADODB.Recordset
        'Dim mRMCode As String			
        'Dim mSrn As String			
        'Dim xSrn As String			
        'Dim j As Long			
        '			
        CheckSubRecord = False
        SqlStr = " SELECT " & vbCrLf _
        & " IH.PRODUCT_CODE, ID.RM_CODE " & vbCrLf _
        & " FROM PRD_NEWBOM_HDR IH,PRD_NEWBOM_DET ID" & vbCrLf _
        & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
        & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' " & vbCrLf _
        & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_NEWBOM_HDR " & vbCrLf _
        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
        & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "') "       '& vbCrLf _			
        '& " AND WEF<= '" & VB6.Format(pWEF, "DD-MMM-YYYY") & "')" & vbCrLf _			

        SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SUBROWNO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsShow.EOF Then
            '        Do While Not RsShow.EOF			
            '           mRMCode = Trim(IIf(IsNull(RsShow!RM_CODE), "", RsShow!RM_CODE))			
            CheckSubRecord = True
            '        Loop			
        Else

            SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, ID.ITEM_CODE AS RM_CODE " & vbCrLf & " FROM PRD_OUTBOM_HDR IH,PRD_OUTBOM_DET ID" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' " & vbCrLf & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_OUTBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "') "

            SqlStr = SqlStr & vbCrLf & " AND IH.IS_INHOUSE='N' AND IH.STATUS='O'"

            SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SERIAL_NO"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

            If Not RsShow.EOF Then
                '            Do While Not RsShow.EOF			
                '                mRMCode = Trim(IIf(IsNull(RsShow!RM_CODE), "", RsShow!RM_CODE))			
                CheckSubRecord = True
                RsShow.MoveNext()
                '            Loop			
            End If
        End If
        RsShow = Nothing
        '        RsShow.Close			

        Exit Function
FillERR:
        CheckSubRecord = False
        MsgBox(Err.Description)
        '    Resume			
    End Function
    Private Sub FillSubRecord(ByRef pProductCode As String, ByRef pWEF As String, ByVal pSrn As String, ByRef pLevel As Integer, ByRef pMainProductCode As String)

        On Error GoTo FillERR
        Dim SqlStr As String
        Dim RsShow As ADODB.Recordset
        Dim mRMCode As String
        Dim mSrn As String
        Dim xSrn As String
        Dim j As Integer

        mSrn = pSrn
        pLevel = pLevel + 1
        SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.RM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " ID.DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " ID.STD_QTY, ID.GROSS_WT_SCRAP," & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM,UOM_FACTOR "

        SqlStr = SqlStr & vbCrLf _
        & " FROM PRD_NEWBOM_HDR IH,PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf _
        & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
        & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
        & " AND ID.RM_CODE=INVMST.ITEM_CODE " & vbCrLf _
        & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' " & vbCrLf _
        & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_NEWBOM_HDR " & vbCrLf _
        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
        & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "') "       '& vbCrLf _			
        '& " AND WEF<= '" & VB6.Format(pWEF, "DD-MMM-YYYY") & "')" & vbCrLf _			

        SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SUBROWNO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsShow.EOF Then
            Do While Not RsShow.EOF
                '            mcntRow = mcntRow + 1			
                '            sprdbop.MaxRows = sprdbop.MaxRows + 1			
                SprdBOP.Row = mcntRow

                j = j + 1
                xSrn = mSrn & "." & j
                pSrn = pSrn & "." & j

                mRMCode = Trim(IIf(IsDBNull(RsShow.Fields("RM_CODE").Value), "", RsShow.Fields("RM_CODE").Value))
                Call FillGridCol(RsShow, xSrn, pLevel, pMainProductCode, pProductCode)
                RsShow.MoveNext()
            Loop
        Else
            '        Exit Sub			
            SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.ITEM_CODE AS RM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " 'J/W' AS DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " ID.ITEM_QTY AS STD_QTY, ID.SCRAP_QTY AS GROSS_WT_SCRAP," & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM, UOM_FACTOR"

            SqlStr = SqlStr & vbCrLf & " FROM PRD_OUTBOM_HDR IH,PRD_OUTBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' " & vbCrLf & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_OUTBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "') "

            SqlStr = SqlStr & vbCrLf & " AND IH.STATUS='O'"

            SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SERIAL_NO"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

            If Not RsShow.EOF Then
                Do While Not RsShow.EOF
                    '                mcntRow = mcntRow + 1			
                    '                sprdbop.MaxRows = sprdbop.MaxRows + 1			
                    SprdBOP.Row = mcntRow

                    j = j + 1
                    xSrn = mSrn & "." & j
                    pSrn = pSrn & "." & j

                    mRMCode = Trim(IIf(IsDBNull(RsShow.Fields("RM_CODE").Value), "", RsShow.Fields("RM_CODE").Value))
                    Call FillGridCol(RsShow, xSrn, pLevel, pMainProductCode, pProductCode)
                    RsShow.MoveNext()
                Loop
            End If
        End If
        Exit Sub
FillERR:
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

        mTitle = "Finish Goods Costing"

        SqlStr = " SELECT PRD_CUST_FG_COST_HDR.*, PRD_CUST_FG_COST_RM_DET.*, FIN_SUPP_CUST_MST.SUPP_CUST_NAME, " & vbCrLf & " PRODMST.ITEM_SHORT_DESC, PRODMST.CUSTOMER_PART_NO, PREP.EMP_NAME, APP.EMP_NAME " & vbCrLf & " FROM PRD_CUST_FG_COST_HDR, PRD_CUST_FG_COST_RM_DET, FIN_SUPP_CUST_MST, " & vbCrLf & " INV_ITEM_MST PRODMST, PAY_EMPLOYEE_MST PREP, PAY_EMPLOYEE_MST APP " & vbCrLf & " WHERE PRD_CUST_FG_COST_HDR.MKEY=PRD_CUST_FG_COST_RM_DET.MKEY(+) " & vbCrLf & " AND PRD_CUST_FG_COST_HDR.COMPANY_CODE=FIN_SUPP_CUST_MST.COMPANY_CODE(+) " & vbCrLf & " AND PRD_CUST_FG_COST_HDR.SUPP_CUST_CODE=FIN_SUPP_CUST_MST.SUPP_CUST_CODE(+) " & vbCrLf & " AND PRD_CUST_FG_COST_HDR.COMPANY_CODE=PRODMST.COMPANY_CODE(+) " & vbCrLf & " AND PRD_CUST_FG_COST_HDR.PRODUCT_CODE=PRODMST.ITEM_CODE(+) " & vbCrLf & " AND PRD_CUST_FG_COST_HDR.COMPANY_CODE=PREP.COMPANY_CODE(+) " & vbCrLf & " AND PRD_CUST_FG_COST_HDR.PREP_BY=PREP.EMP_CODE(+) " & vbCrLf & " AND PRD_CUST_FG_COST_HDR.COMPANY_CODE=APP.COMPANY_CODE(+) " & vbCrLf & " AND PRD_CUST_FG_COST_HDR.APP_BY=APP.EMP_CODE(+) " & vbCrLf & " AND PRD_CUST_FG_COST_HDR.MKEY='" & MainClass.AllowSingleQuote(nMkey) & "' "

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\FGCosting.rpt"
        SetCrpt(Report1, Mode, 1, mTitle, mSubTitle, True)
        Report1.SQLQuery = SqlStr
        Report1.WindowShowGroupTree = False

        SqlStr1 = " SELECT PRD_CUST_FG_COST_BOP_DET.*, SUBCOSTING.* " & vbCrLf & " FROM PRD_CUST_FG_COST_BOP_DET, " & vbCrLf & " (SELECT DISTINCT COMPANY_CODE, SUPP_CUST_CODE, PRODUCT_CODE " & vbCrLf & " FROM PRD_CUST_FG_COST_HDR) SUBCOSTING " & vbCrLf & " WHERE PRD_CUST_FG_COST_BOP_DET.MKEY='" & MainClass.AllowSingleQuote(nMkey) & "' " & vbCrLf & " AND PRD_CUST_FG_COST_BOP_DET.COMPANY_CODE=SUBCOSTING.COMPANY_CODE (+) " & vbCrLf & " AND PRD_CUST_FG_COST_BOP_DET.ITEM_CODE=SUBCOSTING.PRODUCT_CODE (+) " & vbCrLf & " AND PRD_CUST_FG_COST_BOP_DET.SUPP_CUST_CODE=SUBCOSTING.SUPP_CUST_CODE (+) " & vbCrLf & " ORDER BY PRD_CUST_FG_COST_BOP_DET.SUBROWNO "

        Report1.SubreportToChange = Report1.GetNthSubreportName(0)
        Report1.Connect = STRRptConn
        Report1.SQLQuery = SqlStr1

        SqlStr2 = " SELECT * FROM PRD_CUST_FG_COST_CONVER_DET " & vbCrLf & " WHERE " & vbCrLf & " MKEY='" & MainClass.AllowSingleQuote(nMkey) & "' " & vbCrLf & " ORDER BY SUBROWNO "

        Report1.SubreportToChange = Report1.GetNthSubreportName(1)
        Report1.Connect = STRRptConn
        Report1.SQLQuery = SqlStr2

        SqlStr4 = " SELECT * FROM PRD_CUST_FG_COST_PNT_DET " & vbCrLf & " WHERE " & vbCrLf & " MKEY='" & MainClass.AllowSingleQuote(nMkey) & "' " & vbCrLf & " ORDER BY SUBROWNO "

        Report1.SubreportToChange = Report1.GetNthSubreportName(2)
        Report1.Connect = STRRptConn
        Report1.SQLQuery = SqlStr4

        SqlStr3 = " SELECT * FROM PRD_CUST_FG_COST_PACK_DET " & vbCrLf & " WHERE PRD_CUST_FG_COST_PACK_DET.MKEY='" & MainClass.AllowSingleQuote(nMkey) & "' " & vbCrLf & " ORDER BY PRD_CUST_FG_COST_PACK_DET.SUBROWNO "

        Report1.SubreportToChange = Report1.GetNthSubreportName(3)
        Report1.Connect = STRRptConn
        Report1.SQLQuery = SqlStr3



        Report1.SubreportToChange = ""

        Report1.Action = 1

        '****************			
        SubSqlStr1 = " SELECT * FROM PRD_CUST_FG_COST_BOP_DET " & vbCrLf & " WHERE MKEY='" & MainClass.AllowSingleQuote(nMkey) & "' " & vbCrLf & " ORDER BY SUBROWNO "

        MainClass.UOpenRecordSet(SubSqlStr1, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp1, ADODB.LockTypeEnum.adLockReadOnly)

        With RsTemp1
            If Not .EOF Then
                .MoveFirst()
                Do While Not .EOF
                    mSuppCustCode = IIf(IsDBNull(.Fields("SUPP_CUST_CODE").Value), "", .Fields("SUPP_CUST_CODE").Value)
                    mItemCode = IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)

                    SubSqlStr2 = " SELECT * FROM PRD_CUST_FG_COST_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSuppCustCode) & "' " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' " & vbCrLf & " AND WEF=(SELECT MAX(WEF) FROM PRD_CUST_FG_COST_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSuppCustCode) & "' " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "') "

                    MainClass.UOpenRecordSet(SubSqlStr2, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp2, ADODB.LockTypeEnum.adLockReadOnly)

                    If Not RsTemp2.EOF Then
                        Call ReportOnCosting(RsTemp2.Fields("mKey").Value, Mode)
                    End If

                    .MoveNext()
                Loop
            End If
        End With

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
            mIsShowing = False
            ADDMode = False
            MODIFYMode = False
            txtProductCode_Validating(txtProductCode, New System.ComponentModel.CancelEventArgs(False))
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
        Resume
    End Sub

    Private Sub cmdSearchAppBy_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchAppBy.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If ADDMode = True Then
            SqlStr = SqlStr & " AND EMP_LEAVE_DATE IS NULL "
        End If

        If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            txtAppBy.Text = AcName1
            lblAppBy.Text = AcName
        End If
    End Sub

    Private Sub cmdSearchCopyProdCode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchCopyProdCode.Click
        Dim mSqlStr As String

        mSqlStr = " SELECT IH.SUPP_CUST_CODE, IH.PRODUCT_CODE, INV.ITEM_SHORT_DESC " & vbCrLf & " FROM PRD_CUST_FG_COST_HDR IH, INV_ITEM_MST INV " & vbCrLf & " WHERE IH.COMPANY_CODE=INV.COMPANY_CODE " & vbCrLf & " AND IH.PRODUCT_CODE=INV.ITEM_CODE " & vbCrLf & " AND STATUS='O'"

        mSqlStr = mSqlStr & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If Trim(txtCopyProductCode.Text) <> "" Then
            mSqlStr = mSqlStr & vbCrLf & " AND IH.PRODUCT_CODE='" & Trim(txtCopyProductCode.Text) & "'"
        End If

        If Trim(txtCopyCustCode.Text) <> "" Then
            mSqlStr = mSqlStr & vbCrLf & " AND IH.SUPP_CUST_CODE='" & Trim(txtCopyCustCode.Text) & "'"
        End If

        If MainClass.SearchGridMasterBySQL2("", mSqlStr) = True Then
            txtCopyProductCode.Text = AcName1
            txtCopyCustCode.Text = AcName
            If txtCopyProductCode.Enabled = True Then txtCopyProductCode.Focus()
            Call txtCopyProductCode_Validating(txtCopyProductCode, New System.ComponentModel.CancelEventArgs(False))
        End If
    End Sub

    Private Sub cmdSearchCust_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchCust.Click
        On Error GoTo SrchERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='C' "
        If MainClass.SearchGridMaster("", "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            txtSuppCustCode.Text = AcName1
            txtSuppCustName.Text = AcName
            If txtSuppCustCode.Enabled = True Then txtSuppCustCode.Focus()
        End If
        Exit Sub
SrchERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchProdCode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchProdCode.Click
        On Error GoTo SrchERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value
        If MainClass.SearchGridMaster("", "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", "CUSTOMER_PART_NO", , SqlStr) = True Then
            txtProductCode.Text = AcName1
            txtProductDesc.Text = AcName
            If txtProductCode.Enabled = True Then txtProductCode.Focus()
        End If
        Exit Sub
SrchERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchPrepBy_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchPrepBy.Click
        On Error GoTo SrchERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If ADDMode = True Then
            SqlStr = SqlStr & " AND EMP_LEAVE_DATE IS NULL "
        End If

        If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            txtPrepBy.Text = AcName1
            lblPrepBy.Text = AcName
        End If
        Exit Sub
SrchERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchWEF_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchWEF.Click
        Dim mSqlStr As String

        If Trim(txtProductCode.Text) = "" Then Exit Sub

        mSqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If Trim(txtProductCode.Text) <> "" Then
            mSqlStr = mSqlStr & " AND PRODUCT_CODE='" & Trim(txtProductCode.Text) & "'"
        End If

        If Trim(txtSuppCustCode.Text) <> "" Then
            mSqlStr = mSqlStr & " AND SUPP_CUST_CODE='" & Trim(txtSuppCustCode.Text) & "'"
        End If

        If MainClass.SearchGridMaster("", "PRD_CUST_FG_COST_HDR", "WEF", "SUPP_CUST_CODE", "PRODUCT_CODE", "ISSUE_UOM", mSqlStr) = True Then
            txtWEF.Text = Format(AcName, "DD/MM/YYYY")
            txtSuppCustCode.Text = AcName1
            If txtWEF.Enabled = True Then txtWEF.Focus()
        End If
    End Sub

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Private Sub frmFGCostingCustomerWise_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String
        If FormActive = True Then Exit Sub
        Me.Text = "Finished Goods Costing (Customer Wise)"

        SqlStr = ""
        SqlStr = "Select * from PRD_CUST_FG_COST_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsFGCostMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from PRD_CUST_FG_COST_RM_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsFGCostRMDet, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from PRD_CUST_FG_COST_BOP_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsFGCostBOPDet, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from PRD_CUST_FG_COST_CONVER_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsFGCostOprDet, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from PRD_CUST_FG_COST_WELD_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsFGCostWeldDet, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from PRD_CUST_FG_COST_PNT_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsFGCostPNTDet, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from PRD_CUST_FG_COST_PACK_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsFGCostPackDet, ADODB.LockTypeEnum.adLockReadOnly)

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

    Private Sub frmFGCostingCustomerWise_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmFGCostingCustomerWise_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub frmFGCostingCustomerWise_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        Dim mDocNo As String
        Dim mDateOrg As String
        Dim mRevNo As String
        Dim mDateRev As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        ADDMode = False
        MODIFYMode = False
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7590)			
        'Me.Width = VB6.TwipsToPixelsX(11385)			


        CboPlatingType.Items.Clear()
        CboPlatingType.Items.Add("None")
        CboPlatingType.Items.Add("Nickle")
        CboPlatingType.Items.Add("Zinc")

        cboPowderType.Items.Clear()
        cboPowderType.Items.Add("None")
        cboPowderType.Items.Add("Matt")
        cboPowderType.Items.Add("Glossy")

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub SetTextLengths()
        On Error GoTo ERR1
        With RsFGCostMain
            txtProductCode.MaxLength = .Fields("PRODUCT_CODE").DefinedSize
            txtSuppCustCode.MaxLength = .Fields("SUPP_CUST_CODE").DefinedSize
            txtWEF.MaxLength = .Fields("WEF").DefinedSize - 6
            txtTotRMCost.MaxLength = .Fields("TOT_RM_COST").Precision
            txtTotBOPCost.MaxLength = .Fields("TOT_BOP_COST").Precision
            txtTotProcessCost.MaxLength = .Fields("TOT_PROCESS_COST").Precision
            txtTotWeldCost.MaxLength = .Fields("TOT_WELD_COST").Precision
            txtTotPltCost.MaxLength = .Fields("PLT_TOT_COST_PC").Precision
            txtTotPntCost.MaxLength = .Fields("PNT_TOT_COST_PC").Precision
            txtTotPdrCost.MaxLength = .Fields("PDR_TOT_COST_PC").Precision
            txtTotValueAdd.MaxLength = .Fields("TOT_VALUE_ADD").Precision
            txtTotProdCost.MaxLength = .Fields("TOT_PROD_COST").Precision
            txtOverheadPer.MaxLength = .Fields("OVERHEAD_PER").Precision
            txtOverheadCost.MaxLength = .Fields("OVERHEAD_COST").Precision
            '        txtTotPackCost.MaxLength = .Fields("TOT_PACK_COST").Precision			
            txtRejPer.MaxLength = .Fields("REJ_PER").Precision
            txtRejCost.MaxLength = .Fields("REJ_COST").Precision
            txtTotSaleCost.MaxLength = .Fields("TOT_SALE_COST").Precision
            txtProfitPer.MaxLength = .Fields("PROFIT_PER").Precision
            txtProfitCost.MaxLength = .Fields("PROFIT_COST").Precision
            txtTransportCost.MaxLength = .Fields("TRANSPORT_COST").Precision
            txtToolCost.MaxLength = .Fields("TOT_TOOL_COST").Precision
            txtICC.MaxLength = .Fields("TOT_INTEREST_COST").Precision
            txtHandling.MaxLength = .Fields("TOT_HANDLING_COST").Precision
            txtPMCost.MaxLength = .Fields("TOT_PACK_MAT_COST").Precision


            txtTotSalePrice.MaxLength = .Fields("TOT_SALE_PRICE").Precision
            txtTotPriceSettelled.MaxLength = .Fields("TOT_PRICE_SETTELED").Precision
            txtDiscount.MaxLength = .Fields("DISCOUNT").Precision
            txtCustPONo.MaxLength = .Fields("CUST_PO_NO").Precision
            txtCustPODate.MaxLength = .Fields("CUST_PO_DATE").DefinedSize - 6
            txtRemarks.MaxLength = .Fields("Remarks").DefinedSize
            txtPrepBy.MaxLength = .Fields("PREP_BY").DefinedSize
            txtAppBy.MaxLength = .Fields("APP_BY").DefinedSize

            txtToolQty.MaxLength = .Fields("TOOL_QTY").Precision
            txtToolCostPerPc.MaxLength = .Fields("TOOL_COST_PER_PC").Precision
            txtCostReduction.MaxLength = .Fields("COST_REDUCTION").Precision


        End With
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        '    Resume			
    End Sub

    Private Sub MakeEnableDesableField(ByRef mMode As Boolean)
        txtProductCode.Enabled = mMode
        cmdSearchProdCode.Enabled = mMode
        txtWEF.Enabled = mMode
        txtSuppCustCode.Enabled = mMode
        cmdSearchCust.Enabled = mMode
        txtPrepBy.Enabled = mMode
        cmdSearchPrepBy.Enabled = mMode
        cmdPopulate.Enabled = mMode
    End Sub

    Private Sub frmFGCostingCustomerWise_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False

        RsFGCostMain.Close()
        RsFGCostRMDet.Close()
        RsFGCostBOPDet.Close()
        RsFGCostOprDet.Close()
        RsFGCostWeldDet.Close()
        RsFGCostPackDet.Close()
        RsFGCostMain = Nothing
        RsFGCostRMDet = Nothing
        RsFGCostBOPDet = Nothing
        RsFGCostOprDet = Nothing
        RsFGCostWeldDet = Nothing
        RsFGCostPackDet = Nothing
    End Sub

    Private Sub Clear1()

        Dim i As Integer

        lblMKey.Text = ""
        txtProductCode.Text = ""
        txtProductDesc.Text = ""
        txtUnit.Text = ""
        txtWEF.Text = ""
        txtModelNo.Text = ""
        txtCustPartNo.Text = ""
        txtSuppCustCode.Text = ""
        txtSuppCustName.Text = ""
        txtAmendNo.Text = "0"

        txtTotRMCost.Text = "0.000"
        txtTotBOPCost.Text = "0.000"
        txtTotProcessCost.Text = "0.000"
        txtTotWeldCost.Text = "0.000"
        lblOperationCost.Text = "0.000"

        txtToolQty.Text = "0.00"
        txtToolCostPerPc.Text = "0.00"
        txtCostReduction.Text = "0.00"

        txtTotPltCost.Text = "0.000"
        txtTotPntCost.Text = "0.000"
        txtTotPdrCost.Text = "0.000"
        txtTotValueAdd.Text = "0.000"
        txtTotProdCost.Text = "0.000"
        txtOverheadPer.Text = "0.000"
        txtOverheadCost.Text = "0.000"
        '    txtTotPackCost.Text = "0.000"			
        txtRejPer.Text = "0.000"
        txtRejCost.Text = "0.000"
        txtTotSaleCost.Text = "0.000"
        txtProfitPer.Text = "0.000"
        txtProfitCost.Text = "0.000"
        txtTransportCost.Text = "0.000"
        txtToolCost.Text = "0.000"
        txtHandling.Text = "0.000"
        txtICC.Text = "0.000"
        txtPMCost.Text = "0.000"
        txtTotSalePrice.Text = "0.000"
        txtTotPriceSettelled.Text = "0.000"
        txtDiscount.Text = "0.000"
        txtCustPONo.Text = ""
        txtCustPODate.Text = ""

        txtRemarks.Text = ""
        txtPrepBy.Text = ""
        lblPrepBy.Text = ""
        txtAppBy.Text = ""
        lblAppBy.Text = ""


        txtCopyCustCode.Text = ""
        txtCopyProductCode.Text = ""
        txtCopyProductDesc.Text = ""

        txtCopyProductDesc.Enabled = False
        txtCopyCustCode.Enabled = True
        txtCopyProductCode.Enabled = True
        cmdSearchCopyProdCode.Enabled = True



        lblHandlingCode.Text = "0.000"
        lblToolCost.Text = "0.000"
        lblInterest.Text = "0.000"
        lblPackMaterialCost.Text = "0.000"

        chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked

        CboPlatingType.SelectedIndex = 0
        cboPowderType.SelectedIndex = 0

        mIsShowing = False

        mAmendStatus = False
        cmdAmend.Enabled = True

        MainClass.ClearGrid(SprdRM)
        FormatSprdRM(-1)

        MainClass.ClearGrid(SprdBOP)
        FormatSprdBOP(-1)

        MainClass.ClearGrid(SprdWeld)
        FormatSprdWeld(-1)

        MainClass.ClearGrid(SprdOpr)
        FormatSprdOPR(-1)

        With SprdPlt
            .Col = 4
            For i = 1 To .MaxRows
                .Row = i
                .Text = "0.000"
            Next
        End With
        FormatSprdPlt(-1)

        '    With SprdPnt			
        '        .Col = 4			
        '        For I = 1 To .MaxRows			
        '            .Row = I			
        '            .Text = "0.000"			
        '        Next			
        '    End With			
        MainClass.ClearGrid(SprdPnt)
        FormatSprdPNT(-1)

        With SprdPdr
            .Col = 4
            For i = 1 To .MaxRows
                .Row = i
                .Text = "0.000"
            Next
        End With
        FormatSprdPdr(-1)

        MainClass.ClearGrid(SprdPack)
        FormatSprdPack(-1)
        cmdPopulate.Enabled = False

        SSTab1.SelectedIndex = 0

        Call MakeEnableDesableField(True)

        MainClass.ButtonStatus(Me, XRIGHT, RsFGCostMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub FormatSprdRM(ByRef mRow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer

        With SprdRM
            .Row = mRow
            .set_RowHeight(0, ConRowHeight * 2.5)
            .set_RowHeight(mRow, ConRowHeight)

            .Col = ColMannualCalc
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER

            .Col = ColProductDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = 255
            .set_ColWidth(.Col, 25)

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
            .TypeEditLen = RsFGCostRMDet.Fields("ISSUE_UOM").DefinedSize
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
        '    MainClass.ProtectCell SprdMain, 1, SprdMain.MaxRows, ColRMUOM, ColRMUOM			

        MainClass.ProtectCell(SprdRM, 1, SprdRM.MaxRows, ColWtPerStrip, ColWtPerStrip)
        MainClass.ProtectCell(SprdRM, 1, SprdRM.MaxRows, ColWtPerPc, ColRMCost)
        MainClass.ProtectCell(SprdRM, 1, SprdRM.MaxRows, ColScrapWt, ColScrapWt)
        MainClass.ProtectCell(SprdRM, 1, SprdRM.MaxRows, ColScrapCost, ColNetRMCost)

        Call LockSprdRM()

        MainClass.SetSpreadColor(SprdRM, mRow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then
            RsFGCostRMDet.Requery()
            '        Resume			
        End If
        MsgBox(Err.Description, MsgBoxStyle.Information)
        'On Error GoTo ERR1			
        'Dim mCol As Long			
        '			
        '    With SprdRM			
        '        .Row = mRow			
        '        .RowHeight(0) = ConRowHeight * 2.5			
        '        .RowHeight(mRow) = ConRowHeight			
        '			
        '        .Col = ColRMNewItem			
        '        .CellType = SS_CELL_TYPE_CHECKBOX			
        '        .TypeHAlign = SS_CELL_H_ALIGN_CENTER			
        '			
        '        .Col = ColRMItemCode			
        '        .CellType = SS_CELL_TYPE_EDIT			
        '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII			
        '        .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE			
        '        .TypeEditMultiLine = False			
        '        .TypeEditLen = RsFGCostRMDet.Fields("ITEM_CODE").DefinedSize			
        '			
        '        .Col = ColRMItemDesc			
        '        .CellType = SS_CELL_TYPE_EDIT			
        '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII			
        '        .TypeEditMultiLine = True			
        '        .TypeEditLen = RsFGCostRMDet.Fields("ITEM_DESC").DefinedSize			
        '			
        '        For mCol = ColRMGrossWt To ColRMTotAmount			
        '            .Col = mCol			
        '            .CellType = SS_CELL_TYPE_FLOAT			
        '            .TypeFloatDecimalChar = Asc(".")			
        '            .TypeFloatDecimalPlaces = 3			
        '            .TypeFloatMax = "9999999.999"			
        '            .TypeFloatMin = "-9999999.999"			
        '            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC			
        '        Next			
        '			
        '        .Col = ColRMRemarks			
        '        .CellType = SS_CELL_TYPE_EDIT			
        '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII			
        '        .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE			
        '        .TypeEditMultiLine = False			
        '        .TypeEditLen = RsFGCostRMDet.Fields("REMARKS").DefinedSize			
        '			
        '        MainClass.ProtectCell SprdRM, 1, .MaxRows, ColRMItemAmount, ColRMItemAmount			
        '        MainClass.ProtectCell SprdRM, 1, .MaxRows, ColRMScrapAmount, ColRMNetAmount			
        '        MainClass.ProtectCell SprdRM, 1, .MaxRows, ColRMTotAmount, ColRMTotAmount			
        '    End With			
        '			
        '    MainClass.SetSpreadColor SprdRM, mRow			
        '    Exit Sub			
        'ERR1:			
        '    If err.Number = -2147418113 Then			
        '        RsFGCostRMDet.Requery			
        ''        Resume			
        '    End If			
        '    MsgBox err.Description, vbInformation			
    End Sub

    Private Sub LockSprdRM()

        On Error GoTo ERR1
        Dim i As Integer
        Dim mNewItem As Integer
        Dim mMannualCalc As Integer

        With SprdRM
            MainClass.UnProtectCell(SprdRM, 1, .MaxRows, 1, .MaxCols)

            '        MainClass.ProtectCell SprdRM, 1, SprdRM.MaxRows, ColWtPerStrip, ColWtPerStrip			
            MainClass.ProtectCell(SprdRM, 1, SprdRM.MaxRows, ColWtPerPc, ColRMCost)
            MainClass.ProtectCell(SprdRM, 1, SprdRM.MaxRows, ColScrapWt, ColScrapWt)
            MainClass.ProtectCell(SprdRM, 1, SprdRM.MaxRows, ColScrapCost, ColNetRMCost)


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

        '    With SprdRM			
        '        MainClass.UnProtectCell SprdRM, 1, .MaxRows, 1, .MaxCols			
        '			
        '        MainClass.ProtectCell SprdRM, 1, .MaxRows, ColRMItemAmount, ColRMItemAmount			
        '        MainClass.ProtectCell SprdRM, 1, .MaxRows, ColRMScrapAmount, ColRMNetAmount			
        '        MainClass.ProtectCell SprdRM, 1, .MaxRows, ColRMTotAmount, ColRMTotAmount			
        '			
        '        For i = 1 To .MaxRows			
        '            .Row = i			
        '            .Col = ColRMNewItem			
        '            mNewItem = .Value			
        '            If mNewItem = vbUnchecked Then			
        '                .Row = i			
        '                .Row2 = i			
        '                .Col = ColRMItemDesc			
        '                .col2 = ColRMItemDesc			
        '                .BlockMode = True			
        '                .Protect = True			
        '                .Lock = True			
        '                .BlockMode = False			
        '            End If			
        '        Next			
        '    End With			

        Exit Sub
ERR1:
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub

    Private Sub FormatSprdBOP(ByRef mRow As Integer)

        On Error GoTo ERR1
        Dim mCol As Integer

        With SprdBOP
            .Row = mRow
            .set_RowHeight(0, ConRowHeight * 2.5)
            .set_RowHeight(mRow, ConRowHeight)

            .Col = ColBOPNewItem
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(.Col, 5)

            .Col = ColBOPItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsFGCostBOPDet.Fields("ITEM_CODE").DefinedSize

            .Col = ColBOPItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = RsFGCostBOPDet.Fields("ITEM_DESC").DefinedSize
            .set_ColWidth(.Col, 18)

            .Col = ColBOPItemUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsFGCostBOPDet.Fields("ITEM_UOM").DefinedSize
            .set_ColWidth(.Col, 4)

            For mCol = ColBOPItemQty To ColBOPTotAmount
                .Col = mCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatDecimalPlaces = 3
                .TypeFloatMax = CDbl("9999999.999")
                .TypeFloatMin = CDbl("-9999999.999")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(.Col, 7)
            Next

            .Col = ColBOPSubCosting
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(.Col, 5)

            .Col = ColBOPRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsFGCostBOPDet.Fields("REMARKS").DefinedSize
            .set_ColWidth(.Col, 12)

            MainClass.ProtectCell(SprdBOP, 1, .MaxRows, ColBOPItemAmount, ColBOPItemAmount)
            MainClass.ProtectCell(SprdBOP, 1, .MaxRows, ColBOPTotAmount, ColBOPSubCosting)
        End With

        MainClass.SetSpreadColor(SprdBOP, mRow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then
            RsFGCostBOPDet.Requery()
            '        Resume			
        End If
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub

    Private Sub FormatSprdPNT(ByRef mRow As Integer)

        On Error GoTo ERR1
        Dim mCol As Integer

        With SprdPnt
            .Row = mRow
            .set_RowHeight(0, ConRowHeight * 2.5)
            .set_RowHeight(mRow, ConRowHeight)


            .Col = ColPNTItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsFGCostPNTDet.Fields("ITEM_CODE").DefinedSize

            .Col = ColPNTItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = RsFGCostPNTDet.Fields("ITEM_DESC").DefinedSize

            .Col = ColPNTItemUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsFGCostPNTDet.Fields("ITEM_UOM").DefinedSize

            For mCol = ColPNTItemQty To ColPNTItemAmount
                .Col = mCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatDecimalPlaces = 3
                .TypeFloatMax = CDbl("9999999.999")
                .TypeFloatMin = CDbl("-9999999.999")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            Next

            .Col = ColPNTRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsFGCostPNTDet.Fields("REMARKS").DefinedSize

            MainClass.ProtectCell(SprdPnt, 1, .MaxRows, ColPNTItemDesc, ColPNTItemUOM)
            MainClass.ProtectCell(SprdPnt, 1, .MaxRows, ColPNTItemAmount, ColPNTItemAmount)
        End With

        MainClass.SetSpreadColor(SprdPnt, mRow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then
            RsFGCostPNTDet.Requery()
            '        Resume			
        End If
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub


    Private Sub LockSprdBOP()

        On Error GoTo ERR1
        Dim i As Integer
        Dim mNewItem As Integer

        With SprdBOP
            MainClass.UnProtectCell(SprdBOP, 1, .MaxRows, 1, .MaxCols)

            MainClass.ProtectCell(SprdBOP, 1, .MaxRows, ColBOPItemAmount, ColBOPItemAmount)
            MainClass.ProtectCell(SprdBOP, 1, .MaxRows, ColBOPTotAmount, ColBOPSubCosting)

            For i = 1 To .MaxRows
                .Row = i
                .Col = ColBOPNewItem
                mNewItem = CInt(.Value)
                If mNewItem = System.Windows.Forms.CheckState.Unchecked Then
                    .Row = i
                    .Row2 = i
                    .Col = ColBOPItemDesc
                    .Col2 = ColBOPItemUOM
                    .BlockMode = True
                    .Protect = True
                    .Lock = True
                    .BlockMode = False
                End If

                .Col = ColBOPSubCosting
                mNewItem = CInt(.Value)
                If mNewItem = System.Windows.Forms.CheckState.Checked Then
                    .Row = i
                    .Row2 = i
                    .Col = ColBOPItemRate
                    .Col2 = ColBOPItemRate
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

    Private Sub FormatSprdOPR(ByRef mRow As Integer)

        On Error GoTo ERR1
        With SprdOpr
            .MaxCols = ColOprRemarks

            .Row = mRow
            .set_RowHeight(0, ConRowHeight * 2.5)
            .set_RowHeight(mRow, ConRowHeight)

            .Col = ColOprOprCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            '        .TypeEditLen = RsFGCostOprDet.Fields("OPR_CODE").DefinedSize			
            .ColHidden = True

            .Col = ColOprOprDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsFGCostOprDet.Fields("CONVER_DESC").DefinedSize
            .TypeEditLen = 255

            If mIsShowing = False Then
                .Col = ColOprType
                .CellType = SS_CELL_TYPE_COMBOBOX
                .TypeComboBoxEditable = False

                .TypeComboBoxList = ""
                .TypeComboBoxList = "1.Welding Cost" & Chr(9) & "2.Process Cost" & Chr(9) & "3.Consumable Cost" & Chr(9) & "4.Others Cost"

                .TypeComboBoxCurSel = 0
            End If

            .Col = ColOprOprRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 3
            .TypeFloatMax = CDbl("9999999.999")
            .TypeFloatMin = CDbl("-9999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC

            .Col = ColOprRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False

            .TypeEditLen = RsFGCostOprDet.Fields("REMARKS").DefinedSize

            '        MainClass.ProtectCell SprdOPR, 1, .MaxRows, ColOprOprDesc, ColOprOprDesc			
            .Row = 0
            .Col = ColOprType
            .Text = "Type"
            .set_ColWidth(.Col, 20)

            .Col = ColOprRemarks
            .Text = "Remarks"
            .set_ColWidth(.Col, 20)

        End With

        MainClass.SetSpreadColor(SprdOpr, mRow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then
            RsFGCostOprDet.Requery()
            '        Resume			
        End If
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub FormatSprdWeld(ByRef mRow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer

        With SprdWeld
            .MaxCols = ColWeldRemarks

            .Row = mRow
            .set_RowHeight(0, ConRowHeight * 2.5)
            .set_RowHeight(mRow, ConRowHeight)


            .Col = ColWeldDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsFGCostWeldDet.Fields("WELD_DESC").DefinedSize
            .TypeEditLen = 255

            If mIsShowing = False Then
                .Col = ColWeldType
                .CellType = SS_CELL_TYPE_COMBOBOX
                .TypeComboBoxEditable = False

                .TypeComboBoxList = ""
                .TypeComboBoxList = "1.Spot Welding" & Chr(9) & "2.MIG Welding" & Chr(9) & "3.TIG Welding" & Chr(9) & "4.Projection Welding" & Chr(9) & "5.Tack Welding" & Chr(9) & "6.Others"

                .TypeComboBoxCurSel = 0
            End If

            .Col = ColWeldUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsFGCostWeldDet.Fields("WELD_UOM").DefinedSize
            .TypeEditLen = 255

            For cntCol = ColWeldQty To ColWeldAmount
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatDecimalPlaces = 3
                .TypeFloatMax = CDbl("9999999.999")
                .TypeFloatMin = CDbl("-9999999.999")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            Next

            .Col = ColWeldRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False

            .TypeEditLen = RsFGCostWeldDet.Fields("REMARKS").DefinedSize

            MainClass.ProtectCell(SprdWeld, 1, .MaxRows, ColWeldAmount, ColWeldAmount)
            '        .Row = 0			
            '        .Col = ColOprType			
            '        .Text = "Type"			
            '        .ColWidth(.Col) = 20			
            '			
            '        .Col = ColOprRemarks			
            '        .Text = "Remarks"			
            '        .ColWidth(.Col) = 20			

        End With

        MainClass.SetSpreadColor(SprdWeld, mRow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then
            RsFGCostWeldDet.Requery()
            '        Resume			
        End If
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub FormatSprdPlt(ByRef mRow As Integer)

        On Error GoTo ERR1

        With SprdPlt
            .Row = mRow

            MainClass.SetSpreadColor(SprdPlt, mRow)

            .Col = 1
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER

            .Col = 2
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT

            .Col = 3
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER

            .Col = 4
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 3
            .TypeFloatMax = CDbl("9999999.999")
            .TypeFloatMin = CDbl("-9999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC

            .Col = 5
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT

            MainClass.ProtectCell(SprdPlt, 1, .MaxRows, 1, 3)
            MainClass.ProtectCell(SprdPlt, 1, .MaxRows, 5, 5)

            .Row = 1
            .Row2 = .MaxRows
            .Col = 1
            .Col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF)
            .BlockMode = False

            .Col = 4
            .Col2 = 4

            .Row = 1
            .Row2 = 2
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
            .Protect = False
            .Lock = False
            .BlockMode = False

            .Row = 3
            .Row2 = 3
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF)
            .Protect = True
            .Lock = True
            .BlockMode = False

            .Row = 4
            .Row2 = 4
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
            .Protect = False
            .Lock = False
            .BlockMode = False

            .Row = 5
            .Row2 = 5
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF)
            .Protect = True
            .Lock = True
            .BlockMode = False

            .Row = 6
            .Row2 = 7
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
            .Protect = False
            .Lock = False
            .BlockMode = False

            .Row = 8
            .Row2 = 8
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF)
            .Protect = True
            .Lock = True
            .BlockMode = False

            .Row = 9
            .Row2 = 9
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
            .Protect = False
            .Lock = False
            .BlockMode = False

            .Row = 10
            .Row2 = 11
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF)
            .Protect = True
            .Lock = True
            .BlockMode = False

            .Row = 12
            .Row2 = 12
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
            .Protect = False
            .Lock = False
            .BlockMode = False

            .Row = 13
            .Row2 = 13
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF)
            .Protect = True
            .Lock = True
            .BlockMode = False

            .Row = 14
            .Row2 = 15
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
            .Protect = False
            .Lock = False
            .BlockMode = False

            .Row = 16
            .Row2 = 17
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF)
            .Protect = True
            .Lock = True
            .BlockMode = False

            .Row = 18
            .Row2 = 18
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
            .Protect = False
            .Lock = False
            .BlockMode = False

            .Row = 19
            .Row2 = 19
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF)
            .Protect = True
            .Lock = True
            .BlockMode = False

            .Row = 20
            .Row2 = 20
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
            .Protect = False
            .Lock = False
            .BlockMode = False

            .Row = 21
            .Row2 = 21
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF)
            .Protect = True
            .Lock = True
            .BlockMode = False

            .Row = 22
            .Row2 = 22
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
            .Protect = False
            .Lock = False
            .BlockMode = False

            .Row = 23
            .Row2 = 23
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF)
            .Protect = True
            .Lock = True
            .BlockMode = False

            .Row = 24
            .Row2 = 24
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
            .Protect = False
            .Lock = False
            .BlockMode = False

            .Row = 25
            .Row2 = 25
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF)
            .Protect = True
            .Lock = True
            .BlockMode = False

            .Row = 26
            .Row2 = 26
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
            .Protect = False
            .Lock = False
            .BlockMode = False

            .Row = 27
            .Row2 = 27
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF)
            .Protect = True
            .Lock = True
            .BlockMode = False

            .Row = 28
            .Row2 = 28
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
            .Protect = False
            .Lock = False
            .BlockMode = False

            .Row = 29
            .Row2 = 30
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF)
            .Protect = True
            .Lock = True
            .BlockMode = False

        End With

        Exit Sub
ERR1:
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub

    Private Sub FormatSprdPntOld(ByRef mRow As Integer)

        On Error GoTo ERR1

        With SprdPnt
            .Row = mRow

            MainClass.SetSpreadColor(SprdPnt, mRow)

            .Col = 1
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER

            .Col = 2
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT

            .Col = 3
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER

            .Col = 4
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 3
            .TypeFloatMax = CDbl("9999999.999")
            .TypeFloatMin = CDbl("-9999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC

            .Col = 5
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT

            MainClass.ProtectCell(SprdPnt, 1, .MaxRows, 1, 3)
            MainClass.ProtectCell(SprdPnt, 1, .MaxRows, 5, 5)

            .Row = 1
            .Row2 = .MaxRows
            .Col = 1
            .Col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF)
            .BlockMode = False

            .Col = 4
            .Col2 = 4

            .Row = 1
            .Row2 = 3
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF)
            .Protect = True
            .Lock = True
            .BlockMode = False

            .Row = 4
            .Row2 = 4
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
            .Protect = False
            .Lock = False
            .BlockMode = False

            .Row = 5
            .Row2 = 5
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF)
            .Protect = True
            .Lock = True
            .BlockMode = False

            .Row = 6
            .Row2 = 7
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
            .Protect = False
            .Lock = False
            .BlockMode = False

            .Row = 8
            .Row2 = 8
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF)
            .Protect = True
            .Lock = True
            .BlockMode = False

            .Row = 9
            .Row2 = 9
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
            .Protect = False
            .Lock = False
            .BlockMode = False

            .Row = 10
            .Row2 = 11
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF)
            .Protect = True
            .Lock = True
            .BlockMode = False

            .Row = 12
            .Row2 = 12
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
            .Protect = False
            .Lock = False
            .BlockMode = False

            .Row = 13
            .Row2 = 14
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF)
            .Protect = True
            .Lock = True
            .BlockMode = False

        End With

        Exit Sub
ERR1:
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub

    Private Sub FormatSprdPdr(ByRef mRow As Integer)

        On Error GoTo ERR1

        With SprdPdr
            .Row = mRow

            MainClass.SetSpreadColor(SprdPdr, mRow)

            .Col = 1
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER

            .Col = 2
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT

            .Col = 3
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER

            .Col = 4
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 3
            .TypeFloatMax = CDbl("9999999.999")
            .TypeFloatMin = CDbl("-9999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC

            .Col = 5
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT

            MainClass.ProtectCell(SprdPdr, 1, .MaxRows, 1, 3)
            MainClass.ProtectCell(SprdPdr, 1, .MaxRows, 5, 5)

            .Row = 1
            .Row2 = .MaxRows
            .Col = 1
            .Col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF)
            .BlockMode = False

            .Col = 4
            .Col2 = 4

            .Row = 1
            .Row2 = 3
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF)
            .Protect = True
            .Lock = True
            .BlockMode = False

            .Row = 4
            .Row2 = 4
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
            .Protect = False
            .Lock = False
            .BlockMode = False

            .Row = 5
            .Row2 = 5
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF)
            .Protect = True
            .Lock = True
            .BlockMode = False

            .Row = 6
            .Row2 = 7
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
            .Protect = False
            .Lock = False
            .BlockMode = False

            .Row = 8
            .Row2 = 8
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF)
            .Protect = True
            .Lock = True
            .BlockMode = False

            .Row = 9
            .Row2 = 9
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
            .Protect = False
            .Lock = False
            .BlockMode = False

            .Row = 10
            .Row2 = 11
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF)
            .Protect = True
            .Lock = True
            .BlockMode = False

            .Row = 12
            .Row2 = 12
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
            .Protect = False
            .Lock = False
            .BlockMode = False

            .Row = 13
            .Row2 = 14
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF)
            .Protect = True
            .Lock = True
            .BlockMode = False

        End With

        Exit Sub
ERR1:
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub

    Private Sub FormatSprdPack(ByRef mRow As Integer)

        On Error GoTo ERR1
        With SprdPack
            .MaxCols = ColPackRemarks
            .Row = mRow

            .set_RowHeight(0, ConRowHeight * 2.5)
            .set_RowHeight(mRow, ConRowHeight)

            .Col = ColPackPackDetail
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsFGCostPackDet.Fields("PACK_DETAIL").DefinedSize

            .Col = ColPackPackRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 3
            .TypeFloatMax = CDbl("9999999.999")
            .TypeFloatMin = CDbl("-9999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC

            If mIsShowing = False Then
                .Col = ColPackType
                .CellType = SS_CELL_TYPE_COMBOBOX
                .TypeComboBoxEditable = False
                .TypeComboBoxList = ""
                .TypeComboBoxList = "1.Handling Charges" & Chr(9) & "2.Tool Cost" & Chr(9) & "3.Interest" & Chr(9) & "4.Pack Cost"
                .TypeComboBoxCurSel = 0
                .set_ColWidth(ColPackType, 9)
            End If

            .Col = ColPackRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = RsFGCostPackDet.Fields("REMARKS").DefinedSize

            .Row = 0
            .Col = ColPackType
            .Text = "Type"
            .set_ColWidth(.Col, 16)

            .Col = ColPackRemarks
            .Text = "Remarks"
            .set_ColWidth(.Col, 16)
        End With


        MainClass.SetSpreadColor(SprdPack, mRow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then
            RsFGCostPackDet.Requery()
            '        Resume			
        End If
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub

    Private Sub Show1()

        On Error GoTo ERR1

        With RsFGCostMain
            If Not .EOF Then
                ADDMode = False
                MODIFYMode = False
                mIsShowing = True

                lblMKey.Text = .Fields("MKey").Value

                txtProductCode.Text = IIf(IsDBNull(.Fields("PRODUCT_CODE").Value), "", .Fields("PRODUCT_CODE").Value)
                If MainClass.ValidateWithMasterTable(txtProductCode.Text, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtProductDesc.Text = MasterNo
                End If

                txtUnit.Text = IIf(IsDBNull(.Fields("ISSUE_UOM").Value), "", .Fields("ISSUE_UOM").Value)
                txtWEF.Text = IIf(IsDBNull(.Fields("WEF").Value), "", .Fields("WEF").Value)
                If MainClass.ValidateWithMasterTable(txtProductCode.Text, "ITEM_CODE", "ITEM_MODEL", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtModelNo.Text = MasterNo
                End If
                If MainClass.ValidateWithMasterTable(txtProductCode.Text, "ITEM_CODE", "CUSTOMER_PART_NO", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtCustPartNo.Text = MasterNo
                End If

                txtSuppCustCode.Text = IIf(IsDBNull(.Fields("SUPP_CUST_CODE").Value), "", .Fields("SUPP_CUST_CODE").Value)
                If MainClass.ValidateWithMasterTable(txtSuppCustCode.Text, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtSuppCustName.Text = MasterNo
                End If
                txtAmendNo.Text = IIf(IsDBNull(.Fields("AMEND_NO").Value), "", .Fields("AMEND_NO").Value)

                txtTotRMCost.Text = IIf(IsDBNull(.Fields("TOT_RM_COST").Value), "0.000", .Fields("TOT_RM_COST").Value)
                txtTotBOPCost.Text = IIf(IsDBNull(.Fields("TOT_BOP_COST").Value), "0.000", .Fields("TOT_BOP_COST").Value)

                txtToolQty.Text = IIf(IsDBNull(.Fields("TOOL_QTY").Value), "0.000", .Fields("TOOL_QTY").Value)
                txtToolCostPerPc.Text = IIf(IsDBNull(.Fields("TOOL_COST_PER_PC").Value), "0.000", .Fields("TOOL_COST_PER_PC").Value)
                txtCostReduction.Text = IIf(IsDBNull(.Fields("COST_REDUCTION").Value), "0.000", .Fields("COST_REDUCTION").Value)


                txtTotWeldCost.Text = IIf(IsDBNull(.Fields("TOT_WELD_COST").Value), "0.000", .Fields("TOT_WELD_COST").Value)
                txtTotProcessCost.Text = IIf(IsDBNull(.Fields("TOT_PROCESS_COST").Value), "0.000", .Fields("TOT_PROCESS_COST").Value)
                lblOperationCost.Text = IIf(IsDBNull(.Fields("TOT_OPR_COST").Value), "0.000", .Fields("TOT_OPR_COST").Value)

                txtTotPltCost.Text = IIf(IsDBNull(.Fields("PLT_TOT_COST_PC").Value), "0.000", .Fields("PLT_TOT_COST_PC").Value)
                txtTotPntCost.Text = IIf(IsDBNull(.Fields("PNT_TOT_COST_PC").Value), "0.000", .Fields("PNT_TOT_COST_PC").Value)
                txtTotPdrCost.Text = IIf(IsDBNull(.Fields("PDR_TOT_COST_PC").Value), "0.000", .Fields("PDR_TOT_COST_PC").Value)
                txtTotValueAdd.Text = IIf(IsDBNull(.Fields("TOT_VALUE_ADD").Value), "0.000", .Fields("TOT_VALUE_ADD").Value)
                txtTotProdCost.Text = IIf(IsDBNull(.Fields("TOT_PROD_COST").Value), "0.000", .Fields("TOT_PROD_COST").Value)
                txtOverheadPer.Text = IIf(IsDBNull(.Fields("OVERHEAD_PER").Value), "0.000", VB6.Format(.Fields("OVERHEAD_PER").Value, "0.000"))
                txtOverheadCost.Text = IIf(IsDBNull(.Fields("OVERHEAD_COST").Value), "0.000", .Fields("OVERHEAD_COST").Value)
                '            txtTotPackCost.Text = IIf(IsNull(.Fields("TOT_PACK_COST")), "0.000", .Fields("TOT_PACK_COST"))			
                txtRejPer.Text = IIf(IsDBNull(.Fields("REJ_PER").Value), "0.000", VB6.Format(.Fields("REJ_PER").Value, "0.000"))
                txtRejCost.Text = IIf(IsDBNull(.Fields("REJ_COST").Value), "0.000", .Fields("REJ_COST").Value)
                txtTotSaleCost.Text = IIf(IsDBNull(.Fields("TOT_SALE_COST").Value), "0.000", .Fields("TOT_SALE_COST").Value)
                txtProfitPer.Text = IIf(IsDBNull(.Fields("PROFIT_PER").Value), "0.000", VB6.Format(.Fields("PROFIT_PER").Value, "0.000"))
                txtProfitCost.Text = IIf(IsDBNull(.Fields("PROFIT_COST").Value), "0.000", .Fields("PROFIT_COST").Value)
                txtTransportCost.Text = IIf(IsDBNull(.Fields("TRANSPORT_COST").Value), "0.000", VB6.Format(.Fields("TRANSPORT_COST").Value, "0.000"))
                txtToolCost.Text = IIf(IsDBNull(.Fields("TOT_TOOL_COST").Value), "0.000", VB6.Format(.Fields("TOT_TOOL_COST").Value, "0.000"))

                txtICC.Text = IIf(IsDBNull(.Fields("TOT_INTEREST_COST").Value), "0.000", VB6.Format(.Fields("TOT_INTEREST_COST").Value, "0.000"))
                txtHandling.Text = IIf(IsDBNull(.Fields("TOT_HANDLING_COST").Value), "0.000", VB6.Format(.Fields("TOT_HANDLING_COST").Value, "0.000"))
                txtPMCost.Text = IIf(IsDBNull(.Fields("TOT_PACK_MAT_COST").Value), "0.000", VB6.Format(.Fields("TOT_PACK_MAT_COST").Value, "0.000"))

                txtTotSalePrice.Text = IIf(IsDBNull(.Fields("TOT_SALE_PRICE").Value), "0.000", .Fields("TOT_SALE_PRICE").Value)
                txtTotPriceSettelled.Text = IIf(IsDBNull(.Fields("TOT_PRICE_SETTELED").Value), "0.000", VB6.Format(.Fields("TOT_PRICE_SETTELED").Value, "0.000"))
                txtDiscount.Text = IIf(IsDBNull(.Fields("DISCOUNT").Value), "0.000", .Fields("DISCOUNT").Value)
                txtCustPONo.Text = IIf(IsDBNull(.Fields("CUST_PO_NO").Value), "", .Fields("CUST_PO_NO").Value)
                txtCustPODate.Text = IIf(IsDBNull(.Fields("CUST_PO_DATE").Value), "", VB6.Format(.Fields("CUST_PO_DATE").Value, "DD/MM/YYYY"))

                txtRemarks.Text = IIf(IsDBNull(.Fields("Remarks").Value), "", .Fields("Remarks").Value)
                txtPrepBy.Text = IIf(IsDBNull(.Fields("PREP_BY").Value), "", .Fields("PREP_BY").Value)
                txtPrepBy_Validating(txtPrepBy, New System.ComponentModel.CancelEventArgs(False))
                txtAppBy.Text = IIf(IsDBNull(.Fields("APP_BY").Value), "", .Fields("APP_BY").Value)
                txtAppBy_Validating(txtAppBy, New System.ComponentModel.CancelEventArgs(False))


                CboPlatingType.Text = IIf(IsDBNull(.Fields("PLT_TYPE").Value), "", .Fields("PLT_TYPE").Value)
                cboPowderType.Text = IIf(IsDBNull(.Fields("POWDER_TYPE").Value), "", .Fields("POWDER_TYPE").Value)

                lblHandlingCode.Text = IIf(IsDBNull(.Fields("TOT_HANDLING_COST").Value), "0.000", .Fields("TOT_HANDLING_COST").Value)
                lblToolCost.Text = IIf(IsDBNull(.Fields("TOT_TOOL_COST").Value), "0.000", .Fields("TOT_TOOL_COST").Value)
                lblInterest.Text = IIf(IsDBNull(.Fields("TOT_INTEREST_COST").Value), "0.000", .Fields("TOT_INTEREST_COST").Value)
                lblPackMaterialCost.Text = IIf(IsDBNull(.Fields("TOT_PACK_MAT_COST").Value), "0.000", .Fields("TOT_PACK_MAT_COST").Value)

                chkStatus.CheckState = IIf(.Fields("Status").Value = "O", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)

                Call ShowRMDetail()
                Call ShowBOPDetail()
                Call ShowWeldDetail()
                Call ShowOprDetail()
                Call ShowPltDetail()
                Call ShowPNTDetail()
                Call ShowPdrDetail()
                Call ShowPackDetail()
                Call AutoCalc()

                txtCopyCustCode.Enabled = False
                txtCopyProductCode.Enabled = False
                cmdSearchCopyProdCode.Enabled = False

                Call MakeEnableDesableField(False)
                mIsShowing = False

            End If
        End With
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsFGCostMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        SprdRM.Enabled = False
        SprdBOP.Enabled = False
        SprdOpr.Enabled = False
        SprdPlt.Enabled = False
        SprdPnt.Enabled = False
        SprdPdr.Enabled = False
        SprdPack.Enabled = False
        txtProductCode.Enabled = False
        cmdSearchProdCode.Enabled = False
        cmdSearchWEF.Enabled = False
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
        '    Resume			
    End Sub

    Private Sub Copy1(ByRef pRs As ADODB.Recordset, ByRef pCopyMkey As String)
        On Error GoTo ERR1

        Clear1()

        With pRs
            If Not .EOF Then

                txtTotRMCost.Text = IIf(IsDBNull(.Fields("TOT_RM_COST").Value), "0.000", .Fields("TOT_RM_COST").Value)
                txtTotBOPCost.Text = IIf(IsDBNull(.Fields("TOT_BOP_COST").Value), "0.000", .Fields("TOT_BOP_COST").Value)
                txtTotWeldCost.Text = IIf(IsDBNull(.Fields("TOT_WELD_COST").Value), "0.000", .Fields("TOT_WELD_COST").Value)
                txtTotProcessCost.Text = IIf(IsDBNull(.Fields("TOT_PROCESS_COST").Value), "0.000", .Fields("TOT_PROCESS_COST").Value)
                txtTotPltCost.Text = IIf(IsDBNull(.Fields("PLT_TOT_COST_PC").Value), "0.000", .Fields("PLT_TOT_COST_PC").Value)
                txtTotPntCost.Text = IIf(IsDBNull(.Fields("PNT_TOT_COST_PC").Value), "0.000", .Fields("PNT_TOT_COST_PC").Value)
                txtTotPdrCost.Text = IIf(IsDBNull(.Fields("PDR_TOT_COST_PC").Value), "0.000", .Fields("PDR_TOT_COST_PC").Value)
                txtTotValueAdd.Text = IIf(IsDBNull(.Fields("TOT_VALUE_ADD").Value), "0.000", .Fields("TOT_VALUE_ADD").Value)
                txtTotProdCost.Text = IIf(IsDBNull(.Fields("TOT_PROD_COST").Value), "0.000", .Fields("TOT_PROD_COST").Value)
                txtOverheadPer.Text = IIf(IsDBNull(.Fields("OVERHEAD_PER").Value), "0.000", VB6.Format(.Fields("OVERHEAD_PER").Value, "0.000"))
                txtOverheadCost.Text = IIf(IsDBNull(.Fields("OVERHEAD_COST").Value), "0.000", .Fields("OVERHEAD_COST").Value)
                '            txtTotPackCost.Text = IIf(IsNull(.Fields("TOT_PACK_COST")), "0.000", .Fields("TOT_PACK_COST"))			
                txtRejPer.Text = IIf(IsDBNull(.Fields("REJ_PER").Value), "0.000", VB6.Format(.Fields("REJ_PER").Value, "0.000"))
                txtRejCost.Text = IIf(IsDBNull(.Fields("REJ_COST").Value), "0.000", .Fields("REJ_COST").Value)
                txtTotSaleCost.Text = IIf(IsDBNull(.Fields("TOT_SALE_COST").Value), "0.000", .Fields("TOT_SALE_COST").Value)
                txtProfitPer.Text = IIf(IsDBNull(.Fields("PROFIT_PER").Value), "0.000", VB6.Format(.Fields("PROFIT_PER").Value, "0.000"))
                txtProfitCost.Text = IIf(IsDBNull(.Fields("PROFIT_COST").Value), "0.000", .Fields("PROFIT_COST").Value)
                txtTransportCost.Text = IIf(IsDBNull(.Fields("TRANSPORT_COST").Value), "0.000", VB6.Format(.Fields("TRANSPORT_COST").Value, "0.000"))
                txtToolCost.Text = IIf(IsDBNull(.Fields("TOT_TOOL_COST").Value), "0.000", VB6.Format(.Fields("TOT_TOOL_COST").Value, "0.000"))

                txtToolQty.Text = IIf(IsDBNull(.Fields("TOOL_QTY").Value), "0.000", VB6.Format(.Fields("TOOL_QTY").Value, "0.000"))
                txtToolCostPerPc.Text = IIf(IsDBNull(.Fields("TOOL_COST_PER_PC").Value), "0.000", VB6.Format(.Fields("TOOL_COST_PER_PC").Value, "0.000"))
                txtCostReduction.Text = IIf(IsDBNull(.Fields("COST_REDUCTION").Value), "0.000", VB6.Format(.Fields("COST_REDUCTION").Value, "0.000"))


                txtICC.Text = IIf(IsDBNull(.Fields("TOT_INTEREST_COST").Value), "0.000", VB6.Format(.Fields("TOT_INTEREST_COST").Value, "0.000"))
                txtHandling.Text = IIf(IsDBNull(.Fields("TOT_HANDLING_COST").Value), "0.000", VB6.Format(.Fields("TOT_HANDLING_COST").Value, "0.000"))
                txtPMCost.Text = IIf(IsDBNull(.Fields("TOT_PACK_MAT_COST").Value), "0.000", VB6.Format(.Fields("TOT_PACK_MAT_COST").Value, "0.000"))

                CboPlatingType.Text = IIf(IsDBNull(.Fields("PLT_TYPE").Value), "", .Fields("PLT_TYPE").Value)
                cboPowderType.Text = IIf(IsDBNull(.Fields("POWDER_TYPE").Value), "", .Fields("POWDER_TYPE").Value)

                txtTotSalePrice.Text = IIf(IsDBNull(.Fields("TOT_SALE_PRICE").Value), "0.000", .Fields("TOT_SALE_PRICE").Value)
                txtTotPriceSettelled.Text = IIf(IsDBNull(.Fields("TOT_PRICE_SETTELED").Value), "0.000", VB6.Format(.Fields("TOT_PRICE_SETTELED").Value, "0.000"))
                txtDiscount.Text = IIf(IsDBNull(.Fields("DISCOUNT").Value), "0.000", .Fields("DISCOUNT").Value)
                txtCustPONo.Text = IIf(IsDBNull(.Fields("CUST_PO_NO").Value), "", .Fields("CUST_PO_NO").Value)
                txtCustPODate.Text = IIf(IsDBNull(.Fields("CUST_PO_DATE").Value), "", VB6.Format(.Fields("CUST_PO_DATE").Value, "DD/MM/YYYY"))

                Call CopyRMDetail(pCopyMkey)
                Call CopyBOPDetail(pCopyMkey)
                Call CopyOprDetail(pCopyMkey)
                Call CopyPltDetail(pRs)
                Call CopyPNTDetail(pCopyMkey)
                Call CopyPdrDetail(pRs)
                Call CopyPackDetail(pCopyMkey)
                Call AutoCalc()
            End If
        End With
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
        '    Resume			
    End Sub
    Private Sub ShowRMDetail()

        On Error GoTo ERR1
        Dim i As Integer
        Dim SqlStr As String

        SqlStr = ""
        SqlStr = " SELECT * FROM PRD_CUST_FG_COST_RM_DET " & vbCrLf & " WHERE Mkey='" & MainClass.AllowSingleQuote(lblMKey.Text) & "'" & " ORDER BY SubRowNo"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsFGCostRMDet, ADODB.LockTypeEnum.adLockReadOnly)

        With RsFGCostRMDet
            If Not .EOF Then
                If .EOF = True Then Exit Sub
                SprdRM.MaxRows = MainClass.GetMaxRecord("PRD_CUST_FG_COST_RM_DET", PubDBCn, " Mkey='" & MainClass.AllowSingleQuote(lblMKey.Text) & "'") + 1
                FormatSprdRM(-1)
                i = 0
                .MoveFirst()

                Do While Not .EOF
                    i = i + 1
                    SprdRM.Row = i

                    SprdRM.Col = ColMannualCalc
                    If .Fields("MANNUAL_CALC").Value = "Y" Then
                        SprdRM.Value = CStr(System.Windows.Forms.CheckState.Checked)
                    ElseIf .Fields("MANNUAL_CALC").Value = "N" Then
                        SprdRM.Value = CStr(System.Windows.Forms.CheckState.Unchecked)
                    End If

                    SprdRM.Col = ColProductDesc
                    SprdRM.Text = IIf(IsDBNull(.Fields("RM_REMARKS").Value), "", .Fields("RM_REMARKS").Value)

                    SprdRM.Col = ColRMDesc
                    SprdRM.Text = IIf(IsDBNull(.Fields("RM_DESC").Value), "", .Fields("RM_DESC").Value)

                    SprdRM.Col = ColRMUOM
                    SprdRM.Text = IIf(IsDBNull(.Fields("ISSUE_UOM").Value), "", .Fields("ISSUE_UOM").Value)

                    SprdRM.Col = ColRMRate
                    SprdRM.Text = VB6.Format(IIf(IsDBNull(.Fields("RATE_PCS").Value), 0, .Fields("RATE_PCS").Value), "0.00")

                    SprdRM.Col = ColRMThick
                    SprdRM.Text = VB6.Format(IIf(IsDBNull(.Fields("THICKNESS_RM").Value), 0, .Fields("THICKNESS_RM").Value), "0.000")

                    SprdRM.Col = ColRMLenth
                    SprdRM.Text = VB6.Format(IIf(IsDBNull(.Fields("LENGTH_RM").Value), 0, .Fields("LENGTH_RM").Value), "0.000")

                    SprdRM.Col = ColRMWidth
                    SprdRM.Text = VB6.Format(IIf(IsDBNull(.Fields("WIDTH_RM").Value), 0, .Fields("WIDTH_RM").Value), "0.000")

                    SprdRM.Col = ColRMDiaMeter
                    SprdRM.Text = VB6.Format(IIf(IsDBNull(.Fields("DIAMETER_RM").Value), 0, .Fields("DIAMETER_RM").Value), "0.000")

                    SprdRM.Col = ColWtPerStrip
                    SprdRM.Text = VB6.Format(IIf(IsDBNull(.Fields("WT_PER_STRIP").Value), 0, .Fields("WT_PER_STRIP").Value), "0.000")

                    SprdRM.Col = ColQtyPerStrip
                    SprdRM.Text = VB6.Format(IIf(IsDBNull(.Fields("QTY_PER_STRIP").Value), 0, .Fields("QTY_PER_STRIP").Value), "0.000")

                    SprdRM.Col = ColWtPerPc
                    SprdRM.Text = VB6.Format(IIf(IsDBNull(.Fields("GROSS_WT_PCS").Value), 0, .Fields("GROSS_WT_PCS").Value), "0.000")

                    SprdRM.Col = ColRMCost
                    SprdRM.Text = VB6.Format(IIf(IsDBNull(.Fields("COST_PCS").Value), 0, .Fields("COST_PCS").Value), "0.00")

                    SprdRM.Col = ColNetWt
                    SprdRM.Text = VB6.Format(IIf(IsDBNull(.Fields("NET_WT_PCS").Value), 0, .Fields("NET_WT_PCS").Value), "0.000")

                    SprdRM.Col = ColScrapWt
                    SprdRM.Text = VB6.Format(IIf(IsDBNull(.Fields("GROSS_WT_SCRAP").Value), 0, .Fields("GROSS_WT_SCRAP").Value), "0.000")

                    SprdRM.Col = ColScrapRate
                    SprdRM.Text = VB6.Format(IIf(IsDBNull(.Fields("RATE_SCRAP").Value), 0, .Fields("RATE_SCRAP").Value), "0.00")

                    SprdRM.Col = ColScrapCost
                    SprdRM.Text = VB6.Format(IIf(IsDBNull(.Fields("COST_SCRAP").Value), 0, .Fields("COST_SCRAP").Value), "0.00")

                    SprdRM.Col = ColNetRMCost
                    SprdRM.Text = VB6.Format(IIf(IsDBNull(.Fields("NET_COST_PCS").Value), 0, .Fields("NET_COST_PCS").Value), "0.00")

                    .MoveNext()
                Loop
            End If
        End With


        '    With RsFGCostRMDet			
        '        If Not .EOF Then			
        '            i = 1			
        '            .MoveFirst			
        '			
        '            Do While Not .EOF			
        '                SprdRM.Row = i			
        '			
        '                SprdRM.Col = ColRMNewItem			
        '                If !NEW_ITEM = "Y" Then			
        '                    SprdRM.Value = vbChecked			
        '                ElseIf !NEW_ITEM = "N" Then			
        '                    SprdRM.Value = vbUnchecked			
        '                End If			
        '			
        '                SprdRM.Col = ColRMItemCode			
        '                SprdRM.Text = IIf(IsNull(!ITEM_CODE), "", !ITEM_CODE)			
        '			
        '                SprdRM.Col = ColRMItemDesc			
        '                SprdRM.Text = IIf(IsNull(!ITEM_DESC), "", !ITEM_DESC)			
        '			
        '                SprdRM.Col = ColRMGrossWt			
        '                SprdRM.Text = Format(IIf(IsNull(!GROSS_WT), 0, !GROSS_WT), "0.000")			
        '			
        '                SprdRM.Col = ColRMItemRate			
        '                SprdRM.Text = Format(IIf(IsNull(!ITEM_RATE), 0, !ITEM_RATE), "0.000")			
        '			
        '                SprdRM.Col = ColRMItemAmount			
        '                SprdRM.Text = Format(IIf(IsNull(!ITEM_AMOUNT), 0, !ITEM_AMOUNT), "0.000")			
        '			
        '                SprdRM.Col = ColRMScrapWt			
        '                SprdRM.Text = Format(IIf(IsNull(!SCRAP_WT), 0, !SCRAP_WT), "0.000")			
        '			
        '                SprdRM.Col = ColRMScrapRate			
        '                SprdRM.Value = Format(IIf(IsNull(!SCRAP_RATE), 0, !SCRAP_RATE), "0.000")			
        '			
        '                SprdRM.Col = ColRMScrapAmount			
        '                SprdRM.Value = Format(IIf(IsNull(!SCRAP_AMOUNT), 0, !SCRAP_AMOUNT), "0.000")			
        '			
        '                SprdRM.Col = ColRMNetWt			
        '                SprdRM.Value = Format(IIf(IsNull(!NET_WT), 0, !NET_WT), "0.000")			
        '			
        '                SprdRM.Col = ColRMNetAmount			
        '                SprdRM.Value = Format(IIf(IsNull(!NET_AMOUNT), 0, !NET_AMOUNT), "0.000")			
        '			
        '                SprdRM.Col = ColRMFreight			
        '                SprdRM.Value = Format(IIf(IsNull(!FREIGHT), 0, !FREIGHT), "0.000")			
        '			
        '                SprdRM.Col = ColRMTotAmount			
        '                SprdRM.Value = Format(IIf(IsNull(!TOT_AMOUNT), 0, !TOT_AMOUNT), "0.000")			
        '			
        '                SprdRM.Col = ColRMRemarks			
        '                SprdRM.Text = IIf(IsNull(!REMARKS), "", !REMARKS)			
        '			
        '                .MoveNext			
        '                i = i + 1			
        '                SprdRM.MaxRows = i			
        '            Loop			
        '        End If			
        '    End With			
        FormatSprdRM(-1)

        Exit Sub
ERR1:
        '    Resume			
        MsgBox(Err.Description)
    End Sub

    Private Sub CopyRMDetail(ByRef pCopyMkey As String)
        On Error GoTo ERR1
        Dim i As Integer
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        '    SqlStr = ""			
        '    SqlStr = " SELECT * FROM PRD_CUST_FG_COST_RM_DET " & vbCrLf _			
        ''            & " WHERE Mkey='" & MainClass.AllowSingleQuote(pCopyMkey) & "'" _			
        ''            & " ORDER BY SubRowNo"			
        '			
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly			
        '			
        '    With RsTemp			
        '        If Not .EOF Then			
        '            i = 1			
        '            .MoveFirst			
        '			
        '            Do While Not .EOF			
        '                SprdRM.Row = i			
        '			
        '                SprdRM.Col = ColRMNewItem			
        '                If !NEW_ITEM = "Y" Then			
        '                    SprdRM.Value = vbChecked			
        '                ElseIf !NEW_ITEM = "N" Then			
        '                    SprdRM.Value = vbUnchecked			
        '                End If			
        '			
        '                SprdRM.Col = ColRMItemCode			
        '                SprdRM.Text = IIf(IsNull(!ITEM_CODE), "", !ITEM_CODE)			
        '			
        '                SprdRM.Col = ColRMItemDesc			
        '                SprdRM.Text = IIf(IsNull(!ITEM_DESC), "", !ITEM_DESC)			
        '			
        '                SprdRM.Col = ColRMGrossWt			
        '                SprdRM.Text = Format(IIf(IsNull(!GROSS_WT), 0, !GROSS_WT), "0.000")			
        '			
        '                SprdRM.Col = ColRMItemRate			
        '                SprdRM.Text = Format(IIf(IsNull(!ITEM_RATE), 0, !ITEM_RATE), "0.000")			
        '			
        '                SprdRM.Col = ColRMItemAmount			
        '                SprdRM.Text = Format(IIf(IsNull(!ITEM_AMOUNT), 0, !ITEM_AMOUNT), "0.000")			
        '			
        '                SprdRM.Col = ColRMScrapWt			
        '                SprdRM.Text = Format(IIf(IsNull(!SCRAP_WT), 0, !SCRAP_WT), "0.000")			
        '			
        '                SprdRM.Col = ColRMScrapRate			
        '                SprdRM.Value = Format(IIf(IsNull(!SCRAP_RATE), 0, !SCRAP_RATE), "0.000")			
        '			
        '                SprdRM.Col = ColRMScrapAmount			
        '                SprdRM.Value = Format(IIf(IsNull(!SCRAP_AMOUNT), 0, !SCRAP_AMOUNT), "0.000")			
        '			
        '                SprdRM.Col = ColRMNetWt			
        '                SprdRM.Value = Format(IIf(IsNull(!NET_WT), 0, !NET_WT), "0.000")			
        '			
        '                SprdRM.Col = ColRMNetAmount			
        '                SprdRM.Value = Format(IIf(IsNull(!NET_AMOUNT), 0, !NET_AMOUNT), "0.000")			
        '			
        '                SprdRM.Col = ColRMFreight			
        '                SprdRM.Value = Format(IIf(IsNull(!FREIGHT), 0, !FREIGHT), "0.000")			
        '			
        '                SprdRM.Col = ColRMTotAmount			
        '                SprdRM.Value = Format(IIf(IsNull(!TOT_AMOUNT), 0, !TOT_AMOUNT), "0.000")			
        '			
        '                SprdRM.Col = ColRMRemarks			
        '                SprdRM.Text = IIf(IsNull(!REMARKS), "", !REMARKS)			
        '			
        '                .MoveNext			
        '                i = i + 1			
        '                SprdRM.MaxRows = i			
        '            Loop			
        '        End If			
        '    End With			
        '    FormatSprdRM -1			

        Exit Sub
ERR1:
        '    Resume			
        MsgBox(Err.Description)
    End Sub

    Private Sub ShowBOPDetail()

        On Error GoTo ERR1
        Dim i As Integer
        Dim SqlStr As String
        Dim SqlStr1 As String
        Dim mItemCode As String
        Dim RsTemp As ADODB.Recordset
        Dim mBOPCode As String

        SqlStr = ""
        SqlStr = " SELECT * FROM PRD_CUST_FG_COST_BOP_DET " & vbCrLf & " WHERE Mkey='" & MainClass.AllowSingleQuote(lblMKey.Text) & "'" & " ORDER BY SubRowNo"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsFGCostBOPDet, ADODB.LockTypeEnum.adLockReadOnly)

        With RsFGCostBOPDet
            If Not .EOF Then
                i = 1
                .MoveFirst()

                Do While Not .EOF
                    SprdBOP.Row = i

                    SprdBOP.Col = ColBOPNewItem
                    If .Fields("NEW_ITEM").Value = "Y" Then
                        SprdBOP.Value = CStr(System.Windows.Forms.CheckState.Checked)
                    ElseIf .Fields("NEW_ITEM").Value = "N" Then
                        SprdBOP.Value = CStr(System.Windows.Forms.CheckState.Unchecked)
                    End If

                    SprdBOP.Col = ColBOPItemCode
                    SprdBOP.Text = IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                    mItemCode = IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)


                    Call FillGridRow(SprdBOP, i, ColBOPItemCode, mItemCode, ColBOPItemDesc, ColBOPItemUOM)
                    If mItemCode = "" Then
                        SprdBOP.Col = ColBOPItemDesc
                        SprdBOP.Text = IIf(IsDBNull(.Fields("ITEM_DESC").Value), "", .Fields("ITEM_DESC").Value)

                        SprdBOP.Col = ColBOPItemUOM
                        SprdBOP.Text = IIf(IsDBNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)
                    End If

                    SprdBOP.Col = ColBOPItemQty
                    SprdBOP.Text = VB6.Format(IIf(IsDBNull(.Fields("ITEM_QTY").Value), 0, .Fields("ITEM_QTY").Value), "0.000")

                    SprdBOP.Col = ColBOPItemRate
                    SprdBOP.Text = VB6.Format(IIf(IsDBNull(.Fields("ITEM_RATE").Value), 0, .Fields("ITEM_RATE").Value), "0.000")

                    SprdBOP.Col = ColBOPItemAmount
                    SprdBOP.Text = VB6.Format(IIf(IsDBNull(.Fields("ITEM_AMOUNT").Value), 0, .Fields("ITEM_AMOUNT").Value), "0.000")

                    SprdBOP.Col = ColBOPFreight
                    SprdBOP.Value = VB6.Format(IIf(IsDBNull(.Fields("FREIGHT").Value), 0, .Fields("FREIGHT").Value), "0.000")

                    SprdBOP.Col = ColBOPTotAmount
                    SprdBOP.Value = VB6.Format(IIf(IsDBNull(.Fields("TOT_AMOUNT").Value), 0, .Fields("TOT_AMOUNT").Value), "0.000")

                    '                SqlStr1 = " SELECT * FROM PRD_CUST_FG_COST_HDR " & vbCrLf _			
                    ''                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _			
                    ''                        & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' " & vbCrLf _			
                    ''                        & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtSuppCustCode.Text) & "' "			
                    '			
                    ''                        & vbCrLf _			
                    '''                        & " AND WEF=(SELECT MAX(WEF) FROM PRD_CUST_FG_COST_HDR " & vbCrLf _			
                    '''                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _			
                    '''                        & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "') "			
                    '			
                    '                MainClass.UOpenRecordSet SqlStr1, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly			
                    '			
                    '                SprdBOP.Col = ColBOPSubCosting			
                    '			
                    '                If Not RsTemp.EOF Then			
                    '                   SprdBOP.Value = vbChecked			
                    '                Else			
                    '                   SprdBOP.Value = vbUnchecked			
                    '                End If			

                    SprdBOP.Col = ColBOPRemarks
                    SprdBOP.Text = IIf(IsDBNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)

                    .MoveNext()
                    i = i + 1
                    SprdBOP.MaxRows = i
                Loop
            End If
        End With
        FormatSprdBOP(-1)

        Exit Sub
ERR1:
        '    Resume			
        MsgBox(Err.Description)
    End Sub

    Private Sub CopyBOPDetail(ByRef pCopymMkey As String)

        On Error GoTo ERR1
        Dim i As Integer
        Dim SqlStr As String
        Dim SqlStr1 As String
        Dim mItemCode As String
        Dim RsTemp As ADODB.Recordset
        Dim RsTemp1 As ADODB.Recordset

        SqlStr = ""
        SqlStr = " SELECT * FROM PRD_CUST_FG_COST_BOP_DET " & vbCrLf & " WHERE Mkey='" & MainClass.AllowSingleQuote(pCopymMkey) & "'" & " ORDER BY SubRowNo"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        With RsTemp
            If Not .EOF Then
                i = 1
                .MoveFirst()

                Do While Not .EOF
                    SprdBOP.Row = i

                    SprdBOP.Col = ColBOPNewItem
                    If .Fields("NEW_ITEM").Value = "Y" Then
                        SprdBOP.Value = CStr(System.Windows.Forms.CheckState.Checked)
                    ElseIf .Fields("NEW_ITEM").Value = "N" Then
                        SprdBOP.Value = CStr(System.Windows.Forms.CheckState.Unchecked)
                    End If

                    SprdBOP.Col = ColBOPItemCode
                    SprdBOP.Text = IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                    mItemCode = IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)

                    SprdBOP.Col = ColBOPItemDesc
                    SprdBOP.Text = IIf(IsDBNull(.Fields("ITEM_DESC").Value), "", .Fields("ITEM_DESC").Value)

                    SprdBOP.Col = ColBOPItemUOM
                    SprdBOP.Text = IIf(IsDBNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)

                    SprdBOP.Col = ColBOPItemQty
                    SprdBOP.Text = VB6.Format(IIf(IsDBNull(.Fields("ITEM_QTY").Value), 0, .Fields("ITEM_QTY").Value), "0.000")

                    SprdBOP.Col = ColBOPItemRate
                    SprdBOP.Text = VB6.Format(IIf(IsDBNull(.Fields("ITEM_RATE").Value), 0, .Fields("ITEM_RATE").Value), "0.000")

                    SprdBOP.Col = ColBOPItemAmount
                    SprdBOP.Text = VB6.Format(IIf(IsDBNull(.Fields("ITEM_AMOUNT").Value), 0, .Fields("ITEM_AMOUNT").Value), "0.000")

                    SprdBOP.Col = ColBOPFreight
                    SprdBOP.Value = VB6.Format(IIf(IsDBNull(.Fields("FREIGHT").Value), 0, .Fields("FREIGHT").Value), "0.000")

                    SprdBOP.Col = ColBOPTotAmount
                    SprdBOP.Value = VB6.Format(IIf(IsDBNull(.Fields("TOT_AMOUNT").Value), 0, .Fields("TOT_AMOUNT").Value), "0.000")

                    SqlStr1 = " SELECT * FROM PRD_CUST_FG_COST_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' " & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtSuppCustCode.Text) & "' "

                    '                        & vbCrLf _			
                    ''                        & " AND WEF=(SELECT MAX(WEF) FROM PRD_CUST_FG_COST_HDR " & vbCrLf _			
                    ''                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _			
                    ''                        & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "') "			

                    MainClass.UOpenRecordSet(SqlStr1, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp1, ADODB.LockTypeEnum.adLockReadOnly)

                    SprdBOP.Col = ColBOPSubCosting

                    If Not RsTemp1.EOF Then
                        SprdBOP.Value = CStr(System.Windows.Forms.CheckState.Checked)
                    Else
                        SprdBOP.Value = CStr(System.Windows.Forms.CheckState.Unchecked)
                    End If

                    SprdBOP.Col = ColBOPRemarks
                    SprdBOP.Text = IIf(IsDBNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)

                    .MoveNext()
                    i = i + 1
                    SprdBOP.MaxRows = i
                Loop
            End If
        End With
        FormatSprdBOP(-1)

        Exit Sub
ERR1:
        '    Resume			
        MsgBox(Err.Description)
    End Sub

    Private Sub ShowPNTDetail()

        On Error GoTo ERR1
        Dim i As Integer
        Dim SqlStr As String
        Dim SqlStr1 As String
        Dim mItemCode As String
        Dim RsTemp As ADODB.Recordset

        SqlStr = ""
        SqlStr = " SELECT * FROM PRD_CUST_FG_COST_PNT_DET " & vbCrLf & " WHERE Mkey='" & MainClass.AllowSingleQuote(lblMKey.Text) & "'" & " ORDER BY SubRowNo"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsFGCostBOPDet, ADODB.LockTypeEnum.adLockReadOnly)

        With RsFGCostBOPDet
            If Not .EOF Then
                i = 1
                .MoveFirst()

                Do While Not .EOF
                    SprdPnt.Row = i

                    SprdPnt.Col = ColPNTItemCode
                    SprdPnt.Text = IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                    mItemCode = IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)

                    SprdPnt.Col = ColPNTItemDesc
                    SprdPnt.Text = IIf(IsDBNull(.Fields("ITEM_DESC").Value), "", .Fields("ITEM_DESC").Value)

                    SprdPnt.Col = ColPNTItemUOM
                    SprdPnt.Text = IIf(IsDBNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)

                    SprdPnt.Col = ColPNTItemQty
                    SprdPnt.Text = VB6.Format(IIf(IsDBNull(.Fields("ITEM_QTY").Value), 0, .Fields("ITEM_QTY").Value), "0.000")

                    SprdPnt.Col = ColPNTItemRate
                    SprdPnt.Text = VB6.Format(IIf(IsDBNull(.Fields("ITEM_RATE").Value), 0, .Fields("ITEM_RATE").Value), "0.000")

                    SprdPnt.Col = ColPNTItemAmount
                    SprdPnt.Text = VB6.Format(IIf(IsDBNull(.Fields("ITEM_AMOUNT").Value), 0, .Fields("ITEM_AMOUNT").Value), "0.000")

                    SprdPnt.Col = ColPNTRemarks
                    SprdPnt.Text = IIf(IsDBNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)

                    .MoveNext()
                    i = i + 1
                    SprdPnt.MaxRows = i
                Loop
            End If
        End With
        FormatSprdPNT(-1)

        Exit Sub
ERR1:
        '    Resume			
        MsgBox(Err.Description)
    End Sub

    Private Sub CopyPNTDetail(ByRef pCopymMkey As String)

        On Error GoTo ERR1
        Dim i As Integer
        Dim SqlStr As String
        Dim SqlStr1 As String
        Dim mItemCode As String
        Dim RsTemp As ADODB.Recordset

        SqlStr = ""
        SqlStr = " SELECT * FROM PRD_CUST_FG_COST_PNT_DET " & vbCrLf & " WHERE Mkey='" & MainClass.AllowSingleQuote(pCopymMkey) & "'" & " ORDER BY SubRowNo"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        With RsTemp
            If Not .EOF Then
                i = 1
                .MoveFirst()

                Do While Not .EOF
                    SprdPnt.Row = i

                    SprdPnt.Col = ColPNTItemCode
                    SprdPnt.Text = IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                    mItemCode = IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)

                    SprdPnt.Col = ColPNTItemDesc
                    SprdPnt.Text = IIf(IsDBNull(.Fields("ITEM_DESC").Value), "", .Fields("ITEM_DESC").Value)

                    SprdPnt.Col = ColPNTItemUOM
                    SprdPnt.Text = IIf(IsDBNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)

                    SprdPnt.Col = ColPNTItemQty
                    SprdPnt.Text = VB6.Format(IIf(IsDBNull(.Fields("ITEM_QTY").Value), 0, .Fields("ITEM_QTY").Value), "0.000")

                    SprdPnt.Col = ColPNTItemRate
                    SprdPnt.Text = VB6.Format(IIf(IsDBNull(.Fields("ITEM_RATE").Value), 0, .Fields("ITEM_RATE").Value), "0.000")

                    SprdPnt.Col = ColPNTItemAmount
                    SprdPnt.Text = VB6.Format(IIf(IsDBNull(.Fields("ITEM_AMOUNT").Value), 0, .Fields("ITEM_AMOUNT").Value), "0.000")

                    SprdPnt.Col = ColPNTRemarks
                    SprdPnt.Text = IIf(IsDBNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)

                    .MoveNext()
                    i = i + 1
                    SprdPnt.MaxRows = i
                Loop
            End If
        End With
        FormatSprdPNT(-1)

        Exit Sub
ERR1:
        '    Resume			
        MsgBox(Err.Description)
    End Sub
    Private Sub ShowOprDetail()

        On Error GoTo ERR1
        Dim i As Integer
        Dim SqlStr As String
        Dim mItemCode As String
        Dim mType As Double

        SqlStr = ""
        SqlStr = " SELECT * FROM PRD_CUST_FG_COST_CONVER_DET " & vbCrLf & " WHERE Mkey='" & MainClass.AllowSingleQuote(lblMKey.Text) & "'" & " ORDER BY SubRowNo"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsFGCostOprDet, ADODB.LockTypeEnum.adLockReadOnly)

        With RsFGCostOprDet
            If Not .EOF Then
                i = 1
                .MoveFirst()

                Do While Not .EOF
                    SprdOpr.Row = i

                    SprdOpr.Col = ColOprOprCode
                    SprdOpr.Text = "" ''IIf(IsNull(!OPR_CODE), "", !OPR_CODE)			

                    '                SprdOpr.Col = ColOprOprDesc			
                    '                If MainClass.ValidateWithMasterTable(IIf(IsNull(!OPR_CODE), "", !OPR_CODE), "OPR_CODE", "OPR_DESC", "PRD_OPR_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then			
                    '                    SprdOpr.Text = MasterNo			
                    '                End If			

                    SprdOpr.Col = ColOprOprDesc
                    SprdOpr.Text = IIf(IsDBNull(.Fields("CONVER_DESC").Value), "", .Fields("CONVER_DESC").Value)

                    SprdOpr.Col = ColOprOprRate
                    SprdOpr.Text = VB6.Format(IIf(IsDBNull(.Fields("CONVER_RATE").Value), 0, .Fields("CONVER_RATE").Value), "0.000")

                    SprdOpr.Col = ColOprType
                    mType = Val(IIf(IsDBNull(.Fields("CONVER_TYPE").Value), "0", .Fields("CONVER_TYPE").Value))
                    SprdOpr.TypeComboBoxCurSel = mType - 1

                    SprdOpr.Col = ColOprRemarks
                    SprdOpr.Text = IIf(IsDBNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)

                    .MoveNext()
                    i = i + 1
                    SprdOpr.MaxRows = i
                Loop
            End If
        End With
        FormatSprdOPR(-1)

        Exit Sub
ERR1:
        '    Resume			
        MsgBox(Err.Description)
    End Sub

    Private Sub ShowWeldDetail()

        On Error GoTo ERR1
        Dim i As Integer
        Dim SqlStr As String
        Dim mItemCode As String
        Dim mType As Double

        SqlStr = ""
        SqlStr = " SELECT * FROM PRD_CUST_FG_COST_WELD_DET " & vbCrLf & " WHERE Mkey='" & MainClass.AllowSingleQuote(lblMKey.Text) & "'" & " ORDER BY SubRowNo"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsFGCostWeldDet, ADODB.LockTypeEnum.adLockReadOnly)

        With RsFGCostWeldDet
            If Not .EOF Then
                i = 1
                .MoveFirst()

                Do While Not .EOF
                    SprdWeld.Row = i

                    SprdWeld.Col = ColWeldDesc
                    SprdWeld.Text = IIf(IsDBNull(.Fields("WELD_DESC").Value), "", .Fields("WELD_DESC").Value)

                    SprdWeld.Col = ColWeldType
                    mType = Val(IIf(IsDBNull(.Fields("WELD_TYPE").Value), "0", .Fields("WELD_TYPE").Value))
                    SprdWeld.TypeComboBoxCurSel = mType - 1

                    SprdWeld.Col = ColWeldUOM
                    SprdWeld.Text = IIf(IsDBNull(.Fields("WELD_UOM").Value), "", .Fields("WELD_UOM").Value)

                    SprdWeld.Col = ColWeldQty
                    SprdWeld.Text = VB6.Format(IIf(IsDBNull(.Fields("WELD_QTY").Value), 0, .Fields("WELD_QTY").Value), "0.000")

                    SprdWeld.Col = ColWeldRate
                    SprdWeld.Text = VB6.Format(IIf(IsDBNull(.Fields("WELD_RATE").Value), 0, .Fields("WELD_RATE").Value), "0.000")

                    SprdWeld.Col = ColWeldAmount
                    SprdWeld.Text = VB6.Format(IIf(IsDBNull(.Fields("WELD_AMOUNT").Value), 0, .Fields("WELD_AMOUNT").Value), "0.000")

                    SprdWeld.Col = ColWeldRemarks
                    SprdWeld.Text = IIf(IsDBNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)

                    .MoveNext()
                    i = i + 1
                    SprdWeld.MaxRows = i
                Loop
            End If
        End With
        FormatSprdWeld(-1)

        Exit Sub
ERR1:
        '    Resume			
        MsgBox(Err.Description)
    End Sub

    Private Sub CopyOprDetail(ByRef pCopyMkey As String)

        On Error GoTo ERR1
        Dim i As Integer
        Dim SqlStr As String
        Dim mItemCode As String
        Dim RsTemp As ADODB.Recordset

        SqlStr = ""
        SqlStr = " SELECT * FROM PRD_CUST_FG_COST_CONVER_DET " & vbCrLf & " WHERE Mkey='" & MainClass.AllowSingleQuote(pCopyMkey) & "'" & " ORDER BY SubRowNo"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        With RsTemp
            If Not .EOF Then
                i = 1
                .MoveFirst()

                Do While Not .EOF
                    SprdOpr.Row = i

                    SprdOpr.Col = ColOprOprCode
                    SprdOpr.Text = "" ''IIf(IsNull(!OPR_CODE), "", !OPR_CODE)			

                    '                SprdOpr.Col = ColOprOprDesc			
                    '                If MainClass.ValidateWithMasterTable(IIf(IsNull(!OPR_CODE), "", !OPR_CODE), "OPR_CODE", "OPR_DESC", "PRD_OPR_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then			
                    '                    SprdOpr.Text = MasterNo			
                    '                End If			

                    SprdOpr.Col = ColOprOprDesc
                    SprdOpr.Text = IIf(IsDBNull(.Fields("CONVER_DESC").Value), "", .Fields("CONVER_DESC").Value)

                    SprdOpr.Col = ColOprOprRate
                    SprdOpr.Text = VB6.Format(IIf(IsDBNull(.Fields("CONVER_RATE").Value), 0, .Fields("CONVER_RATE").Value), "0.000")

                    SprdOpr.Col = ColOprRemarks
                    SprdOpr.Text = IIf(IsDBNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)

                    .MoveNext()
                    i = i + 1
                    SprdOpr.MaxRows = i
                Loop
            End If
        End With
        FormatSprdOPR(-1)

        Exit Sub
ERR1:
        '    Resume			
        MsgBox(Err.Description)
    End Sub
    Private Sub CopyWeldDetail(ByRef pCopyMkey As String)

        On Error GoTo ERR1
        Dim i As Integer
        Dim SqlStr As String
        Dim mType As String
        Dim RsTemp As ADODB.Recordset

        SqlStr = ""
        SqlStr = " SELECT * FROM PRD_CUST_FG_COST_WELD_DET " & vbCrLf & " WHERE Mkey='" & MainClass.AllowSingleQuote(pCopyMkey) & "'" & " ORDER BY SubRowNo"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


        With RsTemp
            If Not .EOF Then
                i = 1
                .MoveFirst()

                Do While Not .EOF
                    SprdWeld.Row = i

                    SprdWeld.Col = ColWeldDesc
                    SprdWeld.Text = IIf(IsDBNull(.Fields("WELD_DESC").Value), "", .Fields("WELD_DESC").Value)

                    SprdWeld.Col = ColWeldType
                    mType = CStr(Val(IIf(IsDBNull(.Fields("WELD_TYPE").Value), "0", .Fields("WELD_TYPE").Value)))
                    SprdWeld.TypeComboBoxCurSel = CDbl(mType) - 1

                    SprdWeld.Col = ColWeldUOM
                    SprdWeld.Text = IIf(IsDBNull(.Fields("WELD_UOM").Value), "", .Fields("WELD_UOM").Value)

                    SprdWeld.Col = ColWeldQty
                    SprdWeld.Text = VB6.Format(IIf(IsDBNull(.Fields("WELD_QTY").Value), 0, .Fields("WELD_QTY").Value), "0.000")

                    SprdWeld.Col = ColWeldRate
                    SprdWeld.Text = VB6.Format(IIf(IsDBNull(.Fields("WELD_RATE").Value), 0, .Fields("WELD_RATE").Value), "0.000")

                    SprdWeld.Col = ColWeldAmount
                    SprdWeld.Text = VB6.Format(IIf(IsDBNull(.Fields("WELD_AMOUNT").Value), 0, .Fields("WELD_AMOUNT").Value), "0.000")

                    SprdWeld.Col = ColWeldRemarks
                    SprdWeld.Text = IIf(IsDBNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)

                    .MoveNext()
                    i = i + 1
                    SprdWeld.MaxRows = i
                Loop
            End If
        End With
        FormatSprdWeld(-1)

        Exit Sub
ERR1:
        '    Resume			
        MsgBox(Err.Description)
    End Sub
    Private Sub ShowPltDetail()
        On Error GoTo ERR1
        With SprdPlt
            .Col = 4

            .Row = 1
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("GROSS_AREA").Value), 0, RsFGCostMain.Fields("GROSS_AREA").Value), "0.000")

            .Row = 2
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("NO_OF_SIDE").Value), 0, RsFGCostMain.Fields("NO_OF_SIDE").Value), "0.000")

            .Row = 3
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("TOT_GROSS_AREA").Value), 0, RsFGCostMain.Fields("TOT_GROSS_AREA").Value), "0.000")

            .Row = 4
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PLT_NET_AREA_PER").Value), 0, RsFGCostMain.Fields("PLT_NET_AREA_PER").Value), "0.000")

            .Row = 5
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PLT_NET_AREA").Value), 0, RsFGCostMain.Fields("PLT_NET_AREA").Value), "0.000")

            .Row = 6
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PLT_NI_CONS").Value), 0, RsFGCostMain.Fields("PLT_NI_CONS").Value), "0.000")

            .Row = 7
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PLT_NI_RATE").Value), 0, RsFGCostMain.Fields("PLT_NI_RATE").Value), "0.000")

            .Row = 8
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PLT_COST_NI_CONS").Value), 0, RsFGCostMain.Fields("PLT_COST_NI_CONS").Value), "0.000")

            .Row = 9
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PLT_CONV_COST").Value), 0, RsFGCostMain.Fields("PLT_CONV_COST").Value), "0.000")

            .Row = 10
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PLT_COST_NI_DM").Value), 0, RsFGCostMain.Fields("PLT_COST_NI_DM").Value), "0.000")

            .Row = 11
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PLT_COST_NI_PC").Value), 0, RsFGCostMain.Fields("PLT_COST_NI_PC").Value), "0.000")

            .Row = 12
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PLT_NET_CHEM_AREA_PER").Value), 0, RsFGCostMain.Fields("PLT_NET_CHEM_AREA_PER").Value), "0.000")

            .Row = 13
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PLT_NET_CHEM_AREA").Value), 0, RsFGCostMain.Fields("PLT_NET_CHEM_AREA").Value), "0.000")

            .Row = 14
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PLT_CHEM_CONS").Value), 0, RsFGCostMain.Fields("PLT_CHEM_CONS").Value), "0.000")

            .Row = 15
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PLT_CHEM_RATE").Value), 0, RsFGCostMain.Fields("PLT_CHEM_RATE").Value), "0.000")

            .Row = 16
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PLT_COST_CHEM_DM").Value), 0, RsFGCostMain.Fields("PLT_COST_CHEM_DM").Value), "0.000")

            .Row = 17
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PLT_COST_CHEM_PC").Value), 0, RsFGCostMain.Fields("PLT_COST_CHEM_PC").Value), "0.000")

            .Row = 18
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PLT_NET_BUFFING_AREA_PER").Value), 0, RsFGCostMain.Fields("PLT_NET_BUFFING_AREA_PER").Value), "0.000")

            .Row = 19
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PLT_NET_BUFFING_AREA").Value), 0, RsFGCostMain.Fields("PLT_NET_BUFFING_AREA").Value), "0.000")

            .Row = 20
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PLT_COST_BUFFING_DM").Value), 0, RsFGCostMain.Fields("PLT_COST_BUFFING_DM").Value), "0.000")

            .Row = 21
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PLT_COST_BUFFING_PC").Value), 0, RsFGCostMain.Fields("PLT_COST_BUFFING_PC").Value), "0.000")

            .Row = 22
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PLT_NET_CROME_AREA_PER").Value), 0, RsFGCostMain.Fields("PLT_NET_CROME_AREA_PER").Value), "0.000")

            .Row = 23
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PLT_NET_CROME_AREA").Value), 0, RsFGCostMain.Fields("PLT_NET_CROME_AREA").Value), "0.000")

            .Row = 24
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PLT_COST_CROME_DM").Value), 0, RsFGCostMain.Fields("PLT_COST_CROME_DM").Value), "0.000")

            .Row = 25
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PLT_COST_CROME_PC").Value), 0, RsFGCostMain.Fields("PLT_COST_CROME_PC").Value), "0.000")

            .Row = 26
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PLT_COST_HYDROGEN_PC").Value), 0, RsFGCostMain.Fields("PLT_COST_HYDROGEN_PC").Value), "0.000")

            .Row = 27
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PLT_TOT_COST").Value), 0, RsFGCostMain.Fields("PLT_TOT_COST").Value), "0.000")

            .Row = 28
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PLT_OVERHEAD_PER").Value), 0, RsFGCostMain.Fields("PLT_OVERHEAD_PER").Value), "0.000")

            .Row = 29
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PLT_OVERHEAD").Value), 0, RsFGCostMain.Fields("PLT_OVERHEAD").Value), "0.000")

            .Row = 30
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PLT_TOT_COST_PC").Value), 0, RsFGCostMain.Fields("PLT_TOT_COST_PC").Value), "0.000")

        End With
        Exit Sub
ERR1:
        '    Resume			
        MsgBox(Err.Description)
    End Sub

    Private Sub CopyPltDetail(ByRef pRsTemp As ADODB.Recordset)
        On Error GoTo ERR1
        With SprdPlt
            .Col = 4

            .Row = 1
            .Text = VB6.Format(IIf(IsDBNull(pRsTemp.Fields("GROSS_AREA").Value), 0, pRsTemp.Fields("GROSS_AREA").Value), "0.000")

            .Row = 2
            .Text = VB6.Format(IIf(IsDBNull(pRsTemp.Fields("NO_OF_SIDE").Value), 0, pRsTemp.Fields("NO_OF_SIDE").Value), "0.000")

            .Row = 3
            .Text = VB6.Format(IIf(IsDBNull(pRsTemp.Fields("TOT_GROSS_AREA").Value), 0, pRsTemp.Fields("TOT_GROSS_AREA").Value), "0.000")

            .Row = 4
            .Text = VB6.Format(IIf(IsDBNull(pRsTemp.Fields("PLT_NET_AREA_PER").Value), 0, pRsTemp.Fields("PLT_NET_AREA_PER").Value), "0.000")

            .Row = 5
            .Text = VB6.Format(IIf(IsDBNull(pRsTemp.Fields("PLT_NET_AREA").Value), 0, pRsTemp.Fields("PLT_NET_AREA").Value), "0.000")

            .Row = 6
            .Text = VB6.Format(IIf(IsDBNull(pRsTemp.Fields("PLT_NI_CONS").Value), 0, pRsTemp.Fields("PLT_NI_CONS").Value), "0.000")

            .Row = 7
            .Text = VB6.Format(IIf(IsDBNull(pRsTemp.Fields("PLT_NI_RATE").Value), 0, pRsTemp.Fields("PLT_NI_RATE").Value), "0.000")

            .Row = 8
            .Text = VB6.Format(IIf(IsDBNull(pRsTemp.Fields("PLT_COST_NI_CONS").Value), 0, pRsTemp.Fields("PLT_COST_NI_CONS").Value), "0.000")

            .Row = 9
            .Text = VB6.Format(IIf(IsDBNull(pRsTemp.Fields("PLT_CONV_COST").Value), 0, pRsTemp.Fields("PLT_CONV_COST").Value), "0.000")

            .Row = 10
            .Text = VB6.Format(IIf(IsDBNull(pRsTemp.Fields("PLT_COST_NI_DM").Value), 0, pRsTemp.Fields("PLT_COST_NI_DM").Value), "0.000")

            .Row = 11
            .Text = VB6.Format(IIf(IsDBNull(pRsTemp.Fields("PLT_COST_NI_PC").Value), 0, pRsTemp.Fields("PLT_COST_NI_PC").Value), "0.000")

            .Row = 12
            .Text = VB6.Format(IIf(IsDBNull(pRsTemp.Fields("PLT_NET_CHEM_AREA_PER").Value), 0, pRsTemp.Fields("PLT_NET_CHEM_AREA_PER").Value), "0.000")

            .Row = 13
            .Text = VB6.Format(IIf(IsDBNull(pRsTemp.Fields("PLT_NET_CHEM_AREA").Value), 0, pRsTemp.Fields("PLT_NET_CHEM_AREA").Value), "0.000")

            .Row = 14
            .Text = VB6.Format(IIf(IsDBNull(pRsTemp.Fields("PLT_CHEM_CONS").Value), 0, pRsTemp.Fields("PLT_CHEM_CONS").Value), "0.000")

            .Row = 15
            .Text = VB6.Format(IIf(IsDBNull(pRsTemp.Fields("PLT_CHEM_RATE").Value), 0, pRsTemp.Fields("PLT_CHEM_RATE").Value), "0.000")

            .Row = 16
            .Text = VB6.Format(IIf(IsDBNull(pRsTemp.Fields("PLT_COST_CHEM_DM").Value), 0, pRsTemp.Fields("PLT_COST_CHEM_DM").Value), "0.000")

            .Row = 17
            .Text = VB6.Format(IIf(IsDBNull(pRsTemp.Fields("PLT_COST_CHEM_PC").Value), 0, pRsTemp.Fields("PLT_COST_CHEM_PC").Value), "0.000")

            .Row = 18
            .Text = VB6.Format(IIf(IsDBNull(pRsTemp.Fields("PLT_NET_BUFFING_AREA_PER").Value), 0, pRsTemp.Fields("PLT_NET_BUFFING_AREA_PER").Value), "0.000")

            .Row = 19
            .Text = VB6.Format(IIf(IsDBNull(pRsTemp.Fields("PLT_NET_BUFFING_AREA").Value), 0, pRsTemp.Fields("PLT_NET_BUFFING_AREA").Value), "0.000")

            .Row = 20
            .Text = VB6.Format(IIf(IsDBNull(pRsTemp.Fields("PLT_COST_BUFFING_DM").Value), 0, pRsTemp.Fields("PLT_COST_BUFFING_DM").Value), "0.000")

            .Row = 21
            .Text = VB6.Format(IIf(IsDBNull(pRsTemp.Fields("PLT_COST_BUFFING_PC").Value), 0, pRsTemp.Fields("PLT_COST_BUFFING_PC").Value), "0.000")

            .Row = 22
            .Text = VB6.Format(IIf(IsDBNull(pRsTemp.Fields("PLT_NET_CROME_AREA_PER").Value), 0, pRsTemp.Fields("PLT_NET_CROME_AREA_PER").Value), "0.000")

            .Row = 23
            .Text = VB6.Format(IIf(IsDBNull(pRsTemp.Fields("PLT_NET_CROME_AREA").Value), 0, pRsTemp.Fields("PLT_NET_CROME_AREA").Value), "0.000")

            .Row = 24
            .Text = VB6.Format(IIf(IsDBNull(pRsTemp.Fields("PLT_COST_CROME_DM").Value), 0, pRsTemp.Fields("PLT_COST_CROME_DM").Value), "0.000")

            .Row = 25
            .Text = VB6.Format(IIf(IsDBNull(pRsTemp.Fields("PLT_COST_CROME_PC").Value), 0, pRsTemp.Fields("PLT_COST_CROME_PC").Value), "0.000")

            .Row = 26
            .Text = VB6.Format(IIf(IsDBNull(pRsTemp.Fields("PLT_COST_HYDROGEN_PC").Value), 0, pRsTemp.Fields("PLT_COST_HYDROGEN_PC").Value), "0.000")

            .Row = 27
            .Text = VB6.Format(IIf(IsDBNull(pRsTemp.Fields("PLT_TOT_COST").Value), 0, pRsTemp.Fields("PLT_TOT_COST").Value), "0.000")

            .Row = 28
            .Text = VB6.Format(IIf(IsDBNull(pRsTemp.Fields("PLT_OVERHEAD_PER").Value), 0, pRsTemp.Fields("PLT_OVERHEAD_PER").Value), "0.000")

            .Row = 29
            .Text = VB6.Format(IIf(IsDBNull(pRsTemp.Fields("PLT_OVERHEAD").Value), 0, pRsTemp.Fields("PLT_OVERHEAD").Value), "0.000")

            .Row = 30
            .Text = VB6.Format(IIf(IsDBNull(pRsTemp.Fields("PLT_TOT_COST_PC").Value), 0, pRsTemp.Fields("PLT_TOT_COST_PC").Value), "0.000")

        End With
        Exit Sub
ERR1:
        '    Resume			
        MsgBox(Err.Description)
    End Sub
    Private Sub ShowPntDetailOld()
        On Error GoTo ERR1
        With SprdPnt
            .Col = 4

            .Row = 1
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("GROSS_AREA").Value), 0, RsFGCostMain.Fields("GROSS_AREA").Value), "0.000")

            .Row = 2
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("NO_OF_SIDE").Value), 0, RsFGCostMain.Fields("NO_OF_SIDE").Value), "0.000")

            .Row = 3
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("TOT_GROSS_AREA").Value), 0, RsFGCostMain.Fields("TOT_GROSS_AREA").Value), "0.000")

            .Row = 4
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PNT_NET_AREA_PER").Value), 0, RsFGCostMain.Fields("PNT_NET_AREA_PER").Value), "0.000")

            .Row = 5
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PNT_NET_AREA").Value), 0, RsFGCostMain.Fields("PNT_NET_AREA").Value), "0.000")

            .Row = 6
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PNT_CHEM_CONS").Value), 0, RsFGCostMain.Fields("PNT_CHEM_CONS").Value), "0.000")

            .Row = 7
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PNT_CHEM_RATE").Value), 0, RsFGCostMain.Fields("PNT_CHEM_RATE").Value), "0.000")

            .Row = 8
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PNT_COST_CHEM_CONS").Value), 0, RsFGCostMain.Fields("PNT_COST_CHEM_CONS").Value), "0.000")

            .Row = 9
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PNT_CONV_COST").Value), 0, RsFGCostMain.Fields("PNT_CONV_COST").Value), "0.000")

            .Row = 10
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PNT_COST_DM").Value), 0, RsFGCostMain.Fields("PNT_COST_DM").Value), "0.000")

            .Row = 11
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PNT_COST_PC").Value), 0, RsFGCostMain.Fields("PNT_COST_PC").Value), "0.000")

            .Row = 12
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PNT_OVERHEAD_PER").Value), 0, RsFGCostMain.Fields("PNT_OVERHEAD_PER").Value), "0.000")

            .Row = 13
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PNT_OVERHEAD").Value), 0, RsFGCostMain.Fields("PNT_OVERHEAD").Value), "0.000")

            .Row = 14
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PNT_TOT_COST_PC").Value), 0, RsFGCostMain.Fields("PNT_TOT_COST_PC").Value), "0.000")

        End With
        Exit Sub
ERR1:
        '    Resume			
        MsgBox(Err.Description)
    End Sub

    Private Sub ShowPdrDetail()
        On Error GoTo ERR1
        With SprdPdr
            .Col = 4

            .Row = 1
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("GROSS_AREA").Value), 0, RsFGCostMain.Fields("GROSS_AREA").Value), "0.000")

            .Row = 2
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("NO_OF_SIDE").Value), 0, RsFGCostMain.Fields("NO_OF_SIDE").Value), "0.000")

            .Row = 3
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("TOT_GROSS_AREA").Value), 0, RsFGCostMain.Fields("TOT_GROSS_AREA").Value), "0.000")

            .Row = 4
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PDR_NET_AREA_PER").Value), 0, RsFGCostMain.Fields("PDR_NET_AREA_PER").Value), "0.000")

            .Row = 5
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PDR_NET_AREA").Value), 0, RsFGCostMain.Fields("PDR_NET_AREA").Value), "0.000")

            .Row = 6
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PDR_CHEM_CONS").Value), 0, RsFGCostMain.Fields("PDR_CHEM_CONS").Value), "0.000")

            .Row = 7
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PDR_CHEM_RATE").Value), 0, RsFGCostMain.Fields("PDR_CHEM_RATE").Value), "0.000")

            .Row = 8
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PDR_COST_CHEM_CONS").Value), 0, RsFGCostMain.Fields("PDR_COST_CHEM_CONS").Value), "0.000")

            .Row = 9
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PDR_CONV_COST").Value), 0, RsFGCostMain.Fields("PDR_CONV_COST").Value), "0.000")

            .Row = 10
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PDR_COST_DM").Value), 0, RsFGCostMain.Fields("PDR_COST_DM").Value), "0.000")

            .Row = 11
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PDR_COST_PC").Value), 0, RsFGCostMain.Fields("PDR_COST_PC").Value), "0.000")

            .Row = 12
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PDR_OVERHEAD_PER").Value), 0, RsFGCostMain.Fields("PDR_OVERHEAD_PER").Value), "0.000")

            .Row = 13
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PDR_OVERHEAD").Value), 0, RsFGCostMain.Fields("PDR_OVERHEAD").Value), "0.000")

            .Row = 14
            .Text = VB6.Format(IIf(IsDBNull(RsFGCostMain.Fields("PDR_TOT_COST_PC").Value), 0, RsFGCostMain.Fields("PDR_TOT_COST_PC").Value), "0.000")

        End With
        Exit Sub
ERR1:
        '    Resume			
        MsgBox(Err.Description)
    End Sub
    Private Sub CopyPdrDetail(ByRef pRsTemp As ADODB.Recordset)
        On Error GoTo ERR1
        With SprdPdr
            .Col = 4

            .Row = 1
            .Text = VB6.Format(IIf(IsDBNull(pRsTemp.Fields("GROSS_AREA").Value), 0, pRsTemp.Fields("GROSS_AREA").Value), "0.000")

            .Row = 2
            .Text = VB6.Format(IIf(IsDBNull(pRsTemp.Fields("NO_OF_SIDE").Value), 0, pRsTemp.Fields("NO_OF_SIDE").Value), "0.000")

            .Row = 3
            .Text = VB6.Format(IIf(IsDBNull(pRsTemp.Fields("TOT_GROSS_AREA").Value), 0, pRsTemp.Fields("TOT_GROSS_AREA").Value), "0.000")

            .Row = 4
            .Text = VB6.Format(IIf(IsDBNull(pRsTemp.Fields("PDR_NET_AREA_PER").Value), 0, pRsTemp.Fields("PDR_NET_AREA_PER").Value), "0.000")

            .Row = 5
            .Text = VB6.Format(IIf(IsDBNull(pRsTemp.Fields("PDR_NET_AREA").Value), 0, pRsTemp.Fields("PDR_NET_AREA").Value), "0.000")

            .Row = 6
            .Text = VB6.Format(IIf(IsDBNull(pRsTemp.Fields("PDR_CHEM_CONS").Value), 0, pRsTemp.Fields("PDR_CHEM_CONS").Value), "0.000")

            .Row = 7
            .Text = VB6.Format(IIf(IsDBNull(pRsTemp.Fields("PDR_CHEM_RATE").Value), 0, pRsTemp.Fields("PDR_CHEM_RATE").Value), "0.000")

            .Row = 8
            .Text = VB6.Format(IIf(IsDBNull(pRsTemp.Fields("PDR_COST_CHEM_CONS").Value), 0, pRsTemp.Fields("PDR_COST_CHEM_CONS").Value), "0.000")

            .Row = 9
            .Text = VB6.Format(IIf(IsDBNull(pRsTemp.Fields("PDR_CONV_COST").Value), 0, pRsTemp.Fields("PDR_CONV_COST").Value), "0.000")

            .Row = 10
            .Text = VB6.Format(IIf(IsDBNull(pRsTemp.Fields("PDR_COST_DM").Value), 0, pRsTemp.Fields("PDR_COST_DM").Value), "0.000")

            .Row = 11
            .Text = VB6.Format(IIf(IsDBNull(pRsTemp.Fields("PDR_COST_PC").Value), 0, pRsTemp.Fields("PDR_COST_PC").Value), "0.000")

            .Row = 12
            .Text = VB6.Format(IIf(IsDBNull(pRsTemp.Fields("PDR_OVERHEAD_PER").Value), 0, pRsTemp.Fields("PDR_OVERHEAD_PER").Value), "0.000")

            .Row = 13
            .Text = VB6.Format(IIf(IsDBNull(pRsTemp.Fields("PDR_OVERHEAD").Value), 0, pRsTemp.Fields("PDR_OVERHEAD").Value), "0.000")

            .Row = 14
            .Text = VB6.Format(IIf(IsDBNull(pRsTemp.Fields("PDR_TOT_COST_PC").Value), 0, pRsTemp.Fields("PDR_TOT_COST_PC").Value), "0.000")

        End With
        Exit Sub
ERR1:
        '    Resume			
        MsgBox(Err.Description)
    End Sub
    Private Sub ShowPackDetail()

        On Error GoTo ERR1
        Dim i As Integer
        Dim SqlStr As String
        Dim mItemCode As String
        Dim mType As Integer

        SqlStr = ""
        SqlStr = " SELECT * FROM PRD_CUST_FG_COST_PACK_DET " & vbCrLf & " WHERE Mkey='" & MainClass.AllowSingleQuote(lblMKey.Text) & "'" & " ORDER BY SubRowNo"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsFGCostPackDet, ADODB.LockTypeEnum.adLockReadOnly)

        With RsFGCostPackDet
            If Not .EOF Then
                i = 1
                .MoveFirst()

                Do While Not .EOF
                    SprdPack.Row = i

                    SprdPack.Col = ColPackPackDetail
                    SprdPack.Text = IIf(IsDBNull(.Fields("PACK_DETAIL").Value), "", .Fields("PACK_DETAIL").Value)

                    SprdPack.Col = ColPackPackRate
                    SprdPack.Text = VB6.Format(IIf(IsDBNull(.Fields("PACK_RATE").Value), 0, .Fields("PACK_RATE").Value), "0.000")

                    SprdPack.Col = ColPackType
                    mType = Val(IIf(IsDBNull(.Fields("PACK_TYPE").Value), "0", .Fields("PACK_TYPE").Value))
                    SprdPack.TypeComboBoxCurSel = mType - 1

                    SprdPack.Col = ColPackRemarks
                    SprdPack.Text = IIf(IsDBNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)

                    .MoveNext()
                    i = i + 1
                    SprdPack.MaxRows = i
                Loop
            End If
        End With

        FormatSprdPack(-1)
        Exit Sub
ERR1:
        '    Resume			
        MsgBox(Err.Description)
    End Sub

    Private Sub CopyPackDetail(ByRef pCopymMkey As String)

        On Error GoTo ERR1
        Dim i As Integer
        Dim SqlStr As String
        Dim mItemCode As String
        Dim RsTemp As ADODB.Recordset

        SqlStr = ""
        SqlStr = " SELECT * FROM PRD_CUST_FG_COST_PACK_DET " & vbCrLf & " WHERE Mkey='" & MainClass.AllowSingleQuote(pCopymMkey) & "'" & " ORDER BY SubRowNo"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        With RsTemp
            If Not .EOF Then
                i = 1
                .MoveFirst()

                Do While Not .EOF
                    SprdPack.Row = i

                    SprdPack.Col = ColPackPackDetail
                    SprdPack.Text = IIf(IsDBNull(.Fields("PACK_DETAIL").Value), "", .Fields("PACK_DETAIL").Value)

                    SprdPack.Col = ColPackPackRate
                    SprdPack.Text = VB6.Format(IIf(IsDBNull(.Fields("PACK_RATE").Value), 0, .Fields("PACK_RATE").Value), "0.000")

                    SprdPack.Col = ColPackRemarks
                    SprdPack.Text = IIf(IsDBNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)

                    .MoveNext()
                    i = i + 1
                    SprdPack.MaxRows = i
                Loop
            End If
        End With

        FormatSprdPack(-1)
        Exit Sub
ERR1:
        '    Resume			
        MsgBox(Err.Description)
    End Sub

    Private Function Update1() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim nMkey As String
        Dim mRowNo As Integer
        Dim mStatus As String

        Dim mGrossArea As Double
        Dim mNoOfSide As Double
        Dim mTotGrossArea As Double

        Dim mPltNetAreaPer As Double
        Dim mPltNetArea As Double
        Dim mPltNiCons As Double
        Dim mPltNiRate As Double
        Dim mPltCostNiCons As Double
        Dim mPltConvCost As Double
        Dim mPltCostNiDM As Double
        Dim mPltCostNiPc As Double
        Dim mPltNetChemAreaPer As Double
        Dim mPltNetChemArea As Double
        Dim mPltChemCons As Double
        Dim mPltChemRate As Double
        Dim mPltCostChemDM As Double
        Dim mPltCostChemPc As Double
        Dim mPltNetBuffingAreaPer As Double
        Dim mPltNetBuffingArea As Double
        Dim mPltCostBuffingDM As Double
        Dim mPltCostBuffingPc As Double
        Dim mPltNetCromeAreaPer As Double
        Dim mPltNetCromeArea As Double
        Dim mPltCostCromeDM As Double
        Dim mPltCostCromePc As Double
        Dim mPltCostHydrogenPc As Double
        Dim mPltTotCost As Double
        Dim mPltOverheadPer As Double
        Dim mPltOverhead As Double

        Dim mPntNetAreaPer As Double
        Dim mPntNetArea As Double
        Dim mPntChemCons As Double
        Dim mPntChemRate As Double
        Dim mPntCostChemCons As Double
        Dim mPntConvCost As Double
        Dim mPntCostDm As Double
        Dim mPntCostPc As Double
        Dim mPntOverheadPer As Double
        Dim mPntOverhead As Double

        Dim mPdrNetAreaPer As Double
        Dim mPdrNetArea As Double
        Dim mPdrChemCons As Double
        Dim mPdrChemRate As Double
        Dim mPdrCostChemCons As Double
        Dim mPdrConvCost As Double
        Dim mPdrCostDm As Double
        Dim mPdrCostPc As Double
        Dim mPdrOverheadPer As Double
        Dim mPdrOverhead As Double
        Dim mTotPackCost As Double
        Dim mPlatingType As String
        Dim mPowderType As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mTotPackCost = CDbl(VB6.Format(lblTotPackCost.Text, "0.000"))

        mPlatingType = Trim(CboPlatingType.Text)
        mPowderType = Trim(cboPowderType.Text)

        With SprdPlt
            .Col = 4

            .Row = 1
            mGrossArea = Val(.Text)
            .Row = 2
            mNoOfSide = Val(.Text)
            .Row = 3
            mTotGrossArea = Val(.Text)
            .Row = 4
            mPltNetAreaPer = Val(.Text)
            .Row = 5
            mPltNetArea = Val(.Text)
            .Row = 6
            mPltNiCons = Val(.Text)
            .Row = 7
            mPltNiRate = Val(.Text)
            .Row = 8
            mPltCostNiCons = Val(.Text)
            .Row = 9
            mPltConvCost = Val(.Text)
            .Row = 10
            mPltCostNiDM = Val(.Text)
            .Row = 11
            mPltCostNiPc = Val(.Text)
            .Row = 12
            mPltNetChemAreaPer = Val(.Text)
            .Row = 13
            mPltNetChemArea = Val(.Text)
            .Row = 14
            mPltChemCons = Val(.Text)
            .Row = 15
            mPltChemRate = Val(.Text)
            .Row = 16
            mPltCostChemDM = Val(.Text)
            .Row = 17
            mPltCostChemPc = Val(.Text)
            .Row = 18
            mPltNetBuffingAreaPer = Val(.Text)
            .Row = 19
            mPltNetBuffingArea = Val(.Text)
            .Row = 20
            mPltCostBuffingDM = Val(.Text)
            .Row = 21
            mPltCostBuffingPc = Val(.Text)
            .Row = 22
            mPltNetCromeAreaPer = Val(.Text)
            .Row = 23
            mPltNetCromeArea = Val(.Text)
            .Row = 24
            mPltCostCromeDM = Val(.Text)
            .Row = 25
            mPltCostCromePc = Val(.Text)
            .Row = 26
            mPltCostHydrogenPc = Val(.Text)
            .Row = 27
            mPltTotCost = Val(.Text)
            .Row = 28
            mPltOverheadPer = Val(.Text)
            .Row = 29
            mPltOverhead = Val(.Text)
        End With

        '    With SprdPnt			
        '        .Col = 4			
        '			
        '        .Row = 4			
        '        mPntNetAreaPer = Val(.Text)			
        '        .Row = 5			
        '        mPntNetArea = Val(.Text)			
        '        .Row = 6			
        '        mPntChemCons = Val(.Text)			
        '        .Row = 7			
        '        mPntChemRate = Val(.Text)			
        '        .Row = 8			
        '        mPntCostChemCons = Val(.Text)			
        '        .Row = 9			
        '        mPntConvCost = Val(.Text)			
        '        .Row = 10			
        '        mPntCostDm = Val(.Text)			
        '        .Row = 11			
        '        mPntCostPc = Val(.Text)			
        '        .Row = 12			
        '        mPntOverheadPer = Val(.Text)			
        '        .Row = 13			
        '        mPntOverhead = Val(.Text)			
        '    End With			

        With SprdPdr
            .Col = 4

            .Row = 4
            mPdrNetAreaPer = Val(.Text)
            .Row = 5
            mPdrNetArea = Val(.Text)
            .Row = 6
            mPdrChemCons = Val(.Text)
            .Row = 7
            mPdrChemRate = Val(.Text)
            .Row = 8
            mPdrCostChemCons = Val(.Text)
            .Row = 9
            mPdrConvCost = Val(.Text)
            .Row = 10
            mPdrCostDm = Val(.Text)
            .Row = 11
            mPdrCostPc = Val(.Text)
            .Row = 12
            mPdrOverheadPer = Val(.Text)
            .Row = 13
            mPdrOverhead = Val(.Text)
        End With

        mStatus = IIf(chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked, "O", "C")

        SqlStr = ""
        If ADDMode = True Then
            nMkey = RsCompany.Fields("COMPANY_CODE").Value & UCase(Trim(txtSuppCustCode.Text)) & UCase(Trim(txtProductCode.Text)) & VB6.Format(txtWEF.Text, "YYYYMMDD") & VB6.Format(txtAmendNo.Text, "000")

            lblMKey.Text = nMkey
            SqlStr = " INSERT INTO PRD_CUST_FG_COST_HDR ( " & vbCrLf _
                & " MKEY, COMPANY_CODE, PRODUCT_CODE, " & vbCrLf _
                & " SUPP_CUST_CODE, WEF, AMEND_NO, " & vbCrLf _
                & " ISSUE_UOM, BOM_TYPE, " & vbCrLf _
                & " TOT_RM_COST, TOT_BOP_COST, TOT_OPR_COST, " & vbCrLf _
                & " GROSS_AREA, NO_OF_SIDE, TOT_GROSS_AREA, "

            SqlStr = SqlStr & vbCrLf & " PLT_NET_AREA_PER, PLT_NET_AREA, PLT_NI_CONS, " & vbCrLf & " PLT_NI_RATE, PLT_COST_NI_CONS, PLT_CONV_COST, " & vbCrLf & " PLT_COST_NI_DM, PLT_COST_NI_PC, PLT_NET_CHEM_AREA_PER, " & vbCrLf & " PLT_NET_CHEM_AREA, PLT_CHEM_CONS, PLT_CHEM_RATE, " & vbCrLf & " PLT_COST_CHEM_DM, PLT_COST_CHEM_PC, PLT_NET_BUFFING_AREA_PER, " & vbCrLf & " PLT_NET_BUFFING_AREA, PLT_COST_BUFFING_DM, PLT_COST_BUFFING_PC, " & vbCrLf & " PLT_NET_CROME_AREA_PER, PLT_NET_CROME_AREA, PLT_COST_CROME_DM, " & vbCrLf & " PLT_COST_CROME_PC, PLT_COST_HYDROGEN_PC, PLT_TOT_COST, " & vbCrLf & " PLT_OVERHEAD_PER, PLT_OVERHEAD, PLT_TOT_COST_PC, " & vbCrLf & " PNT_NET_AREA_PER, " & vbCrLf & " PNT_NET_AREA, PNT_CHEM_CONS, PNT_CHEM_RATE, " & vbCrLf & " PNT_COST_CHEM_CONS, PNT_CONV_COST, PNT_COST_DM, " & vbCrLf & " PNT_COST_PC, PNT_OVERHEAD_PER, PNT_OVERHEAD, " & vbCrLf & " PNT_TOT_COST_PC, PDR_NET_AREA_PER, PDR_NET_AREA, " & vbCrLf & " PDR_CHEM_CONS, PDR_CHEM_RATE, PDR_COST_CHEM_CONS, " & vbCrLf & " PDR_CONV_COST, PDR_COST_DM, PDR_COST_PC, " & vbCrLf & " PDR_OVERHEAD_PER, PDR_OVERHEAD, PDR_TOT_COST_PC, "

            SqlStr = SqlStr & vbCrLf _
                & " TOT_VALUE_ADD, TOT_PROD_COST, OVERHEAD_PER, " & vbCrLf _
                & " OVERHEAD_COST, TOT_PACK_COST, REJ_PER, " & vbCrLf _
                & " REJ_COST, TOT_SALE_COST, PROFIT_PER, " & vbCrLf _
                & " PROFIT_COST, TRANSPORT_COST, TOT_SALE_PRICE, " & vbCrLf _
                & " TOT_PRICE_SETTELED, DISCOUNT, " & vbCrLf _
                & " CUST_PO_NO, CUST_PO_DATE, " & vbCrLf _
                & " STATUS, REMARKS, PREP_BY, APP_BY,  " & vbCrLf _
                & " TOT_WELD_COST , TOT_PROCESS_COST, TOT_HANDLING_COST, TOT_TOOL_COST, TOT_INTEREST_COST, TOT_PACK_MAT_COST," & vbCrLf _
                & " PLT_TYPE, POWDER_TYPE, ADDUSER, ADDDATE, MODUSER, MODDATE, TOOL_QTY, TOOL_COST_PER_PC, COST_REDUCTION " & vbCrLf _
                & " ) VALUES (  "




            SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(nMkey) & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", '" & MainClass.AllowSingleQuote(txtProductCode.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtSuppCustCode.Text) & "', TO_DATE('" & VB6.Format(txtWEF.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY'), " & Val(txtAmendNo.Text) & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtUnit.Text) & "', 'P', " & vbCrLf & " " & Val(txtTotRMCost.Text) & ", " & Val(txtTotBOPCost.Text) & ", " & Val(lblOperationCost.Text) & ", " & vbCrLf & " " & mGrossArea & ", " & mNoOfSide & ", " & mTotGrossArea & ", "

            SqlStr = SqlStr & vbCrLf & " " & mPltNetAreaPer & ", " & mPltNetArea & ", " & mPltNiCons & ", " & vbCrLf & " " & mPltNiRate & ", " & mPltCostNiCons & ", " & mPltConvCost & ", " & vbCrLf & " " & mPltCostNiDM & ", " & mPltCostNiPc & ", " & mPltNetChemAreaPer & ", " & vbCrLf & " " & mPltNetChemArea & ", " & mPltChemCons & ", " & mPltChemRate & ", " & vbCrLf & " " & mPltCostChemDM & ", " & mPltCostChemPc & ", " & mPltNetBuffingAreaPer & ", " & vbCrLf & " " & mPltNetBuffingArea & ", " & mPltCostBuffingDM & ", " & mPltCostBuffingPc & ", " & vbCrLf & " " & mPltNetCromeAreaPer & ", " & mPltNetCromeArea & ", " & mPltCostCromeDM & ", " & vbCrLf & " " & mPltCostCromePc & ", " & mPltCostHydrogenPc & ", " & mPltTotCost & ", " & vbCrLf & " " & mPltOverheadPer & ", " & mPltOverhead & ", " & Val(txtTotPltCost.Text) & ", " & vbCrLf & " " & mPntNetAreaPer & ", " & vbCrLf & " " & mPntNetArea & ", " & mPntChemCons & ", " & mPntChemRate & ", " & vbCrLf & " " & mPntCostChemCons & ", " & mPntConvCost & ", " & mPntCostDm & ", " & vbCrLf & " " & mPntCostPc & ", " & mPntOverheadPer & ", " & mPntOverhead & ", " & vbCrLf & " " & Val(txtTotPntCost.Text) & ", " & mPdrNetAreaPer & ", " & mPdrNetArea & ", " & vbCrLf & " " & mPdrChemCons & ", " & mPdrChemRate & ", " & mPntCostChemCons & ", " & vbCrLf & " " & mPdrConvCost & ", " & mPdrCostDm & ", " & mPdrCostPc & ", " & vbCrLf & " " & mPdrOverheadPer & ", " & mPdrOverhead & ", " & Val(txtTotPdrCost.Text) & ", "

            SqlStr = SqlStr & vbCrLf _
                & " " & Val(txtTotValueAdd.Text) & ", " & Val(txtTotProdCost.Text) & ", " & Val(txtOverheadPer.Text) & ", " & vbCrLf _
                & " " & Val(txtOverheadCost.Text) & ", " & Val(CStr(mTotPackCost)) & ", " & Val(txtRejPer.Text) & ", " & vbCrLf _
                & " " & Val(txtRejCost.Text) & ", " & Val(txtTotSaleCost.Text) & ", " & Val(txtProfitPer.Text) & ", " & vbCrLf _
                & " " & Val(txtProfitCost.Text) & ", " & Val(txtTransportCost.Text) & ", " & Val(txtTotSalePrice.Text) & ", " & vbCrLf _
                & " " & Val(txtTotPriceSettelled.Text) & ", " & Val(txtDiscount.Text) & ", " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtCustPONo.Text) & "', TO_DATE('" & VB6.Format(txtCustPODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & mStatus & "', '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtPrepBy.Text) & "', '" & MainClass.AllowSingleQuote(txtAppBy.Text) & "', " & vbCrLf _
                & " " & Val(txtTotWeldCost.Text) & " , " & Val(txtTotProcessCost.Text) & ", " & Val(lblHandlingCode.Text) & ", " & Val(lblToolCost.Text) & ", " & Val(lblInterest.Text) & ", " & Val(lblPackMaterialCost.Text) & "," & vbCrLf & " '" & mPlatingType & "', '" & mPowderType & "', '" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'',''," & vbCrLf _
                & " " & Val(txtToolQty.Text) & " ," & Val(txtToolCostPerPc.Text) & " ," & Val(txtCostReduction.Text) & " )"



        ElseIf MODIFYMode = True Then
            SqlStr = ""
            SqlStr = " UPDATE PRD_CUST_FG_COST_HDR  SET " & vbCrLf & " TOT_RM_COST = " & Val(txtTotRMCost.Text) & ",  " & vbCrLf & " TOT_BOP_COST = " & Val(txtTotBOPCost.Text) & ",  " & vbCrLf & " TOT_OPR_COST = " & Val(lblOperationCost.Text) & ",  " & vbCrLf & " GROSS_AREA = " & mGrossArea & ", " & vbCrLf & " NO_OF_SIDE = " & mNoOfSide & ", " & vbCrLf & " TOT_GROSS_AREA = " & mTotGrossArea & ", "

            SqlStr = SqlStr & vbCrLf & " PLT_NET_AREA_PER = " & mPltNetAreaPer & ", " & vbCrLf & " PLT_NET_AREA = " & mPltNetArea & ", " & vbCrLf & " PLT_NI_CONS = " & mPltNiCons & ", " & vbCrLf & " PLT_NI_RATE = " & mPltNiRate & ", " & vbCrLf & " PLT_COST_NI_CONS = " & mPltCostNiCons & ", " & vbCrLf & " PLT_CONV_COST = " & mPltConvCost & ", " & vbCrLf & " PLT_COST_NI_DM = " & mPltCostNiDM & ", " & vbCrLf & " PLT_COST_NI_PC = " & mPltCostNiPc & ", "

            SqlStr = SqlStr & vbCrLf & " PLT_NET_CHEM_AREA_PER = " & mPltNetChemAreaPer & ", " & vbCrLf & " PLT_NET_CHEM_AREA = " & mPltNetChemArea & ", " & vbCrLf & " PLT_CHEM_CONS = " & mPltChemCons & ", " & vbCrLf & " PLT_CHEM_RATE = " & mPltChemRate & ", " & vbCrLf & " PLT_COST_CHEM_DM = " & mPltCostChemDM & ", " & vbCrLf & " PLT_COST_CHEM_PC = " & mPltCostChemPc & ", " & vbCrLf & " PLT_NET_BUFFING_AREA_PER = " & mPltNetBuffingAreaPer & ", " & vbCrLf & " PLT_NET_BUFFING_AREA = " & mPltNetBuffingArea & ", " & vbCrLf & " PLT_COST_BUFFING_DM = " & mPltCostBuffingDM & ", " & vbCrLf & " PLT_COST_BUFFING_PC = " & mPltCostBuffingPc & ", " & vbCrLf & " PLT_NET_CROME_AREA_PER = " & mPltNetCromeAreaPer & ", " & vbCrLf & " PLT_NET_CROME_AREA = " & mPltNetCromeArea & ", " & vbCrLf & " PLT_COST_CROME_DM = " & mPltCostCromeDM & ", " & vbCrLf & " PLT_COST_CROME_PC = " & mPltCostCromePc & ", " & vbCrLf & " PLT_COST_HYDROGEN_PC = " & mPltCostHydrogenPc & ", " & vbCrLf & " PLT_TOT_COST = " & mPltTotCost & ", " & vbCrLf & " PLT_OVERHEAD_PER = " & mPltOverheadPer & ", " & vbCrLf & " PLT_OVERHEAD = " & mPltOverhead & ", " & vbCrLf & " PLT_TOT_COST_PC = " & Val(txtTotPltCost.Text) & ", "

            SqlStr = SqlStr & vbCrLf & " PNT_NET_AREA_PER = " & mPntNetAreaPer & ", " & vbCrLf & " PNT_NET_AREA = " & mPntNetArea & ", " & vbCrLf & " PNT_CHEM_CONS = " & mPntChemCons & ", " & vbCrLf & " PNT_CHEM_RATE = " & mPntChemRate & ", " & vbCrLf & " PNT_COST_CHEM_CONS = " & mPntCostChemCons & ", " & vbCrLf & " PNT_CONV_COST = " & mPntConvCost & ", " & vbCrLf & " PNT_COST_DM = " & mPntCostDm & ", " & vbCrLf & " PNT_COST_PC = " & mPntCostPc & ", " & vbCrLf & " PNT_OVERHEAD_PER = " & mPntOverheadPer & ", " & vbCrLf & " PNT_OVERHEAD = " & mPntOverhead & ", " & vbCrLf & " PNT_TOT_COST_PC = " & Val(txtTotPntCost.Text) & ", "

            SqlStr = SqlStr & vbCrLf & " PDR_NET_AREA_PER = " & mPdrNetAreaPer & ", " & vbCrLf & " PDR_NET_AREA = " & mPdrNetArea & ", " & vbCrLf & " PDR_CHEM_CONS = " & mPdrChemCons & ", " & vbCrLf & " PDR_CHEM_RATE = " & mPdrChemRate & ", " & vbCrLf & " PDR_COST_CHEM_CONS = " & mPdrCostChemCons & ", " & vbCrLf & " PDR_CONV_COST = " & mPdrConvCost & ", " & vbCrLf & " PDR_COST_DM = " & mPdrCostDm & ", " & vbCrLf & " PDR_COST_PC = " & mPdrCostPc & ", " & vbCrLf & " PDR_OVERHEAD_PER = " & mPdrOverheadPer & ", " & vbCrLf & " PDR_OVERHEAD = " & mPdrOverhead & ", " & vbCrLf & " PDR_TOT_COST_PC = " & Val(txtTotPdrCost.Text) & ", "

            SqlStr = SqlStr & vbCrLf & " TOT_VALUE_ADD = " & Val(txtTotValueAdd.Text) & ", " & vbCrLf & " TOT_PROD_COST = " & Val(txtTotProdCost.Text) & ", " & vbCrLf & " OVERHEAD_PER = " & Val(txtOverheadPer.Text) & ", " & vbCrLf & " OVERHEAD_COST = " & Val(txtOverheadCost.Text) & ", " & vbCrLf & " TOT_PACK_COST = " & Val(CStr(mTotPackCost)) & ", " & vbCrLf & " REJ_PER = " & Val(txtRejPer.Text) & ", " & vbCrLf & " REJ_COST = " & Val(txtRejCost.Text) & ", " & vbCrLf & " TOT_SALE_COST = " & Val(txtTotSaleCost.Text) & ", " & vbCrLf & " PROFIT_PER = " & Val(txtProfitPer.Text) & ", " & vbCrLf & " PROFIT_COST = " & Val(txtProfitCost.Text) & ", " & vbCrLf & " TRANSPORT_COST = " & Val(txtTransportCost.Text) & ", " & vbCrLf & " TOT_SALE_PRICE = " & Val(txtTotSalePrice.Text) & ", " & vbCrLf & " TOT_PRICE_SETTELED = " & Val(txtTotPriceSettelled.Text) & ", " & vbCrLf & " DISCOUNT = " & Val(txtDiscount.Text) & ", " & vbCrLf & " CUST_PO_NO = '" & MainClass.AllowSingleQuote(txtCustPONo.Text) & "', " & vbCrLf & " CUST_PO_DATE = TO_DATE('" & VB6.Format(txtCustPODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), "

            SqlStr = SqlStr & vbCrLf _
                & " STATUS = '" & mStatus & "', " & vbCrLf _
                & " TOT_WELD_COST = " & Val(txtTotWeldCost.Text) & " , " & vbCrLf _
                & " TOT_PROCESS_COST = " & Val(txtTotProcessCost.Text) & ", " & vbCrLf _
                & " TOT_HANDLING_COST = " & Val(lblHandlingCode.Text) & "," & vbCrLf _
                & " TOT_TOOL_COST = " & Val(lblToolCost.Text) & "," & vbCrLf _
                & " TOT_INTEREST_COST = " & Val(lblInterest.Text) & ", " & vbCrLf _
                & " TOT_PACK_MAT_COST = " & Val(lblPackMaterialCost.Text) & ", " & vbCrLf _
                & " REMARKS = '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "',  " & vbCrLf _
                & " PREP_BY = '" & MainClass.AllowSingleQuote(txtPrepBy.Text) & "',  " & vbCrLf _
                & " APP_BY = '" & MainClass.AllowSingleQuote(txtAppBy.Text) & "', " & vbCrLf _
                & " PLT_TYPE='" & mPlatingType & "', POWDER_TYPE='" & mPowderType & "', " & vbCrLf _
                & " TOOL_QTY = " & Val(txtToolQty.Text) & "," & vbCrLf _
                & " TOOL_COST_PER_PC = " & Val(txtToolCostPerPc.Text) & "," & vbCrLf _
                & " COST_REDUCTION = " & Val(txtCostReduction.Text) & "," & vbCrLf _
                & " ModUser = '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                & " ModDate = TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & " WHERE Mkey = '" & MainClass.AllowSingleQuote(lblMKey.Text) & "'"

        End If
        PubDBCn.Execute(SqlStr)

        If UpdateRMDetail() = False Then GoTo ErrPart
        If UpdateBOPDetail() = False Then GoTo ErrPart
        If UpdateWeldDetail() = False Then GoTo ErrPart
        If UpdateOprDetail() = False Then GoTo ErrPart
        If UpdatePNTDetail() = False Then GoTo ErrPart
        If UpdatePackDetail() = False Then GoTo ErrPart

        If Val(txtAmendNo.Text) > 0 Then
            If UpdatePreviousCost((txtProductCode.Text), Val(txtAmendNo.Text), "C") = False Then GoTo ErrPart
        End If

        Update1 = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsFGCostMain.Requery()
        RsFGCostRMDet.Requery()
        RsFGCostBOPDet.Requery()
        RsFGCostOprDet.Requery()
        RsFGCostWeldDet.Requery()
        RsFGCostPackDet.Requery()
        If Err.Description = "" Then Exit Function
        MsgBox(Err.Description)
        '    Resume			
    End Function

    Private Function UpdatePreviousCost(ByRef pItemCode As String, ByRef pAmendNo As Integer, ByRef pPreviousStatus As String) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String

        SqlStr = ""
        SqlStr = " UPDATE PRD_CUST_FG_COST_HDR SET " & vbCrLf & " STATUS = '" & pPreviousStatus & "', " & vbCrLf & " MODUSER = '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " MODDATE = TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AMEND_NO = " & pAmendNo - 1 & "" & vbCrLf & " AND SUPP_CUST_CODE ='" & MainClass.AllowSingleQuote(txtSuppCustCode.Text) & "'" & vbCrLf & " AND PRODUCT_CODE ='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        PubDBCn.Execute(SqlStr)

        UpdatePreviousCost = True

        Exit Function
ErrPart:
        UpdatePreviousCost = False
        If Err.Description = "" Then Exit Function
        MsgBox(Err.Description)
        '    Resume			
    End Function

    Private Function UpdateRMDetail() As Boolean

        On Error GoTo UpdateRMDetailErr
        Dim SqlStr As String
        'Dim i As Long			
        '			
        'Dim mNewItem As String			
        'Dim mItemCode As String			
        'Dim mItemDesc As String			
        'Dim mGrossWt As Double			
        'Dim mItemRate As Double			
        'Dim mItemAmount As Double			
        'Dim mScrapWt As Double			
        'Dim mScrapRate As Double			
        'Dim mScrapAmount As Double			
        'Dim mNetWt As Double			
        'Dim mNetAmount As Double			
        'Dim mFreight As Double			
        'Dim mTotAmount As Double			
        'Dim mRemarks As String			

        Dim i As Integer
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
        Dim mProductDesc As String


        PubDBCn.Execute(" DELETE FROM PRD_CUST_FG_COST_RM_DET " & vbCrLf & " WHERE MKEY='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")

        With SprdRM
            For i = 1 To .MaxRows - 1
                .Row = i

                .Col = ColMannualCalc
                If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                    mMannualCalc = "Y"
                ElseIf .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                    mMannualCalc = "N"
                End If

                .Col = ColProductDesc
                mProductDesc = MainClass.AllowSingleQuote(.Text)

                .Col = ColRMDesc
                mRMDesc = MainClass.AllowSingleQuote(.Text)

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

                    SqlStr = " INSERT INTO  PRD_CUST_FG_COST_RM_DET (" & vbCrLf & " MKEY, COMPANY_CODE, SUPP_CUST_CODE, PRODUCT_CODE, WEF, " & vbCrLf & " SUBROWNO,MANNUAL_CALC,RM_REMARKS, RM_DESC, ISSUE_UOM, " & vbCrLf & " RATE_PCS, THICKNESS_RM, LENGTH_RM, " & vbCrLf & " WIDTH_RM, DIAMETER_RM, WT_PER_STRIP, " & vbCrLf & " QTY_PER_STRIP, GROSS_WT_PCS, COST_PCS, " & vbCrLf & " NET_WT_PCS, GROSS_WT_SCRAP, RATE_SCRAP, " & vbCrLf & " COST_SCRAP, NET_COST_PCS ) VALUES ( "


                    SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(lblMKey.Text) & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", '" & MainClass.AllowSingleQuote(txtSuppCustCode.Text) & "','" & MainClass.AllowSingleQuote(txtProductCode.Text) & "', TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " " & i & ", '" & mMannualCalc & "','" & mProductDesc & "', '" & mRMDesc & "', '" & mRMUOM & "', " & vbCrLf & " " & mRMRate & ", " & mRMThick & ", " & mRMLenth & ", " & vbCrLf & " " & mRMWidth & ", " & mRMDiaMeter & ", " & mWtPerStrip & ", " & vbCrLf & " " & mQtyPerStrip & ", " & mWtPerPc & ", " & mRMCost & ", " & vbCrLf & " " & mNetWt & ", " & mScrapWt & ", " & mScrapRate & ", " & vbCrLf & " " & mScrapCost & ", " & mNetRMCost & ")"

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With

        '    With SprdRM			
        '        For i = 1 To .MaxRows - 1			
        '            .Row = i			
        '			
        '            .Col = ColRMNewItem			
        '            If .Value = vbChecked Then			
        '                mNewItem = "Y"			
        '            ElseIf .Value = vbUnchecked Then			
        '                mNewItem = "N"			
        '            End If			
        '			
        '            .Col = ColRMItemCode			
        '            mItemCode = MainClass.AllowSingleQuote(.Text)			
        '			
        '            .Col = ColRMItemDesc			
        '            mItemDesc = MainClass.AllowSingleQuote(.Text)			
        '			
        '            .Col = ColRMGrossWt			
        '            mGrossWt = Val(.Text)			
        '			
        '            .Col = ColRMItemRate			
        '            mItemRate = Val(.Text)			
        '			
        '            .Col = ColRMItemAmount			
        '            mItemAmount = Val(.Text)			
        '			
        '            .Col = ColRMScrapWt			
        '            mScrapWt = Val(.Text)			
        '			
        '            .Col = ColRMScrapRate			
        '            mScrapRate = Val(.Text)			
        '			
        '            .Col = ColRMScrapAmount			
        '            mScrapAmount = Val(.Text)			
        '			
        '            .Col = ColRMNetWt			
        '            mNetWt = Val(.Text)			
        '			
        '            .Col = ColRMNetAmount			
        '            mNetAmount = Val(.Text)			
        '			
        '            .Col = ColRMFreight			
        '            mFreight = Val(.Text)			
        '			
        '            .Col = ColRMTotAmount			
        '            mTotAmount = Val(.Text)			
        '			
        '            .Col = ColRMRemarks			
        '            mRemarks = MainClass.AllowSingleQuote(.Text)			
        '			
        '            SqlStr = ""			
        '            If Trim(mItemDesc) <> "" Then			
        '			
        '                SqlStr = " INSERT INTO  PRD_CUST_FG_COST_RM_DET ( " & vbCrLf _			
        ''                        & " MKEY, COMPANY_CODE, PRODUCT_CODE, " & vbCrLf _			
        ''                        & " SUPP_CUST_CODE, WEF, AMEND_NO, " & vbCrLf _			
        ''                        & " SUBROWNO, NEW_ITEM, ITEM_CODE, " & vbCrLf _			
        ''                        & " ITEM_DESC, GROSS_WT, ITEM_RATE, " & vbCrLf _			
        ''                        & " ITEM_AMOUNT, SCRAP_WT, SCRAP_RATE, " & vbCrLf _			
        ''                        & " SCRAP_AMOUNT, NET_WT, NET_AMOUNT, " & vbCrLf _			
        ''                        & " FREIGHT, TOT_AMOUNT, REMARKS " & vbCrLf _			
        ''                        & " ) VALUES ( "			
        '			
        '                SqlStr = SqlStr & vbCrLf _			
        ''                        & " '" & MainClass.AllowSingleQuote(lblMKey.text) & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", '" & MainClass.AllowSingleQuote(txtProductCode.Text) & "', " & vbCrLf _			
        ''                        & " '" & MainClass.AllowSingleQuote(txtSuppCustCode.Text) & "', '" & VB6.Format(txtWEF.Text, "DD/MMM/YYYY") & "', " & Val(txtAmendNo.Text) & ", " & vbCrLf _			
        ''                        & " " & i & ", '" & mNewItem & "', '" & mItemCode & "', " & vbCrLf _			
        ''                        & " '" & mItemDesc & "', " & mGrossWt & ", " & mItemRate & ", " & vbCrLf _			
        ''                        & " '" & mItemAmount & "', " & mScrapWt & ", " & mScrapRate & ", " & vbCrLf _			
        ''                        & " '" & mScrapAmount & "', " & mNetWt & ", " & mNetAmount & ", " & vbCrLf _			
        ''                        & " " & mFreight & ", " & mTotAmount & ", '" & mRemarks & "' )"			
        '			
        '                PubDBCn.Execute SqlStr			
        '            End If			
        '        Next			
        '    End With			
        UpdateRMDetail = True
        Exit Function
UpdateRMDetailErr:
        MsgBox(Err.Description)
        UpdateRMDetail = False
    End Function

    Private Function UpdateBOPDetail() As Boolean

        On Error GoTo UpdateBOPDetailErr
        Dim SqlStr As String
        Dim i As Integer

        Dim mNewItem As String
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mItemUOM As String
        Dim mItemQty As Double
        Dim mItemRate As Double
        Dim mItemAmount As Double
        Dim mFreight As Double
        Dim mTotAmount As Double
        Dim mRemarks As String

        PubDBCn.Execute(" DELETE FROM PRD_CUST_FG_COST_BOP_DET " & vbCrLf & " WHERE MKEY='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")

        With SprdBOP
            For i = 1 To .MaxRows - 1
                .Row = i

                .Col = ColBOPNewItem
                If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                    mNewItem = "Y"
                ElseIf .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                    mNewItem = "N"
                End If

                .Col = ColBOPItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColBOPItemDesc
                mItemDesc = MainClass.AllowSingleQuote(.Text)

                .Col = ColBOPItemUOM
                mItemUOM = MainClass.AllowSingleQuote(.Text)

                .Col = ColBOPItemQty
                mItemQty = Val(.Text)

                .Col = ColBOPItemRate
                mItemRate = Val(.Text)

                .Col = ColBOPItemAmount
                mItemAmount = Val(.Text)

                .Col = ColBOPFreight
                mFreight = Val(.Text)

                .Col = ColBOPTotAmount
                mTotAmount = Val(.Text)

                .Col = ColBOPRemarks
                mRemarks = MainClass.AllowSingleQuote(.Text)

                SqlStr = ""
                If Trim(mItemDesc) <> "" Then

                    SqlStr = " INSERT INTO  PRD_CUST_FG_COST_BOP_DET ( " & vbCrLf & " MKEY, COMPANY_CODE, PRODUCT_CODE, " & vbCrLf & " SUPP_CUST_CODE, WEF, AMEND_NO, " & vbCrLf & " SUBROWNO, NEW_ITEM, ITEM_CODE, " & vbCrLf & " ITEM_DESC, ITEM_UOM, ITEM_QTY, " & vbCrLf & " ITEM_RATE, ITEM_AMOUNT, FREIGHT, " & vbCrLf & " TOT_AMOUNT, REMARKS " & vbCrLf & " ) VALUES ( "

                    SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(lblMKey.Text) & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", '" & MainClass.AllowSingleQuote(txtProductCode.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtSuppCustCode.Text) & "', TO_DATE('" & VB6.Format(txtWEF.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY'), " & Val(txtAmendNo.Text) & ", " & vbCrLf & " " & i & ", '" & mNewItem & "', '" & mItemCode & "', " & vbCrLf & " '" & mItemDesc & "', '" & mItemUOM & "', " & mItemQty & ", " & vbCrLf & " '" & mItemRate & "', " & mItemAmount & ", " & mFreight & ", " & vbCrLf & " " & mTotAmount & ", '" & mRemarks & "' )"

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        UpdateBOPDetail = True
        Exit Function
UpdateBOPDetailErr:
        MsgBox(Err.Description)
        UpdateBOPDetail = False
        '    Resume			
    End Function
    Private Function UpdatePNTDetail() As Boolean

        On Error GoTo UpdateBOPDetailErr
        Dim SqlStr As String
        Dim i As Integer

        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mItemUOM As String
        Dim mItemQty As Double
        Dim mItemRate As Double
        Dim mItemAmount As Double
        Dim mRemarks As String

        PubDBCn.Execute(" DELETE FROM PRD_CUST_FG_COST_PNT_DET " & vbCrLf & " WHERE MKEY='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")

        With SprdPnt
            For i = 1 To .MaxRows - 1
                .Row = i


                .Col = ColPNTItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColPNTItemDesc
                mItemDesc = MainClass.AllowSingleQuote(.Text)

                .Col = ColPNTItemUOM
                mItemUOM = MainClass.AllowSingleQuote(.Text)

                .Col = ColPNTItemQty
                mItemQty = Val(.Text)

                .Col = ColPNTItemRate
                mItemRate = Val(.Text)

                .Col = ColPNTItemAmount
                mItemAmount = Val(.Text)

                .Col = ColPNTRemarks
                mRemarks = MainClass.AllowSingleQuote(.Text)

                SqlStr = ""
                If Trim(mItemDesc) <> "" Then

                    SqlStr = " INSERT INTO  PRD_CUST_FG_COST_PNT_DET ( " & vbCrLf & " MKEY, COMPANY_CODE, PRODUCT_CODE, " & vbCrLf & " SUPP_CUST_CODE, WEF, AMEND_NO, " & vbCrLf & " SUBROWNO, ITEM_CODE, " & vbCrLf & " ITEM_DESC, ITEM_UOM, ITEM_QTY, " & vbCrLf & " ITEM_RATE, ITEM_AMOUNT, " & vbCrLf & " REMARKS " & vbCrLf & " ) VALUES ( "

                    SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(lblMKey.Text) & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", '" & MainClass.AllowSingleQuote(txtProductCode.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtSuppCustCode.Text) & "', TO_DATE('" & VB6.Format(txtWEF.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY'), " & Val(txtAmendNo.Text) & ", " & vbCrLf & " " & i & ", '" & mItemCode & "', " & vbCrLf & " '" & mItemDesc & "', '" & mItemUOM & "', " & mItemQty & ", " & vbCrLf & " '" & mItemRate & "', " & mItemAmount & ", '" & mRemarks & "' )"

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        UpdatePNTDetail = True
        Exit Function
UpdateBOPDetailErr:
        MsgBox(Err.Description)
        UpdatePNTDetail = False
        '    Resume			
    End Function
    Private Function UpdateOprDetail() As Boolean

        On Error GoTo UpdateOprDetailErr
        Dim SqlStr As String
        Dim i As Integer

        Dim mOPRCode As String
        Dim mOprRate As Double
        Dim mRemarks As String
        Dim mType As String

        PubDBCn.Execute(" DELETE FROM PRD_CUST_FG_COST_CONVER_DET " & vbCrLf & " WHERE MKEY='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")

        With SprdOpr
            For i = 1 To .MaxRows - 1
                .Row = i

                .Col = ColOprOprDesc
                mOPRCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColOprOprRate
                mOprRate = Val(.Text)

                .Col = ColOprType
                mType = VB.Left(.Text, 1)

                .Col = ColOprRemarks
                mRemarks = MainClass.AllowSingleQuote(.Text)

                SqlStr = ""
                If Trim(mOPRCode) <> "" Then

                    SqlStr = " INSERT INTO  PRD_CUST_FG_COST_CONVER_DET (" & vbCrLf & " MKEY, COMPANY_CODE, PRODUCT_CODE, " & vbCrLf & " SUPP_CUST_CODE, WEF, AMEND_NO, " & vbCrLf & " SUBROWNO, CONVER_DESC, CONVER_RATE, CONVER_TYPE, " & vbCrLf & " REMARKS " & vbCrLf & " ) VALUES ( "

                    SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(lblMKey.Text) & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", '" & MainClass.AllowSingleQuote(txtProductCode.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtSuppCustCode.Text) & "', TO_DATE('" & VB6.Format(txtWEF.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY'), " & Val(txtAmendNo.Text) & ", " & vbCrLf & " " & i & ", '" & mOPRCode & "', " & mOprRate & ", " & vbCrLf & " '" & mType & "', '" & mRemarks & "' )"

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        UpdateOprDetail = True
        Exit Function
UpdateOprDetailErr:
        MsgBox(Err.Description)
        UpdateOprDetail = False
        '    Resume			
    End Function

    Private Function UpdateWeldDetail() As Boolean

        On Error GoTo UpdateOprDetailErr
        Dim SqlStr As String
        Dim i As Integer

        Dim mWeldDesc As String
        Dim mWeldType As String
        Dim mWeldUOM As String
        Dim mRemarks As String

        Dim mWeldQty As Double
        Dim mWeldRate As Double
        Dim mWeldAmount As Double

        PubDBCn.Execute(" DELETE FROM PRD_CUST_FG_COST_WELD_DET " & vbCrLf & " WHERE MKEY='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")


        With SprdWeld
            For i = 1 To .MaxRows - 1
                .Row = i

                .Col = ColWeldDesc
                mWeldDesc = MainClass.AllowSingleQuote(.Text)

                .Col = ColWeldType
                mWeldType = VB.Left(.Text, 1)

                .Col = ColWeldUOM
                mWeldUOM = MainClass.AllowSingleQuote(.Text)

                .Col = ColWeldQty
                mWeldQty = Val(.Text)

                .Col = ColWeldRate
                mWeldRate = Val(.Text)

                .Col = ColWeldAmount
                mWeldAmount = Val(.Text)

                .Col = ColWeldRemarks
                mRemarks = MainClass.AllowSingleQuote(.Text)

                SqlStr = ""
                If Trim(mWeldType) <> "" And mWeldAmount <> 0 Then

                    SqlStr = " INSERT INTO  PRD_CUST_FG_COST_WELD_DET (" & vbCrLf & " MKEY, COMPANY_CODE, PRODUCT_CODE, " & vbCrLf & " SUPP_CUST_CODE, WEF, AMEND_NO, " & vbCrLf & " SUBROWNO, WELD_DESC, WELD_TYPE, WELD_UOM, " & vbCrLf & " WELD_QTY, WELD_RATE, WELD_AMOUNT, REMARKS " & vbCrLf & " ) VALUES ( "


                    SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(lblMKey.Text) & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", '" & MainClass.AllowSingleQuote(txtProductCode.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtSuppCustCode.Text) & "', TO_DATE('" & VB6.Format(txtWEF.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY'), " & Val(txtAmendNo.Text) & ", " & vbCrLf & " " & i & ", '" & mWeldDesc & "', '" & mWeldType & "', '" & mWeldUOM & "'," & vbCrLf & " " & mWeldQty & ", " & mWeldRate & ", " & mWeldAmount & ",  '" & mRemarks & "' )"

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        UpdateWeldDetail = True
        Exit Function
UpdateOprDetailErr:
        MsgBox(Err.Description)
        UpdateWeldDetail = False
        '    Resume			
    End Function
    Private Function UpdatePackDetail() As Boolean

        On Error GoTo UpdatePackDetailErr
        Dim SqlStr As String
        Dim i As Integer
        Dim mPackDetail As String
        Dim mPackRate As Double
        Dim mRemarks As String
        Dim mType As String

        PubDBCn.Execute(" DELETE FROM PRD_CUST_FG_COST_PACK_DET " & vbCrLf & " WHERE MKEY='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")

        With SprdPack
            For i = 1 To .MaxRows - 1
                .Row = i

                .Col = ColPackPackDetail
                mPackDetail = MainClass.AllowSingleQuote(.Text)

                .Col = ColPackPackRate
                mPackRate = Val(.Text)

                .Col = ColPackType
                mType = VB.Left(.Text, 1)

                .Col = ColPackRemarks
                mRemarks = MainClass.AllowSingleQuote(.Text)

                SqlStr = ""
                If Trim(mPackDetail) <> "" Then

                    SqlStr = " INSERT INTO  PRD_CUST_FG_COST_PACK_DET (" & vbCrLf & " MKEY, COMPANY_CODE, PRODUCT_CODE, " & vbCrLf & " SUPP_CUST_CODE, WEF, AMEND_NO, " & vbCrLf & " SUBROWNO, PACK_DETAIL, PACK_RATE, PACK_TYPE, " & vbCrLf & " REMARKS " & vbCrLf & " ) VALUES ( "

                    SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(lblMKey.Text) & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", '" & MainClass.AllowSingleQuote(txtProductCode.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtSuppCustCode.Text) & "',TO_DATE('" & VB6.Format(txtWEF.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY'), " & Val(txtAmendNo.Text) & ", " & vbCrLf & " " & i & ", '" & mPackDetail & "', " & mPackRate & ", " & vbCrLf & " '" & mType & "', '" & mRemarks & "' )"

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        UpdatePackDetail = True
        Exit Function
UpdatePackDetailErr:
        MsgBox(Err.Description)
        UpdatePackDetail = False
    End Function

    Private Sub ViewGrid()

        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            FormatSprdView()
            SprdView.Refresh()

            SprdView.Focus()
            SprdView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            SprdView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsFGCostMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Function CheckQty(ByRef pSprd As Object, ByVal Col As Integer, ByVal Row As Integer) As Boolean

        On Error GoTo ERR1
        With pSprd
            .Row = Row
            .Col = Col
            If Val(.Text) <> 0 Then
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

    Private Sub SprdOpr_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdOpr.Change

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdOPR_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdOpr.ClickEvent

        Dim SqlStr As String
        Dim mOPRCode As String
        Dim mSqlStr As String

        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        '    If Row = 0 And Col = ColOprOprCode Then			
        '        With SprdOPR			
        '            .Row = .ActiveRow			
        '            .Col = ColOprOprCode			
        '            If MainClass.SearchGridMaster(.Text, "PRD_OPR_MST", "OPR_CODE", "OPR_DESC", "", "", SqlStr) = True Then			
        '                .Row = .ActiveRow			
        '                .Col = ColOprOprCode			
        '                mOPRCode = Trim(AcName)			
        '                .Text = Trim(AcName)			
        '			
        '                .Col = ColOprOprDesc			
        '                .Text = Trim(AcName1)			
        '            End If			
        '        End With			
        '    End If			
        '			
        '    If Row = 0 And Col = ColOprOprDesc Then			
        '        With SprdOPR			
        '            .Row = .ActiveRow			
        '            .Col = ColOprOprDesc			
        '            If MainClass.SearchGridMaster("", "PRD_OPR_MST", "OPR_DESC", "OPR_CODE", "", "", SqlStr) = True Then			
        '                .Row = .ActiveRow			
        '                .Col = ColOprOprCode			
        '                 mOPRCode = Trim(AcName1)			
        '                .Text = Trim(AcName1)			
        '			
        '                .Col = ColOprOprDesc			
        '                .Text = Trim(AcName)			
        '			
        '            End If			
        '        End With			
        '    End If			

        If eventArgs.col = 0 And eventArgs.row > 0 Then
            MainClass.DeleteSprdRow(SprdOpr, eventArgs.row, ColOprOprCode)
        End If

        Call AutoCalc()
    End Sub
    Private Sub SprdOPR_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdOpr.KeyUpEvent
        Dim mCol As Short
        mCol = SprdOpr.ActiveCol
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColOprOprCode Then SprdOPR_ClickEvent(SprdOpr, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColOprOprCode, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColOprOprDesc Then SprdOPR_ClickEvent(SprdOpr, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColOprOprDesc, 0))
    End Sub

    Private Sub SprdOPR_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdOpr.LeaveCell

        On Error GoTo ErrPart
        Dim xOPRCode As String
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset

        If eventArgs.newRow = -1 Then Exit Sub

        With SprdOpr
            .Row = .ActiveRow
            .Col = ColOprOprDesc
            xOPRCode = Trim(.Text)
            If xOPRCode = "" Then Exit Sub

            Select Case eventArgs.col
                Case ColOprOprCode
                    If MainClass.ValidateWithMasterTable(xOPRCode, "OPR_CODE", "OPR_DESC", "PRD_OPR_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        .Col = ColOprOprDesc
                        .Text = Trim(MasterNo)
                        Call CheckDuplicateOpr(xOPRCode)
                    Else
                        MsgInformation("Invalid Operation Code")
                        MainClass.SetFocusToCell(SprdOpr, .ActiveRow, ColOprOprDesc)
                    End If
                Case ColOprOprDesc
                    '                .Col = ColOprOprDesc			
                    '                .Text = Trim(MasterNo)			
                    Call CheckDuplicateOpr(xOPRCode)

                Case ColOprOprRate
                    If CheckQty(SprdOpr, eventArgs.col, eventArgs.row) = True Then
                        MainClass.AddBlankSprdRow(SprdOpr, ColOprOprDesc, ConRowHeight)
                        FormatSprdOPR(.MaxRows)
                    End If
            End Select
        End With

        Call AutoCalc()
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdPNT_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdPnt.Change

        With SprdPnt
            SprdPNT_LeaveCell(SprdPnt, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdPNT_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdPnt.ClickEvent

        Dim SqlStr As String
        Dim mDeleted As Boolean

        If eventArgs.row = 0 And eventArgs.col = ColPNTItemCode Then
            With SprdPnt
                .Row = .ActiveRow

                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value
                If MainClass.SearchGridMaster("", "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", , , SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColPNTItemCode
                    .Text = AcName

                    .Col = ColPNTItemDesc
                    .Text = AcName1
                End If
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColPNTItemDesc Then
            With SprdPnt
                .Row = .ActiveRow

                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value
                If MainClass.SearchGridMaster("", "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColPNTItemCode
                    .Text = AcName1

                    .Col = ColPNTItemDesc
                    .Text = AcName
                End If
            End With
        End If

        '    If Row = 0 And Col = ColPNTItemUOM Then			
        '        With SprdPnt			
        '            .Row = .ActiveRow			
        '            .Col = ColPNTNewItem			
        '            If .Value = vbUnchecked Then Exit Sub			
        '			
        '            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE = 'U' "			
        '            .Row = .ActiveRow			
        '            .Col = ColPNTItemUOM			
        '            If MainClass.SearchGridMaster(.Text, "INV_GENERAL_MST", "GEN_CODE", "GEN_DESC", , , SqlStr) = True Then			
        '                .Row = .ActiveRow			
        '			
        '                .Col = ColPNTItemUOM			
        '                .Text = AcName			
        '            End If			
        '        End With			
        '    End If			

        If eventArgs.col = 0 And eventArgs.row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdPnt, eventArgs.row, ColPNTItemCode, mDeleted)
        End If

        Call AutoCalc()
    End Sub

    Private Sub SprdPNT_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdPnt.KeyUpEvent
        Dim mCol As Short
        mCol = SprdPnt.ActiveCol
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColPNTItemCode Then SprdPNT_ClickEvent(SprdPnt, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColPNTItemCode, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColPNTItemDesc Then SprdPNT_ClickEvent(SprdPnt, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColPNTItemDesc, 0))
        '    If KeyCode = vbKeyF1 And mCol = ColPNTItemUOM Then SprdPNT_Click ColPNTItemUOM, 0			
    End Sub

    Private Sub SprdPNT_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdPnt.LeaveCell

        On Error GoTo ErrPart
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim i As Integer

        If eventArgs.newRow = -1 Then Exit Sub
        With SprdPnt
            .Row = .ActiveRow


            Select Case eventArgs.col
'            Case ColPNTNewItem			
'                Call LockSprdPNT			
                Case ColPNTItemCode
                    .Row = .ActiveRow
                    i = .Row
                    .Col = ColPNTItemCode
                    mItemCode = Trim(.Text)
                    If mItemCode = "" Then Exit Sub
                    If mItemCode = Trim(txtProductCode.Text) Then
                        MsgInformation("Item Code is same as Product Code")
                        MainClass.SetFocusToCell(SprdPnt, .ActiveRow, ColPNTItemCode)
                    Else
                        If CheckDuplicatePntItem(SprdPnt, ColPNTItemCode, mItemCode) = False Then

                            Call FillGridRow(SprdPnt, ColPNTItemCode, i, mItemCode, ColPNTItemDesc, ColPNTItemUOM)

                        Else
                            MainClass.SetFocusToCell(SprdPnt, .ActiveRow, ColPNTItemCode)
                            Exit Sub
                        End If
                    End If
                Case ColPNTItemDesc
                    .Row = .ActiveRow
                    .Col = ColPNTItemDesc
                    mItemDesc = Trim(.Text)
                    If mItemDesc = "" Then Exit Sub
                    If mItemDesc = Trim(txtProductDesc.Text) Then
                        MsgInformation("Item Desc is same as Product Desc")
                        MainClass.SetFocusToCell(SprdPnt, .ActiveRow, ColPNTItemDesc)
                    Else
                        If CheckDuplicatePntItem(SprdPnt, ColPNTItemDesc, mItemDesc) = False Then
                        Else
                            MainClass.SetFocusToCell(SprdPnt, .ActiveRow, ColPNTItemDesc)
                            Exit Sub
                        End If
                    End If
                Case ColPNTItemUOM
                    .Row = .ActiveRow
                    .Col = ColPNTItemUOM
                    If Trim(.Text) <> "" Then Call CheckUnit(SprdPnt, ColPNTItemUOM, .ActiveRow)
                Case ColPNTItemQty
                    If CheckQty(SprdPnt, eventArgs.col, eventArgs.row) = True Then
                        MainClass.AddBlankSprdRow(SprdPnt, ColPNTItemDesc, ConRowHeight)
                        FormatSprdPNT(.MaxRows)
                    End If
                Case ColPNTItemRate
                    If CheckQty(SprdPnt, eventArgs.col, eventArgs.row) = True Then
                    End If
            End Select
        End With

        Call AutoCalc()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub


    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick

        SprdView.Row = SprdView.ActiveRow

        SprdView.Col = 1
        txtProductCode.Text = Trim(SprdView.Text)

        SprdView.Col = 3
        txtWEF.Text = VB6.Format(SprdView.Text, "DD/MM/YYYY")

        SprdView.Col = 6
        txtSuppCustCode.Text = SprdView.Text

        txtProductCode_Validating(txtProductCode, New System.ComponentModel.CancelEventArgs(False))
        ViewGrid()
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Private Sub SprdRM_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdRM.Change

        With SprdRM
            SprdRM_LeaveCell(SprdRM, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdRM_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdRM.ClickEvent

        Dim SqlStr As String
        Dim mDeleted As Boolean

        Dim mRMName As String

        '    If Row = 0 And Col = ColRMItemCode Then			
        '        With SprdRM			
        '            .Row = .ActiveRow			
        '            .Col = ColRMNewItem			
        '            If .Value = vbChecked Then Exit Sub			
        '			
        '            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value			
        '            If MainClass.SearchGridMaster("", "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", , , SqlStr) = True Then			
        '                .Row = .ActiveRow			
        '			
        '                .Col = ColRMItemCode			
        '                .Text = AcName			
        '			
        '                .Col = ColRMItemDesc			
        '                .Text = AcName1			
        '            End If			
        '        End With			
        '    End If			
        '			
        '    If Row = 0 And Col = ColRMItemDesc Then			
        '        With SprdRM			
        '            .Row = .ActiveRow			
        '            .Col = ColRMNewItem			
        '            If .Value = vbChecked Then Exit Sub			
        '			
        '            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value			
        '            If MainClass.SearchGridMaster("", "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr) = True Then			
        '                .Row = .ActiveRow			
        '			
        '                .Col = ColRMItemCode			
        '                .Text = AcName1			
        '			
        '                .Col = ColRMItemDesc			
        '                .Text = AcName			
        '            End If			
        '        End With			
        '    End If			

        If eventArgs.row = 0 And eventArgs.col = ColRMDesc Then
            With SprdRM
                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  "

                .Row = .ActiveRow

                .Col = ColRMDesc
                mRMName = Trim(.Text)

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
                Call SprdRM_LeaveCell(SprdRM, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColRMDesc, SprdRM.ActiveRow, ColRMDesc, SprdRM.ActiveRow, False))
            End With
        End If

        If eventArgs.col = 0 And eventArgs.row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdRM, eventArgs.row, ColRMDesc, True)
            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
        Call AutoCalc()

        '    If Col = 0 And Row > 0 And (ADDMode = True Or MODIFYMode = True) Then			
        '        MainClass.DeleteSprdRow SprdRM, Row, ColRMItemCode, mDeleted			
        '    End If			
        '			
        '    Call AutoCalc			
    End Sub

    Private Sub SprdRM_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdRM.KeyUpEvent
        'Dim mCol As Integer			
        '    mCol = SprdRM.ActiveCol			
        '    If KeyCode = vbKeyF1 And mCol = ColRMItemCode Then SprdRM_Click ColRMItemCode, 0			
        '    If KeyCode = vbKeyF1 And mCol = ColRMItemDesc Then SprdRM_Click ColRMItemDesc, 0			
    End Sub

    Private Sub SprdRM_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdRM.LeaveCell

        On Error GoTo ErrPart
        Dim mRMDesc As String

        If eventArgs.newRow = -1 Then Exit Sub
        SprdRM.Row = eventArgs.row
        SprdRM.Col = ColRMDesc
        If Trim(SprdRM.Text) = "" Then Exit Sub

        Select Case eventArgs.col
            Case ColMannualCalc
                Call LockSprdRM()
            Case ColRMDesc
                SprdRM.Row = SprdRM.ActiveRow

                SprdRM.Col = ColRMDesc
                mRMDesc = Trim(SprdRM.Text)

                '            If Trim(txtItemCode.Text) = Trim(SprdRM.Text) Then			
                '                MainClass.setfocusToCell SprdRM, SprdRM.ActiveRow, ColRMCode			
                '            Else			
                If CheckDuplicateRM(mRMDesc, ColRMDesc, SprdRM) = False Then
                    SprdRM.Row = SprdRM.ActiveRow
                    SprdRM.Col = ColRMDesc
                    If FillGridRowRM(Trim(SprdRM.Text)) = False Then
                        MainClass.SetFocusToCell(SprdRM, SprdRM.ActiveRow, ColRMDesc)
                        Exit Sub
                    End If
                Else
                    MainClass.SetFocusToCell(SprdRM, SprdRM.ActiveRow, ColRMDesc)
                End If
'            End If			
            Case ColRMRate
                If CheckQty(SprdRM, eventArgs.col, eventArgs.row) = True Then
                    MainClass.AddBlankSprdRow(SprdRM, ColRMDesc, ConRowHeight)
                    FormatSprdRM((SprdRM.MaxRows))
                End If

                '        Case ColLengthRM			
                '            Call FillStripWidth			
                '        Case ColWidthRM			
                '            Call FillStripWidth			
                '        Case ColThicknessRM			
                '            Call FillStripWidth			
                '        Case ColMtrlCode			
                '            SprdRM.Row = SprdRM.ActiveRow			
                '            SprdRM.Col = ColMtrlCode			
                '            Call FillMtrlRow(SprdRM.Text)			
        End Select

        FormatSprdRM(-1)
        Call AutoCalc()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        'On Error GoTo ErrPart			
        'Dim mNewItem As Long			
        'Dim mItemCode As String			
        'Dim mItemDesc As String			
        'Dim i As Long			
        '    If NewRow = -1 Then Exit Sub			
        '			
        '    With SprdRM			
        '        .Row = .ActiveRow			
        '        .Col = ColRMNewItem			
        '        mNewItem = .Value			
        '			
        '        Select Case Col			
        '            Case ColRMNewItem			
        '                Call LockSprdRM			
        '            Case ColRMItemCode			
        '                Call LockSprdRM			
        '                .Row = .ActiveRow			
        '                i = .Row			
        '                .Col = ColRMItemCode			
        '                mItemCode = Trim(.Text)			
        '                If mItemCode = "" Then Exit Sub			
        '			
        '                If mItemCode = Trim(txtProductCode.Text) Then			
        '                    MsgInformation "Item Code is same as Product Code"			
        '                    MainClass.setfocusToCell SprdRM, .ActiveRow, ColRMItemCode			
        '                Else			
        '                    If CheckDuplicateItem(SprdRM, ColRMItemCode, mItemCode, ColRMNewItem, mNewItem) = False Then			
        '                        If mNewItem = vbUnchecked Then			
        '                            Call FillGridRow(SprdRM, i, ColRMItemCode, mItemCode, ColRMItemDesc)			
        '                        End If			
        '                    Else			
        '                        MainClass.setfocusToCell SprdRM, .ActiveRow, ColRMItemCode			
        '                        Exit Sub			
        '                    End If			
        '                End If			
        '            Case ColRMItemDesc			
        '                .Row = .ActiveRow			
        '                .Col = ColRMItemDesc			
        '                mItemDesc = Trim(.Text)			
        '                If mItemDesc = "" Then Exit Sub			
        '			
        '                If mItemDesc = Trim(txtProductDesc.Text) Then			
        '                    MsgInformation "Item Desc is same as Product Desc"			
        '                    MainClass.setfocusToCell SprdRM, .ActiveRow, ColRMItemDesc			
        '                Else			
        '                    If CheckDuplicateItem(SprdRM, ColRMItemDesc, mItemDesc, ColRMNewItem, mNewItem) = False Then			
        '                    Else			
        '                        MainClass.setfocusToCell SprdRM, .ActiveRow, ColRMItemDesc			
        '                        Exit Sub			
        '                    End If			
        '                End If			
        '            Case ColRMGrossWt			
        '                If CheckQty(SprdRM, Col, Row) = True Then			
        '                    MainClass.AddBlankSprdRow SprdRM, ColRMItemDesc, ConRowHeight			
        '                    FormatSprdRM .MaxRows			
        '                End If			
        '            Case ColRMItemRate			
        '                If CheckQty(SprdRM, Col, Row) = True Then			
        '                End If			
        '        End Select			
        '    End With			
        '			
        '    Call AutoCalc			
        '    Exit Sub			
        'ErrPart:			
        '    MsgBox err.Description			
    End Sub
    Private Function FillGridRowRM(ByRef mItemCode As String) As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim RsMisc As ADODB.Recordset
        Dim mSizeCode As Integer

        FillGridRowRM = False
        If Trim(mItemCode) = "" Then FillGridRowRM = True : Exit Function

        SqlStr = " SELECT " & vbCrLf _
        & " MTRL_CODE,MTRL_DESC " & vbCrLf _
        & " FROM " & vbCrLf _
        & " PRD_MTRL_MST " & vbCrLf _
        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        & " AND MTRL_DESC='" & MainClass.AllowSingleQuote(mItemCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsMisc.EOF Then
            SprdRM.Row = SprdRM.ActiveRow
            With RsMisc
                SprdRM.Col = ColRMDesc
                SprdRM.Text = Trim(IIf(IsDBNull(.Fields("MTRL_DESC").Value), "", .Fields("MTRL_DESC").Value))
            End With
            FillGridRowRM = True
        Else
            FillGridRowRM = False
        End If
        Exit Function
ERR1:
        MsgBox(Err.Description)
        FillGridRowRM = False
    End Function
    Private Function CheckDuplicateRM(ByRef pCheckCode As String, ByRef pCol As Integer, ByRef pSprd As AxFPSpreadADO.AxfpSpread) As Boolean
        On Error GoTo ERR1
        Dim i As Integer
        Dim mItemRept As Integer

        If Trim(pCheckCode) = "" Then CheckDuplicateRM = False : Exit Function

        CheckDuplicateRM = False
        Exit Function

        With pSprd
            For i = 1 To .MaxRows
                .Row = i
                .Col = pCol
                If UCase(Trim(.Text)) = UCase(Trim(pCheckCode)) Then
                    mItemRept = mItemRept + 1
                    If mItemRept > 1 Then
                        MsgInformation("Duplication Item.")
                        CheckDuplicateRM = True
                        Exit Function
                    End If
                End If
            Next
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function CheckDuplicateItem(ByRef pSprd As AxFPSpreadADO.AxfpSpread, ByRef pItemCol As Integer, ByRef pItem As String, ByRef pNewItemCol As Integer, ByRef pNewItem As Integer) As Boolean
        On Error GoTo ERR1
        Dim i As Integer
        Dim mItemRept As Integer

        If Trim(pItem) = "" Then CheckDuplicateItem = False : Exit Function
        With pSprd
            For i = 1 To .MaxRows
                .Row = i
                .Col = pItemCol
                If UCase(Trim(.Text)) = UCase(Trim(pItem)) Then
                    .Col = pNewItemCol
                    If CDbl(.Value) = pNewItem Then
                        mItemRept = mItemRept + 1
                        If mItemRept > 1 Then
                            MsgInformation("Duplication Item.")
                            CheckDuplicateItem = True
                            Exit Function
                        End If
                    End If
                End If
            Next
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function CheckDuplicatePntItem(ByRef pSprd As AxFPSpreadADO.AxfpSpread, ByRef pItemCol As Integer, ByRef pItem As String) As Boolean
        On Error GoTo ERR1
        Dim i As Integer
        Dim mItemRept As Integer

        If Trim(pItem) = "" Then CheckDuplicatePntItem = False : Exit Function
        With pSprd
            For i = 1 To .MaxRows
                .Row = i
                .Col = pItemCol
                If UCase(Trim(.Text)) = UCase(Trim(pItem)) Then
                    mItemRept = mItemRept + 1
                    If mItemRept > 1 Then
                        MsgInformation("Duplication Item.")
                        CheckDuplicatePntItem = True
                        Exit Function
                    End If
                End If
            Next
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub FillGridRow(ByRef pSprd As AxFPSpreadADO.AxfpSpread, ByRef pRow As Integer, ByRef pItemCodeCol As Integer, ByRef pItemCode As String, ByRef pItemDescCol As Integer, Optional ByRef pItemUOMCol As Integer = 0)

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim RsMisc As ADODB.Recordset

        If Trim(pItemCode) = "" Then Exit Sub
        SqlStr = " SELECT ITEM_CODE, ITEM_SHORT_DESC, ISSUE_UOM " & " FROM INV_ITEM_MST " & " WHERE ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "' " & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" '' AND ITEM_STATUS = 'A' "			

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If Not RsMisc.EOF Then
            pSprd.Row = pRow
            With RsMisc
                pSprd.Col = pItemDescCol
                pSprd.Text = IIf(IsDBNull(.Fields("ITEM_SHORT_DESC").Value), "", .Fields("ITEM_SHORT_DESC").Value)

                If pItemUOMCol <> 0 Then
                    pSprd.Col = pItemUOMCol
                    pSprd.Text = IIf(IsDBNull(.Fields("ISSUE_UOM").Value), "", .Fields("ISSUE_UOM").Value)
                End If
            End With
        Else
            MainClass.SetFocusToCell(pSprd, pRow, pItemCodeCol)
            '        MsgInformation "Invalid Item Code."			
        End If

        SqlStr = " SELECT RM_NETCOST, NET_COST, NET_COST - RM_NETCOST AS OPR_RATE " & vbCrLf & " FROM PRD_FG_SUB_COST_HDR " & vbCrLf & " WHERE ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "' " & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtSuppCustCode.Text) & "' " & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND WEF = (SELECT MAX(WEF) " & vbCrLf & " FROM PRD_FG_SUB_COST_HDR " & vbCrLf & " WHERE ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "' " & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtSuppCustCode.Text) & "' " & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND WEF <= TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')) " & vbCrLf
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If Not RsMisc.EOF Then
            pSprd.Row = pRow
            With RsMisc
                pSprd.Col = ColBOPItemRate
                pSprd.Text = VB6.Format(IIf(IsDBNull(.Fields("NET_COST").Value), "0.00", .Fields("NET_COST").Value), "0.00")

                pSprd.Col = ColBOPSubCosting
                pSprd.Value = CStr(System.Windows.Forms.CheckState.Checked)
            End With
        Else
            '        MainClass.setfocusToCell pSprd, pRow, pItemCodeCol			
            '        MsgInformation "Invalid Item Code."			
        End If
        LockSprdBOP()
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub

    Private Sub SprdBOP_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdBOP.Change

        With SprdBOP
            SprdBOP_LeaveCell(SprdBOP, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdBOP_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdBOP.ClickEvent

        Dim SqlStr As String
        Dim mDeleted As Boolean

        If eventArgs.row = 0 And eventArgs.col = ColBOPItemCode Then
            With SprdBOP
                .Row = .ActiveRow
                .Col = ColBOPNewItem
                If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then Exit Sub

                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value
                If MainClass.SearchGridMaster("", "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", , , SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColBOPItemCode
                    .Text = AcName

                    .Col = ColBOPItemDesc
                    .Text = AcName1
                End If
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColBOPItemDesc Then
            With SprdBOP
                .Row = .ActiveRow
                .Col = ColBOPNewItem
                If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then Exit Sub

                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value
                If MainClass.SearchGridMaster("", "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColBOPItemCode
                    .Text = AcName1

                    .Col = ColBOPItemDesc
                    .Text = AcName
                End If
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColBOPItemUOM Then
            With SprdBOP
                .Row = .ActiveRow
                .Col = ColBOPNewItem
                If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then Exit Sub

                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE = 'U' "
                .Row = .ActiveRow
                .Col = ColBOPItemUOM
                If MainClass.SearchGridMaster(.Text, "INV_GENERAL_MST", "GEN_CODE", "GEN_DESC", , , SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColBOPItemUOM
                    .Text = AcName
                End If
            End With
        End If

        If eventArgs.col = 0 And eventArgs.row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdBOP, eventArgs.row, ColBOPItemCode, mDeleted)
        End If

        Call AutoCalc()
    End Sub

    Private Sub SprdBOP_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdBOP.KeyUpEvent
        Dim mCol As Short
        mCol = SprdBOP.ActiveCol
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColBOPItemCode Then SprdBOP_ClickEvent(SprdBOP, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColBOPItemCode, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColBOPItemDesc Then SprdBOP_ClickEvent(SprdBOP, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColBOPItemDesc, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColBOPItemUOM Then SprdBOP_ClickEvent(SprdBOP, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColBOPItemUOM, 0))
    End Sub

    Private Sub SprdBOP_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdBOP.LeaveCell

        On Error GoTo ErrPart
        Dim mNewItem As Integer
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim i As Integer
        If eventArgs.newRow = -1 Then Exit Sub
        With SprdBOP
            .Row = .ActiveRow
            .Col = ColBOPNewItem
            mNewItem = CInt(.Value)

            Select Case eventArgs.col
                Case ColBOPNewItem
                    Call LockSprdBOP()
                Case ColBOPItemCode
                    .Row = .ActiveRow
                    i = .Row
                    .Col = ColBOPItemCode
                    mItemCode = Trim(.Text)
                    If Trim(mItemCode) <> "" Then
                        If Trim(txtSuppCustCode.Text) = "" Then
                            MsgInformation("Please Select Customer First.'")
                            Exit Sub
                        End If
                        If Trim(txtProductCode.Text) = "" Then
                            MsgInformation("Please Select Product Code First.'")
                            Exit Sub
                        End If
                    End If
                    If mItemCode = Trim(txtProductCode.Text) Then
                        MsgInformation("Item Code is same as Product Code")
                        MainClass.SetFocusToCell(SprdBOP, .ActiveRow, ColBOPItemCode)
                    Else
                        If CheckDuplicateItem(SprdBOP, ColBOPItemCode, mItemCode, ColBOPNewItem, mNewItem) = False Then
                            If mNewItem = System.Windows.Forms.CheckState.Unchecked Then
                                Call FillGridRow(SprdBOP, i, ColBOPItemCode, mItemCode, ColBOPItemDesc, ColBOPItemUOM)
                            End If
                        Else
                            MainClass.SetFocusToCell(SprdBOP, .ActiveRow, ColBOPItemCode)
                            Exit Sub
                        End If
                    End If
                Case ColBOPItemDesc
                    .Row = .ActiveRow
                    .Col = ColBOPItemDesc
                    mItemDesc = Trim(.Text)
                    If mItemDesc = Trim(txtProductDesc.Text) Then
                        MsgInformation("Item Desc is same as Product Desc")
                        MainClass.SetFocusToCell(SprdBOP, .ActiveRow, ColBOPItemDesc)
                    Else
                        If CheckDuplicateItem(SprdBOP, ColBOPItemDesc, mItemDesc, ColBOPNewItem, mNewItem) = False Then
                        Else
                            MainClass.SetFocusToCell(SprdBOP, .ActiveRow, ColBOPItemDesc)
                            Exit Sub
                        End If
                    End If
                Case ColBOPItemUOM
                    .Row = .ActiveRow
                    .Col = ColBOPItemUOM
                    If Trim(.Text) <> "" Then Call CheckUnit(SprdBOP, ColBOPItemUOM, .ActiveRow)
                Case ColBOPItemQty
                    If CheckQty(SprdBOP, eventArgs.col, eventArgs.row) = True Then
                        MainClass.AddBlankSprdRow(SprdBOP, ColBOPItemDesc, ConRowHeight)
                        FormatSprdBOP(.MaxRows)
                    End If
                Case ColBOPItemRate
                    If CheckQty(SprdBOP, eventArgs.col, eventArgs.row) = True Then
                    End If
            End Select
        End With

        Call AutoCalc()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub





    Private Function CheckDuplicateOpr(ByRef pOPRCode As String) As Boolean

        On Error GoTo ERR1
        Dim i As Integer
        Dim xOPRCode As String
        Dim mItemRept As Integer

        If pOPRCode = "" Then CheckDuplicateOpr = False : Exit Function
        With SprdOpr
            For i = 1 To .MaxRows
                .Row = i

                .Col = ColOprOprDesc
                xOPRCode = .Text

                If UCase(Trim(xOPRCode)) = UCase(Trim(pOPRCode)) Then
                    mItemRept = mItemRept + 1
                    If mItemRept > 1 Then
                        CheckDuplicateOpr = True
                        MsgInformation("Duplicate Operation.")
                        MainClass.SetFocusToCell(SprdOpr, .ActiveRow, .ActiveCol)
                        Exit Function
                    End If
                End If
            Next
        End With
        Exit Function
ERR1:
        CheckDuplicateOpr = False
        MsgInformation(Err.Description)
    End Function

    Private Function CheckDuplicateWeld(ByRef pDesc As String) As Boolean

        On Error GoTo ERR1
        Dim i As Integer
        Dim xDesc As String
        Dim mItemRept As Integer

        If pDesc = "" Then CheckDuplicateWeld = False : Exit Function
        With SprdWeld
            For i = 1 To .MaxRows
                .Row = i

                .Col = ColWeldDesc
                xDesc = .Text

                If UCase(Trim(xDesc)) = UCase(Trim(pDesc)) Then
                    mItemRept = mItemRept + 1
                    If mItemRept > 1 Then
                        CheckDuplicateWeld = True
                        MsgInformation("Duplicate Welding Description.")
                        MainClass.SetFocusToCell(SprdWeld, .ActiveRow, .ActiveCol)
                        Exit Function
                    End If
                End If
            Next
        End With
        Exit Function
ERR1:
        CheckDuplicateWeld = False
        MsgInformation(Err.Description)
    End Function
    Private Sub SprdPlt_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdPlt.Change

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdPlt_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdPlt.LeaveCell
        On Error GoTo ErrPart

        Call AutoCalc()

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdPntOld_Change(ByVal Col As Integer, ByVal Row As Integer)

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdPntOld_LeaveCell(ByVal Col As Integer, ByVal Row As Integer, ByVal NewCol As Integer, ByVal NewRow As Integer, ByRef Cancel As Boolean)
        On Error GoTo ErrPart

        Call AutoCalc()

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdPdr_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdPdr.Change

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdPdr_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdPdr.LeaveCell
        On Error GoTo ErrPart

        Call AutoCalc()

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdPack_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdPack.Change

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdPack_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdPack.ClickEvent

        Dim SqlStr As String

        If eventArgs.col = 0 And eventArgs.row > 0 Then
            MainClass.DeleteSprdRow(SprdPack, eventArgs.row, ColPackPackDetail)
        End If

        Call AutoCalc()
    End Sub

    Private Sub SprdPack_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdPack.LeaveCell

        On Error GoTo ErrPart
        Dim xPackDetail As String
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset

        If eventArgs.newRow = -1 Then Exit Sub

        With SprdPack
            .Row = .ActiveRow
            .Col = ColPackPackDetail
            xPackDetail = Trim(.Text)
            If xPackDetail = "" Then Exit Sub

            Select Case eventArgs.col
                Case ColPackPackRate
                    If CheckQty(SprdPack, eventArgs.col, eventArgs.row) = True Then
                        MainClass.AddBlankSprdRow(SprdPack, ColPackPackDetail, ConRowHeight)
                        FormatSprdPack(.MaxRows)
                    End If
            End Select
        End With

        Call AutoCalc()
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub AutoCalc()

        On Error GoTo AuERR
        Dim i As Integer

        Dim mTotWeldCost As Double
        Dim mTotProcessCost As Double
        Dim mTotHandlingCost As Double
        Dim mTotToolCost As Double
        Dim mTotInterestCost As Double
        Dim mTotPackMaterialCost As Double

        'Dim mRMGrossWt  As Double			
        'Dim mRMItemRate As Double			
        'Dim mRMItemAmount As Double			
        'Dim mRMScrapWt As Double			
        'Dim mRMScrapRate As Double			
        'Dim mRMScrapAmount As Double			
        'Dim mRMNetWt As Double			
        'Dim mRMNetAmount As Double			
        'Dim mRMFreight As Double			
        'Dim mRMTotAmount As Double			
        'Dim mTotRMCost As Double			

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



        Dim mBOPItemQty As Double
        Dim mBOPItemRate As Double
        Dim mBOPItemAmount As Double
        Dim mBOPFreight As Double
        Dim mBOPTotAmount As Double
        Dim mTotBOPCost As Double

        Dim mWeldQty As Double
        Dim mWeldRate As Double

        Dim mOprOprRate As Double
        Dim mTotOprCost As Double

        Dim mGrossArea As Double
        Dim mNoOfSide As Double
        Dim mTotGrossArea As Double

        Dim mPltNetAreaPer As Double
        Dim mPltNetArea As Double
        Dim mPltNiCons As Double
        Dim mPltNiRate As Double
        Dim mPltCostNiCons As Double
        Dim mPltConvCost As Double
        Dim mPltCostNiDM As Double
        Dim mPltCostNiPc As Double
        Dim mPltNetChemAreaPer As Double
        Dim mPltNetChemArea As Double
        Dim mPltChemCons As Double
        Dim mPltChemRate As Double
        Dim mPltCostChemDM As Double
        Dim mPltCostChemPc As Double
        Dim mPltNetBuffingAreaPer As Double
        Dim mPltNetBuffingArea As Double
        Dim mPltCostBuffingDM As Double
        Dim mPltCostBuffingPc As Double
        Dim mPltNetCromeAreaPer As Double
        Dim mPltNetCromeArea As Double
        Dim mPltCostCromeDM As Double
        Dim mPltCostCromePc As Double
        Dim mPltCostHydrogenPc As Double
        Dim mPltTotCost As Double
        Dim mPltOverheadPer As Double
        Dim mPltOverhead As Double
        Dim mTotPltCost As Double

        Dim mPntNetAreaPer As Double
        Dim mPntNetArea As Double
        Dim mPntChemCons As Double
        Dim mPntChemRate As Double
        Dim mPntCostChemCons As Double
        Dim mPntConvCost As Double
        Dim mPntCostDm As Double
        Dim mPntCostPc As Double
        Dim mPntOverheadPer As Double
        Dim mPntOverhead As Double
        Dim mTotPNTCost As Double

        Dim mPdrNetAreaPer As Double
        Dim mPdrNetArea As Double
        Dim mPdrChemCons As Double
        Dim mPdrChemRate As Double
        Dim mPdrCostChemCons As Double
        Dim mPdrConvCost As Double
        Dim mPdrCostDm As Double
        Dim mPdrCostPc As Double
        Dim mPdrOverheadPer As Double
        Dim mPdrOverhead As Double
        Dim mTotPdrCost As Double

        Dim mPackPackRate As Double
        Dim mTotPackCost As Double

        Dim mTotValueAdd As Double
        Dim mTotProdCost As Double
        Dim mOverheadPer As Double
        Dim mOverheadCost As Double
        Dim mRejPer As Double
        Dim mRejCost As Double
        Dim mTotSaleCost As Double
        Dim mProfitPer As Double
        Dim mProfitCost As Double
        Dim mTransportCost As Double
        Dim mTotSalePrice As Double
        Dim mTotPriceSettelled As Double
        Dim mDiscount As Double

        'Dim mTotPNTCost As Double			
        Dim mPNTItemAmount As Double
        Dim mPNTItemRate As Double
        Dim mPNTItemQty As Double
        Dim mToolCost As Double

        Dim mMannualCalc As String
        Dim mRMTypeDesc As String

        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mDensity As Double
        Dim mRMType As String

        Dim mTotalGrossWt As Double
        Dim mTotalScrapWt As Double
        Dim mTotalNetWt As Double

        Dim mTotalGrossCost As Double
        Dim mTotalScrapCost As Double
        Dim mTotalNetCost As Double
        Dim mTotalPartCost As Double
        Dim mTotalProcessACost As Double
        Dim mTotalProcessBCost As Double
        Dim mTotalNetBOPCost As Double


        mTotWeldCost = 0
        mTotProcessCost = 0
        mTotHandlingCost = 0
        mTotToolCost = 0
        mTotInterestCost = 0
        mTotPackMaterialCost = 0

        With SprdRM
            For i = 1 To .MaxRows
                .Row = i

                .Col = ColRMDesc
                If Trim(.Text) = "" Then GoTo NextLoop
                mRMTypeDesc = Trim(.Text)

                SqlStr = "SELECT *  FROM PRD_MTRL_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND MTRL_DESC='" & MainClass.AllowSingleQuote(mRMTypeDesc) & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)

                mDensity = 7.86
                mRMType = ""
                If RsTemp.EOF = False Then
                    mDensity = IIf(IsDBNull(RsTemp.Fields("MTRL_DENSITY").Value), 0, RsTemp.Fields("MTRL_DENSITY").Value)
                    mRMType = Trim(IIf(IsDBNull(RsTemp.Fields("MTRL_TYPE").Value), 0, RsTemp.Fields("MTRL_TYPE").Value))
                End If

                .Col = ColMannualCalc
                mMannualCalc = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

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
                    If mRMType = "SHEET" Then
                        mWtPerStrip = CDbl(VB6.Format(mRMThick * mRMLenth * mRMWidth * mDensity / (1000), "0.000"))
                    ElseIf mRMType = "ROD" Then
                        mWtPerStrip = CDbl(VB6.Format((3.14 / 4) * (mRMDiaMeter * mRMDiaMeter) * mRMLenth * mDensity / (1000), "0.000"))
                    ElseIf mRMType = "ROUND PIPE" Then
                        mWtPerStrip = CDbl(VB6.Format(3.14 * (mRMDiaMeter - mRMThick) * mRMLenth * mDensity / (1000), "0.000"))
                    Else
                        If mRMThick <> 0 And mRMLenth <> 0 And mRMWidth <> 0 Then
                            mWtPerStrip = CDbl(VB6.Format(mRMThick * mRMLenth * mRMWidth * 7.85 / (1000), "0.000"))
                        ElseIf mRMThick <> 0 And mRMLenth <> 0 And mRMDiaMeter <> 0 Then
                            mWtPerStrip = CDbl(VB6.Format(3.14 * (mRMDiaMeter - mRMThick) * mRMLenth * 7.85 / (1000), "0.000"))
                        ElseIf mRMLenth <> 0 And mRMDiaMeter <> 0 Then
                            mWtPerStrip = CDbl(VB6.Format((3.14 / 4) * (mRMDiaMeter * mRMDiaMeter) * mRMLenth * 7.85 / (1000), "0.000"))
                        End If
                    End If
                    '                mWtPerStrip = mWtPerStrip * 1000 ''IN Grams			
                    .Col = ColWtPerStrip
                    .Text = VB6.Format(mWtPerStrip, "0.000")
                Else
                    .Col = ColWtPerStrip
                    mWtPerStrip = CDbl(VB6.Format(Val(.Text), "0.000"))
                End If
                .Col = ColQtyPerStrip
                mQtyPerStrip = Val(.Text)

                If mQtyPerStrip = 0 Then GoTo NextLoop

                .Col = ColWtPerPc
                mWtPerPc = CDbl(VB6.Format(mWtPerStrip / mQtyPerStrip, "0.000"))
                .Text = VB6.Format(mWtPerPc, "0.000")

                .Col = ColRMCost
                mRMCost = CDbl(VB6.Format(mWtPerPc * mRMRate, "0.00"))
                mRMCost = mRMCost / 1000 ''In KGS			
                .Text = VB6.Format(mRMCost, "0.00")

                .Col = ColNetWt
                mNetWt = Val(.Text)

                .Col = ColScrapWt
                mScrapWt = CDbl(VB6.Format(mWtPerPc - mNetWt, "0.000"))
                .Text = VB6.Format(mScrapWt, "0.000")

                .Col = ColScrapRate
                mScrapRate = Val(.Text)

                .Col = ColScrapCost
                mScrapCost = CDbl(VB6.Format(mScrapWt * mScrapRate, "0.00"))
                mScrapCost = mScrapCost / 1000 ''In KGS			
                .Text = VB6.Format(mScrapCost, "0.00")

                .Col = ColNetRMCost
                mNetRMCost = CDbl(VB6.Format(mRMCost - mScrapCost, "0.00"))
                .Text = VB6.Format(mNetRMCost, "0.00")

                mTotalGrossCost = mTotalGrossCost + mRMCost
                mTotalScrapCost = mTotalScrapCost + mScrapCost
                mTotalNetCost = mTotalNetCost + mNetRMCost
                mTotalGrossWt = mTotalGrossWt + mWtPerPc
                mTotalScrapWt = mTotalScrapWt + mScrapWt
                mTotalNetWt = mTotalNetWt + mNetWt

NextLoop:
            Next
        End With

        '    With SprdRM			
        '        For i = 1 To .MaxRows			
        '            .Row = i			
        '			
        '            .Col = ColRMItemDesc			
        '            If Trim(.Text) = "" Then GoTo NextRMLoop:			
        '			
        '            .Col = ColRMGrossWt			
        '            mRMGrossWt = Val(.Text)			
        '			
        '            .Col = ColRMItemRate			
        '            mRMItemRate = Val(.Text)			
        '			
        '            mRMItemAmount = mRMGrossWt * mRMItemRate / 1000			
        '			
        '            .Col = ColRMItemAmount			
        '            .Text = mRMItemAmount			
        '			
        '            .Col = ColRMScrapWt			
        '            mRMScrapWt = Val(.Text)			
        '			
        '            .Col = ColRMScrapRate			
        '            mRMScrapRate = Val(.Text)			
        '			
        '            mRMScrapAmount = mRMScrapWt * mRMScrapRate / 1000			
        '			
        '            .Col = ColRMScrapAmount			
        '            .Text = mRMScrapAmount			
        '			
        '            mRMNetWt = mRMGrossWt - mRMScrapWt			
        '			
        '            .Col = ColRMNetWt			
        '            .Text = mRMNetWt			
        '			
        '            mRMNetAmount = mRMItemAmount - mRMScrapAmount			
        '			
        '            .Col = ColRMNetAmount			
        '            .Text = mRMNetAmount			
        '			
        '            .Col = ColRMFreight			
        '            mRMFreight = Val(.Text)			
        '			
        '            mRMTotAmount = mRMNetAmount + mRMFreight			
        '			
        '            .Col = ColRMTotAmount			
        '            .Text = mRMTotAmount			
        '			
        '            mTotRMCost = mTotRMCost + mRMTotAmount			
        'NextRMLoop:			
        '        Next			
        '    End With			

        With SprdBOP
            For i = 1 To .MaxRows
                .Row = i

                .Col = ColBOPItemDesc
                If Trim(.Text) = "" Then GoTo NextBOPLoop

                .Col = ColBOPItemQty
                mBOPItemQty = Val(.Text)

                .Col = ColBOPItemRate
                mBOPItemRate = Val(.Text)

                mBOPItemAmount = mBOPItemQty * mBOPItemRate

                .Col = ColBOPItemAmount
                .Text = CStr(mBOPItemAmount)

                .Col = ColBOPFreight
                mBOPFreight = Val(.Text)

                mBOPTotAmount = mBOPItemAmount + mBOPFreight

                .Col = ColBOPTotAmount
                .Text = CStr(mBOPTotAmount)

                mTotBOPCost = mTotBOPCost + mBOPTotAmount
NextBOPLoop:
            Next
        End With

        With SprdWeld
            For i = 1 To .MaxRows
                .Row = i

                .Col = ColWeldDesc
                If Trim(.Text) = "" Then GoTo NextWeldLoop

                .Col = ColWeldQty
                mWeldQty = Val(.Text)

                .Col = ColWeldRate
                mWeldRate = Val(.Text)

                .Col = ColWeldAmount
                .Text = VB6.Format(mWeldQty * mWeldRate, "0.000")
                mTotWeldCost = mTotWeldCost + Val(.Text)
NextWeldLoop:
            Next
        End With

        With SprdOpr
            For i = 1 To .MaxRows
                .Row = i

                .Col = ColOprOprDesc
                If Trim(.Text) = "" Then GoTo NextOprLoop

                .Col = ColOprOprRate
                mOprOprRate = Val(.Text)

                mTotOprCost = mTotOprCost + mOprOprRate
                .Col = ColOprType
                If Val(.Text) = 1 Then
                    mTotWeldCost = mTotWeldCost + mOprOprRate
                Else
                    mTotProcessCost = mTotProcessCost + mOprOprRate
                End If
NextOprLoop:
            Next
        End With

        With SprdPlt
            .Col = 4

            .Row = 1
            mGrossArea = Val(.Text)

            .Row = 2
            mNoOfSide = Val(.Text)

            mTotGrossArea = mGrossArea * mNoOfSide

            .Row = 3
            .Text = CStr(mTotGrossArea)

            .Row = 4
            mPltNetAreaPer = Val(.Text)

            mPltNetArea = mTotGrossArea * mPltNetAreaPer / 100

            .Row = 5
            .Text = CStr(mPltNetArea)

            .Row = 6
            mPltNiCons = Val(.Text)

            .Row = 7
            mPltNiRate = Val(.Text)

            mPltCostNiCons = mPltNiCons * mPltNiRate

            .Row = 8
            .Text = CStr(mPltCostNiCons)

            .Row = 9
            mPltConvCost = Val(.Text)

            mPltCostNiDM = mPltCostNiCons + mPltConvCost

            .Row = 10
            .Text = CStr(mPltCostNiDM)

            mPltCostNiPc = mPltNetArea * mPltCostNiDM

            .Row = 11
            .Text = CStr(mPltCostNiPc)

            .Row = 12
            mPltNetChemAreaPer = Val(.Text)

            mPltNetChemArea = mTotGrossArea * mPltNetChemAreaPer / 100

            .Row = 13
            .Text = CStr(mPltNetChemArea)

            .Row = 14
            mPltChemCons = Val(.Text)

            .Row = 15
            mPltChemRate = Val(.Text)

            mPltCostChemDM = mPltChemCons * mPltChemRate

            .Row = 16
            .Text = CStr(mPltCostChemDM)

            mPltCostChemPc = mPltNetChemArea * mPltCostChemDM

            .Row = 17
            .Text = CStr(mPltCostChemPc)

            .Row = 18
            mPltNetBuffingAreaPer = Val(.Text)

            mPltNetBuffingArea = mTotGrossArea * mPltNetBuffingAreaPer / 100

            .Row = 19
            .Text = CStr(mPltNetBuffingArea)

            .Row = 20
            mPltCostBuffingDM = Val(.Text)

            mPltCostBuffingPc = mPltNetBuffingArea * mPltCostBuffingDM

            .Row = 21
            .Text = CStr(mPltCostBuffingPc)

            .Row = 22
            mPltNetCromeAreaPer = Val(.Text)

            mPltNetCromeArea = mTotGrossArea * mPltNetCromeAreaPer / 100

            .Row = 23
            .Text = CStr(mPltNetCromeArea)

            .Row = 24
            mPltCostCromeDM = Val(.Text)

            mPltCostCromePc = mPltNetCromeArea * mPltCostCromeDM

            .Row = 25
            .Text = CStr(mPltCostCromePc)

            .Row = 26
            mPltCostHydrogenPc = Val(.Text)

            mPltTotCost = mPltCostNiPc + mPltCostChemPc + mPltCostBuffingPc + mPltCostCromePc + mPltCostHydrogenPc

            .Row = 27
            .Text = CStr(mPltTotCost)

            .Row = 28
            mPltOverheadPer = Val(.Text)

            mPltOverhead = mPltTotCost * mPltOverheadPer / 100

            .Row = 29
            .Text = CStr(mPltOverhead)

            mTotPltCost = mPltTotCost + mPltOverhead

            .Row = 30
            .Text = CStr(mTotPltCost)
        End With



        With SprdPnt
            For i = 1 To .MaxRows
                .Row = i

                .Col = ColPNTItemDesc
                If Trim(.Text) = "" Then GoTo NextPNTLoop

                .Col = ColPNTItemQty
                mPNTItemQty = Val(.Text)

                .Col = ColPNTItemRate
                mPNTItemRate = Val(.Text)

                mPNTItemAmount = mPNTItemQty * mPNTItemRate / 1000

                .Col = ColPNTItemAmount
                .Text = CStr(mPNTItemAmount)

                mTotPNTCost = mTotPNTCost + mPNTItemAmount
NextPNTLoop:
            Next
        End With

        '    With SprdPnt			
        '        .Col = 4			
        '			
        '        .Row = 1			
        '        .Text = mGrossArea			
        '			
        '        .Row = 2			
        '        .Text = mNoOfSide			
        '			
        '        .Row = 3			
        '        .Text = mTotGrossArea			
        '			
        '        .Row = 4			
        '        mPntNetAreaPer = Val(.Text)			
        '			
        '        mPntNetArea = mTotGrossArea * mPntNetAreaPer / 100			
        '			
        '        .Row = 5			
        '        .Text = mPntNetArea			
        '			
        '        .Row = 6			
        '        mPntChemCons = Val(.Text)			
        '			
        '        .Row = 7			
        '        mPntChemRate = Val(.Text)			
        '			
        '        mPntCostChemCons = mPntChemCons * mPntChemRate			
        '			
        '        .Row = 8			
        '        .Text = mPntCostChemCons			
        '			
        '        .Row = 9			
        '        mPntConvCost = Val(.Text)			
        '			
        '        mPntCostDm = mPntCostChemCons + mPntConvCost			
        '			
        '        .Row = 10			
        '        .Text = mPntCostDm			
        '			
        '        mPntCostPc = mPntCostDm * mPntNetArea			
        '			
        '        .Row = 11			
        '        .Text = mPntCostPc			
        '			
        '        .Row = 12			
        '        mPntOverheadPer = Val(.Text)			
        '			
        '        mPntOverhead = mPntCostPc * mPntOverheadPer / 100			
        '			
        '        .Row = 13			
        '        .Text = mPntOverhead			
        '			
        '        mTotPntCost = mPntCostPc + mPntOverhead			
        '			
        '        .Row = 14			
        '        .Text = mTotPntCost			
        '    End With			

        With SprdPdr
            .Col = 4

            .Row = 1
            .Text = CStr(mGrossArea)

            .Row = 2
            .Text = CStr(mNoOfSide)

            .Row = 3
            .Text = CStr(mTotGrossArea)

            .Row = 4
            mPdrNetAreaPer = Val(.Text)

            mPdrNetArea = mTotGrossArea * mPdrNetAreaPer / 100

            .Row = 5
            .Text = CStr(mPdrNetArea)

            .Row = 6
            mPdrChemCons = Val(.Text)

            .Row = 7
            mPdrChemRate = Val(.Text)

            mPdrCostChemCons = mPdrChemCons * mPdrChemRate

            .Row = 8
            .Text = CStr(mPdrCostChemCons)

            .Row = 9
            mPdrConvCost = Val(.Text)

            mPdrCostDm = mPdrCostChemCons + mPdrConvCost

            .Row = 10
            .Text = CStr(mPdrCostDm)

            mPdrCostPc = mPdrCostDm * mPdrNetArea

            .Row = 11
            .Text = CStr(mPdrCostPc)

            .Row = 12
            mPdrOverheadPer = Val(.Text)

            mPdrOverhead = mPdrCostPc * mPdrOverheadPer / 100

            .Row = 13
            .Text = CStr(mPdrOverhead)

            mTotPdrCost = mPdrCostPc + mPdrOverhead

            .Row = 14
            .Text = CStr(mTotPdrCost)
        End With

        With SprdPack
            For i = 1 To .MaxRows
                .Row = i

                .Col = ColPackPackDetail
                If Trim(.Text) = "" Then GoTo NextPackLoop

                .Col = ColPackPackRate
                mPackPackRate = Val(.Text)

                mTotPackCost = mTotPackCost + mPackPackRate

                .Col = ColPackType
                If Val(.Text) = 1 Then
                    mTotHandlingCost = mTotHandlingCost + mPackPackRate
                ElseIf Val(.Text) = 2 Then
                    mTotToolCost = mTotToolCost + mPackPackRate
                ElseIf Val(.Text) = 3 Then
                    mTotInterestCost = mTotInterestCost + mPackPackRate
                Else
                    mTotPackMaterialCost = mTotPackMaterialCost + mPackPackRate
                End If
NextPackLoop:
            Next
            mTotPackCost = mTotPackCost - mTotToolCost
        End With

        txtTotRMCost.Text = VB6.Format(mTotalNetCost, "0.000")
        txtTotBOPCost.Text = VB6.Format(mTotBOPCost, "0.000")
        txtTotWeldCost.Text = VB6.Format(mTotWeldCost, "0.000")
        txtTotProcessCost.Text = VB6.Format(mTotProcessCost, "0.000")
        lblOperationCost.Text = VB6.Format(mTotWeldCost + mTotProcessCost, "0.000")

        If Val(txtToolQty.Text) > 0 Then
            txtToolCostPerPc.Text = VB6.Format(Val(txtToolCost.Text) / Val(txtToolQty.Text), "0.000")
        Else
            txtToolCostPerPc.Text = "0.000"
        End If


        '    mTotalGrossCost = mTotalGrossCost + mRMCost			
        '            mTotalScrapCost = mTotalScrapCost + mScrapCost			
        '            mTotalNetCost = mTotalNetCost + mNetRMCost			
        '            mTotalGrossWt = mTotalGrossWt + mWtPerPc			
        '            mTotalScrapWt = mTotalScrapWt + mScrapWt			
        '            mTotalNetWt = mTotalNetWt + mNetWt			



        '    txtTotProcessCost.Text = Format(mTotOprCost, "0.000")			
        txtTotPltCost.Text = VB6.Format(mTotPltCost, "0.000")
        txtTotPntCost.Text = VB6.Format(mTotPNTCost, "0.000")
        txtTotPdrCost.Text = VB6.Format(mTotPdrCost, "0.000")
        mTotValueAdd = mTotPltCost + mTotPNTCost + mTotPdrCost
        txtTotValueAdd.Text = VB6.Format(mTotValueAdd, "0.000")
        mTotProdCost = mTotalNetCost + mTotBOPCost + mTotWeldCost + mTotProcessCost + mTotValueAdd
        txtTotProdCost.Text = VB6.Format(mTotProdCost, "0.000")
        mOverheadPer = Val(txtOverheadPer.Text)

        If mOverheadPer = 0 Then
            mOverheadCost = CDbl(VB6.Format(Val(txtOverheadCost.Text), "0.000"))
        Else
            mOverheadCost = mTotProdCost * mOverheadPer / 100
        End If

        txtOverheadCost.Text = VB6.Format(mOverheadCost, "0.000")


        lblTotPackCost.Text = VB6.Format(mTotPackCost, "0.000")

        mRejPer = Val(txtRejPer.Text)
        If mRejPer = 0 Then
            mRejCost = Val(txtRejCost.Text)
        Else
            mRejCost = mTotProdCost * mRejPer / 100
        End If

        txtRejCost.Text = VB6.Format(mRejCost, "0.000")
        mTransportCost = Val(txtTransportCost.Text)

        lblHandlingCode.Text = VB6.Format(mTotHandlingCost, "0.000")
        txtHandling.Text = VB6.Format(mTotHandlingCost, "0.000")
        lblToolCost.Text = VB6.Format(mTotToolCost, "0.000")
        txtToolCost.Text = VB6.Format(mTotToolCost, "0.000")
        lblInterest.Text = VB6.Format(mTotInterestCost, "0.000")
        txtICC.Text = VB6.Format(mTotInterestCost, "0.000")
        lblPackMaterialCost.Text = VB6.Format(mTotPackMaterialCost, "0.000")
        txtPMCost.Text = VB6.Format(mTotPackMaterialCost, "0.000")

        mTotSaleCost = mTotProdCost + mOverheadCost + mRejCost + mTransportCost + mTotToolCost + mTotHandlingCost + mTotInterestCost + mTotPackMaterialCost '' mTotPackCost +			
        txtTotSaleCost.Text = VB6.Format(mTotSaleCost, "0.000")

        mProfitPer = Val(txtProfitPer.Text)
        If mProfitPer = 0 Then
            mProfitCost = Val(txtProfitCost.Text)
        Else
            mProfitCost = mTotProdCost * mProfitPer / 100 ''mTotSaleCost			
        End If
        txtProfitCost.Text = VB6.Format(mProfitCost, "0.000")


        mTotSalePrice = mTotSaleCost + mProfitCost - Val(txtCostReduction.Text)

        txtTotSalePrice.Text = VB6.Format(mTotSalePrice, "0.000")
        mTotPriceSettelled = Val(txtTotPriceSettelled.Text)
        mDiscount = mTotSalePrice - mTotPriceSettelled
        If mDiscount < 0 Then mDiscount = 0
        txtDiscount.Text = VB6.Format(mDiscount, "0.000")


        Exit Sub
AuERR:
        '    Resume			
        MsgBox(Err.Description)
    End Sub



    Private Sub SprdWeld_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdWeld.Change

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdWeld_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdWeld.ClickEvent



        If eventArgs.col = 0 And eventArgs.row > 0 Then
            MainClass.DeleteSprdRow(SprdWeld, eventArgs.row, ColWeldDesc)
        End If

        Call AutoCalc()
    End Sub

    Private Sub SprdWeld_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdWeld.LeaveCell

        On Error GoTo ErrPart
        Dim xWeldDesc As String
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset

        If eventArgs.newRow = -1 Then Exit Sub

        With SprdWeld
            .Row = .ActiveRow
            .Col = ColWeldDesc
            xWeldDesc = Trim(.Text)
            If xWeldDesc = "" Then Exit Sub

            Select Case eventArgs.col
                Case ColWeldDesc
                    .Col = ColWeldDesc
                    xWeldDesc = Trim(.Text)
                    Call CheckDuplicateWeld(xWeldDesc)

                Case ColWeldQty
                    If CheckQty(SprdWeld, eventArgs.col, eventArgs.row) = True Then
                        MainClass.AddBlankSprdRow(SprdWeld, ColWeldDesc, ConRowHeight)
                        FormatSprdWeld(.MaxRows)
                    End If
            End Select
        End With

        Call AutoCalc()
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtAmendNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAmendNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtAppBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAppBy.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAppBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAppBy.DoubleClick
        Call cmdSearchAppBy_Click(cmdSearchAppBy, New System.EventArgs())
    End Sub

    Private Sub txtAppBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtAppBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchAppBy_Click(cmdSearchAppBy, New System.EventArgs())
    End Sub

    Private Sub txtAppBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAppBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim SqlStr As String
        If Trim(txtAppBy.Text) = "" Then GoTo EventExitSub
        txtAppBy.Text = VB6.Format(Trim(txtAppBy.Text), "000000")

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        If ADDMode = True Then
            SqlStr = SqlStr & " AND EMP_LEAVE_DATE IS NULL "
        End If

        If MainClass.ValidateWithMasterTable(txtAppBy.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("EMPLOYEE Does Not Exist In Master.")
            Cancel = True
        Else
            lblAppBy.Text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCopyCustCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCopyCustCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCopyCustCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCopyCustCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = Asc(UCase(Chr(KeyAscii)))
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCopyCustCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCopyCustCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchCopyCustCode()
    End Sub

    Private Sub txtCopyCustCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCopyCustCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim mRs As ADODB.Recordset
        Dim SqlStr As String
        Dim mCopyMkey As String

        If Trim(txtCopyCustCode.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtCopyCustCode.Text, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            If Trim(txtCopyProductCode.Text) = "" Then GoTo EventExitSub

            SqlStr = " SELECT * FROM PRD_CUST_FG_COST_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCopyCustCode.Text) & "'" & vbCrLf _
            & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtCopyProductCode.Text) & "' "

            SqlStr = SqlStr & vbCrLf _
            & " AND AMEND_NO = (" & vbCrLf _
            & " SELECT MAX(AMEND_NO) AS AMEND_NO " & vbCrLf _
            & " FROM PRD_CUST_FG_COST_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCopyCustCode.Text) & "'" & vbCrLf _
            & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtCopyProductCode.Text) & "'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRs, ADODB.LockTypeEnum.adLockReadOnly)
            If mRs.EOF = False Then
                mCopyMkey = mRs.Fields("mKey").Value
                '            MainClass.ClearGrid SprdMain			
                '            Call ShowBOMDetail1(lblCopyMKey.text)			
                '            Call ShowBOMAlterDetail(lblCopyMKey.text)			
            Else
                MsgBox("Costing Not defined for this Customer", MsgBoxStyle.Information)
                Cancel = True
                GoTo EventExitSub
            End If
        Else
            MsgBox("Invaild Customer Code.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub SearchCopyCustCode()
        On Error GoTo SrchERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='C' "
        If MainClass.SearchGridMaster("", "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            txtCopyCustCode.Text = AcName1
            If txtCopyCustCode.Enabled = True Then txtCopyCustCode.Focus()
        End If
        Exit Sub
SrchERR:
        MsgBox(Err.Description)
    End Sub
    Private Sub txtCopyProductCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCopyProductCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCopyProductCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCopyProductCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = Asc(UCase(Chr(KeyAscii)))
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCopyProductCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCopyProductCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchCopyProdCode_Click(cmdSearchCopyProdCode, New System.EventArgs())
    End Sub

    Private Sub txtCopyProductCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCopyProductCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim mRs As ADODB.Recordset
        Dim SqlStr As String
        Dim mCopyMkey As String

        If Trim(txtCopyProductCode.Text) = "" Then GoTo EventExitSub


        If MainClass.ValidateWithMasterTable(txtCopyProductCode.Text, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtCopyProductDesc.Text = MasterNo

            If Trim(txtCopyCustCode.Text) = "" Then GoTo EventExitSub
            SqlStr = " SELECT * FROM PRD_CUST_FG_COST_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCopyCustCode.Text) & "'" & vbCrLf _
            & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtCopyProductCode.Text) & "' "

            SqlStr = SqlStr & vbCrLf _
            & " AND AMEND_NO = (" & vbCrLf _
            & " SELECT MAX(AMEND_NO) AS AMEND_NO " & vbCrLf _
            & " FROM PRD_CUST_FG_COST_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCopyCustCode.Text) & "'" & vbCrLf _
            & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtCopyProductCode.Text) & "')"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRs, ADODB.LockTypeEnum.adLockReadOnly)
            If mRs.EOF = False Then
                mCopyMkey = mRs.Fields("mKey").Value
                Call Copy1(mRs, mCopyMkey)
                '            MainClass.ClearGrid SprdMain			
                '            Call ShowBOMDetail1(lblCopyMKey.text)			
                '            Call ShowBOMAlterDetail(lblCopyMKey.text)			
            Else
                MsgBox("Costing Not defined for this Product", MsgBoxStyle.Information)
                Cancel = True
                GoTo EventExitSub
            End If
        Else
            MsgBox("Invaild Item Code.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCustPODate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustPODate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCustPODate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCustPODate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtCustPODate.Text) = "" Then GoTo EventExitSub
        If IsDate(txtCustPODate.Text) = False Then
            MsgBox("Invalid Date")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCustPONo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustPONo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCustPONo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCustPONo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCustPONo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtHandling_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtHandling.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtHandling_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtHandling.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtHandling_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtHandling.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtHandling.Text = VB6.Format(txtHandling.Text, "0.000")
        Call AutoCalc()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtICC_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtICC.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtICC_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtICC.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtICC_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtICC.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtICC.Text = VB6.Format(txtICC.Text, "0.000")
        Call AutoCalc()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtOverheadCost_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOverheadCost.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOverheadCost_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOverheadCost.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtOverheadCost_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtOverheadCost.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtOverheadCost.Text = VB6.Format(txtOverheadCost.Text, "0.000")
        Call AutoCalc()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtOverheadPer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOverheadPer.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOverheadPer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOverheadPer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtOverheadPer_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtOverheadPer.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtOverheadPer.Text = VB6.Format(txtOverheadPer.Text, "0.000")
        Call AutoCalc()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPMCost_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPMCost.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPMCost_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPMCost.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtPMCost_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPMCost.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtPMCost.Text = VB6.Format(txtPMCost.Text, "0.000")
        Call AutoCalc()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtProfitCost_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProfitCost.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtProfitCost_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtProfitCost.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtProfitCost_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtProfitCost.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtProfitCost.Text = VB6.Format(txtProfitCost.Text, "0.000")
        Call AutoCalc()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtProfitPer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProfitPer.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtProfitPer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtProfitPer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtProfitPer_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtProfitPer.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtProfitPer.Text = VB6.Format(txtProfitPer.Text, "0.000")
        Call AutoCalc()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRejCost_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRejCost.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRejCost_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRejCost.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRejCost_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRejCost.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtRejCost.Text = VB6.Format(txtRejCost.Text, "0.000")
        Call AutoCalc()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRejPer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRejPer.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRejPer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRejPer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRejPer_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRejPer.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtRejPer.Text = VB6.Format(txtRejPer.Text, "0.000")
        Call AutoCalc()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSuppCustCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSuppCustCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSuppCustCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSuppCustCode.DoubleClick
        Call cmdSearchCust_Click(cmdSearchCust, New System.EventArgs())
    End Sub

    Private Sub txtSuppCustCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSuppCustCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchCust_Click(cmdSearchCust, New System.EventArgs())
    End Sub

    Private Sub txtSuppCustCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSuppCustCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValEMP
        Dim SqlStr As String
        If Trim(txtSuppCustCode.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(txtSuppCustCode.Text, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Customer Does Not Exist In Master.")
            Cancel = True
        Else
            txtSuppCustName.Text = MasterNo
        End If
        Call ShowRecord()
        GoTo EventExitSub
ValEMP:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtProductCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProductCode.DoubleClick
        cmdSearchProdCode_Click(cmdSearchProdCode, New System.EventArgs())
    End Sub

    Private Sub txtProductCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProductCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtProductCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtProductCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = Asc(UCase(Chr(KeyAscii)))
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtProductCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtProductCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchProdCode_Click(cmdSearchProdCode, New System.EventArgs())
    End Sub

    Private Sub txtProductCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtProductCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim mRs As ADODB.Recordset
        Dim SqlStr As String
        Dim xMkey As String
        If Trim(txtProductCode.Text) = "" Then GoTo EventExitSub

        SqlStr = " SELECT ITEM_CODE, ITEM_SHORT_DESC, ISSUE_UOM, ITEM_MODEL, CUSTOMER_PART_NO " & vbCrLf _
        & " FROM INV_ITEM_MST " & vbCrLf _
        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
        & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "'" & vbCrLf _
        & " AND ITEM_STATUS = 'A' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRs, ADODB.LockTypeEnum.adLockReadOnly)

        If Not mRs.EOF Then
            txtProductDesc.Text = IIf(IsDBNull(mRs.Fields("ITEM_SHORT_DESC").Value), "", mRs.Fields("ITEM_SHORT_DESC").Value)
            txtUnit.Text = IIf(IsDBNull(mRs.Fields("ISSUE_UOM").Value), "", mRs.Fields("ISSUE_UOM").Value)
            txtModelNo.Text = IIf(IsDBNull(mRs.Fields("ITEM_MODEL").Value), "", mRs.Fields("ITEM_MODEL").Value)
            txtCustPartNo.Text = IIf(IsDBNull(mRs.Fields("CUSTOMER_PART_NO").Value), "", mRs.Fields("CUSTOMER_PART_NO").Value)
        Else
            txtProductDesc.Text = ""
            txtUnit.Text = ""
            txtModelNo.Text = ""
            txtCustPartNo.Text = ""
            MsgBox("Invalid Item.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

        Call ShowRecord()
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
        '    Resume			
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPrepBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPrepBy.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPrepBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPrepBy.DoubleClick
        Call cmdSearchPrepBy_Click(cmdSearchPrepBy, New System.EventArgs())
    End Sub

    Private Sub txtPrepBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPrepBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchPrepBy_Click(cmdSearchPrepBy, New System.EventArgs())
    End Sub

    Private Sub txtPrepBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPrepBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValEMP
        Dim SqlStr As String
        If Trim(txtPrepBy.Text) = "" Then GoTo EventExitSub
        txtPrepBy.Text = VB6.Format(Trim(txtPrepBy.Text), "000000")
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If ADDMode = True Then
            SqlStr = SqlStr & " AND EMP_LEAVE_DATE IS NULL "
        End If

        If MainClass.ValidateWithMasterTable(txtPrepBy.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Employee Does Not Exist In Master.")
            Cancel = True
        Else
            lblPrepBy.Text = MasterNo
        End If
        GoTo EventExitSub
ValEMP:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
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

    Private Sub txtToolCost_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtToolCost.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtToolCost.Text = VB6.Format(txtToolCost.Text, "0.000")
        Call AutoCalc()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtTotPriceSettelled_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTotPriceSettelled.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTotPriceSettelled_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTotPriceSettelled.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTotPriceSettelled_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTotPriceSettelled.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtTotPriceSettelled.Text = VB6.Format(txtTotPriceSettelled.Text, "0.000")
        Call AutoCalc()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtTotSalePrice_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTotSalePrice.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTotSalePrice_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTotSalePrice.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTransportCost_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTransportCost.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTransportCost_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTransportCost.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTransportCost_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTransportCost.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtTransportCost.Text = VB6.Format(txtTransportCost.Text, "0.000")
        Call AutoCalc()
        eventArgs.Cancel = Cancel
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
        If Trim(txtWEF.Text) = "" Then GoTo EventExitSub

        If IsDate(txtWEF.Text) = False Then
            MsgBox("Invalid Date")
            Cancel = True
        Else
            Call ShowRecord()
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Function ShowRecord() As Boolean

        On Error GoTo ERR1
        Dim mRs As ADODB.Recordset
        Dim SqlStr As String
        Dim xMkey As String

        ShowRecord = True

        If Trim(txtSuppCustCode.Text) = "" Or Trim(txtProductCode.Text) = "" Then Exit Function

        If MODIFYMode = True And RsFGCostMain.EOF = False Then xMkey = RsFGCostMain.Fields("mKey").Value

        SqlStr = " SELECT * FROM PRD_CUST_FG_COST_HDR " & vbCrLf _
        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtSuppCustCode.Text) & "' " & vbCrLf _
        & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "' "

        If Trim(txtWEF.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND WEF=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            SqlStr = SqlStr & vbCrLf _
            & " AND WEF = (" & vbCrLf _
            & " SELECT MAX(WEF) AS WEF " & vbCrLf _
            & " FROM PRD_CUST_FG_COST_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtSuppCustCode.Text) & "' " & vbCrLf _
            & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "') "
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsFGCostMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsFGCostMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Costing Not Made For This Item. Click Add For New.", MsgBoxStyle.Information)
                ShowRecord = False
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM PRD_CUST_FG_COST_HDR " & vbCrLf & " WHERE UPPER(LTRIM(RTRIM(MKey)))='" & MainClass.AllowSingleQuote(UCase(xMkey)) & "' " & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsFGCostMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
End Class
