Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class FrmModelWiseItemEntry
    Inherits System.Windows.Forms.Form
    Dim RsModelMain As ADODB.Recordset ''Recordset				
    Dim RsModelDetail As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection				

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim FormActive As Boolean

    Private Const ConRowHeight As Short = 12

    Private Const ColItemCode As Short = 1
    Private Const ColItemDesc As Short = 2
    Private Const ColUOM As Short = 3
    Private Const ColStdQty As Short = 4
    Private Const ColRemarks As Short = 5

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click

        On Error GoTo AddErr
        If cmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            SprdMain.Enabled = True
            '        txtModelCode.Enabled = False				
            '        cmdSearch.Enabled = False				
        Else
            cmdAdd.Text = ConCmdAddCaption
            ADDMode = False
            MODIFYMode = False
            MainClass.ClearGrid(SprdMain)
            Call FormatSprdMain(-1)
            Clear1()
            Show1()
        End If
        Exit Sub
AddErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDelete.Click
        On Error GoTo DelErrPart
        Dim mItemCode As String

        If Trim(txtModelCode.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub

        If Not RsModelMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "INV_MODELWISE_PROD_HDR ", (txtModelCode.Text), RsModelMain, "MODEL_CODE") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "INV_MODELWISE_PROD_HDR", "MODEL_CODE", (txtModelCode.Text)) = False Then GoTo DelErrPart

                PubDBCn.Execute("DELETE FROM INV_MODELWISE_PROD_DET WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND MODEL_CODE='" & MainClass.AllowSingleQuote(txtModelCode.Text) & "'")
                PubDBCn.Execute("DELETE FROM INV_MODELWISE_PROD_HDR WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND MODEL_CODE='" & MainClass.AllowSingleQuote(txtModelCode.Text) & "'")

                PubDBCn.CommitTrans()
                RsModelMain.Requery()
                RsModelDetail.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        RsModelMain.Requery()
        RsModelDetail.Requery()
    End Sub
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdModify.Click

        On Error GoTo ModifyErr
        If cmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsModelMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
            '        txtModelCode.Enabled = False				
            '        cmdSearch.Enabled = False				
        Else
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        End If
        Exit Sub
ModifyErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If UpdateMain1() = True Then
            ADDMode = False
            MODIFYMode = False
            txtModelCode_Validating(txtModelCode, New System.ComponentModel.CancelEventArgs(False))
            If cmdAdd.Enabled = True Then cmdAdd.Focus()
        Else
            MsgInformation("Record not saved")
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        If Err.Description = "" Then Exit Sub
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume				
    End Sub

    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If MainClass.SearchGridMaster(txtModelCode.Text, "GEN_MODEL_MST ", "MODEL_CODE", "MODEL_DESC", "", "", SqlStr) = True Then
            txtModelCode.Text = AcName
            If txtModelCode.Enabled = True Then txtModelCode.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub FrmModelWiseItemEntry_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Close()
    End Sub


    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        'Dim xIName As String = ""
        'Dim SqlStr As String
        'Dim mProductCode As String
        'Dim RsTemp As ADODB.Recordset
        If Trim(txtModelCode.Text) = "" Then Exit Sub

        If eventArgs.row = 0 And eventArgs.col = ColItemCode Then
            With SprdMain
                .Row = .ActiveRow

                .Col = ColItemCode
                If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", "", "", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow

                    .Col = ColItemCode
                    .Text = Trim(AcName)

                    .Col = ColItemDesc
                    .Text = Trim(AcName1)
                    SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColItemCode, SprdMain.ActiveRow, ColItemCode, SprdMain.ActiveRow, False))

                End If

            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColItemDesc Then
            With SprdMain
                .Row = .ActiveRow

                .Col = ColItemDesc

                If MainClass.SearchGridMaster("", "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", "", "", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow

                    .Col = ColItemCode
                    .Text = Trim(AcName1)

                    .Col = ColItemDesc
                    .Text = Trim(AcName)
                    SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColItemCode, SprdMain.ActiveRow, ColItemCode, SprdMain.ActiveRow, False))

                End If


            End With
        End If


        If eventArgs.col = 0 And eventArgs.row > 0 Then
            SprdMain.Row = eventArgs.row
            SprdMain.Col = ColItemCode
            If eventArgs.row < SprdMain.MaxRows And (ADDMode = True Or MODIFYMode = True) Then
                MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColItemCode)
                MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
                FormatSprdMain(eventArgs.row)
            End If
        End If

    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        If eventArgs.newRow = -1 Then Exit Sub
        SprdMain.Row = SprdMain.ActiveRow
        SprdMain.Col = ColItemCode
        If Trim(SprdMain.Text) = "" Then Exit Sub
        Select Case eventArgs.col
            Case ColItemCode
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColItemCode
                If DuplicateItem() = False Then
                    SprdMain.Row = SprdMain.ActiveRow
                    SprdMain.Col = ColItemCode
                    If FillItemDescPart(Trim(SprdMain.Text)) = False Then
                        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
                        eventArgs.cancel = True
                        Exit Sub
                    Else
                        MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight * 1.5)
                        FormatSprdMain((SprdMain.MaxRows))
                    End If
                Else
                    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
                    eventArgs.cancel = True
                    Exit Sub
                End If
        End Select
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Function DuplicateItem() As Boolean
        Dim cntRow As Integer
        Dim mCount As Byte
        Dim mCheckItemCode As String
        Dim mItemCode As String

        With SprdMain
            .Row = .ActiveRow
            .Col = ColItemCode
            mCheckItemCode = Trim(UCase(.Text))
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColItemCode
                mItemCode = Trim(UCase(.Text))

                If (mCheckItemCode = mItemCode And mCheckItemCode <> "") Then
                    mCount = mCount + 1
                End If

                If mCount > 1 Then
                    DuplicateItem = True
                    MsgInformation("Duplicate Item : " & mCheckItemCode)
                    Exit Function
                End If
            Next
        End With
    End Function
    Private Function FillItemDescPart(ByRef pItemCode As String) As Boolean
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset
        Dim mProductCode As String
        Dim mItemDesc As String
        Dim mItemUOM As String = ""

        FillItemDescPart = False
        If Trim(pItemCode) = "" Then Exit Function


        If MainClass.ValidateWithMasterTable(pItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mItemDesc = MasterNo
        Else
            MsgInformation("Invalid Product Code")
            FillItemDescPart = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable(pItemCode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mItemUOM = MasterNo
        End If

        SprdMain.Col = ColItemDesc
        SprdMain.Text = Trim(mItemDesc)

        SprdMain.Col = ColUOM
        SprdMain.Text = Trim(mItemUOM)

        FillItemDescPart = True
        Exit Function

        Exit Function
ERR1:
        FillItemDescPart = False
        MsgInformation(Err.Description)
    End Function

    Private Sub SprdView_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdView.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        With SprdView
            .Row = eventArgs.row
            .Col = 1
            txtModelCode.Text = .Text
            txtModelCode_Validating(txtModelCode, New System.ComponentModel.CancelEventArgs(False))
            If txtModelCode.Enabled = True Then txtModelCode.Focus()
            CmdView_Click(CmdView, New System.EventArgs())
        End With
    End Sub
    Private Function UpdateMain1() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mEntryDate As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = ""

        mEntryDate = VB6.Format(PubCurrDate, "DD-MMM-YYYY") & " " & GetServerTime()

        If ADDMode = True Then
            SqlStr = " INSERT INTO INV_MODELWISE_PROD_HDR  " & vbCrLf _
            & " (COMPANY_CODE, MODEL_CODE," & vbCrLf _
            & " ADDUSER,ADDDATE,MODUSER,MODDATE) " & vbCrLf _
            & " VALUES ( " & vbCrLf _
            & " " & RsCompany.Fields("COMPANY_CODE").Value & ",'" & MainClass.AllowSingleQuote(txtModelCode.Text) & "', " & vbCrLf _
            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & mEntryDate & "','DD-MON-YYYY HH24:MI'),'','')"
        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE INV_MODELWISE_PROD_HDR  SET " & vbCrLf _
                & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                & " MODDATE=TO_DATE('" & mEntryDate & "','DD-MON-YYYY HH24:MI')" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND MODEL_CODE='" & MainClass.AllowSingleQuote(txtModelCode.Text) & "'"
        End If

        PubDBCn.Execute(SqlStr)

        If UpdateDetail1() = False Then GoTo ErrPart


        UpdateMain1 = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:

        UpdateMain1 = False
        PubDBCn.RollbackTrans()
        RsModelMain.Requery()
        RsModelDetail.Requery()
        If Trim(Err.Description) <> "" Then
            MsgBox(Err.Description)
        End If
        If ADDMode = True Then
            lblMKey.Text = ""
            txtModelCode.Text = ""
        End If
        '    Resume				
    End Function
    Private Function UpdateDetail1() As Boolean

        On Error GoTo UpdateDetail1Err
        Dim SqlStr As String
        Dim I As Integer
        Dim mItemCode As String
        Dim mRemarks As String
        Dim mStoreLoc As String
        Dim mStdQty As Double = 0
        Dim mSOB As Double

        SqlStr = " DELETE FROM INV_MODELWISE_PROD_DET " & vbCrLf _
        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
        & " AND MODEL_CODE='" & MainClass.AllowSingleQuote(txtModelCode.Text) & "'"

        PubDBCn.Execute(SqlStr)


        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColRemarks
                mRemarks = MainClass.AllowSingleQuote(.Text)

                .Col = ColStdQty
                mStdQty = Val(.Text)

                If mItemCode <> "" Then
                    SqlStr = " INSERT INTO INV_MODELWISE_PROD_DET ( " & vbCrLf _
                        & " COMPANY_CODE,MODEL_CODE,SERIAL_NO,ITEM_CODE,REMARKS,PROD_STD_QTY) " & vbCrLf _
                        & " VALUES (" & RsCompany.Fields("COMPANY_CODE").Value & ",'" & MainClass.AllowSingleQuote(txtModelCode.Text) & "', " & I & "," & vbCrLf _
                        & " '" & mItemCode & "','" & mRemarks & "'," & mStdQty & ")"

                    PubDBCn.Execute(SqlStr)
                End If
NextRec:
            Next
        End With
        UpdateDetail1 = True
        Exit Function
UpdateDetail1Err:
        '    Resume				
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume				
    End Function

    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim mDeptCode As String
        Dim mCheckLastEntryDate As String
        Dim RsTemp As ADODB.Recordset
        Dim SqlStr As String
        Dim cntRow As Integer
        Dim mProductCode As String
        Dim mOPRCode As String
        Dim mOPRDesc As String
        Dim mTotalProduction As Double
        Dim mItemCode As String
        Dim mUOM As String
        Dim mReworkQty As Double
        Dim mStockQty As Double

        FieldsVarification = True

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Memo No or modify an existing Memo No")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsModelMain.EOF = True Then Exit Function

        If txtModelCode.Text = "" Then
            MsgBox("Model is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtModelCode.Focus()
            Exit Function
        End If


        '    Call txtModelCode_Validate(True)				


        If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "ItemCode Is Blank.") = False Then FieldsVarification = False : Exit Function


        Exit Function
err_Renamed:
        FieldsVarification = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume				
    End Function

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Public Sub FrmModelWiseItemEntry_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor


        Me.Text = "Model Wise Product Code Entry"

        SqlStr = ""
        SqlStr = "Select * from INV_MODELWISE_PROD_HDR  Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsModelMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from INV_MODELWISE_PROD_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsModelDetail, ADODB.LockTypeEnum.adLockReadOnly)

        AssignGrid(False)
        Call SetTextLengths()
        Clear1()
        If cmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo AssignGridErr
        Dim SqlStr As String

        SqlStr = ""
        SqlStr = " SELECT  IH.MODEL_CODE, MMST.MODEL_DESC " & vbCrLf _
            & " FROM INV_MODELWISE_PROD_HDR IH, GEN_MODEL_MST MMST  " & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.COMPANY_CODE=MMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.MODEL_CODE=MMST.MODEL_CODE"

        SqlStr = SqlStr & vbCrLf & " ORDER BY IH.MODEL_CODE"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
AssignGridErr:
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1

            .set_RowHeight(0, 400)

            .set_ColWidth(0, 600)
            .set_ColWidth(1, 1200)
            .Col = 1
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight

            .set_ColWidth(2, 1200)
            .Col = 2
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter

            .set_ColWidth(3, 1000)
            .set_ColWidth(4, 1000)
            .set_ColWidth(5, 1000)
            .set_ColWidth(6, 1000)
            .set_ColWidth(7, 1000)
            .set_ColWidth(8, 1000)
            .set_ColWidth(9, 1000)
            .set_ColWidth(10, 1000)

            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            SprdView.set_RowHeight(-1, 300)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''' OperationModeSingle				
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        With SprdMain
            .set_RowHeight(0, ConRowHeight * 2)
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight * 1.5)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsModelDetail.Fields("ITEM_CODE").DefinedSize
            .set_ColWidth(.Col, 8)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(.Col, 34)

            .Col = ColUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ISSUE_UOM", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(.Col, 6)

            .Col = ColStdQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsModelDetail.Fields("PROD_STD_QTY").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 10)

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = RsModelDetail.Fields("REMARKS").DefinedSize
            .set_ColWidth(.Col, 10)
        End With
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemDesc, ColUOM)
        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then RsModelDetail.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        With RsModelMain
            txtModelCode.MaxLength = .Fields("MODEL_CODE").DefinedSize
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Show1()

        On Error GoTo ERR1
        Dim mProdType As String


        With RsModelMain
            If Not .EOF Then
                txtModelCode.Text = IIf(IsDBNull(.Fields("MODEL_CODE").Value), "", .Fields("MODEL_CODE").Value)
                If MainClass.ValidateWithMasterTable(Trim(txtModelCode.Text), "MODEL_CODE", "MODEL_DESC", "GEN_MODEL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    lblModelDesc.Text = MasterNo
                Else
                    lblModelDesc.Text = ""
                End If
                Call ShowDetail1()
            End If
        End With
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsModelMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        SprdMain.Enabled = True
        txtModelCode.Enabled = True
        cmdSearch.Enabled = True
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume				
    End Sub
    Private Sub ShowDetail1()

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String
        Dim mItemCode As String

        SqlStr = " SELECT * " & vbCrLf _
        & " FROM INV_MODELWISE_PROD_DET  " & vbCrLf _
        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        & " AND MODEL_CODE = '" & MainClass.AllowSingleQuote(txtModelCode.Text) & "' " & vbCrLf _
        & " ORDER BY  SERIAL_NO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsModelDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsModelDetail
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            I = 1
            .MoveFirst()
            Do While Not .EOF
                SprdMain.Row = I

                SprdMain.Col = ColItemCode
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))
                mItemCode = Trim(IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value))

                SprdMain.Col = ColItemDesc
                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    SprdMain.Text = MasterNo
                Else
                    SprdMain.Text = ""
                End If

                SprdMain.Col = ColUOM
                If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "ISSUE_UOM", "INV_ITEM_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    SprdMain.Text = MasterNo
                Else
                    SprdMain.Text = ""
                End If

                SprdMain.Col = ColStdQty
                SprdMain.Text = Val(IIf(IsDBNull(.Fields("PROD_STD_QTY").Value), 0, .Fields("PROD_STD_QTY").Value))

                SprdMain.Col = ColRemarks
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value))
                .MoveNext()

                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With
        FormatSprdMain(-1)
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume				
    End Sub
    Private Sub ViewGrid()

        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        AdoDCMain.Refresh				
            FormatSprdView()
            SprdView.Focus()
            FraFront.SendToBack()
        Else
            CmdView.Text = ConCmdGridViewCaption
            FraFront.BringToFront()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsModelMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub Clear1()

        txtModelCode.Text = ""
        lblModelDesc.Text = ""
        txtModelCode.Enabled = True
        cmdSearch.Enabled = True
        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsModelMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub FrmModelWiseItemEntry_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub FrmModelWiseItemEntry_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Public Sub FrmModelWiseItemEntry_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Set PvtDBCn = New ADODB.Connection				
        'PvtDBCn.Open StrConn				


        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)

        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Top = 0
        Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(10935)
        'AdoDCMain.Visible = False
        FormActive = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        Dim mRow As Short


        mCol = SprdMain.ActiveCol
        mRow = SprdMain.ActiveRow

        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemDesc, 0))

        SprdMain.Refresh()
    End Sub
    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        '    With SprdMain				
        '        SprdMain_LeaveCell .ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False				
        '    End With				

    End Sub

    Private Sub txtModelCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtModelCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtModelCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtModelCode.DoubleClick
        Call cmdsearch_Click(cmdSearch, New System.EventArgs())
    End Sub
    Private Sub txtModelCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtModelCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtModelCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtModelCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtModelCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdSearch, New System.EventArgs())
    End Sub
    Public Sub txtModelCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtModelCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mModelNo As String

        If Trim(txtModelCode.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(Trim(txtModelCode.Text), "MODEL_CODE", "MODEL_DESC", "GEN_MODEL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            lblModelDesc.Text = MasterNo
        Else
            lblModelDesc.Text = ""
            MsgBox("No Such Model", vbInformation)
            Cancel = False
            Exit Sub
        End If


        If MODIFYMode = True And RsModelMain.EOF = False Then mModelNo = RsModelMain.Fields("MODEL_CODE").Value

        SqlStr = "Select * From INV_MODELWISE_PROD_HDR  " & vbCrLf _
        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        & " AND MODEL_CODE='" & MainClass.AllowSingleQuote(txtModelCode.Text) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsModelMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsModelMain.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Model", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * From INV_MODELWISE_PROD_HDR  " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MODEL_CODE='" & MainClass.AllowSingleQuote(mModelNo) & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsModelMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        'Resume				
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
