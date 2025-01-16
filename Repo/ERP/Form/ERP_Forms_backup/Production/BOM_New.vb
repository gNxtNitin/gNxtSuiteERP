Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class FrmBOMNew
    Inherits System.Windows.Forms.Form
    Dim RsBOMMain As ADODB.Recordset
    Dim RsBOMDetail As ADODB.Recordset
    Dim RsToolDetail As ADODB.Recordset
    Dim RsConsumableDetail As ADODB.Recordset
    Dim RsProdSeqMain As ADODB.Recordset
    Dim RsProdSeqDetail As ADODB.Recordset
    Dim RsTransMain As ADODB.Recordset ''Recordset
    Dim RsTransDetail As ADODB.Recordset ''Recordset
    Dim RsBOMOtherDetail As ADODB.Recordset

    Dim FileDBCn As ADODB.Connection

    'Dim PvtDBCn As ADODB.Connection

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

    Private Const ConRowHeight As Short = 24

    Private Const ColDeptCode As Short = 1
    Private Const ColRMCode As Short = 2
    Private Const ColRMDesc As Short = 3
    Private Const ColPartNo As Short = 4
    Private Const ColQUnit As Short = 5
    Private Const colStdQty As Short = 6
    Private Const ColGrossWtScrp As Short = 7
    Private Const ColWtVar As Short = 8
    Private Const ColStockType As Short = 9
    Private Const ColAlternate As Short = 10
    Private Const ColOperation As Short = 11

    Private Const ColToolCode As Short = 1
    Private Const ColToolDesc As Short = 2
    Private Const ColToolQUnit As Short = 3
    Private Const ColToolDeptCode As Short = 4
    Private Const colToolStdQty As Short = 5
    Private Const ColToolLife As Short = 6
    Private Const ColToolRemarks As Short = 7

    Private Const ColConsumableCode As Short = 1
    Private Const ColConsumableDesc As Short = 2
    Private Const ColConsumableUnit As Short = 3
    Private Const ColConsumableDeptCode As Short = 4
    Private Const colConsumableOnQty As Short = 5
    Private Const ColConsumableQty As Short = 6
    Private Const ColConsumableRemarks As Short = 7

    Private Const ColDept As Short = 1
    Private Const ColDeptDesc As Short = 2
    Private Const ColOPRN As Short = 3
    Private Const ColMinQty As Short = 4
    Private Const ColMaxQty As Short = 5

    Private Const ColRelItemCode As Short = 1
    Private Const ColRelItemDesc As Short = 2
    Private Const ColRelRemarks As Short = 3

    Private Const ColIsAlterOTH As Short = 1
    Private Const ColMainItemCodeOTH As Short = 2
    Private Const ColItemCodeOTH As Short = 3
    Private Const ColItemDescOTH As Short = 4
    Private Const ColQUnitOTH As Short = 5
    Private Const ColDeptCodeOTH As Short = 6
    Private Const ColOprOTH As Short = 7
    Private Const ColRemarkOTH As Short = 8
    Private Const colStdQtyOTH As Short = 9
    Private Const colStdConsOTH As Short = 10
    Private Const ColNetConsumptionOTH As Short = 11



    Dim mAmendStatus As Boolean

    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo AssignGridErr
        Dim SqlStr As String = ""
        SqlStr = ""
        SqlStr = "SELECT DECODE(IH.BOM_TYPE,'P','PRODUCTION','JOBWORK') AS BOM_TYPE, AMEND_NO,IH.PRODUCT_CODE, INVMST.ITEM_SHORT_DESC, TO_CHAR(WEF,'DD/MM/YYYY') AS WEF " & vbCrLf & " FROM PRD_NEWBOM_HDR IH, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND IH.PRODUCT_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND BOM_TYPE='" & lblType.Text & "'" & vbCrLf & " ORDER BY BOM_TYPE, INVMST.ITEM_SHORT_DESC, AMEND_NO "

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
            .set_ColWidth(0, 5)
            .set_ColWidth(1, 12)
            .set_ColWidth(2, 6)
            .set_ColWidth(3, 9)
            .set_ColWidth(4, 40)
            .set_ColWidth(5, 10)

            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub

    Private Function CheckDuplicateItem(ByRef pSprd As AxFPSpreadADO.AxfpSpread, ByRef pRMCode As String, ByRef pDeptCode As String) As Boolean
        On Error GoTo ERR1
        Dim i As Integer
        Dim mItemRept As Integer

        If Trim(pRMCode) = "" Then CheckDuplicateItem = False : Exit Function
        With pSprd
            For i = 1 To .MaxRows
                .Row = i
                .Col = ColRMCode
                If UCase(Trim(.Text)) = UCase(Trim(pRMCode)) Then
                    .Col = ColDeptCode
                    If UCase(Trim(.Text)) = UCase(Trim(pDeptCode)) Then
                        mItemRept = mItemRept + 1
                        If mItemRept > 1 Then
                            MsgInformation("Duplication Item in the Same Department")
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

    Private Function FieldsVarification() As Boolean

        On Error GoTo err_Renamed
        Dim pMainItemCode As String
        Dim mCategory As String
        Dim cntRow As Integer
        Dim cntRowMain As Integer
        Dim CntMainRow As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mItemCode As String
        Dim mRMCode As String
        Dim mRMCategory As String
        Dim mMainItemCode As String
        Dim mDeptCode As String

        FieldsVarification = True
        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsBOMMain.EOF = True Then Exit Function

        If MODIFYMode = True And chkStatus.CheckState = System.Windows.Forms.CheckState.Checked And chkStatus.Enabled = False Then
            MsgInformation("Closed BOM Cann't be Modified")
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

        If Trim(txtProductDesc.Text) = "" Then
            MsgBox("Product Desc is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtProductDesc.Enabled = True Then txtProductDesc.Focus()
            Exit Function
        End If
        If Trim(txtUnit.Text) = "" Then
            MsgBox("Unit is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtUnit.Enabled = True Then txtUnit.Focus()
            Exit Function
        End If

        If Val(txtOutPutQty.Text) <= 0 Then
            MsgBox("Invalid Output Qty", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtOutPutQty.Enabled = True Then txtOutPutQty.Focus()
            Exit Function
        End If

        If Trim(txtPreparedBy.Text) = "" Then
            MsgBox("Prepared By is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtPreparedBy.Focus()
            Exit Function
        End If

        If Trim(cboProcessType.Text) = "" Or cboProcessType.SelectedIndex = -1 Then
            MsgBox("Process Type cann't Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            cboProcessType.Focus()
            Exit Function
        End If

        If cboProcessType.SelectedIndex = 1 Or cboProcessType.SelectedIndex = 2 Then
            If Val(txtSA.Text) = 0 Then
                MsgBox("Surface Area cann't Blank", MsgBoxStyle.Information)
                FieldsVarification = False
                txtSA.Focus()
                Exit Function
            End If
        End If

        If CheckLastOpenBOM("") = True Then
            MsgBox("BOM Already Entered For This Item.", MsgBoxStyle.Information)
            FieldsVarification = False
            txtProductCode.Focus()
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

        If chkApproved.CheckState = System.Windows.Forms.CheckState.Checked And Trim(txtApprovedBy.Text) = "" Then
            MsgBox("Approved By cann't be Blank, Please enter Approved Name.", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtApprovedBy.Enabled = True Then txtApprovedBy.Focus()
            Exit Function
        End If

        If chkStatus.Enabled = False And chkStatus.CheckState = System.Windows.Forms.CheckState.Checked Then
            If chkApproved.Enabled = True And chkApproved.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgBox("Closed BOM cann't be approved.", MsgBoxStyle.Information)
                FieldsVarification = False
                If txtApprovedBy.Enabled = True Then txtApprovedBy.Focus()
                Exit Function
            End If
        End If

        mCategory = GetProductionType(Trim(txtProductCode.Text))

        'If lblType.Text = "P" And mCategory = "J" Then
        '    MsgBox("You cann't be save Third party B.O.M.", MsgBoxStyle.Information)
        '    FieldsVarification = False
        '    Exit Function
        'End If

        'If lblType.Text = "J" And mCategory <> "J" Then
        '    MsgBox("You cann't be save regular B.O.M.", MsgBoxStyle.Information)
        '    FieldsVarification = False
        '    Exit Function
        'End If

        'If chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked Then
        '    If (mCategory = "B" Or mCategory = "R" Or mCategory = "3") Then
        '        If chkBOP.CheckState = System.Windows.Forms.CheckState.Unchecked Then
        '            MsgInformation("Please check Product Category. You Defined BOP/Raw Material Category.")
        '            FieldsVarification = False
        '            Exit Function
        '        Else
        '            If CheckPurchaseOrder((txtWEF.Text), Trim(txtProductCode.Text)) = False Then
        '                MsgInformation("Purchase Order is Not Aviable, so cann't be select BOP.")
        '                FieldsVarification = False
        '                Exit Function
        '            End If
        '        End If
        '    End If

        '    If chkBOP.CheckState = System.Windows.Forms.CheckState.Checked Then
        '        If (mCategory = "B" Or mCategory = "R" Or mCategory = "3") Then

        '        Else
        '            MsgInformation("Category is not BOP/Raw Material.Please unchecked from BOP.")
        '            FieldsVarification = False
        '            Exit Function
        '        End If
        '    Else
        '        If mCategory = "I" Or mCategory = "P" Or mCategory = "J" Then

        '        Else
        '            MsgInformation("Please check Product Category. Category Should be Inhouse Or Production")
        '            FieldsVarification = False
        '            Exit Function
        '        End If
        '    End If

        'End If

        If CheckItemConsumptionExists(Trim(txtProductCode.Text), "N") = True Then
            If MsgQuestion("Outward Jobwork Consumption is also Available.Want to Continue ? ") = CStr(MsgBoxResult.No) Then
                FieldsVarification = False
                Exit Function
            End If
        End If

        If CheckItemRelation(pMainItemCode) = True Then
            MsgBox("Product relationship with " & pMainItemCode & ". Cann't be save", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If


        If CheckProcessDept() = False Then
            FieldsVarification = False
            Exit Function
        End If

        If CheckOperation() = False Then
            FieldsVarification = False
            Exit Function
        End If

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColRMCode
                mRMCode = Trim(.Text)

                If ValidateMainCode(mRMCode) = False Then
                    FieldsVarification = False
                    Exit Function
                End If

                If mRMCode <> "" Then
                    mRMCategory = GetProductionType(Trim(mRMCode))

                    If (mRMCategory = "G" Or mRMCategory = "T" Or mRMCategory = "A") Then
                        MsgInformation("Please check RM/BOP Item Category for Item : " & mRMCode & ". ")
                        FieldsVarification = False
                        Exit Function
                    End If
                End If
            Next
        End With

        With SprdTool
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColToolCode
                mRMCode = Trim(.Text)

                If mRMCode <> "" Then
                    mRMCategory = GetProductionType(Trim(mRMCode))

                    If mRMCategory <> "T" Then
                        MsgInformation("Please check Item Category for Item : " & mRMCode & ". ")
                        FieldsVarification = False
                        Exit Function
                    End If
                End If
            Next
        End With

        With SprdOthers
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColConsumableCode
                mRMCode = Trim(.Text)

                If mRMCode <> "" Then
                    mRMCategory = GetProductionType(Trim(mRMCode))

                    If mRMCategory <> "G" Then
                        MsgInformation("Please check Item Category for Item : " & mRMCode & ". ")
                        FieldsVarification = False
                        Exit Function
                    End If
                End If
            Next
        End With

        With SprdMainRel
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColRelItemCode
                mItemCode = Trim(.Text)
                If mItemCode <> "" Then
                    SqlStr = "SELECT PRODUCT_CODE FROM PRD_NEWBOM_HDR" & vbCrLf _
                        & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
                        & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' AND STATUS='O'"

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                    If RsTemp.EOF = False Then
                        MsgInformation("Relation Item Code BOM is already exist.Cann't be Saved.")
                        FieldsVarification = False
                        Exit Function
                    End If
                End If
            Next
        End With

        With SprdMainRel
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColRelItemCode
                mItemCode = Trim(.Text)
                If mItemCode <> "" Then
                    For CntMainRow = 1 To SprdMain.MaxRows
                        SprdMain.Row = CntMainRow
                        SprdMain.Col = ColRMCode
                        If mItemCode = Trim(SprdMain.Text) Then
                            MsgInformation("Please Check Relation Item Code.Cann't be Saved.")
                            FieldsVarification = False
                            Exit Function
                        End If
                    Next
                End If
            Next
        End With

        '    With SprdMain
        '        For CntRow = 1 To .MaxRows
        '            .Row = CntRow
        '            .Col = ColRMCode
        '            mItemCode = Trim(.Text)
        '            If mItemCode <> "" Then
        '                SqlStr = "SELECT '" & MainClass.AllowSingleQuote(lblMKey.text) & "', COMPANY_CODE, DEPT_CODE, " & vbCrLf _
        ''                        & " MAINITEM_CODE, MAINSUBROWNO, SUBROWNO, " & vbCrLf _
        ''                        & " ALTER_RM_CODE, ALTER_STD_QTY, ALETRSCRAP, ALETR_WT_VAR,ALTER_STOCK_TYPE " & vbCrLf _
        ''                        & " FROM TEMP_PRD_BOM_ALTER_DET" & vbCrLf _
        ''                        & " WHERE USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf _
        ''                        & " AND MAINITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
        '                MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly
        '
        '            End If
        '        Next
        '    End With

        With SprdMain
            For cntRowMain = 1 To .MaxRows
                .Row = cntRowMain
                .Col = ColRMCode
                mMainItemCode = Trim(.Text)

                .Col = ColDeptCode
                mDeptCode = Trim(.Text)

                SqlStr = "SELECT '" & MainClass.AllowSingleQuote(lblMKey.Text) & "', COMPANY_CODE, DEPT_CODE, " & vbCrLf & " MAINITEM_CODE, MAINSUBROWNO, SUBROWNO, " & vbCrLf & " ALTER_RM_CODE, ALTER_STD_QTY, ALETRSCRAP, ALETR_WT_VAR,ALTER_STOCK_TYPE " & vbCrLf & " FROM TEMP_PRD_BOM_ALTER_DET" & vbCrLf & " WHERE USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf & " AND MAINITEM_CODE='" & MainClass.AllowSingleQuote(mMainItemCode) & "'" & vbCrLf & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(mDeptCode) & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then
                    Do While RsTemp.EOF = False
                        mItemCode = Trim(IIf(IsDbNull(RsTemp.Fields("ALTER_RM_CODE").Value), "", RsTemp.Fields("ALTER_RM_CODE").Value))
                        mMainItemCode = Trim(IIf(IsDbNull(RsTemp.Fields("MAINITEM_CODE").Value), "", RsTemp.Fields("MAINITEM_CODE").Value))
                        If ValidateAlternetCode(mMainItemCode, mItemCode) = True Then
                            If mItemCode <> "" Then
                                With SprdMainRel
                                    For cntRow = 1 To .MaxRows
                                        .Row = cntRow
                                        .Col = ColRelItemCode
                                        If mItemCode = Trim(.Text) Then
                                            MsgInformation("Please Check Relation Item Code " & mItemCode & ". You define Alternate also. Cann't be Saved.")
                                            FieldsVarification = False
                                            Exit Function
                                        End If
                                    Next
                                End With
                            End If
                        Else
                            'MsgInformation("Main Item Code : " & mMainItemCode & " & Alter Item Code : " & mItemCode & " not defined in Alternate Master.")
                            'FieldsVarification = False
                            'Exit Function
                        End If
                        RsTemp.MoveNext()
                    Loop
                End If
            Next
        End With

        For cntRow = 1 To 4
            If SprdOthValidation(IIf(cntRow = 1, SprdMainMWS, IIf(cntRow = 2, SprdMainPLT, IIf(cntRow = 3, SprdMainPPS, SprdMainPC)))) = False Then
                FieldsVarification = False
                Exit Function
            End If
        Next

        If MainClass.ValidDataInGrid(SprdMain, ColDeptCode, "S", "Dept Code Is Blank") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColRMCode, "S", "Item Code Is Blank") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColRMDesc, "S", "Item Name Is Blank") = False Then FieldsVarification = False : Exit Function
        '    If MainClass.ValidDataInGrid(SprdMain, colStdQty, "N", "Please Check Std.Qty") = False Then FieldsVarification = False: Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColQUnit, "S", "Please Check Unit") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColStockType, "S", "Please Check Stock Type") = False Then FieldsVarification = False : Exit Function

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function

    Private Function SprdOthValidation(ByRef pSprd As AxFPSpreadADO.AxfpSpread) As Boolean
        On Error GoTo err_Renamed
        Dim mMainItemCode As String
        Dim mItemCode As String
        Dim mMainDeptCode As String
        Dim mDeptCode As String
        Dim cntRow As Integer
        Dim cntRowMain As Integer
        Dim mMainOPRType As String
        Dim mIsAlter As String
        Dim mValidMainItemCode As Boolean
        Dim mCheckDeptCode As String
        Dim mDeptFind As Boolean

        SprdOthValidation = False

        With pSprd
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColDeptCodeOTH
                mCheckDeptCode = Trim(.Text)

                If mCheckDeptCode = "" Then
                    SprdOthValidation = False
                    MsgInformation("Dept Can't be Blank.")
                    Exit Function
                End If

                mDeptFind = False

                With SprdSeq
                    For cntRowMain = 1 To .MaxRows
                        .Row = cntRowMain
                        .Col = ColDept
                        If mCheckDeptCode = Trim(.Text) Then
                            mDeptFind = True
                            Exit For
                        End If
                    Next
                End With
                If mDeptFind = False Then
                    SprdOthValidation = False
                    MsgInformation("Dept Not Define in Sequence.")
                    Exit Function
                End If
            Next
        End With

        With pSprd
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColIsAlterOTH
                mIsAlter = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                mValidMainItemCode = False

                If mIsAlter = "N" Then
                    .Col = ColMainItemCodeOTH
                    .Text = ""
                Else
                    .Col = ColMainItemCodeOTH
                    mMainItemCode = Trim(.Text)

                    For cntRowMain = 1 To .MaxRows
                        .Row = cntRowMain
                        .Col = ColIsAlterOTH
                        If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                            .Col = ColItemCodeOTH
                            mItemCode = Trim(.Text)

                            If mMainItemCode = mItemCode Then
                                .Row = cntRowMain
                                .Col = ColDeptCodeOTH
                                mMainDeptCode = Trim(.Text)

                                .Col = ColOprOTH
                                mMainOPRType = Trim(.Text)

                                mValidMainItemCode = True
                                Exit For
                            End If
                        End If
                    Next

                    If mValidMainItemCode = False Then
                        SprdOthValidation = False
                        MsgInformation("Invalid Main Item Code")
                        Exit Function
                    End If


                    .Row = cntRow
                    .Col = ColDeptCodeOTH
                    .Text = mMainDeptCode

                    .Col = ColOprOTH
                    .Text = mMainOPRType
                End If
            Next
            If pSprd.MaxRows > 1 Then
                If MainClass.ValidDataInGrid(pSprd, ColDeptCodeOTH, "S", "Dept Code Is Blank.") = False Then SprdOthValidation = False : Exit Function
                If MainClass.ValidDataInGrid(pSprd, ColItemCodeOTH, "S", "Item Code Is Blank.") = False Then SprdOthValidation = False : Exit Function
                If MainClass.ValidDataInGrid(pSprd, ColItemDescOTH, "S", "Item Description Is Blank.") = False Then SprdOthValidation = False : Exit Function
                If MainClass.ValidDataInGrid(pSprd, ColQUnitOTH, "S", "Item UOM Is Blank.") = False Then SprdOthValidation = False : Exit Function
                If MainClass.ValidDataInGrid(pSprd, ColNetConsumptionOTH, "N", "Net Consumption Is Zero.") = False Then SprdOthValidation = False : Exit Function
            End If
        End With
        SprdOthValidation = True
        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        Resume
    End Function

    Private Function ValidateAlternetCode(ByRef mMainItemCode As String, ByRef mAlterItemCode As String) As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsMisc As ADODB.Recordset = Nothing

        ValidateAlternetCode = True
        If Trim(mMainItemCode) = "" Then Exit Function
        If Trim(mAlterItemCode) = "" Then Exit Function

        ValidateAlternetCode = False

        SqlStr = "SELECT A.ITEM_SHORT_DESC " & vbCrLf _
            & " FROM INV_ITEM_MST A, INV_ITEM_ALTER_DET B " & vbCrLf _
            & " WHERE A.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
            & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf _
            & " AND A.ITEM_CODE=B.ALTER_ITEM_CODE" & vbCrLf _
            & " AND B.ALTER_ITEM_CODE='" & MainClass.AllowSingleQuote(mAlterItemCode) & "'" & vbCrLf _
            & " AND B.ITEM_CODE='" & MainClass.AllowSingleQuote(mMainItemCode) & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If Not RsMisc.EOF Then
            ValidateAlternetCode = True
        Else
            ValidateAlternetCode = False
        End If
        Exit Function
ERR1:
        MsgBox(Err.Description)
    End Function
    Private Function ValidateMainCode(ByRef mItemCode As String) As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsMisc As ADODB.Recordset = Nothing

        ValidateMainCode = False
        If Trim(mItemCode) = "" Then ValidateMainCode = True : Exit Function

        SqlStr = "SELECT B.ITEM_CODE " & vbCrLf _
            & " FROM INV_ITEM_ALTER_DET B " & vbCrLf _
            & " WHERE B.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
            & " AND B.ALTER_ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If Not RsMisc.EOF Then
            If MsgQuestion("Item Code : " & mItemCode & " is Alternate for Item Code : " & Trim(IIf(IsDbNull(RsMisc.Fields("ITEM_CODE").Value), "", RsMisc.Fields("ITEM_CODE").Value)) & ". Want to proceed.") = CStr(MsgBoxResult.Yes) Then
                ValidateMainCode = True
            Else
                ValidateMainCode = False
            End If
            '        MsgInformation "Item Code : " & mItemCode & " is Alternate for Item Code : " & Trim(IIf(IsNull(RsMisc!ITEM_CODE), "", RsMisc!ITEM_CODE)) & ". Please Select Main Item Code."
            '        ValidateMainCode = False
        Else
            ValidateMainCode = True
        End If
        Exit Function
ERR1:
        MsgBox(Err.Description)
    End Function
    Private Function CheckOperation() As Boolean

        On Error GoTo ERR1
        Dim i As Integer
        Dim mRMCode As String
        Dim mDeptCode As String
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mOPRCode As String

        CheckOperation = True

        With SprdMain
            For i = 1 To .MaxRows
                .Row = i

                .Col = ColRMCode
                mRMCode = Trim(.Text)

                .Col = ColDeptCode
                mDeptCode = Trim(.Text)

                .Col = ColOperation

                SqlStr = " SELECT TRN.OPR_CODE " & vbCrLf _
                    & " FROM TEMP_PRD_OPR_TRN  TRN, PRD_OPR_MST MST" & vbCrLf _
                    & " WHERE TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
                    & " AND TRN.COMPANY_CODE=MST.COMPANY_CODE AND TRN.OPR_CODE=MST.OPR_CODE" & vbCrLf _
                    & " AND TRN.DEPT_CODE='" & mDeptCode & "'" & vbCrLf _
                    & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "' AND WEF=TO_DATE('" & vb6.Format(lblOldWEF.text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


                If Trim(.Text) <> "" Then
                    SqlStr = SqlStr & vbCrLf & " AND MST.OPR_DESC='" & MainClass.AllowSingleQuote(.Text) & "'"
                End If

                SqlStr = SqlStr & vbCrLf & " AND UserId='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If Trim(.Text) <> "" Then
                    If RsTemp.EOF = True Then
                        MsgBox("Please Select Valid Operation for RM Code : " & mRMCode & " in Dept : " & mDeptCode & ".")
                        MainClass.SetFocusToCell(SprdMain, i, ColOperation)
                        CheckOperation = False
                        Exit Function
                    End If
                Else
                    If RsTemp.EOF = False Then
                        MsgBox("Please Select Operation for RM Code : " & mRMCode & " in Dept : " & mDeptCode & ".")
                        MainClass.SetFocusToCell(SprdMain, i, ColOperation)
                        CheckOperation = False
                        Exit Function
                    End If
                End If
            Next
        End With
        Exit Function
ERR1:
        MsgBox(Err.Description)
        CheckOperation = False
    End Function

    Private Function CheckProcessDept() As Boolean
        On Error GoTo ERR1
        Dim i As Integer
        Dim j As Integer
        Dim mDeptCode As String
        Dim mProcessDeptCode As String
        Dim mFindDept As Boolean

        CheckProcessDept = False


        With SprdMain
            For i = 1 To .MaxRows - 1
                mFindDept = False

                .Row = i

                .Col = ColDeptCode
                mDeptCode = Trim(.Text)

                For j = 1 To SprdSeq.MaxRows
                    SprdSeq.Row = j
                    SprdSeq.Col = ColDept
                    mProcessDeptCode = Trim(SprdSeq.Text)
                    If mDeptCode = mProcessDeptCode Then
                        mFindDept = True
                        Exit For
                    End If
                Next
                If mFindDept = False Then
                    MsgBox("Dept Not Defined in Process : " & mDeptCode)
                    CheckProcessDept = False
                    Exit Function
                End If
            Next
            CheckProcessDept = True
        End With
        Exit Function
ERR1:
        MsgBox(Err.Description)
        CheckProcessDept = False
    End Function
    Private Function CheckItemRelation(ByRef xMainItemCode As String) As Boolean

        On Error GoTo ERR1
        Dim i As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        CheckItemRelation = False

        SqlStr = "SELECT ITEM_CODE FROM INV_ITEM_RELATIONSHIP_DET" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
            & " AND REF_ITEM_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            xMainItemCode = IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)
            CheckItemRelation = True
        End If
        Exit Function
ERR1:
        MsgBox(Err.Description)
        CheckItemRelation = False
    End Function
    Private Sub FillGridRow(ByRef mItemCode As String)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim mSizeCode As Integer

        If Trim(mItemCode) = "" Then Exit Sub
        SqlStr = " SELECT ITEM_CODE,ITEM_SHORT_DESC,CUSTOMER_PART_NO,ISSUE_UOM, " & " SEMI_FIN_ITEM_CODE,DRAWING_NO,DRW_REVNO," & " ITEM_WEIGHT,ITEM_MAKE,ITEM_SURFACE_AREA" & " FROM INV_ITEM_MST " & " WHERE ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' " & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS = 'A' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If Not RsMisc.EOF Then
            SprdMain.Row = SprdMain.ActiveRow
            With RsMisc
                SprdMain.Col = ColRMDesc
                SprdMain.Text = IIf(IsDbNull(.Fields("ITEM_SHORT_DESC").Value), "", .Fields("ITEM_SHORT_DESC").Value)

                SprdMain.Col = ColPartNo
                SprdMain.Text = IIf(IsDbNull(.Fields("CUSTOMER_PART_NO").Value), "", .Fields("CUSTOMER_PART_NO").Value)

                SprdMain.Col = ColQUnit
                SprdMain.Text = IIf(IsDbNull(.Fields("ISSUE_UOM").Value), "", .Fields("ISSUE_UOM").Value)
            End With
        Else
            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColRMCode)
        End If
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub

    Private Sub FillOthGridRow(ByRef mItemCode As String, ByRef pSprd As AxFPSpreadADO.AxfpSpread)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim mSizeCode As Integer

        If Trim(mItemCode) = "" Then Exit Sub
        SqlStr = " SELECT ITEM_CODE,ITEM_SHORT_DESC,CUSTOMER_PART_NO,ISSUE_UOM, " & " SEMI_FIN_ITEM_CODE,DRAWING_NO,DRW_REVNO," & " ITEM_WEIGHT,ITEM_MAKE,ITEM_SURFACE_AREA" & " FROM INV_ITEM_MST " & " WHERE ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' " & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS = 'A' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If Not RsMisc.EOF Then
            pSprd.Row = pSprd.ActiveRow
            With RsMisc
                pSprd.Col = ColItemDescOTH
                pSprd.Text = IIf(IsDbNull(.Fields("ITEM_SHORT_DESC").Value), "", .Fields("ITEM_SHORT_DESC").Value)

                pSprd.Col = ColQUnitOTH
                pSprd.Text = IIf(IsDbNull(.Fields("ISSUE_UOM").Value), "", .Fields("ISSUE_UOM").Value)
            End With
        Else
            MainClass.SetFocusToCell(pSprd, pSprd.ActiveRow, ColItemCodeOTH)
        End If
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub

    Private Sub chkApproved_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkApproved.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)

        If chkApproved.Enabled = True And chkApproved.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtApprovedBy.Text = PubUserID
            txtApprovedBy_Validating(txtApprovedBy, New System.ComponentModel.CancelEventArgs(False))
        End If
    End Sub

    Private Sub chkBOP_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkBOP.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkScrap_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkScrap.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkStatus_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkStatus.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
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
            SprdMain.Enabled = True
            txtCopyProductCode.Enabled = True
            cmdSearchCopyProdCode.Enabled = True
        Else
            CmdAdd.Text = ConCmdAddCaption
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
            txtCopyProductCode.Enabled = False
            cmdSearchCopyProdCode.Enabled = False
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

        If chkApproved.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MsgInformation("B.O.M. has not approved, So cann't be Amend.")
            Exit Sub
        End If

        Call txtProductCode_Validating(txtProductCode, New System.ComponentModel.CancelEventArgs(True))

        txtAmendNo.Text = CStr(GetMaxAmendNo(mItemCode))
        chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked
        lblOldWEF.Text = VB6.Format(txtWEF.Text, "DD/MM/YYYY")
        mAmendStatus = True
        cmdAmend.Enabled = False
        txtWEF.Enabled = True
        SprdMain.Enabled = True

        '    txtPreparedBy.Text = ""
        lblPreparedBy.Text = ""
        txtPreparedBy.Text = PubUserID
        txtPreparedBy_Validating(txtPreparedBy, New System.ComponentModel.CancelEventArgs(False))
        txtApprovedBy.Text = ""
        lblApprovedBy.Text = ""
        chkApproved.CheckState = System.Windows.Forms.CheckState.Unchecked
        '    txtPreparedBy.Enabled = True
        '    cmdSearchPrepBy.Enabled = True

        ADDMode = True
        MODIFYMode = False

        MainClass.ButtonStatus(Me, XRIGHT, RsBOMMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click

        On Error GoTo DelErrPart

        If chkStatus.CheckState = System.Windows.Forms.CheckState.Checked And PubSuperUser <> "S" Then
            MsgInformation("Closed BOM Cann't be Deleted")
            Exit Sub
        End If

        If Trim(txtProductCode.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        PubDBCn.Cancel()
        PubDBCn.BeginTrans()

        If Not RsBOMMain.EOF Then
            If IIf(IsDBNull(RsBOMMain.Fields("APP_EMP_CODE").Value), "", RsBOMMain.Fields("APP_EMP_CODE").Value) <> "" Then MsgBox("BOM has been approved or Costing Defined for this BOM, So cann't be deleted") : Exit Sub
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then

                If InsertIntoDelAudit(PubDBCn, "PRD_NEWBOM_HDR", (txtProductCode.Text), RsBOMMain) = False Then GoTo DelErrPart
                If InsertIntoDelAudit(PubDBCn, "PRD_NEWBOM_DET", (txtProductCode.Text), RsBOMDetail) = False Then GoTo DelErrPart

                If InsertIntoDeleteTrn(PubDBCn, "PRD_NEWBOM_HDR", "MKEY", (lblMKey.Text)) = False Then GoTo DelErrPart

                '            If Val(txtAmendNo.Text) = 0 Then
                If InsertIntoDelAudit(PubDBCn, "PRD_PRODSEQUENCE_HDR", txtProductCode.Text & ":" & VB6.Format(txtWEF.Text, "DD/MM/YYYY"), RsProdSeqMain) = False Then GoTo DelErrPart
                If InsertIntoDelAudit(PubDBCn, "INV_ITEM_RELATIONSHIP_HDR", (txtProductCode.Text), RsTransMain, "ITEM_CODE") = False Then GoTo DelErrPart

                If InsertIntoDeleteTrn(PubDBCn, "INV_ITEM_RELATIONSHIP_DET", "ITEM_CODE", (txtProductCode.Text)) = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "PRD_PRODSEQUENCE_DET", "ITEM_CODE", txtProductCode.Text & ":" & VB6.Format(txtWEF.Text, "DD/MM/YYYY")) = False Then GoTo DelErrPart

                PubDBCn.Execute("Delete from INV_ITEM_RELATIONSHIP_DET Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "'")
                PubDBCn.Execute("Delete from INV_ITEM_RELATIONSHIP_HDR Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "'")

                ''


                PubDBCn.Execute("DELETE FROM PRD_OPR_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "' AND WEF=TO_DATE('" & VB6.Format(txtWEF.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY')")
                PubDBCn.Execute("DELETE FROM PRD_PRODSEQUENCE_DET WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "' AND WEF=TO_DATE('" & VB6.Format(txtWEF.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY')")
                PubDBCn.Execute("DELETE FROM PRD_PRODSEQUENCE_HDR WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "' AND WEF=TO_DATE('" & VB6.Format(txtWEF.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY')")
                '            End If

                PubDBCn.Execute("DELETE FROM PRD_NEWBOM_TOOL_DET  WHERE Mkey='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")

                PubDBCn.Execute("DELETE FROM PRD_NEWBOM_OTH_DET  WHERE Mkey='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")
                PubDBCn.Execute("DELETE FROM PRD_BOM_ALTER_DET WHERE Mkey='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")
                PubDBCn.Execute("DELETE FROM PRD_NEWBOM_DET WHERE Mkey='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")
                PubDBCn.Execute("DELETE FROM PRD_NEWBOM_HDR  WHERE Mkey='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")

                If Val(txtAmendNo.Text) > 0 Then
                    If UpdatePreviousCost((txtProductCode.Text), Val(txtAmendNo.Text), "O") = False Then GoTo DelErrPart
                End If

                PubDBCn.CommitTrans()
                RsBOMMain.Requery()
                RsBOMDetail.Requery()
                RsBOMOtherDetail.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        RsBOMMain.Requery()
        RsBOMDetail.Requery()
        RsBOMOtherDetail.Requery()
        PubDBCn.Cancel()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdPopFromFile_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPopFromFile.Click
        On Error GoTo ErrPart
        Dim strFilePath As String

        If Trim(txtProductCode.Text) = "" Then
            MsgBox("Please select Product Code First.")
            Exit Sub
        End If

        If Trim(txtWEF.Text) = "" Then
            MsgBox("Please select WEF First.")
            Exit Sub
        End If

        strFilePath = My.Application.Info.DirectoryPath
        If Not fOpenFile(strFilePath, "*.xls", "Excel Data", CommonDialogOpen) Then
            GoTo NormalExit
        End If

        Call PopulateFromXLSFile(strFilePath)

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
NormalExit:
    End Sub
    Private Sub PopulateFromXLSFile(ByVal strXLSFile As String)

        On Error GoTo ErrPart
        Dim i As Integer
        Dim SqlStr As String = ""
        Dim mDeptCode As String
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mPartNo As String
        Dim mUOM As String
        Dim mStdQty As Double
        Dim mSCQty As Double
        Dim mWtVariance As Double
        Dim mStockType As String

        Dim xSqlStr As String
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsFile As ADODB.Recordset
        Dim FileConnStr As String

        Dim strTemp As String
        Dim strWkShName As String
        Dim strError As String

        MainClass.ClearGrid(SprdMain)
        FormatSprdMain(-1)


        FileConnStr = "Provider=MSDASQL.1;Connect Timeout=15;Extended Properties='DSN=Excel Files;DBQ=XXLSFILEX;DefaultDir=XXLSDIRX;DriverId=790;FIL=excel 8.0;MaxBufferSize=2048;PageTimeout=5;UID=admin;';Locale Identifier=1033"
        FileConnStr = Replace(FileConnStr, "XXLSFILEX", strXLSFile)
        strTemp = Mid(strXLSFile, 1, InStrRev(strXLSFile, "\") - 1)
        FileConnStr = Replace(FileConnStr, "XXLSDIRX", strTemp)

        If Not XLSConnect(Trim(FileConnStr), FileDBCn) Then
            GoTo ErrPart
        End If

        RsFile = FileDBCn.OpenSchema(ADODB.SchemaEnum.adSchemaTables)
        strWkShName = RsFile.Fields("Table_Name").Value

        mSqlStr = "SELECT * FROM ""XWKSHTX"" " ''WHERE F1 <> NULL"
        mSqlStr = Replace(mSqlStr, "XWKSHTX", strWkShName)


        If OpenExcelRecordSet(mSqlStr, RsFile, strError, FileDBCn, False) = 0 Then

            If RsFile.EOF = False Then
                Do While Not RsFile.EOF
                    mDeptCode = UCase(Trim(IIf(IsDbNull(RsFile.Fields(0).Value), "", RsFile.Fields(0).Value)))
                    If MainClass.ValidateWithMasterTable(mDeptCode, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then GoTo NextRecord

                    mItemCode = UCase(Trim(IIf(IsDbNull(RsFile.Fields(1).Value), "", RsFile.Fields(1).Value)))

                    xSqlStr = " SELECT ITEM_SHORT_DESC,ISSUE_UOM,CUSTOMER_PART_NO " & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND LTRIM(RTRIM(ITEM_CODE))='" & MainClass.AllowSingleQuote(mItemCode) & "'"
                    MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                    If RsTemp.EOF = False Then
                        mItemDesc = Trim(IIf(IsDbNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value))
                        mUOM = Trim(IIf(IsDbNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value))
                        mPartNo = Trim(IIf(IsDbNull(RsTemp.Fields("CUSTOMER_PART_NO").Value), "", RsTemp.Fields("CUSTOMER_PART_NO").Value))
                    Else
                        GoTo NextRecord
                    End If
                    If DuplicateItem = True Then GoTo NextRecord

                    mStdQty = Val(IIf(IsDbNull(RsFile.Fields(5).Value), 0, RsFile.Fields(5).Value))
                    If mStdQty = 0 Then GoTo NextRecord
                    mSCQty = Val(IIf(IsDbNull(RsFile.Fields(6).Value), 0, RsFile.Fields(6).Value))
                    mWtVariance = Val(IIf(IsDbNull(RsFile.Fields(7).Value), 0, RsFile.Fields(7).Value))

                    mStockType = UCase(Trim(IIf(IsDbNull(RsFile.Fields(8).Value), "", RsFile.Fields(8).Value)))
                    If MainClass.ValidateWithMasterTable(mStockType, "STOCK_TYPE_CODE", "STOCK_TYPE_CODE", "INV_TYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then GoTo NextRecord


                    SprdMain.Row = SprdMain.MaxRows

                    SprdMain.Col = ColDeptCode
                    SprdMain.Text = mDeptCode

                    SprdMain.Col = ColRMCode
                    SprdMain.Text = mItemCode

                    SprdMain.Col = ColRMDesc
                    SprdMain.Text = mItemDesc

                    SprdMain.Col = ColPartNo
                    SprdMain.Text = mPartNo

                    SprdMain.Col = ColQUnit
                    SprdMain.Text = mUOM

                    SprdMain.Col = colStdQty
                    SprdMain.Text = VB6.Format(mStdQty, "0.0000")

                    SprdMain.Col = ColGrossWtScrp
                    SprdMain.Text = VB6.Format(mSCQty, "0.0000")

                    SprdMain.Col = ColWtVar
                    SprdMain.Text = VB6.Format(mWtVariance, "0.0000")

                    SprdMain.Col = ColStockType
                    SprdMain.Text = mStockType

                    SprdMain.MaxRows = SprdMain.MaxRows + 1
                    '               FormatSprdMain -1, False

NextRecord:
                    RsFile.MoveNext()
                Loop
            End If
        End If

        If RsFile.State = ADODB.ObjectStateEnum.adStateOpen Then RsFile.Close()
        RsFile = Nothing

        If FileDBCn.State = ADODB.ObjectStateEnum.adStateOpen Then
            FileDBCn.Close()
            FileDBCn = Nothing
        End If

        '    CmdPopFromFile.Enabled = False
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '        Resume
    End Sub

    Private Function DuplicateItem() As Boolean
        Dim cntRow As Integer
        Dim mCount As Byte
        Dim mCheckItemCode As String
        Dim mItemCode As String

        With SprdMain
            .Row = .ActiveRow
            .Col = ColDeptCode
            mCheckItemCode = Trim(UCase(.Text))

            .Col = ColRMCode
            mCheckItemCode = mCheckItemCode & Trim(UCase(.Text))

            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColDeptCode
                mItemCode = Trim(UCase(.Text))

                .Col = ColRMCode
                mItemCode = mItemCode & Trim(UCase(.Text))

                If (mItemCode = mCheckItemCode And mCheckItemCode <> "") Then
                    mCount = mCount + 1
                End If

                If mCount > 1 Then
                    DuplicateItem = True
                    MsgInformation("Duplicate Item : " & mCheckItemCode)
                    '                MainClass.SetFocusToCell SprdMain, .ActiveRow, ColItemCode
                    Exit Function
                End If
            Next
        End With
    End Function
    Private Sub SprdMainMWS_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdMainMWS.ButtonClicked
        Call SprdMainItemEnable(eventArgs.Col, eventArgs.Row, SprdMainMWS)
    End Sub

    Private Sub SprdMainItemEnable(ByVal Col As Integer, ByVal Row As Integer, ByRef pSprd As AxFPSpreadADO.AxfpSpread)

        Dim mIsAlter As String

        If Row > 0 And Col = ColIsAlterOTH Then
            With pSprd
                .Row = .ActiveRow
                .Col = ColIsAlterOTH
                mIsAlter = IIf(.Value = CStr(System.Windows.Forms.CheckState.Unchecked), "N", "Y")
                MainClass.UnProtectCell(pSprd, 1, pSprd.MaxRows, ColIsAlterOTH, ColNetConsumptionOTH)

                If mIsAlter = "N" Then
                    MainClass.ProtectCell(pSprd, 1, pSprd.MaxRows, ColMainItemCodeOTH, ColMainItemCodeOTH)
                Else
                    MainClass.ProtectCell(pSprd, 1, pSprd.MaxRows, ColDeptCodeOTH, ColOprOTH)
                End If

                MainClass.ProtectCell(pSprd, 1, pSprd.MaxRows, ColItemDescOTH, ColQUnitOTH)
                MainClass.ProtectCell(pSprd, 1, pSprd.MaxRows, ColNetConsumptionOTH, ColNetConsumptionOTH)
                MainClass.SetSpreadColor(pSprd, Row)
            End With
        End If
    End Sub
    Private Sub SprdMainMWS_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMainMWS.Change

        With SprdMainMWS
            SprdMainMWS_LeaveCell(SprdMainMWS, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub SprdMainMWS_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMainMWS.ClickEvent
        Call SprdOTHClick(SprdMainMWS, eventArgs.Col, eventArgs.Row)
    End Sub


    Private Sub SprdMainMWS_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMainMWS.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMainMWS.ActiveCol
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColDeptCodeOTH Then SprdMainMWS_ClickEvent(SprdMainMWS, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColDeptCodeOTH, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCodeOTH Then SprdMainMWS_ClickEvent(SprdMainMWS, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCodeOTH, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemDescOTH Then SprdMainMWS_ClickEvent(SprdMainMWS, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemDescOTH, 0))
    End Sub

    Private Sub SprdMainMWS_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMainMWS.LeaveCell
        On Error GoTo ErrPart
        If eventArgs.NewRow = -1 Then Exit Sub
        Call SprdOthLeaveCell(SprdMainMWS, eventArgs.Col, eventArgs.Row, eventArgs.NewCol, eventArgs.NewRow, eventArgs.Cancel)
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Sub SprdMainPC_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdMainPC.ButtonClicked
        Call SprdMainItemEnable(eventArgs.Col, eventArgs.Row, SprdMainPC)
    End Sub

    Private Sub SprdMainPC_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMainPC.Change

        With SprdMainPC
            SprdMainPC_LeaveCell(SprdMainPC, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdOTHClick(ByRef pSprd As AxFPSpreadADO.AxfpSpread, ByVal Col As Integer, ByVal Row As Integer)

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mRMName As String
        Dim mDeleted As Boolean
        Dim mRMCode As String
        Dim mDeptCode As String
        Dim mItemCode As String
        'Dim mISAlter As String
        '
        '    If Row > 0 And Col = ColIsAlterOTH Then
        '        With pSprd
        '            .Row = .ActiveRow
        '            .Col = ColIsAlterOTH
        '            mISAlter = IIf(.Value = vbUnchecked, "N", "Y")
        '            Call FormatSprdMainOTH(.Row, pSprd, mISAlter)
        '        End With
        '    End If

        If Row = 0 And Col = ColDeptCodeOTH Then
            With pSprd
                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value
                If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColDeptCodeOTH
                    .Text = AcName1
                End If
            End With
        End If

        If Row = 0 And Col = ColItemCodeOTH Then
            With pSprd
                SqlStr = "SELECT ITEM_CODE,ITEM_SHORT_DESC " & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS = 'A' " & vbCrLf & " ORDER BY ITEM_CODE "
                .Row = .ActiveRow
                .Col = ColItemCodeOTH
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColItemCodeOTH
                    .Text = AcName
                    mItemCode = Trim(.Text)

                    .Col = ColItemDescOTH
                    .Text = AcName1

                    Call FillOthGridRow(mItemCode, pSprd)
                End If
            End With
        End If

        If Row = 0 And Col = ColItemDescOTH Then
            With pSprd
                SqlStr = "SELECT ITEM_SHORT_DESC,ITEM_CODE " & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS = 'A' " & vbCrLf & " ORDER BY ITEM_SHORT_DESC "

                .Row = .ActiveRow

                .Col = ColItemDescOTH
                mRMName = Trim(.Text)

                .Text = ""
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColItemDescOTH
                    .Text = AcName

                    .Col = ColItemCodeOTH
                    .Text = AcName1
                Else
                    .Row = .ActiveRow

                    .Col = ColItemDescOTH
                    .Text = mRMName
                End If
                .Col = ColItemCodeOTH
                mItemCode = Trim(.Text)
                Call FillOthGridRow(mItemCode, pSprd)
            End With
        End If

        If Col = 0 And Row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(pSprd, Row, ColItemCodeOTH)
            MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        End If
        Exit Sub
ErrPart:
        MsgInformation(Err.Description)
    End Sub

    Private Sub SprdMainPC_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMainPC.ClickEvent
        Call SprdOTHClick(SprdMainPC, eventArgs.Col, eventArgs.Row)
    End Sub

    Private Sub SprdMainPC_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMainPC.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMainPC.ActiveCol
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColDeptCodeOTH Then SprdMainPC_ClickEvent(SprdMainPC, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColDeptCodeOTH, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCodeOTH Then SprdMainPC_ClickEvent(SprdMainPC, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCodeOTH, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemDescOTH Then SprdMainPC_ClickEvent(SprdMainPC, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemDescOTH, 0))
    End Sub

    Private Sub SprdOthLeaveCell(ByRef pSprd As AxFPSpreadADO.AxfpSpread, ByVal Col As Integer, ByVal Row As Integer, ByVal NewCol As Integer, ByVal NewRow As Integer, ByRef Cancel As Boolean)

        On Error GoTo ErrPart
        Dim mDeptCode As String
        Dim mRMCode As String
        Dim mMainItemCode As String
        Dim mIsAlter As String

        Dim cntRow As Integer
        Dim mMainDeptCode As String
        Dim mOPRType As String
        Dim mValidMainCode As Boolean
        Dim mStdArea As Double

        pSprd.Row = Row


        Select Case Col

            Case ColMainItemCodeOTH
                pSprd.Row = pSprd.ActiveRow

                pSprd.Col = ColIsAlterOTH

                If pSprd.Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                    pSprd.Col = ColMainItemCodeOTH
                    If Trim(pSprd.Text) <> "" Then
                        pSprd.Text = ""
                        MsgInformation("Please Click on Alter First.")
                    End If
                    Exit Sub
                Else
                    pSprd.Col = ColMainItemCodeOTH
                    mMainItemCode = Trim(pSprd.Text)

                    If mMainItemCode = "" Then Exit Sub

                    If MainClass.ValidateWithMasterTable(mMainItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then

                    Else
                        MainClass.SetFocusToCell(pSprd, pSprd.ActiveRow, ColMainItemCodeOTH, "Main Code not in Master.")
                        Exit Sub
                    End If

                    mValidMainCode = False

                    With pSprd
                        For cntRow = 1 To .MaxRows
                            .Row = cntRow
                            .Col = ColItemCodeOTH
                            If mMainItemCode = Trim(.Text) Then
                                .Col = ColDeptCodeOTH
                                mMainDeptCode = Trim(.Text)

                                .Col = ColOprOTH
                                mOPRType = Trim(.Text)
                                mValidMainCode = True
                                Exit For
                            End If
                        Next

                        If mValidMainCode = True Then
                            pSprd.Row = pSprd.ActiveRow

                            pSprd.Col = ColDeptCodeOTH
                            pSprd.Text = mMainDeptCode

                            pSprd.Col = ColOprOTH
                            pSprd.Text = mOPRType

                        Else
                            MainClass.SetFocusToCell(pSprd, pSprd.ActiveRow, ColMainItemCodeOTH, "Invalid Main Code.")
                            Exit Sub
                        End If
                    End With
                End If
            Case ColDeptCodeOTH
                pSprd.Row = pSprd.ActiveRow
                pSprd.Col = ColItemCodeOTH
                If Trim(pSprd.Text) = "" Then Exit Sub

                pSprd.Col = ColDeptCodeOTH
                mDeptCode = Trim(pSprd.Text)
                If Trim(pSprd.Text) = "" Then Exit Sub

                If MainClass.ValidateWithMasterTable(mDeptCode, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then

                Else
                    MainClass.SetFocusToCell(pSprd, pSprd.ActiveRow, ColDeptCodeOTH)
                End If

                pSprd.Col = ColItemCodeOTH
                mRMCode = Trim(pSprd.Text)

                If mRMCode <> "" Then
                    If CheckDuplicateItem(pSprd, mRMCode, mDeptCode) = True Then
                        MainClass.SetFocusToCell(pSprd, pSprd.ActiveRow, ColDeptCodeOTH)
                    End If
                End If
            Case ColItemCodeOTH
                pSprd.Row = pSprd.ActiveRow
                pSprd.Col = ColItemCodeOTH
                If Trim(pSprd.Text) = "" Then Exit Sub

                pSprd.Row = pSprd.ActiveRow
                pSprd.Col = ColIsAlterOTH
                mIsAlter = IIf(pSprd.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                pSprd.Col = ColDeptCodeOTH
                mDeptCode = Trim(pSprd.Text)

                pSprd.Col = ColItemCodeOTH
                mRMCode = Trim(pSprd.Text)

                pSprd.Col = ColMainItemCodeOTH
                mMainItemCode = Trim(pSprd.Text)

                If mIsAlter = "Y" Then
                    If Trim(mMainItemCode) = Trim(mRMCode) Then
                        MainClass.SetFocusToCell(pSprd, pSprd.ActiveRow, ColItemCodeOTH)
                        Exit Sub
                    End If
                End If

                If Trim(txtProductCode.Text) = Trim(pSprd.Text) Then
                    MainClass.SetFocusToCell(pSprd, pSprd.ActiveRow, ColItemCodeOTH)
                Else
                    '                If ValidateMainCode(mRMCode) = True Then
                    If CheckDuplicateItem(pSprd, mRMCode, mDeptCode) = False Then
                        pSprd.Row = pSprd.ActiveRow
                        pSprd.Col = ColItemCodeOTH
                        Call FillOthGridRow((pSprd.Text), pSprd)
                    Else
                        MainClass.SetFocusToCell(pSprd, pSprd.ActiveRow, ColItemCodeOTH)
                    End If
                    '                Else
                    '                    MainClass.SetFocusToCell pSprd, pSprd.ActiveRow, ColRMCode
                    '                End If
                End If

            Case colStdQtyOTH
                pSprd.Row = pSprd.ActiveRow
                pSprd.Col = ColItemCodeOTH
                If Trim(pSprd.Text) = "" Then Exit Sub

                If CheckQty(pSprd, Col, Row) = True Then
                    MainClass.AddBlankSprdRow(pSprd, ColItemCodeOTH, ConRowHeight)
                    FormatSprdMainOTH((pSprd.MaxRows), pSprd)
                End If

            Case colStdConsOTH
                pSprd.Row = pSprd.ActiveRow
                pSprd.Col = ColItemCodeOTH
                If Trim(pSprd.Text) = "" Then Exit Sub

                If CheckQty(pSprd, Col, Row) = True Then
                    MainClass.AddBlankSprdRow(pSprd, ColItemCodeOTH, ConRowHeight)
                    FormatSprdMainOTH((pSprd.MaxRows), pSprd)
                End If

            Case ColQUnit
                pSprd.Row = pSprd.ActiveRow
                pSprd.Col = ColItemCodeOTH
                If Trim(pSprd.Text) = "" Then Exit Sub

                pSprd.Row = pSprd.ActiveRow
                pSprd.Col = ColQUnitOTH
                If Trim(pSprd.Text) <> "" Then Call CheckUnit(pSprd, ColQUnit, pSprd.ActiveRow)

        End Select

        Call CalcTots(pSprd)

        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        '    Resume
    End Sub

    Private Sub SprdMainPC_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMainPC.LeaveCell
        On Error GoTo ErrPart
        If eventArgs.NewRow = -1 Then Exit Sub
        Call SprdOthLeaveCell(SprdMainPC, eventArgs.Col, eventArgs.Row, eventArgs.NewCol, eventArgs.NewRow, eventArgs.Cancel)
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Sub SprdMainPLT_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdMainPLT.ButtonClicked
        Call SprdMainItemEnable(eventArgs.Col, eventArgs.Row, SprdMainPLT)
    End Sub

    Private Sub SprdMainPLT_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMainPLT.Change

        With SprdMainPLT
            SprdMainPLT_LeaveCell(SprdMainPLT, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub SprdMainPLT_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMainPLT.ClickEvent
        Call SprdOTHClick(SprdMainPLT, eventArgs.Col, eventArgs.Row)
    End Sub


    Private Sub SprdMainPLT_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMainPLT.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMainPLT.ActiveCol
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColDeptCodeOTH Then SprdMainPLT_ClickEvent(SprdMainPLT, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColDeptCodeOTH, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCodeOTH Then SprdMainPLT_ClickEvent(SprdMainPLT, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCodeOTH, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemDescOTH Then SprdMainPLT_ClickEvent(SprdMainPLT, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemDescOTH, 0))
    End Sub

    Private Sub SprdMainPLT_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMainPLT.LeaveCell
        On Error GoTo ErrPart
        If eventArgs.NewRow = -1 Then Exit Sub
        Call SprdOthLeaveCell(SprdMainPLT, eventArgs.Col, eventArgs.Row, eventArgs.NewCol, eventArgs.NewRow, eventArgs.Cancel)
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Sub SprdMainPPS_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdMainPPS.ButtonClicked
        Call SprdMainItemEnable(eventArgs.Col, eventArgs.Row, SprdMainPPS)
    End Sub

    Private Sub SprdMainPPS_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMainPPS.Change

        With SprdMainPPS
            SprdMainPPS_LeaveCell(SprdMainPPS, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub SprdMainPPS_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMainPPS.ClickEvent
        Call SprdOTHClick(SprdMainPPS, eventArgs.Col, eventArgs.Row)
    End Sub


    Private Sub SprdMainPPS_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMainPPS.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMainPPS.ActiveCol
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColDeptCodeOTH Then SprdMainPPS_ClickEvent(SprdMainPPS, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColDeptCodeOTH, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCodeOTH Then SprdMainPPS_ClickEvent(SprdMainPPS, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCodeOTH, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemDescOTH Then SprdMainPPS_ClickEvent(SprdMainPPS, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemDescOTH, 0))
    End Sub

    Private Sub SprdMainPPS_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMainPPS.LeaveCell
        On Error GoTo ErrPart
        If eventArgs.NewRow = -1 Then Exit Sub
        Call SprdOthLeaveCell(SprdMainPPS, eventArgs.Col, eventArgs.Row, eventArgs.NewCol, eventArgs.NewRow, eventArgs.Cancel)
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Sub SprdMainRel_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMainRel.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdMainRel_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMainRel.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMainRel.ActiveCol

        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColRelItemCode Then SprdMainRel_ClickEvent(SprdMainRel, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColRelItemCode, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColRelItemDesc Then SprdMainRel_ClickEvent(SprdMainRel, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColRelItemDesc, 0))
        SprdMainRel.Refresh()
    End Sub

    Private Sub SprdMainRel_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMainRel.Leave
        With SprdMainRel
            SprdMainRel_LeaveCell(SprdMainRel, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub
    Private Sub SprdMainRel_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMainRel.ClickEvent

        On Error GoTo ErrPart
        Dim xIName As String
        Dim xSupp As String
        Dim SqlStr As String = ""

        If eventArgs.row = 0 And eventArgs.col = ColRelItemCode Then
            With SprdMainRel
                .Row = .ActiveRow
                .Col = ColRelItemCode
                If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", , , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
                    .Row = .ActiveRow
                    .Col = ColRelItemCode
                    .Text = AcName
                End If
                MainClass.SetFocusToCell(SprdMainRel, SprdMainRel.ActiveRow, ColRelItemCode)
            End With
        End If

        If eventArgs.Row = 0 And eventArgs.Col = ColRelItemDesc Then
            With SprdMainRel
                .Row = .ActiveRow
                .Col = ColRelItemDesc
                xIName = .Text
                .Text = ""
                If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow
                    .Col = ColRelItemDesc
                    .Text = AcName
                Else
                    .Row = .ActiveRow
                    .Col = ColRelItemDesc
                    .Text = xIName
                End If
                MainClass.ValidateWithMasterTable(.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                .Row = .ActiveRow
                .Col = ColRelItemCode
                .Text = MasterNo
                MainClass.SetFocusToCell(SprdMainRel, SprdMainRel.ActiveRow, ColRelItemCode)
            End With
        End If

        If eventArgs.col = 0 And eventArgs.row > 0 Then
            SprdMainRel.Row = eventArgs.row
            SprdMainRel.Col = ColRelItemCode
            If eventArgs.row < SprdMainRel.MaxRows And (ADDMode = True Or MODIFYMode = True) Then
                MainClass.DeleteSprdRow(SprdMainRel, eventArgs.row, ColRelItemCode)
                MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
                FormatSprdMainRel(eventArgs.row)
            End If
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdMainRel_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdMainRel.KeyDownEvent

        Dim mActiveCol As Integer

        mActiveCol = SprdMainRel.ActiveCol

        If eventArgs.KeyCode = System.Windows.Forms.Keys.Return Or eventArgs.KeyCode = System.Windows.Forms.Keys.Tab Then
            If mActiveCol = ColRelItemCode Then
                SprdMainRel.Row = SprdMainRel.ActiveRow
                SprdMainRel.Col = ColRelItemCode
                If Trim(SprdMainRel.Text) <> "" Then
                    If SprdMainRel.MaxRows = SprdMainRel.ActiveRow Then
                        MainClass.AddBlankSprdRow(SprdMainRel, ColRelItemCode, ConRowHeight)
                        FormatSprdMainRel((SprdMainRel.MaxRows))
                    End If
                End If
                '            SprdMainRel.Row = SprdMainRel.MaxRows
            End If
        ElseIf eventArgs.KeyCode = System.Windows.Forms.Keys.F1 Then
            If mActiveCol = ColRelItemCode Then SprdMainRel_ClickEvent(SprdMainRel, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColRelItemCode, 0))
            If mActiveCol = ColRelItemDesc Then SprdMainRel_ClickEvent(SprdMainRel, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColRelItemDesc, 0))
            '    Else
            '        MainClass.SetFocusToCell SprdMainRel, SprdMainRel.ActiveRow, mActiveCol
        End If
        eventArgs.KeyCode = 9999
    End Sub

    Private Sub SprdMainRel_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMainRel.LeaveCell
        On Error GoTo ErrPart
        Dim mItemCode As String
        Dim mUOM As String

        If eventArgs.NewRow = -1 Then Exit Sub

        SprdMainRel.Row = SprdMainRel.ActiveRow

        Select Case eventArgs.col
            Case ColRelItemCode
                SprdMainRel.Col = ColRelItemCode
                Call FillItemDescFromItemCode((SprdMainRel.Text))
                If DuplicateRelItem() = False Then
                    '                FormatSprdMainRel -1
                End If
                SprdMainRel.Row = SprdMainRel.ActiveRow
                mItemCode = Trim(SprdMainRel.Text)

                '            SprdMainRel.Col = ColUOM
                '            mUOM = Trim(SprdMainRel.Text)

            Case ColRelItemDesc
                SprdMainRel.Col = ColRelItemDesc
                Call FillItemDescFromItemDesc((SprdMainRel.Text))
                If DuplicateRelItem() = False Then
                End If
        End Select
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Function DuplicateRelItem() As Boolean

        Dim cntRow As Integer
        Dim mCount As Byte
        Dim mCheckItemCode As String
        Dim mItemCode As String

        With SprdMainRel
            .Row = .ActiveRow
            .Col = ColRelItemCode
            mCheckItemCode = Trim(UCase(.Text))

            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColRelItemCode
                mItemCode = Trim(UCase(.Text))

                If (mItemCode = mCheckItemCode And mCheckItemCode <> "") Then
                    mCount = mCount + 1
                End If

                If mCount > 1 Then
                    DuplicateRelItem = True
                    MsgInformation("Duplicate Item : " & mCheckItemCode)
                    MainClass.SetFocusToCell(SprdMainRel, .ActiveRow, ColRelItemCode)
                    Exit Function
                End If
            Next
        End With
    End Function
    Private Function UpdatePreviousCost(ByRef pItemCode As String, ByRef pAmendNo As Integer, ByRef pPreviousStatus As String) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = ""
        SqlStr = " UPDATE PRD_NEWBOM_HDR SET " & vbCrLf & " STATUS = '" & pPreviousStatus & "', " & vbCrLf & " MODUSER = '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " MODDATE = TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AMEND_NO = " & pAmendNo - 1 & "" & vbCrLf & " AND PRODUCT_CODE ='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        PubDBCn.Execute(SqlStr)

        UpdatePreviousCost = True

        Exit Function
ErrPart:
        UpdatePreviousCost = False
        If Err.Description = "" Then Exit Function
        MsgBox(Err.Description)
        '    Resume
    End Function

    Private Function GetMaxAmendNo(ByRef pItemCode As String) As Integer

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = " SELECT MAX(AMEND_NO) AS AMEND_NO" & vbCrLf _
        & " FROM PRD_NEWBOM_HDR" & vbCrLf _
        & " WHERE " & vbCrLf _
        & " COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
        & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If IsDbNull(RsTemp.Fields("AMEND_NO").Value) Then
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

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            If PubUserID <> "G0416" Then
                If RsBOMMain.Fields("IS_APPROVED").Value = "Y" Then MsgBox("BOM has been approved, So cann't be modified") : Exit Sub
            End If
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsBOMMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            txtProductCode.Enabled = False
            cmdSearchProdCode.Enabled = False
            SprdMain.Enabled = True
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
        Call PrintBOM(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call PrintBOM(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub PrintBOM(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mTitle As String

        If InsertIntoPrintdummyData = False Then GoTo ERR1

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        mTitle = "Bill Of Material"

        SqlStr = ""
        SqlStr = "SELECT * " & vbCrLf & " FROM TEMP_PRINTDUMMYDATA " & vbCrLf & " WHERE UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf & " ORDER BY SUBROW"

        MainClass.AssignCRptFormulas(Report1, "PName=""" & txtProductDesc.Text & """")
        MainClass.AssignCRptFormulas(Report1, "PartNo=""" & txtCustPartNo.Text & """")
        MainClass.AssignCRptFormulas(Report1, "Model=""" & txtModelNo.Text & """")

        MainClass.AssignCRptFormulas(Report1, "PCode=""" & txtProductCode.Text & """")

        MainClass.AssignCRptFormulas(Report1, "AmendNo=""" & txtAmendNo.Text & """")
        MainClass.AssignCRptFormulas(Report1, "WEFdate=""" & VB6.Format(txtWEF.Text, "DD/MM/YYYY") & """")
        MainClass.AssignCRptFormulas(Report1, "Remarks=""" & TxtRemarks.Text & """")
        MainClass.AssignCRptFormulas(Report1, "PrepareBy=""" & lblPreparedBy.Text & """")
        MainClass.AssignCRptFormulas(Report1, "ApprBy=""" & lblApprovedBy.Text & """")


        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\FGBOMPrint.rpt" 'BillOfMat.rpt"

        SetCrpt(Report1, Mode, 1, mTitle)
        Report1.SQLQuery = SqlStr
        Report1.WindowShowGroupTree = False
        Report1.Action = 1

        Exit Sub
ERR1:
        'Resume
        MsgInformation(Err.Description)
    End Sub

    Private Function InsertIntoPrintdummyData() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim pSqlStr As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        PubDBCn.Execute("DELETE FROM TEMP_PRINTDUMMYDATA WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'")

        SqlStr = " SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " ID.SUBROWNO,'', '',ID.SUBROWNO, " & vbCrLf & " ID.RM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO, " & vbCrLf & " ID.DEPT_CODE, DECODE(SEMI_FIN_ITEM_CODE,'Y','Yes','No'), '' , INVMST.DRW_REVNO, TO_CHAR(ID.STD_QTY,'9999.99'),  " & vbCrLf & " INVMST.ITEM_TECH_DESC, '', INVMST.SURFACE_TREATMENT,TO_CHAR(GROSS_WT_SCRAP,'999.999'),INVMST.ISSUE_UOM, IH.OUTPUT_QTY, IH.ISSUE_UOM "

        SqlStr = SqlStr & vbCrLf & " FROM PRD_NEWBOM_HDR IH,PRD_NEWBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.RM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "' " & vbCrLf & " AND IH.WEF=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SUBROWNO"

        pSqlStr = " INSERT INTO " & vbCrLf & " TEMP_PRINTDUMMYDATA (" & vbCrLf & " USERID, SUBROW, FIELD1, " & vbCrLf & " FIELD2, FIELD3, FIELD4, " & vbCrLf & " FIELD5, FIELD6, FIELD7, " & vbCrLf & " FIELD8, FIELD9, FIELD10, " & vbCrLf & " FIELD11, FIELD12, FIELD13, FIELD14, FIELD18, FIELD16,FIELD17, FIELD15 )" & vbCrLf & SqlStr

        PubDBCn.Execute(pSqlStr)

        PubDBCn.CommitTrans()
        InsertIntoPrintdummyData = True
        Exit Function
ErrPart:
        InsertIntoPrintdummyData = False
        PubDBCn.RollbackTrans()
    End Function

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If Update1 = True Then
            ADDMode = False
            MODIFYMode = False
            txtProductCode_Validating(txtProductCode, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True And CmdAdd.Visible = True Then CmdAdd.Focus()
            txtCopyProductCode.Enabled = False
            cmdSearchCopyProdCode.Enabled = False
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
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If ADDMode = True Then
            SqlStr = SqlStr & " AND STATUS ='O' "
        End If

        If MainClass.SearchGridMaster("", "ATH_PASSWORD_MST", "EMP_NAME", "USER_ID", , , SqlStr) = True Then
            txtApprovedBy.Text = AcName1
            lblApprovedBy.text = AcName
        End If

        '    SqlStr = "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & ""
        '
        '    If ADDMode = True Then
        '        SqlStr = SqlStr & " AND EMP_LEAVE_DATE IS NULL "
        '    End If
        '
        '    If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
        '        txtApprovedBy.Text = AcName1
        '        lblApprovedBy.text = AcName
        '    End If
    End Sub

    Private Sub cmdSearchCopyProdCode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchCopyProdCode.Click
        Dim mSqlStr As String

        mSqlStr = " SELECT IH.PRODUCT_CODE, IH.AMEND_NO, INV.ITEM_SHORT_DESC, IH.WEF " & vbCrLf & " FROM PRD_NEWBOM_HDR IH, INV_ITEM_MST INV " & vbCrLf & " WHERE IH.COMPANY_CODE=INV.COMPANY_CODE " & vbCrLf & " AND IH.PRODUCT_CODE=INV.ITEM_CODE " & vbCrLf & " AND BOM_TYPE='" & lblType.Text & "'"

        mSqlStr = mSqlStr & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If Trim(txtCopyProductCode.Text) <> "" Then
            mSqlStr = mSqlStr & vbCrLf & " AND IH.PRODUCT_CODE='" & Trim(txtCopyProductCode.Text) & "'"
        End If

        If MainClass.SearchGridMasterBySQL2("", mSqlStr) = True Then
            txtCopyAmendNo.Text = AcName1
            txtCopyProductCode.Text = AcName
            If txtCopyProductCode.Enabled = True Then txtCopyProductCode.Focus()
        End If

    End Sub

    Private Sub cmdSearchProdCode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchProdCode.Click
        Dim mSqlStr As String

        mSqlStr = " SELECT IH.PRODUCT_CODE, IH.WEF, INV.ITEM_SHORT_DESC, INV.ISSUE_UOM " & vbCrLf & " FROM PRD_NEWBOM_HDR IH, INV_ITEM_MST INV " & vbCrLf & " WHERE IH.COMPANY_CODE=INV.COMPANY_CODE " & vbCrLf & " AND IH.PRODUCT_CODE=INV.ITEM_CODE " & vbCrLf & " AND BOM_TYPE='" & lblType.Text & "'"

        mSqlStr = mSqlStr & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If Trim(txtProductCode.Text) <> "" Then
            mSqlStr = mSqlStr & vbCrLf & " AND IH.PRODUCT_CODE='" & Trim(txtProductCode.Text) & "'"
        End If

        If MainClass.SearchGridMasterBySQL2("", mSqlStr) = True Then
            txtWEF.Text = VB6.Format(AcName1, "DD/MM/YYYY")
            txtProductCode.Text = AcName
            If txtProductCode.Enabled = True Then txtProductCode.Focus()
            If ShowRecord() = False Then Exit Sub
        End If

    End Sub

    Private Sub cmdSearchPrepBy_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchPrepBy.Click
        On Error GoTo SrchERR
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If ADDMode = True Then
            SqlStr = SqlStr & " AND STATUS ='O' "
        End If

        If MainClass.SearchGridMaster("", "ATH_PASSWORD_MST", "EMP_NAME", "USER_ID", , , SqlStr) = True Then
            txtPreparedBy.Text = AcName1
            lblPreparedBy.text = AcName
        End If

        '    If ADDMode = True Then
        '        SqlStr = SqlStr & " AND EMP_LEAVE_DATE IS NULL "
        '    End If
        '
        '    If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
        '        txtPreparedBy.Text = AcName1
        '        lblPreparedBy.text = AcName
        '    End If
        Exit Sub
SrchERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchWEF_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchWEF.Click
        Dim mSqlStr As String

        mSqlStr = " SELECT IH.WEF, IH.PRODUCT_CODE, INV.ITEM_SHORT_DESC, INV.ISSUE_UOM " & vbCrLf & " FROM PRD_NEWBOM_HDR IH, INV_ITEM_MST INV " & vbCrLf & " WHERE IH.COMPANY_CODE=INV.COMPANY_CODE " & vbCrLf & " AND IH.PRODUCT_CODE=INV.ITEM_CODE " & vbCrLf & " AND BOM_TYPE='" & lblType.Text & "'"

        mSqlStr = mSqlStr & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If Trim(txtProductCode.Text) <> "" Then
            mSqlStr = mSqlStr & vbCrLf & " AND IH.PRODUCT_CODE='" & Trim(txtProductCode.Text) & "'"
        End If

        If MainClass.SearchGridMasterBySQL2("", mSqlStr) = True Then
            txtWEF.Text = VB6.Format(AcName, "DD/MM/YYYY")
            txtProductCode.Text = AcName1
            If txtWEF.Enabled = True Then txtWEF.Focus()
            If ShowRecord() = False Then Exit Sub
        End If
    End Sub

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Public Sub FrmBOMNew_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        If FormActive = True Then Exit Sub

        Me.Text = "Bill Of Material - " & IIf(lblType.Text = "P", "PRODUCTION", "JOBWORK") & IIf(lblApproval.Text = "Y", " (Approval)", "")

        SqlStr = ""

        SqlStr = "Select * from PRD_NEWBOM_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBOMMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * from PRD_NEWBOM_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBOMDetail, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * from PRD_NEWBOM_TOOL_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsToolDetail, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * from PRD_NEWBOM_OTHERS_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsConsumableDetail, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * from PRD_NEWBOM_OTH_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBOMOtherDetail, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From PRD_PRODSEQUENCE_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProdSeqMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From PRD_PRODSEQUENCE_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProdSeqDetail, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * from INV_ITEM_RELATIONSHIP_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTransMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * from INV_ITEM_RELATIONSHIP_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTransDetail, ADODB.LockTypeEnum.adLockReadOnly)

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

    Private Sub FormatSprdMainRel(ByRef Arow As Integer)

        On Error GoTo ERR1
        With SprdMainRel
            .set_RowHeight(0, ConRowHeight)
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)

            .Col = ColRelItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsTransDetail.Fields("ITEM_CODE").DefinedSize ''
            .set_ColWidth(ColRelItemCode, 8)

            .Col = ColRelItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(ColRelItemDesc, 20)

            '        .Col = ColUOM
            '        .CellType = SS_CELL_TYPE_EDIT
            '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            '        .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            '        .TypeEditMultiLine = False
            '        .TypeEditLen = RsTransDetail.Fields("ITEM_UOM").DefinedSize           ''
            '        .ColWidth(ColUOM) = 4
            '
            .Col = ColRelRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = MainClass.SetMaxLength("REMARKS", "INV_ITEM_RELATIONSHIP_DET", PubDBCn)
            .set_ColWidth(ColRelRemarks, 12)

        End With
        MainClass.ProtectCell(SprdMainRel, 1, SprdMainRel.MaxRows, ColRelItemDesc, ColRelItemDesc)
        MainClass.SetSpreadColor(SprdMainRel, Arow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then RsTransDetail.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub ShowRelDetail1(ByRef pItemCode As String)

        On Error GoTo ERR1
        Dim i As Integer
        Dim SqlStr As String = ""
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mUOM As String

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf _
            & " FROM INV_ITEM_RELATIONSHIP_DET  " & vbCrLf _
            & " Where COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
            & " AND ITEM_CODE = '" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
            & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTransDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsTransDetail
            If .EOF = True Then Exit Sub
            '        FormatSprdMainRel -1
            i = 1
            .MoveFirst()

            Do While Not .EOF

                SprdMainRel.Row = i

                SprdMainRel.Col = ColRelItemCode
                mItemCode = IIf(IsDbNull(.Fields("REF_ITEM_CODE").Value), "", .Fields("REF_ITEM_CODE").Value)
                SprdMainRel.Text = mItemCode

                SprdMainRel.Col = ColRelItemDesc
                If MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mItemDesc = MasterNo
                Else
                    mItemDesc = ""
                End If
                SprdMainRel.Text = mItemDesc

                '
                '            SprdMainRel.Col = ColUOM
                '            SprdMainRel.Text = IIf(IsNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)
                '            mUOM = Trim(SprdMainRel.Text)
                '
                SprdMainRel.Col = ColRelRemarks
                SprdMainRel.Text = IIf(IsDbNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)


                .MoveNext()

                i = i + 1
                SprdMainRel.MaxRows = i
            Loop
        End With
        FormatSprdMainRel(-1)

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume
    End Sub
    Private Sub FrmBOMNew_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub FrmBOMNew_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub FrmBOMNew_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        cboProcessType.Items.Clear()
        cboProcessType.Items.Add("1 : BLACK")
        cboProcessType.Items.Add("2 : PAINTING")
        cboProcessType.Items.Add("3 : PLATING")
        cboProcessType.SelectedIndex = -1

        SSTab1.SelectedIndex = 0

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub SetTextLengths()

        On Error GoTo ERR1
        With RsBOMMain
            txtProductCode.Maxlength = .Fields("PRODUCT_CODE").DefinedSize
            txtProductDesc.Maxlength = MainClass.SetMaxLength("ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn)
            txtUnit.Maxlength = .Fields("ISSUE_UOM").DefinedSize
            txtWEF.Maxlength = .Fields("WEF").DefinedSize - 6
            TxtRemarks.Maxlength = .Fields("Remarks").DefinedSize
            txtPreparedBy.Maxlength = .Fields("PREPARED_BY").DefinedSize
            txtApprovedBy.Maxlength = .Fields("APP_EMP_CODE").DefinedSize
            txtSA.Maxlength = .Fields("SURFACE_AREA").Precision
            txtWL.Maxlength = .Fields("WELD_LENGTH").Precision

            txtSAPS_E.Maxlength = .Fields("EXT_PAINT_AREA").Precision
            txtSAPS_I.Maxlength = .Fields("INT_PAINT_AREA").Precision
            txtSAPC.Maxlength = .Fields("COATING_AREA").Precision

            txtProcessCost.Maxlength = .Fields("PROCESS_COST").Precision
            txtOutPutQty.Maxlength = .Fields("OUTPUT_QTY").Precision
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
        '    txtPreparedBy.Enabled = mMode
        '    cmdSearchPrepBy.Enabled = mMode

        txtProductDesc.Enabled = False
        txtUnit.Enabled = False
        txtModelNo.Enabled = False
        txtCustPartNo.Enabled = False
        txtAmendNo.Enabled = False
        txtOutPutQty.Enabled = False


        '    If Trim(txtUnit.Text) = "KGS" Or Trim(txtUnit.Text) = "TON" Or Trim(txtUnit.Text) = "MT" Then
        '        txtOutPutQty.Enabled = True
        '    Else
        '        txtOutPutQty.Enabled = False
        '    End If

    End Sub

    Private Sub FrmBOMNew_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsBOMMain.Close()
        RsBOMDetail.Close()
        RsBOMMain = Nothing
        RsBOMDetail = Nothing
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub Clear1()

        'Dim Index As Integer

        lblMKey.Text = ""
        txtProductCode.Text = ""
        txtProductDesc.Text = ""
        txtUnit.Text = ""
        txtOutPutQty.Text = "1.00"
        txtOutPutQty.Enabled = False
        txtWEF.Enabled = True
        txtWEF.Text = ""
        txtModelNo.Text = ""
        txtCustPartNo.Text = ""
        TxtRemarks.Text = ""

        lblPreparedBy.Text = ""

        txtPreparedBy.Text = PubUserID
        txtPreparedBy_Validating(txtPreparedBy, New System.ComponentModel.CancelEventArgs(False))
        '
        txtPreparedBy.Enabled = False
        cmdSearchPrepBy.Enabled = False

        txtApprovedBy.Text = ""
        chkApproved.CheckState = System.Windows.Forms.CheckState.Unchecked
        lblApprovedBy.Text = ""
        txtAmendNo.Text = "0"
        lblOldWEF.Text = ""
        txtCopyProductCode.Text = ""
        txtCopyProductDesc.Text = ""
        txtCopyAmendNo.Text = ""

        txtSA.Text = ""
        txtSAPS_E.Text = ""
        txtSAPS_I.Text = ""
        txtSAPC.Text = ""
        txtWL.Text = ""
        txtProcessCost.Text = ""
        cboProcessType.SelectedIndex = 0
        chkScrap.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkBOP.CheckState = System.Windows.Forms.CheckState.Unchecked

        chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkStatus.Enabled = False
        mAmendStatus = False
        cmdAmend.Enabled = IIf(InStr(1, XRIGHT, "S") > 0, True, False) '' True

        Call MakeEnableDesableField(True)

        chkApproved.Enabled = IIf(lblApproval.Text = "N", False, True)
        txtApprovedBy.Enabled = False '18-06-2018 IIf(lblApproval.text = "N", False, True)
        cmdSearchAppBy.Enabled = False '18-06-2018 IIf(lblApproval.text = "N", False, True)

        MainClass.ClearGrid(SprdMain)
        FormatSprdMain(-1)

        MainClass.ClearGrid(SprdTool)
        FormatSprdTool(-1)

        MainClass.ClearGrid(SprdOthers)
        FormatSprdOthers(-1)

        MainClass.ClearGrid(SprdSeq, ConRowHeight)
        FormatSprdSeq(-1)

        MainClass.ClearGrid(SprdMainRel)
        Call FormatSprdMainRel(-1)

        MainClass.ClearGrid(SprdMainMWS)
        FormatSprdMainOTH(-1, SprdMainMWS)

        MainClass.ClearGrid(SprdMainPLT)
        FormatSprdMainOTH(-1, SprdMainPLT)

        MainClass.ClearGrid(SprdMainPPS)
        FormatSprdMainOTH(-1, SprdMainPPS)

        MainClass.ClearGrid(SprdMainPC)
        FormatSprdMainOTH(-1, SprdMainPC)

        Call DelTemp_OPRNDetail()
        Call DelTemp_BOMAlterDetail()

        SSTab1.SelectedIndex = 0

        MainClass.ButtonStatus(Me, XRIGHT, RsBOMMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub FormatSprdTool(ByRef mRow As Integer)

        On Error GoTo ERR1
        With SprdTool
            .Row = mRow
            .set_RowHeight(0, ConRowHeight * 1.5)
            .set_RowHeight(mRow, ConRowHeight)



            .Col = ColToolCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsToolDetail.Fields("TOOL_CODE").DefinedSize
            .set_ColWidth(.Col, 10)

            .Col = ColToolDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(.Col, 30)

            .Col = ColToolQUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ISSUE_UOM", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(.Col, 4)

            .Col = ColToolDeptCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsToolDetail.Fields("DEPT_CODE").DefinedSize
            .set_ColWidth(.Col, 4.5)

            .Col = colToolStdQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 3
            .TypeFloatMax = CDbl("99999999999.999")
            .TypeFloatMin = CDbl("-99999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 8)


            .Col = ColToolLife
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 3
            .TypeFloatMax = CDbl("9999999999.999")
            .TypeFloatMin = CDbl("-9999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 8)

            .Col = ColToolRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsToolDetail.Fields("TOOL_REMARKS").DefinedSize
            .set_ColWidth(.Col, 12)

        End With
        MainClass.UnProtectCell(SprdTool, 1, SprdTool.MaxRows, ColToolCode, ColToolRemarks)

        MainClass.ProtectCell(SprdTool, 1, SprdTool.MaxRows, ColToolDesc, ColToolQUnit)
        MainClass.SetSpreadColor(SprdTool, mRow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then
            RsToolDetail.Requery()
            '        Resume
        End If
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub FormatSprdOthers(ByRef mRow As Integer)

        On Error GoTo ERR1
        With SprdOthers
            .Row = mRow
            .set_RowHeight(0, ConRowHeight * 1.5)
            .set_RowHeight(mRow, ConRowHeight)


            .Col = ColConsumableCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsConsumableDetail.Fields("CONSUMABLE_CODE").DefinedSize
            .set_ColWidth(.Col, 10)

            .Col = ColConsumableDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(.Col, 30)

            .Col = ColConsumableUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ISSUE_UOM", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(.Col, 4)

            .Col = ColConsumableDeptCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsConsumableDetail.Fields("DEPT_CODE").DefinedSize
            .set_ColWidth(.Col, 4.5)

            .Col = colConsumableOnQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 3
            .TypeFloatMax = CDbl("99999999999.999")
            .TypeFloatMin = CDbl("-99999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 8)


            .Col = ColConsumableQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 3
            .TypeFloatMax = CDbl("9999999999.999")
            .TypeFloatMin = CDbl("-9999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 8)

            .Col = ColConsumableRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsConsumableDetail.Fields("CONSUMABLE_REMARKS").DefinedSize
            .set_ColWidth(.Col, 12)

        End With
        MainClass.UnProtectCell(SprdOthers, 1, SprdOthers.MaxRows, ColConsumableCode, ColConsumableRemarks)

        MainClass.ProtectCell(SprdOthers, 1, SprdOthers.MaxRows, ColConsumableDesc, ColConsumableUnit)
        MainClass.SetSpreadColor(SprdOthers, mRow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then
            RsToolDetail.Requery()
            '        Resume
        End If
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub FormatSprdMain(ByRef mRow As Integer)

        On Error GoTo ERR1
        With SprdMain
            .Row = mRow
            .set_RowHeight(0, ConRowHeight * 1.5)
            .set_RowHeight(mRow, ConRowHeight)

            .Col = ColDeptCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsBOMDetail.Fields("DEPT_CODE").DefinedSize
            .set_ColWidth(.Col, 4.5)

            .Col = ColRMCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsBOMDetail.Fields("RM_CODE").DefinedSize
            .set_ColWidth(.Col, 8)

            .Col = ColRMDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(.Col, 30)

            .Col = ColPartNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("CUSTOMER_PART_NO", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(.Col, 10)

            .Col = colStdQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 3
            .TypeFloatMax = CDbl("99999999999.999")
            .TypeFloatMin = CDbl("-99999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 7)

            .Col = ColQUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ISSUE_UOM", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(.Col, 4)

            .Col = ColGrossWtScrp
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 3
            .TypeFloatMax = CDbl("9999999999.999")
            .TypeFloatMin = CDbl("-9999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 6)

            .Col = ColWtVar
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 3
            .TypeFloatMax = CDbl("9999999999.99")
            .TypeFloatMin = CDbl("-9999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 6)

            .Col = ColStockType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsBOMDetail.Fields("STOCK_TYPE").DefinedSize
            .set_ColWidth(.Col, 3.5)

            .Col = ColAlternate
            .CellType = SS_CELL_TYPE_BUTTON
            .TypeButtonText = "Alternate"
            .TypeButtonAlign = SS_CELL_BUTTON_ALIGN_LEFT
            .set_ColWidth(ColAlternate, 6)

            .Col = ColOperation
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = MainClass.SetMaxLength("OPR_DESC", "PRD_OPR_MST", PubDBCn)
            .set_ColWidth(.Col, 12)

        End With
        MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, ColDeptCode, ColStockType)

        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColRMDesc, ColQUnit)
        MainClass.SetSpreadColor(SprdMain, mRow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then
            RsBOMDetail.Requery()
            '        Resume
        End If
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub

    Private Sub FormatSprdMainOTH(ByRef mRow As Integer, ByRef pSpread As AxFPSpreadADO.AxfpSpread)

        On Error GoTo ERR1
        Dim cntCol As Integer

        With pSpread
            .Row = mRow
            .set_RowHeight(0, ConRowHeight * 1.5)
            .set_RowHeight(mRow, ConRowHeight)

            .Col = ColIsAlterOTH
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            '        If mISAlter = "Y" Then
            '
            '        Else
            '            .Value = vbUnchecked
            '        End If
            .set_ColWidth(ColIsAlterOTH, 4)

            .Col = ColMainItemCodeOTH
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = MainClass.SetMaxLength("ITEM_CODE", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(.Col, 6)

            .Col = ColItemCodeOTH
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = MainClass.SetMaxLength("ITEM_CODE", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(.Col, 6)

            .Col = ColItemDescOTH
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(.Col, 18)

            .Col = ColDeptCodeOTH
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = MainClass.SetMaxLength("DEPT_CODE", "PAY_DEPT_MST", PubDBCn)
            .set_ColWidth(.Col, 4.5)

            .Col = ColOprOTH
            .CellType = SS_CELL_TYPE_COMBOBOX
            If UCase(pSpread.Name) = UCase("SprdMainMWS") Then
                .TypeComboBoxList = "11.MIG WELD" & Chr(9) & "12.SS WELD" & Chr(9) & "13.SPOT WELD" & Chr(9) & "14.TIG WELD"
            ElseIf UCase(pSpread.Name) = UCase("SprdMainPPS") Then
                .TypeComboBoxList = "21.EXTERNAL" & Chr(9) & "22.INTERNAL"
            ElseIf UCase(pSpread.Name) = UCase("SprdMainPLT") Then
                .TypeComboBoxList = "31.NICKEL" & Chr(9) & "32.ZINC"
            ElseIf UCase(pSpread.Name) = UCase("SprdMainPC") Then
                .TypeComboBoxList = "41.POWDER"
            End If

            .TypeComboBoxCurSel = 0
            .set_ColWidth(.Col, 10)

            .Col = ColRemarkOTH
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            '        .TypeEditLen = MainClass.SetMaxLength("DEPT_CODE", "PAY_DEPT_MST", PubDBCn)
            .set_ColWidth(.Col, 12)

            .Col = ColQUnitOTH
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ISSUE_UOM", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(.Col, 4)

            For cntCol = colStdQtyOTH To ColNetConsumptionOTH
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatDecimalPlaces = 4
                .TypeFloatMax = CDbl("9999999.9999")
                .TypeFloatMin = CDbl("-9999999.9999")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(.Col, 7)
            Next

        End With

        MainClass.UnProtectCell(pSpread, 1, pSpread.MaxRows, ColIsAlterOTH, ColNetConsumptionOTH)

        '    If mISAlter = "Y" Then
        ''        MainClass.ProtectCell pSpread, 1, pSpread.MaxRows, ColItemDescOTH, ColQUnitOTH
        '    Else
        '        MainClass.ProtectCell pSpread, 1, pSpread.MaxRows, ColMainItemCodeOTH, ColMainItemCodeOTH
        '    End If

        MainClass.ProtectCell(pSpread, 1, pSpread.MaxRows, ColItemDescOTH, ColQUnitOTH)
        MainClass.ProtectCell(pSpread, 1, pSpread.MaxRows, ColNetConsumptionOTH, ColNetConsumptionOTH)
        MainClass.SetSpreadColor(pSpread, mRow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then
            RsBOMOtherDetail.Requery()
            '        Resume
        End If
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub Show1()

        On Error GoTo ERR1
        Dim mProcessType As Integer

        With RsBOMMain
            If Not .EOF Then
                ADDMode = False
                MODIFYMode = False
                lblMKey.Text = .Fields("MKey").Value
                lblType.Text = .Fields("BOM_TYPE").Value

                txtProductCode.Text = Trim(IIf(IsDbNull(.Fields("PRODUCT_CODE").Value), "", .Fields("PRODUCT_CODE").Value))

                If MainClass.ValidateWithMasterTable(txtProductCode.Text, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
                    txtProductDesc.Text = MasterNo
                End If

                txtUnit.Text = IIf(IsDbNull(.Fields("ISSUE_UOM").Value), "", .Fields("ISSUE_UOM").Value)
                txtOutPutQty.Text = VB6.Format(IIf(IsDbNull(.Fields("OUTPUT_QTY").Value), 1, .Fields("OUTPUT_QTY").Value), "0.00")
                txtWEF.Text = IIf(IsDbNull(.Fields("WEF").Value), "", .Fields("WEF").Value)
                lblOldWEF.Text = IIf(IsDbNull(.Fields("WEF").Value), "", .Fields("WEF").Value)

                If MainClass.ValidateWithMasterTable(txtProductCode.Text, "ITEM_CODE", "ITEM_MODEL", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
                    txtModelNo.Text = MasterNo
                End If

                If MainClass.ValidateWithMasterTable(txtProductCode.Text, "ITEM_CODE", "CUSTOMER_PART_NO", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
                    txtCustPartNo.Text = MasterNo
                End If
                txtAmendNo.Text = IIf(IsDbNull(.Fields("AMEND_NO").Value), "", .Fields("AMEND_NO").Value)
                chkStatus.CheckState = IIf(.Fields("Status").Value = "O", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)

                chkStatus.Enabled = IIf(.Fields("Status").Value = "O", True, False)

                TxtRemarks.Text = IIf(IsDbNull(.Fields("Remarks").Value), "", .Fields("Remarks").Value)
                txtProcessCost.Text = VB6.Format(IIf(IsDbNull(.Fields("PROCESS_COST").Value), 0, .Fields("PROCESS_COST").Value), "0.000")

                txtSA.Text = VB6.Format(IIf(IsDbNull(.Fields("SURFACE_AREA").Value), 0, .Fields("SURFACE_AREA").Value), "0.0000")
                txtWL.Text = VB6.Format(IIf(IsDbNull(.Fields("WELD_LENGTH").Value), 0, .Fields("WELD_LENGTH").Value), "0.0000")
                txtSAPS_E.Text = VB6.Format(IIf(IsDbNull(.Fields("EXT_PAINT_AREA").Value), 0, .Fields("EXT_PAINT_AREA").Value), "0.0000")
                txtSAPS_I.Text = VB6.Format(IIf(IsDbNull(.Fields("INT_PAINT_AREA").Value), 0, .Fields("INT_PAINT_AREA").Value), "0.0000")
                txtSAPC.Text = VB6.Format(IIf(IsDbNull(.Fields("COATING_AREA").Value), 0, .Fields("COATING_AREA").Value), "0.0000")

                mProcessType = IIf(IsDbNull(.Fields("PROCESS_TYPE").Value), 0, .Fields("PROCESS_TYPE").Value)


                If mProcessType = 1 Then
                    cboProcessType.SelectedIndex = 0
                ElseIf mProcessType = 2 Then
                    cboProcessType.SelectedIndex = 1
                ElseIf mProcessType = 3 Then
                    cboProcessType.SelectedIndex = 2
                Else
                    cboProcessType.SelectedIndex = -1
                End If

                txtPreparedBy.Text = IIf(IsDbNull(.Fields("PREPARED_BY").Value), "", .Fields("PREPARED_BY").Value)
                txtPreparedBy_Validating(txtPreparedBy, New System.ComponentModel.CancelEventArgs(False))

                chkApproved.CheckState = IIf(.Fields("IS_APPROVED").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                txtApprovedBy.Text = IIf(IsDbNull(.Fields("APP_EMP_CODE").Value), "", .Fields("APP_EMP_CODE").Value)
                txtApprovedBy_Validating(txtApprovedBy, New System.ComponentModel.CancelEventArgs(False))

                chkScrap.CheckState = IIf(.Fields("FROM_SCRAP").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkBOP.CheckState = IIf(.Fields("IS_BOP").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)


                Call ShowBOMDetail1((lblMKey.Text))
                Call ShowBOMAlterDetail((lblMKey.Text))
                Call ShowSeqDetail1((txtProductCode.Text))
                Call ShowOperation((txtProductCode.Text))
                Call ShowRelDetail1((txtProductCode.Text))

                Call ShowToolDetail1((lblMKey.Text))
                Call ShowOthersDetail1((lblMKey.Text))
                Call ShowBOMOTHDetail1((lblMKey.Text), SprdMainMWS, "MWS")
                Call ShowBOMOTHDetail1((lblMKey.Text), SprdMainPLT, "PLT")
                Call ShowBOMOTHDetail1((lblMKey.Text), SprdMainPPS, "PPS")
                Call ShowBOMOTHDetail1((lblMKey.Text), SprdMainPC, "NPC")
                Call MakeEnableDesableField(False)

                If chkApproved.CheckState = System.Windows.Forms.CheckState.Checked Then
                    chkApproved.Enabled = False
                    txtApprovedBy.Enabled = False
                    cmdSearchAppBy.Enabled = False
                End If
            End If
        End With
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsBOMMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        '    SprdMain.Enabled = True    '' False Sandeep 15/05/2022
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
        '    Resume
    End Sub
    Private Sub ShowToolDetail1(ByRef pMKey As String)

        On Error GoTo ERR1
        Dim i As Integer
        Dim SqlStr As String = ""
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mUOM As String

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf _
            & " FROM PRD_NEWBOM_TOOL_DET  " & vbCrLf _
            & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND MKEY = '" & MainClass.AllowSingleQuote(pMKey) & "'" & vbCrLf _
            & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsToolDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsToolDetail
            If .EOF = True Then Exit Sub

            i = 1
            .MoveFirst()

            Do While Not .EOF

                SprdTool.Row = i

                SprdTool.Col = ColToolCode
                mItemCode = IIf(IsDBNull(.Fields("TOOL_CODE").Value), "", .Fields("TOOL_CODE").Value)
                SprdTool.Text = mItemCode

                SprdTool.Col = ColToolDesc
                If MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mItemDesc = MasterNo
                Else
                    mItemDesc = ""
                End If
                SprdTool.Text = mItemDesc

                SprdTool.Col = ColToolQUnit
                If MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "ISSUE_UOM", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mItemDesc = MasterNo
                Else
                    mItemDesc = ""
                End If
                SprdTool.Text = mItemDesc

                SprdTool.Col = ColToolDeptCode
                SprdTool.Text = IIf(IsDBNull(.Fields("DEPT_CODE").Value), "", .Fields("DEPT_CODE").Value)

                SprdTool.Col = colToolStdQty
                SprdTool.Text = IIf(IsDBNull(.Fields("TOOL_STD_QTY").Value), 0, .Fields("TOOL_STD_QTY").Value)

                SprdTool.Col = ColToolLife
                SprdTool.Text = IIf(IsDBNull(.Fields("TOOL_LIFE").Value), 0, .Fields("TOOL_LIFE").Value)

                SprdTool.Col = ColToolRemarks
                SprdTool.Text = IIf(IsDBNull(.Fields("TOOL_REMARKS").Value), "", .Fields("TOOL_REMARKS").Value)


                .MoveNext()

                i = i + 1
                SprdTool.MaxRows = i
            Loop
        End With
        FormatSprdTool(-1)

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume
    End Sub

    Private Sub ShowOthersDetail1(ByRef pMKey As String)

        On Error GoTo ERR1
        Dim i As Integer
        Dim SqlStr As String = ""
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mUOM As String

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf _
            & " FROM PRD_NEWBOM_OTHERS_DET  " & vbCrLf _
            & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND MKEY = '" & MainClass.AllowSingleQuote(pMKey) & "'" & vbCrLf _
            & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsConsumableDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsConsumableDetail
            If .EOF = True Then Exit Sub

            i = 1
            .MoveFirst()

            Do While Not .EOF

                SprdOthers.Row = i

                SprdOthers.Col = ColConsumableCode
                mItemCode = IIf(IsDBNull(.Fields("CONSUMABLE_CODE").Value), "", .Fields("CONSUMABLE_CODE").Value)
                SprdOthers.Text = mItemCode

                SprdOthers.Col = ColConsumableDesc
                If MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mItemDesc = MasterNo
                Else
                    mItemDesc = ""
                End If
                SprdOthers.Text = mItemDesc

                SprdOthers.Col = ColConsumableUnit
                If MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "ISSUE_UOM", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mItemDesc = MasterNo
                Else
                    mItemDesc = ""
                End If
                SprdOthers.Text = mItemDesc

                SprdOthers.Col = ColConsumableDeptCode
                SprdOthers.Text = IIf(IsDBNull(.Fields("DEPT_CODE").Value), "", .Fields("DEPT_CODE").Value)

                SprdOthers.Col = colConsumableOnQty
                SprdOthers.Text = IIf(IsDBNull(.Fields("CONSUMABLE_ON_QTY").Value), 0, .Fields("CONSUMABLE_ON_QTY").Value)

                SprdOthers.Col = ColConsumableQty
                SprdOthers.Text = IIf(IsDBNull(.Fields("CONSUMABLE_QTY").Value), 0, .Fields("CONSUMABLE_QTY").Value)

                SprdOthers.Col = ColConsumableRemarks
                SprdOthers.Text = IIf(IsDBNull(.Fields("CONSUMABLE_REMARKS").Value), "", .Fields("CONSUMABLE_REMARKS").Value)

                .MoveNext()

                i = i + 1
                SprdOthers.MaxRows = i
            Loop
        End With
        FormatSprdOthers(-1)

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume
    End Sub
    Private Sub ShowBOMDetail1(ByRef nMkey As String)

        On Error GoTo ERR1
        Dim i As Integer
        Dim SqlStr As String = ""
        Dim mItemDesc As String
        Dim mOPRCode As String
        Dim mOPRDesc As String
        Dim mDeptCode As String
        Dim mRMCode As String

        SqlStr = ""
        SqlStr = " SELECT * FROM PRD_NEWBOM_DET " & vbCrLf & " WHERE Mkey='" & MainClass.AllowSingleQuote(nMkey) & "'" & " ORDER BY SubRowNo"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBOMDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsBOMDetail
            If Not .EOF Then
                If .EOF = True Then Exit Sub
                SprdMain.MaxRows = MainClass.GetMaxRecord("PRD_NEWBOM_DET", PubDBCn, " Mkey='" & MainClass.AllowSingleQuote(nMkey) & "'") + 1
                FormatSprdMain(-1)
                i = 0
                .MoveFirst()
                Do While Not .EOF
                    i = i + 1
                    SprdMain.Row = i

                    SprdMain.Col = ColDeptCode
                    SprdMain.Text = IIf(IsDbNull(.Fields("DEPT_CODE").Value), "", .Fields("DEPT_CODE").Value)
                    mDeptCode = Trim(SprdMain.Text)

                    SprdMain.Col = ColRMCode
                    SprdMain.Text = Trim(IIf(IsDbNull(.Fields("RM_CODE").Value), "", .Fields("RM_CODE").Value))
                    mRMCode = Trim(IIf(IsDBNull(.Fields("RM_CODE").Value), "", .Fields("RM_CODE").Value))

                    SprdMain.Col = ColRMDesc
                    If MainClass.ValidateWithMasterTable(mRMCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "company_code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mItemDesc = MasterNo

                    Else
                        mItemDesc = ""
                    End If
                    SprdMain.Text = mItemDesc

                    SprdMain.Col = ColPartNo
                    If MainClass.ValidateWithMasterTable(mRMCode, "ITEM_CODE", "CUSTOMER_PART_NO", "INV_ITEM_MST", PubDBCn, MasterNo, , "company_code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        SprdMain.Text = MasterNo
                    End If

                    SprdMain.Col = colStdQty
                    SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("STD_QTY").Value), "", .Fields("STD_QTY").Value)))

                    SprdMain.Col = ColQUnit
                    If MainClass.ValidateWithMasterTable(mRMCode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "company_code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        SprdMain.Text = MasterNo
                    End If

                    SprdMain.Col = ColGrossWtScrp
                    SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("GROSS_WT_SCRAP").Value), "", .Fields("GROSS_WT_SCRAP").Value)))

                    SprdMain.Col = ColWtVar
                    SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("QTY_VAR").Value), "", .Fields("QTY_VAR").Value)))

                    SprdMain.Col = ColStockType
                    SprdMain.Text = Trim(IIf(IsDbNull(.Fields("STOCK_TYPE").Value), "", .Fields("STOCK_TYPE").Value))

                    mOPRCode = IIf(IsDbNull(.Fields("OPR_CODE").Value), "", .Fields("OPR_CODE").Value)

                    If MainClass.ValidateWithMasterTable(mOPRCode, "OPR_CODE", "OPR_DESC", "PRD_OPR_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(mDeptCode) & "'") = True Then
                        mOPRDesc = MasterNo
                    Else
                        mOPRDesc = ""
                    End If
                    SprdMain.Col = ColOperation
                    SprdMain.Text = mOPRDesc

                    .MoveNext()
                Loop
            End If
        End With
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub

    Private Sub ShowBOMOTHDetail1(ByRef nMkey As String, ByRef pSprd As AxFPSpreadADO.AxfpSpread, ByRef pBookType As String)

        On Error GoTo ERR1
        Dim i As Integer
        Dim SqlStr As String = ""
        Dim mItemDesc As String
        Dim mOPRCode As String
        Dim mOPRDesc As String
        Dim mDeptCode As String

        Dim mIsAlter As String
        Dim mOPRType As String

        SqlStr = ""
        SqlStr = " SELECT * FROM PRD_NEWBOM_OTH_DET " & vbCrLf & " WHERE Mkey='" & MainClass.AllowSingleQuote(nMkey) & "'" & vbCrLf & " AND BOOK_TYPE='" & pBookType & "'" & vbCrLf & " ORDER BY SubRowNo"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBOMOtherDetail, ADODB.LockTypeEnum.adLockReadOnly)

        With RsBOMOtherDetail
            If Not .EOF Then
                If .EOF = True Then Exit Sub
                pSprd.MaxRows = MainClass.GetMaxRecord("PRD_NEWBOM_OTH_DET", PubDBCn, " Mkey='" & MainClass.AllowSingleQuote(nMkey) & "' AND BOOK_TYPE='" & pBookType & "'") + 1
                FormatSprdMainOTH(-1, pSprd)
                i = 0
                .MoveFirst()
                Do While Not .EOF
                    i = i + 1
                    pSprd.Row = i

                    pSprd.Col = ColIsAlterOTH
                    mIsAlter = IIf(IsDbNull(.Fields("IS_ALTER").Value), "N", .Fields("IS_ALTER").Value)
                    pSprd.Value = IIf(mIsAlter = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                    pSprd.Col = ColMainItemCodeOTH
                    pSprd.Text = Trim(IIf(IsDbNull(.Fields("MAIN_ITEM_CODE").Value), "", .Fields("MAIN_ITEM_CODE").Value))

                    pSprd.Col = ColItemCodeOTH
                    pSprd.Text = Trim(IIf(IsDbNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))

                    pSprd.Col = ColItemDescOTH
                    If MainClass.ValidateWithMasterTable(.Fields("ITEM_CODE"), "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "company_code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mItemDesc = MasterNo

                    Else
                        mItemDesc = ""
                    End If
                    pSprd.Text = mItemDesc

                    pSprd.Col = ColQUnitOTH
                    If MainClass.ValidateWithMasterTable(.Fields("ITEM_CODE"), "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "company_code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        pSprd.Text = MasterNo
                    End If

                    pSprd.Col = ColDeptCodeOTH
                    pSprd.Text = IIf(IsDbNull(.Fields("DEPT_CODE").Value), "", .Fields("DEPT_CODE").Value)
                    mDeptCode = Trim(pSprd.Text)

                    pSprd.Col = ColOprOTH
                    mOPRType = IIf(IsDbNull(.Fields("OPERATION_TYPE").Value), "", .Fields("OPERATION_TYPE").Value)

                    If UCase(pSprd.Name) = UCase("SprdMainMWS") Then
                        If mOPRType = "11" Then
                            pSprd.TypeComboBoxCurSel = 0
                        ElseIf mOPRType = "12" Then
                            pSprd.TypeComboBoxCurSel = 1
                        ElseIf mOPRType = "13" Then
                            pSprd.TypeComboBoxCurSel = 2
                        ElseIf mOPRType = "14" Then
                            pSprd.TypeComboBoxCurSel = 3
                        End If
                    ElseIf UCase(pSprd.Name) = UCase("SprdMainPPS") Then
                        If mOPRType = "21" Then
                            pSprd.TypeComboBoxCurSel = 0
                        ElseIf mOPRType = "22" Then
                            pSprd.TypeComboBoxCurSel = 1
                        End If
                    ElseIf UCase(pSprd.Name) = UCase("SprdMainPLT") Then
                        If mOPRType = "31" Then
                            pSprd.TypeComboBoxCurSel = 0
                        ElseIf mOPRType = "32" Then
                            pSprd.TypeComboBoxCurSel = 1
                        End If
                    ElseIf UCase(pSprd.Name) = UCase("SprdMainPC") Then
                        pSprd.TypeComboBoxCurSel = 0
                    End If



                    pSprd.Col = ColRemarkOTH
                    pSprd.Text = Trim(IIf(IsDbNull(.Fields("OPERATION_DESC").Value), "", .Fields("OPERATION_DESC").Value))


                    pSprd.Col = colStdQtyOTH
                    pSprd.Text = CStr(Val(IIf(IsDbNull(.Fields("AREA_LEN_NOS").Value), "", .Fields("AREA_LEN_NOS").Value)))

                    pSprd.Col = colStdConsOTH
                    pSprd.Text = CStr(Val(IIf(IsDbNull(.Fields("UNIT_QTY").Value), "", .Fields("UNIT_QTY").Value)))

                    pSprd.Col = ColNetConsumptionOTH
                    pSprd.Text = CStr(Val(IIf(IsDbNull(.Fields("NET_CONSUMPTION").Value), "", .Fields("NET_CONSUMPTION").Value)))

                    .MoveNext()
                Loop
            End If
        End With
        Call CalcTots(pSprd)
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        '    Resume
    End Sub

    Private Function UpdateDetail1() As Boolean

        On Error GoTo UpdateDetail1
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim SqlStr As String = ""
        Dim i As Integer
        Dim mRMType As String
        Dim mDeptCode As String
        Dim mRMCode As String
        Dim mRMDesc As String
        Dim mPartNo As String
        Dim mStdQty As Double
        Dim mQUnit As String
        Dim mGrossWtScrp As Double
        Dim mWtVar As Double
        Dim mStockType As String
        Dim mOPRDesc As String
        Dim mOPRCode As String

        PubDBCn.Execute("DELETE FROM PRD_BOM_ALTER_DET  " & vbCrLf & " WHERE Mkey='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")

        PubDBCn.Execute("DELETE FROM PRD_NEWBOM_DET  " & vbCrLf & " WHERE Mkey='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")

        With SprdMain
            For i = 1 To .MaxRows - 1
                .Row = i

                .Col = ColDeptCode
                mDeptCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColRMCode
                mRMCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColRMDesc
                mRMDesc = MainClass.AllowSingleQuote(.Text)

                .Col = ColPartNo
                mPartNo = MainClass.AllowSingleQuote(.Text)

                .Col = colStdQty
                mStdQty = Val(.Text)

                .Col = ColQUnit
                mQUnit = MainClass.AllowSingleQuote(.Text)

                .Col = ColGrossWtScrp
                mGrossWtScrp = Val(.Text)

                .Col = ColWtVar
                mWtVar = Val(.Text)

                .Col = ColStockType
                mStockType = MainClass.AllowSingleQuote(.Text)

                .Col = ColOperation
                mOPRDesc = MainClass.AllowSingleQuote(.Text)
                If Trim(mOPRDesc) = "" Then
                    mOPRCode = ""
                Else
                    SqlStr = " SELECT OPR_CODE " & vbCrLf & " FROM PRD_OPR_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND DEPT_CODE='" & mDeptCode & "'" & vbCrLf & " AND OPR_DESC='" & mOPRDesc & "'"

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                    If RsTemp.EOF = False Then
                        mOPRCode = IIf(IsDbNull(RsTemp.Fields("OPR_CODE").Value), "", RsTemp.Fields("OPR_CODE").Value)
                    Else
                        mOPRCode = ""
                    End If
                End If

                SqlStr = ""
                If Trim(mRMCode) <> "" And mStdQty <> 0 Then
                    SqlStr = " INSERT INTO  PRD_NEWBOM_DET ( " & vbCrLf & " MKEY, COMPANY_CODE, " & vbCrLf & " PRODUCT_CODE, SUBROWNO, " & vbCrLf & " DEPT_CODE, RM_CODE, " & vbCrLf & " STD_QTY, GROSS_WT_SCRAP, QTY_VAR, " & vbCrLf & " STOCK_TYPE, OPR_CODE ) VALUES ( " & vbCrLf & " '" & MainClass.AllowSingleQuote(lblMKey.Text) & "', " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " '" & MainClass.AllowSingleQuote(txtProductCode.Text) & "', " & i & ", " & vbCrLf & " '" & mDeptCode & "', '" & mRMCode & "', " & vbCrLf & " " & mStdQty & ", " & mGrossWtScrp & ", " & mWtVar & ", " & vbCrLf & " '" & mStockType & "', '" & mOPRCode & "')"

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        UpdateDetail1 = True
        Exit Function
UpdateDetail1:
        MsgBox(Err.Description)
        UpdateDetail1 = False
    End Function

    Private Function UpdateOTHDetail1(ByRef nMkey As String, ByRef pSprd As AxFPSpreadADO.AxfpSpread, ByRef pBookType As String) As Boolean

        On Error GoTo UpdateDetail1
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim SqlStr As String = ""
        Dim i As Integer
        Dim mIsAlter As String
        Dim mDeptCode As String
        Dim mOPRType As String
        Dim mRemark As String
        Dim mRMCode As String
        Dim mRMDesc As String
        Dim mQUnit As String
        Dim mStdQty As Double
        Dim mStdQtyConsumption As Double
        Dim mNetConsumption As Double
        Dim mMainItemCode As String

        PubDBCn.Execute("DELETE FROM PRD_NEWBOM_OTH_DET  " & vbCrLf & " WHERE Mkey='" & MainClass.AllowSingleQuote(UCase(nMkey)) & "'" & vbCrLf & " AND BOOK_TYPE='" & pBookType & "'")

        With pSprd
            For i = 1 To .MaxRows - 1
                .Row = i
                .Col = ColIsAlterOTH
                mIsAlter = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                .Col = ColMainItemCodeOTH
                mMainItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColDeptCodeOTH
                mDeptCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColOprOTH
                If Trim(.Text) = "" Then
                    mOPRType = ""
                Else
                    mOPRType = VB.Left(.Text, 2)
                End If

                .Col = ColRemarkOTH
                mRemark = MainClass.AllowSingleQuote(.Text)

                .Col = ColItemCodeOTH
                mRMCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColItemDescOTH
                mRMDesc = MainClass.AllowSingleQuote(.Text)

                .Col = ColQUnitOTH
                mQUnit = MainClass.AllowSingleQuote(.Text)

                .Col = colStdQtyOTH
                mStdQty = Val(.Text)

                .Col = colStdConsOTH
                mStdQtyConsumption = Val(.Text)

                .Col = ColNetConsumptionOTH
                mNetConsumption = Val(.Text)

                SqlStr = ""
                If Trim(mRMCode) <> "" And mStdQty <> 0 Then
                    SqlStr = " INSERT INTO  PRD_NEWBOM_OTH_DET ( " & vbCrLf & " MKEY, COMPANY_CODE, BOOK_TYPE, SUBROWNO, " & vbCrLf & " PRODUCT_CODE, DEPT_CODE, ITEM_CODE, " & vbCrLf & " OPERATION_TYPE, OPERATION_DESC, AREA_LEN_NOS, " & vbCrLf & " UNIT_QTY, NET_CONSUMPTION, IS_ALTER, MAIN_ITEM_CODE) VALUES ( " & vbCrLf & " '" & MainClass.AllowSingleQuote(lblMKey.Text) & "', " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " '" & pBookType & "', " & i & "," & vbCrLf & " '" & MainClass.AllowSingleQuote(txtProductCode.Text) & "', '" & mDeptCode & "'," & vbCrLf & " '" & mRMCode & "', '" & mOPRType & "', '" & MainClass.AllowSingleQuote(mRemark) & "', " & vbCrLf & " " & mStdQty & ", " & mStdQtyConsumption & ", " & mNetConsumption & ",'" & mIsAlter & "', '" & mMainItemCode & "')"

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With


        UpdateOTHDetail1 = True
        Exit Function
UpdateDetail1:
        MsgBox(Err.Description)
        UpdateOTHDetail1 = False
        '    Resume
    End Function
    Public Function UpdateItemConsumption(ByRef pItemCode As String) As Boolean
        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing

        UpdateItemConsumption = False
        If pItemCode = "" Then
            Exit Function
        End If

        If CheckItemConsumptionExists(Trim(pItemCode), "N") = True Then

            mSqlStr = " UPDATE PRD_OUTBOM_HDR SET IS_INHOUSE='Y'" & vbCrLf _
                   & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
                   & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
                   & " AND STATUS='O' AND IS_INHOUSE='N'"

            PubDBCn.Execute(mSqlStr)
        End If
        UpdateItemConsumption = True

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function Update1() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim nMkey As String
        Dim mRowNo As Integer
        Dim mInHouse As String
        Dim mFromScrap As String
        Dim mSeqAddMode As Boolean
        Dim mStatus As String
        Dim mProcessType As Integer
        Dim mBOP As String

        mStatus = IIf(chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked, "O", "C")

        If MainClass.ValidateWithMasterTable(Trim(txtProductCode.Text), "PRODUCT_CODE", "PRODUCT_CODE", "PRD_PRODSEQUENCE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "  AND WEF=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')") = True Then
            mSeqAddMode = False
        Else
            mSeqAddMode = True
        End If

        If Trim(cboProcessType.Text) = "" Then
            mProcessType = 0
        Else
            mProcessType = CInt(VB.Left(cboProcessType.Text, 1))
        End If


        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = ""


        mFromScrap = IIf(chkScrap.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mBOP = IIf(chkBOP.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        If ADDMode = True Then

            mRowNo = MainClass.AutoGenRowNo("PRD_NEWBOM_HDR", "RowNo", PubDBCn)
            nMkey = RsCompany.Fields("COMPANY_CODE").Value & mRowNo & UCase(Trim(txtProductCode.Text)) & VB6.Format(txtWEF.Text, "YYYYMMDD")
            lblMKey.Text = nMkey

            SqlStr = " INSERT INTO PRD_NEWBOM_HDR (" & vbCrLf & " MKEY, COMPANY_CODE, ROWNO, " & vbCrLf & " BOM_TYPE, PRODUCT_CODE, ISSUE_UOM, " & vbCrLf & " WEF, REMARKS, PREPARED_BY, " & vbCrLf & " APP_EMP_CODE, FROM_SCRAP, STATUS, AMEND_NO," & vbCrLf & " SURFACE_AREA,WELD_LENGTH,PROCESS_TYPE, PROCESS_COST," & vbCrLf & " ADDUSER, ADDDATE, " & vbCrLf & " MODUSER, MODDATE,IS_BOP,OUTPUT_QTY, " & vbCrLf & " EXT_PAINT_AREA, INT_PAINT_AREA, COATING_AREA, IS_APPROVED" & vbCrLf & " ) VALUES ( "

            SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(nMkey) & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & mRowNo & ", " & vbCrLf & " '" & lblType.Text & "', '" & MainClass.AllowSingleQuote(txtProductCode.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtUnit.Text) & "', TO_DATE('" & VB6.Format(txtWEF.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & MainClass.AllowSingleQuote(TxtRemarks.Text) & "', '" & MainClass.AllowSingleQuote(txtPreparedBy.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtApprovedBy.Text) & "', '" & mFromScrap & "', '" & mStatus & "', " & Val(txtAmendNo.Text) & "," & vbCrLf & " " & Val(VB6.Format(txtSA.Text, "0.0000")) & "," & Val(VB6.Format(txtWL.Text, "0.0000")) & "," & Val(CStr(mProcessType)) & "," & Val(txtProcessCost.Text) & "," & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','','" & mBOP & "'," & Val(txtOutPutQty.Text) & "," & vbCrLf & " " & Val(VB6.Format(txtSAPS_E.Text, "0.0000")) & "," & Val(VB6.Format(txtSAPS_I.Text, "0.0000")) & "," & Val(VB6.Format(txtSAPC.Text, "0.0000")) & ", '" & IIf(chkApproved.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "')"

        ElseIf MODIFYMode = True Then
            SqlStr = ""
            SqlStr = " UPDATE PRD_NEWBOM_HDR  SET " & vbCrLf & " BOM_TYPE='" & lblType.Text & "', " & vbCrLf & " PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "', " & vbCrLf & " ISSUE_UOM='" & MainClass.AllowSingleQuote(txtUnit.Text) & "', " & vbCrLf & " WEF=TO_DATE('" & VB6.Format(txtWEF.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " REMARKS='" & MainClass.AllowSingleQuote(TxtRemarks.Text) & "', " & vbCrLf & " PREPARED_BY='" & MainClass.AllowSingleQuote(txtPreparedBy.Text) & "', " & vbCrLf & " APP_EMP_CODE='" & MainClass.AllowSingleQuote(txtApprovedBy.Text) & "', " & vbCrLf & " FROM_SCRAP='" & mFromScrap & "', " & vbCrLf & " STATUS='" & mStatus & "', PROCESS_COST=" & Val(txtProcessCost.Text) & "," & vbCrLf & " SURFACE_AREA=" & Val(VB6.Format(txtSA.Text, "0.0000")) & ", " & vbCrLf & " WELD_LENGTH=" & Val(VB6.Format(txtWL.Text, "0.0000")) & ", " & vbCrLf & " EXT_PAINT_AREA=" & Val(VB6.Format(txtSAPS_E.Text, "0.0000")) & ", " & vbCrLf & " INT_PAINT_AREA=" & Val(VB6.Format(txtSAPS_I.Text, "0.0000")) & ", " & vbCrLf & " COATING_AREA=" & Val(VB6.Format(txtSAPC.Text, "0.0000")) & ", " & vbCrLf & " PROCESS_TYPE=" & Val(CStr(mProcessType)) & ", IS_BOP='" & mBOP & "'," & vbCrLf & " IS_APPROVED='" & IIf(chkApproved.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "'," & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),OUTPUT_QTY=" & Val(txtOutPutQty.Text) & " " & vbCrLf & " WHERE Mkey ='" & MainClass.AllowSingleQuote(lblMKey.Text) & "'"

        End If

        PubDBCn.Execute(SqlStr)

        If UpdateDetail1() = False Then GoTo ErrPart
        If UpdateToolDetail1() = False Then GoTo ErrPart
        If UpdateOthersDetail1() = False Then GoTo ErrPart
        If UpdateOTHDetail1((lblMKey.Text), SprdMainMWS, "MWS") = False Then GoTo ErrPart
        If UpdateOTHDetail1((lblMKey.Text), SprdMainPLT, "PLT") = False Then GoTo ErrPart
        If UpdateOTHDetail1((lblMKey.Text), SprdMainPPS, "PPS") = False Then GoTo ErrPart
        If UpdateOTHDetail1((lblMKey.Text), SprdMainPC, "NPC") = False Then GoTo ErrPart

        If UpdateBOMAlterDetail = False Then GoTo ErrPart
        If UpdateItemConsumption(Trim(txtProductCode.Text)) = False Then GoTo ErrPart

        If mSeqAddMode = True Then
            SqlStr = " INSERT INTO PRD_PRODSEQUENCE_HDR " & vbCrLf & " (PRODUCT_CODE,COMPANY_CODE,WEF," & vbCrLf & " ADDUSER, ADDDATE, MODUSER, MODDATE) " & vbCrLf & " VALUES ( " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtProductCode.Text) & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", TO_DATE('" & vb6.Format(txtWEF.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','')"
        Else
            SqlStr = " UPDATE PRD_PRODSEQUENCE_HDR SET " & vbCrLf & " PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "', " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE ='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "' " & vbCrLf & " AND WEF=TO_DATE('" & vb6.Format(txtWEF.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY')"
        End If

        PubDBCn.Execute(SqlStr)

        If UpdateSeqDetail = False Then GoTo ErrPart
        If UpdateOPRNDetail = False Then GoTo ErrPart



        If UpdateRelDetail1() = False Then GoTo ErrPart

        If Val(txtAmendNo.Text) > 0 Then
            If UpdatePreviousCost((txtProductCode.Text), Val(txtAmendNo.Text), "C") = False Then GoTo ErrPart
        End If

        Update1 = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsBOMMain.Requery()
        RsBOMDetail.Requery()
        If Err.Description = "" Then Exit Function
        MsgBox(Err.Description)
        '    Resume
    End Function
    Private Function UpdateRelDetail1() As Boolean

        On Error GoTo UpdateRelDetail1
        Dim SqlStr As String = ""
        Dim i As Integer
        Dim mItemCode As String
        Dim mUOM As String
        Dim mRemarks As String
        'Dim mRelAddMode As Boolean

        '    If MainClass.ValidateWithMasterTable(Trim(txtProductCode.Text), "ITEM_CODE", "ITEM_CODE", "INV_ITEM_RELATIONSHIP_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
        '        mRelAddMode = False
        '    Else
        '        mRelAddMode = True
        '    End If

        SqlStr = " Delete From INV_ITEM_RELATIONSHIP_DET " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
            & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "'"

        PubDBCn.Execute(SqlStr)

        SqlStr = "DELETE FROM INV_ITEM_RELATIONSHIP_HDR WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "'"
        PubDBCn.Execute(SqlStr)

        If SprdMainRel.MaxRows > 1 Then
            SqlStr = "INSERT INTO INV_ITEM_RELATIONSHIP_HDR (" & vbCrLf _
                    & " COMPANY_CODE, ITEM_CODE, ITEM_UOM, " & vbCrLf _
                    & " ADDUSER,ADDDATE,MODUSER,MODDATE)" & vbCrLf _
                    & " VALUES( " & vbCrLf _
                    & " " & RsCompany.Fields("Company_Code").Value & "," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtProductCode.Text) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtUnit.Text) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"


            PubDBCn.Execute(SqlStr)
        End If

        With SprdMainRel
            For i = 1 To .MaxRows - 1
                .Row = i

                .Col = ColRelItemCode
                mItemCode = Trim(MainClass.AllowSingleQuote(.Text))

                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mUOM = MasterNo
                End If
                '            .Col = ColUOM
                '            mUOM = MainClass.AllowSingleQuote(.Text)
                '
                .Col = ColRelRemarks
                mRemarks = MainClass.AllowSingleQuote(.Text)

                SqlStr = ""

                If mItemCode <> "" Then
                    SqlStr = " INSERT INTO INV_ITEM_RELATIONSHIP_DET ( COMPANY_CODE, " & vbCrLf & " ITEM_CODE,SERIAL_NO,REF_ITEM_CODE,ITEM_UOM," & vbCrLf & " REMARKS) "
                    SqlStr = SqlStr & vbCrLf & " VALUES (" & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtProductCode.Text) & "', " & i & "," & vbCrLf & " '" & MainClass.AllowSingleQuote(mItemCode) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mUOM) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mRemarks) & "') "

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        UpdateRelDetail1 = True
        Exit Function
UpdateRelDetail1:
        UpdateRelDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function
    Private Function UpdateSeqDetail() As Boolean

        On Error GoTo UpdateDetailERR
        Dim SqlStr As String = ""
        Dim i As Integer
        Dim mDept As String
        Dim mMinQty As Double
        Dim mMaxQty As Double

        PubDBCn.Execute("DELETE FROM PRD_OPR_TRN WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "' AND WEF=TO_DATE('" & vb6.Format(txtWEF.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY')")
        PubDBCn.Execute("DELETE FROM PRD_PRODSEQUENCE_DET WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "' AND WEF=TO_DATE('" & vb6.Format(txtWEF.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY')")

        With SprdSeq
            For i = 1 To .MaxRows
                .Row = i

                SprdSeq.Col = ColDept
                mDept = MainClass.AllowSingleQuote(.Text)

                SprdSeq.Col = ColMinQty
                mMinQty = Val(.Text)

                SprdSeq.Col = ColMaxQty
                mMaxQty = Val(.Text)

                SqlStr = ""

                If Trim(mDept) <> "" Then
                    SqlStr = " INSERT INTO  PRD_PRODSEQUENCE_DET ( " & vbCrLf & " COMPANY_CODE,PRODUCT_CODE,WEF,SERIAL_NO,DEPT_CODE,MIN_QTY,MAX_QTY) " & vbCrLf & " VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ",'" & MainClass.AllowSingleQuote(txtProductCode.Text) & "',TO_DATE('" & vb6.Format(txtWEF.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY')," & i & ",'" & mDept & "'," & mMinQty & "," & mMaxQty & " ) "
                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        UpdateSeqDetail = True
        Exit Function
UpdateDetailERR:
        UpdateSeqDetail = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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
        MainClass.ButtonStatus(Me, XRIGHT, RsBOMMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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

    Private Sub CalcTots(ByRef pSprd As AxFPSpreadADO.AxfpSpread)
        On Error GoTo ErrPart
        Dim i As Integer
        Dim mStdQtyOTH As Double
        Dim mStdConsOTH As Double
        Dim mNetConsumptionOTH As Double
        Dim mStdArea As Double
        Dim mOPRType As String


        With pSprd
            For i = 1 To .MaxRows
                .Row = i

                .Col = ColItemCodeOTH
                If Trim(.Text) <> "" Then
                    .Col = ColOprOTH
                    mOPRType = VB.Left(.Text, 2)
                    mStdArea = 0

                    If UCase(pSprd.Name) = UCase("SprdMainPLT") Then
                        mStdArea = Val(txtSA.Text)
                    ElseIf UCase(pSprd.Name) = UCase("SprdMainPPS") Then
                        If mOPRType = "21" Then
                            mStdArea = Val(txtSAPS_E.Text)
                        ElseIf mOPRType = "22" Then
                            mStdArea = Val(txtSAPS_I.Text)
                        End If
                    ElseIf UCase(pSprd.Name) = UCase("SprdMainPC") Then
                        mStdArea = Val(txtSAPC.Text)
                    ElseIf UCase(pSprd.Name) = UCase("SprdMainMWS") Then
                        If mOPRType = "11" Then
                            mStdArea = Val(txtWL.Text)
                        End If
                    End If

                    .Col = colStdQtyOTH
                    If Val(.Text) = 0 Then
                        .Text = VB6.Format(mStdArea, "0.0000")
                    End If
                    mStdQtyOTH = Val(.Text)

                    .Col = colStdConsOTH
                    mStdConsOTH = Val(.Text)

                    mNetConsumptionOTH = CDbl(VB6.Format(mStdQtyOTH * mStdConsOTH, "0.0000"))

                    .Col = ColNetConsumptionOTH
                    .Text = CStr(Val(CStr(mNetConsumptionOTH)))
                End If
            Next
        End With
        Exit Sub
ErrPart:
        '    Resume
        MsgBox(Err.Description)
    End Sub
    Private Function CheckUnit(ByRef pSprd As Object, ByVal Col As Integer, ByVal Row As Integer) As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE = 'U' "
        With pSprd
            .Row = Row
            .Col = Col
            If MainClass.ValidateWithMasterTable(.Text, "GEN_CODE", "GEN_DESC", "INV_GENERAL_MST", PubDBCn, MasterNo, , SqlStr) = True Then
                CheckUnit = True
            Else
                MainClass.SetFocusToCell(pSprd, Row, Col)
            End If
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Sub SprdMain_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdMain.ButtonClicked
        Call ShowFormBOMAlterDetail(eventArgs.Col, eventArgs.Row)
    End Sub

    Private Sub ShowFormBOMAlterDetail(ByRef pCol As Integer, ByRef pRow As Integer)

        Dim i As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim pDate As String
        Dim mItemCode As String
        Dim mDeptCode As String

        With SprdMain
            .Row = pRow

            .Col = ColDeptCode
            mDeptCode = Trim(.Text)
            .Col = ColRMCode
            mItemCode = Trim(.Text)

        End With
        If mItemCode = "" Then Exit Sub

        ConBOMDetail = False

        With FrmBOMAlternate
            .LblAddMode.Text = CStr(ADDMode)
            .LblModifyMode.Text = CStr(MODIFYMode)
            .lblMKey.Text = lblMKey.Text
            .lblMainItemCode.Text = mItemCode
            .lblDeptCode.Text = mDeptCode
            .LblMainItemSNO.Text = CStr(pRow)
            .ShowDialog()
        End With

        If ConBOMDetail = True Then
            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If

    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick

        SprdView.Row = SprdView.ActiveRow

        SprdView.Col = 3
        txtProductCode.Text = Trim(SprdView.Text)

        SprdView.Col = 5
        txtWEF.Text = VB6.Format(SprdView.Text, "DD/MM/YYYY")

        txtProductCode_Validating(txtProductCode, New System.ComponentModel.CancelEventArgs(False))
        ViewGrid()
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        With SprdMain
            SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim SqlStr As String = ""
        Dim mRMName As String
        Dim mDeleted As Boolean
        Dim mRMCode As String
        Dim mDeptCode As String
        Dim Response As Integer

        If eventArgs.Row = 0 And eventArgs.Col = ColDeptCode Then
            With SprdMain
                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value
                If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColDeptCode
                    .Text = AcName1
                End If
            End With
        End If

        If eventArgs.Row = 0 And eventArgs.Col = ColRMCode Then
            With SprdMain
                SqlStr = "SELECT ITEM_CODE,ITEM_SHORT_DESC " & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS = 'A' " & vbCrLf & " ORDER BY ITEM_CODE "
                .Row = .ActiveRow
                .Col = ColRMCode
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColRMCode
                    .Text = AcName

                    .Col = ColRMDesc
                    .Text = AcName1

                    .Col = ColRMCode
                    Call FillGridRow(SprdMain.Text)
                End If
            End With
        End If

        If eventArgs.Row = 0 And eventArgs.Col = ColRMDesc Then
            With SprdMain
                SqlStr = "SELECT ITEM_SHORT_DESC,ITEM_CODE " & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS = 'A' " & vbCrLf & " ORDER BY ITEM_SHORT_DESC "

                .Row = .ActiveRow

                .Col = ColRMDesc
                mRMName = .Text

                .Text = ""
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColRMDesc
                    .Text = AcName

                    .Col = ColRMCode
                    .Text = AcName1
                Else
                    .Row = .ActiveRow

                    .Col = ColRMDesc
                    .Text = mRMName
                End If
                .Col = ColRMCode
                Call FillGridRow((SprdMain.Text))
            End With
        End If

        If eventArgs.Row = 0 And eventArgs.Col = ColStockType Then
            With SprdMain
                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value
                If MainClass.SearchGridMaster("", "INV_TYPE_MST", "STOCK_TYPE_DESC", "STOCK_TYPE_CODE", , , SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColStockType
                    .Text = AcName1
                End If
            End With
        End If

        If eventArgs.Row = 0 And eventArgs.Col = ColOperation Then
            With SprdMain
                .Row = .ActiveRow

                .Col = ColDeptCode
                mDeptCode = Trim(.Text)

                .Col = ColOperation

                SqlStr = " SELECT MST.OPR_DESC, TRN.OPR_CODE, TO_CHAR(OPR_SNO,'000') AS OPR_SNO, TRN.CYCLE_TIME " & vbCrLf _
                    & " FROM TEMP_PRD_OPR_TRN TRN, PRD_OPR_MST MST" & vbCrLf _
                    & " WHERE " & vbCrLf _
                    & " TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND TRN.COMPANY_CODE=MST.COMPANY_CODE " & vbCrLf _
                    & " AND TRN.OPR_CODE=MST.OPR_CODE " & vbCrLf _
                    & " AND TRN.DEPT_CODE='" & MainClass.AllowSingleQuote(mDeptCode) & "'" & vbCrLf _
                    & " AND TRN.PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "' AND WEF=TO_DATE('" & VB6.Format(lblOldWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND ISOPTIONAL='N'"


                SqlStr = SqlStr & vbCrLf & " AND UserId='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"

                If Trim(.Text) <> "" Then
                    SqlStr = SqlStr & vbCrLf & " AND MST.OPR_DESC LIKE ('" & MainClass.AllowSingleQuote(.Text) & "%')"
                End If

                SqlStr = SqlStr & vbCrLf & " ORDER BY OPR_SNO"

                If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColOperation
                    .Text = Trim(AcName)
                End If
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColOperation, .ActiveRow, ColOperation, .ActiveRow, False))
            End With
        End If

        With SprdMain
            If eventArgs.Col = 0 And eventArgs.Row > 0 And (ADDMode = True Or MODIFYMode = True) Then
                Response = CInt(MsgQuestion("Click 'Yes' for Insert  And 'No' for Delete. "))
                If Response = MsgBoxResult.Yes Then
                    .Row = eventArgs.Row
                    .Action = SS_ACTION_INSERT_ROW
                    If .MaxRows >= 1 Then .MaxRows = .MaxRows + 1
                    MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
                Else
                    Response = CInt(MsgQuestion("Are you sure to Delete this Row ? "))
                    If Response = MsgBoxResult.Yes Then
                        .Row = eventArgs.Row
                        .Col = ColRMCode
                        mRMCode = Trim(.Text)
                        .Col = ColDeptCode
                        mDeptCode = Trim(.Text)
                        If CheckAlterItem(mRMCode, mDeptCode) = True Then
                            MsgInformation("Alternate Code is Defined for this Item, So please first Delete Alternate Code.")
                            Exit Sub
                        Else
                            .Row = eventArgs.Row
                            .Action = SS_ACTION_DELETE_ROW
                            If .MaxRows > 1 Then .MaxRows = .MaxRows - 1
                        End If
                        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
                    End If
                End If
            End If
        End With
    End Sub

    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColDeptCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColDeptCode, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColRMCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColRMCode, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColRMDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColRMDesc, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColStockType Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColStockType, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColOperation Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColOperation, 0))
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim mDeptCode As String
        Dim mRMCode As String
        Dim mStockType As String

        If eventArgs.NewRow = -1 Then Exit Sub
        SprdMain.Row = eventArgs.row
        SprdMain.Col = ColRMCode
        If Trim(SprdMain.Text) = "" Then Exit Sub
        Select Case eventArgs.col
            Case ColDeptCode
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColDeptCode
                mDeptCode = Trim(SprdMain.Text)
                SprdMain.Col = ColRMCode
                mRMCode = Trim(SprdMain.Text)
                If mRMCode <> "" Then
                    If CheckDuplicateItem(SprdMain, mRMCode, mDeptCode) = True Then
                        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColDeptCode)
                    End If
                End If
            Case ColRMCode
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColDeptCode
                mDeptCode = Trim(SprdMain.Text)
                SprdMain.Col = ColRMCode
                mRMCode = Trim(SprdMain.Text)
                If Trim(txtProductCode.Text) = Trim(SprdMain.Text) Then
                    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColRMCode)
                Else
                    If ValidateMainCode(mRMCode) = True Then
                        If CheckDuplicateItem(SprdMain, mRMCode, mDeptCode) = False Then
                            SprdMain.Row = SprdMain.ActiveRow
                            SprdMain.Col = ColRMCode
                            Call FillGridRow((SprdMain.Text))
                        Else
                            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColRMCode)
                        End If
                    Else
                        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColRMCode)
                    End If
                End If
            Case colStdQty
                If CheckQty(SprdMain, eventArgs.col, eventArgs.row) = True Then
                    MainClass.AddBlankSprdRow(SprdMain, ColRMCode, ConRowHeight)
                    FormatSprdMain((SprdMain.MaxRows))
                End If
            Case ColQUnit
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColQUnit
                If Trim(SprdMain.Text) <> "" Then Call CheckUnit(SprdMain, ColQUnit, SprdMain.ActiveRow)
            Case ColStockType
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColStockType
                mStockType = Trim(SprdMain.Text)
                If mStockType <> "" Then
                    If MainClass.ValidateWithMasterTable(mStockType, "STOCK_TYPE_CODE", "STOCK_TYPE_CODE", "INV_TYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColStockType)
                    End If
                End If
            Case ColOperation
                Call CheckOPR()
        End Select

        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Sub CheckOPR()

        On Error GoTo ChkERR
        Dim mOPR As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mDeptCode As String

        With SprdMain
            .Row = .ActiveRow

            .Col = ColDeptCode
            mDeptCode = Trim(.Text)

            .Col = ColOperation
            If Trim(.Text) = "" Then Exit Sub

            '        SqlStr = " SELECT TRN.OPR_CODE " & vbCrLf _
            ''                & " FROM PRD_OPR_TRN TRN, PRD_OPR_MST MST" & vbCrLf _
            ''                & " WHERE " & vbCrLf _
            ''                & " TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
            ''                & " AND TRN.COMPANY_CODE=MST.COMPANY_CODE " & vbCrLf _
            ''                & " AND TRN.OPR_CODE=MST.OPR_CODE " & vbCrLf _
            ''                & " AND TRN.DEPT_CODE='" & MainClass.AllowSingleQuote(mDeptCode) & "'" & vbCrLf _
            ''                & " AND MST.OPR_DESC='" & MainClass.AllowSingleQuote(.Text) & "'" & vbCrLf _
            ''                & " AND TRN.PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "'"

            SqlStr = " SELECT TRN.OPR_CODE " & vbCrLf _
                & " FROM TEMP_PRD_OPR_TRN TRN, PRD_OPR_MST MST" & vbCrLf _
                & " WHERE " & vbCrLf _
                & " TRN.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
                & " AND TRN.COMPANY_CODE=MST.COMPANY_CODE " & vbCrLf _
                & " AND TRN.OPR_CODE=MST.OPR_CODE " & vbCrLf _
                & " AND TRN.DEPT_CODE='" & MainClass.AllowSingleQuote(mDeptCode) & "'" & vbCrLf _
                & " AND MST.OPR_DESC='" & MainClass.AllowSingleQuote(.Text) & "'" & vbCrLf _
                & " AND TRN.PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "' AND WEF=TO_DATE('" & vb6.Format(lblOldWEF.text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND ISOPTIONAL='N'"


            SqlStr = SqlStr & vbCrLf & " AND UserId='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"

            If mDeptCode <> "" Then
                SqlStr = SqlStr & " AND TRN.DEPT_CODE='" & MainClass.AllowSingleQuote(UCase(mDeptCode)) & "' "
            End If

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = True Then
                MsgInformation("Invalid Operation for such Dept.")
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColOperation)
                Exit Sub
            End If

        End With
        Exit Sub
ChkERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        '    With SprdMain
        '        SprdMain_LeaveCell .ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False
        '    End With
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

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtApprovedBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtApprovedBy.DoubleClick
        Call cmdSearchAppBy_Click(cmdSearchAppBy, New System.EventArgs())
    End Sub

    Private Sub txtApprovedBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtApprovedBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchAppBy_Click(cmdSearchAppBy, New System.EventArgs())
    End Sub

    Private Sub txtApprovedBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtApprovedBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim SqlStr As String = ""
        Dim pSqlStr As String

        If Trim(txtApprovedBy.Text) = "" Then GoTo EventExitSub

        txtApprovedBy.Text = VB6.Format(Trim(txtApprovedBy.Text), "000000")

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        If ADDMode = True Then
            SqlStr = SqlStr & " AND STATUS='O' "
        End If

        If MainClass.ValidateWithMasterTable(txtApprovedBy.Text, "USER_ID", "EMP_NAME", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            SqlStr = "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " "
            If MainClass.ValidateWithMasterTable(txtApprovedBy.Text, "USER_CODE", "USER_ID", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , SqlStr) = False Then
                MsgBox("EMPLOYEE Does Not Exist In Master.")
                Cancel = True
            Else
                lblApprovedBy.text = MasterNo
            End If
        Else
            lblApprovedBy.text = MasterNo
        End If


        '    txtApprovedBy.Text = Format(Trim(txtApprovedBy.Text), "000000")
        '
        '    SqlStr = "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " "
        '    If ADDMode = True Then
        '        SqlStr = SqlStr & " AND EMP_LEAVE_DATE IS NULL "
        '    End If
        '
        '    If MainClass.ValidateWithMasterTable(txtApprovedBy.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
        '        MsgBox "EMPLOYEE Does Not Exist In Master."
        '        Cancel = True
        '    Else
        '        lblApprovedBy.text = MasterNo
        '    End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCopyProductCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCopyProductCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
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
        Dim SqlStr As String = ""
        Dim mProcessType As Integer

        If Trim(txtCopyProductCode.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtCopyProductCode.Text, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtCopyProductDesc.Text = MasterNo

            SqlStr = " SELECT * FROM PRD_NEWBOM_HDR " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
                & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtCopyProductCode.Text) & "' " & vbCrLf _
                & " AND BOM_TYPE='" & lblType.text & "'"

            If Trim(txtCopyAmendNo.Text) <> "" Then
                SqlStr = SqlStr & vbCrLf & " AND AMEND_NO='" & Val(txtCopyAmendNo.Text) & "'"
            Else
                SqlStr = SqlStr & vbCrLf & " AND AMEND_NO = (" & vbCrLf & " SELECT MAX(AMEND_NO) AS AMEND_NO " & vbCrLf & " FROM PRD_NEWBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE='" & Trim(txtCopyProductCode.Text) & "'" & vbCrLf & " AND BOM_TYPE='" & lblType.Text & "')"
            End If

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRs, ADODB.LockTypeEnum.adLockReadOnly)
            If mRs.EOF = False Then
                lblCopyMKey.Text = mRs.Fields("mKey").Value
                MainClass.ClearGrid(SprdMain)

                txtProcessCost.Text = VB6.Format(IIf(IsDbNull(mRs.Fields("PROCESS_COST").Value), 0, mRs.Fields("PROCESS_COST").Value), "0.000")
                mProcessType = IIf(IsDbNull(mRs.Fields("PROCESS_TYPE").Value), 0, mRs.Fields("PROCESS_TYPE").Value)

                txtSA.Text = VB6.Format(IIf(IsDbNull(mRs.Fields("SURFACE_AREA").Value), 0, mRs.Fields("SURFACE_AREA").Value), "0.0000")
                txtWL.Text = VB6.Format(IIf(IsDbNull(mRs.Fields("WELD_LENGTH").Value), 0, mRs.Fields("WELD_LENGTH").Value), "0.0000")
                txtSAPS_E.Text = VB6.Format(IIf(IsDbNull(mRs.Fields("EXT_PAINT_AREA").Value), 0, mRs.Fields("EXT_PAINT_AREA").Value), "0.0000")
                txtSAPS_I.Text = VB6.Format(IIf(IsDbNull(mRs.Fields("INT_PAINT_AREA").Value), 0, mRs.Fields("INT_PAINT_AREA").Value), "0.0000")
                txtSAPC.Text = VB6.Format(IIf(IsDbNull(mRs.Fields("COATING_AREA").Value), 0, mRs.Fields("COATING_AREA").Value), "0.0000")

                If mProcessType = 1 Then
                    cboProcessType.SelectedIndex = 0
                ElseIf mProcessType = 2 Then
                    cboProcessType.SelectedIndex = 1
                ElseIf mProcessType = 3 Then
                    cboProcessType.SelectedIndex = 2
                Else
                    cboProcessType.SelectedIndex = -1
                End If

                chkScrap.CheckState = IIf(mRs.Fields("FROM_SCRAP").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkBOP.CheckState = IIf(mRs.Fields("IS_BOP").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                Call ShowSeqDetail1((txtCopyProductCode.Text))
                Call ShowOperation((txtCopyProductCode.Text))
                Call ShowBOMDetail1((lblCopyMKey.Text))
                Call ShowBOMAlterDetail((lblCopyMKey.Text))

                Call ShowBOMOTHDetail1((lblCopyMKey.Text), SprdMainMWS, "MWS")
                Call ShowBOMOTHDetail1((lblCopyMKey.Text), SprdMainPLT, "PLT")
                Call ShowBOMOTHDetail1((lblCopyMKey.Text), SprdMainPPS, "PPS")
                Call ShowBOMOTHDetail1((lblCopyMKey.Text), SprdMainPC, "NPC")

            Else
                MsgBox("BOM Not defined for this Product", MsgBoxStyle.Information)
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

    Private Sub txtCustPartNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustPartNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtModelNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtModelNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOutPutQty_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOutPutQty.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOutPutQty_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOutPutQty.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtProcessCost_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProcessCost.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtProcessCost_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtProcessCost.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSA_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSA.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSA_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSA.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSAPC_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSAPC.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSAPS_E_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSAPS_E.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSAPS_I_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSAPS_I.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtUnit_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtUnit.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtProductCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProductCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
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

    Public Sub txtProductCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtProductCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim mRs As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim xMkey As String = ""
        If Trim(txtProductCode.Text) = "" Then GoTo EventExitSub

        SqlStr = " SELECT ITEM_SHORT_DESC,ISSUE_UOM,ITEM_MODEL,CUSTOMER_PART_NO " & " FROM INV_ITEM_MST " & " WHERE ITEM_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "'" & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS = 'A' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRs, ADODB.LockTypeEnum.adLockReadOnly)
        If Not mRs.EOF Then
            txtProductDesc.Text = IIf(IsDBNull(mRs.Fields("ITEM_SHORT_DESC").Value), "", mRs.Fields("ITEM_SHORT_DESC").Value)
            txtUnit.Text = IIf(IsDBNull(mRs.Fields("ISSUE_UOM").Value), "", mRs.Fields("ISSUE_UOM").Value)
            txtModelNo.Text = IIf(IsDBNull(mRs.Fields("ITEM_MODEL").Value), "", mRs.Fields("ITEM_MODEL").Value)
            txtCustPartNo.Text = IIf(IsDBNull(mRs.Fields("CUSTOMER_PART_NO").Value), "", mRs.Fields("CUSTOMER_PART_NO").Value)


            'If Trim(txtUnit.Text) = "KGS" Or Trim(txtUnit.Text) = "TON" Or Trim(txtUnit.Text) = "MT" Then
            '    txtOutPutQty.Enabled = True
            'Else
            '    txtOutPutQty.Enabled = False
            'End If

        Else
            txtProductDesc.Text = ""
            txtUnit.Text = ""
            txtModelNo.Text = ""
            txtCustPartNo.Text = ""
            txtOutPutQty.Enabled = False
            MsgBox("Invaild Item Code.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

        If ShowRecord() = False Then Cancel = True
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
        '    Resume
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPreparedBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPreparedBy.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
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
        Dim SqlStr As String = ""

        If Trim(txtPreparedBy.Text) = "" Then GoTo EventExitSub

        '    txtApprovedBy.Text = Format(Trim(txtApprovedBy.Text), "000000")

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        If ADDMode = True Then
            SqlStr = SqlStr & " AND STATUS='O' "
        End If

        If MainClass.ValidateWithMasterTable(txtPreparedBy.Text, "USER_ID", "EMP_NAME", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            SqlStr = "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " "
            If MainClass.ValidateWithMasterTable(txtPreparedBy.Text, "USER_CODE", "USER_ID", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , SqlStr) = False Then
                MsgBox("EMPLOYEE Does Not Exist In Master.")
                Cancel = True
            Else
                lblPreparedBy.text = MasterNo
            End If
        Else
            lblPreparedBy.text = MasterNo
        End If



        '    If Trim(txtPreparedBy.Text) = "" Then Exit Sub
        '    txtPreparedBy.Text = Format(Trim(txtPreparedBy.Text), "000000")
        '    SqlStr = "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & ""
        '
        '    If ADDMode = True Then
        '        SqlStr = SqlStr & " AND EMP_LEAVE_DATE IS NULL "
        '    End If
        '
        '    If MainClass.ValidateWithMasterTable(txtPreparedBy, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
        '        MsgBox "Employee Does Not Exist In Master."
        '        Cancel = True
        '    Else
        '        lblPreparedBy.text = MasterNo
        '    End If
        GoTo EventExitSub
ValEMP:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtRemarks.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtUnit_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtUnit.DoubleClick
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE = 'U' "
        If MainClass.SearchGridMaster(txtUnit.Text, "INV_GENERAL_MST", "GEN_CODE", "GEN_DESC", , , SqlStr) = True Then
            txtUnit.Text = AcName
        End If
    End Sub

    Private Sub txtUnit_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtUnit.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call txtUnit_DoubleClick(txtUnit, New System.EventArgs())
    End Sub

    Private Sub txtUnit_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtUnit.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        If Trim(txtUnit.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE = 'U' "
        If MainClass.ValidateWithMasterTable(txtUnit.Text, "GEN_CODE", "GEN_DESC", "INV_GENERAL_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Unit does not exist in Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtWEF_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWEF.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtWEF_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtWEF.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchWEF_Click(cmdSearchWEF, New System.EventArgs())
    End Sub

    Private Sub txtWEF_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtWEF.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If ShowRecord = False Then Cancel = True

        If ADDMode = True And Val(txtAmendNo.Text) = 0 Then
            lblOldWEF.Text = VB6.Format(txtWEF.Text, "DD/MM/YYYY")
        End If
        '    Call ShowRecord
        eventArgs.Cancel = Cancel
    End Sub

    Private Function ShowRecord() As Boolean

        On Error GoTo ERR1
        Dim mRs As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim xMkey As String = ""

        ShowRecord = True

        If Trim(txtProductCode.Text) = "" Then Exit Function

        If Trim(txtWEF.Text) = "" Then
            SqlStr = " SELECT MAX(WEF) AS WEF " & vbCrLf _
                & " FROM PRD_NEWBOM_HDR " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND PRODUCT_CODE='" & Trim(txtProductCode.Text) & "'" & vbCrLf & " AND BOM_TYPE='" & lblType.Text & "'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBOMMain, ADODB.LockTypeEnum.adLockReadOnly)
            If RsBOMMain.EOF = True Then
                Exit Function
            End If
        End If

        If MODIFYMode = True And RsBOMMain.EOF = False Then xMkey = RsBOMMain.Fields("mKey").Value
        SqlStr = " SELECT * FROM PRD_NEWBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "' " & vbCrLf & " AND BOM_TYPE='" & lblType.Text & "'"

        If Trim(txtWEF.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND WEF=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND WEF = (" & vbCrLf & " SELECT MAX(WEF) AS WEF " & vbCrLf & " FROM PRD_NEWBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE='" & Trim(txtProductCode.Text) & "'" & vbCrLf & " AND BOM_TYPE='" & lblType.Text & "')"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBOMMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsBOMMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("BOM Not Made For This Item. Click Add For New.", MsgBoxStyle.Information)
                ShowRecord = False
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM PRD_NEWBOM_HDR" & vbCrLf & " WHERE UPPER(LTRIM(RTRIM(MKey)))='" & MainClass.AllowSingleQuote(UCase(xMkey)) & "' " & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBOMMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Sub ShowBOMAlterDetail(ByRef nMkey As String)

        On Error GoTo ShowSerialNoErr
        Dim RsSRLNo As ADODB.Recordset
        Dim SqlStr As String = ""

        Call DelTemp_BOMAlterDetail()

        SqlStr = ""

        SqlStr = "INSERT INTO TEMP_PRD_BOM_ALTER_DET ( " & vbCrLf & " USERID, COMPANY_CODE, DEPT_CODE, " & vbCrLf & " MAINITEM_CODE, MAINSUBROWNO, SUBROWNO, " & vbCrLf & " ALTER_RM_CODE, ALTER_STD_QTY, ALETRSCRAP, ALETR_WT_VAR,ALTER_STOCK_TYPE) " & vbCrLf & " SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "', COMPANY_CODE, DEPT_CODE, " & vbCrLf & " MAINITEM_CODE, MAINSUBROWNO, SUBROWNO, " & vbCrLf & " ALTER_RM_CODE, ALTER_STD_QTY, ALETRSCRAP, ALETR_WT_VAR,ALTER_STOCK_TYPE " & vbCrLf & " FROM PRD_BOM_ALTER_DET" & vbCrLf & " WHERE MKEY ='" & MainClass.AllowSingleQuote(nMkey) & "'" & vbCrLf & " ORDER BY MAINSUBROWNO, SUBROWNO" & vbCrLf
        PubDBCn.Execute(SqlStr)

        Exit Sub
ShowSerialNoErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub DelTemp_BOMAlterDetail()

        Dim SqlStr As String = ""

        SqlStr = ""
        SqlStr = "DELETE FROM TEMP_PRD_BOM_ALTER_DET " & "WHERE USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"

        PubDBCn.Execute(SqlStr)
    End Sub

    Private Function UpdateBOMAlterDetail() As Boolean

        On Error GoTo UpdateErr1
        Dim RsTemp_SRLNo As ADODB.Recordset
        Dim ii As Integer
        Dim SqlStr As String = ""
        Dim mItemCode As String
        Dim mDeptCode As String

        With SprdMain
            For ii = 1 To .MaxRows - 1
                .Row = ii
                .Col = ColRMCode
                mItemCode = Trim(.Text)

                .Col = ColDeptCode
                mDeptCode = Trim(.Text)

                SqlStr = "INSERT INTO PRD_BOM_ALTER_DET ( " & vbCrLf & " MKEY, COMPANY_CODE, DEPT_CODE, " & vbCrLf & " MAINITEM_CODE, MAINSUBROWNO, SUBROWNO, " & vbCrLf & " ALTER_RM_CODE, ALTER_STD_QTY, ALETRSCRAP, ALETR_WT_VAR,ALTER_STOCK_TYPE) " & vbCrLf & " SELECT '" & MainClass.AllowSingleQuote(lblMKey.Text) & "', COMPANY_CODE, '" & mDeptCode & "', " & vbCrLf & " MAINITEM_CODE, " & ii & ", SUBROWNO, " & vbCrLf & " ALTER_RM_CODE, ALTER_STD_QTY, ALETRSCRAP, ALETR_WT_VAR,ALTER_STOCK_TYPE " & vbCrLf & " FROM TEMP_PRD_BOM_ALTER_DET" & vbCrLf & " WHERE USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf & " AND MAINITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'" ''& vbCrLf |                    & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(mDeptCode) & "'"

                PubDBCn.Execute(SqlStr)
            Next
        End With

        UpdateBOMAlterDetail = True
        Exit Function
UpdateErr1:
        UpdateBOMAlterDetail = False
        If Err.Description <> "" Then
            MsgBox(Err.Description)
        End If
    End Function

    Private Function CheckAlterItem(ByRef pRMCode As String, ByRef pDeptCode As String) As Boolean

        On Error GoTo UpdateErr1
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        CheckAlterItem = False

        SqlStr = "SELECT MAINITEM_CODE, ALTER_RM_CODE " & vbCrLf & " FROM TEMP_PRD_BOM_ALTER_DET" & vbCrLf & " WHERE USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf & " AND MAINITEM_CODE='" & MainClass.AllowSingleQuote(pRMCode) & "'" & vbCrLf & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(pDeptCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            CheckAlterItem = True
        End If
        Exit Function
UpdateErr1:
        CheckAlterItem = False
        If Err.Description <> "" Then
            MsgBox(Err.Description)
        End If
    End Function
    Private Function CheckWEFDate(ByRef pWEFDate As String) As Boolean

        On Error GoTo ErrorPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCheckWEFDate As String

        CheckWEFDate = True

        SqlStr = " SELECT MAX(WEF) AS WEF" & vbCrLf _
                & " FROM PRD_NEWBOM_HDR" & vbCrLf _
                & " WHERE " & vbCrLf _
                & " COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
                & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "'" & vbCrLf _
                & " AND AMEND_NO< " & Val(txtAmendNo.Text) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mCheckWEFDate = IIf(IsDbNull(RsTemp.Fields("WEF").Value), "", RsTemp.Fields("WEF").Value)
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
    Private Function CheckLastOpenBOM(ByRef pWEFDate As String) As Boolean

        On Error GoTo ErrorPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCheckWEFDate As String
        Dim mMKEY As String

        CheckLastOpenBOM = False
        mMKEY = lblMKey.Text

        SqlStr = " SELECT PRODUCT_CODE" & vbCrLf _
                & " FROM PRD_NEWBOM_HDR" & vbCrLf _
                & " WHERE " & vbCrLf _
                & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "'" & vbCrLf _
                & " AND STATUS='O' AND BOM_TYPE<>'" & lblType.text & "'"


        If mMKEY <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND MKEY<> '" & mMKEY & "'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            CheckLastOpenBOM = True
        End If

        Exit Function
ErrorPart:
        CheckLastOpenBOM = False
    End Function
    Private Function CheckDuplicateDept(ByRef pDept As String) As Boolean

        On Error GoTo ERR1
        Dim i As Integer
        Dim mItemRept As Integer

        If pDept = "" Then CheckDuplicateDept = True : Exit Function
        With SprdSeq
            For i = 1 To .MaxRows
                .Row = i
                .Col = ColDept
                If UCase(Trim(.Text)) = UCase(Trim(pDept)) Then
                    mItemRept = mItemRept + 1
                    If mItemRept > 1 Then
                        CheckDuplicateDept = True
                        MsgInformation("Duplicate Deptt")
                        MainClass.SetFocusToCell(SprdSeq, .ActiveRow, ColDept)
                        Exit Function
                    End If
                End If
            Next
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub FormatSprdSeq(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim SqlStr As String = ""


        With SprdSeq
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = ColDept
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = RsProdSeqDetail.Fields("DEPT_CODE").DefinedSize
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 6)

            .Col = ColDeptDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 16)

            .Col = ColOPRN
            .CellType = SS_CELL_TYPE_BUTTON
            '.Lock = False
            .TypeButtonText = "Details"
            .TypeButtonAlign = SS_CELL_BUTTON_ALIGN_LEFT
            .set_ColWidth(ColOPRN, 7)

            .Col = ColMinQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 3
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .set_ColWidth(.Col, 7)
            .ColHidden = False

            .Col = ColMaxQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 3
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .set_ColWidth(.Col, 10)
            .ColHidden = True

            MainClass.ProtectCell(SprdSeq, 1, .MaxRows, ColDeptDesc, ColDeptDesc)
            MainClass.SetSpreadColor(SprdSeq, Arow)
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub
    Private Function CheckProductionDeptMissing(ByRef pBOMDept As Object) As Boolean

        On Error GoTo err_Renamed
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntRow As Integer
        Dim mFound As Boolean
        CheckProductionDeptMissing = False
        mFound = False

        SqlStr = " SELECT DISTINCT ID.DEPT_CODE " & vbCrLf & " FROM PRD_NEWBOM_HDR IH,PRD_NEWBOM_DET ID" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        SqlStr = SqlStr & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "' " & vbCrLf & " AND IH.STATUS='O'"

        SqlStr = SqlStr & vbCrLf & " ORDER BY ID.DEPT_CODE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                pBOMDept = IIf(IsDbNull(RsTemp.Fields("DEPT_CODE").Value), "", RsTemp.Fields("DEPT_CODE").Value)
                mFound = False
                For cntRow = 1 To SprdSeq.MaxRows
                    SprdSeq.Row = cntRow
                    SprdSeq.Col = ColDept
                    If Trim(SprdSeq.Text) <> "" Then
                        If Trim(pBOMDept) = Trim(SprdSeq.Text) Then
                            mFound = True
                        End If
                    End If
                Next
                If mFound = False Then
                    CheckProductionDeptMissing = True
                    Exit Function
                End If
                RsTemp.MoveNext()
            Loop
        Else
            CheckProductionDeptMissing = True
            Exit Function
        End If
        Exit Function
err_Renamed:
        CheckProductionDeptMissing = True
        MsgBox(Err.Description)
    End Function
    Private Sub SprdSeq_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdSeq.ButtonClicked
        Call ShowFormOPRNDetail(eventArgs.Col, eventArgs.Row)
    End Sub
    Private Sub ShowFormOPRNDetail(ByRef pCol As Integer, ByRef pRow As Integer)

        Dim i As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mDeptCode As String
        Dim mWef As String

        With SprdSeq
            .Row = pRow

            .Col = ColDept
            mDeptCode = .Text
        End With
        If mDeptCode = "" Then Exit Sub
        If Trim(txtWEF.Text) = "" Then Exit Sub

        Me.lblDetail.Text = "False"


        With FrmOPRDailyDetail
            .LblAddMode.Text = CStr(ADDMode)
            .LblModifyMode.Text = CStr(MODIFYMode)
            .lblProductCode.Text = txtProductCode.Text
            .lblDeptCode.Text = mDeptCode
            .lblWEF.Text = VB6.Format(lblOldWEF.Text, "DD/MM/YYYY")
            .ShowDialog()
        End With

        If Me.lblDetail.Text = "True" Then
            MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
            FrmOPRDailyDetail.Close()
        End If


    End Sub
    Private Sub SprdSeq_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdSeq.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdSeq_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdSeq.ClickEvent

        Dim SqlStr As String = ""

        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If eventArgs.Row = 0 And eventArgs.Col = ColDept Then
            With SprdSeq
                .Row = .ActiveRow
                .Col = ColDept

                If MainClass.SearchGridMaster(.Text, "PAY_DEPT_MST", "DEPT_CODE", "DEPT_DESC", "", "", SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColDept
                    .Text = Trim(AcName)

                    .Col = ColDeptDesc
                    .Text = Trim(AcName1)
                End If
                Call SprdSeq_LeaveCell(SprdSeq, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColDept, .ActiveRow, ColDeptDesc, .ActiveRow, False))
            End With
        End If

        If eventArgs.Row = 0 And eventArgs.Col = ColDeptDesc Then
            With SprdSeq
                .Row = .ActiveRow
                .Col = ColDeptDesc
                If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", "", "", SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColDept
                    .Text = Trim(AcName1)

                    .Col = ColDeptDesc
                    .Text = Trim(AcName)
                End If
                Call SprdSeq_LeaveCell(SprdSeq, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColDept, .ActiveRow, ColDeptDesc, .ActiveRow, False))
            End With
        End If

        If eventArgs.Col = 0 And eventArgs.Row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdSeq, eventArgs.Row, ColDept)
            MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub SprdSeq_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdSeq.KeyUpEvent
        Dim mCol As Short
        mCol = SprdSeq.ActiveCol
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColDept Then SprdSeq_ClickEvent(SprdSeq, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColDept, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColDeptDesc Then SprdSeq_ClickEvent(SprdSeq, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColDeptDesc, 0))
        SprdSeq.Refresh()
    End Sub

    Private Sub SprdSeq_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdSeq.LeaveCell

        On Error GoTo ErrPart
        Dim xDept As String
        If eventArgs.NewRow = -1 Then Exit Sub

        SprdSeq.Row = SprdSeq.ActiveRow
        SprdSeq.Col = ColDept
        xDept = Trim(SprdSeq.Text)
        If xDept = "" Then Exit Sub

        Select Case eventArgs.col
            Case ColDept
                SprdSeq.Row = SprdSeq.ActiveRow

                SprdSeq.Col = ColDept
                xDept = Trim(SprdSeq.Text)
                If xDept = "" Then Exit Sub
                If CheckDept() = True Then
                    If CheckDuplicateDept(xDept) = False Then
                        MainClass.AddBlankSprdRow(SprdSeq, ColDept, ConRowHeight)
                        FormatSprdSeq((SprdSeq.MaxRows))
                    End If
                End If
        End Select
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function CheckDept() As Boolean

        On Error GoTo CheckERR
        With SprdSeq
            .Row = .ActiveRow
            .Col = ColDept
            If MainClass.ValidateWithMasterTable(.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ") = True Then
                .Row = .ActiveRow
                .Col = ColDeptDesc
                .Text = CStr(MasterNo)
                CheckDept = True
            Else
                .Col = ColDeptDesc
                .Text = ""
                MainClass.SetFocusToCell(SprdSeq, .ActiveRow, ColDept)
            End If
        End With
        Exit Function
CheckERR:
        MsgBox(Err.Description)
    End Function

    Private Sub SprdSeq_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdSeq.Leave
        With SprdSeq
            SprdSeq_LeaveCell(SprdSeq, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub
    'Private Sub SprdView_DblClick(ByVal Col As Long, ByVal Row As Long)
    '    SprdView.Row = SprdView.ActiveRow
    '
    '    SprdView.Col = 1
    '    txtProductCode.Text = SprdView.Text
    '
    '    txtProductCode_Validate False
    '    Call CmdView_Click
    'End Sub
    'Private Sub SprdView_KeyPress(KeyAscii As Integer)
    '    If KeyAscii = vbKeyReturn Then SprdView_DblClick SprdView.ActiveCol, SprdView.ActiveRow
    'End Sub

    Private Sub ShowOperation(ByRef pProductCode As String)

        On Error GoTo ShowSerialNoErr
        Dim RsSRLNo As ADODB.Recordset
        Dim SqlStr As String = ""

        Call DelTemp_OPRNDetail()

        SqlStr = ""
        SqlStr = "INSERT INTO TEMP_PRD_OPR_TRN ( " & vbCrLf _
            & " USERID, COMPANY_CODE, PRODUCT_CODE, WEF, " & vbCrLf _
            & " DEPT_CODE, OPR_SNO, OPR_CODE,ISOPTIONAL, CYCLE_TIME)" & vbCrLf _
            & " SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
            & " COMPANY_CODE, '" & txtProductCode.Text & "',WEF," & vbCrLf _
            & " DEPT_CODE, OPR_SNO, OPR_CODE,ISOPTIONAL, CYCLE_TIME" & vbCrLf _
            & " FROM PRD_OPR_TRN " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "' " & vbCrLf _
            & " AND WEF=TO_DATE('" & VB6.Format(lblOldWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') ORDER BY DEPT_CODE,OPR_SNO"

        PubDBCn.Execute(SqlStr)

        Exit Sub
ShowSerialNoErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub DelTemp_OPRNDetail(Optional ByRef mDeptCode As String = "")

        Dim SqlStr As String = ""

        SqlStr = ""
        SqlStr = "DELETE FROM TEMP_PRD_OPR_TRN " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
            & " AND UserId='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' AND WEF=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        If mDeptCode <> "" Then
            SqlStr = SqlStr & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(UCase(mDeptCode)) & "' "
        End If
        PubDBCn.Execute(SqlStr)
    End Sub
    Private Function UpdateOPRNDetail() As Boolean
        On Error GoTo UpdateErr1
        Dim RsTemp_SRLNo As ADODB.Recordset
        Dim ii As Integer
        Dim SqlStr As String = ""
        Dim mDeptCode As String


        With SprdSeq
            For ii = 1 To .MaxRows - 1
                .Row = ii
                .Col = ColDept
                mDeptCode = Trim(.Text)


                SqlStr = "INSERT INTO PRD_OPR_TRN (" & vbCrLf _
                        & " COMPANY_CODE, PRODUCT_CODE, WEF, DEPT_CODE, " & vbCrLf _
                        & " OPR_SNO, OPR_CODE,ISOPTIONAL,CYCLE_TIME )" & vbCrLf _
                        & " SELECT " & vbCrLf _
                        & " COMPANY_CODE, PRODUCT_CODE, TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), DEPT_CODE, " & vbCrLf _
                        & " OPR_SNO, OPR_CODE,ISOPTIONAL,CYCLE_TIME " & vbCrLf _
                        & " FROM TEMP_PRD_OPR_TRN " & vbCrLf _
                        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf _
                        & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "'" & vbCrLf _
                        & " AND WEF=TO_DATE('" & VB6.Format(lblOldWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                        & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(mDeptCode) & "'"


                PubDBCn.Execute(SqlStr)
            Next
        End With

        UpdateOPRNDetail = True
        Exit Function
UpdateErr1:
        UpdateOPRNDetail = False
        If Err.Description <> "" Then
            MsgBox(Err.Description)
        End If
    End Function
    Public Function DeleteOPRNDetail() As Boolean
        Dim SqlStr As String = ""
        On Error GoTo DeleteOPRNDetailErr
        SqlStr = ""
        SqlStr = "DELETE FROM PRD_OPR_TRN  " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
            & " AND PRODUCT_CODE ='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "' AND WEF =TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        PubDBCn.Execute(SqlStr)
        DeleteOPRNDetail = True
        Exit Function
DeleteOPRNDetailErr:
        MsgInformation(Err.Description)
        DeleteOPRNDetail = False
    End Function
    Private Sub ShowSeqDetail1(ByRef pProductCode As String)

        On Error GoTo ERR1
        Dim i As Integer
        Dim SqlStr As String = ""
        Dim mDeptt As String


        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf _
            & " FROM PRD_PRODSEQUENCE_DET " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
            & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "'" & vbCrLf _
            & " AND WEF=TO_DATE('" & vb6.Format(txtWEF.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " ORDER BY SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProdSeqDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsProdSeqDetail
            If .EOF = True Then Exit Sub
            FormatSprdSeq(-1)
            i = 1
            Do While Not .EOF
                SprdSeq.Row = i

                SprdSeq.Col = ColDept
                SprdSeq.Text = Trim(IIf(IsDbNull(.Fields("DEPT_CODE").Value), "", .Fields("DEPT_CODE").Value))
                mDeptt = SprdSeq.Text

                SprdSeq.Col = ColDeptDesc
                If MainClass.ValidateWithMasterTable(mDeptt, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ") = True Then
                    SprdSeq.Text = MasterNo
                Else
                    SprdSeq.Text = ""
                End If

                SprdSeq.Col = ColMinQty
                SprdSeq.Text = CStr(Val(IIf(IsDbNull(.Fields("MIN_QTY").Value), "", .Fields("MIN_QTY").Value)))

                SprdSeq.Col = ColMaxQty
                SprdSeq.Text = CStr(Val(IIf(IsDbNull(.Fields("MAX_QTY").Value), "", .Fields("MAX_QTY").Value)))

                .MoveNext()
                i = i + 1
                SprdSeq.MaxRows = i
            Loop
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub FillItemDescFromItemCode(ByRef pItemCode As String)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        If Trim(pItemCode) = "" Then Exit Sub


        With SprdMainRel
            If Trim(pItemCode) = Trim(txtProductCode.Text) Then
                MsgInformation("Item Cann't be Equal to Product Code")
                MainClass.SetFocusToCell(SprdMainRel, .ActiveRow, ColRelItemCode)
                Exit Sub
            End If

            SqlStr = "SELECT ITEM_SHORT_DESC,ISSUE_UOM " & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND LTRIM(RTRIM(ITEM_CODE))='" & MainClass.AllowSingleQuote(pItemCode) & "'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                .Row = .ActiveRow
                .Col = ColRelItemDesc
                .Text = IIf(IsDbNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value)

                '            .Col = ColUOM
                '            .Text = IIf(IsNull(RsTemp!ISSUE_UOM), "", RsTemp!ISSUE_UOM)

            Else
                MsgInformation("Invaild Item Code")
                MainClass.SetFocusToCell(SprdMainRel, .ActiveRow, ColRelItemCode)
            End If
        End With
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub FillItemDescFromItemDesc(ByRef pItemDesc As String)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        If Trim(pItemDesc) = "" Then Exit Sub
        With SprdMainRel
            SqlStr = "SELECT ITEM_CODE,CUSTOMER_PART_NO,ISSUE_UOM " & vbCrLf _
            & " FROM INV_ITEM_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
            & " AND LTRIM(RTRIM(ITEM_SHORT_DESC))='" & MainClass.AllowSingleQuote(pItemDesc) & "'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                .Row = .ActiveRow
                .Col = ColRelItemCode
                .Text = IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)

                '            .Col = ColUOM
                '            .Text = IIf(IsNull(RsTemp!ISSUE_UOM), "", RsTemp!ISSUE_UOM)
            Else
                MsgInformation("Invaild Item Description")
                MainClass.SetFocusToCell(SprdMainRel, .ActiveRow, ColRelItemCode)
            End If
        End With
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub txtWL_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWL.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtWL_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtWL.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Function UpdateToolDetail1() As Boolean

        On Error GoTo UpdateDetail1
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim SqlStr As String = ""
        Dim i As Integer

        Dim mDeptCode As String
        Dim mToolCode As String
        Dim mStdQty As Double
        Dim mToolUOM As String
        Dim mToolLife As Double
        Dim mToolRemarks As String


        PubDBCn.Execute("DELETE FROM PRD_NEWBOM_TOOL_DET  " & vbCrLf & " WHERE Mkey='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")

        With SprdTool
            For i = 1 To .MaxRows - 1
                .Row = i

                .Col = ColToolCode
                mToolCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColToolQUnit
                mToolUOM = MainClass.AllowSingleQuote(.Text)

                .Col = ColToolDeptCode
                mDeptCode = MainClass.AllowSingleQuote(.Text)

                .Col = colToolStdQty
                mStdQty = Val(.Text)

                .Col = ColToolLife
                mToolLife = Val(.Text)

                .Col = ColToolRemarks
                mToolRemarks = MainClass.AllowSingleQuote(.Text)

                SqlStr = ""
                If Trim(mToolCode) <> "" And mStdQty <> 0 Then
                    SqlStr = " INSERT INTO  PRD_NEWBOM_TOOL_DET ( " & vbCrLf _
                        & " MKEY, COMPANY_CODE, " & vbCrLf _
                        & " SERIAL_NO, TOOL_CODE, TOOL_UOM, " & vbCrLf _
                        & " DEPT_CODE, TOOL_STD_QTY, " & vbCrLf _
                        & " TOOL_LIFE, TOOL_REMARKS ) VALUES ( " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(lblMKey.Text) & "', " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf _
                        & " " & i & ", '" & mToolCode & "', '" & mToolUOM & "'," & vbCrLf _
                        & " '" & mDeptCode & "',  " & vbCrLf _
                        & " " & mStdQty & ", " & mToolLife & ", '" & mToolRemarks & "')"

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        UpdateToolDetail1 = True
        Exit Function
UpdateDetail1:
        MsgBox(Err.Description)
        UpdateToolDetail1 = False
    End Function

    Private Function UpdateOthersDetail1() As Boolean

        On Error GoTo UpdateDetail1
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim SqlStr As String = ""
        Dim i As Integer

        Dim mDeptCode As String
        Dim mToolCode As String
        Dim mConsumableOnQty As Double
        Dim mToolUOM As String
        Dim mConsumableQty As Double
        Dim mToolRemarks As String


        PubDBCn.Execute("DELETE FROM PRD_NEWBOM_OTHERS_DET  " & vbCrLf & " WHERE Mkey='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")

        With SprdOthers
            For i = 1 To .MaxRows - 1
                .Row = i

                .Col = ColConsumableCode
                mToolCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColConsumableUnit
                mToolUOM = MainClass.AllowSingleQuote(.Text)

                .Col = ColConsumableDeptCode
                mDeptCode = MainClass.AllowSingleQuote(.Text)

                .Col = colConsumableOnQty
                mConsumableOnQty = Val(.Text)

                .Col = ColConsumableQty
                mConsumableQty = Val(.Text)

                .Col = ColConsumableRemarks
                mToolRemarks = MainClass.AllowSingleQuote(.Text)

                SqlStr = ""
                If Trim(mToolCode) <> "" And mConsumableOnQty <> 0 Then
                    SqlStr = " INSERT INTO  PRD_NEWBOM_OTHERS_DET ( " & vbCrLf _
                        & " MKEY, COMPANY_CODE, " & vbCrLf _
                        & " SERIAL_NO, CONSUMABLE_CODE, CONSUMABLE_UOM, " & vbCrLf _
                        & " DEPT_CODE, CONSUMABLE_ON_QTY, " & vbCrLf _
                        & " CONSUMABLE_QTY, CONSUMABLE_REMARKS ) VALUES ( " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(lblMKey.Text) & "', " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf _
                        & " " & i & ", '" & mToolCode & "', '" & mToolUOM & "'," & vbCrLf _
                        & " '" & mDeptCode & "',  " & vbCrLf _
                        & " " & mConsumableOnQty & ", " & mConsumableQty & ", '" & mToolRemarks & "')"

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        UpdateOthersDetail1 = True
        Exit Function
UpdateDetail1:
        MsgBox(Err.Description)
        UpdateOthersDetail1 = False
    End Function
    Private Sub SprdTool_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdTool.Change
        With SprdTool
            SprdTool_LeaveCell(SprdTool, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdTool_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdTool.KeyUpEvent
        Dim mCol As Short
        mCol = SprdTool.ActiveCol
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColToolDeptCode Then SprdTool_ClickEvent(SprdTool, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColToolDeptCode, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColToolCode Then SprdTool_ClickEvent(SprdTool, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColToolCode, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColToolDesc Then SprdTool_ClickEvent(SprdTool, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColToolDesc, 0))
    End Sub
    Private Sub SprdTool_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdTool.ClickEvent

        Dim SqlStr As String = ""
        Dim mToolName As String
        Dim mDeleted As Boolean
        Dim mToolCode As String
        Dim mDeptCode As String
        Dim Response As Integer

        If eventArgs.row = 0 And eventArgs.col = ColToolDeptCode Then
            With SprdTool
                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value
                If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColToolDeptCode
                    .Text = AcName1
                End If
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColToolCode Then
            With SprdTool
                SqlStr = "SELECT A.ITEM_CODE, A.ITEM_SHORT_DESC " & vbCrLf _
                    & " FROM INV_ITEM_MST A, INV_GENERAL_MST B" & vbCrLf _
                    & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND A.ITEM_STATUS = 'A' " & vbCrLf _
                    & " AND A.COMPANY_CODE=B.COMPANY_CODE AND A.CATEGORY_CODE=B.GEN_CODE AND B.GEN_TYPE='C' AND B.PRD_TYPE='T'" & vbCrLf _
                    & " ORDER BY A.ITEM_CODE "

                .Row = .ActiveRow
                .Col = ColToolCode
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColToolCode
                    .Text = AcName

                    .Col = ColToolDesc
                    .Text = AcName1

                    .Col = ColToolCode
                    mToolCode = .Text
                    Call FillToolGridRow(mToolCode)
                End If
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColToolDesc Then
            With SprdTool

                SqlStr = "SELECT  A.ITEM_SHORT_DESC, A.ITEM_CODE " & vbCrLf _
                    & " FROM INV_ITEM_MST A, INV_GENERAL_MST B" & vbCrLf _
                    & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND A.ITEM_STATUS = 'A' " & vbCrLf _
                    & " AND A.COMPANY_CODE=B.COMPANY_CODE AND A.CATEGORY_CODE=B.GEN_CODE AND B.GEN_TYPE='C' AND B..PRD_TYPE='T'" & vbCrLf _
                    & " ORDER BY A.ITEM_SHORT_DESC "

                .Row = .ActiveRow

                .Col = ColToolDesc
                mToolName = .Text

                .Text = ""
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColToolDesc
                    .Text = AcName

                    .Col = ColToolCode
                    .Text = AcName1
                Else
                    .Row = .ActiveRow

                    .Col = ColToolDesc
                    .Text = mToolName
                End If
                .Col = ColToolCode
                mToolCode = .Text
                Call FillToolGridRow(mToolCode)
            End With
        End If

        With SprdTool
            If eventArgs.col = 0 And eventArgs.row > 0 And (ADDMode = True Or MODIFYMode = True) Then
                Response = CInt(MsgQuestion("Click 'Yes' for Insert  And 'No' for Delete. "))
                If Response = MsgBoxResult.Yes Then
                    .Row = eventArgs.row
                    .Action = SS_ACTION_INSERT_ROW
                    If .MaxRows >= 1 Then .MaxRows = .MaxRows + 1
                    MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
                Else
                    Response = CInt(MsgQuestion("Are you sure to Delete this Row ? "))
                    If Response = MsgBoxResult.Yes Then
                        .Row = eventArgs.row
                        .Col = ColToolCode
                        mToolCode = Trim(.Text)

                        .Row = eventArgs.row
                        .Action = SS_ACTION_DELETE_ROW
                        If .MaxRows > 1 Then .MaxRows = .MaxRows - 1

                        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
                    End If
                End If
            End If
        End With
    End Sub
    Private Sub SprdTool_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdTool.LeaveCell

        On Error GoTo ErrPart
        Dim mDeptCode As String
        Dim mToolCode As String

        If eventArgs.newRow = -1 Then Exit Sub
        SprdTool.Row = eventArgs.row
        SprdTool.Col = ColToolCode
        If Trim(SprdTool.Text) = "" Then Exit Sub
        Select Case eventArgs.col
            Case ColToolDeptCode
                SprdTool.Row = SprdTool.ActiveRow
                SprdTool.Col = ColToolDeptCode
                mDeptCode = Trim(SprdTool.Text)
                SprdTool.Col = ColToolCode
                mToolCode = Trim(SprdTool.Text)
                If mToolCode <> "" Then
                    If CheckDuplicateTool(SprdTool, mToolCode, mDeptCode) = True Then
                        MainClass.SetFocusToCell(SprdTool, SprdTool.ActiveRow, ColToolDeptCode)
                    End If
                End If
            Case ColToolCode
                SprdTool.Row = SprdTool.ActiveRow
                SprdTool.Col = ColToolDeptCode
                mDeptCode = Trim(SprdTool.Text)
                SprdTool.Col = ColToolCode
                mToolCode = Trim(SprdTool.Text)
                If Trim(txtProductCode.Text) = Trim(mToolCode) Then
                    MainClass.SetFocusToCell(SprdTool, SprdTool.ActiveRow, ColToolCode)
                Else
                    If CheckDuplicateItem(SprdTool, mToolCode, mDeptCode) = False Then
                        SprdTool.Row = SprdTool.ActiveRow
                        SprdTool.Col = ColToolCode
                        Call FillToolGridRow((SprdTool.Text))
                    Else
                        MainClass.SetFocusToCell(SprdTool, SprdTool.ActiveRow, ColToolCode)
                    End If
                End If
            Case colToolStdQty
                If CheckQty(SprdTool, eventArgs.col, eventArgs.row) = True Then
                    MainClass.AddBlankSprdRow(SprdTool, ColToolCode, ConRowHeight)
                    FormatSprdTool((SprdTool.MaxRows))
                End If
            Case ColToolLife
                If CheckQty(SprdTool, eventArgs.col, eventArgs.row) = True Then
                    MainClass.AddBlankSprdRow(SprdTool, ColToolCode, ConRowHeight)
                    FormatSprdTool((SprdTool.MaxRows))
                End If
            Case ColToolQUnit
                SprdTool.Row = SprdTool.ActiveRow
                SprdTool.Col = ColToolQUnit
                If Trim(SprdTool.Text) <> "" Then Call CheckUnit(SprdTool, ColToolQUnit, SprdTool.ActiveRow)

        End Select

        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Function CheckDuplicateTool(ByRef pSprd As AxFPSpreadADO.AxfpSpread, ByRef pToolCode As String, ByRef pDeptCode As String) As Boolean
        On Error GoTo ERR1
        Dim i As Integer
        Dim mItemRept As Integer

        If Trim(pToolCode) = "" Then CheckDuplicateTool = False : Exit Function
        With pSprd
            For i = 1 To .MaxRows
                .Row = i
                .Col = ColToolCode
                If UCase(Trim(.Text)) = UCase(Trim(pToolCode)) Then
                    .Col = ColDeptCode
                    If UCase(Trim(.Text)) = UCase(Trim(pDeptCode)) Then
                        mItemRept = mItemRept + 1
                        If mItemRept > 1 Then
                            MsgInformation("Duplication Item in the Same Department")
                            CheckDuplicateTool = True
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

    Private Sub FillToolGridRow(ByRef mItemCode As String)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsMisc As ADODB.Recordset = Nothing

        If Trim(mItemCode) = "" Then Exit Sub
        SqlStr = " SELECT ITEM_CODE,ITEM_SHORT_DESC,ISSUE_UOM " & vbCrLf _
            & " FROM INV_ITEM_MST WHERE ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS = 'A' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If Not RsMisc.EOF Then
            SprdTool.Row = SprdTool.ActiveRow
            With RsMisc
                SprdTool.Col = ColToolDesc
                SprdTool.Text = IIf(IsDBNull(.Fields("ITEM_SHORT_DESC").Value), "", .Fields("ITEM_SHORT_DESC").Value)

                SprdTool.Col = ColToolQUnit
                SprdTool.Text = IIf(IsDBNull(.Fields("ISSUE_UOM").Value), "", .Fields("ISSUE_UOM").Value)
            End With
        Else
            MainClass.SetFocusToCell(SprdTool, SprdTool.ActiveRow, ColToolCode)
        End If
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Private Sub SprdOthers_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdOthers.Change
        With SprdOthers
            SprdOthers_LeaveCell(SprdOthers, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdOthers_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdOthers.KeyUpEvent
        Dim mCol As Short
        mCol = SprdOthers.ActiveCol
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColConsumableDeptCode Then SprdOthers_ClickEvent(SprdOthers, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColConsumableDeptCode, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColConsumableCode Then SprdOthers_ClickEvent(SprdOthers, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColConsumableCode, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColConsumableDesc Then SprdOthers_ClickEvent(SprdOthers, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColConsumableDesc, 0))
    End Sub
    Private Sub SprdOthers_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdOthers.ClickEvent

        Dim SqlStr As String = ""
        Dim mToolName As String
        Dim mDeleted As Boolean
        Dim mToolCode As String
        Dim mDeptCode As String
        Dim Response As Integer

        If eventArgs.row = 0 And eventArgs.col = ColToolDeptCode Then
            With SprdOthers
                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value
                If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColToolDeptCode
                    .Text = AcName1
                End If
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColConsumableCode Then
            With SprdOthers
                SqlStr = "SELECT A.ITEM_CODE, A.ITEM_SHORT_DESC, A.ISSUE_UOM " & vbCrLf _
                    & " FROM INV_ITEM_MST A, INV_GENERAL_MST B" & vbCrLf _
                    & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND A.ITEM_STATUS = 'A' " & vbCrLf _
                    & " AND A.COMPANY_CODE=B.COMPANY_CODE AND A.CATEGORY_CODE=B.GEN_CODE AND B.GEN_TYPE='C' AND B.PRD_TYPE IN ('G','C')" & vbCrLf _
                    & " ORDER BY A.ITEM_CODE "

                .Row = .ActiveRow
                .Col = ColConsumableCode
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColConsumableCode
                    .Text = AcName

                    .Col = ColConsumableDesc
                    .Text = AcName1

                    .Col = ColConsumableUnit
                    .Text = AcName2

                    .Col = ColConsumableCode
                    mToolCode = SprdOthers.Text
                    Call FillOthersGridRow(mToolCode)
                End If
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColConsumableDesc Then
            With SprdOthers

                SqlStr = "SELECT  A.ITEM_SHORT_DESC, A.ITEM_CODE, A.ISSUE_UOM " & vbCrLf _
                    & " FROM INV_ITEM_MST A, INV_GENERAL_MST B" & vbCrLf _
                    & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND A.ITEM_STATUS = 'A' " & vbCrLf _
                    & " AND A.COMPANY_CODE=B.COMPANY_CODE AND A.CATEGORY_CODE=B.GEN_CODE AND B.GEN_TYPE='C' AND B..PRD_TYPE IN ('G','C')" & vbCrLf _
                    & " ORDER BY A.ITEM_SHORT_DESC "

                .Row = .ActiveRow

                .Col = ColConsumableDesc
                mToolName = .Text

                .Text = ""
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColConsumableDesc
                    .Text = AcName

                    .Col = ColConsumableCode
                    .Text = AcName1

                    .Col = ColConsumableUnit
                    .Text = AcName2

                Else
                    .Row = .ActiveRow

                    .Col = ColConsumableDesc
                    .Text = mToolName
                End If
                .Col = ColConsumableCode
                mToolCode = SprdOthers.Text
                Call FillOthersGridRow(mToolCode)
            End With
        End If

        With SprdOthers
            If eventArgs.col = 0 And eventArgs.row > 0 And (ADDMode = True Or MODIFYMode = True) Then
                Response = CInt(MsgQuestion("Click 'Yes' for Insert  And 'No' for Delete. "))
                If Response = MsgBoxResult.Yes Then
                    .Row = eventArgs.row
                    .Action = SS_ACTION_INSERT_ROW
                    If .MaxRows >= 1 Then .MaxRows = .MaxRows + 1
                    MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
                Else
                    Response = CInt(MsgQuestion("Are you sure to Delete this Row ? "))
                    If Response = MsgBoxResult.Yes Then
                        .Row = eventArgs.row
                        .Col = ColConsumableCode
                        mToolCode = Trim(.Text)

                        .Row = eventArgs.row
                        .Action = SS_ACTION_DELETE_ROW
                        If .MaxRows > 1 Then .MaxRows = .MaxRows - 1

                        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
                    End If
                End If
            End If
        End With
    End Sub
    Private Sub SprdOthers_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdOthers.LeaveCell

        On Error GoTo ErrPart
        Dim mDeptCode As String
        Dim mToolCode As String

        If eventArgs.newRow = -1 Then Exit Sub
        SprdOthers.Row = eventArgs.row
        SprdOthers.Col = ColConsumableCode
        If Trim(SprdOthers.Text) = "" Then Exit Sub
        Select Case eventArgs.col
            Case ColConsumableDeptCode
                SprdOthers.Row = SprdOthers.ActiveRow
                SprdOthers.Col = ColConsumableDeptCode
                mDeptCode = Trim(SprdOthers.Text)
                SprdOthers.Col = ColConsumableCode
                mToolCode = Trim(SprdOthers.Text)
                If mToolCode <> "" Then
                    If CheckDuplicateOthers(SprdOthers, mToolCode, mDeptCode) = True Then
                        MainClass.SetFocusToCell(SprdOthers, SprdOthers.ActiveRow, ColConsumableDeptCode)
                    End If
                End If
            Case ColConsumableCode
                SprdOthers.Row = SprdOthers.ActiveRow
                SprdOthers.Col = ColConsumableDeptCode
                mDeptCode = Trim(SprdOthers.Text)
                SprdOthers.Col = ColConsumableCode
                mToolCode = Trim(SprdOthers.Text)
                If Trim(txtProductCode.Text) = Trim(mToolCode) Then
                    MainClass.SetFocusToCell(SprdOthers, SprdOthers.ActiveRow, ColConsumableCode)
                Else
                    If CheckDuplicateItem(SprdOthers, mToolCode, mDeptCode) = False Then
                        SprdOthers.Row = SprdOthers.ActiveRow
                        SprdOthers.Col = ColConsumableCode
                        mToolCode = SprdOthers.Text
                        Call FillOthersGridRow(mToolCode)
                    Else
                        MainClass.SetFocusToCell(SprdOthers, SprdOthers.ActiveRow, ColConsumableCode)
                    End If
                End If
            Case colConsumableOnQty
                If CheckQty(SprdOthers, eventArgs.col, eventArgs.row) = True Then
                    MainClass.AddBlankSprdRow(SprdOthers, ColConsumableCode, ConRowHeight)
                    FormatSprdOthers((SprdOthers.MaxRows))
                End If
            Case ColConsumableQty
                If CheckQty(SprdOthers, eventArgs.col, eventArgs.row) = True Then
                    MainClass.AddBlankSprdRow(SprdOthers, ColConsumableCode, ConRowHeight)
                    FormatSprdOthers((SprdOthers.MaxRows))
                End If
            Case ColConsumableUnit
                SprdOthers.Row = SprdOthers.ActiveRow
                SprdOthers.Col = ColConsumableUnit
                If Trim(SprdOthers.Text) <> "" Then Call CheckUnit(SprdOthers, ColConsumableUnit, SprdOthers.ActiveRow)

        End Select

        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Function CheckDuplicateOthers(ByRef pSprd As AxFPSpreadADO.AxfpSpread, ByRef pToolCode As String, ByRef pDeptCode As String) As Boolean
        On Error GoTo ERR1
        Dim i As Integer
        Dim mItemRept As Integer

        If Trim(pToolCode) = "" Then CheckDuplicateOthers = False : Exit Function
        With pSprd
            For i = 1 To .MaxRows
                .Row = i
                .Col = ColToolCode
                If UCase(Trim(.Text)) = UCase(Trim(pToolCode)) Then
                    .Col = ColDeptCode
                    If UCase(Trim(.Text)) = UCase(Trim(pDeptCode)) Then
                        mItemRept = mItemRept + 1
                        If mItemRept > 1 Then
                            MsgInformation("Duplication Item in the Same Department")
                            CheckDuplicateOthers = True
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

    Private Sub FillOthersGridRow(ByRef mItemCode As String)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsMisc As ADODB.Recordset = Nothing

        If Trim(mItemCode) = "" Then Exit Sub
        SqlStr = " SELECT ITEM_CODE,ITEM_SHORT_DESC,ISSUE_UOM " & vbCrLf _
            & " FROM INV_ITEM_MST WHERE ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS = 'A' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If Not RsMisc.EOF Then
            SprdOthers.Row = SprdOthers.ActiveRow
            With RsMisc
                SprdOthers.Col = ColConsumableDesc
                SprdOthers.Text = IIf(IsDBNull(.Fields("ITEM_SHORT_DESC").Value), "", .Fields("ITEM_SHORT_DESC").Value)

                SprdOthers.Col = ColConsumableUnit
                SprdOthers.Text = IIf(IsDBNull(.Fields("ISSUE_UOM").Value), "", .Fields("ISSUE_UOM").Value)
            End With
        Else
            MainClass.SetFocusToCell(SprdOthers, SprdOthers.ActiveRow, ColConsumableCode)
        End If
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
End Class
