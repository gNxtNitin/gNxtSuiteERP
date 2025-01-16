Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmPOApprovalWithoutTC
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
        Dim mPONo As Double
        Dim mWef As String
        Dim mSupplier As String
        Dim mUpdateCount As Integer
        Dim mPOAmendNo As Integer
        Dim mCanPostPO As Boolean
        Dim mPOSeq As Double
        Dim mAuthorisation As String

        mAuthorisation = IIf(InStr(1, XRIGHT, "S") > 0, "Y", "N")
        If mAuthorisation = "N" Then
            MsgBox("You have no Right to Post PO. ", MsgBoxStyle.Critical)
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

                .Col = ColPONo
                mPONo = CDbl(Trim(.Text))
                mPOSeq = CDbl(Mid(CStr(mPONo), 1, Len(Str(mPONo)) - 6))

                .Col = ColWEF
                mWef = Trim(.Text)

                .Col = ColPartyName
                mSupplier = Trim(.Text)

                .Col = ColPOAmendNo
                mPOAmendNo = CInt(Trim(.Text))

                .Col = ColPostStatus
                If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                    If mWef = "" Then
                        MsgInformation("WEF Date is Blank, So cann't be saved.")
                        GoTo ErrPart
                    End If


                    SqlStr = "UPDATE PUR_PURCHASE_HDR SET APPROVAL_WO_TC='Y', UPDATE_FROM='N'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE MKEY=" & mMKEY & "" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

                    PubDBCn.Execute(SqlStr)

                    mUpdateCount = mUpdateCount + 1
                    '                End If
                End If
            Next
        End With
        PubDBCn.CommitTrans()
        MsgBox("Total " & mUpdateCount & " PO Posted.", MsgBoxStyle.Information)
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
        MainClass.ClearGrid(SprdMain, RowHeight)
        OptSelection(1).Checked = True
        Show1()

        FormatSprdMain()
        cmdShow.Enabled = False
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
    Public Sub frmPOApprovalWithoutTC_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Approval for Third Party Insp in PO (Without TC)"
        '    blIsCapital.text
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmPOApprovalWithoutTC_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        ChkALL.CheckState = System.Windows.Forms.CheckState.Checked

        FormatSprdMain()
        cmdShow.Enabled = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub Show1()

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mSuppCustCode As String


        SqlStr = "SELECT POMain.MKEY, POMain.AUTO_KEY_PO, TO_CHAR(POMAIN.AMEND_WEF_DATE,'DD/MM/YYYY') AS AMEND_WEF_DATE ,AMEND_NO,  " & vbCrLf & " CASE WHEN POMain.PUR_TYPE='P' THEN 'Purchase Order'  " & vbCrLf & " WHEN POMain.PUR_TYPE='W' THEN 'Work Order' " & vbCrLf & " WHEN POMain.PUR_TYPE='J' THEN 'Job Work Order' WHEN POMain.PUR_TYPE='R' THEN 'Project Order' END AS Purchase_Type, " & vbCrLf & " CASE WHEN POMain.ORDER_TYPE='O' THEN 'Open'  " & vbCrLf & " WHEN POMain.ORDER_TYPE='C' THEN 'Close' END AS Order_Type, " & vbCrLf & " ACM.SUPP_CUST_NAME,To_CHAR(SUM(GROSS_AMT)) AS ITEM_VALUE,'' " & vbCrLf & " FROM PUR_PURCHASE_HDR POMain, PUR_PURCHASE_DET POD, FIN_SUPP_CUST_MST ACM, INV_ITEM_MST IMST, INV_GENERAL_MST GMST" & vbCrLf & " WHERE " & vbCrLf & " POMain.MKEY=POD.MKEY " & vbCrLf & " AND POMain.SUPP_CUST_CODE=ACM.SUPP_CUST_CODE " & vbCrLf & " AND POMain.Company_Code=ACM.Company_Code " & vbCrLf & " AND POD.ITEM_CODE=IMST.ITEM_CODE " & vbCrLf & " AND POMain.Company_Code=IMST.Company_Code" & vbCrLf & " AND IMST.CATEGORY_CODE=GMST.GEN_CODE" & vbCrLf & " AND IMST.Company_Code=GMST.Company_Code AND GMST.GEN_TYPE='C' AND GMST.IS_TC_REQ='Y'" & vbCrLf & " AND POMain.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " "


        If RsCompany.Fields("FYEAR").Value < ConOPENPO_CONTINOUS_YEAR Then
            SqlStr = SqlStr & vbCrLf & " AND SUBSTR(AUTO_KEY_PO,LENGTH(AUTO_KEY_PO)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
        Else
            SqlStr = SqlStr & vbCrLf & " AND SUBSTR(AUTO_KEY_PO,LENGTH(AUTO_KEY_PO)-5,4)>=" & ConOPENPO_CONTINOUS_YEAR & ""
        End If

        SqlStr = SqlStr & vbCrLf & " AND POMain.PUR_TYPE IN ('P','R')"

        SqlStr = SqlStr & vbCrLf & " AND POMain.PO_STATUS='N' AND POMain.PO_CLOSED='N' AND APPROVAL_WO_TC='N'"


        If ChkALL.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(TxtAccount.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " And ACM.SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(UCase(TxtAccount.Text)) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " GROUP BY " & vbCrLf & " POMain.MKEY, POMain.AUTO_KEY_PO, POMAIN.AMEND_WEF_DATE, AMEND_NO,  " & vbCrLf & " POMain.PUR_TYPE,  POMain.ORDER_TYPE, ACM.SUPP_CUST_NAME "

        SqlStr = SqlStr & vbCrLf & " ORDER BY SUBSTR(AUTO_KEY_PO,LENGTH(AUTO_KEY_PO)-5,4), POMain.PUR_TYPE,POMain.ORDER_TYPE,POMAIN.AMEND_WEF_DATE,POMain.AUTO_KEY_PO"



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
            .set_colwidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColMKEY
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_colwidth(ColMKEY, 11)
            .ColHidden = True

            .Col = ColPONo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_colwidth(ColPONo, 9)

            .Col = ColWEF

            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeDateFormat = SS_CELL_DATE_FORMAT_DDMMYY

            '    .CellType = SS_CELL_TYPE_EDIT
            '    .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_colwidth(ColWEF, 9)

            .Col = ColPOAmendNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_colwidth(ColPOAmendNo, 6)

            .Col = ColPurType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_colwidth(ColPurType, 10)

            .Col = ColOrderType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_colwidth(ColOrderType, 6)

            .Col = ColPartyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_colwidth(ColPartyName, 30)

            .Col = ColItemValue
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("99999999999.99")
            .TypeFloatMin = CDbl("-99999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_colwidth(ColItemValue, 9)


            .Col = ColPostStatus
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_colwidth(ColPostStatus, 6)
            .Value = CStr(System.Windows.Forms.CheckState.Unchecked)

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, ColItemValue)
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

            .Col = ColPONo
            .Text = "PO No."

            .Col = ColWEF
            .Text = "WEF Date"

            .Col = ColPOAmendNo
            .Text = "Amend No"

            .Col = ColPurType
            .Text = "Purchase Type"

            .Col = ColOrderType
            .Text = "Order Type"

            .Col = ColPartyName
            .Text = "Supplier Name"

            .Col = ColItemValue
            .Text = "Gross Amount"

            .Col = ColPostStatus
            .Text = "Post Status"
        End With
    End Sub
    Private Sub frmPOApprovalWithoutTC_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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

        'Dim ss As New frmPO

        SprdMain.Row = SprdMain.ActiveRow

        SprdMain.Col = ColPONo
        xPoNo = Val(SprdMain.Text)

        If xPoNo <= 0 Then Exit Sub

        SprdMain.Col = ColPOAmendNo
        xAmendPONo = Val(SprdMain.Text)

        SprdMain.Col = ColWEF
        xPOWEF = VB6.Format(CDate(SprdMain.Text), "DD/MM/YYYY")

        SqlStr = "SELECT * from PUR_PURCHASE_HDR WHERE AUTO_KEY_PO=" & xPoNo & " AND AMEND_NO=" & xAmendPONo & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


        If RsTemp.EOF = False Then
            frmPO_GST.MdiParent = Me.MdiParent

            frmPO_GST.lblBookType.Text = RsTemp.Fields("PUR_TYPE").Value & RsTemp.Fields("ORDER_TYPE").Value
            frmPO_GST.Show()
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
End Class
