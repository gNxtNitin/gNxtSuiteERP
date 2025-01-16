Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamSaleReg
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20

    Dim mAccountCode As String
    Dim cntSearchRow As Integer

    Private Const ColLocked As Short = 1
    Private Const ColChallanDate As Short = 2
    Private Const ColChallanNo As Short = 3
    Private Const ColBillDate As Short = 4
    Private Const ColBillNo As Short = 5
    Private Const ColPartyCode As Short = 6
    Private Const ColPartyName As Short = 7
    Private Const ColBillAmount As Short = 8
    Private Const ColSaleAmount As Short = 9
    Private Const ColBED As Short = 10
    Private Const ColCESS As Short = 11
    Private Const ColSHCESS As Short = 12
    Private Const ColCST As Short = 13
    Private Const ColHGST As Short = 14
    Private Const ColSurcharge As Short = 15
    Private Const ColFreight As Short = 16
    Private Const ColDiscount As Short = 17
    Private Const ColMSC As Short = 18
    Private Const ColOthCharges As Short = 19
    Private Const ColCD As Short = 20
    Private Const ColCD_CESS As Short = 21
    Private Const ColInvType As Short = 22
    Private Const ColTINNo As Short = 23
    Private Const ColMKEY As Short = 24

    Private Const TabBillDate As Short = 0
    Private Const TabBillNo As Short = 12
    Private Const TabName As Short = 22
    Private Const TabBillAmount As Short = 66
    Private Const TabItemValue As Short = 81
    Private Const TabEDClaimed As Short = 96
    Private Const TabCESS As Short = 111
    Private Const TabSHCESS As Short = 127
    Private Const TabCST As Short = 140 ''126
    Private Const TabHGST As Short = 153 ''141
    Private Const TabSurCharge As Short = 166 ''156
    Private Const TabFreight As Short = 179 ''171
    Private Const TabDiscount As Short = 192 ''186
    Private Const TabMSC As Short = 205 ''201
    Private Const TabOtherChr As Short = 218 ''216

    Dim mClickProcess As Boolean

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub cboAgtD3_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboAgtD3.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboCancelled_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCancelled.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboCT1_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCT1.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboCT3_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCT3.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboDuty_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDuty.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboExport_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboExport.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboFOC_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboFOC.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboLocation_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboLocation.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboMRP_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboMRP.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboRejection_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboRejection.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboShow_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShow.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboShow_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShow.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        Call PrintStatus(False)
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            TxtAccount.Enabled = False
            cmdsearch.Enabled = False
        Else
            TxtAccount.Enabled = True
            cmdsearch.Enabled = True
        End If
    End Sub

    Private Sub chkAllEmp_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllEmp.CheckStateChanged
        Call PrintStatus(False)
        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtEmpName.Enabled = False
            cmdsearchEmp.Enabled = False
        Else
            txtEmpName.Enabled = True
            cmdsearchEmp.Enabled = True
        End If
    End Sub

    Private Sub chkShowTariff_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkShowTariff.CheckStateChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If mDOSPRINTING = True Then
            Call SaleReport("V")
        Else
            Call ReportForSale(Crystal.DestinationConstants.crptToWindow)
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If mDOSPRINTING = True Then
            Call SaleReport("P")
        Else
            Call ReportForSale(Crystal.DestinationConstants.crptToPrinter)
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportForSale(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String

        Report1.Reset()
        mTitle = "Sales Register"
        mSubTitle = "From : " & txtDateFrom.Text & " To : " & txtDateTo.Text

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\SALESREG.RPT"

        SqlStr = MakeSQL
        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchAccounts()
    End Sub

    Private Sub cmdsearchEmp_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchEmp.Click
        SearchEmp(("Y"))
    End Sub

    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        If FieldsVerification = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        If Show1 = False Then GoTo ErrPart
        Call PrintStatus(True)
        CalcSprdTotal()
        ''MainClass.SetFocusToCell SprdMain, mActiveRow, 4
        FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamSaleReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Sale Register"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamSaleReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)

        lblTrnType.Text = CStr(-1)
        cboAgtD3.Items.Clear()
        cboFOC.Items.Clear()
        cboRejection.Items.Clear()
        cboCancelled.Items.Clear()
        cboCT3.Items.Clear()
        cboShow.Items.Clear()
        cboLocation.Items.Clear()
        cboMRP.Items.Clear()
        cboCT1.Items.Clear()
        cboDuty.Items.Clear()

        cboAgtD3.Items.Add("BOTH")
        cboAgtD3.Items.Add("YES")
        cboAgtD3.Items.Add("NO")

        cboCT3.Items.Add("BOTH")
        cboCT3.Items.Add("YES")
        cboCT3.Items.Add("NO")

        cboFOC.Items.Add("BOTH")
        cboFOC.Items.Add("YES")
        cboFOC.Items.Add("NO")

        cboRejection.Items.Add("BOTH")
        cboRejection.Items.Add("YES")
        cboRejection.Items.Add("NO")

        cboCancelled.Items.Add("BOTH")
        cboCancelled.Items.Add("YES")
        cboCancelled.Items.Add("NO")

        cboExport.Items.Add("BOTH")
        cboExport.Items.Add("YES")
        cboExport.Items.Add("NO")

        cboCT1.Items.Add("BOTH")
        cboCT1.Items.Add("YES")
        cboCT1.Items.Add("NO")

        cboDuty.Items.Add("BOTH")
        cboDuty.Items.Add("YES")
        cboDuty.Items.Add("NO")


        cboShow.Items.Add("All")
        cboShow.Items.Add("Only Excise")
        cboShow.Items.Add("Only Service Tax")
        cboShow.Items.Add("Only Cess")
        cboShow.Items.Add("Only W/o Excise")
        cboShow.Items.Add("Only W/o Service Tax")
        cboShow.Items.Add("Only W/o Cess")

        cboMRP.Items.Add("BOTH")
        cboMRP.Items.Add("YES")
        cboMRP.Items.Add("NO")

        cboMRP.SelectedIndex = 0
        cboAgtD3.SelectedIndex = 0
        cboCT3.SelectedIndex = 0
        cboFOC.SelectedIndex = 0
        cboRejection.SelectedIndex = 0
        cboCancelled.SelectedIndex = 0
        cboExport.SelectedIndex = 0
        cboShow.SelectedIndex = 0
        cboCT1.SelectedIndex = 0
        cboDuty.SelectedIndex = 0

        Call FillInvoiceType()

        '    MainClass.FillCombo cboInvoiceType, "FIN_INVTYPE_MST", "NAME", "ALL", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY='S'"

        optType(2).Checked = True

        '    cboInvoiceType.ListIndex = 0
        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtAccount.Enabled = False
        cmdsearch.Enabled = False

        chkAllEmp.CheckState = System.Windows.Forms.CheckState.Checked
        txtEmpName.Enabled = False
        cmdsearchEmp.Enabled = False

        Call PrintStatus(True)
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamSaleReg_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdMain.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame4.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11379.7, 749)
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamSaleReg_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub lstInvoiceType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lstInvoiceType.SelectedIndexChanged
        Call PrintStatus(False)
        '    lstInvoiceType.ToolTipText = lstInvoiceType.Text
    End Sub

    Private Sub lstInvoiceType_MouseDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles lstInvoiceType.MouseDown
        Dim Button As Short = eventArgs.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim X As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim Y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
        ToolTip1.SetToolTip(lstInvoiceType, lstInvoiceType.Text)
    End Sub

    Private Sub optType_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optType.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optType.GetIndex(eventSender)
            Call PrintStatus(False)
        End If
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim SqlStr As String = ""
        Dim mMKey As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mStr1 As String
        Dim mStr2 As String
        Dim mStr As String
        'Dim cntSearchRow As Long
        'Dim mSearchKey As String
        '
        '    cntSearchRow = 1
        '    If eventArgs.row = 0 And eventArgs.col = ColBillNo Then
        '        mSearchKey = ""
        '        mSearchKey = InputBox("Enter Bill No :", "Search", mSearchKey)
        '        MainClass.SearchIntoGrid SprdMain, ColBillNo, mSearchKey, cntSearchRow
        '        cntSearchRow = cntSearchRow + 1
        '        SprdMain.SetFocus
        '    End If

        If eventArgs.row = 0 Then Exit Sub

        SprdMain.Row = eventArgs.row
        SprdMain.Col = ColMKEY
        mMKey = SprdMain.Text

        SqlStr = " SELECT VEHICLENO, CARRIERS " & vbCrLf & " FROM FIN_INVOICE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR ='" & RsCompany.Fields("FYEAR").Value & "'" & vbCrLf & " AND MKEY='" & mMKey & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mStr1 = IIf(IsDbNull(RsTemp.Fields("VEHICLENO").Value), "", RsTemp.Fields("VEHICLENO").Value)
            mStr2 = IIf(IsDbNull(RsTemp.Fields("CARRIERS").Value), "", RsTemp.Fields("CARRIERS").Value)
            mStr1 = IIf(mStr1 = "", "", "Vehicle No : " & mStr1)
            mStr2 = IIf(mStr2 = "", "", "Carriers : " & mStr2)
            mStr = mStr1 & IIf(mStr2 = "", "", IIf(mStr1 = "", "", ",") & mStr2)

            ToolTip1.SetToolTip(SprdMain, mStr)
        End If
    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub
    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent


        Dim mSearchKey As String
        Dim mCol As Integer

        mCol = SprdMain.ActiveCol
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 Then
            cntSearchRow = 1
            mSearchKey = ""
            mSearchKey = InputBox("Enter Search String :", "Search", mSearchKey)
            If mSearchKey <> "" Then
                MainClass.SearchIntoGrid(SprdMain, mCol, mSearchKey, cntSearchRow)
                cntSearchRow = cntSearchRow + 1
            End If
            SprdMain.Focus()
        End If
    End Sub

    Private Sub SprdMain_RightClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_RightClickEvent) Handles SprdMain.RightClick
        'Dim SqlStr As String=""
        'Dim mMkey As String
        'Dim RsTemp As ADODB.Recordset = Nothing
        'Dim mStr1 As String
        'Dim mStr2 As String
        'Dim mStr As String
        '
        '    SprdMain.Row = Row
        '    SprdMain.Col = ColMKEY
        '    mMkey = SprdMain.Text
        '
        '    SqlStr = " SELECT VEHICLENO, CARRIERS " & vbCrLf _
        ''            & " FROM FIN_INVOICE_HDR " & vbCrLf _
        ''            & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
        ''            & " AND FYEAR =" & RsCompany.fields("FYEAR").value & "" & vbCrLf _
        ''            & " AND MKEY='" & mMkey & "'"
        '
        '    MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, RsTemp, adLockReadOnly
        '
        '    If RsTemp.EOF = False Then
        '        mStr1 = IIf(IsNull(RsTemp!VEHICLENO), "", RsTemp!VEHICLENO)
        '        mStr2 = IIf(IsNull(RsTemp!CARRIERS), "", RsTemp!CARRIERS)
        '        mStr1 = IIf(mStr1 = "", "", "Vehicle No : " & mStr1)
        '        mStr2 = IIf(mStr2 = "", "", "Carriers : " & mStr2)
        '        mStr = mStr1 & IIf(mStr2 = "", "", "," & mStr2)
        '
        '        SprdMain.ToolTipText = mStr
        '    End If
    End Sub

    Private Sub txtAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub TxtAccount_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.DoubleClick
        SearchAccounts()
    End Sub
    Private Sub txtEmpCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpCode.DoubleClick
        SearchEmp(("N"))
    End Sub
    Private Sub txtEmpCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEmpCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtEmpCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEmpCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtEmpCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchEmp(("N"))
    End Sub

    Private Sub txtEmpCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmpCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If txtEmpCode.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable((txtEmpCode.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtEmpCode.Text = UCase(Trim(txtEmpCode.Text))
            txtEmpName.Text = UCase(Trim(MasterNo))
        Else
            MsgInformation("No Such Employee in Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtEmpName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpName.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtEmpName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpName.DoubleClick
        SearchEmp(("Y"))
    End Sub

    Private Sub txtEmpName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEmpName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtEmpName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtEmpName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtEmpName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchEmp(("Y"))
    End Sub


    Private Sub txtEmpName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmpName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If txtEmpName.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable((txtEmpName.Text), "EMP_NAME", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtEmpName.Text = UCase(Trim(txtEmpName.Text))
            txtEmpCode.Text = UCase(Trim(MasterNo))
        Else
            MsgInformation("No Such Employee in Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtTariffHeading_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTariffHeading.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateFrom) = False Then
            txtDateFrom.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then
            txtDateFrom.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateTo) = False Then
            txtDateTo.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then
            txtDateTo.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtdateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub SearchAccounts()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('C','S')"
        ''MainClass.SearchMaster TxtAccount, "FIN_SUPP_CUST_MST", "NAME", SqlStr
        MainClass.SearchGridMaster(TxtAccount.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr)
        If AcName <> "" Then
            TxtAccount.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchEmp(ByRef pIsName As String)

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If pIsName = "Y" Then
            MainClass.SearchGridMaster(txtEmpName.Text, "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr)
            If AcName <> "" Then
                txtEmpName.Text = AcName
                txtEmpCode.Text = AcName1
            End If
        Else
            MainClass.SearchGridMaster(txtEmpCode.Text, "PAY_EMPLOYEE_MST", "EMP_CODE", "EMP_NAME", , , SqlStr)
            If AcName <> "" Then
                txtEmpCode.Text = AcName
                txtEmpName.Text = AcName1
            End If
        End If

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub TxtAccount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtAccount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtAccount.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtAccount_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtAccount.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchAccounts()
    End Sub
    Private Sub txtAccount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtAccount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        lblAcCode.Text = ""
        If TxtAccount.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('C','S')"

        If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            lblAcCode.Text = MasterNo
            TxtAccount.Text = UCase(Trim(TxtAccount.Text))
        Else
            lblAcCode.Text = ""
            MsgInformation("No Such Account in Account Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        Dim cntCol As Integer
        With SprdMain
            .MaxCols = ColMKEY
            .set_RowHeight(0, RowHeight * 1.25)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColLocked
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColLocked, 15)
            .ColHidden = True

            .Col = ColChallanDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColChallanDate, 9)
            .ColHidden = IIf(lblBookType.Text = "S", True, False)

            .Col = ColChallanNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColChallanNo, 9)
            .ColHidden = IIf(lblBookType.Text = "S", True, False)

            .Col = ColBillDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBillDate, 9)
            .ColHidden = IIf(lblBookType.Text = "S", True, False)

            .Col = ColBillNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBillNo, 9)
            .ColHidden = IIf(lblBookType.Text = "S", True, False)


            '        If OptSumDet(0).Value = True Then
            '            .ColHidden = False
            '        Else
            '            .ColHidden = True
            '        End If

            .Col = ColPartyCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyCode, 6)

            .Col = ColPartyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyName, 15)
            '        If OptSumDet(0).Value = True Then
            '            .ColHidden = False
            '            .ColsFrozen = ColAcctName
            '        Else
            '            .ColHidden = True
            '        End If
            .ColsFrozen = ColPartyName

            For cntCol = ColBillAmount To ColCD_CESS
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 2
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                '            .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 12)
            Next

            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColMKEY, 8)
            .ColHidden = True


            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            '        SprdMain.OperationMode = OperationModeNormal
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            SprdMain.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Function Show1() As Boolean

        On Error GoTo LedgError
        Dim RsOP As ADODB.Recordset
        Dim mOpening As Double
        Dim mOpDr As Double
        Dim mOpCr As Double
        Dim SqlStr As String = ""
        Dim SqlStr1 As String
        Dim SqlStr2 As String

        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQL
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        '********************************
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function

    Private Function MakeSQL() As String

        On Error GoTo ERR1
        Dim mTrnCode As Integer
        Dim mTrnTypeStr As String = ""
        Dim CntLst As Integer
        Dim mInvoiceType As String
        Dim mShowAll As Boolean
        Dim mDivision As Double
        Dim mAccountCode As String
        Dim mEmpCode As String
        ''SELECT CLAUSE...

        If lblBookType.Text = "D" Then
            MakeSQL = " SELECT '', IH.DCDATE, IH.AUTO_KEY_DESP, " & vbCrLf & " IH.INVOICE_DATE, IH.BILLNO,  "
        Else
            MakeSQL = " SELECT '', '', '', " & vbCrLf & " '', '',  "
        End If

        MakeSQL = MakeSQL & vbCrLf & " CMST.SUPP_CUST_CODE,CMST.SUPP_CUST_NAME, "

        If lblBookType.Text = "D" Then
            MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(DECODE(CANCELLED,'Y',0,IH.NETVALUE)), TO_CHAR(DECODE(CANCELLED,'Y',0,IH.ITEMVALUE)), TO_CHAR(DECODE(CANCELLED,'Y',0,DECODE(IH.TOTEDAMOUNT,0,IH.TOTSERVICEAMOUNT,IH.TOTEDAMOUNT)))  , " & vbCrLf & " TO_CHAR(DECODE(CANCELLED,'Y',0,IH.TOTEDUAMOUNT)), " & vbCrLf & " TO_CHAR(DECODE(CANCELLED,'Y',0,IH.TOTSHECAMOUNT)), " & vbCrLf & " TO_CHAR(DECODE(CANCELLED,'Y',0,DECODE(WITHIN_STATE,'N',IH.TOTSTAMT,0)))," & vbCrLf & " TO_CHAR(DECODE(CANCELLED,'Y',0,DECODE(WITHIN_STATE,'Y',IH.TOTSTAMT,0)))," & vbCrLf & " TO_CHAR(DECODE(CANCELLED,'Y',0,TOTSURCHARGEAMT)),TO_CHAR(DECODE(CANCELLED,'Y',0,TOTFREIGHT))," & vbCrLf & " TO_CHAR(DECODE(CANCELLED,'Y',0,TOTDISCAMOUNT)),TO_CHAR(DECODE(CANCELLED,'Y',0,TOTMSCAMOUNT))," & vbCrLf & " TO_CHAR(DECODE(CANCELLED,'Y',0,TO_CHAR(NVL(TOT_EXPORTEXP,0)+NVL(TOTCHARGES,0)+NVL(TOTRO,0)))), " & vbCrLf & " TO_CHAR(DECODE(CANCELLED,'Y',0,IH.TOT_CUSTOMDUTY)), TO_CHAR(DECODE(CANCELLED,'Y',0,IH.TOT_CD_CESS)),  "
        Else
            MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(DECODE(CANCELLED,'Y',0,IH.NETVALUE))), " & vbCrLf & " TO_CHAR(SUM(DECODE(CANCELLED,'Y',0,IH.ITEMVALUE))), " & vbCrLf & " TO_CHAR(SUM(DECODE(CANCELLED,'Y',0,DECODE(IH.TOTEDAMOUNT,0,IH.TOTSERVICEAMOUNT,IH.TOTEDAMOUNT))))  , " & vbCrLf & " TO_CHAR(SUM(DECODE(CANCELLED,'Y',0,IH.TOTEDUAMOUNT))), " & vbCrLf & " TO_CHAR(SUM(DECODE(CANCELLED,'Y',0,IH.TOTSHECAMOUNT))), " & vbCrLf & " TO_CHAR(SUM(DECODE(CANCELLED,'Y',0,DECODE(WITHIN_STATE,'N',IH.TOTSTAMT,0))))," & vbCrLf & " TO_CHAR(SUM(DECODE(CANCELLED,'Y',0,DECODE(WITHIN_STATE,'Y',IH.TOTSTAMT,0))))," & vbCrLf & " TO_CHAR(SUM(DECODE(CANCELLED,'Y',0,TOTSURCHARGEAMT))),TO_CHAR(SUM(DECODE(CANCELLED,'Y',0,TOTFREIGHT)))," & vbCrLf & " TO_CHAR(SUM(DECODE(CANCELLED,'Y',0,TOTDISCAMOUNT))),TO_CHAR(SUM(DECODE(CANCELLED,'Y',0,TOTMSCAMOUNT)))," & vbCrLf & " TO_CHAR(SUM(DECODE(CANCELLED,'Y',0,TO_CHAR(NVL(TOT_EXPORTEXP,0)+NVL(TOTCHARGES,0)+NVL(TOTRO,0))))), " & vbCrLf & " TO_CHAR(SUM(DECODE(CANCELLED,'Y',0,IH.TOT_CUSTOMDUTY))), TO_CHAR(SUM(DECODE(CANCELLED,'Y',0,IH.TOT_CD_CESS))),  "
        End If

        If lblBookType.Text = "D" Then
            MakeSQL = MakeSQL & vbCrLf & " INVMST.NAME, CMST.ACCOUNT_CODE, IH.MKEY"
        Else
            MakeSQL = MakeSQL & vbCrLf & " INVMST.NAME, CMST.ACCOUNT_CODE,  ''"
        End If

        ''FROM CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_SUPP_CUST_MST CMST, FIN_INVTYPE_MST INVMST"

        ''WHERE CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR='" & RsCompany.Fields("FYEAR").Value & "'" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND IH.TRNTYPE=INVMST.CODE AND IH.INVOICESEQTYPE NOT IN (7,8)"

        MakeSQL = MakeSQL & vbCrLf & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "')" & vbCrLf & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "')"


        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                lblAcCode.Text = MasterNo
            Else
                lblAcCode.Text = "-1"
            End If
            mAccountCode = Trim(lblAcCode.Text)
            MakeSQL = MakeSQL & vbCrLf & "AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(lblAcCode.text) & "'"
        Else
            mAccountCode = "-1"
        End If

        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            '        If MainClass.ValidateWithMasterTable(txtEmpName.Text, "EMP_NAME", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
            mEmpCode = Trim(txtEmpCode.Text) '' MasterNo
            MakeSQL = MakeSQL & vbCrLf & "AND CMST.EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'"
            '        End If
        End If

        If optType(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "AND CMST.WITHIN_STATE='Y'"
        ElseIf optType(1).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & "AND CMST.WITHIN_STATE='N'"
        End If

        '    If cboInvoiceType.Text = "ALL" Then
        '        MakeSQL = MakeSQL & vbCrLf & "AND INVMST.IDENTIFICATION<>'J'"
        '    Else
        '        MakeSQL = MakeSQL & vbCrLf & "AND IH.TRNTYPE=" & Val(lblTrnType.text) & ""
        '    End If

        mShowAll = True
        For CntLst = 1 To lstInvoiceType.Items.Count - 1
            If lstInvoiceType.GetItemChecked(CntLst) = True Then
                '            lstInvoiceType.ListIndex = CntLst
                mInvoiceType = VB6.GetItemString(lstInvoiceType, CntLst)
                If MainClass.ValidateWithMasterTable(mInvoiceType, "NAME", "CODE", "FIN_INVTYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mTrnCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                End If
                mTrnTypeStr = IIf(mTrnTypeStr = "", mTrnCode, mTrnTypeStr & "," & mTrnCode)
            Else
                mShowAll = False
            End If
        Next

        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable(cboDivision.Text, "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND IH.DIV_CODE=" & mDivision & ""
            End If
        Else
            mDivision = -1
        End If

        If mShowAll = False Then
            If mTrnTypeStr <> "" Then
                mTrnTypeStr = "(" & mTrnTypeStr & ")"
                MakeSQL = MakeSQL & vbCrLf & " AND IH.TRNTYPE IN " & mTrnTypeStr & ""
            End If
        End If

        If cboAgtD3.SelectedIndex > 0 Then
            '        MakeSQL = MakeSQL & vbCrLf & "AND IH.AGTD3='" & vb.Left(cboAgtD3.Text, 1) & "'"
            If cboAgtD3.SelectedIndex = 1 Then
                MakeSQL = MakeSQL & vbCrLf & " AND IH.REF_DESP_TYPE ='S'"
            Else
                MakeSQL = MakeSQL & vbCrLf & " AND IH.REF_DESP_TYPE <>'S'"
            End If
        End If

        If cboCT3.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.AGTCT3='" & VB.Left(cboCT3.Text, 1) & "'"
        End If

        If cboCT1.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.AGTCT1='" & VB.Left(cboCT1.Text, 1) & "'"
        End If

        If cboDuty.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.ISDUTY_FORGONE='" & VB.Left(cboDuty.Text, 1) & "'"
        End If


        If cboFOC.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.FOC='" & VB.Left(cboFOC.Text, 1) & "'"
        End If

        If cboRejection.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.REJECTION='" & VB.Left(cboRejection.Text, 1) & "'"
        End If

        If cboMRP.SelectedIndex = 1 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.TAX_ON_MRP='Y'"
        ElseIf cboMRP.SelectedIndex = 2 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.TAX_ON_MRP='N'"
        End If

        If cboCancelled.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.CANCELLED='" & VB.Left(cboCancelled.Text, 1) & "'"
        End If

        If chkShowTariff.CheckState = System.Windows.Forms.CheckState.Checked Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.TARIFFHEADING IS NULL"
        Else
            If Trim(txtTariffHeading.Text) <> "" Then
                MakeSQL = MakeSQL & vbCrLf & "AND IH.TARIFFHEADING='" & MainClass.AllowSingleQuote(txtTariffHeading.Text) & "'"
            End If
        End If

        If cboExport.SelectedIndex = 1 Then
            MakeSQL = MakeSQL & vbCrLf & "AND CMST.WITHIN_COUNTRY='N'"
        ElseIf cboExport.SelectedIndex = 2 Then
            MakeSQL = MakeSQL & vbCrLf & "AND CMST.WITHIN_COUNTRY='Y'"
        End If

        If cboLocation.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.DESP_LOCATION='" & MainClass.AllowSingleQuote(cboLocation.Text) & "'"
        End If

        If cboShow.SelectedIndex = 1 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.TOTEDAMOUNT>0"
        ElseIf cboShow.SelectedIndex = 2 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.TOTSERVICEAMOUNT>0"
        ElseIf cboShow.SelectedIndex = 3 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.TOTEDUAMOUNT>0"
        ElseIf cboShow.SelectedIndex = 4 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.TOTEDAMOUNT=0"
        ElseIf cboShow.SelectedIndex = 5 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.TOTSERVICEAMOUNT=0"
        ElseIf cboShow.SelectedIndex = 6 Then
            MakeSQL = MakeSQL & vbCrLf & "AND IH.TOTEDUAMOUNT=0"
        End If


        ''ORDER CLAUSE...
        If lblBookType.Text = "S" Then
            MakeSQL = MakeSQL & vbCrLf & "GROUP BY " & vbCrLf & " CMST.SUPP_CUST_CODE,CMST.SUPP_CUST_NAME,INVMST.NAME, CMST.ACCOUNT_CODE "

            MakeSQL = MakeSQL & vbCrLf & "ORDER BY CMST.SUPP_CUST_NAME"

        Else
            MakeSQL = MakeSQL & vbCrLf & "ORDER BY IH.INVOICE_DATE,IH.BILLNO"
        End If



        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then txtDateFrom.Focus()
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then txtDateTo.Focus()
        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(TxtAccount.Text) = "" Then
                MsgInformation("Invaild Account Name")
                TxtAccount.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
            Else
                MsgInformation("Invaild Account Name")
                TxtAccount.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If

        If chkAllEmp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtEmpName.Text) = "" Then
                MsgInformation("Invaild Employee Name")
                txtEmpName.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((txtEmpName.Text), "EMP_NAME", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invaild Employee Name")
                txtEmpName.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Private Sub CalcSprdTotal()

        On Error GoTo ErrPart
        Dim cntRow As Integer

        Dim mBillAmount As Double
        Dim mSaleAmount As Double
        Dim mBED As Double
        Dim mCess As Double
        Dim mCST As Double
        Dim mHGST As Double
        Dim mSurcharge As Double
        Dim mFreight As Double
        Dim mDiscount As Double
        Dim mMSC As Double
        Dim mOthCharges As Double
        Dim mSHCess As Double
        Dim mCD As Double
        Dim mCD_Cess As Double

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColBillAmount
                mBillAmount = mBillAmount + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColSaleAmount
                mSaleAmount = mSaleAmount + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColBED
                mBED = mBED + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColCESS
                mCess = mCess + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColSHCESS
                mSHCess = mSHCess + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColCST
                mCST = mCST + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColHGST
                mHGST = mHGST + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColSurcharge
                mSurcharge = mSurcharge + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColFreight
                mFreight = mFreight + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColDiscount
                mDiscount = mDiscount + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColMSC
                mMSC = mMSC + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColOthCharges
                mOthCharges = mOthCharges + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColCD
                mCD = mCD + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

                .Col = ColCD_CESS
                mCD_Cess = mCD_Cess + Val(CStr(CDbl(IIf(IsNumeric(.Text), .Text, 0))))

            Next

            Call MainClass.AddBlankfpSprdRow(SprdMain, ColPartyName)
            .Col = ColPartyName
            .Row = .MaxRows
            .Text = "GRAND TOTAL :"
            .Font = VB6.FontChangeBold(.Font, True)

            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = 1
            .col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80
            .BlockMode = False

            .Row = .MaxRows

            .Col = ColBillAmount
            .Text = VB6.Format(mBillAmount, "0.00")

            .Col = ColSaleAmount
            .Text = VB6.Format(mSaleAmount, "0.00")

            .Col = ColBED
            .Text = VB6.Format(mBED, "0.00")

            .Col = ColCESS
            .Text = VB6.Format(mCess, "0.00")

            .Col = ColSHCESS
            .Text = VB6.Format(mSHCess, "0.00")

            .Col = ColCST
            .Text = VB6.Format(mCST, "0.00")

            .Col = ColHGST
            .Text = VB6.Format(mHGST, "0.00")

            .Col = ColSurcharge
            .Text = VB6.Format(mSurcharge, "0.00")

            .Col = ColFreight
            .Text = VB6.Format(mFreight, "0.00")

            .Col = ColDiscount
            .Text = VB6.Format(mDiscount, "0.00")

            .Col = ColMSC
            .Text = VB6.Format(mMSC, "0.00")

            .Col = ColOthCharges
            .Text = VB6.Format(mOthCharges, "0.00")

            .Col = ColCD
            .Text = VB6.Format(mCD, "0.00")

            .Col = ColCD_CESS
            .Text = VB6.Format(mCD_Cess, "0.00")

        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub

    Private Function SaleReport(ByRef pPrintMode As String) As Boolean
        On Error GoTo ErrPart
        Dim mLineCount As Integer
        Dim pPageNo As Integer
        Dim cntRow As Double
        Dim mPrintFooter As Boolean

        Dim mBillAmount As Double
        Dim mItemValue As Double
        Dim mEDAmount As Double
        Dim mCessAmount As Double
        Dim mSHCESSAmount As Double
        Dim mCST As Double
        Dim mHGST As Double
        Dim mSurcharge As Double
        Dim mFreight As Double
        Dim mDiscount As Double
        Dim mMSC As Double
        Dim mOCharges As Double
        Dim pFileName As String

        mBillAmount = 0
        mItemValue = 0
        mEDAmount = 0
        mCessAmount = 0
        mCST = 0
        mHGST = 0
        mSurcharge = 0
        mFreight = 0
        mDiscount = 0
        mMSC = 0
        mOCharges = 0
        mSHCESSAmount = 0

        mLineCount = 1
        pFileName = mLocalPath & "\Report.Prn"
        ''Shell "ATTRIB +A -R " & pFileName

        Call ShellAndContinue("ATTRIB +A -R " & pFileName)

        With SprdMain
            If .MaxRows >= 1 Then

                FileOpen(1, pFileName, OpenMode.Output)
                For cntRow = 1 To .MaxRows - 1
                    If mLineCount = 1 Then
                        pPageNo = pPageNo + 1
                        Call PrintHeader()
                        mLineCount = 12
                        mPrintFooter = False
                        If pPageNo <> 1 Then
                            Call PrintPageTotal("Brought Forward : ", mLineCount, mBillAmount, mItemValue, mEDAmount, mCessAmount, mSHCESSAmount, mCST, mHGST, mSurcharge, mFreight, mDiscount, mMSC, mOCharges)
                            mLineCount = mLineCount + 1
                        End If
                    End If

                    .Row = cntRow

                    .Col = ColBillAmount
                    mBillAmount = CDbl(VB6.Format(mBillAmount + CDbl(IIf(IsNumeric(.Text), .Text, 0)), "0.00"))

                    .Col = ColSaleAmount
                    mItemValue = CDbl(VB6.Format(mItemValue + CDbl(IIf(IsNumeric(.Text), .Text, 0)), "0.00"))

                    .Col = ColBED
                    mEDAmount = CDbl(VB6.Format(mEDAmount + CDbl(IIf(IsNumeric(.Text), .Text, 0)), "0.00"))

                    .Col = ColCESS
                    mCessAmount = CDbl(VB6.Format(mCessAmount + CDbl(IIf(IsNumeric(.Text), .Text, 0)), "0.00"))

                    .Col = ColSHCESS
                    mSHCESSAmount = CDbl(VB6.Format(mSHCESSAmount + CDbl(IIf(IsNumeric(.Text), .Text, 0)), "0.00"))

                    .Col = ColCST
                    mCST = CDbl(VB6.Format(mCST + CDbl(IIf(IsNumeric(.Text), .Text, 0)), "0.00"))

                    .Col = ColHGST
                    mHGST = CDbl(VB6.Format(mHGST + CDbl(IIf(IsNumeric(.Text), .Text, 0)), "0.00"))

                    .Col = ColSurcharge
                    mSurcharge = CDbl(VB6.Format(mSurcharge + CDbl(IIf(IsNumeric(.Text), .Text, 0)), "0.00"))

                    .Col = ColFreight
                    mFreight = CDbl(VB6.Format(mFreight + CDbl(IIf(IsNumeric(.Text), .Text, 0)), "0.00"))

                    .Col = ColDiscount
                    mDiscount = CDbl(VB6.Format(mDiscount + CDbl(IIf(IsNumeric(.Text), .Text, 0)), "0.00"))

                    .Col = ColMSC
                    mMSC = CDbl(VB6.Format(mMSC + CDbl(IIf(IsNumeric(.Text), .Text, 0)), "0.00"))

                    .Col = ColOthCharges
                    mOCharges = CDbl(VB6.Format(mOCharges + CDbl(IIf(IsNumeric(.Text), .Text, 0)), "0.00"))

                    Call PrintDetail(cntRow, mLineCount)

                    If mLineCount >= 60 And mPrintFooter = False Then
                        PrintLine(1, TAB(0), New String("-", 230))
                        mLineCount = mLineCount + 1
                        Call PrintPageTotal("Page Total :", mLineCount, mBillAmount, mItemValue, mEDAmount, mCessAmount, mSHCESSAmount, mCST, mHGST, mSurcharge, mFreight, mDiscount, mMSC, mOCharges)
                        Call PrintFooter(pPageNo, mLineCount, mPrintFooter)
                    ElseIf cntRow = SprdMain.MaxRows - 1 Then
                        Do While mLineCount <= 60
                            PrintLine(1, " ")
                            mLineCount = mLineCount + 1
                        Loop
                        PrintLine(1, TAB(0), New String("-", 230))
                        mLineCount = mLineCount + 1
                        Call PrintPageTotal("Grand Total :", mLineCount, mBillAmount, mItemValue, mEDAmount, mCessAmount, mSHCESSAmount, mCST, mHGST, mSurcharge, mFreight, mDiscount, mMSC, mOCharges)
                        Call PrintFooter(pPageNo, mLineCount, mPrintFooter)
                    End If
                Next
                FileClose(1)
            End If
        End With

        Dim mFP As Boolean
        If pPrintMode = "P" Then
            mFP = Shell(My.Application.Info.DirectoryPath & "\PrintReport.bat", AppWinStyle.NormalFocus)
            If mFP = False Then GoTo ErrPart
            '        Shell App.path & "\PrintReport.bat",vbNormalFocus
        Else
            Shell("ATTRIB +R -A " & pFileName)
            Shell("NOTEPAD.EXE " & pFileName, AppWinStyle.MaximizedFocus)
            'App.Path & "\RVIEW.EXE "
        End If

        SaleReport = True
        Exit Function
ErrPart:
        MsgBox(Err.Description)
        SaleReport = False
        ''Resume
        FileClose(1)
    End Function

    Private Function PrintPageTotal(ByRef pRemarks As String, ByRef mLineCount As Integer, ByRef pBillAmount As Double, ByRef pItemValue As Double, ByRef pEDAmount As Double, ByRef pCESSAmount As Double, ByRef pSHCESSAmount As Double, ByRef pCST As Double, ByRef pHGST As Double, ByRef pSurcharge As Double, ByRef pFreight As Double, ByRef pDiscount As Double, ByRef pMSC As Double, ByRef pOCharges As Double) As Boolean
        On Error GoTo ErrPart

        Print(1, TAB(TabName), pRemarks)
        Print(1, TAB(TabBillAmount), New String(" ", TabItemValue - TabBillAmount - 1 - Len(Trim(VB6.Format(pBillAmount, "0.00")))) & Trim(VB6.Format(pBillAmount, "0.00")))
        Print(1, TAB(TabItemValue), New String(" ", TabEDClaimed - TabItemValue - 1 - Len(Trim(VB6.Format(pItemValue, "0.00")))) & Trim(VB6.Format(pItemValue, "0.00")))
        Print(1, TAB(TabEDClaimed), New String(" ", TabCESS - TabEDClaimed - 1 - Len(Trim(VB6.Format(pEDAmount, "0.00")))) & Trim(VB6.Format(pEDAmount, "0.00")))
        Print(1, TAB(TabCESS), New String(" ", TabSHCESS - TabCESS - 1 - Len(Trim(VB6.Format(pCESSAmount, "0.00")))) & Trim(VB6.Format(pCESSAmount, "0.00")))
        Print(1, TAB(TabSHCESS), New String(" ", TabCST - TabSHCESS - 1 - Len(Trim(VB6.Format(pSHCESSAmount, "0.00")))) & Trim(VB6.Format(pSHCESSAmount, "0.00")))
        Print(1, TAB(TabCST), New String(" ", TabHGST - TabCST - 1 - Len(Trim(VB6.Format(pCST, "0.00")))) & Trim(VB6.Format(pCST, "0.00")))
        Print(1, TAB(TabHGST), New String(" ", TabSurCharge - TabHGST - 1 - Len(Trim(VB6.Format(pHGST, "0.00")))) & Trim(VB6.Format(pHGST, "0.00")))
        Print(1, TAB(TabSurCharge), New String(" ", TabFreight - TabSurCharge - 1 - Len(Trim(VB6.Format(pSurcharge, "0.00")))) & Trim(VB6.Format(pSurcharge, "0.00")))
        Print(1, TAB(TabFreight), New String(" ", TabDiscount - TabFreight - 1 - Len(Trim(VB6.Format(pFreight, "0.00")))) & Trim(VB6.Format(pFreight, "0.00")))
        Print(1, TAB(TabDiscount), New String(" ", TabMSC - TabDiscount - 1 - Len(Trim(VB6.Format(pDiscount, "0.00")))) & Trim(VB6.Format(pDiscount, "0.00")))
        Print(1, TAB(TabMSC), New String(" ", TabOtherChr - TabMSC - 1 - Len(Trim(VB6.Format(pMSC, "0.00")))) & Trim(VB6.Format(pMSC, "0.00")))
        PrintLine(1, TAB(TabOtherChr), New String(" ", 230 - TabOtherChr - 1 - Len(Trim(VB6.Format(pOCharges, "0.00")))) & Trim(VB6.Format(pOCharges, "0.00")))

        mLineCount = mLineCount + 1
        PrintPageTotal = True
        Exit Function
ErrPart:
        MsgBox(Err.Description)
        PrintPageTotal = False
        ''Resume
    End Function
    Private Function PrintFooter(ByRef xPageNo As Integer, ByRef mLineCount As Integer, ByRef pPrintFooter As Boolean) As Boolean
        On Error GoTo ErrPart

        Do While mLineCount <= 63
            PrintLine(1, " ")
            mLineCount = mLineCount + 1
        Loop

        PrintLine(1, TAB(0), New String("-", 230))
        PrintLine(1, TAB(TabMSC), "Page No. : " & xPageNo & Chr(12) & Chr(18))
        mLineCount = 1
        PrintFooter = True
        pPrintFooter = True
        Exit Function
ErrPart:
        MsgBox(Err.Description)
        PrintFooter = False
        pPrintFooter = False
    End Function
    Private Function PrintDetail(ByRef mRow As Double, ByRef mLineCount As Integer) As Boolean
        On Error GoTo ErrPart



        With SprdMain
            .Row = mRow

            .Col = ColBillDate
            Print(1, TAB(TabBillDate), Trim(.Text))

            .Col = ColBillNo
            Print(1, TAB(TabBillNo), Mid(Trim(.Text), 2))

            .Col = ColPartyName
            Print(1, TAB(TabName), GetMultiLine(Trim(.Text), mLineCount, TabBillAmount - TabName - 1, TabName))

            .Col = ColBillAmount
            Print(1, TAB(TabBillAmount), New String(" ", TabItemValue - TabBillAmount - 1 - Len(Trim(.Text))) & Trim(.Text))

            .Col = ColSaleAmount
            Print(1, TAB(TabItemValue), New String(" ", TabEDClaimed - TabItemValue - 1 - Len(Trim(.Text))) & Trim(.Text))

            .Col = ColBED
            Print(1, TAB(TabEDClaimed), New String(" ", TabCESS - TabEDClaimed - 1 - Len(Trim(.Text))) & Trim(.Text))

            .Col = ColCESS
            Print(1, TAB(TabCESS), New String(" ", TabSHCESS - TabCESS - 1 - Len(Trim(.Text))) & Trim(.Text))

            .Col = ColSHCESS
            Print(1, TAB(TabSHCESS), New String(" ", TabCST - TabSHCESS - 1 - Len(Trim(.Text))) & Trim(.Text))

            .Col = ColCST
            Print(1, TAB(TabCST), New String(" ", TabHGST - TabCST - 1 - Len(Trim(.Text))) & Trim(.Text))

            .Col = ColHGST
            Print(1, TAB(TabHGST), New String(" ", TabSurCharge - TabHGST - 1 - Len(Trim(.Text))) & Trim(.Text))

            .Col = ColSurcharge
            Print(1, TAB(TabSurCharge), New String(" ", TabFreight - TabSurCharge - 1 - Len(Trim(.Text))) & Trim(.Text))

            .Col = ColFreight
            Print(1, TAB(TabFreight), New String(" ", TabDiscount - TabFreight - 1 - Len(Trim(.Text))) & Trim(.Text))

            .Col = ColDiscount
            Print(1, TAB(TabDiscount), New String(" ", TabMSC - TabDiscount - 1 - Len(Trim(.Text))) & Trim(.Text))

            .Col = ColMSC
            Print(1, TAB(TabMSC), New String(" ", TabOtherChr - TabMSC - 1 - Len(Trim(.Text))) & Trim(.Text))

            .Col = ColOthCharges
            PrintLine(1, TAB(TabOtherChr), New String(" ", 230 - TabOtherChr - 1 - Len(Trim(.Text))) & Trim(.Text))

            mLineCount = mLineCount + 1
        End With

        PrintLine(1, " ")
        mLineCount = mLineCount + 1

        PrintDetail = True
        Exit Function
ErrPart:
        MsgBox(Err.Description)
        PrintDetail = False
        'Resume
    End Function
    Private Function PrintHeader() As Boolean
        On Error GoTo ErrPart
        Dim mTitle As String = ""
        Dim mTitle1 As String = ""
        Dim CntLst As Integer = ""
        Dim mInvoiceType As String = ""
        Dim mSelected As Boolean

        PrintLine(1, TAB(0), " ")
        PrintLine(1, TAB(0), " ")

        PrintLine(1, TAB(0), Chr(15) & Chr(14) & RsCompany.Fields("COMPANY_NAME").Value)
        PrintLine(1, TAB(0), " ") ''xCompanyAddr

        '    mTitle = IIf(UCase(cboInvoiceType.Text) = "ALL", "Sale Register", UCase(cboInvoiceType.Text))

        mSelected = True
        For CntLst = 1 To lstInvoiceType.Items.Count - 1
            If lstInvoiceType.GetItemChecked(CntLst) = True Then
                mInvoiceType = VB6.GetItemString(lstInvoiceType, CntLst)
                mTitle = IIf(mTitle = "", mInvoiceType, mTitle & "/" & mInvoiceType)
            Else
                mSelected = False
            End If
        Next
        If mSelected = True Then
            mTitle = "Sale Register"
        End If

        If Trim(txtTariffHeading.Text) <> "" Then
            mTitle = mTitle & " (" & txtTariffHeading.Text & ")"
        End If

        If cboAgtD3.SelectedIndex = 1 Then
            mTitle1 = "Agt. D3"
        End If

        If cboCT3.SelectedIndex = 1 Then
            mTitle1 = mTitle1 & IIf(mTitle1 = "", "Agt. CT3", "/" & "Agt. CT3")
        End If

        If cboFOC.SelectedIndex = 1 Then
            mTitle1 = mTitle1 & IIf(mTitle1 = "", "FOC", "/" & "FOC")
        End If

        If cboRejection.SelectedIndex = 1 Then
            mTitle1 = mTitle1 & IIf(mTitle1 = "", "Rejection", "/" & "Rejection")
        End If

        If cboCancelled.SelectedIndex = 1 Then
            mTitle1 = mTitle1 & IIf(mTitle1 = "", "Cancelled", "/" & "Cancelled")
        End If

        If Trim(mTitle1) <> "" Then
            mTitle = mTitle & " (" & mTitle1 & ")"
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            mTitle = mTitle & " (Account : " & TxtAccount.Text & ")"
        End If

        PrintLine(1, TAB(0), Chr(27) & Chr(69) & mTitle & Chr(27) & Chr(70))
        PrintLine(1, TAB(0), Chr(27) & Chr(69) & "For the period : " & VB6.Format(txtDateFrom.Text, "DD/MM/YYYY") & "-" & VB6.Format(txtDateTo.Text, "DD/MM/YYYY") & Chr(27) & Chr(70))
        PrintLine(1, TAB(0), New String("-", 230))

        Print(1, TAB(TabBillDate), "Bill Date")
        Print(1, TAB(TabBillNo), "Bill No.")
        Print(1, TAB(TabName), "Party Name")
        Print(1, TAB(TabBillAmount), New String(" ", TabItemValue - TabBillAmount - 1 - Len("Bill Amount")) & "Bill Amount")
        Print(1, TAB(TabItemValue), New String(" ", TabEDClaimed - TabItemValue - 1 - Len("Sale Amount")) & "Sale Amount")
        Print(1, TAB(TabEDClaimed), New String(" ", TabCESS - TabEDClaimed - 1 - Len("Basic Excise")) & "Basic Excise")
        Print(1, TAB(TabCESS), New String(" ", TabSHCESS - TabCESS - 1 - Len("CESS Amount")) & "CESS Amount")
        Print(1, TAB(TabSHCESS), New String(" ", TabCST - TabSHCESS - 1 - Len("S.H.E. CESS")) & "S.H.E. CESS")
        Print(1, TAB(TabCST), New String(" ", TabHGST - TabCST - 1 - Len("CST")) & "CST")
        Print(1, TAB(TabHGST), New String(" ", TabSurCharge - TabHGST - 1 - Len("HGST")) & "HGST")
        Print(1, TAB(TabSurCharge), New String(" ", TabFreight - TabSurCharge - 1 - Len("Surcharge")) & "Surcharge")
        Print(1, TAB(TabFreight), New String(" ", TabDiscount - TabFreight - 1 - Len("Freight")) & "Freight")
        Print(1, TAB(TabDiscount), New String(" ", TabMSC - TabDiscount - 1 - Len("Discount")) & "Discount")
        Print(1, TAB(TabMSC), New String(" ", TabOtherChr - TabMSC - 1 - Len("Material")) & "Material")
        PrintLine(1, TAB(TabOtherChr), New String(" ", 230 - TabOtherChr - 1 - Len("Other")) & "Other")


        Print(1, TAB(TabEDClaimed), New String(" ", TabCESS - TabEDClaimed - 1 - Len("Duty")) & "Duty")
        Print(1, TAB(TabSHCESS), New String(" ", TabCST - TabSHCESS - 1 - Len("Amount")) & "Amount")
        Print(1, TAB(TabMSC), New String(" ", TabOtherChr - TabMSC - 1 - Len("Supplied")) & "Supplied")
        PrintLine(1, TAB(TabOtherChr), New String(" ", 230 - TabOtherChr - 1 - Len("Charges")) & "Charges")


        PrintLine(1, TAB(TabMSC), New String(" ", TabOtherChr - TabMSC - 1 - Len("By Client")) & "By Client")

        PrintLine(1, TAB(0), New String("-", 230) & Chr(15))



        PrintHeader = True
        Exit Function
ErrPart:
        MsgBox(Err.Description)
        PrintHeader = False
        Resume
    End Function

    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick
        Dim xVDate As String
        Dim xMkey As String = ""
        Dim xVNo As String
        Dim xBookType As String = ""
        'Dim xBookSubType As String


        SprdMain.Row = SprdMain.ActiveRow

        SprdMain.Col = ColBillDate
        xVDate = Me.SprdMain.Text

        SprdMain.Col = ColMKEY
        xMKey = Me.SprdMain.Text

        SprdMain.Col = ColBillNo
        xVNo = Me.SprdMain.Text


        Call ShowTrn(xMkey, xVDate, "", xVNo, "S", "", Me)

    End Sub

    Private Sub SprdMain_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdMain.KeyDownEvent
        If eventArgs.KeyCode = System.Windows.Forms.Keys.Return Then
            SprdMain_DblClick(SprdMain, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(1, 1))
        End If
        '    If KeyCode = vbKeyEscape Then cmdClose = True
    End Sub

    Private Sub txtTariffHeading_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTariffHeading.DoubleClick
        SearchTariff()
    End Sub

    Private Sub txtTariffHeading_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTariffHeading.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTariffHeading_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtTariffHeading.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchTariff()
    End Sub

    Private Sub txtTariffHeading_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTariffHeading.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtTariffHeading.Text) = "" Then GoTo EventExitSub

        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable((txtTariffHeading.Text), "TARRIF_CODE", "TARRIF_DESC", "FIN_TARRIF_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            ErrorMsg("Please Enter Vaild Tariff.", "", MsgBoxStyle.Critical)
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub SearchTariff()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster((txtTariffHeading.Text), "FIN_TARRIF_MST", "TARRIF_CODE", "TARRIF_DESC", , , SqlStr) = True Then
            txtTariffHeading.Text = AcName
            '        txtTariff_Validate False
            If txtTariffHeading.Enabled = True Then txtTariffHeading.Focus()
        End If


        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub FillInvoiceType()

        On Error GoTo FillErr2
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing
        Dim CntLst As Integer

        lstInvoiceType.Items.Clear()
        SqlStr = "SELECT NAME FROM FIN_INVTYPE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND CATEGORY='S' AND IDENTIFICATION NOT IN ('G','S') ORDER BY NAME"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If RS.EOF = False Then
            lstInvoiceType.Items.Add("ALL")
            Do While RS.EOF = False
                lstInvoiceType.Items.Add(RS.Fields("Name").Value)
                lstInvoiceType.SetItemChecked(CntLst, True)
                RS.MoveNext()
                CntLst = CntLst + 1
            Loop
        End If

        lstInvoiceType.SelectedIndex = 0

        SqlStr = "SELECT DISTINCT DESP_LOCATION FROM FIN_INVOICE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR='" & RsCompany.Fields("FYEAR").Value & "'" & vbCrLf & " ORDER BY DESP_LOCATION"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        cboLocation.Items.Clear()
        cboLocation.Items.Add("All")

        Do While RS.EOF = False
            cboLocation.Items.Add(IIf(IsDbNull(RS.Fields("DESP_LOCATION").Value), "", RS.Fields("DESP_LOCATION").Value))
            RS.MoveNext()
        Loop

        cboLocation.SelectedIndex = 0


        cboDivision.Items.Clear()

        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        cboDivision.Items.Add("ALL")

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboDivision.SelectedIndex = 0

        Exit Sub
FillErr2:
        MsgBox(Err.Description)
    End Sub
    Private Sub lstInvoiceType_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles lstInvoiceType.ItemCheck
        Try
            If mClickProcess = True Then Exit Sub
            mClickProcess = True

            If e.Index = 0 Then
                If e.NewValue = System.Windows.Forms.CheckState.Checked Then     ''lstInvoiceType.GetItemChecked(0) = True Then
                    For I = 1 To lstInvoiceType.Items.Count - 1
                        lstInvoiceType.SetItemChecked(I, True)
                    Next
                Else
                    For I = 1 To lstInvoiceType.Items.Count - 1
                        lstInvoiceType.SetItemChecked(I, False)
                    Next
                End If
            Else
                If e.NewValue = System.Windows.Forms.CheckState.Unchecked Then      ''lstInvoiceType.GetItemChecked(e.Index - 1) = False Then
                    lstInvoiceType.SetItemChecked(0, False)
                End If
            End If
            mClickProcess = False
        Catch ex As Exception

        End Try
    End Sub
End Class
