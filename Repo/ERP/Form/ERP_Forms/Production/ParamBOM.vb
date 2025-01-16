Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamBOM
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection	
    Private Const RowHeight As Short = 22

    Private Const ColPicMain As Short = 1
    Private Const ColPicSub As Short = 2
    Private Const ColSRNo As Short = 3
    Private Const ColProdCode As Short = 4
    Private Const ColProdDesc As Short = 5
    Private Const ColWEF As Short = 6
    Private Const ColAmend As Short = 7
    Private Const ColRMCode As Short = 8
    Private Const ColRMDesc As Short = 9
    Private Const ColPartNo As Short = 10
    Private Const ColDept As Short = 11
    Private Const ColDrgYN As Short = 12
    Private Const ColDrgSize As Short = 13
    Private Const ColDrgRevNo As Short = 14
    Private Const colStdQty As Short = 15
    Private Const ColMtrl As Short = 16
    Private Const ColItemVendor As Short = 17
    Private Const ColSurfTreat As Short = 18
    Private Const ColLevel As Short = 19
    Private Const ColUom As Short = 20
    Private Const ColFlag As Short = 21
    Private Const ColScrap As Short = 22
    Private Const ColRate As Short = 23
    Private Const ColProcessRate As Short = 24
    Private Const ColValue As Short = 25
    Private Const ColCategory As Short = 26

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


    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub cmdSearchProdCode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchProdCode.Click
        Dim SqlStr As String = ""
        '    If MainClass.SearchGridMaster("", "PRD_NEWBOM_HDR", "PRODUCT_CODE", "WEF", "", "", " COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then	
        '        txtProductCode.Text = AcName	
        '        txtWEF.Text = AcName1	
        '    End If	
        '	
        SqlStr = " SELECT IH.PRODUCT_CODE,IH.WEF, INV.ITEM_SHORT_DESC,CUSTOMER_PART_NO " & vbCrLf _
            & " FROM PRD_NEWBOM_HDR IH, INV_ITEM_MST INV " & vbCrLf _
            & " WHERE IH.COMPANY_CODE=INV.COMPANY_CODE " & vbCrLf _
            & " AND IH.PRODUCT_CODE=INV.ITEM_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If Trim(txtProductCode.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND IH.PRODUCT_CODE LIKE '" & Trim(txtProductCode.Text) & "%'"
        End If

        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtWEF.Text = AcName1
            txtProductCode.Text = AcName
            '        If txtItemCode.Enabled = True Then txtItemCode.SetFocus
        End If

    End Sub

    Private Sub cmdSearchWEF_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchWEF.Click

        Dim SqlStr As String = ""

        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If Trim(txtProductCode.Text) <> "" Then
            SqlStr = SqlStr & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "' "
        End If

        If MainClass.SearchGridMaster("", "PRD_NEWBOM_HDR", "PRODUCT_CODE", "TO_CHAR(WEF,'DD/MM/YYYY')", "AMEND_NO", "", SqlStr) = True Then
            txtProductCode.Text = AcName
            txtWEF.Text = AcName1
        End If
    End Sub

    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        MainClass.ClearGrid(SprdMain, RowHeight)
        FormatSprdMain()

        If Trim(txtWEF.Text) = "" Then
            MsgInformation("Please Enter Date.")
            txtWEF.Focus()
            Exit Sub
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        lblProductCode.Text = ""
        chkApproved.CheckState = System.Windows.Forms.CheckState.Unchecked
        Show1()
        lblProductCode.Text = Trim(txtProductCode.Text)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        SprdMain.Focus()
        Call PrintStatus(True)
    End Sub
    Private Sub Show1()

        On Error GoTo LedgError
        Dim RsShow As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mRMCode As String
        Dim i As Integer
        Dim mSrn As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim pSqlStr As String
        Dim mLevel As Integer
        Dim mApproved As String
        Dim mProductCode As String = ""
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If Trim(txtWEF.Text) = "" Or Trim(txtProductCode.Text) = "" Then Exit Sub
        If IsDate(txtWEF.Text) = False Then
            MsgBox("Invalid Date")
            Exit Sub
        End If

        SqlStr = ""
        SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.RM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " ID.DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " ID.STD_QTY, ID.GROSS_WT_SCRAP, " & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM,STATUS,OUTPUT_QTY,IS_APPROVED,AMEND_NO "

        SqlStr = SqlStr & vbCrLf & " FROM PRD_NEWBOM_HDR IH,PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.RM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        SqlStr = SqlStr & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "' " & vbCrLf & " AND IH.WEF=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " ORDER BY IH.PRODUCT_CODE, ID.SUBROWNO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)
        mcntRow = 0
        i = 0

        If Not RsShow.EOF Then
            mProductCode = RsShow.Fields("PRODUCT_CODE").Value
            pSqlStr = "SELECT ITEM_SHORT_DESC, CUSTOMER_PART_NO, ITEM_MODEL, ISSUE_UOM" & vbCrLf _
                & " FROM INV_ITEM_MST " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'"

            MainClass.UOpenRecordSet(pSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                lblProductDesc.Text = IIf(IsDBNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value)
                lblPartNo.Text = IIf(IsDBNull(RsTemp.Fields("CUSTOMER_PART_NO").Value), "", RsTemp.Fields("CUSTOMER_PART_NO").Value)
                lblModel.Text = IIf(IsDBNull(RsTemp.Fields("ITEM_MODEL").Value), "", RsTemp.Fields("ITEM_MODEL").Value)
                lblUOM.Text = IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)

            End If
            lblAmendNo.Text = IIf(IsDBNull(RsShow.Fields("AMEND_NO").Value), "O", RsShow.Fields("AMEND_NO").Value)
            lblStatus.Text = IIf(IsDBNull(RsShow.Fields("Status").Value), "O", RsShow.Fields("Status").Value)
            lblOutputQty.Text = VB6.Format(IIf(IsDBNull(RsShow.Fields("OUTPUT_QTY").Value), 0, RsShow.Fields("OUTPUT_QTY").Value), "0.00")
            mApproved = IIf(IsDBNull(RsShow.Fields("IS_APPROVED").Value), "N", RsShow.Fields("IS_APPROVED").Value)
            chkApproved.CheckState = IIf(mApproved = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            Do While Not RsShow.EOF
                mcntRow = mcntRow + 1
                i = i + 1
                SprdMain.Row = mcntRow

                mSrn = Str(i)

                mRMCode = Trim(IIf(IsDBNull(RsShow.Fields("RM_CODE").Value), "", RsShow.Fields("RM_CODE").Value))
                mLevel = 1
                Call FillGridCol(RsShow, mSrn, mLevel, Trim(txtProductCode.Text), Trim(txtProductCode.Text), "")

                SprdMain.MaxRows = SprdMain.MaxRows + 1
                RsShow.MoveNext()
            Loop
        End If
        Call FormatSprdMain()
        GroupBySpread(ColPicMain)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        RsShow.Cancel()
        RsShow.Close()
        RsShow = Nothing
        Exit Sub
LedgError:
        MsgInformation(Err.Description)
    End Sub
    Private Sub FillGridCol(ByRef pRs As ADODB.Recordset, ByRef pSRNo As String, ByRef pLevel As Integer, ByRef pProductCode As String, ByRef pParentCode As String, ByRef pIsAlterItem As String)
        On Error GoTo FillGERR
        Dim mDrgRevNo As String
        Dim mRMCode As String
        Dim mDeptCode As String

        Dim mRM_PURCHASE_COST As Double
        Dim mRM_LANDED_COST As Double
        Dim mRMUOM As String
        Dim mFactor As Double
        Dim mStatus As String
        Dim mRate As Double
        Dim mValue As Double
        Dim mProcessRate As Double
        'Dim mDeptCode As String	
        Dim mUOM As String

        With SprdMain
            mStatus = IIf(IsDbNull(pRs.Fields("Status").Value), "O", pRs.Fields("Status").Value)
            If lblStatus.Text = "O" Then
                If mStatus = "C" Then GoTo NextRow
            End If

            .Col = ColSRNo
            .Text = pSRNo
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))
            If pIsAlterItem = "A" Then
                .ForeColor = System.Drawing.ColorTranslator.FromOle(&HC0)
            End If
            mDeptCode = IIf(IsDbNull(pRs.Fields("DEPT_CODE").Value), "", pRs.Fields("DEPT_CODE").Value)

            .Col = ColProdCode
            .Text = Trim(txtProductCode.Text)
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))
            If pIsAlterItem = "A" Then
                .ForeColor = System.Drawing.ColorTranslator.FromOle(&HC0)
            End If

            .Col = ColProdDesc
            .Text = Trim(lblProductDesc.Text)
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))
            If pIsAlterItem = "A" Then
                .ForeColor = System.Drawing.ColorTranslator.FromOle(&HC0)
            End If

            .Col = ColWEF
            .Text = Trim(txtWEF.Text)
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))
            If pIsAlterItem = "A" Then
                .ForeColor = System.Drawing.ColorTranslator.FromOle(&HC0)
            End If

            .Col = ColAmend
            .Text = Trim(lblAmendNo.Text)
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))
            If pIsAlterItem = "A" Then
                .ForeColor = System.Drawing.ColorTranslator.FromOle(&HC0)
            End If

            .Col = ColRMCode
            .Text = IIf(IsDbNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value)
            mRMCode = Trim(IIf(IsDbNull(pRs.Fields("RM_CODE").Value), "", pRs.Fields("RM_CODE").Value))
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))
            If pIsAlterItem = "A" Then
                .ForeColor = System.Drawing.ColorTranslator.FromOle(&HC0)
            End If

            .Col = ColRMDesc
            .Text = IIf(IsDbNull(pRs.Fields("ITEM_SHORT_DESC").Value), "", pRs.Fields("ITEM_SHORT_DESC").Value)
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))
            If pIsAlterItem = "A" Then
                .ForeColor = System.Drawing.ColorTranslator.FromOle(&HC0)
            End If

            .Col = ColPartNo
            .Text = IIf(IsDbNull(pRs.Fields("CUSTOMER_PART_NO").Value), "", pRs.Fields("CUSTOMER_PART_NO").Value)
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))
            If pIsAlterItem = "A" Then
                .ForeColor = System.Drawing.ColorTranslator.FromOle(&HC0)
            End If

            .Col = ColCategory
            .Text = GetItemCategory(mRMCode)
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))
            If pIsAlterItem = "A" Then
                .ForeColor = System.Drawing.ColorTranslator.FromOle(&HC0)
            End If

            .Col = ColDept
            .Text = IIf(IsDbNull(pRs.Fields("DEPT_CODE").Value), "", pRs.Fields("DEPT_CODE").Value)
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))
            If pIsAlterItem = "A" Then
                .ForeColor = System.Drawing.ColorTranslator.FromOle(&HC0)
            End If

            mDrgRevNo = IIf(IsDbNull(pRs.Fields("DRW_REVNO").Value), "", pRs.Fields("DRW_REVNO").Value)
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

            .Col = ColDrgYN
            .Text = IIf(mDrgRevNo = "", "No", "Yes")
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))
            If pIsAlterItem = "A" Then
                .ForeColor = System.Drawing.ColorTranslator.FromOle(&HC0)
            End If

            .Col = ColDrgSize
            .Text = ""
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))
            If pIsAlterItem = "A" Then
                .ForeColor = System.Drawing.ColorTranslator.FromOle(&HC0)
            End If

            .Col = ColDrgRevNo
            .Text = IIf(IsDbNull(pRs.Fields("DRW_REVNO").Value), "", pRs.Fields("DRW_REVNO").Value)
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))
            If pIsAlterItem = "A" Then
                .ForeColor = System.Drawing.ColorTranslator.FromOle(&HC0)
            End If

            .Col = colStdQty
            .Text = CStr(Val(IIf(IsDbNull(pRs.Fields("STD_QTY").Value), "", pRs.Fields("STD_QTY").Value)))
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))
            If pIsAlterItem = "A" Then
                .ForeColor = System.Drawing.ColorTranslator.FromOle(&HC0)
            End If

            .Col = ColMtrl
            .Text = IIf(IsDbNull(pRs.Fields("ITEM_TECH_DESC").Value), "", pRs.Fields("ITEM_TECH_DESC").Value)
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))
            If pIsAlterItem = "A" Then
                .ForeColor = System.Drawing.ColorTranslator.FromOle(&HC0)
            End If

            .Col = ColSurfTreat

            '        mRMUOM = IIf(IsNull(pRs!ISSUE_UOM), "", pRs!ISSUE_UOM)	
            '        If MainClass.ValidateWithMasterTable(mRMCode, "ITEM_CODE", "UOM_FACTOR", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then	
            '            mFactor = MasterNo	
            '        End If	
            '        If GetLatestItemCostFromPO(mRMCode, mRM_PURCHASE_COST, mRM_LANDED_COST, Format(PubCurrDate, "DD/MM/YYYY"), "ST", "-1", mRMUOM, mFactor) = False Then GoTo FillGERR	
            '        .Text = Format(mRM_PURCHASE_COST, "0.0000")	

            .Text = Str(IIf(IsDbNull(pRs.Fields("ITEM_SURFACE_AREA").Value), 0, Trim(pRs.Fields("ITEM_SURFACE_AREA").Value)))
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))

            .Col = ColItemVendor
            If chkVendor.CheckState = System.Windows.Forms.CheckState.Checked Then
                .Text = GetVendorName(mRMCode)
            Else
                .Text = ""
            End If
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))
            If pIsAlterItem = "A" Then
                .ForeColor = System.Drawing.ColorTranslator.FromOle(&HC0)
            End If

            .Col = ColLevel
            .Text = Str(pLevel)
            If pIsAlterItem = "A" Then
                .ForeColor = System.Drawing.ColorTranslator.FromOle(&HC0)
            End If

            .Col = ColUom
            .Text = IIf(IsDbNull(pRs.Fields("ISSUE_UOM").Value), "", pRs.Fields("ISSUE_UOM").Value)
            mUOM = IIf(IsDbNull(pRs.Fields("ISSUE_UOM").Value), "", pRs.Fields("ISSUE_UOM").Value)
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))
            If pIsAlterItem = "A" Then
                .ForeColor = System.Drawing.ColorTranslator.FromOle(&HC0)
            End If

            .Col = ColFlag
            .Text = "0"
            If pIsAlterItem = "A" Then
                .ForeColor = System.Drawing.ColorTranslator.FromOle(&HC0)
            End If

            .Col = ColScrap
            .Text = CStr(Val(IIf(IsDbNull(pRs.Fields("GROSS_WT_SCRAP").Value), "", pRs.Fields("GROSS_WT_SCRAP").Value)))
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))
            If pIsAlterItem = "A" Then
                .ForeColor = System.Drawing.ColorTranslator.FromOle(&HC0)
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
                    mRate = mRate
                    If mUOM = "KGS" Then
                        mRate = mRate / 1000
                    ElseIf mUOM = "TON" Then
                        mRate = mRate / 1000
                        mRate = mRate / 1000
                    End If
                    mValue = mRate * Val(IIf(IsDbNull(pRs.Fields("STD_QTY").Value), 0, pRs.Fields("STD_QTY").Value))
                End If
            Else
                mValue = 0
            End If

            .Col = ColValue
            .Text = VB6.Format(mValue, "0.00")
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))
            If pIsAlterItem = "A" Then
                .ForeColor = System.Drawing.ColorTranslator.FromOle(&HC0)
            End If

            .Col = ColRate
            .Text = VB6.Format(mRate, "0.00")
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))
            If pIsAlterItem = "A" Then
                .ForeColor = System.Drawing.ColorTranslator.FromOle(&HC0)
            End If

            .Col = ColProcessRate
            .Text = VB6.Format(mProcessRate, "0.00")
            .Font = VB6.FontChangeBold(.Font, IIf(pLevel = 1, True, False))
            If pIsAlterItem = "A" Then
                .ForeColor = System.Drawing.ColorTranslator.FromOle(&HC0)
            End If
        End With
NextRow:
        '    If pLevel > 1 Then	
        '        pRs.MoveNext	
        '        If pRs.EOF = False Then	
        '            mRMCode = IIf(IsNull(pRs!RM_CODE), "", pRs!RM_CODE)	
        '        Else	
        '            mRMCode = "-1"	
        '        End If	
        '    End If	

        Call FillSubAlterRecord(mRMCode, "", pSRNo, pLevel, pProductCode, mDeptCode, pParentCode)
        Call FillSubRecord(mRMCode, (txtWEF.Text), pSRNo, pLevel, pProductCode, pIsAlterItem)

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
        Dim mStatus As String

        mSrn = pSrn
        '    pLevel = pLevel + 1	

        'If pDeptCode <> "J/W" Then
        SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.ALTER_RM_CODE AS RM_CODE, '(*) - ' || INVMST.ITEM_SHORT_DESC AS ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " ID.DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " ID.ALTER_STD_QTY AS STD_QTY, ID.ALETRSCRAP AS GROSS_WT_SCRAP," & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM, MAXIMUM_QTY, MINIMUM_QTY,STATUS "

        SqlStr = SqlStr & vbCrLf _
            & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET IDET, PRD_BOM_ALTER_DET ID, INV_ITEM_MST INVMST" & vbCrLf _
            & " WHERE IH.MKEY=IDET.MKEY " & vbCrLf _
            & " AND IDET.MKEY=ID.MKEY " & vbCrLf _
            & " AND IDET.RM_CODE=ID.MAINITEM_CODE " & vbCrLf _
            & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
            & " AND ID.ALTER_RM_CODE=INVMST.ITEM_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND ID.MAINITEM_CODE='" & MainClass.AllowSingleQuote(pRMMainCode) & "'" & vbCrLf _
            & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pParentCode) & "'" & vbCrLf _
            & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_NEWBOM_HDR " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pParentCode) & "') "

        '& vbCrLf |                & " AND WEF<= '" & vb6.Format(pWEF, "DD-MMM-YYYY") & "')" & vbCrLf |	
        ''CHANGE 'pMainProductCode' with 'pParentCode' on dated .. 16-10-2012 'F00016 (Unit VIII) Alter Code not Come'.	


        '        SqlStr = SqlStr & vbCrLf & " AND IH.STATUS='O'"	
        SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SUBROWNO"
        'Else

        '    SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.ALTER_ITEM_CODE AS RM_CODE, '(**) - ' || INVMST.ITEM_SHORT_DESC AS ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " 'J/W' AS DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " ID.ALTER_ITEM_QTY * DECODE(INVMST.ISSUE_UOM,'KGS',1000,DECODE(INVMST.ISSUE_UOM,'TON',1000 * 1000,1)) AS STD_QTY, ID.ALTER_SCRAP_QTY * DECODE(INVMST.ISSUE_UOM,'KGS',1000,DECODE(INVMST.ISSUE_UOM,'TON',1000 * 1000,1)) AS GROSS_WT_SCRAP," & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM, MAXIMUM_QTY, MINIMUM_QTY,STATUS "

        '    SqlStr = SqlStr & vbCrLf _
        '        & " FROM PRD_OUTBOM_HDR IH, PRD_OUTBOM_ALTER_DET ID, INV_ITEM_MST INVMST" & vbCrLf _
        '        & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
        '        & " and ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
        '        & " AND ID.ALTER_ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf _
        '        & " AND ID.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
        '        & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pRMMainCode) & "' " & vbCrLf _
        '        & " AND ID.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pParentCode) & "' " & vbCrLf _
        '        & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_OUTBOM_HDR " & vbCrLf _
        '        & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
        '        & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pParentCode) & "') "

        '    SqlStr = SqlStr & vbCrLf & " AND IH.IS_INHOUSE='N' AND IH.STATUS='O'"

        '    SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SERIAL_NO"
        'End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsShow.EOF Then
            Do While Not RsShow.EOF
                mStatus = Trim(IIf(IsDbNull(RsShow.Fields("Status").Value), "", RsShow.Fields("Status").Value))
                mStatus = IIf(lblStatus.Text = "C", "O", mStatus)
                If mStatus = "O" Then ''Or lblStatus.text = "C"	
                    mcntRow = mcntRow + 1
                    SprdMain.MaxRows = SprdMain.MaxRows + 1
                    SprdMain.Row = mcntRow

                    xSrn = mSrn
                    pSrn = pSrn
                End If

                mRMCode = Trim(IIf(IsDbNull(RsShow.Fields("RM_CODE").Value), "", RsShow.Fields("RM_CODE").Value))
                Call FillGridCol(RsShow, xSrn, pLevel, pMainProductCode, pRMMainCode, "A")
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
        Dim i As Short
        Dim currentrow As Integer
        Dim lastid As String
        Dim prevtext As Object
        Dim lastheaderrow As Integer
        Dim ret As Boolean
        Dim Currentid As String


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        SprdMain.Redraw = False
        BoldHeader(Col)

        '    SprdMain.MaxCols = SprdMain.MaxCols + 2	
        'Insert 2 columns at beginning	
        For i = 1 To 2
            '        SprdMain.InsertCols i, 1	

            'Change col width	
            SprdMain.set_ColWidth(i, 2)
        Next i

        SprdMain.Col = ColPicMain
        SprdMain.Row = -1
        SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) 'Gray	

        'Init variables	
        lastheaderrow = 0
        currentrow = 1
        lastid = "  "

        While currentrow <= SprdMain.DataRowCnt

            SprdMain.Row = currentrow
            SprdMain.Col = ColSRNo
            Currentid = UCase(Trim(SprdMain.Text))
            If InStr(1, Currentid, ".") > 0 Then
                Currentid = VB.Left(Currentid, InStr(1, Currentid, ".") - 1)
            End If
            If Currentid <> lastid Then
                If lastheaderrow <> 0 Then
                    SprdMain.SetRowItemData(lastheaderrow, (SprdMain.Row - lastheaderrow - 1))
                    '                mRecordCount = SprdMain.GetRowItemData(lastheaderrow)	
                End If

                lastid = UCase(Trim(SprdMain.Text))
                If InStr(1, lastid, ".") > 0 Then
                    lastid = VB.Left(lastid, InStr(1, lastid, ".") - 1)
                End If

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
    Private Sub FillSubRecord(ByRef pProductCode As String, ByRef pWEF As String, ByVal pSrn As String, ByRef pLevel As Integer, ByRef pMainProductCode As String, ByRef pIsAlterItem As String)

        On Error GoTo FillERR
        Dim SqlStr As String = ""
        Dim RsShow As ADODB.Recordset = Nothing
        Dim mRMCode As String
        Dim mSrn As String
        Dim xSrn As String
        Dim j As Integer
        Dim mStatus As String

        mSrn = pSrn
        pLevel = pLevel + 1
        SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.RM_CODE, CASE WHEN '" & pIsAlterItem & "'='A' THEN '(*) - ' ELSE '' END || INVMST.ITEM_SHORT_DESC AS ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " ID.DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " ID.STD_QTY, ID.GROSS_WT_SCRAP," & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM,STATUS "

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
        '    SqlStr = SqlStr & vbCrLf & " AND IH.STATUS='O'"	
        SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SUBROWNO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsShow.EOF Then
            Do While Not RsShow.EOF
                mStatus = Trim(IIf(IsDbNull(RsShow.Fields("Status").Value), "", RsShow.Fields("Status").Value))
                mStatus = IIf(lblStatus.Text = "C", "O", mStatus)
                If mStatus = "O" Then ''Or lblStatus.text = "O"	
                    mcntRow = mcntRow + 1
                    SprdMain.MaxRows = SprdMain.MaxRows + 1
                    SprdMain.Row = mcntRow

                    j = j + 1
                    xSrn = mSrn & "." & j
                    pSrn = pSrn & "." & j
                End If

                mRMCode = Trim(IIf(IsDbNull(RsShow.Fields("RM_CODE").Value), "", RsShow.Fields("RM_CODE").Value))
                Call FillGridCol(RsShow, xSrn, pLevel, pMainProductCode, pProductCode, pIsAlterItem)
                RsShow.MoveNext()
            Loop
        End If

        SqlStr = " SELECT " & vbCrLf & " IH.PRODUCT_CODE, " & vbCrLf & " ID.ITEM_CODE AS RM_CODE, CASE WHEN '" & pIsAlterItem & "'='A' THEN '(*) - ' ELSE '' END || INVMST.ITEM_SHORT_DESC AS ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " 'J/W' AS DEPT_CODE, INVMST.DRW_REVNO, INVMST.ITEM_SURFACE_AREA, " & vbCrLf & " ID.ITEM_QTY * DECODE(INVMST.ISSUE_UOM,'KGS',1000,DECODE(INVMST.ISSUE_UOM,'TON',1000 * 1000,1)) AS STD_QTY, ID.SCRAP_QTY * DECODE(INVMST.ISSUE_UOM,'KGS',1000,DECODE(INVMST.ISSUE_UOM,'TON',1000 * 1000,1)) AS GROSS_WT_SCRAP," & vbCrLf & " INVMST.ITEM_TECH_DESC, INVMST.ISSUE_UOM,STATUS "

        SqlStr = SqlStr & vbCrLf & " FROM PRD_OUTBOM_HDR IH,PRD_OUTBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' " & vbCrLf & " AND IH.WEF=(SELECT MAX(WEF) FROM PRD_OUTBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "') "

        SqlStr = SqlStr & vbCrLf & " AND IH.IS_INHOUSE='N' AND IH.STATUS='O'"

        SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SERIAL_NO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsShow.EOF Then
            Do While Not RsShow.EOF
                mStatus = Trim(IIf(IsDbNull(RsShow.Fields("Status").Value), "", RsShow.Fields("Status").Value))
                mStatus = IIf(lblStatus.Text = "C", "O", mStatus)
                If mStatus = "O" Then ''Or lblStatus.text = "O"	
                    mcntRow = mcntRow + 1
                    SprdMain.MaxRows = SprdMain.MaxRows + 1
                    SprdMain.Row = mcntRow

                    j = j + 1
                    xSrn = mSrn & "." & j
                    pSrn = pSrn & "." & j
                End If
                mRMCode = Trim(IIf(IsDbNull(RsShow.Fields("RM_CODE").Value), "", RsShow.Fields("RM_CODE").Value))
                Call FillGridCol(RsShow, xSrn, pLevel, pMainProductCode, pProductCode, pIsAlterItem)
                RsShow.MoveNext()
            Loop
        End If
        '    End If	
        Exit Sub
FillERR:
        MsgBox(Err.Description)
        '    Resume	
    End Sub

    Public Sub frmParamBOM_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        FormatSprdMain()
        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub frmParamBOM_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        minuspict = System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\Picture\minus1.bmp")
        pluspict = System.Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\Picture\plus1.bmp")

        txtMainCode.Enabled = False
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

            .Col = ColSRNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE

            .Col = ColProdCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .ColHidden = True

            .Col = ColProdDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .ColHidden = True

            .Col = ColWEF
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .ColHidden = True

            .Col = ColAmend
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .ColHidden = True

            .Col = ColRMCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColCategory
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .ColsFrozen = ColCategory

            .Col = ColRMDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .ColsFrozen = ColRMDesc

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
            '        .Col = ColRate	
            '        .ColHidden = True	
            '	
            '        .Col = ColProcessRate	
            '        .ColHidden = True	
            '	
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

            mIsGrouped = False
            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal

        End With

    End Sub

    Private Sub frmParamBOM_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
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

    Private Sub frmParamBOM_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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


                For cntCol = 1 To .MaxCols
                    .Col = cntCol

                    If cntCol = .MaxCols Then
                        mFieldStr = "FIELD" & cntCol
                        mValueStr = "'" & MainClass.AllowSingleQuote(VB.Left(.Text, 255)) & "'"
                    Else
                        mFieldStr = "FIELD" & cntCol & ","
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


        mTitle = "Bill Of Material" & IIf(chkApproved.CheckState = System.Windows.Forms.CheckState.Checked, " (APPROVED)", " (NOT APPROVED)")
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\FGBOMPrint.rpt"
        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)

        Dim ii As Integer
        Dim mHeadStr As String

        MainClass.AssignCRptFormulas(Report1, "PName=""" & lblProductDesc.Text & """")
        MainClass.AssignCRptFormulas(Report1, "PartNo=""" & lblPartNo.Text & """")
        MainClass.AssignCRptFormulas(Report1, "Model=""" & lblModel.Text & """")

        MainClass.AssignCRptFormulas(Report1, "PCode=""" & txtProductCode.Text & """")


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
        Dim i As Short
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
        For i = 1 To SprdMain.GetRowItemData(Row)
            SprdMain.Row = SprdMain.Row + 1
            If collapsetype = 0 Then
                SprdMain.RowHidden = True
            Else
                SprdMain.RowHidden = False
            End If
        Next i
        SprdMain.Redraw = True

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
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim xMkey As String = ""
        Dim mMainItemCode As String

        If Trim(txtProductCode.Text) = "" Then GoTo EventExitSub

        mMainItemCode = GetMainItemCode(Trim(txtProductCode.Text))
        txtMainCode.Text = mMainItemCode

        If Trim(txtProductCode.Text) <> Trim(lblProductCode.Text) Then
            txtWEF.Text = ""
        End If

        SqlStr = " SELECT ITEM_SHORT_DESC,ISSUE_UOM,ITEM_MODEL,CUSTOMER_PART_NO " & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE ITEM_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS = 'A' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If Not RsTemp.EOF Then
            lblProductDesc.Text = IIf(IsDbNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value)
            lblPartNo.Text = IIf(IsDbNull(RsTemp.Fields("CUSTOMER_PART_NO").Value), "", RsTemp.Fields("CUSTOMER_PART_NO").Value)
            lblModel.Text = IIf(IsDbNull(RsTemp.Fields("ITEM_MODEL").Value), "", RsTemp.Fields("ITEM_MODEL").Value)
            lblProductCode.Text = Trim(txtProductCode.Text)
            lblUOM.Text = IIf(IsDbNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)
        Else
            lblProductDesc.Text = ""
            lblPartNo.Text = ""
            lblModel.Text = ""
            lblProductCode.Text = ""
            lblUOM.Text = ""
            MsgBox("Invaild Item Code.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

        If Trim(txtWEF.Text) = "" Then
            SqlStr = " SELECT MAX(WEF) AS WEF " & vbCrLf & " FROM PRD_NEWBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE='" & Trim(txtProductCode.Text) & "'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If Not RsTemp.EOF Then
                txtWEF.Text = IIf(IsDbNull(RsTemp.Fields("WEF").Value), "", RsTemp.Fields("WEF").Value)
            End If
        End If

        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtWEF_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtWEF.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchWEF_Click(cmdSearchWEF, New System.EventArgs())
    End Sub

    Private Function GetVendorName(ByRef pItemCode As String) As String

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCnt As Integer

        '    If MainClass.ValidateWithMasterTable() = True Then	
        '	
        '    End If	

        GetVendorName = ""
        SqlStr = ""
        '    SqlStr = " SELECT DISTINCT CMST.SUPP_CUST_NAME " & vbCrLf _	
        ''            & " FROM INV_GATE_HDR IH, INV_GATE_DET ID, FIN_SUPP_CUST_MST CMST" & vbCrLf _	
        ''            & " WHERE IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _	
        ''            & " AND SUBSTR(IH.AUTO_KEY_MRR,LENGTH(IH.AUTO_KEY_MRR)-5,4) IN (" & RsCompany.fields("FYEAR").value & ", " & RsCompany.fields("FYEAR").value - 1 & ")" & vbCrLf _	
        ''            & " AND IH.AUTO_KEY_MRR=ID.AUTO_KEY_MRR" & vbCrLf _	
        ''            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _	
        ''            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _	
        ''            & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _	
        ''            & " AND CMST.STATUS='O' " & vbCrLf _	
        ''            & " AND IH.SUPP_CUST_CODE IN ("	




        SqlStr = SqlStr & " SELECT DISTINCT CMST.SUPP_CUST_NAME " & vbCrLf _
            & " FROM PUR_PURCHASE_HDR IHS, PUR_PURCHASE_DET IDS, FIN_SUPP_CUST_MST CMST" & vbCrLf _
            & " WHERE IHS.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
            & " AND IHS.MKEY=IDS.MKEY AND IHS.COMPANY_CODE=CMST.COMPANY_CODE AND IHS.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND IDS.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
            & " AND IHS.PO_STATUS='Y' AND IHS.PO_CLOSED='N' AND IHS.PUR_TYPE='P' AND IHS.ORDER_TYPE='O' AND CMST.STATUS='O'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        mCnt = 0
        If Not RsTemp.EOF Then
            Do While RsTemp.EOF = False
                mCnt = mCnt + 1
                GetVendorName = IIf(GetVendorName = "", "", GetVendorName & ", ") & IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
                RsTemp.MoveNext()
                If mCnt = 3 Then Exit Function
            Loop
        End If
        Exit Function
ErrPart:
        GetVendorName = ""
    End Function
End Class
