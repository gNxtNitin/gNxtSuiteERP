Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports System.Data.SqlClient   '' System.Data.OleDb					
Imports System.Data.OleDb
Imports Microsoft.VisualBasic.Compatibility
Imports System.ComponentModel

Friend Class frmAcm
    Inherits System.Windows.Forms.Form
    Dim RsACM As ADODB.Recordset ''ADODB.Recordset
    Dim RsACMOthers As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection					

    ''Dim RsOpOuts As ADODB.Recordset					

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean
    Dim mAccountCode As String
    Dim ResizeForm As New Resizer

    Private Const ConRowHeight As Short = 14
    Dim xMyMenu As String


    Private Const ColLocation As Short = 1
    Private Const ColAddress As Short = 2
    Private Const ColCity As Short = 3
    Private Const ColState As Short = 4
    Private Const ColPin As Short = 5
    Private Const ColCountry As Short = 6
    Private Const ColGSTNo As Short = 7
    Private Const ColDistance As Short = 8
    Private Const ColAlias As Short = 9
    Private Const ColContactNo As Short = 10
    Private Const ColeMailID As Short = 11
    Private Const ColWithinDistrict As Short = 12
    Private Const ColWithinState As Short = 13
    Private Const ColWithinCountry As Short = 14

    Private Sub SetCombo(ByRef ComboName As System.Windows.Forms.ComboBox, ByRef mMasterType As String)
        Dim CntCount As Integer

        For CntCount = 0 To ComboName.Items.Count - 1
            ComboName.SelectedIndex = CntCount
            If mMasterType = VB.Left(ComboName.Text, 1) Then
                Exit Sub
            End If
        Next
        ComboName.SelectedIndex = -1
    End Sub

    Private Sub cboCategory_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCategory.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboCategory_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCategory.SelectedIndexChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboCType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCType.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboCType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCType.SelectedIndexChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboCustGroup_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCustGroup.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboCustGroup_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCustGroup.SelectedIndexChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboHeadType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboHeadType.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboHeadType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboHeadType.SelectedIndexChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboPaymentMode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboPaymentMode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboSymbol_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboSymbol.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub



    Private Sub cboSymbol_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboSymbol.SelectedIndexChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkSMERegd_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkSMERegd.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkSMEStatus_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkSMEStatus.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCountryCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCountryCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCountryCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCountryCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCountryCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCurrencyCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCurrencyCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCurrencyCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCurrencyCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCurrencyCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtGUID_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGUID.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtGUID_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtGUID.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtGUID.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtLenderBank_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLenderBank.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtLenderBank_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLenderBank.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtLenderBank.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtLenderBank_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtLenderBank.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
    End Sub

    Private Sub txtLenderBank_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtLenderBank.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtLenderBank.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtLenderBank.Text, "SUPP_CUST_NAME", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='2'") = False Then
            MsgInformation("Please Select The valid Bank Name.")
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtUdyogAahaarNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtUdyogAahaarNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtUdyogAahaarNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtUdyogAahaarNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtUdyogAahaarNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub cboPaymentMode_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboPaymentMode.SelectedIndexChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboSupplierType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboSupplierType.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboSupplierType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboSupplierType.SelectedIndexChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub CboTDSCategory_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CboTDSCategory.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkSEZ_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkSEZ.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkAuthorised_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAuthorised.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkGroupLimit_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkGroupLimit.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkDistt_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkDistt.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkCountry_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCountry.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkInterUnit_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkInterUnit.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)

        If chkInterUnit.Checked = True Then
            txtCompanyName.Enabled = True
        Else
            txtCompanyName.Enabled = False
        End If
    End Sub

    Private Sub chkLowerDeduction_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkLowerDeduction.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkMonthWiseLdgr_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkMonthWiseLdgr.CheckStateChanged, chkAccountHide.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub ChkPoRate_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkPoRate.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkSecurityChq_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkSecurityChq.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkState_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkState.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkStopBP_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkStopBP.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkStopGP_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkStopGP.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkStopInvoice_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkStopInvoice.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkStopMRR_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkStopMRR.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkStopPO_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkStopPO.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        If CmdAdd.Text = ConCmdAddCaption Then
            CmdAdd.Text = ConCmdCancelCaption
            ADDMode = True
            MODIFYMode = False
            Clear1()
            SprdMain.Enabled = True
            txtName.Focus()
        Else
            CmdAdd.Text = ConCmdAddCaption
            ADDMode = False
            MODIFYMode = False
            'MainClass.ClearGrid(SprdMain)
            'Call FormatSprdMain(-1)
            Clear1()
            Show1()
        End If
    End Sub
    Private Sub FillComboBox()

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mNature As String

        cboCategory.Items.Clear()
        If lblMasterType.Text = "Accounts" Then
            cboCategory.Items.Add("Employee")
            cboCategory.Items.Add("1- Cash")
            cboCategory.Items.Add("2- Bank")
            cboCategory.Items.Add("Other")
            cboCategory.Items.Add("Fixed Assets")
        ElseIf lblMasterType.Text = "C" Then
            cboCategory.Items.Add("Customer")
            cboCategory.Items.Add("Supplier")
        ElseIf lblMasterType.Text = "S" Then
            cboCategory.Items.Add("Supplier")
            cboCategory.Items.Add("Customer")
        End If

        '    cboCategory.AddItem "Customer"					
        '    cboCategory.AddItem "Supplier"					
        '    cboCategory.AddItem "Employee"					
        '    cboCategory.AddItem "1- Cash"					
        '    cboCategory.AddItem "2- Bank"					
        '    cboCategory.AddItem "Other"					
        '    cboCategory.AddItem "Fixed Assets"					

        cboHeadType.Items.Clear()
        cboHeadType.Items.Add("None")
        cboHeadType.Items.Add("Loan & Advance Head")
        cboHeadType.Items.Add("TDS Head")
        cboHeadType.Items.Add("Imprest Head")
        cboHeadType.Items.Add("ESI Head")
        cboHeadType.Items.Add("Service Tax Claim")
        cboHeadType.Items.Add("Jobworker - Supporting Manu.")
        cboHeadType.Items.Add("Profit & Loss")
        cboHeadType.Items.Add("1. TDS (Salary) Head")
        cboHeadType.Items.Add("2. Duties")
        cboHeadType.Items.Add("3. Increase & Decrease Stock")
        cboHeadType.Items.Add("4. Service Head")
        cboHeadType.Items.Add("5. Salary Head")
        '    cboHeadType.AddItem "Gratuity"					

        '    cboHeadType.AddItem "1. Freight Outward"					
        '    cboHeadType.AddItem "2. Freight Inward"					
        cboHeadType.SelectedIndex = 0

        CboTDSCategory.Items.Clear()
        CboTDSCategory.Items.Add("NONE")
        CboTDSCategory.Items.Add("CONTRACTOR")
        CboTDSCategory.Items.Add("PROFESSIONAL")
        CboTDSCategory.SelectedIndex = 0


        cboSupplierType.Items.Clear()

        cboSupplierType.Items.Add("")
        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
            cboSupplierType.Items.Add("MANUFACTURER")
            cboSupplierType.Items.Add("DEALER")
            cboSupplierType.Items.Add("DISTRIBUTOR")
            cboSupplierType.Items.Add("TRADERS")
            cboSupplierType.Items.Add("IMPORTER")
            cboSupplierType.Items.Add("CONTRACTOR")
            cboSupplierType.Items.Add("OTHER")
            cboSupplierType.Items.Add("CUSTOMER")
            cboSupplierType.Items.Add("SERVICES")
            cboSupplierType.Items.Add("EXPORTER-MERCHANT")
            cboSupplierType.Items.Add("SUPPLIER")
            cboSupplierType.Items.Add("OEM")
            cboSupplierType.Items.Add("INTER BRANCH")
        Else
            cboSupplierType.Items.Add("CONSIGNMENT AGENT")
            cboSupplierType.Items.Add("MANUFACTURER")
            cboSupplierType.Items.Add("1st-STAGE DEALER")
            cboSupplierType.Items.Add("2nd-STAGE DEALER")
            cboSupplierType.Items.Add("TRADERS")
            cboSupplierType.Items.Add("IMPORTER")
            cboSupplierType.Items.Add("CONTRACTOR")
            cboSupplierType.Items.Add("OTHER")
            cboSupplierType.Items.Add("100% EOU")
            cboSupplierType.Items.Add("CUSTOMER")
            cboSupplierType.Items.Add("CUSTOMER-RM")
            cboSupplierType.Items.Add("SERVICES")
            cboSupplierType.Items.Add("EXPORTER-MERCHANT")
            cboSupplierType.Items.Add("RM-SUPPLIER")
            cboSupplierType.Items.Add("OEM")
        End If

        cboSupplierType.SelectedIndex = 0




        SqlStr = "SELECT DISTINCT SUPP_CUST_NATURE FROM FIN_SUPP_CUST_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ORDER BY SUPP_CUST_NATURE"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        cboNature.Items.Clear()

        cboNature.Items.Add("")

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mNature = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NATURE").Value), "", RsTemp.Fields("SUPP_CUST_NATURE").Value)
                If mNature <> "" Then
                    cboNature.Items.Add(mNature)
                End If
                RsTemp.MoveNext()
            Loop
            cboNature.SelectedIndex = 0
        End If

        '    cboNature.AddItem "CAPITAL GOODS"					
        '    cboNature.AddItem "GAS"					
        '    cboNature.AddItem "INSURANCE"					
        '    cboNature.AddItem "JOB WORK/CONTRACTOR_INSIDE"					
        '    cboNature.AddItem "JOB WORK/CONTRACTOR_OUTSIDE"					
        '    cboNature.AddItem "OTHERS"					
        '    cboNature.AddItem "PACKAGING"					
        '    cboNature.AddItem "PLATING CHEMICAL"					
        '    cboNature.AddItem "PRINTING & STATIONARY"					
        '    cboNature.AddItem "PROFESSIONAL & TECHNICAL SERVICE"					
        '    cboNature.AddItem "REPAIR & MAINTAINANCE"					
        '    cboNature.AddItem "SMALL TOOLS & DIES CONSUMABLES"					
        '    cboNature.AddItem "SUPPLIER CONSUMABLE_MIG WIRE"					
        '    cboNature.AddItem "SUPPLIER CONSUMABLE_NICKLE"					
        '    cboNature.AddItem "SUPPLIER CONSUMABLE_OTHER"					
        '    cboNature.AddItem "SUPPLIER IT"					
        '    cboNature.AddItem "SUPPLIER POWER & FUEL"					
        '    cboNature.AddItem "SUPPLIER RM-BOP"					
        '    cboNature.AddItem "SUPPLIER RM-SHEET"					
        '    cboNature.AddItem "SUPPLIER RM-TUBE"					
        '    cboNature.AddItem "SUPPLIER-PAINTS & CHEMICAL"					
        '    cboNature.AddItem "TRANSPORTER"					
        ''cboNature.AddItem "TRANSPORTER"					



        cboCType.Items.Clear()
        cboCType.Items.Add("COMPANY")
        cboCType.Items.Add("NON-COMPANY")
        cboCType.SelectedIndex = 0

        cboPaymentMode.Items.Clear()
        cboPaymentMode.Items.Add("1. Cheque")
        cboPaymentMode.Items.Add("2. Hundi")
        cboPaymentMode.Items.Add("3. LC")
        cboPaymentMode.Items.Add("4. MSME")
        cboPaymentMode.Items.Add("5. PDC")
        cboPaymentMode.Items.Add("6. DISC-YES")
        cboPaymentMode.Items.Add("7. DISC-CASH")
        cboPaymentMode.Items.Add("8. DISC-TCFL")
        cboPaymentMode.Items.Add("9. UGRO")
        cboPaymentMode.Items.Add("A. ONLINE")
        ''cboPaymentMode.AddItem "10. NON MSME"					
        cboPaymentMode.SelectedIndex = 0

        cboEnterpriseType.Items.Clear()
        cboEnterpriseType.Items.Add("")
        cboEnterpriseType.Items.Add("MICRO")
        cboEnterpriseType.Items.Add("SMALL")
        cboEnterpriseType.Items.Add("MEDIUM")
        cboEnterpriseType.SelectedIndex = 0

        cboSymbol.Items.Clear()
        cboSymbol.Items.Add("")
        cboSymbol.Items.Add("A")
        cboSymbol.Items.Add("B")
        cboSymbol.Items.Add("C")
        cboSymbol.Items.Add("F")
        cboSymbol.SelectedIndex = 0

        FillComboName
        FillComboCode

        cboCustGroup.Items.Clear()
        MainClass.FillCombo(cboCustGroup, "FIN_SUPP_CUST_MST", "CUSTOMER_GROUP", , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
        cboCustGroup.SelectedIndex = 0 ''-1
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        On Error Resume Next
        '    'If PvtDBCn.State = adStateOpen Then					
        '        ''PvtDBCn.Close					
        '        ''Set PvtDBCn = Nothing					
        '    End If					
        RsACM.Close()
        Me.Hide() ''me.hide 	
        Me.Close()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click

        On Error GoTo DelErrPart
        Dim SqlStr As String = ""
        Dim RsOpOuts As ADODB.Recordset = Nothing
        Dim mOPBal As Integer

        If txtName.Text = "" Then Call ErrorMsg("Nothing to Delete", "", MsgBoxStyle.Critical) : Exit Sub

        If Not RsACM.EOF Then
            SqlStr = " SELECT COUNT(1) AS CNTROW From FIN_POSTED_TRN WHERE " & vbCrLf _
                & " FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                & " AND BOOKTYPE='" & VB.Left(ConOpening, 1) & "'" & vbCrLf _
                & " AND BOOKSUBTYPE='" & VB.Right(ConOpening, 1) & "'" & vbCrLf _
                & " AND ACCOUNTCODE='" & RsACM.Fields("SUPP_CUST_CODE").Value & "'"

            If CheckConsolidatedMaster("FIN_SUPP_CUST_MST") = True Then
            Else
                SqlStr = SqlStr & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
            End If

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOpOuts, ADODB.LockTypeEnum.adLockReadOnly)
            If Not RsOpOuts.EOF Then
                mOPBal = IIf(IsDBNull(RsOpOuts.Fields("cntRow").Value), 0, RsOpOuts.Fields("cntRow").Value)
            End If

            If mOPBal > 0 Then
                MsgInformation("First Delete Opening Balance.")
                Exit Sub
            End If

            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()

                If InsertIntoDelAudit(PubDBCn, "FIN_SUPP_CUST_MST", (txtName.Text), RsACM, "SUPP_CUST_NAME", "D") = False Then GoTo DelErrPart

                If InsertIntoDeleteTrn(PubDBCn, "FIN_SUPP_CUST_MST", "SUPP_CUST_CODE", RsACM.Fields("SUPP_CUST_CODE").Value) = False Then GoTo DelErrPart

                '            SqlStr = " DELETE From FIN_POSTED_TRN WHERE " & vbCrLf _					
                ''                    & " COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & "" & vbCrLf _					
                ''                    & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _					
                ''                    & " AND BOOKTYPE='" & vb.Left(ConOpening, 1) & "'" & vbCrLf _					
                ''                    & " AND BOOKSUBTYPE='" & Right(ConOpening, 1) & "'" & vbCrLf _					
                ''                    & " AND ACCOUNTCODE='" & RsACM.Fields("SUPP_CUST_CODE").Value & "'"					
                '            PubDBCn.Execute SqlStr					
                SqlStr = " DELETE From FIN_SUPP_CUST_BUSINESS_MST WHERE " & vbCrLf _
                    & " SUPP_CUST_CODE='" & RsACM.Fields("SUPP_CUST_CODE").Value & "'"

                If CheckConsolidatedMaster("FIN_SUPP_CUST_MST") = True Then
                Else
                    SqlStr = SqlStr & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
                End If

                PubDBCn.Execute(SqlStr)

                SqlStr = " DELETE From FIN_SUPP_CUST_MST WHERE " & vbCrLf _
                    & " SUPP_CUST_CODE='" & RsACM.Fields("SUPP_CUST_CODE").Value & "'"

                If CheckConsolidatedMaster("FIN_SUPP_CUST_MST") = True Then
                Else
                    SqlStr = SqlStr & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
                End If

                PubDBCn.Execute(SqlStr)

                PubDBCn.CommitTrans()
                RsACM.Requery() ''.Refresh					
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        '    Resume					
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans() ''					
        RsACM.Requery() ''.Refresh					
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsACM, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler

        If FieldVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If Update1() = True Then
            ADDMode = False
            MODIFYMode = False
            FillComboName()
            FillComboCode()
            SSTInfo.SelectedIndex = 0
            TxtName_Validating(txtName, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True Then CmdAdd.Focus()
        Else
            MsgInformation("Record not saved")
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        Call ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Function Update1() As Boolean
        On Error GoTo UpdateError
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xCompanyCode As Long
        Dim SqlStr As String

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
                If UpdateAcm(xCompanyCode) = False Then GoTo UpdateError
                RsTemp.MoveNext()
            Loop
        End If
        PubDBCn.CommitTrans()
        Update1 = True
        Exit Function
UpdateError:
        Call ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans() ''					
        Update1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Private Function UpdateAcm(xCompanyCode As Long) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mGroupCodeCr As Integer
        Dim mGroupCode As Integer
        Dim mStatus As String = ""
        Dim mCategory As String = ""
        Dim mBalancingMethod As String = ""
        Dim mHeadType As String = ""
        Dim mPaymentMode As String = ""
        Dim mWithInState As String = ""
        Dim mWithInDistt As String = ""
        Dim mWithInCountry As String = ""
        Dim mTypeofSupplier As String = ""
        Dim mCustGroup As String = ""
        Dim mRegdDealer As String = ""
        Dim mPORATEEDITABLE As String = ""
        Dim mCurrencyName As String = ""

        Dim mSectionCode As Integer
        Dim mCTYPE As String = ""

        Dim mPurchaseSTRecd As Integer
        Dim mPurchaseSTDue As Integer
        Dim mSaleSTRecd As Integer
        Dim mSaleSTDue As Integer
        Dim mBuyerCode As String = ""
        Dim mMonthWiseLdgr As String = ""

        Dim mServiceProviderCode As Double
        Dim mInterUnit As String = ""
        Dim mAuthorised As String = ""

        Dim mStopMRR As String = ""
        Dim mStopInvoice As String = ""
        Dim mStopGP As String = ""
        Dim mStopPO As String = ""
        Dim mStopBP As String = ""
        Dim mIsLowerDed As String = ""
        Dim mGSTRegd As String = ""
        Dim mGSTClass As String = ""
        Dim mSEZ As String = ""
        Dim mSMERegd As String = ""
        Dim mSMEStatus As String = ""
        Dim mLenderBankCode As String = ""
        Dim mTCSApplicable As String = ""
        Dim mTCSNotApplicable As String
        Dim mPlaceofSupply As String
        Dim mInterUnitCompanyCode As Integer
        Dim mTDSDED_UNDER194Q As String
        Dim mTDSNOTDED_UNDER194Q As String
        Dim mTDSDED_Submitted As String
        Dim mSecurityDeposit As String
        Dim mGroupLimit As String
        Dim mAccountHide As String

        txtName.Text = UCase(txtName.Text)

        txtaddress.Text = Trim(UCase(txtaddress.Text))
        txtaddress.Text = Replace(txtaddress.Text, System.Environment.NewLine, " ")

        If MainClass.ValidateWithMasterTable(txtCompanyName.Text, "COMPANY_NAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
            mInterUnitCompanyCode = MasterNo
        End If

        If MainClass.ValidateWithMasterTable(txtLenderBank.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCompanyCode & "") = True Then
            mLenderBankCode = MasterNo
        Else
            mLenderBankCode = ""
        End If

        If MainClass.ValidateWithMasterTable(txtPayment.Text, "PAY_TERM_CODE", "PAY_TERM_DESC", "FIN_PAYTERM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCompanyCode & "") = True Then
            lblPaymentTerms.Text = MasterNo
        Else
            lblPaymentTerms.Text = ""
        End If

        If MainClass.ValidateWithMasterTable(txtSection.Text, "Name", "Code", "TDS_Section_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCompanyCode & "") = True Then
            mSectionCode = MasterNo
        Else
            mSectionCode = -1
        End If

        If MainClass.ValidateWithMasterTable(txtBuyerName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCompanyCode & "") = True Then
            mBuyerCode = MasterNo
        Else
            mBuyerCode = ""
        End If

        If MainClass.ValidateWithMasterTable(txtServProvided.Text, "NAME", "CODE", "FIN_SERVPROV_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCompanyCode & "") = True Then
            mServiceProviderCode = MasterNo
        Else
            mServiceProviderCode = -1
        End If

        mCTYPE = VB.Left(cboCType.Text, 1)

        mCurrencyName = IIf(Trim(txtCurrency.Text) = "", "Rs", Trim(txtCurrency.Text))
        mStatus = IIf(OptStatus(0).Checked = True, "O", "C")
        mCategory = VB.Left(cboCategory.Text, 1)
        mHeadType = VB.Left(cboHeadType.Text, 1)
        mPaymentMode = VB.Left(cboPaymentMode.Text, 1)

        mTypeofSupplier = Trim(cboSupplierType.Text)
        mCustGroup = Trim(cboCustGroup.Text)

        If MainClass.ValidateWithMasterTable(txtGroupName.Text, "Group_Name", "Group_Code", "FIN_Group_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCompanyCode & "") = True Then
            mGroupCode = MasterNo
        End If

        If MainClass.ValidateWithMasterTable(txtGroupNameCr.Text, "Group_Name", "Group_Code", "FIN_Group_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCompanyCode & "") = True Then
            mGroupCodeCr = MasterNo
        End If

        'If MainClass.ValidateWithMasterTable(txtPurchaseSTRecd.Text, "NAME", "CODE", "FIN_STFORM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCompanyCode & "") = True Then
        '    mPurchaseSTRecd = MasterNo
        'End If

        'If MainClass.ValidateWithMasterTable(txtPurchaseSTDue.Text, "NAME", "CODE", "FIN_STFORM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCompanyCode & "") = True Then
        '    mPurchaseSTDue = MasterNo
        'End If

        'If MainClass.ValidateWithMasterTable(txtSaleSTRecd.Text, "NAME", "CODE", "FIN_STFORM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCompanyCode & "") = True Then
        '    mSaleSTRecd = MasterNo
        'End If

        'If MainClass.ValidateWithMasterTable(txtSaleSTDue.Text, "NAME", "CODE", "FIN_STFORM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & xCompanyCode & "") = True Then
        '    mSaleSTDue = MasterNo
        'End If



        '*********					
        mBalancingMethod = IIf(optBalMethod(0).Checked = True, "S", "D")
        mWithInState = IIf(chkState.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mInterUnit = IIf(chkInterUnit.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mAuthorised = IIf(chkAuthorised.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mGroupLimit = IIf(chkGroupLimit.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mSEZ = IIf(chkSEZ.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mWithInCountry = IIf(chkCountry.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mPORATEEDITABLE = IIf(ChkPoRate.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mSecurityDeposit = IIf(chkSecurityChq.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mTCSApplicable = IIf(chkTCSApplicable.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mTCSNotApplicable = IIf(chkTCSNotApplicable.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mPlaceofSupply = IIf(chkPlaceofSupply.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mMonthWiseLdgr = IIf(chkMonthWiseLdgr.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mAccountHide = IIf(chkAccountHide.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") ''chkAccountHide
        mIsLowerDed = IIf(chkLowerDeduction.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mSMERegd = IIf(chkSMERegd.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mSMEStatus = IIf(chkSMEStatus.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mStopMRR = IIf(chkStopMRR.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mStopInvoice = IIf(chkStopInvoice.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mStopGP = IIf(chkStopGP.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mStopBP = IIf(chkStopBP.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mStopPO = IIf(chkStopPO.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mTDSDED_UNDER194Q = IIf(chkTDSDeduct.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mTDSNOTDED_UNDER194Q = IIf(chkTDSNotDeduct.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mTDSDED_Submitted = IIf(chkRtnDeclaration.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")


        mWithInDistt = IIf(chkDistt.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        If optRegd(0).Checked = True Then
            mRegdDealer = "Y"
        Else
            mRegdDealer = "N"
        End If

        If optGSTRegd(0).Checked = True Then
            mGSTRegd = "Y"
        ElseIf optGSTRegd(1).Checked = True Then
            mGSTRegd = "N"
        ElseIf optGSTRegd(2).Checked = True Then
            mGSTRegd = "E"
        ElseIf optGSTRegd(3).Checked = True Then
            mGSTRegd = "F"
        ElseIf optGSTRegd(4).Checked = True Then
            mGSTRegd = "C"
        End If

        If optGSTClassification(0).Checked = True Then
            mGSTClass = "F"
        Else
            mGSTClass = "R"
        End If

        If MODIFYMode = True Then
            If InsertIntoDelAudit(PubDBCn, "FIN_SUPP_CUST_MST", (txtCode.Text), RsACM, "SUPP_CUST_CODE", "M") = False Then GoTo ErrPart
        End If

        If ADDMode = True Then

            mAccountCode = MainClass.AllowSingleQuote(txtCode.Text) ''MainClass.AutoGenRowNo("ACM", "Code", PubDBCn)					

            SqlStr = ""
            SqlStr = " INSERT INTO FIN_SUPP_CUST_MST ( " & vbCrLf _
                    & " COMPANY_CODE, SUPP_CUST_CODE, SUPP_CUST_TYPE,  " & vbCrLf _
                    & " SUPP_CUST_NAME, SUPP_CUST_ADDR, SUPP_CUST_CITY,  " & vbCrLf _
                    & " SUPP_CUST_STATE, SUPP_CUST_PIN, SUPP_CUST_PHONE, " & vbCrLf _
                    & " SUPP_CUST_FAXNO, SUPP_CUST_MAILID,SUPP_CUST_MOBILE, " & vbCrLf _
                    & " CST_NO, LST_NO, PAN_NO,  " & vbCrLf _
                    & " EXCISE_DIV, EXCISE_RANGE, CENT_EXC_RGN_NO, " & vbCrLf _
                    & " ECC_NO, SUPP_CUST_REMARKS, " & vbCrLf _
                    & " WITHIN_STATE, WITHIN_DISTT, WITHIN_COUNTRY," & vbCrLf _
                    & " COMMISIONER_RATE, REGD_DEALER, " & vbCrLf _
                    & " CONTACT_TELNO, " & vbCrLf _
                    & " ACTIVITY, TYPE_OF_SUPPLIER, SRV_REGN_NO, " & vbCrLf _
                    & " GROUPCODE, GROUPCODECR, BALANCINGMETHOD, HEADTYPE, " & vbCrLf _
                    & " HEAD_PER, TDSCATEGORY, TDS_PER, STDS_PER, ESI_PER, " & vbCrLf _
                    & " PAIDDAY,ACCOUNT_CODE,PORATEEDITABLE,DSP_RPT_SEQ, CURRENCYNAME, EMP_CODE, " & vbCrLf _
                    & " CTYPE, SECTIONCODE, EXPTIONCNO, STATUS," & vbCrLf _
                    & " PUR_STRECD_FORMCODE, PUR_STDUE_FORMCODE, " & vbCrLf _
                    & " SALE_STRECD_FORMCODE, SALE_STDUE_FORMCODE, ALIAS_NAME, "


            SqlStr = SqlStr & vbCrLf _
                    & " COUNTRY, BUYERCODE, CARRIAGE, " & vbCrLf _
                    & " LOADINGPORT, DISCHARGEPORT, FINALDEST, " & vbCrLf _
                    & " PAYMENTTERMS, "

            SqlStr = SqlStr & vbCrLf _
                    & " PAIDDAY2, PAIDDAY3, PAIDDAY4, VENDOR_CODE, " & vbCrLf _
                    & " MONTHWISE_LDGR, SERVPROV_CODE, INTER_UNIT, " & vbCrLf _
                    & " PAYMENT_CODE, PAYMENT_DESC, AUTHORISED, "

            SqlStr = SqlStr & vbCrLf _
                    & " STOP_MRR, STOP_INVOICE, STOP_RGP, STOP_BANK, WEB_PASSWORD, " & vbCrLf _
                    & " LOWER_DED_CERT_NO, IS_LOWER_DED,PAYMENT_MODE, "

            SqlStr = SqlStr & vbCrLf _
                    & " CUST_BANK_ACCT_NO, BANK_SWIFT_CODE, BANK_BRANCH_NAME, " & vbCrLf _
                    & " BANK_IFSC_CODE, CUST_BANK_BANK, " & vbCrLf _
                    & " ADDUSER,ADDDATE,MODUSER,MODDATE, " & vbCrLf _
                    & " GST_RGN_NO, GST_REGD, SUPP_CUST_POLICYNO, GST_CLASSIFICATION, IS_SEZ, " & vbCrLf _
                    & " CUSTOMER_GROUP,STOP_PO, ENTERPRISE_TYPE, SME_SYMBOL, " & vbCrLf _
                    & " UDYOGAAHAARNO,SME_REGD,SME_STATUS, GROUP_UID, SUPP_CUST_NATURE, LENDER_BANK_CODE, " & vbCrLf _
                    & " TCS_APP, TCS_NOT_APP, PLACE_OF_SUPPLY, CURRENCY_CODE, COUNTRY_CODE,INTERUNIT_COMPANY_CODE, " & vbCrLf _
                    & " TDS_UNDER_194Q,TDS_NOT_UNDER_194Q, TDS_DECLARATION_SUB, CREDIT_LIMIT, RESPONSIBLE_PERSON,IS_SECURITY_DEPOSIT," & vbCrLf _
                    & " SECURITY_AMOUNT,SECURITY_CHEQUE_NO,GROUP_LIMIT,ACCOUNT_HIDE,SUPP_CUST_SHORT_NAME,RECEIPT_DAYS) VALUES ( "

            '
            SqlStr = SqlStr & vbCrLf _
                    & " " & xCompanyCode & ", '" & MainClass.AllowSingleQuote(txtCode.Text) & "', '" & MainClass.AllowSingleQuote(mCategory) & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtName.Text) & "', '" & MainClass.AllowSingleQuote(txtaddress.Text.Replace(vbCrLf, "")) & "', '" & MainClass.AllowSingleQuote(txtCity.Text) & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtState.Text) & "','" & MainClass.AllowSingleQuote(txtPinCode.Text) & "','" & MainClass.AllowSingleQuote(txtPhone.Text) & "'," & vbCrLf & " '" & MainClass.AllowSingleQuote(txtFax.Text) & "','" & MainClass.AllowSingleQuote(txtEmail.Text) & "','" & MainClass.AllowSingleQuote(txtMobile.Text) & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtCstNo.Text) & "','" & MainClass.AllowSingleQuote(txtLSTNo.Text) & "','" & MainClass.AllowSingleQuote(txtPan.Text) & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtDivision.Text) & "', '" & MainClass.AllowSingleQuote(txtRange.Text) & "','" & MainClass.AllowSingleQuote(txtRegnNo.Text) & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtECCNo.Text) & "', '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "'," & vbCrLf _
                    & " '" & mWithInState & "', '" & mWithInDistt & "', '" & mWithInCountry & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtCommRate.Text) & "', '" & MainClass.AllowSingleQuote(mRegdDealer) & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtContact.Text) & "', " & vbCrLf _
                    & " " & Val(txtChqFrequency.Text) & ",'" & mTypeofSupplier & "', '" & MainClass.AllowSingleQuote(txtSrvRegnNo.Text) & "'," & vbCrLf _
                    & " " & mGroupCode & ", " & mGroupCodeCr & ", '" & mBalancingMethod & "', '" & mHeadType & "'," & vbCrLf _
                    & " 0,'" & MainClass.AllowSingleQuote(CboTDSCategory.Text) & "', " & vbCrLf _
                    & " " & Val(txtTDSPer.Text) & ", " & Val(txtSTDSPer.Text) & ", " & Val(txtESIPer.Text) & ", " & vbCrLf _
                    & " " & Val(txtPaidDay.Text) & ",'" & MainClass.AllowSingleQuote(txtTINNo.Text) & "'," & vbCrLf _
                    & " '" & mPORATEEDITABLE & "', '" & MainClass.AllowSingleQuote(txtSeq.Text) & "', '" & Trim(mCurrencyName) & "', '" & Trim(txtEmpCode.Text) & "', " & vbCrLf _
                    & " '" & mCTYPE & "', " & mSectionCode & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(Trim(txtExptionCNo.Text)) & "', '" & mStatus & "', " & vbCrLf _
                    & " " & mPurchaseSTRecd & ", " & mPurchaseSTDue & ", " & vbCrLf _
                    & " " & mSaleSTRecd & ", " & mSaleSTDue & ", '" & MainClass.AllowSingleQuote(txtAlias.Text) & "', "



            SqlStr = SqlStr & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtCountry.Text) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(mBuyerCode) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtCarriage.Text) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtLoadingPort.Text) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtDischargePort.Text) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtFinalDest.Text) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtExportPaymetTerms.Text) & "', "


            SqlStr = SqlStr & vbCrLf _
                    & " " & Val(txtPaidDay2.Text) & ", " & Val(txtPaidDay3.Text) & ", " & Val(txtPaidDay4.Text) & ", " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtVendorCode.Text) & "', '" & mMonthWiseLdgr & "', " & vbCrLf _
                    & " " & IIf(mServiceProviderCode = -1, "Null", mServiceProviderCode) & ", '" & mInterUnit & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtPayment.Text) & "','" & MainClass.AllowSingleQuote(lblPaymentTerms.Text) & "','" & mAuthorised & "',"


            SqlStr = SqlStr & vbCrLf _
                    & " '" & mStopMRR & "', '" & mStopInvoice & "', '" & mStopGP & "', '" & mStopBP & "', '" & MainClass.AllowSingleQuote(txtCode.Text) & "'," & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtLDCertiNo.Text) & "', '" & mIsLowerDed & "', '" & mPaymentMode & "',"



            SqlStr = SqlStr & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtBankAccountNo.Text) & "', '" & MainClass.AllowSingleQuote(txtSwitCode.Text) & "', '" & MainClass.AllowSingleQuote(txtBankBranch.Text) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtIFSCCode.Text) & "', '" & MainClass.AllowSingleQuote(txtBankName.Text) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtGSTRegnNo.Text) & "', '" & mGSTRegd & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(txtPolicyNo.Text) & "', '" & mGSTClass & "','" & mSEZ & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(mCustGroup) & "','" & mStopPO & "'," & vbCrLf _
                    & " '" & Trim(cboEnterpriseType.Text) & "', '" & Trim(cboSymbol.Text) & "', '" & MainClass.AllowSingleQuote(txtUdyogAahaarNo.Text) & "','" & mSMERegd & "','" & mSMEStatus & "', " & vbCrLf _
                    & " '" & Trim(txtGUID.Text) & "', '" & Trim(cboNature.Text) & "', '" & MainClass.AllowSingleQuote(mLenderBankCode) & "', '" & mTCSApplicable & "', '" & mTCSNotApplicable & "'," & vbCrLf _
                    & " '" & mPlaceofSupply & "','" & Trim(txtCurrencyCode.Text) & "', '" & Trim(txtCountryCode.Text) & "'," & mInterUnitCompanyCode & "," & vbCrLf _
                    & " '" & mTDSDED_UNDER194Q & "','" & mTDSNOTDED_UNDER194Q & "','" & mTDSDED_Submitted & "'," & Val(txtCreditLimit.Text) & ",'" & MainClass.AllowSingleQuote(txtResponsiblePerson.Text.Replace(vbCrLf, "")) & "'," & vbCrLf _
                    & " '" & mSecurityDeposit & "', " & Val(txtSecurityAmount.Text) & ", '" & MainClass.AllowSingleQuote(txtSecurityChqNo.Text) & "','" & mGroupLimit & "','" & mAccountHide & "','" & MainClass.AllowSingleQuote(txtShortName.Text) & "'," & Val(txtReceiptDays.Text) & " )"

            PubDBCn.Execute(SqlStr)
        End If

        If MODIFYMode = True Then
            SqlStr = ""

            ''
            SqlStr = " UPDATE FIN_SUPP_CUST_MST SET  SECURITY_CHEQUE_NO='" & MainClass.AllowSingleQuote(txtSecurityChqNo.Text) & "'," & vbCrLf _
                & " SUPP_CUST_NAME= '" & MainClass.AllowSingleQuote(txtName.Text) & "', CREDIT_LIMIT=" & Val(txtCreditLimit.Text) & "," & vbCrLf _
                & " GROUPCODE= " & mGroupCode & " , GROUPCODECR= " & mGroupCodeCr & " , RECEIPT_DAYS=" & Val(txtReceiptDays.Text) & "," & vbCrLf _
                & " SUPP_CUST_TYPE= '" & mCategory & "'," & vbCrLf _
                & " INTER_UNIT= '" & mInterUnit & "', GROUP_UID = '" & Trim(txtGUID.Text) & "', SUPP_CUST_NATURE = '" & Trim(cboNature.Text) & "', " & vbCrLf _
                & " BALANCINGMETHOD='" & mBalancingMethod & "' , RESPONSIBLE_PERSON= '" & MainClass.AllowSingleQuote(txtResponsiblePerson.Text.Replace(vbCrLf, "")) & "'," & vbCrLf _
                & " SUPP_CUST_ADDR= '" & MainClass.AllowSingleQuote(txtaddress.Text.Replace(vbCrLf, "")) & "', INTERUNIT_COMPANY_CODE=" & mInterUnitCompanyCode & "," & vbCrLf _
                & " SUPP_CUST_CITY= '" & MainClass.AllowSingleQuote(txtCity.Text) & "', " & vbCrLf _
                & " SUPP_CUST_PIN= '" & MainClass.AllowSingleQuote(txtPinCode.Text) & "', " & vbCrLf _
                & " SUPP_CUST_STATE= '" & MainClass.AllowSingleQuote(txtState.Text) & "' , " & vbCrLf _
                & " SUPP_CUST_PHONE= '" & MainClass.AllowSingleQuote(txtPhone.Text) & "', " & vbCrLf _
                & " SUPP_CUST_FAXNO= '" & MainClass.AllowSingleQuote(txtFax.Text) & "', " & vbCrLf _
                & " SUPP_CUST_MAILID= '" & MainClass.AllowSingleQuote(txtEmail.Text) & "', " & vbCrLf _
                & " SUPP_CUST_MOBILE= '" & MainClass.AllowSingleQuote(txtMobile.Text) & "', " & vbCrLf _
                & " SUPP_CUST_POLICYNO='" & MainClass.AllowSingleQuote(txtPolicyNo.Text) & "', " & vbCrLf _
                & " AUTHORISED='" & mAuthorised & "', ACCOUNT_HIDE='" & mAccountHide & "'," & vbCrLf _
                & " LST_NO= '" & MainClass.AllowSingleQuote(txtLSTNo.Text) & "', " & vbCrLf _
                & " CST_NO= '" & MainClass.AllowSingleQuote(txtCstNo.Text) & "', " & vbCrLf _
                & " SECTIONCODE=" & mSectionCode & ", " & vbCrLf _
                & " EXPTIONCNO='" & MainClass.AllowSingleQuote(Trim(txtExptionCNo.Text)) & "', " & vbCrLf _
                & " LOWER_DED_CERT_NO='" & MainClass.AllowSingleQuote(txtLDCertiNo.Text) & "', IS_LOWER_DED='" & mIsLowerDed & "'," & vbCrLf _
                & " ALIAS_NAME='" & MainClass.AllowSingleQuote(txtAlias.Text) & "', " & vbCrLf _
                & " CURRENCY_CODE='" & Trim(txtCurrencyCode.Text) & "', COUNTRY_CODE='" & Trim(txtCountryCode.Text) & "'," & vbCrLf _
                & " CTYPE='" & mCTYPE & "', STATUS='" & mStatus & "', GROUP_LIMIT='" & mGroupLimit & "'," & vbCrLf _
                & " TDS_UNDER_194Q='" & mTDSDED_UNDER194Q & "',TDS_NOT_UNDER_194Q='" & mTDSNOTDED_UNDER194Q & "',TDS_DECLARATION_SUB='" & mTDSDED_Submitted & "',"


            SqlStr = SqlStr & vbCrLf _
                & " CURRENCYNAME = '" & Trim(mCurrencyName) & "', " & vbCrLf _
                & " PAN_NO= '" & MainClass.AllowSingleQuote(txtPan.Text) & "', " & vbCrLf _
                & " EXCISE_DIV= '" & MainClass.AllowSingleQuote(txtDivision.Text) & "', " & vbCrLf _
                & " EXCISE_RANGE= '" & MainClass.AllowSingleQuote(txtRange.Text) & "', " & vbCrLf _
                & " CENT_EXC_RGN_NO= '" & MainClass.AllowSingleQuote(txtRegnNo.Text) & "', " & vbCrLf _
                & " ECC_NO= '" & MainClass.AllowSingleQuote(txtECCNo.Text) & "', " & vbCrLf _
                & " SUPP_CUST_REMARKS= '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                & " WITHIN_STATE= '" & MainClass.AllowSingleQuote(mWithInState) & "', " & vbCrLf _
                & " WITHIN_DISTT= '" & MainClass.AllowSingleQuote(mWithInDistt) & "', " & vbCrLf _
                & " WITHIN_COUNTRY= '" & MainClass.AllowSingleQuote(mWithInCountry) & "', " & vbCrLf _
                & " COMMISIONER_RATE= '" & MainClass.AllowSingleQuote(txtCommRate.Text) & "', " & vbCrLf _
                & " REGD_DEALER= '" & MainClass.AllowSingleQuote(mRegdDealer) & "', " & vbCrLf _
                & " CONTACT_TELNO= '" & MainClass.AllowSingleQuote(txtContact.Text) & "', " & vbCrLf _
                & " ACTIVITY= " & Val(txtChqFrequency.Text) & ", TYPE_OF_SUPPLIER= '" & mTypeofSupplier & "', " & vbCrLf _
                & " HEADTYPE= '" & MainClass.AllowSingleQuote(mHeadType) & "', " & vbCrLf _
                & " HEAD_PER= 0, EMP_CODE='" & Trim(txtEmpCode.Text) & "', " & vbCrLf _
                & " TDSCATEGORY= '" & MainClass.AllowSingleQuote(CboTDSCategory.Text) & "', " & vbCrLf _
                & " TDS_PER= " & Val(txtTDSPer.Text) & ", STDS_PER= " & Val(txtSTDSPer.Text) & ", ESI_PER= " & Val(txtESIPer.Text) & ", " & vbCrLf _
                & " PORATEEDITABLE='" & mPORATEEDITABLE & "'," & vbCrLf _
                & " PAIDDAY= " & Val(txtPaidDay.Text) & ",ACCOUNT_CODE='" & MainClass.AllowSingleQuote(txtTINNo.Text) & "', " & vbCrLf _
                & " SRV_REGN_NO='" & MainClass.AllowSingleQuote(txtSrvRegnNo.Text) & "'," & vbCrLf _
                & " DSP_RPT_SEQ='" & MainClass.AllowSingleQuote(txtSeq.Text) & "', "


            SqlStr = SqlStr & vbCrLf _
                & " PUR_STRECD_FORMCODE=" & mPurchaseSTRecd & ", " & vbCrLf _
                & " PUR_STDUE_FORMCODE=" & mPurchaseSTDue & ", " & vbCrLf _
                & " SALE_STRECD_FORMCODE=" & mSaleSTRecd & ", " & vbCrLf _
                & " SALE_STDUE_FORMCODE=" & mSaleSTDue & ", " & vbCrLf _
                & " GST_RGN_NO = '" & MainClass.AllowSingleQuote(txtGSTRegnNo.Text) & "', GST_REGD = '" & mGSTRegd & "', " & vbCrLf _
                & " TCS_APP='" & mTCSApplicable & "',TCS_NOT_APP='" & mTCSNotApplicable & "',PLACE_OF_SUPPLY='" & mPlaceofSupply & "',"



            SqlStr = SqlStr & vbCrLf _
                & " COUNTRY='" & MainClass.AllowSingleQuote(txtCountry.Text) & "'," & vbCrLf _
                & " BUYERCODE='" & MainClass.AllowSingleQuote(mBuyerCode) & "', " & vbCrLf _
                & " CARRIAGE='" & MainClass.AllowSingleQuote(txtCarriage.Text) & "', " & vbCrLf _
                & " LOADINGPORT='" & MainClass.AllowSingleQuote(txtLoadingPort.Text) & "'," & vbCrLf _
                & " DISCHARGEPORT='" & MainClass.AllowSingleQuote(txtDischargePort.Text) & "', " & vbCrLf _
                & " FINALDEST='" & MainClass.AllowSingleQuote(txtFinalDest.Text) & "', " & vbCrLf _
                & " PAYMENTTERMS='" & MainClass.AllowSingleQuote(txtExportPaymetTerms.Text) & "', " & vbCrLf _
                & " LENDER_BANK_CODE = '" & MainClass.AllowSingleQuote(mLenderBankCode) & "',"


            SqlStr = SqlStr & vbCrLf _
                & " PAIDDAY2=" & Val(txtPaidDay2.Text) & ", " & vbCrLf _
                & " PAIDDAY3=" & Val(txtPaidDay3.Text) & ", " & vbCrLf _
                & " PAIDDAY4=" & Val(txtPaidDay4.Text) & ", " & vbCrLf _
                & " MONTHWISE_LDGR='" & mMonthWiseLdgr & "', " & vbCrLf _
                & " SERVPROV_CODE= " & IIf(mServiceProviderCode = -1, "Null", mServiceProviderCode) & ", " & vbCrLf _
                & " PAYMENT_CODE='" & MainClass.AllowSingleQuote(txtPayment.Text) & "', " & vbCrLf _
                & " PAYMENT_DESC='" & MainClass.AllowSingleQuote(lblPaymentTerms.Text) & "', "


            SqlStr = SqlStr & vbCrLf _
                & " STOP_MRR='" & mStopMRR & "', STOP_INVOICE='" & mStopInvoice & "', " & vbCrLf _
                & " PAYMENT_MODE='" & mPaymentMode & "',STOP_RGP='" & mStopGP & "', STOP_BANK='" & mStopBP & "', STOP_PO='" & mStopPO & "', " & vbCrLf _
                & " ENTERPRISE_TYPE='" & Trim(cboEnterpriseType.Text) & "', " & vbCrLf _
                & " SME_SYMBOL='" & Trim(cboSymbol.Text) & "', " & vbCrLf _
                & " UDYOGAAHAARNO='" & MainClass.AllowSingleQuote(txtUdyogAahaarNo.Text) & "'," & vbCrLf _
                & " SME_REGD= '" & mSMERegd & "', SME_STATUS='" & mSMEStatus & "',"



            SqlStr = SqlStr & vbCrLf _
                & " CUST_BANK_ACCT_NO= '" & MainClass.AllowSingleQuote(txtBankAccountNo.Text) & "', " & vbCrLf _
                & " BANK_SWIFT_CODE= '" & MainClass.AllowSingleQuote(txtSwitCode.Text) & "', " & vbCrLf _
                & " BANK_BRANCH_NAME= '" & MainClass.AllowSingleQuote(txtBankBranch.Text) & "', " & vbCrLf _
                & " BANK_IFSC_CODE= '" & MainClass.AllowSingleQuote(txtIFSCCode.Text) & "', " & vbCrLf _
                & " CUST_BANK_BANK= '" & MainClass.AllowSingleQuote(txtBankName.Text) & "',  IS_SECURITY_DEPOSIT = '" & mSecurityDeposit & "', SECURITY_AMOUNT=" & Val(txtSecurityAmount.Text) & "," & vbCrLf _
                & " GST_CLASSIFICATION='" & mGSTClass & "', IS_SEZ='" & mSEZ & "', CUSTOMER_GROUP='" & MainClass.AllowSingleQuote(mCustGroup) & "'," & vbCrLf _
                & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "',MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')"


            SqlStr = SqlStr & vbCrLf _
                & " Where COMPANY_CODE=" & xCompanyCode & " " & vbCrLf _
                & " AND SUPP_CUST_CODE = '" & mAccountCode & "'"


            PubDBCn.Execute(SqlStr)



        End If

        If RsCompany.Fields("COMPANY_CODE").Value = xCompanyCode Then
            SqlStr = " UPDATE FIN_SUPP_CUST_MST SET VENDOR_CODE='" & MainClass.AllowSingleQuote(txtVendorCode.Text) & "', LOC_DISTANCE= " & Val(txtDistance.Text) & ", SUPP_CUST_SHORT_NAME = '" & MainClass.AllowSingleQuote(txtShortName.Text) & "'" & vbCrLf _
                   & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                   & " AND SUPP_CUST_CODE = '" & mAccountCode & "'"

            PubDBCn.Execute(SqlStr)
        End If

        SqlStr = "UPDATE FIN_SUPP_CUST_HDR Set PAYMENT_CODE='" & MainClass.AllowSingleQuote(txtPayment.Text) & "'" & vbCrLf _
                        & " Where COMPANY_CODE=" & xCompanyCode & " " & vbCrLf _
                        & " AND SUPP_CUST_CODE = '" & mAccountCode & "'"


        PubDBCn.Execute(SqlStr)

        If VB.Left(cboCategory.Text, 1) = "S" Or VB.Left(cboCategory.Text, 1) = "C" Or mBalancingMethod = "D" Then
            If UpdateDetail1(mAccountCode, xCompanyCode) = False Then GoTo ErrPart
        End If

        UpdateAcm = True
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateAcm = False
        '    Resume					
    End Function
    Private Function UpdateDetail1(ByVal pAccountCode As String, pCompanyCode As Long) As Boolean

        On Error GoTo UpdateDetail1Err

        Dim SqlStr As String = ""
        Dim I As Integer
        Dim j As Integer
        Dim pLocationID As String = ""
        Dim pAccountName As String = ""
        Dim pAddress As String = ""
        Dim mCityName As String = ""
        Dim pStateName As String = ""
        Dim pPinCode As String = ""
        Dim pPhoneNo As String = ""
        Dim pFaxNo As String = ""
        Dim pMailId As String = ""
        Dim pMobileNo As String = ""
        Dim pWithInState As String = ""
        Dim pWithinDistt As String = ""
        Dim pLocDistance As Double
        Dim pCountry As String = ""

        Dim pWithinCountry As String = "Y"
        Dim pVendorCode As String = ""
        Dim pGSTNo As String = ""
        Dim pAlias As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing


        'PubDBCn.Execute("DELETE FROM FIN_SUPP_CUST_BUSINESS_MST WHERE COMPANY_CODE=" & pCompanyCode & " AND  SUPP_CUST_CODE='" & pAccountCode & "'")

        pWithInState = IIf(chkState.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        pWithinDistt = IIf(chkDistt.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        pWithinCountry = IIf(chkCountry.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        SqlStr = "SELECT * FROM FIN_SUPP_CUST_BUSINESS_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf _
            & " And  SUPP_CUST_CODE='" & pAccountCode & "' AND SERIAL_NO=1"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            SqlStr = " UPDATE FIN_SUPP_CUST_BUSINESS_MST SET " & vbCrLf _
                    & " SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtName.Text) & "', " & vbCrLf _
                    & " SUPP_CUST_ADDR='" & MainClass.AllowSingleQuote(txtaddress.Text) & "', " & vbCrLf _
                    & " SUPP_CUST_CITY='" & MainClass.AllowSingleQuote(txtCity.Text) & "', " & vbCrLf _
                    & " SUPP_CUST_STATE='" & MainClass.AllowSingleQuote(txtState.Text) & "', " & vbCrLf _
                    & " SUPP_CUST_PIN='" & MainClass.AllowSingleQuote(txtPinCode.Text) & "', " & vbCrLf _
                    & " SUPP_CUST_PHONE='" & MainClass.AllowSingleQuote(txtPhone.Text) & "', " & vbCrLf _
                    & " SUPP_CUST_FAXNO='" & MainClass.AllowSingleQuote(txtFax.Text) & "', " & vbCrLf _
                    & " SUPP_CUST_MAILID='" & MainClass.AllowSingleQuote(txtEmail.Text) & "', " & vbCrLf _
                    & " SUPP_CUST_MOBILE='" & MainClass.AllowSingleQuote(txtMobile.Text) & "', COUNTRY='" & MainClass.AllowSingleQuote(txtCountry.Text) & "'," & vbCrLf _
                    & " WITHIN_STATE='" & MainClass.AllowSingleQuote(pWithInState) & "', " & vbCrLf _
                    & " WITHIN_DISTT='" & MainClass.AllowSingleQuote(pWithinDistt) & "'," & vbCrLf _
                    & " WITHIN_COUNTRY='" & MainClass.AllowSingleQuote(pWithinCountry) & "', " & vbCrLf _
                    & " VENDOR_CODE= '" & MainClass.AllowSingleQuote(txtVendorCode.Text) & "', GST_RGN_NO='" & MainClass.AllowSingleQuote(txtGSTRegnNo.Text) & "', " & vbCrLf _
                    & " ALIAS_NAME='" & MainClass.AllowSingleQuote(txtAlias.Text) & "'" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf _
                    & " And  SUPP_CUST_CODE='" & pAccountCode & "' AND SERIAL_NO=1"
        Else
            SqlStr = " INSERT INTO FIN_SUPP_CUST_BUSINESS_MST ( " & vbCrLf _
                    & " COMPANY_CODE, SERIAL_NO, SUPP_CUST_CODE, LOCATION_ID, " & vbCrLf _
                    & " SUPP_CUST_NAME, SUPP_CUST_ADDR, SUPP_CUST_CITY, " & vbCrLf _
                    & " SUPP_CUST_STATE, SUPP_CUST_PIN, SUPP_CUST_PHONE, " & vbCrLf _
                    & " SUPP_CUST_FAXNO, SUPP_CUST_MAILID, SUPP_CUST_MOBILE, COUNTRY," & vbCrLf _
                    & " WITHIN_STATE, WITHIN_DISTT, WITHIN_COUNTRY, " & vbCrLf _
                    & " VENDOR_CODE, GST_RGN_NO, ALIAS_NAME" & vbCrLf _
                    & " ) "

            SqlStr = SqlStr & vbCrLf _
                        & " VALUES ( " & vbCrLf _
                        & " " & pCompanyCode & ", 1, '" & pAccountCode & "', '" & txtCity.Text & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(txtName.Text) & "', '" & MainClass.AllowSingleQuote(txtaddress.Text) & "', '" & MainClass.AllowSingleQuote(txtCity.Text) & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(txtState.Text) & "', '" & MainClass.AllowSingleQuote(txtPinCode.Text) & "', '" & MainClass.AllowSingleQuote(txtPhone.Text) & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(txtFax.Text) & "', '" & MainClass.AllowSingleQuote(txtEmail.Text) & "', '" & MainClass.AllowSingleQuote(txtMobile.Text) & "', '" & MainClass.AllowSingleQuote(txtCountry.Text) & "'," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(pWithInState) & "', '" & MainClass.AllowSingleQuote(pWithinDistt) & "', '" & MainClass.AllowSingleQuote(pWithinCountry) & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(txtVendorCode.Text) & "','" & MainClass.AllowSingleQuote(txtGSTRegnNo.Text) & "','" & MainClass.AllowSingleQuote(txtAlias.Text) & "'" & vbCrLf _
                        & " ) "
        End If
        PubDBCn.Execute(SqlStr)

        If RsCompany.Fields("COMPANY_CODE").Value = pCompanyCode Then
            SqlStr = " UPDATE FIN_SUPP_CUST_BUSINESS_MST SET LOC_DISTANCE= " & Val(txtDistance.Text) & "" & vbCrLf _
                   & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                   & " AND SUPP_CUST_CODE = '" & pAccountCode & "' AND SERIAL_NO=1"

            PubDBCn.Execute(SqlStr)
        End If

        With SprdMain
            I = 0
            For j = 1 To .MaxRows - 1
                .Row = j
                I = I + 1

                .Col = ColLocation
                pLocationID = MainClass.AllowSingleQuote(.Text)

                pAccountName = MainClass.AllowSingleQuote(txtName.Text)

                .Col = ColAddress
                pAddress = MainClass.AllowSingleQuote(.Text)

                .Col = ColCity
                mCityName = MainClass.AllowSingleQuote(.Text)

                .Col = ColState
                pStateName = MainClass.AllowSingleQuote(.Text)

                .Col = ColPin
                pPinCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColDistance
                pLocDistance = Val(.Text)

                .Col = ColAlias
                pAlias = MainClass.AllowSingleQuote(.Text)

                .Col = ColContactNo
                pMobileNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColeMailID
                pMailId = MainClass.AllowSingleQuote(.Text)

                .Col = ColGSTNo
                pGSTNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColWithinDistrict
                pWithinDistt = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                .Col = ColWithinState
                pWithInState = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                .Col = ColWithinCountry
                pWithinCountry = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")


                pCountry = MainClass.AllowSingleQuote(txtCountry.Text)

                pPhoneNo = ""
                pFaxNo = ""
                'pMailId = ""
                'pMobileNo = ""

                pVendorCode = MainClass.AllowSingleQuote(txtVendorCode.Text)

                SqlStr = "SELECT * FROM FIN_SUPP_CUST_BUSINESS_MST " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf _
                    & " And  SUPP_CUST_CODE='" & pAccountCode & "' AND SERIAL_NO=" & j + 1 & ""

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then
                    SqlStr = " UPDATE FIN_SUPP_CUST_BUSINESS_MST SET " & vbCrLf _
                            & " SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(pAccountName) & "', " & vbCrLf _
                            & " SUPP_CUST_ADDR='" & MainClass.AllowSingleQuote(pAddress) & "', " & vbCrLf _
                            & " SUPP_CUST_CITY='" & MainClass.AllowSingleQuote(mCityName) & "', " & vbCrLf _
                            & " SUPP_CUST_STATE='" & MainClass.AllowSingleQuote(pStateName) & "', " & vbCrLf _
                            & " SUPP_CUST_PIN='" & MainClass.AllowSingleQuote(pPinCode) & "', " & vbCrLf _
                            & " SUPP_CUST_PHONE='" & MainClass.AllowSingleQuote(pPhoneNo) & "', " & vbCrLf _
                            & " SUPP_CUST_FAXNO='" & MainClass.AllowSingleQuote(pFaxNo) & "', " & vbCrLf _
                            & " SUPP_CUST_MAILID='" & MainClass.AllowSingleQuote(pMailId) & "', " & vbCrLf _
                            & " SUPP_CUST_MOBILE='" & MainClass.AllowSingleQuote(pMobileNo) & "', COUNTRY='" & MainClass.AllowSingleQuote(pCountry) & "'," & vbCrLf _
                            & " WITHIN_STATE='" & MainClass.AllowSingleQuote(pWithInState) & "', " & vbCrLf _
                            & " WITHIN_DISTT='" & MainClass.AllowSingleQuote(pWithinDistt) & "'," & vbCrLf _
                            & " WITHIN_COUNTRY='" & MainClass.AllowSingleQuote(pWithinCountry) & "', " & vbCrLf _
                            & " VENDOR_CODE='" & MainClass.AllowSingleQuote(pVendorCode) & "', GST_RGN_NO='" & MainClass.AllowSingleQuote(pGSTNo) & "', " & vbCrLf _
                            & " ALIAS_NAME='" & MainClass.AllowSingleQuote(pAlias) & "'" & vbCrLf _
                            & " WHERE COMPANY_CODE=" & pCompanyCode & "" & vbCrLf _
                            & " And  SUPP_CUST_CODE='" & pAccountCode & "' AND SERIAL_NO=" & j + 1 & ""

                    PubDBCn.Execute(SqlStr)

                Else
                    If mCityName <> "" Then
                        SqlStr = " INSERT INTO FIN_SUPP_CUST_BUSINESS_MST ( " & vbCrLf _
                            & " COMPANY_CODE, SERIAL_NO, SUPP_CUST_CODE, LOCATION_ID, " & vbCrLf _
                            & " SUPP_CUST_NAME, SUPP_CUST_ADDR, SUPP_CUST_CITY, " & vbCrLf _
                            & " SUPP_CUST_STATE, SUPP_CUST_PIN, SUPP_CUST_PHONE, " & vbCrLf _
                            & " SUPP_CUST_FAXNO, SUPP_CUST_MAILID, SUPP_CUST_MOBILE, COUNTRY," & vbCrLf _
                            & " WITHIN_STATE, WITHIN_DISTT, WITHIN_COUNTRY, " & vbCrLf _
                            & " VENDOR_CODE, GST_RGN_NO, ALIAS_NAME" & vbCrLf _
                            & " ) "

                        SqlStr = SqlStr & vbCrLf _
                            & " VALUES ( " & vbCrLf _
                            & " " & pCompanyCode & ", " & j + 1 & ", '" & pAccountCode & "', '" & pLocationID & "'," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(pAccountName) & "', '" & MainClass.AllowSingleQuote(pAddress) & "', '" & MainClass.AllowSingleQuote(mCityName) & "'," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(pStateName) & "', '" & MainClass.AllowSingleQuote(pPinCode) & "', '" & MainClass.AllowSingleQuote(pPhoneNo) & "'," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(pFaxNo) & "', '" & MainClass.AllowSingleQuote(pMailId) & "', '" & MainClass.AllowSingleQuote(pMobileNo) & "', '" & MainClass.AllowSingleQuote(pCountry) & "'," & vbCrLf _
                            & "'" & MainClass.AllowSingleQuote(pWithInState) & "', '" & MainClass.AllowSingleQuote(pWithinDistt) & "', '" & MainClass.AllowSingleQuote(pWithinCountry) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(pVendorCode) & "', '" & MainClass.AllowSingleQuote(pGSTNo) & "','" & MainClass.AllowSingleQuote(pAlias) & "'" & vbCrLf _
                            & " ) "

                        PubDBCn.Execute(SqlStr)
                    End If
                End If


                If RsCompany.Fields("COMPANY_CODE").Value = pCompanyCode Then
                    SqlStr = " UPDATE FIN_SUPP_CUST_BUSINESS_MST SET LOC_DISTANCE=" & Val(pLocDistance) & "" & vbCrLf _
                           & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                           & " AND SUPP_CUST_CODE = '" & pAccountCode & "' AND SERIAL_NO=" & j + 1 & ""

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With

        UpdateDetail1 = True

        Exit Function
UpdateDetail1Err:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume						
    End Function
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
        MainClass.ButtonStatus(Me, XRIGHT, RsACM, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Public Sub frmAcm_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = "Select * From FIN_SUPP_CUST_MST WHERE 1<>1 Order by SUPP_CUST_NAME"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACM, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From FIN_SUPP_CUST_BUSINESS_MST WHERE 1<>1 Order by SUPP_CUST_NAME"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACMOthers, ADODB.LockTypeEnum.adLockReadOnly)

        If lblMasterType.Text = "S" Then
            Me.Text = "Supplier Master"
        ElseIf lblMasterType.Text = "C" Then
            Me.Text = "Customer Master"
        Else
            Me.Text = "Accounts Master"
        End If


        '    SqlStr = "Select * From OpOuts Where 1<>1"					
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsOpOuts, adLockReadOnly					

        FillComboBox()
        Call AssignGrid(False)

        SetTextLengths()
        Clear1()
        If CmdAdd.Enabled = True Then cmdAdd_Click(CmdAdd, New System.EventArgs())
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

        SqlStr = " SELECT ACM.SUPP_CUST_CODE AS CODE, ACM.SUPP_CUST_NAME AS NAME, " & vbCrLf _
            & " PAIDDAY, ACM.SUPP_CUST_ADDR AS ADDRESS, ACM.SUPP_CUST_CITY AS CITY, " & vbCrLf _
            & " ACM.SUPP_CUST_STATE AS STATE, ACM.SUPP_CUST_PIN as PINCODE, " & vbCrLf _
            & " ACM.SUPP_CUST_PHONE AS PHONE, " & vbCrLf _
            & " ACCOUNT_CODE, DECODE(CST_NO,NULL,' ',CST_NO) AS CST_NO, DECODE(LST_NO,NULL,' ',LST_NO) AS LST_NO, DECODE(PAN_NO,NULL,' ',PAN_NO) AS PAN_NO, " & vbCrLf _
            & " EXCISE_DIV, EXCISE_RANGE, CENT_EXC_RGN_NO, ECC_NO, ACCOUNT_CODE AS TIN_NO, GST_RGN_NO, GST_REGD, RESPONSIBLE_PERSON, IS_SECURITY_DEPOSIT, SECURITY_AMOUNT, CREDIT_LIMIT," & vbCrLf _
            & " FIN_GROUP_MST.GROUP_NAME" & vbCrLf _
            & " FROM FIN_SUPP_CUST_MST ACM, FIN_GROUP_MST " & vbCrLf _
            & " WHERE ACM.COMPANY_CODE=FIN_GROUP_MST.COMPANY_CODE(+) " & vbCrLf _
            & " AND ACM.GROUPCODE=FIN_GROUP_MST.GROUP_CODE(+) " & vbCrLf _
            & " AND ACM.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If lblMasterType.Text = "S" Then
            SqlStr = SqlStr & " AND ACM.SUPP_CUST_TYPE='S'"
        ElseIf lblMasterType.Text = "C" Then
            SqlStr = SqlStr & " AND ACM.SUPP_CUST_TYPE='C'"
        ElseIf lblMasterType.Text = "C" Then
            SqlStr = SqlStr & " AND ACM.SUPP_CUST_TYPE NOT IN ('S','C')"
        End If

        SqlStr = SqlStr & " ORDER BY ACM.SUPP_CUST_NAME"



        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmAcm_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmAcm_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection					
        ''PvtDBCn.Open StrConn					
        Call SetMainFormCordinate(Me)



        xMyMenu = myMenu
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, xMyMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        '    FillComboBox	

        FillComboBox()
        SSTInfo.SelectedIndex = 0

        '    MainClass.Init Me					

        ResizeForm.FindAllControls(Me)

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
        Dim SqlStr As String = ""
        mAccountCode = CStr(-1)
        txtName.Text = ""
        txtCode.Text = ""
        txtAlias.Text = ""
        txtCompanyName.Text = ""
        txtVendorCode.Text = ""
        txtShortName.Text = ""

        txtLenderBank.Text = ""
        txtCode.Enabled = True
        txtName.Enabled = True
        txtCurrencyCode.Text = ""
        txtCountryCode.Text = ""
        txtCurrencyCode.Enabled = True
        txtCountryCode.Enabled = True
        txtPayment.Text = ""
        lblPaymentTerms.Text = ""

        lblRemarks.Text = IIf(RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104, "Additional Address :", "Remarks :")
        '' lblRemarks.Font.SizeInPoints = 8.25

        'txtName.Appearance.FontData.SizeInPoints = 8.5
        '= "Segoe UI Semibold, 8.25pt"

        'FillComboBox()

        If lblMasterType.Text = "Accounts" Then
            cboCategory.SelectedIndex = 0
            cboCategory.Enabled = True

            cboPaymentMode.SelectedIndex = 0
            cboPaymentMode.Enabled = True

            FraView.Visible = False
            Frame8.Visible = False
            Frame5.Visible = False
            Frame3.Visible = False
            chkGroupLimit.Visible = False
        Else
            Call SetCombo(cboCategory, (lblMasterType.Text))
            cboCategory.Enabled = IIf(PubSuperUser = "S", True, False)
            cboPaymentMode.Enabled = False

            FraView.Visible = True
            Frame8.Visible = True
            Frame5.Visible = True
            Frame3.Visible = True
        End If

        cboSupplierType.SelectedIndex = 0
        cboCustGroup.SelectedIndex = -1

        txtUdyogAahaarNo.Text = ""
        cboSymbol.SelectedIndex = -1
        cboEnterpriseType.SelectedIndex = -1

        cboHeadType.SelectedIndex = 0
        txtGroupName.Text = ""
        txtGroupNameCr.Text = ""
        txtaddress.Text = ""
        txtResponsiblePerson.Text = ""
        txtBankAccountNo.Text = ""
        txtSwitCode.Text = ""
        txtBankBranch.Text = ""
        txtIFSCCode.Text = ""
        txtBankName.Text = ""

        txtSecurityChqNo.Text = ""



        txtBankAccountNo.Enabled = True
        txtIFSCCode.Enabled = True
        txtSwitCode.Enabled = True
        txtBankName.Enabled = True
        txtBankBranch.Enabled = True

        txtCity.Text = ""
        txtPinCode.Text = ""
        txtDistance.Text = ""
        txtCreditLimit.Text = ""
        txtState.Text = ""
        txtPhone.Text = ""
        txtFax.Text = ""
        txtEmail.Text = ""
        txtMobile.Text = ""
        txtPolicyNo.Text = ""
        txtLSTNo.Text = ""
        txtCstNo.Text = ""
        txtPan.Text = ""
        txtEmpCode.Text = ""

        txtDivision.Text = ""
        txtRange.Text = ""
        txtRegnNo.Text = ""
        txtECCNo.Text = ""

        txtGSTRegnNo.Text = ""
        optGSTRegd(1).Checked = True
        optGSTClassification(0).Checked = True

        txtGSTRegnNo.Enabled = True
        FraGSTClass.Enabled = True
        FraGSTStatus.Enabled = True

        txtCommRate.Text = ""
        optRegd(0).Checked = True
        txtContact.Text = ""
        CboTDSCategory.SelectedIndex = -1
        txtTDSPer.Text = ""
        txtSTDSPer.Text = ""
        txtESIPer.Text = ""
        txtSeq.Text = ""
        txtReceiptDays.Text = ""
        txtCurrency.Text = "RS"
        txtChqFrequency.Text = ""

        txtCountry.Text = "INDIA"
        txtBuyerName.Text = ""
        txtCarriage.Text = ""
        txtLoadingPort.Text = ""
        txtDischargePort.Text = ""
        txtFinalDest.Text = ""
        txtExportPaymetTerms.Text = ""

        chkState.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkInterUnit.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkDistt.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkCountry.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkAuthorised.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkGroupLimit.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkSEZ.CheckState = System.Windows.Forms.CheckState.Unchecked

        chkTDSDeduct.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkTDSNotDeduct.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkRtnDeclaration.CheckState = System.Windows.Forms.CheckState.Unchecked

        If InStr(1, XRIGHT, "S") = 0 Then
            chkAuthorised.Enabled = False
        Else
            chkAuthorised.Enabled = True
        End If

        lblAddUser.Text = ""
        lblAddDate.Text = ""
        lblModUser.Text = ""
        lblModDate.Text = ""

        optBalMethod(0).Checked = True


        txtRemarks.Text = ""
        OptStatus(0).Checked = True

        chkTCSApplicable.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkTCSNotApplicable.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkPlaceofSupply.CheckState = System.Windows.Forms.CheckState.Unchecked

        chkSecurityChq.CheckState = System.Windows.Forms.CheckState.Unchecked
        txtSecurityAmount.Text = "0.00"



        If PubSuperUser = "S" Or PubSuperUser = "A" Then
            chkTCSApplicable.Enabled = True
            ChkPoRate.Enabled = True
        Else
            ChkPoRate.Enabled = False
            chkTCSApplicable.Enabled = IIf(chkTCSApplicable.CheckState = System.Windows.Forms.CheckState.Unchecked, True, False)
        End If


        ChkPoRate.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkMonthWiseLdgr.CheckState = System.Windows.Forms.CheckState.Unchecked

        chkAccountHide.CheckState = System.Windows.Forms.CheckState.Unchecked

        chkAccountHide.Visible = IIf(PubUserID = "G0416", True, False)

        chkSMERegd.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkSMEStatus.CheckState = System.Windows.Forms.CheckState.Unchecked

        txtPaidDay.Text = CStr(3)
        txtPaidDay2.Text = ""
        txtPaidDay3.Text = ""
        txtPaidDay4.Text = ""

        txtTINNo.Text = ""
        txtSrvRegnNo.Text = ""
        SSTInfo.SelectedIndex = 0

        txtServProvided.Text = ""
        txtSection.Text = ""
        txtExptionCNo.Text = ""
        cboCType.SelectedIndex = 0
        txtLDCertiNo.Text = ""
        chkLowerDeduction.CheckState = System.Windows.Forms.CheckState.Unchecked

        chkStopMRR.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkStopInvoice.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkStopGP.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkStopBP.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkStopPO.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkStopMRR.Enabled = True
        chkStopInvoice.Enabled = True
        chkStopGP.Enabled = True
        chkStopBP.Enabled = True
        chkStopPO.Enabled = True
        txtChqFrequency.Enabled = True
        txtPayment.Enabled = True
        cmdPaySearch.Enabled = True

        txtGUID.Text = ""
        cboNature.SelectedIndex = 0

        FraStatus.Enabled = True

        If lblMasterType.Text = "Accounts" Then
            SqlStr = "SUPP_CUST_TYPE NOT IN ('S','C')"
            optBalMethod(0).Checked = True
        ElseIf lblMasterType.Text = "S" Then
            SqlStr = "SUPP_CUST_TYPE='S'"
            optBalMethod(1).Checked = True
        ElseIf lblMasterType.Text = "C" Then
            SqlStr = "SUPP_CUST_TYPE='C'"
            optBalMethod(1).Checked = True
        End If

        'Call AutoCompleteSearch("FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", SqlStr, txtName)
        'Call AutoCompleteSearch("FIN_SUPP_CUST_MST", "SUPP_CUST_CODE", SqlStr, txtCode)

        Call AutoCompleteSearch("FIN_GROUP_MST", "GROUP_NAME", "GROUP_CATEGORY='G'", txtGroupName)
        Call AutoCompleteSearch("FIN_GROUP_MST", "GROUP_NAME", "GROUP_CATEGORY='G'", txtGroupNameCr)
        Call AutoCompleteSearch("PAY_EMPLOYEE_MST", "EMP_NAME", "", txtEmpCode)

        Call AutoCompleteSearch("FIN_PAYTERM_MST", "PAY_TERM_CODE", "", txtPayment)
        Call AutoCompleteSearch("FIN_CURRENCY_MST", "CURR_DESC", "", txtCurrency)
        Call AutoCompleteSearch("TDS_SECTION_MST", "NAME", "", txtSection)

        Call AutoCompleteSearch("GEN_CITY_MST", "CITY_NAME", "", txtCity)
        Call AutoCompleteSearch("GEN_STATE_MST", "NAME", "", txtState)

        Call AutoCompleteSearch("FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_TYPE IN ('S','C')", txtBuyerName)

        Call AutoCompleteSearch("FIN_SERVPROV_MST", "NAME", "", txtServProvided)
        Call AutoCompleteSearch("FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_TYPE='2'", txtLenderBank)

        Call AutoCompleteCompanySearch(txtCompanyName)
        txtCompanyName.Enabled = False

        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)

        MainClass.ButtonStatus(Me, XRIGHT, RsACM, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume					
    End Sub
    Private Sub cboNature_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboNature.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboNature_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboNature.SelectedIndexChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 300)

            .set_ColWidth(0, 500)
            .set_ColWidth(1, 900)
            .set_ColWidth(2, 3500)
            .set_ColWidth(3, 500)
            .set_ColWidth(4, 3500)
            .set_ColWidth(5, 1500)
            .set_ColWidth(6, 1500)
            .set_ColWidth(7, 1500)
            .set_ColWidth(8, 1500)

            .set_ColWidth(9, 1500)
            .set_ColWidth(10, 2000)
            .set_ColWidth(11, 2000)
            .set_ColWidth(12, 2000)
            .set_ColWidth(13, 2000)
            .set_ColWidth(14, 2000)
            .set_ColWidth(15, 2000)
            .set_ColWidth(16, 2000)

            .set_ColWidth(17, 2000)
            .set_ColWidth(18, 3500)

            .ColsFrozen = 2
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub SetTextLengths()

        On Error GoTo ERR1

        txtLenderBank.MaxLength = RsACM.Fields("SUPP_CUST_NAME").DefinedSize
        txtName.MaxLength = RsACM.Fields("SUPP_CUST_NAME").DefinedSize ''					
        txtCode.MaxLength = RsACM.Fields("SUPP_CUST_CODE").DefinedSize ''					
        txtGUID.MaxLength = RsACM.Fields("GROUP_UID").DefinedSize
        txtGroupName.MaxLength = MainClass.SetMaxLength("GROUP_NAME", "FIN_GROUP_MST", PubDBCn)
        txtGroupNameCr.MaxLength = MainClass.SetMaxLength("GROUP_NAME", "FIN_GROUP_MST", PubDBCn)
        txtaddress.MaxLength = RsACM.Fields("SUPP_CUST_ADDR").DefinedSize ''	

        txtResponsiblePerson.MaxLength = RsACM.Fields("RESPONSIBLE_PERSON").DefinedSize ''	
        txtCity.MaxLength = RsACM.Fields("SUPP_CUST_CITY").DefinedSize ''					
        txtPinCode.MaxLength = RsACM.Fields("SUPP_CUST_PIN").DefinedSize ''					
        txtDistance.MaxLength = RsACM.Fields("LOC_DISTANCE").Precision
        txtCreditLimit.MaxLength = RsACM.Fields("CREDIT_LIMIT").Precision
        txtState.MaxLength = RsACM.Fields("SUPP_CUST_STATE").DefinedSize ''					
        txtPhone.MaxLength = RsACM.Fields("SUPP_CUST_PHONE").DefinedSize ''					
        txtFax.MaxLength = RsACM.Fields("SUPP_CUST_FAXNO").DefinedSize ''					
        txtEmail.MaxLength = RsACM.Fields("SUPP_CUST_MAILID").DefinedSize ''					
        txtMobile.MaxLength = RsACM.Fields("SUPP_CUST_MOBILE").DefinedSize ''					
        txtPolicyNo.MaxLength = RsACM.Fields("SUPP_CUST_POLICYNO").DefinedSize
        txtAlias.MaxLength = RsACM.Fields("ALIAS_NAME").DefinedSize
        txtVendorCode.MaxLength = RsACM.Fields("VENDOR_CODE").DefinedSize
        txtShortName.MaxLength = RsACM.Fields("SUPP_CUST_SHORT_NAME").DefinedSize

        txtCompanyName.MaxLength = RsCompany.Fields("COMPANY_NAME").DefinedSize
        txtCurrencyCode.MaxLength = RsACM.Fields("CURRENCY_CODE").DefinedSize
        txtCountryCode.MaxLength = RsACM.Fields("COUNTRY_CODE").DefinedSize

        txtBankAccountNo.MaxLength = RsACM.Fields("CUST_BANK_ACCT_NO").DefinedSize
        txtSwitCode.MaxLength = RsACM.Fields("BANK_SWIFT_CODE").DefinedSize
        txtBankBranch.MaxLength = RsACM.Fields("BANK_BRANCH_NAME").DefinedSize
        txtIFSCCode.MaxLength = RsACM.Fields("BANK_IFSC_CODE").DefinedSize
        txtBankName.MaxLength = RsACM.Fields("CUST_BANK_BANK").DefinedSize

        txtSecurityChqNo.MaxLength = RsACM.Fields("SECURITY_CHEQUE_NO").DefinedSize


        txtLSTNo.MaxLength = RsACM.Fields("LST_NO").DefinedSize ''					
        txtCstNo.MaxLength = RsACM.Fields("CST_NO").DefinedSize ''					
        txtPan.MaxLength = 10 'RsACM.Fields("PAN_NO").DefinedSize           ''					
        txtDivision.MaxLength = RsACM.Fields("EXCISE_DIV").DefinedSize ''					
        txtRange.MaxLength = RsACM.Fields("EXCISE_RANGE").DefinedSize ''					
        txtRegnNo.MaxLength = RsACM.Fields("CENT_EXC_RGN_NO").DefinedSize ''					

        txtGSTRegnNo.MaxLength = RsACM.Fields("GST_RGN_NO").DefinedSize

        txtECCNo.MaxLength = RsACM.Fields("ECC_NO").DefinedSize ''					

        txtCommRate.MaxLength = RsACM.Fields("COMMISIONER_RATE").DefinedSize ''					
        txtContact.MaxLength = RsACM.Fields("CONTACT_TELNO").DefinedSize ''					
        txtTDSPer.MaxLength = RsACM.Fields("TDS_PER").Precision ''					
        txtSTDSPer.MaxLength = RsACM.Fields("STDS_PER").Precision
        txtESIPer.MaxLength = RsACM.Fields("ESI_PER").Precision
        txtRemarks.MaxLength = RsACM.Fields("SUPP_CUST_REMARKS").DefinedSize ''					
        txtTINNo.MaxLength = RsACM.Fields("ACCOUNT_CODE").DefinedSize
        txtSrvRegnNo.MaxLength = RsACM.Fields("SRV_REGN_NO").DefinedSize
        txtSeq.MaxLength = RsACM.Fields("DSP_RPT_SEQ").DefinedSize
        txtReceiptDays.MaxLength = RsACM.Fields("RECEIPT_DAYS").Precision

        txtCurrency.MaxLength = RsACM.Fields("CurrencyName").DefinedSize
        txtEmpCode.MaxLength = RsACM.Fields("EMP_CODE").DefinedSize
        txtChqFrequency.MaxLength = 1

        'txtPurchaseSTRecd.MaxLength = MainClass.SetMaxLength("NAME", "FIN_STFORM_MST", PubDBCn)
        'txtPurchaseSTDue.MaxLength = MainClass.SetMaxLength("NAME", "FIN_STFORM_MST", PubDBCn)
        'txtSaleSTRecd.MaxLength = MainClass.SetMaxLength("NAME", "FIN_STFORM_MST", PubDBCn)
        'txtSaleSTDue.MaxLength = MainClass.SetMaxLength("NAME", "FIN_STFORM_MST", PubDBCn)

        txtCountry.MaxLength = RsACM.Fields("COUNTRY").DefinedSize
        txtExptionCNo.MaxLength = RsACM.Fields("EXPTIONCNO").DefinedSize
        txtLDCertiNo.MaxLength = RsACM.Fields("LOWER_DED_CERT_NO").DefinedSize
        txtBuyerName.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
        txtCarriage.MaxLength = RsACM.Fields("CARRIAGE").DefinedSize
        txtLoadingPort.MaxLength = RsACM.Fields("LOADINGPORT").DefinedSize
        txtDischargePort.MaxLength = RsACM.Fields("DISCHARGEPORT").DefinedSize
        txtFinalDest.MaxLength = RsACM.Fields("FINALDEST").DefinedSize
        txtExportPaymetTerms.MaxLength = RsACM.Fields("PAYMENTTERMS").DefinedSize
        txtServProvided.MaxLength = MainClass.SetMaxLength("NAME", "FIN_SERVPROV_MST", PubDBCn)

        txtPayment.MaxLength = RsACM.Fields("PAYMENT_CODE").DefinedSize

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume					
    End Sub

    Function FieldVarification() As Boolean

        On Error GoTo err_Renamed
        Dim mSqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mAccountUser As String

        FieldVarification = True



        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldVarification = False
            Exit Function
        End If
        If txtName.Text = "" Then
            MsgInformation("Account Name is empty. Cannot Save")
            txtName.Focus()
            FieldVarification = False
            Exit Function
        End If

        If txtCode.Text = "" Then
            MsgInformation("Account Code is empty. Cannot Save")
            If txtCode.Enabled Then txtCode.Focus()
            FieldVarification = False
            Exit Function
        End If

        'If Val(txtCode.Text) = 0 Then
        '    MsgInformation("Invalid Account Code. Cannot Save")
        '    If txtCode.Enabled Then txtCode.Focus()
        '    FieldVarification = False
        '    Exit Function
        'End If

        If Len(Trim(txtCode.Text)) <> 5 And ADDMode = True Then
            MsgInformation("Account Code Must be Five Digit. Cannot Save")
            If txtCode.Enabled Then txtCode.Focus()
            FieldVarification = False
            Exit Function
        End If

        If txtLenderBank.Text <> "" Then
            If MainClass.ValidateWithMasterTable(txtLenderBank.Text, "SUPP_CUST_NAME", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='2'") = False Then
                MsgInformation("Invaild Lender Bank Name. Cannot Save")
                txtLenderBank.Focus()
                FieldVarification = False
                Exit Function
            End If
        End If

        If MODIFYMode = True Then
            If IsDate(lblAddDate.Text) = False Then
                lblAddDate.Text = RsCompany.Fields("Start_Date").Value
            End If
            If MainClass.GetUserCanModifyMaster(lblAddDate.Text, XRIGHT) = False Then
                MsgBox("You Have Not Rights to change back Entry.", vbInformation)
                FieldVarification = False
                Exit Function
            End If
        End If

        If PubSuperUser = "S" Then
        Else
            If chkAuthorised.CheckState = System.Windows.Forms.CheckState.Checked And chkAuthorised.Enabled = False Then
                MsgInformation("Account Master is Authorised, so can't be change.")
                FieldVarification = False
                Exit Function
            End If
        End If

        If Trim(txtGSTRegnNo.Text) = "" And optGSTRegd(0).Checked = True Then
            MsgBox("Please enter the GST Regn No.", MsgBoxStyle.Information)
            FieldVarification = False
            Exit Function
        End If

        If Trim(txtGSTRegnNo.Text) <> "" And optGSTRegd(1).Checked = True Then
            MsgBox("Please Click in GST Regn", MsgBoxStyle.Information)
            FieldVarification = False
            Exit Function
        End If

        If Trim(txtGSTRegnNo.Text) <> "" Then
            If CheckGSTValidation(Trim(txtGSTRegnNo.Text), Trim(txtState.Text)) = False Then
                MsgBox("Invalid GST Regn No., so that cann't be save.", MsgBoxStyle.Information)
                FieldVarification = False
                Exit Function
            Else
                txtGSTRegnNo.Text = Trim(txtGSTRegnNo.Text)
            End If
        End If

        If chkInterUnit.Checked = True Then
            If txtCompanyName.Text = "" Then
                MsgBox("Please Select Inter Unit Company Name., so that cann't be save.", MsgBoxStyle.Information)
                FieldVarification = False
                Exit Function
            End If

            If MainClass.ValidateWithMasterTable(txtCompanyName.Text, "COMPANY_NAME", "COMPANY_NAME", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = False Then
                MsgInformation("Invalid Inter Unit Company Name")
                If txtCompanyName.Enabled Then txtCompanyName.Focus()
                FieldVarification = False
                Exit Function
            End If

        Else
            txtCompanyName.Text = ""
        End If

        'If ADDMode = True Then
        '    If PubHO = "Y" Then
        '        If VB.Left(cboCategory.Text, 1) = "C" Then
        '            If Val(txtCode.Text) >= 10001 And Val(txtCode.Text) <= 15000 Then
        '            Else
        '                MsgInformation("Customer Code should be between 10001 and 15000. Cannot Save")
        '                If txtCode.Enabled Then txtCode.Focus()
        '                FieldVarification = False
        '                Exit Function
        '            End If
        '        ElseIf VB.Left(cboCategory.Text, 1) = "S" Then
        '            If Val(txtCode.Text) >= 22001 And Val(txtCode.Text) <= 30000 Then
        '            Else
        '                MsgInformation("Supplier Code should be between 22001 and 30000. Cannot Save")
        '                If txtCode.Enabled Then txtCode.Focus()
        '                FieldVarification = False
        '                Exit Function
        '            End If

        '        ElseIf VB.Left(cboCategory.Text, 1) = "E" Then
        '            If Val(txtCode.Text) >= 31001 And Val(txtCode.Text) <= 35000 Then
        '            Else
        '                MsgInformation("Employee Code should be between 31001 and 35000. Cannot Save")
        '                If txtCode.Enabled Then txtCode.Focus()
        '                FieldVarification = False
        '                Exit Function
        '            End If

        '        Else
        '            If Val(txtCode.Text) >= 16001 And Val(txtCode.Text) <= 22000 Then
        '            Else
        '                MsgInformation("Cash/Bank/Assets/Others Code should be between 16001 and 22000. Cannot Save")
        '                If txtCode.Enabled Then txtCode.Focus()
        '                FieldVarification = False
        '                Exit Function
        '            End If
        '        End If
        '    Else
        '        If VB.Left(cboCategory.Text, 1) = "C" Then
        '            If Val(txtCode.Text) >= 50001 And Val(txtCode.Text) <= 65000 Then
        '            Else
        '                MsgInformation("Customer Code should be between 50001 and 65000. Cannot Save")
        '                If txtCode.Enabled Then txtCode.Focus()
        '                FieldVarification = False
        '                Exit Function
        '            End If
        '        ElseIf VB.Left(cboCategory.Text, 1) = "S" Then
        '            If Val(txtCode.Text) >= 72001 And Val(txtCode.Text) <= 80000 Then
        '            Else
        '                MsgInformation("Supplier Code should be between 72001 and 80000. Cannot Save")
        '                If txtCode.Enabled Then txtCode.Focus()
        '                FieldVarification = False
        '                Exit Function
        '            End If

        '        ElseIf VB.Left(cboCategory.Text, 1) = "E" Then
        '            If Val(txtCode.Text) >= 81001 And Val(txtCode.Text) <= 85000 Then
        '            Else
        '                MsgInformation("Employee Code should be between 81001 and 85000. Cannot Save")
        '                If txtCode.Enabled Then txtCode.Focus()
        '                FieldVarification = False
        '                Exit Function
        '            End If

        '        Else
        '            If Val(txtCode.Text) >= 66001 And Val(txtCode.Text) <= 72000 Then
        '            Else
        '                MsgInformation("Cash/Bank/Assets/Others Code should be between 66001 and 72000. Cannot Save")
        '                If txtCode.Enabled Then txtCode.Focus()
        '                FieldVarification = False
        '                Exit Function
        '            End If
        '        End If
        '    End If
        'End If
        If cboCategory.Text = "" Then
            MsgInformation("Category is must.")
            FieldVarification = False
            cboCategory.Focus()
            Exit Function
        End If

        'mAccountUser = GetUserPermission("ALLOW_ACCOUNT_MASTER", "N", PubUserID, RsCompany.Fields("COMPANY_CODE").Value)

        'If VB.Left(cboCategory.Text, 1) = "S" Or VB.Left(cboCategory.Text, 1) = "C" Then
        '    If mAccountUser = "N" And ADDMode = True Then
        '        MsgInformation("You have no Rights to Open Customer/Supplier Master.")
        '        FieldVarification = False
        '        Exit Function
        '    End If
        'End If

        If Trim(UCase(txtGroupName.Text)) = "" Then
            MsgInformation("Group Can Not Be Blank.")
            txtGroupName.Focus()
            FieldVarification = False
            Exit Function
        End If

        If VB.Left(cboHeadType.Text, 1) = "P" Then
            mSqlStr = "SELECT SUPP_CUST_CODE, SUPP_CUST_NAME FROM FIN_SUPP_CUST_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND HEADTYPE='P' AND SUPP_CUST_CODE<>'" & Trim(txtCode.Text) & "'"
            MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                MsgInformation("Profit & Loss You Already Define for Account Name :(" & IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_CODE").Value), "", RsTemp.Fields("SUPP_CUST_CODE").Value) & ")" & IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value))
                cboHeadType.Focus()
                FieldVarification = False
                Exit Function
            End If
        End If

        If VB.Left(cboHeadType.Text, 1) = "4" And VB.Left(cboCategory.Text, 1) <> "O" Then
            MsgInformation("You can select Service Head for only Account GL.")
            cboHeadType.Focus()
            FieldVarification = False
            Exit Function
        End If

        If VB.Left(cboHeadType.Text, 1) = "5" And VB.Left(lblMasterType.Text, 1) <> "A" Then
            MsgInformation("You can select Salary Head for only Account GL.")
            cboHeadType.Focus()
            FieldVarification = False
            Exit Function
        End If


        '    If Left(cboHeadType.Text, 1) = "G" Then					
        '        mSqlStr = "SELECT SUPP_CUST_CODE, SUPP_CUST_NAME FROM FIN_SUPP_CUST_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND HEADTYPE='G' AND SUPP_CUST_CODE<>'" & Trim(txtCode.Text) & "'"					
        '        MainClass.UOpenRecordSet mSqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly					
        '        If RsTemp.EOF = False Then					
        '            MsgInformation "Gratuity you already define for Account Name :(" & IIf(IsNull(RsTemp!SUPP_CUST_CODE), "", RsTemp!SUPP_CUST_CODE) & ")" & IIf(IsNull(RsTemp!SUPP_CUST_NAME), "", RsTemp!SUPP_CUST_NAME)					
        '            cboHeadType.SetFocus					
        '            FieldVarification = False					
        '            Exit Function					
        '        End If					
        '    End If					

        If (VB.Left(cboCategory.Text, 1) = "S" Or VB.Left(cboCategory.Text, 1) = "C") And RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Then
            If cboSupplierType.Text = "" Then
                MsgInformation("Please Select Valid Supplier Type.")
                FieldVarification = False
                Exit Function
            End If
        End If


        If MainClass.ValidateWithMasterTable(txtGroupName.Text, "GROUP_Name", "GROUP_Code", "FIN_GROUP_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GROUP_STATUS='O'") = False Then
            MsgInformation("Group Not Defined")
            txtGroupName.Focus()
            FieldVarification = False
            Exit Function
        End If


        If Trim(UCase(txtGroupNameCr.Text)) = "" Then
            MsgInformation("Group Can Not Be Blank.")
            txtGroupNameCr.Focus()
            FieldVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable(txtGroupNameCr.Text, "GROUP_Name", "GROUP_Code", "FIN_GROUP_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GROUP_STATUS='O'") = False Then
            MsgInformation("Group Not Defined")
            txtGroupNameCr.Focus()
            FieldVarification = False
            Exit Function
        End If

        If Val(txtTDSPer.Text) > 100 Then
            FieldVarification = False
            MsgBox("TDS % cann't be greater than 100", MsgBoxStyle.Information)
            txtTDSPer.Focus()
            Exit Function
        End If

        If Val(txtSTDSPer.Text) > 100 Then
            FieldVarification = False
            MsgBox("STDS % cann't be greater than 100", MsgBoxStyle.Information)
            txtSTDSPer.Focus()
            Exit Function
        End If

        If Val(txtESIPer.Text) > 100 Then
            FieldVarification = False
            MsgBox("ESI % cann't be greater than 100", MsgBoxStyle.Information)
            txtESIPer.Focus()
            Exit Function
        End If

        If Val(txtPaidDay.Text) > 31 Then
            FieldVarification = False
            MsgBox("Paid Day cann't be greater than 31", MsgBoxStyle.Information)
            txtPaidDay.Focus()
            Exit Function
        End If

        If Val(txtPaidDay2.Text) > 31 Then
            FieldVarification = False
            MsgBox("Paid Day cann't be greater than 31", MsgBoxStyle.Information)
            txtPaidDay2.Focus()
            Exit Function
        End If

        If Val(txtPaidDay3.Text) > 31 Then
            FieldVarification = False
            MsgBox("Paid Day cann't be greater than 31", MsgBoxStyle.Information)
            txtPaidDay3.Focus()
            Exit Function
        End If

        If Val(txtPaidDay4.Text) > 31 Then
            FieldVarification = False
            MsgBox("Paid Day cann't be greater than 31", MsgBoxStyle.Information)
            txtPaidDay4.Focus()
            Exit Function
        End If

        If VB.Left(cboHeadType.Text, 1) = "L" Or VB.Left(cboHeadType.Text, 1) = "I" Or VB.Left(cboHeadType.Text, 1) = "5" Then
            If Trim(txtEmpCode.Text) = "" Then
                MsgInformation("Emp Code Can Not Be Blank.")
                txtEmpCode.Focus()
                FieldVarification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable(txtEmpCode.Text, "EMP_CODE", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                ErrorMsg("Invalid Emp Code.", , MsgBoxStyle.Information)
                txtEmpCode.Focus()
                FieldVarification = False
                Exit Function
            End If
        End If

        If (VB.Left(cboCategory.Text, 1) = "S" Or VB.Left(cboCategory.Text, 1) = "C") And optBalMethod(0).Checked = True Then
            MsgInformation("Please Check Balancing Method.")
            FieldVarification = False
            Exit Function
        End If

        If (VB.Left(cboCategory.Text, 1) = "S" Or VB.Left(cboCategory.Text, 1) = "C") And Trim(txtPayment.Text) = "" Then
            MsgInformation("Please Check Payment Terms.")
            txtPayment.Focus()
            FieldVarification = False
            Exit Function
        End If

        If Trim(txtCurrency.Text) = "" Then
            MsgInformation("Please Enter Currency. Help For Press F1 Key.")
            txtCurrency.Focus()
            FieldVarification = False
            Exit Function
        Else
            If MainClass.ValidateWithMasterTable(txtCurrency.Text, "CURR_DESC", "CURR_DESC", "FIN_CURRENCY_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Please Enter Valid Currency. Help For Press F1 Key.")
                txtCurrency.Focus()
                FieldVarification = False
                Exit Function
            End If
        End If
        If VB.Left(cboCategory.Text, 1) = "S" Or VB.Left(cboCategory.Text, 1) = "C" Then
            If Trim(txtaddress.Text) = "" Then
                MsgInformation("Please Enter Address.")
                FieldVarification = False
                Exit Function
            End If

            If Trim(txtCity.Text) = "" Then
                MsgInformation("Please Enter City.")
                FieldVarification = False
                Exit Function
            End If

            If Trim(txtState.Text) = "" Then
                MsgInformation("Please Enter State.")
                FieldVarification = False
                Exit Function
            End If
        Else
            txtaddress.Text = "-"
            txtCity.Text = "-"
            txtState.Text = "-"
            txtPinCode.Text = "-"
        End If

        Dim mCheckStateCode As String = ""
        Dim mStateCode As String = ""
        If VB.Left(cboCategory.Text, 1) = "S" Or VB.Left(cboCategory.Text, 1) = "C" Then
            If MainClass.ValidateWithMasterTable(txtCity.Text, "CITY_NAME", "STATE_CODE", "GEN_CITY_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invalid City Name")
                SSTInfo.SelectedIndex = 0
                If txtCity.Enabled = True Then txtCity.Focus()
                FieldVarification = False
                Exit Function
            Else
                mCheckStateCode = MasterNo
            End If

            If MainClass.ValidateWithMasterTable(txtState.Text, "NAME", "CODE", "GEN_STATE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invalid State Name")
                SSTInfo.SelectedIndex = 0
                If txtState.Enabled = True Then txtState.Focus()
                FieldVarification = False
                Exit Function
            Else
                mStateCode = MasterNo
            End If

            If mStateCode <> mCheckStateCode Then
                MsgInformation("Invalid State Name for Such City")
                SSTInfo.SelectedIndex = 0
                If txtState.Enabled = True Then txtState.Focus()
                FieldVarification = False
                Exit Function
            End If

            If Trim(UCase(RsCompany.Fields("COMPANY_STATE").Value)) = UCase(Trim(txtState.Text)) And chkState.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                MsgInformation("Please Select Within State.")
                chkState.Focus()
                FieldVarification = False
                Exit Function
            End If

            If chkLowerDeduction.CheckState = System.Windows.Forms.CheckState.Checked And Trim(txtLDCertiNo.Text) = "" Then
                MsgInformation("Please Enter the Lower Deduction Certificate No.")
                txtLDCertiNo.Focus()
                FieldVarification = False
                Exit Function
            End If

            If Trim(UCase(RsCompany.Fields("COMPANY_STATE").Value)) <> UCase(Trim(txtState.Text)) And chkState.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("Not In Within State.")
                chkState.Focus()
                FieldVarification = False
                Exit Function
            End If

            If Trim(UCase(RsCompany.Fields("COMPANY_STATE").Value)) <> UCase(Trim(txtState.Text)) And chkDistt.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("Not In Within Distt.")
                chkDistt.Focus()
                FieldVarification = False
                Exit Function
            End If

            If chkMonthWiseLdgr.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("You Cann't Select Month wise Ledger For Supplier or Customer.")
                chkMonthWiseLdgr.Focus()
                FieldVarification = False
                Exit Function
            End If
        End If

        'If Trim(txtPinCode.Text) = "" Then
        '    MsgInformation("Please Enter Pin Code.")
        '    FieldVarification = False
        '    Exit Function
        'End If

        'If Trim(txtCountry.Text) = "" Then
        '    MsgInformation("Please Enter Country Name.")
        '    FieldVarification = False
        '    Exit Function
        'End If

        If VB.Left(cboCategory.Text, 1) = "S" Or VB.Left(cboCategory.Text, 1) = "C" Then

            If Val(txtPinCode.Text) = 0 Then
                MsgInformation("Please Enter Pin Code.")
                FieldVarification = False
                Exit Function
            End If

            If chkCountry.CheckState = System.Windows.Forms.CheckState.Checked Then
                If Len(txtPinCode.Text) <> 6 Then
                    MsgInformation("Please Enter the corect Pin Code.")
                    FieldVarification = False
                    Exit Function
                End If
            End If


            'If Val(txtDistance.Text) = 0 Then
            '    MsgInformation("Please Enter party location distance from our Premises.")
            '    FieldVarification = False
            '    Exit Function
            'End If

            If Trim(txtState.Text) = RsCompany.Fields("COMPANY_STATE").Value And chkState.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                MsgInformation("Please select the Within State.")
                FieldVarification = False
                Exit Function
            ElseIf Trim(txtState.Text) <> RsCompany.Fields("COMPANY_STATE").Value And chkState.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("Please unselect the Within State.")
                FieldVarification = False
                Exit Function
            End If

            If Trim(txtCountry.Text) = "INDIA" And chkCountry.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                MsgInformation("Please select the Within Country.")
                FieldVarification = False
                Exit Function
            ElseIf Trim(txtCountry.Text) <> "INDIA" And chkCountry.CheckState = System.Windows.Forms.CheckState.Checked Then
                MsgInformation("Please unselect the Within Country.")
                FieldVarification = False
                Exit Function
            End If
            '					
            If Trim(txtPan.Text) = IIf(IsDBNull(RsCompany.Fields("PAN_NO").Value), "", RsCompany.Fields("PAN_NO").Value) And chkInterUnit.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                If MsgQuestion("PAN No is same with our company but you not select the Inter Unit, want to continue ? ") = CStr(MsgBoxResult.No) Then
                    FieldVarification = False
                    Exit Function
                End If
            End If
        End If



        If Trim(txtBuyerName.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(txtBuyerName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = False Then
                MsgInformation("Invalid Buyer.")
                FieldVarification = False
                Exit Function
            End If
        End If
        'If VB.Left(cboCategory.Text, 1) = "C" Then
        '    If Trim(cboCustGroup.Text) = "" Then
        '        MsgInformation("Please Enter the Customer Group.")
        '        FieldVarification = False
        '        cboCustGroup.Focus()
        '        Exit Function
        '    End If
        'End If

        If (VB.Left(cboCategory.Text, 1) = "S" Or VB.Left(cboCategory.Text, 1) = "C") And chkCountry.CheckState = System.Windows.Forms.CheckState.Checked Then
            If cboSupplierType.Text = "CONTRACTOR" Then
                If Trim(txtPan.Text) = "" Then
                    MsgInformation("PAN NO Cann't be Blank.")
                    FieldVarification = False
                    txtPan.Focus()
                    Exit Function
                End If
            Else
                '            If optRegd(0).Value = True Then					
                '                If Trim(txtLSTNo.Text) = "" Then					
                '                    MsgInformation "LST No Cann't be Blank."					
                '                    FieldVarification = False					
                '                    txtLSTNo.SetFocus					
                '                    Exit Function					
                '                End If					
                '					
                '                If Trim(txtCstNo.Text) = "" Then					
                '                    MsgInformation "CST No Cann't be Blank."					
                '                    FieldVarification = False					
                '                    txtCstNo.SetFocus					
                '                    Exit Function					
                '                End If					
                '					
                '					
                '                If Trim(txtTINNo.Text) = "" Or Val(txtTINNo.Text) = 0 Then					
                '                    MsgInformation "TIN No Cann't be Blank."					
                '                    FieldVarification = False					
                '                    txtTINNo.SetFocus					
                '                    Exit Function					
                '                End If					
                '					
                '                If Len(txtTINNo.Text) < 10 Or Len(txtTINNo.Text) > 11 Then					
                '                    MsgInformation "TIN No. must be 10 Or 11 Digit."					
                '                    FieldVarification = False					
                '                    txtTINNo.SetFocus					
                '                    Exit Function					
                '                End If					
                '					
                '                If Trim(txtRegnNo.Text) = "" Then					
                '                    MsgInformation "Excise Regn. No Cann't be Blank."					
                '                    FieldVarification = False					
                '                    txtRegnNo.SetFocus					
                '                    Exit Function					
                '                End If					
                '            End If					
            End If
            If Trim(txtPan.Text) <> "" Then
                If CheckPANValidation((txtPan.Text)) = False Then
                    MsgInformation("Invalid PAN No.")
                    FieldVarification = False
                    txtPan.Focus()
                    Exit Function
                End If
            End If
        End If

        mCheckStateCode = ""
        mStateCode = ""
        Dim CntRow As Long
        Dim mWithinState As String = ""
        Dim mWithinDistrict As String = ""
        Dim mWithinCountry As String = ""
        Dim mStateName As String = ""

        If VB.Left(cboCategory.Text, 1) = "S" Or VB.Left(cboCategory.Text, 1) = "C" Then
            With SprdMain
                For CntRow = 1 To .MaxRows - 1
                    .Row = CntRow
                    .Col = ColCity

                    If MainClass.ValidateWithMasterTable(.Text, "CITY_NAME", "STATE_CODE", "GEN_CITY_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                        MsgInformation("Invalid City Name")
                        SSTInfo.SelectedIndex = 1
                        If SprdMain.Enabled = True Then SprdMain.Focus()
                        FieldVarification = False
                        Exit Function
                    Else
                        mCheckStateCode = MasterNo
                    End If

                    .Col = ColState
                    mStateName = Trim(.Text)
                    If MainClass.ValidateWithMasterTable(.Text, "NAME", "CODE", "GEN_STATE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                        MsgInformation("Invalid State Name")
                        SSTInfo.SelectedIndex = 1
                        If SprdMain.Enabled = True Then SprdMain.Focus()
                        FieldVarification = False
                        Exit Function
                    Else
                        mStateCode = MasterNo
                    End If

                    If mStateCode <> mCheckStateCode Then
                        MsgInformation("Invalid State Name for Such City")
                        SSTInfo.SelectedIndex = 1
                        If SprdMain.Enabled = True Then SprdMain.Focus()
                        FieldVarification = False
                        Exit Function
                    End If

                    .Col = ColWithinDistrict
                    mWithinDistrict = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                    .Col = ColWithinState
                    mWithinState = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")


                    '.Col = ColState
                    'If Trim(UCase(RsCompany.Fields("COMPANY_STATE").Value)) = UCase(Trim(.Text)) And mWithinState = "N" Then
                    '    MsgInformation("Please Select Within State.")
                    '    If SprdMain.Enabled = True Then SprdMain.Focus()
                    '    FieldVarification = False
                    '    Exit Function
                    'End If

                    'If Trim(UCase(RsCompany.Fields("COMPANY_STATE").Value)) <> UCase(Trim(.Text)) And mWithinState = "Y" Then
                    '    MsgInformation("Not In Within State.")
                    '    If SprdMain.Enabled = True Then SprdMain.Focus()
                    '    FieldVarification = False
                    '    Exit Function
                    'End If

                    'If Trim(UCase(RsCompany.Fields("COMPANY_STATE").Value)) <> UCase(Trim(.Text)) And mWithinDistrict = "Y" Then
                    '    MsgInformation("Not In Within Distt.")
                    '    If SprdMain.Enabled = True Then SprdMain.Focus()
                    '    FieldVarification = False
                    '    Exit Function
                    'End If

                    .Col = ColPin
                    If Trim(.Text) = "" Then
                        MsgInformation("Please Enter Pin Code.")
                        FieldVarification = False
                        Exit Function
                    End If

                    If chkCountry.CheckState = System.Windows.Forms.CheckState.Checked Then
                        If Len(.Text) <> 6 Then
                            MsgInformation("Please Enter the corect Pin Code.")
                            FieldVarification = False
                            Exit Function
                        End If
                    End If

                    '.Col = ColDistance
                    'If Val(.Text) = 0 Then
                    '    MsgInformation("Please Enter party location distance from our Premises.")
                    '    FieldVarification = False
                    '    Exit Function
                    'End If

                    .Col = ColWithinCountry
                    mWithinCountry = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                    If Trim(txtCountry.Text) = "INDIA" And mWithinCountry = "N" Then
                        MsgInformation("Please select the Within Country.")
                        FieldVarification = False
                        Exit Function
                    ElseIf Trim(txtCountry.Text) <> "INDIA" And mWithinCountry = "Y" Then
                        MsgInformation("Please unselect the Within Country.")
                        FieldVarification = False
                        Exit Function
                    End If

                    .Col = ColGSTNo
                    If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
                    Else
                        If Trim(.Text) = "" And optGSTRegd(0).Checked = True Then
                            MsgBox("Please enter the GST Regn No.", MsgBoxStyle.Information)
                            FieldVarification = False
                            Exit Function
                        End If

                        If Trim(.Text) <> "" And optGSTRegd(0).Checked = False Then
                            MsgBox("Please Click in GST Regn", MsgBoxStyle.Information)
                            FieldVarification = False
                            Exit Function
                        End If
                    End If


                    If Trim(.Text) <> "" Then
                        If CheckGSTValidation(Trim(.Text), mStateName) = False Then
                            MsgBox("Invalid GST Regn No., so that cann't be save.", MsgBoxStyle.Information)
                            FieldVarification = False
                            Exit Function
                        Else
                            .Text = Trim(.Text)
                        End If
                    End If
                Next
            End With
        End If
        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function

    Private Sub frmAcm_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        ResizeForm.ResizeAllControls(Me)   '    MainClass.FormResize Me					
    End Sub

    Private Sub frmAcm_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        '    'If PvtDBCn.State = adStateOpen Then					
        '					
        '        ''PvtDBCn.Close					
        '        ''Set PvtDBCn = Nothing					
        '    End If					
        Me.Hide() ''me.hide 	
        Me.Close()
        RsACM.Close()
        'RsOpOuts.Close					
    End Sub

    Private Sub optBalMethod_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optBalMethod.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optBalMethod.GetIndex(eventSender)

            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub optGSTClassification_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optGSTClassification.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optGSTClassification.GetIndex(eventSender)

            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub optGSTRegd_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optGSTRegd.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optGSTRegd.GetIndex(eventSender)

            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub optRegd_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optRegd.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optRegd.GetIndex(eventSender)

            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub OptStatus_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptStatus.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptStatus.GetIndex(eventSender)

            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub txtAlias_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAlias.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAlias_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAlias.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtAlias.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCompanyName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCompanyName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCompanyName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCompanyName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCompanyName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtBankAccountNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBankAccountNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBankAccountNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBankAccountNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtBankAccountNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtBankBranch_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBankBranch.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBankBranch_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBankBranch.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtBankBranch.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtBankName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBankName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBankName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBankName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtBankName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSecurityChqNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSecurityChqNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSecurityChqNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSecurityChqNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSecurityChqNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtBuyerName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBuyerName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtBuyerName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBuyerName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtBuyerName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtBuyerName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtBuyerName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
    End Sub

    Private Sub txtBuyerName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBuyerName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtBuyerName.Text) = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.ValidateWithMasterTable(txtBuyerName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Invalid Buyer.", vbInformation)
            Cancel = True
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCarriage_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCarriage.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCarriage_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCarriage.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCarriage.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtChqFrequency_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtChqFrequency.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtChqFrequency_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtChqFrequency.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCountry_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCountry.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCountry_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCountry.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCountry.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCurrency_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCurrency.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCurrency.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCurrency_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCurrency.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
    End Sub


    Private Sub txtCurrency_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCurrency.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtCurrency.Text) = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable(txtCurrency.Text, "CURR_DESC", "CURR_DESC", "FIN_CURRENCY_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Invalid Currency.", vbInformation)
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDischargePort_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDischargePort.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDischargePort_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDischargePort.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDischargePort.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDistance_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDistance.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDistance_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDistance.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCreditLimit_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCreditLimit.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCreditLimit_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCreditLimit.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtExptionCNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtExptionCNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtExptionCNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtFinalDest_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFinalDest.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtFinalDest_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFinalDest.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtFinalDest.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtGSTRegnNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGSTRegnNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtGSTRegnNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtGSTRegnNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtGSTRegnNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtIFSCCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtIFSCCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtIFSCCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtIFSCCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtIFSCCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtLDCertiNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLDCertiNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLDCertiNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLDCertiNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtLDCertiNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtLOADINGPORT_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLoadingPort.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLOADINGPORT_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLoadingPort.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtLoadingPort.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPaidDay2_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPaidDay2.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPaidDay2_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPaidDay2.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPaidDay3_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPaidDay3.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPaidDay3_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPaidDay3.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSecurityAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSecurityAmount.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSecurityAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSecurityAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPaidDay4_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPaidDay4.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPaidDay4_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPaidDay4.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtExportPaymetTerms_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtExportPaymetTerms.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtExportPaymetTerms_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtExportPaymetTerms.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtExportPaymetTerms.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPayment_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPayment.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPolicyNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPolicyNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPolicyNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPolicyNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPolicyNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        If SprdView.ActiveRow < 1 Then Exit Sub

        SprdView.Col = 2
        SprdView.Row = SprdView.ActiveRow
        txtName.Text = Trim(SprdView.Text)
        TxtName_Validating(txtName, New System.ComponentModel.CancelEventArgs(True))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub
    Private Sub txtaddress_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtaddress.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtaddress_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtaddress.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtaddress.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtResponsiblePerson_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtResponsiblePerson.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtResponsiblePerson_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtResponsiblePerson.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtResponsiblePerson.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub SearchResponsiblePerson()
        On Error GoTo SearchError

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
            If MainClass.SearchMaster(txtResponsiblePerson.Text, "FIN_SALESPERSON_MST", "NAME", "") = True Then
                txtResponsiblePerson.Text = AcName
                txtResponsiblePerson_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
            End If
        Else
            If MainClass.SearchMaster(txtResponsiblePerson.Text, "PAY_EMPLOYEE_MST", "EMP_NAME", "") = True Then
                txtResponsiblePerson.Text = AcName
                txtResponsiblePerson_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))
            End If
        End If

        Exit Sub
SearchError:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtResponsiblePerson_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtResponsiblePerson.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchResponsiblePerson()
    End Sub
    Private Sub txtResponsiblePerson_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtResponsiblePerson.DoubleClick
        SearchResponsiblePerson()
    End Sub
    Private Sub txtResponsiblePerson_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtResponsiblePerson.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim Sqlstr As String
        Dim RsTemp As ADODB.Recordset

        Sqlstr = ""
        If Trim(txtResponsiblePerson.Text) = "" Then GoTo EventExitSub

        If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 104 Or RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
            Sqlstr = "SELECT * FROM FIN_SALESPERSON_MST " & vbCrLf _
                & " WHERE NAME='" & MainClass.AllowSingleQuote(UCase((Trim(txtResponsiblePerson.Text)))) & "'"
        Else
            Sqlstr = "SELECT * FROM PAY_EMPLOYEE_MST " & vbCrLf _
               & " WHERE EMP_NAME='" & MainClass.AllowSingleQuote(UCase((Trim(txtResponsiblePerson.Text)))) & "'"
        End If

        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = True Then
            MsgBox("Sale Person name is not In Master.")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCode.Text)  '' MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
        FillComboCode()
    End Sub
    Private Sub FillComboName()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        Dim RS As ADODB.Recordset
        Dim CntLst As Long

        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet

        oledbCnn = New OleDbConnection(StrConn)

        oledbCnn.Open()

        SqlStr = "Select DISTINCT SUPP_CUST_NAME, SUPP_CUST_CODE, SUPP_CUST_ADDR, SUPP_CUST_CITY, SUPP_CUST_STATE, PAN_NO, GST_RGN_NO  " & vbCrLf _
            & " FROM FIN_SUPP_CUST_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If lblMasterType.Text = "Accounts" Then
            SqlStr = SqlStr & " AND SUPP_CUST_TYPE NOT IN ('S','C')"
        ElseIf lblMasterType.Text = "S" Then
            SqlStr = SqlStr & " AND SUPP_CUST_TYPE='S'"
        ElseIf lblMasterType.Text = "C" Then
            SqlStr = SqlStr & " AND SUPP_CUST_TYPE='C'"
        End If

        If Trim(txtName.Text) <> "" Then
            SqlStr = SqlStr & " AND SUPP_CUST_NAME Like '%" & txtName.Text & "%'"
        End If

        SqlStr = SqlStr & " ORDER BY SUPP_CUST_NAME"

        SqlStr = SqlStr & vbCrLf & " FETCH FIRST 500 ROWS ONLY"

        oledbAdapter = New OleDbDataAdapter(SqlStr, oledbCnn)
        oledbAdapter.Fill(ds)

        ' Set the data source and data member to bind the grid.
        txtName.DataSource = ds
        txtName.DataMember = ""
        'cmbCompany.ValueMember = "COMPANY_CODE"
        'cmbCompany.DisplayMember = "Company Name"

        txtName.Appearance.FontData.SizeInPoints = 8.5

        txtName.DisplayLayout.Bands(0).Columns(0).Header.Caption = "Name"
        txtName.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Code"
        txtName.DisplayLayout.Bands(0).Columns(2).Header.Caption = "Address"
        txtName.DisplayLayout.Bands(0).Columns(3).Header.Caption = "City"
        txtName.DisplayLayout.Bands(0).Columns(4).Header.Caption = "State"
        txtName.DisplayLayout.Bands(0).Columns(5).Header.Caption = "PAN No"
        txtName.DisplayLayout.Bands(0).Columns(6).Header.Caption = "GST No"


        txtName.DisplayLayout.Bands(0).Columns(0).Width = 350
        txtName.DisplayLayout.Bands(0).Columns(1).Width = 100
        txtName.DisplayLayout.Bands(0).Columns(2).Width = 150
        txtName.DisplayLayout.Bands(0).Columns(3).Width = 80
        txtName.DisplayLayout.Bands(0).Columns(4).Width = 80
        txtName.DisplayLayout.Bands(0).Columns(5).Width = 80
        txtName.DisplayLayout.Bands(0).Columns(6).Width = 80

        txtName.DisplayLayout.Appearance.FontData.SizeInPoints = 8.5


        txtName.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown ''List       '' Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown

        oledbAdapter.Dispose()
        oledbCnn.Close()

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub FillComboCode()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        Dim RS As ADODB.Recordset
        Dim CntLst As Long

        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet

        oledbCnn = New OleDbConnection(StrConn)

        oledbCnn.Open()

        SqlStr = "Select DISTINCT SUPP_CUST_CODE, SUPP_CUST_NAME, SUPP_CUST_ADDR, SUPP_CUST_CITY, SUPP_CUST_STATE, PAN_NO, GST_RGN_NO  " & vbCrLf _
                 & " FROM FIN_SUPP_CUST_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If lblMasterType.Text = "Accounts" Then
            SqlStr = SqlStr & " AND SUPP_CUST_TYPE NOT IN ('S','C')"
        ElseIf lblMasterType.Text = "S" Then
            SqlStr = SqlStr & " AND SUPP_CUST_TYPE='S'"
        ElseIf lblMasterType.Text = "C" Then
            SqlStr = SqlStr & " AND SUPP_CUST_TYPE='C'"
        End If

        If Trim(txtCode.Text) <> "" Then
            SqlStr = SqlStr & " AND SUPP_CUST_CODE Like '%" & txtCode.Text & "%'"
        End If

        SqlStr = SqlStr & " ORDER BY SUPP_CUST_CODE DESC"

        SqlStr = SqlStr & vbCrLf & " FETCH FIRST 500 ROWS ONLY"

        oledbAdapter = New OleDbDataAdapter(SqlStr, oledbCnn)
        oledbAdapter.Fill(ds)

        ' Set the data source and data member to bind the grid.
        txtCode.DataSource = ds
        txtCode.DataMember = ""
        'cmbCompany.ValueMember = "COMPANY_CODE"
        'cmbCompany.DisplayMember = "Company Name"

        txtCode.Appearance.FontData.SizeInPoints = 8.5

        txtCode.DisplayLayout.Bands(0).Columns(0).Header.Caption = "Code"
        txtCode.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Name"
        txtCode.DisplayLayout.Bands(0).Columns(2).Header.Caption = "Address"
        txtCode.DisplayLayout.Bands(0).Columns(3).Header.Caption = "City"
        txtCode.DisplayLayout.Bands(0).Columns(4).Header.Caption = "State"
        txtCode.DisplayLayout.Bands(0).Columns(5).Header.Caption = "PAN No"
        txtCode.DisplayLayout.Bands(0).Columns(6).Header.Caption = "GST No"


        txtCode.DisplayLayout.Bands(0).Columns(0).Width = 100
        txtCode.DisplayLayout.Bands(0).Columns(1).Width = 35
        txtCode.DisplayLayout.Bands(0).Columns(2).Width = 150
        txtCode.DisplayLayout.Bands(0).Columns(3).Width = 80
        txtCode.DisplayLayout.Bands(0).Columns(4).Width = 80
        txtCode.DisplayLayout.Bands(0).Columns(5).Width = 80
        txtCode.DisplayLayout.Bands(0).Columns(6).Width = 80

        txtCode.DisplayLayout.Appearance.FontData.SizeInPoints = 8.5


        txtCode.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown ''List       '' Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown

        oledbAdapter.Dispose()
        oledbCnn.Close()

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub TxtCity_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCity.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCity_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCity.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCity.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchCode()
    End Sub
    Private Sub SearchCode()
        On Error GoTo ErrPart
        Dim Sqlstr As String
        Sqlstr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If lblMasterType.Text = "Accounts" Then
            Sqlstr = Sqlstr & " AND SUPP_CUST_TYPE NOT IN ('S','C')"
        ElseIf lblMasterType.Text = "S" Then
            Sqlstr = Sqlstr & " AND SUPP_CUST_TYPE='S'"
        ElseIf lblMasterType.Text = "C" Then
            Sqlstr = Sqlstr & " AND SUPP_CUST_TYPE='C'"
        End If

        If MainClass.SearchGridMaster(txtCode.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_CODE", "SUPP_CUST_NAME", , , Sqlstr) = True Then
            txtCode.Text = AcName
            txtCode_Validating(txtCode, New System.ComponentModel.CancelEventArgs(False))       ''_Validate False
            If txtCode.Enabled = True Then txtCode.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, Err.Number, vbCritical)
    End Sub
    Public Sub txtCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtCode.Text) = "" Then GoTo EventExitSub
        If MODIFYMode = True And RsACM.EOF = False Then mAccountCode = RsACM.Fields("SUPP_CUST_CODE").Value
        SqlStr = "Select * From FIN_SUPP_CUST_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACM, ADODB.LockTypeEnum.adLockReadOnly)

        If RsACM.EOF = False Then
            If lblMasterType.Text = "Accounts" Then
                If RsACM.Fields("SUPP_CUST_TYPE").Value = "S" Or RsACM.Fields("SUPP_CUST_TYPE").Value = "C" Then
                    MsgInformation("Supplier Or Customer you cann't be Select here.")
                    Cancel = True
                    GoTo EventExitSub
                End If
            ElseIf lblMasterType.Text = "S" Then
                If RsACM.Fields("SUPP_CUST_TYPE").Value <> "S" Then
                    MsgInformation("Please select Supplier Only.")
                    Cancel = True
                    GoTo EventExitSub
                End If
            ElseIf lblMasterType.Text = "C" Then
                If RsACM.Fields("SUPP_CUST_TYPE").Value <> "C" Then
                    MsgInformation("Please select Customer Only.")
                    Cancel = True
                    GoTo EventExitSub
                End If
            End If

            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Name Does Not Exist In Master, Click Add To Add In Master", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * From FIN_SUPP_CUST_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & mAccountCode & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACM, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCommRate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCommRate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCommRate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCommRate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCommRate.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtContact_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtContact.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtContact_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtContact.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtContact.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtCSTNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCstNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtCSTNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCstNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCstNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCurrency_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCurrency.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDivision.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDivision_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDivision.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDivision.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtECCNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtECCNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtECCNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtECCNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtECCNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEmail_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmail.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtEmail_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEmail.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        '    KeyAscii = MainClass.UpperCase(KeyAscii, txtEmail.Text)					
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEmpCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtEmpCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEmpCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtFax.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEmpCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtEmpCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
    End Sub

    Private Sub txtEmpCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmpCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtEmpCode.Text) = "" Then GoTo EventExitSub

        txtEmpCode.Text = VB6.Format(txtEmpCode.Text, "000000")

        If MainClass.ValidateWithMasterTable(txtEmpCode.Text, "EMP_CODE", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            ErrorMsg("Invalid Emp Code.", , vbInformation)
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtESIPer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtESIPer.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtESIPer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtESIPer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtExptionCNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtExptionCNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtFax_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFax.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtFax_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFax.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtFax.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtGroupName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGroupName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub TxtGroupName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtGroupName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtGroupName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtGroupName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtGroupName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchHead_Click(cmdSearchHead, New System.ComponentModel.CancelEventArgs(False))
    End Sub

    Private Sub txtGroupName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtGroupName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If Trim(txtName.Text) = "" Then GoTo EventExitSub
        If Trim(txtGroupName.Text) = "" Then GoTo EventExitSub
        If Trim(txtGroupName.Text) = "" Then
            ErrorMsg("Group Cann't be Blank.", , MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

        If MainClass.ValidateWithMasterTable(txtGroupName.Text, "GROUP_Name", "GROUP_Code", "FIN_GROUP_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GROUP_STATUS='O'") = False Then
            ErrorMsg("Invalid Group.", , vbInformation)
            Cancel = True
        End If

        If Trim(txtGroupNameCr.Text) = "" Then
            txtGroupNameCr.Text = Trim(txtGroupName.Text)
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtGroupNameCr_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGroupNameCr.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtGroupNameCr_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtGroupNameCr.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtGroupNameCr.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtGroupNameCr_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtGroupNameCr.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchHeadCr_Click(cmdSearchHead, New System.ComponentModel.CancelEventArgs(False))
    End Sub

    Private Sub txtGroupNameCr_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtGroupNameCr.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtName.Text) = "" Then GoTo EventExitSub
        If Trim(txtGroupNameCr.Text) = "" Then GoTo EventExitSub

        If Trim(txtGroupNameCr.Text) = "" Then
            ErrorMsg("Group Cann't be Blank.", , MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If

        If MainClass.ValidateWithMasterTable(txtGroupNameCr.Text, "GROUP_Name", "GROUP_Code", "FIN_GROUP_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GROUP_STATUS='O'") = False Then
            ErrorMsg("Invalid Group.", , vbInformation)
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub TxtLSTNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLSTNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtLSTNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLSTNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtLSTNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtMobile_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMobile.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMobile_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMobile.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtMobile.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub TxtName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
        FillComboName()
    End Sub

    Private Sub TxtName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchAccounts()
    End Sub

    Private Sub TxtName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtName.Text) = "" Then GoTo EventExitSub
        If MODIFYMode = True And RsACM.EOF = False Then mAccountCode = RsACM.Fields("SUPP_CUST_CODE").Value

        If ADDMode = True Then
            txtName.Text = Trim(UCase(txtName.Text))
            txtName.Text = Replace(txtName.Text, vbCrLf, "")
        End If

        SqlStr = "Select * From FIN_SUPP_CUST_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND LTRIM(RTRIM(SUPP_CUST_NAME))='" & MainClass.AllowSingleQuote(txtName.Text) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACM, ADODB.LockTypeEnum.adLockReadOnly)

        If RsACM.EOF = False Then
            If lblMasterType.Text = "Accounts" Then
                If RsACM.Fields("SUPP_CUST_TYPE").Value = "S" Or RsACM.Fields("SUPP_CUST_TYPE").Value = "C" Then
                    MsgInformation("Supplier Or Customer you cann't be Select here.")
                    Cancel = True
                    GoTo EventExitSub
                End If
            ElseIf lblMasterType.Text = "S" Then
                If RsACM.Fields("SUPP_CUST_TYPE").Value <> "S" Then
                    MsgInformation("Please select Supplier Only.")
                    Cancel = True
                    GoTo EventExitSub
                End If
            ElseIf lblMasterType.Text = "C" Then
                If RsACM.Fields("SUPP_CUST_TYPE").Value <> "C" Then
                    MsgInformation("Please select Customer Only.")
                    Cancel = True
                    GoTo EventExitSub
                End If
            End If
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Name Does Not Exist In Master, Click Add To Add In Master", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * From FIN_SUPP_CUST_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & mAccountCode & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACM, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim mBalOpt As Integer
        Dim mControlBranchCode As Integer
        Dim mBuyerCode As String = ""
        Dim mServiceProviderCode As Double
        Dim mIsAuthorisedUser As String = ""
        Dim mEnterpriseType As String = ""
        Dim mSMESymbol As String = ""
        Dim mLenderBankCode As String = ""
        Dim mAcctCode As String = ""
        Dim mInterUnitCompanyCode As Integer

        Clear1()
        If Not RsACM.EOF Then


            mAccountCode = IIf(IsDBNull(RsACM.Fields("SUPP_CUST_CODE").Value), -1, RsACM.Fields("SUPP_CUST_CODE").Value)

            txtName.Text = Trim(IIf(IsDBNull(RsACM.Fields("SUPP_CUST_NAME").Value), "", RsACM.Fields("SUPP_CUST_NAME").Value))

            txtCode.Text = Trim(IIf(IsDBNull(RsACM.Fields("SUPP_CUST_CODE").Value), "", RsACM.Fields("SUPP_CUST_CODE").Value))

            txtGUID.Text = Trim(IIf(IsDBNull(RsACM.Fields("GROUP_UID").Value), "", RsACM.Fields("GROUP_UID").Value))



            txtEmpCode.Text = Trim(IIf(IsDBNull(RsACM.Fields("EMP_CODE").Value), "", RsACM.Fields("EMP_CODE").Value))

            txtAlias.Text = Trim(IIf(IsDBNull(RsACM.Fields("ALIAS_NAME").Value), "", RsACM.Fields("ALIAS_NAME").Value))

            txtVendorCode.Text = Trim(IIf(IsDBNull(RsACM.Fields("VENDOR_CODE").Value), "", RsACM.Fields("VENDOR_CODE").Value))
            txtShortName.Text = Trim(IIf(IsDBNull(RsACM.Fields("SUPP_CUST_SHORT_NAME").Value), "", RsACM.Fields("SUPP_CUST_SHORT_NAME").Value))


            txtCurrencyCode.Text = Trim(IIf(IsDBNull(RsACM.Fields("CURRENCY_CODE").Value), "", RsACM.Fields("CURRENCY_CODE").Value))

            txtCountryCode.Text = Trim(IIf(IsDBNull(RsACM.Fields("COUNTRY_CODE").Value), "", RsACM.Fields("COUNTRY_CODE").Value))

            mLenderBankCode = Trim(IIf(IsDBNull(RsACM.Fields("LENDER_BANK_CODE").Value), "", RsACM.Fields("LENDER_BANK_CODE").Value))
            If MainClass.ValidateWithMasterTable(mLenderBankCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtLenderBank.Text = MasterNo
            Else
                txtLenderBank.Text = ""
            End If

            txtCode.Enabled = False
            If CheckTransactionMade(Trim(txtCode.Text), "A") = True Then
                txtName.Enabled = If(PubSuperUser = "S", True, False)
            Else
                txtName.Enabled = True
            End If



            mAcctCode = IIf(IsDBNull(RsACM.Fields("GROUPCODE").Value), "", RsACM.Fields("GROUPCODE").Value)

            If mAcctCode = "" Then
                txtGroupName.Text = ""
            ElseIf MainClass.ValidateWithMasterTable(mAcctCode, "GROUP_CODE", "GROUP_NAME", "FIN_GROUP_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtGroupName.Text = MasterNo
            End If

            mAcctCode = IIf(IsDBNull(RsACM.Fields("GROUPCODECR").Value), "", RsACM.Fields("GROUPCODECR").Value)

            If mAcctCode = "" Then
                txtGroupNameCr.Text = ""
            ElseIf MainClass.ValidateWithMasterTable(mAcctCode, "GROUP_CODE", "GROUP_NAME", "FIN_GROUP_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtGroupNameCr.Text = MasterNo
            End If


            txtaddress.Text = IIf(IsDBNull(RsACM.Fields("SUPP_CUST_ADDR").Value), "", RsACM.Fields("SUPP_CUST_ADDR").Value)
            txtResponsiblePerson.Text = IIf(IsDBNull(RsACM.Fields("RESPONSIBLE_PERSON").Value), "", RsACM.Fields("RESPONSIBLE_PERSON").Value)

            txtBankAccountNo.Text = IIf(IsDBNull(RsACM.Fields("CUST_BANK_ACCT_NO").Value), "", RsACM.Fields("CUST_BANK_ACCT_NO").Value)

            txtSwitCode.Text = IIf(IsDBNull(RsACM.Fields("BANK_SWIFT_CODE").Value), "", RsACM.Fields("BANK_SWIFT_CODE").Value)

            txtBankBranch.Text = IIf(IsDBNull(RsACM.Fields("BANK_BRANCH_NAME").Value), "", RsACM.Fields("BANK_BRANCH_NAME").Value)

            txtIFSCCode.Text = IIf(IsDBNull(RsACM.Fields("BANK_IFSC_CODE").Value), "", RsACM.Fields("BANK_IFSC_CODE").Value)

            txtBankName.Text = IIf(IsDBNull(RsACM.Fields("CUST_BANK_BANK").Value), "", RsACM.Fields("CUST_BANK_BANK").Value)

            txtSecurityChqNo.Text = IIf(IsDBNull(RsACM.Fields("SECURITY_CHEQUE_NO").Value), "", RsACM.Fields("SECURITY_CHEQUE_NO").Value)




            txtBankAccountNo.Enabled = IIf(Trim(txtBankAccountNo.Text) = "", True, IIf(PubSuperUser = "S", True, False))
            txtIFSCCode.Enabled = IIf(Trim(txtIFSCCode.Text) = "", True, IIf(PubSuperUser = "S", True, False))
            txtSwitCode.Enabled = IIf(Trim(txtSwitCode.Text) = "", True, IIf(PubSuperUser = "S", True, False))
            txtBankName.Enabled = IIf(Trim(txtBankName.Text) = "", True, IIf(PubSuperUser = "S", True, False))
            txtBankBranch.Enabled = IIf(Trim(txtBankBranch.Text) = "", True, IIf(PubSuperUser = "S", True, False))


            txtCity.Text = IIf(IsDBNull(RsACM.Fields("SUPP_CUST_CITY").Value), "", RsACM.Fields("SUPP_CUST_CITY").Value)

            txtPinCode.Text = IIf(IsDBNull(RsACM.Fields("SUPP_CUST_PIN").Value), "", RsACM.Fields("SUPP_CUST_PIN").Value)

            txtDistance.Text = IIf(IsDBNull(RsACM.Fields("LOC_DISTANCE").Value), "", RsACM.Fields("LOC_DISTANCE").Value)
            txtCreditLimit.Text = IIf(IsDBNull(RsACM.Fields("CREDIT_LIMIT").Value), "", RsACM.Fields("CREDIT_LIMIT").Value)

            txtState.Text = IIf(IsDBNull(RsACM.Fields("SUPP_CUST_STATE").Value), "", RsACM.Fields("SUPP_CUST_STATE").Value)

            txtPhone.Text = IIf(IsDBNull(RsACM.Fields("SUPP_CUST_PHONE").Value), "", RsACM.Fields("SUPP_CUST_PHONE").Value)

            txtFax.Text = IIf(IsDBNull(RsACM.Fields("SUPP_CUST_FAXNO").Value), "", RsACM.Fields("SUPP_CUST_FAXNO").Value)

            txtEmail.Text = IIf(IsDBNull(RsACM.Fields("SUPP_CUST_MAILID").Value), "", RsACM.Fields("SUPP_CUST_MAILID").Value)

            txtMobile.Text = IIf(IsDBNull(RsACM.Fields("SUPP_CUST_MOBILE").Value), "", RsACM.Fields("SUPP_CUST_MOBILE").Value)


            txtPolicyNo.Text = IIf(IsDBNull(RsACM.Fields("SUPP_CUST_POLICYNO").Value), "", RsACM.Fields("SUPP_CUST_POLICYNO").Value)


            txtLSTNo.Text = IIf(IsDBNull(RsACM.Fields("LST_NO").Value), "", RsACM.Fields("LST_NO").Value)

            txtCstNo.Text = IIf(IsDBNull(RsACM.Fields("CST_NO").Value), "", RsACM.Fields("CST_NO").Value)

            txtPan.Text = IIf(IsDBNull(RsACM.Fields("PAN_NO").Value), "", RsACM.Fields("PAN_NO").Value)

            txtDivision.Text = IIf(IsDBNull(RsACM.Fields("EXCISE_DIV").Value), "", RsACM.Fields("EXCISE_DIV").Value)

            txtRange.Text = IIf(IsDBNull(RsACM.Fields("EXCISE_RANGE").Value), "", RsACM.Fields("EXCISE_RANGE").Value)

            txtRegnNo.Text = IIf(IsDBNull(RsACM.Fields("CENT_EXC_RGN_NO").Value), "", RsACM.Fields("CENT_EXC_RGN_NO").Value)


            txtGSTRegnNo.Text = IIf(IsDBNull(RsACM.Fields("GST_RGN_NO").Value), "", RsACM.Fields("GST_RGN_NO").Value)

            If RsACM.Fields("GST_REGD").Value = "Y" Then
                optGSTRegd(0).Checked = True
            ElseIf RsACM.Fields("GST_REGD").Value = "N" Then
                optGSTRegd(1).Checked = True
            ElseIf RsACM.Fields("GST_REGD").Value = "E" Then
                optGSTRegd(2).Checked = True
            ElseIf RsACM.Fields("GST_REGD").Value = "F" Then
                optGSTRegd(3).Checked = True
            ElseIf RsACM.Fields("GST_REGD").Value = "C" Then
                optGSTRegd(4).Checked = True
            End If

            If RsACM.Fields("GST_CLASSIFICATION").Value = "F" Then
                optGSTClassification(0).Checked = True
            Else
                optGSTClassification(1).Checked = True
            End If

            If PubSuperUser <> "S" Then
                If Trim(txtGSTRegnNo.Text) <> "" Then
                    txtGSTRegnNo.Enabled = False
                    FraGSTClass.Enabled = False
                    FraGSTStatus.Enabled = False
                End If
            End If

            txtECCNo.Text = IIf(IsDBNull(RsACM.Fields("ECC_NO").Value), "", RsACM.Fields("ECC_NO").Value)


            txtCommRate.Text = IIf(IsDBNull(RsACM.Fields("COMMISIONER_RATE").Value), "", RsACM.Fields("COMMISIONER_RATE").Value)

            If RsACM.Fields("REGD_DEALER").Value = "Y" Then
                optRegd(0).Checked = True
            Else
                optRegd(1).Checked = True
            End If


            txtContact.Text = IIf(IsDBNull(RsACM.Fields("CONTACT_TELNO").Value), "", RsACM.Fields("CONTACT_TELNO").Value)

            CboTDSCategory.Text = IIf(IsDBNull(RsACM.Fields("TDSCATEGORY").Value), "NONE", RsACM.Fields("TDSCATEGORY").Value)


            txtCurrency.Text = IIf(IsDBNull(RsACM.Fields("CURRENCYNAME").Value), "Rs.", RsACM.Fields("CURRENCYNAME").Value)


            If IsDBNull(RsACM.Fields("TYPE_OF_SUPPLIER").Value) Then

            Else
                cboSupplierType.Text = IIf(IsDBNull(RsACM.Fields("TYPE_OF_SUPPLIER").Value), "", RsACM.Fields("TYPE_OF_SUPPLIER").Value)
            End If


            If IsDBNull(RsACM.Fields("SUPP_CUST_NATURE").Value) Then

            Else
                cboNature.Text = IIf(IsDBNull(RsACM.Fields("SUPP_CUST_NATURE").Value), "", RsACM.Fields("SUPP_CUST_NATURE").Value)
            End If


            If IsDBNull(RsACM.Fields("CUSTOMER_GROUP").Value) Then

            Else
                cboCustGroup.Text = IIf(IsDBNull(RsACM.Fields("CUSTOMER_GROUP").Value), "", RsACM.Fields("CUSTOMER_GROUP").Value)
            End If


            txtTDSPer.Text = VB6.Format(IIf(IsDBNull(RsACM.Fields("TDS_PER").Value), 0, RsACM.Fields("TDS_PER").Value), "0.000")

            txtSTDSPer.Text = VB6.Format(IIf(IsDBNull(RsACM.Fields("STDS_PER").Value), 0, RsACM.Fields("STDS_PER").Value), "0.000")

            txtESIPer.Text = VB6.Format(IIf(IsDBNull(RsACM.Fields("ESI_PER").Value), 0, RsACM.Fields("ESI_PER").Value), "0.000")

            chkState.CheckState = IIf(RsACM.Fields("WITHIN_STATE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkInterUnit.CheckState = IIf(RsACM.Fields("INTER_UNIT").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            If chkInterUnit.Checked = True Then
                txtCompanyName.Enabled = True
                mInterUnitCompanyCode = IIf(IsDBNull(RsACM.Fields("INTERUNIT_COMPANY_CODE").Value), "", RsACM.Fields("INTERUNIT_COMPANY_CODE").Value)

                If MainClass.ValidateWithMasterTable(mInterUnitCompanyCode, "COMPANY_CODE", "COMPANY_NAME", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                    txtCompanyName.Text = MasterNo
                Else
                    txtCompanyName.Text = ""
                End If
            Else
                txtCompanyName.Enabled = False
            End If
            chkCountry.CheckState = IIf(RsACM.Fields("WITHIN_COUNTRY").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            chkSEZ.CheckState = IIf(RsACM.Fields("IS_SEZ").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            chkAuthorised.CheckState = IIf(RsACM.Fields("AUTHORISED").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkAuthorised.Enabled = IIf(RsACM.Fields("AUTHORISED").Value = "Y", False, True)

            chkGroupLimit.CheckState = IIf(RsACM.Fields("GROUP_LIMIT").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)


            ChkPoRate.CheckState = IIf(RsACM.Fields("PORATEEDITABLE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkTCSApplicable.CheckState = IIf(RsACM.Fields("TCS_APP").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkMonthWiseLdgr.CheckState = IIf(RsACM.Fields("MONTHWISE_LDGR").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)


            chkAccountHide.CheckState = IIf(RsACM.Fields("ACCOUNT_HIDE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)


            chkTCSNotApplicable.CheckState = IIf(RsACM.Fields("TCS_NOT_APP").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkPlaceofSupply.CheckState = IIf(RsACM.Fields("PLACE_OF_SUPPLY").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)


            chkSMERegd.CheckState = IIf(RsACM.Fields("SME_REGD").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkSMEStatus.CheckState = IIf(RsACM.Fields("SME_STATUS").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            chkDistt.CheckState = IIf(RsACM.Fields("WITHIN_DISTT").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            chkSecurityChq.CheckState = IIf(RsACM.Fields("IS_SECURITY_DEPOSIT").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            txtSecurityAmount.Text = VB6.Format(IIf(IsDBNull(RsACM.Fields("SECURITY_AMOUNT").Value), 0, RsACM.Fields("SECURITY_AMOUNT").Value), "0.00")


            txtRemarks.Text = IIf(IsDBNull(RsACM.Fields("SUPP_CUST_REMARKS").Value), "", RsACM.Fields("SUPP_CUST_REMARKS").Value)

            txtTINNo.Text = IIf(IsDBNull(RsACM.Fields("ACCOUNT_CODE").Value), "", RsACM.Fields("ACCOUNT_CODE").Value)

            txtSrvRegnNo.Text = IIf(IsDBNull(RsACM.Fields("SRV_REGN_NO").Value), "", RsACM.Fields("SRV_REGN_NO").Value)


            txtSeq.Text = IIf(IsDBNull(RsACM.Fields("DSP_RPT_SEQ").Value), "", RsACM.Fields("DSP_RPT_SEQ").Value)
            txtReceiptDays.Text = IIf(IsDBNull(RsACM.Fields("RECEIPT_DAYS").Value), "", RsACM.Fields("RECEIPT_DAYS").Value)

            txtChqFrequency.Text = CStr(Val(IIf(IsDBNull(RsACM.Fields("ACTIVITY").Value), "", RsACM.Fields("ACTIVITY").Value)))

            If RsACM.Fields("BALANCINGMETHOD").Value = "S" Then
                optBalMethod(0).Checked = True
            Else
                optBalMethod(1).Checked = True
            End If

            If RsACM.Fields("STATUS").Value = "O" Then
                OptStatus(0).Checked = True
                FraStatus.Enabled = True
            Else
                OptStatus(1).Checked = True
                '            FraStatus.Enabled = IIf(PubSuperUser = "S" OR PubSuperUser = "A", True, False)					
                mIsAuthorisedUser = MainClass.STRMenuRight(PubUserID, CurrModuleID, xMyMenu, PubDBCn)
                If InStr(1, mIsAuthorisedUser, "S") = 0 Then
                    FraStatus.Enabled = False
                Else
                    FraStatus.Enabled = True
                End If
            End If

            chkStopMRR.CheckState = IIf(RsACM.Fields("STOP_MRR").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkStopInvoice.CheckState = IIf(RsACM.Fields("STOP_INVOICE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkStopGP.CheckState = IIf(RsACM.Fields("STOP_RGP").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkStopBP.CheckState = IIf(RsACM.Fields("STOP_BANK").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkStopPO.CheckState = IIf(RsACM.Fields("STOP_PO").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            '        mIsAuthorisedUser = MainClass.STRMenuRight(PubUserID, CurrModuleID, xMyMenu, PubDBCn)					
            mIsAuthorisedUser = GetUserPermission("ALLOW_ACCOUNT_MASTER", "N", PubUserID, RsCompany.Fields("COMPANY_CODE").Value)
            If mIsAuthorisedUser = "N" Then
                chkStopMRR.Enabled = IIf(chkStopMRR.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
                chkStopInvoice.Enabled = IIf(chkStopInvoice.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
                chkStopGP.Enabled = IIf(chkStopGP.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
                chkStopBP.Enabled = IIf(chkStopBP.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
                chkStopPO.Enabled = IIf(chkStopPO.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
            Else
                chkStopMRR.Enabled = True
                chkStopInvoice.Enabled = True
                chkStopGP.Enabled = True
                chkStopBP.Enabled = True
                chkStopPO.Enabled = True
            End If


            txtPaidDay.Text = CStr(Val(IIf(IsDBNull(RsACM.Fields("PaidDay").Value), 1, RsACM.Fields("PaidDay").Value)))


            txtPaidDay2.Text = CStr(Val(IIf(IsDBNull(RsACM.Fields("PaidDay2").Value), 0, RsACM.Fields("PaidDay2").Value)))

            txtPaidDay3.Text = CStr(Val(IIf(IsDBNull(RsACM.Fields("PaidDay3").Value), 0, RsACM.Fields("PaidDay3").Value)))

            txtPaidDay4.Text = CStr(Val(IIf(IsDBNull(RsACM.Fields("PaidDay4").Value), 0, RsACM.Fields("PaidDay4").Value)))

            Call SetCombo(cboCategory, (RsACM.Fields("SUPP_CUST_TYPE").Value))

            Call SetCombo(cboHeadType, IIf(IsDBNull(RsACM.Fields("HEADTYPE").Value), "", RsACM.Fields("HEADTYPE").Value))

            If RsACM.Fields("CType").Value = "C" Then
                cboCType.SelectedIndex = 0
            Else
                cboCType.SelectedIndex = 1
            End If

            If RsACM.Fields("PAYMENT_MODE").Value = "1" Then
                cboPaymentMode.SelectedIndex = 0
            ElseIf RsACM.Fields("PAYMENT_MODE").Value = "2" Then
                cboPaymentMode.SelectedIndex = 1
            ElseIf RsACM.Fields("PAYMENT_MODE").Value = "3" Then
                cboPaymentMode.SelectedIndex = 2
            ElseIf RsACM.Fields("PAYMENT_MODE").Value = "4" Then
                cboPaymentMode.SelectedIndex = 3
            ElseIf RsACM.Fields("PAYMENT_MODE").Value = "5" Then
                cboPaymentMode.SelectedIndex = 4
            ElseIf RsACM.Fields("PAYMENT_MODE").Value = "6" Then
                cboPaymentMode.SelectedIndex = 5
            ElseIf RsACM.Fields("PAYMENT_MODE").Value = "7" Then
                cboPaymentMode.SelectedIndex = 6
            ElseIf RsACM.Fields("PAYMENT_MODE").Value = "8" Then
                cboPaymentMode.SelectedIndex = 7
            ElseIf RsACM.Fields("PAYMENT_MODE").Value = "9" Then
                cboPaymentMode.SelectedIndex = 8
            ElseIf RsACM.Fields("PAYMENT_MODE").Value = "A" Then
                cboPaymentMode.SelectedIndex = 9
            Else
                cboPaymentMode.SelectedIndex = 2
            End If


            txtUdyogAahaarNo.Text = IIf(IsDBNull(RsACM.Fields("UDYOGAAHAARNO").Value), "", RsACM.Fields("UDYOGAAHAARNO").Value)

            mEnterpriseType = IIf(IsDBNull(RsACM.Fields("ENTERPRISE_TYPE").Value), "", RsACM.Fields("ENTERPRISE_TYPE").Value)

            If mEnterpriseType = "" Then
                cboEnterpriseType.SelectedIndex = 0
            ElseIf mEnterpriseType = "MICRO" Then
                cboEnterpriseType.SelectedIndex = 1
            ElseIf mEnterpriseType = "SMALL" Then
                cboEnterpriseType.SelectedIndex = 2
            ElseIf mEnterpriseType = "MEDIUM" Then
                cboEnterpriseType.SelectedIndex = 3
            End If


            mSMESymbol = IIf(IsDBNull(RsACM.Fields("SME_SYMBOL").Value), "", RsACM.Fields("SME_SYMBOL").Value)

            If mSMESymbol = "" Then
                cboSymbol.SelectedIndex = 0
            ElseIf mSMESymbol = "A" Then
                cboSymbol.SelectedIndex = 1
            ElseIf mSMESymbol = "B" Then
                cboSymbol.SelectedIndex = 2
            ElseIf mSMESymbol = "C" Then
                cboSymbol.SelectedIndex = 3
            End If

            If MainClass.ValidateWithMasterTable(IIf(IsDBNull(RsACM.Fields("SECTIONCODE").Value), "", RsACM.Fields("SECTIONCODE").Value), "Code", "Name", "TDS_Section_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtSection.Text = IIf(IsDBNull(MasterNo), "", MasterNo)
            End If


            txtExptionCNo.Text = IIf(IsDBNull(RsACM.Fields("EXPTIONCNO").Value), "", RsACM.Fields("EXPTIONCNO").Value)

            txtLDCertiNo.Text = IIf(IsDBNull(RsACM.Fields("LOWER_DED_CERT_NO").Value), "", RsACM.Fields("LOWER_DED_CERT_NO").Value)
            chkLowerDeduction.CheckState = IIf(RsACM.Fields("IS_LOWER_DED").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            'mAcctCode = IIf(IsDBNull(RsACM.Fields("PUR_STRECD_FORMCODE").Value), "", RsACM.Fields("PUR_STRECD_FORMCODE").Value)

            'If mAcctCode = "" Then
            '    txtPurchaseSTRecd.Text = ""
            'ElseIf MainClass.ValidateWithMasterTable(mAcctCode, "CODE", "NAME", "FIN_STFORM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            '    txtPurchaseSTRecd.Text = MasterNo
            'End If

            'mAcctCode = IIf(IsDBNull(RsACM.Fields("PUR_STDUE_FORMCODE").Value), "", RsACM.Fields("PUR_STDUE_FORMCODE").Value)

            'If mAcctCode = "" Then
            '    txtPurchaseSTDue.Text = ""
            'ElseIf MainClass.ValidateWithMasterTable(mAcctCode, "CODE", "NAME", "FIN_STFORM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            '    txtPurchaseSTDue.Text = MasterNo
            'End If

            'mAcctCode = IIf(IsDBNull(RsACM.Fields("SALE_STRECD_FORMCODE").Value), "", RsACM.Fields("SALE_STRECD_FORMCODE").Value)

            'If mAcctCode = "" Then
            '    txtSaleSTRecd.Text = ""
            'ElseIf MainClass.ValidateWithMasterTable(mAcctCode, "CODE", "NAME", "FIN_STFORM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            '    txtSaleSTRecd.Text = MasterNo
            'End If

            'mAcctCode = IIf(IsDBNull(RsACM.Fields("SALE_STDUE_FORMCODE").Value), "", RsACM.Fields("SALE_STDUE_FORMCODE").Value)
            'If mAcctCode = "" Then
            '    txtSaleSTDue.Text = ""
            'ElseIf MainClass.ValidateWithMasterTable(mAcctCode, "CODE", "NAME", "FIN_STFORM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            '    txtSaleSTDue.Text = MasterNo
            'End If



            '********09-03-2005					

            txtCountry.Text = IIf(IsDBNull(RsACM.Fields("COUNTRY").Value), "", RsACM.Fields("COUNTRY").Value)


            mBuyerCode = IIf(IsDBNull(RsACM.Fields("BUYERCODE").Value), "", RsACM.Fields("BUYERCODE").Value)

            If mBuyerCode = "" Then
                txtBuyerName.Text = ""
            Else
                If MainClass.ValidateWithMasterTable(mBuyerCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtBuyerName.Text = MasterNo
                End If
            End If


            mServiceProviderCode = IIf(IsDBNull(RsACM.Fields("SERVPROV_CODE").Value), -1, RsACM.Fields("SERVPROV_CODE").Value)

            If mServiceProviderCode = -1 Then
                txtServProvided.Text = ""
            Else
                If MainClass.ValidateWithMasterTable(mServiceProviderCode, "CODE", "NAME", "FIN_SERVPROV_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtServProvided.Text = MasterNo
                End If
            End If


            txtCarriage.Text = IIf(IsDBNull(RsACM.Fields("CARRIAGE").Value), "", RsACM.Fields("CARRIAGE").Value)

            txtLoadingPort.Text = IIf(IsDBNull(RsACM.Fields("LOADINGPORT").Value), "", RsACM.Fields("LOADINGPORT").Value)

            txtDischargePort.Text = IIf(IsDBNull(RsACM.Fields("DISCHARGEPORT").Value), "", RsACM.Fields("DISCHARGEPORT").Value)

            txtFinalDest.Text = IIf(IsDBNull(RsACM.Fields("FINALDEST").Value), "", RsACM.Fields("FINALDEST").Value)

            txtExportPaymetTerms.Text = IIf(IsDBNull(RsACM.Fields("PAYMENTTERMS").Value), "", RsACM.Fields("PAYMENTTERMS").Value)


            txtPayment.Text = IIf(IsDBNull(RsACM.Fields("PAYMENT_CODE").Value), "", RsACM.Fields("PAYMENT_CODE").Value)
            If MainClass.ValidateWithMasterTable(txtPayment.Text, "PAY_TERM_CODE", "PAY_TERM_DESC", "FIN_PAYTERM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblPaymentTerms.Text = MasterNo
            End If


            lblAddUser.Text = IIf(IsDBNull(RsACM.Fields("ADDUSER").Value), "", RsACM.Fields("ADDUSER").Value)

            lblAddDate.Text = VB6.Format(IIf(IsDBNull(RsACM.Fields("ADDDATE").Value), "", RsACM.Fields("ADDDATE").Value), "dd/MM/yyyy")

            lblModUser.Text = IIf(IsDBNull(RsACM.Fields("MODUSER").Value), "", RsACM.Fields("MODUSER").Value)

            lblModDate.Text = VB6.Format(IIf(IsDBNull(RsACM.Fields("MODDATE").Value), "", RsACM.Fields("MODDATE").Value), "dd/MM/yyyy")
            txtChqFrequency.Enabled = False
            txtPayment.Enabled = IIf(txtPayment.Text = "", True, False)
            cmdPaySearch.Enabled = IIf(txtPayment.Text = "", True, False)

            chkTDSDeduct.CheckState = IIf(RsACM.Fields("TDS_UNDER_194Q").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkTDSNotDeduct.CheckState = IIf(RsACM.Fields("TDS_NOT_UNDER_194Q").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkRtnDeclaration.CheckState = IIf(RsACM.Fields("TDS_DECLARATION_SUB").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            If lblMasterType.Text = "Accounts" Then

            Else
                Call ShowDetail1(txtCode.Text)
            End If


            '        OPBalType					

            'Field Disable...					
        End If
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsACM, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub
    Private Sub ShowDetail1(ByVal pSupplierCode As String)

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String = ""


        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf _
            & " FROM FIN_SUPP_CUST_BUSINESS_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & pSupplierCode & "' AND SERIAL_NO>1" & vbCrLf _
            & " ORDER BY SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsACMOthers, ADODB.LockTypeEnum.adLockReadOnly)

        With RsACMOthers
            If .EOF = True Then Exit Sub
            I = 1
            .MoveFirst()

            Do While Not .EOF

                SprdMain.Row = I

                SprdMain.Col = ColLocation
                SprdMain.Text = IIf(IsDBNull(.Fields("LOCATION_ID").Value), "", .Fields("LOCATION_ID").Value)

                SprdMain.Col = ColAddress
                SprdMain.Text = IIf(IsDBNull(.Fields("SUPP_CUST_ADDR").Value), "", .Fields("SUPP_CUST_ADDR").Value)

                SprdMain.Col = ColCity
                SprdMain.Text = IIf(IsDBNull(.Fields("SUPP_CUST_CITY").Value), "", .Fields("SUPP_CUST_CITY").Value)

                SprdMain.Col = ColState
                SprdMain.Text = IIf(IsDBNull(.Fields("SUPP_CUST_STATE").Value), "", .Fields("SUPP_CUST_STATE").Value)

                SprdMain.Col = ColPin
                SprdMain.Text = IIf(IsDBNull(.Fields("SUPP_CUST_PIN").Value), "", .Fields("SUPP_CUST_PIN").Value)

                SprdMain.Col = ColCountry
                SprdMain.Text = IIf(IsDBNull(.Fields("COUNTRY").Value), "", .Fields("COUNTRY").Value)

                SprdMain.Col = ColAlias
                SprdMain.Text = IIf(IsDBNull(.Fields("ALIAS_NAME").Value), "", .Fields("ALIAS_NAME").Value)

                SprdMain.Col = ColContactNo
                SprdMain.Text = IIf(IsDBNull(.Fields("SUPP_CUST_MOBILE").Value), "", .Fields("SUPP_CUST_MOBILE").Value)

                SprdMain.Col = ColeMailID
                SprdMain.Text = IIf(IsDBNull(.Fields("SUPP_CUST_MAILID").Value), "", .Fields("SUPP_CUST_MAILID").Value)

                SprdMain.Col = ColDistance
                SprdMain.Text = IIf(IsDBNull(.Fields("LOC_DISTANCE").Value), 0, .Fields("LOC_DISTANCE").Value)

                SprdMain.Col = ColGSTNo
                SprdMain.Text = IIf(IsDBNull(.Fields("GST_RGN_NO").Value), "", .Fields("GST_RGN_NO").Value)

                SprdMain.Col = ColWithinDistrict
                SprdMain.Value = IIf(.Fields("WITHIN_DISTT").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                SprdMain.Col = ColWithinState
                SprdMain.Value = IIf(.Fields("WITHIN_STATE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                SprdMain.Col = ColWithinCountry
                SprdMain.Value = IIf(.Fields("WITHIN_COUNTRY").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                FormatSprdMain(I)


                .MoveNext()

                I = I + 1
                SprdMain.MaxRows = I
            Loop

            MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, ColLocation, SprdMain.MaxCols)
            MainClass.ProtectCell(SprdMain, 1, I - 1, ColLocation, ColLocation)

        End With

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume						
    End Sub
    Private Sub txtPaidDay_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPaidDay.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPaidDay_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPaidDay.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPan_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPan.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPAN_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPan.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPan.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtphone_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPhone.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPhone_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPhone.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPhone.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtpincode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPinCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtpincode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPinCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPinCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRange_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRange.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRange_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRange.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtRange.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtRegnNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRegnNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRegnNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRegnNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtRegnNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRemarks_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRemarks.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtRemarks.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSection_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSection.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtSection_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSection.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSection.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSection_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSection.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
    End Sub

    Private Sub txtSection_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSection.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mSectionCode As Integer
        Dim mTdsRate As Double

        If Trim(txtSection.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtSection.Text, "Name", "Code", "TDS_Section_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Secion Name Does Not Exist In Master", vbInformation)
            Cancel = True
            Exit Sub
        Else
            mSectionCode = MasterNo
        End If

        mTdsRate = CalcTDSRate(mSectionCode, VB.Left(cboCType.Text, 1), PubDBCn)

        txtTDSPer.Text = VB6.Format(IIf(Trim(txtTDSPer.Text) = "", mTdsRate, txtTDSPer.Text), "0.000")

        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtSeq_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSeq.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtReceiptDays_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtReceiptDays.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtReceiptDays_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtReceiptDays.KeyPress

        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSeq_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSeq.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSeq.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub



    Private Sub txtServProvided_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtServProvided.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtServProvided_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtServProvided.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtServProvided.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtServProvided_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtServProvided.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
    End Sub

    Private Sub txtServProvided_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtServProvided.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1

        If Trim(txtServProvided.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtServProvided.Text, "NAME", "CODE", "FIN_SERVPROV_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Please Select Valid Service Provided")
            Cancel = True
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtSrvRegnNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSrvRegnNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSrvRegnNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSrvRegnNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSrvRegnNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtstate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtState.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtState_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtState.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtState.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtState_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtState.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
    End Sub

    Private Sub txtState_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtState.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtState.Text) = "" Then GoTo EventExitSub

        If VB.Left(cboCategory.Text, 1) = "S" Or VB.Left(cboCategory.Text, 1) = "C" Then
            If MainClass.ValidateWithMasterTable(txtState.Text, "NAME", "NAME", "GEN_STATE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                SSTInfo.SelectedIndex = 0
                ErrorMsg("Invalid State Name", , vbInformation)
                Cancel = True
            End If
        End If


EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSTDSPer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSTDSPer.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSTDSPer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSTDSPer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtSwitCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSwitCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSwitCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSwitCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSwitCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTDSPer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTDSPer.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTDSPer_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTDSPer.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTINNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTINNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTINNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTINNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtVendorCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVendorCode.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtVendorCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVendorCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtVendorCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtShortName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtShortName.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtShortName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtShortName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtShortName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPayment_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPayment.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPayment.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPayment_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPayment.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdPaySearch_Click()
    End Sub
    Private Sub cmdPaySearch_Click() Handles cmdPaySearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster(txtPayment.Text, "FIN_PAYTERM_MST", "PAY_TERM_CODE", "PAY_TERM_DESC", , , SqlStr) = True Then
            txtPayment.Text = AcName
            txtPayment_Validating(txtPayment, New System.ComponentModel.CancelEventArgs(False))
            txtPayment.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, Err.Number, vbCritical)
    End Sub

    Private Sub txtPayment_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPayment.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim xAcctCode As String

        If Trim(txtPayment.Text) = "" Then GoTo EventExitSub


        If MainClass.ValidateWithMasterTable(txtPayment.Text, "PAY_TERM_CODE", "PAY_TERM_DESC", "FIN_PAYTERM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            lblPaymentTerms.Text = MasterNo
        Else
            MsgBox("Invalid Payment Code.", vbInformation)
            Cancel = True
            Exit Sub
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCity_Validating(sender As Object, e As CancelEventArgs) Handles txtCity.Validating
        Dim Cancel As Boolean = e.Cancel
        Dim mStateCode As String = ""
        If Trim(txtCity.Text) = "" Then GoTo EventExitSub

        If VB.Left(cboCategory.Text, 1) = "S" Or VB.Left(cboCategory.Text, 1) = "C" Then
            If MainClass.ValidateWithMasterTable(txtCity.Text, "CITY_NAME", "STATE_CODE", "GEN_CITY_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                SSTInfo.SelectedIndex = 0
                ErrorMsg("Invalid City Name", , vbInformation)
                Cancel = True
            Else
                mStateCode = MasterNo
                If MainClass.ValidateWithMasterTable(mStateCode, "CODE", "NAME", "GEN_STATE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtState.Text = MasterNo
                End If
            End If
        End If


EventExitSub:
        e.Cancel = Cancel
    End Sub
    Private Sub FormatSprdMain(ByVal Arow As Integer)

        On Error GoTo ERR1


        With SprdMain
            .Row = Arow
            .set_RowHeight(-1, ConRowHeight * 2.5)


            .Col = ColLocation
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsACMOthers.Fields("LOCATION_ID").DefinedSize ''						
            .set_ColWidth(ColLocation, 12)

            .Col = ColAddress
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = RsACMOthers.Fields("SUPP_CUST_ADDR").DefinedSize ''						
            .set_ColWidth(ColAddress, 32)


            .Col = ColCity
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = MainClass.SetMaxLength("CITY_NAME", "GEN_CITY_MST", PubDBCn)
            .ColsFrozen = ColLocation
            .set_ColWidth(ColCity, 15)

            .Col = ColState
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = MainClass.SetMaxLength("NAME", "GEN_STATE_MST", PubDBCn)
            .set_ColWidth(ColState, 8)

            .Col = ColGSTNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsACMOthers.Fields("GST_RGN_NO").DefinedSize ''						
            .set_ColWidth(ColGSTNo, 15)


            .Col = ColPin
            '.CellType = SS_CELL_TYPE_EDIT
            '.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            '.TypeEditMultiLine = False
            ''.TypeEditLen = MainClass.SetMaxLength("CODE", "GEN_STATE_MST", PubDBCn)
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 0
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999")
            .TypeFloatMin = CDbl("111111")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColPin, 6)

            .Col = ColCountry
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .TypeEditLen = MainClass.SetMaxLength("COUNTRY_NAME", "GEN_COUNTRY_MST", PubDBCn)
            .set_ColWidth(ColCountry, 8)


            .Col = ColAlias
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsACMOthers.Fields("ALIAS_NAME").DefinedSize ''						
            .set_ColWidth(ColAlias, 6)


            .Col = ColContactNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsACMOthers.Fields("SUPP_CUST_MOBILE").DefinedSize ''						
            .set_ColWidth(ColContactNo, 12)

            .Col = ColeMailID
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsACMOthers.Fields("SUPP_CUST_MAILID").DefinedSize ''						
            .set_ColWidth(ColeMailID, 12)

            .Col = ColDistance
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 0
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999")
            .TypeFloatMin = CDbl("0")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColDistance, 6)
            .ColHidden = False

            .Col = ColWithinDistrict
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeVAlign = SS_CELL_STATIC_V_ALIGN_TOP
            .set_ColWidth(ColWithinDistrict, 5)
            '.Value = vbUnchecked
            .ColHidden = True

            .Col = ColWithinState
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeVAlign = SS_CELL_STATIC_V_ALIGN_TOP
            .set_ColWidth(ColWithinState, 5)
            .ColHidden = True

            .Col = ColWithinCountry
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .TypeVAlign = SS_CELL_STATIC_V_ALIGN_TOP
            .set_ColWidth(ColWithinCountry, 5)

        End With

        MainClass.UnProtectCell(SprdMain, Arow, SprdMain.MaxRows, Arow, SprdMain.MaxCols)

        'MainClass.ProtectCell(SprdMain, Arow, SprdMain.MaxRows, ColState, ColState)
        'MainClass.ProtectCell(SprdMain, Arow, SprdMain.MaxRows, ColCountry, ColCountry)

        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then RsACM.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume						
    End Sub
    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        Dim mPONo As Double

        mCol = SprdMain.ActiveCol

        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColCity Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColCity, 0))

        SprdMain.Refresh()


    End Sub
    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim mCity As String = ""

        SprdMain.Row = SprdMain.ActiveRow
        'SprdMain.Col = ColCity
        'mCity = Trim(SprdMain.Text)



        If eventArgs.row = 0 And eventArgs.col = ColCity Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColCity
                mCity = Trim(SprdMain.Text)
                ''If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                If MainClass.SearchGridMaster(mCity, "GEN_CITY_MST", "CITY_NAME", "", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow
                    .Col = ColCity
                    .Text = Trim(AcName)
                End If


                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColCity)
            End With
        End If


        Dim DelStatus As Boolean
        If eventArgs.col = 0 And eventArgs.row > 1 Then
            SprdMain.Row = eventArgs.row
            SprdMain.Col = ColLocation
            If eventArgs.row < SprdMain.MaxRows And (ADDMode = True Or MODIFYMode = True) Then

                'mPONo = SprdMain.Text

                'SprdMain.Col = ColItemCode
                'mItemCode = SprdMain.Text

                MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColLocation, DelStatus)
                FormatSprdMain(-1)
                MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
            End If
        End If

    End Sub
    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        Dim mRow As Long
        Dim mCity As String
        Dim mStateCode As String = ""
        Dim mStateName As String = ""
        Dim mCountryName As String = ""
        Dim mCountryCode As String = ""


        If eventArgs.newRow = -1 Then Exit Sub

        mRow = eventArgs.row
        SprdMain.Row = mRow
        Select Case eventArgs.col
            Case ColCity
                SprdMain.Row = mRow

                SprdMain.Col = ColCity
                mCity = SprdMain.Text


                If MainClass.ValidateWithMasterTable(mCity, "CITY_NAME", "STATE_CODE", "GEN_CITY_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                    MsgInformation("Invalid City Name")
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColCity)
                    eventArgs.cancel = True
                    Exit Sub
                Else
                    mStateCode = MasterNo
                End If

                If MainClass.ValidateWithMasterTable(mStateCode, "CODE", "NAME", "GEN_STATE_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mStateName = MasterNo
                End If

                If MainClass.ValidateWithMasterTable(mStateCode, "CODE", "COUNTRY_CODE", "GEN_STATE_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mCountryCode = MasterNo
                End If

                If MainClass.ValidateWithMasterTable(mCountryCode, "COUNTRY_CODE", "COUNTRY_NAME", "GEN_COUNTRY_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mCountryName = MasterNo
                End If

                SprdMain.Col = ColState
                SprdMain.Text = mStateName

                SprdMain.Col = ColCountry
                SprdMain.Text = mCountryName

                'SprdMain.Col = ColWithinState
                'If UCase(Trim(mStateName)) = UCase(RsCompany.Fields("COMPANY_STATE").Value) Then
                '    SprdMain.Value = System.Windows.Forms.CheckState.Checked
                'Else
                '    SprdMain.Value = System.Windows.Forms.CheckState.Unchecked

                '    SprdMain.Col = ColWithinDistrict
                '    SprdMain.Value = System.Windows.Forms.CheckState.Unchecked
                'End If

                SprdMain.Col = ColWithinCountry
                SprdMain.Value = IIf(mCountryName = "INDIA", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                MainClass.AddBlankSprdRow(SprdMain, ColCity, ConRowHeight)
                FormatSprdMain(eventArgs.row)

        End Select
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    'Private Sub cmdsearch_Click(sender As Object, e As EventArgs) Handles cmdsearch.Click
    '    SearchAccounts()
    'End Sub
    Private Sub SearchAccounts()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If lblMasterType.Text = "Accounts" Then
            SqlStr = SqlStr & " AND SUPP_CUST_TYPE NOT IN ('S','C')"
        ElseIf lblMasterType.Text = "S" Then
            SqlStr = SqlStr & " AND SUPP_CUST_TYPE='S'"
        ElseIf lblMasterType.Text = "C" Then
            SqlStr = SqlStr & " AND SUPP_CUST_TYPE='C'"
        End If

        ''MainClass.SearchMaster TxtAccount, "FIN_SUPP_CUST_MST", "NAME", SqlStr
        MainClass.SearchGridMaster("", "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr)
        If AcName <> "" Then
            txtName.Text = AcName
            TxtName_Validating(txtName, New System.ComponentModel.CancelEventArgs(False))
        End If

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub txtName_DoubleClick(sender As Object, e As EventArgs) Handles txtName.DoubleClick
        SearchAccounts()
    End Sub

    Private Sub cmdSearchHead_Click(sender As Object, e As EventArgs) Handles cmdSearchHead.Click
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GROUP_CATEGORY='G' AND GROUP_STATUS='O'"
        If MainClass.SearchMaster(txtGroupName.Text, "FIN_GROUP_MST", "GROUP_NAME", SqlStr) = True Then
            txtGroupName.Text = AcName
            txtGroupName_Validating(txtGroupName, New System.ComponentModel.CancelEventArgs(False))
            txtGroupName.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, Err.Number, vbCritical)
    End Sub

    Private Sub cmdSearchHeadCr_Click(sender As Object, e As EventArgs) Handles cmdSearchHeadCr.Click
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GROUP_CATEGORY='G' AND GROUP_STATUS='O'"
        If MainClass.SearchMaster(txtGroupNameCr.Text, "FIN_GROUP_MST", "GROUP_NAME", SqlStr) = True Then
            txtGroupNameCr.Text = AcName
            txtGroupNameCr_Validating(txtGroupNameCr, New System.ComponentModel.CancelEventArgs(False))
            txtGroupNameCr.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, Err.Number, vbCritical)
    End Sub

    Private Sub txtPayment_DoubleClick(sender As Object, e As EventArgs) Handles txtPayment.DoubleClick
        cmdPaySearch_Click()
    End Sub

    Private Sub txtCode_DoubleClick(sender As Object, e As EventArgs) Handles txtCode.DoubleClick
        Call SearchCode()
    End Sub

    Private Sub chkTDSDeduct_CheckedChanged(sender As Object, e As EventArgs) Handles chkTDSDeduct.CheckedChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkTDSNotDeduct_CheckedChanged(sender As Object, e As EventArgs) Handles chkTDSNotDeduct.CheckedChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub chkRtnDeclaration_CheckedChanged(sender As Object, e As EventArgs) Handles chkRtnDeclaration.CheckedChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

End Class
