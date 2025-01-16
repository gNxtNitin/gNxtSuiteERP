Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinDataSource
'Imports Infragistics.Win.UltraWinTabControl
Imports System.Data.OleDb

Friend Class frmOrderPosting
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection
    Dim mAccountCode As String
    Private Const RowHeight As Short = 15

    Private Const ColMKEY As Short = 1
    Private Const ColPONo As Short = 2
    Private Const ColWEF As Short = 3
    Private Const ColPOAmendNo As Short = 4
    Private Const ColPurType As Short = 5
    Private Const ColOrderType As Short = 6
    Private Const ColPartyName As Short = 7
    Private Const ColItemValue As Short = 8
    Private Const ColPostStatus As Short = 9

    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles ChkALL.CheckStateChanged
        cmdShow.Enabled = True
        TxtAccount.Enabled = IIf(chkAll.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
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
        Dim mPONo As Double
        Dim mWef As String
        Dim mSupplier As String
        Dim mUpdateCount As Integer
        Dim mPOAmendNo As Integer
        Dim mCanPostPO As Boolean
        Dim mPOSeq As Double
        Dim mAuthorisation As String
        Dim mRow As UltraGridRow
        Dim mMaxRow As Long
        Dim mFlag As String
        mAuthorisation = IIf(InStr(1, XRIGHT, "S") > 0, "Y", "N")
        If mAuthorisation = "N" Then
            MsgBox("You have no Authorisation Right to Post PO. ", MsgBoxStyle.Critical)
            Exit Sub
        End If

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        mUpdateCount = 0




        mMaxRow = UltraGrid1.Rows.Count


        For CntRow = 0 To mMaxRow - 1
            mRow = Me.UltraGrid1.Rows(CntRow)



            mCanPostPO = False

            mMKEY = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColMKEY - 1))
            mPONo = Val(mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColPONo - 1)))
            mPOSeq = CDbl(Mid(CStr(mPONo), 1, Len(Str(mPONo)) - 6))
            mWef = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColWEF - 1))
            mSupplier = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColPartyName - 1))
            mPOAmendNo = Val(mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColPOAmendNo - 1)))

            mFlag = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColPostStatus - 1))
            If UCase(mFlag) = "TRUE" Then
                If mWef = "" Then
                    MsgInformation("WEF Date is Blank, So cann't be saved.")
                    GoTo ErrPart
                End If

                ''                    & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _

                SqlStr = "UPDATE PUR_PURCHASE_HDR SET PO_CLOSED='Y', UPDATE_FROM='N'" & vbCrLf _
                    & " WHERE AUTO_KEY_PO=" & mPONo & "" & vbCrLf _
                    & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

                PubDBCn.Execute(SqlStr)
                ''open Only Current PO
                SqlStr = "UPDATE PUR_PURCHASE_HDR SET PO_STATUS='Y',PO_CLOSED='N', UPDATE_FROM='N'," & vbCrLf _
                    & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                    & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
                    & " WHERE MKEY=" & mMKEY & "" & vbCrLf _
                    & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

                PubDBCn.Execute(SqlStr)
                ''Not Required..
                'If mPOAmendNo <> 0 Then
                '    If Update1(mMKEY, mPONo, mWef, mSupplier, mPOAmendNo) = False Then GoTo ErrPart
                'End If
                mUpdateCount = mUpdateCount + 1
                '                End If
            End If
        Next

        PubDBCn.CommitTrans()
        MsgBox("Total " & mUpdateCount & " PO Posted.", MsgBoxStyle.Information)
        CmdSave.Enabled = False
        cmdShow.Enabled = True
        Exit Sub
ErrPart:
        '    Resume
        If Err.Description <> "" Then
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        PubDBCn.RollbackTrans()
    End Sub

    Public Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        If FieldsVerification = False Then Exit Sub
        'MainClass.ClearGrid(SprdMain, RowHeight)
        OptSelection(1).Checked = True
        Show1("S")
        cmdShow.Enabled = False
        CmdSave.Enabled = True

        cmdShow.Enabled = False
    End Sub
    Function FieldsVerification() As Boolean
        On Error GoTo ERR1
        '    If MainClass.ChkIsdateF(txtDateFrom) = False Then txtDateFrom.SetFocus: Exit Function
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then txtDateFrom.SetFocus

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
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
    Public Sub frmOrderPosting_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Purchase Order Posting" ''(" & IIf(lblIsCapital.Text = "Y", "Capital Order", "Other Than Capital Order") & ")"
        '    blIsCapital.text
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmOrderPosting_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
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


        TxtAccount.Enabled = False
        chkAll.CheckState = System.Windows.Forms.CheckState.Checked

        Call Show1("L")
        cmdShow.Enabled = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub Show1(pShowType As String)

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mSuppCustCode As String

        If OptOrderType(0).Checked = True Then
            SqlStr = "SELECT POMain.MKEY, POMain.AUTO_KEY_PO, TO_CHAR(POMAIN.AMEND_WEF_DATE,'DD/MM/YYYY') AS AMEND_WEF_DATE ,AMEND_NO,  " & vbCrLf _
                & " CASE WHEN POMain.PUR_TYPE='P' THEN 'Purchase Order'  " & vbCrLf _
                & " WHEN POMain.PUR_TYPE='W' THEN 'Work Order' " & vbCrLf _
                & " WHEN POMain.PUR_TYPE='J' THEN 'Job Work Order' WHEN POMain.PUR_TYPE='R' THEN 'Project Order' END AS Purchase_Type, " & vbCrLf _
                & " CASE WHEN POMain.ORDER_TYPE='O' THEN 'Open'  " & vbCrLf _
                & " WHEN POMain.ORDER_TYPE='C' THEN 'Close' END AS Order_Type, " & vbCrLf _
                & " ACM.SUPP_CUST_NAME,To_CHAR(SUM(GROSS_AMT)) AS ITEM_VALUE,'False' " & vbCrLf _
                & " FROM PUR_PURCHASE_HDR POMain, PUR_PURCHASE_DET POD, FIN_SUPP_CUST_MST ACM, INV_ITEM_MST IMST, INV_GENERAL_MST GMST" & vbCrLf _
                & " WHERE " & vbCrLf _
                & " POMain.MKEY=POD.MKEY " & vbCrLf _
                & " AND POMain.SUPP_CUST_CODE=ACM.SUPP_CUST_CODE " & vbCrLf _
                & " AND POMain.Company_Code=ACM.Company_Code " & vbCrLf _
                & " AND POD.ITEM_CODE=IMST.ITEM_CODE " & vbCrLf _
                & " AND POMain.Company_Code=IMST.Company_Code" & vbCrLf _
                & " AND IMST.CATEGORY_CODE=GMST.GEN_CODE" & vbCrLf _
                & " AND IMST.Company_Code=GMST.Company_Code AND GMST.GEN_TYPE='C'" & vbCrLf _
                & " AND POMain.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        Else
            SqlStr = "SELECT POMain.MKEY, POMain.AUTO_KEY_PO, TO_CHAR(POMAIN.AMEND_WEF_DATE,'DD/MM/YYYY') AS AMEND_WEF_DATE ,AMEND_NO,  " & vbCrLf _
                & " CASE WHEN POMain.PUR_TYPE='P' THEN 'Purchase Order'  " & vbCrLf _
                & " WHEN POMain.PUR_TYPE='W' THEN 'Work Order' " & vbCrLf _
                & " WHEN POMain.PUR_TYPE='J' THEN 'Job Work Order' WHEN POMain.PUR_TYPE='R' THEN 'Project Order' END AS Purchase_Type, " & vbCrLf _
                & " CASE WHEN POMain.ORDER_TYPE='O' THEN 'Open'  " & vbCrLf _
                & " WHEN POMain.ORDER_TYPE='C' THEN 'Close' END AS Order_Type, " & vbCrLf _
                & " ACM.SUPP_CUST_NAME,To_CHAR(SUM(GROSS_AMT)) AS ITEM_VALUE,'False' " & vbCrLf _
                & " FROM PUR_PURCHASE_HDR POMain, PUR_PURCHASE_DET POD, FIN_SUPP_CUST_MST ACM" & vbCrLf _
                & " WHERE " & vbCrLf _
                & " POMain.MKEY=POD.MKEY " & vbCrLf _
                & " AND POMain.SUPP_CUST_CODE=ACM.SUPP_CUST_CODE " & vbCrLf _
                & " AND POMain.Company_Code=ACM.Company_Code " & vbCrLf _
                & " AND POMain.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        End If

        If RsCompany.Fields("FYEAR").Value < ConOPENPO_CONTINOUS_YEAR Then
            SqlStr = SqlStr & vbCrLf & " AND SUBSTR(AUTO_KEY_PO,LENGTH(AUTO_KEY_PO)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
        Else
            SqlStr = SqlStr & vbCrLf & " AND SUBSTR(AUTO_KEY_PO,LENGTH(AUTO_KEY_PO)-5,4)>=" & ConOPENPO_CONTINOUS_YEAR & ""
        End If

        If OptOrderType(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND POMain.PUR_TYPE IN ('P')"
        ElseIf OptOrderType(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND POMain.PUR_TYPE ='J'"
        ElseIf OptOrderType(2).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND POMain.PUR_TYPE ='R'"   ''L'
        ElseIf OptOrderType(3).Checked = True Then
            SqlStr = SqlStr & vbCrLf & " AND POMain.PUR_TYPE ='W'"
        End If


        SqlStr = SqlStr & vbCrLf & " AND POMain.PO_STATUS='N' AND POMain.PO_CLOSED='N'"


        'SqlStr = SqlStr & vbCrLf & " AND POMain.ISCAPITAL ='" & lblIsCapital.Text & "'"

        If RsCompany.Fields("PO_LOCK").Value = "N" Then
            If RsCompany.Fields("PO_PRINT_APP_REQ").Value = "Y" Then
                If OptOrderType(0).Checked = True Then
                    SqlStr = SqlStr & vbCrLf & " AND CASE WHEN POMain.PUR_TYPE='P' THEN  (CASE WHEN PRD_TYPE IN ('P','R','B','I','D','3') THEN POMain.PO_PRINT_APP ELSE 'Y' END) ELSE 'Y' END ='Y'"
                End If
            End If
        End If

        If ChkALL.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(TxtAccount.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " And ACM.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(UCase(TxtAccount.Text)) & "'"
        End If

        If pShowType = "L" Then
            SqlStr = SqlStr & vbCrLf & "AND 1=2"
        End If


        SqlStr = SqlStr & vbCrLf _
            & " GROUP BY " & vbCrLf _
            & " POMain.MKEY, POMain.AUTO_KEY_PO, POMAIN.AMEND_WEF_DATE, AMEND_NO,  " & vbCrLf _
            & " POMain.PUR_TYPE,  POMain.ORDER_TYPE, ACM.SUPP_CUST_NAME "

        SqlStr = SqlStr & vbCrLf _
            & " ORDER BY SUBSTR(AUTO_KEY_PO,LENGTH(AUTO_KEY_PO)-5,4), POMain.PUR_TYPE,POMain.ORDER_TYPE,POMAIN.AMEND_WEF_DATE,POMain.AUTO_KEY_PO"

        FillUltraGrid(SqlStr)

        'MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmOrderPosting_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
    End Sub

    Private Sub optOrderType_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptOrderType.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optOrderType.GetIndex(eventSender)
            cmdShow.Enabled = True
        End If
    End Sub

    Private Sub OptSelection_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptSelection.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptSelection.GetIndex(eventSender)
            Dim ultRow As UltraGridRow
            Dim mMaxRow As Long
            mMaxRow = UltraGrid1.Rows.Count


            For CntRow = 0 To mMaxRow - 1
                ultRow = Me.UltraGrid1.Rows(CntRow)
                ultRow.Cells(ColPostStatus - 1).Value = IIf(Index = 0, True, False)
            Next

        End If
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

    Private Sub UltraGrid1_InitializeLayout(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs) Handles UltraGrid1.InitializeLayout

        Try
            e.Layout.Override.FilterUIType = FilterUIType.FilterRow

            ' FilterEvaluationTrigger specifies when UltraGrid applies the filter criteria typed 
            ' into a filter row. Default is OnCellValueChange which will cause the UltraGrid to
            ' re-filter the data as soon as the user modifies the value of a filter cell.
            e.Layout.Override.FilterEvaluationTrigger = FilterEvaluationTrigger.OnCellValueChange

            ' By default the UltraGrid selects the type of the filter operand editor based on
            ' the column's DataType. For DateTime and boolean columns it uses the column's editors.
            ' For other column types it uses the Combo. You can explicitly specify the operand
            ' editor style by setting the FilterOperandStyle on the override or the individual
            ' columns.
            'e.Layout.Override.FilterOperandStyle = FilterOperandStyle.Combo;

            ' By default UltraGrid displays user interface for selecting the filter operator. 
            ' You can set the FilterOperatorLocation to hide this user interface. This
            ' property is available on column as well so it can be controlled on a per column
            ' basis. Default is WithOperand. This property is exposed off the column as well.
            e.Layout.Override.FilterOperatorLocation = FilterOperatorLocation.WithOperand

            ' By default the UltraGrid uses StartsWith as the filter operator. You use
            ' the FilterOperatorDefaultValue property to specify a different filter operator
            ' to use. This is the default or the initial filter operator value of the cells
            ' in filter row. If filter operator user interface is enabled (FilterOperatorLocation
            ' is not set to None) then that ui will be initialized to the value of this
            ' property. The user can then change the operator as he/she chooses via the operator
            ' drop down.
            e.Layout.Override.FilterOperatorDefaultValue = FilterOperatorDefaultValue.StartsWith

            ' FilterOperatorDropDownItems property can be used to control the options provided
            ' to the user for selecting the filter operator. By default UltraGrid bases 
            ' what operator options to provide on the column's data type. This property is
            ' avaibale on the column as well.
            'e.Layout.Override.FilterOperatorDropDownItems = FilterOperatorDropDownItems.All;

            ' By default UltraGrid displays a clear button in each cell of the filter row
            ' as well as in the row selector of the filter row. When the user clicks this
            ' button the associated filter criteria is cleared. You can use the 
            ' FilterClearButtonLocation property to control if and where the filter clear
            ' buttons are displayed.
            e.Layout.Override.FilterClearButtonLocation = FilterClearButtonLocation.RowAndCell

            ' Appearance of the filter row can be controlled using the FilterRowAppearance proeprty.
            e.Layout.Override.FilterRowAppearance.BackColor = Color.LightYellow

            ' You can use the FilterRowPrompt to display a prompt in the filter row. By default
            ' UltraGrid does not display any prompt in the filter row.
            e.Layout.Override.FilterRowPrompt = "Filter"

            ' You can use the FilterRowPromptAppearance to change the appearance of the prompt.
            ' By default the prompt is transparent and uses the same fore color as the filter row.
            ' You can make it non-transparent by setting the appearance' BackColorAlpha property 
            ' or by setting the BackColor to a desired value.
            e.Layout.Override.FilterRowPromptAppearance.BackColorAlpha = Alpha.Opaque

            ' By default the prompt is spread across multiple cells if it's bigger than the
            ' first cell. You can confine the prompt to a particular cell by setting the
            ' SpecialRowPromptField property off the band to the key of a column.
            'e.Layout.Bands[0].SpecialRowPromptField = e.Layout.Bands[0].Columns[0].Key;

            ' Display a separator between the filter row other rows. SpecialRowSeparator property 
            ' can be used to display separators between various 'special' rows, including for the
            ' filter row. This property is a flagged enum property so it can take multiple values.
            e.Layout.Override.SpecialRowSeparator = SpecialRowSeparator.FilterRow


            e.Layout.Override.RowSelectors = DefaultableBoolean.True

            ''To Stop the resizing of row
            e.Layout.Override.RowSizing = RowSizing.Fixed

            ''For Selecting a single row
            e.Layout.Override.SelectTypeRow = SelectType.Single

            ''To stop the resizzing of Column
            e.Layout.Override.AllowColSizing = AllowColSizing.Free


            ''To display row no on the row header
            e.Layout.Override.RowSelectorNumberStyle = RowSelectorNumberStyle.RowIndex

            e.Layout.Override.HeaderClickAction = HeaderClickAction.SortSingle


            e.Layout.GroupByBox.Prompt = ""

            e.Layout.Override.SelectTypeRow = SelectType.Extended
            e.Layout.Override.SelectTypeCol = SelectType.Extended
            '    e.Layout.Override.CellClickAction = CellClickAction.CellSelect
            e.Layout.Override.AllowMultiCellOperations = AllowMultiCellOperation.CopyWithHeaders


        Catch ex As Exception


        End Try

    End Sub
    Private Sub UltraGrid1_DoubleClick(sender As Object, e As EventArgs) Handles UltraGrid1.DoubleClick

        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing


        Dim xPoNo As Double
        Dim xAmendPONo As Double
        Dim xPOWEF As String

        Dim mRow As UltraGridRow

        If Me.UltraGrid1.ActiveRow.Index < 0 Then Exit Sub
        mRow = Me.UltraGrid1.Rows(Me.UltraGrid1.ActiveRow.Index)

        xPoNo = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColPONo - 1))
        If xPoNo <= 0 Then Exit Sub

        xAmendPONo = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColPOAmendNo - 1))
        xPOWEF = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColWEF - 1))


        xPOWEF = VB6.Format(xPOWEF, "DD/MM/YYYY")

        SqlStr = "SELECT * from PUR_PURCHASE_HDR WHERE AUTO_KEY_PO=" & xPoNo & " AND AMEND_NO=" & xAmendPONo & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


        If RsTemp.EOF = False Then
            frmPO_GST.MdiParent = Me.MdiParent
            frmPO_GST.Show()
            frmPO_GST.lblBookType.Text = RsTemp.Fields("PUR_TYPE").Value & RsTemp.Fields("ORDER_TYPE").Value

            frmPO_GST.frmPO_GST_Activated(Nothing, New System.EventArgs())

            frmPO_GST.txtPONo.Text = RsTemp.Fields("AUTO_KEY_PO").Value
            frmPO_GST.txtAmendNo.Text = RsTemp.Fields("AMEND_NO").Value

            frmPO_GST.txtAmendNo_Validating(Nothing, New System.ComponentModel.CancelEventArgs(False)) ''txtPONO_Validate False

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

    Private Function UpdateRateDiffVoucherForDN(ByRef mPOKey As Double, ByRef pPONO As Double, ByRef pWEF As String, ByRef pSupplierCode As String, ByRef pItemCode As String, ByRef pItemPriceAfterDisc As Double) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsPur As ADODB.Recordset = Nothing
        Dim pVMKEY As String
        Dim pPurFYEAR As Integer
        Dim pPURSuppCode As String
        Dim pPURVNo As String
        Dim pPURVDate As String
        Dim pPURBillNo As String
        Dim pPURBillDate As String
        Dim pPURMRRNo As String
        Dim pPURMRRDate As String
        Dim pPURBillQty As Double
        Dim pPURApprovedQty As Double
        Dim mPURBillRate As Double
        Dim pPURBookType As String
        Dim pPURBookSubType As String
        Dim mDIFF_RATE As Double

        UpdateRateDiffVoucherForDN = False


        SqlStr = " SELECT IH.MKEY, IH.COMPANY_CODE, IH.FYEAR, " & vbCrLf & " IH.SUPP_CUST_CODE, IH.VNO, IH.VDATE, IH.BILLNO, IH.INVOICE_DATE, " & vbCrLf & " IH.AUTO_KEY_MRR, IH.MRRDATE, " & vbCrLf & " ID.ITEM_CODE, ID.ITEM_QTY, " & vbCrLf & " NVL(ID.ITEM_QTY,0)-NVL(ID.SHORTAGE_QTY,0)-NVL(ID.REJECTED_QTY,0) AS APP_QTY, " & vbCrLf & " ITEM_RATE, " & vbCrLf & " BOOKTYPE, BOOKSUBTYPE "

        SqlStr = SqlStr & vbCrLf & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID "

        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.MKEY=ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.SUPP_CUST_CODE='" & pSupplierCode & "'" & vbCrLf & " AND ITEM_CODE='" & pItemCode & "' " & vbCrLf & " AND CUST_REF_NO='" & pPONO & "' " & vbCrLf & " AND ITEM_RATE<>" & pItemPriceAfterDisc & " " & vbCrLf & " AND NVL(ID.ITEM_QTY,0)-NVL(ID.SHORTAGE_QTY,0)-NVL(ID.REJECTED_QTY,0)>0 "

        SqlStr = SqlStr & vbCrLf & " AND ISFINALPOST='Y' AND VNO<>'-1'" ' AND IS_RATEDIFF='N'"

        SqlStr = SqlStr & vbCrLf & " AND MRRDATE>=TO_DATE('" & VB6.Format(pWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPur, ADODB.LockTypeEnum.adLockReadOnly)

        If RsPur.EOF = False Then
            Do While RsPur.EOF = False
                pVMKEY = IIf(IsDBNull(RsPur.Fields("mKey").Value), "-1", RsPur.Fields("mKey").Value)
                pPurFYEAR = IIf(IsDBNull(RsPur.Fields("FYEAR").Value), -1, RsPur.Fields("FYEAR").Value)
                pPURSuppCode = IIf(IsDBNull(RsPur.Fields("SUPP_CUST_CODE").Value), "", RsPur.Fields("SUPP_CUST_CODE").Value)
                pPURVNo = IIf(IsDBNull(RsPur.Fields("VNO").Value), "", RsPur.Fields("VNO").Value)
                pPURVDate = IIf(IsDBNull(RsPur.Fields("VDate").Value), "", RsPur.Fields("VDate").Value)
                pPURBillNo = IIf(IsDBNull(RsPur.Fields("BILLNO").Value), "", RsPur.Fields("BILLNO").Value)
                pPURBillDate = IIf(IsDBNull(RsPur.Fields("INVOICE_DATE").Value), "", RsPur.Fields("INVOICE_DATE").Value)
                pPURMRRNo = IIf(IsDBNull(RsPur.Fields("AUTO_KEY_MRR").Value), "", RsPur.Fields("AUTO_KEY_MRR").Value)
                pPURMRRDate = IIf(IsDBNull(RsPur.Fields("MRRDATE").Value), "", RsPur.Fields("MRRDATE").Value)
                pPURBillQty = IIf(IsDBNull(RsPur.Fields("ITEM_QTY").Value), 0, RsPur.Fields("ITEM_QTY").Value)
                pPURApprovedQty = IIf(IsDBNull(RsPur.Fields("APP_QTY").Value), 0, RsPur.Fields("APP_QTY").Value)
                mPURBillRate = IIf(IsDBNull(RsPur.Fields("ITEM_RATE").Value), 0, RsPur.Fields("ITEM_RATE").Value)
                pPURBookType = IIf(IsDBNull(RsPur.Fields("BookType").Value), "", RsPur.Fields("BookType").Value)
                pPURBookSubType = IIf(IsDBNull(RsPur.Fields("BOOKSUBTYPE").Value), "", RsPur.Fields("BOOKSUBTYPE").Value)
                mDIFF_RATE = GetAmountDiff(pPONO, mPOKey, pWEF, pSupplierCode, pItemCode)
                mDIFF_RATE = CDbl(VB6.Format((pItemPriceAfterDisc - mPURBillRate) - mDIFF_RATE, "0.000"))

                If pVMKEY <> "-1" And mDIFF_RATE <> 0 Then
                    SqlStr = " INSERT INTO FIN_DNCN_AMEND ( " & vbCrLf & " VMKEY, COMPANY_CODE, FYEAR, " & vbCrLf & " SUPP_CUST_CODE, VNO, VDATE, BILLNO, INVOICE_DATE, " & vbCrLf & " AUTO_KEY_MRR, MRRDATE, POMKEY , " & vbCrLf & " AUTO_KEY_PO, AMEND_WEF_DATE, " & vbCrLf & " ITEM_CODE, BILL_QTY, APPROVED_QTY, " & vbCrLf & " BILL_RATE, PO_RATE, " & vbCrLf & " BOOKTYPE, BOOKSUBTYPE, " & vbCrLf & " DNCN_NO, DNCN_DATE, IS_DNCN_MADE,DIFF_RATE " & vbCrLf & " ) VALUES ( "


                    SqlStr = SqlStr & vbCrLf & " '" & pVMKEY & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & pPurFYEAR & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(pPURSuppCode) & "', '" & pPURVNo & "', TO_DATE('" & VB6.Format(pPURVDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & pPURBillNo & "', TO_DATE('" & VB6.Format(pPURBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " '" & pPURMRRNo & "', TO_DATE('" & VB6.Format(pPURMRRDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & mPOKey & ", " & vbCrLf & " " & pPONO & ", TO_DATE('" & VB6.Format(pWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & pItemCode & "', " & pPURBillQty & ", " & pPURApprovedQty & ", " & vbCrLf & " " & mPURBillRate & ", " & pItemPriceAfterDisc & ", " & vbCrLf & " '" & pPURBookType & "', '" & pPURBookSubType & "'," & vbCrLf & " '', '', 'N', " & mDIFF_RATE & ")"

                    PubDBCn.Execute(SqlStr)

                    'Sk 08-06-2005
                    '                SqlStr = " UPDATE FIN_PURCHASE_HDR " & vbCrLf _
                    ''                        & " SET IS_RATEDIFF='Y'" & vbCrLf _
                    ''                        & " WHERE MKEY IN ("
                    '
                    '                SqlStr = SqlStr & vbCrLf & " SELECT IH.MKEY " & vbCrLf _
                    ''                        & " FROM FIN_PURCHASE_HDR IH, FIN_PURCHASE_DET ID "
                    '
                    '                SqlStr = SqlStr & vbCrLf _
                    ''                        & " WHERE " & vbCrLf _
                    ''                        & " IH.MKEY=ID.MKEY " & vbCrLf _
                    ''                        & " AND IH.COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
                    ''                        & " AND IH.SUPP_CUST_CODE='" & pSupplierCode & "'" & vbCrLf _
                    ''                        & " AND ITEM_CODE='" & pItemCode & "' " & vbCrLf _
                    ''                        & " AND CUST_REF_NO='" & pPONO & "' " & vbCrLf _
                    ''                        & " AND ITEM_RATE<>" & pItemPriceAfterDisc & " " & vbCrLf _
                    ''                        & " AND NVL(ID.ITEM_QTY,0)-NVL(ID.SHORTAGE_QTY,0)-NVL(ID.REJECTED_QTY,0)>0 "
                    '
                    '                SqlStr = SqlStr & vbCrLf & " AND ISFINALPOST='Y' AND VNO<>'-1' AND IS_RATEDIFF='N'"
                    '                SqlStr = SqlStr & vbCrLf _
                    ''                        & " AND MRRDATE>='" & VB6.Format(pWEF, "DD-MMM-YYYY") & "')"
                    '
                    '                PubDBCn.Execute SqlStr
                End If
                RsPur.MoveNext()
            Loop
        End If
        UpdateRateDiffVoucherForDN = True
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateRateDiffVoucherForDN = False
    End Function

    Private Function GetAmountDiff(ByRef pPONO As Double, ByRef pPOKey As Double, ByRef pWEF As String, ByRef pSupplierCode As String, ByRef pItemCode As String) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mTotalDIFF_RATE As Double

        GetAmountDiff = 0

        SqlStr = "SELECT DIFF_RATE " & vbCrLf & " FROM FIN_DNCN_AMEND IH"

        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.SUPP_CUST_CODE='" & pSupplierCode & "'" & vbCrLf & " AND ITEM_CODE='" & pItemCode & "' " & vbCrLf & " AND AUTO_KEY_PO='" & pPONO & "' " & vbCrLf & " AND POMKEY<>'" & pPOKey & "'" & vbCrLf & " AND DIFF_RATE<>0"

        SqlStr = SqlStr & vbCrLf & " AND AMEND_WEF_DATE>=TO_DATE('" & VB6.Format(pWEF, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mTotalDIFF_RATE = mTotalDIFF_RATE + IIf(IsDbNull(RsTemp.Fields("DIFF_RATE").Value), 0, RsTemp.Fields("DIFF_RATE").Value)
                RsTemp.MoveNext()
            Loop
        End If
        GetAmountDiff = mTotalDIFF_RATE
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function Update1(ByRef mPOKey As Double, ByRef pPONO As Double, ByRef pWEF As String, ByRef pSupplier As String, ByRef pPOAmendNo As Integer) As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsPO As ADODB.Recordset = Nothing
        Dim pSupplierCode As String = ""
        Dim mItemCode As String
        Dim mItemUOM As String = ""
        Dim mItemPrice As Double
        Dim mItemPriceAfterDisc As Double

        If MainClass.ValidateWithMasterTable(pSupplier, "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            pSupplierCode = MasterNo
        End If

        Update1 = False
        SqlStr = " SELECT IH.MKEY, IH.AUTO_KEY_PO, " & vbCrLf & " ID.PO_WEF_DATE, " & vbCrLf & " ID.ITEM_CODE, ID.ITEM_UOM, ID.ITEM_PRICE, ID.ITEM_DIS_PER, " & vbCrLf & " (NVL(ID.ITEM_PRICE,0) - ROUND((NVL(ID.ITEM_PRICE,0) * ID.ITEM_DIS_PER)/100,4)) AS I_RATE " & vbCrLf & " FROM PUR_PURCHASE_HDR IH, PUR_PURCHASE_DET ID " & vbCrLf & " WHERE " & vbCrLf & " IH.MKEY=" & mPOKey & "" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.SUPP_CUST_CODE='" & pSupplierCode & "'" & vbCrLf & " AND IH.MKEY=ID.MKEY " & vbCrLf & " AND GETLASTPORATE(IH.COMPANY_CODE, IH.AUTO_KEY_PO, IH.AMEND_NO, ID.ITEM_CODE) <> (NVL(ID.ITEM_PRICE,0) - ROUND((NVL(ID.ITEM_PRICE,0) * ID.ITEM_DIS_PER)/100,4)) "


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPO, ADODB.LockTypeEnum.adLockReadOnly)

        If RsPO.EOF = False Then
            Do While Not RsPO.EOF
                mItemCode = Trim(IIf(IsDbNull(RsPO.Fields("ITEM_CODE").Value), "", RsPO.Fields("ITEM_CODE").Value))
                mItemUOM = IIf(IsDbNull(RsPO.Fields("ITEM_UOM").Value), "", RsPO.Fields("ITEM_UOM").Value)
                mItemPrice = IIf(IsDbNull(RsPO.Fields("ITEM_PRICE").Value), 0, RsPO.Fields("ITEM_PRICE").Value)
                mItemPriceAfterDisc = IIf(IsDbNull(RsPO.Fields("I_RATE").Value), 0, RsPO.Fields("I_RATE").Value)
                pWEF = VB6.Format(IIf(IsDbNull(RsPO.Fields("PO_WEF_DATE").Value), "", RsPO.Fields("PO_WEF_DATE").Value), "DD/MM/YYYY")
                pPONO = IIf(IsDbNull(RsPO.Fields("AUTO_KEY_PO").Value), "", RsPO.Fields("AUTO_KEY_PO").Value)

                '            If UpdateRateDiffVoucherForDN(mPOKey, pPONO, pWEF, pSupplierCode, mItemCode, mItemPriceAfterDisc) = False Then GoTo ErrPart

                RsPO.MoveNext()
            Loop
        End If
        Update1 = True
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        Update1 = False
    End Function

    Private Sub _OptOrderType_0_CheckedChanged(sender As Object, e As EventArgs) Handles _OptOrderType_0.CheckedChanged
        cmdShow.Enabled = True
    End Sub

    Private Sub _OptOrderType_2_CheckedChanged(sender As Object, e As EventArgs) Handles _OptOrderType_2.CheckedChanged
        cmdShow.Enabled = True
    End Sub

    Private Sub _OptOrderType_1_CheckedChanged(sender As Object, e As EventArgs) Handles _OptOrderType_1.CheckedChanged
        cmdShow.Enabled = True
    End Sub

    Private Sub _OptOrderType_3_CheckedChanged(sender As Object, e As EventArgs) Handles _OptOrderType_3.CheckedChanged
        cmdShow.Enabled = True
    End Sub

    Private Sub ChkALL_CheckedChanged(sender As Object, e As EventArgs) Handles ChkALL.CheckedChanged
        cmdShow.Enabled = True
    End Sub
    Private Sub FillUltraGrid(pMakeSql As String)
        Dim oledbCnn As OleDbConnection
        Dim oledbAdapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim sql As String
        Dim i As Integer
        Dim inti As Integer

        'UltraGrid1.DataSource.Rows.Clear()
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

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMKEY - 1).Header.Caption = "MKey"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPONo - 1).Header.Caption = "Purchase Order No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColWEF - 1).Header.Caption = "WEF Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPOAmendNo - 1).Header.Caption = "Amend No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPurType - 1).Header.Caption = "Purchase Type"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColOrderType - 1).Header.Caption = "Order Type"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPartyName - 1).Header.Caption = "Supplier Name"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemValue - 1).Header.Caption = "Gross Amount"

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPostStatus - 1).Header.Caption = "Post Status"

            ''enable/disable the columns
            For inti = 0 To UltraGrid1.DisplayLayout.Bands(0).Columns.Count - 2
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).CellActivation = Activation.ActivateOnly
                UltraGrid1.DisplayLayout.Bands(0).Columns(inti).Header.Appearance.TextHAlign = HAlign.Center
            Next

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPostStatus - 1).CellActivation = Activation.AllowEdit

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemValue - 1).CellAppearance.TextHAlign = HAlign.Right

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemValue - 1).MaskInput = "9999999.999"


            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemValue - 1).PromptChar = ""


            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemValue - 1).Style = UltraWinGrid.ColumnStyle.Double


            ''for hidden
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColMKEY - 1).Hidden = True


            ' to define width of the columns

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPONo - 1).Width = 75
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColWEF - 1).Width = 75
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPOAmendNo - 1).Width = 50
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPurType - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColOrderType - 1).Width = 60
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPartyName - 1).Width = 250
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemValue - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPostStatus - 1).Width = 80


            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPostStatus - 1).Style = UltraWinGrid.ColumnStyle.CheckBox
            'UltraGrid1.DisplayLayout.Bands(0).Columns(ColPrint - 1).Header.Caption = "Print"
            'UltraGrid1.DisplayLayout.Bands(0).Columns(ColPrint - 1).ButtonDisplayStyle = UltraWinGrid.ButtonDisplayStyle.Always

            MainClass.SetInfragisticsGrid(UltraGrid1, -1, "Filter Row", "")
        Catch ex As Exception
            ErrorMsg(ex.Message, "")
            Me.Cursor = Windows.Forms.Cursors.Default
        End Try
    End Sub
End Class
