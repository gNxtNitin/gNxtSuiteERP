Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Imports VB = Microsoft.VisualBasic
Friend Class frmParamStandardConsumptionNew
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    ''Dim PvtDBCn As ADODB.Connection								
    Private Const RowHeight As Short = 22
    Dim mClickProcess As Boolean

    Private Structure AlterItemArray
        Dim mAlterCode As String
    End Structure
    Private mAlterItemData() As AlterItemArray

    'Private Const ColPicMain = 1								
    'Private Const ColPicSub = 2								
    'Private Const ColRMCode = 3								
    'Private Const ColRMDesc = 4								
    'Private Const ColUnit = 5								
    'Private Const ColOPQty = 6								
    'Private Const ColPurQty = 7								
    'Private Const ColINHouseQty = 8								
    'Private Const ColDespQty = 9								
    'Private Const ColClosingQty = 10								
    'Private Const ColVarQty = 11								
    'Private Const ColVarPer = 12								
    'Private Const ColRate = 13								
    'Private Const ColVarAmount = 14								

    'Private Const ColPicMain = 1								
    'Private Const ColPicSub = 2								
    Private Const ColCustomerCode As Short = 1
    Private Const ColRMCode As Short = 2
    Private Const ColRMDesc As Short = 3
    Private Const ColUnit As Short = 4
    Private Const ColMainProd As Short = 5
    Private Const ColProductDesc As Short = 6
    Private Const ColProdOpQty As Short = 7
    Private Const ColProdPurQty As Short = 8
    Private Const ColProdJobWorker As Short = 9
    Private Const ColProdRGPQty As Short = 10
    Private Const ColProdScrapQty As Short = 11
    Private Const ColDespQty As Short = 12
    Private Const ColProdQty As Short = 13
    Private Const ColProdSRQty As Short = 14
    Private Const ColProdAdjQty As Short = 15
    Private Const ColProdCLQty As Short = 16
    Private Const colStdQty As Short = 17
    Private Const ColTotalProdOpQty As Short = 18
    Private Const ColTotalProdPurQty As Short = 19
    Private Const ColTotalProdJobWorker As Short = 20
    Private Const ColTotalProdRGPQty As Short = 21
    Private Const ColTotalProdScrapQty As Short = 22
    Private Const ColTotalDespQty As Short = 23
    Private Const ColTotalProdQty As Short = 24
    Private Const ColTotalProdSRQty As Short = 25
    Private Const ColTotalProdAdjQty As Short = 26
    'Private Const ColBOMQty As Short = 17
    'Private Const ColWIPOPQty As Short = 18
    'Private Const ColOPQty As Short = 19
    'Private Const ColPurQty As Short = 20
    'Private Const ColJobWorkerQty As Short = 21
    'Private Const ColINHouseQty As Short = 22
    'Private Const ColRGPQty As Short = 23
    'Private Const ColScrapQty As Short = 24
    Private Const ColNetStdQty As Short = 27
    Private Const ColERPCLQty As Short = 28
    'Private Const ColWIPQty As Short = 27
    'Private Const ColAdjQty As Short = 28
    'Private Const ColWIPAdjQty As Short = 29
    Private Const ColPhyQty As Short = 29
    Private Const ColWIPPhyQty As Short = 30
    Private Const ColVarQty As Short = 31
    Private Const ColRate As Short = 32
    Private Const ColVarAmount As Short = 33
    Private Const ColPurchaseAmount As Short = 34
    Private Const ColPurchasePer As Short = 35

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer

    Dim mActiveRow As Integer
    Dim mcntRow As Integer

    Dim minuspict As System.Drawing.Image
    Dim pluspict As System.Drawing.Image
    Dim mIsGrouped As Boolean

    'Dim mFixedCol As Integer								
    '								
    'Dim mMaxRow As Long								
    'Dim mMaxCol As Long								
    'Dim mColWidth As Single								
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub
    Private Sub chkFG_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkFG.CheckStateChanged
        txtFGName.Enabled = IIf(chkFG.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        cmdSearchFG.Enabled = IIf(chkFG.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        PrintStatus(False)
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub
    Private Sub cmdSearchFG_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchFG.Click
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster(txtFGName.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr) = True Then
            txtFGName.Text = AcName
            txtFGName_Validating(txtFGName, New System.ComponentModel.CancelEventArgs(False)) ''_Validate False
            txtFGName.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        On Error GoTo ErrPart
        Dim SqlStr As String

        MainClass.ClearGrid(SprdMain, RowHeight)
        FormatSprdMain(-1)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Call Show1()

        Call FormatSprdMain(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Call PrintStatus(True)
        Exit Sub
ErrPart:
        '    Resume								
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    '    Private Function SQLQry() As String
    '        On Error GoTo LedgError
    '        Dim RsBudgetMain As ADODB.Recordset
    '        Dim SqlStr As String
    '        'Dim mProdCode As String								
    '        'Dim mProdName As String								
    '        Dim mRMCode As String
    '        'Dim mCustName As String								
    '        Dim mCheckProdCode As String
    '        Dim mCatCode As String
    '        Dim mSubCatCode As String

    '        Dim mRMCatCode As String
    '        Dim mRMCatCodeStr As String
    '        Dim CntLst As Integer
    '        Dim mMaterialType As String

    '        '', WEF								

    '        SqlStr = " SELECT DISTINCT ITEM_CODE, RM_NAME, " & vbCrLf _
    '            & " ISSUE_UOM, UOM_FACTOR FROM ("

    '        SqlStr = SqlStr & vbCrLf _
    '            & " SELECT DISTINCT RMMST.ITEM_CODE AS ITEM_CODE, RMMST.ITEM_SHORT_DESC AS RM_NAME, " & vbCrLf _
    '            & " RMMST.ISSUE_UOM, RMMST.UOM_FACTOR "

    '        SqlStr = SqlStr & vbCrLf _
    '            & " FROM PRD_NEWBOM_HDR IH, PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST, INV_ITEM_MST RMMST" & vbCrLf _
    '            & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
    '            & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
    '            & " AND IH.PRODUCT_CODE=INVMST.ITEM_CODE " & vbCrLf _
    '            & " AND ID.COMPANY_CODE=RMMST.COMPANY_CODE " & vbCrLf _
    '            & " AND ID.RM_CODE=RMMST.ITEM_CODE " & vbCrLf _
    '            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

    '        SqlStr = SqlStr & vbCrLf & " AND IH.STATUS='O' AND BOM_TYPE='P'"

    '        If cboClass.SelectedIndex <> 0 Then
    '            SqlStr = SqlStr & vbCrLf & " AND RMMST.ITEM_CLASS='" & VB.Left(cboClass.Text, 1) & "'"
    '        End If

    '        If chkAllBOP.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtBOPName.Text) <> "" Then
    '            If MainClass.ValidateWithMasterTable(txtBOPName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
    '                mRMCode = MasterNo
    '                SqlStr = SqlStr & vbCrLf & "AND ID.RM_CODE='" & MainClass.AllowSingleQuote(mRMCode) & "'"
    '            End If
    '        End If

    '        If chkFG.CheckState = System.Windows.Forms.CheckState.Unchecked Then
    '            If MainClass.ValidateWithMasterTable(txtFGName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
    '                mCheckProdCode = MasterNo
    '                SqlStr = SqlStr & vbCrLf & "AND INVMST.ITEM_CODE='" & MainClass.AllowSingleQuote(mCheckProdCode) & "'"
    '            End If
    '        End If

    '        If chkAllCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
    '            If MainClass.ValidateWithMasterTable(txtCategory.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
    '                mCatCode = MasterNo
    '                SqlStr = SqlStr & vbCrLf & "AND INVMST.CATEGORY_CODE='" & MainClass.AllowSingleQuote(mCatCode) & "'"
    '            End If
    '        End If

    '        If chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Unchecked Then
    '            If MainClass.ValidateWithMasterTable(txtSubCategory.Text, "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCatCode & "'") = True Then
    '                mSubCatCode = MasterNo
    '                SqlStr = SqlStr & vbCrLf & "AND INVMST.SUBCATEGORY_CODE='" & MainClass.AllowSingleQuote(mSubCatCode) & "'"
    '            End If
    '        End If

    '        For CntLst = 0 To lstMaterialType.Items.Count - 1
    '            If lstMaterialType.GetItemChecked(CntLst) = True Then
    '                mMaterialType = VB6.GetItemString(lstMaterialType, CntLst)
    '                If MainClass.ValidateWithMasterTable(mMaterialType, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
    '                    mRMCatCode = IIf(IsDBNull(MasterNo), "", MasterNo)
    '                End If
    '                mRMCatCodeStr = IIf(mRMCatCodeStr = "", "'" & mRMCatCode & "'", mRMCatCodeStr & "," & "'" & mRMCatCode & "'")
    '            End If
    '        Next

    '        If mRMCatCodeStr <> "" Then
    '            mRMCatCodeStr = "(" & mRMCatCodeStr & ")"
    '            SqlStr = SqlStr & vbCrLf & " AND RMMST.CATEGORY_CODE IN " & mRMCatCodeStr & ""
    '        End If

    '        SqlStr = SqlStr & vbCrLf & " UNION ALL"

    '        SqlStr = SqlStr & vbCrLf _
    '            & " SELECT DISTINCT RMMST.ITEM_CODE AS ITEM_CODE, RMMST.ITEM_SHORT_DESC AS RM_NAME, " & vbCrLf _
    '            & " RMMST.ISSUE_UOM, RMMST.UOM_FACTOR "

    '        SqlStr = SqlStr & vbCrLf _
    '            & " FROM PRD_NEWBOM_HDR IH, PRD_BOM_ALTER_DET ID, INV_ITEM_MST INVMST, INV_ITEM_MST RMMST" & vbCrLf _
    '            & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
    '            & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf _
    '            & " AND IH.PRODUCT_CODE=INVMST.ITEM_CODE " & vbCrLf _
    '            & " AND ID.COMPANY_CODE=RMMST.COMPANY_CODE " & vbCrLf _
    '            & " AND ID.ALTER_RM_CODE=RMMST.ITEM_CODE " & vbCrLf _
    '            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

    '        SqlStr = SqlStr & vbCrLf & " AND IH.STATUS='O' AND BOM_TYPE='P'"

    '        If cboClass.SelectedIndex <> 0 Then
    '            SqlStr = SqlStr & vbCrLf & " AND RMMST.ITEM_CLASS='" & VB.Left(cboClass.Text, 1) & "'"
    '        End If

    '        If chkAllBOP.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtBOPName.Text) <> "" Then
    '            If MainClass.ValidateWithMasterTable(txtBOPName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
    '                mRMCode = MasterNo
    '                SqlStr = SqlStr & vbCrLf & "AND ID.ALTER_RM_CODE='" & MainClass.AllowSingleQuote(mRMCode) & "'"
    '            End If
    '        End If

    '        If chkFG.CheckState = System.Windows.Forms.CheckState.Unchecked Then
    '            If MainClass.ValidateWithMasterTable(txtFGName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
    '                mCheckProdCode = MasterNo
    '                SqlStr = SqlStr & vbCrLf & "AND INVMST.ITEM_CODE='" & MainClass.AllowSingleQuote(mCheckProdCode) & "'"
    '            End If
    '        End If

    '        If chkAllCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
    '            If MainClass.ValidateWithMasterTable(txtCategory.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
    '                mCatCode = MasterNo
    '                SqlStr = SqlStr & vbCrLf & "AND INVMST.CATEGORY_CODE='" & MainClass.AllowSingleQuote(mCatCode) & "'"
    '            End If
    '        End If

    '        If chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Unchecked Then
    '            If MainClass.ValidateWithMasterTable(txtSubCategory.Text, "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCatCode & "'") = True Then
    '                mSubCatCode = MasterNo
    '                SqlStr = SqlStr & vbCrLf & "AND INVMST.SUBCATEGORY_CODE='" & MainClass.AllowSingleQuote(mSubCatCode) & "'"
    '            End If
    '        End If

    '        For CntLst = 0 To lstMaterialType.Items.Count - 1
    '            If lstMaterialType.GetItemChecked(CntLst) = True Then
    '                mMaterialType = VB6.GetItemString(lstMaterialType, CntLst)
    '                If MainClass.ValidateWithMasterTable(mMaterialType, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
    '                    mRMCatCode = IIf(IsDBNull(MasterNo), "", MasterNo)
    '                End If
    '                mRMCatCodeStr = IIf(mRMCatCodeStr = "", "'" & mRMCatCode & "'", mRMCatCodeStr & "," & "'" & mRMCatCode & "'")
    '            End If
    '        Next

    '        If mRMCatCodeStr <> "" Then
    '            mRMCatCodeStr = "(" & mRMCatCodeStr & ")"
    '            SqlStr = SqlStr & vbCrLf & " AND RMMST.CATEGORY_CODE IN " & mRMCatCodeStr & ""
    '        End If

    '        SqlStr = SqlStr & vbCrLf & ")"

    '        SQLQry = SqlStr

    '        Exit Function
    'LedgError:
    '        '    Resume								
    '        MsgInformation(Err.Description)
    '        SQLQry = ""
    '    End Function
    Private Function SQLItemQry() As String
        On Error GoTo LedgError
        Dim RsBudgetMain As ADODB.Recordset
        Dim SqlStr As String
        'Dim mProdCode As String								
        'Dim mProdName As String								
        Dim mRMCode As String
        'Dim mCustName As String								
        Dim mCheckProdCode As String
        Dim mCatCode As String
        Dim mSubCatCode As String

        Dim mRMCatCode As String
        Dim mRMCatCodeStr As String
        Dim CntLst As Integer
        Dim mMaterialType As String

        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim mCompanyCodeStr As String

        '', WEF								



        SqlStr = " SELECT DISTINCT RMMST.ITEM_CODE AS ITEM_CODE, RMMST.ITEM_SHORT_DESC AS RM_NAME, " & vbCrLf _
            & " RMMST.ISSUE_UOM, RMMST.UOM_FACTOR "

        SqlStr = SqlStr & vbCrLf _
            & " FROM INV_STOCK_REC_TRN IH, INV_ITEM_MST RMMST" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=RMMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.ITEM_CODE=RMMST.ITEM_CODE "

        '& vbCrLf _
        '    & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If lstCompanyName.GetItemChecked(0) = True Then
            mCompanyCodeStr = ""
        Else
            For CntLst = 1 To lstCompanyName.Items.Count - 1
                If lstCompanyName.GetItemChecked(CntLst) = True Then
                    mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                    If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_SHORTNAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                        mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
                End If
            Next
        End If

        If mCompanyCodeStr <> "" Then
            mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If


        SqlStr = SqlStr & vbCrLf & " AND IH.STATUS='O'"

        If cboClass.SelectedIndex <> 0 Then
            SqlStr = SqlStr & vbCrLf & " AND RMMST.ITEM_CLASS='" & VB.Left(cboClass.Text, 1) & "'"
        End If

        If chkAllBOP.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtBOPName.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtBOPName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mRMCode = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND IH.ITEM_CODE='" & MainClass.AllowSingleQuote(mRMCode) & "'"
            End If
        End If

        'If chkFG.CheckState = System.Windows.Forms.CheckState.Unchecked Then
        '    If MainClass.ValidateWithMasterTable(txtFGName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mCheckProdCode = MasterNo
        '        SqlStr = SqlStr & vbCrLf & "AND INVMST.ITEM_CODE='" & MainClass.AllowSingleQuote(mCheckProdCode) & "'"
        '    End If
        'End If

        If chkAllCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtCategory.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                mCatCode = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND RMMST.CATEGORY_CODE='" & MainClass.AllowSingleQuote(mCatCode) & "'"
            End If
        End If

        If chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtSubCategory.Text, "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCatCode & "'") = True Then
                mSubCatCode = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND RMMST.SUBCATEGORY_CODE='" & MainClass.AllowSingleQuote(mSubCatCode) & "'"
            End If
        End If

        For CntLst = 0 To lstMaterialType.Items.Count - 1
            If lstMaterialType.GetItemChecked(CntLst) = True Then
                mMaterialType = VB6.GetItemString(lstMaterialType, CntLst)
                If MainClass.ValidateWithMasterTable(mMaterialType, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                    mRMCatCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                End If
                mRMCatCodeStr = IIf(mRMCatCodeStr = "", "'" & mRMCatCode & "'", mRMCatCodeStr & "," & "'" & mRMCatCode & "'")
            End If
        Next

        If mRMCatCodeStr <> "" Then
            mRMCatCodeStr = "(" & mRMCatCodeStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND RMMST.CATEGORY_CODE IN " & mRMCatCodeStr & ""
        End If

        SQLItemQry = SqlStr

        Exit Function
LedgError:
        '    Resume								
        MsgInformation(Err.Description)
        SQLItemQry = ""
    End Function

    Private Function Show1() As Boolean
        On Error GoTo LedgError
        Dim RsShow As ADODB.Recordset
        Dim SqlStr As String
        Dim mRMCode As String
        Dim mRMName As String
        Dim mItemUOM As String
        Dim mPurQty As Double
        Dim mRGPINQty As Double
        Dim mSRQty As Double
        Dim mDespQty As Double
        Dim mINHouseQty As Double
        Dim mVarQty As Double
        Dim mRate As Double
        Dim mAmount As Double
        Dim mChildCode As String
        Dim mCatCode As String
        Dim mCatCodeStr As String
        Dim CntLst As Integer
        Dim mMaterialType As String
        Dim mDate As String
        Dim mOPQty As Double
        Dim mCLQty As Double
        Dim xSqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mAlterItemCodeStr As String
        Dim mUpperBound As Integer
        Dim I As Integer
        Dim mCurrRow As Integer
        Dim pStartRow As Integer
        Dim pEndRow As Integer
        Dim xDespSqlQry As String
        Dim mItemCodeStr As String
        Dim pTotalDespatch As Double
        Dim pTotalProductionQty As Double
        Dim pTotalWIPOPQty As Double
        Dim pTotalWIPCLQty As Double
        Dim pTotalWIPOUTQty As Double
        Dim pTotalPhyWIPQty As Double
        Dim pStdBalQty As Double
        Dim pRMScrapQty As Double
        Dim pRMRGPQty As Double
        Dim pPhyQty As Double
        Dim pNetStdBalQty As Double
        Dim mBackColor As Integer
        Dim mAdjQty As Double
        Dim mFactor As Double
        Dim mPurchaseRate As Double
        Dim mLandedCost As Double
        Dim pTotalWIPAdjQty As Double
        Dim pTotalScrapQty As Double
        Dim pINHouseDept As String

        Dim pTotalCROPQty As Double
        Dim pTotalCRRecdQty As Double
        Dim pTotalCRDespQty As Double
        Dim pTotalCRCLQty As Double
        Dim mPurchaseReturnQty As Double
        Dim mJobWorkerQty As Double
        Dim mRGPQty As Double


        'SQLItemQry


        'SqlStr = SQLQry() & vbCrLf & " ORDER BY 1"     

        SqlStr = SQLItemQry() & vbCrLf & " ORDER BY 1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)
        mcntRow = 1

        SprdMain.MaxRows = 1
        With SprdMain
            If RsShow.EOF = False Then
                Do While Not RsShow.EOF
                    .Row = .MaxRows
                    mRMCode = Trim(IIf(IsDBNull(RsShow.Fields("ITEM_CODE").Value), "", RsShow.Fields("ITEM_CODE").Value))
                    mRMName = IIf(IsDBNull(RsShow.Fields("RM_NAME").Value), "", RsShow.Fields("RM_NAME").Value)
                    mItemUOM = IIf(IsDBNull(RsShow.Fields("ISSUE_UOM").Value), "", RsShow.Fields("ISSUE_UOM").Value)
                    mFactor = IIf(IsDBNull(RsShow.Fields("UOM_FACTOR").Value) Or RsShow.Fields("UOM_FACTOR").Value = 0, 1, RsShow.Fields("UOM_FACTOR").Value)

                    mRMCode = Trim(IIf(IsDBNull(RsShow.Fields("ITEM_CODE").Value), "", RsShow.Fields("ITEM_CODE").Value))
                    mChildCode = mRMCode
                    ReDim mAlterItemData(0)
                    mAlterItemData(0).mAlterCode = ""

                    mOPQty = GetStockQty(mRMCode, mItemUOM, "STR", "QC", ConWH, "OP") ''+ GetStockQty(mRMCode, mItemUOM, "", "QC", ConPH, "OP")
                    mPurQty = GetNetPurchase(mRMCode, "P")
                    mPurchaseReturnQty = GetNetPurchase(mRMCode, "PD")
                    mSRQty = GetNetPurchase(mRMCode, "I")
                    mJobWorkerQty = GetNetPurchase(mRMCode, "J")

                    mRGPQty = GetNetPurchase(mRMCode, "R")

                    pRMScrapQty = GetNetPurchase(mRMCode, "S")

                    mAdjQty = GetNetPurchase(mRMCode, "A")
                    mINHouseQty = GetNetPurchase(mRMCode, "PMD")

                    mCLQty = GetStockQty(mRMCode, mItemUOM, "STR", "QC", "", "CL")  ''+ GetStockQty(mRMCode, mItemUOM, "", "QC", ConPH, "CL")

                    pPhyQty = GetPhysicalQty(mRMCode, "", ConWH) + GetPhysicalQty(mRMCode, "", ConPH)
                    xDespSqlQry = DespatchSqlQry(mRMCode)

                    mItemCodeStr = Trim(IIf(IsDBNull(RsShow.Fields("ITEM_CODE").Value), "", RsShow.Fields("ITEM_CODE").Value)) & mAlterItemCodeStr
                    mCurrRow = .MaxRows
                    pStartRow = mCurrRow

                    If GetDespatchQty(xDespSqlQry, mItemCodeStr, mRMName, mItemUOM, mCurrRow) = False Then GoTo LedgError
                    'mDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(txtDate(0).Text)))

                    .Row = mCurrRow

                    .Col = ColRMCode
                    .Text = Trim(IIf(IsDBNull(RsShow.Fields("ITEM_CODE").Value), "", RsShow.Fields("ITEM_CODE").Value)) & mAlterItemCodeStr

                    .Col = ColRMDesc
                    .Text = IIf(IsDBNull(RsShow.Fields("RM_NAME").Value), "", RsShow.Fields("RM_NAME").Value)

                    .Col = ColUnit
                    .Text = IIf(IsDBNull(RsShow.Fields("ISSUE_UOM").Value), "", RsShow.Fields("ISSUE_UOM").Value)
                    mItemUOM = IIf(IsDBNull(RsShow.Fields("ISSUE_UOM").Value), "", RsShow.Fields("ISSUE_UOM").Value)

                    .Col = ColMainProd
                    .Text = Trim(IIf(IsDBNull(RsShow.Fields("ITEM_CODE").Value), "", RsShow.Fields("ITEM_CODE").Value)) & mAlterItemCodeStr

                    .Col = ColProductDesc
                    .Text = IIf(IsDBNull(RsShow.Fields("RM_NAME").Value), "", RsShow.Fields("RM_NAME").Value)

                    .Col = ColTotalProdOpQty
                    .Text = VB6.Format(mOPQty, "0.00")

                    .Col = ColTotalProdPurQty
                    .Text = VB6.Format(mPurQty, "0.00")

                    .Col = ColTotalProdJobWorker
                    .Text = VB6.Format(mJobWorkerQty, "0.00")

                    .Col = ColTotalProdQty
                    .Text = VB6.Format(mINHouseQty, "0.00")

                    .Col = ColTotalProdRGPQty
                    .Text = VB6.Format(mRGPQty, "0.00")

                    .Col = ColTotalProdScrapQty
                    .Text = VB6.Format(pRMScrapQty, "0.00")

                    .Col = ColTotalDespQty
                    .Text = VB6.Format(mPurchaseReturnQty, "0.00")

                    .Col = ColTotalProdSRQty
                    .Text = VB6.Format(mSRQty, "0.00")

                    .Col = ColTotalProdAdjQty
                    .Text = VB6.Format(mAdjQty, "0.00")

                    .Col = ColERPCLQty
                    .Text = VB6.Format(mCLQty, "0.00")

                    .Col = ColPhyQty
                    .Text = VB6.Format(pPhyQty, "0.00")

                    .Col = ColVarQty
                    .Text = VB6.Format(mVarQty, "0.00")

                    .MaxRows = .MaxRows + 1
                    mCurrRow = .MaxRows

                    pEndRow = mCurrRow
                    .Row = mCurrRow
                    .Col = ColCustomerCode
                    .Text = "TOTAL :"

                    .Col = ColRMCode
                    .Text = Trim(IIf(IsDBNull(RsShow.Fields("ITEM_CODE").Value), "", RsShow.Fields("ITEM_CODE").Value)) & mAlterItemCodeStr

                    .Col = ColRMDesc
                    .Text = IIf(IsDBNull(RsShow.Fields("RM_NAME").Value), "", RsShow.Fields("RM_NAME").Value)


                    .Col = ColUnit
                    .Text = IIf(IsDBNull(RsShow.Fields("ISSUE_UOM").Value), "", RsShow.Fields("ISSUE_UOM").Value)
                    mItemUOM = IIf(IsDBNull(RsShow.Fields("ISSUE_UOM").Value), "", RsShow.Fields("ISSUE_UOM").Value)

                    .Col = ColMainProd
                    .Text = Trim(IIf(IsDBNull(RsShow.Fields("ITEM_CODE").Value), "", RsShow.Fields("ITEM_CODE").Value)) & mAlterItemCodeStr

                    .Col = ColProductDesc
                    .Text = "Total : " & IIf(IsDBNull(RsShow.Fields("RM_NAME").Value), "", RsShow.Fields("RM_NAME").Value)



                    Call CalcRowTotal(SprdMain, ColTotalProdOpQty, pStartRow, ColTotalProdOpQty, pEndRow - 1, pEndRow, ColTotalProdOpQty)
                    Call CalcRowTotal(SprdMain, ColTotalProdPurQty, pStartRow, ColTotalProdPurQty, pEndRow - 1, pEndRow, ColTotalProdPurQty)
                    Call CalcRowTotal(SprdMain, ColTotalProdJobWorker, pStartRow, ColTotalProdJobWorker, pEndRow - 1, pEndRow, ColTotalProdJobWorker)
                    Call CalcRowTotal(SprdMain, ColTotalProdRGPQty, pStartRow, ColTotalProdRGPQty, pEndRow - 1, pEndRow, ColTotalProdRGPQty)
                    Call CalcRowTotal(SprdMain, ColTotalProdScrapQty, pStartRow, ColTotalProdScrapQty, pEndRow - 1, pEndRow, ColTotalProdScrapQty)
                    Call CalcRowTotal(SprdMain, ColTotalDespQty, pStartRow, ColTotalDespQty, pEndRow - 1, pEndRow, ColTotalDespQty)
                    Call CalcRowTotal(SprdMain, ColTotalProdSRQty, pStartRow, ColTotalProdSRQty, pEndRow - 1, pEndRow, ColTotalProdSRQty)
                    Call CalcRowTotal(SprdMain, ColTotalProdAdjQty, pStartRow, ColTotalProdAdjQty, pEndRow - 1, pEndRow, ColTotalProdAdjQty)
                    Call CalcRowTotal(SprdMain, ColTotalProdQty, pStartRow, ColTotalProdQty, pEndRow - 1, pEndRow, ColTotalProdQty)

                    Call CalcRowTotal(SprdMain, ColERPCLQty, pStartRow, ColERPCLQty, pEndRow - 1, pEndRow, ColERPCLQty)

                    .Col = ColTotalProdOpQty
                    pStdBalQty = Val(.Text)

                    .Col = ColTotalProdPurQty
                    pStdBalQty = pStdBalQty + Val(.Text)

                    .Col = ColTotalProdJobWorker
                    pStdBalQty = pStdBalQty + Val(.Text)

                    .Col = ColTotalProdRGPQty
                    pStdBalQty = pStdBalQty + Val(.Text)

                    .Col = ColTotalProdScrapQty
                    pStdBalQty = pStdBalQty + Val(.Text)

                    If optDespatch.Checked = True Then
                        .Col = ColTotalDespQty
                        pStdBalQty = pStdBalQty + Val(.Text)
                    Else
                        .Col = ColTotalProdQty
                        pStdBalQty = pStdBalQty - Val(.Text)
                    End If


                    .Col = ColTotalProdSRQty
                    pStdBalQty = pStdBalQty + Val(.Text)

                    .Col = ColTotalProdAdjQty
                    pStdBalQty = pStdBalQty + Val(.Text)

                    .Col = ColERPCLQty
                    mCLQty = Val(.Text)

                    .Col = ColNetStdQty
                    If optDespatch.Checked = True Then
                        .Text = VB6.Format(pStdBalQty, "0.00")
                    Else
                        .Text = VB6.Format(pStdBalQty + mPurchaseReturnQty, "0.00")
                    End If



                    .Col = ColPhyQty
                    .Text = VB6.Format(pPhyQty, "0.00")

                    .Col = ColWIPPhyQty
                    .Text = VB6.Format(pTotalPhyWIPQty, "0.00")

                    mVarQty = pPhyQty + pTotalPhyWIPQty - pStdBalQty

                    .Col = ColVarQty
                    .Text = VB6.Format(mVarQty, "0.00")

                    'pStdBalQty = mOPQty + mPurQty + mRGPINQty + mINHouseQty - mDespQty - pRMRGPQty - pTotalWIPOUTQty
                    'pNetStdBalQty = pStdBalQty + CDbl(VB6.Format(System.Math.Abs(pTotalWIPOPQty), "0.00"))
                    '



                    '.Col = ColERPCLQty
                    '.Text = VB6.Format(mCLQty, "0.00")

                    '.Col = ColWIPQty
                    '.Text = VB6.Format(pTotalWIPCLQty, "0.00")

                    '.Col = ColAdjQty
                    '.Text = VB6.Format(mAdjQty, "0.00")

                    '.Col = ColWIPAdjQty
                    '.Text = VB6.Format(pTotalWIPAdjQty, "0.00")


                    '                mRate = GetCurrentItemRate(mRMCode, Format(txtDate(1).Text, "DD/MM/YYYY"))								

                    If chkRate.CheckState = System.Windows.Forms.CheckState.Checked Then
                        If GetLatestItemCostFromPO(mRMCode, mPurchaseRate, mLandedCost, VB6.Format(txtDate(1).Text, "DD/MM/YYYY"), "ST", "", mItemUOM, mFactor) = False Then GoTo LedgError
                        mRate = IIf(mPurchaseRate = 0, 0, mPurchaseRate)
                    Else
                        mRate = 0
                    End If

                    mAmount = mRate * mVarQty

                    .Col = ColRate
                    .Text = VB6.Format(mRate, "0.00")

                    .Col = ColVarAmount
                    .Text = VB6.Format(mAmount, "0.00")

                    .Col = ColPurchaseAmount
                    .Text = VB6.Format(mPurQty * mRate, "0.00")

                    .Col = ColPurchasePer
                    If mDespQty <> 0 Then
                        .Text = CStr(System.Math.Round((mPurQty) * 100 / mDespQty, 0))
                    Else
                        .Text = "0"
                    End If

                    mBackColor = IIf(mBackColor = &H8000000F, &H80FF80, &H8000000F)
                    '                mBackColor = &H8000000F								
                    .Row = .MaxRows
                    .Row2 = .MaxRows
                    .Col = 1
                    .Col2 = .MaxCols
                    .BlockMode = True
                    .BackColor = System.Drawing.ColorTranslator.FromOle(mBackColor) ''&H80FF80								
                    .BlockMode = False

                    RsShow.MoveNext()

                    If RsShow.EOF = False Then
                        .MaxRows = .MaxRows + 1
                    End If
                Loop
            End If
        End With

        RsShow = Nothing
        Show1 = True
        Exit Function
LedgError:
        '    Resume								
        MsgInformation(Err.Description)
        Show1 = False
    End Function
    Private Function Show1OLD() As Boolean
        On Error GoTo LedgError
        Dim RsShow As ADODB.Recordset
        Dim SqlStr As String
        Dim mRMCode As String
        Dim mRMName As String
        Dim mItemUOM As String
        Dim mPurQty As Double
        Dim mRGPINQty As Double

        Dim mDespQty As Double
        Dim mINHouseQty As Double
        Dim mVarQty As Double
        Dim mRate As Double
        Dim mAmount As Double
        Dim mChildCode As String
        Dim mCatCode As String
        Dim mCatCodeStr As String
        Dim CntLst As Integer
        Dim mMaterialType As String
        Dim mDate As String
        Dim mOPQty As Double
        Dim mCLQty As Double
        Dim xSqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mAlterItemCodeStr As String
        Dim mUpperBound As Integer
        Dim I As Integer
        Dim mCurrRow As Integer
        Dim xDespSqlQry As String
        Dim mItemCodeStr As String
        Dim pTotalDespatch As Double
        Dim pTotalProductionQty As Double
        Dim pTotalWIPOPQty As Double
        Dim pTotalWIPCLQty As Double
        Dim pTotalWIPOUTQty As Double
        Dim pTotalPhyWIPQty As Double
        Dim pStdBalQty As Double
        Dim pRMScrapQty As Double
        Dim pRMRGPQty As Double
        Dim pPhyQty As Double
        Dim pNetStdBalQty As Double
        Dim mBackColor As Integer
        Dim mAdjQty As Double
        Dim mFactor As Double
        Dim mPurchaseRate As Double
        Dim mLandedCost As Double
        Dim pTotalWIPAdjQty As Double
        Dim pTotalScrapQty As Double
        Dim pINHouseDept As String

        Dim pTotalCROPQty As Double
        Dim pTotalCRRecdQty As Double
        Dim pTotalCRDespQty As Double
        Dim pTotalCRCLQty As Double
        Dim mPurchaseReturnQty As Double
        Dim mJobWorkerQty As Double



        'SqlStr = SQLQry() & vbCrLf & " ORDER BY 1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsShow, ADODB.LockTypeEnum.adLockReadOnly)
        mcntRow = 1

        SprdMain.MaxRows = 1
        With SprdMain
            If RsShow.EOF = False Then
                Do While Not RsShow.EOF
                    .Row = .MaxRows
                    mRMCode = Trim(IIf(IsDBNull(RsShow.Fields("ITEM_CODE").Value), "", RsShow.Fields("ITEM_CODE").Value))
                    mRMName = IIf(IsDBNull(RsShow.Fields("RM_NAME").Value), "", RsShow.Fields("RM_NAME").Value)
                    mItemUOM = IIf(IsDBNull(RsShow.Fields("ISSUE_UOM").Value), "", RsShow.Fields("ISSUE_UOM").Value)
                    mFactor = IIf(IsDBNull(RsShow.Fields("UOM_FACTOR").Value) Or RsShow.Fields("UOM_FACTOR").Value = 0, 1, RsShow.Fields("UOM_FACTOR").Value)

                    '                .Col = ColRMCode								
                    mRMCode = Trim(IIf(IsDBNull(RsShow.Fields("ITEM_CODE").Value), "", RsShow.Fields("ITEM_CODE").Value))
                    mChildCode = mRMCode

                    'xSqlStr = GetQueryForAlterItem(mRMCode)
                    'MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                    'mUpperBound = 0
                    'mAlterItemCodeStr = ""
                    'If RsTemp.EOF = False Then
                    '    Do While RsTemp.EOF = False
                    '        RsTemp.MoveNext()
                    '        If RsTemp.EOF = False Then
                    '            mUpperBound = mUpperBound + 1
                    '        End If
                    '    Loop
                    '    ReDim mAlterItemData(mUpperBound)
                    '    RsTemp.MoveFirst()
                    '    I = 0
                    '    Do While RsTemp.EOF = False
                    '        mAlterItemData(I).mAlterCode = Trim(IIf(IsDBNull(RsTemp.Fields("ALTER_RM_CODE").Value), "", RsTemp.Fields("ALTER_RM_CODE").Value))
                    '        mAlterItemCodeStr = mAlterItemCodeStr & "/" & Trim(IIf(IsDBNull(RsTemp.Fields("ALTER_RM_CODE").Value), "", RsTemp.Fields("ALTER_RM_CODE").Value))
                    '        RsTemp.MoveNext()
                    '        I = I + 1
                    '    Loop
                    'Else
                    ReDim mAlterItemData(0)
                    mAlterItemData(0).mAlterCode = ""
                    'End If
                    '                .Text = Trim(IIf(IsNull(RsShow!ITEM_CODE), "", RsShow!ITEM_CODE)) & mAlterItemCodeStr								

                    mOPQty = GetStockQty(mRMCode, mItemUOM, "STR", "QC", ConWH, "OP") ''+ GetStockQty(mRMCode, mItemUOM, "", "QC", ConPH, "OP")

                    mPurQty = GetNetPurchase(mRMCode, "P")
                    mPurchaseReturnQty = GetNetPurchase(mRMCode, "D")
                    mINHouseQty = GetNetPurchase(mRMCode, "I")
                    mJobWorkerQty = GetNetPurchase(mRMCode, "J")



                    'mPurQty = GetNetPurchase(mRMCode, "P") - GetRMSaleQty(mRMCode, "P")
                    'mRGPINQty = GetNetPurchase(mRMCode, "R")
                    '                pRMOUTQty = 0 ''GetRMSaleQty(mRMCode, "R")								
                    pRMScrapQty = System.Math.Abs(GetStockQty(mRMCode, mItemUOM, "STR", "SC", ConWH, "", "'" & ConStockRefType_SRN & "'"))
                    pRMRGPQty = System.Math.Abs(GetStockQty(mRMCode, mItemUOM, "STR", "", ConWH, "", "'" & ConStockRefType_NRG & "','" & ConStockRefType_RGP & "'"))
                    mAdjQty = GetStockQty(mRMCode, mItemUOM, "STR", "", ConWH, "", "'" & ConStockRefType_ADJ & "'")
                    mAdjQty = mAdjQty + GetStockQty(mRMCode, mItemUOM, "", "", ConPH, "", "'" & ConStockRefType_ADJ & "'")

                    pINHouseDept = GetProductDept(mRMCode, 1, txtDate(0).Text)
                    If pINHouseDept <> "" Then
                        mINHouseQty = GetStockQty(mRMCode, mItemUOM, pINHouseDept, "ST", ConPH, "", "'" & ConStockRefType_PMEMODEPT & "'", "I")
                    Else
                        mINHouseQty = 0
                    End If
                    mCLQty = GetStockQty(mRMCode, mItemUOM, "STR", "QC", ConWH, "CL") + GetStockQty(mRMCode, mItemUOM, "", "QC", ConPH, "CL")
                    '                mCLQty = mCLQty - GetPhysicalAdjQty(mRMCode, "", ConWH)								
                    pPhyQty = GetPhysicalQty(mRMCode, "", ConWH) + GetPhysicalQty(mRMCode, "", ConPH)
                    xDespSqlQry = DespatchSqlQry(mRMCode)

                    'For I = 0 To mUpperBound
                    '    If mAlterItemData(I).mAlterCode <> "" Then
                    '        mOPQty = mOPQty + GetStockQty(mAlterItemData(I).mAlterCode, mItemUOM, "STR", "QC", ConWH, "OP") + GetStockQty(mAlterItemData(I).mAlterCode, mItemUOM, "", "QC", ConPH, "OP")
                    '        mPurQty = mPurQty + GetNetPurchase(mAlterItemData(I).mAlterCode, "P") - GetRMSaleQty(mAlterItemData(I).mAlterCode, "P")
                    '        mRGPINQty = mRGPINQty + GetNetPurchase(mAlterItemData(I).mAlterCode, "R")
                    '        '                        pRMOUTQty = pRMOUTQty + GetRMSaleQty(mAlterItemData(I).mAlterCode, "R")								
                    '        pRMRGPQty = pRMRGPQty + System.Math.Abs(GetStockQty(mAlterItemData(I).mAlterCode, mItemUOM, "STR", "", ConWH, "", "'" & ConStockRefType_NRG & "','" & ConStockRefType_RGP & "'"))
                    '        pRMScrapQty = pRMScrapQty + System.Math.Abs(GetStockQty(mAlterItemData(I).mAlterCode, mItemUOM, "STR", "SC", ConWH, "", "'" & ConStockRefType_SRN & "'"))
                    '        '                        mINHouseQty = mINHouseQty + GetStockQty(mAlterItemData(I).mAlterCode, mItemUOM, "STR", "", ConWH, "", "'" & ConStockRefType_PISS & "'")								

                    '        pINHouseDept = GetProductDept(mAlterItemData(I).mAlterCode, 1, txtDate(0).Text)
                    '        If pINHouseDept <> "" Then
                    '            mINHouseQty = mINHouseQty + GetStockQty(mAlterItemData(I).mAlterCode, mItemUOM, pINHouseDept, "ST", ConPH, "", "'" & ConStockRefType_PMEMODEPT & "'", "I")
                    '        End If

                    '        mAdjQty = mAdjQty + GetStockQty(mAlterItemData(I).mAlterCode, mItemUOM, "STR", "", ConWH, "", "'" & ConStockRefType_ADJ & "'")
                    '        mAdjQty = mAdjQty + GetStockQty(mAlterItemData(I).mAlterCode, mItemUOM, "", "", ConPH, "", "'" & ConStockRefType_ADJ & "'")

                    '        mCLQty = mCLQty + GetStockQty(mAlterItemData(I).mAlterCode, mItemUOM, "STR", "QC", ConWH, "CL") + GetStockQty(mAlterItemData(I).mAlterCode, mItemUOM, "", "QC", ConPH, "CL")
                    '        '                        mCLQty = mCLQty - GetPhysicalAdjQty(mAlterItemData(I).mAlterCode, "", ConWH)								
                    '        pPhyQty = pPhyQty + GetPhysicalQty(mAlterItemData(I).mAlterCode, "", ConWH) + GetPhysicalQty(mAlterItemData(I).mAlterCode, "", ConPH)
                    '        xDespSqlQry = xDespSqlQry & vbCrLf & " UNION " & vbCrLf & DespatchSqlQry(mAlterItemData(I).mAlterCode)
                    '    End If
                    'Next
                    mItemCodeStr = Trim(IIf(IsDBNull(RsShow.Fields("ITEM_CODE").Value), "", RsShow.Fields("ITEM_CODE").Value)) & mAlterItemCodeStr
                    mCurrRow = .MaxRows
                    'mDespQty = GetDespatchQty(xDespSqlQry, mItemCodeStr, mRMName, mItemUOM, mCurrRow, pTotalDespatch, pTotalProductionQty, pTotalWIPOPQty, pTotalWIPCLQty, pTotalWIPOUTQty, pTotalPhyWIPQty, pTotalWIPAdjQty, pTotalScrapQty, pTotalCROPQty, pTotalCRRecdQty, pTotalCRDespQty, pTotalCRCLQty)
                    mDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, CDate(txtDate(0).Text)))

                    .Row = mCurrRow

                    .Col = ColRMCode
                    .Text = Trim(IIf(IsDBNull(RsShow.Fields("ITEM_CODE").Value), "", RsShow.Fields("ITEM_CODE").Value)) & mAlterItemCodeStr

                    .Col = ColRMDesc
                    .Text = IIf(IsDBNull(RsShow.Fields("RM_NAME").Value), "", RsShow.Fields("RM_NAME").Value)

                    .Col = ColUnit
                    .Text = IIf(IsDBNull(RsShow.Fields("ISSUE_UOM").Value), "", RsShow.Fields("ISSUE_UOM").Value)
                    mItemUOM = IIf(IsDBNull(RsShow.Fields("ISSUE_UOM").Value), "", RsShow.Fields("ISSUE_UOM").Value)

                    .Col = ColMainProd
                    .Text = Trim(IIf(IsDBNull(RsShow.Fields("ITEM_CODE").Value), "", RsShow.Fields("ITEM_CODE").Value)) & mAlterItemCodeStr

                    .Col = ColProductDesc
                    .Text = IIf(IsDBNull(RsShow.Fields("RM_NAME").Value), "", RsShow.Fields("RM_NAME").Value)

                    '.Col = ColOPQty
                    '.Text = VB6.Format(mOPQty, "0.00")

                    '.Col = ColPurQty
                    '.Text = VB6.Format(mPurQty, "0.00")

                    '.Col = ColINHouseQty
                    '.Text = VB6.Format(mINHouseQty, "0.00")

                    '.Col = ColJobWorkerQty
                    '.Text = VB6.Format(mJobWorkerQty, "0.00") 'pRMOUTQty - pRMRGPQty - pTotalWIPOUTQty										

                    '.Col = ColScrapQty
                    '.Text = VB6.Format(pRMScrapQty, "0.00")

                    '.Col = ColERPCLQty
                    '.Text = VB6.Format(mCLQty, "0.00")

                    '.Col = ColAdjQty
                    '.Text = VB6.Format(mAdjQty, "0.00")

                    .Col = ColPhyQty
                    .Text = VB6.Format(pPhyQty, "0.00")

                    .Col = ColVarQty
                    .Text = VB6.Format(mVarQty, "0.00")

                    .MaxRows = .MaxRows + 1
                    mCurrRow = .MaxRows

                    .Row = mCurrRow
                    .Col = ColCustomerCode
                    .Text = "TOTAL :"

                    .Col = ColRMCode
                    .Text = Trim(IIf(IsDBNull(RsShow.Fields("ITEM_CODE").Value), "", RsShow.Fields("ITEM_CODE").Value)) & mAlterItemCodeStr

                    .Col = ColRMDesc
                    .Text = IIf(IsDBNull(RsShow.Fields("RM_NAME").Value), "", RsShow.Fields("RM_NAME").Value)


                    .Col = ColUnit
                    .Text = IIf(IsDBNull(RsShow.Fields("ISSUE_UOM").Value), "", RsShow.Fields("ISSUE_UOM").Value)
                    mItemUOM = IIf(IsDBNull(RsShow.Fields("ISSUE_UOM").Value), "", RsShow.Fields("ISSUE_UOM").Value)

                    .Col = ColMainProd
                    .Text = Trim(IIf(IsDBNull(RsShow.Fields("ITEM_CODE").Value), "", RsShow.Fields("ITEM_CODE").Value)) & mAlterItemCodeStr

                    .Col = ColProductDesc
                    .Text = "Total : " & IIf(IsDBNull(RsShow.Fields("RM_NAME").Value), "", RsShow.Fields("RM_NAME").Value)

                    .Col = ColDespQty
                    .Text = VB6.Format(System.Math.Abs(pTotalDespatch), "0.00")

                    .Col = ColProdQty
                    .Text = VB6.Format(System.Math.Abs(pTotalProductionQty), "0.00")

                    '.Col = ColBOMQty
                    '.Text = VB6.Format(System.Math.Abs(mDespQty), "0.00")

                    '.Col = ColWIPOPQty
                    '.Text = VB6.Format(System.Math.Abs(pTotalWIPOPQty), "0.00")

                    '.Col = ColOPQty
                    '.Text = VB6.Format(mOPQty, "0.00")

                    '.Col = ColPurQty
                    '.Text = VB6.Format(mPurQty, "0.00")

                    '.Col = ColINHouseQty
                    '.Text = VB6.Format(mINHouseQty, "0.00")

                    '.Col = ColJobWorkerQty
                    '.Text = VB6.Format(mJobWorkerQty, "0.00") 'pRMOUTQty - pRMRGPQty - pTotalWIPOUTQty				

                    '.Col = ColScrapQty
                    '.Text = VB6.Format(pTotalScrapQty + pRMScrapQty, "0.00")

                    ''                .Col = ColRMDespQty								
                    ''                .Text = Format(Abs(pRMOUTQty), "0.00")								
                    ''								
                    ''                .Col = ColRMRGPQty								
                    ''                .Text = Format(Abs(pRMRGPQty), "0.00")								
                    ''								
                    ''                .Col = ColWIPOutQty								
                    ''                .Text = Format(pTotalWIPOUTQty, "0.00")								

                    '.Col = ColCROPQty
                    '.Text = VB6.Format(pTotalCROPQty, "0.00")

                    '.Col = ColCRRecdQty
                    '.Text = VB6.Format(pTotalCRRecdQty, "0.00")

                    '.Col = ColCRDespQty
                    '.Text = VB6.Format(pTotalCRDespQty, "0.00")

                    '.Col = ColCRCLQty
                    '.Text = VB6.Format(pTotalCRCLQty, "0.00")


                    pStdBalQty = mOPQty + mPurQty + mRGPINQty + mINHouseQty - mDespQty - pRMRGPQty - pTotalWIPOUTQty
                    pNetStdBalQty = pStdBalQty + CDbl(VB6.Format(System.Math.Abs(pTotalWIPOPQty), "0.00"))
                    mVarQty = pPhyQty + pTotalPhyWIPQty - pNetStdBalQty

                    '                .Col = ColCLQty								
                    '                .Text = Format(pStdBalQty, "0.00")								

                    .Col = ColNetStdQty
                    .Text = VB6.Format(pNetStdBalQty, "0.00")

                    .Col = ColERPCLQty
                    .Text = VB6.Format(mCLQty, "0.00")

                    '.Col = ColWIPQty
                    '.Text = VB6.Format(pTotalWIPCLQty, "0.00")

                    '.Col = ColAdjQty
                    '.Text = VB6.Format(mAdjQty, "0.00")

                    '.Col = ColWIPAdjQty
                    '.Text = VB6.Format(pTotalWIPAdjQty, "0.00")

                    .Col = ColPhyQty
                    .Text = VB6.Format(pPhyQty, "0.00")

                    .Col = ColWIPPhyQty
                    .Text = VB6.Format(pTotalPhyWIPQty, "0.00")

                    .Col = ColVarQty
                    .Text = VB6.Format(mVarQty, "0.00")

                    '                mRate = GetCurrentItemRate(mRMCode, Format(txtDate(1).Text, "DD/MM/YYYY"))								

                    If chkRate.CheckState = System.Windows.Forms.CheckState.Checked Then
                        If GetLatestItemCostFromPO(mRMCode, mPurchaseRate, mLandedCost, VB6.Format(txtDate(1).Text, "DD/MM/YYYY"), "ST", "", mItemUOM, mFactor) = False Then GoTo LedgError
                        mRate = IIf(mPurchaseRate = 0, 0, mPurchaseRate)
                    Else
                        mRate = 0
                    End If

                    mAmount = mRate * mVarQty

                    .Col = ColRate
                    .Text = VB6.Format(mRate, "0.00")

                    .Col = ColVarAmount
                    .Text = VB6.Format(mAmount, "0.00")

                    .Col = ColPurchaseAmount
                    .Text = VB6.Format(mPurQty * mRate, "0.00")

                    .Col = ColPurchasePer
                    If mDespQty <> 0 Then
                        .Text = CStr(System.Math.Round((mPurQty) * 100 / mDespQty, 0))
                    Else
                        .Text = "0"
                    End If

                    mBackColor = IIf(mBackColor = &H8000000F, &H80FF80, &H8000000F)
                    '                mBackColor = &H8000000F								
                    .Row = .MaxRows
                    .Row2 = .MaxRows
                    .Col = 1
                    .Col2 = .MaxCols
                    .BlockMode = True
                    .BackColor = System.Drawing.ColorTranslator.FromOle(mBackColor) ''&H80FF80								
                    .BlockMode = False

                    RsShow.MoveNext()

                    If RsShow.EOF = False Then
                        .MaxRows = .MaxRows + 1
                    End If
                Loop
            End If
        End With

        RsShow = Nothing
        Show1OLD = True
        Exit Function
LedgError:
        '    Resume								
        MsgInformation(Err.Description)
        Show1OLD = False
    End Function

    Private Sub frmParamStandardConsumptionNew_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame4.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11535, 769)
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    'Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig								
    '	SprdMain.Row = -1							
    '	SprdMain.Col = Col							
    '	SprdMain.DAutoCellTypes = True							
    '	SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH							
    '	SprdMain.TypeEditLen = 1000							
    'End Sub								

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
        Dim SqlStr As String

        If txtCategory.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'"
        If MainClass.ValidateWithMasterTable(txtCategory.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtCategory.Text = UCase(Trim(txtCategory.Text))
        Else
            MsgInformation("No Such Account in Account Master")
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
        Dim SqlStr As String

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
        Dim SqlStr As String
        Dim mCatCode As String

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
    Private Sub cmdSearchCategory_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchCategory.Click
        SearchCategory()
    End Sub
    Private Sub cmdSubCatsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSubCatsearch.Click
        SearchSubCategory()
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
        Dim SqlStr As String
        Dim mCatCode As String

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
    Private Sub txtBOPName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBOPName.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtBOPName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBOPName.DoubleClick
        SearchBOP()
    End Sub
    Private Sub txtBOPName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBOPName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtBOPName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtBOPName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtBOPName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchBOP()
    End Sub
    Private Sub txtBOPName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBOPName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String


        If txtBOPName.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(txtBOPName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtBOPName.Text = UCase(Trim(txtBOPName.Text))
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
    Private Sub SearchBOP()
        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster(txtBOPName.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr) = True Then
            If AcName <> "" Then
                txtBOPName.Text = AcName
            End If
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdsearchBOPName_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchBOPName.Click
        SearchBOP()
    End Sub
    Private Sub chkAllBOP_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllBOP.CheckStateChanged
        Call PrintStatus(False)
        If chkAllBOP.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtBOPName.Enabled = False
            cmdsearchBOPName.Enabled = False
        Else
            txtBOPName.Enabled = True
            cmdsearchBOPName.Enabled = True
        End If
    End Sub






    Public Sub frmParamStandardConsumptionNew_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        Me.Text = "Material Consumption Vs Purchase"
        FormatSprdMain(-1)
        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub frmParamStandardConsumptionNew_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection								
        ''PvtDBCn.Open StrConn								
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = VB6.TwipsToPixelsY(24)
        Me.Left = VB6.TwipsToPixelsX(24)
        Me.Height = VB6.TwipsToPixelsY(7440)
        Me.Width = VB6.TwipsToPixelsX(11625)


        '    txtDateFrom.Text = Format(RsCompany!START_DATE, "DD/MM/YYYY")								
        '    txtDateTo.Text = Format(RunDate, "DD/MM/YYYY")								


        txtDate(0).Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDate(1).Text = VB6.Format(RunDate, "DD/MM/YYYY")

        chkFG.CheckState = System.Windows.Forms.CheckState.Checked
        chkFG_CheckStateChanged(chkFG, New System.EventArgs())
        chkAllBOP.CheckState = System.Windows.Forms.CheckState.Checked

        txtBOPName.Enabled = False
        cmdsearchBOPName.Enabled = False

        txtCategory.Enabled = False
        cmdsearchCategory.Enabled = False

        txtSubCategory.Enabled = False
        cmdSubCatsearch.Enabled = False


        Call FillMaterialType()


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
    Private Sub FormatSprdMain(ByRef mRow As Integer)
        Dim cntCol As Integer

        With SprdMain
            .MaxCols = ColPurchasePer ''ColTotalAmount								
            .set_RowHeight(-1, RowHeight)

            .Row = -1
            .set_ColWidth(0, 4)

            '        .Col = ColPicMain								
            '        .CellType = CellTypePicture								
            '        .TypePictCenter = True								
            '        .TypePictMaintainScale = False								
            '        .TypePictStretch = False								
            '								
            '        .Col = ColPicSub								
            '        .CellType = CellTypePicture								
            '        .TypePictCenter = True								
            '        .TypePictMaintainScale = False								
            '        .TypePictStretch = False								
            '        .ColHidden = True								

            .Col = ColCustomerCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .ColHidden = True

            '        .Col = ColCustomerName								
            '        .CellType = SS_CELL_TYPE_EDIT								
            '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII								
            '        .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE								
            '        .TypeEditMultiLine = True								
            '        .ColWidth(ColCustomerName) = 15								
            '        .ColHidden = True								

            .Col = ColMainProd
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .ColHidden = False '''True								

            .Col = ColProductDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .ColHidden = False '''True								

            '

            For cntCol = ColProdOpQty To ColPurchasePer '' ColTotalAmount			
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.999")
                .TypeFloatMin = CDbl("-999999999.999")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntCol, 9)
            Next

            '.Col = ColDespQty
            '.CellType = SS_CELL_TYPE_FLOAT
            '.TypeFloatDecimalPlaces = 2
            '.TypeFloatDecimalChar = Asc(".")
            '.TypeFloatMax = CDbl("999999999.999")
            '.TypeFloatMin = CDbl("-999999999.999")
            '.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            '.set_ColWidth(ColDespQty, 9)
            '.ColHidden = False '''True								

            '.Col = ColProdQty
            '.CellType = SS_CELL_TYPE_FLOAT
            '.TypeFloatDecimalPlaces = 2
            '.TypeFloatDecimalChar = Asc(".")
            '.TypeFloatMax = CDbl("999999999.999")
            '.TypeFloatMin = CDbl("-999999999.999")
            '.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            '.set_ColWidth(ColProdQty, 9)
            '.ColHidden = False '''True								

            .Col = ColRMCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColRMCode, 6)

            .Col = ColRMDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColRMDesc, 25)

            .Col = colStdQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(colStdQty, 7)

            .Col = ColUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColUnit, 4)

            'For cntCol = ColBOMQty To ColPurchasePer '' ColTotalAmount								
            '    .Col = cntCol
            '    .CellType = SS_CELL_TYPE_FLOAT
            '    .TypeFloatDecimalPlaces = 2
            '    .TypeFloatDecimalChar = Asc(".")
            '    .TypeFloatMax = CDbl("999999999.999")
            '    .TypeFloatMin = CDbl("-999999999.999")
            '    .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            '    .set_ColWidth(cntCol, 9)
            'Next

            .ColsFrozen = ColMainProd

            '        .Col = ColRGPINQty								
            '        .ColHidden = True								

            If optDespatch.Checked = True Then
                For cntCol = ColProdOpQty To ColProdCLQty '' ColTotalAmount			
                    .Col = cntCol
                    .ColHidden = False
                Next
            Else
                For cntCol = ColProdOpQty To ColProdCLQty '' ColTotalAmount			
                    .Col = cntCol
                    .ColHidden = True
                Next
                .Col = ColProdQty
                .ColHidden = False
            End If

            Call FillHeading()

            mIsGrouped = False
            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle								

        End With
    End Sub

    Private Sub FillMaterialType()
        On Error GoTo FillErr2
        Dim SqlStr As String
        Dim RS As ADODB.Recordset
        Dim CntLst As Integer
        Dim pCompanyCode As Long
        Dim mRights As String

        lstMaterialType.Items.Clear()
        '    SqlStr = "SELECT GEN_DESC FROM INV_GENERAL_MST " & vbCrLf _								
        ''        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _								
        ''        & " AND GEN_TYPE='C' ORDER BY GEN_DESC"								

        SqlStr = "SELECT GEN_DESC FROM INV_GENERAL_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND GEN_TYPE='C' AND PRD_TYPE IN ('R','B','P','I') ORDER BY GEN_DESC"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If RS.EOF = False Then
            Do While RS.EOF = False
                lstMaterialType.Items.Add(RS.Fields("GEN_DESC").Value)
                lstMaterialType.SetItemChecked(CntLst, False)
                RS.MoveNext()
                CntLst = CntLst + 1
            Loop
        End If

        lstMaterialType.SelectedIndex = 0



        lstCompanyName.Items.Clear()
        SqlStr = "SELECT COMPANY_SHORTNAME, COMPANY_CODE FROM GEN_COMPANY_MST " & vbCrLf _
            & " ORDER BY COMPANY_SHORTNAME"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If RS.EOF = False Then
            lstCompanyName.Items.Add("ALL")
            CntLst = CntLst + 1
            Do While RS.EOF = False
                pCompanyCode = RS.Fields("COMPANY_CODE").Value
                mRights = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn, pCompanyCode)
                If mRights <> "" Then
                    lstCompanyName.Items.Add(RS.Fields("COMPANY_SHORTNAME").Value)
                    lstCompanyName.SetItemChecked(CntLst, IIf(RS.Fields("COMPANY_SHORTNAME").Value = RsCompany.Fields("COMPANY_SHORTNAME").Value, True, False))
                    CntLst = CntLst + 1
                End If
                RS.MoveNext()
            Loop
        End If

        lstCompanyName.SelectedIndex = 0

        cboClass.Items.Clear()
        cboClass.Items.Add("ALL")
        cboClass.Items.Add("A")
        cboClass.Items.Add("B")
        cboClass.Items.Add("C")
        cboClass.Items.Add("DOL")
        cboClass.SelectedIndex = 0

        Exit Sub
FillErr2:
        MsgBox(Err.Description)
    End Sub
    Private Sub FillHeading()
        On Error GoTo ErrPart

        With SprdMain
            .MaxCols = ColPurchasePer ''ColTotalAmount								

            .ColHeaderRows = 2
            '        .AddCellSpan 1, SpreadHeader, 3, 1								
            '        .AddCellSpan 4, SpreadHeader, 3, 1								

            .Row = FPSpreadADO.CoordConstants.SpreadHeader

            .Col = ColRate
            .Text = "Rate"

            .Col = ColVarAmount
            .Text = "Variation Amount"

            .Col = ColPurchaseAmount
            .Text = "Purchase Amount"

            .Col = ColPurchasePer
            .Text = "Purchase / Despatch %"

        End With
        Exit Sub
ErrPart:
        '    Resume								
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamStandardConsumptionNew_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnStock(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportOnStock(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim mTitle As String
        Dim mSubTitle As String
        Dim SqlStr As String

        Report1.Reset()

        If MainClass.FillPrintDummyDataFromSprd(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols, PubDBCn) = False Then GoTo ERR1

        '*************** Fetching Record For Report ***************************								
        SqlStr = ""
        SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")

        mTitle = "Productwise Stock Statement"
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\MatBudget.rpt"

        '    mSubTitle = "As On Date : " & vb6.Format(txtDateTo.Text, "DD/MM/YYYY")								

        If chkFG.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(txtFGName.Text) <> "" Then
            mSubTitle = mSubTitle & " [Category : " & txtFGName.Text & "]"
        End If

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Exit Sub



ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Dim ii As Integer

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnStock(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub txtFGName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFGName.TextChanged
        PrintStatus(False)
    End Sub

    Private Sub txtFGName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFGName.DoubleClick
        Call cmdSearchFG_Click(cmdSearchFG, New System.EventArgs())
    End Sub

    Private Sub txtFGName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFGName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtFGName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtFGName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtFGName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchFG_Click(cmdSearchFG, New System.EventArgs())
    End Sub
    Private Sub txtFGName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtFGName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtFGName.Text) = "" Then GoTo EventExitSub
        '    If MainClass.ValidateWithMasterTable(txtFGName.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = False Then								
        '        MsgBox "Invalid Category Code."								
        '        Cancel = True								
        '    Else								
        '        lblCatCode.Caption = MasterNo								
        '    End If								

        If MainClass.ValidateWithMasterTable(txtFGName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Invalid Item Code.")
            Cancel = True
        End If

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent
        'Sort on specified column or show/collapse rows								

        'Show Summary/Detail info.								
        'If clicked on a "+" or "-" grouping								

        '    If Col = ColPicMain Then								
        '        SprdMain.Col = ColPicMain								
        '        SprdMain.Row = Row								
        '        If SprdMain.CellType = CellTypePicture Then								
        '            'Show or hide the specified rows								
        '            ShowHideRows ColPicMain, Row								
        '        End If								
        '    End If								
    End Sub
    Private Sub ShowHideRows(ByRef Col As Integer, ByRef Row As Integer)
        'Collapse or uncollape the specified rows								
        Dim I As Short
        Dim collapsetype As Short

        SprdMain.Row = Row
        '    SprdMain.Col = ColFlag								

        '    If SprdMain.Text = "0" Then								
        '        collapsetype = 0  'collape/hide rows : minus picture								
        '        SprdMain.Col = 1								
        '        SprdMain.TypePictPicture = pluspict								
        '        SprdMain.Col = ColFlag								
        '        SprdMain.Text = "1"								
        '    Else								
        '        collapsetype = 1  'uncollapse / show rows: plus picture								
        '        SprdMain.Col = 1								
        '        SprdMain.TypePictPicture = minuspict								
        '        SprdMain.Col = ColFlag								
        '        SprdMain.Text = "0"								
        '    End If								

        SprdMain.ReDraw = False
        For I = 1 To SprdMain.GetRowItemData(Row)
            SprdMain.Row = SprdMain.Row + 1
            If collapsetype = 0 Then
                SprdMain.RowHidden = True
            Else
                SprdMain.RowHidden = False
            End If
        Next I
        SprdMain.ReDraw = True

    End Sub
    Private Function GetStockQty(ByRef pItemCode As String, ByRef pPackUnit As String, ByRef pDeptCode As String, ByRef pStockType As String, ByRef pStock_ID As String, ByRef xShowType As String, Optional ByRef pRefType As String = "", Optional ByRef pIO As String = "") As Double

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsDept As ADODB.Recordset
        Dim RsBalStock As ADODB.Recordset
        Dim mBalQty As Double

        Dim RsTemp As ADODB.Recordset
        Dim mIssueUOM As String
        Dim mPurchaseUOM As String
        Dim mFactor As Double
        Dim mTableName As String
        Dim mDeptCode As String

        mDeptCode = ""

        SqlStr = ""

        If pIO = "I" Then
            SqlStr = "SELECT SUM(ITEM_QTY*DECODE(ITEM_IO,'I',1,0)) AS BALQTY"
        ElseIf pIO = "O" Then
            SqlStr = "SELECT SUM(ITEM_QTY*DECODE(ITEM_IO,'I',0,-1)) AS BALQTY"
        Else
            SqlStr = "SELECT SUM(ITEM_QTY*DECODE(ITEM_IO,'I',1,-1)) AS BALQTY"
        End If

        mTableName = ConInventoryTable

        '    If RsCompany.Fields("COMPANY_CODE").Value = 1 Then								
        '        mTableName = "INV_STOCK_REC_TRN" & RsCompany.Fields("FYEAR").Value								
        '    ElseIf RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Or RsCompany.Fields("COMPANY_CODE").Value = 12 Then								
        '        mTableName = "INV_STOCK_REC_TRN" & vb6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00") & RsCompany.Fields("FYEAR").Value								
        '    Else								
        '        mTableName = "INV_STOCK_REC_TRN"								
        '    End If								

        SqlStr = SqlStr & vbCrLf & " FROM " & mTableName & " "

        SqlStr = SqlStr & vbCrLf _
            & " WHERE FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim mCompanyCodeStr As String

        If lstCompanyName.GetItemChecked(0) = True Then
            mCompanyCodeStr = ""
        Else
            For CntLst = 1 To lstCompanyName.Items.Count - 1
                If lstCompanyName.GetItemChecked(CntLst) = True Then
                    mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                    If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_SHORTNAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                        mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
                End If
            Next
        End If

        If mCompanyCodeStr <> "" Then
            mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If


        'SqlStr = SqlStr & vbCrLf & "AND STOCK_ID='" & pStock_ID & "'"

        SqlStr = SqlStr & vbCrLf & "AND STOCK_ID IN ('WH','PH')"

        SqlStr = SqlStr & vbCrLf & " AND STATUS='O'"

        SqlStr = SqlStr & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        'If pDeptCode <> "" And pStock_ID = ConPH Then
        '    SqlStr = SqlStr & vbCrLf & "AND DEPT_CODE_FROM='" & pDeptCode & "'"
        'ElseIf pDeptCode = "PAD" And pStock_ID = ConWH And pStockType = "FG" Then
        '    ''02-08-2006								
        '    'SqlStr = SqlStr & vbCrLf & "AND (DEPT_CODE_FROM='" & pDeptCode & "' OR DEPT_CODE_TO='" & pDeptCode & "')"								
        'End If

        If pRefType <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND REF_TYPE  IN (" & pRefType & ")"
        End If

        'If pStockType = "QC" Then
        '    If xShowType = "OP" Or xShowType = "CL" Then
        '        SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE NOT IN ('SC','CR')"
        '    Else
        '        SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE IN ('ST','" & pStockType & "')"
        '    End If
        'Else
        '    If pStockType = "" Then
        '        '            SqlStr = SqlStr & vbCrLf & " AND E_DATE<=TO_DATE('" & vb6.Format(pDateTo, "dd-mmm-yyyy") & "')"								
        '        SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE <>'CR'"
        '    Else
        '        SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE='" & pStockType & "'" '' AND E_DATE<=TO_DATE('" & vb6.Format(pDateTo, "dd-mmm-yyyy") & "')"								
        '    End If
        'End If

        '    SqlStr = SqlStr & vbCrLf & " AND REF_DATE<=TO_DATE('" & vb6.Format((pDateTo), "DD-MMM-YYYY") & "')"								

        If xShowType = "OP" Then
            SqlStr = SqlStr & vbCrLf & " AND REF_DATE<TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        ElseIf xShowType = "CL" Then
            SqlStr = SqlStr & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtDate(1).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND REF_DATE>=TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            SqlStr = SqlStr & vbCrLf & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtDate(1).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBalStock, ADODB.LockTypeEnum.adLockReadOnly)

        If RsBalStock.EOF = False Then
            If IsDBNull(RsBalStock.Fields(0).Value) Then
                mBalQty = 0
            Else
                mBalQty = RsBalStock.Fields(0).Value
            End If
        Else
            mBalQty = 0
        End If

        RsBalStock = Nothing

        If mBalQty <> 0 Then
            RsTemp = Nothing

            SqlStr = " SELECT ISSUE_UOM, PURCHASE_UOM, UOM_FACTOR FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                mIssueUOM = IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)
                mPurchaseUOM = IIf(IsDBNull(RsTemp.Fields("PURCHASE_UOM").Value), "", RsTemp.Fields("PURCHASE_UOM").Value)
                mFactor = IIf(IsDBNull(RsTemp.Fields("UOM_FACTOR").Value) Or RsTemp.Fields("UOM_FACTOR").Value = 0, 1, RsTemp.Fields("UOM_FACTOR").Value)

                If pPackUnit = mPurchaseUOM Then
                    mBalQty = mBalQty / mFactor
                End If

                RsTemp = Nothing
                '            RsTemp.Close								
            End If
        End If

        GetStockQty = mBalQty

        Exit Function
ErrPart:
        GetStockQty = 0
    End Function



    Private Function GetNetPurchase(ByRef pItemCode As String, ByRef pType As String, Optional ByRef xItemCode As String = "") As Double


        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsDept As ADODB.Recordset
        Dim RsBalStock As ADODB.Recordset = Nothing
        Dim mBalQty As Double

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mIssueUOM As String = ""
        Dim mPurchaseUOM As String = ""
        Dim mFactor As Double
        Dim mTableName As String
        Dim mDeptCode As String

        mDeptCode = ""
        SqlStr = ""
        mTableName = ConInventoryTable
        GetNetPurchase = 0

        SqlStr = "SELECT SUM(ITEM_QTY*DECODE(ITEM_IO,'I',1,-1)) AS BALQTY" & vbCrLf _
            & " FROM " & mTableName & " " & vbCrLf _
            & " WHERE FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND STOCK_ID IN ('WH','PH')" & vbCrLf _
            & " AND STATUS='O'" & vbCrLf _
            & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim mCompanyCodeStr As String

        If lstCompanyName.GetItemChecked(0) = True Then
            mCompanyCodeStr = ""
        Else
            For CntLst = 1 To lstCompanyName.Items.Count - 1
                If lstCompanyName.GetItemChecked(CntLst) = True Then
                    mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                    If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_SHORTNAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                        mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
                End If
            Next
        End If

        If mCompanyCodeStr <> "" Then
            mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If

        If pType = "P" Then
            SqlStr = SqlStr & vbCrLf & " AND REF_TYPE  IN ('MRR') AND REF_FLAG NOT IN ('I','R') AND STOCK_TYPE<>'SC'"
        ElseIf pType = "D" Then
            SqlStr = SqlStr & vbCrLf & " AND REF_TYPE  IN ('DSP','RGP','NRG') AND STOCK_TYPE<>'SC'"
        ElseIf pType = "R" Then
            SqlStr = SqlStr & vbCrLf & " AND REF_TYPE  IN ('RGP','NRG') AND STOCK_TYPE<>'SC'"
        ElseIf pType = "PD" Then
            SqlStr = SqlStr & vbCrLf & " AND REF_TYPE  IN ('DSP') AND STOCK_TYPE<>'SC'"
        ElseIf pType = "I" Then
            SqlStr = SqlStr & vbCrLf & " AND REF_TYPE  IN ('MRR')  AND REF_FLAG='I' "
        ElseIf pType = "J" Then
            SqlStr = SqlStr & vbCrLf & " AND REF_TYPE  IN ('MRR') AND REF_FLAG='R' AND STOCK_TYPE<>'SC'"
        ElseIf pType = "A" Then
            SqlStr = SqlStr & vbCrLf & " AND REF_TYPE  IN ('ADJ') AND STOCK_TYPE<>'SC'"
        ElseIf pType = "S" Then
            SqlStr = SqlStr & vbCrLf & " AND STOCK_TYPE='SC'"
        ElseIf pType = "PMD" Then
            SqlStr = SqlStr & vbCrLf & " AND REF_TYPE='" & ConStockRefType_PMEMODEPT & "' AND ITEM_IO='I'"

            If xItemCode <> "" Then
                SqlStr = SqlStr & vbCrLf & " AND REF_ITEM_CODE='" & MainClass.AllowSingleQuote(xItemCode) & "'"
            End If
        End If

        SqlStr = SqlStr & vbCrLf _
            & " AND REF_DATE>=TO_DATE('" & VB6.Format(txtDate(0).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND REF_DATE<=TO_DATE('" & VB6.Format(txtDate(1).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBalStock, ADODB.LockTypeEnum.adLockReadOnly)


        If RsBalStock.EOF = False Then
            Do While RsBalStock.EOF = False
                mBalQty = 0
                If IsDBNull(RsBalStock.Fields(0).Value) Then
                    mBalQty = 0
                Else
                    mBalQty = RsBalStock.Fields(0).Value
                End If
                GetNetPurchase = GetNetPurchase + mBalQty
                RsBalStock.MoveNext()
            Loop
        Else
            mBalQty = 0
        End If


        RsBalStock = Nothing


        Exit Function
ErrPart:
        GetNetPurchase = 0
    End Function

    Private Function GetDespatchQty(ByRef pQry As String, ByRef xItemCode As String, ByRef xItemDesc As String,
                                    ByRef xItemUOM As String, ByRef mCurrRow As Integer) As Boolean


        ', ByRef pTotalDespatch As Double,
        '                            ByRef pTotalProductionQty As Double, ByRef pTotalWIPOPQty As Double, ByRef pTotalWIPCLQty As Double,
        '                            ByRef pTotalWIPOUTQty As Double, ByRef pTotalPhyWIPQty As Double, ByRef pTotalWIPAdjQty As Double,
        '                            ByRef pTotalScrapQty As Double, ByRef pTotalCRRecdQty As Double,
        '                            ByRef pTotalProdOpQty As Double, ByRef mTotalWIPPurQty As Double, ByRef mTotalWIPJWQty As Double,
        '                            ByRef mTotaWIPRGPQty As Double, ByRef mTotalWIPScrapQty As Double, ByRef mTotalWIPAdjQty As Double, ByRef mTotaCRRecdQty As Double) As Double
        '''GetDespatchQty(pItemCode As String) As Double								
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mParentcode As String
        Dim mChildCode As String
        Dim mStdQty As Double
        Dim mLevel As Integer

        Dim pItemUOM As String
        Dim mSqlStrRel As String
        Dim RsRel As ADODB.Recordset
        Dim xProductRelCode As String
        Dim pProductDesc As String

        Dim mOPWIPQty As Double
        Dim mWIPPurQty As Double
        Dim mWIPJWQty As Double
        Dim mWIPRGPQty As Double
        Dim mWIPScrapQty As Double
        Dim mDespQty As Double
        Dim mProdQty As Double
        Dim mCRRecdQty As Double
        Dim mWIPAdjQty As Double
        Dim mCLWIPQty As Double
        Dim mWIPPhyQty As Double

        Dim mDeptCode As String

        Dim mItemLevelStdQty() As Double


        GetDespatchQty = True

        mOPWIPQty = 0
        mWIPPurQty = 0
        mWIPJWQty = 0
        mWIPRGPQty = 0
        mWIPScrapQty = 0
        mDespQty = 0
        mProdQty = 0
        mCRRecdQty = 0
        mWIPAdjQty = 0
        mCLWIPQty = 0
        mWIPPhyQty = 0



        MainClass.UOpenRecordSet(pQry, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        mStdQty = 1
        ReDim mItemLevelStdQty(1000)
        '    mIsFirstRecord = True								
        If RsTemp.EOF = False Then
            Do While Not RsTemp.EOF

                mLevel = Val(IIf(IsDBNull(RsTemp.Fields("Level").Value), 1, RsTemp.Fields("Level").Value))

                If mLevel = 1 Then
                    mStdQty = CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("STD_QTY").Value), "", RsTemp.Fields("STD_QTY").Value), "0.0000"))
                Else
                    mStdQty = mItemLevelStdQty(mLevel - 1) * CDbl(VB6.Format(IIf(IsDBNull(RsTemp.Fields("STD_QTY").Value), "", RsTemp.Fields("STD_QTY").Value), "0.0000"))
                End If
                mItemLevelStdQty(mLevel) = mStdQty


                mParentcode = Trim(IIf(IsDBNull(RsTemp.Fields("PRODUCT_CODE").Value), "", RsTemp.Fields("PRODUCT_CODE").Value))
                mDeptCode = Trim(IIf(IsDBNull(RsTemp.Fields("DEPT_CODE").Value), "", RsTemp.Fields("DEPT_CODE").Value))

                If MainClass.ValidateWithMasterTable(mParentcode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    pProductDesc = Trim(MasterNo)
                End If

                If MainClass.ValidateWithMasterTable(mParentcode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    pItemUOM = Trim(MasterNo)
                End If

                If optDespatch.Checked = True Then
                    mOPWIPQty = GetStockQty(mParentcode, pItemUOM, mDeptCode, "ST", ConPH, "OP")
                    mWIPPurQty = GetNetPurchase(mParentcode, "P")
                    mWIPJWQty = GetNetPurchase(mParentcode, "J")
                    mWIPRGPQty = GetNetPurchase(mParentcode, "R")
                    mWIPScrapQty = GetNetPurchase(mParentcode, "S")  ''System.Math.Abs(GetWIPProductionQty(mParentcode, pItemUOM, mDeptCode, "SCP"))
                    mDespQty = GetNetPurchase(mParentcode, "PD")
                    mCRRecdQty = GetNetPurchase(mParentcode, "I")
                    mWIPAdjQty = GetNetPurchase(mParentcode, "A") ' GetWIPProductionQty(mParentcode, pItemUOM, mDeptCode, "ADJ")
                    mCLWIPQty = GetStockQty(mParentcode, pItemUOM, mDeptCode, "ST", ConPH, "CL") ''GetWIPProductionQty(mParentcode, pItemUOM, mDeptCode, "CL")
                    mWIPPhyQty = GetPhysicalQty(mParentcode, mDeptCode, ConPH)
                    mWIPPhyQty = mWIPPhyQty + GetPhysicalQty(mParentcode, "", ConWH) ''+ GetStockQty(mParentcode, pItemUOM, "", "FG", ConWH, "CL")	
                End If

                If mLevel = 1 Then
                    mProdQty = GetNetPurchase(mParentcode, "PMD", xItemCode)
                Else
                    mProdQty = 0
                End If


                With SprdMain
                    .Row = mCurrRow

                    .Col = ColRMCode
                    .Text = xItemCode

                    .Col = ColRMDesc
                    .Text = xItemDesc

                    .Col = ColUnit
                    .Text = xItemUOM

                    .Col = ColMainProd
                    .Text = mParentcode

                    .Col = ColProductDesc
                    .Text = pProductDesc

                    .Col = colStdQty
                    .Text = CStr(mStdQty)

                    .Col = ColProdOpQty
                    .Text = CStr(mOPWIPQty)

                    .Col = ColProdPurQty
                    .Text = CStr(mWIPPurQty)

                    .Col = ColProdJobWorker
                    .Text = CStr(mWIPJWQty)

                    .Col = ColProdRGPQty
                    .Text = CStr(mWIPRGPQty)

                    .Col = ColProdScrapQty
                    .Text = CStr(mWIPScrapQty)

                    .Col = ColDespQty
                    .Text = CStr(mDespQty)

                    .Col = ColProdQty
                    .Text = CStr(mProdQty)

                    .Col = ColProdSRQty
                    .Text = CStr(mCRRecdQty)

                    .Col = ColProdAdjQty
                    .Text = CStr(mWIPAdjQty)

                    .Col = ColProdCLQty
                    .Text = CStr(mCLWIPQty)

                    .Col = ColTotalProdOpQty
                    .Text = CStr(mOPWIPQty * mStdQty)

                    .Col = ColTotalProdPurQty
                    .Text = CStr(mWIPPurQty * mStdQty)

                    .Col = ColTotalProdJobWorker
                    .Text = CStr(mWIPJWQty * mStdQty)

                    .Col = ColTotalProdRGPQty
                    .Text = CStr(mWIPRGPQty * mStdQty)

                    .Col = ColTotalProdScrapQty
                    .Text = CStr(mWIPScrapQty * mStdQty)

                    .Col = ColTotalDespQty
                    .Text = CStr(mDespQty * mStdQty)

                    .Col = ColTotalProdQty
                    .Text = CStr(mProdQty * mStdQty)

                    .Col = ColTotalProdSRQty
                    .Text = CStr(mCRRecdQty * mStdQty)

                    .Col = ColTotalProdAdjQty
                    .Text = CStr(mWIPAdjQty * mStdQty)

                    .Col = ColERPCLQty
                    .Text = CStr(mCLWIPQty * mStdQty)

                    .MaxRows = .MaxRows + 1
                    mCurrRow = mCurrRow + 1
                End With

                'mDespQty = mDespQty * mStdQty
                'mProdQty = mProdQty * mStdQty


                mSqlStrRel = GetRelationItem(mParentcode)
                If mSqlStrRel <> "" Then
                    MainClass.UOpenRecordSet(mSqlStrRel, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRel, ADODB.LockTypeEnum.adLockReadOnly)
                    If RsRel.EOF = False Then
                        Do While RsRel.EOF = False
                            xProductRelCode = Trim(IIf(IsDBNull(RsRel.Fields("REF_ITEM_CODE").Value), "", RsRel.Fields("REF_ITEM_CODE").Value))

                            If optDespatch.Checked = True Then
                                mOPWIPQty = GetStockQty(xProductRelCode, pItemUOM, mDeptCode, "ST", ConPH, "OP") '' GetWIPProductionQty(mParentcode, pItemUOM, mDeptCode, "OP")
                                mWIPPurQty = GetNetPurchase(xProductRelCode, "P")
                                mWIPJWQty = GetNetPurchase(xProductRelCode, "J")
                                mWIPRGPQty = GetNetPurchase(xProductRelCode, "R")
                                mWIPScrapQty = GetNetPurchase(xProductRelCode, "S")  ''System.Math.Abs(GetWIPProductionQty(mParentcode, pItemUOM, mDeptCode, "SCP"))
                                mDespQty = GetNetPurchase(xProductRelCode, "PD")

                                mCRRecdQty = GetNetPurchase(xProductRelCode, "I")
                                mWIPAdjQty = GetNetPurchase(xProductRelCode, "A") ' GetWIPProductionQty(mParentcode, pItemUOM, mDeptCode, "ADJ")
                                mCLWIPQty = GetStockQty(xProductRelCode, pItemUOM, mDeptCode, "ST", ConPH, "CL") ''GetWIPProductionQty(mParentcode, pItemUOM, mDeptCode, "CL")
                                mWIPPhyQty = GetPhysicalQty(xProductRelCode, mDeptCode, ConPH)
                                mWIPPhyQty = mWIPPhyQty + GetPhysicalQty(xProductRelCode, "", ConWH) ''+ GetStockQty(mParentcode, pItemUOM, "", "FG", ConWH, "CL")	
                            End If

                            If mLevel = 1 Then
                                mProdQty = GetNetPurchase(xProductRelCode, "PMD", xItemCode) '
                            Else
                                mProdQty = 0
                            End If

                            With SprdMain
                                .Row = mCurrRow

                                .Col = ColRMCode
                                .Text = xItemCode

                                .Col = ColRMDesc
                                .Text = xItemDesc

                                .Col = ColUnit
                                .Text = xItemUOM

                                .Col = ColMainProd
                                .Text = xProductRelCode

                                If MainClass.ValidateWithMasterTable(xProductRelCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                                    pProductDesc = Trim(MasterNo)
                                End If
                                .Col = ColProductDesc
                                .Text = pProductDesc

                                .Col = colStdQty
                                .Text = CStr(mStdQty)

                                .Col = ColProdOpQty
                                .Text = CStr(mOPWIPQty)

                                .Col = ColProdPurQty
                                .Text = CStr(mWIPPurQty)

                                .Col = ColProdJobWorker
                                .Text = CStr(mWIPJWQty)

                                .Col = ColProdRGPQty
                                .Text = CStr(mWIPRGPQty)

                                .Col = ColProdScrapQty
                                .Text = CStr(mWIPScrapQty)

                                .Col = ColDespQty
                                .Text = CStr(mDespQty)

                                .Col = ColProdQty
                                .Text = CStr(mProdQty)

                                .Col = ColProdSRQty
                                .Text = CStr(mCRRecdQty)

                                .Col = ColProdAdjQty
                                .Text = CStr(mWIPAdjQty)

                                .Col = ColProdCLQty
                                .Text = CStr(mCLWIPQty)

                                .Col = ColTotalProdOpQty
                                .Text = CStr(mOPWIPQty * mStdQty)

                                .Col = ColTotalProdPurQty
                                .Text = CStr(mWIPPurQty * mStdQty)

                                .Col = ColTotalProdJobWorker
                                .Text = CStr(mWIPJWQty * mStdQty)

                                .Col = ColTotalProdRGPQty
                                .Text = CStr(mWIPRGPQty * mStdQty)

                                .Col = ColTotalProdScrapQty
                                .Text = CStr(mWIPScrapQty * mStdQty)

                                .Col = ColTotalDespQty
                                .Text = CStr(mDespQty * mStdQty)

                                .Col = ColTotalProdQty
                                .Text = CStr(mProdQty * mStdQty)

                                .Col = ColTotalProdSRQty
                                .Text = CStr(mCRRecdQty * mStdQty)

                                .Col = ColTotalProdAdjQty
                                .Text = CStr(mWIPAdjQty * mStdQty)

                                .Col = ColERPCLQty
                                .Text = CStr(mCLWIPQty * mStdQty)

                                .Col = ColWIPPhyQty
                                .Text = CStr(mWIPPhyQty * mStdQty)

                                .MaxRows = .MaxRows + 1
                                mCurrRow = mCurrRow + 1
                            End With

                            RsRel.MoveNext()
                        Loop
                    End If
                End If
                RsTemp.MoveNext()
            Loop
        End If


        Exit Function
ErrPart:
        GetDespatchQty = False
    End Function

    Private Function GetPhysicalQty(ByRef mProductCode As String, ByRef mDeptCode As String, ByRef pStockID As String) As Double
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mDeptSeq As Integer
        Dim mMaxDepSeq As Integer
        Dim pDeptCode As String
        Dim pDeptCodeStr As String
        Dim I As Integer

        If pStockID = ConPH Then
            If mDeptCode = "" Then
                pDeptCodeStr = ""
            Else
                mDeptSeq = GetProductSeqNo(mProductCode, mDeptCode, txtDate(0).Text)
                mMaxDepSeq = GetMaxProductSeqNo(mProductCode, txtDate(0).Text)

                For I = mDeptSeq To mMaxDepSeq
                    pDeptCode = GetProductDept(mProductCode, I, txtDate(0).Text)
                    If pDeptCodeStr = "" Then
                        pDeptCodeStr = pDeptCodeStr & "('" & pDeptCode & "'"
                    Else
                        pDeptCodeStr = pDeptCodeStr & ", '" & pDeptCode & "'"
                    End If
                Next
                pDeptCodeStr = pDeptCodeStr & ")"
            End If
        End If

        SqlStr = " SELECT SUM(PHY_QTY) AS PHY_QTY " & vbCrLf _
            & " FROM INV_PHY_HDR IH, INV_PHY_DET ID" & vbCrLf _
            & " WHERE IH.AUTO_KEY_PHY=ID.AUTO_KEY_PHY" & vbCrLf _
            & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'" & vbCrLf _
            & " AND IH.PHY_DATE=TO_DATE('" & VB6.Format(txtDate(1).Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim mCompanyCodeStr As String

        If lstCompanyName.GetItemChecked(0) = True Then
            mCompanyCodeStr = ""
        Else
            For CntLst = 1 To lstCompanyName.Items.Count - 1
                If lstCompanyName.GetItemChecked(CntLst) = True Then
                    mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                    If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_SHORTNAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                        mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
                End If
            Next
        End If

        If mCompanyCodeStr <> "" Then
            mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If



        If pStockID = ConPH Then
            '        SqlStr = SqlStr & vbCrLf & " AND ID.STOCK_TYPE='ST'"								
        Else
            '        SqlStr = SqlStr & vbCrLf & " AND ID.STOCK_TYPE IN ('FG','ST')"								
        End If

        SqlStr = SqlStr & vbCrLf & " AND IH.BOOKTYPE='" & pStockID & "'"

        If pStockID = ConPH And pDeptCodeStr <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND IH.DEPT_CODE IN " & pDeptCodeStr & ""
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetPhysicalQty = IIf(IsDBNull(RsTemp.Fields("PHY_QTY").Value), 0, RsTemp.Fields("PHY_QTY").Value)
        End If

        Exit Function
ErrPart:
        GetPhysicalQty = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function GetWIPProductionQty(ByRef mProductCode As String, ByRef mItemUOM As String, ByRef mDeptCode As String, ByRef mFieldName As String) As Double
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mDeptSeq As Integer
        Dim mMaxDepSeq As Integer
        Dim pDeptCode As String
        Dim I As Integer


        mDeptSeq = GetProductSeqNo(mProductCode, mDeptCode, txtDate(0).Text)
        mMaxDepSeq = GetMaxProductSeqNo(mProductCode, txtDate(0).Text)

        If mFieldName = "ADJ" Then
            For I = mDeptSeq To mMaxDepSeq
                pDeptCode = GetProductDept(mProductCode, I, txtDate(0).Text)
                GetWIPProductionQty = GetWIPProductionQty + GetStockQty(mProductCode, mItemUOM, pDeptCode, "ST", ConPH, mFieldName, "'" & ConStockRefType_ADJ & "'")
                GetWIPProductionQty = GetWIPProductionQty + GetStockQty(mProductCode, mItemUOM, pDeptCode, "WR", ConPH, mFieldName, "'" & ConStockRefType_ADJ & "'")
            Next

            '        If mDeptSeq = mMaxDepSeq Then								
            ''TEMP COMMITED...  29-04-2011								
            GetWIPProductionQty = GetWIPProductionQty + GetStockQty(mProductCode, mItemUOM, "", "", ConWH, mFieldName, "'" & ConStockRefType_ADJ & "'")
            GetWIPProductionQty = GetWIPProductionQty + GetStockQty(mProductCode, mItemUOM, "", "WR", ConWH, mFieldName, "'" & ConStockRefType_ADJ & "'")
            '        Else								
            '            ''TEMP COMMITED...  29-04-2011								
            '            GetWIPProductionQty = GetWIPProductionQty + GetStockQty(mProductCode, mItemUOM, "", "", ConWH, mFieldName, "'" & ConStockRefType_ADJ & "'")								
            '        End If								
        ElseIf mFieldName = "SCP" Then
            GetWIPProductionQty = GetWIPProductionQty + GetStockQty(mProductCode, mItemUOM, "", "", ConWH, mFieldName, "'" & ConStockRefType_SCP & "'")
        ElseIf mFieldName = "OP_CR" Then
            GetWIPProductionQty = GetWIPProductionQty + GetStockQty(mProductCode, mItemUOM, "", "CR", ConWH, "OP")
        ElseIf mFieldName = "CL_CR" Then
            GetWIPProductionQty = GetWIPProductionQty + GetStockQty(mProductCode, mItemUOM, "", "CR", ConWH, "CL")
        ElseIf mFieldName = "DSP_CR" Then
            GetWIPProductionQty = GetWIPProductionQty + GetStockQty(mProductCode, mItemUOM, "", "CR", ConWH, "", "'" & ConStockRefType_DSP & "','" & ConStockRefType_NRG & "','" & ConStockRefType_RGP & "'")
        Else
            For I = mDeptSeq To mMaxDepSeq
                pDeptCode = GetProductDept(mProductCode, I, txtDate(0).Text)
                GetWIPProductionQty = GetWIPProductionQty + GetStockQty(mProductCode, mItemUOM, pDeptCode, "ST", ConPH, mFieldName)
                GetWIPProductionQty = GetWIPProductionQty + GetStockQty(mProductCode, mItemUOM, pDeptCode, "WR", ConPH, mFieldName)
            Next

            '        If mDeptSeq = mMaxDepSeq Then								
            ''TEMP COMMITED...  29-04-2011								
            GetWIPProductionQty = GetWIPProductionQty + GetStockQty(mProductCode, mItemUOM, "", "", ConWH, mFieldName)
            GetWIPProductionQty = GetWIPProductionQty - GetStockQty(mProductCode, mItemUOM, "", "WR", ConWH, mFieldName)
            '        Else								
            '            ''TEMP COMMITED...  29-04-2011								
            '            GetWIPProductionQty = GetWIPProductionQty + GetStockQty(mProductCode, mItemUOM, "", "", ConWH, mFieldName)								
            '        End If								
        End If
        ''GetStockQty								
        Exit Function
ErrPart:
        GetWIPProductionQty = 0
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function DespatchSqlQry(ByRef pItemCode As String) As String
        On Error GoTo ErrPart
        Dim SqlStr As String

        ''TRN.RM_CODE,								
        ''    ''TEMP_BOM								

        ''DISTINCT								

        DespatchSqlQry = ""
        SqlStr = " SELECT  " & vbCrLf _
            & " LEVEL,TRN.PRODUCT_CODE, TRN.STD_QTY+  GROSS_WT_SCRAP AS STD_QTY, DEPT_CODE" & vbCrLf _
            & " FROM VW_PRD_BOM_TRN TRN" & vbCrLf _
            & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='O'"

        SqlStr = SqlStr & vbCrLf _
            & " START WITH  TRIM(RM_CODE) || '-' || TRN.COMPANY_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "-" & RsCompany.Fields("COMPANY_CODE").Value & "'" & vbCrLf _
            & " CONNECT BY PRIOR (TRIM(PRODUCT_CODE) || COMPANY_CODE) || ' '=TRIM(RM_CODE) || COMPANY_CODE || ' '"

        DespatchSqlQry = SqlStr

        Exit Function
ErrPart:
        DespatchSqlQry = ""
    End Function
    Private Function GetRelationItem(ByRef mProductCode As String) As String
        On Error GoTo ErrPart


        GetRelationItem = " SELECT REF_ITEM_CODE , ITEM_UOM " & vbCrLf _
            & " FROM INV_ITEM_RELATIONSHIP_DET " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'"


        Exit Function
ErrPart:
        GetRelationItem = ""
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub lstCompanyName_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles lstCompanyName.ItemCheck

        Try
            If mClickProcess = True Then Exit Sub
            mClickProcess = True

            If e.Index = 0 Then
                If e.NewValue = System.Windows.Forms.CheckState.Checked Then     ''lstCompanyName.GetItemChecked(0) = True Then
                    For I = 1 To lstCompanyName.Items.Count - 1
                        lstCompanyName.SetItemChecked(I, True)
                    Next
                Else
                    For I = 1 To lstCompanyName.Items.Count - 1
                        lstCompanyName.SetItemChecked(I, False)
                    Next
                End If
            Else
                If e.NewValue = System.Windows.Forms.CheckState.Unchecked Then      ''lstCompanyName.GetItemChecked(e.Index - 1) = False Then
                    lstCompanyName.SetItemChecked(0, False)
                End If
            End If
            mClickProcess = False
        Catch ex As Exception

        End Try
    End Sub
End Class
