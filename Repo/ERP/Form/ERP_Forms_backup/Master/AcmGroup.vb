Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmAcmGroup
   Inherits System.Windows.Forms.Form
   Dim RsAcmGroup As ADODB.Recordset ''ADODB.Recordset
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
         fraMain.BringToFront()
      End If
      MainClass.ButtonStatus(Me, XRIGHT, RsAcmGroup, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
      Me.Cursor = System.Windows.Forms.Cursors.Default
   End Sub
   Private Sub Show1()

      On Error GoTo ShowErrPart
        Dim SqlStr As String = ""
        Dim mGroupHead As String
        Dim mStatus As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If RsAcmGroup.EOF = False Then
            mGroupCode = RsAcmGroup.Fields("GROUP_Code").Value
            TxtName.Text = IIf(IsDBNull(RsAcmGroup.Fields("GROUP_Name").Value), "", RsAcmGroup.Fields("GROUP_Name").Value)

            If IsDBNull(RsAcmGroup.Fields("GROUP_PARENTCODE").Value) Or RsAcmGroup.Fields("GROUP_PARENTCODE").Value = -1 Then
                TxtParentName.Text = ""
                fraBSGroup.Enabled = True
            Else
                If MainClass.ValidateWithMasterTable(RsAcmGroup.Fields("GROUP_PARENTCODE").Value, "GROUP_CODE", "GROUP_NAME", "FIN_GROUP_MST", PubDBCn, MasterNo, , SqlStr) = True Then
                    TxtParentName.Text = MasterNo
                    fraBSGroup.Enabled = False
                Else
                    TxtParentName.Text = ""
                    fraBSGroup.Enabled = True
                End If
            End If

            If IsDBNull(RsAcmGroup.Fields("GROUP_BSCodeDr").Value) Or RsAcmGroup.Fields("GROUP_BSCodeDr").Value = -1 Then
                TxtBSGroupDr.Text = ""
            Else
                If MainClass.ValidateWithMasterTable(RsAcmGroup.Fields("GROUP_BSCodeDr").Value, "BSGROUP_CODE", "BSGROUP_NAME", "FIN_BSGROUP_MST", PubDBCn, MasterNo, , SqlStr) = True Then
                    TxtBSGroupDr.Text = MasterNo
                Else
                    TxtBSGroupDr.Text = ""
                End If
            End If

            If IsDBNull(RsAcmGroup.Fields("GROUP_BSCodeCr").Value) Or RsAcmGroup.Fields("GROUP_BSCodeCr").Value = -1 Then
                TxtBSGroupCr.Text = ""
            Else
                If MainClass.ValidateWithMasterTable(RsAcmGroup.Fields("GROUP_BSCodeCr").Value, "BSGROUP_CODE", "BSGROUP_NAME", "FIN_BSGROUP_MST", PubDBCn, MasterNo, , SqlStr) = True Then
                    TxtBSGroupCr.Text = MasterNo
                Else
                    TxtBSGroupCr.Text = ""
                End If
            End If

            txtScheduleNo.Text = IIf(IsDBNull(RsAcmGroup.Fields("GROUP_SCHEDULENO").Value), "", RsAcmGroup.Fields("GROUP_SCHEDULENO").Value)
            txtSeqNo.Text = IIf(IsDBNull(RsAcmGroup.Fields("GROUP_SEQNO").Value), "", RsAcmGroup.Fields("GROUP_SEQNO").Value)
            txtMISSeqNo.Text = IIf(IsDBNull(RsAcmGroup.Fields("MIS_SEQNO").Value), "", RsAcmGroup.Fields("MIS_SEQNO").Value)
            mGroupHead = IIf(IsDBNull(RsAcmGroup.Fields("GROUP_HEAD").Value), "", RsAcmGroup.Fields("GROUP_HEAD").Value)

            If Trim(mGroupHead) = "" Then
                cboGroupType.SelectedIndex = -1
            Else
                If mGroupHead = "O1" Then
                    cboGroupType.SelectedIndex = 0
                ElseIf mGroupHead = "O2" Then
                    cboGroupType.SelectedIndex = 1
                ElseIf mGroupHead = "O3" Then
                    cboGroupType.SelectedIndex = 2
                ElseIf mGroupHead = "O4" Then
                    cboGroupType.SelectedIndex = 3
                ElseIf mGroupHead = "O5" Then
                    cboGroupType.SelectedIndex = 4
                ElseIf mGroupHead = "O6" Then
                    cboGroupType.SelectedIndex = 5
                ElseIf mGroupHead = "O7" Then
                    cboGroupType.SelectedIndex = 6
                ElseIf mGroupHead = "O8" Then
                    cboGroupType.SelectedIndex = 7
                ElseIf mGroupHead = "O9" Then
                    cboGroupType.SelectedIndex = 8
                ElseIf mGroupHead = "I1" Then
                    cboGroupType.SelectedIndex = 9
                ElseIf mGroupHead = "I2" Then
                    cboGroupType.SelectedIndex = 10
                ElseIf mGroupHead = "I3" Then
                    cboGroupType.SelectedIndex = 11
                ElseIf mGroupHead = "I4" Then
                    cboGroupType.SelectedIndex = 12
                ElseIf mGroupHead = "I5" Then
                    cboGroupType.SelectedIndex = 13
                ElseIf mGroupHead = "I6" Then
                    cboGroupType.SelectedIndex = 14
                ElseIf mGroupHead = "I7" Then
                    cboGroupType.SelectedIndex = 15
                ElseIf mGroupHead = "I8" Then
                    cboGroupType.SelectedIndex = 16
                ElseIf mGroupHead = "E1" Then
                    cboGroupType.SelectedIndex = 17
                ElseIf mGroupHead = "E2" Then
                    cboGroupType.SelectedIndex = 18
                ElseIf mGroupHead = "E3" Then
                    cboGroupType.SelectedIndex = 19
                ElseIf mGroupHead = "E4" Then
                    cboGroupType.SelectedIndex = 20
                ElseIf mGroupHead = "E5" Then
                    cboGroupType.SelectedIndex = 21
                ElseIf mGroupHead = "E6" Then
                    cboGroupType.SelectedIndex = 22
                ElseIf mGroupHead = "E7" Then
                    cboGroupType.SelectedIndex = 23
                ElseIf mGroupHead = "E8" Then
                    cboGroupType.SelectedIndex = 24
                ElseIf mGroupHead = "E9" Then
                    cboGroupType.SelectedIndex = 25
                ElseIf mGroupHead = "E10" Then
                    cboGroupType.SelectedIndex = 26
                ElseIf mGroupHead = "E11" Then
                    cboGroupType.SelectedIndex = 27
                ElseIf mGroupHead = "E12" Then
                    cboGroupType.SelectedIndex = 28
                ElseIf mGroupHead = "E13" Then
                    cboGroupType.SelectedIndex = 29
                ElseIf mGroupHead = "E14" Then
                    cboGroupType.SelectedIndex = 30
                ElseIf mGroupHead = "E15" Then
                    cboGroupType.SelectedIndex = 31
                ElseIf mGroupHead = "E16" Then
                    cboGroupType.SelectedIndex = 32
                ElseIf mGroupHead = "E17" Then
                    cboGroupType.SelectedIndex = 33
                ElseIf mGroupHead = "E18" Then
                    cboGroupType.SelectedIndex = 34
                ElseIf mGroupHead = "E19" Then
                    cboGroupType.SelectedIndex = 35
                ElseIf mGroupHead = "E20" Then
                    cboGroupType.SelectedIndex = 36
                ElseIf mGroupHead = "E21" Then
                    cboGroupType.SelectedIndex = 37
                ElseIf mGroupHead = "E22" Then
                    cboGroupType.SelectedIndex = 38
                ElseIf mGroupHead = "E23" Then
                    cboGroupType.SelectedIndex = 39
                ElseIf mGroupHead = "E24" Then
                    cboGroupType.SelectedIndex = 40
                ElseIf mGroupHead = "E25" Then
                    cboGroupType.SelectedIndex = 41
                ElseIf mGroupHead = "E26" Then
                    cboGroupType.SelectedIndex = 42
                ElseIf mGroupHead = "E27" Then
                    cboGroupType.SelectedIndex = 43
                ElseIf mGroupHead = "E28" Then
                    cboGroupType.SelectedIndex = 44
                ElseIf mGroupHead = "E29" Then
                    cboGroupType.SelectedIndex = 45
                ElseIf mGroupHead = "E30" Then
                    cboGroupType.SelectedIndex = 46
                ElseIf mGroupHead = "E31" Then
                    cboGroupType.SelectedIndex = 47
                ElseIf mGroupHead = "E32" Then
                    cboGroupType.SelectedIndex = 48
                ElseIf mGroupHead = "E33" Then
                    cboGroupType.SelectedIndex = 49
                ElseIf mGroupHead = "E34" Then
                    cboGroupType.SelectedIndex = 50
                Else
                    cboGroupType.SelectedIndex = 51
                End If
            End If
            If RsAcmGroup.Fields("GROUP_TYPE").Value = "E" Then
                optType(0).Checked = True
            ElseIf RsAcmGroup.Fields("GROUP_TYPE").Value = "G" Then
                optType(1).Checked = True
            ElseIf RsAcmGroup.Fields("GROUP_TYPE").Value = "D" Then
                optType(2).Checked = True
            Else
                optType(3).Checked = True
            End If

            mStatus = IIf(IsDBNull(RsAcmGroup.Fields("GROUP_STATUS").Value), "O", RsAcmGroup.Fields("GROUP_STATUS").Value)

            If mStatus = "O" Then
                OptStatus(0).Checked = True
            Else
                OptStatus(1).Checked = True
            End If

            mStatus = IIf(IsDBNull(RsAcmGroup.Fields("STOCK_GROUP").Value), "N", RsAcmGroup.Fields("STOCK_GROUP").Value)
            chkStockGroup.CheckState = IIf(mStatus = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)


            mStatus = IIf(IsDBNull(RsAcmGroup.Fields("STOCK_HEAD_TYPE").Value), "C", RsAcmGroup.Fields("STOCK_HEAD_TYPE").Value)

            If mStatus = "O" Then
                optOpeningBalHead.Checked = True
            Else
                optClosingBalHead.Checked = True
            End If

        End If

        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsAcmGroup, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
        'Resume	
    End Sub

    Private Sub Clear1()

        TxtName.Text = ""
        TxtParentName.Text = ""

        txtScheduleNo.Text = ""
        txtSeqNo.Text = ""
        txtMISSeqNo.Text = ""
        TxtBSGroupDr.Text = ""
        TxtBSGroupCr.Text = ""
        optType(0).Checked = True

        cboGroupType.SelectedIndex = -1
        OptStatus(0).Checked = True

        optClosingBalHead.Checked = True
        optOpeningBalHead.Checked = False

        fraBSGroup.Enabled = False
        chkStockGroup.CheckState = System.Windows.Forms.CheckState.Unchecked

        Call AutoCompleteSearch("FIN_GROUP_MST", "GROUP_NAME", "", TxtName)
        Call AutoCompleteSearch("FIN_GROUP_MST", "GROUP_NAME", "", TxtParentName)
        Call AutoCompleteSearch("FIN_BSGROUP_MST", "BSGROUP_NAME", "", TxtBSGroupDr)
        Call AutoCompleteSearch("FIN_BSGROUP_MST", "BSGROUP_NAME", "", TxtBSGroupCr)


        MainClass.ButtonStatus(Me, XRIGHT, RsAcmGroup, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Function FieldsVerification() As Boolean

        On Error GoTo FieldsVerificationErrpart
        Dim mSqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mGroupCode As Double
        Dim mGroupName As String

        FieldsVerification = True
        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVerification = False
            Exit Function
        End If

        If TxtName.Text = "" Then
            FieldsVerification = False
            MsgInformation("Group Name Missing")
            TxtName.Focus()
            FieldsVerification = False
            Exit Function
        End If

        If Trim(TxtName.Text) = Trim(TxtParentName.Text) Then
            FieldsVerification = False
            MsgInformation("Cann't Be Placed in the same group")
            TxtParentName.Focus()
            FieldsVerification = False
            Exit Function
        End If

        'If Val(txtScheduleNo.Text) = "" Then
        '    FieldsVerification = False
        '    MsgInformation("Schedule No Cann't be Blank")
        '    txtScheduleNo.Focus()
        '    FieldsVerification = False
        '    Exit Function
        'End If

        If Trim(TxtBSGroupDr.Text) = "" Then
            FieldsVerification = False
            MsgInformation("Balance Sheet : Debit Account Cann't be Blank")
            TxtBSGroupDr.Focus()
            FieldsVerification = False
            Exit Function
        End If

        If Trim(TxtBSGroupCr.Text) = "" Then
            FieldsVerification = False
            MsgInformation("Balance Sheet : Credit Account Cann't be Blank")
            TxtBSGroupCr.Focus()
            FieldsVerification = False
            Exit Function
        End If

        If optType(0).Checked = True And cboGroupType.SelectedIndex = -1 Then
            MsgInformation("Please select Group Type.")
            cboGroupType.Focus()
            FieldsVerification = False
            Exit Function
        End If

        If MODIFYMode = True Then
            If OptStatus(1).Checked = True Then
                If MainClass.ValidateWithMasterTable(TxtName.Text, "GROUP_NAME", "GROUP_CODE", "FIN_GROUP_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mGroupCode = MasterNo
                Else
                    mGroupCode = -1
                End If

                mSqlStr = " SELECT GROUP_NAME FROM FIN_GROUP_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND GROUP_PARENTCODE=" & mGroupCode & " AND GROUP_STATUS='O'"

                MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then
                    mGroupName = IIf(IsDBNull(RsTemp.Fields("GROUP_NAME").Value), "", RsTemp.Fields("GROUP_NAME").Value)
                    MsgInformation("This is Parent of " & mGroupName & ", So cann't be closed.")
                    FieldsVerification = False
                    Exit Function
                End If

                mSqlStr = " SELECT SUPP_CUST_NAME FROM FIN_SUPP_CUST_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND (GROUPCODE=" & mGroupCode & " OR GROUPCODE=" & mGroupCode & ")" '' AND GROUP_STATUS='O'"	

                MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then
                    mGroupName = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
                    MsgInformation("Already Link with " & mGroupName & " Account Ledger, So cann't be closed.")
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

    Private Sub cboGroupType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboGroupType.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboGroupType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboGroupType.SelectedIndexChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub FillComboBox()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        cboGroupType.Items.Clear()
        '    cboGroupType.AddItem ""	
        cboGroupType.Items.Add("O1 : -")
        cboGroupType.Items.Add("O2 : CURRENT LIABILITIES")
        cboGroupType.Items.Add("O3 : RESERVES & SURPLUS")
        cboGroupType.Items.Add("O4 : SECURED LOANS")
        cboGroupType.Items.Add("O5 : UNSECURED LOANS")
        cboGroupType.Items.Add("O6 : CURRENT ASSETS")
        cboGroupType.Items.Add("O7 : SHARE CAPITAL")
        cboGroupType.Items.Add("O8 : FIXED ASSETS")
        cboGroupType.Items.Add("O9 : DUTY & TAXES")
        cboGroupType.Items.Add("I1 : INCOME SALE")
        cboGroupType.Items.Add("I2 : INCOME OTHER SALE")
        cboGroupType.Items.Add("I3 : INCOME SALE SCRAP")
        cboGroupType.Items.Add("I4 : INCOME SALE JOBWORK")
        cboGroupType.Items.Add("I5 : INCOME SALE INTER-UNIT")
        cboGroupType.Items.Add("I6 : INCOME OTHER INCOME")
        cboGroupType.Items.Add("I7 : SALE RETURN")
        cboGroupType.Items.Add("I8 : SALE SUPPLIMENTRY")
        cboGroupType.Items.Add("E1 : MATERIAL COST")
        cboGroupType.Items.Add("E2 : POWER & FUEL")
        cboGroupType.Items.Add("E3 : FREIGHT OUTWARD")
        cboGroupType.Items.Add("E4 : FINANCE COST")
        cboGroupType.Items.Add("E5 : OTHER EXPENSES")
        cboGroupType.Items.Add("E6 : MANPOWER - PRODUCTION STAFF")
        cboGroupType.Items.Add("E7 : MANPOWER - GENERAL STAFF")
        cboGroupType.Items.Add("E8 : MANUFACTURING EXPENSES")
        cboGroupType.Items.Add("E9 : ADMINISTRATIVE EXPENSES")
        cboGroupType.Items.Add("E10 : DEPRECIATION")
        cboGroupType.Items.Add("E11 : PACKING EXPENSES")
        cboGroupType.Items.Add("E12 : JOBWORK SUB CONTACTING EXPENSES")
        cboGroupType.Items.Add("E13 : CORPORATE EXPENSES")
        cboGroupType.Items.Add("E14 : FREIGHT INWARD")
        cboGroupType.Items.Add("E15 : DUTY DRAW BACK")
        cboGroupType.Items.Add("E16 : FOREIGN EXCHANGE RATE FLUCTION")

        cboGroupType.Items.Add("E17 : INCREASE/(DECREASE) IN WIP & FG STOCKS")
        cboGroupType.Items.Add("E18 : BOP CONSUMPTION")
        cboGroupType.Items.Add("E19 : RAW MATERIAL WIP")
        cboGroupType.Items.Add("E20 : CONSUMPTION OF STORES & SPARES")
        cboGroupType.Items.Add("E21 : CUTTING TOOLS")
        cboGroupType.Items.Add("E22 : TOOLS & DIES")
        cboGroupType.Items.Add("E23 : GAS (LPG) & FURNACE OIL")
        cboGroupType.Items.Add("E24 : DIESEL")
        cboGroupType.Items.Add("E25 : PERSONNEL EXPENSES")
        cboGroupType.Items.Add("E26 : WORKMEN & STAFF WELFARE EXPENSES")
        cboGroupType.Items.Add("E27 : CONTRIBUTION TO PROVIDENT FUND & OTHER")
        cboGroupType.Items.Add("E28 : INTEREST")
        cboGroupType.Items.Add("E29 : INTEREST ON TERM LOAN")
        cboGroupType.Items.Add("E30 : INTEREST ON WORKING CAPITAL")
        cboGroupType.Items.Add("E31 : INTEREST ON UNSECURED LOANS")
        cboGroupType.Items.Add("E32 : BANK CHARGES")
        cboGroupType.Items.Add("E33 : SELLING EXPENSES")
        cboGroupType.Items.Add("E34 : BUSINESS PROMOTION")



        '    cboGroupType.AddItem "E11 : EXPORT EXPENSES"	
        cboGroupType.SelectedIndex = 0
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            If TxtName.Enabled = True Then TxtName.Focus()
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        On Error Resume Next
        '    If PvtDBCn.State = adStateOpen Then	
        '        PvtDBCn.Close	
        '        Set PvtDBCn = Nothing	
        '    End If	
        RsAcmGroup.Close()
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
        If TxtName.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub
        If RsAcmGroup.EOF Then Exit Sub
        If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
            If MainClass.ValidateWithMasterTable(TxtName.Text, "GROUP_NAME", "GROUP_CODE", "FIN_GROUP_MST", PubDBCn, MasterNo, , SqlStr) = False Then GoTo DelErrPart
            mCode = MasterNo

            If CheckChildGroup(mCode) = True Then Exit Sub


            If InsertIntoDelAudit(PubDBCn, "FIN_GROUP_MST", (TxtName.Text), RsAcmGroup) = False Then GoTo DelErrPart
            If InsertIntoDeleteTrn(PubDBCn, "FIN_GROUP_MST", "GROUP_NAME", (TxtName.Text)) = False Then GoTo DelErrPart

            SqlStr = " Delete from FIN_GROUP_MST " & vbCrLf & " WHERE GROUP_Name='" & MainClass.AllowSingleQuote(TxtName.Text) & "'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

            PubDBCn.Execute(SqlStr)
            PubDBCn.CommitTrans()
            RsAcmGroup.Requery() ''.Refresh	
            Clear1()
        End If
        Exit Sub
DelErrPart:
        'Resume	
        Call ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans() ''	
        RsAcmGroup.Requery() ''.Refresh	
        'PubDBCn.Errors.Clear	

    End Sub
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsAcmGroup, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
    End Sub
    Private Function Update1() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim pSqlStr As String = ""
        Dim mParentcode As Integer
        Dim mCategory As String
        Dim mGroupLevel As Integer
        Dim mStatus As String
        Dim mBSCodeDR As Integer
        Dim mBSCodeCR As Integer
        Dim mType As String
        Dim mGroupHead As String
        Dim RsTemp As ADODB.Recordset
        Dim xCompanyCode As Long
        Dim mGroupStock As String
        Dim mStockHeadType As String

        mCategory = "G"
        mGroupLevel = -1
        mStatus = IIf(OptStatus(0).Checked = True, "O", "C")
        mStockHeadType = IIf(optClosingBalHead.Checked = True, "C", "O")

        mGroupStock = IIf(chkStockGroup.CheckState = System.Windows.Forms.CheckState.Unchecked, "N", "Y")

        If optType(0).Checked = True Then
            mType = "E"
        ElseIf optType(1).Checked = True Then
            mType = "G"
        ElseIf optType(2).Checked = True Then
            mType = "D"
        Else
            mType = "C"
        End If
        If ADDMode = True Then
            mGroupCode = MainClass.AutoGenRowNo("GROUP", "Code", PubDBCn)
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
                pSqlStr = "COMPANY_CODE=" & xCompanyCode & ""

                mGroupHead = Trim(VB.Left(cboGroupType.Text, 3))
                If MainClass.ValidateWithMasterTable(TxtParentName.Text, "GROUP_Name", "GROUP_Code", "FIN_GROUP_MST", PubDBCn, MasterNo, , pSqlStr) = True Then
                    mParentcode = MasterNo
                Else
                    mParentcode = -1
                End If

                If MainClass.ValidateWithMasterTable(TxtBSGroupDr.Text, "BSGROUP_NAME", "BSGROUP_CODE", "FIN_BSGROUP_MST", PubDBCn, MasterNo, , pSqlStr) = True Then
                    mBSCodeDR = MasterNo
                Else
                    mBSCodeDR = -1
                End If

                If MainClass.ValidateWithMasterTable(TxtBSGroupCr.Text, "BSGROUP_NAME", "BSGROUP_CODE", "FIN_BSGROUP_MST", PubDBCn, MasterNo, , pSqlStr) = True Then
                    mBSCodeCR = MasterNo
                Else
                    mBSCodeCR = -1
                End If

                If ADDMode = True Then
                    SqlStr = ""
                    SqlStr = " INSERT INTO FIN_GROUP_MST ( " & vbCrLf _
                        & " COMPANY_CODE,GROUP_CODE, GROUP_NAME, GROUP_PARENTCODE, " & vbCrLf _
                        & " GROUP_BSCODEDR, GROUP_BSCODECR , GROUP_CATEGORY, GROUP_GROUPLEVEL,  " & vbCrLf _
                        & " GROUP_STATUS,GROUP_TYPE,GROUP_SCHEDULENO,GROUP_SEQNO,GROUP_HEAD,MIS_SEQNO,STOCK_GROUP,STOCK_HEAD_TYPE ) " & vbCrLf _
                        & " VALUES ( " & vbCrLf & " " & xCompanyCode & ", " & vbCrLf _
                        & " " & mGroupCode & ",'" & MainClass.AllowSingleQuote(TxtName.Text) & "', " & vbCrLf _
                        & " " & mParentcode & ", " & mBSCodeDR & ", " & mBSCodeCR & ", '" & mCategory & "'," & vbCrLf _
                        & " " & mGroupLevel & ",  " & vbCrLf _
                        & " '" & mStatus & "','" & mType & "','" & Trim(txtScheduleNo.Text) & "', " & Val(txtSeqNo.Text) & ",'" & mGroupHead & "'," & Val(txtMISSeqNo.Text) & ",'" & mGroupStock & "','" & mStockHeadType & "') "
                Else
                    SqlStr = ""
                    SqlStr = " UPDATE FIN_GROUP_MST SET  " & vbCrLf _
                        & " GROUP_NAME= '" & MainClass.AllowSingleQuote(TxtName.Text) & "' ," & vbCrLf _
                        & " GROUP_PARENTCODE= " & mParentcode & " ," & vbCrLf _
                        & " GROUP_BSCODEDR= " & mBSCodeDR & " ," & vbCrLf _
                        & " GROUP_BSCODECR= " & mBSCodeCR & " ," & vbCrLf _
                        & " GROUP_CATEGORY= '" & mCategory & "' ," & vbCrLf _
                        & " GROUP_GROUPLEVEL= " & mGroupLevel & " ," & vbCrLf _
                        & " GROUP_STATUS= '" & mStatus & "', STOCK_HEAD_TYPE='" & mStockHeadType & "'," & vbCrLf _
                        & " GROUP_TYPE= '" & mType & "', " & vbCrLf _
                        & " GROUP_SCHEDULENO= '" & Trim(txtScheduleNo.Text) & "', " & vbCrLf _
                        & " GROUP_SEQNO= " & Val(txtSeqNo.Text) & ", MIS_SEQNO=" & Val(txtMISSeqNo.Text) & "," & vbCrLf _
                        & " GROUP_HEAD= '" & mGroupHead & "',STOCK_GROUP='" & mGroupStock & "' " & vbCrLf _
                        & " Where GROUP_Code = " & mGroupCode & " " & vbCrLf _
                        & " AND COMPANY_CODE=" & xCompanyCode & ""
                End If
                PubDBCn.Execute(SqlStr)
                If UpdateChildGroup(mGroupCode, mBSCodeDR, mBSCodeCR, xCompanyCode) = False Then GoTo ErrPart
                RsTemp.MoveNext()
            Loop
        End If


        PubDBCn.CommitTrans()

        Update1 = True
        Exit Function
ErrPart:
        '    Resume	
        MsgBox(Err.Description)
        Update1 = False
        PubDBCn.RollbackTrans() ''	
        PubDBCn.Errors.Clear()
        RsAcmGroup.Requery()
    End Function


    Private Function UpdateChildGroup(ByRef pGroupCode As Integer, ByRef pBSCodeDr As Integer, ByRef pBSCodeCr As Integer, ByVal xCompanyCode As Long) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing ''ADODB.Recordset	

        SqlStr = " SELECT * " & vbCrLf _
            & " FROM FIN_GROUP_MST " & vbCrLf _
            & " WHERE GROUP_CODE <>" & pGroupCode & " " & vbCrLf _
            & " AND COMPANY_CODE=" & xCompanyCode & "" & vbCrLf _
            & " START WITH  GROUP_CODE=" & pGroupCode & " " & vbCrLf _
            & " AND COMPANY_CODE=" & xCompanyCode & "" & vbCrLf _
            & " CONNECT BY PRIOR GROUP_CODE= GROUP_ParentCode " & vbCrLf _
            & " AND COMPANY_CODE=" & xCompanyCode & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        If RS.EOF = False Then
            Do While Not RS.EOF
                SqlStr = "Update FIN_GROUP_MST SET GROUP_BSCodeDr=" & pBSCodeDr & ", GROUP_BSCodeCr=" & pBSCodeCr & "" & vbCrLf _
                    & " WHERE GROUP_CODE=" & RS.Fields("GROUP_CODE").Value & "" & vbCrLf _
                    & " AND COMPANY_CODE=" & xCompanyCode & ""

                PubDBCn.Execute(SqlStr)
                RS.MoveNext()
            Loop
        End If
        UpdateChildGroup = True
        RS = Nothing
        Exit Function
ErrPart:
        MsgInformation(Err.Description & vbCrLf & " Child Record not updated")
        UpdateChildGroup = False
    End Function




    Private Function CheckChildGroup(ByRef pGroupCode As Integer) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing ''ADODB.Recordset	

        SqlStr = " SELECT * " & vbCrLf _
            & " FROM FIN_GROUP_MST " & vbCrLf _
            & " WHERE GROUP_CODE <>" & pGroupCode & " " & vbCrLf _
            & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " START WITH  GROUP_CODE=" & pGroupCode & " " & vbCrLf _
            & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " CONNECT BY PRIOR GROUP_CODE= GROUP_PARENTCODE " & vbCrLf _
            & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

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
        MsgBox(Err.Description & vbCrLf & "Unable to Delete", MsgBoxStyle.Critical)
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
            TxtName_Validating(TxtName, New System.ComponentModel.CancelEventArgs(False))
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
        If MainClass.SearchGridMaster(TxtName.Text, "FIN_GROUP_MST", "GROUP_NAME", "", "", "", SqlStr) = True Then
            TxtName.Text = AcName
            TxtName_Validating(TxtName, New System.ComponentModel.CancelEventArgs(False))
        End If
    End Sub

    Private Sub CmdSearchBSCr_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearchBSCr.Click
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster(TxtBSGroupCr.Text, "FIN_BSGROUP_MST", "BSGROUP_NAME", "", "", "", SqlStr) = True Then
            TxtBSGroupCr.Text = AcName
        End If
    End Sub

    Private Sub CmdSearchBSDr_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearchBSDr.Click
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster(TxtBSGroupDr.Text, "FIN_BSGROUP_MST", "BSGROUP_NAME", "", "", "", SqlStr) = True Then
            TxtBSGroupDr.Text = AcName
            TxtBSGroupCr.Focus()
        End If
    End Sub

    Private Sub CmdSearchGroup_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearchGroup.Click
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster(TxtParentName.Text, "FIN_GROUP_MST", "GROUP_NAME", "", "", "", SqlStr) = True Then
            TxtParentName.Text = AcName
        End If
        TxtParentName.Focus()
    End Sub
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ViewGrid()
        Me.Cursor = System.Windows.Forms.Cursors.Arrow
    End Sub

    Private Sub frmAcmGroup_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        AssignGrid(False)
        SqlStr = "Select * From FIN_GROUP_MST WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAcmGroup, ADODB.LockTypeEnum.adLockReadOnly)
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

    Private Sub frmAcmGroup_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub frmAcmGroup_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub OptStatus_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptStatus.CheckedChanged, optOpeningBalHead.CheckedChanged, optClosingBalHead.CheckedChanged
        If eventSender.Checked Then
            'Dim Index As Short = OptStatus.GetIndex(eventSender)

            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub optType_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optType.CheckedChanged
        If eventSender.Checked Then
            'Dim Index As Short = optType.GetIndex(eventSender)

            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = eventArgs.row
        TxtName.Text = Trim(SprdView.Text)
        TxtName_Validating(TxtName, New System.ComponentModel.CancelEventArgs(True))
        Call CmdView_Click(CmdView, New System.EventArgs())
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Private Sub TxtBSGroupCr_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtBSGroupCr.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtBSGroupCr_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtBSGroupCr.DoubleClick
        CmdSearchBSCr_Click(CmdSearchBSCr, New System.EventArgs())
    End Sub


    Private Sub TxtBSGroupCr_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtBSGroupCr.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtBSGroupCr.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub TxtBSGroupCr_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtBSGroupCr.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            CmdSearchBSCr_Click(CmdSearchBSCr, New System.EventArgs())
        End If
    End Sub

    Private Sub TxtBSGroupDr_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtBSGroupDr.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtBSGroupDr_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtBSGroupDr.DoubleClick
        CmdSearchBSDr_Click(CmdSearchBSDr, New System.EventArgs())
    End Sub

    Private Sub TxtBSGroupDr_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtBSGroupDr.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtBSGroupDr.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub TxtBSGroupDr_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtBSGroupDr.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            CmdSearchBSDr_Click(CmdSearchBSDr, New System.EventArgs())
        End If
    End Sub

    Private Sub TxtName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub TxtName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtName.DoubleClick
        cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Sub TxtName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub TxtName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            cmdsearch_Click(cmdsearch, New System.EventArgs())
        End If
    End Sub
    Private Sub TxtName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(TxtName.Text) = "" Then GoTo EventExitSub

        If MODIFYMode = True And RsAcmGroup.EOF = False Then mGroupCode = RsAcmGroup.Fields("GROUP_Code").Value

        SqlStr = "Select * from FIN_GROUP_MST WHERE " & vbCrLf & " GROUP_NAME='" & MainClass.AllowSingleQuote(Trim(TxtName.Text)) & "'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAcmGroup, ADODB.LockTypeEnum.adLockReadOnly)
        If RsAcmGroup.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Group Does Not Exist, Click Add To Add In Master")
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * from FIN_GROUP_MST WHERE " & vbCrLf & " GROUP_Code=" & mGroupCode & "" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAcmGroup, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub TxtParentName_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtParentName.Click

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtParentName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtParentName.DoubleClick
        CmdSearchGroup_Click(CmdSearchGroup, New System.EventArgs())
    End Sub

    Private Sub TxtParentName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtParentName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtParentName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtParentName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtParentName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            CmdSearchGroup_Click(CmdSearchGroup, New System.EventArgs())
        End If
    End Sub
    Private Sub frmAcmGroup_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        Dim ii As Short
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection	
        ''PvtDBCn.Open StrConn	
        Call SetMainFormCordinate(Me)
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        fraBSGroup.Enabled = False
        CmdView.Text = ConCmdGridViewCaption
        FillComboBox()
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
        TxtName.MaxLength = RsAcmGroup.Fields("GROUP_NAME").DefinedSize ''	
        TxtParentName.MaxLength = RsAcmGroup.Fields("GROUP_NAME").DefinedSize ''	
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Private Sub frmAcmGroup_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        '    If PvtDBCn.State = adStateOpen Then	
        '        PvtDBCn.Close	
        '        Set PvtDBCn = Nothing	
        '    End If	
        RsAcmGroup.Close()
        Me.Hide()
        Me.Close()
        ''Me = Nothing
        RsAcmGroup = Nothing
    End Sub
    Private Sub ShowReport(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim mTitle As String = ""
        mTitle = ""
        Report1.Reset()
        mTitle = "List Of Account Group"
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\AcmGroup.rpt"
        SetCrpt(Report1, Mode, 1, mTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = " SELECT LPAD(' ',4*(LEVEL-1)) || GROUP_NAME as GROUP_NAME, GROUP_SEQNO," & vbCrLf _
            & " CASE WHEN GROUP_HEAD='O1' THEN '-'" & vbCrLf _
            & " WHEN GROUP_HEAD='O2' THEN 'CURRENT LIABILITIES' " & vbCrLf _
            & " WHEN GROUP_HEAD='O3' THEN 'RESERVES & SURPLUS' " & vbCrLf _
            & " WHEN GROUP_HEAD='O4' THEN 'SECURED LOANS' " & vbCrLf _
            & " WHEN GROUP_HEAD='O5' THEN 'UNSECURED LOANS' " & vbCrLf _
            & " WHEN GROUP_HEAD='O6' THEN 'CURRENT ASSETS' " & vbCrLf _
            & " WHEN GROUP_HEAD='O7' THEN 'SHARE CAPITAL' " & vbCrLf _
            & " WHEN GROUP_HEAD='O8' THEN 'FIXED ASSETS' " & vbCrLf _
            & " WHEN GROUP_HEAD='O9' THEN 'DUTY & TAXES' "

        SqlStr = SqlStr & vbCrLf _
            & " WHEN GROUP_HEAD='I1' THEN 'INCOME SALE'" & vbCrLf _
            & " WHEN GROUP_HEAD='I2' THEN 'INCOME OTHER SALE' " & vbCrLf _
            & " WHEN GROUP_HEAD='I3' THEN 'INCOME SALE SCRAP' " & vbCrLf _
            & " WHEN GROUP_HEAD='I4' THEN 'INCOME SALE JOBWORK' " & vbCrLf _
            & " WHEN GROUP_HEAD='I5' THEN 'INCOME SALE INTER-UNIT' " & vbCrLf _
            & " WHEN GROUP_HEAD='I6' THEN 'INCOME OTHER INCOME' " & vbCrLf _
            & " WHEN GROUP_HEAD='I7' THEN 'SALE RETURN' " & vbCrLf _
            & " WHEN GROUP_HEAD='I8' THEN 'SALE SUPPLIMENTRY' "


        SqlStr = SqlStr & vbCrLf _
            & " WHEN GROUP_HEAD='E1' THEN 'MATERIAL COST' " & vbCrLf _
            & " WHEN GROUP_HEAD='E2' THEN 'POWER & FUEL' " & vbCrLf _
            & " WHEN GROUP_HEAD='E3' THEN 'FREIGHT OUTWARD' " & vbCrLf _
            & " WHEN GROUP_HEAD='E4' THEN 'FINANCE COST' " & vbCrLf _
            & " WHEN GROUP_HEAD='E5' THEN 'OTHER EXPENSES' " & vbCrLf _
            & " WHEN GROUP_HEAD='E6' THEN 'MANPOWER - PRODUCTION STAFF' " & vbCrLf _
            & " WHEN GROUP_HEAD='E7' THEN 'MANPOWER - GENERAL STAFF' " & vbCrLf _
            & " WHEN GROUP_HEAD='E8' THEN 'MANUFACTURING EXPENSES' " & vbCrLf _
            & " WHEN GROUP_HEAD='E9' THEN 'ADMINISTRATIVE EXPENSES' " & vbCrLf _
            & " WHEN GROUP_HEAD='E10' THEN 'DEPRECIATION' " & vbCrLf _
            & " WHEN GROUP_HEAD='E11' THEN 'PACKING EXPENSES' " & vbCrLf _
            & " WHEN GROUP_HEAD='E12' THEN 'JOBWORK SUB CONTACTING EXPENSES' " & vbCrLf _
            & " WHEN GROUP_HEAD='E13' THEN 'CORPORATE EXPENSES' " & vbCrLf _
            & " WHEN GROUP_HEAD='E14' THEN 'FREIGHT INWARD' " & vbCrLf _
            & " WHEN GROUP_HEAD='E15' THEN 'DUTY DRAW BACK' " & vbCrLf _
            & " WHEN GROUP_HEAD='E16' THEN 'FOREIGN EXCHANGE RATE FLUCTION' "

        SqlStr = SqlStr & vbCrLf _
            & " WHEN GROUP_HEAD='E17' THEN 'INCREASE/(DECREASE) IN WIP & FG STOCKS' " & vbCrLf _
            & " WHEN GROUP_HEAD='E18' THEN 'BOP CONSUMPTION' " & vbCrLf _
            & " WHEN GROUP_HEAD='E19' THEN 'RAW MATERIAL WIP' " & vbCrLf _
            & " WHEN GROUP_HEAD='E20' THEN 'CONSUMPTION OF STORES & SPARES' " & vbCrLf _
            & " WHEN GROUP_HEAD='E21' THEN 'CUTTING TOOLS' " & vbCrLf _
            & " WHEN GROUP_HEAD='E22' THEN 'TOOLS & DIES' " & vbCrLf _
            & " WHEN GROUP_HEAD='E23' THEN 'GAS (LPG) & FURNACE OIL' " & vbCrLf _
            & " WHEN GROUP_HEAD='E24' THEN 'DIESEL' " & vbCrLf _
            & " WHEN GROUP_HEAD='E25' THEN 'PERSONNEL EXPENSES' " & vbCrLf _
            & " WHEN GROUP_HEAD='E26' THEN 'WORKMEN & STAFF WELFARE EXPENSES' " & vbCrLf _
            & " WHEN GROUP_HEAD='E27' THEN 'CONTRIBUTION TO PROVIDENT FUND & OTHER' " & vbCrLf _
            & " WHEN GROUP_HEAD='E28' THEN 'INTEREST' " & vbCrLf _
            & " WHEN GROUP_HEAD='E29' THEN 'INTEREST ON TERM LOAN' " & vbCrLf _
            & " WHEN GROUP_HEAD='E30' THEN 'INTEREST ON WORKING CAPITAL' " & vbCrLf _
            & " WHEN GROUP_HEAD='E31' THEN 'INTEREST ON UNSECURED LOANS' " & vbCrLf _
            & " WHEN GROUP_HEAD='E32' THEN 'BANK CHARGES' " & vbCrLf _
            & " WHEN GROUP_HEAD='E33' THEN 'SELLING EXPENSES' " & vbCrLf _
            & " WHEN GROUP_HEAD='E34' THEN 'BUSINESS PROMOTION' "

        SqlStr = SqlStr & vbCrLf _
            & " ELSE '' END AS GROUP_HEAD, MIS_SEQNO," & vbCrLf _
            & " GROUP_TYPE "

        SqlStr = SqlStr & vbCrLf _
            & " FROM FIN_GROUP_MST " & vbCrLf _
            & " WHERE GROUP_CATEGORY='G'" & vbCrLf _
            & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " START WITH  GROUP_PARENTCODE= -1 " & vbCrLf _
            & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " CONNECT BY PRIOR GROUP_CODE || COMPANY_CODE =GROUP_PARENTCODE || COMPANY_CODE" & vbCrLf _
            & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        '	
        '    SqlStr = " SELECT GROUP_NAME " & vbCrLf _	
        ''            & " FROM FIN_GROUP_MST " & vbCrLf _	
        ''            & " WHERE GROUP_CATEGORY='G'" & vbCrLf _	
        ''            & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""	
        '	
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
            .set_ColWidth(2, 20)
            .set_ColWidth(3, 8)
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
    Private Sub TxtParentName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtParentName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim mParenctCode As Integer
        If Trim(TxtParentName.Text) = "" Then
            fraBSGroup.Enabled = True
            GoTo EventExitSub
        End If
        If MainClass.ValidateWithMasterTable(Trim(TxtParentName.Text), "GROUP_Name", "GROUP_Code", "FIN_GROUP_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            fraBSGroup.Enabled = False
            mParenctCode = MasterNo
            Call FillBSHead(mParenctCode)
            If MainClass.ValidateWithMasterTable(Trim(TxtParentName.Text), "GROUP_Name", "GROUP_SCHEDULENO", "FIN_GROUP_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtScheduleNo.Text = MasterNo
            End If
        Else
            ErrorMsg("Invalid Parent Name", "", vbInformation)
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub FillBSHead(ByRef pParentCode As Integer)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing


        Sqlstr = "Select BSDR.BSGROUP_NAME,BSCR.BSGROUP_NAME " & vbCrLf & " FROM FIN_GROUP_MST, FIN_BSGROUP_MST BSDR ,FIN_BSGROUP_MST BSCR " & vbCrLf & " WHERE FIN_GROUP_MST.COMPANY_CODE=BSDR.COMPANY_CODE" & vbCrLf & " AND FIN_GROUP_MST.COMPANY_CODE=BSCR.COMPANY_CODE " & vbCrLf & " AND FIN_GROUP_MST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FIN_GROUP_MST.GROUP_Code=" & pParentCode & "" & vbCrLf & " AND FIN_GROUP_MST.GROUP_BSCODEDR=BSDR.BSGROUP_CODE " & vbCrLf & " AND FIN_GROUP_MST.GROUP_BSCODECR=BSCR.BSGROUP_CODE "
        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            TxtBSGroupDr.Text = IIf(IsDbNull(RS.Fields(0).Value), "", RS.Fields(0).Value)
            TxtBSGroupCr.Text = IIf(IsDbNull(RS.Fields(1).Value), "", RS.Fields(1).Value)
        End If
        RS.Close()
        Exit Sub
ERR1:
        Call ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        RS.Close()
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

    Private Sub txtSeqNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSeqNo.TextChanged, txtMISSeqNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSeqNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSeqNo.KeyPress, txtMISSeqNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub chkStockGroup_CheckStateChanged(sender As Object, e As EventArgs) Handles chkStockGroup.CheckStateChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
End Class
