Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmCandidateMst
    Inherits System.Windows.Forms.Form
    Dim RsCandidate As ADODB.Recordset = Nothing

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection

    Dim Shw As Boolean
    Dim xCode As String
    Dim SqlStr As String = ""
    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12

    Private Const ColCode As Short = 1
    Private Const ColDesc As Short = 2
    Private Const ColPer As Short = 3
    Private Const ColAmt As Short = 4
    Private Const ColForm1Amt As Short = 5
    Private Const ColChk As Short = 6

    Private Const ColSpouseName As Short = 1
    Private Const ColSpouseRel As Short = 2
    Private Const ColSpouseGender As Short = 3
    Private Const ColBloodGroup As Short = 4
    Private Const ColSpouseDOB As Short = 5

    Private Const ColOpening As Short = 3
    Private Const ColTotEntitle As Short = 4

    Private Function UpdateEmpPhoto(ByRef mCode As String) As Boolean
        On Error GoTo ErrPart
        Dim mFilename As String
        Dim mFromPathName As String
        Dim mToPathName As String
        Dim mTempFileName As String
        Dim mExtName As String

        mFromPathName = lblPhotoFileName.Text

        mTempFileName = mFromPathName
        Do While InStr(1, mTempFileName, ".") > 0
            mTempFileName = Mid(mTempFileName, InStr(1, mTempFileName, ".") + 1)
        Loop
        mExtName = mTempFileName

        mFilename = VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00") & VB6.Format(txtRefNo.Text, "000000") & "." & mExtName

        mToPathName = My.Application.Info.DirectoryPath & "\EmpPhoto\" & mFilename

        ''EmpPhoto

        If CopyFile(mFromPathName, mToPathName, False) Then


            SqlStr = " DELETE FROM PAY_EMPPHOTO_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND REF_NO='" & mCode & "'"
            PubDBCn.Execute(SqlStr)


            SqlStr = " INSERT INTO PAY_EMPPHOTO_MST ( " & vbCrLf & " COMPANY_CODE, REF_NO, DESCRIPTION ) " & vbCrLf & " VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", '" & mCode & "', '" & mToPathName & "' )"

            PubDBCn.Execute(SqlStr)
        End If

        UpdateEmpPhoto = True
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateEmpPhoto = False
    End Function

    Private Function ShowEmpPhoto(ByRef mCode As String) As Boolean

        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mFilename As String
        SqlStr = " SELECT *  FROM PAY_EMPPHOTO_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND REF_NO='" & mCode & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)

        If RsTemp.EOF = False Then
            mFilename = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)
            If mFilename <> "" Then
                ImagePhoto.Image = System.Drawing.Image.FromFile(mFilename)
            End If
            lblPhotoFileName.Text = mFilename
        Else
            lblPhotoFileName.Text = ""
        End If
        ShowEmpPhoto = True
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ShowEmpPhoto = False
    End Function
    Private Sub ViewGrid()

        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        ADataMain.Refresh
            FormatSprdView()
            SprdView.Refresh()

            SprdView.Focus()
            SprdView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            SprdView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsCandidate, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub ShowSprdSpouse(ByRef xCode As String)

        On Error GoTo ErrPart
        Dim RsSpouse As ADODB.Recordset

        Dim SqlStr As String = ""
        Dim cntRow As Integer
        Dim mRel As String
        Dim mGender As String

        MainClass.ClearGrid(sprdSpouse, -1)

        SqlStr = " SELECT * from PAY_CAND_SPOUSE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND REF_NO='" & xCode & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSpouse, ADODB.LockTypeEnum.adLockOptimistic)

        cntRow = 1
        With sprdSpouse
            If RsSpouse.EOF = False Then
                Do While RsSpouse.EOF = False
                    .Row = cntRow
                    .Col = ColSpouseName
                    .Text = IIf(IsDbNull(RsSpouse.Fields("SPOUSE_NAME").Value), "", RsSpouse.Fields("SPOUSE_NAME").Value)

                    .Col = ColSpouseRel
                    mRel = IIf(IsDbNull(RsSpouse.Fields("SPOUSE_REL").Value), "", RsSpouse.Fields("SPOUSE_REL").Value)

                    If mRel = "FATHER" Then
                        .TypeComboBoxCurSel = 1
                    ElseIf mRel = "MOTHER" Then
                        .TypeComboBoxCurSel = 2
                    ElseIf mRel = "WIFE" Then
                        .TypeComboBoxCurSel = 3
                    ElseIf mRel = "SON" Then
                        .TypeComboBoxCurSel = 4
                    ElseIf mRel = "DAUGHTER" Then
                        .TypeComboBoxCurSel = 5
                    ElseIf mRel = "BOTHER" Then
                        .TypeComboBoxCurSel = 6
                    ElseIf mRel = "SISTER" Then
                        .TypeComboBoxCurSel = 7
                    End If

                    .Col = ColSpouseGender
                    mGender = IIf(IsDbNull(RsSpouse.Fields("SPOUSE_GENDER").Value), "", RsSpouse.Fields("SPOUSE_GENDER").Value)

                    If mGender = "MALE" Then
                        .TypeComboBoxCurSel = 1
                    ElseIf mGender = "FEMALE" Then
                        .TypeComboBoxCurSel = 2
                    End If

                    .Col = ColBloodGroup
                    .Text = IIf(IsDbNull(RsSpouse.Fields("BLOOD_GROUP").Value), "", RsSpouse.Fields("BLOOD_GROUP").Value)

                    .Col = ColSpouseDOB
                    .Text = VB6.Format(IIf(IsDbNull(RsSpouse.Fields("SPOUSE_DOB").Value), "", RsSpouse.Fields("SPOUSE_DOB").Value), "DD/MM/YYYY")

                    RsSpouse.MoveNext()
                    cntRow = cntRow + 1
                    .MaxRows = .MaxRows + 1

                Loop
            End If
            FormatSprdSpouse(-1)
        End With
        Exit Sub
ErrPart:

    End Sub



    Private Sub FormatSprdSpouse(ByRef mRow As Integer)

        On Error GoTo ERR1
        With sprdSpouse

            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight * 1.5)

            .Col = ColSpouseName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColSpouseName, 30)

            .Col = ColSpouseRel
            If FormActive = False Then
                .CellType = SS_CELL_TYPE_COMBOBOX
                .TypeComboBoxList = "" & Chr(9) & "FATHER" & Chr(9) & "MOTHER" & Chr(9) & "WIFE" & Chr(9) & "SON" & Chr(9) & "DAUGHTER" & Chr(9) & "BOTHER" & Chr(9) & "SISTER"

                .TypeComboBoxCurSel = 0
            End If
            .set_ColWidth(ColSpouseRel, 15)

            .Col = ColSpouseGender
            If FormActive = False Then
                .CellType = SS_CELL_TYPE_COMBOBOX
                .TypeComboBoxList = "" & Chr(9) & "MALE" & Chr(9) & "FEMALE"
                .TypeComboBoxCurSel = 0
            End If
            .set_ColWidth(ColSpouseGender, 15)

            .Col = ColBloodGroup
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColBloodGroup, 10)

            .Col = ColSpouseDOB
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeDateFormat = SS_CELL_DATE_FORMAT_DDMMYY
            .TypeDateMin = "01011900"
            .TypeDateMax = "01312030"
            .TypeDateCentury = True
            .set_ColWidth(ColSpouseDOB, 12)

        End With

        MainClass.SetSpreadColor(sprdSpouse, mRow)


        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub

    Private Function UpdateSpouse(ByRef xCode As String) As Boolean
        On Error GoTo UpdateLoanErr

        Dim SqlStr As String = ""
        Dim cntRow As Integer
        Dim xName As String
        Dim xRel As String
        Dim xGender As String
        Dim xSpouseDOB As String
        Dim xBloodGroup As String
        UpdateSpouse = True

        SqlStr = " DELETE FROM PAY_CAND_SPOUSE_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND REF_NO='" & xCode & "' "

        PubDBCn.Execute(SqlStr)

        SqlStr = ""

        With sprdSpouse
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColSpouseName
                xName = Trim(.Text)

                .Col = ColSpouseRel
                xRel = Trim(.Text)

                .Col = ColSpouseGender
                xGender = Trim(.Text)

                .Col = ColBloodGroup
                xBloodGroup = Trim(.Text)

                .Col = ColSpouseDOB
                xSpouseDOB = Trim(.Text)

                If Trim(xName) <> "" Then
                    SqlStr = " Insert Into PAY_CAND_SPOUSE_MST ( " & vbCrLf & " COMPANY_CODE, REF_NO," & vbCrLf & " SPOUSE_NAME, SPOUSE_REL, " & vbCrLf & " SPOUSE_GENDER, SPOUSE_DOB, BLOOD_GROUP" & vbCrLf & " ) VALUES " & vbCrLf & " (" & RsCompany.Fields("COMPANY_CODE").Value & ", '" & xCode & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(xName) & "', '" & MainClass.AllowSingleQuote(xRel) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(xGender) & "', TO_DATE('" & VB6.Format(xSpouseDOB, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(xBloodGroup) & "' " & vbCrLf & " ) "
                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With

        Exit Function
UpdateLoanErr:
        'Resume
        MsgBox(Err.Description)
        UpdateSpouse = False
    End Function


    Private Sub Clear1()

        txtRefNo.Text = ""
        txtEmpCode.Text = ""
        chkIsJoined.CheckState = System.Windows.Forms.CheckState.Unchecked
        TxtName.Text = ""
        txtFName.Text = ""
        txtBloodGroup.Text = ""
        txtDOB.Text = ""
        txtBSalary.Text = ""
        txtGSalary.Text = ""
        txtDeduction.Text = ""
        txtNetSalary.Text = ""
        txtCTC.Text = ""
        txtQualification.Text = ""
        txtLastCompany.Text = ""
        txtExperience.Text = ""
        txtDOJ.Text = ""
        txtDOI.Text = ""
        txtAddress.Text = ""
        txtCity.Text = ""
        txtPinCode.Text = ""
        txtState.Text = ""
        txtPhone.Text = ""
        txtEmail.Text = ""
        txtOffeMail.Text = ""
        txtSpouse.Text = ""
        txtPFNo.Text = ""
        txtESINo.Text = ""
        txtDispensary.Text = ""
        txtPanNo.Text = ""
        txtLICID.Text = ""

        txtAdhaarNo.Text = ""
        txtMobileOff.Text = ""
        txtDOBActual.Text = ""
        txtDOM.Text = ""

        txtForm1BSalary.Text = ""
        txtForm1GSalary.Text = ""
        txtForm1NetSalary.Text = ""

        txtForm1CTC.Text = ""
        txtCostCenter.Text = ""
        txtRefNo.Enabled = True
        cmdSearch.Enabled = True
        cboSex.SelectedIndex = -1
        cboMStatus.SelectedIndex = -1
        cboCorporate.SelectedIndex = 0
        cboESIApp.SelectedIndex = -1
        cboDept.SelectedIndex = -1
        cbodesignation.SelectedIndex = -1
        CboJoinDesignation.SelectedIndex = -1
        cboCatgeory.SelectedIndex = -1
        chkMetroCity.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkBonusApp.CheckState = System.Windows.Forms.CheckState.Checked
        chkLEApp.CheckState = System.Windows.Forms.CheckState.Checked
        ImagePhoto.Image = Nothing 'CDLPhoto.FileName
        ImagePhoto.Image = Nothing
        lblPhotoFileName.Text = ""
        MainClass.ClearGrid(sprdEarn, -1)
        MainClass.ClearGrid(sprdDeduct, -1)
        MainClass.ClearGrid(sprdPerks, -1)
        SSTab1.SelectedIndex = 0

        chkMDApp.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkCFOApp.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkCEOApp.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkHRApp.CheckState = System.Windows.Forms.CheckState.Unchecked

        cboDivision.SelectedIndex = -1
        cboDivision.Enabled = True

        FillSalarySprd()
        MainClass.ButtonStatus(Me, XRIGHT, RsCandidate, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub cboCatgeory_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCatgeory.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboCatgeory_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCatgeory.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboCorporate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCorporate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboCorporate_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCorporate.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboDept_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDept.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cbodesignation_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cbodesignation.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboESIApp_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboESIApp.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub CboJoinDesignation_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CboJoinDesignation.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboMStatus_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboMStatus.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboSex_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboSex.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkBonusApp_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkBonusApp.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkCEOApp_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCEOApp.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkCFOApp_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCFOApp.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkHRApp_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkHRApp.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkLEApp_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkLEApp.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkMDApp_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkMDApp.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkMetroCity_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkMetroCity.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdeMail_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdeMail.Click
        On Error GoTo ErrPart

        Dim mCC As String
        Dim mTo As String
        Dim mFrom As String
        Dim mSubject As String


        Dim mBodyTextHeader As String
        Dim mBodyText As String
        Dim mBodyTextDetail As String
        Dim Cnt As Integer
        Dim mAttachmentFile As String

        If chkIsJoined.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Employee Already Joined, So cann't be send email to concern persons.")
            Exit Sub
        End If

        ' *****************************************************************************
        ' This is where all of the Components Properties are set / Methods called
        ' *****************************************************************************

        mFrom = ""
        If MainClass.ValidateWithMasterTable(PubUserID, "USER_ID", "EMAIL", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mFrom = MasterNo
        End If

        If mFrom = "" Then
            MsgBox("Email id is not Configuration for such User.", MsgBoxStyle.Information)
            Exit Sub
        End If

        mCC = ""
        '    For Cnt = 1 To 4
        '        If Cnt = 1 And chkMDApp.Value = vbUnchecked Then
        '            mTo = GetAuthoritiesEMailID("MD")
        '        ElseIf Cnt = 2 And chkCFOApp.Value = vbUnchecked Then
        '            mTo = GetAuthoritiesEMailID("CFO")
        '        ElseIf Cnt = 3 And chkCEOApp.Value = vbUnchecked Then
        '            mTo = GetAuthoritiesEMailID("CEO")
        '        ElseIf Cnt = 4 And chkHRApp.Value = vbUnchecked Then
        '            mTo = GetAuthoritiesEMailID("HRC")
        '        End If

        mTo = GetAuthoritiesEMailID("HRC")

        If Trim(mTo) <> "" Then
            mSubject = "Approval for New Employee Master in ERP " & Trim(TxtName.Text) & " - " & RsCompany.Fields("Company_Name").Value

            mBodyTextDetail = "<table align=center border=1 cellPadding=2 cellSpacing=0>" & "<tr>" & "<td font size=4 font face=verdana align=Center width=100><b>Candidate Name</b></td>" & "<td font size=4 font face=verdana align=Center width=100><b>Designation</b></td>" & "<td font size=4 font face=verdana align=Center width=100><b>Department</b></td>" & "<td font size=4 font face=verdana align=Center width=100><b>CTC</b></td>" & "</tr>"

            mBodyTextDetail = mBodyTextDetail & "<tr>" & "<td align=Left>" & Trim(TxtName.Text) & "</td>" & "<td align=Left>" & cbodesignation.Text & "</td>" & "<td align=Left>" & cboDept.Text & "</td>" & "<td align=Left>" & VB6.Format(txtCTC.Text, "0.00") & "</td>" & "</tr>"

            mBodyTextDetail = mBodyTextDetail & "</table>"

            mBodyText = "<html><body><br />" & "<b></b>Respected Sir,<br />" & "You are requested to please approve the following candidate for enter the data in ERP.<br />" & "<br />" & "<br />" & mBodyTextDetail & "<br />" & "<br />" & "<br />" & "<br />" & "Best Regards<br />" & "for " & RsCompany.Fields("Company_Name").Value & "<br />" & "</body></html>"



            If SendMailProcess(mFrom, mTo, mCC, "", mAttachmentFile, mSubject, mBodyText) = False Then GoTo ErrPart
        End If
        '    Next

        MsgInformation("Mail Send to the concern persons for approval.")

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)

        '    Resume
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPreview.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPrint(Crystal.DestinationConstants.crptToWindow)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ImagePhoto_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ImagePhoto.DoubleClick
        'Dim mfileName As String
        '    cdgPhoto.filter = "(*.bmp;*.ico;*.gif;*.jpg)/*.bmp;*.ico;*.gif;*.jpg"
        '    cdgPhoto.ShowOpen
        '
        '    'assign the image file name to the fileName variable
        '    mfileName = cdgPhoto.FileName
        '
        '    'if the file name is valid, load the image in the image control on the form
        '    If mfileName <> "" Then
        '        Set ImagePhoto.Picture = LoadPicture(mfileName)
        '    End If

    End Sub

    Private Sub ReportForPrint(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mRptFileName As String


        PubDBCn.Errors.Clear()

        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_PrintDummyData WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        If TempFillPrintDummyData(sprdEarn, 1, sprdEarn.MaxRows, 0, sprdEarn.MaxCols, "E", PubDBCn) = False Then GoTo ERR2
        If TempFillPrintDummyData(sprdPerks, 1, sprdPerks.MaxRows, 0, sprdPerks.MaxCols, "P", PubDBCn) = False Then GoTo ERR2

        PubDBCn.CommitTrans()

        frmPrintAppLtr.ShowDialog()

        If G_PrintAppLtr = False Then
            Exit Sub
        End If

        'Insert Data from Grid to PrintDummyData Table...


        'Select Record for print...

        SqlStr = ""

        SqlStr = " SELECT * FROM Temp_PrintDummyData PRINTDUMMYDATA " & vbCrLf & " WHERE  UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY FIELD10, SUBROW"

        mSubTitle = ""
        mTitle = ""

        If frmPrintAppLtr.OptPrint(0).Checked = True Then
            mRptFileName = "Appointment_Ltr.rpt"
        ElseIf frmPrintAppLtr.OptPrint(1).Checked = True Then
            mRptFileName = "IntentLetter.rpt"
        ElseIf frmPrintAppLtr.OptPrint(3).Checked = True Then
            mRptFileName = "ConfirmationLetter.rpt"
        ElseIf frmPrintAppLtr.OptPrint(4).Checked = True Then
            mRptFileName = "JoiningKit.rpt"
        Else
            mRptFileName = "SalaryStructure.rpt"
        End If


        Call ShowReport(SqlStr, mRptFileName, Mode, mTitle, mSubTitle)

        frmPrintAppLtr.Close()

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        If Err.Number = 32755 Or Err.Number = 20507 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        Exit Sub
ERR2:
        MsgInformation(Err.Description)
        If Err.Number = 32755 Or Err.Number = 20507 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        PubDBCn.RollbackTrans()
        'Resume
    End Sub
    Private Function TempFillPrintDummyData(ByRef GridName As Object, ByVal prmStartGridRow As Integer, ByVal prmEndGridRow As Integer, ByVal prmStartGridCol As Integer, ByVal prmEndGridCol As Integer, ByRef mDefaultValue As String, ByRef mPvtDBCn As ADODB.Connection) As Boolean

        ' This procedure fills the Grid Data into PrintDummy table for printing...
        On Error GoTo PrintDummyErr

        Dim RSPrintDummy As ADODB.Recordset
        Dim FieldCnt As Short
        Dim RowNum As Short
        Dim FieldNum As Short
        Dim GetData As String
        Dim SetData As String
        Dim SqlStr As String = ""

        For RowNum = prmStartGridRow To prmEndGridRow
            FieldCnt = 1
            SetData = ""
            GetData = ""
            GridName.Row = RowNum
            For FieldNum = prmStartGridCol To prmEndGridCol
                GridName.Col = FieldNum
                If FieldNum = prmStartGridCol Then
                    SetData = "FIELD" & FieldCnt
                    GetData = "'" & MainClass.AllowSingleQuote(GridName.Text) & "'"
                ElseIf FieldNum = ColAmt Then
                    SetData = SetData & ", " & "FIELD" & FieldCnt
                    GetData = GetData & ", " & "'" & VB6.Format(Val(GridName.Text), "0.00") & "'"
                Else
                    SetData = SetData & ", " & "FIELD" & FieldCnt
                    GetData = GetData & ", " & "'" & MainClass.AllowSingleQuote(GridName.Text) & "'"
                End If
                FieldCnt = FieldCnt + 1
            Next
            SqlStr = " INSERT INTO TEMP_PRINTDUMMYDATA (USERID, SUBROW, FIELD10, " & vbCrLf & " " & SetData & ") " & vbCrLf & " VALUES (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & RowNum & ", '" & mDefaultValue & "'," & vbCrLf & " " & GetData & ") "
            mPvtDBCn.Execute(SqlStr)
NextRec:
        Next



        TempFillPrintDummyData = True

        Exit Function
PrintDummyErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        TempFillPrintDummyData = False
        '    mPvtDBCn.RollbackTrans
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function


    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)

        Dim mEmpName As String
        Dim mEmpDegn As String
        Dim mWef As String
        Dim mBasic As String
        Dim mGrossAmount As String
        Dim mAddress As String
        Dim mGrade As String
        Dim mUnit As String

        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)


        If MainClass.ValidateWithMasterTable((cbodesignation.Text), "DESG_DESC", "GRADE_CODE", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mGrade = MasterNo
        End If

        If RsCompany.Fields("COMPANY_CODE").Value = 1 Then
            mUnit = " - UNIT I"
        Else
            mUnit = ""
        End If

        MainClass.AssignCRptFormulas(Report1, "mEmpName='" & MainClass.AllowSingleQuote(TxtName.Text) & "'")
        MainClass.AssignCRptFormulas(Report1, "mAddress='" & MainClass.AllowSingleQuote(txtAddress.Text) & "'")
        MainClass.AssignCRptFormulas(Report1, "mCity='" & MainClass.AllowSingleQuote(txtCity.Text) & "'")
        MainClass.AssignCRptFormulas(Report1, "mPinCode='" & MainClass.AllowSingleQuote(txtPinCode.Text) & "'")
        MainClass.AssignCRptFormulas(Report1, "mState='" & MainClass.AllowSingleQuote(txtState.Text) & "'")
        MainClass.AssignCRptFormulas(Report1, "mEmpDegn='" & MainClass.AllowSingleQuote(cbodesignation.Text) & "'")
        MainClass.AssignCRptFormulas(Report1, "Dept='" & MainClass.AllowSingleQuote(cboDept.Text) & "'")
        MainClass.AssignCRptFormulas(Report1, "Grade='" & MainClass.AllowSingleQuote(mGrade) & "'")
        MainClass.AssignCRptFormulas(Report1, "mDOI='" & VB6.Format(txtDOI.Text, "DD/MM/YYYY") & "'")
        MainClass.AssignCRptFormulas(Report1, "mDOJ='" & VB6.Format(txtDOJ.Text, "DD/MM/YYYY") & "'")
        MainClass.AssignCRptFormulas(Report1, "mUnit='" & mUnit & "'")
        MainClass.AssignCRptFormulas(Report1, "mConfirmationDate=''")
        MainClass.AssignCRptFormulas(Report1, "BasicSalary='" & Val(txtBSalary.Text) & "'")

        '    MainClass.AssignCRptFormulas Report1, "mGrossAmount='" & txtGSalary.Text & "'"

        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub

    Private Sub sprdPerks_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles sprdPerks.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub sprdPerks_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles sprdPerks.LeaveCell
        On Error GoTo ErrPart
        Dim xPer As Double
        Dim I As Integer

        If eventArgs.NewRow = -1 Then Exit Sub
        sprdPerks.Row = eventArgs.row

        With sprdPerks
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColPer
                xPer = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColAmt
                If xPer <> 0 Then
                    .Text = CStr(xPer * Val(txtBSalary.Text) / 100)
                End If
            Next
        End With

        '    Select Case Col
        '        Case ColPer
        '            sprdPerks.Row = sprdPerks.ActiveRow
        '
        '            sprdPerks.Col = ColPer
        '            xPer = IIf(IsNumeric(sprdPerks.Text), sprdPerks.Text, 0)
        '
        '            sprdPerks.Col = ColAmt
        '            If xPer <> 0 Then
        '                sprdPerks.Text = CStr(xPer * Val(txtBSalary.Text) / 100)
        '            End If
        '    End Select
        CalcPFESI()
        CalcGrossSalary()
        Exit Sub
ErrPart:
        'Resume
        MsgBox(Err.Description)
    End Sub
    Private Sub sprdPerks_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles sprdPerks.Leave
        'With sprdPerks
        '    sprdPerks_LeaveCell(sprdPerks, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        'End With
    End Sub

    Private Sub sprdSpouse_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles sprdSpouse.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub sprdSpouse_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles sprdSpouse.ClickEvent

        On Error GoTo ERR1
        Select Case eventArgs.Col
            Case 0
                If eventArgs.Row > 0 And sprdSpouse.Enabled = True Then
                    MainClass.DeleteSprdRow(sprdSpouse, eventArgs.Row, ColSpouseName)
                    MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
                End If
        End Select
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub sprdSpouse_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles sprdSpouse.LeaveCell

        On Error GoTo ERR1

        If eventArgs.NewRow = -1 Then Exit Sub
        Select Case eventArgs.col

            Case ColSpouseName

                sprdSpouse.Row = eventArgs.row
                sprdSpouse.Col = ColSpouseName

                If Trim(sprdSpouse.Text) <> "" Then
                    If sprdSpouse.MaxRows = sprdSpouse.ActiveRow Then
                        MainClass.AddBlankSprdRow(sprdSpouse, ColSpouseName, ConRowHeight)
                        FormatSprdSpouse(-1)
                    End If
                End If

        End Select
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick

        SqlStr = ""
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow

        SqlStr = "SELECT * FROM PAY_CANDIDATE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND REF_NO='" & MainClass.AllowSingleQuote((SprdView.Text)) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCandidate, ADODB.LockTypeEnum.adLockOptimistic)
        If RsCandidate.EOF = False Then
            Clear1()
            Show1()
            CmdView_Click(CmdView, New System.EventArgs())
        End If
    End Sub
    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub



    Private Sub txtAddress_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAddress.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtAddress.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtBloodGroup_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBloodGroup.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBloodGroup_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBloodGroup.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtBloodGroup.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtBSalary_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBSalary.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim I As Integer
        Dim xPer As Double

        With sprdEarn
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColPer
                xPer = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColAmt
                If xPer <> 0 Then
                    .Text = CStr(xPer * Val(txtBSalary.Text) / 100)
                End If
            Next
        End With

        With sprdDeduct
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColPer
                xPer = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColAmt
                If xPer <> 0 Then
                    .Text = CStr(xPer * Val(txtBSalary.Text) / 100)
                End If
            Next
        End With

        With sprdPerks
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColPer
                xPer = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColAmt
                If xPer <> 0 Then
                    .Text = CStr(xPer * Val(txtBSalary.Text) / 100)
                End If
            Next
        End With

        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCostCenter_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCostCenter.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtCostCenter_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCostCenter.DoubleClick
        Call SearchCCenter()
    End Sub
    Private Sub txtCostCenter_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCostCenter.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCostCenter.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCostCenter_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCostCenter.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            Call SearchCCenter()
        End If
    End Sub
    Private Sub txtCostCenter_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCostCenter.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ErrPart
        Dim mCostCenter As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mDeptCode As String

        mCostCenter = Trim(txtCostCenter.Text)

        If mCostCenter = "" Then GoTo EventExitSub

        If Trim(cboDept.Text) = "" Then
            MsgInformation("Please Select Dept. First.")
            cboDept.Focus()
            GoTo EventExitSub
        End If

        If MainClass.ValidateWithMasterTable((cboDept.Text), "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDeptCode = MasterNo
        Else
            MsgInformation("Invalid Department Code. Cannot Save")
            cboDept.Focus()
            GoTo EventExitSub
        End If

        SqlStr = " SELECT IH.CC_CODE, IH.CC_DESC, ID.DEPT_CODE " & vbCrLf & " FROM FIN_CCENTER_HDR IH, FIN_CCENTER_DET ID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=ID.COMPANY_CODE AND IH.CC_CODE=ID.CC_CODE" & vbCrLf & " AND IH.CC_DESC='" & MainClass.AllowSingleQuote(mCostCenter) & "'" & vbCrLf & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(mDeptCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = True Then
            MsgInformation("Invalid Cost Center Code for Department : " & Trim(cboDept.Text))
            Cancel = True
        End If

        '    If MainClass.ValidateWithMasterTable(mCostCenter, "CC_DESC", "CC_CODE", "FIN_CCENTER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
        '        MsgInformation "Invalid Cost Center. Cannot Save"
        '        Cancel = True
        '    End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtCity_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCity.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCity.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDispensary_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDispensary.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDispensary.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDOI_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDOI.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDOI_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDOI.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDOI.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtDOI_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDOI.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDOI.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtDOI.Text) Then
            MsgInformation("Please enter the vaild date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDOJ_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDOJ.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDOJ.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEmail_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEmail.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        '    KeyAscii = MainClass.UpperCase(KeyAscii, txtEmail.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtESINo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtESINo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtESINo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtExperience_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtExperience.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtExperience_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtExperience.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtLastCompany_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLastCompany.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLastCompany_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLastCompany.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtLastCompany.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtLICID_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLICID.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLICID_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLICID.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtLICID.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtAdhaarNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAdhaarNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAdhaarNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAdhaarNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtAdhaarNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtMobileOff_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMobileOff.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMobileOff_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMobileOff.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtMobileOff.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDOBActual_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDOBActual.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDOBActual_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDOBActual.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDOBActual.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDOM_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDOM.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDOM_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDOM.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDOM.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDOBActual_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDOBActual.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDOBActual.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtDOBActual.Text) Then
            MsgInformation("Please enter the vaild date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtDOM_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDOM.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDOM.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtDOM.Text) Then
            MsgInformation("Please enter the vaild date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtOffeMail_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOffeMail.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        If CmdModify.Text = ConcmdmodifyCaption Then
            If chkIsJoined.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("Employee Master Generated, Can't be Modify.")
                Exit Sub
            End If
            ADDMode = False
            MODIFYMode = True
            txtRefNo.Enabled = False
            cmdSearch.Enabled = False
            MainClass.ButtonStatus(Me, XRIGHT, RsCandidate, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            txtRefNo.Enabled = True
            cmdSearch.Enabled = True
            Show1()
        End If
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPrint(Crystal.DestinationConstants.crptToPrinter)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdSelectPhoto_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSelectPhoto.Click
        On Error GoTo IconFileErr
        Dim mFilename As String

        cdgPhotoOpen.Filter = "(*.bmp;*.ico;*.gif;*.jpg)/*.bmp;*.ico;*.gif;*.jpg"
        cdgPhotoOpen.ShowDialog()

        'assign the image file name to the fileName variable
        mFilename = cdgPhotoOpen.FileName

        'if the file name is valid, load the image in the image control on the form
        If mFilename <> "" Then
            ImagePhoto.Image = System.Drawing.Image.FromFile(mFilename)
        End If

        '    'lblPhotoFileName.Caption = ""
        '    CDLPhoto.FileName = ""
        '    CDLPhoto.FilterIndex = 1
        '    CDLPhoto.DefaultExt = "*.ico"
        '    CDLPhoto.filter = "Pictures (*.bmp;*.ico)|*.bmp;*.ico"
        '    CDLPhoto.InitDir = App.path + "\global"
        '    CDLPhoto.CancelError = False
        '    CDLPhoto.Action = 1
        '    If CDLPhoto.FileName <> "" Then
        '        ImagePhoto.Picture = LoadPicture(CDLPhoto.FileName)
        '    ElseIf CDLPhoto.FileName = "" Then
        '        ImagePhoto.Picture = LoadPicture(CDLPhoto.FileName)
        '    End If
        lblPhotoFileName.Text = mFilename
        ' DataChanged
IconFileErr:
        'If cdgPhoto.CancelError = True Then MsgInformation("Cancelled by user")

    End Sub

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtBSalary.Enabled = True
            If txtRefNo.Enabled = True Then txtRefNo.Focus()
        Else
            ADDMode = False
            MODIFYMode = False
            If RsCandidate.EOF = False Then RsCandidate.MoveFirst()
            Show1()
        End If
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If TxtName.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub

        If chkIsJoined.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Employee Master Generated, Can't be Deleted.")
            Exit Sub
        End If

        If Not RsCandidate.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                If Delete1 = False Then GoTo DelErrPart
                If RsCandidate.EOF = True Then
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
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtRefNo.Text), "PAY_CANDIDATE_MST", "EMP_NAME", "TO_CHAR(REF_NO)", , , SqlStr) = True Then
            txtRefNo.Text = AcName1
            txtRefNo_Validating(txtRefNo, New System.ComponentModel.CancelEventArgs(False))
            If txtRefNo.Enabled = True Then txtRefNo.Focus()
        End If

        Exit Sub

    End Sub
    Private Sub frmCandidateMst_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub sprdDeduct_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles sprdDeduct.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub sprdDeduct_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles sprdDeduct.LeaveCell
        On Error GoTo ErrPart
        Dim I As Integer
        Dim xPer As Double

        If eventArgs.NewRow = -1 Then Exit Sub
        sprdDeduct.Row = eventArgs.row

        With sprdDeduct
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColPer
                xPer = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColAmt
                If xPer <> 0 Then
                    .Text = CStr(xPer * Val(txtBSalary.Text) / 100)
                End If
            Next
        End With

        CalcPFESI()
        CalcGrossSalary()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        'Resume
    End Sub

    Private Sub sprdDeduct_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles sprdDeduct.Leave
        'With sprdDeduct
        '    sprdDeduct_LeaveCell(sprdDeduct, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        'End With
    End Sub


    Private Sub sprdEarn_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles sprdEarn.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub sprdEarn_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles sprdEarn.LeaveCell
        On Error GoTo ErrPart
        Dim xPer As Double
        Dim I As Integer

        If eventArgs.NewRow = -1 Then Exit Sub
        sprdEarn.Row = eventArgs.row

        For I = 1 To sprdEarn.MaxRows
            sprdEarn.Row = I

            sprdEarn.Col = ColPer
            xPer = IIf(IsNumeric(sprdEarn.Text), sprdEarn.Text, 0)

            sprdEarn.Col = ColAmt
            If xPer <> 0 Then
                sprdEarn.Text = CStr(xPer * Val(txtBSalary.Text) / 100)
            End If
        Next

        '    Select Case Col
        '        Case ColPer
        '            sprdEarn.Row = sprdEarn.ActiveRow
        '
        '            sprdEarn.Col = ColPer
        '            xPer = IIf(IsNumeric(sprdEarn.Text), sprdEarn.Text, 0)
        '
        '            sprdEarn.Col = ColAmt
        '            If xPer <> 0 Then
        '                sprdEarn.Text = CStr(xPer * Val(txtBSalary.Text) / 100)
        '            End If
        '    End Select
        CalcPFESI()
        CalcGrossSalary()
        Exit Sub
ErrPart:
        'Resume
        MsgBox(Err.Description)
    End Sub


    Private Sub sprdEarn_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles sprdEarn.Leave
        'With sprdEarn
        '    sprdEarn_LeaveCell(sprdEarn, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        'End With
    End Sub


    Private Sub txtAddress_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAddress.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtCity_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCity.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPANNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPanNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPANNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPanNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPanNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPanNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPanNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtPanNo.Text) <> "" Then
            If CheckPANValidation((txtPanNo.Text)) = False Then
                MsgInformation("Invalid PAN No.")
                Cancel = True
            End If
        End If
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPFNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPFNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPFNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPhone_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPhone.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPhone.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPinCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPinCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtBSalary_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBSalary.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtBSalary_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBSalary.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtBSalary_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBSalary.Leave
        CalcGrossSalary()
    End Sub

    Private Sub txtPinCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPinCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPinCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtQualification_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtQualification.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtQualification_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtQualification.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtQualification.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSpouse_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSpouse.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSpouse_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSpouse.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSpouse.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtstate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtState.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtEmail_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmail.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtDispensary_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDispensary.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtDOB_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDOB.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtDOB_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDOB.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDOB.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtDOB.Text) Then
            MsgInformation("Please enter the vaild date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtDOJ_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDOJ.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDOJ_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDOJ.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDOJ.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtDOJ.Text) Then
            MsgInformation("Please enter the vaild date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRefNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRefNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRefNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRefNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtRefNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtESINo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtESINo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtFName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtFName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtFName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtGSalary_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGSalary.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtGSalary_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtGSalary.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtdeduction_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDeduction.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtdeduction_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDeduction.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtNetSalary_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNetSalary.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNetSalary_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNetSalary.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub TxtName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub frmCandidateMst_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        SqlStr = ""
        If FormActive = True Then Exit Sub
        MainClass.UOpenRecordSet("SELECT * FROM PAY_CANDIDATE_MST WHERE 1<>1", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCandidate, ADODB.LockTypeEnum.adLockOptimistic)
        AssignGrid(False)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        settextlength()
        Clear1()
        FormActive = True
        If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '    Resume
    End Sub
    Private Sub frmCandidateMst_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)
        Me.Left = VB6.TwipsToPixelsX(20)
        Me.Top = VB6.TwipsToPixelsY(20)

        FormatSprd(-1)
        FillComboMst()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub
    Private Sub FillComboMst()

        Dim RS As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        cboSex.Items.Clear()
        cboSex.Items.Add("Male")
        cboSex.Items.Add("Female")

        cboMStatus.Items.Clear()
        cboMStatus.Items.Add("Married")
        cboMStatus.Items.Add("Unmarried")

        '************cboPaymentMode Should Be filled in following series****
        '    cboPaymentMode.Clear
        '    cboPaymentMode.AddItem "Cash", 0
        '    cboPaymentMode.AddItem "Cheque", 1
        '    cboPaymentMode.AddItem "DD", 2
        '    cboPaymentMode.AddItem "Bank Transfer", 3
        '    If Trim(cboPaymentMode.Text) = "" Then cboPaymentMode.ListIndex = 0
        '
        '    cboWeeklyOff.Clear
        '    cboWeeklyOff.AddItem "MONDAY"
        '    cboWeeklyOff.AddItem "TUESDAY"
        '    cboWeeklyOff.AddItem "WEDNESSDAY"
        '    cboWeeklyOff.AddItem "THURSDAY"
        '    cboWeeklyOff.AddItem "FRIDAY"
        '    cboWeeklyOff.AddItem "SATURDAY"
        '    cboWeeklyOff.AddItem "SUNDAY"

        MainClass.FillCombo(cboDept, "PAY_DEPT_MST", "DEPT_DESC", , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
        '    MainClass.FillCombo cboMajorDept, "PAY_DEPT_MST", "DEPT_DESC", , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.FillCombo(cbodesignation, "PAY_DESG_MST", "DESG_DESC", , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
        MainClass.FillCombo(CboJoinDesignation, "PAY_DESG_MST", "DESG_DESC", , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")

        cboESIApp.Items.Clear()
        cboESIApp.Items.Add("Yes")
        cboESIApp.Items.Add("No")
        cboESIApp.SelectedIndex = 1
        '
        '    cboEmpCatType.Clear
        '    cboEmpCatType.AddItem "1 : Staff"
        '    cboEmpCatType.AddItem "2 : Workers"
        '    cboEmpCatType.ListIndex = -1
        '
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

        SqlStr = "Select CATEGORY_DESC FROM PAY_CATEGORY_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " Order by CATEGORY_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockOptimistic)

        cboCatgeory.Items.Clear()
        If RS.EOF = False Then
            Do While Not RS.EOF
                cboCatgeory.Items.Add(RS.Fields("CATEGORY_DESC").Value)
                RS.MoveNext()
            Loop
        End If
        cboCatgeory.SelectedIndex = -1

        cboCorporate.Items.Clear()
        cboCorporate.Items.Add("No")
        cboCorporate.Items.Add("Yes")
        cboCorporate.SelectedIndex = 0

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmCandidateMst_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        '    'PvtDBCn.Cancel
        '    'PvtDBCn.Close
        RsCandidate = Nothing
        '    'Set PvtDBCn = Nothing
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim mCategoryName As String
        Dim mCostCenter As String
        Dim mDivisionCode As Double
        Dim mDivisionDesc As String

        Shw = True
        With RsCandidate
            If Not RsCandidate.EOF Then

                txtRefNo.Text = IIf(IsDbNull(.Fields("REF_NO").Value), "", .Fields("REF_NO").Value)
                txtEmpCode.Text = IIf(IsDbNull(.Fields("EMP_CODE").Value), "", .Fields("EMP_CODE").Value)
                chkIsJoined.CheckState = IIf(.Fields("IS_JOINED").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                TxtName.Text = IIf(IsDbNull(.Fields("EMP_NAME").Value), "", .Fields("EMP_NAME").Value)
                txtFName.Text = IIf(IsDbNull(.Fields("EMP_FNAME").Value), "", .Fields("EMP_FNAME").Value)
                txtBloodGroup.Text = IIf(IsDbNull(.Fields("BLOOD_GROUP").Value), "", .Fields("BLOOD_GROUP").Value)
                txtDOB.Text = VB6.Format(IIf(IsDbNull(.Fields("EMP_DOB").Value), "", .Fields("EMP_DOB").Value), "DD/MM/YYYY")
                txtBSalary.Text = CStr(Val(IIf(IsDbNull(.Fields("BASIC_SALARY").Value), "", .Fields("BASIC_SALARY").Value)))
                txtGSalary.Text = CStr(Val(IIf(IsDbNull(.Fields("GROSS_SALARY").Value), "", .Fields("GROSS_SALARY").Value)))
                '            txtDeduction
                '            txtNetSalary
                txtQualification.Text = IIf(IsDbNull(.Fields("EMP_QUALIFICATION").Value), "", .Fields("EMP_QUALIFICATION").Value)
                txtLastCompany.Text = IIf(IsDbNull(.Fields("EMP_LAST_COMPANY").Value), "", .Fields("EMP_LAST_COMPANY").Value)
                txtExperience.Text = IIf(IsDbNull(.Fields("EMP_TOTEXP").Value), "", .Fields("EMP_TOTEXP").Value)
                '            txtBankName.Text = IIf(IsNull(!EMP_BANK_NAME), "", !EMP_BANK_NAME)
                '            txtBankAcno.Text = IIf(IsNull(!EMP_BANK_NO), "", !EMP_BANK_NO)
                txtDOJ.Text = VB6.Format(IIf(IsDbNull(.Fields("EMP_DOJ").Value), "", .Fields("EMP_DOJ").Value), "DD/MM/YYYY")
                txtDOI.Text = VB6.Format(IIf(IsDbNull(.Fields("EMP_DOI").Value), "", .Fields("EMP_DOI").Value), "DD/MM/YYYY")
                '            txtGroupDOJ.Text = Format(IIf(IsNull(!EMP_GROUP_DOJ), "", !EMP_GROUP_DOJ), "DD/MM/YYYY")
                '            txtDOP.Text = Format(IIf(IsNull(!EMP_DOC), "", !EMP_DOC), "DD/MM/YYYY")
                '            txtDOL.Text = Format(IIf(IsNull(!EMP_LEAVE_DATE), "", !EMP_LEAVE_DATE), "DD/MM/YYYY")
                '            txtReasonForLeaving.Text = IIf(IsNull(!EMP_LEAVE_REASON), "", !EMP_LEAVE_REASON)
                '            txtWorkingFrom.Text = IIf(IsNull(!WORKINGTIMEFROM), "", !WORKINGTIMEFROM)
                '            txtWorkingTo.Text = IIf(IsNull(!WORKINGTIMETO), "", !WORKINGTIMETO)
                '            txtOTRate.Text = IIf(IsNull(!EMP_OT_RATE), "", !EMP_OT_RATE)
                txtAddress.Text = IIf(IsDbNull(.Fields("EMP_ADDR").Value), "", .Fields("EMP_ADDR").Value)
                txtCity.Text = IIf(IsDbNull(.Fields("EMP_CITY").Value), "", .Fields("EMP_CITY").Value)
                txtPinCode.Text = IIf(IsDbNull(.Fields("EMP_PIN").Value), "", .Fields("EMP_PIN").Value)
                txtState.Text = IIf(IsDbNull(.Fields("EMP_STATE").Value), "", .Fields("EMP_STATE").Value)
                txtPhone.Text = IIf(IsDbNull(.Fields("EMP_PHONE_NO").Value), "", .Fields("EMP_PHONE_NO").Value)
                txtEmail.Text = IIf(IsDbNull(.Fields("EMP_EMAILID").Value), "", .Fields("EMP_EMAILID").Value)
                txtOffeMail.Text = IIf(IsDbNull(.Fields("EMP_EMAILID_OFF").Value), "", .Fields("EMP_EMAILID_OFF").Value)
                txtSpouse.Text = IIf(IsDbNull(.Fields("EMP_SPOUSE_NAME").Value), "", .Fields("EMP_SPOUSE_NAME").Value)
                txtPFNo.Text = IIf(IsDbNull(.Fields("EMP_PF_ACNO").Value), "", .Fields("EMP_PF_ACNO").Value)
                txtESINo.Text = IIf(IsDbNull(.Fields("EMP_ESI_NO").Value), "", .Fields("EMP_ESI_NO").Value)
                txtDispensary.Text = IIf(IsDbNull(.Fields("ESI_DISPENSARY").Value), "", .Fields("ESI_DISPENSARY").Value)
                txtPanNo.Text = IIf(IsDbNull(.Fields("EMP_PANNO").Value), "", .Fields("EMP_PANNO").Value)
                txtLICID.Text = IIf(IsDbNull(.Fields("EMP_LICNO").Value), "", .Fields("EMP_LICNO").Value)
                '            txtWEF.Text = Format(IIf(IsNull(!SALARY_EFF_DATE), "", !SALARY_EFF_DATE), "DD/MM/YYYY")

                '            txtLICAmount.Text = Format(IIf(IsNull(!LIC_DED), "0", !LIC_DED), "0.00")
                '            txtBankLoan.Text = Format(IIf(IsNull(!BNKLOAN_DED), "0", !BNKLOAN_DED), "0.00")
                '            txtITAmount.Text = Format(IIf(IsNull(!ITAX_DED), "0", !ITAX_DED), "0.00")

                '            txtLTAAmount.Text = Format(IIf(IsNull(!LTA_AMT), "0", !LTA_AMT), "0.00")
                '            txtBonusPer.Text = Format(IIf(IsNull(!BONUS_PER), "0", !BONUS_PER), "0.00")

                '            If IIf(IsNull(!WEEKLYOFF), "", !WEEKLYOFF) <> "" Then
                '                cboWeeklyOff.Text = !WEEKLYOFF
                '            Else
                '                cboWeeklyOff.ListIndex = -1
                '            End If

                txtAdhaarNo.Text = IIf(IsDbNull(.Fields("EMP_ADHAAR_NO").Value), "", .Fields("EMP_ADHAAR_NO").Value)
                txtMobileOff.Text = IIf(IsDbNull(.Fields("EMP_MOBILE_NO_OFF").Value), "", .Fields("EMP_MOBILE_NO_OFF").Value)
                txtDOBActual.Text = VB6.Format(IIf(IsDbNull(.Fields("EMP_DOB_ACTUAL").Value), IIf(IsDbNull(.Fields("EMP_DOB").Value), "", .Fields("EMP_DOB").Value), .Fields("EMP_DOB_ACTUAL").Value), "DD/MM/YYYY")
                txtDOM.Text = IIf(IsDbNull(.Fields("EMP_DOM").Value), "", .Fields("EMP_DOM").Value)

                If .Fields("JOININGDESIGN").Value <> "" Then
                    If MainClass.ValidateWithMasterTable(.Fields("JOININGDESIGN").Value, "DESG_CODE", "DESG_DESC", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        CboJoinDesignation.Text = MasterNo
                    End If
                Else
                    CboJoinDesignation.SelectedIndex = -1
                End If

                '            Call SetCboText(cboPaymentMode, Val(IIf(IsNull(!PAYMENTMODE), -1, !PAYMENTMODE)))

                If .Fields("EMP_DEPT_CODE").Value <> "" Then
                    If MainClass.ValidateWithMasterTable(.Fields("EMP_DEPT_CODE").Value, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        cboDept.Text = MasterNo
                    End If
                End If

                mCostCenter = IIf(IsDbNull(.Fields("COST_CENTER_CODE").Value), "", .Fields("COST_CENTER_CODE").Value)

                If mCostCenter <> "" Then
                    If MainClass.ValidateWithMasterTable(mCostCenter, "CC_CODE", "CC_DESC", "FIN_CCENTER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        txtCostCenter.Text = MasterNo
                    End If
                End If

                '            If !EMP_MAJOR_DEPT <> "" Then
                '                If MainClass.ValidateWithMasterTable(!EMP_MAJOR_DEPT, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '                    cboMajorDept.Text = MasterNo
                '                End If
                '            End If

                If .Fields("EMP_DESG_CODE").Value <> "" Then
                    If MainClass.ValidateWithMasterTable(Trim(.Fields("EMP_DESG_CODE").Value), "DESG_CODE", "DESG_DESC", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        cbodesignation.Text = MasterNo
                    End If
                End If

                cboSex.Text = IIf(.Fields("EMP_SEX").Value = "M", "Male", "Female")
                cboMStatus.Text = IIf(.Fields("EMP_MARITAL_STATUS").Value = "M", "Married", "Unmarried")


                mDivisionCode = IIf(IsDbNull(.Fields("DIV_CODE").Value), -1, .Fields("DIV_CODE").Value)

                If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionDesc = Trim(MasterNo)
                    cboDivision.Text = mDivisionDesc
                End If

                If .Fields("EMP_ESI_FLAG").Value = "Y" Then
                    cboESIApp.SelectedIndex = 0
                Else
                    cboESIApp.SelectedIndex = 1
                End If

                If .Fields("IS_CORPORATE").Value = "N" Then
                    cboCorporate.SelectedIndex = 0
                Else
                    cboCorporate.SelectedIndex = 1
                End If


                '            If !ADV_ACCOUNT_CODE <> "" Then
                '                If MainClass.ValidateWithMasterTable(!ADV_ACCOUNT_CODE, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '                    txtLoanAcName.Text = MasterNo
                '                End If
                '            End If
                '
                '            txtLoanAcNo.Text = IIf(IsNull(!EMP_LOANNO), "", !EMP_LOANNO)
                '
                '            If !IMPREST_ACCOUNT_CODE <> "" Then
                '                If MainClass.ValidateWithMasterTable(!IMPREST_ACCOUNT_CODE, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '                    txtImprestAcName.Text = MasterNo
                '                End If
                '            End If
                '
                '            If Not IsNull(!CONTRACTOR_CODE) Then
                '                If MainClass.ValidateWithMasterTable(!CONTRACTOR_CODE, "CON_CODE", "CON_NAME", "PAY_CONTRACTOR_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '                    txtContractor.Text = MasterNo
                '                End If
                '            End If

                If IsDbNull(.Fields("EMP_CATG").Value) Then

                Else
                    cboCatgeory.Text = GetEmployeeCategoryName(.Fields("EMP_CATG").Value)
                End If

                '            chkStopSal.Value = IIf(!EMP_STOP_SALARY = "Y", vbChecked, vbUnchecked)
                '            chkGroupInsurance.Value = IIf(!EMP_GROUP_INSURANCE = "Y", vbChecked, vbUnchecked)
                '            chkRGPAuthorization.Value = IIf(!RGP_AUTH = "Y", vbChecked, vbUnchecked)
                chkMetroCity.CheckState = IIf(.Fields("ISMETROCITY").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkBonusApp.CheckState = IIf(.Fields("IS_BONUS_PAYABLE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkLEApp.CheckState = IIf(.Fields("IS_LEAVE_ENCHASE_PAYABLE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                chkMDApp.CheckState = IIf(.Fields("MD_APPROVAL").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkCFOApp.CheckState = IIf(.Fields("CFO_APPROVAL").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkCEOApp.CheckState = IIf(.Fields("CEO_APPROVAL").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkHRApp.CheckState = IIf(.Fields("HR_APPROVAL").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                '            chkStopSal.Enabled = IIf(PubSuperUser = "S" OR PubSuperUser = "A", True, IIf(chkStopSal.Value = vbChecked, True, False))
            End If
        End With

        Shw = False
        ADDMode = False
        MODIFYMode = False
        If RsCandidate.EOF = False Then
            xCode = RsCandidate.Fields("REF_NO").Value
            '        txtRefNo.Enabled = False
            '        cmdSearch.Enabled = False
            '        If ShowEmpPhoto(RsCandidate!REF_NO) = False Then GoTo ShowErrPart
            Call ShowSalary(RsCandidate.Fields("REF_NO").Value)
            Call ShowSprdSpouse(RsCandidate.Fields("REF_NO").Value)

            If Val(txtBSalary.Text) <> 0 Then
                CalcGrossSalary()
            End If
        End If
        SSTab1.SelectedIndex = 0
        MainClass.ButtonStatus(Me, XRIGHT, RsCandidate, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
        If Err.Number = 383 Then
            Resume Next
        End If
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        CalcAddDeduct()
        CalcPFESI()
        If Update1 = True Then
            txtRefNo_Validating(txtRefNo, New System.ComponentModel.CancelEventArgs(False))
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
    Private Function GetCboTextIndex(ByRef pComboBox As System.Windows.Forms.ComboBox) As Integer
        On Error GoTo GetERR
        Dim ii As Integer
        Dim JJ As Integer
        If Trim(pComboBox.Text) = "" Then GetCboTextIndex = -1 : Exit Function
        For ii = 0 To pComboBox.Items.Count - 1
            If pComboBox.Text = VB6.GetItemString(pComboBox, ii) Then
                JJ = JJ + 1
                Exit For
            End If
            JJ = JJ + 1
        Next ii
        GetCboTextIndex = JJ
        Exit Function
GetERR:
        MsgBox(Err.Description)
    End Function
    Private Sub SetCboText(ByRef pComboBox As System.Windows.Forms.ComboBox, ByRef pCboIndex As Integer)
        On Error GoTo GetERR
        Dim ii As Integer
        Dim JJ As Integer
        If pCboIndex = 0 Or pCboIndex = -1 Then pComboBox.Text = "" : Exit Sub
        pComboBox.Text = VB6.GetItemString(pComboBox, pCboIndex - 1)
        Exit Sub
GetERR:
        MsgBox(Err.Description)
    End Sub
    Private Function Update1() As Boolean

        On Error GoTo UpdateError
        Dim mCode As String
        Dim mDeptCode As String
        Dim mDesgCode As String
        Dim mMaritalStatus As String
        Dim mSex As String
        Dim mESIFlag As String
        Dim mJoiningDesc As String
        Dim mGrossSalary As Double
        Dim mMetroCity As String
        Dim mCostCenterCode As String
        Dim mBonusApp As String
        Dim mLEApp As String
        Dim mDivisionCode As Double
        Dim mCategory As String

        Dim mCorporate As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        mSex = IIf(cboSex.Text = "Male", "M", "F")
        mMaritalStatus = IIf(cboMStatus.Text = "Married", "M", "U")
        mCategory = VB.Left(cboCatgeory.Text, 1)

        If IsNumeric(txtGSalary.Text) Then
            mGrossSalary = CDbl(txtGSalary.Text)
        Else
            mGrossSalary = 0
        End If
        mMetroCity = IIf(chkMetroCity.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mBonusApp = IIf(chkBonusApp.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mLEApp = IIf(chkLEApp.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mESIFlag = VB.Left(cboESIApp.Text, 1)

        If MainClass.ValidateWithMasterTable((cboDept.Text), "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDeptCode = MasterNo
        Else
            mDeptCode = CStr(-1)
        End If


        If MainClass.ValidateWithMasterTable((txtCostCenter.Text), "CC_DESC", "CC_CODE", "FIN_CCENTER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCostCenterCode = MasterNo
        Else
            mCostCenterCode = CStr(-1)
        End If

        If MainClass.ValidateWithMasterTable((cbodesignation.Text), "DESG_DESC", "DESG_CODE", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDesgCode = MasterNo
        Else
            mDesgCode = CStr(-1)
        End If

        If MainClass.ValidateWithMasterTable((CboJoinDesignation.Text), "DESG_DESC", "DESG_CODE", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mJoiningDesc = MasterNo
        Else
            mJoiningDesc = CStr(-1)
        End If

        If Trim(txtDOBActual.Text) = "" Then
            txtDOBActual.Text = txtDOB.Text
        End If

        mCorporate = VB.Left(cboCorporate.Text, 1)

        SqlStr = ""

        If ADDMode = True Then
            mCode = CStr(AutoGenSeqNo("REF_NO", "PAY_CANDIDATE_MST"))
            SqlStr = "INSERT INTO PAY_CANDIDATE_MST ( " & vbCrLf _
                & "  COMPANY_CODE,REF_NO,EMP_NAME,  " & vbCrLf _
                & "  EMP_ADDR,EMP_CITY,EMP_STATE,  " & vbCrLf _
                & "  EMP_PIN,EMP_PHONE_NO,EMP_MOBILE_NO,  " & vbCrLf _
                & "  EMP_EMAILID,EMP_EMAILID_OFF,EMP_CONTACT_PERSON,  " & vbCrLf _
                & "  EMP_DEPT_CODE,EMP_MARITAL_STATUS,EMP_SEX,  " & vbCrLf _
                & "  EMP_DESG_CODE,EMP_LAST_COMPANY,EMP_QUALIFICATION,  " & vbCrLf _
                & "  EMP_TOTEXP,EMP_DOB,EMP_DOJ, EMP_DOI, " & vbCrLf _
                & "  EMP_PF_ACNO,EMP_PF_DATE,EMP_ESI_FLAG,  " & vbCrLf _
                & "  EMP_PROH_EXT,COST_CENTER_CODE,GROSS_SALARY,  " & vbCrLf _
                & "  BASIC_SALARY,EMP_FNAME,BLOOD_GROUP,EMP_SPOUSE_NAME,  " & vbCrLf _
                & "  EMP_ESI_NO,ESI_DISPENSARY,EMP_PANNO,  " & vbCrLf _
                & "  EMP_LICNO,JOININGDESIGN,ADDUSER,  " & vbCrLf _
                & "  ADDDATE," & vbCrLf _
                & "  ISMETROCITY,IS_BONUS_PAYABLE,IS_LEAVE_ENCHASE_PAYABLE, " & vbCrLf _
                & "  DIV_CODE,EMP_CATG,IS_CORPORATE, " & vbCrLf _
                & "  EMP_MOBILE_NO_OFF, EMP_DOB_ACTUAL, EMP_DOM, EMP_ADHAAR_NO,CTC_SALARY )" & vbCrLf _
                & "  VALUES ( "

            SqlStr = SqlStr & vbCrLf _
                & " " & RsCompany.Fields("COMPANY_CODE").Value & ",'" & mCode & "',  '" & MainClass.AllowSingleQuote((TxtName.Text)) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtAddress.Text)) & "', '" & MainClass.AllowSingleQuote((txtCity.Text)) & "', '" & MainClass.AllowSingleQuote((txtState.Text)) & "',  " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtPinCode.Text)) & "', '" & MainClass.AllowSingleQuote((txtPhone.Text)) & "',  " & vbCrLf _
                & " '', '" & MainClass.AllowSingleQuote((txtEmail.Text)) & "', '" & MainClass.AllowSingleQuote((txtOffeMail.Text)) & "', '',  " & vbCrLf _
                & " '" & mDeptCode & "', '" & mMaritalStatus & "', '" & mSex & "',  " & vbCrLf _
                & " '" & mDesgCode & "', '" & MainClass.AllowSingleQuote((txtLastCompany.Text)) & "', '" & MainClass.AllowSingleQuote((txtQualification.Text)) & "',  " & vbCrLf _
                & " " & Val(txtExperience.Text) & ", TO_DATE('" & VB6.Format(Trim(txtDOB.Text), "DD-MMM-YYYY") & "','DD-MON-YYYY'), TO_DATE('" & VB6.Format(txtDOJ.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),  TO_DATE('" & VB6.Format(txtDOI.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtPFNo.Text)) & "', '', " & vbCrLf _
                & " '" & mESIFlag & "',  " & vbCrLf _
                & " '', '" & mCostCenterCode & "'," & vbCrLf _
                & " " & mGrossSalary & ", " & Val(txtBSalary.Text) & "," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtFName.Text)) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtBloodGroup.Text)) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtSpouse.Text)) & "',  " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtESINo.Text)) & "', '" & MainClass.AllowSingleQuote((txtDispensary.Text)) & "', '" & MainClass.AllowSingleQuote((txtPanNo.Text)) & "',  " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtLICID.Text)) & "', '" & mJoiningDesc & "',  " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & mMetroCity & "'," & vbCrLf _
                & " '" & mBonusApp & "', '" & mLEApp & "'," & mDivisionCode & ",'" & mCategory & "','" & mCorporate & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtMobileOff.Text)) & "', TO_DATE('" & VB6.Format(txtDOBActual.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtDOM.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote((txtAdhaarNo.Text)) & "', " & CDbl(txtCTC.Text) & "" & vbCrLf _
                & " ) "
        Else
            mCode = CStr(Val(txtRefNo.Text))
            SqlStr = "UPDATE  PAY_CANDIDATE_MST SET " & vbCrLf & " REF_NO='" & mCode & "',  " & vbCrLf & " EMP_NAME='" & MainClass.AllowSingleQuote(TxtName.Text) & "', " & vbCrLf & " EMP_ADDR='" & MainClass.AllowSingleQuote(txtAddress.Text) & "', " & vbCrLf & " EMP_CITY='" & MainClass.AllowSingleQuote(txtCity.Text) & "',  " & vbCrLf & " EMP_STATE='" & MainClass.AllowSingleQuote(txtState.Text) & "', " & vbCrLf & " EMP_PIN='" & MainClass.AllowSingleQuote(txtPinCode.Text) & "', " & vbCrLf & " EMP_PHONE_NO='" & MainClass.AllowSingleQuote(txtPhone.Text) & "' , " & vbCrLf & " EMP_MOBILE_NO='', IS_CORPORATE='" & mCorporate & "'," & vbCrLf & " EMP_EMAILID='" & MainClass.AllowSingleQuote(txtEmail.Text) & "', " & vbCrLf & " EMP_EMAILID_OFF='" & MainClass.AllowSingleQuote(txtOffeMail.Text) & "', " & vbCrLf & " EMP_CONTACT_PERSON='' , " & vbCrLf & " EMP_DEPT_CODE='" & mDeptCode & "', " & vbCrLf & " EMP_MARITAL_STATUS='" & mMaritalStatus & "', " & vbCrLf & " EMP_SEX='" & mSex & "' , " & vbCrLf & " EMP_DESG_CODE='" & mDesgCode & "', DIV_CODE=" & mDivisionCode & ","

            SqlStr = SqlStr & vbCrLf & " EMP_LAST_COMPANY='" & MainClass.AllowSingleQuote(txtLastCompany.Text) & "', " & vbCrLf & " EMP_QUALIFICATION='" & MainClass.AllowSingleQuote(txtQualification.Text) & "',  " & vbCrLf & " EMP_TOTEXP=" & Val(txtExperience.Text) & ", " & vbCrLf & " EMP_DOB=TO_DATE('" & VB6.Format(txtDOB.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " EMP_DOJ=TO_DATE('" & VB6.Format(txtDOJ.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),  " & vbCrLf & " EMP_DOI=TO_DATE('" & VB6.Format(txtDOI.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),  " & vbCrLf & " EMP_PF_ACNO='" & MainClass.AllowSingleQuote(txtPFNo.Text) & "', " & vbCrLf & " EMP_PF_DATE='', " & vbCrLf & " EMP_ESI_FLAG='" & mESIFlag & "', EMP_CATG = '" & mCategory & "'," & vbCrLf & " EMP_PROH_EXT='', " & vbCrLf & " COST_CENTER_CODE='" & mCostCenterCode & "', " & vbCrLf & " GROSS_SALARY=" & mGrossSalary & ", " & vbCrLf & " BASIC_SALARY=" & Val(txtBSalary.Text) & ", CTC_SALARY=" & CDbl(txtCTC.Text) & ", "

            SqlStr = SqlStr & vbCrLf & " EMP_FNAME='" & MainClass.AllowSingleQuote(txtFName.Text) & "', " & vbCrLf & " BLOOD_GROUP='" & MainClass.AllowSingleQuote(txtBloodGroup.Text) & "', " & vbCrLf & " EMP_SPOUSE_NAME='" & MainClass.AllowSingleQuote(txtSpouse.Text) & "',  " & vbCrLf & " EMP_ESI_NO='" & MainClass.AllowSingleQuote(txtESINo.Text) & "', " & vbCrLf & " EMP_MOBILE_NO_OFF='" & MainClass.AllowSingleQuote(txtMobileOff.Text) & "'," & vbCrLf & " EMP_DOB_ACTUAL=TO_DATE('" & VB6.Format(txtDOBActual.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " EMP_DOM=TO_DATE('" & VB6.Format(txtDOM.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " EMP_ADHAAR_NO='" & MainClass.AllowSingleQuote(txtAdhaarNo.Text) & "',"


            SqlStr = SqlStr & vbCrLf & " ESI_DISPENSARY='" & MainClass.AllowSingleQuote(txtDispensary.Text) & "' , " & vbCrLf & " EMP_PANNO='" & MainClass.AllowSingleQuote(txtPanNo.Text) & "', " & vbCrLf & " EMP_LICNO='" & MainClass.AllowSingleQuote(txtLICID.Text) & "', " & vbCrLf & " JOININGDESIGN='" & mJoiningDesc & "',  " & vbCrLf & " ISMETROCITY='" & mMetroCity & "', " & vbCrLf & " IS_BONUS_PAYABLE='" & mBonusApp & "', " & vbCrLf & " IS_LEAVE_ENCHASE_PAYABLE='" & mLEApp & "', " & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " ModDate=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

            SqlStr = SqlStr & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND REF_NO='" & mCode & "'"
        End If


UpdatePart:
        PubDBCn.Execute(SqlStr)

        '    If UpdateEmpPhoto(mCode) = False Then GoTo UpdateError

        '    If CheckSalary(mCode) = False Then
        If UpdateSalaryDef(mCode, (txtDOJ.Text), Val(txtBSalary.Text), Val(txtForm1BSalary.Text), mDesgCode) = False Then GoTo UpdateError
        If UpdateSpouse(mCode) = False Then GoTo UpdateError
        '    End If

        PubDBCn.CommitTrans()
        txtRefNo.Text = mCode
        'RsCandidate.Requery()

        Update1 = True
        Exit Function
UpdateError:
        '    If err.Number = -2147467259 Then
        '        Resume
        '        MsgBox "Can't Modify Transaction Exists Against this Code"
        '        PubDBCn.RollbackTrans
        '        Exit Function
        '    End If
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        Update1 = False
        PubDBCn.RollbackTrans()
        'RsCandidate.Requery()
        PubDBCn.Errors.Clear()
        '   Resume
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function AutoGenSeqNo(ByRef mFieldName As String, ByRef mTableName As String) As Double

        On Error GoTo AutoGenSeqNoErr
        Dim RsAutoGen As ADODB.Recordset = Nothing
        Dim mNewSeqNo As Integer
        Dim SqlStr As String = ""

        SqlStr = ""
        SqlStr = "SELECT Max(TO_NUMBER(substr(" & mFieldName & ",1,length(" & mFieldName & ")-2)))  AS AUTO_KEY " & vbCrLf & " FROM " & mTableName & " " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields.Item("COMPANY_CODE").Value & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAutoGen, ADODB.LockTypeEnum.adLockReadOnly)

        With RsAutoGen
            If .EOF = False Then
                If Not IsDbNull(RsAutoGen.Fields.Item("AUTO_KEY").Value) Then
                    mNewSeqNo = RsAutoGen.Fields.Item("AUTO_KEY").Value + 1
                Else
                    mNewSeqNo = 1
                End If
            End If
        End With

        AutoGenSeqNo = CDbl(mNewSeqNo & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"))
        Exit Function
AutoGenSeqNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub txtRefNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRefNo.DoubleClick
        cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub

    Private Sub txtName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRefNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtRefNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            cmdsearch_Click(cmdsearch, New System.EventArgs())
        End If
    End Sub
    Private Function FieldsVarification() As Boolean

        On Error GoTo ERR1
        Dim xAmount As Decimal
        Dim mCostCenter As String
        Dim mDeptCode As String
        Dim RsTemp As ADODB.Recordset = Nothing

        FieldsVarification = True
        If Trim(TxtName.Text) = "" Then
            MsgInformation("Name is empty. Cannot Save")
            TxtName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If MODIFYMode = True Then
            If Trim(txtRefNo.Text) = "" Then
                MsgInformation("Ref No is empty. Cannot Save")
                txtRefNo.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If chkIsJoined.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Employee Master Generated, Can't be Modify.")
            FieldsVarification = False
            Exit Function
        End If


        '    If Not IsDate(txtDOB.Text) Or Trim(txtDOB.Text) = "" Then
        '        MsgInformation "DOB cann't be blank."
        '        txtDOB.SetFocus
        '        FieldsVarification = False
        '        Exit Function
        '    End If

        If cboCatgeory.SelectedIndex = -1 Then
            MsgInformation("Please enter the Category.")
            cboCatgeory.Focus()
            FieldsVarification = False
            Exit Function
        End If

        '    If Left(cboCatgeory.Text, 1) = "R" Then
        '        If Trim(cboPcRateType.Text) = "" Then
        '            MsgInformation "Pc. Rate Type Cann't be Blank"
        '            cboPcRateType.SetFocus
        '            FieldsVarification = False
        '            Exit Function
        '        End If
        '    End If

        If cboSex.SelectedIndex = -1 Then
            MsgInformation("Please enter the Sex.")
            cboSex.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If cboMStatus.SelectedIndex = -1 Then
            MsgInformation("Please enter the Status.")
            cboMStatus.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Not IsNumeric(txtBSalary.Text) Then
            MsgInformation("Invaild Basic Salary.")
            txtBSalary.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Not IsNumeric(txtGSalary.Text) Then
            MsgInformation("Invaild Gross Salary")
            If txtGSalary.Enabled = True Then txtGSalary.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(cboDept.Text) = "" Then
            MsgInformation("Department Cann't be Blank")
            cboDept.Focus()
            FieldsVarification = False
            Exit Function
        End If



        If MainClass.ValidateWithMasterTable((cboDept.Text), "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDeptCode = MasterNo
        Else
            MsgInformation("Invalid Department Code. Cannot Save")
            cboDept.Focus()
            FieldsVarification = False
            Exit Function
        End If

        mCostCenter = Trim(txtCostCenter.Text)

        If mCostCenter = "" Then
            MsgInformation("Cost Center is empty. Cannot Save")
            txtCostCenter.Focus()
            FieldsVarification = False
            Exit Function
        Else
            SqlStr = " SELECT IH.CC_CODE, IH.CC_DESC, ID.DEPT_CODE " & vbCrLf & " FROM FIN_CCENTER_HDR IH, FIN_CCENTER_DET ID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=ID.COMPANY_CODE AND IH.CC_CODE=ID.CC_CODE" & vbCrLf & " AND IH.CC_DESC='" & MainClass.AllowSingleQuote(mCostCenter) & "'" & vbCrLf & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(mDeptCode) & "'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = True Then
                MsgInformation("Invalid Cost Center Code for Department : " & Trim(cboDept.Text))
                FieldsVarification = False
                txtCostCenter.Focus()
            End If
        End If

        '    If MainClass.ValidateWithMasterTable(mCostCenter, "CC_DESC", "CC_CODE", "FIN_CCENTER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
        '        MsgInformation "Invalid Cost Center. Cannot Save"
        '        txtCostCenter.SetFocus
        '        FieldsVarification = False
        '        Exit Function
        '    End If

        '    If Trim(cboMajorDept.Text) = "" Then
        '        MsgInformation "Major Department Cann't be Blank"
        '        cboMajorDept.SetFocus
        '        FieldsVarification = False
        '        Exit Function
        '    End If

        If Trim(cbodesignation.Text) = "" Then
            MsgInformation("Designation Cann't be Blank")
            cbodesignation.Focus()
            FieldsVarification = False
            Exit Function
        End If


        If Not IsDate(txtDOJ.Text) Then
            MsgInformation("Joining Date cann't be blank.")
            txtDOJ.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Not IsDate(txtDOI.Text) Then
            MsgInformation("Interview Date cann't be blank.")
            txtDOI.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If CDate(txtDOI.Text) > CDate(txtDOJ.Text) Then
            MsgInformation("Interview Date cann't be greater than Joining Date. ")
            txtDOI.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtAddress.Text) = "" Then
            MsgInformation("Address Cann't be Blank")
            txtAddress.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtCity.Text) = "" Then
            MsgInformation("City Cann't be Blank")
            txtCity.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtState.Text) = "" Then
            MsgInformation("State Cann't be Blank")
            txtState.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtPinCode.Text) = "" Then
            MsgInformation("Pin Code Cann't be Blank")
            txtPinCode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(cboESIApp.Text) = "" Then
            MsgInformation("ESI Applicable Cann't be Blank")
            cboESIApp.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(cboDivision.Text) = "" Then
            MsgInformation("Please Select Division.")
            cboDivision.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Val(txtGSalary.Text) > 20000 And Trim(txtPanNo.Text) = "" Then
            MsgInformation("PAN No. is Must, please enter the PAN No.")
            FieldsVarification = False
            txtPanNo.Focus()
            Exit Function
        End If

        If Trim(txtPanNo.Text) <> "" Then
            If CheckPANValidation((txtPanNo.Text)) = False Then
                MsgInformation("Invalid PAN No.")
                FieldsVarification = False
                txtPanNo.Focus()
                Exit Function
            End If
        End If

        '    If Val(txtAdhaarNo.Text) = 0 Then
        '        MsgInformation "Adhaar No is Must, please enter the Adhaar No."
        '        FieldsVarification = False
        '        txtAdhaarNo.SetFocus
        '        Exit Function
        '    End If

        If Val(txtAdhaarNo.Text) = 0 Then
            If MsgQuestion("Adhaar No is Blank, Want to Continue..? ") = CStr(MsgBoxResult.No) Then
                FieldsVarification = False
                txtAdhaarNo.Focus()
                Exit Function
            End If
        Else
            If Len(txtAdhaarNo.Text) <> 12 Then
                MsgInformation("Invalid Adhaar No, please enter the correct Adhaar No.")
                FieldsVarification = False
                txtAdhaarNo.Focus()
                Exit Function
            End If
        End If




        If cboCorporate.Text = "" Then
            MsgInformation("Please Select Corporate .")
            cboCorporate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        'If ADDMode = True Then
        '    If CheckVacantPost(Trim(mDeptCode), VB.Left(cboCorporate.Text, 1), VB6.Format(txtDOJ.Text, "DD/MM/YYYY")) = False Then
        '        MsgInformation("You have not Sanctioned/Vacant post in this Dept.")
        '        FieldsVarification = False
        '        Exit Function
        '    End If
        'End If

        If sprdSpouse.MaxRows > 1 Then
            If MainClass.ValidDataInGrid(sprdSpouse, ColSpouseName, "S", "Spouse Name is Blank.") = False Then FieldsVarification = False : Exit Function
            If MainClass.ValidDataInGrid(sprdSpouse, ColSpouseRel, "S", "Spouse Relation is Blank.") = False Then FieldsVarification = False : Exit Function
            If MainClass.ValidDataInGrid(sprdSpouse, ColSpouseGender, "S", "Spouse Gender is Blank.") = False Then FieldsVarification = False : Exit Function
            If MainClass.ValidDataInGrid(sprdSpouse, ColSpouseDOB, "S", "Spouse D.O.B. is Blank.") = False Then FieldsVarification = False : Exit Function
        End If

Label1:
        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
        End If
        If MODIFYMode = True And (RsCandidate.EOF = True) Then Exit Function
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        FieldsVarification = False
        ''Resume
    End Function
    Private Sub settextlength()

        On Error GoTo ERR1

        txtRefNo.Maxlength = RsCandidate.Fields("REF_NO").DefinedSize
        TxtName.Maxlength = RsCandidate.Fields("EMP_NAME").DefinedSize
        txtFName.Maxlength = RsCandidate.Fields("EMP_FNAME").DefinedSize
        txtBloodGroup.Maxlength = RsCandidate.Fields("BLOOD_GROUP").DefinedSize
        txtDOB.Maxlength = 10
        txtBSalary.Maxlength = RsCandidate.Fields("BASIC_SALARY").Precision
        txtGSalary.Maxlength = RsCandidate.Fields("GROSS_SALARY").Precision
        txtDeduction.Maxlength = RsCandidate.Fields("GROSS_SALARY").Precision
        txtNetSalary.Maxlength = RsCandidate.Fields("GROSS_SALARY").Precision
        txtQualification.Maxlength = RsCandidate.Fields("EMP_QUALIFICATION").DefinedSize

        txtForm1BSalary.MaxLength = RsCandidate.Fields("BASIC_SALARY").Precision
        txtForm1GSalary.MaxLength = RsCandidate.Fields("GROSS_SALARY").Precision
        txtForm1NetSalary.MaxLength = RsCandidate.Fields("GROSS_SALARY").Precision
        txtForm1CTC.MaxLength = RsCandidate.Fields("GROSS_SALARY").Precision

        txtLastCompany.Maxlength = RsCandidate.Fields("EMP_LAST_COMPANY").DefinedSize
        txtExperience.Maxlength = RsCandidate.Fields("EMP_TOTEXP").DefinedSize
        '    txtBankName.MaxLength = RsCandidate.Fields("EMP_BANK_NAME").DefinedSize
        '    txtBankAcno.MaxLength = RsCandidate.Fields("EMP_BANK_NO").DefinedSize
        txtDOJ.Maxlength = 10
        txtDOI.Maxlength = 10

        txtAdhaarNo.Maxlength = RsCandidate.Fields("EMP_ADHAAR_NO").DefinedSize
        txtMobileOff.Maxlength = RsCandidate.Fields("EMP_MOBILE_NO_OFF").DefinedSize
        txtDOBActual.Maxlength = 10
        txtDOM.Maxlength = 10

        '    txtGroupDOJ.MaxLength = 10
        '    txtDOP.MaxLength = 10
        '    txtDOL.MaxLength = 10
        '    txtReasonForLeaving.MaxLength = RsCandidate.Fields("EMP_LEAVE_REASON").DefinedSize
        '    txtWorkingFrom.MaxLength = RsCandidate.Fields("WORKINGTIMEFROM").DefinedSize
        '    txtWorkingTo.MaxLength = RsCandidate.Fields("WORKINGTIMETO").DefinedSize
        '    txtOTRate.MaxLength = RsCandidate.Fields("EMP_OT_RATE").DefinedSize
        txtAddress.Maxlength = RsCandidate.Fields("EMP_ADDR").DefinedSize
        txtCity.Maxlength = RsCandidate.Fields("EMP_CITY").DefinedSize
        txtPinCode.Maxlength = RsCandidate.Fields("EMP_PIN").DefinedSize
        txtState.Maxlength = RsCandidate.Fields("EMP_STATE").DefinedSize
        txtPhone.Maxlength = RsCandidate.Fields("EMP_PHONE_NO").DefinedSize
        txtEmail.Maxlength = RsCandidate.Fields("EMP_EMAILID").DefinedSize
        txtOffeMail.Maxlength = RsCandidate.Fields("EMP_EMAILID_OFF").DefinedSize
        txtSpouse.Maxlength = RsCandidate.Fields("EMP_SPOUSE_NAME").DefinedSize
        txtPFNo.Maxlength = RsCandidate.Fields("EMP_PF_ACNO").DefinedSize
        txtESINo.Maxlength = RsCandidate.Fields("EMP_ESI_NO").DefinedSize
        txtDispensary.Maxlength = RsCandidate.Fields("ESI_DISPENSARY").DefinedSize
        txtPanNo.Maxlength = RsCandidate.Fields("EMP_PANNO").DefinedSize
        txtLICID.Maxlength = RsCandidate.Fields("EMP_LICNO").DefinedSize
        '    txtLoanAcNo.MaxLength = RsCandidate.Fields("EMP_LOANNO").DefinedSize

        '    txtLoanAcName.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
        '    txtImprestAcName.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)

        txtCostCenter.Maxlength = MainClass.SetMaxLength("CC_DESC", "FIN_CCENTER_HDR", PubDBCn)
        '    txtLICAmount.MaxLength = RsCandidate.Fields("LIC_DED").Precision
        '    txtBankLoan.MaxLength = RsCandidate.Fields("BNKLOAN_DED").Precision
        '    txtITAmount.MaxLength = RsCandidate.Fields("ITAX_DED").Precision
        '    txtLTAAmount.MaxLength = RsCandidate.Fields("LTA_AMT").Precision
        '    txtBonusPer.MaxLength = RsCandidate.Fields("BONUS_PER").Precision

        '    txtContractor.MaxLength = MainClass.SetMaxLength("CON_NAME", "PAY_CONTRACTOR_MST", PubDBCn)

        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Sub AssignGrid(ByRef mRefresh As Boolean)

        Dim SqlStr As String = ""
        SqlStr = " SELECT TO_CHAR(REF_NO,'000000') AS REF_NO, EMP_NAME, EMP_DOJ," & vbCrLf & " EMP_FNAME,  EMP_DEPT_CODE, " & vbCrLf & " GROSS_SALARY, EMP_PF_ACNO, EMP_ESI_NO" & vbCrLf & " FROM PAY_CANDIDATE_MST "


        SqlStr = SqlStr & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""


        SqlStr = SqlStr & vbCrLf & " ORDER BY REF_NO,EMP_DOJ,EMP_NAME "


        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()


    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 12)
            .set_ColWidth(0, 5)
            .set_ColWidth(1, 8)
            .set_ColWidth(2, 16)
            .set_ColWidth(3, 12)
            .set_ColWidth(4, 12)
            .set_ColWidth(5, 12)
            .set_ColWidth(6, 12)
            .set_ColWidth(7, 12)
            .set_ColWidth(8, 12)
            .set_ColWidth(9, 12)
            .set_ColWidth(10, 12)
            .set_ColWidth(11, 12)
            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Function Delete1() As Boolean

        On Error GoTo DeleteErr
        Dim mEmpCode As Double
        SqlStr = ""
        'MainClass.ValidateWithMasterTable(TxtName.Text, "EMP_NAME", "REF_NO", "PAY_CANDIDATE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
        'mEmpCode = Val(MasterNo)



        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If InsertIntoDeleteTrn(PubDBCn, "PAY_CANDIDATE_MST", "REF_NO", VB6.Format(mEmpCode, "000000")) = False Then GoTo DeleteErr

        SqlStr = "Delete from PAY_CAND_SALARYDEF_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND REF_NO=" & Val(CStr(txtRefNo.Text)) & ""
        PubDBCn.Execute(SqlStr)

        SqlStr = "Delete from PAY_CANDIDATE_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND REF_NO=" & Val(CStr(txtRefNo.Text)) & ""
        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()
        'RsCandidate.Requery()
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans()
        'RsCandidate.Requery()
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete Transaction Exists Against This Employee." & Err.Description)
            Exit Function
        End If
        MsgBox(Err.Description)
    End Function
    Private Sub txtRefNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRefNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim mEmpCode As String

        If Trim(txtRefNo.Text) = "" Then GoTo EventExitSub
        mEmpCode = Trim(txtRefNo.Text)


        If ADDMode Then
            Clear1()

        End If
        txtRefNo.Text = VB6.Format(mEmpCode, "000000")

        If MODIFYMode = True And RsCandidate.EOF = False Then xCode = RsCandidate.Fields("REF_NO").Value

        SqlStr = ""
        SqlStr = "SELECT * FROM  PAY_CANDIDATE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND REF_NO='" & MainClass.AllowSingleQuote(Trim(txtRefNo.Text)) & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCandidate, ADODB.LockTypeEnum.adLockOptimistic)
        If RsCandidate.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Name Does Not Exist In Master" & vbCrLf & "Click Add To Add In Master", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM  PAY_CANDIDATE_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND REF_NO='" & xCode & "' "


                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCandidate, ADODB.LockTypeEnum.adLockOptimistic)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
        '    Resume
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtPFno_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPFNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtphone_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPhone.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub FillSalarySprd()

        Dim RsADD As ADODB.Recordset
        Dim SqlStr As String = ""

        MainClass.ClearGrid(sprdEarn, -1)
        MainClass.ClearGrid(sprdDeduct, -1)
        MainClass.ClearGrid(sprdPerks, -1)

        SSTab1.SelectedIndex = 3

        SqlStr = " SELECT * From PAY_SALARYHEAD_MST  " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND (CALC_ON=" & ConCalcBSalary & " OR CALC_ON =" & ConCalcFixed & ") " & vbCrLf _
            & " AND TYPE <> " & ConOT & " AND ADDDEDUCT IN (" & ConEarning & "," & ConDeduct & ")"

        SqlStr = SqlStr & vbCrLf & "ORDER BY SEQ "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsADD, ADODB.LockTypeEnum.adLockOptimistic)

        If RsADD.EOF = False Then
            With RsADD
                Do While Not .EOF
                    If .Fields("ADDDEDUCT").Value = ConEarning Then
                        With sprdEarn
                            .Row = .MaxRows
                            .Col = ColCode
                            .Text = CStr(RsADD.Fields("Code").Value)

                            .Col = ColDesc
                            .Text = RsADD.Fields("Name").Value

                            .Col = ColPer
                            .Text = CStr(RsADD.Fields("PERCENTAGE").Value)
                        End With
                    ElseIf .Fields("ADDDEDUCT").Value = ConDeduct Then
                        With sprdDeduct
                            .Row = .MaxRows

                            .Col = ColCode
                            .Text = CStr(RsADD.Fields("Code").Value)

                            .Col = ColDesc
                            .Text = RsADD.Fields("Name").Value

                            .Col = ColPer
                            .Text = CStr(RsADD.Fields("PERCENTAGE").Value)
                        End With
                    End If
                    .MoveNext()
                    If Not .EOF Then
                        If .Fields("ADDDEDUCT").Value = ConEarning Then
                            sprdEarn.Col = 1
                            sprdEarn.Row = sprdEarn.MaxRows
                            If Trim(sprdEarn.Text) <> "" Then
                                sprdEarn.MaxRows = sprdEarn.MaxRows + 1
                                If sprdEarn.MaxRows > 3 Then
                                    sprdEarn.set_ColWidth(ColDesc, 14)
                                End If
                            End If
                        ElseIf .Fields("ADDDEDUCT").Value = ConDeduct Then
                            sprdDeduct.Col = 1
                            sprdDeduct.Row = sprdDeduct.MaxRows
                            If Trim(sprdDeduct.Text) <> "" Then
                                sprdDeduct.MaxRows = sprdDeduct.MaxRows + 1
                                If sprdDeduct.MaxRows > 3 Then
                                    sprdDeduct.set_ColWidth(ColDesc, 14)
                                End If
                            End If
                        End If
                    End If
                Loop
            End With
        End If

        SSTab1.SelectedIndex = 4

        SqlStr = " SELECT * From PAY_SALARYHEAD_MST  " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND (CALC_ON=" & ConCalcBSalary & " OR CALC_ON =" & ConCalcFixed & ") " & vbCrLf _
            & " AND TYPE <> " & ConOT & " AND ADDDEDUCT IN (" & ConPerks & ")"

        SqlStr = SqlStr & vbCrLf & "ORDER BY SEQ "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsADD, ADODB.LockTypeEnum.adLockOptimistic)

        If RsADD.EOF = False Then
            With RsADD
                Do While Not .EOF
                    With sprdPerks
                        .Row = .MaxRows
                        .Col = ColCode
                        .Text = CStr(RsADD.Fields("Code").Value)

                        .Col = ColDesc
                        .Text = RsADD.Fields("Name").Value

                        .Col = ColPer
                        .Text = CStr(RsADD.Fields("PERCENTAGE").Value)
                    End With

                    .MoveNext()

                    sprdPerks.Col = 1
                    sprdPerks.Row = sprdPerks.MaxRows
                    If Trim(sprdPerks.Text) <> "" Then
                        sprdPerks.MaxRows = sprdPerks.MaxRows + 1
                        If sprdPerks.MaxRows > 3 Then
                            sprdPerks.set_ColWidth(ColDesc, 14)
                        End If
                    End If
                Loop
            End With
        End If

        MainClass.ProtectCell(sprdEarn, 1, sprdEarn.MaxRows, ColCode, ColDesc)
        MainClass.ProtectCell(sprdDeduct, 1, sprdDeduct.MaxRows, ColCode, ColDesc)
        MainClass.ProtectCell(sprdPerks, 1, sprdPerks.MaxRows, ColCode, ColDesc)
    End Sub
    Private Sub ShowSalary(ByRef xCode As String)

        Dim RsADD As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim mTypeCode As Object
        Dim xSubkey As Integer
        Dim mEarnAmt As Object
        Dim mDeductAmt As Decimal
        Dim cntRow As Integer

        With sprdEarn
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = 1
                mTypeCode = Val(.Text)

                SqlStr = " SELECT * from PAY_CAND_SALARYDEF_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND REF_NO='" & xCode & "'" & vbCrLf & " AND ADD_DEDUCTCode=" & mTypeCode & " AND  " & vbCrLf & " SALARY_EFF_DATE=(SELECT MAX(SALARY_EFF_DATE) From PAY_CAND_SALARYDEF_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND REF_NO='" & xCode & "'" & vbCrLf & " AND SALARY_EFF_DATE<= TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsADD, ADODB.LockTypeEnum.adLockOptimistic)

                If Not RsADD.EOF Then
                    txtBSalary.Text = VB6.Format(RsADD.Fields("BASICSALARY").Value, "0.00")
                    txtForm1BSalary.Text = VB6.Format(RsADD.Fields("FORM1_BASICSALARY").Value, "0.00")
                    '                txtWEF.Text = Format(IIf(IsNull(RsADD!SALARY_EFF_DATE), "", RsADD!SALARY_EFF_DATE), "DD/MM/YYYY")
                    '                lblNextIncDue.Caption = Format(IIf(IsNull(RsADD!NEXT_INC_DATE), "", RsADD!NEXT_INC_DATE), "DD/MM/YYYY")

                    If RsADD.Fields("EMP_DESG_CODE").Value <> "" Then
                        If MainClass.ValidateWithMasterTable(Trim(RsADD.Fields("EMP_DESG_CODE").Value), "DESG_CODE", "DESG_DESC", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            cbodesignation.Text = MasterNo
                        End If
                    End If

                    .Row = cntRow
                    .Col = ColPer
                    .Text = CStr(IIf(IsDBNull(RsADD.Fields("PERCENTAGE").Value), "", RsADD.Fields("PERCENTAGE").Value))

                    .Col = ColAmt
                    .Text = CStr(IIf(IsDBNull(RsADD.Fields("Amount").Value), "", RsADD.Fields("Amount").Value))

                    .Col = ColForm1Amt
                    .Text = CStr(IIf(IsDBNull(RsADD.Fields("Form1_Amount").Value), "", RsADD.Fields("FORM1_Amount").Value))

                    txtBSalary.Enabled = IIf(chkIsJoined.CheckState = System.Windows.Forms.CheckState.Unchecked, True, False)
                    MainClass.ProtectCell(sprdEarn, 1, sprdEarn.MaxRows, ColCode, IIf(chkIsJoined.CheckState = System.Windows.Forms.CheckState.Unchecked, ColDesc, ColAmt))
                    MainClass.ProtectCell(sprdDeduct, 1, sprdDeduct.MaxRows, ColCode, IIf(chkIsJoined.CheckState = System.Windows.Forms.CheckState.Unchecked, ColDesc, ColAmt))
                Else
                    txtBSalary.Enabled = True
                    MainClass.ProtectCell(sprdEarn, 1, sprdEarn.MaxRows, ColCode, ColDesc)
                    MainClass.ProtectCell(sprdDeduct, 1, sprdDeduct.MaxRows, ColCode, ColDesc)
                End If
            Next
        End With

        cntRow = 1
        With sprdDeduct
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = 1
                mTypeCode = Val(.Text)

                If MainClass.ValidateWithMasterTable(mTypeCode, "Code", "TYPE", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    If MasterNo = ConLoan Then

                        'Call MonthLoan(xCode, xMonth, xYear, cntRow)
                        GoTo NextRow1
                    End If
                End If

                SqlStr = " SELECT * from PAY_CAND_SALARYDEF_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND REF_NO='" & xCode & "'" & vbCrLf & " AND ADD_DEDUCTCode=" & mTypeCode & " AND  " & vbCrLf & " SALARY_EFF_DATE=(SELECT MAX(SALARY_EFF_DATE) From PAY_CAND_SALARYDEF_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND REF_NO='" & xCode & "'" & vbCrLf & " AND SALARY_EFF_DATE<= TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsADD, ADODB.LockTypeEnum.adLockOptimistic)

                If Not RsADD.EOF Then
                    txtBSalary.Text = VB6.Format(RsADD.Fields("BASICSALARY").Value, "0.00")
                    txtForm1BSalary.Text = VB6.Format(RsADD.Fields("FORM1_BASICSALARY").Value, "0.00")
                    '                txtWEF.Text = Format(IIf(IsNull(RsADD!SALARY_EFF_DATE), "", RsADD!SALARY_EFF_DATE), "DD/MM/YYYY")
                    '                lblNextIncDue.Caption = Format(IIf(IsNull(RsADD!NEXT_INC_DATE), "", RsADD!NEXT_INC_DATE), "DD/MM/YYYY")

                    .Row = cntRow
                    .Col = ColPer
                    .Text = CStr(IIf(IsDBNull(RsADD.Fields("PERCENTAGE").Value), "", RsADD.Fields("PERCENTAGE").Value))

                    .Col = ColAmt
                    .Text = CStr(IIf(IsDBNull(RsADD.Fields("Amount").Value), "", RsADD.Fields("Amount").Value))

                    .Col = ColForm1Amt
                    .Text = CStr(IIf(IsDBNull(RsADD.Fields("Form1_Amount").Value), "", RsADD.Fields("FORM1_Amount").Value))


                    '
                    '                txtBSalary.Enabled = False
                    '                MainClass.ProtectCell sprdEarn, 1, sprdEarn.MaxRows, ColCode, ColAmt
                    '                MainClass.ProtectCell sprdDeduct, 1, sprdDeduct.MaxRows, ColCode, ColAmt

                    txtBSalary.Enabled = IIf(chkIsJoined.CheckState = System.Windows.Forms.CheckState.Unchecked, True, False)
                    MainClass.ProtectCell(sprdEarn, 1, sprdEarn.MaxRows, ColCode, IIf(chkIsJoined.CheckState = System.Windows.Forms.CheckState.Unchecked, ColDesc, ColAmt))
                    MainClass.ProtectCell(sprdDeduct, 1, sprdDeduct.MaxRows, ColCode, IIf(chkIsJoined.CheckState = System.Windows.Forms.CheckState.Unchecked, ColDesc, ColAmt))

                Else
                    txtBSalary.Enabled = True
                    MainClass.ProtectCell(sprdEarn, 1, sprdEarn.MaxRows, ColCode, ColDesc)
                    MainClass.ProtectCell(sprdDeduct, 1, sprdDeduct.MaxRows, ColCode, ColDesc)
                End If
NextRow1:
            Next
        End With

        cntRow = 1
        With sprdPerks
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = 1
                mTypeCode = Val(.Text)

                SqlStr = " SELECT * from PAY_CAND_SALARYDEF_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND REF_NO='" & xCode & "'" & vbCrLf & " AND ADD_DEDUCTCode=" & mTypeCode & " AND  " & vbCrLf & " SALARY_EFF_DATE=(SELECT MAX(SALARY_EFF_DATE) From PAY_CAND_SALARYDEF_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND REF_NO='" & xCode & "'" & vbCrLf & " AND SALARY_EFF_DATE<= TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsADD, ADODB.LockTypeEnum.adLockOptimistic)

                If Not RsADD.EOF Then
                    '                txtBSalary.Text = Format(RsADD!BASICSALARY, "0.00")
                    '                txtWEF.Text = Format(IIf(IsNull(RsADD!SALARY_EFF_DATE), "", RsADD!SALARY_EFF_DATE), "DD/MM/YYYY")
                    '                lblNextIncDue.Caption = Format(IIf(IsNull(RsADD!NEXT_INC_DATE), "", RsADD!NEXT_INC_DATE), "DD/MM/YYYY")

                    .Row = cntRow
                    .Col = ColPer
                    .Text = CStr(IIf(IsDbNull(RsADD.Fields("PERCENTAGE").Value), "", RsADD.Fields("PERCENTAGE").Value))

                    .Col = ColAmt
                    .Text = CStr(IIf(IsDBNull(RsADD.Fields("Amount").Value), "", RsADD.Fields("Amount").Value))

                    .Col = ColForm1Amt
                    .Text = CStr(IIf(IsDBNull(RsADD.Fields("Form1_Amount").Value), "", RsADD.Fields("FORM1_Amount").Value))


                    '                txtBSalary.Enabled = False
                    '                MainClass.ProtectCell sprdEarn, 1, sprdEarn.MaxRows, ColCode, ColAmt
                    '                MainClass.ProtectCell sprdDeduct, 1, sprdDeduct.MaxRows, ColCode, ColAmt
                    '                MainClass.ProtectCell sprdPerks, 1, sprdPerks.MaxRows, ColCode, ColAmt

                    txtBSalary.Enabled = IIf(chkIsJoined.CheckState = System.Windows.Forms.CheckState.Unchecked, True, False)
                    MainClass.ProtectCell(sprdEarn, 1, sprdEarn.MaxRows, ColCode, IIf(chkIsJoined.CheckState = System.Windows.Forms.CheckState.Unchecked, ColDesc, ColAmt))
                    MainClass.ProtectCell(sprdDeduct, 1, sprdDeduct.MaxRows, ColCode, IIf(chkIsJoined.CheckState = System.Windows.Forms.CheckState.Unchecked, ColDesc, ColAmt))
                    MainClass.ProtectCell(sprdPerks, 1, sprdPerks.MaxRows, ColCode, IIf(chkIsJoined.CheckState = System.Windows.Forms.CheckState.Unchecked, ColDesc, ColAmt))
                Else
                    '                txtBSalary.Enabled = True
                    MainClass.ProtectCell(sprdEarn, 1, sprdEarn.MaxRows, ColCode, ColDesc)
                    MainClass.ProtectCell(sprdDeduct, 1, sprdDeduct.MaxRows, ColCode, ColDesc)
                    MainClass.ProtectCell(sprdPerks, 1, sprdPerks.MaxRows, ColCode, ColDesc)
                End If
            Next
        End With
    End Sub
    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1
        With sprdEarn
            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight * 1.5)

            .Col = ColCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColCode, 5)
            .ColHidden = True

            .Col = ColDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColDesc, 19)

            .Col = ColPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColPer, 8)

            .Col = ColAmt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColAmt, 12)
        End With

        MainClass.ProtectCell(sprdEarn, 1, sprdEarn.MaxRows, ColCode, ColPer)
        MainClass.SetSpreadColor(sprdEarn, mRow)

        With sprdDeduct

            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight * 1.5)

            .Col = ColCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColCode, 5)
            .ColHidden = True

            .Col = ColDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColDesc, 19)

            .Col = ColPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColPer, 8)

            .Col = ColAmt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColAmt, 12)
        End With

        MainClass.ProtectCell(sprdDeduct, 1, sprdDeduct.MaxRows, ColCode, ColPer)
        MainClass.SetSpreadColor(sprdDeduct, mRow)

        With sprdPerks

            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight * 1.5)

            .Col = ColCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColCode, 5)
            .ColHidden = True

            .Col = ColDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColDesc, 19)

            .Col = ColPer
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColPer, 8)

            .Col = ColAmt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColAmt, 12)
        End With

        FormatSprdSpouse(-1)

        MainClass.ProtectCell(sprdPerks, 1, sprdPerks.MaxRows, ColCode, ColPer)
        MainClass.SetSpreadColor(sprdPerks, mRow)

        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub

    Private Function UpdateSalaryDef(ByRef xCode As String, ByRef xWEF As String, ByRef xSalary As Double, ByRef xForm1Salary As Double, ByRef mDesgCode As String) As Boolean
        On Error GoTo UpdateSalaryDefErr
        Dim SqlStr As String = ""
        Dim xTypeCode As Object
        Dim cntRow As Integer
        Dim xAmount As Object
        Dim xPer As Decimal
        Dim mNextIncDue As String
        Dim xForm1Amount As Double
        Dim mForm1Salary As Double



        '    If IsDate(lblNextIncDue.Caption) = True Then
        '        mNextIncDue = Format(lblNextIncDue.Caption, "DD/MM/YYYY")
        '    Else
        '        mNextIncDue = Format(txtDOP.Text, "DD/MM/YYYY")
        '    End If

        mNextIncDue = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 12, CDate(xWEF)))
        SqlStr = " DELETE FROM PAY_CAND_SALARYDEF_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND REF_NO='" & xCode & "'" ''& vbCrLf |            & " AND SALARY_EFF_DATE=TO_DATE('" & VB6.Format(xWEF, "DD-MMM-YYYY") & "')"


        PubDBCn.Execute(SqlStr)

        mForm1Salary = xForm1Salary

        If Val(xForm1Salary) = 0 Then
            xForm1Salary = xSalary
        End If

        SqlStr = ""

        With sprdEarn
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColCode
                If Trim(.Text) = "" Then GoTo NextEarnRow
                xTypeCode = Val(.Text)

                .Col = ColPer
                xPer = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColAmt
                xAmount = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColForm1Amt
                xForm1Amount = IIf(IsNumeric(.Text), .Text, 0)

                If mForm1Salary = 0 Then
                    xForm1Amount = xAmount
                End If

                SqlStr = " Insert Into PAY_CAND_SALARYDEF_MST (COMPANY_CODE, FYEAR, " & vbCrLf _
                    & " REF_NO, SALARY_EFF_DATE, BASICSALARY, " & vbCrLf _
                    & " ADD_DEDUCTCODE, PERCENTAGE, AMOUNT, " & vbCrLf _
                    & " SALARY_APP_DATE, PREVIOUS_BASICSALARY,PREVIOUS_AMOUNT," & vbCrLf _
                    & " ARREAR_DATE,TOT_ARR_MONTH,IS_ARREAR,EMP_DESG_CODE, " & vbCrLf _
                    & " ADDUSER, ADDDATE,NEXT_INC_DATE," & vbCrLf _
                    & " FORM1_BASICSALARY,FORM1_AMOUNT,PREVIOUS_FORM1_BASICSALARY, PREVIOUS_FORM1_AMOUNT" & vbCrLf _
                    & " ) VALUES " & vbCrLf _
                    & " (" & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                    & " " & RsCompany.Fields("FYEAR").Value & ",'" & xCode & "', TO_DATE('" & VB6.Format(xWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " " & xSalary & "," & xTypeCode & "," & xPer & "," & xAmount & "," & vbCrLf _
                    & " TO_DATE('" & VB6.Format(xWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & xSalary & "," & vbCrLf _
                    & " " & xAmount & ",'',0,'N','" & mDesgCode & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                    & " TO_DATE('" & VB6.Format(mNextIncDue, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                    & " " & xForm1Salary & ", " & xForm1Amount & ", " & xForm1Salary & ", " & xForm1Amount & ")"


                PubDBCn.Execute(SqlStr)
NextEarnRow:
            Next
        End With

        cntRow = 1
        With sprdDeduct
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColCode
                If Trim(.Text) = "" Then GoTo NextDeductRow
                xTypeCode = Val(.Text)

                .Col = ColPer
                xPer = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColAmt
                xAmount = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColForm1Amt
                xForm1Amount = IIf(IsNumeric(.Text), .Text, 0)

                If mForm1Salary = 0 Then
                    xForm1Amount = xAmount
                End If

                SqlStr = " Insert Into PAY_CAND_SALARYDEF_MST (COMPANY_CODE, FYEAR, " & vbCrLf _
                    & " REF_NO, SALARY_EFF_DATE, BASICSALARY, " & vbCrLf _
                    & " ADD_DEDUCTCODE, PERCENTAGE, AMOUNT, " & vbCrLf _
                    & " SALARY_APP_DATE, PREVIOUS_BASICSALARY,PREVIOUS_AMOUNT," & vbCrLf _
                    & " ARREAR_DATE,TOT_ARR_MONTH,IS_ARREAR,EMP_DESG_CODE, " & vbCrLf _
                    & " ADDUSER, ADDDATE,NEXT_INC_DATE ," & vbCrLf _
                    & " FORM1_BASICSALARY,FORM1_AMOUNT,PREVIOUS_FORM1_BASICSALARY, PREVIOUS_FORM1_AMOUNT" & vbCrLf _
                    & " ) VALUES " & vbCrLf _
                    & " (" & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                    & " " & RsCompany.Fields("FYEAR").Value & ",'" & xCode & "', TO_DATE('" & VB6.Format(xWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " " & xSalary & "," & xTypeCode & "," & xPer & "," & xAmount & "," & vbCrLf _
                    & " TO_DATE('" & VB6.Format(xWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & xSalary & "," & vbCrLf _
                    & " " & xAmount & ",'',0,'N','" & mDesgCode & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                    & " TO_DATE('" & VB6.Format(mNextIncDue, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                    & " " & xForm1Salary & ", " & xForm1Amount & ", " & xForm1Salary & ", " & xForm1Amount & ")"

                PubDBCn.Execute(SqlStr)
NextDeductRow:
            Next
        End With

        cntRow = 1
        With sprdPerks
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColCode
                If Trim(.Text) = "" Then GoTo NextPerksRow
                xTypeCode = Val(.Text)

                .Col = ColPer
                xPer = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColAmt
                xAmount = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColForm1Amt
                xForm1Amount = IIf(IsNumeric(.Text), .Text, 0)

                If mForm1Salary = 0 Then
                    xForm1Amount = xAmount

                End If
                SqlStr = " Insert Into PAY_CAND_SALARYDEF_MST (COMPANY_CODE, FYEAR, " & vbCrLf _
                    & " REF_NO, SALARY_EFF_DATE, BASICSALARY, " & vbCrLf _
                    & " ADD_DEDUCTCODE, PERCENTAGE, AMOUNT, " & vbCrLf _
                    & " SALARY_APP_DATE, PREVIOUS_BASICSALARY,PREVIOUS_AMOUNT," & vbCrLf _
                    & " ARREAR_DATE,TOT_ARR_MONTH,IS_ARREAR,EMP_DESG_CODE, " & vbCrLf _
                    & " ADDUSER, ADDDATE,NEXT_INC_DATE ," & vbCrLf _
                    & " FORM1_BASICSALARY,FORM1_AMOUNT,PREVIOUS_FORM1_BASICSALARY, PREVIOUS_FORM1_AMOUNT" & vbCrLf _
                    & " ) VALUES " & vbCrLf _
                    & " (" & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                    & " " & RsCompany.Fields("FYEAR").Value & ",'" & xCode & "', TO_DATE('" & VB6.Format(xWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " " & xSalary & "," & xTypeCode & "," & xPer & "," & xAmount & "," & vbCrLf _
                    & " TO_DATE('" & VB6.Format(xWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & xSalary & "," & vbCrLf _
                    & " " & xAmount & ",'',0,'N','" & mDesgCode & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                    & " TO_DATE('" & VB6.Format(mNextIncDue, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                    & " " & xForm1Salary & ", " & xForm1Amount & ", " & xForm1Salary & ", " & xForm1Amount & ")"

                PubDBCn.Execute(SqlStr)
NextPerksRow:
            Next
        End With

        UpdateSalaryDef = True
        Exit Function
UpdateSalaryDefErr:
        MsgBox(Err.Description)
        UpdateSalaryDef = False
        '    Resume
    End Function
    Private Sub CalcGrossSalary()

        Dim mSalary As Double
        Dim mEarn As Double
        Dim mDeduct As Double
        Dim mPerks As Double
        Dim cntRow As Integer

        Dim mForm1Salary As Double
        Dim mForm1Earn As Double
        Dim mForm1Deduct As Double

        Dim mForm1Perks As Double

        mSalary = Val(txtBSalary.Text)
        mForm1Salary = Val(txtForm1BSalary.Text)

        With sprdEarn
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColAmt
                mEarn = mEarn + Val(.Text)

                .Col = ColForm1Amt
                mForm1Earn = mForm1Earn + Val(.Text)
            Next
        End With

        cntRow = 1
        With sprdDeduct
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColAmt
                mDeduct = mDeduct + Val(.Text)

                .Col = ColForm1Amt
                mForm1Deduct = mForm1Deduct + Val(.Text)

            Next
        End With

        cntRow = 1
        With sprdPerks
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColAmt
                mPerks = mPerks + Val(.Text)

                .Col = ColForm1Amt
                mForm1Perks = mForm1Perks + Val(.Text)
            Next
        End With

        'txtGSalary.Text = Format((Val(mSalary) + Val(mEarn)), "0.00")
        txtGSalary.Text = MainClass.FormatRupees(Val(CStr(mSalary)) + Val(CStr(mEarn)))
        txtDeduction.Text = MainClass.FormatRupees(Val(CStr(mDeduct)))
        txtNetSalary.Text = MainClass.FormatRupees(Val(CStr(mSalary)) + Val(CStr(mEarn)) - Val(CStr(mDeduct)))
        txtCTC.Text = MainClass.FormatRupees(Val(CStr(mSalary)) + Val(CStr(mEarn)) + Val(CStr(mPerks)))

        txtForm1GSalary.Text = MainClass.FormatRupees(Val(CStr(mForm1Salary)) + Val(CStr(mForm1Earn)))
        'txtDeduction.Text = MainClass.FormatRupees(Val(CStr(mDeduct)))
        txtForm1NetSalary.Text = MainClass.FormatRupees(Val(CStr(mForm1Salary)) + Val(CStr(mForm1Earn)) - Val(CStr(mDeduct)))
        txtForm1CTC.Text = MainClass.FormatRupees(Val(CStr(mForm1Salary)) + Val(CStr(mForm1Earn)) + Val(CStr(mForm1Perks)))

    End Sub
    Private Sub CalcAddDeduct()
        Dim cntRow As Integer
        Dim xPer As Double

        With sprdEarn
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColPer
                xPer = Val(.Text)
                If xPer <> 0 Then
                    .Col = ColAmt
                    .Text = CStr(Val(txtBSalary.Text) * Val(CStr(xPer)) / 100)
                End If
            Next
        End With
    End Sub

    Private Function CalcBasicPFSalary(ByRef mType As Integer) As Double
        Dim cntRow As Integer
        Dim mCode As Integer
        Dim mPFCeiling As String
        CalcBasicPFSalary = IIf(IsNumeric(txtBSalary.Text), txtBSalary.Text, 0)
        With sprdEarn
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColCode
                mCode = CInt(.Text)
                If mType = ConPF Or mType = ConVPFAllw Or mType = ConEmployerPF Then
                    If MainClass.ValidateWithMasterTable(mCode, "Code", "IncludedPF", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        If MasterNo = "Y" Then
                            .Col = ColAmt
                            CalcBasicPFSalary = CalcBasicPFSalary + IIf(IsNumeric(.Text), .Text, 0)
                        End If
                    End If
                ElseIf mType = ConESI Then
                    If MainClass.ValidateWithMasterTable(mCode, "Code", "IncludedESI", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        If MasterNo = "Y" Then
                            .Col = ColAmt
                            CalcBasicPFSalary = CalcBasicPFSalary + IIf(IsNumeric(.Text), .Text, 0)
                        End If
                    End If
                End If
            Next
        End With

        If mType = ConPF Or mType = ConVPFAllw Or mType = ConEmployerPF Then
            If CheckPFCeilingOn(txtDOJ.Text) = "C" Then
                mPFCeiling = CheckPFCeiling(txtDOJ.Text)
            Else
                mPFCeiling = CalcBasicPFSalary
            End If

            CalcBasicPFSalary = IIf(CalcBasicPFSalary >= mPFCeiling, mPFCeiling, CalcBasicPFSalary)
        End If
    End Function

    Public Sub CalcPFESI()
        Dim xPer As Double
        Dim mcntRow As Integer
        Dim mCode As Integer
        Dim mType As Integer
        For mcntRow = 1 To sprdDeduct.MaxRows
            sprdDeduct.Row = mcntRow

            sprdDeduct.Col = ColCode
            If sprdDeduct.Text = "" Then Exit Sub
            mCode = CInt(sprdDeduct.Text)
            If MainClass.ValidateWithMasterTable(mCode, "Code", "Type", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mType = MasterNo
            End If
            sprdDeduct.Col = ColPer
            xPer = IIf(IsNumeric(sprdDeduct.Text), sprdDeduct.Text, 0)

            sprdDeduct.Col = ColAmt
            If xPer <> 0 Then
                sprdDeduct.Text = CStr(xPer * CalcBasicPFSalary(mType) / 100)
            End If
        Next
    End Sub

    Private Function CheckSalary(ByRef xCode As String) As Boolean

        On Error GoTo ErrCheckSalary
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        SqlStr = " SELECT * FROM PAY_CAND_SALARYDEF_MST WHERE " & vbCrLf & " COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " REF_NO = '" & xCode & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            CheckSalary = True
        Else
            CheckSalary = False
        End If
        Exit Function
ErrCheckSalary:
        CheckSalary = True
    End Function

    Private Sub txtState_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtState.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtState.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub SearchCCenter()
        On Error GoTo ErrPart
        Dim mDeptCode As String

        If Trim(cboDept.Text) = "" Then
            MsgInformation("Please Select Dept. First.")
            cboDept.Focus()
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable((cboDept.Text), "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDeptCode = MasterNo
        Else
            MsgInformation("Invalid Department Code. Cannot Save")
            cboDept.Focus()
            Exit Sub
        End If

        SqlStr = " SELECT IH.CC_DESC,IH.CC_CODE, ID.DEPT_CODE " & vbCrLf & " FROM FIN_CCENTER_HDR IH, FIN_CCENTER_DET ID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=ID.COMPANY_CODE AND IH.CC_CODE=ID.CC_CODE" & vbCrLf & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(mDeptCode) & "'"


        '    If MainClass.SearchGridMaster(txtCostCenter.Text, "FIN_CCENTER_HDR", "CC_DESC", "CC_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        If MainClass.SearchGridMasterBySQL2((txtCostCenter.Text), SqlStr) = True Then
            txtCostCenter.Text = AcName
            txtCostCenter_Validating(txtCostCenter, New System.ComponentModel.CancelEventArgs(False))
            If txtCostCenter.Enabled = True Then txtCostCenter.Focus()
        End If

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
End Class
