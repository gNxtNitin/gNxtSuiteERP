Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmItemInvLevelUpdate
    Inherits System.Windows.Forms.Form
    'Dim PvtDBCn As ADODB.Connection				

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12

    Private Const ColItemCode As Short = 1
    Private Const ColItemName As Short = 2
    Private Const ColItemUOM As Short = 3
    Private Const ColMinLevel As Short = 4
    Private Const ColMaxLevel As Short = 5
    Private Const ColReOrderLevel As Short = 6
    Private Const ColInvDays As Short = 7
    Private Const ColUpdated As Short = 8

    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1

        With SprdMain
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)
            .MaxCols = ColUpdated

            .Row = 0
            SetColHeadings()
            .Row = Arow

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColItemCode, 9)
            .ColHidden = False

            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemName, 39)
            .ColHidden = False

            .Col = ColItemUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemUOM, 5)
            .ColHidden = False

            .Col = ColMinLevel
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColMinLevel, 10)

            .Col = ColMaxLevel
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColMaxLevel, 10)

            .Col = ColReOrderLevel
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColReOrderLevel, 10)

            .Col = ColInvDays
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColInvDays, 10)

            .Col = ColUpdated
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColUpdated, 40)
            .ColHidden = True

            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemCode, ColItemName)
        End With
        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        '    Resume				
    End Sub
    Private Sub SetColHeadings()
        With SprdMain
            .Row = 0
            .Col = ColItemCode
            .Text = "Item Code"

            .Col = ColItemName
            .Text = "Item Name"

            .Col = ColItemUOM
            .Text = "Item UOM"

            .Col = ColMinLevel
            .Text = "Minimum Qty"

            .Col = ColMaxLevel
            .Text = "Maximum Qty"

            .Col = ColReOrderLevel
            .Text = "Re-Order Qty"

            .Col = ColInvDays
            .Text = "Inventory Days"

            .set_RowHeight(0, 20)
        End With
    End Sub

    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        txtItemName.Enabled = IIf(chkAll.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        cmdSearch.Enabled = IIf(chkAll.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
    End Sub

    Private Sub chkAllCategory_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllCategory.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        If chkAllCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtCategory.Enabled = False
            cmdsearchCategory.Enabled = False
        Else
            txtCategory.Enabled = True
            cmdsearchCategory.Enabled = True
        End If
    End Sub

    Private Sub chkAllSubCat_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllSubCat.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
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

        If Trim(txtCategory.Text) = "" Then
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

    Private Sub cmdSubCatsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSubCatsearch.Click
        SearchSubCategory()
    End Sub

    Private Sub txtCategory_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCategory.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
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

        If MainClass.ValidateWithMasterTable(txtCategory.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , SqlStr) = False Then
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

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
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

        If Trim(txtCategory.Text) = "" Then
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
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Call SaleReport("V")				
        ReportForShow(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Call SaleReport("V")				
        ReportForShow(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        On Error GoTo AddErr

        Clear1()
        If chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtSubCategory.Text) = "" Then
                MsgInformation("Please Select Sub Category")
                If txtSubCategory.Enabled = True Then txtSubCategory.Focus()
                Exit Sub
            End If
        End If
        Show1()
        Call FormatSprdMain(-1)
        Exit Sub
AddErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If PubInvLevelUser = "N" Then
            MsgInformation("You have no rights to Update Inventory Level. Please contact to Administrator.")
            CmdSave.Enabled = False
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        If Update1() = False Then GoTo ErrPart
        CmdSave.Enabled = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        ErrorMsg(CStr(Err.Number), Err.Description, MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click

        If MainClass.SearchGridMaster(txtItemName.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", "CUSTOMER_PART_NO", , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtItemName.Text = AcName
            TxtItemName_Validating(txtItemName, New System.ComponentModel.CancelEventArgs(False))
        End If

        If SprdMain.Enabled = True Then SprdMain.Focus()
    End Sub

    Private Sub frmItemInvLevelUpdate_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1

        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmItemInvLevelUpdate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmItemInvLevelUpdate_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)

        'Set PvtDBCn = New ADODB.Connection				
        'PvtDBCn.Open StrConn				

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)

        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        txtItemName.Enabled = IIf(chkAll.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        cmdSearch.Enabled = IIf(chkAll.CheckState = System.Windows.Forms.CheckState.Checked, False, True)

        chkAllCategory.CheckState = System.Windows.Forms.CheckState.Checked
        txtCategory.Enabled = False
        cmdsearchCategory.Enabled = False

        chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Checked
        txtSubCategory.Enabled = False
        cmdSubCatsearch.Enabled = False

        MainClass.SetControlsColor(Me)

        ADDMode = False
        MODIFYMode = False

        '    OptProdCustWise(0).Value = True				
        '   OptProdCustWise_Click (0)				
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Clear1()

        On Error GoTo ClearErr
        CmdSave.Enabled = True
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)
        Exit Sub
ClearErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim SqlStr As String
        Dim mCATEGORY_CODE As String
        Dim mSubCatCode As String

        SqlStr = "SELECT ITEM_CODE,ITEM_SHORT_DESC, ISSUE_UOM," & vbCrLf & " MINIMUM_QTY,MAXIMUM_QTY,REORDER_QTY,ECONOMIC_QTY,''" & vbCrLf & " FROM INV_ITEM_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtItemName.Text) <> "" Then
                SqlStr = SqlStr & vbCrLf & "AND ITEM_SHORT_DESC='" & MainClass.AllowSingleQuote(txtItemName.Text) & "'"
            End If
        End If

        If chkAllCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtCategory.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                mCATEGORY_CODE = MasterNo
                SqlStr = SqlStr & vbCrLf & " AND CATEGORY_CODE='" & MainClass.AllowSingleQuote(mCATEGORY_CODE) & "'"
            End If
        End If

        If chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtSubCategory.Text, "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCATEGORY_CODE & "'") = True Then
                mSubCatCode = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND SUBCATEGORY_CODE='" & MainClass.AllowSingleQuote(mSubCatCode) & "'"
            End If
        End If

        If optShow(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & "AND (MINIMUM_QTY + MINIMUM_QTY) = 0"
        ElseIf optShow(2).Checked = True Then
            SqlStr = SqlStr & vbCrLf & "AND (MINIMUM_QTY + MINIMUM_QTY)<>0"
        End If

        'If Trim(txtTariffHeading.Text) <> "" Then
        '    SqlStr = SqlStr & vbCrLf & "AND TARIFF_CODE='" & MainClass.AllowSingleQuote(txtTariffHeading) & "'"
        'End If

        If optOrderBy(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & "ORDER BY ITEM_SHORT_DESC"
        Else
            SqlStr = SqlStr & vbCrLf & "ORDER BY ITEM_CODE"
        End If
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
        '    Resume				
    End Sub
    Private Function Update1() As Boolean

        On Error GoTo UpdateErr
        Dim SqlStr As String
        Dim I As Integer
        Dim mItemCode As String
        Dim mMinimumQty As Double
        Dim mMaximumQty As Double
        Dim mInvDays As Double
        Dim mReOrder As Double

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColUpdated
                If .Text <> "Y" Then GoTo LoopNext

                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColMinLevel
                mMinimumQty = Val(.Text)

                .Col = ColMaxLevel
                mMaximumQty = Val(.Text)

                .Col = ColReOrderLevel
                mReOrder = Val(.Text)

                .Col = ColInvDays
                mInvDays = Val(.Text)

                SqlStr = " UPDATE INV_ITEM_MST " & vbCrLf _
                    & " SET MINIMUM_QTY=" & mMinimumQty & ", REORDER_QTY=" & mReOrder & "," & vbCrLf _
                    & " MAXIMUM_QTY=" & mMaximumQty & ",ECONOMIC_QTY=" & mInvDays & "," & vbCrLf _
                    & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                    & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                    & " WHERE Company_Code= " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND ITEM_CODE= '" & mItemCode & "' "


                PubDBCn.Execute(SqlStr)
LoopNext:
            Next
        End With
        Update1 = True
        PubDBCn.CommitTrans()
        Exit Function
UpdateErr:
        Update1 = False
        PubDBCn.RollbackTrans()
        If Err.Description <> "" Then
            MsgBox(Err.Description)
        End If
        ''Resume				
    End Function
    Private Sub frmItemInvLevelUpdate_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        'PvtDBCn.Close				
        'Set PvtDBCn = Nothing				
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        CmdSave.Enabled = True
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        SprdMain.Col = ColUpdated
        SprdMain.Row = SprdMain.ActiveRow
        SprdMain.Text = "Y"
    End Sub

    Private Sub txtItemName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtItemName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemName.DoubleClick
        Call cmdsearch_Click(cmdSearch, New System.EventArgs())
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdsearch_Click(cmdSearch, New System.EventArgs())
    End Sub

    Private Sub TxtItemName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim RsItemName As ADODB.Recordset
        Dim SqlStr As String

        If Trim(txtItemName.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable(txtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Item Name Does Not Exist In Master")
            Cancel = True
            Exit Sub
        End If

        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub ReportForShow(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mRPTName As String

        PubDBCn.Errors.Clear()


        'If TxtName.Text = "" Then Exit Sub				

        SqlStr = "DELETE FROM Temp_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = ""

        Call InsertPrintDummy()


        '''''Select Record for print...				

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        mTitle = "Item Min/Max Level List"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mTitle = mTitle & IIf(Trim(txtItemName.Text) = "", "", " - " & txtItemName.Text)
        End If
        mSubTitle = IIf(chkAllCategory.CheckState = System.Windows.Forms.CheckState.Unchecked, txtCategory.Text, "(Category : All)")

        mRPTName = "Item_MinMax.Rpt"

        Call ShowReport(SqlStr, mRPTName, Mode, mTitle, mSubTitle)

        Exit Sub
ERR1:
        If Err.Number = 32755 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        Else
            MsgInformation(Err.Description)
        End If
        ''Resume				
    End Sub
    Private Sub InsertPrintDummy()


        On Error GoTo ERR1
        Dim mItemCode As String
        Dim mItemName As String
        Dim mItemUOM As String

        Dim mMinLevel As String
        Dim mMaxLevel As String
        Dim mReOrder As String

        Dim SqlStr As String
        Dim cntRow As Integer
        Dim mInvDays As String

        PubDBCn.Errors.Clear()

        PubDBCn.BeginTrans()

        SqlStr = ""
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(Trim(.Text))

                .Col = ColItemName
                mItemName = MainClass.AllowSingleQuote(Trim(.Text))

                .Col = ColItemUOM
                mItemUOM = MainClass.AllowSingleQuote(Trim(.Text))

                .Col = ColMinLevel
                mMinLevel = Trim(.Text)

                .Col = ColMaxLevel
                mMaxLevel = MainClass.AllowSingleQuote(Trim(.Text))

                .Col = ColInvDays
                mInvDays = MainClass.AllowSingleQuote(Trim(.Text))

                .Col = ColReOrderLevel
                mReOrder = MainClass.AllowSingleQuote(Trim(.Text))

                SqlStr = "Insert into Temp_PrintDummyData (UserID,SubRow," & vbCrLf & " Field1,Field2,Field3,Field4,Field5,Field6,Field7 " & vbCrLf & " ) Values (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " " & cntRow & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(mItemCode) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mItemName) & "', '" & MainClass.AllowSingleQuote(mItemUOM) & "', " & vbCrLf & " '" & mMinLevel & "', '" & mMaxLevel & "', '" & mInvDays & "','" & mReOrder & "') "

                PubDBCn.Execute(SqlStr)
            Next

        End With
        PubDBCn.CommitTrans()
        Exit Sub
ERR1:
        PubDBCn.RollbackTrans()
        MsgInformation(Err.Description)
    End Sub
    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String

        mSqlStr = mSqlStr & "SELECT * " & " FROM Temp_PrintDummyData PRINTDUMMYDATA " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY SUBROW"

        FetchRecordForReport = mSqlStr

    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub

    Private Sub cmdsearchCategory_Click(sender As Object, e As EventArgs) Handles cmdsearchCategory.Click
        SearchCategory()
    End Sub
End Class
