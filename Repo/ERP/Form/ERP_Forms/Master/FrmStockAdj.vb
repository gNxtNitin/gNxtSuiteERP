Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmStoreAdjustment
   Inherits System.Windows.Forms.Form
   Dim RsAdjMain As ADODB.Recordset ''Recordset
   Dim RsAdjDetail As ADODB.Recordset ''Recordset
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
    Private Const ColLotNo As Short = 4
    Private Const ColHeatNo As Short = 5

    Private Const ColRate As Short = 6
    Private Const ColStockType As Short = 7
    Private Const ColStockQty As Short = 8
    Private Const ColAdjQty As Short = 9
    Private Const ColAdjAmount As Short = 10
    Private Const ColRemarks As Short = 11

    Dim FileDBCn As ADODB.Connection
   Dim mSearchStartRow As Integer

   Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged

      MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
   End Sub

   Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged

      MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
   End Sub

   Private Sub chkUpDateStock_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkUpDateStock.CheckStateChanged

      MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
   End Sub

   Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click

      On Error GoTo AddErr
      If cmdAdd.Text = ConCmdAddCaption Then
         ADDMode = True
         MODIFYMode = False
         Clear1()
         SprdMain.Enabled = True
         txtADJNo.Enabled = False
      Else
         cmdAdd.Text = ConCmdAddCaption
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

      Dim mItemCode As String


      If ValidateBranchLocking((txtADJDate.Text)) = True Then
         Exit Sub
      End If

      If ValidateBookLocking(PubDBCn, CInt(ConLockSTN), txtADJDate.Text) = True Then
         Exit Sub
      End If

      If Trim(txtADJNo.Text) = "" Then MsgInformation("Nothing to Delete") : Exit Sub

      If Not RsAdjMain.EOF Then
         If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.	
            PubDBCn.Errors.Clear()
            PubDBCn.BeginTrans()
            If InsertIntoDelAudit(PubDBCn, "INV_ADJ_HDR", (txtADJNo.Text), RsAdjMain, "AUTO_KEY_ADJ") = False Then GoTo DelErrPart
            If InsertIntoDeleteTrn(PubDBCn, "INV_ADJ_HDR", "AUTO_KEY_ADJ", (txtADJNo.Text)) = False Then GoTo DelErrPart

            If DeleteStockTRN(PubDBCn, ConStockRefType_ADJ, (txtADJNo.Text)) = False Then GoTo DelErrPart

            PubDBCn.Execute("Delete from INV_ADJ_DET Where AUTO_KEY_ADJ=" & Val(txtADJNo.Text) & "")
            PubDBCn.Execute("Delete from INV_ADJ_HDR Where AUTO_KEY_ADJ=" & Val(txtADJNo.Text) & "")

            PubDBCn.CommitTrans()
            RsAdjMain.Requery() ''.Refresh	
            RsAdjDetail.Requery() ''.Refresh	
            Clear1()
         End If
      End If
      Exit Sub
DelErrPart:
      PubDBCn.RollbackTrans() ''	
      RsAdjMain.Requery() ''.Refresh	
      RsAdjDetail.Requery() ''.Refresh	
      If Err.Description <> "" Then
         ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
      End If
   End Sub
   Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdModify.Click

      On Error GoTo ModifyErr

      If cmdModify.Text = ConcmdmodifyCaption Then
         ADDMode = False
         MODIFYMode = True
         MainClass.ButtonStatus(Me, XRIGHT, RsAdjMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
         SprdMain.Enabled = True
         txtADJNo.Enabled = False
      Else
         ADDMode = False
         MODIFYMode = False
         Show1()
      End If
      Exit Sub
ModifyErr:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
   End Sub

   Private Sub CmdPopFromFile_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPopFromFile.Click
      On Error GoTo ErrPart
      Dim strFilePath As String

      If Trim(txtDept.Text) = "" Then
         MsgBox("Please select Dept First.")
         Exit Sub
      End If

      If Trim(cboDivision.Text) = "" Then
         MsgBox("Please select Division First.")
         Exit Sub
      End If

      strFilePath = My.Application.Info.DirectoryPath

        ''Commit on convert to .net
        If Not fOpenFile(strFilePath, "*.xls", "Excel Data", CommonDialogOpen) Then
            GoTo NormalExit
        End If

        Call PopulateFromXLSFile(strFilePath)

        Exit Sub
ErrPart:
      ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
NormalExit:
   End Sub

   Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
      System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
      Call ReportONSRN(Crystal.DestinationConstants.crptToWindow)
      System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
   End Sub

   Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
      System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
      Call ReportONSRN(Crystal.DestinationConstants.crptToWindow)
      System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
   End Sub

   Private Sub ReportONSRN(ByRef Mode As Crystal.DestinationConstants)

      On Error GoTo ERR1
        Dim mTitle As String = ""
        Dim mSubTitle As String = ""
        Dim SqlStr As String = ""
        Dim mRptFileName As String
        Dim mVNo As String

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        SqlStr = ""

        Call MainClass.ClearCRptFormulas(Report1)

        Call SelectQryForSRN(SqlStr)


        mTitle = "Stock Adjustment"
        mSubTitle = ""
        mRptFileName = "SRN.rpt"

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)

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

    Private Function SelectQryForSRN(ByRef mSqlStr As String) As String

        ''SELECT CLAUSE...	

        mSqlStr = " SELECT " & vbCrLf & " IH.*, ID.*, INVMST.ITEM_SHORT_DESC, DEPTMST.DEPT_DESC "


        ''FROM CLAUSE...	
        mSqlStr = mSqlStr & vbCrLf & " FROM INV_ADJ_HDR IH, INV_ADJ_DET ID, " & vbCrLf & " INV_ITEM_MST INVMST, PAY_DEPT_MST DEPTMST "

        ''WHERE CLAUSE...	
        mSqlStr = mSqlStr & vbCrLf & " WHERE " & vbCrLf & " IH.AUTO_KEY_ADJ=ID.AUTO_KEY_ADJ" & vbCrLf & " AND IH.COMPANY_CODE=DEPTMST.COMPANY_CODE" & vbCrLf & " AND IH.DEPT_CODE=DEPTMST.DEPT_CODE" & vbCrLf & " AND IH.COMPANY_CODE=INVMST.COMPANY_CODE" & vbCrLf & " AND ID.ITEM_CODE=INVMST.ITEM_CODE" & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND INVMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND DEPTMST.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.AUTO_KEY_ADJ=" & Val(txtADJNo.Text) & ""

        ''ORDER CLAUSE...	

        mSqlStr = mSqlStr & vbCrLf & "ORDER BY ID.SERIAL_NO"

        SelectQryForSRN = mSqlStr
    End Function
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
            txtADJNo_Validating(txtADJNo, New System.ComponentModel.CancelEventArgs(False))
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

    Private Sub CalcTots()
        On Error GoTo ERR1
        Dim cntRow As Integer
        Dim mRate As Double
        Dim mQty As Double
        Dim mAmount As Double



        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColItemCode
                If .Text = "" Then GoTo DontCalc
                .Col = ColRate
                mRate = CDbl(VB6.Format(Val(.Text), "0.00"))

                .Col = ColAdjQty
                mQty = CDbl(VB6.Format(Val(.Text), "0.00"))

                mAmount = CDbl(VB6.Format(mRate * mQty, "0.00"))

                .Col = ColAdjAmount
                .Text = VB6.Format(mAmount, "0.00")
DontCalc:
            Next
        End With

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Information)
    End Sub

    Private Sub cmdDeptSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDeptSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""


        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , SqlStr) = True Then
            txtDept.Text = AcName1
            lblDeptname.Text = AcName
            'txtDept_Validating()	
            If txtDept.Enabled = True Then txtDept.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdCCSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCCSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""


        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster("", "FIN_CCENTER_HDR", "CC_DESC", "CC_CODE", , , SqlStr) = True Then
            txtCost.Text = AcName1
            lblCostctr.Text = AcName
            'txtCost_Validate(False)	
            If txtCost.Enabled = True Then txtCost.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdEmpSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdEmpSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String = ""


        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            txtEmp.Text = AcName1
            lblEmpname.Text = AcName
            'txtEmp_Validate(False)	
            If txtEmp.Enabled = True Then txtEmp.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdSearchItem_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchItem.Click

        On Error GoTo ErrPart
        Dim mSearchItem As String
        Dim mFindItemName As String
        Dim i As Integer

        mSearchItem = Trim(txtSearchItem.Text)
        Dim counter As Short
        With SprdMain
            counter = mSearchStartRow
            For i = counter To .MaxRows
                .Row = i

                .Col = ColItemCode
                mFindItemName = Trim(.Text)

                '            If mSearchItem = mFindItemName Then	
                If InStr(1, mFindItemName, mSearchItem, CompareMethod.Text) > 0 Then
                    MainClass.SetFocusToCell(SprdMain, i, ColItemCode)
                    mSearchStartRow = i + 1
                    GoTo NextRec
                End If

                .Col = ColItemDesc
                mFindItemName = Trim(.Text)

                '            If mSearchItem = mFindItemName Then	
                If InStr(1, mFindItemName, mSearchItem, CompareMethod.Text) > 0 Then
                    MainClass.SetFocusToCell(SprdMain, i, ColItemCode)
                    mSearchStartRow = i + 1
                    GoTo NextRec
                End If
            Next
            mSearchStartRow = 1
NextRec:
        End With
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmStoreAdjustment_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        On Error GoTo ErrPart
        Dim xIName As String
        Dim xSupp As String
        Dim SqlStr As String = ""
        Dim mDivisionCode As Double

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If

        If eventArgs.row = 0 And eventArgs.col = ColItemCode Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColItemCode
                If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_CODE", "ITEM_SHORT_DESC", "CUSTOMER_PART_NO", , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
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
                If MainClass.SearchGridMaster(.Text, "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", "CUSTOMER_PART_NO", , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
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

        If eventArgs.row = 0 And eventArgs.col = ColStockType Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColStockType
                If MainClass.SearchGridMaster(.Text, "INV_TYPE_MST", "STOCK_TYPE_CODE", "STOCK_TYPE_DESC", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow
                    .Col = ColStockType
                    .Text = AcName
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColStockType)
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
            If mActiveCol = ColAdjQty Then
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColAdjQty
                If Val(SprdMain.Text) <> 0 Then
                    If SprdMain.MaxRows = SprdMain.ActiveRow Then
                        MainClass.AddBlankSprdRow(SprdMain, ColItemCode, ConRowHeight * 1.5)
                        '                    FormatSprdMain SprdMain.MaxRows	
                    End If
                End If
                '            SprdMain.Row = SprdMain.MaxRows	
            End If
        ElseIf eventArgs.keyCode = System.Windows.Forms.Keys.F1 Then
            If mActiveCol = ColItemCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemCode, 0))
            If mActiveCol = ColItemDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColItemDesc, 0))

        End If
        eventArgs.keyCode = 9999
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim xItemCode As String = ""
        Dim xItemUOM As String
        Dim xStockType As String
        Dim mReturnedQty As Double
        Dim mDivisionCode As Double
        Dim mRate As Double
        Dim mQty As Double
        Dim mAmount As Double
        Dim xLotNo As String
        Dim xHeatNo As String


        If eventArgs.newRow = -1 Then Exit Sub

        If cboDivision.Text = "" Then
            If cboDivision.Enabled = True Then cboDivision.Focus()
            MsgInformation("Please Select Division.")
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If

        SprdMain.Row = SprdMain.ActiveRow

        Select Case eventArgs.col
            Case ColItemCode
                SprdMain.Col = ColItemCode
                If Trim(SprdMain.Text) = "" Then Exit Sub
                If FillItemDescFromItemCode((SprdMain.Text)) = True Then
                    If DuplicateItem() = False Then
                        FormatSprdMain(-1)
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColStockType)
                    Else
                        eventArgs.cancel = True
                        Exit Sub
                    End If
                Else
                    MsgInformation("Invalid Item Code")
                    eventArgs.cancel = True
                    Exit Sub
                End If
            Case ColItemDesc
                If Trim(SprdMain.Text) = "" Then Exit Sub
                SprdMain.Col = ColItemCode
                If FillItemDescFromItemDesc((SprdMain.Text)) = True Then
                    If DuplicateItem() = True Then
                        MsgInformation("Duplicate Item Code")
                        eventArgs.cancel = True
                        Exit Sub
                    End If
                Else
                    MsgInformation("Invalid Item Description")
                    eventArgs.cancel = True
                    Exit Sub
                End If
            Case ColAdjQty
                If CheckQty() = True Then
                    SprdMain.Row = SprdMain.ActiveRow
                    SprdMain.Col = ColRate
                    mRate = CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))

                    SprdMain.Col = ColAdjQty
                    mQty = CDbl(VB6.Format(Val(SprdMain.Text), "0.00"))

                    mAmount = CDbl(VB6.Format(mRate * mQty, "0.00"))

                    SprdMain.Col = ColAdjAmount
                    SprdMain.Text = VB6.Format(mAmount, "0.00")

                    '                If Val(SprdMain.Text) <> 0 Then	
                    '                    If SprdMain.MaxRows = SprdMain.ActiveRow Then	
                    '                        MainClass.AddBlankSprdRow SprdMain, ColItemCode, ConRowHeight	
                    ''                        FormatSprdMain SprdMain.MaxRows	
                    '                        FormatSprdMain -1	
                    '                    End If	
                    '                End If	
                End If

            Case ColStockType
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColStockType
                If Trim(SprdMain.Text) = "" Then Exit Sub
                xStockType = Trim(SprdMain.Text)

                If MainClass.ValidateWithMasterTable(SprdMain.Text, "STOCK_TYPE_CODE", "STOCK_TYPE_DESC", "INV_TYPE_MST", PubDBCn, MasterNo, , "Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                    MsgInformation("InValid Stock Type")
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColStockType)
                Else
                    SprdMain.Col = ColItemCode
                    xItemCode = Trim(SprdMain.Text)

                    '                If PubUserID <> "G0416" Then	
                    'If xStockType = "FG" Then
                    '    MsgInformation("Can't be Select FG Stock Type")
                    '    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColStockType)
                    '    eventArgs.cancel = True
                    '    Exit Sub
                    'End If
                    '                End If	

                    If DuplicateItem() = True Then
                        eventArgs.cancel = True
                        Exit Sub
                    End If

                    SprdMain.Row = SprdMain.ActiveRow
                    SprdMain.Col = ColUOM
                    xItemUOM = Trim(SprdMain.Text)


                    SprdMain.Col = ColUOM
                    xLotNo = Trim(SprdMain.Text)


                    SprdMain.Col = ColUOM
                    xHeatNo = Trim(SprdMain.Text)

                    SprdMain.Col = ColStockQty
                    SprdMain.Text = CStr(GetBalanceStockQty(xItemCode, (txtADJDate.Text), xItemUOM, Trim(txtDept.Text), xStockType, xLotNo, (lblStockID.Text), mDivisionCode,,,,, xHeatNo))
                End If

        End Select
        '    FormatSprdMain -1	
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Function DuplicateItem() As Boolean
        Dim cntRow As Integer
        Dim mCount As Byte
        Dim mCheckItemCode As String
        Dim mItemCode As String

        'Dim mCheckStockType As String	
        'Dim mStockType As String = ""	

        With SprdMain
            .Row = .ActiveRow
            .Col = ColItemCode
            mCheckItemCode = Trim(UCase(.Text))

            .Col = ColStockType
            mCheckItemCode = mCheckItemCode & Trim(UCase(.Text))

            .Col = ColLotNo
            mCheckItemCode = mCheckItemCode & Trim(UCase(.Text))

            .Col = ColHeatNo
            mCheckItemCode = mCheckItemCode & Trim(UCase(.Text))

            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColItemCode
                mItemCode = Trim(UCase(.Text))

                .Col = ColStockType
                mItemCode = mItemCode & Trim(UCase(.Text))

                .Col = ColLotNo
                mItemCode = mItemCode & Trim(UCase(.Text))

                .Col = ColHeatNo
                mItemCode = mItemCode & Trim(UCase(.Text))

                If (mItemCode = mCheckItemCode And mCheckItemCode <> "") Then
                    mCount = mCount + 1
                End If

                If mCount > 1 Then
                    DuplicateItem = True
                    MsgInformation("Duplicate Item : " & mCheckItemCode)
                    '                MainClass.SetFocusToCell SprdMain, .ActiveRow, ColItemCode	
                    Exit Function
                End If
            Next
        End With
    End Function
    Private Function CheckQty() As Boolean

        On Error GoTo ERR1
        With SprdMain
            .Row = .ActiveRow
            .Col = ColItemCode
            If Trim(.Text) = "" Then Exit Function

            .Col = ColAdjQty
            If Val(.Text) <> 0 Then
                CheckQty = True
            Else
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColAdjQty)
            End If
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Function FillItemDescFromItemCode(ByRef pItemCode As String) As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mRate As Double
        Dim mItemUOM As String = ""
        Dim mQty As Double
        Dim mAmount As Double

        FillItemDescFromItemCode = True
        If Trim(pItemCode) = "" Then Exit Function
        With SprdMain
            SqlStr = "Select ITEM_SHORT_DESC,ISSUE_UOM " & vbCrLf _
               & " FROM INV_ITEM_MST " & vbCrLf _
               & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
               & " And LTRIM(RTRIM(ITEM_CODE))='" & MainClass.AllowSingleQuote(pItemCode) & "'"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = False Then
                .Row = .ActiveRow
                .Col = ColItemDesc
                .Text = IIf(IsDBNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value)

                .Col = ColUOM
                .Text = IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)
                mItemUOM = IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)

                mRate = GetLatestItemCostFromMRR(pItemCode, mItemUOM, 1, (txtADJDate.Text), "L")
                .Col = ColRate
                .Text = VB6.Format(mRate, "0.00")

                .Col = ColAdjQty
                mQty = CDbl(VB6.Format(Val(.Text), "0.00"))

                mAmount = CDbl(VB6.Format(mRate * mQty, "0.00"))

                .Col = ColAdjAmount
                .Text = VB6.Format(mAmount, "0.00")

            Else
                '            MsgInformation "Invaild Item Code"	
                FillItemDescFromItemCode = False
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColItemCode)
            End If
        End With
        Exit Function
ERR1:
        '    Resume	
        MsgInformation(Err.Description)
        FillItemDescFromItemCode = False
    End Function

    Private Function FillItemDescFromItemDesc(ByRef pItemDesc As String) As Boolean

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mRate As Double
        Dim mItemUOM As String = ""
        Dim mQty As Double
        Dim mAmount As Double
        Dim pItemCode As String

        FillItemDescFromItemDesc = True
        If Trim(pItemDesc) = "" Then Exit Function
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
                pItemCode = IIf(IsDBNull(RsTemp.Fields("ITEM_CODE").Value), "", RsTemp.Fields("ITEM_CODE").Value)

                '            .Col = ColPartNo	
                '            .Text = IIf(IsNull(RsTemp!CUSTOMER_PART_NO), "", RsTemp!CUSTOMER_PART_NO)	

                .Col = ColUOM
                .Text = IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)
                mItemUOM = IIf(IsDBNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value)

                mRate = GetLatestItemCostFromMRR(pItemCode, mItemUOM, 1, (txtADJDate.Text), "L")
                .Col = ColRate
                .Text = VB6.Format(mRate, "0.00")

                .Col = ColAdjQty
                mQty = CDbl(VB6.Format(Val(.Text), "0.00"))

                mAmount = CDbl(VB6.Format(mRate * mQty, "0.00"))

                .Col = ColAdjAmount
                .Text = VB6.Format(mAmount, "0.00")

            Else
                '            MsgInformation "Invaild Item Description"	
                FillItemDescFromItemDesc = False
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColItemCode)
            End If
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        FillItemDescFromItemDesc = False
    End Function

    Private Sub SprdView_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdView.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        With SprdView
            .Row = eventArgs.row
            .Col = 1
            txtADJNo.Text = .Text
            txtADJNo_Validating(txtADJNo, New System.ComponentModel.CancelEventArgs(False))
            If txtADJNo.Enabled = True Then txtADJNo.Focus()
            CmdView_Click(CmdView, New System.EventArgs())
        End With
    End Sub
    Private Function AutoGenSeqNo() As String

        On Error GoTo AutoGenSeqNoErr
        Dim RsMRRMainGen As ADODB.Recordset = Nothing
        Dim mNewSeqNo As Integer
        Dim SqlStr As String = ""
        Dim mValue As String

        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_ADJ)  " & vbCrLf & " FROM INV_ADJ_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_ADJ,LENGTH(AUTO_KEY_ADJ)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMRRMainGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsMRRMainGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mValue = .Fields(0).Value
                    mNewSeqNo = CInt(Mid(mValue, 1, Len(mValue) - 6))
                    mNewSeqNo = mNewSeqNo + 1
                Else
                    mNewSeqNo = 1
                End If
            End If
        End With
        AutoGenSeqNo = mNewSeqNo & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        Exit Function
AutoGenSeqNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function
    Private Function UpdateMain1() As Boolean
        On Error GoTo ErrPart
        Dim mReqnum As String = ""
        Dim SqlStr As String = ""
        Dim mVNoSeq As Double
        'Dim mStatus As String	
        Dim mUpdateStock As String
        Dim mDivisionCode As Double

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()



        mUpdateStock = IIf(chkUpDateStock.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        If Val(txtADJNo.Text) = 0 Then
            mVNoSeq = Int(AutoGenSeqNo())
        Else
            mVNoSeq = Val(txtADJNo.Text)
        End If

        txtADJNo.Text = CStr(Val(CStr(mVNoSeq)))

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If

        SqlStr = ""
        If ADDMode = True Then
            lblMKey.Text = CStr(mVNoSeq)
            SqlStr = "INSERT INTO INV_ADJ_HDR (" & vbCrLf _
                   & " AUTO_KEY_ADJ, " & vbCrLf _
                   & " COMPANY_CODE, " & vbCrLf _
                   & " ADJ_DATE, BOOKTYPE," & vbCrLf _
                   & " DEPT_CODE, " & vbCrLf _
                   & " EMP_CODE, COST_CENTER_CODE, REMARKS,   " & vbCrLf _
                   & " ADDUSER,ADDDATE,MODUSER,MODDATE,UPD_STOCK,DIV_CODE)" & vbCrLf _
                   & " VALUES( " & vbCrLf _
                   & " " & Val(mVNoSeq) & "," & vbCrLf _
                   & " " & RsCompany.Fields("Company_Code").Value & "," & vbCrLf _
                   & " TO_DATE('" & VB6.Format(txtADJDate.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY'), '" & lblStockID.Text & "'," & vbCrLf _
                   & " '" & MainClass.AllowSingleQuote(txtDept.Text) & "', " & vbCrLf _
                   & " '" & MainClass.AllowSingleQuote(txtEmp.Text) & "', " & vbCrLf _
                   & " '" & MainClass.AllowSingleQuote(txtCost.Text) & "', " & vbCrLf _
                   & " '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                   & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'),'','','" & mUpdateStock & "'," & mDivisionCode & ")"

        ElseIf MODIFYMode = True Then
            SqlStr = ""
            SqlStr = "UPDATE INV_ADJ_HDR SET " & vbCrLf _
                   & " DEPT_CODE='" & txtDept.Text & "',  ADJ_DATE=TO_DATE('" & VB6.Format(txtADJDate.Text, "dd-MMM-yyyy") & "','DD-MON-YYYY')," & vbCrLf _
                   & " EMP_CODE ='" & txtEmp.Text & "', " & vbCrLf _
                   & " REMARKS ='" & txtRemarks.Text & "'," & vbCrLf _
                   & " BOOKTYPE='" & lblStockID.Text & "'," & vbCrLf _
                   & " COST_CENTER_CODE ='" & txtCost.Text & "'," & vbCrLf _
                   & " ModUser='" & MainClass.AllowSingleQuote(PubUserID) & "'," & vbCrLf _
                   & " Moddate=TO_DATE('" & VB6.Format(PubCurrDate, "dd-MMM-yyyy") & "','DD-MON-YYYY'), " & vbCrLf _
                   & " UPD_STOCK='" & mUpdateStock & "', DIV_CODE=" & mDivisionCode & "" & vbCrLf _
                   & " WHERE COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf _
                   & " AND AUTO_KEY_ADJ =" & Val(lblMKey.Text) & ""
        End If

        PubDBCn.Execute(SqlStr)

        If UpdateDetail1(CStr(Val(CStr(mVNoSeq))), mDivisionCode) = False Then GoTo ErrPart

        UpdateMain1 = True
        PubDBCn.CommitTrans()
        Exit Function
ErrPart:
        UpdateMain1 = False
        PubDBCn.RollbackTrans() ''	
        RsAdjMain.Requery() ''.Refresh	
        RsAdjDetail.Requery() ''.Refresh	
        If Err.Description = "" Then Exit Function
        If Err.Number = -2147217900 Then
            ErrorMsg("Duplicate Item Consumption Generated, Save Again", "Duplicate", MsgBoxStyle.Critical)
        Else
            ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        End If
        ''Resume	
    End Function
    Private Function UpdateDetail1(ByRef pVnoseq As String, ByRef mDivisionCode As Double) As Boolean

        On Error GoTo UpdateDetail1
        Dim SqlStr As String = ""
        Dim i As Integer
        Dim mItemCode As String
        Dim mUOM As String = ""
        Dim mIO As String
        Dim mStkType As String
        Dim mAdjQty As Double
        Dim mRemarks As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mStatus As String
        Dim xFGBatchNo As String
        Dim mLotNo As String
        Dim mHeatNo As String

        SqlStr = " Delete From INV_ADJ_DET " & vbCrLf & " WHERE AUTO_KEY_ADJ=" & Val(lblMKey.Text) & ""
        PubDBCn.Execute(Sqlstr)

        If DeleteStockTRN(PubDBCn, ConStockRefType_ADJ, (txtADJNo.Text)) = False Then GoTo UpdateDetail1
        mStatus = IIf(chkUpDateStock.CheckState = System.Windows.Forms.CheckState.Checked, "O", "C")

        With SprdMain
            For i = 1 To .MaxRows - 1
                .Row = i

                .Col = ColItemCode
                mItemCode = Trim(MainClass.AllowSingleQuote(.Text))

                .Col = ColUOM
                mUOM = MainClass.AllowSingleQuote(.Text)


                .Col = ColStockType
                mStkType = MainClass.AllowSingleQuote(.Text)

                .Col = ColAdjQty
                mIO = IIf(Val(.Text) >= 0, "I", "O")
                mAdjQty = System.Math.Abs(Val(.Text))

                .Col = ColRemarks
                mRemarks = MainClass.AllowSingleQuote(.Text)

                .Col = ColLotNo
                mLotNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColHeatNo
                mHeatNo = MainClass.AllowSingleQuote(.Text)

                SqlStr = ""

                If mItemCode <> "" And mAdjQty > 0 Then
                    SqlStr = " INSERT INTO INV_ADJ_DET ( " & vbCrLf _
                       & " AUTO_KEY_ADJ,SERIAL_NO,ITEM_CODE,ITEM_UOM,ADJ_QTY," & vbCrLf _
                       & " ITEM_IO,STOCK_TYPE,REMARKS,COMPANY_CODE,BATCH_NO,HEAT_NO) "
                    SqlStr = SqlStr & vbCrLf _
                       & " VALUES (" & Val(lblMKey.Text) & ", " & i & "," & vbCrLf _
                       & " '" & MainClass.AllowSingleQuote(mItemCode) & "', " & vbCrLf _
                       & " '" & MainClass.AllowSingleQuote(mUOM) & "', " & vbCrLf _
                       & " " & mAdjQty & ", " & vbCrLf _
                       & " '" & MainClass.AllowSingleQuote(mIO) & "', " & vbCrLf _
                       & " '" & MainClass.AllowSingleQuote(mStkType) & "', " & vbCrLf _
                       & " '" & MainClass.AllowSingleQuote(mRemarks) & "'," & RsCompany.Fields("COMPANY_CODE").Value & ",'" & MainClass.AllowSingleQuote(mLotNo) & "','" & MainClass.AllowSingleQuote(mHeatNo) & "') "

                    PubDBCn.Execute(SqlStr)
                    '' VARCHAR2 (15) NULL, 
                End If

                'If MainClass.ValidateWithMasterTable(mItemCode, "ITEM_CODE", "DSP_RPT_FLAG", "INV_ITEM_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DSP_RPT_FLAG='Y'") = True Then
                '    xFGBatchNo = "OP1"
                'Else
                '    xFGBatchNo = ""
                'End If

                If UpdateStockTRN(PubDBCn, ConStockRefType_ADJ, (txtADJNo.Text), i, (txtADJDate.Text), (txtADJDate.Text), mStkType, mItemCode, mUOM, mLotNo, mAdjQty, 0, mIO, 0, 0, "", "", (txtDept.Text), (txtDept.Text), "", "N", IIf(mRemarks = "", "STOCK ADJUSTMENT", mRemarks), "", (lblStockID.Text), mDivisionCode, "", "", mStatus, mHeatNo) = False Then GoTo UpdateDetail1

            Next
        End With
        UpdateDetail1 = True
        Exit Function
UpdateDetail1:
        UpdateDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume	
    End Function
    Private Function FieldsVarification() As Boolean

        On Error GoTo err_Renamed
        Dim mRow As Integer
        Dim mSTTaxcount As Integer
        Dim mIsAuthorisedUser As String
        Dim mStockUser As String

        FieldsVarification = True

        If chkUpDateStock.CheckState = System.Windows.Forms.CheckState.Checked Then
            If ValidateBranchLocking((txtADJDate.Text)) = True Then
                FieldsVarification = False
                Exit Function
            End If
            If ValidateBookLocking(PubDBCn, CInt(ConLockSTN), txtADJDate.Text) = True Then
                FieldsVarification = False
                Exit Function
            End If

            mStockUser = GetUserPermission("ALLOW_STOCK_ADJ", "N", PubUserID, RsCompany.Fields("COMPANY_CODE").Value)

            If mStockUser = "N" Then
                MsgInformation("You have no Rights to Save Stock Adj.")
                FieldsVarification = False
                Exit Function
            End If
        End If

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsAdjMain.EOF = True Then Exit Function

        If MODIFYMode = True And txtADJNo.Text = "" Then
            MsgInformation("Ref No. cann't Blank")
            FieldsVarification = False
            Exit Function
        End If

        If txtADJDate.Text = "" Then
            MsgBox("Date is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtADJDate.Focus()
            Exit Function
        Else
            If chkUpDateStock.CheckState = System.Windows.Forms.CheckState.Checked Then
                If FYChk((txtADJDate.Text)) = False Then
                    FieldsVarification = False
                    If txtADJDate.Enabled = True Then txtADJDate.Focus()
                    Exit Function
                End If
            End If
        End If


        If Trim(txtDept.Text) = "" Then
            MsgBox("Department Name is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtDept.Focus()
            Exit Function
        End If

        If Trim(cboDivision.Text) = "" Then
            MsgBox("Division Name is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            cboDivision.Focus()
            Exit Function
        End If

        If Trim(txtEmp.Text) = "" Then
            MsgBox("Employee Name is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtDept.Focus()
            Exit Function
        End If

        If Trim(txtCost.Text) = "" Then
            MsgBox("Cost Center Name is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            txtDept.Focus()
            Exit Function
        End If

        If chkUpDateStock.CheckState = System.Windows.Forms.CheckState.Checked Then
            '        If RsCompany.Fields("COMPANY_CODE").Value = 3 Or RsCompany.Fields("COMPANY_CODE").Value = 10 Then	
            '            If PubSuperUser <> "S" Then	
            '                MsgBox "You have no rights to Stock Adjustment.", vbInformation	
            '                FieldsVarification = False	
            '                Exit Function	
            '            End If	
            '        Else	
            If PubSuperUser <> "S" Then
                mIsAuthorisedUser = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
                If InStr(1, mIsAuthorisedUser, "S") = 0 Then
                    '            If mCheckLastEntryDate <> "" Then	
                    If CDate(txtADJDate.Text) < CDate(PubCurrDate) Then
                        MsgBox("Cann't be Add or Modify Back Entry", MsgBoxStyle.Information)
                        FieldsVarification = False
                        Exit Function
                    End If
                    '            End If	
                End If
            End If
            '        End If	
        End If


        Call CalcTots()

        If MainClass.ValidDataInGrid(SprdMain, ColItemCode, "S", "ItemCode Is Blank.") = False Then FieldsVarification = False : Exit Function
        '    If MainClass.ValidDataInGrid(SprdMain, ColAdjQty, "N", "Please Check Quantity.") = False Then FieldsVarification = False: Exit Function	
        If MainClass.ValidDataInGrid(SprdMain, ColStockType, "S", "Please Check Stock Type.") = False Then FieldsVarification = False : Exit Function

        Exit Function
err_Renamed:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume	
    End Function
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Public Sub frmStoreAdjustment_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        '    If lblStockID.text = "WH" Then	
        '        Me.text = "Stock Adjustment (Store)"	
        '    Else	
        '        Me.text = "Stock Adjustment (Production)"	
        '    End If	

        If lblStockID.Text = ConWH Then
            Me.Text = "Stock Adjustment (Store)"
        ElseIf lblStockID.Text = ConPH Then
            Me.Text = "Stock Adjustment (Production)"
        Else
            Me.Text = "Stock Adjustment (Sub Store)"
        End If


        SqlStr = ""
        SqlStr = "Select * from INV_ADJ_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAdjMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = ""
        SqlStr = "Select * from INV_ADJ_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAdjDetail, ADODB.LockTypeEnum.adLockReadOnly)

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

        SqlStr = "SELECT  AUTO_KEY_ADJ AS SRN_NO, ADJ_DATE, DEPT_CODE, EMP_CODE, REMARKS "

        ''FROM CLAUSE...	

        SqlStr = SqlStr & vbCrLf & " FROM INV_ADJ_HDR "

        ''WHERE CLAUSE...	

        SqlStr = SqlStr & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & vbCrLf & " and SUBSTR(AUTO_KEY_ADJ,LENGTH(AUTO_KEY_ADJ)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        SqlStr = SqlStr & vbCrLf & " AND BOOKTYPE='" & lblStockID.Text & "'"

        '    If lblStockID.text = "WH" Then	
        '        SqlStr = SqlStr & vbCrLf & " AND DEPT_CODE='STR'"	
        '    Else	
        '        SqlStr = SqlStr & vbCrLf & " AND DEPT_CODE<>'STR'"	
        '    End If	

        ''ORDER BY CLAUSE...	

        SqlStr = SqlStr & vbCrLf & " Order by AUTO_KEY_ADJ"

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
            .set_ColWidth(1, 1500)
            .set_ColWidth(2, 1500)


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
            .set_RowHeight(0, ConRowHeight * 2)
            .Row = Arow
            .set_RowHeight(Arow, ConRowHeight * 1.5)

            .Col = ColItemCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsAdjDetail.Fields("ITEM_CODE").DefinedSize ''	
            .set_ColWidth(ColItemCode, 8)

            .Col = ColItemDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeEditLen = MainClass.SetMaxLength("ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn)
            .set_ColWidth(ColItemDesc, 20)

            .Col = ColUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsAdjDetail.Fields("ITEM_UOM").DefinedSize ''	
            .set_ColWidth(ColUOM, 4)

            .Col = ColLotNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsAdjDetail.Fields("BATCH_NO").DefinedSize ''	
            .set_ColWidth(ColLotNo, 4)
            .ColsFrozen = ColLotNo

            .Col = ColHeatNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsAdjDetail.Fields("HEAT_NO").DefinedSize ''	
            .set_ColWidth(ColHeatNo, 4)

            .Col = ColStockType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = MainClass.SetMaxLength("STOCK_TYPE", "INV_ADJ_DET", PubDBCn)
            .set_ColWidth(ColStockType, 4)


            .Col = ColStockQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColStockQty, 10)

            .Col = ColAdjQty
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColAdjQty, 10)

            .Col = ColRate
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.999")
            .TypeFloatMin = CDbl("-999999999.999")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColRate, 7)

            .Col = ColAdjAmount
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 3
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColAdjAmount, 10)

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .TypeEditLen = MainClass.SetMaxLength("REMARKS", "INV_ADJ_DET", PubDBCn)
            .set_ColWidth(ColRemarks, 15)

        End With
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColItemDesc, ColUOM)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColStockQty, ColStockQty)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColRate, ColRate)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColAdjAmount, ColAdjAmount)
        MainClass.SetSpreadColor(SprdMain, Arow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then RsAdjDetail.Requery() : Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1
        With RsAdjMain
            txtADJDate.MaxLength = 10
            txtADJNo.MaxLength = .Fields("AUTO_KEY_ISS").Precision
            txtDept.MaxLength = .Fields("DEPT_CODE").DefinedSize
            txtEmp.MaxLength = .Fields("EMP_CODE").DefinedSize
            txtCost.MaxLength = .Fields("COST_CENTER_CODE").DefinedSize
            txtRemarks.MaxLength = .Fields("REMARKS").DefinedSize

        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub Show1()

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset = Nothing
        Dim mDivisionCode As Double
        Dim mDivisionDesc As String

        Clear1()

        With RsAdjMain
            If Not .EOF Then
                txtADJNo.Enabled = False
                lblMKey.Text = .Fields("AUTO_KEY_ADJ").Value

                txtADJNo.Text = IIf(IsDBNull(.Fields("AUTO_KEY_ADJ").Value), 0, .Fields("AUTO_KEY_ADJ").Value)
                txtADJDate.Text = VB6.Format(IIf(IsDBNull(.Fields("ADJ_DATE").Value), "", .Fields("ADJ_DATE").Value), "dd/MM/yyyy")
                txtDept.Text = IIf(IsDBNull(.Fields("DEPT_CODE").Value), "", .Fields("DEPT_CODE").Value)
                txtEmp.Text = IIf(IsDBNull(.Fields("EMP_CODE").Value), "", .Fields("EMP_CODE").Value)
                txtCost.Text = IIf(IsDBNull(.Fields("COST_CENTER_CODE").Value), "", .Fields("COST_CENTER_CODE").Value)
                '            txtsubdept.Text = IIf(IsNull(!REMARKS), "", !REMARKS)	

                If MainClass.ValidateWithMasterTable(txtDept.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    lblDeptname.Text = MasterNo
                End If


                If MainClass.ValidateWithMasterTable(txtEmp.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    lblEmpname.Text = MasterNo
                End If

                If MainClass.ValidateWithMasterTable(txtCost.Text, "CC_CODE", "CC_DESC", "FIN_CCENTER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    lblCostctr.Text = MasterNo
                End If


                mDivisionCode = IIf(IsDBNull(.Fields("DIV_CODE").Value), -1, .Fields("DIV_CODE").Value)

                If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionDesc = Trim(MasterNo)
                    cboDivision.Text = mDivisionDesc
                End If

                txtRemarks.Text = IIf(IsDBNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)
                chkUpDateStock.CheckState = IIf(.Fields("UPD_STOCK").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                Call ShowDetail1(.Fields("AUTO_KEY_ADJ").Value, mDivisionCode)
                CmdPopFromFile.Enabled = False
            End If
        End With
        ADDMode = False
        MODIFYMode = False

        MainClass.ButtonStatus(Me, XRIGHT, RsAdjMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)

        '    FormatSprdMain -1	

        SprdMain.Enabled = True
        txtADJNo.Enabled = True
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume	
    End Sub
    Private Sub ShowDetail1(ByVal pReqNum As Double, ByRef mDivisionCode As Double)

        On Error GoTo ERR1
        Dim i As Integer
        Dim SqlStr As String = ""
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mSuppCode As String
        Dim mSuppName As String = ""
        Dim mAdjQty As Double
        Dim mItemUOM As String = ""
        Dim mStkType As String
        Dim mIO As String
        Dim mRate As Double
        Dim mLotNo As String
        Dim mHeatNo As String


        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf _
           & " FROM INV_ADJ_DET  " & vbCrLf _
           & " Where AUTO_KEY_ADJ = " & Val(CStr(pReqNum)) & "" & vbCrLf _
           & " Order By SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAdjDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsAdjDetail
            If .EOF = True Then Exit Sub
            '        FormatSprdMain -1	
            i = 1
            .MoveFirst()

            Do While Not .EOF

                SprdMain.Row = i

                SprdMain.Col = ColItemCode
                mItemCode = IIf(IsDBNull(.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                SprdMain.Text = mItemCode

                SprdMain.Col = ColUOM
                SprdMain.Text = IIf(IsDBNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)
                mItemUOM = IIf(IsDBNull(.Fields("ITEM_UOM").Value), "", .Fields("ITEM_UOM").Value)

                '
                SprdMain.Col = ColLotNo
                SprdMain.Text = IIf(IsDBNull(.Fields("BATCH_NO").Value), "", .Fields("BATCH_NO").Value)
                mLotNo = IIf(IsDBNull(.Fields("BATCH_NO").Value), "", .Fields("BATCH_NO").Value)

                SprdMain.Col = ColHeatNo
                SprdMain.Text = IIf(IsDBNull(.Fields("HEAT_NO").Value), "", .Fields("HEAT_NO").Value)
                mHeatno = IIf(IsDBNull(.Fields("HEAT_NO").Value), "", .Fields("HEAT_NO").Value)

                SprdMain.Col = ColStockType
                SprdMain.Text = IIf(IsDBNull(.Fields("STOCK_TYPE").Value), "", .Fields("STOCK_TYPE").Value)
                mStkType = IIf(IsDBNull(.Fields("STOCK_TYPE").Value), "", .Fields("STOCK_TYPE").Value)

                SprdMain.Col = ColAdjQty
                mIO = IIf(IsDBNull(.Fields("ITEM_IO").Value), "I", .Fields("ITEM_IO").Value)
                mAdjQty = System.Math.Abs(Val(IIf(IsDBNull(.Fields("ADJ_QTY").Value), "", .Fields("ADJ_QTY").Value)))
                mAdjQty = mAdjQty * IIf(mIO = "I", 1, -1)
                SprdMain.Text = CStr(mAdjQty)


                SprdMain.Col = ColStockQty
                SprdMain.Text = CStr(GetBalanceStockQty(mItemCode, (txtADJDate.Text), mItemUOM, Trim(txtDept.Text), mStkType, mLotNo, (lblStockID.Text), mDivisionCode,,,,, mHeatno))

                SprdMain.Col = ColRemarks
                SprdMain.Text = IIf(IsDBNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)

                SprdMain.Col = ColItemDesc
                MainClass.ValidateWithMasterTable(mItemCode, "Item_Code", "Item_Short_Desc", "Inv_Item_Mst", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
                mItemDesc = MasterNo
                SprdMain.Text = mItemDesc

                mRate = 0 ''GetLatestItemCostFromMRR(mItemCode, mItemUOM, 1, txtADJDate.Text, "L")	
                SprdMain.Col = ColRate
                SprdMain.Text = VB6.Format(mRate, "0.00")

                .MoveNext()

                i = i + 1
                SprdMain.MaxRows = i
            Loop
        End With
        Call CalcTots()
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
        MainClass.ButtonStatus(Me, XRIGHT, RsAdjMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub Clear1()

        Dim SqlStr As String = ""
        lblMKey.Text = ""

        txtADJDate.Text = VB6.Format(RunDate, "dd/MM/yyyy")
        txtADJNo.Text = ""
        txtDept.Text = ""
        txtEmp.Text = ""
        txtCost.Text = ""
        txtRemarks.Text = ""

        lblCostctr.Text = ""
        lblDeptname.Text = ""
        lblEmpname.Text = ""


        cboDivision.Text = GetDefaultDivision()        'cboDivision.SelectedIndex = -1
        cboDivision.Enabled = True

        CmdPopFromFile.Enabled = True
        chkUpDateStock.CheckState = System.Windows.Forms.CheckState.Checked
        MainClass.ClearGrid(SprdMain)
        Call FormatSprdMain(-1)

        Call AutoCompleteSearch("PAY_DEPT_MST", "DEPT_CODE", "", txtDept)
        Call AutoCompleteSearch("PAY_EMPLOYEE_MST", "EMP_CODE", "", txtEmp)
        Call AutoCompleteSearch("FIN_CCENTER_HDR", "CC_CODE", "", txtCost)


        SqlStr = "ADJ_DATE>=TO_DATE('" & VB6.Format(RsCompany.Fields("START_DATE").Value, "dd-MMM-yyyy") & "','DD-MON-YYYY') AND ADJ_DATE<=TO_DATE('" & VB6.Format(RsCompany.Fields("END_DATE").Value, "dd-MMM-yyyy") & "','DD-MON-YYYY')"


        Call AutoCompleteSearch("INV_ADJ_HDR", "TO_CHAR(AUTO_KEY_ADJ)", SqlStr, txtADJNo)

        MainClass.ButtonStatus(Me, XRIGHT, RsAdjMain, ADDMode, MODIFYMode, cmdAdd, cmdModify, cmdClose, cmdSave, cmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub frmStoreAdjustment_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub frmStoreAdjustment_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        '    MainClass.DoFunctionKey Me, KeyCode	
    End Sub
    Public Sub frmStoreAdjustment_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart

        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        'Set PvtDBCn = New ADODB.Connection	
        'PvtDBCn.Open StrConn	

        If lblStockID.Text = ConWH Then
            Me.Text = "Stock Adjustment (Store)"
        ElseIf lblStockID.Text = ConPH Then
            Me.Text = "Stock Adjustment (Production)"
        Else
            Me.Text = "Stock Adjustment (Sub Store)"
        End If

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        xMyMenu = myMenu
        MainClass.RightsToButton(Me, XRIGHT)

        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Call SetMainFormCordinate(Me)
        'Me.Top = 0
        'Me.Left = 0
        'Me.Height = VB6.TwipsToPixelsY(7245)
        ''Me.Width = VB6.TwipsToPixelsX(11355)

        cboDivision.Items.Clear()

        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboDivision.Text = GetDefaultDivision()        'cboDivision.SelectedIndex = -1


        AdoDCMain.Visible = False
        txtADJNo.Enabled = True
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

    Private Sub txtCost_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCost.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCost_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCost.DoubleClick
        Call cmdCCSearch_Click(cmdCCSearch, New System.EventArgs())
    End Sub

    Private Sub txtCost_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCost.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCost.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCost_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCost.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtCost_DoubleClick(txtCost, New System.EventArgs())
    End Sub

    Private Sub txtCost_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCost.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If txtCost.Text = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable(txtCost.Text, "CC_CODE", "CC_DESC", "FIN_CCENTER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            lblCostctr.Text = MasterNo
        Else
            MsgInformation("Invalid CostC Code")
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtADJDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtADJDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtADJDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtADJDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If Trim(txtADJDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtADJDate.Text) Then
            MsgInformation("Invalid Date")
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDept_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.DoubleClick
        Call cmdDeptSearch_Click(cmdDeptSearch, New System.EventArgs())
    End Sub

    Private Sub txtDept_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDept.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDept.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDept_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDept.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtDept_DoubleClick(txtDept, New System.EventArgs())
    End Sub

    Private Sub txtDept_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDept.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If txtDept.Text = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable(txtDept.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            lblDeptname.Text = MasterNo
        Else
            MsgInformation("Invalid Depatment Code")
            Cancel = True
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtEmp_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmp.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtEmp_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmp.DoubleClick
        Call cmdEmpSearch_Click(cmdEmpSearch, New System.EventArgs())
    End Sub

    Private Sub txtEmp_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEmp.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtEmp.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEmp_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtEmp.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then txtEmp_DoubleClick(txtEmp, New System.EventArgs())
    End Sub

    Private Sub txtEmp_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmp.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart

        If txtEmp.Text = "" Then GoTo EventExitSub
        txtEmp.Text = VB6.Format(txtEmp.Text, "000000")
        If MainClass.ValidateWithMasterTable(txtEmp.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            lblEmpname.Text = MasterNo
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


    Private Sub txtADJNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtADJNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtADJNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtADJNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtADJNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtADJNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtADJNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mReqnum As String = ""

        If Trim(txtADJNo.Text) = "" Then GoTo EventExitSub

        If Len(txtADJNo.Text) < 6 Then
            txtADJNo.Text = Trim(txtADJNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        If MODIFYMode = True And RsAdjMain.EOF = False Then mReqnum = RsAdjMain.Fields("AUTO_KEY_ADJ").Value

        Sqlstr = "Select * From INV_ADJ_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " And SUBSTR(AUTO_KEY_ADJ,LENGTH(AUTO_KEY_ADJ)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " And AUTO_KEY_ADJ=" & Val(txtADJNo.Text) & ""

        Sqlstr = Sqlstr & " And BOOKTYPE='" & lblStockID.Text & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAdjMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsAdjMain.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such ENTRY, Use Generate NEW ENTRY Option To add", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "Select * From INV_ADJ_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUBSTR(AUTO_KEY_ADJ,LENGTH(AUTO_KEY_ADJ)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_ADJ=" & Val(mReqnum) & ""

                SqlStr = SqlStr & " AND BOOKTYPE='" & lblStockID.Text & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAdjMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub PopulateFromXLSFile(ByVal strXLSFile As String)

        On Error GoTo ErrPart
        Dim i As Integer
        Dim SqlStr As String = ""
        Dim mItemCode As String
        Dim mItemDesc As String
        Dim mUOM As String = ""
        Dim mStockType As String = ""
        Dim mStockQty As Double
        Dim mAdjQty As Double
        Dim xSqlStr As String
        Dim mSqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsFile As ADODB.Recordset
        Dim FileConnStr As String

        Dim strTemp As String
        Dim strWkShName As String
        Dim strError As String = ""
        Dim mDivisionCode As Double
        Dim mBatchNO As String
        Dim mHEatNo As String

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = Trim(MasterNo)
        End If

        MainClass.ClearGrid(SprdMain)
        FormatSprdMain(-1)


        FileConnStr = "Provider=MSDASQL.1;Connect Timeout=15;Extended Properties='DSN=Excel Files;DBQ=XXLSFILEX;DefaultDir=XXLSDIRX;DriverId=790;FIL=excel 8.0;MaxBufferSize=2048;PageTimeout=5;UID=admin;';Locale Identifier=1033"
        FileConnStr = Replace(FileConnStr, "XXLSFILEX", strXLSFile)
        strTemp = Mid(strXLSFile, 1, InStrRev(strXLSFile, "\") - 1)
        FileConnStr = Replace(FileConnStr, "XXLSDIRX", strTemp)

        If Not XLSConnect(Trim(FileConnStr), FileDBCn) Then
            GoTo ErrPart
        End If

        RsFile = FileDBCn.OpenSchema(ADODB.SchemaEnum.adSchemaTables)
        strWkShName = RsFile.Fields("Table_Name").Value

        mSqlStr = "SELECT * FROM ""XWKSHTX"" " ''WHERE F1 <> NULL"	
        mSqlStr = Replace(mSqlStr, "XWKSHTX", strWkShName)

        If OpenExcelRecordSet(mSqlStr, RsFile, strError, FileDBCn, False) = 0 Then

            If RsFile.EOF = False Then
                Do While Not RsFile.EOF
                    mItemCode = Trim(IIf(IsDbNull(RsFile.Fields(0).Value), "", RsFile.Fields(0).Value))

                    xSqlStr = " SELECT ITEM_SHORT_DESC,ISSUE_UOM " & vbCrLf & " FROM INV_ITEM_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND LTRIM(RTRIM(ITEM_CODE))='" & MainClass.AllowSingleQuote(mItemCode) & "'"
                    MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                    If RsTemp.EOF = False Then
                        mItemDesc = Trim(IIf(IsDbNull(RsTemp.Fields("ITEM_SHORT_DESC").Value), "", RsTemp.Fields("ITEM_SHORT_DESC").Value))
                        mUOM = Trim(IIf(IsDbNull(RsTemp.Fields("ISSUE_UOM").Value), "", RsTemp.Fields("ISSUE_UOM").Value))
                    Else
                        GoTo NextRecord
                    End If
                    If DuplicateItem = True Then GoTo NextRecord

                    mStockType = Trim(IIf(IsDbNull(RsFile.Fields(3).Value), "", RsFile.Fields(3).Value))
                    If MainClass.ValidateWithMasterTable(mStockType, "STOCK_TYPE_CODE", "STOCK_TYPE_CODE", "INV_TYPE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then GoTo NextRecord

                    mAdjQty = Val(IIf(IsDBNull(RsFile.Fields(4).Value), 0, RsFile.Fields(4).Value))
                    mBatchNO = Trim(IIf(IsDBNull(RsFile.Fields(5).Value), "", RsFile.Fields(5).Value))
                    mHEatNo = Trim(IIf(IsDBNull(RsFile.Fields(6).Value), "", RsFile.Fields(6).Value))

                    If mAdjQty = 0 Then GoTo NextRecord
                    mStockQty = GetBalanceStockQty(mItemCode, (txtADJDate.Text), mUOM, Trim(txtDept.Text), mStockType, "", (lblStockID.Text), mDivisionCode)

                    SprdMain.Row = SprdMain.MaxRows

                    SprdMain.Col = ColItemCode
                    SprdMain.Text = mItemCode

                    SprdMain.Col = ColItemDesc
                    SprdMain.Text = mItemDesc


                    SprdMain.Col = ColUOM
                    SprdMain.Text = mUOM

                    SprdMain.Col = ColStockType
                    SprdMain.Text = mStockType

                    SprdMain.Col = ColStockQty
                    SprdMain.Text = CStr(mStockQty)

                    SprdMain.Col = ColAdjQty
                    SprdMain.Text = CStr(mAdjQty)

                    SprdMain.Col = ColLotNo
                    SprdMain.Text = mBatchNO


                    SprdMain.Col = ColHeatNo
                    SprdMain.Text = mHEatNo




                    SprdMain.MaxRows = SprdMain.MaxRows + 1
                    '               FormatSprdMain -1, False	

NextRecord:
                    RsFile.MoveNext()
                Loop
            End If
        End If

        If RsFile.State = ADODB.ObjectStateEnum.adStateOpen Then RsFile.Close()
        RsFile = Nothing

        If FileDBCn.State = ADODB.ObjectStateEnum.adStateOpen Then
            FileDBCn.Close()
            FileDBCn = Nothing
        End If

        '    CmdPopFromFile.Enabled = False	
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '        Resume	
    End Sub
End Class
