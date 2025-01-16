Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmRMGradeMaster
    Inherits System.Windows.Forms.Form
    Dim RsItemMast As ADODB.Recordset ''ADODB.Recordset				
    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean
    Dim mItemCode As String


    Private Const ConRowHeight As Short = 14

    Private Sub cboRMType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboRMType.TextChanged

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
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        On Error Resume Next
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        Dim SqlStr As String

        If txtItemName.Text = "" Then Call ErrorMsg("Nothing to Delete", "", MsgBoxStyle.Critical) : Exit Sub

        If Not RsItemMast.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()

                If InsertIntoDelAudit(PubDBCn, "PRD_MTRL_MST", (txtItemCode.Text), RsItemMast, "MTRL_CODE") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "PRD_MTRL_MST", "MTRL_CODE", RsItemMast.Fields("MTRL_CODE").Value) = False Then GoTo DelErrPart

                SqlStr = " DELETE From PRD_MTRL_MST WHERE " & vbCrLf _
                & " COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & "" & vbCrLf _
                & " AND MTRL_CODE='" & MainClass.AllowSingleQuote(Trim(RsItemMast.Fields("MTRL_CODE").Value)) & "'"


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
        Dim SqlStr As String


        If ADDMode = True Then
            mItemCode = MainClass.AllowSingleQuote(txtItemCode.Text) ''MainClass.AutoGenRowNo("ACM", "Code", PubDBCn)				


            SqlStr = ""
            SqlStr = " INSERT INTO PRD_MTRL_MST ( " & vbCrLf & " COMPANY_CODE, MTRL_CODE, " & vbCrLf & " MTRL_DESC, MTRL_DENSITY, MTRL_TYPE, GRADE_UOM, " & vbCrLf & " ADDUSER, ADDDATE, MODUSER, MODDATE) VALUES ( "

            SqlStr = SqlStr & vbCrLf & " " & RsCompany.Fields("Company_Code").Value & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtItemCode.Text) & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(UCase(txtItemName.Text)) & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(txtDensity.Text) & "', '" & MainClass.AllowSingleQuote(cboRMType.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtUOM.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"

        End If

        If MODIFYMode = True Then
            SqlStr = ""

            SqlStr = " UPDATE PRD_MTRL_MST SET  " & vbCrLf & " MTRL_DESC= '" & MainClass.AllowSingleQuote(UCase(txtItemName.Text)) & "', " & vbCrLf & " MTRL_DENSITY = '" & MainClass.AllowSingleQuote(txtDensity.Text) & "', " & vbCrLf & " MTRL_TYPE = '" & MainClass.AllowSingleQuote(cboRMType.Text) & "', " & vbCrLf & " GRADE_UOM = '" & MainClass.AllowSingleQuote(txtUOM.Text) & "', " & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND MTRL_CODE = '" & Trim(mItemCode) & "'"

        End If
        PubDBCn.Execute(SqlStr)
        UpdateItem = True
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateItem = False
        'Resume				
    End Function

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster(txtItemName.Text, "PRD_MTRL_MST", "MTRL_DESC", "MTRL_CODE", , , SqlStr) = True Then
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
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster(txtItemCode.Text, "PRD_MTRL_MST", "MTRL_CODE", "MTRL_DESC", , , SqlStr) = True Then
            txtItemCode.Text = AcName
            txtItemCode_Validating(txtItemCode, New System.ComponentModel.CancelEventArgs(False))
            If txtItemCode.Enabled = True Then txtItemCode.Focus()
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
        MainClass.ButtonStatus(Me, XRIGHT, RsItemMast, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmRMGradeMaster_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = "Select * From PRD_MTRL_MST WHERE 1<>1 Order by MTRL_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsItemMast, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)

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
        Dim SqlStr As String

        SqlStr = ""

        SqlStr = " SELECT INVMST.MTRL_CODE,INVMST.MTRL_DESC,MTRL_TYPE, MTRL_DENSITY" & vbCrLf & " FROM PRD_MTRL_MST INVMST" & vbCrLf & " WHERE INVMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        SqlStr = SqlStr & " ORDER BY INVMST.MTRL_DESC"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmRMGradeMaster_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''''Set PvtDBCn = New ADODB.Connection				
        ''''PvtDBCn.Open StrConn				
        Call SetMainFormCordinate(Me)
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        cboRMType.Items.Clear()
        cboRMType.Items.Add("SHEET")
        cboRMType.Items.Add("ROD")
        cboRMType.Items.Add("ROUND PIPE")
        cboRMType.Items.Add("OTHERS")
        cboRMType.SelectedIndex = 0

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

        mItemCode = CStr(-1)
        txtItemName.Text = ""
        txtItemCode.Text = ""
        txtItemCode.Enabled = True

        lblAddUser.Text = ""
        lblAddDate.Text = ""
        lblModUser.Text = ""
        lblModDate.Text = ""

        txtDensity.Text = ""
        txtUOM.Text = ""
        cboRMType.SelectedIndex = -1

        MainClass.ButtonStatus(Me, XRIGHT, RsItemMast, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '''Resume				
    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 500)
            .set_ColWidth(0, 500)
            .set_ColWidth(1, 1500)
            .set_ColWidth(2, 3000)
            .set_ColWidth(3, 1500)
            .set_ColWidth(4, 1500)

            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1

        txtItemName.MaxLength = RsItemMast.Fields("MTRL_DESC").DefinedSize
        txtDensity.MaxLength = RsItemMast.Fields("MTRL_DENSITY").DefinedSize
        txtUOM.MaxLength = RsItemMast.Fields("GRADE_UOM").DefinedSize
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume				
    End Sub

    Function FieldVarification() As Boolean
        On Error GoTo err_Renamed
        Dim mProdType As String

        FieldVarification = True
        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldVarification = False
            Exit Function
        End If

        If MODIFYMode = True Then
            If IsDate(lblAddDate.Text) = False Then
                lblAddDate.Text = RsCompany.Fields("Start_Date").Value
            End If
            If MainClass.GetUserCanModifyMaster(lblAddDate.Text, XRIGHT) = False Then
                MsgBox("You Have Not Rights to change back Entry.", vbInformation)
                FieldVarification = False
                Exit Function
            End If
        End If

        If txtItemName.Text = "" Then
            MsgInformation("Item Name is empty. Cannot Save")
            If txtItemName.Enabled = True Then txtItemName.Focus()
            FieldVarification = False
            Exit Function
        End If

        If Trim(txtItemCode.Text) = "" Then
            MsgInformation("Item Code is empty. Cannot Save")
            txtItemCode.Focus()
            FieldVarification = False
            Exit Function
        End If


        If Len(Trim(txtItemCode.Text)) <> 6 Then
            MsgInformation("Item Code must be six digit. Cannot Save")
            txtItemCode.Focus()
            FieldVarification = False
            Exit Function
        End If

        If Trim(txtDensity.Text) = "" Or Val(txtDensity.Text) = 0 Then
            MsgInformation("Please Define the Density. Cannot Save")
            txtDensity.Focus()
            FieldVarification = False
            Exit Function
        End If

        If Trim(txtUOM.Text) = "" Then
            MsgInformation("Please enter Grade UOM. Cannot Save")
            txtDensity.Focus()
            FieldVarification = False
            Exit Function
        End If

        If Trim(cboRMType.Text) = "" Then
            MsgInformation("Please Select RM Type. Cannot Save")
            cboRMType.Focus()
            FieldVarification = False
            Exit Function
        End If

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function

    Private Sub frmRMGradeMaster_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        Me.Hide()
        Me.Close()
        Me.Dispose()
        RsItemMast.Close()
        'RsOpOuts.Close				
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
    Private Sub txtDensity_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDensity.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDensity_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDensity.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
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
        Dim SqlStr As String

        If Trim(txtItemCode.Text) = "" Then GoTo EventExitSub
        If MODIFYMode = True And RsItemMast.EOF = False Then mItemCode = RsItemMast.Fields("MTRL_CODE").Value
        SqlStr = "Select * From PRD_MTRL_MST " & vbCrLf _
        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        & " AND MTRL_CODE='" & MainClass.AllowSingleQuote(txtItemCode.Text) & "'"


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
                SqlStr = "Select * From PRD_MTRL_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MTRL_CODE=" & mItemCode & ""
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsItemMast, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
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
        Dim SqlStr As String

        If Trim(txtItemName.Text) = "" Then GoTo EventExitSub
        If MODIFYMode = True And RsItemMast.EOF = False Then mItemCode = RsItemMast.Fields("MTRL_CODE").Value
        SqlStr = "Select * From PRD_MTRL_MST " & vbCrLf _
        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        & " AND LTRIM(RTRIM(UPPER(MTRL_DESC)))='" & MainClass.AllowSingleQuote(UCase(txtItemName.Text)) & "'"

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
                SqlStr = "Select * From PRD_MTRL_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MTRL_CODE='" & mItemCode & "'"
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

        Clear1()
        If Not RsItemMast.EOF Then

            mItemCode = IIf(IsDBNull(RsItemMast.Fields("MTRL_CODE").Value), -1, RsItemMast.Fields("MTRL_CODE").Value)
            txtItemName.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("MTRL_DESC").Value), "", RsItemMast.Fields("MTRL_DESC").Value))
            txtItemCode.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("MTRL_CODE").Value), "", RsItemMast.Fields("MTRL_CODE").Value))
            txtItemCode.Enabled = False
            txtDensity.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("MTRL_DENSITY").Value), "", RsItemMast.Fields("MTRL_DENSITY").Value))
            txtUOM.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("GRADE_UOM").Value), "", RsItemMast.Fields("GRADE_UOM").Value))

            cboRMType.Text = Trim(IIf(IsDBNull(RsItemMast.Fields("MTRL_TYPE").Value), "", RsItemMast.Fields("MTRL_TYPE").Value))

            lblAddUser.Text = IIf(IsDBNull(RsItemMast.Fields("ADDUSER").Value), "", RsItemMast.Fields("ADDUSER").Value)
            lblAddDate.Text = VB6.Format(IIf(IsDBNull(RsItemMast.Fields("ADDDATE").Value), "", RsItemMast.Fields("ADDDATE").Value), "DD/MM/YYYY")
            lblModUser.Text = IIf(IsDBNull(RsItemMast.Fields("MODUSER").Value), "", RsItemMast.Fields("MODUSER").Value)
            lblModDate.Text = VB6.Format(IIf(IsDBNull(RsItemMast.Fields("MODDATE").Value), "", RsItemMast.Fields("MODDATE").Value), "DD/MM/YYYY")

            '''Field Disable...				
        End If
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsItemMast, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume				
    End Sub

    Private Sub txtUOM_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtUOM.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtUOM_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtUOM.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtUOM.Text, "N")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
End Class
