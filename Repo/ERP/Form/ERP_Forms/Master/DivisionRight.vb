Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmDivisionRights
   Inherits System.Windows.Forms.Form
   Dim RsDIVISIONRights As ADODB.Recordset = Nothing
    Dim SqlStr As String = ""

    Private Const ColDivCode As Short = 1
    Private Const ColDivName As Short = 2
    Private Const ColCanWork As Short = 3

    Private Const ConRowHeight As Short = 13
    Private Sub Show1()

        On Error GoTo Errshow1
        Dim cntRow As Short
        Dim mDivisionCode As String
        'Dim RsFields As OraFields					


        SqlStr = ""
        SqlStr = " SELECT IH.DIV_CODE, DIVISION.DIV_DESC, IH.Rights " & vbCrLf _
           & " FROM GEN_DIVISIONRIGHT_MST IH, INV_DIVISION_MST DIVISION " & vbCrLf _
           & " WHERE IH.COMPANY_CODE= DIVISION.COMPANY_CODE " & vbCrLf _
           & " AND IH.DIV_CODE = DIVISION.DIV_CODE " & vbCrLf _
           & " AND IH.USER_ID='" & UCase(txtUserId.Text) & "'" & vbCrLf _
           & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
           & " ORDER BY DIVISION.DIV_DESC"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDIVISIONRights, ADODB.LockTypeEnum.adLockOptimistic)

        '    Set RsFields = RsDIVISIONRights.Fields					

        If RsDIVISIONRights.EOF = False Then
            RsDIVISIONRights.MoveFirst()
            Do While Not RsDIVISIONRights.EOF
                mDivisionCode = IIf(IsDBNull(RsDIVISIONRights.Fields("DIV_CODE").Value), "", RsDIVISIONRights.Fields("DIV_CODE").Value)

                For cntRow = 1 To SprdMain.MaxRows
                    SprdMain.Row = cntRow
                    SprdMain.Col = ColDivCode
                    If Val(SprdMain.Text) = IIf(IsDBNull(RsDIVISIONRights.Fields("DIV_CODE").Value), -1, RsDIVISIONRights.Fields("DIV_CODE").Value) Then

                        SprdMain.Col = ColCanWork
                        If IIf(IsDBNull(RsDIVISIONRights.Fields("Rights").Value), "N", RsDIVISIONRights.Fields("Rights").Value) = "N" Then
                            SprdMain.Value = CStr(System.Windows.Forms.CheckState.Unchecked)
                        Else
                            SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked)
                        End If
                        Exit For
                    End If
                Next
                RsDIVISIONRights.MoveNext()
                '            k = k + 1			

            Loop
        End If
        Exit Sub
Errshow1:
        MsgBox(Err.Description)

    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)


        On Error GoTo ErrPart

        With SprdMain
            .set_RowHeight(Arow, ConRowHeight)
            .Row = Arow

            .Col = ColDivCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditLen = 60
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(.Col, 6)

            .Col = ColDivName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditLen = 60
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .set_ColWidth(.Col, 25)

            .Col = ColCanWork
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(.Col, 8.5)
            .Value = CStr(System.Windows.Forms.CheckState.Unchecked)

            MainClass.ProtectCell(SprdMain, Arow, .MaxRows, ColDivCode, ColDivName)
            MainClass.SetSpreadColor(SprdMain, Arow)
        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub Clear1()
        MainClass.ClearGrid(SprdMain)
        FormatSprdMain(-1)
        Call AutoCompleteSearch("ATH_PASSWORD_MST", "USER_ID", "", txtUserId)
    End Sub
    Private Sub FillDIVISION()

        On Error GoTo ErrFillMenu
        Dim RsDIVISION As ADODB.Recordset = Nothing
        Dim mRow As Integer
        Dim SqlStr As String = ""

        mRow = 1
        SqlStr = " SELECT TO_CHAR(DIV_CODE) DIV_CODE,DIV_DESC " & vbCrLf _
           & " FROM INV_DIVISION_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
           & " ORDER BY DIV_DESC "
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDIVISION, ADODB.LockTypeEnum.adLockReadOnly)

        If Not RsDIVISION.EOF Then
            'SprdMain.MaxRows = RsModules.RecordCount				
            FormatSprdMain(-1)
            Do While Not RsDIVISION.EOF
                SprdMain.Row = mRow

                SprdMain.Col = ColDivCode
                SprdMain.Text = RsDIVISION.Fields("DIV_CODE").Value

                SprdMain.Col = ColDivName
                SprdMain.Text = RsDIVISION.Fields("DIV_DESC").Value
                RsDIVISION.MoveNext()
                If RsDIVISION.EOF = False Then
                    mRow = mRow + 1
                    SprdMain.MaxRows = mRow
                End If
            Loop
            FormatSprdMain(-1)
        End If
        Exit Sub
ErrFillMenu:
        MsgBox(Err.Description)
    End Sub
    Private Sub txtUserId_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtUserId.TextChanged
        CmdSave.Enabled = True
    End Sub
    Private Sub TxtUserID_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtUserId.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        LblUserName.Text = ""
        If txtUserId.Text <> "" Then
            If MainClass.ValidateWithMasterTable(txtUserId.Text, "USER_ID", "USER_ID", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtUserId.Text = MasterNo
            Else
                MsgInformation("Invalid User ID")
                Cancel = True
            End If

            If MainClass.ValidateWithMasterTable(txtUserId.Text, "USER_ID", "EMP_NAME", "ATH_PASSWORD_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                LblUserName.Text = MasterNo
            End If
            Clear1()
            FillDIVISION()
            Show1()
        End If
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrSave
        Dim cntRow As Short
        Dim mDivisionCode As Integer
        Dim mRights As String


        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If Trim(txtUserId.Text) = "" Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        PubDBCn.Execute("DELETE FROM GEN_DIVISIONRIGHT_MST " & vbCrLf _
                        & " WHERE USER_ID='" & UCase(txtUserId.Text) & "'" & vbCrLf _
                        & " AND COMPANY_CODE=" & CStr(RsCompany.Fields("COMPANY_CODE").Value) & "")

        For cntRow = 1 To SprdMain.MaxRows

            SprdMain.Row = cntRow

            SprdMain.Col = ColDivCode
            mDivisionCode = Val(SprdMain.Text)

            SprdMain.Col = ColCanWork
            mRights = IIf(SprdMain.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

            ''14-06-2006				
            '        mRights = IIf(UCase(txtUserId.Text) = "SUPER", "Y", mRights)				

            SqlStr = ""
            SqlStr = "INSERT INTO GEN_DIVISIONRIGHT_MST (" & vbCrLf _
               & " USER_ID, COMPANY_CODE, DIV_CODE, RIGHTS " & vbCrLf _
               & " ) VALUES ( " & vbCrLf _
               & " '" & txtUserId.Text & "', " & vbCrLf _
               & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
               & " " & mDivisionCode & ", '" & mRights & "')"
            PubDBCn.Execute(SqlStr)
LabelSave:
        Next
        PubDBCn.CommitTrans()
        Show1()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        CmdSave.Enabled = False
        Exit Sub
ErrSave:
        MsgBox(Err.Description)
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        'Resume					
    End Sub
    Private Sub frmDivisionRights_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Call SetMainFormCordinate(Me)
        MainClass.SetControlsColor(Me)

        Clear1()
        lblCompanyName.Text = RsCompany.Fields("Company_Name").Value
        MainClass.SetControlsColor(Me)
    End Sub
    Private Sub frmDivisionRights_FormClosing(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim UnloadMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
        RsDIVISIONRights = Nothing
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub frmDivisionRights_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub OptRights_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OptRights.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = OptRights.GetIndex(eventSender)
            Static I, j As Integer
            'Static x As Integer
            j = SprdMain.MaxRows
            For I = 1 To j
                SprdMain.Row = I
                SprdMain.Col = ColCanWork
                SprdMain.Text = IIf(Index = 0, System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            Next I
            CmdSave.Enabled = True
        End If
    End Sub
    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change
        CmdSave.Enabled = True
    End Sub
    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent
        CmdSave.Enabled = True
    End Sub
    Private Sub UserIDSearch()
        Dim SqlStr As String = ""

        SqlStr = ""
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster(txtUserId.Text, "ATH_PASSWORD_MST", "USER_ID", "EMP_NAME", , , SqlStr) = True Then
            txtUserId.Text = AcName
            TxtUserID_Validating(txtUserId, New System.ComponentModel.CancelEventArgs(False))
            If SprdMain.Enabled = True Then SprdMain.Focus()
            'End If							
        End If
    End Sub
   Private Sub txtUserId_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtUserId.DoubleClick
      UserIDSearch()
   End Sub
   Private Sub txtUserId_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtUserId.KeyPress
      Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
      KeyAscii = MainClass.UpperCase(KeyAscii, txtUserId.Text)
      eventArgs.KeyChar = Chr(KeyAscii)
      If KeyAscii = 0 Then
         eventArgs.Handled = True
      End If
   End Sub
   Private Sub TxtUserID_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtUserId.KeyUp
      Dim KeyCode As Short = eventArgs.KeyCode
      Dim Shift As Short = eventArgs.KeyData \ &H10000
      If KeyCode = System.Windows.Forms.Keys.F1 Then UserIDSearch()
   End Sub
End Class
