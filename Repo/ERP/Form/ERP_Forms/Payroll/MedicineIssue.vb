Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmMedicineIssue
    Inherits System.Windows.Forms.Form
    Dim RsMedicineTRN As ADODB.Recordset ''ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

    Private Sub cboPurpose_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboPurpose.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        txtEmpCode.Text = ""
        txtEmpName.Text = ""
        txtDepartment.Text = ""
    End Sub

    Private Sub cboPurpose_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboPurpose.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtVNo.Enabled = False
            If txtVDate.Enabled = True Then txtVDate.Focus()
        Else
            ADDMode = False
            MODIFYMode = False
            If RsMedicineTRN.EOF = False Then RsMedicineTRN.MoveFirst()
            Show1()
            txtVNo.Enabled = True
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
        'Resume
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        On Error Resume Next
        Me.hide()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        Dim SqlStr As String = ""

        If ValidateBranchLocking((txtVDate.Text)) = True Then
            Exit Sub
        End If


        If txtVNo.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub

        If Not RsMedicineTRN.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "PAY_MEDICINE_TRN", (txtVNo.Text), RsMedicineTRN) = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "PAY_MEDICINE_TRN", "MKEY", (lblMkey.Text)) = False Then GoTo DelErrPart

                PubDBCn.Execute("DELETE FROM PAY_MEDICINE_TRN WHERE MKEY=" & Val(lblMkey.Text) & "")
                PubDBCn.CommitTrans()
                RsMedicineTRN.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
        RsMedicineTRN.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr


        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsMedicineTRN, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

            txtVNo.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
            txtVNo.Enabled = True
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonST(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportonST(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mSubTitle1 As String
        Dim mHeading As String


        Report1.Reset()
        mTitle = Me.Text
        mSubTitle = ""
        mHeading = ""

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\Medicine.RPT"

        SqlStr = MakeSQL

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mHeading)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef pHeading As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub
    Private Function MakeSQL() As String

        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim I As Integer


        MakeSQL = ""
        ''SELECT CLAUSE...

        MakeSQL = " SELECT *  FROM " & vbCrLf & " PAY_MEDICINE_TRN"

        ''WHERE CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        MakeSQL = MakeSQL & vbCrLf & " AND MKEY='" & MainClass.AllowSingleQuote(lblMkey.Text) & "'"
        'ORDER CLAUSE...
        '
        '    MakeSQL = MakeSQL & vbCrLf & "ORDER BY IH.REF_NO,IH.REF_DATE"
        '
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonST(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler

        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If Update1 = True Then
            ADDMode = False
            MODIFYMode = False
            TxtVNo_Validating(TxtVNo, New System.ComponentModel.CancelEventArgs(False))
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
        Dim mCurRowNo As Integer
        Dim nMkey As String
        Dim mVNo As Double


        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = ""
        If Trim(txtVNo.Text) = "" Then
            mVNo = CDbl(AutoGenSeqRefNo("REF_NO"))
        Else
            mVNo = Val(txtVNo.Text)
        End If

        txtVNo.Text = VB6.Format(Val(CStr(mVNo)), "00000")


        If ADDMode = True Then
            mCurRowNo = MainClass.AutoGenRowNo("PAY_MEDICINE_TRN", "RowNo", PubDBCn)
            nMkey = RsCompany.Fields("COMPANY_CODE").Value & RsCompany.Fields("FYEAR").Value & mCurRowNo

            lblMkey.Text = nMkey

            SqlStr = " INSERT INTO PAY_MEDICINE_TRN ( " & vbCrLf & " MKEY, COMPANY_CODE , FYEAR, ROWNO," & vbCrLf & " REF_NO, REF_DATE," & vbCrLf & " EMP_TYPE, EMP_CODE, EMP_NAME, " & vbCrLf & " DEPT_NAME, DISEASE, MEDICINE_NAME," & vbCrLf & " REMARKS, SECURITY_NAME," & vbCrLf & " ADDUSER, ADDDATE," & vbCrLf & " MODUSER,MODDATE ) "




            SqlStr = SqlStr & vbCrLf & " VALUES ( " & vbCrLf & " '" & nMkey & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & "," & vbCrLf & " " & mCurRowNo & ", " & Val(txtVNo.Text) & ", " & vbCrLf & " TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI'), " & vbCrLf & " '" & MainClass.AllowSingleQuote(cboPurpose.Text) & "', '" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "', '" & MainClass.AllowSingleQuote(txtEmpName.Text) & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(txtDepartment.Text) & "', '" & MainClass.AllowSingleQuote(txtDisease.Text) & "', '" & MainClass.AllowSingleQuote(txtMedicineName.Text) & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', '" & MainClass.AllowSingleQuote(txtSecurityName.Text) & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"
        End If

        If MODIFYMode = True Then
            SqlStr = " UPDATE PAY_MEDICINE_TRN SET " & vbCrLf & " REF_NO=" & Val(txtVNo.Text) & ", " & vbCrLf & " REF_DATE=TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI'), " & vbCrLf & " EMP_TYPE='" & MainClass.AllowSingleQuote(cboPurpose.Text) & "', " & vbCrLf & " EMP_CODE='" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "', " & vbCrLf & " EMP_NAME='" & MainClass.AllowSingleQuote(txtEmpName.Text) & "', " & vbCrLf & " DEPT_NAME='" & MainClass.AllowSingleQuote(txtDepartment.Text) & "', " & vbCrLf & " DISEASE='" & MainClass.AllowSingleQuote(txtDisease.Text) & "', " & vbCrLf & " MEDICINE_NAME='" & MainClass.AllowSingleQuote(txtMedicineName.Text) & "', " & vbCrLf & " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf & " SECURITY_NAME='" & MainClass.AllowSingleQuote(txtSecurityName.Text) & "', " & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND MKEY =" & Val(lblMkey.Text) & ""
        End If

        PubDBCn.Execute(SqlStr)
        txtVNo.Text = VB6.Format(Val(CStr(mVNo)), "00000")

        Update1 = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsMedicineTRN.Requery()
        MsgBox(Err.Description)
        ''Resume
    End Function
    Private Function AutoGenSeqRefNo(ByRef mFieldName As String) As String

        On Error GoTo AutoGenSeqBillNoErr
        Dim RsGen As ADODB.Recordset
        Dim mNewSeqNo As Integer
        Dim SqlStr As String = ""

        SqlStr = ""
        mNewSeqNo = 1

        SqlStr = "SELECT Max(" & mFieldName & ")  FROM PAY_MEDICINE_TRN " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGen, ADODB.LockTypeEnum.adLockReadOnly)

        With RsGen
            If .EOF = False Then
                If Not IsDbNull(.Fields(0).Value) Then
                    mNewSeqNo = .Fields(0).Value + 1
                Else
                    mNewSeqNo = 1
                End If
            End If
        End With
        AutoGenSeqRefNo = CStr(mNewSeqNo)
        Exit Function
AutoGenSeqBillNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Private Sub ViewGrid()

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
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
        MainClass.ButtonStatus(Me, XRIGHT, RsMedicineTRN, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Public Sub frmMedicineIssue_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Medicine Issue Entry"

        SqlStr = "Select * From PAY_MEDICINE_TRN WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMedicineTRN, ADODB.LockTypeEnum.adLockReadOnly)

        cboPurpose.Items.Clear()
        cboPurpose.Items.Add("EMPLOYEE")
        cboPurpose.Items.Add("CASUAL")
        cboPurpose.Items.Add("OTHER")
        cboPurpose.SelectedIndex = -1

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

        SqlStr = " SELECT " & vbCrLf & " REF_NO, TO_CHAR(REF_DATE,'DD/MM/YYYY HH24:MI') AS REF_DATE, " & vbCrLf & " EMP_TYPE, EMP_CODE, EMP_NAME, " & vbCrLf & " DEPT_NAME, DISEASE, MEDICINE_NAME, " & vbCrLf & " REMARKS, SECURITY_NAME" & vbCrLf & " FROM PAY_MEDICINE_TRN " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

        SqlStr = SqlStr & " ORDER BY REF_NO,REF_DATE"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmMedicineIssue_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn
        Call SetMainFormCordinate(Me)
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
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
        Dim temp As Integer



        lblMkey.Text = ""
        txtVNo.Text = ""
        txtVDate.Text = GetServerDate & " " & GetServerTime

        cboPurpose.SelectedIndex = -1
        txtEmpCode.Text = ""
        txtEmpName.Text = ""
        txtDepartment.Text = ""
        txtDisease.Text = ""
        txtMedicineName.Text = ""
        txtRemarks.Text = ""
        txtSecurityName.Text = ""

        txtVDate.Enabled = False

        MainClass.ButtonStatus(Me, XRIGHT, RsMedicineTRN, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub

    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 300)
            .set_ColWidth(0, 500)
            .set_ColWidth(1, 1000)
            .set_ColWidth(2, 1500)
            .ColsFrozen = 2

            .set_ColWidth(3, 1000)
            .set_ColWidth(4, 1000)
            .set_ColWidth(5, 2500)
            .set_ColWidth(6, 2500)
            .set_ColWidth(7, 1500)
            .set_ColWidth(8, 1000)

            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal 'OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1

        txtVNo.Maxlength = RsMedicineTRN.Fields("REF_NO").Precision
        txtVDate.Maxlength = 16


        '    cboPurpose.MaxLength = RsMedicineTRN.Fields("EMP_TYPE").DefinedSize
        txtEmpCode.Maxlength = RsMedicineTRN.Fields("EMP_CODE").DefinedSize
        txtEmpName.Maxlength = RsMedicineTRN.Fields("EMP_NAME").DefinedSize
        txtDepartment.Maxlength = RsMedicineTRN.Fields("DEPT_NAME").DefinedSize
        txtDisease.Maxlength = RsMedicineTRN.Fields("DISEASE").DefinedSize
        txtMedicineName.Maxlength = RsMedicineTRN.Fields("MEDICINE_NAME").DefinedSize
        txtRemarks.Maxlength = RsMedicineTRN.Fields("REMARKS").DefinedSize
        txtSecurityName.Maxlength = RsMedicineTRN.Fields("SECURITY_NAME").DefinedSize

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub

    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim mOutTime As String
        Dim mInTime As String

        FieldsVarification = True
        If ValidateBranchLocking((txtVDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If


        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsMedicineTRN.EOF = True Then Exit Function

        If MODIFYMode = True And Trim(txtVNo.Text) = "" Then
            MsgInformation("REf No. is Blank")
            FieldsVarification = False
            Exit Function
        End If


        If Trim(txtVDate.Text) = "" Then
            MsgInformation(" Ref Date is empty. Cannot Save")
            txtVDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtVDate.Text) <> "" Then
            If IsDate(txtVDate.Text) = False Then
                MsgInformation(" Invalid Ref Date. Cannot Save")
                txtVDate.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If cboPurpose.SelectedIndex = -1 Then
            MsgInformation("Employee Type is Blank")
            FieldsVarification = False
            cboPurpose.Focus()
            Exit Function
        End If

        If Trim(txtEmpName.Text) = "" Then
            MsgInformation("Employee Name is Blank")
            FieldsVarification = False
            txtEmpName.Focus()
            Exit Function
        End If

        If Trim(txtDepartment.Text) = "" Then
            MsgInformation("Department is Blank")
            FieldsVarification = False
            txtDepartment.Focus()
            Exit Function
        End If

        If Trim(txtDisease.Text) = "" Then
            MsgInformation("Disease is Blank")
            FieldsVarification = False
            txtDisease.Focus()
            Exit Function
        End If

        If Trim(txtMedicineName.Text) = "" Then
            MsgInformation("Medicine Name is Blank")
            FieldsVarification = False
            txtMedicineName.Focus()
            Exit Function
        End If

        If Trim(txtSecurityName.Text) = "" Then
            MsgInformation("Security Name is Blank")
            FieldsVarification = False
            txtSecurityName.Focus()
            Exit Function
        End If

        '    If MainClass.ValidDataInGrid(sprdMain, ColDescription, "S", "Please Check Supplier.") = False Then FieldsVarification = False: Exit Function

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        'Resume
    End Function

    Private Sub frmMedicineIssue_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        Dim temp As Integer

        Me.hide()
        RsMedicineTRN.Close()
        'RsOpOuts.Close
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        txtVNo.Text = SprdView.Text

        TxtVNo_Validating(TxtVNo, New System.ComponentModel.CancelEventArgs(False))
        ViewGrid()
    End Sub
    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Private Sub txtDisease_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDisease.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDisease_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDisease.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDisease.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEmpCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpCode.DoubleClick
        Call SearchEmp()
    End Sub

    Private Sub txtEmpCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtEmpCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchEmp()
    End Sub
    Private Sub SearchEmp()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mTable As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        '        If ADDMode = True Then
        SqlStr = SqlStr & " AND (EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE='')"
        '        Else
        '            SqlStr = SqlStr & vbCrLf & " AND (EMP_LEAVE_DATE IS NULL OR (EMP_LEAVE_DATE >= '" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "'))"
        '        End If

        If cboPurpose.SelectedIndex = 0 Then
            mTable = "PAY_EMPLOYEE_MST"
        ElseIf cboPurpose.SelectedIndex = 1 Then
            mTable = "PAY_CONT_EMPLOYEE_MST"
        Else
            Exit Sub
        End If
        If MainClass.SearchGridMaster((txtEmpCode.Text), mTable, "EMP_NAME", "EMP_CODE", "EMP_DEPT_CODE", , SqlStr) = True Then
            txtEmpName.Text = AcName
            txtEmpCode.Text = AcName1
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtEmpCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmpCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        If Trim(txtEmpCode.Text) = "" Then GoTo EventExitSub

        txtEmpCode.Text = VB6.Format(txtEmpCode.Text, "000000")

        SqlStr = "SELECT 'EMPLOYEE' AS EMP_TYPE, EMP_CODE, EMP_NAME, EMP_DEPT_CODE " & vbCrLf & " FROM PAY_EMPLOYEE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote((txtEmpCode.Text)) & "'" & vbCrLf & " AND EMP_DOJ<=TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND (EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE >=TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        SqlStr = SqlStr & vbCrLf & " UNION " & vbCrLf & " SELECT 'CASUAL' AS EMP_TYPE, EMP_CODE, EMP_NAME, EMP_DEPT_CODE " & vbCrLf & " FROM PAY_CONT_EMPLOYEE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote((txtEmpCode.Text)) & "'" & vbCrLf & " AND EMP_DOJ<=TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND (EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE >=TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            cboPurpose.Text = IIf(IsDbNull(RsTemp.Fields("EMP_TYPE").Value), "", RsTemp.Fields("EMP_TYPE").Value)
            txtEmpCode.Text = IIf(IsDbNull(RsTemp.Fields("EMP_CODE").Value), "", RsTemp.Fields("EMP_CODE").Value)
            txtEmpName.Text = IIf(IsDbNull(RsTemp.Fields("EMP_NAME").Value), "", RsTemp.Fields("EMP_NAME").Value)
            txtDepartment.Text = IIf(IsDbNull(RsTemp.Fields("EMP_DEPT_CODE").Value), "", RsTemp.Fields("EMP_DEPT_CODE").Value)
        End If

        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtEmpName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtEmpName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEmpName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtEmpName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEmpCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtEmpCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEmpCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtEmpCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtMedicineName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMedicineName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMedicineName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMedicineName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtMedicineName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
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

    Private Sub txtSecurityName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSecurityName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSecurityName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSecurityName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSecurityName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtVDate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVDate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtVDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtVDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtVDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtVDate.Text) Then
            MsgBox("Invalid Ref Date.", MsgBoxStyle.Information)
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart

        Clear1()
        If Not RsMedicineTRN.EOF Then
            With RsMedicineTRN
                lblMkey.Text = IIf(IsDbNull(.Fields("mKey").Value), "", .Fields("mKey").Value)
                txtVNo.Text = IIf(IsDbNull(.Fields("REF_NO").Value), "", .Fields("REF_NO").Value)
                txtVDate.Text = VB6.Format(IIf(IsDbNull(.Fields("REF_DATE").Value), "", .Fields("REF_DATE").Value), "DD/MM/YYYY HH:MM")

                cboPurpose.Text = IIf(IsDbNull(.Fields("EMP_TYPE").Value), "", .Fields("EMP_TYPE").Value)
                txtEmpCode.Text = IIf(IsDbNull(.Fields("EMP_CODE").Value), "", .Fields("EMP_CODE").Value)
                txtEmpName.Text = IIf(IsDbNull(.Fields("EMP_NAME").Value), "", .Fields("EMP_NAME").Value)
                txtDepartment.Text = IIf(IsDbNull(.Fields("DEPT_NAME").Value), "", .Fields("DEPT_NAME").Value)
                txtDisease.Text = IIf(IsDbNull(.Fields("DISEASE").Value), "", .Fields("DISEASE").Value)
                txtMedicineName.Text = IIf(IsDbNull(.Fields("MEDICINE_NAME").Value), "", .Fields("MEDICINE_NAME").Value)
                txtRemarks.Text = IIf(IsDbNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)
                txtSecurityName.Text = IIf(IsDbNull(.Fields("SECURITY_NAME").Value), "", .Fields("SECURITY_NAME").Value)

                txtVNo.Enabled = False

            End With
        End If
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsMedicineTRN, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Private Sub txtVDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDepartment_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDepartment.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDepartment_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDepartment.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDepartment.Text, "N")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtVno_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtVNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Public Sub TxtVNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtVNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMkey As String = ""
        Dim mVNo As String
        Dim SqlStr As String = ""

        If Trim(txtVNo.Text) = "" Then GoTo EventExitSub
        mVNo = CStr(Val(txtVNo.Text))


        If MODIFYMode = True And RsMedicineTRN.BOF = False Then xMkey = RsMedicineTRN.Fields("mKey").Value

        SqlStr = "SELECT * FROM PAY_MEDICINE_TRN " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND REF_NO=" & Val(mVNo) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMedicineTRN, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMedicineTRN.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Ref No. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM PAY_MEDICINE_TRN WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MKEY=" & Val(xMkey) & ""

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMedicineTRN, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
