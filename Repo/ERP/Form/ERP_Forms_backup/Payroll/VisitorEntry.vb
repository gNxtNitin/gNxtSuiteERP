Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmVisitorEntry
    Inherits System.Windows.Forms.Form
    Dim RsVisitorMain As ADODB.Recordset ''ADODB.Recordset	
    'Private PvtDBCn As ADODB.Connection	

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean
    Dim mAccountCode As String

    Dim AccessCnn As New ADODB.Connection
    Private Const ConRowHeight As Short = 14

    Private Const ColDescription As Short = 1
    Private Const ColAvailable As Short = 2
    Private Const ColRemarks As Short = 3

    Private m_TimeToCapture_milliseconds As Short
    Private m_Width As Integer
    Private m_Height As Integer

    Private Sub cboCardType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCardType.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboPurpose_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboPurpose.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboPurpose_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboPurpose.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub chkOut_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkOut.CheckStateChanged
        If chkOut.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtOutTime.Text = GetServerDate
            TxtOutTm.Text = GetServerTime
        End If
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            SprdMain.Enabled = True
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

        If txtOutTime.Enabled = False Then
            MsgInformation("Slip Closed, Cann't be Deleted")
            Exit Sub
        End If

        If Not RsVisitorMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.	
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "PAY_VISITOR_HDR", (txtVNo.Text), RsVisitorMain) = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "PAY_VISITOR_HDR", "MKEY", (lblMkey.Text)) = False Then GoTo DelErrPart

                PubDBCn.Execute("DELETE FROM PAY_VISITORPHOTO_HDR WHERE MKEY=" & Val(lblMkey.Text) & "")
                PubDBCn.Execute("DELETE FROM PAY_VISITOR_HDR WHERE MKEY=" & Val(lblMkey.Text) & "")


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

    Private Sub cmdeMailResend_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdeMailResend.Click
        On Error GoTo ErrPart
        If Val(txtVNo.Text) = 0 Then Exit Sub

        If SendMail() = False Then Exit Sub
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number))
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr

        If PubUserID <> "G0416" Then
            If txtOutTime.Enabled = False Then
                MsgInformation("Slip Closed, Cann't be Modified")
                Exit Sub
            End If
        End If

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsVisitorMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
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

    Private Sub cmdSearchMobile_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchMobile.Click

        On Error GoTo ShowErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim xMkey As String = ""
        Dim mMobileNo As String

        ''            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _	
        '	
        If Trim(txtMobileNo.Text) = "" Then Exit Sub
        mMobileNo = Trim(txtMobileNo.Text)

        SqlStr = "SELECT * FROM PAY_VISITOR_HDR " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MOBILE_DETAILS='" & Val(txtMobileNo.Text) & "'" & vbCrLf & " ORDER BY REF_DATE DESC"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)



        Clear1()
        If Not RsTemp.EOF Then
            With RsTemp

                xMKey = IIf(IsDbNull(.Fields("mKey").Value), "", .Fields("mKey").Value)
                txtVisitorName.Text = IIf(IsDbNull(.Fields("VISITOR_NAME").Value), "", .Fields("VISITOR_NAME").Value)
                txtCompanyName.Text = IIf(IsDbNull(.Fields("VISITOR_COMPANYNAME").Value), "", .Fields("VISITOR_COMPANYNAME").Value)
                TxtWhomToMeet.Text = IIf(IsDbNull(.Fields("WHOM_TO_MEET").Value), "", .Fields("WHOM_TO_MEET").Value)
                txtEmailID.Text = IIf(IsDbNull(.Fields("EMAIL_ID").Value), "", .Fields("EMAIL_ID").Value)

                If .Fields("PURPOSE").Value = "1" Then
                    cboPurpose.SelectedIndex = 0
                ElseIf .Fields("PURPOSE").Value = "2" Then
                    cboPurpose.SelectedIndex = 1
                End If

                If Val(.Fields("CARD_TYPE").Value) <= 1 Then
                    cboCardType.SelectedIndex = 0
                Else
                    cboCardType.SelectedIndex = Val(.Fields("CARD_TYPE").Value) - 1
                End If

                With SprdMain
                    .Row = 1
                    .Col = ColAvailable
                    .Value = IIf(IsDbNull(RsTemp.Fields("MOBILE_AVAILABLE").Value) Or RsTemp.Fields("MOBILE_AVAILABLE").Value = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)

                    .Col = ColRemarks
                    .Text = IIf(IsDbNull(RsTemp.Fields("MOBILE_DETAILS").Value), "", RsTemp.Fields("MOBILE_DETAILS").Value)


                    .Row = 2
                    .Col = ColAvailable
                    .Value = IIf(IsDbNull(RsTemp.Fields("VEHICLE_AVAILABLE").Value) Or RsTemp.Fields("VEHICLE_AVAILABLE").Value = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
                    .Col = ColRemarks
                    .Text = IIf(IsDbNull(RsTemp.Fields("VEHICLE_DETAILS").Value), "", RsTemp.Fields("VEHICLE_DETAILS").Value)


                    .Row = 3
                    .Col = ColAvailable
                    .Value = IIf(IsDbNull(RsTemp.Fields("LAPTOP_AVAILABLE").Value) Or RsTemp.Fields("LAPTOP_AVAILABLE").Value = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
                    .Col = ColRemarks
                    .Text = IIf(IsDbNull(RsTemp.Fields("LAPTOP_DETAILS").Value), "", RsTemp.Fields("LAPTOP_DETAILS").Value)

                    .Row = 4
                    .Col = ColAvailable
                    .Value = IIf(IsDbNull(RsTemp.Fields("OTHERS_AVAILABLE").Value) Or RsTemp.Fields("OTHERS_AVAILABLE").Value = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
                    .Col = ColRemarks
                    .Text = IIf(IsDbNull(RsTemp.Fields("OTHERS_DETAILS").Value), "", RsTemp.Fields("OTHERS_DETAILS").Value)
                End With

            End With

            If ShowPhoto(xMKey) = False Then GoTo ShowErrPart

        Else
            With SprdMain
                .Row = 1
                .Col = ColAvailable
                .Value = CStr(System.Windows.Forms.CheckState.Checked)

                .Col = ColRemarks
                .Text = mMobileNo
            End With
        End If

        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume	
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

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\VisitorSlip.RPT"

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
        Dim SqlStrSub As String


        'Dim bmp As StdPicture	
        '	
        'Dim pic As craxdrt.OLEObject	

        '    pic.formattedpicture.   - to display properties and methods	

        '    Set bmp = LoadPicture(lblFilePath.Caption)	
        ''    Report1.Sections("Section1").ReportObjects("picture1") = LoadPicture(lblFilePath.Caption)	
        '	
        ''    set Report1.SelectionFormula("Picture1").	
        '    Set pic.FormattedPicture = bmp	

        '    If AccessCnn.State <> adStateOpen Then	
        '        AccessCnn.Open "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & App.path & "\ERPImage.mdb;Persist Security Info=False"	
        '        AccessCnn.Open "Database DLL Name=pdsoledb.dll;Server Type=OLE DB;Server=D:\HemaAccount\ERPImage.mdb;User Name=Admin"	
        '    End If	

        '    AccessRptConn = "DSN=" & DBConImageDSN & ""  '';UID=Admin;PWD=;DSQ=;	

        Report1.SQLQuery = mSqlStr

        SqlStrSub = "SELECT * FROM VISITORIMAGE WHERE MKEY='" & lblMkey.Text & "'"
        Report1.SubreportToChange = Report1.GetNthSubreportName(0)
        'Report1.Connect = AccessRptConn  sandeep
        Report1.SQLQuery = SqlStrSub
        Report1.SubreportToChange = ""

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

        MakeSQL = " SELECT *  FROM " & vbCrLf & " PAY_VISITOR_HDR IH"

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
        Dim mVNO As Double
        Dim mClosedFlag As String
        Dim mPurpose As String
        Dim mCardType As String

        Dim cntRow As Integer
        Dim mMobileAvailable As String
        Dim mVehicleAvailable As String
        Dim mLaptopAvailable As String
        Dim mOthersAvailable As String

        Dim mMobileDetails As String
        Dim mVehicleDetails As String
        Dim mLaptopDetails As String
        Dim mOthersDetails As String
        Dim mOutTime As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM PAY_VISITORPHOTO_HDR WHERE MKEY = '" & lblMkey.Text & "'"
        PubDBCn.Execute(SqlStr)

        mPurpose = VB.Left(cboPurpose.Text, 1)
        mCardType = VB.Left(cboCardType.Text, 1)

        SqlStr = ""
        If Trim(txtVNo.Text) = "" Then
            mVNO = CDbl(AutoGenSeqRefNo("REF_NO"))
        Else
            mVNO = Val(txtVNo.Text)
        End If

        txtVNo.Text = VB6.Format(Val(CStr(mVNO)), "00000")

        If (txtOutTime.Text = "" Or txtOutTime.Text = "__/__/____" Or IsDate(txtOutTime.Text) = False Or TxtOutTm.Text = "" Or TxtOutTm.Text = "__:__") Then
            mOutTime = ""
        Else
            mOutTime = VB6.Format(txtOutTime.Text, "DD-MMM-YYYY") & " " & VB6.Format(TxtOutTm.Text, "HH:MM")
        End If
        With SprdMain
            .Row = 1
            .Col = ColAvailable
            mMobileAvailable = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")
            .Col = ColRemarks
            mMobileDetails = Trim(.Text)

            .Row = 2
            .Col = ColAvailable
            mVehicleAvailable = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")
            .Col = ColRemarks
            mVehicleDetails = Trim(.Text)


            .Row = 3
            .Col = ColAvailable
            mLaptopAvailable = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")
            .Col = ColRemarks
            mLaptopDetails = Trim(.Text)

            .Row = 4
            .Col = ColAvailable
            mOthersAvailable = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")
            .Col = ColRemarks
            mOthersDetails = Trim(.Text)
        End With

        If ADDMode = True Then
            mCurRowNo = MainClass.AutoGenRowNo("PAY_VISITOR", "RowNo", PubDBCn)
            nMkey = RsCompany.Fields("COMPANY_CODE").Value & RsCompany.Fields("FYEAR").Value & mCurRowNo

            lblMkey.Text = nMkey

            SqlStr = " INSERT INTO PAY_VISITOR_HDR ( " & vbCrLf & " MKEY, COMPANY_CODE , FYEAR, ROWNO," & vbCrLf & " REF_NO, REF_DATE," & vbCrLf & " VISITOR_NAME, VISITOR_COMPANYNAME," & vbCrLf & " WHOM_TO_MEET, EMAIL_ID, PURPOSE, " & vbCrLf & " CARD_NO, CARD_TYPE, OUT_TIME," & vbCrLf & " MOBILE_AVAILABLE, MOBILE_DETAILS," & vbCrLf & " VEHICLE_AVAILABLE, VEHICLE_DETAILS," & vbCrLf & " LAPTOP_AVAILABLE, LAPTOP_DETAILS," & vbCrLf & " OTHERS_AVAILABLE, OTHERS_DETAILS," & vbCrLf & " ADDUSER, ADDDATE," & vbCrLf & " MODUSER,MODDATE ) "

            SqlStr = SqlStr & vbCrLf & " VALUES ( " & vbCrLf & " '" & nMkey & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & "," & vbCrLf & " " & mCurRowNo & ", " & Val(txtVNo.Text) & ", " & vbCrLf & " TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI'), " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtVisitorName.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtCompanyName.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(TxtWhomToMeet.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtEmailID.Text) & "', " & vbCrLf & " '" & mPurpose & "', " & vbCrLf & " " & Val(VB6.Format(txtCardNo.Text, "0")) & ", '" & mCardType & "'," & vbCrLf & " TO_DATE('" & mOutTime & "','DD-MON-YYYY HH24:MI'), " & vbCrLf & " '" & mMobileAvailable & "', '" & MainClass.AllowSingleQuote(mMobileDetails) & "'," & vbCrLf & " '" & mVehicleAvailable & "', '" & MainClass.AllowSingleQuote(mVehicleDetails) & "'," & vbCrLf & " '" & mLaptopAvailable & "', '" & MainClass.AllowSingleQuote(mLaptopDetails) & "'," & vbCrLf & " '" & mOthersAvailable & "', '" & MainClass.AllowSingleQuote(mOthersDetails) & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"
        End If

        If MODIFYMode = True Then
            SqlStr = " UPDATE PAY_VISITOR_HDR SET " & vbCrLf & " REF_NO=" & Val(txtVNo.Text) & ", " & vbCrLf & " REF_DATE=TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY HH:MM") & "','DD-MON-YYYY HH24:MI'), " & vbCrLf & " VISITOR_NAME='" & MainClass.AllowSingleQuote(txtVisitorName.Text) & "', " & vbCrLf & " VISITOR_COMPANYNAME='" & MainClass.AllowSingleQuote(txtCompanyName.Text) & "', " & vbCrLf & " WHOM_TO_MEET='" & MainClass.AllowSingleQuote(TxtWhomToMeet.Text) & "', " & vbCrLf & " EMAIL_ID='" & MainClass.AllowSingleQuote(txtEmailID.Text) & "', " & vbCrLf & " PURPOSE='" & mPurpose & "'," & vbCrLf & " CARD_NO=" & Val(VB6.Format(txtCardNo.Text, "0")) & ", " & vbCrLf & " CARD_TYPE='" & mCardType & "'," & vbCrLf & " OUT_TIME=TO_DATE('" & mOutTime & "','DD-MON-YYYY HH24:MI'), " & vbCrLf & " MOBILE_AVAILABLE='" & mMobileAvailable & "', MOBILE_DETAILS='" & MainClass.AllowSingleQuote(mMobileDetails) & "'," & vbCrLf & " VEHICLE_AVAILABLE='" & mVehicleAvailable & "', VEHICLE_DETAILS='" & MainClass.AllowSingleQuote(mVehicleDetails) & "'," & vbCrLf & " LAPTOP_AVAILABLE='" & mLaptopAvailable & "', LAPTOP_DETAILS='" & MainClass.AllowSingleQuote(mLaptopDetails) & "'," & vbCrLf & " OTHERS_AVAILABLE='" & mOthersAvailable & "', OTHERS_DETAILS='" & MainClass.AllowSingleQuote(mOthersDetails) & "'," & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND MKEY =" & Val(lblMkey.Text) & ""
        End If

        PubDBCn.Execute(SqlStr)
        txtVNo.Text = VB6.Format(Val(CStr(mVNO)), "00000")

        '    lblFilePath.Caption = "C:\WINDOWS\myPic\20130807_1110.bmp"	

        If Trim(lblFilePath.Text) <> "" Then
            If UpdatePhoto(Val(lblMkey.Text)) = False Then GoTo ErrPart
            If UpdatePhotoAccess(Val(lblMkey.Text)) = False Then GoTo ErrPart
        End If

        Update1 = True
        PubDBCn.CommitTrans()

        If ADDMode = True Then
            If Trim(txtEmailID.Text) <> "" Then
                If SendMail() = False Then Exit Function
            End If
        End If

        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsVisitorMain.Requery()
        MsgBox(Err.Description)
        '    Resume	
    End Function
    Private Function SendMail() As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        Dim mTo As String
        Dim mCC As String
        Dim mFrom As String
        Dim mAttachmentFile As String
        Dim mDateTime As String
        Dim pAccountCode As String
        Dim mSubject As String
        Dim mBodyText As String
        Dim mSecurityeMailID As String
        Dim mPublishPath As String
        Dim mBodyTextDetail As String
        Dim mContactNo As String
        Dim mPurpose As String

        Dim mBodyHeader As String
        Dim mBodyFooter As String
        Dim mPicName As String

        SendMail = False
        mPicName = "show.jpg"

        strServerPop3 = GetEMailID("POP_ID")
        If RsCompany.Fields("COMPANY_CODE").Value = 35 Then
            strServerSmtp = "mail.hemaengineering.com" '' "103.25.128.53" ' GetEMailID("SMTP_ID")   '"103.25.128.53"  '	
            strAccount = GetEMailID("MAIL_ACCOUNT")
            strPassword = "HEma12345!@#" ''GetEMailID("PASSWORD")	

        Else
            strServerSmtp = GetEMailID("SMTP_ID")
            strAccount = GetEMailID("MAIL_ACCOUNT")
            strPassword = GetEMailID("PASSWORD")
        End If

        SprdMain.Row = 1
        SprdMain.Col = 3
        mContactNo = Trim(SprdMain.Text)

        mPurpose = IIf(Len(cboPurpose.Text) > 3, Mid(Trim(cboPurpose.Text), 3), "")

        mSecurityeMailID = GetEMailID("SECURITY_MAIL")

        mTo = Trim(txtEmailID.Text)
        '    mCC = ReadInI("InternetInfo", "CC", "InternetInfo.INI")	
        mFrom = Trim(mSecurityeMailID) ''ReadInI("InternetInfo", "FROM", "InternetInfo.INI")	

        mAttachmentFile = Trim(lblFilePath.Text)

        mSubject = "Visitor Entry Slip"

        mBodyTextDetail = "<table align=center border=1 cellPadding=2 cellSpacing=0>" & "<tr>" & "<td align=center width=50><b>Ref No</b></td>" & "<td align=center width=100><b>In Time</b></td>" & "<td align=center width=100><b>Visitor Card No</b></td>" & "<td align=center width=1000><b>Visitor Name</b></td>" & "<td align=center width=1000><b>Contact No</b></td>" & "<td align=center width=1000><b>Visitor's Company Name & Address</b></td>" & "<td align=center width=1000><b>whom to Meet</b></td>" & "<td align=center width=500><b>Purpose</b></td>" & "<td align=center width=1000><b>Photo</b></td>"

        mBodyTextDetail = mBodyTextDetail & "<tr>" & "<td align=Left>" & Trim(txtVNo.Text) & "</td>" & "<td align=Left>" & VB6.Format(txtVDate.Text, "DD/MM/YYYY hh:mm") & "</td>" & "<td align=Left>" & Trim(CStr(txtCardNo.TabIndex)) & "</td>" & "<td align=Left>" & Trim(txtVisitorName.Text) & "</td>" & "<td align=Left>" & Trim(mContactNo) & "</td>" & "<td align=Left>" & Trim(txtCompanyName.Text) & "</td>" & "<td align=Left>" & Trim(TxtWhomToMeet.Text) & "</td>" & "<td align=Left>" & Trim(mPurpose) & "</td>"



        If Trim(lblFilePath.Text) = "" Then
            mBodyTextDetail = mBodyTextDetail & "<td align=Left> </td>"
        Else
            mBodyTextDetail = mBodyTextDetail & "<IMG src= " & mPicName & "></P>"
            '        mBodyTextDetail = mBodyTextDetail & "<td align=center><img SRC= 'cid:" & Trim(lblFilePath.Caption) & "'></img></td>"	
        End If

        mBodyTextDetail = mBodyTextDetail & "</tr>"

        mBodyTextDetail = mBodyTextDetail & "</table>"

        ''Visitor Entry Slip	

        mBodyText = "<html><body><b><font size=11, color=Blue></font></b><br />" & "Dear Sir,<br />" & "<br />" & "<br />" & mBodyTextDetail & "<br />" & "<br />" & "Thanking You,<br />" & "<br />" & "<br />" & "Your Faithfully<br />" & "for " & RsCompany.Fields("Company_Name").Value & "<br />" & "</body></html>"

        If strServerPop3 = "" And strServerSmtp = "" And strAccount = "" And strPassword = "" Then
            MsgBox("Please Check Email Configuration", MsgBoxStyle.Information)
            SendMail = False
            Exit Function
        End If

        If Trim(mFrom) = "" Or Trim(mTo) = "" Or Trim(strAccount) = "" Then

        Else
            '        Call SendMailProcess(mFrom, mTo, mCC, "", strAccount, strPassword, mAttachmentFile, mSubject, mBodyText)	
            Call SendMailProcessNew(mFrom, mTo, mCC, "", strAccount, strPassword, "", mAttachmentFile, mPicName, mSubject, "", mBodyText, "") ''	
        End If

        SendMail = True

        Exit Function
ErrPart:
        '    Resume	
        SendMail = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function ShowPhoto(ByRef pMKey As String) As Boolean
        On Error GoTo ErrPart
        'Dim CN As ADODB.Connection
        'Dim RS As ADODB.Recordset = Nothing
        'Dim Sql As String
        'Dim sFileName As String
        'Dim mFileSize As Integer

        'lblFilePath.Text = ""
        'If Dir(mLocalPath & "\myPic", FileAttribute.Directory) = "" Then MkDir((mLocalPath & "\myPic"))
        ''    sFileName = mLocalPath & "\myPic\" & vb6.Format(PubCurrDate, "YYYYMMDD") & "_" & vb6.Format(GetServerTime(), "HHMM") & ".jpg"	

        'sFileName = mLocalPath & "\myPic\show.jpg"

        'CN = New ADODB.Connection
        'RS = New ADODB.Recordset


        'CN.Provider = "OraOLEDB.Oracle"
        'CN.Open(DBConSERVICENAME, DBConUID, DBConPWD)

        'Sql = "SELECT * FROM PAY_VISITORPHOTO_HDR WHERE MKEY='" & pMKey & "'"
        'RS.Open(Sql, CN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        ''	
        '' Save using GetChunk and known size.	
        '' FieldSize (ActualSize) > Threshold arg (16384)	
        'If RS.EOF = False Then
        '    If IsDbNull(RS.Fields("VISITOR_PHOTO").Value) Then
        '        ImagePhoto.Image = Nothing 'CDLPhoto.FileName	
        '        ImagePhoto.Image = Nothing
        '    Else
        '        lblFilePath.Text = sFileName
        '        '            mFileSize = RS!VISITOR_PHOTO.ActualSize	
        '        BlobToFile(RS.Fields("VISITOR_PHOTO"), lblFilePath.Text, (RS.Fields("VISITOR_PHOTO").ActualSize), 16384) ''ok	
        '        ImagePhoto.Image = System.Drawing.Image.FromFile(lblFilePath.Text)

        '        '            ImagePhoto.Stretch = True	
        '    End If
        'End If


        'RS.Close()
        'CN.Close()
        ShowPhoto = True
        Exit Function
ErrPart:
        ShowPhoto = False
    End Function
    Private Function ShowPhotoAccess(ByRef pMKey As String) As Boolean

        On Error GoTo ErrPart
        Dim CN As ADODB.Connection
        Dim RS As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim sFileName As String
        Dim mFileSize As Integer

        lblFilePath.Text = ""
        If Dir(mLocalPath & "\myPic", FileAttribute.Directory) = "" Then MkDir((mLocalPath & "\myPic"))
        '    sFileName = mLocalPath & "\myPic\" & vb6.Format(PubCurrDate, "YYYYMMDD") & "_" & vb6.Format(GetServerTime(), "HHMM") & ".jpg"	

        sFileName = mLocalPath & "\myPic\show.jpg"
        lblFilePath.Text = sFileName

        If AccessCnn.State <> ADODB.ObjectStateEnum.adStateOpen Then
            AccessCnn.Open("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & My.Application.Info.DirectoryPath & "\ERPImage.mdb;Persist Security Info=False")
        End If
        SqlStr = "Select * From VISITORImage Where MKEY='" & pMKey & "'"
        MainClass.UOpenRecordSet(SqlStr, AccessCnn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockOptimistic)
        If RS.RecordCount > 0 Then
            Call FillPhoto(RS, "VISITOR_PHOTO", "ItemPicSize", ImagePhoto)
            '  Picture1.PaintPicture Rs.Fields(3).Value, 0, 100, 0, 100	
            '        SavePicture ImagePhoto.Image, lblFilePath.Caption     ''App.path & "\Picture\MIPLITEM.BMP"	
            ImagePhoto.Image.Save(lblFilePath.Text)
        Else
            If ShowPhoto((lblMkey.Text)) = False Then
                ImagePhoto.Image = System.Drawing.Image.FromFile("")
            End If
        End If

        ShowPhotoAccess = True
        Exit Function
ErrPart:
        ShowPhotoAccess = False
    End Function
    Public Sub FillPhoto(ByRef rstMain As ADODB.Recordset, ByRef PFName As String, ByRef SizeField As String, ByRef picEmp As System.Windows.Forms.PictureBox) ''PictureBox	
        On Error GoTo Handler
        Dim bytes() As Byte
        Dim file_name As String
        Dim file_num As Short
        Dim file_length As Integer
        Dim num_blocks As Integer
        Dim left_over As Integer
        Dim block_num As Integer
        Dim hgt As Single

        '	
        'me.imgPhoto.Visible = False	
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Application.DoEvents()

        ' Get a temporary file name.	
        file_name = lblFilePath.Text ''TemporaryFileName()	

        ' Open the file.	
        file_num = FreeFile
        FileOpen(file_num, file_name, OpenMode.Binary)

        ' Copy the data into the file.	
        file_length = rstMain.Fields(SizeField).Value
        num_blocks = file_length / BLOCK_SIZE
        left_over = file_length Mod BLOCK_SIZE

        For block_num = 1 To num_blocks
            bytes = rstMain.Fields(PFName).GetChunk(BLOCK_SIZE)
            FilePut(file_num, bytes)
        Next block_num

        If left_over > 0 Then
            bytes = rstMain.Fields(PFName).GetChunk(left_over)
            FilePut(file_num, bytes)
        End If

        FileClose(file_num)

        picEmp.Image = System.Drawing.Image.FromFile(file_name)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub

Handler:
        Debug.Print(Err.Description)
        Resume Next
    End Sub

    Private Function UpdatePhoto(ByRef nMkey As Double) As Boolean
        '        On Error GoTo ErrPart
        '        Dim CN As ADODB.Connection
        '        Dim RS As ADODB.Recordset = Nothing
        '        Dim Sql As String

        '        CN = New ADODB.Connection
        '        RS = New ADODB.Recordset


        '        CN.Provider = "OraOLEDB.Oracle"
        '        CN.Open(DBConSERVICENAME, DBConUID, DBConPWD)

        '        Sql = "SELECT * FROM PAY_VISITORPHOTO_HDR WHERE MKEY=" & Val(CStr(nMkey)) & ""
        '        RS.Open(Sql, CN, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)
        '        '	
        '        ' Load using AppendChunk	
        '        '	
        '        RS.AddNew()
        '        RS.Fields("mKey").Value = nMkey
        '        RS.Fields("COMPANY_CODE").Value = RsCompany.Fields("COMPANY_CODE").Value
        '        RS.Fields("BFILE_TYPE").Value = "JPG"
        '        FileToBlob(Trim(lblFilePath.Text), RS.Fields("VISITOR_PHOTO"), 16384)
        '        RS.Update()

        '        RS.Close()
        '        CN.Close()
        '        UpdatePhoto = True
        '        Exit Function
        'ErrPart:
        '        UpdatePhoto = False
        '        PubDBCn.RollbackTrans()
        '        RsVisitorMain.Requery()
        '        MsgBox(Err.Description)
        '        ''Resume	
    End Function
    Private Function UpdatePhotoAccess(ByRef nMkey As Double) As Boolean

        '        On Error GoTo ErrPart
        '        Dim SqlStr As String=""=""
        '        Dim RS As New ADODB.Recordset
        '        Dim mInventoryGroupCode As Integer
        '        Dim mstream As ADODB.Stream

        '        If CDbl(CObj(ImagePhoto.Image)) = 0 Then UpdatePhotoAccess = True : Exit Function

        '        If AccessCnn.State <> ADODB.ObjectStateEnum.adStateOpen Then
        '            AccessCnn.Open("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & My.Application.Info.DirectoryPath & "\ERPIMAGE.mdb;Persist Security Info=False")
        '        End If
        '        AccessCnn.BeginTrans()

        '        SqlStr = "Select * From VISITORImage " 'WHERE ITEMCODE='" & pcls6.AllowSingleQuote(txtItemCode.Text) & "'"	
        '        MainClass.UOpenRecordSet(SqlStr, AccessCnn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockOptimistic)
        '        RS.Find("MKEY='" & nMkey & "'")
        '        Dim ss As PropertyBag
        '        If RS.EOF Then
        '            RS.AddNew()
        '            RS.Fields("mKey").Value = nMkey
        '            RS.Fields("COMPANY_CODE").Value = RsCompany.Fields("COMPANY_CODE").Value
        '            RS.Fields("BFILE_TYPE").Value = "JPG"

        '            '                GetPhoto IIf(CDlg1.FileName = "", "Photo", App.path & "\Picture\MIPLITEM.BMP"), Rs, "ItemPicture", "ItemPicSize"	
        '            GetPhoto(IIf(lblFilePath.Text = "", "Photo", lblFilePath.Text), RS, "VISITOR_PHOTO", "ItemPicSize")
        '            RS.Update()
        '        Else
        '            'GetPhoto IIf(CDlg1.FileName = "", "Photo", App.Path & "\Picture\MIPLITEM.BMP"), Rs, "ItemPicture", "ItemPicSize"	
        '            'SaveImageToDB Me.Picture1.Picture, Rs, "pic"	

        '            'Set ss = New PropertyBag	
        '            'ss.WriteProperty "MyImage", pPic	
        '            'Rs.Fields("ItemPicture").AppendChunk ss.Contents	
        '            ''Rs.Update	
        '            'Set ss = Nothing	


        '            mstream = New ADODB.Stream
        '            mstream.Type = ADODB.StreamTypeEnum.adTypeBinary
        '            mstream.Open()

        '            mstream.LoadFromFile(lblFilePath.Text) ''App.path & "\Picture\MIPLITEM.BMP"	
        '            RS.Fields("VISITOR_PHOTO").Value = mstream.Read


        '            RS.Update()
        '        End If
        '        '       AccessCnn.Execute SqlStr	
        '        AccessCnn.CommitTrans()
        '        UpdatePhotoAccess = True
        '        Exit Function
        'ErrPart:
        '        'Resume	
        '        UpdatePhotoAccess = False
        '        MsgInformation(Err.Description)
    End Function

    Public Sub GetPhoto(ByRef FileName As String, ByRef rstMain As ADODB.Recordset, ByRef FieldName As String, ByRef SizeField As String)
        On Error GoTo Handler
        Dim file_num As String
        Dim file_length As Integer
        Dim bytes() As Byte
        Dim num_blocks As Integer
        Dim left_over As Integer
        Dim block_num As Integer


        file_num = CStr(FreeFile)
        FileOpen(CInt(file_num), FileName, OpenMode.Binary, OpenAccess.Read)

        file_length = LOF(CInt(file_num))
        If file_length > 0 Then
            num_blocks = file_length / BLOCK_SIZE
            left_over = file_length Mod BLOCK_SIZE

            rstMain.Fields(SizeField).Value = file_length

            ReDim bytes(BLOCK_SIZE)
            For block_num = 1 To num_blocks
                FileGet(CShort(file_num), bytes)
                rstMain.Fields(FieldName).AppendChunk(bytes)
            Next block_num

            If left_over > 0 Then
                ReDim bytes(left_over)
                FileGet(CShort(file_num), bytes)
                rstMain.Fields(FieldName).AppendChunk(bytes)
            End If

            'rstEmployee.Update	
            FileClose(CInt(file_num))
        End If
        Exit Sub

Handler:
        MsgBox(Err.Description)
        'Resume	
    End Sub


    Private Function CheckPendingCardNo() As Boolean

        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        SqlStr = ""
        ''SELECT CLAUSE...  CARD_TYPE	
        CheckPendingCardNo = False

        SqlStr = " SELECT REF_NO, REF_DATE  FROM " & vbCrLf & " PAY_VISITOR_HDR IH " & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CARD_NO=" & Val(txtCardNo.Text) & "" & vbCrLf & " AND (OUT_TIME IS NULL OR OUT_TIME='')"

        If RsCompany.Fields("COMPANY_CODE").Value = 3 And RsCompany.Fields("COMPANY_CODE").Value = 10 And RsCompany.Fields("COMPANY_CODE").Value = 17 Then
            SqlStr = SqlStr & vbCrLf & " AND CARD_TYPE='" & VB.Left(cboCardType.Text, 1) & "'"
        End If

        If MODIFYMode = True Then
            SqlStr = SqlStr & vbCrLf & " AND IH.MKEY<>'" & MainClass.AllowSingleQuote(lblMkey.Text) & "'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            CheckPendingCardNo = True
            MsgBox("Please Such Card No Slip is Pending. Ref. No is " & IIf(IsDbNull(RsTemp.Fields("REF_NO").Value), "", RsTemp.Fields("REF_NO").Value) & " & DATE : " & IIf(IsDbNull(RsTemp.Fields("REF_DATE").Value), "", RsTemp.Fields("REF_DATE").Value))
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

        SqlStr = "SELECT Max(" & mFieldName & ")  FROM PAY_VISITOR_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " "
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

    Private Sub cmdStart_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdStart.Click
        Dim sFileName As String
        Dim m_Width As Integer
        Dim m_Height As Integer

        m_Width = 120 '2385 / 2 ' 2325	
        m_Height = 160 ' 2535 / 2 ' 1785	

        If mIsCamStart = True Then
            Call SendCamMessage(hCap, WM_CAP_DRIVER_DISCONNECT, 0, 0)
            mIsCamStart = False
            hCap = 0
            Timer1.Enabled = False
            Timer1.Interval = 1
        End If


        Timer1.Interval = m_TimeToCapture_milliseconds

        ' for safety, call stop, just in case we are already running	
        Me.Timer1.Enabled = False

        '    hCap = capCreateCaptureWindow("Web Cam", WS_CHILD Or WS_VISIBLE, 0, 0, PicWebCam.Width, PicWebCam.Height, PicWebCam.hwnd, 0)	
        hCap = capCreateCaptureWindow("WebCap", 0, 0, 0, m_Width, m_Height, Me.Handle.ToInt32, 0)
        System.Windows.Forms.Application.DoEvents()

        ' connect to the capture device	
        Call SendCamMessage(hCap, WM_CAP_DRIVER_CONNECT, 0, 0)
        System.Windows.Forms.Application.DoEvents()

        Call SendCamMessage(hCap, WM_CAP_SET_PREVIEW, 0, 0)

        cmdVideoFormat.Enabled = True
        cmdCapture.Enabled = True
        cmdeMailResend.Enabled = False

        ' set the timer information	
        mIsCamStart = True
        Me.Timer1.Enabled = True

        '    If hCap <> 0 Then	
        '        Call SendCamMessage(hCap, WM_CAP_DRIVER_CONNECT, 0, 0)	
        '        Call SendCamMessage(hCap, WM_CAP_SET_PREVIEWRATE, 66, 0&)	
        '        Call SendCamMessage(hCap, WM_CAP_SET_PREVIEW, CLng(True), 0&)	
        ''            Call SendCamMessage(hCap, WM_CAP_SET_PREVIEW, 0, 0)	
        '        PicWebCam.Visible = True	
        '        ImagePhoto.Visible = False	
        '        mIsCamStart = True	
        '    End If	

    End Sub

    Function capDlgVideoFormat(ByVal hCapWnd As Integer) As Boolean
        capDlgVideoFormat = SendMessageAsLong(hCapWnd, WM_CAP_DLG_VIDEOFORMAT, 0, 0)
    End Function
    Private Sub cmdStop_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdStop.Click
        On Error GoTo ErrPart
        Call SendCamMessage(hCap, WM_CAP_DRIVER_DISCONNECT, 0, 0)
        mIsCamStart = False
        hCap = 0
        Me.Timer1.Enabled = False
        Me.Timer1.Interval = 1

        Exit Sub
ErrPart:
        Me.Timer1.Enabled = False
        Me.Timer1.Interval = 1
    End Sub

    Private Sub cmdVideoFormat_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdVideoFormat.Click
        On Error Resume Next

        If hCap = 0 Then Exit Sub
        If mIsCamStart = True Then
            Call SendCamMessage(hCap, WM_CAP_DLG_VIDEOFORMAT, 0, 0)
        End If
        System.Windows.Forms.Application.DoEvents()
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
        MainClass.ButtonStatus(Me, XRIGHT, RsVisitorMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Public Sub frmVisitorEntry_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Visitor Slip Entry"

        SqlStr = "Select * From PAY_VISITOR_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsVisitorMain, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)

        SetTextLengths()
        Clear1()
        'If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
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

        SqlStr = " SELECT " & vbCrLf & " REF_NO, TO_CHAR(REF_DATE,'DD/MM/YYYY HH24:MI') AS REF_DATE, " & vbCrLf & " CARD_NO, DECODE(PURPOSE,'1','OFFICIAL','PERSONAL') AS PURPOSE, VISITOR_NAME, VISITOR_COMPANYNAME, " & vbCrLf & " WHOM_TO_MEET,  " & vbCrLf & " TO_CHAR(OUT_TIME,'HH24:MI') OUT_TIME" & vbCrLf & " FROM PAY_VISITOR_HDR " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

        SqlStr = SqlStr & " ORDER BY REF_NO,REF_DATE"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmVisitorEntry_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        cboCardType.Items.Clear()
        If RsCompany.Fields("COMPANY_CODE").Value = 3 And RsCompany.Fields("COMPANY_CODE").Value = 10 And RsCompany.Fields("COMPANY_CODE").Value = 17 Then
            cboCardType.Items.Add("1. Customer")
            cboCardType.Items.Add("2. Supplier")
            cboCardType.Items.Add("3. Visitor")
            cboCardType.Items.Add("4. Others/Interview")
        ElseIf RsCompany.Fields("COMPANY_CODE").Value = 15 And RsCompany.Fields("COMPANY_CODE").Value = 25 Then
            cboCardType.Items.Add("1. Office Area")
            cboCardType.Items.Add("2. Manufacturing Area")
            cboCardType.Items.Add("3. Others/Interview")
        Else
            cboCardType.Items.Add("1. ")
        End If
        cboCardType.SelectedIndex = 0

        m_TimeToCapture_milliseconds = 100
        m_Width = 2385
        m_Height = 2265
        mIsCamStart = False

        '    PicWebCam.AutoSize = True	
        '    hCap = capCreateCaptureWindow("Take a Camera Shot", WS_CHILD Or WS_VISIBLE, 0, 0, PicWebCam.Width, PicWebCam.Height, PicWebCam.hWnd, 0)	
        '    If hCap <> 0 Then	
        '        Call SendCamMessage(hCap, WM_CAP_DRIVER_CONNECT, 0, 0)	
        '        Call SendCamMessage(hCap, WM_CAP_SET_PREVIEWRATE, 66, 0&)	
        '        Call SendCamMessage(hCap, WM_CAP_SET_PREVIEW, CLng(True), 0&)	
        '        mIsCamStart = True	
        '    End If	

        '    cmd1.Caption = "Start &Cam"	
        '    cmd2.Caption = "&Format Cam"	
        '    cmd3.Caption = "&Close Cam"	
        '    cmd4.Caption = "&Save Image"	


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

        If mIsCamStart = True Then
            temp = SendCamMessage(hCap, WM_CAP_DRIVER_DISCONNECT, 0, 0)
            mIsCamStart = False
        End If
        hCap = 0
        Timer1.Enabled = False
        Timer1.Interval = 1

        mAccountCode = "-1"
        lblMkey.Text = ""
        txtVNo.Text = ""
        txtVDate.Text = GetServerDate() & " " & GetServerTime()
        txtVisitorName.Text = ""
        txtCompanyName.Text = ""
        TxtWhomToMeet.Text = ""
        txtEmailID.Text = ""
        cboPurpose.SelectedIndex = 0
        txtCardNo.Text = ""
        cboCardType.SelectedIndex = 0
        txtOutTime.Text = "__/__/____"
        TxtOutTm.Text = "__:__"
        lblFilePath.Text = ""
        txtMobileNo.Text = ""
        ImagePhoto.Image = Nothing 'CDLPhoto.FileName	
        ImagePhoto.Image = Nothing
        cmdeMailResend.Enabled = False

        cmdVideoFormat.Enabled = False
        cmdCapture.Enabled = False
        cmdeMailResend.Enabled = False
        cmdSearchMobile.Enabled = True
        txtMobileNo.Enabled = True

        txtVDate.Enabled = False
        SprdMain.Enabled = True
        MainClass.ClearGrid(SprdMain, ConRowHeight)

        chkOut.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkOut.Enabled = True

        With SprdMain
            .MaxRows = 4
            .Row = 1
            .Col = ColDescription
            .Text = "MOBILE"

            .Row = 2
            .Col = ColDescription
            .Text = "VEHICLE"

            .Row = 3
            .Col = ColDescription
            .Text = "LAPTOP"

            .Row = 4
            .Col = ColDescription
            .Text = "OTHERS"
        End With
        FormatSprdMain(-1)

        MainClass.ButtonStatus(Me, XRIGHT, RsVisitorMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume	
    End Sub

    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim SqlStr As String = ""


        With SprdMain
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = ColDescription
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = 20
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 15)

            .Col = ColAvailable
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(.Col, 8.5)
            .Value = CStr(System.Windows.Forms.CheckState.Unchecked)

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = 100
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 35)

            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColDescription, ColDescription)
            MainClass.SetSpreadColor(SprdMain, Arow)
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume	
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
        txtVisitorName.Maxlength = RsVisitorMain.Fields("VISITOR_NAME").DefinedSize
        txtCompanyName.Maxlength = RsVisitorMain.Fields("VISITOR_COMPANYNAME").DefinedSize
        TxtWhomToMeet.Maxlength = RsVisitorMain.Fields("WHOM_TO_MEET").DefinedSize
        txtCardNo.Maxlength = RsVisitorMain.Fields("CARD_NO").Precision
        txtEmailID.Maxlength = RsVisitorMain.Fields("EMAIL_ID").DefinedSize
        txtOutTime.MaxLength = 10 '' RsVisitorMain.Fields("REF_DATE").DefinedSize	
        TxtOutTm.MaxLength = 5
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume	
    End Sub

    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
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

        If FYChk(VB6.Format(txtVDate.Text, "DD/MM/YYYY")) = False Then
            FieldsVarification = False
            If txtVDate.Enabled = True Then txtVDate.Focus()
            Exit Function
        End If

        If Trim(TxtWhomToMeet.Text) = "" Then
            MsgInformation("Our Whom to Meet is Blank")
            FieldsVarification = False
            TxtWhomToMeet.Focus()
            Exit Function
        End If

        If Trim(txtVisitorName.Text) = "" Then
            MsgInformation("Visitor Name is Blank")
            FieldsVarification = False
            txtVisitorName.Focus()
            Exit Function
        End If

        If Trim(cboPurpose.Text) = "" Then
            MsgInformation("Purpose is Blank.")
            FieldsVarification = False
            cboPurpose.Focus()
            Exit Function
        End If

        If Trim(txtCardNo.Text) = "" Then
            MsgInformation("Card No is Blank")
            FieldsVarification = False
            txtCardNo.Focus()
            Exit Function
        End If

        If Trim(txtCompanyName.Text) = "" Then
            MsgInformation("Company Name is Blank")
            FieldsVarification = False
            txtCompanyName.Focus()
            Exit Function
        End If

        If CheckPendingCardNo = True Then
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtOutTime.Text) = "" Or Trim(txtOutTime.Text) = "__/__/____" Then

        Else
            If IsDate(txtOutTime.Text) = False Then
                MsgInformation(" Invalid Out Date. Cannot Save")
                txtOutTime.Focus()
                FieldsVarification = False
                Exit Function
            End If
            If Trim(TxtOutTm.Text) = "" Or Trim(TxtOutTm.Text) = "__:__" Then
                MsgInformation(" Invalid Out Time. Cannot Save")
                TxtOutTm.Focus()
                FieldsVarification = False
                Exit Function
            End If
            If IsDate(TxtOutTm.Text) = False Then
                MsgInformation(" Invalid Out Time. Cannot Save")
                TxtOutTm.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If Trim(txtEmailID.Text) <> "" Then
            If CheckEMailValidation((txtEmailID.Text)) = False Then
                MsgInformation("Invalid Email ID.")
                FieldsVarification = False
                Exit Function
            End If
        End If
        '    If MainClass.ValidDataInGrid(sprdMain, ColDescription, "S", "Please Check Supplier.") = False Then FieldsVarification = False: Exit Function	

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        'Resume	
    End Function

    Private Sub frmVisitorEntry_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next

        If mIsCamStart = True Then
            Call SendCamMessage(hCap, WM_CAP_DRIVER_DISCONNECT, 0, 0)
            hCap = 0
            mIsCamStart = False
        End If

        Timer1.Enabled = False
        Timer1.Interval = 1
        Me.Hide()
        Me.Close()
        RsVisitorMain.Close()
        ''RsOpOuts.Close	
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        '    With SprdMain	
        '        SprdMain_LeaveCell .ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False	
        '    End With	
    End Sub
    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim SqlStr As String = ""
        If eventArgs.Col = 0 And eventArgs.Row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdMain, eventArgs.Row, ColDescription)
            MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        End If
    End Sub
    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell
        On Error GoTo ErrPart
        Dim xBillNo As String
        Dim xSupplier As String

        If eventArgs.NewRow = -1 Then Exit Sub

        '    Select Case Col	
        '        Case ColDescription	
        '            SprdMain.Row = SprdMain.ActiveRow	
        '            SprdMain.Col = ColDescription	
        '            xSupplier = SprdMain.Text	
        '            If xSupplier = "" Then Exit Sub	
        '	
        ''            If CheckSupplier(SprdMain.ActiveRow) = True Then	
        ''                FormatSprdMain Row	
        ''                MainClass.SetFocusToCell SprdMain, Row, Available	
        ''            End If	
        '	
        '        Case Available	
        '            SprdMain.Row = SprdMain.ActiveRow	
        '	
        '            SprdMain.Col = ColDescription	
        '            xSupplier = SprdMain.Text	
        '	
        '            SprdMain.Col = Available	
        '            xBillNo = SprdMain.Text	
        '            If xBillNo = "" Then Exit Sub	
        '	
        '            If CheckDuplicateBillNo(xBillNo) = True Then	
        '                FormatSprdMain Row	
        '                MainClass.SetFocusToCell SprdMain, Row, Available	
        '            Else	
        '                MainClass.AddBlankSprdRow SprdMain, ColDescription, ConRowHeight	
        '                FormatSprdMain -1	
        '            End If	
        '    End Select	
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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

    Private Sub Timer1_Tick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Timer1.Tick
        On Error Resume Next

        If hCap = 0 Then Exit Sub

        ' pause the timer	
        Timer1.Enabled = False

        ' get the next frame;	
        Call SendCamMessage(hCap, WM_CAP_GET_FRAME, 0, 0)

        ' copy the frame to the clipboard	
        Call SendCamMessage(hCap, WM_CAP_COPY, 0, 0)

        ' For some reason, the API is not resizing the video	
        ' feed to the width and height provided when the video	
        ' feed was started, so we must resize the image here	
        '     ImagePhoto.Stretch = True	

        '    ImagePhoto.Width = 2385  ' 2325	
        '    ImagePhoto.Height = 2535 ' 1785	

        ' get from the clipboard	
        ImagePhoto.Image = My.Computer.Clipboard.GetImage()


        ' restart the timer	
        System.Windows.Forms.Application.DoEvents()
        If mIsCamStart = True Then
            Timer1.Enabled = True
        End If
    End Sub

    Private Sub txtCardNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCardNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub TxtCardNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCardNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtCompanyName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCompanyName.DoubleClick
        Call SearchCompanyName()
    End Sub

    Private Sub txtCompanyName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCompanyName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchCompanyName()
    End Sub

    Private Sub txtEmailID_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmailID.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtEmailID_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmailID.DoubleClick
        Call SearcheMailID()
    End Sub

    Private Sub txtEmailID_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEmailID.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtEmailID.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEmailID_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtEmailID.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtEmailID_DoubleClick(txtEmailID, New System.EventArgs())
    End Sub

    Private Sub txtEmailID_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmailID.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        If Trim(txtEmailID.Text) = "" Then GoTo EventExitSub

        If CheckEMailValidation((txtEmailID.Text)) = False Then
            MsgInformation("Invalid Email ID.")
            Cancel = True
        End If

        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub SearcheMailID()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        '        If ADDMode = True Then	
        SqlStr = SqlStr & " AND (EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE='')"
        '        Else	
        '            SqlStr = SqlStr & vbCrLf & " AND (EMP_LEAVE_DATE IS NULL OR (EMP_LEAVE_DATE >= '" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "'))"	
        '        End If	

        If MainClass.SearchGridMaster((txtEmailID.Text), "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_EMAILID_OFF", , , SqlStr) = True Then
            txtEmailID.Text = AcName1
            '            txtEmailIDName.Text = AcName1	
            '            txtEmailID_Validate False	
            '            If txtEmailID.Enabled = True Then txtEmailID.SetFocus	
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchEmp()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        '        If ADDMode = True Then	
        SqlStr = SqlStr & " AND (EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE='')"
        '        Else	
        '            SqlStr = SqlStr & vbCrLf & " AND (EMP_LEAVE_DATE IS NULL OR (EMP_LEAVE_DATE >= '" & VB6.Format(txtDate.Text, "DD-MMM-YYYY") & "'))"	
        '        End If	

        If MainClass.SearchGridMaster((TxtWhomToMeet.Text), "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_EMAILID_OFF", , , SqlStr) = True Then
            TxtWhomToMeet.Text = AcName
            txtEmailID.Text = AcName1
            '            txtEmailIDName.Text = AcName1	
            '            txtEmailID_Validate False	
            '            If txtEmailID.Enabled = True Then txtEmailID.SetFocus	
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchCompanyName()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.SearchGridMaster((txtCompanyName.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "", , , SqlStr) = True Then
            txtCompanyName.Text = AcName
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub TxtOutTm_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtOutTm.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtOutTm_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtOutTm.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(TxtOutTm.Text) = "" Then GoTo EventExitSub
        If Trim(TxtOutTm.Text) = "__:__" Then GoTo EventExitSub

        If Not IsDate(TxtOutTm.Text) Then
            MsgBox("Invalid Out Time.", MsgBoxStyle.Information)
            TxtOutTm.Text = ""
            Cancel = True
        End If

        '    If Len(TxtOutTm.Text) = 4 Then	
        '        TxtOutTm.Text = Format(Left(TxtOutTm.Text, 2), "00") & ":" & vb6.Format(Right(TxtOutTm.Text, 2), "00")	
        '    End If	
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtVisitorName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVisitorName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtVisitorName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVisitorName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtVisitorName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtOutTime_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOutTime.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtOutTime_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtOutTime.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtOutTime.Text) = "" Then GoTo EventExitSub

        If Trim(txtOutTime.Text) = "__/__/____" Then GoTo EventExitSub

        If Not IsDate(txtOutTime.Text) Then
            MsgBox("Invalid Out Time.", MsgBoxStyle.Information)
            txtOutTime.Text = ""
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub TxtWhomToMeet_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtWhomToMeet.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtWhomToMeet_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtWhomToMeet.DoubleClick
        Call SearchEmp()
    End Sub


    Private Sub TxtWhomToMeet_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtWhomToMeet.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtWhomToMeet.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtCompanyName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCompanyName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCompanyName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCompanyName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCompanyName.Text)
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
        If Not RsVisitorMain.EOF Then
            With RsVisitorMain
                lblMkey.Text = IIf(IsDbNull(.Fields("mKey").Value), "", .Fields("mKey").Value)
                txtVNo.Text = IIf(IsDbNull(.Fields("REF_NO").Value), "", .Fields("REF_NO").Value)
                txtVDate.Text = VB6.Format(IIf(IsDbNull(.Fields("REF_DATE").Value), "", .Fields("REF_DATE").Value), "DD/MM/YYYY HH:MM")

                txtVisitorName.Text = IIf(IsDbNull(.Fields("VISITOR_NAME").Value), "", .Fields("VISITOR_NAME").Value)
                txtCompanyName.Text = IIf(IsDbNull(.Fields("VISITOR_COMPANYNAME").Value), "", .Fields("VISITOR_COMPANYNAME").Value)
                TxtWhomToMeet.Text = IIf(IsDbNull(.Fields("WHOM_TO_MEET").Value), "", .Fields("WHOM_TO_MEET").Value)
                txtEmailID.Text = IIf(IsDbNull(.Fields("EMAIL_ID").Value), "", .Fields("EMAIL_ID").Value)
                txtCardNo.Text = VB6.Format(IIf(IsDbNull(.Fields("CARD_NO").Value), 0, .Fields("CARD_NO").Value), "0")

                txtOutTime.Text = VB6.Format(IIf(IsDbNull(.Fields("OUT_TIME").Value), "__/__/____", .Fields("OUT_TIME").Value), "DD/MM/YYYY")
                TxtOutTm.Text = VB6.Format(IIf(IsDbNull(.Fields("OUT_TIME").Value), "__:__", .Fields("OUT_TIME").Value), "HH:MM")

                txtOutTime.Enabled = IIf(Trim(txtOutTime.Text) = "" Or Trim(txtOutTime.Text) = "__/__/____", True, False)
                TxtOutTm.Enabled = IIf(Trim(TxtOutTm.Text) = "" Or Trim(TxtOutTm.Text) = "__:__", True, False)
                chkOut.Enabled = IIf(Trim(txtOutTime.Text) = "" Or Trim(txtOutTime.Text) = "__/__/____", True, False)

                If RsVisitorMain.Fields("PURPOSE").Value = "1" Then
                    cboPurpose.SelectedIndex = 0
                ElseIf RsVisitorMain.Fields("PURPOSE").Value = "2" Then
                    cboPurpose.SelectedIndex = 1
                End If

                If Val(RsVisitorMain.Fields("CARD_TYPE").Value) <= 1 Then
                    cboCardType.SelectedIndex = 0
                Else
                    cboCardType.SelectedIndex = Val(RsVisitorMain.Fields("CARD_TYPE").Value) - 1
                End If

                With SprdMain
                    .Row = 1
                    .Col = ColAvailable
                    .Value = IIf(IsDbNull(RsVisitorMain.Fields("MOBILE_AVAILABLE").Value) Or RsVisitorMain.Fields("MOBILE_AVAILABLE").Value = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)

                    .Col = ColRemarks
                    .Text = IIf(IsDbNull(RsVisitorMain.Fields("MOBILE_DETAILS").Value), "", RsVisitorMain.Fields("MOBILE_DETAILS").Value)

                    .Row = 2
                    .Col = ColAvailable
                    .Value = IIf(IsDbNull(RsVisitorMain.Fields("VEHICLE_AVAILABLE").Value) Or RsVisitorMain.Fields("VEHICLE_AVAILABLE").Value = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
                    .Col = ColRemarks
                    .Text = IIf(IsDbNull(RsVisitorMain.Fields("VEHICLE_DETAILS").Value), "", RsVisitorMain.Fields("VEHICLE_DETAILS").Value)


                    .Row = 3
                    .Col = ColAvailable
                    .Value = IIf(IsDbNull(RsVisitorMain.Fields("LAPTOP_AVAILABLE").Value) Or RsVisitorMain.Fields("LAPTOP_AVAILABLE").Value = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
                    .Col = ColRemarks
                    .Text = IIf(IsDbNull(RsVisitorMain.Fields("LAPTOP_DETAILS").Value), "", RsVisitorMain.Fields("LAPTOP_DETAILS").Value)

                    .Row = 4
                    .Col = ColAvailable
                    .Value = IIf(IsDbNull(RsVisitorMain.Fields("OTHERS_AVAILABLE").Value) Or RsVisitorMain.Fields("OTHERS_AVAILABLE").Value = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
                    .Col = ColRemarks
                    .Text = IIf(IsDbNull(RsVisitorMain.Fields("OTHERS_DETAILS").Value), "", RsVisitorMain.Fields("OTHERS_DETAILS").Value)
                End With

                '            txtVNo.Enabled = False	

                cmdSearchMobile.Enabled = False
                txtMobileNo.Enabled = False
            End With

            '        If ShowPhoto(LblMKey.Caption) = False Then GoTo ShowErrPart	
            If ShowPhotoAccess((lblMkey.Text)) = False Then GoTo ShowErrPart

            cmdVideoFormat.Enabled = False
            cmdCapture.Enabled = False
            cmdeMailResend.Enabled = True

        End If
        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = True    '' False Sandeep 15/05/2022
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
        Dim mVNO As String
        Dim SqlStr As String = ""

        If Trim(txtVNo.Text) = "" Then GoTo EventExitSub
        mVNO = CStr(Val(txtVNo.Text))


        If MODIFYMode = True And RsVisitorMain.BOF = False Then xMKey = RsVisitorMain.Fields("mKey").Value

        SqlStr = "SELECT * FROM PAY_VISITOR_HDR " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND REF_NO=" & Val(mVNO) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsVisitorMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsVisitorMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such REf No. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM PAY_VISITOR_HDR WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MKEY=" & Val(xMKey) & ""

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsVisitorMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cmdCapture_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCapture.Click
        Dim sFileName As String

        Dim W As Short
        Dim H As Short
        Dim pPicFolder As String

        pPicFolder = mLocalPath & "\MYPIC"

        If Not System.IO.Directory.Exists(pPicFolder) Then
            System.IO.Directory.CreateDirectory(pPicFolder)
        End If

        If Dir(mLocalPath & "\myPic", FileAttribute.Directory) = "" Then MkDir((mLocalPath & "\myPic"))
        '    sFileName = mLocalPath & "\myPic\" & vb6.Format(PubCurrDate, "YYYYMMDD") & "_" & vb6.Format(GetServerTime(), "HHMM") & ".jpg"	

        sFileName = pPicFolder & "\" & "Update.jpg" ''Update.jpg"	

        Call SendCamMessage(hCap, WM_CAP_SET_PREVIEW, CInt(False), 0)
        '    With CDialog	
        '        .CancelError = True	
        '        .flags = cdlOFNPathMustExist Or cdlOFNOverwritePrompt	
        ''        .filter = "Bitmap Picture(*.bmp)|*.bmp|JPEG Picture(*.jpg)|*.jpg|All Files|*.*"	
        ''        .ShowSave	
        ''        sFileName = .FileName	
        '	
        '    End With	
        lblFilePath.Text = sFileName
        Call SendCamMessage(hCap, WM_CAP_FILE_SAVEDIB, 0, CStr(sFileName))

        ImagePhoto.Image = Nothing 'CDLPhoto.FileName	
        ImagePhoto.Image = Nothing

        System.Windows.Forms.Application.DoEvents()
DoFinally:
        '    Call SendCamMessage(hCap, WM_CAP_SET_PREVIEW, 0, 0)	


        Call SendCamMessage(hCap, WM_CAP_DRIVER_DISCONNECT, 0, 0)
        mIsCamStart = False
        hCap = 0
        Timer1.Enabled = False
        Timer1.Interval = 1

        ImagePhoto.Image = System.Drawing.Image.FromFile(sFileName) 'mLocalPath & "\myPic\20130806_1535.bmp")  ''	

        ''test	

        ImagePhoto.Width = VB6.TwipsToPixelsX(2385)
        ImagePhoto.Height = VB6.TwipsToPixelsY(2535)



        System.Windows.Forms.Application.DoEvents()

        cmdVideoFormat.Enabled = False
        cmdCapture.Enabled = False
        cmdeMailResend.Enabled = True

    End Sub



    Private Sub TxtWhomToMeet_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtWhomToMeet.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchEmp()
    End Sub


    Private Sub TxtWhomToMeet_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtWhomToMeet.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        If Trim(TxtWhomToMeet.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(Trim(TxtWhomToMeet.Text), "EMP_NAME", "EMP_EMAILID_OFF", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            If Trim(txtEmailID.Text) = "" Then
                txtEmailID.Text = MasterNo
            End If
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
