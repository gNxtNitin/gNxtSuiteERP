Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmBOMOutAlter
    Inherits System.Windows.Forms.Form
    'Dim PvtDBCn As ADODB.Connection
    Dim FormLoaded As Boolean
    Private Const ConRowHeight As Short = 11

    Private Const ColAlterItemCode As Short = 1
    Private Const ColAlterItemDesc As Short = 2
    Private Const ColAlterItemUOM As Short = 3
    Private Const ColAlterItemQty As Short = 4
    Private Const ColAlterScrapQty As Short = 5
    Private Const ColAlterStockType As Short = 6
    Private Const ColAlterQtyVar As Short = 7

    Public Sub FormatSprdBOM(ByRef Arow As Integer)

        On Error GoTo ERR1
        With SprdBOM
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)

            .Col = ColAlterItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 8)

            .Col = ColAlterItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 20)

            .Col = ColAlterItemUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 8)

            .Col = ColAlterItemQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.9999")
            .TypeFloatMin = CDbl("-999999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 10)

            .Col = ColAlterScrapQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 4
            .TypeFloatMax = CDbl("999999999.9999")
            .TypeFloatMin = CDbl("-999999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 10)

            .Col = ColAlterStockType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 4)

            .Col = ColAlterQtyVar
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatDecimalPlaces = 4
            .TypeFloatMax = CDbl("999999999.9999")
            .TypeFloatMin = CDbl("-999999999.9999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 8)

            MainClass.ProtectCell(SprdBOM, 1, .MaxRows, ColAlterItemDesc, ColAlterItemUOM)

        End With
        MainClass.SetSpreadColor(SprdBOM, Arow)
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        ConOutBOMDetail = False
        FormLoaded = False
        Me.Close()
    End Sub

    Private Sub cmdOk_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOk.Click
        If UCase(LblAddMode.Text) = UCase("False") And UCase(LblModifyMode.Text) = UCase("False") Then Exit Sub
        If InsertIntoTemp_Table = True Then
            ConOutBOMDetail = True
            Me.Hide()
            '' Unload Me
        Else
            ConOutBOMDetail = False
            MsgBox("Can Not Save BOM Alternate Item Deatil", MsgBoxStyle.Critical)
            CmdOK.Enabled = True
        End If
        FormLoaded = False
    End Sub

    Private Sub frmBOMOutAlter_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        If FormLoaded = True Then Exit Sub

        If UCase(LblAddMode.Text) = UCase("False") And UCase(LblModifyMode.Text) = UCase("False") Then
            CmdOK.Enabled = False
        Else
            CmdOK.Enabled = True
        End If

        Call ShowAlterDetail()
        FormLoaded = True

    End Sub

    Private Sub frmBOMOutAlter_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo LoadPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        MainClass.SetControlsColor(Me)

        Me.Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Me.Width)) / 2)
        Me.Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Me.Height)) / 2)
        FormLoaded = False
        MainClass.ClearGrid(SprdBOM)
        FormatSprdBOM(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
LoadPart:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub

    Private Sub frmBOMOutAlter_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormLoaded = False
        lblMKey.Text = ""
        LblItemCode.Text = ""
        lblSerialNo.Text = ""
        LblAddMode.Text = ""
        LblModifyMode.Text = ""
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
    End Sub

    Private Sub ShowAlterDetail()

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim pItemDesc As String = ""

        MainClass.ClearGrid(SprdBOM)
        FormatSprdBOM(-1)

        SqlStr = "SELECT * FROM TEMP_PRD_OUTBOM_ALTER_DET " & vbCrLf & " WHERE USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ITEM_CODE ='" & MainClass.AllowSingleQuote(UCase(LblItemCode.Text)) & "'" & vbCrLf & " ORDER BY ALTER_SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            Do While Not RsTemp.EOF
                I = I + 1

                With SprdBOM
                    .Row = I

                    .Col = ColAlterItemCode
                    .Text = IIf(IsDbNull(RsTemp.Fields("ALTER_ITEM_CODE").Value), "", RsTemp.Fields("ALTER_ITEM_CODE").Value)

                    .Col = ColAlterItemDesc
                    If MainClass.ValidateWithMasterTable(RsTemp.Fields("ALTER_ITEM_CODE"), "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        pItemDesc = MasterNo
                    End If
                    .Text = Trim(pItemDesc)

                    .Col = ColAlterItemUOM
                    .Text = IIf(IsDbNull(RsTemp.Fields("ALTER_ITEM_UOM").Value), "", RsTemp.Fields("ALTER_ITEM_UOM").Value)

                    .Col = ColAlterItemQty
                    .Text = CStr(Val(IIf(IsDbNull(RsTemp.Fields("ALTER_ITEM_QTY").Value), "", RsTemp.Fields("ALTER_ITEM_QTY").Value)))

                    .Col = ColAlterScrapQty
                    .Text = CStr(Val(IIf(IsDbNull(RsTemp.Fields("ALTER_SCRAP_QTY").Value), "", RsTemp.Fields("ALTER_SCRAP_QTY").Value)))

                    .Col = ColAlterQtyVar
                    .Text = CStr(Val(IIf(IsDbNull(RsTemp.Fields("ALTER_QTY_VAR").Value), "", RsTemp.Fields("ALTER_QTY_VAR").Value)))

                    .Col = ColAlterStockType
                    .Text = IIf(IsDbNull(RsTemp.Fields("ALTER_STOCK_TYPE").Value), "", RsTemp.Fields("ALTER_STOCK_TYPE").Value)

                End With

                RsTemp.MoveNext()
                SprdBOM.MaxRows = I + 1
            Loop
        End If
        FormatSprdBOM(-1)
    End Sub

    Private Function InsertIntoTemp_Table() As Boolean

        On Error GoTo InsertErr
        Dim I As Integer

        Dim mAlterItemCode As String
        Dim mAlterItemUOM As String
        Dim mAlterItemQty As Double
        Dim mAlterScrapQty As Double
        Dim mAlterQtyVar As Double
        Dim mAlterStockType As String
        Dim SqlStr As String = ""

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_PRD_OUTBOM_ALTER_DET " & vbCrLf & " WHERE USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ITEM_CODE ='" & MainClass.AllowSingleQuote(UCase(LblItemCode.Text)) & "'"

        PubDBCn.Execute(SqlStr)

        With SprdBOM
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColAlterItemCode
                mAlterItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColAlterItemUOM
                mAlterItemUOM = MainClass.AllowSingleQuote(.Text)

                .Col = ColAlterItemQty
                mAlterItemQty = Val(.Text)

                .Col = ColAlterScrapQty
                mAlterScrapQty = Val(.Text)

                .Col = ColAlterQtyVar
                mAlterQtyVar = Val(.Text)

                .Col = ColAlterStockType
                mAlterStockType = MainClass.AllowSingleQuote(.Text)

                SqlStr = ""
                If mAlterItemCode <> "" And mAlterItemQty > 0 Then
                    SqlStr = "INSERT INTO TEMP_PRD_OUTBOM_ALTER_DET (" & vbCrLf & " USERID, SERIAL_NO, ALTER_SERIAL_NO, " & vbCrLf & " COMPANY_CODE, ITEM_CODE, " & vbCrLf & " ALTER_ITEM_CODE, ALTER_ITEM_UOM, " & vbCrLf & " ALTER_STOCK_TYPE, ALTER_ITEM_QTY, " & vbCrLf & " ALTER_SCRAP_QTY, ALTER_QTY_VAR " & vbCrLf & " ) VALUES ( " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " " & Val(lblSerialNo.Text) & ", " & I & ", " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(LblItemCode.Text) & "', " & vbCrLf & " '" & mAlterItemCode & "', '" & mAlterItemUOM & "', " & vbCrLf & " '" & mAlterStockType & "', " & mAlterItemQty & "," & vbCrLf & " " & mAlterScrapQty & ", " & mAlterQtyVar & ")"
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

    Private Sub SprdBOM_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdBOM.ClickEvent

        Dim SqlStr As String = ""
        Dim mAlterItemDesc As String
        'Dim mDeleted As Boolean

        If eventArgs.Row = 0 And eventArgs.Col = ColAlterItemCode Then
            With SprdBOM
                '            SqlStr = "SELECT ITEM_CODE,ITEM_SHORT_DESC " & vbCrLf _
                ''                    & " FROM INV_ITEM_MST " & vbCrLf _
                ''                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND ITEM_STATUS = 'A' " & vbCrLf _
                ''                    & " ORDER BY ITEM_CODE "

                SqlStr = "SELECT A.ITEM_CODE, A.ITEM_SHORT_DESC " & vbCrLf & " FROM INV_ITEM_MST A, INV_ITEM_ALTER_DET B " & vbCrLf & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND A.ITEM_STATUS = 'A' " & vbCrLf & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf & " AND A.ITEM_CODE=B.ALTER_ITEM_CODE" & vbCrLf & " AND B.ITEM_CODE='" & MainClass.AllowSingleQuote((LblItemCode.Text)) & "'" & vbCrLf & " ORDER BY A.ITEM_CODE "

                eventArgs.row = .ActiveRow
                eventArgs.col = ColAlterItemCode
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    eventArgs.row = .ActiveRow

                    eventArgs.col = ColAlterItemCode
                    .Text = AcName

                    eventArgs.col = ColAlterItemDesc
                    .Text = AcName1
                End If
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColAlterItemDesc Then
            With SprdBOM
                '            SqlStr = "SELECT ITEM_SHORT_DESC,ITEM_CODE " & vbCrLf _
                ''                    & " FROM INV_ITEM_MST " & vbCrLf _
                ''                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND ITEM_STATUS = 'A' " & vbCrLf _
                ''                    & " ORDER BY ITEM_SHORT_DESC "

                SqlStr = "SELECT A.ITEM_SHORT_DESC,A.ITEM_CODE " & vbCrLf & " FROM INV_ITEM_MST A, INV_ITEM_ALTER_DET B " & vbCrLf & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND A.ITEM_STATUS = 'A' " & vbCrLf & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf & " AND A.ITEM_CODE=B.ALTER_ITEM_CODE" & vbCrLf & " AND B.ITEM_CODE='" & MainClass.AllowSingleQuote((lblItemCode.Text)) & "'" & vbCrLf & " ORDER BY A.ITEM_SHORT_DESC"

                eventArgs.row = .ActiveRow

                eventArgs.col = ColAlterItemDesc
                mAlterItemDesc = .Text

                .Text = ""
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    eventArgs.row = .ActiveRow

                    eventArgs.col = ColAlterItemDesc
                    .Text = AcName

                    eventArgs.col = ColAlterItemCode
                    .Text = AcName1
                Else
                    eventArgs.row = .ActiveRow

                    eventArgs.col = ColAlterItemDesc
                    .Text = mAlterItemDesc
                End If
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColAlterStockType Then
            With SprdBOM
                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value
                If MainClass.SearchGridMaster("", "INV_TYPE_MST", "STOCK_TYPE_DESC", "STOCK_TYPE_CODE", , , SqlStr) = True Then
                    eventArgs.row = .ActiveRow
                    eventArgs.col = ColAlterStockType
                    .Text = AcName1
                End If
            End With
        End If

        If eventArgs.Col = 0 And eventArgs.Row > 0 And (CBool(LblAddMode.Text) = True Or CBool(LblModifyMode.Text) = True) Then
            MainClass.DeleteSprdRow(SprdBOM, eventArgs.Row, ColAlterItemCode)
        End If
    End Sub

    Private Sub SprdBOM_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdBOM.LeaveCell

        On Error GoTo ErrPart
        Dim mAlterItemCode As String
        Dim mAlterStockType As String

        If eventArgs.NewRow = -1 Then Exit Sub
        SprdBOM.Row = eventArgs.Row
        SprdBOM.Col = ColAlterItemCode
        If Trim(SprdBOM.Text) = "" Then Exit Sub
        Select Case eventArgs.col
            Case ColAlterItemCode
                SprdBOM.Row = SprdBOM.ActiveRow
                SprdBOM.Col = ColAlterItemCode
                mAlterItemCode = Trim(SprdBOM.Text)
                If Trim(lblItemCode.Text) = Trim(SprdBOM.Text) Then
                    MainClass.SetFocusToCell(SprdBOM, SprdBOM.ActiveRow, ColAlterItemCode)
                Else
                    If CheckDuplicateItem(mAlterItemCode) = False Then
                        SprdBOM.Row = SprdBOM.ActiveRow
                        SprdBOM.Col = ColAlterItemCode
                        Call FillGridRow((SprdBOM.Text))
                    Else
                        MainClass.SetFocusToCell(SprdBOM, SprdBOM.ActiveRow, ColAlterItemCode)
                    End If
                End If
            Case ColAlterItemQty
                If CheckQty(SprdBOM, eventArgs.col, eventArgs.row) = True Then
                    MainClass.AddBlankSprdRow(SprdBOM, ColAlterItemCode, ConRowHeight)
                    FormatSprdBOM((SprdBOM.MaxRows))
                End If
            Case ColAlterStockType
                SprdBOM.Row = SprdBOM.ActiveRow
                SprdBOM.Col = ColAlterStockType
                mAlterStockType = Trim(SprdBOM.Text)
                If mAlterStockType <> "" Then
                    If MainClass.ValidateWithMasterTable(mAlterStockType, "STOCK_TYPE_CODE", "STOCK_TYPE_CODE", "INV_TYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                        MainClass.SetFocusToCell(SprdBOM, SprdBOM.ActiveRow, ColAlterStockType)
                    End If
                End If
        End Select

        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Function CheckDuplicateItem(ByRef pAlterItemCode As String) As Boolean
        On Error GoTo ERR1
        Dim I As Integer
        Dim mAlterItemRept As Integer

        If Trim(pAlterItemCode) = "" Then CheckDuplicateItem = False : Exit Function
        With SprdBOM
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColAlterItemCode
                If UCase(Trim(.Text)) = UCase(Trim(pAlterItemCode)) Then
                    mAlterItemRept = mAlterItemRept + 1
                    If mAlterItemRept > 1 Then
                        MsgInformation("Duplication Item.")
                        CheckDuplicateItem = True
                        Exit Function
                    End If
                End If
            Next
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Sub FillGridRow(ByRef mAlterItemCode As String)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim mSizeCode As Integer

        If Trim(mAlterItemCode) = "" Then Exit Sub
        '    SqlStr = " SELECT ITEM_CODE,ITEM_SHORT_DESC,ISSUE_UOM " _
        ''            & " FROM INV_ITEM_MST " _
        ''            & " WHERE ITEM_CODE='" & MainClass.AllowSingleQuote(mAlterItemCode) & "' " _
        ''            & " AND COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "  "

        ''AND ITEM_STATUS = 'A'

        SqlStr = "SELECT A.ITEM_SHORT_DESC, A.ISSUE_UOM  " & vbCrLf & " FROM INV_ITEM_MST A, INV_ITEM_ALTER_DET B " & vbCrLf & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf & " AND A.ITEM_CODE=B.ALTER_ITEM_CODE" & vbCrLf & " AND B.ALTER_ITEM_CODE='" & MainClass.AllowSingleQuote(mAlterItemCode) & "'" & vbCrLf & " AND B.ITEM_CODE='" & MainClass.AllowSingleQuote((LblItemCode.Text)) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If Not RsMisc.EOF Then
            SprdBOM.Row = SprdBOM.ActiveRow
            With RsMisc
                SprdBOM.Col = ColAlterItemDesc
                SprdBOM.Text = IIf(IsDbNull(.Fields("Item_Short_Desc").Value), "", .Fields("Item_Short_Desc").Value)

                SprdBOM.Col = ColAlterItemUOM
                SprdBOM.Text = IIf(IsDbNull(.Fields("ISSUE_UOM").Value), "", .Fields("ISSUE_UOM").Value)
            End With
        Else
            MainClass.SetFocusToCell(SprdBOM, SprdBOM.ActiveRow, ColAlterItemCode)
        End If
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub

    Private Function CheckQty(ByRef pSprd As Object, ByVal Col As Integer, ByVal Row As Integer) As Boolean

        On Error GoTo ERR1
        With pSprd
            .Row = Row
            .Col = Col
            If Val(.Text) > 0 Then
                CheckQty = True
            Else
                MainClass.SetFocusToCell(pSprd, Row, Col)
            End If
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
End Class
