Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmSchdLocking
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
   Private Const ColUom As Short = 3
   Private Const ColFrom As Short = 4
   Private Const ColTo As Short = 5
   Private Const ColRemarks As Short = 6

   Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click

      On Error GoTo AddErr
      If CmdAdd.Text = ConCmdAddCaption Then
         ADDMode = True
         MODIFYMode = False
         Clear1()
         TxtSupplier.Enabled = True
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

   Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
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
            If InsertIntoDelAudit(PubDBCn, "INV_SCHD_LOCK_HDR", (TxtSupplier.Text), RsTransMain, "SUPP_CUST_CODE") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "INV_SCHD_LOCK_HDR", "SUPP_CUST_CODE || ':' || SCHD_DATE", TxtSupplier.Text & ":" & VB6.Format(txtMonth.Text, "dd-MMM-yyyy")) = False Then GoTo DelErrPart

                mSqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                         & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
                         & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtSupplier.Text) & "'" & vbCrLf _
                         & " AND SCHD_DATE=TO_DATE('" & VB6.Format(txtMonth.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY')"

                PubDBCn.Execute("Delete from INV_SCHD_LOCK_DET Where " & mSqlStr)
                PubDBCn.Execute("Delete from INV_SCHD_LOCK_HDR Where " & mSqlStr)

                PubDBCn.CommitTrans()
                RsTransMain.Requery() ''.Refresh
                RsTransDetail.Requery() ''.Refresh
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans() ''
        RsTransMain.Requery() ''.Refresh
        RsTransDetail.Requery() ''.Refresh
        If Err.Description <> "" Then
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdModify.Click

        On Error GoTo ModifyErr

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsTransMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
        Report1.ReportFileName = PubReportFolderPath & mRptFileName
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

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C')"


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

    Private Sub frmSchdLocking_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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

                SprdMain.Col = ColUom
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

        Dim CntRow As Integer
        Dim mCount As Byte
        Dim mCheckItemCode As String
        Dim mItemCode As String

        With SprdMain
            .Row = .ActiveRow
            .Col = ColItemCode
            mCheckItemCode = Trim(UCase(.Text))

            For CntRow = 1 To .MaxRows
                .Row = CntRow
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

                .Col = ColUom
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

                .Col = ColUom
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
            txtMonth.Text = CDate(.Text).ToString("dd/MM/yyyy")

            txtsupplier_Validating(txtSupplier, New System.ComponentModel.CancelEventArgs(False))
            If txtSupplier.Enabled = True Then txtSupplier.Focus()
            CmdView_Click(CmdView, New System.EventArgs())
        End With
    End Sub
    Private Function UpdateMain1() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mSchdDate As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()


        SqlStr = ""
        'mSchdDate = "01/" & VB6.Format(txtMonth.Text, "MM/YYYY")
        mSchdDate = "01/" & CDate(txtMonth.Text).Month & "/" & CDate(txtMonth.Text).Year

        If ADDMode = True Then
            SqlStr = "INSERT INTO INV_SCHD_LOCK_HDR (" & vbCrLf _
                     & " COMPANY_CODE, FYEAR, SUPP_CUST_CODE, SCHD_DATE, " & vbCrLf _
                     & " ADDUSER,ADDDATE,MODUSER,MODDATE)" & vbCrLf _
                     & " VALUES( " & vbCrLf _
                     & " " & RsCompany.Fields("Company_Code").Value & ", " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf _
                     & " '" & MainClass.AllowSingleQuote(txtSupplier.Text) & "', " & vbCrLf _
                     & " TO_DATE('" & CDate(mSchdDate).ToString("dd-MMM-yyyy") & "','DD-MON-YYYY'), " & vbCrLf _
                     & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & CDate(PubCurrDate).ToString("dd-MMM-yyyy") & "','DD-MON-YYYY'),'','')"
        ElseIf MODIFYMode = True Then
            SqlStr = ""
            SqlStr = "UPDATE INV_SCHD_LOCK_HDR SET " & vbCrLf _
               & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
               & " Moddate=TO_DATE('" & CDate(PubCurrDate).ToString("dd-MMM-yyyy") & "','DD-MON-YYYY') " & vbCrLf _
               & " WHERE COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
               & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf _
               & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtSupplier.Text) & "'" & vbCrLf _
               & " AND SCHD_DATE=TO_DATE('" & CDate(mSchdDate).ToString("dd-MMM-yyyy") & "','DD-MON-YYYY')"
        End If

        PubDBCn.Execute(SqlStr)

        If UpdateDetail1(mSchdDate) = False Then GoTo ErrPart

        UpdateMain1 = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
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
        ''Resume
    End Function

    Private Function UpdateDetail1(ByVal pSchdDate As String) As Boolean

        On Error GoTo UpdateDetail1
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mItemCode As String
        Dim mDateFrom As String
        Dim mDateTo As String
        Dim mRemarks As String

        SqlStr = " Delete From INV_SCHD_LOCK_DET " & vbCrLf _
              & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
              & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
              & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtSupplier.Text) & "'" & vbCrLf _
              & " AND SCHD_DATE=TO_DATE('" & CDate(pSchdDate).ToString("dd-MMM-yyyy") & "','DD-MON-YYYY')"

        PubDBCn.Execute(SqlStr)

        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColItemCode
                mItemCode = Trim(MainClass.AllowSingleQuote(.Text))

                .Col = ColFrom
                mDateFrom = CDate(.Text).ToString("dd-MMM-yyyy")

                .Col = ColTo
                mDateTo = CDate(.Text).ToString("dd-MMM-yyyy")

                .Col = ColRemarks
                mRemarks = Trim(MainClass.AllowSingleQuote(.Text))

                SqlStr = ""


                If mItemCode <> "" Then
                    SqlStr = " INSERT INTO INV_SCHD_LOCK_DET ( COMPANY_CODE, FYEAR, " & vbCrLf _
                       & " SUPP_CUST_CODE, SCHD_DATE, SERIAL_NO, " & vbCrLf _
                       & " ITEM_CODE, DATE_FROM, DATE_TO, " & vbCrLf _
                       & " REMARKS) "
                    SqlStr = SqlStr & vbCrLf _
                       & " VALUES (" & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf _
                       & " '" & MainClass.AllowSingleQuote(txtSupplier.Text) & "', " & vbCrLf _
                       & " TO_DATE('" & CDate(pSchdDate).ToString("dd-MMM-yyyy") & "','DD-MON-YYYY'), " & I & "," & vbCrLf _
                       & " '" & MainClass.AllowSingleQuote(mItemCode) & "', " & vbCrLf _
                       & " TO_DATE('" & CDate(mDateFrom).ToString("dd-MMM-yyyy") & "','DD-MON-YYYY'), " & vbCrLf _
                       & " TO_DATE('" & CDate(mDateTo).ToString("dd-MMM-yyyy") & "','DD-MON-YYYY'), " & vbCrLf _
                       & " '" & MainClass.AllowSingleQuote(mRemarks) & "') "

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
        Dim mSchdDate As String
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

        With SprdMain
            mSchdDate = VB6.Format(txtMonth.Text, "MM/YYYY")
            For mRow = 1 To .MaxRows
                .Row = mRow
                .Col = ColItemCode
                mItemCode = Trim(.Text)

                .Col = ColFrom
                mDetailFromDate = VB6.Format(.Text, "MM/YYYY")

                .Col = ColTo
                mDetailToDate = VB6.Format(.Text, "MM/YYYY")

                If mItemCode <> "" Then
                    If mSchdDate <> mDetailFromDate Then
                        MsgInformation("Not a Schedule Month")
                        MainClass.SetFocusToCell(SprdMain, mRow, ColFrom)
                        FieldsVarification = False
                        Exit Function
                    End If

                    If mSchdDate <> mDetailToDate Then
                        MsgInformation("Not a Schedule Month")
                        MainClass.SetFocusToCell(SprdMain, mRow, ColTo)
                        FieldsVarification = False
                        Exit Function
                    End If
                End If
            Next
        End With
        If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "ItemCode Is Blank.") = False Then FieldsVarification = False : Exit Function

        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Function

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub

    Public Sub frmSchdLocking_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = ""
        SqlStr = "Select * from INV_SCHD_LOCK_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTransMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from INV_SCHD_LOCK_DET Where 1<>1"
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

        SqlStr = "SELECT  IH.SUPP_CUST_CODE, CMST.SUPP_CUST_NAME, TO_CHAR(IH.SCHD_DATE,'DD/MM/YYYY') AS SCHD_DATE"

        ''FROM CLAUSE...

        SqlStr = SqlStr & vbCrLf & " FROM INV_SCHD_LOCK_HDR IH, FIN_SUPP_CUST_MST CMST"

        ''WHERE CLAUSE...

        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE "

        ''ORDER BY CLAUSE...

        SqlStr = SqlStr & vbCrLf & " Order by IH.SCHD_DATE, IH.SUPP_CUST_CODE"

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

            .Col = ColUom
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = MainClass.SetMaxLength("PURCHASE_UOM", "INV_ITEM_MST", PubDBCn) ''
            .set_ColWidth(ColUom, 4)

            .Col = ColFrom
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeDateFormat = SS_CELL_DATE_FORMAT_DDMMYY
            .set_ColWidth(.Col, 8)

            .Col = ColTo
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeDateFormat = SS_CELL_DATE_FORMAT_DDMMYY
            .set_ColWidth(.Col, 8)

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = MainClass.SetMaxLength("REMARKS", "INV_SCHD_LOCK_DET", PubDBCn)
            .set_ColWidth(ColRemarks, 25)

        End With
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemDesc, ColUom)
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
            txtMonth.MaxLength = 10
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub Show1()

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing

        MainClass.ClearGrid(SprdMain)

        With RsTransMain
            If Not .EOF Then
                txtSupplier.Text = IIf(IsDBNull(.Fields("SUPP_CUST_CODE").Value), "", .Fields("SUPP_CUST_CODE").Value)
                txtMonth.Text = VB6.Format(IIf(IsDBNull(.Fields("SCHD_DATE").Value), "", .Fields("SCHD_DATE").Value), "dd/MM/yyyy")

                If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    lblSupplierName.Text = Trim(MasterNo)
                End If

                Call ShowDetail1(.Fields("SUPP_CUST_CODE").Value, (txtMonth.Text))
                txtSupplier.Enabled = False
                cmdSearch.Enabled = False
            End If
        End With
        ADDMode = False
        MODIFYMode = False

        MainClass.ButtonStatus(Me, XRIGHT, RsTransMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

        '    FormatSprdMain -1

        SprdMain.Enabled = True    '' False Sandeep 15/05/2022
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub

    Private Sub ShowDetail1(ByVal pSuppCode As String, ByVal pSchdDate As String)

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mUOM As String = ""

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf _
              & " FROM INV_SCHD_LOCK_DET  " & vbCrLf _
              & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
              & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
              & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pSuppCode) & "'" & vbCrLf _
              & " AND SCHD_DATE=TO_DATE('" & CDate(pSchdDate).ToString("dd-MMM-yyyy") & "','DD-MON-YYYY')" & vbCrLf _
              & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTransDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsTransDetail
            If .EOF = True Then Exit Sub
            '        FormatSprdMain -1
            I = 1
            .MoveFirst()

            Do While Not .EOF

                SprdMain.Row = I

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


                SprdMain.Col = ColUom
                If MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Purchase_UOM", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mUOM = MasterNo
                Else
                    mUOM = ""
                End If
                SprdMain.Text = mUOM

                SprdMain.Col = ColFrom
                SprdMain.Text = CDate(IIf(IsDBNull(.Fields("DATE_FROM").Value), "", .Fields("DATE_FROM").Value)).ToString("dd/MM/yyyy")

                SprdMain.Col = ColTo
                SprdMain.Text = CDate(IIf(IsDBNull(.Fields("DATE_TO").Value), "", .Fields("DATE_TO").Value)).ToString("dd/MM/yyyy")

                SprdMain.Col = ColRemarks
                SprdMain.Text = IIf(IsDBNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)


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

        Call AutoCompleteSearch("FIN_SUPP_CUST_MST", "SUPP_CUST_CODE", "SUPP_CUST_TYPE IN ('S','C')", txtSupplier)
        MainClass.ButtonStatus(Me, XRIGHT, RsTransMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub Clear1()


        txtSupplier.Text = ""
        lblSupplierName.Text = ""
        txtMonth.Text = ""

        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsTransMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub frmSchdLocking_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmSchdLocking_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        '    MainClass.DoFunctionKey Me, KeyCode
    End Sub

    Public Sub frmSchdLocking_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

    Private Sub txtMonth_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMonth.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMonth_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMonth.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim mDate As String

        If Trim(txtMonth.Text) = "" Then GoTo EventExitSub

        If IsDate(txtMonth.Text) = False Then
            MsgInformation(" Invalid Date.")
            Cancel = True
        End If

        If FYChk((txtMonth.Text)) = False Then
            Cancel = True
            GoTo EventExitSub
        End If
        mDate = "01/" & CDate(txtMonth.Text).Month & "/" & CDate(txtMonth.Text).Year

        txtMonth.Text = CDate(mDate).ToString("dd/MM/yyyy")
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
        Dim mSchdDate As String = ""

        If TxtSupplier.Text = "" Then Exit Sub
        If txtMonth.Text = "" Then Exit Sub


        'If MODIFYMode = True And RsTransMain.EOF = False Then
        '   mSupplierCode = RsTransMain.Fields("SUPP_CUST_CODE").Value
        '   mSchdDate = VB6.Format(RsTransMain.Fields("SCHD_DATE").Value, "dd/MM/yyyy")
        'End If

        SqlStr = "Select * From INV_SCHD_LOCK_HDR " & vbCrLf _
              & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
              & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
              & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtSupplier.Text) & "'" & vbCrLf _
              & " AND SCHD_DATE=TO_DATE('" & CDate(txtMonth.Text).ToString("dd/MMM/yyyy") & "','DD-MON-YYYY')"

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
                SqlStr = "Select * From INV_SCHD_LOCK_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSupplierCode) & "'" & vbCrLf & " AND SCHD_DATE=TO_DATE('" & VB6.Format(mSchdDate, "dd-MMM-yyyy") & "','DD-MON-YYYY')"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTransMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
End Class
