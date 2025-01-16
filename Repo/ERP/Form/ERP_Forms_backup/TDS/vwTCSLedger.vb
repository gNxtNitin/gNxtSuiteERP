Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmViewTCSLedger
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection				
    Private Const RowHeight As Short = 24

    Private Const ColLocked As Short = 1
    Private Const ColBookType As Short = 2
    Private Const ColBookSubType As Short = 3
    Private Const ColVDate As Short = 4
    Private Const ColVNo As Short = 5
    Private Const ColPartyName As Short = 6
    Private Const ColPANNO As Short = 7
    Private Const ColFirmType As Short = 8
    Private Const ColNarration As Short = 9
    Private Const ColAmountPaid As Short = 10
    Private Const ColTCSRate As Short = 11
    Private Const ColDeductAmt As Short = 12
    Private Const ColMKEY As Short = 13
    Private Const ColSubRowNo As Short = 14

    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Dim mClickProcess As Boolean
    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer

    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub chkAllAccount_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllAccount.CheckStateChanged
        Call PrintStatus(False)
        If chkAllAccount.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtPartyName.Enabled = False
            cmdPartySearch.Enabled = False
        Else
            txtPartyName.Enabled = True
            cmdPartySearch.Enabled = True
        End If
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub
    Private Sub SearchParty()
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"


        If MainClass.SearchGridMaster(txtPartyName.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            txtPartyName.Text = AcName
            txtPartyName_Validating(txtPartyName, New System.ComponentModel.CancelEventArgs(False))

        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdPartySearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPartySearch.Click
        Call SearchParty()
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForLedger(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click
        If FieldsVerification() = False Then Exit Sub
        MainClass.ClearGrid(SprdLedg, RowHeight)
        LedgInfo()
        CalcSubTotal()
        FormatSprdLedg()
        SprdLedg.Focus()
        Call PrintStatus(True)
        MainClass.SetFocusToCell(SprdLedg, mActiveRow, 4)
    End Sub
    Private Sub CalcSubTotal()
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mPartyName As String
        Dim StartRow As Integer
        Dim EndRow As Integer
        Dim GAmountPaid As Double
        Dim GDeductAmt As Double

        Call MainClass.AddBlankfpSprdRow(SprdLedg, ColVDate)
        With SprdLedg
            .Row = .MaxRows
            .Col = ColPartyName
            .Font = VB6.FontChangeBold(.Font, True)
            .Text = "GRAND TOTAL"

            .Row = .MaxRows
            .Row2 = .MaxRows
            .Col = 1
            .Col2 = .MaxCols
            .BlockMode = True
            .BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F) ''&H80FF80				
            .BlockMode = False

            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColAmountPaid
                GAmountPaid = GAmountPaid + Val(.Text)

                .Col = ColDeductAmt
                GDeductAmt = GDeductAmt + Val(.Text)
            Next

            .Row = .MaxRows
            .Col = ColAmountPaid
            .Text = CStr(GAmountPaid)
            .Font = VB6.FontChangeBold(.Font, True)

            .Col = ColDeductAmt
            .Text = CStr(GDeductAmt)
            .Font = VB6.FontChangeBold(.Font, True)

        End With

    End Sub
    Function FieldsVerification() As Boolean
        On Error GoTo ERR1


        If MainClass.ChkIsdateF(txtDateFrom) = False Then Exit Function
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function

        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then txtDateFrom.Focus()
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then txtDateTo.Focus()

        If chkAllAccount.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtPartyName.Text) = "" Then
                MsgInformation("Please Select Party Name.")
                Exit Function
            End If

            If MainClass.ValidateWithMasterTable(txtPartyName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = False Then
                MsgInformation("Invalid Party Name.")
                Exit Function
            End If
        End If

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function

    Public Sub frmViewTCSLedger_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmViewTCSLedger_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo BSLError
        Dim SqlStr As String
        Dim CntLst As Long
        Dim RS As ADODB.Recordset

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection				
        ''PvtDBCn.Open StrConn				
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)

        chkAllAccount.CheckState = System.Windows.Forms.CheckState.Checked
        txtPartyName.Enabled = False
        cmdPartySearch.Enabled = False



        lstCompanyName.Items.Clear()
        SqlStr = "SELECT COMPANY_SHORTNAME FROM GEN_COMPANY_MST " & vbCrLf _
            & " ORDER BY COMPANY_SHORTNAME"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        CntLst = 0
        If RS.EOF = False Then
            lstCompanyName.Items.Add("ALL")
            CntLst = CntLst + 1
            Do While RS.EOF = False
                lstCompanyName.Items.Add(RS.Fields("COMPANY_SHORTNAME").Value)
                lstCompanyName.SetItemChecked(CntLst, IIf(RS.Fields("COMPANY_SHORTNAME").Value = RsCompany.Fields("COMPANY_SHORTNAME").Value, True, False))
                RS.MoveNext()
                CntLst = CntLst + 1
            Loop
        End If

        lstCompanyName.SelectedIndex = 0

        Call PrintStatus(True)
        txtDateFrom.Text = RsCompany.Fields("START_DATE").Value
        txtDateTo.Text = CStr(RunDate)
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
    Private Sub LedgInfo()
        On Error GoTo LedgError
        Dim SqlStr As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = MakeSQL()
        MainClass.AssignDataInSprd8(SqlStr, SprdLedg, StrConn, "Y")

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
LedgError:
        MsgInformation(Err.Description)

    End Sub
    Private Function MakeSQL() As String
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mCompanyName As String = ""
        Dim mCompanyCode As String = ""
        Dim mCompanyCodeStr As String = ""

        SqlStr = " Select CC.COMPANY_SHORTNAME ,BookType,BookSubType,TO_CHAR(INVOICE_DATE,'DD/MM/YYYY') AS VDate, " & vbCrLf _
            & " BILLNO AS V_No, ACM.SUPP_CUST_NAME AS PartyName, ACM.PAN_NO, ''," & vbCrLf _
            & " DECODE(CANCELLED,'N',TCSTRN.Remarks,'<<CANCELLED>>'), " & vbCrLf _
            & " DECODE(CANCELLED,'N',TO_CHAR(NETVALUE-TCSAMOUNT),'0.00') AS AMOUNTPAID, " & vbCrLf _
            & " DECODE(CANCELLED,'N',TO_CHAR(TCSPER),'0.0000') AS TDSRATE, " & vbCrLf _
            & " DECODE(CANCELLED,'N',TO_CHAR(TCSAMOUNT),'0.00') As Amount, " & vbCrLf _
            & " TCSTRN.Mkey,TO_CHAR(TCSTRN.SubRowNo) AS SubRowNo " & vbCrLf _
            & " FROM  TCS_TRN TCSTRN, FIN_SUPP_CUST_MST ACM, GEN_COMPANY_MST CC" & vbCrLf _
            & " WHERE TCSTRN.Company_Code = CC.Company_Code AND TCSTRN.SUPP_CUST_CODE = ACM.SUPP_CUST_CODE " & vbCrLf _
            & " AND TCSTRN.Company_Code= ACM.Company_Code  " & vbCrLf _
            & " AND TCSTRN.INVOICE_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND TCSTRN.INVOICE_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND TCSTRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

        ''AND TCSTRN.BOOKCODE=-1		

        ''AND TCSTRN.Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " 

        If lstCompanyName.GetItemChecked(0) = True Then
            mCompanyCodeStr = ""
        Else
            For CntLst = 1 To lstCompanyName.Items.Count - 1
                If lstCompanyName.GetItemChecked(0) = True Then
                    mCompanyCodeStr = ""
                Else
                    If lstCompanyName.GetItemChecked(CntLst) = True Then
                        mCompanyName = VB6.GetItemString(lstCompanyName, CntLst)
                        If MainClass.ValidateWithMasterTable(mCompanyName, "COMPANY_SHORTNAME", "COMPANY_CODE", "GEN_COMPANY_MST", PubDBCn, MasterNo, , "") = True Then
                            mCompanyCode = IIf(IsDBNull(MasterNo), "", MasterNo)
                        End If
                        mCompanyCodeStr = IIf(mCompanyCodeStr = "", mCompanyCode, mCompanyCodeStr & "," & mCompanyCode)
                    End If
                End If
            Next
        End If

        If mCompanyCodeStr <> "" Then
            mCompanyCodeStr = "(" & mCompanyCodeStr & ")"
            SqlStr = SqlStr & vbCrLf & " AND TCSTRN.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If

        If chkAllAccount.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND ACM.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(txtPartyName.Text) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY TCSTRN.INVOICE_DATE,TCSTRN.BILLNO,TCSTRN.BOOKTYPE,TCSTRN.BOOKSUBTYPE,TCSTRN.SUBROWNO "

        MakeSQL = SqlStr
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        MakeSQL = ""
    End Function

    Private Sub FormatSprdLedg()
        With SprdLedg
            .MaxCols = ColSubRowNo
            .set_RowHeight(0, RowHeight * 1.25)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColLocked
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColLocked, 10)
            .ColHidden = False

            .Col = ColBookType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColBookType, 1)
            .ColHidden = True

            .Col = ColBookSubType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColBookSubType, 1)
            .ColHidden = True

            .Col = ColVDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColVDate, 8)

            .Col = ColVNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColVNo, 12)

            .Col = ColDeductAmt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .set_ColWidth(ColDeductAmt, 12)

            .Col = ColPartyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyName, 30)

            .Col = ColPANNO
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPANNO, 15)

            .Col = ColFirmType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColFirmType, 15)

            .Col = ColNarration
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColNarration, 10)

            .Col = ColAmountPaid
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .set_ColWidth(ColAmountPaid, 12)

            .Col = ColTCSRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 4
            .TypeFloatMax = CDbl("999999999.9999")
            .TypeFloatMin = CDbl("-999999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .set_ColWidth(ColTCSRate, 9)

            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .ColHidden = True

            .Col = ColSubRowNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .ColHidden = True

            Call FillHeading()

            MainClass.SetSpreadColor(SprdLedg, -1)
            MainClass.ProtectCell(SprdLedg, 1, .MaxRows, 1, .MaxCols)
            SprdLedg.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            SprdLedg.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            SprdLedg.DAutoCellTypes = True
            SprdLedg.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdLedg.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Sub frmViewTCSLedger_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
    End Sub
    Private Sub SprdLedg_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdLedg.DataColConfig
        SprdLedg.Row = -1
        SprdLedg.Col = eventArgs.col
        SprdLedg.DAutoCellTypes = True
        SprdLedg.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdLedg.TypeEditLen = 1000
    End Sub
    Private Sub SprdLedg_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdLedg.DblClick
        ShowTDSDetail()
    End Sub
    Private Sub ShowTDSDetail()
        Dim MyDate As String
        Dim FormLoaded As Boolean
        Dim mAccountName As String
        Dim mCTYPE As String
        Dim mPartyName As String

        If SprdLedg.ActiveRow < 0 Then
            Exit Sub
        End If

        frmTCSDetail.frmTCSDetail_Load(Nothing, New System.EventArgs())
        frmTCSDetail.frmTCSDetail_Activated(Nothing, New System.EventArgs())
        frmTCSDetail.CmdModify_Click(Nothing, New System.EventArgs())



        frmTCSDetail.CmdAdd.Visible = False

        frmTCSDetail.CmdDelete.Visible = False
        frmTCSDetail.CmdModify.Visible = False
        frmTCSDetail.CmdPreview.Visible = False
        frmTCSDetail.cmdPrint.Visible = False

        frmTCSDetail.cmdSavePrint.Visible = False
        frmTCSDetail.CmdView.Visible = False

        frmTCSDetail.CmdClose.Left = VB6.TwipsToPixelsX(120)
        frmTCSDetail.CmdSave.Left = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(cmdClose.Width) + 230)

        frmTCSDetail.lblBookCode.Text = "-2" ''Child Form				


        mActiveRow = SprdLedg.ActiveRow
        SprdLedg.Row = SprdLedg.ActiveRow

        SprdLedg.Col = ColVNo
        frmTCSDetail.txtVNo.Text = SprdLedg.Text

        SprdLedg.Col = ColMKEY
        frmTCSDetail.lblMKey.Text = SprdLedg.Text

        SprdLedg.Col = ColVDate
        frmTCSDetail.txtVDate.Text = SprdLedg.Text
        frmTCSDetail.txtVDate.ReadOnly = True

        SprdLedg.Col = ColBookType
        frmTCSDetail.lblBookType.Text = Trim(SprdLedg.Text)

        SprdLedg.Col = ColBookSubType
        frmTCSDetail.lblBookSubType.Text = Trim(SprdLedg.Text)

        SprdLedg.Col = ColPartyName
        mPartyName = SprdLedg.Text
        frmTCSDetail.txtPartyName.Text = SprdLedg.Text

        SprdLedg.Col = ColAmountPaid
        frmTCSDetail.txtAmountPaid.Text = SprdLedg.Text

        SprdLedg.Col = ColTCSRate
        frmTCSDetail.txtTCSRate.Text = SprdLedg.Text

        SprdLedg.Col = ColDeductAmt
        frmTCSDetail.txtTCSAmount.Text = SprdLedg.Text


        ''    frmTCSDetail.txtAmountPaid.Enabled = False				
        ''    frmTCSDetail.txtTdsRate.Enabled = False				
        frmTCSDetail.txtTCSAmount.Enabled = False
        frmTCSDetail.TxtVNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(True))
        frmTCSDetail.Show()

        FormLoaded = True


    End Sub
    Private Sub SprdLedg_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdLedg.KeyDownEvent
        If eventArgs.keyCode = System.Windows.Forms.Keys.Return Then
            SprdLedg_DblClick(SprdLedg, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(1, 1))
        End If
        If eventArgs.keyCode = System.Windows.Forms.Keys.Escape Then cmdClose.PerformClick()
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForLedger(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportForLedger(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim All As Boolean
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String
        Dim PrintStatus As Boolean
        Dim mReportFileName As String

        PubDBCn.Errors.Clear()

        PrintStatus = True

        SqlStr = "DELETE FROM TEmp_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = ""

        Call InsertSelectedAcct()

        '''''Select Record for print...				

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        mTitle = "TCS Account Ledger"
        mSubTitle = "From: " & VB6.Format(txtDateFrom.Text, "DD MMM, YYYY") & " To: " & VB6.Format(txtDateTo.Text, "DD MMM, YYYY")

        mReportFileName = "TCSLedger.Rpt"

        Call ShowReport(SqlStr, mReportFileName, Mode, mTitle, mSubTitle)

        SqlStr = "DELETE FROM TEmp_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
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
    Private Sub InsertSelectedAcct()
        On Error GoTo ERR1
        Dim mLocked As String
        Dim mVDate As String
        Dim mVNo As String
        Dim mPartyName As String
        Dim mNarration As String
        Dim mAmountPaid As String
        Dim mTdsRate As String
        Dim mDeductAmt As String
        Dim mMkey As String
        Dim mSubRowNo As String
        Dim SqlStr As String
        Dim cntRow As Integer


        PubDBCn.Errors.Clear()

        PubDBCn.BeginTrans()

        SqlStr = ""
        With SprdLedg

            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow

                .Col = ColLocked
                mLocked = Trim(.Text)
                .Col = ColVDate
                mVDate = Trim(.Text)
                .Col = ColVNo
                mVNo = Trim(.Text)
                .Col = ColPartyName
                mPartyName = Trim(.Text)
                .Col = ColNarration
                mNarration = Trim(.Text)
                .Col = ColAmountPaid
                mAmountPaid = Trim(.Text)
                .Col = ColTCSRate
                mTdsRate = Trim(.Text)
                .Col = ColDeductAmt
                mDeductAmt = Trim(.Text)
                .Col = ColMKEY
                mMkey = Trim(.Text)
                .Col = ColSubRowNo
                mSubRowNo = Trim(.Text)

                SqlStr = "Insert into TEMP_PrintDummyData (UserID,SubRow,Field1," & vbCrLf & " Field2,Field3,Field4,Field5,Field6,Field7) Values (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " " & cntRow + 1 & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(mVDate) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mVNo) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mPartyName) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mNarration) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mAmountPaid) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mTdsRate) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mDeductAmt) & "') "

                PubDBCn.Execute(SqlStr)
            Next
        End With
        PubDBCn.CommitTrans()
        Exit Sub
ERR1:
        'Resume				
        PubDBCn.RollbackTrans()
        MsgInformation(Err.Description)
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        ' Report1.CopiesToPrinter = PrintCopies				
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub
    Private Function FetchRecordForReport(ByRef mSqlStr As String) As String
        mSqlStr = mSqlStr & "SELECT * " & " FROM Temp_PrintDummyData PrintDummyData " & vbCrLf & " WHERE  " & vbCrLf & " UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " ORDER BY SUBROW"

        FetchRecordForReport = mSqlStr

    End Function
    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        Call PrintStatus(False)
    End Sub


    Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel


        If MainClass.ChkIsdateF(txtDateFrom) = False Then
            txtDateFrom.Focus()
            Cancel = True
            Exit Sub
        End If
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then
            txtDateFrom.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub FillHeading()

        With SprdLedg
            .Row = 0
            .Col = ColLocked
            .Text = "Unit Name"

            .Col = ColVDate
            .Text = "Date"

            .Col = ColVNo
            .Text = "VNo"

            .Col = ColDeductAmt
            .Text = "Deducted Amount"

            .Col = ColPartyName
            .Text = "Party Name"

            .Col = ColPANNO
            .Text = "PAN No"

            .Col = ColFirmType
            .Text = "Firm Type"

            .Col = ColNarration
            .Text = "Narration"

            .Col = ColAmountPaid
            .Text = "Amount Paid / Credited"

            .Col = ColTCSRate
            .Text = "Rate at Which deducted"

            .Col = ColMKEY
            .Text = "MKey"

            .Col = ColSubRowNo
            .Text = "SubRowNo"

        End With

    End Sub

    Private Sub txtDateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel


        If MainClass.ChkIsdateF(txtDateTo) = False Then
            txtDateTo.Focus()
            Cancel = True
            Exit Sub
        End If
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then
            txtDateTo.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPartyName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPartyName.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtPartyName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPartyName.DoubleClick
        SearchParty()
    End Sub

    Private Sub txtPartyName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPartyName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtPartyName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPartyName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPartyName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchParty()
    End Sub

    Private Sub txtPartyName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPartyName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim SqlStr As String
        On Error GoTo ERR1
        If txtPartyName.Text = "" Then GoTo EventExitSub


        If MainClass.ValidateWithMasterTable(txtPartyName.Text, "SUPP_CUST_NAME", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')") = True Then
            txtPartyName.Text = MasterNo
        Else
            MsgInformation("No Such Party Name in Account Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub frmViewTCSLedger_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        SprdLedg.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 190, mReFormWidth - 190, mReFormWidth))
        Frame4.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11379.7, 749)
        CurrFormWidth = mReFormWidth

        MainClass.SetSpreadColor(SprdLedg, -1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
End Class
