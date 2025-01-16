Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmUpdatePFESINo
    Inherits System.Windows.Forms.Form
    'Dim PvtDBCn As ADODB.Connection

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12

    Private Const ColEmpCode As Short = 1
    Private Const ColEmpName As Short = 2
    Private Const ColFName As Short = 3
    Private Const ColPFNo As Short = 4
    Private Const ColESI As Short = 5
    Private Const ColPANNo As Short = 6
    Private Const ColUANNo As Short = 7
    Private Const ColUpdated As Short = 8
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1

        With SprdMain
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)
            .MaxCols = ColUpdated

            .Row = 0
            SetColHeadings()
            .Row = Arow

            .Col = ColEmpCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColEmpCode, 10)
            .ColHidden = False

            .Col = ColEmpName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColEmpName, 25)
            .ColHidden = False

            .Col = ColFName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColFName, 25)
            .ColHidden = False

            .Col = ColPFNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColPFNo, 12)
            If lblCategory.Text = "P" Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = ColESI
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColESI, 12)
            If lblCategory.Text = "P" Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = ColPANNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColPANNo, 12)
            If lblCategory.Text = "A" Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = ColUANNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColPANNo, 12)
            If lblCategory.Text = "B" Then
                .ColHidden = False
            Else
                .ColHidden = True
            End If

            .Col = ColUpdated
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .set_ColWidth(ColUpdated, 40)
            .ColHidden = True

            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColEmpCode, ColFName)
        End With
        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        '    Resume
    End Sub
    Private Sub SetColHeadings()
        With SprdMain
            .Row = 0
            .Col = ColEmpCode
            .Text = "Emp Code"

            .Col = ColEmpName
            .Text = "Emp Name"

            .Col = ColFName
            .Text = "Father Name"

            .Col = ColPFNo
            .Text = "PF No"

            .Col = ColESI
            .Text = "ESI No"

            .Col = ColPANNo
            .Text = "PAN No"

            .Col = ColUANNo
            .Text = "UAN No"

            .set_RowHeight(0, 20)
        End With
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Call SaleReport("V")
        ReportForShow(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Call SaleReport("V")
        ReportForShow(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        On Error GoTo AddErr
        Dim cntRow As Integer

        If Trim(TxtCardNo.Text) = "" Then
            MsgBox("Emp Code is empty.", MsgBoxStyle.Information)
            Exit Sub
        Else
            If MainClass.ValidateWithMasterTable((TxtCardNo.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                MsgBox("Card No. Does Not Exist In Master.", MsgBoxStyle.Information)
                Exit Sub
            End If
        End If

        Clear1()
        Show1()
        Call FormatSprdMain(-1)

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColPFNo
                If Trim(.Text) <> "" Then
                    MainClass.ProtectCell(SprdMain, cntRow, cntRow, ColPFNo, ColPFNo)
                End If

                .Col = ColESI
                If Trim(.Text) <> "" Then
                    MainClass.ProtectCell(SprdMain, cntRow, cntRow, ColESI, ColESI)
                End If

                .Col = ColPANNo
                If Trim(.Text) <> "" Then
                    MainClass.ProtectCell(SprdMain, cntRow, cntRow, ColPANNo, ColPANNo)
                End If
            Next
        End With

        Exit Sub
AddErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.hide()
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Update1 = False Then GoTo ErrPart
        CmdSave.Enabled = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        ErrorMsg(CStr(Err.Number), Err.Description, MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmUpdatePFESINo_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1

        If FormActive = True Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If lblCategory.Text = "P" Then
            Me.Text = "Update -PF & ESI No"
        ElseIf lblCategory.Text = "A" Then
            Me.Text = "Update -PAN No"
        Else
            Me.Text = "Update -UAN No"
        End If

        If lblCategory.Text = "P" Then
            lblPF.Visible = True
            txtPFNo.Visible = True
            lblESI.Visible = True
            txtESINo.Visible = True
            FraPF.Visible = True
            FraESI.Visible = True
        Else
            lblPF.Visible = False
            txtPFNo.Visible = False
            lblESI.Visible = False
            txtESINo.Visible = False
            FraPF.Visible = False
            FraESI.Visible = False
        End If

        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmUpdatePFESINo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmUpdatePFESINo_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)

        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn


        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)

        MainClass.SetControlsColor(Me)

        ADDMode = False
        MODIFYMode = False

        '    OptProdCustWise(0).Value = True
        '   OptProdCustWise_Click (0)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Clear1()

        On Error GoTo ClearErr
        CmdSave.Enabled = True
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)
        Exit Sub
ClearErr:
        MsgBox(Err.Description)
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim SqlStr As String = ""
        Dim mCont_Code As String

        SqlStr = "SELECT EMP_CODE, EMP_NAME, EMP_FNAME, EMP_PF_ACNO, " & vbCrLf & " EMP_ESI_NO,EMP_PANNO,UID_NO, ''" & vbCrLf & " FROM PAY_EMPLOYEE_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        '    If chkAll.Value = vbUnchecked Then
        If Trim(TxtCardNo.Text) <> "" Then
            If MainClass.ValidateWithMasterTable(TxtCardNo.Text, "EMP_CODE", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                SqlStr = SqlStr & vbCrLf & "AND EMP_CODE='" & MainClass.AllowSingleQuote(TxtCardNo.Text) & "'"
            End If
        End If
        '    End If
        '
        If Trim(txtPFNo.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & "AND EMP_PF_ACNO='" & MainClass.AllowSingleQuote(txtPFNo.Text) & "'"
        End If

        If Trim(txtESINo.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & "AND EMP_ESI_NO='" & MainClass.AllowSingleQuote(txtESINo.Text) & "'"
        End If

        If optPFShow(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & "AND (EMP_PF_ACNO IS NULL OR EMP_PF_ACNO = '')"
        ElseIf optPFShow(2).Checked = True Then
            SqlStr = SqlStr & vbCrLf & "AND EMP_PF_ACNO IS NOT NULL"
        End If

        If optESIShow(1).Checked = True Then
            SqlStr = SqlStr & vbCrLf & "AND (EMP_ESI_NO IS NULL OR EMP_ESI_NO = '')"
        ElseIf optESIShow(2).Checked = True Then
            SqlStr = SqlStr & vbCrLf & "AND EMP_ESI_NO IS NOT NULL"
        End If

        If optOrderBy(0).Checked = True Then
            SqlStr = SqlStr & vbCrLf & "ORDER BY EMP_NAME"
        Else
            SqlStr = SqlStr & vbCrLf & "ORDER BY EMP_CODE"
        End If
        MainClass.AssignDataInSprd8(SqlStr, SprdMain, StrConn, "Y")

        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
        '    Resume
    End Sub
    Private Function Update1() As Boolean

        On Error GoTo UpdateErr
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mEmpCode As String
        Dim mPFNo As String
        Dim mESINo As String
        Dim mPANNo As String
        Dim mUANNo As String
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColUpdated
                If .Text <> "Y" Then GoTo LoopNext

                .Col = ColEmpCode
                mEmpCode = Trim(.Text)

                .Col = ColPFNo
                mPFNo = Trim(.Text)

                .Col = ColESI
                mESINo = Trim(.Text)

                .Col = ColPANNo
                mPANNo = Trim(.Text)

                .Col = ColUANNo
                mUANNo = Trim(.Text)

                SqlStr = " UPDATE PAY_EMPLOYEE_MST " & vbCrLf & " SET EMP_PF_ACNO='" & MainClass.AllowSingleQuote(mPFNo) & "', " & vbCrLf & " EMP_ESI_NO='" & MainClass.AllowSingleQuote(mESINo) & "', " & vbCrLf & " EMP_PANNO='" & MainClass.AllowSingleQuote(mPANNo) & "', " & vbCrLf & " UID_NO='" & MainClass.AllowSingleQuote(mUANNo) & "', " & vbCrLf & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " WHERE Company_Code= " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE= '" & mEmpCode & "' "


                PubDBCn.Execute(SqlStr)
LoopNext:
            Next
        End With
        Update1 = True
        PubDBCn.CommitTrans()
        Exit Function
UpdateErr:
        Update1 = False
        PubDBCn.RollbackTrans()
        If Err.Description <> "" Then
            MsgBox(Err.Description)
        End If
        ''Resume
    End Function
    Private Sub frmUpdatePFESINo_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        CmdSave.Enabled = True
        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
        SprdMain.Col = ColUpdated
        SprdMain.Row = SprdMain.ActiveRow
        SprdMain.Text = "Y"
    End Sub
    Private Sub ReportForShow(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mRPTName As String

        PubDBCn.Errors.Clear()


        'If TxtName.Text = "" Then Exit Sub

        SqlStr = "DELETE FROM Temp_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = ""

        If MainClass.FillPrintDummyDataFromSprd(SprdMain, 1, SprdMain.MaxRows, ColEmpCode, ColESI, PubDBCn) = False Then GoTo ERR1


        'Select Record for print...

        SqlStr = ""

        SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")

        mTitle = "PF & ESI List"

        mRPTName = "ContPF_ESIList.Rpt"

        Call ShowReport(SqlStr, mRPTName, Mode, mTitle, mSubTitle)

        Exit Sub
ERR1:
        If Err.Number = 32755 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        Else
            MsgInformation(Err.Description)
        End If
        ''Resume
    End Sub

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub
    Private Sub TxtCardNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtCardNo.DoubleClick
        cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub
    Private Sub TxtCardNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtCardNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdsearch, New System.EventArgs())
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub TxtCardNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtCardNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If TxtCardNo.Text = "" Then GoTo EventExitSub
        TxtCardNo.Text = VB6.Format(TxtCardNo.Text, "000000")
        If MainClass.ValidateWithMasterTable((TxtCardNo.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("CardNo. Does Not Exist In Master.", MsgBoxStyle.Information)
            Cancel = True
        Else
            TxtName.Text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        On Error GoTo SearchErr
        Dim SqlStr As String = ""

        SqlStr = "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            TxtCardNo.Text = AcName1
            TxtName.Text = AcName
            TxtCardNo_Validating(TxtCardNo, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
SearchErr:
        MsgBox(Err.Description)
    End Sub
End Class
