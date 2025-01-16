Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinDataSource
Imports System.Data.OleDb

Friend Class frmLoadingSlipApproval
   Inherits System.Windows.Forms.Form
   Dim RsTransMain As ADODB.Recordset ''Recordset
   'Private PvtDBCn As ADODB.Connection

   Dim ADDMode As Boolean
   Dim MODIFYMode As Boolean
   Dim XRIGHT As String
   Dim xMyMenu As String

    Private Const ColMKEY As Short = 1
    Private Const ColInvoiceNo As Short = 2
    Private Const ColInvoiceDate As Short = 3
    Private Const ColCustomerCode As Short = 4
    Private Const ColPartyName As Short = 5
    Private Const ColItemValue As Short = 6
    Private Const ColPreviousClearDate As Short = 7
    Private Const ColPostStatus As Short = 8

    Dim mActiveRow As Integer

    Dim FormActive As Boolean

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub


    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If UpdateMain1() = True Then
            cmdSave.Enabled = False
        Else
            MsgInformation("Record not saved")
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        If Err.Description = "" Then Exit Sub
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub
    Private Sub frmLoadingSlipApproval_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Close()
    End Sub
    'Private Sub SprdView_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdView.Change

    '    MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    'End Sub

    'Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
    '    With SprdView
    '        .Row = eventArgs.row
    '        .Col = 1
    '        txtBillNo.Text = .Text

    '        .Col = 2
    '        txtBillDate.Text = CDate(.Text).ToString("dd/MM/yyyy")

    '        txtBillNo_Validating(txtBillNo, New System.ComponentModel.CancelEventArgs(False))
    '        If txtBillNo.Enabled = True Then txtBillNo.Focus()
    '        CmdView_Click(CmdView, New System.EventArgs())
    '    End With
    'End Sub
    Private Function UpdateMain1() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mClearDate As String

        Dim CntRow As Integer
        Dim mMKEY As String
        Dim mInvoiceNo As String
        Dim mInvoiceDate As String
        Dim mSupplier As String
        Dim mUpdateCount As Integer
        Dim mPOAmendNo As Integer
        Dim mCanPostPO As Boolean
        Dim mPOSeq As Double
        Dim mAuthorisation As String
        Dim mRow As UltraGridRow
        Dim mMaxRow As Long
        Dim mFlag As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mMaxRow = UltraGrid1.Rows.Count

        For CntRow = 0 To mMaxRow - 1
            mRow = Me.UltraGrid1.Rows(CntRow)



            mCanPostPO = False

            mMKEY = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColMKEY - 1))
            mInvoiceNo = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColInvoiceNo - 1))
            mInvoiceDate = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColInvoiceDate - 1))


            mFlag = mRow.GetCellText(Me.UltraGrid1.DisplayLayout.Bands(0).Columns(ColPostStatus - 1))
            If UCase(mFlag) = "TRUE" Then

                If txtClearDate.Text <> "__/__/____ __:__" Then
                    mClearDate = CDate(txtClearDate.Text).ToString("dd-MMM-yyyy HH:mm")
                Else
                    mClearDate = ""
                End If

                SqlStr = ""



                If mClearDate <> "" Then

                    SqlStr = "DELETE FROM FIN_LOADING_SLIP_UNLOCK" & vbCrLf _
                            & " WHERE COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & "" & vbCrLf _
                            & " AND BILL_NO='" & MainClass.AllowSingleQuote(mInvoiceNo) & "'" & vbCrLf _
                            & " AND BILL_DATE=TO_DATE('" & CDate(mInvoiceDate).ToString("dd-MMM-yyyy") & "','DD-MON-YYYY') AND BOOKTYPE='" & lblBookType.Text & "'"

                    PubDBCn.Execute(SqlStr)

                    SqlStr = "INSERT INTO FIN_LOADING_SLIP_UNLOCK (" & vbCrLf _
                             & " COMPANY_CODE, BILL_NO, BILL_DATE,CLEAR_DATE, AUTH_GIVEN_BY, REMARKS, " & vbCrLf _
                             & " ADDUSER,ADDDATE,MODUSER,MODDATE,BOOKTYPE)" & vbCrLf _
                             & " VALUES( " & vbCrLf _
                             & " " & RsCompany.Fields("Company_Code").Value & ", '" & MainClass.AllowSingleQuote(mInvoiceNo) & "', " & vbCrLf _
                             & " TO_DATE('" & CDate(mInvoiceDate).ToString("dd-MMM-yyyy") & "','DD-MON-YYYY'), " & vbCrLf _
                             & " TO_DATE('" & mClearDate & "','DD-MON-YYYY HH24:MI'), " & vbCrLf _
                             & " '" & MainClass.AllowSingleQuote(txtAuthorityName.Text) & "', " & vbCrLf _
                             & " '" & MainClass.AllowSingleQuote(txtReason.Text) & "', " & vbCrLf _
                             & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & CDate(PubCurrDate).ToString("dd-MMM-yyyy") & "','DD-MON-YYYY'),'','','" & lblBookType.Text & "')"

                    PubDBCn.Execute(SqlStr)

                    mUpdateCount = mUpdateCount + 1
                End If
            End If
        Next



        'If txtClearDate.Text <> "__/__/____ __:__" Then
        '    mClearDate = CDate(txtClearDate.Text).ToString("dd-MMM-yyyy HH:mm")
        'Else
        '    mClearDate = ""
        'End If

        'SqlStr = ""


        'If ADDMode = True Then
        '    SqlStr = "INSERT INTO FIN_LOADING_SLIP_UNLOCK (" & vbCrLf _
        '             & " COMPANY_CODE, BILL_NO, BILL_DATE,CLEAR_DATE, AUTH_GIVEN_BY, REMARKS, " & vbCrLf _
        '             & " ADDUSER,ADDDATE,MODUSER,MODDATE)" & vbCrLf _
        '             & " VALUES( " & vbCrLf _
        '             & " " & RsCompany.Fields("Company_Code").Value & ", '" & MainClass.AllowSingleQuote(txtBillNo.Text) & "', " & vbCrLf _
        '             & " TO_DATE('" & CDate(txtBillDate.Text).ToString("dd-MMM-yyyy") & "','DD-MON-YYYY'), " & vbCrLf _
        '             & " TO_DATE('" & mClearDate & "','DD-MON-YYYY HH24:MI'), " & vbCrLf _
        '             & " '" & MainClass.AllowSingleQuote(txtAuthorityName.Text) & "', " & vbCrLf _
        '             & " '" & MainClass.AllowSingleQuote(txtReason.Text) & "', " & vbCrLf _
        '             & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & CDate(PubCurrDate).ToString("dd-MMM-yyyy") & "','DD-MON-YYYY'),'','')"
        'ElseIf MODIFYMode = True Then
        '    SqlStr = ""
        '    SqlStr = "UPDATE FIN_LOADING_SLIP_UNLOCK SET " & vbCrLf _
        '       & " CLEAR_DATE=TO_DATE('" & mClearDate & "','DD-MON-YYYY HH24:MI')," & vbCrLf _
        '       & " AUTH_GIVEN_BY='" & MainClass.AllowSingleQuote(txtAuthorityName.Text) & "'," & vbCrLf _
        '       & " REMARKS='" & MainClass.AllowSingleQuote(txtReason.Text) & "'," & vbCrLf _
        '       & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
        '       & " Moddate=TO_DATE('" & CDate(PubCurrDate).ToString("dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
        '       & " WHERE COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
        '       & " AND BILL_NO='" & MainClass.AllowSingleQuote(txtBillNo.Text) & "' " & vbCrLf _
        '       & " AND BILL_DATE=TO_DATE('" & CDate(txtBillDate.Text).ToString("dd-MMM-yyyy") & "','DD-MON-YYYY')"
        'End If

        'PubDBCn.Execute(SqlStr)

        UpdateMain1 = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        '    Resume
        UpdateMain1 = False
        PubDBCn.RollbackTrans() ''
        RsTransMain.Requery() ''.Refresh
        If Err.Description = "" Then Exit Function
        If Err.Number = -2147217900 Then
            ErrorMsg("Duplicate Ref Generated, Save Again", "Duplicate", MsgBoxStyle.Critical)
        Else
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If

    End Function
    Private Function FieldsVarification() As Boolean

        On Error GoTo err_Renamed

        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        FieldsVarification = True

        'If txtClearDate.Text <> "__/__/____ __:__" Then
        '    If CDate(txtClearDate.Text) < CDate(txtBillDate.Text) Then
        '        MsgInformation("Clear Date Cann't be Less Than Bill Date.")
        '        FieldsVarification = False
        '        Exit Function
        '    End If
        'End If


        If txtAuthorityName.Text = "" Then
            MsgBox("Authority Name is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtAuthorityName.Focus()
            Exit Function
        End If

        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Function
    Public Sub frmLoadingSlipApproval_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = ""
        SqlStr = "Select * from FIN_LOADING_SLIP_UNLOCK Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTransMain, ADODB.LockTypeEnum.adLockReadOnly)

        Call Show1("L")

        'Call AssignGrid(False)
        '    Call SetTextLengths

        'If cmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ''Resume
    End Sub

    '    Private Sub AssignGrid(ByRef mRefresh As Boolean)

    '        On Error GoTo AssignGridErr
    '        Dim SqlStr As String = ""

    '        SqlStr = ""

    '        ''SELECT CLAUSE...

    '        SqlStr = "SELECT  IH.BILL_NO, TO_CHAR(BILL_DATE,'DD/MM/YYYY') AS BILL_DATE, TO_CHAR(CLEAR_DATE,'DD/MM/YYYY HH24:MI') CLEAR_DATE, CMST.SUPP_CUST_NAME "

    '        ''FROM CLAUSE...

    '        SqlStr = SqlStr & vbCrLf & " FROM FIN_LOADING_SLIP_UNLOCK IH, FIN_INVOICE_HDR INV, FIN_SUPP_CUST_MST CMST"

    '        ''WHERE CLAUSE...

    '        SqlStr = SqlStr & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
    '           & " AND IH.COMPANY_CODE=INV.COMPANY_CODE " & vbCrLf _
    '           & " AND IH.BILL_NO=INV.BILLNO" & vbCrLf _
    '           & " AND IH.BILL_DATE=INV.INVOICE_DATE" & vbCrLf _
    '           & " AND INV.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf _
    '           & " AND INV.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE "



    '        ''ORDER BY CLAUSE...

    '        SqlStr = SqlStr & vbCrLf & " Order by IH.BILL_NO, IH.BILL_DATE"

    '        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
    '        FormatSprdView()
    '        Exit Sub
    'AssignGridErr:
    '        MsgBox(Err.Description, MsgBoxStyle.Information)
    '    End Sub

    Private Sub FormatSprdView()

        With SprdView
            .Row = -1

            .set_RowHeight(0, 400)

            .set_ColWidth(0, 600)
            .set_ColWidth(1, 1000)
            .set_ColWidth(2, 1000)
            .set_ColWidth(3, 1500)
            .set_ColWidth(4, 4000)
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            SprdView.set_RowHeight(-1, 300)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        With RsTransMain
            txtAuthorityName.MaxLength = .Fields("AUTH_GIVEN_BY").DefinedSize
            txtReason.MaxLength = .Fields("REMARKS").DefinedSize
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub Clear1()



        txtClearDate.Text = "__/__/____ __:__"

        txtAuthorityName.Text = ""
        txtReason.Text = ""
        txtAuthorityName.Enabled = True

        txtReason.Enabled = True
        'MainClass.ButtonStatus(Me, XRIGHT, RsTransMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub frmLoadingSlipApproval_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmLoadingSlipApproval_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        '    MainClass.DoFunctionKey Me, KeyCode
    End Sub

    Public Sub frmLoadingSlipApproval_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        xMyMenu = myMenu
        MainClass.RightsToButton(Me, XRIGHT)

        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Call SetMainFormCordinate(Me)
        'Me.Top = 0
        'Me.Left = 0
        ''Me.Height = VB6.TwipsToPixelsY(3420)
        ''Me.Width = VB6.TwipsToPixelsX(9915)

        AdoDCMain.Visible = False

        FormActive = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub txtAuthorityName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAuthorityName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAuthorityName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAuthorityName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtAuthorityName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtClearDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtClearDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If txtClearDate.Text = "__/__/____ __:__" Then GoTo EventExitSub

        If Not IsDate(txtClearDate.Text) Then
            MsgInformation("Invalid Clear Date.")
            txtClearDate.Focus()
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtReason_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtReason.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtReason_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtReason.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtReason.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtClearDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtClearDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
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
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColInvoiceNo - 1).Header.Caption = "Invoice No"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColInvoiceDate - 1).Header.Caption = "Invoice Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCustomerCode - 1).Header.Caption = "Customer Code"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPartyName - 1).Header.Caption = "Customer Date"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemValue - 1).Header.Caption = "Bill Amount"
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPreviousClearDate - 1).Header.Caption = "Clear Date"
            '
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPostStatus - 1).Header.Caption = "Status"

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

            UltraGrid1.DisplayLayout.Bands(0).Columns(ColInvoiceNo - 1).Width = 120
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColInvoiceDate - 1).Width = 90
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColCustomerCode - 1).Width = 60
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPartyName - 1).Width = 250
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColItemValue - 1).Width = 100
            UltraGrid1.DisplayLayout.Bands(0).Columns(ColPreviousClearDate - 1).Width = 100
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

    Private Sub cmdShow_Click(sender As Object, e As EventArgs) Handles cmdShow.Click
        'If FieldsVerification = False Then Exit Sub
        ''MainClass.ClearGrid(SprdMain, RowHeight)
        'OptSelection(1).Checked = True
        Show1("S")
        cmdSave.Enabled = True
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub Show1(pShowType As String)

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mSuppCustCode As String

        Dim mFromDate As String = ""

        mFromDate = "01/06/2023"

        SqlStr = " SELECT IH.MKEY, IH.BILLNO, IH.INVOICE_DATE, IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, IH.NETVALUE, "

        SqlStr = SqlStr & vbCrLf & " (SELECT TO_CHAR(CLEAR_DATE,'DD-MM-YYYY HH24:MI') FROM FIN_LOADING_SLIP_UNLOCK " & vbCrLf _
            & " WHERE COMPANY_CODE=IH.COMPANY_CODE AND BILL_NO=IH.BILLNO AND BILL_DATE=IH.INVOICE_DATE AND BOOKTYPE='R') AS CLEAR_DATE,"

        SqlStr = SqlStr & vbCrLf & "'False'"


        SqlStr = SqlStr & vbCrLf _
              & " FROM FIN_INVOICE_HDR IH, FIN_SUPP_CUST_MST CMST" & vbCrLf _
              & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
              & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
              & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE"

        SqlStr = SqlStr & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

        SqlStr = SqlStr & vbCrLf & " AND IH.INVOICESEQTYPE NOT IN (3,5,4,7,8,9)"

        If lblBookType.Text = "R" Then
            SqlStr = SqlStr & vbCrLf & " And IH.CANCELLED='N' AND IH.BOOKCODE=" & ConSalesBookCode & " AND (GRNNO IS NULL OR GRNNO='')"

            SqlStr = SqlStr & vbCrLf & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            SqlStr = SqlStr & vbCrLf _
                & " And BILLNo Not In (Select BILLNO FROM FIN_supp_sale_HDR WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " And CANCELLED='N' and ISFINALPOST='Y')"
        Else
            mFromDate = "01/12/2023"

            SqlStr = SqlStr & vbCrLf & " And IH.CANCELLED='N' AND IH.BOOKCODE=" & ConSalesBookCode & " AND (GRNNO IS NULL OR GRNNO='')"

            SqlStr = SqlStr & vbCrLf & " AND IH.INVOICE_DATE>=TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            SqlStr = SqlStr & vbCrLf _
                & " And BILLNo Not In (Select REF_NO FROM DSP_LOADING_DET WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " )"
        End If



        If pShowType = "L" Then
            SqlStr = SqlStr & vbCrLf & "AND 1=2"
        End If


        SqlStr = SqlStr & vbCrLf _
            & " ORDER BY IH.BILLNO, IH.INVOICE_DATE"

        FillUltraGrid(SqlStr)

        txtAuthorityName.Enabled = True
        txtReason.Enabled = True

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgInformation(Err.Description)
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
End Class
