Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmCategoryMst
   Inherits System.Windows.Forms.Form
   Dim RsGeneral As ADODB.Recordset
   Dim ADDMode As Boolean
   Dim MODIFYMode As Boolean
   Dim XRIGHT As String
   ''Private PvtDBCn As ADODB.Connection

   Dim xCode As String
   Dim FormActive As Boolean
   Dim Shw As Boolean
   Dim MasterNo As Object
    Dim SqlStr As String = ""
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
        MainClass.ButtonStatus(Me, XRIGHT, RsGeneral, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ErrorPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtItemPrefix_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItemPrefix.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtItemPrefix.Text)
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub

    Private Sub txtItemPrefix_TextChanged(sender As Object, e As System.EventArgs) Handles txtItemPrefix.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub Clear1()

        txtCode.Text = ""
        txtDesc.Text = ""
        txtItemPrefix.Text = ""
        txtItemPrefix.Enabled = True
        txtStockType.Text = ""
        '    txtHSNCode.Text = ""	
        '    lblHSNDesc.text = ""	
        txtAcctConsumption.Text = ""

        txtSales.Text = ""
        txtPurchase.Text = ""


        lblAddUser.Text = ""
        lblAddDate.Text = ""
        lblModUser.Text = ""
        lblModDate.Text = ""

        chkIndentItem.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkAutoIssueSubStore.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkBOMItem.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkCostingRequired.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkTCRequired.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkQuotation.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkTPReport.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkQFRRequired.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkMaxLevel.CheckState = System.Windows.Forms.CheckState.Unchecked

        chkER1.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkAutoMovement.CheckState = System.Windows.Forms.CheckState.Unchecked
        CboType.SelectedIndex = 0
        optClassification(0).Checked = True
        txtCode.Enabled = True

        Call AutoCompleteSearch("INV_GENERAL_MST", "GEN_CODE", "GEN_TYPE='" & lblCategory.Text & "'", txtCode)
        Call AutoCompleteSearch("INV_GENERAL_MST", "GEN_DESC", "GEN_TYPE='" & lblCategory.Text & "'", txtDesc)

        Call AutoCompleteSearch("INV_TYPE_MST", "STOCK_TYPE_CODE", "", txtStockType)
        Call AutoCompleteSearch("FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "", txtAcctConsumption)
        Call AutoCompleteSearch("FIN_INVTYPE_MST", "NAME", "CATEGORY='S'", txtSales)
        Call AutoCompleteSearch("FIN_INVTYPE_MST", "NAME", "CATEGORY='P'", txtPurchase)



        MainClass.ButtonStatus(Me, XRIGHT, RsGeneral, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr
        Sqlstr = ""

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        If InsertIntoDelAudit(PubDBCn, "INV_GENERAL_MST", (txtCode.Text), RsGeneral, "GEN_DESC") = False Then GoTo DeleteErr
        If InsertIntoDeleteTrn(PubDBCn, "INV_GENERAL_MST", "GEN_CODE || ':' || GEN_TYPE", txtCode.Text & ":" & lblCategory.Text) = False Then GoTo DeleteErr

        Sqlstr = " DELETE FROM INV_GENERAL_MST " & vbCrLf _
                  & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                  & " AND GEN_CODE='" & MainClass.AllowSingleQuote(UCase((txtCode.Text))) & "'" & vbCrLf _
                  & " AND GEN_TYPE='" & lblCategory.Text & "'"

        PubDBCn.Execute(Sqlstr)
        PubDBCn.CommitTrans()
        RsGeneral.Requery() ''.Refresh	
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans() ''	
        RsGeneral.Requery() ''.Refresh	
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete, Transactions Exists Against This Expense Head.", MsgBoxStyle.Information)
            Exit Function
        End If
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim mPrdType As String
        Dim mHSNCode As Integer
        Dim mAccountCode As String
        Dim mCatClass As Integer

        If Not RsGeneral.EOF Then

            txtCode.Text = IIf(IsDbNull(RsGeneral.Fields("GEN_CODE").Value), "", RsGeneral.Fields("GEN_CODE").Value)
            txtDesc.Text = IIf(IsDBNull(RsGeneral.Fields("GEN_DESC").Value), "", RsGeneral.Fields("GEN_DESC").Value)
            txtItemPrefix.Text = IIf(IsDBNull(RsGeneral.Fields("CODE_PREFIX").Value), "", RsGeneral.Fields("CODE_PREFIX").Value)
            txtItemPrefix.Enabled = False
            txtStockType.Text = IIf(IsDbNull(RsGeneral.Fields("STOCKTYPE").Value), "", RsGeneral.Fields("STOCKTYPE").Value)
            chkER1.CheckState = IIf(RsGeneral.Fields("ER_ITEM").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            chkIndentItem.CheckState = IIf(RsGeneral.Fields("IS_INDENT_ITEM").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkBOMItem.CheckState = IIf(RsGeneral.Fields("IS_BOM_ITEM").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkCostingRequired.CheckState = IIf(RsGeneral.Fields("IS_COSTING_REQ").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkTCRequired.CheckState = IIf(RsGeneral.Fields("IS_TC_REQ").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkTPReport.CheckState = IIf(RsGeneral.Fields("IS_TPI_REQ").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkQFRRequired.CheckState = IIf(RsGeneral.Fields("IS_QFR_REQ").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkMaxLevel.CheckState = IIf(RsGeneral.Fields("MAX_LEVEL_CHECK").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            chkAutoIssueSubStore.CheckState = IIf(RsGeneral.Fields("IS_AUTO_ISSUE_SUBSTR").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkQuotation.CheckState = IIf(RsGeneral.Fields("IS_QUOTATION_REQ").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)


            'chkIndentItem.Enabled = IIf(chkIndentItem.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
            'chkBOMItem.Enabled = IIf(chkBOMItem.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
            'chkCostingRequired.Enabled = IIf(chkCostingRequired.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
            'chkTCRequired.Enabled = IIf(chkTCRequired.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
            'chkTPReport.Enabled = IIf(chkTPReport.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
            'chkQFRRequired.Enabled = IIf(chkQFRRequired.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
            'chkMaxLevel.Enabled = IIf(chkMaxLevel.CheckState = System.Windows.Forms.CheckState.Checked, False, True)

            mCatClass = IIf(IsDbNull(RsGeneral.Fields("CAT_CLASS").Value), 0, RsGeneral.Fields("CAT_CLASS").Value)

            optClassification(mCatClass).Checked = True

            chkAutoMovement.CheckState = IIf(RsGeneral.Fields("AUTO_MOVEMENT").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            mPrdType = IIf(IsDbNull(RsGeneral.Fields("PRD_TYPE").Value), "", RsGeneral.Fields("PRD_TYPE").Value)

            '        mHSNCode = IIf(IsNull(RsGeneral.Fields("HSNCODE").Value), 0, RsGeneral.Fields("HSNCODE").Value)	

            '        txtHSNCode.Text = ""	
            '        lblHSNDesc.text = ""	
            '	
            '        If MainClass.ValidateWithMasterTable(mHSNCode, "HSN_CODE", "HSN_DESC", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then	
            '            txtHSNCode.Text = mHSNCode	
            '            lblHSNDesc.text = Trim(MasterNo)	
            '        End If	

            mAccountCode = IIf(IsDbNull(RsGeneral.Fields("ACCT_CONSUM_CODE").Value), 0, RsGeneral.Fields("ACCT_CONSUM_CODE").Value)
            If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtAcctConsumption.Text = Trim(MasterNo)
            Else
                txtAcctConsumption.Text = ""
            End If

            If mPrdType = "G" Then
                CboType.SelectedIndex = 0
            ElseIf mPrdType = "P" Then
                CboType.SelectedIndex = 1
            ElseIf mPrdType = "J" Then
                CboType.SelectedIndex = 2
            ElseIf mPrdType = "C" Then
                CboType.SelectedIndex = 3
            ElseIf mPrdType = "T" Then
                CboType.SelectedIndex = 4
            ElseIf mPrdType = "A" Then
                CboType.SelectedIndex = 5
            ElseIf mPrdType = "R" Then
                CboType.SelectedIndex = 6
            ElseIf mPrdType = "B" Then
                CboType.SelectedIndex = 7
            ElseIf mPrdType = "I" Then
                CboType.SelectedIndex = 8
            ElseIf mPrdType = "D" Then
                CboType.SelectedIndex = 9
            ElseIf mPrdType = "M" Then
                CboType.SelectedIndex = 10
            ElseIf mPrdType = "E" Then
                CboType.SelectedIndex = 11
            ElseIf mPrdType = "S" Then
                CboType.SelectedIndex = 12
            ElseIf mPrdType = "1" Then
                CboType.SelectedIndex = 13
            ElseIf mPrdType = "2" Then
                CboType.SelectedIndex = 14
            ElseIf mPrdType = "3" Then
                CboType.SelectedIndex = 15
            Else
                CboType.SelectedIndex = 16
            End If

            If IsDbNull(RsGeneral.Fields("SALEINVTYPECODE").Value) Then
                txtSales.Text = ""
            Else
                If MainClass.ValidateWithMasterTable(RsGeneral.Fields("SALEINVTYPECODE").Value, "CODE", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND CATEGORY='S'") = True Then
                    txtSales.Text = MasterNo
                Else
                    txtSales.Text = ""
                End If
            End If

            If IsDbNull(RsGeneral.Fields("PURCHASEINVTYPECODE").Value) Then
                txtPurchase.Text = ""
            Else
                If MainClass.ValidateWithMasterTable(RsGeneral.Fields("PURCHASEINVTYPECODE").Value, "CODE", "NAME", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND CATEGORY='P'") = True Then
                    txtPurchase.Text = MasterNo
                Else
                    txtPurchase.Text = ""
                End If
            End If

            lblAddUser.Text = IIf(IsDbNull(RsGeneral.Fields("ADDUSER").Value), "", RsGeneral.Fields("ADDUSER").Value)
            lblAddDate.Text = VB6.Format(IIf(IsDBNull(RsGeneral.Fields("ADDDATE").Value), "", RsGeneral.Fields("ADDDATE").Value), "dd/MM/yyyy")
            lblModUser.Text = IIf(IsDBNull(RsGeneral.Fields("MODUSER").Value), "", RsGeneral.Fields("MODUSER").Value)
            lblModDate.Text = VB6.Format(IIf(IsDBNull(RsGeneral.Fields("MODDATE").Value), "", RsGeneral.Fields("MODDATE").Value), "dd/MM/yyyy")

            xCode = RsGeneral.Fields("GEN_CODE").Value
        End If
        txtCode.Enabled = True
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsGeneral, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
        '    Resume	
    End Sub
    Private Function Update1() As Boolean

        On Error GoTo UpdateError
        Dim mERITEM As String
        Dim mPrdType As String
        Dim mDivCode As String
        Dim mAcctConsumptionCode As String
        Dim mAutoMovement As String
        Dim mSalesPostCode As String
        Dim mPurchasePostCode As String

        Dim mIndentItem As String
        Dim mBOMItem As String
        Dim mCostingRequired As String
        Dim mTCRequired As String
        Dim mTPReport As String
        Dim mQFRRequired As String
        Dim mMaxLevelCheck As String
        Dim mCatClass As Integer
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xCompanyCode As Long
        Dim mAutoIssueSubStr As String
        Dim mQuotation As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mERITEM = IIf(chkER1.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mAutoMovement = IIf(chkAutoMovement.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mPrdType = VB.Left(Trim(CboType.Text), 1)

        mIndentItem = IIf(chkIndentItem.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mBOMItem = IIf(chkBOMItem.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mCostingRequired = IIf(chkCostingRequired.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mTCRequired = IIf(chkTCRequired.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mTPReport = IIf(chkTPReport.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mQFRRequired = IIf(chkQFRRequired.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mMaxLevelCheck = IIf(chkMaxLevel.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mAutoIssueSubStr = IIf(chkAutoIssueSubStore.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mQuotation = IIf(chkQuotation.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        If optClassification(0).Checked = True Then
            mCatClass = 0
        ElseIf optClassification(1).Checked = True Then
            mCatClass = 1
        ElseIf optClassification(2).Checked = True Then
            mCatClass = 2
        ElseIf optClassification(3).Checked = True Then
            mCatClass = 3
        End If

        If CheckConsolidatedMaster("FIN_SUPP_CUST_MST") = True Then
            SqlStr = "SELECT COMPANY_CODE FROM GEN_COMPANY_MST"
        Else
            SqlStr = "SELECT COMPANY_CODE FROM GEN_COMPANY_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                xCompanyCode = RsTemp.Fields("COMPANY_CODE").Value
                If MainClass.ValidateWithMasterTable(Trim(txtSales.Text), "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCompanyCode & " AND CATEGORY='S'") = False Then
                    mSalesPostCode = ""
                Else
                    mSalesPostCode = MasterNo
                End If

                If MainClass.ValidateWithMasterTable(Trim(txtPurchase.Text), "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCompanyCode & " AND CATEGORY='P'") = False Then
                    mPurchasePostCode = ""
                Else
                    mPurchasePostCode = MasterNo
                End If

                mDivCode = ""

                If MainClass.ValidateWithMasterTable(txtAcctConsumption.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCompanyCode & "") = True Then
                    mAcctConsumptionCode = Val(MasterNo)
                Else
                    mAcctConsumptionCode = ""
                End If

                SqlStr = ""
                If ADDMode = True Then
                    '        mCode = MainClass.AutoGenRowNo("FIN_TARRIF_MST", "Code", PubDBCn)	
                    SqlStr = "INSERT INTO INV_GENERAL_MST (" & vbCrLf _
                                 & " COMPANY_CODE, GEN_CODE, GEN_TYPE, GEN_DESC,STOCKTYPE, " & vbCrLf _
                                 & " ER_ITEM, PRD_TYPE, " & vbCrLf _
                                 & " ADDUSER, ADDDATE, MODUSER, MODDATE,ACCT_CONSUM_CODE,AUTO_MOVEMENT," & vbCrLf _
                                 & " SALEINVTYPECODE , PURCHASEINVTYPECODE, " & vbCrLf _
                                 & " IS_INDENT_ITEM, IS_BOM_ITEM, IS_COSTING_REQ," & vbCrLf _
                                 & " IS_TC_REQ, IS_TPI_REQ, IS_QFR_REQ, MAX_LEVEL_CHECK, CAT_CLASS,IS_AUTO_ISSUE_SUBSTR,CODE_PREFIX,IS_QUOTATION_REQ" & vbCrLf _
                                 & " ) VALUES ( " & vbCrLf _
                                 & " " & xCompanyCode & ", " & vbCrLf _
                                 & " '" & MainClass.AllowSingleQuote(txtCode.Text) & "', " & vbCrLf _
                                 & " '" & lblCategory.Text & "','" & MainClass.AllowSingleQuote(txtDesc.Text) & "'," & vbCrLf _
                                 & " '" & MainClass.AllowSingleQuote(txtStockType.Text) & "','" & mERITEM & "','" & mPrdType & "', " & vbCrLf _
                                 & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','','" & MainClass.AllowSingleQuote(mAcctConsumptionCode) & "', " & vbCrLf _
                                 & " '" & mAutoMovement & "','" & mSalesPostCode & "', '" & mPurchasePostCode & "', " & vbCrLf _
                                 & " '" & mIndentItem & "','" & mBOMItem & "', '" & mCostingRequired & "', " & vbCrLf _
                                 & " '" & mTCRequired & "','" & mTPReport & "', '" & mQFRRequired & "','" & mMaxLevelCheck & "'," & mCatClass & ",'" & mAutoIssueSubStr & "','" & MainClass.AllowSingleQuote(txtItemPrefix.Text) & "','" & mQuotation & "')"
                Else
                    SqlStr = " UPDATE INV_GENERAL_MST  SET " & vbCrLf _
                                & " GEN_DESC='" & MainClass.AllowSingleQuote(txtDesc.Text) & "'," & vbCrLf _
                                & " STOCKTYPE='" & MainClass.AllowSingleQuote(txtStockType.Text) & "'," & vbCrLf _
                                & " ER_ITEM='" & mERITEM & "', CAT_CLASS=" & mCatClass & "," & vbCrLf _
                                & " PRD_TYPE='" & mPrdType & "', AUTO_MOVEMENT='" & mAutoMovement & "'," & vbCrLf _
                                & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                                & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'), " & vbCrLf _
                                & " ACCT_CONSUM_CODE='" & MainClass.AllowSingleQuote(mAcctConsumptionCode) & "', " & vbCrLf _
                                & " SALEINVTYPECODE='" & mSalesPostCode & "' , " & vbCrLf & " PURCHASEINVTYPECODE='" & mPurchasePostCode & "', " & vbCrLf _
                                & " IS_INDENT_ITEM='" & mIndentItem & "', " & vbCrLf _
                                & " IS_BOM_ITEM='" & mBOMItem & "', " & vbCrLf _
                                & " IS_COSTING_REQ='" & mCostingRequired & "', " & vbCrLf & " IS_TC_REQ='" & mTCRequired & "', " & vbCrLf _
                                & " IS_TPI_REQ='" & mTPReport & "', IS_QUOTATION_REQ='" & mQuotation & "'," & vbCrLf _
                                & " IS_QFR_REQ='" & mQFRRequired & "', IS_AUTO_ISSUE_SUBSTR='" & mAutoIssueSubStr & "',CODE_PREFIX='" & MainClass.AllowSingleQuote(txtItemPrefix.Text) & "'" & vbCrLf _
                                & " WHERE COMPANY_CODE=" & xCompanyCode & "" & vbCrLf _
                                & " AND GEN_CODE= '" & xCode & "'" & vbCrLf _
                                & " AND GEN_TYPE='" & lblCategory.Text & "'"
                End If

UpdatePart:
                PubDBCn.Execute(SqlStr)

                If xCompanyCode = RsCompany.Fields("COMPANY_CODE").Value Then
                    SqlStr = " UPDATE INV_GENERAL_MST  SET " & vbCrLf _
                                & " MAX_LEVEL_CHECK='" & mMaxLevelCheck & "' " & vbCrLf _
                                & " WHERE COMPANY_CODE=" & xCompanyCode & "" & vbCrLf _
                                & " AND GEN_CODE= '" & xCode & "'" & vbCrLf _
                                & " AND GEN_TYPE='" & lblCategory.Text & "'"

                    PubDBCn.Execute(SqlStr)

                End If
                RsTemp.MoveNext()
            Loop
        End If

        PubDBCn.CommitTrans()
        Update1 = True
        Exit Function
UpdateError:
        '    Resume	
        Update1 = False
        PubDBCn.RollbackTrans() ''	
        RsGeneral.Requery() ''.Refresh	
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Sub SetTextLengths()

        On Error GoTo ERR1
        txtCode.Maxlength = RsGeneral.Fields("GEN_CODE").DefinedSize
        txtDesc.MaxLength = RsGeneral.Fields("GEN_DESC").DefinedSize
        txtItemPrefix.MaxLength = RsGeneral.Fields("CODE_PREFIX").DefinedSize
        txtStockType.Maxlength = RsGeneral.Fields("STOCKTYPE").DefinedSize
        '    txtHSNCode.MaxLength = MainClass.SetMaxLength("HSN_CODE", "GEN_HSN_MST", PubDBCn)	
        txtAcctConsumption.Maxlength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)

        txtPurchase.Maxlength = MainClass.SetMaxLength("NAME", "FIN_INVTYPE_MST", PubDBCn)
        txtSales.Maxlength = MainClass.SetMaxLength("NAME", "FIN_INVTYPE_MST", PubDBCn)

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        FieldsVarification = True
        If Trim(txtCode.Text) = "" Then
            MsgInformation(" Category code is empty. Cannot Save")
            txtCode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtDesc.Text) = "" Then
            MsgInformation(" Category Description is empty. Cannot Save")
            txtDesc.Focus()
            FieldsVarification = False
            Exit Function
        End If



        If lblCategory.Text = "C" Then
            If RsCompany.Fields("AUTO_GEN_CODE").Value = "Y" And Trim(txtItemPrefix.Text) = "" Then
                MsgInformation("Item Prefix is empty. Cannot Save")
                txtItemPrefix.Focus()
                FieldsVarification = False
                Exit Function
            End If

            If Trim(txtStockType.Text) = "" Then
                MsgInformation("Stock Type is empty. Cannot Save")
                txtStockType.Focus()
                FieldsVarification = False
                Exit Function
            End If

            'If chkIndentItem.CheckState = System.Windows.Forms.CheckState.Checked And chkBOMItem.CheckState = System.Windows.Forms.CheckState.Checked Then
            '    MsgInformation("Please select either Indent Item or BOM Item not both. Cannot Save")
            '    FieldsVarification = False
            '    Exit Function
            'End If

            'If chkIndentItem.CheckState = System.Windows.Forms.CheckState.Unchecked And chkBOMItem.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            '    MsgInformation("Please select either Indent Item or BOM Item. Cannot Save")
            '    FieldsVarification = False
            '    Exit Function
            'End If

            '        If Trim(txtHSNCode.Text) = "" Then	
            '            MsgInformation "Division is empty. Cannot Save"	
            '            txtHSNCode.SetFocus	
            '            FieldsVarification = False	
            '            Exit Function	
            '        End If	

            '        If Trim(txtAcctConsumption.Text) = "" Then	
            '            MsgInformation "Account Consumption Name is empty. Cannot Save"	
            '            txtAcctConsumption.SetFocus	
            '            FieldsVarification = False	
            '            Exit Function	
            '        End If	

        End If

        If Trim(CboType.Text) = "" Then
            MsgInformation("Type is empty. Cannot Save")
            CboType.Focus()
            FieldsVarification = False
            Exit Function
        End If

        '    If Trim(txtHSNCode.Text) <> "" Then	
        ''        MsgInformation "HSN Code Cann't be Blank. Cannot Save"	
        ''        txtSales.SetFocus	
        ''        FieldsVarification = False	
        ''        Exit Function	
        ''    Else	
        '        If MainClass.ValidateWithMasterTable(Trim(txtHSNCode.Text), "HSN_CODE", "HSN_DESC", "GEN_HSN_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then	
        '            MsgInformation "HSN Code Does Not Exist In Master. Cannot Save"	
        '            txtHSNCode.SetFocus	
        '            FieldsVarification = False	
        '            Exit Function	
        '        End If	
        '    End If	


        If Trim(txtSales.Text) <> "" Then
            '        MsgInformation "Sale Posting Account Cann't be Blank. Cannot Save"	
            '        txtSales.SetFocus	
            '        FieldsVarification = False	
            '        Exit Function	
            '    Else	
            If MainClass.ValidateWithMasterTable(Trim(txtSales.Text), "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'") = False Then
                MsgInformation("Sale Posting Account Does Not Exist In Invoice Type. Cannot Save")
                txtSales.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If Trim(txtPurchase.Text) <> "" Then
            '        MsgInformation "Purchase Posting Account Cann't be Blank. Cannot Save"	
            '        txtPurchase.SetFocus	
            '        FieldsVarification = False	
            '        Exit Function	
            '    Else	
            If MainClass.ValidateWithMasterTable(Trim(txtPurchase.Text), "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='P'") = False Then
                MsgInformation("Purchase Posting Account Does Not Exist In Invoice Type. Cannot Save")
                txtPurchase.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If


        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
        End If
        If MODIFYMode = True And RsGeneral.EOF = True Then Exit Function
        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        Dim SqlStr As String = ""

        Sqlstr = ""

        If lblCategory.Text = "C" Then
            SqlStr = " SELECT A.GEN_CODE, A.GEN_DESC, " & vbCrLf _
                & " A.STOCKTYPE, CODE_PREFIX, B.DIV_DESC, " & vbCrLf _
                & " CASE WHEN PRD_TYPE='G' THEN 'GENERAL' " & vbCrLf _
                & " WHEN PRD_TYPE='P' THEN 'PRODUCTION' " & vbCrLf _
                & " WHEN PRD_TYPE='C' THEN 'CONS (PROD)' " & vbCrLf _
                & " WHEN PRD_TYPE='A' THEN 'ASSETS' " & vbCrLf _
                & " WHEN PRD_TYPE='J' THEN 'JOBWORK THIRD PARTY' " & vbCrLf _
                & " WHEN PRD_TYPE='R' THEN 'RAW MATERIAL' " & vbCrLf _
                & " WHEN PRD_TYPE='T' THEN 'TOOL' " & vbCrLf _
                & " WHEN PRD_TYPE='B' THEN 'BOP' " & vbCrLf _
                & " WHEN PRD_TYPE='I' THEN 'INHOUSE' " & vbCrLf _
                & " WHEN PRD_TYPE='D' THEN 'DEVELOPMENT' " & vbCrLf _
                & " WHEN PRD_TYPE='M' THEN 'MAINTANCE' " & vbCrLf _
                & " WHEN PRD_TYPE='E' THEN 'GENETAL' " & vbCrLf _
                & " WHEN PRD_TYPE='1' THEN 'TROLLY & BINS' " & vbCrLf _
                & " WHEN PRD_TYPE='2' THEN 'THIRD PARTY MATERIAL' " & vbCrLf _
                & " WHEN PRD_TYPE='3' THEN 'RAW MATERIAL (TUBES)' " & vbCrLf & " ELSE '' END AS PRD_TYPE, " & vbCrLf _
                & " DECODE(AUTO_MOVEMENT,'Y','YES','NO') AUTO_MOVEMENT," & vbCrLf _
                & " DECODE(IS_INDENT_ITEM,'Y','YES','NO') IS_INDENT_ITEM," & vbCrLf _
                & " DECODE(IS_BOM_ITEM,'Y','YES','NO') IS_BOM_ITEM," & vbCrLf _
                & " DECODE(IS_COSTING_REQ,'Y','YES','NO') IS_COSTING_REQ," & vbCrLf _
                & " DECODE(MAX_LEVEL_CHECK,'Y','YES','NO') MAX_LEVEL_CHECK," & vbCrLf _
                & " DECODE(IS_QUOTATION_REQ,'Y','YES','NO') QUOTATION_REQUIRED"


            SqlStr = SqlStr & vbCrLf _
                & " FROM INV_GENERAL_MST A, INV_DIVISION_MST B, FIN_SUPP_CUST_MST C" & vbCrLf _
                & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND A.COMPANY_CODE=B.COMPANY_CODE" & vbCrLf _
                & " AND A.GEN_DIV_CODE=B.DIV_CODE AND A.COMPANY_CODE=C.COMPANY_CODE(+) AND A.ACCT_CONSUM_CODE=C.SUPP_CUST_CODE(+)" & vbCrLf _
                & " AND A.GEN_TYPE='" & lblCategory.Text & "'" & vbCrLf _
                & " ORDER BY GEN_DESC"
        Else
            SqlStr = " SELECT GEN_CODE,GEN_DESC " & vbCrLf _
                & " FROM INV_GENERAL_MST" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND GEN_TYPE='" & lblCategory.Text & "'" & vbCrLf _
                & " ORDER BY GEN_DESC"
        End If


        MainClass.AssignDataInSprd8(Sqlstr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 12)
            .set_ColWidth(0, 5)
            .set_ColWidth(1, 8)
            .set_ColWidth(2, 25)
            .set_ColWidth(3, 10)
            .set_ColWidth(4, 10)
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
        mTitle = "Category Master"
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\Tariff.rpt"
        SetCrpt(Report1, Mode, 1, mTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub CboType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles CboType.SelectedIndexChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub CboType_TextChanged(sender As Object, e As System.EventArgs) Handles CboType.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkAutoMovement_CheckStateChanged(sender As Object, e As System.EventArgs) Handles chkAutoMovement.CheckStateChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkBOMItem_CheckStateChanged(sender As Object, e As System.EventArgs) Handles chkBOMItem.CheckStateChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkCostingRequired_CheckStateChanged(sender As Object, e As System.EventArgs) Handles chkCostingRequired.CheckStateChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkER1_CheckStateChanged(sender As Object, e As System.EventArgs) Handles chkER1.CheckStateChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkIndentItem_CheckStateChanged(sender As Object, e As System.EventArgs) Handles chkIndentItem.CheckStateChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkMaxLevel_CheckStateChanged(sender As Object, e As System.EventArgs) Handles chkMaxLevel.CheckStateChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkQFRRequired_CheckStateChanged(sender As Object, e As System.EventArgs) Handles chkQFRRequired.CheckStateChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkTCRequired_CheckStateChanged(sender As Object, e As System.EventArgs) Handles chkTCRequired.CheckStateChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkQuotation_CheckStateChanged(sender As Object, e As System.EventArgs) Handles chkQuotation.CheckStateChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkTPReport_CheckStateChanged(sender As Object, e As System.EventArgs) Handles chkTPReport.CheckStateChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub CmdModify_Click(sender As Object, e As System.EventArgs) Handles CmdModify.Click
        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsGeneral, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
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

    Private Sub cmdPreview_Click(sender As Object, e As System.EventArgs) Handles cmdPreview.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ShowReport(Crystal.DestinationConstants.crptToWindow)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(sender As Object, e As System.EventArgs) Handles cmdPrint.Click
        Me.Cursor = System.Windows.Forms.Cursors.Arrow
        ShowReport(Crystal.DestinationConstants.crptToPrinter)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub CmdView_Click(sender As Object, e As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Private Sub CmdAdd_Click(sender As Object, e As System.EventArgs) Handles CmdAdd.Click
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

    Private Sub CmdClose_Click(sender As Object, e As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub CmdDelete_Click(sender As Object, e As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart

        If CheckConsolidatedMaster("FIN_SUPP_CUST_MST") = True Then
            MsgInformation("Cann't be Delete.")
            Exit Sub
        End If

        If txtCode.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub
        If Not RsGeneral.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.	
                If Delete1() = False Then GoTo DelErrPart
                If RsGeneral.EOF = True Then
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

    Private Sub frmCategoryMst_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        MainClass.UOpenRecordSet("Select * From INV_GENERAL_MST Where 1<>1 ", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGeneral, ADODB.LockTypeEnum.adLockReadOnly)


        lblStockType.Visible = IIf(lblCategory.Text = "C", True, False)
        txtStockType.Visible = IIf(lblCategory.Text = "C", True, False)

        txtItemPrefix.Visible = IIf(lblCategory.Text = "C", True, False)
        lblItemCodePrefix.Visible = IIf(lblCategory.Text = "C", True, False)

        Frame1.Visible = IIf(lblCategory.Text = "C", True, False)
        Frame2.Visible = IIf(lblCategory.Text = "C", True, False)
        Frame3.Visible = IIf(lblCategory.Text = "C", True, False)

        lblStockType.Enabled = IIf(lblCategory.Text = "C", True, False)
        txtStockType.Enabled = IIf(lblCategory.Text = "C", True, False)

        chkER1.Visible = IIf(lblCategory.Text = "C", True, False)
        chkER1.Enabled = IIf(lblCategory.Text = "C", True, False)

        chkIndentItem.Visible = IIf(lblCategory.Text = "C", True, False)
        chkBOMItem.Visible = IIf(lblCategory.Text = "C", True, False)
        chkCostingRequired.Visible = IIf(lblCategory.Text = "C", True, False)
        chkTCRequired.Visible = IIf(lblCategory.Text = "C", True, False)
        chkTPReport.Visible = IIf(lblCategory.Text = "C", True, False)
        chkQFRRequired.Visible = IIf(lblCategory.Text = "C", True, False)
        chkMaxLevel.Visible = IIf(lblCategory.Text = "C", True, False)

        chkAutoIssueSubStore.Visible = IIf(lblCategory.Text = "C", True, False)

        chkIndentItem.Enabled = IIf(lblCategory.Text = "C", True, False)
        chkBOMItem.Enabled = IIf(lblCategory.Text = "C", True, False)
        chkCostingRequired.Enabled = IIf(lblCategory.Text = "C", True, False)
        chkTCRequired.Enabled = IIf(lblCategory.Text = "C", True, False)
        chkTPReport.Enabled = IIf(lblCategory.Text = "C", True, False)
        chkQFRRequired.Enabled = IIf(lblCategory.Text = "C", True, False)
        chkMaxLevel.Enabled = IIf(lblCategory.Text = "C", True, False)

        chkAutoIssueSubStore.Enabled = IIf(lblCategory.Text = "C", True, False)

        chkQuotation.Visible = IIf(lblCategory.Text = "C", True, False)
        chkQuotation.Enabled = IIf(lblCategory.Text = "C", True, False)

        chkAutoMovement.Visible = IIf(lblCategory.Text = "C", True, False)
        chkAutoMovement.Enabled = IIf(lblCategory.Text = "C", True, False)

        optClassification(0).Visible = IIf(lblCategory.Text = "C", True, False)
        optClassification(0).Enabled = IIf(lblCategory.Text = "C", True, False)

        optClassification(1).Visible = IIf(lblCategory.Text = "C", True, False)
        optClassification(1).Enabled = IIf(lblCategory.Text = "C", True, False)


        optClassification(2).Visible = IIf(lblCategory.Text = "C", True, False)
        optClassification(2).Enabled = IIf(lblCategory.Text = "C", True, False)

        optClassification(3).Visible = IIf(lblCategory.Text = "C", True, False)
        optClassification(3).Enabled = IIf(lblCategory.Text = "C", True, False)


        If RsCompany.Fields("AUTO_ISSUE").Value = "N" Then
            chkAutoMovement.Enabled = False
        End If

        lblDesc.Visible = IIf(lblCategory.Text = "C", True, False)
        CboType.Visible = IIf(lblCategory.Text = "C", True, False)

        lblDesc.Enabled = IIf(lblCategory.Text = "C", True, False)
        CboType.Enabled = IIf(lblCategory.Text = "C", True, False)

        '    lblDivision.Enabled = IIf(lblCategory.text = "C", True, False)	
        '    txtHSNCode.Enabled = IIf(lblCategory.text = "C", True, False)	
        '    cmdsearchHSN.Enabled = IIf(lblCategory.text = "C", True, False)	

        '    lblDivision.Visible = IIf(lblCategory.text = "C", True, False)	
        '    txtHSNCode.Visible = IIf(lblCategory.text = "C", True, False)	
        '    cmdsearchHSN.Visible = IIf(lblCategory.text = "C", True, False)	


        lblAcctConsumption.Enabled = IIf(lblCategory.Text = "C", True, False)
        txtAcctConsumption.Enabled = IIf(lblCategory.Text = "C", True, False)

        lblAcctConsumption.Visible = IIf(lblCategory.Text = "C", True, False)
        txtAcctConsumption.Visible = IIf(lblCategory.Text = "C", True, False)


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        AssignGrid(False)
        SetTextLengths()
        Clear1()
        If CmdAdd.Enabled = True Then CmdAdd_Click(CmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        'Resume	
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmCategoryMst_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsGeneral = Nothing
        RsGeneral.Close()
    End Sub

    Private Sub frmCategoryMst_KeyDown(sender As Object, EventArgs As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Dim KeyCode As Short = EventArgs.KeyCode
        Dim Shift As Short = EventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub frmCategoryMst_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub

    Private Sub txtAcctConsumption_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAcctConsumption.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtAcctConsumption.Text)
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub

    Private Sub txtAcctConsumption_KeyUp(sender As Object, EventArgs As System.Windows.Forms.KeyEventArgs) Handles txtAcctConsumption.KeyUp
        Dim KeyCode As Short = EventArgs.KeyCode
        Dim Shift As Short = EventArgs.KeyData \ &H10000
    End Sub

    Private Sub txtAcctConsumption_TextChanged(sender As Object, e As System.EventArgs) Handles txtAcctConsumption.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAcctConsumption_Validating(sender As Object, EventArgs As System.ComponentModel.CancelEventArgs) Handles txtAcctConsumption.Validating
        Dim Cancel As Boolean = EventArgs.Cancel
        On Error GoTo ERR1
        Sqlstr = ""
        If Trim(txtAcctConsumption.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable(txtAcctConsumption.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='O'") = False Then
            MsgBox("Invalid Account Name.")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        EventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCode_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCode.Text)
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCode_KeyUp(sender As Object, EventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCode.KeyUp
        Dim KeyCode As Short = EventArgs.KeyCode
        Dim Shift As Short = EventArgs.KeyData \ &H10000
    End Sub

    Private Sub txtCode_TextChanged(sender As Object, e As System.EventArgs) Handles txtCode.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCode_Validating(sender As Object, EventArgs As System.ComponentModel.CancelEventArgs) Handles txtCode.Validating
        Dim Cancel As Boolean = EventArgs.Cancel

        On Error GoTo ERR1
        Sqlstr = ""
        If Trim(txtCode.Text) = "" Then GoTo EventExitSub
        If MODIFYMode = True And RsGeneral.EOF = False Then xCode = RsGeneral.Fields("GEN_CODE").Value

        Sqlstr = "SELECT * FROM INV_GENERAL_MST " & vbCrLf _
                  & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                  & " AND GEN_CODE='" & MainClass.AllowSingleQuote(UCase((Trim(txtCode.Text)))) & "'" & vbCrLf _
                  & " AND GEN_TYPE='" & lblCategory.Text & "'"

        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGeneral, ADODB.LockTypeEnum.adLockReadOnly)

        If RsGeneral.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Category Code Does Not Exist In Master" & vbCrLf & "Click Add To Add In Master")
                Cancel = True
            ElseIf MODIFYMode = True Then
                Sqlstr = ""
                Sqlstr = "SELECT * FROM INV_GENERAL_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND GEN_CODE='" & xCode & "'" & vbCrLf & " AND GEN_TYPE='" & lblCategory.Text & "'"

                MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGeneral, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        EventArgs.Cancel = Cancel
    End Sub

    Private Sub frmCategoryMst_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Set PvtDBCn = New ADODB.Connection	
        'PvtDBCn.Open StrConn	

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CmdView.Text = ConCmdGridViewCaption
        ADDMode = False
        MODIFYMode = False

        CboType.Items.Clear()
        CboType.Items.Add("General Consumable")
        CboType.Items.Add("Production")
        CboType.Items.Add("Jobwork Third Party")
        CboType.Items.Add("Consumable (Production)")
        CboType.Items.Add("Tool")
        CboType.Items.Add("Assets")
        CboType.Items.Add("Raw Material") '' (Sheets)	
        CboType.Items.Add("BOP")
        CboType.Items.Add("InHouse")
        CboType.Items.Add("Development - BOP/RM")
        CboType.Items.Add("Maintances Item")
        CboType.Items.Add("Expense General")
        CboType.Items.Add("Service / Work")
        CboType.Items.Add("1. Trolly & Bins")
        CboType.Items.Add("2. Third Party Materials")
        CboType.Items.Add("3. Raw Material (Tubes)")
        CboType.Items.Add("4. Scrap")
        '     CboType.AddItem "4. Diesel"	
        '     CboType.AddItem "5. CO2"	
        '     CboType.AddItem "6. Iron Scrap"	
        '     CboType.AddItem "7. MIG Wire"	

        CboType.SelectedIndex = 0
        Call SetMainFormCordinate(Me)
        Me.Left = 0
        Me.Top = 0
        'Me.Height = VB6.TwipsToPixelsY(5895)
        ''Me.Width = VB6.TwipsToPixelsX(8265)
        'ResizeForm.FindAllControls(Me)
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

   Private Sub CmdSave_Click(sender As Object, e As System.EventArgs) Handles CmdSave.Click
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

   Private Sub txtDesc_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDesc.KeyPress
      Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDesc.Text)
        EventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         EventArgs.Handled = True
      End If
   End Sub

   Private Sub txtDesc_TextChanged(sender As Object, e As System.EventArgs) Handles txtDesc.TextChanged
      MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
   End Sub

   Private Sub txtPurchase_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPurchase.KeyPress
      Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

      KeyAscii = MainClass.UpperCase(KeyAscii, txtPurchase.Text)
      EventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         EventArgs.Handled = True
      End If
   End Sub

   Private Sub txtPurchase_KeyUp(sender As Object, EventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPurchase.KeyUp
      Dim KeyCode As Short = EventArgs.KeyCode
      Dim Shift As Short = EventArgs.KeyData \ &H10000
   End Sub

   Private Sub txtPurchase_TextChanged(sender As Object, e As System.EventArgs) Handles txtPurchase.TextChanged
      MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
   End Sub

   Private Sub txtSales_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSales.KeyPress
      Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

      KeyAscii = MainClass.UpperCase(KeyAscii, txtSales.Text)
      EventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         EventArgs.Handled = True
      End If
   End Sub

   Private Sub txtSales_KeyUp(sender As Object, EventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSales.KeyUp
      Dim KeyCode As Short = EventArgs.KeyCode
      Dim Shift As Short = EventArgs.KeyData \ &H10000
   End Sub

   Private Sub txtSales_TextChanged(sender As Object, e As System.EventArgs) Handles txtSales.TextChanged
      MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
   End Sub

   Private Sub txtStockType_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtStockType.KeyPress
      Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

      KeyAscii = MainClass.UpperCase(KeyAscii, txtStockType.Text)
      EventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         EventArgs.Handled = True
      End If
   End Sub

   Private Sub txtStockType_KeyUp(sender As Object, EventArgs As System.Windows.Forms.KeyEventArgs) Handles txtStockType.KeyUp
      Dim KeyCode As Short = EventArgs.KeyCode
      Dim Shift As Short = EventArgs.KeyData \ &H10000
   End Sub

   Private Sub txtStockType_TextChanged(sender As Object, e As System.EventArgs) Handles txtStockType.TextChanged
      MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
   End Sub

   Private Sub txtStockType_Validating(sender As Object, EventArgs As System.ComponentModel.CancelEventArgs) Handles txtStockType.Validating
      Dim Cancel As Boolean = EventArgs.Cancel
      On Error GoTo ERR1
      Sqlstr = ""
      If Trim(txtStockType.Text) = "" Then GoTo EventExitSub

      If MainClass.ValidateWithMasterTable(txtStockType.Text, "STOCK_TYPE_CODE", "STOCK_TYPE_DESC", "INV_TYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
         MsgBox("Invalid Stock Type")
         Cancel = True
      End If
      GoTo EventExitSub
ERR1:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
      EventArgs.Cancel = Cancel
   End Sub

   Private Sub txtDesc_Validating(sender As Object, EventArgs As System.ComponentModel.CancelEventArgs) Handles txtDesc.Validating
      Dim Cancel As Boolean = EventArgs.Cancel

      On Error GoTo ERR1
      Sqlstr = ""
      If Trim(txtDesc.Text) = "" Then GoTo EventExitSub
      If MODIFYMode = True And RsGeneral.EOF = False Then xCode = RsGeneral.Fields("GEN_CODE").Value

      Sqlstr = "SELECT * FROM INV_GENERAL_MST " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND GEN_DESC='" & MainClass.AllowSingleQuote(UCase((Trim(txtDesc.Text)))) & "'" & vbCrLf _
                & " AND GEN_TYPE='" & lblCategory.Text & "'"

      MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGeneral, ADODB.LockTypeEnum.adLockReadOnly)

      If RsGeneral.EOF = False Then
         ADDMode = False
         MODIFYMode = False
         Show1()
      Else
         If ADDMode = False And MODIFYMode = False Then
            MsgBox("Category Description Does Not Exist In Master" & vbCrLf & "Click Add To Add In Master")
            Cancel = True
         ElseIf MODIFYMode = True Then
            Sqlstr = ""
            Sqlstr = "SELECT * FROM INV_GENERAL_MST " & vbCrLf _
               & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
               & " AND GEN_CODE='" & xCode & "'" & vbCrLf _
               & " AND GEN_TYPE='" & lblCategory.Text & "'"

            MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGeneral, ADODB.LockTypeEnum.adLockReadOnly)
         End If
      End If
      GoTo EventExitSub
ERR1:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
      EventArgs.Cancel = Cancel
   End Sub

   Private Sub optClassification_CheckedChanged(eventSender As Object, e As System.EventArgs) Handles optClassification.CheckedChanged
      If eventSender.Checked Then
         Dim Index As Short = optClassification.GetIndex(eventSender)

         MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
      End If
   End Sub

   Private Sub SprdView_DblClick(sender As Object, e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
      SprdView.Col = 1
      SprdView.Row = SprdView.ActiveRow
      txtCode.Text = Trim(SprdView.Text)
      txtCode_Validating(txtCode, New System.ComponentModel.CancelEventArgs(False))
      CmdView_Click(CmdView, New System.EventArgs())
   End Sub

    Private Sub frmCategoryMst_Resize(sender As Object, e As System.EventArgs) Handles Me.Resize
        'ResizeForm.ResizeAllControls(Me)
    End Sub
    Private Sub chkAutoIssueSubStore_CheckStateChanged(sender As Object, e As System.EventArgs) Handles chkAutoIssueSubStore.CheckStateChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
End Class
