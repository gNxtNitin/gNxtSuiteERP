Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmPOExchangeRate
    Inherits System.Windows.Forms.Form
    Dim RsPOMain As ADODB.Recordset ''ADODB.Recordset

    Dim XRIGHT As String
    Dim FormActive As Boolean

    Dim pmyMenu As String
    Private Sub ChkCapital_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCapital.CheckStateChanged
        cmdSave.Enabled = True
    End Sub
    Private Sub ChkActivate_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkActivate.CheckStateChanged
        cmdSave.Enabled = True
    End Sub

    Private Sub chkModvatable_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkModvatable.CheckStateChanged
        cmdSave.Enabled = True
    End Sub
    Private Sub chkStatus_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkStatus.CheckStateChanged
        cmdSave.Enabled = True
    End Sub

    Private Sub chkSTRefundable_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkSTRefundable.CheckStateChanged
        cmdSave.Enabled = True
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        On Error Resume Next
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        If Update1 = True Then
            txtAmendNo_Validating(txtAmendNo, New System.ComponentModel.CancelEventArgs(False))
            cmdSave.Enabled = False
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
        Dim mPONo As Double
        Dim mPurType As String
        Dim mOrderType As String
        Dim mStatus As String
        Dim mActivate As String
        Dim mAmendNo As Integer
        Dim mRecdAcct As String
        Dim mModvatable As String
        Dim mSTRefundable As String
        Dim mCapital As String
        Dim mServiceCode As Double

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()


        mModvatable = IIf(chkModvatable.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mSTRefundable = IIf(chkSTRefundable.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mCapital = IIf(chkCapital.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        SqlStr = ""

        '    If MainClass.ValidateWithMasterTable(Trim(txtServProvided.Text), "NAME", "CODE", "FIN_SERVPROV_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        mServiceCode = Val(MasterNo)
        '    Else
        '        mServiceCode = -1
        '    End If

        SqlStr = " UPDATE PUR_PURCHASE_HDR SET " & vbCrLf _
            & " EXCHANGERATE= " & Val(TxtExchangeRate.Text) & ", " & vbCrLf _
            & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "',Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND MKEY =" & Val(lblMkey.Text) & ""

        PubDBCn.Execute(SqlStr)

        Update1 = True
        PubDBCn.CommitTrans()

        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsPOMain.Requery()
        MsgBox(Err.Description)
        ''Resume
    End Function

    Private Sub cmdSearchPO_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchPO.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If RsCompany.Fields("FYEAR").Value < ConOPENPO_CONTINOUS_YEAR Then
            SqlStr = SqlStr & vbCrLf & "AND SUBSTR(AUTO_KEY_PO,LENGTH(AUTO_KEY_PO)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
        Else
            SqlStr = SqlStr & vbCrLf & "AND SUBSTR(AUTO_KEY_PO,LENGTH(AUTO_KEY_PO)-5,4)>=" & ConOPENPO_CONTINOUS_YEAR & ""
        End If

        If MainClass.SearchGridMaster((txtPONo.Text), "PUR_PURCHASE_HDR", "AUTO_KEY_PO", "AMEND_NO", "PUR_ORD_DATE", "SUPP_CUST_CODE", SqlStr) = True Then
            txtPONo.Text = AcName
            txtAmendNo.Text = AcName1
            txtAmendNo_Validating(txtAmendNo, New System.ComponentModel.CancelEventArgs(False)) '' txtPONO_Validate False
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdServProvided_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdServProvided.Click
        Call SearchProvidedMaster()
    End Sub

    Public Sub frmPOExchangeRate_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Me.Text = "Purchase Order Modification"

        SqlStr = "Select * From PUR_PURCHASE_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPOMain, ADODB.LockTypeEnum.adLockReadOnly)


        SetTextLengths()
        Clear1()

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmPOExchangeRate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmPOExchangeRate_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        Call SetMainFormCordinate(Me)
        pmyMenu = myMenu
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, pmyMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)

        MainClass.SetControlsColor(Me)

        FormActive = False
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub Clear1()
        On Error GoTo ClearErr


        lblMKey.Text = ""
        txtPONo.Text = ""
        txtPODate.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        txtAmendNo.Text = CStr(0)
        txtAmendDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkStatus.Enabled = False
        ChkActivate.Enabled = False

        txtSupplierName.Text = ""

        txtDivision.Text = ""
        lblDivision.Text = ""


        chkModvatable.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkSTRefundable.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkCapital.CheckState = System.Windows.Forms.CheckState.Unchecked

        txtCode.Text = ""

        txtSupplierName.Enabled = False
        txtCode.Enabled = False
        txtDivision.Enabled = False

        TxtExchangeRate.Text = "1.000"


        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub
    Private Sub SetTextLengths()

        On Error GoTo ERR1

        txtPONo.Maxlength = RsPOMain.Fields("AUTO_KEY_PO").Precision
        txtPODate.Maxlength = RsPOMain.Fields("PUR_ORD_DATE").DefinedSize - 6

        txtAmendNo.Maxlength = RsPOMain.Fields("AMEND_NO").Precision
        txtAmendDate.Maxlength = RsPOMain.Fields("AMEND_DATE").DefinedSize - 6

        TxtExchangeRate.Maxlength = RsPOMain.Fields("ExchangeRate").Precision

        txtSupplierName.Maxlength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
        txtCode.Maxlength = RsPOMain.Fields("SUPP_CUST_CODE").DefinedSize

        txtDivision.Maxlength = RsPOMain.Fields("DIV_CODE").DefinedSize

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub

    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim mItemCode As String
        Dim mQty As Double
        Dim mPOWEFCheck As String
        Dim mPOWEF As String
        Dim mCheckPOWEF As Boolean

        Dim pPervRate As Double
        Dim pCurrRate As Double
        Dim mPrice As Double
        Dim mDisc As Double

        Dim I As Integer
        Dim mIsApproved As String
        Dim pPONO As Double
        Dim mItemCategory As String
        Dim mItemUOM As String = ""
        Dim mItemStock As Double
        Dim mIsCapitalCheck As String
        Dim mIsItemCapital As String

        FieldsVarification = True

        If MainClass.GetUserCanModify((txtAmendDate.Text)) = False Then
            MsgBox("You Have Not Rights to change back P.O.", MsgBoxStyle.Information)
            FieldsVarification = False
            Exit Function
        End If

        If chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MsgInformation("PO is UnPost, Please change from PO.")
            FieldsVarification = False
            Exit Function
        End If


        If Trim(txtPONo.Text) = "" Then
            MsgInformation("PO No. is Blank")
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtSupplierName.Text) = "" Then
            MsgInformation("Supplier Name is Blank. Cannot Save")
            If txtSupplierName.Enabled = True Then txtSupplierName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtDivision.Text) = "" Then
            MsgInformation("Division is Blank. Cannot Save")
            If txtDivision.Enabled = True Then txtDivision.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable((lblDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invalid txtDivision Name. Cannot Save")
            If txtDivision.Enabled = True Then txtDivision.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable((txtSupplierName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtCode.Text = MasterNo
        Else
            MsgInformation("Invalid Supplier Name. Cannot Save")
            If txtSupplierName.Enabled = True Then txtSupplierName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        ''24.11.2003

        '    If Left(lblBookType.text, 1) = "W" Then
        '        If Trim(txtServProvided.Text) = "" Then
        '            MsgBox "Please Select The Service., So cann't be Saved.", vbInformation
        '            FieldsVarification = False
        '            Exit Function
        '        End If
        '
        '        If MainClass.ValidateWithMasterTable(Trim(txtServProvided.Text), "NAME", "CODE", "FIN_SERVPROV_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
        '            MsgBox "Service Provided is not defined in Master, So cann't be Saved.", vbInformation
        '            FieldsVarification = False
        '            Exit Function
        '        End If
        '    End If
        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        'Resume
    End Function

    Private Sub frmPOExchangeRate_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        Me.Hide()
        Me.Close()
        Me.Dispose()
        RsPOMain.Close()
        'RsOpOuts.Close
    End Sub


    Private Sub txtAmendDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAmendDate.TextChanged
        cmdSave.Enabled = True
    End Sub

    Private Sub txtAmendDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAmendDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtAmendDate.Text) = "" Then GoTo EventExitSub

        If IsDate(txtAmendDate.Text) = False Then
            MsgInformation(" Invalid Date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtAmendNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAmendNo.TextChanged
        cmdSave.Enabled = True
    End Sub

    Private Sub txtAmendNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAmendNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Public Sub txtAmendNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAmendNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMKey As String
        Dim mPONo As Double
        Dim SqlStr As String = ""


        If Trim(txtPONo.Text) = "" Then GoTo EventExitSub

        If Len(txtPONo.Text) < 6 Then
            txtPONo.Text = Val(txtPONo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        mPONo = Val(txtPONo.Text)

        SqlStr = "SELECT * FROM PUR_PURCHASE_HDR " & " WHERE AUTO_KEY_PO='" & MainClass.AllowSingleQuote(UCase(CStr(mPONo))) & "'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        SqlStr = SqlStr & vbCrLf & " AND AMEND_NO=" & Val(txtAmendNo.Text) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPOMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsPOMain.EOF = False Then
            Clear1()
            Show1()
        Else
            SqlStr = "SELECT * FROM PUR_PURCHASE_HDR " & " WHERE AUTO_KEY_PO='" & MainClass.AllowSingleQuote(UCase(CStr(mPONo))) & "'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

            SqlStr = SqlStr & vbCrLf & " AND AMEND_NO IN (" & vbCrLf & " SELECT MAX(AMEND_NO) FROM PUR_PURCHASE_HDR " & " WHERE AUTO_KEY_PO='" & MainClass.AllowSingleQuote(UCase(CStr(mPONo))) & "'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ") "

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPOMain, ADODB.LockTypeEnum.adLockReadOnly)
            If RsPOMain.EOF = False Then
                Clear1()
                Show1()
            Else
                MsgBox("No Such PO No. Click, Add for New", MsgBoxStyle.Information)
                txtAmendNo.Text = CStr(0)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCode.TextChanged
        cmdSave.Enabled = True
    End Sub
    Private Sub txtCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDivision.TextChanged
        cmdSave.Enabled = True
    End Sub

    Private Sub txtDivision_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDivision.DoubleClick
        cmdDivSearch_Click()
    End Sub


    Private Sub txtDivision_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDivision.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDivision.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtDivision_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDivision.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdDivSearch_Click()
    End Sub
    Private Sub txtDivision_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDivision.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtDivision.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtDivision.Text), "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            lblDivision.Text = MasterNo
        Else
            MsgBox("Invalid Division Code.", MsgBoxStyle.Information)
            Cancel = True
            GoTo EventExitSub
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, err.NUmber, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cmdDivSearch_Click()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtDivision.Text), "INV_DIVISION_MST", "DIV_CODE", "DIV_DESC", , , SqlStr) = True Then
            txtDivision.Text = AcName
            txtDivision_Validating(txtDivision, New System.ComponentModel.CancelEventArgs(False))
            txtDivision.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub TxtExchangeRate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtExchangeRate.TextChanged
        cmdSave.Enabled = True
    End Sub

    Private Sub TxtExchangeRate_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtExchangeRate.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtPODate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPODate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtPODate.Text) = "" Then GoTo EventExitSub

        If IsDate(txtPODate.Text) = False Then
            MsgInformation(" Invalid Date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPONo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPONo.DoubleClick
        cmdSearchPO_Click(cmdSearchPO, New System.EventArgs())
    End Sub

    Private Sub txtPONo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPONo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchPO_Click(cmdSearchPO, New System.EventArgs())
    End Sub

    Private Sub txtServProvided_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtServProvided.TextChanged
        cmdSave.Enabled = True
    End Sub

    Private Sub txtServProvided_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtServProvided.DoubleClick
        SearchProvidedMaster()
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
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchProvidedMaster()
    End Sub

    Private Sub txtServProvided_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtServProvided.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        'Dim mReverseChargeApp As String
        'Dim mReverseChargePer As String

        '    If Trim(txtServProvided.Text) = "" Then Exit Sub
        '
        '
        '    SqlStr = " SELECT CODE, NAME, REVERSE_CHARGE_APP, REVERSE_CHARGE_PER" & vbCrLf _
        ''            & " FROM FIN_SERVPROV_MST" & vbCrLf _
        ''            & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
        ''            & " AND NAME='" & MainClass.AllowSingleQuote(txtServProvided.Text) & "'"
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly
        '
        '    If RsTemp.EOF = True Then
        '        MsgInformation "Please Select Valid Service Provided"
        '        Cancel = True
        '        Exit Sub
        '    End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub SearchProvidedMaster()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        '    SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        '
        '    If MainClass.SearchGridMaster(txtServProvided.Text, "FIN_SERVPROV_MST", "NAME", , , , SqlStr) = True Then
        '        txtServProvided.Text = AcName
        '        txtServProvided_Validate False
        '    End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtSupplierName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplierName.TextChanged
        cmdSave.Enabled = True
    End Sub
    Private Sub Show1()
        On Error GoTo ShowErrPart
        Dim mAccountName As String = ""
        Dim mAccountCode As String
        Dim mServiceCode As Double

        Clear1()

        If Not RsPOMain.EOF Then

            lblMKey.Text = IIf(IsDbNull(RsPOMain.Fields("MKEY").Value), "", RsPOMain.Fields("MKEY").Value)
            txtPONo.Text = IIf(IsDbNull(RsPOMain.Fields("AUTO_KEY_PO").Value), "", RsPOMain.Fields("AUTO_KEY_PO").Value)
            txtPODate.Text = VB6.Format(IIf(IsDbNull(RsPOMain.Fields("PUR_ORD_DATE").Value), "", RsPOMain.Fields("PUR_ORD_DATE").Value), "DD/MM/YYYY")

            TxtExchangeRate.Text = VB6.Format(IIf(IsDbNull(RsPOMain.Fields("ExchangeRate").Value), "1", RsPOMain.Fields("ExchangeRate").Value), "0.000")

            txtDivision.Text = IIf(IsDbNull(RsPOMain.Fields("DIV_CODE").Value), "", RsPOMain.Fields("DIV_CODE").Value)

            If MainClass.ValidateWithMasterTable((txtDivision.Text), "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblDivision.Text = MasterNo
            End If

            ChkActivate.CheckState = System.Windows.Forms.CheckState.Unchecked
            txtAmendNo.Text = IIf(IsDbNull(RsPOMain.Fields("AMEND_NO").Value), 0, RsPOMain.Fields("AMEND_NO").Value)
            txtAmendDate.Text = VB6.Format(IIf(IsDbNull(RsPOMain.Fields("AMEND_DATE").Value), "", RsPOMain.Fields("AMEND_DATE").Value), "DD/MM/YYYY")
            chkStatus.CheckState = IIf(RsPOMain.Fields("PO_STATUS").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            ChkActivate.CheckState = IIf(RsPOMain.Fields("PO_CLOSED").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            chkModvatable.CheckState = IIf(RsPOMain.Fields("ISMODVATABLE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkSTRefundable.CheckState = IIf(RsPOMain.Fields("ISSTREFUNDABLE").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkCapital.CheckState = IIf(RsPOMain.Fields("ISCAPITAL").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)


            mAccountCode = IIf(IsDbNull(RsPOMain.Fields("SUPP_CUST_CODE").Value), -1, RsPOMain.Fields("SUPP_CUST_CODE").Value)
            If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountName = MasterNo
            End If

            txtSupplierName.Text = mAccountName
            txtCode.Text = Trim(IIf(IsDbNull(RsPOMain.Fields("SUPP_CUST_CODE").Value), "", RsPOMain.Fields("SUPP_CUST_CODE").Value))

            '        mServiceCode = IIf(IsNull(RsPOMain!SERVICE_CODE), -1, RsPOMain!SERVICE_CODE)
            '        If MainClass.ValidateWithMasterTable(mServiceCode, "CODE", "NAME", "FIN_SERVPROV_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            '            txtServProvided.Text = Trim(MasterNo)
            '        Else
            '            txtServProvided.Text = ""
            '        End If

            lblBookType.Text = IIf(IsDbNull(RsPOMain.Fields("PUR_TYPE").Value), "", RsPOMain.Fields("PUR_TYPE").Value) & IIf(IsDbNull(RsPOMain.Fields("ORDER_TYPE").Value), "", RsPOMain.Fields("ORDER_TYPE").Value)
        End If


        txtPONo.Enabled = True
        cmdSearchPO.Enabled = True
        cmdSearchAmend.Enabled = True
        cmdSave.Enabled = False
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub

    Private Sub txtPODate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPODate.TextChanged
        cmdSave.Enabled = True
    End Sub

    Private Sub txtPONo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPONo.TextChanged
        cmdSave.Enabled = True
    End Sub
    Private Sub txtPONo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPONo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Public Sub txtPONO_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPONo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMKey As String
        Dim mPONo As Double
        Dim SqlStr As String = ""


        If Trim(txtPONo.Text) = "" Then GoTo EventExitSub

        If Len(txtPONo.Text) < 6 Then
            txtPONo.Text = Val(txtPONo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        mPONo = Val(txtPONo.Text)

        SqlStr = "SELECT * FROM PUR_PURCHASE_HDR " & " WHERE AUTO_KEY_PO='" & MainClass.AllowSingleQuote(UCase(CStr(mPONo))) & "'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        SqlStr = SqlStr & vbCrLf & " AND AMEND_NO = (" & vbCrLf & " SELECT MAX(AMEND_NO) AS AMEND_NO FROM PUR_PURCHASE_HDR" & vbCrLf & " WHERE AUTO_KEY_PO='" & MainClass.AllowSingleQuote(UCase(CStr(mPONo))) & "'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ")"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPOMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsPOMain.EOF = False Then
            Clear1()
            Show1()
        Else
            SqlStr = "SELECT * FROM PUR_PURCHASE_HDR " & " WHERE AUTO_KEY_PO='" & MainClass.AllowSingleQuote(UCase(CStr(mPONo))) & "'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

            SqlStr = SqlStr & vbCrLf & " AND AMEND_NO IN (" & vbCrLf & " SELECT MAX(AMEND_NO) FROM PUR_PURCHASE_HDR " & " WHERE AUTO_KEY_PO='" & MainClass.AllowSingleQuote(UCase(CStr(mPONo))) & "')" & vbCrLf
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPOMain, ADODB.LockTypeEnum.adLockReadOnly)
            If RsPOMain.EOF = False Then
                Clear1()
                Show1()
            Else
                MsgBox("No Such PO No. Click, Add for New", MsgBoxStyle.Information)
                txtAmendNo.Text = CStr(0)
                Cancel = True
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
