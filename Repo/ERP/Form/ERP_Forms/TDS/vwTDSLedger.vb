Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmViewTDSLedger
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    ''Dim PvtDBCn As ADODB.Connection				
    Dim mAccountCode As String
    Private Const RowHeight As Short = 24

    Private Const ColLocked As Short = 1
    Private Const ColBookType As Short = 2
    Private Const ColBookSubType As Short = 3
    Private Const ColVDate As Short = 4
    Private Const ColVNo As Short = 5
    Private Const ColPartyName As Short = 6
    Private Const ColPANNO As Short = 7
    Private Const ColFirmType As Short = 8
    Private Const ColSection As Short = 9
    Private Const ColNarration As Short = 10
    Private Const ColAmountPaid As Short = 11
    Private Const ColTDSRate As Short = 12
    Private Const ColDeductAmt As Short = 13
    Private Const ColMKEY As Short = 14
    Private Const ColSubRowNo As Short = 15

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer

    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Dim mClickProcess As Boolean
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

    Private Sub chkAllSection_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllSection.CheckStateChanged
        Call PrintStatus(False)
        If chkAllSection.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtSectionName.Enabled = False
            cmdSection.Enabled = False
        Else
            txtSectionName.Enabled = True
            cmdSection.Enabled = True
        End If
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub
    Private Sub SearchAccounts()
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND HEADTYPE='T'"
        If MainClass.SearchGridMaster(TxtAccount.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            TxtAccount.Text = AcName
            txtAccount_Validating(TxtAccount, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        Call SearchAccounts()
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
        If FYChk(CStr(CDate(txtDateFrom.Text))) = False Then txtDateFrom.Focus()
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then txtDateTo.Focus()
        If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND HEADTYPE='T'") = True Then
            mAccountCode = MasterNo
        Else
            MsgInformation("Please Select Account")
            Exit Function
        End If


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

        If chkAllSection.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtSectionName.Text) = "" Then
                MsgInformation("Please Select Section Name.")
                Exit Function
            End If

            If MainClass.ValidateWithMasterTable(txtSectionName.Text, "NAME", "NAME", "TDS_SECTION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invalid Section Name.")
                Exit Function
            End If
        End If

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function

    Public Sub frmViewTDSLedger_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmViewTDSLedger_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
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

        chkAllSection.CheckState = System.Windows.Forms.CheckState.Checked
        txtSectionName.Enabled = False
        cmdSection.Enabled = False

        OptType(0).Checked = True



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
        Dim mPartyCode As String
        Dim mSectionName As String
        Dim mCompanyName As String = ""
        Dim mCompanyCode As String = ""
        Dim mCompanyCodeStr As String = ""

        SqlStr = " Select CC.COMPANY_SHORTNAME ,BookType,BookSubType,TO_CHAR(Vdate,'DD/MM/YYYY') AS VDate, " & vbCrLf _
            & " Vno AS V_No, DECODE(PartyName,'-1','',PartyName) AS PartyName, ACM1.PAN_NO, ''," & vbCrLf _
            & " TDSSection.Name As SectionName, " & vbCrLf & " DECODE(CANCELLED,'N',TDSTRN.Remarks,'<<CANCELLED>>'), " & vbCrLf _
            & " DECODE(CANCELLED,'N',TO_CHAR(AMOUNTPAID),'0.00') AS AMOUNTPAID, " & vbCrLf & " DECODE(CANCELLED,'N',TO_CHAR(TDSRATE),'0.00') AS TDSRATE, " & vbCrLf _
            & " DECODE(CANCELLED,'N',TO_CHAR(TDSAMOUNT),'0.00') As Amount, " & vbCrLf _
            & " TDSTRN.Mkey,TO_CHAR(TDSTRN.SubRowNo) AS SubRowNo " & vbCrLf _
            & " FROM  TDS_TRN TDSTRN, TDS_Section_MST TDSSection, FIN_SUPP_CUST_MST ACM, GEN_COMPANY_MST CC, FIN_SUPP_CUST_MST ACM1 " & vbCrLf _
            & " WHERE TDSTRN.Company_Code =CC.Company_Code  " & vbCrLf _
            & " AND TDSTRN.AccountCode = ACM.SUPP_CUST_CODE AND TDSTRN.Company_Code= ACM.Company_Code" & vbCrLf _
            & " AND TDSTRN.PartyCode = ACM1.SUPP_CUST_CODE AND TDSTRN.Company_Code= ACM1.Company_Code" & vbCrLf _
            & " AND TDSTRN.SectionCode = TDSSection.Code(+) " & vbCrLf _
            & " AND TDSTRN.Company_Code= TDSSection.Company_Code(+) " & vbCrLf _
            & " AND TDSTRN.Vdate>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND TDSTRN.Vdate<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf _
            & " AND TDSTRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
            & " AND TDSTRN.AccountCode = '" & mAccountCode & "'"

        ''AND TDSTRN.Company_Code = " & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " 
        ''AND TDSTRN.BOOKCODE=-1				

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
            SqlStr = SqlStr & vbCrLf & " AND TDSTRN.COMPANY_CODE IN " & mCompanyCodeStr & ""
        End If

        If chkAllAccount.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtPartyName.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mPartyCode = MasterNo
            Else
                mPartyCode = "-1"
            End If
            SqlStr = SqlStr & vbCrLf & " AND TDSTRN.PARTYCODE='" & MainClass.AllowSingleQuote(mPartyCode) & "'"
        End If

        If chkAllSection.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtSectionName.Text, "NAME", "NAME", "TDS_SECTION_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSectionName = MasterNo
            Else
                mSectionName = ""
            End If
            SqlStr = SqlStr & vbCrLf & " AND TDSSection.Name='" & MainClass.AllowSingleQuote(mSectionName) & "'"
        End If

        If chkUnderDed.CheckState = System.Windows.Forms.CheckState.Checked Then
            SqlStr = SqlStr & vbCrLf & " AND ISLOWERDED='Y'"
        End If

        If OptType(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND PartyName='-1'"
        ElseIf OptType(2).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND PartyName<>'-1'"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY TDSTRN.Vdate,TDSTRN.Vno,TDSTRN.BOOKTYPE,TDSTRN.BOOKSUBTYPE,TDSTRN.SUBROWNO "

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
            .set_ColWidth(ColVNo, 8)

            .Col = ColDeductAmt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .set_ColWidth(ColDeductAmt, 8)

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

            .Col = ColSection
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColSection, 6)

            .Col = ColNarration
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeMaxEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColNarration, 10)

            .Col = ColAmountPaid
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .set_ColWidth(ColAmountPaid, 8)

            .Col = ColTDSRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 3
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .set_ColWidth(ColTDSRate, 7)

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
    Private Sub frmViewTDSLedger_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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

        If TxtAccount.Text <> "" Then
            mAccountName = TxtAccount.Text
        End If

        If SprdLedg.ActiveRow < 0 Then
            Exit Sub
        End If

        frmTDSDetail.frmTDSDetail_Load(Nothing, New System.EventArgs())
        frmTDSDetail.frmTDSDetail_Activated(Nothing, New System.EventArgs())
        frmTDSDetail.CmdModify_Click(Nothing, New System.EventArgs())



        frmTDSDetail.CmdAdd.Visible = False

        frmTDSDetail.CmdDelete.Visible = False
        frmTDSDetail.CmdModify.Visible = False
        frmTDSDetail.CmdPreview.Visible = False
        frmTDSDetail.cmdPrint.Visible = False

        frmTDSDetail.cmdSavePrint.Visible = False
        frmTDSDetail.CmdView.Visible = False

        frmTDSDetail.CmdClose.Left = VB6.TwipsToPixelsX(120)
        frmTDSDetail.CmdSave.Left = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(cmdClose.Width) + 230)

        frmTDSDetail.lblBookCode.Text = "-1" ''Child Form				

        frmTDSDetail.TxtAccount.Text = TxtAccount.Text
        frmTDSDetail.TxtAccount.ReadOnly = True

        mActiveRow = SprdLedg.ActiveRow
        SprdLedg.Row = SprdLedg.ActiveRow

        SprdLedg.Col = ColVNo
        frmTDSDetail.txtVNo.Text = SprdLedg.Text

        SprdLedg.Col = ColMKEY
        frmTDSDetail.lblMKey.Text = SprdLedg.Text

        SprdLedg.Col = ColVDate
        frmTDSDetail.txtVDate.Text = SprdLedg.Text
        frmTDSDetail.txtVDate.ReadOnly = True

        SprdLedg.Col = ColBookType
        frmTDSDetail.lblBookType.Text = Trim(SprdLedg.Text)

        SprdLedg.Col = ColBookSubType
        frmTDSDetail.lblBookSubType.Text = Trim(SprdLedg.Text)

        SprdLedg.Col = ColPartyName
        mPartyName = SprdLedg.Text
        frmTDSDetail.txtPartyName.Text = SprdLedg.Text

        SprdLedg.Col = ColSection
        frmTDSDetail.txtSection.Text = SprdLedg.Text

        SprdLedg.Col = ColAmountPaid
        frmTDSDetail.txtAmountPaid.Text = SprdLedg.Text

        SprdLedg.Col = ColTDSRate
        frmTDSDetail.txtTdsRate.Text = SprdLedg.Text

        SprdLedg.Col = ColDeductAmt
        frmTDSDetail.txtTDSAmount.Text = SprdLedg.Text

        If MainClass.ValidateWithMasterTable(mPartyName, "SUPP_CUST_NAME", "CTYPE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCTYPE = IIf(IsDBNull(MasterNo), "C", MasterNo)
        Else
            mCTYPE = "C"
        End If

        frmTDSDetail.cboCType.SelectedIndex = IIf(mCTYPE = "C", 0, 1)

        ''    frmTDSDetail.txtAmountPaid.Enabled = False				
        ''    frmTDSDetail.txtTdsRate.Enabled = False				
        frmTDSDetail.txtTDSAmount.Enabled = False

        frmTDSDetail.Show()

        FormLoaded = True


    End Sub
    Private Sub SprdLedg_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdLedg.KeyDownEvent
        If eventArgs.keyCode = System.Windows.Forms.Keys.Return Then
            SprdLedg_DblClick(SprdLedg, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(1, 1))
        End If
        If eventArgs.keyCode = System.Windows.Forms.Keys.Escape Then cmdClose.PerformClick()
    End Sub
    Private Sub txtAccount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtAccount.TextChanged
        Call PrintStatus(False)
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
        Dim SqlStr As String
        On Error GoTo ERR1
        lblAcCode.Text = ""
        If TxtAccount.Text = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable(TxtAccount.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND HEADTYPE='T'") = True Then
            lblAcCode.Text = MasterNo
            TxtAccount.Text = UCase(Trim(TxtAccount.Text))
        Else
            lblAcCode.Text = ""
            MsgInformation("No Such Account in Account Master")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
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

        If TxtAccount.Text = "" Then PrintStatus = False Else PrintStatus = True

        SqlStr = "DELETE FROM TEmp_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = ""

        Call InsertSelectedAcct()

        '''''Select Record for print...				

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        mTitle = "TDS Account Ledger (" & TxtAccount.Text & ")"
        mSubTitle = "From: " & VB6.Format(txtDateFrom.Text, "DD MMM, YYYY") & " To: " & VB6.Format(txtDateTo.Text, "DD MMM, YYYY")

        mReportFileName = "TDSLedger.Rpt"

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
        Dim mSection As String
        Dim mNarration As String
        Dim mAmountPaid As String
        Dim mTdsRate As String
        Dim mDeductAmt As String
        Dim mMkey As String
        Dim mSubRowNo As String
        Dim mTDSAccountName As String
        Dim SqlStr As String
        Dim cntRow As Integer


        PubDBCn.Errors.Clear()

        PubDBCn.BeginTrans()

        mTDSAccountName = TxtAccount.Text

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
                .Col = ColSection
                mSection = Trim(.Text)
                .Col = ColNarration
                mNarration = Trim(.Text)
                .Col = ColAmountPaid
                mAmountPaid = Trim(.Text)
                .Col = ColTDSRate
                mTdsRate = Trim(.Text)
                .Col = ColDeductAmt
                mDeductAmt = Trim(.Text)
                .Col = ColMKEY
                mMkey = Trim(.Text)
                .Col = ColSubRowNo
                mSubRowNo = Trim(.Text)

                SqlStr = "Insert into TEMP_PrintDummyData (UserID,SubRow,Field1," & vbCrLf & " Field2,Field3,Field4,Field5,Field6,Field7," & vbCrLf & " Field8) Values (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " " & cntRow + 1 & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(mVDate) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mVNo) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mPartyName) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mSection) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mNarration) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mAmountPaid) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mTdsRate) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mDeductAmt) & "') "

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
            .Text = "PAN No Name"

            .Col = ColFirmType
            .Text = "Firm Type"

            .Col = ColSection
            .Text = "Section Name"

            .Col = ColNarration
            .Text = "Narration"

            .Col = ColAmountPaid
            .Text = "Amount Paid / Credited"

            .Col = ColTDSRate
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

    Private Sub txtSectionName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSectionName.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtSectionName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSectionName.DoubleClick
        cmdSection_Click(cmdSection, New System.EventArgs())
    End Sub


    Private Sub txtSectionName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSectionName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtSectionName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtSectionName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSectionName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSection_Click(cmdSection, New System.EventArgs())
    End Sub

    Private Sub txtSectionName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSectionName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtSectionName.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtSectionName.Text, "NAME", "NAME", "TDS_SECTION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Invalid Section Name.", vbInformation)
            Cancel = True
        End If

        GoTo EventExitSub
ErrPart:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cmdSection_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSection.Click
        Dim mFieldName As String
        If MainClass.SearchMaster(txtSectionName.Text, "TDS_Section_MST", "NAME", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtSectionName.Text = AcName
            txtSectionName.Focus()
        End If
    End Sub

    Private Sub frmViewTDSLedger_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer
        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)
        SprdLedg.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 300, mReFormWidth))
        Frame4.Width = VB6.ToPixelsUserWidth(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth), 11379.7, 749)
        CurrFormWidth = mReFormWidth
        'MainClass.SetSpreadColor(UltraGrid1, -1)

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
End Class
