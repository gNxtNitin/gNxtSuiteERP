Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmProductionPlanDailyAmend
    Inherits System.Windows.Forms.Form
    Dim RsProdPlanAmend As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection	

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

    Private Const ConRowHeight As Short = 14

    Dim mShowDate As String
    Dim pDeptCode As String
    Dim pProductCode As String

    Private Sub Clear1()

        lblMKey.Text = ""
        txtCode.Text = ""
        lblDescription.Text = ""
        txtPlanDate.Text = ""
        txtDept.Text = ""
        txtDeptName.Text = ""

        cboReason.SelectedIndex = -1
        txtPreviousPlan.Text = ""
        txtAmendQty.Text = ""
        txtNetPlan.Text = ""
        txtRemarks.Text = ""



        Call MakeEnableDesableField(True)

        PrintStatus((False))
        MainClass.ButtonStatus(Me, XRIGHT, RsProdPlanAmend, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub cboReason_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboReason.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click

        On Error GoTo AddErr
        If cmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
        Else
            ADDMode = False
            MODIFYMode = False
            Clear1()
            If RsProdPlanAmend.EOF = False Then
                RsProdPlanAmend.MoveFirst()
            Else
                Exit Sub
            End If

            lblMKey.Text = IIf(IsDbNull(RsProdPlanAmend.Fields("AUTO_KEY_AMEND").Value), "", RsProdPlanAmend.Fields("AUTO_KEY_AMEND").Value)
            Call Show1()
            MakeEnableDesableField((False))
            ADDMode = False
            MODIFYMode = False
            MainClass.ButtonStatus(Me, XRIGHT, RsProdPlanAmend, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            '        CmdSave.Enabled = True	

        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        On Error Resume Next
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        Dim mItemCode As String

        If ValidateBranchLocking((txtPlanDate.Text)) = True Then
            Exit Sub
        End If
        If Trim(txtPlanDate.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub

        If Not RsProdPlanAmend.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "PRD_PROD_PLAN_AMEND_TRN ", (lblMKey.Text), RsProdPlanAmend, "AUTO_KEY_AMEND") = False Then GoTo DelErrPart

                PubDBCn.Execute("DELETE FROM PRD_PROD_PLAN_AMEND_TRN  WHERE AUTO_KEY_AMEND='" & Trim(lblMKey.Text) & "'") '' AND BOOKTYPE='" & vb.Left(lblBookType.text, 1) & "'"	
                PubDBCn.CommitTrans()
                RsProdPlanAmend.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        RsProdPlanAmend.Requery()
    End Sub

    Private Sub cmdDeptSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDeptSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster(txtDept.Text, "PAY_DEPT_MST", "DEPT_CODE", "DEPT_DESC", , , SqlStr) = True Then
            txtDept.Text = AcName
            txtDeptName.Text = AcName1
            '            txtDept_Validate False
            If txtDept.Enabled = True Then txtDept.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr
        Dim mDate As String
        Dim mCheckDate As String
        Dim mDays As Integer



        If CDate(txtPlanDate.Text) < CDate(PubCurrDate) Then
            MsgInformation("You have no rights to change Plan.")
            Exit Sub
        End If

        If cmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsProdPlanAmend, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
        'Resume	
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification = False Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Update1 = True Then
            MakeEnableDesableField((False))
            ADDMode = False
            MODIFYMode = False
            If CheckValidate() = False Then GoTo ErrorHandler
            '        Show1	
            PrintStatus((True))
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
        Dim mReqnum As String
        Dim SqlStr As String = ""
        Dim mReason As String
        Dim mMKEY As String
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()




        mReason = VB.Left(cboReason.Text, 1)
        mMKEY = VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00") & Trim(txtCode.Text) & Trim(txtDept.Text) & VB6.Format(txtPlanDate.Text, "YYYYMMDD")

        SqlStr = ""
        If ADDMode = True Then
            lblMKey.Text = mMKEY
            SqlStr = "INSERT INTO PRD_PROD_PLAN_AMEND_TRN (" & vbCrLf _
                & " AUTO_KEY_AMEND, COMPANY_CODE, " & vbCrLf _
                & " PRODUCT_CODE, DEPT_CODE, " & vbCrLf _
                & " SERIAL_DATE, AMEND_QTY, REASON, " & vbCrLf _
                & " REMARKS, " & vbCrLf _
                & " ADDUSER, ADDDATE, MODUSER, MODDATE)" & vbCrLf _
                & " VALUES( " & vbCrLf _
                & " '" & Trim(mMKEY) & "'," & vbCrLf _
                & " " & RsCompany.Fields("Company_Code").Value & "," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtCode.Text) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtDept.Text) & "', " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtPlanDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & Val(txtAmendQty.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(mReason) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"




        ElseIf MODIFYMode = True Then
            SqlStr = ""
            SqlStr = "UPDATE PRD_PROD_PLAN_AMEND_TRN SET " & vbCrLf _
                & " AMEND_QTY=" & Val(txtAmendQty.Text) & "," & vbCrLf _
                & " REASON='" & MainClass.AllowSingleQuote(mReason) & "', " & vbCrLf _
                & " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
                & " AND AUTO_KEY_AMEND ='" & MainClass.AllowSingleQuote(lblMkey.Text) & "'"
        End If

        PubDBCn.Execute(SqlStr)

        Update1 = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans() ''	

        RsProdPlanAmend.Requery() ''.Refresh	
        If Err.Description = "" Then Exit Function
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume	
    End Function
    Private Sub cmdSearchCode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchCode.Click
        Dim SqlStr As String = ""
        SqlStr = " SELECT A.ITEM_CODE, B.ITEM_SHORT_DESC " & vbCrLf & " FROM FIN_SUPP_CUST_DET A, INV_ITEM_MST B " & vbCrLf & " WHERE B.COMPANY_CODE =A.COMPANY_CODE " & vbCrLf & " AND B.ITEM_CODE = A.ITEM_CODE " & vbCrLf & " AND A.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " ORDER BY B.ITEM_SHORT_DESC "
        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtCode.Text = AcName
            lblDescription.text = AcName1
            If txtCode.Enabled = True Then txtCode.Focus()
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
        MainClass.ButtonStatus(Me, XRIGHT, RsProdPlanAmend, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmProductionPlanDailyAmend_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Daily Production Plan (Amendment)"

        SqlStr = "Select * From PRD_PROD_PLAN_AMEND_TRN  WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProdPlanAmend, ADODB.LockTypeEnum.adLockReadOnly)

        Call SetTextLengths()

        If cmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        Resume
    End Sub

    Private Sub frmProductionPlanDailyAmend_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmProductionPlanDailyAmend_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        Me.Left = 0
        Me.Top = 0
        Me.Height = VB6.TwipsToPixelsY(4455)
        Me.Width = VB6.TwipsToPixelsX(9000)

        cboReason.Items.Clear()

        cboReason.Items.Add("A: Material Short")
        cboReason.Items.Add("B: Material Rejected")
        cboReason.Items.Add("C: Manpower Short")
        cboReason.Items.Add("D: M/c Break Down")
        cboReason.Items.Add("E: Electric Break Down")
        cboReason.Items.Add("F: Customer Amendment")
        cboReason.SelectedIndex = -1
        FormActive = False
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        txtCode.Maxlength = RsProdPlanAmend.Fields("PRODUCT_CODE").DefinedSize
        txtPlanDate.Maxlength = RsProdPlanAmend.Fields("SERIAL_DATE").Precision - 6

        txtDept.Maxlength = RsProdPlanAmend.Fields("DEPT_CODE").Precision - 6
        txtAmendQty.Maxlength = RsProdPlanAmend.Fields("AMEND_QTY").Precision
        txtRemarks.Maxlength = RsProdPlanAmend.Fields("REMARKS").DefinedSize


        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume	
    End Sub

    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim ii As Integer
        Dim mDeptCode As String
        Dim mMonthPlan As Double
        Dim mProductionQty As Double
        Dim mDayPlan As Double
        Dim mProductCode As String
        Dim mInhouseCode As String
        Dim mCheckDate As String
        Dim mDays As Integer

        FieldsVarification = True

        If ValidateDeptRight(PubUserID, Trim(txtDept.Text), UCase(Trim(txtDeptName.Text))) = False Then
            FieldsVarification = False
            Exit Function
        End If

        If txtDept.Text = "" Then
            MsgBox("Department code is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtDept.Focus()
            Exit Function
        End If

        If Trim(txtCode.Text) = "" Then
            MsgInformation("Product Code is empty, So unable to save.")
            txtCode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtPlanDate.Text) = "" Then
            MsgInformation("Plan Date is empty, So unable to save.")
            txtPlanDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        'If Val(txtAmendQty.Text) = 0 Then
        '    MsgInformation("Amend Qty is empty, So unable to save.")
        '    txtAmendQty.Focus()
        '    FieldsVarification = False
        '    Exit Function
        'End If

        If cboReason.SelectedIndex = -1 Then
            MsgInformation("Please select the Reason.")
            cboReason.Focus()
            FieldsVarification = False
            Exit Function
        End If


        'If DateDiff(Microsoft.VisualBasic.DateInterval.Day, CDate(PubCurrDate), CDate(txtPlanDate.Text)) > 2 Then 'CDate(txtPlanDate.Text) > CDate(PubCurrDate) Then	
        '    MsgInformation("You have no rights to change Plan of 2 days before.")
        '    FieldsVarification = False
        '    Exit Function
        'End If

        If CDate(txtPlanDate.Text) < CDate(PubCurrDate) Then ' Then	
            MsgInformation("You have no rights to change Plan of Current / Previous Date.")
            FieldsVarification = False
            Exit Function
        End If

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        'Resume	
    End Function

    Private Function GetPlanQty() As Double

        On Error GoTo err_Renamed
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        GetPlanQty = 0
        SqlStr = "SELECT SUM(DPLAN_QTY) AS DPLAN_QTY" & vbCrLf _
            & " FROM PRD_PRODPLAN_MONTH_DET ID" & vbCrLf _
            & " WHERE ID.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND" & vbCrLf _
            & " ID.INHOUSE_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'" & vbCrLf _
            & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "'" & vbCrLf _
            & " AND ID.SERIAL_DATE=TO_DATE('" & VB6.Format(txtPlanDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetPlanQty = IIf(IsDbNull(RsTemp.Fields("DPLAN_QTY").Value), 0, RsTemp.Fields("DPLAN_QTY").Value)
        End If

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        'Resume	
    End Function

    Private Sub frmProductionPlanDailyAmend_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        RsProdPlanAmend.Close()
        RsProdPlanAmend = Nothing

        Me.Hide()
        Me.Close()
    End Sub
    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick

        On Error GoTo ErrPart
        Dim xMkey As String = ""
        Dim SqlStr As String = ""


        Clear1()

        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        xMkey = SprdView.Text

        SprdView.Col = 2
        SprdView.Row = SprdView.ActiveRow
        pProductCode = Trim(SprdView.Text)

        SprdView.Col = 3
        SprdView.Row = SprdView.ActiveRow
        pDeptCode = Trim(SprdView.Text)

        SprdView.Col = 4
        SprdView.Row = SprdView.ActiveRow
        txtPlanDate.Text = VB6.Format(SprdView.Text, "DD/MM/YYYY")

        SqlStr = "SELECT *  " & vbCrLf & " FROM PRD_PROD_PLAN_AMEND_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTO_KEY_AMEND='" & xMkey & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProdPlanAmend, ADODB.LockTypeEnum.adLockReadOnly)
        If RsProdPlanAmend.EOF = False Then
            lblMKey.Text = IIf(IsDbNull(RsProdPlanAmend.Fields("AUTO_KEY_AMEND").Value), "", RsProdPlanAmend.Fields("AUTO_KEY_AMEND").Value)
            Call Show1()
        Else
            MsgBox("Amendment Plan not made for these parameters.", MsgBoxStyle.Information)
            '        ShowRecord = False	
        End If
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsProdPlanAmend, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Call CmdView_Click(CmdView, New System.EventArgs())
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtAmendQty_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAmendQty.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAmendQty_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAmendQty.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtAmendQty_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAmendQty.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CDbl(VB6.Format(Val(txtAmendQty.Text) + Val(txtPreviousPlan.Text), "0.00")) < 0 Then
            MsgInformation("Invalid Amendment Qty. Net Qty cann't be Less Than 0.")
            Cancel = True
            GoTo EventExitSub
        End If
        txtNetPlan.Text = VB6.Format(Val(txtAmendQty.Text) + Val(txtPreviousPlan.Text), "0.00")
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If txtCode.Text = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtCode.Text, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
            lblDescription.text = MasterNo
        Else
            MsgInformation("Invalid Product Code")
            Cancel = True
            Exit Sub
        End If

        If CheckValidate() = False Then Cancel = True

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDept_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.DoubleClick
        Call cmdDeptSearch_Click(cmdDeptSearch, New System.EventArgs())
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtDept_DoubleClick(txtDept, New System.EventArgs())
    End Sub

    Private Sub txtDept_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDept.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        If Trim(txtDept.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable(txtDept.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
            txtDeptName.Text = MasterNo
        Else
            MsgInformation("Invalid Depatment Code")
            Cancel = True
            Exit Sub
        End If
        If CheckValidate() = False Then Cancel = True

        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtNetPlan_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNetPlan.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPlanDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPlanDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPlanDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPlanDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If txtPlanDate.Text = "" Then GoTo EventExitSub
        If Len(txtPlanDate.Text) = 8 Then
            txtPlanDate.Text = VB.Left(txtPlanDate.Text, 2) & "/" & Mid(txtPlanDate.Text, 3, 2) & "/" & Mid(txtPlanDate.Text, 5)
        End If
        If IsDate(txtPlanDate.Text) = False Then
            MsgBox("Not a valid Date")
            Cancel = True
        Else
            If FYChk((txtPlanDate.Text)) = False Then
                Cancel = True
            Else
                If CheckValidate() = False Then Cancel = True
            End If
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCode.DoubleClick
        Call cmdSearchCode_Click(cmdSearchCode, New System.EventArgs())
    End Sub

    Private Sub txtCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchCode_Click(cmdSearchCode, New System.EventArgs())
    End Sub

    Private Function CheckValidate() As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mMKEY As String
        Dim xMkey As String = ""
        Dim mPreviousPlanQty As Double
        CheckValidate = True
        If Trim(txtCode.Text) = "" Then CheckValidate = True : Exit Function
        If Trim(txtDept.Text) = "" Then CheckValidate = True : Exit Function
        If Trim(txtPlanDate.Text) = "" Then CheckValidate = True : Exit Function

        mPreviousPlanQty = GetPlanQty()
        txtPreviousPlan.Text = VB6.Format(mPreviousPlanQty, "0.00")
        txtNetPlan.Text = VB6.Format(Val(txtAmendQty.Text) + mPreviousPlanQty, "0.00")


        xMkey = VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00") & Trim(txtCode.Text) & Trim(txtDept.Text) & VB6.Format(txtPlanDate.Text, "YYYYMMDD")

        If MODIFYMode = True And RsProdPlanAmend.EOF = False Then mMKEY = RsProdPlanAmend.Fields("AUTO_KEY_AMEND").Value

        SqlStr = "Select * From PRD_PROD_PLAN_AMEND_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND AUTO_KEY_AMEND='" & Trim(xMkey) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProdPlanAmend, ADODB.LockTypeEnum.adLockReadOnly)

        If RsProdPlanAmend.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
            CheckValidate = True
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Parameter Found, Use Generate Item Consumption Option To add", MsgBoxStyle.Information)
                CheckValidate = True
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * From PRD_PROD_PLAN_AMEND_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AUTO_KEY_AMEND='" & Trim(mMKEY) & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsProdPlanAmend, ADODB.LockTypeEnum.adLockReadOnly)
                CheckValidate = True
            End If
        End If
        Exit Function
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
    End Function
    Private Sub Show1()

        On Error GoTo ERR1
        Dim mReason As String
        Dim mPreviousPlanQty As Double
        Dim NetPlanQty As Double

        With RsProdPlanAmend
            If Not .EOF Then
                lblMKey.Text = .Fields("AUTO_KEY_AMEND").Value
                txtCode.Text = .Fields("PRODUCT_CODE").Value

                txtPlanDate.Text = VB6.Format(.Fields("SERIAL_DATE").Value, "DD/MM/YYYY")
                txtDept.Text = IIf(IsDbNull(.Fields("DEPT_CODE").Value), "", .Fields("DEPT_CODE").Value)

                mReason = IIf(IsDbNull(.Fields("REASON").Value), "", .Fields("REASON").Value)
                If mReason = "A" Then
                    cboReason.SelectedIndex = 0
                ElseIf mReason = "B" Then
                    cboReason.SelectedIndex = 1
                ElseIf mReason = "C" Then
                    cboReason.SelectedIndex = 2
                ElseIf mReason = "D" Then
                    cboReason.SelectedIndex = 3
                ElseIf mReason = "E" Then
                    cboReason.SelectedIndex = 4
                ElseIf mReason = "F" Then
                    cboReason.SelectedIndex = 5
                End If


                txtAmendQty.Text = VB6.Format(.Fields("AMEND_QTY").Value, "0.00")
                txtRemarks.Text = IIf(IsDbNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)

                If MainClass.ValidateWithMasterTable(txtCode.Text, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
                    lblDescription.text = MasterNo
                End If

                If MainClass.ValidateWithMasterTable(txtDept.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
                    txtDeptName.Text = MasterNo
                End If

                mPreviousPlanQty = GetPlanQty()
                txtPreviousPlan.Text = VB6.Format(mPreviousPlanQty, "0.00")
                txtNetPlan.Text = VB6.Format(Val(txtAmendQty.Text) + mPreviousPlanQty, "0.00")

            End If
        End With
        ADDMode = False
        MODIFYMode = False

        MainClass.ButtonStatus(Me, XRIGHT, RsProdPlanAmend, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Call MakeEnableDesableField(False)

        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume	
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = ""

        SqlStr = " SELECT AUTO_KEY_AMEND, PRODUCT_CODE,  " & vbCrLf & " DEPT_CODE, SERIAL_DATE," & vbCrLf & " AMEND_QTY, REASON, REMARKS" & vbCrLf & " FROM PRD_PROD_PLAN_AMEND_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " ORDER BY " & vbCrLf & " PRODUCT_CODE, DEPT_CODE, SERIAL_DATE"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))

        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 600)
            .set_ColWidth(0, 500)
            .set_ColWidth(1, 500 * 4)

            .set_ColWidth(2, 500 * 3)
            .set_ColWidth(3, 500 * 3)
            .set_ColWidth(4, 500 * 3)
            .set_ColWidth(5, 500 * 3)
            .set_ColWidth(6, 500 * 5)
            .set_ColWidth(5, 500 * 3)

            .Col = 1
            .ColHidden = True
            .ColsFrozen = 2
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub MakeEnableDesableField(ByRef mMode As Boolean)
        '    txtSupplierCode.Enabled = mMode	
        '    cmdsearchSupp.Enabled = mMode	
        txtCode.Enabled = mMode
        cmdSearchCode.Enabled = mMode
        txtPlanDate.Enabled = mMode
        txtDept.Enabled = mMode
        cmdDeptSearch.Enabled = mMode

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

    Private Sub ReportOnProdPlan(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mRPTName As String
        Dim mTitle As String
        Dim mSubTitle As String

        SqlStr = ""
        SqlStr = ""

        SqlStr = "SELECT * FROM TEMP_PRD_REQ_PRODPLAN_DET " & vbCrLf & " WHERE USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' ORDER BY USERID, INHOUSE_CODE" 'RM_CODE	

        mRPTName = "DailyProdPlanReqAmend.rpt"
        mTitle = "Daily Production Plan (Amendment)"

        mTitle = mTitle & " - as on " & VB6.Format(txtPlanDate.Text, "DD/MM/YYYY")

        mSubTitle = "(Product : " & txtCode.Text & ")"

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRPTName)


        Exit Sub
ERR1:
        If Err.Number = 32755 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        Else
            MsgInformation(Err.Description)
        End If
        '    Resume	
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnProdPlan(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnProdPlan(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub txtPreviousPlan_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPreviousPlan.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
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
End Class
