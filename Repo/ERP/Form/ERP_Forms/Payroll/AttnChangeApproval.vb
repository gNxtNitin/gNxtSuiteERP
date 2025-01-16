Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmAttnChangeApproval
   Inherits System.Windows.Forms.Form
   Dim RsTransMain As ADODB.Recordset ''Recordset
   'Private PvtDBCn As ADODB.Connection

   Dim ADDMode As Boolean
   Dim MODIFYMode As Boolean
   Dim XRIGHT As String
   Dim xMyMenu As String

   Dim FormActive As Boolean


   Private Sub chkAttn_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAttn.CheckStateChanged

      MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
   End Sub

   Private Sub chkMannual_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkMannual.CheckStateChanged

      MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
   End Sub


   Private Sub chkShift_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkShift.CheckStateChanged

      MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
   End Sub

   Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click
      On Error GoTo AddErr
      If cmdAdd.Text = ConCmdAddCaption Then
         ADDMode = True
         MODIFYMode = False
         Clear1()
      Else
         cmdAdd.Text = ConCmdAddCaption
         ADDMode = False
         MODIFYMode = False
         Show1()
      End If
      Exit Sub
AddErr:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
   End Sub

   Private Sub cmdRequestSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRequestSearch.Click
      On Error GoTo ErrPart
        Dim SqlStr As String = "" 

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND STATUS='O'"

        If MainClass.SearchGridMaster(txtRequestCode.Text, "ATH_PASSWORD_MST", "USER_ID", "EMP_NAME", , , SqlStr) = True Then
            txtRequestCode.Text = AcName
            txtRequestName.Text = AcName1
            txtRequestCode_Validating(txtRequestName, New System.ComponentModel.CancelEventArgs(True))
            If txtRequestCode.Enabled = True Then txtRequestCode.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDelete.Click
        On Error GoTo DelErrPart
        Dim mSqlStr As String

        If PubUserID <> "G0416" Then Exit Sub

        '    If Not RsTransMain.EOF Then
        '        If MsgQuestion("Want to Delete ? ") = vbYes Then    ' User chose Yes.
        '            PubDBCn.Errors.Clear
        '            PubDBCn.BeginTrans
        '            If InsertIntoDelAudit(PubDBCn, "PAY_ATTN_CHANGE_TRN", txtMRRNo.Text, RsTransMain, "AUTO_KEY_MRR") = False Then GoTo DelErrPart:
        '
        '            mSqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
        ''                    & " AND AUTO_KEY_MRR=" & Val(txtMRRNo.Text) & ""
        '
        '            PubDBCn.Execute "Delete from PAY_ATTN_CHANGE_TRN Where " & mSqlStr
        '
        '            PubDBCn.CommitTrans
        '            RsTransMain.Requery           ''.Refresh
        '            Clear1
        '        End If
        '    End If
        Exit Sub
DelErrPart:
        ''Resume
        PubDBCn.RollbackTrans() ''
        RsTransMain.Requery() ''.Refresh
        If Err.Description <> "" Then
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdModify.Click

        On Error GoTo ModifyErr

        If cmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsTransMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportONPrint(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportONPrint(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportONPrint(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""
        Dim SqlStr As String = "" 
        Dim mRptFileName As String
        Dim mVNo As String

        '    Report1.Reset
        '    MainClass.ClearCrptFormulas Report1
        '
        '    SqlStr = ""
        '
        '    Call MainClass.ClearCrptFormulas(Report1)
        '
        '
        '    mTitle = "Item RelationShip"
        '    mSubTitle = ""
        '    mRptFileName = "IR.rpt"
        '
        '    Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String)
        On Error GoTo ErrPart

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, True, xMyMenu)
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\" & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        Report1.Action = 1
        Report1.Reset()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub



    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If UpdateMain1 = True Then
            ADDMode = False
            MODIFYMode = False
            CheckData(False)
            If cmdAdd.Enabled = True Then cmdAdd.Focus()
        Else
            MsgInformation("Record not saved")
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        If Err.Description = "" Then Exit Sub
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub
    Private Sub frmAttnChangeApproval_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub SprdView_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdView.Change

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        With SprdView
            .Row = eventArgs.row
            .Col = 1
            txtRequestCode.Text = .Text

            .Col = 2
            txtFromDate.Text = .Text

            .Col = 3
            txtToDate.Text = .Text


            Show1()
            '        If txtMRRNo.Enabled = True Then txtMRRNo.SetFocus
            CmdView_Click(CmdView, New System.EventArgs())
        End With
    End Sub
    Private Function UpdateMain1() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = "" 
        Dim mShiftChange As String
        Dim mAttnChange As String
        Dim mMannualChange As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = ""
        mShiftChange = IIf(chkShift.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mAttnChange = IIf(chkAttn.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mMannualChange = IIf(chkMannual.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        If ADDMode = True Then
            SqlStr = "INSERT INTO PAY_ATTN_CHANGE_TRN (" & vbCrLf _
                     & " COMPANY_CODE, AUTH_GIVEN_TO, FROM_DATE, TO_DATE, REMARKS, " & vbCrLf _
                     & " ADDUSER,ADDDATE,MODUSER,MODDATE,CHANGE_SHIFT,CHANGE_ATTN,CHANGE_MANNUAL)" & vbCrLf _
                     & " VALUES( " & vbCrLf _
                     & " " & RsCompany.Fields("Company_Code").Value & ", '" & Trim(txtRequestCode.Text) & "', " & vbCrLf _
                     & " TO_DATE('" & VB6.Format(txtFromDate.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY'), " & vbCrLf _
                     & " TO_DATE('" & VB6.Format(txtToDate.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY'), " & vbCrLf _
                     & " '" & MainClass.AllowSingleQuote(txtReason.Text) & "', " & vbCrLf _
                     & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'',''," & vbCrLf _
                     & " '" & mShiftChange & "', '" & mAttnChange & "', '" & mMannualChange & "')"
        ElseIf MODIFYMode = True Then
            SqlStr = ""
            SqlStr = "UPDATE PAY_ATTN_CHANGE_TRN SET " & vbCrLf _
               & " CHANGE_SHIFT = '" & mShiftChange & "', " & vbCrLf _
               & " CHANGE_ATTN = '" & mAttnChange & "', " & vbCrLf _
               & " CHANGE_MANNUAL = '" & mMannualChange & "', " & vbCrLf _
               & " REMARKS='" & MainClass.AllowSingleQuote(txtReason.Text) & "'," & vbCrLf _
               & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
               & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
               & " WHERE COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
               & " AND AUTH_GIVEN_TO='" & MainClass.AllowSingleQuote(txtRequestCode.Text) & "'" & vbCrLf _
               & " AND FROM_DATE = TO_DATE('" & VB6.Format(txtFromDate.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
               & " AND TO_DATE = TO_DATE('" & VB6.Format(txtToDate.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY') "
        End If

        PubDBCn.Execute(SqlStr)

        UpdateMain1 = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        '    Resume
        UpdateMain1 = False
        PubDBCn.RollbackTrans() ''
        RsTransMain.Requery() ''.Refresh
        If Err.Description = "" Then Exit Function
        If Err.Number = -2147217900 Then
            ErrorMsg("Duplicate Ref Generated, Save Again", "Duplicate", MsgBoxStyle.Critical)
        Else
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If

    End Function
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim mRow As Integer
        Dim mItemCode As String
        Dim mRGPDate As String
        Dim mDetailFromDate As String
        Dim mDetailToDate As String

        FieldsVarification = True

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsTransMain.EOF = True Then Exit Function


        If txtRequestCode.Text = "" Then
            MsgBox("Request By Name is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtRequestCode.Focus()
            Exit Function
        End If

        If txtFromDate.Text = "" Then
            MsgBox("From date is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtFromDate.Focus()
            Exit Function
        End If

        If txtToDate.Text = "" Then
            MsgBox("To date is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtToDate.Focus()
            Exit Function
        End If

        If IsDate(txtFromDate.Text) = False Then
            MsgBox("Invalid From date.", MsgBoxStyle.Information)
            FieldsVarification = False
            txtFromDate.Focus()
            Exit Function
        End If

        If IsDate(txtToDate.Text) = False Then
            MsgBox("Invalid To date.", MsgBoxStyle.Information)
            FieldsVarification = False
            txtToDate.Focus()
            Exit Function
        End If

        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Function

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Public Sub frmAttnChangeApproval_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = "" 

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = ""
        SqlStr = "Select * from PAY_ATTN_CHANGE_TRN Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTransMain, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)
        '    Call SetTextLengths

        If cmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ''Resume
    End Sub

    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo AssignGridErr
        Dim SqlStr As String = "" 

        SqlStr = ""

        ''SELECT CLAUSE...


        SqlStr = "SELECT AUTH_GIVEN_TO, TO_CHAR(FROM_DATE,'DD/MM/YYYY') AS FROM_DATE, TO_CHAR(TO_DATE,'DD/MM/YYYY') AS TO_DATE, CHANGE_SHIFT, CHANGE_ATTN, CHANGE_MANNUAL  "

        ''FROM CLAUSE...

        SqlStr = SqlStr & vbCrLf & " FROM PAY_ATTN_CHANGE_TRN IH"

        ''WHERE CLAUSE...

        SqlStr = SqlStr & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        ''ORDER BY CLAUSE...

        SqlStr = SqlStr & vbCrLf & " Order by AUTH_GIVEN_TO,FROM_DATE"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
AssignGridErr:
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub

    Private Sub FormatSprdView()

        With SprdView
            .Row = -1

            .set_RowHeight(0, 400)

            .set_ColWidth(0, 600)
            .set_ColWidth(1, 1000)
            .set_ColWidth(2, 1000)
            .set_ColWidth(3, 4000)

            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            SprdView.set_RowHeight(-1, 300)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        With RsTransMain
            txtRequestCode.MaxLength = .Fields("AUTH_GIVEN_TO").DefinedSize
            txtReason.MaxLength = .Fields("REMARKS").DefinedSize
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub Show1()

        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = "" 
        Dim mValue As String

        SqlStr = " SELECT * " & vbCrLf & " FROM PAY_ATTN_CHANGE_TRN" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTH_GIVEN_TO='" & Trim(txtRequestCode.Text) & "' " & vbCrLf & " AND FROM_DATE=TO_DATE('" & VB6.Format(txtFromDate.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " AND TO_DATE=TO_DATE('" & VB6.Format(txtToDate.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY') "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTransMain, ADODB.LockTypeEnum.adLockReadOnly)

        With RsTransMain
            Clear1()
            If Not .EOF Then


                txtRequestCode.Text = IIf(IsDBNull(.Fields("AUTH_GIVEN_TO").Value), "", .Fields("AUTH_GIVEN_TO").Value)
                If MainClass.ValidateWithMasterTable(txtRequestCode.Text, "USER_ID", "EMP_NAME", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtRequestName.Text = MasterNo
                End If

                txtFromDate.Text = VB6.Format(IIf(IsDBNull(.Fields("FROM_DATE").Value), "", .Fields("FROM_DATE").Value), "dd/MM/yyyy")
                txtToDate.Text = VB6.Format(IIf(IsDBNull(.Fields("TO_DATE").Value), "", .Fields("TO_DATE").Value), "dd/MM/yyyy")

                mValue = IIf(IsDBNull(.Fields("CHANGE_SHIFT").Value), "N", .Fields("CHANGE_SHIFT").Value)
                chkShift.CheckState = IIf(mValue = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                mValue = IIf(IsDBNull(.Fields("CHANGE_ATTN").Value), "N", .Fields("CHANGE_ATTN").Value)
                chkAttn.CheckState = IIf(mValue = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                mValue = IIf(IsDBNull(.Fields("CHANGE_MANNUAL").Value), "N", .Fields("CHANGE_MANNUAL").Value)
                chkMannual.CheckState = IIf(mValue = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                txtReason.Text = IIf(IsDBNull(.Fields("Remarks").Value), "", .Fields("Remarks").Value)

                txtRequestCode.Enabled = False
                txtFromDate.Enabled = False
                txtToDate.Enabled = False
            End If
        End With
        ADDMode = False
        MODIFYMode = False

        MainClass.ButtonStatus(Me, XRIGHT, RsTransMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub
    Private Sub ViewGrid()

        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        AdoDCMain.Refresh
            FormatSprdView()
            SprdView.Focus()
            FraFront.SendToBack()
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraFront.BringToFront()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsTransMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub Clear1()



        txtRequestCode.Text = ""
        txtRequestName.Text = ""
        txtFromDate.Text = ""
        txtToDate.Text = ""
        txtReason.Text = ""

        chkShift.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkAttn.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkMannual.CheckState = System.Windows.Forms.CheckState.Unchecked

        txtRequestCode.Enabled = True
        txtFromDate.Enabled = True
        txtToDate.Enabled = True

        cmdRequestSearch.Enabled = True
        txtReason.Enabled = True

        Call AutoCompleteSearch("ATH_PASSWORD_MST", "USER_ID", "STATUS='O'", txtRequestCode)

        MainClass.ButtonStatus(Me, XRIGHT, RsTransMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub frmAttnChangeApproval_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmAttnChangeApproval_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        '    MainClass.DoFunctionKey Me, KeyCode
    End Sub

    Public Sub frmAttnChangeApproval_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        xMyMenu = myMenu
        MainClass.RightsToButton(Me, XRIGHT)

        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Call SetMainFormCordinate(Me)
        'Me.Top = 0
        'Me.Left = 0
        ''Me.Height = VB6.TwipsToPixelsY(3420)
        ''Me.Width = VB6.TwipsToPixelsX(9915)

        AdoDCMain.Visible = False

        FormActive = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub CheckData(ByRef Cancel As Boolean)

        On Error GoTo ERR1
        Dim xMkey As String = ""
        Dim mMRRNo As String
        Dim SqlStr As String = "" 
        Dim RsTemp As ADODB.Recordset = Nothing



        SqlStr = " SELECT * " & vbCrLf & " FROM PAY_ATTN_CHANGE_TRN" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTH_GIVEN_TO='" & Trim(txtRequestCode.Text) & "' " & vbCrLf & " AND FROM_DATE=TO_DATE('" & VB6.Format(txtFromDate.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " AND TO_DATE=TO_DATE('" & VB6.Format(txtToDate.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY') "


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTransMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTransMain.EOF = False Then
            '        Clear1
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such MRR, Use Generate MRR Option To add", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM PAY_ATTN_CHANGE_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTH_GIVEN_TO='" & Trim(txtRequestCode.Text) & "' " & vbCrLf & " AND FROM_DATE=TO_DATE('" & VB6.Format(txtFromDate.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " AND TO_DATE=TO_DATE('" & VB6.Format(txtToDate.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY') "

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTransMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtReason_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtReason.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtReason_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtReason.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtReason.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtRequestCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRequestCode.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRequestCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRequestCode.DoubleClick
        Call cmdRequestSearch_Click(cmdRequestSearch, New System.EventArgs())
    End Sub

    Private Sub txtRequestCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRequestCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtRequestCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRequestCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtRequestCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtRequestCode_DoubleClick(txtRequestCode, New System.EventArgs())
    End Sub

    Private Sub txtRequestCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRequestCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        If Trim(txtRequestCode.Text) = "" Then GoTo EventExitSub

        txtRequestCode.Text = VB6.Format(Trim(txtRequestCode.Text), "000000")

        If MainClass.ValidateWithMasterTable(txtRequestCode.Text, "USER_ID", "EMP_NAME", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtRequestName.Text = MasterNo
        Else
            MsgInformation("Invalid User ID")
            Cancel = True
        End If

        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
