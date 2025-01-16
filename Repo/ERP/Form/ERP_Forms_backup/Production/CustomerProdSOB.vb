Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility

Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports System.Configuration
Imports System.Drawing.Color

Imports System.Drawing
Imports System.Drawing.Printing
Friend Class FrmCustomerProdSOB
    Inherits System.Windows.Forms.Form
    Dim RsCustSOBMain As ADODB.Recordset ''Recordset				
    Dim RsCustSOBDetail As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection				

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String

    Dim FormActive As Boolean

    Private Const ConRowHeight As Short = 12

    Private Const ColModelCode As Short = 1
    Private Const ColModelDesc As Short = 2
    Private Const ColStoreLoc As Short = 3
    Private Const ColItemCode As Short = 4
    Private Const ColItemDesc As Short = 5
    Private Const ColUOM As Short = 6
    Private Const ColStdQty As Short = 7
    Private Const ColSOB As Short = 8


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

        If Trim(txtCustomerCode.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub

        If Not RsCustSOBMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "DSP_CUST_SOB_HDR", (txtCustomerCode.Text), RsCustSOBMain, "SUPP_CUST_CODE") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "DSP_CUST_SOB_HDR", "SUPP_CUST_CODE", (txtCustomerCode.Text)) = False Then GoTo DelErrPart

                PubDBCn.Execute("DELETE FROM DSP_CUST_SOB_DET WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCustomerCode.Text) & "'")
                PubDBCn.Execute("DELETE FROM DSP_CUST_SOB_HDR WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCustomerCode.Text) & "'")

                PubDBCn.CommitTrans()
                RsCustSOBMain.Requery()
                RsCustSOBDetail.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        RsCustSOBMain.Requery()
        RsCustSOBDetail.Requery()
    End Sub
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdModify.Click

        On Error GoTo ModifyErr
        If cmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsCustSOBMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
            txtCustomerCode_Validating(txtCustomerCode, New System.ComponentModel.CancelEventArgs(False))
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

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"

        If MainClass.SearchGridMaster(txtCustomerCode.Text, "FIN_SUPP_CUST_MST ", "SUPP_CUST_NAME", "SUPP_CUST_CODE", "", "", SqlStr) = True Then
            txtCustomerCode.Text = AcName1
            lblCustomerName.Text = AcName
            If txtCustomerCode.Enabled = True Then txtCustomerCode.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub FrmCustomerProdSOB_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Close()
    End Sub
    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        'Dim xIName As String = ""
        Dim SqlStr As String = ""
        'Dim mProductCode As String
        'Dim RsTemp As ADODB.Recordset
        Dim mModelCode As String

        If eventArgs.col = ColModelCode Then
            SqlStr = "SELECT M.MODEL_CODE, M.MODEL_DESC, MP.ITEM_CODE, I.ITEM_SHORT_DESC, I.ISSUE_UOM, M.LOC_CODE, I.CUSTOMER_PART_NO "
        ElseIf eventArgs.col = ColItemCode Then
            SqlStr = "SELECT MP.ITEM_CODE, I.ITEM_SHORT_DESC, I.ISSUE_UOM, M.MODEL_CODE, M.MODEL_DESC, M.LOC_CODE, I.CUSTOMER_PART_NO "
        End If

        SqlStr = SqlStr & vbCrLf _
                & " FROM GEN_MODEL_MST M, INV_MODELWISE_PROD_DET MP, INV_ITEM_MST I" & vbCrLf _
                & " WHERE M.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND M.COMPANY_CODE=MP.COMPANY_CODE" & vbCrLf _
                & " AND M.MODEL_CODE=MP.MODEL_CODE" & vbCrLf _
                & " AND MP.COMPANY_CODE=I.COMPANY_CODE" & vbCrLf _
                & " AND MP.ITEM_CODE=I.ITEM_CODE"


        If Trim(txtCustomerCode.Text) = "" Then Exit Sub

        If eventArgs.row = 0 And eventArgs.col = ColModelCode Then
            With SprdMain
                .Row = .ActiveRow

                .Col = ColModelCode
                ''If MainClass.SearchGridMaster(.Text, "GEN_MODEL_MST", "MODEL_CODE", "MODEL_DESC", "", "", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColModelCode
                    .Text = Trim(AcName)

                    .Col = ColModelDesc
                    .Text = Trim(AcName1)

                    .Col = ColItemCode
                    .Text = Trim(AcName2)

                    .Col = ColItemDesc
                    .Text = Trim(AcName3)

                    .Col = ColUOM
                    .Text = Trim(AcName4)

                    .Col = ColStoreLoc
                    .Text = Trim(AcName5)

                    SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColModelCode, SprdMain.ActiveRow, ColModelCode, SprdMain.ActiveRow, False))

                End If

            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColItemCode Then
            With SprdMain
                .Row = .ActiveRow

                .Col = ColModelCode
                mModelCode = Trim(.Text)

                If mModelCode <> "" Then
                    SqlStr = SqlStr & vbCrLf _
                    & " AND MP.MODEL_CODE='" & MainClass.AllowSingleQuote(mModelCode) & "'"
                End If

                .Col = ColItemCode
                ''If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", "", "", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColItemCode
                    .Text = Trim(AcName)

                    .Col = ColItemDesc
                    .Text = Trim(AcName1)

                    .Col = ColUOM
                    .Text = Trim(AcName2)

                    .Col = ColModelCode
                    .Text = Trim(AcName3)

                    .Col = ColModelDesc
                    .Text = Trim(AcName4)

                    .Col = ColStoreLoc
                    .Text = Trim(AcName5)

                    SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColItemCode, SprdMain.ActiveRow, ColItemCode, SprdMain.ActiveRow, False))

                End If

            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColItemDesc Then
            With SprdMain
                .Row = .ActiveRow

                .Col = ColModelCode
                mModelCode = Trim(.Text)

                If mModelCode <> "" Then
                    SqlStr = SqlStr & vbCrLf _
                    & " AND MP.MODEL_CODE='" & MainClass.AllowSingleQuote(mModelCode) & "'"
                End If

                .Col = ColItemDesc

                'If MainClass.SearchGridMaster("", "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", "", "", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow


                    .Col = ColItemCode
                    .Text = Trim(AcName1)

                    .Col = ColItemDesc
                    .Text = Trim(AcName)

                    .Col = ColUOM
                    .Text = Trim(AcName2)

                    .Col = ColModelCode
                    .Text = Trim(AcName3)

                    .Col = ColModelDesc
                    .Text = Trim(AcName4)

                    .Col = ColStoreLoc
                    .Text = Trim(AcName5)

                    SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColItemCode, SprdMain.ActiveRow, ColItemCode, SprdMain.ActiveRow, False))

                End If


            End With
        End If

        'If eventArgs.row = 0 And eventArgs.col = ColStoreLoc Then
        '    With SprdMain
        '        .Row = .ActiveRow

        '        .Col = ColStoreLoc

        '        If MainClass.SearchGridMaster(.Text, "DSP_CUST_STORE_LOC_MST", "LOC_CODE", "LOC_DESCRIPTION", "", "", "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '            .Row = .ActiveRow

        '            .Col = ColStoreLoc
        '            .Text = Trim(AcName)

        '            SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColItemCode, SprdMain.ActiveRow, ColStoreLoc, SprdMain.ActiveRow, False))
        '        End If
        '    End With
        'End If

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
        Dim mModelCode As String

        If eventArgs.newRow = -1 Then Exit Sub
        SprdMain.Row = SprdMain.ActiveRow
        SprdMain.Col = ColModelCode
        If Trim(SprdMain.Text) = "" Then Exit Sub
        Select Case eventArgs.col
            Case ColModelCode
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColModelCode
                If Trim(SprdMain.Text) <> "" Then
                    If MainClass.ValidateWithMasterTable(Trim(SprdMain.Text), "MODEL_CODE", "MODEL_DESC", "GEN_MODEL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                        MsgInformation("Invalid Model.")
                        MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColModelCode)
                        eventArgs.cancel = True
                        Exit Sub
                    Else
                        SprdMain.Col = ColModelDesc
                        SprdMain.Text = Trim(MasterNo)

                        If MainClass.ValidateWithMasterTable(Trim(SprdMain.Text), "MODEL_CODE", "LOC_CODE", "GEN_MODEL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                            SprdMain.Col = ColModelDesc
                            SprdMain.Text = Trim(MasterNo)
                        End If
                    End If
                Else
                    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColModelCode)
                    eventArgs.cancel = True
                    Exit Sub
                End If
            Case ColItemCode
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColItemCode
                If Trim(SprdMain.Text) = "" Then Exit Sub

                If DuplicateItem() = False Then
                    SprdMain.Row = SprdMain.ActiveRow

                    SprdMain.Col = ColModelCode
                    mModelCode = Trim(SprdMain.Text)

                    SprdMain.Col = ColItemCode
                    If FillItemDescPart(Trim(SprdMain.Text), mModelCode) = False Then
                        MsgInformation("Invalid Product Code.")
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
                'Case ColStoreLoc
                '    SprdMain.Row = SprdMain.ActiveRow
                '    SprdMain.Col = ColStoreLoc
                '    If Trim(SprdMain.Text) <> "" Then
                '        If MainClass.ValidateWithMasterTable(Trim(SprdMain.Text), "LOC_CODE", "LOC_DESCRIPTION", "DSP_CUST_STORE_LOC_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                '            MsgInformation("Invalid Store Location.")
                '            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColStoreLoc)
                '            eventArgs.cancel = True
                '            Exit Sub
                '        End If
                '    End If
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
            .Col = ColModelCode
            mCheckItemCode = Trim(UCase(.Text))

            .Col = ColItemCode
            mCheckItemCode = mCheckItemCode & "-" & Trim(UCase(.Text))

            .Col = ColStoreLoc
            mCheckItemCode = mCheckItemCode & "-" & Trim(UCase(.Text))

            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColModelCode
                mItemCode = Trim(UCase(.Text))

                .Col = ColItemCode
                mItemCode = mItemCode & "-" & Trim(UCase(.Text))

                .Col = ColStoreLoc
                mItemCode = mItemCode & "-" & Trim(UCase(.Text))

                If (mCheckItemCode = mItemCode And mCheckItemCode <> "") Then
                    mCount = mCount + 1
                End If

                If mCount > 1 Then
                    DuplicateItem = True
                    MsgInformation("Duplicate Item  " & mCheckItemCode)
                    Exit Function
                End If
            Next
        End With
    End Function
    Private Function FillItemDescPart(ByVal pItemCode As String, ByVal pModelCode As String) As Boolean
        On Error GoTo ERR1
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mModelDesc As String
        Dim mItemDesc As String
        Dim mItemUOM As String = ""
        'Dim mModelCode As String

        FillItemDescPart = False
        If Trim(pItemCode) = "" Then Exit Function
        If Trim(pModelCode) = "" Then Exit Function

        SqlStr = "Select MP.ITEM_CODE, I.ITEM_SHORT_DESC, I.ISSUE_UOM, M.MODEL_CODE, M.MODEL_DESC "

        SqlStr = SqlStr & vbCrLf _
                & " FROM GEN_MODEL_MST M, INV_MODELWISE_PROD_DET MP, INV_ITEM_MST I" & vbCrLf _
                & " WHERE M.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " And M.COMPANY_CODE=MP.COMPANY_CODE" & vbCrLf _
                & " And M.MODEL_CODE=MP.MODEL_CODE" & vbCrLf _
                & " And MP.COMPANY_CODE=I.COMPANY_CODE" & vbCrLf _
                & " And MP.ITEM_CODE=I.ITEM_CODE" & vbCrLf _
                & " And MP.ITEM_CODE='" & MainClass.AllowSingleQuote(pItemCode) & "'" & vbCrLf _
                & " AND M.MODEL_CODE='" & MainClass.AllowSingleQuote(pModelCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            mItemDesc = IIf(IsDBNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value)
            mItemUOM = IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)
            'mModelCode = IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)
            mModelDesc = IIf(IsDBNull(RsTemp.Fields("MODEL_DESC").Value), "", RsTemp.Fields("MODEL_DESC").Value)
        Else
            MsgInformation("Invalid Product Code")
            FillItemDescPart = False
            Exit Function
        End If

        SprdMain.Col = ColItemDesc
        SprdMain.Text = Trim(mItemDesc)

        SprdMain.Col = ColUOM
        SprdMain.Text = Trim(mItemUOM)

        SprdMain.Col = ColModelDesc
        SprdMain.Text = Trim(mModelDesc)

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
            txtCustomerCode.Text = .Text
            txtCustomerCode_Validating(txtCustomerCode, New System.ComponentModel.CancelEventArgs(False))
            If txtCustomerCode.Enabled = True Then txtCustomerCode.Focus()
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
            SqlStr = " INSERT INTO DSP_CUST_SOB_HDR  " & vbCrLf _
                    & " (COMPANY_CODE, SUPP_CUST_CODE," & vbCrLf _
                    & " ADDUSER,ADDDATE,MODUSER,MODDATE) " & vbCrLf _
                    & " VALUES ( " & vbCrLf _
                    & " " & RsCompany.Fields("COMPANY_CODE").Value & ",'" & MainClass.AllowSingleQuote(txtCustomerCode.Text) & "', " & vbCrLf _
                    & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & mEntryDate & "','DD-MON-YYYY HH24:MI'),'','')"
        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE DSP_CUST_SOB_HDR  SET " & vbCrLf _
                & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                & " MODDATE=TO_DATE('" & mEntryDate & "','DD-MON-YYYY HH24:MI')" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCustomerCode.Text) & "'"
        End If

        PubDBCn.Execute(SqlStr)

        If UpdateDetail1() = False Then GoTo ErrPart


        UpdateMain1 = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:

        UpdateMain1 = False
        PubDBCn.RollbackTrans()
        RsCustSOBMain.Requery()
        RsCustSOBDetail.Requery()
        If Trim(Err.Description) <> "" Then
            MsgBox(Err.Description)
        End If
        If ADDMode = True Then
            lblMKey.Text = ""
            txtCustomerCode.Text = ""
        End If
        '    Resume				
    End Function
    Private Function UpdateDetail1() As Boolean

        On Error GoTo UpdateDetail1Err
        Dim SqlStr As String
        Dim I As Integer
        Dim mModelCode As String
        Dim mItemCode As String
        Dim mStoreLoc As String
        Dim mSOB As Double


        SqlStr = " DELETE FROM DSP_CUST_SOB_DET " & vbCrLf _
        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
        & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCustomerCode.Text) & "'"

        PubDBCn.Execute(SqlStr)


        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColItemCode
                mItemCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColModelCode
                mModelCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColStoreLoc
                mStoreLoc = MainClass.AllowSingleQuote(.Text)

                .Col = ColSOB
                mSOB = Val(.Text)
                If mItemCode <> "" Then
                    SqlStr = " INSERT INTO DSP_CUST_SOB_DET ( " & vbCrLf _
                        & " COMPANY_CODE, SUPP_CUST_CODE, SERIAL_NO, " & vbCrLf _
                        & " MODEL_CODE, ITEM_CODE,  " & vbCrLf _
                        & " PROD_SOB) " & vbCrLf _
                        & " VALUES (" & RsCompany.Fields("COMPANY_CODE").Value & ",'" & MainClass.AllowSingleQuote(txtCustomerCode.Text) & "', " & I & "," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mModelCode) & "', " & vbCrLf _
                        & " '" & mItemCode & "', " & mSOB & ")"

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
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String
        Dim cntRow As Integer
        Dim mProductCode As String
        Dim mModelCode As String
        Dim mStoreLoc As String
        Dim mSOB As Double
        Dim mUOM As String



        FieldsVarification = True

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Memo No or modify an existing Memo No")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsCustSOBMain.EOF = True Then Exit Function

        If txtCustomerCode.Text = "" Then
            MsgBox("Customer Code is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtCustomerCode.Focus()
            Exit Function
        End If

        'Dim mProductCode As String
        'Dim mModelCode As String
        'Dim mStoreLoc As String
        'Dim mSOB As Double
        'Dim mUOM As String

        With SprdMain
            For cntRow = 1 To .MaxRows - 1
                .Row = cntRow
                .Col = ColModelCode
                mModelCode = Trim(.Text)

                .Col = ColItemCode
                mProductCode = Trim(.Text)

                SqlStr = "SELECT MP.ITEM_CODE " & vbCrLf _
                        & " FROM GEN_MODEL_MST M, INV_MODELWISE_PROD_DET MP, INV_ITEM_MST I" & vbCrLf _
                        & " WHERE M.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                        & " AND M.COMPANY_CODE=MP.COMPANY_CODE" & vbCrLf _
                        & " AND M.MODEL_CODE=MP.MODEL_CODE" & vbCrLf _
                        & " AND MP.COMPANY_CODE=I.COMPANY_CODE" & vbCrLf _
                        & " AND MP.ITEM_CODE=I.ITEM_CODE" & vbCrLf _
                        & " AND MP.ITEM_CODE='" & MainClass.AllowSingleQuote(mProductCode) & "'" & vbCrLf _
                        & " AND M.MODEL_CODE='" & MainClass.AllowSingleQuote(mModelCode) & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = True Then
                    MsgInformation("Invalid Product Code " & mProductCode & " for Model - " & mModelCode)
                    MainClass.SetFocusToCell(SprdMain, cntRow, ColItemCode)
                    FieldsVarification = False
                    Exit Function
                End If

                '.Col = ColStoreLoc
                'If Trim(.Text) <> "" Then
                '    If MainClass.ValidateWithMasterTable(Trim(SprdMain.Text), "LOC_CODE", "LOC_DESCRIPTION", "DSP_CUST_STORE_LOC_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                '        MainClass.SetFocusToCell(SprdMain, cntRow, ColStoreLoc)
                '        FieldsVarification = False
                '        Exit Function
                '    End If
                'End If

                .Col = ColSOB
                mSOB = Val(.Text)
                If mSOB > 100 Then
                    MsgInformation("S.O.B. Cann't be greate than 100, Please check Model Code " & mModelCode)
                    MainClass.SetFocusToCell(SprdMain, cntRow, ColSOB)
                    FieldsVarification = False
                    Exit Function
                End If
            Next
        End With



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
    Public Sub FrmCustomerProdSOB_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor


        Me.Text = "Customer Wise Product SOB Entry"

        SqlStr = ""
        SqlStr = "Select * from DSP_CUST_SOB_HDR  Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCustSOBMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from DSP_CUST_SOB_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCustSOBDetail, ADODB.LockTypeEnum.adLockReadOnly)

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

        SqlStr = "" '
        SqlStr = " SELECT  IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, ID.MODEL_CODE, MMST.MODEL_DESC,  " & vbCrLf _
            & " ID.ITEM_CODE, INVMST.ITEM_SHORT_DESC, MMST.LOC_CODE, PROD_SOB " & vbCrLf _
            & " FROM DSP_CUST_SOB_HDR IH, DSP_CUST_SOB_DET ID, GEN_MODEL_MST MMST, INV_ITEM_MST INVMST, FIN_SUPP_CUST_MST CMST  " & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.COMPANY_CODE=ID.COMPANY_CODE" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=ID.SUPP_CUST_CODE" & vbCrLf _
            & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE" & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE" & vbCrLf _
            & " AND ID.COMPANY_CODE=MMST.COMPANY_CODE" & vbCrLf _
            & " AND ID.MODEL_CODE=MMST.MODEL_CODE" & vbCrLf _
            & " AND ID.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf _
            & " AND ID.ITEM_CODE=INVMST.ITEM_CODE"


        SqlStr = SqlStr & vbCrLf & " ORDER BY CMST.SUPP_CUST_NAME, INVMST.ITEM_SHORT_DESC"
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

            .Col = ColModelCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsCustSOBDetail.Fields("MODEL_CODE").DefinedSize
            .set_ColWidth(.Col, 8)

            .Col = ColModelDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("MODEL_DESC", "GEN_MODEL_MST", PubDBCn)
            .set_ColWidth(.Col, 12)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsCustSOBDetail.Fields("ITEM_CODE").DefinedSize
            .set_ColWidth(.Col, 8)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(.Col, 25)

            .Col = ColUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ISSUE_UOM", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(.Col, 6)

            .Col = ColStoreLoc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("LOC_CODE", "GEN_MODEL_MST", PubDBCn)
            .set_ColWidth(.Col, 10)

            .Col = ColSOB
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsCustSOBDetail.Fields("PROD_SOB").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 10)


        End With
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColModelDesc, ColModelDesc)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemDesc, ColUOM)
        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then RsCustSOBDetail.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        With RsCustSOBMain
            txtCustomerCode.MaxLength = .Fields("SUPP_CUST_CODE").DefinedSize
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Show1()

        On Error GoTo ERR1

        With RsCustSOBMain
            If Not .EOF Then
                txtCustomerCode.Text = IIf(IsDBNull(.Fields("SUPP_CUST_CODE").Value), "", .Fields("SUPP_CUST_CODE").Value)
                If MainClass.ValidateWithMasterTable(Trim(txtCustomerCode.Text), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    lblCustomerName.Text = MasterNo
                Else
                    lblCustomerName.Text = ""
                End If
                Call ShowDetail1()
            End If
        End With
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsCustSOBMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        SprdMain.Enabled = True
        txtCustomerCode.Enabled = True
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
        Dim mModelCode As String
        Dim mModelDesc As String

        SqlStr = " SELECT * " & vbCrLf _
                & " FROM DSP_CUST_SOB_DET  " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND SUPP_CUST_CODE = '" & MainClass.AllowSingleQuote(txtCustomerCode.Text) & "' " & vbCrLf _
                & " ORDER BY  SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCustSOBDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsCustSOBDetail
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            I = 1
            .MoveFirst()
            Do While Not .EOF
                SprdMain.Row = I

                SprdMain.Col = ColModelCode
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("MODEL_CODE").Value), "", .Fields("MODEL_CODE").Value))
                mModelCode = Trim(IIf(IsDBNull(.Fields("MODEL_CODE").Value), "", .Fields("MODEL_CODE").Value))

                SprdMain.Col = ColModelDesc
                If MainClass.ValidateWithMasterTable(mModelCode, "MODEL_CODE", "MODEL_DESC", "GEN_MODEL_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    SprdMain.Text = MasterNo
                Else
                    SprdMain.Text = ""
                End If

                SprdMain.Col = ColStoreLoc
                If MainClass.ValidateWithMasterTable(mModelCode, "MODEL_CODE", "LOC_CODE", "GEN_MODEL_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    SprdMain.Text = MasterNo
                Else
                    SprdMain.Text = ""
                End If

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


                SprdMain.Col = ColSOB
                SprdMain.Text = Val(IIf(IsDBNull(.Fields("PROD_SOB").Value), 0, .Fields("PROD_SOB").Value))

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
        MainClass.ButtonStatus(Me, XRIGHT, RsCustSOBMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub Clear1()

        txtCustomerCode.Text = ""
        lblCustomerName.Text = ""
        txtCustomerCode.Enabled = True
        cmdSearch.Enabled = True
        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsCustSOBMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub FrmCustomerProdSOB_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub FrmCustomerProdSOB_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Public Sub FrmCustomerProdSOB_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColModelCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColModelCode, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColItemDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemDesc, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColStoreLoc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColStoreLoc, 0))


        SprdMain.Refresh()
    End Sub
    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        '    With SprdMain				
        '        SprdMain_LeaveCell .ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False				
        '    End With				

    End Sub

    Private Sub txtCustomerCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustomerCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtCustomerCodeCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustomerCode.DoubleClick
        Call cmdsearch_Click(cmdSearch, New System.EventArgs())
    End Sub
    Private Sub txtCustomerCodeCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCustomerCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCustomerCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCustomerCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCustomerCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdSearch, New System.EventArgs())
    End Sub
    Public Sub txtCustomerCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCustomerCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mModelNo As String

        If Trim(txtCustomerCode.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(Trim(txtCustomerCode.Text), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND SUPP_CUST_TYPE IN ('S','C')") = True Then
            lblCustomerName.Text = MasterNo
        Else
            lblCustomerName.Text = ""
            MsgBox("No Such Customer", vbInformation)
            Cancel = False
            Exit Sub
        End If


        If MODIFYMode = True And RsCustSOBMain.EOF = False Then mModelNo = RsCustSOBMain.Fields("MODEL_CODE").Value

        SqlStr = "Select * From DSP_CUST_SOB_HDR  " & vbCrLf _
        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCustomerCode.Text) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCustSOBMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsCustSOBMain.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Customer", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * From DSP_CUST_SOB_HDR  " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mModelNo) & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCustSOBMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        'Resume				
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub CmdPopFromFile_Click(sender As Object, e As EventArgs) Handles CmdPopFromFile.Click
        Try
            Dim strFilePath As String = ""
            Dim intflag As Integer
            CommonDialogOpen.FileName = ""

            intflag = CommonDialogOpen.ShowDialog()

            If intflag = 1 Then
                If CommonDialogOpen.FileName <> "" Then
                    strFilePath = CommonDialogOpen.FileName
                    'strfilename = CommonDialogOpen.SafeFileName
                    Call PopulateFromXLSFile(strFilePath)
                End If
            End If

        Catch ex As Exception

        End Try
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
NormalExit:

    End Sub
    Private Sub PopulateFromXLSFile(ByVal strXLSFile As String)

        On Error GoTo ErrPart
        Dim I As Integer
        Dim SqlStr As String = ""

        Dim xSqlStr As String
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsFile As ADODB.Recordset
        Dim FileConnStr As String

        Dim mCustomerCode As String
        Dim mModelName As String
        Dim mStoreLoc As String
        Dim mItemCode As String
        Dim mItemPartNo As String
        Dim mItemDesc As String
        Dim mStdQty As Double
        Dim mSOB As Double
        Dim mUOM As String

        Dim strTemp As String
        Dim strWkShName As String
        Dim strError As String = ""
        Dim CntRow As Long = 1


        MainClass.ClearGrid(SprdMain)
        FormatSprdMain(-1)

        Dim ErrorFile As System.IO.StreamWriter


        Dim FileName As String = Path.GetFileName(strXLSFile)
        Dim Extension As String = Path.GetExtension(strXLSFile)


        Dim conStr As String = ""
        Select Case UCase(Extension)
            Case ".XLS"
                'Excel 97-03 
                conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & strXLSFile & ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'"
                Exit Select
            Case ".XLSX"
                'Excel 07 
                conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & strXLSFile & ";Extended Properties='Excel 12.0 Xml;HDR=Yes'"
                Exit Select
        End Select

        conStr = String.Format(conStr, strXLSFile)    ''isHDR='Yes'

        Dim connExcel As New OleDbConnection(conStr)
        Dim cmdExcel As New OleDbCommand()
        Dim oda As New OleDbDataAdapter()
        Dim dt As New DataTable()

        cmdExcel.Connection = connExcel

        'Get the name of First Sheet 
        connExcel.Open()
        Dim dtExcelSchema As DataTable
        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
        Dim SheetName As String = dtExcelSchema.Rows(0)("TABLE_NAME").ToString()

        connExcel.Close()

        'Read Data from First Sheet 
        connExcel.Open()

        cmdExcel.CommandText = "SELECT * From [" & SheetName & "]"      '' ORDER BY 4 DESC
        oda.SelectCommand = cmdExcel
        oda.Fill(dt)
        connExcel.Close()

        For Each dtRow In dt.Rows




            mCustomerCode = UCase(Trim(IIf(IsDBNull(dtRow.Item(0)), "", dtRow.Item(0))))
            If Trim(txtCustomerCode.Text) <> mCustomerCode Then GoTo NextRecord

            mModelName = UCase(Trim(IIf(IsDBNull(dtRow.Item(1)), "", dtRow.Item(1))))
            mStoreLoc = UCase(Trim(IIf(IsDBNull(dtRow.Item(2)), "", dtRow.Item(2))))
            mItemCode = UCase(Trim(IIf(IsDBNull(dtRow.Item(3)), "", dtRow.Item(3))))
            mItemPartNo = UCase(Trim(IIf(IsDBNull(dtRow.Item(4)), "", dtRow.Item(4))))
            mItemDesc = UCase(Trim(IIf(IsDBNull(dtRow.Item(5)), "", dtRow.Item(5))))
            mStdQty = Val(IIf(IsDBNull(dtRow.Item(6)), 0, dtRow.Item(6)))
            mSOB = Val(IIf(IsDBNull(dtRow.Item(7)), 0, dtRow.Item(7)))

            Dim mModelCode As String = ""
            If MainClass.ValidateWithMasterTable(Trim(mModelName), "MODEL_DESC", "MODEL_CODE", "GEN_MODEL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                mModelCode = MasterNo
            End If



            OpenLocalConnection()

            If Trim(mItemCode) <> "" Then
                xSqlStr = " SELECT ITEM_SHORT_DESC, ITEM_CODE, ISSUE_UOM " & vbCrLf _
                   & " FROM INV_ITEM_MST " & vbCrLf _
                   & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                   & " AND ITEM_CODE ='" & MainClass.AllowSingleQuote(mItemCode) & "'"
            Else
                xSqlStr = " SELECT ITEM_SHORT_DESC, ITEM_CODE, ISSUE_UOM " & vbCrLf _
                   & " FROM INV_ITEM_MST " & vbCrLf _
                   & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                   & " AND ITEM_CODE ='" & MainClass.AllowSingleQuote(mItemDesc) & "'"
            End If



            MainClass.UOpenRecordSet(xSqlStr, LocalPubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                mItemCode = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value))
                mItemDesc = Trim(IIf(IsDBNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value))
                mUOM = Trim(IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value))
            Else
                GoTo NextRecord
            End If

            SprdMain.Row = SprdMain.MaxRows

            SprdMain.Col = ColModelCode
            SprdMain.Text = mModelCode

            SprdMain.Col = ColModelDesc
            SprdMain.Text = mModelName

            SprdMain.Col = ColStoreLoc
            SprdMain.Text = mStoreLoc

            SprdMain.Col = ColItemCode
            SprdMain.Text = mItemCode

            SprdMain.Col = ColItemDesc
            SprdMain.Text = mItemDesc

            SprdMain.Col = ColUOM
            SprdMain.Text = mUOM

            SprdMain.Col = ColStdQty
            SprdMain.Text = mStdQty

            SprdMain.Col = ColSOB
            SprdMain.Text = mSOB


            SprdMain.MaxRows = SprdMain.MaxRows + 1
            CntRow = CntRow + 1

            RsTemp.Close()
            RsTemp = Nothing

            CloseLocalConnection()
NextRecord:

        Next

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '        Resume
    End Sub
End Class
