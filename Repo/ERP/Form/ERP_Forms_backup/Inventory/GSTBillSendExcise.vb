Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmGSTBillSendExcise
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection					
    Dim mAccountCode As String
    Private Const RowHeight As Short = 15

    Private Const ColMRRNo As Short = 1
    Private Const ColMRRDate As Short = 2
    Private Const ColPartyName As Short = 3
    Private Const ColBillNo As Short = 4
    Private Const ColBillDate As Short = 5
    Private Const ColReceivedFlag As Short = 6

    Dim mActiveRow As Integer
    Dim FormActive As Boolean

    Private Sub chkShow_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkShow.CheckStateChanged
        If chkShow.CheckState = System.Windows.Forms.CheckState.Checked Then
            txtDateFrom.Enabled = False
            txtDateTo.Enabled = False
        Else
            txtDateFrom.Enabled = True
            txtDateTo.Enabled = True
        End If
        cmdShow.Enabled = True
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrPart
        Dim cntRow As Integer
        Dim SqlStr As String = ""
        Dim mMRRNO As Double
        Dim mUpdateCount As Integer

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        mUpdateCount = 0
        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColMRRNo
                mMRRNO = CDbl(Trim(.Text))

                .Col = ColReceivedFlag
                If .Value = CStr(System.Windows.Forms.CheckState.Checked) Then
                    SqlStr = "UPDATE INV_GATE_HDR SET SEND_GSTBILL_FLAG='Y', SEND_GSTBILL_DATE=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),UPDATE_FROM='N'," & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf & " WHERE AUTO_KEY_MRR=" & mMRRNO & ""
                    PubDBCn.Execute(SqlStr)

                    mUpdateCount = mUpdateCount + 1
                End If
            Next
        End With
        PubDBCn.CommitTrans()
        MsgBox("Total " & mUpdateCount & " Bill Send to Excise.", MsgBoxStyle.Information)
        Exit Sub
ErrPart:
        ''Resume					
        ErrorMsg(Err.Description, err.Number, MsgBoxStyle.Critical)
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
        If MainClass.ChkIsdateF(txtDateFrom) = False Then txtDateFrom.Focus() : Exit Function
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then txtDateFrom.SetFocus					

        If MainClass.ChkIsdateF(txtDateTo) = False Then txtDateTo.Focus() : Exit Function
        '    If FYChk(CDate(txtDateFrom.Text)) = False Then txtDateFrom.SetFocus					

        FieldsVerification = True
        Exit Function
ERR1:
        FieldsVerification = False
    End Function
    Public Sub frmGSTBillSendExcise_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        FormActive = True
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmGSTBillSendExcise_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection					
        ''PvtDBCn.Open StrConn					
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)					
        ''Me.Width = VB6.TwipsToPixelsX(11355)					

        txtDateFrom.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        chkShow.CheckState = System.Windows.Forms.CheckState.Checked
        txtDateFrom.Enabled = False
        txtDateTo.Enabled = False


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

        SqlStr = "SELECT GRMain.AUTO_KEY_MRR,TO_CHAR(GRMain.MRR_DATE,'DD/MM/YYYY') AS MRR_DATE,  " & vbCrLf & " ACM.SUPP_CUST_NAME, GRMain.BILL_NO, TO_CHAR(GRMain.BILL_DATE,'DD/MM/YYYY') AS BILL_DATE, DECODE(SEND_GSTBILL_FLAG,'Y','1','0') AS PFLAG " & vbCrLf & " FROM INV_GATE_HDR GRMain,FIN_SUPP_CUST_MST ACM" & vbCrLf & " WHERE GRMain.SUPP_CUST_CODE=ACM.SUPP_CUST_CODE " & vbCrLf & " AND SUBSTR(GRMain.AUTO_KEY_MRR,LENGTH(GRMain.AUTO_KEY_MRR)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND GRMain.Company_Code=ACM.Company_Code " & vbCrLf & " AND GRMain.Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND REF_TYPE IN ('P','I','C','1','2','3')"

        If chkShow.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            SqlStr = SqlStr & vbCrLf & " AND GRMain.SEND_GSTBILL_FLAG='Y'"
            If txtDateFrom.Text <> "" Then
                SqlStr = SqlStr & vbCrLf & " AND GRMain.SEND_GSTBILL_DATE>=TO_DATE('" & VB6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND GRMain.SEND_GSTBILL_DATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            End If
        Else
            SqlStr = SqlStr & vbCrLf & " AND GRMain.SEND_GSTBILL_FLAG='N'"
        End If

        SqlStr = SqlStr & vbCrLf & " ORDER BY GRMain.AUTO_KEY_MRR"

        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")


        cmdSave.Enabled = IIf(chkShow.CheckState = System.Windows.Forms.CheckState.Unchecked, False, True)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgInformation(Err.Description)
    End Sub
    Private Sub FormatSprdMain()

        With SprdMain
            .MaxCols = ColReceivedFlag
            .set_RowHeight(0, RowHeight * 1.5)
            .set_ColWidth(0, 4.5)

            .set_RowHeight(-1, RowHeight)
            .Row = -1

            .Col = ColMRRDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColMRRDate, 10)

            .Col = ColMRRNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .set_ColWidth(ColMRRNo, 10)

            .Col = ColPartyName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColPartyName, 35)

            .Col = ColBillNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBillNo, 12)

            .Col = ColBillDate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = 255
            .TypeEditMultiLine = True
            .set_ColWidth(ColBillDate, 10)

            .Col = ColReceivedFlag
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(ColReceivedFlag, 8)
            .Value = CStr(System.Windows.Forms.CheckState.Unchecked)

            MainClass.SetSpreadColor(SprdMain, -1)
            MainClass.ProtectCell(SprdMain, 1, .MaxRows, 1, ColPartyName)
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

            .Col = ColMRRNo
            .Text = "MRR No."

            .Col = ColMRRDate
            .Text = "MRR Date"

            .Col = ColPartyName
            .Text = "Party Name"

            .Col = ColBillNo
            .Text = "Bill No."

            .Col = ColBillDate
            .Text = "Bill Date"

            .Col = ColReceivedFlag
            .Text = "Send (Yes/No)"
        End With
    End Sub
    Private Sub frmGSTBillSendExcise_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
    End Sub

    Private Sub OptSelection_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptSelection.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptSelection.GetIndex(eventSender)
            Dim cntRow As Integer
            With SprdMain
                For cntRow = 1 To .MaxRows
                    .Row = cntRow
                    .Col = ColReceivedFlag
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
    Private Sub txtDatefrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateFrom.TextChanged
        cmdShow.Enabled = True
    End Sub

    Private Sub txtdateFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateFrom) = False Then
            txtDateFrom.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        '    If FYChk(CDate(TxtDateFrom.Text)) = False Then					
        '        TxtDateFrom.SetFocus					
        '        Cancel = True					
        '        Exit Sub					
        '    End If					
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        cmdShow.Enabled = True
    End Sub

    Private Sub txtdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If MainClass.ChkIsdateF(txtDateTo) = False Then
            txtDateTo.Focus()
            Cancel = True
            GoTo EventExitSub
        End If
        '    If FYChk(CDate(txtDateTo.Text)) = False Then					
        '        txtDateTo.SetFocus					
        '        Cancel = True					
        '        Exit Sub					
        '    End If					
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
