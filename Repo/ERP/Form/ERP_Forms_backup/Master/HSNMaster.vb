Option Strict Off
Option Explicit On
Imports System
Imports System.Windows.Forms
'Imports VB = Microsoft.VisualBasic
'Imports Microsoft.VisualBasic.Compatibility.VB6
'Imports Microsoft.VisualBasic.Compatibility.Data
'Imports ADODC = VB6.ADODC
Imports System.Data.SqlClient   '' System.Data.OleDb
Imports System.Data.OleDb
Imports ADODB
Imports System.Collections 'For arraylist
Imports Microsoft.VisualBasic.Compatibility

Friend Class frmHSNMaster
    Inherits System.Windows.Forms.Form
    Dim RsHSN As ADODB.Recordset
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Private PvtDBCn As ADODB.Connection			

    Dim xCode As String
    Dim FormActive As Boolean
    Dim Shw As Boolean
    Dim MasterNo As Object
    Dim SqlStr As String = ""
    'Dim rs As New Resizer
    'Private originalSize As Size
    'Dim pCurrentWitdh As Integer ' Forms current Width.
    'Dim pCurrentHeight As Integer ' Forms current Height.
    'Dim pInitialWitdh As Integer ''= 1280 ' Forms initial width.
    'Dim pInitialHeight As Integer ''= 760 ' Forms initial height.
    '' Retrieve the working rectangle from the Screen class using the        PrimaryScreen and the WorkingArea properties.  
    'Dim pWorkingRectangle As System.Drawing.Rectangle = Screen.PrimaryScreen.WorkingArea
    'Private CtlArray As New ArrayList
    'Dim intX, intY As Integer
    'Dim Xratio, Yratio As Single

    'Dim ResizeForm As New Resizer





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
        MainClass.ButtonStatus(Me, XRIGHT, RsHSN, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ErrorPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Clear1()
        txtCode.Text = ""
        txtHSNDesc.Text = ""
        txtCGSTPer.Text = ""
        txtSGSTPer.Text = ""
        txtIGSTPer.Text = ""

        txtUnCGSTPer.Text = ""
        txtUnSGSTPer.Text = ""
        txtUnIGSTPer.Text = ""

        txtCompositePer.Text = ""

        chkReverseChargeApp.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkGSTApp.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkExempted.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkOption.CheckState = System.Windows.Forms.CheckState.Unchecked

        chkGSTApp.Enabled = True
        chkExempted.Enabled = True

        If lblCodeType.Text = "G" Then
            chkGSTApp.CheckState = System.Windows.Forms.CheckState.Checked
            chkReverseChargeApp.Enabled = False
            chkOption.Enabled = False
        Else
            chkReverseChargeApp.Enabled = True
            chkOption.Enabled = True
        End If

        txtCode.Enabled = True
        Call AutoCompleteSearch("GEN_HSN_MST", "HSN_CODE", "CODETYPE='" & lblCodeType.Text & "'", txtCode)
        MainClass.ButtonStatus(Me, XRIGHT, RsHSN, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub chkExempted_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkExempted.CheckStateChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkGSTApp_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkGSTApp.CheckStateChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkOption_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkOption.CheckStateChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkReverseChargeApp_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkReverseChargeApp.CheckStateChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click
        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsHSN, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
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
            txtCode.Focus()
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide() ''me.hide 
        Me.Close()
    End Sub
    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr
        SqlStr = ""

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        If InsertIntoDelAudit(PubDBCn, "GEN_HSN_MST", (txtCode.Text), RsHSN, "HSN_CODE") = False Then GoTo DeleteErr
        If InsertIntoDeleteTrn(PubDBCn, "GEN_HSN_MST", "HSN_CODE", (txtCode.Text)) = False Then GoTo DeleteErr

        SqlStr = "DELETE FROM GEN_HSN_MST " & vbCrLf _
            & "WHERE HSN_CODE='" & MainClass.AllowSingleQuote(UCase((txtCode.Text))) & "'"

        If CheckConsolidatedMaster("FIN_SUPP_CUST_MST") = True Then
        Else
            SqlStr = SqlStr & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If

        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        RsHSN.Requery() ''.Refresh				
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans() ''				
        RsHSN.Requery() ''.Refresh				
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete, Transactions Exists Against This Expense Head.", MsgBoxStyle.Information)
            Exit Function
        End If
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If txtCode.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub
        'If CheckConsolidatedMaster("FIN_SUPP_CUST_MST") = True Then
        '    MsgInformation("Cann't be Delete.")
        '    Exit Sub
        'End If
        If Not RsHSN.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.				
                If Delete1() = False Then GoTo DelErrPart
                If RsHSN.EOF = True Then
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
    Private Sub frmHSNMaster_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub frmHSNMaster_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
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
        If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub
    Private Sub txtCGSTPer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCGSTPer.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtCGSTPer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCGSTPer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCGSTPer_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCGSTPer.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Val(txtCGSTPer.Text) > 100 Then
            MsgInformation("Tax Per Cann't be Greater Than 100")
            Cancel = True
            GoTo EventExitSub
        Else
            txtCGSTPer.Text = VB6.Format(Val(txtCGSTPer.Text), "0.00")
            If ADDMode = True Then
                txtSGSTPer.Text = VB6.Format(Val(txtCGSTPer.Text), "0.00")
                txtIGSTPer.Text = VB6.Format(Val(txtCGSTPer.Text) * 2, "0.00")

                txtUnCGSTPer.Text = VB6.Format(Val(txtCGSTPer.Text), "0.00")
                txtUnSGSTPer.Text = VB6.Format(Val(txtCGSTPer.Text), "0.00")
                txtUnIGSTPer.Text = VB6.Format(Val(txtCGSTPer.Text) * 2, "0.00")
            End If
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCode.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtCode.Text)
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
        If MODIFYMode = True And RsHSN.EOF = False Then xCode = RsHSN.Fields("HSN_CODE").Value

        SqlStr = "SELECT * FROM GEN_HSN_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND HSN_CODE='" & MainClass.AllowSingleQuote(UCase((Trim(txtCode.Text)))) & "'" & vbCrLf _
            & " AND CODETYPE='" & lblCodeType.Text & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsHSN, ADODB.LockTypeEnum.adLockReadOnly)

        If RsHSN.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("HSN Code Does Not Exist In Master" & vbCrLf & "Click Add To Add In Master")
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = ""
                SqlStr = "SELECT * FROM GEN_HSN_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND HSN_CODE='" & xCode & "' AND CODETYPE='" & lblCodeType.Text & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsHSN, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub frmHSNMaster_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        MainClass.UOpenRecordSet("Select * From GEN_HSN_MST Where 1<>1 ", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsHSN, ADODB.LockTypeEnum.adLockReadOnly)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        AssignGrid(False)
        SetTextLengths()
        Clear1()
        If CmdAdd.Enabled = True Then cmdAdd_Click(CmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmHSNMaster_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo ErrPart
        'Screen Resolution values on running computer

        'intX = Screen.PrimaryScreen.Bounds.Width
        'intY = Screen.PrimaryScreen.Bounds.Height
        'These are design screen resolutions, but should work with other resolutions too
        'You should design at low resolution, so components will grow and not shrink when taken
        'to other computers. I haven't check for font size, but by growing the components you won't have
        'a problem.

        Me.Left = 0
        Me.Top = 0

        'Xratio = intX / 1366 ''1152
        'Yratio = intY / 768   '' 864
        ''Get the controls on the form, including menus, but not the controls in other containers
        'For Each Cnt As Control In Me.Controls
        '    CtlArray.Add(Cnt)
        'Next

        'Get the children controls
        'GetTheChildren()
        'Adjust New size and position
        'ResizeThem()

        'ResizeForm.FindAllControls(Me)

        ''Set PvtDBCn = New ADODB.Connection				
        ''PvtDBCn.Open StrConn				

        XRIGHT = MainClass.STRMenuRight(PubUserID, 1, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CmdView.Text = ConCmdGridViewCaption
        ADDMode = False
        MODIFYMode = False

        AutoCompleteSearch("GEN_HSN_MST", "HSN_CODE", "CODETYPE='" & lblCodeType.Text & "'", txtCode) ''HSNSearch()
        'rs.FindAllControls(Me)
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmHSNMaster_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsHSN = Nothing
        RsHSN.Close()
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub Show1()
        On Error GoTo ShowErrPart
        If Not RsHSN.EOF Then

            txtCode.Text = IIf(IsDBNull(RsHSN.Fields("HSN_CODE").Value), "", RsHSN.Fields("HSN_CODE").Value)
            txtHSNDesc.Text = IIf(IsDBNull(RsHSN.Fields("HSN_DESC").Value), "", RsHSN.Fields("HSN_DESC").Value)
            txtCGSTPer.Text = VB6.Format(IIf(IsDBNull(RsHSN.Fields("CGST_PER").Value), 0, RsHSN.Fields("CGST_PER").Value), "0.00")
            txtSGSTPer.Text = VB6.Format(IIf(IsDBNull(RsHSN.Fields("SGST_PER").Value), 0, RsHSN.Fields("SGST_PER").Value), "0.00")
            txtIGSTPer.Text = VB6.Format(IIf(IsDBNull(RsHSN.Fields("IGST_PER").Value), 0, RsHSN.Fields("IGST_PER").Value), "0.00")

            txtUnCGSTPer.Text = VB6.Format(IIf(IsDBNull(RsHSN.Fields("CGST_PER_UNREG").Value), 0, RsHSN.Fields("CGST_PER_UNREG").Value), "0.00")
            txtUnSGSTPer.Text = VB6.Format(IIf(IsDBNull(RsHSN.Fields("SGST_PER_UNREG").Value), 0, RsHSN.Fields("SGST_PER_UNREG").Value), "0.00")
            txtUnIGSTPer.Text = VB6.Format(IIf(IsDBNull(RsHSN.Fields("IGST_PER_UNREG").Value), 0, RsHSN.Fields("IGST_PER_UNREG").Value), "0.00")


            txtCompositePer.Text = VB6.Format(IIf(IsDBNull(RsHSN.Fields("GST_COMPOSIT_PER").Value), 0, RsHSN.Fields("GST_COMPOSIT_PER").Value), "0.00")

            chkReverseChargeApp.CheckState = IIf(RsHSN.Fields("REVERSE_CHARGE_APP").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkGSTApp.CheckState = IIf(RsHSN.Fields("GST_APP").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkExempted.CheckState = IIf(RsHSN.Fields("GST_EXEMPTED").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkOption.CheckState = IIf(RsHSN.Fields("GST_RATE_OPT").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            xCode = RsHSN.Fields("HSN_CODE").Value
        End If
        txtCode.Enabled = True
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsHSN, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
        'Resume
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
            txtCode_Validating(txtCode, New System.ComponentModel.CancelEventArgs(False))
            'Call AutoCompleteSearch("GEN_HSN_MST", "HSN_CODE", "CODETYPE='" & lblCodeType.Text & "'", txtCode)     ''HSNSearch()
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
        Dim mRCApp As String
        Dim mCreditApp As String
        Dim mExempted As String
        Dim mRateOption As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xCompanyCode As Long

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()


        mRCApp = IIf(chkReverseChargeApp.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mCreditApp = IIf(chkGSTApp.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mExempted = IIf(chkExempted.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mRateOption = IIf(chkOption.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") '

        If CheckConsolidatedMaster("FIN_SUPP_CUST_MST") = True Then
            SqlStr = "SELECT COMPANY_CODE FROM GEN_COMPANY_MST"
        Else
            SqlStr = "SELECT COMPANY_CODE FROM GEN_COMPANY_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                xCompanyCode = RsTemp.Fields("COMPANY_CODE").Value
                SqlStr = ""
                If ADDMode = True Then
                    '        mCode = MainClass.AutoGenRowNo("GEN_HSN_MST", "Code", PubDBCn)	
                    SqlStr = "INSERT INTO GEN_HSN_MST (" & vbCrLf _
                        & " COMPANY_CODE, HSN_CODE, HSN_DESC, " & vbCrLf _
                        & " CGST_PER, SGST_PER, IGST_PER, " & vbCrLf _
                        & " CGST_PER_UNREG, SGST_PER_UNREG, IGST_PER_UNREG, " & vbCrLf _
                        & " CODETYPE, REVERSE_CHARGE_APP, GST_APP," & vbCrLf _
                        & " ADDUSER,ADDDATE, MODUSER,MODDATE,GST_EXEMPTED,GST_RATE_OPT,GST_COMPOSIT_PER " & vbCrLf _
                        & " ) VALUES ( " & vbCrLf _
                        & " " & xCompanyCode & ", " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(txtCode.Text) & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(UCase(txtHSNDesc.Text)) & "'," & vbCrLf _
                        & " " & VB6.Format(Val(txtCGSTPer.Text), "0.00") & ", " & vbCrLf _
                        & " " & VB6.Format(Val(txtSGSTPer.Text), "0.00") & ", " & VB6.Format(Val(txtIGSTPer.Text), "0.00") & "," & vbCrLf _
                        & " " & VB6.Format(Val(txtUnCGSTPer.Text), "0.00") & ", " & vbCrLf _
                        & " " & VB6.Format(Val(txtUnSGSTPer.Text), "0.00") & ", " & VB6.Format(Val(txtUnIGSTPer.Text), "0.00") & "," & vbCrLf _
                        & " '" & lblCodeType.Text & "', '" & mRCApp & "',  '" & mCreditApp & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                        & " TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'), '','','" & mExempted & "','" & mRateOption & "'," & VB6.Format(Val(txtCompositePer.Text), "0.00") & " " & vbCrLf _
                        & " )"
                Else
                    SqlStr = " UPDATE GEN_HSN_MST  SET " & vbCrLf _
                        & " HSN_DESC='" & MainClass.AllowSingleQuote(UCase(txtHSNDesc.Text)) & "'," & vbCrLf _
                        & " CGST_PER=" & VB6.Format(Val(txtCGSTPer.Text), "0.00") & "," & vbCrLf _
                        & " SGST_PER=" & VB6.Format(Val(txtSGSTPer.Text), "0.00") & "," & vbCrLf _
                        & " IGST_PER=" & VB6.Format(Val(txtIGSTPer.Text), "0.00") & ", " & vbCrLf _
                        & " CGST_PER_UNREG=" & VB6.Format(Val(txtUnCGSTPer.Text), "0.00") & "," & vbCrLf _
                        & " SGST_PER_UNREG=" & VB6.Format(Val(txtUnSGSTPer.Text), "0.00") & "," & vbCrLf _
                        & " IGST_PER_UNREG=" & VB6.Format(Val(txtUnIGSTPer.Text), "0.00") & ", " & vbCrLf _
                        & " GST_COMPOSIT_PER=" & VB6.Format(Val(txtCompositePer.Text), "0.00") & ", " & vbCrLf _
                        & " CODETYPE = '" & lblCodeType.Text & "', " & vbCrLf _
                        & " GST_EXEMPTED='" & mExempted & "' ," & vbCrLf _
                        & " GST_RATE_OPT='" & mRateOption & "' ," & vbCrLf _
                        & " REVERSE_CHARGE_APP ='" & mRCApp & "' , " & vbCrLf _
                        & " GST_APP = '" & mCreditApp & "'," & vbCrLf _
                        & " MODUSER = '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                        & " MODDATE = TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')" & vbCrLf _
                        & " WHERE COMPANY_CODE=" & xCompanyCode & "" & vbCrLf & " AND HSN_CODE= '" & xCode & "'"
                End If
UpdatePart:
                PubDBCn.Execute(SqlStr)
                RsTemp.MoveNext()
            Loop
        End If
        PubDBCn.CommitTrans()
        Update1 = True
        Exit Function
UpdateError:
        Update1 = False
        PubDBCn.RollbackTrans() ''		
        RsHSN.Requery() ''.Refresh		
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        txtCode.MaxLength = RsHSN.Fields("HSN_CODE").DefinedSize
        txtHSNDesc.MaxLength = RsHSN.Fields("HSN_DESC").DefinedSize
        txtCGSTPer.MaxLength = RsHSN.Fields("CGST_PER").Precision
        txtSGSTPer.MaxLength = RsHSN.Fields("SGST_PER").Precision
        txtIGSTPer.MaxLength = RsHSN.Fields("IGST_PER").Precision

        txtUnCGSTPer.MaxLength = RsHSN.Fields("CGST_PER_UNREG").Precision
        txtUnSGSTPer.MaxLength = RsHSN.Fields("SGST_PER_UNREG").Precision
        txtUnIGSTPer.MaxLength = RsHSN.Fields("IGST_PER_UNREG").Precision

        txtCompositePer.MaxLength = RsHSN.Fields("GST_COMPOSIT_PER").Precision
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        FieldsVarification = True
        If Trim(txtCode.Text) = "" Then
            MsgInformation("HSN Code is empty. Cannot Save")
            txtCode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtHSNDesc.Text) = "" Then
            MsgInformation("HSN Description is empty. Cannot Save")
            txtHSNDesc.Focus()
            FieldsVarification = False
            Exit Function
        End If
        '		
        '    If Trim(txtSGSTPer.Text) = "" Then		
        '        MsgInformation "SGST_PER is empty. Cannot Save"		
        '        txtSGSTPer.SetFocus		
        '        FieldsVarification = False		
        '        Exit Function		
        '    End If		
        '		
        '    If Trim(txtCGSTPer.Text) = "" Then		
        '        MsgInformation " Main Tariff Description is empty. Cannot Save"		
        '        txtCGSTPer.SetFocus		
        '        FieldsVarification = False		
        '        Exit Function		
        '    End If		

        If lblCodeType.Text = "G" And chkGSTApp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MsgQuestion("Are you Sure that Credit is not Applicable for this HSN Code?") = CStr(MsgBoxResult.No) Then ' User chose Yes.	
                FieldsVarification = False
                Exit Function
            End If
        End If

        If Val(txtSGSTPer.Text) + Val(txtCGSTPer.Text) <> Val(txtIGSTPer.Text) Then
            MsgInformation("IGST% Should be Equal to CGST% & SGST%, so cannot Save")
            FieldsVarification = False
            Exit Function
        End If

        If Val(txtUnSGSTPer.Text) + Val(txtUnCGSTPer.Text) <> Val(txtUnIGSTPer.Text) Then
            MsgInformation("IGST% Should be Equal to CGST% & SGST%, so cannot Save")
            FieldsVarification = False
            Exit Function
        End If

        If chkExempted.CheckState = System.Windows.Forms.CheckState.Checked Then
            If chkGSTApp.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("Service / Good is Exempted, So cann't be Select GST Credit Applicable.")
                FieldsVarification = False
                Exit Function
            End If

            If chkReverseChargeApp.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("Service / Good is Exempted, So cann't be Select Reverse Charge Applicable.")
                FieldsVarification = False
                Exit Function
            End If

            If Val(txtSGSTPer.Text) + Val(txtCGSTPer.Text) + Val(txtIGSTPer.Text) <> 0 Then
                MsgInformation("GST Percent Should be Zero, so cannot Save")
                FieldsVarification = False
                Exit Function
            End If

            If Val(txtUnSGSTPer.Text) + Val(txtUnCGSTPer.Text) + Val(txtUnIGSTPer.Text) <> 0 Then
                MsgInformation("GST Percent Should be Zero, so cannot Save")
                FieldsVarification = False
                Exit Function
            End If
        Else
            If Val(txtSGSTPer.Text) + Val(txtCGSTPer.Text) + Val(txtIGSTPer.Text) = 0 Then
                MsgInformation("GST Percent cann't be Zero, So cannot Save")
                FieldsVarification = False
                Exit Function
            End If
            If Val(txtUnSGSTPer.Text) + Val(txtUnCGSTPer.Text) + Val(txtUnIGSTPer.Text) = 0 Then
                MsgInformation("GST Percent cann't be Zero, So cannot Save")
                FieldsVarification = False
                Exit Function
            End If
        End If

        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
        End If
        If MODIFYMode = True And RsHSN.EOF = True Then Exit Function
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
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = ""

        SqlStr = " SELECT HSN_CODE, HSN_DESC, CGST_PER, SGST_PER, IGST_PER,DECODE(REVERSE_CHARGE_APP,'Y','YES','NO') AS REVERSE_CHARGE_APP,DECODE(GST_APP,'Y','YES','NO') AS GST_APP ,DECODE(GST_EXEMPTED,'Y','YES','NO') AS GST_EXEMPTED, DECODE(GST_RATE_OPT,'Y','YES','NO') AS GST_RATE_OPT  " & vbCrLf _
                & " FROM GEN_HSN_MST" & vbCrLf _
                & " WHERE GEN_HSN_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CODETYPE='" & lblCodeType.Text & "'"
        'MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        'SprdView.DataSource = RsTemp.DataSource

        'MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N")
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
    End Sub
    Private Sub FormatSprdView()
        With SprdView
            .Row = -1
            .set_RowHeight(0, 12)
            .set_ColWidth(0, 5)
            .set_ColWidth(1, 12)
            .set_ColWidth(2, 15)
            .set_ColWidth(3, 12)
            .set_ColWidth(4, 8)
            .set_ColWidth(5, 8)
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
        mTitle = "HSN Master"

        Report1.ReportFileName = App_Path() & "\reports\HSN.rpt"
        SetCrpt(Report1, Mode, 1, mTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub txtCompositePer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCompositePer.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtCompositePer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCompositePer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCompositePer_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCompositePer.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Val(txtCompositePer.Text) > 100 Then
            MsgInformation("Tax Per Cann't be Greater Than 100")
            Cancel = True
            GoTo EventExitSub
        Else
            txtCompositePer.Text = VB6.Format(Val(txtCompositePer.Text), "0.00")
        End If

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtIGSTPer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtIGSTPer.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtIGSTPer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtIGSTPer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtHSNDesc_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtHSNDesc.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtHSNDesc_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtHSNDesc.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, TxtCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtIGSTPer_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtIGSTPer.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Val(txtIGSTPer.Text) > 100 Then
            MsgInformation("Tax Per Cann't be Greater Than 100")
            Cancel = True
            GoTo EventExitSub
        Else
            txtIGSTPer.Text = VB6.Format(Val(txtIGSTPer.Text), "0.00")
        End If

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSGSTPer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSGSTPer.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSGSTPer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSGSTPer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSGSTPer_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSGSTPer.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Val(txtSGSTPer.Text) > 100 Then
            MsgInformation("Tax Per Cann't be Greater Than 100")
            Cancel = True
            GoTo EventExitSub
        Else
            txtSGSTPer.Text = VB6.Format(Val(txtSGSTPer.Text), "0.00")
        End If

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtUnCGSTPer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtUnCGSTPer.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtUnCGSTPer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtUnCGSTPer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtUnCGSTPer_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtUnCGSTPer.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Val(txtUnCGSTPer.Text) > 100 Then
            MsgInformation("Tax Per Cann't be Greater Than 100")
            Cancel = True
            GoTo EventExitSub
        Else
            txtUnCGSTPer.Text = VB6.Format(Val(txtUnCGSTPer.Text), "0.00")
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtUnSGSTPer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtUnSGSTPer.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtUnSGSTPer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtUnSGSTPer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtUnSGSTPer_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtUnSGSTPer.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Val(txtUnSGSTPer.Text) > 100 Then
            MsgInformation("Tax Per Cann't be Greater Than 100")
            Cancel = True
            GoTo EventExitSub
        Else
            txtUnSGSTPer.Text = VB6.Format(Val(txtUnSGSTPer.Text), "0.00")
        End If

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtUnIGSTPer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtUnIGSTPer.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtUnIGSTPer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtUnIGSTPer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtUnIGSTPer_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtUnIGSTPer.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Val(txtUnIGSTPer.Text) > 100 Then
            MsgInformation("Tax Per Cann't be Greater Than 100")
            Cancel = True
            GoTo EventExitSub
        Else
            txtUnIGSTPer.Text = VB6.Format(Val(txtUnIGSTPer.Text), "0.00")
        End If

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
#Region "HSN Search"
    Private Sub HSNSearch()

        Dim mSqlStr As String = ""
        Dim dt As New DataTable()
        'Using con As New OleDbConnection(ConfigurationManager.ConnectionStrings("StackOverflow").ConnectionString)
        'Dim con As New OleDbConnection(StrConn)

        mSqlStr = "SELECT HSN_CODE FROM GEN_HSN_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND CODETYPE='" & lblCodeType.Text & "'"

        Using da As New OleDbDataAdapter(mSqlStr, PubDBCnDataGrid)
            da.Fill(dt)
        End Using
        'End Using

        Dim autoSource As String() = (From r As DataRow In dt.AsEnumerable() Select r.Field(Of String)("HSN_CODE")).ToArray()
        Dim source As AutoCompleteStringCollection = New AutoCompleteStringCollection()

        source.AddRange(autoSource)

        With txtCode
            .AutoCompleteCustomSource = source
            .AutoCompleteMode = AutoCompleteMode.SuggestAppend
            .AutoCompleteSource = AutoCompleteSource.CustomSource
        End With

    End Sub
#End Region

    Private Sub frmHSNMaster_Resize(sender As Object, e As System.EventArgs) Handles Me.Resize
        'ResizeForm.ResizeAllControls(Me)
        'rs.ResizeAllControls(Me)
    End Sub

    'Private Sub frmHSNMaster_SizeChanged(sender As Object, e As System.EventArgs) Handles Me.SizeChanged
    '    Call ResizeThem()
    'End Sub
    'Private Sub GetTheChildren()
    '    'Gets the controls inside containes like panels or tabcontrols
    '    'For Each ctl As Control In GetAllControls(Me.Parent)
    '    For Each ctl As Control In GetAllControls(Me)
    '        If ctl.Parent IsNot Me Then
    '            If TypeOf ctl.Parent Is TabPage Then
    '                If ctl.Name = "" Then
    '                    CtlArray.Add(ctl)
    '                Else
    '                    CtlArray.Add(ctl)
    '                End If
    '            Else
    '                If Not TypeOf (ctl) Is TabPage Then
    '                    If ctl.Name = "" Then
    '                        CtlArray.Add(ctl)
    '                    Else
    '                        CtlArray.Add(ctl)
    '                    End If
    '                End If
    '            End If
    '        End If
    '    Next
    'End Sub
    'Private Function GetAllControls(ByVal container As Control) As Control()
    '    Dim al As New ArrayList
    '    Dim ctl As Control
    '    For Each ctl In container.Controls
    '        GetControlsWithChildren(ctl, al)
    '    Next
    '    Return al.ToArray(GetType(Control))
    'End Function
    'Private Sub GetControlsWithChildren(ByVal container As Control, ByVal al As ArrayList)
    '    ' add this control to the ArrayList
    '    al.Add(container)
    '    ' add all its child controls, by calling this routine recursively
    '    Dim ctl As Control

    '    For Each ctl In container.Controls
    '        'A TabPage is a Panel; SplitContainer is a Panel
    '        GetControlsWithChildren(ctl, al)
    '    Next

    'End Sub
    'Private Sub ResizeThem()
    '    Dim i As Integer
    '    For i = 0 To CtlArray.Count - 1
    '        If TypeOf CtlArray.Item(i) Is MenuStrip Then
    '        Else
    '            If TypeOf CtlArray.Item(i) Is Panel And CtlArray.Item(i).parent IsNot Me Then
    '                'SplitPanel for instance
    '            Else
    '                CtlArray.Item(i).autosize = False
    '                CtlArray.Item(i).dock = 0
    '                CtlArray.Item(i).width = CtlArray.Item(i).width * Xratio
    '                CtlArray.Item(i).left = CtlArray.Item(i).left * Xratio
    '                CtlArray.Item(i).height = CtlArray.Item(i).height * Yratio
    '                CtlArray.Item(i).top = CtlArray.Item(i).top * Yratio
    '                'If TypeOf CtlArray.Item(i) Is Label Then
    '                'CtlArray.Item(i).Fontsize = CtlArray.Item(i).FontSize * Yratio
    '                'End If
    '            End If
    '        End If
    '    Next
    'End Sub
End Class
