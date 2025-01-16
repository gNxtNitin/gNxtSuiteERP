Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmProdSeqMst
    Inherits System.Windows.Forms.Form
    Dim RsProdSeqMain As ADODB.Recordset
    Dim RsProdSeqDetail As ADODB.Recordset
    Private PvtDBCn As ADODB.Connection

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

    Dim IsShowing As Boolean
    Private Const ConRowHeight As Short = 14

    Private Const ColDept As Short = 1
    Private Const ColDeptDesc As Short = 2
    Private Const ColOPRN As Short = 3
    Private Const ColMinQty As Short = 4
    Private Const ColMaxQty As Short = 5
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            '        txtProductCode.Enabled = False	
            '        cmdSearchPCode.Enabled = False	
            SprdMain.Enabled = True
        Else
            ADDMode = False
            MODIFYMode = False
            If RsProdSeqMain.EOF = False Then RsProdSeqMain.MoveFirst()
            Show1()
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
        'Resume	
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        On Error Resume Next
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click

        On Error GoTo DelErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing

        If txtProductCode.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If txtWEF.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub

        mSqlStr = " SELECT * " & vbCrLf & " FROM PRD_NEWBOM_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE='" & Trim(txtProductCode.Text) & "'" & vbCrLf & " AND WEF=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            MsgInformation("B.O.M. Exists, So cann't be Deleted.")
            Exit Sub
        End If

        If Not RsProdSeqMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then
                PubDBCn.Cancel()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "PRD_PRODSEQUENCE_HDR", txtProductCode.Text & ":" & VB6.Format(txtWEF.Text, "DD/MMM/YYYY"), RsProdSeqMain) = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "PRD_PRODSEQUENCE_HDR", "PRODUCT_CODE", txtProductCode.Text & ":" & VB6.Format(txtWEF.Text, "DD/MMM/YYYY")) = False Then GoTo DelErrPart

                PubDBCn.Execute("DELETE FROM PRD_OPR_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "' AND WEF=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')")
                PubDBCn.Execute("DELETE FROM PRD_PRODSEQUENCE_DET WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "' AND WEF=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')")
                PubDBCn.Execute("DELETE FROM PRD_PRODSEQUENCE_HDR WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "' AND WEF=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')")
                PubDBCn.CommitTrans()
                RsProdSeqMain.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Cancel()
        RsProdSeqMain.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing

        If CmdModify.Text = ConcmdmodifyCaption Then

            mSqlStr = " SELECT * " & vbCrLf & " FROM PRD_NEWBOM_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE='" & Trim(txtProductCode.Text) & "'" & vbCrLf & " AND WEF=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                MsgInformation("B.O.M. Exists, So cann't be Modify.")
                Exit Sub
            End If

            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsProdSeqMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            Call MakeEnableDesableField(False)
            SprdMain.Enabled = True
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification = False Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Update1 = True Then
            ADDMode = False
            MODIFYMode = False
            txtProductCode_Validating(txtProductCode, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True Then CmdAdd.Focus()
        Else
            MsgInformation("Record not saved")
            GoTo ErrorHandler
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Function Update1() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        If ADDMode = True Then
            SqlStr = " INSERT INTO PRD_PRODSEQUENCE_HDR " & vbCrLf & " (PRODUCT_CODE,COMPANY_CODE,WEF," & vbCrLf & " ADDUSER, ADDDATE, MODUSER, MODDATE) " & vbCrLf & " VALUES ( " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtProductCode.Text) & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"
        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE PRD_PRODSEQUENCE_HDR SET " & vbCrLf & " PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "', " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE ='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "' AND WEF=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        End If

        PubDBCn.Execute(SqlStr)
        If UpdateDetail = False Then GoTo ErrPart
        If UpdateOPRNDetail = False Then GoTo ErrPart
        Update1 = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsProdSeqMain.Requery()
        RsProdSeqDetail.Requery()
        MsgBox(Err.Description)
        '    Resume	
    End Function
    Private Function UpdateDetail() As Boolean

        On Error GoTo UpdateDetailERR
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mDept As String
        Dim mMinQty As Double
        Dim mMaxQty As Double

        PubDBCn.Execute("DELETE FROM PRD_OPR_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "' AND WEF=TO_DATE('" & vb6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')")
        PubDBCn.Execute("DELETE FROM PRD_PRODSEQUENCE_DET WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "' AND WEF=TO_DATE('" & vb6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')")


        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                SprdMain.Col = ColDept
                mDept = MainClass.AllowSingleQuote(.Text)

                SprdMain.Col = ColMinQty
                mMinQty = Val(.Text)

                SprdMain.Col = ColMaxQty
                mMaxQty = Val(.Text)

                SqlStr = ""

                If Trim(mDept) <> "" Then
                    SqlStr = " INSERT INTO  PRD_PRODSEQUENCE_DET ( " & vbCrLf & " COMPANY_CODE,PRODUCT_CODE,WEF,SERIAL_NO,DEPT_CODE,MIN_QTY,MAX_QTY) " & vbCrLf & " VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ",'" & MainClass.AllowSingleQuote(txtProductCode.Text) & "',TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & I & ",'" & mDept & "'," & mMinQty & "," & mMaxQty & " ) "
                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        UpdateDetail = True
        Exit Function
UpdateDetailERR:
        UpdateDetail = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub cmdSearchPCode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchPCode.Click
        Dim mSqlStr As String

        mSqlStr = " SELECT IH.PRODUCT_CODE, IH.WEF, INV.ITEM_SHORT_DESC, INV.ISSUE_UOM " & vbCrLf & " FROM PRD_NEWBOM_HDR IH, INV_ITEM_MST INV " & vbCrLf & " WHERE IH.COMPANY_CODE=INV.COMPANY_CODE " & vbCrLf & " AND IH.PRODUCT_CODE=INV.ITEM_CODE "

        mSqlStr = mSqlStr & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If Trim(txtProductCode.Text) <> "" Then
            mSqlStr = mSqlStr & vbCrLf & " AND IH.PRODUCT_CODE='" & Trim(txtProductCode.Text) & "'"
        End If

        If MainClass.SearchGridMasterBySQL2("", mSqlStr) = True Then
            txtWEF.Text = VB6.Format(AcName1, "DD/MM/YYYY")
            txtProductCode.Text = AcName
            If txtProductCode.Enabled = True Then txtProductCode.Focus()
            '        If ShowRecord = False Then Exit Sub
            'Call txtProductCode_Validate(False)
            txtProductCode_Validating(txtProductCode, New System.ComponentModel.CancelEventArgs(False))
        End If

    End Sub

    Private Sub cmdSearchWEF_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchWEF.Click
        Dim mSqlStr As String

        mSqlStr = " SELECT IH.WEF, IH.PRODUCT_CODE, INV.ITEM_SHORT_DESC, INV.ISSUE_UOM " & vbCrLf & " FROM PRD_NEWBOM_HDR IH, INV_ITEM_MST INV " & vbCrLf & " WHERE IH.COMPANY_CODE=INV.COMPANY_CODE " & vbCrLf & " AND IH.PRODUCT_CODE=INV.ITEM_CODE "

        mSqlStr = mSqlStr & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If Trim(txtProductCode.Text) <> "" Then
            mSqlStr = mSqlStr & vbCrLf & " AND IH.PRODUCT_CODE='" & Trim(txtProductCode.Text) & "'"
        End If

        If MainClass.SearchGridMasterBySQL2("", mSqlStr) = True Then
            txtWEF.Text = VB6.Format(AcName, "DD/MM/YYYY")
            txtProductCode.Text = AcName1
            If txtWEF.Enabled = True Then txtWEF.Focus()
            'Call txtProductCode_Validate(False)
            txtProductCode_Validating(txtProductCode, New System.ComponentModel.CancelEventArgs(False))
        End If

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
            '        ADataGrid.Refresh	
            FormatSprdView()
            SprdView.Refresh()
            SprdView.Focus()
            SprdView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            SprdView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsProdSeqMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmProdSeqMst_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Product Sequence Master"

        SqlStr = "Select * From PRD_PRODSEQUENCE_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProdSeqMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From PRD_PRODSEQUENCE_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProdSeqDetail, ADODB.LockTypeEnum.adLockReadOnly)

        AssignGrid(False)
        Call SetTextLengths()
        Call Clear1()
        If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume	
    End Sub

    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = ""

        SqlStr = " SELECT " & vbCrLf & " PRODUCT_CODE,WEF " & vbCrLf & " FROM PRD_PRODSEQUENCE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " ORDER BY PRODUCT_CODE,WEF "
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmProdSeqMst_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmProdSeqMst_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection	
        'PvtDBCn.Open StrConn	
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Left = 0
        Me.Top = 0
        Me.Height = VB6.TwipsToPixelsY(5580)
        Me.Width = VB6.TwipsToPixelsX(9375)

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


        txtProductCode.Text = ""
        lblProductCode.Text = ""
        txtWEF.Text = ""
        txtMainCode.Text = ""
        txtMainCode.Enabled = False

        Call DelTemp_OPRNDetail()

        Call MakeEnableDesableField(True)

        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsProdSeqMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume	
    End Sub
    Private Function CheckDuplicateDept(ByRef pDept As String) As Boolean

        On Error GoTo ERR1
        Dim I As Integer
        Dim mItemRept As Integer

        If pDept = "" Then CheckDuplicateDept = True : Exit Function
        With SprdMain
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColDept
                If UCase(Trim(.Text)) = UCase(Trim(pDept)) Then
                    mItemRept = mItemRept + 1
                    If mItemRept > 1 Then
                        CheckDuplicateDept = True
                        MsgInformation("Duplicate Deptt")
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColDept)
                        Exit Function
                    End If
                End If
            Next
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim SqlStr As String = ""


        With SprdMain
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = ColDept
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = RsProdSeqDetail.Fields("DEPT_CODE").DefinedSize
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 8)

            .Col = ColDeptDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 30)

            .Col = ColOPRN
            .CellType = SS_CELL_TYPE_BUTTON
            '.Lock = False	
            .TypeButtonText = "Details"
            .TypeButtonAlign = SS_CELL_BUTTON_ALIGN_LEFT
            .set_ColWidth(ColOPRN, 8)

            .Col = ColMinQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 3
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .set_ColWidth(.Col, 10)

            .Col = ColMaxQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 3
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .set_ColWidth(.Col, 10)

            MainClass.ProtectCell(SprdMain, 1, .MaxRows, ColDeptDesc, ColDeptDesc)
            MainClass.SetSpreadColor(SprdMain, Arow)
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume	
    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 600)
            .set_ColWidth(0, 500 * 1)
            .set_ColWidth(1, 500 * 3)
            .ColsFrozen = 2
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        txtProductCode.Maxlength = RsProdSeqMain.Fields("PRODUCT_CODE").Precision
        txtWEF.Maxlength = 10
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume	
    End Sub

    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim ii As Integer
        Dim pBOMDept As String

        FieldsVarification = True

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsProdSeqMain.EOF = True Then Exit Function

        If Trim(txtProductCode.Text) = "" Then
            MsgInformation("Product Code empty, So unable to save.")
            txtProductCode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtWEF.Text) = "" Then
            MsgInformation("WEF is empty, So unable to save.")
            txtWEF.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If IsDate(txtWEF.Text) = False Then
            MsgInformation("Invaild WEF, So unable to save.")
            txtWEF.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtProductCode.Text) <> Trim(txtMainCode.Text) Then
            MsgInformation("Please Enter Sequence in Item Code : " & Trim(txtMainCode.Text) & ". Cann't Be save.")
            txtProductCode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If CheckProductionDeptMissing(pBOMDept) = True Then
            If pBOMDept = "" Then
                MsgInformation("BOM not defined, So unable to save.")
            Else
                MsgInformation(pBOMDept & " is defined in BOM but you not defined in Sequence, So unable to save.")
            End If
            SprdMain.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidDataInGrid(SprdMain, ColDept, "S", "Please Check Deptt.") = False Then FieldsVarification = False : Exit Function

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function

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
                For cntRow = 1 To SprdMain.MaxRows
                    SprdMain.Row = cntRow
                    SprdMain.Col = ColDept
                    If Trim(SprdMain.Text) <> "" Then
                        If Trim(pBOMDept) = Trim(SprdMain.Text) Then
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


    Private Sub frmProdSeqMst_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        '    RsProdSeqMain.Close	
        RsProdSeqMain = Nothing
        '    RsProdSeqDetail.Close	
        RsProdSeqDetail = Nothing
        'PvtDBCn.Close	
        'Set PvtDBCn = Nothing	
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub SprdMain_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdMain.ButtonClicked
        Call ShowFormOPRNDetail(eventArgs.Col, eventArgs.Row)
    End Sub
    Private Sub ShowFormOPRNDetail(ByRef pCol As Integer, ByRef pRow As Integer)

        Dim I As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mDeptCode As String

        With SprdMain
            .Row = pRow

            .Col = ColDept
            mDeptCode = .Text
        End With
        If mDeptCode = "" Then Exit Sub

        Me.lblDetail.Text = "False"

        With FrmOPRDailyDetail
            .LblAddMode.Text = CStr(ADDMode)
            .LblModifyMode.Text = CStr(MODIFYMode)
            .lblProductCode.Text = txtProductCode.Text
            .lblDeptCode.Text = mDeptCode
            .ShowDialog()
        End With

        If Me.lblDetail.Text = "True" Then
            MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
            FrmOPRDailyDetail.Close()
        End If


    End Sub
    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim SqlStr As String = ""

        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If eventArgs.Row = 0 And eventArgs.Col = ColDept Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColDept

                If MainClass.SearchGridMaster(.Text, "PAY_DEPT_MST", "DEPT_CODE", "DEPT_DESC", "", "", SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColDept
                    .Text = Trim(AcName)

                    .Col = ColDeptDesc
                    .Text = Trim(AcName1)
                End If
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColDept, .ActiveRow, ColDeptDesc, .ActiveRow, False))
            End With
        End If

        If eventArgs.Row = 0 And eventArgs.Col = ColDeptDesc Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColDeptDesc
                If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", "", "", SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColDept
                    .Text = Trim(AcName1)

                    .Col = ColDeptDesc
                    .Text = Trim(AcName)
                End If
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColDept, .ActiveRow, ColDeptDesc, .ActiveRow, False))
            End With
        End If

        If eventArgs.Col = 0 And eventArgs.Row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdMain, eventArgs.Row, ColDept)
            MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColDept Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColDept, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColDeptDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColDeptDesc, 0))
        SprdMain.Refresh()
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim xDept As String
        If eventArgs.NewRow = -1 Then Exit Sub

        SprdMain.Row = SprdMain.ActiveRow
        SprdMain.Col = ColDept
        xDept = Trim(SprdMain.Text)
        If xDept = "" Then Exit Sub

        Select Case eventArgs.col
            Case ColDept
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColDept
                xDept = Trim(SprdMain.Text)
                If xDept = "" Then Exit Sub
                If CheckDept() = True Then
                    If CheckDuplicateDept(xDept) = False Then
                        MainClass.AddBlankSprdRow(SprdMain, ColDept, ConRowHeight)
                        FormatSprdMain((SprdMain.MaxRows))
                    End If
                End If
        End Select
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function CheckDept() As Boolean

        On Error GoTo CheckERR
        With SprdMain
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
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColDept)
            End If
        End With
        Exit Function
CheckERR:
        MsgBox(Err.Description)
    End Function

    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        With SprdMain
            SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub
    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Row = SprdView.ActiveRow

        SprdView.Col = 1
        txtProductCode.Text = SprdView.Text

        SprdView.Col = 2
        txtWEF.Text = SprdView.Text

        txtProductCode_Validating(txtProductCode, New System.ComponentModel.CancelEventArgs(False))
        Call CmdView_Click(CmdView, New System.EventArgs())
    End Sub
    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub


    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim SqlStr As String = ""
        Dim mMainItemCode As String

        If Not RsProdSeqMain.EOF Then
            IsShowing = True
            txtProductCode.Text = IIf(IsDbNull(RsProdSeqMain.Fields("PRODUCT_CODE").Value), "", RsProdSeqMain.Fields("PRODUCT_CODE").Value)


            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS = 'A' "
            If MainClass.ValidateWithMasterTable(txtProductCode.Text, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = False Then
                lblProductCode.Text = ""
            Else
                lblProductCode.Text = MasterNo
            End If
            txtWEF.Text = IIf(IsDbNull(RsProdSeqMain.Fields("WEF").Value), "", RsProdSeqMain.Fields("WEF").Value)

            mMainItemCode = GetMainItemCode((txtProductCode.Text))
            txtMainCode.Text = mMainItemCode

            Call ShowDetail1()
            Call ShowOperation()
            Call MakeEnableDesableField(True)
            IsShowing = False
        End If
        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = True    '' False Sandeep 15/05/2022
        '    txtProductCode.Enabled = True	
        '    cmdSearchPCode.Enabled = True	
        MainClass.ButtonStatus(Me, XRIGHT, RsProdSeqMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume	
    End Sub
    Private Sub ShowOperation()

        On Error GoTo ShowSerialNoErr
        Dim RsSRLNo As ADODB.Recordset
        Dim SqlStr As String = ""

        Call DelTemp_OPRNDetail()

        SqlStr = ""
        SqlStr = "INSERT INTO TEMP_PRD_OPR_TRN ( " & vbCrLf & " USERID, COMPANY_CODE, PRODUCT_CODE, WEF," & vbCrLf & " DEPT_CODE, OPR_SNO, OPR_CODE)" & vbCrLf & " SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " COMPANY_CODE, PRODUCT_CODE, WEF, " & vbCrLf & " DEPT_CODE, OPR_SNO, OPR_CODE " & vbCrLf & " FROM PRD_OPR_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "' " & vbCrLf & " AND WEF=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') ORDER BY DEPT_CODE,OPR_SNO"

        PubDBCn.Execute(SqlStr)

        Exit Sub
ShowSerialNoErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub DelTemp_OPRNDetail(Optional ByRef mDeptCode As String = "")

        Dim SqlStr As String = ""

        SqlStr = ""
        SqlStr = "DELETE FROM TEMP_PRD_OPR_TRN " _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
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


        With SprdMain
            For ii = 1 To .MaxRows - 1
                .Row = ii
                .Col = ColDept
                mDeptCode = Trim(.Text)

                SqlStr = "INSERT INTO PRD_OPR_TRN (" & vbCrLf _
                    & " COMPANY_CODE, PRODUCT_CODE, WEF,DEPT_CODE, " & vbCrLf _
                    & " OPR_SNO, OPR_CODE )" & vbCrLf _
                    & " SELECT " & vbCrLf _
                    & " COMPANY_CODE, PRODUCT_CODE, WEF,DEPT_CODE, " & vbCrLf _
                    & " OPR_SNO, OPR_CODE " & vbCrLf _
                    & " FROM TEMP_PRD_OPR_TRN " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf _
                    & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "'" & vbCrLf _
                    & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(mDeptCode) & "' AND WEF=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


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
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND PRODUCT_CODE ='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "' "
        PubDBCn.Execute(SqlStr)
        DeleteOPRNDetail = True
        Exit Function
DeleteOPRNDetailErr:
        MsgInformation(Err.Description)
        DeleteOPRNDetail = False
    End Function
    Private Sub ShowDetail1()

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mDeptt As String


        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf _
            & " FROM PRD_PRODSEQUENCE_DET " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "'" & vbCrLf _
            & " AND WEF=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " ORDER BY SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProdSeqDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsProdSeqDetail
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            I = 1
            Do While Not .EOF
                SprdMain.Row = I

                SprdMain.Col = ColDept
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("DEPT_CODE").Value), "", .Fields("DEPT_CODE").Value))
                mDeptt = SprdMain.Text

                SprdMain.Col = ColDeptDesc
                If MainClass.ValidateWithMasterTable(mDeptt, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ") = True Then
                    SprdMain.Text = MasterNo
                Else
                    SprdMain.Text = ""
                End If

                SprdMain.Col = ColMinQty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("MIN_QTY").Value), "", .Fields("MIN_QTY").Value)))

                SprdMain.Col = ColMaxQty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("MAX_QTY").Value), "", .Fields("MAX_QTY").Value)))

                .MoveNext()
                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtProductCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProductCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtProductCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProductCode.DoubleClick
        Call cmdSearchPCode_Click(cmdSearchPCode, New System.EventArgs())
    End Sub

    Private Sub txtProductCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtProductCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtProductCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtProductCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtProductCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchPCode_Click(cmdSearchPCode, New System.EventArgs())
    End Sub
    Private Sub txtProductCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtProductCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xProduct As String
        Dim xWEF As String
        Dim SqlStr As String = ""
        Dim mMainItemCode As String

        If Trim(txtProductCode.Text) = "" Then GoTo EventExitSub
        If Trim(txtWEF.Text) = "" Then GoTo EventExitSub

        If MODIFYMode = True And RsProdSeqMain.BOF = False Then
            xProduct = RsProdSeqMain.Fields("PRODUCT_CODE").Value
            xWEF = RsProdSeqMain.Fields("WEF").Value
        End If
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS = 'A' "
        If MainClass.ValidateWithMasterTable(txtProductCode.Text, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Not a valid Product Code.")
            Cancel = True
            Exit Sub
        Else
            lblProductCode.Text = MasterNo
        End If

        mMainItemCode = GetMainItemCode(Trim(txtProductCode.Text))
        txtMainCode.Text = mMainItemCode


        SqlStr = "SELECT * FROM PRD_PRODSEQUENCE_HDR " _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "'" & vbCrLf _
            & " AND WEF=TO_DATE('" & VB6.Format(txtWEF.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProdSeqMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsProdSeqMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Product. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM PRD_PRODSEQUENCE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(xProduct) & "'" & vbCrLf & " AND WEF='" & VB6.Format(xWEF, "DD/MMM/YYYY") & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProdSeqMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub MakeEnableDesableField(ByRef mMode As Boolean)
        txtProductCode.Enabled = mMode
        cmdSearchPCode.Enabled = mMode
        cmdSearchWEF.Enabled = mMode
        txtWEF.Enabled = mMode
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String)
        On Error GoTo ErrPart
        Dim mAmountInword As String

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\" & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        Report1.Action = 1
        Report1.Reset()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub ReportOnProdSeq(ByRef Mode As Crystal.DestinationConstants)

    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnProdSeq(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnProdSeq(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub txtWEF_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWEF.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtWEF_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWEF.DoubleClick
        Call cmdSearchWEF_Click(cmdSearchWEF, New System.EventArgs())
    End Sub


    Private Sub txtWEF_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtWEF.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtWEF.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtWEF_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtWEF.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchWEF_Click(cmdSearchWEF, New System.EventArgs())
    End Sub


    Private Sub txtWEF_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtWEF.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1

        If Trim(txtWEF.Text) = "" Then GoTo EventExitSub

        If IsDate(txtWEF.Text) = False Then
            MsgInformation("Invaild WEF.")
            Cancel = True
            GoTo EventExitSub
        End If
        Call txtProductCode_Validating(txtProductCode, New System.ComponentModel.CancelEventArgs(False))
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
