Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmTestReport
    Inherits System.Windows.Forms.Form
    Dim RsTestReportMain As ADODB.Recordset
    Dim RsTestReportDetail As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

    Dim IsShowing As Boolean

    Dim mDocType As String

    Private Const ConRowHeight As Short = 14

    Private Const ColParameter As Short = 1
    Private Const ColSpecification As Short = 2
    Private Const ColObservation As Short = 3
    Private Const ColResult As Short = 4
    Private Const ColRemarks As Short = 5



    Private Sub cboDocType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboDocType.SelectedIndexChanged
        On Error GoTo cboERR
        If cboDocType.Text = "Painted/Powder Coated Parts" Then
            mDocType = "A"
            txtPreTreatMake.Enabled = True
            txtBatchNoPre.Enabled = True
            txtMake.Enabled = True
            txtBatchNoMake.Enabled = True
            txtPaintMake.Enabled = True
            txtBatchNoPaint.Enabled = True
        ElseIf cboDocType.Text = "Electro Plating" Then
            mDocType = "E"
            txtPreTreatMake.Text = ""
            txtBatchNoPre.Text = ""
            txtMake.Text = ""
            txtBatchNoMake.Text = ""
            txtPaintMake.Text = ""
            txtBatchNoPaint.Text = ""
            txtPreTreatMake.Enabled = False
            txtBatchNoPre.Enabled = False
            txtMake.Enabled = False
            txtBatchNoMake.Enabled = False
            txtPaintMake.Enabled = False
            txtBatchNoPaint.Enabled = False
        End If
        If txtPartNo.Enabled = True Then txtPartNo.Focus()
cboERR:
        If Err.Number = 5 Then Resume Next
    End Sub

    Private Sub cboDocType_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles cboDocType.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        txtPartNo.Focus()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtSlipNo.Enabled = False
            cmdSearchSlipNo.Enabled = False
            SprdMain.Enabled = True
        Else
            ADDMode = False
            MODIFYMode = False
            If RsTestReportMain.EOF = False Then RsTestReportMain.MoveFirst()
            Show1()
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
        'Resume
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        On Error Resume Next
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart

        If txtSlipNo.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If Not RsTestReportMain.EOF Then
            If RsTestReportMain.Fields("APP_EMP_CODE").Value <> "" Then MsgBox("Number been approved, So cann't be deleted") : Exit Sub
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "QAL_PART_TESTREPORT_HDR", (txtSlipNo.Text), RsTestReportMain) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM QAL_PART_TESTREPORT_DET WHERE AUTO_KEY_PARTTEST=" & Val(lblMkey.Text) & "")
                PubDBCn.Execute("DELETE FROM QAL_PART_TESTREPORT_HDR WHERE AUTO_KEY_PARTTEST=" & Val(lblMkey.Text) & "")
                PubDBCn.CommitTrans()
                RsTestReportMain.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
        RsTestReportMain.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr


        If CmdModify.Text = ConcmdmodifyCaption Then
            If RsTestReportMain.Fields("APP_EMP_CODE").Value <> "" Then MsgBox("Number been approved, So cann't be modified") : Exit Sub
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsTestReportMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            txtSlipNo.Enabled = False
            cmdSearchSlipNo.Enabled = False
            SprdMain.Enabled = True
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub
    Private Function CheckDuplicateParam_Specfic(ByRef pParameter As String, ByRef pSpecification As String) As Boolean

        On Error GoTo ERR1
        Dim I As Integer
        Dim mItemRept As Integer
        Dim xParameter As String
        Dim xSpecification As String

        If pParameter = "" Or pSpecification = "" Then CheckDuplicateParam_Specfic = False : Exit Function
        With SprdMain
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColParameter
                xParameter = UCase(Trim(.Text))

                .Col = ColSpecification
                xSpecification = UCase(Trim(.Text))

                If xParameter = UCase(Trim(pParameter)) And xSpecification = UCase(Trim(pSpecification)) Then
                    mItemRept = mItemRept + 1
                    If mItemRept > 1 Then
                        CheckDuplicateParam_Specfic = True
                        MsgInformation("Duplicate Entry.")
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, .ActiveCol)
                        Exit Function
                    End If
                End If
            Next
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification = False Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Update1 = True Then
            ADDMode = False
            MODIFYMode = False
            txtSlipNo_Validating(txtSlipNo, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True Then CmdAdd.Focus()
        Else
            MsgInformation("Record not saved")
            GoTo ErrorHandler
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Function Update1() As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mSlipNo As Double

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()


        SqlStr = ""
        mSlipNo = Val(txtSlipNo.Text)
        If Val(txtSlipNo.Text) = 0 Then
            mSlipNo = AutoGenKeyNo()
        End If
        txtSlipNo.Text = CStr(mSlipNo)
        If ADDMode = True Then
            lblMkey.Text = CStr(mSlipNo)
            SqlStr = " INSERT INTO QAL_PART_TESTREPORT_HDR " & vbCrLf _
                            & " (AUTO_KEY_PARTTEST,COMPANY_CODE," & vbCrLf _
                            & " DOC_DATE,DOC_TYPE,ITEM_CODE,SUPP_CUST_CODE,PROD_DATE," & vbCrLf _
                            & " LOT_QTY,SAMPLE_SIZE,AUTO_KEY_STD,TREATMENT_MAKE, " & vbCrLf _
                            & " TREATMENT_BATCH,POLYSTER_MAKE,POLYSTER_BATCH,PAINT_POWDER_MAKE, " & vbCrLf _
                            & " PAINT_POWDER_BATCH,CHECK_EMP_CODE,APP_EMP_CODE,REMARKS, " & vbCrLf _
                            & " ADDUSER, ADDDATE, MODUSER, MODDATE) " & vbCrLf _
                            & " VALUES ( " & vbCrLf _
                            & " " & mSlipNo & "," & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & mDocType & "','" & MainClass.AllowSingleQuote(txtPartNo.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtCustomer.Text) & "',TO_DATE('" & vb6.Format(txtDateOfProd.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " " & Val(txtQuantity.Text) & ",'" & MainClass.AllowSingleQuote(txtSampleSize.Text) & "'," & Val(txtInspectionSTD.Text) & ", " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtPreTreatMake.Text) & "','" & MainClass.AllowSingleQuote(txtBatchNoPre.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtMake.Text) & "','" & MainClass.AllowSingleQuote(txtBatchNoMake.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtPaintMake.Text) & "','" & MainClass.AllowSingleQuote(txtBatchNoPaint.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtCheckedBy.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtApprovedBy.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"
        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE QAL_PART_TESTREPORT_HDR SET " & vbCrLf _
                    & " AUTO_KEY_PARTTEST=" & mSlipNo & ",COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "," & vbCrLf _
                    & " DOC_DATE=TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),DOC_TYPE='" & mDocType & "', " & vbCrLf _
                    & " ITEM_CODE='" & MainClass.AllowSingleQuote(txtPartNo.Text) & "',SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCustomer.Text) & "', " & vbCrLf _
                    & " PROD_DATE=TO_DATE('" & vb6.Format(txtDateOfProd.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " LOT_QTY=" & Val(txtQuantity.Text) & ",SAMPLE_SIZE='" & MainClass.AllowSingleQuote(txtSampleSize.Text) & "', " & vbCrLf _
                    & " AUTO_KEY_STD=" & Val(txtInspectionSTD.Text) & ",TREATMENT_MAKE='" & MainClass.AllowSingleQuote(txtPreTreatMake.Text) & "', " & vbCrLf _
                    & " TREATMENT_BATCH='" & MainClass.AllowSingleQuote(txtBatchNoPre.Text) & "',POLYSTER_MAKE='" & MainClass.AllowSingleQuote(txtMake.Text) & "', " & vbCrLf _
                    & " POLYSTER_BATCH='" & MainClass.AllowSingleQuote(txtBatchNoMake.Text) & "',PAINT_POWDER_MAKE='" & MainClass.AllowSingleQuote(txtPaintMake.Text) & "', " & vbCrLf _
                    & " PAINT_POWDER_BATCH='" & MainClass.AllowSingleQuote(txtBatchNoPaint.Text) & "',CHECK_EMP_CODE='" & MainClass.AllowSingleQuote(txtCheckedBy.Text) & "', " & vbCrLf _
                    & " APP_EMP_CODE='" & MainClass.AllowSingleQuote(txtApprovedBy.Text) & "',REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                    & " MODDATE=TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                    & " AND AUTO_KEY_PARTTEST =" & Val(lblMkey.text) & ""
        End If

        PubDBCn.Execute(SqlStr)
        If UpdateDetail = False Then GoTo ErrPart
        Update1 = True
        PubDBCn.CommitTrans()
        txtSlipNo.Text = CStr(mSlipNo)
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsTestReportMain.Requery()
        RsTestReportDetail.Requery()
        MsgBox(Err.Description)
        '    Resume
    End Function
    Private Function AutoGenKeyNo() As Double

        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset
        Dim mAutoGen As Double
        Dim SqlStr As String
        Dim mMaxValue As String

        mAutoGen = 1
        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_PARTTEST)  " & vbCrLf & " FROM QAL_PART_TESTREPORT_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_PARTTEST,LENGTH(AUTO_KEY_PARTTEST)-5,4)=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND DOC_TYPE='" & mDocType & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAutoGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsAutoGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mMaxValue = .Fields(0).Value
                    mAutoGen = CDbl(Mid(mMaxValue, 1, Len(mMaxValue) - 6))
                    mAutoGen = mAutoGen + 1
                Else
                    mAutoGen = 1
                End If
            End If
        End With
        AutoGenKeyNo = CDbl(mAutoGen & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"))
        RsAutoGen.Close()
        RsAutoGen = Nothing
        Exit Function
AutogenErr:
        MsgBox(Err.Description)
    End Function

    Private Function UpdateDetail() As Boolean

        On Error GoTo UpdateDetailERR
        Dim SqlStr As String
        Dim I As Integer
        Dim mParameter As String
        Dim mSpecification As String
        Dim mObservation As String
        Dim mResult As String
        Dim mRemarks As String

        PubDBCn.Execute("DELETE FROM QAL_PART_TESTREPORT_DET WHERE AUTO_KEY_PARTTEST=" & Val(lblMkey.Text) & "")

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColParameter
                mParameter = MainClass.AllowSingleQuote(.Text)

                .Col = ColSpecification
                mSpecification = MainClass.AllowSingleQuote(.Text)

                .Col = ColObservation
                mObservation = MainClass.AllowSingleQuote(.Text)

                .Col = ColResult
                mResult = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                .Col = ColRemarks
                mRemarks = MainClass.AllowSingleQuote(.Text)

                SqlStr = ""

                If mParameter <> "" And mSpecification <> "" Then
                    SqlStr = " INSERT INTO  QAL_PART_TESTREPORT_DET ( " & vbCrLf & " DOC_TYPE,AUTO_KEY_PARTTEST,SERIAL_NO,PARAMETER,SPEC_DESC,OBSERVATION, " & vbCrLf & " ON_NG_FLAG,REMARKS ) " & vbCrLf & " VALUES ( " & vbCrLf & " '" & mDocType & "'," & Val(lblMkey.Text) & "," & I & ",'" & mParameter & "','" & mSpecification & "', " & vbCrLf & " '" & mObservation & "','" & mResult & "','" & mRemarks & "') "
                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        UpdateDetail = True
        Exit Function
UpdateDetailERR:
        UpdateDetail = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Function


    Private Sub cmdSearchChecked_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchChecked.Click
        Call SearchEmp(txtCheckedBy, lblCheckedBy)
    End Sub


    Private Sub cmdSearchApproved_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchApproved.Click
        Call SearchEmp(txtApprovedBy, lblApprovedBy)
    End Sub


    Private Sub SearchEmp(ByRef pTextBax As System.Windows.Forms.TextBox, ByRef pLable As System.Windows.Forms.Label)
        On Error GoTo SrchERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND EMP_LEAVE_DATE IS NULL "
        If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            pTextBax.Text = AcName1
            pLable.text = AcName
            If pTextBax.Enabled = True Then pTextBax.Focus()
        End If
        Exit Sub
SrchERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdSearchCustomer_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchCustomer.Click


        Dim SqlStr As String
        SqlStr = "SELECT A.SUPP_CUST_CODE, B.SUPP_CUST_NAME " & vbCrLf & " FROM FIN_SUPP_CUST_DET A, FIN_SUPP_CUST_MST B " & vbCrLf & " Where b.COMPANY_CODE = a.COMPANY_CODE" & vbCrLf & " AND LTRIM(RTRIM(B.SUPP_CUST_CODE)) = LTRIM(RTRIM(A.SUPP_CUST_CODE))" & vbCrLf & " AND A.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND LTRIM(RTRIM(A.ITEM_CODE)) ='" & LTrim(RTrim(MainClass.AllowSingleQuote(txtPartNo.Text))) & "' " & vbCrLf & " ORDER BY B.SUPP_CUST_NAME "
        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtCustomer.Text = AcName
            lblCustomer.Text = AcName1
            If txtCustomer.Enabled = True Then txtCustomer.Focus()
        End If

    End Sub

    Private Sub cmdSearchPartNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchPartNo.Click
        Dim SqlStr As String
        SqlStr = "SELECT A.ITEM_CODE, B.ITEM_SHORT_DESC " & vbCrLf & " FROM QAL_INSPECTION_STD_HDR A, INV_ITEM_MST B " & vbCrLf & " Where b.COMPANY_CODE = a.COMPANY_CODE " & vbCrLf & " AND LTRIM(RTRIM(B.ITEM_CODE)) = LTRIM(RTRIM(A.ITEM_CODE)) " & vbCrLf & " AND A.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND A.INSP_TYPE ='" & mDocType & "' " & vbCrLf & " ORDER BY B.ITEM_SHORT_DESC "
        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtPartNo.Text = AcName
            lblPartNo.text = AcName1
            If txtPartNo.Enabled = True Then txtPartNo.Focus()
        End If
    End Sub

    Private Sub cmdSearchSlipNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchSlipNo.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUBSTR(AUTO_KEY_PARTTEST,LENGTH(AUTO_KEY_PARTTEST)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
        If MainClass.SearchGridMaster(txtSlipNo.Text, "QAL_PART_TESTREPORT_HDR", "AUTO_KEY_PARTTEST", "DOC_TYPE", "DOC_DATE", "ITEM_CODE", SqlStr) = True Then
            txtSlipNo.Text = AcName
            'cboDocType.ListIndex = IIf(AcName1 = "A", 0, 1)
            Call txtSlipNo_Validating(txtSlipNo, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
        End If
    End Sub

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Private Sub ViewGrid()

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        ADataGrid.Refresh
            FormatSprdView()
            SprdView.Refresh()
            SprdView.Focus()
            SprdView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            SprdView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsTestReportMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmTestReport_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Test Report"

        SqlStr = "Select * From QAL_PART_TESTREPORT_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTestReportMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From QAL_PART_TESTREPORT_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTestReportDetail, ADODB.LockTypeEnum.adLockReadOnly)

        Call AssignGrid(False)
        Call SetTextLengths()
        Call Clear1()
        If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        Resume
    End Sub

    Private Sub AssignGrid(ByRef mRefresh As Boolean)

        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = ""

        SqlStr = " SELECT " & vbCrLf & " AUTO_KEY_PARTTEST AS SLIP_NUMBER,TO_CHAR(DOC_DATE,'DD/MM/YYYY') AS DOC_DATE, " & vbCrLf & " DOC_TYPE,ITEM_CODE,SUPP_CUST_CODE,PROD_DATE,LOT_QTY " & vbCrLf & " FROM QAL_PART_TESTREPORT_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_PARTTEST,LENGTH(AUTO_KEY_PARTTEST)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " ORDER BY AUTO_KEY_PARTTEST"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmTestReport_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Left = 0
        Me.Top = 0
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)
        cboDocType.Items.Add("Painted/Powder Coated Parts")
        cboDocType.Items.Add("Electro Plating")
        cboDocType.SelectedIndex = 0
        cboDocType_SelectedIndexChanged(cboDocType, New System.EventArgs())
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

        lblMkey.Text = ""
        txtSlipNo.Text = ""
        txtDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        cboDocType.SelectedIndex = 0
        txtPartNo.Text = ""
        lblPartNo.Text = ""
        txtCustomer.Text = ""
        lblCustomer.Text = ""
        txtDateOfProd.Text = ""
        txtQuantity.Text = ""
        txtSampleSize.Text = ""
        txtInspectionStd.Text = ""
        txtPreTreatMake.Text = ""
        txtBatchNoPre.Text = ""
        txtMake.Text = ""
        txtBatchNoMake.Text = ""
        txtPaintMake.Text = ""
        txtBatchNoPaint.Text = ""
        txtCheckedBy.Text = ""
        lblCheckedBy.Text = ""
        txtApprovedBy.Text = ""
        lblApprovedBy.Text = ""
        txtRemarks.Text = ""
        Call MakeEnableDesableField(True)
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsTestReportMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub
    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim SqlStr As String


        With SprdMain
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = ColParameter
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsTestReportDetail.Fields("PARAMETER").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColSpecification
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsTestReportDetail.Fields("SPEC_DESC").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColObservation
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsTestReportDetail.Fields("OBSERVATION").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColResult
            .CellType = SS_CELL_TYPE_CHECKBOX


            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsTestReportDetail.Fields("REMARKS").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColParameter, ColSpecification)
            MainClass.SetSpreadColor(SprdMain, Arow)
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume
    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 600)
            .set_ColWidth(0, 500 * 1)
            .set_ColWidth(1, 500 * 4)
            .set_ColWidth(2, 500 * 3)
            .set_ColWidth(3, 500 * 3)
            .set_ColWidth(4, 500 * 3)
            .set_ColWidth(5, 500 * 3)
            .set_ColWidth(6, 500 * 3)
            .set_ColWidth(7, 500 * 3)
            .set_ColWidth(8, 500 * 3)
            .set_ColWidth(9, 500 * 3)
            .set_ColWidth(10, 500 * 3)
            .set_ColWidth(11, 500 * 3)
            .set_ColWidth(12, 500 * 3)
            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1

        txtSlipNo.Maxlength = RsTestReportMain.Fields("AUTO_KEY_PARTTEST").Precision
        txtDate.Maxlength = RsTestReportMain.Fields("DOC_DATE").DefinedSize - 6
        txtPartNo.Maxlength = RsTestReportMain.Fields("ITEM_CODE").DefinedSize
        txtCustomer.Maxlength = RsTestReportMain.Fields("SUPP_CUST_CODE").DefinedSize
        txtDateOfProd.Maxlength = RsTestReportMain.Fields("PROD_DATE").DefinedSize - 6
        txtQuantity.Maxlength = RsTestReportMain.Fields("LOT_QTY").Precision
        txtSampleSize.Maxlength = RsTestReportMain.Fields("SAMPLE_SIZE").DefinedSize
        txtInspectionStd.Maxlength = RsTestReportMain.Fields("AUTO_KEY_STD").Precision
        txtPreTreatMake.Maxlength = RsTestReportMain.Fields("TREATMENT_MAKE").DefinedSize
        txtBatchNoPre.Maxlength = RsTestReportMain.Fields("TREATMENT_BATCH").DefinedSize
        txtMake.Maxlength = RsTestReportMain.Fields("POLYSTER_MAKE").DefinedSize
        txtBatchNoMake.Maxlength = RsTestReportMain.Fields("POLYSTER_BATCH").DefinedSize
        txtPaintMake.Maxlength = RsTestReportMain.Fields("PAINT_POWDER_MAKE").DefinedSize
        txtBatchNoPaint.Maxlength = RsTestReportMain.Fields("PAINT_POWDER_BATCH").DefinedSize
        txtCheckedBy.Maxlength = RsTestReportMain.Fields("CHECK_EMP_CODE").DefinedSize
        txtApprovedBy.Maxlength = RsTestReportMain.Fields("APP_EMP_CODE").DefinedSize
        txtRemarks.Maxlength = RsTestReportMain.Fields("REMARKS").DefinedSize
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub

    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        Dim ii As Integer
        FieldsVarification = True

        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsTestReportMain.EOF = True Then Exit Function
        If Trim(cboDocType.Text) = "" Then
            MsgInformation("Document Type is empty, So unable to save.")
            cboDocType.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtDate.Text) = "" Then
            MsgInformation("Date is empty, So unable to save.")
            txtDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtPartNo.Text) = "" Then
            MsgInformation("Part No. empty, So unable to save.")
            txtPartNo.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtCustomer.Text) = "" Then
            MsgInformation("Customer Code empty, So unable to save.")
            txtCustomer.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtDateOfProd.Text) = "" Then
            MsgInformation("Date Of Production is empty, So unable to save.")
            txtDateOfProd.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Val(txtQuantity.Text) = 0 Then
            MsgInformation("Quantity (LOT) is empty, So unable to save.")
            txtQuantity.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtSampleSize.Text) = "" Then
            MsgInformation("Sample Size is empty, So unable to save.")
            txtSampleSize.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtCheckedBy.Text) = "" Then
            MsgInformation("Checked Employee Code is empty, So unable to save.")
            txtCheckedBy.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If mDocType = "A" Then '--painted powder coating
            If Trim(txtPreTreatMake.Text) = "" Then
                MsgInformation("Treatment Make is empty, So unable to save.")
                txtPreTreatMake.Focus()
                FieldsVarification = False
                Exit Function
            End If
            If Trim(txtBatchNoPre.Text) = "" Then
                MsgInformation("Treatment Batch is empty, So unable to save.")
                txtBatchNoPre.Focus()
                FieldsVarification = False
                Exit Function
            End If
            If Trim(txtMake.Text) = "" Then
                MsgInformation("Polyster Make is empty, So unable to save.")
                txtMake.Focus()
                FieldsVarification = False
                Exit Function
            End If
            If Trim(txtBatchNoMake.Text) = "" Then
                MsgInformation("Polyster Batch is empty, So unable to save.")
                txtBatchNoMake.Focus()
                FieldsVarification = False
                Exit Function
            End If
            If Trim(txtPaintMake.Text) = "" Then
                MsgInformation("Paint/Powder Make is empty, So unable to save.")
                txtPaintMake.Focus()
                FieldsVarification = False
                Exit Function
            End If
            If Trim(txtBatchNoPaint.Text) = "" Then
                MsgInformation("Paint/Powder Batch is empty, So unable to save.")
                txtBatchNoPaint.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If


        If MainClass.ValidDataInGrid(SprdMain, ColParameter, "S", "Please Check Parameter Description.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColSpecification, "S", "Please Check Specification Details.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColObservation, "S", "Please Check Observation.") = False Then FieldsVarification = False : Exit Function

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        Resume
    End Function

    Private Sub frmTestReport_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        RsTestReportMain.Close()
        RsTestReportMain = Nothing
        RsTestReportDetail.Close()
        RsTestReportDetail = Nothing
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim SqlStr As String
        If eventArgs.Col = 0 And eventArgs.Row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdMain, eventArgs.Row, ColParameter)
            MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
        End If
    End Sub
    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell
        On Error GoTo ErrPart
        Dim xParameter As String
        Dim xSpecification As String

        If eventArgs.NewRow = -1 Then Exit Sub


        SprdMain.Row = SprdMain.ActiveRow
        SprdMain.Col = ColParameter
        xParameter = Trim(SprdMain.Text)
        If xParameter = "" Then Exit Sub

        Select Case eventArgs.col
            Case ColParameter 'GRID IS FILLING ON THE BASIS OF txtInspectionSTD.TEXT, SO MARKED
                '            SprdMain.Row = SprdMain.ActiveRow
                '
                '            SprdMain.Col = ColParameter
                '            xParameter = Trim(SprdMain.Text)
                '            If xParameter = "" Then Exit Sub
                '
                '            SprdMain.Col = ColSpecification
                '            xSpecification = Trim(SprdMain.Text)
                '
                '            If CheckDuplicateParam_Specfic(xParameter, xSpecification) = False Then
                '                MainClass.AddBlankSprdRow SprdMain, ColParameter, ConRowHeight
                '                FormatSprdMain SprdMain.MaxRows
                '            End If
            Case ColSpecification 'GRID IS FILLING ON THE BASIS OF txtInspectionSTD.TEXT, SO MARKED
                '            SprdMain.Row = SprdMain.ActiveRow
                '
                '            SprdMain.Col = ColParameter
                '            xParameter = Trim(SprdMain.Text)
                '            If xParameter = "" Then Exit Sub
                '
                '            SprdMain.Col = ColSpecification
                '            xSpecification = Trim(SprdMain.Text)
                '
                '            Call CheckDuplicateParam_Specfic(xParameter, xSpecification)
        End Select
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        With SprdMain
            SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub
    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Row = SprdView.ActiveRow

        SprdView.Col = 1
        txtSlipNo.Text = SprdView.Text

        SprdView.Col = 3
        cboDocType.SelectedIndex = IIf(SprdView.Text = "A", 0, 1)

        txtSlipNo_Validating(txtSlipNo, New System.ComponentModel.CancelEventArgs(False))
        Call CmdView_Click(CmdView, New System.EventArgs())
    End Sub
    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Private Function ValidateEMP(ByRef pTextBox As System.Windows.Forms.TextBox, ByRef pLable As System.Windows.Forms.Label) As Boolean
        On Error GoTo ValEMP
        Dim SqlStr As String
        ValidateEMP = True
        If Trim(pTextBox.Text) = "" Then Exit Function
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND EMP_LEAVE_DATE IS NULL "
        If MainClass.ValidateWithMasterTable(pTextBox.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Employee Does Not Exist In Master.")
            ValidateEMP = False
        Else
            pLable.text = MasterNo
        End If
        Exit Function
ValEMP:
        MsgBox(Err.Description)
    End Function


    Private Sub txtCustomer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustomer.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCustomer_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustomer.DoubleClick
        Call CmdSearchCustomer_Click(CmdSearchCustomer, New System.EventArgs())
    End Sub

    Private Sub txtCustomer_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCustomer.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call CmdSearchCustomer_Click(CmdSearchCustomer, New System.EventArgs())
    End Sub

    Private Sub txtCustomer_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCustomer.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim SqlStr As String
        Dim mRsTemp As ADODB.Recordset
        If Trim(txtCustomer.Text) = "" Then GoTo EventExitSub
        SqlStr = " SELECT A.SUPP_CUST_NAME " & vbCrLf _
                    & " FROM FIN_SUPP_CUST_MST A, FIN_SUPP_CUST_DET B " & vbCrLf _
                    & " Where a.COMPANY_CODE = b.COMPANY_CODE " & vbCrLf _
                    & " And LTrim(RTrim(a.SUPP_CUST_CODE)) = LTrim(RTrim(b.SUPP_CUST_CODE)) " & vbCrLf _
                    & " AND B.COMPANY_CODE = " & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                    & " AND LTRIM(RTRIM(B.ITEM_CODE)) ='" & MainClass.AllowSingleQuote(LTrim(RTrim(txtPartNo.Text))) & "' " & vbCrLf _
                    & " AND LTRIM(RTRIM(B.SUPP_CUST_CODE)) = '" & MainClass.AllowSingleQuote(LTrim(RTrim(txtCustomer.Text))) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With mRsTemp
            If Not .EOF Then
                lblCustomer.Text = IIf(IsDbNull(mRsTemp.Fields("SUPP_CUST_NAME").Value), "", .Fields("SUPP_CUST_NAME").Value)
            Else
                MsgBox("Not a valid Customer.")
                lblCustomer.Text = "'"
                Cancel = True
            End If
        End With
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtInspectionSTD_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtInspectionSTD.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo FillERR
        Dim SqlStr As String
        Dim I As Integer
        Dim mRsTemp As ADODB.Recordset
        If Trim(txtInspectionStd.Text) = "" Then GoTo EventExitSub
        SqlStr = "SELECT SERIAL_NO,PARAM_DESC, SPECIFICATION " & vbCrLf & " From QAL_INSPECTION_STD_DET " & vbCrLf & " WHERE DETAIL_TYPE NOT IN ('A','E') " & vbCrLf & " AND AUTO_KEY_STD =" & Val(txtInspectionStd.Text) & " ORDER BY SERIAL_NO"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With mRsTemp
            If .EOF = True Then GoTo EventExitSub
            FormatSprdMain(-1)
            I = 1
            Do While Not .EOF
                SprdMain.Row = I

                SprdMain.Col = ColParameter
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("PARAM_DESC").Value), "", .Fields("PARAM_DESC").Value))

                SprdMain.Col = ColSpecification
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("SPECIFICATION").Value), "", .Fields("SPECIFICATION").Value))

                .MoveNext()
                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With
        GoTo EventExitSub
FillERR:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPartNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPartNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPartNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPartNo.DoubleClick
        Call cmdSearchPartNo_Click(cmdSearchPartNo, New System.EventArgs())
    End Sub

    Private Sub txtPartNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtPartNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchPartNo_Click(cmdSearchPartNo, New System.EventArgs())
    End Sub

    Private Sub txtPartNo_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPartNo.Leave
        If Trim(txtPartNo.Text) = "" Then Exit Sub
        If txtCustomer.Enabled = True Then txtCustomer.Focus()
    End Sub

    Private Sub txtPartNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPartNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim SqlStr As String
        Dim mRsTemp As ADODB.Recordset
        If Trim(txtPartNo.Text) = "" Then GoTo EventExitSub
        SqlStr = "SELECT B.ITEM_SHORT_DESC, A.AUTO_KEY_STD " & vbCrLf _
                    & " FROM QAL_INSPECTION_STD_HDR A, INV_ITEM_MST B " & vbCrLf _
                    & " Where b.COMPANY_CODE = a.COMPANY_CODE " & vbCrLf _
                    & " AND LTRIM(RTRIM(B.ITEM_CODE)) = LTRIM(RTRIM(A.ITEM_CODE)) " & vbCrLf _
                    & " AND A.COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                    & " AND A.INSP_TYPE ='" & mDocType & "' " & vbCrLf _
                    & " AND LTRIM(RTRIM(A.ITEM_CODE)) = '" & MainClass.AllowSingleQuote(txtPartNo.Text) & "'"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With mRsTemp
            If Not .EOF Then
                lblPartNo.Text = IIf(IsDbNull(mRsTemp.Fields("Item_Short_Desc").Value), "", .Fields("Item_Short_Desc").Value)
                txtInspectionStd.Text = IIf(IsDbNull(mRsTemp.Fields("AUTO_KEY_STD").Value), "", .Fields("AUTO_KEY_STD").Value)
                txtInspectionSTD_Validating(txtInspectionSTD, New System.ComponentModel.CancelEventArgs(False))
            Else
                MsgBox("Not a valid Part No.")
                lblPartNo.Text = ""
                txtInspectionStd.Text = ""
                Cancel = True
            End If
        End With
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtQuantity_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtQuantity.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtQuantity_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtQuantity.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDateOfProd_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateOfProd.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDateOfProd_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateOfProd.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDateOfProd.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtDateOfProd.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtMake_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMake.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtPaintMake_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPaintMake.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCheckedBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCheckedBy.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCheckedBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCheckedBy.DoubleClick
        Call cmdSearchChecked_Click(cmdSearchChecked, New System.EventArgs())
    End Sub

    Private Sub txtCheckedBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCheckedBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchChecked_Click(cmdSearchChecked, New System.EventArgs())
    End Sub

    Private Sub txtCheckedBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCheckedBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If ValidateEMP(txtCheckedBy, lblCheckedBy) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtBatchNoMake_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBatchNoMake.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBatchNoPre_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBatchNoPre.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSampleSize_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSampleSize.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtInspectionSTD_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInspectionSTD.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPreTreatMake_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPreTreatMake.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtApprovedBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtApprovedBy.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtApprovedBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtApprovedBy.DoubleClick
        Call cmdSearchApproved_Click(cmdSearchApproved, New System.EventArgs())
    End Sub

    Private Sub txtApprovedBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtApprovedBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchApproved_Click(cmdSearchApproved, New System.EventArgs())
    End Sub

    Private Sub txtApprovedBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtApprovedBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If ValidateEMP(txtApprovedBy, lblApprovedBy) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSlipNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSlipNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart

        If Not RsTestReportMain.EOF Then
            IsShowing = True
            lblMkey.Text = IIf(IsDbNull(RsTestReportMain.Fields("AUTO_KEY_PARTTEST").Value), "", RsTestReportMain.Fields("AUTO_KEY_PARTTEST").Value)
            cboDocType.SelectedIndex = IIf(IsDbNull(RsTestReportMain.Fields("DOC_TYPE").Value) Or RsTestReportMain.Fields("DOC_TYPE").Value = "A", 0, 1)
            txtSlipNo.Text = IIf(IsDbNull(RsTestReportMain.Fields("AUTO_KEY_PARTTEST").Value), "", RsTestReportMain.Fields("AUTO_KEY_PARTTEST").Value)
            txtDate.Text = IIf(IsDbNull(RsTestReportMain.Fields("DOC_DATE").Value), "", RsTestReportMain.Fields("DOC_DATE").Value)

            txtPartNo.Text = IIf(IsDbNull(RsTestReportMain.Fields("ITEM_CODE").Value), "", RsTestReportMain.Fields("ITEM_CODE").Value)
            txtPartNo_Validating(txtPartNo, New System.ComponentModel.CancelEventArgs(False))
            txtCustomer.Text = IIf(IsDbNull(RsTestReportMain.Fields("SUPP_CUST_CODE").Value), "", RsTestReportMain.Fields("SUPP_CUST_CODE").Value)
            txtCustomer_Validating(txtCustomer, New System.ComponentModel.CancelEventArgs(False))
            txtDateOfProd.Text = IIf(IsDbNull(RsTestReportMain.Fields("PROD_DATE").Value), "", RsTestReportMain.Fields("PROD_DATE").Value)
            txtQuantity.Text = IIf(IsDbNull(RsTestReportMain.Fields("LOT_QTY").Value), "", RsTestReportMain.Fields("LOT_QTY").Value)
            txtSampleSize.Text = IIf(IsDbNull(RsTestReportMain.Fields("SAMPLE_SIZE").Value), "", RsTestReportMain.Fields("SAMPLE_SIZE").Value)
            txtInspectionStd.Text = IIf(IsDbNull(RsTestReportMain.Fields("AUTO_KEY_STD").Value), "", RsTestReportMain.Fields("AUTO_KEY_STD").Value)
            txtPreTreatMake.Text = IIf(IsDbNull(RsTestReportMain.Fields("TREATMENT_MAKE").Value), "", RsTestReportMain.Fields("TREATMENT_MAKE").Value)
            txtBatchNoPre.Text = IIf(IsDbNull(RsTestReportMain.Fields("TREATMENT_BATCH").Value), "", RsTestReportMain.Fields("TREATMENT_BATCH").Value)
            txtMake.Text = IIf(IsDbNull(RsTestReportMain.Fields("POLYSTER_MAKE").Value), "", RsTestReportMain.Fields("POLYSTER_MAKE").Value)
            txtBatchNoMake.Text = IIf(IsDbNull(RsTestReportMain.Fields("POLYSTER_BATCH").Value), "", RsTestReportMain.Fields("POLYSTER_BATCH").Value)
            txtPaintMake.Text = IIf(IsDbNull(RsTestReportMain.Fields("PAINT_POWDER_MAKE").Value), "", RsTestReportMain.Fields("PAINT_POWDER_MAKE").Value)
            txtBatchNoPaint.Text = IIf(IsDbNull(RsTestReportMain.Fields("PAINT_POWDER_BATCH").Value), "", RsTestReportMain.Fields("PAINT_POWDER_BATCH").Value)
            txtCheckedBy.Text = IIf(IsDbNull(RsTestReportMain.Fields("CHECK_EMP_CODE").Value), "", RsTestReportMain.Fields("CHECK_EMP_CODE").Value)
            txtCheckedBy_Validating(txtCheckedBy, New System.ComponentModel.CancelEventArgs(False))
            txtApprovedBy.Text = IIf(IsDbNull(RsTestReportMain.Fields("APP_EMP_CODE").Value), "", RsTestReportMain.Fields("APP_EMP_CODE").Value)
            txtApprovedBy_Validating(txtApprovedBy, New System.ComponentModel.CancelEventArgs(False))
            txtRemarks.Text = IIf(IsDbNull(RsTestReportMain.Fields("REMARKS").Value), "", RsTestReportMain.Fields("REMARKS").Value)
            Call ShowDetail1()
            Call MakeEnableDesableField(False)
            IsShowing = False
        End If
        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = True    '' False Sandeep 15/05/2022
        txtSlipNo.Enabled = True
        cmdSearchSlipNo.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsTestReportMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        Resume
    End Sub
    Private Sub ShowDetail1()

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM QAL_PART_TESTREPORT_DET " & vbCrLf & " WHERE AUTO_KEY_PARTTEST=" & Val(lblMkey.Text) & "" & vbCrLf & " ORDER BY SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTestReportDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsTestReportDetail
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            I = 1
            Do While Not .EOF
                SprdMain.Row = I

                SprdMain.Col = ColParameter
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("Parameter").Value), "", .Fields("Parameter").Value))

                SprdMain.Col = ColSpecification
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("SPEC_DESC").Value), "", .Fields("SPEC_DESC").Value))

                SprdMain.Col = ColObservation
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("OBSERVATION").Value), "", .Fields("OBSERVATION").Value))

                SprdMain.Col = ColResult
                SprdMain.Value = IIf(IsDbNull(.Fields("ON_NG_FLAG").Value) Or .Fields("ON_NG_FLAG").Value = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)

                SprdMain.Col = ColRemarks
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value))

                .MoveNext()
                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtSlipNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSlipNo.DoubleClick
        Call cmdSearchSlipNo_Click(cmdSearchSlipNo, New System.EventArgs())
    End Sub

    Private Sub txtSlipNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSlipNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchSlipNo_Click(cmdSearchSlipNo, New System.EventArgs())
    End Sub
    Private Sub txtSlipNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSlipNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMKey As Double
        Dim mSlipNo As Double
        Dim SqlStr As String


        If Trim(txtSlipNo.Text) = "" Then GoTo EventExitSub
        mSlipNo = Val(txtSlipNo.Text)

        If MODIFYMode = True And RsTestReportMain.BOF = False Then xMKey = RsTestReportMain.Fields("AUTO_KEY_PARTTEST").Value

        SqlStr = "SELECT * FROM QAL_PART_TESTREPORT_HDR " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_PARTTEST,LENGTH(AUTO_KEY_PARTTEST)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_PARTTEST=" & mSlipNo & " AND DOC_TYPE='" & mDocType & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTestReportMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTestReportMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Number. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM QAL_PART_TESTREPORT_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_PARTTEST,LENGTH(AUTO_KEY_PARTTEST)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_PARTTEST=" & Val(CStr(xMKey)) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTestReportMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub MakeEnableDesableField(ByRef mMode As Boolean)
        txtDate.Enabled = mMode
        cboDocType.Enabled = mMode
        txtPartNo.Enabled = mMode
        cmdSearchPartNo.Enabled = mMode
        txtCustomer.Enabled = mMode
        cmdSearchCustomer.Enabled = mMode
        txtInspectionStd.Enabled = False

    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String)
        On Error GoTo ErrPart
        Dim mAmountInword As String

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\" & mRptFileName
        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        Report1.Action = 1
        Report1.Reset()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
    Private Sub ReportOnTest(ByRef Mode As Crystal.DestinationConstants)

    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnTest(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnTest(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
End Class
