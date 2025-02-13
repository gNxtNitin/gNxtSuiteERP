Imports System.Data.SqlClient   '' System.Data.OleDb
Imports System.Data.OleDb
Imports ADODB
Imports Microsoft.VisualBasic.Compatibility
Imports System.ComponentModel
Imports Microsoft.Win32

Public Class FrmLogin
    Dim cmd As OleDbCommand = Nothing
    Private Sub FrmLogin_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'Me.Hide()
        'Me.Dispose()
        'Me.Close()
    End Sub


    Private Sub txtUserName_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtUserID.KeyPress
        Dim NotAllowed As String = "~`@%^&+={[}]()!:,;'><?/|\-.#+"

        'Allowed letters numbers and ( _ $ *)

        If e.KeyChar <> ControlChars.Back = True Then
            If NotAllowed.IndexOf(e.KeyChar) = -1 = False Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtPassword_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtPassword.KeyPress
        'Dim NotAllowed As String = "~`@%^&+={[}]()!:,;'><?/|\-.#+"

        ''Allowed letters numbers and ( _ $ *)

        'If e.KeyChar <> ControlChars.Back = True Then
        '    If NotAllowed.IndexOf(e.KeyChar) = -1 = False Then
        '        e.Handled = True
        '    End If
        'End If
    End Sub

    Private Sub CmdLogin_Click(sender As Object, e As System.EventArgs) Handles CmdLogin.Click
        Dim RsModule As Recordset = Nothing
        Dim mSqlStr As String = ""
        Dim mValue As String = ""
        Dim mCompanyCode As Long = -1
        Dim mFYear As Long
        Dim mIsAdmin As String = "N"
        Dim mZeroRepeatNo As Integer
        If MainClass.ValidateWithMasterTable(cboCompany.Text, "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo) = True Then
            mCompanyCode = Val(MasterNo)
        End If

        If FieldVarification(mCompanyCode) = False Then
            Exit Sub
        End If

        mFYear = Val(cboFYear.Text)

        If MainClass.ValidateWithMasterTable(UCase(txtUserID.Text.ToString()), "USER_ID", "SUPER_USER", "ATH_PASSWORD_MST", PubDBCn, MasterNo,, "COMPANY_CODE=" & mCompanyCode & "") = True Then
            mIsAdmin = Trim(MasterNo)
        End If

        RunDate = VB6.Format(Convert.ToDateTime(txtRunDate.Text.ToString), "dd/MM/yyyy")  '' VB6.Format(txtRunDate.Text, "dd/MM/yyyy")
        PubCurrDate = VB6.Format(Convert.ToDateTime(GetServerDate), "dd/MM/yyyy")
        PubUserID = UCase(txtUserID.Text.ToString())

        If MainClass.ValidateWithMasterTable(PubUserID, "USER_ID", "EMP_NAME", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & mCompanyCode & "") = True Then
            PubUserName = Trim(MasterNo)
        End If

        'PubSuperUser = mIsAdmin
        Call MakeRsCompany(Val(mCompanyCode), mFYear)

        If ValidateERP() = False Then Exit Sub
        'If ValidatePassWord = False Then Exit Sub
        If ValidateIP() = False Then Exit Sub
        If ValidateWindowUser() = False Then Exit Sub

        PubAllowGrant = GetUserPermission("ALLOW_CREATE", "N", PubUserID, RsCompany.Fields("COMPANY_CODE").Value)
        PubSuperUser = GetUserPermission("SUPER_USER", "G", PubUserID, RsCompany.Fields("COMPANY_CODE").Value)
        PubSuperUser = IIf(PubSuperUser = "Y", "A", IIf(PubSuperUser = "N", "U", PubSuperUser))
        PubAllowRunDateChange = GetUserPermission("RUNDATE_CHANGE", "N", PubUserID, RsCompany.Fields("COMPANY_CODE").Value)
        PubUserLevel = GetUserPermission("USER_LEVEL", 0, PubUserID, RsCompany.Fields("COMPANY_CODE").Value)
        PubAllowPermission = GetUserPermission("ALLOW_GRANT", "N", PubUserID, RsCompany.Fields("COMPANY_CODE").Value)
        PubPayCorpUser = GetUserPermission("PAY_CORP_USER", "N", PubUserID, RsCompany.Fields("COMPANY_CODE").Value)
        PubInvLevelUser = GetUserPermission("INV_LEVEL_USER", "N", PubUserID, RsCompany.Fields("COMPANY_CODE").Value)
        PubInvLevelAPPUser = GetUserPermission("INV_LEVEL_APP_USER", "N", PubUserID, RsCompany.Fields("COMPANY_CODE").Value)

        PubUserEMPCode = GetUserPermission("USER_CODE", "", PubUserID, RsCompany.Fields("COMPANY_CODE").Value)
        PubATHUSER = IIf(PubSuperUser = "S" Or PubSuperUser = "A", True, False)

        If PubSuperUser <> "S" Then
            If CDate(txtRunDate.Text) > CDate(PubCurrDate) Then
                MsgInformation("Date cann't be Greater Then Current date.")
                If txtRunDate.Enabled Then txtRunDate.Focus()
                Exit Sub
            End If
        End If


        If VaildFYChk(cboFYear.Text, mCompanyCode) = False Then
            cboFYear.Focus()
            Exit Sub
        End If

        'PubAuditUser = GetUserPermission("IS_AUDIT", "N", PubUserID, RsCompany.Fields("COMPANY_CODE").Value)
        'mValue = GetUserPermission("USER_LOCK", "N", PubUserID, RsCompany.Fields("COMPANY_CODE").Value)
        'PubGridLockUser = IIf(mValue = "Y", "N", "Y")

        ConInventoryTable = "INV_STOCK_REC_TRN" & IIf(RsCompany.Fields("INV_TAB_CC").Value = "Y", VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"), "") & IIf(RsCompany.Fields("INV_TAB_FY").Value = "Y", RsCompany.Fields("FYEAR").Value, "")
        mZeroRepeatNo = GetDocumentInvoiceDigit() '' IIf(IsDBNull(RsCompany.Fields("INVOICE_DIGIT").Value), 1, RsCompany.Fields("INVOICE_DIGIT").Value)
        ConBillFormat = StrDup(mZeroRepeatNo, "0")

        PubColorTheme = 1
        If MainClass.ValidateWithMasterTable(PubUserID, "USER_ID", "COLOR_THEME", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & mCompanyCode & "") = True Then
            PubColorTheme = Val(MasterNo)
        End If
        PubColorTheme = IIf(PubColorTheme <= 0, 1, PubColorTheme)

        PubGSTApplicableDate = "01/07/2017"
        PubGSTApplicable = IIf(CDate(RunDate) < CDate(PubGSTApplicableDate), False, True)
        PubPAYYEAR = VB6.Format(RunDate, "YYYY")

        'Dim RsModule As Recordset = Nothing
        Dim mModuleName As String = ""
        Dim mModuleCode As Long
        mSqlStr = "SELECT * FROM GEN_MODULE_MST WHERE STATUS='O'"
        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, CursorTypeEnum.adOpenKeyset, RsModule, LockTypeEnum.adLockOptimistic)

        If RsModule.EOF = False Then
            Do While RsModule.EOF = False
                mModuleName = IIf(IsDBNull(RsModule.Fields("MODULENAME").Value), "", RsModule.Fields("MODULENAME").Value)
                mModuleCode = IIf(IsDBNull(RsModule.Fields("MODULEID").Value), "", RsModule.Fields("MODULEID").Value)
                Select Case mModuleName

                    Case "ADMINISTRATOR MODULE"
                        mAdminModuleID = mModuleCode
                    Case "PAYROLL MODULE"
                        mPayrollModuleID = mModuleCode
                    Case "PRODUCTION MODULE"
                        mProductionModuleID = mModuleCode
                    Case "QUALITY MODULE"
                        mQualityModuleID = mModuleCode
                    Case "TDS MODULE"
                        mTDSModuleID = mModuleCode
                    Case "MIS MODULE"
                        mMISModuleID = mModuleCode
                    Case "INVENTORY GST MODULE"
                        mInventoryModuleID = mModuleCode
                    Case "SALE GST MODULE"
                        mSaleModuleID = mModuleCode
                    Case "ACCOUNT GST MODULE"
                        mAccountModuleID = mModuleCode

                End Select

                RsModule.MoveNext()
            Loop
        End If

        Try
            Dim frm As New FormMain
            Me.Hide()
            Call EnableModule(frm)
            Call SetStatusBar()
            frm.Show()
            'Me.Dispose()
            'Me.Close()  ''13-01-2022
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CmdCancel_Click(sender As Object, e As System.EventArgs) Handles CmdCancel.Click
        End
    End Sub
    Private Sub FrmLogin_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Call Main()
        Call FillLoginCombo()
        txtRunDate.Text = GetServerDate()
        'MainClass.SetControlsColor(Me)
    End Sub
    Private Sub FillLoginCombo()
        Dim RsTemp As Recordset
        Dim mSqlStr As String = ""
        Dim mCompanyName As String
        Dim mCount As Long
        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet

        oledbCnn = New OleDbConnection(StrConn)

        RsTemp = New Recordset
        Try
            mCount = 1
            mSqlStr = "Select COUNT(1) AS CNTCOMPANY from GEN_COMPANY_MST"
            MainClass.UOpenRecordSet(mSqlStr, PubDBCn, CursorTypeEnum.adOpenKeyset, RsTemp, LockTypeEnum.adLockOptimistic)
            If RsTemp.EOF = False Then
                mCount = IIf(IsDBNull(RsTemp.Fields("CNTCOMPANY").Value), 1, RsTemp.Fields("CNTCOMPANY").Value)
            End If

            RsTemp.Close()
            RsTemp = Nothing

            If mCount = 1 Then
                mSqlStr = "Select COMPANY_NAME from GEN_COMPANY_MST ORDER BY COMPANY_NAME"
            Else
                mSqlStr = "Select COMPANY_NAME, COMPANY_CODE, COMPANY_ADDR, COMPANY_CITY, COMPANY_GST_RGN_NO from GEN_COMPANY_MST ORDER BY COMPANY_NAME"
            End If


            'MainClass.UOpenRecordSet(mSqlStr, PubDBCn, CursorTypeEnum.adOpenKeyset, RsTemp, LockTypeEnum.adLockOptimistic)

            'cboCompany.Items.Clear()

            'If RsTemp.EOF = False Then
            '    Do While RsTemp.EOF = False
            '        mCompanyName = IIf(IsDBNull(RsTemp.Fields("COMPANY_NAME").Value), "", RsTemp.Fields("COMPANY_NAME").Value)
            '        cboCompany.Items.Add(mCompanyName)
            '        RsTemp.MoveNext()
            '    Loop
            'End If


            oledbCnn.Open()
            oledbAdapter = New OleDbDataAdapter(mSqlStr, oledbCnn)

            oledbAdapter.Fill(ds)

            ' Set the data source and data member to bind the grid.
            cboCompany.DataSource = ds
            cboCompany.DataMember = ""
            'cmbCompany.ValueMember = "COMPANY_CODE"
            'cmbCompany.DisplayMember = "Company Name"

            cboCompany.Appearance.FontData.SizeInPoints = 8.5

            cboCompany.DisplayLayout.Bands(0).Columns(0).Header.Caption = "Company Name"
            cboCompany.DisplayLayout.Bands(0).Columns(0).Width = 350

            If mCount = 1 Then
                cboCompany.Rows(0).Selected = True
            Else
                cboCompany.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Code"
                cboCompany.DisplayLayout.Bands(0).Columns(2).Header.Caption = "Address"
                cboCompany.DisplayLayout.Bands(0).Columns(3).Header.Caption = "City"
                cboCompany.DisplayLayout.Bands(0).Columns(4).Header.Caption = "GST No"


                cboCompany.DisplayLayout.Bands(0).Columns(1).Width = 50
                cboCompany.DisplayLayout.Bands(0).Columns(2).Width = 300
                cboCompany.DisplayLayout.Bands(0).Columns(3).Width = 100
                cboCompany.DisplayLayout.Bands(0).Columns(4).Width = 140
            End If

            cboCompany.DisplayLayout.Appearance.FontData.SizeInPoints = 8.5


            cboCompany.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList       '' Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown

            'cboCompany.Rows(0).Selected = True


            oledbAdapter.Dispose()
            oledbCnn.Close()



            cboLoginType.Items.Clear()
            cboLoginType.Items.Add("USER")
            cboLoginType.Items.Add("ADMIN")
            cboLoginType.Items.Add("GUEST")
            cboLoginType.SelectedIndex = 0

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub txtUserID_Validated(sender As Object, e As System.EventArgs) Handles txtUserID.Validated
        Dim RsPassword As Recordset
        Dim mSqlStr As String = ""

        If Len(Trim(txtUserID.Text)) = 0 Then
            Exit Sub
        End If

        Try
            RsPassword = New Recordset

            mSqlStr = "Select EMP_NAME AS USER_NAME from ATH_PASSWORD_MST WHERE UPPER(USER_ID)='" & UCase(txtUserID.Text.ToString()) & "'"

            MainClass.UOpenRecordSet(mSqlStr, PubDBCn, CursorTypeEnum.adOpenKeyset, RsPassword, LockTypeEnum.adLockOptimistic)

            If RsPassword.EOF = False Then
                txtUserName.Text = IIf(IsDBNull(RsPassword.Fields("USER_NAME").Value), "", RsPassword.Fields("USER_NAME").Value)
            Else
                MessageBox.Show("UserName is wrong", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                RsPassword.Close()
                txtUserID.Focus()
                Return
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function FieldVarification(mCompanyCode As Long) As Boolean
        Dim RsPassword As Recordset
        Dim RsModule As Recordset
        Dim mPassWord As String
        Dim mSqlStr As String = ""
        Dim mIsAdmin As String
        Dim mUserEncryptPassWord As String

        FieldVarification = False

        If Len(Trim(txtUserID.Text)) = 0 Then
            MessageBox.Show("Please enter user name", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtUserID.Focus()
            Exit Function
        End If

        If Len(Trim(txtPassword.Text)) = 0 Then
            MessageBox.Show("Please enter password", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtPassword.Focus()
            Exit Function
        End If

        Try
            RsPassword = New Recordset
            RsModule = New Recordset

            mSqlStr = "Select * from ATH_PASSWORD_MST WHERE UPPER(USER_ID)='" & UCase(txtUserID.Text.ToString()) & "' AND COMPANY_CODE='" & mCompanyCode & "'"

            MainClass.UOpenRecordSet(mSqlStr, PubDBCn, CursorTypeEnum.adOpenKeyset, RsPassword, LockTypeEnum.adLockOptimistic)

            If RsPassword.EOF = False Then
                If cboLoginType.Text.ToString = "ADMIN" Then
                    mIsAdmin = IIf(IsDBNull(RsPassword.Fields("SUPER_USER").Value), "", RsPassword.Fields("SUPER_USER").Value)
                    mPassWord = IIf(IsDBNull(RsPassword.Fields("ADMIN_PASSWORD").Value), "", RsPassword.Fields("ADMIN_PASSWORD").Value)
                Else
                    mIsAdmin = "U"
                    mPassWord = IIf(IsDBNull(RsPassword.Fields("NEWPASSWORD").Value), "", RsPassword.Fields("NEWPASSWORD").Value)
                End If

                mUserEncryptPassWord = ToHexDump(CryptRC4(UCase(txtPassword.Text), "password"))

                'If mIsAdmin <> "A" Then
                '    MessageBox.Show("You have no Admin Rights", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                '    RsPassword.Close()
                '    cboLoginType.Focus()
                '    Exit Function
                'End If


                If UCase(mPassWord) = UCase(mUserEncryptPassWord) Then
                    'mSqlStr = "Select A.MODULENAME " & vbCrLf _
                    '         & "FROM GEN_MODULE_MST A, FIN_RIGHTS_MST B " & vbCrLf _
                    '         & "WHERE A.MODULEID=B.MODULEID AND RIGHTS='Y' " & vbCrLf _
                    '         & "AND UPPER(USERID)='" & UCase(txtUserID.Text.ToString()) & "'"
                    'MainClass.UOpenRecordSet(mSqlStr, PubDBCn, CursorTypeEnum.adOpenKeyset, RsModule, LockTypeEnum.adLockOptimistic)

                    'If RsModule.EOF = True Then
                    '    MessageBox.Show("You Have no Module Rights", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    '    RsPassword.Close()
                    '    txtPassword.Focus()
                    '    Exit Function
                    'End If

                Else
                    MessageBox.Show("Password is wrong", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    RsPassword.Close()
                    txtPassword.Focus()
                    Exit Function
                End If
            Else
                MessageBox.Show("UserName is wrong", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                RsPassword.Close()
                txtUserID.Focus()
                Exit Function
            End If


            FieldVarification = True
            Exit Function
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function
    Private Function VaildFYChk(ByRef mCheckFY As Long, ByRef mCompanyCode As Long) As Boolean
        On Error GoTo FillFYErr
        Dim SqlStr As String
        Dim RsCFYNo As ADODB.Recordset

        VaildFYChk = False

        SqlStr = "SELECT FYEAR FROM GEN_CMPYRDTL_TRN " & vbCrLf _
            & " WHERE COMPANY_CODE=" & Val(mCompanyCode) & " " & vbCrLf _
            & " AND START_DATE<=TO_DATE('" & VB6.Format(txtRunDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND END_DATE>=TO_DATE('" & VB6.Format(txtRunDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        '            & " AND START_DATE<=TO_DATE('" & vb6.Format(txtRundate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " _
        '            & " AND END_DATE>=TO_DATE('" & vb6.Format(txtRundate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, CursorTypeEnum.adOpenKeyset, RsCFYNo, LockTypeEnum.adLockOptimistic)

        If Not RsCFYNo.EOF Then
            If VB6.Format(mCheckFY, "0000") <> VB6.Format(RsCFYNo.Fields("FYEAR").Value, "0000") Then
                MsgInformation("Please Select Valid FYear")
            Else
                VaildFYChk = True
            End If
        End If
        Exit Function
FillFYErr:
        VaildFYChk = False
    End Function


    Private Sub EnableModule(pfrm As FormMain)
        Dim RsModule As Recordset
        Dim mSqlStr As String = ""
        Dim mCompanyCode As Long

        Try
            RsModule = New Recordset
            mCompanyCode = -1

            'pfrm.AccountModule.Visible = False
            'pfrm.AdminModule.Visible = False
            'pfrm.PayrollModule.Visible = False
            'pfrm.QualityModule.Visible = False
            'pfrm.TDSModule.Visible = False
            'pfrm.MISModule.Visible = False
            'pfrm.InventoryModule.Visible = False
            'pfrm.SaleModule.Visible = False
            'pfrm.ProductionModule.Visible = False
            'pfrm.CostingModule.Visible = False

            If MainClass.ValidateWithMasterTable(cboCompany.Text, "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo) = True Then
                mCompanyCode = Val(MasterNo)
            End If

            If PubSuperUser = "S" Or PubSuperUser = "A" Then
                mSqlStr = "SELECT 'YES' AS IS_RIGHTS, MST.MODULEID, MST.MODULENAME, MST.MODULE_CAPTION, MODULE_MENU_NAME, MODULE_SHOW_SEQ,MODULE_CAPTION" & vbCrLf _
                        & " FROM GEN_Module_MST MST " & vbCrLf _
                        & " WHERE STATUS='O' AND IS_GROUP='Y' " & vbCrLf _
                        & " ORDER BY MODULE_SHOW_SEQ"
            Else
                mSqlStr = "SELECT UPPER(RIGHTS) AS IS_RIGHTS, MST.MODULEID, MST.MODULENAME, MST.MODULE_CAPTION, MODULE_MENU_NAME, MODULE_SHOW_SEQ,MODULE_CAPTION" & vbCrLf _
                        & " FROM GEN_MODULERIGHT_MST IH, GEN_Module_MST MST " & vbCrLf _
                        & " WHERE IH.COMPANY_CODE=" & mCompanyCode & "" & vbCrLf _
                        & " AND IH.USERID='" & PubUserID & "'" & vbCrLf _
                        & " AND IH.MODULEID=MST.MODULEID AND STATUS='O' AND IS_GROUP='Y' " & vbCrLf _
                        & " AND UPPER(RIGHTS)='YES'  " & vbCrLf _
                        & " ORDER BY MODULE_SHOW_SEQ"
            End If

            'mSqlStr = "SELECT UPPER(RIGHTS) AS IS_RIGHTS, MST.MODULEID, MST.MODULENAME AS MODULE_MENU_NAME, MST.MODULE_CAPTION" & vbCrLf _
            '            & " FROM GEN_MODULERIGHT_MST IH, GEN_Module_MST MST " & vbCrLf _
            '            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            '            & " AND IH.USERID='" & PubUserID & "'" & vbCrLf _
            '            & " AND IH.MODULEID=MST.MODULEID AND STATUS='O' AND IS_GROUP='Y' " & vbCrLf _
            '            & " AND UPPER(RIGHTS)='YES'  " & vbCrLf _
            '            & " ORDER BY IH.MODULEID"

            MainClass.UOpenRecordSet(mSqlStr, PubDBCn, CursorTypeEnum.adOpenKeyset, RsModule, LockTypeEnum.adLockOptimistic)

            If RsModule.EOF = False Then
                'Do While RsModule.EOF = False
                '    If PubSuperUser = "S" Or PubSuperUser = "A" Then
                '        pfrm.AccountModule.Visible = True
                '        pfrm.AdminModule.Visible = True
                '        pfrm.PayrollModule.Visible = True
                '        pfrm.QualityModule.Visible = True
                '        pfrm.TDSModule.Visible = True
                '        pfrm.MISModule.Visible = True
                '        pfrm.InventoryModule.Visible = True
                '        pfrm.SaleModule.Visible = True
                '        pfrm.ProductionModule.Visible = True
                '        pfrm.CostingModule.Visible = True
                '    Else
                '        If RsModule.Fields("MODULEID").Value = pfrm.AccountModule.Tag Then
                '            pfrm.AccountModule.Visible = IIf(RsModule.Fields("IS_RIGHTS").Value = "YES", True, False)
                '        End If

                '        If RsModule.Fields("MODULEID").Value = pfrm.AdminModule.Tag Then
                '            pfrm.AdminModule.Visible = IIf(RsModule.Fields("IS_RIGHTS").Value = "YES", True, False)
                '        End If

                '        If RsModule.Fields("MODULEID").Value = pfrm.PayrollModule.Tag Then
                '            pfrm.PayrollModule.Visible = IIf(RsModule.Fields("IS_RIGHTS").Value = "YES", True, False)
                '        End If

                '        If RsModule.Fields("MODULEID").Value = pfrm.ProductionModule.Tag Then
                '            pfrm.ProductionModule.Visible = IIf(RsModule.Fields("IS_RIGHTS").Value = "YES", True, False)
                '        End If

                '        If RsModule.Fields("MODULEID").Value = pfrm.QualityModule.Tag Then
                '            pfrm.QualityModule.Visible = IIf(RsModule.Fields("IS_RIGHTS").Value = "YES", True, False)
                '        End If

                '        If RsModule.Fields("MODULEID").Value = pfrm.TDSModule.Tag Then
                '            pfrm.TDSModule.Visible = IIf(RsModule.Fields("IS_RIGHTS").Value = "YES", True, False)
                '        End If

                '        If RsModule.Fields("MODULEID").Value = pfrm.MISModule.Tag Then
                '            pfrm.MISModule.Visible = IIf(RsModule.Fields("IS_RIGHTS").Value = "YES", True, False)
                '        End If

                '        If RsModule.Fields("MODULEID").Value = pfrm.InventoryModule.Tag Then
                '            pfrm.InventoryModule.Visible = IIf(RsModule.Fields("IS_RIGHTS").Value = "YES", True, False)
                '        End If

                '        If RsModule.Fields("MODULEID").Value = pfrm.SaleModule.Tag Then
                '            pfrm.SaleModule.Visible = IIf(RsModule.Fields("IS_RIGHTS").Value = "YES", True, False)
                '        End If

                '        If RsModule.Fields("MODULEID").Value = pfrm.CostingModule.Tag Then
                '            pfrm.CostingModule.Visible = IIf(RsModule.Fields("IS_RIGHTS").Value = "YES", True, False)
                '        End If
                '    End If
                '    RsModule.MoveNext()
                'Loop
            Else
                MsgBox(" You have Not rights In  any Module Master." & vbCrLf _
                & "Application Aborted.........................! ", vbExclamation)
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cboCompany_Click(sender As Object, e As System.EventArgs) Handles cboCompany.Click
        'Call FillFYNo()
    End Sub

    Private Sub cboCompany_Leave(sender As Object, e As System.EventArgs) Handles cboCompany.Leave
        Call FillFYNo()
    End Sub
    Private Sub cboCompany_Validated(sender As Object, e As System.EventArgs) Handles cboCompany.Validated
        'Call FillBranchCombo(cboCompany.Text)

    End Sub


    Private Sub FillFYNo()
        On Error GoTo FillFYErr
        Dim SqlStr As String = ""
        Dim RsFYNo As ADODB.Recordset = Nothing
        Dim mFYNo As String
        Dim mCCode As Long

        If Trim(cboCompany.Text) = "" Then Exit Sub

        mCCode = IIf(MainClass.ValidateWithMasterTable(cboCompany.Text, "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo) = True, MasterNo, -1)

        cboFYear.Items.Clear()

        SqlStr = "SELECT FYEAR,START_DATE,END_DATE FROM GEN_CMPYRDTL_TRN" _
                & " WHERE COMPANY_CODE=" & Val(mCCode) & " " _
                & " ORDER BY FYEAR"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, CursorTypeEnum.adOpenKeyset, RsFYNo, LockTypeEnum.adLockOptimistic)


        If Not RsFYNo.EOF Then
            RsFYNo.MoveFirst()
            Do While Not RsFYNo.EOF
                mFYNo = RsFYNo.Fields("FYEAR").Value
                cboFYear.Items.Add(mFYNo)      ''cboFYear.AddItem(VB6.Format(mFYNo, "00"))
                RsFYNo.MoveNext()
            Loop
            Call SetCurrentFYNO()
            'Call FillLblFYDateFromTo()
        End If
        Exit Sub
FillFYErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub SetCurrentFYNO()

        On Error GoTo FillFYErr
        Dim SqlStr As String = ""
        Dim RsCFYNo As ADODB.Recordset = Nothing
        Dim mCCode As Long
        Dim mDate As String
        Dim mDateTime As Date

        mCCode = IIf(MainClass.ValidateWithMasterTable(cboCompany.Text, "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo) = True, MasterNo, -1)

        mDateTime = txtRunDate.Text
        mDate = mDateTime.ToString("dd-MMM-yyyy")

        ''SqlStr = "SELECT FYEAR,START_DATE,END_DATE FROM GEN_CMPYRDTL_TRN" & " WHERE COMPANY_CODE=" & Val(mCCode) & " " & " AND START_DATE<=TO_DATE'" & txtRunDate.ToString("dd-MMM-yyyy") & "' AND END_DATE>='" & txtRunDate.ToString("dd-MMM-yyyy") & "' "

        SqlStr = "SELECT FYEAR,START_DATE,END_DATE FROM GEN_CMPYRDTL_TRN" &
            " WHERE COMPANY_CODE=" & Val(mCCode) & " " &
            " AND START_DATE<=TO_DATE('" & mDate & "','DD-MON-YYYY') " &
            " AND END_DATE>=TO_DATE('" & mDate & "','DD-MON-YYYY')"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCFYNo)
        If Not RsCFYNo.EOF Then
            cboFYear.Text = RsCFYNo.Fields("FYEAR").Value
        End If
        Exit Sub
FillFYErr:
        cboFYear.SelectedIndex = -1
    End Sub

    Private Sub txtPassword_Leave(sender As Object, e As System.EventArgs) Handles txtPassword.Leave
        Dim SqlStr As String = ""
        'Dim mStatus As String
        Dim mPassword As String
        Dim mCompanyCode As Long = -1
        Dim mUserEncryptPassWord As String = ""
        txtRunDate.Enabled = False

        mUserEncryptPassWord = ToHexDump(CryptRC4(UCase(txtPassword.Text), "password"))

        If Trim(txtUserID.Text) <> "" Then

            If Trim(cboCompany.Text) <> "" Then
                If MainClass.ValidateWithMasterTable(cboCompany.Text, "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                    mCompanyCode = IIf(IsDBNull(MasterNo), -1, Val(MasterNo))
                    SqlStr = "COMPANY_CODE=" & Val(mCompanyCode) & " AND STATUS='O'"
                End If
            Else
                SqlStr = "UPPER(TRIM(NEWPASSWORD))='" & MainClass.AllowSingleQuote(Trim(UCase(mUserEncryptPassWord))) & "' AND STATUS='O'"
            End If

            If MainClass.ValidateWithMasterTable(txtUserID.Text, "USER_ID", "NEWPASSWORD", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , SqlStr) = True Then
                mPassword = IIf(IsDBNull(MasterNo), "", MasterNo)
                If Trim(UCase(mUserEncryptPassWord)) = Trim(UCase(mPassword)) Then
                    PubAllowRunDateChange = GetUserPermission("RUNDATE_CHANGE", "N", txtUserID.Text, Val(mCompanyCode))
                    If PubAllowRunDateChange = "Y" Then
                        txtRunDate.Enabled = True
                    Else
                        txtRunDate.Enabled = False
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub txtRunDate_Leave(sender As Object, e As System.EventArgs) Handles txtRunDate.Leave
        'Dim Cancel As Boolean = e.Cancel

        If Not IsDate(txtRunDate.Text) Then
            MsgInformation("Invalid Date")
            txtRunDate.Focus()
            'e.Cancel = True
            GoTo EventExitSub
        End If

        Call FillFYNo()
EventExitSub:
        'e.Cancel = Cancel
    End Sub
    Private Function ValidateERP() As Boolean
        On Error GoTo ValidateERR

        Dim mKey As String

        Dim mKey1 As String
        Dim mKey2 As String
        Dim mKey3 As String
        Dim mKey4 As String
        Dim mKey5 As String
        Dim mKey6 As String
        Dim mKey7 As String
        Dim mKey8 As String
        Dim mKey9 As String
        Dim mKey10 As String

        Dim mDate As String

        Dim mCurrKey As String
        Dim mCurrDate As String

        mKey = IIf(IsDBNull(RsCompany.Fields("K_NO").Value), "", RsCompany.Fields("K_NO").Value)

        If Trim(mKey) = "" Or Len(mKey) <> 10 Then
            ValidateERP = True
            Exit Function
        End If

        mKey1 = Mid(mKey, 1, 1) 'D
        mKey2 = Mid(mKey, 2, 1) 'Y
        mKey3 = Mid(mKey, 3, 1) 'D
        mKey4 = Mid(mKey, 4, 1) 'Y
        mKey5 = Mid(mKey, 5, 1) 'C
        mKey6 = Mid(mKey, 6, 1) 'M
        mKey7 = Mid(mKey, 7, 1) 'Y
        mKey8 = Mid(mKey, 8, 1) 'M
        mKey9 = Mid(mKey, 9, 1) 'Y
        mKey10 = Mid(mKey, 10, 1) 'C

        '' DDMMx YYYYx ''DYDYxMYMYx

        mCurrKey = mKey2 & mKey4 & mKey7 & mKey9 & mKey6 & mKey8 & mKey1 & mKey3           ''mKey1 & mKey3 & mKey5 & mKey7 & mKey2 & mKey4 & mKey6 & mKey8
        mDate = mKey1 & mKey3 & "/" & mKey6 & mKey8 & "/" & mKey2 & mKey4 & mKey7 & mKey9

        If IsDate(CDate(mDate)) = False Then
            ValidateERP = False
            Exit Function
        End If

        mCurrDate = VB6.Format(PubCurrDate, "YYYYMMDD")

        If Val(mCurrDate) > Val(mCurrKey) Then
            ValidateERP = False
            Exit Function
        End If

        ValidateERP = True
        Exit Function
ValidateERR:
        ValidateERP = False
        MsgBox(Err.Description)
    End Function
    Private Function ValidateIP() As Boolean
        On Error GoTo ValidateERR
        Dim SqlStr As String = ""
        Dim RsUsers As ADODB.Recordset = Nothing
        Dim mUserIP As String

        SqlStr = "SELECT * FROM ATH_PASSWORD_MST " _
                & " WHERE UPPER(LTRIM(RTRIM(USER_ID))) = '" & MainClass.AllowSingleQuote(UCase(txtUserID.Text)) & "' AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, CursorTypeEnum.adOpenKeyset, RsUsers, LockTypeEnum.adLockOptimistic)
        If RsUsers.EOF = True Then
            MsgBox("User already Logged in by this User ID")
            ValidateIP = False
            Exit Function
        End If

        mUserIP = IIf(IsDBNull(RsUsers.Fields("USER_IP").Value), "", RsUsers.Fields("USER_IP").Value)

        If mUserIP = "" Then
            ValidateIP = True
            Exit Function
        End If

        If InStr(1, PubTerminalIPAddress, mUserIP) = 0 Then
            MsgInformation("Invalid User for this Terminal")
            ValidateIP = False
            Exit Function
        End If


        ValidateIP = True
        Exit Function
ValidateERR:
        ValidateIP = False
        MsgBox(Err.Description)
    End Function
    Private Function ValidateWindowUser() As Boolean
        On Error GoTo ValidateERR
        Dim SqlStr As String = ""
        Dim RsUsers As ADODB.Recordset = Nothing
        Dim mWindowUser As String

        SqlStr = "SELECT * FROM ATH_PASSWORD_MST " _
                & " WHERE UPPER(LTRIM(RTRIM(USER_ID))) = '" & MainClass.AllowSingleQuote(UCase(txtUserID.Text)) & "' AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, CursorTypeEnum.adOpenKeyset, RsUsers, LockTypeEnum.adLockOptimistic)

        If RsUsers.EOF = True Then
            MsgBox("User already Logged in by this User ID")
            ValidateWindowUser = False
            Exit Function
        End If

        mWindowUser = IIf(IsDBNull(RsUsers.Fields("WINDOW_USER").Value), "", RsUsers.Fields("WINDOW_USER").Value)

        If mWindowUser = "" Then
            ValidateWindowUser = True
            Exit Function
        End If

        If Trim(UCase(mWindowUser)) = Trim(UCase(PubDomainUserName)) Then
            ValidateWindowUser = True
            Exit Function
        Else
            MsgInformation("Invalid User for this User ID")
            ValidateWindowUser = False
            Exit Function
        End If

        ValidateWindowUser = True
        Exit Function
ValidateERR:
        ValidateWindowUser = False
        MsgBox(Err.Description)
    End Function

    Private Sub cboFYear_Validating(sender As Object, e As CancelEventArgs) Handles cboFYear.Validating
        Dim mCompanyCode As Long
        Dim SqlStr As String
        Dim pCurrDate As String
        Dim mCurrentFY As Integer
        Dim mRunDateFY As String

        Try
            If Trim(cboCompany.Text) <> "" Then
                If MainClass.ValidateWithMasterTable(cboCompany.Text, "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                    mCompanyCode = IIf(IsDBNull(MasterNo), -1, Val(MasterNo))
                    SqlStr = "COMPANY_CODE=" & Val(mCompanyCode) & " AND STATUS='O'"
                End If
                PubAllowRunDateChange = GetUserPermission("RUNDATE_CHANGE", "N", txtUserID.Text, Val(mCompanyCode))
                If PubAllowRunDateChange = "Y" Then
                    pCurrDate = VB6.Format(Convert.ToDateTime(GetServerDate), "dd/MM/yyyy")
                    mCurrentFY = GetCurrentFYNo(PubDBCn, pCurrDate)
                    If Val(mCurrentFY) = Val(cboFYear.Text) Then
                        mRunDateFY = GetCurrentFYNo(PubDBCn, txtRunDate.Text)
                        If Val(mRunDateFY) = Val(cboFYear.Text) Then
                        Else
                            txtRunDate.Text = VB6.Format(Convert.ToDateTime(GetServerDate), "dd/MM/yyyy")
                        End If
                    Else
                        txtRunDate.Text = GetFYStartEndDate(PubDBCn, "END_DATE", cboFYear.Text)
                    End If
                End If
            End If



        Catch ex As Exception

        End Try
    End Sub
End Class
