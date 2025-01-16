Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class FrmBOMAlternate
    Inherits System.Windows.Forms.Form
    'Dim PvtDBCn As ADODB.Connection	
    Dim FormLoaded As Boolean
    Private Const ConRowHeight As Short = 11

    Private Const ColItemCode As Short = 1
    Private Const ColItemName As Short = 2
    Private Const colStdQty As Short = 3
    Private Const ColScrapQty As Short = 4
    Private Const ColStockType As Short = 5
    Private Const ColWtVar As Short = 6

    Public Sub FormatSprdBOM(ByRef Arow As Integer)

        On Error GoTo ERR1
        With SprdBOM
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 8)

            .Col = ColItemName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 20)

            .Col = colStdQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 10)

            .Col = ColScrapQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 10)

            .Col = ColStockType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 4)

            .Col = ColWtVar
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 8)

            MainClass.ProtectCell(SprdBOM, 1, .MaxRows, ColItemName, ColItemName)

        End With
        MainClass.SetSpreadColor(SprdBOM, Arow)
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        ConBOMDetail = False
        FormLoaded = False
        Me.hide()
    End Sub
    Private Sub cmdOk_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOk.Click
        If UCase(LblAddMode.Text) = UCase("False") And UCase(LblModifyMode.Text) = UCase("False") Then Exit Sub
        If InsertIntoTemp_Table = True Then
            If SprdBOM.MaxRows > 1 Then
                If MainClass.ValidDataInGrid(SprdBOM, ColStockType, "S", "Please Check Stock Type") = False Then
                    Exit Sub
                End If
            End If
            ConBOMDetail = True
            Me.Hide()
            '' Unload Me	
        Else
            ConBOMDetail = False
            MsgBox("Can Not Save BOM Alternate Item Deatil", MsgBoxStyle.Critical)
            cmdOk.Enabled = True
        End If
        FormLoaded = False
    End Sub
    Private Sub FrmBOMAlternate_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        If FormLoaded = True Then Exit Sub

        If UCase(LblAddMode.Text) = UCase("False") And UCase(LblModifyMode.Text) = UCase("False") Then
            cmdOk.Enabled = False
        Else
            cmdOk.Enabled = True
        End If

        Call ShowDSDailyDetail()
        FormLoaded = True

    End Sub
    Private Sub FrmBOMAlternate_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
    Private Sub FrmBOMAlternate_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormLoaded = False
        lblMKey.Text = ""
        lblDeptCode.Text = ""
        lblMainItemCode.Text = ""
        LblMainItemSNO.Text = ""
        LblAddMode.Text = ""
        LblModifyMode.Text = ""
        PubDBCn.Cancel()
        'PvtDBCn.Close	
        'Set PvtDBCn = Nothing	
    End Sub
    Private Sub ShowDSDailyDetail()

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim pItemDesc As String

        MainClass.ClearGrid(SprdBOM)
        FormatSprdBOM(-1)


        SqlStr = "SELECT * FROM TEMP_PRD_BOM_ALTER_DET " & vbCrLf & " WHERE USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND DEPT_CODE ='" & MainClass.AllowSingleQuote(UCase(lblDeptCode.Text)) & "'" & vbCrLf & " AND MAINITEM_CODE ='" & MainClass.AllowSingleQuote(UCase(lblMainItemCode.Text)) & "'" & vbCrLf & " ORDER BY SUBROWNO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            Do While Not RsTemp.EOF
                I = I + 1

                With SprdBOM
                    .Row = I

                    .Col = ColItemCode
                    .Text = IIf(IsDbNull(RsTemp.Fields("ALTER_RM_CODE").Value), "", RsTemp.Fields("ALTER_RM_CODE").Value)

                    .Col = ColItemName
                    If MainClass.ValidateWithMasterTable(RsTemp.Fields("ALTER_RM_CODE").Value, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        pItemDesc = MasterNo
                    End If
                    .Text = Trim(pItemDesc)

                    .Col = colStdQty
                    .Text = CStr(Val(IIf(IsDbNull(RsTemp.Fields("ALTER_STD_QTY").Value), "", RsTemp.Fields("ALTER_STD_QTY").Value)))

                    .Col = ColScrapQty
                    .Text = CStr(Val(IIf(IsDbNull(RsTemp.Fields("ALETRSCRAP").Value), "", RsTemp.Fields("ALETRSCRAP").Value)))

                    .Col = ColWtVar
                    .Text = CStr(Val(IIf(IsDbNull(RsTemp.Fields("ALETR_WT_VAR").Value), "", RsTemp.Fields("ALETR_WT_VAR").Value)))

                    .Col = ColStockType
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

        Dim mStdQty As Double
        Dim mScrapQty As Double
        Dim SqlStr As String = ""
        Dim mItemCode As String
        Dim mWtVar As Double
        Dim mStockType As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_PRD_BOM_ALTER_DET " & vbCrLf & " WHERE USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND DEPT_CODE ='" & MainClass.AllowSingleQuote(UCase(lblDeptCode.Text)) & "'" & vbCrLf & " AND MAINITEM_CODE ='" & MainClass.AllowSingleQuote(UCase(lblMainItemCode.Text)) & "'"

        PubDBCn.Execute(SqlStr)

        With SprdBOM
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColItemCode
                mItemCode = .Text

                .Col = colStdQty
                mStdQty = Val(.Text)

                .Col = ColScrapQty
                mScrapQty = Val(.Text)

                .Col = ColWtVar
                mWtVar = Val(.Text)

                .Col = ColStockType
                mStockType = Trim(.Text)

                SqlStr = ""
                If mItemCode <> "" And mStdQty > 0 Then
                    SqlStr = "INSERT INTO TEMP_PRD_BOM_ALTER_DET (" & vbCrLf & " USERID, COMPANY_CODE, " & vbCrLf & " DEPT_CODE, MAINITEM_CODE, " & vbCrLf & " MAINSUBROWNO, SUBROWNO, " & vbCrLf & " ALTER_RM_CODE, ALTER_STD_QTY, " & vbCrLf & " ALETRSCRAP, ALETR_WT_VAR, ALTER_STOCK_TYPE) VALUES ( " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(lblDeptCode.Text) & "', '" & MainClass.AllowSingleQuote(lblMainItemCode.Text) & "', " & vbCrLf & " " & Val(LblMainItemSNO.Text) & ", " & I & ", " & vbCrLf & " '" & MainClass.AllowSingleQuote(mItemCode) & "', " & mStdQty & "," & vbCrLf & " " & mScrapQty & ", " & mWtVar & ", '" & mStockType & "')"
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
        Dim mRMName As String
        'Dim mDeleted As Boolean	

        If eventArgs.Row = 0 And eventArgs.Col = ColItemCode Then
            With SprdBOM
                SqlStr = "SELECT A.ITEM_CODE, A.ITEM_SHORT_DESC " & vbCrLf _
                    & " FROM INV_ITEM_MST A, INV_ITEM_ALTER_DET B " & vbCrLf _
                    & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND A.ITEM_STATUS = 'A' " & vbCrLf _
                    & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf _
                    & " AND A.ITEM_CODE=B.ALTER_ITEM_CODE" & vbCrLf _
                    & " AND B.ITEM_CODE='" & MainClass.AllowSingleQuote(lblMainItemCode.Text) & "'" & vbCrLf _
                    & " ORDER BY A.ITEM_CODE "
                .Row = .ActiveRow
                .Col = ColItemCode
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColItemCode
                    .Text = AcName

                    .Col = ColItemName
                    .Text = AcName1
                End If
            End With
        End If

        If eventArgs.Row = 0 And eventArgs.Col = ColItemName Then
            With SprdBOM
                SqlStr = "SELECT A.ITEM_SHORT_DESC,A.ITEM_CODE " & vbCrLf _
                    & " FROM INV_ITEM_MST A, INV_ITEM_ALTER_DET B " & vbCrLf _
                    & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND A.ITEM_STATUS = 'A' " & vbCrLf _
                    & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf _
                    & " AND A.ITEM_CODE=B.ALTER_ITEM_CODE" & vbCrLf _
                    & " AND B.ITEM_CODE='" & MainClass.AllowSingleQuote(lblMainItemCode.Text) & "'" & vbCrLf _
                    & " ORDER BY A.ITEM_SHORT_DESC"

                .Row = .ActiveRow

                .Col = ColItemName
                mRMName = .Text

                .Text = ""
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColItemName
                    .Text = AcName

                    .Col = ColItemCode
                    .Text = AcName1
                Else
                    .Row = .ActiveRow

                    .Col = ColItemName
                    .Text = mRMName
                End If
            End With
        End If

        If eventArgs.Row = 0 And eventArgs.Col = ColStockType Then
            With SprdBOM
                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value
                If MainClass.SearchGridMaster("", "INV_TYPE_MST", "STOCK_TYPE_DESC", "STOCK_TYPE_CODE", , , SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColStockType
                    .Text = AcName1
                End If
            End With
        End If

        If eventArgs.Col = 0 And eventArgs.Row > 0 And (CBool(LblAddMode.Text) = True Or CBool(LblModifyMode.Text) = True) Then
            MainClass.DeleteSprdRow(SprdBOM, eventArgs.Row, ColItemCode)
            '        MainClass.SaveStatus Me, LblAddMode.text, LblModifyMode.text	
        End If
    End Sub
    Private Sub SprdBOM_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdBOM.LeaveCell

        On Error GoTo ErrPart
        Dim mDeptCode As String
        Dim mRMCode As String
        Dim mStockType As String

        If eventArgs.NewRow = -1 Then Exit Sub
        SprdBOM.Row = eventArgs.row
        SprdBOM.Col = ColItemCode
        If Trim(SprdBOM.Text) = "" Then Exit Sub
        Select Case eventArgs.col
            Case ColItemCode
                SprdBOM.Row = SprdBOM.ActiveRow
                SprdBOM.Col = ColItemCode
                mRMCode = Trim(SprdBOM.Text)
                If Trim(lblMainItemCode.Text) = Trim(SprdBOM.Text) Then
                    MainClass.SetFocusToCell(SprdBOM, SprdBOM.ActiveRow, ColItemCode)
                Else
                    If CheckDuplicateItem(mRMCode) = False Then
                        SprdBOM.Row = SprdBOM.ActiveRow
                        SprdBOM.Col = ColItemCode
                        Call FillGridRow((SprdBOM.Text))
                    Else
                        MainClass.SetFocusToCell(SprdBOM, SprdBOM.ActiveRow, ColItemCode)
                    End If
                End If
            Case colStdQty
                If CheckQty(SprdBOM, eventArgs.col, eventArgs.row) = True Then
                    MainClass.AddBlankSprdRow(SprdBOM, ColItemCode, ConRowHeight)
                    FormatSprdBOM((SprdBOM.MaxRows))
                End If
            Case ColStockType
                SprdBOM.Row = SprdBOM.ActiveRow
                SprdBOM.Col = ColStockType
                mStockType = Trim(SprdBOM.Text)
                If mStockType <> "" Then
                    If MainClass.ValidateWithMasterTable(mStockType, "STOCK_TYPE_CODE", "STOCK_TYPE_CODE", "INV_TYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                        MainClass.SetFocusToCell(SprdBOM, SprdBOM.ActiveRow, ColStockType)
                    End If
                End If
        End Select

        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Function CheckDuplicateItem(ByRef pRMCode As String) As Boolean
        On Error GoTo ERR1
        Dim I As Integer
        Dim mItemRept As Integer

        If Trim(pRMCode) = "" Then CheckDuplicateItem = False : Exit Function
        With SprdBOM
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColItemCode
                If UCase(Trim(.Text)) = UCase(Trim(pRMCode)) Then
                    mItemRept = mItemRept + 1
                    If mItemRept > 1 Then
                        MsgInformation("Duplication Item in the Same Department")
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

    Private Sub FillGridRow(ByRef mItemCode As String)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim mSizeCode As Integer


        If Trim(mItemCode) = "" Then Exit Sub
        '    SqlStr = " SELECT ITEM_CODE,ITEM_SHORT_DESC,CUSTOMER_PART_NO,ISSUE_UOM, " _	
        ''            & " SEMI_FIN_ITEM_CODE,DRAWING_NO,DRW_REVNO," _	
        ''            & " ITEM_WEIGHT,ITEM_MAKE,ITEM_SURFACE_AREA" _	
        ''            & " FROM INV_ITEM_MST " _	
        ''            & " WHERE ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "' " _	
        ''            & " AND COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND ITEM_STATUS = 'A' "	

        '' AND A.ITEM_STATUS = 'A'	

        'SqlStr = "SELECT A.ITEM_SHORT_DESC " & vbCrLf _
        '    & " FROM INV_ITEM_MST A, INV_ITEM_ALTER_DET B " & vbCrLf _
        '    & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
        '    & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf _
        '    & " AND A.ITEM_CODE=B.ALTER_ITEM_CODE" & vbCrLf _
        '    & " AND B.ALTER_ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'" & vbCrLf _
        '    & " AND B.ITEM_CODE='" & MainClass.AllowSingleQuote(lblMainItemCode.Text) & "'"


        SqlStr = "SELECT A.ITEM_SHORT_DESC " & vbCrLf _
            & " FROM INV_ITEM_MST A " & vbCrLf _
            & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND A.ITEM_CODE='" & MainClass.AllowSingleQuote(mItemCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If Not RsMisc.EOF Then
            SprdBOM.Row = SprdBOM.ActiveRow
            With RsMisc
                SprdBOM.Col = ColItemName
                SprdBOM.Text = IIf(IsDbNull(.Fields("ITEM_SHORT_DESC").Value), "", .Fields("ITEM_SHORT_DESC").Value)
            End With
        Else
            MsgInformation("Invalid Alternate Code for this Item")
            MainClass.SetFocusToCell(SprdBOM, SprdBOM.ActiveRow, ColItemCode)
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
