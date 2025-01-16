Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamProdSeqBOMWise
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection	
    Private Const RowHeight As Short = 22

    Private Const ColPicMain As Short = 1
    Private Const ColPicSub As Short = 2
    Private Const ColMainProductCode As Short = 3
    Private Const ColMainProductDesc As Short = 4
    Private Const ColProductionCode As Short = 5
    Private Const ColProductionDesc As Short = 6
    Private Const ColSeqDept As Short = 7
    Private Const ColOPRCode As Short = 8
    Private Const ColOperationName As Short = 9
    Private Const ColOperationRate As Short = 10
    Private Const ColLevel As Short = 11
    Private Const ColFlag As Short = 12

    'Dim mcntRow As Long	

    Dim cntRow As Integer

    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Dim minuspict As System.Drawing.Image
    Dim pluspict As System.Drawing.Image
    Dim mIsGrouped As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        cmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub
    Private Sub chkProductionAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkProductionAll.CheckStateChanged
        Call PrintStatus(False)
        If chkProductionAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtProductDesc.Enabled = False
            cmdSearchProd.Enabled = False
        Else
            txtProductDesc.Enabled = True
            cmdSearchProd.Enabled = True
        End If
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub cmdSearchProd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchProd.Click
        SearchProduct()
    End Sub
    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        MainClass.ClearGrid(SprdMain, RowHeight)
        FormatSprdMain()

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
        Dim xProdCode As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = " SELECT IH.PRODUCT_CODE, WEF " & vbCrLf & " FROM PRD_NEWBOM_HDR IH, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND IH.PRODUCT_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If chkProductionAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtProductDesc.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                xProdCode = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND INVMST.ITEM_CODE='" & MainClass.AllowSingleQuote(xProdCode) & "'"
            End If
        End If

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

        SqlStr = SqlStr & vbCrLf & " AND IH.STATUS='O'"

        SqlStr = SqlStr & vbCrLf & " ORDER BY IH.PRODUCT_CODE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMain, ADODB.LockTypeEnum.adLockReadOnly)
        cntRow = 0

        If RsMain.EOF = False Then
            Do While Not RsMain.EOF
                mProductCode = Trim(IIf(IsDbNull(RsMain.Fields("PRODUCT_CODE").Value), "", RsMain.Fields("PRODUCT_CODE").Value))
                pWEF = Trim(IIf(IsDbNull(RsMain.Fields("WEF").Value), "", RsMain.Fields("WEF").Value))

                SqlStr = ""
                SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.RM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " ID.DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " ID.STD_QTY, ID.GROSS_WT_SCRAP, " & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM "

                SqlStr = SqlStr & vbCrLf & " FROM PRD_NEWBOM_HDR IH,PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.RM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

                SqlStr = SqlStr & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "' " & vbCrLf & " AND IH.WEF=TO_DATE('" & VB6.Format(pWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


                SqlStr = SqlStr & vbCrLf & " ORDER BY ID.PRODUCT_CODE, ID.SUBROWNO"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

                I = 0
                mLevel = 1
                If Not RsShow.EOF Then
                    Do While Not RsShow.EOF
                        Call FillGridCol(RsShow, mLevel, mProductCode, mProductCode)
                        RsShow.MoveNext()

                    Loop
                End If
                RsMain.MoveNext()
            Loop
        End If

        Call FormatSprdMain()
        GroupBySpread(ColPicMain)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '    RsShow.Cancel	
        '    RsShow.Close	
        '    Set RsShow = Nothing	
        Exit Sub
LedgError:
        MsgInformation(Err.Description)
        '    Resume	
    End Sub


    Private Sub FillGridCol(ByRef pRs As ADODB.Recordset, ByRef pLevel As Integer, ByRef pProductCode As String, ByRef pParentCode As String)
        On Error GoTo FillGERR
        Dim mRMCode As String
        Dim mDeptCode As String
        Dim mMainProductDesc As String
        Dim mProductDesc As String

        With SprdMain
            mRMCode = IIf(IsDbNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value)

            If pLevel = 1 Then
                cntRow = cntRow + 1
                .MaxRows = cntRow
                .Row = cntRow
                .Col = ColMainProductCode
                .Text = pProductCode
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                If MainClass.ValidateWithMasterTable(pProductCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
                    .Col = ColMainProductDesc
                    mMainProductDesc = MasterNo
                    .Text = mMainProductDesc
                    .FontBold = IIf(pLevel = 1, True, False)
                End If

                mDeptCode = IIf(IsDbNull(pRs.Fields("DEPT_CODE").Value), "", pRs.Fields("DEPT_CODE").Value)

                .Col = ColProductionCode
                .Text = pProductCode

                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColProductionDesc
                .Text = mMainProductDesc
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColLevel
                .Text = Str(pLevel)

                .Col = ColFlag
                .Text = "0"

                If FillOperationSeq(cntRow, pProductCode, mMainProductDesc, pProductCode, mMainProductDesc, mDeptCode) = False Then GoTo FillGERR
                pLevel = pLevel + 1
            End If

            If CheckSubRecord(mRMCode) = True Then
                cntRow = cntRow + 1
                .MaxRows = cntRow
                .Row = cntRow
                .Col = ColMainProductCode
                .Text = pProductCode
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                If MainClass.ValidateWithMasterTable(pProductCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
                    .Col = ColMainProductDesc
                    mMainProductDesc = MasterNo
                    .Text = mMainProductDesc
                    .FontBold = IIf(pLevel = 1, True, False)
                End If

                mDeptCode = IIf(IsDbNull(pRs.Fields("DEPT_CODE").Value), "", pRs.Fields("DEPT_CODE").Value)

                .Col = ColProductionCode
                .Text = mRMCode

                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColProductionDesc
                mProductDesc = IIf(IsDbNull(pRs.Fields("ITEM_SHORT_DESC").Value), "", pRs.Fields("ITEM_SHORT_DESC").Value)
                .Text = mProductDesc
                .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

                .Col = ColLevel
                .Text = Str(pLevel)

                .Col = ColFlag
                .Text = "0"

                If FillOperationSeq(cntRow, pProductCode, mMainProductDesc, mRMCode, mProductDesc, mDeptCode) = False Then GoTo FillGERR
            End If
        End With

        Call FillSubAlterRecord(mRMCode, "", pLevel, pProductCode, mDeptCode, pParentCode)
        Call FillSubRecord(mRMCode, "", pLevel, pProductCode)


        Exit Sub
FillGERR:
        '    Resume	
        MsgBox(Err.Description)
    End Sub
    Private Function FillOperationSeq(ByRef cntRow As Integer, ByRef xMainProductCode As Object, ByRef xMainProductname As Object, ByRef xProductCode As String, ByRef xProductName As String, ByRef xDeptCode As String) As Boolean

        On Error GoTo FillGERR
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsOPR As ADODB.Recordset
        Dim mSeqDept As String
        Dim mOPRCode As String
        Dim mOprRate As Double
        Dim mPackingRate As Double

        SqlStr = " SELECT IH.DEPT_CODE, IH.SERIAL_NO" & vbCrLf & " FROM PRD_PRODSEQUENCE_DET IH " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & xProductCode & "'"

        SqlStr = SqlStr & vbCrLf & " AND WEF = (" & vbCrLf & " SELECT MAX(WEF) FROM PRD_PRODSEQUENCE_DET" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE='" & xProductCode & "')"

        SqlStr = SqlStr & vbCrLf & "ORDER BY IH.SERIAL_NO "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                '            cntROW = cntROW + 1	
                SprdMain.MaxRows = cntRow
                SprdMain.Row = cntRow

                SprdMain.Col = ColMainProductCode
                SprdMain.Text = xMainProductCode

                SprdMain.Col = ColMainProductDesc
                SprdMain.Text = xMainProductname

                SprdMain.Col = ColProductionCode
                SprdMain.Text = xProductCode

                SprdMain.Col = ColProductionDesc
                SprdMain.Text = xProductName

                SprdMain.Col = ColSeqDept
                mSeqDept = IIf(IsDbNull(RsTemp.Fields("DEPT_CODE").Value), "", RsTemp.Fields("DEPT_CODE").Value)
                SprdMain.Text = mSeqDept

                SqlStr = OperationQuery(Trim(xProductCode), Trim(mSeqDept), "", "", "", "TRN.OPR_SNO", "TRN.OPR_CODE", "TO_CHAR(OPR_SNO,'00') || '-' || OPR_DESC AS OPR_DESC")

                '            SqlStr = " SELECT TRN.OPR_CODE, TO_CHAR(OPR_SNO,'00') || '-' || PMST.OPR_DESC AS OPR_DESC" & vbCrLf _	
                ''                    & " FROM PRD_OPR_TRN TRN, PRD_OPR_MST PMST " & vbCrLf _	
                ''                    & " WHERE " & vbCrLf _	
                ''                    & " TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _	
                ''                    & " AND TRN.COMPANY_CODE=PMST.COMPANY_CODE" & vbCrLf _	
                ''                    & " AND TRN.OPR_CODE=PMST.OPR_CODE" & vbCrLf _	
                ''                    & " AND TRN.DEPT_CODE='" & mSeqDept & "'" & vbCrLf _	
                ''                    & " AND TRN.PRODUCT_CODE='" & xProductCode & "'"	
                '	
                '           SqlStr = SqlStr & vbCrLf & " AND TRN.WEF = (" & vbCrLf _	
                ''                    & " SELECT MAX(WEF) FROM PRD_OPR_TRN" & vbCrLf _	
                ''                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _	
                ''                    & " AND TRN.DEPT_CODE='" & mSeqDept & "'" & vbCrLf _	
                ''                    & " AND PRODUCT_CODE='" & xProductCode & "')"	
                '	
                '           SqlStr = SqlStr & vbCrLf & "ORDER BY TRN.OPR_SNO "	
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOPR, ADODB.LockTypeEnum.adLockReadOnly)

                If RsOPR.EOF = False Then
                    Do While RsOPR.EOF = False
                        '                    cntROW = cntROW + 1	
                        SprdMain.MaxRows = cntRow
                        SprdMain.Row = cntRow

                        SprdMain.Col = ColMainProductCode
                        SprdMain.Text = xMainProductCode

                        SprdMain.Col = ColMainProductDesc
                        SprdMain.Text = xMainProductname

                        SprdMain.Col = ColProductionCode
                        SprdMain.Text = xProductCode

                        SprdMain.Col = ColProductionDesc
                        SprdMain.Text = xProductName

                        SprdMain.Col = ColSeqDept
                        SprdMain.Text = mSeqDept

                        SprdMain.Col = ColOPRCode
                        mOPRCode = IIf(IsDbNull(RsOPR.Fields("OPR_CODE").Value), "", RsOPR.Fields("OPR_CODE").Value)
                        SprdMain.Text = mOPRCode

                        SprdMain.Col = ColOperationName
                        SprdMain.Text = IIf(IsDbNull(RsOPR.Fields("OPR_DESC").Value), "", RsOPR.Fields("OPR_DESC").Value)

                        SprdMain.Col = ColOperationRate
                        mOprRate = GetOperationRate(xProductCode, mSeqDept, mOPRCode)
                        SprdMain.Text = VB6.Format(mOprRate, "0.0000") ''IIf(IsNull(RsTemp!DEPT_CODE), "", RsTemp!DEPT_CODE)	

                        RsOPR.MoveNext()
                        If RsOPR.EOF = False Then
                            cntRow = cntRow + 1
                        End If
                    Loop

                    mPackingRate = GetPackingRate("", xProductCode, VB6.Format(RunDate, "DD/MM/YYYY"))
                    If mPackingRate <> 0 Then
                        cntRow = cntRow + 1
                        SprdMain.MaxRows = cntRow
                        SprdMain.Row = cntRow

                        SprdMain.Col = ColMainProductCode
                        SprdMain.Text = xMainProductCode

                        SprdMain.Col = ColMainProductDesc
                        SprdMain.Text = xMainProductname

                        SprdMain.Col = ColProductionCode
                        SprdMain.Text = xProductCode

                        SprdMain.Col = ColProductionDesc
                        SprdMain.Text = xProductName

                        SprdMain.Col = ColSeqDept
                        SprdMain.Text = mSeqDept

                        SprdMain.Col = ColOPRCode
                        SprdMain.Text = ""

                        SprdMain.Col = ColOperationName
                        SprdMain.Text = "PACKING"

                        SprdMain.Col = ColOperationRate
                        SprdMain.Text = VB6.Format(mPackingRate, "0.0000")

                    End If
                End If
                RsTemp.MoveNext()
                If RsTemp.EOF = False Then
                    cntRow = cntRow + 1
                End If
            Loop
        Else
            SprdMain.Row = cntRow
            SprdMain.Col = ColSeqDept
            SprdMain.Text = xDeptCode
        End If

        FillOperationSeq = True
        Exit Function
FillGERR:
        '    Resume	
        MsgBox(Err.Description)
        FillOperationSeq = False
    End Function
    Private Function GetOperationRate(ByRef xProductCode As String, ByRef xDeptCode As String, ByRef xOPRCode As String) As Double

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsOprRate As ADODB.Recordset

        SqlStr = " SELECT * FROM PRD_OPR_RATE_DET " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
                & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(xProductCode) & "'" & vbCrLf _
                & " AND DEPT_CODE = '" & MainClass.AllowSingleQuote(xDeptCode) & "'" & vbCrLf _
                & " AND OPR_CODE= '" & MainClass.AllowSingleQuote(xOPRCode) & "'" & vbCrLf _
                & " AND WEF=(SELECT MAX(WEF) FROM PRD_OPR_RATE_DET " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
                & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(xProductCode) & "'" & vbCrLf _
                & " AND DEPT_CODE = '" & MainClass.AllowSingleQuote(xDeptCode) & "'" & vbCrLf _
                & " AND OPR_CODE= '" & MainClass.AllowSingleQuote(xOPRCode) & "'" & vbCrLf _
                & " AND WEF<='" & vb6.Format(RunDate, "DD-MMM-YYYY") & "')"

        SqlStr = SqlStr & vbCrLf & " ORDER BY SERIAL_NO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOprRate, ADODB.LockTypeEnum.adLockReadOnly)

        With RsOprRate
            If .EOF = False Then
                GetOperationRate = Val(IIf(IsDbNull(.Fields("SKILLED_RATE").Value), "0", .Fields("SKILLED_RATE").Value))
            End If
        End With

        Exit Function
ERR1:
        MsgBox(Err.Description)
    End Function
    Private Sub FillSubAlterRecord(ByRef pRMMainCode As String, ByRef pWEF As String, ByRef pLevel As Integer, ByRef pMainProductCode As String, ByRef pDeptCode As String, ByRef pParentCode As String)

        On Error GoTo FillERR
        Dim SqlStr As String = ""
        Dim RsShow As ADODB.Recordset = Nothing

        If pDeptCode <> "J/W" Then
            SqlStr = " SELECT DISTINCT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.ALTER_RM_CODE AS RM_CODE, '(*) - ' || INVMST.ITEM_SHORT_DESC AS ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " ID.DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " ID.ALTER_STD_QTY AS STD_QTY, ID.ALETRSCRAP AS GROSS_WT_SCRAP," & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM, MAXIMUM_QTY, MINIMUM_QTY "


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
            '        SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SUBROWNO"	
        Else

            SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.ALTER_ITEM_CODE AS RM_CODE, '(**) - ' || INVMST.ITEM_SHORT_DESC AS ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " 'J/W' AS DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " ID.ALTER_ITEM_QTY AS STD_QTY, ID.ALTER_SCRAP_QTY AS GROSS_WT_SCRAP," & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM, MAXIMUM_QTY, MINIMUM_QTY "

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

            '        SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SERIAL_NO"	
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsShow.EOF Then
            Do While Not RsShow.EOF
                Call FillGridCol(RsShow, pLevel, pMainProductCode, pRMMainCode)
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
    Private Function CheckSubRecord(ByRef pProductCode As String) As Boolean


        On Error GoTo FillERR
        Dim SqlStr As String = ""
        Dim RsShow As ADODB.Recordset = Nothing

        CheckSubRecord = False

        SqlStr = " SELECT " & vbCrLf _
            & " IH.PRODUCT_CODE, ID.RM_CODE " & vbCrLf _
            & " FROM PRD_NEWBOM_HDR IH,PRD_NEWBOM_DET ID" & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY " & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
            & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' " & vbCrLf _
            & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_NEWBOM_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
            & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "') "

        '& vbCrLf |            & " AND WEF<= '" & vb6.Format(pWEF, "DD-MMM-YYYY") & "')" & vbCrLf |	
        SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SUBROWNO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsShow.EOF Then
            '        Do While Not RsShow.EOF	
            '           mRMCode = Trim(IIf(IsNull(RsShow!RM_CODE), "", RsShow!RM_CODE))	
            CheckSubRecord = True
            '        Loop	
        Else

            SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, ID.ITEM_CODE AS RM_CODE " & vbCrLf & " FROM PRD_OUTBOM_HDR IH,PRD_OUTBOM_DET ID" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' " & vbCrLf & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_OUTBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "') "

            SqlStr = SqlStr & vbCrLf & " AND IH.STATUS='O'"

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
    Private Sub GroupBySpread(ByRef Col As Integer)
        'Group the data by the specified column	
        Dim I As Short
        Dim currentrow As Integer
        Dim lastid As String
        'Dim prevtext As Object
        Dim lastheaderrow As Integer
        'Dim ret As Boolean
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
            SprdMain.Col = ColMainProductCode ''ColSRNo	
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

    Private Sub InsertHeaderRow(ByRef rownum As Integer, ByRef pRecordCount As Integer)
        'Insert a header row at the specifed location	

        '    SprdMain.InsertRows rownum, 1	

        SprdMain.Col = -1
        SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) 'Gray	
        SprdMain.ForeColor = System.Drawing.ColorTranslator.FromOle(&HC00000) 'Blue	
        SprdMain.Font = VB6.FontChangeBold(SprdMain.Font, True)

        MakePictureCellType(rownum, ColPicMain)

        SprdMain.Col = ColPicMain
        SprdMain.TypePictPicture = minuspict
        SprdMain.Col = ColPicSub
        SprdMain.Text = ""

        'Add picture state values	
        SprdMain.Col = ColFlag
        SprdMain.Text = "0"

        'Add Border	

        SprdMain.SetCellBorder(ColPicMain, rownum, SprdMain.MaxCols, rownum, SS_BORDER_TYPE_OUTLINE, 0, FPSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid)

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
    Private Sub FillSubRecord(ByRef pProductCode As String, ByRef pWEF As String, ByRef pLevel As Integer, ByRef pMainProductCode As String)

        On Error GoTo FillERR
        Dim SqlStr As String = ""
        Dim RsShow As ADODB.Recordset = Nothing
        Dim mRMCode As String

        pLevel = pLevel + 1
        SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.RM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " ID.DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " ID.STD_QTY, ID.GROSS_WT_SCRAP," & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM "

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
                Call FillGridCol(RsShow, pLevel, pMainProductCode, pProductCode)
                RsShow.MoveNext()
            Loop
        Else
            SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.ITEM_CODE AS RM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " 'J/W' AS DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " ID.ITEM_QTY AS STD_QTY, ID.SCRAP_QTY AS GROSS_WT_SCRAP," & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM "


            SqlStr = SqlStr & vbCrLf & " FROM PRD_OUTBOM_HDR IH,PRD_OUTBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' " & vbCrLf & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_OUTBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "') "

            SqlStr = SqlStr & vbCrLf & " AND IH.STATUS='O'"

            SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SERIAL_NO"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

            If Not RsShow.EOF Then
                Do While Not RsShow.EOF
                    Call FillGridCol(RsShow, pLevel, pMainProductCode, pProductCode)
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

    Public Sub frmParamProdSeqBOMWise_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        FormatSprdMain()
        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub frmParamProdSeqBOMWise_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection	
        'PvtDBCn.Open StrConn	
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Top = VB6.TwipsToPixelsY(24)
        Me.Left = VB6.TwipsToPixelsX(24)
        Me.Height = VB6.TwipsToPixelsY(7440)
        Me.Width = VB6.TwipsToPixelsX(11625)

        chkProductionAll.CheckState = System.Windows.Forms.CheckState.Checked
        txtProductDesc.Enabled = False
        cmdSearchProd.Enabled = False

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

            .Col = ColMainProductCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .ColHidden = True

            .Col = ColMainProductDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .ColHidden = True

            .Col = ColProductionCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColProductionDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .ColsFrozen = ColProductionDesc

            .Col = ColSeqDept
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColOPRCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColOperationName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColOperationRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999.99")
            .TypeFloatMin = CDbl("-999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC

            .Col = ColLevel
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .ColHidden = True

            .Col = ColFlag
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .ColHidden = True

            mIsGrouped = False
            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle	

        End With

    End Sub

    Private Sub frmParamProdSeqBOMWise_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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

        mTitle = Me.Text

        If chkProductionAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mSubTitle = mSubTitle & " [Product : " & txtProductDesc.Text & "]"
        End If

        If chkAllCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mSubTitle = mSubTitle & " [Category : " & txtCategory.Text & "]"
        End If

        If chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mSubTitle = mSubTitle & " [SubCategory : " & txtSubCategory.Text & "]"
        End If

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\ProdSeqBOMWise.rpt"

        If MainClass.FillPrintDummyDataFromSprd(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols, PubDBCn) = False Then GoTo ERR1


        SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        'Dim ii As Integer
        'Dim mHeadStr As String

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
    Private Sub SearchProduct()
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster(txtProductDesc.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr) = True Then
            If AcName <> "" Then
                txtProductDesc.Text = AcName
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

    Private Sub txtProductDesc_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProductDesc.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtProductDesc_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProductDesc.DoubleClick
        SearchProduct()
    End Sub


    Private Sub txtProductDesc_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtProductDesc.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtProductDesc.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtProductDesc_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtProductDesc.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchProduct()
    End Sub

    Private Sub txtProductDesc_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtProductDesc.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""


        If txtProductDesc.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable(txtProductDesc.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtProductDesc.Text = UCase(Trim(txtProductDesc.Text))
        Else
            MsgInformation("No Such Item in Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
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
