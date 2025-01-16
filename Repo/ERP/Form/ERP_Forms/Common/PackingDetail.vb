Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class FrmPackingDetail
    Inherits System.Windows.Forms.Form
    'Dim PvtDBCn As ADODB.Connection
    Dim FormLoaded As Boolean
    Private Const ConRowHeight As Short = 11

    Private Const ColOPRCode As Short = 1
    Private Const ColOprName As Short = 2
    Private Const ColPackQty As Short = 3

    Public Sub FormatSprdOPR(ByRef Arow As Integer)

        On Error GoTo ERR1
        With SprdOPR
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)

            .Col = ColOPRCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE

            .Col = ColOprName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE

            .Col = ColPackQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.9999")
            .TypeFloatMin = CDbl("-999999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 9)

            MainClass.ProtectCell(SprdOPR, 1, .MaxRows, ColOprName, ColOprName)

        End With
        MainClass.SetSpreadColor(SprdOPR, Arow)
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        FrmPDI.lblDetail.Text = "False"
        FormLoaded = False
        Me.Close()
    End Sub
    Private Sub cmdOk_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOk.Click

        If UCase(LblAddMode.Text) = UCase("False") And UCase(LblModifyMode.Text) = UCase("False") Then Exit Sub

        If Val(lblPDIQty.Text) <> Val(lblTotQty.Text) Then
            MsgBox("PDI OK Qty Not Equal to Packing Qty.", MsgBoxStyle.Critical)
            cmdOk.Enabled = True
            Exit Sub
        End If
        If InsertIntoTemp_Table = True Then
            FrmPDI.lblDetail.Text = "True"
            Me.Hide()
            '' Unload Me
        Else
            FrmPDI.lblDetail.Text = "False"
            MsgBox("Can Not Save Packing Detail", MsgBoxStyle.Critical)
            cmdOk.Enabled = True
        End If
        FormLoaded = False
    End Sub
    Private Sub FrmPackingDetail_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        If FormLoaded = True Then Exit Sub
        If UCase(LblAddMode.Text) = UCase("False") And UCase(LblModifyMode.Text) = UCase("False") Then
            cmdOk.Enabled = False
        Else
            cmdOk.Enabled = True
        End If

        Call ShowPackingDetails()
        FormLoaded = True

    End Sub
    Private Sub FrmPackingDetail_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo LoadPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        MainClass.SetControlsColor(Me)

        Me.Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Me.Width)) / 2)
        Me.Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Me.Height)) / 2)
        lblTotQty.Text = "0.00"
        FormLoaded = False
        MainClass.ClearGrid(SprdOPR)
        FormatSprdOPR(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
LoadPart:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub

    Private Sub FrmPackingDetail_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormLoaded = False
        lblDeptCode.Text = ""
        lblProductCode.Text = ""
        LblAddMode.Text = ""
        LblModifyMode.Text = ""
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
    End Sub
    Private Sub ShowPackingDetails()

        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mOPRDesc As String
        Dim mOPRCode As String

        MainClass.ClearGrid(SprdOPR)
        FormatSprdOPR(-1)
        SqlStr = "SELECT * FROM TEMP_PRD_PACKING_TRN " & vbCrLf & " WHERE USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf & " AND COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE ='" & MainClass.AllowSingleQuote(lblProductCode.Text) & "'" & vbCrLf & " AND DEPT_CODE ='" & MainClass.AllowSingleQuote(lblDeptCode.Text) & "'" & vbCrLf & " ORDER BY SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            Do While Not RsTemp.EOF
                I = I + 1

                With SprdOPR
                    .Row = I

                    .Col = ColOPRCode
                    .Text = IIf(IsDbNull(RsTemp.Fields("EMP_CODE").Value), "", RsTemp.Fields("EMP_CODE").Value)
                    mOPRCode = IIf(IsDbNull(RsTemp.Fields("EMP_CODE").Value), "", RsTemp.Fields("EMP_CODE").Value)

                    If MainClass.ValidateWithMasterTable(mOPRCode, "EMP_CODE", "EMP_NAME", "PAY_CONT_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mOPRDesc = MasterNo
                    Else
                        mOPRDesc = ""
                    End If
                    .Col = ColOprName
                    .Text = mOPRDesc

                    .Col = ColPackQty
                    .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("PACK_QTY").Value), 0, RsTemp.Fields("PACK_QTY").Value), "0.000")
                End With

                RsTemp.MoveNext()
                SprdOPR.MaxRows = I + 1
            Loop
        End If
        Call CalcPackingQty()
        FormatSprdOPR(-1)
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function InsertIntoTemp_Table() As Boolean

        On Error GoTo InsertErr
        Dim I As Integer
        Dim mOPRCode As String
        Dim SqlStr As String = ""
        Dim mPackQty As Double

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_PRD_PACKING_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf & " AND PRODUCT_CODE ='" & MainClass.AllowSingleQuote((lblProductCode.Text)) & "'" & vbCrLf & " AND DEPT_CODE ='" & MainClass.AllowSingleQuote((lblDeptCode.Text)) & "'"

        PubDBCn.Execute(SqlStr)

        With SprdOPR
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColOPRCode
                mOPRCode = .Text

                .Col = ColPackQty
                mPackQty = CDbl(VB6.Format(Val(.Text), "0.000"))

                SqlStr = ""
                If mOPRCode <> "" Then
                    SqlStr = "INSERT INTO TEMP_PRD_PACKING_TRN " & " ( USERID, COMPANY_CODE, PRODUCT_CODE, MAIN_PRODUCT_CODE," & vbCrLf & " DEPT_CODE, SERIAL_NO, EMP_CODE,PACK_QTY  " & vbCrLf & " ) VALUES (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(lblProductCode.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(lblMainProductCode.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(lblDeptCode.Text) & "', " & vbCrLf & " " & I & ", '" & MainClass.AllowSingleQuote(mOPRCode) & "'," & mPackQty & ") "

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

        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DEPT_CODE='" & MainClass.AllowSingleQuote((lblDeptCode.Text)) & "'"
        If eventArgs.Row = 0 And eventArgs.Col = ColOPRCode Then
            With SprdOPR
                eventArgs.Row = .ActiveRow
                eventArgs.Col = ColOPRCode
                SqlStr = " SELECT EMP_NAME, EMP_CODE " & vbCrLf & " FROM PAY_CONT_EMPLOYEE_MST" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_DEPT_CODE='" & MainClass.AllowSingleQuote((lblDeptCode.Text)) & "'"


                SqlStr = SqlStr & vbCrLf & " AND (EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE='' OR EMP_LEAVE_DATE>=TO_DATE('" & VB6.Format(lblRefDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
                If UCase(LblAddMode.Text) = "TRUE" Then
                    SqlStr = SqlStr & vbCrLf & " AND EMP_CAT='P'"
                End If
                SqlStr = SqlStr & vbCrLf & " ORDER BY EMP_NAME"

                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    eventArgs.Row = .ActiveRow

                    eventArgs.Col = ColOPRCode
                    .Text = Trim(AcName1)

                    eventArgs.Col = ColOprName
                    .Text = Trim(AcName)
                End If
                '            Call SprdMain_LeaveCell(ColOPRCode, .ActiveRow, ColOPRCode, .ActiveRow, False)
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

        Select Case eventArgs.Col
            Case ColOPRCode
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
        End Select
        Call CalcPackingQty()
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function CheckOPRCode() As Boolean

        On Error GoTo CheckERR
        Dim SqlStr As String = ""

        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_DEPT_CODE='" & MainClass.AllowSingleQuote((lblDeptCode.Text)) & "'"

        SqlStr = SqlStr & vbCrLf & " AND (EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE='' OR EMP_LEAVE_DATE>=TO_DATE('" & VB6.Format(lblRefDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
        If UCase(LblAddMode.Text) = "TRUE" Then
            SqlStr = SqlStr & vbCrLf & " AND EMP_CAT='P'"
        End If

        With SprdOPR
            .Row = .ActiveRow
            .Col = ColOPRCode
            If MainClass.ValidateWithMasterTable(.Text, "EMP_CODE", "EMP_NAME", "PAY_CONT_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = True Then
                .Row = .ActiveRow
                .Col = ColOprName
                .Text = CStr(MasterNo)
                CheckOPRCode = True
            Else
                .Col = ColOprName
                .Text = ""
                MsgInformation("Invalid Operator Code")
                MainClass.SetFocusToCell(SprdOPR, .ActiveRow, ColOprName)
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
                        MsgInformation("Duplicate Operator")
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
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_DEPT_CODE='" & MainClass.AllowSingleQuote((lblDeptCode.Text)) & "'"

        SqlStr = SqlStr & vbCrLf & " AND (EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE='' OR EMP_LEAVE_DATE>=TO_DATE('" & VB6.Format(lblRefDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"
        If UCase(LblAddMode.Text) = "TRUE" Then
            SqlStr = SqlStr & vbCrLf & " AND EMP_CAT='P'"
        End If

        With SprdOPR
            .Row = .ActiveRow
            .Col = ColOPRCode
            If Trim(.Text) = "" Then GoTo EventExitSub
            If MainClass.ValidateWithMasterTable(.Text, "EMP_CODE", "EMP_NAME", "PAY_CONT_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
                Cancel = True
            End If
        End With
        Call CalcPackingQty()
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub CalcPackingQty()
        On Error GoTo ERR1
        Dim mPackingQty As Double
        Dim I As Double

        mPackingQty = 0
        With SprdOPR
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColOPRCode
                If Trim(.Text) <> "" Then
                    .Col = ColPackQty
                    mPackingQty = mPackingQty + Val(.Text)
                End If
            Next
        End With
        lblTotQty.Text = VB6.Format(mPackingQty, "0.00")
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
End Class
