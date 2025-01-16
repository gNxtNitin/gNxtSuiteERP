Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmTDSSectionWithRate
    Inherits System.Windows.Forms.Form
    Dim RsTDSSection As ADODB.Recordset
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection				
    Dim xCode As Integer
    Dim SqlStr As String
    Dim FormActive As Boolean
    Private Sub ViewGrid()
        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        ADataMain.Refresh				
            FormatSprdView()
            SprdView.Refresh()

            SprdView.Focus()
            Fragridview.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            Fragridview.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsTDSSection, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub Clear1()
        txtName.Text = ""
        TxtNature.Text = ""
        txtSectionCode.Text = ""
        cboFormNo.SelectedIndex = -1
        txtBRate.Text = ""
        txtSurcharge.Text = ""
        txtNCBRate.Text = ""
        txtNCSurcharge.Text = ""
        txtWEF.Text = ""
        lblWEF.Text = ""
        OptStatus(0).Checked = True
        MainClass.ButtonStatus(Me, XRIGHT, RsTDSSection, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click
        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsTDSSection, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ShowReport(Crystal.DestinationConstants.crptToWindow)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ShowReport(Crystal.DestinationConstants.crptToPrinter)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtName.Focus()
        Else
            ADDMode = False
            MODIFYMode = False
            If RsTDSSection.EOF = False Then RsTDSSection.MoveFirst()
            Show1()
        End If
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If txtName.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub
        If Not RsTDSSection.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.				
                If Delete1() = False Then GoTo DelErrPart
                If RsTDSSection.EOF = True Then
                    Clear1()
                Else
                    Clear1()
                    Show1()
                End If
            End If
        End If
        Exit Sub
DelErrPart:
        MsgBox("Record Not Deleted")
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim ii As Integer

        SqlStr = " SELECT TDSSECTION.NAME || ' : ' || TO_CHAR(TDSRATE.WEF,'DD/MM/YYYY') AS NAME" & vbCrLf & " From TDS_Section_MST TDSSECTION, TDS_Rate_MST TDSRATE " & vbCrLf & " Where TDSSECTION.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TDSRATE.FYEAR(+) = " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND TDSSECTION.COMPANY_CODE = TDSRATE.COMPANY_CODE(+) " & vbCrLf & " AND TDSSECTION.CODE = TDSRATE.CODE(+) "

        If MainClass.SearchBySQL(SqlStr, "NAME") = True Then
            ii = InStr(1, AcName, ":", vbTextCompare)
            txtName.Text = Trim(VB.Left(AcName, ii - 1))
            txtWEF.Text = Trim(Mid(AcName, ii + 1))
            TxtName_Validating(txtName, New System.ComponentModel.CancelEventArgs(False)) ''_Validate False
            txtWEF.Focus()
        End If

        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub frmTDSSectionWithRate_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub frmTDSSectionWithRate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub OptStatus_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptStatus.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptStatus.GetIndex(eventSender)
            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SqlStr = ""
        SprdView.Row = SprdView.ActiveRow

        SprdView.Col = 1
        txtName.Text = SprdView.Text

        SprdView.Col = 3
        txtWEF.Text = SprdView.Text

        TxtName_Validating(txtName, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub
    Private Sub txtBRate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBRate.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBRate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBRate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub cboFormNo_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboFormNo.SelectedIndexChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub TxtName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub TxtName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtName.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub frmTDSSectionWithRate_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        SqlStr = ""
        If FormActive = True Then Exit Sub
        SqlStr = "SELECT * " & vbCrLf & " FROM TDS_Section_MST TDSSECTION,TDS_Rate_MST TDSRATE  " & vbCrLf & " WHERE TDSSECTION.CODE=TDSRATE.CODE AND 1<>1"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTDSSection, ADODB.LockTypeEnum.adLockReadOnly)
        Call AssignGrid(False)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        SetTextLength()
        Clear1()
        If CmdAdd.Enabled = True Then cmdAdd_Click(CmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmTDSSectionWithRate_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
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
        'Me.Height = VB6.TwipsToPixelsY(4500)
        'Me.Width = VB6.TwipsToPixelsX(8355)

        cboFormNo.Items.Clear()
        cboFormNo.Items.Add("26A")
        cboFormNo.Items.Add("26B")
        cboFormNo.Items.Add("26C")
        cboFormNo.Items.Add("26D")
        cboFormNo.Items.Add("26E")
        cboFormNo.Items.Add("26F")
        cboFormNo.Items.Add("26G")
        cboFormNo.Items.Add("26H")
        cboFormNo.Items.Add("26I")
        cboFormNo.Items.Add("26J")
        cboFormNo.Items.Add("26K")
        cboFormNo.Items.Add("27A")

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmTDSSectionWithRate_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsTDSSection = Nothing
        frmTDSSection = Nothing
        '    PubDBCn.Cancel				
        '    PvtDBCn.Close				
        '    Set PvtDBCn = Nothing				
    End Sub
    Private Sub Show1()
        On Error GoTo ShowErrPart
        If Not RsTDSSection.EOF Then
            txtName.Text = IIf(IsDBNull(RsTDSSection.Fields("Name").Value), "", RsTDSSection.Fields("Name").Value)
            TxtNature.Text = IIf(IsDBNull(RsTDSSection.Fields("NATURE").Value), "", RsTDSSection.Fields("NATURE").Value)
            txtSectionCode.Text = IIf(IsDBNull(RsTDSSection.Fields("SECTIONCODE").Value), "", RsTDSSection.Fields("SECTIONCODE").Value)
            xCode = IIf(IsDBNull(RsTDSSection.Fields("CODE").Value), -1, RsTDSSection.Fields("CODE").Value)

            Call ShowTDSRATE(xCode)
        End If

        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsTDSSection, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub ShowTDSRATE(ByRef pSectionCode As Integer)
        On Error GoTo ShowErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mWef As ADODB.Recordset
        Dim I As Integer

        If Trim(txtWEF.Text) = "" Then Exit Sub
        SqlStr = "Select * from TDS_Rate_MST  WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR =" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND CODE=" & pSectionCode & " AND WEF=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsTemp.EOF Then

            For I = cboFormNo.Items.Count - 1 To -1 Step -1
                cboFormNo.SelectedIndex = I
                If cboFormNo.Text = IIf(IsDBNull(RsTemp.Fields("FORMNO").Value), "", RsTemp.Fields("FORMNO").Value) Then
                    Exit For
                End If
            Next
            ''cboFormNo.Text = IIf(IsNull(RsTemp!FORMNO), "", RsTemp!FORMNO)				
            txtBRate.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("BASICRATEPER").Value), "", RsTemp.Fields("BASICRATEPER").Value), "0.00")
            txtSurcharge.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("SURCHARGEPER").Value), "", RsTemp.Fields("SURCHARGEPER").Value), "0.00")

            txtNCBRate.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("NCBASICRATEPER").Value), "", RsTemp.Fields("NCBASICRATEPER").Value), "0.00")
            txtNCSurcharge.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("NCSURCHARGEPER").Value), "", RsTemp.Fields("NCSURCHARGEPER").Value), "0.00")
            txtWEF.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("WEF").Value), "", RsTemp.Fields("WEF").Value), "DD/MM/YYYY")
            lblWEF.Text = VB6.Format(IIf(IsDBNull(RsTemp.Fields("WEF").Value), "", RsTemp.Fields("WEF").Value), "DD/MM/YYYY")

        End If
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
            ADDMode = False
            MODIFYMode = False
            TxtName_Validating(txtName, New System.ComponentModel.CancelEventArgs(False))
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

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mStatus = IIf(OptStatus(0).Checked = True, "O", "C")


        If ADDMode = True Then
            xCode = MainClass.AutoGenRowNo("TDS_Section_MST", "Code", PubDBCn)

            SqlStr = " INSERT INTO TDS_Section_MST ( " & vbCrLf & " COMPANY_CODE, Code, Name, Nature, Status, EDITRF, " & vbCrLf & " SECTIONCODE, " & vbCrLf & " AddUser, AddDate, ModUser, ModDate ) VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & xCode & ", '" & MainClass.AllowSingleQuote(Trim(txtName.Text)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(Trim(TxtNature.Text)) & "', " & vbCrLf & " '" & mStatus & "', 'N', " & vbCrLf & " '" & MainClass.AllowSingleQuote(Trim(txtSectionCode.Text)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                & " TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"

        Else

            SqlStr = "DELETE FROM TDS_Rate_MST  WHERE  COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR = " & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " CODE='" & xCode & "' AND  " & vbCrLf _
                & " WEF=TO_DATE('" & VB6.Format(lblWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
            PubDBCn.Execute(SqlStr)

            SqlStr = " UPDATE TDS_Section_MST SET " & vbCrLf & " NAME = '" & MainClass.AllowSingleQuote(Trim(txtName.Text)) & "', " & vbCrLf & " Nature = '" & MainClass.AllowSingleQuote(Trim(TxtNature.Text)) & "', " & vbCrLf & " SECTIONCODE = '" & MainClass.AllowSingleQuote(Trim(txtSectionCode.Text)) & "', " & vbCrLf & " Status = '" & mStatus & "', " & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                & " ModDate=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " AND CODE='" & xCode & "'"

        End If

        PubDBCn.Execute(SqlStr)

        If UpdateTDSRate(xCode) = False Then GoTo UpdateError

        PubDBCn.CommitTrans()
        Update1 = True
        Exit Function
UpdateError:
        Update1 = False
        PubDBCn.RollbackTrans()
        '    MsgBox err.Description + " Error No.: " + Str(err.Number)				
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        PubDBCn.Errors.Clear()
        RsTDSSection.Requery()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Sub TxtName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtName.DoubleClick
        cmdsearch_Click(CmdSearch, New System.EventArgs())
    End Sub
    Private Sub TxtName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(CmdSearch, New System.EventArgs())
    End Sub
    Private Function FieldsVarification() As Boolean
        On Error GoTo ERR1
        SqlStr = ""
        FieldsVarification = True
        If Trim(txtName.Text) = "" Then
            MsgInformation("Name is empty. Cannot Save")
            txtName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(TxtNature.Text) = "" Then
            MsgInformation("Nature is empty. Cannot Save")
            TxtNature.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(cboFormNo.Text) = "" Then
            MsgInformation("Form No is empty. Cannot Save")
            cboFormNo.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtSectionCode.Text) = "" Then
            MsgInformation("Section Code is empty. Cannot Save")
            txtSectionCode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Val(txtBRate.Text) > 100 Then
            MsgInformation("Basic Rate Cannot be Greater Than 100. Cannot Save")
            txtBRate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Val(txtSurcharge.Text) > 100 Then
            MsgInformation("Surcharge Cannot be Greater Than 100. Cannot Save")
            txtSurcharge.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Val(txtNCBRate.Text) > 100 Then
            MsgInformation("Basic Rate Cannot be Greater Than 100. Cannot Save")
            txtNCBRate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Val(txtNCSurcharge.Text) > 100 Then
            MsgInformation("Surcharge Cannot be Greater Than 100. Cannot Save")
            txtNCSurcharge.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtWEF.Text) = "" Then
            MsgInformation("WEF Date cann't be blank. Cannot Save")
            txtWEF.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Not IsDate(txtWEF.Text) Then
            MsgInformation("Invalid WEF Date. Cannot Save")
            txtWEF.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
        End If
        If MODIFYMode = True And RsTDSSection.EOF = True Then Exit Function
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub SetTextLength()
        On Error GoTo ERR1
        txtName.MaxLength = RsTDSSection.Fields("Name").DefinedSize
        TxtNature.MaxLength = RsTDSSection.Fields("Nature").DefinedSize
        ''    cboFormNo.MaxLength = RsTDSSection.Fields("FormNo").DefinedSize				
        txtSectionCode.MaxLength = RsTDSSection.Fields("SectionCode").DefinedSize
        txtBRate.MaxLength = RsTDSSection.Fields("BasicRatePer").Precision
        txtSurcharge.MaxLength = RsTDSSection.Fields("SurchargePer").Precision
        txtNCBRate.MaxLength = RsTDSSection.Fields("NCBasicRatePer").Precision
        txtNCSurcharge.MaxLength = RsTDSSection.Fields("NCSurchargePer").Precision
        txtWEF.MaxLength = RsTDSSection.Fields("WEF").Precision
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        '' Resume				
    End Sub

    Private Sub TxtName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        If Trim(txtName.Text) = "" Then GoTo EventExitSub

        If MODIFYMode = True And RsTDSSection.EOF = False Then xCode = RsTDSSection.Fields("CODE").Value
        SqlStr = ""

        SqlStr = "SELECT * " & vbCrLf _
            & " FROM TDS_Section_MST TDSSECTION " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND NAME = '" & MainClass.AllowSingleQuote(UCase(txtName.Text)) & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTDSSection, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTDSSection.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("New Entry, Click Add To Add In Master", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = " SELECT * " & vbCrLf & " FROM TDS_SECTION_MST TDSSECTION " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TDSSECTION.CODE = " & xCode & ""

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTDSSection, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        '    Resume				
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        SqlStr = "SELECT " & vbCrLf & " TDSSection.Name, SECTIONCODE, TO_CHAR(TDSRate.WEF,'DD/MM/YYYY') AS WEF, TDSSection.Nature, " & vbCrLf & " TDSRate.FormNo,TDSRate.BasicRatePer,TDSRate.SurchargePer," & vbCrLf & " TDSRate.NCBasicRatePer,TDSRate.NCSurchargePer, " & vbCrLf & " DECODE(STATUS,'O','OPEN','CLOSE') AS Status,TDSSection.AddUser, TDSRate.ModUser" & vbCrLf & " FROM TDS_Section_MST TDSSection ,TDS_Rate_MST TDSRate " & vbCrLf & " WHERE TDSSection .CODE=TDSRate.CODE(+) AND TDSRATE.COMPANY_CODE=TDSRate.COMPANY_CODE" & vbCrLf & " AND TDSRATE.COMPANY_CODE(+)=" & RsCompany.Fields("COMPANY_CODE").Value & " AND TDSRATE.FYEAR(+)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND TDSSection.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ORDER BY TDSSection.Name"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
    End Sub
    Private Sub FormatSprdView()
        With SprdView
            .Row = -1
            .set_RowHeight(0, 12)
            .set_ColWidth(0, 5)
            .set_ColWidth(1, 12)
            .set_ColWidth(2, 12)
            .set_ColWidth(3, 12)
            .set_ColWidth(4, 12)
            .set_ColWidth(5, 12)
            .set_ColWidth(6, 12)
            .set_ColWidth(7, 12)
            .set_ColWidth(8, 12)
            .set_ColWidth(9, 12)
            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub

    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr

        SqlStr = ""
        '     If IsFieldExist = True Then Delete1 = False: Exit Function				

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        If InsertIntoDelAudit(PubDBCn, "TDS_Section_MST", (txtName.Text), RsTDSSection, "", "D") = False Then GoTo DeleteErr
        If InsertIntoDeleteTrn(PubDBCn, "TDS_Section_MST", "Code || ':' || WEF", xCode & ":" & VB6.Format(lblWEF.Text, "DD-MMM-YYYY")) = False Then GoTo DeleteErr

        '''If InsertIntoDelAudit(PubDBCn, "TDSRATE", txtName.Text, RsTDSSection) = False Then GoTo DeleteErr:				

        SqlStr = "Delete from TDS_Rate_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND Code=" & xCode & "" & vbCrLf & " AND WEF=TO_DATE('" & VB6.Format(lblWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        PubDBCn.Execute(SqlStr)

        '     SqlStr = "Delete from TDSSECTION where Code=" & xCode & ""				
        '     PubDBCn.Execute SqlStr				

        PubDBCn.CommitTrans()
        RsTDSSection.Requery()
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans()
        RsTDSSection.Requery()
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete Transaction Exists Against this Code")
            Exit Function
        End If
        MsgBox(Err.Description)
    End Function

    Private Sub ShowReport(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim mTitle As String
        mTitle = ""
        Report1.Reset()
        mTitle = "Listing Of TDS Section"
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\TDSSection.rpt"
        SetCrpt(Report1, Mode, 1, mTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub



    Private Function UpdateTDSRate(ByRef pCode As Integer) As Boolean
        On Error GoTo UpdateTDSRateErr

        SqlStr = " INSERT INTO TDS_Rate_MST ( " & vbCrLf _
              & " COMPANY_CODE, FYEAR, Code, FormNo, BasicRatePer, SurchargePer, " & vbCrLf _
              & " NCBasicRatePer, NCSurchargePer, WEF," & vbCrLf _
              & " EDITRF, ModUser, ModDate ) VALUES ( " & vbCrLf _
              & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & "," & pCode & ", " & vbCrLf _
              & " '" & MainClass.AllowSingleQuote(Trim(cboFormNo.Text)) & "', " & vbCrLf _
              & " " & Val(txtBRate.Text) & ", " & vbCrLf _
              & " " & Val(txtSurcharge.Text) & ", " & vbCrLf _
              & " " & Val(txtNCBRate.Text) & ", " & vbCrLf _
              & " " & Val(txtNCSurcharge.Text) & ", " & vbCrLf _
              & " TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
              & " 'N', " & vbCrLf _
              & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
              & " TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        PubDBCn.Execute(SqlStr)

        UpdateTDSRate = True
        Exit Function
UpdateTDSRateErr:
        UpdateTDSRate = False
    End Function

    Private Sub TxtNature_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtNature.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtNature_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtNature.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, TxtNature.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtNCBRate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNCBRate.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNCBRate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNCBRate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtNCSurcharge_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNCSurcharge.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNCSurcharge_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNCSurcharge.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSectionCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSectionCode.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSectionCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSectionCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtSectionCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSurcharge_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSurcharge.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSurcharge_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSurcharge.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtWef_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWEF.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtWef_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtWEF.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        TxtName_Validating(txtName, New System.ComponentModel.CancelEventArgs(False))
        ''Call ShowTDSRATE(xCode)				
        eventArgs.Cancel = Cancel
    End Sub
End Class
