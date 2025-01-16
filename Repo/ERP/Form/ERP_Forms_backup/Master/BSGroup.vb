Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmBSGroup
   Inherits System.Windows.Forms.Form
   Dim RsBSGroup As ADODB.Recordset ''ADODB.Recordset		
   Dim ADDMode As Boolean
   Dim MODIFYMode As Boolean
   Dim XRIGHT As String
   Dim FormActive As Boolean
   Dim mGroupCode As Integer
   'Private PvtDBCn As ADODB.Connection		
   Private Sub ViewGrid()

      Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
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
      MainClass.ButtonStatus(Me, XRIGHT, RsBSGroup, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
      Me.Cursor = System.Windows.Forms.Cursors.Default
   End Sub
   Private Sub Show1()

      On Error GoTo ShowErrPart
        Dim SqlStr As String = ""
        Dim mStatus As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If RsBSGroup.EOF = False Then
            mGroupCode = RsBSGroup.Fields("BSGROUP_Code").Value
            TxtGroupName.Text = IIf(IsDBNull(RsBSGroup.Fields("BSGROUP_Name").Value), "", RsBSGroup.Fields("BSGROUP_Name").Value)

            If IsDBNull(RsBSGroup.Fields("BSGROUP_PARENTCODE").Value) Or RsBSGroup.Fields("BSGROUP_PARENTCODE").Value = -1 Then
                TxtSubGroupName.Text = ""
                CboAcctType.Enabled = True
            Else
                If MainClass.ValidateWithMasterTable(RsBSGroup.Fields("BSGROUP_PARENTCODE").Value, "BSGROUP_Code", "BSGROUP_Name", "FIN_BSGROUP_MST", PubDBCn, MasterNo, , SqlStr) = True Then
                    TxtSubGroupName.Text = MasterNo
                    CboAcctType.Enabled = False
                End If
            End If

            CboAcctType.Text = SetAccountType((RsBSGroup.Fields("BSGROUP_ACCTTYPE").Value))
            txtScheduleNo.Text = IIf(IsDBNull(RsBSGroup.Fields("BSGROUP_SCHEDULENO").Value), "", RsBSGroup.Fields("BSGROUP_SCHEDULENO").Value)
            txtSeqNo.Text = IIf(IsDBNull(RsBSGroup.Fields("BSGROUP_SEQNO").Value), "", RsBSGroup.Fields("BSGROUP_SEQNO").Value)

            ChkPrintIn(0).CheckState = IIf(RsBSGroup.Fields("BSGROUP_PRINT").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            ChkPrintIn(1).CheckState = IIf(RsBSGroup.Fields("PLGROUP_PRINT").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            ChkPrintIn(2).CheckState = IIf(RsBSGroup.Fields("SCGROUP_PRINT").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            ChkPrintIn(3).CheckState = IIf(RsBSGroup.Fields("FUNDFLOW_PRINT").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            mStatus = IIf(IsDBNull(RsBSGroup.Fields("BSGROUP_STATUS").Value), "O", RsBSGroup.Fields("BSGROUP_STATUS").Value)

            If mStatus = "O" Then
                OptStatus(0).Checked = True
            Else
                OptStatus(1).Checked = True
            End If
        End If

        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsBSGroup, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
        '    Resume			
    End Sub

    Private Function SetAccountType(ByRef mAcctType As Short) As String
        On Error GoTo ERR1
        SetAccountType = ""
        If mAcctType = ConLiabilities Then
            SetAccountType = "Liabilities"
        ElseIf mAcctType = ConAssets Then
            SetAccountType = "Assets"
        ElseIf mAcctType = ConTradingAcct Then
            SetAccountType = "Trading Account"
        ElseIf mAcctType = ConPnLAcct Then
            SetAccountType = "Profit & Loss"
        ElseIf mAcctType = ConStock Then
            SetAccountType = "Stock"
        ElseIf mAcctType = ConIncome Then
            SetAccountType = "Income"
        ElseIf mAcctType = ConExpenses Then
            SetAccountType = "Expenses"
        End If
        Exit Function
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume			
    End Function
    Private Sub Clear1()

        TxtGroupName.Text = ""
        TxtSubGroupName.Text = ""
        CboAcctType.SelectedIndex = -1
        txtScheduleNo.Text = ""
        txtSeqNo.Text = ""
        ChkPrintIn(0).CheckState = System.Windows.Forms.CheckState.Unchecked
        ChkPrintIn(1).CheckState = System.Windows.Forms.CheckState.Unchecked
        ChkPrintIn(2).CheckState = System.Windows.Forms.CheckState.Unchecked
        ChkPrintIn(3).CheckState = System.Windows.Forms.CheckState.Unchecked
        OptStatus(0).Checked = True

        CboAcctType.Enabled = False

        Call AutoCompleteSearch("FIN_BSGROUP_MST", "BSGROUP_NAME", "", TxtGroupName)
        Call AutoCompleteSearch("FIN_BSGROUP_MST", "BSGROUP_NAME", "", TxtSubGroupName)
        Call AutoCompleteSearch("FIN_BSGROUP_MST", "TO_CHAR(BSGROUP_SEQNO)", "", txtSeqNo)


        MainClass.ButtonStatus(Me, XRIGHT, RsBSGroup, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Function FieldsVerification() As Boolean

        On Error GoTo FieldsVerificationErrpart
        Dim mSqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mBSCode As Double
        Dim mGroupName As String

        FieldsVerification = True
        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVerification = False
            Exit Function
        End If

        If TxtGroupName.Text = "" Then
            FieldsVerification = False
            MsgInformation("Group Name Missing")
            TxtGroupName.Focus()
            FieldsVerification = False
            Exit Function
        End If

        If CboAcctType.SelectedIndex = -1 Or CboAcctType.Text = "" Then
            FieldsVerification = False
            MsgInformation("Account Type Missing")
            CboAcctType.Focus()
            FieldsVerification = False
            Exit Function
        End If

        ''    If Trim(txtScheduleNo.Text) = "" Or Val(txtScheduleNo.Text) = 0 Then			
        ''        FieldsVerification = False			
        ''        MsgInformation "Schedule No. Missing"			
        ''        txtScheduleNo.SetFocus			
        ''        FieldsVerification = False			
        ''        Exit Function			
        ''    End If			

        If Trim(TxtGroupName.Text) = Trim(TxtSubGroupName.Text) Then
            MsgInformation("Cann't Be Placed in the same group")
            TxtSubGroupName.Focus()
            FieldsVerification = False
            Exit Function
        End If

        If MODIFYMode = True Then
            If OptStatus(1).Checked = True Then
                If MainClass.ValidateWithMasterTable(TxtGroupName.Text, "BSGROUP_NAME", "BSGROUP_CODE", "FIN_BSGROUP_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mBSCode = MasterNo
                Else
                    mBSCode = -1
                End If

                mSqlStr = " SELECT BSGROUP_NAME FROM FIN_BSGROUP_MST " & vbCrLf _
                   & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                   & " AND BSGROUP_PARENTCODE=" & mBSCode & " AND BSGROUP_STATUS='O'"

                MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then
                    mGroupName = IIf(IsDBNull(RsTemp.Fields("BSGROUP_NAME").Value), "", RsTemp.Fields("BSGROUP_NAME").Value)
                    MsgInformation("This is Parent of " & mGroupName & ", So cann't be closed.")
                    FieldsVerification = False
                    Exit Function
                End If

                mSqlStr = " SELECT GROUP_NAME FROM FIN_GROUP_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND (GROUP_BSCODEDR=" & mBSCode & " OR GROUP_BSCODECR=" & mBSCode & ") AND GROUP_STATUS='O'"

                MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then
                    mGroupName = IIf(IsDBNull(RsTemp.Fields("GROUP_NAME").Value), "", RsTemp.Fields("GROUP_NAME").Value)
                    MsgInformation("Already Link with " & mGroupName & " group, So cann't be closed.")
                    FieldsVerification = False
                    Exit Function
                End If
            End If
        End If

        Exit Function
FieldsVerificationErrpart:
        MsgBox(Err.Description)
        FieldsVerification = False
    End Function
    Private Sub FillCbo()


        CboAcctType.Items.Clear()
        CboAcctType.Items.Add("Liabilities")
        CboAcctType.Items.Add("Assets")
        CboAcctType.Items.Add("Trading Account")
        CboAcctType.Items.Add("Profit & Loss")
        CboAcctType.Items.Add("Stock")
        CboAcctType.Items.Add("Income")
        CboAcctType.Items.Add("Expenses")
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub CboAcctType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CboAcctType.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub CboAcctType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CboAcctType.SelectedIndexChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub ChkPrintIn_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkPrintIn.CheckStateChanged
        Dim Index As Short = ChkPrintIn.GetIndex(eventSender)

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            If TxtGroupName.Enabled = True Then TxtGroupName.Focus()
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click

        On Error GoTo DelErrPart
        Dim SqlStr As String = ""
        Dim mCode As Integer

        If CheckConsolidatedMaster("FIN_SUPP_CUST_MST") = True Then
            MsgInformation("Cann't be Delete.")
            Exit Sub
        End If

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = ""
        If TxtGroupName.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub
        If RsBSGroup.EOF Then Exit Sub
        If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
            If MainClass.ValidateWithMasterTable(TxtGroupName.Text, "BSGROUP_NAME", "BSGROUP_CODE", "FIN_BSGROUP_MST", PubDBCn, MasterNo, , SqlStr) = False Then GoTo DelErrPart

            mCode = MasterNo

            If CheckChildGroup(mCode) = True Then Exit Sub


            If InsertIntoDelAudit(PubDBCn, "FIN_BSGROUP_MST", (TxtGroupName.Text), RsBSGroup) = False Then GoTo DelErrPart
            If InsertIntoDeleteTrn(PubDBCn, "FIN_BSGROUP_MST", "BSGROUP_NAME", (TxtGroupName.Text)) = False Then GoTo DelErrPart

            SqlStr = " Delete from FIN_BSGROUP_MST " & vbCrLf & " WHERE BSGROUP_Name='" & MainClass.AllowSingleQuote(TxtGroupName.Text) & "'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

            PubDBCn.Execute(SqlStr)
            PubDBCn.CommitTrans()
            RsBSGroup.Requery() ''.Refresh			
            Clear1()
        End If
        Exit Sub
DelErrPart:
        'Resume			
        Call ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans() ''			
        RsBSGroup.Requery() ''.Refresh			
        'PubDBCn.Errors.Clear			

    End Sub
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsBSGroup, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
    End Sub
    Private Function Update1() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mParentcode As Integer
        Dim mAcctType As Short
        Dim mCategory As String
        Dim mGroupLevel As Integer
        Dim mScheduleNo As String
        Dim mBSGroup As String
        Dim mPLGroup As String
        Dim mSCGroup As String
        Dim mStatus As String
        Dim mFundFlow As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xCompanyCode As Long

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(TxtSubGroupName.Text, "BSGROUP_Name", "BSGROUP_Code", "FIN_BSGROUP_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            mParentcode = MasterNo
        Else
            mParentcode = -1
        End If

        mAcctType = GetAccountType((CboAcctType.Text))
        mCategory = "H"
        mGroupLevel = -1
        mScheduleNo = Trim(txtScheduleNo.Text)
        mBSGroup = IIf(ChkPrintIn(0).CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mPLGroup = IIf(ChkPrintIn(1).CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mSCGroup = IIf(ChkPrintIn(2).CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mFundFlow = IIf(ChkPrintIn(3).CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mStatus = IIf(OptStatus(0).Checked = True, "O", "C")
        If ADDMode = True Then
            mGroupCode = MainClass.AutoGenRowNo("BSGROUP", "Code", PubDBCn)
        End If

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If CheckConsolidatedMaster("FIN_SUPP_CUST_MST") = True Then
            SqlStr = "SELECT COMPANY_CODE FROM GEN_COMPANY_MST"
        Else
            SqlStr = "SELECT COMPANY_CODE FROM GEN_COMPANY_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                xCompanyCode = RsTemp.Fields("COMPANY_CODE").Value
                If ADDMode = True Then

                    SqlStr = ""
                    SqlStr = " INSERT INTO FIN_BSGROUP_MST ( " & vbCrLf _
                       & " COMPANY_CODE,BSGROUP_CODE, BSGROUP_NAME, BSGROUP_PARENTCODE, " & vbCrLf _
                       & " BSGROUP_ACCTTYPE, BSGROUP_CATEGORY, BSGROUP_GROUPLEVEL, BSGROUP_SCHEDULENO, " & vbCrLf _
                       & " BSGROUP_PRINT, PLGROUP_PRINT, SCGROUP_PRINT,  " & vbCrLf _
                       & " BSGROUP_STATUS,BSGROUP_SEQNO, FUNDFLOW_PRINT ) " & vbCrLf _
                       & " VALUES ( " & vbCrLf _
                       & " " & xCompanyCode & ", " & vbCrLf _
                       & " " & mGroupCode & ",'" & MainClass.AllowSingleQuote(TxtGroupName.Text) & "', " & vbCrLf _
                       & " " & mParentcode & ", " & mAcctType & ", '" & mCategory & "'," & vbCrLf _
                       & " " & mGroupLevel & ", '" & mScheduleNo & "', " & vbCrLf _
                       & " '" & mBSGroup & "', '" & mPLGroup & "', '" & mSCGroup & "', " & vbCrLf _
                       & " '" & mStatus & "'," & Val(txtSeqNo.Text) & ",'" & mFundFlow & "') "
                Else
                    SqlStr = ""
                    SqlStr = " UPDATE FIN_BSGROUP_MST SET  " & vbCrLf _
                       & " BSGROUP_NAME= '" & MainClass.AllowSingleQuote(TxtGroupName.Text) & "' ," & vbCrLf _
                       & " BSGROUP_PARENTCODE= " & mParentcode & " ," & vbCrLf _
                       & " BSGROUP_ACCTTYPE= " & mAcctType & " ," & vbCrLf _
                       & " BSGROUP_CATEGORY= '" & mCategory & "' ," & vbCrLf _
                       & " BSGROUP_GROUPLEVEL= " & mGroupLevel & " ," & vbCrLf _
                       & " BSGROUP_SCHEDULENO= '" & mScheduleNo & "' ," & vbCrLf _
                       & " BSGROUP_PRINT= '" & mBSGroup & "' ," & vbCrLf _
                       & " PLGROUP_PRINT= '" & mPLGroup & "' ," & vbCrLf _
                       & " SCGROUP_PRINT= '" & mSCGroup & "' ," & vbCrLf _
                       & " BSGROUP_STATUS= '" & mStatus & "' ," & vbCrLf _
                       & " FUNDFLOW_PRINT='" & mFundFlow & "'," & vbCrLf _
                       & " BSGROUP_SEQNO=" & Val(txtSeqNo.Text) & "" & vbCrLf _
                       & " Where COMPANY_CODE=" & xCompanyCode & " " & vbCrLf _
                       & " AND BSGROUP_Code = " & mGroupCode & " "
                End If
                PubDBCn.Execute(SqlStr)
                If UpdateChildGroup(mGroupCode, mAcctType, xCompanyCode) = False Then GoTo ErrPart
                RsTemp.MoveNext()
            Loop
        End If

        PubDBCn.CommitTrans()

        Update1 = True
        Exit Function
ErrPart:
        ''Resume			
        MsgBox(Err.Description)
        Update1 = False
        PubDBCn.RollbackTrans() ''			
        PubDBCn.Errors.Clear()
        RsBSGroup.Requery()
    End Function
    Private Function UpdateChildGroup(ByRef pGroupCode As Integer, ByRef pAcctType As Short, xCompanyCode As Long) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing ''ADODB.Recordset			

        SqlStr = " SELECT * " & vbCrLf _
           & " FROM FIN_BSGROUP_MST " & vbCrLf _
           & " WHERE BSGROUP_CODE <>" & pGroupCode & " " & vbCrLf _
           & " AND COMPANY_CODE=" & xCompanyCode & " " & vbCrLf _
           & " START WITH  BSGROUP_CODE=" & pGroupCode & " " & vbCrLf _
           & " AND COMPANY_CODE=" & xCompanyCode & " " & vbCrLf _
           & " CONNECT BY PRIOR BSGROUP_CODE= BSGROUP_ParentCode " & vbCrLf _
           & " AND COMPANY_CODE=" & xCompanyCode & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        If RS.EOF = False Then
            Do While Not RS.EOF
                SqlStr = " Update FIN_BSGROUP_MST SET BSGROUP_ACCTTYPE=" & pAcctType & "" & vbCrLf _
                   & " WHERE BSGROUP_CODE=" & RS.Fields("BSGROUP_Code").Value & " AND COMPANY_CODE=" & xCompanyCode & ""

                PubDBCn.Execute(SqlStr)
                RS.MoveNext()
            Loop
        End If
        UpdateChildGroup = True
        RS = Nothing
        Exit Function
ErrPart:
        MsgInformation(Err.Description & " Child Record not updated")
        UpdateChildGroup = False
    End Function
    Private Function CheckChildGroup(ByRef pGroupCode As Integer) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing ''ADODB.Recordset			

        SqlStr = " SELECT * " & vbCrLf _
           & " FROM FIN_BSGROUP_MST " & vbCrLf _
           & " WHERE BSGROUP_CODE <>" & pGroupCode & " " & vbCrLf _
           & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
           & " START WITH  BSGROUP_CODE=" & pGroupCode & " " & vbCrLf _
           & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
           & " CONNECT BY PRIOR BSGROUP_CODE= BSGROUP_ParentCode " & vbCrLf _
           & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        If RS.EOF = True Then
            CheckChildGroup = False
        Else
            MsgInformation("Cann't Delete. Child Record Found")
            CheckChildGroup = True
        End If
        RS = Nothing
        Exit Function
ErrPart:
        MsgBox(Err.Description & " Unable to Delete", MsgBoxStyle.Critical)
        CheckChildGroup = True
        RS = Nothing
    End Function
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ShowReport(Crystal.DestinationConstants.crptToWindow)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ShowReport(Crystal.DestinationConstants.crptToPrinter)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo SaveErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVerification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        If Update1() = True Then
            ADDMode = False
            MODIFYMode = False
            txtGroupName_Validating(TxtGroupName, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True Then CmdAdd.Focus()
        Else
            MsgInformation("Record Not Saved")
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
SaveErrPart:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        If Err.Description = "" Then Exit Sub
        MsgBox(Err.Description)
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        'If MainClass.SearchMaster(TxtGroupName.Text, "FIN_BSGROUP_MST", "BSGROUP_NAME", SqlStr) = True Then
        If MainClass.SearchGridMaster((TxtGroupName.Text), "FIN_BSGROUP_MST", "BSGROUP_NAME", "BSGROUP_CODE", , , SqlStr) = True Then
            TxtGroupName.Text = AcName
            txtGroupName_Validating(TxtGroupName, New System.ComponentModel.CancelEventArgs(True))
        End If
    End Sub
    Private Sub CmdSearchGroup_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearchGroup.Click
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        'If MainClass.SearchMaster(TxtSubGroupName.Text, "FIN_BSGROUP_MST", "BSGROUP_NAME", SqlStr) = True Then
        If MainClass.SearchGridMaster((TxtSubGroupName.Text), "FIN_BSGROUP_MST", "BSGROUP_NAME", "BSGROUP_CODE", , , SqlStr) = True Then
            TxtSubGroupName.Text = AcName
            If TxtSubGroupName.Enabled = True Then TxtSubGroupName.Focus()
        End If
    End Sub
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ViewGrid()
        Me.Cursor = System.Windows.Forms.Cursors.Arrow
    End Sub

    Private Sub frmBSGroup_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        AssignGrid(False)
        SqlStr = "Select * From FIN_BSGROUP_MST WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBSGroup, ADODB.LockTypeEnum.adLockReadOnly)
        Call SetTextLength()
        Clear1()

        If CmdAdd.Enabled = True Then cmdAdd_Click(CmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '   Resume			
    End Sub

    Private Sub frmBSGroup_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub frmBSGroup_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub OptStatus_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptStatus.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptStatus.GetIndex(eventSender)

            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = eventArgs.row
        TxtGroupName.Text = Trim(SprdView.Text)
        txtGroupName_Validating(TxtGroupName, New System.ComponentModel.CancelEventArgs(True))
        Call CmdView_Click(CmdView, New System.EventArgs())
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Private Sub TxtGroupName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtGroupName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub TxtGroupName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtGroupName.DoubleClick
        cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Sub TxtGroupName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtGroupName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtGroupName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtGroupName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtGroupName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            cmdsearch_Click(cmdsearch, New System.EventArgs())
        End If
    End Sub
    Private Sub txtGroupName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtGroupName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(TxtGroupName.Text) = "" Then GoTo EventExitSub

        If MODIFYMode = True And RsBSGroup.EOF = False Then mGroupCode = RsBSGroup.Fields("BSGROUP_Code").Value

        SqlStr = "Select * from FIN_BSGROUP_MST WHERE " & vbCrLf _
           & " BSGROUP_NAME='" & MainClass.AllowSingleQuote(Trim(TxtGroupName.Text)) & "'" & vbCrLf _
           & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBSGroup, ADODB.LockTypeEnum.adLockReadOnly)
        If RsBSGroup.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Group Does Not Exist, Click Add To Add In Master")
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * from FIN_BSGROUP_MST WHERE " & vbCrLf _
                   & " BSGROUP_Code=" & mGroupCode & "" & vbCrLf _
                   & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBSGroup, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtScheduleNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtScheduleNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtScheduleNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtScheduleNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtScheduleNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSeqNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSeqNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSeqNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSeqNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtSubGroupName_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtSubGroupName.Click

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtSubGroupName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtSubGroupName.DoubleClick
        CmdSearchGroup_Click(CmdSearchGroup, New System.EventArgs())
    End Sub

    Private Sub TxtSubGroupName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtSubGroupName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtSubGroupName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtSubGroupName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtSubGroupName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            CmdSearchGroup_Click(CmdSearchGroup, New System.EventArgs())
        End If
    End Sub
    Private Sub frmBSGroup_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        Dim ii As Short
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection			
        ''PvtDBCn.Open StrConn			
        Call SetMainFormCordinate(Me)
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        Call FillCbo()
        CmdView.Text = ConCmdGridViewCaption
        ADDMode = False
        MODIFYMode = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume			
    End Sub
    Private Sub SetTextLength()
        On Error GoTo ERR1
        TxtGroupName.MaxLength = RsBSGroup.Fields("BSGROUP_NAME").DefinedSize ''			
        TxtSubGroupName.MaxLength = RsBSGroup.Fields("BSGROUP_NAME").DefinedSize ''			
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Private Sub frmBSGroup_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        RsBSGroup = Nothing
    End Sub
    Private Sub ShowReport(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        '    Dim mTitle As String = ""			
        '    mTitle = ""			
        '    Report1.Reset			
        '    mTitle = "List Of Account Group"			
        '    Report1.ReportFileName = App.Path & "\reports\BSGroup.rpt"			
        '    SetCrpt Report1, Mode, 1, mTitle			
        '    Report1.WindowShowGroupTree = False			
        '    Report1.Action = 1			
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        '    SqlStr = " SELECT LPAD(' ',4*(LEVEL-1)) || BSGROUP_NAME as BSGROUP_NAME, " & vbCrLf _			
        ''            & " CASE WHEN BSGROUP_ACCTTYPE=" & ConLiabilities & "  THEN 'Liabilities' " & vbCrLf _			
        ''            & " WHEN BSGROUP_ACCTTYPE=" & ConAssets & "  THEN 'Assets' " & vbCrLf _			
        ''            & " WHEN BSGROUP_ACCTTYPE=" & ConTradingAcct & "  THEN 'Trading Account' " & vbCrLf _			
        ''            & " WHEN BSGROUP_ACCTTYPE=" & ConPnLAcct & "  THEN 'Profit and Loss' " & vbCrLf _			
        ''            & " WHEN BSGROUP_ACCTTYPE=" & ConStock & " THEN 'Stock' " & vbCrLf _			
        ''            & " WHEN BSGROUP_ACCTTYPE=" & ConIncome & "  THEN 'Income' " & vbCrLf _			
        ''            & " WHEN BSGROUP_ACCTTYPE=" & ConExpenses & "  THEN 'Expenses' END as AccountType, " & vbCrLf _			
        ''            & " TO_CHAR(BSGROUP_SCHEDULENO) AS SCHEDULENO," & vbCrLf _			
        ''            & " DECODE(BSGROUP_PRINT,'Y','YES','NO') AS IS_BSGROUP," & vbCrLf _			
        ''            & " DECODE(PLGROUP_PRINT,'Y','YES','NO') AS IS_PLGROUP," & vbCrLf _			
        ''            & " DECODE(SCGROUP_PRINT,'Y','YES','NO') AS IS_SCGROUP" & vbCrLf _			
        ''            & " FROM FIN_BSGROUP_MST WHERE BSGROUP_CATEGORY='H'" & vbCrLf _			
        ''            & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _			
        ''            & " START WITH  BSGROUP_PARENTCODE= -1 " & vbCrLf _			
        ''            & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _			
        ''            & " CONNECT BY PRIOR BSGROUP_CODE =BSGROUP_PARENTCODE " & vbCrLf _			
        ''            & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "			
        '			
        SqlStr = " SELECT LPAD(' ',4*(LEVEL-1)) || BSGROUP_NAME as BSGROUP_NAME " & vbCrLf _
           & " FROM FIN_BSGROUP_MST " & vbCrLf _
           & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
           & " START WITH  BSGROUP_ParentCode=-1 " & vbCrLf _
           & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
           & " CONNECT BY PRIOR BSGROUP_CODE= BSGROUP_ParentCode " & vbCrLf _
           & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 12)
            .set_ColWidth(0, 5)
            .set_ColWidth(1, 30)
            .set_ColWidth(2, 12)
            .set_ColWidth(3, 12)
            .set_ColWidth(4, 12)
            .set_ColWidth(5, 12)
            .set_ColWidth(6, 12)
            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)

        End With
    End Sub
    Private Sub TxtSubGroupName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtSubGroupName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim pAcctType As Short
        Dim SqlStr As String = ""

        If Trim(TxtSubGroupName.Text) = "" Then
            CboAcctType.Enabled = True
            CboAcctType.Focus()
            GoTo EventExitSub
        End If

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable(Trim(TxtSubGroupName.Text), "BSGROUP_Name", "BSGROUP_ACCTTYPE", "FIN_BSGROUP_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            pAcctType = MasterNo
            CboAcctType.Text = SetAccountType(pAcctType)
            CboAcctType.Enabled = False
            'Else
            '   MsgInformation("Invalid Parent Name")
            '   Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
   Private Function GetAccountType(ByRef mAcctType As String) As Short
      On Error GoTo ERR1
      If mAcctType = "Liabilities" Then
         GetAccountType = ConLiabilities
      ElseIf mAcctType = "Assets" Then
         GetAccountType = ConAssets
      ElseIf mAcctType = "Trading Account" Then
         GetAccountType = ConTradingAcct
      ElseIf mAcctType = "Profit & Loss" Then
         GetAccountType = ConPnLAcct
      ElseIf mAcctType = "Stock" Then
         GetAccountType = ConStock
      ElseIf mAcctType = "Income" Then
         GetAccountType = ConIncome
      ElseIf mAcctType = "Expenses" Then
         GetAccountType = ConExpenses
      End If
      Exit Function
ERR1:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
      'Resume	
   End Function
End Class
