Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmRGPApproval
    Inherits System.Windows.Forms.Form
    Dim RsTransMain As ADODB.Recordset ''Recordset
    Dim RsTransDetail As ADODB.Recordset ''Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim xMyMenu As String

    Dim FormActive As Boolean

    Private Const ConRowHeight As Short = 12

    Private Const ColItemCode As Short = 1
    Private Const ColItemDesc As Short = 2
    Private Const ColUOM As Short = 3
    Private Const ColQty As Short = 4
    Private Const ColRemarks As Short = 5

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click

        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtSupplier.Enabled = True
            cmdsearch.Enabled = True
            SprdMain.Enabled = True
        Else
            CmdAdd.Text = ConCmdAddCaption
            ADDMode = False
            MODIFYMode = False
            MainClass.ClearGrid(SprdMain)
            Call FormatSprdMain(-1)
            Show1()
        End If
        Exit Sub
AddErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdAuthSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAuthSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND RGP_AUTH='Y'"

        If ADDMode = True Then
            SqlStr = SqlStr & " AND (EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE='')"
        Else
            SqlStr = SqlStr & vbCrLf & " AND (EMP_LEAVE_DATE IS NULL OR (EMP_LEAVE_DATE >= TO_DATE('" & VB6.Format(txtRGPDate.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY')))"
        End If

        If MainClass.SearchGridMaster(txtAuthority.Text, "PAY_EMPLOYEE_MST", "EMP_CODE", "EMP_NAME", , , SqlStr) = True Then
            txtAuthority.Text = AcName
            txtAuthorityName.Text = AcName1
            txtAuthority_Validating(txtAuthority, New System.ComponentModel.CancelEventArgs(False))
            If txtAuthority.Enabled = True Then txtAuthority.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDelete.Click
        On Error GoTo DelErrPart
        Dim mSqlStr As String = ""


        If Not RsTransMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "INV_RGP_APP_HDR", (txtSupplier.Text), RsTransMain, "SUPP_CUST_CODE") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "INV_RGP_APP_HDR", "SUPP_CUST_CODE || ':' || RGP_DATE", txtSupplier.Text & ":" & CDate(txtRGPDate.Text).ToString("dd-MMM-yyyy")) = False Then GoTo DelErrPart

                mSqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
                            & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtSupplier.Text) & "'" & vbCrLf _
                            & " AND RGP_DATE=TO_DATE('" & CDate(txtRGPDate.Text).ToString("dd-MMM-yyyy") & "','DD-MON-YYYY') AND BOOKTYPE='" & lblBookType.Text & "'"


                PubDBCn.Execute("Delete from INV_RGP_APP_DET Where " & mSqlStr)
                PubDBCn.Execute("Delete from INV_RGP_APP_HDR Where " & mSqlStr)

                PubDBCn.CommitTrans()
                RsTransMain.Requery() ''.Refresh
                RsTransDetail.Requery() ''.Refresh
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        ''Resume
        PubDBCn.RollbackTrans() ''
        RsTransMain.Requery() ''.Refresh
        RsTransDetail.Requery() ''.Refresh
        If Err.Description <> "" Then
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdModify.Click

        On Error GoTo ModifyErr

        If cmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsTransMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True

        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportONPrint(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportONPrint(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportONPrint(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""
        Dim SqlStr As String = ""
        Dim mRptFileName As String
        Dim mVNo As String

        '    Report1.Reset
        '    MainClass.ClearCrptFormulas Report1
        '
        '    SqlStr = ""
        '
        '    Call MainClass.ClearCrptFormulas(Report1)
        '
        '
        '    mTitle = "Item RelationShip"
        '    mSubTitle = ""
        '    mRptFileName = "IR.rpt"
        '
        '    Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String)
        On Error GoTo ErrPart

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, True, xMyMenu)
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\" & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        Report1.Action = 1
        Report1.Reset()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
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
            txtsupplier_Validating(txtSupplier, New System.ComponentModel.CancelEventArgs(False))
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
        Dim SqlStr As String = ""

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster("", "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", "", , SqlStr) = True Then
            txtSupplier.Text = AcName1
            lblSupplierName.Text = AcName
            txtsupplier_Validating(txtSupplier, New System.ComponentModel.CancelEventArgs(False))
            If txtSupplier.Enabled = True Then txtSupplier.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmRGPApproval_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        On Error GoTo ErrPart
        Dim xIName As String
        Dim xSupp As String
        Dim SqlStr As String = ""

        If eventArgs.row = 0 And eventArgs.col = ColItemCode Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode
                If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow
                    .Col = ColItemCode
                    .Text = AcName
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColItemDesc Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemDesc
                xIName = .Text
                .Text = ""
                If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow
                    .Col = ColItemDesc
                    .Text = AcName
                Else
                    .Row = .ActiveRow
                    .Col = ColItemDesc
                    .Text = xIName
                End If
                MainClass.ValidateWithMasterTable(.Text, "ITEM_SHORT_DESC", "ITEM_CODE", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                .Row = .ActiveRow
                .Col = ColItemCode
                .Text = MasterNo
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColItemCode)
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
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdMain_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdMain.KeyDownEvent

        Dim mActiveCol As Integer

        mActiveCol = SprdMain.ActiveCol

        If eventArgs.keyCode = System.Windows.Forms.Keys.Return Or eventArgs.keyCode = System.Windows.Forms.Keys.Tab Then
            If mActiveCol = ColItemCode Then
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColItemCode
                If Trim(SprdMain.Text) <> "" Then
                    If SprdMain.MaxRows = SprdMain.ActiveRow Then
                        MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight)
                        FormatSprdMain((SprdMain.MaxRows))
                    End If
                End If
                '            SprdMain.Row = SprdMain.MaxRows
            End If
        ElseIf eventArgs.keyCode = System.Windows.Forms.Keys.F1 Then
            If mActiveCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
            If mActiveCol = ColItemDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemDesc, 0))
            '    Else
            '        MainClass.SetFocusToCell SprdMain, SprdMain.ActiveRow, mActiveCol
        End If
        eventArgs.keyCode = 9999
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell
        On Error GoTo ErrPart
        Dim mItemCode As String
        Dim mUOM As String = ""

        If eventArgs.newRow = -1 Then Exit Sub

        SprdMain.Row = SprdMain.ActiveRow

        Select Case eventArgs.col
            Case ColItemCode
                SprdMain.Col = ColItemCode
                Call FillItemDescFromItemCode((SprdMain.Text))
                If DuplicateItem() = False Then
                    '                FormatSprdMain -1
                End If
                SprdMain.Row = SprdMain.ActiveRow
                mItemCode = Trim(SprdMain.Text)

                SprdMain.Col = ColUOM
                mUOM = Trim(SprdMain.Text)

            Case ColItemDesc
                SprdMain.Col = ColItemDesc
                Call FillItemDescFromItemDesc((SprdMain.Text))
                If DuplicateItem() = False Then
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

                If (mItemCode = mCheckItemCode And mCheckItemCode <> "") Then
                    mCount = mCount + 1
                End If

                If mCount > 1 Then
                    DuplicateItem = True
                    MsgInformation("Duplicate Item : " & mCheckItemCode)
                    MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColItemCode)
                    Exit Function
                End If
            Next
        End With
    End Function


    Private Sub FillItemDescFromItemCode(ByRef pItemCode As String)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        If Trim(pItemCode) = "" Then Exit Sub


        With SprdMain
            If Trim(pItemCode) = Trim(txtSupplier.Text) Then
                MsgInformation("Item Cann't be Equal to 'From Item'")
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColItemCode)
                Exit Sub
            End If

            SqlStr = "SELECT ITEM_SHORT_DESC,ISSUE_UOM " & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND LTRIM(RTRIM(ITEM_CODE))='" & MainClass.AllowSingleQuote(pItemCode) & "'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                .Row = .ActiveRow
                .Col = ColItemDesc
                .Text = IIf(IsDBNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value)

                .Col = ColUOM
                .Text = IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)

            Else
                MsgInformation("Invaild Item Code")
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColItemCode)
            End If
        End With
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub FillItemDescFromItemDesc(ByRef pItemDesc As String)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        If Trim(pItemDesc) = "" Then Exit Sub
        With SprdMain
            SqlStr = "SELECT ITEM_CODE,CUSTOMER_PART_NO,ISSUE_UOM " & vbCrLf _
                & " FROM INV_ITEM_MST " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND LTRIM(RTRIM(ITEM_SHORT_DESC))='" & MainClass.AllowSingleQuote(pItemDesc) & "'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                .Row = .ActiveRow
                .Col = ColItemCode
                .Text = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)

                .Col = ColUOM
                .Text = IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)
            Else
                MsgInformation("Invaild Item Description")
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColItemCode)
            End If
        End With
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub SprdView_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdView.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        With SprdView
            .Row = eventArgs.row
            .Col = 1
            txtSupplier.Text = .Text

            .Col = 3
            txtRGPDate.Text = .Text

            txtsupplier_Validating(txtSupplier, New System.ComponentModel.CancelEventArgs(False))
            If txtSupplier.Enabled = True Then txtSupplier.Focus()
            CmdView_Click(CmdView, New System.EventArgs())
        End With
    End Sub
    Private Function UpdateMain1() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mRGPDate As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()


        SqlStr = ""
        mRGPDate = CDate(txtRGPDate.Text).ToString("dd/MM/yyyy")

        If ADDMode = True Then
            SqlStr = "INSERT INTO INV_RGP_APP_HDR (" & vbCrLf _
                & " COMPANY_CODE, FYEAR, SUPP_CUST_CODE, RGP_DATE, AUTH_GIVEN_BY," & vbCrLf _
                & " ADDUSER,ADDDATE,MODUSER,MODDATE,BOOKTYPE)" & vbCrLf _
                & " VALUES( " & vbCrLf _
                & " " & RsCompany.Fields("Company_Code").Value & ", " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtSupplier.Text) & "', " & vbCrLf _
                & " TO_DATE('" & CDate(mRGPDate).ToString("dd-MMM-yyyy") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtAuthority.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & CDate(PubCurrDate).ToString("dd-MMM-yyyy") & "','DD-MON-YYYY'),'','','" & lblBookType.Text & "')"
        ElseIf MODIFYMode = True Then
            SqlStr = ""
            SqlStr = "UPDATE INV_RGP_APP_HDR SET " & vbCrLf _
                & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                & " Moddate=TO_DATE('" & CDate(PubCurrDate).ToString("dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
                & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
                & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtSupplier.Text) & "'" & vbCrLf _
                & " AND RGP_DATE=TO_DATE('" & CDate(mRGPDate).ToString("dd-MMM-yyyy") & "','DD-MON-YYYY') AND BOOKTYPE='" & lblBookType.Text & "'"
        End If

        PubDBCn.Execute(SqlStr)

        If UpdateDetail1(mRGPDate) = False Then GoTo ErrPart

        UpdateMain1 = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        '    Resume
        UpdateMain1 = False
        PubDBCn.RollbackTrans() ''
        RsTransMain.Requery() ''.Refresh
        RsTransDetail.Requery() ''.Refresh
        If Err.Description = "" Then Exit Function
        If Err.Number = -2147217900 Then
            ErrorMsg("Duplicate Ref Generated, Save Again", "Duplicate", MsgBoxStyle.Critical)
        Else
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If

    End Function

    Private Function UpdateDetail1(ByRef pRGPDate As String) As Boolean

        On Error GoTo UpdateDetail1
        Dim SqlStr As String = ""
        Dim i As Integer
        Dim mItemCode As String
        Dim mRemarks As String
        Dim mQty As Double

        SqlStr = " Delete From INV_RGP_APP_DET " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtSupplier.Text) & "'" & vbCrLf _
                & " AND RGP_DATE=TO_DATE('" & CDate(pRGPDate).ToString("dd-MMM-yyyy") & "','DD-MON-YYYY') AND BOOKTYPE='" & lblBookType.Text & "'"


        PubDBCn.Execute(SqlStr)

        With SprdMain
            For i = 1 To .MaxRows - 1
                .Row = i

                .Col = ColItemCode
                mItemCode = Trim(MainClass.AllowSingleQuote(.Text))

                .Col = ColQty
                mQty = CDbl(VB6.Format(.Text, "0.00"))

                .Col = ColRemarks
                mRemarks = MainClass.AllowSingleQuote(.Text)

                SqlStr = ""


                If mItemCode <> "" Then
                    SqlStr = " INSERT INTO INV_RGP_APP_DET ( COMPANY_CODE, FYEAR, " & vbCrLf _
                        & " SUPP_CUST_CODE, RGP_DATE, SERIAL_NO, " & vbCrLf _
                        & " ITEM_CODE, APPROVED_QTY, " & vbCrLf _
                        & " REMARKS,BOOKTYPE) "
                    SqlStr = SqlStr & vbCrLf _
                        & " VALUES (" & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(txtSupplier.Text) & "', " & vbCrLf _
                        & " TO_DATE('" & CDate(pRGPDate).ToString("dd-MMM-yyyy") & "','DD-MON-YYYY'), " & i & "," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mItemCode) & "', " & Val(CStr(mQty)) & ", " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mRemarks) & "','" & lblBookType.Text & "') "

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        UpdateDetail1 = True
        Exit Function
UpdateDetail1:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function

    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim mRow As Integer
        Dim mItemCode As String
        Dim mRGPDate As String
        Dim mDetailFromDate As String
        Dim mDetailToDate As String

        FieldsVarification = True

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsTransMain.EOF = True Then Exit Function

        If txtSupplier.Text = "" Then
            MsgInformation("Supplier can not be Blank")
            FieldsVarification = False
            Exit Function
        End If

        If txtAuthority.Text = "" Then
            MsgBox("Authority code is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtAuthority.Focus()
            Exit Function
        End If

        '    With SprdMain
        '        mSchdDate = VB6.Format(txtRGPDate.Text, "MM/YYYY")
        '        For mRow = 1 To .MaxRows
        '            .Row = mRow
        '            .Col = ColItemCode
        '            mItemCode = Trim(.Text)
        '
        '            .Col = ColFrom
        '            mDetailFromDate = VB6.Format(.Text, "MM/YYYY")
        '
        '            .Col = ColTo
        '            mDetailToDate = VB6.Format(.Text, "MM/YYYY")
        '
        '            If mItemCode <> "" Then
        '                If mSchdDate <> mDetailFromDate Then
        '                    MsgInformation "Not a Schedule Month"
        '                    MainClass.SetFocusToCell SprdMain, mRow, ColFrom
        '                    FieldsVarification = False
        '                    Exit Function
        '                End If
        '
        '                If mSchdDate <> mDetailToDate Then
        '                    MsgInformation "Not a Schedule Month"
        '                    MainClass.SetFocusToCell SprdMain, mRow, ColTo
        '                    FieldsVarification = False
        '                    Exit Function
        '                End If
        '            End If
        '        Next
        '    End With

        If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "ItemCode Is Blank.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColQty, "N", "Qty Is Blank.") = False Then FieldsVarification = False : Exit Function


        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Function

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Public Sub frmRGPApproval_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = ""
        SqlStr = "Select * from INV_RGP_APP_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTransMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from INV_RGP_APP_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTransDetail, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)
        '    Call SetTextLengths

        If cmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ''Resume
    End Sub

    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo AssignGridErr
        Dim SqlStr As String = ""

        SqlStr = ""

        ''SELECT CLAUSE...

        SqlStr = "SELECT  IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, TO_CHAR(IH.RGP_DATE,'DD/MM/YYYY') AS RGP_DATE"

        ''FROM CLAUSE...

        SqlStr = SqlStr & vbCrLf & " FROM INV_RGP_APP_HDR IH, FIN_SUPP_CUST_MST CMST"

        ''WHERE CLAUSE...

        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf _
            & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE AND IH.BOOKTYPE='" & lblBookType.Text & "'"

        ''ORDER BY CLAUSE...

        SqlStr = SqlStr & vbCrLf & " Order by IH.RGP_DATE, IH.SUPP_CUST_CODE"

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
            .set_ColWidth(1, 1000)
            .set_ColWidth(2, 3500)
            .set_ColWidth(3, 1500)

            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            SprdView.set_RowHeight(-1, 300)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub

    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        With SprdMain
            .set_RowHeight(0, ConRowHeight * 2.5)
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight * 1.5)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsTransDetail.Fields("ITEM_CODE").DefinedSize ''
            .set_ColWidth(ColItemCode, 10)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(ColItemDesc, 25)

            .Col = ColUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = MainClass.SetMaxLength("PURCHASE_UOM", "INV_ITEM_MST", PubDBCn) ''
            .set_ColWidth(ColUOM, 4)

            .Col = ColQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColQty, 12)

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = MainClass.SetMaxLength("REMARKS", "INV_RGP_APP_DET", PubDBCn)
            .set_ColWidth(ColRemarks, 25)

        End With
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemDesc, ColUOM)
        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then RsTransDetail.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SetTextLengths()
        On Error GoTo ERR1
        With RsTransMain
            txtSupplier.MaxLength = .Fields("SUPP_CUST_CODE").DefinedSize
            txtRGPDate.MaxLength = 10
            txtAuthority.MaxLength = .Fields("AUTH_GIVEN_BY").DefinedSize

        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub Show1()

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim mRGPDate As String
        MainClass.ClearGrid(SprdMain)

        With RsTransMain
            If Not .EOF Then
                txtSupplier.Text = IIf(IsDBNull(.Fields("SUPP_CUST_CODE").Value), "", .Fields("SUPP_CUST_CODE").Value)
                mRGPDate = IIf(IsDBNull(.Fields("RGP_DATE").Value), "", .Fields("RGP_DATE").Value)
                txtRGPDate.Text = CDate(mRGPDate).ToString("dd/MM/yyyy")

                If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    lblSupplierName.Text = Trim(MasterNo)
                End If

                txtAuthority.Text = IIf(IsDBNull(.Fields("AUTH_GIVEN_BY").Value), "", .Fields("AUTH_GIVEN_BY").Value)
                If MainClass.ValidateWithMasterTable(txtAuthority.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtAuthorityName.Text = MasterNo
                End If

                Call ShowDetail1(.Fields("SUPP_CUST_CODE").Value, (txtRGPDate.Text))
                txtSupplier.Enabled = False
                cmdSearch.Enabled = False
                txtRGPDate.Enabled = False
                txtAuthority.Enabled = False
                cmdAuthSearch.Enabled = False
            End If
        End With
        ADDMode = False
        MODIFYMode = False

        MainClass.ButtonStatus(Me, XRIGHT, RsTransMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

        '    FormatSprdMain -1

        SprdMain.Enabled = True
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub

    Private Sub ShowDetail1(ByVal pSuppCode As String, ByVal pRGPDate As String)

        On Error GoTo ERR1
        Dim i As Integer
        Dim SqlStr As String = ""
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mUOM As String = ""

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf _
                & " FROM INV_RGP_APP_DET  " & vbCrLf _
                & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pSuppCode) & "'" & vbCrLf _
                & " AND RGP_DATE=TO_DATE('" & CDate(pRGPDate).ToString("dd-MMM-yyyy") & "','DD-MON-YYYY') AND BOOKTYPE='" & lblBookType.Text & "'" & vbCrLf _
                & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTransDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsTransDetail
            If .EOF = True Then Exit Sub
            '        FormatSprdMain -1
            i = 1
            .MoveFirst()

            Do While Not .EOF

                SprdMain.Row = i

                SprdMain.Col = ColItemCode
                mItemCode = IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                SprdMain.Text = mItemCode

                SprdMain.Col = ColItemDesc
                If MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mItemDesc = MasterNo
                Else
                    mItemDesc = ""
                End If
                SprdMain.Text = mItemDesc


                SprdMain.Col = ColUOM
                If MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Purchase_UOM", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mUOM = MasterNo
                Else
                    mUOM = ""
                End If
                SprdMain.Text = mUOM

                SprdMain.Col = ColQty
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("APPROVED_QTY").Value), 0, .Fields("APPROVED_QTY").Value), "0.00")

                SprdMain.Col = ColRemarks
                SprdMain.Text = IIf(IsDBNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)


                .MoveNext()

                i = i + 1
                SprdMain.MaxRows = i
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
        MainClass.ButtonStatus(Me, XRIGHT, RsTransMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub Clear1()


        txtSupplier.Text = ""
        lblSupplierName.Text = ""
        txtRGPDate.Text = ""
        txtAuthority.Text = ""
        txtAuthorityName.Text = ""

        txtSupplier.Enabled = True
        cmdSearch.Enabled = True
        txtRGPDate.Enabled = True
        txtAuthority.Enabled = True
        cmdAuthSearch.Enabled = True

        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)

        Call AutoCompleteSearch("FIN_SUPP_CUST_MST", "SUPP_CUST_CODE", "SUPP_CUST_TYPE IN ('S','C')", txtSupplier)
        Call AutoCompleteSearch("PAY_EMPLOYEE_MST", "EMP_CODE", "(EMP_LEAVE_DATE IS NULL OR EMP_LEAVE_DATE='')", txtAuthority)
        MainClass.ButtonStatus(Me, XRIGHT, RsTransMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub frmRGPApproval_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmRGPApproval_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        '    MainClass.DoFunctionKey Me, KeyCode
    End Sub

    Public Sub frmRGPApproval_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        xMyMenu = myMenu
        MainClass.RightsToButton(Me, XRIGHT)

        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Call SetMainFormCordinate(Me)
        'Me.Top = 0
        'Me.Left = 0
        ''Me.Height = VB6.TwipsToPixelsY(7245)
        ''Me.Width = VB6.TwipsToPixelsX(11355)

        AdoDCMain.Visible = False

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
        mCol = SprdMain.ActiveCol

        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemDesc, 0))
        SprdMain.Refresh()
    End Sub

    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        With SprdMain
            SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub

    Private Sub txtAuthority_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAuthority.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAuthority_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAuthority.DoubleClick
        Call cmdAuthSearch_Click(cmdAuthSearch, New System.EventArgs())
    End Sub

    Private Sub txtAuthority_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAuthority.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtAuthority.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtAuthority_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtAuthority.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtAuthority_DoubleClick(txtAuthority, New System.EventArgs())
    End Sub

    Private Sub txtAuthority_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAuthority.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        If Trim(txtAuthority.Text) = "" Then GoTo EventExitSub

        txtAuthority.Text = VB6.Format(Trim(txtAuthority.Text), "000000")
        If MainClass.ValidateWithMasterTable(txtAuthority.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND RGP_AUTH='Y'") = True Then
            txtAuthorityName.Text = MasterNo
        Else
            MsgInformation("Invalid Employee Code")
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRGPDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRGPDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRGPDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRGPDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtRGPDate.Text) = "" Then GoTo EventExitSub

        If IsDate(txtRGPDate.Text) = False Then
            MsgInformation(" Invalid Date.")
            Cancel = True
        End If

        If FYChk((txtRGPDate.Text)) = False Then
            Cancel = True
            GoTo EventExitSub
        End If

        txtRGPDate.Text = CDate(txtRGPDate.Text).ToString("dd/MM/yyyy")
        Call ShowRecord()
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSupplier_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplier.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtSupplier_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplier.DoubleClick
        Call cmdsearch_Click(cmdSearch, New System.EventArgs())
    End Sub
    Private Sub txtSupplier_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSupplier.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSupplier.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSupplier_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSupplier.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtSupplier_DoubleClick(txtSupplier, New System.EventArgs())
    End Sub
    Private Sub txtsupplier_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSupplier.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mItemCode As String

        If txtSupplier.Text = "" Then GoTo EventExitSub

        SqlStr = " SELECT SUPP_CUST_NAME" & vbCrLf _
                & " FROM FIN_SUPP_CUST_MST " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtSupplier.Text) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            lblSupplierName.Text = IIf(IsDBNull(RsTemp.Fields("SUPP_CUST_NAME").Value), "", RsTemp.Fields("SUPP_CUST_NAME").Value)
        Else
            MsgInformation("Invalid Supplier Code")
            Cancel = True
        End If

        Call ShowRecord()
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub ShowRecord()

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mSupplierCode As String = ""
        Dim mRGPDate As String = ""

        If txtSupplier.Text = "" Then Exit Sub
        If txtRGPDate.Text = "" Then Exit Sub


        'If MODIFYMode = True And RsTransMain.EOF = False Then
        '      mSupplierCode = RsTransMain.Fields("SUPP_CUST_CODE").Value
        '      mRGPDate = RsTransMain.Fields("RGP_DATE").Value
        '      mRGPDate = CDate(mRGPDate).ToString("dd/MM/yyyy")
        'End If

        SqlStr = "Select * From INV_RGP_APP_HDR " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtSupplier.Text) & "'" & vbCrLf _
                & " AND RGP_DATE=TO_DATE('" & CDate(txtRGPDate.Text).ToString("dd-MMM-yyyy") & "','DD-MON-YYYY') AND BOOKTYPE='" & lblBookType.Text & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTransMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTransMain.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Record, Click add to Generate New", MsgBoxStyle.Information)
                Exit Sub
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * From INV_RGP_APP_HDR " & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
                    & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSupplierCode) & "'" & vbCrLf _
                    & " AND RGP_DATE=TO_DATE('" & CDate(txtRGPDate.Text).ToString("dd-MMM-yyyy") & "','DD-MON-YYYY') AND BOOKTYPE='" & lblBookType.Text & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTransMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
End Class
