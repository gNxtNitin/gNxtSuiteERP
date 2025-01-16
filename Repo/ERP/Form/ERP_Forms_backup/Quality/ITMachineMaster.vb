Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmITMachineMaster
    Inherits System.Windows.Forms.Form
    Dim RsMMST As ADODB.Recordset
    Dim RsMMSTConf As ADODB.Recordset
    Dim RsMMSTSoftware As ADODB.Recordset

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim PvtDBCn As ADODB.Connection
    Dim Shw As Boolean
    Dim FormActive As Boolean
    Dim xMachineno As String
    Dim SqlStr As String

    Private Const ConRowHeight As Short = 14

    Private Const ColName As Short = 1
    Private Const ColValue As Short = 2

    Private Const ColRenewalOn As Short = 2
    Private Const ColRemarks As Short = 3

    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim i As Short

        With SprdConfigMain
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = ColName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = MainClass.SetMaxLength("CONFIGUATION_DESC", "IT_CONFIGUATION_MST", PubDBCn) '' RsMMSTConf.Fields("CONFIG_NAME").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColName, 18)

            .Col = ColValue
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsMMSTConf.Fields("CONFIG_VALUE").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColValue, 18)

            '.Col = ColLastPM
            '.CellType = SS_CELL_TYPE_DATE
            '.TypeDateFormat = SS_CELL_DATE_FORMAT_DDMMYY
            '.set_ColWidth(ColLastPM, 15)

            'MainClass.ProtectCell(SprdMain, 1, .MaxRows, ColDuePM, ColDuePM)
            MainClass.SetSpreadColor(SprdConfigMain, Arow)
        End With

        With SprdSoftwareMain
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = ColName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = MainClass.SetMaxLength("SOFTWARE_DESC", "IT_SOFTWARE_MST", PubDBCn) ''RsMMSTConf.Fields("SOFTWARE_NAME").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColName, 15)

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsMMSTSoftware.Fields("REMARKS").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColRemarks, 14)

            .Col = ColRenewalOn
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateFormat = SS_CELL_DATE_FORMAT_DDMMYY
            .set_ColWidth(ColRenewalOn, 10)

            'MainClass.ProtectCell(SprdMain, 1, .MaxRows, ColDuePM, ColDuePM)
            MainClass.SetSpreadColor(SprdSoftwareMain, Arow)
        End With

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
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
            SprdView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            SprdView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsMMST, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_ColWidth(0, 0)
            .set_ColWidth(1, 900)
            .set_ColWidth(2, 900)
            .ColsFrozen = 2

            .set_ColWidth(3, 1500)
            .set_ColWidth(4, 900)
            .set_ColWidth(5, 900)
            .set_ColWidth(6, 900)
            .set_ColWidth(7, 900)
            .set_ColWidth(8, 1500)
            .set_ColWidth(9, 900)
            .set_ColWidth(10, 900)
            .set_ColWidth(11, 900)
            .set_ColWidth(12, 900)
            .set_ColWidth(13, 900)
            .set_ColWidth(14, 3500)
            .set_ColWidth(15, 3500)

            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub Clear1()

        txtMachineNo.Text = ""
        txtInsDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtItemCode.Text = ""
        txtItemName.Text = ""
        txtMachineDesc.Text = ""
        txtSpec.Text = ""

        txtDept.Text = ""
        txtDeptName.Text = ""
        txtLocation.Text = ""
        txtMake.Text = ""
        txtSerialNo.Text = ""

        chkMchbkDown.CheckState = System.Windows.Forms.CheckState.Unchecked



        cboStatus.Text = "OPEN/ACTIVE"
        cboMaintType.SelectedIndex = -1

        txtRemarks.Text = ""
        txtIPAddress.Text = ""
        cboDivision.SelectedIndex = -1
        cboDivision.Enabled = True

        Call MakeEnableDeField(True)

        MainClass.ClearGrid(SprdConfigMain, ConRowHeight)
        MainClass.ClearGrid(SprdSoftwareMain, ConRowHeight)
        FormatSprdMain(-1)

        MainClass.ButtonStatus(Me, XRIGHT, RsMMST, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Function UpdateDetail() As Boolean

        On Error GoTo UpdateDetailERR
        Dim SqlStr As String
        Dim i As Integer
        Dim mName As String
        Dim mCode As Long
        Dim mValue As String
        Dim mRemarks As String
        Dim mRenewalDate As String

        PubDBCn.Execute(" DELETE FROM IT_MACHINE_CONFIG_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "'")

        PubDBCn.Execute(" DELETE FROM IT_MACHINE_SOFTWARE_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "'")


        With SprdConfigMain
            For i = 1 To .MaxRows - 1
                .Row = i

                .Col = ColName
                mName = MainClass.AllowSingleQuote(.Text)

                If MainClass.ValidateWithMasterTable(mName, "CONFIGUATION_DESC", "CONFIGUATION_CODE", "IT_CONFIGUATION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mCode = Val(MasterNo)
                Else
                    MsgInformation("Invalid Name." & mName)
                    GoTo UpdateDetailERR
                End If

                .Col = ColValue
                mValue = MainClass.AllowSingleQuote(.Text)

                SqlStr = ""

                If mName <> "" And mValue <> "" Then
                    SqlStr = " INSERT INTO IT_MACHINE_CONFIG_MST ( " & vbCrLf _
                        & " COMPANY_CODE,MACHINE_NO,SERIAL_NO, " & vbCrLf _
                        & " CONFIG_CODE,CONFIG_VALUE)" & vbCrLf _
                        & " VALUES ( " & vbCrLf _
                        & " " & RsCompany.Fields("COMPANY_CODE").Value & ",'" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "'," & i & ", " & vbCrLf _
                        & " " & mCode & ",'" & mValue & "') "

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With

        With SprdSoftwareMain
            For i = 1 To .MaxRows - 1
                .Row = i

                .Col = ColName
                mName = MainClass.AllowSingleQuote(.Text)

                If MainClass.ValidateWithMasterTable(mName, "SOFTWARE_DESC", "SOFTWARE_CODE", "IT_SOFTWARE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mCode = Val(MasterNo)
                Else
                    MsgInformation("Invalid Name." & mName)
                    GoTo UpdateDetailERR
                End If

                .Col = ColRemarks
                mRemarks = MainClass.AllowSingleQuote(.Text)

                .Col = ColRenewalOn
                mRenewalDate = VB6.Format(.Text, "DD/MM/YYYY")

                SqlStr = ""

                If mName <> "" Then
                    SqlStr = " INSERT INTO IT_MACHINE_SOFTWARE_MST ( " & vbCrLf _
                        & " COMPANY_CODE,MACHINE_NO,SERIAL_NO, " & vbCrLf _
                        & " SOFTWARE_CODE,REMARKS, RENEWAL_DATE)" & vbCrLf _
                        & " VALUES ( " & vbCrLf _
                        & " " & RsCompany.Fields("COMPANY_CODE").Value & ",'" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "'," & i & ", " & vbCrLf _
                        & " " & mCode & ",'" & mRemarks & "', TO_DATE('" & VB6.Format(mRenewalDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

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
    Private Sub ShowDetail1()

        On Error GoTo ERR1
        Dim i As Integer
        Dim SqlStr As String
        Dim mCode As Long
        Dim mName As String

        SqlStr = ""

        SqlStr = " SELECT * " & vbCrLf _
                & " FROM IT_MACHINE_CONFIG_MST" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "' ORDER BY SERIAL_NO"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMMSTConf, ADODB.LockTypeEnum.adLockReadOnly)
        With RsMMSTConf
            If .EOF = True Then Exit Sub
            i = 1
            Do While Not .EOF
                SprdConfigMain.Row = i

                mCode = IIf(IsDBNull(.Fields("CONFIG_CODE").Value), 0, .Fields("CONFIG_CODE").Value)
                mName = ""

                If MainClass.ValidateWithMasterTable(mCode, "CONFIGUATION_CODE", "CONFIGUATION_DESC", "IT_CONFIGUATION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mName = Trim(MasterNo)
                End If


                SprdConfigMain.Col = ColName
                SprdConfigMain.Text = mName 'IIf(IsDBNull(.Fields("CONFIG_NAME").Value), "", .Fields("CONFIG_NAME").Value)

                SprdConfigMain.Col = ColValue
                SprdConfigMain.Text = IIf(IsDBNull(.Fields("CONFIG_VALUE").Value), "", .Fields("CONFIG_VALUE").Value)

                'SprdMain.Col = ColLastPM
                'SprdMain.Text = IIf(IsDBNull(.Fields("LAST_PM").Value), "", VB6.Format(.Fields("LAST_PM").Value, "DD/MM/YYYY"))

                .MoveNext()
                i = i + 1
                SprdConfigMain.MaxRows = i
            Loop
        End With

        SqlStr = " SELECT * " & vbCrLf _
                & " FROM IT_MACHINE_SOFTWARE_MST" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "'  ORDER BY SERIAL_NO"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMMSTSoftware, ADODB.LockTypeEnum.adLockReadOnly)
        With RsMMSTSoftware
            If .EOF = True Then Exit Sub
            i = 1
            Do While Not .EOF
                SprdSoftwareMain.Row = i

                mCode = IIf(IsDBNull(.Fields("SOFTWARE_CODE").Value), 0, .Fields("SOFTWARE_CODE").Value)
                mName = ""

                If MainClass.ValidateWithMasterTable(mCode, "SOFTWARE_CODE", "SOFTWARE_DESC", "IT_SOFTWARE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mName = Trim(MasterNo)
                End If

                SprdSoftwareMain.Col = ColName
                SprdSoftwareMain.Text = mName ' IIf(IsDBNull(.Fields("SOFTWARE_NAME").Value), "", .Fields("SOFTWARE_NAME").Value)

                SprdSoftwareMain.Col = ColRemarks '
                SprdSoftwareMain.Text = IIf(IsDBNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)

                SprdSoftwareMain.Col = ColRenewalOn
                SprdSoftwareMain.Text = IIf(IsDBNull(.Fields("RENEWAL_DATE").Value), "", VB6.Format(.Fields("RENEWAL_DATE").Value, "DD/MM/YYYY"))

                .MoveNext()
                i = i + 1
                SprdSoftwareMain.MaxRows = i
            Loop
        End With

        FormatSprdMain(-1)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub MakeEnableDeField(ByRef mMode As Boolean)
        '    txtMachineNo.Enabled = mMode
        '    TxtInsDate.Enabled = mMode
        '    cmdSearchMacNo.Enabled = mMode
        '    txtItemCode.Enabled = mMode
        '    cmdSearchItemCode.Enabled = mMode
        '    TxtItemName.Enabled = False
        '    TxtDept.Enabled = mMode
        '    CmdSearchDept.Enabled = mMode
        '    txtLocation.Enabled = False
        '    txtOperation.Enabled = mMode
        '    CmdSearchOp.Enabled = mMode
        '    TxtOperationName.Enabled = False
    End Sub

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboMaintType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboMaintType.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboMaintType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboMaintType.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboStatus_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboStatus.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboStatus_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboStatus.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
    Private Sub ChkMchbkDown_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkMchbkDown.CheckStateChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsMMST, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

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
        Call ShowReport(Crystal.DestinationConstants.crptToWindow)
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ShowReport(Crystal.DestinationConstants.crptToPrinter)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub CmdSearchItemCode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearchItemCode.Click
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster("", "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr) = True Then
            txtItemName.Text = AcName
            txtItemCode.Text = AcName1
            If txtItemCode.Enabled = True Then txtItemCode.Focus()
        End If
    End Sub

    Private Sub cmdSearchDept_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearchDept.Click
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , SqlStr) = True Then
            txtDept.Text = AcName1
            txtDeptName.Text = AcName
            If txtDept.Enabled = True Then txtDept.Focus()
        End If
    End Sub

    Private Sub cmdSearchMacNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchMacNo.Click
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster("", "IT_MACHINE_MST", "MACHINE_DESC", "MACHINE_NO", , , SqlStr) = True Then
            txtItemName.Text = AcName
            txtMachineNo.Text = AcName1
            txtMachineNo_Validating(txtMachineNo, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
        End If
    End Sub
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        Call ViewGrid()
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtMachineNo.Focus()

        Else
            ADDMode = False
            MODIFYMode = False
            If RsMMST.EOF = False Then RsMMST.MoveFirst()
            Show1()
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
        ''Resume
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If txtMachineNo.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub
        If RsMMST.EOF Then Exit Sub
        If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
            If Delete1 = False Then GoTo DelErrPart
            If RsMMST.EOF = True Then
                Clear1()
            Else
                Show1()
            End If
        End If
        Exit Sub
DelErrPart:
        MsgBox("Record Not Deleted")
    End Sub

    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr
        SqlStr = ""
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        If InsertIntoDelAudit(PubDBCn, "IT_MACHINE_MST", (txtMachineNo.Text), RsMMST) = False Then GoTo DeleteErr
        If InsertIntoDeleteTrn(PubDBCn, "IT_MACHINE_MST", "MACHINE_NO", (txtMachineNo.Text)) = False Then GoTo DeleteErr

        SqlStr = " DELETE FROM IT_MACHINE_CONFIG_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = " DELETE FROM IT_MACHINE_SOFTWARE_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "'"
        PubDBCn.Execute(SqlStr)


        SqlStr = " DELETE FROM IT_MACHINE_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "'"
        PubDBCn.Execute(SqlStr)


        PubDBCn.CommitTrans()
        RsMMST.Requery()
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans()
        RsMMST.Requery()
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete Transaction Exists Against This MACHINE NO.")
            Exit Function
        End If
        MsgBox(Err.Description)
    End Function

    Private Sub frmITMachineMaster_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub frmITMachineMaster_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = eventArgs.row
        txtMachineNo.Text = SprdView.Text
        txtMachineNo_Validating(txtMachineNo, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Public Sub frmITMachineMaster_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = "Select * From IT_MACHINE_MST Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMMST, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From IT_MACHINE_CONFIG_MST Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMMSTConf, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From IT_MACHINE_SOFTWARE_MST Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMMSTSoftware, ADODB.LockTypeEnum.adLockReadOnly)



        Call SetTextLengths()
        Call AssignGrid(False)
        Call Clear1()

        If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '   Resume
    End Sub

    Public Sub frmITMachineMaster_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RS As ADODB.Recordset

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Left = 0
        Me.Top = 0
        'Me.Height = VB6.TwipsToPixelsY(6180)
        'Me.Width = VB6.TwipsToPixelsX(10920)
        chkMchbkDown.Enabled = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        cboStatus.Items.Clear()
        cboStatus.Items.Add("OPEN/ACTIVE")
        cboStatus.Items.Add("TRANSFER SALE")
        cboStatus.Items.Add("SCRAP SALE")
        cboStatus.Items.Add("CLOSE/INACTIVE")
        cboStatus.SelectedIndex = 0

        cboMaintType.Items.Clear()
        cboMaintType.Items.Add("01. Computer")
        cboMaintType.Items.Add("02. Laptop")
        cboMaintType.Items.Add("03. Printer")
        cboMaintType.Items.Add("04. Scanner")
        cboMaintType.Items.Add("05. Others")
        cboMaintType.Items.Add("06. Network")
        cboMaintType.Items.Add("07. UPS")
        cboMaintType.SelectedIndex = 0



        cboDivision.Items.Clear()

        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboDivision.SelectedIndex = -1

        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmITMachineMaster_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsMMST.Close()
        RsMMST = Nothing

        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim mDivisionCode As Double
        Dim mDivisionDesc As String
        Dim mMaintType As Integer

        Shw = True

        Clear1()

        If Not RsMMST.EOF Then
            txtMachineNo.Text = IIf(IsDbNull(RsMMST.Fields("MACHINE_NO").Value), "", RsMMST.Fields("MACHINE_NO").Value)
            txtInsDate.Text = IIf(IsDbNull(RsMMST.Fields("MACHINE_INST_DATE").Value), "", RsMMST.Fields("MACHINE_INST_DATE").Value)
            txtItemCode.Text = Trim(IIf(IsDbNull(RsMMST.Fields("MACHINE_ITEM_CODE").Value), "", RsMMST.Fields("MACHINE_ITEM_CODE").Value))
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
            If MainClass.ValidateWithMasterTable(txtItemCode.Text, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
                txtItemName.Text = MasterNo
            Else
                txtItemName.Text = ""
            End If
            txtMachineDesc.Text = IIf(IsDbNull(RsMMST.Fields("MACHINE_DESC").Value), "", RsMMST.Fields("MACHINE_DESC").Value)
            txtSpec.Text = IIf(IsDbNull(RsMMST.Fields("MACHINE_SPEC").Value), "", RsMMST.Fields("MACHINE_SPEC").Value)

            txtDept.Text = IIf(IsDbNull(RsMMST.Fields("DEPT_CODE").Value), "", RsMMST.Fields("DEPT_CODE").Value)
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
            If MainClass.ValidateWithMasterTable(txtDept.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , SqlStr) = True Then
                txtDeptName.Text = MasterNo
            Else
                txtDeptName.Text = ""
            End If
            txtLocation.Text = IIf(IsDbNull(RsMMST.Fields("Location").Value), "", RsMMST.Fields("Location").Value)
            txtMake.Text = IIf(IsDbNull(RsMMST.Fields("MAKE").Value), "", RsMMST.Fields("MAKE").Value)
            txtSerialNo.Text = IIf(IsDBNull(RsMMST.Fields("SERIAL_NO").Value), "", RsMMST.Fields("SERIAL_NO").Value)

            chkMchbkDown.CheckState = IIf(RsMMST.Fields("MACHINE_UB").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            If RsMMST.Fields("Status").Value = "O" Then
                cboStatus.Text = "OPEN/ACTIVE"
            ElseIf RsMMST.Fields("Status").Value = "T" Then
                cboStatus.Text = "TRANSFER SALE"
            ElseIf RsMMST.Fields("Status").Value = "S" Then
                cboStatus.Text = "SCRAP SALE"
            ElseIf RsMMST.Fields("Status").Value = "C" Then
                cboStatus.Text = "CLOSE/INACTIVE"
            End If

            mMaintType = CInt(VB.Left(IIf(IsDbNull(RsMMST.Fields("MACHINE_TYPE").Value), "", RsMMST.Fields("MACHINE_TYPE").Value), 2))
            cboMaintType.SelectedIndex = mMaintType - 1


            txtRemarks.Text = IIf(IsDbNull(RsMMST.Fields("REMARKS").Value), "", RsMMST.Fields("REMARKS").Value)
            txtIPAddress.Text = IIf(IsDbNull(RsMMST.Fields("IP_ADDRESS").Value), "", RsMMST.Fields("IP_ADDRESS").Value)
            mDivisionCode = IIf(IsDbNull(RsMMST.Fields("DIV_CODE").Value), -1, RsMMST.Fields("DIV_CODE").Value)

            If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivisionDesc = Trim(MasterNo)
                cboDivision.Text = mDivisionDesc
            End If
            cboDivision.Enabled = True

            Call ShowDetail1()
            Call MakeEnableDeField(False)
        End If
        Shw = False
        ADDMode = False
        MODIFYMode = False

        MainClass.ButtonStatus(Me, XRIGHT, RsMMST, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
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
            txtMachineNo_Validating(txtMachineNo, New System.ComponentModel.CancelEventArgs(False))
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
        Dim mMchbkdown As String
        Dim MKeyMachine As String
        Dim mStatus As String
        Dim mMaintType As String
        Dim mDivisionCode As Double

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        SqlStr = ""

        mMchbkdown = IIf(chkMchbkDown.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mStatus = VB.Left(cboStatus.Text, 1)
        mMaintType = Trim(cboMaintType.Text)

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If

        If ADDMode = True Then
            SqlStr = " INSERT INTO IT_MACHINE_MST ( " & vbCrLf _
                            & " COMPANY_CODE, MACHINE_NO, MACHINE_ITEM_CODE, " & vbCrLf _
                            & " DEPT_CODE, MACHINE_DESC, " & vbCrLf _
                            & " LOCATION, MAKE, MACHINE_INST_DATE," & vbCrLf _
                            & " MACHINE_SPEC," & vbCrLf _
                            & " MACHINE_UB, STATUS,REMARKS,MACHINE_TYPE, " & vbCrLf _
                            & " ADDUSER,ADDDATE,MODUSER,MODDATE,DIV_CODE,IP_ADDRESS,SERIAL_NO) " & vbCrLf _
                            & " VALUES (" & RsCompany.Fields("COMPANY_CODE").Value & ", '" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtItemCode.Text) & "',  " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtDept.Text) & "'," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtMachineDesc.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtLocation.Text) & "', '" & MainClass.AllowSingleQuote(txtMake.Text) & "', " & vbCrLf _
                            & " TO_DATE('" & VB6.Format(txtInsDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(txtSpec.Text) & "', " & vbCrLf _
                            & " '" & mMchbkdown & "', " & vbCrLf _
                            & " '" & mStatus & "','" & MainClass.AllowSingleQuote(txtRemarks.Text) & "','" & mMaintType & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                            & " TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'',''," & mDivisionCode & ",'" & MainClass.AllowSingleQuote(txtIPAddress.Text) & "','" & MainClass.AllowSingleQuote(txtSerialNo.Text) & "') "

        Else
            SqlStr = " UPDATE IT_MACHINE_MST SET " & vbCrLf _
                & " MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "', " & vbCrLf _
                & " MACHINE_ITEM_CODE='" & MainClass.AllowSingleQuote(txtItemCode.Text) & "', " & vbCrLf _
                & " DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "', " & vbCrLf _
                & " MACHINE_DESC='" & MainClass.AllowSingleQuote(txtMachineDesc.Text) & "', " & vbCrLf _
                & " LOCATION='" & MainClass.AllowSingleQuote(txtLocation.Text) & "', " & vbCrLf _
                & " MAKE='" & MainClass.AllowSingleQuote(txtMake.Text) & "', " & vbCrLf _
                & " MACHINE_INST_DATE=TO_DATE('" & VB6.Format(txtInsDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " IP_ADDRESS='" & MainClass.AllowSingleQuote(txtIPAddress.Text) & "'," & vbCrLf _
                & " MACHINE_SPEC='" & MainClass.AllowSingleQuote(txtSpec.Text) & "', " & vbCrLf _
                & " MACHINE_UB='" & mMchbkdown & "', " & vbCrLf _
                & " STATUS='" & mStatus & "', SERIAL_NO='" & MainClass.AllowSingleQuote(txtSerialNo.Text) & "'," & vbCrLf _
                & " MACHINE_TYPE='" & mMaintType & "', " & vbCrLf _
                & " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', DIV_CODE=" & mDivisionCode & "," & vbCrLf _
                & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "'"

        End If

        PubDBCn.Execute(SqlStr)

        If UpdateDetail() = False Then GoTo UpdateError

        PubDBCn.CommitTrans()
        Update1 = True
        Exit Function
UpdateError:
        Update1 = False
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Private Function FieldsVarification() As Boolean
        On Error GoTo VarificationErr
        FieldsVarification = True

        If Trim(txtMachineNo.Text) = "" Then
            MsgInformation("Machine Number is empty, So unable to Save")
            txtMachineNo.Focus()
            FieldsVarification = False
            Exit Function
        Else
            If Len(Trim(txtMachineNo.Text)) < 8 Then
                MsgInformation("Machine Number Cann't be less than 8 charactor, So unable to Save")
                txtMachineNo.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If
        If Trim(txtInsDate.Text) = "" Then
            MsgInformation("Installation date is empty, So unable to Save")
            txtInsDate.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtItemCode.Text) = "" Then
            MsgInformation("Item Code is empty. Cannot Save")
            txtItemCode.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtMachineDesc.Text) = "" Then
            MsgInformation("Item Name is empty. Cannot Save")
            txtItemCode.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtDept.Text) = "" Then
            MsgInformation("Dept Name is empty. Cannot Save")
            txtDept.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(cboMaintType.Text) = "" Then
            MsgInformation("Maintenance Type is empty, So unable to Save")
            cboMaintType.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(cboDivision.Text) = "" Then
            MsgBox("Division Name is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If cboDivision.Enabled = True Then cboDivision.Focus()
            Exit Function
        End If

        '    If Trim(cboMaintType.Text) <> "Preductive" Then
        '        If Trim(txtFrequency.Text) = "" Then
        '            MsgInformation "PM Frequency is empty. Cannot Save."
        '            txtFrequency.SetFocus
        '            FieldsVarification = False
        '            Exit Function
        '        End If
        '        If Trim(txtLastPM.Text) = "" Then
        '            MsgInformation "Last PM Date is empty. Cannot Save."
        '            txtLastPM.SetFocus
        '            FieldsVarification = False
        '            Exit Function
        '        End If
        '    End If
        If Trim(cboStatus.Text) = "" Then
            MsgInformation("Status is empty, So unable to Save")
            cboStatus.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Machine Details or modify an existing item")
            FieldsVarification = False
        End If
        If MODIFYMode = True And RsMMST.EOF = True Then Exit Function
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
VarificationErr:
        FieldsVarification = False
        MsgInformation(Err.Description)
    End Function
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        MainClass.ClearGrid(SprdView)

        SqlStr = " SELECT MACHINE_NO,MACHINE_ITEM_CODE CODE,MACHINE_DESC,DEPT_CODE," & vbCrLf & " MAKE,MACHINE_INST_DATE," & vbCrLf & " MACHINE_UB,LOCATION,MACHINE_SPEC,MACHINE_TYPE,IP_ADDRESS" & vbCrLf & " FROM IT_MACHINE_MST " & vbCrLf & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY MACHINE_NO"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
    End Sub

    Private Sub ShowReport(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim mTitle As String
        mTitle = ""
        Report1.Reset()

        mTitle = "IT EQUIPMENT MASTER"
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\ITMCHMaster.rpt"

        SetCrpt(Report1, Mode, 1, mTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub txtDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchDept_Click(cmdSearchDept, New System.EventArgs())
    End Sub

    Private Sub txtDept_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDept.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDept.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(txtDept.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Deparment Does Not Exist In Master.")
            Cancel = True
        Else
            txtDeptName.Text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtIPAddress_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtIPAddress.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLocation_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLocation.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLocation_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLocation.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtLocation.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtInsDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInsDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtInsDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtInsDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtInsDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtInsDate.Text) Then
            MsgBox("Not a Valid Date")
            Cancel = True
        End If
        txtInsDate.Text = VB6.Format(txtInsDate.Text, "DD/MM/YYYY")
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtItemCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtItemCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call CmdSearchItemCode_Click(CmdSearchItemCode, New System.EventArgs())
    End Sub

    Private Sub txtItemCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtItemCode.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(txtItemCode.Text, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Item Code Does Not Exist In Master.")
            Cancel = True
        Else
            txtItemName.Text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtMachineDesc_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMachineDesc.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMachineDesc_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMachineDesc.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtMachineDesc.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Public Sub txtMachineNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMachineNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1

        If Trim(txtMachineNo.Text) = "" Then GoTo EventExitSub

        If MODIFYMode = True And RsMMST.EOF = False Then xMachineno = RsMMST.Fields("MACHINE_NO").Value
        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf _
                & " FROM IT_MACHINE_MST" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
                & " AND MACHINE_NO='" & MainClass.AllowSingleQuote(txtMachineNo.Text) & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMMST, ADODB.LockTypeEnum.adLockReadOnly)

        If RsMMST.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            xMachineno = RsMMST.Fields("MACHINE_NO").Value
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Such Machine No Does Not Exist" & vbCrLf & "Click Add To Add for New.", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = " SELECT * " & vbCrLf & " FROM IT_MACHINE_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MACHINE_NO='" & MainClass.AllowSingleQuote(xMachineno) & "' "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMMST, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If

        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub SetTextLengths()
        On Error GoTo ERR1
        txtMachineNo.Maxlength = RsMMST.Fields("MACHINE_NO").DefinedSize
        txtInsDate.Maxlength = RsMMST.Fields("MACHINE_INST_DATE").DefinedSize - 6
        txtItemCode.Maxlength = RsMMST.Fields("MACHINE_ITEM_CODE").DefinedSize
        txtItemName.Maxlength = 255
        txtMachineDesc.Maxlength = RsMMST.Fields("MACHINE_DESC").DefinedSize
        txtSpec.Maxlength = RsMMST.Fields("MACHINE_SPEC").DefinedSize

        txtDept.Maxlength = RsMMST.Fields("DEPT_CODE").DefinedSize
        txtDeptName.Maxlength = 255
        txtLocation.Maxlength = RsMMST.Fields("LOCATION").DefinedSize
        txtMake.MaxLength = RsMMST.Fields("MAKE").DefinedSize
        txtSerialNo.MaxLength = RsMMST.Fields("SERIAL_NO").DefinedSize
        txtIPAddress.Maxlength = RsMMST.Fields("IP_ADDRESS").DefinedSize
        txtRemarks.Maxlength = RsMMST.Fields("REMARKS").Precision
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub txtItemCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemCode.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtItemCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItemCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtItemCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtMachineNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMachineNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMachineNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMachineNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtMachineNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtMake_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMake.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMake_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMake.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtMake.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSerialNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSerialNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSerialNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSerialNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSerialNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtSpec_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSpec.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtSpec_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSpec.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSpec.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub SprdConfigMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdConfigMain.Change

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdConfigMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdConfigMain.ClickEvent

        Dim SqlStr As String = ""

        If eventArgs.row = 0 And eventArgs.col = ColName Then
            With SprdConfigMain
                .Row = .ActiveRow
                .Col = ColName
                If MainClass.SearchGridMaster(.Text, "IT_CONFIGUATION_MST", "CONFIGUATION_DESC", "", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow
                    .Col = ColName
                    .Text = AcName

                    MainClass.SetFocusToCell(SprdConfigMain, SprdConfigMain.ActiveRow, ColName)
                End If

            End With
        End If


        If eventArgs.col = 0 And eventArgs.row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdConfigMain, eventArgs.row, ColName)
            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub SprdConfigMain_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdConfigMain.KeyDownEvent

        Dim mActiveCol As Integer
        Dim mValue As String

        mActiveCol = SprdConfigMain.ActiveCol

        If eventArgs.keyCode = System.Windows.Forms.Keys.Return Or eventArgs.keyCode = System.Windows.Forms.Keys.Tab Then
            If mActiveCol = ColName Then
                SprdConfigMain.Row = SprdConfigMain.ActiveRow
                SprdConfigMain.Col = ColName
                mValue = Trim(SprdConfigMain.Text)

                If mValue <> "" And SprdConfigMain.MaxRows = SprdConfigMain.ActiveRow Then
                    MainClass.AddBlankSprdRow(SprdConfigMain, ColName, ConRowHeight)
                End If
            End If
        End If
        eventArgs.keyCode = 9999
    End Sub

    Private Sub SprdConfigMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdConfigMain.LeaveCell
        On Error GoTo ErrPart
        Dim mFrequency As Short
        Dim mLastPM As String

        If eventArgs.newRow = -1 Then Exit Sub

        With SprdConfigMain
            .Row = .ActiveRow
            Select Case eventArgs.col
                Case ColName
                    .Col = ColName
                    If Trim(.Text) = "" Then Exit Sub

                    If MainClass.ValidateWithMasterTable(Trim(SprdConfigMain.Text), "CONFIGUATION_DESC", "CONFIGUATION_DESC", "IT_CONFIGUATION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                        MsgInformation("Invalid Name.")
                        MainClass.SetFocusToCell(SprdConfigMain, SprdConfigMain.ActiveRow, ColName)
                        Exit Sub
                    End If

                    If DuplicateConfigCheck() = False Then
                        FormatSprdMain(-1)
                    End If

            End Select
        End With
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Function DuplicateConfigCheck() As Boolean

        Dim cntRow As Integer
        Dim mCount As Byte
        Dim mCheckName As String
        Dim mName As String

        DuplicateConfigCheck = False

        With SprdConfigMain
            .Row = .ActiveRow
            .Col = ColName
            mCheckName = Trim(UCase(.Text))

            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColName
                mName = Trim(UCase(.Text))

                If (mName = mCheckName And mCheckName <> "") Then
                    mCount = mCount + 1
                End If

                If mCount > 1 Then
                    DuplicateConfigCheck = True
                    MsgInformation("Duplicate Name : " & mCheckName)
                    MainClass.SetFocusToCell(SprdConfigMain, .ActiveRow, ColName)
                    Exit Function
                End If
            Next
        End With
    End Function

    Private Sub SprdSoftwareMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdSoftwareMain.Change
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdSoftwareMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdSoftwareMain.ClickEvent

        Dim SqlStr As String = ""


        If eventArgs.row = 0 And eventArgs.col = ColName Then
            With SprdSoftwareMain
                .Row = .ActiveRow
                .Col = ColName
                If MainClass.SearchGridMaster(.Text, "IT_SOFTWARE_MST", "SOFTWARE_DESC", "", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow
                    .Col = ColName
                    .Text = AcName

                    MainClass.SetFocusToCell(SprdSoftwareMain, SprdSoftwareMain.ActiveRow, ColName)
                End If

            End With
        End If

        If eventArgs.col = 0 And eventArgs.row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdSoftwareMain, eventArgs.row, ColName)
            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub SprdSoftwareMain_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdSoftwareMain.KeyDownEvent

        Dim mActiveCol As Integer
        Dim mName As String

        mActiveCol = SprdSoftwareMain.ActiveCol

        If eventArgs.keyCode = System.Windows.Forms.Keys.Return Or eventArgs.keyCode = System.Windows.Forms.Keys.Tab Then
            If mActiveCol = ColName Then
                SprdSoftwareMain.Row = SprdSoftwareMain.ActiveRow
                SprdSoftwareMain.Col = ColName
                mName = Trim(SprdSoftwareMain.Text)

                If mName <> "" And SprdSoftwareMain.MaxRows = SprdSoftwareMain.ActiveRow Then
                    MainClass.AddBlankSprdRow(SprdSoftwareMain, ColName, ConRowHeight)
                End If
            End If
        End If
        eventArgs.keyCode = 9999
    End Sub

    Private Sub SprdSoftwareMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdSoftwareMain.LeaveCell
        On Error GoTo ErrPart

        Dim mName As String

        If eventArgs.newRow = -1 Then Exit Sub

        With SprdSoftwareMain
            .Row = .ActiveRow
            Select Case eventArgs.col
                Case ColName
                    .Col = ColName
                    If Trim(.Text) = "" Then Exit Sub

                    If MainClass.ValidateWithMasterTable(Trim(SprdSoftwareMain.Text), "SOFTWARE_DESC", "SOFTWARE_DESC", "IT_SOFTWARE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                        MsgInformation("Invalid Name.")
                        MainClass.SetFocusToCell(SprdSoftwareMain, SprdSoftwareMain.ActiveRow, ColName)
                        Exit Sub
                    End If

                    If DuplicateCheck() = False Then
                        FormatSprdMain(-1)
                    End If

            End Select
        End With
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Function DuplicateCheck() As Boolean

        Dim cntRow As Integer
        Dim mCount As Byte
        Dim mCheckName As String
        Dim mName As String

        DuplicateCheck = False

        With SprdSoftwareMain
            .Row = .ActiveRow
            .Col = ColName
            mCheckName = Trim(UCase(.Text))

            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColName
                mName = Trim(UCase(.Text))

                If (mName = mCheckName And mCheckName <> "") Then
                    mCount = mCount + 1
                End If

                If mCount > 1 Then
                    DuplicateCheck = True
                    MsgInformation("Duplicate Check Type : " & mCheckName)
                    MainClass.SetFocusToCell(SprdSoftwareMain, .ActiveRow, ColName)
                    Exit Function
                End If
            Next
        End With
    End Function
End Class
