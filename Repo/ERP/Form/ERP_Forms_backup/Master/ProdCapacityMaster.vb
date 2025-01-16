Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmProdCapacityMaster
   Inherits System.Windows.Forms.Form
   Dim RsCapacityHdr As ADODB.Recordset
   Dim RsCapacityDet As ADODB.Recordset

   Dim xMyMenu As String

   Dim ADDMode As Boolean
   Dim MODIFYMode As Boolean
   Dim XRIGHT As String
   Dim FormActive As Boolean

   Private Const ConRowHeight As Short = 12

   Private Const ColDeptCode As Short = 1
   Private Const ColDeptName As Short = 2
   Private Const ColManpowerNo As Short = 3
   Private Const ColProdCapacity As Short = 4
   Dim mAmendStatus As Boolean

   Private Sub AssignGrid(ByRef mRefresh As Boolean)

      On Error GoTo AssignGridErr
        Dim SqlStr As String = "" 

        SqlStr = " SELECT IH.PRODUCT_CODE, TO_CHAR(WEF,'dd-MM-yyyy') AS WEF, AMEND_NO " & vbCrLf _
           & " FROM INV_ITEMWISE_CAPACITY_HDR IH" & vbCrLf _
           & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
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
            .set_ColWidth(1, 10)

            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle				
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub

    Private Function FieldsVarification() As Boolean

        On Error GoTo err_Renamed
        Dim mOldAmendNo As Integer
        Dim mLastestWEF As String
        Dim mSqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        FieldsVarification = True
        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsCapacityHdr.EOF = True Then Exit Function

        If MODIFYMode = True And chkStatus.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Closed Master Cann't be Modified")
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtProductCode.Text) = "" Then
            MsgBox("Product Code is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtProductCode.Enabled = True Then txtProductCode.Focus()
            Exit Function
        End If

        If Trim(txtProductDesc.Text) = "" Then
            MsgBox("Product Desc is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtProductDesc.Enabled = True Then txtProductDesc.Focus()
            Exit Function
        End If

        If Trim(txtWEF.Text) = "" Then
            MsgBox("W.E.F is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtWEF.Enabled = True Then txtWEF.Focus()
            Exit Function
        End If

        If Val(txtAmendNo.Text) > 0 Then
            mOldAmendNo = Val(txtAmendNo.Text) - 1
            mSqlStr = " SELECT WEF FROM INV_ITEMWISE_CAPACITY_HDR " & vbCrLf _
                             & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                             & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "'" & vbCrLf _
                             & " AND AMEND_NO=" & Val(mOldAmendNo) & ""

            MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                mLastestWEF = IIf(IsDBNull(RsTemp.Fields("WEF").Value), "", RsTemp.Fields("WEF").Value)
                If CDate(txtWEF.Text) <= CDate(mLastestWEF) Then
                    MsgBox("W.E.F Cann't be less than or equal to Last WEF.", MsgBoxStyle.Information)
                    FieldsVarification = False
                    If txtWEF.Enabled = True Then txtWEF.Focus()
                    Exit Function
                End If
            End If
        End If

        If MainClass.ValidDataInGrid(SprdMain, ColDeptCode, "S", "Dept Code Is Blank") = False Then FieldsVarification = False : Exit Function

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function

    Private Sub chkStatus_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkStatus.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtProductCode.Enabled = True
            cmdSearchProductCode.Enabled = True
            cmdSearchWEF.Enabled = True
            SprdMain.Enabled = True
        Else
            CmdAdd.Text = ConCmdAddCaption
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdAmend_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAmend.Click

        On Error GoTo ERR1
        Dim mItemCode As String
        Dim i As Integer


        txtAmendNo.Text = CStr(GetMaxAmendNo())
        chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked

        mAmendStatus = True
        cmdAmend.Enabled = False
        txtWEF.Enabled = True
        SprdMain.Enabled = True
        txtProductCode.Enabled = False
        cmdSearchProductCode.Enabled = False
        ADDMode = True
        MODIFYMode = False

        MainClass.ButtonStatus(Me, XRIGHT, RsCapacityHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click

        On Error GoTo DelErrPart

        If chkStatus.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Closed Master Cann't be Deleted")
            Exit Sub
        End If

        PubDBCn.Cancel()
        PubDBCn.BeginTrans()

        If Not RsCapacityHdr.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then

                If InsertIntoDelAudit(PubDBCn, "INV_ITEMWISE_CAPACITY_HDR ", (lblMKey.Text), RsCapacityHdr) = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "INV_ITEMWISE_CAPACITY_HDR ", "MKEY", (lblMKey.Text)) = False Then GoTo DelErrPart

                PubDBCn.Execute("DELETE FROM INV_ITEMWISE_CAPACITY_DET WHERE Mkey='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")
                PubDBCn.Execute("DELETE FROM INV_ITEMWISE_CAPACITY_HDR WHERE Mkey='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")

                If Val(txtAmendNo.Text) > 0 Then
                    If UpdatePreviousRate(Val(txtAmendNo.Text), "O") = False Then GoTo DelErrPart
                End If

                PubDBCn.CommitTrans()
                RsCapacityHdr.Requery()
                RsCapacityDet.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        RsCapacityHdr.Requery()
        RsCapacityDet.Requery()
        PubDBCn.Cancel()
        MsgBox(Err.Description)
    End Sub

    Private Function UpdatePreviousRate(ByRef pAmendNo As Integer, ByRef pPreviousStatus As String) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = "" 

        SqlStr = ""
        SqlStr = " UPDATE INV_ITEMWISE_CAPACITY_HDR  SET " & vbCrLf _
           & " STATUS = '" & pPreviousStatus & "', " & vbCrLf _
           & " MODUSER = '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
           & " MODDATE = TO_DATE('" & VB6.Format(RunDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')" & vbCrLf _
           & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
           & " AND PRODUCT_CODE = '" & MainClass.AllowSingleQuote(txtProductCode.Text) & "' AND AMEND_NO = " & pAmendNo - 1 & ""

        PubDBCn.Execute(SqlStr)

        UpdatePreviousRate = True

        Exit Function
ErrPart:
        UpdatePreviousRate = False
        If Err.Description = "" Then Exit Function
        MsgBox(Err.Description)
    End Function

    Private Function GetMaxAmendNo() As Integer

        On Error GoTo ErrPart
        Dim SqlStr As String = "" 
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = " SELECT MAX(AMEND_NO) AS AMEND_NO" & vbCrLf & " FROM INV_ITEMWISE_CAPACITY_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If IsDBNull(RsTemp.Fields("AMEND_NO").Value) Then
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
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsCapacityHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            Call MakeEnableDesableField(False)
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
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call PrintOprRate(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call PrintOprRate(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub PrintOprRate(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String = "" 
        Dim mTitle As String = "" 
        Dim mSubTitle As String = "" 
        Dim mRsTemp As ADODB.Recordset = Nothing

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        mTitle = "Manpower Sanctioned & Budget Master"

        SqlStr = " SELECT IH.*, ID.*, DEPT.* " & vbCrLf & " FROM INV_ITEMWISE_CAPACITY_HDR IH, INV_ITEMWISE_CAPACITY_DET ID, PAY_DEPT_MST DEPT " & vbCrLf & " WHERE IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=DEPT.COMPANY_CODE" & vbCrLf & " AND ID.DEPT_CODE=DEPT.DEPT_CODE " & vbCrLf & " AND IH.MKEY='" & MainClass.AllowSingleQuote(lblMKey.Text) & "' ORDER BY ID.DEPT_CODE"

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\PayManPowerBudget.rpt"

        SetCrpt(Report1, Mode, 1, mTitle, mSubTitle, True, xMyMenu)
        Report1.SQLQuery = SqlStr
        Report1.WindowShowGroupTree = False

        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub



    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If Update1() = True Then
            ADDMode = False
            MODIFYMode = False
            TxtWef_Validating(txtWEF, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True And CmdAdd.Visible = True Then CmdAdd.Focus()
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

    Private Sub cmdSearchProductCode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchProductCode.Click
        Dim mSqlStr As String = ""

        mSqlStr = " SELECT IH.PRODUCT_CODE, TO_CHAR(IH.WEF,'dd-MM-yyyy') AS WEF, INV.ITEM_SHORT_DESC, INV.CUSTOMER_PART_NO " & vbCrLf _
              & " FROM INV_ITEMWISE_CAPACITY_HDR IH, INV_ITEM_MST INV " & vbCrLf _
              & " WHERE IH.COMPANY_CODE=INV.COMPANY_CODE " & vbCrLf _
              & " AND IH.PRODUCT_CODE=INV.ITEM_CODE "

        mSqlStr = mSqlStr & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If Trim(txtProductCode.Text) <> "" Then
            mSqlStr = mSqlStr & vbCrLf & " AND IH.PRODUCT_CODE='" & Trim(txtProductCode.Text) & "'"
        End If

        If MainClass.SearchGridMasterBySQL2("", mSqlStr) = True Then
            txtWEF.Text = VB6.Format(AcName1, "dd-MM-yyyy")
            txtProductCode.Text = AcName
            If txtProductCode.Enabled = True Then txtProductCode.Focus()
            If ShowRecord() = False Then Exit Sub
        End If
    End Sub

    Private Sub cmdSearchWEF_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchWEF.Click
        On Error GoTo SrchERR
        Dim SqlStr As String = "" 

        SqlStr = " SELECT IH.WEF" & vbCrLf _
                  & " FROM INV_ITEMWISE_CAPACITY_HDR IH " & vbCrLf _
                  & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                  & " AND IH.PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "'"

        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtWEF.Text = VB6.Format(AcName, "dd-MM-yyyy")
            If txtWEF.Enabled = True Then txtWEF.Focus()
        End If
        Exit Sub
SrchERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Private Sub frmProdCapacityMaster_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = "" 
        If FormActive = True Then Exit Sub

        Me.Text = "Product Capacity Master"

        SqlStr = "Select * from INV_ITEMWISE_CAPACITY_HDR  Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCapacityHdr, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * from INV_ITEMWISE_CAPACITY_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCapacityDet, ADODB.LockTypeEnum.adLockReadOnly)

        FormatSprdMain(-1)

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

    Private Sub frmProdCapacityMaster_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmProdCapacityMaster_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub frmProdCapacityMaster_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        xMyMenu = myMenu
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        ADDMode = False
        MODIFYMode = False
        Call SetMainFormCordinate(Me)
        'Me.Top = 0
        'Me.Left = 0
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
        With RsCapacityHdr
            txtProductCode.MaxLength = .Fields("PRODUCT_CODE").DefinedSize
            txtWEF.MaxLength = .Fields("WEF").DefinedSize - 6
            txtRemarks.MaxLength = .Fields("Remarks").DefinedSize
        End With
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        '    Resume				
    End Sub

    Private Sub MakeEnableDesableField(ByRef mMode As Boolean)
        txtProductCode.Enabled = mMode
        cmdSearchProductCode.Enabled = mMode
        txtWEF.Enabled = mMode
        cmdSearchWEF.Enabled = mMode
        txtAmendNo.Enabled = False
    End Sub

    Private Sub frmProdCapacityMaster_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs)
        On Error Resume Next
        FormActive = False
        RsCapacityHdr.Close()
        RsCapacityDet.Close()

        RsCapacityHdr = Nothing
        RsCapacityDet = Nothing
    End Sub

    Private Sub Clear1()

        lblMKey.Text = ""
        txtProductCode.Text = ""
        txtProductDesc.Text = ""
        txtWEF.Text = ""
        lblWEF.Text = ""
        txtRemarks.Text = ""
        txtAmendNo.Text = "0"
        chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked

        chkStatus.Enabled = False
        mAmendStatus = False
        cmdAmend.Enabled = True

        MainClass.ClearGrid(SprdMain)
        FormatSprdMain(-1)
        Call MakeEnableDesableField(True)
        MainClass.ButtonStatus(Me, XRIGHT, RsCapacityHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub FormatSprdMain(ByRef mRow As Integer)

        On Error GoTo ERR1
        With SprdMain
            .Row = mRow
            .set_RowHeight(0, ConRowHeight * 2)
            .set_RowHeight(mRow, ConRowHeight)


            .Col = ColDeptCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsCapacityDet.Fields("DEPT_CODE").DefinedSize
            .set_ColWidth(.Col, 8)

            .Col = ColDeptName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("DEPT_DESC", "PAY_DEPT_MST", PubDBCn)
            .set_ColWidth(.Col, 35)

            .Col = ColManpowerNo
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeNumberMax = CDbl("999")
            .TypeNumberMin = CDbl("-99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 15)

            .Col = ColProdCapacity
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 15)

        End With
        MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, 1, SprdMain.MaxCols)

        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColDeptName, ColDeptName)

        MainClass.SetSpreadColor(SprdMain, mRow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then
            RsCapacityDet.Requery()
        End If
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub

    Private Sub Show1()

        On Error GoTo ERR1

        With RsCapacityHdr
            If Not .EOF Then
                ADDMode = False
                MODIFYMode = False
                lblMKey.Text = .Fields("MKey").Value
                txtProductCode.Text = Trim(IIf(IsDBNull(.Fields("PRODUCT_CODE").Value), "", .Fields("PRODUCT_CODE").Value))

                If MainClass.ValidateWithMasterTable(txtProductCode.Text, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtProductDesc.Text = MasterNo
                End If


                txtWEF.Text = IIf(IsDBNull(.Fields("WEF").Value), "", .Fields("WEF").Value)
                lblWEF.Text = IIf(IsDBNull(.Fields("WEF").Value), "", .Fields("WEF").Value)
                txtAmendNo.Text = IIf(IsDBNull(.Fields("AMEND_NO").Value), "", .Fields("AMEND_NO").Value)
                chkStatus.CheckState = IIf(.Fields("Status").Value = "O", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
                txtRemarks.Text = IIf(IsDBNull(.Fields("Remarks").Value), "", .Fields("Remarks").Value)
                Call ShowDetail()

                Call MakeEnableDesableField(True)

            End If
        End With
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsCapacityHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        SprdMain.Enabled = True
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub

    Private Sub ShowDetail()

        On Error GoTo ERR1
        Dim i As Integer
        Dim SqlStr As String = "" 
        Dim mItemDesc As String
        Dim cntRow As Integer
        Dim mDeptCode As String

        SqlStr = ""
        SqlStr = " SELECT * FROM INV_ITEMWISE_CAPACITY_DET " & vbCrLf & " WHERE Mkey='" & MainClass.AllowSingleQuote(lblMKey.Text) & "'" & vbCrLf & " ORDER BY DEPT_CODE"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCapacityDet, ADODB.LockTypeEnum.adLockReadOnly)

        With RsCapacityDet
            If .EOF = False Then
                i = 1
                Do While Not .EOF
                    SprdMain.Row = i

                    SprdMain.Col = ColDeptCode
                    SprdMain.Text = IIf(IsDBNull(.Fields("DEPT_CODE").Value), "", .Fields("DEPT_CODE").Value)

                    SprdMain.Col = ColDeptName
                    If MainClass.ValidateWithMasterTable(.Fields("DEPT_CODE").Value, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        SprdMain.Text = MasterNo
                    End If

                    SprdMain.Col = ColManpowerNo
                    SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("MANPOWER_DAY").Value), "", .Fields("MANPOWER_DAY").Value)))
                    '				
                    SprdMain.Col = ColProdCapacity
                    SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("CAPACITY_DAY").Value), "", .Fields("CAPACITY_DAY").Value)))

                    .MoveNext()
                    i = i + 1
                    SprdMain.MaxRows = i
                Loop
            End If
        End With

        FormatSprdMain(-1)
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub

    Private Function Update1() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = "" 
        Dim mMKey As String
        Dim mStatus As String

        mStatus = IIf(chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked, "O", "C")

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If ADDMode = True Then
            mMKey = Trim(txtProductCode.Text) & VB6.Format(txtAmendNo.Text, "000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
            lblMKey.Text = mMKey

            SqlStr = " INSERT INTO INV_ITEMWISE_CAPACITY_HDR  (" & vbCrLf & " MKEY, COMPANY_CODE, PRODUCT_CODE, " & vbCrLf & " WEF, AMEND_NO, REMARKS, STATUS," & vbCrLf & " ADDUSER, ADDDATE, " & vbCrLf & " MODUSER, MODDATE ) VALUES ( "

            SqlStr = SqlStr & vbCrLf & " '" & MainClass.AllowSingleQuote(mMKey) & "'," & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
               & " '" & MainClass.AllowSingleQuote(txtProductCode.Text) & "', " & vbCrLf _
               & " TO_DATE('" & VB6.Format(txtWEF.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY')," & Val(txtAmendNo.Text) & ", " & vbCrLf _
               & " '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', '" & mStatus & "', " & vbCrLf _
               & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(RunDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','')"
            '    WEF AMEND_NO				

        ElseIf MODIFYMode = True Then
            SqlStr = ""
            SqlStr = " UPDATE INV_ITEMWISE_CAPACITY_HDR   SET " & vbCrLf _
               & " PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "', " & vbCrLf _
               & " WEF=TO_DATE('" & VB6.Format(txtWEF.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY'), " & vbCrLf _
               & " AMEND_NO=" & Val(txtAmendNo.Text) & ", " & vbCrLf _
               & " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
               & " STATUS='" & mStatus & "', " & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
               & " MODDATE=TO_DATE('" & VB6.Format(RunDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
               & " WHERE Mkey ='" & MainClass.AllowSingleQuote(lblMKey.Text) & "'"

        End If

        PubDBCn.Execute(SqlStr)
        If UpdateDetail1() = False Then GoTo ErrPart

        If Val(txtAmendNo.Text) > 0 Then
            If UpdatePreviousRate(Val(txtAmendNo.Text), "C") = False Then GoTo ErrPart
        End If

        Update1 = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsCapacityHdr.Requery()
        RsCapacityDet.Requery()
        If Err.Description = "" Then Exit Function
        MsgBox(Err.Description)
    End Function

    Private Function UpdateDetail1() As Boolean

        On Error GoTo UpdateDetail1
        Dim SqlStr As String = "" 
        Dim i As Integer
        Dim mDeptCode As String
        Dim mNos As Short
        Dim mProdCapacity As Double

        PubDBCn.Execute("DELETE FROM INV_ITEMWISE_CAPACITY_DET  " & vbCrLf & " WHERE Mkey='" & MainClass.AllowSingleQuote(UCase(lblMKey.Text)) & "'")

        With SprdMain
            For i = 1 To .MaxRows - 1
                .Row = i

                .Col = ColDeptCode
                mDeptCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColManpowerNo
                mNos = Val(.Text)

                .Col = ColProdCapacity
                mProdCapacity = Val(.Text)

                SqlStr = ""
                If Trim(mDeptCode) <> "" Then
                    SqlStr = " INSERT INTO  INV_ITEMWISE_CAPACITY_DET ( " & vbCrLf & " MKEY, COMPANY_CODE, " & vbCrLf & " DEPT_CODE, " & vbCrLf & " MANPOWER_DAY, CAPACITY_DAY" & vbCrLf & " ) VALUES ( " & vbCrLf & " '" & MainClass.AllowSingleQuote(lblMKey.Text) & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " '" & mDeptCode & "', " & vbCrLf & " " & mNos & ", " & mProdCapacity & ")"

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

    Private Sub ViewGrid()

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
        MainClass.ButtonStatus(Me, XRIGHT, RsCapacityHdr, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub


    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        On Error GoTo ErrPart

        SprdView.Row = SprdView.ActiveRow

        SprdView.Col = 1
        txtProductCode.Text = Trim(SprdView.Text)

        SprdView.Col = 2
        txtWEF.Text = VB6.Format(SprdView.Text, "dd-MM-yyyy")

        If ShowRecord() = False Then GoTo ErrPart

        ViewGrid()
        Exit Sub
ErrPart:
        MsgInformation(Err.Description)
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        On Error GoTo ERR1
        Dim mDeptCode As String
        Dim SqlStr As String = "" 
        Dim mCCDesc As String

        If eventArgs.row = 0 And eventArgs.col = ColDeptCode Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColDeptCode
                If MainClass.SearchGridMaster(.Text, "PAY_DEPT_MST", "DEPT_CODE", "DEPT_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow
                    .Col = ColDeptCode
                    .Text = AcName
                    .Col = ColDeptName
                    .Text = AcName1
                End If
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColDeptName Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColDeptName
                If MainClass.SearchGridMaster(.Text, "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow
                    .Col = ColDeptCode
                    .Text = AcName1
                    .Col = ColDeptName
                    .Text = AcName
                End If
            End With
        End If

        If eventArgs.col = 0 And eventArgs.row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColDeptCode)
            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)

    End Sub

    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColDeptCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColDeptCode, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColDeptName Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColDeptName, 0))
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim mDeptCode As String
        Dim mRow As Integer

        If eventArgs.newRow = -1 Then Exit Sub
        mRow = SprdMain.ActiveRow
        '    SprdMain.Row=eventArgs.Row				
        '    If Trim(SprdMain.Text) = "" Then Exit Sub				

        Select Case eventArgs.col
            Case ColDeptCode
                SprdMain.Row = mRow
                SprdMain.Col = ColDeptCode
                If Trim(SprdMain.Text) <> "" Then
                    If MainClass.ValidateWithMasterTable(SprdMain.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColDeptCode)
                        eventArgs.cancel = True
                    Else
                        SprdMain.Col = ColDeptName
                        SprdMain.Text = MasterNo
                    End If
                    MainClass.AddBlankSprdRow(SprdMain, ColDeptCode, ConRowHeight)
                    FormatSprdMain(eventArgs.row)
                End If
                If DuplicateRow() = True Then
                    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColDeptCode)
                    eventArgs.cancel = True
                    Exit Sub
                End If
            Case ColDeptName
                SprdMain.Row = mRow
                SprdMain.Col = ColDeptName
                If Trim(SprdMain.Text) <> "" Then
                    If MainClass.ValidateWithMasterTable(SprdMain.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColDeptCode)
                        eventArgs.cancel = True
                    End If
                End If
        End Select

        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Function DuplicateRow() As Boolean
        Dim cntRow As Integer
        Dim mCount As Byte
        Dim mCheckString As String
        Dim mRowString As String
        Dim mDeptCode As String

        DuplicateRow = False

        With SprdMain
            .Row = .ActiveRow

            .Col = ColDeptCode
            mCheckString = UCase(Trim(.Text))

            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColDeptCode
                mDeptCode = UCase(Trim(.Text))
                mRowString = UCase(Trim(.Text))

                If mCheckString = mRowString Then
                    mCount = mCount + 1
                End If

                If mCount > 1 Then
                    DuplicateRow = True
                    MsgInformation("Duplicate Dept : " & mDeptCode)
                    Exit Function
                End If
            Next
        End With
    End Function
    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        'With SprdMain
        '    SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        'End With
    End Sub

    Private Sub txtAmendNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAmendNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Function ShowRecord() As Boolean

        On Error GoTo ERR1
        Dim mRs As ADODB.Recordset = Nothing
        Dim SqlStr As String = "" 
        Dim mWef As String = ""
        Dim xMkey As String = "" 

        ShowRecord = True

        If Trim(txtWEF.Text) = "" Then
            SqlStr = " SELECT MAX(WEF) AS WEF " & vbCrLf _
                         & " FROM INV_ITEMWISE_CAPACITY_HDR  " & vbCrLf _
                         & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                         & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCapacityHdr, ADODB.LockTypeEnum.adLockReadOnly)
            If RsCapacityHdr.EOF = True Then
                Exit Function
            End If
        End If

        If MODIFYMode = True And RsCapacityHdr.EOF = False Then xMkey = RsCapacityHdr.Fields("mKey").Value
        SqlStr = " SELECT * FROM INV_ITEMWISE_CAPACITY_HDR  " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "'"

        If Trim(txtWEF.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND WEF=TO_DATE('" & VB6.Format(txtWEF.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND WEF = (" & vbCrLf & " SELECT MAX(WEF) AS WEF " & vbCrLf & " FROM INV_ITEMWISE_CAPACITY_HDR  " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "')"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCapacityHdr, ADODB.LockTypeEnum.adLockReadOnly)
        If RsCapacityHdr.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Operation Rate Not Entered For This Item. Click Add For New.", MsgBoxStyle.Information)
                ShowRecord = False
            ElseIf MODIFYMode = True Then
                Sqlstr = "SELECT * FROM INV_ITEMWISE_CAPACITY_HDR " & vbCrLf _
                   & " WHERE UPPER(LTRIM(RTRIM(MKey)))='" & MainClass.AllowSingleQuote(UCase(xMkey)) & "' " & vbCrLf _
                   & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCapacityHdr, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Sub txtProductCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProductCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchProductCode_Click(cmdSearchProductCode, New System.EventArgs())
    End Sub

    Private Sub txtProductCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtProductCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim mRs As ADODB.Recordset = Nothing
        Dim SqlStr As String = "" 
        Dim xMkey As String = "" 
        If Trim(txtProductCode.Text) = "" Then GoTo EventExitSub

        SqlStr = " SELECT ITEM_SHORT_DESC,ISSUE_UOM,ITEM_MODEL,CUSTOMER_PART_NO " & " FROM INV_ITEM_MST " & " WHERE ITEM_CODE='" & MainClass.AllowSingleQuote(txtProductCode.Text) & "'" & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS = 'A' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRs, ADODB.LockTypeEnum.adLockReadOnly)
        If Not mRs.EOF Then
            txtProductDesc.Text = IIf(IsDBNull(mRs.Fields("Item_Short_Desc").Value), "", mRs.Fields("Item_Short_Desc").Value)
        Else
            txtProductDesc.Text = ""
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

    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub TxtWef_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWEF.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtWEF_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtWEF.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchWEF_Click(cmdSearchWEF, New System.EventArgs())
    End Sub
    Private Sub TxtWef_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtWEF.Validating
      Dim Cancel As Boolean = eventArgs.Cancel
      On Error GoTo ERR1
      If Trim(txtWEF.Text) = "" Then GoTo EventExitSub

      If Not IsDate(txtWEF.Text) Then
         MsgBox("Not a valid date.")
         Cancel = True
         GoTo EventExitSub
      End If

      If mAmendStatus = True Then
         If CDate(txtWEF.Text) <= CDate(lblWEF.Text) Then
            MsgBox("W.E.F. Date Should be greater than Previous Date")
            Cancel = True
            GoTo EventExitSub
         End If
      End If

      If ShowRecord = False Then Cancel = True


      GoTo EventExitSub
ERR1:
      MsgBox(Err.Description)
EventExitSub:
      eventArgs.Cancel = Cancel
   End Sub
End Class
