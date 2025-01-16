Option Strict Off
Option Explicit On
Imports AxFPSpreadADO
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmNRGPApproval
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection
    Dim mAccountCode As String
    Private Const RowHeight As Short = 15

    Private Const ColMKEY As Short = 1
    Private Const ColRGPSlipNo As Short = 2
    Private Const ColRGPSlipDate As Short = 3
    Private Const ColPartyName As Short = 4
    Private Const ColItemValue As Short = 5
    Private Const ColRGPType As Short = 6
    Private Const ColCompanyName As Short = 7
    Private Const ColPostStatus As Short = 8

    Dim mClickProcess As Boolean

    Dim mSearchKey As String
    Dim cntSearchRow As Long
    Dim cntSearchCol As Long

    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkALL.CheckStateChanged
        cmdShow.Enabled = True
        TxtAccount.Enabled = IIf(ChkALL.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click

        On Error GoTo ErrPart
        Dim CntRow As Integer
        Dim SqlStr As String = ""
        Dim mMKEY As Double
        Dim mWef As String
        Dim mSupplier As String
        Dim mUpdateCount As Integer
        Dim mPOAmendNo As Integer
        Dim mCanPostPO As Boolean
        Dim mPOSeq As Double
        Dim mAuthorisation As String
        Dim mCompanyName As String
        Dim mCompanyCode As Long


        mAuthorisation = IIf(InStr(1, XRIGHT, "S") > 0, "Y", "N")
        If mAuthorisation = "N" Then
            MsgBox("You have no Authorisation Right to Approve NRGP. ", MsgBoxStyle.Critical)
            Exit Sub
        End If

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        mUpdateCount = 0

        With SprdMain
            For CntRow = 1 To .MaxRows
                mCanPostPO = False
                .Row = CntRow

                .Col = ColMKEY
                mMKEY = CDbl(Trim(.Text))

                .Col = ColCompanyName
                mCompanyName = Trim(.Text)

                If MainClass.ValidateWithMasterTable(Trim(mCompanyName), "COMPANY_SHORTNAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                    mCompanyCode = MasterNo
                End If


                '.Col = ColRGPSlipNo

                '.Col = ColRGPSlipDate
                'mWef = Trim(.Text)

                '.Col = ColPartyName
                'mSupplier = Trim(.Text)

                .Col = ColPostStatus
                If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                    'If mWef = "" Then
                    '    MsgInformation("WEF Date is Blank, So cann't be saved.")
                    '    GoTo ErrPart
                    'End If

                    SqlStr = "UPDATE INV_RGP_SLIP_HDR SET NRGP_APPROVED='Y', ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                        & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
                        & " WHERE AUTO_KEY_RGPSLIP=" & mMKEY & "" & vbCrLf _
                        & " AND COMPANY_CODE=" & mCompanyCode & ""

                    PubDBCn.Execute(SqlStr)

                    mUpdateCount = mUpdateCount + 1
                End If
            Next
        End With
        PubDBCn.CommitTrans()
        MsgBox("Total " & mUpdateCount & " PO Posted.", MsgBoxStyle.Information)
        CmdSave.Enabled = False

        Exit Sub
ErrPart:
        '    Resume
        If Err.Description <> "" Then
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        PubDBCn.RollbackTrans()
    End Sub

    Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        If FieldsVerification() = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)
        OptSelection(1).Checked = True
        Show1()

        FormatSprdMain()
        cmdShow.Enabled = True
        CmdSave.Enabled = True
    End Sub
    Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        '    If MainClass.ChkIsdateF(txtDateFrom) = False Then txtDateFrom.SetFocus: Exit Function
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then txtDateFrom.SetFocus

        If ChkALL.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(TxtAccount.Text) = "" Then
                MsgInformation("Please Select Account")
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
            Else
                MsgInformation("Please Select Valid Account")
                FieldsVerification = False
                Exit Function
            End If
        End If

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Public Sub frmNRGPApproval_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "NRGP Approval" ''(" & IIf(lblIsCapital.Text = "Y", "Capital Order", "Other Than Capital Order") & ")"
        '    blIsCapital.text
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmNRGPApproval_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing
        Dim pCompanyCode As Long
        Dim CntLst As Long
        Dim mRights As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)

        lstCompanyName.Items.Clear()
        SqlStr = "SELECT COMPANY_CODE, COMPANY_SHORTNAME FROM GEN_COMPANY_MST " & vbCrLf _
            & " ORDER BY COMPANY_SHORTNAME"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If RS.EOF = False Then
            lstCompanyName.Items.Add("ALL")
            CntLst = CntLst + 1
            Do While RS.EOF = False
                pCompanyCode = RS.Fields("COMPANY_CODE").Value
                mRights = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn, pCompanyCode)
                If mRights <> "" Then
                    lstCompanyName.Items.Add(RS.Fields("COMPANY_SHORTNAME").Value)
                    lstCompanyName.SetItemChecked(CntLst, IIf(RS.Fields("COMPANY_SHORTNAME").Value = RsCompany.Fields("COMPANY_SHORTNAME").Value, True, False))
                    CntLst = CntLst + 1
                End If
                RS.MoveNext()
            Loop
        End If

        TxtAccount.Enabled = False
        ChkALL.CheckState = System.Windows.Forms.CheckState.Checked

        FormatSprdMain()
        cmdShow.Enabled = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub lstCompanyName_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles lstCompanyName.ItemCheck

        Try
            If mClickProcess = True Then Exit Sub
            mClickProcess = True

            If e.Index = 0 Then
                If e.NewValue = System.Windows.Forms.CheckState.Checked Then     ''lstCompanyName.GetItemChecked(0) = True Then
                    For I = 1 To lstCompanyName.Items.Count - 1
                        lstCompanyName.SetItemChecked(I, True)
                    Next
                Else
                    For I = 1 To lstCompanyName.Items.Count - 1
                        lstCompanyName.SetItemChecked(I, False)
                    Next
                End If
            Else
                If e.NewValue = System.Windows.Forms.CheckState.Unchecked Then      ''lstCompanyName.GetItemChecked(e.Index - 1) = False Then
                    lstCompanyName.SetItemChecked(0, False)
                End If
            End If
            mClickProcess = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Show1()

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mSuppCustCode As String
        Dim mCompanyName As String
        Dim mCompanyCode As String
        Dim mCompanyCodeStr As String = ""

        Dim CntLst As Long

        SqlStr = "SELECT AUTO_KEY_RGPSLIP, AUTO_KEY_RGPSLIP AS SLIP_NO, TO_CHAR(RGP_SLIP_DATE,'DD/MM/YYYY') AS AMEND_WEF_DATE , " & vbCrLf _
            & " ACM.SUPP_CUST_NAME, PURPOSE, DECODE(GATEPASS_TYPE,'R','RGP','NRGP') AS GATEPASS_TYPE, CMST.COMPANY_SHORTNAME, '' " & vbCrLf _
            & " FROM INV_RGP_SLIP_HDR POMain, FIN_SUPP_CUST_MST ACM, GEN_COMPANY_MST CMST " & vbCrLf _
            & " WHERE POMain.Company_Code=CMST.Company_Code AND POMain.Company_Code=ACM.Company_Code" & vbCrLf _
            & " AND POMain.SUPP_CUST_CODE=ACM.SUPP_CUST_CODE " & vbCrLf _
            & " AND NRGP_APPROVED='N' AND RGP_SLIP_STATUS='N'"

        '& " AND POMain.SUPP_CUST_CODE=ACM.SUPP_CUST_CODE " &


        SqlStr = SqlStr & vbCrLf & " AND (GATEPASS_TYPE='N' OR (GATEPASS_TYPE||PURPOSE<>'RB' AND GATEPASS_TYPE||INTER_UNIT='RN')) "

        If lstCompanyName.GetItemChecked(0) = True Then
            mCompanyCodeStr = ""
        Else
            For CntLst = 1 To lstCompanyName.Items.Count - 1
                If lstCompanyName.GetItemChecked(CntLst) = True Then
                    mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                    If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_SHORTNAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                        mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                    End If
                    mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
                End If
            Next
        End If

        If mCompanyCodeStr <> "" Then
            mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND POMain.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If

        If Trim(TxtAccount.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND ACM.SUPP_CUST_NAME = '" & MainClass.AllowSingleQuote(TxtAccount.Text) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY TO_CHAR(RGP_SLIP_DATE,'DD/MM/YYYY'),AUTO_KEY_RGPSLIP"

        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgInformation(Err.Description)
    End Sub
    Private Sub FormatSprdMain()

        With SprdMain
            .MaxCols = ColPostStatus
            .set_RowHeight(0, RowHeight * 1.5)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColMKEY, 11)
            .ColHidden = True

            .Col = ColRGPType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColRGPType, 8)
            .ColHidden = False

            .Col = ColCompanyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColRGPType, 8)
            .ColHidden = False

            .Col = ColRGPSlipNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColRGPSlipNo, 9)

            .Col = ColRGPSlipDate

            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeDateFormat = SS_CELL_DATE_FORMAT_DDMMYY

            '    .CellType = SS_CELL_TYPE_EDIT
            '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColRGPSlipDate, 9)


            .Col = ColItemValue
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColItemValue, 6)

            .Col = ColPartyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .set_ColWidth(ColPartyName, 25)
            .TypeEditMultiLine = True


            .Col = ColPostStatus
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColPostStatus, 6)
            .Value = CStr(System.Windows.Forms.CheckState.Unchecked)

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, ColCompanyName)
            '    SprdMain.OperationMode = OperationModeSingle
            '    SprdMain.DAutoCellTypes = True
            '    SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            '    SprdMain.GridColor = &HC00000
        End With
        WriteColHeadings()
    End Sub
    Private Sub WriteColHeadings()
        With SprdMain
            .Row = 0

            .Col = ColMKEY
            .Text = "MKey"

            .Col = ColRGPSlipNo
            .Text = "Slip No."

            .Col = ColRGPSlipDate
            .Text = "RGP Slip Date"

            .Col = ColPartyName
            .Text = "Supplier Name"

            .Col = ColItemValue
            .Text = "Purpose"

            .Col = ColRGPType
            .Text = "RGP Type"

            .Col = ColCompanyName
            .Text = "Company Name"

            .Col = ColPostStatus
            .Text = "Post Status"
        End With
    End Sub
    Private Sub frmNRGPApproval_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
    End Sub

    Private Sub optOrderType_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptOrderType.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptOrderType.GetIndex(eventSender)
            cmdShow.Enabled = True
        End If
    End Sub

    Private Sub OptSelection_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptSelection.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptSelection.GetIndex(eventSender)
            Dim CntRow As Integer
            With SprdMain
                For CntRow = 1 To .MaxRows
                    .Row = CntRow
                    .Col = ColPostStatus
                    .Value = IIf(Index = 0, System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                Next
            End With
        End If
    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub
    Private Sub SearchAccounts()

        On Error GoTo ERR1
        MainClass.SearchGridMaster(TxtAccount.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND (SUPP_CUST_TYPE IN ('S','C'))")
        If AcName <> "" Then
            TxtAccount.Text = AcName
        End If
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick

        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xPoNo As Double
        Dim xAmendPONo As Double
        Dim xPOWEF As String
        Dim mType As String
        Dim mCompanyName As String

        'Dim ss As New frmPO

        SprdMain.Row = SprdMain.ActiveRow

        SprdMain.Col = ColMKEY
        xPoNo = Val(SprdMain.Text)

        SprdMain.Col = ColRGPType
        mType = Mid(SprdMain.Text, 1, 1)

        SprdMain.Col = ColCompanyName
        mCompanyName = Trim(SprdMain.Text)


        If xPoNo <= 0 Then Exit Sub

        If mCompanyName <> IIf(IsDBNull(RsCompany.Fields("COMPANY_SHORTNAME").Value), "", RsCompany.Fields("COMPANY_SHORTNAME").Value) Then
            Exit Sub
        End If
        SqlStr = "SELECT * from INV_RGP_SLIP_HDR WHERE AUTO_KEY_RGPSLIP=" & xPoNo & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


        If RsTemp.EOF = False Then
            FrmRGPSlip.MdiParent = Me.MdiParent


            FrmRGPSlip.FrmRGPSlip_Activated(Nothing, New System.EventArgs())

            FrmRGPSlip.txtSlipno.Text = RsTemp.Fields("AUTO_KEY_PO").Value
            FrmRGPSlip.lblBookType.Text = mType

            FrmRGPSlip.Show()

            FrmRGPSlip.txtSlipNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False)) ''txtPONO_Validate False
        End If

    End Sub

    Private Sub txtAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.TextChanged
        cmdShow.Enabled = True
    End Sub

    Private Sub TxtAccount_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.DoubleClick
        SearchAccounts()
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
        Dim SqlStr As String = ""
        On Error GoTo ERR1
        If TxtAccount.Text = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable((TxtAccount.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            TxtAccount.Text = UCase(Trim(TxtAccount.Text))
        Else
            MsgInformation("No Such Account in Account Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub frmNRGPApproval_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)

        If KeyAscii = 6 Then
            SprdMain.Row = 1
            SprdMain.Row2 = SprdMain.MaxRows
            SprdMain.Col = 1
            SprdMain.Col2 = SprdMain.MaxCols '' SprdMain.ActiveCol
            SprdMain.BlockMode = True
            SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
            SprdMain.BlockMode = False

            mSearchKey = ""
            cntSearchRow = 1
            cntSearchCol = 1
            mSearchKey = InputBox("Search :", "Search", mSearchKey)
            If MainClass.SearchIntoFullGrid(SprdMain, ColRGPSlipNo, mSearchKey, cntSearchRow, cntSearchCol) = True Then

                SprdMain.Row = cntSearchRow
                SprdMain.Row2 = cntSearchRow
                SprdMain.Col = 1
                SprdMain.Col2 = SprdMain.MaxCols
                SprdMain.BlockMode = True
                SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFF80)
                SprdMain.BlockMode = False

                MainClass.SetFocusToCell(SprdMain, cntSearchRow, ColPostStatus)
                cntSearchRow = cntSearchRow + 1
                cntSearchCol = cntSearchCol + 1
            End If
        End If
    End Sub

    Private Sub SprdMain_KeyPressEvent(sender As Object, e As _DSpreadEvents_KeyPressEvent) Handles SprdMain.KeyPressEvent
        'Dim KeyAscii As Short = Asc(e.keyAscii)

        'KeyAscii = MainClass.SetNumericField(KeyAscii)
        'EventArgs.KeyChar = Chr(KeyAscii)
        'If KeyAscii = 67 Then
        '    EventArgs.Handled = True
        'End If

        If e.keyAscii = 6 Then
            SprdMain.Row = 1
            SprdMain.Row2 = SprdMain.MaxRows
            SprdMain.Col = 1
            SprdMain.Col2 = SprdMain.MaxCols '' SprdMain.ActiveCol
            SprdMain.BlockMode = True
            SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
            SprdMain.BlockMode = False

            mSearchKey = ""
            cntSearchRow = 1
            cntSearchCol = 1
            mSearchKey = InputBox("Search :", "Search", mSearchKey)
            If MainClass.SearchIntoFullGrid(SprdMain, ColRGPSlipNo, mSearchKey, cntSearchRow, cntSearchCol) = True Then

                SprdMain.Row = cntSearchRow
                SprdMain.Row2 = cntSearchRow
                SprdMain.Col = 1
                SprdMain.Col2 = SprdMain.MaxCols
                SprdMain.BlockMode = True
                SprdMain.BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFF80)
                SprdMain.BlockMode = False

                MainClass.SetFocusToCell(SprdMain, cntSearchRow, ColPostStatus)
                cntSearchRow = cntSearchRow + 1
                cntSearchCol = cntSearchCol + 1
            End If
        End If
    End Sub
End Class
