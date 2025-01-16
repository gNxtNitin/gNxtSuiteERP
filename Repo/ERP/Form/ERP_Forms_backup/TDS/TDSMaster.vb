Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmTDSMaster
    Inherits System.Windows.Forms.Form
    Dim RsTDS As ADODB.Recordset
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection				

    Dim Shw As Boolean
    Dim FormActive As Boolean
    Dim xCode As String
    Dim SqlStr As String
    Private Sub ViewGrid()


        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        ADataGrid.Refresh				
            FormatSprdView()
            SprdView.Refresh()
            FraGridView.Visible = True
            FraView.Visible = False
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraGridView.Visible = False
            FraView.Visible = True
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsTDS, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub Clear1()

        TxtName.Text = ""
        txtaddress1.Text = ""
        TxtCity.Text = ""
        txtstate.Text = ""
        txtpincode.Text = ""
        txtPAN.Text = ""
        txtSection.Text = ""
        txtExptionCNo.Text = ""
        txtTDSRate.Text = ""

        OptStatus(0).Checked = True
        TxtName.Enabled = True
        cboCType.SelectedIndex = 0
        MainClass.ButtonStatus(Me, XRIGHT, RsTDS, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub cboCType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCType.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboCType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCType.SelectedIndexChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            TxtName.Enabled = False
            MainClass.ButtonStatus(Me, XRIGHT, RsTDS, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            TxtName.Enabled = True
            Show1()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPreview.Click
        ShowReport(Crystal.DestinationConstants.crptToWindow)
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ShowReport(Crystal.DestinationConstants.crptToPrinter)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdSecSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSecSearch.Click
        Dim mFieldName As String
        If MainClass.SearchMaster(txtSection.Text, "TDS_Section_MST", "NAME", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtSection.Text = AcName
            txtSection.Focus()
        End If
    End Sub
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            TxtName.Focus()
        Else
            ADDMode = False
            MODIFYMode = False
            If RsTDS.EOF = False Then RsTDS.MoveFirst()
            Show1()
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
        ''Resume				
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart

        If PubSuperUser <> "S" Then
            Exit Sub
        End If

        If TxtName.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub
        If RsTDS.EOF Then Exit Sub
        If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.				
            If Delete1() = False Then GoTo DelErrPart
            If RsTDS.EOF = True Then
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
        Dim mCode As String

        If MainClass.ValidateWithMasterTable(TxtName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCode = MasterNo
        Else
            mCode = -1
        End If


        SqlStr = ""

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If InsertIntoDelAudit(PubDBCn, "FIN_SUPP_CUST_MST", (TxtName.Text), RsTDS, "", "D") = False Then GoTo DeleteErr
        If InsertIntoDeleteTrn(PubDBCn, "FIN_SUPP_CUST_MST", "SUPP_CUST_CODE", mCode) = False Then GoTo DeleteErr

        SqlStr = "Delete from FIN_SUPP_CUST_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & mCode & "'"
        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        RsTDS.Requery()
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans()
        RsTDS.Requery()
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete Transaction Exists Against This Unit")
            Exit Function
        End If
        MsgBox(Err.Description)
    End Function
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        Dim mFieldName As String
        If MainClass.SearchMaster(TxtName.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            TxtName.Text = AcName
            If TxtName.Enabled = True Then TxtName.Focus()
        End If
    End Sub
    Private Sub frmTDSMaster_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick

        SprdView.Col = 1
        SprdView.Row = eventArgs.row
        SqlStr = ""

        SqlStr = " SELECT ACM.SUPP_CUST_NAME AS AccountName " & vbCrLf _
            & " FROM FIN_SUPP_CUST_MST ACM " & vbCrLf _
            & " WHERE ACM.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND ACM.SUPP_CUST_NAME = '" & MainClass.AllowSingleQuote(SprdView.Text) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTDS, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTDS.EOF = False Then
            TxtName.Text = SprdView.Text
            TxtName_Validating(TxtName, New System.ComponentModel.CancelEventArgs(False))
            CmdView_Click(CmdView, New System.EventArgs())
        End If
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub
    Private Sub OptStatus_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptStatus.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptStatus.GetIndex(eventSender)

            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub txtaddress1_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtaddress1.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtaddress1_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtaddress1.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtaddress1.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub TxtCity_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtCity.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtCity_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtCity.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtCity.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub TxtName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub frmTDSMaster_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        SqlStr = " Select * " & vbCrLf & " From FIN_SUPP_CUST_MST ACM " & vbCrLf & " Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTDS, ADODB.LockTypeEnum.adLockReadOnly)
        SetTextLengths()
        AssignGrid(False)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If CmdAdd.Enabled = True Then cmdAdd_Click(CmdAdd, New System.EventArgs())
        Clear1()
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmTDSMaster_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection				
        ''PvtDBCn.Open StrConn				
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Left = 0
        Me.Top = 0
        'Me.Height = VB6.TwipsToPixelsY(5085)
        'Me.Width = VB6.TwipsToPixelsX(8355)
        FillCTypeCombo()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmTDSMaster_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        RsTDS = Nothing
        Me.Hide()
        Me.Dispose()
        Me.Close()

        '    PubDBCn.Cancel				
        '    PvtDBCn.Close				
        '    Set PvtDBCn = Nothing				
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Shw = True
        If Not RsTDS.EOF Then
            TxtName.Text = IIf(IsDBNull(RsTDS.Fields("SUPP_CUST_NAME").Value), "", RsTDS.Fields("SUPP_CUST_NAME").Value)

            txtaddress1.Text = IIf(IsDBNull(RsTDS.Fields("SUPP_CUST_ADDR").Value), "", RsTDS.Fields("SUPP_CUST_ADDR").Value)
            TxtCity.Text = IIf(IsDBNull(RsTDS.Fields("SUPP_CUST_CITY").Value), "", RsTDS.Fields("SUPP_CUST_CITY").Value)
            txtstate.Text = IIf(IsDBNull(RsTDS.Fields("SUPP_CUST_STATE").Value), "", RsTDS.Fields("SUPP_CUST_STATE").Value)
            txtpincode.Text = IIf(IsDBNull(RsTDS.Fields("SUPP_CUST_PIN").Value), "", RsTDS.Fields("SUPP_CUST_PIN").Value)

            If RsTDS.Fields("CType").Value = "C" Then
                cboCType.SelectedIndex = 0
            Else
                cboCType.SelectedIndex = 1
            End If

            If MainClass.ValidateWithMasterTable(RsTDS.Fields("SECTIONCODE").Value, "Code", "Name", "TDS_Section_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtSection.Text = IIf(IsDBNull(MasterNo), "", MasterNo)
            End If

            txtExptionCNo.Text = IIf(IsDBNull(RsTDS.Fields("EXPTIONCNO").Value), "", RsTDS.Fields("EXPTIONCNO").Value)
            txtPAN.Text = IIf(IsDBNull(RsTDS.Fields("PAN_NO").Value), "", RsTDS.Fields("PAN_NO").Value)
            txtTDSRate.Text = VB6.Format(IIf(IsDBNull(RsTDS.Fields("TDS_PER").Value), 0, RsTDS.Fields("TDS_PER").Value), "0.000")

            OptStatus(0).Checked = IIf(RsTDS.Fields("Status").Value = "O", True, False)
            OptStatus(1).Checked = IIf(RsTDS.Fields("Status").Value = "C", True, False)

            xCode = RsTDS.Fields("SUPP_CUST_CODE").Value
            TxtName.Enabled = False
        End If
        Shw = False
        ADDMode = False
        MODIFYMode = False

        MainClass.ButtonStatus(Me, XRIGHT, RsTDS, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        If Update1() = True Then
            TxtName_Validating(TxtName, New System.ComponentModel.CancelEventArgs(False))
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
        Dim mStatus As String
        Dim mAccountCode As String
        Dim mSectionCode As Integer
        Dim mCTYPE As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        mStatus = IIf(OptStatus(0).Checked = True, "O", "C")
        SqlStr = ""

        If MainClass.ValidateWithMasterTable(TxtName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mAccountCode = MasterNo
        Else
            mAccountCode = -1
        End If

        If MainClass.ValidateWithMasterTable(txtSection.Text, "Name", "Code", "TDS_Section_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mSectionCode = MasterNo
        Else
            mSectionCode = -1
        End If


        mCTYPE = VB.Left(cboCType.Text, 1)

        SqlStr = " UPDATE FIN_SUPP_CUST_MST SET " & vbCrLf & " SUPP_CUST_ADDR='" & MainClass.AllowSingleQuote(txtaddress1.Text) & "', " & vbCrLf & " SUPP_CUST_CITY    ='" & MainClass.AllowSingleQuote(TxtCity.Text) & "', " & vbCrLf & " SUPP_CUST_PIN ='" & MainClass.AllowSingleQuote(txtpincode.Text) & "', " & vbCrLf & " SUPP_CUST_STATE   ='" & MainClass.AllowSingleQuote(txtstate.Text) & "', " & vbCrLf & " PAN_NO ='" & MainClass.AllowSingleQuote(txtPAN.Text) & "'," & vbCrLf & " SECTIONCODE=" & mSectionCode & ", " & vbCrLf & " EXPTIONCNO='" & MainClass.AllowSingleQuote(Trim(txtExptionCNo.Text)) & "', " & vbCrLf & " CTYPE='" & mCTYPE & "', " & vbCrLf & " TDS_PER=" & Val(txtTDSRate.Text) & ", " & vbCrLf & " STATUS='" & mStatus & "', " & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
            & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & xCode & "'"


        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()
        '    ADataGrid.Refresh				
        RsTDS.Requery()
        Update1 = True
        Exit Function
UpdateError:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsTDS.Requery()
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        PubDBCn.Errors.Clear()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Sub TxtName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtName.DoubleClick
        cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Sub TxtName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub TxtName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Function FieldsVarification() As Boolean
        On Error GoTo VarificationErr
        FieldsVarification = True
        If Trim(TxtName.Text) = "" Then
            MsgInformation("Name is empty. Cannot Save")
            TxtName.Focus()
            FieldsVarification = False
            Exit Function
        Else
            If MainClass.ValidateWithMasterTable(TxtName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgBox("Account Name Does Not Exist In Master ", vbInformation)
                TxtName.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If Trim(txtSection.Text) = "" Then
            MsgInformation("Section is empty. Cannot Save")
            txtSection.Focus()
            FieldsVarification = False
            Exit Function
        Else
            If MainClass.ValidateWithMasterTable(txtSection.Text, "Name", "Code", "TDS_Section_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgBox("Section Does Not Exist In Master ", vbInformation)
                txtSection.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If Val(txtTDSRate.Text) >= 100 Then
            MsgInformation("TDS Rate Cann't be Greater Than 100.")
            txtTDSRate.Focus()
            FieldsVarification = False
            Exit Function
        ElseIf Val(txtTDSRate.Text) < 0 Then
            MsgInformation("TDS Rate Cann't be Less Than Zero.")
            txtTDSRate.Focus()
            FieldsVarification = False
            Exit Function
        ElseIf Val(txtTDSRate.Text) = 0 Then
            If Trim(txtExptionCNo.Text) = "" Then
                MsgInformation("Exeption Certification No. is empty. Cannot Save.")
                txtExptionCNo.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
        End If
        If MODIFYMode = True And RsTDS.EOF = True Then Exit Function
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
VarificationErr:
        FieldsVarification = False
        MsgInformation(Err.Description)
    End Function
    Private Sub AssignGrid(ByRef mRefresh As Boolean)


        SqlStr = " SELECT ACM.SUPP_CUST_NAME AS ACCOUNTNAME, " & vbCrLf & " DECODE(ACM.CTYPE,'C','COMPANY','NON-COMPANY') AS TYPE," & vbCrLf & " TDSSECTION.NAME AS SECTIONNAME, ACM.EXPTIONCNO, " & vbCrLf & " ACM.PAN_NO As PANNO, ACM.TDS_PER, " & vbCrLf & " DECODE(ACM.STATUS,'O','OPEN','CLOSE') AS STATUS " & vbCrLf & " FROM FIN_SUPP_CUST_MST ACM, TDS_Section_MST TDSSECTION " & vbCrLf & " Where ACM.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ACM.COMPANY_CODE=TDSSECTION.COMPANY_CODE " & vbCrLf & " AND ACM.SECTIONCODE=TDSSECTION.CODE "

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()

    End Sub

    Private Sub ShowReport(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim mTitle As String
        mTitle = ""
        Report1.Reset()

        mTitle = "TDS MASTER"
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\TDSMaster.rpt"

        SetCrpt(Report1, Mode, 1, mTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub TxtName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1

        If Trim(TxtName.Text) = "" Then GoTo EventExitSub

        If MODIFYMode = True And RsTDS.EOF = False Then xCode = RsTDS.Fields("SUPP_CUST_CODE").Value
        SqlStr = ""
        SqlStr = " SELECT ACM.* " & vbCrLf _
                & " FROM FIN_SUPP_CUST_MST ACM " & vbCrLf _
                & " WHERE ACM.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND ACM.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(Trim(TxtName.Text)) & "' "


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTDS, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTDS.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            xCode = RsTDS.Fields("SUPP_CUST_CODE").Value
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Account Name Does Not Exist In Master" & vbCrLf & "Click Add To Add In Master", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = " SELECT ACM.* " & vbCrLf & " FROM FIN_SUPP_CUST_MST ACM " & vbCrLf & " WHERE ACM.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ACM.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(Trim(xCode)) & "' "

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTDS, ADODB.LockTypeEnum.adLockReadOnly)
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
        TxtName.MaxLength = RsTDS.Fields("SUPP_CUST_NAME").DefinedSize
        txtaddress1.MaxLength = RsTDS.Fields("SUPP_CUST_ADDR").DefinedSize
        TxtCity.MaxLength = RsTDS.Fields("SUPP_CUST_CITY").DefinedSize
        txtstate.MaxLength = RsTDS.Fields("SUPP_CUST_STATE").DefinedSize
        txtpincode.MaxLength = RsTDS.Fields("SUPP_CUST_PIN").DefinedSize
        txtSection.MaxLength = MainClass.SetMaxLength("Code", "TDS_Section_MST", PubDBCn)
        txtExptionCNo.MaxLength = RsTDS.Fields("EXPTIONCNO").DefinedSize
        txtPAN.MaxLength = RsTDS.Fields("PAN_NO").DefinedSize
        txtTDSRate.MaxLength = RsTDS.Fields("TDS_PER").Precision

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_ColWidth(0, 0)
            .set_ColWidth(1, 3500)
            .set_ColWidth(2, 1500)
            .set_ColWidth(3, 1500)
            .set_ColWidth(4, 1500)
            .set_ColWidth(5, 1500)
            .set_ColWidth(6, 900)
            .set_ColWidth(7, 900)
            .set_ColWidth(8, 900)
            .set_ColWidth(9, 900)
            .set_ColWidth(10, 900)
            .set_ColWidth(11, 900)

            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub

    Private Sub txtPan_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPAN.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPAN_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPAN.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPAN.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtpincode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtpincode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtpincode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtpincode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtSection_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSection.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSection_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSection.DoubleClick
        cmdSecSearch_Click(cmdSecSearch, New System.EventArgs())
    End Sub

    Private Sub txtSection_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSection.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSection.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSection_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSection.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSecSearch_Click(cmdSecSearch, New System.EventArgs())
    End Sub

    Private Sub txtSection_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSection.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset
        Dim SqlStr As String
        Dim mSectionCode As Integer
        Dim mTdsRate As Double

        If Trim(txtSection.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtSection.Text, "Name", "Code", "TDS_Section_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Secion Name Does Not Exist In Master", vbInformation)
            Cancel = True
            Exit Sub
        Else
            mSectionCode = MasterNo
        End If


        mTdsRate = CalcTDSRate(mSectionCode, VB.Left(cboCType.Text, 1), PubDBCn)

        txtTDSRate.Text = VB6.Format(IIf(Trim(txtTDSRate.Text) = "", mTdsRate, txtTDSRate.Text), "0.00")

        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtExptionCNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtExptionCNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtExptionCNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtExptionCNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtExptionCNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtstate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtstate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtstate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtstate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtTdsRate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTDSRate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTdsRate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTDSRate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub




    Private Sub FillCTypeCombo()
        cboCType.Items.Clear()
        cboCType.Items.Add("COMPANY")
        cboCType.Items.Add("NON-COMPANY")
        cboCType.SelectedIndex = 0
    End Sub
End Class
