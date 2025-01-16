Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmInitailSample
    Inherits System.Windows.Forms.Form
    Dim RsInitSampleMain As ADODB.Recordset
    Dim RsInitSampleDetail As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim xMenuID As String

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

    Dim IsShowing As Boolean

    Private Const ConRowHeight As Short = 14

    Private Const ColParameter As Short = 1
    Private Const ColSpecification As Short = 2
    Private Const ColInspection As Short = 3
    Private Const ColActDim1 As Short = 4
    Private Const ColActDim2 As Short = 5
    Private Const ColActDim3 As Short = 6
    Private Const ColActDim4 As Short = 7
    Private Const ColActDim5 As Short = 8
    Private Const ColRemarks As Short = 9

    Private Sub chkInHouse_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkInHouse.CheckStateChanged
        txtDept.Enabled = IIf(chkInHouse.CheckState = System.Windows.Forms.CheckState.Checked, True, False)
        cmdSearchDept.Enabled = IIf(chkInHouse.CheckState = System.Windows.Forms.CheckState.Checked, True, False)
        txtSupplier.Enabled = IIf(chkInHouse.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        cmdsearchSupplier.Enabled = IIf(chkInHouse.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
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
            If RsInitSampleMain.EOF = False Then RsInitSampleMain.MoveFirst()
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
        If Not RsInitSampleMain.EOF Then
            If RsInitSampleMain.Fields("SIGN_EMP_CODE").Value <> "" Then MsgBox("Number been approved, So cann't be deleted") : Exit Sub
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "QAL_INS_INITSAMPART_HDR", (txtSlipNo.Text), RsInitSampleMain) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM QAL_INS_INITSAMPART_DET WHERE AUTO_KEY_INITSAMPART=" & Val(lblMkey.Text) & "")
                PubDBCn.Execute("DELETE FROM QAL_INS_INITSAMPART_HDR WHERE AUTO_KEY_INITSAMPART=" & Val(lblMkey.Text) & "")
                PubDBCn.CommitTrans()
                RsInitSampleMain.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        '    PubDBCn.Errors.Clear
        RsInitSampleMain.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdFile_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdFile.Click
        On Error GoTo ERR1
        Dim I As Short
        Dim mDirName As String

        If txtFile.Text = "" Then
            CommonDialog1Open.InitialDirectory = "C:\"
        Else
            txtFile.Text = Trim(txtFile.Text)
            I = InStrRev(txtFile.Text, "\")
            mDirName = Mid(txtFile.Text, 1, I)
            CommonDialog1Open.InitialDirectory = mDirName
        End If
        CommonDialog1Open.FileName = "*.TXT"
        CommonDialog1Open.ShowDialog()
        If UCase(CommonDialog1Open.FileName) <> "*.TXT" Then
            txtFile.Text = CommonDialog1Open.FileName
        End If
        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr


        If CmdModify.Text = ConcmdmodifyCaption Then
            If RsInitSampleMain.Fields("SIGN_EMP_CODE").Value <> "" Then MsgBox("Number been approved, So cann't be modified") : Exit Sub
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsInitSampleMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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

        Exit Function ''14-04-2010  'Bhupender 'Not Required.. Duplicate may be..

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

    Private Sub cmdPopulate_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPopulate.Click
        On Error GoTo ERR1
        Dim StrLine As String
        Dim mParameter As String
        Dim mSpecification As String
        Dim mActDim1 As String
        Dim I As Integer

        If Trim(txtFile.Text) = "" Then Exit Sub

        FileOpen(1, txtFile.Text, OpenMode.Input)
        Do While Not EOF(1) ' Loop until end of file.
            StrLine = LineInput(1)
            StrLine = Trim(StrLine)
            If StrLine = "" Or VB.Left(StrLine, 10) = "REV NUMBER" Or VB.Left(StrLine, 10) = "SER NUMBER" Or VB.Left(StrLine, 11) = "STATS COUNT" Or VB.Left(StrLine, 3) = "DIM" Or VB.Left(StrLine, 12) = "AX   NOMINAL" Or VB.Left(StrLine, 5) = "GRAPH" Then
                GoTo NextLine
            End If

            If VB.Left(StrLine, 9) = "PART NAME" Then
                txtPartName.Text = Trim(Mid(StrLine, 14))
                GoTo NextLine
            End If

            mParameter = ""
            mSpecification = ""
            mActDim1 = ""

            I = InStr(1, StrLine, " ")
            mParameter = Trim(VB.Left(StrLine, I))
            If mParameter = "A" Then
                mParameter = "ANGLE"
            ElseIf mParameter = "D" Then
                mParameter = "DIA"
            ElseIf mParameter = "R" Then
                mParameter = "RADIUS"
            ElseIf mParameter = "M" Then
                mParameter = "DIMENSION"
            Else
                GoTo NextLine
            End If

            StrLine = Trim(Mid(StrLine, I))
            I = InStr(1, StrLine, " ")
            mSpecification = Trim(VB.Left(StrLine, I))

            StrLine = Trim(Mid(StrLine, I))
            I = InStr(1, StrLine, " ")
            mSpecification = mSpecification & " + " & Trim(VB.Left(StrLine, I))

            StrLine = Trim(Mid(StrLine, I))
            I = InStr(1, StrLine, " ")
            mSpecification = mSpecification & " - " & Trim(VB.Left(StrLine, I))

            StrLine = Trim(Mid(StrLine, I))
            I = InStr(1, StrLine, " ")
            mActDim1 = Trim(VB.Left(StrLine, I))

            With SprdMain
                .Row = .MaxRows
                .MaxRows = .MaxRows + 1

                .Col = ColParameter
                .Text = mParameter

                .Col = ColSpecification
                .Text = mSpecification

                .Col = ColActDim1
                .Text = mActDim1
            End With

NextLine:
        Loop
        FileClose(1)

        Exit Sub
ERR1:
        MsgBox(Err.Description)
        Resume
    End Sub

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
        Dim mInHouse As String
        Dim mDim As String
        Dim mFitment As String
        Dim mLabTest As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()


        SqlStr = ""

        mInHouse = IIf(chkInHouse.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mDim = IIf(chkDim.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mFitment = IIf(chkFitment.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mLabTest = IIf(chkLabTest.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")


        mSlipNo = Val(txtSlipNo.Text)
        If Val(txtSlipNo.Text) = 0 Then
            mSlipNo = AutoGenKeyNo()
        End If
        txtSlipNo.Text = CStr(mSlipNo)
        If ADDMode = True Then
            lblMkey.Text = CStr(mSlipNo)
            SqlStr = " INSERT INTO QAL_INS_INITSAMPART_HDR " & vbCrLf _
                        & " (AUTO_KEY_INITSAMPART,COMPANY_CODE,FYEAR," & vbCrLf _
                        & " DOC_DATE,PART_NO,PART_NAME,DRAWINGNO,SAMPLE_DESC,PROJECT,INHOUSE,DEPT_CODE,SUPP_CUST_CODE," & vbCrLf _
                        & " NO_OF_SAMPLES,SUB_FREQ,PREV_IRNO,PREV_IRNO_DATE, " & vbCrLf _
                        & " DIMENSIONAL_CHECK,FITMENT_CHECK,LAB_TEST,DISPOSITION, " & vbCrLf _
                        & " REMARKS,INSP_EMP_CODE,SIGN_EMP_CODE, " & vbCrLf _
                        & " ADDUSER, ADDDATE, MODUSER, MODDATE) " & vbCrLf _
                        & " VALUES ( " & vbCrLf _
                        & " " & mSlipNo & "," & RsCompany.Fields("COMPANY_CODE").Value & "," & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf _
                        & " TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(txtPartNo.Text) & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(txtPartName.Text) & "','" & MainClass.AllowSingleQuote(txtDrawingNo.Text) & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(cboSample.Text) & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(txtProject.Text) & "','" & mInHouse & "','" & MainClass.AllowSingleQuote(txtDept.Text) & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(txtSupplier.Text) & "', " & vbCrLf _
                        & " " & Val(txtNoOfSamples.Text) & ",'" & MainClass.AllowSingleQuote(txtFrequency.Text) & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(txtRefIRNo.Text) & "',TO_DATE('" & vb6.Format(txtRefIRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                        & " '" & mDim & "','" & mFitment & "','" & mLabTest & "','" & MainClass.AllowSingleQuote(cboDisposition.Text) & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "','" & MainClass.AllowSingleQuote(txtInspectedBy.Text) & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(txtAuthorisedBy.Text) & "', " & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"
        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE QAL_INS_INITSAMPART_HDR SET " & vbCrLf _
                    & " DOC_DATE=TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),PART_NO='" & MainClass.AllowSingleQuote(txtPartNo.Text) & "', " & vbCrLf _
                    & " PART_NAME='" & MainClass.AllowSingleQuote(txtPartName.Text) & "', " & vbCrLf _
                    & " DRAWINGNO='" & MainClass.AllowSingleQuote(txtDrawingNo.Text) & "', " & vbCrLf _
                    & " SAMPLE_DESC='" & MainClass.AllowSingleQuote(cboSample.Text) & "', " & vbCrLf _
                    & " PROJECT='" & MainClass.AllowSingleQuote(txtProject.Text) & "',INHOUSE='" & mInHouse & "', " & vbCrLf _
                    & " DEPT_CODE='" & MainClass.AllowSingleQuote(txtDept.Text) & "', " & vbCrLf _
                    & " SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtSupplier.Text) & "', " & vbCrLf _
                    & " NO_OF_SAMPLES=" & Val(txtNoOfSamples.Text) & ",SUB_FREQ='" & MainClass.AllowSingleQuote(txtFrequency.Text) & "', " & vbCrLf _
                    & " PREV_IRNO='" & MainClass.AllowSingleQuote(txtRefIRNo.Text) & "',PREV_IRNO_DATE=TO_DATE('" & vb6.Format(txtRefIRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " DIMENSIONAL_CHECK='" & mDim & "',FITMENT_CHECK='" & mFitment & "',LAB_TEST='" & mLabTest & "', " & vbCrLf _
                    & " DISPOSITION='" & MainClass.AllowSingleQuote(cboDisposition.Text) & "', " & vbCrLf _
                    & " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                    & " INSP_EMP_CODE='" & MainClass.AllowSingleQuote(txtInspectedBy.Text) & "', " & vbCrLf _
                    & " SIGN_EMP_CODE='" & MainClass.AllowSingleQuote(txtAuthorisedBy.Text) & "', " & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                    & " MODDATE=TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND AUTO_KEY_INITSAMPART =" & Val(lblMkey.Text) & ""
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
        RsInitSampleMain.Requery()
        RsInitSampleDetail.Requery()
        MsgBox(Err.Description)
        Resume
    End Function
    Private Function AutoGenKeyNo() As Double

        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset
        Dim mAutoGen As Double
        Dim SqlStr As String
        Dim mMaxValue As String

        mAutoGen = 1
        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_INITSAMPART)  " & vbCrLf & " FROM QAL_INS_INITSAMPART_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

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
        Dim mInspection As String
        Dim mActDim1 As String
        Dim mActDim2 As String
        Dim mActDim3 As String
        Dim mActDim4 As String
        Dim mActDim5 As String
        Dim mRemarks As String

        PubDBCn.Execute("DELETE FROM QAL_INS_INITSAMPART_DET WHERE AUTO_KEY_INITSAMPART=" & Val(lblMkey.Text) & "")

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColParameter
                mParameter = MainClass.AllowSingleQuote(.Text)

                .Col = ColSpecification
                mSpecification = MainClass.AllowSingleQuote(.Text)

                .Col = ColInspection
                mInspection = MainClass.AllowSingleQuote(.Text)

                .Col = ColActDim1
                mActDim1 = MainClass.AllowSingleQuote(.Text)

                .Col = ColActDim2
                mActDim2 = MainClass.AllowSingleQuote(.Text)

                .Col = ColActDim3
                mActDim3 = MainClass.AllowSingleQuote(.Text)

                .Col = ColActDim4
                mActDim4 = MainClass.AllowSingleQuote(.Text)

                .Col = ColActDim5
                mActDim5 = MainClass.AllowSingleQuote(.Text)

                .Col = ColRemarks
                mRemarks = MainClass.AllowSingleQuote(.Text)

                SqlStr = ""

                If mParameter <> "" And mSpecification <> "" Then
                    SqlStr = " INSERT INTO  QAL_INS_INITSAMPART_DET ( " & vbCrLf & " AUTO_KEY_INITSAMPART,SERIAL_NO,PARAM_DESC,SPECIFICATION,INSP_MTH, " & vbCrLf & " DIMENSION1,DIMENSION2,DIMENSION3,DIMENSION4,DIMENSION5,REMARKS ) " & vbCrLf & " VALUES ( " & vbCrLf & " " & Val(lblMkey.Text) & "," & I & ",'" & mParameter & "','" & mSpecification & "', " & vbCrLf & " '" & mInspection & "','" & mActDim1 & "','" & mActDim2 & "','" & mActDim3 & "', " & vbCrLf & " '" & mActDim4 & "','" & mActDim5 & "','" & mRemarks & "') "
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


    Private Sub cmdSearchAuthorised_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchAuthorised.Click
        Call SearchEmp(txtAuthorisedBy, lblAuthorisedBy)
    End Sub
    Private Sub cmdSearchDept_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchDept.Click
        Dim SqlStr As String
        If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_CODE", "DEPT_DESC", , , " COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " ") = True Then
            txtDept.Text = AcName
            lblDept.text = AcName1
            If txtDept.Enabled = True Then txtDept.Focus()
        End If
    End Sub

    Private Sub cmdSearchInspected_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchInspected.Click
        Call SearchEmp(txtInspectedBy, lblInspectedBy)
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

    Private Sub cmdsearchSupplier_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearchSupplier.Click
        Dim SqlStr As String
        If MainClass.SearchGridMaster("", "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , " COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND SUPP_CUST_TYPE IN ('C','S') ") = True Then
            txtSupplier.Text = AcName1
            lblSupplier.text = AcName
            If txtSupplier.Enabled = True Then txtSupplier.Focus()
        End If
    End Sub

    Private Sub cmdSearchSlipNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchSlipNo.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
        If MainClass.SearchGridMaster(txtSlipNo.Text, "QAL_INS_INITSAMPART_HDR", "AUTO_KEY_INITSAMPART", "DOC_DATE", "PART_NO", "PART_NAME", SqlStr) = True Then
            txtSlipNo.Text = AcName
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
        MainClass.ButtonStatus(Me, XRIGHT, RsInitSampleMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmInitailSample_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Initial Sample Parts Inspection"

        SqlStr = "Select * From QAL_INS_INITSAMPART_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsInitSampleMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From QAL_INS_INITSAMPART_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsInitSampleDetail, ADODB.LockTypeEnum.adLockReadOnly)

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

        SqlStr = " SELECT " & vbCrLf & " AUTO_KEY_INITSAMPART AS SLIP_NUMBER,TO_CHAR(DOC_DATE,'DD/MM/YYYY') AS DOC_DATE, " & vbCrLf & " PART_NO,PART_NAME,SAMPLE_DESC,PROJECT,SUPP_CUST_CODE " & vbCrLf & " FROM QAL_INS_INITSAMPART_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " ORDER BY AUTO_KEY_INITSAMPART"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmInitailSample_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmInitailSample_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        xMenuID = myMenu
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Left = 0
        Me.Top = 0
        'Me.Height = VB6.TwipsToPixelsY(8010)
        'Me.Width = VB6.TwipsToPixelsX(10755)
        Call FillCbo()
        ADDMode = False
        MODIFYMode = False
        FormActive = False
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub FillCbo()
        cboSample.Items.Add("New Part")
        cboSample.Items.Add("Changed Part")
        cboSample.Items.Add("New Tooling")
        cboSample.Items.Add("New Source")
        cboSample.Items.Add("Semi Finished")
        cboSample.Items.Add("Finished")
        cboSample.Items.Add(" ")
        cboSample.SelectedIndex = 0

        cboDisposition.Items.Add("Approved")
        cboDisposition.Items.Add("Rejected")
        cboDisposition.Items.Add("Deviation For Pilot Lot")
        cboDisposition.Items.Add("To be Corrected")
        cboDisposition.Items.Add("Accepted (Under Deviation)")
        cboDisposition.Items.Add("")
        cboDisposition.SelectedIndex = 0

    End Sub
    Private Sub Clear1()

        On Error GoTo ClearErr

        lblMkey.Text = ""
        txtSlipNo.Text = ""
        txtDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtPartNo.Text = ""
        txtPartName.Text = ""
        txtDrawingNo.Text = ""
        cboSample.SelectedIndex = 0
        txtProject.Text = ""
        chkInHouse.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkInHouse_CheckStateChanged(chkInHouse, New System.EventArgs())
        txtDept.Text = ""
        lblDept.Text = ""
        txtSupplier.Text = ""
        lblSupplier.Text = ""
        txtNoOfSamples.Text = ""
        txtFrequency.Text = ""
        txtRefIRNo.Text = ""
        txtRefIRDate.Text = ""
        chkDim.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkFitment.CheckState = System.Windows.Forms.CheckState.Unchecked
        chkLabTest.CheckState = System.Windows.Forms.CheckState.Unchecked
        cboDisposition.SelectedIndex = 0
        txtRemarks.Text = ""
        txtInspectedBy.Text = ""
        lblInspectedBy.Text = ""
        txtAuthorisedBy.Text = ""
        lblAuthorisedBy.Text = ""
        txtFile.Text = ""

        txtAuthorisedBy.Enabled = IIf(lblBookType.Text = "H", True, False)
        cmdSearchAuthorised.Enabled = IIf(lblBookType.Text = "H", True, False)

        Call MakeEnableDesableField(True)
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsInitSampleMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
            .TypeEditLen = RsInitSampleDetail.Fields("PARAM_DESC").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColSpecification
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsInitSampleDetail.Fields("SPECIFICATION").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColInspection
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsInitSampleDetail.Fields("INSP_MTH").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColActDim1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsInitSampleDetail.Fields("DIMENSION1").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColActDim2
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsInitSampleDetail.Fields("DIMENSION2").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColActDim3
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsInitSampleDetail.Fields("DIMENSION3").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColActDim4
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsInitSampleDetail.Fields("DIMENSION4").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColActDim5
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsInitSampleDetail.Fields("DIMENSION5").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsInitSampleDetail.Fields("REMARKS").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

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
            .set_ColWidth(4, 500 * 4)
            .set_ColWidth(5, 500 * 5)
            .set_ColWidth(6, 500 * 5)
            .set_ColWidth(7, 500 * 3)
            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1

        txtSlipNo.Maxlength = RsInitSampleMain.Fields("AUTO_KEY_INITSAMPART").Precision
        txtDate.Maxlength = RsInitSampleMain.Fields("DOC_DATE").DefinedSize - 6
        txtPartNo.Maxlength = RsInitSampleMain.Fields("PART_NO").DefinedSize
        txtPartName.Maxlength = RsInitSampleMain.Fields("PART_NAME").DefinedSize
        txtDrawingNo.Maxlength = RsInitSampleMain.Fields("DRAWINGNO").DefinedSize
        txtProject.Maxlength = RsInitSampleMain.Fields("PROJECT").DefinedSize
        txtDept.Maxlength = RsInitSampleMain.Fields("DEPT_CODE").DefinedSize
        txtSupplier.Maxlength = RsInitSampleMain.Fields("SUPP_CUST_CODE").DefinedSize
        txtNoOfSamples.Maxlength = RsInitSampleMain.Fields("NO_OF_SAMPLES").Precision
        txtFrequency.Maxlength = RsInitSampleMain.Fields("SUB_FREQ").DefinedSize
        txtRefIRNo.Maxlength = RsInitSampleMain.Fields("PREV_IRNO").DefinedSize
        txtRefIRDate.Maxlength = RsInitSampleMain.Fields("PREV_IRNO_DATE").DefinedSize - 6
        txtRemarks.Maxlength = RsInitSampleMain.Fields("REMARKS").DefinedSize
        txtInspectedBy.Maxlength = RsInitSampleMain.Fields("INSP_EMP_CODE").DefinedSize
        txtAuthorisedBy.Maxlength = RsInitSampleMain.Fields("SIGN_EMP_CODE").DefinedSize
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        Resume
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
        If MODIFYMode = True And RsInitSampleMain.EOF = True Then Exit Function

        If Trim(txtDate.Text) = "" Then
            MsgInformation("Date is empty, So unable to save.")
            txtDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtPartName.Text) = "" Then
            MsgInformation("Part Name. empty, So unable to save.")
            txtPartName.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtDrawingNo.Text) = "" Then
            MsgInformation("Drawing No. empty, So unable to save.")
            txtDrawingNo.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If chkInHouse.CheckState = System.Windows.Forms.CheckState.Checked Then
            If Trim(txtDept.Text) = "" Then
                MsgInformation("Dept empty, So unable to save.")
                txtDept.Focus()
                FieldsVarification = False
                Exit Function
            End If
        Else
            If Trim(txtSupplier.Text) = "" Then
                MsgInformation("Supplier Code empty, So unable to save.")
                txtSupplier.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If Trim(txtProject.Text) = "" Then
            MsgInformation("Project is empty, So unable to save.")
            txtProject.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtInspectedBy.Text) = "" Then
            MsgInformation("Inspected employee code is empty, So unable to save.")
            txtInspectedBy.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidDataInGrid(SprdMain, ColParameter, "S", "Please Check Parameter Description.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColSpecification, "S", "Please Check Specification Details.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColInspection, "S", "Please Check Inspection Method.") = False Then FieldsVarification = False : Exit Function

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        Resume
    End Function

    Private Sub frmInitailSample_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        RsInitSampleMain.Close()
        RsInitSampleMain = Nothing
        RsInitSampleDetail.Close()
        RsInitSampleDetail = Nothing
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
            Case ColParameter
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColParameter
                xParameter = Trim(SprdMain.Text)
                If xParameter = "" Then Exit Sub

                SprdMain.Col = ColSpecification
                xSpecification = Trim(SprdMain.Text)

                If CheckDuplicateParam_Specfic(xParameter, xSpecification) = False Then
                    MainClass.AddBlankSprdRow(SprdMain, ColParameter, ConRowHeight)
                    FormatSprdMain((SprdMain.MaxRows))
                End If
            Case ColSpecification
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColParameter
                xParameter = Trim(SprdMain.Text)
                If xParameter = "" Then Exit Sub

                SprdMain.Col = ColSpecification
                xSpecification = Trim(SprdMain.Text)

                Call CheckDuplicateParam_Specfic(xParameter, xSpecification)
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


    Private Sub txtDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDept_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDept.DoubleClick
        Call cmdSearchDept_Click(cmdSearchDept, New System.EventArgs())
    End Sub

    Private Sub txtDept_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDept.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchDept_Click(cmdSearchDept, New System.EventArgs())
    End Sub

    Private Sub txtDept_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDept.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim SqlStr As String
        If Trim(txtDept.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable(txtDept.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " ") = True Then
            lblDept.text = MasterNo
        Else
            MsgBox("Not a valid Dept.")
            lblDept.Text = ""
            Cancel = True
        End If

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSupplier_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplier.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSupplier_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSupplier.DoubleClick
        Call cmdsearchSupplier_Click(cmdsearchSupplier, New System.EventArgs())
    End Sub

    Private Sub txtSupplier_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSupplier.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdsearchSupplier_Click(cmdsearchSupplier, New System.EventArgs())
    End Sub

    Private Sub txtSupplier_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSupplier.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim SqlStr As String
        If Trim(txtSupplier.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable(txtSupplier.Text, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " AND SUPP_CUST_TYPE IN ('C','S') ") = True Then
            lblSupplier.text = MasterNo
        Else
            MsgBox("Not a valid Customer.")
            lblSupplier.Text = ""
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPartNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPartNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtNoOfSamples_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNoOfSamples.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtNoOfSamples_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNoOfSamples.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRefIRDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRefIRDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRefIRDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRefIRDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtRefIRDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtRefIRDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtRefIRNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRefIRNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAuthorisedBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAuthorisedBy.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAuthorisedBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAuthorisedBy.DoubleClick
        Call cmdSearchAuthorised_Click(cmdSearchAuthorised, New System.EventArgs())
    End Sub

    Private Sub txtAuthorisedBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtAuthorisedBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchAuthorised_Click(cmdSearchAuthorised, New System.EventArgs())
    End Sub

    Private Sub txtAuthorisedBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAuthorisedBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If ValidateEMP(txtAuthorisedBy, lblAuthorisedBy) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtProject_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProject.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtFrequency_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFrequency.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtInspectedBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInspectedBy.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtInspectedBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInspectedBy.DoubleClick
        Call cmdSearchInspected_Click(cmdSearchInspected, New System.EventArgs())
    End Sub

    Private Sub txtInspectedBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtInspectedBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchInspected_Click(cmdSearchInspected, New System.EventArgs())
    End Sub

    Private Sub txtInspectedBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtInspectedBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If ValidateEMP(txtInspectedBy, lblInspectedBy) = False Then Cancel = True
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
        Dim mDisposition As String
        Clear1()

        If Not RsInitSampleMain.EOF Then
            IsShowing = True
            lblMkey.Text = IIf(IsDbNull(RsInitSampleMain.Fields("AUTO_KEY_INITSAMPART").Value), "", RsInitSampleMain.Fields("AUTO_KEY_INITSAMPART").Value)
            txtSlipNo.Text = IIf(IsDbNull(RsInitSampleMain.Fields("AUTO_KEY_INITSAMPART").Value), "", RsInitSampleMain.Fields("AUTO_KEY_INITSAMPART").Value)
            txtDate.Text = IIf(IsDbNull(RsInitSampleMain.Fields("DOC_DATE").Value), "", RsInitSampleMain.Fields("DOC_DATE").Value)
            txtPartNo.Text = IIf(IsDbNull(RsInitSampleMain.Fields("PART_NO").Value), "", RsInitSampleMain.Fields("PART_NO").Value)
            txtPartName.Text = IIf(IsDbNull(RsInitSampleMain.Fields("PART_NAME").Value), "", RsInitSampleMain.Fields("PART_NAME").Value)
            txtDrawingNo.Text = IIf(IsDbNull(RsInitSampleMain.Fields("DRAWINGNO").Value), "", RsInitSampleMain.Fields("DRAWINGNO").Value)
            cboSample.Text = IIf(IsDbNull(RsInitSampleMain.Fields("SAMPLE_DESC").Value), "", RsInitSampleMain.Fields("SAMPLE_DESC").Value)
            chkInHouse.CheckState = IIf(IsDbNull(RsInitSampleMain.Fields("INHOUSE").Value) Or RsInitSampleMain.Fields("INHOUSE").Value = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
            txtProject.Text = IIf(IsDbNull(RsInitSampleMain.Fields("PROJECT").Value), "", RsInitSampleMain.Fields("PROJECT").Value)
            txtDept.Text = IIf(IsDbNull(RsInitSampleMain.Fields("DEPT_CODE").Value), "", RsInitSampleMain.Fields("DEPT_CODE").Value)
            txtDept_Validating(txtDept, New System.ComponentModel.CancelEventArgs(False))
            txtSupplier.Text = IIf(IsDbNull(RsInitSampleMain.Fields("SUPP_CUST_CODE").Value), "", RsInitSampleMain.Fields("SUPP_CUST_CODE").Value)
            txtSupplier_Validating(txtSupplier, New System.ComponentModel.CancelEventArgs(False))
            txtNoOfSamples.Text = IIf(IsDbNull(RsInitSampleMain.Fields("NO_OF_SAMPLES").Value), "", RsInitSampleMain.Fields("NO_OF_SAMPLES").Value)
            txtFrequency.Text = IIf(IsDbNull(RsInitSampleMain.Fields("SUB_FREQ").Value), "", RsInitSampleMain.Fields("SUB_FREQ").Value)
            txtRefIRNo.Text = IIf(IsDbNull(RsInitSampleMain.Fields("PREV_IRNO").Value), "", RsInitSampleMain.Fields("PREV_IRNO").Value)
            txtRefIRDate.Text = IIf(IsDbNull(RsInitSampleMain.Fields("PREV_IRNO_DATE").Value), "", RsInitSampleMain.Fields("PREV_IRNO_DATE").Value)
            chkDim.CheckState = IIf(IsDbNull(RsInitSampleMain.Fields("DIMENSIONAL_CHECK").Value) Or RsInitSampleMain.Fields("DIMENSIONAL_CHECK").Value = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
            chkFitment.CheckState = IIf(IsDbNull(RsInitSampleMain.Fields("FITMENT_CHECK").Value) Or RsInitSampleMain.Fields("FITMENT_CHECK").Value = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
            chkLabTest.CheckState = IIf(IsDbNull(RsInitSampleMain.Fields("LAB_TEST").Value) Or RsInitSampleMain.Fields("LAB_TEST").Value = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)
            mDisposition = Trim(IIf(IsDbNull(RsInitSampleMain.Fields("DISPOSITION").Value), "", RsInitSampleMain.Fields("DISPOSITION").Value))
            If mDisposition = "" Then
                cboDisposition.SelectedIndex = 5
            Else
                cboDisposition.Text = mDisposition
            End If

            txtRemarks.Text = IIf(IsDbNull(RsInitSampleMain.Fields("REMARKS").Value), "", RsInitSampleMain.Fields("REMARKS").Value)
            txtInspectedBy.Text = IIf(IsDbNull(RsInitSampleMain.Fields("INSP_EMP_CODE").Value), "", RsInitSampleMain.Fields("INSP_EMP_CODE").Value)
            txtInspectedBy_Validating(txtInspectedBy, New System.ComponentModel.CancelEventArgs(False))
            txtAuthorisedBy.Text = IIf(IsDbNull(RsInitSampleMain.Fields("SIGN_EMP_CODE").Value), "", RsInitSampleMain.Fields("SIGN_EMP_CODE").Value)
            txtAuthorisedBy_Validating(txtAuthorisedBy, New System.ComponentModel.CancelEventArgs(False))
            Call ShowDetail1()
            Call MakeEnableDesableField(False)
            IsShowing = False
        End If
        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = True    '' False Sandeep 15/05/2022
        txtSlipNo.Enabled = True
        cmdSearchSlipNo.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsInitSampleMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Private Sub ShowDetail1()

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf & " FROM QAL_INS_INITSAMPART_DET " & vbCrLf & " WHERE AUTO_KEY_INITSAMPART=" & Val(lblMkey.Text) & "" & vbCrLf & " ORDER BY SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsInitSampleDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsInitSampleDetail
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            I = 1
            Do While Not .EOF
                SprdMain.Row = I

                SprdMain.Col = ColParameter
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("PARAM_DESC").Value), "", .Fields("PARAM_DESC").Value))

                SprdMain.Col = ColSpecification
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("SPECIFICATION").Value), "", .Fields("SPECIFICATION").Value))

                SprdMain.Col = ColInspection
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("INSP_MTH").Value), "", .Fields("INSP_MTH").Value))

                SprdMain.Col = ColActDim1
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("DIMENSION1").Value), "", .Fields("DIMENSION1").Value))

                SprdMain.Col = ColActDim2
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("DIMENSION2").Value), "", .Fields("DIMENSION2").Value))

                SprdMain.Col = ColActDim3
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("DIMENSION3").Value), "", .Fields("DIMENSION3").Value))

                SprdMain.Col = ColActDim4
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("DIMENSION4").Value), "", .Fields("DIMENSION4").Value))

                SprdMain.Col = ColActDim5
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("DIMENSION5").Value), "", .Fields("DIMENSION5").Value))

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


        If Trim(txtSlipNo.Text) = "" Or Val(txtSlipNo.Text) = 0 Then GoTo EventExitSub
        If Len(txtSlipNo.Text) <= 5 Then
            txtSlipNo.Text = Val(txtSlipNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If
        mSlipNo = Val(txtSlipNo.Text)

        If MODIFYMode = True And RsInitSampleMain.BOF = False Then xMKey = RsInitSampleMain.Fields("AUTO_KEY_INITSAMPART").Value

        SqlStr = "SELECT * FROM QAL_INS_INITSAMPART_HDR " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_INITSAMPART,LENGTH(AUTO_KEY_INITSAMPART)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_INITSAMPART=" & mSlipNo & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsInitSampleMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsInitSampleMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Number. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM QAL_INS_INITSAMPART_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_INITSAMPART,LENGTH(AUTO_KEY_INITSAMPART)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_INITSAMPART=" & Val(CStr(xMKey)) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsInitSampleMain, ADODB.LockTypeEnum.adLockReadOnly)
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
        txtPartNo.Enabled = mMode
        txtPartName.Enabled = mMode
        txtDrawingNo.Enabled = mMode
        txtInspectedBy.Enabled = mMode
        cmdSearchInspected.Enabled = mMode
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
    Private Sub ReportOnSample(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mTitle As String

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        mTitle = "INSPECTION REPORT-INITIAL SAMPLE PARTS"
        SqlStr = "SELECT QAL_INS_INITSAMPART_HDR.*,QAL_INS_INITSAMPART_DET.*, " & vbCrLf & " PAY_DEPT_MST.DEPT_DESC,FIN_SUPP_CUST_MST.SUPP_CUST_NAME, " & vbCrLf & " PAY_EMPLOYEE_MST.EMP_NAME,EMP2.EMP_NAME " & vbCrLf & " FROM QAL_INS_INITSAMPART_HDR,QAL_INS_INITSAMPART_DET,  " & vbCrLf & " PAY_DEPT_MST,FIN_SUPP_CUST_MST,PAY_EMPLOYEE_MST ,PAY_EMPLOYEE_MST EMP2 " & vbCrLf & " WHERE QAL_INS_INITSAMPART_HDR.AUTO_KEY_INITSAMPART=QAL_INS_INITSAMPART_DET.AUTO_KEY_INITSAMPART " & vbCrLf & " AND QAL_INS_INITSAMPART_HDR.COMPANY_CODE=PAY_DEPT_MST.COMPANY_CODE (+) " & vbCrLf & " AND QAL_INS_INITSAMPART_HDR.DEPT_CODE=PAY_DEPT_MST.DEPT_CODE (+) " & vbCrLf & " AND QAL_INS_INITSAMPART_HDR.COMPANY_CODE=FIN_SUPP_CUST_MST.COMPANY_CODE (+) " & vbCrLf & " AND QAL_INS_INITSAMPART_HDR.SUPP_CUST_CODE=FIN_SUPP_CUST_MST.SUPP_CUST_CODE (+) " & vbCrLf & " AND QAL_INS_INITSAMPART_HDR.COMPANY_CODE=PAY_EMPLOYEE_MST.COMPANY_CODE (+) " & vbCrLf & " AND QAL_INS_INITSAMPART_HDR.INSP_EMP_CODE=PAY_EMPLOYEE_MST.EMP_CODE (+) " & vbCrLf & " AND QAL_INS_INITSAMPART_HDR.COMPANY_CODE=EMP2.COMPANY_CODE (+) " & vbCrLf & " AND QAL_INS_INITSAMPART_HDR.SIGN_EMP_CODE=EMP2.EMP_CODE (+) " & vbCrLf & " AND QAL_INS_INITSAMPART_HDR.AUTO_KEY_INITSAMPART=" & Val(lblMkey.Text) & ""

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\InspecRepInitSamp.rpt"

        SetCrpt(Report1, Mode, 1, mTitle, , True, xMenuID)
        Report1.SQLQuery = SqlStr
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)

    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnSample(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnSample(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
End Class
