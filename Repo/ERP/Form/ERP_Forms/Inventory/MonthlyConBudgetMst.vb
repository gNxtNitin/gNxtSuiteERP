Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmMonthlyConBudgetMst
    Inherits System.Windows.Forms.Form
    Dim RsBudgetMain As ADODB.Recordset ''ADODB.Recordset
    Dim RsBudgetDetail As ADODB.Recordset ''ADODB.Recordset

    'Private PvtDBCn As ADODB.Connection

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

    Private Const ConRowHeight As Short = 14

    Dim pmyMenu As String

    Private Const ColItemCode As Short = 1
    Private Const ColItemName As Short = 2
    Private Const ColItemUOM As Short = 3
    Private Const ColQty As Short = 4
    Private Const ColRemarks As Short = 5

    Dim mAmendStatus As Boolean
    Private Sub chkStatus_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkStatus.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            SprdMain.Enabled = True
            txtRefNo.Enabled = False
            cmdSearchRef.Enabled = False
            cmdSearchAmend.Enabled = False

        Else
            ADDMode = False
            MODIFYMode = False
            If RsBudgetMain.EOF = False Then RsBudgetMain.MoveFirst()
            Show1()
            txtRefNo.Enabled = True
            cmdSearchRef.Enabled = True
            cmdSearchAmend.Enabled = True

        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
        'Resume
    End Sub

    Private Sub cmdAmend_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAmend.Click

        Dim mRefNo As Double
        'Dim I As Integer

        mRefNo = Val(txtRefNo.Text)

        If mRefNo = 0 Then
            MsgInformation("Please Select PO.")
            Exit Sub
        End If

        Call txtRefNo_Validating(txtRefNo, New System.ComponentModel.CancelEventArgs(True))

        If CheckUnPostedRef(mRefNo) = True Then
            txtRefNo.Enabled = True
            cmdSearchRef.Enabled = True
            cmdSearchAmend.Enabled = True
            cmdSearchAmend.Focus()
            Exit Sub
        End If

        txtAmendNo.Text = CStr(GetMaxAmendNo(mRefNo))
        txtAmendDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        ChkActivate.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked

        txtDivision.Enabled = IIf(Trim(txtDivision.Text) = "", True, False)
        cmdDivSearch.Enabled = IIf(Trim(txtDivision.Text) = "", True, False)

        mAmendStatus = True
        cmdAmend.Enabled = False

        ADDMode = True
        MODIFYMode = False
        SprdMain.Enabled = True

        txtRefNo.Enabled = False
        cmdSearchRef.Enabled = False
        cmdSearchAmend.Enabled = False
        MainClass.ButtonStatus(Me, XRIGHT, RsBudgetMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        On Error Resume Next
        Me.Close()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If ValidateBranchLocking((txtAmendDate.Text)) = True Then
            Exit Sub
        End If

        If ValidateBookLocking(PubDBCn, CInt(ConLockPO), txtRefDate.Text) = True Then
            Exit Sub
        End If

        If MainClass.GetUserCanModify((txtAmendDate.Text)) = False Then
            MsgBox("You Have Not Rights to delete back Entry", MsgBoxStyle.Information)
            Exit Sub
        End If

        If chkStatus.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Posted Budget Cann't be Deleted")
            Exit Sub
        End If

        If txtRefNo.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If Not RsBudgetMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Cancel()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "INV_BUDGET_HDR", (txtRefNo.Text), RsBudgetMain, "REF NO") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "INV_BUDGET_HDR", "MKEY", (lblMKey.Text)) = False Then GoTo DelErrPart

                PubDBCn.Execute("DELETE FROM INV_BUDGET_DET WHERE MKEY=" & Val(lblMKey.Text) & "")
                PubDBCn.Execute("DELETE FROM INV_BUDGET_HDR WHERE MKEY=" & Val(lblMKey.Text) & "")

                PubDBCn.CommitTrans()
                RsBudgetMain.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Cancel()
        RsBudgetMain.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr

        '    If chkStatus.Value = vbChecked Then
        '        MsgInformation "Posted PO Cann't be Modified"
        '        Exit Sub
        '    End If

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsBudgetMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True

            txtRefNo.Enabled = False
            cmdSearchRef.Enabled = False
            cmdSearchAmend.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportonPO(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportonPO(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportonPO(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""
        Dim SqlStr As String = ""
        Dim mRptFileName As String


        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        SqlStr = ""
        mSubTitle = ""

        mTitle = "Monthly Consumable Budget Entry"

        mSubTitle = Trim(lblDeptname.Text)
        mSubTitle = mSubTitle & " - " & Trim(lblDivision.Text)

        If Val(txtAmendNo.Text) > 0 Then
            mSubTitle = mSubTitle & "-AMENDMENT"
        End If

        Call MainClass.ClearCRptFormulas(Report1)

        Call SelectQryForPO(SqlStr)
        mRptFileName = "MOnthlyConsBudget.rpt"


        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName, False)

        Exit Sub
ERR1:
        MsgInformation(Err.Description)

    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String, ByRef ISAnnexPrint As Boolean)
        On Error GoTo ErrPart
        Dim mAmountInword As String
        Dim mCurr As String
        Dim CntRow As Integer
        Dim mItemValue As Double
        Dim SqlStrSub As String


        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        Report1.ReportFileName = PubReportFolderPath & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        Report1.Action = 1
        Report1.Reset()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
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
            txtRefNo_Validating(txtRefNo, New System.ComponentModel.CancelEventArgs(False))
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
        Dim mRefNo As Double
        Dim mStatus As String
        Dim mActivate As String
        Dim mAmendNo As Integer

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mStatus = IIf(chkStatus.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mActivate = IIf(ChkActivate.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        SqlStr = ""
        mRefNo = Val(txtRefNo.Text)
        If Val(txtRefNo.Text) = 0 Then
            mRefNo = AutoGenRefNoSeq()
        End If
        txtRefNo.Text = CStr(mRefNo)

        mAmendNo = Val(txtAmendNo.Text)

        txtAmendNo.Text = CStr(Val(CStr(mAmendNo)))



        If ADDMode = True Then
            lblMKey.Text = mRefNo & VB6.Format(mAmendNo, "000")
            SqlStr = " INSERT INTO INV_BUDGET_HDR ( " & vbCrLf & " MKEY, AUTO_KEY_REF, COMPANY_CODE, " & vbCrLf & " REF_DATE, AMEND_NO, AMEND_DATE, " & vbCrLf & " AMEND_WEF_DATE, BUDGET_STATUS, BUDGET_CLOSED, " & vbCrLf & " UPDATE_FROM, DEPT_CODE, COST_CENTER_CODE, " & vbCrLf & " DIV_CODE, ADDUSER, ADDDATE, " & vbCrLf & " MODUSER, MODDATE )"

            SqlStr = SqlStr & vbCrLf & " VALUES ( " & vbCrLf & " " & Val(lblMkey.Text) & "," & mRefNo & ", " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " TO_DATE('" & VB6.Format(txtRefDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & Val(CStr(mAmendNo)) & ", TO_DATE('" & VB6.Format(txtAmendDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & mStatus & "', '" & mActivate & "', " & vbCrLf & " 'N', '" & MainClass.AllowSingleQuote((txtDept.Text)) & "', '" & MainClass.AllowSingleQuote((txtCost.Text)) & "'," & vbCrLf & " " & Val(txtDivision.Text) & ", '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','')"
        End If

        If MODIFYMode = True Then
            SqlStr = " UPDATE INV_BUDGET_HDR SET " & vbCrLf & " MKEY=" & Val(lblMkey.Text) & ", " & vbCrLf & " AUTO_KEY_REF=" & mRefNo & ", " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf & " REF_DATE=TO_DATE('" & VB6.Format(txtRefDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " AMEND_NO=" & Val(CStr(mAmendNo)) & ", " & vbCrLf & " AMEND_DATE=TO_DATE('" & VB6.Format(txtAmendDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " AMEND_WEF_DATE=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " BUDGET_STATUS='" & mStatus & "', " & vbCrLf & " BUDGET_CLOSED='" & mActivate & "', " & vbCrLf & " UPDATE_FROM='N', " & vbCrLf & " DEPT_CODE='" & MainClass.AllowSingleQuote((txtDept.Text)) & "', " & vbCrLf & " COST_CENTER_CODE='" & MainClass.AllowSingleQuote((txtCost.Text)) & "', " & vbCrLf & " DIV_CODE=" & Val(txtDivision.Text) & ", " & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND MKEY =" & Val(lblMkey.Text) & ""
        End If


        PubDBCn.Execute(SqlStr)
        If UpdateDetail1(mRefNo) = False Then GoTo ErrPart
        Update1 = True
        PubDBCn.CommitTrans()
        txtRefNo.Text = CStr(mRefNo)
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsBudgetMain.Requery()
        MsgBox(Err.Description)
        ''Resume
    End Function
    Private Function AutoGenRefNoSeq() As Double

        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset = Nothing
        Dim mAutoGen As Integer
        Dim SqlStr As String = ""
        Dim mStartingChk As Double
        Dim mMaxValue As String
        mAutoGen = 1

        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_REF)  " & vbCrLf & " FROM INV_BUDGET_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_REF,LENGTH(AUTO_KEY_REF)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAutoGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsAutoGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mMaxValue = .Fields(0).Value
                    mAutoGen = CInt(Mid(mMaxValue, 1, Len(mMaxValue) - 6))
                    mAutoGen = mAutoGen + 1
                Else
                    mAutoGen = 1
                End If
            End If
        End With

        AutoGenRefNoSeq = CDbl(mAutoGen & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"))
        RsAutoGen.Close()
        RsAutoGen = Nothing
        Exit Function
AutogenErr:
        MsgBox(Err.Description)
    End Function

    Private Function UpdateDetail1(ByRef mRefNo As Double) As Boolean

        On Error GoTo UpdateDetail1
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mItemCode As String
        Dim mItemUOM As String = ""
        Dim mQty As Double
        Dim mRemarks As String

        SqlStr = "Delete From  INV_BUDGET_DET " & vbCrLf & " Where " & vbCrLf & " MKEY=" & Val(lblMKey.Text) & ""

        PubDBCn.Execute(SqlStr)

        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColItemUOM
                mItemUOM = MainClass.AllowSingleQuote(.Text)

                .Col = ColQty
                mQty = Val(.Text)

                .Col = ColRemarks
                mRemarks = MainClass.AllowSingleQuote(.Text)

                If mItemCode <> "" Then
                    SqlStr = " INSERT INTO INV_BUDGET_DET ( " & vbCrLf & " MKEY, AUTO_KEY_REF, SERIAL_NO, ITEM_CODE, " & vbCrLf & " ITEM_UOM, ITEM_QTY, COMPANY_CODE, REMARKS) "

                    SqlStr = SqlStr & vbCrLf & " VALUES ( " & vbCrLf & " " & Val(lblMKey.Text) & ", " & mRefNo & ", " & I & ", " & vbCrLf & " '" & mItemCode & "','" & mItemUOM & "', " & vbCrLf & " " & mQty & ", " & RsCompany.Fields("COMPANY_CODE").Value & ", '" & MainClass.AllowSingleQuote(mRemarks) & "')"

                    PubDBCn.Execute(SqlStr)

                End If
            Next
        End With

        UpdateDetail1 = True

        Exit Function
UpdateDetail1:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function
    Private Sub cmdSearchAmend_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchAmend.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        If Trim(txtRefNo.Text) = "" Then
            Exit Sub
        End If

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_REF=" & Val(txtRefNo.Text) & ""

        If MainClass.SearchGridMaster("", "INV_BUDGET_HDR", "trim(TO_CHAR(AMEND_NO,'000'))", "AMEND_DATE", , , SqlStr) = True Then
            txtAmendNo.Text = AcName
            txtAmendDate.Text = AcName1
            txtRefNo_Validating(txtRefNo, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdSearchCC_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchCC.Click
        Call txtCost_DoubleClick(txtCost, New System.EventArgs())
    End Sub

    Private Sub cmdSearchDept_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchDept.Click
        Call TxtDept_DoubleClick(TxtDept, New System.EventArgs())
    End Sub

    Private Sub cmdSearchRef_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchRef.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtRefNo.Text), "INV_BUDGET_HDR", "AUTO_KEY_REF", "AMEND_NO", "REF_DATE", "DEPT_CODE", SqlStr) = True Then
            txtRefNo.Text = AcName
            txtAmendNo.Text = AcName1
            txtRefNo_Validating(txtRefNo, New System.ComponentModel.CancelEventArgs(False))
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
            '        ADataGrid.Refresh
            FormatSprdView()
            SprdView.Refresh()
            SprdView.Focus()
            SprdView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            SprdView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsBudgetMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Public Sub frmMonthlyConBudgetMst_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Monthly Consumable Budget Master"

        SqlStr = "Select * From INV_BUDGET_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBudgetMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From INV_BUDGET_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBudgetDetail, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)

        SetTextLengths()
        Clear1()
        If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
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

        SqlStr = " SELECT " & vbCrLf & " A.MKEY AS MKEY, A.AUTO_KEY_REF AS REF_NO, A.REF_DATE AS REF_DATE, " & vbCrLf & " A.AMEND_NO, A.AMEND_DATE,  " & vbCrLf & " A.AMEND_WEF_DATE AS WEF, A.DEPT_CODE " & vbCrLf & " FROM INV_BUDGET_HDR A" & vbCrLf & " WHERE" & vbCrLf & " A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        SqlStr = SqlStr & " ORDER BY SUBSTR(AUTO_KEY_REF,LENGTH(AUTO_KEY_REF)-5,4),A.AUTO_KEY_REF,A.AMEND_NO"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmMonthlyConBudgetMst_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmMonthlyConBudgetMst_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        Call SetMainFormCordinate(Me)
        pmyMenu = myMenu
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, pmyMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

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

        lblMKey.Text = ""
        txtRefNo.Text = ""
        txtRefDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtAmendNo.Text = CStr(0)
        txtAmendDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtWEF.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkStatus.Enabled = False
        ChkActivate.Enabled = False

        txtDivision.Text = ""
        lblDivision.Text = ""
        txtDivision.Enabled = True

        SprdMain.Enabled = True

        txtAmendNo.Enabled = False
        txtAmendDate.Enabled = False

        txtRemarks.Text = ""

        txtDept.Text = ""
        txtCost.Text = ""
        lblCostctr.Text = ""
        lblDeptname.Text = ""
        txtDept.Enabled = True
        txtCost.Enabled = True

        cmdSearchDept.Enabled = True
        cmdSearchCC.Enabled = True

        mAmendStatus = False
        cmdAmend.Enabled = IIf(InStr(1, XRIGHT, "M") > 0, True, False) '' True

        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)

        MainClass.ButtonStatus(Me, XRIGHT, RsBudgetMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        With SprdMain
            .set_RowHeight(-1, ConRowHeight * 1)
            .Row = Arow


            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsBudgetDetail.Fields("ITEM_CODE").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 10)

            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = MainClass.SetMaxLength("Item_Short_Desc", "INV_ITEM_MST", PubDBCn)
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 40)

            .Col = ColItemUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ALPHANUMERIC
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = RsBudgetDetail.Fields("ITEM_UOM").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(.Col, 6)

            .Col = ColQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsBudgetDetail.Fields("ITEM_QTY").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColQty, 12)

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsBudgetDetail.Fields("REMARKS").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(.Col, 15)

            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemName, ColItemUOM)
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
            .set_RowHeight(0, 300)
            .set_ColWidth(0, 500)
            .set_ColWidth(1, 1400)
            .set_ColWidth(2, 1200)
            .set_ColWidth(3, 1000)
            .set_ColWidth(4, 800)
            .set_ColWidth(5, 1000)
            .set_ColWidth(6, 1000)
            .set_ColWidth(7, 3500)
            .set_ColWidth(8, 800)
            .set_ColWidth(9, 2000)
            .set_ColWidth(10, 2000)
            .set_ColWidth(11, 2000)
            .set_ColWidth(12, 1200)
            .ColsFrozen = 2

            .Col = 1
            .ColHidden = True

            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1

        txtRefNo.Maxlength = RsBudgetMain.Fields("AUTO_KEY_REF").Precision
        txtRefDate.Maxlength = RsBudgetMain.Fields("REF_DATE").DefinedSize - 6
        txtRemarks.Maxlength = RsBudgetMain.Fields("REMARKS").DefinedSize

        txtAmendNo.Maxlength = RsBudgetMain.Fields("AMEND_NO").Precision
        txtAmendDate.Maxlength = RsBudgetMain.Fields("AMEND_DATE").DefinedSize - 6
        txtWEF.Maxlength = RsBudgetMain.Fields("AMEND_WEF_DATE").DefinedSize - 6
        txtDivision.Maxlength = RsBudgetMain.Fields("DIV_CODE").DefinedSize

        txtDept.Maxlength = RsBudgetMain.Fields("DEPT_CODE").DefinedSize
        txtCost.Maxlength = RsBudgetMain.Fields("COST_CENTER_CODE").DefinedSize

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub

    Private Function FieldsVarification() As Boolean

        On Error GoTo err_Renamed
        Dim mItemCode As String
        Dim mQty As Double
        Dim mPOWEFCheck As String
        Dim mPOWEF As String
        Dim mCheckPOWEF As Boolean

        Dim pPervRate As Double
        Dim pCurrRate As Double
        Dim mPrice As Double
        Dim mDisc As Double

        Dim I As Integer
        Dim mIsApproved As String
        Dim pRefNo As Double
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        FieldsVarification = True

        If MainClass.GetUserCanModify((txtAmendDate.Text)) = False Then
            MsgBox("You Have Not Rights to change back P.O.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        If ValidateBranchLocking((txtAmendDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If

        If ValidateBookLocking(PubDBCn, CInt(ConLockPO), txtAmendDate.Text) = True Then
            FieldsVarification = False
            Exit Function
        End If

        If MODIFYMode = True And chkStatus.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Posted PO Cann't be Modified")
            FieldsVarification = False
            Exit Function
        End If

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsBudgetMain.EOF = True Then Exit Function

        If MODIFYMode = True And Trim(txtRefNo.Text) = "" Then
            MsgInformation("PO No. is Blank")
            FieldsVarification = False
            Exit Function
        End If


        If Trim(txtRefDate.Text) = "" Then
            MsgInformation("Ref Date is empty. Cannot Save")
            txtRefDate.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtRefDate.Text) <> "" Then
            If IsDate(txtRefDate.Text) = False Then
                MsgInformation(" Invalid Ref Date. Cannot Save")
                If txtRefDate.Enabled = True Then txtRefDate.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If Trim(txtAmendDate.Text) <> "" Then
            If IsDate(txtAmendDate.Text) = False Then
                MsgInformation(" Invalid Ref Amend Date. Cannot Save")
                If txtAmendDate.Enabled = True Then txtAmendDate.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If Trim(txtWEF.Text) <> "" Then
            If IsDate(txtWEF.Text) = False Then
                MsgInformation(" Invalid WEF Date. Cannot Save")
                If txtWEF.Enabled Then txtWEF.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If CDate(txtRefDate.Text) > CDate(txtAmendDate.Text) Then
            MsgInformation(" Amend Date Cann't be less than Ref Date. Cannot Save")
            FieldsVarification = False
            Exit Function
        End If
        '    If CVDate(txtAmendDate.Text) > CVDate(txtWEF.Text) Then
        '        MsgInformation " WEF Date Cann't be less than Amend Date. Cannot Save"
        '        FieldsVarification = False
        '        Exit Function
        '    End If

        If Trim(txtDivision.Text) = "" Then
            MsgInformation("Division is Blank. Cannot Save")
            If txtDivision.Enabled = True Then txtDivision.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable((lblDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invalid txtDivision Name. Cannot Save")
            If txtDivision.Enabled = True Then txtDivision.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtDept.Text) = "" Then
            MsgBox("Dept Code is Blank. Cann't Save", MsgBoxStyle.Information)
            FieldsVarification = False
            txtDept.Focus()
            Exit Function
        Else
            If MainClass.ValidateWithMasterTable((txtDept.Text), "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgBox("Invalid Dept Code. Cann't Save", MsgBoxStyle.Information)
                FieldsVarification = False
                txtDept.Focus()
                Exit Function
            End If
        End If

        If Trim(txtCost.Text) = "" Then
            MsgBox("Cost Center Code is Blank. Cann't Save", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtCost.Enabled Then txtCost.Focus()
            Exit Function
        Else
            SqlStr = " SELECT IH.CC_CODE, IH.CC_DESC, ID.DEPT_CODE " & vbCrLf & " FROM FIN_CCENTER_HDR IH, FIN_CCENTER_DET ID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=ID.COMPANY_CODE AND IH.CC_CODE=ID.CC_CODE" & vbCrLf & " AND IH.CC_CODE='" & MainClass.AllowSingleQuote((txtCost.Text)) & "'" & vbCrLf & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote((txtDept.Text)) & "'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = True Then
                MsgInformation("Invalid Cost Center Code for Department : " & Trim(txtDept.Text))
                FieldsVarification = False
                If txtCost.Enabled Then txtCost.Focus()
                Exit Function
            End If
        End If

        If CheckPreviousBudgetExists((txtDept.Text), Val(txtDivision.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "Please Check Item Code.") = False Then FieldsVarification = False
        If MainClass.ValidDataInGrid(SprdMain, ColItemName, "S", "Please Check Item Description.") = False Then FieldsVarification = False
        If MainClass.ValidDataInGrid(SprdMain, ColItemUOM, "S", "Please Check Unit.") = False Then FieldsVarification = False


        For I = 1 To SprdMain.MaxRows - 1
            SprdMain.Row = I
            SprdMain.Col = ColItemCode
            mItemCode = Trim(UCase(SprdMain.Text))
            If ADDMode = True Then
                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS='I'") = True Then
                    MsgInformation("Item Status is Closed, So cann't be Saved. [" & mItemCode & "]")
                    MainClass.SetFocusToCell(SprdMain, I, ColItemCode)
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        Next

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        'Resume
    End Function
    Private Sub frmMonthlyConBudgetMst_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        Me.Close()
        RsBudgetMain.Close()
        'RsOpOuts.Close
    End Sub


    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Function CheckDuplicateItem(ByRef mItemCode As String) As Boolean

        On Error GoTo ERR1
        Dim I As Integer
        Dim mItemRept As Integer

        If mItemCode = "" Then CheckDuplicateItem = True : Exit Function
        With SprdMain
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColItemCode
                If UCase(Trim(.Text)) = UCase(Trim(mItemCode)) Then
                    mItemRept = mItemRept + 1
                    If mItemRept > 1 Then
                        CheckDuplicateItem = True
                        MsgInformation("Duplicate Item Code")
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColItemCode)
                        Exit Function
                    End If
                End If
            Next
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        With SprdMain
            '        SprdMain_LeaveCell .ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False
        End With
    End Sub
    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim SqlStr As String = ""



        If eventArgs.Row = 0 And eventArgs.Col = ColItemCode And SprdMain.Enabled = True Then
            With SprdMain
                eventArgs.Row = .ActiveRow
                eventArgs.Col = ColItemCode
                SqlStr = GetSearchItem("Y")
                ''If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "", "ITEM_CODE") = True Then
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    eventArgs.Row = .ActiveRow
                    eventArgs.Col = ColItemCode
                    .Text = Trim(AcName)
                    eventArgs.Col = ColItemName
                    .Text = Trim(AcName1)
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            End With
        End If

        If eventArgs.Row = 0 And eventArgs.Col = ColItemName And SprdMain.Enabled = True Then
            With SprdMain
                eventArgs.Row = .ActiveRow
                eventArgs.Col = ColItemName
                SqlStr = GetSearchItem("N")
                ''If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "", "ITEM_CODE") = True Then
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    eventArgs.Row = .ActiveRow
                    eventArgs.Col = ColItemName
                    .Text = Trim(AcName)
                    eventArgs.Col = ColItemCode
                    .Text = Trim(AcName1)
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            End With
        End If

        '    If mAmendStatus = True Or (txtAmendNo.Text) > 0 Then
        '        Exit Sub
        '    End If

        If eventArgs.Col = 0 And eventArgs.Row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdMain, eventArgs.Row, ColItemName)
            MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        End If
    End Sub
    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemName Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemName, 0))

        If eventArgs.KeyCode = System.Windows.Forms.Keys.F2 And mCol = ColItemName Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F2 And mCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemName, 0))

        SprdMain.Refresh()
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim xICode As String
        Dim xIDesc As String

        If eventArgs.NewRow = -1 Then Exit Sub

        If Val(txtDivision.Text) = 0 Then
            MsgInformation("Please Select Division First.")
            If txtDivision.Enabled = True Then txtDivision.Focus()
            Exit Sub
        End If

        Select Case eventArgs.Col
            Case ColItemCode
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColItemCode
                xICode = SprdMain.Text
                If xICode = "" Then Exit Sub

                If GetValidItem(xICode) = True Then
                    If CheckDuplicateItem(xICode) = False Then
                        If FillGridRow(xICode) = False Then Exit Sub
                        '                    FormatSprdMain Row
                        '                MainClass.SetFocusToCell SprdMain, Row, ColItemRate
                    End If
                Else
                    MainClass.SetFocusToCell(SprdMain, eventArgs.Row, ColItemCode)
                End If

            Case ColQty
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColItemCode
                xICode = SprdMain.Text
                If xICode = "" Then Exit Sub
                SprdMain.Col = ColItemName
                xIDesc = SprdMain.Text

                If CheckItemQty() = True Then
                    If SprdMain.MaxRows = SprdMain.ActiveRow Then
                        MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                        '                        FormatSprdMain SprdMain.MaxRows
                        FormatSprdMain(-1)
                    End If
                End If
        End Select

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function CheckItemQty() As Boolean
        On Error GoTo ERR1

        CheckItemQty = False
        With SprdMain
            .Row = .ActiveRow
            .Col = ColItemCode
            If Trim(.Text) = "" Then Exit Function

            .Col = ColQty
            If Val(.Text) > 0 Then
                CheckItemQty = True
            Else
                '            MsgInformation "Please Check the Qty."
                '            MainClass.SetFocusToCell SprdMain, .ActiveRow, ColQty
            End If
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function FillGridRow(ByRef mItemCode As String) As Boolean

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        If mItemCode = "" Then Exit Function

        SqlStr = ""

        SqlStr = " Select ITEM_CODE,ITEM_SHORT_DESC,PURCHASE_UOM " & vbCrLf & " FROM INV_ITEM_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ITEM_CODE='" & Trim(mItemCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMisc.EOF = False Then
            SprdMain.Row = SprdMain.ActiveRow
            With RsMisc

                SprdMain.Col = ColItemName
                SprdMain.Text = IIf(IsDbNull(.Fields("ITEM_SHORT_DESC").Value), "", .Fields("ITEM_SHORT_DESC").Value)

                SprdMain.Col = ColItemUOM
                SprdMain.Text = IIf(IsDbNull(.Fields("PURCHASE_UOM").Value), "", .Fields("PURCHASE_UOM").Value)

            End With
            FillGridRow = True
        Else
            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            FillGridRow = False
        End If

        Exit Function
ERR1:
        '    Resume
        FillGridRow = False
        MsgBox(Err.Description)
    End Function
    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Row = SprdView.ActiveRow

        SprdView.Col = 2
        txtRefNo.Text = SprdView.Text

        SprdView.Col = 4
        txtAmendNo.Text = SprdView.Text

        txtRefNo_Validating(txtRefNo, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub
    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdMain.ActiveCol, SprdMain.ActiveRow))
    End Sub

    Private Sub txtAmendDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAmendDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAmendDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAmendDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtAmendDate.Text) = "" Then GoTo EventExitSub

        If IsDate(txtAmendDate.Text) = False Then
            MsgInformation(" Invalid Date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtAmendNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAmendNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAmendNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAmendNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCost_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCost.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCost_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCost.DoubleClick
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        If Trim(txtDept.Text) = "" Then
            MsgInformation("Please Select Dept. First.")
            txtDept.Focus()
            Exit Sub
        End If
        SqlStr = " SELECT IH.CC_CODE, IH.CC_DESC, ID.DEPT_CODE " & vbCrLf & " FROM FIN_CCENTER_HDR IH, FIN_CCENTER_DET ID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=ID.COMPANY_CODE AND IH.CC_CODE=ID.CC_CODE" & vbCrLf & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote((txtDept.Text)) & "'"

        '    If MainClass.SearchGridMaster(txtCost.Text, "FIN_CCENTER_HDR", "CC_CODE", "CC_DESC", , , SqlStr) = True Then
        If MainClass.SearchGridMasterBySQL2((txtCost.Text), SqlStr) = True Then
            txtCost.Text = AcName
            lblCostctr.Text = AcName1
            txtCost_Validating(txtCost, New System.ComponentModel.CancelEventArgs(False))
            If txtCost.Enabled = True Then txtCost.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtCost_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCost.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCost.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCost_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCost.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtCost_DoubleClick(txtCost, New System.EventArgs())
    End Sub

    Private Sub txtCost_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCost.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        If Trim(txtCost.Text) = "" Then GoTo EventExitSub
        txtCost.Text = VB6.Format(txtCost.Text, "000")
        If Trim(txtDept.Text) = "" Then
            MsgInformation("Please Select Dept. First.")
            txtDept.Focus()
            GoTo EventExitSub
        End If

        SqlStr = " SELECT IH.CC_CODE, IH.CC_DESC, ID.DEPT_CODE " & vbCrLf & " FROM FIN_CCENTER_HDR IH, FIN_CCENTER_DET ID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=ID.COMPANY_CODE AND IH.CC_CODE=ID.CC_CODE" & vbCrLf & " AND IH.CC_CODE='" & MainClass.AllowSingleQuote((txtCost.Text)) & "'" & vbCrLf & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote((txtDept.Text)) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            lblCostctr.Text = IIf(IsDbNull(RsTemp.Fields("CC_DESC").Value), "", RsTemp.Fields("CC_DESC").Value)
        Else
            MsgInformation("Invalid Cost Center Code for Department : " & Trim(txtDept.Text))
            Cancel = True
        End If

        '    If MainClass.ValidateWithMasterTable(txtCost.Text, "CC_CODE", "CC_DESC", "FIN_CCENTER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
        '        lblCostctr.text = MasterNo
        '    Else
        '        MsgInformation "Invalid CostC Code"
        '        Cancel = True
        '    End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtDept_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.DoubleClick
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , SqlStr) = True Then
            txtDept.Text = AcName1
            lblDeptname.Text = AcName
            '            txtDept_Validate False
            If txtDept.Enabled = True Then txtDept.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtDept_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDept.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDept.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDept_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDept.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then TxtDept_DoubleClick(TxtDept, New System.EventArgs())
    End Sub

    Private Sub txtDept_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDept.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtDept.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtDept.Text), "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            lblDeptname.Text = MasterNo
        Else
            MsgInformation("Invalid Depatment Code")
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDivision.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtDivision_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDivision.DoubleClick
        cmdDivSearch_Click(cmdDivSearch, New System.EventArgs())
    End Sub
    Private Sub txtDivision_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDivision.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDivision.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtDivision_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDivision.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdDivSearch_Click(cmdDivSearch, New System.EventArgs())
    End Sub
    Private Sub txtDivision_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDivision.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtDivision.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtDivision.Text), "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            lblDivision.Text = MasterNo
        Else
            MsgBox("Invalid Division Code.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cmdDivSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDivSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtDivision.Text), "INV_DIVISION_MST", "DIV_CODE", "DIV_DESC", , , SqlStr) = True Then
            txtDivision.Text = AcName
            txtDivision_Validating(txtDivision, New System.ComponentModel.CancelEventArgs(False))
            txtDivision.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtRefDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRefDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtRefDate.Text) = "" Then GoTo EventExitSub

        If IsDate(txtRefDate.Text) = False Then
            MsgInformation(" Invalid Date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtRefNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRefNo.DoubleClick
        cmdSearchRef_Click(cmdSearchRef, New System.EventArgs())
    End Sub

    Private Sub txtRefNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtRefNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchRef_Click(cmdSearchRef, New System.EventArgs())
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim mAccountName As String
        Dim mAddMode As Boolean

        Clear1()

        If Not RsBudgetMain.EOF Then

            lblMKey.Text = IIf(IsDbNull(RsBudgetMain.Fields("MKEY").Value), "", RsBudgetMain.Fields("MKEY").Value)
            txtRefNo.Text = IIf(IsDbNull(RsBudgetMain.Fields("AUTO_KEY_REF").Value), "", RsBudgetMain.Fields("AUTO_KEY_REF").Value)
            txtRefDate.Text = VB6.Format(IIf(IsDbNull(RsBudgetMain.Fields("REF_DATE").Value), "", RsBudgetMain.Fields("REF_DATE").Value), "DD/MM/YYYY")

            txtWEF.Text = VB6.Format(IIf(IsDbNull(RsBudgetMain.Fields("AMEND_WEF_DATE").Value), "", RsBudgetMain.Fields("AMEND_WEF_DATE").Value), "DD/MM/YYYY")

            txtDivision.Text = IIf(IsDbNull(RsBudgetMain.Fields("DIV_CODE").Value), "", RsBudgetMain.Fields("DIV_CODE").Value)

            If MainClass.ValidateWithMasterTable((txtDivision.Text), "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblDivision.Text = MasterNo
            End If

            ChkActivate.CheckState = System.Windows.Forms.CheckState.Unchecked
            txtAmendNo.Text = IIf(IsDbNull(RsBudgetMain.Fields("AMEND_NO").Value), 0, RsBudgetMain.Fields("AMEND_NO").Value)
            txtAmendDate.Text = VB6.Format(IIf(IsDbNull(RsBudgetMain.Fields("AMEND_DATE").Value), "", RsBudgetMain.Fields("AMEND_DATE").Value), "DD/MM/YYYY")
            chkStatus.CheckState = IIf(RsBudgetMain.Fields("BUDGET_STATUS").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            ChkActivate.CheckState = IIf(RsBudgetMain.Fields("BUDGET_CLOSED").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            cmdAmend.Enabled = IIf(RsBudgetMain.Fields("BUDGET_CLOSED").Value = "Y", False, True)

            txtRemarks.Text = IIf(IsDbNull(RsBudgetMain.Fields("REMARKS").Value), "", RsBudgetMain.Fields("REMARKS").Value)

            txtDept.Text = IIf(IsDbNull(RsBudgetMain.Fields("DEPT_CODE").Value), "", RsBudgetMain.Fields("DEPT_CODE").Value)
            txtCost.Text = IIf(IsDbNull(RsBudgetMain.Fields("COST_CENTER_CODE").Value), "", RsBudgetMain.Fields("COST_CENTER_CODE").Value)

            If MainClass.ValidateWithMasterTable((txtDept.Text), "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblDeptname.Text = MasterNo
            Else
                lblDeptname.Text = ""
            End If

            If MainClass.ValidateWithMasterTable((txtCost.Text), "CC_CODE", "CC_DESC", "FIN_CCENTER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblCostctr.Text = MasterNo
            Else
                lblCostctr.Text = ""
            End If

            Call ShowDetail1()

        End If
        FormatSprdMain(-1)

        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = True
        txtRefNo.Enabled = True
        cmdSearchRef.Enabled = True
        cmdSearchAmend.Enabled = True

        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemName, ColItemUOM)
        MainClass.ButtonStatus(Me, XRIGHT, RsBudgetMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Private Sub ShowDetail1()

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mItemCode As String
        Dim mItemDesc As String

        MainClass.ClearGrid(SprdMain, ConRowHeight)
        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM INV_BUDGET_DET " & vbCrLf & " Where " & vbCrLf & " MKEY=" & Val(lblMKey.Text) & "" & vbCrLf & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBudgetDetail, ADODB.LockTypeEnum.adLockReadOnly)

        With RsBudgetDetail
            If .EOF = True Then Exit Sub
            '        FormatSprdMain -1
            I = 1
            '        .MoveFirst

            Do While Not .EOF

                SprdMain.Row = I
                SprdMain.Col = ColItemCode
                mItemCode = Trim(IIf(IsDbNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))
                SprdMain.Text = mItemCode

                SprdMain.Col = ColItemName
                MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mItemDesc = MasterNo
                SprdMain.Text = mItemDesc

                SprdMain.Col = ColItemUOM
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value))

                SprdMain.Col = ColQty
                SprdMain.Text = CStr(Val(IIf(IsDbNull(.Fields("ITEM_QTY").Value), 0, .Fields("ITEM_QTY").Value)))

                SprdMain.Col = ColRemarks
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value))

                .MoveNext()

                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With


        Call FormatSprdMain(-1)

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''   Resume
    End Sub
    Private Sub txtRefDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRefDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtRefNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRefNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtRefNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRefNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Public Sub txtRefNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRefNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMkey As String = ""
        Dim mRefNo As Double
        Dim SqlStr As String = ""

        If Trim(txtRefNo.Text) = "" Then GoTo EventExitSub

        If Len(txtRefNo.Text) < 6 Then
            txtRefNo.Text = Val(txtRefNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        mRefNo = Val(txtRefNo.Text)

        If MODIFYMode = True And RsBudgetMain.BOF = False Then xMkey = RsBudgetMain.Fields("mKey").Value

        SqlStr = "SELECT * FROM INV_BUDGET_HDR " & " WHERE AUTO_KEY_REF='" & MainClass.AllowSingleQuote(UCase(CStr(mRefNo))) & "'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If Trim(txtAmendNo.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND AMEND_NO=" & Val(txtAmendNo.Text) & ""
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBudgetMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsBudgetMain.EOF = False Then
            Clear1()
            Show1()
        Else
            SqlStr = "SELECT * FROM INV_BUDGET_HDR " & " WHERE AUTO_KEY_REF='" & MainClass.AllowSingleQuote(UCase(CStr(mRefNo))) & "'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "


            SqlStr = SqlStr & vbCrLf & " AND AMEND_NO IN (" & vbCrLf & " SELECT MAX(AMEND_NO) FROM INV_BUDGET_HDR " & " WHERE AUTO_KEY_REF='" & MainClass.AllowSingleQuote(UCase(CStr(mRefNo))) & "'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ")"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBudgetMain, ADODB.LockTypeEnum.adLockReadOnly)
            If RsBudgetMain.EOF = False Then
                Clear1()
                Show1()
            Else

                If ADDMode = False And MODIFYMode = False Then
                    MsgBox("No Such PO No. Click, Add for New", MsgBoxStyle.Information)
                    txtAmendNo.Text = CStr(0)
                    Cancel = True
                ElseIf MODIFYMode = True Then
                    SqlStr = "SELECT * FROM INV_BUDGET_HDR WHERE MKEY=" & Val(xMkey) & ""
                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBudgetMain, ADODB.LockTypeEnum.adLockReadOnly)
                End If
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtRemarks_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRemarks.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtRemarks.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub TxtWef_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWEF.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub



    Private Function GetSearchItem(ByRef mByCode As String) As String
        On Error GoTo ErrPart
        Dim mSqlStr As String

        If mByCode = "Y" Then
            mSqlStr = "SELECT A.ITEM_CODE,A.ITEM_SHORT_DESC "
        Else
            mSqlStr = "SELECT A.ITEM_SHORT_DESC,A.ITEM_CODE "
        End If

        mSqlStr = mSqlStr & vbCrLf & " FROM INV_ITEM_MST A" & vbCrLf & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""


        If mByCode = "Y" Then
            mSqlStr = mSqlStr & vbCrLf & "ORDER BY A.ITEM_CODE "
        Else
            mSqlStr = mSqlStr & vbCrLf & "ORDER BY A.ITEM_SHORT_DESC"
        End If

        GetSearchItem = mSqlStr
        Exit Function
ErrPart:
        GetSearchItem = ""

    End Function

    Private Function GetValidItem(ByRef pItemCode As String) As Boolean
        On Error GoTo ErrPart

        If MainClass.ValidateWithMasterTable(pItemCode, "ITEM_CODE", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            GetValidItem = True
        Else
            MsgInformation("Please Check Item.")
            GetValidItem = False
        End If

        Exit Function
ErrPart:
        GetValidItem = False
    End Function
    Private Function SelectQryForPO(ByRef mSqlStr As String) As String

        ''SELECT CLAUSE...
        '
        '    mSqlStr = " SELECT " & vbCrLf _
        ''            & " IH.*, ID.*,TEMP_PO.*,"
        '
        '    mSqlStr = mSqlStr & " CMST.SUPP_CUST_NAME, CMST.SUPP_CUST_ADDR, " & vbCrLf _
        ''            & " CMST.SUPP_CUST_CITY, CMST.SUPP_CUST_STATE, " & vbCrLf _
        ''            & " CMST.SUPP_CUST_PIN, CMST.SUPP_CUST_PHONE, " & vbCrLf _
        ''            & " CMST.SUPP_CUST_FAXNO, CMST.SUPP_CUST_MAILID, " & vbCrLf _
        ''            & " CMST.SUPP_CUST_MOBILE, CMST.CST_NO, " & vbCrLf _
        ''            & " CMST.LST_NO, CMST.PAN_NO, " & vbCrLf _
        ''            & " CMST.EXCISE_DIV, CMST.EXCISE_RANGE, " & vbCrLf _
        ''            & " CMST.CENT_EXC_RGN_NO, CMST.ECC_NO, " & vbCrLf _
        ''            & " CMST.SUPP_CUST_REMARKS, CMST.WITHIN_STATE, " & vbCrLf _
        ''            & " CMST.WITHIN_DISTT, CMST.COMMISIONER_RATE, " & vbCrLf _
        ''            & " CMST.REGD_DEALER, CMST.DATE_OF_APPROVAL, WITHIN_STATE"
        '
        '    ''FROM CLAUSE...
        '    mSqlStr = mSqlStr & vbCrLf & " FROM INV_BUDGET_HDR IH, INV_BUDGET_DET ID, " & vbCrLf _
        ''            & " FIN_SUPP_CUST_MST CMST, FIN_PAYTERM_MST PAYMST, Temp_PO_PRN TEMP_PO"
        '
        '    ''WHERE CLAUSE...
        '    mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf _
        ''            & " IH.MKEY=ID.MKEY" & vbCrLf _
        ''            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
        ''            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
        ''            & " AND IH.COMPANY_CODE=PAYMST.COMPANY_CODE(+)" & vbCrLf _
        ''            & " AND IH.PAYMENT_CODE=PAYMST.PAY_TERM_CODE(+)" & vbCrLf _
        ''            & " AND ID.ITEM_CODE=TEMP_PO.ITEM_CODE" & vbCrLf _
        ''            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''            & " AND IH.AUTO_KEY_REF=" & Val(txtRefNo.Text) & "" & vbCrLf _
        ''            & " AND IH.AMEND_NO=" & Val(txtAmendNo.Text) & "" & vbCrLf _
        ''            & " AND TEMP_PO.USERID='" & MainClass.AllowSingleQuote(PubUserID) & "' AND TEMP_PO.PRINT_STATUS='Y'"
        '
        'ORDER CLAUSE...
        '
        '    If pItemCodeWisePrint = True Then
        '        mSqlStr = mSqlStr & vbCrLf & "ORDER BY ID.SERIAL_NO"
        '    Else
        '        mSqlStr = mSqlStr & vbCrLf & "ORDER BY TEMP_PO.ITEM_SHORT_DESC"
        '    End If

        SelectQryForPO = mSqlStr
    End Function
    Private Sub TxtWef_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtWEF.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtWEF.Text) = "" Then GoTo EventExitSub

        If IsDate(txtWEF.Text) = False Then
            MsgInformation(" Invalid Date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Function GetMaxAmendNo(ByRef pRefNo As Double) As Integer

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = " SELECT MAX(AMEND_NO) AS AMEND_NO" & vbCrLf & " FROM INV_BUDGET_HDR" & vbCrLf & " WHERE AUTO_KEY_REF=" & Val(CStr(pRefNo)) & "" '& vbCrLf |        & " AND BUDGET_STATUS='Y' " & vbCrLf |        & " AND BUDGET_CLOSED='N' "

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

    Private Function CheckUnPostedRef(ByRef pRefNo As Double) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing


        CheckUnPostedRef = False

        SqlStr = " SELECT Count(1) AS CNTPO" & vbCrLf & " FROM INV_BUDGET_HDR" & vbCrLf & " WHERE AUTO_KEY_REF=" & Val(CStr(pRefNo)) & "" & vbCrLf & " AND BUDGET_STATUS='N' " '& vbCrLf |        & " AND BUDGET_CLOSED='N' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If IsDbNull(RsTemp.Fields("CNTPO").Value) Or RsTemp.Fields("CNTPO").Value < 1 Then
                CheckUnPostedRef = False
            Else
                MsgInformation("There are " & RsTemp.Fields("CNTPO").Value & " UnPosted Budget. So Please Post UnPosted Budget - " & pRefNo)
                CheckUnPostedRef = True
            End If
        Else
            CheckUnPostedRef = False
        End If

        Exit Function
ErrPart:
        CheckUnPostedRef = True
    End Function


    Private Function CheckPreviousBudgetExists(ByRef pDeptCode As String, ByRef pDivCode As Double) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xRefNo As Double
        Dim CntRow As Integer
        Dim pItemCode As String

        CheckPreviousBudgetExists = False

        If Trim(txtRefNo.Text) = "" Then
            xRefNo = -1
        Else
            xRefNo = Val(txtRefNo.Text)
        End If

        SqlStr = "SELECT DISTINCT AUTO_KEY_REF " & vbCrLf & " FROM INV_BUDGET_HDR IH " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.DEPT_CODE='" & MainClass.AllowSingleQuote(pDeptCode) & "'" & vbCrLf & " AND IH.DIV_CODE=" & Val(CStr(pDivCode)) & "" & vbCrLf & " AND BUDGET_CLOSED='N'"

        SqlStr = SqlStr & vbCrLf & " AND AUTO_KEY_REF<>" & Val(CStr(xRefNo)) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            MsgInformation("Budget Already made for Such Department & Division. : " & RsTemp.Fields("AUTO_KEY_REF").Value)
            CheckPreviousBudgetExists = True
            Exit Function
        End If

        Exit Function
ErrPart:
        CheckPreviousBudgetExists = True
    End Function
End Class
