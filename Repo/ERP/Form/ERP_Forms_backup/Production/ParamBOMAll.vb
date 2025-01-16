Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamBOMAll
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection	
    Private Const RowHeight As Short = 22

    Private Const ColPicMain As Short = 1
    Private Const ColPicSub As Short = 2
    Private Const ColProductCode As Short = 3
    Private Const ColProductDesc As Short = 4
    Private Const ColSRNo As Short = 5
    Private Const ColRMCode As Short = 6
    Private Const ColRMDesc As Short = 7
    Private Const ColPartNo As Short = 8
    Private Const ColDept As Short = 9
    Private Const ColDrgYN As Short = 10
    Private Const ColDrgSize As Short = 11
    Private Const ColDrgRevNo As Short = 12
    Private Const colStdQty As Short = 13
    Private Const ColMtrl As Short = 14
    Private Const ColItemVendor As Short = 15
    Private Const ColSurfTreat As Short = 16
    Private Const ColLevel As Short = 17
    Private Const ColUom As Short = 18
    Private Const ColFlag As Short = 19
    Private Const ColScrap As Short = 20
    Private Const ColProductPartNo As Short = 21
    Private Const ColProductModel As Short = 22
    Private Const ColRate As Short = 23
    Private Const ColProcessRate As Short = 24
    Private Const ColValue As Short = 25

    Private Const ColProdCategory As Short = 26
    Private Const ColRMCategory As Short = 27

    Private Const ColProdUOM As Short = 28
    Private Const ColProdOutputQty As Short = 29

    Dim mcntRow As Integer

    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Dim minuspict As System.Drawing.Image
    Dim pluspict As System.Drawing.Image
    Dim mIsGrouped As Boolean

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer

    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub


    Private Sub chkRate_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkRate.CheckStateChanged
        Call PrintStatus(False)
    End Sub

    Private Sub chkVendor_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkVendor.CheckStateChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        MainClass.ClearGrid(SprdMain, RowHeight)
        FormatSprdMain()

        '    If chkAll.Value = vbUnchecked Then	
        '        If Trim(txtWEF.Text) = "" Then	
        '            MsgInformation "Please Enter Date."	
        '            txtWEF.SetFocus	
        '            Exit Sub	
        '        End If	
        '    End If	
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Show1()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        SprdMain.Focus()
        Call PrintStatus(True)
    End Sub
    Private Sub Show1()

        On Error GoTo LedgError
        Dim RsMain As ADODB.Recordset
        Dim RsShow As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mRMCode As String
        Dim mProductCode As String
        Dim mNextProductCode As String
        Dim I As Integer
        Dim mSrn As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim pSqlStr As String
        Dim mLevel As Integer
        Dim mCatCode As String = ""
        Dim mSubCatCode As String
        Dim pWEF As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = " SELECT IH.PRODUCT_CODE, WEF " & vbCrLf _
            & " FROM PRD_NEWBOM_HDR IH, INV_ITEM_MST INVMST" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.PRODUCT_CODE=INVMST.ITEM_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If chkAllCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtCategory.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                mCatCode = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND INVMST.CATEGORY_CODE='" & MainClass.AllowSingleQuote(mCatCode) & "'"
            End If
        End If

        If chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtSubCategory.Text, "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCatCode & "'") = True Then
                mSubCatCode = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND INVMST.SUBCATEGORY_CODE='" & MainClass.AllowSingleQuote(mSubCatCode) & "'"
            End If
        End If

        SqlStr = SqlStr & vbCrLf & " AND IH.STATUS='O' "

        If chkApproved.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " AND IH.IS_APPROVED='Y'"
            'Else
            '    SqlStr = SqlStr & vbCrLf & " AND IH.IS_APPROVED='N'"
        End If

        If chkDespatch.Checked = True Then
            SqlStr = SqlStr & vbCrLf _
                & " AND IH.PRODUCT_CODE IN (SELECT DISTINCT DD.ITEM_CODE FROM FIN_INVOICE_HDR DH, FIN_INVOICE_DET DD " & vbCrLf _
                & " WHERE DH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " And DH.MKEY=DD.MKEY)"
        ElseIf chkTopProduct.Checked = True Then
            SqlStr = SqlStr & vbCrLf _
                & " AND IH.PRODUCT_CODE NOT IN (SELECT DISTINCT RM_CODE FROM PRD_NEWBOM_DET " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ")"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY IH.PRODUCT_CODE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMain, ADODB.LockTypeEnum.adLockReadOnly)
        mcntRow = 0

        If RsMain.EOF = False Then
            Do While Not RsMain.EOF
                mProductCode = Trim(IIf(IsDbNull(RsMain.Fields("PRODUCT_CODE").Value), "", RsMain.Fields("PRODUCT_CODE").Value))
                pWEF = Trim(IIf(IsDbNull(RsMain.Fields("WEF").Value), "", RsMain.Fields("WEF").Value))

                SqlStr = ""
                SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.RM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " ID.DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " ID.STD_QTY, ID.GROSS_WT_SCRAP, " & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM, IH.ISSUE_UOM AS PROD_ISSUE_UOM, IH.OUTPUT_QTY "

                SqlStr = SqlStr & vbCrLf & " FROM PRD_NEWBOM_HDR IH,PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.RM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

                SqlStr = SqlStr & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "' " & vbCrLf & " AND IH.WEF=TO_DATE('" & VB6.Format(pWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


                SqlStr = SqlStr & vbCrLf & " ORDER BY ID.PRODUCT_CODE, ID.SUBROWNO"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

                I = 0
                mLevel = 1
                If Not RsShow.EOF Then
                    Do While Not RsShow.EOF
                        mcntRow = mcntRow + 1

                        I = I + 1
                        SprdMain.Row = mcntRow


                        mSrn = Str(I)

                        '                    mRMCode = Trim(IIf(IsNull(RsShow!RM_CODE), "", RsShow!RM_CODE))	
                        '                    mProductCode = Trim(IIf(IsNull(RsShow!PRODUCT_CODE), "", RsShow!PRODUCT_CODE))	

                        Call FillGridCol(RsShow, mSrn, mLevel, mProductCode, mProductCode)

                        SprdMain.MaxRows = SprdMain.MaxRows + 1
                        RsShow.MoveNext()

                    Loop
                End If
                RsMain.MoveNext()
            Loop
            RsShow.Cancel()
            RsShow.Close()
        End If

        Call FormatSprdMain()
        GroupBySpread(ColPicMain)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        RsShow = Nothing
        Exit Sub
LedgError:
        MsgInformation(Err.Description)
    End Sub


    Private Sub FillGridCol(ByRef pRs As ADODB.Recordset, ByRef pSRNo As String, ByRef pLevel As Integer, ByRef pProductCode As String, ByRef pParentCode As String)
        On Error GoTo FillGERR
        Dim mDrgRevNo As String
        Dim mRMCode As String
        'Dim pProductCode As String	
        Dim mDeptCode As String
        Dim mRate As Double
        Dim mValue As Double
        Dim mProcessRate As Double
        Dim mUOM As String

        With SprdMain
            .Col = ColProductCode
            .Text = pProductCode ''IIf(IsNull(pRs!PRODUCT_CODE), "", pRs!PRODUCT_CODE)	
            '        pProductCode = IIf(IsNull(pRs!PRODUCT_CODE), "", pRs!PRODUCT_CODE)	
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

            If MainClass.ValidateWithMasterTable(pProductCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
                .Col = ColProductDesc
                .Text = MasterNo
                .FontBold = IIf(pLevel = 1, True, False)
            End If

            .Col = ColProdCategory
            .Text = GetItemCategory(pProductCode)
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

            .Col = ColSRNo
            .Text = pSRNo
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

            mDeptCode = IIf(IsDbNull(pRs.Fields("DEPT_CODE").Value), "", pRs.Fields("DEPT_CODE").Value)

            .Col = ColRMCode
            .Text = IIf(IsDbNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value)
            mRMCode = Trim(IIf(IsDbNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value))
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

            .Col = ColRMDesc
            .Text = IIf(IsDbNull(pRs.Fields("ITEM_SHORT_DESC").Value), "", pRs.Fields("ITEM_SHORT_DESC").Value)
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

            .Col = ColRMCategory
            .Text = GetItemCategory(mRMCode)
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

            .Col = ColPartNo
            .Text = IIf(IsDbNull(pRs.Fields("CUSTOMER_PART_NO").Value), "", pRs.Fields("CUSTOMER_PART_NO").Value)
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

            .Col = ColDept
            .Text = IIf(IsDbNull(pRs.Fields("DEPT_CODE").Value), "", pRs.Fields("DEPT_CODE").Value)
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

            mDrgRevNo = IIf(IsDbNull(pRs.Fields("DRW_REVNO").Value), "", pRs.Fields("DRW_REVNO").Value)
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

            .Col = ColDrgYN
            .Text = IIf(mDrgRevNo = "", "No", "Yes")
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

            .Col = ColDrgSize
            .Text = ""
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

            .Col = ColDrgRevNo
            .Text = IIf(IsDbNull(pRs.Fields("DRW_REVNO").Value), "", pRs.Fields("DRW_REVNO").Value)
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

            .Col = colStdQty
            .Text = CStr(Val(IIf(IsDbNull(pRs.Fields("STD_QTY").Value), "", pRs.Fields("STD_QTY").Value)))
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

            .Col = ColMtrl
            .Text = IIf(IsDbNull(pRs.Fields("ITEM_TECH_DESC").Value), "", pRs.Fields("ITEM_TECH_DESC").Value)
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

            .Col = ColSurfTreat
            .Text = Str(IIf(IsDbNull(pRs.Fields("ITEM_SURFACE_AREA").Value), 0, pRs.Fields("ITEM_SURFACE_AREA").Value))
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

            .Col = ColItemVendor
            If chkVendor.CheckState = System.Windows.Forms.CheckState.Checked Then
                .Text = GetVendorName(mRMCode)
            Else
                .Text = ""
            End If
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

            .Col = ColLevel
            .Text = Str(pLevel)

            .Col = ColUom
            .Text = IIf(IsDbNull(pRs.Fields("ISSUE_UOM").Value), "", pRs.Fields("ISSUE_UOM").Value)
            mUOM = IIf(IsDbNull(pRs.Fields("ISSUE_UOM").Value), "", pRs.Fields("ISSUE_UOM").Value)
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

            .Col = ColProdUOM
            .Text = IIf(IsDbNull(pRs.Fields("PROD_ISSUE_UOM").Value), "", pRs.Fields("PROD_ISSUE_UOM").Value)
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

            .Col = ColProdOutputQty
            .Text = CStr(Val(IIf(IsDbNull(pRs.Fields("OUTPUT_QTY").Value), "1", pRs.Fields("OUTPUT_QTY").Value)))
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

            .Col = ColFlag
            .Text = "0"

            .Col = ColScrap
            .Text = CStr(Val(IIf(IsDbNull(pRs.Fields("GROSS_WT_SCRAP").Value), "", pRs.Fields("GROSS_WT_SCRAP").Value)))
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

            If MainClass.ValidateWithMasterTable(pProductCode, "ITEM_CODE", "CUSTOMER_PART_NO", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
                .Col = ColProductPartNo
                .Text = MasterNo
                .FontBold = IIf(pLevel = 1, True, False)
            End If

            If MainClass.ValidateWithMasterTable(pProductCode, "ITEM_CODE", "ITEM_MODEL", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
                .Col = ColProductModel
                .Text = MasterNo
                .FontBold = IIf(pLevel = 1, True, False)
            End If

            If chkRate.CheckState = System.Windows.Forms.CheckState.Checked Then
                If CheckItemBom(mRMCode) = True Then
                    mProcessRate = GetProcessCost(mRMCode)
                    mRate = 0
                    mValue = GetLatestWIPCost(mRMCode, mUOM, 1, VB6.Format(RunDate, "DD/MM/YYYY"), "L", "ST", mDeptCode)
                    mValue = mValue - mProcessRate
                    mValue = mValue * Val(IIf(IsDbNull(pRs.Fields("STD_QTY").Value), 0, pRs.Fields("STD_QTY").Value))
                    If mUOM = "KGS" Then
                        mValue = mValue / 1000
                    ElseIf mUOM = "TON" Then
                        mValue = mValue / 1000
                        mValue = mValue / 1000
                    End If
                    mValue = mValue + mProcessRate
                Else
                    mProcessRate = 0
                    mRate = GetLatestItemCostFromMRR(mRMCode, mUOM, 1, VB6.Format(RunDate, "DD/MM/YYYY"), "L", "ST", mDeptCode)
                    mRate = mRate * Val(IIf(IsDbNull(pRs.Fields("STD_QTY").Value), 0, pRs.Fields("STD_QTY").Value))
                    If mUOM = "KGS" Then
                        mRate = mRate / 1000
                    ElseIf mUOM = "TON" Then
                        mRate = mRate / 1000
                        mRate = mRate / 1000
                    End If
                    mValue = mRate
                End If
            Else
                mValue = 0
                mRate = 0
                mProcessRate = 0
            End If

            .Col = ColValue
            .Text = VB6.Format(mValue, "0.00")
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

            .Col = ColRate
            .Text = VB6.Format(mRate, "0.00")
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

            .Col = ColProcessRate
            .Text = VB6.Format(mProcessRate, "0.00")
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

        End With

        '    If pLevel > 1 Then	
        '        pRs.MoveNext	
        '        If pRs.EOF = False Then	
        '            mRMCode = IIf(IsNull(pRs!RM_CODE), "", pRs!RM_CODE)	
        '        Else	
        '            mRMCode = "-1"	
        '        End If	
        '    End If	

        'Call FillSubAlterRecord(mRMCode, "", pSRNo, pLevel, pProductCode, mDeptCode, pParentCode)
        Call FillSubRecord(mRMCode, "", pSRNo, pLevel, pProductCode)


        Exit Sub
FillGERR:
        '    Resume	
        MsgBox(Err.Description)
    End Sub
    Private Sub FillSubAlterRecord(ByRef pRMMainCode As String, ByRef pWEF As String, ByVal pSrn As String, ByRef pLevel As Integer, ByRef pMainProductCode As String, ByRef pDeptCode As String, ByRef pParentCode As String)

        On Error GoTo FillERR
        Dim SqlStr As String = ""
        Dim RsShow As ADODB.Recordset = Nothing
        Dim mRMCode As String
        Dim mSrn As String
        Dim xSrn As String

        mSrn = pSrn
        '    pLevel = pLevel + 1	

        If pDeptCode <> "J/W" Then
            SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.ALTER_RM_CODE AS RM_CODE, '(*) - ' || INVMST.ITEM_SHORT_DESC AS ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " ID.DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " ID.ALTER_STD_QTY AS STD_QTY, ID.ALETRSCRAP AS GROSS_WT_SCRAP," & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM, MAXIMUM_QTY, MINIMUM_QTY, IH.ISSUE_UOM AS PROD_ISSUE_UOM, IH.OUTPUT_QTY "
            SqlStr = SqlStr & vbCrLf _
                & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET IDET, PRD_BOM_ALTER_DET ID, INV_ITEM_MST INVMST" & vbCrLf _
                & " WHERE IH.MKEY=IDET.MKEY " & vbCrLf _
                & " AND IDET.MKEY=ID.MKEY " & vbCrLf _
                & " AND IDET.RM_CODE=ID.MAINITEM_CODE " & vbCrLf _
                & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
                & " AND ID.ALTER_RM_CODE=INVMST.ITEM_CODE " & vbCrLf _
                & " AND IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
                & " AND ID.MAINITEM_CODE='" & MainClass.AllowSingleQuote(pRMMainCode) & "'" & vbCrLf _
                & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pMainProductCode) & "'" & vbCrLf _
                & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_NEWBOM_HDR " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pMainProductCode) & "') "

            '& vbCrLf |                & " AND WEF<= '" & vb6.Format(pWEF, "DD-MMM-YYYY") & "')" & vbCrLf |	
            SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SUBROWNO"
        Else

            SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.ALTER_ITEM_CODE AS RM_CODE, '(**) - ' || INVMST.ITEM_SHORT_DESC AS ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " 'J/W' AS DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " ID.ALTER_ITEM_QTY * DECODE(INVMST.ISSUE_UOM,'KGS',1000,DECODE(INVMST.ISSUE_UOM,'TON',1000 * 1000,1)) AS STD_QTY, ID.ALTER_SCRAP_QTY * DECODE(INVMST.ISSUE_UOM,'KGS',1000,DECODE(INVMST.ISSUE_UOM,'TON',1000 * 1000,1)) AS GROSS_WT_SCRAP," & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM, MAXIMUM_QTY, MINIMUM_QTY,IH.PRODUCT_UOM AS PROD_ISSUE_UOM, IH.OUTPUT_QTY "

            SqlStr = SqlStr & vbCrLf _
                & " FROM PRD_OUTBOM_HDR IH, PRD_OUTBOM_ALTER_DET ID, INV_ITEM_MST INVMST" & vbCrLf _
                & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
                & " and ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
                & " AND ID.ALTER_ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf _
                & " AND ID.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
                & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pRMMainCode) & "' " & vbCrLf _
                & " AND ID.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pParentCode) & "' " & vbCrLf _
                & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_OUTBOM_HDR " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pParentCode) & "') "

            SqlStr = SqlStr & vbCrLf & " AND IH.STATUS='O'"

            SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SERIAL_NO"
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsShow.EOF Then
            Do While Not RsShow.EOF
                mcntRow = mcntRow + 1
                SprdMain.MaxRows = SprdMain.MaxRows + 1
                SprdMain.Row = mcntRow

                xSrn = mSrn
                pSrn = pSrn

                mRMCode = Trim(IIf(IsDbNull(RsShow.Fields("RM_CODE").Value), "", RsShow.Fields("RM_CODE").Value))
                Call FillGridCol(RsShow, xSrn, pLevel, pMainProductCode, pRMMainCode)
                RsShow.MoveNext()
            Loop
        End If
        RsShow = Nothing
        '        RsShow.Close	

        Exit Sub
FillERR:
        MsgBox(Err.Description)
        '    Resume	
    End Sub
    Private Sub GroupBySpread(ByRef Col As Integer)
        'Group the data by the specified column	
        Dim I As Short
        Dim currentrow As Integer
        Dim lastid As String
        Dim prevtext As Object
        Dim lastheaderrow As Integer
        Dim ret As Boolean
        Dim Currentid As String


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        SprdMain.Redraw = False
        BoldHeader(Col)

        '    For I = 1 To SprdMain.MaxRows	
        '        SprdMain.Row = I	
        '        SprdMain.Col = ColLevel	
        '        If Trim(SprdMain.Text) = 1 Then	
        '            SprdMain.Row = I	
        '            SprdMain.Row2 = I	
        '            SprdMain.Col = 1	
        '            SprdMain.col2 = SprdMain.MaxCols	
        '            SprdMain.BlockMode = True	
        '            SprdMain.BackColor = &H8000000F         ''&H80FF80	
        '            SprdMain.BlockMode = False	
        '        End If	
        '    Next	
        '    Exit Sub	

        '    SprdMain.MaxCols = SprdMain.MaxCols + 2	
        'Insert 2 columns at beginning	
        For I = 1 To 2
            '        SprdMain.InsertCols i, 1	

            'Change col width	
            SprdMain.set_ColWidth(I, 2)
        Next I

        SprdMain.Col = ColPicMain
        SprdMain.Row = -1
        SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) 'Gray	

        'Init variables	
        lastheaderrow = 0
        currentrow = 1
        lastid = "  "

        While currentrow <= SprdMain.DataRowCnt

            SprdMain.Row = currentrow
            SprdMain.Col = ColProductCode ''ColSRNo	
            Currentid = UCase(Trim(SprdMain.Text))
            '        If InStr(1, Currentid, ".") > 0 Then	
            '            Currentid = Left(Currentid, InStr(1, Currentid, ".") - 1)	
            '        End If	
            If Currentid <> lastid Then
                If lastheaderrow <> 0 Then
                    SprdMain.SetRowItemData(lastheaderrow, (SprdMain.Row - lastheaderrow - 1))
                    '                mRecordCount = SprdMain.GetRowItemData(lastheaderrow)	
                End If

                lastid = UCase(Trim(SprdMain.Text))
                '            If InStr(1, lastid, ".") > 0 Then	
                '                lastid = Left(lastid, InStr(1, lastid, ".") - 1)	
                '            End If	

                lastheaderrow = currentrow

                'Insert a new header row	
                InsertHeaderRow(currentrow, 0)
            Else
                MakePictureCellType((SprdMain.Row), ColPicSub)
                SprdMain.Col = ColPicSub
                SprdMain.TypePictPicture = minuspict
                SprdMain.SetCellBorder(ColPicSub, SprdMain.Row, ColPicSub, SprdMain.Row, SS_BORDER_TYPE_LEFT, 0, FPSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid)
            End If
            SprdMain.Row = SprdMain.Row + 1
            currentrow = currentrow + 1

        End While

        'Display last read data	
        SprdMain.SetRowItemData(lastheaderrow, (SprdMain.Row - lastheaderrow))
        SprdMain.MaxRows = SprdMain.DataRowCnt
        SprdMain.SetActiveCell(1, 1)

        'Paint Spread	
        SprdMain.Redraw = True

        'Update displays	
        System.Windows.Forms.Application.DoEvents()

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub MakePictureCellType(ByRef Row As Integer, ByRef Col As Short)
        'Define specified cell as type PICTURE	

        Exit Sub
        SprdMain.Col = Col
        SprdMain.Row = Row

        SprdMain.CellType = FPSpreadADO.CellTypeConstants.CellTypePicture
        SprdMain.TypePictCenter = True
        SprdMain.TypePictMaintainScale = False
        SprdMain.TypePictStretch = False

    End Sub

    Private Sub InsertHeaderRow(ByRef RowNum As Integer, ByRef pRecordCount As Integer)
        'Insert a header row at the specifed location	

        '    SprdMain.InsertRows rownum, 1	

        SprdMain.Col = -1
        SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) 'Gray	
        SprdMain.ForeColor = System.Drawing.ColorTranslator.FromOle(&HC00000) 'Blue	
        SprdMain.Font = VB6.FontChangeBold(SprdMain.Font, True)

        MakePictureCellType(RowNum, ColPicMain)

        SprdMain.Col = ColPicMain
        SprdMain.TypePictPicture = minuspict
        SprdMain.Col = ColPicSub
        SprdMain.Text = ""

        'Add picture state values	
        SprdMain.Col = ColFlag
        SprdMain.Text = "0"

        'Add Border	

        SprdMain.SetCellBorder(ColPicMain, RowNum, SprdMain.MaxCols, RowNum, SS_BORDER_TYPE_OUTLINE, 0, FPSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid)

    End Sub
    Private Sub BoldHeader(ByRef Col As Integer)
        'Reset the header bolds and make the sort col bold	

        'Change font for visual cue to what column sorting on	
        'Reset all header fonts	
        With SprdMain
            .Row = 0
            .Col = -1
            .Font = VB6.FontChangeBold(.Font, False)

            'Bold the specified column	
            .Row = 0
            .Col = Col
            .Font = VB6.FontChangeBold(.Font, True)
        End With
    End Sub
    Private Sub FillSubRecord(ByRef pProductCode As String, ByRef pWEF As String, ByVal pSrn As String, ByRef pLevel As Integer, ByRef pMainProductCode As String)

        On Error GoTo FillERR
        Dim SqlStr As String = ""
        Dim RsShow As ADODB.Recordset = Nothing
        Dim mRMCode As String
        Dim mDeptCode As String
        Dim mSrn As String
        Dim xSrn As String
        Dim j As Integer
        Dim pRMType As String

        mSrn = pSrn
        pLevel = pLevel + 1
        SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.RM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " ID.DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " ID.STD_QTY, ID.GROSS_WT_SCRAP," & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM,IH.ISSUE_UOM AS PROD_ISSUE_UOM, IH.OUTPUT_QTY "

        SqlStr = SqlStr & vbCrLf _
            & " FROM PRD_NEWBOM_HDR IH,PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
            & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
            & " AND ID.RM_CODE=INVMST.ITEM_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
            & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' " & vbCrLf _
            & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_NEWBOM_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
            & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "') "

        '& vbCrLf |            & " AND WEF<= '" & vb6.Format(pWEF, "DD-MMM-YYYY") & "')" & vbCrLf |	
        SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SUBROWNO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsShow.EOF Then
            Do While Not RsShow.EOF
                mRMCode = Trim(IIf(IsDBNull(RsShow.Fields("RM_CODE").Value), "", RsShow.Fields("RM_CODE").Value))
                pRMType = GetProductionType(mRMCode)
                mDeptCode = Trim(IIf(IsDBNull(RsShow.Fields("DEPT_CODE").Value), "", RsShow.Fields("DEPT_CODE").Value))
                If (mDeptCode = "SHR") And chkTillRM.Checked = False Then          ''If (pRMType = "R" Or pRMType = "4") And chkTillRM.Checked = False Then
                Else
                    mcntRow = mcntRow + 1
                    SprdMain.MaxRows = SprdMain.MaxRows + 1
                    SprdMain.Row = mcntRow

                    j = j + 1
                    xSrn = mSrn & "." & j
                    pSrn = pSrn & "." & j


                    Call FillGridCol(RsShow, xSrn, pLevel, pMainProductCode, pProductCode)
                End If
                RsShow.MoveNext()
            Loop
        Else
            SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.ITEM_CODE AS RM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " 'J/W' AS DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " ID.ITEM_QTY * DECODE(INVMST.ISSUE_UOM,'KGS',1000,DECODE(INVMST.ISSUE_UOM,'TON',1000 * 1000,1)) AS STD_QTY, ID.SCRAP_QTY * DECODE(INVMST.ISSUE_UOM,'KGS',1000,DECODE(INVMST.ISSUE_UOM,'TON',1000 * 1000,1)) AS GROSS_WT_SCRAP," & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM,IH.PRODUCT_CODE AS PROD_ISSUE_UOM, IH.OUTPUT_QTY "

            SqlStr = SqlStr & vbCrLf & " FROM PRD_OUTBOM_HDR IH,PRD_OUTBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' " & vbCrLf & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_OUTBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "') "

            SqlStr = SqlStr & vbCrLf & " AND IH.STATUS='O'"

            SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SERIAL_NO"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

            If Not RsShow.EOF Then
                Do While Not RsShow.EOF
                    mcntRow = mcntRow + 1
                    SprdMain.MaxRows = SprdMain.MaxRows + 1
                    SprdMain.Row = mcntRow

                    j = j + 1
                    xSrn = mSrn & "." & j
                    pSrn = pSrn & "." & j

                    mRMCode = Trim(IIf(IsDbNull(RsShow.Fields("RM_CODE").Value), "", RsShow.Fields("RM_CODE").Value))
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

    Private Sub cmdSubCatsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSubCatsearch.Click
        SearchSubCategory()
    End Sub

    Public Sub frmParamBOMAll_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        FormatSprdMain()
        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub frmParamBOMAll_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection	
        'PvtDBCn.Open StrConn	
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        CurrFormHeight = 7440
        CurrFormWidth = 11625

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7440)
        'Me.Width = VB6.TwipsToPixelsX(11625)

        chkAllCategory.CheckState = System.Windows.Forms.CheckState.Checked
        txtCategory.Enabled = False
        cmdsearchCategory.Enabled = False

        chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Checked
        txtSubCategory.Enabled = False
        cmdSubCatsearch.Enabled = False

        minuspict = System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\Picture\minus1.bmp")
        pluspict = System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\Picture\plus1.bmp")

        mIsGrouped = True
        Call PrintStatus(True)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub FormatSprdMain()

        Dim cntCol As Integer
        With SprdMain
            .set_RowHeight(0, RowHeight)

            .Row = -1
            .set_RowHeight(-1, RowHeight)

            .Col = ColPicMain
            .CellType = FPSpreadADO.CellTypeConstants.CellTypePicture
            .TypePictCenter = True
            .TypePictMaintainScale = False
            .TypePictStretch = False

            .Col = ColPicSub
            .CellType = FPSpreadADO.CellTypeConstants.CellTypePicture
            .TypePictCenter = True
            .TypePictMaintainScale = False
            .TypePictStretch = False

            .Col = ColProductCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColProductDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColSRNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE

            .Col = ColRMCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColRMDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .ColsFrozen = ColRMDesc


            .Col = ColProdCategory
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColRMCategory
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True


            .Col = ColPartNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColDept
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColDrgYN
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColDrgSize
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColDrgRevNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = colStdQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999.99")
            .TypeFloatMin = CDbl("-999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC

            .Col = ColScrap
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999.99")
            .TypeFloatMin = CDbl("-999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC

            .Col = ColMtrl
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColSurfTreat
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColLevel
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .ColHidden = True

            .Col = ColUom
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .ColHidden = False

            .Col = ColFlag
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .ColHidden = True

            .Col = ColItemVendor
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColProductPartNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .ColHidden = True

            .Col = ColProductModel
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .ColHidden = True

            For cntCol = ColRate To ColValue
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999.99")
                .TypeFloatMin = CDbl("-999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .ColHidden = False ''True	
            Next

            .Col = ColProdCategory
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColRMCategory
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColProdUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColProdOutputQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999.99")
            .TypeFloatMin = CDbl("-999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC

            mIsGrouped = False
            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle	

        End With

    End Sub

    Private Sub frmParamBOMAll_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))
        Frame4.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        CurrFormWidth = mReFormWidth

        '    MainClass.SetSpreadColor SprdMain, -1	
        '    MainClass.SetSpreadColor SprdOption, -1	
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamBOMAll_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call PrintBOM(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub InsertIntoPrintdummyData()

        Dim SqlStr As String = ""
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mInsertSQL As String
        Dim mValueSQL As String
        Dim mFieldStr As String
        Dim mValueStr As String

        On Error GoTo ERR1

        PubDBCn.Errors.Clear()

        PubDBCn.BeginTrans()

        SqlStr = ""
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                mInsertSQL = ""
                mValueSQL = ""
                SqlStr = ""

                mInsertSQL = "Insert into TEMP_PrintDummyData (UserID,SubRow,"
                mValueSQL = " Values (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " " & cntRow & ", "

                For cntCol = 3 To 4
                    .Col = cntCol

                    mFieldStr = "FIELD" & 22 + cntCol & ","
                    mValueStr = "'" & MainClass.AllowSingleQuote(VB.Left(.Text, 255)) & "'" & ","

                    mInsertSQL = mInsertSQL & mFieldStr
                    mValueSQL = mValueSQL & mValueStr
                Next

                For cntCol = 3 To .MaxCols
                    .Col = cntCol

                    If cntCol = .MaxCols Then
                        mFieldStr = "FIELD" & cntCol - 2
                        mValueStr = "'" & MainClass.AllowSingleQuote(VB.Left(.Text, 255)) & "'"
                    Else
                        mFieldStr = "FIELD" & cntCol - 2 & ","
                        mValueStr = "'" & MainClass.AllowSingleQuote(VB.Left(.Text, 255)) & "'" & ","
                    End If

                    mInsertSQL = mInsertSQL & mFieldStr
                    mValueSQL = mValueSQL & mValueStr


                Next
                mInsertSQL = mInsertSQL & ")"
                mValueSQL = mValueSQL & ")"

                SqlStr = mInsertSQL & vbCrLf & mValueSQL
                PubDBCn.Execute(SqlStr)
            Next
        End With
        PubDBCn.CommitTrans()
        Exit Sub
ERR1:
        'Resume	
        PubDBCn.RollbackTrans()
        MsgInformation(Err.Description)
    End Sub
    Private Sub PrintBOM(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mCustDealer As String
        Dim SqlStr As String = ""

        Report1.Reset()
        SqlStr = ""
        PubDBCn.Execute("DELETE FROM TEMP_PRINTDUMMYDATA WHERE UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'")

        MainClass.ClearCRptFormulas(Report1)

        Call InsertIntoPrintdummyData()

        '*************** Fetching Record For Report ***************************	
        SqlStr = ""
        SqlStr = "SELECT * " & vbCrLf & " FROM TEMP_PRINTDUMMYDATA " & vbCrLf & " WHERE UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf & " ORDER BY SUBROW"


        mTitle = "Bill Of Material" & IIf(chkApproved.CheckState = System.Windows.Forms.CheckState.Checked, " - APPROVED", " - NOT APPROVED")
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\BOMPrintAll.rpt"
        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Dim ii As Integer
        Dim mHeadStr As String

        '    MainClass.AssignCRptFormulas Report1, "PName=""" & lblProductDesc.text & """"	
        '    MainClass.AssignCRptFormulas Report1, "PartNo=""" & lblPartNo.text & """"	
        '    MainClass.AssignCRptFormulas Report1, "Model=""" & lblModel.text & """"	

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, True, "mnuBOM")
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call PrintBOM(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub



    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent
        'Sort on specified column or show/collapse rows	

        'Show Summary/Detail info.	
        'If clicked on a "+" or "-" grouping	

        If eventArgs.col = ColPicMain Then
            SprdMain.Col = ColPicMain
            SprdMain.Row = eventArgs.row
            If SprdMain.CellType = FPSpreadADO.CellTypeConstants.CellTypePicture Then
                'Show or hide the specified rows	
                ShowHideRows(ColPicMain, eventArgs.row)
            End If
        End If
    End Sub
    Private Sub ShowHideRows(ByRef Col As Integer, ByRef Row As Integer)
        'Collapse or uncollape the specified rows	
        Dim I As Short
        Dim collapsetype As Short

        SprdMain.Row = Row
        SprdMain.Col = ColFlag

        If SprdMain.Text = "0" Then
            collapsetype = 0 'collape/hide rows : minus picture	
            SprdMain.Col = 1
            SprdMain.TypePictPicture = pluspict
            SprdMain.Col = ColFlag
            SprdMain.Text = "1"
        Else
            collapsetype = 1 'uncollapse / show rows: plus picture	
            SprdMain.Col = 1
            SprdMain.TypePictPicture = minuspict
            SprdMain.Col = ColFlag
            SprdMain.Text = "0"
        End If

        SprdMain.Redraw = False
        For I = 1 To SprdMain.GetRowItemData(Row)
            SprdMain.Row = SprdMain.Row + 1
            If collapsetype = 0 Then
                SprdMain.RowHidden = True
            Else
                SprdMain.RowHidden = False
            End If
        Next I
        SprdMain.Redraw = True

    End Sub
    Private Function GetVendorName(ByRef pItemCode As String) As String

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        '    If MainClass.ValidateWithMasterTable() = True Then	
        '	
        '    End If	

        '    GetVendorName = ""	
        '    SqlStr = " SELECT DISTINCT SUPP_CUST_NAME " & vbCrLf _	
        ''            & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID, FIN_SUPP_CUST_MST CMST" & vbCrLf _	
        ''            & " WHERE IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _	
        ''            & " AND SUBSTR(IH.AUTO_KEY_PO,LENGTH(IH.AUTO_KEY_PO)-5,4)>=2007" & vbCrLf _	
        ''            & " AND IH.MKEY=ID.MKEY" & vbCrLf _	
        ''            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _	
        ''            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _	
        ''            & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "' AND IH.PO_STATUS='Y' AND IH.PO_CLOSED='N' AND IH.PUR_TYPE='P' AND IH.ORDER_TYPE='O' AND CMST.STATUS='O'"	
        '	
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly	

        SqlStr = " SELECT DISTINCT CMST.SUPP_CUST_NAME " & vbCrLf _
            & " FROM INV_GATE_HDR IH, INV_GATE_DET ID, FIN_SUPP_CUST_MST CMST" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUBSTR(IH.AUTO_KEY_MRR,LENGTH(IH.AUTO_KEY_MRR)-5,4) IN (" & RsCompany.Fields("FYEAR").Value & ", " & RsCompany.Fields("FYEAR").Value - 1 & ")" & vbCrLf _
            & " AND IH.AUTO_KEY_MRR=ID.AUTO_KEY_MRR" & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
            & " AND CMST.STATUS='O' " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE IN ("

        SqlStr = SqlStr & " SELECT DISTINCT IHS.SUPP_CUST_CODE " & vbCrLf _
                & " FROM PUR_PURCHASE_HDR IHS, PUR_PURCHASE_DET IDS" & vbCrLf _
                & " WHERE IHS.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
                & " AND IHS.MKEY=IDS.MKEY" & vbCrLf _
                & " AND IDS.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
                & " AND IHS.PO_STATUS='Y' AND IHS.PO_CLOSED='N' AND IHS.PUR_TYPE='P' AND IHS.ORDER_TYPE='O')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsTemp.EOF Then
            Do While RsTemp.EOF = False
                GetVendorName = IIf(GetVendorName = "", "", GetVendorName & ", ") & IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
                RsTemp.MoveNext()
            Loop
        End If
        Exit Function
ErrPart:
        GetVendorName = ""
    End Function

    Private Sub txtCategory_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCategory.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtCategory_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCategory.DoubleClick
        SearchCategory()
    End Sub
    Private Sub txtCategory_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCategory.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCategory.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCategory_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCategory.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchCategory()
    End Sub
    Private Sub txtCategory_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCategory.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""


        If txtCategory.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'"

        If MainClass.ValidateWithMasterTable(txtCategory.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtCategory.Text = UCase(Trim(txtCategory.Text))
        Else
            MsgInformation("No Such Category in Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub SearchCategory()
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'"

        If MainClass.SearchGridMaster(txtCategory.Text, "INV_GENERAL_MST", "GEN_DESC", "GEN_CODE", , , SqlStr) = True Then
            If AcName <> "" Then
                txtCategory.Text = AcName
            End If
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdSearchCategory_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchCategory.Click
        SearchCategory()
    End Sub
    Private Sub chkAllCategory_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllCategory.CheckStateChanged
        Call PrintStatus(False)
        If chkAllCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtCategory.Enabled = False
            cmdsearchCategory.Enabled = False
        Else
            txtCategory.Enabled = True
            cmdsearchCategory.Enabled = True
        End If
    End Sub

    Private Sub chkAllSubCat_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllSubCat.CheckStateChanged
        Call PrintStatus(False)
        If chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtSubCategory.Enabled = False
            cmdSubCatsearch.Enabled = False
        Else
            txtSubCategory.Enabled = True
            cmdSubCatsearch.Enabled = True
        End If
    End Sub
    Private Sub SearchSubCategory()
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mCatCode As String = ""

        If txtCategory.Text = "" Then
            MsgInformation("Please Select category .")
            txtCategory.Focus()
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(txtCategory.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            mCatCode = MasterNo
        End If
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCatCode & "'"


        If MainClass.SearchGridMaster(txtSubCategory.Text, "INV_SUBCATEGORY_MST", "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", , , SqlStr) = True Then
            If AcName <> "" Then
                txtSubCategory.Text = AcName
            End If
        End If

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub txtSubCategory_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSubCategory.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtSubCategory_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSubCategory.DoubleClick
        SearchSubCategory()
    End Sub


    Private Sub txtSubCategory_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSubCategory.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSubCategory.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSubCategory_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSubCategory.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchSubCategory()
    End Sub

    Private Sub txtSubCategory_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSubCategory.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mCatCode As String = ""

        If txtSubCategory.Text = "" Then GoTo EventExitSub

        If txtCategory.Text = "" Then
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        Else
            If MainClass.ValidateWithMasterTable(txtCategory.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , SqlStr) = True Then
                mCatCode = MasterNo
            End If
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCatCode & "'"
        End If

        If MainClass.ValidateWithMasterTable(txtSubCategory.Text, "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgInformation("No Such Sub Category ")
            Cancel = True
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
