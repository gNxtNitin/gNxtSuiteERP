Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility

Imports Infragistics.Shared
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinDataSource
Imports System.Data.OleDb

Friend Class frmOperatorMaster
    Inherits System.Windows.Forms.Form
    Dim RsEmp As ADODB.Recordset = Nothing

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection

    Dim Shw As Boolean
    Dim xCode As String
    Dim SqlStr As String = ""
    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12

    Private Const ColCode As Short = 1
    Private Const ColDesc As Short = 2
    Private Const ColPer As Short = 3
    Private Const ColAmt As Short = 4
    Private Const ColForm1Amt As Short = 5
    Private Const ColChk As Short = 6

    Private Const ColSpouseName As Short = 1
    Private Const ColSpouseRel As Short = 2
    Private Const ColSpouseGender As Short = 3
    Private Const ColBloodGroup As Short = 4
    Private Const ColSpouseDOB As Short = 5

    Private Const ColAssetDesc As Short = 1
    Private Const ColAssetMake As Short = 2
    Private Const ColAssetPrice As Short = 3
    Private Const ColAssetDOP As Short = 4
    Private Const ColAssetDOI As Short = 5
    Private Const ColAssetRemarks As Short = 6


    Private Const ColOpening As Short = 3
    Private Const ColTotEntitle As Short = 4

    Private Sub ViewGrid()

        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        ADataMain.Refresh
            FormatSprdView()
            SprdView.Refresh()

            SprdView.Focus()
            SprdView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            SprdView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsEmp, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub Clear1()

        txtEmpNo.Text = ""
        TxtName.Text = ""
        txtFName.Text = ""
        'txtBloodGroup.Text = ""

        txtDOB.Text = ""



        txtQualification.Text = ""

        txtDOJ.Text = ""

        txtDOL.Text = ""

        txtWorkingFrom.Text = ""
        txtWorkingTo.Text = ""
        txtWorkingHours.Text = 8

        txtCostCenter.Text = ""

        txtEmpNo.Enabled = True
        cmdSearch.Enabled = True

        txtContractor.Text = ""

        txtContractor.Enabled = False '' IIf(lblEmpType.Caption = "C", True, False)

        cboDivision.SelectedIndex = -1
        cboDivision.Enabled = True


        cboSex.SelectedIndex = -1

        cboMStatus.SelectedIndex = -1

        cboDept.SelectedIndex = -1


        cboPcRateType.SelectedIndex = 0
        cboPcRateType.Enabled = True


        Call AutoCompleteSearchSQL("SELECT CC_DESC FROM FIN_CCENTER_HDR IH, FIN_CCENTER_DET ID WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IH.COMPANY_CODE=ID.COMPANY_CODE AND IH.CC_CODE=ID.CC_CODE Order By 1", "CC_DESC", txtCostCenter)

        MainClass.ButtonStatus(Me, XRIGHT, RsEmp, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub cboDept_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDept.SelectedIndexChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        Dim mDeptCode As String = ""

        If Trim(cboDept.Text) = "" Then Exit Sub

        If MainClass.ValidateWithMasterTable((cboDept.Text), "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDeptCode = MasterNo
        End If
        Call AutoCompleteSearchSQL("SELECT CC_DESC FROM FIN_CCENTER_HDR IH, FIN_CCENTER_DET ID WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IH.COMPANY_CODE=ID.COMPANY_CODE AND IH.CC_CODE=ID.CC_CODE AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(mDeptCode) & "' Order By 1", "CC_DESC", txtCostCenter)

    End Sub
    Private Sub cboDivision_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboDivision_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDivision.SelectedIndexChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboMStatus_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboMStatus.SelectedIndexChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboPcRateType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboPcRateType.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboPcRateType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboPcRateType.SelectedIndexChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cboSex_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboSex.SelectedIndexChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPreview.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPrint(Crystal.DestinationConstants.crptToWindow)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportForPrint(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mRptFileName As String
        Exit Sub


        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        If Err.Number = 32755 Or Err.Number = 20507 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        Exit Sub
ERR2:
        MsgInformation(Err.Description)
        If Err.Number = 32755 Or Err.Number = 20507 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        PubDBCn.RollbackTrans()
        'Resume
    End Sub

    Private Function TempFillPrintDummyData(ByRef GridName As Object, ByVal prmStartGridRow As Integer, ByVal prmEndGridRow As Integer, ByVal prmStartGridCol As Integer, ByVal prmEndGridCol As Integer, ByRef mDefaultValue As String, ByRef mPvtDBCn As ADODB.Connection) As Boolean

        ' This procedure fills the Grid Data into PrintDummy table for printing...
        On Error GoTo PrintDummyErr

        Dim RSPrintDummy As ADODB.Recordset
        Dim FieldCnt As Short
        Dim RowNum As Short
        Dim FieldNum As Short
        Dim GetData As String
        Dim SetData As String
        Dim SqlStr As String = ""

        For RowNum = prmStartGridRow To prmEndGridRow
            FieldCnt = 1
            SetData = ""
            GetData = ""
            GridName.Row = RowNum
            For FieldNum = prmStartGridCol To prmEndGridCol
                GridName.Col = FieldNum
                If FieldNum = prmStartGridCol Then
                    SetData = "FIELD" & FieldCnt
                    GetData = "'" & MainClass.AllowSingleQuote(GridName.Text) & "'"
                ElseIf FieldNum = ColAmt Or FieldNum = ColForm1Amt Then
                    SetData = SetData & ", " & "FIELD" & FieldCnt
                    GetData = GetData & ", " & "'" & VB6.Format(Val(GridName.Text) * IIf(mDefaultValue = "3", -1, 1), "0.00") & "'"
                Else
                    SetData = SetData & ", " & "FIELD" & FieldCnt
                    GetData = GetData & ", " & "'" & MainClass.AllowSingleQuote(GridName.Text) & "'"
                End If
                FieldCnt = FieldCnt + 1
            Next
            SqlStr = " INSERT INTO TEMP_PRINTDUMMYDATA (USERID, SUBROW, FIELD10, " & vbCrLf _
                & " " & SetData & ") " & vbCrLf & " VALUES (" & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & RowNum & ", '" & mDefaultValue & "'," & vbCrLf _
                & " " & GetData & ") "
            mPvtDBCn.Execute(SqlStr)
NextRec:
        Next



        TempFillPrintDummyData = True

        Exit Function
PrintDummyErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        TempFillPrintDummyData = False
        '    mPvtDBCn.RollbackTrans
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)

        Dim mEmpName As String
        Dim mEmpDegn As String
        Dim mWef As String
        Dim mBasic As String
        Dim mGrossAmount As String
        Dim mAddress As String
        Dim mGrade As String
        Dim mDOI As String
        Dim mUnit As String

        Exit Sub
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        '    MainClass.AssignCRptFormulas Report1, "mGrossAmount='" & txtGSalary.Text & "'"

        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub
    'Private Sub txtBloodGroup_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)

    '    MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    'End Sub

    'Private Sub txtBloodGroup_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs)
    '    Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

    '    KeyAscii = MainClass.UpperCase(KeyAscii, txtBloodGroup.Text)
    '    eventArgs.KeyChar = Chr(KeyAscii)
    '    If KeyAscii = 0 Then
    '        eventArgs.Handled = True
    '    End If
    'End Sub
    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick

        SqlStr = ""
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow

        SqlStr = "SELECT * FROM PAY_CONT_EMPLOYEE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote((SprdView.Text)) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmp, ADODB.LockTypeEnum.adLockOptimistic)
        If RsEmp.EOF = False Then
            Clear1()
            Show1()
            CmdView_Click(CmdView, New System.EventArgs())
        End If
    End Sub
    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub
    Private Sub txtCostCenter_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCostCenter.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtCostCenter_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCostCenter.DoubleClick
        Call SearchCCenter()
    End Sub

    Private Sub txtCostCenter_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCostCenter.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCostCenter.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCostCenter_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCostCenter.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            Call SearchCCenter()
        End If
    End Sub

    Private Sub txtCostCenter_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCostCenter.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ErrPart
        Dim mCostCenter As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mDeptCode As String

        mCostCenter = Trim(txtCostCenter.Text)

        If mCostCenter = "" Then GoTo EventExitSub

        If Trim(cboDept.Text) = "" Then
            MsgInformation("Please Select Dept. First.")
            cboDept.Focus()
            GoTo EventExitSub
        End If

        If MainClass.ValidateWithMasterTable((cboDept.Text), "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDeptCode = MasterNo
        Else
            MsgInformation("Invalid Department Code. Cannot Save")
            cboDept.Focus()
            GoTo EventExitSub
        End If

        SqlStr = " SELECT IH.CC_CODE, IH.CC_DESC, ID.DEPT_CODE " & vbCrLf & " FROM FIN_CCENTER_HDR IH, FIN_CCENTER_DET ID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=ID.COMPANY_CODE AND IH.CC_CODE=ID.CC_CODE" & vbCrLf & " AND IH.CC_DESC='" & MainClass.AllowSingleQuote(mCostCenter) & "'" & vbCrLf & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(mDeptCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = True Then
            MsgInformation("Invalid Cost Center Code for Department : " & Trim(cboDept.Text))
            Cancel = True
        End If

        '    If MainClass.ValidateWithMasterTable(mCostCenter, "CC_DESC", "CC_CODE", "FIN_CCENTER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
        '        MsgInformation "Invalid Cost Center. Cannot Save"
        '        Cancel = True
        '    End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtContractor_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtContractor.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtContractor_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtContractor.DoubleClick
        SearchContractor()
    End Sub


    Private Sub txtContractor_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtContractor.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtContractor.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtContractor_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtContractor.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            SearchContractor()
        End If
    End Sub

    Private Sub txtContractor_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtContractor.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo EventExitSub


        If Trim(txtContractor.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable((txtContractor.Text), "CON_NAME", "CON_CODE", "PAY_CONTRACTOR_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgBox("Contractor Name Does Not Exist In Master" & vbCrLf & "Click Add To Add In Master", MsgBoxStyle.Information)
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDOJ_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDOJ.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtDOJ.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    '
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            txtEmpNo.Enabled = False
            cmdSearch.Enabled = False
            MainClass.ButtonStatus(Me, XRIGHT, RsEmp, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            txtEmpNo.Enabled = True
            cmdSearch.Enabled = True
            Show1()
        End If
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPrint(Crystal.DestinationConstants.crptToPrinter)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            If txtEmpNo.Enabled = True Then txtEmpNo.Focus()
        Else
            ADDMode = False
            MODIFYMode = False
            If RsEmp.EOF = False Then RsEmp.MoveFirst()
            Show1()
        End If
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If TxtName.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub
        If Not RsEmp.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                If Delete1() = False Then GoTo DelErrPart
                If RsEmp.EOF = True Then
                    Clear1()
                Else
                    Clear1()
                    Show1()
                End If
            End If
        End If
        Exit Sub
DelErrPart:
        MsgBox("Record Not Deleted")
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If lblEmpType.Text = "O" Then

        ElseIf lblEmpType.Text = "S" Then
            'SqlStr = SqlStr & " AND EMP_CAT_TYPE='1'"
            SqlStr = SqlStr & " AND EMP_CAT='1'"
        Else
            'SqlStr = SqlStr & " AND EMP_CAT_TYPE='2'"
            SqlStr = SqlStr & " AND EMP_CAT='2'"
        End If

        If MainClass.SearchGridMaster((txtEmpNo.Text), "PAY_CONT_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            txtEmpNo.Text = AcName1
            TxtEmpNo_Validating(txtEmpNo, New System.ComponentModel.CancelEventArgs(False))
            If txtEmpNo.Enabled = True Then txtEmpNo.Focus()
        End If

        Exit Sub

    End Sub
    Private Sub SearchContractor()

        If MainClass.SearchGridMaster((txtContractor.Text), "PAY_CONTRACTOR_MST", "CON_NAME", , , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtContractor.Text = AcName1
            txtContractor_Validating(txtContractor, New System.ComponentModel.CancelEventArgs(False))
            If txtContractor.Enabled = True Then txtContractor.Focus()
        End If

        Exit Sub

    End Sub
    Private Sub frmOperatorMaster_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub txtQualification_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtQualification.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtQualification_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtQualification.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtQualification.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtDOB_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDOB.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtDOB_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDOB.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDOB.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtDOB.Text) Then
            MsgInformation("Please enter the vaild date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtDOJ_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDOJ.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtDOJ_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDOJ.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDOJ.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtDOJ.Text) Then
            MsgInformation("Please enter the vaild date.")
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtDOL_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDOL.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDOL_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDOL.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDOL.Text) = "" Then GoTo EventExitSub

        If Not IsDate(txtDOL.Text) Then
            MsgInformation("Please enter the vaild date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtEmpNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtEmpNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEmpNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtEmpNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtFName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtFName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtFName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub TxtName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtName.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Public Sub frmOperatorMaster_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        SqlStr = ""
        If FormActive = True Then Exit Sub
        MainClass.UOpenRecordSet("SELECT * FROM PAY_CONT_EMPLOYEE_MST WHERE 1<>1", PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmp, ADODB.LockTypeEnum.adLockOptimistic)
        AssignGrid(False)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        settextlength()
        FillComboMst()
        'Clear1()
        FormActive = True
        If CmdAdd.Enabled = True Then cmdAdd_Click(CmdAdd, New System.EventArgs())

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '    Resume
    End Sub
    Private Sub frmOperatorMaster_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        'Me.Height = VB6.TwipsToPixelsY(7245)
        'Me.Width = VB6.TwipsToPixelsX(11355)
        Me.Left = VB6.TwipsToPixelsX(20)
        Me.Top = VB6.TwipsToPixelsY(20)
        '    FillComboMst
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub
    Private Sub FillComboMst()

        Dim RS As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""


        cboSex.Items.Clear()
        cboSex.Items.Add("Male")
        cboSex.Items.Add("Female")

        cboMStatus.Items.Clear()
        cboMStatus.Items.Add("Married")
        cboMStatus.Items.Add("Unmarried")



        MainClass.FillCombo(cboDept, "PAY_DEPT_MST", "DEPT_DESC", , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "")
        '    MainClass.FillCombo cboMajorDept, "PAY_DEPT_MST", "DEPT_DESC", , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""




        cboPcRateType.Items.Clear()
        cboPcRateType.Items.Add("GENERAL")
        cboPcRateType.Items.Add("PC Rate")
        'cboPcRateType.Items.Add("OLD")
        'cboPcRateType.Items.Add("1. OTHER")
        'cboPcRateType.Items.Add("2. OTHER II")
        'cboPcRateType.Items.Add("3. OTHER III")
        cboPcRateType.SelectedIndex = 0

        cboDivision.Items.Clear()

        SqlStr = "SELECT DIV_DESC FROM INV_DIVISION_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " ORDER BY DIV_DESC"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.EOF = False Then
            Do While RS.EOF = False
                cboDivision.Items.Add(RS.Fields("DIV_DESC").Value)
                RS.MoveNext()
            Loop
        End If

        cboDivision.SelectedIndex = -1

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmOperatorMaster_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        '    'PvtDBCn.Cancel
        '    'PvtDBCn.Close
        RsEmp = Nothing
        '    'Set PvtDBCn = Nothing
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim mCategoryName As String
        Dim mCostCenter As String
        Dim mDivisionCode As Double
        Dim mDivisionDesc As String
        Dim mHODName As String
        Dim mHODCode As String
        Dim mValue As String
        Dim EmpPFCont As String

        Shw = True
        With RsEmp
            If Not RsEmp.EOF Then

                txtEmpNo.Text = IIf(IsDBNull(.Fields("EMP_CODE").Value), "", .Fields("EMP_CODE").Value)
                TxtName.Text = IIf(IsDBNull(.Fields("EMP_NAME").Value), "", .Fields("EMP_NAME").Value)


                txtFName.Text = IIf(IsDBNull(.Fields("EMP_FNAME").Value), "", .Fields("EMP_FNAME").Value)
                'txtBloodGroup.Text = IIf(IsDBNull(.Fields("BLOOD_GROUP").Value), "", .Fields("BLOOD_GROUP").Value)
                txtDOB.Text = VB6.Format(IIf(IsDBNull(.Fields("EMP_DOB").Value), "", .Fields("EMP_DOB").Value), "DD/MM/YYYY")
                txtQualification.Text = IIf(IsDBNull(.Fields("EMP_QUALIFICATION").Value), "", .Fields("EMP_QUALIFICATION").Value)
                txtDOJ.Text = VB6.Format(IIf(IsDBNull(.Fields("EMP_DOJ").Value), "", .Fields("EMP_DOJ").Value), "DD/MM/YYYY")
                txtDOL.Text = VB6.Format(IIf(IsDBNull(.Fields("EMP_LEAVE_DATE").Value), "", .Fields("EMP_LEAVE_DATE").Value), "DD/MM/YYYY")
                txtWorkingFrom.Text = IIf(IsDBNull(.Fields("WORKINGTIMEFROM").Value), "", .Fields("WORKINGTIMEFROM").Value)
                txtWorkingTo.Text = IIf(IsDBNull(.Fields("WORKINGTIMETO").Value), "", .Fields("WORKINGTIMETO").Value)
                'txtWorkingHours.Text = IIf(IsDBNull(.Fields("WORKING_HOURS").Value), "", .Fields("WORKING_HOURS").Value)


                'EmpPFCont = IIf(IsDBNull(RsEmp.Fields("EMP_CONT").Value), "B", RsEmp.Fields("EMP_CONT").Value)


                If .Fields("EMP_DEPT_CODE").Value <> "" Then
                    If MainClass.ValidateWithMasterTable(.Fields("EMP_DEPT_CODE").Value, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        cboDept.Text = MasterNo
                    End If
                End If

                mCostCenter = IIf(IsDBNull(.Fields("COST_CENTER_CODE").Value), "", .Fields("COST_CENTER_CODE").Value)

                If mCostCenter <> "" Then
                    If MainClass.ValidateWithMasterTable(mCostCenter, "CC_CODE", "CC_DESC", "FIN_CCENTER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        txtCostCenter.Text = MasterNo
                    End If
                End If

                '            If !EMP_MAJOR_DEPT <> "" Then
                '                If MainClass.ValidateWithMasterTable(!EMP_MAJOR_DEPT, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '                    cboMajorDept.Text = MasterNo
                '                End If
                '            End If


                cboSex.Text = IIf(.Fields("EMP_SEX").Value = "M", "Male", "Female")
                cboMStatus.Text = IIf(.Fields("EMP_MARITAL_STATUS").Value = "M", "Married", "Unmarried")

                If .Fields("EMP_RATE_TYPE").Value = "G" Then
                    cboPcRateType.Text = "GENERAL"
                ElseIf .Fields("EMP_RATE_TYPE").Value = "P" Then
                    cboPcRateType.Text = "Pc RATE"
                ElseIf .Fields("EMP_RATE_TYPE").Value = "N" Then
                    cboPcRateType.Text = "NEW"
                ElseIf .Fields("EMP_RATE_TYPE").Value = "O" Then
                    cboPcRateType.Text = "OLD"
                ElseIf .Fields("EMP_RATE_TYPE").Value = "1" Then
                    cboPcRateType.Text = "1. OTHER"
                ElseIf .Fields("EMP_RATE_TYPE").Value = "2" Then
                    cboPcRateType.Text = "2. OTHER II"
                ElseIf .Fields("EMP_RATE_TYPE").Value = "3" Then
                    cboPcRateType.Text = "3. OTHER III"
                End If


                mValue = IIf(IsDBNull(.Fields("CONTRACTOR_CODE").Value), "", .Fields("CONTRACTOR_CODE").Value)
                If mValue <> "" Then
                    If MainClass.ValidateWithMasterTable(.Fields("CONTRACTOR_CODE").Value, "CON_CODE", "CON_NAME", "PAY_CONTRACTOR_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        txtContractor.Text = MasterNo
                    End If
                End If

                If PubSuperUser = "S" Or PubSuperUser = "A" Then
                    cboPcRateType.Enabled = IIf(lblEmpType.Text = "O", False, True)
                Else
                    cboPcRateType.Enabled = False ''IIf(GetEmpSalaryMade(txtEmpNo.Text) = True, False, True)
                End If

                mDivisionCode = IIf(IsDBNull(.Fields("DIV_CODE").Value), -1, .Fields("DIV_CODE").Value)

                If MainClass.ValidateWithMasterTable(mDivisionCode, "DIV_CODE", "DIV_DESC", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDivisionDesc = Trim(MasterNo)
                    cboDivision.Text = mDivisionDesc
                End If

            End If
        End With

        Shw = False
        ADDMode = False
        MODIFYMode = False
        If RsEmp.EOF = False Then
            xCode = RsEmp.Fields("EMP_CODE").Value
        End If


        MainClass.ButtonStatus(Me, XRIGHT, RsEmp, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
        If Err.Number = 383 Then
            Resume Next
        End If
    End Sub
    Private Function GetCboTextIndex(ByRef pComboBox As System.Windows.Forms.ComboBox) As Integer
        On Error GoTo GetERR
        Dim ii As Integer
        Dim JJ As Integer
        If Trim(pComboBox.Text) = "" Then GetCboTextIndex = -1 : Exit Function
        For ii = 0 To pComboBox.Items.Count - 1
            If pComboBox.Text = VB6.GetItemString(pComboBox, ii) Then
                JJ = JJ + 1
                Exit For
            End If
            JJ = JJ + 1
        Next ii
        GetCboTextIndex = JJ
        Exit Function
GetERR:
        MsgBox(Err.Description)
    End Function
    Private Sub SetCboText(ByRef pComboBox As System.Windows.Forms.ComboBox, ByRef pCboIndex As Integer)
        On Error GoTo GetERR
        Dim ii As Integer
        Dim JJ As Integer
        If pCboIndex = 0 Or pCboIndex = -1 Then pComboBox.Text = "" : Exit Sub
        pComboBox.Text = VB6.GetItemString(pComboBox, pCboIndex - 1)
        Exit Sub
GetERR:
        MsgBox(Err.Description)
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If Update1() = True Then
            TxtEmpNo_Validating(txtEmpNo, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True Then CmdAdd.Focus()
        Else
            MsgInformation("Record not saved")
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    Private Function Update1() As Boolean

        On Error GoTo UpdateError
        Dim mTaxRegime As String
        Dim mCode As String
        Dim mEmpType As String
        Dim mDeptCode As String
        Dim mMajorDeptCode As String
        Dim mDesgCode As String
        Dim mMaritalStatus As String
        Dim mSex As String
        Dim mShiftCode As String
        Dim mSalaryType As String
        Dim mESIFlag As String
        Dim mPFPensionFlag As String
        Dim mCategory As String
        Dim mWeeklyOff As String
        Dim mJoiningDesc As String
        Dim mPaymentMode As String
        Dim mGroupInsurance As String
        Dim mRGPAuthorization As String
        Dim mStopSalary As String
        Dim mAccountAdvanceCode As String
        Dim mAccountImprestCode As String
        Dim mGrossSalary As Double
        Dim mMetroCity As String
        Dim mContractCode As Double
        Dim mCostCenterCode As String
        Dim mBonusApp As String
        Dim mLEApp As String
        Dim mEmpCatType As String
        Dim mDivisionCode As Double
        Dim mCorporate As String
        Dim mEL As String
        Dim mHODCode As String
        Dim mPMetroCity As String
        Dim mOverTimeApp As String
        Dim EmpPFCont As String
        Dim mWFH As String
        Dim mMachineCode As Double

        Dim mHRHOD As String
        Dim mDeptHOD As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If MainClass.ValidateWithMasterTable(Trim(cboDivision.Text), "DIV_DESC", "DIV_CODE", "INV_DIVISION_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDivisionCode = CDbl(Trim(MasterNo))
        End If

        mSex = IIf(cboSex.Text = "Male", "M", "F")


        mMaritalStatus = IIf(cboMStatus.Text = "Married", "M", "U")

        If MainClass.ValidateWithMasterTable((cboDept.Text), "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDeptCode = MasterNo
        Else
            mDeptCode = CStr(-1)
        End If

        If MainClass.ValidateWithMasterTable((txtCostCenter.Text), "CC_DESC", "CC_CODE", "FIN_CCENTER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCostCenterCode = MasterNo
        Else
            mCostCenterCode = CStr(-1)
        End If

        If MainClass.ValidateWithMasterTable((txtContractor.Text), "CON_NAME", "CON_CODE", "PAY_CONTRACTOR_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mContractCode = MasterNo
        End If

        mCode = txtEmpNo.Text
        SqlStr = ""

        If ADDMode = True Then

            SqlStr = "INSERT INTO PAY_CONT_EMPLOYEE_MST ( " & vbCrLf _
                & " COMPANY_CODE,  EMP_CODE,  EMP_NAME," & vbCrLf _
                & " EMP_DEPT_CODE, EMP_MARITAL_STATUS, " & vbCrLf _
                & " EMP_SEX, EMP_DESG_CODE, " & vbCrLf _
                & " EMP_DOB, EMP_DOJ, " & vbCrLf _
                & " EMP_CAT, SHIFT_CODE, " & vbCrLf _
                & " EMP_LEAVE_DATE, EMP_ESI_FLAG, " & vbCrLf _
                & " COST_CENTER_CODE, EMP_FNAME, " & vbCrLf _
                & " WORKINGTIMEFROM, WORKINGTIMETO, " & vbCrLf _
                & " EMP_OT_RATE, CONTRACTOR_CODE, " & vbCrLf _
                & " ADDUSER, ADDDATE, " & vbCrLf _
                & " MODUSER, MODDATE, " & vbCrLf _
                & " EMP_RATE_TYPE, DIV_CODE, " & vbCrLf _
                & " WORKER_TYPE, OT_APP"

            SqlStr = SqlStr & vbCrLf & " )  VALUES ( "

            SqlStr = SqlStr & vbCrLf _
                & " " & RsCompany.Fields("COMPANY_CODE").Value & ", '" & mCode & "', '" & MainClass.AllowSingleQuote((TxtName.Text)) & "'," & vbCrLf _
                & " '" & mDeptCode & "', '" & mMaritalStatus & "'," & vbCrLf _
                & " '" & mSex & "', '-', " & vbCrLf _
                & " TO_DATE('" & VB6.Format(Trim(txtDOB.Text), "DD-MMM-YYYY") & "','DD-MON-YYYY'), TO_DATE('" & VB6.Format(txtDOJ.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " 'W','A'," & vbCrLf _
                & " TO_DATE('" & VB6.Format(Trim(txtDOL.Text), "DD-MMM-YYYY") & "','DD-MON-YYYY'), 'N'," & vbCrLf _
                & " '" & mCostCenterCode & "', '" & MainClass.AllowSingleQuote((txtFName.Text)) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote((txtWorkingFrom.Text)) & "',  '" & MainClass.AllowSingleQuote((txtWorkingTo.Text)) & "'," & vbCrLf _
                & " 1, " & mContractCode & "," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                & " '',''," & vbCrLf _
                & " '" & VB.Left(cboPcRateType.Text, 1) & "'," & mDivisionCode & "," & vbCrLf _
                & " 'W','Y' )"

        Else

            SqlStr = "UPDATE PAY_CONT_EMPLOYEE_MST SET " & vbCrLf _
                & " EMP_NAME = '" & MainClass.AllowSingleQuote((TxtName.Text)) & "'," & vbCrLf _
                & " EMP_DEPT_CODE= '" & mDeptCode & "', EMP_MARITAL_STATUS='" & mMaritalStatus & "', " & vbCrLf _
                & " EMP_SEX='" & mSex & "', EMP_DESG_CODE='-', " & vbCrLf _
                & " EMP_DOB=TO_DATE('" & VB6.Format(Trim(txtDOB.Text), "DD-MMM-YYYY") & "','DD-MON-YYYY'), EMP_DOJ=TO_DATE('" & VB6.Format(txtDOJ.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " EMP_CAT='W', SHIFT_CODE='A', " & vbCrLf _
                & " EMP_LEAVE_DATE=TO_DATE('" & VB6.Format(Trim(txtDOL.Text), "DD-MMM-YYYY") & "','DD-MON-YYYY'), EMP_ESI_FLAG='Y', " & vbCrLf _
                & " COST_CENTER_CODE='" & mCostCenterCode & "', EMP_FNAME='" & MainClass.AllowSingleQuote((txtFName.Text)) & "', " & vbCrLf _
                & " WORKINGTIMEFROM='" & MainClass.AllowSingleQuote((txtWorkingFrom.Text)) & "', WORKINGTIMETO='" & MainClass.AllowSingleQuote((txtWorkingTo.Text)) & "', " & vbCrLf _
                & " EMP_OT_RATE=1, CONTRACTOR_CODE=" & mContractCode & ", " & vbCrLf _
                & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " EMP_RATE_TYPE='" & VB.Left(cboPcRateType.Text, 1) & "', DIV_CODE=" & mDivisionCode & ", " & vbCrLf _
                & " WORKER_TYPE='W', OT_APP='Y'"

            SqlStr = SqlStr & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND EMP_CODE='" & mCode & "'"
        End If

        ''& " EMP_MAJOR_DEPT='" & mMajorDeptCode & "', " & vbCrLf

UpdatePart:
        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()
        'RsEmp.Requery()

        Update1 = True
        Exit Function
UpdateError:
        '    If err.Number = -2147467259 Then
        ''        Resume
        '        MsgBox "Can't Modify Transaction Exists Against this Code"
        '        PubDBCn.RollbackTrans
        '        Exit Function
        '    End If
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        Update1 = False
        PubDBCn.RollbackTrans()
        'RsEmp.Requery()
        PubDBCn.Errors.Clear()
        '   Resume
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Private Sub TxtEmpNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpNo.DoubleClick
        cmdsearch_Click(cmdSearch, New System.EventArgs())
    End Sub

    Private Sub txtName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtEmpNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtEmpNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            cmdsearch_Click(cmdSearch, New System.EventArgs())
        End If
    End Sub
    Private Function FieldsVarification() As Boolean

        On Error GoTo ERR1
        Dim xAmount As Decimal
        Dim mCostCenter As String
        Dim mDeptCode As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mHODCode As String
        Dim mEmpCategory As String

        Dim mRefNo As String
        Dim mMDApproval As String
        Dim mCFOApproval As String
        Dim mCEOApproval As String
        Dim mHRApproval As String
        Dim mApprovalCount As Integer
        Dim mESICeiling As Double

        FieldsVarification = True

        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Master or modify an existing Master")
            FieldsVarification = False
            Exit Function
        End If

        'If ADDMode = True Then
        '    If Trim(txtRefNo.Text) = "" Then
        '        MsgInformation("Please Select the Employee Requisition No.")
        '        FieldsVarification = False
        '        Exit Function
        '    End If

        '    mRefNo = VB6.Format(Val(txtRefNo.Text), "000000")

        '    SqlStr = ""
        '    SqlStr = "SELECT * FROM  PAY_CANDIDATE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND REF_NO=" & Val(Trim(txtRefNo.Text)) & "" & vbCrLf & " AND IS_JOINED='N'"

        '    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        '    mApprovalCount = 0
        '    If RsTemp.EOF = False Then
        '        mMDApproval = IIf(IsDbNull(RsTemp.Fields("MD_APPROVAL").Value), "N", RsTemp.Fields("MD_APPROVAL").Value)
        '        mCFOApproval = IIf(IsDbNull(RsTemp.Fields("CFO_APPROVAL").Value), "N", RsTemp.Fields("CFO_APPROVAL").Value)
        '        mCEOApproval = IIf(IsDbNull(RsTemp.Fields("CEO_APPROVAL").Value), "N", RsTemp.Fields("CEO_APPROVAL").Value)
        '        mHRApproval = IIf(IsDbNull(RsTemp.Fields("HR_APPROVAL").Value), "N", RsTemp.Fields("HR_APPROVAL").Value)
        '        mApprovalCount = IIf(mMDApproval = "Y", 1, 0)
        '        mApprovalCount = mApprovalCount + IIf(mCFOApproval = "Y", 1, 0)
        '        mApprovalCount = mApprovalCount + IIf(mCEOApproval = "Y", 1, 0)
        '        mApprovalCount = mApprovalCount + IIf(mHRApproval = "Y", 1, 0)
        '        '            If mApprovalCount < 3 Then
        '        '                MsgInformation "Atleast Three Approval Required for Update Master."
        '        '                FieldsVarification = False
        '        '                Exit Function
        '        '            End If
        '    Else
        '        MsgInformation("Invalid Employee Requisition No.")
        '        FieldsVarification = False
        '        Exit Function
        '    End If
        'End If

        If Trim(TxtName.Text) = "" Then
            MsgInformation("Name is empty. Cannot Save")
            TxtName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtEmpNo.Text) = "" Then
            MsgInformation("Card No is empty. Cannot Save")
            txtEmpNo.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtFName.Text) = "" Then
            MsgInformation("Father's Name is empty. Cannot Save")
            txtFName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        'If Trim(txtBloodGroup.Text) = "" Then
        '    MsgInformation("Blood Group is empty. Cannot Save")
        '    txtBloodGroup.Focus()
        '    FieldsVarification = False
        '    Exit Function
        'End If

        If Not IsDate(txtDOB.Text) Or Trim(txtDOB.Text) = "" Then
            MsgInformation("DOB cann't be blank.")
            txtDOB.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Not IsDate(txtDOJ.Text) Or Trim(txtDOJ.Text) = "" Then
            MsgInformation("Joining Date cann't be blank.")
            txtDOJ.Focus()
            FieldsVarification = False
            Exit Function
        End If


        If ADDMode = True Then
            If AgeYears(CDate(VB6.Format(txtDOB.Text, "DD/MM/YYYY")), CDate(VB6.Format(txtDOJ.Text, "DD/MM/YYYY"))) < 18 Then
                MsgInformation("Age Cann't be Less Than 18 at the time of Joining.")
                txtDOJ.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If IsDate(txtDOL.Text) Then
            If CDate(txtDOL.Text) < CDate(txtDOL.Text) Then
                MsgInformation("Leaving Date Cann't be less than Joining Date.")
                txtDOL.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        'If RsCompany.Fields("COMPANY_CODE").Value = 42 Then
        '    If Val(txtEmpNo.Text) < 190001 And Val(txtEmpNo.Text) < 200000 Then
        '        MsgInformation("Please Enter Emp Code more then 190000. Cannot Save")
        '        txtEmpNo.Focus()
        '        FieldsVarification = False
        '        Exit Function
        '    End If
        'ElseIf RsCompany.Fields("COMPANY_CODE").Value = 43 Then
        '    If Val(txtEmpNo.Text) < 200001 And Val(txtEmpNo.Text) < 210000 Then
        '        MsgInformation("Please Enter Emp Code more then 200000. Cannot Save")
        '        txtEmpNo.Focus()
        '        FieldsVarification = False
        '        Exit Function
        '    End If
        'Else
        '    If VB.Left(cboCatgeory.Text, 1) = "C" Then
        '        If Val(txtEmpNo.Text) < 100000 Then
        '            MsgInformation("Please Enter Emp Code more then 100000. Cannot Save")
        '            txtEmpNo.Focus()
        '            FieldsVarification = False
        '            Exit Function
        '        End If
        '        If Trim(txtContractor.Text) = "" Then
        '            MsgInformation("Contractor Name Cann't be Blank. Cannot Save")
        '            txtContractor.Focus()
        '            FieldsVarification = False
        '            Exit Function
        '        End If
        '        If MainClass.ValidateWithMasterTable(txtContractor.Text, "CON_NAME", "CON_CODE", "PAY_CONTRACTOR_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
        '            MsgInformation("Invalid Contractor Name. Cannot Save")
        '            txtContractor.Focus()
        '            FieldsVarification = False
        '            Exit Function
        '        End If
        '    Else
        '        If Val(txtEmpNo.Text) > 100000 Then
        '            MsgInformation("Please Enter Emp Code less then 100000. Cannot Save")
        '            txtEmpNo.Focus()
        '            FieldsVarification = False
        '            Exit Function
        '        End If
        '    End If
        'End If

        If cboSex.SelectedIndex = -1 Then
            MsgInformation("Please enter the Sex.")
            cboSex.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If cboMStatus.SelectedIndex = -1 Then
            MsgInformation("Please enter the Status.")
            cboMStatus.Focus()
            FieldsVarification = False
            Exit Function
        End If


        If Trim(cboDept.Text) = "" Then
            MsgInformation("Department Cann't be Blank")
            cboDept.Focus()
            FieldsVarification = False
            Exit Function
        End If


        If MainClass.ValidateWithMasterTable(cboDept.Text, "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDeptCode = MasterNo
        Else
            MsgInformation("Invalid Department Code. Cannot Save")
            cboDept.Focus()
            FieldsVarification = False
            Exit Function
        End If

        mCostCenter = Trim(txtCostCenter.Text)

        If mCostCenter = "" Then
            MsgInformation("Cost Center is empty. Cannot Save")
            txtCostCenter.Focus()
            FieldsVarification = False
            Exit Function
        Else
            SqlStr = " SELECT IH.CC_CODE, IH.CC_DESC, ID.DEPT_CODE " & vbCrLf & " FROM FIN_CCENTER_HDR IH, FIN_CCENTER_DET ID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=ID.COMPANY_CODE AND IH.CC_CODE=ID.CC_CODE" & vbCrLf & " AND IH.CC_DESC='" & MainClass.AllowSingleQuote(mCostCenter) & "'" & vbCrLf & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(mDeptCode) & "'"

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
            If RsTemp.EOF = True Then
                MsgInformation("Invalid Cost Center Code for Department : " & Trim(cboDept.Text))
                FieldsVarification = False
                txtCostCenter.Focus()
            End If
        End If

        '    If MainClass.ValidateWithMasterTable(mCostCenter, "CC_DESC", "CC_CODE", "FIN_CCENTER_HDR", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
        '        MsgInformation "Invalid Cost Center. Cannot Save"
        '        txtCostCenter.SetFocus
        '        FieldsVarification = False
        '        Exit Function
        '    End If

        '    If Trim(cboMajorDept.Text) = "" Then
        '        MsgInformation "Major Department Cann't be Blank"
        '        cboMajorDept.SetFocus
        '        FieldsVarification = False
        '        Exit Function
        '    End If

        If Not IsDate(txtDOJ.Text) Then
            MsgInformation("Joining Date cann't be blank.")
            txtDOJ.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If MODIFYMode = True Then
            If CDate(RsEmp.Fields("EMP_DOJ").Value) <> CDate(txtDOJ.Text) Then
                If CheckSalaryMade((txtEmpNo.Text), "") = True Then
                    MsgInformation("Salary Made. So Cann't be Modified")
                    FieldsVarification = False
                    Exit Function
                End If
            End If
        End If


        If Trim(cboDivision.Text) = "" Then
            MsgBox("Division Name is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If cboDivision.Enabled = True Then cboDivision.Focus()
            Exit Function
        End If

        'If ADDMode = True And lblEmpType.Text = "S" Then
        '    If CheckVacantPost(Trim(mDeptCode), VB.Left(cboCorporate.Text, 1), VB6.Format(txtDOJ.Text, "DD/MM/YYYY")) = False Then
        '        MsgInformation("You have not Sanction for this Dept.")
        '        FieldsVarification = False
        '        Exit Function
        '    End If
        'End If

Label1:
        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
        End If
        If MODIFYMode = True And (RsEmp.EOF = True) Then Exit Function
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        FieldsVarification = False
        ''Resume
    End Function
    Private Sub settextlength()

        On Error GoTo ERR1

        txtEmpNo.MaxLength = RsEmp.Fields("EMP_CODE").DefinedSize
        TxtName.MaxLength = RsEmp.Fields("EMP_NAME").DefinedSize

        txtFName.MaxLength = RsEmp.Fields("EMP_FNAME").DefinedSize
        'txtBloodGroup.MaxLength = RsEmp.Fields("BLOOD_GROUP").DefinedSize
        txtDOB.MaxLength = 10

        txtQualification.MaxLength = RsEmp.Fields("EMP_QUALIFICATION").DefinedSize



        txtDOJ.MaxLength = 10

        txtDOL.MaxLength = 10

        txtWorkingFrom.MaxLength = RsEmp.Fields("WORKINGTIMEFROM").DefinedSize
        txtWorkingTo.MaxLength = RsEmp.Fields("WORKINGTIMETO").DefinedSize

        'txtWorkingHours.MaxLength = RsEmp.Fields("WORKING_HOURS").Precision


        txtCostCenter.MaxLength = MainClass.SetMaxLength("CC_DESC", "FIN_CCENTER_HDR", PubDBCn)

        txtContractor.MaxLength = MainClass.SetMaxLength("CON_NAME", "PAY_CONTRACTOR_MST", PubDBCn)

        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Sub AssignGrid(ByRef mRefresh As Boolean)

        Dim SqlStr As String = ""
        SqlStr = " SELECT EMP_CODE, EMP_NAME, " & vbCrLf _
            & " EMP_FNAME,  EMP_DEPT_CODE, EMP_DOJ, EMP_LEAVE_DATE " & vbCrLf _
            & " FROM PAY_CONT_EMPLOYEE_MST "


        SqlStr = SqlStr & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        SqlStr = SqlStr & vbCrLf & " ORDER BY EMP_NAME "


        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()


    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 12)
            .set_ColWidth(0, 5)
            .set_ColWidth(1, 8)
            .set_ColWidth(2, 16)
            .set_ColWidth(3, 12)
            .set_ColWidth(4, 12)
            .set_ColWidth(5, 12)
            .set_ColWidth(6, 12)
            .set_ColWidth(7, 12)
            .set_ColWidth(8, 12)
            .set_ColWidth(9, 12)
            .set_ColWidth(10, 12)
            .set_ColWidth(11, 12)
            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr
        Dim mEmpCode As String
        SqlStr = ""
        '    MainClass.ValidateWithMasterTable txtName.Text, "EMP_NAME", "EMP_CODE", "PAY_CONT_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        mEmpCode = Trim(txtEmpNo.Text)


        If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_CODE", "PAY_SAL_TRN", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            MsgBox("Salary Exists Against This Employee.")
            Delete1 = False
            Exit Function
        End If
        '    If MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_CODE", "PAY_SALARYDEF_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        MsgBox "Salary Exists Against This Employee."
        '        Delete1 = False
        '        Exit Function
        ''    ElseIf MainClass.ValidateWithMasterTable(mEmpCode, "EmpCode", "EmpCode", "SalTrn", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        ''        MsgBox "Salary Exists Against This Employee."
        ''        Delete1 = False
        ''        Exit Function
        '    ElseIf MainClass.ValidateWithMasterTable(mEmpCode, "EMP_CODE", "EMP_CODE", "PAY_OPLeave_TRN", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        '        MsgBox "Opening Leaves Exists Against This Employee."
        '        Delete1 = False
        '        Exit Function
        '    End If

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If InsertIntoDeleteTrn(PubDBCn, "PAY_CONT_EMPLOYEE_MST", "EMP_CODE", xCode) = False Then GoTo DeleteErr

        SqlStr = "Delete from PAY_CONT_EMPLOYEE_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_CODE='" & xCode & "'"
        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()
        RsEmp.Requery()
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans()
        RsEmp.Requery()
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete Transaction Exists Against This Employee." & Err.Description)
            Exit Function
        End If
        MsgBox(Err.Description)
    End Function
    Public Sub TxtEmpNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmpNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim mEmpCode As String

        If Trim(txtEmpNo.Text) = "" Then GoTo EventExitSub
        mEmpCode = Trim(txtEmpNo.Text)

        txtEmpNo.Text = VB6.Format(mEmpCode, "000000")

        If MODIFYMode = True And RsEmp.EOF = False Then xCode = RsEmp.Fields("EMP_CODE").Value

        SqlStr = ""
        SqlStr = "SELECT * FROM  PAY_CONT_EMPLOYEE_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & MainClass.AllowSingleQuote(Trim(txtEmpNo.Text)) & "' "


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmp, ADODB.LockTypeEnum.adLockOptimistic)
        If RsEmp.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Name Does Not Exist In Master" & vbCrLf & "Click Add To Add In Master", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM  PAY_CONT_EMPLOYEE_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & xCode & "' "

                If lblEmpType.Text = "O" Then

                ElseIf lblEmpType.Text = "S" Then
                    SqlStr = SqlStr & " AND EMP_CAT_TYPE='1'"
                Else
                    SqlStr = SqlStr & " AND EMP_CAT_TYPE='2'"
                End If

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmp, ADODB.LockTypeEnum.adLockOptimistic)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
        '    Resume
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtWorkingHours_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWorkingHours.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtWorkingHours_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtWorkingHours.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtWorkingFrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWorkingFrom.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtWorkingFrom_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtWorkingFrom.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtWorkingTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWorkingTo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtWorkingTo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtWorkingTo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub SearchCCenter()
        On Error GoTo ErrPart
        Dim mDeptCode As String

        If Trim(cboDept.Text) = "" Then
            MsgInformation("Please Select Dept. First.")
            cboDept.Focus()
            Exit Sub
        End If

        If MainClass.ValidateWithMasterTable((cboDept.Text), "DEPT_DESC", "DEPT_CODE", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mDeptCode = MasterNo
        Else
            MsgInformation("Invalid Department Code. Cannot Save")
            cboDept.Focus()
            Exit Sub
        End If

        SqlStr = " SELECT IH.CC_DESC,IH.CC_CODE, ID.DEPT_CODE " & vbCrLf & " FROM FIN_CCENTER_HDR IH, FIN_CCENTER_DET ID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE=ID.COMPANY_CODE AND IH.CC_CODE=ID.CC_CODE" & vbCrLf & " AND ID.DEPT_CODE='" & MainClass.AllowSingleQuote(mDeptCode) & "'"


        '    If MainClass.SearchGridMaster(txtCostCenter.Text, "FIN_CCENTER_HDR", "CC_DESC", "CC_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
        If MainClass.SearchGridMasterBySQL2((txtCostCenter.Text), SqlStr) = True Then
            txtCostCenter.Text = AcName
            txtCostCenter_Validating(txtCostCenter, New System.ComponentModel.CancelEventArgs(False))
            If txtCostCenter.Enabled = True Then txtCostCenter.Focus()
        End If

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub Label63_Click(sender As Object, e As EventArgs) Handles Label63.Click

    End Sub
End Class
