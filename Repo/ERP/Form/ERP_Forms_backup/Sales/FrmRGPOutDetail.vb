Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class FrmRGPOutDetail
    Inherits System.Windows.Forms.Form
    'Dim PvtDBCn As ADODB.Connection
    Dim FormLoaded As Boolean
    Private Const ConRowHeight As Short = 11

    Private Const ColInwardItemCode As Short = 1
    Private Const ColInwardDesc As Short = 2
    Private Const ColGrossQty As Short = 3
    Private Const ColNetQty As Short = 4
    Private Const ColScrapQty As Short = 5
    Private Const ColINQty As Short = 6
    Private Const ColTotalQty As Short = 7

    Private Function FieldsVarification() As Boolean
        'On Error GoTo err
        'Dim mRow As Long
        '
        'Dim mItemCode As String
        'Dim mReqQty As Double
        'Dim mSendRGPQty As Double
        '
        '    FieldsVarification = True
        '
        '
        '    With SprdSubMain
        '        For mRow = 1 To .MaxRows
        '            .Row = mRow
        '            .Col = ColInwardItemCode
        '            mItemCode = Trim(.Text)
        '
        '            .Col = ColTotalQty
        '            mReqQty = Val(.Text)
        '
        '            If mItemCode <> "" Then
        '                If Val(LblPONo.text) > 0 And lblPurpose.text = "B" Then
        '                    mPOQty = GetPOQty(mItemCode, Val(LblPONo.text))
        '                    If mPOQty > 0 Then
        '                        mSendRGPQty = IIf(lblStatus.text = 0, mReqQty, 0) + GetRGPQty(mItemCode, mPONo)
        '                        If mPOQty < mSendRGPQty Then
        '                            MsgInformation "You cann't be send more than Job Order Qty. Job Order Qty is " & mPOQty & " & You Send For RGP Qty " & mSendRGPQty & "."
        '                            MainClass.SetFocusToCell SprdMain, mRow, ColTotalQty
        '                            FieldsVarification = False
        '                            Exit Function
        '                        End If
        '                    End If
        '              End If
        '            End If
        '        Next
        '    End With
        '
        '    Exit Function
        'err:
        '    ErrorMsg err.Description, err.Number, vbCritical
        '    ''Resume
    End Function
    Public Sub FormatSprdSubMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim I As Integer

        With SprdSubMain
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)

            .Col = ColInwardItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColInwardItemCode, 6)

            .Col = ColInwardDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(ColInwardDesc, 25)

            For I = ColGrossQty To ColTotalQty
                .Col = I
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 4
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.99")
                .TypeFloatMin = CDbl("-999999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(I, 8)
            Next

            MainClass.ProtectCell(SprdSubMain, 1, .MaxRows, ColInwardDesc, ColInwardDesc)
            MainClass.ProtectCell(SprdSubMain, 1, .MaxRows, ColGrossQty, ColScrapQty)
            MainClass.ProtectCell(SprdSubMain, 1, .MaxRows, ColTotalQty, ColTotalQty)
        End With
        MainClass.SetSpreadColor(SprdSubMain, Arow)
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        ConRGPSlipDetail = False
        Me.Close()
    End Sub

    Private Sub cmdOk_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOk.Click

        Call CalcTots()
        If UCase(LblAddMode.Text) = UCase("False") And UCase(LblModifyMode.Text) = UCase("False") Then Exit Sub

        If Val(lblInQty.Text) = 0 Then
            ConRGPSlipDetail = False
            MsgBox("Nothing to Save", MsgBoxStyle.Critical)
            Exit Sub
            '        cmdOk.Enabled = True
        End If

        If Int(Val(lblOutQty.Text)) <> Int(Val(lblInQty.Text)) Then
            ConRGPSlipDetail = False
            MsgBox("Inward Qty not match with Outward Qty.", MsgBoxStyle.Critical)
            Exit Sub
            '        cmdOk.Enabled = True
        End If


        If InsertIntoTemp_Table = True Then
            ConRGPSlipDetail = True
            Me.Hide()
            '' Unload Me
        Else
            ConRGPSlipDetail = False
            MsgBox("Can Not Save Detail", MsgBoxStyle.Critical)
            cmdOk.Enabled = True
        End If

    End Sub
    Private Sub FrmRGPOutDetail_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        If FormLoaded = True Then Exit Sub
        If UCase(LblAddMode.Text) = UCase("False") And UCase(LblModifyMode.Text) = UCase("False") Then
            cmdOk.Enabled = False
        Else
            cmdOk.Enabled = True
        End If

        Call ShowOutWardDetail()
        FormLoaded = True

    End Sub
    Private Sub FrmRGPOutDetail_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo LoadPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        MainClass.SetControlsColor(Me) ''FrmDSDailyDetail

        Me.Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Me.Width)) / 2)
        Me.Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Me.Height)) / 2)
        MainClass.ClearGrid(SprdSubMain)
        FormatSprdSubMain(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
LoadPart:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    Private Sub FrmRGPOutDetail_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormLoaded = False
        lblItemCode.Text = ""
        LblAddMode.Text = ""
        LblModifyMode.Text = ""
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
    End Sub
    Private Sub ShowOutWardDetail()

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim I As Integer
        Dim SqlStr As String = ""
        'Dim mLastDay As Integer
        'Dim pSDate As String

        MainClass.ClearGrid(SprdSubMain)
        FormatSprdSubMain(-1)

        SqlStr = "SELECT * FROM TEMP_RGP_OUT_DET " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf & " AND ITEM_CODE ='" & MainClass.AllowSingleQuote((lblItemCode.Text)) & "'" & vbCrLf & " AND TRN_SERIAL_NO= " & Val(lblMainActiveRow.Text) & "" & vbCrLf & " ORDER BY SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = False Then
            Do While Not RsTemp.EOF
                I = I + 1

                With SprdSubMain
                    .Row = I

                    .Col = ColInwardItemCode
                    .Text = Trim(IIf(IsDbNull(RsTemp.Fields("IN_ITEM_CODE").Value), "", RsTemp.Fields("IN_ITEM_CODE").Value))

                    MainClass.ValidateWithMasterTable(.Text, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                    .Col = ColInwardDesc
                    .Text = Trim(MasterNo)

                    .Col = ColGrossQty
                    .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("GROSS_WT").Value), 0, RsTemp.Fields("GROSS_WT").Value), "0.0000")

                    .Col = ColNetQty
                    .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("NET_WT").Value), 0, RsTemp.Fields("NET_WT").Value), "0.0000")

                    .Col = ColScrapQty
                    .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("SCRAP_WT").Value), 0, RsTemp.Fields("SCRAP_WT").Value), "0.0000")

                    .Col = ColINQty
                    .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("IN_QTY").Value), 0, RsTemp.Fields("IN_QTY").Value), "0.0000")

                    .Col = ColTotalQty
                    .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("TOTAL_IN_WT").Value), 0, RsTemp.Fields("TOTAL_IN_WT").Value), "0.0000")
                End With

                RsTemp.MoveNext()
                '            If RsTemp.EOF = False Then
                SprdSubMain.MaxRows = I + 1
                '            End If
            Loop
        End If
        FormatSprdSubMain(-1)
        Call CalcTots()
    End Sub
    Private Sub CalcTots()
        On Error GoTo ERR1
        ''Dim RsMisc As ADODB.Recordset=Nothing
        Dim mTotalQty As Double
        Dim I As Integer
        Dim mGrossQty As Double
        Dim mINQty As Double

        mTotalQty = 0


        With SprdSubMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColGrossQty
                mGrossQty = Val(.Text)

                .Col = ColINQty
                mINQty = Val(.Text)
                If Val(.Text) = 0 Then
                    If mGrossQty > 0 Then
                        mINQty = Int(Val(lblOutQty.Text) / mGrossQty)
                    End If
                End If

                .Col = ColInwardItemCode
                If Trim(.Text) <> "" Then
                    .Col = ColTotalQty
                    .Text = VB6.Format(mGrossQty * mINQty, "0.0000")
                    mTotalQty = mTotalQty + Val(.Text)
                End If
            Next I
        End With

        lblInQty.Text = VB6.Format(mTotalQty, "#0.0000")
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub
    Private Function InsertIntoTemp_Table() As Boolean

        On Error GoTo InsertErr
        Dim I As Integer
        Dim mSerialNo As Integer
        Dim mGrossQty As Double
        Dim mNetQty As Double
        Dim mScrapQty As Double
        Dim mINQty As Double
        Dim mTotalQty As Double
        Dim mInItemCode As String

        Dim SqlStr As String = ""


        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = "DELETE FROM TEMP_RGP_OUT_DET " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND USERID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "' " & vbCrLf & " AND ITEM_CODE ='" & MainClass.AllowSingleQuote((lblItemCode.Text)) & "'" & vbCrLf & " AND TRN_SERIAL_NO= " & Val(lblMainActiveRow.Text) & ""

        PubDBCn.Execute(SqlStr)

        With SprdSubMain
            For I = 1 To .MaxRows
                .Row = I
                mSerialNo = I

                .Col = ColInwardItemCode
                mInItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColGrossQty
                mGrossQty = Val(.Text)

                .Col = ColNetQty
                mNetQty = Val(.Text)

                .Col = ColScrapQty
                mScrapQty = Val(.Text)

                .Col = ColINQty
                mINQty = Val(.Text)

                .Col = ColTotalQty
                mTotalQty = Val(.Text)

                SqlStr = ""
                If mInItemCode <> "" And mINQty <> 0 Then
                    SqlStr = "INSERT INTO TEMP_RGP_OUT_DET " & " (USERID, COMPANY_CODE, TRN_SERIAL_NO, SERIAL_NO, " & vbCrLf & " ITEM_CODE, IN_ITEM_CODE, GROSS_WT, " & vbCrLf & " NET_WT, SCRAP_WT, IN_QTY, TOTAL_IN_WT" & vbCrLf & " ) VALUES (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " " & Val(lblMainActiveRow.Text) & ", " & Val(CStr(mSerialNo)) & "," & vbCrLf & " '" & MainClass.AllowSingleQuote(lblItemCode.Text) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote(mInItemCode) & "', " & vbCrLf & " " & mGrossQty & ", " & mNetQty & ", " & mScrapQty & ", " & mINQty & "," & vbCrLf & " " & mTotalQty & ") "
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

    Private Sub SprdSubMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdSubMain.ClickEvent

        Dim xIName As String
        Dim SqlStr As String = ""
        Dim xRGPCode As String

        SprdSubMain.Row = SprdSubMain.ActiveRow
        xRGPCode = Trim(lblItemCode.Text)

        If eventArgs.row = 0 And eventArgs.col = ColInwardItemCode Then
            With SprdSubMain
                SqlStr = SelectQuery(True, xRGPCode)
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColInwardItemCode
                    .Text = Trim(AcName)
                End If
                MainClass.SetFocusToCell(SprdSubMain, SprdSubMain.ActiveRow, ColInwardItemCode)
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColInwardDesc Then
            With SprdSubMain
                .Row = .ActiveRow
                .Col = ColInwardDesc
                xIName = Trim(.Text)

                SqlStr = SelectQuery(False, xRGPCode)
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColInwardDesc
                    .Text = Trim(AcName)
                Else
                    .Row = .ActiveRow
                    .Col = ColInwardDesc
                    .Text = xIName
                End If

                MainClass.ValidateWithMasterTable(.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                .Row = .ActiveRow
                .Col = ColInwardItemCode
                .Text = Trim(MasterNo)
                MainClass.SetFocusToCell(SprdSubMain, SprdSubMain.ActiveRow, ColInwardItemCode)
            End With
        End If

        Dim mItemCode As String
        Dim DelStatus As Boolean
        If eventArgs.col = 0 And eventArgs.row > 0 Then
            SprdSubMain.Row = eventArgs.row
            SprdSubMain.Col = ColInwardItemCode
            If eventArgs.row < SprdSubMain.MaxRows And (CBool(LblAddMode.Text) = True Or CBool(LblModifyMode.Text) = True) Then

                SprdSubMain.Col = ColInwardItemCode
                mItemCode = SprdSubMain.Text

                MainClass.DeleteSprdRow(SprdSubMain, eventArgs.row, ColInwardItemCode, DelStatus)
                FormatSprdSubMain(-1)
                '            MainClass.SaveStatus Me, LblAddMode.text, LblModifyMode.text
            End If
        End If

        CalcTots()
    End Sub
    Private Function SelectQuery(ByRef xIsItemCode As Boolean, Optional ByRef pRGPItemCode As String = "") As String
        On Error GoTo ErrPart
        Dim mSuppCode As String
        Dim SelectQuery1 As String


        If xIsItemCode = True Then
            SelectQuery1 = "SELECT DISTINCT INVMST.ITEM_CODE, INVMST.ITEM_SHORT_DESC "
        Else
            SelectQuery1 = "SELECT DISTINCT INVMST.ITEM_SHORT_DESC, INVMST.ITEM_CODE "
        End If

        SelectQuery = SelectQuery1 & vbCrLf & " FROM  " & vbCrLf & " PRD_OUTBOM_HDR IH,PRD_OUTBOM_DET ID, " & vbCrLf & " INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY = ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE = INVMST.COMPANY_CODE " & vbCrLf & " AND ID.PRODUCT_CODE = INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ID.ITEM_CODE='" & pRGPItemCode & "'"

        SelectQuery = SelectQuery & vbCrLf & " UNION " & SelectQuery1 & vbCrLf & " FROM  " & vbCrLf & " PRD_OUTBOM_HDR IH,PRD_OUTBOM_ALTER_DET ID, " & vbCrLf & " INV_ITEM_MST INVMST" & vbCrLf & " WHERE IH.MKEY = ID.MKEY " & vbCrLf & " AND IH.COMPANY_CODE = INVMST.COMPANY_CODE " & vbCrLf & " AND ID.PRODUCT_CODE = INVMST.ITEM_CODE " & vbCrLf & " AND IH.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND ID.ALTER_ITEM_CODE='" & pRGPItemCode & "'"

        '    SelectQuery = SelectQuery & vbCrLf _
        ''            & " UNION " & SelectQuery1 & vbCrLf _
        ''            & " FROM  " & vbCrLf _
        ''            & " INV_ITEM_MST INVMST" & vbCrLf _
        ''            & " WHERE INVMST.COMPANY_CODE = " & RsCompany.fields("COMPANY_CODE").value & "" & vbCrLf _
        ''            & " AND INVMST.ITEM_CODE='" & pRGPItemCode & "'"

        SelectQuery = SelectQuery & vbCrLf & " ORDER BY 1 "

        Exit Function
ErrPart:
        SelectQuery = ""
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Sub SprdSubMain_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdSubMain.KeyPressEvent
        With SprdSubMain
            If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then
                SprdSubMain_LeaveCell(SprdSubMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow + 1, False))
                '            SprdSubMain_LeaveCell .ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False
            End If

        End With
    End Sub
    Private Sub SprdSubMain_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles SprdSubMain.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim mCancel As Boolean
        mCancel = False
        With SprdSubMain
            SprdSubMain_LeaveCell(SprdSubMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, mCancel))
            Cancel = mCancel
        End With
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub SprdSubMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdSubMain.LeaveCell

        'Dim xPoNo As String
        Dim xICode As String
        'Dim mQty As Double
        'Dim mAcceptQty As Double
        'Dim mItemClassType As String
        'Dim mLotNoRequied As String
        'Dim SqlStr As String = ""
        'Dim RsTemp As ADODB.Recordset=Nothing
        Dim xRGPItemCode As String
        Dim mRow As Integer

        If eventArgs.NewRow = -1 Then Exit Sub

        xRGPItemCode = Trim(lblItemCode.Text)


        If xRGPItemCode = "" Then Exit Sub

        SprdSubMain.Row = eventArgs.Row

        mRow = eventArgs.row
        SprdSubMain.Row = mRow

        Select Case eventArgs.col
            Case ColInwardItemCode
                SprdSubMain.Row = mRow
                SprdSubMain.Col = ColInwardItemCode
                xICode = Trim(SprdSubMain.Text)

                If xICode = "" Then Exit Sub
                If MainClass.ValidateWithMasterTable(xICode, "Item_Code", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    SprdSubMain.Col = ColInwardDesc
                    SprdSubMain.Text = MasterNo

                    If DuplicateItemCode() = False Then
                        SprdSubMain.Row = mRow
                        If FillGridRow(xICode, xRGPItemCode) = False Then Exit Sub
                        FormatSprdSubMain(eventArgs.row)
                    Else
                        MainClass.SetFocusToCell(SprdSubMain, eventArgs.row, ColInwardItemCode)
                        eventArgs.cancel = True
                    End If
                Else
                    MainClass.SetFocusToCell(SprdSubMain, eventArgs.row, ColInwardItemCode)
                    eventArgs.cancel = True
                End If

                MainClass.AddBlankSprdRow(SprdSubMain, ColInwardItemCode, ConRowHeight)
                FormatSprdSubMain(eventArgs.row)

            Case ColInwardDesc
                SprdSubMain.Row = mRow
                SprdSubMain.Col = ColInwardDesc
                If MainClass.ValidateWithMasterTable(SprdSubMain.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                    MainClass.SetFocusToCell(SprdSubMain, eventArgs.row, ColInwardItemCode)
                    eventArgs.cancel = True
                End If

                '        Case ColBillQty
                '            SprdSubMain.Row = mRow
                '            SprdSubMain.Col = ColPONo
                '            xPoNo = SprdSubMain.Text
                '            If mWithOutOrder = False Then
                '                If xPoNo = "" Then Exit Sub
                '            End If
                '
                '            SprdSubMain.Col = ColItemCode
                '            xICode = SprdSubMain.Text
                '            If xICode = "" Then Exit Sub
                '
                '            ''25-06-2007
                '
                '            If PubSuperUser = "U" Then
                '                If CheckBillQty(ColBillQty, Row) = True Then
                '                    SprdSubMain.Col = ColReceivedQty
                '                    mQty = Val(SprdSubMain.Text)
                '                    MainClass.AddBlankSprdRow SprdSubMain, ColItemCode, ConRowHeight
                '                    FormatSprdSubMain Row
                '                Else
                '                    Cancel = True
                '                    Exit Sub
                '                End If
                '            Else
                '                SprdSubMain.Col = ColReceivedQty
                '                mQty = Val(SprdSubMain.Text)
                '                MainClass.AddBlankSprdRow SprdSubMain, ColItemCode, ConRowHeight
                '                FormatSprdSubMain Row
                '            End If

        End Select
        Call CalcTots()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Function FillGridRow(ByRef mItemCode As String, ByRef mOutItemCode As String) As Boolean

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        'Dim mOrdQty, mRecvQty As Double
        'Dim xPoNo As String
        'Dim xFYNo As Long
        'Dim xSupplierCode As Long
        'Dim mOrderSno As Long
        Dim SqlStr As String = ""
        'Dim RsTemp As ADODB.Recordset=Nothing
        'Dim mQCEmpCode As String

        If mItemCode = "" Then Exit Function

        SqlStr = ""
        SqlStr = " SELECT Item_Code,ITEM_SHORT_DESC AS NAME," & vbCrLf & " PURCHASE_UOM " & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND Item_Code='" & MainClass.AllowSingleQuote(UCase(mItemCode)) & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMisc.EOF = False Then
            SprdSubMain.Row = SprdSubMain.Row
            With RsMisc

                If CollectData(mItemCode, mOutItemCode, (SprdSubMain.Row)) = False Then
                    MsgInformation("Invalid Item Code.")
                    MainClass.SetFocusToCell(SprdSubMain, SprdSubMain.ActiveRow, ColInwardItemCode)
                    FillGridRow = False
                    Exit Function
                End If

                '            SprdSubMain.Row = SprdSubMain.ActiveRow
                '            Select Case Left(cboRefType.Text, 1)
                '                Case "P", "I", "1"
                '
                '                Case "R"
                '                    If GetOutJobworkManyItem(mItemCode, Trim(txtMRRDate.Text)) = True Then
                '                        GoTo NextLoop
                '                    End If
                '                Case Else
                '            End Select


            End With
            FillGridRow = True
        Else
            MainClass.SetFocusToCell(SprdSubMain, SprdSubMain.ActiveRow, ColInwardItemCode)
            FillGridRow = False
        End If

        Exit Function
ERR1:
        FillGridRow = False
        MsgBox(Err.Description)
    End Function
    Private Function CollectData(ByRef xItemCode As String, ByRef xOutItemCode As String, ByRef mRowNo As Integer) As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsPO As ADODB.Recordset
        Dim xFYNo As Integer
        Dim jj As Integer
        Dim mSprdRowNo As Integer
        Dim mInwardItemCode As String
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim mInConUnit As Double
        Dim mOutConUnit As Double

        Dim mMultiItemCode As Boolean
        Dim mMKey As String
        Dim mCheckOutItem As String

        SqlStr = ""


        If xItemCode = xOutItemCode Then
            mOutConUnit = 1
            SprdSubMain.Row = mRowNo
            SprdSubMain.Col = ColGrossQty
            SprdSubMain.Text = VB6.Format(1, "0.0000")

            SprdSubMain.Col = ColNetQty
            SprdSubMain.Text = VB6.Format(1, "0.0000")

            SprdSubMain.Col = ColScrapQty
            SprdSubMain.Text = VB6.Format(0, "0.0000")
        Else
            SqlStr = "SELECT ID.ITEM_QTY, ID.SCRAP_QTY " & vbCrLf & " FROM PRD_OUTBOM_HDR IH, PRD_OUTBOM_DET ID " & vbCrLf & " WHERE " & vbCrLf & " IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & xItemCode & "'" & vbCrLf & " AND ID.ITEM_CODE='" & xOutItemCode & "'"

            SqlStr = SqlStr & vbCrLf & " AND IH.WEF = (" & vbCrLf & " SELECT MAX(WEF) " & vbCrLf & " FROM PRD_OUTBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE='" & xItemCode & "'" & vbCrLf & " AND WEF<=TO_DATE('" & VB6.Format(lblRGPDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            If CBool(LblAddMode.Text) = True Then
                SqlStr = SqlStr & " AND STATUS='O')"
            Else
                SqlStr = SqlStr & ")"
            End If

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                SprdSubMain.Row = mRowNo
                SprdSubMain.Col = ColGrossQty
                SprdSubMain.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value) + IIf(IsDbNull(RsTemp.Fields("SCRAP_QTY").Value), 0, RsTemp.Fields("SCRAP_QTY").Value), "0.0000")

                SprdSubMain.Col = ColNetQty
                SprdSubMain.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value), "0.0000")

                SprdSubMain.Col = ColScrapQty
                SprdSubMain.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("SCRAP_QTY").Value), 0, RsTemp.Fields("SCRAP_QTY").Value), "0.0000")
            Else
                SqlStr = "SELECT ID.ALTER_ITEM_QTY AS ITEM_QTY, ALTER_SCRAP_QTY AS SCRAP_QTY" & vbCrLf & " FROM PRD_OUTBOM_HDR IH, PRD_OUTBOM_ALTER_DET ID " & vbCrLf & " WHERE " & vbCrLf & " IH.MKEY=ID.MKEY" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.PRODUCT_CODE='" & xItemCode & "'" & vbCrLf & " AND ALTER_ITEM_CODE='" & xOutItemCode & "'"

                SqlStr = SqlStr & vbCrLf & " AND IH.WEF = (" & vbCrLf & " SELECT MAX(WEF) " & vbCrLf & " FROM PRD_OUTBOM_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND PRODUCT_CODE='" & xItemCode & "'" & vbCrLf & " AND WEF<=TO_DATE('" & VB6.Format(lblRGPDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                If CBool(LblAddMode.Text) = True Then
                    SqlStr = SqlStr & " AND STATUS='O')"
                Else
                    SqlStr = SqlStr & ")"
                End If
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then
                    SprdSubMain.Row = mRowNo
                    SprdSubMain.Col = ColGrossQty
                    SprdSubMain.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value) + IIf(IsDbNull(RsTemp.Fields("SCRAP_QTY").Value), 0, RsTemp.Fields("SCRAP_QTY").Value), "0.0000")

                    SprdSubMain.Col = ColNetQty
                    SprdSubMain.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("ITEM_QTY").Value), 0, RsTemp.Fields("ITEM_QTY").Value), "0.0000")

                    SprdSubMain.Col = ColScrapQty
                    SprdSubMain.Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("SCRAP_QTY").Value), 0, RsTemp.Fields("SCRAP_QTY").Value), "0.0000")
                End If
            End If

        End If

        CalcTots()
        CollectData = True
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        CollectData = False
    End Function
    Private Function DuplicateItemCode() As Boolean

        Dim cntRow As Integer
        Dim mCount As Byte
        Dim mCheckItemCode As String
        'Dim mItemCode As String
        'Dim mPONo As Double
        Dim xCheckCode As String

        With SprdSubMain
            .Row = .ActiveRow

            .Col = ColInwardItemCode
            mCheckItemCode = Trim(UCase(.Text))

            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColInwardItemCode
                xCheckCode = Trim(UCase(.Text))

                If (xCheckCode = mCheckItemCode And mCheckItemCode <> "") Then
                    mCount = mCount + 1
                End If

                If mCount > 1 Then
                    DuplicateItemCode = True
                    MsgInformation("Duplicate Item Code : " & mCheckItemCode)
                    MainClass.SetFocusToCell(SprdSubMain, .ActiveRow, ColInwardItemCode)
                    Exit Function
                End If
            Next
        End With
    End Function
End Class
