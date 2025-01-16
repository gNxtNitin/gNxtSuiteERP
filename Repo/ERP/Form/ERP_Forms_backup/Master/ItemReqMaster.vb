Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmItemReqMaster
    Inherits System.Windows.Forms.Form
    Dim RsItemReqMast As ADODB.Recordset

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean
    Dim mItemName As String

    Private Const ConRowHeight As Short = 14

    Private Sub CboExciseFlag_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CboExciseFlag.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub CboExciseFlag_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CboExciseFlag.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub CmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ShowReport(Crystal.DestinationConstants.crptToWindow)
        Me.Cursor = System.Windows.Forms.Cursors.Default 'ItemReqMst
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ShowReport(Crystal.DestinationConstants.crptToPrinter)
        Me.Cursor = System.Windows.Forms.Cursors.Default '
    End Sub
    Private Sub ShowReport(ByRef Mode As Crystal.DestinationConstants)

        Dim mTitle As String = ""
        On Error GoTo ERR1
        Dim mSqlStr As String

        mTitle = ""
        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        mTitle = "Item Requisition Master"


        mSqlStr = "Select * From INV_ITEM_REQ_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND UPPER(LTRIM(RTRIM(ITEM_SHORT_DESC)))='" & MainClass.AllowSingleQuote(UCase(txtItemName.Text)) & "'"


        Report1.SQLQuery = mSqlStr
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\ItemReqMst.rpt"
        SetCrpt(Report1, Mode, 1, mTitle)
        MainClass.AssignCRptFormulas(Report1, "CATEGORY=""" & lblCatName.Text & """")
        MainClass.AssignCRptFormulas(Report1, "SUBCATEGORY=""" & lblSubCatName.Text & """")

        Report1.WindowShowGroupTree = False
        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub txtHSNCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtHSNCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtHSNCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtHSNCode.DoubleClick
        Call cmdSearchHSN_Click(cmdSearchHSN, New System.EventArgs())
    End Sub

    Private Sub txtHSNCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtHSNCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtHSNCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtHSNCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtHSNCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchHSN_Click(cmdSearchHSN, New System.EventArgs())
    End Sub

    Private Sub txtHSNCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtHSNCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtHSNCode.Text) = "" Then lblHSNName.Text = "" : GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtHSNCode.Text), "HSN_CODE", "HSN_DESC", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='G'") = False Then
            ErrorMsg("Invalid HSN Code.", , MsgBoxStyle.Information)
            Cancel = True
        Else
            lblHSNName.Text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub cmdSearchHSN_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchHSN.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='G'"

        If MainClass.SearchGridMaster((txtHSNCode.Text), "GEN_HSN_MST", "HSN_CODE", "HSN_DESC", , , SqlStr) = True Then
            txtHSNCode.Text = AcName
            txtHSNCode_Validating(txtHSNCode, New System.ComponentModel.CancelEventArgs(False))
        End If
        txtHSNCode.Focus()
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub cboGSTClass_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboGSTClass.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboGSTClass_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboGSTClass.SelectedIndexChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub CboItemClass_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CboItemClass.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub CboItemClass_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CboItemClass.SelectedIndexChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboItemClassification_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboItemClassification.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboItemClassification_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboItemClassification.SelectedIndexChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboItemType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CboItemType.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboItemType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CboItemType.SelectedIndexChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboStatus_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CboStatus.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboStatus_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CboStatus.SelectedIndexChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkAutoIndent_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAutoIndent.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkConsumable_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkConsumable.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkDrawing_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkDrawing.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkExportItem_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkExportItem.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkPOReqd_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkPOReqd.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkRequired_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkRequired.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkStockItem_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkStockItem.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        If CmdAdd.Text = ConCmdAddCaption Then
            CmdAdd.Text = ConCmdCancelCaption
            ADDMode = True
            MODIFYMode = False
            Clear1()
            If txtItemName.Enabled = True Then txtItemName.Focus()
        Else
            CmdAdd.Text = ConCmdAddCaption
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
    End Sub

    Private Sub FillComboBox()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        CboItemType.Items.Clear()
        CboItemType.Items.Add("Local")
        CboItemType.Items.Add("Imported")

        CboItemClass.Items.Clear()
        CboItemClass.Items.Add("A Class")
        CboItemClass.Items.Add("B Class")
        CboItemClass.Items.Add("C Class")
        CboItemClass.Items.Add("DOL Class")

        CboExciseFlag.Items.Clear()
        CboExciseFlag.Items.Add("Yes")
        CboExciseFlag.Items.Add("No ")

        cboItemClassification.Items.Clear()

        cboItemClassification.Items.Add("BOP     ")
        cboItemClassification.Items.Add("In House")
        cboItemClassification.Items.Add("Job Work")
        cboItemClassification.Items.Add("Regular ")
        cboItemClassification.Items.Add("Development")
        cboItemClassification.Items.Add("Tool")
        cboItemClassification.Items.Add("Assets")

        CboStatus.Items.Clear()
        CboStatus.Items.Add("Active  ")
        CboStatus.Items.Add("Inactive")

        cboGSTClass.Items.Clear()
        cboGSTClass.Items.Add("0-GST Relevant")
        cboGSTClass.Items.Add("1-Non GST")
        cboGSTClass.Items.Add("2-GST Exempt")

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        On Error Resume Next
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        Dim SqlStr As String = ""

        If txtItemName.Text = "" Then Call ErrorMsg("Nothing to Delete", "", MsgBoxStyle.Critical) : Exit Sub

        If Not RsItemReqMast.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()

                If InsertIntoDelAudit(PubDBCn, "INV_ITEM_REQ_MST", VB.Left(txtItemName.Text, 30), RsItemReqMast, "ITEM_SHORT_DESC") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "INV_ITEM_REQ_MST", "ITEM_SHORT_DESC", RsItemReqMast.Fields("Item_Short_Desc").Value) = False Then GoTo DelErrPart

                SqlStr = " DELETE From INV_ITEM_REQ_MST WHERE " & vbCrLf _
                    & " COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & "" & vbCrLf _
                    & " AND ITEM_SHORT_DESC='" & MainClass.AllowSingleQuote(Trim(RsItemReqMast.Fields("ITEM_SHORT_DESC").Value)) & "'"

                PubDBCn.Execute(SqlStr)

                PubDBCn.CommitTrans()
                RsItemReqMast.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans()
        RsItemReqMast.Requery()
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsItemReqMast, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler

        If FieldVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If Update1() = True Then
            ADDMode = False
            MODIFYMode = False
            SSTInfo.SelectedIndex = 0
            If lblType.Text = "M" Then
                TxtItemName_Validating(txtItemName, New System.ComponentModel.CancelEventArgs(False))
                If CmdAdd.Enabled = True Then CmdAdd.Focus()
            Else
                Clear1()
            End If
        Else
            MsgInformation("Record not saved")
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        Call ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Function Update1() As Boolean
        On Error GoTo UpdateError

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        If UpdateItem() = False Then GoTo UpdateError

        PubDBCn.CommitTrans()
        Update1 = True
        Exit Function
UpdateError:
        Call ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans() ''
        Update1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Private Function UpdateItem() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mAutoIndent As String
        Dim mConsumable As String
        Dim mDrawingAvailable As String

        txtItemName.Text = UCase(txtItemName.Text)

        If ADDMode = True Then
            mItemName = MainClass.AllowSingleQuote(txtItemName.Text)

            SqlStr = ""
            SqlStr = " INSERT INTO INV_ITEM_REQ_MST ( " & vbCrLf _
                & " COMPANY_CODE, ITEM_CODE, SEMI_FIN_ITEM_CODE,  " & vbCrLf _
                & " CATEGORY_CODE, SUBCATEGORY_CODE, ITEM_TYPE,  " & vbCrLf _
                & " ISSUE_UOM, ITEM_EXCISE_FLAG,MINIMUM_QTY, " & vbCrLf _
                & " MAXIMUM_QTY, REORDER_QTY,ECONOMIC_QTY, " & vbCrLf _
                & " ITEM_STATUS , ITEM_STD_COST, ITEM_WEIGHT, " & vbCrLf _
                & " ITEM_SURFACE_AREA,SHEAR_SCRAP_WGT, " & vbCrLf _
                & " ITEM_MAKE,ITEM_COLOR,ITEM_SHORT_DESC, " & vbCrLf _
                & " ITEM_TECH_DESC,ITEM_DIMENSIONS,CUSTOMER_PART_NO," & vbCrLf _
                & " ITEM_GRADE,ITEM_QAS_NO,ITEM_MODEL, " & vbCrLf _
                & " TARIFF_CODE,DRAWING_NO,IDENT_MARK," & vbCrLf _
                & " AUTO_INDENT, PRODTYPE_DESC, PURCHASE_COST,CONSUMABLE_FLAG, " & vbCrLf _
                & " ITEM_CLASSIFICATION,LEAD_TIME,ITEM_CLASS, " & vbCrLf _
                & " ITEM_CLASS_QTY,PURCHASE_UOM,UOM_FACTOR," & vbCrLf _
                & " DRW_REVNO,DRW_REVEFF_DATE,PACK_ITEM_CODE,PACK_STD," & vbCrLf _
                & " DSP_RPT_FLAG, " & vbCrLf _
                & " STOCKITEM, POREQD, IS_EXPORT_ITEM, SCRAP_ITEM_CODE,ITEM_WLENGTH,ITEM_TACKS, " & vbCrLf _
                & " MAT_DESC, MAT_LEN, MAT_WIDTH, " & vbCrLf _
                & " MAT_THICHNESS, MAT_DENSITY, SURFACE_TREATMENT, " & vbCrLf _
                & " ADDUSER, ADDDATE, MODUSER, MODDATE,ITEM_LOCATION,HSN_CODE, GST_ITEMCLASS) VALUES ( "

            SqlStr = SqlStr & vbCrLf _
                & " " & RsCompany.Fields("Company_Code").Value & ", " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtItemCode.Text) & "'," & vbCrLf _
                & " '" & IIf(chkDrawing.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtCatName.Text) & "', "

            SqlStr = SqlStr & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtSubCatName.Text) & "'," & vbCrLf _
                & " '" & VB.Left(CboItemType.Text, 1) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtItemUom.Text) & "', " & vbCrLf _
                & " '" & VB.Left(CboExciseFlag.Text, 1) & "'," & vbCrLf _
                & " '" & Val(txtMinQnty.Text) & "'," & vbCrLf _
                & " '" & Val(txtMaxQnty.Text) & "'," & vbCrLf _
                & " '" & Val(txtReQnty.Text) & "'," & vbCrLf _
                & " '" & Val(txtEcoQnty.Text) & "'," & vbCrLf _
                & " '" & VB.Left(CboStatus.Text, 1) & "',"

            SqlStr = SqlStr & vbCrLf _
                & " '" & Val(txtSaleCost.Text) & "'," & vbCrLf _
                & " '" & Val(txtWeight.Text) & "'," & vbCrLf _
                & " '" & Val(txtSurfaceArea.Text) & "'," & vbCrLf _
                & " '" & Val(txtScrapWeight.Text) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtItemMake.Text) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtColor.Text) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtItemName.Text) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtTechnicalDescription.Text) & "',"

            SqlStr = SqlStr & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtDimention.Text) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtPartNo.Text) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtSpecification.Text) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtInspectionNo.Text) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtModel.Text) & "'," & vbCrLf _
                & " ''," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtDwgNo.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtIdMark.Text) & "'," & vbCrLf _
                & " '" & IIf(chkAutoIndent.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtProdType.Text) & "'," & vbCrLf _
                & " '" & Val(txtPurchaseCost.Text) & "',"

            SqlStr = SqlStr & vbCrLf _
                & " '" & IIf(chkConsumable.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "'," & vbCrLf _
                & " '" & VB.Left(cboItemClassification.Text, 1) & "'," & vbCrLf _
                & " '" & Val(txtLeadTime.Text) & "'," & vbCrLf _
                & " '" & VB.Left(CboItemClass.Text, 1) & "'," & vbCrLf _
                & " '" & Val(txtItemClassQnty.Text) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtPurchaseUom.Text) & "'," & vbCrLf _
                & " '" & Val(txtUOMFactor.Text) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtDwgRevNo.Text) & "'," & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtDwgRevDate.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY')," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtPackingItemCode.Text) & "'," & vbCrLf _
                & " '" & Val(txtPackingStandard.Text) & "', " & vbCrLf _
                & " '" & IIf(chkRequired.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "', " & vbCrLf _
                & " '" & IIf(chkStockItem.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "'," & vbCrLf _
                & " '" & IIf(chkPOReqd.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "'," & vbCrLf _
                & " '" & IIf(chkExportItem.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtScrapItemCode.Text) & "'," & Val(txtWLength.Text) & "," & Val(txtTacks.Text) & ", " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtMaterial.Text) & "', " & Val(txtLength.Text) & ", " & Val(txtWidth.Text) & ", " & vbCrLf _
                & " " & Val(txtThickness.Text) & ", '" & MainClass.AllowSingleQuote(txtDensity.Text) & "', '" & MainClass.AllowSingleQuote(txtSurfaceTreatment.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','','" & Trim(txtLocation.Text) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtHSNCode.Text) & "', '" & VB.Left(cboGSTClass.Text, 1) & "'" & vbCrLf _
                & " )"

        End If

        If MODIFYMode = True Then
            SqlStr = ""

            SqlStr = " UPDATE INV_ITEM_REQ_MST SET  " & vbCrLf & " ITEM_CODE= '" & MainClass.AllowSingleQuote(txtItemCode.Text) & "', " & vbCrLf & " CATEGORY_CODE= '" & MainClass.AllowSingleQuote(txtCatName.Text) & "', " & vbCrLf & " SUBCATEGORY_CODE= '" & MainClass.AllowSingleQuote(txtSubCatName.Text) & "', " & vbCrLf & " ISSUE_UOM= '" & MainClass.AllowSingleQuote(txtItemUom.Text) & "', " & vbCrLf & " ITEM_SHORT_DESC= '" & MainClass.AllowSingleQuote(txtItemName.Text) & "', " & vbCrLf & " ITEM_TECH_DESC= '" & MainClass.AllowSingleQuote(txtTechnicalDescription.Text) & "', " & vbCrLf & " CUSTOMER_PART_NO= '" & MainClass.AllowSingleQuote(txtPartNo.Text) & "', " & vbCrLf & " PURCHASE_UOM= '" & MainClass.AllowSingleQuote(txtPurchaseUom.Text) & "', "

            'ALL NUMERIC COLUMNS
            SqlStr = SqlStr & vbCrLf & " MINIMUM_QTY= '" & Val(txtMinQnty.Text) & "', " & vbCrLf & " MAXIMUM_QTY= '" & Val(txtMaxQnty.Text) & "', " & vbCrLf & " REORDER_QTY= '" & Val(txtReQnty.Text) & "', " & vbCrLf & " ECONOMIC_QTY= '" & Val(txtEcoQnty.Text) & "', " & vbCrLf & " ITEM_STD_COST= '" & Val(txtSaleCost.Text) & "', " & vbCrLf & " ITEM_WEIGHT= '" & Val(txtWeight.Text) & "', " & vbCrLf & " ITEM_SURFACE_AREA= '" & Val(txtSurfaceArea.Text) & "', " & vbCrLf & " SHEAR_SCRAP_WGT= '" & Val(txtScrapWeight.Text) & "', " & vbCrLf & " PURCHASE_COST= '" & Val(txtPurchaseCost.Text) & "', " & vbCrLf & " LEAD_TIME= '" & Val(txtLeadTime.Text) & "', " & vbCrLf & " UOM_FACTOR= '" & Val(txtUOMFactor.Text) & "', " & vbCrLf & " PACK_STD= '" & Val(txtPackingStandard.Text) & "', "

            ' CHECK BOXES & COMBO BOXES
            SqlStr = SqlStr & vbCrLf & " SEMI_FIN_ITEM_CODE= '" & IIf(chkDrawing.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "', " & vbCrLf & " CONSUMABLE_FLAG= '" & IIf(chkConsumable.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "', " & vbCrLf & " AUTO_INDENT= '" & IIf(chkAutoIndent.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "', " & vbCrLf & " ITEM_TYPE= '" & VB.Left(CboItemType.Text, 1) & "', " & vbCrLf & " ITEM_CLASS= '" & VB.Left(CboItemClass.Text, 1) & "', " & vbCrLf & " ITEM_EXCISE_FLAG= '" & VB.Left(CboExciseFlag.Text, 1) & "', " & vbCrLf & " ITEM_CLASSIFICATION= '" & VB.Left(cboItemClassification.Text, 1) & "', " & vbCrLf & " ITEM_STATUS= '" & VB.Left(CboStatus.Text, 1) & "', "

            SqlStr = SqlStr & vbCrLf & " ITEM_MAKE= '" & MainClass.AllowSingleQuote(txtItemMake.Text) & "', " & vbCrLf & " ITEM_COLOR= '" & MainClass.AllowSingleQuote(txtColor.Text) & "', " & vbCrLf & " ITEM_DIMENSIONS= '" & MainClass.AllowSingleQuote(txtDimention.Text) & "', " & vbCrLf & " ITEM_GRADE= '" & MainClass.AllowSingleQuote(txtSpecification.Text) & "', " & vbCrLf & " ITEM_QAS_NO= '" & MainClass.AllowSingleQuote(txtInspectionNo.Text) & "', " & vbCrLf & " ITEM_MODEL= '" & MainClass.AllowSingleQuote(txtModel.Text) & "', " & vbCrLf & " TARIFF_CODE= '', " & vbCrLf & " DRAWING_NO= '" & MainClass.AllowSingleQuote(txtDwgNo.Text) & "', " & vbCrLf & " IDENT_MARK= '" & MainClass.AllowSingleQuote(txtIdMark.Text) & "', " & vbCrLf & " ITEM_CLASS_QTY= '" & MainClass.AllowSingleQuote(txtItemClassQnty.Text) & "', " & vbCrLf & " DRW_REVNO= '" & MainClass.AllowSingleQuote(txtDwgRevNo.Text) & "', " & vbCrLf _
                & " DRW_REVEFF_DATE= TO_DATE('" & VB6.Format(txtDwgRevDate.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY'), " & vbCrLf _
                & " PRODTYPE_DESC= '" & MainClass.AllowSingleQuote(txtProdType.Text) & "', " & vbCrLf & " PACK_ITEM_CODE= '" & MainClass.AllowSingleQuote(txtPackingItemCode.Text) & "', "

            SqlStr = SqlStr & vbCrLf & " DSP_RPT_FLAG= '" & IIf(chkRequired.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "', " & vbCrLf & " STOCKITEM= '" & IIf(chkStockItem.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "', " & vbCrLf & " POREQD= '" & IIf(chkPOReqd.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "', " & vbCrLf & " IS_EXPORT_ITEM= '" & IIf(chkExportItem.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "', " & vbCrLf & " SCRAP_ITEM_CODE='" & MainClass.AllowSingleQuote(txtScrapItemCode.Text) & "', " & vbCrLf & " ITEM_WLENGTH=" & Val(txtWLength.Text) & ",ITEM_TACKS=" & Val(txtTacks.Text) & "," & vbCrLf & " MAT_DESC = '" & MainClass.AllowSingleQuote(txtMaterial.Text) & "', " & vbCrLf & " MAT_LEN = " & Val(txtLength.Text) & ", " & vbCrLf & " MAT_WIDTH = " & Val(txtWidth.Text) & ", " & vbCrLf & " MAT_THICHNESS = " & Val(txtThickness.Text) & ", " & vbCrLf & " MAT_DENSITY = '" & MainClass.AllowSingleQuote(txtDensity.Text) & "', " & vbCrLf & " SURFACE_TREATMENT = '" & MainClass.AllowSingleQuote(txtSurfaceTreatment.Text) & "', ITEM_LOCATION='" & Trim(txtLocation.Text) & "'," & vbCrLf & " HSN_CODE= '" & MainClass.AllowSingleQuote(txtHSNCode.Text) & "', " & vbCrLf & " GST_ITEMCLASS='" & VB.Left(cboGSTClass.Text, 1) & "', " & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')"

            SqlStr = SqlStr & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND UPPER(ITEM_SHORT_DESC) = '" & MainClass.AllowSingleQuote(UCase(lblItemName.Text)) & "'"
        End If
        PubDBCn.Execute(SqlStr)

        Dim RsTemp As ADODB.Recordset
        Dim xCompanyCode As Long

        If lblType.Text = "R" Then

            If CheckConsolidatedMaster("INV_ITEM_MST") = True Then
                SqlStr = "SELECT COMPANY_CODE FROM GEN_COMPANY_MST"
            Else
                SqlStr = "SELECT COMPANY_CODE FROM GEN_COMPANY_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
            End If
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                Do While RsTemp.EOF = False
                    xCompanyCode = RsTemp.Fields("COMPANY_CODE").Value

                    SqlStr = "INSERT INTO INV_ITEM_MST (" & vbCrLf _
                            & "COMPANY_CODE,ITEM_CODE,SEMI_FIN_ITEM_CODE,CATEGORY_CODE,SUBCATEGORY_CODE," & vbCrLf _
                            & "ITEM_TYPE,ISSUE_UOM,ITEM_EXCISE_FLAG,MINIMUM_QTY,MAXIMUM_QTY,REORDER_QTY," & vbCrLf _
                            & "ECONOMIC_QTY,ITEM_STATUS,ITEM_STD_COST,ITEM_WEIGHT,ITEM_SURFACE_AREA,SHEAR_SCRAP_WGT," & vbCrLf _
                            & "ACCOUNT_CODE,ITEM_MAKE,ITEM_COLOR,ITEM_SHORT_DESC,ITEM_TECH_DESC,ITEM_DIMENSIONS," & vbCrLf _
                            & "CUSTOMER_PART_NO,ITEM_GRADE,ITEM_QAS_NO,ITEM_MODEL,TARIFF_CODE,DRAWING_NO,IDENT_MARK," & vbCrLf _
                            & "AUTO_INDENT,PURCHASE_COST,CONSUMABLE_FLAG,ITEM_CLASSIFICATION,LEAD_TIME,ITEM_CLASS," & vbCrLf _
                            & "ITEM_CLASS_QTY,PURCHASE_UOM,UOM_FACTOR,DRW_REVNO,DRW_REVEFF_DATE,PACK_ITEM_CODE," & vbCrLf _
                            & "PACK_STD,DSP_RPT_SEQ,DSP_RPT_FLAG,DDR_TITLE,STOCKITEM,POREQD,SCRAP_ITEM_CODE," & vbCrLf _
                            & "ITEM_WLENGTH,ITEM_TACKS,IS_EXPORT_ITEM,MAT_DESC,MAT_LEN,MAT_WIDTH,MAT_THICHNESS," & vbCrLf _
                            & "MAT_DENSITY,SURFACE_TREATMENT," & vbCrLf _
                            & "ADDUSER,ADDDATE,MODUSER,MODDATE,ITEM_LOCATION,PRODTYPE_DESC, HSN_CODE, GST_ITEMCLASS,ITEM_JW_UOM" & vbCrLf _
                            & ") "

                    SqlStr = SqlStr & vbCrLf & "SELECT" & vbCrLf _
                            & "" & xCompanyCode & ",ITEM_CODE,SEMI_FIN_ITEM_CODE,CATEGORY_CODE,SUBCATEGORY_CODE," & vbCrLf _
                            & "ITEM_TYPE,ISSUE_UOM,ITEM_EXCISE_FLAG,MINIMUM_QTY,MAXIMUM_QTY,REORDER_QTY," & vbCrLf _
                            & "ECONOMIC_QTY,ITEM_STATUS,ITEM_STD_COST,ITEM_WEIGHT,ITEM_SURFACE_AREA,SHEAR_SCRAP_WGT," & vbCrLf _
                            & "ACCOUNT_CODE,ITEM_MAKE,ITEM_COLOR,ITEM_SHORT_DESC,ITEM_TECH_DESC,ITEM_DIMENSIONS," & vbCrLf _
                            & "CUSTOMER_PART_NO,ITEM_GRADE,ITEM_QAS_NO,ITEM_MODEL,TARIFF_CODE,DRAWING_NO,IDENT_MARK," & vbCrLf _
                            & "AUTO_INDENT,PURCHASE_COST,CONSUMABLE_FLAG,ITEM_CLASSIFICATION,LEAD_TIME,ITEM_CLASS," & vbCrLf _
                            & "ITEM_CLASS_QTY,PURCHASE_UOM,UOM_FACTOR,DRW_REVNO,DRW_REVEFF_DATE,PACK_ITEM_CODE," & vbCrLf _
                            & "PACK_STD,DSP_RPT_SEQ,DSP_RPT_FLAG,DDR_TITLE,STOCKITEM,POREQD,SCRAP_ITEM_CODE," & vbCrLf _
                            & "ITEM_WLENGTH,ITEM_TACKS,IS_EXPORT_ITEM,MAT_DESC,MAT_LEN,MAT_WIDTH,MAT_THICHNESS," & vbCrLf _
                            & "MAT_DENSITY,SURFACE_TREATMENT," & vbCrLf _
                            & "ADDUSER,ADDDATE,MODUSER,MODDATE,'" & Trim(txtLocation.Text) & "',PRODTYPE_DESC, HSN_CODE, GST_ITEMCLASS,ISSUE_UOM" & vbCrLf _
                            & "FROM INV_ITEM_REQ_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
                            & " AND ITEM_CODE = '" & MainClass.AllowSingleQuote(txtItemCode.Text) & "'"

                    PubDBCn.Execute(SqlStr)

                    RsTemp.MoveNext()
                Loop
            End If



            SqlStr = "DELETE FROM INV_ITEM_REQ_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND ITEM_CODE = '" & MainClass.AllowSingleQuote(txtItemCode.Text) & "'"

            PubDBCn.Execute(SqlStr)

        End If

        UpdateItem = True
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateItem = False
    End Function

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtItemName.Text), "INV_ITEM_REQ_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr) = True Then
            txtItemName.Text = AcName
            TxtItemName_Validating(txtItemName, New System.ComponentModel.CancelEventArgs(False))
            If txtItemCode.Enabled = True Then txtItemCode.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SearchCode()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtItemCode.Text), "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", , , SqlStr) = True Then
            txtItemCode.Text = AcName
            If txtItemCode.Enabled = True Then txtItemCode.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdSearchCategory_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchCategory.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'"

        If MainClass.SearchGridMaster((txtCatName.Text), "INV_GENERAL_MST", "GEN_CODE", "GEN_DESC", , , SqlStr) = True Then
            txtCatName.Text = AcName
            txtCatName_Validating(txtCatName, New System.ComponentModel.CancelEventArgs(False))
            txtCatName.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdSearchModel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchModel.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value

        If MainClass.SearchGridMaster((txtModel.Text), "GEN_MODEL_MST", "MODEL_DESC", "MODEL_CODE", , , SqlStr) = True Then
            txtModel.Text = AcName
            txtModel_Validating(txtModel, New System.ComponentModel.CancelEventArgs(False))
            txtModel.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdSearchPIC_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchPIC.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtPackingItemCode.Text), "INV_ITEM_REQ_MST", "ITEM_CODE", "ITEM_SHORT_DESC", , , SqlStr) = True Then
            txtPackingItemCode.Text = AcName
            txtPackingItemCode_Validating(txtPackingItemCode, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdSearchPurUom_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchPurUom.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='U'"

        If MainClass.SearchGridMaster((txtPurchaseUom.Text), "INV_GENERAL_MST", "GEN_CODE", "GEN_DESC", , , SqlStr) = True Then
            txtPurchaseUom.Text = AcName
            txtPurchaseUom_Validating(txtPurchaseUom, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdSearchScrap_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchScrap.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtScrapItemCode.Text), "INV_ITEM_REQ_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr) = True Then
            txtScrapItemCode.Text = AcName1
            txtScrapItemCode_Validating(txtScrapItemCode, New System.ComponentModel.CancelEventArgs(False))
        End If

        txtScrapItemCode.Focus()
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdSearchSubCat_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchSubCat.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        If Trim(txtCatName.Text) = "" Then
            MsgInformation("Please Select Category first.")
            txtCatName.Focus()
            Exit Sub
        End If
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & txtCatName.Text & "'"

        If MainClass.SearchGridMaster((txtSubCatName.Text), "INV_SUBCATEGORY_MST", "SUBCATEGORY_CODE", "SUBCATEGORY_DESC", , , SqlStr) = True Then
            txtSubCatName.Text = AcName
            txtSubCatName_Validating(txtSubCatName, New System.ComponentModel.CancelEventArgs(False))
            txtSubCatName.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdSearchUom_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchUom.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='U'"

        If MainClass.SearchGridMaster((txtItemUom.Text), "INV_GENERAL_MST", "GEN_CODE", "GEN_DESC", , , SqlStr) = True Then
            txtItemUom.Text = AcName
            txtItemUom_Validating(txtItemUom, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Private Sub ViewGrid()

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
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
        MainClass.ButtonStatus(Me, XRIGHT, RsItemReqMast, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmItemReqMaster_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = "Select * From INV_ITEM_REQ_MST WHERE 1<>1 Order by ITEM_SHORT_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsItemReqMast, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)

        If lblType.Text = "M" Then
            Me.Text = "Item Requisition Master"
        Else
            Me.Text = "Item Requisition Master (Regularisation)"
        End If

        txtItemCode.Enabled = IIf(lblType.Text = "M", False, True)
        CmdAdd.Visible = IIf(lblType.Text = "M", True, False)

        SetTextLengths()
        Clear1()
        If CmdAdd.Enabled = True Then cmdAdd_Click(CmdAdd, New System.EventArgs())
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = ""

        SqlStr = "SELECT ITEM_CODE,ITEM_SHORT_DESC AS DESCRIPTION, " & vbCrLf _
            & "CUSTOMER_PART_NO AS PART_NO,PURCHASE_UOM AS UNIT " & vbCrLf _
            & "FROM INV_ITEM_REQ_MST " & vbCrLf _
            & "WHERE " & vbCrLf _
            & "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        SqlStr = SqlStr & " ORDER BY ITEM_SHORT_DESC"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmItemReqMaster_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call SetMainFormCordinate(Me)
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        FillComboBox()
        SSTInfo.SelectedIndex = 0

        ADDMode = False
        MODIFYMode = False
        FormActive = False
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Clear1()

        On Error GoTo ClearErr

        mItemName = CStr(-1)
        txtItemName.Text = ""
        txtItemCode.Text = ""
        txtItemCode.Enabled = IIf(lblType.Text = "M", False, True)

        txtCatName.Text = ""
        txtColor.Text = ""
        txtDimention.Text = ""
        txtDwgNo.Text = ""
        txtDwgRevDate.Text = ""
        txtDwgRevNo.Text = ""
        txtEcoQnty.Text = ""
        txtIdMark.Text = ""

        txtInspectionNo.Text = ""
        txtItemClassQnty.Text = ""
        txtItemMake.Text = ""
        txtItemUom.Text = ""
        txtLeadTime.Text = ""
        txtWeight.Text = ""
        txtMaxQnty.Text = ""

        txtMinQnty.Text = ""
        txtModel.Text = ""
        txtPackingItemCode.Text = ""
        txtPackingStandard.Text = ""
        txtPartNo.Text = ""
        txtPurchaseCost.Text = ""
        txtPurchaseUom.Text = ""
        txtReQnty.Text = ""

        txtSaleCost.Text = ""
        txtScrapWeight.Text = ""
        txtSpecification.Text = ""
        txtSubCatName.Text = ""
        txtSurfaceArea.Text = ""
        txtTechnicalDescription.Text = ""
        txtUOMFactor.Text = ""
        txtWLength.Text = ""
        txtTacks.Text = ""
        txtLocation.Text = ""
        txtProdType.Text = ""

        chkConsumable.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkDrawing.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkAutoIndent.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkRequired.CheckState = System.Windows.Forms.CheckState.Unchecked

        chkStockItem.CheckState = System.Windows.Forms.CheckState.Checked
        chkPOReqd.CheckState = System.Windows.Forms.CheckState.Checked
        chkExportItem.CheckState = System.Windows.Forms.CheckState.Unchecked
        txtScrapItemCode.Text = ""
        CboItemType.SelectedIndex = 0
        CboItemClass.SelectedIndex = 0
        CboExciseFlag.SelectedIndex = 0
        cboItemClassification.SelectedIndex = 0
        CboStatus.SelectedIndex = 0

        lblCatName.Text = ""
        lblSubCatName.Text = ""
        lblItemUom.Text = ""
        lblPurUom.Text = ""

        lblPackItemName.Text = ""
        lblScrapItemName.Text = ""

        txtItemUom.Enabled = True
        txtPurchaseUom.Enabled = True
        cmdSearchUom.Enabled = True
        cmdSearchPurUom.Enabled = True

        lblAddUser.Text = ""
        lblAddDate.Text = ""
        lblModUser.Text = ""
        lblModDate.Text = ""

        txtMaterial.Text = ""
        txtLength.Text = ""
        txtWidth.Text = ""
        txtThickness.Text = ""
        txtDensity.Text = ""
        txtSurfaceTreatment.Text = ""

        txtHSNCode.Text = ""
        lblHSNName.Text = ""

        txtHSNCode.Enabled = True
        cmdSearchHSN.Enabled = True



        cboGSTClass.SelectedIndex = 0
        cboGSTClass.Enabled = True

        SSTInfo.SelectedIndex = 0

        Call AutoCompleteSearch("INV_ITEM_REQ_MST", "ITEM_SHORT_DESC", "", txtItemName)
        Call AutoCompleteSearch("INV_ITEM_MST", "ITEM_CODE", "", txtItemCode)

        Call AutoCompleteSearch("INV_GENERAL_MST", "GEN_CODE", "GEN_TYPE='U'", txtItemUom)
        Call AutoCompleteSearch("INV_GENERAL_MST", "GEN_CODE", "GEN_TYPE='U'", txtPurchaseUom)
        Call AutoCompleteSearch("INV_GENERAL_MST", "GEN_DESC", "GEN_TYPE='C'", txtCatName)
        Call AutoCompleteSearch("GEN_HSN_MST", "HSN_CODE", "", txtHSNCode)
        Call AutoCompleteSearch("INV_ITEM_MST", "ITEM_SHORT_DESC", "", txtPackingItemCode)
        Call AutoCompleteSearch("INV_ITEM_MST", "ITEM_SHORT_DESC", "", txtScrapItemCode)
        Call AutoCompleteSearch("GEN_MODEL_MST", "MODEL_DESC", "", txtModel)
        Call AutoCompleteSearch("INV_PRODUCTTYPE_MST", "PRODTYPE_DESC", "", txtProdType)


        MainClass.ButtonStatus(Me, XRIGHT, RsItemReqMast, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 500)
            .set_ColWidth(0, 500)
            .set_ColWidth(1, 1500)
            .set_ColWidth(2, 4500)
            .set_ColWidth(3, 2000)
            .set_ColWidth(4, 800)
            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub

    Private Sub SetTextLengths()

        On Error GoTo ERR1

        txtItemName.MaxLength = RsItemReqMast.Fields("ITEM_SHORT_DESC").DefinedSize
        txtItemCode.MaxLength = RsItemReqMast.Fields("ITEM_CODE").DefinedSize
        txtItemUom.MaxLength = RsItemReqMast.Fields("ISSUE_UOM").DefinedSize
        txtPurchaseUom.MaxLength = RsItemReqMast.Fields("PURCHASE_UOM").DefinedSize
        txtUOMFactor.MaxLength = RsItemReqMast.Fields("UOM_FACTOR").DefinedSize
        txtCatName.MaxLength = MainClass.SetMaxLength("GEN_DESC", "INV_GENERAL_MST", PubDBCn)
        txtSubCatName.MaxLength = MainClass.SetMaxLength("SUBCATEGORY_DESC", "INV_SUBCATEGORY_MST", PubDBCn)
        txtPartNo.MaxLength = RsItemReqMast.Fields("CUSTOMER_PART_NO").DefinedSize
        txtLeadTime.MaxLength = RsItemReqMast.Fields("LEAD_TIME").Precision
        txtItemClassQnty.MaxLength = RsItemReqMast.Fields("ITEM_CLASS_QTY").Precision
        txtPurchaseCost.MaxLength = RsItemReqMast.Fields("PURCHASE_COST").Precision
        txtSaleCost.MaxLength = RsItemReqMast.Fields("ITEM_STD_COST").Precision
        txtMinQnty.MaxLength = RsItemReqMast.Fields("MINIMUM_QTY").Precision
        txtEcoQnty.MaxLength = RsItemReqMast.Fields("ECONOMIC_QTY").Precision
        txtMaxQnty.MaxLength = RsItemReqMast.Fields("MAXIMUM_QTY").Precision
        txtReQnty.MaxLength = RsItemReqMast.Fields("REORDER_QTY").Precision

        txtTechnicalDescription.MaxLength = RsItemReqMast.Fields("ITEM_TECH_DESC").DefinedSize
        txtDwgNo.MaxLength = RsItemReqMast.Fields("DRAWING_NO").DefinedSize
        txtDwgRevNo.MaxLength = RsItemReqMast.Fields("DRW_REVNO").DefinedSize
        txtDwgRevDate.MaxLength = 10
        txtIdMark.MaxLength = RsItemReqMast.Fields("IDENT_MARK").DefinedSize
        txtSurfaceArea.MaxLength = RsItemReqMast.Fields("ITEM_SURFACE_AREA").Precision
        txtModel.MaxLength = RsItemReqMast.Fields("ITEM_MODEL").DefinedSize
        txtItemMake.MaxLength = RsItemReqMast.Fields("ITEM_MAKE").DefinedSize
        txtColor.MaxLength = RsItemReqMast.Fields("ITEM_COLOR").DefinedSize
        txtWeight.MaxLength = RsItemReqMast.Fields("ITEM_WEIGHT").DefinedSize
        txtDimention.MaxLength = RsItemReqMast.Fields("ITEM_DIMENSIONS").DefinedSize
        txtSpecification.MaxLength = RsItemReqMast.Fields("ITEM_QAS_NO").DefinedSize
        txtPackingStandard.MaxLength = RsItemReqMast.Fields("PACK_STD").Precision
        txtScrapItemCode.MaxLength = RsItemReqMast.Fields("SCRAP_ITEM_CODE").Precision

        txtScrapWeight.MaxLength = RsItemReqMast.Fields("SHEAR_SCRAP_WGT").Precision

        txtPackingItemCode.MaxLength = RsItemReqMast.Fields("PACK_ITEM_CODE").DefinedSize
        txtWLength.MaxLength = RsItemReqMast.Fields("ITEM_WLENGTH").Precision
        txtTacks.MaxLength = RsItemReqMast.Fields("ITEM_TACKS").Precision
        txtLocation.MaxLength = RsItemReqMast.Fields("ITEM_LOCATION").DefinedSize

        txtMaterial.MaxLength = RsItemReqMast.Fields("MAT_DESC").DefinedSize
        txtLength.MaxLength = RsItemReqMast.Fields("MAT_LEN").Precision
        txtWidth.MaxLength = RsItemReqMast.Fields("MAT_WIDTH").Precision
        txtThickness.MaxLength = RsItemReqMast.Fields("MAT_THICHNESS").Precision
        txtDensity.MaxLength = RsItemReqMast.Fields("MAT_DENSITY").DefinedSize
        txtSurfaceTreatment.MaxLength = RsItemReqMast.Fields("SURFACE_TREATMENT").DefinedSize
        txtProdType.MaxLength = RsItemReqMast.Fields("PRODTYPE_DESC").DefinedSize

        txtHSNCode.MaxLength = RsItemReqMast.Fields("HSN_CODE").DefinedSize

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Function FieldVarification() As Boolean
        On Error GoTo err_Renamed
        Dim mProdType As String = ""
        FieldVarification = True
        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldVarification = False
            Exit Function
        End If

        If txtItemName.Text = "" Then
            MsgInformation("Item Name is empty. Cannot Save")
            If txtItemName.Enabled = True Then txtItemName.Focus()
            FieldVarification = False
            Exit Function
        End If

        If lblType.Text = "R" Then
            If Trim(txtItemCode.Text) = "" Then
                MsgInformation("Item Code is empty. Cannot Save")
                txtItemCode.Focus()
                FieldVarification = False
                Exit Function
            End If
        End If

        If ADDMode = True Then
            If lblType.Text = "R" Then
                If Len(Trim(txtItemCode.Text)) <> 6 Then
                    MsgInformation("Item Code must be six digit. Cannot Save")
                    txtItemCode.Focus()
                    FieldVarification = False
                    Exit Function
                End If
            End If
        End If

        If Trim(txtItemUom.Text) = "" Then
            MsgInformation("Item UOM is empty. Cannot Save")
            txtItemUom.Focus()
            FieldVarification = False
            Exit Function
        End If

        If Trim(txtPurchaseUom.Text) = "" Then
            MsgInformation("Purchase UOM is empty. Cannot Save")
            txtPurchaseUom.Focus()
            FieldVarification = False
            Exit Function
        End If

        If Trim(txtUOMFactor.Text) = "" Or Val(txtUOMFactor.Text) = 0 Then
            MsgInformation("Factor UOM is empty. Cannot Save")
            txtUOMFactor.Focus()
            FieldVarification = False
            Exit Function
        End If

        If Trim(txtItemUom.Text) = Trim(txtPurchaseUom.Text) Then
            If Val(txtUOMFactor.Text) <> 1 Then
                MsgInformation("Invalid UOM Factor. Cannot Save")
                txtUOMFactor.Focus()
                FieldVarification = False
                Exit Function
            End If
        End If

        If Trim(txtCatName.Text) = "" Then
            MsgInformation("Category Name is empty. Cannot Save")
            txtCatName.Focus()
            FieldVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable((txtCatName.Text), "GEN_CODE", "GEN_DESC", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = False Then
            MsgInformation("Invalid Category Name. Cannot Save")
            txtCatName.Focus()
            FieldVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable((txtCatName.Text), "GEN_CODE", "PRD_TYPE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
            If MasterNo = "T" Then
                cboItemClassification.Text = "Tool"
            ElseIf MasterNo = "A" Then
                cboItemClassification.Text = "Assets"
            End If
        End If

        If Trim(txtSubCatName.Text) = "" Then
            MsgInformation("Sub Category is empty. Cannot Save")
            txtSubCatName.Focus()
            FieldVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable((txtSubCatName.Text), "SUBCATEGORY_CODE", "SUBCATEGORY_DESC", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & txtCatName.Text & "' ") = False Then
            MsgInformation("Invalid Sub Category Name. Cannot Save")
            txtSubCatName.Focus()
            FieldVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable((txtCatName.Text), "GEN_CODE", "PRD_TYPE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
            mProdType = Trim(MasterNo)
        End If

        If mProdType = "P" Or mProdType = "R" Or mProdType = "I" Or mProdType = "B" Then
            If Val(txtWeight.Text) <= 0 Then
                MsgInformation("Please Enter the Weight. Cannot Save.")
                SSTInfo.SelectedIndex = 1
                txtWeight.Focus()
                FieldVarification = False
                Exit Function
            End If

            If Val(txtScrapWeight.Text) <= 0 Then
                MsgInformation("Please Enter the Scrap Weight. Cannot Save.")
                SSTInfo.SelectedIndex = 1
                txtScrapWeight.Focus()
                FieldVarification = False
                Exit Function
            End If

            If Trim(txtScrapItemCode.Text) = "" Then
                MsgInformation("Please Enter the Scrap Item Code. Cannot Save.")
                txtScrapItemCode.Focus()
                FieldVarification = False
                Exit Function
            End If

        End If

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function

    Private Sub frmItemReqMaster_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        Me.Hide()
        Me.Close()
        RsItemReqMast.Close()
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        If eventArgs.row < 1 Then Exit Sub

        SprdView.Col = 2
        SprdView.Row = eventArgs.row
        txtItemName.Text = Trim(SprdView.Text)
        TxtItemName_Validating(txtItemName, New System.ComponentModel.CancelEventArgs(True))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Private Sub txtCatName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCatName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        txtSubCatName.Text = ""
        lblSubCatName.Text = ""
    End Sub

    Private Sub txtCatName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCatName.DoubleClick
        Call cmdSearchCategory_Click(cmdSearchCategory, New System.EventArgs())
    End Sub

    Private Sub txtCatName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCatName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchCategory_Click(cmdSearchCategory, New System.EventArgs())
    End Sub

    Private Sub txtDensity_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDensity.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDensity_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDensity.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDensity.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtItemCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtItemCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemCode.DoubleClick
        Call SearchCode()
    End Sub

    Private Sub txtItemCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItemCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtItemCode.Text, "N")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtItemCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtItemCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchCode()
    End Sub

    Private Sub txtItemCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtItemCode.Text) = "" Then GoTo EventExitSub
        If MODIFYMode = True And RsItemReqMast.EOF = False Then mItemName = RsItemReqMast.Fields("Item_Short_Desc").Value
        '    SqlStr = "Select * From INV_ITEM_REQ_MST " & vbCrLf _
        ''            & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
        ''            & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(txtItemCode.Text) & "'"
        '
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsItemReqMast, adLockReadOnly
        '
        '    If RsItemReqMast.EOF = False Then
        '        ADDMode = False
        '        MODIFYMode = False
        '        Show1
        '    Else
        '        If ADDMode = False And MODIFYMode = False Then
        '            MsgBox "Name Does Not Exist In Master, Click Add To Add In Master", vbInformation
        '            Cancel = True
        '        ElseIf MODIFYMode = True Then
        '            SqlStr = "Select * From INV_ITEM_REQ_MST " & vbCrLf _
        ''                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
        ''                    & " AND ITEM_CODE=" & mItemCode & ""
        '            MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsItemReqMast, adLockReadOnly
        '        End If
        '    End If
        If MainClass.ValidateWithMasterTable((txtItemCode.Text), "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value) = True Then
            MsgInformation("Item Code already exists in Item Master")
            Cancel = True
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCatName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCatName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If Trim(txtCatName.Text) = "" Then lblCatName.Text = "" : GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtCatName.Text), "GEN_CODE", "GEN_DESC", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = False Then
            ErrorMsg("Invalid Category Code.", , MsgBoxStyle.Information)
            Cancel = True
        Else
            lblCatName.Text = MasterNo
        End If

        If MainClass.ValidateWithMasterTable((txtCatName.Text), "GEN_CODE", "PRD_TYPE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
            If MasterNo = "T" Then
                cboItemClassification.Text = "Tool"
            ElseIf MasterNo = "A" Then
                cboItemClassification.Text = "Assets"
            End If
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDwgRevDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDwgRevDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDwgRevDate.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtDwgRevDate.Text) Then
            MsgInformation("Invalid Date.")
            Cancel = True
        End If
        txtDwgRevDate.Text = CDate(txtDwgRevDate.Text).ToString("dd/MM/yyyy")
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtItemUom_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemUom.DoubleClick
        Call cmdSearchUom_Click(cmdSearchUom, New System.EventArgs())
    End Sub

    Private Sub txtItemUom_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtItemUom.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchUom_Click(cmdSearchUom, New System.EventArgs())
    End Sub

    Private Sub txtItemUom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemUom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If Trim(txtItemUom.Text) = "" Then lblItemUom.Text = "" : GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtItemUom.Text), "GEN_CODE", "GEN_DESC", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='U'") = False Then
            ErrorMsg("Invalid UOM.", , MsgBoxStyle.Information)
            Cancel = True
        Else
            lblItemUom.Text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtItemName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtItemName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemName.DoubleClick
        cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub

    Private Sub txtItemName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItemName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtItemName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtItemName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtItemName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub

    Private Sub TxtItemName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtItemName.Text) = "" Then GoTo EventExitSub
        If MODIFYMode = True And RsItemReqMast.EOF = False Then mItemName = RsItemReqMast.Fields("ITEM_SHORT_DESC").Value
        SqlStr = "Select * From INV_ITEM_REQ_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND UPPER(LTRIM(RTRIM(ITEM_SHORT_DESC)))='" & MainClass.AllowSingleQuote(UCase(txtItemName.Text)) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsItemReqMast, ADODB.LockTypeEnum.adLockReadOnly)

        If RsItemReqMast.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Name Does Not Exist In Master, Click Add To Add In Master", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * From INV_ITEM_REQ_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND ITEM_SHORT_DESC='" & mItemName & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsItemReqMast, ADODB.LockTypeEnum.adLockReadOnly)
            End If
            If ADDMode = True Or MODIFYMode = True Then
                If MainClass.ValidateWithMasterTable(txtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value) = True Then
                    MsgInformation("Item Name already exists in Item Master")
                    Cancel = True
                End If
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim mBalOpt As Integer
        Dim mControlBranchCode As Integer
        Dim mLock As Boolean
        Dim mGSTItemClass As String

        Clear1()
        If Not RsItemReqMast.EOF Then

            mItemName = Trim(UCase(IIf(IsDBNull(RsItemReqMast.Fields("ITEM_SHORT_DESC").Value), "", RsItemReqMast.Fields("ITEM_SHORT_DESC").Value)))
            txtItemName.Text = mItemName '' Trim(IIf(IsNull(RsItemReqMast.Fields("ITEM_SHORT_DESC").Value), "", RsItemReqMast.Fields("ITEM_SHORT_DESC").Value))
            lblItemName.Text = mItemName '' Trim(IIf(IsNull(RsItemReqMast.Fields("ITEM_SHORT_DESC").Value), "", RsItemReqMast.Fields("ITEM_SHORT_DESC").Value))
            txtItemCode.Text = Trim(IIf(IsDBNull(RsItemReqMast.Fields("ITEM_CODE").Value), "", RsItemReqMast.Fields("ITEM_CODE").Value))
            '        txtItemCode.Enabled = False

            txtItemUom.Text = Trim(IIf(IsDBNull(RsItemReqMast.Fields("ISSUE_UOM").Value), "", RsItemReqMast.Fields("ISSUE_UOM").Value))
            If MainClass.ValidateWithMasterTable((RsItemReqMast.Fields("ISSUE_UOM").Value), "GEN_CODE", "GEN_DESC", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='U'") = True Then
                lblItemUom.Text = MasterNo
            End If

            txtPurchaseUom.Text = Trim(IIf(IsDBNull(RsItemReqMast.Fields("PURCHASE_UOM").Value), "", RsItemReqMast.Fields("PURCHASE_UOM").Value))
            If MainClass.ValidateWithMasterTable((RsItemReqMast.Fields("PURCHASE_UOM").Value), "GEN_CODE", "GEN_DESC", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='U'") = True Then
                lblPurUom.Text = MasterNo
            End If

            txtUOMFactor.Text = Trim(IIf(IsDBNull(RsItemReqMast.Fields("UOM_FACTOR").Value), "", RsItemReqMast.Fields("UOM_FACTOR").Value))

            txtCatName.Text = Trim(IIf(IsDBNull(RsItemReqMast.Fields("CATEGORY_CODE").Value), "", RsItemReqMast.Fields("CATEGORY_CODE").Value))
            If MainClass.ValidateWithMasterTable((RsItemReqMast.Fields("CATEGORY_CODE").Value), "GEN_CODE", "GEN_DESC", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                lblCatName.Text = MasterNo
            End If

            txtSubCatName.Text = Trim(IIf(IsDBNull(RsItemReqMast.Fields("SUBCATEGORY_CODE").Value), "", RsItemReqMast.Fields("SUBCATEGORY_CODE").Value))
            If MainClass.ValidateWithMasterTable((RsItemReqMast.Fields("SUBCATEGORY_CODE").Value), "SUBCATEGORY_CODE", "SUBCATEGORY_DESC", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & txtCatName.Text & "' ") = True Then
                lblSubCatName.Text = MasterNo
            End If


            txtPackingItemCode.Text = Trim(IIf(IsDBNull(RsItemReqMast.Fields("PACK_ITEM_CODE").Value), "", RsItemReqMast.Fields("PACK_ITEM_CODE").Value))
            If Not IsDBNull(RsItemReqMast.Fields("PACK_ITEM_CODE").Value) Then
                If MainClass.ValidateWithMasterTable((RsItemReqMast.Fields("PACK_ITEM_CODE").Value), "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value) = True Then
                    lblPackItemName.Text = MasterNo
                End If
            End If

            txtScrapItemCode.Text = Trim(IIf(IsDBNull(RsItemReqMast.Fields("SCRAP_ITEM_CODE").Value), "", RsItemReqMast.Fields("SCRAP_ITEM_CODE").Value))
            If Not IsDBNull(RsItemReqMast.Fields("SCRAP_ITEM_CODE").Value) Then
                If MainClass.ValidateWithMasterTable(Trim(RsItemReqMast.Fields("SCRAP_ITEM_CODE").Value), "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value) = True Then
                    lblScrapItemName.Text = MasterNo
                End If
            End If

            txtPartNo.Text = Trim(IIf(IsDBNull(RsItemReqMast.Fields("CUSTOMER_PART_NO").Value), "", RsItemReqMast.Fields("CUSTOMER_PART_NO").Value))
            txtLeadTime.Text = Trim(IIf(IsDBNull(RsItemReqMast.Fields("LEAD_TIME").Value), "", RsItemReqMast.Fields("LEAD_TIME").Value))
            txtItemClassQnty.Text = Trim(IIf(IsDBNull(RsItemReqMast.Fields("ITEM_CLASS_QTY").Value), "", RsItemReqMast.Fields("ITEM_CLASS_QTY").Value))
            txtPurchaseCost.Text = Trim(IIf(IsDBNull(RsItemReqMast.Fields("PURCHASE_COST").Value), "", RsItemReqMast.Fields("PURCHASE_COST").Value))
            txtSaleCost.Text = Trim(IIf(IsDBNull(RsItemReqMast.Fields("ITEM_STD_COST").Value), "", RsItemReqMast.Fields("ITEM_STD_COST").Value))
            txtMinQnty.Text = Trim(IIf(IsDBNull(RsItemReqMast.Fields("MINIMUM_QTY").Value), "", RsItemReqMast.Fields("MINIMUM_QTY").Value))
            txtEcoQnty.Text = Trim(IIf(IsDBNull(RsItemReqMast.Fields("ECONOMIC_QTY").Value), "", RsItemReqMast.Fields("ECONOMIC_QTY").Value))
            txtMaxQnty.Text = Trim(IIf(IsDBNull(RsItemReqMast.Fields("MAXIMUM_QTY").Value), "", RsItemReqMast.Fields("MAXIMUM_QTY").Value))
            txtReQnty.Text = Trim(IIf(IsDBNull(RsItemReqMast.Fields("REORDER_QTY").Value), "", RsItemReqMast.Fields("REORDER_QTY").Value))
            txtTechnicalDescription.Text = Trim(IIf(IsDBNull(RsItemReqMast.Fields("ITEM_TECH_DESC").Value), "", RsItemReqMast.Fields("ITEM_TECH_DESC").Value))
            txtDwgNo.Text = Trim(IIf(IsDBNull(RsItemReqMast.Fields("DRAWING_NO").Value), "", RsItemReqMast.Fields("DRAWING_NO").Value))
            txtDwgRevNo.Text = Trim(IIf(IsDBNull(RsItemReqMast.Fields("DRW_REVNO").Value), "", RsItemReqMast.Fields("DRW_REVNO").Value))
            txtDwgRevDate.Text = Trim(IIf(IsDBNull(RsItemReqMast.Fields("DRW_REVEFF_DATE").Value), "", RsItemReqMast.Fields("DRW_REVEFF_DATE").Value))
            txtInspectionNo.Text = Trim(IIf(IsDBNull(RsItemReqMast.Fields("ITEM_QAS_NO").Value), "", RsItemReqMast.Fields("ITEM_QAS_NO").Value))
            txtIdMark.Text = Trim(IIf(IsDBNull(RsItemReqMast.Fields("IDENT_MARK").Value), "", RsItemReqMast.Fields("IDENT_MARK").Value))
            txtSurfaceArea.Text = Trim(IIf(IsDBNull(RsItemReqMast.Fields("ITEM_SURFACE_AREA").Value), "", RsItemReqMast.Fields("ITEM_SURFACE_AREA").Value))
            txtModel.Text = Trim(IIf(IsDBNull(RsItemReqMast.Fields("ITEM_MODEL").Value), "", RsItemReqMast.Fields("ITEM_MODEL").Value))
            txtItemMake.Text = Trim(IIf(IsDBNull(RsItemReqMast.Fields("ITEM_MAKE").Value), "", RsItemReqMast.Fields("ITEM_MAKE").Value))
            txtColor.Text = Trim(IIf(IsDBNull(RsItemReqMast.Fields("ITEM_COLOR").Value), "", RsItemReqMast.Fields("ITEM_COLOR").Value))
            txtWeight.Text = Trim(IIf(IsDBNull(RsItemReqMast.Fields("ITEM_WEIGHT").Value), "", RsItemReqMast.Fields("ITEM_WEIGHT").Value))
            txtDimention.Text = Trim(IIf(IsDBNull(RsItemReqMast.Fields("ITEM_DIMENSIONS").Value), "", RsItemReqMast.Fields("ITEM_DIMENSIONS").Value))

            txtPackingStandard.Text = Trim(IIf(IsDBNull(RsItemReqMast.Fields("PACK_STD").Value), "", RsItemReqMast.Fields("PACK_STD").Value))
            txtScrapWeight.Text = Trim(IIf(IsDBNull(RsItemReqMast.Fields("SHEAR_SCRAP_WGT").Value), "", RsItemReqMast.Fields("SHEAR_SCRAP_WGT").Value))

            chkConsumable.CheckState = IIf(Trim(RsItemReqMast.Fields("CONSUMABLE_FLAG").Value) = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkDrawing.CheckState = IIf(Trim(RsItemReqMast.Fields("SEMI_FIN_ITEM_CODE").Value) = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkAutoIndent.CheckState = IIf((RsItemReqMast.Fields("AUTO_INDENT").Value = "Y" Or IsDBNull(RsItemReqMast.Fields("AUTO_INDENT").Value)), System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            chkStockItem.CheckState = IIf(Trim(RsItemReqMast.Fields("STOCKITEM").Value) = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkPOReqd.CheckState = IIf(Trim(RsItemReqMast.Fields("POREQD").Value) = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkExportItem.CheckState = IIf(Trim(RsItemReqMast.Fields("IS_EXPORT_ITEM").Value) = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            chkRequired.CheckState = IIf((RsItemReqMast.Fields("DSP_RPT_FLAG").Value = "Y"), System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            txtWLength.Text = Trim(IIf(IsDBNull(RsItemReqMast.Fields("ITEM_WLENGTH").Value), "", RsItemReqMast.Fields("ITEM_WLENGTH").Value))
            txtTacks.Text = Trim(IIf(IsDBNull(RsItemReqMast.Fields("ITEM_TACKS").Value), "", RsItemReqMast.Fields("ITEM_TACKS").Value))
            txtLocation.Text = Trim(IIf(IsDBNull(RsItemReqMast.Fields("ITEM_LOCATION").Value), "", RsItemReqMast.Fields("ITEM_LOCATION").Value))

            txtProdType.Text = Trim(IIf(IsDBNull(RsItemReqMast.Fields("PRODTYPE_DESC").Value), "", RsItemReqMast.Fields("PRODTYPE_DESC").Value))

            If IIf(IsDBNull(RsItemReqMast.Fields("ITEM_TYPE").Value), "I", RsItemReqMast.Fields("ITEM_TYPE").Value) = "I" Then
                CboItemType.SelectedIndex = 1
            Else
                CboItemType.SelectedIndex = 0
            End If

            Select Case RsItemReqMast.Fields("ITEM_CLASS").Value
                Case "A"
                    CboItemClass.SelectedIndex = 0
                Case "B"
                    CboItemClass.SelectedIndex = 1
                Case "C"
                    CboItemClass.SelectedIndex = 2
                Case "D"
                    CboItemClass.SelectedIndex = 3
            End Select

            Select Case RsItemReqMast.Fields("ITEM_EXCISE_FLAG").Value
                Case "Y"
                    CboExciseFlag.SelectedIndex = 0
                Case "N"
                    CboExciseFlag.SelectedIndex = 1
            End Select

            Select Case RsItemReqMast.Fields("ITEM_CLASSIFICATION").Value
                Case "B"
                    cboItemClassification.SelectedIndex = 0
                Case "I"
                    cboItemClassification.SelectedIndex = 1
                Case "J"
                    cboItemClassification.SelectedIndex = 2
                Case "R"
                    cboItemClassification.SelectedIndex = 3
                Case "D"
                    cboItemClassification.SelectedIndex = 4
                Case "T"
                    cboItemClassification.SelectedIndex = 5
                Case "A"
                    cboItemClassification.SelectedIndex = 6
            End Select

            Select Case RsItemReqMast.Fields("ITEM_STATUS").Value
                Case "A"
                    CboStatus.SelectedIndex = 0
                Case "I"
                    CboStatus.SelectedIndex = 1
            End Select

            txtHSNCode.Text = Trim(IIf(IsDBNull(RsItemReqMast.Fields("HSN_CODE").Value), "", RsItemReqMast.Fields("HSN_CODE").Value))
            If Not IsDBNull(RsItemReqMast.Fields("HSN_CODE").Value) Then
                If MainClass.ValidateWithMasterTable((RsItemReqMast.Fields("HSN_CODE").Value), "HSN_CODE", "HSN_DESC", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='G'") = True Then
                    lblHSNName.Text = MasterNo
                    txtHSNCode.Enabled = False
                    cmdSearchHSN.Enabled = False
                    cboGSTClass.Enabled = False
                End If
            End If

            mGSTItemClass = IIf(IsDBNull(RsItemReqMast.Fields("GST_ITEMCLASS").Value), 0, RsItemReqMast.Fields("GST_ITEMCLASS").Value)
            cboGSTClass.SelectedIndex = mGSTItemClass

            '        txtItemUom.Enabled = IIf(PubSuperUser = "S" OR PubSuperUser = "A", True, False)
            '        txtPurchaseUom.Enabled = IIf(PubSuperUser = "S" OR PubSuperUser = "A", True, False)
            '        cmdSearchUom.Enabled = IIf(PubSuperUser = "S" OR PubSuperUser = "A", True, False)
            '        cmdSearchPurUom.Enabled = IIf(PubSuperUser = "S" OR PubSuperUser = "A", True, False)

            txtMaterial.Text = Trim(IIf(IsDBNull(RsItemReqMast.Fields("MAT_DESC").Value), "", RsItemReqMast.Fields("MAT_DESC").Value))
            txtLength.Text = Trim(IIf(IsDBNull(RsItemReqMast.Fields("MAT_LEN").Value), "", RsItemReqMast.Fields("MAT_LEN").Value))
            txtWidth.Text = Trim(IIf(IsDBNull(RsItemReqMast.Fields("MAT_WIDTH").Value), "", RsItemReqMast.Fields("MAT_WIDTH").Value))
            txtThickness.Text = Trim(IIf(IsDBNull(RsItemReqMast.Fields("MAT_THICHNESS").Value), "", RsItemReqMast.Fields("MAT_THICHNESS").Value))
            txtDensity.Text = Trim(IIf(IsDBNull(RsItemReqMast.Fields("MAT_DENSITY").Value), "", RsItemReqMast.Fields("MAT_DENSITY").Value))
            txtSurfaceTreatment.Text = Trim(IIf(IsDBNull(RsItemReqMast.Fields("SURFACE_TREATMENT").Value), "", RsItemReqMast.Fields("SURFACE_TREATMENT").Value))

            lblAddUser.Text = IIf(IsDBNull(RsItemReqMast.Fields("ADDUSER").Value), "", RsItemReqMast.Fields("ADDUSER").Value)
            lblAddDate.Text = IIf(IsDBNull(RsItemReqMast.Fields("ADDDATE").Value), "", VB6.Format(RsItemReqMast.Fields("ADDDATE").Value, "dd/MM/yyyy"))
            lblModUser.Text = IIf(IsDBNull(RsItemReqMast.Fields("MODUSER").Value), "", RsItemReqMast.Fields("MODUSER").Value)
            lblModDate.Text = IIf(IsDBNull(RsItemReqMast.Fields("MODDATE").Value), "", VB6.Format(RsItemReqMast.Fields("MODDATE").Value, "dd/MM/yyyy"))

            mLock = GetItemTransaction()

            txtItemName.ReadOnly = mLock

        End If
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsItemReqMast, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtLength_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLength.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLength_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLength.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtLocation_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLocation.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLocation_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLocation.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtLocation.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtMaterial_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMaterial.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMaterial_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMaterial.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtMaterial.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtModel_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtModel.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtModel_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtModel.DoubleClick
        Call cmdSearchModel_Click(cmdSearchModel, New System.EventArgs())
    End Sub

    Private Sub txtModel_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtModel.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtModel.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtModel_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtModel.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchModel_Click(cmdSearchModel, New System.EventArgs())
    End Sub

    Private Sub txtModel_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtModel.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtModel.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtModel.Text), "MODEL_DESC", "MODEL_CODE", "GEN_MODEL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value) = False Then
            ErrorMsg("Invalid MODEL .", , MsgBoxStyle.Information)
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPackingItemCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPackingItemCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPackingItemCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPackingItemCode.DoubleClick
        Call cmdSearchPIC_Click(cmdSearchPIC, New System.EventArgs())
    End Sub

    Private Sub txtPackingItemCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPackingItemCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPackingItemCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPackingItemCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPackingItemCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchPIC_Click(cmdSearchPIC, New System.EventArgs())
    End Sub

    Private Sub txtPackingItemCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPackingItemCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtPackingItemCode.Text) = "" Then lblPackItemName.Text = "" : GoTo EventExitSub
        If MainClass.ValidateWithMasterTable((txtPackingItemCode.Text), "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_REQ_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value) = False Then
            ErrorMsg("Invalid ITEM Code.", , MsgBoxStyle.Information)
            Cancel = True
        Else
            lblPackItemName.Text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPackingStandard_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPackingStandard.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPackingStandard_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPackingStandard.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPartNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPartNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPartNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPartNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPartNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtProdType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProdType.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtProdType_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProdType.DoubleClick
        Call cmdSearchProdType_Click(cmdSearchProdType, New System.EventArgs())
    End Sub


    Private Sub txtProdType_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtProdType.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtProdType.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtProdType_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtProdType.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchProdType_Click(cmdSearchProdType, New System.EventArgs())
    End Sub


    Private Sub txtProdType_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtProdType.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtProdType.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtProdType.Text), "PRODTYPE_DESC", "UOM", "INV_PRODUCTTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value) = False Then
            ErrorMsg("Invalid Product Type .", , MsgBoxStyle.Information)
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub cmdSearchProdType_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchProdType.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value

        If MainClass.SearchGridMaster((txtProdType.Text), "INV_PRODUCTTYPE_MST", "PRODTYPE_DESC", "UOM", , , SqlStr) = True Then
            txtProdType.Text = AcName
            txtProdType_Validating(txtProdType, New System.ComponentModel.CancelEventArgs(False))
            txtProdType.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Sub txtPurchaseCost_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPurchaseCost.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPurchaseCost_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPurchaseCost.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPurchaseUom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPurchaseUom.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        lblPurUom.Text = ""
    End Sub

    Private Sub txtPurchaseUom_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPurchaseUom.DoubleClick
        Call cmdSearchPurUom_Click(cmdSearchPurUom, New System.EventArgs())
    End Sub

    Private Sub txtPurchaseUom_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPurchaseUom.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPurchaseUom.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPurchaseUom_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPurchaseUom.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchPurUom_Click(cmdSearchPurUom, New System.EventArgs())
    End Sub

    Private Sub txtPurchaseUom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPurchaseUom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtPurchaseUom.Text) = "" Then lblPurUom.Text = "" : GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtPurchaseUom.Text), "GEN_CODE", "GEN_DESC", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='U'") = False Then
            ErrorMsg("Invalid Purchase UOM.", , MsgBoxStyle.Information)
            Cancel = True
        Else
            lblPurUom.Text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtReQnty_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtReQnty.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtReQnty_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtReQnty.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSaleCost_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSaleCost.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSaleCost_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSaleCost.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtScrapItemCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtScrapItemCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtScrapItemCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtScrapItemCode.DoubleClick
        Call cmdSearchScrap_Click(cmdSearchScrap, New System.EventArgs())
    End Sub

    Private Sub txtScrapItemCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtScrapItemCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtScrapItemCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtScrapItemCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtScrapItemCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchScrap_Click(cmdSearchScrap, New System.EventArgs())
    End Sub

    Private Sub txtScrapItemCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtScrapItemCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtScrapItemCode.Text) = "" Then lblScrapItemName.Text = "" : GoTo EventExitSub
        If MainClass.ValidateWithMasterTable((txtScrapItemCode.Text), "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value) = False Then
            ErrorMsg("Invalid ITEM Code.", , MsgBoxStyle.Information)
            Cancel = True
        Else
            lblScrapItemName.Text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtScrapWeight_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtScrapWeight.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtScrapWeight_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtScrapWeight.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSurfaceTreatment_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSurfaceTreatment.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSurfaceTreatment_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSurfaceTreatment.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSurfaceTreatment.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtThickness_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtThickness.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtThickness_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtThickness.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtWidth_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWidth.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtWidth_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtWidth.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtWLength_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWLength.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTacks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTacks.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSpecification_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSpecification.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSpecification_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSpecification.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSpecification.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSubCatName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSubCatName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSubCatName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSubCatName.DoubleClick
        Call cmdSearchSubCat_Click(cmdSearchSubCat, New System.EventArgs())
    End Sub

    Private Sub txtSubCatName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSubCatName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSubCatName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSubCatName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSubCatName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchSubCat_Click(cmdSearchSubCat, New System.EventArgs())
    End Sub

    Private Sub txtSubCatName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSubCatName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If Trim(txtSubCatName.Text) = "" Then lblSubCatName.Text = "" : GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtSubCatName.Text), "SUBCATEGORY_CODE", "SUBCATEGORY_DESC", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & txtCatName.Text & "' ") = False Then
            ErrorMsg("Invalid Sub Category Code.", , MsgBoxStyle.Information)
            Cancel = True
        Else
            lblSubCatName.Text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSurfaceArea_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSurfaceArea.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSurfaceArea_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSurfaceArea.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtTechnicalDescription_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTechnicalDescription.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTechnicalDescription_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTechnicalDescription.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtTechnicalDescription.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtUomFactor_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtUOMFactor.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtUomFactor_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtUOMFactor.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtColor_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtColor.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtColor_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtColor.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtColor.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDimention_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDimention.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDimention_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDimention.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDimention.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDwgNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDwgNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDwgNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDwgNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDwgNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDwgRevDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDwgRevDate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDwgRevDate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDwgRevDate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDwgRevDate.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDwgRevNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDwgRevNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDwgRevNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDwgRevNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDwgRevNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEcoQnty_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEcoQnty.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtEcoQnty_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEcoQnty.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtIdMark_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtIdMark.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtIdMark_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtIdMark.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtIdMark.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtInspectionNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInspectionNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtInspectionNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtInspectionNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtInspectionNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtItemClassQnty_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemClassQnty.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtItemClassQnty_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItemClassQnty.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtItemMake_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemMake.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtItemMake_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItemMake.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtItemMake.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtItemUom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemUom.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        lblItemUom.Text = ""
    End Sub

    Private Sub txtItemUom_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItemUom.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtItemUom.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtLeadTime_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLeadTime.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLeadTime_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLeadTime.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtWeight_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWeight.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtWeight_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtWeight.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtWLength_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtWLength.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTacks_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTacks.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtMaxQnty_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMaxQnty.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMaxQnty_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMaxQnty.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtMinQnty_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMinQnty.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMinQnty_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMinQnty.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Function GetItemTransaction() As Boolean

        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mTableName As String

        GetItemTransaction = False

        mTableName = ConInventoryTable

        '    If RsCompany.fields("COMPANY_CODE").value = 1 Then
        '        mTableName = "INV_STOCK_REC_TRN" & RsCompany.fields("FYEAR").value
        '    ElseIf RsCompany.fields("COMPANY_CODE").value = 3 Or RsCompany.fields("COMPANY_CODE").value = 10 Or RsCompany.fields("COMPANY_CODE").value = 12 Then
        '        mTableName = "INV_STOCK_REC_TRN" & VB6.Format(RsCompany.fields("COMPANY_CODE").value, "00") & RsCompany.fields("FYEAR").value
        '    Else
        '        mTableName = "INV_STOCK_REC_TRN"
        '    End If

        SqlStr = " SELECT COUNT(ITEM_CODE) AS CNTREC" & vbCrLf _
            & " FROM " & mTableName & " " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND ITEM_CODE='" & MainClass.AllowSingleQuote((txtItemCode.Text)) & "'"

        SqlStr = SqlStr & vbCrLf & " AND STATUS='O'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)

        If RsTemp.EOF = False Then
            GetItemTransaction = IIf(RsTemp.Fields("CntRec").Value = 0, False, True)
        End If

        SqlStr = " SELECT COUNT(ITEM_CODE) AS CNTREC" & vbCrLf _
            & " FROM PUR_PURCHASE_DET " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(txtItemCode.Text) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)

        If RsTemp.EOF = False Then
            GetItemTransaction = IIf(RsTemp.Fields("CntRec").Value = 0, GetItemTransaction, True)
        End If

        Exit Function
ErrPart:
        GetItemTransaction = False
    End Function
End Class
