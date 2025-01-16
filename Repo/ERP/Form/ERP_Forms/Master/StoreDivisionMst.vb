Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmStoreDivisionMst
    Inherits System.Windows.Forms.Form
    Dim RsGeneral As ADODB.Recordset
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    ''Private PvtDBCn As ADODB.Connection

    Dim xCode As String
    Dim FormActive As Boolean
    Dim Shw As Boolean
    Dim MasterNo As Object
    Dim SqlStr As String = ""
    Private Sub ViewGrid()

        On Error GoTo ErrorPart
        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        ADataMain.Refresh	
            FormatSprdView()
            SprdView.Refresh()

            SprdView.Focus()
            FraGridView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraGridView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsGeneral, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ErrorPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Clear1()

        txtCode.Text = ""
        txtDesc.Text = ""
        txtAlias.Text = ""
        chkCommonDiv.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkWareHouse.CheckState = System.Windows.Forms.CheckState.Unchecked
        optStatus(0).Checked = True
        lblAddUser.Text = ""
        lblAddDate.Text = ""
        lblModUser.Text = ""
        lblModDate.Text = ""


        txtAddress.Text = ""
        txtCity.Text = ""
        txtPinCode.Text = ""
        txtState.Text = ""
        txtContactNo.Text = ""

        If RsCompany.Fields("DIV_AS_LOCATION").Value = "Y" Then
            txtAddress.Visible = True
            txtCity.Visible = True
            txtPinCode.Visible = True
            txtState.Visible = True
            lblAddress.Visible = True
            lblCity.Visible = True
            lblPinCode.Visible = True
            lblState.Visible = True
            txtContactNo.Visible = True
        Else
            txtAddress.Visible = False
            txtCity.Visible = False
            txtPinCode.Visible = False
            txtState.Visible = False
            lblAddress.Visible = False
            lblCity.Visible = False
            lblPinCode.Visible = False
            lblState.Visible = False
            txtContactNo.Visible = False
        End If



        txtMRRSeries.Text = ""
        txtDSPSeries.Text = ""
        txtRGPSeries.Text = ""
        txtINVSeries.Text = ""
        txtPURSeries.Text = ""

        txtMRRSeries.Enabled = IIf(RsCompany.Fields("SEPARATE_MRR_SERIES").Value = "Y", True, False)
        txtDSPSeries.Enabled = IIf(RsCompany.Fields("SEPARATE_DSP_SERIES").Value = "Y", True, False)
        txtRGPSeries.Enabled = IIf(RsCompany.Fields("SEPARATE_RGP_SERIES").Value = "Y", True, False)
        txtINVSeries.Enabled = IIf(RsCompany.Fields("SEPARATE_INV_SERIES").Value = "Y", True, False)
        txtPURSeries.Enabled = IIf(RsCompany.Fields("SEPARATE_PUR_SERIES").Value = "Y", True, False)

        txtCode.Enabled = True
        Call AutoCompleteSearch("INV_DIVISION_MST", "TO_CHAR(DIV_CODE)", "", txtCode)
        Call AutoCompleteSearch("INV_DIVISION_MST", "DIV_DESC", "", txtDesc)

        MainClass.ButtonStatus(Me, XRIGHT, RsGeneral, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub chkCommonDiv_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCommonDiv.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkWareHouse_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkWareHouse.CheckStateChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsGeneral, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            txtCode.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPreview.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ShowReport(Crystal.DestinationConstants.crptToWindow)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        Me.Cursor = System.Windows.Forms.Cursors.Arrow
        ShowReport(Crystal.DestinationConstants.crptToPrinter)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo ERR1
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtCode.Enabled = False
            '        txtCode.SetFocus	
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr
        SqlStr = ""

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        If InsertIntoDelAudit(PubDBCn, "INV_DIVISION_MST", (txtCode.Text), RsGeneral, "DIV_DESC") = False Then GoTo DeleteErr
        If InsertIntoDeleteTrn(PubDBCn, "INV_DIVISION_MST", "DIV_CODE", (txtCode.Text)) = False Then GoTo DeleteErr

        SqlStr = " DELETE FROM INV_DIVISION_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND DIV_CODE=" & Val(txtCode.Text) & ""

        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        RsGeneral.Requery() ''.Refresh	
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans() ''	
        RsGeneral.Requery() ''.Refresh	
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete, Transactions Exists Against This Expense Head.", MsgBoxStyle.Information)
            Exit Function
        End If
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If txtCode.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub
        If Not RsGeneral.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.	
                If Delete1 = False Then GoTo DelErrPart
                If RsGeneral.EOF = True Then
                    Clear1()
                Else
                    Show1()
                End If
            End If
        End If
        Exit Sub
DelErrPart:
        ErrorMsg("Record Not Deleted", "DELETE", MsgBoxStyle.Critical)
    End Sub
    Private Sub frmStoreDivisionMst_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub frmStoreDivisionMst_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub optStatus_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optStatus.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optStatus.GetIndex(eventSender)

            MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        txtCode.Text = Trim(SprdView.Text)
        txtCode_Validating(txtCode, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub
    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Private Sub txtAlias_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAlias.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAlias_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAlias.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtAlias.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        SqlStr = ""
        If Trim(txtCode.Text) = "" Then GoTo EventExitSub
        If MODIFYMode = True And RsGeneral.EOF = False Then xCode = RsGeneral.Fields("DIV_CODE").Value

        SqlStr = "SELECT * FROM INV_DIVISION_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND DIV_CODE=" & Val(txtCode.Text) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGeneral, ADODB.LockTypeEnum.adLockReadOnly)

        If RsGeneral.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Division Code Does Not Exist In Master" & vbCrLf & "Click Add To Add In Master")
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = ""
                SqlStr = "SELECT * FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND DIV_CODE='" & xCode & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGeneral, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub frmStoreDivisionMst_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        MainClass.UOpenRecordSet("Select * From INV_DIVISION_MST Where 1<>1 ", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGeneral, ADODB.LockTypeEnum.adLockReadOnly)


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        AssignGrid(False)
        SetTextLengths()
        Clear1()
        If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmStoreDivisionMst_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Set PvtDBCn = New ADODB.Connection	
        'PvtDBCn.Open StrConn	

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CmdView.Text = ConCmdGridViewCaption
        ADDMode = False
        MODIFYMode = False
        Call SetMainFormCordinate(Me)
        'Me.Left = 0
        'Me.Top = 0
        'Me.Height = VB6.TwipsToPixelsY(5220)
        ''Me.Width = VB6.TwipsToPixelsX(8265)
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmStoreDivisionMst_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsGeneral = Nothing
        RsGeneral.Close()
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim mPrdType As String
        Dim mStatus As String

        If Not RsGeneral.EOF Then

            txtCode.Text = IIf(IsDbNull(RsGeneral.Fields("DIV_CODE").Value), "", RsGeneral.Fields("DIV_CODE").Value)
            txtDesc.Text = IIf(IsDbNull(RsGeneral.Fields("DIV_DESC").Value), "", RsGeneral.Fields("DIV_DESC").Value)
            txtAlias.Text = IIf(IsDBNull(RsGeneral.Fields("DIV_ALIAS").Value), "", RsGeneral.Fields("DIV_ALIAS").Value)

            txtAddress.Text = IIf(IsDBNull(RsGeneral.Fields("DIV_ADDRESS").Value), "", RsGeneral.Fields("DIV_ADDRESS").Value)
            txtCity.Text = IIf(IsDBNull(RsGeneral.Fields("DIV_CITY").Value), "", RsGeneral.Fields("DIV_CITY").Value)
            txtPinCode.Text = IIf(IsDBNull(RsGeneral.Fields("DIV_PINCODE").Value), "", RsGeneral.Fields("DIV_PINCODE").Value)
            txtContactNo.Text = IIf(IsDBNull(RsGeneral.Fields("DIV_CONTACTNO").Value), "", RsGeneral.Fields("DIV_CONTACTNO").Value)

            txtState.Text = IIf(IsDBNull(RsGeneral.Fields("DIV_STATE").Value), "", RsGeneral.Fields("DIV_STATE").Value)

            mStatus = IIf(IsDbNull(RsGeneral.Fields("DIV_STATUS").Value), "", RsGeneral.Fields("DIV_STATUS").Value)
            optStatus(0).Checked = IIf(mStatus = "O", True, False)
            optStatus(1).Checked = IIf(mStatus = "C", True, False)

            chkCommonDiv.CheckState = IIf(RsGeneral.Fields("IS_COMMON_DIV").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            chkCommonDiv.Enabled = IIf(RsGeneral.Fields("IS_COMMON_DIV").Value = "Y", False, True)

            chkWareHouse.CheckState = IIf(RsGeneral.Fields("IS_WAREHOUSE_DIV").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkWareHouse.Enabled = IIf(RsGeneral.Fields("IS_WAREHOUSE_DIV").Value = "Y", False, True)

            txtMRRSeries.Text = IIf(IsDbNull(RsGeneral.Fields("MRR_SERIES").Value), "", RsGeneral.Fields("MRR_SERIES").Value)
            txtDSPSeries.Text = IIf(IsDbNull(RsGeneral.Fields("DSP_SERIES").Value), "", RsGeneral.Fields("DSP_SERIES").Value)
            txtRGPSeries.Text = IIf(IsDbNull(RsGeneral.Fields("RGP_SERIES").Value), "", RsGeneral.Fields("RGP_SERIES").Value)
            txtINVSeries.Text = IIf(IsDbNull(RsGeneral.Fields("INV_SERIES").Value), "", RsGeneral.Fields("INV_SERIES").Value)
            txtPURSeries.Text = IIf(IsDbNull(RsGeneral.Fields("PUR_SERIES").Value), "", RsGeneral.Fields("PUR_SERIES").Value)

            lblAddUser.Text = IIf(IsDbNull(RsGeneral.Fields("ADDUSER").Value), "", RsGeneral.Fields("ADDUSER").Value)
            lblAddDate.Text = VB6.Format(IIf(IsDBNull(RsGeneral.Fields("ADDDATE").Value), "", RsGeneral.Fields("ADDDATE").Value), "dd/MM/yyyy")
            lblModUser.Text = IIf(IsDBNull(RsGeneral.Fields("MODUSER").Value), "", RsGeneral.Fields("MODUSER").Value)
            lblModDate.Text = VB6.Format(IIf(IsDBNull(RsGeneral.Fields("MODDATE").Value), "", RsGeneral.Fields("MODDATE").Value), "dd/MM/yyyy")

            xCode = RsGeneral.Fields("DIV_CODE").Value
        End If
        txtCode.Enabled = True
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsGeneral, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
        '    Resume	
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
            txtCode_Validating(txtCode, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True Then CmdAdd.Focus()
        Else
            MsgInformation("Record not saved")
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    Private Function Update1() As Boolean

        On Error GoTo UpdateError
        Dim mCode As Double
        Dim mIsCommonDiv As String
        Dim mIsWareHouseDiv As String
        Dim mStatus As String
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mStatus = IIf(optStatus(0).Checked = True, "O", "C")

        SqlStr = ""
        mIsCommonDiv = IIf(chkCommonDiv.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mIsWareHouseDiv = IIf(chkWareHouse.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")



        If ADDMode = True Then
            mCode = MainClass.AutoGenVNo("SELECT MAX(DIV_CODE) AS DIV_CODE FROM INV_DIVISION_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "", PubDBCn)
            txtCode.Text = CStr(mCode)
            SqlStr = "INSERT INTO INV_DIVISION_MST (" & vbCrLf _
                & " COMPANY_CODE, DIV_CODE, DIV_DESC, DIV_ALIAS, " & vbCrLf _
                & " ADDUSER,ADDDATE,MODUSER,MODDATE, " & vbCrLf _
                & " MRR_SERIES,DSP_SERIES," & vbCrLf _
                & " RGP_SERIES,INV_SERIES,PUR_SERIES, IS_COMMON_DIV, DIV_STATUS,DIV_ADDRESS,DIV_CITY,DIV_PINCODE,DIV_STATE, DIV_CONTACTNO , IS_WAREHOUSE_DIV" & vbCrLf _
                & " ) VALUES ( " & vbCrLf _
                & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                & " " & Val(txtCode.Text) & ", " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtDesc.Text) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtAlias.Text) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','', " & vbCrLf _
                & " " & Val(txtMRRSeries.Text) & ", " & Val(txtDSPSeries.Text) & ", " & vbCrLf & " " & Val(txtRGPSeries.Text) & ", " & Val(txtINVSeries.Text) & "," & Val(txtPURSeries.Text) & ",'" & mIsCommonDiv & "', " & vbCrLf _
                & " '" & mStatus & "','" & MainClass.AllowSingleQuote(txtAddress.Text) & "','" & MainClass.AllowSingleQuote(txtCity.Text) & "','" & MainClass.AllowSingleQuote(txtPinCode.Text) & "','" & MainClass.AllowSingleQuote(txtState.Text) & "','" & MainClass.AllowSingleQuote(txtContactNo.Text) & "','" & mIsWareHouseDiv & "')"
        Else
            SqlStr = " UPDATE INV_DIVISION_MST  SET " & vbCrLf _
                & " DIV_DESC='" & MainClass.AllowSingleQuote(txtDesc.Text) & "',DIV_STATUS='" & mStatus & "'," & vbCrLf _
                & " DIV_ALIAS='" & MainClass.AllowSingleQuote(txtAlias.Text) & "'," & vbCrLf _
                & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'), IS_COMMON_DIV= '" & mIsCommonDiv & "',IS_WAREHOUSE_DIV='" & mIsWareHouseDiv & "'," & vbCrLf _
                & " MRR_SERIES=" & Val(txtMRRSeries.Text) & ", DSP_SERIES=" & Val(txtDSPSeries.Text) & ", DIV_CONTACTNO='" & MainClass.AllowSingleQuote(txtContactNo.Text) & "'," & vbCrLf _
                & " RGP_SERIES=" & Val(txtRGPSeries.Text) & ", INV_SERIES=" & Val(txtINVSeries.Text) & ",PUR_SERIES=" & Val(txtPURSeries.Text) & ", " & vbCrLf _
                & " DIV_ADDRESS='" & MainClass.AllowSingleQuote(txtAddress.Text) & "',DIV_CITY='" & MainClass.AllowSingleQuote(txtCity.Text) & "',DIV_PINCODE='" & MainClass.AllowSingleQuote(txtPinCode.Text) & "',DIV_STATE='" & MainClass.AllowSingleQuote(txtState.Text) & "'" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND DIV_CODE= " & Val(xCode) & ""
        End If


UpdatePart:
        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        Update1 = True
        Exit Function
UpdateError:
        Update1 = False
        PubDBCn.RollbackTrans() ''	
        RsGeneral.Requery() ''.Refresh	
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        txtCode.Maxlength = RsGeneral.Fields("DIV_CODE").Precision
        txtDesc.Maxlength = RsGeneral.Fields("DIV_DESC").DefinedSize
        txtAlias.MaxLength = RsGeneral.Fields("DIV_ALIAS").DefinedSize

        txtAddress.MaxLength = RsGeneral.Fields("DIV_ADDRESS").DefinedSize
        txtCity.MaxLength = RsGeneral.Fields("DIV_CITY").DefinedSize
        txtPinCode.MaxLength = RsGeneral.Fields("DIV_PINCODE").DefinedSize
        txtState.MaxLength = RsGeneral.Fields("DIV_STATE").DefinedSize
        txtContactNo.MaxLength = RsGeneral.Fields("DIV_CONTACTNO").DefinedSize
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function FieldsVarification() As Boolean

        On Error GoTo err_Renamed
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        FieldsVarification = True
        '    If Trim(txtCode) = "" Or Val(txtCode.Text) <= 0 Then	
        '        MsgInformation "Division code is empty. Cannot Save"	
        '        txtCode.SetFocus	
        '        FieldsVarification = False	
        '        Exit Function	
        '    End If	

        If Trim(txtDesc.Text) = "" Then
            MsgInformation("Division Description is empty. Cannot Save")
            txtDesc.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtAlias.Text) = "" Then
            MsgInformation("Division Alias is empty. Cannot Save")
            txtAlias.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If chkCommonDiv.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = "SELECT IS_COMMON_DIV FROM INV_DIVISION_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IS_COMMON_DIV='Y'"
            If MODIFYMode = True Then
                SqlStr = SqlStr & vbCrLf & " AND DIV_CODE<> " & Val(txtCode.Text) & ""
            End If
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                MsgInformation("You Already Defined Common Division. Cannot Save")
                If chkCommonDiv.Enabled = True Then chkCommonDiv.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        '    If chkMRR.Value = vbChecked Then	
        '        If Val(txtMRRSeries.Text) = 0 Then	
        '            MsgInformation "MRR Starting Series No is empty. Cannot Save"	
        '            txtMRRSeries.SetFocus	
        '            FieldsVarification = False	
        '            Exit Function	
        '        End If	
        '	
        '        If (Val(txtMRRSeries.Text) <= 60000 Or Val(txtMRRSeries.Text) >= 100000) And Val(txtCode.Text) > 1 Then	
        '            MsgInformation "MRR Starting Series not Less than 60000 or not Greater Than 100000. Cannot Save"	
        '            txtMRRSeries.SetFocus	
        '            FieldsVarification = False	
        '            Exit Function	
        '        End If	
        '	
        '    End If	
        '	
        '    If chkDespatch.Value = vbChecked Then	
        '        If Val(txtDSPSeries.Text) = 0 Then	
        '            MsgInformation "Despatch Starting Series No is empty. Cannot Save"	
        '            txtDSPSeries.SetFocus	
        '            FieldsVarification = False	
        '            Exit Function	
        '        End If	
        '	
        '        If (Val(txtDSPSeries.Text) <= 60000 Or Val(txtDSPSeries.Text) >= 100000) And Val(txtCode.Text) > 1 Then	
        '            MsgInformation "Despatch Starting Series not Less than 60000 or not Greater Than 100000. Cannot Save"	
        '            txtDSPSeries.SetFocus	
        '            FieldsVarification = False	
        '            Exit Function	
        '        End If	
        '	
        '    End If	
        '	
        '    If chkGatepass.Value = vbChecked Then	
        '        If Val(txtRGPSeries.Text) = 0 Then	
        '            MsgInformation "GatePass Starting Series No is empty. Cannot Save"	
        '            txtRGPSeries.SetFocus	
        '            FieldsVarification = False	
        '            Exit Function	
        '        End If	
        '	
        '        If (Val(txtRGPSeries.Text) <= 60000 Or Val(txtRGPSeries.Text) >= 100000) And Val(txtCode.Text) > 1 Then	
        '            MsgInformation "GatePass Starting Series not Less than 60000 or not Greater Than 100000. Cannot Save"	
        '            txtRGPSeries.SetFocus	
        '            FieldsVarification = False	
        '            Exit Function	
        '        End If	
        '	
        '    End If	
        '	
        '    If chkInvoice.Value = vbChecked Then	
        '        If Val(txtINVSeries.Text) = 0 Then	
        '            MsgInformation "Invoice Starting Series No is empty. Cannot Save"	
        '            txtINVSeries.SetFocus	
        '            FieldsVarification = False	
        '            Exit Function	
        '        End If	
        '	
        '        If (Val(txtINVSeries.Text) <= 100000 Or Val(txtINVSeries.Text) >= 100000) And Val(txtCode.Text) > 1 Then	
        '            MsgInformation "Invoice Starting Series not Less than 60000 or not Greater Than 100000. Cannot Save"	
        '            txtINVSeries.SetFocus	
        '            FieldsVarification = False	
        '            Exit Function	
        '        End If	
        '	
        '    End If	

        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
        End If
        If MODIFYMode = True And RsGeneral.EOF = True Then Exit Function
        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function
    Private Sub txtCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        Dim SqlStr As String = ""

        SqlStr = ""

        SqlStr = " SELECT DIV_CODE,DIV_DESC,DECODE(DIV_STATUS,'O','OPEN','CLOSED') AS STATUS " '& vbCrLf |	
        SqlStr = SqlStr & vbCrLf & " FROM INV_DIVISION_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        SqlStr = SqlStr & vbCrLf & "ORDER BY DIV_DESC"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 12)
            .set_ColWidth(0, 5)
            .set_ColWidth(1, 8)
            .set_ColWidth(2, 25)

            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub

    Private Sub ShowReport(ByRef Mode As Crystal.DestinationConstants)
        Dim mTitle As String = ""
        On Error GoTo ERR1
        mTitle = ""
        Report1.Reset()
        mTitle = "Category Master"
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\StrDivision.rpt"
        SetCrpt(Report1, Mode, 1, mTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub txtDesc_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDesc.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDesc_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDesc.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDSPSeries_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDSPSeries.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDSPSeries_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDSPSeries.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtINVSeries_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtINVSeries.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtINVSeries_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtINVSeries.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtMRRSeries_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMRRSeries.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMRRSeries_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMRRSeries.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPURSeries_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPURSeries.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPURSeries_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPURSeries.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRGPSeries_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRGPSeries.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRGPSeries_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRGPSeries.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDesc_Validating(sender As Object, EventArgs As System.ComponentModel.CancelEventArgs) Handles txtDesc.Validating
        Dim Cancel As Boolean = EventArgs.Cancel

        On Error GoTo ERR1
        SqlStr = ""
        If Trim(txtDesc.Text) = "" Then GoTo EventExitSub
        If MODIFYMode = True And RsGeneral.EOF = False Then xCode = RsGeneral.Fields("DIV_CODE").Value

        SqlStr = "SELECT * FROM INV_DIVISION_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND DIV_DESC='" & MainClass.AllowSingleQuote(txtDesc.Text) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGeneral, ADODB.LockTypeEnum.adLockReadOnly)

        If RsGeneral.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Division Description Does Not Exist In Master" & vbCrLf & "Click Add To Add In Master")
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = ""
                SqlStr = "SELECT * FROM INV_DIVISION_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND DIV_CODE='" & xCode & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGeneral, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        EventArgs.Cancel = Cancel
    End Sub
End Class
