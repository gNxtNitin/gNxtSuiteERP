Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmTCSChallan
    Inherits System.Windows.Forms.Form
    Dim RsTCSChallan As ADODB.Recordset
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection					
    Dim Shw As Boolean
    Dim FormActive As Boolean
    Dim xRefNo As Integer
    Dim SqlStr As String
    Private Const ColLocked As Short = 1
    Private Const ColBillNo As Short = 2
    Private Const ColBillDate As Short = 3
    Private Const ColPartyName As Short = 4
    Private Const ColDeductAmt As Short = 5
    Private Const ColTaxableAmount As Short = 6
    Private Const ColCessAmt As Short = 7
    Private Const ColSurcharge As Short = 8
    Private Const ColTCSAmount As Short = 9
    Private Const ColMKEY As Short = 10
    Private Const ColChallanMkey As Short = 11
    Private Const ColCompanyCode As Short = 12

    Private Const RowHeight As Short = 12



    Private Sub SetTextLength()
        On Error GoTo ERR1
        txtRefDate.MaxLength = 10
        txtBankName.MaxLength = RsTCSChallan.Fields("BANKNAME").DefinedSize
        txtBankCode.MaxLength = RsTCSChallan.Fields("BANKCODE").DefinedSize
        txtChallanDate.MaxLength = 10
        txtChallanNo.MaxLength = RsTCSChallan.Fields("CHALLANNO").DefinedSize
        txtAmountPaid.MaxLength = RsTCSChallan.Fields("PAIDAMOUNT").Precision

        txtChqNo.MaxLength = RsTCSChallan.Fields("CHQ_NO").DefinedSize
        txtChqDate.MaxLength = 10
        txtTCSAmount.MaxLength = RsTCSChallan.Fields("TCS_AMOUNT").Precision
        txtSurcharge.MaxLength = RsTCSChallan.Fields("SURCHARGE").Precision
        txtCess.MaxLength = RsTCSChallan.Fields("EDU_CESS").Precision
        txtInterest.MaxLength = RsTCSChallan.Fields("INTEREST_AMOUNT").Precision
        txtOthers.MaxLength = RsTCSChallan.Fields("OTHER_AMOUNT").Precision


        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Private Sub Clear1()
        txtRefNo.Text = ""
        txtAmountPaid.Text = "0.00"
        '    txtBankName.Text = ""					
        '    txtBankCode.Text = ""					

        If RsCompany.Fields("COMPANY_CODE").Value = 16 Then
            txtBankName.Text = "AXIS BANK LTD, SECTOR -14 NEAR HUDA OFFICE, GURGAON"
            txtBankCode.Text = "6360057"
        Else
            txtBankName.Text = "AXIS BANK LTD, GARIA BRANCH, KOLKATA"
            txtBankCode.Text = "6360218"
        End If

        txtChallanNo.Text = ""
        txtRefDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtChallanDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        lblMKey.Text = ""

        txtChqNo.Text = ""
        txtChqDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtTCSAmount.Text = "0.00"
        txtSurcharge.Text = "0.00"
        txtCess.Text = "0.00"
        txtInterest.Text = "0.00"
        txtOthers.Text = "0.00"
        cboCollectionCode.SelectedIndex = 4

        txtRefNo.Enabled = True
        MainClass.ClearGrid(SprdMain)
        MainClass.ButtonStatus(Me, XRIGHT, RsTCSChallan, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub cboCollectionCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCollectionCode.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboCollectionCode_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCollectionCode.SelectedIndexChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click
        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            txtRefNo.Enabled = False
            MainClass.ButtonStatus(Me, XRIGHT, RsTCSChallan, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            txtRefNo.Enabled = True
            Show1()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
        Else
            ADDMode = False
            MODIFYMode = False
            txtRefNo.Enabled = True
            If RsTCSChallan.EOF = False Then RsTCSChallan.MoveFirst()
            Show1()
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
        'Resume					
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Close()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If txtRefNo.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub
        If Not RsTCSChallan.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.					
                If Delete1() = False Then GoTo DelErrPart
                If RsTCSChallan.EOF = True Then
                    Clear1()
                Else
                    Show1()
                End If
            End If
        End If
        Exit Sub
DelErrPart:
        MsgBox("Record Not Deleted")
    End Sub
    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr

        SqlStr = ""
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If InsertIntoDelAudit(PubDBCn, "TCS_Challan", (lblMKey.Text), RsTCSChallan, "", "D") = False Then GoTo DeleteErr
        If InsertIntoDeleteTrn(PubDBCn, "TDS_Challan", "MKEY", (lblMKey.Text)) = False Then GoTo DeleteErr

        If DeleteFromInvoice((lblMKey.Text)) = False Then GoTo DeleteErr

        SqlStr = "Delete from TCS_Challan where MKey='" & lblMKey.Text & "' "
        PubDBCn.Execute(SqlStr)


        PubDBCn.CommitTrans()
        RsTCSChallan.Requery()
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans()
        RsTCSChallan.Requery()
        MsgBox(Err.Description)
    End Function





    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        'If FieldsVarification = False Then Exit Sub					

        MainClass.ClearGrid(SprdMain, RowHeight)
        LedgInfo()
        FormatSprdMain()
        Call ReFormatSprdMain()
        SprdMain.Focus()
        MainClass.SetFocusToCell(SprdMain, 1, 4)
    End Sub
    Private Sub FormatSprdMain()
        Dim cntCol As Integer
        With SprdMain
            .MaxCols = ColCompanyCode
            .set_RowHeight(0, RowHeight * 1.75)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColBillNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBillNo, 6)

            .Col = ColBillDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColBillDate, 8)

            .Col = ColPartyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyName, 22)

            For cntCol = ColDeductAmt To ColTCSAmount
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.99")
                .TypeFloatMin = CDbl("-999999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
                .set_ColWidth(cntCol, 8)
            Next

            .ColsFrozen = ColDeductAmt


            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .ColHidden = True

            .Col = ColChallanMkey
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .ColHidden = True

            .Col = ColCompanyCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT

            .ColHidden = True
            .Col = ColLocked
            .set_ColWidth(ColLocked, 6)
            .CellType = SS_CELL_TYPE_CHECKBOX
            .Row = -1
            .Col = ColLocked
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .Row = -1
            .Col = ColLocked
            .Value = CStr(System.Windows.Forms.CheckState.Unchecked)

            Call FillHeading()

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, ColBillNo, ColDeductAmt)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, ColCessAmt, .MaxCols)
            '        SprdMain.OperationMode = OperationModeNormal					
            '        SprdMain.DAutoCellTypes = True					
            '        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH					
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub

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
            FraView.BringToFront()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsTCSChallan, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmTCSChallan_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub OptSelection_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptSelection.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptSelection.GetIndex(eventSender)
            Dim cntRow As Integer
            Dim mAmount As Double
            Dim mTCSAmt As Double
            Dim mSurAmt As Double
            Dim mCESSAmt As Double

            For cntRow = 1 To SprdMain.MaxRows
                SprdMain.Row = cntRow
                SprdMain.Col = ColLocked
                SprdMain.Value = IIf(Index = 0, System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                SprdMain.Row = cntRow
                SprdMain.Col = ColDeductAmt
                mAmount = IIf(Index = 0, Val(SprdMain.Text), 0)

                SprdMain.Col = ColTaxableAmount
                SprdMain.Text = VB6.Format(System.Math.Round(mAmount, 0), "0.00")

                If CDate(txtRefDate.Text) < CDate("01/10/2009") Then
                    SprdMain.Col = ColCessAmt
                    mCESSAmt = mAmount * 100 * 0.022 / 112.2
                    SprdMain.Text = VB6.Format(mCESSAmt, "0.00")

                    SprdMain.Col = ColSurcharge
                    mSurAmt = mAmount * 100 * 0.1 / 112.2
                    SprdMain.Text = VB6.Format(mSurAmt, "0.00")

                    SprdMain.Col = ColTCSAmount
                    mTCSAmt = mAmount - mCESSAmt - mSurAmt
                    SprdMain.Text = VB6.Format(mTCSAmt, "0.00")
                Else
                    SprdMain.Col = ColCessAmt
                    mCESSAmt = 0
                    SprdMain.Text = VB6.Format(mCESSAmt, "0.00")

                    SprdMain.Col = ColSurcharge
                    mSurAmt = 0
                    SprdMain.Text = VB6.Format(mSurAmt, "0.00")

                    SprdMain.Col = ColTCSAmount
                    mTCSAmt = mAmount - mCESSAmt - mSurAmt
                    SprdMain.Text = VB6.Format(mTCSAmt, "0.00")
                End If
            Next
            CalcChallanAmount()
        End If
    End Sub

    Private Sub SprdMain_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdMain.ButtonClicked
        If eventArgs.row = 0 Then Exit Sub
        If eventArgs.col <> ColLocked Then Exit Sub
        If FormActive = False Then Exit Sub

        SprdMain.Row = eventArgs.row
        SprdMain.Col = ColLocked
        '    SprdMain.Value = IIf(SprdMain.Value = vbChecked, vbUnchecked, vbChecked)					
        CalcChallanAmount()
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent
        '    If Row = 0 Then Exit Sub					
        '    If Col <> ColLocked Then Exit Sub					
        '					
        '    SprdMain.Row = Row					
        '    SprdMain.Col = ColLocked					
        '    SprdMain.Value = IIf(SprdMain.Value = vbChecked, vbUnchecked, vbChecked)					
        '    CalcChallanAmount					
        '    MainClass.SaveStatus Me, ADDMode, MODIFYMode					
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell
        On Error GoTo ERR1
        Dim mAmount As Double
        Dim mTCSAmt As Double
        Dim mSurAmt As Double
        Dim mCESSAmt As Double


        If eventArgs.newRow = -1 Then Exit Sub
        Select Case eventArgs.col
            Case ColTaxableAmount
                SprdMain.Row = eventArgs.row

                SprdMain.Col = ColTaxableAmount
                mAmount = System.Math.Round(CDbl(SprdMain.Text), 0)
                SprdMain.Text = VB6.Format(mAmount, "0.00")

                If CDate(txtRefDate.Text) < CDate("01/10/2009") Then
                    mAmount = mAmount
                Else
                    mAmount = 0
                End If

                SprdMain.Col = ColCessAmt
                mCESSAmt = mAmount * 100 * 0.022 / 112.2
                SprdMain.Text = VB6.Format(mCESSAmt, "0.00")

                SprdMain.Col = ColSurcharge
                mSurAmt = mAmount * 100 * 0.1 / 112.2
                SprdMain.Text = VB6.Format(mSurAmt, "0.00")

                SprdMain.Col = ColTCSAmount
                mTCSAmt = mAmount - mCESSAmt - mSurAmt
                SprdMain.Text = VB6.Format(mTCSAmt, "0.00")



        End Select
        CalcChallanAmount()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        txtRefNo.Text = Trim(SprdView.Text)

        txtRefNo_Validating(txtRefNo, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())

    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdMain.ActiveCol, SprdMain.ActiveRow))
    End Sub


    Private Sub frmTCSChallan_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        MainClass.UOpenRecordSet("Select * From TCS_Challan Where 1<>1", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTCSChallan, ADODB.LockTypeEnum.adLockReadOnly)
        SqlStr = ""
        SetTextLength()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call AssignGrid(False)
        Clear1()
        If CmdAdd.Enabled = True Then cmdAdd_Click(CmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmTCSChallan_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection					
        ''PvtDBCn.Open StrConn					
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Left = 0
        Me.Top = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11040)

        cboCollectionCode.Items.Clear()
        cboCollectionCode.Items.Add("A : Alcoholic Liquor for human Consumption")
        cboCollectionCode.Items.Add("B : Timer obtained under a forest lease")
        cboCollectionCode.Items.Add("C : Timber obtained by any mode other than under a forest lease")
        cboCollectionCode.Items.Add("D : Any other forest produce not being timber or tendu leaves")
        cboCollectionCode.Items.Add("E : Scrap")
        cboCollectionCode.Items.Add("F : Parking lot")
        cboCollectionCode.Items.Add("G : Toll plaza")
        cboCollectionCode.Items.Add("H : Mining and Quarrying")
        cboCollectionCode.SelectedIndex = 4

        FormatSprdMain()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmTCSChallan_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        RsTCSChallan = Nothing
        Me.Dispose()
        Me.Close()

        '    PubDBCn.Cancel					
        '    PvtDBCn.Close					
        '    Set PvtDBCn = Nothing					
    End Sub
    Private Sub Show1()
        On Error GoTo ShowErrPart
        Dim mSection As String

        Shw = True
        If Not RsTCSChallan.EOF Then
            txtRefNo.Enabled = True
            With RsTCSChallan
                txtRefNo.Text = IIf(IsDBNull(.Fields("REFNO").Value), "", .Fields("REFNO").Value)
                txtRefDate.Text = VB6.Format(IIf(IsDBNull(.Fields("REFDATE").Value), "", .Fields("REFDATE").Value), "DD/MM/YYYY")
                txtBankName.Text = IIf(IsDBNull(.Fields("BANKNAME").Value), "", .Fields("BANKNAME").Value)
                txtBankCode.Text = IIf(IsDBNull(.Fields("BANKCODE").Value), "", .Fields("BANKCODE").Value)
                txtChallanDate.Text = VB6.Format(IIf(IsDBNull(.Fields("CHALLANDATE").Value), "", .Fields("CHALLANDATE").Value), "DD/MM/YYYY")
                txtChallanNo.Text = IIf(IsDBNull(.Fields("CHALLANNO").Value), "", .Fields("CHALLANNO").Value)
                txtAmountPaid.Text = VB6.Format(IIf(IsDBNull(.Fields("PAIDAMOUNT").Value), 0, .Fields("PAIDAMOUNT").Value), "0.00")
                lblMKey.Text = RsTCSChallan.Fields("mKey").Value

                txtChqNo.Text = IIf(IsDBNull(.Fields("CHQ_NO").Value), "", .Fields("CHQ_NO").Value)
                txtChqDate.Text = VB6.Format(IIf(IsDBNull(.Fields("CHQ_DATE").Value), "", .Fields("CHQ_DATE").Value), "DD/MM/YYYY")
                txtTCSAmount.Text = VB6.Format(IIf(IsDBNull(.Fields("TCS_AMOUNT").Value), 0, .Fields("TCS_AMOUNT").Value), "0.00")
                txtSurcharge.Text = VB6.Format(IIf(IsDBNull(.Fields("SURCHARGE").Value), 0, .Fields("SURCHARGE").Value), "0.00")
                txtCess.Text = VB6.Format(IIf(IsDBNull(.Fields("EDU_CESS").Value), 0, .Fields("EDU_CESS").Value), "0.00")
                txtInterest.Text = VB6.Format(IIf(IsDBNull(.Fields("INTEREST_AMOUNT").Value), 0, .Fields("INTEREST_AMOUNT").Value), "0.00")
                txtOthers.Text = VB6.Format(IIf(IsDBNull(.Fields("OTHER_AMOUNT").Value), 0, .Fields("OTHER_AMOUNT").Value), "0.00")

                If .Fields("COLLECTIONCODE").Value = "A" Then
                    cboCollectionCode.SelectedIndex = 0
                ElseIf .Fields("COLLECTIONCODE").Value = "B" Then
                    cboCollectionCode.SelectedIndex = 1
                ElseIf .Fields("COLLECTIONCODE").Value = "C" Then
                    cboCollectionCode.SelectedIndex = 2
                ElseIf .Fields("COLLECTIONCODE").Value = "D" Then
                    cboCollectionCode.SelectedIndex = 3
                ElseIf .Fields("COLLECTIONCODE").Value = "E" Then
                    cboCollectionCode.SelectedIndex = 4
                ElseIf .Fields("COLLECTIONCODE").Value = "F" Then
                    cboCollectionCode.SelectedIndex = 5
                ElseIf .Fields("COLLECTIONCODE").Value = "G" Then
                    cboCollectionCode.SelectedIndex = 6
                ElseIf .Fields("COLLECTIONCODE").Value = "H" Then
                    cboCollectionCode.SelectedIndex = 7
                End If

                xRefNo = RsTCSChallan.Fields("REFNO").Value
            End With
            Call cmdShow_Click(cmdShow, New System.EventArgs())
        End If
        Shw = False
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsTCSChallan, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        If Update1() = True Then
            txtRefNo_Validating(txtRefNo, New System.ComponentModel.CancelEventArgs(False))
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
    Private Function Update1() As Boolean
        On Error GoTo UpdateError
        Dim mRefNo As Integer
        Dim pMkey As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = ""

        If ADDMode = True Then
            mRefNo = MaxRefNo()
            pMkey = (RsCompany.Fields("COMPANY_CODE").Value) & RsCompany.Fields("FYEAR").Value & mRefNo

            txtRefNo.Text = CStr(mRefNo)

            SqlStr = "INSERT INTO TCS_Challan (MKEY, COMPANY_CODE, FYEAR, " & vbCrLf _
                & " REFNO, REFDATE, COLLECTIONCODE, " & vbCrLf _
                & " BANKNAME, BANKCODE, CHALLANDATE, CHALLANNO, PAIDAMOUNT, " & vbCrLf _
                & " CHQ_NO, CHQ_DATE," & vbCrLf _
                & " TCS_AMOUNT, SURCHARGE, EDU_CESS, " & vbCrLf _
                & " INTEREST_AMOUNT, OTHER_AMOUNT, " & vbCrLf _
                & " ADDUSER, ADDDATE, MODUSER, MODDATE) VALUES ( " & vbCrLf _
                & " '" & pMkey & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf _
                & " " & mRefNo & ", TO_DATE('" & VB6.Format(txtRefDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & VB.Left(cboCollectionCode.Text, 1) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtBankName.Text) & "',  " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtBankCode.Text) & "',  " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtChallanDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtChallanNo.Text) & "', " & Val(txtNetAmount.Text) & ", " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtChqNo.Text) & "'," & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtChqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & Val(txtTCSAmount.Text) & "," & vbCrLf _
                & " " & Val(txtSurcharge.Text) & ", " & Val(txtCess.Text) & "," & vbCrLf _
                & " " & Val(txtInterest.Text) & ", " & Val(txtOthers.Text) & "," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                & " TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"

        Else

            SqlStr = "UPDATE TCS_Challan SET " & vbCrLf _
                & " REFDATE=TO_DATE('" & VB6.Format(txtRefDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " COLLECTIONCODE='" & VB.Left(cboCollectionCode.Text, 1) & "'," & vbCrLf _
                & " BANKNAME='" & MainClass.AllowSingleQuote(txtBankName.Text) & "', " & vbCrLf _
                & " BANKCODE='" & MainClass.AllowSingleQuote(txtBankCode.Text) & "', " & vbCrLf _
                & " CHALLANDATE=TO_DATE('" & VB6.Format(txtChallanDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " CHALLANNO='" & MainClass.AllowSingleQuote(txtChallanNo.Text) & "', " & vbCrLf _
                & " CHQ_NO='" & MainClass.AllowSingleQuote(txtChqNo.Text) & "', " & vbCrLf _
                & " CHQ_DATE=TO_DATE('" & VB6.Format(txtChqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " TCS_AMOUNT=" & Val(txtTCSAmount.Text) & ", " & vbCrLf _
                & " SURCHARGE=" & Val(txtSurcharge.Text) & ", " & vbCrLf _
                & " EDU_CESS=" & Val(txtCess.Text) & ", " & vbCrLf _
                & " INTEREST_AMOUNT=" & Val(txtInterest.Text) & ", " & vbCrLf _
                & " OTHER_AMOUNT=" & Val(txtOthers.Text) & ", " & vbCrLf _
                & " PAIDAMOUNT=" & Val(txtNetAmount.Text) & ", " & vbCrLf _
                & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
                & " WHERE REFNO=" & txtRefNo.Text & " AND MKEY=" & lblMKey.Text & ""

            pMkey = lblMKey.Text
        End If

UpdatePart:
        PubDBCn.Execute(SqlStr)

        If UpdateInvoice(pMkey) = False Then GoTo UpdateError

        PubDBCn.CommitTrans()
        RsTCSChallan.Requery()
        Update1 = True
        Exit Function
UpdateError:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsTCSChallan.Requery()
        If Err.Number = -2147467259 Then
            MsgBox("Can't Modify Transaction Exists Against this Code")
            Exit Function
        End If
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        ''Resume					
        PubDBCn.Errors.Clear()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function FieldsVarification() As Boolean
        On Error GoTo VarificationErr
        FieldsVarification = True

        If Trim(txtBankName.Text) = "" Then
            MsgInformation("Bank Name is empty. Cannot Save")
            txtBankName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtBankCode.Text) = "" Then
            MsgInformation("Bank Code is empty. Cannot Save")
            txtBankCode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Len(txtBankCode.Text) <> 7 Then
            MsgInformation("Invalid Bank Code. Cannot Save")
            txtBankCode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtChallanDate.Text) = "" Then
            MsgInformation("Challan Date is empty. Cannot Save")
            txtChallanDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Not IsDate(txtChallanDate.Text) Then
            MsgInformation("Invalid Challan Date. Cannot Save")
            txtChallanDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtChallanNo.Text) = "" Then
            MsgInformation("Challan No is empty. Cannot Save")
            txtChallanNo.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Val(txtAmountPaid.Text) = 0 Then
            MsgInformation("Deduction Amount is zero. Cannot Save")
            SprdMain.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
        End If
        ''If MODIFYMode = True And (RSTCSChallan.EOF=true Or RSTCSChallan.EOF = True) Then Exit Function					
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Function
VarificationErr:
        FieldsVarification = False
        MsgInformation(Err.Description)
    End Function
    Private Sub txtBankCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBankCode.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtBankCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBankCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtBankCode.Text)
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
    Private Sub txtCess_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCess.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtCess_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCess.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCess_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCess.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcTCSAmount()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtChallanDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtChallanDate.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtChallanDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtChallanDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtChallanDate.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtChallanDate.Text) Then
            MsgBox("Invalid Challan Date", MsgBoxStyle.Information)
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtChallanNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtChallanNo.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtChallanNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtChallanNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtChallanNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtChqDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtChqDate.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtChqDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtChqDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtChqDate.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtChqDate.Text) Then
            MsgBox("Invalid Cheque / DD Date", MsgBoxStyle.Information)
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtChqNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtChqNo.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtChqNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtChqNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtRefDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRefDate.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtInterest_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInterest.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtInterest_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtInterest.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtInterest_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtInterest.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcTCSAmount()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtNetAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNetAmount.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNetAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNetAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtNetAmount_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtNetAmount.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcTCSAmount()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtOthers_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOthers.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOthers_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOthers.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtOthers_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtOthers.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcTCSAmount()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtRefNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRefNo.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRefNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRefNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtRefNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRefNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1

        If Trim(txtRefNo.Text) = "" Then GoTo EventExitSub
        If MODIFYMode = True And RsTCSChallan.EOF = False Then xRefNo = RsTCSChallan.Fields("REFNO").Value

        SqlStr = ""
        SqlStr = "Select * from  TCS_Challan Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " ANd RefNo=" & txtRefNo.Text & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTCSChallan, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTCSChallan.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Click Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                MainClass.UOpenRecordSet("Select * From TCS_Challan Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND RefNo=" & xRefNo & "", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTCSChallan, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If

        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub LedgInfo()
        On Error GoTo LedgError
        Dim SqlStr As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQL()
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")
        Call FormatSprdMain()
        CalcChallanAmount()

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
LedgError:
        MsgInformation(Err.Description)

    End Sub
    Function MakeSQL() As String
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mChallanNo As String

        mChallanNo = lblMKey.Text



        SqlStr = " Select DECODE(ISTCSPAID,'N','0','1') AS LOCKED ," & vbCrLf _
            & " BILLNo," & vbCrLf & " TO_CHAR(INVOICE_DATE,'DD/MM/YYYY') AS BillDate, " & vbCrLf _
            & " ACM.SUPP_CUST_NAME AS PartyName, " & vbCrLf _
            & " TO_CHAR(TCSAMOUNT) As Amount, " & vbCrLf _
            & " TO_CHAR(NETTAXAMOUNT) As Amount, "


        SqlStr = SqlStr & vbCrLf & " '0.00' As CessAmount, " & vbCrLf & " '0.00' As SurAmount, " & vbCrLf _
            & " TO_CHAR(NETTAXAMOUNT,'99999999.99') As TCSAmount,"



        SqlStr = SqlStr & vbCrLf _
            & " IH.Mkey,IH.TCSCHALLANMKEY, IH.COMPANY_CODE " & vbCrLf _
            & " FROM TCS_TRN IH, FIN_SUPP_CUST_MST ACM " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " IH.COMPANY_CODE = ACM.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE = ACM.SUPP_CUST_CODE "

        'SqlStr = SqlStr & vbCrLf & " AND IH.Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & " "

        SqlStr = SqlStr & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

        SqlStr = SqlStr & vbCrLf & " AND IH.INVOICE_DATE<=TO_DATE('" & VB6.Format(txtRefDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        If mChallanNo = "" Then
            SqlStr = SqlStr & vbCrLf & " AND IH.ISTCSPAID='N'"
        Else
            SqlStr = SqlStr & vbCrLf & " AND (IH.TCSCHALLANMKEY='" & MainClass.AllowSingleQuote(mChallanNo) & "' OR IH.TCSCHALLANMKEY='' OR IH.TCSCHALLANMKEY IS NULL)"
        End If

        SqlStr = SqlStr & vbCrLf & " AND IH.CANCELLED='N' AND TCSAMOUNT<>0"


        SqlStr = SqlStr & vbCrLf & " ORDER BY IH.INVOICE_DATE,IH.BILLNO"

        MakeSQL = SqlStr
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        MakeSQL = ""
    End Function
    Private Sub FillHeading()


        With SprdMain
            .Row = 0
            .Col = ColLocked
            .Text = "Update"

            .Col = ColBillNo
            .Text = "Bill No"

            .Col = ColBillDate
            .Text = "Bill Date"

            .Col = ColPartyName
            .Text = "Party Name"

            .Col = ColDeductAmt
            .Text = "Amount"

            .Col = ColTaxableAmount
            .Text = "Amount (R/O)"

            .Col = ColCessAmt
            .Text = "Cess"

            .Col = ColSurcharge
            .Text = "Surcharge"

            .Col = ColTCSAmount
            .Text = "TCS Amount"

            .Col = ColMKEY
            .Text = "MKey"

            .Col = ColChallanMkey
            .Text = "Challan MKey"

            .Col = ColCompanyCode
            .Text = "Company Code"
        End With

    End Sub

    Private Sub CalcChallanAmount()
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mNetAmount As Double
        Dim mCESSAmount As Double
        Dim mSURAmount As Double
        Dim mTCSAMOUNT As Double

        mNetAmount = 0
        mCESSAmount = 0
        mSURAmount = 0
        mTCSAMOUNT = 0
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColLocked

                If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then GoTo NextRow

                .Col = ColTaxableAmount
                mNetAmount = mNetAmount + Val(.Text)

                .Col = ColCessAmt
                mCESSAmount = mCESSAmount + Val(.Text)

                .Col = ColSurcharge
                mSURAmount = mSURAmount + Val(.Text)

                .Col = ColTCSAmount
                mTCSAMOUNT = mTCSAMOUNT + Val(.Text)


NextRow:
            Next
        End With
        mCESSAmount = System.Math.Round(mCESSAmount, 0)
        mSURAmount = System.Math.Round(mSURAmount, 0)
        '    mCESSAmount = Format(mCESSAmount, "0.00")					
        '    mSURAmount = Format(mSURAmount, "0.00")					
        mTCSAMOUNT = mNetAmount - (mCESSAmount + mSURAmount)

        txtAmountPaid.Text = VB6.Format(mNetAmount, "0.00")
        txtCess.Text = VB6.Format(mCESSAmount, "0.00")
        txtSurcharge.Text = VB6.Format(mSURAmount, "0.00")
        txtTCSAmount.Text = VB6.Format(mTCSAMOUNT, "0.00")

        Call CalcTCSAmount()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        '    Resume					
    End Sub

    Private Sub CalcTCSAmount()
        On Error GoTo ErrPart

        txtTCSAmount.Text = CStr(Val(txtAmountPaid.Text) - (Val(txtSurcharge.Text) + Val(txtCess.Text)))
        txtTCSAmount.Text = VB6.Format(txtTCSAmount.Text, "0.00")

        txtNetAmount.Text = CStr(Val(txtAmountPaid.Text) + Val(txtInterest.Text) + Val(txtOthers.Text))
        txtNetAmount.Text = VB6.Format(txtNetAmount.Text, "0.00")
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Function MaxRefNo() As Integer
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        SqlStr = "SELECT MAX(REFNO) AS REFNO FROM TCS_Challan " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = True Then
            MaxRefNo = 1
        Else
            MaxRefNo = IIf(IsDBNull(RsTemp.Fields("REFNO").Value), 1, RsTemp.Fields("REFNO").Value + 1)
        End If

        Exit Function
ErrPart:
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Function

    Private Function UpdateInvoice(ByRef pChallanMKey As String) As Boolean
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mKey As String
        Dim mISTCSPAID As String
        Dim mChallanMkey As String
        Dim mRoNetAmount As Double
        Dim mCompanyCode As Long

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColLocked

                If .Value = CStr(System.Windows.Forms.CheckState.Unchecked) Then
                    mISTCSPAID = "N"
                    mChallanMkey = ""
                Else
                    mISTCSPAID = "Y"
                    mChallanMkey = pChallanMKey
                End If

                .Col = ColMKEY
                mKey = Trim(.Text)

                .Col = ColTaxableAmount
                mRoNetAmount = Val(.Text)

                .Col = ColCompanyCode
                mCompanyCode = Val(.Text)

                SqlStr = " UPDATE FIN_INVOICE_HDR SET " & vbCrLf _
                    & " ISTCSPAID='" & mISTCSPAID & "', " & vbCrLf & " TCSMKEY='" & MainClass.AllowSingleQuote(mChallanMkey) & "', UPDATE_FROM='H'," & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                    & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " WHERE MKey= '" & mKey & "'"

                PubDBCn.Execute(SqlStr)

                SqlStr = " UPDATE TCS_TRN SET " & vbCrLf & " NETTAXAMOUNT=" & mRoNetAmount & ", " & vbCrLf _
                    & " ISTCSPAID='" & mISTCSPAID & "', " & vbCrLf _
                    & " TCSCHALLANMKEY='" & MainClass.AllowSingleQuote(mChallanMkey) & "', UPDATE_FROM='H'," & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                    & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " WHERE MKey= '" & mKey & "'"

                PubDBCn.Execute(SqlStr)

            Next

        End With

        UpdateInvoice = True

        Exit Function
ErrPart:
        UpdateInvoice = False
    End Function


    Private Function DeleteFromInvoice(ByRef pChallanMKey As String) As Boolean
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mKey As String
        Dim mISTCSPAID As String
        Dim mChallanMkey As String

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColLocked

                If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                    mISTCSPAID = "N"
                    mChallanMkey = ""

                    .Col = ColMKEY
                    mKey = Trim(.Text)

                    SqlStr = " UPDATE FIN_INVOICE_HDR SET " & vbCrLf & " ISTCSPAID='" & mISTCSPAID & "', UPDATE_FROM='H'," & vbCrLf & " TCSMKEY='" & MainClass.AllowSingleQuote(mChallanMkey) & "'," & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                        & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " WHERE MKey= '" & mKey & "'"

                    PubDBCn.Execute(SqlStr)

                    SqlStr = " UPDATE TCS_TRN SET " & vbCrLf & " ISTCSPAID='" & mISTCSPAID & "', UPDATE_FROM='H'," & vbCrLf & " TCSCHALLANMKEY='" & MainClass.AllowSingleQuote(mChallanMkey) & "'," & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                        & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " WHERE MKey= '" & mKey & "'"

                    PubDBCn.Execute(SqlStr)

                End If
            Next

        End With

        DeleteFromInvoice = True
        Exit Function
ErrPart:
        DeleteFromInvoice = False
    End Function
    Private Sub ReFormatSprdMain()
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim mChallanNo As String

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColChallanMkey
                mChallanNo = Trim(.Text)

                .Col = ColLocked
                If mChallanNo = "" Then
                    .Value = CStr(System.Windows.Forms.CheckState.Unchecked)
                Else
                    .Value = CStr(System.Windows.Forms.CheckState.Checked)
                End If
            Next
        End With

        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)
        On Error GoTo ERR1
        SqlStr = ""

        SqlStr = " Select TO_CHAR(REFNO,'00000') AS REFNO,TO_CHAR(REFDATE,'DD/MM/YYYY') AS REFDATE, " & vbCrLf & " BANKNAME, CHALLANNO, " & vbCrLf & " TO_CHAR(CHALLANDATE,'DD/MM/YYYY') AS ChallanDate, " & vbCrLf & " TO_CHAR(PAIDAMOUNT) As Amount " & vbCrLf & " FROM TCS_Challan" & vbCrLf & " WHERE " & vbCrLf & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " ORDER BY REFNO"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub FormatSprdView()
        With SprdView
            .Row = -1
            .set_RowHeight(0, 300)
            .set_ColWidth(0, 0)
            .set_ColWidth(1, 1000)
            .set_ColWidth(2, 1000)
            .set_ColWidth(3, 1000)
            .set_ColWidth(4, 2500)
            .set_ColWidth(5, 1500)
            .set_ColWidth(6, 1500)
            .set_ColWidth(7, 1000)
            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub txtSurcharge_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSurcharge.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSurcharge_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSurcharge.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSurcharge_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSurcharge.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcTCSAmount()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtTCSAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTCSAmount.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtTCSAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTCSAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForChallan(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForChallan(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportForChallan(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim All As Boolean
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String
        Dim PrintStatus As Boolean
        Dim mReportFileName As String

        PubDBCn.Errors.Clear()

        PrintStatus = True

        SqlStr = "DELETE FROM Temp_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = ""

        '''''Select Record for print...					

        SqlStr = ""

        SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")

        mTitle = "T.D.S. / T.C.S. Challan"
        mSubTitle = ""

        mReportFileName = "TDSChallan.Rpt"

        Call ShowReport(SqlStr, mReportFileName, Mode, mTitle, mSubTitle)

        SqlStr = "DELETE FROM Temp_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

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
        Dim mAYEAR As String
        Dim mTaxType As String
        Dim mCompanyTan As String
        Dim mCompanyPhone As String
        Dim mCompanyPin As String
        Dim mPaymentCode As String
        Dim mTotalInWords As String
        Dim mAmountStr As String
        Dim CompanyAdd As String

        Dim mAmount As String
        Dim mCroreStr As String
        Dim mLacsStr As String
        Dim mThousandStr As String
        Dim mHundredStr As String
        Dim mTenStr As String
        Dim mUnitStr As String

        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)


        CompanyAdd = IIf(IsDBNull(RsCompany.Fields("REGD_ADDR1").Value), "", RsCompany.Fields("REGD_ADDR1").Value)
        CompanyAdd = CompanyAdd & " " & IIf(IsDBNull(RsCompany.Fields("REGD_ADDR2").Value), "", RsCompany.Fields("REGD_ADDR2").Value)
        CompanyAdd = CompanyAdd & " " & IIf(IsDBNull(RsCompany.Fields("REGD_CITY").Value), "", RsCompany.Fields("REGD_CITY").Value)
        '    CompanyAdd = CompanyAdd & " " & IIf(IsNull(RsCompany!REGD_STATE), "", RsCompany!REGD_STATE)					
        MainClass.AssignCRptFormulas(Report1, "CompanyAddress=""" & CompanyAdd & """")

        mAYEAR = Year(RsCompany.Fields("END_DATE").Value) & "-" & VB6.Format(CDbl(VB6.Format(RsCompany.Fields("END_DATE").Value, "YY")) + 1, "00")

        MainClass.AssignCRptFormulas(Report1, "AYear=""" & mAYEAR & """")

        mTaxType = "0020"
        MainClass.AssignCRptFormulas(Report1, "TaxType=""" & mTaxType & """")

        mCompanyTan = IIf(IsDBNull(RsCompany.Fields("TDSACNO").Value), "", RsCompany.Fields("TDSACNO").Value)
        MainClass.AssignCRptFormulas(Report1, "CompanyTan=""" & mCompanyTan & """")

        mCompanyPhone = "" ''IIf(IsNull(RsCompany!REGD_PHONE), "", RsCompany!REGD_PHONE)					
        MainClass.AssignCRptFormulas(Report1, "CompanyPhone=""" & mCompanyPhone & """")

        mCompanyPin = IIf(IsDBNull(RsCompany.Fields("REGD_PIN").Value), "", RsCompany.Fields("REGD_PIN").Value)
        MainClass.AssignCRptFormulas(Report1, "CompanyPin=""" & mCompanyPin & """")
        MainClass.AssignCRptFormulas(Report1, "PaymentCode=""" & mPaymentCode & """")

        mAmountStr = New String(" ", 12 - Len(VB6.Format(txtTCSAmount.Text, "0"))) & VB6.Format(txtTCSAmount.Text, "0")
        MainClass.AssignCRptFormulas(Report1, "IncomeTax=""" & mAmountStr & """")

        mAmountStr = New String(" ", 12 - Len(VB6.Format(txtSurcharge.Text, "0"))) & VB6.Format(txtSurcharge.Text, "0")
        MainClass.AssignCRptFormulas(Report1, "Surcharge=""" & mAmountStr & """")

        mAmountStr = New String(" ", 12 - Len(VB6.Format(txtCess.Text, "0"))) & VB6.Format(txtCess.Text, "0")
        MainClass.AssignCRptFormulas(Report1, "EduCess=""" & mAmountStr & """")

        mAmountStr = New String(" ", 12 - Len(VB6.Format(txtInterest.Text, "0"))) & VB6.Format(txtInterest.Text, "0")
        MainClass.AssignCRptFormulas(Report1, "Interest=""" & mAmountStr & """")

        mAmountStr = New String(" ", 12 - Len(VB6.Format(txtOthers.Text, "0"))) & VB6.Format(txtOthers.Text, "0")
        MainClass.AssignCRptFormulas(Report1, "Penalty=""" & mAmountStr & """")

        mAmountStr = New String(" ", 12 - Len(VB6.Format(txtNetAmount.Text, "0"))) & VB6.Format(txtNetAmount.Text, "0")
        MainClass.AssignCRptFormulas(Report1, "Total=""" & mAmountStr & """")

        mTotalInWords = MainClass.RupeesConversion(txtNetAmount.Text)

        MainClass.AssignCRptFormulas(Report1, "TotalInWords=""" & mTotalInWords & """")
        MainClass.AssignCRptFormulas(Report1, "ChequeNo=""" & Trim(txtChqNo.Text) & """")
        MainClass.AssignCRptFormulas(Report1, "ChequeDate=""" & Trim(txtChqDate.Text) & """")
        MainClass.AssignCRptFormulas(Report1, "BankName=""" & Trim(txtBankName.Text) & """")

        mAmount = New String("0", 9 - Len(VB6.Format(txtNetAmount.Text, "0"))) & VB6.Format(txtNetAmount.Text, "0")
        mAmountStr = VB.Left(mAmount, 2)
        mTotalInWords = MainClass.RupeesConversion(Val(mAmountStr))
        If Trim(mTotalInWords) = "" Then
            mTotalInWords = "Zero"
        Else
            mTotalInWords = Trim(Mid(mTotalInWords, 1, Len(mTotalInWords) - 5))
        End If
        mCroreStr = mTotalInWords

        mAmountStr = Mid(mAmount, 3, 2)
        mTotalInWords = MainClass.RupeesConversion(Val(mAmountStr))
        If Trim(mTotalInWords) = "" Then
            mTotalInWords = "Zero"
        Else
            mTotalInWords = Trim(Mid(mTotalInWords, 1, Len(mTotalInWords) - 5))
        End If
        mLacsStr = mTotalInWords


        mAmountStr = Mid(mAmount, 5, 2)
        mTotalInWords = MainClass.RupeesConversion(Val(mAmountStr))
        If Trim(mTotalInWords) = "" Then
            mTotalInWords = "Zero"
        Else
            mTotalInWords = Trim(Mid(mTotalInWords, 1, Len(mTotalInWords) - 5))
        End If
        mThousandStr = mTotalInWords

        mAmountStr = Mid(mAmount, 7, 1)
        mTotalInWords = MainClass.RupeesConversion(Val(mAmountStr))
        If Trim(mTotalInWords) = "" Then
            mTotalInWords = "Zero"
        Else
            mTotalInWords = Trim(Mid(mTotalInWords, 1, Len(mTotalInWords) - 5))
        End If
        mHundredStr = mTotalInWords

        mAmountStr = Mid(mAmount, 8, 1)
        mTotalInWords = MainClass.RupeesConversion(Val(mAmountStr))
        If Trim(mTotalInWords) = "" Then
            mTotalInWords = "Zero"
        Else
            mTotalInWords = Trim(Mid(mTotalInWords, 1, Len(mTotalInWords) - 5))
        End If
        mTenStr = mTotalInWords

        mAmountStr = VB.Right(mAmount, 1)
        mTotalInWords = MainClass.RupeesConversion(Val(mAmountStr))
        If Trim(mTotalInWords) = "" Then
            mTotalInWords = "Zero"
        Else
            mTotalInWords = Trim(Mid(mTotalInWords, 1, Len(mTotalInWords) - 5))
        End If
        mUnitStr = mTotalInWords

        MainClass.AssignCRptFormulas(Report1, "CroreStr=""" & mCroreStr & """")
        MainClass.AssignCRptFormulas(Report1, "LacsStr=""" & mLacsStr & """")
        MainClass.AssignCRptFormulas(Report1, "ThousandStr=""" & mThousandStr & """")
        MainClass.AssignCRptFormulas(Report1, "HundredStr=""" & mHundredStr & """")
        MainClass.AssignCRptFormulas(Report1, "TenStr=""" & mTenStr & """")
        MainClass.AssignCRptFormulas(Report1, "UnitStr=""" & mUnitStr & """")

        ' Report1.CopiesToPrinter = PrintCopies					
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub
End Class
