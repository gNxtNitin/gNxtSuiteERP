Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmTaxiINOUT
    Inherits System.Windows.Forms.Form
    Dim RsVisitorMain As ADODB.Recordset ''ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean
    Dim mAccountCode As String


    Private Const ConRowHeight As Short = 14

    Private Const ColDescription As Short = 1
    Private Const ColAvailable As Short = 2
    Private Const ColRemarks As Short = 3



    Private Sub cboPurpose_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboPurpose.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboPurpose_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboPurpose.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub cboPurpose_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles cboPurpose.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, cboPurpose.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub chkAutoInTime_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAutoInTime.CheckStateChanged
        If chkAutoInTime.CheckState = System.Windows.Forms.CheckState.Checked Then
            TxtINDate.Text = GetServerDate
            txtINTime.Text = GetServerTime
        Else
            TxtINDate.Text = ""
            txtINTime.Text = ""
        End If
    End Sub

    Private Sub chkAutoOutTime_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAutoOutTime.CheckStateChanged
        If chkAutoOutTime.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtOutDate.Text = GetServerDate
            txtOutTime.Text = GetServerTime
        End If
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
            If RsVisitorMain.EOF = False Then RsVisitorMain.MoveFirst()
            Show1()
            txtVNo.Enabled = True
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
        'Resume
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
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

        If txtINTime.Enabled = False Then
            MsgInformation("Slip Closed, Cann't be Deleted")
            Exit Sub
        End If

        If Not RsVisitorMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "PAY_TAXI_TRIP_TRN", (txtVNo.Text), RsVisitorMain) = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "PAY_TAXI_TRIP_TRN", "MKEY", (lblMKey.Text)) = False Then GoTo DelErrPart

                PubDBCn.Execute("DELETE FROM PAY_TAXI_TRIP_TRN WHERE MKEY=" & Val(lblMKey.Text) & "")
                PubDBCn.CommitTrans()
                RsVisitorMain.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
        RsVisitorMain.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr

        If TxtINDate.Enabled = False Then
            MsgInformation("Slip Closed, Cann't be Modified")
            Exit Sub
        End If

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsVisitorMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

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

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\TaxiSlip.RPT"

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

        MakeSQL = " SELECT *  FROM " & vbCrLf & " PAY_TAXI_TRIP_TRN IH"

        ''WHERE CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        MakeSQL = MakeSQL & vbCrLf & " AND IH.MKEY='" & MainClass.AllowSingleQuote(lblMkey.Text) & "'"
        'ORDER CLAUSE...
        '
        '    MakeSQL = MakeSQL & vbCrLf & "ORDER BY IH.REF_NO,IH.REF_DATE"
        '
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function GetOpeningReading(ByRef mTaxiNo As String) As Double

        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim I As Integer


        SqlStr = ""
        GetOpeningReading = 0
        ''SELECT CLAUSE...

        SqlStr = " SELECT MAX(CL_READING) As CL_READING  FROM " & vbCrLf & " PAY_TAXI_TRIP_TRN IH" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.TAXI_NO='" & MainClass.AllowSingleQuote(mTaxiNo) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetOpeningReading = IIf(IsDbNull(RsTemp.Fields("CL_READING").Value), 0, RsTemp.Fields("CL_READING").Value)
        End If

        If GetOpeningReading = 0 Then
            txtOPReading.Enabled = True
        Else
            txtOPReading.Enabled = False
        End If
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
        Dim mClosedFlag As String
        Dim mPurpose As String
        Dim mCardType As String

        Dim cntRow As Integer

        Dim mOutTime As String
        Dim mInTime As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mPurpose = VB.Left(cboPurpose.Text, 1)

        SqlStr = ""
        If Trim(txtVNo.Text) = "" Then
            mVNo = CDbl(AutoGenSeqRefNo("REF_NO"))
        Else
            mVNo = Val(txtVNo.Text)
        End If

        txtVNo.Text = VB6.Format(Val(CStr(mVNo)), "00000")

        If (txtOutDate.Text = "" Or IsDate(txtOutDate.Text) = False Or txtOutTime.Text = "") Then
            mOutTime = ""
        Else
            mOutTime = VB6.Format(txtOutDate.Text, "DD-MMM-YYYY") & " " & VB6.Format(txtOutTime.Text, "HH:MM")
        End If

        If (TxtINDate.Text = "" Or IsDate(TxtINDate.Text) = False Or txtINTime.Text = "") Then
            mInTime = ""
        Else
            mInTime = VB6.Format(TxtINDate.Text, "DD-MMM-YYYY") & " " & VB6.Format(txtINTime.Text, "HH:MM")
        End If

        If Val(txtCLReading.Text) <= 0 Then
            txtRunningKM.Text = VB6.Format(0, "0.00")
        Else
            txtRunningKM.Text = VB6.Format(Val(txtCLReading.Text) - Val(txtOPReading.Text), "0.00")
        End If

        If ADDMode = True Then
            mCurRowNo = MainClass.AutoGenRowNo("PAY_TAXI_TRIP_TRN", "RowNo", PubDBCn)
            nMkey = RsCompany.Fields("COMPANY_CODE").Value & RsCompany.Fields("FYEAR").Value & mCurRowNo

            lblMkey.Text = nMkey

            SqlStr = " INSERT INTO PAY_TAXI_TRIP_TRN ( " & vbCrLf & " MKEY, COMPANY_CODE , FYEAR, ROWNO," & vbCrLf & " REF_NO, REF_DATE," & vbCrLf & " TAXI_NO, DRIVER_NAME,USER_NAME," & vbCrLf & " APP_NAME, PURPOSE, OUT_TIME, " & vbCrLf & " IN_TIME, OP_READING, CL_READING, " & vbCrLf & " RUNNING_KM, " & vbCrLf & " ADDUSER, ADDDATE," & vbCrLf & " MODUSER,MODDATE ) "



            SqlStr = SqlStr & vbCrLf & " VALUES ( " & vbCrLf & " '" & nMkey & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & "," & vbCrLf & " " & mCurRowNo & ", " & Val(txtVNo.Text) & ", " & vbCrLf & " TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI'), " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtTaxiNo.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtDrivername.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtusername.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtAname.Text) & "', " & vbCrLf & " '" & mPurpose & "', " & vbCrLf & " TO_DATE('" & mOutTime & "','DD-MON-YYYY HH24:MI'), " & vbCrLf & " TO_DATE('" & mInTime & "','DD-MON-YYYY HH24:MI'), " & vbCrLf & " " & Val(txtOPReading.Text) & "," & vbCrLf & " " & Val(txtCLReading.Text) & "," & vbCrLf & " " & Val(txtRunningKM.Text) & "," & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"
        End If

        If MODIFYMode = True Then
            SqlStr = " UPDATE PAY_TAXI_TRIP_TRN SET " & vbCrLf & " REF_NO=" & Val(txtVNo.Text) & ", " & vbCrLf & " REF_DATE=TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI'), " & vbCrLf & " TAXI_NO='" & MainClass.AllowSingleQuote(txtTaxiNo.Text) & "', " & vbCrLf & " DRIVER_NAME='" & MainClass.AllowSingleQuote(txtDriverName.Text) & "', " & vbCrLf & " USER_NAME='" & MainClass.AllowSingleQuote(txtUserName.Text) & "', " & vbCrLf & " APP_NAME='" & MainClass.AllowSingleQuote(txtAname.Text) & "', " & vbCrLf & " PURPOSE='" & mPurpose & "'," & vbCrLf & " OUT_TIME=TO_DATE('" & mOutTime & "','DD-MON-YYYY HH24:MI'), " & vbCrLf & " IN_TIME=TO_DATE('" & mInTime & "','DD-MON-YYYY HH24:MI'), " & vbCrLf & " OP_READING=" & Val(txtOPReading.Text) & "," & vbCrLf & " CL_READING=" & Val(txtCLReading.Text) & "," & vbCrLf & " RUNNING_KM=" & Val(txtRunningKM.Text) & "," & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND MKEY =" & Val(lblMKey.Text) & ""
        End If

        PubDBCn.Execute(SqlStr)
        txtVNo.Text = VB6.Format(Val(CStr(mVNo)), "00000")

        Update1 = True
        PubDBCn.CommitTrans()

        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsVisitorMain.Requery()
        MsgBox(Err.Description)
        ''Resume
    End Function
    Private Function CheckPendingTaxi() As Boolean

        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        SqlStr = ""
        ''SELECT CLAUSE...
        CheckPendingTaxi = False

        SqlStr = " SELECT REF_NO, REF_DATE  FROM " & vbCrLf & " PAY_TAXI_TRIP_TRN IH " & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TAXI_NO='" & (txtTaxiNo).Text & "'" & vbCrLf & " AND " & vbCrLf & " (IN_TIME IS NULL OR IN_TIME='')"

        If MODIFYMode = True Then
            SqlStr = SqlStr & vbCrLf & " AND IH.MKEY<>'" & MainClass.AllowSingleQuote(lblMKey.Text) & "'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            CheckPendingTaxi = True
            MsgBox("Please Such Taxi No Slip is Pending. Ref. No is " & IIf(IsDbNull(RsTemp.Fields("REF_NO").Value), "", RsTemp.Fields("REF_NO").Value) & " & DATE : " & IIf(IsDbNull(RsTemp.Fields("REF_DATE").Value), "", RsTemp.Fields("REF_DATE").Value))
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function AutoGenSeqRefNo(ByRef mFieldName As String) As String

        On Error GoTo AutoGenSeqBillNoErr
        Dim RsGen As ADODB.Recordset
        Dim mNewSeqNo As Integer
        Dim SqlStr As String = ""

        SqlStr = ""
        mNewSeqNo = 1

        SqlStr = "SELECT Max(" & mFieldName & ")  FROM PAY_TAXI_TRIP_TRN " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " "
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
        MainClass.ButtonStatus(Me, XRIGHT, RsVisitorMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Public Sub frmTaxiINOUT_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        '    Me.Caption = "Visitor Slip Entry"

        SqlStr = "Select * From PAY_TAXI_TRIP_TRN WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsVisitorMain, ADODB.LockTypeEnum.adLockReadOnly)

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

        SqlStr = " SELECT " & vbCrLf & " REF_NO, TO_CHAR(REF_DATE,'DD/MM/YYYY HH24:MI') AS REF_DATE, " & vbCrLf & " TAXI_NO, DRIVER_NAME, USER_NAME, APP_NAME, " & vbCrLf & " DECODE(PURPOSE,'1','OFFICIAL','PERSONAL') AS PURPOSE, " & vbCrLf & " TO_CHAR(OUT_TIME,'HH24:MI') OUT_TIME," & vbCrLf & " TO_CHAR(IN_TIME,'HH24:MI') IN_TIME, RUNNING_KM " & vbCrLf & " FROM PAY_TAXI_TRIP_TRN " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

        SqlStr = SqlStr & " ORDER BY REF_NO,REF_DATE"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmTaxiINOUT_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn
        Call SetMainFormCordinate(Me)
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        cboPurpose.Items.Clear()
        cboPurpose.Items.Add("1. Official")
        cboPurpose.Items.Add("2. Personal")
        cboPurpose.SelectedIndex = 0




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

        mAccountCode = "-1"
        lblMKey.Text = ""
        txtVNo.Text = ""
        txtVDate.Text = GetServerDate & " " & GetServerTime

        txtTaxiNo.Text = ""
        txtDriverName.Text = ""
        txtUserName.Text = ""
        txtAname.Text = ""
        cboPurpose.SelectedIndex = -1
        txtOutDate.Text = ""
        txtOutTime.Text = ""
        txtINTime.Text = ""
        TxtINDate.Text = ""
        txtRunningKM.Text = ""
        txtOPReading.Text = ""
        txtCLReading.Text = ""

        chkAutoInTime.Enabled = True
        chkAutoOutTime.Enabled = True
        chkAutoInTime.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkAutoOutTime.CheckState = System.Windows.Forms.CheckState.Unchecked
        TxtINDate.Enabled = True
        txtINTime.Enabled = True

        txtVDate.Enabled = False


        MainClass.ButtonStatus(Me, XRIGHT, RsVisitorMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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

        txtVNo.Maxlength = RsVisitorMain.Fields("REF_NO").Precision
        txtVDate.Maxlength = 16

        txtTaxiNo.Maxlength = RsVisitorMain.Fields("TAXI_NO").DefinedSize
        txtDriverName.Maxlength = RsVisitorMain.Fields("DRIVER_NAME").DefinedSize
        txtUserName.Maxlength = RsVisitorMain.Fields("USER_NAME").DefinedSize
        txtAname.Maxlength = RsVisitorMain.Fields("APP_NAME").DefinedSize
        txtOutDate.Maxlength = 10
        txtOutTime.Maxlength = 5
        txtINTime.Maxlength = 5
        TxtINDate.Maxlength = 10
        txtRunningKM.Maxlength = RsVisitorMain.Fields("RUNNING_KM").Precision
        txtOPReading.Maxlength = RsVisitorMain.Fields("OP_READING").Precision
        txtCLReading.Maxlength = RsVisitorMain.Fields("CL_READING").Precision

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
        If MODIFYMode = True And RsVisitorMain.EOF = True Then Exit Function

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

        If Trim(cboPurpose.Text) = "" Then
            MsgInformation("Purpose is Blank.")
            FieldsVarification = False
            cboPurpose.Focus()
            Exit Function
        End If

        If Trim(txtTaxiNo.Text) = "" Then
            MsgInformation("Taxi No is Blank")
            FieldsVarification = False
            txtTaxiNo.Focus()
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable((txtTaxiNo.Text), "NAME", "NAME", "FIN_VEHICLE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invalid Taxi No.")
            FieldsVarification = False
            txtTaxiNo.Focus()
            Exit Function
        End If


        If CheckPendingTaxi = True Then
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtOutDate.Text) = "" Then
            MsgInformation(" Invalid Out Date. Cannot Save")
            txtOutDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If IsDate(txtOutDate.Text) = False Then
            MsgInformation(" Invalid Out Date. Cannot Save")
            txtOutDate.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtOutTime.Text) = "" Then
            MsgInformation(" Invalid Out Time. Cannot Save")
            txtOutTime.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If IsDate(txtOutTime.Text) = False Then
            MsgInformation(" Invalid Out Time. Cannot Save")
            txtOutTime.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Val(txtOPReading.Text) <= 0 Then
            MsgInformation(" Please Enter the Opening Reading. Cannot Save")
            If txtOPReading.Enabled = True Then txtOPReading.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(TxtINDate.Text) <> "" Then
            If IsDate(TxtINDate.Text) = False Then
                MsgInformation(" Invalid IN Date. Cannot Save")
                TxtINDate.Focus()
                FieldsVarification = False
                Exit Function
            End If
            If Trim(txtINTime.Text) = "" Then
                MsgInformation(" Invalid IN Time. Cannot Save")
                txtINTime.Focus()
                FieldsVarification = False
                Exit Function
            End If
            If IsDate(txtINTime.Text) = False Then
                MsgInformation(" Invalid IN Time. Cannot Save")
                txtINTime.Focus()
                FieldsVarification = False
                Exit Function
            End If


            mOutTime = VB6.Format(txtOutDate.Text, "DD-MMM-YYYY") & " " & VB6.Format(txtOutTime.Text, "HH:MM")
            mInTime = VB6.Format(TxtINDate.Text, "DD-MMM-YYYY") & " " & VB6.Format(txtINTime.Text, "HH:MM")

            If CDate(mInTime) < CDate(mOutTime) Then
                MsgInformation("IN Time Cann't be less than Out Time. Cannot Save")
                txtINTime.Focus()
                FieldsVarification = False
                Exit Function
            End If

            If Val(txtCLReading.Text) <= 0 Then
                MsgInformation(" Please Enter the Closing Reading. Cannot Save")
                If txtCLReading.Enabled = True Then txtCLReading.Focus()
                FieldsVarification = False
                Exit Function
            End If

            If Val(txtCLReading.Text) <= Val(txtOPReading.Text) Then
                MsgInformation("Closing Reading Cann't be less than Opening Reading. Cannot Save")
                If txtCLReading.Enabled = True Then txtCLReading.Focus()
                FieldsVarification = False
                Exit Function
            End If
            txtRunningKM.Text = CStr(Val(txtCLReading.Text) - Val(txtOPReading.Text))
        End If

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        'Resume
    End Function

    Private Sub frmTaxiINOUT_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next

        Me.hide()
        RsVisitorMain.Close()
        'RsOpOuts.Close
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        txtVNo.Text = SprdView.Text

        TxtVNo_Validating(TxtVNo, New System.ComponentModel.CancelEventArgs(False))
        ViewGrid()
    End Sub
    Private Sub txtAname_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAname.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtAname_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAname.DoubleClick
        Call SearchEmp(txtAname)
    End Sub
    Private Sub txtAname_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAname.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtAname.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtAname_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtAname.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchEmp(txtAname)
    End Sub

    Private Sub txtCLReading_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCLReading.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCLReading_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCLReading.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCLReading_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCLReading.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If Val(txtCLReading.Text) <= 0 Then GoTo EventExitSub

        If Val(txtCLReading.Text) <= Val(txtOPReading.Text) Then
            MsgInformation("Closing Reading Cann't be less than Opening Reading. Cannot Save")
            Cancel = True
            GoTo EventExitSub
        End If

        If Val(txtCLReading.Text) <= 0 Then
            txtRunningKM.Text = VB6.Format(0, "0.00")
        Else
            txtRunningKM.Text = VB6.Format(Val(txtCLReading.Text) - Val(txtOPReading.Text), "0.00")
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDriverName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDrivername.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDriverName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDrivername.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDrivername.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtINDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtINDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtInDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(TxtINDate.Text) = "" Then GoTo EventExitSub

        If Not IsDate(TxtINDate.Text) Then
            MsgBox("Invalid Out Time.", MsgBoxStyle.Information)
            TxtINDate.Text = ""
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtINTime_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInTime.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtINTime_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtInTime.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Len(txtINTime.Text) = 4 Then
            txtINTime.Text = VB6.Format(VB.Left(txtINTime.Text, 2), "00") & ":" & VB6.Format(VB.Right(txtINTime.Text, 2), "00")
        End If
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtOPReading_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOPReading.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOPReading_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOPReading.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtOPReading_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtOPReading.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Val(txtCLReading.Text) <= 0 Then
            txtRunningKM.Text = VB6.Format(0, "0.00")
        Else
            txtRunningKM.Text = VB6.Format(Val(txtCLReading.Text) - Val(txtOPReading.Text), "0.00")
        End If
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtOutDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOutDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOutDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtOutDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtOutDate.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtOutDate.Text) Then
            MsgBox("Invalid Out Time.", MsgBoxStyle.Information)
            txtOutDate.Text = ""
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtOutTime_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOutTime.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOutTime_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtOutTime.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Len(txtOutTime.Text) = 4 Then
            txtOutTime.Text = VB6.Format(VB.Left(txtOutTime.Text, 2), "00") & ":" & VB6.Format(VB.Right(txtOutTime.Text, 2), "00")
        End If
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtrunningKM_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRunningKM.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtrunningKM_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRunningKM.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTaxiNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTaxiNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTaxiNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTaxiNo.DoubleClick
        SearchVehicleMaster()
    End Sub


    Private Sub txtTaxiNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTaxiNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtTaxiNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTaxiNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtTaxiNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchVehicleMaster()
    End Sub

    Private Sub txtTaxiNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTaxiNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtTaxiNo.Text) = "" Then
            txtOPReading.Text = CStr(0)
            GoTo EventExitSub
        End If
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable((txtTaxiNo.Text), "NAME", "NAME", "FIN_VEHICLE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgInformation("Invalid Taxi No")
            txtOPReading.Text = CStr(0)
            Cancel = True
        End If

        txtOPReading.Text = CStr(GetOpeningReading(txtTaxiNo.Text))

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub SearchVehicleMaster()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtTaxiNo.Text), "FIN_VEHICLE_MST", "NAME", , , , SqlStr) = True Then
            txtTaxiNo.Text = AcName
            txtTaxiNo_Validating(txtTaxiNo, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtusername_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtusername.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtusername_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtusername.DoubleClick
        Call SearchEmp(txtUserName)
    End Sub
    Private Sub txtusername_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtusername.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtusername.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtusername_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtusername.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchEmp(txtUserName)
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
        If Not RsVisitorMain.EOF Then
            With RsVisitorMain
                lblMKey.Text = IIf(IsDbNull(.Fields("mKey").Value), "", .Fields("mKey").Value)
                txtVNo.Text = IIf(IsDbNull(.Fields("REF_NO").Value), "", .Fields("REF_NO").Value)
                txtVDate.Text = VB6.Format(IIf(IsDbNull(.Fields("REF_DATE").Value), "", .Fields("REF_DATE").Value), "DD/MM/YYYY HH:MM")


                txtTaxiNo.Text = IIf(IsDbNull(.Fields("TAXI_NO").Value), "", .Fields("TAXI_NO").Value)
                txtDriverName.Text = IIf(IsDbNull(.Fields("DRIVER_NAME").Value), "", .Fields("DRIVER_NAME").Value)
                txtUserName.Text = IIf(IsDbNull(.Fields("USER_NAME").Value), "", .Fields("USER_NAME").Value)
                txtAname.Text = IIf(IsDbNull(.Fields("APP_NAME").Value), "", .Fields("APP_NAME").Value)

                txtOPReading.Text = IIf(IsDbNull(.Fields("OP_READING").Value), 0, .Fields("OP_READING").Value)
                txtCLReading.Text = IIf(IsDbNull(.Fields("CL_READING").Value), "", .Fields("CL_READING").Value)
                txtRunningKM.Text = CStr(Val(txtCLReading.Text) - Val(txtOPReading.Text))

                If RsVisitorMain.Fields("PURPOSE").Value = "1" Then
                    cboPurpose.SelectedIndex = 0
                ElseIf RsVisitorMain.Fields("PURPOSE").Value = "2" Then
                    cboPurpose.SelectedIndex = 1
                End If


                txtOutDate.Text = VB6.Format(IIf(IsDbNull(.Fields("OUT_TIME").Value), "", .Fields("OUT_TIME").Value), "DD/MM/YYYY")
                txtOutTime.Text = VB6.Format(IIf(IsDbNull(.Fields("OUT_TIME").Value), "", .Fields("OUT_TIME").Value), "HH:MM")

                TxtINDate.Text = VB6.Format(IIf(IsDbNull(.Fields("IN_TIME").Value), "", .Fields("IN_TIME").Value), "DD/MM/YYYY")
                txtINTime.Text = VB6.Format(IIf(IsDbNull(.Fields("IN_TIME").Value), "", .Fields("IN_TIME").Value), "HH:MM")

                TxtINDate.Enabled = IIf(Trim(TxtINDate.Text) = "", True, False)
                txtINTime.Enabled = IIf(Trim(txtINTime.Text) = "", True, False)

                chkAutoInTime.Enabled = IIf(Trim(TxtINDate.Text) = "", True, False)
                chkAutoOutTime.Enabled = IIf(Trim(txtINTime.Text) = "", True, False)

                '            txtVNo.Enabled = False

            End With
        End If
        ADDMode = False
        MODIFYMode = False

        MainClass.ButtonStatus(Me, XRIGHT, RsVisitorMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Private Sub txtVDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtVno_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtVNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
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


        If MODIFYMode = True And RsVisitorMain.BOF = False Then xMkey = RsVisitorMain.Fields("mKey").Value

        SqlStr = "SELECT * FROM PAY_TAXI_TRIP_TRN " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND REF_NO=" & Val(mVNo) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsVisitorMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsVisitorMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such REf No. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM PAY_TAXI_TRIP_TRN WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MKEY=" & Val(xMkey) & ""

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsVisitorMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub SearchEmp(ByRef pTextBox As System.Windows.Forms.TextBox)
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        '        If ADDMode = True Then
        SqlStr = SqlStr & " AND (EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE='')"
        '        Else
        '            SqlStr = SqlStr & vbCrLf & " AND (EMP_LEAVE_DATE IS NULL OR (EMP_LEAVE_DATE >= '" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "'))"
        '        End If

        If MainClass.SearchGridMaster((pTextBox.Text), "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            pTextBox.Text = AcName
            '            txtEmailIDName.Text = AcName1
            '            txtEmailID_Validate False
            '            If txtEmailID.Enabled = True Then txtEmailID.SetFocus
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
End Class
