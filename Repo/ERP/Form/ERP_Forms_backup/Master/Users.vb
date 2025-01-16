Option Strict Off
Option Explicit On
Imports System
Imports System.Windows.Forms
'Imports VB = Microsoft.VisualBasic
'Imports Microsoft.VisualBasic.Compatibility.VB6
Imports Microsoft.VisualBasic.Compatibility
'Imports Microsoft.VisualBasic.Compatibility.Data
'Imports ADODC = VB6.ADODC
Imports System.Data.SqlClient   '' System.Data.OleDb
Imports System.Data.OleDb
Imports ADODB
Imports AxFPSpreadADO

Public Class frmUsers
    Dim RsUser As Recordset
    Dim ADDMode As Boolean = False
    Dim MODIFYMode As Boolean = False
    Dim XRIGHT As String
    Dim FormActive As Boolean
    Dim RsCompanyRights As ADODB.Recordset

    Private Const ColCompanyCode As Short = 1
    Private Const ColCompanyName As Short = 2
    Private Const ColCanWork As Short = 3

    Private Const ConRowHeight As Short = 13

    Private Sub FormatSprdMain(ByRef Arow As Integer)


        On Error GoTo ErrPart

        With SprdMain
            .set_RowHeight(Arow, ConRowHeight)
            .Row = Arow

            .Col = ColCompanyCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditLen = 60
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(.Col, 6)

            .Col = ColCompanyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditLen = 60
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(.Col, 45)

            .Col = ColCanWork
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(.Col, 8.5)
            .Value = CStr(System.Windows.Forms.CheckState.Unchecked)

            MainClass.ProtectCell(SprdMain, 1, .MaxRows, ColCompanyCode, ColCompanyName)
            MainClass.SetSpreadColor(SprdMain, 1)
        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub Clear1()


        OptStatOpen.Checked = True
        OptStatClosed.Checked = False
        optMenuRights.Checked = False
        cboLevel.SelectedIndex = -1
        chkRunDate.Checked = False

        chkDS.Checked = False
        chkInvoiceAdmin.Checked = False
        chkBookLocking.Checked = False
        chkAllow_AccountMaster.Checked = False
        chkAllow_BopPo.Checked = False
        chkAllow_RmPo.Checked = False
        chkAllow_PoprintApp.Checked = False
        chkAllow_StockAdj.Checked = False
        chkPay_CorpUser.Checked = False
        chkInv_LevelUser.Checked = False
        chkInv_Level_AppUser.Checked = False
        chkAllow_ExcessIssue.Checked = False
        chkDigitalSign.Checked = False

        txtEmpCode.Text = ""
        txtEquivalent.Text = ""
        txtEquivalentName.Text = ""
        txtName.Text = ""
        txtpassword.Text = ""
        txtAdminPassword.Text = ""
        txtUserID.Text = ""

        txtIPAddress.Text = ""
        txtWindowUser.Text = ""
        txteMailId.Text = ""


        txtDigitalSignUID.Text = ""
        txtDigitalSignPassword.Text = ""
        txtDSCertificateNo.Text = ""
        txtDLLPathName.Text = ""
        txtDLLFileName.Text = ""


        cboUserType.SelectedIndex = -1
        optModuleRights.Checked = False
        optBranchRights.Checked = False
        optDeptRights.Checked = False

        Me.txtUserID.CharacterCasing = CharacterCasing.Upper
        Me.txtEmpCode.CharacterCasing = CharacterCasing.Upper

        MainClass.ClearGrid(SprdMain)
        FormatSprdMain(-1)

        MainClass.ClearGrid(sprdView, 10)
        Call FillCompany()

        MainClass.ButtonStatus(Me, XRIGHT, RsUser, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click
        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            txtUserID.Enabled = False
            '' MainClass.ButtonStatus(Me, XRIGHT, RsUser, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            MainClass.ButtonStatus(Me, XRIGHT, RsUser, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
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
            txtUserID_Validating(txtUserID, New System.ComponentModel.CancelEventArgs(False))
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
    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles sprdView.DblClick
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        sprdView.Col = 1
        sprdView.Row = sprdView.ActiveRow
        txtUserID.Text = sprdView.Text
        txtUserID_Validating(txtUserID, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Private Sub ViewGrid()
        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(sprdView)
            AssignGrid(True)
            '        ADataMain.Refresh
            FormatSprdView()
            'SprdView.Refresh()

            sprdView.Focus()
            sprdView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            sprdView.SendToBack()
        End If
        ''MainClass.ButtonStatus(Me, XRIGHT, RsUser, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        MainClass.ButtonStatus(Me, XRIGHT, RsUser, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtUserID.Focus()
        Else
            ADDMode = False
            MODIFYMode = False
            If RsUser.EOF = False Then RsUser.MoveFirst()
            Show1()
        End If
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        ''frmSearchGrid.DefInstance.Close()
        Me.Hide()
        Me.Dispose()
        Me.Close()
        'FormMain.ch
        ''ChildForm.Close()
        ''me.hide ''me.hide 
        ''frmUsers.Close()
        'frmUsers.DefInstance = Nothing
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If txtUserID.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub
        If Not RsUser.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then
                If Delete1() = False Then GoTo DelErrPart
                If RsUser.EOF = True Then
                    Clear1()
                Else
                    Show1()
                End If
            End If
        End If
        Exit Sub
DelErrPart:
        MsgBox("Record Not Deleted")
    End Sub
    Private Sub frmUsers_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        RsCompanyRights = Nothing
        Me.Hide()
        Me.Dispose()
        Me.Close()  ''me.hide ''me.hide 
    End Sub
    Private Sub frmUsers_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'PvtDBCn.Open StrConn

        Call SetMainFormCordinate(Me)
        XRIGHT = MainClass.STRMenuRight(PubUserID, 1, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        ADDMode = False
        MODIFYMode = False
        FormActive = False

        'OptRights(0).Checked = False
        'OptRights(1).Checked = False

        'SqlStr = ""
        MainClass.UOpenRecordSet("Select * From ATH_PASSWORD_MST Where 1<>1", PubDBCn, CursorTypeEnum.adOpenStatic, RsUser, LockTypeEnum.adLockReadOnly)
        'Call AssignGrid(False)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call settextlength()

        cboUserType.Items.Clear()
        cboUserType.Items.Add("SUPER")
        cboUserType.Items.Add("USER")
        cboUserType.Items.Add("GUEST")
        cboUserType.Items.Add("ADMIN")
        cboUserType.SelectedIndex = -1

        cboLevel.Items.Clear()
        cboLevel.Items.Add("1. H.O.D.")
        cboLevel.Items.Add("2. Grade III")
        cboLevel.Items.Add("3. Grade II")
        cboLevel.Items.Add("4. Grade I")
        cboLevel.SelectedIndex = -1

        Call Clear1()
        USerIdSearch()
        Call frmUsers_Activated(eventSender, eventArgs)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        ''Resume
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub
    Private Sub Show1()
        On Error GoTo Errhand1
        Dim mLevel As Integer
        Dim mStatus As String = ""
        Dim mUserType As String
        Dim mUserNewPassWord As String
        Dim mUserEncryptPassWord As String

        Dim mAdminNewPassWord As String
        Dim mAdminEncryptPassWord As String

        If Not RsUser.EOF Then
            txtUserID.Text = IIf(IsDBNull(RsUser.Fields("USER_ID").Value), "", RsUser.Fields("USER_ID").Value)

            txtEmpCode.Text = IIf(IsDBNull(RsUser.Fields("USER_CODE").Value), "", RsUser.Fields("USER_CODE").Value)
            txtName.Text = IIf(IsDBNull(RsUser.Fields("EMP_NAME").Value), "", RsUser.Fields("EMP_NAME").Value)

            mUserNewPassWord = IIf(IsDBNull(RsUser.Fields("NEWPASSWORD").Value), "", RsUser.Fields("NEWPASSWORD").Value)
            mUserEncryptPassWord = CryptRC4(FromHexDump(mUserNewPassWord), "password") ' ToHexDump(CryptRC4(UCase(mUserNewPassWord), "password")

            txtpassword.Text = mUserEncryptPassWord

            mAdminNewPassWord = IIf(IsDBNull(RsUser.Fields("ADMIN_PASSWORD").Value), "", RsUser.Fields("ADMIN_PASSWORD").Value)
            mAdminEncryptPassWord = CryptRC4(FromHexDump(mAdminNewPassWord), "password") ' ToHexDump(CryptRC4(UCase(mUserNewPassWord), "password")
            txtAdminPassword.Text = mAdminEncryptPassWord   '' IIf(IsDBNull(RsUser.Fields("ADMIN_PASSWORD").Value), "", RsUser.Fields("ADMIN_PASSWORD").Value)
            txtIPAddress.Text = IIf(IsDBNull(RsUser.Fields("USER_IP").Value), "", RsUser.Fields("USER_IP").Value)

            txtWindowUser.Text = IIf(IsDBNull(RsUser.Fields("WINDOW_USER").Value), "", RsUser.Fields("WINDOW_USER").Value)
            txteMailId.Text = IIf(IsDBNull(RsUser.Fields("EMAIL").Value), "", RsUser.Fields("EMAIL").Value)


            txtDigitalSignUID.Text = IIf(IsDBNull(RsUser.Fields("DS_USERID").Value), "", RsUser.Fields("DS_USERID").Value)
            txtDigitalSignPassword.Text = IIf(IsDBNull(RsUser.Fields("DS_PASSWORD").Value), "", RsUser.Fields("DS_PASSWORD").Value)
            txtDSCertificateNo.Text = IIf(IsDBNull(RsUser.Fields("DS_CERTIFICATE_SNO").Value), "", RsUser.Fields("DS_CERTIFICATE_SNO").Value)
            txtDLLPathName.Text = IIf(IsDBNull(RsUser.Fields("DS_DLL_PATH").Value), "", RsUser.Fields("DS_DLL_PATH").Value)
            txtDLLFileName.Text = IIf(IsDBNull(RsUser.Fields("DS_DLL_FILENAME").Value), "", RsUser.Fields("DS_DLL_FILENAME").Value)



            mStatus = IIf(IsDBNull(RsUser.Fields("STATUS").Value), "C", RsUser.Fields("STATUS").Value)

            If mStatus = "O" Then
                OptStatOpen.Checked = True
                OptStatClosed.Checked = False
            Else
                OptStatOpen.Checked = False
                OptStatClosed.Checked = True
            End If
            chkRunDate.CheckState = IIf(RsUser.Fields("RUNDATE_CHANGE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            chkDS.CheckState = IIf(RsUser.Fields("PUR_DELV_SCHD_APP").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked) '	
            chkInvoiceAdmin.CheckState = IIf(RsUser.Fields("INVOICE_ADMIN").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)   '	
            chkBookLocking.CheckState = IIf(RsUser.Fields("BOOK_LOCKING").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)    '	
            chkAllow_AccountMaster.CheckState = IIf(RsUser.Fields("ALLOW_ACCOUNT_MASTER").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)    '	
            chkAllow_BopPo.CheckState = IIf(RsUser.Fields("ALLOW_BOP_PO").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)    '	
            chkAllow_RmPo.CheckState = IIf(RsUser.Fields("ALLOW_RM_PO").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked) '	
            chkAllow_PoprintApp.CheckState = IIf(RsUser.Fields("ALLOW_POPRINT_APP").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)   '	
            chkAllow_StockAdj.CheckState = IIf(RsUser.Fields("ALLOW_STOCK_ADJ").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked) '	
            chkPay_CorpUser.CheckState = IIf(RsUser.Fields("PAY_CORP_USER").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)   '	
            chkInv_LevelUser.CheckState = IIf(RsUser.Fields("INV_LEVEL_USER").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)  '	
            chkInv_Level_AppUser.CheckState = IIf(RsUser.Fields("INV_LEVEL_APP_USER").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)  '	
            chkAllow_ExcessIssue.CheckState = IIf(RsUser.Fields("ALLOW_EXCESS_ISSUE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)  '	
            chkDigitalSign.CheckState = IIf(RsUser.Fields("DIGITAL_SIGN").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)  '	


            mUserType = IIf(IsDBNull(RsUser.Fields("SUPER_USER").Value), 0, RsUser.Fields("SUPER_USER").Value)

            If mUserType = "S" Then
                cboUserType.SelectedIndex = 0
            ElseIf mUserType = "U" Then
                cboUserType.SelectedIndex = 1
            ElseIf mUserType = "G" Then
                cboUserType.SelectedIndex = 2
            Else
                cboUserType.SelectedIndex = 3
            End If

            mLevel = IIf(IsDBNull(RsUser.Fields("USER_LEVEL").Value), 0, RsUser.Fields("USER_LEVEL").Value)
                txtUserID.Enabled = True
                cboLevel.SelectedIndex = mLevel - 1
                ShowCompanyRights()
            End If
            ADDMode = False
        MODIFYMode = False
        ''MainClass.ButtonStatus(Me, XRIGHT, RsUser, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        MainClass.ButtonStatus(Me, XRIGHT, RsUser, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
Errhand1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub

    Private Sub SearchUserID(ByRef mTextBox As System.Windows.Forms.TextBox, ByRef mUserIdCheck As Boolean)
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster("", "ATH_PASSWORD_MST", "USER_ID", "EMP_NAME", "USER_CODE", , SqlStr) = True Then
            mTextBox.Text = AcName
            If mUserIdCheck = True Then
                txtUserID_Validating(txtUserID, New System.ComponentModel.CancelEventArgs(False))
                txtpassword.Focus()
            End If
        End If

    End Sub
    Private Function Update1() As Boolean
        On Error GoTo UpdateError

        Dim SqlStr As String = ""
        Dim mSuperUser As String
        Dim mRUNDateChange As String
        Dim mStatus As String
        Dim mPassword As String
        Dim mAdminPassword As String

        Dim mDS As String
        Dim mInvoiceAdmin As String
        Dim mBookLocking As String
        Dim mAllow_AccountMaster As String
        Dim mAllow_BopPo As String
        Dim mAllow_RmPo As String
        Dim mAllow_PoprintApp As String
        Dim mAllow_StockAdj As String
        Dim mPay_CorpUser As String
        Dim mInv_LevelUser As String
        Dim mInv_Level_AppUser As String
        Dim mAllow_ExcessIssue As String
        Dim mDigitalSign As String

        mStatus = IIf(OptStatOpen.Checked = True, "O", "C")
        mRUNDateChange = IIf(chkRunDate.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mDS = IIf(chkDS.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mInvoiceAdmin = IIf(chkInvoiceAdmin.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mBookLocking = IIf(chkBookLocking.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mAllow_AccountMaster = IIf(chkAllow_AccountMaster.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mAllow_BopPo = IIf(chkAllow_BopPo.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mAllow_RmPo = IIf(chkAllow_RmPo.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mAllow_PoprintApp = IIf(chkAllow_PoprintApp.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mAllow_StockAdj = IIf(chkAllow_StockAdj.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mPay_CorpUser = IIf(chkPay_CorpUser.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mInv_LevelUser = IIf(chkInv_LevelUser.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mInv_Level_AppUser = IIf(chkInv_Level_AppUser.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mAllow_ExcessIssue = IIf(chkAllow_ExcessIssue.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mDigitalSign = IIf(chkDigitalSign.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")


        mPassword = ToHexDump(CryptRC4(UCase(txtpassword.Text), "password"))
        mAdminPassword = ToHexDump(CryptRC4(UCase(txtAdminPassword.Text), "password"))
        ''ADMIN_PASSWORD,  '" & MainClass.AllowSingleQuote(mAdminPassword) & "', 

        SqlStr = ""
        If ADDMode = True Then
            SqlStr = " INSERT INTO ATH_PASSWORD_MST ( " & vbCrLf _
                        & " COMPANY_CODE, USER_ID, EMP_NAME," & vbCrLf _
                        & " USER_CODE, PASSWORD, NEWPASSWORD, USER_IP, " & vbCrLf _
                        & " SUPER_USER ,RUNDATE_CHANGE, USER_LEVEL, STATUS,ADMIN_PASSWORD,WINDOW_USER,EMAIL," & vbCrLf _
                        & " PUR_DELV_SCHD_APP , INVOICE_ADMIN ,BOOK_LOCKING ,ALLOW_ACCOUNT_MASTER , " & vbCrLf _
                        & " ALLOW_BOP_PO ,ALLOW_RM_PO ,ALLOW_POPRINT_APP ,ALLOW_STOCK_ADJ , " & vbCrLf _
                        & " PAY_CORP_USER ,INV_LEVEL_USER ,INV_LEVEL_APP_USER ,ALLOW_EXCESS_ISSUE,DIGITAL_SIGN,  " & vbCrLf _
                        & " DS_USERID, DS_PASSWORD, DS_CERTIFICATE_SNO, DS_DLL_PATH, DS_DLL_FILENAME, ADDUSER, ADDDATE" & vbCrLf _
                        & " ) " & vbCrLf _
                        & " VALUES ( " & vbCrLf _
                        & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(txtUserID.Text) & "', '" & MainClass.AllowSingleQuote(txtName.Text) & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "', '-', '" & MainClass.AllowSingleQuote(mPassword) & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(txtIPAddress.Text) & "', " & vbCrLf _
                        & " '" & Mid(cboUserType.Text, 1, 1) & "','" & mRUNDateChange & "', '" & Mid(cboLevel.Text, 1, 1) & "'," & vbCrLf _
                        & " '" & mStatus & "', '" & MainClass.AllowSingleQuote(mAdminPassword) & "', '" & MainClass.AllowSingleQuote(txtWindowUser.Text) & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(txteMailId.Text) & "'," & vbCrLf _
                        & " '" & mDS & "', '" & mInvoiceAdmin & "', '" & mBookLocking & "', '" & mAllow_AccountMaster & "', " & vbCrLf _
                        & " '" & mAllow_BopPo & "', '" & mAllow_RmPo & "', '" & mAllow_PoprintApp & "', '" & mAllow_StockAdj & "', " & vbCrLf _
                        & " '" & mPay_CorpUser & "', '" & mInv_LevelUser & "', '" & mInv_Level_AppUser & "', '" & mAllow_ExcessIssue & "','" & mDigitalSign & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(txtDigitalSignUID.Text) & "', '" & MainClass.AllowSingleQuote(txtDigitalSignPassword.Text) & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(txtDSCertificateNo.Text) & "', '" & MainClass.AllowSingleQuote(txtDLLPathName.Text) & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(txtDLLFileName.Text) & "','" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
                        & " )"


        Else
            SqlStr = "UPDATE  ATH_PASSWORD_MST  SET " & vbCrLf _
                    & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                    & " USER_ID='" & MainClass.AllowSingleQuote(txtUserID.Text) & "',  " & vbCrLf _
                    & " EMP_NAME='" & MainClass.AllowSingleQuote(txtName.Text) & "', " & vbCrLf _
                    & " USER_CODE='" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "', " & vbCrLf _
                    & " NEWPASSWORD='" & MainClass.AllowSingleQuote(mPassword) & "', " & vbCrLf _
                    & " USER_IP='" & MainClass.AllowSingleQuote(txtIPAddress.Text) & "', " & vbCrLf _
                    & " ADMIN_PASSWORD='" & MainClass.AllowSingleQuote(mAdminPassword) & "', " & vbCrLf _
                    & " WINDOW_USER='" & MainClass.AllowSingleQuote(txtWindowUser.Text) & "', " & vbCrLf _
                    & " SUPER_USER='" & Mid(cboUserType.Text, 1, 1) & "' ,EMAIL='" & MainClass.AllowSingleQuote(txteMailId.Text) & "'," & vbCrLf _
                    & " RUNDATE_CHANGE='" & mRUNDateChange & "', " & vbCrLf _
                    & " USER_LEVEL='" & Mid(cboLevel.Text, 1, 1) & "', " & vbCrLf _
                    & " STATUS='" & mStatus & "',  DIGITAL_SIGN='" & mDigitalSign & "'," & vbCrLf _
                    & " PUR_DELV_SCHD_APP ='" & mDS & "', INVOICE_ADMIN ='" & mInvoiceAdmin & "' ,BOOK_LOCKING='" & mBookLocking & "' ,ALLOW_ACCOUNT_MASTER='" & mAllow_AccountMaster & "' , " & vbCrLf _
                    & " ALLOW_BOP_PO='" & mAllow_BopPo & "' ,ALLOW_RM_PO='" & mAllow_RmPo & "' ,ALLOW_POPRINT_APP ='" & mAllow_PoprintApp & "',ALLOW_STOCK_ADJ ='" & mAllow_StockAdj & "', " & vbCrLf _
                    & " PAY_CORP_USER ='" & mPay_CorpUser & "',INV_LEVEL_USER='" & mInv_LevelUser & "' ,INV_LEVEL_APP_USER='" & mInv_Level_AppUser & "' ,ALLOW_EXCESS_ISSUE='" & mAllow_ExcessIssue & "',  " & vbCrLf _
                    & " DS_USERID='" & MainClass.AllowSingleQuote(txtDigitalSignUID.Text) & "', DS_PASSWORD='" & MainClass.AllowSingleQuote(txtDigitalSignPassword.Text) & "', " & vbCrLf _
                    & " DS_CERTIFICATE_SNO='" & MainClass.AllowSingleQuote(txtDSCertificateNo.Text) & "', DS_DLL_PATH='" & MainClass.AllowSingleQuote(txtDLLPathName.Text) & "'," & vbCrLf _
                    & " DS_DLL_FILENAME='" & MainClass.AllowSingleQuote(txtDLLFileName.Text) & "', ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "', ModDate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND USER_ID='" & MainClass.AllowSingleQuote(UCase(txtUserID.Text)) & "'"
        End If



        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        PubDBCn.Execute(SqlStr)
        If UpdateCompanyDetails(mPassword, mAdminPassword, mRUNDateChange) = False Then GoTo UpdateError

        If Trim(txtEquivalent.Text) <> "" Then
            If UpdateDivsionRight() = False Then GoTo UpdateError
            If UpdateModuleRight() = False Then GoTo UpdateError
            If UpdateMenuRight() = False Then GoTo UpdateError
        End If

        PubDBCn.CommitTrans()
        RsUser.Requery() ''.Refresh
        Update1 = True
        Exit Function
UpdateError:
        Call ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        Update1 = False
        PubDBCn.RollbackTrans() ''
        RsUser.Requery() ''.Refresh
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function UpdateCompanyDetails(mPassword As String, mAdminPassword As String, mRUNDateChange As String) As Boolean
        On Error GoTo ErrSave
        Dim cntRow As Short
        Dim mCompanyCode As Integer
        Dim mRights As String
        Dim SqlStr As String
        Dim mIsNew As Boolean
        Dim mStatus As String
        Dim mDigitalSign As String

        If Trim(txtUserID.Text) = "" Then Exit Function

        mDigitalSign = IIf(chkDigitalSign.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        UpdateCompanyDetails = False
        PubDBCn.Execute("DELETE FROM GEN_COMPANYRIGHT_MST WHERE USER_ID='" & UCase(txtUserID.Text) & "'")

        For cntRow = 1 To SprdMain.MaxRows

            SprdMain.Row = cntRow

            SprdMain.Col = ColCompanyCode
            mCompanyCode = Val(SprdMain.Text)

            SprdMain.Col = ColCanWork
            mRights = IIf(SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

            If mRights = "Y" Then
                SqlStr = ""
                SqlStr = "INSERT INTO GEN_COMPANYRIGHT_MST (" & vbCrLf _
                   & " USER_ID, COMPANY_CODE,  RIGHTS " & vbCrLf _
                   & " ) VALUES ( " & vbCrLf _
                   & " '" & txtUserID.Text & "', " & vbCrLf _
                   & " " & mCompanyCode & ", '" & mRights & "')"
                PubDBCn.Execute(SqlStr)

                If MainClass.ValidateWithMasterTable(txtUserID.Text, "USER_ID", "USER_ID", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & mCompanyCode & "") = False Then
                    mIsNew = True
                Else
                    mIsNew = False
                End If

                mStatus = IIf(mRights = "Y", "O", "C")

                If mIsNew = True Then
                    SqlStr = " INSERT INTO ATH_PASSWORD_MST ( " & vbCrLf _
                            & " COMPANY_CODE, USER_ID, EMP_NAME," & vbCrLf _
                            & " USER_CODE, PASSWORD, NEWPASSWORD, USER_IP, " & vbCrLf _
                            & " SUPER_USER ,RUNDATE_CHANGE, USER_LEVEL, STATUS, ADMIN_PASSWORD, WINDOW_USER, EMAIL," & vbCrLf _
                            & " DS_USERID, DS_PASSWORD, DS_CERTIFICATE_SNO, DS_DLL_PATH, DS_DLL_FILENAME,ADDUSER,ADDDATE,DIGITAL_SIGN" & vbCrLf _
                            & " ) " & vbCrLf _
                            & " VALUES ( " & vbCrLf _
                            & " " & mCompanyCode & ", " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtUserID.Text) & "', '" & MainClass.AllowSingleQuote(txtName.Text) & "'," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "', '-', '" & MainClass.AllowSingleQuote(mPassword) & "'," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtIPAddress.Text) & "', " & vbCrLf _
                            & " '" & Mid(cboUserType.Text, 1, 1) & "','" & mRUNDateChange & "', '" & Mid(cboLevel.Text, 1, 1) & "'," & vbCrLf _
                            & " '" & mStatus & "', '" & MainClass.AllowSingleQuote(mAdminPassword) & "', '" & MainClass.AllowSingleQuote(txtWindowUser.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txteMailId.Text) & "'," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtDigitalSignUID.Text) & "', '" & MainClass.AllowSingleQuote(txtDigitalSignPassword.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtDSCertificateNo.Text) & "', '" & MainClass.AllowSingleQuote(txtDLLPathName.Text) & "'," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtDLLFileName.Text) & "','" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') ,'" & mDigitalSign & "'" & vbCrLf _
                            & " )"
                Else
                    SqlStr = "UPDATE  ATH_PASSWORD_MST  SET " & vbCrLf _
                        & " COMPANY_CODE=" & mCompanyCode & ", " & vbCrLf _
                        & " USER_ID='" & MainClass.AllowSingleQuote(txtUserID.Text) & "',  " & vbCrLf _
                        & " EMP_NAME='" & MainClass.AllowSingleQuote(txtName.Text) & "', " & vbCrLf _
                        & " USER_CODE='" & MainClass.AllowSingleQuote(txtEmpCode.Text) & "', " & vbCrLf _
                        & " USER_IP='" & MainClass.AllowSingleQuote(txtIPAddress.Text) & "', " & vbCrLf _
                        & " WINDOW_USER='" & MainClass.AllowSingleQuote(txtWindowUser.Text) & "', " & vbCrLf _
                        & " EMAIL='" & MainClass.AllowSingleQuote(txteMailId.Text) & "'," & vbCrLf _
                        & " RUNDATE_CHANGE='" & mRUNDateChange & "', " & vbCrLf _
                        & " STATUS='" & mStatus & "',  DIGITAL_SIGN='" & mDigitalSign & "'," & vbCrLf _
                        & " DS_USERID='" & MainClass.AllowSingleQuote(txtDigitalSignUID.Text) & "', DS_PASSWORD='" & MainClass.AllowSingleQuote(txtDigitalSignPassword.Text) & "', " & vbCrLf _
                        & " DS_CERTIFICATE_SNO='" & MainClass.AllowSingleQuote(txtDSCertificateNo.Text) & "', DS_DLL_PATH='" & MainClass.AllowSingleQuote(txtDLLPathName.Text) & "'," & vbCrLf _
                        & " DS_DLL_FILENAME='" & MainClass.AllowSingleQuote(txtDLLFileName.Text) & "',ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "', ModDate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')" & vbCrLf _
                        & " WHERE COMPANY_CODE=" & mCompanyCode & "" & vbCrLf _
                        & " AND USER_ID='" & MainClass.AllowSingleQuote(UCase(txtUserID.Text)) & "'"
                End If
                PubDBCn.Execute(SqlStr)
            End If
        Next

        ''
        '' 
        '

        UpdateCompanyDetails = True
        Exit Function
ErrSave:

    End Function
    Private Function UpdateDivsionRight() As Boolean
        On Error GoTo ErrUpdateModuleRight
        Dim SqlStr As String = ""
        Dim RsModule As Recordset

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        PubDBCn.Execute("Delete From GEN_DivisionRight_MST  " & vbCrLf _
                        & " Where USER_ID='" & UCase(MainClass.AllowSingleQuote(Trim(txtUserID.Text)) & "'" & vbCrLf _
                        & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "))


        ''AND ModuleId=" & CurrModuleID & "

        SqlStr = " SELECT '" & UCase(MainClass.AllowSingleQuote(Trim(txtUserID.Text))) & "'," & vbCrLf _
                & " COMPANY_CODE,DIV_CODE, Rights FROM GEN_DivisionRight_MST " & vbCrLf _
                & " Where User_ID='" & UCase(MainClass.AllowSingleQuote(Trim(txtEquivalent.Text))) & "'" & vbCrLf _
                & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        ''AND ModuleId=" & CurrModuleID & "

        SqlStr = "Insert Into GEN_DivisionRight_MST (User_ID,COMPANY_CODE,DIV_CODE, RIGHTS) " & vbCrLf & SqlStr

        PubDBCn.Execute(SqlStr)
        UpdateDivsionRight = True
        Exit Function
ErrUpdateModuleRight:
        MsgBox(Err.Description)
        UpdateDivsionRight = False
        '    Resume
    End Function
    Private Function UpdateModuleRight() As Boolean
        On Error GoTo ErrUpdateModuleRight
        Dim SqlStr As String = ""
        Dim RsModule As Recordset

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        PubDBCn.Execute("Delete From GEN_ModuleRight_MST  " & vbCrLf _
                        & " Where UserID='" & UCase(MainClass.AllowSingleQuote(Trim(txtUserID.Text)) & "'" & vbCrLf _
                        & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "))

        ''AND ModuleId=" & CurrModuleID & "

        SqlStr = " SELECT '" & UCase(MainClass.AllowSingleQuote(Trim(txtUserID.Text))) & "'," & vbCrLf _
                & " COMPANY_CODE,MODULEID, Rights FROM GEN_ModuleRight_MST " & vbCrLf _
                & " Where UserID='" & UCase(MainClass.AllowSingleQuote(Trim(txtEquivalent.Text))) & "'" & vbCrLf _
                & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        ''AND ModuleId=" & CurrModuleID & "

        SqlStr = "Insert Into GEN_ModuleRight_MST (UserID,COMPANY_CODE,MODULEID, RIGHTS) " & vbCrLf & SqlStr

        PubDBCn.Execute(SqlStr)
        UpdateModuleRight = True
        Exit Function
ErrUpdateModuleRight:
        MsgBox(Err.Description)
        UpdateModuleRight = False
        '    Resume
    End Function
    Private Function UpdateMenuRight() As Boolean
        On Error GoTo ErrUpdateMenuRight
        Dim SqlStr As String = "" = ""
        Dim RsModule As Recordset

        PubDBCn.Execute("Delete From FIN_RIGHTS_MST Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND UserID='" & MainClass.AllowSingleQuote(Trim(txtUserID.Text)) & "'")

        SqlStr = " Select " & vbCrLf _
            & " COMPANY_CODE, '" & MainClass.AllowSingleQuote(Trim(txtUserID.Text)) & "'," & vbCrLf _
            & " MENUHEAD, RIGHTS, MODULEID FROM FIN_RIGHTS_MST " & vbCrLf _
            & " Where UserID='" & MainClass.AllowSingleQuote(Trim(txtEquivalent.Text)) & "'" & vbCrLf _
            & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        SqlStr = "Insert Into FIN_RIGHTS_MST " & vbCrLf _
            & " (COMPANY_CODE,UserID, MENUHEAD,RIGHTS,MODULEID) " & vbCrLf & SqlStr

        PubDBCn.Execute(SqlStr)
        UpdateMenuRight = True
        Exit Function
ErrUpdateMenuRight:
        MsgBox(Err.Description)
        UpdateMenuRight = False
    End Function
    Private Sub settextlength()
        On Error GoTo ERR1
        txtUserID.MaxLength = 6 ''RsUser.Fields("USER_ID").DefinedSize           ''
        txtpassword.MaxLength = 8 'RsUser.Fields("PASSWORD").DefinedSize
        txtAdminPassword.MaxLength = 8 ' RsUser.Fields("ADMIN_PASSWORD").DefinedSize ''
        txtEmpCode.MaxLength = RsUser.Fields("USER_CODE").DefinedSize
        txtName.MaxLength = RsUser.Fields("EMP_NAME").DefinedSize ''

        txtIPAddress.MaxLength = RsUser.Fields("USER_IP").DefinedSize ''
        txtWindowUser.MaxLength = RsUser.Fields("WINDOW_USER").DefinedSize ''
        txteMailId.MaxLength = RsUser.Fields("EMAIL").DefinedSize ''

        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Private Sub frmUsers_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        If FormActive = True Then Exit Sub
        Clear1()
        If CmdAdd.Enabled = True Then cmdAdd_Click(CmdAdd, New System.EventArgs)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmUsers_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub frmUsers_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)
        Dim SqlStr As String = ""
        Dim RsTemp As Recordset = Nothing
        Dim I As Long

        SqlStr = " Select USER_ID,EMP_NAME FROM ATH_PASSWORD_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, CursorTypeEnum.adOpenStatic, RsTemp, LockTypeEnum.adLockReadOnly)
        I = 1
        sprdView.MaxRows = I
        sprdView.MaxCols = 2

        If RsTemp.EOF = False Then
            With sprdView
                Do While RsTemp.EOF = False
                    .Row = I
                    .Col = 1
                    .Text = RsTemp.Fields("USER_ID").Value

                    .Col = 2
                    .Text = IIf(IsDBNull(RsTemp.Fields("EMP_NAME").Value), "", RsTemp.Fields("EMP_NAME").Value)

                    RsTemp.MoveNext()
                    If RsTemp.EOF = False Then
                        .MaxRows = .MaxRows + 1
                        I = I + 1
                    End If
                Loop
            End With
        End If
        'MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N")


        FormatSprdView()

    End Sub
    Private Sub FormatSprdView()

        With sprdView
            .Row = -1
            .set_RowHeight(0, 12)
            .set_ColWidth(0, 5)
            .set_ColWidth(1, 8)
            .set_ColWidth(2, 20)
            .set_ColWidth(3, 12)
            .set_ColWidth(4, 12)
            .ColsFrozen = 2

            MainClass.ProtectCell(sprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(sprdView, -1)
            ''.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            MainClass.CellColor(sprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr
        Dim SqlStr As String = ""

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        '' If InsertIntoDelAudit(PubDBCn, "ATH_PASSWORD_MST", (txtUserID.Text), RsUser) = False Then GoTo DeleteErr
        '' If InsertIntoDeleteTrn(PubDBCn, "ATH_PASSWORD_MST", "USER_ID", (txtUserID.Text) = False Then GoTo DeleteErr

        SqlStr = " DELETE FROM ATH_PASSWORD_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND USER_ID='" & MainClass.AllowSingleQuote(UCase(Trim(txtUserID.Text))) & "'"
        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()
        RsUser.Requery() ''.Refresh

        Delete1 = True
        Exit Function
DeleteErr:
        ''Resume
        Call ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        Delete1 = False
        PubDBCn.RollbackTrans() ''
        '    RsUser.Requery           ''.Refresh

    End Function

    Private Function FieldsVarification() As Boolean
        On Error GoTo ERR1

        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
        End If
        If MODIFYMode = True And (RsUser.EOF = True Or RsUser.EOF = True) Then Exit Function

        FieldsVarification = True

        If Trim(txtUserID.Text) = "" Then
            MsgInformation("UserId is empty. Cannot Save")
            txtUserID.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtpassword.Text) = "" Then
            MsgInformation("Password is empty. Cannot Save")
            txtpassword.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If cboLevel.SelectedIndex = -1 Then
            MsgInformation("User Level Not Defined. Cannot Save")
            cboLevel.Focus()
            FieldsVarification = False
            Exit Function
        End If

        'If Trim(txtEmpCode.Text) <> "" Then
        '    If MainClass.ValidateWithMasterTable(txtEmpCode.Text, "USER_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
        '        MsgBox("Invalid Employee Code.", vbCritical)
        '        txtEmpCode.Focus()
        '        FieldsVarification = False
        '        Exit Function
        '    End If
        'End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Sub SearchName()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster(txtName.Text, "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            'If MainClass.SearchGridMaster("", "ATH_PASSWORD_MST", "USER_ID", "User Code", "EMP_NAME", "User Name", , , , , , , SqlStr) = True Then
            txtName.Text = AcName
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchEmpCode()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster(txtEmpCode.Text, "PAY_EMPLOYEE_MST", "EMP_CODE", "EMP_NAME", , , SqlStr) = True Then
            'If MainClass.SearchGridMaster("", "ATH_PASSWORD_MST", "USER_ID", "User Code", "EMP_NAME", "User Name", , , , , , , SqlStr) = True Then
            txtEmpCode.Text = AcName
            txtName.Text = AcName1
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtUserID_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUserID.DoubleClick
        Call SearchUserID(txtUserID, True)
    End Sub

    Private Sub txtUserID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUserID.KeyPress
        Dim NotAllowed As String = "~`@%^&+={[}]()!:,;'><?/|\-.#+"
        Dim KeyAscii As Short = Asc(e.KeyChar)

        'Allowed letters numbers and ( _ $ *)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtUserID.Text)
        If e.KeyChar <> ControlChars.Back = True Then
            If NotAllowed.IndexOf(e.KeyChar) = -1 = False Then
                e.Handled = True
            End If
        End If


    End Sub

    Private Sub txtUserID_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtUserID.KeyUp
        Dim KeyCode As Short
        'Dim Shift As Short = EventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchUserID(txtUserID, True)
    End Sub

    Private Sub txtUserID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUserID.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtEmpCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEmpCode.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtName_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.DoubleClick
        SearchName()
    End Sub

    Private Sub txtName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        'e.KeyChar = CChar(CStr(MainClass.UpperCase(e.KeyChar, txtName.Text)))
        Dim KeyAscii As Short = Asc(e.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtName.Text)
        e.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtName_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtName.KeyUp
        Dim KeyCode As Short
        ''Dim Shift As Short = EventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchName()
    End Sub
    Private Sub txtName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtName.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtpassword_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtpassword.KeyPress
        Dim KeyAscii As Short
        KeyAscii = MainClass.UpperCase(KeyAscii, txtUserID.Text)
    End Sub

    Private Sub txtpassword_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtpassword.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAdminPassword_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAdminPassword.KeyPress
        Dim KeyAscii As Short
        KeyAscii = MainClass.UpperCase(KeyAscii, txtUserID.Text)
    End Sub

    Private Sub txtAdminPassword_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAdminPassword.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboUserType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboUserType.Click
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboUserType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboUserType.SelectedIndexChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboLevel_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboLevel.SelectedIndexChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtEquivalent_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEquivalent.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtEquivalent.Text)

        e.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            e.Handled = True
        End If

    End Sub

    Private Sub txtEquivalent_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEquivalent.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkRunDate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub OptStatOpen_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OptStatOpen.CheckedChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub OptStatOpen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles OptStatOpen.Click
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub OptStatClosed_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OptStatClosed.CheckedChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub OptStatClosed_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles OptStatClosed.Click
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtName_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtName.Validating
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mEmpName As String

        'If Trim(txtName.Text) = "" Then GoTo EventExitSub
        'mEmpName = Trim(txtName.Text)

        'SqlStr = "COMPANY_CODE=" & Val(txtCompanyCode.Text) & " AND BRANCH_CODE=" & Val(txtBranchCode.Text) & ""

        'If MainClass.ValidateWithMasterTable(txtName.Text), "EMP_NAME", "USER_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
        '    MsgBox("Invalid Employee Name.", vbCritical)
        '    e.Cancel = False
        'End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        'e = Cancel
    End Sub

    Private Sub txtUserID_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtUserID.Validating
        Dim Cancel As Boolean = False
        On Error GoTo ERR1
        Dim xUserID As String = ""
        Dim SqlStr As String = ""

        If Trim(txtUserID.Text) = "" Then GoTo EventExitSub

        If MODIFYMode = True And RsUser.EOF = False Then xUserID = RsUser.Fields("USER_ID").Value

        SqlStr = " SELECT * FROM  ATH_PASSWORD_MST " & vbCrLf _
                & " WHERE USER_ID='" & MainClass.AllowSingleQuote(Trim(UCase(txtUserID.Text))) & "' AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, CursorTypeEnum.adOpenStatic, RsUser, LockTypeEnum.adLockReadOnly)
        If RsUser.EOF = True Then
            SqlStr = " SELECT * FROM  ATH_PASSWORD_MST " & vbCrLf _
                 & " WHERE USER_ID='" & MainClass.AllowSingleQuote(Trim(UCase(txtUserID.Text))) & "'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, CursorTypeEnum.adOpenStatic, RsUser, LockTypeEnum.adLockReadOnly)
        End If

        If RsUser.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            FillCompany()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("New Entry, Click Add To Add In Master", MsgBoxStyle.Information)
                e.Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = " SELECT * FROM  ATH_PASSWORD_MST " & vbCrLf _
                        & " WHERE USER_ID='" & MainClass.AllowSingleQuote(Trim(UCase(xUserID))) & "' "

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, CursorTypeEnum.adOpenStatic, RsUser, LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:

    End Sub
    Private Sub txtEquivalent_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtEquivalent.Validating
        Dim Cancel As Boolean = False
        On Error GoTo ERR1
        Dim xUserID As String = ""
        Dim SqlStr As String = ""
        Dim RSTemp As ADODB.Recordset

        If Trim(txtEquivalent.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtEquivalent.Text, "USER_ID", "EMP_NAME", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtEquivalentName.Text = MasterNo
        Else
            MsgBox("Invalid Equivalent Employee Name.", vbCritical)
        End If


        GoTo EventExitSub
ERR1:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:

    End Sub
    Private Sub CmdPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdPreview.Click
        On Error GoTo ErrPart
        Call ShowReport(False)
        Exit Sub
ErrPart:
        'Resume
        ErrorMsg(Err.Description, Err.Number, vbCritical)
    End Sub
    Private Sub ShowReport(ByVal pPrintToPrinter As Boolean)
        On Error GoTo ErrPart
        'Dim crapp As New CRAXDRT.Application
        'Dim RsTemp As New Recordset
        'Dim RS As New Recordset

        'Dim objRpt As CRAXDRT.Report
        'Dim fPath As String
        'Dim SqlStr As String = ""="" = ""

        'Dim mRPTName As String = "
        'Dim mTitle As String = "
        'Dim mSubTitle As String = "

        'mTitle = "USERS MASTER"
        'mSubTitle = "FOR THE PERIOD"

        'mRPTName = App_Path() & "Reports\UserMaster.rpt" ''& mRPTName

        'SqlStr = " SELECT * FROM ATH_PASSWORD_MST"
        'MainClass.UOpenRecordSet(SqlStr, PubDBCn, CursorTypeEnum.adOpenStatic, RsTemp, LockTypeEnum.adLockReadOnly)

        'If RsTemp.EOF = False Then
        '   objRpt = crapp.OpenReport(mRPTName)
        '   With objRpt
        '      Call MainClass.ClearCRptFormulas(objRpt)
        '      .DiscardSavedData()
        '      .Database.SetDataSource(RsTemp)
        '      MainClass.SetCrpt(objRpt, pPrintToPrinter, 1, mTitle, mSubTitle)
        '      .VerifyOnEveryPrint = True  '' blnVerifyOnEveryPrint

        '      If pPrintToPrinter = True Then
        '         .PrinterSetup(0)
        '         .PrintOut(False)
        '         While .PrintingStatus.Progress = CRAXDRT.CRPrintingProgress.crPrintingInProgress
        '            Application.DoEvents()
        '         End While

        '         '.PrintOutEx()
        '      Else
        '         fPath = "D:\Temp.pdf"


        '         .ExportOptions.FormatType = CRAXDRT.CRExportFormatType.crEFTPortableDocFormat
        '         .ExportOptions.DestinationType = CRAXDRT.CRExportDestinationType.crEDTDiskFile
        '         .ExportOptions.DiskFileName = fPath
        '         '    .ExportOptions.PDFExportAllPages = True
        '         .Export(False)
        '      End If
        '   End With
        'End If
        Exit Sub
ErrPart:
        'Resume
        ErrorMsg(Err.Description, Err.Number, vbCritical)
    End Sub

    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click
        On Error GoTo ErrPart
        Call ShowReport(True)
        Exit Sub
ErrPart:
        'Resume
        ErrorMsg(Err.Description, Err.Number, vbCritical)
    End Sub

    Private Sub txtCompanyCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBranchCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtEmpCode_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtEmpCode.Validating
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mEmpCode As String

        If Trim(txtEmpCode.Text) = "" Then GoTo EventExitSub
        mEmpCode = Trim(txtEmpCode.Text)

        'SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        'If MainClass.ValidateWithMasterTable(txtEmpCode.Text, "USER_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
        '    MsgBox("Invalid Employee Name.", vbCritical)
        '    e.Cancel = False
        'End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        'e = Cancel
    End Sub

    Private Sub txtIPAddress_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIPAddress.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)

        If KeyAscii = 0 Then
            e.Handled = True
        End If

    End Sub

    Private Sub txtIPAddress_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIPAddress.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
#Region "User ID Search"
    Private Sub USerIdSearch()
        Dim mSqlStr As String = ""
        Dim RsTemp As New Recordset
        mSqlStr = "Select USER_ID From ATH_PASSWORD_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, CursorTypeEnum.adOpenStatic, RsTemp, LockTypeEnum.adLockReadOnly)
        While RsTemp.EOF = False
            With txtUserID
                .AutoCompleteMode = AutoCompleteMode.Suggest
                .AutoCompleteCustomSource.Add(RsTemp.Fields("USER_ID").Value)
                .AutoCompleteSource = AutoCompleteSource.CustomSource
            End With
            RsTemp.MoveNext()
        End While
    End Sub
#End Region

    Private Function CRAXDRT() As Object
        Throw New NotImplementedException
    End Function
    Private Sub txtWindowUser_TextChanged(sender As Object, e As System.EventArgs) Handles txtWindowUser.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txteMailId_TextChanged(sender As Object, e As System.EventArgs) Handles txteMailId.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub optDeptRights_Click(sender As Object, e As System.EventArgs) Handles optDeptRights.Click

        frmDeptRights.MdiParent = Me.MdiParent
        frmDeptRights.txtUserId.Text = txtUserID.Text
        frmDeptRights.Show()

    End Sub
    Private Sub optMenuRights_Click(sender As Object, e As EventArgs) Handles optMenuRights.Click

        frmMnuRightsNew.MdiParent = Me.MdiParent
        frmMnuRightsNew.txtUserId.Text = txtUserID.Text
        frmMnuRightsNew.Show()
    End Sub

    Private Sub optModuleRights_Click(sender As Object, e As EventArgs) Handles optModuleRights.Click

        frmModuleRights.MdiParent = Me.MdiParent
        frmModuleRights.txtUserId.Text = txtUserID.Text
        frmModuleRights.Show()
    End Sub

    Private Sub optBranchRights_Click(sender As Object, e As EventArgs) Handles optBranchRights.Click

        frmDivisionRights.MdiParent = Me.MdiParent
        frmDivisionRights.txtUserId.Text = txtUserID.Text
        frmDivisionRights.Show()
    End Sub
    Private Sub ShowCompanyRights()

        On Error GoTo Errshow1
        Dim cntRow As Short
        Dim mDivisionCode As String
        Dim SqlStr As String


        SqlStr = ""
        SqlStr = " SELECT IH.COMPANY_CODE, ID.COMPANY_NAME, IH.Rights " & vbCrLf _
           & " FROM GEN_COMPANYRIGHT_MST IH, GEN_COMPANY_MST ID " & vbCrLf _
           & " WHERE IH.COMPANY_CODE= ID.COMPANY_CODE " & vbCrLf _
           & " AND IH.USER_ID='" & UCase(txtUserID.Text) & "'" & vbCrLf _
           & " ORDER BY IH.COMPANY_CODE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCompanyRights, ADODB.LockTypeEnum.adLockOptimistic)

        '    Set RsFields = RsCompanyRights.Fields					

        If RsCompanyRights.EOF = False Then
            RsCompanyRights.MoveFirst()
            Do While Not RsCompanyRights.EOF
                mDivisionCode = IIf(IsDBNull(RsCompanyRights.Fields("COMPANY_CODE").Value), "", RsCompanyRights.Fields("COMPANY_CODE").Value)

                For cntRow = 1 To SprdMain.MaxRows
                    SprdMain.Row = cntRow
                    SprdMain.Col = ColCompanyCode
                    If Val(SprdMain.Text) = IIf(IsDBNull(RsCompanyRights.Fields("COMPANY_CODE").Value), -1, RsCompanyRights.Fields("COMPANY_CODE").Value) Then

                        SprdMain.Col = ColCanWork
                        If IIf(IsDBNull(RsCompanyRights.Fields("Rights").Value), "N", RsCompanyRights.Fields("Rights").Value) = "N" Then
                            SprdMain.Value = CStr(System.Windows.Forms.CheckState.Unchecked)
                        Else
                            SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked)
                        End If
                        Exit For
                    End If
                Next
                RsCompanyRights.MoveNext()
                '            k = k + 1			

            Loop
        End If
        Exit Sub
Errshow1:
        MsgBox(Err.Description)

    End Sub
    Private Sub FillCompany()

        On Error GoTo ErrFillMenu
        Dim RsDIVISION As ADODB.Recordset = Nothing
        Dim mRow As Integer
        Dim SqlStr As String = ""

        mRow = 1
        SqlStr = " SELECT TO_CHAR(COMPANY_CODE) COMPANY_CODE, COMPANY_NAME " & vbCrLf _
           & " FROM GEN_COMPANY_MST " & vbCrLf _
           & " ORDER BY COMPANY_CODE "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDIVISION, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsDIVISION.EOF Then
            'SprdMain.MaxRows = RsModules.RecordCount				
            FormatSprdMain(-1)
            Do While Not RsDIVISION.EOF
                SprdMain.Row = mRow

                SprdMain.Col = ColCompanyCode
                SprdMain.Text = RsDIVISION.Fields("COMPANY_CODE").Value

                SprdMain.Col = ColCompanyName
                SprdMain.Text = RsDIVISION.Fields("COMPANY_NAME").Value
                RsDIVISION.MoveNext()
                If RsDIVISION.EOF = False Then
                    mRow = mRow + 1
                    SprdMain.MaxRows = mRow
                End If
            Loop
            FormatSprdMain(-1)
        End If
        Exit Sub
ErrFillMenu:
        MsgBox(Err.Description)
    End Sub

    Private Sub frmUsers_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        RsCompanyRights = Nothing
    End Sub
    Private Sub chkDS_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDS.CheckedChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkInvoiceAdmin_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkInvoiceAdmin.CheckedChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkBookLocking_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBookLocking.CheckedChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkAllow_AccountMaster_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAllow_AccountMaster.CheckedChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkAllow_BopPo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAllow_BopPo.CheckedChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkAllow_RmPo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAllow_RmPo.CheckedChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkAllow_PoprintApp_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAllow_PoprintApp.CheckedChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkAllow_StockAdj_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAllow_StockAdj.CheckedChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkPay_CorpUser_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPay_CorpUser.CheckedChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkInv_LevelUser_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkInv_LevelUser.CheckedChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkInv_Level_AppUser_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkInv_Level_AppUser.CheckedChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkAllow_ExcessIssue_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAllow_ExcessIssue.CheckedChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkDigitalSign_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDigitalSign.CheckedChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdMain_Change(sender As Object, e As _DSpreadEvents_ChangeEvent) Handles SprdMain.Change
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cmdsearch_Click(sender As Object, e As EventArgs) Handles cmdsearch.Click
        SearchUserID(txtUserID, True)
    End Sub

    Private Sub cmdsearchEquivalent_Click(sender As Object, e As EventArgs) Handles cmdsearchEquivalent.Click
        SearchEquivalent(txtEquivalent, True)
    End Sub

    Private Sub SearchEquivalent(ByRef mTextBox As System.Windows.Forms.TextBox, ByRef mUserIdCheck As Boolean)
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster("", "ATH_PASSWORD_MST", "USER_ID", "EMP_NAME", "USER_CODE", , SqlStr) = True Then
            mTextBox.Text = AcName
            If mUserIdCheck = True Then
                txtEquivalent_Validating(txtEquivalent, New System.ComponentModel.CancelEventArgs(False))
            End If
        End If

    End Sub

    Private Sub txtEquivalent_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEquivalent.DoubleClick
        Call SearchEquivalent(txtEquivalent, True)
    End Sub

    Private Sub txtEquivalent_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtEquivalent.KeyUp
        Dim KeyCode As Short
        'Dim Shift As Short = EventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchEquivalent(txtEquivalent, True)
    End Sub
    Private Sub txtDigitalSignPassword_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDigitalSignPassword.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtDigitalSignUID_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDigitalSignUID.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtDSCertificateNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDSCertificateNo.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtDLLPathName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDLLPathName.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtDLLFileName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDLLFileName.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

End Class
