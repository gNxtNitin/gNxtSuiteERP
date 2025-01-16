Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class FrmPOItemIndent
    Inherits System.Windows.Forms.Form
    'Dim PvtDBCn As ADODB.Connection
    Dim FormLoaded As Boolean
    Private Const ConRowHeight As Short = 11
    Private Const ColIndentNo As Short = 1
    Private Const ColIndentSlNo As Short = 2
    Private Const ColBalQty As Short = 3
    Private Const ColQty As Short = 4

    Public Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim RsEquip As ADODB.Recordset
        With SprdMain
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = ColIndentNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = MainClass.SetMaxLength("AUTO_KEY_INDENT", "PUR_INDENT_HDR", PubDBCn)
            .set_ColWidth(.Col, 12)

            .Col = ColIndentSlNo
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeEditLen = MainClass.SetMaxLength("SERIAL_NO", "PUR_INDENT_DET", PubDBCn)
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 12)
            .ColHidden = True

            .Col = ColBalQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
                .TypeFloatDecimalPlaces = 4
                .TypeFloatMax = CDbl("9999999.9999")
                .TypeFloatMin = CDbl("-9999999.9999")
            Else
                .TypeFloatMax = CDbl("999999999.99")
                .TypeFloatMin = CDbl("-999999999.99")
                .TypeFloatDecimalPlaces = 3
            End If

            .TypeEditLen = MainClass.SetMaxLength("INDENT_QTY", "PUR_POCONS_IND_TRN", PubDBCn)
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 12)

            .Col = ColQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            If RsCompany.Fields("ERP_CUSTOMER_ID").Value = 106 Then
                .TypeFloatDecimalPlaces = 4
                .TypeFloatMax = CDbl("9999999.9999")
                .TypeFloatMin = CDbl("-9999999.9999")
            Else
                .TypeFloatDecimalPlaces = 3
                .TypeFloatMax = CDbl("999999999.99")
                .TypeFloatMin = CDbl("-999999999.99")
            End If

            .TypeEditLen = MainClass.SetMaxLength("INDENT_QTY", "PUR_POCONS_IND_TRN", PubDBCn)
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 12)

            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColIndentSlNo, ColBalQty)
        End With

        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Function VarifyBeforInsert() As Boolean
        On Error GoTo ErrPart
        Dim I As Integer

        VarifyBeforInsert = False
        With SprdMain
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColIndentNo
                If .Text <> "" Then
                    If CheckDuplicateIndent(Trim(.Text)) = True Then
                        .Row = I
                        .Col = ColIndentNo
                        MsgBox("Duplicate Indent :- " & .Text, MsgBoxStyle.Information)
                        Exit Function
                    End If
                End If
            Next
        End With

        If SprdMain.MaxRows > 1 Then
            If MainClass.ValidDataInGrid(SprdMain, ColIndentNo, "N", "Indent No. Is Blank...") = False Then Exit Function
        End If

        '    If Val(LblQty.text) <> Val(lblPOQty.text) Then
        '        MsgInformation "PO Qty is not Equal to Indent Qty"
        '        MainClass.SetFocusToCell SprdMain, SprdMain.MaxRows, ColQty
        '    Else
        lblPOQty.Text = CStr(Val(LblQty.Text))
        VarifyBeforInsert = True
        '    End If
        Exit Function
ErrPart:
        MsgBox(Err.Description)
    End Function

    Private Sub cmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click

        ConPOIndentDetail = False
        Me.Hide()
        Me.Close()
        FormLoaded = False
        frmPO_GST.Refresh()


    End Sub

    Private Sub cmdOk_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdOK.Click
        If VarifyBeforInsert = True Then
            If InsertIntoTemp_Indent = True Then
                '            If PubGSTApplicable = False Then
                '                frmPO.lblPOType.text = "True"
                '                FrmPOItemIndent.Hide
                '    '        Unload Me
                '                FormLoaded = False
                '                frmPO.Refresh
                '                Screen.MousePointer = 0
                '            Else
                ConPOIndentDetail = True
                Me.Hide()
                '        Unload Me
                FormLoaded = False
                frmPO_GST.Refresh()
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                '            End If
            End If
        End If
    End Sub
    Private Sub FrmPOItemIndent_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        If FormLoaded = False Then
            FormatSprdMain(-1)
            Call ShowItemIndent()

            FormLoaded = True
        End If

        'PressedCancel = False
    End Sub

    Private Sub FrmPOItemIndent_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo LoadPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        MainClass.SetControlsColor(Me)

        ''Me.Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Me.Width)) / 2)
        ''Me.Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Me.Height)) / 2)

        Me.Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Me.Width)) / 2)
        Me.Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Me.Height)) / 2)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormatSprdMain(-1)
        Exit Sub
LoadPart:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    Private Sub FrmPOItemIndent_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormLoaded = False
        LblItemCode.Text = ""
        LblQty.Text = ""
        LblPONo.Text = ""
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
    End Sub
    Private Sub ShowItemIndent()

        Dim RsItemIndent As ADODB.Recordset = Nothing
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mIndentNo As String
        Dim mItemCode As String
        Dim mQty As Double

        MainClass.ClearGrid(SprdMain)
        SqlStr = "SELECT * FROM TEMP_PUR_POCONS_IND_TRN " & vbCrLf _
            & " WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf _
            & " AND Item_Code='" & MainClass.AllowSingleQuote(UCase(LblItemCode.Text)) & "' " & " ORDER BY SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsItemIndent, ADODB.LockTypeEnum.adLockReadOnly)

        If RsItemIndent.EOF = False Then
            I = 1
            Do While Not RsItemIndent.EOF
                With SprdMain
                    .Row = I
                    FormatSprdMain(I)

                    .Col = ColIndentNo
                    .Text = CStr(Val(IIf(IsDbNull(RsItemIndent.Fields("AUTO_KEY_INDENT").Value), "", RsItemIndent.Fields("AUTO_KEY_INDENT").Value)))
                    mIndentNo = IIf(IsDbNull(RsItemIndent.Fields("AUTO_KEY_INDENT").Value), "", RsItemIndent.Fields("AUTO_KEY_INDENT").Value)

                    mItemCode = IIf(IsDbNull(RsItemIndent.Fields("ITEM_CODE").Value), "", RsItemIndent.Fields("ITEM_CODE").Value)

                    .Col = ColIndentSlNo
                    .Text = CStr(Val(IIf(IsDbNull(RsItemIndent.Fields("SERIAL_NO_INDENT").Value), "", RsItemIndent.Fields("SERIAL_NO_INDENT").Value)))

                    .Col = ColQty
                    mQty = Val(IIf(IsDbNull(RsItemIndent.Fields("INDENT_QTY").Value), "", RsItemIndent.Fields("INDENT_QTY").Value))
                    .Text = CStr(mQty)

                    .Col = ColBalQty
                    .Text = CStr(GetIndentBalQty(mIndentNo, mItemCode, mQty))

                End With
                RsItemIndent.MoveNext()
                I = I + 1
                SprdMain.MaxRows = I
            Loop
        Else
            SqlStr = " SELECT IH.AUTO_KEY_INDENT,ID.SERIAL_NO,TO_CHAR(REQ_QTY-SUM(NVL(INDENT_QTY,0))) AS BAL_QTY" & vbCrLf _
                & " FROM PUR_INDENT_HDR IH,PUR_INDENT_DET ID,PUR_POCONS_IND_TRN POD " & vbCrLf _
                & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND IH.AUTO_KEY_INDENT=ID.AUTO_KEY_INDENT" & vbCrLf _
                & " AND ID.AUTO_KEY_INDENT=POD.AUTO_KEY_INDENT(+)" & vbCrLf _
                & " AND ID.ITEM_CODE=POD.ITEM_CODE(+)" & vbCrLf _
                & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(UCase(LblItemCode.Text)) & "'" & vbCrLf _
                & " AND APP_EMP_CODE IS NOT NULL AND  APPROVAL_STATUS='Y' AND INDENT_STATUS='N'" & vbCrLf _
                & " GROUP BY IH.AUTO_KEY_INDENT,ID.SERIAL_NO,REQ_QTY " & vbCrLf _
                & " HAVING REQ_QTY-SUM(NVL(INDENT_QTY,0))>0"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsItemIndent, ADODB.LockTypeEnum.adLockReadOnly)

            If RsItemIndent.EOF = False Then
                I = 1
                FormatSprdMain(-1)
                Do While Not RsItemIndent.EOF
                    With SprdMain
                        .Row = I

                        .Col = ColIndentNo
                        .Text = CStr(Val(IIf(IsDbNull(RsItemIndent.Fields("AUTO_KEY_INDENT").Value), "", RsItemIndent.Fields("AUTO_KEY_INDENT").Value)))
                        mIndentNo = IIf(IsDbNull(RsItemIndent.Fields("AUTO_KEY_INDENT").Value), "", RsItemIndent.Fields("AUTO_KEY_INDENT").Value)

                        mItemCode = UCase(LblItemCode.Text)

                        .Col = ColIndentSlNo
                        .Text = CStr(Val(IIf(IsDbNull(RsItemIndent.Fields("SERIAL_NO").Value), "", RsItemIndent.Fields("SERIAL_NO").Value)))

                        .Col = ColBalQty
                        mQty = Val(IIf(IsDbNull(RsItemIndent.Fields("BAL_QTY").Value), "", RsItemIndent.Fields("BAL_QTY").Value))
                        .Text = CStr(mQty)

                        '                    .Col = ColBalQty
                        '                    .Text = GetIndentBalQty(mIndentNo, mItemCode, mQty)

                    End With
                    RsItemIndent.MoveNext()
                    I = I + 1
                    SprdMain.MaxRows = I
                Loop
                FormatSprdMain(-1)
            End If
        End If
        FormatSprdMain(-1)
        CalcTots()
    End Sub
    Private Function InsertIntoTemp_Indent() As Boolean

        On Error GoTo InsertErr
        Dim I As Integer
        Dim mIndentNo As String
        Dim SqlStr As String = ""
        Dim mIndentSlNo As String
        Dim mQty As Double

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_PUR_POCONS_IND_TRN " & vbCrLf _
            & "WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf _
            & "AND ITEM_CODE='" & MainClass.AllowSingleQuote(UCase(LblItemCode.Text)) & "' "


        '        & "AND MKEY=" & Val(LblPONo.text) & ""
        PubDBCn.Execute(SqlStr)

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColIndentNo
                mIndentNo = CStr(Val(.Text))

                .Col = ColIndentSlNo
                mIndentSlNo = CStr(I)

                .Col = ColQty
                mQty = Val(.Text)

                SqlStr = ""
                If mIndentNo <> "" And Val(CStr(mQty)) > 0 Then
                    SqlStr = "INSERT INTO TEMP_PUR_POCONS_IND_TRN " & vbCrLf _
                        & "(USERID, " & vbCrLf & " SERIAL_NO, AUTO_KEY_INDENT," & vbCrLf _
                        & " SERIAL_NO_INDENT, INDENT_QTY," & vbCrLf _
                        & " ITEM_CODE ) VALUES (" & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                        & " " & I & "," & mIndentNo & ", " & vbCrLf _
                        & " " & mIndentSlNo & ", " & mQty & "," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(LblItemCode.Text) & "')"

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        PubDBCn.CommitTrans()
        InsertIntoTemp_Indent = True
        Exit Function
InsertErr:
        'Resume
        PubDBCn.RollbackTrans()
        InsertIntoTemp_Indent = False
        MsgBox(Err.Description)
    End Function
    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim SqlStr As String = ""
        If eventArgs.Row = 0 And eventArgs.Col = ColIndentNo Then
            With SprdMain
                '            SqlStr = " SUBSTR(AUTO_KEY_INDENT,LENGTH(AUTO_KEY_INDENT)-1)=" & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
                ''                    & " AND Item_Code='" & MainClass.AllowSingleQuote(UCase(LblItemCode.text)) & "'"

                SqlStr = " SELECT IH.AUTO_KEY_INDENT,TO_CHAR(REQ_QTY-SUM(NVL(DECODE(POD.MKEY," & Val(LblPONo.Text) & ",0,INDENT_QTY),0))) AS BAL_QTY" & vbCrLf _
                    & " FROM PUR_INDENT_HDR IH,PUR_INDENT_DET ID,PUR_POCONS_IND_TRN POD " & vbCrLf _
                    & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND IH.AUTO_KEY_INDENT=ID.AUTO_KEY_INDENT" & vbCrLf _
                    & " AND ID.AUTO_KEY_INDENT=POD.AUTO_KEY_INDENT(+)" & vbCrLf _
                    & " AND ID.ITEM_CODE=POD.ITEM_CODE(+)" & vbCrLf _
                    & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(UCase(LblItemCode.Text)) & "' " & vbCrLf _
                    & " AND DIV_CODE=" & Val(lblDivisionCode.Text) & "" & vbCrLf _
                    & " AND APP_EMP_CODE IS NOT NULL AND APPROVAL_STATUS='Y' AND INDENT_STATUS='N'" & vbCrLf _
                    & " GROUP BY IH.AUTO_KEY_INDENT,REQ_QTY " & vbCrLf _
                    & " HAVING REQ_QTY-SUM(NVL(DECODE(POD.MKEY," & Val(LblPONo.Text) & ",0,INDENT_QTY),0))>0"

                ''AND APP_EMP_CODE IS NOT NULL AND  '' Sandeep 12/08/2022

                eventArgs.Row = .ActiveRow
                eventArgs.Col = ColIndentNo
                ''If MainClass.SearchMaster(.Text, "PUR_INDENT_DET", "AUTO_KEY_INDENT", SqlStr) = True Then
                ''If MainClass.SearchGridMaster(.Text, "PUR_INDENT_DET", "AUTO_KEY_INDENT", "SERIAL_NO", "", "", SqlStr) = True Then
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    eventArgs.Row = .ActiveRow
                    eventArgs.Col = ColIndentNo
                    .Text = AcName
                End If
            End With
        End If
        If eventArgs.Col = 0 And eventArgs.Row > 0 Then
            MainClass.DeleteSprdRow(SprdMain, eventArgs.Row, ColIndentNo)
        End If
    End Sub

    Private Sub SprdMain_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdMain.KeyPressEvent
        With SprdMain
            If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then
                SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
            End If
        End With
    End Sub
    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        With SprdMain
            If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 Then Call SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColIndentNo, 0))
        End With
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim mIndentNo As String
        If eventArgs.NewRow = -1 Then Exit Sub

        Select Case eventArgs.col
            Case ColIndentNo
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColIndentNo
                mIndentNo = SprdMain.Text
                If mIndentNo = "" Then Exit Sub
                If MainClass.ValidateWithMasterTable(mIndentNo, "AUTO_KEY_INDENT", "AUTO_KEY_INDENT", "PUR_INDENT_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DIV_CODE=" & Val(lblDivisionCode.Text) & " AND APP_EMP_CODE IS NOT NULL AND APPROVAL_STATUS='Y'") = False Then
                    MsgInformation("Either Indent No is Invalid OR Not Approved")
                    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColIndentNo)
                End If
                If SprdMain.Text <> "" Then
                    If CheckDuplicateIndent(Trim(SprdMain.Text)) = False Then
                        SprdMain.Row = SprdMain.ActiveRow
                        SprdMain.Col = ColBalQty
                        SprdMain.Text = CStr(GetIndentBalQty(mIndentNo, (LblItemCode.Text), 0))

                        MainClass.AddBlankSprdRow(SprdMain, ColIndentNo, ConRowHeight)
                        FormatSprdMain(-1)
                        'MainClass.SetFocusToCell SprdMain, Row, ColQty
                    End If

                End If


            Case ColQty
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColIndentNo
                If SprdMain.Text = "" Then Exit Sub
                If CheckItemQty() = True Then

                End If
        End Select

        Call CalcTots()

        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Function CheckItemQty() As Boolean

        On Error GoTo ERR1
        Dim mQty As Double
        Dim mBalQty As Double

        With SprdMain
            .Row = .ActiveRow
            .Col = ColIndentNo
            If Trim(.Text) = "" Then Exit Function

            .Col = ColQty
            mQty = Val(.Text)
            'If Val(mQty) = 0 Then Exit Function

            If mQty > 0 Then
                .Col = ColBalQty
                mBalQty = Val(.Text)
                If mQty <= mBalQty Then
                    CheckItemQty = True
                Else
                    MsgInformation("Bal Qty is Less Than Req. Qty.")
                    MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColQty)
                End If
            Else
                MsgInformation("Please Check the Qty.")
                'MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColQty)
            End If
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function CheckDuplicateIndent(ByRef mItemIndent As String) As Boolean

        On Error GoTo ERR1
        Dim I As Integer
        Dim mItemRept As Integer
        If mItemIndent = "" Then CheckDuplicateIndent = True : Exit Function
        With SprdMain
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColIndentNo
                If UCase(Trim(.Text)) = UCase(Trim(mItemIndent)) Then
                    mItemRept = mItemRept + 1
                    If mItemRept > 1 Then
                        CheckDuplicateIndent = True
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColIndentNo)
                        Exit Function
                    End If
                End If
            Next
        End With
        Exit Function
ERR1:
        CheckDuplicateIndent = True
        MsgInformation(Err.Description)
    End Function

    Private Sub SprdMain_LeaveRow(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveRowEvent) Handles SprdMain.LeaveRow
        Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(SprdMain.ActiveCol, SprdMain.ActiveRow, SprdMain.ActiveCol, SprdMain.ActiveRow, False))
    End Sub

    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(SprdMain.ActiveCol, SprdMain.ActiveRow, SprdMain.ActiveCol, SprdMain.ActiveRow, False))
    End Sub
    Private Sub CalcTots()
        On Error GoTo ERR1

        Dim CntRow As Integer

        Dim mQty As Double
        Dim mTotQty As Double

        With SprdMain
            For CntRow = 1 To .MaxRows
                .Row = CntRow

                .Col = ColIndentNo
                If .Text = "" Then GoTo DontCalc

                .Col = ColQty
                mQty = Val(.Text)

                mTotQty = mTotQty + mQty
DontCalc:
            Next CntRow
        End With
        LblQty.Text = Val(mTotQty)

        Exit Sub
ERR1:
        'Resume
        If Err.Number = 6 Then Resume Next 'OverFlow
        MsgInformation(Err.Description)
    End Sub

    Public Function GetIndentBalQty(ByRef pIndentNo As String, ByRef pItemCode As String, ByRef lQty As Double) As Double

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mPOQty As Double
        Dim mReqQty As Double

        SqlStr = " SELECT SUM(REQ_QTY) AS REQ_QTY" & vbCrLf _
            & " FROM PUR_INDENT_DET " & vbCrLf _
            & " WHERE AUTO_KEY_INDENT=" & Val(pIndentNo) & "" & vbCrLf _
            & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mReqQty = IIf(IsDbNull(RsTemp.Fields("REQ_QTY").Value), 0, RsTemp.Fields("REQ_QTY").Value)
        End If

        SqlStr = " SELECT SUM(INDENT_QTY) AS REC_QTY" & vbCrLf & " FROM PUR_POCONS_IND_TRN " & vbCrLf & " WHERE AUTO_KEY_INDENT=" & Val(pIndentNo) & "" & vbCrLf & " AND ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'"

        If Val(LblPONo.Text) <> 0 Then
            SqlStr = SqlStr & vbCrLf & " AND MKEY<>" & Val(LblPONo.Text) & ""
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mPOQty = IIf(IsDbNull(RsTemp.Fields("REC_QTY").Value), 0, RsTemp.Fields("REC_QTY").Value)
        End If

        GetIndentBalQty = mReqQty - mPOQty ''+ lQty
        Exit Function
ErrPart:
        GetIndentBalQty = 0
    End Function
End Class
