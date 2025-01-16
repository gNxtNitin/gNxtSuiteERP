Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports AxFPSpreadADO
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinDataSource
'Imports Infragistics.Win.UltraWinTabControl
Imports System.Data.OleDb

Friend Class frmItemMaster
    Inherits System.Windows.Forms.Form
    Dim RsItemMast As ADODB.Recordset = Nothing ''ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection

    ''Dim RsOpOuts As ADODB.Recordset

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean
    Dim mItemCode As String

    Private Const ColCompanyCode As Short = 1
    Private Const ColCompanyName As Short = 2
    Private Const ColAutoQC As Short = 3
    Private Const ColMaxQty As Short = 4
    Private Const ColMinQty As Short = 5
    Private Const ColInvDays As Short = 6
    Private Const ColReorderQty As Short = 7


    Private Const ConRowHeight As Short = 14

    Private Sub FormatSprdMain(ByRef Arow As Integer)
        On Error GoTo ErrPart
        Dim CntCol As Long

        With SprdMain
            .set_RowHeight(Arow, ConRowHeight)
            .Row = Arow

            .Col = ColCompanyCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditLen = 60
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(.Col, 6)

            .Col = ColCompanyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditLen = 60
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(.Col, 45)

            .Col = ColAutoQC
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter ''SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColAutoQC, 6)
            .Value = CStr(System.Windows.Forms.CheckState.Unchecked)

            For CntCol = ColMaxQty To ColReorderQty
                .Col = CntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.99")
                .TypeFloatMin = CDbl("-999999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(.Col, 8.5)
                .Value = CStr(System.Windows.Forms.CheckState.Unchecked)
            Next

            MainClass.ProtectCell(SprdMain, 1, .MaxRows, ColCompanyCode, ColCompanyName)
            MainClass.SetSpreadColor(SprdMain, 1)
        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub CboExciseFlag_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CboExciseFlag.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub CboExciseFlag_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CboExciseFlag.SelectedIndexChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
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

    Private Sub cboPressLine_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboPressLine.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboPressLine_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboPressLine.SelectedIndexChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboStatus_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CboStatus.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboStatus_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CboStatus.SelectedIndexChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboSurfaceTreatment_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboSurfaceTreatment.SelectedIndexChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkAutoQC_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAutoQC.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkHeatReq_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkHeatReq.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboWeldingLine_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboWeldingLine.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboWeldingLine_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboWeldingLine.SelectedIndexChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkAutoIssue_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAutoIssue.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkChildItem_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkChildItem.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        txtParentItemName.Enabled = IIf(chkChildItem.CheckState = System.Windows.Forms.CheckState.Checked, True, False)
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

    Private Sub chkGrinding_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkGrinding.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkPOReqd_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkPOReqd.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkRequired_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkRequired.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkSB_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkSB.CheckStateChanged

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

        'Dim RS As ADODB.Recordset
        'Dim CntLst As Long

        'Dim oledbCnn As OleDbConnection
        'Dim oledbAdapter As OleDbDataAdapter
        'Dim ds As New DataSet

        'oledbCnn = New OleDbConnection(StrConn)

        cboSurfaceTreatment.Items.Clear()
        cboSurfaceTreatment.Items.Add("0. None")
        cboSurfaceTreatment.Items.Add("1. Painted")
        cboSurfaceTreatment.Items.Add("2. Powder Coated")
        cboSurfaceTreatment.Items.Add("3. Nickel Plated")
        cboSurfaceTreatment.Items.Add("4. Painted/Powder Coated")
        cboSurfaceTreatment.Items.Add("5. Zinc Plated")
        cboSurfaceTreatment.SelectedIndex = 0

        cboWeldingLine.Items.Clear()
        cboWeldingLine.Items.Add("0. None")
        If RsCompany.Fields("COMPANY_CODE").Value = 10 Then
            cboWeldingLine.Items.Add("1. TVSM Weldshop")
            cboWeldingLine.Items.Add("2. RE Weldshop")
            cboWeldingLine.Items.Add("3. H/E Weldshop")
            cboWeldingLine.Items.Add("4. Chain Case")
            cboWeldingLine.Items.Add("5. BMW Weldshop")
        ElseIf RsCompany.Fields("COMPANY_CODE").Value = 3 Then
            cboWeldingLine.Items.Add("1. TVSM WELD")
            cboWeldingLine.Items.Add("2. RE Frame")
            cboWeldingLine.Items.Add("3. RE Weldshop")
            cboWeldingLine.Items.Add("4. Chain Case")
            cboWeldingLine.Items.Add("5. BMW Weldshop")
            cboWeldingLine.Items.Add("6. TATA Weldshop")
        ElseIf RsCompany.Fields("COMPANY_CODE").Value = 32 Then
            cboWeldingLine.Items.Add("1. HERO WELD")
            cboWeldingLine.Items.Add("2. H/E WELD")
            cboWeldingLine.Items.Add("3. RE Weldshop")
            cboWeldingLine.Items.Add("4. Chain Case")
            cboWeldingLine.Items.Add("5. BMW Weldshop")
        Else
            cboWeldingLine.Items.Add("1. MIG")
            cboWeldingLine.Items.Add("2. Frame")
            cboWeldingLine.Items.Add("3. Handle")
            cboWeldingLine.Items.Add("4. Chain Case")
            cboWeldingLine.Items.Add("5. BMW Weldshop")
        End If

        cboWeldingLine.SelectedIndex = 0


        cboPressLine.Items.Clear()
        cboPressLine.Items.Add("0. None")
        cboPressLine.Items.Add("1. Press Shop")
        cboPressLine.Items.Add("2. H/E Press Shop")

        cboPressLine.SelectedIndex = 0

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
        cboItemClassification.Items.Add("SPD")
        cboItemClassification.Items.Add("1. MIG Wire")
        cboItemClassification.Items.Add("2. CO2")
        cboItemClassification.Items.Add("3. Diesel")
        cboItemClassification.Items.Add("4. Paint")
        cboItemClassification.Items.Add("5. Powder")
        cboItemClassification.Items.Add("6. Nickel")
        cboItemClassification.Items.Add("7. Zinc")
        cboItemClassification.Items.Add("8. Catalytic") ''CATALYTIC	

        CboStatus.Items.Clear()
        CboStatus.Items.Add("Active  ")
        CboStatus.Items.Add("Inactive")

        cboGSTClass.Items.Clear()
        cboGSTClass.Items.Add("0-GST Relevant")
        cboGSTClass.Items.Add("1-Non GST")
        cboGSTClass.Items.Add("2-GST Exempt")


        FillComboItemName
        FillComboItemCode

        'oledbCnn.Open()

        'SqlStr = "Select DISTINCT ITEM_SHORT_DESC, ITEM_CODE, ISSUE_UOM,  PURCHASE_UOM " & vbCrLf _
        '    & " FROM INV_ITEM_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ORDER BY ITEM_SHORT_DESC"

        'oledbAdapter = New OleDbDataAdapter(SqlStr, oledbCnn)
        'oledbAdapter.Fill(ds)

        '' Set the data source and data member to bind the grid.
        'txtItemName.DataSource = ds
        'txtItemName.DataMember = ""
        ''cmbCompany.ValueMember = "COMPANY_CODE"
        ''cmbCompany.DisplayMember = "Company Name"

        'txtItemName.Appearance.FontData.SizeInPoints = 8.5

        'txtItemName.DisplayLayout.Bands(0).Columns(0).Header.Caption = "Item Name"
        'txtItemName.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Item Code"
        'txtItemName.DisplayLayout.Bands(0).Columns(2).Header.Caption = "Issue UOM"
        'txtItemName.DisplayLayout.Bands(0).Columns(3).Header.Caption = "Purchase UOM"


        'txtItemName.DisplayLayout.Bands(0).Columns(0).Width = 350
        'txtItemName.DisplayLayout.Bands(0).Columns(1).Width = 100
        'txtItemName.DisplayLayout.Bands(0).Columns(2).Width = 80
        'txtItemName.DisplayLayout.Bands(0).Columns(3).Width = 80

        'txtItemName.DisplayLayout.Appearance.FontData.SizeInPoints = 8.5


        'txtItemName.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown ''List       '' Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown


        'SqlStr = "Select DISTINCT ITEM_CODE, ITEM_SHORT_DESC,  ISSUE_UOM,  PURCHASE_UOM " & vbCrLf _
        '    & " FROM INV_ITEM_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ORDER BY ITEM_CODE DESC"

        'oledbAdapter = New OleDbDataAdapter(SqlStr, oledbCnn)
        'oledbAdapter.Fill(ds)

        '' Set the data source and data member to bind the grid.
        'txtItemCode.DataSource = ds
        'txtItemCode.DataMember = ""
        ''cmbCompany.ValueMember = "COMPANY_CODE"
        ''cmbCompany.DisplayMember = "Company Name"

        'txtItemCode.Appearance.FontData.SizeInPoints = 8.5

        'txtItemCode.DisplayLayout.Bands(0).Columns(0).Header.Caption = "Item Code"
        'txtItemCode.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Item Name"
        'txtItemCode.DisplayLayout.Bands(0).Columns(2).Header.Caption = "Issue UOM"
        'txtItemCode.DisplayLayout.Bands(0).Columns(3).Header.Caption = "Purchase UOM"


        'txtItemCode.DisplayLayout.Bands(0).Columns(0).Width = 100
        'txtItemCode.DisplayLayout.Bands(0).Columns(1).Width = 350
        'txtItemCode.DisplayLayout.Bands(0).Columns(2).Width = 80
        'txtItemCode.DisplayLayout.Bands(0).Columns(3).Width = 80

        'txtItemCode.DisplayLayout.Appearance.FontData.SizeInPoints = 8.5


        'txtItemCode.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown ''List       '' Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown



        'oledbAdapter.Dispose()
        'oledbCnn.Close()

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub FillComboItemCode()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        Dim RS As ADODB.Recordset
        Dim CntLst As Long

        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet

        oledbCnn = New OleDbConnection(StrConn)

        oledbCnn.Open()

        SqlStr = "Select DISTINCT ITEM_CODE, ITEM_SHORT_DESC,  ISSUE_UOM,  PURCHASE_UOM " & vbCrLf _
            & " FROM INV_ITEM_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        'If Trim(txtItemCode.Text) <> "" Then
        '    SqlStr = SqlStr & " AND ITEM_CODE Like '%" & txtItemCode.Text & "%'"
        'End If

        SqlStr = SqlStr & " ORDER BY ITEM_CODE DESC"

        'SqlStr = SqlStr & vbCrLf & " FETCH FIRST 500 ROWS ONLY"

        oledbAdapter = New OleDbDataAdapter(SqlStr, oledbCnn)
        oledbAdapter.Fill(ds)

        ' Set the data source and data member to bind the grid.
        txtItemCode.DataSource = ds
        txtItemCode.DataMember = ""
        'cmbCompany.ValueMember = "COMPANY_CODE"
        'cmbCompany.DisplayMember = "Company Name"

        txtItemCode.Appearance.FontData.SizeInPoints = 8.5

        txtItemCode.DisplayLayout.Bands(0).Columns(0).Header.Caption = "Item Code"
        txtItemCode.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Item Name"
        txtItemCode.DisplayLayout.Bands(0).Columns(2).Header.Caption = "Issue UOM"
        txtItemCode.DisplayLayout.Bands(0).Columns(3).Header.Caption = "Purchase UOM"


        txtItemCode.DisplayLayout.Bands(0).Columns(0).Width = 100
        txtItemCode.DisplayLayout.Bands(0).Columns(1).Width = 350
        txtItemCode.DisplayLayout.Bands(0).Columns(2).Width = 80
        txtItemCode.DisplayLayout.Bands(0).Columns(3).Width = 80

        txtItemCode.DisplayLayout.Appearance.FontData.SizeInPoints = 8.5


        txtItemCode.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown ''List       '' Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown

        oledbAdapter.Dispose()
        oledbCnn.Close()

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub FillComboItemName()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        Dim RS As ADODB.Recordset
        Dim CntLst As Long

        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet

        oledbCnn = New OleDbConnection(StrConn)

        oledbCnn.Open()

        SqlStr = "Select DISTINCT ITEM_SHORT_DESC, ITEM_CODE, ISSUE_UOM,  PURCHASE_UOM " & vbCrLf _
            & " FROM INV_ITEM_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        'If Trim(txtItemName.Text) <> "" Then
        '    SqlStr = SqlStr & " AND ITEM_SHORT_DESC Like '%" & txtItemName.Text & "%'"
        'End If

        SqlStr = SqlStr & " ORDER BY ITEM_SHORT_DESC"

        'SqlStr = SqlStr & vbCrLf & " FETCH FIRST 500 ROWS ONLY"

        oledbAdapter = New OleDbDataAdapter(SqlStr, oledbCnn)
        oledbAdapter.Fill(ds)

        ' Set the data source and data member to bind the grid.
        txtItemName.DataSource = ds
        txtItemName.DataMember = ""
        'cmbCompany.ValueMember = "COMPANY_CODE"
        'cmbCompany.DisplayMember = "Company Name"

        txtItemName.Appearance.FontData.SizeInPoints = 8.5

        txtItemName.DisplayLayout.Bands(0).Columns(0).Header.Caption = "Item Name"
        txtItemName.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Item Code"
        txtItemName.DisplayLayout.Bands(0).Columns(2).Header.Caption = "Issue UOM"
        txtItemName.DisplayLayout.Bands(0).Columns(3).Header.Caption = "Purchase UOM"


        txtItemName.DisplayLayout.Bands(0).Columns(0).Width = 350
        txtItemName.DisplayLayout.Bands(0).Columns(1).Width = 100
        txtItemName.DisplayLayout.Bands(0).Columns(2).Width = 80
        txtItemName.DisplayLayout.Bands(0).Columns(3).Width = 80

        txtItemName.DisplayLayout.Appearance.FontData.SizeInPoints = 8.5


        txtItemName.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown ''List       '' Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown

        oledbAdapter.Dispose()
        oledbCnn.Close()

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        On Error Resume Next
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        Dim SqlStr As String = ""

        If txtItemName.Text = "" Then Call ErrorMsg("Nothing to Delete", "", MsgBoxStyle.Critical) : Exit Sub
        'If CheckConsolidatedMaster("INV_ITEM_MST") = True Then
        '    MsgInformation("Cann't be Delete.")
        '    Exit Sub
        'End If

        If MainClass.ValidateWithMasterTable(txtItemCode.Text, "PARENT_CODE", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            MsgInformation("Item is Defined as Parent Code so Cann't be Deleted.")
            Exit Sub
        End If


        If Not RsItemMast.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()

                If InsertIntoDelAudit(PubDBCn, "INV_ITEM_MST", (txtItemCode.Text), RsItemMast, "ITEM_CODE") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "INV_ITEM_MST", "ITEM_CODE", RsItemMast.Fields("ITEM_CODE").Value) = False Then GoTo DelErrPart

                SqlStr = " DELETE From INV_ITEM_MST WHERE " & vbCrLf _
                    & " ITEM_CODE='" & MainClass.AllowSingleQuote(Trim(RsItemMast.Fields("ITEM_CODE").Value)) & "'"

                If CheckConsolidatedMaster("INV_ITEM_MST") = True Then
                Else
                    SqlStr = SqlStr & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
                End If

                PubDBCn.Execute(SqlStr)

                PubDBCn.CommitTrans()
                RsItemMast.Requery() ''.Refresh	
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        ''Resume	
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans() ''	
        RsItemMast.Requery() ''.Refresh	
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsItemMast, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
            TxtItemName_Validating(txtItemName, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True Then CmdAdd.Focus()
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
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xCompanyCode As Long
        Dim SqlStr As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If CheckConsolidatedMaster("INV_ITEM_MST") = True Then
            SqlStr = "SELECT COMPANY_CODE FROM GEN_COMPANY_MST"
        Else
            SqlStr = "SELECT COMPANY_CODE FROM GEN_COMPANY_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                xCompanyCode = RsTemp.Fields("COMPANY_CODE").Value
                If UpdateItem(xCompanyCode) = False Then GoTo UpdateError
                RsTemp.MoveNext()
            Loop
        End If

        If UpdateItemPara(xCompanyCode) = False Then GoTo UpdateError

        PubDBCn.CommitTrans()
        Update1 = True
        Exit Function
UpdateError:
        Call ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans() ''	
        Update1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function


    Private Function UpdateItem(ByVal xCompanyCode As Long) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mAutoIndent As String = ""
        Dim mConsumable As String = ""
        Dim mDrawingAvailable As String = "N"
        Dim mIsChild As String = ""
        Dim mParentcode As String = ""
        Dim mProdType As String = ""
        Dim mModel As String = ""
        Dim mScrapItemCode As String = ""
        Dim mPackingItemCode As String = ""
        Dim mCatCode As String = ""
        Dim mSubCatCode As String = ""
        Dim mDwgRevDate As String = ""

        txtItemName.Text = UCase(txtItemName.Text)

        If Trim(txtDwgRevDate.Text) = "" Then
            mDwgRevDate = ""    ''CDate(DBNull.Value.ToString())
        Else
            mDwgRevDate = CDate(txtDwgRevDate.Text).ToString("dd-MMM-yyyy")
        End If


        If MainClass.ValidateWithMasterTable(txtCatName.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCompanyCode & " AND GEN_TYPE='C'") = True Then
            mCatCode = MasterNo
        End If

        If MainClass.ValidateWithMasterTable(txtSubCatName.Text, "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCompanyCode & " AND CATEGORY_CODE='" & mCatCode & "' ") = True Then
            mSubCatCode = MasterNo
        End If


        If MainClass.ValidateWithMasterTable(Trim(txtPackingItemCode.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCompanyCode & "") = True Then
            mPackingItemCode = MasterNo
        End If

        If MainClass.ValidateWithMasterTable(Trim(txtScrapItemCode.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCompanyCode & "") = True Then
            mScrapItemCode = MasterNo
        End If

        If MainClass.ValidateWithMasterTable(txtModel.Text, "MODEL_DESC", "MODEL_CODE", "GEN_MODEL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCompanyCode) = True Then
            mModel = MasterNo
        End If

        If MainClass.ValidateWithMasterTable(Trim(txtProdType.Text), "PRODTYPE_DESC", "PRODTYPE_DESC", "INV_PRODUCTTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCompanyCode & "") = True Then
            mProdType = MasterNo
        End If

        mIsChild = IIf(chkChildItem.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        If mIsChild = "Y" Then
            If MainClass.ValidateWithMasterTable(Trim(txtParentItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCompanyCode & " AND IS_CHILD='N'") = True Then
                mParentcode = MasterNo
                mParentcode = Trim(mParentcode)
            End If
        Else
            mParentcode = ""
        End If

        If ADDMode = True Then
            Dim mAutoGenCode = IIf(IsDBNull(RsCompany.Fields("AUTO_GEN_CODE").Value), "N", RsCompany.Fields("AUTO_GEN_CODE").Value)
            If mAutoGenCode = "N" Then
                mItemCode = MainClass.AllowSingleQuote(txtItemCode.Text) ''MainClass.AutoGenRowNo("ACM", "Code", PubDBCn)	
            Else
                If Trim(txtItemCode.Text) = "" Then
                    mItemCode = GenerateItemCode(xCompanyCode, mCatCode, mSubCatCode)
                    txtItemCode.Text = mItemCode
                Else
                    mItemCode = MainClass.AllowSingleQuote(txtItemCode.Text)
                End If
            End If
            SqlStr = ""

            SqlStr = " INSERT INTO INV_ITEM_MST ( " & vbCrLf _
                & " COMPANY_CODE, ITEM_CODE, SEMI_FIN_ITEM_CODE,  " & vbCrLf _
                & " CATEGORY_CODE, SUBCATEGORY_CODE, ITEM_TYPE,  " & vbCrLf _
                & " ISSUE_UOM, ITEM_EXCISE_FLAG,MINIMUM_QTY, " & vbCrLf _
                & " MAXIMUM_QTY, REORDER_QTY,ECONOMIC_QTY, " & vbCrLf _
                & " ITEM_STATUS , ITEM_STD_COST, ITEM_WEIGHT, " & vbCrLf _
                & " ITEM_SURFACE_AREA,SHEAR_SCRAP_WGT, " & vbCrLf _
                & " ITEM_MAKE,ITEM_COLOR,ITEM_SHORT_DESC, " & vbCrLf _
                & " ITEM_TECH_DESC,ITEM_DIMENSIONS,CUSTOMER_PART_NO,OLD_CUSTOMER_PART_NO," & vbCrLf _
                & " ITEM_GRADE,ITEM_QAS_NO,ITEM_MODEL, PRODTYPE_DESC," & vbCrLf _
                & " TARIFF_CODE,DRAWING_NO,IDENT_MARK," & vbCrLf _
                & " AUTO_INDENT,PURCHASE_COST,CONSUMABLE_FLAG, " & vbCrLf _
                & " ITEM_CLASSIFICATION,LEAD_TIME,ITEM_CLASS, " & vbCrLf _
                & " ITEM_CLASS_QTY,PURCHASE_UOM,UOM_FACTOR," & vbCrLf _
                & " DRW_REVNO,DRW_REVEFF_DATE,PACK_ITEM_CODE,PACK_STD," & vbCrLf _
                & " DSP_RPT_FLAG, " & vbCrLf _
                & " STOCKITEM, POREQD, IS_EXPORT_ITEM, SCRAP_ITEM_CODE,ITEM_WLENGTH,ITEM_TACKS, " & vbCrLf _
                & " MAT_DESC, MAT_LEN, MAT_WIDTH, " & vbCrLf _
                & " MAT_THICHNESS, MAT_DENSITY, SURFACE_TREATMENT, WELD_LINE," & vbCrLf _
                & " ADDUSER, ADDDATE, MODUSER, MODDATE," & vbCrLf _
                & " ITEM_LOCATION,IS_CHILD, PARENT_CODE, " & vbCrLf _
                & " ITEM_SS_WLENGTH,ITEM_SURFACE_AREA_IN,ADD_PPS_SURFACE_AREA, " & vbCrLf _
                & " ADD_NPC_SURFACE_AREA, ADD_PLT_SURFACE_AREA, " & vbCrLf _
                & " TIG_WLENGTH, BRAZING_WLENGTH," & vbCrLf _
                & " SEAM_WLENGTH , SPOT_NOS, IS_GRINDING, PRESS_LINE, "

            SqlStr = SqlStr & vbCrLf _
                & " IS_SHOTBLASTING,HSN_CODE,GST_ITEMCLASS, ITEM_WLENGTH_CUST, ITEM_SS_WLENGTH_CUST, WT_PER_STRIP, GROUP_ITEM_CODE,PACK_TYPE,ITEM_JW_UOM,HEAT_NO_REQ" & vbCrLf _
                & " ) VALUES ( "

            SqlStr = SqlStr & vbCrLf _
                & " " & xCompanyCode & ", " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtItemCode.Text) & "'," & vbCrLf _
                & " '" & IIf(chkDrawing.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(mCatCode) & "', "

                SqlStr = SqlStr & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(mSubCatCode) & "'," & vbCrLf _
                & " '" & VB.Left(CboItemType.Text, 1) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtItemUom.Text) & "', " & vbCrLf _
                & " '" & VB.Left(CboExciseFlag.Text, 1) & "'," & vbCrLf _
                & " " & Val(txtMinQnty.Text) & "," & vbCrLf _
                & " " & Val(txtMaxQnty.Text) & "," & vbCrLf _
                & " " & Val(txtReQnty.Text) & "," & vbCrLf _
                & " " & Val(txtEcoQnty.Text) & "," & vbCrLf _
                & " '" & VB.Left(CboStatus.Text, 1) & "',"

                SqlStr = SqlStr & vbCrLf _
                & " '" & Val(txtSaleCost.Text) & "'," & vbCrLf _
                & " " & Val(txtWeight.Text) & "," & vbCrLf _
                & " " & Val(txtSurfaceArea.Text) & "," & vbCrLf _
                & " " & Val(txtScrapWeight.Text) & "," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtItemMake.Text) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtColor.Text) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(UCase(txtItemName.Text.Replace(vbCrLf, ""))) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtTechnicalDescription.Text) & "',"

                SqlStr = SqlStr & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtDimention.Text) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtPartNo.Text) & "','" & MainClass.AllowSingleQuote(txtOldPartNo.Text) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtSpecification.Text) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtInspectionNo.Text) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(mModel) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(mProdType) & "'," & vbCrLf _
                & " ''," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtDwgNo.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtIdMark.Text) & "'," & vbCrLf _
                & " '" & IIf(chkAutoIssue.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "'," & vbCrLf _
                & " " & Val(txtPurchaseCost.Text) & ","

                SqlStr = SqlStr & vbCrLf _
                & " '" & IIf(chkConsumable.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "'," & vbCrLf _
                & " '" & VB.Left(cboItemClassification.Text, 1) & "'," & vbCrLf _
                & " '" & Val(txtLeadTime.Text) & "'," & vbCrLf _
                & " '" & VB.Left(CboItemClass.Text, 1) & "'," & vbCrLf _
                & " " & Val(txtItemClassQnty.Text) & "," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtPurchaseUom.Text) & "'," & vbCrLf _
                & " '" & Val(txtUOMFactor.Text) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtDwgRevNo.Text) & "'," & vbCrLf _
                & " TO_DATE( '" & mDwgRevDate & "','DD-MON-YYYY')," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(mPackingItemCode) & "'," & vbCrLf _
                & " '" & Val(txtPackingStandard.Text) & "', " & vbCrLf _
                & " '" & IIf(chkRequired.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "', " & vbCrLf _
                & " '" & IIf(chkStockItem.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "'," & vbCrLf _
                & " '" & IIf(chkPOReqd.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "'," & vbCrLf _
                & " '" & IIf(chkExportItem.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "',"

            SqlStr = SqlStr & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(mScrapItemCode) & "'," & Val(txtWLength.Text) & "," & Val(txtTacks.Text) & ", " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtMaterial.Text) & "', " & Val(txtLength.Text) & ", " & Val(txtWidth.Text) & ", " & vbCrLf _
                & " " & Val(txtThickness.Text) & ", '" & MainClass.AllowSingleQuote(txtDensity.Text) & "', '" & VB.Left(cboSurfaceTreatment.Text, 1) & "', '" & VB.Left(cboWeldingLine.Text, 1) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','','" & Trim(txtLocation.Text) & "', " & vbCrLf _
                & " '" & mIsChild & "', '" & MainClass.AllowSingleQuote(mParentcode) & "', " & vbCrLf _
                & " " & Val(txtSSWLength.Text) & ", " & Val(txtSurfaceAreaInner.Text) & ", " & Val(txtAddSurfaceAreaPPS.Text) & "," & vbCrLf _
                & " " & Val(txtAddSurfaceAreaNPC.Text) & ", " & Val(txtAddSurfaceAreaPLT.Text) & "," & vbCrLf _
                & " " & Val(txtTIGLen.Text) & ", " & Val(txtBrazingLen.Text) & ", " & Val(txtSeamLen.Text) & "," & vbCrLf _
                & " " & Val(txtSpotNos.Text) & ", '" & IIf(chkGrinding.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "'," & vbCrLf _
                & " '" & VB.Left(cboPressLine.Text, 1) & "', '" & IIf(chkSB.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtHSNCode.Text) & "', '" & VB.Left(cboGSTClass.Text, 1) & "'," & Val(txtWLengthCust.Text) & "," & Val(txtSSWLengthCust.Text) & "," & Val(txtWtPerStrip.Text) & ", '" & MainClass.AllowSingleQuote(txtGUID.Text) & "', '" & MainClass.AllowSingleQuote(txtPackType.Text) & "','" & MainClass.AllowSingleQuote(txtJWUOM.Text) & "'," & vbCrLf _
                & " '" & IIf(chkHeatReq.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "')"

        End If

            If MODIFYMode = True Then
            SqlStr = ""

            ''CATEGORY_CODE= '" & MainClass.AllowSingleQuote(mCatCode) & "',
            ''SUBCATEGORY_CODE= '" & MainClass.AllowSingleQuote(mSubCatCode) & "',

            SqlStr = " UPDATE INV_ITEM_MST SET  " & vbCrLf _
                & " GROUP_ITEM_CODE='" & MainClass.AllowSingleQuote(txtGUID.Text) & "'," & vbCrLf _
                & " HEAT_NO_REQ='" & IIf(chkHeatReq.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "'," & vbCrLf _
                & " ISSUE_UOM= '" & MainClass.AllowSingleQuote(txtItemUom.Text) & "', " & vbCrLf _
                & " ITEM_SHORT_DESC= '" & MainClass.AllowSingleQuote(UCase(txtItemName.Text.Replace(vbCrLf, ""))) & "', " & vbCrLf _
                & " ITEM_TECH_DESC= '" & MainClass.AllowSingleQuote(txtTechnicalDescription.Text) & "', " & vbCrLf _
                & " CUSTOMER_PART_NO= '" & MainClass.AllowSingleQuote(txtPartNo.Text) & "', OLD_CUSTOMER_PART_NO= '" & MainClass.AllowSingleQuote(txtOldPartNo.Text) & "', ITEM_JW_UOM= '" & MainClass.AllowSingleQuote(txtJWUOM.Text) & "'," & vbCrLf _
                & " PURCHASE_UOM= '" & MainClass.AllowSingleQuote(txtPurchaseUom.Text) & "', PACK_TYPE='" & MainClass.AllowSingleQuote(txtPackType.Text) & "',"

            'ALL NUMERIC COLUMNS	
            SqlStr = SqlStr & vbCrLf _
                & " MINIMUM_QTY= " & Val(txtMinQnty.Text) & ", " & vbCrLf _
                & " MAXIMUM_QTY= " & Val(txtMaxQnty.Text) & ", " & vbCrLf _
                & " REORDER_QTY= " & Val(txtReQnty.Text) & ", " & vbCrLf _
                & " ECONOMIC_QTY= " & Val(txtEcoQnty.Text) & ", " & vbCrLf _
                & " ITEM_STD_COST= " & Val(txtSaleCost.Text) & ", " & vbCrLf _
                & " ITEM_WEIGHT= " & Val(txtWeight.Text) & ", " & vbCrLf _
                & " ITEM_SURFACE_AREA= " & Val(txtSurfaceArea.Text) & ", " & vbCrLf _
                & " SHEAR_SCRAP_WGT= " & Val(txtScrapWeight.Text) & ", " & vbCrLf _
                & " PURCHASE_COST= " & Val(txtPurchaseCost.Text) & ", " & vbCrLf _
                & " LEAD_TIME= " & Val(txtLeadTime.Text) & ", " & vbCrLf _
                & " UOM_FACTOR= " & Val(txtUOMFactor.Text) & ", " & vbCrLf _
                & " PACK_STD= " & Val(txtPackingStandard.Text) & ", "

            ' CHECK BOXES & COMBO BOXES	
            SqlStr = SqlStr & vbCrLf _
                & " SEMI_FIN_ITEM_CODE= '" & IIf(chkDrawing.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "', " & vbCrLf _
                & " CONSUMABLE_FLAG= '" & IIf(chkConsumable.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "', " & vbCrLf _
                & " AUTO_INDENT= '" & IIf(chkAutoIssue.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "', " & vbCrLf _
                & " ITEM_TYPE= '" & VB.Left(CboItemType.Text, 1) & "', " & vbCrLf _
                & " ITEM_CLASS= '" & VB.Left(CboItemClass.Text, 1) & "', " & vbCrLf _
                & " ITEM_EXCISE_FLAG= '" & VB.Left(CboExciseFlag.Text, 1) & "', " & vbCrLf _
                & " ITEM_CLASSIFICATION= '" & VB.Left(cboItemClassification.Text, 1) & "', " & vbCrLf _
                & " ITEM_STATUS= '" & VB.Left(CboStatus.Text, 1) & "', "

            SqlStr = SqlStr & vbCrLf & " ITEM_MAKE= '" & MainClass.AllowSingleQuote(txtItemMake.Text) & "', " & vbCrLf _
                & " ITEM_COLOR= '" & MainClass.AllowSingleQuote(txtColor.Text) & "', " & vbCrLf _
                & " ITEM_DIMENSIONS= '" & MainClass.AllowSingleQuote(txtDimention.Text) & "', " & vbCrLf _
                & " ITEM_GRADE= '" & MainClass.AllowSingleQuote(txtSpecification.Text) & "', " & vbCrLf _
                & " ITEM_QAS_NO= '" & MainClass.AllowSingleQuote(txtInspectionNo.Text) & "', " & vbCrLf _
                & " ITEM_MODEL= '" & MainClass.AllowSingleQuote(mModel) & "', " & vbCrLf _
                & " PRODTYPE_DESC= '" & MainClass.AllowSingleQuote(mProdType) & "', " & vbCrLf _
                & " TARIFF_CODE= '', " & vbCrLf _
                & " DRAWING_NO= '" & MainClass.AllowSingleQuote(txtDwgNo.Text) & "', " & vbCrLf _
                & " IDENT_MARK= '" & MainClass.AllowSingleQuote(txtIdMark.Text) & "', " & vbCrLf _
                & " ITEM_CLASS_QTY= '" & MainClass.AllowSingleQuote(txtItemClassQnty.Text) & "', " & vbCrLf _
                & " DRW_REVNO= '" & MainClass.AllowSingleQuote(txtDwgRevNo.Text) & "', " & vbCrLf _
                & " DRW_REVEFF_DATE=TO_DATE( '" & mDwgRevDate & "','DD-MON-YYYY'), " & vbCrLf _
                & " PACK_ITEM_CODE= '" & MainClass.AllowSingleQuote(mPackingItemCode) & "', "

            SqlStr = SqlStr & vbCrLf _
                & " DSP_RPT_FLAG= '" & IIf(chkRequired.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "', " & vbCrLf _
                & " STOCKITEM= '" & IIf(chkStockItem.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "', " & vbCrLf _
                & " POREQD= '" & IIf(chkPOReqd.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "', " & vbCrLf _
                & " IS_EXPORT_ITEM= '" & IIf(chkExportItem.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "', " & vbCrLf _
                & " SCRAP_ITEM_CODE='" & MainClass.AllowSingleQuote(mScrapItemCode) & "', " & vbCrLf _
                & " ITEM_WLENGTH=" & Val(txtWLength.Text) & ",ITEM_TACKS=" & Val(txtTacks.Text) & "," & vbCrLf _
                & " MAT_DESC = '" & MainClass.AllowSingleQuote(txtMaterial.Text) & "', " & vbCrLf _
                & " MAT_LEN = " & Val(txtLength.Text) & ", " & vbCrLf _
                & " MAT_WIDTH = " & Val(txtWidth.Text) & ", " & vbCrLf _
                & " MAT_THICHNESS = " & Val(txtThickness.Text) & ", " & vbCrLf _
                & " MAT_DENSITY = '" & MainClass.AllowSingleQuote(txtDensity.Text) & "', " & vbCrLf _
                & " SURFACE_TREATMENT = '" & VB.Left(cboSurfaceTreatment.Text, 1) & "', WELD_LINE='" & VB.Left(cboWeldingLine.Text, 1) & "'," & vbCrLf _
                & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')," & vbCrLf _
                & " ITEM_LOCATION='" & Trim(txtLocation.Text) & "'," & vbCrLf _
                & " IS_CHILD='" & mIsChild & "', PARENT_CODE='" & MainClass.AllowSingleQuote(mParentcode) & "'," & vbCrLf _
                & " ITEM_SS_WLENGTH=" & Val(txtSSWLength.Text) & ", ITEM_SURFACE_AREA_IN=" & Val(txtSurfaceAreaInner.Text) & "," & vbCrLf _
                & " ADD_PPS_SURFACE_AREA=" & Val(txtAddSurfaceAreaPPS.Text) & ", ADD_NPC_SURFACE_AREA=" & Val(txtAddSurfaceAreaNPC.Text) & ", ADD_PLT_SURFACE_AREA= " & Val(txtAddSurfaceAreaPLT.Text) & ","

            SqlStr = SqlStr & vbCrLf _
                & " HSN_CODE= '" & MainClass.AllowSingleQuote(txtHSNCode.Text) & "', " & vbCrLf _
                & " TIG_WLENGTH=" & Val(txtTIGLen.Text) & ", " & vbCrLf _
                & " BRAZING_WLENGTH=" & Val(txtBrazingLen.Text) & "," & vbCrLf _
                & " SEAM_WLENGTH=" & Val(txtSeamLen.Text) & ", " & vbCrLf _
                & " SPOT_NOS=" & Val(txtSpotNos.Text) & ", " & vbCrLf _
                & " IS_GRINDING='" & IIf(chkGrinding.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "', " & vbCrLf _
                & " PRESS_LINE='" & VB.Left(cboPressLine.Text, 1) & "', " & vbCrLf _
                & " IS_SHOTBLASTING='" & IIf(chkSB.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "', " & vbCrLf _
                & " GST_ITEMCLASS='" & VB.Left(cboGSTClass.Text, 1) & "', " & vbCrLf _
                & " ITEM_WLENGTH_CUST=" & Val(txtWLengthCust.Text) & ", " & vbCrLf _
                & " ITEM_SS_WLENGTH_CUST=" & Val(txtSSWLengthCust.Text) & ", WT_PER_STRIP= " & Val(txtWtPerStrip.Text) & " "


            SqlStr = SqlStr & vbCrLf _
                & " WHERE COMPANY_CODE=" & xCompanyCode & " " & vbCrLf _
                & " AND ITEM_CODE = '" & Trim(mItemCode) & "'"

        End If
        PubDBCn.Execute(SqlStr)

        If MODIFYMode = True And xCompanyCode = RsCompany.Fields("COMPANY_CODE").Value Then
            SqlStr = " UPDATE INV_ITEM_MST SET  " & vbCrLf _
                & " CATEGORY_CODE= '" & MainClass.AllowSingleQuote(mCatCode) & "'," & vbCrLf _
                & " SUBCATEGORY_CODE= '" & MainClass.AllowSingleQuote(mSubCatCode) & "'" & vbCrLf _
                & " WHERE COMPANY_CODE=" & xCompanyCode & " " & vbCrLf _
                & " AND ITEM_CODE = '" & Trim(mItemCode) & "'"

            PubDBCn.Execute(SqlStr)
        End If

        UpdateItem = True
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateItem = False
        'Resume	
    End Function
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Private Sub ViewGrid()

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            'MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        ADataGrid.Refresh	
            'FormatSprdView()
            UltraGrid1.Refresh()
            UltraGrid1.Focus()
            UltraGrid1.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            UltraGrid1.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsItemMast, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Public Sub frmItemMaster_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = "Select * From inv_item_mst WHERE 1<>1 Order by ITEM_SHORT_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsItemMast, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)

        SetTextLengths()
        'Clear1()
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

        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim i As Integer
        Dim inti As Integer

        oledbCnn = New OleDbConnection(StrConn)

        SqlStr = ""

        SqlStr = " SELECT INVMST.ITEM_CODE,INVMST.ITEM_SHORT_DESC AS DESCRIPTION, " & vbCrLf _
            & " INVMST.CUSTOMER_PART_NO AS PART_NO,INVMST.PURCHASE_UOM AS PUR_UNIT, INVMST.ISSUE_UOM AS ISS_UNIT, " & vbCrLf _
            & " INVMST.UOM_FACTOR, INVMST.ITEM_MODEL, " & vbCrLf _
            & " GMST.GEN_DESC AS CATEGORY, SMST.SUBCATEGORY_DESC AS SUB_CATEGORY, MAT_DESC, ITEM_WEIGHT,ITEM_STATUS," & vbCrLf _
            & " INVMST.ITEM_TYPE,INVMST.MINIMUM_QTY,INVMST.MAXIMUM_QTY,INVMST.REORDER_QTY, " & vbCrLf _
            & " INVMST.ECONOMIC_QTY,INVMST.ITEM_STD_COST,INVMST.ITEM_SURFACE_AREA, " & vbCrLf _
            & " INVMST.SHEAR_SCRAP_WGT,INVMST.ACCOUNT_CODE,INVMST.ITEM_MAKE,INVMST.ITEM_COLOR, " & vbCrLf _
            & " INVMST.ITEM_TECH_DESC,INVMST.ITEM_DIMENSIONS,INVMST.ITEM_GRADE,INVMST.ITEM_QAS_NO, " & vbCrLf _
            & " INVMST.DRAWING_NO,INVMST.IDENT_MARK, " & vbCrLf _
            & " INVMST.AUTO_INDENT,INVMST.CONSUMABLE_FLAG,INVMST.ITEM_CLASSIFICATION, " & vbCrLf _
            & " INVMST.LEAD_TIME,INVMST.ITEM_CLASS,INVMST.ITEM_CLASS_QTY,INVMST.DRW_REVNO, " & vbCrLf _
            & " INVMST.DRW_REVEFF_DATE,INVMST.PACK_ITEM_CODE,INVMST.PACK_STD, " & vbCrLf _
            & " INVMST.DSP_RPT_SEQ,INVMST.DSP_RPT_FLAG,INVMST.ADDUSER,INVMST.ADDDATE, " & vbCrLf _
            & " INVMST.MODUSER,INVMST.MODDATE,INVMST.DDR_TITLE,INVMST.STOCKITEM, " & vbCrLf _
            & " INVMST.POREQD,INVMST.SCRAP_ITEM_CODE,INVMST.ITEM_WLENGTH,INVMST.ITEM_TACKS, " & vbCrLf _
            & " INVMST.IS_EXPORT_ITEM,INVMST.MAT_LEN,INVMST.MAT_WIDTH,INVMST.MAT_THICHNESS, " & vbCrLf _
            & " INVMST.MAT_DENSITY,INVMST.SURFACE_TREATMENT,INVMST.ITEM_LOCATION, " & vbCrLf _
            & " INVMST.PRODTYPE_DESC,INVMST.ADDUSER,INVMST.ADDDATE,INVMST.MODUSER,INVMST.MODDATE "


        SqlStr = SqlStr & vbCrLf _
            & " FROM INV_ITEM_MST INVMST, INV_GENERAL_MST GMST, INV_SUBCATEGORY_MST SMST " & vbCrLf _
            & " WHERE INVMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND INVMST.COMPANY_CODE=GMST.COMPANY_CODE" & vbCrLf _
            & " AND INVMST.CATEGORY_CODE=GMST.GEN_CODE" & vbCrLf _
            & " AND INVMST.COMPANY_CODE=SMST.COMPANY_CODE" & vbCrLf _
            & " AND INVMST.CATEGORY_CODE=SMST.CATEGORY_CODE" & vbCrLf _
            & " AND INVMST.SUBCATEGORY_CODE=SMST.SUBCATEGORY_CODE" & vbCrLf _
            & " AND GMST.GEN_TYPE='C'"

        SqlStr = SqlStr & " ORDER BY INVMST.ITEM_SHORT_DESC"
        'MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        'FormatSprdView()


        ClearGroupFromUltraGrid(UltraGrid1)
        ClearFilterFromUltraGrid(UltraGrid1)

        oledbCnn.Open()
        oledbAdapter = New OleDbDataAdapter(SqlStr, oledbCnn)

        oledbAdapter.Fill(ds)

        ' Set the data source and data member to bind the grid.
        Me.UltraGrid1.DataSource = ds
        Me.UltraGrid1.DataMember = ""
        CreateGridHeader("S")

        MainClass.SetInfragisticsGrid(UltraGrid1, -1, "Filter Row", "Group Row")
        oledbAdapter.Dispose()
        oledbCnn.Close()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub CreateGridHeader(pShowType As String)
        '----------------------------------------------------------------------------
        'Argument       :   Nil
        'Return Value   :   Nil
        'Function       :   to create the grid header
        'Comments       :   Nil
        '----------------------------------------------------------------------------
        Try
            Dim inti As Integer
            'create column header



            'UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Key = "Locked"
            UltraGrid1.DisplayLayout.Bands(0).Columns(0).Header.Caption = "Item code"
            UltraGrid1.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Item Description"
            UltraGrid1.DisplayLayout.Bands(0).Columns(2).Header.Caption = "Part No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(3).Header.Caption = "Purchase UOM"
            UltraGrid1.DisplayLayout.Bands(0).Columns(4).Header.Caption = "Issue UOM"
            UltraGrid1.DisplayLayout.Bands(0).Columns(5).Header.Caption = "UOM Factor"

            UltraGrid1.DisplayLayout.Bands(0).Columns(6).Header.Caption = "Item Model"
            UltraGrid1.DisplayLayout.Bands(0).Columns(7).Header.Caption = "Category"
            UltraGrid1.DisplayLayout.Bands(0).Columns(8).Header.Caption = "SubCategory"
            UltraGrid1.DisplayLayout.Bands(0).Columns(9).Header.Caption = "Material Desc"
            UltraGrid1.DisplayLayout.Bands(0).Columns(10).Header.Caption = "Item Weight"

            UltraGrid1.DisplayLayout.Bands(0).Columns(11).Header.Caption = "Item Status"
            UltraGrid1.DisplayLayout.Bands(0).Columns(12).Header.Caption = "Item Type"
            UltraGrid1.DisplayLayout.Bands(0).Columns(13).Header.Caption = "Minimum Qty"
            UltraGrid1.DisplayLayout.Bands(0).Columns(14).Header.Caption = "Maximum Qty"
            UltraGrid1.DisplayLayout.Bands(0).Columns(15).Header.Caption = "Reorder Qty"
            UltraGrid1.DisplayLayout.Bands(0).Columns(16).Header.Caption = "Economic Qty"
            UltraGrid1.DisplayLayout.Bands(0).Columns(17).Header.Caption = "Item Cost"

            UltraGrid1.DisplayLayout.Bands(0).Columns(18).Header.Caption = "Item Surface Area"
            UltraGrid1.DisplayLayout.Bands(0).Columns(19).Header.Caption = "Shearing Scrap Wgt."
            UltraGrid1.DisplayLayout.Bands(0).Columns(20).Header.Caption = "Account Code"
            UltraGrid1.DisplayLayout.Bands(0).Columns(21).Header.Caption = "Item Make"
            UltraGrid1.DisplayLayout.Bands(0).Columns(22).Header.Caption = "Item Color"
            UltraGrid1.DisplayLayout.Bands(0).Columns(23).Header.Caption = "Item Technical Desc"
            UltraGrid1.DisplayLayout.Bands(0).Columns(24).Header.Caption = "Item Dimensions"
            UltraGrid1.DisplayLayout.Bands(0).Columns(25).Header.Caption = "Identification Mark"
            UltraGrid1.DisplayLayout.Bands(0).Columns(26).Header.Caption = "Auto Indent"
            UltraGrid1.DisplayLayout.Bands(0).Columns(27).Header.Caption = "Consumable Flag"

            UltraGrid1.DisplayLayout.Bands(0).Columns(28).Header.Caption = "Item Classification"
            UltraGrid1.DisplayLayout.Bands(0).Columns(29).Header.Caption = "Lead Time"
            UltraGrid1.DisplayLayout.Bands(0).Columns(30).Header.Caption = "Item Class"
            UltraGrid1.DisplayLayout.Bands(0).Columns(31).Header.Caption = "Item Class Qty"
            UltraGrid1.DisplayLayout.Bands(0).Columns(32).Header.Caption = "Drawing Rev No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(33).Header.Caption = "Drawing Rev Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(34).Header.Caption = "Pack Item Code"
            UltraGrid1.DisplayLayout.Bands(0).Columns(35).Header.Caption = "Packing Std"
            UltraGrid1.DisplayLayout.Bands(0).Columns(36).Header.Caption = "DSP RPT Seq"
            UltraGrid1.DisplayLayout.Bands(0).Columns(37).Header.Caption = "Desp RTP Flag"
            UltraGrid1.DisplayLayout.Bands(0).Columns(38).Header.Caption = "Add User"

            UltraGrid1.DisplayLayout.Bands(0).Columns(39).Header.Caption = "Mod User"
            UltraGrid1.DisplayLayout.Bands(0).Columns(40).Header.Caption = "Mod Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(41).Header.Caption = "DDR Title"
            UltraGrid1.DisplayLayout.Bands(0).Columns(42).Header.Caption = "PO Reqd"
            UltraGrid1.DisplayLayout.Bands(0).Columns(43).Header.Caption = "Scrap Item Code"
            UltraGrid1.DisplayLayout.Bands(0).Columns(44).Header.Caption = "W Length"

            UltraGrid1.DisplayLayout.Bands(0).Columns(45).Header.Caption = "Item Tacks"
            UltraGrid1.DisplayLayout.Bands(0).Columns(46).Header.Caption = "Export Item"
            UltraGrid1.DisplayLayout.Bands(0).Columns(47).Header.Caption = "Material Length"
            UltraGrid1.DisplayLayout.Bands(0).Columns(48).Header.Caption = "Material Width"
            UltraGrid1.DisplayLayout.Bands(0).Columns(49).Header.Caption = "Material Thichness"
            UltraGrid1.DisplayLayout.Bands(0).Columns(50).Header.Caption = "Material Density"
            UltraGrid1.DisplayLayout.Bands(0).Columns(51).Header.Caption = "Surface Treatment"
            UltraGrid1.DisplayLayout.Bands(0).Columns(52).Header.Caption = "Item Location"
            UltraGrid1.DisplayLayout.Bands(0).Columns(53).Header.Caption = "Product Type"
            UltraGrid1.DisplayLayout.Bands(0).Columns(54).Header.Caption = "Add User"
            UltraGrid1.DisplayLayout.Bands(0).Columns(55).Header.Caption = "Add Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(56).Header.Caption = "Mod User"
            UltraGrid1.DisplayLayout.Bands(0).Columns(57).Header.Caption = "Mod Date"


            ''enable/disable the columns
            For inti = 0 To UltraGrid1.DisplayLayout.Bands(0).Columns.Count - 1
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).CellActivation = Activation.NoEdit  ''  .AllowEdit
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).Header.Appearance.TextHAlign = HAlign.Center

                ''UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.SubDepartmentName).Style = UltraWinGrid.ColumnStyle.DropDown
                '' UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.Qty).Style = UltraWinGrid.ColumnStyle.DoubleNonNegative
                ''UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.SubDepartmentName).EditorComponent = cmbDepartment
            Next

            ' to define width of the columns
            UltraGrid1.DisplayLayout.Bands(0).Columns(0).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(1).Width = 250
            UltraGrid1.DisplayLayout.Bands(0).Columns(2).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(3).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(4).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(5).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(6).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(7).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(8).Width = 200
            UltraGrid1.DisplayLayout.Bands(0).Columns(9).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(10).Width = 100


            'UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.MFGQty).MaskInput = "99999"

            Me.UltraGrid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True
            Me.UltraGrid1.DisplayLayout.Override.RowSelectorNumberStyle = RowSelectorNumberStyle.RowIndex

            'fill labels 
            'FillLabelsFromResFile(Me)
            'Catch sqlex As SqlException
            '    ErrorTrap(sqlex.Message, "frmRMReturn.vb", "CreateHeader", "", "", "Sql Exception")
            '    Me.Cursor = Windows.Forms.Cursors.Default
        Catch ex As Exception
            ErrorMsg(ex.Message, "")
            Me.Cursor = Windows.Forms.Cursors.Default
        End Try
    End Sub
    Private Sub frmItemMaster_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection	
        ''PvtDBCn.Open StrConn	
        Call SetMainFormCordinate(Me)
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        'FillComboBox()
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
        Dim mAutoGenCode As String

        mAutoGenCode = IIf(IsDBNull(RsCompany.Fields("AUTO_GEN_CODE").Value), "N", RsCompany.Fields("AUTO_GEN_CODE").Value)
        mItemCode = CStr(-1)
        txtItemName.Text = ""
        txtItemCode.Text = ""
        txtGUID.Text = ""
        txtParentItemName.Text = ""
        txtParentItemName.Enabled = False
        txtItemCode.Enabled = IIf(mAutoGenCode = "Y", False, True)
        txtGUID.Enabled = True
        txtItemName.ReadOnly = False

        txtJWUOM.Enabled = True
        txtJWUOM.Text = ""
        cmdSearchJWUom.Enabled = True

        txtCatName.Text = ""
        txtColor.Text = ""
        txtDimention.Text = ""
        txtDwgNo.Text = ""
        txtDwgRevDate.Text = ""
        txtDwgRevNo.Text = ""

        txtIdMark.Text = ""

        txtInspectionNo.Text = ""
        txtItemClassQnty.Text = ""
        txtItemMake.Text = ""
        txtItemUom.Text = ""
        txtLeadTime.Text = ""
        txtWeight.Text = ""
        txtMaxQnty.Text = ""
        txtReQnty.Text = ""
        txtMinQnty.Text = ""
        txtEcoQnty.Text = ""

        txtCatName.Enabled = True
        txtMaxQnty.Enabled = True
        txtReQnty.Enabled = True
        txtMinQnty.Enabled = True
        txtEcoQnty.Enabled = True

        txtSubCatName.Enabled = True
        txtUOMFactor.Enabled = True
        txtPartNo.Enabled = True
        txtOldPartNo.Enabled = True
        CboItemType.Enabled = True
        CboItemClass.Enabled = True
        CboExciseFlag.Enabled = True
        cboItemClassification.Enabled = True
        txtScrapItemCode.Enabled = True
        txtProdType.Enabled = True
        FraWeld.Enabled = True
        FraSurface.Enabled = True
        FraPress.Enabled = True
        chkGrinding.Enabled = True
        chkSB.Enabled = True


        txtModel.Text = ""
        txtProdType.Text = ""
        txtPackingItemCode.Text = ""
        txtPackingStandard.Text = ""
        txtPartNo.Text = ""
        txtOldPartNo.Text = ""
        txtPurchaseCost.Text = ""
        txtPurchaseUom.Text = ""


        txtSaleCost.Text = ""
        txtScrapWeight.Text = ""
        txtSpecification.Text = ""
        txtSubCatName.Text = ""
        txtSurfaceArea.Text = ""

        txtAddSurfaceAreaPPS.Text = ""
        txtAddSurfaceAreaNPC.Text = ""
        txtAddSurfaceAreaPLT.Text = ""

        txtTechnicalDescription.Text = ""
        txtUOMFactor.Text = ""
        txtWLength.Text = ""
        txtSSWLength.Text = ""
        txtWLengthCust.Text = ""
        txtSSWLengthCust.Text = ""
        txtSurfaceAreaInner.Text = ""
        txtTacks.Text = ""
        txtLocation.Text = ""

        txtTIGLen.Text = ""
        txtBrazingLen.Text = ""
        txtSeamLen.Text = ""
        txtSpotNos.Text = ""
        chkGrinding.CheckState = System.Windows.Forms.CheckState.Unchecked

        chkConsumable.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkDrawing.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkAutoIssue.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkRequired.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkAutoQC.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkHeatReq.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkStockItem.CheckState = System.Windows.Forms.CheckState.Checked
        chkStockItem.Enabled = IIf(PubSuperUser = "S", True, False)

        chkPOReqd.CheckState = System.Windows.Forms.CheckState.Checked
        chkExportItem.CheckState = System.Windows.Forms.CheckState.Unchecked
        txtScrapItemCode.Text = ""



        lblItemUom.Text = ""
        lblPurUom.Text = ""


        txtItemUom.Enabled = True
        txtPurchaseUom.Enabled = True
        cmdSearchUom.Enabled = True
        cmdSearchPurUom.Enabled = True
        lblItemUom.Text = ""
        lblPurUom.Text = ""

        lblAddUser.Text = ""
        lblAddDate.Text = ""
        lblModUser.Text = ""
        lblModDate.Text = ""
        cmdSearchHSN.Enabled = True
        cmdSearchScrap.Enabled = True
        txtMaterial.Text = ""
        txtLength.Text = ""
        txtWidth.Text = ""
        txtThickness.Text = ""
        txtDensity.Text = ""
        txtWtPerStrip.Text = ""

        chkMRRLocking.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkMRRLockingOM.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkScheduleLocking.CheckState = System.Windows.Forms.CheckState.Unchecked


        txtHSNCode.Text = ""
        lblHSNName.Text = ""

        txtPackType.Text = ""
        txtHSNCode.Enabled = True

        SSTInfo.SelectedIndex = 0



        '*********	


        '    If PubUserID = "SUPER" Then	
        '        ChkPoRate.Enabled = True	
        '    Else	
        '        ChkPoRate.Enabled = False	
        '    End If	
        '    ChkPoRate.Value = vbUnchecked	
        '*******	

        MainClass.ClearGrid(SprdMain)
        FormatSprdMain(-1)

        Call FillCompany()
        FillComboBox()


        CboItemType.SelectedIndex = 0
        CboItemClass.SelectedIndex = 0
        CboExciseFlag.SelectedIndex = 0
        cboItemClassification.SelectedIndex = 0
        CboStatus.SelectedIndex = 0
        CboStatus.Enabled = True

        cboGSTClass.SelectedIndex = 0
        cboGSTClass.Enabled = True

        cboSurfaceTreatment.SelectedIndex = 0
        cboWeldingLine.SelectedIndex = 0
        cboPressLine.SelectedIndex = 0
        chkSB.CheckState = System.Windows.Forms.CheckState.Unchecked


        'Call AutoCompleteSearch("INV_ITEM_MST", "ITEM_SHORT_DESC", "", txtItemName)
        'Call AutoCompleteSearch("INV_ITEM_MST", "ITEM_CODE", "", txtItemCode, "D")

        Call AutoCompleteSearch("INV_GENERAL_MST", "GEN_CODE", "GEN_TYPE='U'", txtItemUom)
        Call AutoCompleteSearch("INV_GENERAL_MST", "GEN_CODE", "GEN_TYPE='U'", txtPurchaseUom)
        Call AutoCompleteSearch("INV_GENERAL_MST", "GEN_DESC", "GEN_TYPE='C'", txtCatName)
        Call AutoCompleteSearch("GEN_HSN_MST", "HSN_CODE", "", txtHSNCode)
        Call AutoCompleteSearch("INV_ITEM_MST", "ITEM_COLOR", "", txtColor)

        'Call AutoCompleteSearch("INV_ITEM_MST", "ITEM_SHORT_DESC", "", txtPackingItemCode)
        'Call AutoCompleteSearch("INV_ITEM_MST", "ITEM_SHORT_DESC", "", txtScrapItemCode)
        Call AutoCompleteSearch("GEN_MODEL_MST", "MODEL_DESC", "", txtModel)
        Call AutoCompleteSearch("INV_PRODUCTTYPE_MST", "PRODTYPE_DESC", "", txtProdType)
        'Call AutoCompleteSearch("INV_ITEM_MST", "ITEM_SHORT_DESC", "", txtParentItemName)

        MainClass.ButtonStatus(Me, XRIGHT, RsItemMast, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume	
    End Sub
    Private Function UpdateItemPara(xCompanyCode As Long) As Boolean
        On Error GoTo ErrSave
        Dim cntRow As Short
        Dim mCompanyCode As Integer
        Dim mRights As String
        Dim SqlStr As String
        Dim mIsNew As Boolean
        Dim mStatus As String
        Dim mMaxQty As Double
        Dim mMinQty As Double
        Dim mInventoryDays As Double
        Dim mReOrderQty As Double
        Dim mAutoQc As String
        If Trim(txtItemCode.Text) = "" Then Exit Function

        UpdateItemPara = False

        For cntRow = 1 To SprdMain.MaxRows

            SprdMain.Row = cntRow

            SprdMain.Col = ColCompanyCode
            mCompanyCode = Val(SprdMain.Text)

            SprdMain.Col = ColAutoQC
            mAutoQc = IIf(SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

            SprdMain.Col = ColMaxQty
            mMaxQty = Val(SprdMain.Text)


            SprdMain.Col = ColMinQty
            mMinQty = Val(SprdMain.Text)

            SprdMain.Col = ColInvDays
            mInventoryDays = Val(SprdMain.Text)

            SprdMain.Col = ColReorderQty
            mReOrderQty = Val(SprdMain.Text)

            SqlStr = ""
            SqlStr = "UPDATE INV_ITEM_MST SET AUTO_QC='" & mAutoQc & "'," & vbCrLf _
               & " MINIMUM_QTY = " & mMinQty & ", MAXIMUM_QTY = " & mMaxQty & ", REORDER_QTY=" & mReOrderQty & ", ECONOMIC_QTY=" & mInventoryDays & "" & vbCrLf _
               & " WHERE COMPANY_CODE=" & mCompanyCode & " AND ITEM_CODE ='" & MainClass.AllowSingleQuote(txtItemCode.Text) & "'"

            PubDBCn.Execute(SqlStr)

        Next

        UpdateItemPara = True
        Exit Function
ErrSave:

    End Function
    Private Sub ShowItemPara()

        On Error GoTo Errshow1
        Dim cntRow As Short
        Dim mDivisionCode As String
        Dim SqlStr As String
        Dim RsItemPara As ADODB.Recordset
        Dim mAutoQc As String

        SSTInfo.SelectedIndex = 3

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf _
           & " FROM INV_ITEM_MST "

        If CheckConsolidatedMaster("INV_ITEM_MST") = True Then
            SqlStr = SqlStr & vbCrLf & " WHERE ITEM_CODE ='" & MainClass.AllowSingleQuote(txtItemCode.Text) & "'"
        Else
            SqlStr = SqlStr & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_CODE ='" & MainClass.AllowSingleQuote(txtItemCode.Text) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY COMPANY_CODE "


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsItemPara, ADODB.LockTypeEnum.adLockOptimistic)


        If RsItemPara.EOF = False Then
            RsItemPara.MoveFirst()
            Do While Not RsItemPara.EOF
                mDivisionCode = IIf(IsDBNull(RsItemPara.Fields("COMPANY_CODE").Value), "", RsItemPara.Fields("COMPANY_CODE").Value)

                For cntRow = 1 To SprdMain.MaxRows
                    SprdMain.Row = cntRow
                    SprdMain.Col = ColCompanyCode
                    If Val(SprdMain.Text) = IIf(IsDBNull(RsItemPara.Fields("COMPANY_CODE").Value), -1, RsItemPara.Fields("COMPANY_CODE").Value) Then

                        mAutoQc = IIf(IsDBNull(RsItemPara.Fields("AUTO_QC").Value), 0, RsItemPara.Fields("AUTO_QC").Value)

                        SprdMain.Col = ColAutoQC
                        SprdMain.Value = IIf(mAutoQc = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                        SprdMain.Col = ColMaxQty
                        SprdMain.Text = IIf(IsDBNull(RsItemPara.Fields("MAXIMUM_QTY").Value), 0, RsItemPara.Fields("MAXIMUM_QTY").Value)

                        SprdMain.Col = ColMinQty
                        SprdMain.Text = IIf(IsDBNull(RsItemPara.Fields("MINIMUM_QTY").Value), 0, RsItemPara.Fields("MINIMUM_QTY").Value)

                        SprdMain.Col = ColInvDays
                        SprdMain.Text = IIf(IsDBNull(RsItemPara.Fields("ECONOMIC_QTY").Value), 0, RsItemPara.Fields("ECONOMIC_QTY").Value)

                        SprdMain.Col = ColReorderQty
                        SprdMain.Text = IIf(IsDBNull(RsItemPara.Fields("REORDER_QTY").Value), 0, RsItemPara.Fields("REORDER_QTY").Value)
                        Exit For
                    End If
                Next
                RsItemPara.MoveNext()
                '            k = k + 1			

            Loop
        End If
        SSTInfo.SelectedIndex = 0
        Exit Sub
Errshow1:
        MsgBox(Err.Description)

    End Sub
    Private Sub FillCompany()

        On Error GoTo ErrFillMenu
        Dim RsDIVISION As ADODB.Recordset = Nothing
        Dim mRow As Integer
        Dim SqlStr As String = ""

        mRow = 1
        SqlStr = " SELECT TO_CHAR(COMPANY_CODE) COMPANY_CODE, COMPANY_NAME " & vbCrLf _
           & " FROM GEN_COMPANY_MST "

        If CheckConsolidatedMaster("INV_ITEM_MST") = False Then
            SqlStr = SqlStr & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " "
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY COMPANY_CODE "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDIVISION, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsDIVISION.EOF Then
            'SprdMain.MaxRows = RsModules.RecordCount				
            FormatSprdMain(-1)
            Do While Not RsDIVISION.EOF
                SprdMain.Row = mRow

                SprdMain.Col = ColCompanyCode
                SprdMain.Text = RsDIVISION.Fields("COMPANY_CODE").Value

                SprdMain.Col = ColCompanyName
                SprdMain.Text = RsDIVISION.Fields("COMPANY_NAME").Value
                RsDIVISION.MoveNext()
                If RsDIVISION.EOF = False Then
                    mRow = mRow + 1
                    SprdMain.MaxRows = mRow
                End If
            Loop
            FormatSprdMain(-1)
        End If
        Exit Sub
ErrFillMenu:
        MsgBox(Err.Description)
    End Sub

    'Private Sub FormatSprdView()

    '    With SprdView
    '        .Row = -1
    '        .set_RowHeight(0, 500)
    '        .set_ColWidth(0, 500)
    '        .set_ColWidth(1, 1500)
    '        .set_ColWidth(2, 4500)
    '        .set_ColWidth(3, 2000)
    '        .set_ColWidth(4, 800)
    '        .set_ColWidth(5, 800)
    '        .set_ColWidth(6, 800)
    '        .set_ColWidth(7, 800)
    '        .set_ColWidth(8, 4500)
    '        .set_ColWidth(9, 4500)
    '        .set_ColWidth(10, 4500)
    '        .ColsFrozen = 1

    '        MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
    '        MainClass.SetSpreadColor(SprdView, -1)
    '        .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
    '        MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
    '    End With
    'End Sub


    Private Sub SetTextLengths()

        On Error GoTo ERR1

        txtItemName.MaxLength = RsItemMast.Fields("ITEM_SHORT_DESC").DefinedSize
        txtParentItemName.MaxLength = RsItemMast.Fields("ITEM_SHORT_DESC").DefinedSize
        txtItemCode.MaxLength = RsItemMast.Fields("ITEM_CODE").DefinedSize
        txtGUID.MaxLength = RsItemMast.Fields("GROUP_ITEM_CODE").DefinedSize
        txtItemUom.MaxLength = RsItemMast.Fields("ISSUE_UOM").DefinedSize
        txtPurchaseUom.MaxLength = RsItemMast.Fields("PURCHASE_UOM").DefinedSize
        txtUOMFactor.MaxLength = RsItemMast.Fields("UOM_FACTOR").DefinedSize
        txtCatName.MaxLength = MainClass.SetMaxLength("GEN_DESC", "INV_GENERAL_MST", PubDBCn)
        txtSubCatName.MaxLength = MainClass.SetMaxLength("SUBCATEGORY_DESC", "INV_SUBCATEGORY_MST", PubDBCn)
        txtPartNo.MaxLength = RsItemMast.Fields("CUSTOMER_PART_NO").DefinedSize
        txtOldPartNo.MaxLength = RsItemMast.Fields("OLD_CUSTOMER_PART_NO").DefinedSize
        txtLeadTime.MaxLength = RsItemMast.Fields("LEAD_TIME").Precision
        txtItemClassQnty.MaxLength = RsItemMast.Fields("ITEM_CLASS_QTY").Precision
        txtPurchaseCost.MaxLength = RsItemMast.Fields("PURCHASE_COST").Precision
        txtSaleCost.MaxLength = RsItemMast.Fields("ITEM_STD_COST").Precision
        txtMinQnty.MaxLength = RsItemMast.Fields("MINIMUM_QTY").Precision
        txtEcoQnty.MaxLength = RsItemMast.Fields("ECONOMIC_QTY").Precision
        txtMaxQnty.MaxLength = RsItemMast.Fields("MAXIMUM_QTY").Precision
        txtReQnty.MaxLength = RsItemMast.Fields("REORDER_QTY").Precision
        txtJWUOM.MaxLength = RsItemMast.Fields("ITEM_JW_UOM").DefinedSize

        txtTechnicalDescription.MaxLength = RsItemMast.Fields("ITEM_TECH_DESC").DefinedSize
        txtDwgNo.MaxLength = RsItemMast.Fields("DRAWING_NO").DefinedSize
        txtDwgRevNo.MaxLength = RsItemMast.Fields("DRW_REVNO").DefinedSize
        txtDwgRevDate.MaxLength = 10
        'txtInspectionNo	
        txtIdMark.MaxLength = RsItemMast.Fields("IDENT_MARK").DefinedSize
        txtSurfaceArea.MaxLength = RsItemMast.Fields("ITEM_SURFACE_AREA").Precision

        txtAddSurfaceAreaPPS.MaxLength = RsItemMast.Fields("ADD_PPS_SURFACE_AREA").Precision
        txtAddSurfaceAreaNPC.MaxLength = RsItemMast.Fields("ADD_NPC_SURFACE_AREA").Precision
        txtAddSurfaceAreaPLT.MaxLength = RsItemMast.Fields("ADD_PLT_SURFACE_AREA").Precision

        txtModel.MaxLength = MainClass.SetMaxLength("MODEL_DESC", "GEN_MODEL_MST", PubDBCn) ''RsItemMast.Fields("ITEM_MODEL").DefinedSize
        txtProdType.MaxLength = RsItemMast.Fields("PRODTYPE_DESC").DefinedSize
        txtItemMake.MaxLength = RsItemMast.Fields("ITEM_MAKE").DefinedSize
        txtColor.MaxLength = RsItemMast.Fields("ITEM_COLOR").DefinedSize
        txtWeight.MaxLength = RsItemMast.Fields("ITEM_WEIGHT").DefinedSize
        txtDimention.MaxLength = RsItemMast.Fields("ITEM_DIMENSIONS").DefinedSize
        txtSpecification.MaxLength = RsItemMast.Fields("ITEM_QAS_NO").DefinedSize
        txtPackingStandard.MaxLength = RsItemMast.Fields("PACK_STD").Precision
        txtScrapItemCode.MaxLength = MainClass.SetMaxLength("ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn)

        txtScrapWeight.MaxLength = RsItemMast.Fields("SHEAR_SCRAP_WGT").Precision

        txtPackingItemCode.MaxLength = MainClass.SetMaxLength("ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn)
        txtWLength.MaxLength = RsItemMast.Fields("ITEM_WLENGTH").Precision

        txtWLengthCust.MaxLength = RsItemMast.Fields("ITEM_WLENGTH_CUST").Precision
        txtSSWLengthCust.MaxLength = RsItemMast.Fields("ITEM_SS_WLENGTH_CUST").Precision

        txtTacks.MaxLength = RsItemMast.Fields("ITEM_TACKS").Precision
        txtLocation.MaxLength = RsItemMast.Fields("ITEM_LOCATION").DefinedSize
        txtMaterial.MaxLength = RsItemMast.Fields("MAT_DESC").DefinedSize
        txtLength.MaxLength = RsItemMast.Fields("MAT_LEN").Precision
        txtWidth.MaxLength = RsItemMast.Fields("MAT_WIDTH").Precision
        txtThickness.MaxLength = RsItemMast.Fields("MAT_THICHNESS").Precision
        txtDensity.MaxLength = RsItemMast.Fields("MAT_DENSITY").DefinedSize
        txtWtPerStrip.MaxLength = RsItemMast.Fields("WT_PER_STRIP").Precision

        txtTIGLen.MaxLength = RsItemMast.Fields("TIG_WLENGTH").Precision
        txtBrazingLen.MaxLength = RsItemMast.Fields("BRAZING_WLENGTH").Precision
        txtSeamLen.MaxLength = RsItemMast.Fields("SEAM_WLENGTH").Precision
        txtSpotNos.MaxLength = RsItemMast.Fields("SPOT_NOS").Precision

        txtSSWLength.MaxLength = RsItemMast.Fields("ITEM_SS_WLENGTH").Precision
        txtSurfaceAreaInner.MaxLength = RsItemMast.Fields("ITEM_SURFACE_AREA_IN").Precision

        txtHSNCode.MaxLength = RsItemMast.Fields("HSN_CODE").DefinedSize
        txtPackType.MaxLength = RsItemMast.Fields("PACK_TYPE").DefinedSize

        '    txtSurfaceTreatment.MaxLength = RsItemMast.Fields("SURFACE_TREATMENT").DefinedSize	

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume	
    End Sub

    Function FieldVarification() As Boolean
        On Error GoTo err_Renamed
        Dim mProdType As String = ""
        Dim mCategoryCode As String = ""

        FieldVarification = True
        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a New Account Or modify an existing item")
            FieldVarification = False
            Exit Function
        End If

        If MODIFYMode = True Then
            If IsDate(lblAddDate.Text) = False Then
                lblAddDate.Text = RsCompany.Fields("Start_Date").Value
            End If
            If MainClass.GetUserCanModifyMaster(lblAddDate.Text, XRIGHT) = False Then
                MsgBox("You Have Not Rights to change Item Master.", vbInformation)
                FieldVarification = False
                Exit Function
            End If
        End If

        If txtItemName.Text = "" Then
            MsgInformation("Item Name Is empty. Cannot Save")
            If txtItemName.Enabled = True Then txtItemName.Focus()
            FieldVarification = False
            Exit Function
        End If

        Dim mAutoGenCode = IIf(IsDBNull(RsCompany.Fields("AUTO_GEN_CODE").Value), "N", RsCompany.Fields("AUTO_GEN_CODE").Value)

        If mAutoGenCode = "N" Then
            If Trim(txtItemCode.Text) = "" Then
                MsgInformation("Item Code Is empty. Cannot Save")
                txtItemCode.Focus()
                FieldVarification = False
                Exit Function
            End If
        End If

        'If ADDMode = True Then
        '    If Len(Trim(txtItemCode.Text)) <> 6 Then
        '        MsgInformation("Item Code must be six digit. Cannot Save")
        '        txtItemCode.Focus()
        '        FieldVarification = False
        '        Exit Function
        '    End If
        'End If

        If Trim(txtItemUom.Text) = "" Then
            MsgInformation("Item UOM Is empty. Cannot Save")
            txtItemUom.Focus()
            FieldVarification = False
            Exit Function
        End If

        If Trim(txtPurchaseUom.Text) = "" Then
            MsgInformation("Purchase UOM Is empty. Cannot Save")
            txtPurchaseUom.Focus()
            FieldVarification = False
            Exit Function
        End If

        If Trim(txtUOMFactor.Text) = "" Or Val(txtUOMFactor.Text) = 0 Then
            MsgInformation("Factor UOM Is empty. Cannot Save")
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



        If Trim(txtItemUom.Text) = "KGS" Or Trim(txtItemUom.Text) = "TON" Or Trim(txtItemUom.Text) = "MT" Then
            If Val(txtWeight.Text) > 0 Then
                If Val(txtWeight.Text) <> 1000 Then
                    MsgInformation("Please Check Item Weight it should be 1000 Gms. Cannot Save")
                    If txtWeight.Enabled = True Then txtWeight.Focus()
                    FieldVarification = False
                    Exit Function
                End If
            ElseIf Val(txtWeight.Text) < 0 Then
                MsgInformation("Please Check Item Weight. Cannot Save")
                If txtWeight.Enabled = True Then txtWeight.Focus()
                FieldVarification = False
                Exit Function
            End If
        End If


        If Trim(txtJWUOM.Text) = "" Then
            txtJWUOM.Text = txtItemUom.Text
        End If
        If Trim(txtJWUOM.Text) = "" Then
            MsgInformation("J/W Rate UOM Is empty. Cannot Save")
            txtJWUOM.Focus()
            FieldVarification = False
            Exit Function
        End If

        If Trim(txtCatName.Text) = "" Then
            MsgInformation("Category Name Is empty. Cannot Save")
            txtCatName.Focus()
            FieldVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable(txtCatName.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " And GEN_TYPE='C'") = False Then
            MsgInformation("Invalid Category Name. Cannot Save")
            txtCatName.Focus()
            FieldVarification = False
            Exit Function
        Else
            mCategoryCode = MasterNo
        End If

        If MainClass.ValidateWithMasterTable(txtCatName.Text, "GEN_CODE", "PRD_TYPE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
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

        If MainClass.ValidateWithMasterTable(txtSubCatName.Text, "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCategoryCode & "' ") = False Then
            MsgInformation("Invalid Sub Category Name. Cannot Save")
            txtSubCatName.Focus()
            FieldVarification = False
            Exit Function
        End If

        If CboStatus.SelectedIndex = 1 Then
            If MainClass.ValidateWithMasterTable(txtItemCode.Text, "PARENT_CODE", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                MsgInformation("Item is Defined as Parent Code so Cann't be Inactive. Cannot Save")
                FieldVarification = False
                Exit Function
            End If
        End If

        If Val(txtWLength.Text) <> 0 And CDbl(VB.Left(cboWeldingLine.Text, 1)) = 0 Then
            MsgInformation("Please select Welding Line. Cannot Save")
            SSTInfo.SelectedIndex = 1
            cboWeldingLine.Focus()
            FieldVarification = False
            Exit Function
        End If

        If Val(txtWLengthCust.Text) <> 0 And CDbl(VB.Left(cboWeldingLine.Text, 1)) = 0 Then
            MsgInformation("Please select Customer Welding Line. Cannot Save")
            SSTInfo.SelectedIndex = 1
            cboWeldingLine.Focus()
            FieldVarification = False
            Exit Function
        End If

        If chkChildItem.CheckState = System.Windows.Forms.CheckState.Checked Then
            If MainClass.ValidateWithMasterTable(txtItemCode.Text, "PARENT_CODE", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                MsgInformation("Item is Defined as Parent Code so Cann't defined Child. Cannot Save")
                FieldVarification = False
                Exit Function
            End If

            If Trim(txtParentItemName.Text) = "" Then
                MsgInformation("Please Select Parent Item Name. Cannot Save")
                FieldVarification = False
                Exit Function
            End If

            If MainClass.ValidateWithMasterTable(txtParentItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invalid Parent Item. Cannot Save")
                FieldVarification = False
                Exit Function
            End If

            If MainClass.ValidateWithMasterTable(txtParentItemName.Text, "ITEM_SHORT_DESC", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                If Trim(MasterNo) <> Trim(txtItemUom.Text) Then
                    MsgInformation("Parent Issue UOM not Match with Item Issue UOM. Cannot Save")
                    FieldVarification = False
                    Exit Function
                End If
            End If
        End If


        Dim pCategoryType As String = ""
        Dim SqlStr As String = ""

        If MainClass.ValidateWithMasterTable(txtCatName.Text, "GEN_DESC", "PRD_TYPE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
            pCategoryType = MasterNo
        End If

        If pCategoryType = "S" Then
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'"
        Else
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='G'"
        End If

        If txtHSNCode.Text <> "" Then
            If MainClass.ValidateWithMasterTable(txtHSNCode.Text, "HSN_CODE", "HSN_DESC", "GEN_HSN_MST", PubDBCn, MasterNo, , SqlStr) = False Then
                MsgInformation("Invalid HSN Code. Cannot Save")
                FieldVarification = False
                Exit Function
            Else
                lblHSNName.Text = MasterNo
            End If
        End If

        If chkStockItem.CheckState = System.Windows.Forms.CheckState.Checked And pCategoryType = "S" Then
            'chkStockItem.CheckState = System.Windows.Forms.CheckState.Unchecked
            MsgInformation("This is Service Category, Please uncheck Stock Item.")
            FieldVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable(txtCatName.Text, "GEN_CODE", "PRD_TYPE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
            mProdType = Trim(MasterNo)
        End If

        If mProdType = "P" Or mProdType = "R" Or mProdType = "I" Or mProdType = "B" Then
            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then

            Else
                If Val(txtWeight.Text) <= 0 Then
                    MsgInformation("Please Enter the Component Weight. Cannot Save.")
                    SSTInfo.SelectedIndex = 1
                    txtWeight.Focus()
                    FieldVarification = False
                    Exit Function
                End If

                '        If Val(txtScrapWeight.Text) <= 0 Then	
                '            MsgInformation "Please Enter the Scrap Weight. Cannot Save."	
                '            SSTInfo.Tab = 1	
                '            txtScrapWeight.SetFocus	
                '            FieldVarification = False	
                '            Exit Function	
                '        End If	

                If Trim(txtScrapItemCode.Text) = "" Then
                    MsgInformation("Please Enter the Scrap Item Code. Cannot Save.")
                    txtScrapItemCode.Focus()
                    FieldVarification = False
                    Exit Function
                End If
            End If


        End If

        Dim mStockType As String = ""

        If MainClass.ValidateWithMasterTable(txtCatName.Text, "GEN_DESC", "PRD_TYPE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
            mProdType = Trim(MasterNo)
        End If

        If MainClass.ValidateWithMasterTable(txtCatName.Text, "GEN_DESC", "STOCKTYPE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
            mStockType = Trim(MasterNo)
        End If

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 And mProdType = "P" And mStockType = "FG" Then

            If Val(txtLength.Text) <= 0 Then
                MsgInformation("Please Enter the Length. Cannot Save.")
                FieldVarification = False
                Exit Function
            End If

            'If Val(txtWeight.Text) <= 0 Then
            '    MsgInformation("Please Enter the Weight. Cannot Save.")
            '    FieldVarification = False
            '    Exit Function
            'End If

            If Val(txtThickness.Text) <= 0 Then
                MsgInformation("Please Enter the Thickness. Cannot Save.")
                FieldVarification = False
                Exit Function
            End If

            If Trim(txtColor.Text) = "" Then
                MsgInformation("Please Enter the Color. Cannot Save.")
                FieldVarification = False
                Exit Function
            End If

        End If

        '    If Trim(txtHSNCode.Text) = "" Then	
        '        MsgInformation "Please Enter the HSN Code. Cannot Save."	
        '        txtHSNCode.SetFocus	
        '        FieldVarification = False	
        '        Exit Function	
        '    End If	
        '	
        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function

    Private Sub frmItemMaster_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        Me.Hide()
        Me.Close()
        RsItemMast.Close()
        'RsOpOuts.Close	
    End Sub

    Private Sub UltraGrid1_DoubleClick(sender As Object, e As EventArgs) Handles UltraGrid1.DoubleClick

        Dim mItemName As String
        Dim mAmendNo As String

        Dim mRow As UltraGridRow

        If Me.UltraGrid1.ActiveRow.Index < 0 Then Exit Sub
        mRow = Me.UltraGrid1.Rows(Me.UltraGrid1.ActiveRow.Index)

        mItemName = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(1))

        txtItemName.Text = mItemName

        TxtItemName_Validating(txtItemName, New System.ComponentModel.CancelEventArgs(True))
        CmdView_Click(CmdView, New System.EventArgs())

    End Sub

    'Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent)
    '    If eventArgs.row < 1 Then Exit Sub

    '    SprdView.Col = 2
    '    SprdView.Row = eventArgs.row
    '    txtItemName.Text = Trim(SprdView.Text)
    '    TxtItemName_Validating(txtItemName, New System.ComponentModel.CancelEventArgs(True))
    '    CmdView_Click(CmdView, New System.EventArgs())
    'End Sub
    'Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent)
    '    If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    'End Sub

    Private Sub txtGUID_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGUID.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAddSurfaceAreaNPC_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAddSurfaceAreaNPC.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAddSurfaceAreaNPC_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAddSurfaceAreaNPC.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtAddSurfaceAreaPLT_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAddSurfaceAreaPLT.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAddSurfaceAreaPLT_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAddSurfaceAreaPLT.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtAddSurfaceAreaPPS_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAddSurfaceAreaPPS.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAddSurfaceAreaPPS_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAddSurfaceAreaPPS.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCatName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCatName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        txtSubCatName.Text = ""
    End Sub
    Private Sub txtCatName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCatName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
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
    Private Sub txtCatName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCatName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCatName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtGUID_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtGUID.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtGUID.Text, "N")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtHSNCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtHSNCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
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
    End Sub

    Private Sub txtHSNCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtHSNCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim pCategoryType As String = ""
        Dim SqlStr As String = ""

        If Trim(txtHSNCode.Text) = "" Then lblHSNName.Text = "" : GoTo EventExitSub

        If txtCatName.Text = "" Then
            MsgInformation("Please Select The Category.")
            txtHSNCode.Text = ""
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(txtCatName.Text, "GEN_DESC", "PRD_TYPE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
            pCategoryType = MasterNo
        End If

        If pCategoryType = "S" Then
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'"
        Else
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='G'"
        End If

        If MainClass.ValidateWithMasterTable(txtHSNCode.Text, "HSN_CODE", "HSN_DESC", "GEN_HSN_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            ErrorMsg("Invalid HSN Code.", , vbInformation)
            Cancel = True
        Else
            lblHSNName.Text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtPackType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPackType.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtPackType_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPackType.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPackType.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPackType_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPackType.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
    End Sub

    Private Sub txtPackType_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPackType.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtPackType.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtPackType.Text, "NAME", "NAME", "DSP_PACKINGTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            ErrorMsg("Invalid Packing Type.", , vbInformation)
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtItemCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtItemCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItemCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtItemCode.Text, "N")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
        'FillComboItemCode()
    End Sub
    Private Sub txtItemCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtItemCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
    End Sub

    Public Sub txtItemCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtItemCode.Text) = "" Then GoTo EventExitSub
        If MODIFYMode = True And RsItemMast.EOF = False Then mItemCode = RsItemMast.Fields("ITEM_CODE").Value
        SqlStr = "Select * From INV_ITEM_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " And ITEM_CODE='" & MainClass.AllowSingleQuote(txtItemCode.Text) & "'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsItemMast, ADODB.LockTypeEnum.adLockReadOnly)

        If RsItemMast.EOF = False Then
            ADDMode = False
            MODIFYMode = False

            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Name Does Not Exist In Master, Click Add To Add In Master", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * From INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ITEM_CODE=" & mItemCode & ""
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsItemMast, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtCatName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCatName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim mCatCode As String = ""

        If Trim(txtCatName.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtCatName.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = False Then
            ErrorMsg("Invalid Category Name.", , vbInformation)
            Cancel = True
        Else
            mCatCode = MasterNo
        End If

        If MainClass.ValidateWithMasterTable(txtCatName.Text, "GEN_DESC", "PRD_TYPE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
            If MasterNo = "T" Then
                cboItemClassification.Text = "Tool"
            ElseIf MasterNo = "A" Then
                cboItemClassification.Text = "Assets"
            End If
        End If

        Call AutoCompleteSearch("INV_SUBCATEGORY_MST", "SUBCATEGORY_DESC", "CATEGORY_CODE='" & mCatCode & "'", txtSubCatName)

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

    Private Sub txtItemUom_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtItemUom.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
    End Sub

    Private Sub txtItemUom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemUom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If Trim(txtItemUom.Text) = "" Then lblItemUom.Text = "" : GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtItemUom.Text, "GEN_CODE", "GEN_DESC", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='U'") = False Then
            ErrorMsg("Invalid UOM.", , vbInformation)
            Cancel = True
        Else
            lblItemUom.Text = MasterNo
            If ADDMode = True Then
                txtPurchaseUom.Text = txtItemUom.Text
                lblPurUom.Text = MasterNo
                txtUOMFactor.Text = 1
            Else
                If Trim(txtPurchaseUom.Text) = "" Then
                    txtPurchaseUom.Text = txtItemUom.Text
                    lblPurUom.Text = MasterNo
                    txtUOMFactor.Text = 1
                End If
            End If

        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtItemName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtItemName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItemName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtItemName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If

        'FillComboItemName()

    End Sub
    Private Sub txtItemName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtItemName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchAccounts()
    End Sub

    Private Sub TxtItemName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtItemName.Text) = "" Then GoTo EventExitSub
        If MODIFYMode = True And RsItemMast.EOF = False Then mItemCode = RsItemMast.Fields("ITEM_CODE").Value

        SqlStr = "Select * From INV_ITEM_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND LTRIM(RTRIM(UPPER(ITEM_SHORT_DESC)))='" & MainClass.AllowSingleQuote(UCase(txtItemName.Text)) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsItemMast, ADODB.LockTypeEnum.adLockReadOnly)

        If RsItemMast.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Name Does Not Exist In Master, Click Add To Add In Master", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * From INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ITEM_CODE='" & mItemCode & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsItemMast, ADODB.LockTypeEnum.adLockReadOnly)
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
        Dim mSurfaceTreated As String
        Dim mWeldLine As Integer
        Dim mGSTItemClass As Integer
        Dim mCatCode As String = ""
        Dim mSubCatCode As String = ""
        Dim mPackingCode As String = ""
        Dim mScrapItemCode As String = ""
        Dim mModelCode As String

        Clear1()
        If Not RsItemMast.EOF Then

            mItemCode = IIf(IsDBNull(RsItemMast.Fields("ITEM_CODE").Value), -1, RsItemMast.Fields("ITEM_CODE").Value)
            txtItemName.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("ITEM_SHORT_DESC").Value), "", RsItemMast.Fields("ITEM_SHORT_DESC").Value))
            txtItemCode.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("ITEM_CODE").Value), "", RsItemMast.Fields("ITEM_CODE").Value))
            txtItemCode.Enabled = False

            txtGUID.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("GROUP_ITEM_CODE").Value), "", RsItemMast.Fields("GROUP_ITEM_CODE").Value))


            chkChildItem.CheckState = IIf((RsItemMast.Fields("IS_CHILD").Value = "Y"), System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            If chkChildItem.CheckState = System.Windows.Forms.CheckState.Checked Then
                If RsItemMast.Fields("PARENT_CODE").Value = "" Or IsDBNull(RsItemMast.Fields("PARENT_CODE").Value) Then
                    txtParentItemName.Text = ""
                Else
                    If MainClass.ValidateWithMasterTable(RsItemMast.Fields("PARENT_CODE").Value, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IS_CHILD='N'") = True Then
                        txtParentItemName.Text = MasterNo
                    End If
                End If
            Else
                txtParentItemName.Text = ""
            End If
            txtParentItemName.Enabled = IIf(chkChildItem.CheckState = System.Windows.Forms.CheckState.Checked, True, False)

            txtItemUom.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("ISSUE_UOM").Value), "", RsItemMast.Fields("ISSUE_UOM").Value))
            If MainClass.ValidateWithMasterTable(txtItemUom.Text, "GEN_CODE", "GEN_DESC", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='U'") = True Then
                lblItemUom.Text = MasterNo
            End If

            txtPurchaseUom.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("PURCHASE_UOM").Value), "", RsItemMast.Fields("PURCHASE_UOM").Value))
            If MainClass.ValidateWithMasterTable(txtPurchaseUom.Text, "GEN_CODE", "GEN_DESC", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='U'") = True Then
                lblPurUom.Text = MasterNo
            End If



            txtUOMFactor.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("UOM_FACTOR").Value), "", RsItemMast.Fields("UOM_FACTOR").Value))

            txtJWUOM.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("ITEM_JW_UOM").Value), "", RsItemMast.Fields("ITEM_JW_UOM").Value))

            mCatCode = Trim(IIf(IsDBNull(RsItemMast.Fields("CATEGORY_CODE").Value), "", RsItemMast.Fields("CATEGORY_CODE").Value))
            If MainClass.ValidateWithMasterTable(mCatCode, "GEN_CODE", "GEN_DESC", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                txtCatName.Text = MasterNo
            End If

            mSubCatCode = Trim(IIf(IsDBNull(RsItemMast.Fields("SUBCATEGORY_CODE").Value), "", RsItemMast.Fields("SUBCATEGORY_CODE").Value))
            If MainClass.ValidateWithMasterTable(mSubCatCode, "SUBCATEGORY_CODE", "SUBCATEGORY_DESC", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCatCode & "' ") = True Then
                txtSubCatName.Text = MasterNo
            End If


            mPackingCode = Trim(IIf(IsDBNull(RsItemMast.Fields("PACK_ITEM_CODE").Value), "", RsItemMast.Fields("PACK_ITEM_CODE").Value))
            If mPackingCode <> "" Then
                If MainClass.ValidateWithMasterTable(mPackingCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value) = True Then
                    txtPackingItemCode.Text = MasterNo
                End If
            End If


            mScrapItemCode = Trim(IIf(IsDBNull(RsItemMast.Fields("SCRAP_ITEM_CODE").Value), "", RsItemMast.Fields("SCRAP_ITEM_CODE").Value))
            If mScrapItemCode <> "" Then
                If MainClass.ValidateWithMasterTable(mScrapItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value) = True Then
                    txtScrapItemCode.Text = MasterNo
                End If
            End If

            txtPartNo.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("CUSTOMER_PART_NO").Value), "", RsItemMast.Fields("CUSTOMER_PART_NO").Value))
            txtOldPartNo.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("OLD_CUSTOMER_PART_NO").Value), "", RsItemMast.Fields("OLD_CUSTOMER_PART_NO").Value))
            txtLeadTime.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("LEAD_TIME").Value), "", RsItemMast.Fields("LEAD_TIME").Value))
            txtItemClassQnty.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("ITEM_CLASS_QTY").Value), "", RsItemMast.Fields("ITEM_CLASS_QTY").Value))
            txtPurchaseCost.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("PURCHASE_COST").Value), "", RsItemMast.Fields("PURCHASE_COST").Value))
            txtSaleCost.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("ITEM_STD_COST").Value), "", RsItemMast.Fields("ITEM_STD_COST").Value))
            txtMinQnty.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("MINIMUM_QTY").Value), "", RsItemMast.Fields("MINIMUM_QTY").Value))
            txtEcoQnty.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("ECONOMIC_QTY").Value), "", RsItemMast.Fields("ECONOMIC_QTY").Value))
            txtMaxQnty.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("MAXIMUM_QTY").Value), "", RsItemMast.Fields("MAXIMUM_QTY").Value))
            txtReQnty.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("REORDER_QTY").Value), "", RsItemMast.Fields("REORDER_QTY").Value))
            txtTechnicalDescription.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("ITEM_TECH_DESC").Value), "", RsItemMast.Fields("ITEM_TECH_DESC").Value))
            txtDwgNo.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("DRAWING_NO").Value), "", RsItemMast.Fields("DRAWING_NO").Value))
            txtDwgRevNo.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("DRW_REVNO").Value), "", RsItemMast.Fields("DRW_REVNO").Value))
            txtDwgRevDate.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("DRW_REVEFF_DATE").Value), "", RsItemMast.Fields("DRW_REVEFF_DATE").Value))
            txtInspectionNo.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("ITEM_QAS_NO").Value), "", RsItemMast.Fields("ITEM_QAS_NO").Value))
            txtIdMark.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("IDENT_MARK").Value), "", RsItemMast.Fields("IDENT_MARK").Value))
            txtSurfaceArea.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("ITEM_SURFACE_AREA").Value), "", RsItemMast.Fields("ITEM_SURFACE_AREA").Value))

            txtAddSurfaceAreaPPS.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("ADD_PPS_SURFACE_AREA").Value), "", RsItemMast.Fields("ADD_PPS_SURFACE_AREA").Value))
            txtAddSurfaceAreaNPC.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("ADD_NPC_SURFACE_AREA").Value), "", RsItemMast.Fields("ADD_NPC_SURFACE_AREA").Value))
            txtAddSurfaceAreaPLT.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("ADD_PLT_SURFACE_AREA").Value), "", RsItemMast.Fields("ADD_PLT_SURFACE_AREA").Value))

            mModelCode = Trim(IIf(IsDBNull(RsItemMast.Fields("ITEM_MODEL").Value), "", RsItemMast.Fields("ITEM_MODEL").Value))

            If mModelCode = "" Then
                txtModel.Text = ""
            Else
                If MainClass.ValidateWithMasterTable(mModelCode, "MODEL_CODE", "MODEL_DESC", "GEN_MODEL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value) = True Then
                    txtModel.Text = MasterNo
                End If
            End If

            'txtModel.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("ITEM_MODEL").Value), "", RsItemMast.Fields("ITEM_MODEL").Value))



            txtProdType.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("PRODTYPE_DESC").Value), "", RsItemMast.Fields("PRODTYPE_DESC").Value))

            txtItemMake.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("ITEM_MAKE").Value), "", RsItemMast.Fields("ITEM_MAKE").Value))
            txtColor.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("ITEM_COLOR").Value), "", RsItemMast.Fields("ITEM_COLOR").Value))
            txtWeight.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("ITEM_WEIGHT").Value), "", RsItemMast.Fields("ITEM_WEIGHT").Value))
            txtDimention.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("ITEM_DIMENSIONS").Value), "", RsItemMast.Fields("ITEM_DIMENSIONS").Value))

            txtPackingStandard.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("PACK_STD").Value), "", RsItemMast.Fields("PACK_STD").Value))
            txtScrapWeight.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("SHEAR_SCRAP_WGT").Value), "", RsItemMast.Fields("SHEAR_SCRAP_WGT").Value))



            chkConsumable.CheckState = IIf(Trim(RsItemMast.Fields("CONSUMABLE_FLAG").Value) = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkDrawing.CheckState = IIf(Trim(RsItemMast.Fields("SEMI_FIN_ITEM_CODE").Value) = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkAutoIssue.CheckState = IIf((RsItemMast.Fields("AUTO_INDENT").Value = "Y" Or IsDBNull(RsItemMast.Fields("AUTO_INDENT").Value)), System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            chkAutoQC.CheckState = IIf((RsItemMast.Fields("AUTO_QC").Value = "Y" Or IsDBNull(RsItemMast.Fields("AUTO_QC").Value)), System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkHeatReq.CheckState = IIf((RsItemMast.Fields("HEAT_NO_REQ").Value = "Y" Or IsDBNull(RsItemMast.Fields("HEAT_NO_REQ").Value)), System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)


            chkStockItem.CheckState = IIf(Trim(RsItemMast.Fields("STOCKITEM").Value) = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            If chkStockItem.CheckState = System.Windows.Forms.CheckState.Checked Then
                chkStockItem.Enabled = False
            End If

            chkPOReqd.CheckState = IIf(Trim(RsItemMast.Fields("POREQD").Value) = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkExportItem.CheckState = IIf(Trim(RsItemMast.Fields("IS_EXPORT_ITEM").Value) = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            chkRequired.CheckState = IIf((RsItemMast.Fields("DSP_RPT_FLAG").Value = "Y"), System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            chkMRRLocking.CheckState = IIf((RsItemMast.Fields("MRR_LOCK").Value = "Y"), System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkMRRLockingOM.CheckState = IIf((RsItemMast.Fields("MRR_LOCK_OVERMAX").Value = "Y"), System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkScheduleLocking.CheckState = IIf((RsItemMast.Fields("SCHEDULE_LOCK").Value = "Y"), System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            txtWLength.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("ITEM_WLENGTH").Value), "", RsItemMast.Fields("ITEM_WLENGTH").Value))

            txtWLengthCust.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("ITEM_WLENGTH_CUST").Value), "", RsItemMast.Fields("ITEM_WLENGTH_CUST").Value))
            txtSSWLengthCust.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("ITEM_SS_WLENGTH_CUST").Value), "", RsItemMast.Fields("ITEM_SS_WLENGTH_CUST").Value))

            txtTacks.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("ITEM_TACKS").Value), "", RsItemMast.Fields("ITEM_TACKS").Value))
            txtLocation.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("ITEM_LOCATION").Value), "", RsItemMast.Fields("ITEM_LOCATION").Value))

            txtTIGLen.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("TIG_WLENGTH").Value), "", RsItemMast.Fields("TIG_WLENGTH").Value))
            txtBrazingLen.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("BRAZING_WLENGTH").Value), "", RsItemMast.Fields("BRAZING_WLENGTH").Value))
            txtSeamLen.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("SEAM_WLENGTH").Value), "", RsItemMast.Fields("SEAM_WLENGTH").Value))
            txtSpotNos.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("SPOT_NOS").Value), "", RsItemMast.Fields("SPOT_NOS").Value))
            chkGrinding.CheckState = IIf(Trim(RsItemMast.Fields("IS_GRINDING").Value) = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)


            txtSSWLength.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("ITEM_SS_WLENGTH").Value), "", RsItemMast.Fields("ITEM_SS_WLENGTH").Value))
            txtSurfaceAreaInner.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("ITEM_SURFACE_AREA_IN").Value), "", RsItemMast.Fields("ITEM_SURFACE_AREA_IN").Value))

            txtPackType.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("PACK_TYPE").Value), "", RsItemMast.Fields("PACK_TYPE").Value))
            txtHSNCode.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("HSN_CODE").Value), "", RsItemMast.Fields("HSN_CODE").Value))
            If txtHSNCode.Text <> "" Then
                If MainClass.ValidateWithMasterTable(txtHSNCode.Text, "HSN_CODE", "HSN_DESC", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='G'") = True Then
                    lblHSNName.Text = MasterNo
                    'txtHSNCode.Enabled = False
                    'cmdSearchHSN.Enabled = False
                    'cboGSTClass.Enabled = False
                End If
            End If

            If IIf(IsDBNull(RsItemMast.Fields("ITEM_TYPE").Value), "I", RsItemMast.Fields("ITEM_TYPE").Value) = "I" Then
                CboItemType.SelectedIndex = 1
            Else
                CboItemType.SelectedIndex = 0
            End If

            Select Case RsItemMast.Fields("ITEM_CLASS").Value
                Case "A"
                    CboItemClass.SelectedIndex = 0
                Case "B"
                    CboItemClass.SelectedIndex = 1
                Case "C"
                    CboItemClass.SelectedIndex = 2
                Case "D"
                    CboItemClass.SelectedIndex = 3
            End Select


            Select Case RsItemMast.Fields("ITEM_EXCISE_FLAG").Value
                Case "Y"
                    CboExciseFlag.SelectedIndex = 0
                Case "N"
                    CboExciseFlag.SelectedIndex = 1
            End Select

            Select Case RsItemMast.Fields("ITEM_CLASSIFICATION").Value
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
                Case "S"
                    cboItemClassification.SelectedIndex = 7
                Case "1"
                    cboItemClassification.SelectedIndex = 8
                Case "2"
                    cboItemClassification.SelectedIndex = 9
                Case "3"
                    cboItemClassification.SelectedIndex = 10
                Case "4"
                    cboItemClassification.SelectedIndex = 11
                Case "5"
                    cboItemClassification.SelectedIndex = 12
                Case "6"
                    cboItemClassification.SelectedIndex = 13
                Case "7"
                    cboItemClassification.SelectedIndex = 14
                Case "8"
                    cboItemClassification.SelectedIndex = 15
            End Select


            Select Case RsItemMast.Fields("ITEM_STATUS").Value
                Case "A"
                    CboStatus.SelectedIndex = 0
                Case "I"
                    CboStatus.SelectedIndex = 1
                    CboStatus.Enabled = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)
            End Select

            mGSTItemClass = IIf(IsDBNull(RsItemMast.Fields("GST_ITEMCLASS").Value), 0, RsItemMast.Fields("GST_ITEMCLASS").Value)
            cboGSTClass.SelectedIndex = mGSTItemClass


            txtItemUom.Enabled = IIf(PubSuperUser = "S", True, False)
            txtPurchaseUom.Enabled = IIf(PubSuperUser = "S", True, False)

            '        chkStockItem.Enabled = IIf(PubSuperUser = "S", True, False)	

            txtMaterial.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("MAT_DESC").Value), "", RsItemMast.Fields("MAT_DESC").Value))
            txtLength.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("MAT_LEN").Value), "", RsItemMast.Fields("MAT_LEN").Value))
            txtWidth.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("MAT_WIDTH").Value), "", RsItemMast.Fields("MAT_WIDTH").Value))
            txtThickness.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("MAT_THICHNESS").Value), "", RsItemMast.Fields("MAT_THICHNESS").Value))
            txtWtPerStrip.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("WT_PER_STRIP").Value), "", RsItemMast.Fields("WT_PER_STRIP").Value))
            txtDensity.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("MAT_DENSITY").Value), "", RsItemMast.Fields("MAT_DENSITY").Value))

            '        if left(cboSurfaceTreatment.1	
            mSurfaceTreated = VB.Left(Trim(IIf(IsDBNull(RsItemMast.Fields("SURFACE_TREATMENT").Value), 0, RsItemMast.Fields("SURFACE_TREATMENT").Value)), 1)
            cboSurfaceTreatment.SelectedIndex = Val(mSurfaceTreated)

            '        If mSurfaceTreated = "1" Or mSurfaceTreated = "2" Or mSurfaceTreated = "3" Or mSurfaceTreated = "4" Then	
            '            cboSurfaceTreatment.ListIndex = mSurfaceTreated	
            '        Else	
            '            cboSurfaceTreatment.ListIndex = 0	
            '        End If	

            mWeldLine = Val(VB.Left(Trim(IIf(IsDBNull(RsItemMast.Fields("WELD_LINE").Value), 0, RsItemMast.Fields("WELD_LINE").Value)), 1))
            cboWeldingLine.SelectedIndex = mWeldLine

            mWeldLine = Val(VB.Left(Trim(IIf(IsDBNull(RsItemMast.Fields("PRESS_LINE").Value), 0, RsItemMast.Fields("PRESS_LINE").Value)), 1))
            cboPressLine.SelectedIndex = mWeldLine
            chkSB.CheckState = IIf(Trim(RsItemMast.Fields("IS_SHOTBLASTING").Value) = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)



            '        cboSurfaceTreatment.Text = Trim(IIf(IsNull(RsItemMast!SURFACE_TREATMENT), "", RsItemMast!SURFACE_TREATMENT))	

            lblAddUser.Text = IIf(IsDBNull(RsItemMast.Fields("ADDUSER").Value), "", RsItemMast.Fields("ADDUSER").Value)
            lblAddDate.Text = VB6.Format(IIf(IsDBNull(RsItemMast.Fields("ADDDATE").Value), "", RsItemMast.Fields("ADDDATE").Value), "dd/MM/yyyy")
            lblModUser.Text = IIf(IsDBNull(RsItemMast.Fields("MODUSER").Value), "", RsItemMast.Fields("MODUSER").Value)
            Dim mModDate As String
            mModDate = IIf(IsDBNull(RsItemMast.Fields("MODDATE").Value), "", RsItemMast.Fields("MODDATE").Value)

            If mModDate = "" Then
                lblModDate.Text = ""
            Else
                lblModDate.Text = CDate(mModDate).ToString("dd/MM/yyyy")
            End If


            'mLock = GetItemTransaction()

            If PubSuperUser = "S" Then
                txtItemName.ReadOnly = False
            Else
                If CheckTransactionMade(Trim(txtItemCode.Text), "I") = True Then
                    txtItemName.ReadOnly = True
                Else
                    txtItemName.ReadOnly = False
                End If
            End If


            'If PubUserID = "G0416" Then 'If PubSuperUser = "S" Then	
            txtMaxQnty.Enabled = IIf(Val(txtMaxQnty.Text) <= 0, True, False)
            txtReQnty.Enabled = IIf(Val(txtReQnty.Text) <= 0, True, False)
            txtMinQnty.Enabled = IIf(Val(txtMinQnty.Text) <= 0, True, False)
            txtEcoQnty.Enabled = IIf(Val(txtEcoQnty.Text) <= 0, True, False)
            txtCatName.Enabled = True
            txtSubCatName.Enabled = True
            txtUOMFactor.Enabled = True
            txtPartNo.Enabled = True
            txtOldPartNo.Enabled = True
            cmdSearchScrap.Enabled = True
            CboItemType.Enabled = True
            CboItemClass.Enabled = True
            CboExciseFlag.Enabled = True
            cboItemClassification.Enabled = True

            txtScrapItemCode.Enabled = True

            txtProdType.Enabled = True

            FraWeld.Enabled = True
            FraSurface.Enabled = True
            FraPress.Enabled = True
            chkGrinding.Enabled = True
            chkSB.Enabled = True


            'Else
            '    txtMaxQnty.Enabled = False
            '    txtReQnty.Enabled = False
            '    txtMinQnty.Enabled = False
            '    txtEcoQnty.Enabled = False
            '    txtCatName.Enabled = False
            '    txtSubCatName.Enabled = False
            '    txtUOMFactor.Enabled = False
            '    txtPartNo.Enabled = IIf(Trim(txtPartNo.Text) = "", True, False)

            '    CboItemType.Enabled = False
            '    CboItemClass.Enabled = False
            '    CboExciseFlag.Enabled = False
            '    cboItemClassification.Enabled = False

            '    txtScrapItemCode.Enabled = IIf(Trim(txtScrapItemCode.Text) = "", True, False)
            '    cmdSearchScrap.Enabled = IIf(Trim(txtScrapItemCode.Text) = "", True, False)
            '    txtProdType.Enabled = IIf(Trim(txtProdType.Text) = "", True, False)

            '    FraWeld.Enabled = False
            '    FraSurface.Enabled = False
            '    FraPress.Enabled = False
            '    chkGrinding.Enabled = False
            '    chkSB.Enabled = False
            'End If

            Call ShowItemPara()
            'Field Disable...	
        End If

        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsItemMast, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume	
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
    End Sub


    Private Sub txtModel_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtModel.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtModel.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtModel.Text, "MODEL_DESC", "MODEL_CODE", "GEN_MODEL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value) = False Then
            ErrorMsg("Invalid MODEL .", , vbInformation)
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
    End Sub

    Private Sub txtPackingItemCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPackingItemCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtPackingItemCode.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable(txtPackingItemCode.Text, "ITEM_SHORT_DESC", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value) = False Then
            ErrorMsg("Invalid ITEM Name.", , vbInformation)
            Cancel = True
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

    Private Sub txtParentItemName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtParentItemName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtParentItemName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtParentItemName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtParentItemName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtParentItemName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtParentItemName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
    End Sub

    Private Sub txtParentItemName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtParentItemName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtParentItemName.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(Trim(txtParentItemName.Text), "ITEM_SHORT_DESC", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IS_CHILD='N' AND ITEM_STATUS='A'") = True Then
            txtParentItemName.Text = MasterNo
        Else
            Cancel = True
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
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

    Private Sub txtPartNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPartNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
    End Sub
    Private Sub txtOldPartNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOldPartNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOldPartNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOldPartNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtOldPartNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtOldPartNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtOldPartNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
    End Sub
    Private Sub txtProdType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProdType.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
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
    End Sub


    Private Sub txtProdType_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtProdType.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtProdType.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtProdType.Text, "PRODTYPE_DESC", "UOM", "INV_PRODUCTTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value) = False Then
            ErrorMsg("Invalid Product Type .", , vbInformation)
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
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
    End Sub

    Private Sub txtPurchaseUom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPurchaseUom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtPurchaseUom.Text) = "" Then lblPurUom.Text = "" : GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtPurchaseUom.Text, "GEN_CODE", "GEN_DESC", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='U'") = False Then
            ErrorMsg("Invalid Purchase UOM.", , vbInformation)
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
    End Sub

    Private Sub txtScrapItemCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtScrapItemCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtScrapItemCode.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable(txtScrapItemCode.Text, "ITEM_SHORT_DESC", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value) = False Then
            ErrorMsg("Invalid ITEM Name.", , vbInformation)
            Cancel = True
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

    Private Sub cboSurfaceTreatment_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboSurfaceTreatment.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSSWLength_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSSWLength.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtSSWLength_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSSWLength.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSurfaceAreaInner_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSurfaceAreaInner.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtSurfaceAreaInner_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSurfaceAreaInner.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
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
    Private Sub txtWtPerStrip_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWtPerStrip.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtWtPerStrip_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtWtPerStrip.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtTIGLen_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTIGLen.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtTIGLen_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTIGLen.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtBrazingLen_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBrazingLen.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtBrazingLen_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBrazingLen.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSeamLen_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSeamLen.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtSeamLen_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSeamLen.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSpotNos_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSpotNos.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtSpotNos_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSpotNos.KeyPress
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
    End Sub

    Private Sub txtSubCatName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSubCatName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim mCatCode As String = ""

        If Trim(txtSubCatName.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtCatName.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
            mCatCode = MasterNo
        End If

        If MainClass.ValidateWithMasterTable(txtSubCatName.Text, "SUBCATEGORY_DESC", "SUBCATEGORY_DESC", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCatCode & "' ") = False Then
            ErrorMsg("Invalid Sub Category Name.", , vbInformation)
            Cancel = True
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
    Private Sub txtWLengthCust_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWLengthCust.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtWLengthCust_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtWLengthCust.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSSWLengthCust_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSSWLengthCust.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtSSWLengthCust_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSSWLengthCust.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub cmdSearchCategory_Click(sender As Object, e As EventArgs) Handles cmdSearchCategory.Click
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'"

        If MainClass.SearchGridMaster(txtCatName.Text, "INV_GENERAL_MST", "GEN_DESC", "GEN_CODE",  , , SqlStr) = True Then
            txtCatName.Text = AcName
            txtCatName_Validating(txtCatName, New System.ComponentModel.CancelEventArgs(False))
            txtCatName.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, Err.Number, vbCritical)
    End Sub
    Private Sub cmdSearchSubCat_Click(sender As Object, e As EventArgs) Handles cmdSearchSubCat.Click
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mCatCode As String

        If Trim(txtCatName.Text) = "" Then
            MsgInformation("Please Select Category first.")
            txtCatName.Focus()
            Exit Sub
        End If
        If MainClass.ValidateWithMasterTable(txtCatName.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
            mCatCode = MasterNo
        Else
            MsgInformation("Item is Defined as Parent Code so Cann't be Deleted.")
            Exit Sub
        End If

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCatCode & "'"

        If MainClass.SearchGridMaster(txtSubCatName.Text, "INV_SUBCATEGORY_MST", "SUBCATEGORY_DESC", "SUBCATEGORY_CODE",  , , SqlStr) = True Then
            txtSubCatName.Text = AcName
            txtSubCatName_Validating(txtSubCatName, New System.ComponentModel.CancelEventArgs(False))
            txtSubCatName.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, Err.Number, vbCritical)
    End Sub
    Private Sub cmdSearchHSN_Click(sender As Object, e As EventArgs) Handles cmdSearchHSN.Click
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim pCategoryType As String = ""

        If txtCatName.Text = "" Then
            MsgInformation("Please Select The Category.")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(txtCatName.Text, "GEN_DESC", "PRD_TYPE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
            pCategoryType = MasterNo
        End If

        If pCategoryType = "S" Then
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='S'"
        Else
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='G'"
        End If

        If MainClass.SearchGridMaster(txtHSNCode.Text, "GEN_HSN_MST", "HSN_CODE", "HSN_DESC", , , SqlStr) = True Then
            txtHSNCode.Text = AcName
            txtHSNCode_Validating(txtHSNCode, New System.ComponentModel.CancelEventArgs(False))
        End If
        txtHSNCode.Focus()
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, Err.Number, vbCritical)
    End Sub
    Private Sub cmdSearchPackType_Click(sender As Object, e As EventArgs) Handles cmdSearchPackType.Click
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster(txtPackType.Text, "DSP_PACKINGTYPE_MST", "NAME", "", , , SqlStr) = True Then
            txtPackType.Text = AcName
            txtPackType_Validating(txtPackType, New System.ComponentModel.CancelEventArgs(False))
        End If
        txtPackType.Focus()
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, Err.Number, vbCritical)
    End Sub
    Private Sub cmdSearchPIC_Click(sender As Object, e As EventArgs) Handles cmdSearchPIC.Click
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster(txtPackingItemCode.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE",  , , SqlStr) = True Then
            txtPackingItemCode.Text = AcName
            txtPackingItemCode_Validating(txtPackingItemCode, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, Err.Number, vbCritical)
    End Sub
    Private Sub cmdSearchScrap_Click(sender As Object, e As EventArgs) Handles cmdSearchScrap.Click
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster(txtScrapItemCode.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr) = True Then
            txtScrapItemCode.Text = AcName
            txtScrapItemCode_Validating(txtScrapItemCode, New System.ComponentModel.CancelEventArgs(False))
        End If

        txtScrapItemCode.Focus()
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, Err.Number, vbCritical)
    End Sub
    '    Private Sub cmdsearch_Click(sender As Object, e As EventArgs)
    '        SearchAccounts()
    '    End Sub
    Private Sub SearchAccounts()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        ''MainClass.SearchMaster txtItemName, "INV_ITEM_MST", "NAME", SqlStr
        MainClass.SearchGridMaster("", "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", "CUSTOMER_PART_NO", , SqlStr)
        If AcName <> "" Then
            txtItemName.Text = AcName
            TxtItemName_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub txtItemName_DoubleClick(sender As Object, e As EventArgs) Handles txtItemName.DoubleClick
        SearchAccounts()
    End Sub

    Private Sub cmdSearchUom_Click(sender As Object, e As EventArgs) Handles cmdSearchUom.Click
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='U'"

        If MainClass.SearchGridMaster(txtItemUom.Text, "INV_GENERAL_MST", "GEN_CODE", "GEN_DESC", , , SqlStr) = True Then
            txtItemUom.Text = AcName
            txtItemUom_Validating(txtItemUom, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, Err.Number, vbCritical)
    End Sub

    Private Sub cmdSearchPurUom_Click(sender As Object, e As EventArgs) Handles cmdSearchPurUom.Click
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='U'"

        If MainClass.SearchGridMaster(txtPurchaseUom.Text, "INV_GENERAL_MST", "GEN_CODE", "GEN_DESC", , , SqlStr) = True Then
            txtPurchaseUom.Text = AcName
            txtPurchaseUom_Validating(txtPurchaseUom, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, Err.Number, vbCritical)
    End Sub

    Private Sub txtPurchaseUom_DoubleClick(sender As Object, e As EventArgs) Handles txtPurchaseUom.DoubleClick
        Call cmdSearchPurUom_Click(cmdSearchPurUom, New System.EventArgs())
    End Sub

    Private Sub txtItemUom_DoubleClick(sender As Object, e As EventArgs) Handles txtItemUom.DoubleClick
        Call cmdSearchUom_Click(cmdSearchUom, New System.EventArgs())
    End Sub

    Private Sub txtJWUOM_TextChanged(sender As Object, e As EventArgs) Handles txtJWUOM.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtJWUOM_DoubleClick(sender As Object, e As EventArgs) Handles txtJWUOM.DoubleClick
        Call cmdSearchUom_Click(cmdSearchJWUom, New System.EventArgs())
    End Sub

    Private Sub txtJWUOM_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtJWUOM.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtJWUOM.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtJWUOM_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtJWUOM.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
    End Sub
    Private Sub txtJWUOM_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtJWUOM.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If Trim(txtJWUOM.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtJWUOM.Text, "GEN_CODE", "GEN_DESC", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='U'") = False Then
            ErrorMsg("Invalid J/W UOM.", , vbInformation)
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub cmdSearchJWUom_Click(sender As Object, e As EventArgs) Handles cmdSearchJWUom.Click
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='U'"

        If MainClass.SearchGridMaster(txtJWUOM.Text, "INV_GENERAL_MST", "GEN_CODE", "GEN_DESC", , , SqlStr) = True Then
            txtJWUOM.Text = AcName
            txtJWUOM_Validating(txtJWUOM, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, Err.Number, vbCritical)
    End Sub

    Private Sub SprdMain_Change(sender As Object, e As _DSpreadEvents_ChangeEvent) Handles SprdMain.Change
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Public Function GenerateItemCode(ByRef pCompanyCode As Long, ByRef pCatCode As String, ByRef pSubCatCode As String) As String
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCatPreFix As String = "'"
        Dim mSubCatPreFix As String = ""
        Dim mSeqNo As Double
        Dim mItemPrefix As String = ""

        SqlStr = "SELECT CODE_PREFIX " & vbCrLf _
            & " FROM INV_GENERAL_MST " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " COMPANY_CODE=" & pCompanyCode & "" & vbCrLf _
            & " AND GEN_CODE ='" & pCatCode & "'" & vbCrLf _
            & " AND GEN_TYPE='C'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            mCatPreFix = Trim(IIf(IsDBNull(RsTemp.Fields("CODE_PREFIX").Value), "", RsTemp.Fields("CODE_PREFIX").Value))
        End If

        SqlStr = "SELECT SUBCODE_PREFIX " & vbCrLf _
            & " FROM INV_SUBCATEGORY_MST " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " COMPANY_CODE=" & pCompanyCode & "" & vbCrLf _
            & " AND CATEGORY_CODE ='" & pCatCode & "'" & vbCrLf _
            & " AND SUBCATEGORY_CODE ='" & pSubCatCode & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            mSubCatPreFix = Trim(IIf(IsDBNull(RsTemp.Fields("SUBCODE_PREFIX").Value), "", RsTemp.Fields("SUBCODE_PREFIX").Value))
        End If

        mItemPrefix = UCase(mCatPreFix & mSubCatPreFix)

        SqlStr = "SELECT MAX(SUBSTR(ITEM_CODE,LENGTH('" & mItemPrefix & "')+1,8-(LENGTH('" & mItemPrefix & "')))) AS MAXCODE " & vbCrLf _
            & " FROM INV_ITEM_MST " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " COMPANY_CODE=" & pCompanyCode & "" & vbCrLf _
            & " AND SUBSTR(ITEM_CODE,1,LENGTH('" & mItemPrefix & "'))='" & mItemPrefix & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        mSeqNo = 0
        If RsTemp.EOF = False Then
            mSeqNo = Val(IIf(IsDBNull(RsTemp.Fields("MAXCODE").Value), 0, RsTemp.Fields("MAXCODE").Value))
            mSeqNo = mSeqNo + 1
        End If

        Dim mZeroRepeatNo As Integer
        Dim pItemFormat As String

        mZeroRepeatNo = 8 - Len(mItemPrefix)
        pItemFormat = StrDup(mZeroRepeatNo, "0")
        GenerateItemCode = mItemPrefix & VB6.Format(mSeqNo, pItemFormat)

        'If Len(mItemPrefix) = 2 Then
        '    GenerateItemCode = mItemPrefix & VB6.Format(mSeqNo, "000000")
        'Else
        '    GenerateItemCode = mItemPrefix & VB6.Format(mSeqNo, "0000")
        'End If

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        GenerateItemCode = ""
    End Function
End Class
