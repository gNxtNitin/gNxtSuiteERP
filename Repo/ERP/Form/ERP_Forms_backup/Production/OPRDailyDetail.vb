Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class FrmOPRDailyDetail
    Inherits System.Windows.Forms.Form
    'Dim PvtDBCn As ADODB.Connection	
    Dim FormLoaded As Boolean
    Private Const ConRowHeight As Short = 11

    Private Const ColOptional As Short = 1
    Private Const ColOPRCode As Short = 2
    Private Const ColOprDesc As Short = 3
    Private Const ColCycleTime As Short = 4

    Public Sub FormatSprdOPR(ByRef Arow As Integer)

        On Error GoTo ERR1
        With SprdOPR
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)

            .Col = ColOptional
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER

            .Col = ColOPRCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE

            .Col = ColOprDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE

            .Col = ColCycleTime
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 2
            .TypeFloatMax = CDbl("9999.99")
            .TypeFloatMin = CDbl("-9999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 7)

            MainClass.ProtectCell(SprdOPR, 1, .MaxRows, ColOprDesc, ColOprDesc)

        End With
        MainClass.SetSpreadColor(SprdOPR, Arow)
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        FrmBOMNew.lblDetail.Text = "False"
        FormLoaded = False
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub cmdOk_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOk.Click

        If FieldsVarification = False Then
            Exit Sub
        End If
        If UCase(LblAddMode.Text) = UCase("False") And UCase(LblModifyMode.Text) = UCase("False") Then Exit Sub
        If InsertIntoTemp_Table = True Then
            FrmBOMNew.lblDetail.Text = "True"
            Me.Hide()
            '' Unload Me	
        Else
            FrmBOMNew.lblDetail.Text = "False"
            MsgBox("Can Not Save Operation Detail", MsgBoxStyle.Critical)
            cmdOk.Enabled = True
        End If
        FormLoaded = False
    End Sub
    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim cntRow As Integer
        Dim cntRow2 As Integer
        Dim mOptional As String
        Dim mOPRCode As String

        FieldsVarification = False
        With SprdOPR
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColOPRCode
                mOPRCode = Trim(.Text)

                .Col = ColOptional
                mOptional = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                If mOptional = "Y" Then
                    For cntRow2 = cntRow + 1 To .MaxRows
                        .Row = cntRow2
                        .Col = ColOPRCode
                        mOPRCode = Trim(.Text)
                        If mOPRCode <> "" Then
                            .Col = ColOptional
                            mOptional = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")
                            If mOptional = "N" Then
                                MsgInformation("You can define Optional Operation at the last row, after Regular Operation.")
                                FieldsVarification = False
                                Exit Function
                            End If
                        End If
                    Next
                End If
            Next
        End With
        FieldsVarification = True
        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        FieldsVarification = False
    End Function
    Private Sub FrmOPRDailyDetail_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        If FormLoaded = True Then Exit Sub
        If UCase(LblAddMode.Text) = UCase("False") And UCase(LblModifyMode.Text) = UCase("False") Then
            cmdOk.Enabled = False
        Else
            cmdOk.Enabled = True
        End If

        Call ShowOPRDailyDetail()
        FormLoaded = True

    End Sub
    Private Sub FrmOPRDailyDetail_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo LoadPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection	
        'PvtDBCn.Open StrConn	

        MainClass.SetControlsColor(Me)

        Me.Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Me.Width)) / 2)
        Me.Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Me.Height)) / 2)
        FormLoaded = False
        MainClass.ClearGrid(SprdOPR)
        FormatSprdOPR(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
LoadPart:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub

    Private Sub FrmOPRDailyDetail_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormLoaded = False
        lblDeptCode.Text = ""
        lblProductCode.Text = ""
        LblAddMode.Text = ""
        LblModifyMode.Text = ""
        lblWEF.Text = ""

        PubDBCn.Cancel()

        'PvtDBCn.Close	
        'Set PvtDBCn = Nothing	
    End Sub
    Private Sub ShowOPRDailyDetail()

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mOPRDesc As String
        Dim mOPRCode As String
        Dim mIsOptional As String

        MainClass.ClearGrid(SprdOPR)
        FormatSprdOPR(-1)
        SqlStr = "SELECT * FROM TEMP_PRD_OPR_TRN " & vbCrLf _
            & " WHERE USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf _
            & " AND COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND PRODUCT_CODE ='" & MainClass.AllowSingleQuote(lblProductCode.Text) & "'" & vbCrLf _
            & " AND DEPT_CODE ='" & MainClass.AllowSingleQuote(lblDeptCode.Text) & "'" & vbCrLf _
            & " AND WEF =TO_DATE('" & VB6.Format(lblWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " ORDER BY OPR_SNO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            Do While Not RsTemp.EOF
                I = I + 1

                With SprdOPR
                    .Row = I

                    .Col = ColOptional
                    mIsOptional = IIf(IsDbNull(RsTemp.Fields("ISOPTIONAL").Value), "N", RsTemp.Fields("ISOPTIONAL").Value)
                    .Value = IIf(mIsOptional = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                    .Col = ColOPRCode
                    .Text = IIf(IsDbNull(RsTemp.Fields("OPR_CODE").Value), "", RsTemp.Fields("OPR_CODE").Value)
                    mOPRCode = IIf(IsDbNull(RsTemp.Fields("OPR_CODE").Value), "", RsTemp.Fields("OPR_CODE").Value)

                    If MainClass.ValidateWithMasterTable(mOPRCode, "OPR_CODE", "OPR_DESC", "PRD_OPR_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(lblDeptCode.Text) & "'") = True Then
                        mOPRDesc = MasterNo
                    Else
                        mOPRDesc = ""
                    End If
                    .Col = ColOprDesc
                    .Text = mOPRDesc

                    .Col = ColCycleTime
                    .Text = IIf(IsDBNull(RsTemp.Fields("CYCLE_TIME").Value), 0, RsTemp.Fields("CYCLE_TIME").Value)


                End With

                RsTemp.MoveNext()
                SprdOPR.MaxRows = I + 1
            Loop
        End If
        FormatSprdOPR(-1)
    End Sub
    Private Function InsertIntoTemp_Table() As Boolean

        On Error GoTo InsertErr
        Dim I As Integer
        Dim mOPRCode As String
        Dim SqlStr As String = ""
        Dim mIsOptional As String
        Dim mCycleTime As Double = 0

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_PRD_OPR_TRN " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf _
            & " AND PRODUCT_CODE ='" & MainClass.AllowSingleQuote(lblProductCode.Text) & "'" & vbCrLf _
            & " AND WEF =TO_DATE('" & VB6.Format(lblWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
            & " AND DEPT_CODE ='" & MainClass.AllowSingleQuote(lblDeptCode.Text) & "'"


        PubDBCn.Execute(SqlStr)

        With SprdOPR
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColOptional
                mIsOptional = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                .Col = ColOPRCode
                mOPRCode = .Text

                .Col = ColCycleTime
                mCycleTime = Val(.Text)

                SqlStr = ""
                If mOPRCode <> "" Then
                    SqlStr = "INSERT INTO TEMP_PRD_OPR_TRN " & vbCrLf _
                        & " ( USERID, COMPANY_CODE, PRODUCT_CODE, WEF," & vbCrLf _
                        & " DEPT_CODE, OPR_SNO, OPR_CODE,ISOPTIONAL, CYCLE_TIME  " & vbCrLf _
                        & " ) VALUES (" & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(lblProductCode.Text) & "', " & vbCrLf _
                        & " TO_DATE('" & VB6.Format(lblWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(lblDeptCode.Text) & "', " & vbCrLf _
                        & " " & I & ", '" & MainClass.AllowSingleQuote(mOPRCode) & "','" & mIsOptional & "', " & mCycleTime & ") "

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        PubDBCn.CommitTrans()
        InsertIntoTemp_Table = True
        Exit Function
InsertErr:
        'Resume	
        PubDBCn.RollbackTrans()
        InsertIntoTemp_Table = False
        MsgBox(Err.Description)
    End Function

    Private Sub SprdOPR_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdOPR.ClickEvent

        Dim SqlStr As String = ""

        SqlStr = " COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(lblDeptCode.text) & "'"

        If eventArgs.Row = 0 And eventArgs.Col = ColOPRCode Then
            With SprdOPR
                .Row = .ActiveRow
                .Col = ColOPRCode

                If MainClass.SearchGridMaster(.Text, "PRD_OPR_MST", "OPR_CODE", "OPR_DESC", "", "", SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColOPRCode
                    .Text = Trim(AcName)

                    .Col = ColOprDesc
                    .Text = Trim(AcName1)
                End If
                Call SprdOPR_LeaveCell(SprdOPR, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColOPRCode, .ActiveRow, ColOprDesc, .ActiveRow, False))
            End With
        End If

        If eventArgs.Row = 0 And eventArgs.Col = ColOprDesc Then
            With SprdOPR
                .Row = .ActiveRow
                .Col = ColOprDesc
                If MainClass.SearchGridMaster("", "PRD_OPR_MST", "OPR_DESC", "OPR_CODE", "", "", SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColOPRCode
                    .Text = Trim(AcName1)

                    .Col = ColOprDesc
                    .Text = Trim(AcName)
                End If
                Call SprdOPR_LeaveCell(SprdOPR, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColOPRCode, .ActiveRow, ColOprDesc, .ActiveRow, False))
            End With
        End If

        If eventArgs.Col = 0 And eventArgs.Row > 0 And (CBool(LblAddMode.Text) = True Or CBool(LblModifyMode.Text) = True) Then
            MainClass.DeleteSprdRow(SprdOPR, eventArgs.Row, ColOPRCode)
            '        MainClass.SaveStatus Me, LblAddMode.text, LblModifyMode.text	
        End If
    End Sub

    Private Sub SprdOPR_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdOPR.KeyPressEvent
        With SprdOPR
            If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then
                SprdOPR_LeaveCell(SprdOPR, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow + 1, False))
                '            SprdOPR_LeaveCell .ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False	
            End If

        End With
    End Sub

    Private Sub SprdOPR_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdOPR.KeyUpEvent

        Dim mCol As Short
        Dim xOPRCode As String

        mCol = SprdOPR.ActiveCol
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColOPRCode Then SprdOPR_ClickEvent(SprdOPR, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColOPRCode, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColOprDesc Then SprdOPR_ClickEvent(SprdOPR, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColOprDesc, 0))

        If eventArgs.KeyCode = System.Windows.Forms.Keys.Tab And mCol = ColOPRCode Then
            SprdOPR.Row = SprdOPR.ActiveRow

            SprdOPR.Col = ColOPRCode
            xOPRCode = Trim(SprdOPR.Text)
            If xOPRCode = "" Then Exit Sub
            If CheckOPRCode = True Then
                If CheckDuplicateOPRCode(xOPRCode) = False Then
                    MainClass.AddBlankSprdRow(SprdOPR, ColOPRCode, ConRowHeight)
                    FormatSprdOPR((SprdOPR.MaxRows))
                End If
            End If
        End If
        SprdOPR.Refresh()
    End Sub

    Private Sub SprdOPR_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdOPR.LeaveCell

        On Error GoTo ErrPart
        Dim xOPRCode As String
        If eventArgs.NewRow = -1 Then Exit Sub

        SprdOPR.Row = SprdOPR.ActiveRow
        SprdOPR.Col = ColOPRCode
        xOPRCode = Trim(SprdOPR.Text)
        If xOPRCode = "" Then Exit Sub

        Select Case eventArgs.col
            Case ColOPRCode
                SprdOPR.Row = SprdOPR.ActiveRow

                SprdOPR.Col = ColOPRCode
                xOPRCode = Trim(SprdOPR.Text)
                If xOPRCode = "" Then Exit Sub
                If CheckOPRCode() = True Then
                    If CheckDuplicateOPRCode(xOPRCode) = False Then
                        MainClass.AddBlankSprdRow(SprdOPR, ColOPRCode, ConRowHeight)
                        FormatSprdOPR((SprdOPR.MaxRows))
                    End If
                End If
        End Select
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function CheckOPRCode() As Boolean

        On Error GoTo CheckERR
        With SprdOPR
            .Row = .ActiveRow
            .Col = ColOPRCode
            If MainClass.ValidateWithMasterTable(.Text, "OPR_CODE", "OPR_DESC", "PRD_OPR_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(lblDeptCode.Text) & "'") = True Then
                .Row = .ActiveRow
                .Col = ColOprDesc
                .Text = CStr(MasterNo)
                CheckOPRCode = True
            Else
                .Col = ColOprDesc
                .Text = ""
                MsgInformation("Invalid Operation Code")
                MainClass.SetFocusToCell(SprdOPR, .ActiveRow, ColOprDesc)
                CheckOPRCode = False
            End If
        End With
        Exit Function
CheckERR:
        MsgBox(Err.Description)
    End Function
    Private Function CheckDuplicateOPRCode(ByRef pOPRCode As String) As Boolean

        On Error GoTo ERR1
        Dim I As Integer
        Dim mOprRept As Integer

        If pOPRCode = "" Then CheckDuplicateOPRCode = True : Exit Function
        With SprdOPR
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColOPRCode
                If UCase(Trim(.Text)) = UCase(Trim(pOPRCode)) Then
                    mOprRept = mOprRept + 1
                    If mOprRept > 1 Then
                        CheckDuplicateOPRCode = True
                        MsgInformation("Duplicate Operation")
                        MainClass.SetFocusToCell(SprdOPR, .ActiveRow, ColOPRCode)
                        Exit Function
                    End If
                End If
            Next
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Sub SprdOPR_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles SprdOPR.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        With SprdOPR
            .Row = .ActiveRow
            .Col = ColOPRCode
            If Trim(.Text) = "" Then GoTo EventExitSub
            If MainClass.ValidateWithMasterTable(.Text, "OPR_CODE", "OPR_DESC", "PRD_OPR_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(lblDeptCode.text) & "'") = False Then
                Cancel = True
            End If
        End With

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
