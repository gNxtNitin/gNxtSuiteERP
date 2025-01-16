Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility

Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinDataSource
Imports System.Data.OleDb

Friend Class frmEmployee
    Inherits System.Windows.Forms.Form
    Dim RsEmp As ADODB.Recordset = Nothing

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
    Private Const ColDeductOn As Short = 3
    Private Const ColPer As Short = 4
    Private Const ColAmt As Short = 5
    Private Const ColForm1Amt As Short = 6
    Private Const ColChk As Short = 7

    Private Const ColSpouseName As Short = 1
    Private Const ColSpouseRel As Short = 2
    Private Const ColSpouseGender As Short = 3
    Private Const ColBloodGroup As Short = 4
    Private Const ColSpouseDOB As Short = 5

    Private Const ColAssetDesc As Short = 1
    Private Const ColAssetMake As Short = 2
    Private Const ColAssetPrice As Short = 3
    Private Const ColAssetDOP As Short = 4
    Private Const ColAssetDOI As Short = 5
    Private Const ColAssetRemarks As Short = 6


    Private Const ColOpening As Short = 3
    Private Const ColTotEntitle As Short = 4
    Private Function UpdateEmpPhotoOld(ByRef mCode As String) As Boolean
        'On Error GoTo ErrPart
        'Dim mFileName As String
        ''Dim mLoadFile As String
        'Dim RsEmpPhoto As ADODB.Recordset
        'Dim cnnEmp As ADODB.Connection
        '
        '
        '
        '    Dim smEmp As ADODB.Stream
        '
        '
        '
        '    Set smEmp = New ADODB.Stream
        '    Set RsEmpPhoto = New ADODB.Recordset
        '    Set cnnEmp = New ADODB.Connection
        '    smEmp.Charset = "ascii"
        '
        '    smEmp.Type = adTypeBinary
        '    smEmp.Open
        '    mFileName = cdgPhoto.FileName            ''lblPhotoFileName.Caption
        '    smEmp.LoadFromFile mFileName
        '    smEmp.Position = 0
        '
        '
        '    SqlStr = "DRIVER={Microsoft ODBC for ORACLE};" & _
        ''                     "UID=" & DBConUID & ";PWD=" & DBConPWD & "@" & DBConSERVICENAME
        '
        '    cnnEmp.Open SqlStr
        '
        '    SqlStr = " SELECT COMPANY_CODE,EMP_CODE,EMP_PHOTO FROM PAY_EMPPHOTO_MST " & vbCrLf _
        ''            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        '
        ''    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsEmpPhoto, adLockReadOnly
        '    With RsEmpPhoto
        '      .CursorType = adOpenStatic
        '      .LockType = adLockOptimistic
        '      .Open SqlStr, cnnEmp
        '    End With
        '
        '    SqlStr = " DELETE FROM PAY_EMPPHOTO_MST " & vbCrLf _
        ''            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''            & " AND EMP_CODE='" & mCode & "'"
        '    PubDBCn.Execute SqlStr
        '
        '
        ''    SqlStr = " INSERT INTO PAY_EMPPHOTO_MST ( " & vbCrLf _
        '            & " COMPANY_CODE, EMP_CODE, EMP_PHOTO ) " & vbCrLf _
        '            & " VALUES ( " & vbCrLf _
        '            & " " & RsCompany.Fields("COMPANY_CODE").Value & ", '" & mCode & "', '" & lblPhotoFileName.Caption & "' )"
        '
        '    If smEmp.Size > 0 Then
        ''        SqlStr = " INSERT INTO PAY_EMPPHOTO_MST ( " & vbCrLf _
        '                & " COMPANY_CODE, EMP_CODE, EMP_PHOTO ) " & vbCrLf _
        '                & " VALUES ( " & vbCrLf _
        '                & " " & RsCompany.Fields("COMPANY_CODE").Value & ", '" & mCode & "', '" & mLoadFile & "'))"
        ''
        ''        PubDBCn.Execute SqlStr
        '        RsEmpPhoto.AddNew
        '        RsEmpPhoto("COMPANY_CODE") = RsCompany.Fields("COMPANY_CODE").Value
        '        RsEmpPhoto("EMP_CODE") = mCode
        '        RsEmpPhoto("EMP_PHOTO") = smEmp.Read
        '        'Update the data
        '        RsEmp.Update
        '
        '    End If


        UpdateEmpPhotoOld = True
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateEmpPhotoOld = False
    End Function
    Private Function UpdateEmpPhoto(ByRef mCode As String) As Boolean
        On Error GoTo ErrPart
        Dim mFilename As String
        Dim mFromPathName As String
        Dim mToPathName As String
        Dim mTempFileName As String
        Dim mExtName As String

        mFromPathName = lblPhotoFileName.Text

        If mFromPathName = "" Then UpdateEmpPhoto = True : Exit Function
        '    mTempFileName = mFromPathName
        '    Do While InStr(1, mTempFileName, "\") > 0
        '        mTempFileName = Mid(mTempFileName, InStr(1, mTempFileName, "\") + 1)
        '    Loop
        '
        '    mFileName = mTempFileName

        mTempFileName = mFromPathName
        Do While InStr(1, mTempFileName, ".") > 0
            mTempFileName = Mid(mTempFileName, InStr(1, mTempFileName, ".") + 1)
        Loop
        mExtName = mTempFileName

        mFilename = VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00") & VB6.Format(txtEmpNo.Text, "000000") & "." & mExtName

        mToPathName = My.Application.Info.DirectoryPath & "\EmpPhoto\" & mFilename

        ''EmpPhoto

        If CopyFile(mFromPathName, mToPathName, False) Then

        End If

        SqlStr = " DELETE FROM PAY_EMPPHOTO_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & mCode & "'"
        PubDBCn.Execute(SqlStr)


        SqlStr = " INSERT INTO PAY_EMPPHOTO_MST ( " & vbCrLf _
            & " COMPANY_CODE, EMP_CODE, DESCRIPTION ) " & vbCrLf _
            & " VALUES ( " & vbCrLf _
            & " " & RsCompany.Fields("COMPANY_CODE").Value & ", '" & mCode & "', '" & mFilename & "' )"

        PubDBCn.Execute(SqlStr)


        UpdateEmpPhoto = True
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateEmpPhoto = False
    End Function

    Private Function ShowEmpPhotoOld(ByRef mCode As String) As Boolean
        'On Error GoTo ErrPart
        'Dim RsTemp As ADODB.Recordset = Nothing
        'Dim mFileName As String
        'Dim diskFile As String
        'diskFile = App.path & "\temp\emp.bmp"
        '
        '    SqlStr = " SELECT EMP_PHOTO FROM PAY_EMPPHOTO_MST " & vbCrLf _
        ''            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''            & " AND EMP_CODE='" & mCode & "'"
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly
        '
        '    If RsTemp.EOF = False Then
        '        mFileName = IIf(IsNull(RsTemp!EMP_PHOTO), "", RsTemp!EMP_PHOTO)
        '        ImagePhoto.Picture = LoadPicture(mFileName)
        '    End If
        '
        '
        '
        '    'Create an instance of the stream object
        '    Dim smEmp As ADODB.Stream
        '    Set smEmp = New ADODB.Stream
        '
        '    'set the type to binary to load the image as a binary stream
        '    smEmp.Type = adTypeBinary
        '    smEmp.Open
        '
        '    'Load the binary image data from the DB into the stream object
        '    smEmp.Write RsTemp.Fields("EMP_PHOTO").Value
        '
        '    'Check the size of the ado stream to make sure there is data
        '    If smEmp.Size > 0 Then
        '        'Write the content of the stream object to a file
        '        'The file will br created if doesn't exists. Otherwise over writes the existing file
        '        smEmp.SaveToFile diskFile, adSaveCreateOverWrite
        '
        '        'Load the temp Picture into the Image control
        '        ImagePhoto.Picture = LoadPicture(App.path & "\temp\emp.bmp")
        '    Else
        '        MsgBox "Error reading the Photo"
        '    End If
        '
        '    'Close and destroy the stream object
        '    smEmp.Close
        '    Set smEmp = Nothing


        ShowEmpPhotoOld = True
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ShowEmpPhotoOld = False
    End Function
    Private Function ShowEmpPhoto(ByRef mCode As String) As Boolean

        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mFilename As String
        'SqlStr = " SELECT *  FROM PAY_EMPPHOTO_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & mCode & "'"
        'MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)

        'If RsTemp.EOF = False Then
        mFilename = My.Application.Info.DirectoryPath & "\EmpPhoto\" & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00") & VB6.Format(txtEmpNo.Text, "000000") & ".JPeG" ''& Right(IIf(IsNull(RsTemp!Description), "", RsTemp!Description), 3)

        ''mFilename = Format(RsCompany.Fields("COMPANY_CODE").Value, "00") & vb6.Format(txtEmpNo.Text, "000000") & "." & mExtName
        If mFilename <> "" Then
            If Not FILEExists(mFilename) Then
            Else
                ImagePhoto.Image = System.Drawing.Image.FromFile(mFilename)
            End If

        End If
        lblPhotoFileName.Text = mFilename
        'Else
        '    lblPhotoFileName.Text = ""
        'End If
        ShowEmpPhoto = True
        Exit Function
ErrPart:
        ''Resume
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
        MainClass.ButtonStatus(Me, XRIGHT, RsEmp, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub txtAddEmpCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAddEmpCode.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtAddEmpCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAddEmpCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtAddEmpCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub Clear1()

        txtEmpNo.Text = ""
        txtName.Text = ""
        txtFName.Text = ""
        txtBloodGroup.Text = ""
        txtHODName.Text = ""
        txtDOB.Text = ""
        txtBSalary.Text = ""
        txtGSalary.Text = ""
        txtDeduction.Text = ""
        txtNetSalary.Text = ""
        txtCTC.Text = ""
        txtAddEmpCode.Text = ""
        chkWFH.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkHRHOD.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkDeptHOD.CheckState = System.Windows.Forms.CheckState.Unchecked

        txtForm1BSalary.Text = ""
        txtForm1GSalary.Text = ""
        txtForm1NetSalary.Text = ""
        txtForm1CTC.Text = ""

        txtQualification.Text = ""
        txtLastCompany.Text = ""
        txtExperience.Text = ""
        txtBankName.Text = ""
        txtBankAcno.Text = ""
        txtIFSCCode.Text = ""
        txtDOJ.Text = ""
        txtBonusDOJ.Text = ""
        txtBonusDOJ.Visible = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 105, True, False)
        Label90.Visible = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 105, True, False)
        txtGroupDOJ.Text = ""
        txtDOP.Text = ""
        txtDOL.Text = ""
        txtReasonForLeaving.Text = ""
        txtWorkingFrom.Text = ""
        txtWorkingTo.Text = ""
        txtWorkingHours.Text = 8
        txtOTRate.Text = "1"
        txtAddress.Text = ""
        txtCity.Text = ""
        txtPinCode.Text = ""
        txtState.Text = ""
        txtPhone.Text = ""

        txtPAddress.Text = ""
        txtPCity.Text = ""
        txtPPinCode.Text = ""
        txtPState.Text = ""
        txtPPhone.Text = ""
        chkPMetroCity.CheckState = System.Windows.Forms.CheckState.Unchecked

        txtEmail.Text = ""
        txtOffeMail.Text = ""

        txtPFNo.Text = ""
        txtUIDNo.Text = ""
        txtESINo.Text = ""
        txtDispensary.Text = ""
        txtPanNo.Text = ""
        txtLICID.Text = ""
        txtAdhaarNo.Text = ""
        txtMobileOff.Text = ""
        txtDOBActual.Text = ""
        txtDOM.Text = ""
        txtCostCenter.Text = ""
        txtLICAmount.Text = "0.00"
        txtDAAmount.Text = "0.00"
        txtBankLoan.Text = "0.00"
        txtITAmount.Text = "0.00"

        txtLTAAmount.Text = "0.00"
        txtBonusPer.Text = "0.00"

        txtWEF.Enabled = False
        txtEmpNo.Enabled = True
        cmdSearch.Enabled = True

        txtLoanAcName.Text = ""
        txtLoanAcNo.Text = ""

        txtImprestAcName.Text = ""
        txtWEF.Text = ""
        txtContractor.Text = ""

        cboType.SelectedIndex = -1
        cboCatgeory.SelectedIndex = -1
        cboOverTime.SelectedIndex = -1

        txtContractor.Enabled = False '' IIf(lblEmpType.Caption = "C", True, False)

        cboDivision.SelectedIndex = -1
        cboDivision.Enabled = True
        cboCorporate.SelectedIndex = 0

        cboShift.SelectedIndex = -1
        cboSex.SelectedIndex = -1
        cboTaxRegime.SelectedIndex = -1
        cboMStatus.SelectedIndex = -1
        cboESIApp.SelectedIndex = -1
        cboPFPension.SelectedIndex = -1
        cboDept.SelectedIndex = -1
        cbodesignation.SelectedIndex = -1
        cboPaymentMode.SelectedIndex = -1
        CboJoinDesignation.SelectedIndex = -1
        cboWeeklyOff.SelectedIndex = -1
        cboEmpCatType.SelectedIndex = IIf(lblEmpType.Text = "S", 0, 1)
        cboEmpCatType.Enabled = False

        chkStopSal.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkEL.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkEL.Enabled = IIf(RsCompany.Fields("COMPANY_CODE").Value = 16, True, False)

        chkStopSal.Enabled = IIf(PubSuperUser = "S", True, False)
        chkGroupInsurance.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkRGPAuthorization.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkMetroCity.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkBonusApp.CheckState = System.Windows.Forms.CheckState.Checked
        chkLEApp.CheckState = System.Windows.Forms.CheckState.Checked
        '    CDLPhoto.FileName = ""
        ImagePhoto.Image = Nothing 'CDLPhoto.FileName
        ImagePhoto.Image = Nothing
        lblPhotoFileName.Text = ""

        txtNextIncDueDate.Text = ""

        cboPcRateType.SelectedIndex = 0
        cboPcRateType.Enabled = True

        cboPFPension.Enabled = True

        optContBasic.Checked = True
        optContCeiling.Checked = False
        optContGross.Checked = False
        optContCeilingGross.Checked = False
        If lblEmpType.Text = "O" Then
            txtName.Enabled = False
            txtFName.Enabled = False
            txtBloodGroup.Enabled = False
            txtHODName.Enabled = False
            txtDOB.Enabled = False
            txtBSalary.Enabled = False
            txtGSalary.Enabled = False
            txtDeduction.Enabled = False
            txtNetSalary.Enabled = False
            txtCTC.Enabled = False
            txtAddEmpCode.Enabled = False

            txtForm1BSalary.Enabled = True
            txtForm1GSalary.Enabled = False
            txtForm1NetSalary.Enabled = False
            txtForm1CTC.Enabled = False

            txtQualification.Enabled = False
            txtLastCompany.Enabled = False
            txtExperience.Enabled = False
            txtBankName.Enabled = False
            txtBankAcno.Enabled = False
            txtIFSCCode.Enabled = False
            txtDOJ.Enabled = False
            txtBonusDOJ.Enabled = False
            txtGroupDOJ.Enabled = False
            txtDOP.Enabled = False
            txtDOL.Enabled = False
            txtReasonForLeaving.Enabled = False
            txtWorkingFrom.Enabled = False
            txtWorkingTo.Enabled = False
            txtWorkingHours.Enabled = True
            txtOTRate.Text = "1"
            txtAddress.Enabled = False
            txtCity.Enabled = False
            txtPinCode.Enabled = False
            txtState.Enabled = False
            txtPhone.Enabled = False

            txtPAddress.Enabled = False
            txtPCity.Enabled = False
            txtPPinCode.Enabled = False
            txtPState.Enabled = False
            txtPPhone.Enabled = False
            chkPMetroCity.Enabled = False

            txtEmail.Enabled = False
            txtOffeMail.Enabled = False

            txtPFNo.Enabled = False
            txtUIDNo.Enabled = False
            txtESINo.Enabled = False
            txtDispensary.Enabled = False
            txtPanNo.Enabled = False
            txtLICID.Enabled = False
            txtCostCenter.Enabled = False
            txtLICAmount.Enabled = False
            txtDAAmount.Enabled = False
            txtBankLoan.Enabled = False
            txtITAmount.Enabled = False

            txtAdhaarNo.Enabled = False
            txtMobileOff.Enabled = False
            txtDOBActual.Enabled = False
            txtDOM.Enabled = False

            txtLTAAmount.Enabled = False
            txtBonusPer.Enabled = False

            txtWEF.Enabled = False
            txtEmpNo.Enabled = True
            cmdSearch.Enabled = True

            txtLoanAcName.Enabled = False
            txtLoanAcNo.Enabled = False

            txtImprestAcName.Enabled = False
            txtWEF.Enabled = False
            txtContractor.Enabled = False

            cboType.Enabled = False
            cboCatgeory.Enabled = False
            cboOverTime.Enabled = False
            cboPcRateType.Enabled = False
            txtContractor.Enabled = False
            txtOTRate.Enabled = False
            cboDivision.Enabled = False
            cboCorporate.Enabled = False

            cboShift.Enabled = False
            cboSex.Enabled = False
            cboTaxRegime.Enabled = False
            cboMStatus.Enabled = False
            cboESIApp.Enabled = False
            cboPFPension.Enabled = False
            cboDept.Enabled = False
            cbodesignation.Enabled = False
            cboPaymentMode.Enabled = False
            CboJoinDesignation.Enabled = False
            cboWeeklyOff.Enabled = False
            cboEmpCatType.Enabled = False

            chkStopSal.Enabled = False
            chkEL.Enabled = False

            chkStopSal.Enabled = False
            chkGroupInsurance.Enabled = False
            chkRGPAuthorization.Enabled = False
            chkMetroCity.Enabled = False
            chkBonusApp.Enabled = False
            chkLEApp.Enabled = False

            txtNextIncDueDate.Enabled = False
        End If

        Label81.Visible = IIf(RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "N", False, True)
        txtForm1BSalary.Visible = IIf(RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "N", False, True)


        Label82.Visible = IIf(RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "N", False, True)
        txtForm1GSalary.Visible = IIf(RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "N", False, True)
        Label84.Visible = IIf(RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "N", False, True)
        txtForm1NetSalary.Visible = IIf(RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "N", False, True)
        Label83.Visible = IIf(RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "N", False, True)
        txtForm1CTC.Visible = IIf(RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "N", False, True)

        lblLeavesYear.Text = "Opening Leaves For Year : " & Year(RunDate)
        MainClass.ClearGrid(sprdEarn, -1)
        MainClass.ClearGrid(sprdDeduct, -1)
        MainClass.ClearGrid(sprdPerks, -1)
        MainClass.ClearGrid(sprdLeaves, -1)
        MainClass.ClearGrid(sprdSpouse, -1)
        MainClass.ClearGrid(sprdAssets, -1)


        Call AutoCompleteSearchSQL("SELECT CC_DESC FROM FIN_CCENTER_HDR IH, FIN_CCENTER_DET ID WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IH.COMPANY_CODE=ID.COMPANY_CODE AND IH.CC_CODE=ID.CC_CODE Order By 1", "CC_DESC", txtCostCenter)

        '' "


        txtRefNo.Text = ""
        txtRefNo.Enabled = True
        cmdSearchRef.Enabled = True
        'SSTab1.SelectedIndex = 0

        FillOpLeave()
        FillSalarySprd()

        SSTab1.SelectedIndex = 0
        MainClass.ButtonStatus(Me, XRIGHT, RsEmp, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub optContBasic_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optContBasic.CheckedChanged
        If eventSender.Checked Then

            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
            If FormActive = False Then Exit Sub
            Call CalcPFESI()
            Call CalcGrossSalary()
        End If
    End Sub
    Private Sub optContGross_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optContGross.CheckedChanged
        If eventSender.Checked Then

            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
            If FormActive = False Then Exit Sub
            Call CalcPFESI()
            Call CalcGrossSalary()
        End If
    End Sub
    Private Sub optContCeiling_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optContCeiling.CheckedChanged
        If eventSender.Checked Then

            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
            If FormActive = False Then Exit Sub
            Call CalcPFESI()
            Call CalcGrossSalary()
        End If
    End Sub
    Private Sub optContCeilingGross_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optContCeilingGross.CheckedChanged
        If eventSender.Checked Then

            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
            If FormActive = False Then Exit Sub
            Call CalcPFESI()
            Call CalcGrossSalary()
        End If
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
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        Dim mDeptCode As String = ""

        If Trim(cboDept.Text) = "" Then Exit Sub

        If MainClass.ValidateWithMasterTable((cboDept.Text), "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDeptCode = MasterNo
        End If
        Call AutoCompleteSearchSQL("SELECT CC_DESC FROM FIN_CCENTER_HDR IH, FIN_CCENTER_DET ID WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IH.COMPANY_CODE=ID.COMPANY_CODE AND IH.CC_CODE=ID.CC_CODE AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(mDeptCode) & "' Order By 1", "CC_DESC", txtCostCenter)

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

    Private Sub cboEmpCatType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboEmpCatType.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboEmpCatType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboEmpCatType.SelectedIndexChanged

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

    Private Sub cboOverTime_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboOverTime.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboOverTime_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboOverTime.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboPaymentMode_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboPaymentMode.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboPcRateType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboPcRateType.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboPcRateType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboPcRateType.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboPFPension_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboPFPension.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    'cboTaxRegime
    Private Sub cboTaxRegime_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboTaxRegime.SelectedIndexChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboSex_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboSex.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboShift_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShift.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboShift_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShift.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboType.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboType.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboWeeklyOff_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboWeeklyOff.SelectedIndexChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkBonusApp_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkBonusApp.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkWFH_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkWFH.CheckStateChanged, chkHRHOD.CheckStateChanged, chkDeptHOD.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkEL_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkEL.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkGroupInsurance_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkGroupInsurance.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub chkLEApp_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkLEApp.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkMetroCity_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkMetroCity.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkPMetroCity_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkPMetroCity.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkRGPAuthorization_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkRGPAuthorization.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPreview.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPrint(Crystal.DestinationConstants.crptToWindow)
        Me.Cursor = System.Windows.Forms.Cursors.Default
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

        If TempFillPrintDummyData(sprdEarn, 1, sprdEarn.MaxRows, 0, sprdEarn.MaxCols, "1", PubDBCn) = False Then GoTo ERR2
        If TempFillPrintDummyData(sprdDeduct, 1, sprdDeduct.MaxRows, 0, sprdDeduct.MaxCols, "3", PubDBCn) = False Then GoTo ERR2
        'If TempFillPrintDummyData(sprdPerks, 1, sprdPerks.MaxRows, 0, sprdPerks.MaxCols, "2", PubDBCn) = False Then GoTo ERR2

        PubDBCn.CommitTrans()

        frmPrintAppLtr.ShowDialog()

        If G_PrintAppLtr = False Then
            Exit Sub
        End If

        'Insert Data from Grid to PrintDummyData Table...


        'Select Record for print...

        SqlStr = ""

        SqlStr = " SELECT * FROM Temp_PrintDummyData PRINTDUMMYDATA " & vbCrLf _
            & " WHERE  UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf _
            & " ORDER BY FIELD10, SUBROW"

        mSubTitle = ""
        mTitle = ""

        If frmPrintAppLtr.OptPrint(0).Checked = True Then
            mRptFileName = "Appointment_Ltr.rpt"
        ElseIf frmPrintAppLtr.OptPrint(1).Checked = True Then
            mRptFileName = "IntentLetter.rpt"
        ElseIf frmPrintAppLtr.OptPrint(2).Checked = True Then
            mRptFileName = "SalaryStructure.rpt"
        ElseIf frmPrintAppLtr.OptPrint(3).Checked = True Then
            mRptFileName = "ConfirmationLetter.rpt"
        Else
            mRptFileName = "EmpJoiningKit.rpt"
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
                ElseIf FieldNum = ColAmt Or FieldNum = ColForm1Amt Then
                    SetData = SetData & ", " & "FIELD" & FieldCnt
                    GetData = GetData & ", " & "'" & VB6.Format(Val(GridName.Text) * IIf(mDefaultValue = "3", -1, 1), "0.00") & "'"
                Else
                    SetData = SetData & ", " & "FIELD" & FieldCnt
                    GetData = GetData & ", " & "'" & MainClass.AllowSingleQuote(GridName.Text) & "'"
                End If
                FieldCnt = FieldCnt + 1
            Next
            SqlStr = " INSERT INTO TEMP_PRINTDUMMYDATA (USERID, SUBROW, FIELD10, " & vbCrLf _
                & " " & SetData & ") " & vbCrLf & " VALUES (" & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & RowNum & ", '" & mDefaultValue & "'," & vbCrLf _
                & " " & GetData & ") "
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
        Dim mDOI As String
        Dim mUnit As String

        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)


        If MainClass.ValidateWithMasterTable((cbodesignation.Text), "DESG_DESC", "GRADE_CODE", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mGrade = MasterNo
        End If

        If MainClass.ValidateWithMasterTable((txtEmpNo.Text), "EMP_CODE", "EMP_DOI", "PAY_CANDIDATE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDOI = MasterNo
        End If

        'If RsCompany.Fields("COMPANY_CODE").Value = 1 Then
        '    mUnit = " - UNIT I"
        'Else
        mUnit = ""
        'End If

        MainClass.AssignCRptFormulas(Report1, "mEmpName='" & MainClass.AllowSingleQuote(txtName.Text) & "'")
        MainClass.AssignCRptFormulas(Report1, "mAddress='" & MainClass.AllowSingleQuote(txtAddress.Text) & "'")
        MainClass.AssignCRptFormulas(Report1, "mCity='" & MainClass.AllowSingleQuote(txtCity.Text) & "'")
        MainClass.AssignCRptFormulas(Report1, "mPinCode='" & MainClass.AllowSingleQuote(txtPinCode.Text) & "'")
        MainClass.AssignCRptFormulas(Report1, "mState='" & MainClass.AllowSingleQuote(txtState.Text) & "'")
        MainClass.AssignCRptFormulas(Report1, "mEmpDegn='" & MainClass.AllowSingleQuote(cbodesignation.Text) & "'")
        MainClass.AssignCRptFormulas(Report1, "Dept='" & MainClass.AllowSingleQuote(cboDept.Text) & "'")
        MainClass.AssignCRptFormulas(Report1, "Grade='" & MainClass.AllowSingleQuote(mGrade) & "'")
        MainClass.AssignCRptFormulas(Report1, "mDOI='" & VB6.Format(mDOI, "DD/MM/YYYY") & "'")
        MainClass.AssignCRptFormulas(Report1, "mDOJ='" & VB6.Format(txtDOJ.Text, "DD/MM/YYYY") & "'")
        MainClass.AssignCRptFormulas(Report1, "mUnit='" & mUnit & "'")
        MainClass.AssignCRptFormulas(Report1, "mConfirmationDate='" & VB6.Format(txtDOP.Text, "DD/MM/YYYY") & "'")
        MainClass.AssignCRptFormulas(Report1, "BasicSalary='" & Val(txtBSalary.Text) & "'")

        '    MainClass.AssignCRptFormulas Report1, "mGrossAmount='" & txtGSalary.Text & "'"

        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub


    Private Sub cmdSearchRef_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchRef.Click

        '    SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IS_JOINED='N' AND (DECODE(MD_APPROVAL,'Y',1,0)+DECODE(CFO_APPROVAL,'Y',1,0)+DECODE(CEO_APPROVAL,'Y',1,0)+DECODE(HR_APPROVAL,'Y',1,0))>=3"

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IS_JOINED='N' "



        If MainClass.SearchGridMaster((txtRefNo.Text), "PAY_CANDIDATE_MST", "EMP_NAME", "TO_CHAR(REF_NO,'000000')", , , SqlStr) = True Then
            txtRefNo.Text = VB6.Format(AcName1, "000000")
            txtRefNo_Validating(txtRefNo, New System.ComponentModel.CancelEventArgs(False))
            If txtRefNo.Enabled = True Then txtRefNo.Focus()
        End If

        Exit Sub

    End Sub

    Private Sub sprdAssets_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles sprdAssets.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub sprdAssets_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles sprdAssets.ClickEvent

        On Error GoTo ERR1
        Select Case eventArgs.Col
            Case 0
                If eventArgs.Row > 0 And sprdAssets.Enabled = True Then
                    MainClass.DeleteSprdRow(sprdAssets, eventArgs.Row, ColAssetDesc)
                    MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
                End If
        End Select
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub sprdAssets_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles sprdAssets.LeaveCell

        On Error GoTo ERR1

        If eventArgs.NewRow = -1 Then Exit Sub
        Select Case eventArgs.col

            Case ColAssetDesc

                sprdAssets.Row = eventArgs.row
                sprdAssets.Col = ColAssetDesc

                If Trim(sprdAssets.Text) <> "" Then
                    If sprdAssets.MaxRows = sprdAssets.ActiveRow Then
                        MainClass.AddBlankSprdRow(sprdAssets, ColAssetDesc, ConRowHeight)
                        FormatSprdAssets(-1)
                    End If
                End If

        End Select
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
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

    Private Sub txtHODName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtHODName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtHODName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtHODName.DoubleClick
        SearchHOD()
    End Sub
    Private Sub txtHODName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtHODName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            SearchHOD()
        End If
    End Sub
    Private Sub SearchHOD()


        If MainClass.SearchGridMaster((txtHODName.Text), "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtHODName.Text = AcName
        End If

        Exit Sub

    End Sub

    Private Sub txtHODName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtHODName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtHODName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtHODName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtHODName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim mHODName As String

        If Trim(txtHODName.Text) = "" Then GoTo EventExitSub
        mHODName = Trim(txtHODName.Text)

        If MainClass.ValidateWithMasterTable(mHODName, "EMP_NAME", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("HOD Name Does Not Exist In Master, Please select Correct Name.", MsgBoxStyle.Information)
            Cancel = True
        End If

        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
        '    Resume
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtIFSCCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtIFSCCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtIFSCCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtIFSCCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtIFSCCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNextIncDueDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNextIncDueDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNextIncDueDate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNextIncDueDate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtNextIncDueDate.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtNextIncDueDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtNextIncDueDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtNextIncDueDate.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtNextIncDueDate.Text) Then
            MsgInformation("Please enter the vaild date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPAddress_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPAddress.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPAddress_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPAddress.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPAddress.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtPCity_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPCity.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtPCity_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPCity.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPCity.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtPPhone_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPPhone.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPPhone_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPPhone.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPPhone.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtPPinCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPPinCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtPPinCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPPinCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPPinCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtPState_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPState.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtPState_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPState.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPState.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtRefNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRefNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim mRefNo As Double
        Dim RsCandidate As ADODB.Recordset
        Dim mEmpCode As String

        If Trim(txtRefNo.Text) = "" Then GoTo EventExitSub
        mRefNo = CDbl(VB6.Format(Val(txtRefNo.Text), "000000"))
        mEmpCode = VB6.Format(Val(txtEmpNo.Text), "000000")

        SqlStr = ""
        SqlStr = "SELECT * FROM  PAY_CANDIDATE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND REF_NO=" & Val(Trim(txtRefNo.Text)) & " AND IS_JOINED='N' "

        '            AND (DECODE(MD_APPROVAL,'Y',1,0)+DECODE(CFO_APPROVAL,'Y',1,0)+DECODE(CEO_APPROVAL,'Y',1,0)+DECODE(HR_APPROVAL,'Y',1,0))>=3"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCandidate, ADODB.LockTypeEnum.adLockOptimistic)


        If RsCandidate.EOF = False Then
            Clear1()
            txtRefNo.Text = VB6.Format(mRefNo, "000000")
            If Val(mEmpCode) <> 0 Then
                txtEmpNo.Text = VB6.Format(mEmpCode, "000000")
            End If
            Call ShowFromRef(RsCandidate)
        Else
            MsgBox("Ref No Does Not Exist In Master or not Approved", MsgBoxStyle.Information)
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
        '    Resume
EventExitSub:
        eventArgs.Cancel = Cancel
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

                '.Col = ColAmt
                'If xPer <> 0 Then
                '    .Text = CStr(xPer * Val(txtBSalary.Text) / 100)
                'End If

                '.Col = ColForm1Amt
                'If xPer <> 0 Then
                '    .Text = CStr(xPer * Val(txtBSalary.Text) / 100)
                'End If
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
        With sprdPerks
            sprdPerks_LeaveCell(sprdPerks, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub


    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick

        SqlStr = ""
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow

        SqlStr = "SELECT * FROM PAY_EMPLOYEE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote((SprdView.Text)) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmp, ADODB.LockTypeEnum.adLockOptimistic)
        If RsEmp.EOF = False Then
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

    Private Sub txtBankName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBankName.DoubleClick
        SearchBankMaster()
    End Sub

    Private Sub txtBankName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtBankName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            SearchBankMaster()
        End If
    End Sub

    Private Sub txtBankName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBankName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1


        If Trim(txtBankName.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtBankName.Text), "BANK_NAME", "BANK_NAME", "PAY_BANK_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Invalid Bank Name.", MsgBoxStyle.Information)
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
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

                '.Col = ColAmt
                'If xPer <> 0 Then
                '    .Text = CStr(xPer * Val(txtBSalary.Text) / 100)
                'End If

                '.Col = ColForm1Amt
                'If xPer <> 0 Then
                '    .Text = CStr(xPer * Val(txtBSalary.Text) / 100)
                'End If
            Next
        End With

        With sprdDeduct
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColPer
                xPer = IIf(IsNumeric(.Text), .Text, 0)

                '.Col = ColAmt
                'If xPer <> 0 Then
                '    .Text = CStr(xPer * Val(txtBSalary.Text) / 100)
                'End If

                '.Col = ColForm1Amt
                'If xPer <> 0 Then
                '    .Text = CStr(xPer * Val(txtBSalary.Text) / 100)
                'End If

            Next
        End With

        With sprdPerks
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColPer
                xPer = IIf(IsNumeric(.Text), .Text, 0)

                '.Col = ColAmt
                'If xPer <> 0 Then
                '    .Text = CStr(xPer * Val(txtBSalary.Text) / 100)
                'End If

                '.Col = ColForm1Amt
                'If xPer <> 0 Then
                '    .Text = CStr(xPer * Val(txtBSalary.Text) / 100)
                'End If

            Next
        End With

        CalcPFESI()
        CalcGrossSalary()

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


    Private Sub txtGroupDOJ_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGroupDOJ.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtGroupDOJ_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtGroupDOJ.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtGroupDOJ.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtGroupDOJ.Text) Then
            MsgInformation("Please enter the vaild date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtGroupDOJ_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtGroupDOJ.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtGroupDOJ.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtLoanAcName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLoanAcName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLoanAcName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLoanAcName.DoubleClick
        CallSearchAccount()
    End Sub


    Private Sub txtLoanAcName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLoanAcName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtLoanAcName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtLoanAcName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtLoanAcName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            CallSearchAccount()
        End If
    End Sub

    Private Sub txtLoanAcName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtLoanAcName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1

        If Trim(txtLoanAcName.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtLoanAcName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invalid Account Name")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtBankAcno_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBankAcno.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtBankAcno.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtBankLoan_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBankLoan.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBankLoan_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBankLoan.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtBankLoan_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBankLoan.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtBankLoan.Text = VB6.Format(txtBankLoan.Text, "0.00")
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtBankName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBankName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBankName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBankName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtBankName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtBonusPer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBonusPer.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBonusPer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBonusPer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtBonusPer_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBonusPer.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtBonusPer.Text = VB6.Format(txtBonusPer.Text, "0.00")
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

    Private Sub txtContractor_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtContractor.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtContractor_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtContractor.DoubleClick
        SearchContractor()
    End Sub


    Private Sub txtContractor_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtContractor.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtContractor.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtContractor_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtContractor.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            SearchContractor()
        End If
    End Sub

    Private Sub txtContractor_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtContractor.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1


        If Trim(txtContractor.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtContractor.Text), "CON_NAME", "CON_CODE", "PAY_CONTRACTOR_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Contractor Name Does Not Exist In Master" & vbCrLf & "Click Add To Add In Master", MsgBoxStyle.Information)
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDispensary_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDispensary.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDispensary.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDOJ_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDOJ.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDOJ.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    '
    Private Sub txtBonusDOJ_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBonusDOJ.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtBonusDOJ.Text)
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

    Private Sub txtImprestAcName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtImprestAcName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtITAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtITAmount.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtITAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtITAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtITAmount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtITAmount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtITAmount.Text = VB6.Format(txtITAmount.Text, "0.00")
        eventArgs.Cancel = Cancel
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

    Private Sub txtLICAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLICAmount.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLICAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLICAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtDAAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDAAmount.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDAAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDAAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtLICAmount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtLICAmount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtLICAmount.Text = VB6.Format(txtLICAmount.Text, "0.00")
        eventArgs.Cancel = Cancel
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

    Private Sub txtLoanAcNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLoanAcNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLoanAcNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLoanAcNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtLoanAcNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtLTAAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLTAAmount.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLTAAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLTAAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtLTAAmount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtLTAAmount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtLTAAmount.Text = VB6.Format(txtLTAAmount.Text, "0.00")
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtOffeMail_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOffeMail.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOTRate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOTRate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkStopSal_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkStopSal.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            txtEmpNo.Enabled = False
            cmdSearch.Enabled = False
            MainClass.ButtonStatus(Me, XRIGHT, RsEmp, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            txtEmpNo.Enabled = True
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

        cdgPhotoOpen.Filter = "(*.bmp)|*.bmp|(*.jpg)|*.jpg" ''|All Files|*.*

        ''"Report Files (*.xls)|*.xls|(*.html)|*.html|All Files|*.*"
        '' "(*.bmp;*.ico;*.gif;*.jpg)/*.bmp;*.ico;*.gif;*.jpg"


        '"Report Files (*.xls)|*.xls|(*.html)|*.html|All Files|*.*"
        cdgPhotoOpen.ShowDialog()

        'assign the image file name to the fileName variable
        mFilename = cdgPhotoOpen.FileName

        'if the file name is valid, load the image in the image control on the form
        If Not FILEExists(mFilename) Then
        Else
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
            txtForm1BSalary.Enabled = True

            txtNextIncDueDate.Enabled = True
            If txtEmpNo.Enabled = True Then txtEmpNo.Focus()
        Else
            ADDMode = False
            MODIFYMode = False
            If RsEmp.EOF = False Then RsEmp.MoveFirst()
            Show1()
        End If
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If txtName.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub
        If Not RsEmp.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                If Delete1 = False Then GoTo DelErrPart
                If RsEmp.EOF = True Then
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

        If lblEmpType.Text = "O" Then

        ElseIf lblEmpType.Text = "S" Then
            SqlStr = SqlStr & " AND EMP_CAT_TYPE='1'"
        Else
            SqlStr = SqlStr & " AND EMP_CAT_TYPE='2'"
        End If

        If MainClass.SearchGridMaster((txtEmpNo.Text), "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            txtEmpNo.Text = AcName1
            TxtEmpNo_Validating(TxtEmpNo, New System.ComponentModel.CancelEventArgs(False))
            If txtEmpNo.Enabled = True Then txtEmpNo.Focus()
        End If

        Exit Sub

    End Sub
    Private Sub SearchContractor()

        If MainClass.SearchGridMaster((txtContractor.Text), "PAY_CONTRACTOR_MST", "CON_NAME", , , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtContractor.Text = AcName1
            txtContractor_Validating(txtContractor, New System.ComponentModel.CancelEventArgs(False))
            If txtContractor.Enabled = True Then txtContractor.Focus()
        End If

        Exit Sub

    End Sub
    Private Sub frmEmployee_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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

                '.Col = ColAmt
                'If xPer <> 0 Then
                '    .Text = CStr(xPer * Val(txtBSalary.Text) / 100)
                'End If

                '.Col = ColForm1Amt
                'If xPer <> 0 Then
                '    .Text = CStr(xPer * Val(txtBSalary.Text) / 100)
                'End If

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

            'sprdEarn.Col = ColAmt
            'If xPer <> 0 Then
            '    sprdEarn.Text = CStr(xPer * Val(txtBSalary.Text) / 100)
            'End If
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
        With sprdEarn
            sprdEarn_LeaveCell(sprdEarn, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub


    Private Sub sprdLeaves_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles sprdLeaves.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
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
    Private Sub txtBankAcno_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBankAcno.TextChanged

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

    Private Sub txtRefNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRefNo.DoubleClick
        cmdSearchRef_Click(cmdSearchRef, New System.EventArgs())
    End Sub

    Private Sub txtRefNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtRefNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            cmdSearchRef_Click(cmdSearchRef, New System.EventArgs())
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
    '
    Private Sub txtBonusDOJ_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBonusDOJ.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtDOJ_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDOJ.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDOJ.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtDOJ.Text) Then
            MsgInformation("Please enter the vaild date.")
            Cancel = True
            GoTo EventExitSub
        End If
        If txtNextIncDueDate.Enabled = True Or ADDMode = True Then
            If Trim(txtNextIncDueDate.Text) = "" Then
                txtNextIncDueDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Year, 1, CDate(txtDOJ.Text)))
            End If
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    ''
    Private Sub txtBonusDOJ_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBonusDOJ.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtBonusDOJ.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtBonusDOJ.Text) Then
            MsgInformation("Please enter the vaild date.")
            Cancel = True
            GoTo EventExitSub
        End If
        'If txtNextIncDueDate.Enabled = True Or ADDMode = True Then
        '    If Trim(txtNextIncDueDate.Text) = "" Then
        '        txtNextIncDueDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Year, 1, CDate(txtDOJ.Text)))
        '    End If
        'End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtDOL_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDOL.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDOL_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDOL.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDOL.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtDOL.Text) Then
            MsgInformation("Please enter the vaild date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDOP_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDOP.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDOP_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDOP.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDOP.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtDOP.Text) Then
            MsgInformation("Please enter the vaild date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtEmpNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtEmpNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEmpNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtEmpNo.Text)
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
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Public Sub frmEmployee_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        SqlStr = ""
        If FormActive = True Then Exit Sub
        MainClass.UOpenRecordSet("SELECT * FROM PAY_EMPLOYEE_MST WHERE 1<>1", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmp, ADODB.LockTypeEnum.adLockOptimistic)
        AssignGrid(False)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        settextlength()
        FillComboMst()
        'Clear1()
        FormActive = True
        If CmdAdd.Enabled = True Then cmdAdd_Click(CmdAdd, New System.EventArgs())

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '    Resume
    End Sub
    Private Sub frmEmployee_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)
        Me.Left = VB6.TwipsToPixelsX(20)
        Me.Top = VB6.TwipsToPixelsY(20)

        FormatSprd(-1)
        '    FillComboMst
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
        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet

        oledbCnn = New OleDbConnection(StrConn)

        oledbCnn.Open()

        SqlStr = "Select DISTINCT NAME, CODE, MACHINE_NAME, MACHINE_MODEL, MACHINE_MAKE, DEPT_CODE  " & vbCrLf _
                 & " FROM PAY_MACHINE_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        oledbAdapter = New OleDbDataAdapter(SqlStr, oledbCnn)
        oledbAdapter.Fill(ds)

        ' Set the data source and data member to bind the grid.
        cboMachineName.DataSource = ds
        cboMachineName.DataMember = ""
        cboMachineName.DisplayMember = "NAME"
        cboMachineName.ValueMember = "CODE"

        cboMachineName.Appearance.FontData.SizeInPoints = 8.5
        cboMachineName.DisplayLayout.Bands(0).Columns(0).Header.Caption = "Machine Name"
        cboMachineName.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Machine Code"
        cboMachineName.DisplayLayout.Bands(0).Columns(0).Width = 300
        cboMachineName.DisplayLayout.Bands(0).Columns(1).Width = 35
        cboMachineName.DisplayLayout.Bands(0).Columns(2).Width = 100
        cboMachineName.DisplayLayout.Bands(0).Columns(3).Width = 100
        cboMachineName.DisplayLayout.Bands(0).Columns(4).Width = 100
        cboMachineName.DisplayLayout.Bands(0).Columns(5).Width = 100

        cboMachineName.DisplayLayout.Bands(0).Columns(1).Hidden = True

        cboMachineName.DisplayLayout.Appearance.FontData.SizeInPoints = 8.5
        cboMachineName.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown ''List       '' Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown

        oledbAdapter.Dispose()
        oledbCnn.Close()

        cboSex.Items.Clear()
        cboSex.Items.Add("Male")
        cboSex.Items.Add("Female")

        cboTaxRegime.Items.Clear()
        cboTaxRegime.Items.Add("Old")
        cboTaxRegime.Items.Add("New")

        cboMStatus.Items.Clear()
        cboMStatus.Items.Add("Married")
        cboMStatus.Items.Add("Unmarried")

        '************cboPaymentMode Should Be filled in following series****
        cboPaymentMode.Items.Clear()
        cboPaymentMode.Items.Insert(0, "Cash")
        cboPaymentMode.Items.Insert(1, "Cheque")
        cboPaymentMode.Items.Insert(2, "DD")
        cboPaymentMode.Items.Insert(3, "Bank Transfer")
        If Trim(cboPaymentMode.Text) = "" Then cboPaymentMode.SelectedIndex = 0

        cboWeeklyOff.Items.Clear()
        cboWeeklyOff.Items.Add("MONDAY")
        cboWeeklyOff.Items.Add("TUESDAY")
        cboWeeklyOff.Items.Add("WEDNESSDAY")
        cboWeeklyOff.Items.Add("THURSDAY")
        cboWeeklyOff.Items.Add("FRIDAY")
        cboWeeklyOff.Items.Add("SATURDAY")
        cboWeeklyOff.Items.Add("SUNDAY")

        MainClass.FillCombo(cboDept, "PAY_DEPT_MST", "DEPT_DESC", , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
        '    MainClass.FillCombo cboMajorDept, "PAY_DEPT_MST", "DEPT_DESC", , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.FillCombo(cbodesignation, "PAY_DESG_MST", "DESG_DESC", , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
        MainClass.FillCombo(CboJoinDesignation, "PAY_DESG_MST", "DESG_DESC", , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")

        cboESIApp.Items.Clear()
        cboESIApp.Items.Add("Yes")
        cboESIApp.Items.Add("No")
        cboESIApp.SelectedIndex = 1

        cboPFPension.Items.Clear()
        cboPFPension.Items.Add("Yes")
        cboPFPension.Items.Add("No")
        cboPFPension.SelectedIndex = 1

        cboEmpCatType.Items.Clear()
        cboEmpCatType.Items.Add("1 : Staff")
        cboEmpCatType.Items.Add("2 : Workers")
        cboEmpCatType.SelectedIndex = -1

        SqlStr = "Select CATEGORY_DESC FROM PAY_CATEGORY_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If lblEmpType.Text = "O" Then

        Else
            SqlStr = SqlStr & vbCrLf & " AND CATEGORY_TYPE='" & lblEmpType.Text & "' "
        End If
        SqlStr = SqlStr & vbCrLf & " Order by CATEGORY_DESC"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockOptimistic)

        cboCatgeory.Items.Clear()
        If RS.EOF = False Then
            Do While Not RS.EOF
                cboCatgeory.Items.Add(RS.Fields("CATEGORY_DESC").Value)
                RS.MoveNext()
            Loop
        End If
        cboCatgeory.SelectedIndex = -1

        '    cboCatgeory.Clear
        '    cboCatgeory.AddItem "General Staff"
        '    cboCatgeory.AddItem "Production Staff"
        '    cboCatgeory.AddItem "Export Staff"
        '    cboCatgeory.AddItem "Regular Worker"
        '    cboCatgeory.AddItem "Staff R & D"
        ''    cboCatgeory.AddItem "Contratcor Staff"
        '    cboCatgeory.AddItem "Director"
        '    cboCatgeory.AddItem "Trainee Staff"

        cboType.Items.Clear()
        cboType.Items.Add("Permanent") 'Genernal Duty for Contractor staff
        cboType.Items.Add("Casual") 'Pc. Rate for Contractor Staff
        cboType.Items.Add("Trainee")
        cboType.Items.Add("Workers")
        cboType.Items.Add("")
        cboType.SelectedIndex = 0

        'cboShift.Items.Clear()
        'cboShift.Items.Add("General")
        'cboShift.Items.Add("A-Shift")
        'cboShift.Items.Add("B-Shift")
        'cboShift.Items.Add("C-Shift")
        'cboShift.SelectedIndex = 0

        SqlStr = "Select SHIFT_CODE FROM PAY_SHIFT_MST" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockOptimistic)

        cboShift.Items.Clear()
        If RS.EOF = False Then
            Do While Not RS.EOF
                cboShift.Items.Add(RS.Fields("SHIFT_CODE").Value)
                RS.MoveNext()
            Loop
            cboShift.SelectedIndex = 0
        Else
            cboShift.SelectedIndex = -1
        End If



        cboPcRateType.Items.Clear()
        cboPcRateType.Items.Add("GENERAL")
        cboPcRateType.Items.Add("PC Rate")
        'cboPcRateType.Items.Add("OLD")
        'cboPcRateType.Items.Add("1. OTHER")
        'cboPcRateType.Items.Add("2. OTHER II")
        'cboPcRateType.Items.Add("3. OTHER III")
        cboPcRateType.SelectedIndex = 0

        cboCorporate.Items.Clear()
        cboCorporate.Items.Add("No")
        cboCorporate.Items.Add("Yes")
        cboCorporate.SelectedIndex = 0

        cboOverTime.Items.Clear()
        cboOverTime.Items.Add("0 - NO")
        cboOverTime.Items.Add("1 - YES BASIC")
        cboOverTime.Items.Add("2 - SNACKS")
        cboOverTime.Items.Add("3 - YES GROSS")
        cboOverTime.SelectedIndex = -1

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
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmEmployee_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        '    'PvtDBCn.Cancel
        '    'PvtDBCn.Close
        RsEmp = Nothing
        '    'Set PvtDBCn = Nothing
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim mCategoryName As String
        Dim mCostCenter As String
        Dim mDivisionCode As Double
        Dim mDivisionDesc As String
        Dim mHODName As String
        Dim mHODCode As String
        Dim mValue As String
        Dim EmpPFCont As String

        Shw = True
        With RsEmp
            If Not RsEmp.EOF Then

                txtEmpNo.Text = IIf(IsDbNull(.Fields("EMP_CODE").Value), "", .Fields("EMP_CODE").Value)
                txtName.Text = IIf(IsDbNull(.Fields("EMP_NAME").Value), "", .Fields("EMP_NAME").Value)
                txtAddEmpCode.Text = IIf(IsDBNull(.Fields("ADD_EMP_CODE").Value), "", .Fields("ADD_EMP_CODE").Value)

                mHODCode = IIf(IsDbNull(.Fields("EMP_HOD_CODE").Value), "", .Fields("EMP_HOD_CODE").Value)
                If mHODCode <> "" Then
                    If MainClass.ValidateWithMasterTable(mHODCode, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mHODName = MasterNo
                        txtHODName.Text = Trim(mHODName)
                    End If
                End If

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
                txtBankName.Text = IIf(IsDbNull(.Fields("EMP_BANK_NAME").Value), "", .Fields("EMP_BANK_NAME").Value)
                txtBankAcno.Text = IIf(IsDbNull(.Fields("EMP_BANK_NO").Value), "", .Fields("EMP_BANK_NO").Value)
                txtIFSCCode.Text = IIf(IsDbNull(.Fields("EMPBANK_IFSC").Value), "", .Fields("EMPBANK_IFSC").Value)
                txtDOJ.Text = VB6.Format(IIf(IsDBNull(.Fields("EMP_DOJ").Value), "", .Fields("EMP_DOJ").Value), "DD/MM/YYYY")
                txtBonusDOJ.Text = VB6.Format(IIf(IsDBNull(.Fields("EMP_DOJ_BONUS").Value), "", .Fields("EMP_DOJ_BONUS").Value), "DD/MM/YYYY")
                txtGroupDOJ.Text = VB6.Format(IIf(IsDbNull(.Fields("EMP_GROUP_DOJ").Value), "", .Fields("EMP_GROUP_DOJ").Value), "DD/MM/YYYY")
                txtDOP.Text = VB6.Format(IIf(IsDbNull(.Fields("EMP_DOC").Value), "", .Fields("EMP_DOC").Value), "DD/MM/YYYY")
                txtDOL.Text = VB6.Format(IIf(IsDbNull(.Fields("EMP_LEAVE_DATE").Value), "", .Fields("EMP_LEAVE_DATE").Value), "DD/MM/YYYY")
                txtReasonForLeaving.Text = IIf(IsDbNull(.Fields("EMP_LEAVE_REASON").Value), "", .Fields("EMP_LEAVE_REASON").Value)
                txtWorkingFrom.Text = IIf(IsDbNull(.Fields("WORKINGTIMEFROM").Value), "", .Fields("WORKINGTIMEFROM").Value)
                txtWorkingTo.Text = IIf(IsDBNull(.Fields("WORKINGTIMETO").Value), "", .Fields("WORKINGTIMETO").Value)
                txtWorkingHours.Text = IIf(IsDBNull(.Fields("WORKING_HOURS").Value), "", .Fields("WORKING_HOURS").Value)


                txtOTRate.Text = IIf(IsDbNull(.Fields("EMP_OT_RATE").Value), "", .Fields("EMP_OT_RATE").Value)
                txtAddress.Text = IIf(IsDbNull(.Fields("EMP_ADDR").Value), "", .Fields("EMP_ADDR").Value)
                txtCity.Text = IIf(IsDbNull(.Fields("EMP_CITY").Value), "", .Fields("EMP_CITY").Value)
                txtPinCode.Text = IIf(IsDbNull(.Fields("EMP_PIN").Value), "", .Fields("EMP_PIN").Value)
                txtState.Text = IIf(IsDbNull(.Fields("EMP_STATE").Value), "", .Fields("EMP_STATE").Value)
                txtPhone.Text = IIf(IsDbNull(.Fields("EMP_PHONE_NO").Value), "", .Fields("EMP_PHONE_NO").Value)

                txtPAddress.Text = IIf(IsDbNull(.Fields("EMP_PERMANENT_ADDR").Value), "", .Fields("EMP_PERMANENT_ADDR").Value)
                txtPCity.Text = IIf(IsDbNull(.Fields("EMP_PERMANENT_CITY").Value), "", .Fields("EMP_PERMANENT_CITY").Value)
                txtPPinCode.Text = IIf(IsDbNull(.Fields("EMP_PERMANENT_PIN").Value), "", .Fields("EMP_PERMANENT_PIN").Value)
                txtPState.Text = IIf(IsDbNull(.Fields("EMP_PERMANENT_STATE").Value), "", .Fields("EMP_PERMANENT_STATE").Value)
                txtPPhone.Text = IIf(IsDbNull(.Fields("EMP_PERMANENT_PHONE_NO").Value), "", .Fields("EMP_PERMANENT_PHONE_NO").Value)

                txtEmail.Text = IIf(IsDbNull(.Fields("EMP_EMAILID").Value), "", .Fields("EMP_EMAILID").Value)
                txtOffeMail.Text = IIf(IsDbNull(.Fields("EMP_EMAILID_OFF").Value), "", .Fields("EMP_EMAILID_OFF").Value)

                txtPFNo.Text = IIf(IsDbNull(.Fields("EMP_PF_ACNO").Value), "", .Fields("EMP_PF_ACNO").Value)
                txtUIDNo.Text = IIf(IsDbNull(.Fields("UID_NO").Value), "", .Fields("UID_NO").Value)
                txtESINo.Text = IIf(IsDbNull(.Fields("EMP_ESI_NO").Value), "", .Fields("EMP_ESI_NO").Value)
                txtDispensary.Text = IIf(IsDbNull(.Fields("ESI_DISPENSARY").Value), "", .Fields("ESI_DISPENSARY").Value)
                txtPanNo.Text = IIf(IsDbNull(.Fields("EMP_PANNO").Value), "", .Fields("EMP_PANNO").Value)
                txtLICID.Text = IIf(IsDbNull(.Fields("EMP_LICNO").Value), "", .Fields("EMP_LICNO").Value)

                txtAdhaarNo.Text = IIf(IsDbNull(.Fields("EMP_ADHAAR_NO").Value), "", .Fields("EMP_ADHAAR_NO").Value)
                txtMobileOff.Text = IIf(IsDbNull(.Fields("EMP_MOBILE_NO_OFF").Value), "", .Fields("EMP_MOBILE_NO_OFF").Value)
                txtDOBActual.Text = VB6.Format(IIf(IsDbNull(.Fields("EMP_DOB_ACTUAL").Value), IIf(IsDbNull(.Fields("EMP_DOB").Value), "", .Fields("EMP_DOB").Value), .Fields("EMP_DOB_ACTUAL").Value), "DD/MM/YYYY")
                txtDOM.Text = IIf(IsDbNull(.Fields("EMP_DOM").Value), "", .Fields("EMP_DOM").Value)

                cboMachineName.Value = IIf(IsDBNull(.Fields("MACHINE_CODE").Value), "", .Fields("MACHINE_CODE").Value)

                '            txtWEF.Text = Format(IIf(IsNull(!SALARY_EFF_DATE), "", !SALARY_EFF_DATE), "DD/MM/YYYY")

                txtLICAmount.Text = VB6.Format(IIf(IsDBNull(.Fields("LIC_DED").Value), "0", .Fields("LIC_DED").Value), "0.00")
                txtDAAmount.Text = VB6.Format(IIf(IsDBNull(.Fields("DA_AMOUNT").Value), "0", .Fields("DA_AMOUNT").Value), "0.00")
                txtBankLoan.Text = VB6.Format(IIf(IsDbNull(.Fields("BNKLOAN_DED").Value), "0", .Fields("BNKLOAN_DED").Value), "0.00")
                txtITAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("ITAX_DED").Value), "0", .Fields("ITAX_DED").Value), "0.00")

                txtLTAAmount.Text = VB6.Format(IIf(IsDbNull(.Fields("LTA_AMT").Value), "0", .Fields("LTA_AMT").Value), "0.00")
                txtBonusPer.Text = VB6.Format(IIf(IsDbNull(.Fields("BONUS_PER").Value), "0", .Fields("BONUS_PER").Value), "0.00")

                EmpPFCont = IIf(IsDBNull(RsEmp.Fields("EMP_CONT").Value), "B", RsEmp.Fields("EMP_CONT").Value)
                optContBasic.Checked = IIf(EmpPFCont = "B", True, False)
                optContCeiling.Checked = IIf(EmpPFCont = "C", True, False)
                optContGross.Checked = IIf(EmpPFCont = "G", True, False)
                optContCeilingGross.Checked = IIf(EmpPFCont = "E", True, False)

                If IIf(IsDbNull(.Fields("WEEKLYOFF").Value), "", .Fields("WEEKLYOFF").Value) <> "" Then
                    cboWeeklyOff.Text = .Fields("WEEKLYOFF").Value
                Else
                    cboWeeklyOff.SelectedIndex = -1
                End If

                If IIf(IsDBNull(.Fields("JOININGDESIGN").Value), "", .Fields("JOININGDESIGN").Value) <> "" Then
                    If MainClass.ValidateWithMasterTable(.Fields("JOININGDESIGN").Value, "DESG_CODE", "DESG_DESC", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        CboJoinDesignation.Text = MasterNo
                    End If
                Else
                    CboJoinDesignation.SelectedIndex = -1
                End If

                Call SetCboText(cboPaymentMode, Val(IIf(IsDbNull(.Fields("PAYMENTMODE").Value), -1, .Fields("PAYMENTMODE").Value)))

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
                cboTaxRegime.Text = IIf(.Fields("EMP_TAX_REGIME").Value = "O", "Old", "New")
                cboMStatus.Text = IIf(.Fields("EMP_MARITAL_STATUS").Value = "M", "Married", "Unmarried")

                If .Fields("EMP_RATE_TYPE").Value = "G" Then
                    cboPcRateType.Text = "GENERAL"
                ElseIf .Fields("EMP_RATE_TYPE").Value = "P" Then
                    cboPcRateType.Text = "Pc RATE"
                ElseIf .Fields("EMP_RATE_TYPE").Value = "N" Then
                    cboPcRateType.Text = "NEW"
                ElseIf .Fields("EMP_RATE_TYPE").Value = "O" Then
                    cboPcRateType.Text = "OLD"
                ElseIf .Fields("EMP_RATE_TYPE").Value = "1" Then
                    cboPcRateType.Text = "1. OTHER"
                ElseIf .Fields("EMP_RATE_TYPE").Value = "2" Then
                    cboPcRateType.Text = "2. OTHER II"
                ElseIf .Fields("EMP_RATE_TYPE").Value = "3" Then
                    cboPcRateType.Text = "3. OTHER III"
                End If

                If IIf(IsDbNull(.Fields("EMP_TYPE").Value), "", .Fields("EMP_TYPE").Value) = "P" Then
                    cboType.Text = "Permanent"
                ElseIf IIf(IsDbNull(.Fields("EMP_TYPE").Value), "", .Fields("EMP_TYPE").Value) = "T" Then
                    cboType.Text = "Trainee"
                ElseIf IIf(IsDbNull(.Fields("EMP_TYPE").Value), "", .Fields("EMP_TYPE").Value) = "C" Then
                    cboType.Text = "Casual"
                ElseIf IIf(IsDbNull(.Fields("EMP_TYPE").Value), "", .Fields("EMP_TYPE").Value) = "W" Then
                    cboType.Text = "Workers"
                End If

                cboCatgeory.Text = GetEmployeeCategoryName(.Fields("EMP_CATG").Value)

                '            If !EMP_CATG = "G" Then
                '                cboCatgeory.Text = "General Staff"
                '            ElseIf !EMP_CATG = "P" Then
                '                cboCatgeory.Text = "Production Staff"
                '            ElseIf !EMP_CATG = "E" Then
                '                cboCatgeory.Text = "Export Staff"
                '            ElseIf !EMP_CATG = "R" Then
                '                cboCatgeory.Text = "Regular Worker"
                '            ElseIf !EMP_CATG = "C" Then
                '                cboCatgeory.Text = "Contratcor Staff"
                '            ElseIf !EMP_CATG = "D" Then
                '                cboCatgeory.Text = "Director"
                '            ElseIf !EMP_CATG = "S" Then
                '                cboCatgeory.Text = "Staff R & D"
                '            ElseIf !EMP_CATG = "T" Then
                '                cboCatgeory.Text = "Trainee Staff"
                '            End If

                If .Fields("EMP_CAT_TYPE").Value = "1" Then
                    cboEmpCatType.SelectedIndex = 0
                ElseIf .Fields("EMP_CAT_TYPE").Value = "2" Then
                    cboEmpCatType.SelectedIndex = 1
                Else
                    cboEmpCatType.SelectedIndex = -1
                End If

                If IsDbNull(.Fields("SHIFT_CODE").Value) Then
                    cboShift.SelectedIndex = -1
                Else
                    cboShift.Text = IIf(IsDBNull(.Fields("SHIFT_CODE").Value), "G", .Fields("SHIFT_CODE").Value)
                    'If .Fields("SHIFT_CODE").Value = "G" Then
                    '    cboShift.Text = "General"
                    'ElseIf .Fields("SHIFT_CODE").Value = "A" Then
                    '    cboShift.Text = "A-Shift"
                    'ElseIf .Fields("SHIFT_CODE").Value = "B" Then
                    '    cboShift.Text = "B-Shift"
                    'ElseIf .Fields("SHIFT_CODE").Value = "C" Then
                    '    cboShift.Text = "C-Shift"
                    'End If
                End If

                If .Fields("EMP_ESI_FLAG").Value = "Y" Then
                    cboESIApp.SelectedIndex = 0
                Else
                    cboESIApp.SelectedIndex = 1
                End If

                If .Fields("PF_PENSION_APP").Value = "Y" Then
                    cboPFPension.SelectedIndex = 0
                Else
                    cboPFPension.SelectedIndex = 1
                End If

                If .Fields("IS_CORPORATE").Value = "N" Then
                    cboCorporate.SelectedIndex = 0
                Else
                    cboCorporate.SelectedIndex = 1
                End If

                mValue = IIf(IsDBNull(.Fields("ADV_ACCOUNT_CODE").Value), "", .Fields("ADV_ACCOUNT_CODE").Value)
                If mValue <> "" Then
                    If MainClass.ValidateWithMasterTable(.Fields("ADV_ACCOUNT_CODE").Value, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        txtLoanAcName.Text = MasterNo
                    End If
                End If

                txtLoanAcNo.Text = IIf(IsDbNull(.Fields("EMP_LOANNO").Value), "", .Fields("EMP_LOANNO").Value)

                mValue = IIf(IsDBNull(.Fields("IMPREST_ACCOUNT_CODE").Value), "", .Fields("IMPREST_ACCOUNT_CODE").Value)
                If mValue <> "" Then
                    If MainClass.ValidateWithMasterTable(.Fields("IMPREST_ACCOUNT_CODE").Value, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        txtImprestAcName.Text = MasterNo
                    End If
                End If

                mValue = IIf(IsDBNull(.Fields("CONTRACTOR_CODE").Value), "", .Fields("CONTRACTOR_CODE").Value)
                If mValue <> "" Then
                    If MainClass.ValidateWithMasterTable(.Fields("CONTRACTOR_CODE").Value, "CON_CODE", "CON_NAME", "PAY_CONTRACTOR_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        txtContractor.Text = MasterNo
                    End If
                End If

                If PubSuperUser = "S" Or PubSuperUser = "A" Then
                    cboPcRateType.Enabled = IIf(lblEmpType.Text = "O", False, True)
                Else
                    cboPcRateType.Enabled = False ''IIf(GetEmpSalaryMade(txtEmpNo.Text) = True, False, True)
                End If

                mDivisionCode = IIf(IsDbNull(.Fields("DIV_CODE").Value), -1, .Fields("DIV_CODE").Value)

                If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionDesc = Trim(MasterNo)
                    cboDivision.Text = mDivisionDesc
                End If

                chkStopSal.CheckState = IIf(.Fields("EMP_STOP_SALARY").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkGroupInsurance.CheckState = IIf(.Fields("EMP_GROUP_INSURANCE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkRGPAuthorization.CheckState = IIf(.Fields("RGP_AUTH").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkMetroCity.CheckState = IIf(.Fields("ISMETROCITY").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkPMetroCity.CheckState = IIf(.Fields("ISPERMANENT_METROCITY").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                chkBonusApp.CheckState = IIf(.Fields("IS_BONUS_PAYABLE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkLEApp.CheckState = IIf(.Fields("IS_LEAVE_ENCHASE_PAYABLE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkStopSal.Enabled = IIf(PubSuperUser = "S", True, IIf(chkStopSal.CheckState = System.Windows.Forms.CheckState.Checked, True, False))
                chkEL.CheckState = IIf(.Fields("IS_EL_CARRY").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                chkWFH.CheckState = IIf(.Fields("IS_WFH").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                chkHRHOD.CheckState = IIf(.Fields("IS_HR_HOD").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkDeptHOD.CheckState = IIf(.Fields("IS_DEPT_HOD").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                If .Fields("OVERTIME_APP").Value = "0" Then
                    cboOverTime.SelectedIndex = 0
                ElseIf .Fields("OVERTIME_APP").Value = "1" Then
                    cboOverTime.SelectedIndex = 1
                ElseIf .Fields("OVERTIME_APP").Value = "2" Then
                    cboOverTime.SelectedIndex = 2
                ElseIf .Fields("OVERTIME_APP").Value = "3" Then
                    cboOverTime.SelectedIndex = 3
                End If

            End If
        End With

        Shw = False
        ADDMode = False
        MODIFYMode = False
        If RsEmp.EOF = False Then
            xCode = RsEmp.Fields("EMP_CODE").Value
            '        txtEmpNo.Enabled = False
            '        cmdSearch.Enabled = False
            Call ShowSprdSpouse(RsEmp.Fields("EMP_CODE").Value)
            Call ShowSprdAssets(RsEmp.Fields("EMP_CODE").Value)

            Call ShowSalary(RsEmp.Fields("EMP_CODE").Value)
            Call ShowSprdOpLeave(RsEmp.Fields("EMP_CODE").Value)


            If Val(txtBSalary.Text) <> 0 Then
                CalcGrossSalary()
                If ShowEmpPhoto(RsEmp.Fields("EMP_CODE").Value) = False Then GoTo NextLine
NextLine:
            End If
        End If

        txtRefNo.Enabled = False
        cmdSearchRef.Enabled = False
        'cboPFPension.Enabled = False
        SSTab1.SelectedIndex = 0
        MainClass.ButtonStatus(Me, XRIGHT, RsEmp, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
        If Err.Number = 383 Then
            Resume Next
        End If
    End Sub
    Private Sub ShowFromRef(ByRef mRsCandidate As ADODB.Recordset)
        On Error GoTo ShowErrPart
        Dim mCategoryName As String
        Dim mCostCenter As String
        Dim mDivisionCode As Double
        Dim mDivisionDesc As String
        Dim mHODCode As String
        Dim mHODName As String

        With mRsCandidate
            If Not mRsCandidate.EOF Then

                txtName.Text = IIf(IsDbNull(.Fields("EMP_NAME").Value), "", .Fields("EMP_NAME").Value)
                txtFName.Text = IIf(IsDbNull(.Fields("EMP_FNAME").Value), "", .Fields("EMP_FNAME").Value)
                txtBloodGroup.Text = IIf(IsDbNull(.Fields("BLOOD_GROUP").Value), "", .Fields("BLOOD_GROUP").Value)

                '            mHODCode = IIf(IsNull(!EMP_HOD_CODE), "", !EMP_HOD_CODE)
                '            If mHODCode <> "" Then
                '                If MainClass.ValidateWithMasterTable(mHODCode, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '                    mHODName = MasterNo
                '                    txtHODName.Text = Trim(mHODName)
                '                End If
                '            End If

                txtDOB.Text = VB6.Format(IIf(IsDbNull(.Fields("EMP_DOB").Value), "", .Fields("EMP_DOB").Value), "DD/MM/YYYY")
                txtBSalary.Text = CStr(Val(IIf(IsDbNull(.Fields("BASIC_SALARY").Value), "", .Fields("BASIC_SALARY").Value)))
                txtGSalary.Text = CStr(Val(IIf(IsDbNull(.Fields("GROSS_SALARY").Value), "", .Fields("GROSS_SALARY").Value)))
                txtQualification.Text = IIf(IsDbNull(.Fields("EMP_QUALIFICATION").Value), "", .Fields("EMP_QUALIFICATION").Value)
                txtLastCompany.Text = IIf(IsDbNull(.Fields("EMP_LAST_COMPANY").Value), "", .Fields("EMP_LAST_COMPANY").Value)
                txtExperience.Text = IIf(IsDbNull(.Fields("EMP_TOTEXP").Value), "", .Fields("EMP_TOTEXP").Value)
                txtDOJ.Text = VB6.Format(IIf(IsDBNull(.Fields("EMP_DOJ").Value), "", .Fields("EMP_DOJ").Value), "DD/MM/YYYY")
                txtBonusDOJ.Text = VB6.Format(IIf(IsDBNull(.Fields("EMP_DOJ").Value), "", .Fields("EMP_DOJ").Value), "DD/MM/YYYY")
                txtAddress.Text = IIf(IsDbNull(.Fields("EMP_ADDR").Value), "", .Fields("EMP_ADDR").Value)
                txtCity.Text = IIf(IsDbNull(.Fields("EMP_CITY").Value), "", .Fields("EMP_CITY").Value)
                txtPinCode.Text = IIf(IsDbNull(.Fields("EMP_PIN").Value), "", .Fields("EMP_PIN").Value)
                txtState.Text = IIf(IsDbNull(.Fields("EMP_STATE").Value), "", .Fields("EMP_STATE").Value)
                txtPhone.Text = IIf(IsDbNull(.Fields("EMP_PHONE_NO").Value), "", .Fields("EMP_PHONE_NO").Value)
                txtEmail.Text = IIf(IsDbNull(.Fields("EMP_EMAILID").Value), "", .Fields("EMP_EMAILID").Value)
                txtOffeMail.Text = IIf(IsDbNull(.Fields("EMP_EMAILID_OFF").Value), "", .Fields("EMP_EMAILID_OFF").Value)
                '            txtSpouse.Text = IIf(IsNull(!EMP_SPOUSE_NAME), "", !EMP_SPOUSE_NAME)
                txtPFNo.Text = IIf(IsDbNull(.Fields("EMP_PF_ACNO").Value), "", .Fields("EMP_PF_ACNO").Value)
                'txtUIDNo.Text = IIf(IsNull(!UID_NO), "", !UID_NO)
                txtESINo.Text = IIf(IsDbNull(.Fields("EMP_ESI_NO").Value), "", .Fields("EMP_ESI_NO").Value)
                txtDispensary.Text = IIf(IsDbNull(.Fields("ESI_DISPENSARY").Value), "", .Fields("ESI_DISPENSARY").Value)
                txtPanNo.Text = IIf(IsDbNull(.Fields("EMP_PANNO").Value), "", .Fields("EMP_PANNO").Value)
                txtLICID.Text = IIf(IsDbNull(.Fields("EMP_LICNO").Value), "", .Fields("EMP_LICNO").Value)

                txtAdhaarNo.Text = IIf(IsDbNull(.Fields("EMP_ADHAAR_NO").Value), "", .Fields("EMP_ADHAAR_NO").Value)
                txtMobileOff.Text = IIf(IsDbNull(.Fields("EMP_MOBILE_NO_OFF").Value), "", .Fields("EMP_MOBILE_NO_OFF").Value)
                txtDOBActual.Text = VB6.Format(IIf(IsDbNull(.Fields("EMP_DOB_ACTUAL").Value), IIf(IsDbNull(.Fields("EMP_DOB").Value), "", .Fields("EMP_DOB").Value), .Fields("EMP_DOB_ACTUAL").Value), "DD/MM/YYYY")
                txtDOM.Text = IIf(IsDbNull(.Fields("EMP_DOM").Value), "", .Fields("EMP_DOM").Value)

                mDivisionCode = IIf(IsDbNull(.Fields("DIV_CODE").Value), -1, .Fields("DIV_CODE").Value)

                If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionDesc = Trim(MasterNo)
                    cboDivision.Text = mDivisionDesc
                End If

                If .Fields("JOININGDESIGN").Value <> "" Then
                    If MainClass.ValidateWithMasterTable(.Fields("JOININGDESIGN").Value, "DESG_CODE", "DESG_DESC", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        CboJoinDesignation.Text = MasterNo
                    End If
                Else
                    CboJoinDesignation.SelectedIndex = -1
                End If

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

                If .Fields("EMP_DESG_CODE").Value <> "" Then
                    If MainClass.ValidateWithMasterTable(Trim(.Fields("EMP_DESG_CODE").Value), "DESG_CODE", "DESG_DESC", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        cbodesignation.Text = MasterNo
                    End If
                End If

                cboSex.Text = IIf(.Fields("EMP_SEX").Value = "M", "Male", "Female")
                'cboTaxRegime.Text = IIf(.Fields("EMP_TAX_REGIME").Value = "O", "Old", "New")
                cboMStatus.Text = IIf(.Fields("EMP_MARITAL_STATUS").Value = "M", "Married", "Unmarried")

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

                chkMetroCity.CheckState = IIf(.Fields("ISMETROCITY").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkBonusApp.CheckState = IIf(.Fields("IS_BONUS_PAYABLE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkLEApp.CheckState = IIf(.Fields("IS_LEAVE_ENCHASE_PAYABLE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                If Not IsDbNull(.Fields("EMP_CATG").Value) Then
                    cboCatgeory.Text = GetEmployeeCategoryName(.Fields("EMP_CATG").Value)
                End If

                '            If ShowEmpPhoto(!REF_NO) = False Then GoTo ShowErrPart
                Call ShowSalaryFromRef(Val(.Fields("REF_NO").Value))
                Call ShowSprdSpouseFromRef(CStr(Val(.Fields("REF_NO").Value)))
                CalcGrossSalary()
            End If
        End With

        Exit Sub
ShowErrPart:
        '    Resume
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
            TxtEmpNo_Validating(TxtEmpNo, New System.ComponentModel.CancelEventArgs(False))
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
        Dim mTaxRegime As String
        Dim mCode As String
        Dim mEmpType As String
        Dim mDeptCode As String
        Dim mMajorDeptCode As String
        Dim mDesgCode As String
        Dim mMaritalStatus As String
        Dim mSex As String
        Dim mShiftCode As String
        Dim mSalaryType As String
        Dim mESIFlag As String
        Dim mPFPensionFlag As String
        Dim mCategory As String
        Dim mWeeklyOff As String
        Dim mJoiningDesc As String
        Dim mPaymentMode As String
        Dim mGroupInsurance As String
        Dim mRGPAuthorization As String
        Dim mStopSalary As String
        Dim mAccountAdvanceCode As String
        Dim mAccountImprestCode As String
        Dim mGrossSalary As Double
        Dim mMetroCity As String
        Dim mContractCode As Double
        Dim mCostCenterCode As String
        Dim mBonusApp As String
        Dim mLEApp As String
        Dim mEmpCatType As String
        Dim mDivisionCode As Double
        Dim mCorporate As String
        Dim mEL As String
        Dim mHODCode As String
        Dim mPMetroCity As String
        Dim mOverTimeApp As String
        Dim EmpPFCont As String
        Dim mWFH As String
        Dim mMachineCode As Double

        Dim mHRHOD As String
        Dim mDeptHOD As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        mHODCode = ""
        If Trim(txtHODName.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtHODName.Text, "EMP_NAME", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mHODCode = Trim(MasterNo)
            End If
        End If

        EmpPFCont = IIf(optContBasic.Checked = True, "B", IIf(optContGross.Checked = True, "G", IIf(optContCeiling.Checked = True, "C", "E")))
        mSex = IIf(cboSex.Text = "Male", "M", "F")

        mTaxRegime = IIf(cboTaxRegime.Text = "Old", "O", "N")
        mMaritalStatus = IIf(cboMStatus.Text = "Married", "M", "U")

        mMachineCode = IIf(Trim(cboMachineName.Text) = "", 0, cboMachineName.Value)

        mCategory = VB.Left(cboCatgeory.Text, 1)
        mEmpType = VB.Left(cboType.Text, 1)
        mSalaryType = mEmpType

        mOverTimeApp = VB.Left(cboOverTime.Text, 1)
        mPMetroCity = IIf(chkPMetroCity.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mPaymentMode = CStr(GetCboTextIndex(cboPaymentMode))

        If IsNumeric(txtGSalary.Text) Then
            mGrossSalary = CDbl(txtGSalary.Text)
        Else
            mGrossSalary = 0
        End If
        mStopSalary = IIf(chkStopSal.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mGroupInsurance = IIf(chkGroupInsurance.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mRGPAuthorization = IIf(chkRGPAuthorization.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mMetroCity = IIf(chkMetroCity.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mBonusApp = IIf(chkBonusApp.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mLEApp = IIf(chkLEApp.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mWFH = IIf(chkWFH.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mHRHOD = IIf(chkHRHOD.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mDeptHOD = IIf(chkDeptHOD.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")


        mEL = IIf(chkEL.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mShiftCode = VB.Left(cboShift.Text, 1)
        mCorporate = VB.Left(cboCorporate.Text, 1)
        mESIFlag = VB.Left(cboESIApp.Text, 1)
        mWeeklyOff = cboWeeklyOff.Text

        mPFPensionFlag = VB.Left(cboPFPension.Text, 1)

        If Trim(txtWEF.Text) = "" Then
            txtWEF.Text = VB6.Format(txtDOJ.Text, "DD/MM/YYYY")
        End If

        If Trim(txtGroupDOJ.Text) = "" Then
            txtGroupDOJ.Text = VB6.Format(txtDOJ.Text, "DD/MM/YYYY")
        End If

        If MainClass.ValidateWithMasterTable((cboDept.Text), "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDeptCode = MasterNo
        Else
            mDeptCode = CStr(-1)
        End If

        mEmpCatType = VB.Left(cboEmpCatType.Text, 1)

        If MainClass.ValidateWithMasterTable((txtCostCenter.Text), "CC_DESC", "CC_CODE", "FIN_CCENTER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCostCenterCode = MasterNo
        Else
            mCostCenterCode = CStr(-1)
        End If

        '    If MainClass.ValidateWithMasterTable(cboMajorDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mMajorDeptCode = MasterNo
        '    Else
        '        mMajorDeptCode = -1
        '    End If

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

        If MainClass.ValidateWithMasterTable((txtLoanAcName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mAccountAdvanceCode = MasterNo
        End If

        If MainClass.ValidateWithMasterTable((txtImprestAcName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mAccountImprestCode = MasterNo
        End If

        If MainClass.ValidateWithMasterTable((txtContractor.Text), "CON_NAME", "CON_CODE", "PAY_CONTRACTOR_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mContractCode = MasterNo
        End If


        If Trim(txtDOBActual.Text) = "" Then
            txtDOBActual.Text = txtDOB.Text
        End If

        mCode = txtEmpNo.Text
        SqlStr = ""

        If ADDMode = True Then

            SqlStr = "INSERT INTO PAY_EMPLOYEE_MST ( " & vbCrLf _
                & " COMPANY_CODE, EMP_TYPE, EMP_CODE,  " & vbCrLf _
                & " EMP_NAME, EMP_ADDR, EMP_CITY,  " & vbCrLf _
                & " EMP_STATE, EMP_PIN, EMP_PHONE_NO,  " & vbCrLf _
                & " EMP_MOBILE_NO, EMP_EMAILID, EMP_EMAILID_OFF, EMP_CONTACT_PERSON,  " & vbCrLf _
                & " EMP_DEPT_CODE, EMP_MARITAL_STATUS, EMP_SEX,  " & vbCrLf _
                & " EMP_DESG_CODE, EMP_LAST_COMPANY, EMP_QUALIFICATION,  " & vbCrLf _
                & " EMP_TOTEXP, EMP_DOB, EMP_DOJ,  " & vbCrLf _
                & " SHIFT_CODE, SALARY_TYPE, EMP_DOC,  " & vbCrLf _
                & " EMP_PF_ACNO, EMP_PF_DATE, EMP_LEAVE_DATE,  " & vbCrLf _
                & " EMP_LEAVE_REASON, EMP_BANK_NO, EMP_ESI_FLAG,  " & vbCrLf _
                & " EMP_PROH_EXT, COST_CENTER_CODE, EMP_CATG,  " & vbCrLf _
                & " GROSS_SALARY, BASIC_SALARY, HRA_ALW,  " & vbCrLf _
                & " HRA_ALW_PERCENT, CONV_ALW, CHILD_EDU_ALW,  " & vbCrLf _
                & " CHILD_EDU_ALW_PERCENT, OTHERS1_ALW, OTHERS2_ALW,  " & vbCrLf _
                & " OTHERS3_ALW, PF_DED, ESI_DED,  "

            SqlStr = SqlStr & vbCrLf & " ADV_LOAN_DED, BNKLOAN_DED, LIC_DED,  " & vbCrLf _
                & " ITAX_DED, OTHER_DED, CPF_PER,  " & vbCrLf _
                & " BONUS_PER, LTA_AMT, LTA_PER,  " & vbCrLf _
                & " IMPREST_DED, SALARY_EFF_DATE, CONV_ALW_PERCENT,  " & vbCrLf _
                & " EMP_FNAME, BLOOD_GROUP, EMP_BANK_NAME, WORKINGTIMEFROM,  " & vbCrLf _
                & " WORKINGTIMETO, EMP_OT_RATE, EMP_SPOUSE_NAME,  " & vbCrLf _
                & " EMP_ESI_NO, ESI_DISPENSARY, EMP_PANNO,  " & vbCrLf _
                & " EMP_LICNO, WEEKLYOFF, JOININGDESIGN,  " & vbCrLf _
                & " PAYMENTMODE, EMP_GROUP_INSURANCE, EMP_STOP_SALARY,  " & vbCrLf _
                & " ADV_ACCOUNT_CODE, IMPREST_ACCOUNT_CODE, " & vbCrLf _
                & " ADDUSER, ADDDATE,ISMETROCITY,RGP_AUTH," & vbCrLf _
                & " CONTRACTOR_CODE,IS_BONUS_PAYABLE, " & vbCrLf _
                & " IS_LEAVE_ENCHASE_PAYABLE, " & vbCrLf _
                & " EMP_LOANNO ,EMP_CAT_TYPE, EMP_GROUP_DOJ," & vbCrLf _
                & " EMP_RATE_TYPE,DIV_CODE,IS_CORPORATE,IS_EL_CARRY,EMP_HOD_CODE,"

            SqlStr = SqlStr & vbCrLf _
                & " EMP_PERMANENT_ADDR, EMP_PERMANENT_CITY, EMP_PERMANENT_PIN,  " & vbCrLf _
                & " EMP_PERMANENT_STATE, EMP_PERMANENT_PHONE_NO, ISPERMANENT_METROCITY,  " & vbCrLf _
                & " OVERTIME_APP,UID_NO,PF_PENSION_APP,  " & vbCrLf _
                & " EMP_MOBILE_NO_OFF, EMP_DOB_ACTUAL, EMP_DOM, EMP_ADHAAR_NO, EMPBANK_IFSC,WORKING_HOURS," & vbCrLf _
                & " EMP_CONT,ADD_EMP_CODE, IS_WFH, MACHINE_CODE, DA_AMOUNT,EMP_TAX_REGIME,EMP_DOJ_BONUS, IS_HR_HOD, IS_DEPT_HOD"

            SqlStr = SqlStr & vbCrLf & " )  VALUES ( "

            SqlStr = SqlStr & vbCrLf _
                & " " & RsCompany.Fields("COMPANY_CODE").Value & ",'" & mEmpType & "', '" & mCode & "',  " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((TxtName.Text)) & "', '" & MainClass.AllowSingleQuote((txtAddress.Text)) & "', '" & MainClass.AllowSingleQuote((txtCity.Text)) & "',  " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtState.Text)) & "', '" & MainClass.AllowSingleQuote((txtPinCode.Text)) & "', '" & MainClass.AllowSingleQuote((txtPhone.Text)) & "',  " & vbCrLf _
                & " '', '" & MainClass.AllowSingleQuote((txtEmail.Text)) & "', '" & MainClass.AllowSingleQuote((txtOffeMail.Text)) & "', '',  " & vbCrLf _
                & " '" & mDeptCode & "', '" & mMaritalStatus & "', '" & mSex & "',  " & vbCrLf _
                & " '" & mDesgCode & "', '" & MainClass.AllowSingleQuote((txtLastCompany.Text)) & "', '" & MainClass.AllowSingleQuote((txtQualification.Text)) & "',  " & vbCrLf _
                & " " & Val(txtExperience.Text) & ", TO_DATE('" & VB6.Format(Trim(txtDOB.Text), "DD-MMM-YYYY") & "','DD-MON-YYYY'), TO_DATE('" & VB6.Format(txtDOJ.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),  " & vbCrLf & " '" & mShiftCode & "', '" & mSalaryType & "', TO_DATE('" & VB6.Format(Trim(txtDOP.Text), "DD-MMM-YYYY") & "','DD-MON-YYYY'),  " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtPFNo.Text)) & "', '', TO_DATE('" & VB6.Format(Trim(txtDOL.Text), "DD-MMM-YYYY") & "','DD-MON-YYYY'),  " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtReasonForLeaving.Text)) & "', '" & MainClass.AllowSingleQuote((txtBankAcno.Text)) & "', '" & mESIFlag & "',  " & vbCrLf & " '', '" & mCostCenterCode & "', '" & mCategory & "',  " & vbCrLf & " " & mGrossSalary & ", " & Val(txtBSalary.Text) & ", 0,  " & vbCrLf & " 0, 0, 0,  " & vbCrLf & " 0, 0, 0,  " & vbCrLf & " 0, 0, 0,  "



            SqlStr = SqlStr & vbCrLf & " 0, " & Val(txtBankLoan.Text) & ", " & Val(txtLICAmount.Text) & ",  " & vbCrLf _
                & " " & Val(txtITAmount.Text) & ", 0, 0,  " & vbCrLf _
                & " " & Val(txtBonusPer.Text) & ", " & Val(txtLTAAmount.Text) & ", 0,  " & vbCrLf _
                & " 0, TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), 0,  " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtFName.Text)) & "', '" & MainClass.AllowSingleQuote((txtBloodGroup.Text)) & "', '" & MainClass.AllowSingleQuote((txtBankName.Text)) & "', '" & MainClass.AllowSingleQuote((txtWorkingFrom.Text)) & "',  " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtWorkingTo.Text)) & "', " & Val(txtOTRate.Text) & ", '',  " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtESINo.Text)) & "', '" & MainClass.AllowSingleQuote((txtDispensary.Text)) & "', '" & MainClass.AllowSingleQuote((txtPanNo.Text)) & "',  " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtLICID.Text)) & "', '" & mWeeklyOff & "', '" & mJoiningDesc & "',  " & vbCrLf & " '" & mPaymentMode & "', '" & mGroupInsurance & "', '" & mStopSalary & "',  " & vbCrLf & " '" & mAccountAdvanceCode & "', '" & mAccountImprestCode & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & mMetroCity & "','" & mRGPAuthorization & "'," & mContractCode & "," & vbCrLf & " '" & mBonusApp & "', '" & mLEApp & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtLoanAcNo.Text)) & "','" & mEmpCatType & "', TO_DATE('" & VB6.Format(txtGroupDOJ.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & VB.Left(cboPcRateType.Text, 1) & "'," & mDivisionCode & ",'" & mCorporate & "','" & mEL & "', '" & MainClass.AllowSingleQuote(mHODCode) & "',"

            ''" & MainClass.AllowSingleQuote(txtSpouse.Text) & "

            SqlStr = SqlStr & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtPAddress.Text) & "', '" & MainClass.AllowSingleQuote(txtPCity.Text) & "', '" & MainClass.AllowSingleQuote(txtPPinCode.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtPState.Text) & "',  '" & MainClass.AllowSingleQuote(txtPPhone.Text) & "', '" & mPMetroCity & "', " & vbCrLf & " '" & mOverTimeApp & "'," & Val(txtUIDNo.Text) & ",'" & mPFPensionFlag & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(txtMobileOff.Text) & "', TO_DATE('" & VB6.Format(txtDOBActual.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtDOM.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(txtAdhaarNo.Text) & "','" & MainClass.AllowSingleQuote(txtIFSCCode.Text) & "',"


            SqlStr = SqlStr & vbCrLf _
                & "" & Val(txtWorkingHours.Text) & ",'" & EmpPFCont & "','" & MainClass.AllowSingleQuote((txtAddEmpCode.Text)) & "', '" & mWFH & "'," & vbCrLf _
                & " " & IIf(Val(mMachineCode) = 0, "NULL", Val(mMachineCode)) & ", " & Val(txtDAAmount.Text) & ",'" & mTaxRegime & "',TO_DATE('" & VB6.Format(txtBonusDOJ.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & mHRHOD & "','" & mDeptHOD & "' ) " '','" & mMajorDeptCode & "'

        Else
            SqlStr = "UPDATE  PAY_EMPLOYEE_MST SET " & vbCrLf & " EMP_TYPE='" & mEmpType & "', " & vbCrLf & " EMP_CODE='" & mCode & "',  " & vbCrLf & " EMP_NAME='" & MainClass.AllowSingleQuote(txtName.Text) & "', " & vbCrLf & " EMP_ADDR='" & MainClass.AllowSingleQuote(txtAddress.Text) & "', " & vbCrLf & " EMP_CITY='" & MainClass.AllowSingleQuote(txtCity.Text) & "',  " & vbCrLf & " EMP_STATE='" & MainClass.AllowSingleQuote(txtState.Text) & "', " & vbCrLf & " EMP_PIN='" & MainClass.AllowSingleQuote(txtPinCode.Text) & "', " & vbCrLf & " EMP_PHONE_NO='" & MainClass.AllowSingleQuote(txtPhone.Text) & "' , " & vbCrLf & " EMP_MOBILE_NO='', " & vbCrLf & " EMP_EMAILID='" & MainClass.AllowSingleQuote(txtEmail.Text) & "', " & vbCrLf & " EMP_EMAILID_OFF='" & MainClass.AllowSingleQuote(txtOffeMail.Text) & "', " & vbCrLf & " EMP_CONTACT_PERSON='' , " & vbCrLf & " EMP_DEPT_CODE='" & mDeptCode & "', " & vbCrLf & " EMP_MARITAL_STATUS='" & mMaritalStatus & "', " & vbCrLf & " EMP_SEX='" & mSex & "' , " & vbCrLf & " EMP_DESG_CODE='" & mDesgCode & "', "

            SqlStr = SqlStr & vbCrLf & " EMP_CONT='" & EmpPFCont & "', EMP_LAST_COMPANY='" & MainClass.AllowSingleQuote(txtLastCompany.Text) & "', " & vbCrLf & " EMP_QUALIFICATION='" & MainClass.AllowSingleQuote(txtQualification.Text) & "',  " & vbCrLf & " EMP_TOTEXP=" & Val(txtExperience.Text) & ", " & vbCrLf & " EMP_DOB=TO_DATE('" & VB6.Format(txtDOB.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " EMP_DOJ=TO_DATE('" & VB6.Format(txtDOJ.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),  " & vbCrLf & " EMP_GROUP_DOJ=TO_DATE('" & VB6.Format(txtGroupDOJ.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),  " & vbCrLf & " SHIFT_CODE='" & mShiftCode & "', " & vbCrLf & " SALARY_TYPE='" & mSalaryType & "', " & vbCrLf & " EMP_DOC=TO_DATE('" & VB6.Format(txtDOP.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),  " & vbCrLf & " EMP_PF_ACNO='" & MainClass.AllowSingleQuote(txtPFNo.Text) & "', UID_NO=" & Val(txtUIDNo.Text) & "," & vbCrLf & " EMP_PF_DATE='', " & vbCrLf & " EMP_LEAVE_DATE=TO_DATE('" & VB6.Format(txtDOL.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),  " & vbCrLf & " EMP_LEAVE_REASON='" & MainClass.AllowSingleQuote(txtReasonForLeaving.Text) & "', " & vbCrLf & " EMP_BANK_NO='" & MainClass.AllowSingleQuote(txtBankAcno.Text) & "', EMPBANK_IFSC='" & MainClass.AllowSingleQuote(txtIFSCCode.Text) & "', " & vbCrLf & " EMP_ESI_FLAG='" & mESIFlag & "',  " & vbCrLf & " EMP_PROH_EXT='', " & vbCrLf & " COST_CENTER_CODE='" & mCostCenterCode & "', " & vbCrLf & " EMP_CATG='" & mCategory & "', " & vbCrLf & " GROSS_SALARY=" & mGrossSalary & ", " & vbCrLf & " BASIC_SALARY=" & Val(txtBSalary.Text) & ",PF_PENSION_APP= '" & mPFPensionFlag & "',"

            SqlStr = SqlStr & vbCrLf & " SALARY_EFF_DATE=TO_DATE('" & VB6.Format(txtWEF.Text, "DD/MMM/YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " EMP_FNAME='" & MainClass.AllowSingleQuote(txtFName.Text) & "', " & vbCrLf & " BLOOD_GROUP='" & MainClass.AllowSingleQuote(txtBloodGroup.Text) & "', " & vbCrLf & " EMP_BANK_NAME='" & MainClass.AllowSingleQuote(txtBankName.Text) & "', " & vbCrLf & " WORKINGTIMEFROM='" & MainClass.AllowSingleQuote(txtWorkingFrom.Text) & "',  " & vbCrLf & " WORKINGTIMETO='" & MainClass.AllowSingleQuote(txtWorkingTo.Text) & "', " & vbCrLf & " EMP_OT_RATE=" & Val(txtOTRate.Text) & ", " & vbCrLf & " EMP_ESI_NO='" & MainClass.AllowSingleQuote(txtESINo.Text) & "', " & vbCrLf & " BNKLOAN_DED='" & Val(txtBankLoan.Text) & "', " & vbCrLf & " LIC_DED='" & Val(txtLICAmount.Text) & "', " & vbCrLf & " ITAX_DED='" & Val(txtITAmount.Text) & "', " & vbCrLf & " LTA_AMT='" & Val(txtLTAAmount.Text) & "', " & vbCrLf & " BONUS_PER='" & Val(txtBonusPer.Text) & "', EMP_CAT_TYPE='" & mEmpCatType & "',"

            '                & " EMP_SPOUSE_NAME='" & MainClass.AllowSingleQuote(txtSpouse.Text) & "',  "

            SqlStr = SqlStr & vbCrLf & " EMP_PERMANENT_ADDR='" & MainClass.AllowSingleQuote(txtPAddress.Text) & "', " & vbCrLf & " EMP_PERMANENT_CITY='" & MainClass.AllowSingleQuote(txtPCity.Text) & "', " & vbCrLf & " EMP_PERMANENT_PIN='" & MainClass.AllowSingleQuote(txtPPinCode.Text) & "', " & vbCrLf & " EMP_PERMANENT_STATE='" & MainClass.AllowSingleQuote(txtPState.Text) & "', " & vbCrLf & " EMP_PERMANENT_PHONE_NO='" & MainClass.AllowSingleQuote(txtPPhone.Text) & "', " & vbCrLf & " ISPERMANENT_METROCITY='" & mPMetroCity & "', " & vbCrLf & " OVERTIME_APP='" & mOverTimeApp & "',  " & vbCrLf & " EMP_MOBILE_NO_OFF='" & MainClass.AllowSingleQuote(txtMobileOff.Text) & "'," & vbCrLf & " EMP_DOB_ACTUAL=TO_DATE('" & VB6.Format(txtDOBActual.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " EMP_DOM=TO_DATE('" & VB6.Format(txtDOM.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " EMP_ADHAAR_NO='" & MainClass.AllowSingleQuote(txtAdhaarNo.Text) & "',"



            SqlStr = SqlStr & vbCrLf _
                & " ESI_DISPENSARY='" & MainClass.AllowSingleQuote(txtDispensary.Text) & "' , EMP_DOJ_BONUS=TO_DATE('" & VB6.Format(txtBonusDOJ.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " EMP_PANNO='" & MainClass.AllowSingleQuote(txtPanNo.Text) & "', " & vbCrLf _
                & " EMP_LICNO='" & MainClass.AllowSingleQuote(txtLICID.Text) & "', " & vbCrLf _
                & " WEEKLYOFF='" & mWeeklyOff & "', " & vbCrLf _
                & " JOININGDESIGN='" & mJoiningDesc & "',  DA_AMOUNT=" & Val(txtDAAmount.Text) & "," & vbCrLf _
                & " PAYMENTMODE='" & mPaymentMode & "', " & vbCrLf _
                & " EMP_GROUP_INSURANCE='" & mGroupInsurance & "', " & vbCrLf _
                & " RGP_AUTH='" & mRGPAuthorization & "'," & vbCrLf _
                & " EMP_STOP_SALARY='" & mStopSalary & "',  " & vbCrLf _
                & " ADV_ACCOUNT_CODE='" & mAccountAdvanceCode & "', " & vbCrLf _
                & " IMPREST_ACCOUNT_CODE='" & mAccountImprestCode & "', " & vbCrLf _
                & " ISMETROCITY='" & mMetroCity & "', " & vbCrLf _
                & " IS_BONUS_PAYABLE='" & mBonusApp & "', " & vbCrLf _
                & " IS_LEAVE_ENCHASE_PAYABLE='" & mLEApp & "', " & vbCrLf _
                & " CONTRACTOR_CODE=" & mContractCode & ", DIV_CODE=" & mDivisionCode & "," & vbCrLf _
                & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                & " ModDate=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), IS_CORPORATE='" & mCorporate & "'," & vbCrLf _
                & " EMP_LOANNO='" & MainClass.AllowSingleQuote(txtLoanAcNo.Text) & "'," & vbCrLf _
                & " EMP_HOD_CODE='" & MainClass.AllowSingleQuote(mHODCode) & "'," & vbCrLf _
                & " EMP_RATE_TYPE='" & VB.Left(cboPcRateType.Text, 1) & "', IS_EL_CARRY='" & mEL & "', WORKING_HOURS=" & Val(txtWorkingHours.Text) & "," & vbCrLf _
                & " ADD_EMP_CODE='" & MainClass.AllowSingleQuote((txtAddEmpCode.Text)) & "', IS_WFH= '" & mWFH & "', MACHINE_CODE=" & IIf(Val(mMachineCode) = 0, "NULL", Val(mMachineCode)) & ", EMP_TAX_REGIME='" & mTaxRegime & "', IS_HR_HOD='" & mHRHOD & "', IS_DEPT_HOD='" & mDeptHOD & "'"

            SqlStr = SqlStr & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & mCode & "'"
        End If

        ''& " EMP_MAJOR_DEPT='" & mMajorDeptCode & "', " & vbCrLf

UpdatePart:
        PubDBCn.Execute(SqlStr)

        If Val(txtRefNo.Text) <> 0 Then
            SqlStr = "UPDATE PAY_CANDIDATE_MST SET EMP_CODE='" & mCode & "', IS_JOINED='Y', ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "', ModDate=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND REF_NO=" & Val(txtRefNo.Text) & ""
            PubDBCn.Execute(SqlStr)
        End If

        If UpdateEmpPhoto(mCode) = False Then GoTo UpdateError

        '    If chkStopSal.Value = vbUnchecked Then
        If UpdateSpouse(mCode) = False Then GoTo UpdateError

        If UpdateAssets(mCode) = False Then GoTo UpdateError

        If lblEmpType.Text <> "O" Then
            If UpdateOPLeave(mCode) = False Then GoTo UpdateError

            If CheckSalary(mCode) = False Then
                If UpdateSalaryDef(mCode, (txtWEF.Text), Val(txtBSalary.Text), Val(txtForm1BSalary.Text), mDesgCode) = False Then GoTo UpdateError
            End If
        End If
        '    End If
        PubDBCn.CommitTrans()
        'RsEmp.Requery()

        Update1 = True
        Exit Function
UpdateError:
        '    If err.Number = -2147467259 Then
        ''        Resume
        '        MsgBox "Can't Modify Transaction Exists Against this Code"
        '        PubDBCn.RollbackTrans
        '        Exit Function
        '    End If
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        Update1 = False
        PubDBCn.RollbackTrans()
        'RsEmp.Requery()
        PubDBCn.Errors.Clear()
        '   Resume
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function UpdateAssets(ByRef xCode As String) As Boolean
        On Error GoTo UpdateLoanErr

        Dim SqlStr As String = ""
        Dim cntRow As Integer
        Dim xAssetDesc As String
        Dim xAssetMake As String
        Dim xAssetPrice As String
        Dim xAssetDOP As String
        Dim xAssetDOI As String
        Dim xAssetRemarks As String

        UpdateAssets = True

        SqlStr = " DELETE FROM PAY_ASSETS_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & xCode & "' "

        PubDBCn.Execute(SqlStr)

        SqlStr = ""

        With sprdAssets
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColAssetDesc
                xAssetDesc = Trim(.Text)

                .Col = ColAssetMake
                xAssetMake = Trim(.Text)

                .Col = ColAssetPrice
                xAssetPrice = CStr(Val(.Text))

                .Col = ColAssetDOP
                xAssetDOP = Trim(.Text)

                .Col = ColAssetDOI
                xAssetDOI = Trim(.Text)

                .Col = ColAssetRemarks
                xAssetRemarks = Trim(.Text)

                If Trim(xAssetDesc) <> "" Then
                    SqlStr = " Insert Into PAY_ASSETS_MST ( " & vbCrLf & " COMPANY_CODE, EMP_CODE," & vbCrLf & " ASSETS_DESC, ASSETS_MAKE, ASSETS_PRICE, " & vbCrLf & " ASSETS_DOP, ASSETS_DOI, ASSETS_REMARKS" & vbCrLf & " ) VALUES " & vbCrLf & " (" & RsCompany.Fields("COMPANY_CODE").Value & ", '" & xCode & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(xAssetDesc) & "', '" & MainClass.AllowSingleQuote(xAssetMake) & "', " & Val(xAssetPrice) & ", " & vbCrLf & " TO_DATE('" & VB6.Format(xAssetDOP, "DD-MMM-YYYY") & "','DD-MON-YYYY'), TO_DATE('" & VB6.Format(xAssetDOI, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(xAssetRemarks) & "' " & vbCrLf & " ) "
                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With

        Exit Function
UpdateLoanErr:
        MsgBox(Err.Description)
        UpdateAssets = False
    End Function
    Private Sub TxtEmpNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpNo.DoubleClick
        cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub

    Private Sub txtName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtEmpNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtEmpNo.KeyUp
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
        Dim mHODCode As String
        Dim mEmpCategory As String

        Dim mRefNo As String
        Dim mMDApproval As String
        Dim mCFOApproval As String
        Dim mCEOApproval As String
        Dim mHRApproval As String
        Dim mApprovalCount As Integer
        Dim mESICeiling As Double

        FieldsVarification = True

        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Master or modify an existing Master")
            FieldsVarification = False
            Exit Function
        End If

        'If ADDMode = True Then
        '    If Trim(txtRefNo.Text) = "" Then
        '        MsgInformation("Please Select the Employee Requisition No.")
        '        FieldsVarification = False
        '        Exit Function
        '    End If

        '    mRefNo = VB6.Format(Val(txtRefNo.Text), "000000")

        '    SqlStr = ""
        '    SqlStr = "SELECT * FROM  PAY_CANDIDATE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND REF_NO=" & Val(Trim(txtRefNo.Text)) & "" & vbCrLf & " AND IS_JOINED='N'"

        '    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        '    mApprovalCount = 0
        '    If RsTemp.EOF = False Then
        '        mMDApproval = IIf(IsDbNull(RsTemp.Fields("MD_APPROVAL").Value), "N", RsTemp.Fields("MD_APPROVAL").Value)
        '        mCFOApproval = IIf(IsDbNull(RsTemp.Fields("CFO_APPROVAL").Value), "N", RsTemp.Fields("CFO_APPROVAL").Value)
        '        mCEOApproval = IIf(IsDbNull(RsTemp.Fields("CEO_APPROVAL").Value), "N", RsTemp.Fields("CEO_APPROVAL").Value)
        '        mHRApproval = IIf(IsDbNull(RsTemp.Fields("HR_APPROVAL").Value), "N", RsTemp.Fields("HR_APPROVAL").Value)
        '        mApprovalCount = IIf(mMDApproval = "Y", 1, 0)
        '        mApprovalCount = mApprovalCount + IIf(mCFOApproval = "Y", 1, 0)
        '        mApprovalCount = mApprovalCount + IIf(mCEOApproval = "Y", 1, 0)
        '        mApprovalCount = mApprovalCount + IIf(mHRApproval = "Y", 1, 0)
        '        '            If mApprovalCount < 3 Then
        '        '                MsgInformation "Atleast Three Approval Required for Update Master."
        '        '                FieldsVarification = False
        '        '                Exit Function
        '        '            End If
        '    Else
        '        MsgInformation("Invalid Employee Requisition No.")
        '        FieldsVarification = False
        '        Exit Function
        '    End If
        'End If

        If Trim(txtName.Text) = "" Then
            MsgInformation("Name is empty. Cannot Save")
            txtName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtEmpNo.Text) = "" Then
            MsgInformation("Card No is empty. Cannot Save")
            txtEmpNo.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtFName.Text) = "" Then
            MsgInformation("Father's Name is empty. Cannot Save")
            txtFName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtBloodGroup.Text) = "" Then
            MsgInformation("Blood Group is empty. Cannot Save")
            txtBloodGroup.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtHODName.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(Trim(txtHODName.Text), "EMP_NAME", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mHODCode = MasterNo

                If Trim(txtEmpNo.Text) = Trim(mHODCode) Then
                    MsgInformation("Employee HOD should not be Same. Cannot Save")
                    txtHODName.Focus()
                    FieldsVarification = False
                    Exit Function
                End If
            Else
                MsgInformation("Invalid HOD Name. Cannot Save")
                txtHODName.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If Not IsDate(txtDOB.Text) Or Trim(txtDOB.Text) = "" Then
            MsgInformation("DOB cann't be blank.")
            txtDOB.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Not IsDate(txtDOJ.Text) Or Trim(txtDOJ.Text) = "" Then
            MsgInformation("Joining Date cann't be blank.")
            txtDOJ.Focus()
            FieldsVarification = False
            Exit Function
        End If


        If ADDMode = True Then
            If AgeYears(CDate(VB6.Format(txtDOB.Text, "DD/MM/YYYY")), CDate(VB6.Format(txtDOJ.Text, "DD/MM/YYYY"))) < 18 Then
                MsgInformation("Age Cann't be Less Than 18 at the time of Joining.")
                txtDOJ.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If Not IsDate(txtBonusDOJ.Text) Or Trim(txtBonusDOJ.Text) = "" Then
            txtBonusDOJ.Text = txtDOJ.Text
        End If

        If IsDate(txtDOL.Text) Then
            If CDate(txtDOL.Text) < CDate(txtDOL.Text) Then
                MsgInformation("Leaving Date Cann't be less than Joining Date.")
                txtDOL.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        'If RsCompany.Fields("COMPANY_CODE").Value = 42 Then
        '    If Val(txtEmpNo.Text) < 190001 And Val(txtEmpNo.Text) < 200000 Then
        '        MsgInformation("Please Enter Emp Code more then 190000. Cannot Save")
        '        txtEmpNo.Focus()
        '        FieldsVarification = False
        '        Exit Function
        '    End If
        'ElseIf RsCompany.Fields("COMPANY_CODE").Value = 43 Then
        '    If Val(txtEmpNo.Text) < 200001 And Val(txtEmpNo.Text) < 210000 Then
        '        MsgInformation("Please Enter Emp Code more then 200000. Cannot Save")
        '        txtEmpNo.Focus()
        '        FieldsVarification = False
        '        Exit Function
        '    End If
        'Else
        '    If VB.Left(cboCatgeory.Text, 1) = "C" Then
        '        If Val(txtEmpNo.Text) < 100000 Then
        '            MsgInformation("Please Enter Emp Code more then 100000. Cannot Save")
        '            txtEmpNo.Focus()
        '            FieldsVarification = False
        '            Exit Function
        '        End If
        '        If Trim(txtContractor.Text) = "" Then
        '            MsgInformation("Contractor Name Cann't be Blank. Cannot Save")
        '            txtContractor.Focus()
        '            FieldsVarification = False
        '            Exit Function
        '        End If
        '        If MainClass.ValidateWithMasterTable(txtContractor.Text, "CON_NAME", "CON_CODE", "PAY_CONTRACTOR_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
        '            MsgInformation("Invalid Contractor Name. Cannot Save")
        '            txtContractor.Focus()
        '            FieldsVarification = False
        '            Exit Function
        '        End If
        '    Else
        '        If Val(txtEmpNo.Text) > 100000 Then
        '            MsgInformation("Please Enter Emp Code less then 100000. Cannot Save")
        '            txtEmpNo.Focus()
        '            FieldsVarification = False
        '            Exit Function
        '        End If
        '    End If
        'End If

        If chkStopSal.CheckState = System.Windows.Forms.CheckState.Checked Then GoTo Label1

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

        If cboSex.SelectedIndex = -1 Then
            MsgInformation("Please enter the Sex.")
            cboSex.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If cboTaxRegime.SelectedIndex = -1 Then
            MsgInformation("Please enter the Tax Regime.")
            cboTaxRegime.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If cboCorporate.Text = "" Then
            MsgInformation("Please Select Corporate .")
            cboCorporate.Focus()
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

        If Trim(cboEmpCatType.Text) = "" Then
            MsgInformation("Employee Category Type Cann't be Blank")
            cboEmpCatType.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If cboEmpCatType.SelectedIndex = -1 Then
            MsgInformation("Employee Category Type Cann't be Blank")
            cboEmpCatType.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
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

        mEmpCategory = "S"
        If MainClass.ValidateWithMasterTable(Trim(cbodesignation.Text), "DESG_DESC", "DESG_CAT", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mEmpCategory = MasterNo
        End If

        If PubPayCorpUser = "N" And (mEmpCategory = "D" Or mEmpCategory = "M") Then
            MsgInformation("You have not Rights to change Employee Master of this Employee.")
            FieldsVarification = False
            Exit Function
        End If

        If Trim(cboPaymentMode.Text) = "" Then
            MsgInformation("Payment Mode Cann't be Blank")
            cboPaymentMode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If cboPaymentMode.Text = "Cheque" Or cboPaymentMode.Text = "DD" Or cboPaymentMode.Text = "Bank Transfer" Then
            If Trim(txtBankName.Text) = "" Then
                MsgInformation("Please Enter the Bank Name.")
                txtBankName.Focus()
                FieldsVarification = False
                Exit Function
            End If

            If Trim(txtBankAcno.Text) = "" Then
                MsgInformation("Please Enter the Bank Account No.")
                txtBankAcno.Focus()
                FieldsVarification = False
                Exit Function
            End If

            If Trim(txtIFSCCode.Text) = "" Then
                MsgInformation("Please Enter the Bank IFSC Code.")
                txtIFSCCode.Focus()
                FieldsVarification = False
                Exit Function
            End If

        End If

        If Not IsDate(txtDOJ.Text) Then
            MsgInformation("Joining Date cann't be blank.")
            txtDOJ.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If MODIFYMode = True Then
            If CDate(RsEmp.Fields("EMP_DOJ").Value) <> CDate(txtDOJ.Text) Then
                If CheckSalaryMade((txtEmpNo.Text), "") = True Then
                    MsgInformation("Salary Made. So Cann't be Modified")
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        End If

        If Not IsDate(txtDOP.Text) Then
            MsgInformation("Permanent Date cann't be blank.")
            txtDOP.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Not IsDate(txtNextIncDueDate.Text) Then
            MsgInformation("Next Increment Date cann't be blank.")
            txtNextIncDueDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If cboWeeklyOff.Text = "" Then
            MsgInformation("Please enter the WEEKLY OFF.")
            cboWeeklyOff.Focus()
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

        If Trim(cboPFPension.Text) = "" Then
            MsgInformation("PF Pension Applicable Cann't be Blank")
            cboPFPension.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Val(txtBSalary.Text) <= 15000 And cboPFPension.SelectedIndex = 1 Then
            If MsgQuestion("PF Pension Applicable is NO, Want to Continue..? ") = CStr(MsgBoxResult.No) Then
                FieldsVarification = False
                cboPFPension.Focus()
                Exit Function
            End If
            'MsgInformation("PF Pension Applicable Cann't be No.")
            'cboPFPension.Focus()
            'FieldsVarification = False
            'Exit Function
        End If

        If Val(txtGSalary.Text) > 20000 And Trim(txtPanNo.Text) = "" Then
            If MsgQuestion("PAN No is Blank, Want to Continue..? ") = CStr(MsgBoxResult.No) Then
                FieldsVarification = False
                txtPanNo.Focus()
                Exit Function
            End If
            'MsgInformation("PAN No. is Must, please enter the PAN No.")
            'FieldsVarification = False
            'txtPanNo.Focus()
            'Exit Function
        End If

        If Trim(txtPanNo.Text) <> "" Then
            If CheckPANValidation((txtPanNo.Text)) = False Then
                MsgInformation("Invalid PAN No.")
                FieldsVarification = False
                txtPanNo.Focus()
                Exit Function
            End If
        End If

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





        If VB.Left(cboCatgeory.Text, 1) = "R" Then
            If Trim(cboPcRateType.Text) = "" Then
                MsgInformation("Pc. Rate Type Cann't be Blank")
                cboPcRateType.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If Trim(cboDivision.Text) = "" Then
            MsgBox("Division Name is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If cboDivision.Enabled = True Then cboDivision.Focus()
            Exit Function
        End If

        If Trim(cboOverTime.Text) = "" Or cboOverTime.SelectedIndex = -1 Then
            MsgBox("Over Time Applicable is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If cboOverTime.Enabled = True Then cboOverTime.Focus()
            Exit Function
        End If

        Dim mESIAmount As Double

        Call CalcPFESI(mESIAmount)
        mESICeiling = CheckESICeiling(txtWEF.Text)

        If mESIAmount > 0 And Val(VB6.Format(txtGSalary.Text, "0.00")) > 0 Then
            If Val(VB6.Format(txtGSalary.Text, "0.00")) > mESICeiling Then

                If MsgBox("Please Check ESI Amount.Gross Amount is greater than ESI Ceiling ... " & vbNewLine & vbNewLine & "Want To Process ...", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        End If

        If ADDMode Then
            Call ShowLeaveMaster()
        End If


        'If ADDMode = True And lblEmpType.Text = "S" Then
        '    If CheckVacantPost(Trim(mDeptCode), VB.Left(cboCorporate.Text, 1), VB6.Format(txtDOJ.Text, "DD/MM/YYYY")) = False Then
        '        MsgInformation("You have not Sanction for this Dept.")
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

        If sprdAssets.MaxRows > 1 Then
            If MainClass.ValidDataInGrid(sprdAssets, ColAssetDesc, "S", "Assets Description is Blank.") = False Then FieldsVarification = False : Exit Function
            If MainClass.ValidDataInGrid(sprdAssets, ColAssetMake, "S", "Assets Make is Blank.") = False Then FieldsVarification = False : Exit Function
            If MainClass.ValidDataInGrid(sprdAssets, ColAssetPrice, "N", "Price is Blank.") = False Then FieldsVarification = False : Exit Function
            If MainClass.ValidDataInGrid(sprdAssets, ColAssetDOP, "S", "Date of Purchase is Blank.") = False Then FieldsVarification = False : Exit Function
            If MainClass.ValidDataInGrid(sprdAssets, ColAssetDOI, "S", "Date of Issue is Blank.") = False Then FieldsVarification = False : Exit Function
        End If

Label1:
        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
        End If
        If MODIFYMode = True And (RsEmp.EOF = True) Then Exit Function
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        FieldsVarification = False
        ''Resume
    End Function
    Private Sub settextlength()

        On Error GoTo ERR1

        txtEmpNo.Maxlength = RsEmp.Fields("EMP_CODE").DefinedSize
        TxtName.MaxLength = RsEmp.Fields("EMP_NAME").DefinedSize
        txtAddEmpCode.MaxLength = RsEmp.Fields("ADD_EMP_CODE").DefinedSize
        txtHODName.Maxlength = MainClass.SetMaxLength("EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn)
        txtFName.Maxlength = RsEmp.Fields("EMP_FNAME").DefinedSize
        txtBloodGroup.Maxlength = RsEmp.Fields("BLOOD_GROUP").DefinedSize
        txtDOB.Maxlength = 10
        txtBSalary.Maxlength = RsEmp.Fields("BASIC_SALARY").Precision
        txtGSalary.Maxlength = RsEmp.Fields("GROSS_SALARY").Precision
        txtDeduction.MaxLength = RsEmp.Fields("GROSS_SALARY").Precision
        txtNetSalary.MaxLength = RsEmp.Fields("GROSS_SALARY").Precision
        txtQualification.Maxlength = RsEmp.Fields("EMP_QUALIFICATION").DefinedSize

        txtForm1BSalary.MaxLength = RsEmp.Fields("BASIC_SALARY").Precision
        txtForm1GSalary.MaxLength = RsEmp.Fields("GROSS_SALARY").Precision
        txtForm1NetSalary.MaxLength = RsEmp.Fields("GROSS_SALARY").Precision
        txtForm1CTC.MaxLength = RsEmp.Fields("GROSS_SALARY").Precision

        txtLastCompany.Maxlength = RsEmp.Fields("EMP_LAST_COMPANY").DefinedSize
        txtExperience.Maxlength = RsEmp.Fields("EMP_TOTEXP").DefinedSize
        txtBankName.Maxlength = RsEmp.Fields("EMP_BANK_NAME").DefinedSize
        txtBankAcno.Maxlength = RsEmp.Fields("EMP_BANK_NO").DefinedSize
        txtIFSCCode.Maxlength = RsEmp.Fields("EMPBANK_IFSC").DefinedSize
        txtDOJ.MaxLength = 10
        txtBonusDOJ.MaxLength = 10
        txtGroupDOJ.Maxlength = 10
        txtDOP.Maxlength = 10
        txtDOL.Maxlength = 10
        txtReasonForLeaving.Maxlength = RsEmp.Fields("EMP_LEAVE_REASON").DefinedSize
        txtWorkingFrom.Maxlength = RsEmp.Fields("WORKINGTIMEFROM").DefinedSize
        txtWorkingTo.MaxLength = RsEmp.Fields("WORKINGTIMETO").DefinedSize

        txtWorkingHours.MaxLength = RsEmp.Fields("WORKING_HOURS").Precision

        txtOTRate.Maxlength = RsEmp.Fields("EMP_OT_RATE").DefinedSize
        txtAddress.Maxlength = RsEmp.Fields("EMP_ADDR").DefinedSize
        txtCity.Maxlength = RsEmp.Fields("EMP_CITY").DefinedSize
        txtPinCode.Maxlength = RsEmp.Fields("EMP_PIN").DefinedSize
        txtState.Maxlength = RsEmp.Fields("EMP_STATE").DefinedSize
        txtPhone.Maxlength = RsEmp.Fields("EMP_PHONE_NO").DefinedSize

        txtPAddress.Maxlength = RsEmp.Fields("EMP_PERMANENT_ADDR").DefinedSize
        txtPCity.Maxlength = RsEmp.Fields("EMP_PERMANENT_CITY").DefinedSize
        txtPPinCode.Maxlength = RsEmp.Fields("EMP_PERMANENT_PIN").DefinedSize
        txtPState.Maxlength = RsEmp.Fields("EMP_PERMANENT_STATE").DefinedSize
        txtPPhone.Maxlength = RsEmp.Fields("EMP_PERMANENT_PHONE_NO").DefinedSize

        txtEmail.Maxlength = RsEmp.Fields("EMP_EMAILID").DefinedSize
        txtOffeMail.Maxlength = RsEmp.Fields("EMP_EMAILID_OFF").DefinedSize
        '    txtSpouse.MaxLength = RsEmp.Fields("EMP_SPOUSE_NAME").DefinedSize
        txtPFNo.Maxlength = RsEmp.Fields("EMP_PF_ACNO").DefinedSize
        txtUIDNo.Maxlength = RsEmp.Fields("UID_NO").Precision

        txtESINo.Maxlength = RsEmp.Fields("EMP_ESI_NO").DefinedSize
        txtDispensary.Maxlength = RsEmp.Fields("ESI_DISPENSARY").DefinedSize
        txtPanNo.Maxlength = RsEmp.Fields("EMP_PANNO").DefinedSize
        txtLICID.Maxlength = RsEmp.Fields("EMP_LICNO").DefinedSize
        txtLoanAcNo.Maxlength = RsEmp.Fields("EMP_LOANNO").DefinedSize

        txtAdhaarNo.Maxlength = RsEmp.Fields("EMP_ADHAAR_NO").DefinedSize
        txtMobileOff.Maxlength = RsEmp.Fields("EMP_MOBILE_NO_OFF").DefinedSize
        txtDOBActual.Maxlength = 10
        txtDOM.Maxlength = 10


        txtLoanAcName.Maxlength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
        txtImprestAcName.Maxlength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)

        txtCostCenter.Maxlength = MainClass.SetMaxLength("CC_DESC", "FIN_CCENTER_HDR", PubDBCn)
        txtLICAmount.Maxlength = RsEmp.Fields("LIC_DED").Precision
        txtBankLoan.Maxlength = RsEmp.Fields("BNKLOAN_DED").Precision
        txtITAmount.Maxlength = RsEmp.Fields("ITAX_DED").Precision
        txtLTAAmount.Maxlength = RsEmp.Fields("LTA_AMT").Precision
        txtBonusPer.Maxlength = RsEmp.Fields("BONUS_PER").Precision
        txtDAAmount.MaxLength = RsEmp.Fields("DA_AMOUNT").Precision
        txtContractor.Maxlength = MainClass.SetMaxLength("CON_NAME", "PAY_CONTRACTOR_MST", PubDBCn)

        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Sub AssignGrid(ByRef mRefresh As Boolean)

        Dim SqlStr As String = ""
        SqlStr = " SELECT EMP_CODE, EMP_NAME, " & vbCrLf & " EMP_FNAME,  EMP_DEPT_CODE, " & vbCrLf & " GROSS_SALARY, EMP_PF_ACNO, EMP_ESI_NO,EMP_BANK_NO " & vbCrLf & " FROM PAY_EMPLOYEE_MST "


        SqlStr = SqlStr & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If lblEmpType.Text = "O" Then

        ElseIf lblEmpType.Text = "S" Then
            SqlStr = SqlStr & " AND EMP_CAT_TYPE='1'"
        Else
            SqlStr = SqlStr & " AND EMP_CAT_TYPE='2'"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY EMP_NAME "


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
        Dim mEmpCode As String
        SqlStr = ""
        '    MainClass.ValidateWithMasterTable txtName.Text, "EMP_NAME", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        mEmpCode = Trim(txtEmpNo.Text)


        If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_CODE", "PAY_SAL_TRN", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            MsgBox("Salary Exists Against This Employee.")
            Delete1 = False
            Exit Function
        End If
        '    If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_CODE", "PAY_SALARYDEF_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        MsgBox "Salary Exists Against This Employee."
        '        Delete1 = False
        '        Exit Function
        ''    ElseIf MainClass.ValidateWithMasterTable(mEmpCode, "EmpCode", "EmpCode", "SalTrn", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        ''        MsgBox "Salary Exists Against This Employee."
        ''        Delete1 = False
        ''        Exit Function
        '    ElseIf MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_CODE", "PAY_OPLeave_TRN", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        MsgBox "Opening Leaves Exists Against This Employee."
        '        Delete1 = False
        '        Exit Function
        '    End If

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If InsertIntoDeleteTrn(PubDBCn, "PAY_EMPLOYEE_MST", "EMP_CODE", xCode) = False Then GoTo DeleteErr

        SqlStr = "Delete from PAY_SALARYDEF_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CODE='" & xCode & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = "Delete from PAY_SPOUSE_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CODE='" & xCode & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = "Delete from PAY_ASSETS_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CODE='" & xCode & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = "Delete from PAY_OPLeave_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CODE='" & xCode & "'"
        PubDBCn.Execute(SqlStr)

        If Val(txtRefNo.Text) > 0 Then
            SqlStr = "UPDATE PAY_CANDIDATE_MST SET IS_JOINED='N', EMP_CODE=NULL WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND REF_NO=" & Val(txtRefNo.Text) & ""
            PubDBCn.Execute(SqlStr)
        End If

        SqlStr = "Delete from PAY_EMPLOYEE_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CODE='" & xCode & "'"
        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()
        RsEmp.Requery()
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans()
        RsEmp.Requery()
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete Transaction Exists Against This Employee." & Err.Description)
            Exit Function
        End If
        MsgBox(Err.Description)
    End Function
    Public Sub TxtEmpNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmpNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim mEmpCode As String

        If Trim(txtEmpNo.Text) = "" Then GoTo EventExitSub
        mEmpCode = Trim(txtEmpNo.Text)

        If lblEmpType.Text <> "O" Then
            If cboCatgeory.Enabled = False Then
                If Val(mEmpCode) < 100000 Then
                    MsgBox("Invalid Card No for Contractor Employee.")
                    Cancel = True
                    GoTo EventExitSub
                End If
            End If
        End If

        If ADDMode Then
            If Val(txtRefNo.Text) = 0 Then
                Clear1()
            End If
            Call ShowLeaveMaster()
        End If
        txtEmpNo.Text = VB6.Format(mEmpCode, "000000")

        If MODIFYMode = True And RsEmp.EOF = False Then xCode = RsEmp.Fields("EMP_CODE").Value

        SqlStr = ""
        SqlStr = "SELECT * FROM  PAY_EMPLOYEE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(Trim(txtEmpNo.Text)) & "' "

        If lblEmpType.Text = "O" Then

        ElseIf lblEmpType.Text = "S" Then
            SqlStr = SqlStr & " AND EMP_CAT_TYPE='1'"
        Else
            SqlStr = SqlStr & " AND EMP_CAT_TYPE='2'"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmp, ADODB.LockTypeEnum.adLockOptimistic)
        If RsEmp.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Name Does Not Exist In Master" & vbCrLf & "Click Add To Add In Master", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM  PAY_EMPLOYEE_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & xCode & "' "

                If lblEmpType.Text = "O" Then

                ElseIf lblEmpType.Text = "S" Then
                    SqlStr = SqlStr & " AND EMP_CAT_TYPE='1'"
                Else
                    SqlStr = SqlStr & " AND EMP_CAT_TYPE='2'"
                End If

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmp, ADODB.LockTypeEnum.adLockOptimistic)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
        '    Resume
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtOTRate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOTRate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPFno_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPFNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtphone_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPhone.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtReasonForLeaving_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtReasonForLeaving.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub FillSalarySprd()
        On Error GoTo Err1
        Dim RsADD As ADODB.Recordset
        Dim SqlStr As String = ""


        MainClass.ClearGrid(sprdEarn, -1)
        MainClass.ClearGrid(sprdDeduct, -1)
        MainClass.ClearGrid(sprdPerks, -1)


        SSTab1.SelectedIndex = 6

        SqlStr = " SELECT * From PAY_SALARYHEAD_MST  " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND (CALC_ON=" & ConCalcBSalary & " OR CALC_ON =" & ConCalcFixed & ") " & vbCrLf _
            & " AND (ADDDEDUCT=" & ConEarning & " ) " & vbCrLf _
            & " AND TYPE <> " & ConOT & " "

        SqlStr = SqlStr & vbCrLf & "ORDER BY ADDDEDUCT, SEQ "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsADD, ADODB.LockTypeEnum.adLockOptimistic)

        If RsADD.EOF = False Then
            With RsADD
                Do While Not .EOF
                    'MsgBox(sprdEarn.MaxRows & sprdDeduct.MaxRows & sprdPerks.MaxRows)
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
                        End If
                    End If
                Loop
            End With
        End If

        SqlStr = " SELECT * From PAY_SALARYHEAD_MST  " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND (CALC_ON=" & ConCalcBSalary & " OR CALC_ON =" & ConCalcFixed & ") " & vbCrLf _
            & " AND (ADDDEDUCT =" & ConDeduct & ") " & vbCrLf _
            & " AND TYPE <> " & ConOT & " "

        SqlStr = SqlStr & vbCrLf & "ORDER BY ADDDEDUCT, SEQ "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsADD, ADODB.LockTypeEnum.adLockOptimistic)

        If RsADD.EOF = False Then
            With RsADD
                Do While Not .EOF
                    'MsgBox(sprdEarn.MaxRows & sprdDeduct.MaxRows & sprdPerks.MaxRows)
                    If .Fields("ADDDEDUCT").Value = ConDeduct Then
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
                        If .Fields("ADDDEDUCT").Value = ConDeduct Then
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


        SSTab1.SelectedIndex = 7

        SqlStr = " SELECT * From PAY_SALARYHEAD_MST  " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND (CALC_ON=" & ConCalcBSalary & " OR CALC_ON =" & ConCalcFixed & ") " & vbCrLf _
            & " AND (ADDDEDUCT=" & ConPerks & ") " & vbCrLf _
            & " AND TYPE <> " & ConOT & " "

        SqlStr = SqlStr & vbCrLf & "ORDER BY ADDDEDUCT, SEQ "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsADD, ADODB.LockTypeEnum.adLockOptimistic)

        If RsADD.EOF = False Then
            With RsADD
                Do While Not .EOF
                    'MsgBox(sprdEarn.MaxRows & sprdDeduct.MaxRows & sprdPerks.MaxRows)
                    If .Fields("ADDDEDUCT").Value = ConPerks Then
                        With sprdPerks
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
                        If .Fields("ADDDEDUCT").Value = ConPerks Then
                            sprdPerks.Col = 1
                            sprdPerks.Row = sprdPerks.MaxRows
                            If Trim(sprdPerks.Text) <> "" Then
                                sprdPerks.MaxRows = sprdPerks.MaxRows + 1
                                If sprdPerks.MaxRows > 3 Then
                                    sprdPerks.set_ColWidth(ColDesc, 14)
                                End If
                            End If
                        End If
                    End If
                Loop
            End With
        End If
        MainClass.ProtectCell(sprdEarn, 1, sprdEarn.MaxRows, ColCode, ColDesc)
        MainClass.ProtectCell(sprdDeduct, 1, sprdDeduct.MaxRows, ColCode, ColDesc)
        MainClass.ProtectCell(sprdPerks, 1, sprdPerks.MaxRows, ColCode, ColDesc)
        Exit Sub
Err1:
        ErrorMsg(Err.Description, Err.Number)
    End Sub
    Private Sub ShowSalary(ByRef xCode As String)
        On Error GoTo Err1
        Dim RsADD As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim mTypeCode As Object
        Dim xSubkey As Integer
        Dim mEarnAmt As Object
        Dim mDeductAmt As Decimal
        Dim cntRow As Integer

        txtBSalary.Enabled = True
        txtForm1BSalary.Enabled = True
        txtNextIncDueDate.Enabled = True

        SSTab1.SelectedIndex = 6
        With sprdEarn
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = 1
                mTypeCode = Val(.Text)

                SqlStr = " SELECT * from PAY_SalaryDef_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND EMP_CODE='" & xCode & "'" & vbCrLf _
                    & " AND ADD_DEDUCTCode=" & mTypeCode & " AND  " & vbCrLf _
                    & " SALARY_EFF_DATE=(SELECT MAX(SALARY_EFF_DATE) From PAY_SalaryDef_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND EMP_CODE='" & xCode & "'" & vbCrLf _
                    & " AND SALARY_EFF_DATE<= TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsADD, ADODB.LockTypeEnum.adLockOptimistic)

                If Not RsADD.EOF Then
                    txtBSalary.Text = VB6.Format(RsADD.Fields("BASICSALARY").Value, "0.00")
                    txtForm1BSalary.Text = VB6.Format(RsADD.Fields("FORM1_BASICSALARY").Value, "0.00")
                    txtWEF.Text = VB6.Format(IIf(IsDbNull(RsADD.Fields("SALARY_EFF_DATE").Value), "", RsADD.Fields("SALARY_EFF_DATE").Value), "DD/MM/YYYY")
                    txtNextIncDueDate.Text = VB6.Format(IIf(IsDbNull(RsADD.Fields("NEXT_INC_DATE").Value), "", RsADD.Fields("NEXT_INC_DATE").Value), "DD/MM/YYYY")

                    If RsADD.Fields("EMP_DESG_CODE").Value <> "" Then
                        If MainClass.ValidateWithMasterTable(Trim(RsADD.Fields("EMP_DESG_CODE").Value), "DESG_CODE", "DESG_DESC", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            cbodesignation.Text = MasterNo
                        End If
                    End If

                    .Row = cntRow
                    .Col = ColPer
                    .Text = CStr(IIf(IsDbNull(RsADD.Fields("PERCENTAGE").Value), "", RsADD.Fields("PERCENTAGE").Value))


                    .Col = ColDeductOn
                    .Text = CStr(IIf(IsDBNull(RsADD.Fields("AMOUNT_DEDUCT_ON").Value), "", RsADD.Fields("AMOUNT_DEDUCT_ON").Value))

                    .Col = ColAmt
                    .Text = CStr(IIf(IsDbNull(RsADD.Fields("Amount").Value), "", RsADD.Fields("Amount").Value))

                    .Col = ColForm1Amt
                    .Text = CStr(IIf(IsDBNull(RsADD.Fields("Form1_Amount").Value), "", RsADD.Fields("FORM1_Amount").Value))

                    txtBSalary.Enabled = False
                    txtForm1BSalary.Enabled = IIf(Val(txtForm1BSalary.Text) > 0, False, True)
                    txtNextIncDueDate.Enabled = False
                    '    MainClass.ProtectCell(sprdEarn, 1, sprdEarn.MaxRows, ColCode, ColAmt)
                    '    MainClass.ProtectCell(sprdDeduct, 1, sprdDeduct.MaxRows, ColCode, ColAmt)
                    'Else
                    '    '                txtBSalary.Enabled = True
                    '    '                txtNextIncDueDate.Enabled = True
                    '    MainClass.ProtectCell(sprdEarn, 1, sprdEarn.MaxRows, ColCode, ColDesc)
                    '    MainClass.ProtectCell(sprdDeduct, 1, sprdDeduct.MaxRows, ColCode, ColDesc)
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

                SqlStr = " SELECT * from PAY_SalaryDef_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & xCode & "'" & vbCrLf & " AND ADD_DEDUCTCode=" & mTypeCode & " AND  " & vbCrLf & " SALARY_EFF_DATE=(SELECT MAX(SALARY_EFF_DATE) From PAY_SalaryDef_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & xCode & "'" & vbCrLf & " AND SALARY_EFF_DATE<= TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsADD, ADODB.LockTypeEnum.adLockOptimistic)

                If Not RsADD.EOF Then
                    txtBSalary.Text = VB6.Format(RsADD.Fields("BASICSALARY").Value, "0.00")
                    txtForm1BSalary.Text = VB6.Format(RsADD.Fields("FORM1_BASICSALARY").Value, "0.00")
                    txtWEF.Text = VB6.Format(IIf(IsDbNull(RsADD.Fields("SALARY_EFF_DATE").Value), "", RsADD.Fields("SALARY_EFF_DATE").Value), "DD/MM/YYYY")
                    txtNextIncDueDate.Text = VB6.Format(IIf(IsDbNull(RsADD.Fields("NEXT_INC_DATE").Value), "", RsADD.Fields("NEXT_INC_DATE").Value), "DD/MM/YYYY")

                    .Row = cntRow

                    .Col = ColDeductOn
                    .Text = CStr(IIf(IsDBNull(RsADD.Fields("AMOUNT_DEDUCT_ON").Value), "", RsADD.Fields("AMOUNT_DEDUCT_ON").Value))


                    .Col = ColPer
                    .Text = CStr(IIf(IsDbNull(RsADD.Fields("PERCENTAGE").Value), "", RsADD.Fields("PERCENTAGE").Value))

                    .Col = ColAmt
                    .Text = CStr(IIf(IsDbNull(RsADD.Fields("Amount").Value), "", RsADD.Fields("Amount").Value))

                    .Col = ColForm1Amt
                    .Text = CStr(IIf(IsDBNull(RsADD.Fields("Form1_Amount").Value), "", RsADD.Fields("FORM1_Amount").Value))

                    txtBSalary.Enabled = False
                    txtForm1BSalary.Enabled = IIf(Val(txtForm1BSalary.Text) > 0, False, True)
                    txtNextIncDueDate.Enabled = False
                    '    MainClass.ProtectCell(sprdEarn, 1, sprdEarn.MaxRows, ColCode, ColAmt)
                    '    MainClass.ProtectCell(sprdDeduct, 1, sprdDeduct.MaxRows, ColCode, ColAmt)
                    'Else
                    '    '                txtBSalary.Enabled = True
                    '    '                txtNextIncDueDate.Enabled = True
                    '    MainClass.ProtectCell(sprdEarn, 1, sprdEarn.MaxRows, ColCode, ColDesc)
                    '    MainClass.ProtectCell(sprdDeduct, 1, sprdDeduct.MaxRows, ColCode, ColDesc)
                End If
NextRow1:
            Next
        End With

        SSTab1.SelectedIndex = 7
        cntRow = 1
        With sprdPerks
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = 1
                mTypeCode = Val(.Text)

                SqlStr = " SELECT * from PAY_SalaryDef_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND EMP_CODE='" & xCode & "'" & vbCrLf _
                    & " AND ADD_DEDUCTCode=" & mTypeCode & " AND  " & vbCrLf _
                    & " SALARY_EFF_DATE=(SELECT MAX(SALARY_EFF_DATE) From PAY_SalaryDef_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND EMP_CODE='" & xCode & "'" & vbCrLf _
                    & " AND SALARY_EFF_DATE<= TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsADD, ADODB.LockTypeEnum.adLockOptimistic)

                If Not RsADD.EOF Then
                    '                txtBSalary.Text = Format(RsADD!BASICSALARY, "0.00")
                    '                txtWEF.Text = Format(IIf(IsNull(RsADD!SALARY_EFF_DATE), "", RsADD!SALARY_EFF_DATE), "DD/MM/YYYY")
                    '                txtNextIncDueDate.Text = Format(IIf(IsNull(RsADD!NEXT_INC_DATE), "", RsADD!NEXT_INC_DATE), "DD/MM/YYYY")

                    .Row = cntRow

                    .Col = ColDeductOn
                    .Text = CStr(IIf(IsDBNull(RsADD.Fields("AMOUNT_DEDUCT_ON").Value), "", RsADD.Fields("AMOUNT_DEDUCT_ON").Value))

                    .Col = ColPer
                    .Text = CStr(IIf(IsDbNull(RsADD.Fields("PERCENTAGE").Value), "", RsADD.Fields("PERCENTAGE").Value))

                    .Col = ColAmt
                    .Text = CStr(IIf(IsDBNull(RsADD.Fields("Amount").Value), "", RsADD.Fields("Amount").Value))

                    .Col = ColForm1Amt
                    .Text = CStr(IIf(IsDBNull(RsADD.Fields("Form1_Amount").Value), "", RsADD.Fields("FORM1_Amount").Value))

                    '                txtBSalary.Enabled = False
                    '    MainClass.ProtectCell(sprdEarn, 1, sprdEarn.MaxRows, ColCode, ColAmt)
                    '    MainClass.ProtectCell(sprdDeduct, 1, sprdDeduct.MaxRows, ColCode, ColAmt)
                    '    MainClass.ProtectCell(sprdPerks, 1, sprdPerks.MaxRows, ColCode, ColAmt)
                    'Else
                    '    '                txtBSalary.Enabled = True
                    '    MainClass.ProtectCell(sprdEarn, 1, sprdEarn.MaxRows, ColCode, ColDesc)
                    '    MainClass.ProtectCell(sprdDeduct, 1, sprdDeduct.MaxRows, ColCode, ColDesc)
                    '    MainClass.ProtectCell(sprdPerks, 1, sprdPerks.MaxRows, ColCode, ColDesc)
                Else
                    .Row = cntRow
                    .Col = ColPer
                    .Text = "0.00"

                    .Col = ColDeductOn
                    .Text = "0.00"


                    .Col = ColAmt
                    .Text = "0.00"

                    .Col = ColForm1Amt
                    .Text = "0.00"
                End If
            Next
        End With

        If CheckSalaryMade(xCode, RunDate) = True Then
            MainClass.ProtectCell(sprdEarn, 1, sprdEarn.MaxRows, ColCode, ColForm1Amt)
            MainClass.ProtectCell(sprdDeduct, 1, sprdDeduct.MaxRows, ColCode, ColForm1Amt)
            MainClass.ProtectCell(sprdPerks, 1, sprdPerks.MaxRows, ColCode, ColForm1Amt)
        Else
            '                txtBSalary.Enabled = True
            MainClass.ProtectCell(sprdEarn, 1, sprdEarn.MaxRows, ColCode, ColDesc)
            MainClass.ProtectCell(sprdDeduct, 1, sprdDeduct.MaxRows, ColCode, ColDesc)
            MainClass.ProtectCell(sprdPerks, 1, sprdPerks.MaxRows, ColCode, ColDesc)
        End If
Err1:
        ErrorMsg(Err.Description, Err.Number)
    End Sub


    Private Sub ShowSalaryFromRef(ByRef xRefNo As Double)
        On Error GoTo Err1
        Dim RsADD As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim mTypeCode As Object
        Dim xSubkey As Integer
        Dim mEarnAmt As Object
        Dim mDeductAmt As Decimal
        Dim cntRow As Integer

        txtBSalary.Enabled = True
        txtForm1BSalary.Enabled = True
        txtNextIncDueDate.Enabled = True

        With sprdEarn
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = 1
                mTypeCode = Val(.Text)

                SqlStr = " SELECT * from PAY_CAND_SALARYDEF_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND REF_NO='" & xRefNo & "'" & vbCrLf & " AND ADD_DEDUCTCode=" & mTypeCode & "" & vbCrLf '                    & " AND  " & vbCrLf |'                    & " SALARY_EFF_DATE=(SELECT MAX(SALARY_EFF_DATE) From PAY_CAND_SALARYDEF_MST " & vbCrLf |'                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf |'                    & " AND REF_NO='" & xRefNo & "'" & vbCrLf |'                    & " AND SALARY_EFF_DATE<= TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "')) "

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsADD, ADODB.LockTypeEnum.adLockOptimistic)

                If Not RsADD.EOF Then
                    txtBSalary.Text = VB6.Format(RsADD.Fields("BASICSALARY").Value, "0.00")
                    txtForm1BSalary.Text = VB6.Format(RsADD.Fields("FORM1_BASICSALARY").Value, "0.00")
                    '                txtWEF.Text = Format(IIf(IsNull(RsADD!SALARY_EFF_DATE), "", RsADD!SALARY_EFF_DATE), "DD/MM/YYYY")
                    '                txtNextIncDueDate.Text = Format(IIf(IsNull(RsADD!NEXT_INC_DATE), "", RsADD!NEXT_INC_DATE), "DD/MM/YYYY")

                    If RsADD.Fields("EMP_DESG_CODE").Value <> "" Then
                        If MainClass.ValidateWithMasterTable(Trim(RsADD.Fields("EMP_DESG_CODE").Value), "DESG_CODE", "DESG_DESC", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            cbodesignation.Text = MasterNo
                        End If
                    End If

                    .Row = cntRow

                    .Col = ColPer
                    .Text = CStr(IIf(IsDbNull(RsADD.Fields("PERCENTAGE").Value), "", RsADD.Fields("PERCENTAGE").Value))

                    .Col = ColAmt
                    .Text = CStr(IIf(IsDbNull(RsADD.Fields("Amount").Value), "", RsADD.Fields("Amount").Value))

                    .Col = ColForm1Amt
                    .Text = CStr(IIf(IsDBNull(RsADD.Fields("FORM1_AMOUNT").Value), "", RsADD.Fields("FORM1_AMOUNT").Value))

                    'Col = ColAmt
                    '.Text = CStr(IIf(IsDBNull(RsADD.Fields("Amount").Value), "", RsADD.Fields("Amount").Value))

                    txtBSalary.Enabled = False
                    txtNextIncDueDate.Enabled = False
                    MainClass.ProtectCell(sprdEarn, 1, sprdEarn.MaxRows, ColCode, ColForm1Amt)
                    MainClass.ProtectCell(sprdDeduct, 1, sprdDeduct.MaxRows, ColCode, ColForm1Amt)
                Else
                    '                txtBSalary.Enabled = True
                    '                txtNextIncDueDate.Enabled = True
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

                SqlStr = " SELECT * from PAY_CAND_SALARYDEF_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND REF_NO='" & xRefNo & "'" & vbCrLf & " AND ADD_DEDUCTCode=" & mTypeCode & "" & vbCrLf '                    & " AND  " & vbCrLf |'                    & " SALARY_EFF_DATE=(SELECT MAX(SALARY_EFF_DATE) From PAY_CAND_SALARYDEF_MST " & vbCrLf |'                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf |'                    & " AND REF_NO='" & xRefNo & "'" & vbCrLf |'                    & " AND SALARY_EFF_DATE<= TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "')) "

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsADD, ADODB.LockTypeEnum.adLockOptimistic)

                If Not RsADD.EOF Then
                    txtBSalary.Text = VB6.Format(RsADD.Fields("BASICSALARY").Value, "0.00")
                    txtForm1BSalary.Text = VB6.Format(RsADD.Fields("FORM1_BASICSALARY").Value, "0.00")
                    '                txtWEF.Text = Format(IIf(IsNull(RsADD!SALARY_EFF_DATE), "", RsADD!SALARY_EFF_DATE), "DD/MM/YYYY")
                    '                txtNextIncDueDate.Text= Format(IIf(IsNull(RsADD!NEXT_INC_DATE), "", RsADD!NEXT_INC_DATE), "DD/MM/YYYY")
                    '
                    .Row = cntRow
                    .Col = ColPer
                    .Text = CStr(IIf(IsDbNull(RsADD.Fields("PERCENTAGE").Value), "", RsADD.Fields("PERCENTAGE").Value))

                    .Col = ColAmt
                    .Text = CStr(IIf(IsDbNull(RsADD.Fields("Amount").Value), "", RsADD.Fields("Amount").Value))

                    .Col = ColForm1Amt
                    .Text = CStr(IIf(IsDBNull(RsADD.Fields("FORM1_AMOUNT").Value), "", RsADD.Fields("FORM1_AMOUNT").Value))

                    txtBSalary.Enabled = False
                    txtNextIncDueDate.Enabled = False
                    MainClass.ProtectCell(sprdEarn, 1, sprdEarn.MaxRows, ColCode, ColForm1Amt)
                    MainClass.ProtectCell(sprdDeduct, 1, sprdDeduct.MaxRows, ColCode, ColForm1Amt)
                Else
                    '                txtBSalary.Enabled = True
                    '                txtNextIncDueDate.Enabled = True
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

                SqlStr = " SELECT * from PAY_CAND_SALARYDEF_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND REF_NO='" & xRefNo & "'" & vbCrLf & " AND ADD_DEDUCTCode=" & mTypeCode & "" & vbCrLf '                    & " AND  " & vbCrLf |'                    & " SALARY_EFF_DATE=(SELECT MAX(SALARY_EFF_DATE) From PAY_CAND_SALARYDEF_MST " & vbCrLf |'                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf |'                    & " AND REF_NO='" & xRefNo & "'" & vbCrLf |'                    & " AND SALARY_EFF_DATE<= TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "')) "

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsADD, ADODB.LockTypeEnum.adLockOptimistic)

                If Not RsADD.EOF Then
                    '                txtBSalary.Text = Format(RsADD!BASICSALARY, "0.00")
                    '                txtWEF.Text = Format(IIf(IsNull(RsADD!SALARY_EFF_DATE), "", RsADD!SALARY_EFF_DATE), "DD/MM/YYYY")
                    '                txtNextIncDueDate.Text = Format(IIf(IsNull(RsADD!NEXT_INC_DATE), "", RsADD!NEXT_INC_DATE), "DD/MM/YYYY")

                    .Row = cntRow
                    .Col = ColPer
                    .Text = CStr(IIf(IsDbNull(RsADD.Fields("PERCENTAGE").Value), "", RsADD.Fields("PERCENTAGE").Value))

                    .Col = ColAmt
                    .Text = CStr(IIf(IsDbNull(RsADD.Fields("Amount").Value), "", RsADD.Fields("Amount").Value))

                    .Col = ColForm1Amt
                    .Text = CStr(IIf(IsDBNull(RsADD.Fields("FORM1_AMOUNT").Value), "", RsADD.Fields("FORM1_AMOUNT").Value))

                    '                txtBSalary.Enabled = False
                    '                txtNextIncDueDate.Enabled=False
                    MainClass.ProtectCell(sprdEarn, 1, sprdEarn.MaxRows, ColCode, ColForm1Amt)
                    MainClass.ProtectCell(sprdDeduct, 1, sprdDeduct.MaxRows, ColCode, ColForm1Amt)
                    MainClass.ProtectCell(sprdPerks, 1, sprdPerks.MaxRows, ColCode, ColForm1Amt)
                Else
                    '                txtBSalary.Enabled = True
                    '               txtNextIncDueDate.Enabled=true
                    MainClass.ProtectCell(sprdEarn, 1, sprdEarn.MaxRows, ColCode, ColDesc)
                    MainClass.ProtectCell(sprdDeduct, 1, sprdDeduct.MaxRows, ColCode, ColDesc)
                    MainClass.ProtectCell(sprdPerks, 1, sprdPerks.MaxRows, ColCode, ColDesc)
                End If
            Next
        End With
Err1:
        ErrorMsg(Err.Description, Err.Number)
    End Sub
    Private Sub ShowSprdOpLeave(ByRef xCode As String)
        On Error GoTo Err1
        Dim RsLeave As ADODB.Recordset

        Dim SqlStr As String = ""
        Dim cntRow As Integer
        Dim mYear As String

        SSTab1.SelectedIndex = 4
        MainClass.ClearGrid(sprdLeaves, -1)

        FillOpLeave()

        SqlStr = " SELECT * from PAY_OPLeave_TRN " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND PAYYEAR=" & PubPAYYEAR & " " & vbCrLf & " AND EMP_CODE='" & xCode & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLeave, ADODB.LockTypeEnum.adLockOptimistic)

        With sprdLeaves
            If RsLeave.EOF = False Then
                For cntRow = 1 To .MaxRows
                    RsLeave.MoveFirst()
                    Do While Not RsLeave.EOF
                        .Row = cntRow
                        .Col = ColCode
                        If Val(.Text) = RsLeave.Fields("LeaveCode").Value Then
                            .Col = ColOpening
                            .Text = CStr(RsLeave.Fields("OPENING").Value)

                            .Col = ColTotEntitle
                            .Text = CStr(RsLeave.Fields("TOTENTITLE").Value)

                            GoTo NextRow1
                        End If
                        RsLeave.MoveNext()
                    Loop
NextRow1:
                Next
            Else
                Call ShowLeaveMaster()
            End If
        End With

        MainClass.ProtectCell(sprdLeaves, 1, sprdLeaves.MaxRows, 1, sprdLeaves.MaxCols)

        '    If lblEmpType.Caption = "O" Then
        '        MainClass.ProtectCell sprdLeaves, 1, sprdLeaves.MaxRows, 1, sprdLeaves.MaxCols
        '    End If
Err1:
        ErrorMsg(Err.Description, Err.Number)
    End Sub

    Private Sub ShowSprdSpouse(ByRef xCode As String)

        On Error GoTo ErrPart
        Dim RsSpouse As ADODB.Recordset

        Dim SqlStr As String = ""
        Dim cntRow As Integer
        Dim mRel As String
        Dim mGender As String

        MainClass.ClearGrid(sprdSpouse, -1)

        SqlStr = " SELECT * from PAY_SPOUSE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & xCode & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSpouse, ADODB.LockTypeEnum.adLockOptimistic)

        'SSTab1.SelectedIndex = 2
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
    Private Sub ShowSprdSpouseFromRef(ByRef xCode As String)

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

    Private Sub ShowSprdAssets(ByRef xCode As String)

        On Error GoTo ErrPart
        Dim RsAssets As ADODB.Recordset

        Dim SqlStr As String = ""
        Dim cntRow As Integer

        MainClass.ClearGrid(sprdAssets, -1)

        SqlStr = " SELECT * from PAY_ASSETS_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & xCode & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAssets, ADODB.LockTypeEnum.adLockOptimistic)
        'SSTab1.SelectedIndex = 3
        cntRow = 1
        With sprdAssets
            If RsAssets.EOF = False Then
                Do While RsAssets.EOF = False
                    .Row = cntRow
                    .Col = ColAssetDesc
                    .Text = IIf(IsDbNull(RsAssets.Fields("ASSETS_DESC").Value), "", RsAssets.Fields("ASSETS_DESC").Value)

                    .Col = ColAssetMake
                    .Text = IIf(IsDbNull(RsAssets.Fields("ASSETS_MAKE").Value), "", RsAssets.Fields("ASSETS_MAKE").Value)

                    .Col = ColAssetPrice
                    .Text = VB6.Format(IIf(IsDbNull(RsAssets.Fields("ASSETS_PRICE").Value), "", RsAssets.Fields("ASSETS_PRICE").Value), "0.00")

                    .Col = ColAssetDOP
                    .Text = VB6.Format(IIf(IsDbNull(RsAssets.Fields("ASSETS_DOP").Value), "", RsAssets.Fields("ASSETS_DOP").Value), "DD/MM/YYYY")

                    .Col = ColAssetDOI
                    .Text = VB6.Format(IIf(IsDbNull(RsAssets.Fields("ASSETS_DOI").Value), "", RsAssets.Fields("ASSETS_DOI").Value), "DD/MM/YYYY")

                    .Col = ColAssetRemarks
                    .Text = IIf(IsDbNull(RsAssets.Fields("ASSETS_REMARKS").Value), "", RsAssets.Fields("ASSETS_REMARKS").Value)

                    RsAssets.MoveNext()
                    cntRow = cntRow + 1
                    .MaxRows = .MaxRows + 1

                Loop
            End If
        End With
        FormatSprdAssets(-1)
        Exit Sub
ErrPart:

    End Sub
    Private Sub ShowLeaveMaster()

        Dim RsLeave As ADODB.Recordset

        Dim SqlStr As String = ""
        Dim cntRow As Integer
        Dim mYear As String
        Dim mEntitle As Double
        Dim mBalMonth As Double

        MainClass.ClearGrid(sprdLeaves, -1)

        FillOpLeave()
        mYear = CStr(Year(RunDate))

        SqlStr = " SELECT * from PAY_LEAVEDTL_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND PAYYEAR=" & PubPAYYEAR & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsLeave, ADODB.LockTypeEnum.adLockOptimistic)

        With sprdLeaves
            If RsLeave.EOF = False Then
                For cntRow = 1 To .MaxRows
                    RsLeave.MoveFirst()
                    Do While Not RsLeave.EOF
                        .Row = cntRow
                        .Col = ColCode
                        If Val(.Text) = RsLeave.Fields("LeaveCode").Value Then
                            .Col = ColOpening
                            .Text = "" ''CStr(RsLeave!OPENING)

                            If VB.Left(cboEmpCatType.Text, 1) = "1" Then
                                If RsLeave.Fields("TOTENTITLE_WRKS").Value = 0 Or IsDbNull(RsLeave.Fields("TOTENTITLE_WRKS").Value) Then
                                    mEntitle = IIf(IsDbNull(RsLeave.Fields("TOTENTITLE").Value), 0, RsLeave.Fields("TOTENTITLE").Value)
                                Else
                                    mEntitle = IIf(IsDbNull(RsLeave.Fields("TOTENTITLE_WRKS").Value), 0, RsLeave.Fields("TOTENTITLE_WRKS").Value)
                                End If
                            Else
                                mEntitle = IIf(IsDbNull(RsLeave.Fields("TOTENTITLE").Value), 0, RsLeave.Fields("TOTENTITLE").Value)
                            End If

                            If IsDate(txtDOJ.Text) Then
                                If Year(CDate(txtDOJ.Text)) = CDbl(PubPAYYEAR) Then
                                    If VB.Day(CDate(txtDOJ.Text)) <= 15 Then
                                        mBalMonth = 12 - Month(CDate(txtDOJ.Text)) + 1
                                    Else
                                        mBalMonth = 12 - Month(CDate(txtDOJ.Text))
                                    End If

                                    mEntitle = mEntitle / 12 * mBalMonth

                                    mEntitle = System.Math.Round(mEntitle / 0.5, 0) * 0.5
                                End If
                            End If

                            .Col = ColTotEntitle
                            .Text = CStr(mEntitle)

                            GoTo NextRow1
                        End If
                        RsLeave.MoveNext()
                    Loop
NextRow1:
                Next
            End If
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
            .set_ColWidth(ColDesc, 25)

            .Col = ColDeductOn
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColPer, 8)
            .ColHidden = True

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
            .set_ColWidth(ColAmt, 10)

            .Col = ColForm1Amt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .ColHidden = IIf(RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "N", True, False)

            .set_ColWidth(ColForm1Amt, 10)

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
            .set_ColWidth(ColDesc, 20)

            .Col = ColDeductOn
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColPer, 8)
            .ColHidden = False

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
            .set_ColWidth(ColAmt, 9)

            .Col = ColForm1Amt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .ColHidden = IIf(RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "N", True, False)
            .set_ColWidth(ColForm1Amt, 9)

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
            .set_ColWidth(ColDesc, 25)

            .Col = ColDeductOn
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColPer, 8)
            .ColHidden = True

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
            .set_ColWidth(ColAmt, 10)

            .Col = ColForm1Amt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .ColHidden = IIf(RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "N", True, False)
            .set_ColWidth(ColForm1Amt, 10)
        End With

        MainClass.ProtectCell(sprdPerks, 1, sprdPerks.MaxRows, ColCode, ColPer)
        MainClass.SetSpreadColor(sprdPerks, mRow)

        With sprdLeaves
            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight * 2)

            .Col = ColCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColCode, 10)
            .ColHidden = True

            .Col = ColDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(ColDesc, 25)

            .Col = ColOpening
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 1
            .TypeFloatMax = CDbl("9999999")
            .TypeFloatMin = CDbl("-9999999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColOpening, 12)

            .Col = ColTotEntitle
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 1
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColTotEntitle, 15)

            FillOpLeave()
        End With

        MainClass.ProtectCell(sprdLeaves, 1, sprdLeaves.MaxRows, ColCode, ColDesc)
        MainClass.SetSpreadColor(sprdLeaves, mRow)

        FormatSprdSpouse(-1)
        FormatSprdAssets(-1)


        MainClass.SetSpreadColor(sprdAssets, mRow)


        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub

    Private Sub FormatSprdAssets(ByRef mRow As Integer)

        On Error GoTo ERR1

        With sprdAssets

            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight * 1.5)

            .Col = ColAssetDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColAssetDesc, 19)
            .TypeEditLen = MainClass.SetMaxLength("ASSETS_DESC", "PAY_ASSETS_MST", PubDBCn)

            .Col = ColAssetMake
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColAssetMake, 19)
            .TypeEditLen = MainClass.SetMaxLength("ASSETS_MAKE", "PAY_ASSETS_MST", PubDBCn)

            .Col = ColAssetPrice
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColAssetPrice, 10)

            .Col = ColAssetDOP
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeDateFormat = SS_CELL_DATE_FORMAT_DDMMYY
            .TypeDateCentury = True
            .set_ColWidth(ColAssetDOP, 9)

            .Col = ColAssetDOI
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeDateFormat = SS_CELL_DATE_FORMAT_DDMMYY
            .TypeDateCentury = True
            .set_ColWidth(ColAssetDOI, 9)

            .Col = ColAssetRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColAssetRemarks, 18)
            .TypeEditLen = MainClass.SetMaxLength("ASSETS_REMARKS", "PAY_ASSETS_MST", PubDBCn)
        End With

        MainClass.SetSpreadColor(sprdAssets, mRow)


        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
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
            .TypeEditLen = MainClass.SetMaxLength("SPOUSE_NAME", "PAY_SPOUSE_MST", PubDBCn)

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
            .TypeEditLen = MainClass.SetMaxLength("BLOOD_GROUP", "PAY_SPOUSE_MST", PubDBCn)

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
    Private Function UpdateSalaryDef(ByRef xCode As String, ByRef xWEF As String, ByRef xSalary As Double, ByRef xForm1Salary As Double, ByRef mDesgCode As String) As Boolean
        On Error GoTo UpdateSalaryDefErr
        Dim SqlStr As String = ""
        Dim xTypeCode As Object
        Dim cntRow As Integer
        Dim xAmount As Double
        Dim xPer As Decimal
        Dim xDeductOnAmount As Double
        Dim mNextIncDue As String
        Dim xForm1Amount As Double
        Dim mForm1Salary As Double
        Dim EmpPFCont As String


        If IsDate(txtNextIncDueDate.Text) = True Then
            mNextIncDue = VB6.Format(txtNextIncDueDate.Text, "DD/MM/YYYY")
            '    Else
            '        mNextIncDue = Format(txtDOP.Text, "DD/MM/YYYY")
        End If

        SqlStr = " DELETE FROM PAY_SalaryDef_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND EMP_CODE='" & xCode & "'" & vbCrLf _
            & " AND SALARY_EFF_DATE=TO_DATE('" & VB6.Format(xWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        PubDBCn.Execute(SqlStr)

        mForm1Salary = xForm1Salary

        'If Val(xForm1Salary) = 0 Then
        '    xForm1Salary = xSalary
        'End If

        If RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "N" Then
            xForm1Salary = xSalary
        Else
            If Val(xForm1Salary) = 0 Then
                xForm1Salary = xSalary
            End If
        End If

        SqlStr = ""

        EmpPFCont = IIf(optContBasic.Checked = True, "B", IIf(optContGross.Checked = True, "G", IIf(optContCeiling.Checked = True, "C", "E")))

        With sprdEarn
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColCode
                If Trim(.Text) = "" Then GoTo NextEarnRow
                xTypeCode = Val(.Text)

                .Col = ColPer
                xPer = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColDeductOn
                xDeductOnAmount = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColAmt
                xAmount = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColForm1Amt
                xForm1Amount = IIf(IsNumeric(.Text), .Text, 0)

                If RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "N" Then
                    xForm1Amount = xAmount
                Else
                    If mForm1Salary = 0 Then
                        xForm1Amount = xAmount
                    End If
                End If


                If xTypeCode > 0 Then
                    SqlStr = " Insert Into PAY_SalaryDef_MST (COMPANY_CODE, FYEAR, " & vbCrLf _
                        & " EMP_CODE, SALARY_EFF_DATE, BASICSALARY, FORM1_BASICSALARY," & vbCrLf _
                        & " ADD_DEDUCTCODE, PERCENTAGE, AMOUNT, FORM1_AMOUNT," & vbCrLf _
                        & " SALARY_APP_DATE, PREVIOUS_BASICSALARY,PREVIOUS_FORM1_BASICSALARY,PREVIOUS_AMOUNT,PREVIOUS_FORM1_AMOUNT," & vbCrLf _
                        & " ARREAR_DATE,TOT_ARR_MONTH,IS_ARREAR,EMP_DESG_CODE, " & vbCrLf _
                        & " ADDUSER, ADDDATE,NEXT_INC_DATE, EMP_CONT, AMOUNT_DEDUCT_ON " & vbCrLf _
                        & " ) VALUES " & vbCrLf _
                        & " (" & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                        & " " & RsCompany.Fields("FYEAR").Value & ",'" & xCode & "', TO_DATE('" & VB6.Format(xWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                        & " " & xSalary & ", " & xForm1Salary & ", " & xTypeCode & "," & xPer & "," & xAmount & ", " & xForm1Amount & "," & vbCrLf _
                        & " TO_DATE('" & VB6.Format(xWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & xSalary & ", " & xForm1Salary & "," & vbCrLf _
                        & " " & xAmount & "," & xForm1Amount & ",'',0,'N','" & mDesgCode & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                        & " TO_DATE('" & VB6.Format(mNextIncDue, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & EmpPFCont & "'," & xDeductOnAmount & ")"

                    PubDBCn.Execute(SqlStr)
                End If

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

                .Col = ColDeductOn
                xDeductOnAmount = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColPer
                xPer = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColAmt
                xAmount = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColForm1Amt
                xForm1Amount = IIf(IsNumeric(.Text), .Text, 0)

                If RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "N" Then
                    xForm1Amount = xAmount
                Else
                    If mForm1Salary = 0 Then
                        xForm1Amount = xAmount
                    End If
                End If

                If xTypeCode > 0 Then
                    SqlStr = " Insert Into PAY_SalaryDef_MST (COMPANY_CODE, FYEAR, " & vbCrLf _
                        & " EMP_CODE, SALARY_EFF_DATE, BASICSALARY, FORM1_BASICSALARY, " & vbCrLf _
                        & " ADD_DEDUCTCODE, PERCENTAGE, AMOUNT,  FORM1_AMOUNT," & vbCrLf _
                        & " SALARY_APP_DATE, PREVIOUS_BASICSALARY,PREVIOUS_FORM1_BASICSALARY,PREVIOUS_AMOUNT,PREVIOUS_FORM1_AMOUNT," & vbCrLf _
                        & " ARREAR_DATE,TOT_ARR_MONTH,IS_ARREAR,EMP_DESG_CODE, " & vbCrLf _
                        & " ADDUSER, ADDDATE,NEXT_INC_DATE,EMP_CONT, AMOUNT_DEDUCT_ON " & vbCrLf _
                        & " ) VALUES " & vbCrLf _
                        & " (" & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                        & " " & RsCompany.Fields("FYEAR").Value & ",'" & xCode & "', TO_DATE('" & VB6.Format(xWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                        & " " & xSalary & "," & xForm1Salary & "," & xTypeCode & "," & xPer & "," & xAmount & "," & xForm1Amount & "," & vbCrLf _
                        & " TO_DATE('" & VB6.Format(xWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & xSalary & "," & xForm1Salary & "," & vbCrLf _
                        & " " & xAmount & "," & xForm1Amount & ",'',0,'N','" & mDesgCode & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                        & " TO_DATE('" & VB6.Format(mNextIncDue, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & EmpPFCont & "'," & xDeductOnAmount & ")"

                    PubDBCn.Execute(SqlStr)
                End If

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

                .Col = ColDeductOn
                xDeductOnAmount = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColPer
                xPer = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColAmt
                xAmount = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColForm1Amt
                xForm1Amount = IIf(IsNumeric(.Text), .Text, 0)

                If RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "N" Then
                    xForm1Amount = xAmount
                Else
                    If mForm1Salary = 0 Then
                        xForm1Amount = xAmount
                    End If
                End If

                If xTypeCode > 0 Then
                    SqlStr = " Insert Into PAY_SalaryDef_MST (COMPANY_CODE, FYEAR, " & vbCrLf _
                        & " EMP_CODE, SALARY_EFF_DATE, BASICSALARY, FORM1_BASICSALARY, " & vbCrLf _
                        & " ADD_DEDUCTCODE, PERCENTAGE, AMOUNT, FORM1_AMOUNT," & vbCrLf _
                        & " SALARY_APP_DATE, PREVIOUS_BASICSALARY,PREVIOUS_FORM1_BASICSALARY,PREVIOUS_AMOUNT,PREVIOUS_FORM1_AMOUNT," & vbCrLf _
                        & " ARREAR_DATE,TOT_ARR_MONTH,IS_ARREAR,EMP_DESG_CODE, " & vbCrLf _
                        & " ADDUSER, ADDDATE,NEXT_INC_DATE,EMP_CONT, AMOUNT_DEDUCT_ON " & vbCrLf _
                        & " ) VALUES " & vbCrLf _
                        & " (" & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                        & " " & RsCompany.Fields("FYEAR").Value & ",'" & xCode & "', TO_DATE('" & VB6.Format(xWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                        & " " & xSalary & "," & xForm1Salary & "," & xTypeCode & "," & xPer & "," & xAmount & "," & xForm1Amount & "," & vbCrLf _
                        & " TO_DATE('" & VB6.Format(xWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & xSalary & "," & xForm1Salary & "," & vbCrLf _
                        & " " & xAmount & "," & xForm1Amount & ",'',0,'N','" & mDesgCode & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                        & " TO_DATE('" & VB6.Format(mNextIncDue, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & EmpPFCont & "'," & xDeductOnAmount & ")"

                    PubDBCn.Execute(SqlStr)
                End If


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
    Private Function UpdateOPLeave(ByRef xCode As String) As Boolean
        On Error GoTo UpdateLoanErr

        Dim SqlStr As String = ""
        Dim mYear As Integer
        Dim xOpCode As Integer
        Dim xOpening As Double
        Dim xTOTENTITLE As Double
        Dim cntRow As Integer

        UpdateOPLeave = True

        SqlStr = " DELETE FROM PAY_OPLeave_TRN WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PAYYEAR=" & PubPAYYEAR & " " & vbCrLf & " AND EMP_CODE='" & xCode & "' "

        PubDBCn.Execute(SqlStr)

        SqlStr = ""

        With sprdLeaves
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColCode
                xOpCode = Val(.Text)

                .Col = ColOpening
                xOpening = Val(.Text)

                .Col = ColTotEntitle
                xTOTENTITLE = IIf(IsNumeric(.Text), .Text, 0)

                '            If xTOTENTITLE <> 0 Then
                SqlStr = " Insert Into PAY_OPLeave_TRN (COMPANY_CODE, PAYYEAR, " & vbCrLf _
                    & " EMP_CODE, LEAVECODE, OPENING, " & vbCrLf _
                    & " TOTENTITLE) VALUES " & vbCrLf _
                    & " (" & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                    & " " & PubPAYYEAR & ", " & vbCrLf _
                    & " '" & xCode & "', " & vbCrLf _
                    & " " & xOpCode & "," & xOpening & "," & xTOTENTITLE & ") "

                PubDBCn.Execute(SqlStr)
                '           End If
            Next
        End With

        Exit Function
UpdateLoanErr:
        MsgBox(Err.Description)
        UpdateOPLeave = False
    End Function

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

        SqlStr = " DELETE FROM PAY_SPOUSE_MST WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & xCode & "' "

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
                    SqlStr = " Insert Into PAY_SPOUSE_MST ( " & vbCrLf & " COMPANY_CODE, EMP_CODE," & vbCrLf & " SPOUSE_NAME, SPOUSE_REL, " & vbCrLf & " SPOUSE_GENDER, SPOUSE_DOB, BLOOD_GROUP" & vbCrLf & " ) VALUES " & vbCrLf & " (" & RsCompany.Fields("COMPANY_CODE").Value & ", '" & xCode & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(xName) & "', '" & MainClass.AllowSingleQuote(xRel) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(xGender) & "',TO_DATE('" & VB6.Format(xSpouseDOB, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(xBloodGroup) & "' " & vbCrLf & " ) "
                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With

        Exit Function
UpdateLoanErr:
        MsgBox(Err.Description)
        UpdateSpouse = False
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

    Private Sub FillOpLeave()

        Dim cntCol As Integer
        Dim mDate As Date

        SSTab1.SelectedIndex = 5

        With sprdLeaves
            .MaxRows = 4
            .Row = 0

            .Row = 1
            .set_RowHeight(1, ConRowHeight * 1.15)
            .Col = ColCode
            .Text = CStr(EARN)

            .Col = ColDesc
            .Text = "EARN"

            .Row = 2
            .set_RowHeight(2, ConRowHeight * 1.15)
            .Col = ColCode
            .Text = CStr(SICK)

            .Col = ColDesc
            .Text = "SICK"

            .Row = 3
            .set_RowHeight(3, ConRowHeight * 1.15)
            .Col = ColCode
            .Text = CStr(CASUAL)

            .Col = ColDesc
            .Text = "CASUAL"

            .Row = 4
            .set_RowHeight(4, ConRowHeight * 1.15)
            .Col = ColCode
            .Text = CStr(CPLEARN)

            .Col = ColDesc
            .Text = "CPLEARN"

            .Row = 5
            .set_RowHeight(5, ConRowHeight * 1.15)
            .Col = ColCode
            .Text = CStr(MATERNITY)

            .Col = ColDesc
            .Text = "MATERNITY"

        End With
        MainClass.ProtectCell(sprdLeaves, 1, sprdLeaves.MaxRows, ColCode, ColDesc)
    End Sub


    Private Function CalcBasicPFSalary(ByRef mType As Integer) As Double
        Dim cntRow As Integer
        Dim mCode As Integer
        Dim mPFCeiling As Double

        CalcBasicPFSalary = IIf(IsNumeric(txtBSalary.Text), txtBSalary.Text, 0)
        With sprdEarn
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColCode
                mCode = CInt(.Text)
                If mType = ConPF Or mType = ConVPFAllw Or mType = ConEmployerPF Then
                    If optContCeiling.Checked = True Or optContGross.Checked = True Then
                        If MainClass.ValidateWithMasterTable(mCode, "Code", "IncludedPF", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            If MasterNo = "Y" Then
                                .Col = ColAmt
                                CalcBasicPFSalary = CalcBasicPFSalary + IIf(IsNumeric(.Text), .Text, 0)
                            End If
                        End If
                    End If
                ElseIf mType = ConESI Or mType = ConEmployerESI Then
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

            If CheckPFCeilingOn(txtWEF.Text) = "C" Then
                mPFCeiling = CheckPFCeiling(txtWEF.Text)
            Else
                mPFCeiling = CalcBasicPFSalary
            End If

            CalcBasicPFSalary = IIf(CalcBasicPFSalary >= mPFCeiling, mPFCeiling, CalcBasicPFSalary)
        End If

    End Function

    Public Sub CalcPFESI(Optional ByRef mESIAmount As Double = 0)
        On Error GoTo ErrPart
        Dim xPer As Double
        Dim mcntRow As Integer
        Dim mCode As Integer
        Dim mType As Integer
        Dim mBasicSal As Double
        Dim mPFCeiling As Double
        Dim mPFAmount As Double
        Dim mDeductOn As Double

        For mcntRow = 1 To sprdDeduct.MaxRows
            sprdDeduct.Row = mcntRow

            sprdDeduct.Col = ColCode
            If sprdDeduct.Text = "" Then Exit Sub
            mCode = CInt(sprdDeduct.Text)
            If MainClass.ValidateWithMasterTable(mCode, "Code", "Type", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mType = MasterNo
            End If

            sprdDeduct.Col = ColDeductOn
            If mType = ConPF Then
                If optContBasic.Checked = True Then
                    sprdDeduct.Text = Val(txtBSalary.Text)
                ElseIf optContGross.Checked = True Then
                    sprdDeduct.Text = CStr(CalcBasicPFSalary(mType))
                ElseIf optContCeilingGross.Checked = True Then
                    mBasicSal = CalcBasicPFSalary(mType)
                    mPFCeiling = CheckPFCeiling(CDate(txtWEF.Text))
                    mBasicSal = IIf(mBasicSal > mPFCeiling, mPFCeiling, mBasicSal)

                    sprdDeduct.Text = CStr(mBasicSal)
                Else
                    If Trim(txtWEF.Text) <> "" Then
                        mBasicSal = Val(txtBSalary.Text)       'CalcBasicPFSalary(mType)
                        mPFCeiling = CheckPFCeiling(CDate(txtWEF.Text))
                        mBasicSal = IIf(mBasicSal > mPFCeiling, mPFCeiling, mBasicSal)

                        sprdDeduct.Text = CStr(mBasicSal)
                    End If
                End If

                mDeductOn = Val(sprdDeduct.Text)
            Else
                sprdDeduct.Text = CStr(CalcBasicPFSalary(mType))
                mDeductOn = Val(sprdDeduct.Text)
            End If

            sprdDeduct.Col = ColPer
            xPer = IIf(IsNumeric(sprdDeduct.Text), sprdDeduct.Text, 0)

            sprdDeduct.Col = ColAmt
            If xPer <> 0 Then
                If mType = ConPF Then
                    'If optContBasic.Checked = True Then
                    '    sprdDeduct.Text = CStr(xPer * Val(txtBSalary.Text) / 100)
                    'ElseIf optContGross.Checked = True Then
                    '    sprdDeduct.Text = CStr(xPer * CalcBasicPFSalary(mType) / 100)
                    'ElseIf optContCeilingGross.Checked = True Then
                    '    mBasicSal = CalcBasicPFSalary(mType)
                    '    mPFCeiling = CheckPFCeiling(CDate(txtWEF.Text))
                    '    mBasicSal = IIf(mBasicSal > mPFCeiling, mPFCeiling, mBasicSal)

                    '    sprdDeduct.Text = CStr(xPer * mBasicSal / 100)
                    'Else
                    '    If Trim(txtWEF.Text) <> "" Then
                    '        mBasicSal = Val(txtBSalary.Text)       'CalcBasicPFSalary(mType)
                    '        mPFCeiling = CheckPFCeiling(CDate(txtWEF.Text))
                    '        mBasicSal = IIf(mBasicSal > mPFCeiling, mPFCeiling, mBasicSal)

                    '        sprdDeduct.Text = CStr(xPer * mBasicSal / 100)
                    '    End If
                    'End If

                    mPFAmount = mDeductOn * xPer / 100 '' Val(sprdDeduct.Text)
                    sprdDeduct.Text = mPFAmount

                    sprdDeduct.Col = ColForm1Amt
                    sprdDeduct.Text = mPFAmount
                Else
                    sprdDeduct.Text = CStr(xPer * CalcBasicPFSalary(mType) / 100)
                End If
                'sprdDeduct.Text = CStr(xPer * CalcBasicPFSalary(mType) / 100)
            End If

            If mType = ConESI Then
                mESIAmount = IIf(IsNumeric(sprdDeduct.Text), sprdDeduct.Text, 0)
            End If

            sprdDeduct.Col = ColForm1Amt
            If xPer <> 0 Then
                sprdDeduct.Text = CStr(xPer * CalcBasicPFSalary(mType) / 100)
            End If

        Next

        For mcntRow = 1 To sprdPerks.MaxRows
            sprdPerks.Row = mcntRow

            sprdPerks.Col = ColCode
            If sprdPerks.Text = "" Then Exit Sub
            mCode = CInt(sprdPerks.Text)
            If MainClass.ValidateWithMasterTable(mCode, "Code", "Type", "PAY_SALARYHEAD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mType = MasterNo
            End If
            sprdPerks.Col = ColPer
            xPer = IIf(IsNumeric(sprdPerks.Text), sprdPerks.Text, 0)

            sprdPerks.Col = ColAmt
            If xPer <> 0 Then
                sprdPerks.Text = CStr(xPer * CalcBasicPFSalary(mType) / 100)
            End If

            sprdPerks.Col = ColForm1Amt
            If xPer <> 0 Then
                sprdPerks.Text = CStr(xPer * CalcBasicPFSalary(mType) / 100)
            End If
        Next
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description)
    End Sub

    Private Function CheckSalary(ByRef xCode As String) As Boolean

        On Error GoTo ErrCheckSalary
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        CheckSalary = True
        If ADDMode = True Or Trim(txtWEF.Text) = "" Then
            CheckSalary = False
            Exit Function
        End If
        If CDate(txtDOJ.Text) = CDate(txtWEF.Text) Then
            CheckSalary = False
            Exit Function

        End If

        SqlStr = " SELECT * FROM PAY_SalaryDef_MST WHERE " & vbCrLf _
            & " COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf _
            & " EMP_CODE = '" & xCode & "'"

        '& vbCrLf _
        '    & " AND SALARY_EFF_DATE = TO_DATE('" & VB6.Format(txtDOJ.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

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

    Private Sub txtUIDNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtUIDNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtUIDNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtUIDNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtWorkingHours_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWorkingHours.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtWorkingHours_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtWorkingHours.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtWorkingFrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWorkingFrom.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtWorkingFrom_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtWorkingFrom.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtWorkingTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWorkingTo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtWorkingTo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtWorkingTo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtImprestAcName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtImprestAcName.DoubleClick
        CallSearchImpAccount()
    End Sub


    Private Sub txtImprestAcName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtImprestAcName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtImprestAcName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtImprestAcName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtImprestAcName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            CallSearchImpAccount()
        End If
    End Sub

    Private Sub txtImprestAcName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtImprestAcName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1

        If Trim(txtImprestAcName.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtImprestAcName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invalid Account Name")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub CallSearchImpAccount()
        On Error GoTo ErrPart
        SqlStr = ""

        If MainClass.SearchGridMaster((txtImprestAcName.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='O'") = True Then
            txtImprestAcName.Text = AcName
            txtImprestAcName_Validating(txtImprestAcName, New System.ComponentModel.CancelEventArgs(False))
            If txtImprestAcName.Enabled = True Then txtImprestAcName.Focus()
        End If

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub CallSearchAccount()
        On Error GoTo ErrPart
        SqlStr = ""

        If MainClass.SearchGridMaster((txtLoanAcName.Text), "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='O'") = True Then
            txtLoanAcName.Text = AcName
            txtLoanAcName_Validating(txtLoanAcName, New System.ComponentModel.CancelEventArgs(False))
            If txtLoanAcName.Enabled = True Then txtLoanAcName.Focus()
        End If

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchBankMaster()
        On Error GoTo ErrPart
        SqlStr = ""

        If MainClass.SearchGridMaster((txtBankName.Text), "PAY_BANK_MST", "BANK_NAME", "", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtBankName.Text = AcName
            txtBankName_Validating(txtBankName, New System.ComponentModel.CancelEventArgs(False))
        End If

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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
    Private Sub cboMachineName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboMachineName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboMachineName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles cboMachineName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, cboMachineName.Text)  '' MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If

    End Sub


End Class
