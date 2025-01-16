Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmInspecRepGF
    Inherits System.Windows.Forms.Form
    Dim RsInspecRepGFMain As ADODB.Recordset
    Dim RsInspecRepGFDetail As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

    Dim xMenuID As String
    Dim IsShowing As Boolean

    Private Const ConRowHeight As Short = 14

    Private Const ColParameter As Short = 1
    Private Const ColSpecification As Short = 2
    Private Const ColInspecMethod As Short = 3
    Private Const ColActDim1 As Short = 4
    Private Const ColActDim2 As Short = 5
    Private Const ColActDim3 As Short = 6
    Private Const ColActDim4 As Short = 7
    Private Const ColRemarks As Short = 8



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
            If RsInspecRepGFMain.EOF = False Then RsInspecRepGFMain.MoveFirst()
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
        If Not RsInspecRepGFMain.EOF Then
            If Not IsDbNull(RsInspecRepGFMain.Fields("AUTH_EMP_CODE").Value) Or RsInspecRepGFMain.Fields("AUTH_EMP_CODE").Value <> "" Then MsgBox("Number been approved, So cann't be deleted") : Exit Sub
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "QAL_NEW_GAUSFIXT_HDR", (txtSlipNo.Text), RsInspecRepGFMain) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM QAL_NEW_GAUSFIXT_DET WHERE AUTO_KEY_GAUSFIXT=" & Val(lblMkey.Text) & "")
                PubDBCn.Execute("DELETE FROM QAL_NEW_GAUSFIXT_HDR WHERE AUTO_KEY_GAUSFIXT=" & Val(lblMkey.Text) & "")
                PubDBCn.CommitTrans()
                RsInspecRepGFMain.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        '    Resume
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
        RsInspecRepGFMain.Requery()
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
            If RsInspecRepGFMain.Fields("AUTH_EMP_CODE").Value <> "" Then MsgBox("Number been approved, So cann't be modified") : Exit Sub
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsInspecRepGFMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
            If StrLine = "" Or VB.Left(StrLine, 10) = "REV NUMBER" Or VB.Left(StrLine, 10) = "SER NUMBER" Or VB.Left(StrLine, 11) = "STATS COUNT" Or VB.Left(StrLine, 3) = "DIM" Or VB.Left(StrLine, 12) = "AX   NOMINAL" Then
                GoTo NextLine
            End If

            If VB.Left(StrLine, 9) = "PART NAME" Then
                txtTypeOfGF.Text = Trim(Mid(StrLine, 14))
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
            SqlStr = " INSERT INTO QAL_NEW_GAUSFIXT_HDR " & vbCrLf _
                            & " (AUTO_KEY_GAUSFIXT,COMPANY_CODE," & vbCrLf _
                            & " DOC_DATE,SOURCE,PO_NO,PO_DATE," & vbCrLf _
                            & " TYPE_OF_GAUSFIXT,PROJECT,MODEL,DRAWING_NO,CHL_NO, " & vbCrLf _
                            & " CHL_DATE,MRR_NO,MRR_DATE,RECD_QTY,ACC_QTY,REJ_QTY, " & vbCrLf _
                            & " BAL_RECD_QTY,INS_EMP_CODE,INS_DATE,VER_EMP_CODE,VER_DATE, " & vbCrLf _
                            & " AUTH_EMP_CODE,AUTH_DATE,ADDUSER, ADDDATE, MODUSER, MODDATE) " & vbCrLf _
                            & " VALUES ( " & vbCrLf _
                            & " " & mSlipNo & "," & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(txtSource.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtPONo.Text) & "',TO_DATE('" & vb6.Format(txtPODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtTypeOfGF.Text) & "','" & MainClass.AllowSingleQuote(txtProject.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtModel.Text) & "','" & MainClass.AllowSingleQuote(txtGFDrgNo.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtChallanNo.Text) & "',TO_DATE('" & vb6.Format(txtChallanDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtMRRNo.Text) & "',TO_DATE('" & vb6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " " & Val(txtReceiveQty.Text) & "," & Val(txtAcceptedQty.Text) & "," & Val(txtRejectedQty.Text) & "," & Val(txtBalQty.Text) & ", " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtInspectionBy.Text) & "',O_DATE('" & vb6.Format(txtInspectionDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtVerifiedBy.Text) & "',O_DATE('" & vb6.Format(txtVerifiedDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtSignatoryBy.Text) & "',O_DATE('" & vb6.Format(txtSignatoryDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"
        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE QAL_NEW_GAUSFIXT_HDR SET " & vbCrLf _
                    & " AUTO_KEY_GAUSFIXT=" & mSlipNo & ",COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                    & " DOC_DATE=TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),SOURCE='" & MainClass.AllowSingleQuote(txtSource.Text) & "', " & vbCrLf _
                    & " PO_NO='" & MainClass.AllowSingleQuote(txtPONo.Text) & "', " & vbCrLf _
                    & " PO_DATE=TO_DATE('" & vb6.Format(txtPODate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " TYPE_OF_GAUSFIXT='" & MainClass.AllowSingleQuote(txtTypeOfGF.Text) & "', " & vbCrLf _
                    & " PROJECT='" & MainClass.AllowSingleQuote(txtProject.Text) & "', " & vbCrLf _
                    & " MODEL='" & MainClass.AllowSingleQuote(txtModel.Text) & "', " & vbCrLf _
                    & " DRAWING_NO='" & MainClass.AllowSingleQuote(txtGFDrgNo.Text) & "', " & vbCrLf _
                    & " CHL_NO='" & MainClass.AllowSingleQuote(txtChallanNo.Text) & "', " & vbCrLf _
                    & " CHL_DATE=TO_DATE('" & vb6.Format(txtChallanDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " MRR_NO='" & MainClass.AllowSingleQuote(txtMRRNo.Text) & "', " & vbCrLf _
                    & " MRR_DATE=TO_DATE('" & vb6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " RECD_QTY=" & Val(txtReceiveQty.Text) & ",ACC_QTY=" & Val(txtAcceptedQty.Text) & ", " & vbCrLf _
                    & " REJ_QTY=" & Val(txtRejectedQty.Text) & ", " & vbCrLf _
                    & " BAL_RECD_QTY=" & Val(txtBalQty.Text) & ", " & vbCrLf _
                    & " INS_EMP_CODE='" & MainClass.AllowSingleQuote(txtInspectionBy.Text) & "',INS_DATE=TO_DATE('" & vb6.Format(txtInspectionDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " VER_EMP_CODE='" & MainClass.AllowSingleQuote(txtVerifiedBy.Text) & "',VER_DATE=TO_DATE('" & vb6.Format(txtVerifiedDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " AUTH_EMP_CODE='" & MainClass.AllowSingleQuote(txtSignatoryBy.Text) & "',AUTH_DATE=TO_DATE('" & vb6.Format(txtSignatoryDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                    & " MODDATE=TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND AUTO_KEY_GAUSFIXT =" & Val(lblMkey.Text) & ""
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
        RsInspecRepGFMain.Requery()
        RsInspecRepGFDetail.Requery()
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
        SqlStr = "SELECT Max(AUTO_KEY_GAUSFIXT)  " & vbCrLf & " FROM QAL_NEW_GAUSFIXT_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_GAUSFIXT,LENGTH(AUTO_KEY_GAUSFIXT)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

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
        Dim mInspecMethod As String
        Dim mActDim1 As String
        Dim mActDim2 As String
        Dim mActDim3 As String
        Dim mActDim4 As String
        Dim mRemarks As String

        PubDBCn.Execute("DELETE FROM QAL_NEW_GAUSFIXT_DET WHERE AUTO_KEY_GAUSFIXT=" & Val(lblMkey.Text) & "")

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColParameter
                mParameter = MainClass.AllowSingleQuote(.Text)

                .Col = ColSpecification
                mSpecification = MainClass.AllowSingleQuote(.Text)

                .Col = ColInspecMethod
                mInspecMethod = MainClass.AllowSingleQuote(.Text)

                .Col = ColActDim1
                mActDim1 = MainClass.AllowSingleQuote(.Text)

                .Col = ColActDim2
                mActDim2 = MainClass.AllowSingleQuote(.Text)

                .Col = ColActDim3
                mActDim3 = MainClass.AllowSingleQuote(.Text)

                .Col = ColActDim4
                mActDim4 = MainClass.AllowSingleQuote(.Text)

                .Col = ColRemarks
                mRemarks = MainClass.AllowSingleQuote(.Text)

                SqlStr = ""

                If mParameter <> "" And mSpecification <> "" Then
                    SqlStr = " INSERT INTO  QAL_NEW_GAUSFIXT_DET ( " & vbCrLf & " AUTO_KEY_GAUSFIXT,SERIAL_NO,PARAM,SPEC,INSP_MTH,ACT_DIM1, " & vbCrLf & " ACT_DIM2,ACT_DIM3,ACT_DIM4,REMARKS ) " & vbCrLf & " VALUES ( " & vbCrLf & " " & Val(lblMkey.Text) & "," & I & ",'" & mParameter & "','" & mSpecification & "', " & vbCrLf & " '" & mInspecMethod & "','" & mActDim1 & "','" & mActDim2 & "','" & mActDim3 & "','" & mActDim4 & "', " & vbCrLf & " '" & mRemarks & "') "
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


    Private Sub cmdSearchInspec_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchInspec.Click
        Call SearchEmp(txtInspectionBy, lblInspectionBy)
    End Sub


    Private Sub cmdSearchModel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchModel.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        '            & " AND SUBSTR(AUTO_KEY_GAUSFIXT,LENGTH(AUTO_KEY_GAUSFIXT)-5,4)=" & RsCompany.fields("FYEAR").value & ""
        If MainClass.SearchGridMaster(txtModel.Text, "QAL_NEW_GAUSFIXT_HDR", "MODEL", "AUTO_KEY_GAUSFIXT", "SOURCE", "PROJECT", SqlStr) = True Then
            txtModel.Text = AcName
            txtSlipNo.Text = AcName1
            Call txtSlipNo_Validating(txtSlipNo, New System.ComponentModel.CancelEventArgs(False)) ''_Validating(XXXX, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
        End If
    End Sub

    Private Sub cmdSearchSource_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchSource.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        '            & " AND SUBSTR(AUTO_KEY_GAUSFIXT,LENGTH(AUTO_KEY_GAUSFIXT)-5,4)=" & RsCompany.fields("FYEAR").value & ""
        If MainClass.SearchGridMaster(txtSource.Text, "QAL_NEW_GAUSFIXT_HDR", "SOURCE", "AUTO_KEY_GAUSFIXT", "MODEL", "PROJECT", SqlStr) = True Then
            txtSource.Text = AcName
            txtSlipNo.Text = AcName1
            Call txtSlipNo_Validating(txtSlipNo, New System.ComponentModel.CancelEventArgs(False)) ''_Validating(XXXX, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
        End If
    End Sub

    Private Sub cmdSearchVerified_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchVerified.Click
        Call SearchEmp(txtVerifiedBy, lblVerifiedBy)
    End Sub


    Private Sub cmdSearchSignatory_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchSignatory.Click
        Call SearchEmp(txtSignatoryBy, lblSignatoryBy)
    End Sub
    Private Sub SearchEmp(ByRef pTextBax As System.Windows.Forms.TextBox, ByRef pLable As System.Windows.Forms.Label)
        On Error GoTo SrchERR
        Dim SqlStr As String
        '    SqlStr = "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "  AND EMP_LEAVE_DATE IS NULL "

        If ADDMode = True Then
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND EMP_LEAVE_DATE IS NULL "
        Else
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If

        If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            pTextBax.Text = AcName1
            pLable.text = AcName
            If pTextBax.Enabled = True Then pTextBax.Focus()
        End If
        Exit Sub
SrchERR:
        MsgBox(Err.Description)
    End Sub
    Private Sub cmdSearchSlipNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchSlipNo.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        '            & " AND SUBSTR(AUTO_KEY_GAUSFIXT,LENGTH(AUTO_KEY_GAUSFIXT)-5,4)=" & RsCompany.fields("FYEAR").value & ""
        If MainClass.SearchGridMaster(txtSlipNo.Text, "QAL_NEW_GAUSFIXT_HDR", "AUTO_KEY_GAUSFIXT", "SOURCE", "MODEL", "PROJECT", SqlStr) = True Then
            txtSlipNo.Text = AcName
            Call txtSlipNo_Validating(txtSlipNo, New System.ComponentModel.CancelEventArgs(False)) ''_Validating(XXXX, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
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
        MainClass.ButtonStatus(Me, XRIGHT, RsInspecRepGFMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmInspecRepGF_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Inspection Report Of New Gauses/Fixtures"

        SqlStr = "Select * From QAL_NEW_GAUSFIXT_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsInspecRepGFMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From QAL_NEW_GAUSFIXT_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsInspecRepGFDetail, ADODB.LockTypeEnum.adLockReadOnly)

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

        SqlStr = " SELECT " & vbCrLf & " AUTO_KEY_GAUSFIXT AS SLIP_NUMBER,TO_CHAR(DOC_DATE,'DD/MM/YYYY') AS DOCDATE, " & vbCrLf & " SOURCE,TYPE_OF_GAUSFIXT,PROJECT,MODEL,DRAWING_NO, " & vbCrLf & " CHL_NO,TO_CHAR(CHL_DATE,'DD/MM/YYYY') AS CHL_DATE,  " & vbCrLf & " MRR_NO,TO_CHAR(MRR_DATE,'DD/MM/YYYY') AS CHL_DATE,  " & vbCrLf & " RECD_QTY,ACC_QTY,REJ_QTY,BAL_RECD_QTY " & vbCrLf & " FROM QAL_NEW_GAUSFIXT_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " ORDER BY DOC_DATE,AUTO_KEY_GAUSFIXT"

        '            & " AND SUBSTR(AUTO_KEY_GAUSFIXT,LENGTH(AUTO_KEY_GAUSFIXT)-5,4)=" & RsCompany.fields("FYEAR").value & "" & vbCrLf _
        '
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmInspecRepGF_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmInspecRepGF_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        txtSource.Text = ""
        txtPONo.Text = ""
        txtPODate.Text = ""
        txtTypeOfGF.Text = ""
        txtProject.Text = ""
        txtModel.Text = ""
        txtGFDrgNo.Text = ""
        txtChallanNo.Text = ""
        txtChallanDate.Text = ""
        txtMRRNo.Text = ""
        txtMRRDate.Text = ""
        txtReceiveQty.Text = ""
        txtAcceptedQty.Text = ""
        txtRejectedQty.Text = ""
        txtBalQty.Text = ""
        txtInspectionBy.Text = ""
        lblInspectionBy.Text = ""
        txtInspectionDate.Text = ""
        txtVerifiedBy.Text = ""
        lblVerifiedBy.Text = ""
        txtSignatoryBy.Text = ""
        lblSignatoryBy.Text = ""
        txtFile.Text = ""
        Call MakeEnableDesableField(True)
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsInspecRepGFMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
            .TypeEditLen = RsInspecRepGFDetail.Fields("PARAM").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColSpecification
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsInspecRepGFDetail.Fields("SPEC").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColInspecMethod
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsInspecRepGFDetail.Fields("INSP_MTH").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColActDim1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsInspecRepGFDetail.Fields("ACT_DIM1").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColActDim2
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsInspecRepGFDetail.Fields("ACT_DIM2").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColActDim3
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsInspecRepGFDetail.Fields("ACT_DIM3").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColActDim4
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsInspecRepGFDetail.Fields("ACT_DIM4").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsInspecRepGFDetail.Fields("REMARKS").DefinedSize
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

        txtSlipNo.Maxlength = RsInspecRepGFMain.Fields("AUTO_KEY_GAUSFIXT").Precision
        txtDate.Maxlength = RsInspecRepGFMain.Fields("DOC_DATE").DefinedSize - 6
        txtSource.Maxlength = RsInspecRepGFMain.Fields("SOURCE").DefinedSize
        txtPONo.Maxlength = RsInspecRepGFMain.Fields("PO_NO").DefinedSize
        txtPODate.Maxlength = RsInspecRepGFMain.Fields("PO_DATE").DefinedSize - 6
        txtTypeOfGF.Maxlength = RsInspecRepGFMain.Fields("TYPE_OF_GAUSFIXT").DefinedSize
        txtProject.Maxlength = RsInspecRepGFMain.Fields("PROJECT").DefinedSize
        txtModel.Maxlength = RsInspecRepGFMain.Fields("MODEL").DefinedSize
        txtGFDrgNo.Maxlength = RsInspecRepGFMain.Fields("DRAWING_NO").DefinedSize
        txtChallanNo.Maxlength = RsInspecRepGFMain.Fields("CHL_NO").DefinedSize
        txtChallanDate.Maxlength = RsInspecRepGFMain.Fields("CHL_DATE").DefinedSize - 6
        txtMRRNo.Maxlength = RsInspecRepGFMain.Fields("MRR_NO").DefinedSize
        txtMRRDate.Maxlength = RsInspecRepGFMain.Fields("MRR_DATE").DefinedSize - 6
        txtReceiveQty.Maxlength = RsInspecRepGFMain.Fields("RECD_QTY").Precision
        txtAcceptedQty.Maxlength = RsInspecRepGFMain.Fields("ACC_QTY").Precision
        txtRejectedQty.Maxlength = RsInspecRepGFMain.Fields("REJ_QTY").Precision
        txtBalQty.Maxlength = RsInspecRepGFMain.Fields("BAL_RECD_QTY").Precision
        txtInspectionBy.Maxlength = RsInspecRepGFMain.Fields("INS_EMP_CODE").DefinedSize
        txtInspectionDate.Maxlength = RsInspecRepGFMain.Fields("INS_DATE").DefinedSize - 6
        txtVerifiedBy.Maxlength = RsInspecRepGFMain.Fields("VER_EMP_CODE").DefinedSize
        txtVerifiedDate.Maxlength = RsInspecRepGFMain.Fields("VER_DATE").DefinedSize - 6
        txtSignatoryBy.Maxlength = RsInspecRepGFMain.Fields("AUTH_EMP_CODE").DefinedSize
        txtSignatoryDate.Maxlength = RsInspecRepGFMain.Fields("AUTH_DATE").DefinedSize - 6
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
        If MODIFYMode = True And RsInspecRepGFMain.EOF = True Then Exit Function

        If Trim(txtDate.Text) = "" Then
            MsgInformation("Date is empty, So unable to save.")
            txtDate.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtSource.Text) = "" Then
            MsgInformation("Source is empty, So unable to save.")
            txtSource.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtTypeOfGF.Text) = "" Then
            MsgInformation("Type Of Gauge/Fixture is empty, So unable to save.")
            txtTypeOfGF.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If MainClass.ValidDataInGrid(SprdMain, ColParameter, "S", "Please Check Parameter Description.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColSpecification, "S", "Please Check Specification Details.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColInspecMethod, "S", "Please Check Inspection Method.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColActDim1, "S", "Please Check Actual Dimension (1).") = False Then FieldsVarification = False : Exit Function

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        Resume
    End Function

    Private Sub frmInspecRepGF_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        RsInspecRepGFMain.Close()
        RsInspecRepGFMain = Nothing
        RsInspecRepGFDetail.Close()
        RsInspecRepGFDetail = Nothing
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
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
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
        If ADDMode = True Then
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND EMP_LEAVE_DATE IS NULL "
        Else
            SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        End If

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


    Private Sub txtAcceptedQty_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAcceptedQty.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
    Private Function CheckQty() As Boolean
        CheckQty = True
        If Val(txtAcceptedQty.Text) > Val(txtReceiveQty.Text) Then
            MsgBox("Accepted Qty cann't be greater than Receive Qty.")
            CheckQty = False : Exit Function
        End If
    End Function
    Private Sub CalcRejQty()
        txtRejectedQty.Text = VB6.Format(Val(txtReceiveQty.Text) - Val(txtAcceptedQty.Text), "#0.00")
    End Sub


    Private Sub txtAcceptedQty_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAcceptedQty.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtAcceptedQty_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAcceptedQty.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckQty = False Then Cancel = True : GoTo EventExitSub
        Call CalcRejQty()
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtBalQty_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBalQty.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBalQty_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBalQty.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtBalQty_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBalQty.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckQty = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtChallanDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtChallanDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtChallanDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtChallanDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtChallanDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtChallanDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtModel_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtModel.DoubleClick
        Call cmdSearchModel_Click(cmdSearchModel, New System.EventArgs())
    End Sub

    Private Sub txtModel_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtModel.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtModel.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtModel_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtModel.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchModel_Click(cmdSearchModel, New System.EventArgs())
    End Sub

    Private Sub txtMRRNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMRRNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPODate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPODate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPODate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPODate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtPODate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtPODate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtReceiveQty_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtReceiveQty.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtReceiveQty_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtReceiveQty.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)

        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtReceiveQty_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtReceiveQty.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckQty = False Then Cancel = True : GoTo EventExitSub
        Call CalcRejQty()
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRejectedQty_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRejectedQty.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRejectedQty_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRejectedQty.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtRejectedQty_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRejectedQty.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckQty = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSignatoryDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSignatoryDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSignatoryDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSignatoryDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtSignatoryDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtSignatoryDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSource_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSource.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSource_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSource.DoubleClick
        Call cmdSearchSource_Click(cmdSearchSource, New System.EventArgs())
    End Sub

    Private Sub txtSource_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSource.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSource.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSource_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSource.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchSource_Click(cmdSearchSource, New System.EventArgs())
    End Sub

    Private Sub txtTypeOfGF_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTypeOfGF.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtInspectionDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInspectionDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtInspectionDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtInspectionDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtInspectionDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtInspectionDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtInspectionBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInspectionBy.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtInspectionBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInspectionBy.DoubleClick
        Call cmdSearchInspec_Click(cmdSearchInspec, New System.EventArgs())
    End Sub

    Private Sub txtInspectionBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtInspectionBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchInspec_Click(cmdSearchInspec, New System.EventArgs())
    End Sub

    Private Sub txtInspectionBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtInspectionBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If ValidateEMP(txtInspectionBy, lblInspectionBy) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtProject_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProject.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtModel_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtModel.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtChallanNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtChallanNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtGFDrgNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtGFDrgNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPONo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPONo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtVerifiedBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVerifiedBy.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtVerifiedBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVerifiedBy.DoubleClick
        Call cmdSearchVerified_Click(cmdSearchVerified, New System.EventArgs())
    End Sub

    Private Sub txtVerifiedBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtVerifiedBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchVerified_Click(cmdSearchVerified, New System.EventArgs())
    End Sub

    Private Sub txtVerifiedBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtVerifiedBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If ValidateEMP(txtVerifiedBy, lblVerifiedBy) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtMRRDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMRRDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMRRDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMRRDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtMRRDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtMRRDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtSignatoryBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSignatoryBy.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSignatoryBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSignatoryBy.DoubleClick
        Call cmdSearchSignatory_Click(cmdSearchSignatory, New System.EventArgs())
    End Sub

    Private Sub txtSignatoryBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSignatoryBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchSignatory_Click(cmdSearchSignatory, New System.EventArgs())
    End Sub

    Private Sub txtSignatoryBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSignatoryBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If ValidateEMP(txtSignatoryBy, lblSignatoryBy) = False Then Cancel = True
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
        Clear1()
        If Not RsInspecRepGFMain.EOF Then
            IsShowing = True
            lblMkey.Text = IIf(IsDbNull(RsInspecRepGFMain.Fields("AUTO_KEY_GAUSFIXT").Value), "", RsInspecRepGFMain.Fields("AUTO_KEY_GAUSFIXT").Value)
            txtSlipNo.Text = IIf(IsDbNull(RsInspecRepGFMain.Fields("AUTO_KEY_GAUSFIXT").Value), "", RsInspecRepGFMain.Fields("AUTO_KEY_GAUSFIXT").Value)
            txtDate.Text = IIf(IsDbNull(RsInspecRepGFMain.Fields("DOC_DATE").Value), "", RsInspecRepGFMain.Fields("DOC_DATE").Value)
            txtSource.Text = IIf(IsDbNull(RsInspecRepGFMain.Fields("Source").Value), "", RsInspecRepGFMain.Fields("Source").Value)
            txtPONo.Text = IIf(IsDbNull(RsInspecRepGFMain.Fields("PO_NO").Value), "", RsInspecRepGFMain.Fields("PO_NO").Value)
            txtPODate.Text = IIf(IsDbNull(RsInspecRepGFMain.Fields("PO_DATE").Value), "", RsInspecRepGFMain.Fields("PO_DATE").Value)
            txtTypeOfGF.Text = IIf(IsDbNull(RsInspecRepGFMain.Fields("TYPE_OF_GAUSFIXT").Value), "", RsInspecRepGFMain.Fields("TYPE_OF_GAUSFIXT").Value)
            txtProject.Text = IIf(IsDbNull(RsInspecRepGFMain.Fields("PROJECT").Value), "", RsInspecRepGFMain.Fields("PROJECT").Value)
            txtModel.Text = IIf(IsDbNull(RsInspecRepGFMain.Fields("MODEL").Value), "", RsInspecRepGFMain.Fields("MODEL").Value)
            txtGFDrgNo.Text = IIf(IsDbNull(RsInspecRepGFMain.Fields("DRAWING_NO").Value), "", RsInspecRepGFMain.Fields("DRAWING_NO").Value)
            txtChallanNo.Text = IIf(IsDbNull(RsInspecRepGFMain.Fields("CHL_NO").Value), "", RsInspecRepGFMain.Fields("CHL_NO").Value)
            txtChallanDate.Text = IIf(IsDbNull(RsInspecRepGFMain.Fields("CHL_DATE").Value), "", RsInspecRepGFMain.Fields("CHL_DATE").Value)
            txtMRRNo.Text = IIf(IsDbNull(RsInspecRepGFMain.Fields("MRR_NO").Value), "", RsInspecRepGFMain.Fields("MRR_NO").Value)
            txtMRRDate.Text = IIf(IsDbNull(RsInspecRepGFMain.Fields("MRR_DATE").Value), "", RsInspecRepGFMain.Fields("MRR_DATE").Value)
            txtReceiveQty.Text = IIf(IsDbNull(RsInspecRepGFMain.Fields("RECD_QTY").Value), " ", RsInspecRepGFMain.Fields("RECD_QTY").Value)
            txtAcceptedQty.Text = IIf(IsDbNull(RsInspecRepGFMain.Fields("ACC_QTY").Value), " ", RsInspecRepGFMain.Fields("ACC_QTY").Value)
            txtRejectedQty.Text = IIf(IsDbNull(RsInspecRepGFMain.Fields("REJ_QTY").Value), " ", RsInspecRepGFMain.Fields("REJ_QTY").Value)
            txtBalQty.Text = IIf(IsDbNull(RsInspecRepGFMain.Fields("BAL_RECD_QTY").Value), " ", RsInspecRepGFMain.Fields("BAL_RECD_QTY").Value)
            txtInspectionBy.Text = IIf(IsDbNull(RsInspecRepGFMain.Fields("INS_EMP_CODE").Value), "", RsInspecRepGFMain.Fields("INS_EMP_CODE").Value)
            txtInspectionBy_Validating(txtInspectionBy, New System.ComponentModel.CancelEventArgs(False))
            txtInspectionDate.Text = IIf(IsDbNull(RsInspecRepGFMain.Fields("INS_DATE").Value), "", RsInspecRepGFMain.Fields("INS_DATE").Value)
            txtVerifiedBy.Text = IIf(IsDbNull(RsInspecRepGFMain.Fields("VER_EMP_CODE").Value), "", RsInspecRepGFMain.Fields("VER_EMP_CODE").Value)
            txtVerifiedBy_Validating(txtVerifiedBy, New System.ComponentModel.CancelEventArgs(False))
            txtVerifiedDate.Text = IIf(IsDbNull(RsInspecRepGFMain.Fields("VER_DATE").Value), "", RsInspecRepGFMain.Fields("VER_DATE").Value)
            txtSignatoryBy.Text = IIf(IsDbNull(RsInspecRepGFMain.Fields("AUTH_EMP_CODE").Value), "", RsInspecRepGFMain.Fields("AUTH_EMP_CODE").Value)
            txtSignatoryBy_Validating(txtSignatoryBy, New System.ComponentModel.CancelEventArgs(False))
            txtSignatoryDate.Text = IIf(IsDbNull(RsInspecRepGFMain.Fields("AUTH_DATE").Value), "", RsInspecRepGFMain.Fields("AUTH_DATE").Value)
            Call ShowDetail1()
            Call MakeEnableDesableField(False)
            IsShowing = False
        End If
        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = True    '' False Sandeep 15/05/2022
        txtSlipNo.Enabled = True
        cmdSearchSlipNo.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsInspecRepGFMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
        SqlStr = " SELECT * " & vbCrLf & " FROM QAL_NEW_GAUSFIXT_DET " & vbCrLf & " WHERE AUTO_KEY_GAUSFIXT=" & Val(lblMkey.Text) & "" & vbCrLf & " ORDER BY SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsInspecRepGFDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsInspecRepGFDetail
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            I = 1
            Do While Not .EOF
                SprdMain.Row = I

                SprdMain.Col = ColParameter
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("PARAM").Value), "", .Fields("PARAM").Value))

                SprdMain.Col = ColSpecification
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("SPEC").Value), "", .Fields("SPEC").Value))

                SprdMain.Col = ColInspecMethod
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("INSP_MTH").Value), "", .Fields("INSP_MTH").Value))

                SprdMain.Col = ColActDim1
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("ACT_DIM1").Value), "", .Fields("ACT_DIM1").Value))

                SprdMain.Col = ColActDim2
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("ACT_DIM2").Value), "", .Fields("ACT_DIM2").Value))

                SprdMain.Col = ColActDim3
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("ACT_DIM3").Value), "", .Fields("ACT_DIM3").Value))

                SprdMain.Col = ColActDim4
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("ACT_DIM4").Value), "", .Fields("ACT_DIM4").Value))

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

        If MODIFYMode = True And RsInspecRepGFMain.BOF = False Then xMKey = RsInspecRepGFMain.Fields("AUTO_KEY_GAUSFIXT").Value

        SqlStr = "SELECT * FROM QAL_NEW_GAUSFIXT_HDR " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTO_KEY_GAUSFIXT=" & mSlipNo & ""

        '            & " AND SUBSTR(AUTO_KEY_GAUSFIXT,LENGTH(AUTO_KEY_GAUSFIXT)-5,4)=" & RsCompany.fields("FYEAR").value & "" & vbCrLf _
        '
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsInspecRepGFMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsInspecRepGFMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Number. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM QAL_NEW_GAUSFIXT_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTO_KEY_GAUSFIXT=" & Val(CStr(xMKey)) & " "

                '            & " AND SUBSTR(AUTO_KEY_GAUSFIXT,LENGTH(AUTO_KEY_GAUSFIXT)-5,4)=" & RsCompany.fields("FYEAR").value & "" & vbCrLf _
                '
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsInspecRepGFMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub MakeEnableDesableField(ByRef mMode As Boolean)
        txtDate.Enabled = IIf(PubSuperUser = "S", True, mMode)
        txtRejectedQty.Enabled = False
        txtInspectionBy.Enabled = mMode
        cmdSearchInspec.Enabled = mMode

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
    Private Sub ReportOnInspecRepGF(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mTitle As String

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        mTitle = "INSPECTION REPORT OF NEW GAUSES/FIXTURES"
        SqlStr = "SELECT QAL_NEW_GAUSFIXT_HDR.*,QAL_NEW_GAUSFIXT_DET.* " & vbCrLf & " PAY_EMPLOYEE_MST.EMP_NAME,EMP2.EMP_NAME,EMP3.EMP_NAME " & vbCrLf & " FROM QAL_NEW_GAUSFIXT_HDR,QAL_NEW_GAUSFIXT_DET,  " & vbCrLf & " PAY_EMPLOYEE_MST ,PAY_EMPLOYEE_MST EMP2,PAY_EMPLOYEE_MST EMP3 " & vbCrLf & " WHERE QAL_NEW_GAUSFIXT_HDR.AUTO_KEY_GAUSFIXT=QAL_NEW_GAUSFIXT_DET.AUTO_KEY_GAUSFIXT " & vbCrLf & " AND QAL_NEW_GAUSFIXT_HDR.COMPANY_CODE=PAY_EMPLOYEE_MST.COMPANY_CODE (+) " & vbCrLf & " AND QAL_NEW_GAUSFIXT_HDR.INS_EMP_CODE=PAY_EMPLOYEE_MST.EMP_CODE (+) " & vbCrLf & " AND QAL_NEW_GAUSFIXT_HDR.COMPANY_CODE=EMP2.COMPANY_CODE (+) " & vbCrLf & " AND QAL_NEW_GAUSFIXT_HDR.VER_EMP_CODE=EMP2.EMP_CODE (+) " & vbCrLf & " AND QAL_NEW_GAUSFIXT_HDR.COMPANY_CODE=EMP3.COMPANY_CODE (+) " & vbCrLf & " AND QAL_NEW_GAUSFIXT_HDR.AUTH_EMP_CODE=EMP3.EMP_CODE (+) " & vbCrLf & " AND QAL_NEW_GAUSFIXT_HDR.AUTO_KEY_GAUSFIXT=" & Val(lblMkey.Text) & ""

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\InspecRepNewGF.rpt"

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
        Call ReportOnInspecRepGF(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnInspecRepGF(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub txtVerifiedDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVerifiedDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtVerifiedDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtVerifiedDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtVerifiedDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtVerifiedDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
