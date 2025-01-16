Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmSysPref
    Inherits System.Windows.Forms.Form

    Dim XRIGHT As String
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim PostTaxWiseSale As Byte
    Dim PostSaleAcCode As Integer

    Private Const ConRowHeight As Short = 12

    Private Const ColFromAccountName As Short = 1
    Private Const ColToAccountName As Short = 2

    Private Const ColCategoryName As Short = 1
    Private Const ColOPAccount As Short = 2
    Private Const ColCLAccount As Short = 3


    Private Sub ShowForwardOption()

        On Error GoTo ErrPart
        Dim mSqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntRow As Integer

        mSqlStr = " SELECT FROM_ACCOUNT, TO_ACCOUNT, " & vbCrLf _
          & " FROMCMST.SUPP_CUST_NAME AS FROM_NAME, TOCMST.SUPP_CUST_NAME AS TO_NAME" & vbCrLf _
          & " FROM GEN_CARRYFORWARD_MST TRN, FIN_SUPP_CUST_MST FROMCMST, FIN_SUPP_CUST_MST TOCMST" & vbCrLf _
          & " WHERE TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
          & " AND TRN.COMPANY_CODE=FROMCMST.COMPANY_CODE" & vbCrLf _
          & " AND TRN.FROM_ACCOUNT=FROMCMST.SUPP_CUST_CODE" & vbCrLf _
          & " AND TRN.COMPANY_CODE=TOCMST.COMPANY_CODE" & vbCrLf _
          & " AND TRN.TO_ACCOUNT=TOCMST.SUPP_CUST_CODE"

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        cntRow = 1
        If RsTemp.EOF = False Then
            Do While Not RsTemp.EOF
                SprdMain.Row = cntRow
                SprdMain.Col = ColFromAccountName
                SprdMain.Text = IIf(IsDBNull(RsTemp.Fields("FROM_NAME").Value), "", RsTemp.Fields("FROM_NAME").Value)

                SprdMain.Col = ColToAccountName
                SprdMain.Text = IIf(IsDBNull(RsTemp.Fields("TO_NAME").Value), "", RsTemp.Fields("TO_NAME").Value)

                RsTemp.MoveNext()
                cntRow = cntRow + 1
                SprdMain.MaxRows = cntRow
            Loop
        End If

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub ShowCategoryMapping()

        On Error GoTo ErrPart
        Dim mSqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntRow As Integer
        Dim mCatCode As String
        Dim mValueCode As String
        Dim mValueName As String

        mSqlStr = " SELECT GEN_DESC, GEN_CODE " & vbCrLf _
          & " FROM INV_GENERAL_MST" & vbCrLf _
          & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'" & vbCrLf _
          & " ORDER BY GEN_DESC"

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        cntRow = 1
        If RsTemp.EOF = False Then
            Do While Not RsTemp.EOF
                SprdCategory.Row = cntRow
                SprdCategory.Col = ColCategoryName
                SprdCategory.Text = IIf(IsDBNull(RsTemp.Fields("GEN_DESC").Value), "", RsTemp.Fields("GEN_DESC").Value)

                mCatCode = IIf(IsDBNull(RsTemp.Fields("GEN_CODE").Value), "", RsTemp.Fields("GEN_CODE").Value)

                SprdCategory.Col = ColOPAccount
                mValueCode = GetCategoryMapCode(mCatCode, "OP")
                mValueName = ""
                If MainClass.ValidateWithMasterTable(mValueCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mValueName = MasterNo
                End If


                SprdCategory.Text = mValueName

                SprdCategory.Col = ColCLAccount
                mValueCode = GetCategoryMapCode(mCatCode, "CL")
                mValueName = ""
                If MainClass.ValidateWithMasterTable(mValueCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mValueName = MasterNo
                End If


                SprdCategory.Text = mValueName

                RsTemp.MoveNext()
                cntRow = cntRow + 1
                SprdCategory.MaxRows = cntRow
            Loop
        End If

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function GetCategoryMapCode(ByRef pCategoryCode As String, ByRef pType As String) As String

        On Error GoTo ErrPart
        Dim mSqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mFieldName As String

        If pType = "OP" Then
            mFieldName = "OP_ACCOUNT"
        Else
            mFieldName = "CL_ACCOUNT"
        End If
        GetCategoryMapCode = ""

        mSqlStr = " SELECT " & mFieldName & " AS MAPP_CODE " & vbCrLf _
                  & " FROM GEN_CATEGORY_MAPPING_MST TRN" & vbCrLf _
                  & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                  & " AND CATEGORY_CODE='" & pCategoryCode & "'"

        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetCategoryMapCode = IIf(IsDBNull(RsTemp.Fields("MAPP_CODE").Value), "", RsTemp.Fields("MAPP_CODE").Value)
        End If

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function UpdateCatgeoryMapping() As Boolean
        On Error GoTo ErrPart
        Dim mSqlStr As String = ""
        Dim cntRow As Integer
        Dim mOPName As String
        Dim mOPCode As String
        Dim mCLName As String
        Dim mCLCode As String
        Dim mCategoryName As String
        Dim mCategoryCode As String

        UpdateCatgeoryMapping = False

        mSqlStr = "DELETE FROM GEN_CATEGORY_MAPPING_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        PubDBCn.Execute(mSqlStr)

        mSqlStr = ""

        With SprdCategory
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColCategoryName
                mCategoryName = Trim(.Text)

                If MainClass.ValidateWithMasterTable(mCategoryName, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                    mCategoryCode = MasterNo
                Else
                    mCategoryCode = ""
                End If

                .Col = ColOPAccount
                mOPName = Trim(UCase(.Text))


                .Col = ColCLAccount
                mCLName = Trim(UCase(.Text))

                If MainClass.ValidateWithMasterTable(mOPName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mOPCode = MasterNo
                Else
                    mOPCode = ""
                End If

                If MainClass.ValidateWithMasterTable(mCLName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mCLCode = MasterNo
                Else
                    mCLCode = ""
                End If


                If mCategoryCode <> "" Then
                    mSqlStr = " INSERT INTO GEN_CATEGORY_MAPPING_MST ( " & vbCrLf _
                            & " COMPANY_CODE, CATEGORY_CODE, OP_ACCOUNT, CL_ACCOUNT )" & vbCrLf _
                            & " VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", '" & mCategoryCode & "'," & vbCrLf _
                            & " '" & mOPCode & "', '" & mCLCode & "'" & vbCrLf _
                            & " )"

                    PubDBCn.Execute(mSqlStr)
                    mSqlStr = ""
                End If

            Next
        End With
        UpdateCatgeoryMapping = True
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateCatgeoryMapping = False
    End Function
    Private Function UpdateCarryOption() As Boolean
        On Error GoTo ErrPart
        Dim mSqlStr As String = ""
        Dim cntRow As Integer
        Dim mFromName As String
        Dim mFromCode As String
        Dim mToName As String
        Dim mToCode As String

        UpdateCarryOption = False

        mSqlStr = "DELETE FROM GEN_CARRYFORWARD_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        PubDBCn.Execute(mSqlStr)

        mSqlStr = ""

        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColFromAccountName
                mFromName = Trim(UCase(.Text))


                .Col = ColToAccountName
                mToName = Trim(UCase(.Text))

                If MainClass.ValidateWithMasterTable(mFromName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mFromCode = MasterNo
                Else
                    mFromCode = "-1"
                End If

                If MainClass.ValidateWithMasterTable(mToName, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mToCode = MasterNo
                Else
                    mToCode = "-1"
                End If


                If mFromCode <> "-1" Or mToCode <> "-1" Then
                    mSqlStr = "INSERT INTO GEN_CARRYFORWARD_MST ( " & vbCrLf _
                   & " COMPANY_CODE, FROM_ACCOUNT, TO_ACCOUNT )" & vbCrLf _
                   & " VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                   & " '" & mFromCode & "', '" & mToCode & "'" & vbCrLf _
                   & " )"

                    PubDBCn.Execute(mSqlStr)
                    mSqlStr = ""
                End If

            Next
        End With
        UpdateCarryOption = True
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateCarryOption = False
    End Function

    Private Sub chkAttMc_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAttMc.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkAutoIssue_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAutoIssue.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkAutoProdIssue_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAutoProdIssue.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkBOPCheck_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkBOPCheck.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkBOPMaxLevel_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkBOPMaxLevel.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkConsMaxLevel_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkConsMaxLevel.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkDespatch_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkDespatch.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkFGCheck_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkFGCheck.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkGateEntry_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkGateEntry.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub chkGatepass_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkGatepass.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkInvoice_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkInvoice.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkInvoiceA4_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkInvPrePrint_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)

    End Sub

    Private Sub chkInvTableCC_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkInvTableCC.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkInvTableFYear_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkInvTableFYear.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkMaintMaxLevel_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkMaintMaxLevel.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkMaxInvInGate_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkMaxInvInGate.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkMRR_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkMRR.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkOnLine_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkOnLine.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkPOCheckInGE_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkPOCheckInGE.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkCheckPORate_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCheckPORate.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkPOPrintApproval_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkPOPrintApproval.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkCreditLimit_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCreditLimit.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    'Private PvtDBCN As ADODB.Connection		
    Private Sub chkPrintBotCompanyAddress_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkPrintBotCompanyAddress.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub ChkPrintBotCompanyName_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkPrintBotCompanyName.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkPrintBotCompanyPhone_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkPrintBotCompanyPhone.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub ChkPrintPAgeNo_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkPrintPAgeNo.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub ChkPrintRunDate_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkPrintRunDate.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkPrintTopCompanyAddress_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkPrintTopCompanyAddress.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkPrintTopCompanyPhone_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkPrintTopCompanyPhone.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkPrintUser_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkPrintUser.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkPurchase_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkPurchase.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkPurPlanning_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkPurPlanning.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkRateDiffCN_App_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkRateDiffCN_App.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkRateDiffCN_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkRateDiffCN.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkRateDiffDN_App_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkRateDiffDN_App.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkRateDiffDN_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkRateDiffDN.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkRejection_App_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkRejection_App.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkRejection_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkRejection.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkRGPPrePrint_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkRJDespatchNote_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkRJDespatchNote.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkRMMaxLevel_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkRMMaxLevel.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkShortage_App_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkShortage_App.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkShortage_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkShortage.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkStockBal_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkStockBal.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkSTReceivable_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkWeeklySchd_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkWeeklySchd.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdcancel.Click
        Me.Hide()
        Me.Close()
        'Me = Nothing		
    End Sub
    Private Sub CmdDefaultMargin_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDefaultMargin.Click

        txtMargin(0).Text = CStr(1)
        txtMargin(1).Text = CStr(1)
        txtMargin(2).Text = CStr(0.5)
        txtMargin(3).Text = CStr(0)
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        If FieldVerification = False Then Exit Sub
        If Update1 = True Then CmdSave.Enabled = False
    End Sub

    Private Sub frmSysPref_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        Call SetMainFormCordinate(Me)

        SSTab1.SelectedIndex = 0
        ''Set PvtDBCn = New ADODB.Connection		
        ''PvtDBCn.Open StrConn		
        XRIGHT = MainClass.STRMenuRight(PubUserID, 1, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        SetMaxLength()
        MainClass.SetControlsColor(Me)
        Call SetMainFormCordinate(Me)
        'Me.Left = 0
        'Me.Top = 0

        FormatSprdMain(-1)
        FormatSprdCategory(-1)

        ADDMode = False
        MODIFYMode = False
        If XRIGHT <> "" Then MODIFYMode = True
        Show1()
    End Sub
    Private Sub SetMaxLength()


        Dim mAccountLength As Integer

        txtMargin(0).Maxlength = 3
        txtMargin(1).Maxlength = 3
        txtMargin(2).Maxlength = 3
        txtMargin(3).Maxlength = 3

        mAccountLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)

        txtTDSCircle.Maxlength = RsCompany.Fields("TDSCIRCLE").DefinedSize ''		
        txtTDSAcNo.Maxlength = RsCompany.Fields("TDSACNO").DefinedSize ''		
        txtPANNo.Maxlength = RsCompany.Fields("PAN_NO").DefinedSize ''		
        txtTDSCreditAcct.Maxlength = mAccountLength ''MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)		
        txtSTDS.Maxlength = mAccountLength ''MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)		
        txtESI.Maxlength = mAccountLength ''MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)		

        txtAuthorized.Maxlength = RsCompany.Fields("TDSAUTHORIZED").DefinedSize
        txtAuthorizedFName.Maxlength = RsCompany.Fields("TDSAUTHORIZED_FNAME").DefinedSize
        txtDesignation.Maxlength = RsCompany.Fields("TDSAUTHORIZED_DESIG").DefinedSize ''		


    End Sub
    Private Sub Show1()
        On Error GoTo ERR1
        ShowReports()
        ''ShowAccounts		
        ShowTDS()
        ShowSaleReturn()
        ShowOthers()
        ShowForwardOption()
        ShowCategoryMapping()
        'cmdSave.Enabled = False
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Sub ShowReports()
        On Error GoTo ERR1
        ChkPrintTopCompanyName.CheckState = IIf(IsDbNull(RsCompany.Fields("PrintTopCompanyName").Value) Or RsCompany.Fields("PrintTopCompanyName").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        chkPrintTopCompanyAddress.CheckState = IIf(IsDbNull(RsCompany.Fields("PrintTopCompanyAddress").Value) Or RsCompany.Fields("PrintTopCompanyAddress").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        chkPrintTopCompanyPhone.CheckState = IIf(IsDbNull(RsCompany.Fields("PRintTopCompanyPhone").Value) Or RsCompany.Fields("PRintTopCompanyPhone").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        ChkPrintBotCompanyName.CheckState = IIf(IsDbNull(RsCompany.Fields("PrintBotCompanyName").Value) Or RsCompany.Fields("PrintBotCompanyName").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        chkPrintBotCompanyAddress.CheckState = IIf(IsDbNull(RsCompany.Fields("PrintBotCompanyAddress").Value) Or RsCompany.Fields("PrintBotCompanyAddress").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        chkPrintBotCompanyPhone.CheckState = IIf(IsDbNull(RsCompany.Fields("PrintBotCompanyPhone").Value) Or RsCompany.Fields("PrintBotCompanyPhone").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        If RsCompany.Fields("PrintCompanyFull_ShortName").Value = "F" Then
            OptPrintCompanyFull_ShortName(0).Checked = True
        Else
            OptPrintCompanyFull_ShortName(1).Checked = True
        End If

        If RsCompany.Fields("INVOICE_PRINT_STYLE").Value = "P" Then
            optPrintPortrait.Checked = True
        Else
            optPrintLandScape.Checked = True
        End If

        If RsCompany.Fields("INVOICE_A4").Value = "Y" Then
            optA4.Checked = True
            optA3.Checked = False
        Else
            optA3.Checked = True
            optA4.Checked = False
        End If

        chkPrintUser.CheckState = IIf(IsDbNull(RsCompany.Fields("Printuser").Value) Or RsCompany.Fields("Printuser").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        ChkPrintRunDate.CheckState = IIf(IsDbNull(RsCompany.Fields("PrintrunDate").Value) Or RsCompany.Fields("PrintrunDate").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        ChkPrintPAgeNo.CheckState = IIf(IsDbNull(RsCompany.Fields("PrintPageNo").Value) Or RsCompany.Fields("PrintPageNo").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

        txtMargin(0).Text = IIf(IsDbNull(RsCompany.Fields("REPORTMARGINTOP").Value), "", RsCompany.Fields("REPORTMARGINTOP").Value)
        txtMargin(1).Text = IIf(IsDbNull(RsCompany.Fields("REPORTMARGINBOT").Value), "", RsCompany.Fields("REPORTMARGINBOT").Value)
        txtMargin(2).Text = IIf(IsDbNull(RsCompany.Fields("REPORTMARGINLEFT").Value), "", RsCompany.Fields("REPORTMARGINLEFT").Value)
        txtMargin(3).Text = IIf(IsDbNull(RsCompany.Fields("REPORTMARGINRIGHT").Value), "", RsCompany.Fields("REPORTMARGINRIGHT").Value)

        txtQCDays.Text = IIf(IsDbNull(RsCompany.Fields("QC_ALLOW_DAYS").Value), "", RsCompany.Fields("QC_ALLOW_DAYS").Value)
        chkMaxInvInGate.CheckState = IIf(RsCompany.Fields("CHECK_MAX_INV_GE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        chkRJDespatchNote.CheckState = IIf(RsCompany.Fields("GEN_RJ_DESPATCH").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

        chkBOPCheck.CheckState = IIf(RsCompany.Fields("CHECK_BOP_STOCK").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        chkFGCheck.CheckState = IIf(RsCompany.Fields("CHECK_FG_STOCK").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

        chkInvTableCC.CheckState = IIf(RsCompany.Fields("INV_TAB_CC").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        chkInvTableFYear.CheckState = IIf(RsCompany.Fields("INV_TAB_FY").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        chkOnLine.CheckState = IIf(RsCompany.Fields("COMP_ONLINE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        chkAttMc.CheckState = IIf(RsCompany.Fields("ATTN_MC").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

        optPOPrint(0).Checked = IIf(RsCompany.Fields("PO_PREPRINT").Value = "Y", True, False)
        optPOPrint(1).Checked = IIf(RsCompany.Fields("PO_PREPRINT").Value = "N", True, False)
        chkPOPrintApproval.CheckState = IIf(RsCompany.Fields("PO_PRINT_APP_REQ").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        chkPOPrintApproval.Enabled = IIf(chkPOPrintApproval.CheckState = System.Windows.Forms.CheckState.Checked, False, True)

        chkCreditLimit.CheckState = IIf(RsCompany.Fields("CREDIT_LIMIT_APP").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        chkCreditLimit.Enabled = IIf(PubUserID = "G0416", True, IIf(chkCreditLimit.CheckState = System.Windows.Forms.CheckState.Checked, False, True))



        chkInvTableCC.Enabled = IIf(chkInvTableCC.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        chkInvTableFYear.Enabled = IIf(chkInvTableFYear.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        chkOnLine.Enabled = IIf(chkOnLine.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        chkAttMc.Enabled = IIf(chkAttMc.CheckState = System.Windows.Forms.CheckState.Checked, False, True)

        chkMRR.CheckState = IIf(RsCompany.Fields("SEPARATE_MRR_SERIES").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        chkDespatch.CheckState = IIf(RsCompany.Fields("SEPARATE_DSP_SERIES").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        chkGatepass.CheckState = IIf(RsCompany.Fields("SEPARATE_RGP_SERIES").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        chkInvoice.CheckState = IIf(RsCompany.Fields("SEPARATE_INV_SERIES").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        chkPurchase.CheckState = IIf(RsCompany.Fields("SEPARATE_PUR_SERIES").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

        chkGateEntry.CheckState = IIf(RsCompany.Fields("MRR_AGT_GE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        chkPOCheckInGE.CheckState = IIf(RsCompany.Fields("PO_IN_GE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        chkCheckPORate.CheckState = IIf(RsCompany.Fields("CHECK_PO_RATE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

        chkBOPMaxLevel.CheckState = IIf(RsCompany.Fields("BOP_MAX_LEVEL").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        chkRMMaxLevel.CheckState = IIf(RsCompany.Fields("RM_MAX_LEVEL").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        chkConsMaxLevel.CheckState = IIf(RsCompany.Fields("CONS_MAX_LEVEL").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        chkMaintMaxLevel.CheckState = IIf(RsCompany.Fields("MAINT_MAX_LEVEL").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

        chkBOPMaxLevel.Enabled = IIf(chkBOPMaxLevel.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        chkRMMaxLevel.Enabled = IIf(chkRMMaxLevel.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        chkConsMaxLevel.Enabled = IIf(chkConsMaxLevel.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        chkMaintMaxLevel.Enabled = IIf(chkMaintMaxLevel.CheckState = System.Windows.Forms.CheckState.Checked, False, True)

        If RsCompany.Fields("PROD_PALN_LOCK").Value = "Y" Then
            optProductionPlanLocking(0).Checked = True
            optProductionPlanLocking(1).Checked = False
            optProductionPlanLocking(0).Enabled = False
            optProductionPlanLocking(1).Enabled = False
            txtPlanLockDays.Text = IIf(IsDbNull(RsCompany.Fields("PROD_PALN_LOCK_DAY").Value), 0, RsCompany.Fields("PROD_PALN_LOCK_DAY").Value)
            txtPlanLockDays.Enabled = False
        Else
            optProductionPlanLocking(1).Checked = True
            optProductionPlanLocking(0).Checked = False
            optProductionPlanLocking(0).Enabled = True
            optProductionPlanLocking(1).Enabled = True
            txtPlanLockDays.Text = CStr(0)
            txtPlanLockDays.Enabled = True
        End If

        If RsCompany.Fields("PO_LOCK").Value = "Y" Then
            optPOLocking(0).Checked = True
            optPOLocking(0).Enabled = False
            optPOLocking(1).Enabled = False
        Else
            optPOLocking(1).Checked = True
            optPOLocking(0).Enabled = True
            optPOLocking(1).Enabled = True
        End If

        If RsCompany.Fields("SO_LOCK").Value = "Y" Then
            optSOLocking(0).Checked = True
            optSOLocking(0).Enabled = False
            optSOLocking(1).Enabled = False
        Else
            optSOLocking(1).Checked = True
            optSOLocking(0).Enabled = True
            optSOLocking(1).Enabled = True
        End If

        If RsCompany.Fields("LOADIND_APP").Value = "Y" Then
            optLoadingSlip(0).Checked = True
            optLoadingSlip(1).Checked = False
            optLoadingSlip(0).Enabled = False
            optLoadingSlip(1).Enabled = False
            txtLoadingAppDate.Enabled = False
            txtLoadingAppDate.Text = VB6.Format(IIf(IsDBNull(RsCompany.Fields("LOADING_APP_DATE").Value), "__/__/____", RsCompany.Fields("LOADING_APP_DATE").Value), "dd/MM/yyyy")
        Else
            optLoadingSlip(0).Checked = False
            optLoadingSlip(1).Checked = True
            optLoadingSlip(0).Enabled = True
            optLoadingSlip(1).Enabled = True
            txtLoadingAppDate.Enabled = True
            txtLoadingAppDate.Text = "__/__/____"
        End If


        If RsCompany.Fields("INV_GENERATE_24_HOURS").Value = "Y" Then
            optInvGen(0).Checked = True
            optInvGen(1).Checked = False
            txtInvTmFrom.Enabled = False
            txtInvTmTo.Enabled = False
        Else
            optInvGen(0).Checked = False
            optInvGen(1).Checked = True
            txtInvTmFrom.Text = VB6.Format(IIf(IsDBNull(RsCompany.Fields("INV_GENERATE_FROM_TM").Value), "", RsCompany.Fields("INV_GENERATE_FROM_TM").Value), "HH:MM")
            txtInvTmTo.Text = VB6.Format(IIf(IsDBNull(RsCompany.Fields("INV_GENERATE_TO_TM").Value), "", RsCompany.Fields("INV_GENERATE_TO_TM").Value), "HH:MM")
            txtInvTmFrom.Enabled = True
            txtInvTmTo.Enabled = True
        End If

        txtMaxPOItems.Text = IIf(IsDBNull(RsCompany.Fields("MAX_PO_ITEMS").Value), 0, RsCompany.Fields("MAX_PO_ITEMS").Value)
        txtPendingIndentNo.Text = IIf(IsDBNull(RsCompany.Fields("MAX_PENDING_INDENT").Value), 0, RsCompany.Fields("MAX_PENDING_INDENT").Value)

        Exit Sub
ERR1:
        '    Resume		
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Sub ShowSaleReturn()
        On Error GoTo ERR1

        txtCreditBank.Text = IIf(IsDBNull(RsCompany.Fields("CREDITBANK").Value), "", RsCompany.Fields("CREDITBANK").Value)
        txtCreditBankAddress.Text = IIf(IsDBNull(RsCompany.Fields("CREDITBANK_ADD").Value), "", RsCompany.Fields("CREDITBANK_ADD").Value)
        txtFurtherBank.Text = IIf(IsDBNull(RsCompany.Fields("FURTHER_BANK").Value), "", RsCompany.Fields("FURTHER_BANK").Value)
        txtADCode.Text = IIf(IsDBNull(RsCompany.Fields("AD_CODE").Value), "", RsCompany.Fields("AD_CODE").Value)

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Sub ShowOthers()
        On Error GoTo ERR1
        Dim mPDIRAcct As String
        Dim mPDIRCrAcct As String
        Dim mPostingType As String

        mPDIRAcct = IIf(IsDBNull(RsCompany.Fields("PDIR_ACCOUNT").Value), "-1", RsCompany.Fields("PDIR_ACCOUNT").Value)


        mPDIRCrAcct = IIf(IsDBNull(RsCompany.Fields("PDIR_CreditAcct").Value), "-1", RsCompany.Fields("PDIR_CreditAcct").Value)


        txtPDIRAmount.Text = CStr(Val(IIf(IsDBNull(RsCompany.Fields("PDIR_AMOUNT").Value), 0, RsCompany.Fields("PDIR_AMOUNT").Value)))
        txtMRRExcessPer.Text = CStr(Val(IIf(IsDBNull(RsCompany.Fields("GREXCESSPER").Value), 0, RsCompany.Fields("GREXCESSPER").Value)))

        txtInvoiceDigit.Text = CStr(Val(IIf(IsDBNull(RsCompany.Fields("INVOICE_DIGIT").Value), 0, RsCompany.Fields("INVOICE_DIGIT").Value)))

        txtMaxBillAmount.Text = CStr(Val(IIf(IsDBNull(RsCompany.Fields("BILLAMOUNT_LIMIT").Value), 0, RsCompany.Fields("BILLAMOUNT_LIMIT").Value)))

        chkAutoIssue.CheckState = IIf(RsCompany.Fields("AUTO_ISSUE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        chkAutoIssue.Enabled = IIf(RsCompany.Fields("AUTO_ISSUE").Value = "Y", False, True)
        txtAutoIssueDate.Text = VB6.Format(IIf(IsDBNull(RsCompany.Fields("AUTO_ISSUE_DATE").Value), "__/__/____", RsCompany.Fields("AUTO_ISSUE_DATE").Value), "dd/MM/yyyy")
        txtAutoIssueDate.Enabled = IIf(RsCompany.Fields("AUTO_ISSUE").Value = "Y", False, True)

        chkAutoProdIssue.CheckState = IIf(RsCompany.Fields("AUTO_ISSUE_PROD").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        chkAutoProdIssue.Enabled = IIf(RsCompany.Fields("AUTO_ISSUE_PROD").Value = "Y", False, True)
        txtAutoProdIssueDate.Text = VB6.Format(IIf(IsDBNull(RsCompany.Fields("AUTO_ISSUE_PROD_DATE").Value), "__/__/____", RsCompany.Fields("AUTO_ISSUE_PROD_DATE").Value), "dd/MM/yyyy")
        txtAutoProdIssueDate.Enabled = IIf(RsCompany.Fields("AUTO_ISSUE_PROD").Value = "Y", False, True)

        chkStockBal.CheckState = IIf(RsCompany.Fields("StockBalCheck").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        chkPurPlanning.CheckState = IIf(RsCompany.Fields("PUR_PLANNING").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        chkWeeklySchd.CheckState = IIf(RsCompany.Fields("WEEKLY_SCHD").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

        chkShortage.CheckState = IIf(RsCompany.Fields("Shortage_DN").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        chkShortage_App.CheckState = IIf(RsCompany.Fields("Shortage_DN_APP").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

        chkRejection.CheckState = IIf(RsCompany.Fields("Rejection_DN").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        chkRejection_App.CheckState = IIf(RsCompany.Fields("Rejection_DN_APP").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

        chkRateDiffDN.CheckState = IIf(RsCompany.Fields("RATE_Diff_DN").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        chkRateDiffDN_App.CheckState = IIf(RsCompany.Fields("RATE_Diff_DN_APP").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

        chkRateDiffCN.CheckState = IIf(RsCompany.Fields("RATE_Diff_CN").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        chkRateDiffCN_App.CheckState = IIf(RsCompany.Fields("RATE_Diff_CN_APP").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

        chkRateDiffCrWithGST.CheckState = IIf(RsCompany.Fields("RateDiff_CR_With_GST").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        chkRateDiffDrWithGST.CheckState = IIf(RsCompany.Fields("RateDiff_DR_With_GST").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        chkRejectionWithGST.CheckState = IIf(RsCompany.Fields("Rejection_With_GST").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        chkShortageWithGST.CheckState = IIf(RsCompany.Fields("Shortage_With_GST").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)


        chkHideBatchNo.CheckState = IIf(RsCompany.Fields("BATCHNO_HIDE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        chkHideHeatNo.CheckState = IIf(RsCompany.Fields("HEATNO_HIDE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

        ChkPrintOTInPayslip.CheckState = IIf(RsCompany.Fields("PRINTOTINPAYSLIP").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        ChkGSTSeparate.CheckState = IIf(RsCompany.Fields("GST_SEPARATE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        ChkAWavailable.CheckState = IIf(RsCompany.Fields("EWAYBILLAPP").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        ChkEInvoiceApp.CheckState = IIf(RsCompany.Fields("E_INVOICE_APP").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        ChkTCS.CheckState = IIf(RsCompany.Fields("TCS_APPLICABLE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        ChkPacketCol.CheckState = IIf(RsCompany.Fields("PACKETS_COL_SHOW").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        ChkInnerPackCol.CheckState = IIf(RsCompany.Fields("INNER_PACK_COL_SHOW").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        ChkOuterPackCol.CheckState = IIf(RsCompany.Fields("OUTER_PACK_COL_SHOW").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        ChkDivlocation.CheckState = IIf(RsCompany.Fields("DIV_AS_LOCATION").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        ChkBilladj.CheckState = IIf(RsCompany.Fields("MANNUAL_BILL_ADJUST").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        ChkDCInLedger.CheckState = IIf(RsCompany.Fields("IS_POST_DC_IN_LEDGER").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        ChkAutoGenCode.CheckState = IIf(RsCompany.Fields("AUTO_GEN_CODE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        ChkWarHouse.CheckState = IIf(RsCompany.Fields("IS_WAREHOUSE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        ChkAfterConfirm.CheckState = IIf(RsCompany.Fields("ENTITLE_AFTER_CONFIRM").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        ChkElFixed.CheckState = IIf(RsCompany.Fields("IS_EL_FIXED").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        ChkDoubleSalary.CheckState = IIf(RsCompany.Fields("DOUBLE_SALARY_OPTION").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        ChkSaleOrderIndent.CheckState = IIf(RsCompany.Fields("SALEORDER_WISE_INDENT").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        ChkLockPayterms.CheckState = IIf(RsCompany.Fields("LOCK_INVOICE_PAYTERMS").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        ChkMRPSaleOrder.CheckState = IIf(RsCompany.Fields("CHECK_MRP_SALEORDER").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        ChkINVFromStock.CheckState = IIf(RsCompany.Fields("INV_TAKEN_FROM_STOCK").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        ChkQtyCheck.CheckState = IIf(RsCompany.Fields("MINIMUN_QTY_CHECK_DESP").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        ChkSaleScheduleReq.CheckState = IIf(RsCompany.Fields("SALE_SCHEDULE_APP_REQUIRED").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        ChkBom.CheckState = IIf(RsCompany.Fields("CHECK_BOM_SO").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

        chkAcPRAutoJv.CheckState = IIf(RsCompany.Fields("AC_PR_AUTO_JV").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
        If MainClass.ValidateWithMasterTable(RsCompany.Fields("COMP_AC_CODE").Value, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,, "INTER_UNIT='Y'") = True Then
            txtCompAc.Text = MasterNo
        Else
            txtCompAc.Text = ""
        End If

        mPostingType = RsCompany.Fields("PURCHASE_POSTINGTYPE").Value

        If mPostingType = "B" Then
            optPurPostingType(0).Checked = True
        Else
            optPurPostingType(1).Checked = True
        End If

        mPostingType = RsCompany.Fields("SALES_POSTINGTYPE").Value

        If mPostingType = "B" Then
            optInvPostingType(0).Checked = True
        Else
            optInvPostingType(1).Checked = True
        End If

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub chkRateDiffCrWithGST_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkRateDiffCrWithGST.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkRateDiffDrWithGST_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkRateDiffDrWithGST.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkRejectionWithGST_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkRejectionWithGST.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkShortageWithGST_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkShortageWithGST.CheckStateChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub ShowTDS()
        On Error Resume Next
        Dim mTDSCRAcct As String
        Dim mESICRAcct As String
        Dim mSTDSCRAcct As String

        txtTDSCircle.Text = IIf(IsDBNull(RsCompany.Fields("TDSCIRCLE").Value), "", RsCompany.Fields("TDSCIRCLE").Value)
        txtTDSAcNo.Text = IIf(IsDBNull(RsCompany.Fields("TDSACNO").Value), "", RsCompany.Fields("TDSACNO").Value)
        txtPANNo.Text = IIf(IsDBNull(RsCompany.Fields("PAN_NO").Value), "", RsCompany.Fields("PAN_NO").Value)
        txtAuthorized.Text = IIf(IsDBNull(RsCompany.Fields("TDSAUTHORIZED").Value), "", RsCompany.Fields("TDSAUTHORIZED").Value)
        txtAuthorizedFName.Text = IIf(IsDBNull(RsCompany.Fields("TDSAUTHORIZED_FNAME").Value), "", RsCompany.Fields("TDSAUTHORIZED_FNAME").Value)
        txtDesignation.Text = IIf(IsDBNull(RsCompany.Fields("TDSAUTHORIZED_DESIG").Value), "", RsCompany.Fields("TDSAUTHORIZED_DESIG").Value)

        mTDSCRAcct = IIf(IsDBNull(RsCompany.Fields("TDSCREDITACCOUNT").Value), "-1", RsCompany.Fields("TDSCREDITACCOUNT").Value)



        mESICRAcct = IIf(IsDBNull(RsCompany.Fields("ESICREDITACCOUNT").Value), "-1", RsCompany.Fields("ESICREDITACCOUNT").Value)


        mSTDSCRAcct = IIf(IsDBNull(RsCompany.Fields("STDSCREDITACCOUNT").Value), "-1", RsCompany.Fields("STDSCREDITACCOUNT").Value)


    End Sub
    Private Function Update1() As Boolean

        On Error GoTo err_Renamed
        Dim SqlStr As String = ""
        Dim xCode As Integer
        Dim TopName As String
        Dim TopAddress As String
        Dim TopPhone As String
        Dim BotName As String
        Dim BotAddress As String
        Dim BotPhone As String
        Dim FullName As String
        Dim mPrintUser As String
        Dim mPrintRunDate As String
        Dim mPrintPageNo As String
        Dim mCompAcCode As String

        'Dim mModvatRAcctCode As String
        'Dim mModvatOAcctCode As String
        'Dim mSTRefundAccountCode As String
        'Dim mSTReceivableAccountCode As String
        'Dim mCSTRefundAccountCode As String

        Dim mTDSCreditAcctCode As String
        Dim mESICreditAcctCode As String
        Dim mSTDSCreditAcctCode As String
        'Dim mSREDAcctCode As String
        'Dim mSRSTAcctCode As String
        'Dim mSRCSTAcctCode As String
        Dim mPDIRAcctCode As String
        Dim mPDIRCrAcctCode As String
        'Dim mModvatRecdAcctCode As String
        'Dim mCESSAccountCode As String
        'Dim mCESSRecdAccountCode As String
        Dim mStockBalCheck As String
        Dim mPurPlanning As String
        Dim mISSTReceivable As String
        'Dim mBEDReturnAccountCode As String
        'Dim mCessOnSrv_SaleAccountCode As String
        'Dim mECTReturnAccountCode As String
        'Dim mSRVTReturnAccountCode As String

        'Dim mServicesTaxAcct As String
        'Dim mServicesTaxCessAcct As String
        'Dim mSRCessAcct As String
        'Dim mSRServicesTaxAcct As String
        'Dim mSRServicesTaxCessAcct As String
        'Dim mSrvTaxCessPurchaseRtn As String
        'Dim mADEPurchaseAcct As String
        'Dim mADERecdPurchaseAcct As String
        'Dim mADEPurchaseRtnAcct As String

        Dim mADDMode As Boolean

        Dim mShortage As String
        Dim mShortage_App As String
        Dim mRejection As String
        Dim mRejection_App As String
        Dim mRateDiffDN As String
        Dim mRateDiffDN_App As String
        Dim mRateDiffCN As String
        Dim mRateDiffCN_App As String

        Dim mShortageWithGST As String
        Dim mRejectionWithGST As String
        Dim mRateDiffDrWithGST As String
        Dim mRateDiffCrWithGST As String

        'Dim mSHCESSAccountCode As String
        'Dim mSHCESSRecdAccountCode As String
        'Dim mSHCessOnSrv_SaleAccountCode As String
        'Dim mSHECTReturnAccountCode As String
        'Dim mServicesTaxSHCessAcct As String
        'Dim mServicesTaxKKCessAcct As String
        'Dim mSRSHCessAcct As String
        'Dim mSRServicesTaxSHCessAcct As String
        'Dim mSrvTaxSHCessPurchaseRtn As String
        'Dim mSHCessPurchaseRtn As String
        Dim mPurPostingType As String
        Dim mInvPostingType As String
        Dim mAutoIssue As String
        Dim mAutoIssueDate As String

        Dim mLoadingApp As String
        Dim mLoadingAppDate As String

        Dim mAutoIssueProd As String
        Dim mAutoIssueProdDate As String

        'Dim mSurOn_Vat_SRAcct As String
        'Dim mSurOn_Vat_PAcct As String

        Dim mInvTableCC As String
        Dim mInvTableFYear As String
        Dim mOnLine As String
        Dim mAttMc As String
        Dim mInvPrePrint As String
        Dim mPOPrePrint As String
        Dim mRGPPrePrint As String
        Dim mInvoiceA4 As String
        Dim mMRRSeries As String
        Dim mDSPSeries As String
        Dim mRGPSeries As String
        Dim mINVSeries As String
        Dim mPURSeries As String
        Dim mGateEntry As String
        Dim mPOInGE As String
        Dim mWeeklySchd As String

        Dim mBOPMaxLevel As String
        Dim mRMMaxLevel As String
        Dim mConsMaxLevel As String
        Dim mMaintMaxLevel As String

        'Dim mBEDReturnAccountCodeInv As String
        'Dim mECTReturnAccountCodeInv As String
        'Dim mSRVTReturnAccountCodeInv As String
        'Dim mSrvTaxCessPurchaseRtnInv As String
        'Dim mADEPurchaseRtnAcctInv As String
        'Dim mSHCessPurchaseRtnInv As String
        'Dim mSrvTaxSHCessPurchaseRtnInv As String

        Dim mHideBatchNo As String
        Dim mHideHeatNo As String
        Dim mAcPRAutoJv As String
        Dim mCheckPORate As String
        Dim mInvoicePrintStyle As String


        Dim mPrintOTInPayslip As String
        Dim mGSTSeparate As String
        Dim mEWayBillAPP As String
        Dim mEInvoice As String
        Dim mTCSApplicable As String
        Dim mShowPacketCol As String
        Dim mShowInnerPacketCol As String
        Dim mShowOuterPacketCol As String
        Dim mDivLocation As String
        Dim mManualBillAdj As String
        Dim mIsPostDc As String
        Dim mAutoGenCode As String
        Dim mIsWareHouse As String
        Dim mEntitleAfterConf As String
        Dim mELFixed As String
        Dim mDoubleSalary As String
        Dim mSalesOrderWiseIndent As String
        Dim mLockInvoice As String
        Dim mCheckMRPSale As String
        Dim mINVFromStock As String
        Dim mMINQtyChk As String
        Dim mSaleScgedule As String
        Dim mCheckBom As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = ""
        xCode = RsCompany.Fields("Company_Code").Value


        mGateEntry = IIf(chkGateEntry.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mPOInGE = IIf(chkPOCheckInGE.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mCheckPORate = IIf(chkCheckPORate.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mMRRSeries = IIf(chkMRR.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mDSPSeries = IIf(chkDespatch.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mRGPSeries = IIf(chkGatepass.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mINVSeries = IIf(chkInvoice.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mPURSeries = IIf(chkPurchase.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mBOPMaxLevel = IIf(chkBOPMaxLevel.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mRMMaxLevel = IIf(chkRMMaxLevel.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mConsMaxLevel = IIf(chkConsMaxLevel.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mMaintMaxLevel = IIf(chkMaintMaxLevel.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mInvTableCC = IIf(chkInvTableCC.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mInvTableFYear = IIf(chkInvTableFYear.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mOnLine = IIf(chkOnLine.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mAttMc = IIf(chkAttMc.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mPurPostingType = IIf(optPurPostingType(0).Checked = True, "B", "I")
        mInvPostingType = IIf(optInvPostingType(0).Checked = True, "B", "I")

        mPOPrePrint = IIf(optPOPrint(0).Checked = True, "Y", "N")

        mInvoicePrintStyle = IIf(optPrintPortrait.Checked = True, "P", "L")

        mInvoiceA4 = IIf(optA4.Checked = True, "Y", "N")

        TopName = IIf(ChkPrintTopCompanyName.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        TopAddress = IIf(chkPrintTopCompanyAddress.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        TopPhone = IIf(chkPrintTopCompanyPhone.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        FullName = IIf(OptPrintCompanyFull_ShortName(0).Checked = True, "F", "S")
        BotName = IIf(ChkPrintBotCompanyName.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        BotAddress = IIf(chkPrintBotCompanyAddress.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        BotPhone = IIf(chkPrintBotCompanyPhone.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mPrintUser = IIf(chkPrintUser.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mPrintRunDate = IIf(ChkPrintRunDate.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mPrintPageNo = IIf(ChkPrintPAgeNo.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mStockBalCheck = IIf(chkStockBal.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mPurPlanning = IIf(chkPurPlanning.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mWeeklySchd = IIf(chkWeeklySchd.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mAutoIssue = IIf(chkAutoIssue.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mShortage = IIf(chkShortage.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mShortage_App = IIf(chkShortage_App.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mRejection = IIf(chkRejection.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mRejection_App = IIf(chkRejection_App.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mRateDiffDN = IIf(chkRateDiffDN.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mRateDiffDN_App = IIf(chkRateDiffDN_App.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mRateDiffCN = IIf(chkRateDiffCN.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mRateDiffCN_App = IIf(chkRateDiffCN_App.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mShortageWithGST = IIf(chkShortageWithGST.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mRejectionWithGST = IIf(chkRejectionWithGST.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mRateDiffDrWithGST = IIf(chkRateDiffDrWithGST.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mRateDiffCrWithGST = IIf(chkRateDiffCrWithGST.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mPrintOTInPayslip = IIf(ChkPrintOTInPayslip.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mGSTSeparate = IIf(ChkGSTSeparate.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mEWayBillAPP = IIf(ChkAWavailable.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mEInvoice = IIf(ChkEInvoiceApp.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mTCSApplicable = IIf(ChkTCS.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mShowInnerPacketCol = IIf(ChkInnerPackCol.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mShowOuterPacketCol = IIf(ChkOuterPackCol.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mShowPacketCol = IIf(ChkPacketCol.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mDivLocation = IIf(ChkDivlocation.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mManualBillAdj = IIf(ChkBilladj.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mIsPostDc = IIf(ChkDCInLedger.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mAutoGenCode = IIf(ChkAutoGenCode.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mIsWareHouse = IIf(ChkWarHouse.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mEntitleAfterConf = IIf(ChkAfterConfirm.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mELFixed = IIf(ChkElFixed.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mDoubleSalary = IIf(ChkDoubleSalary.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mSalesOrderWiseIndent = IIf(ChkSaleOrderIndent.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mLockInvoice = IIf(ChkLockPayterms.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mCheckMRPSale = IIf(ChkMRPSaleOrder.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mINVFromStock = IIf(ChkINVFromStock.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mMINQtyChk = IIf(ChkQtyCheck.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mSaleScgedule = IIf(ChkSaleScheduleReq.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mCheckBom = IIf(ChkBom.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")


        mHideBatchNo = IIf(chkHideBatchNo.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mHideHeatNo = IIf(chkHideHeatNo.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mAcPRAutoJv = IIf(chkAcPRAutoJv.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mAutoIssue = IIf(chkAutoIssue.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        If Trim(txtAutoIssueDate.Text) = "__/__/____" Or Not IsDate(txtAutoIssueDate.Text) Then
            mAutoIssueDate = ""
        Else
            mAutoIssueDate = VB6.Format(txtAutoIssueDate.Text, "dd/MM/yyyy")
        End If

        mAutoIssueProd = IIf(chkAutoProdIssue.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        If Trim(txtAutoProdIssueDate.Text) = "__/__/____" Or Not IsDate(txtAutoProdIssueDate.Text) Then
            mAutoIssueProdDate = ""
        Else
            mAutoIssueProdDate = VB6.Format(txtAutoProdIssueDate.Text, "dd/MM/yyyy")
        End If


        mLoadingApp = IIf(optLoadingSlip(0).Checked = True, "Y", "N")
        If Trim(txtLoadingAppDate.Text) = "__/__/____" Or Not IsDate(txtLoadingAppDate.Text) Then
            mLoadingAppDate = ""
        Else
            mLoadingAppDate = VB6.Format(txtLoadingAppDate.Text, "dd/MM/yyyy")
        End If

        If mADDMode = True Then
            SqlStr = "INSERT INTO FIN_PRINT_MST ( " & vbCrLf _
                    & " COMPANY_CODE, PRINTTOPCOMPANYNAME, " & vbCrLf _
                    & " PRINTTOPCOMPANYADDRESS, PRINTTOPCOMPANYPHONE, " & vbCrLf _
                    & " PRINTBOTCOMPANYNAME, PRINTBOTCOMPANYADDRESS, " & vbCrLf _
                    & " PRINTBOTCOMPANYPHONE, PRINTCOMPANYFULL_SHORTNAME, " & vbCrLf _
                    & " PRINTUSER, PRINTRUNDATE, PRINTPAGENO, " & vbCrLf _
                    & " REPORTMARGINTOP, REPORTMARGINBOT, REPORTMARGINLEFT,REPORTMARGINRIGHT, " & vbCrLf _
                    & " TDSCIRCLE, TDSACNO, TDSAUTHORIZED, TDSAUTHORIZED_FNAME, " & vbCrLf _
                    & " TDSAUTHORIZED_DESIG, " & vbCrLf _
                    & " PDIR_AMOUNT,PDIR_ACCOUNT,PDIR_CreditAcct," & vbCrLf _
                    & " Shortage_DN, Shortage_DN_APP," & vbCrLf _
                    & " Rejection_DN, Rejection_DN_APP," & vbCrLf _
                    & " RATE_Diff_DN, RATE_Diff_DN_APP," & vbCrLf _
                    & " RATE_Diff_CN, RATE_Diff_CN_APP," & vbCrLf _
                    & " CREDITBANK, CREDITBANK_ADD, AD_CODE, FURTHER_BANK," & vbCrLf _
                    & " PURCHASE_POSTINGTYPE, SALES_POSTINGTYPE,UPDATE_FROM,AUTO_ISSUE,AUTO_ISSUE_DATE, " & vbCrLf _
                    & " AUTO_ISSUE_PROD,AUTO_ISSUE_PROD_DATE, "

            SqlStr = SqlStr & vbCrLf _
                    & " INV_TAB_CC, INV_TAB_FY, COMP_ONLINE, ATTN_MC," & vbCrLf _
                    & " INVOICE_PREPRINT,INVOICE_A4,INVOICE_DIGIT, IS_STRECEIVABLE, STRECEIVABLEACCOUNT," & vbCrLf _
                    & " SEPARATE_MRR_SERIES,SEPARATE_DSP_SERIES," & vbCrLf _
                    & " SEPARATE_RGP_SERIES,SEPARATE_INV_SERIES, SEPARATE_PUR_SERIES," & vbCrLf _
                    & " MRR_AGT_GE,PO_IN_GE,PUR_PLANNING,WEEKLY_SCHD,RGP_PREPRINT," & vbCrLf _
                    & " BOP_MAX_LEVEL, RM_MAX_LEVEL, CONS_MAX_LEVEL, MAINT_MAX_LEVEL, " & vbCrLf _
                    & " QC_ALLOW_DAYS, CHECK_MAX_INV_GE,GEN_RJ_DESPATCH," & vbCrLf _
                    & " INV_GENERATE_24_HOURS, INV_GENERATE_FROM_TM, INV_GENERATE_TO_TM,LOADIND_APP,LOADING_APP_DATE, " & vbCrLf _
                    & " PROD_PALN_LOCK, PROD_PALN_LOCK_DAY,PO_PREPRINT, CHECK_BOP_STOCK, CHECK_FG_STOCK, PO_LOCK, SO_LOCK, " & vbCrLf _
                    & " PO_PRINT_APP_REQ,BILLAMOUNT_LIMIT, MAX_PO_ITEMS, MAX_PENDING_INDENT,CREDIT_LIMIT_APP) VALUES ( "


            SqlStr = SqlStr & vbCrLf _
                    & " " & xCode & ", '" & Trim(TopName) & "', " & vbCrLf _
                    & " '" & Trim(TopAddress) & "', '" & Trim(TopPhone) & "', " & vbCrLf _
                    & " '" & Trim(BotName) & "', '" & Trim(BotAddress) & "', " & vbCrLf _
                    & " '" & Trim(BotPhone) & "', '" & Trim(FullName) & "', " & vbCrLf _
                    & " '" & Trim(mPrintUser) & "', '" & Trim(mPrintRunDate) & "', '" & Trim(mPrintPageNo) & "', " & vbCrLf _
                    & " " & Val(txtMargin(0).Text) & ", " & Val(txtMargin(1).Text) & ", " & Val(txtMargin(2).Text) & ", " & vbCrLf _
                    & " " & Val(txtMargin(3).Text) & ", '" & MainClass.AllowSingleQuote(Trim(txtTDSCircle.Text)) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(Trim(txtTDSAcNo.Text)) & "', '" & MainClass.AllowSingleQuote(Trim(txtAuthorized.Text)) & "', '" & MainClass.AllowSingleQuote(Trim(txtAuthorizedFName.Text)) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(Trim(txtDesignation.Text)) & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(Trim(mTDSCreditAcctCode)) & "','" & MainClass.AllowSingleQuote(Trim(mESICreditAcctCode)) & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(Trim(mSTDSCreditAcctCode)) & "'," & vbCrLf _
                    & " " & Val(txtPDIRAmount.Text) & ",'" & MainClass.AllowSingleQuote(Trim(mPDIRAcctCode)) & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(Trim(mPDIRCrAcctCode)) & "'," & vbCrLf _
                    & " " & Val(txtMRRExcessPer.Text) & ", '" & mStockBalCheck & "',"


            SqlStr = SqlStr & vbCrLf _
                    & " '" & mShortage & "', '" & mShortage_App & "', " & vbCrLf _
                    & " '" & mRejection & "', '" & mRejection_App & "', " & vbCrLf _
                    & " '" & mRateDiffDN & "', '" & mRateDiffDN_App & "', " & vbCrLf _
                    & " '" & mRateDiffCN & "', '" & mRateDiffCN_App & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtCreditBank.Text) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtCreditBankAddress.Text) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtADCode.Text) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtFurtherBank.Text) & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(Trim(mPurPostingType)) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(Trim(mInvPostingType)) & "','H', " & vbCrLf _
                    & " '" & mAutoIssue & "', TO_DATE('" & VB6.Format(txtAutoIssueDate.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY')),  " & vbCrLf _
                    & " '" & mAutoIssueProd & "', TO_DATE('" & VB6.Format(mAutoIssueProdDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'), "

            SqlStr = SqlStr & vbCrLf _
                    & " '" & mInvTableCC & "','" & mInvTableFYear & "','" & mOnLine & "','" & mAttMc & "', " & vbCrLf _
                    & " '" & mInvPrePrint & "','" & mInvoiceA4 & "'," & vbCrLf _
                    & " '" & mMRRSeries & "', '" & mDSPSeries & "', " & vbCrLf _
                    & " '" & mRGPSeries & "', '" & mINVSeries & "', '" & mPURSeries & "'," & vbCrLf _
                    & " '" & mGateEntry & "','" & mPOInGE & "','" & mPurPlanning & "','" & mWeeklySchd & "','" & mRGPPrePrint & "'," & vbCrLf _
                    & " '" & mBOPMaxLevel & "', '" & mRMMaxLevel & "', '" & mConsMaxLevel & "', '" & mMaintMaxLevel & "'" & vbCrLf _
                    & " " & Val(txtQCDays.Text) & ", '" & IIf(chkMaxInvInGate.Checked = True, "Y", "N") & "', '" & IIf(chkRJDespatchNote.Checked = True, "Y", "N") & "'," & vbCrLf _
                    & " '" & IIf(optInvGen(0).Checked = True, "Y", "N") & "',TO_DATE('" & txtInvTmFrom.Text & "','HH24:MI'),TO_DATE('" & txtInvTmTo.Text & "','HH24:MI'),'" & mLoadingApp & "',TO_DATE('" & VB6.Format(mLoadingAppDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " '" & IIf(optProductionPlanLocking(0).Checked = True, "Y", "N") & "', " & Val(txtPlanLockDays.Text) & ",'" & mPOPrePrint & "'," & vbCrLf _
                    & " '" & IIf(chkBOPCheck.Checked = True, "Y", "N") & "',  '" & IIf(chkFGCheck.Checked = True, "Y", "N") & "','" & IIf(optPOLocking(0).Checked = True, "Y", "N") & "','" & IIf(optSOLocking(0).Checked = True, "Y", "N") & "'," & vbCrLf _
                    & " '" & IIf(chkPOPrintApproval.Checked = True, "Y", "N") & "', " & Val(txtMaxBillAmount.Text) & ", " & Val(txtMaxPOItems.Text) & ", " & Val(txtPendingIndentNo.Text) & ",'" & IIf(chkCreditLimit.Checked = True, "Y", "N") & "')"

        Else
            SqlStr = "UPDATE  FIN_PRINT_MST SET PrintTopCompanyName='" & Trim(TopName) & "', " & vbCrLf _
                    & " MAX_PO_ITEMS=" & Val(txtMaxPOItems.Text) & ", MAX_PENDING_INDENT=" & Val(txtPendingIndentNo.Text) & "," & vbCrLf _
                    & " PrintTopCompanyAddress='" & Trim(TopAddress) & "', " & vbCrLf _
                    & " PrintTopCompanyPhone='" & Trim(TopPhone) & "',  " & vbCrLf _
                    & " PrintCompanyFull_ShortName='" & Trim(FullName) & "'," & vbCrLf _
                    & " PrintBotCompanyName='" & Trim(BotName) & "',  " & vbCrLf _
                    & " PrintBotCompanyAddress='" & Trim(BotAddress) & "', " & vbCrLf _
                    & " PrintBotCompanyPhone='" & Trim(BotPhone) & "',  " & vbCrLf _
                    & " PrintUser='" & Trim(mPrintUser) & "'," & vbCrLf _
                    & " PrintRunDate='" & Trim(mPrintRunDate) & "',  " & vbCrLf _
                    & " PrintPageNo='" & Trim(mPrintPageNo) & "'," & vbCrLf _
                    & " PUR_PLANNING='" & mPurPlanning & "', WEEKLY_SCHD='" & mWeeklySchd & "',"

            SqlStr = SqlStr & vbCrLf _
                    & " ReportMarginTop=" & Val(txtMargin(0).Text) & ", " _
                    & " ReportMarginBot=" & Val(txtMargin(1).Text) & ", " _
                    & " ReportMarginLeft=" & Val(txtMargin(2).Text) & ", " _
                    & " ReportMarginRight=" & Val(txtMargin(3).Text) & ", "

            SqlStr = SqlStr & vbCrLf _
                    & " TDSCIRCLE = '" & MainClass.AllowSingleQuote(Trim(txtTDSCircle.Text)) & "'," & vbCrLf _
                    & " TDSACNO = '" & MainClass.AllowSingleQuote(Trim(txtTDSAcNo.Text)) & "', " & vbCrLf _
                    & " TDSAUTHORIZED = '" & MainClass.AllowSingleQuote(Trim(txtAuthorized.Text)) & "', " & vbCrLf _
                    & " TDSAUTHORIZED_FNAME = '" & MainClass.AllowSingleQuote(Trim(txtAuthorizedFName.Text)) & "', " & vbCrLf _
                    & " TDSAUTHORIZED_DESIG = '" & MainClass.AllowSingleQuote(Trim(txtDesignation.Text)) & "', " & vbCrLf _
                    & " TDSCREDITACCOUNT = '" & MainClass.AllowSingleQuote(Trim(mTDSCreditAcctCode)) & "'," & vbCrLf _
                    & " ESICREDITACCOUNT = '" & MainClass.AllowSingleQuote(Trim(mESICreditAcctCode)) & "'," & vbCrLf _
                    & " STDSCREDITACCOUNT = '" & MainClass.AllowSingleQuote(Trim(mSTDSCreditAcctCode)) & "',"

            SqlStr = SqlStr & vbCrLf _
                    & " PDIR_CreditAcct='" & MainClass.AllowSingleQuote(Trim(mPDIRCrAcctCode)) & "'," & vbCrLf _
                    & " PDIR_ACCOUNT='" & MainClass.AllowSingleQuote(Trim(mPDIRAcctCode)) & "'," & vbCrLf _
                    & " PDIR_AMOUNT = " & Val(txtPDIRAmount.Text) & ", "


            SqlStr = SqlStr & vbCrLf _
                    & " GREXCESSPER=" & Val(txtMRRExcessPer.Text) & "," & vbCrLf _
                    & " STOCKBALCHECK = '" & mStockBalCheck & "', INVOICE_DIGIT=" & Val(txtInvoiceDigit.Text) & ","

            SqlStr = SqlStr & vbCrLf _
                    & " Shortage_DN='" & mShortage & "'," & vbCrLf _
                    & " Shortage_DN_APP='" & mShortage_App & "'," & vbCrLf _
                    & " Rejection_DN='" & mRejection & "'," & vbCrLf _
                    & " Rejection_DN_APP='" & mRejection_App & "'," & vbCrLf _
                    & " RATE_Diff_DN='" & mRateDiffDN & "'," & vbCrLf _
                    & " RATE_Diff_DN_APP='" & mRateDiffDN_App & "'," & vbCrLf _
                    & " RATE_Diff_CN='" & mRateDiffCN & "'," & vbCrLf _
                    & " RATE_Diff_CN_APP='" & mRateDiffCN_App & "'," & vbCrLf _
                    & " Shortage_With_GST='" & mShortageWithGST & "'," & vbCrLf _
                    & " Rejection_With_GST='" & mRejectionWithGST & "'," & vbCrLf _
                    & " RateDiff_DR_With_GST='" & mRateDiffDrWithGST & "'," & vbCrLf _
                    & " RateDiff_CR_With_GST='" & mRateDiffCrWithGST & "',"

            SqlStr = SqlStr & vbCrLf _
                    & " CREDITBANK='" & MainClass.AllowSingleQuote(Trim(txtCreditBank.Text)) & "'," & vbCrLf _
                    & " CREDITBANK_ADD='" & MainClass.AllowSingleQuote(Trim(txtCreditBankAddress.Text)) & "'," & vbCrLf _
                    & " AD_CODE='" & MainClass.AllowSingleQuote(Trim(txtADCode.Text)) & "'," & vbCrLf _
                    & " FURTHER_BANK='" & MainClass.AllowSingleQuote(Trim(txtFurtherBank.Text)) & "',"

            SqlStr = SqlStr & vbCrLf _
                    & " PURCHASE_POSTINGTYPE='" & MainClass.AllowSingleQuote(Trim(mPurPostingType)) & "'," & vbCrLf _
                    & " SALES_POSTINGTYPE='" & MainClass.AllowSingleQuote(Trim(mInvPostingType)) & "'," & vbCrLf _
                    & " UPDATE_FROM='H'," & vbCrLf _
                    & " AUTO_ISSUE='" & mAutoIssue & "', AUTO_ISSUE_DATE=TO_DATE('" & VB6.Format(mAutoIssueDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')," & vbCrLf _
                    & " AUTO_ISSUE_PROD='" & mAutoIssueProd & "', AUTO_ISSUE_PROD_DATE=TO_DATE('" & VB6.Format(mAutoIssueProdDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " INV_TAB_CC = '" & mInvTableCC & "'," & vbCrLf _
                    & " INV_TAB_FY = '" & mInvTableFYear & "'," & vbCrLf _
                    & " COMP_ONLINE = '" & mOnLine & "'," & vbCrLf _
                    & " ATTN_MC = '" & mAttMc & "', PROD_PALN_LOCK = '" & IIf(optProductionPlanLocking(0).Checked = True, "Y", "N") & "', " & vbCrLf _
                    & " PROD_PALN_LOCK_DAY= " & Val(txtPlanLockDays.Text) & "," & vbCrLf _
                    & " INVOICE_PREPRINT = '" & mInvPrePrint & "', RGP_PREPRINT = '" & mRGPPrePrint & "'," & vbCrLf _
                    & " INVOICE_A4 = '" & mInvoiceA4 & "'," & vbCrLf _
                    & " IS_STRECEIVABLE='" & mISSTReceivable & "', MRR_AGT_GE='" & mGateEntry & "',PO_IN_GE='" & mPOInGE & "'," & vbCrLf _
                    & " SEPARATE_MRR_SERIES='" & mMRRSeries & "', SEPARATE_DSP_SERIES='" & mDSPSeries & "', " & vbCrLf _
                    & " SEPARATE_RGP_SERIES='" & mRGPSeries & "', SEPARATE_INV_SERIES='" & mINVSeries & "', " & vbCrLf _
                    & " SEPARATE_PUR_SERIES='" & mPURSeries & "',"

            SqlStr = SqlStr & vbCrLf _
                    & " BOP_MAX_LEVEL='" & mBOPMaxLevel & "', RM_MAX_LEVEL='" & mRMMaxLevel & "', " & vbCrLf _
                    & " CONS_MAX_LEVEL='" & mConsMaxLevel & "', MAINT_MAX_LEVEL='" & mMaintMaxLevel & "', " & vbCrLf _
                    & " QC_ALLOW_DAYS=" & Val(txtQCDays.Text) & ", CHECK_MAX_INV_GE='" & IIf(chkMaxInvInGate.Checked = True, "Y", "N") & "'," & vbCrLf _
                    & " GEN_RJ_DESPATCH='" & IIf(chkRJDespatchNote.Checked = True, "Y", "N") & "',"

            SqlStr = SqlStr & vbCrLf _
                    & " INV_GENERATE_24_HOURS='" & IIf(optInvGen(0).Checked = True, "Y", "N") & "'," & vbCrLf _
                    & " INV_GENERATE_FROM_TM=TO_DATE('" & txtInvTmFrom.Text & "','HH24:MI'), " & vbCrLf _
                    & " INV_GENERATE_TO_TM=TO_DATE('" & txtInvTmTo.Text & "','HH24:MI'), " & vbCrLf _
                    & " LOADIND_APP= '" & mLoadingApp & "', " & vbCrLf _
                    & " LOADING_APP_DATE=TO_DATE('" & VB6.Format(mLoadingAppDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " PO_PREPRINT='" & mPOPrePrint & "', " & vbCrLf _
                    & " CHECK_BOP_STOCK='" & IIf(chkBOPCheck.Checked = True, "Y", "N") & "', " & vbCrLf _
                    & " CHECK_FG_STOCK='" & IIf(chkFGCheck.Checked = True, "Y", "N") & "', " & vbCrLf _
                    & " PO_LOCK='" & IIf(optPOLocking(0).Checked = True, "Y", "N") & "', " & vbCrLf _
                    & " SO_LOCK='" & IIf(optSOLocking(0).Checked = True, "Y", "N") & "', INVOICE_PRINT_STYLE='" & mInvoicePrintStyle & "'," & vbCrLf _
                    & " PO_PRINT_APP_REQ = '" & IIf(chkPOPrintApproval.Checked = True, "Y", "N") & "', BILLAMOUNT_LIMIT=" & Val(txtMaxBillAmount.Text) & ",CREDIT_LIMIT_APP='" & IIf(chkCreditLimit.Checked = True, "Y", "N") & "',"

            SqlStr = SqlStr & vbCrLf _
                    & " PRINTOTINPAYSLIP='" & mPrintOTInPayslip & "'," & vbCrLf _
                    & " GST_SEPARATE='" & mGSTSeparate & "'," & vbCrLf _
                    & " EWAYBILLAPP='" & mEWayBillAPP & "'," & vbCrLf _
                    & " E_INVOICE_APP='" & mEInvoice & "'," & vbCrLf _
                    & " TCS_APPLICABLE='" & mTCSApplicable & "'," & vbCrLf _
                    & " AC_PR_AUTO_JV='" & mAcPRAutoJv & "'," & vbCrLf _
                    & " CHECK_PO_RATE='" & mCheckPORate & "'," & vbCrLf _
                    & " PACKETS_COL_SHOW='" & mShowPacketCol & "'," & vbCrLf _
                    & " INNER_PACK_COL_SHOW='" & mShowInnerPacketCol & "'," & vbCrLf _
                    & " OUTER_PACK_COL_SHOW='" & mShowOuterPacketCol & "'," & vbCrLf _
                    & " DIV_AS_LOCATION='" & mDivLocation & "'," & vbCrLf _
                    & " MANNUAL_BILL_ADJUST='" & mManualBillAdj & "'," & vbCrLf _
                    & " IS_POST_DC_IN_LEDGER='" & mIsPostDc & "'," & vbCrLf _
                    & " AUTO_GEN_CODE='" & mAutoGenCode & "'," & vbCrLf _
                    & " IS_WAREHOUSE='" & mIsWareHouse & "'," & vbCrLf _
                    & " ENTITLE_AFTER_CONFIRM='" & mEntitleAfterConf & "'," & vbCrLf _
                    & " IS_EL_FIXED='" & mELFixed & "'," & vbCrLf _
                    & " DOUBLE_SALARY_OPTION='" & mDoubleSalary & "'," & vbCrLf _
                    & " SALEORDER_WISE_INDENT='" & mSalesOrderWiseIndent & "'," & vbCrLf _
                    & " LOCK_INVOICE_PAYTERMS='" & mLockInvoice & "'," & vbCrLf _
                    & " CHECK_MRP_SALEORDER='" & mCheckMRPSale & "'," & vbCrLf _
                    & " INV_TAKEN_FROM_STOCK='" & mINVFromStock & "'," & vbCrLf _
                    & " MINIMUN_QTY_CHECK_DESP='" & mMINQtyChk & "'," & vbCrLf _
                    & " SALE_SCHEDULE_APP_REQUIRED='" & mSaleScgedule & "'," & vbCrLf _
                    & " CHECK_BOM_SO='" & mCheckBom & "'"

            SqlStr = SqlStr & vbCrLf & " WHERE Company_Code=" & xCode & ""

        End If

        PubDBCn.Execute(SqlStr)

        SqlStr = "UPDATE  GEN_COMPANY_MST SET PAN_NO='" & Trim(txtPANNo.Text) & "' " & vbCrLf
        SqlStr = SqlStr & " WHERE Company_Code=" & xCode & ""
        PubDBCn.Execute(SqlStr)


        If MainClass.ValidateWithMasterTable(txtCompAc.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo,, "INTER_UNIT='Y'") = True Then
            mCompAcCode = MasterNo
        Else
            mCompAcCode = "-1"
        End If

        SqlStr = "UPDATE  FIN_PRINT_MST SET BATCHNO_HIDE='" & mHideBatchNo & "',HEATNO_HIDE='" & mHideHeatNo & "',COMP_AC_CODE='" & mCompAcCode & "',AC_PR_AUTO_JV='" & mAcPRAutoJv & "', CHECK_PO_RATE = '" & mCheckPORate & "'" & vbCrLf
        SqlStr = SqlStr & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        PubDBCn.Execute(SqlStr)

        If UpdateCarryOption() = False Then GoTo err_Renamed

        If UpdateCatgeoryMapping() = False Then GoTo err_Renamed

        PubDBCn.CommitTrans()
        Update1 = True
        RsCompany.Requery() ''.Refresh		

        Exit Function
err_Renamed:
        Call ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        Update1 = False
        PubDBCn.RollbackTrans() ''		
        RsCompany.Requery() ''.Refresh		


    End Function
    Private Sub ChkPrintTopCompanyName_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkPrintTopCompanyName.CheckStateChanged

        If ChkPrintTopCompanyName.CheckState = 1 Then

        Else

            OptPrintCompanyFull_ShortName(0).Checked = False
            OptPrintCompanyFull_ShortName(1).Checked = False
        End If
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode, False)
    End Sub
    Private Sub frmSysPref_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Close()
        Me.Dispose()
        'Me = Nothing		
    End Sub
    Private Sub optInvGen_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optInvGen.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optInvGen.GetIndex(eventSender)

            MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
            If optInvGen(0).Checked = True Then
                txtInvTmFrom.Enabled = False
                txtInvTmTo.Enabled = False
            Else
                txtInvTmFrom.Enabled = True
                txtInvTmTo.Enabled = True
            End If
        End If
    End Sub

    Private Sub optInvGen_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles optInvGen.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim Index As Short = optInvGen.GetIndex(eventSender)

        If optInvGen(1).Checked = True Then
            If txtInvTmFrom.Text <> "" Then
                txtInvTmFrom.Text = Replace(txtInvTmFrom.Text, ":", "")
                txtInvTmFrom.Text = VB6.Format(txtInvTmFrom.Text, "00:00")
            End If

            If txtInvTmTo.Text <> "" Then
                txtInvTmTo.Text = Replace(txtInvTmTo.Text, ":", "")
                txtInvTmTo.Text = VB6.Format(txtInvTmTo.Text, "00:00")
            End If
        End If

        eventArgs.Cancel = Cancel
    End Sub
    Private Sub optInvPostingType_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optInvPostingType.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optInvPostingType.GetIndex(eventSender)

            MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub optLoadingSlip_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optLoadingSlip.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optLoadingSlip.GetIndex(eventSender)

            MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub optPOLocking_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optPOLocking.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optPOLocking.GetIndex(eventSender)

            MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub optPOPrint_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optPOPrint.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optPOPrint.GetIndex(eventSender)

            MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub OptPrintCompanyFull_ShortName_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptPrintCompanyFull_ShortName.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptPrintCompanyFull_ShortName.GetIndex(eventSender)

            MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        End If
    End Sub
    Private Function FieldVerification() As Boolean
        On Error GoTo ERR1
        FieldVerification = True

        If Val(txtMargin(0).Text) > 1 Or Val(txtMargin(1).Text) > 1 Or Val(txtMargin(2).Text) > 1 Or Val(txtMargin(3).Text) > 1 Then
            MsgInformation("Report Margin Can Not be More Than 1 Inch")
            txtMargin(0).Focus()
            FieldVerification = False
            Exit Function
        End If

        If chkAutoIssue.CheckState = System.Windows.Forms.CheckState.Checked Then
            If IsDate(txtAutoIssueDate.Text) = False Then
                MsgInformation("Auto Issue Date cann't be blank.")
                txtAutoIssueDate.Focus()
                FieldVerification = False
                Exit Function
            End If
        End If

        If chkAutoProdIssue.CheckState = System.Windows.Forms.CheckState.Checked Then
            If IsDate(txtAutoProdIssueDate.Text) = False Then
                MsgInformation("Auto Production Issue Date cann't be blank.")
                txtAutoProdIssueDate.Focus()
                FieldVerification = False
                Exit Function
            End If
        End If

        If optProductionPlanLocking(0).Checked And Val(txtPlanLockDays.Text) <= 0 Then
            MsgInformation("Plan Lock Day is must.")
            txtPlanLockDays.Focus()
            FieldVerification = False
            Exit Function
        End If

        If optLoadingSlip(1).Checked = True Then
            txtLoadingAppDate.Text = "__/__/____"
        Else
            If txtLoadingAppDate.Text = "" Or txtLoadingAppDate.Text = "__/__/____" Then
                MsgInformation("Loading Applicable Date cann't be blank.")
                txtLoadingAppDate.Focus()
                FieldVerification = False
                Exit Function
            End If

            If IsDate(txtLoadingAppDate.Text) = False Then
                MsgInformation("Loading Applicable Date is invalid.")
                txtLoadingAppDate.Focus()
                FieldVerification = False
                Exit Function
            End If

        End If

        If optInvGen(0).Checked = True Then
            txtInvTmFrom.Text = ""
            txtInvTmTo.Text = ""
        Else
            If txtInvTmFrom.Text = "" Then
                MsgInformation("Invoice Generation FROM TIME cann't be blank.")
                txtInvTmFrom.Focus()
                FieldVerification = False
                Exit Function
            Else
                txtInvTmFrom.Text = Replace(txtInvTmFrom.Text, ":", "")
                txtInvTmFrom.Text = VB6.Format(txtInvTmFrom.Text, "00:00")
                If IsDate(txtInvTmFrom.Text) = False Then
                    MsgInformation("Invalid FROM TIME.")
                    txtInvTmFrom.Focus()
                    FieldVerification = False
                    Exit Function
                End If
            End If

            If txtInvTmTo.Text = "" Then
                MsgInformation("Invoice Generation TO TIME cann't be blank.")
                txtInvTmTo.Focus()
                FieldVerification = False
                Exit Function
            Else
                txtInvTmTo.Text = Replace(txtInvTmTo.Text, ":", "")
                txtInvTmTo.Text = VB6.Format(txtInvTmTo.Text, "00:00")
                If IsDate(txtInvTmTo.Text) = False Then
                    MsgInformation("Invalid TO TIME.")
                    txtInvTmTo.Focus()
                    FieldVerification = False
                    Exit Function
                End If
            End If
        End If

        If SprdMain.MaxRows > 1 Then
        End If

        Exit Function
ERR1:
        '    Resume		
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Sub optProductionPlanLocking_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optProductionPlanLocking.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optProductionPlanLocking.GetIndex(eventSender)

            MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub optPurPostingType_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optPurPostingType.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optPurPostingType.GetIndex(eventSender)

            MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub optSOLocking_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optSOLocking.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optSOLocking.GetIndex(eventSender)

            MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        On Error GoTo ERR1
        Select Case eventArgs.Col
            Case 0
                If eventArgs.Row > 0 And SprdMain.Enabled = True Then
                    MainClass.DeleteSprdRow(SprdMain, eventArgs.Row, ColFromAccountName)
                    MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
                End If
            Case ColFromAccountName, ColToAccountName
                If eventArgs.Row = 0 Then NameSearch(eventArgs.Col, (SprdMain.ActiveRow))
        End Select
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub NameSearch(ByRef Col As Integer, ByRef Row As Integer)

        On Error GoTo ERR1
        Dim SqlStr As String = "" 
        Dim mString As String = ""
        Dim mTableName As String
        Dim mFieldName1 As String
        Dim mFieldName2 As String

        SprdMain.Row = Row
        SprdMain.Col = Col
        mString = SprdMain.Text
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND STATUS='O'"

        mTableName = "FIN_SUPP_CUST_MST"
        mFieldName1 = "SUPP_CUST_NAME"
        mFieldName2 = "SUPP_CUST_CODE"

        MainClass.SearchGridMaster(mString, mTableName, mFieldName1, mFieldName2, , , SqlStr)

        If AcName <> "" Then
            SprdMain.Row = SprdMain.ActiveRow
            SprdMain.Col = Col
            SprdMain.Text = AcName
        End If

        SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(Col, SprdMain.ActiveRow, Col, SprdMain.ActiveRow, False))

        SprdMain.Refresh()

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdMain_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdMain.KeyDownEvent

        On Error GoTo ERR1

        If SprdMain.ActiveRow <= 0 Then Exit Sub
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)

        Select Case SprdMain.ActiveCol
            Case ColFromAccountName, ColToAccountName
                If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 Then NameSearch((SprdMain.ActiveCol), (SprdMain.ActiveRow))

                If eventArgs.KeyCode = System.Windows.Forms.Keys.Return Or eventArgs.KeyCode = System.Windows.Forms.Keys.Tab Then
                    If SprdMain.MaxRows = SprdMain.ActiveRow Then
                        MainClass.AddBlankSprdRow(SprdMain, ColToAccountName, ConRowHeight)
                        'FormatSprdMain -1		
                    End If
                End If
        End Select
        eventArgs.KeyCode = 9999
        Exit Sub
ERR1:
        ErrorMsg(Err.Description)
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ERR1
        Dim pAccountName As String
        Dim mAccountCode As String

        If eventArgs.NewRow = -1 Then Exit Sub

        Select Case eventSender.Col
            Case ColFromAccountName
                SprdMain.Col = ColFromAccountName
                SprdMain.Row = eventSender.Row
                pAccountName = Trim(SprdMain.Text)
                If CheckAccountName(pAccountName, eventSender.Col, eventSender.Row) = False Then
                    eventArgs.cancel = True
                    Exit Sub
                End If

            Case ColToAccountName
                SprdMain.Col = ColToAccountName
                SprdMain.Row = eventSender.Row
                pAccountName = Trim(SprdMain.Text)
                If CheckAccountName(pAccountName, eventSender.Col, eventSender.Row) = False Then
                    eventArgs.cancel = True
                    Exit Sub
                End If
        End Select

        SprdMain.Row = eventSender.Row
        SprdMain.Col = ColFromAccountName
        pAccountName = Trim(UCase(SprdMain.Text))
        If pAccountName = "" Then Exit Sub

        SprdMain.Col = ColToAccountName
        If pAccountName = Trim(UCase(SprdMain.Text)) Then
            MainClass.SetFocusToCell(SprdMain, eventSender.Row, ColToAccountName, "Same Account not allowed.")
            Exit Sub
        End If

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub


    Private Function CheckAccountName(ByRef pAccountName As String, ByRef col2 As Integer, ByRef Row2 As Integer) As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String = "" 
        Dim RS As ADODB.Recordset = Nothing '' ADODB.Recordset		


        If pAccountName = "" Then
            CheckAccountName = True
            Exit Function
        End If

        CheckAccountName = False
        SqlStr = " SELECT SUPP_CUST_CODE FROM FIN_SUPP_CUST_MST WHERE" & vbCrLf _
            & " SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(Trim(pAccountName)) & "'"


        If ADDMode = True Then
            Sqlstr = Sqlstr & vbCrLf & " AND STATUS='O' "
        End If

        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = True Then
            MainClass.SetFocusToCell(SprdMain, Row2, col2, "Invalid Account.")
            Exit Function
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        CheckAccountName = True
        RS.Close()
        RS = Nothing

        Exit Function
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        CheckAccountName = False
        RS.Close()
        RS = Nothing
    End Function

    Private Sub SprdMain_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles SprdMain.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If SprdMain.ActiveCol = ColToAccountName Then
            Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(SprdMain.ActiveCol, SprdMain.ActiveRow, ColToAccountName, SprdMain.ActiveCol + 1, False))
        End If
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtADCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtADCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs)
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtADCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtADEPurchase_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtAuthorized_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAuthorized.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtAuthorized_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAuthorized.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtAuthorized.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtAuthorizedFName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAuthorizedFName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtAuthorizedFName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAuthorizedFName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtAuthorizedFName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtAutoIssueDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAutoIssueDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAutoProdIssueDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAutoProdIssueDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtCreditBank_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCreditBank_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs)
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCreditBank.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCreditBankAddress_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCreditBankAddress_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs)
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCreditBankAddress.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtDesignation_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDesignation.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtDesignation_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDesignation.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDesignation.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtESI_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtESI.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtESI_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtESI.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtESI.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtESI_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtESI.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccount(txtESI)
    End Sub

    Private Sub txtESI_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtESI.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        If Trim(txtESI.Text) = "" Then GoTo EventExitSub


        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtFurtherBank_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtFurtherBank_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs)
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtFurtherBank.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtInvTmFrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInvTmFrom.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtInvTmFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtInvTmFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If optInvGen(1).Checked = True Then
            If txtInvTmFrom.Text <> "" Then
                txtInvTmFrom.Text = Replace(txtInvTmFrom.Text, ":", "")
                txtInvTmFrom.Text = VB6.Format(txtInvTmFrom.Text, "00:00")
                If IsDate(txtInvTmFrom.Text) = False Then
                    MsgInformation("Invalid FROM TIME.")
                    Cancel = True
                    GoTo EventExitSub
                End If
            End If
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtInvTmTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInvTmTo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtInvTmTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtInvTmTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If optInvGen(1).Checked = True Then
            If txtInvTmTo.Text <> "" Then
                txtInvTmTo.Text = Replace(txtInvTmTo.Text, ":", "")
                txtInvTmTo.Text = VB6.Format(txtInvTmTo.Text, "00:00")
                If IsDate(txtInvTmTo.Text) = False Then
                    MsgInformation("Invalid TO TIME.")
                    Cancel = True
                    GoTo EventExitSub
                End If
            End If
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtLoadingAppDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLoadingAppDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMargin_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMargin.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        Dim Index As Short = txtMargin.GetIndex(eventSender)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtMaxBillAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMaxBillAmount.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMaxBillAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMaxBillAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtMaxPOItems_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMaxPOItems.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMaxPOItems_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMaxPOItems.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtMRRExcessPer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMRRExcessPer.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMRRExcessPer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMRRExcessPer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtInvoiceDigit_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInvoiceDigit.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtInvoiceDigit_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtInvoiceDigit.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtPANNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPANNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtPANNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPANNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPANNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtMargin_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMargin.TextChanged
        Dim Index As Short = txtMargin.GetIndex(eventSender)

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPDIRAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPDIRAccount.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtPDIRAccount_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPDIRAccount.DoubleClick
        Call SearchPDIRAccount(txtPDIRAccount)
    End Sub
    Private Sub txtPDIRAccount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPDIRAccount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPDIRAccount.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtPDIRAccount_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPDIRAccount.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchPDIRAccount(txtPDIRAccount)
    End Sub
    Private Sub txtPDIRAccount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPDIRAccount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        If Trim(txtPDIRAccount.Text) = "" Then GoTo EventExitSub
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPDIRAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPDIRAmount.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPDIRAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPDIRAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPDIRCrAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPDIRCrAccount.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPDIRCrAccount_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPDIRCrAccount.DoubleClick
        Call SearchAccount(txtPDIRCrAccount)
    End Sub


    Private Sub txtPDIRCrAccount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPDIRCrAccount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPDIRCrAccount.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPDIRCrAccount_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPDIRCrAccount.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccount(txtPDIRCrAccount)
    End Sub

    Private Sub txtPDIRCrAccount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPDIRCrAccount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        If Trim(txtPDIRCrAccount.Text) = "" Then GoTo EventExitSub


        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPendingIndentNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPendingIndentNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPendingIndentNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPendingIndentNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPlanLockDays_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPlanLockDays.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPlanLockDays_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPlanLockDays.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtQCDays_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtQCDays.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtQCDays_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtQCDays.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSTDS_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSTDS.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSTDS_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSTDS.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSTDS.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSTDS_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSTDS.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccount(txtSTDS)
    End Sub

    Private Sub txtSTDS_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSTDS.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ErrPart
        If Trim(txtSTDS.Text) = "" Then GoTo EventExitSub


        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtStReceivableAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTDSAcNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTDSAcNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtTDSAcNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTDSAcNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtTDSAcNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtTDSCircle_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTDSCircle.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtTDSCircle_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTDSCircle.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtTDSCircle.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub SearchAccount(ByRef mTextBox As System.Windows.Forms.TextBox)
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND (SUPP_CUST_TYPE='O' OR INTER_UNIT='Y')"
        If MainClass.SearchGridMaster(mTextBox.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", , , , SqlStr) = True Then
            mTextBox.Text = AcName
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SearchPDIRAccount(ByRef mTextBox As System.Windows.Forms.TextBox)
        On Error GoTo SearchError
        Exit Sub
SearchError:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtTDSCreditAcct_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTDSCreditAcct.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTDSCreditAcct_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTDSCreditAcct.DoubleClick
        Call SearchAccount(txtTDSCreditAcct)
    End Sub

    Private Sub txtTDSCreditAcct_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTDSCreditAcct.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtTDSCreditAcct.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTDSCreditAcct_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtTDSCreditAcct.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccount(txtTDSCreditAcct)
    End Sub

    Private Sub txtTDSCreditAcct_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTDSCreditAcct.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        If Trim(txtTDSCreditAcct.Text) = "" Then GoTo EventExitSub


        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ErrPart

        With SprdMain
            .set_RowHeight(Arow, ConRowHeight)
            .Row = Arow

            .Col = ColFromAccountName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColFromAccountName, 30)

            .Col = ColToAccountName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColToAccountName, 30)
        End With

        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ErrPart:
        'Resume		
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtAutoProdIssueDate_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles txtAutoProdIssueDate.MaskInputRejected

    End Sub

    Private Sub txtAutoIssueDate_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles txtAutoIssueDate.MaskInputRejected

    End Sub

    Private Sub chkStockBal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkStockBal.CheckedChanged

    End Sub

    Private Sub Label19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Label20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label20.Click

    End Sub

    Private Sub Label15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label15.Click

    End Sub

    Private Sub Label16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label16.Click

    End Sub

    Private Sub Label14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label14.Click

    End Sub

    Private Sub Frame24_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Frame24.Enter

    End Sub

    Private Sub txtCompAc_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCompAc.TextChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtCompAc_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCompAc.DoubleClick
        Call SearchAccount(txtCompAc)
    End Sub
    Private Sub txtCompAc_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCompAc.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCompAc.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCompAc_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCompAc.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call SearchAccount(txtCompAc)
    End Sub
    Private Sub txtCompAc_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCompAc.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        If Trim(txtCompAc.Text) = "" Then GoTo EventExitSub

        'If MainClass.ValidateWithMasterTable(txtCompAc.Text, "SUPP_CUST_NAME", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='2'") = False Then
        If MainClass.ValidateWithMasterTable(txtCompAc.Text, "SUPP_CUST_NAME", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND INTER_UNIT='Y'") = False Then
            MsgInformation("Invaild Account Name. Cannot Save")
            txtCompAc.Focus()
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub chkHideBatchNo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkHideBatchNo.CheckedChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkHideHeatNo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkHideHeatNo.CheckedChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkAcPRAutoJv_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAcPRAutoJv.CheckedChanged
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdCategory_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdCategory.Change
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdCategory_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdCategory.ClickEvent

        On Error GoTo ERR1
        Select Case eventArgs.col
            'Case 0
            '    If eventArgs.row > 0 And SprdCategory.Enabled = True Then
            '        MainClass.DeleteSprdRow(SprdCategory, eventArgs.row, ColFromAccountName)
            '        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
            '    End If
            Case ColOPAccount, ColCLAccount
                If eventArgs.row = 0 Then AccountNameSearch(eventArgs.col, (SprdCategory.ActiveRow))
        End Select
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub AccountNameSearch(ByRef Col As Integer, ByRef Row As Integer)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mString As String = ""
        Dim mTableName As String
        Dim mFieldName1 As String
        Dim mFieldName2 As String

        SprdCategory.Row = Row
        SprdCategory.Col = Col
        mString = SprdCategory.Text
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND STATUS='O'"

        mTableName = "FIN_SUPP_CUST_MST"
        mFieldName1 = "SUPP_CUST_NAME"
        mFieldName2 = "SUPP_CUST_CODE"

        MainClass.SearchGridMaster(mString, mTableName, mFieldName1, mFieldName2, , , SqlStr)

        If AcName <> "" Then
            SprdCategory.Row = SprdCategory.ActiveRow
            SprdCategory.Col = Col
            SprdCategory.Text = AcName
        End If

        SprdCategory_LeaveCell(SprdCategory, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(Col, SprdCategory.ActiveRow, Col, SprdCategory.ActiveRow, False))

        SprdCategory.Refresh()

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdCategory_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdCategory.KeyDownEvent

        On Error GoTo ERR1

        If SprdCategory.ActiveRow <= 0 Then Exit Sub
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)

        Select Case SprdCategory.ActiveCol
            Case ColOPAccount, ColCLAccount
                If eventArgs.keyCode = System.Windows.Forms.Keys.F1 Then AccountNameSearch((SprdCategory.ActiveCol), (SprdCategory.ActiveRow))

                'If eventArgs.keyCode = System.Windows.Forms.Keys.Return Or eventArgs.keyCode = System.Windows.Forms.Keys.Tab Then
                '    If SprdCategory.MaxRows = SprdCategory.ActiveRow Then
                '        MainClass.AddBlankSprdRow(SprdCategory, ColToAccountName, ConRowHeight)
                '        'FormatSprdCategory -1		
                '    End If
                'End If
        End Select
        eventArgs.keyCode = 9999
        Exit Sub
ERR1:
        ErrorMsg(Err.Description)
    End Sub

    Private Sub SprdCategory_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdCategory.LeaveCell

        On Error GoTo ERR1
        Dim pAccountName As String
        Dim mAccountCode As String

        If eventArgs.newRow = -1 Then Exit Sub

        Select Case eventSender.Col
            Case ColOPAccount
                SprdCategory.Col = ColOPAccount
                SprdCategory.Row = eventSender.Row
                pAccountName = Trim(SprdCategory.Text)
                If CheckAccountName(pAccountName, eventSender.Col, eventSender.Row) = False Then
                    eventArgs.cancel = True
                    Exit Sub
                End If

            Case ColCLAccount
                SprdCategory.Col = ColCLAccount
                SprdCategory.Row = eventSender.Row
                pAccountName = Trim(SprdCategory.Text)
                If CheckAccountName(pAccountName, eventSender.Col, eventSender.Row) = False Then
                    eventArgs.cancel = True
                    Exit Sub
                End If
        End Select

        'SprdCategory.Row = eventSender.Row
        'SprdCategory.Col = ColFromAccountName
        'pAccountName = Trim(UCase(SprdCategory.Text))
        'If pAccountName = "" Then Exit Sub

        'SprdCategory.Col = ColToAccountName
        'If pAccountName = Trim(UCase(SprdCategory.Text)) Then
        '    MainClass.SetFocusToCell(SprdCategory, eventSender.Row, ColToAccountName, "Same Account not allowed.")
        '    Exit Sub
        'End If

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub



    Private Sub SprdCategory_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles SprdCategory.Validating
        'Dim Cancel As Boolean = eventArgs.Cancel
        'If SprdCategory.ActiveCol = ColToAccountName Then
        '    Call SprdCategory_LeaveCell(SprdCategory, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(SprdCategory.ActiveCol, SprdCategory.ActiveRow, ColToAccountName, SprdCategory.ActiveCol + 1, False))
        'End If
        'eventArgs.Cancel = Cancel
    End Sub
    Private Sub FormatSprdCategory(ByRef Arow As Integer)

        On Error GoTo ErrPart

        With SprdCategory
            .set_RowHeight(Arow, ConRowHeight)
            .Row = Arow

            .Col = ColCategoryName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("GEN_DESC", "INV_GENERAL_MST", PubDBCn)
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColCategoryName, 20)

            .Col = ColOPAccount
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColOPAccount, 20)

            .Col = ColCLAccount
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColCLAccount, 20)
        End With

        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ErrPart:
        'Resume		
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub _FraBorder_8_Enter(sender As Object, e As EventArgs) Handles _FraBorder_8.Enter

    End Sub
End Class
