Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmSubCatMst
   Inherits System.Windows.Forms.Form
   Dim RsSubCatMast As ADODB.Recordset ''ADODB.Recordset
   Dim RsSubQCMst As ADODB.Recordset
   ''Private PvtDBCn As ADODB.Connection

   ''Dim RsOpOuts As ADODB.Recordset

   Dim NewCode As Short
   Dim ADDMode As Boolean
   Dim MODIFYMode As Boolean
   Dim XRIGHT As String
   Dim FormActive As Boolean
   Dim mSubCatCode As String

   Private Const ConRowHeight As Short = 14

   Private Const ColDivCode As Short = 1
   Private Const ColDivDesc As Short = 2
   Private Const ColQCEmpCode As Short = 3
   Private Const ColQCEmpName As Short = 4

   Private Sub chkApproved_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkApproved.CheckStateChanged

      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub

   Private Sub chkER6_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkER6.CheckStateChanged

      MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
   End Sub

   Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
      If CmdAdd.Text = ConCmdAddCaption Then
         CmdAdd.Text = ConCmdCancelCaption
         ADDMode = True
         MODIFYMode = False
         Clear1()
         SprdMain.Enabled = True
         txtCatCode.Focus()
      Else
         CmdAdd.Text = ConCmdAddCaption
         ADDMode = False
         MODIFYMode = False
         Show1()
      End If
   End Sub
   Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
      On Error Resume Next
        Me.Hide()
        Me.Close()
   End Sub

   Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
      On Error GoTo DelErrPart
        Dim SqlStr As String = ""

        If txtSubCatName.Text = "" Then Call ErrorMsg("Nothing to Delete", "", MsgBoxStyle.Critical) : Exit Sub

        If CheckConsolidatedMaster("INV_ITEM_MST") = True Then
            MsgInformation("Cann't be Delete.")
            Exit Sub
        End If

        If Not RsSubCatMast.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()

                If InsertIntoDelAudit(PubDBCn, "INV_SUBCATEGORY_MST", (txtSubCatName.Text), RsSubCatMast, "SUBCATEGORY_DESC") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "INV_SUBCATEGORY_MST", "SUBCATEGORY_CODE || ':' || CATEGORY_CODE", RsSubCatMast.Fields("SUBCATEGORY_CODE").Value & ":" & RsSubCatMast.Fields("CATEGORY_CODE").Value) = False Then GoTo DelErrPart


                SqlStr = " DELETE From INV_SUBCATEGORY_MST WHERE " & vbCrLf _
                   & " COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & "" & vbCrLf _
                   & " AND SUBCATEGORY_CODE='" & RsSubCatMast.Fields("SUBCATEGORY_CODE").Value & "'" & vbCrLf _
                   & " AND CATEGORY_CODE='" & RsSubCatMast.Fields("CATEGORY_CODE").Value & "'"
                PubDBCn.Execute(SqlStr)

                PubDBCn.CommitTrans()
                RsSubCatMast.Requery() ''.Refresh	
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        ''Resume	
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans() ''	
        RsSubCatMast.Requery() ''.Refresh	
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            txtCatCode.Enabled = False
            txtSubCatCode.Enabled = False
            SprdMain.Enabled = True
            MainClass.ButtonStatus(Me, XRIGHT, RsSubCatMast, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            SprdMain.Enabled = True    '' False Sandeep 15/05/2022
            Show1()
        End If
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler

        If FieldVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If Update1() = True Then
            ADDMode = False
            MODIFYMode = False
            ' SSTInfo.Tab = 0	
            txtSubCatCode_Validating(txtSubCatCode, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True Then CmdAdd.Focus()
        Else
            MsgInformation("Record not saved")
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '    Clear1	
        Exit Sub
ErrorHandler:
        Call ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Function Update1() As Boolean
        On Error GoTo UpdateError

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        If UpdateItem() = False Then GoTo UpdateError

        PubDBCn.CommitTrans()
        Update1 = True
        Exit Function
UpdateError:
        Call ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans() ''	
        Update1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function


    Private Function UpdateItem() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String = ""
        Dim mAutoIndent As String
        Dim mConsumable As String
        Dim mDrawingAvailable As String
        Dim mApproval As String
        Dim mER6 As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim xCompanyCode As Long

        mApproval = IIf(chkApproved.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mER6 = IIf(chkER6.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        mSubCatCode = MainClass.AllowSingleQuote(txtSubCatCode.Text)

        If CheckConsolidatedMaster("INV_ITEM_MST") = True Then
            SqlStr = "SELECT COMPANY_CODE FROM GEN_COMPANY_MST"
        Else
            SqlStr = "SELECT COMPANY_CODE FROM GEN_COMPANY_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                xCompanyCode = RsTemp.Fields("COMPANY_CODE").Value
                If ADDMode = True Then
                    ''MainClass.AutoGenRowNo("ACM", "Code", PubDBCn)	
                    SqlStr = ""
                    SqlStr = " INSERT INTO INV_SUBCATEGORY_MST( " & vbCrLf _
                       & " COMPANY_CODE, SUBCATEGORY_CODE, CATEGORY_CODE,  " & vbCrLf _
                       & " SUBCATEGORY_DESC,IS_APPROVAL,QC_EMP_CODE,ISER6,SUBCODE_PREFIX ) VALUES ( " & vbCrLf _
                       & " " & xCompanyCode & ", " & vbCrLf _
                       & " '" & MainClass.AllowSingleQuote(txtSubCatCode.Text) & "'," & vbCrLf _
                       & " '" & MainClass.AllowSingleQuote(txtCatCode.Text) & "', " & vbCrLf _
                       & " '" & MainClass.AllowSingleQuote(txtSubCatName.Text) & "'," & vbCrLf _
                       & " '" & mApproval & "', " & vbCrLf & " ''," & vbCrLf & " '" & mER6 & "','" & MainClass.AllowSingleQuote(txtItemPrefix.Text) & "')"

                End If
                '" & MainClass.AllowSingleQuote(txtQCEmpCode.Text) & "	

                If MODIFYMode = True Then
                    SqlStr = ""
                    SqlStr = " UPDATE INV_SUBCATEGORY_MST SET  " & vbCrLf _
                       & " CATEGORY_CODE= '" & MainClass.AllowSingleQuote(txtCatCode.Text) & "', " & vbCrLf _
                       & " SUBCATEGORY_DESC= '" & MainClass.AllowSingleQuote(txtSubCatName.Text) & "', " & vbCrLf _
                       & " IS_APPROVAL= '" & mApproval & "', " & vbCrLf _
                       & " QC_EMP_CODE='', " & vbCrLf _
                       & " ISER6='" & mER6 & "', SUBCODE_PREFIX='" & MainClass.AllowSingleQuote(txtItemPrefix.Text) & "'" & vbCrLf _
                       & " WHERE COMPANY_CODE=" & xCompanyCode & " " & vbCrLf _
                       & " AND SUBCATEGORY_CODE = '" & Trim(mSubCatCode) & "' " & vbCrLf _
                       & " AND CATEGORY_CODE='" & MainClass.AllowSingleQuote(txtCatCode.Text) & "'"


                End If
                PubDBCn.Execute(SqlStr)
                RsTemp.MoveNext()
            Loop
        End If

        If UpdateDetail1((txtCatCode.Text), mSubCatCode) = False Then GoTo ErrPart
        UpdateItem = True
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateItem = False
        'Resume	
    End Function

    Private Function UpdateDetail1(ByRef pCategoryCode As String, ByRef pSubCategoryCode As String) As Boolean

        On Error GoTo UpdateDetail1
        Dim SqlStr As String = ""
        Dim I As Integer
        Dim mDivCode As Double
        Dim mEmpCode As String


        SqlStr = "Delete From  INV_QCEMP_MST " & vbCrLf _
              & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
              & " AND CATEGORY_CODE='" & MainClass.AllowSingleQuote(pCategoryCode) & "'" & vbCrLf _
              & " AND SUBCATEGORY_CODE='" & MainClass.AllowSingleQuote(pSubCategoryCode) & "'"

        PubDBCn.Execute(SqlStr)

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColDivCode
                mDivCode = Val(.Text)

                .Col = ColQCEmpCode
                mEmpCode = MainClass.AllowSingleQuote(.Text)

                SqlStr = ""

                If mEmpCode <> "" And mDivCode > 0 Then
                    SqlStr = " INSERT INTO INV_QCEMP_MST ( " & vbCrLf _
                       & " COMPANY_CODE , CATEGORY_CODE, SUBCATEGORY_CODE, " & vbCrLf _
                       & " DIV_CODE, EMP_CODE) "
                    SqlStr = SqlStr & vbCrLf _
                       & " VALUES ( " & vbCrLf _
                       & " '" & RsCompany.Fields("COMPANY_CODE").Value & "','" & MainClass.AllowSingleQuote(pCategoryCode) & "', '" & MainClass.AllowSingleQuote(pSubCategoryCode) & "', " & vbCrLf _
                       & " " & mDivCode & ", '" & MainClass.AllowSingleQuote(mEmpCode) & "') "

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

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Private Sub ViewGrid()

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            FormatSprdView()
            SprdView.Refresh()
            SprdView.Focus()
            SprdView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            SprdView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsSubCatMast, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmSubCatMst_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = "Select *   " & vbCrLf _
           & "From INV_SUBCATEGORY_MST " & vbCrLf _
           & "WHERE 1=2"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSubCatMast, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)

        SetTextLengths()
        Clear1()
        If CmdAdd.Enabled = True Then cmdAdd_Click(CmdAdd, New System.EventArgs())
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = ""

        SqlStr = "SELECT A.SUBCATEGORY_CODE,A.SUBCATEGORY_DESC," & vbCrLf _
           & " B.GEN_CODE As CATGEORY_CODE,B.GEN_DESC AS CATGEORY, SUBCODE_PREFIX" & vbCrLf _
           & " FROM INV_SUBCATEGORY_MST A,INV_GENERAL_MST B" & vbCrLf _
           & " WHERE A.CATEGORY_CODE=B.GEN_CODE" & vbCrLf _
           & " AND B.GEN_TYPE='C'" & vbCrLf _
           & " AND A.COMPANY_CODE=B.COMPANY_CODE " & vbCrLf _
           & " AND A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        SqlStr = SqlStr & " ORDER BY B.GEN_CODE,A.SUBCATEGORY_CODE,A.SUBCATEGORY_DESC"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmSubCatMst_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection	
        'PvtDBCn.Open StrConn	
        Call SetMainFormCordinate(Me)
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        '    SSTInfo.Tab = 0	
        ADDMode = False
        MODIFYMode = False
        FormActive = False
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub Clear1()

        On Error GoTo ClearErr

        txtSubCatCode.Text = ""
        txtSubCatName.Text = ""
        txtItemPrefix.Text = ""
        txtItemPrefix.Enabled = True
        txtCatCode.Text = ""
        txtCatName.Text = ""
        chkApproved.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkER6.CheckState = System.Windows.Forms.CheckState.Unchecked
        '    txtQCEmpCode.Text = ""	
        '    txtQCEmpName.Text = ""	

        SprdMain.Enabled = True
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FillSprdMain()
        FormatSprdMain(-1)

        Call AutoCompleteSearch("INV_GENERAL_MST", "GEN_CODE", "GEN_TYPE='C'", txtCatCode)
        Call AutoCompleteSearch("INV_GENERAL_MST", "GEN_DESC", "GEN_TYPE='C'", txtCatName)
        MainClass.ButtonStatus(Me, XRIGHT, RsSubCatMast, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume	
    End Sub
    Private Sub FillSprdMain()

        On Error GoTo ERR1
        Dim I As Integer
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        SqlStr = "SELECT DIV_CODE, DIV_DESC FROM INV_DIVISION_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ORDER BY DIV_CODE"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        I = 1
        If RsTemp.EOF = False Then
            With SprdMain
                Do While RsTemp.EOF = False
                    .Row = I
                    .Col = ColDivCode
                    .Text = Trim(IIf(IsDBNull(RsTemp.Fields("DIV_CODE").Value), "-1", RsTemp.Fields("DIV_CODE").Value))

                    .Col = ColDivDesc
                    .Text = Trim(IIf(IsDBNull(RsTemp.Fields("DIV_DESC").Value), "-1", RsTemp.Fields("DIV_DESC").Value))
                    RsTemp.MoveNext()
                    If RsTemp.EOF = False Then
                        I = I + 1
                        .MaxRows = I
                    End If
                Loop
            End With
        End If

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume	
    End Sub
    Private Sub FormatSprdView()


        With SprdView
            .Row = -1
            .set_RowHeight(0, 300)
            .set_ColWidth(0, 500)
            .set_ColWidth(1, 900)
            .set_ColWidth(2, 3500)
            .set_ColWidth(3, 500)
            .set_ColWidth(4, 3500)
            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ' OperationModeSingle	
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1

        txtSubCatCode.MaxLength = RsSubCatMast.Fields("SUBCATEGORY_CODE").DefinedSize
        txtSubCatName.MaxLength = RsSubCatMast.Fields("SUBCATEGORY_DESC").DefinedSize
        txtItemPrefix.MaxLength = RsSubCatMast.Fields("SUBCODE_PREFIX").DefinedSize
        txtCatCode.MaxLength = RsSubCatMast.Fields("CATEGORY_CODE").DefinedSize
        '    txtQCEmpCode.MaxLength = RsSubCatMast.Fields("QC_EMP_CODE").DefinedSize	

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        Resume
    End Sub

    Function FieldVarification() As Boolean

        On Error GoTo err_Renamed
        Dim cntRow As Integer
        Dim mEmpCode As String

        FieldVarification = True
        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldVarification = False
            Exit Function
        End If


        If Val(txtSubCatCode.Text) = 0 Then
            MsgInformation("Code Cann't be empty. Cannot Save")
            txtSubCatCode.Focus()
            FieldVarification = False
            Exit Function
        End If

        If Val(txtSubCatCode.Text) < 10 And ADDMode = True Then
            MsgInformation("Code Cann't be Less than 10. Cannot Save")
            txtSubCatCode.Focus()
            FieldVarification = False
            Exit Function
        End If

        If Trim(txtSubCatName.Text) = "" Then
            MsgInformation("Sub Category Name is empty. Cannot Save")
            txtSubCatName.Focus()
            FieldVarification = False
            Exit Function
        End If

        '    If Trim(txtQCEmpCode.Text) = "" Then	
        '        MsgBox "Please Enter Employee Code.", vbCritical	
        '        FieldVarification = False	
        '        txtQCEmpCode.SetFocus	
        '        Exit Function	
        '    Else	


        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColQCEmpCode

                mEmpCode = Trim(.Text)
                If mEmpCode <> "" Then
                    If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                        MainClass.SetFocusToCell(SprdMain, cntRow, ColQCEmpCode, "Invalid Employee Code.")
                        FieldVarification = False
                        Exit Function
                    End If
                End If
            Next
        End With



        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function

    Private Sub frmSubCatMst_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        Me.Hide()
        Me.Close()
        RsSubCatMast.Close()
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        With SprdMain
            SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub
    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        If eventArgs.row = 0 And eventArgs.col = ColQCEmpCode And SprdMain.Enabled = True Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColQCEmpCode
                If MainClass.SearchGridMaster(.Text, "PAY_EMPLOYEE_MST", "EMP_CODE", "EMP_NAME", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITEM_STATUS='A'", "EMP_CODE") = True Then
                    .Row = .ActiveRow
                    .Col = ColQCEmpCode
                    .Text = Trim(AcName)
                    .Col = ColQCEmpName
                    .Text = Trim(AcName1)
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColQCEmpCode)
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColQCEmpName And SprdMain.Enabled = True Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColQCEmpName
                If MainClass.SearchGridMaster(.Text, "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "", "EMP_CODE") = True Then
                    .Row = .ActiveRow
                    .Col = ColQCEmpName
                    .Text = Trim(AcName)
                    .Col = ColQCEmpCode
                    .Text = Trim(AcName1)
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColQCEmpCode)
            End With
        End If

        '    If eventArgs.Col = 0 And eventArgs.Row > 0 And (ADDMode = True Or MODIFYMode = True) Then	
        '        MainClass.DeleteSprdRow SprdMain, Row, ColQCEmpName	
        '        MainClass.SaveStatus Me, ADDMode, MODIFYMode	
        '    End If	
    End Sub
    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColQCEmpCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColQCEmpCode, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColQCEmpName Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColQCEmpName, 0))
        SprdMain.Refresh()
    End Sub
    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim xEmpCode As String

        If eventArgs.newRow = -1 Then Exit Sub

        Select Case eventArgs.col
            Case ColQCEmpCode
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColQCEmpCode
                xEmpCode = SprdMain.Text
                If xEmpCode = "" Then Exit Sub
                xEmpCode = VB6.Format(xEmpCode, "000000")
                SprdMain.Text = xEmpCode
                If MainClass.ValidateWithMasterTable(xEmpCode, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    SprdMain.Col = ColQCEmpName
                    SprdMain.Text = MasterNo
                Else
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColQCEmpCode, "Invalid Employee Code.")
                End If
        End Select
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim mBalOpt As Integer
        Dim mControlBranchCode As Integer
        Clear1()
        If Not RsSubCatMast.EOF Then

            txtCatCode.Text = Trim(IIf(IsDBNull(RsSubCatMast.Fields("CATEGORY_CODE").Value), "", RsSubCatMast.Fields("CATEGORY_CODE").Value))

            txtSubCatCode.Text = Trim(IIf(IsDBNull(RsSubCatMast.Fields("SUBCATEGORY_CODE").Value), "", RsSubCatMast.Fields("SUBCATEGORY_CODE").Value))

            If MainClass.ValidateWithMasterTable(Trim(txtSubCatCode.Text), "SUBCATEGORY_CODE", "SUBCATEGORY_DESC", "INV_SUBCATEGORY_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & MainClass.AllowSingleQuote(txtCatCode.Text) & "'") = True Then
                txtSubCatName.Text = MasterNo
            End If

            txtItemPrefix.Text = Trim(IIf(IsDBNull(RsSubCatMast.Fields("SUBCODE_PREFIX").Value), "", RsSubCatMast.Fields("SUBCODE_PREFIX").Value))
            txtItemPrefix.Enabled = False
            If MainClass.ValidateWithMasterTable(txtCatCode.Text, "GEN_CODE", "GEN_DESC", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                txtCatName.Text = MasterNo
            End If

            chkApproved.CheckState = IIf(RsSubCatMast.Fields("IS_APPROVAL").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            chkER6.CheckState = IIf(RsSubCatMast.Fields("ISER6").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            '        txtQCEmpCode.Text = Trim(IIf(IsNull(RsSubCatMast.Fields("QC_EMP_CODE").Value), "", RsSubCatMast.Fields("QC_EMP_CODE").Value))	
            '	
            '        If MainClass.ValidateWithMasterTable(txtQCEmpCode.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then	
            '            txtQCEmpName.Text = MasterNo	
            '        End If	
            '	
            'txtSubCatName.Text = Trim(IIf(IsNull(RsSubCatMast.Fields("SUBCATEGORY_DESC").Value), "", RsSubCatMast.Fields("SUBCATEGORY_NAME").Value))	

            Call ShowDetail1((txtCatCode.Text), (txtSubCatCode.Text))
        End If
        ADDMode = False
        MODIFYMode = False
        txtCatCode.Enabled = True
        txtSubCatCode.Enabled = True
        SprdMain.Enabled = True    '' False Sandeep 15/05/2022

        MainClass.ButtonStatus(Me, XRIGHT, RsSubCatMast, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume	
    End Sub


    Private Sub ShowDetail1(ByRef pCategoryCode As String, ByRef pSubCategoryCode As String)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mEmpCode As String
        Dim mEmpName As String
        Dim cntRow As Integer
        Dim mDivCode As Double

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf _
              & " FROM INV_QCEMP_MST " & vbCrLf _
              & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
              & " AND CATEGORY_CODE='" & MainClass.AllowSingleQuote(pCategoryCode) & "'" & vbCrLf _
              & " AND SUBCATEGORY_CODE='" & MainClass.AllowSingleQuote(pSubCategoryCode) & "'" & vbCrLf _
              & " Order By DIV_CODE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSubQCMst, ADODB.LockTypeEnum.adLockReadOnly)

        With RsSubQCMst
            If .EOF = True Then Exit Sub
            '        FormatSprdMain -1	
            '        .MoveFirst	

            Do While Not .EOF
                mDivCode = IIf(IsDBNull(.Fields("DIV_CODE").Value), "-1", .Fields("DIV_CODE").Value)

                With SprdMain
                    For cntRow = 1 To .MaxRows
                        .Row = cntRow
                        .Col = ColDivCode
                        If Val(.Text) = mDivCode Then
                            .Col = ColQCEmpCode
                            mEmpCode = IIf(IsDBNull(RsSubQCMst.Fields("EMP_CODE").Value), "", RsSubQCMst.Fields("EMP_CODE").Value)
                            .Text = mEmpCode

                            MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                            mEmpName = MasterNo

                            .Col = ColQCEmpName
                            .Text = mEmpName
                            Exit For
                        End If
                    Next
                End With

                .MoveNext()
            Loop
        End With
        FormatSprdMain(-1)

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume	
    End Sub
    Private Sub txtQCEmpCode_Change()

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        If eventArgs.row < 1 Then Exit Sub

        SprdView.Row = eventArgs.row

        SprdView.Col = 1
        txtSubCatCode.Text = Trim(SprdView.Text)

        SprdView.Col = 3
        txtCatCode.Text = Trim(SprdView.Text)

        txtSubCatCode_Validating(txtSubCatCode, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub
    Private Sub txtCatCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCatCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtCatCode.Text) = "" Then txtCatName.Text = "" : GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtCatCode.Text, "GEN_CODE", "GEN_DESC", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND GEN_TYPE='C'") = False Then
            ErrorMsg("Invalid Category Code.", , vbInformation)
            Cancel = True
        Else
            txtCatName.Text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtCatCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCatCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
    End Sub
    Private Sub txtCatCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCatCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCatCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCatCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCatCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        'txtCatName.Text = ""
    End Sub
    Private Sub txtSubCatCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSubCatCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
    End Sub
    Private Sub txtSubCatName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSubCatName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
    End Sub
    Private Sub txtSubCatCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSubCatCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        txtSubCatName.Text = ""
    End Sub
    Private Sub txtSubCatName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSubCatName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtSubCatName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSubCatName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSubCatName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtItemPrefix_KeyPress(sender As Object, EventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItemPrefix.KeyPress
        Dim KeyAscii As Short = Asc(EventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtItemPrefix.Text)
        EventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub

    Private Sub txtItemPrefix_TextChanged(sender As Object, e As System.EventArgs) Handles txtItemPrefix.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtSubCatCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSubCatCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtSubCatCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSubCatCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        If Trim(txtCatCode.Text) = "" Then
            MsgInformation("Please Select Category Code First.")
            txtCatCode.Focus()
        End If

        If Trim(txtSubCatCode.Text) = "" Then GoTo EventExitSub

        If MODIFYMode = True And RsSubCatMast.EOF = False Then mSubCatCode = RsSubCatMast.Fields("SUBCATEGORY_CODE").Value
        SqlStr = "Select * From INV_SUBCATEGORY_MST " & vbCrLf _
              & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & MainClass.AllowSingleQuote(txtCatCode.Text) & "'" & vbCrLf _
              & " AND SUBCATEGORY_CODE='" & MainClass.AllowSingleQuote(txtSubCatCode.Text) & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSubCatMast, ADODB.LockTypeEnum.adLockReadOnly)

        If RsSubCatMast.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Name Does Not Exist In Master, Click Add To Add In Master", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * From INV_SUBCATEGORY_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & MainClass.AllowSingleQuote(txtCatCode.Text) & "'" & vbCrLf & " AND SUBCATEGORY_CODE=" & mSubCatCode & ""
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSubCatMast, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number))
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim I As Integer

        With SprdMain
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight)

            .Col = ColDivCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = MainClass.SetMaxLength("DIV_CODE", "INV_DIVISION_MST", PubDBCn)
            .set_ColWidth(ColDivCode, 6)
            '        .ColHidden = True	
            '        .ColsFrozen = ColDivCode	

            .Col = ColDivDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = MainClass.SetMaxLength("DIV_DESC", "INV_DIVISION_MST", PubDBCn)
            .ColsFrozen = ColDivDesc
            .set_ColWidth(ColDivDesc, 15)

            .Col = ColQCEmpCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn)
            .set_ColWidth(ColQCEmpCode, 8)

            .Col = ColQCEmpName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = False
            .TypeEditLen = MainClass.SetMaxLength("EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn) ''	
            .set_ColWidth(ColQCEmpName, 15)


        End With

        MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, ColDivCode, ColQCEmpName)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColDivCode, ColDivDesc)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColQCEmpName, ColQCEmpName)
        '    SprdMain.EditMode = False	
        '    SprdMain.EditModePermanent = True	
        SprdMain.EditModeReplace = True

        SprdMain.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
        SprdMain.DAutoCellTypes = True
        SprdMain.DAutoSizeCols = SS_AUTOSIZE_MAX_COL_WIDTH
        '    SprdMain.GridColor = &HC00000	

        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then RsSubQCMst.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtCatName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCatName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
    End Sub
    Private Sub txtCatName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCatName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCatName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCatName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCatName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        'txtCatCode.Text = ""
    End Sub
    Private Sub txtCatName_Validating(sender As Object, EventArgs As System.ComponentModel.CancelEventArgs) Handles txtCatName.Validating
        Dim Cancel As Boolean = EventArgs.Cancel
        Dim mCatCode As String = ""

        If Trim(txtCatName.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(txtCatName.Text, "GEN_DESC", "GEN_CODE", "INV_GENERAL_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " And GEN_TYPE='C'") = False Then
            ErrorMsg("Invalid Category Name.", , vbInformation)
        Cancel = True
        Else
        mCatCode = MasterNo
        End If

        Call AutoCompleteSearch("INV_SUBCATEGORY_MST", "SUBCATEGORY_DESC", "CATEGORY_CODE='" & mCatCode & "'", txtSubCatName)
        Call AutoCompleteSearch("INV_SUBCATEGORY_MST", "SUBCATEGORY_CODE", "CATEGORY_CODE='" & mCatCode & "'", txtSubCatCode)
EventExitSub:
        EventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSubCatName_Validating(sender As Object, EventArgs As System.ComponentModel.CancelEventArgs) Handles txtSubCatName.Validating
        Dim Cancel As Boolean = EventArgs.Cancel

        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        If Trim(txtCatCode.Text) = "" Then
            MsgInformation("Please Select Category Code First.")
            txtCatCode.Focus()
        End If

        If Trim(txtSubCatName.Text) = "" Then GoTo EventExitSub

        If MODIFYMode = True And RsSubCatMast.EOF = False Then mSubCatCode = RsSubCatMast.Fields("SUBCATEGORY_CODE").Value
        SqlStr = "Select * From INV_SUBCATEGORY_MST " & vbCrLf _
              & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & MainClass.AllowSingleQuote(txtCatCode.Text) & "'" & vbCrLf _
              & " AND SUBCATEGORY_DESC='" & MainClass.AllowSingleQuote(txtSubCatName.Text) & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSubCatMast, ADODB.LockTypeEnum.adLockReadOnly)

        If RsSubCatMast.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Name Does Not Exist In Master, Click Add To Add In Master", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * From INV_SUBCATEGORY_MST " & vbCrLf _
                   & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CATEGORY_CODE='" & MainClass.AllowSingleQuote(txtCatCode.Text) & "'" & vbCrLf _
                   & " AND SUBCATEGORY_CODE=" & mSubCatCode & ""
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsSubCatMast, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number))
EventExitSub:
        EventArgs.Cancel = Cancel
    End Sub
End Class
