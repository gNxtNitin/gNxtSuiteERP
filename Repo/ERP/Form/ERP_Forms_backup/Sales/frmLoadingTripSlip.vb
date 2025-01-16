Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmLoadingTripSlip
    Inherits System.Windows.Forms.Form
    Dim RsLoadMain As ADODB.Recordset

    'Dim PvtDBCn As ADODB.Connection

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim FormActive As Boolean

    Dim CurMKey As String
    Dim SqlStr As String = ""

    Private Const ConRowHeight As Short = 15


    Dim pMenu As String

    Private Sub chkStatus_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkStatus.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtSlipNo.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            If RsLoadMain.EOF = False Then RsLoadMain.MoveFirst()
            Show1()
            '        ShowInvoiceData
            txtSlipNo.Enabled = True
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
        'Resume
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If ValidateBranchLocking((txtSlipDate.Text)) = True Then
            Exit Sub
        End If

        If txtSlipNo.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub

        If Not RsLoadMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Cancel()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "DSP_TRIP_HDR", (txtSlipNo.Text), RsLoadMain, "", "D") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "DSP_TRIP_HDR", "AUTO_KEY_TRIP", (txtSlipNo.Text)) = False Then GoTo DelErrPart

                PubDBCn.Execute("DELETE FROM DSP_TRIP_HDR WHERE AUTO_KEY_TRIP=" & Val(txtSlipNo.Text) & "")

                PubDBCn.CommitTrans()
                RsLoadMain.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Cancel()
        RsLoadMain.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdLoaction_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdLoaction.Click
        SearchLocationMaster()
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsLoadMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            txtSlipNo.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String)
        On Error GoTo ErrPart

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, False, pMenu)
        Report1.ReportFileName = PubReportFolderPath & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        Report1.Action = 1
        Report1.Reset()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Sub ShowTermsReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.ReportFileName = My.Application.Info.DirectoryPath & mRPTName
        'Report1.SQLQuery = mSqlStr
        'Report1.WindowShowGroupTree = False
        Report1.Action = 1

    End Sub

    Private Function AlreadyLoad(ByRef pRefNo As Double, ByRef pLoadingNo As Double) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim I As Integer

        AlreadyLoad = False
        pLoadingNo = 0
        SqlStr = " SELECT DISTINCT IH.AUTO_KEY_LOAD" & vbCrLf & " FROM DSP_LOADING_HDR IH" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.BOOKTYPE='" & lblBookType.Text & "'" & vbCrLf & " AND IH.CSLIP_NO=" & Val(CStr(pRefNo)) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


        If RsTemp.EOF = False Then
            pLoadingNo = IIf(IsDbNull(RsTemp.Fields("AUTO_KEY_LOAD").Value), 0, RsTemp.Fields("AUTO_KEY_LOAD").Value)
            AlreadyLoad = True
        End If

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function PendingCollection(ByRef pRefNo As String, ByRef pLoadingNo As Double) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim I As Integer

        PendingCollection = False
        pLoadingNo = 0

        SqlStr = " SELECT AUTO_KEY_TRIP" & vbCrLf & " FROM DSP_TRIP_HDR IH" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " -- AND IH.BOOKTYPE='" & lblBookType.Text & "'" & vbCrLf & " AND IH.VEHICLE_NO='" & MainClass.AllowSingleQuote((txtVehicleNo.Text)) & "'" & vbCrLf & " AND IH.AUTO_KEY_TRIP <> " & Val(pRefNo) & "" & vbCrLf & " AND IH.AUTO_KEY_TRIP NOT IN (" & vbCrLf
        SqlStr = SqlStr & " SELECT DISTINCT CSLIP_NO" & vbCrLf & " FROM DSP_LOADING_HDR" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " -- AND BOOKTYPE='" & lblBookType.Text & "'" & vbCrLf & " AND VEHICLE_NO='" & MainClass.AllowSingleQuote((txtVehicleNo.Text)) & "')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


        If RsTemp.EOF = False Then
            pLoadingNo = IIf(IsDbNull(RsTemp.Fields("AUTO_KEY_TRIP").Value), 0, RsTemp.Fields("AUTO_KEY_TRIP").Value)
            PendingCollection = True
        End If

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportonDespatch(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportonDespatch(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If UpdateMain1 = True Then
            ADDMode = False
            MODIFYMode = False
            txtSlipNo_Validating(txtSlipNo, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True And CmdAdd.Visible = True Then CmdAdd.Focus()
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
    Private Sub ReportonDespatch(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""
        Dim SqlStr As String = ""
        Dim mRptFileName As String
        Dim mReportPrint As Boolean

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        SqlStr = ""

        Call MainClass.ClearCRptFormulas(Report1)

        Call SelectQryForDespatch(SqlStr)


        mTitle = "Collection Trip Slip"
        mSubTitle = ""
        mRptFileName = "CollectionSlip.rpt"

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)



        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Function SelectQryForDespatch(ByRef mSqlStr As String) As String

        ''SELECT CLAUSE...

        mSqlStr = " SELECT " & vbCrLf & " IH.*"

        ''FROM CLAUSE...

        mSqlStr = mSqlStr & vbCrLf & " FROM DSP_TRIP_HDR IH"

        ''WHERE CLAUSE...
        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_TRIP=" & Val(txtSlipNo.Text) & ""

        ''ORDER CLAUSE...
        '
        '    mSqlStr = mSqlStr & vbCrLf & "ORDER BY ID.SERIAL_NO"

        SelectQryForDespatch = mSqlStr
    End Function
    Private Function UpdateMain1() As Boolean
        On Error GoTo ErrPart
        Dim I As Short
        Dim SqlStr As String = ""
        Dim nMkey As String
        Dim mVNoSeq As Double
        Dim mDivisionCode As Double
        Dim mEntryDate As String
        Dim mStatus As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mEntryDate = VB6.Format(PubCurrDate, "DD-MMM-YYYY") & " " & GetServerTime

        '    If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mDivisionCode = Trim(MasterNo)
        '    End If

        If Val(txtSlipNo.Text) = 0 Then
            mVNoSeq = CDbl(AutoGenSeqNo())
        Else
            mVNoSeq = Val(txtSlipNo.Text)
        End If

        txtSlipNo.Text = CStr(Val(CStr(mVNoSeq)))

        SqlStr = ""

        mStatus = IIf(chkStatus.CheckState = System.Windows.Forms.CheckState.Checked, "C", "O")

        If ADDMode = True Then
            mStatus = "O" ''Addmode Status Always Open.

            lblMkey.Text = CStr(mVNoSeq)
            SqlStr = "INSERT INTO DSP_TRIP_HDR( " & vbCrLf & " COMPANY_CODE, AUTO_KEY_TRIP, TRIP_DATE," & vbCrLf & " VEHICLE_NO, FREIGHT_TYPE, " & vbCrLf & " TRANSPORTER_NAME, VEHICLE_TYPE, REMARKS," & vbCrLf & " ADDUSER, ADDDATE, " & vbCrLf & " MODUSER, MODDATE,BOOKTYPE, TRANSPORTER_TRIP_NO, TRANSPORTER_TRIP_DATE,STATUS, SUPP_LOCATION, TRIP_AMOUNT) "

            SqlStr = SqlStr & vbCrLf & " VALUES(" & RsCompany.Fields("COMPANY_CODE").Value & ", " & Val(CStr(mVNoSeq)) & ", TO_DATE('" & mEntryDate & "','DD-MON-YYYY HH24:MI')," & vbCrLf & " '" & MainClass.AllowSingleQuote((txtVehicleNo.Text)) & "', '" & IIf(optFreightType(0).Checked = True, "R", "P") & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtTransporterName.Text)) & "', '" & MainClass.AllowSingleQuote((txtVehicleType.Text)) & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote((txtRemarks.Text)) & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','','" & lblBookType.Text & "'," & vbCrLf & " " & Val(txtTripNo.Text) & ", TO_DATE('" & VB6.Format(txtTripDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & mStatus & "','" & MainClass.AllowSingleQuote((txtLocation.Text)) & "'," & Val(txtAmount.Text) & ")"

        ElseIf MODIFYMode = True Then

            SqlStr = ""
            SqlStr = "UPDATE DSP_TRIP_HDR SET " & vbCrLf & " AUTO_KEY_TRIP =" & Val(CStr(mVNoSeq)) & " , FREIGHT_TYPE='" & IIf(optFreightType(0).Checked = True, "R", "P") & "', " & vbCrLf & " TRANSPORTER_TRIP_NO='" & MainClass.AllowSingleQuote((txtTripNo.Text)) & "', SUPP_LOCATION='" & MainClass.AllowSingleQuote((txtLocation.Text)) & "',TRIP_AMOUNT=" & Val(txtAmount.Text) & "," & vbCrLf & " TRANSPORTER_TRIP_DATE=TO_DATE('" & VB6.Format(txtTripDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " TRANSPORTER_NAME='" & MainClass.AllowSingleQuote((txtTransporterName.Text)) & "', " & vbCrLf & " VEHICLE_NO='" & MainClass.AllowSingleQuote((txtVehicleNo.Text)) & "', " & vbCrLf & " REMARKS='" & MainClass.AllowSingleQuote((txtRemarks.Text)) & "', VEHICLE_TYPE='" & MainClass.AllowSingleQuote((txtVehicleType.Text)) & "'," & vbCrLf & " BOOKTYPE='" & lblBookType.Text & "',STATUS= '" & mStatus & "'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE AUTO_KEY_TRIP ='" & MainClass.AllowSingleQuote((lblMKey.Text)) & "'"
        End If

        PubDBCn.Execute(SqlStr)

        UpdateMain1 = True
        PubDBCn.CommitTrans()

        Exit Function
ErrPart:
        UpdateMain1 = False
        PubDBCn.RollbackTrans() ''
        RsLoadMain.Requery() ''.Refresh

        If Err.Description = "" Then Exit Function
        If Err.Number = -2147217900 Then
            ErrorMsg("Duplicate Load No. Generated, Save Again", "Duplicate", MsgBoxStyle.Critical)
        Else
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        ''Resume
    End Function


    Private Function AutoGenSeqNo() As String

        On Error GoTo AutoGenSeqNoErr
        Dim RsDNMainGen As ADODB.Recordset = Nothing
        Dim mNewSeqNo As Integer
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mStartingSNo As Integer
        Dim mMaxValue As String

        SqlStr = ""
        mStartingSNo = IIf(lblBookType.Text = "U", 1, 50001)
        mNewSeqNo = mStartingSNo

        SqlStr = "SELECT Max(AUTO_KEY_TRIP)  " & vbCrLf & " FROM DSP_TRIP_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_TRIP,LENGTH(AUTO_KEY_TRIP)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND BOOKTYPE='" & lblBookType.Text & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDNMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsDNMainGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mMaxValue = .Fields(0).Value
                    mNewSeqNo = CInt(Mid(mMaxValue, 1, Len(mMaxValue) - 6))
                    mNewSeqNo = mNewSeqNo + 1
                Else
                    mNewSeqNo = mStartingSNo '' 1
                End If
            End If
        End With
        AutoGenSeqNo = mNewSeqNo & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        Exit Function
AutoGenSeqNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function


    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchVehicleMaster()
    End Sub

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ViewGrid()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub


    Public Sub frmLoadingTripSlip_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        Me.Text = "Collection Gate Pass" '' IIf(lblBookType.text = "L", "Loading Slip", "Unloading Slip")

        SqlStr = ""
        SqlStr = "Select * from DSP_TRIP_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLoadMain, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call SetTextLengths()
        If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub

    Private Sub frmLoadingTripSlip_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmLoadingTripSlip_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        pMenu = myMenu
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        txtSlipDate.Enabled = False
        txtTripDate.Enabled = True
        ADDMode = False
        MODIFYMode = False
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(4905)
        ''Me.Width = VB6.TwipsToPixelsX(9090)


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub SetTextLengths()

        On Error GoTo SetTextLengthsErr
        txtSlipNo.Maxlength = RsLoadMain.Fields("AUTO_KEY_TRIP").Precision
        txtSlipDate.Maxlength = 20

        txtTripNo.Maxlength = RsLoadMain.Fields("TRANSPORTER_TRIP_NO").Precision
        txtTripDate.Maxlength = 20

        txtVehicleNo.Maxlength = MainClass.SetMaxLength("NAME", "FIN_VEHICLE_MST", PubDBCn)
        txtTransporterName.Maxlength = MainClass.SetMaxLength("TRANSPORTER_NAME", "FIN_VEHICLE_MST", PubDBCn)
        txtVehicleType.Maxlength = MainClass.SetMaxLength("VEHICLE_TYPE", "FIN_VEHICLE_MST", PubDBCn)
        txtLocation.Maxlength = RsLoadMain.Fields("SUPP_LOCATION").DefinedSize


        '    txtRefNo
        txtRemarks.Maxlength = RsLoadMain.Fields("REMARKS").DefinedSize

        Exit Sub
SetTextLengthsErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub Clear1()


        txtSlipNo.Text = ""
        txtSlipDate.Text = VB6.Format(PubCurrDate, "DD/MM/YYYY") & " " & GetServerTime

        txtTripNo.Text = ""
        txtTripDate.Text = VB6.Format(PubCurrDate, "DD/MM/YYYY")

        txtVehicleNo.Text = ""
        txtTransporterName.Text = ""
        txtVehicleType.Text = ""
        txtLocation.Text = ""
        txtAmount.Text = "0.00"

        txtRemarks.Text = ""

        chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkStatus.Enabled = True

        optFreightType(0).Checked = False
        optFreightType(1).Checked = False

        lblAddUser.Text = ""
        lblAddDate.Text = ""
        lblModUser.Text = ""
        lblModDate.Text = ""

        MainClass.ButtonStatus(Me, XRIGHT, RsLoadMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub Show1()

        On Error GoTo ERR1
        Dim mDivisionCode As Double
        Dim mDivisionDesc As String

        With RsLoadMain
            If Not .EOF Then
                lblMkey.Text = .Fields("AUTO_KEY_TRIP").Value
                txtSlipNo.Text = IIf(IsDbNull(.Fields("AUTO_KEY_TRIP").Value), "", .Fields("AUTO_KEY_TRIP").Value)
                txtSlipDate.Text = VB6.Format(IIf(IsDbNull(.Fields("TRIP_DATE").Value), "", .Fields("TRIP_DATE").Value), "DD/MM/YYYY HH:MM") '' VB6.Format(IIf(IsNull(.Fields("TRIP_DATE").Value), "", .Fields("TRIP_DATE").Value), "DD/MM/YYYY")

                txtTripNo.Text = IIf(IsDbNull(.Fields("TRANSPORTER_TRIP_NO").Value), "", .Fields("TRANSPORTER_TRIP_NO").Value)
                txtTripDate.Text = VB6.Format(IIf(IsDbNull(.Fields("TRANSPORTER_TRIP_DATE").Value), "", .Fields("TRANSPORTER_TRIP_DATE").Value), "DD/MM/YYYY")

                txtVehicleNo.Text = IIf(IsDbNull(.Fields("VEHICLE_NO").Value), "", .Fields("VEHICLE_NO").Value)
                txtTransporterName.Text = IIf(IsDbNull(.Fields("TRANSPORTER_NAME").Value), "", .Fields("TRANSPORTER_NAME").Value)
                txtVehicleType.Text = IIf(IsDbNull(.Fields("VEHICLE_TYPE").Value), "", .Fields("VEHICLE_TYPE").Value)
                txtRemarks.Text = IIf(IsDbNull(.Fields("Remarks").Value), "", .Fields("Remarks").Value)

                txtLocation.Text = IIf(IsDbNull(.Fields("SUPP_LOCATION").Value), "", .Fields("SUPP_LOCATION").Value)
                txtAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("TRIP_AMOUNT").Value), 0, .Fields("TRIP_AMOUNT").Value), "0.00")

                chkStatus.CheckState = IIf(.Fields("Status").Value = "O", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
                chkStatus.Enabled = IIf(.Fields("Status").Value = "O", True, False)

                If .Fields("FREIGHT_TYPE").Value = "R" Then
                    optFreightType(0).Checked = True
                Else
                    optFreightType(1).Checked = True
                End If


                lblAddUser.Text = IIf(IsDbNull(.Fields("ADDUSER").Value), "", .Fields("ADDUSER").Value)
                lblAddDate.Text = VB6.Format(IIf(IsDbNull(.Fields("ADDDATE").Value), "", .Fields("ADDDATE").Value), "DD/MM/YYYY")
                lblModUser.Text = IIf(IsDbNull(.Fields("MODUSER").Value), "", .Fields("MODUSER").Value)
                lblModDate.Text = VB6.Format(IIf(IsDbNull(.Fields("MODDATE").Value), "", .Fields("MODDATE").Value), "DD/MM/YYYY")


                '            mDivisionCode = IIf(IsNull(!DIV_CODE), -1, !DIV_CODE)
                '
                '            If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '                mDivisionDesc = Trim(MasterNo)
                '                cboDivision.Text = mDivisionDesc
                '            End If

            End If
        End With
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsLoadMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

        txtSlipNo.Enabled = True

        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim pLoadingNo As Double

        FieldsVarification = True
        If ValidateBranchLocking((txtSlipDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsLoadMain.EOF = True Then Exit Function

        If MODIFYMode = True And Trim(txtSlipNo.Text) = "" Then
            MsgInformation("Slip No. is Blank")
            FieldsVarification = False
            Exit Function
        End If


        If Trim(txtSlipDate.Text) = "" Then
            MsgInformation(" Slip Date is empty. Cannot Save")
            txtSlipDate.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtSlipDate.Text) <> "" Then
            If IsDate(txtSlipDate.Text) = False Then
                MsgInformation(" Invalid Slip Date. Cannot Save")
                txtSlipDate.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If FYChk(VB6.Format(txtSlipDate.Text, "DD/MM/YYYY")) = False Then
            FieldsVarification = False
            If txtSlipDate.Enabled = True Then txtSlipDate.Focus()
            Exit Function
        End If

        If Trim(txtTripDate.Text) = "" Then
            MsgInformation("Trip Date is empty. Cannot Save")
            txtTripDate.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtTripDate.Text) <> "" Then
            If IsDate(txtTripDate.Text) = False Then
                MsgInformation("Invalid Trip Date. Cannot Save")
                txtTripDate.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If Trim(txtVehicleNo.Text) = "" Then
            MsgInformation("Vehicle No is Blank. Cannot Save")
            If txtVehicleNo.Enabled = True Then txtVehicleNo.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Val(txtSlipNo.Text) <> 0 Then
            If AlreadyLoad(Val(txtSlipNo.Text), pLoadingNo) = True Then
                MsgInformation("Trip Already Completed in Unloading Slip No " & pLoadingNo & ". Cannot Modify")
                If CmdSave.Enabled = True Then CmdSave.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If optFreightType(0).Checked = False And optFreightType(1).Checked = False Then
            MsgInformation("Please Select The Freight Type.")
            FieldsVarification = False
            Exit Function
        End If

        If Val(txtTripNo.Text) = 0 Then
            MsgInformation("Transport Trip no is Blank.Cann't be Save")
            If CmdSave.Enabled = True Then CmdSave.Focus()
            FieldsVarification = False
            Exit Function

        End If


        If Val(txtAmount.Text) <= 0 Then
            MsgInformation("Please Enter the Trip Amount.Cann't be Save")
            If txtAmount.Enabled = True Then txtAmount.Focus()
            FieldsVarification = False
            Exit Function

        End If

        If chkStatus.CheckState = System.Windows.Forms.CheckState.Checked And chkStatus.Enabled = False Then
            MsgInformation("Slip Already Closed, so cann't be Save.")
            If CmdSave.Enabled = True Then CmdSave.Focus()
            FieldsVarification = False
            Exit Function
        End If


        If PendingCollection((txtSlipNo.Text), pLoadingNo) = True Then
            MsgInformation("Collection Gatepass " & pLoadingNo & " is Pending for such Vechile. Please Make Loading/Unloading Slip first for Such Vechile.")
            If CmdSave.Enabled = True Then CmdSave.Focus()
            FieldsVarification = False
            Exit Function
        End If
        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function
    Private Sub ViewGrid()

        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            AssignGrid(True)
            '        ADataPPOMain.Refresh
            SprdView.Refresh()
            SprdView.Focus()
            FraTop.Visible = False
            Frabot.Visible = False
            SprdView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraTop.Visible = True
            Frabot.Visible = True
            SprdView.SendToBack()
        End If
        Call FormatSprdView()
        MainClass.ButtonStatus(Me, XRIGHT, RsLoadMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub frmLoadingTripSlip_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsLoadMain.Close()
        'PvtDBCn.Close
        RsLoadMain = Nothing
        'Set PvtDBCn = Nothing
    End Sub

    Private Sub optFreightType_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optFreightType.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optFreightType.GetIndex(eventSender)

            MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        txtSlipNo.Text = SprdView.Text

        txtSlipNo_Validating(txtSlipNo, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Private Sub txtAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAmount.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtLocation_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLocation.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SearchLocationMaster()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If lblBookType.Text = "U" Then
            If MainClass.SearchGridMaster((txtLocation.Text), "FIN_LOCATION_RATE_HDR", "SUPP_LOCATION", , , , SqlStr) = True Then
                txtLocation.Text = AcName
                txtLocation_Validating(txtLocation, New System.ComponentModel.CancelEventArgs(False))
            End If
        Else
            If MainClass.SearchGridMaster((txtLocation.Text), "FIN_SUPP_CUST_HDR", "SUPP_CUST_NAME", , , , SqlStr) = True Then
                txtLocation.Text = AcName
                txtLocation_Validating(txtLocation, New System.ComponentModel.CancelEventArgs(False))
            End If
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtLocation_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLocation.DoubleClick
        SearchLocationMaster()
    End Sub

    Private Sub txtLocation_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLocation.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtLocation.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtLocation_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtLocation.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchLocationMaster()
    End Sub

    Private Sub txtLocation_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtLocation.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mAmount As Double

        If Trim(txtLocation.Text) = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If lblBookType.Text = "U" Then
            If MainClass.ValidateWithMasterTable((txtLocation.Text), "SUPP_LOCATION", "SUPP_LOCATION", "FIN_LOCATION_RATE_HDR", PubDBCn, MasterNo, , SqlStr) = False Then
                MsgInformation("Invalid Location")
                Cancel = True
            Else
                mAmount = GetVehicleRateUnLoading(txtVehicleNo.Text, "T", IIf(optFreightType(0).Checked = True, "R", "P"))
                txtAmount.Text = VB6.Format(mAmount, "0.00")
            End If
        Else
            If MainClass.ValidateWithMasterTable((txtLocation.Text), "SUPP_CUST_NAME", "SUPP_CUST_NAME", "FIN_SUPP_CUST_HDR", PubDBCn, MasterNo, , SqlStr) = False Then
                MsgInformation("Invalid Customer")
                Cancel = True
            Else
                mAmount = GetVehicleRateLoading(txtVehicleNo.Text, "T", IIf(optFreightType(0).Checked = True, "R", "P"))
                txtAmount.Text = VB6.Format(mAmount, "0.00")
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Function GetVehicleRateLoading(ByRef mVehicleNo As String, ByRef mFieldType As String, ByRef mFreightType As String) As Double

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCustomerCode As String
        Dim mCustName As String
        Dim mField1 As String = ""
        Dim mField2 As String = ""
        Dim mPoint As Double

        GetVehicleRateLoading = 0

        If Trim(mVehicleNo) = "" Then
            GetVehicleRateLoading = 0
            Exit Function
        End If


        mCustName = Trim(txtLocation.Text)

        If mCustName = "" Then
            GetVehicleRateLoading = 0
            Exit Function
        Else
            If MainClass.ValidateWithMasterTable(mCustName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mCustomerCode = Trim(MasterNo)
            Else
                GetVehicleRateLoading = 0
                Exit Function
            End If
        End If

        If mFieldType = "T" Then
            mField1 = "TRIP_RATE"
            mField2 = IIf(mFreightType = "R", "DEFAULT_TRIP_RATE", "PREMIUM_RATE")
        ElseIf mFieldType = "P" Then
            '        mPoint = GetVehiclePoint(mCustName)
            '        If mPoint = 0 Then
            '            GetVehicleRateLoading = 0
            '            Exit Function
            '        End If
            '        mField1 = "POINT_RATE"
            '        mField2 = "DEFAULT_POINT_RATE"
        End If
        SqlStr = "SELECT " & mField1 & " AS TRIP_RATE FROM FIN_VEHICLE_RATE_DET" & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE = '" & MainClass.AllowSingleQuote(mCustomerCode) & "'" & vbCrLf & " AND VEHICLE_NO = '" & MainClass.AllowSingleQuote(mVehicleNo) & "'" & vbCrLf & " AND WEF = (" & vbCrLf & " SELECT MAX(WEF) FROM FIN_VEHICLE_RATE_DET" & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE = '" & MainClass.AllowSingleQuote(mCustomerCode) & "'" & vbCrLf & " AND VEHICLE_NO = '" & MainClass.AllowSingleQuote(mVehicleNo) & "'" & vbCrLf & " AND WEF <= TO_DATE('" & VB6.Format(txtSlipDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetVehicleRateLoading = IIf(IsDbNull(RsTemp.Fields("TRIP_RATE").Value), 0, RsTemp.Fields("TRIP_RATE").Value)
        Else
            SqlStr = "SELECT " & mField1 & " AS TRIP_RATE FROM FIN_VEHICLE_TP_RATE_DET" & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE = '" & MainClass.AllowSingleQuote(mCustomerCode) & "'" & vbCrLf & " AND TRANSPORT_NAME = '" & MainClass.AllowSingleQuote(txtTransporterName.Text) & "'" & vbCrLf & " AND VT_NAME = '" & MainClass.AllowSingleQuote(txtVehicleType.Text) & "'" & vbCrLf & " AND WEF = (" & vbCrLf & " SELECT MAX(WEF) FROM FIN_VEHICLE_TP_RATE_DET" & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE = '" & MainClass.AllowSingleQuote(mCustomerCode) & "'" & vbCrLf & " AND TRANSPORT_NAME = '" & MainClass.AllowSingleQuote(txtTransporterName.Text) & "'" & vbCrLf & " AND VT_NAME = '" & MainClass.AllowSingleQuote(txtVehicleType.Text) & "'" & vbCrLf & " AND WEF <= TO_DATE('" & VB6.Format(txtSlipDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                GetVehicleRateLoading = IIf(IsDbNull(RsTemp.Fields("TRIP_RATE").Value), 0, RsTemp.Fields("TRIP_RATE").Value)
            Else
                SqlStr = "SELECT " & mField2 & " AS TRIP_RATE FROM FIN_VEHICLE_RATE_HDR" & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE = '" & MainClass.AllowSingleQuote(mCustomerCode) & "'" & vbCrLf & " AND WEF = (" & vbCrLf & " SELECT MAX(WEF) FROM FIN_VEHICLE_RATE_HDR" & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE = '" & MainClass.AllowSingleQuote(mCustomerCode) & "'" & vbCrLf & " AND WEF <= TO_DATE('" & VB6.Format(txtSlipDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                If RsTemp.EOF = False Then
                    GetVehicleRateLoading = IIf(IsDbNull(RsTemp.Fields("TRIP_RATE").Value), 0, RsTemp.Fields("TRIP_RATE").Value)
                End If
            End If
        End If
        If mFieldType = "P" Then
            GetVehicleRateLoading = GetVehicleRateLoading * mPoint
        End If
        Exit Function
ERR1:
        ErrorMsg(Err.Description, err.Number, MsgBoxStyle.Critical)
    End Function
    Private Function GetVehicleRateUnLoading(ByRef mVehicleNo As String, ByRef mFieldType As String, ByRef mFreightType As String) As Double

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mCustomerCode As String = ""
        Dim mCustName As String = ""
        Dim mField1 As String = ""
        Dim mField2 As String = ""
        Dim mPoint As Double = ""

        GetVehicleRateUnLoading = 0

        If Trim(mVehicleNo) = "" Then
            GetVehicleRateUnLoading = 0
            Exit Function
        End If

        If Trim(txtLocation.Text) = "" Then
            GetVehicleRateUnLoading = 0
            Exit Function
        Else
            If MainClass.ValidateWithMasterTable(Trim(txtLocation.Text), "SUPP_LOCATION", "SUPP_LOCATION", "FIN_LOCATION_RATE_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mCustomerCode = Trim(MasterNo)
            Else
                GetVehicleRateUnLoading = 0
                Exit Function
            End If
        End If

        If mFieldType = "T" Then
            mField1 = "TRIP_RATE"
            mField2 = IIf(mFreightType = "R", "DEFAULT_TRIP_RATE", "PREMIUM_RATE")
        ElseIf mFieldType = "P" Then
            '        mPoint = GetVehiclePoint(mCustName)
            '        If mPoint = 0 Then
            '            GetVehicleRate = 0
            '            Exit Function
            '        End If
            '        mField1 = "POINT_RATE"
            '        mField2 = "DEFAULT_POINT_RATE"
        End If

        SqlStr = "SELECT " & mField1 & " AS TRIP_RATE FROM FIN_LOCATION_RATE_DET" & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_LOCATION = '" & MainClass.AllowSingleQuote(mCustomerCode) & "'" & vbCrLf & " AND TRANSPORT_NAME = '" & MainClass.AllowSingleQuote((txtTransporterName.Text)) & "'" & vbCrLf & " AND VT_NAME = '" & MainClass.AllowSingleQuote((txtVehicleType.Text)) & "'" & vbCrLf & " AND WEF = (" & vbCrLf & " SELECT MAX(WEF) FROM FIN_LOCATION_RATE_DET" & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_LOCATION = '" & MainClass.AllowSingleQuote(mCustomerCode) & "'" & vbCrLf & " AND TRANSPORT_NAME = '" & MainClass.AllowSingleQuote((txtTransporterName.Text)) & "'" & vbCrLf & " AND VT_NAME = '" & MainClass.AllowSingleQuote((txtVehicleType.Text)) & "'" & vbCrLf & " AND WEF <= TO_DATE('" & VB6.Format(txtSlipDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetVehicleRateUnLoading = IIf(IsDbNull(RsTemp.Fields("TRIP_RATE").Value), 0, RsTemp.Fields("TRIP_RATE").Value)
        Else
            SqlStr = "SELECT " & mField2 & " AS TRIP_RATE FROM FIN_LOCATION_RATE_HDR" & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_LOCATION = '" & MainClass.AllowSingleQuote(mCustomerCode) & "'" & vbCrLf & " AND WEF = (" & vbCrLf & " SELECT MAX(WEF) FROM FIN_LOCATION_RATE_HDR" & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_LOCATION = '" & MainClass.AllowSingleQuote(mCustomerCode) & "'" & vbCrLf & " AND WEF <= TO_DATE('" & VB6.Format(txtSlipDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                GetVehicleRateUnLoading = IIf(IsDbNull(RsTemp.Fields("TRIP_RATE").Value), 0, RsTemp.Fields("TRIP_RATE").Value)
            End If
        End If

        If mFieldType = "P" Then
            GetVehicleRateUnLoading = GetVehicleRateUnLoading * mPoint
        End If
        Exit Function
ERR1:
        ErrorMsg(Err.Description, err.Number, MsgBoxStyle.Critical)
    End Function
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
    Private Sub txtSlipDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSlipDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtTransporterName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTransporterName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtTransporterName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTransporterName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtTransporterName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSlipNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSlipNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtSlipNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSlipNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Public Sub txtSlipNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSlipNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMkey As String = ""
        Dim mMRRNo As String
        Dim SqlStr As String = ""

        If Trim(txtSlipNo.Text) = "" Then GoTo EventExitSub

        If Len(txtSlipNo.Text) < 6 Then
            txtSlipNo.Text = VB6.Format(Val(txtSlipNo.Text), "00000") & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        If MODIFYMode = True And RsLoadMain.EOF = False Then xMkey = RsLoadMain.Fields("mKey").Value
        mMRRNo = Trim(txtSlipNo.Text)

        SqlStr = " SELECT * FROM DSP_TRIP_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_TRIP,LENGTH(AUTO_KEY_TRIP)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_TRIP=" & Val(mMRRNo) & " AND BOOKTYPE='" & lblBookType.Text & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLoadMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsLoadMain.EOF = False Then
            Clear1()
            Show1()
            '        TxtCustomerName.Enabled = True
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Loading Note, Use Generate Despatch Note Option To add", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM DSP_TRIP_HDR " & " WHERE AUTO_KEY_TRIP=" & Val(xMkey) & " AND BOOKTYPE='" & lblBookType.Text & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLoadMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub frmLoadingTripSlip_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        SqlStr = ""
        MainClass.ClearGrid(SprdView)

        SqlStr = " SELECT DISTINCT " & vbCrLf & " AUTO_KEY_TRIP, TRIP_DATE, VEHICLE_NO, TRANSPORTER_TRIP_NO, TRANSPORTER_TRIP_DATE, TRANSPORTER_NAME, REMARKS, DECODE(STATUS,'O','OPEN','CLOSED') AS STATUS,ADDUSER, ADDDATE " & vbCrLf & " FROM DSP_TRIP_HDR A " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUBSTR(AUTO_KEY_TRIP,LENGTH(AUTO_KEY_TRIP)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND BOOKTYPE='" & lblBookType.Text & "' ORDER BY AUTO_KEY_TRIP,TRIP_DATE"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        Call FormatSprdView()
    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 20)
            .set_ColWidth(0, 5)
            .set_ColWidth(1, 14)
            .ColHidden = False

            .set_ColWidth(2, 14)
            .ColHidden = False

            .set_ColWidth(3, 10)
            .set_ColWidth(4, 12)
            .set_ColWidth(5, 12)
            .set_ColWidth(6, 30)


            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub

    Private Sub txtTripDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTripDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTripNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTripNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTripNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTripNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtVehicleNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVehicleNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SearchVehicleMaster()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtVehicleNo.Text), "FIN_VEHICLE_MST", "NAME", "TRANSPORTER_NAME", "VEHICLE_TYPE", , SqlStr) = True Then
            txtVehicleNo.Text = AcName
            txtVehicleNo_Validating(txtVehicleNo, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Sub txtVehicleNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVehicleNo.DoubleClick
        SearchVehicleMaster()
    End Sub
    Private Sub txtVehicleNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVehicleNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtVehicleNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtVehicleNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtVehicleNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchVehicleMaster()
    End Sub
    Private Sub txtVehicleNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtVehicleNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtVehicleNo.Text) = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable((txtVehicleNo.Text), "NAME", "TRANSPORTER_NAME", "FIN_VEHICLE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgInformation("Invalid Vehicle No")
            Cancel = True
        Else
            txtTransporterName.Text = MasterNo
        End If

        If MainClass.ValidateWithMasterTable((txtVehicleNo.Text), "NAME", "VEHICLE_TYPE", "FIN_VEHICLE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgInformation("Invalid Vehicle No")
            Cancel = True
        Else
            txtVehicleType.Text = MasterNo
        End If

        '
        '    If Left(cboMode.Text, 1) = 4 Then
        '        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        '
        '        If MainClass.ValidateWithMasterTable(txtVehicle.Text, "NAME", "TRANSPORTER_NAME", "FIN_VEHICLE_MST", PubDBCn, MasterNo, , SqlStr) = True Then
        '            TxtTransporter.Text = MasterNo
        '        End If
        '    End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtVehicleType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVehicleType.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtVehicleType_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVehicleType.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtVehicleType.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
End Class
