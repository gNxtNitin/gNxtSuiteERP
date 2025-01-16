Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Imports VB = Microsoft.VisualBasic
Friend Class frmParamFollowupReg
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20

    Private Const ColSupplierCode As Short = 1
    Private Const ColSupplierName As Short = 2
    Private Const ColDSNo As Short = 3
    Private Const ColAmendNo As Short = 4
    Private Const ColItemCode As Short = 5
    Private Const ColItemDesc As Short = 6
    Private Const ColType As Short = 7
    Private Const ColStockQty As Short = 8
    Dim ColMaxCol As Integer

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Dim pmyMenu As String

    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub cboExportItem_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboExportItem.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub cboExportItem_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboExportItem.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboItemType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboItemType.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub cboItemType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboItemType.SelectedIndexChanged
        Call PrintStatus(False)
    End Sub



    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        Call PrintStatus(False)
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtItemName.Enabled = False
            cmdsearch.Enabled = False
        Else
            txtItemName.Enabled = True
            cmdsearch.Enabled = True
        End If
    End Sub

    Private Sub chkAllSupp_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllSupp.CheckStateChanged
        Call PrintStatus(False)
        If chkAllSupp.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtSupplier.Enabled = False
            cmdsearchSupp.Enabled = False
        Else
            txtSupplier.Enabled = True
            cmdsearchSupp.Enabled = True
        End If
    End Sub


    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub cmdeMail_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdeMail.Click

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTo As String
        Dim mCC As String
        Dim mFrom As String
        Dim mAttachmentFile As String
        Dim mSubject As String

        Dim mSupplierName As String = ""
        Dim mCity As String
        Dim mAddress As String
        Dim mPin As String
        Dim mState As String

        Dim mBodyTextHeader As String
        Dim mBodyText As String
        Dim mBodyTextDetail As String = ""
        Dim CntNo As Integer

        Dim mPreviousSupplierCode As String
        Dim mSupplierCode As String
        Dim mDSNo As String = ""
        Dim mAmendNo As String
        Dim mItemCode As String
        Dim mItemDesc As String

        Dim cntRow As Integer
        Dim cntCol As Integer

        Dim mMailCount As Integer

        ' *****************************************************************************
        ' This is where all of the Components Properties are set / Methods called
        ' *****************************************************************************

        mMailCount = 0


        mFrom = GetEMailID("MAIL_FROM")
        mCC = GetEMailID("PUR_MAIL_TO")

        mAttachmentFile = ""

        mSubject = "Delivery Schedule for the month of " & VB6.Format(lblNewDate.Text, "MMMM , YYYY")

        mBodyTextHeader = "<table width=6500 align=center border=1 cellPadding=2 cellSpacing=1>" & "<tr>" & "<td width=1000 align=center><b>Item Code & Name</b></td>" & "<td width=100 align=center><b>01</b></td>" & "<td width=100 align=center><b>02</b></td>" & "<td width=100 align=center><b>03</b></td>" & "<td width=100 align=center><b>04</b></td>" & "<td width=100 align=center><b>05</b></td>" & "<td width=100 align=center><b>06</b></td>" & "<td width=100 align=center><b>07</b></td>" & "<td width=100 align=center><b>08</b></td>" & "<td width=100 align=center><b>09</b></td>" & "<td width=100 align=center><b>10</b></td>" & "<td width=100 align=center><b>11</b></td>" & "<td width=100 align=center><b>12</b></td>" & "<td width=100 align=center><b>13</b></td>" & "<td width=100 align=center><b>14</b></td>" & "<td width=100 align=center><b>15</b></td>" & "<td width=100 align=center><b>16</b></td>" & "<td width=100 align=center><b>17</b></td>" & "<td width=100 align=center><b>18</b></td>" & "<td width=100 align=center><b>19</b></td>" & "<td width=100 align=center><b>20</b></td>"

        mBodyTextHeader = mBodyTextHeader & "<td width=100 align=center><b>21</b></td>" & "<td width=100 align=center><b>22</b></td>" & "<td width=100 align=center><b>23</b></td>" & "<td width=100 align=center><b>24</b></td>" & "<td width=100 align=center><b>25</b></td>" & "<td width=100 align=center><b>26</b></td>" & "<td width=100 align=center><b>27</b></td>" & "<td width=100 align=center><b>28</b></td>" & "<td width=100 align=center><b>29</b></td>" & "<td width=100 align=center><b>30</b></td>" & "<td width=100 align=center><b>31</b></td>" & "<td width=100 align=center><b>Total</b></td>" & "</tr>"

        mPreviousSupplierCode = ""
        With SprdMain
            For cntRow = 1 To .MaxRows

                .Row = cntRow
                .Col = ColSupplierCode
                mSupplierCode = Trim(.Text)

                If cntRow = 1 Then
                    mBodyTextDetail = mBodyTextHeader
                    mPreviousSupplierCode = mSupplierCode
                End If

                .Col = ColSupplierName
                mSupplierName = Trim(.Text)

                .Col = ColType
                If Trim(.Text) = "P" Then
                    .Col = ColDSNo
                    mDSNo = Trim(.Text)

                    .Col = ColAmendNo
                    mAmendNo = Trim(.Text)

                    .Col = ColItemCode
                    mItemCode = Trim(.Text)

                    .Col = ColItemDesc
                    mItemDesc = Trim(.Text)

                    mBodyTextDetail = mBodyTextDetail & "<tr>" & "<td align=Left>" & mItemCode & " - " & mItemDesc & "</td>"

                    For cntCol = ColStockQty + 1 To .MaxCols
                        .Col = cntCol
                        mBodyTextDetail = mBodyTextDetail & "<td align=Right>" & VB6.Format(.Text, "0") & "</td>"
                    Next


                    mBodyTextDetail = mBodyTextDetail & "</tr>"
                End If

                If mSupplierCode <> mPreviousSupplierCode Or cntRow = .MaxRows Then
                    SqlStr = "SELECT SUPP_CUST_MAILID,SUPP_CUST_ADDR,SUPP_CUST_CITY,SUPP_CUST_STATE, SUPP_CUST_PIN " & vbCrLf & " FROM FIN_SUPP_CUST_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSupplierCode) & "'"
                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                    If RsTemp.EOF = False Then
                        mAddress = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_ADDR").Value), "", RsTemp.Fields("SUPP_CUST_ADDR").Value)
                        mCity = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_CITY").Value), "", RsTemp.Fields("SUPP_CUST_CITY").Value)
                        mState = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_STATE").Value), "", RsTemp.Fields("SUPP_CUST_STATE").Value)
                        mPin = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_PIN").Value), "", RsTemp.Fields("SUPP_CUST_PIN").Value)
                        mTo = IIf(IsDbNull(RsTemp.Fields("SUPP_CUST_MAILID").Value), "", RsTemp.Fields("SUPP_CUST_MAILID").Value)

                        If Len(mTo) <= 5 Then
                            mTo = ""
                        End If

                        If InStr(1, mTo, "@") = 0 Then
                            mTo = ""
                        End If


                        mBodyTextDetail = mBodyTextDetail & "</table>"
                        mBodyText = "<html><body>To,<br />" & "<b>M/s </b>" & mSupplierName & "<br />" & "" & mAddress & "<br />" & "" & mCity & "<br />" & "" & mState & "<br />" & "" & mPin & "<br />" & "<br />" & "<br />" & "<b>Delivery Schedule No : </b>" & mDSNo & "<br />" & "<br />" & "<br />" & mBodyTextDetail & "<br />" & "<br />" & "<b>Please follow the instruction given as under : </b><br />" & "1. Our item code & description of material must be mention on your bill.<br />" & "2. Material dispatch advice should be send through eMail.<br />" & "3. Ensure material should come in standard packing (Qty. / Material description / Item code of material / Tag).<br />" & "4. Material Inspection report / MTC must be send along with each and every consignment.<br />" & "<br />" & "<br />" & "Your Faithfully<br />" & "for " & RsCompany.Fields("Company_Name").Value & "<br />" & "</body></html>"

                        If Trim(mTo) <> "" Then
                            If SendMailProcess(mFrom, mTo, mCC, "", strAccount, mSubject, mBodyText) = False Then GoTo ErrPart
                            mMailCount = mMailCount + 1
                        End If
                    End If

                    mBodyTextDetail = mBodyTextHeader
                End If

                .Col = ColSupplierCode
                mPreviousSupplierCode = Trim(.Text)

            Next
        End With


        MsgInformation("Total " & mMailCount & " Mail/s sucessfully send.")


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonPO(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonPO(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportonPO(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ReportErr
        Dim SqlStr As String = ""
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""


        Report1.Reset()

        If InsertIntoPrintdummyData = False Then GoTo ReportErr

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        mTitle = "Shortage Follow-up register for the month of " & VB6.Format(lblNewDate.Text, "MMMM , YYYY")
        mSubTitle = ""
        If optShow(0).Checked = True Then
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\ShortageFollowup.rpt"
        Else
            Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\ShortageFollowupWeek.rpt"
        End If

        '----------
        SqlStr = ""
        SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")
        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)

        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Function InsertIntoPrintdummyData() As Boolean

        Dim SqlStr As String = ""
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mInsertSQL As String
        Dim mValueSQL As String
        Dim mFieldStr As String
        Dim mValueStr As String
        Dim mPartyAddress As String
        Dim mPartyCode As String

        On Error GoTo ERR1

        PubDBCn.Errors.Clear()

        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_PRINTDUMMYDATA WHERE USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = ""
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColSupplierCode
                mPartyCode = Trim(.Text)
                mPartyAddress = GetPartyAddress(mPartyCode)

                mInsertSQL = ""
                mValueSQL = ""
                SqlStr = ""

                mInsertSQL = "Insert into TEMP_PrintDummyData (UserID,SubRow,"
                mValueSQL = " Values (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " " & cntRow & ", "


                For cntCol = 1 To .MaxCols
                    .Col = cntCol

                    If cntCol = .MaxCols Then
                        mFieldStr = "FIELD" & cntCol
                        mValueStr = "'" & MainClass.AllowSingleQuote(.Text) & "'"
                    Else
                        mFieldStr = "FIELD" & cntCol & ","
                        mValueStr = "'" & MainClass.AllowSingleQuote(.Text) & "'" & ","
                    End If
                    mInsertSQL = mInsertSQL & mFieldStr
                    mValueSQL = mValueSQL & mValueStr


                Next
                mInsertSQL = mInsertSQL & ",FIELD55)"
                mValueSQL = mValueSQL & ",'" & MainClass.AllowSingleQuote(mPartyAddress) & "')"

                SqlStr = mInsertSQL & vbCrLf & mValueSQL
                PubDBCn.Execute(SqlStr)
            Next
        End With
        PubDBCn.CommitTrans()
        InsertIntoPrintdummyData = True
        Exit Function
ERR1:
        'Resume
        PubDBCn.RollbackTrans()
        InsertIntoPrintdummyData = False
        MsgInformation(Err.Description)
    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, True, pmyMenu)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        SearchItem()
    End Sub

    Private Sub cmdsearchSupp_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchSupp.Click
        SearchSupplier()
    End Sub


    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo ErrPart
        If FieldsVerification = False Then Exit Sub
        MainClass.ClearGrid(SprdMain, RowHeight)

        If Show1 = False Then GoTo ErrPart
        Call InsertRecdQty()
        FormatSprdMain(-1)
        Call PrintStatus(True)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamFollowupReg_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Shortage Follow-Up Register"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmParamFollowupReg_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        pmyMenu = myMenu
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)

        txtMonth.Enabled = True
        lblNewDate.Text = CStr(RunDate)
        txtMonth.Text = MonthName(Month(RunDate)) & ", " & Year(RunDate)

        chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        txtItemName.Enabled = False
        cmdsearch.Enabled = False
        txtSupplier.Enabled = False
        cmdsearchSupp.Enabled = False

        chkAllCategory.CheckState = System.Windows.Forms.CheckState.Checked
        txtCategory.Enabled = False
        cmdsearchCategory.Enabled = False

        chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Checked
        txtSubCategory.Enabled = False
        cmdSubCatsearch.Enabled = False

        Call PrintStatus(True)
        Call FillPOCombo()
        txtDateTo.Text = "01/" & VB6.Format(RunDate, "MM/YYYY")

        OptShow(0).Checked = True
        ColMaxCol = ColStockQty + 32 + 1
        Call FormatSprdMain(-1)
        'Call FillGridHeader()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub

    Private Sub txtMonth_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Call PrintStatus(False)
    End Sub

    'Private Sub UpDMonth_DownClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles UpDMonth.DownClick
    '    SetNewDate(-1)
    '    txtMonth.Text = MonthName(Month(CDate(lblNewDate.Text))) & ", " & Year(CDate(lblNewDate.Text))
    '    Call PrintStatus(False)
    'End Sub
    'Sub SetNewDate(ByRef prmSpinDirection As Short)
    '    lblNewDate.Text = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, prmSpinDirection, CDate(lblNewDate.Text)))
    'End Sub
    'Private Sub UpDMonth_UpClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles UpDMonth.UpClick
    '    SetNewDate(1)
    '    txtMonth.Text = MonthName(Month(CDate(lblNewDate.Text))) & ", " & Year(CDate(lblNewDate.Text))
    '    Call PrintStatus(False)
    'End Sub
    Private Sub frmParamFollowupReg_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
    Private Sub frmParamFollowupReg_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub optShow_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptShow.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optShow.GetIndex(eventSender)
            Call FillGridHeader()
        End If
    End Sub

    Private Sub SprdMain_DataColConfig(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DataColConfigEvent) Handles SprdMain.DataColConfig
        SprdMain.Row = -1
        SprdMain.Col = eventArgs.col
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        SprdMain.TypeEditLen = 1000
    End Sub


    Private Sub txtCategory_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCategory.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtCategory_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCategory.DoubleClick
        SearchCategory()
    End Sub
    Private Sub txtCategory_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCategory.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCategory.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCategory_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCategory.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchCategory()
    End Sub
    Private Sub txtCategory_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCategory.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If txtCategory.Text = "" Then GoTo EventExitSub

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'"

        If MainClass.ValidateWithMasterTable((txtCategory.Text), "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtCategory.Text = UCase(Trim(txtCategory.Text))
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
    Private Sub SearchCategory()
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'"

        If MainClass.SearchGridMaster((txtCategory.Text), "INV_GENERAL_MST", "GEN_DESC", "GEN_CODE", , , SqlStr) = True Then
            If AcName <> "" Then
                txtCategory.Text = AcName
            End If
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdSearchCategory_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchCategory.Click
        SearchCategory()
    End Sub

    Private Sub chkAllCategory_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllCategory.CheckStateChanged
        Call PrintStatus(False)
        If chkAllCategory.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtCategory.Enabled = False
            cmdsearchCategory.Enabled = False
        Else
            txtCategory.Enabled = True
            cmdsearchCategory.Enabled = True
        End If
    End Sub

    Private Sub chkAllSubCat_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAllSubCat.CheckStateChanged
        Call PrintStatus(False)
        If chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtSubCategory.Enabled = False
            cmdSubCatsearch.Enabled = False
        Else
            txtSubCategory.Enabled = True
            cmdSubCatsearch.Enabled = True
        End If
    End Sub
    Private Sub SearchSubCategory()
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mCatCode As String = ""

        If txtCategory.Text = "" Then
            MsgInformation("Please Select category .")
            txtCategory.Focus()
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable((txtCategory.Text), "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            mCatCode = MasterNo
        End If
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCatCode & "'"


        If MainClass.SearchGridMaster((txtSubCategory.Text), "INV_SUBCATEGORY_MST", "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", , , SqlStr) = True Then
            If AcName <> "" Then
                txtSubCategory.Text = AcName
            End If
        End If

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub txtSubCategory_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSubCategory.TextChanged
        Call PrintStatus(False)
    End Sub

    Private Sub txtSubCategory_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSubCategory.DoubleClick
        SearchSubCategory()
    End Sub


    Private Sub txtSubCategory_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSubCategory.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSubCategory.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSubCategory_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSubCategory.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchSubCategory()
    End Sub

    Private Sub txtSubCategory_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSubCategory.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mCatCode As String = ""

        If txtSubCategory.Text = "" Then GoTo EventExitSub


        If txtCategory.Text = "" Then
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        Else
            If MainClass.ValidateWithMasterTable((txtCategory.Text), "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , SqlStr) = True Then
                mCatCode = MasterNo
            End If
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCatCode & "'"
        End If

        If MainClass.ValidateWithMasterTable((txtSubCategory.Text), "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgInformation("No Such Sub Category ")
            Cancel = True
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cmdSubCatsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSubCatsearch.Click
        SearchSubCategory()
    End Sub


    Private Sub txtItemName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtItemName.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtItemName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtItemName.DoubleClick
        SearchItem()
    End Sub
    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub SearchItem()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        Dim mAccountCode As String

        SqlStr = " SELECT DISTINCT ITEMMST.ITEM_SHORT_DESC, ID.ITEM_CODE,  ITEMMST.CUSTOMER_PART_NO "

        SqlStr = SqlStr & vbCrLf _
            & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID, INV_ITEM_MST ITEMMST"

        ''WHERE CLAUSE...
        SqlStr = SqlStr & vbCrLf _
            & " WHERE IH.MKEY=ID.MKEY" & vbCrLf _
            & " And ID.COMPANY_CODE=ITEMMST.COMPANY_CODE" & vbCrLf _
            & " And ID.ITEM_CODE=ITEMMST.ITEM_CODE"



        If chkAllSupp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mAccountCode = MasterNo
                SqlStr = SqlStr & vbCrLf & "AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mAccountCode) & "'"
            End If
        End If

        MainClass.SearchGridMasterBySQL2(TxtItemName.Text, SqlStr)
        If AcName <> "" Then
            TxtItemName.Text = AcName
        End If

        'SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        'MainClass.SearchGridMaster(TxtItemName.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr)
        'If AcName <> "" Then
        '    txtItemName.Text = AcName
        'End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub SearchSupplier()

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"
        MainClass.SearchGridMaster(txtSupplier.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr)
        If AcName <> "" Then
            txtSupplier.Text = AcName
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub txtItemName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtItemName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtItemName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtItemName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtItemName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchItem()
    End Sub
    Private Sub TxtItemName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtItemName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If txtItemName.Text = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.ValidateWithMasterTable((txtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = True Then
            txtItemName.Text = UCase(Trim(txtItemName.Text))
        Else
            MsgInformation("No Such Item in Item Master")
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
        'Dim mMonthDays As Integer

        With SprdMain
            If optShow(0).Checked = True Then
                ColMaxCol = ColStockQty + 32 + 1
            Else
                ColMaxCol = ColStockQty + 6 + 1
            End If

            .MaxCols = ColMaxCol
            .set_RowHeight(0, RowHeight)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight * 0.75)
            .Row = -1

            .Col = ColSupplierCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColSupplierCode, 6)

            .Col = ColSupplierName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColSupplierName, 15)

            .Col = ColDSNo
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 0
            .TypeFloatMin = CDbl("-99999999999")
            .TypeFloatMax = CDbl("99999999999")
            .TypeFloatMoney = False
            .TypeFloatSeparator = False
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatSepChar = Asc(",")
            .set_ColWidth(ColDSNo, 8)
            .ColHidden = True

            .Col = ColAmendNo
            .CellType = SS_CELL_TYPE_INTEGER
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColAmendNo, 8)
            .ColHidden = True

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemCode, 8)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColItemDesc, 15)

            .Col = ColType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColType, 4)
            .ColsFrozen = ColType

            For cntCol = ColStockQty To ColMaxCol
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 0
                .TypeFloatMin = CDbl("-99999999999")
                .TypeFloatMax = CDbl("99999999999")
                .TypeFloatMoney = False
                .TypeFloatSeparator = False
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatSepChar = Asc(",")
                .set_ColWidth(cntCol, 8)
            Next

            Call FillGridHeader()

            .Col = ColSupplierCode
            .ColMerge = FPSpreadADO.MergeConstants.MergeAlways
            .Col = ColSupplierName
            .ColMerge = FPSpreadADO.MergeConstants.MergeAlways
            .Col = ColItemDesc
            .ColMerge = FPSpreadADO.MergeConstants.MergeAlways
            .Col = ColItemCode
            .ColMerge = FPSpreadADO.MergeConstants.MergeAlways


            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, .MaxCols)
            SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal

            SprdMain.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
            SprdMain.DAutoCellTypes = True
            SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
            SprdMain.GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Function Show1() As Boolean

        On Error GoTo LedgError
        Dim SqlStr As String = ""


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
        Dim mDept As String
        Dim mSupplier As String
        Dim mItemCode As String
        Dim mCatCode As String = ""
        Dim mSubCatCode As String

        ''SELECT CLAUSE...
        MakeSQL = " SELECT " & vbCrLf & " IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, " & vbCrLf & " TO_CHAR(IH.AUTO_KEY_DELV), TO_CHAR(IH.DELV_AMEND_NO), " & vbCrLf & " TRIM(ID.ITEM_CODE), INVMST.ITEM_SHORT_DESC, 'P' AS PLAN, "

        MakeSQL = MakeSQL & vbCrLf & " '0',"

        If optShow(0).Checked = True Then
            MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='01' THEN PLANNED_QTY ELSE 0 END)) AS DAY1," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='02' THEN PLANNED_QTY ELSE 0 END)) AS DAY2," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='03' THEN PLANNED_QTY ELSE 0 END)) AS DAY3," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='04' THEN PLANNED_QTY ELSE 0 END)) AS DAY4," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='05' THEN PLANNED_QTY ELSE 0 END)) AS DAY5," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='06' THEN PLANNED_QTY ELSE 0 END)) AS DAY6," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='07' THEN PLANNED_QTY ELSE 0 END)) AS DAY7,"

            MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='08' THEN PLANNED_QTY ELSE 0 END)) AS DAY8," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='09' THEN PLANNED_QTY ELSE 0 END)) AS DAY9," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='10' THEN PLANNED_QTY ELSE 0 END)) AS DAY10," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='11' THEN PLANNED_QTY ELSE 0 END)) AS DAY11," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='12' THEN PLANNED_QTY ELSE 0 END)) AS DAY12," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='13' THEN PLANNED_QTY ELSE 0 END)) AS DAY13," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='14' THEN PLANNED_QTY ELSE 0 END)) AS DAY14,"

            MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='15' THEN PLANNED_QTY ELSE 0 END)) AS DAY15," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='16' THEN PLANNED_QTY ELSE 0 END)) AS DAY16," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='17' THEN PLANNED_QTY ELSE 0 END)) AS DAY17," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='18' THEN PLANNED_QTY ELSE 0 END)) AS DAY18," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='19' THEN PLANNED_QTY ELSE 0 END)) AS DAY19," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='20' THEN PLANNED_QTY ELSE 0 END)) AS DAY20," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='21' THEN PLANNED_QTY ELSE 0 END)) AS DAY21,"

            MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='22' THEN PLANNED_QTY ELSE 0 END)) AS DAY22," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='23' THEN PLANNED_QTY ELSE 0 END)) AS DAY23," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='24' THEN PLANNED_QTY ELSE 0 END)) AS DAY24," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='25' THEN PLANNED_QTY ELSE 0 END)) AS DAY25," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='26' THEN PLANNED_QTY ELSE 0 END)) AS DAY26," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='27' THEN PLANNED_QTY ELSE 0 END)) AS DAY27," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='28' THEN PLANNED_QTY ELSE 0 END)) AS DAY28,"

            MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='29' THEN PLANNED_QTY ELSE 0 END)) AS DAY29," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='30' THEN PLANNED_QTY ELSE 0 END)) AS DAY30," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD')='31' THEN PLANNED_QTY ELSE 0 END)) AS DAY31,"

        Else
            MakeSQL = MakeSQL & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD') <='07' THEN PLANNED_QTY ELSE 0 END)) AS Week1," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD') >'07' AND TO_CHAR(SERIAL_DATE,'DD') <='14' THEN PLANNED_QTY ELSE 0 END)) AS Week2," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD') >'14' AND TO_CHAR(SERIAL_DATE,'DD') <='21' THEN PLANNED_QTY ELSE 0 END)) AS Week3," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD') >'21' AND TO_CHAR(SERIAL_DATE,'DD') <='28' THEN PLANNED_QTY ELSE 0 END)) AS Week4," & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(SERIAL_DATE,'DD') >'28' THEN PLANNED_QTY ELSE 0 END)) AS Week5,"
        End If

        MakeSQL = MakeSQL & vbCrLf & "TO_CHAR(SUM(PLANNED_QTY)) AS PLANNED_QTY, '' AS SCHEDULE_PER"

        ''FROM CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " FROM PUR_DELV_SCHLD_HDR IH, PUR_DAILY_SCHLD_DET ID, " & vbCrLf & " FIN_SUPP_CUST_MST CMST, INV_ITEM_MST INVMST"

        ''WHERE CLAUSE...
        MakeSQL = MakeSQL & vbCrLf & " WHERE " & vbCrLf & " IH.AUTO_KEY_DELV=ID.AUTO_KEY_DELV" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE"

        MakeSQL = MakeSQL & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" ''& vbCrLf |            & " AND SUBSTR(IH.AUTO_KEY_DELV,LENGTH(IH.AUTO_KEY_DELV)-5,4)=" & RsCompany.fields("FYEAR").value & ""

        If chkAllCategory.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtCategory.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = True Then
                mCatCode = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND INVMST.CATEGORY_CODE='" & MainClass.AllowSingleQuote(mCatCode) & "'"
            End If
        End If

        If chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtSubCategory.Text, "SUBCATEGORY_DESC", "SUBCATEGORY_CODE", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & mCatCode & "'") = True Then
                mSubCatCode = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND INVMST.SUBCATEGORY_CODE='" & MainClass.AllowSingleQuote(mSubCatCode) & "'"
            End If
        End If

        If chkAllSupp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mSupplier = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSupplier) & "'"
            End If
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If MainClass.ValidateWithMasterTable(txtItemName.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                mItemCode = MasterNo
                MakeSQL = MakeSQL & vbCrLf & "AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"
            End If
        End If

        If CboItemType.SelectedIndex >= 1 Then
            MakeSQL = MakeSQL & vbCrLf & "AND INVMST.ITEM_TYPE='" & VB.Left(CboItemType.Text, 1) & "'"
        End If

        If cboExportItem.SelectedIndex >= 1 Then
            MakeSQL = MakeSQL & vbCrLf & "AND INVMST.IS_EXPORT_ITEM='" & VB.Left(cboExportItem.Text, 1) & "'"
        End If

        MakeSQL = MakeSQL & vbCrLf & " AND TO_CHAR(IH.SCHLD_DATE,'MON-YYYY')='" & UCase(VB6.Format(lblNewDate.Text, "MMM-YYYY")) & "'"

        ''GROUP BY CLAUSE...

        MakeSQL = MakeSQL & vbCrLf & "GROUP BY " & vbCrLf & " IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, " & vbCrLf & " TO_CHAR(IH.AUTO_KEY_DELV), TO_CHAR(IH.DELV_AMEND_NO), " & vbCrLf & " TRIM(ID.ITEM_CODE), INVMST.ITEM_SHORT_DESC"

        ''ORDER CLAUSE...

        MakeSQL = MakeSQL & vbCrLf & "ORDER BY " & vbCrLf & " IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, " & vbCrLf & " TO_CHAR(IH.AUTO_KEY_DELV), TO_CHAR(IH.DELV_AMEND_NO), " & vbCrLf & " TRIM(ID.ITEM_CODE), INVMST.ITEM_SHORT_DESC"

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        If MainClass.ChkIsdateF(txtDateTo) = False Then Exit Function
        If FYChk(CStr(CDate(txtDateTo.Text))) = False Then txtDateTo.Focus()
        If chkAllSupp.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtSupplier.Text) = "" Then
                MsgInformation("Invaild Supplier Name")
                txtSupplier.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((txtSupplier.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invaild Supplier Name")
                txtSupplier.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If Trim(txtItemName.Text) = "" Then
                MsgInformation("Invaild Item Name")
                txtItemName.Focus()
                FieldsVerification = False
                Exit Function
            End If
            If MainClass.ValidateWithMasterTable((txtItemName.Text), "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgInformation("Invaild Item Name")
                txtItemName.Focus()
                FieldsVerification = False
                Exit Function
            End If
        End If
        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function

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
    Private Sub FillPOCombo()
        On Error GoTo FillErr2
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing


        CboItemType.Items.Clear()
        CboItemType.Items.Add("All")
        CboItemType.Items.Add("Local")
        CboItemType.Items.Add("Imported")
        CboItemType.SelectedIndex = 0

        cboExportItem.Items.Clear()
        cboExportItem.Items.Add("All")
        cboExportItem.Items.Add("Yes")
        cboExportItem.Items.Add("No")
        cboExportItem.SelectedIndex = 0

        Exit Sub
FillErr2:
        MsgBox(Err.Description)
    End Sub

    Private Sub txtSupplier_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplier.TextChanged
        Call PrintStatus(False)
    End Sub
    Private Sub txtSupplier_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplier.DoubleClick
        SearchSupplier()
    End Sub
    Private Sub txtSupplier_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSupplier.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSupplier.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSupplier_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSupplier.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchSupplier()
    End Sub

    Private Sub FillGridHeader()
        On Error GoTo ErrPart
        Dim cntCol As Integer
        Dim I As Integer

        With SprdMain
            I = 1
            For cntCol = ColStockQty + 1 To ColMaxCol - 2
                .Row = 0
                .Col = cntCol
                .Text = IIf(optShow(0).Checked = True, "", "Week") & VB6.Format(I, "00")
                I = I + 1
            Next

            .Row = 0
            .Col = ColMaxCol - 1
            .Text = "Total"

            .Row = 0
            .Col = ColMaxCol
            .Text = "Delivery %"
        End With

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub InsertRecdQty()
        On Error GoTo ErrPart
        Dim cntCol As Integer
        Dim I As Integer
        Dim mType As String
        Dim mPartyCode As String
        Dim mPartyName As String
        Dim mItemCode As String
        Dim mItemName As String
        Dim pDateSeries As Integer
        Dim mRecdQty As Double
        Dim mTotRecdQty As Double
        Dim mStockQty As Double
        Dim mItemUOM As String = ""
        Dim mScheduleQty As Double

        If optShow(0).Checked = True Then
            ColMaxCol = ColStockQty + 32 + 1
        Else
            ColMaxCol = ColStockQty + 6 + 1
        End If

        With SprdMain
            cntCol = 1
            While cntCol <= .DataRowCnt
                .Row = cntCol

                .Col = ColType
                mType = Trim(.Text)

                .Col = ColSupplierCode
                mPartyCode = Trim(.Text)

                .Col = ColSupplierName
                mPartyName = Trim(.Text)

                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColItemDesc
                mItemName = Trim(.Text)

                If MainClass.ValidateWithMasterTable(Trim(mItemCode), "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mItemUOM = MasterNo
                End If

                If mType = "P" Then
                    mStockQty = GetBalanceStockQty(mItemCode, (txtDateTo.Text), mItemUOM, "STR", "ST", "", ConWH, -1)
                    mStockQty = mStockQty + GetBalanceStockQty(mItemCode, (txtDateTo.Text), mItemUOM, "STR", "QC", "", ConWH, -1)
                Else
                    mStockQty = 0
                End If

                .Col = ColStockQty
                .Text = VB6.Format(mStockQty, "0.00")

                .Col = ColMaxCol - 1
                mScheduleQty = CDbl(VB6.Format(.Text, "0.00"))

                If mType = "P" Then
                    .Row = cntCol + 1
                    .MaxRows = .MaxRows + 1
                    .Action = SS_ACTION_INSERT_ROW

                    .Col = ColSupplierCode
                    .Text = mPartyCode

                    .Col = ColSupplierName
                    .Text = mPartyName

                    .Col = ColItemCode
                    .Text = mItemCode

                    .Col = ColItemDesc
                    .Text = mItemName

                    .Col = ColType
                    .Text = "D"

                    If FillRecdQty(cntCol + 1, mPartyCode, mItemCode, mScheduleQty) = False Then GoTo ErrPart
                    '                pDateSeries = 1
                    '                mTotRecdQty = 0
                    '                For I = ColStockQty + 1 To ColMaxCol - 1
                    '                    .Col = I
                    '                    mRecdQty = GetRecdQty(mPartyCode, mItemCode, VB6.Format(pDateSeries, "00"))
                    '                    mTotRecdQty = mTotRecdQty + mRecdQty
                    '                    .Text = VB6.Format(mRecdQty, "0.00")
                    '                    pDateSeries = pDateSeries + 1
                    '                Next
                    '
                    '                .Col = ColMaxCol
                    '                .Text = VB6.Format(mTotRecdQty, "0.00")


                End If
                cntCol = cntCol + 1
                .Row = .Row + 1
            End While
        End With

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function GetRecdQty(ByRef pSupplierCode As String, ByRef pItemCode As String, ByRef pDateSerial As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsMRR As ADODB.Recordset = Nothing
        Dim mTotQty As Double

        SqlStr = ""
        mTotQty = 0

        SqlStr = "SELECT "

        If optShow(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.MRR_DATE,'DD')='" & pDateSerial & "' THEN RECEIVED_QTY ELSE 0 END)) AS TOTQTY"
        Else
            If pDateSerial = "01" Then
                SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.MRR_DATE,'DD') <='07' THEN RECEIVED_QTY ELSE 0 END)) AS TOTQTY"
            ElseIf pDateSerial = "02" Then
                SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.MRR_DATE,'DD') >'07' AND TO_CHAR(IH.MRR_DATE,'DD') <='14' THEN RECEIVED_QTY ELSE 0 END)) AS TOTQTY"
            ElseIf pDateSerial = "03" Then
                SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.MRR_DATE,'DD') >'14' AND TO_CHAR(IH.MRR_DATE,'DD') <='21' THEN RECEIVED_QTY ELSE 0 END)) AS TOTQTY"
            ElseIf pDateSerial = "04" Then
                SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.MRR_DATE,'DD') >'21' AND TO_CHAR(IH.MRR_DATE,'DD') <='28' THEN RECEIVED_QTY ELSE 0 END)) AS TOTQTY"
            ElseIf pDateSerial = "05" Then
                SqlStr = SqlStr & vbCrLf & " TO_CHAR(SUM(CASE WHEN TO_CHAR(IH.MRR_DATE,'DD') >'28' THEN RECEIVED_QTY ELSE 0 END)) AS TOTQTY"
            End If
        End If

        SqlStr = SqlStr & vbCrLf & " FROM INV_GATE_HDR IH,INV_GATE_DET ID" & vbCrLf & " WHERE IH.AUTO_KEY_MRR=ID.AUTO_KEY_MRR " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pSupplierCode) & "'" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"


        SqlStr = SqlStr & vbCrLf & " AND IH.REF_TYPE ='P' "

        SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(IH.MRR_DATE,'MON-YYYY')='" & UCase(VB6.Format(lblNewDate.Text, "MMM-YYYY")) & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMRR, ADODB.LockTypeEnum.adLockReadOnly)
        If Not RsMRR.EOF Then
            mTotQty = Val(IIf(IsDbNull(RsMRR.Fields("TOTQTY").Value), 0, RsMRR.Fields("TOTQTY").Value))
        End If
        GetRecdQty = mTotQty
        Exit Function
ErrPart:
        'Resume
        GetRecdQty = 0
        MsgBox(Err.Description)
    End Function

    Private Function FillRecdQty(ByRef pRow As Integer, ByRef pSupplierCode As String, ByRef pItemCode As String, ByRef mScheduleQty As Double) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsMRR As ADODB.Recordset = Nothing
        Dim mMRRDATE As String
        Dim mDate As Integer
        Dim mTotQty(31) As Double
        Dim I As Integer
        Dim mTotalQty As Double
        Dim mSchedulePer As Double

        SqlStr = ""
        For mDate = 1 To 31
            mTotQty(mDate) = 0
        Next
        I = I
        For I = ColStockQty + 1 To ColMaxCol - 2
            SprdMain.Row = pRow
            SprdMain.Col = I
            SprdMain.Text = VB6.Format(0, "0.00")
        Next

        mTotalQty = 0

        If optMRR.Checked Then

            SqlStr = "SELECT IH.MRR_DATE, SUM(RECEIVED_QTY) AS TOTQTY"

            SqlStr = SqlStr & vbCrLf & " FROM INV_GATE_HDR IH,INV_GATE_DET ID" & vbCrLf & " WHERE IH.AUTO_KEY_MRR=ID.AUTO_KEY_MRR " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pSupplierCode) & "'" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

            SqlStr = SqlStr & vbCrLf & " AND IH.REF_TYPE ='P' "

            SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(IH.MRR_DATE,'MON-YYYY')='" & UCase(VB6.Format(lblNewDate.Text, "MMM-YYYY")) & "'"

            SqlStr = SqlStr & vbCrLf & " GROUP BY IH.MRR_DATE "
            SqlStr = SqlStr & vbCrLf & " ORDER BY IH.MRR_DATE "
        Else
            SqlStr = "SELECT IH.GATE_DATE AS MRR_DATE, SUM(BILL_QTY) AS TOTQTY"

            SqlStr = SqlStr & vbCrLf & " FROM INV_GATEENTRY_HDR IH,INV_GATEENTRY_DET ID" & vbCrLf & " WHERE IH.AUTO_KEY_GATE=ID.AUTO_KEY_GATE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pSupplierCode) & "'" & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

            SqlStr = SqlStr & vbCrLf & " AND IH.REF_TYPE ='P' "

            SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(IH.GATE_DATE,'MON-YYYY')='" & UCase(VB6.Format(lblNewDate.Text, "MMM-YYYY")) & "'"

            SqlStr = SqlStr & vbCrLf & " GROUP BY IH.GATE_DATE "
            SqlStr = SqlStr & vbCrLf & " ORDER BY IH.GATE_DATE "
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMRR, ADODB.LockTypeEnum.adLockReadOnly)
        If Not RsMRR.EOF Then
            Do While Not RsMRR.EOF
                mMRRDATE = IIf(IsDbNull(RsMRR.Fields("MRR_DATE").Value), "", RsMRR.Fields("MRR_DATE").Value)
                If optShow(0).Checked = True Then
                    mDate = VB.Day(CDate(mMRRDATE))
                Else
                    If VB.Day(CDate(mMRRDATE)) <= 7 Then
                        mDate = 1
                    ElseIf VB.Day(CDate(mMRRDATE)) > 7 And VB.Day(CDate(mMRRDATE)) <= 14 Then
                        mDate = 2
                    ElseIf VB.Day(CDate(mMRRDATE)) > 14 And VB.Day(CDate(mMRRDATE)) <= 21 Then
                        mDate = 3
                    ElseIf VB.Day(CDate(mMRRDATE)) > 21 And VB.Day(CDate(mMRRDATE)) <= 28 Then
                        mDate = 4
                    ElseIf VB.Day(CDate(mMRRDATE)) > 28 Then
                        mDate = 5
                    End If
                End If
                mTotQty(mDate) = mTotQty(mDate) + Val(IIf(IsDbNull(RsMRR.Fields("TOTQTY").Value), 0, RsMRR.Fields("TOTQTY").Value))
                RsMRR.MoveNext()
            Loop
            mDate = 1

            For I = ColStockQty + 1 To ColMaxCol - 2
                SprdMain.Row = pRow
                SprdMain.Col = I
                SprdMain.Text = VB6.Format(mTotQty(mDate), "0.00")
                mTotalQty = mTotalQty + mTotQty(mDate)
                mDate = mDate + 1
            Next
        End If

        SprdMain.Row = pRow
        SprdMain.Col = ColMaxCol - 1
        SprdMain.Text = VB6.Format(mTotalQty, "0.00")

        If mScheduleQty > 0 Then
            mSchedulePer = System.Math.Round(mTotalQty * 100 / mScheduleQty, 0)
        Else
            mSchedulePer = 0
        End If

        SprdMain.Col = ColMaxCol
        SprdMain.Text = VB6.Format(mSchedulePer, "0.00")



        FillRecdQty = True
        Exit Function
ErrPart:
        'Resume
        FillRecdQty = False
        MsgBox(Err.Description)
    End Function

    Private Sub txtMonth_Click(sender As Object, e As EventArgs) Handles txtMonth.Click
        lblNewDate.Text = txtMonth.Text
    End Sub

    Private Sub txtMonth_ValueChanged(sender As Object, e As EventArgs) Handles txtMonth.ValueChanged
        lblNewDate.Text = txtMonth.Text
    End Sub
End Class
