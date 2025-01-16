Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmSBReworkApp
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20

    Private Const ColLocked As Short = 1
    Private Const ColRefNo As Short = 2
    Private Const ColRefDate As Short = 3
    Private Const colDeptCodeFrom As Short = 4
    Private Const colDeptCodeTo As Short = 5
    Private Const ColItemCode As Short = 6
    Private Const ColItemDesc As Short = 7
    Private Const ColUnit As Short = 8
    Private Const ColQty As Short = 9

    Private Const ColMKEY As Short = 10


    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Dim mShow As Boolean
    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            TxtItemName.Enabled = False
            cmdsearch.Enabled = False
        Else
            TxtItemName.Enabled = True
            cmdsearch.Enabled = True
        End If
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchItem()
    End Sub
    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        If FieldsVerification() = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        FormatSprdMain(-1)
        If Show1() = False Then GoTo ErrPart
        mShow = True

        ''MainClass.SetFocusToCell SprdMain, mActiveRow, 4
        FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmSBReworkApp_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Me.Text = "Pending Indent for Approval"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormatSprdMain(-1)
        FormActive = True
        mShow = False
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmSBReworkApp_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355
        Call SetMainFormCordinate(Me)
        Me.Top = 0
        Me.Left = 0
        ''Me.Height = VB6.TwipsToPixelsY(7245)
        ''Me.Width = VB6.TwipsToPixelsX(11355)


        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtItemName.Enabled = False
        cmdsearch.Enabled = False

        txtDeptName.Enabled = False
        cmdsearchDept.Enabled = False

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmSBReworkApp_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame4.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11379.7, 749)
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmSBReworkApp_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub

    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick

        'Dim SqlStr As String = ""
        Dim xRefNo As Double
        'Dim xQCStatus As String

        SprdMain.Row = SprdMain.ActiveRow

        SprdMain.Col = ColRefNo
        xRefNo = Val(SprdMain.Text)

        If Val(xRefNo) <= 0 Then Exit Sub

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, UCase(lblForm.Text), PubDBCn)
        If InStr(1, XRIGHT, "M", CompareMethod.Text) = 0 Then
            Exit Sub
        End If

        If UCase(lblForm.Text) = "MNUSBREWORKRECD" Then
            frmSBRework.MdiParent = Me.MdiParent

            frmSBRework.Show()
            frmSBRework.lblBookType.Text = lblBookType.Text

            frmSBRework.frmSBRework_Activated(Nothing, New System.EventArgs())

            frmSBRework.txtSlipNo.Text = xRefNo
            frmSBRework.txtSlipNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
        ElseIf UCase(lblForm.Text) = "MNURWKPRODRECD" Then
            FrmReworkProdDeptWise.MdiParent = Me.MdiParent

            FrmReworkProdDeptWise.Show()
            FrmReworkProdDeptWise.lblBookType.Text = lblBookType.Text
            FrmReworkProdDeptWise.lblApproval.Text = lblApproval.Text
            FrmReworkProdDeptWise.lblShow.Text = lblShow.Text

            FrmReworkProdDeptWise.FrmReworkProdDeptWise_Activated(Nothing, New System.EventArgs())

            FrmReworkProdDeptWise.txtPMemoNo.Text = xRefNo
            FrmReworkProdDeptWise.txtPMemoNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
        ElseIf UCase(lblForm.Text) = "MNUREWORKSCRAPNOTEAPP" Then
            FrmReworkBreakup.MdiParent = Me.MdiParent

            FrmReworkBreakup.Show()
            FrmReworkBreakup.lblBookType.Text = lblBookType.Text
            FrmReworkBreakup.lblApproval.Text = lblApproval.Text

            FrmReworkBreakup.FrmReworkBreakup_Activated(Nothing, New System.EventArgs())

            FrmReworkBreakup.txtPMemoNo.Text = xRefNo
            FrmReworkBreakup.txtPMemoNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
        ElseIf UCase(lblForm.Text) = "MNUCRREWORKNOTEAPP" Then
            FrmCRRework.MdiParent = Me.MdiParent

            FrmCRRework.Show()
            FrmCRRework.lblBookType.Text = lblBookType.Text
            FrmCRRework.lblApproval.Text = lblApproval.Text
            FrmCRRework.lblShow.Text = lblShow.Text

            FrmCRRework.FrmCRRework_Activated(Nothing, New System.EventArgs())

            FrmCRRework.txtPMemoNo.Text = xRefNo
            FrmCRRework.txtPMemoNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
        ElseIf UCase(lblForm.Text) = "MNUCRSCRAPNOTEAPP" Then
            FrmCRBreakup.MdiParent = Me.MdiParent

            FrmCRBreakup.Show()
            FrmCRBreakup.lblBookType.Text = lblBookType.Text
            FrmCRBreakup.lblApproval.Text = lblApproval.Text

            FrmCRBreakup.FrmCRBreakup_Activated(Nothing, New System.EventArgs())

            FrmCRBreakup.txtPMemoNo.Text = xRefNo
            FrmCRBreakup.txtPMemoNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
        ElseIf UCase(lblForm.Text) = "MNUMATRECVNOTE" Then
            FrmProdIssuRecvNote.MdiParent = Me.MdiParent

            FrmProdIssuRecvNote.Show()
            FrmProdIssuRecvNote.lblBookType.Text = lblBookType.Text

            FrmProdIssuRecvNote.FrmProdIssuRecvNote_Activated(Nothing, New System.EventArgs())

            FrmProdIssuRecvNote.txtIssueNo.Text = xRefNo
            FrmProdIssuRecvNote.txtIssueNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
        End If


    End Sub
    Private Sub txtItemName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtItemName.DoubleClick
        SearchItem()
    End Sub
    Private Sub SearchItem()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.SearchGridMaster(TxtItemName.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr)
        If AcName <> "" Then
            TxtItemName.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub txtItemName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtItemName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtItemName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtItemName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtItemName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchItem()
    End Sub
    Private Sub TxtItemName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtItemName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""


        If TxtItemName.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable((TxtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            TxtItemName.Text = UCase(Trim(TxtItemName.Text))
        Else
            MsgInformation("No Such Item in Item Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)
        With SprdMain
            .MaxCols = ColMKEY
            .set_RowHeight(0, RowHeight * 1.2)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColLocked
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColLocked, 15)
            .ColHidden = True

            .Col = ColRefNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColRefNo, 9)

            .Col = ColRefDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColRefDate, 9)

            .Col = colDeptCodeFrom
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(colDeptCodeFrom, 10)

            .Col = colDeptCodeTo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(colDeptCodeTo, 10)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemCode, 8)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemDesc, 30)

            .Col = ColUnit
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColUnit, 4)

            .Col = ColQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColQty, 9)


            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMKEY, 8)
            .ColHidden = True


            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, ColMKEY)

            '        SprdMain.OperationMode = OperationModeSingle
            '        SprdMain.DAutoCellTypes = True
            '        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            '        SprdMain.GridColor = &HC00000
        End With
    End Sub
    Private Function Show1() As Boolean

        On Error GoTo LedgError
        Dim SqlStr As String = ""


        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If UCase(lblForm.Text) = "MNUSBREWORKRECD" Then  ''done
            SqlStr = MakeSQLSB()
        ElseIf UCase(lblForm.Text) = "MNURWKPRODRECD" Then  'done
            SqlStr = MakeSQLReworkProd()
        ElseIf UCase(lblForm.Text) = "MNUREWORKSCRAPNOTEAPP" Then  'done
            SqlStr = MakeSQLReworkScrapApp()
        ElseIf UCase(lblForm.Text) = "MNUCRREWORKNOTEAPP" Then
            SqlStr = MakeSQLReworkProd()    '' MakeSQLReworkCRApp()
        ElseIf UCase(lblForm.Text) = "MNUCRSCRAPNOTEAPP" Then
            SqlStr = MakeSQLScrapApp()
        ElseIf UCase(lblForm.Text) = "MNUMATRECVNOTE" Then
            SqlStr = MakeSQLMaterialRecdNote()
        End If



        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        '********************************
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function MakeSQLMaterialRecdNote() As String

        On Error GoTo ERR1
        Dim mItemCode As String
        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim SqlStr As String

        SqlStr = ""

        'SqlStr = SqlStr & vbCrLf & " ORDER BY REF_DATE,AUTO_KEY_REF"

        SqlStr = " SELECT ''," & vbCrLf _
            & " IH.AUTO_KEY_ISSREC," & vbCrLf _
            & " TO_CHAR(IH.ISSREC_DATE,'DD/MM/YYYY'),DEPT.DEPT_DESC,DEPT1.DEPT_DESC," & vbCrLf _
            & " ID.ITEM_CODE, INVMST.ITEM_SHORT_DESC," & vbCrLf _
            & " INVMST.ISSUE_UOM, TO_CHAR(ID.ISSUE_QTY), " & vbCrLf _
            & " IH.AUTO_KEY_ISSREC "


        ''FROM CLAUSE...
        SqlStr = SqlStr & vbCrLf & " FROM PRD_ISSREC_HDR IH, PRD_ISSREC_DET ID, INV_ITEM_MST INVMST, PAY_DEPT_MST DEPT, PAY_DEPT_MST DEPT1"

        ''WHERE CLAUSE...
        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf _
            & " SUBSTR(IH.AUTO_KEY_ISSREC,LENGTH(IH.AUTO_KEY_ISSREC)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND IH.AUTO_KEY_ISSREC=ID.AUTO_KEY_ISSREC" & vbCrLf _
            & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND ID.ITEM_CODE=INVMST.ITEM_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=DEPT.COMPANY_CODE" & vbCrLf _
            & " AND IH.FROM_DEPT=DEPT.DEPT_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=DEPT1.COMPANY_CODE" & vbCrLf _
            & " AND IH.TO_DEPT=DEPT1.DEPT_CODE "

        SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE='" & RsCompany.Fields("COMPANY_CODE").Value & "'"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND INVMST.ITEM_SHORT_DESC='" & MainClass.AllowSingleQuote(TxtItemName.Text) & "'"
        End If

        If chkAllDept.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND DEPT.DEPT_DESC='" & MainClass.AllowSingleQuote(txtDeptName.Text) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & "AND IH.RECV_STATUS='N'"

        ''ORDER CLAUSE...
        SqlStr = SqlStr & vbCrLf & "ORDER BY IH.AUTO_KEY_ISSREC, IH.ISSREC_DATE"

        MakeSQLMaterialRecdNote = SqlStr

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function MakeSQLScrapApp() As String

        On Error GoTo ERR1
        Dim mItemCode As String
        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim SqlStr As String


        SqlStr = ""

        SqlStr = " SELECT ''," & vbCrLf _
            & " IH.AUTO_KEY_REF," & vbCrLf _
            & " TO_CHAR(IH.REF_DATE,'DD/MM/YYYY'),DEPT.DEPT_DESC, ''," & vbCrLf _
            & " IH.PRODUCT_CODE, INVMST.ITEM_SHORT_DESC," & vbCrLf _
            & " INVMST.ISSUE_UOM, TO_CHAR(IH.PROD_QTY), " & vbCrLf _
            & " IH.AUTO_KEY_REF "

        ''FROM CLAUSE...
        SqlStr = SqlStr & vbCrLf & " FROM PRD_FGBREAKUP_HDR IH, INV_ITEM_MST INVMST, PAY_DEPT_MST DEPT"

        ''WHERE CLAUSE...
        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf _
            & " SUBSTR(IH.AUTO_KEY_REF,LENGTH(IH.AUTO_KEY_REF)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.PRODUCT_CODE=INVMST.ITEM_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=DEPT.COMPANY_CODE" & vbCrLf _
            & " AND IH.DEPT_CODE=DEPT.DEPT_CODE "

        SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE='" & RsCompany.Fields("COMPANY_CODE").Value & "'"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND INVMST.ITEM_SHORT_DESC='" & MainClass.AllowSingleQuote(TxtItemName.Text) & "'"
        End If

        If chkAllDept.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND DEPT.DEPT_DESC='" & MainClass.AllowSingleQuote(txtDeptName.Text) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & "AND IH.APPROVED='N'"

        ''ORDER CLAUSE...
        SqlStr = SqlStr & vbCrLf & "ORDER BY IH.AUTO_KEY_REF, IH.REF_DATE"

        MakeSQLScrapApp = SqlStr

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    '    Private Function MakeSQLReworkCRApp() As String

    '        On Error GoTo ERR1
    '        Dim mItemCode As String
    '        Dim mCompanyName As String
    '        Dim mCompanyCode As String
    '        Dim SqlStr As String

    '        SqlStr = ""
    '        'SqlStr = " SELECT  AUTO_KEY_REF MEMO_NO, TO_CHAR(REF_DATE,'DD/MM/YYYY') MEMO_DATE, " & vbCrLf _
    '        '    & " DEPT_CODE FROM_DEPT,SEND_DEPT_CODE,SHIFT_CODE,REWORK_QTY,RECD_QTY,DECODE(REWORK_QTY-RECD_QTY,0,'Complete','Pending') AS STATUS, DECODE(PROD_TYPE,'R','Rework','Customer Rej') AS Prod_Type,REMARKS " & vbCrLf _
    '        '    & " FROM PRD_REWORK_HDR  " & vbCrLf _
    '        '    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & vbCrLf _
    '        '    & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
    '        '    & " AND BOOKTYPE='" & vb.Left(lblBookType.Caption, 1) & "' "


    '        SqlStr = " SELECT ''," & vbCrLf _
    '            & " IH.AUTO_KEY_REF," & vbCrLf _
    '            & " TO_CHAR(IH.REF_DATE,'DD/MM/YYYY'),DEPT.DEPT_DESC,DEPT1.DEPT_DESC," & vbCrLf _
    '            & " ID.PRODUCT_CODE, INVMST.ITEM_SHORT_DESC," & vbCrLf _
    '            & " INVMST.ISSUE_UOM, TO_CHAR(ID.SB_QTY), " & vbCrLf _
    '            & " IH.AUTO_KEY_SBRWK "


    '        ''FROM CLAUSE...
    '        SqlStr = SqlStr & vbCrLf & " FROM PRD_REWORK_HDR IH, PRD_SENDBACKFORRWK_DET ID, INV_ITEM_MST INVMST, PAY_DEPT_MST DEPT, PAY_DEPT_MST DEPT1"

    '        ''WHERE CLAUSE...
    '        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf _
    '            & " SUBSTR(IH.AUTO_KEY_REF,LENGTH(IH.AUTO_KEY_REF)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
    '            & " AND IH.AUTO_KEY_REF=ID.AUTO_KEY_SBRWK" & vbCrLf _
    '            & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
    '            & " AND ID.PRODUCT_CODE=INVMST.ITEM_CODE " & vbCrLf _
    '            & " AND IH.COMPANY_CODE=DEPT.COMPANY_CODE" & vbCrLf _
    '            & " AND IH.FROM_DEPT=DEPT.DEPT_CODE " & vbCrLf _
    '            & " AND IH.COMPANY_CODE=DEPT1.COMPANY_CODE" & vbCrLf _
    '            & " AND IH.TO_DEPT=DEPT1.DEPT_CODE "

    '        SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE='" & RsCompany.Fields("COMPANY_CODE").Value & "'"

    '        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
    '            SqlStr = SqlStr & vbCrLf & "AND INVMST.ITEM_SHORT_DESC='" & MainClass.AllowSingleQuote(TxtItemName.Text) & "'"
    '        End If

    '        If chkAllDept.CheckState = System.Windows.Forms.CheckState.Unchecked Then
    '            SqlStr = SqlStr & vbCrLf & "AND DEPT.DEPT_DESC='" & MainClass.AllowSingleQuote(txtDeptName.Text) & "'"
    '        End If

    '        SqlStr = SqlStr & vbCrLf & "AND IH.STATUS='P'"

    '        ''ORDER CLAUSE...
    '        SqlStr = SqlStr & vbCrLf & "ORDER BY IH.AUTO_KEY_SBRWK, IH.SB_DATE"

    '        MakeSQLReworkCRApp = SqlStr

    '        Exit Function
    'ERR1:
    '        MsgInformation(Err.Description)
    '    End Function
    Private Function MakeSQLReworkScrapApp() As String

        On Error GoTo ERR1
        Dim mItemCode As String
        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim SqlStr As String

        SqlStr = ""

        SqlStr = " SELECT ''," & vbCrLf _
            & " IH.AUTO_KEY_REF," & vbCrLf _
            & " TO_CHAR(IH.REF_DATE,'DD/MM/YYYY'),DEPT.DEPT_DESC, ''," & vbCrLf _
            & " IH.PRODUCT_CODE, INVMST.ITEM_SHORT_DESC," & vbCrLf _
            & " INVMST.ISSUE_UOM, TO_CHAR(IH.PROD_QTY), " & vbCrLf _
            & " IH.AUTO_KEY_REF "

        ''FROM CLAUSE...
        SqlStr = SqlStr & vbCrLf & " FROM PRD_WRBREAKUP_HDR IH, INV_ITEM_MST INVMST, PAY_DEPT_MST DEPT"

        ''WHERE CLAUSE...
        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf _
            & " SUBSTR(IH.AUTO_KEY_REF,LENGTH(IH.AUTO_KEY_REF)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.PRODUCT_CODE=INVMST.ITEM_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=DEPT.COMPANY_CODE" & vbCrLf _
            & " AND IH.DEPT_CODE=DEPT.DEPT_CODE "

        SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE='" & RsCompany.Fields("COMPANY_CODE").Value & "'"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND INVMST.ITEM_SHORT_DESC='" & MainClass.AllowSingleQuote(TxtItemName.Text) & "'"
        End If

        If chkAllDept.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND DEPT.DEPT_DESC='" & MainClass.AllowSingleQuote(txtDeptName.Text) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & "AND IH.APPROVED='N'"

        ''ORDER CLAUSE...
        SqlStr = SqlStr & vbCrLf & "ORDER BY IH.AUTO_KEY_REF, IH.REF_DATE"

        MakeSQLReworkScrapApp = SqlStr

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function MakeSQLReworkProd() As String

        On Error GoTo ERR1
        Dim mItemCode As String
        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim SqlStr As String

        SqlStr = ""


        SqlStr = " SELECT ''," & vbCrLf _
            & " IH.AUTO_KEY_REF," & vbCrLf _
            & " TO_CHAR(IH.REF_DATE,'DD/MM/YYYY'),DEPT.DEPT_DESC,DEPT1.DEPT_DESC," & vbCrLf _
            & " IH.PRODUCT_CODE, INVMST.ITEM_SHORT_DESC," & vbCrLf _
            & " INVMST.ISSUE_UOM, TO_CHAR(IH.REWORK_QTY), " & vbCrLf _
            & " IH.AUTO_KEY_REF "


        ''FROM CLAUSE...
        SqlStr = SqlStr & vbCrLf & " FROM PRD_REWORK_HDR IH,  INV_ITEM_MST INVMST, PAY_DEPT_MST DEPT, PAY_DEPT_MST DEPT1"

        ''WHERE CLAUSE...
        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf _
            & " SUBSTR(IH.AUTO_KEY_REF,LENGTH(IH.AUTO_KEY_REF)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.PRODUCT_CODE=INVMST.ITEM_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=DEPT.COMPANY_CODE" & vbCrLf _
            & " AND IH.DEPT_CODE=DEPT.DEPT_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=DEPT1.COMPANY_CODE" & vbCrLf _
            & " AND IH.SEND_DEPT_CODE=DEPT1.DEPT_CODE "

        SqlStr = SqlStr & vbCrLf & " AND IH.BOOKTYPE='" & Mid(lblBookType.Text, 1, 1) & "'"

        SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE='" & RsCompany.Fields("COMPANY_CODE").Value & "'"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND INVMST.ITEM_SHORT_DESC='" & MainClass.AllowSingleQuote(TxtItemName.Text) & "'"
        End If

        If chkAllDept.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND DEPT.DEPT_DESC='" & MainClass.AllowSingleQuote(txtDeptName.Text) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & "AND IH.APPROVED='N'"

        ''ORDER CLAUSE...
        SqlStr = SqlStr & vbCrLf & "ORDER BY IH.AUTO_KEY_REF, IH.REF_DATE"

        MakeSQLReworkProd = SqlStr

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function MakeSQLSB() As String

        On Error GoTo ERR1
        Dim mItemCode As String
        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim SqlStr As String

        SqlStr = " SELECT ''," & vbCrLf _
            & " IH.AUTO_KEY_SBRWK," & vbCrLf _
            & " TO_CHAR(IH.SB_DATE,'DD/MM/YYYY'),DEPT.DEPT_DESC,DEPT1.DEPT_DESC," & vbCrLf _
            & " ID.PRODUCT_CODE, INVMST.ITEM_SHORT_DESC," & vbCrLf _
            & " INVMST.ISSUE_UOM, TO_CHAR(ID.SB_QTY), " & vbCrLf _
            & " IH.AUTO_KEY_SBRWK "


        ''FROM CLAUSE...
        SqlStr = SqlStr & vbCrLf & " FROM PRD_SENDBACKFORRWK_HDR IH, PRD_SENDBACKFORRWK_DET ID, INV_ITEM_MST INVMST, PAY_DEPT_MST DEPT, PAY_DEPT_MST DEPT1"

        ''WHERE CLAUSE...
        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf _
            & " SUBSTR(IH.AUTO_KEY_SBRWK,LENGTH(IH.AUTO_KEY_SBRWK)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND IH.AUTO_KEY_SBRWK=ID.AUTO_KEY_SBRWK" & vbCrLf _
            & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND ID.PRODUCT_CODE=INVMST.ITEM_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=DEPT.COMPANY_CODE" & vbCrLf _
            & " AND IH.FROM_DEPT=DEPT.DEPT_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=DEPT1.COMPANY_CODE" & vbCrLf _
            & " AND IH.TO_DEPT=DEPT1.DEPT_CODE "

        SqlStr = SqlStr & vbCrLf & " AND IH.COMPANY_CODE='" & RsCompany.Fields("COMPANY_CODE").Value & "'"

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND INVMST.ITEM_SHORT_DESC='" & MainClass.AllowSingleQuote(TxtItemName.Text) & "'"
        End If

        If chkAllDept.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & "AND DEPT.DEPT_DESC='" & MainClass.AllowSingleQuote(txtDeptName.Text) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & "AND IH.STATUS='P'"

        ''ORDER CLAUSE...
        SqlStr = SqlStr & vbCrLf & "ORDER BY IH.AUTO_KEY_SBRWK, IH.SB_DATE"

        MakeSQLSB = SqlStr

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        'If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        'If FYChk(CStr(CDate(txtDateTo.Text))) = False Then txtDateTo.Focus()
        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(TxtItemName.Text) = "" Then
                MsgInformation("Invaild Item Name")
                TxtItemName.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((TxtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invaild Item Name")
                TxtItemName.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If

        If chkAllDept.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtDeptName.Text) = "" Then
                MsgInformation("Invaild Deptartment Name")
                txtDeptName.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((txtDeptName.Text), "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invaild Deptartment Name")
                txtDeptName.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Private Sub chkAllDept_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllDept.CheckStateChanged
        If chkAllDept.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtDeptName.Enabled = False
            cmdsearchDept.Enabled = False
        Else
            txtDeptName.Enabled = True
            cmdsearchDept.Enabled = True
        End If
    End Sub

    Private Sub txtDeptName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDeptName.DoubleClick
        SearchDept()
    End Sub
    Private Sub SearchDept()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.SearchGridMaster(txtDeptName.Text, "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , SqlStr)
        If AcName <> "" Then
            txtDeptName.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub txtDeptName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDeptName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDeptName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtDeptName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDeptName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchDept()
    End Sub
    Private Sub txtDeptName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDeptName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""


        If txtDeptName.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable((txtDeptName.Text), "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtDeptName.Text = UCase(Trim(txtDeptName.Text))
        Else
            MsgInformation("No Such Item in Depatrtment Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
