Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Imports VB = Microsoft.VisualBasic
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinDataSource
Imports System.Data.OleDb
'Imports Infragistics.Win.UltraWinTabControl
Friend Class frmParamITAssetsRegister
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20

    Private Const ColLocked As Short = 1
    Private Const ColMachineNo As Short = 2
    Private Const ColMachineCode As Short = 3
    Private Const ColDeptCode As Short = 4
    Private Const ColMachineDesc As Short = 5
    Private Const colMachineSpec As Short = 6
    Private Const colMachineType As Short = 7
    Private Const ColLocation As Short = 8
    Private Const ColMake As Short = 9
    Private Const ColMachineInstDate As Short = 10
    Private Const ColMachineUB As Short = 11
    Private Const ColMachineStatus As Short = 12
    Private Const ColRemarks As Short = 13
    Private Const ColDivCode As Short = 14
    Private Const ColIPAddress As Short = 15
    Private Const ColSerialNo As Short = 16
    Private Const ColConfig As Short = 17
    Private Const ColSoftware As Short = 18


    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged
        Call PrintStatus(False)
    End Sub


    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportONMRR(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportONMRR(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportONMRR(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String = ""
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""


        Report1.Reset()

        mTitle = "IT Assets Register"

        mSubTitle = ""

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\ITAssetReg.rpt"

        SqlStr = MakeSQL("S")
        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub
    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        If FieldsVerification() = False Then Exit Sub
        'MainClass.ClearGrid(SprdMain, RowHeight)
        If Show1("S") = False Then GoTo ErrPart
        Call PrintStatus(True)

        ''MainClass.SetFocusToCell SprdMain, mActiveRow, 4
        'FormatSprdMain(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamITAssetsRegister_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Me.Text = "IT Assets Register"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamITAssetsRegister_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

        Dim RsTemp As ADODB.Recordset = Nothing
        'Dim oledbCnn As OleDbConnection
        'Dim oledbAdapter As OleDbDataAdapter
        'Dim ds As New DataSet

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        ''Me.Width = VB6.TwipsToPixelsX(11355)


        Call PrintStatus(True)
        'Call FillPOCombo

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

        'cboCompany.Enabled = True

        'oledbCnn = New OleDbConnection(StrConn)

        'SqlStr = "Select COMPANY_NAME, COMPANY_CODE " & vbCrLf _
        '    & " FROM GEN_COMPANY_MST"

        'SqlStr = SqlStr & vbCrLf & " WHERE COMPANY_CODE IN (SELECT COMPANY_CODE FROM GEN_COMPANYRIGHT_MST WHERE USER_ID='" & PubUserID & "' AND RIGHTS='Y') "

        'SqlStr = SqlStr & vbCrLf & " AND COMPANY_CODE IN (SELECT COMPANY_CODE FROM FIN_RIGHTS_MST WHERE USERID='" & PubUserID & "' AND MENUHEAD='" & myMenu & "') "


        'SqlStr = SqlStr & vbCrLf & "ORDER BY COMPANY_NAME "


        'oledbCnn.Open()
        'oledbAdapter = New OleDbDataAdapter(SqlStr, oledbCnn)

        'oledbAdapter.Fill(ds)

        '' Set the data source and data member to bind the grid.
        'cboCompany.DataSource = ds
        'cboCompany.DataMember = ""
        'Dim c As UltraGridColumn = Me.cboCompany.DisplayLayout.Bands(0).Columns.Add()
        'With c
        '    .Key = "Selected"
        '    .Header.Caption = String.Empty
        '    .Header.CheckBoxVisibility = HeaderCheckBoxVisibility.Always
        '    .DataType = GetType(Boolean)
        '    .DataType = GetType(Boolean)
        '    .Header.VisiblePosition = 0
        'End With
        'cboCompany.CheckedListSettings.CheckStateMember = "Selected"
        'cboCompany.CheckedListSettings.EditorValueSource = Infragistics.Win.EditorWithComboValueSource.CheckedItems
        '' Set up the control to use a custom list delimiter 
        'cboCompany.CheckedListSettings.ListSeparator = " , "
        '' Set ItemCheckArea to Item, so that clicking directly on an item also checks the item
        'cboCompany.CheckedListSettings.ItemCheckArea = Infragistics.Win.ItemCheckArea.Item
        'cboCompany.DisplayMember = "COMPANY_NAME"
        'cboCompany.ValueMember = "COMPANY_CODE"

        'cboCompany.DisplayLayout.Bands(0).Columns(0).Header.Caption = "Company Name"
        'cboCompany.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Company Code"


        'cboCompany.DisplayLayout.Bands(0).Columns(0).Width = 350
        'cboCompany.DisplayLayout.Bands(0).Columns(1).Width = 100


        'cboCompany.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown ''List       '' Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDown



        'oledbAdapter.Dispose()
        'oledbCnn.Close()

        If Show1("L") = False Then GoTo BSLError     ''CreateGridHeader("L")

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamITAssetsRegister_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        UltraGrid1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame4.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11379.7, 749)
        CurrFormWidth = mReFormWidth

        'MainClass.SetSpreadColor(SprdMain, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamITAssetsRegister_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        FormActive = False
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub


    Private Function Show1(pType As String) As Boolean

        On Error GoTo LedgError
        Dim SqlStr As String = ""


        Show1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQL(pType)
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

    Private Function MakeSQL(mType As String) As String

        On Error GoTo ERR1
        Dim mDept As String
        Dim mSupplier As String
        Dim mEmployee As String
        Dim mCatCode As String = ""
        Dim mSubCatCode As String
        Dim mDivision As Double

        ''SELECT CLAUSE...




        MakeSQL = " SELECT ''," & vbCrLf _
            & " MACHINE_NO, MACHINE_ITEM_CODE, DEPT_CODE," & vbCrLf _
            & " MACHINE_DESC, MACHINE_SPEC, MACHINE_TYPE, " & vbCrLf _
            & " LOCATION, MAKE, MACHINE_INST_DATE," & vbCrLf _
            & " DECODE(MACHINE_UB,'Y','YES','NO') AS MACHINE_UB, DECODE(STATUS,'O','OPEN','CLOSED') AS STATUS, REMARKS," & vbCrLf _
            & " DIV_CODE, IP_ADDRESS, SERIAL_NO," & vbCrLf _
            & " '' AS MAC_CONFIG," & vbCrLf _
            & " '' As MAC_SOFTWARE "

        ''FROM CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " FROM IT_MACHINE_MST"

        ''WHERE CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        'Dim mCompanyCode As String

        'If cboCompany.Text.Trim <> "" Then
        '    For Each r As UltraGridRow In cboCompany.CheckedRows
        '        If mCompanyCode <> "" Then
        '            mCompanyCode += "," & "" & r.Cells("COMPANY_CODE").Value.ToString() & ""
        '        Else
        '            mCompanyCode += "" & r.Cells("COMPANY_CODE").Value.ToString() & ""
        '        End If
        '    Next
        'End If

        'If mCompanyCode = "" Then
        '    MakeSQL = MakeSQL & vbCrLf & " AND IGH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        'Else
        '    MakeSQL = MakeSQL & vbCrLf & " AND IGH.COMPANY_CODE IN (" & mCompanyCode & ")"
        'End If

        If cboDivision.Text <> "ALL" Then
            If MainClass.ValidateWithMasterTable((cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mDivision = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND DIV_CODE=" & mDivision & ""
            End If
        End If

        If mType = "L" Then
            MakeSQL = MakeSQL & vbCrLf & " AND 1=2"
        End If

        MakeSQL = MakeSQL & vbCrLf & "ORDER BY MACHINE_NO, MACHINE_ITEM_CODE, DEPT_CODE"

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        'If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then txtDateFrom.SetFocus
        'If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        '    If FYChk(CDate(txtDateTo.Text)) = False Then txtDateTo.SetFocus


        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function

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

            'UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Key = "Locked"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMachineNo - 1).Header.Caption = "Machine No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMachineCode - 1).Header.Caption = "Machine Code"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColDeptCode - 1).Header.Caption = "Dept Code"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMachineDesc - 1).Header.Caption = "Machine Desc"

            UltraGrid1.DisplayLayout.Bands(0).Columns(colMachineSpec - 1).Header.Caption = "Machine Specification"
            UltraGrid1.DisplayLayout.Bands(0).Columns(colMachineType - 1).Header.Caption = "Machine Type"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocation - 1).Header.Caption = "Location"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMake - 1).Header.Caption = "Make"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMachineInstDate - 1).Header.Caption = "MAchine Installation Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMachineUB - 1).Header.Caption = "Machine Under Break Down"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMachineStatus - 1).Header.Caption = "Machine Status"


            UltraGrid1.DisplayLayout.Bands(0).Columns(ColRemarks - 1).Header.Caption = "Remarks"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColDivCode - 1).Header.Caption = "Div Code"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColIPAddress - 1).Header.Caption = "IP Address"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSerialNo - 1).Header.Caption = "Serial no"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColConfig - 1).Header.Caption = "Configraution"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSoftware - 1).Header.Caption = "Softwares"

            'UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Header.Appearance.TextHAlign = HAlign.Right

            ''enable/disable the columns
            For inti = 0 To UltraGrid1.DisplayLayout.Bands(0).Columns.Count - 1
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).CellActivation = Activation.NoEdit  ''  .AllowEdit
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).Header.Appearance.TextHAlign = HAlign.Center
            Next

            'UltraGrid1.DisplayLayout.Bands(0).Columns(ColQty - 1).Style = UltraWinGrid.ColumnStyle.Double
            'UltraGrid1.DisplayLayout.Bands(0).Columns(ColQty - 1).CellAppearance.TextHAlign = HAlign.Right

            ''for hidden
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Hidden = True

            ' to define width of the columns
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocked - 1).Width = 120
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMachineNo - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMachineCode - 1).Width = 75
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColDeptCode - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMachineDesc - 1).Width = 150
            UltraGrid1.DisplayLayout.Bands(0).Columns(colMachineSpec - 1).Width = 250
            UltraGrid1.DisplayLayout.Bands(0).Columns(colMachineType - 1).Width = 250
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColLocation - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMake - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMachineInstDate - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMachineUB - 1).Width = 80
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMachineStatus - 1).Width = 60
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColRemarks - 1).Width = 90

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColDivCode - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColIPAddress - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSerialNo - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColConfig - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColSoftware - 1).Width = 90

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
    Private Sub UltraGrid1_DoubleClick(sender As Object, e As EventArgs) Handles UltraGrid1.DoubleClick

        Dim mRow As UltraGridRow
        Dim mCol As UltraGridColumn

        'Dim xGateNo As Double
        'Dim xGateDate As String



        'If Me.UltraGrid1.ActiveRow.Index < 0 Then Exit Sub
        'mRow = Me.UltraGrid1.Rows(Me.UltraGrid1.ActiveRow.Index)
        'mCol = Me.UltraGrid1.DisplayLayout.Bands(0).Columns(1)

        'xGateNo = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColGateNo - 1))
        'xGateDate = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColGateDate - 1))

        'If xGateDate = "" Then Exit Sub

        'If CDate(VB6.Format(xGateDate, "DD/MM/YYYY")) < CDate(VB6.Format(RsCompany.Fields("Start_Date").Value, "DD/MM/YYYY")) Then
        '    MsgInformation("Cann't open Last Year Voucher")
        '    Exit Sub
        'End If


        'FrmGateEntry.MdiParent = Me.MdiParent
        'FrmGateEntry.Show()

        'FrmGateEntry.FrmGateEntry_Activated(Nothing, New System.EventArgs())

        'FrmGateEntry.txtMRRNo.Text = CStr(xGateNo)
        'FrmGateEntry.TxtMRRNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False))

    End Sub

    Private Sub cmdExport_Click(sender As Object, e As EventArgs) Handles cmdExport.Click
        Dim lngLoop As Integer  'loop variable

        Dim objMode As Object 'to store the mode of the row
        Dim objChk As Object 'to get the check status of the first column
        Dim strSplit() As String 'split variable
        Dim intAns As Integer ' to store the result from msgbox
        Dim lngRow As Long
        Try

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

    Private Sub UltraGrid1_InitializeLayout(sender As Object, e As InitializeLayoutEventArgs) Handles UltraGrid1.InitializeLayout

        '''Allowing Summaries in the UltraGrid 
        'e.Layout.Override.AllowRowSummaries = AllowRowSummaries.True
        ''' Setting the Sum Summary for the desired column

        'e.Layout.Bands(0).Summaries.Add("ColQty", SummaryType.Sum, e.Layout.Bands(0).Columns(ColQty - 1))
        'e.Layout.Bands(0).Summaries.Add("ColAmount", SummaryType.Sum, e.Layout.Bands(0).Columns(ColAmount - 1))


        '''Set the display format to be just the number 
        'e.Layout.Bands(0).Summaries("ColQty").DisplayFormat = "{0:###0.00}"
        'e.Layout.Bands(0).Summaries("ColAmount").DisplayFormat = "{0:###0.00}"


        '''Hide the SummaryFooterCaption row 
        'e.Layout.Bands(0).Override.SummaryFooterCaptionVisible = Infragistics.Win.DefaultableBoolean.False

        ''band.SummaryFooterCaption = "Subtotal:"

        'e.Layout.Bands(0).Override.SummaryFooterCaptionAppearance.FontData.Bold = DefaultableBoolean.True

        'e.Layout.Bands(0).Override.SummaryFooterCaptionAppearance.BackColor = Color.LightSteelBlue

        'e.Layout.Bands(0).Override.SummaryFooterCaptionAppearance.ForeColor = Color.Black
        ''     / Here, I want to add grand total

        'e.Layout.Bands(0).Summaries("ColQty").Appearance.TextHAlign = HAlign.Right
        'e.Layout.Bands(0).Summaries("ColAmount").Appearance.TextHAlign = HAlign.Right

        ''Disable grid default highlight

        ''UltraGrid1.DisplayLayout.Override.ResetActiveRowAppearance()

        ''UltraGrid1.DisplayLayout.Override.ResetActiveCellAppearance()

        ''UltraGrid1.DisplayLayout.Override.ActiveAppearancesEnabled = DefaultableBoolean.False

        'e.Layout.ViewStyleBand = ViewStyleBand.OutlookGroupBy
    End Sub
End Class
