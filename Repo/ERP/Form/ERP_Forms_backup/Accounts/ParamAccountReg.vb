Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinDataSource
'Imports Infragistics.Win.UltraWinTabControl
Imports System.Data.OleDb
Friend Class frmParamAccountReg
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20

    Private FormSize As New Resizeclass

    Private Const ColCode As Short = 1
    Private Const ColName As Short = 2
    Private Const ColCategory As Short = 3
    Private Const ColAddress As Short = 4
    Private Const ColCity As Short = 5
    Private Const ColState As Short = 6

    Private Const ColGSTNo As Short = 7
    Private Const ColGSTRegd As Short = 8
    Private Const ColGSTClass As Short = 9

    Private Const ColLSTNo As Short = 10
    Private Const ColTINNo As Short = 11
    Private Const ColPhone As Short = 12
    Private Const ColContactName As Short = 13
    Private Const ColPANNo As Short = 14
    Private Const ColPaidDay As Short = 15
    Private Const ColBalancing As Short = 16
    Private Const ColDebitName As Short = 17
    Private Const ColCreditName As Short = 18

    Private Const ColTypeofSupplier As Short = 19
    Private Const ColGUID As Short = 20
    Private Const ColNature As Short = 21
    Private Const ColLocDistance As Short = 22
    Private Const ColCustomerGroup As Short = 23
    Private Const ColEnterType As Short = 24
    Private Const ColUdyogAahaarNo As Short = 25


    Private Const ColPaymentCode As Short = 26
    Private Const ColPaymentDesc As Short = 27
    Private Const ColFromDays As Short = 28
    Private Const ColTodays As Short = 29
    Private Const ColAdhocPayments As Short = 30

    Private Const ColSecurityDeposit As Short = 31
    Private Const ColSecurityAmt As Short = 32
    Private Const ColSecurityChqNo As Short = 33
    Private Const ColBankAccountNo As Short = 34
    Private Const ColBankName As Short = 35
    Private Const ColBankBranchName As Short = 36
    Private Const ColIFSCCode As Short = 37
    Private Const ColSalesRep As Short = 38

    Private Const ColCreditLimited As Short = 39
    Private Const ColeMail As Short = 40
    Private Const ColAddUser As Short = 41
    Private Const ColAddDate As Short = 42
    Private Const ColModUser As Short = 43
    Private Const ColModDate As Short = 44


    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        cmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub cboShow_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShow.TextChanged, cboTransaction.TextChanged
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

    Private Sub chkDate_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkDate.CheckStateChanged
        Call PrintStatus(False)
        If chkDate.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtDateFrom.Enabled = False
            txtDateTo.Enabled = False
        Else
            txtDateFrom.Enabled = True
            txtDateTo.Enabled = True
        End If
    End Sub

    Private Sub ChkPaidDay_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkPaidDay.CheckStateChanged
        Call PrintStatus(False)
        If ChkPaidDay.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtPaidDays.Enabled = False
        Else
            txtPaidDays.Enabled = True
        End If
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForShow(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForShow(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportForShow(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim All As Boolean
        Dim SqlStr As String = ""
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""
        Dim PrintStatus As Boolean
        Dim mReportFileName As String


        'Select Record for print...

        SqlStr = ""

        SqlStr = MakeSQL("S")

        mTitle = "Account Register"
        If cboCategory.SelectedIndex <> 0 Then
            mSubTitle = "List of " & cboCategory.Text
        End If

        If cboHeadType.SelectedIndex <> 0 Then
            mSubTitle = "List of " & cboHeadType.Text
        End If

        mReportFileName = "AcctReg.Rpt"

        Call ShowReport(SqlStr, mReportFileName, Mode, mTitle, mSubTitle)

        Exit Sub
ERR1:
        If Err.Number = 32755 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        Else
            MsgInformation(Err.Description)
        End If
        '    Resume
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = PubReportFolderPath & mRPTName
        Report1.Action = 1
    End Sub

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchAccounts()
    End Sub
    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        If FieldsVerification() = False Then Exit Sub
        'MainClass.ClearGrid(SprdMain, RowHeight)
        If Show1("S") = False Then GoTo ErrPart
        Call PrintStatus(True)
        'FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamAccountReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Me.Text = "Account Register"
        If Show1("L") = False Then GoTo ERR1     ''CreateGridHeader("L")
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamAccountReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mNature As String


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355
        Call SetMainFormCordinate(Me)

        'Me.Top = 0
        'Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        ''Me.Width = VB6.TwipsToPixelsX(11355)
        cboCategory.Items.Clear()
        cboCategory.Items.Add("All")
        cboCategory.Items.Add("Customer")
        cboCategory.Items.Add("Supplier")
        cboCategory.Items.Add("Employee")
        cboCategory.Items.Add("1- Cash")
        cboCategory.Items.Add("2- Bank")
        cboCategory.Items.Add("Other")
        cboCategory.Items.Add("Fixed Assets")
        cboCategory.SelectedIndex = 0

        cboHeadType.Items.Clear()
        cboHeadType.Items.Add("All")
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
        cboHeadType.SelectedIndex = 0

        cboShow.Items.Clear()
        cboShow.Items.Add("All")
        cboShow.Items.Add("Authorised")
        cboShow.Items.Add("Unauthorised")
        cboShow.SelectedIndex = 0


        cboTransaction.Items.Clear()
        cboTransaction.Items.Add("All")
        cboTransaction.Items.Add("With Transaction")
        cboTransaction.Items.Add("Without Transaction")
        cboTransaction.SelectedIndex = 0


        cboEnterpriseType.Items.Clear()
        cboEnterpriseType.Items.Add("ALL")
        cboEnterpriseType.Items.Add("MICRO")
        cboEnterpriseType.Items.Add("SMALL")
        cboEnterpriseType.Items.Add("MEDIUM")
        cboEnterpriseType.SelectedIndex = 0

        cboNature.Items.Clear()
        cboNature.Items.Add("ALL")

        SqlStr = "SELECT DISTINCT SUPP_CUST_NATURE FROM FIN_SUPP_CUST_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ORDER BY SUPP_CUST_NATURE"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mNature = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NATURE").Value), "", RsTemp.Fields("SUPP_CUST_NATURE").Value)
                If mNature <> "" Then
                    cboNature.Items.Add(mNature)
                End If
                RsTemp.MoveNext()
            Loop
        End If

        '    cboNature.AddItem "BOP"
        '    cboNature.AddItem "CAPEX"
        '    cboNature.AddItem "CONSUMABLE "
        '    cboNature.AddItem "DIESEL"
        '    cboNature.AddItem "FINISH GOODS"
        '    cboNature.AddItem "GAS "
        '    cboNature.AddItem "IT"
        '    cboNature.AddItem "JOB WORK"
        '    cboNature.AddItem "M/C SPARES "
        '    cboNature.AddItem "NATURE "
        '    cboNature.AddItem "NICKLE"
        '    cboNature.AddItem "OTHERS"
        '    cboNature.AddItem "PACKING"
        '    cboNature.AddItem "PAINTS & CHEM"
        '    cboNature.AddItem "RAW MATERIAL"
        '    cboNature.AddItem "RAW MATERIAL TOOL"
        '    cboNature.AddItem "SCRAP DEALER"
        '    cboNature.AddItem "SERVICE"
        '    cboNature.AddItem "TRANSPORT"
        cboNature.SelectedIndex = 0

        cboSMERegd.Items.Clear()
        cboSMERegd.Items.Add("ALL")
        cboSMERegd.Items.Add("YES")
        cboSMERegd.Items.Add("NO")
        cboSMERegd.SelectedIndex = 0

        cboSMEStatus.Items.Clear()
        cboSMEStatus.Items.Add("ALL")
        cboSMEStatus.Items.Add("YES")
        cboSMEStatus.Items.Add("NO")
        cboSMEStatus.SelectedIndex = 0

        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        TxtAccount.Enabled = False
        cmdsearch.Enabled = False

        ChkPaidDay.CheckState = System.Windows.Forms.CheckState.Checked
        txtPaidDays.Enabled = False

        chkDate.CheckState = System.Windows.Forms.CheckState.Checked
        txtDateFrom.Enabled = False
        txtDateTo.Enabled = False
        txtDateFrom.Text = VB6.Format(RsCompany.Fields("Start_Date").Value, "dd/MM/yyyy")
        txtDateTo.Text = VB6.Format(RsCompany.Fields("END_DATE").Value, "dd/MM/yyyy")

        Call AutoCompleteSearch("FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "", TxtAccount)
        Call PrintStatus(True)

        FormSize.Init(Me)
        Call frmParamAccountReg_Activated(eventSender, eventArgs)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamAccountReg_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        UltraGrid1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame4.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11379.7, 749)
        CurrFormWidth = mReFormWidth

        'MainClass.SetSpreadColor(SprdMain, -1)

        '    FormSize.formResize Me

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamAccountReg_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        FormActive = False
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub


    Private Sub UltraGrid1_DoubleClick(sender As Object, e As EventArgs) Handles UltraGrid1.DoubleClick
        Dim xCode As String
        Dim mSuppType As String
        Dim mRow As UltraGridRow
        Dim mCol As UltraGridColumn

        If Me.UltraGrid1.ActiveRow.Index < 0 Then Exit Sub
        mRow = Me.UltraGrid1.Rows(Me.UltraGrid1.ActiveRow.Index)
        mCol = Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColCode)

        xCode = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColCode - 1))
        mSuppType = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColCategory - 1))

        If Trim(xCode) <> "" Then
            frmAcm.MdiParent = Me.MdiParent

            frmAcm.lblMasterType.Text = IIf(mSuppType = "Supplier", "S", IIf(mSuppType = "Customer", "C", "Accounts"))
            frmAcm.Show()
            frmAcm.frmAcm_Activated(Nothing, New System.EventArgs())

            frmAcm.txtCode.Text = xCode
            frmAcm.txtCode_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))

            'mm.Text = pFormCaption
            'FormMain.TabControl1.TabPages.Add(frmAcm)
        End If
    End Sub

    Private Sub cmdExport_Click(sender As Object, e As EventArgs) Handles cmdExport.Click

        Dim lngLoop As Integer  'loop variable

        Dim objMode As Object 'to store the mode of the row
        Dim objChk As Object 'to get the check status of the first column
        Dim strSplit() As String 'split variable
        Dim intAns As Integer ' to store the result from msgbox
        Dim lngRow As Long
        Try


            'If m_blnChangeInData = True Then
            '    MessageFromResFile(7284, MessageType.Information)
            '    GridSetFocus(UltraGrid1.ActiveRow.Tag.ToString, UltraGrid1.ActiveCell.Column.Index)
            '    Exit Sub
            'End If

            ''Please provide a location where you whould like to export the data to
            'MessageFromResFile(7304, MessageType.Information, GetLabelDes("7305"))

            Try
                SaveFileDialog1.FileName = Me.Text
            Catch
                SaveFileDialog1.FileName = "File1"
            End Try

            Dim strAction As String = ""
            Try
                strAction = SaveFileDialog1.ShowDialog()
            Catch
                SaveFileDialog1.FileName = "File1"
                strAction = SaveFileDialog1.ShowDialog()
            End Try

            If strAction = "1" Then
                ExportToExcel(SaveFileDialog1.FileName)
            End If
            'Me.Cursor = Cursors.Default

        Catch ex As Exception
            ErrorMsg(Err.Description, Err.Number)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub ExportToExcel(ByVal strFileName As String)
        '----------------------------------------------------------------------------
        'Argument       :   Nil
        'Return Value   :   Nil
        'Function       :   To export the report to excel/csv/text file
        'Comments       :   This function will be called from clickinvoked and enterpressed events
        '                   THIS FUNCTION HAS TO BE OVERRIDED IN THE DERIVED FORM   
        '----------------------------------------------------------------------------
        Me.Cursor = Cursors.WaitCursor
        Dim start As DateTime
        'Dim timespan As TimeSpan
        start = DateTime.Now
        Try
            Me.UltraGridExcelExporter1.FileLimitBehaviour = ExcelExport.FileLimitBehaviour.TruncateData
            Me.UltraGridExcelExporter1.ExportAsync(Me.UltraGrid1, strFileName & ".xls")
            ' timespan = DateTime.Now.Subtract(start)
            'Exported To File : 
            '  MessageFromResFile(7292, MessageType.Information, strFileName)
        Catch
            'Specified Path Does Not Exist,Invalid File Name
            ErrorMsg(Err.Description, Err.Number)
        End Try
        Me.Cursor = Cursors.Default
    End Sub
    Private Sub txtAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub TxtAccount_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.DoubleClick
        SearchAccounts()
    End Sub
    Private Sub SearchAccounts()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"
        ''MainClass.SearchMaster TxtAccount, "FIN_SUPP_CUST_MST", "NAME", SqlStr
        MainClass.SearchGridMaster(TxtAccount.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr)
        If AcName <> "" Then
            TxtAccount.Text = AcName
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

        If TxtAccount.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            TxtAccount.Text = UCase(Trim(TxtAccount.Text))
        Else
            MsgInformation("No Such Account in Account Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Function Show1(pShowType As String) As Boolean

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

        SqlStr = MakeSQL(pShowType)
        'MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")
        FillUltraGrid(SqlStr)
        '********************************
        Show1 = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Exit Function
LedgError:
        Show1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub FillUltraGrid(pMakeSql As String)
        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim sql As String
        Dim i As Integer
        Dim inti As Integer

        UltraDataSource2.Rows.Clear()
        Me.UltraGrid1.DataSource = Nothing
        oledbCnn = New OleDbConnection(StrConn)
        Try

            ClearGroupFromUltraGrid(UltraGrid1)
            ClearFilterFromUltraGrid(UltraGrid1)
            oledbCnn.Open()
            oledbAdapter = New OleDbDataAdapter(pMakeSql, oledbCnn)

            oledbAdapter.Fill(ds)

            ' Set the data source and data member to bind the grid.
            Me.UltraGrid1.DataSource = ds
            Me.UltraGrid1.DataMember = ""

            CreateGridHeader()


            oledbAdapter.Dispose()
            oledbCnn.Close()
        Catch ex As Exception
            MsgBox("Can not open connection ! ")
        End Try
    End Sub
    Private Sub CreateGridHeader()
        '----------------------------------------------------------------------------
        'Argument       :   Nil
        'Return Value   :   Nil
        'Function       :   to create the grid header
        'Comments       :   Nil
        '----------------------------------------------------------------------------
        Try
            Dim inti As Integer
            'create column header

            UltraGrid1.DisplayLayout.Bands(0).Columns(0).RowLayoutColumnInfo.PreferredLabelSize = New System.Drawing.Size(0, 40)
            UltraGrid1.DisplayLayout.Override.WrapHeaderText = Infragistics.Win.DefaultableBoolean.True


            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCode - 1).Header.Caption = "Code"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColName - 1).Header.Caption = "Name"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCategory - 1).Header.Caption = "Category"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAddress - 1).Header.Caption = "Address"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCity - 1).Header.Caption = "City"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColState - 1).Header.Caption = "State"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColGSTNo - 1).Header.Caption = "GST No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColGSTRegd - 1).Header.Caption = "GST Regd"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColGSTClass - 1).Header.Caption = "GST Class"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColLSTNo - 1).Header.Caption = "LST No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColTINNo - 1).Header.Caption = "TIN No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPhone - 1).Header.Caption = "Phone No"


            UltraGrid1.DisplayLayout.Bands(0).Columns(ColContactName - 1).Header.Caption = "Contact Name"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPANNo - 1).Header.Caption = "PAN No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPaidDay - 1).Header.Caption = "Paid Days"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBalancing - 1).Header.Caption = "Balancing method"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColDebitName - 1).Header.Caption = "Debit Group Name"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCreditName - 1).Header.Caption = "Credit Group Name"


            UltraGrid1.DisplayLayout.Bands(0).Columns(ColTypeofSupplier - 1).Header.Caption = "Type of Supplier"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColGUID - 1).Header.Caption = "GUID"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColNature - 1).Header.Caption = "Nature"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocDistance - 1).Header.Caption = "LOC Distance"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCustomerGroup - 1).Header.Caption = "Customer Group"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColEnterType - 1).Header.Caption = "Enterpise Type"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColUdyogAahaarNo - 1).Header.Caption = "Udyog Aahaar No"


            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPaymentCode - 1).Header.Caption = "Payment Code"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPaymentDesc - 1).Header.Caption = "Payment Desc"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColFromDays - 1).Header.Caption = "From Days"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColTodays - 1).Header.Caption = "TO Days"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAdhocPayments - 1).Header.Caption = "Adhoc Payments"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSecurityDeposit - 1).Header.Caption = "Security Deposit"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSecurityAmt - 1).Header.Caption = "Security Amount"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSecurityChqNo - 1).Header.Caption = "Security Chq no"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBankAccountNo - 1).Header.Caption = "Bank Account No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBankName - 1).Header.Caption = "Bank Name"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBankBranchName - 1).Header.Caption = "Bank Branch Name"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColIFSCCode - 1).Header.Caption = "IFSC Code"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSalesRep - 1).Header.Caption = "Sales Person"
            '
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCreditLimited - 1).Header.Caption = "Credit Limited"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColeMail - 1).Header.Caption = "eMail Id"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAddUser - 1).Header.Caption = "Add User"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAddDate - 1).Header.Caption = "Add Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColModUser - 1).Header.Caption = "Modify User"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColModDate - 1).Header.Caption = "Modify Date"


            'UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Header.Appearance.TextHAlign = HAlign.Right

            ''enable/disable the columns
            For inti = 0 To UltraGrid1.DisplayLayout.Bands(0).Columns.Count - 1
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).CellActivation = Activation.NoEdit  ''  .AllowEdit
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).Header.Appearance.TextHAlign = HAlign.Center

                ''UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.SubDepartmentName).Style = UltraWinGrid.ColumnStyle.DropDown
                '' UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.Qty).Style = UltraWinGrid.ColumnStyle.DoubleNonNegative
                ''UltraGrid1.DisplayLayout.Bands(0).Columns(m_udtColumns.SubDepartmentName).EditorComponent = cmbDepartment
            Next

            'UltraGrid1.DisplayLayout.Bands(0).Columns(ColQuantity - 1).Style = UltraWinGrid.ColumnStyle.Double


            'UltraGrid1.DisplayLayout.Bands(0).Columns(ColQuantity - 1).CellAppearance.TextHAlign = HAlign.Right


            '''for hidden
            'UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Hidden = True


            ' to define width of the columns
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCode - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColName - 1).Width = 250
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCategory - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAddress - 1).Width = 250
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCity - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColState - 1).Width = 100



            UltraGrid1.DisplayLayout.Bands(0).Columns(ColGSTNo - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColGSTRegd - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColGSTClass - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColLSTNo - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColTINNo - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPhone - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColContactName - 1).Width = 120
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPANNo - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPaidDay - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBalancing - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColDebitName - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCreditName - 1).Width = 80


            UltraGrid1.DisplayLayout.Bands(0).Columns(ColTypeofSupplier - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColGUID - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColNature - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocDistance - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCustomerGroup - 1).Width = 90

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColEnterType - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColUdyogAahaarNo - 1).Width = 80


            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPaymentCode - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPaymentDesc - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColFromDays - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColTodays - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAdhocPayments - 1).Width = 90

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSecurityDeposit - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSecurityAmt - 1).Width = 120

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSecurityChqNo - 1).Width = 120
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBankAccountNo - 1).Width = 120
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBankName - 1).Width = 120
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColBankBranchName - 1).Width = 120
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColIFSCCode - 1).Width = 120
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSalesRep - 1).Width = 120


            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCreditLimited - 1).Width = 120
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColeMail - 1).Width = 120

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAddUser - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColAddDate - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColModUser - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColModDate - 1).Width = 80

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColGUID - 1).Hidden = True
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColNature - 1).Hidden = True

            MainClass.SetInfragisticsGrid(UltraGrid1, -1, "Filter Row", "Group Row")

            'fill labels 
            'FillLabelsFromResFile(Me)
            'Catch sqlex As SqlException
            '    ErrorTrap(sqlex.Message, "frmRMReturn.vb", "CreateHeader", "", "", "Sql Exception")
            '    Me.Cursor = Windows.Forms.Cursors.Default
        Catch ex As Exception
            ErrorMsg(ex.Message, "")
            Me.Cursor = Windows.Forms.Cursors.Default
        End Try
    End Sub
    Private Function MakeSQL(pShowType As String) As String
        On Error GoTo ERR1
        Dim mAccountCode As String


        ''SELECT CLAUSE...
        MakeSQL = " Select ACM.SUPP_CUST_CODE, ACM.SUPP_CUST_NAME, " & vbCrLf _
           & " Case When ACM.SUPP_CUST_TYPE='C' THEN 'Customer' " & vbCrLf _
           & " WHEN ACM.SUPP_CUST_TYPE='S' THEN 'Supplier' " & vbCrLf _
           & " WHEN ACM.SUPP_CUST_TYPE='E' THEN 'Employee' " & vbCrLf _
           & " WHEN ACM.SUPP_CUST_TYPE='1' THEN 'Cash' " & vbCrLf _
           & " WHEN ACM.SUPP_CUST_TYPE='2' THEN 'Bank' " & vbCrLf _
           & " WHEN ACM.SUPP_CUST_TYPE='O' THEN 'Other' " & vbCrLf _
           & " WHEN ACM.SUPP_CUST_TYPE='F' THEN 'Fixed Assets' END,  " & vbCrLf _
           & " BACM.SUPP_CUST_ADDR, BACM.SUPP_CUST_CITY, " & vbCrLf _
           & " BACM.SUPP_CUST_STATE || ' -' || BACM.SUPP_CUST_PIN, " & vbCrLf _
           & " BACM.GST_RGN_NO, ACM.GST_REGD, GST_CLASSIFICATION, " & vbCrLf _
           & " ACM.LST_NO || decode(ACM.LST_NO,NULL,'',' ,') || ACM.CST_NO, ACM.ACCOUNT_CODE AS TIN_NO, " & vbCrLf _
           & " ACM.SUPP_CUST_PHONE || ' ,' || ACM.SUPP_CUST_MOBILE || ' ,' || ACM.SUPP_CUST_FAXNO, " & vbCrLf _
           & " ACM.CONTACT_TELNO, " & vbCrLf _
           & " ACM.PAN_NO, TO_CHAR(ACM.ACTIVITY) , " & vbCrLf _
           & " DECODE(ACM.BALANCINGMETHOD,'D','Detail','Summary'), " & vbCrLf _
           & " A.GROUP_NAME, B.GROUP_NAME, " & vbCrLf _
           & " TYPE_OF_SUPPLIER, GROUP_UID, SUPP_CUST_NATURE, 0 AS LOC_DISTANCE, CUSTOMER_GROUP," & vbCrLf _
           & " ENTERPRISE_TYPE, UDYOGAAHAARNO, " & vbCrLf _
           & " PAY_TERM_CODE, PAY_TERM_DESC, FROM_DAYS, TO_DAYS, ADHOC_PAY_TERMS, " & vbCrLf _
           & " DECODE(IS_SECURITY_DEPOSIT,'N','NO','YES') AS IS_SECURITY_DEPOSIT, SECURITY_AMOUNT," & vbCrLf _
           & " SECURITY_CHEQUE_NO, CUST_BANK_ACCT_NO, CUST_BANK_BANK, BANK_BRANCH_NAME, BANK_IFSC_CODE,  ACM.RESPONSIBLE_PERSON," & vbCrLf _
           & " CREDIT_LIMIT,BACM.SUPP_CUST_MAILID,ACM.ADDUSER, ACM.ADDDATE,ACM.MODUSER, ACM.MODDATE "



        ''FROM CLAUSE...
        MakeSQL = MakeSQL & vbCrLf _
            & " FROM FIN_SUPP_CUST_MST ACM, FIN_SUPP_CUST_BUSINESS_MST BACM, FIN_GROUP_MST A, FIN_GROUP_MST B, FIN_PAYTERM_MST PMST"
        ''WHERE CLAUSE...

        MakeSQL = MakeSQL & vbCrLf & " WHERE " & vbCrLf _
           & " ACM.COMPANY_CODE=BACM.COMPANY_CODE(+) " & vbCrLf _
           & " And ACM.SUPP_CUST_CODE=BACM.SUPP_CUST_CODE(+) " & vbCrLf _
           & " AND ACM.COMPANY_CODE=A.COMPANY_CODE(+) " & vbCrLf _
           & " And ACM.GROUPCODE=A.GROUP_CODE(+) " & vbCrLf _
           & " And ACM.COMPANY_CODE=B.COMPANY_CODE(+) " & vbCrLf _
           & " And ACM.GROUPCODECR=B.GROUP_CODE(+) " & vbCrLf _
           & " And ACM.COMPANY_CODE=PMST.COMPANY_CODE(+) " & vbCrLf _
           & " And ACM.PAYMENT_CODE=PMST.PAY_TERM_CODE(+) " & vbCrLf _
           & " And ACM.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""


        If ChkPaidDay.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQL = MakeSQL & vbCrLf & " And ACM.PAIDDAY='" & Val(txtPaidDays.Text) & "'"
        End If

        If cboCategory.SelectedIndex <> 0 Then
            MakeSQL = MakeSQL & vbCrLf & " AND ACM.SUPP_CUST_TYPE='" & VB.Left(cboCategory.Text, 1) & "'"
        End If

        If cboHeadType.SelectedIndex <> 0 Then
            MakeSQL = MakeSQL & vbCrLf & " AND ACM.HEADTYPE='" & VB.Left(cboHeadType.Text, 1) & "'"
        End If

        If Trim(txtGSTNo.Text) <> "" Then
            MakeSQL = MakeSQL & vbCrLf & " AND ACM.GST_RGN_NO LIKE '%" & Trim(txtGSTNo.Text) & "%'"
        End If

        If cboShow.SelectedIndex = 1 Then
            MakeSQL = MakeSQL & vbCrLf & " AND ACM.AUTHORISED='Y'"
        ElseIf cboShow.SelectedIndex = 2 Then
            MakeSQL = MakeSQL & vbCrLf & " AND ACM.AUTHORISED='N'"
        End If

        '       Select Case distinct Supp_cust_type,Supp_cust_code, supp_cust_name, SUPP_CUST_ADDR,SUPP_CUST_CITY,GST_RGN_NO from fin_supp_cust_mst
        'where supp_cust_code Not in (
        'Select Case accountcode from fin_posted_trn
        'union all
        ' Select Case supp_cust_code from dsp_saleorder_hdr
        '  union all
        ' Select Case supp_cust_code from pur_purchase_hdr
        '    union all
        ' Select Case supp_cust_code from inv_gateentry_hdr
        ')

        If CheckConsolidatedMaster("FIN_SUPP_CUST_MST") = True Then
            If cboTransaction.SelectedIndex = 1 Then
                MakeSQL = MakeSQL & vbCrLf & " AND ACM.SUPP_CUST_CODE IN ( " & vbCrLf _
                    & " SELECT DISTINCT ACCOUNTCODE FROM FIN_POSTED_TRN " & vbCrLf _
                    & " UNION ALL" & vbCrLf _
                    & " SELECT DISTINCT SUPP_CUST_CODE ACCOUNTCODE FROM PUR_PURCHASE_HDR " & vbCrLf _
                    & " )"
            ElseIf cboTransaction.SelectedIndex = 2 Then
                MakeSQL = MakeSQL & vbCrLf & " AND ACM.SUPP_CUST_CODE NOT IN ( " & vbCrLf _
                    & " SELECT DISTINCT ACCOUNTCODE FROM FIN_POSTED_TRN" & vbCrLf _
                    & " UNION ALL" & vbCrLf _
                    & " SELECT DISTINCT SUPP_CUST_CODE ACCOUNTCODE FROM PUR_PURCHASE_HDR" & vbCrLf _
                    & " )"
            End If
        Else
            If cboTransaction.SelectedIndex = 1 Then
                MakeSQL = MakeSQL & vbCrLf & " AND ACM.SUPP_CUST_CODE IN ( " & vbCrLf _
                    & " SELECT DISTINCT ACCOUNTCODE FROM FIN_POSTED_TRN WHERE COMPANY_CODE= ACM.COMPANY_CODE" & vbCrLf _
                    & " UNION ALL" & vbCrLf _
                    & " SELECT DISTINCT SUPP_CUST_CODE ACCOUNTCODE FROM PUR_PURCHASE_HDR WHERE COMPANY_CODE= ACM.COMPANY_CODE" & vbCrLf _
                    & " )"
            ElseIf cboTransaction.SelectedIndex = 2 Then
                MakeSQL = MakeSQL & vbCrLf & " AND ACM.SUPP_CUST_CODE NOT IN ( " & vbCrLf _
                    & " SELECT DISTINCT ACCOUNTCODE FROM FIN_POSTED_TRN WHERE COMPANY_CODE= ACM.COMPANY_CODE" & vbCrLf _
                    & " UNION ALL" & vbCrLf _
                    & " SELECT DISTINCT SUPP_CUST_CODE ACCOUNTCODE FROM PUR_PURCHASE_HDR WHERE COMPANY_CODE= ACM.COMPANY_CODE" & vbCrLf _
                    & " )"
            End If
        End If


        If cboEnterpriseType.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & " And ACM.ENTERPRISE_TYPE='" & Trim(cboEnterpriseType.Text) & "'"
        End If

        If cboNature.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & " AND ACM.SUPP_CUST_NATURE='" & Trim(cboNature.Text) & "'"
        End If

        If cboSMERegd.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & " AND ACM.SME_REGD='" & VB.Left(cboSMERegd.Text, 1) & "'"
        End If

        If cboSMEStatus.SelectedIndex > 0 Then
            MakeSQL = MakeSQL & vbCrLf & " AND ACM.SME_STATUS='" & VB.Left(cboSMEStatus.Text, 1) & "'"
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, "", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
                MakeSQL = MakeSQL & vbCrLf & " AND ACM.SUPP_CUST_CODE='" & mAccountCode & "'"
            End If
        End If

        If pShowType = "L" Then
            MakeSQL = MakeSQL & vbCrLf & " AND 1=2"
        End If
        If chkDate.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            MakeSQL = MakeSQL & vbCrLf & " AND ACM.ADDDATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY')"
            MakeSQL = MakeSQL & vbCrLf & " AND ACM.ADDDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY')"
        End If

        ''ORDER CLAUSE...

        If optShow(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY ACM.SUPP_CUST_NAME"
        Else
            MakeSQL = MakeSQL & vbCrLf & " ORDER BY ACM.SUPP_CUST_CODE"
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function FieldsVerification() As Boolean

        On Error GoTo ERR1

        If chkDate.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
            If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(TxtAccount.Text) = "" Then
                MsgInformation("Invaild Account Name")
                TxtAccount.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invaild Account Name")
                TxtAccount.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If
        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function

    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtGSTNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGSTNo.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtGSTNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtGSTNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtGSTNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPaidDays_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPaidDays.TextChanged
        Call PrintStatus(False)
    End Sub


    Private Sub txtPaidDays_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPaidDays.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
End Class
