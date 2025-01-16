Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmBOMOut
    Inherits System.Windows.Forms.Form
    Dim RsOutBOMHdr As ADODB.Recordset
    Dim RsOutBOMDet As ADODB.Recordset

    'Dim PvtDBCn As ADODB.Connection

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

    Private Const ConRowHeight As Short = 12

    Private Const ColItemCode As Short = 1
    Private Const ColItemDesc As Short = 2
    Private Const ColItemPartNo As Short = 3
    Private Const ColItemUOM As Short = 4
    Private Const ColItemQty As Short = 5
    Private Const ColScrapQty As Short = 6
    Private Const ColQtyVar As Short = 7
    Private Const ColStockType As Short = 8
    Private Const ColAlternate As Short = 9
    Dim mAmendStatus As Boolean

    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo AssignGridErr
        Dim SqlStr As String = ""
        SqlStr = ""
        SqlStr = "SELECT IH.PRODUCT_CODE, INVMST.ITEM_SHORT_DESC, AMEND_NO, TO_CHAR(WEF,'DD/MM/YYYY') AS WEF, DECODE(STATUS,'O','OPEN','CLOSE') AS STATUS " & vbCrLf & " FROM PRD_OUTBOM_HDR IH, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND IH.PRODUCT_CODE=INVMST.ITEM_CODE" & vbCrLf & " ORDER BY IH.PRODUCT_CODE, AMEND_NO "

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
            .set_ColWidth(2, 40)
            .set_ColWidth(3, 10)
            .set_ColWidth(4, 10)
            .set_ColWidth(5, 10)

            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub

    Private Function CheckDuplicateItem(ByRef pItemCode As String) As Boolean
        On Error GoTo ERR1
        Dim i As Integer
        Dim mItemRept As Integer

        If Trim(pItemCode) = "" Then CheckDuplicateItem = False : Exit Function
        With SprdMain
            For i = 1 To .MaxRows
                .Row = i
                .Col = ColItemCode
                If UCase(Trim(.Text)) = UCase(Trim(pItemCode)) Then
                    mItemRept = mItemRept + 1
                    If mItemRept > 1 Then
                        MsgInformation("Duplication Item in the Same Department")
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

    Private Function FieldsVarification() As Boolean

        On Error GoTo err_Renamed
        Dim mCategory As String
        Dim mRMCode As String
        Dim cntRow As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mItemCode As String
        Dim mMainItemCode As String

        FieldsVarification = True

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsOutBOMHdr.EOF = True Then Exit Function

        If MODIFYMode = True Then
            If RsOutBOMHdr.Fields("Status").Value = "Y" Then
                MsgInformation("Closed BOM Cann't be Modified")
                FieldsVarification = False
                Exit Function
            End If
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

        mCategory = GetProductionType(Trim(txtProductCode.Text))

        If (mCategory = "B" Or mCategory = "R" Or mCategory = "3") And chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If chkBOP.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                MsgInformation("Please check Product Category. You Defined BOP/Raw Material Category.")
                FieldsVarification = False
                Exit Function
            Else
                If CheckPurchaseOrder((txtWEF.Text), Trim(txtProductCode.Text)) = False Then
                    MsgInformation("Purchase Order is Not Aviable, so cann't be select BOP.")
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        End If

        If chkBOP.CheckState = System.Windows.Forms.CheckState.Checked And chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If (mCategory = "B" Or mCategory = "R" Or mCategory = "3") Then

            Else
                MsgInformation("Category is not BOP/Raw Material.Please unchecked from BOP")
                FieldsVarification = False
                Exit Function
            End If

        End If

        '    If mCategory = "I" Or mCategory = "P" Then
        '
        '    Else
        '        MsgInformation "Please check Product Category. Category Should be Inhouse Or Production"
        '        FieldsVarification = False
        '        Exit Function
        '    End If

        If chkInhouse.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If CheckItemBom(Trim(txtProductCode.Text), "B") = True Then
                MsgInformation("Production BOM is Aviable. Please Check that it is Inhouse also.")
                FieldsVarification = False
                Exit Function
            End If
        End If

        If chkInhouse.CheckState = System.Windows.Forms.CheckState.Checked Then
            If CheckItemBom(Trim(txtProductCode.Text), "B") = False Then
                MsgInformation("Production BOM is Not Aviable. Please UnCheck from Inhouse.")
                FieldsVarification = False
                Exit Function
            End If
        End If

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColItemCode
                mRMCode = Trim(.Text)

                If ValidateMainCode(mRMCode) = False Then
                    FieldsVarification = False
                    Exit Function
                End If
            Next
        End With

        SqlStr = "SELECT ITEM_CODE, ALTER_ITEM_CODE  " & vbCrLf & " FROM TEMP_PRD_OUTBOM_ALTER_DET" & vbCrLf & " WHERE USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mItemCode = Trim(IIf(IsDbNull(RsTemp.Fields("ALTER_ITEM_CODE").Value), "", RsTemp.Fields("ALTER_ITEM_CODE").Value))
                mMainItemCode = Trim(IIf(IsDbNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))
                If ValidateAlternetCode(mMainItemCode, mItemCode) = False Then
                    MsgInformation("Main Item Code & Alter Item Code not defined in Alternate Master.")
                    FieldsVarification = False
                    Exit Function
                End If
                RsTemp.MoveNext()
            Loop
        End If

        If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "Item Code Is Blank") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColItemQty, "N", "Please Check Std.Qty") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColStockType, "S", "Please Check Stock Type") = False Then FieldsVarification = False : Exit Function

        Exit Function
err_Renamed:
        Resume
        MsgBox(Err.Description)
    End Function

    Private Function ValidateMainCode(ByRef mItemCode As String) As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsMisc As ADODB.Recordset = Nothing

        ValidateMainCode = False
        If Trim(mItemCode) = "" Then ValidateMainCode = True : Exit Function


        SqlStr = "SELECT B.ITEM_CODE " & vbCrLf & " FROM INV_ITEM_ALTER_DET B " & vbCrLf & " WHERE B.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND B.ALTER_ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If Not RsMisc.EOF Then
            MsgInformation("This Item Code is Alternate for Item Code : " & Trim(IIf(IsDbNull(RsMisc.Fields("ITEM_CODE").Value), "", RsMisc.Fields("ITEM_CODE").Value)) & ". Please Select Main Item Code.")
            ValidateMainCode = False
        Else
            ValidateMainCode = True
        End If
        Exit Function
ERR1:
        MsgBox(Err.Description)
    End Function

    Private Sub FillGridRow(ByRef mItemCode As String)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim mSizeCode As Integer


        If Trim(mItemCode) = "" Then Exit Sub
        SqlStr = " SELECT ITEM_CODE,ITEM_SHORT_DESC,CUSTOMER_PART_NO,ISSUE_UOM " & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' " & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        '' AND ITEM_STATUS = 'A'

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If Not RsMisc.EOF Then
            SprdMain.Row = SprdMain.ActiveRow
            With RsMisc
                SprdMain.Col = ColItemDesc
                SprdMain.Text = IIf(IsDbNull(.Fields("ITEM_SHORT_DESC").Value), "", .Fields("ITEM_SHORT_DESC").Value)

                SprdMain.Col = ColItemPartNo
                SprdMain.Text = IIf(IsDbNull(.Fields("CUSTOMER_PART_NO").Value), "", .Fields("CUSTOMER_PART_NO").Value)

                SprdMain.Col = ColItemUOM
                SprdMain.Text = IIf(IsDbNull(.Fields("ISSUE_UOM").Value), "", .Fields("ISSUE_UOM").Value)
            End With
        Else
            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
        End If
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub

    Private Sub chkBOP_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkBOP.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkInhouse_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkInhouse.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkStatus_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkStatus.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If cmdAdd.Text = ConCmdAddCaption Then
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
            cmdAdd.Text = ConCmdAddCaption
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

        Dim mProductCode As String = ""
        'Dim i As Integer

        mProductCode = Trim(txtProductCode.Text)

        If mProductCode = "" Then
            MsgInformation("Please Select Product")
            Exit Sub
        End If

        Call txtProductCode_Validating(txtProductCode, New System.ComponentModel.CancelEventArgs(True))

        txtAmendNo.Text = CStr(GetMaxAmendNo(mProductCode))
        chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkInhouse.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkBOP.CheckState = System.Windows.Forms.CheckState.Unchecked

        mAmendStatus = True
        cmdAmend.Enabled = False
        txtWEF.Enabled = True
        SprdMain.Enabled = True

        txtPrepBy.Text = ""
        lblPrepBy.Text = ""
        txtAppBy.Text = ""
        lblAppBy.Text = ""

        txtPrepBy.Enabled = True
        cmdSearchPrepBy.Enabled = True

        ADDMode = True
        MODIFYMode = False

        MainClass.ButtonStatus(Me, XRIGHT, RsOutBOMHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Close()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click

        On Error GoTo DelErrPart

        If chkStatus.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Closed BOM Cann't be Deleted")
            Exit Sub
        End If

        If Trim(txtProductCode.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        PubDBCn.Cancel()
        PubDBCn.BeginTrans()

        If Not RsOutBOMHdr.EOF Then
            If PubSuperUser = "U" Then
                If RsOutBOMHdr.Fields("APP_BY").Value <> "" Then MsgBox("BOM has been approved, So cann't be deleted") : Exit Sub
            End If
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then

                If InsertIntoDelAudit(PubDBCn, "PRD_OUTBOM_HDR", (txtProductCode.Text), RsOutBOMHdr) = False Then GoTo DelErrPart
                If InsertIntoDelAudit(PubDBCn, "PRD_OUTBOM_DET", (txtProductCode.Text), RsOutBOMDet) = False Then GoTo DelErrPart

                If InsertIntoDeleteTrn(PubDBCn, "PRD_OUTBOM_HDR", "MKEY", UCase(lblMKey.Text)) = False Then GoTo DelErrPart

                PubDBCn.Execute("DELETE FROM PRD_OUTBOM_ALTER_DET WHERE Mkey='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")
                PubDBCn.Execute("DELETE FROM PRD_OUTBOM_DET WHERE Mkey='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")
                PubDBCn.Execute("DELETE FROM PRD_OUTBOM_HDR  WHERE Mkey='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")

                If Val(txtAmendNo.Text) > 0 Then
                    If UpdatePreviousBOM((txtProductCode.Text), Val(txtAmendNo.Text), "O") = False Then GoTo DelErrPart
                End If

                PubDBCn.CommitTrans()
                RsOutBOMHdr.Requery()
                RsOutBOMDet.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        RsOutBOMHdr.Requery()
        RsOutBOMDet.Requery()
        PubDBCn.Cancel()
        MsgBox(Err.Description)
    End Sub

    Private Function UpdatePreviousBOM(ByRef pProductCode As String, ByRef pAmendNo As Integer, ByRef pPreviousStatus As String) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = ""
        SqlStr = " UPDATE PRD_OUTBOM_HDR SET " & vbCrLf & " STATUS = '" & pPreviousStatus & "', " & vbCrLf & " MODUSER = '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " MODDATE = TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AMEND_NO = " & pAmendNo - 1 & "" & vbCrLf & " AND PRODUCT_CODE ='" & MainClass.AllowSingleQuote(pProductCode) & "'"

        PubDBCn.Execute(SqlStr)

        UpdatePreviousBOM = True

        Exit Function
ErrPart:
        UpdatePreviousBOM = False
        If Err.Description = "" Then Exit Function
        MsgBox(Err.Description)
        '    Resume
    End Function

    Private Function GetMaxAmendNo(ByRef pProductCode As String) As Integer

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = " SELECT MAX(AMEND_NO) AS AMEND_NO" & vbCrLf & " FROM PRD_OUTBOM_HDR" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(pProductCode) & "'"

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
        If cmdModify.Text = ConcmdmodifyCaption Then
            If PubSuperUser = "U" Then
                If RsOutBOMHdr.Fields("APP_BY").Value <> "" Then MsgBox("BOM has been approved, So cann't be modified") : Exit Sub
            End If

            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsOutBOMHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
        Call PrintOutBOM(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call PrintOutBOM(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub PrintOutBOM(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mTitle As String = ""

        If InsertIntoPrintdummyData = False Then GoTo ERR1

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        mTitle = "Bill Of Material"

        SqlStr = ""
        SqlStr = "SELECT * " & vbCrLf & " FROM TEMP_PRINTDUMMYDATA " & vbCrLf & " WHERE UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf & " ORDER BY SUBROW"

        MainClass.AssignCRptFormulas(Report1, "PCode=""" & txtProductCode.Text & """")
        MainClass.AssignCRptFormulas(Report1, "PName=""" & txtProductDesc.Text & """")
        MainClass.AssignCRptFormulas(Report1, "PartNo=""" & txtCustPartNo.Text & """")
        MainClass.AssignCRptFormulas(Report1, "Model=""" & txtModelNo.Text & """")

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\OutBOMPrint.rpt" 'BillOfMat.rpt"

        SetCrpt(Report1, Mode, 1, mTitle)
        Report1.SQLQuery = SqlStr
        Report1.WindowShowGroupTree = False
        Report1.Action = 1

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Function InsertIntoPrintdummyData() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim pSqlStr As String = ""

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        PubDBCn.Execute("DELETE FROM TEMP_PRINTDUMMYDATA WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'")

        SqlStr = " SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " ID.SERIAL_NO, " & vbCrLf & " ID.ITEM_CODE, INVMST.ITEM_SHORT_DESC, INVMST.CUSTOMER_PART_NO,ID.ITEM_UOM, " & vbCrLf & " TO_CHAR(ID.ITEM_QTY,'9999.999'),  " & vbCrLf & " TO_CHAR(SCRAP_QTY,'999.999') "

        SqlStr = SqlStr & vbCrLf & " FROM PRD_OUTBOM_HDR IH,PRD_OUTBOM_DET ID, INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE " & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "' " & vbCrLf & " AND IH.WEF=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " ORDER BY ID.SERIAL_NO"

        pSqlStr = " INSERT INTO " & vbCrLf & " TEMP_PRINTDUMMYDATA (" & vbCrLf & " USERID, SUBROW, FIELD1, " & vbCrLf & " FIELD2, FIELD3, FIELD4, " & vbCrLf & " FIELD5, FIELD6 )" & vbCrLf & SqlStr

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
            If cmdAdd.Enabled = True And cmdAdd.Visible = True Then cmdAdd.Focus()
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
        Resume
    End Sub

    Private Sub cmdSearchAppBy_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchAppBy.Click
        Dim SqlStr As String = ""
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

        mSqlStr = " SELECT IH.PRODUCT_CODE, IH.AMEND_NO, INV.ITEM_SHORT_DESC, IH.WEF " & vbCrLf & " FROM PRD_OUTBOM_HDR IH, INV_ITEM_MST INV " & vbCrLf & " WHERE IH.COMPANY_CODE=INV.COMPANY_CODE " & vbCrLf & " AND IH.PRODUCT_CODE=INV.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If Trim(txtCopyProductCode.Text) <> "" Then
            mSqlStr = mSqlStr & vbCrLf & " AND IH.PRODUCT_CODE='" & Trim(txtCopyProductCode.Text) & "'"
        End If

        If MainClass.SearchGridMasterBySQL2("", mSqlStr) = True Then
            txtCopyAmendNo.Text = AcName1
            txtCopyProductCode.Text = AcName
            If txtCopyProductCode.Enabled = True Then txtCopyProductCode.Focus()
        End If

    End Sub

    Private Sub cmdSearchDept_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchDept.Click
        On Error GoTo SrchERR
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_CODE", "DEPT_DESC", , , SqlStr) = True Then
            txtDeptCode.Text = AcName
        End If
        Exit Sub
SrchERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchProdCode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchProdCode.Click
        Dim mSqlStr As String

        'mSqlStr = " SELECT IH.PRODUCT_CODE, IH.WEF, INV.ITEM_SHORT_DESC, IH.PRODUCT_UOM " & vbCrLf _
        '    & " FROM PRD_OUTBOM_HDR IH, INV_ITEM_MST INV " & vbCrLf _
        '    & " WHERE IH.COMPANY_CODE=INV.COMPANY_CODE " & vbCrLf _
        '    & " AND IH.PRODUCT_CODE=INV.ITEM_CODE " & vbCrLf _
        '    & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        mSqlStr = " SELECT ITEM_CODE, INV.ITEM_SHORT_DESC, ISSUE_UOM " & vbCrLf _
            & " FROM INV_ITEM_MST INV " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If Trim(txtProductCode.Text) <> "" Then
            mSqlStr = mSqlStr & vbCrLf & " AND ITEM_CODE='" & Trim(txtProductCode.Text) & "'"
        End If

        If MainClass.SearchGridMasterBySQL2("", mSqlStr) = True Then
            'txtWEF.Text = VB6.Format(AcName1, "DD/MM/YYYY")
            txtProductCode.Text = AcName
            txtProductDesc.Text = AcName1
            txtUnit.Text = AcName2
            If txtProductCode.Enabled = True Then txtProductCode.Focus()
            'If ShowRecord = False Then Exit Sub
        End If
    End Sub

    Private Sub cmdSearchPrepBy_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchPrepBy.Click
        On Error GoTo SrchERR
        Dim SqlStr As String = ""
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

        mSqlStr = " SELECT IH.WEF, IH.PRODUCT_CODE, INV.ITEM_SHORT_DESC, INV.ISSUE_UOM " & vbCrLf & " FROM PRD_OUTBOM_HDR IH, INV_ITEM_MST INV " & vbCrLf & " WHERE IH.COMPANY_CODE=INV.COMPANY_CODE " & vbCrLf & " AND IH.PRODUCT_CODE=INV.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If Trim(txtProductCode.Text) <> "" Then
            mSqlStr = mSqlStr & vbCrLf & " AND IH.PRODUCT_CODE='" & Trim(txtProductCode.Text) & "'"
        End If

        If MainClass.SearchGridMasterBySQL2("", mSqlStr) = True Then
            txtWEF.Text = VB6.Format(AcName, "DD/MM/YYYY")
            txtProductCode.Text = AcName1
            If txtWEF.Enabled = True Then txtWEF.Focus()
            If ShowRecord = False Then Exit Sub
        End If
    End Sub

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Private Sub frmBOMOut_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        If FormActive = True Then Exit Sub

        Me.Text = "Bill Of Material (Outward Jobwork)"

        SqlStr = ""

        SqlStr = "Select * from PRD_OUTBOM_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOutBOMHdr, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * from PRD_OUTBOM_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOutBOMDet, ADODB.LockTypeEnum.adLockReadOnly)

        AssignGrid(False)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        SetTextLengths()

        If cmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmBOMOut_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmBOMOut_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub frmBOMOut_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        Call SetMainFormCordinate(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7590)
        ''Me.Width = VB6.TwipsToPixelsX(11385)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub SetTextLengths()

        On Error GoTo ERR1
        With RsOutBOMHdr
            txtProductCode.Maxlength = .Fields("PRODUCT_CODE").DefinedSize
            txtProductDesc.Maxlength = MainClass.SetMaxLength("ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn)
            txtUnit.Maxlength = .Fields("PRODUCT_UOM").DefinedSize
            txtWEF.Maxlength = .Fields("WEF").DefinedSize - 6
            txtRemarks.Maxlength = .Fields("Remarks").DefinedSize
            txtPrepBy.Maxlength = .Fields("PREP_BY").DefinedSize
            txtDeptCode.Maxlength = .Fields("DEPTCODE").DefinedSize
            txtAppBy.Maxlength = .Fields("APP_BY").DefinedSize
            txtProcessCost.Maxlength = .Fields("PROCESS_COST").Precision
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
        txtPrepBy.Enabled = mMode
        cmdSearchPrepBy.Enabled = mMode

        txtProductDesc.Enabled = False
        txtUnit.Enabled = False
        txtModelNo.Enabled = False
        txtCustPartNo.Enabled = False
        txtAmendNo.Enabled = False
    End Sub

    Private Sub frmBOMOut_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsOutBOMHdr.Close()
        RsOutBOMDet.Close()
        'PvtDBCn.Close
        RsOutBOMHdr = Nothing
        RsOutBOMDet = Nothing
        'Set PvtDBCn = Nothing
    End Sub

    Private Sub Clear1()

        lblMKey.Text = ""
        txtProductCode.Text = ""
        txtProductDesc.Text = ""
        txtUnit.Text = ""
        txtWEF.Text = ""
        txtModelNo.Text = ""
        txtCustPartNo.Text = ""
        txtRemarks.Text = ""
        txtProcessCost.Text = "0.000"
        txtPrepBy.Text = PubUserEMPCode
        txtDeptCode.Text = ""
        lblPrepBy.Text = ""
        txtAppBy.Text = ""
        lblAppBy.Text = ""
        txtAmendNo.Text = "0"

        txtCopyProductCode.Text = ""
        txtCopyProductDesc.Text = ""
        txtCopyAmendNo.Text = ""

        chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkInhouse.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkBOP.CheckState = System.Windows.Forms.CheckState.Unchecked

        chkStatus.Enabled = True
        mAmendStatus = False

        cmdAmend.Enabled = IIf(InStr(1, XRIGHT, "M") > 0, True, False) ''True   ''15-05-2010


        Call AutoCompleteSearch("PRD_OUTBOM_HDR", "PRODUCT_CODE", "", txtProductCode)
        Call AutoCompleteSearch("PAY_EMPLOYEE_MST", "EMP_CODE", " EMP_LEAVE_DATE IS NULL", txtPrepBy)
        Call AutoCompleteSearch("PAY_DEPT_MST", "DEPT_CODE", "", txtDeptCode)
        Call AutoCompleteSearch("PRD_OUTBOM_HDR", "PRODUCT_CODE", "", txtCopyProductCode)
        Call AutoCompleteSearch("PAY_EMPLOYEE_MST", "EMP_CODE", " EMP_LEAVE_DATE IS NULL", txtAppBy)

        MainClass.ClearGrid(SprdMain)
        FormatSprdMain(-1)
        Call DelTemp_OutBOMAlterDetail()
        Call MakeEnableDesableField(True)
        MainClass.ButtonStatus(Me, XRIGHT, RsOutBOMHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub FormatSprdMain(ByRef mRow As Integer)

        On Error GoTo ERR1
        With SprdMain
            .Row = mRow
            .set_RowHeight(0, ConRowHeight * 2.5)
            .set_RowHeight(mRow, ConRowHeight)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsOutBOMDet.Fields("ITEM_CODE").DefinedSize
            .set_ColWidth(.Col, 8)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(.Col, 30)

            .Col = ColItemPartNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("CUSTOMER_PART_NO", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(.Col, 12)

            .Col = ColItemUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ISSUE_UOM", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(.Col, 4)

            .Col = ColItemQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 4
            .TypeFloatMax = CDbl("9999999.9999")
            .TypeFloatMin = CDbl("-9999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 10)

            .Col = ColScrapQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 4
            .TypeFloatMax = CDbl("9999999999.9999")
            .TypeFloatMin = CDbl("-9999999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 9)

            .Col = ColQtyVar
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 4
            .TypeFloatMax = CDbl("9999999999.9999")
            .TypeFloatMin = CDbl("-9999999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 6)

            .Col = ColStockType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsOutBOMDet.Fields("STOCK_TYPE").DefinedSize
            .set_ColWidth(.Col, 4)

            .Col = ColAlternate
            .CellType = SS_CELL_TYPE_BUTTON
            .TypeButtonText = "Alternate"
            .TypeButtonAlign = SS_CELL_BUTTON_ALIGN_LEFT
            .set_ColWidth(ColAlternate, 6)

        End With
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemDesc, ColItemUOM)
        MainClass.SetSpreadColor(SprdMain, mRow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then
            RsOutBOMDet.Requery()
            '        Resume
        End If
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub

    Private Sub Show1()

        On Error GoTo ERR1

        With RsOutBOMHdr
            If Not .EOF Then
                ADDMode = False
                MODIFYMode = False

                lblMKey.Text = .Fields("MKey").Value
                txtProductCode.Text = Trim(IIf(IsDbNull(.Fields("PRODUCT_CODE").Value), "", .Fields("PRODUCT_CODE").Value))
                If MainClass.ValidateWithMasterTable((txtProductCode.Text), "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtProductDesc.Text = MasterNo
                End If
                txtUnit.Text = IIf(IsDbNull(.Fields("PRODUCT_UOM").Value), "", .Fields("PRODUCT_UOM").Value)
                txtWEF.Text = IIf(IsDbNull(.Fields("WEF").Value), "", .Fields("WEF").Value)
                If MainClass.ValidateWithMasterTable((txtProductCode.Text), "ITEM_CODE", "ITEM_MODEL", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtModelNo.Text = MasterNo
                End If
                If MainClass.ValidateWithMasterTable((txtProductCode.Text), "ITEM_CODE", "CUSTOMER_PART_NO", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtCustPartNo.Text = MasterNo
                End If
                txtAmendNo.Text = IIf(IsDbNull(.Fields("AMEND_NO").Value), "", .Fields("AMEND_NO").Value)
                chkStatus.CheckState = IIf(.Fields("Status").Value = "O", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
                chkInhouse.CheckState = IIf(.Fields("IS_INHOUSE").Value = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
                chkBOP.CheckState = IIf(.Fields("IS_BOP").Value = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)

                chkStatus.Enabled = IIf(.Fields("Status").Value = "O", True, False)
                txtRemarks.Text = IIf(IsDbNull(.Fields("Remarks").Value), "", .Fields("Remarks").Value)
                txtProcessCost.Text = VB6.Format(IIf(IsDbNull(.Fields("PROCESS_COST").Value), "0.000", .Fields("PROCESS_COST").Value), "0.000")
                txtPrepBy.Text = IIf(IsDbNull(.Fields("PREP_BY").Value), "", .Fields("PREP_BY").Value)
                txtPrepBy_Validating(txtPrepBy, New System.ComponentModel.CancelEventArgs(False))
                txtDeptCode.Text = IIf(IsDbNull(.Fields("DEPTCODE").Value), "", .Fields("DEPTCODE").Value)
                txtAppBy.Text = IIf(IsDbNull(.Fields("APP_BY").Value), "", .Fields("APP_BY").Value)
                txtAppBy_Validating(txtAppBy, New System.ComponentModel.CancelEventArgs(False))
                Call ShowDetail1((lblMKey.Text))
                Call ShowAlterDetail((lblMKey.Text))
                Call MakeEnableDesableField(False)
            End If
        End With
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsOutBOMHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        SprdMain.Enabled = True
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
        '    Resume
    End Sub

    Private Sub ShowDetail1(ByRef nMkey As String)

        On Error GoTo ERR1
        Dim i As Integer
        Dim SqlStr As String = ""
        Dim mItemDesc As String

        SqlStr = ""
        SqlStr = " SELECT * FROM PRD_OUTBOM_DET " & vbCrLf & " WHERE Mkey='" & MainClass.AllowSingleQuote(nMkey) & "'" & " ORDER BY SERIAL_NO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOutBOMDet, ADODB.LockTypeEnum.adLockReadOnly)
        With RsOutBOMDet
            If Not .EOF Then
                If .EOF = True Then Exit Sub
                SprdMain.MaxRows = MainClass.GetMaxRecord("PRD_OUTBOM_DET", PubDBCn, " Mkey='" & MainClass.AllowSingleQuote(nMkey) & "'") + 1
                FormatSprdMain(-1)
                i = 0
                .MoveFirst()
                Do While Not .EOF
                    i = i + 1
                    SprdMain.Row = i

                    SprdMain.Col = ColItemCode
                    SprdMain.Text = Trim(IIf(IsDbNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))

                    SprdMain.Col = ColItemDesc
                    If MainClass.ValidateWithMasterTable(.Fields("ITEM_CODE").Value, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "company_code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mItemDesc = MasterNo
                    Else
                        mItemDesc = ""
                    End If
                    SprdMain.Text = mItemDesc

                    SprdMain.Col = ColItemPartNo
                    If MainClass.ValidateWithMasterTable(.Fields("ITEM_CODE").Value, "ITEM_CODE", "CUSTOMER_PART_NO", "INV_ITEM_MST", PubDBCn, MasterNo, , "company_code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        SprdMain.Text = MasterNo
                    End If

                    SprdMain.Col = ColItemUOM
                    If MainClass.ValidateWithMasterTable(.Fields("ITEM_CODE").Value, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "company_code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        SprdMain.Text = MasterNo
                    End If

                    SprdMain.Col = ColItemQty
                    SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("ITEM_QTY").Value), "", .Fields("ITEM_QTY").Value)))

                    SprdMain.Col = ColScrapQty
                    SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("SCRAP_QTY").Value), "", .Fields("SCRAP_QTY").Value)))

                    SprdMain.Col = ColQtyVar
                    SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("QTY_VAR").Value), "", .Fields("QTY_VAR").Value)))

                    SprdMain.Col = ColStockType
                    SprdMain.Text = Trim(IIf(IsDbNull(.Fields("STOCK_TYPE").Value), "", .Fields("STOCK_TYPE").Value))

                    .MoveNext()
                Loop
            End If
        End With
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub

    Private Function UpdateDetail1() As Boolean

        On Error GoTo UpdateDetail1
        Dim SqlStr As String = ""
        Dim i As Integer
        Dim mItemCode As String
        Dim mItemUOM As String = ""
        Dim mItemQty As Double
        Dim mScrapQty As Double
        Dim mQtyVar As Double
        Dim mStockType As String = ""

        PubDBCn.Execute("DELETE FROM PRD_OUTBOM_ALTER_DET  " & vbCrLf & " WHERE Mkey='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")

        PubDBCn.Execute("DELETE FROM PRD_OUTBOM_DET  " & vbCrLf & " WHERE Mkey='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")

        With SprdMain
            For i = 1 To .MaxRows - 1
                .Row = i

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColItemUOM
                mItemUOM = MainClass.AllowSingleQuote(.Text)

                .Col = ColItemQty
                mItemQty = Val(.Text)

                .Col = ColScrapQty
                mScrapQty = Val(.Text)

                .Col = ColQtyVar
                mQtyVar = Val(.Text)

                .Col = ColStockType
                mStockType = MainClass.AllowSingleQuote(.Text)

                SqlStr = ""
                If Trim(mItemCode) <> "" And mItemQty > 0 Then
                    SqlStr = " INSERT INTO  PRD_OUTBOM_DET ( " & vbCrLf & " MKEY, SERIAL_NO, " & vbCrLf & " COMPANY_CODE, PRODUCT_CODE, " & vbCrLf & " ITEM_CODE, ITEM_UOM, " & vbCrLf & " STOCK_TYPE, ITEM_QTY, SCRAP_QTY, QTY_VAR ) VALUES ( " & vbCrLf & " '" & MainClass.AllowSingleQuote(lblMKey.Text) & "', " & i & ", " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", '" & MainClass.AllowSingleQuote(txtProductCode.Text) & "', " & vbCrLf & " '" & mItemCode & "', '" & mItemUOM & "', " & vbCrLf & " '" & mStockType & "', " & mItemQty & ", " & mScrapQty & ", " & mQtyVar & ")"

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

    Private Function Update1() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim nMkey As String
        Dim mRowNo As Integer
        Dim mStatus As String
        Dim mInHouse As String
        Dim mBOP As String

        mStatus = IIf(chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked, "O", "C")
        mInHouse = IIf(chkInhouse.CheckState = System.Windows.Forms.CheckState.Unchecked, "N", "Y")
        mBOP = IIf(chkBOP.CheckState = System.Windows.Forms.CheckState.Unchecked, "N", "Y")

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If ADDMode = True Then
            mRowNo = MainClass.AutoGenRowNo("PRD_OUTBOM_HDR", "ROWNO", PubDBCn)
            nMkey = RsCompany.Fields("COMPANY_CODE").Value & mRowNo & UCase(Trim(txtProductCode.Text)) & VB6.Format(txtWEF.Text, "YYYYMMDD")
            lblMKey.Text = nMkey

            SqlStr = ""
            SqlStr = " INSERT INTO PRD_OUTBOM_HDR (" & vbCrLf & " MKEY, COMPANY_CODE, " & vbCrLf & " PRODUCT_CODE, PRODUCT_UOM, " & vbCrLf & " WEF, AMEND_NO, STATUS, REMARKS, " & vbCrLf & " PREP_BY, APP_BY," & vbCrLf & " ADDUSER, ADDDATE, " & vbCrLf & " MODUSER, MODDATE,IS_INHOUSE,PROCESS_COST,DEPTCODE,IS_BOP ) VALUES ( "

            SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(nMkey) & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtProductCode.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtUnit.Text) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " " & Val(txtAmendNo.Text) & ", '" & mStatus & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(TxtRemarks.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtPrepBy.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtAppBy.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'',''," & vbCrLf & " '" & mInHouse & "'," & Val(txtProcessCost.Text) & ", '" & MainClass.AllowSingleQuote(txtDeptCode.Text) & "','" & mBOP & "')"
        ElseIf MODIFYMode = True Then
            SqlStr = ""
            SqlStr = " UPDATE PRD_OUTBOM_HDR  SET " & vbCrLf & " PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "', " & vbCrLf & " PRODUCT_UOM='" & MainClass.AllowSingleQuote(txtUnit.Text) & "', " & vbCrLf & " WEF=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " AMEND_NO=" & Val(txtAmendNo.Text) & ", " & vbCrLf & " STATUS='" & mStatus & "', IS_INHOUSE='" & mInHouse & "'," & vbCrLf & " PROCESS_COST=" & Val(txtProcessCost.Text) & "," & vbCrLf & " REMARKS='" & MainClass.AllowSingleQuote(TxtRemarks.Text) & "', " & vbCrLf & " PREP_BY='" & MainClass.AllowSingleQuote(txtPrepBy.Text) & "', " & vbCrLf & " APP_BY='" & MainClass.AllowSingleQuote(txtAppBy.Text) & "', IS_BOP='" & mBOP & "'," & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), DEPTCODE='" & MainClass.AllowSingleQuote(txtDeptCode.Text) & "' " & vbCrLf & " WHERE Mkey ='" & MainClass.AllowSingleQuote(lblMKey.Text) & "'"

        End If
        PubDBCn.Execute(SqlStr)
        If UpdateDetail1 = False Then GoTo ErrPart
        If UpdateAlterDetail = False Then GoTo ErrPart

        If Val(txtAmendNo.Text) > 0 Then
            If UpdatePreviousBOM((txtProductCode.Text), Val(txtAmendNo.Text), "C") = False Then GoTo ErrPart
        End If

        Update1 = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsOutBOMHdr.Requery()
        RsOutBOMDet.Requery()
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
        MainClass.ButtonStatus(Me, XRIGHT, RsOutBOMHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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

    Private Sub SprdMain_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdMain.ButtonClicked
        Call ShowFormOutBOMAlterDetail(eventArgs.Col, eventArgs.Row)
    End Sub

    Private Sub ShowFormOutBOMAlterDetail(ByRef pCol As Integer, ByRef pRow As Integer)

        'Dim i As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        'Dim pDate As String
        Dim mItemCode As String

        With SprdMain
            .Row = pRow
            .Col = ColItemCode
            mItemCode = Trim(.Text)
        End With

        If mItemCode = "" Then Exit Sub
        ConOutBOMDetail = False
        'Me.lblDetail.Text = "False"

        With frmBOMOutAlter
            .LblAddMode.Text = CStr(ADDMode)
            .LblModifyMode.Text = CStr(MODIFYMode)
            .lblMKey.Text = lblMKey.Text
            .lblSerialNo.Text = CStr(pRow)
            .LblItemCode.Text = mItemCode
            .ShowDialog()
        End With

        If ConOutBOMDetail = True Then
            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick

        SprdView.Row = SprdView.ActiveRow

        SprdView.Col = 1
        txtProductCode.Text = Trim(SprdView.Text)

        SprdView.Col = 4
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
        Dim mItemDesc As String
        'Dim mDeleted As Boolean

        If eventArgs.Row = 0 And eventArgs.Col = ColItemCode Then
            With SprdMain
                SqlStr = "SELECT ITEM_CODE,ITEM_SHORT_DESC " & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS = 'A' " & vbCrLf & " ORDER BY ITEM_CODE "
                .Row = .ActiveRow
                .Col = ColItemCode
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColItemCode
                    .Text = AcName

                    .Col = ColItemDesc
                    .Text = AcName1

                    .Col = ColItemCode
                    Call FillGridRow((SprdMain.Text))
                End If
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColItemDesc Then
            With SprdMain
                SqlStr = "SELECT ITEM_SHORT_DESC,ITEM_CODE " & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS = 'A' " & vbCrLf & " ORDER BY ITEM_SHORT_DESC "

                .Row = .ActiveRow

                .Col = ColItemDesc
                mItemDesc = .Text

                .Text = ""
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColItemDesc
                    .Text = AcName

                    .Col = ColItemCode
                    .Text = AcName1
                Else
                    .Row = .ActiveRow

                    .Col = ColItemDesc
                    .Text = mItemDesc
                End If
                .Col = ColItemCode
                Call FillGridRow((SprdMain.Text))
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColStockType Then
            With SprdMain
                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value
                If MainClass.SearchGridMaster("", "INV_TYPE_MST", "STOCK_TYPE_DESC", "STOCK_TYPE_CODE", , , SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColStockType
                    .Text = AcName1
                End If
            End With
        End If

        If eventArgs.Col = 0 And eventArgs.Row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdMain, eventArgs.Row, ColItemCode)
            MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemDesc, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColStockType Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColStockType, 0))
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim mItemCode As String
        Dim mStockType As String = ""

        If eventArgs.NewRow = -1 Then Exit Sub
        SprdMain.Row = eventArgs.Row
        SprdMain.Col = ColItemCode
        If Trim(SprdMain.Text) = "" Then Exit Sub
        Select Case eventArgs.col
            Case ColItemCode
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColItemCode
                mItemCode = Trim(SprdMain.Text)
                If Trim(txtProductCode.Text) = Trim(SprdMain.Text) Then
                    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
                Else
                    If ValidateMainCode(mItemCode) = True Then
                        If CheckDuplicateItem(mItemCode) = False Then
                            SprdMain.Row = SprdMain.ActiveRow
                            SprdMain.Col = ColItemCode
                            Call FillGridRow((SprdMain.Text))
                        Else
                            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
                        End If
                    Else
                        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
                    End If
                End If
            Case ColItemQty
                If CheckQty(SprdMain, eventArgs.col, eventArgs.row) = True Then
                    MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                    FormatSprdMain((SprdMain.MaxRows))
                End If
            Case ColStockType
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColStockType
                mStockType = Trim(SprdMain.Text)
                If mStockType <> "" Then
                    If MainClass.ValidateWithMasterTable(mStockType, "STOCK_TYPE_CODE", "STOCK_TYPE_CODE", "INV_TYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColStockType)
                    End If
                End If
        End Select

        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Function ValidateAlternetCode(ByRef mMainItemCode As String, ByRef mAlterItemCode As String) As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsMisc As ADODB.Recordset = Nothing

        ValidateAlternetCode = True
        If Trim(mMainItemCode) = "" Then Exit Function
        If Trim(mAlterItemCode) = "" Then Exit Function

        ValidateAlternetCode = False

        SqlStr = "SELECT A.ITEM_SHORT_DESC " & vbCrLf & " FROM INV_ITEM_MST A, INV_ITEM_ALTER_DET B " & vbCrLf & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf & " AND A.ITEM_CODE=B.ALTER_ITEM_CODE" & vbCrLf & " AND B.ALTER_ITEM_CODE='" & MainClass.AllowSingleQuote(mAlterItemCode) & "'" & vbCrLf & " AND B.ITEM_CODE='" & MainClass.AllowSingleQuote(mMainItemCode) & "'"

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

    Private Sub txtAmendNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAmendNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtAppBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAppBy.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
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
        Dim SqlStr As String = ""
        If Trim(txtAppBy.Text) = "" Then GoTo EventExitSub
        txtAppBy.Text = VB6.Format(Trim(txtAppBy.Text), "000000")

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        If ADDMode = True Then
            SqlStr = SqlStr & " AND EMP_LEAVE_DATE IS NULL "
        End If

        If MainClass.ValidateWithMasterTable((txtAppBy.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("EMPLOYEE Does Not Exist In Master.")
            Cancel = True
        Else
            lblAppBy.Text = MasterNo
        End If
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
        Dim mRs As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        If Trim(txtCopyProductCode.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtCopyProductCode.Text, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtCopyProductDesc.Text = MasterNo

            SqlStr = " SELECT * FROM PRD_OUTBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote((txtCopyProductCode.Text)) & "' "

            If Val(txtCopyAmendNo.Text) <> 0 Then
                SqlStr = SqlStr & vbCrLf & " AND AMEND_NO='" & Val(txtCopyAmendNo.Text) & "'"
            Else
                SqlStr = SqlStr & vbCrLf & " AND AMEND_NO = (" & vbCrLf & " SELECT MAX(AMEND_NO) AS AMEND_NO " & vbCrLf & " FROM PRD_OUTBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & Trim(txtCopyProductCode.Text) & "') "
            End If

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRs, ADODB.LockTypeEnum.adLockReadOnly)
            If mRs.EOF = False Then
                lblCopyMKey.Text = mRs.Fields("mKey").Value
                MainClass.ClearGrid(SprdMain)
                Call ShowDetail1((lblCopyMKey.Text))
                Call ShowAlterDetail((lblCopyMKey.Text))
            Else
                MsgBox("BOM Not defined for this Product", MsgBoxStyle.Information)
                Cancel = True
                GoTo EventExitSub
            End If
        Else
            MsgBox("Invaild Product Code.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDeptCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDeptCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDeptCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDeptCode.DoubleClick
        Call cmdSearchDept_Click(cmdSearchDept, New System.EventArgs())
    End Sub

    Private Sub txtDeptCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDeptCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDeptCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDeptCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDeptCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchDept_Click(cmdSearchDept, New System.EventArgs())
    End Sub

    Private Sub txtDeptCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDeptCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValEMP
        Dim SqlStr As String = ""
        If Trim(txtDeptCode.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable(txtDeptCode, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Dept Does Not Exist In Master.")
            Cancel = True
        End If
        GoTo EventExitSub
ValEMP:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
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

    Private Sub txtProductCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtProductCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim mRs As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim xMkey As String
        If Trim(txtProductCode.Text) = "" Then GoTo EventExitSub

        SqlStr = " SELECT ITEM_SHORT_DESC,ISSUE_UOM,ITEM_MODEL,CUSTOMER_PART_NO " & " FROM INV_ITEM_MST " & " WHERE ITEM_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "'" & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        ''AND ITEM_STATUS = 'A'

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRs, ADODB.LockTypeEnum.adLockReadOnly)
        If Not mRs.EOF Then
            txtProductDesc.Text = IIf(IsDbNull(mRs.Fields("ITEM_SHORT_DESC").Value), "", mRs.Fields("ITEM_SHORT_DESC").Value)
            txtUnit.Text = IIf(IsDbNull(mRs.Fields("ISSUE_UOM").Value), "", mRs.Fields("ISSUE_UOM").Value)
            txtModelNo.Text = IIf(IsDbNull(mRs.Fields("ITEM_MODEL").Value), "", mRs.Fields("ITEM_MODEL").Value)
            txtCustPartNo.Text = IIf(IsDbNull(mRs.Fields("CUSTOMER_PART_NO").Value), "", mRs.Fields("CUSTOMER_PART_NO").Value)
        Else
            txtProductDesc.Text = ""
            txtUnit.Text = ""
            txtModelNo.Text = ""
            txtCustPartNo.Text = ""
            MsgBox("Invaild Item Code.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

        If ShowRecord = False Then Cancel = True
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
        '    Resume
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPrepBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPrepBy.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
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
        Dim SqlStr As String = ""
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

    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtRemarks.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtWef_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWEF.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtWEF_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtWEF.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchWEF_Click(cmdSearchWEF, New System.EventArgs())
    End Sub

    Private Sub TxtWef_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtWEF.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If Trim(txtWEF.Text) = "" Then GoTo EventExitSub

        If IsDate(txtWEF.Text) = False Then
            MsgBox("Invalid Date")
            Cancel = True
            GoTo EventExitSub
        End If
        If ShowRecord = False Then Cancel = True
EventExitSub:
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
            SqlStr = " SELECT MAX(WEF) AS WEF " & vbCrLf & " FROM PRD_OUTBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE='" & Trim(txtProductCode.Text) & "'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOutBOMHdr, ADODB.LockTypeEnum.adLockReadOnly)
            If RsOutBOMHdr.EOF = True Then
                Exit Function
            End If
        End If

        If MODIFYMode = True And RsOutBOMHdr.EOF = False Then xMkey = RsOutBOMHdr.Fields("mKey").Value
        SqlStr = " SELECT * FROM PRD_OUTBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "' "

        If Trim(txtWEF.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND WEF=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND WEF = (" & vbCrLf & " SELECT MAX(WEF) AS WEF " & vbCrLf & " FROM PRD_OUTBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE='" & Trim(txtProductCode.Text) & "')"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOutBOMHdr, ADODB.LockTypeEnum.adLockReadOnly)
        If RsOutBOMHdr.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("BOM Not Made For This Item. Click Add For New.", MsgBoxStyle.Information)
                ShowRecord = False
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM PRD_OUTBOM_HDR" & vbCrLf & " WHERE UPPER(LTRIM(RTRIM(MKey)))='" & MainClass.AllowSingleQuote(UCase(xMkey)) & "' "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOutBOMHdr, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Sub ShowAlterDetail(ByRef nMkey As String)

        On Error GoTo ShowSerialNoErr
        Dim RsSRLNo As ADODB.Recordset
        Dim SqlStr As String = ""

        Call DelTemp_OutBOMAlterDetail()

        SqlStr = ""

        SqlStr = "INSERT INTO TEMP_PRD_OUTBOM_ALTER_DET ( " & vbCrLf & " USERID, SERIAL_NO, ALTER_SERIAL_NO, " & vbCrLf & " COMPANY_CODE, ITEM_CODE, " & vbCrLf & " ALTER_ITEM_CODE, ALTER_ITEM_UOM, ALTER_STOCK_TYPE, ALTER_ITEM_QTY, ALTER_SCRAP_QTY, ALTER_QTY_VAR) " & vbCrLf & " SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " SERIAL_NO, ALTER_SERIAL_NO, " & vbCrLf & " COMPANY_CODE, ITEM_CODE, " & vbCrLf & " ALTER_ITEM_CODE, ALTER_ITEM_UOM, ALTER_STOCK_TYPE, ALTER_ITEM_QTY, ALTER_SCRAP_QTY, ALTER_QTY_VAR " & vbCrLf & " FROM PRD_OUTBOM_ALTER_DET" & vbCrLf & " WHERE MKEY ='" & MainClass.AllowSingleQuote(nMkey) & "'" & vbCrLf & " ORDER BY SERIAL_NO, ALTER_SERIAL_NO" & vbCrLf
        PubDBCn.Execute(SqlStr)

        Exit Sub
ShowSerialNoErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub DelTemp_OutBOMAlterDetail()

        Dim SqlStr As String = ""

        SqlStr = ""
        SqlStr = "DELETE FROM TEMP_PRD_OUTBOM_ALTER_DET " & "WHERE USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"

        PubDBCn.Execute(SqlStr)
    End Sub

    Private Function UpdateAlterDetail() As Boolean

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

                SqlStr = "INSERT INTO PRD_OUTBOM_ALTER_DET ( " & vbCrLf & " MKEY, SERIAL_NO, ALTER_SERIAL_NO, " & vbCrLf & " COMPANY_CODE, PRODUCT_CODE, ITEM_CODE, " & vbCrLf & " ALTER_ITEM_CODE, ALTER_ITEM_UOM, ALTER_STOCK_TYPE, ALTER_ITEM_QTY, ALTER_SCRAP_QTY, ALTER_QTY_VAR) " & vbCrLf & " SELECT '" & MainClass.AllowSingleQuote(lblMKey.Text) & "', " & vbCrLf & " SERIAL_NO, ALTER_SERIAL_NO, " & vbCrLf & " COMPANY_CODE, " & vbCrLf & " '" & txtProductCode.Text & "', " & vbCrLf & " ITEM_CODE, " & vbCrLf & " ALTER_ITEM_CODE, ALTER_ITEM_UOM, ALTER_STOCK_TYPE, ALTER_ITEM_QTY, ALTER_SCRAP_QTY, ALTER_QTY_VAR " & vbCrLf & " FROM TEMP_PRD_OUTBOM_ALTER_DET" & vbCrLf & " WHERE USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"

                PubDBCn.Execute(SqlStr)
            Next
        End With

        UpdateAlterDetail = True
        Exit Function
UpdateErr1:
        UpdateAlterDetail = False
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

        SqlStr = " SELECT MAX(WEF) AS WEF" & vbCrLf & " FROM PRD_OUTBOM_HDR" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "'" & vbCrLf & " AND AMEND_NO< " & Val(txtAmendNo.Text) & ""

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
End Class
