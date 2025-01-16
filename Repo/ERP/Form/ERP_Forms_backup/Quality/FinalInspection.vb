Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmFinalInspection
    Inherits System.Windows.Forms.Form
    Dim RsFinalInspMain As ADODB.Recordset
    Dim RsFinalInspDetail As ADODB.Recordset
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
    Private Const ColInspection As Short = 3
    Private Const ColCategory As Short = 4
    Private Const ColClass As Short = 5
    Private Const ColObserv1 As Short = 6
    Private Const ColObserv2 As Short = 7
    Private Const ColObserv3 As Short = 8
    Private Const ColObserv4 As Short = 9
    Private Const ColObserv5 As Short = 10
    Private Const ColRemarks As Short = 11

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
            If RsFinalInspMain.EOF = False Then RsFinalInspMain.MoveFirst()
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
        If Not RsFinalInspMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "QAL_FINAL_HDR", (txtSlipNo.Text), RsFinalInspMain) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM QAL_FINAL_DET WHERE AUTO_KEY_FINAL=" & Val(lblMkey.Text) & "")
                PubDBCn.Execute("DELETE FROM QAL_FINAL_HDR WHERE AUTO_KEY_FINAL=" & Val(lblMkey.Text) & "")
                PubDBCn.CommitTrans()
                RsFinalInspMain.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
        RsFinalInspMain.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsFinalInspMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
            SqlStr = " INSERT INTO QAL_FINAL_HDR " & vbCrLf _
                            & " (AUTO_KEY_FINAL,COMPANY_CODE," & vbCrLf _
                            & " INSP_DATE,ITEM_CODE,AUTO_KEY_STD,SUPP_CUST_CODE,LOT_SIZE," & vbCrLf _
                            & " SAMPLE_SIZE,REMARKS,ACCEPT_STATUS, " & vbCrLf _
                            & " INSPECTED_BY,INVOICE_NO, " & vbCrLf _
                            & " ADDUSER, ADDDATE, MODUSER, MODDATE) " & vbCrLf _
                            & " VALUES ( " & vbCrLf _
                            & " " & mSlipNo & "," & RsCompany.fields("COMPANY_CODE").value & ", " & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(txtPartNo.Text) & "', " & vbCrLf _
                            & " " & Val(txtInspectionSTD.Text) & ",'" & MainClass.AllowSingleQuote(txtCustomer.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtLotSize.Text) & "','" & MainClass.AllowSingleQuote(txtSampleSize.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "','" & MainClass.AllowSingleQuote(txtStatus.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtInspBy.Text) & "','" & MainClass.AllowSingleQuote(txtInvoiceNo.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"
        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE QAL_FINAL_HDR SET " & vbCrLf _
                    & " AUTO_KEY_FINAL=" & mSlipNo & ",COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & ", " & vbCrLf _
                    & " INSP_DATE=TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),ITEM_CODE='" & MainClass.AllowSingleQuote(txtPartNo.Text) & "', " & vbCrLf _
                    & " AUTO_KEY_STD=" & Val(txtInspectionSTD.Text) & ",SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCustomer.Text) & "', " & vbCrLf _
                    & " LOT_SIZE='" & MainClass.AllowSingleQuote(txtLotSize.Text) & "', " & vbCrLf _
                    & " SAMPLE_SIZE='" & MainClass.AllowSingleQuote(txtSampleSize.Text) & "', " & vbCrLf _
                    & " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                    & " ACCEPT_STATUS='" & MainClass.AllowSingleQuote(txtStatus.Text) & "', " & vbCrLf _
                    & " INSPECTED_BY='" & MainClass.AllowSingleQuote(txtInspBy.Text) & "', " & vbCrLf _
                    & " INVOICE_NO='" & MainClass.AllowSingleQuote(txtInvoiceNo.Text) & "', " & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                    & " MODDATE=TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                    & " AND AUTO_KEY_FINAL =" & Val(lblMkey.text) & ""
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
        RsFinalInspMain.Requery()
        RsFinalInspDetail.Requery()
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
        SqlStr = "SELECT Max(AUTO_KEY_FINAL)  " & vbCrLf & " FROM QAL_FINAL_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_FINAL,LENGTH(AUTO_KEY_FINAL)-5,4)=" & RsCompany.Fields("FYEAR").Value & " "

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
        Dim mObserv1 As String
        Dim mObserv2 As String
        Dim mObserv3 As String
        Dim mObserv4 As String
        Dim mRemarks As String
        Dim mCategory As String
        Dim mClass As String
        Dim mObserv5 As String

        PubDBCn.Execute("DELETE FROM QAL_FINAL_DET WHERE AUTO_KEY_FINAL=" & Val(lblMkey.Text) & "")

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColParameter
                mParameter = MainClass.AllowSingleQuote(.Text)

                .Col = ColSpecification
                mSpecification = MainClass.AllowSingleQuote(.Text)

                .Col = ColInspection
                mInspection = MainClass.AllowSingleQuote(.Text)

                .Col = ColCategory
                mCategory = MainClass.AllowSingleQuote(.Text)

                .Col = ColClass
                mClass = MainClass.AllowSingleQuote(.Text)

                .Col = ColObserv1
                mObserv1 = MainClass.AllowSingleQuote(.Text)

                .Col = ColObserv2
                mObserv2 = MainClass.AllowSingleQuote(.Text)

                .Col = ColObserv3
                mObserv3 = MainClass.AllowSingleQuote(.Text)

                .Col = ColObserv4
                mObserv4 = MainClass.AllowSingleQuote(.Text)

                .Col = ColObserv5
                mObserv5 = MainClass.AllowSingleQuote(.Text)

                .Col = ColRemarks
                mRemarks = MainClass.AllowSingleQuote(.Text)

                SqlStr = ""

                If mParameter <> "" And mSpecification <> "" Then
                    SqlStr = " INSERT INTO  QAL_FINAL_DET ( " & vbCrLf & " AUTO_KEY_FINAL,SERIAL_NO,PARAM_DESC, " & vbCrLf & " SPECIFICATION,INSP_MTH, " & vbCrLf & " OBSERV_1,OBSERV_2,OBSERV_3, " & vbCrLf & " OBSERV_4,OBSERV_5, CAT_DESC, CLASS_DESC, REMARKS ) " & vbCrLf & " VALUES ( " & vbCrLf & " " & Val(lblMkey.Text) & "," & I & ",'" & mParameter & "','" & mSpecification & "', " & vbCrLf & " '" & mInspection & "'," & vbCrLf & " '" & mObserv1 & "','" & mObserv2 & "','" & mObserv3 & "','" & mObserv4 & "','" & mObserv5 & "'," & vbCrLf & " '" & mCategory & "', '" & mClass & "', '" & mRemarks & "') "
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

    Private Sub cmdSearchInspBy_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchInspBy.Click
        Call SearchEmp(txtInspBy, lblInspBy)
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

        SqlStr = " SELECT A.SUPP_CUST_CODE, B.SUPP_CUST_NAME " & vbCrLf & " FROM FIN_SUPP_CUST_DET A, FIN_SUPP_CUST_MST B " & vbCrLf & " WHERE B.COMPANY_CODE = A.COMPANY_CODE " & vbCrLf & " AND LTRIM(RTRIM(B.SUPP_CUST_CODE)) = LTRIM(RTRIM(A.SUPP_CUST_CODE)) " & vbCrLf & " AND B.SUPP_CUST_TYPE IN ('S', 'C') " & vbCrLf & " AND A.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND LTRIM(RTRIM(A.ITEM_CODE)) ='" & LTrim(RTrim(MainClass.AllowSingleQuote(txtPartNo.Text))) & "' " & vbCrLf & " ORDER BY B.SUPP_CUST_NAME "


        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtCustomer.Text = AcName
            lblCustomer.Text = AcName1
            If txtCustomer.Enabled = True Then txtCustomer.Focus()
        End If

    End Sub

    Private Sub cmdSearchInvoiceNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchInvoiceNo.Click

        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf '            & " AND FYEAR=" & RsCompany.fields("FYEAR").value & " "
        '
        '    If Trim(txtCustomer.Text) <> "" Then
        '        SqlStr = SqlStr & vbCrLf _
        ''                & " AND SUPP_CUST_CODE = '" & MainClass.AllowSingleQuote(txtCustomer.Text) & "'"
        '    End If

        '    SqlStr = SqlStr & vbCrLf _
        ''            & " AND BILLNO NOT IN (" & vbCrLf _
        ''            & " SELECT INVOICE_NO " & vbCrLf _
        ''            & " FROM QAL_FINAL_HDR " & vbCrLf _
        ''            & " WHERE COMPANY_CODE = " & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
        ''            & " AND SUBSTR(AUTO_KEY_FINAL,LENGTH(AUTO_KEY_FINAL)-5,4) = " & RsCompany.fields("FYEAR").value & " " & vbCrLf _
        ''            & " AND INVOICE_NO IS NOT NULL " & vbCrLf _
        ''            & " )"

        '    If MainClass.SearchGridMaster(txtInvoiceNo.Text, "FIN_INVOICE_HDR", "BILLNO", "INVOICE_DATE", "AUTO_KEY_DESP", "DCDATE", SqlStr) = True Then
        '        txtInvoiceNo.Text = AcName
        '        Call txtInvoiceNo_Validating(XXXX, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
        '        txtInvoiceNo.SetFocus
        '    End If

        SqlStr = " SELECT DISTINCT IH.BILLNO, IH.INVOICE_DATE, IH.EXPBILLNO, IH.EXPINV_DATE " & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " AND IH.FYEAR = " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY "

        If Trim(TxtInvoiceNo.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND IH.BILLNO='" & Trim(TxtInvoiceNo.Text) & "'"
        End If

        If Trim(txtCustomer.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND IH.SUPP_CUST_CODE = '" & MainClass.AllowSingleQuote(txtCustomer.Text) & "'"
        End If

        If Trim(txtPartNo.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND ID.ITEM_CODE = '" & MainClass.AllowSingleQuote(txtPartNo.Text) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND IH.BILLNO || ID.ITEM_CODE NOT IN (" & vbCrLf & " SELECT INVOICE_NO || ITEM_CODE" & vbCrLf & " FROM QAL_FINAL_HDR " & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_FINAL,LENGTH(AUTO_KEY_FINAL)-5,4) = " & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND INVOICE_NO IS NOT NULL"

        If Trim(txtCustomer.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND SUPP_CUST_CODE = '" & MainClass.AllowSingleQuote(txtCustomer.Text) & "'"
        End If

        If Trim(TxtInvoiceNo.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND INVOICE_NO = '" & MainClass.AllowSingleQuote(TxtInvoiceNo.Text) & "'"
        End If

        If Trim(txtPartNo.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM_CODE = '" & MainClass.AllowSingleQuote(txtPartNo.Text) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " )"

        If MainClass.SearchGridMasterBySQL2(TxtInvoiceNo.Text, SqlStr) = True Then
            TxtInvoiceNo.Text = AcName
            Call txtInvoiceNo_Validating(txtInvoiceNo, New System.ComponentModel.CancelEventArgs(False))
            TxtInvoiceNo.Focus()
        End If

    End Sub

    Private Sub cmdSearchPartNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchPartNo.Click
        Dim SqlStr As String
        SqlStr = "SELECT A.ITEM_CODE, B.ITEM_SHORT_DESC " & vbCrLf & " FROM QAL_INSPECTION_STD_HDR A, INV_ITEM_MST B " & vbCrLf & " WHERE B.COMPANY_CODE = A.COMPANY_CODE " & vbCrLf & " AND LTRIM(RTRIM(B.ITEM_CODE)) = LTRIM(RTRIM(A.ITEM_CODE)) " & vbCrLf & " AND A.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND A.INSP_TYPE = 'F' " & vbCrLf & " ORDER BY B.ITEM_SHORT_DESC "
        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtPartNo.Text = AcName
            lblPartNo.text = AcName1
            If txtPartNo.Enabled = True Then txtPartNo.Focus()
        End If
    End Sub

    Private Sub cmdSearchSlipNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchSlipNo.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_FINAL,LENGTH(AUTO_KEY_FINAL)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
        If MainClass.SearchGridMaster(txtSlipNo.Text, "QAL_FINAL_HDR", "AUTO_KEY_FINAL", "INSP_DATE", "ITEM_CODE", "AUTO_KEY_STD", SqlStr) = True Then
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
        MainClass.ButtonStatus(Me, XRIGHT, RsFinalInspMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmFinalInspection_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Final Inspection"

        SqlStr = "Select * From QAL_FINAL_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsFinalInspMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From QAL_FINAL_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsFinalInspDetail, ADODB.LockTypeEnum.adLockReadOnly)

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

        SqlStr = " SELECT " & vbCrLf & " AUTO_KEY_FINAL AS SLIP_NUMBER,TO_CHAR(INSP_DATE,'DD/MM/YYYY') AS INSP_DATE, " & vbCrLf & " ITEM_CODE,AUTO_KEY_STD,SUPP_CUST_CODE,LOT_SIZE,SAMPLE_SIZE " & vbCrLf & " FROM QAL_FINAL_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_FINAL,LENGTH(AUTO_KEY_FINAL)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " ORDER BY AUTO_KEY_FINAL"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmFinalInspection_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, "")
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmFinalInspection_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        xMenuID = myMenu
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Left = 0
        Me.Top = 0
        Me.Height = VB6.TwipsToPixelsY(8010)
        Me.Width = VB6.TwipsToPixelsX(10755)
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
        txtPartNo.Text = ""
        lblPartNo.Text = ""
        txtInspectionSTD.Text = ""
        lblModel.Text = ""
        txtCustomer.Text = ""
        lblCustomer.Text = ""
        txtLotSize.Text = ""
        txtSampleSize.Text = ""
        txtRemarks.Text = ""
        txtStatus.Text = ""
        txtInspBy.Text = ""
        lblInspBy.Text = ""
        '    lblDocNo.text = ""
        '    lblIssueNo.text = ""
        TxtInvoiceNo.Text = ""
        lblDate.Text = ""
        lblChallanNo.Text = ""
        lblChallanDate.Text = ""

        Call MakeEnableDesableField(True)
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsFinalInspMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
            .TypeEditLen = RsFinalInspDetail.Fields("PARAM_DESC").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 15)
            .TypeEditMultiLine = True

            .Col = ColSpecification
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsFinalInspDetail.Fields("SPECIFICATION").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 15)
            .TypeEditMultiLine = True

            .Col = ColInspection
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsFinalInspDetail.Fields("INSP_MTH").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 15)
            .TypeEditMultiLine = True

            .Col = ColObserv1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsFinalInspDetail.Fields("OBSERV_1").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColObserv2
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsFinalInspDetail.Fields("OBSERV_2").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColObserv3
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsFinalInspDetail.Fields("OBSERV_3").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColObserv4
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsFinalInspDetail.Fields("OBSERV_4").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColObserv5
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsFinalInspDetail.Fields("OBSERV_5").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColCategory
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsFinalInspDetail.Fields("CAT_DESC").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColClass
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsFinalInspDetail.Fields("CLASS_DESC").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsFinalInspDetail.Fields("REMARKS").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColParameter, ColInspection)
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
            .set_ColWidth(5, 500 * 3)
            .set_ColWidth(6, 500 * 3)
            .set_ColWidth(7, 500 * 3)
            .ColsFrozen = 2
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub

    Private Sub SetTextLengths()
        On Error GoTo ERR1

        txtSlipNo.Maxlength = RsFinalInspMain.Fields("AUTO_KEY_FINAL").Precision
        txtDate.Maxlength = RsFinalInspMain.Fields("INSP_DATE").DefinedSize - 6
        txtPartNo.Maxlength = RsFinalInspMain.Fields("ITEM_CODE").DefinedSize
        txtInspectionSTD.Maxlength = RsFinalInspMain.Fields("AUTO_KEY_STD").Precision
        txtCustomer.Maxlength = RsFinalInspMain.Fields("SUPP_CUST_CODE").DefinedSize
        txtLotSize.Maxlength = RsFinalInspMain.Fields("LOT_SIZE").DefinedSize
        txtSampleSize.Maxlength = RsFinalInspMain.Fields("SAMPLE_SIZE").DefinedSize
        txtRemarks.Maxlength = RsFinalInspMain.Fields("REMARKS").DefinedSize
        txtStatus.Maxlength = RsFinalInspMain.Fields("ACCEPT_STATUS").DefinedSize
        txtInspBy.Maxlength = RsFinalInspMain.Fields("INSPECTED_BY").DefinedSize

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
        If MODIFYMode = True And RsFinalInspMain.EOF = True Then Exit Function

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

        If Trim(txtInspBy.Text) = "" Then
            MsgInformation("Inspection Employee Code is empty, So unable to save.")
            txtInspBy.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If MainClass.ValidDataInGrid(SprdMain, ColParameter, "S", "Please Check Parameter Description.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColSpecification, "S", "Please Check Specification Details.") = False Then FieldsVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColInspection, "S", "Please Check Inspection Method.") = False Then FieldsVarification = False : Exit Function

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function

    Private Sub frmFinalInspection_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        RsFinalInspMain.Close()
        RsFinalInspMain = Nothing
        RsFinalInspDetail.Close()
        RsFinalInspDetail = Nothing
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
                    & " AND LTRIM(RTRIM(A.SUPP_CUST_CODE)) = LTRIM(RTRIM(B.SUPP_CUST_CODE)) " & vbCrLf _
                    & " AND A.SUPP_CUST_TYPE IN ('C','S') " & vbCrLf _
                    & " AND B.COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                    & " AND LTRIM(RTRIM(B.ITEM_CODE)) ='" & MainClass.AllowSingleQuote(LTrim(RTrim(txtPartNo.Text))) & "' " & vbCrLf _
                    & " AND LTRIM(RTRIM(B.SUPP_CUST_CODE)) ='" & MainClass.AllowSingleQuote(LTrim(RTrim(txtCustomer.Text))) & "' "

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
        If Trim(txtInspectionSTD.Text) = "" Then GoTo EventExitSub
        SqlStr = "SELECT SERIAL_NO,PARAM_DESC, SPECIFICATION, INSP_MTH " & vbCrLf _
            & " From QAL_INSPECTION_STD_DET " & vbCrLf _
            & " WHERE AUTO_KEY_STD =" & Val(txtInspectionSTD.Text) & " ORDER BY SERIAL_NO"

        ''DETAIL_TYPE NOT IN ('A','E') AND" & vbCrLf _            & " 

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

                SprdMain.Col = ColInspection
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("INSP_MTH").Value), "", .Fields("INSP_MTH").Value))

                .MoveNext()
                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With
        FormatSprdMain(-1)
        GoTo EventExitSub
FillERR:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtInvoiceNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInvoiceNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtInvoiceNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInvoiceNo.DoubleClick
        Call cmdSearchInvoiceNo_Click(cmdSearchInvoiceNo, New System.EventArgs())
    End Sub

    Private Sub txtInvoiceNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtInvoiceNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchInvoiceNo_Click(cmdSearchInvoiceNo, New System.EventArgs())
    End Sub

    Private Sub txtInvoiceNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtInvoiceNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim SqlStr As String
        Dim mRsTemp As ADODB.Recordset
        If Trim(TxtInvoiceNo.Text) = "" Then GoTo EventExitSub

        SqlStr = " SELECT DISTINCT IH.BILLNO, IH.INVOICE_DATE, IH.AUTO_KEY_DESP, IH.DCDATE " & vbCrLf & " FROM FIN_INVOICE_HDR IH, FIN_INVOICE_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " AND IH.FYEAR = " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND IH.MKEY=ID.MKEY "

        If Trim(TxtInvoiceNo.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND IH.BILLNO='" & Trim(TxtInvoiceNo.Text) & "'"
        End If

        If Trim(txtCustomer.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND IH.SUPP_CUST_CODE = '" & MainClass.AllowSingleQuote(txtCustomer.Text) & "'"
        End If

        If Trim(txtPartNo.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND ID.ITEM_CODE = '" & MainClass.AllowSingleQuote(txtPartNo.Text) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " AND IH.BILLNO || ID.ITEM_CODE NOT IN (" & vbCrLf & " SELECT INVOICE_NO || ITEM_CODE" & vbCrLf & " FROM QAL_FINAL_HDR " & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_FINAL,LENGTH(AUTO_KEY_FINAL)-5,4) = " & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND INVOICE_NO IS NOT NULL"

        If Val(txtSlipNo.Text) > 0 Then
            SqlStr = SqlStr & vbCrLf & " AND AUTO_KEY_FINAL <> " & Val(txtSlipNo.Text) & ""
        End If

        If Trim(txtCustomer.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND SUPP_CUST_CODE = '" & MainClass.AllowSingleQuote(txtCustomer.Text) & "'"
        End If

        If Trim(TxtInvoiceNo.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND INVOICE_NO = '" & MainClass.AllowSingleQuote(TxtInvoiceNo.Text) & "'"
        End If

        If Trim(txtPartNo.Text) <> "" Then
            SqlStr = SqlStr & vbCrLf & " AND ITEM_CODE = '" & MainClass.AllowSingleQuote(txtPartNo.Text) & "'"
        End If

        SqlStr = SqlStr & vbCrLf & " )"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With mRsTemp
            If Not .EOF Then
                lblDate.Text = IIf(IsDbNull(mRsTemp.Fields("INVOICE_DATE").Value), "", VB6.Format(.Fields("INVOICE_DATE").Value, "DD/MM/YYYY"))
                lblChallanNo.Text = IIf(IsDbNull(mRsTemp.Fields("AUTO_KEY_DESP").Value), "", .Fields("AUTO_KEY_DESP").Value)
                lblChallanDate.Text = IIf(IsDbNull(mRsTemp.Fields("DCDATE").Value), "", VB6.Format(.Fields("DCDATE").Value, "DD/MM/YYYY"))
            Else
                MsgBox("Not a valid Invoice No.")
                lblDate.Text = ""
                lblChallanNo.Text = ""
                lblChallanDate.Text = ""
                Cancel = True
            End If
        End With
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

        SqlStr = " SELECT B.ITEM_SHORT_DESC, B.ITEM_MODEL, A.DOC_NO, A.REV_NO, A.AUTO_KEY_STD " & vbCrLf _
                    & " FROM QAL_INSPECTION_STD_HDR A, INV_ITEM_MST B " & vbCrLf _
                    & " WHERE B.COMPANY_CODE = A.COMPANY_CODE " & vbCrLf _
                    & " AND LTRIM(RTRIM(B.ITEM_CODE)) = LTRIM(RTRIM(A.ITEM_CODE)) " & vbCrLf _
                    & " AND A.COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                    & " AND A.INSP_TYPE = 'F' " & vbCrLf _
                    & " AND LTRIM(RTRIM(A.ITEM_CODE)) ='" & MainClass.AllowSingleQuote(txtPartNo.Text) & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With mRsTemp
            If Not .EOF Then
                lblPartNo.Text = IIf(IsDbNull(mRsTemp.Fields("Item_Short_Desc").Value), "", .Fields("Item_Short_Desc").Value)
                txtInspectionSTD.Text = IIf(IsDbNull(mRsTemp.Fields("AUTO_KEY_STD").Value), "", .Fields("AUTO_KEY_STD").Value)
                lblModel.Text = IIf(IsDbNull(mRsTemp.Fields("ITEM_MODEL").Value), "", .Fields("ITEM_MODEL").Value)
                '            lblDocNo.text = IIf(isdbnull(mRsTemp!DOC_NO), "", !DOC_NO)
                '            lblIssueNo.text = IIf(isdbnull(mRsTemp!REV_NO), "", !REV_NO)
                txtInspectionSTD_Validating(txtInspectionSTD, New System.ComponentModel.CancelEventArgs(False))

                '            SqlStr = " SELECT A.DOC_NO, A.DOC_REV_NO " & vbCrLf _
                ''                    & " FROM GEN_DOC_MST A, QAL_INSPECTION_STD_HDR B " & vbCrLf _
                ''                    & " Where a.COMPANY_CODE = b.COMPANY_CODE " & vbCrLf _
                ''                    & " AND LTRIM(RTRIM(A.DOC_NO)) = LTRIM(RTRIM(B.DOC_NO)) " & vbCrLf _
                ''                    & " AND B.COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                ''                    & " AND B.AUTO_KEY_STD =" & Val(txtInspectionSTD.Text) & " "
                '            MainClass.UOpenRecordSet SqlStr, PubDBCn, adOpenStatic, mRsTemp, adLockReadOnly
                '            If Not mRsTemp.EOF Then
                '                lblDocNo.text = IIf(isdbnull(mRsTemp!DOC_NO), "", mRsTemp!DOC_NO)
                '                lblIssueNo.text = IIf(isdbnull(mRsTemp!DOC_REV_NO), "", mRsTemp!DOC_REV_NO)
                '            Else
                '                lblDocNo.text = ""
                '                lblIssueNo.text = ""
                '            End If
            Else
                MsgBox("Not a valid Part No.")
                lblPartNo.Text = ""
                txtInspectionSTD.Text = ""
                '            lblDocNo.text = ""
                '            lblIssueNo.text = ""
                Cancel = True
            End If
        End With
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSampleSize_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSampleSize.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtStatus_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtStatus.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtInspBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInspBy.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtInspBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInspBy.DoubleClick
        Call cmdSearchInspBy_Click(cmdSearchInspBy, New System.EventArgs())
    End Sub

    Private Sub txtInspBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtInspBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchInspBy_Click(cmdSearchInspBy, New System.EventArgs())
    End Sub

    Private Sub txtInspBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtInspBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If ValidateEMP(txtInspBy, lblInspBy) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtLotSize_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLotSize.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtInspectionSTD_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInspectionSTD.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
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

        If Not RsFinalInspMain.EOF Then
            IsShowing = True
            lblMkey.Text = IIf(IsDbNull(RsFinalInspMain.Fields("AUTO_KEY_FINAL").Value), "", RsFinalInspMain.Fields("AUTO_KEY_FINAL").Value)
            txtSlipNo.Text = IIf(IsDbNull(RsFinalInspMain.Fields("AUTO_KEY_FINAL").Value), "", RsFinalInspMain.Fields("AUTO_KEY_FINAL").Value)
            txtDate.Text = IIf(IsDbNull(RsFinalInspMain.Fields("INSP_DATE").Value), "", RsFinalInspMain.Fields("INSP_DATE").Value)
            txtPartNo.Text = IIf(IsDbNull(RsFinalInspMain.Fields("ITEM_CODE").Value), "", RsFinalInspMain.Fields("ITEM_CODE").Value)
            txtPartNo_Validating(txtPartNo, New System.ComponentModel.CancelEventArgs(False))
            txtInspectionSTD.Text = IIf(IsDbNull(RsFinalInspMain.Fields("AUTO_KEY_STD").Value), "", RsFinalInspMain.Fields("AUTO_KEY_STD").Value)
            txtCustomer.Text = IIf(IsDbNull(RsFinalInspMain.Fields("SUPP_CUST_CODE").Value), "", RsFinalInspMain.Fields("SUPP_CUST_CODE").Value)
            txtCustomer_Validating(txtCustomer, New System.ComponentModel.CancelEventArgs(False))
            txtLotSize.Text = IIf(IsDbNull(RsFinalInspMain.Fields("LOT_SIZE").Value), "", RsFinalInspMain.Fields("LOT_SIZE").Value)
            txtSampleSize.Text = IIf(IsDbNull(RsFinalInspMain.Fields("SAMPLE_SIZE").Value), "", RsFinalInspMain.Fields("SAMPLE_SIZE").Value)
            txtRemarks.Text = IIf(IsDbNull(RsFinalInspMain.Fields("REMARKS").Value), "", RsFinalInspMain.Fields("REMARKS").Value)
            txtStatus.Text = IIf(IsDbNull(RsFinalInspMain.Fields("ACCEPT_STATUS").Value), "", RsFinalInspMain.Fields("ACCEPT_STATUS").Value)
            txtInspBy.Text = IIf(IsDbNull(RsFinalInspMain.Fields("INSPECTED_BY").Value), "", RsFinalInspMain.Fields("INSPECTED_BY").Value)
            txtInspBy_Validating(txtInspBy, New System.ComponentModel.CancelEventArgs(False))
            TxtInvoiceNo.Text = IIf(IsDbNull(RsFinalInspMain.Fields("INVOICE_NO").Value), "", RsFinalInspMain.Fields("INVOICE_NO").Value)
            txtInvoiceNo_Validating(txtInvoiceNo, New System.ComponentModel.CancelEventArgs((False)))
            Call ShowDetail1()
            Call MakeEnableDesableField(False)
            IsShowing = False
        End If
        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = True    '' False Sandeep 15/05/2022
        txtSlipNo.Enabled = True
        cmdSearchSlipNo.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsFinalInspMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
        SqlStr = " SELECT * " & vbCrLf & " FROM QAL_FINAL_DET " & vbCrLf & " WHERE AUTO_KEY_FINAL=" & Val(lblMkey.Text) & "" & vbCrLf & " ORDER BY SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsFinalInspDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsFinalInspDetail
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

                SprdMain.Col = ColCategory
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("CAT_DESC").Value), "", .Fields("CAT_DESC").Value))

                SprdMain.Col = ColClass
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("CLASS_DESC").Value), "", .Fields("CLASS_DESC").Value))

                SprdMain.Col = ColObserv1
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("OBSERV_1").Value), "", .Fields("OBSERV_1").Value))

                SprdMain.Col = ColObserv2
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("OBSERV_2").Value), "", .Fields("OBSERV_2").Value))

                SprdMain.Col = ColObserv3
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("OBSERV_3").Value), "", .Fields("OBSERV_3").Value))

                SprdMain.Col = ColObserv4
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("OBSERV_4").Value), "", .Fields("OBSERV_4").Value))

                SprdMain.Col = ColObserv5
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("OBSERV_5").Value), "", .Fields("OBSERV_5").Value))

                SprdMain.Col = ColRemarks
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value))

                .MoveNext()
                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With
        FormatSprdMain(-1)
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
        Dim xMkey As Double
        Dim mSlipNo As Double
        Dim SqlStr As String

        If Trim(txtSlipNo.Text) = "" Then GoTo EventExitSub

        If Len(Trim(txtSlipNo.Text)) < 6 Then
            txtSlipNo.Text = Trim(txtSlipNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If
        mSlipNo = Val(txtSlipNo.Text)

        If MODIFYMode = True And RsFinalInspMain.BOF = False Then xMkey = RsFinalInspMain.Fields("AUTO_KEY_FINAL").Value

        SqlStr = "SELECT * FROM QAL_FINAL_HDR " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_FINAL,LENGTH(AUTO_KEY_FINAL)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_FINAL=" & mSlipNo & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsFinalInspMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsFinalInspMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Number. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM QAL_FINAL_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_FINAL,LENGTH(AUTO_KEY_FINAL)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_FINAL=" & Val(CStr(xMkey)) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsFinalInspMain, ADODB.LockTypeEnum.adLockReadOnly)
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
        cmdSearchPartNo.Enabled = mMode
        txtInspectionSTD.Enabled = False
        txtCustomer.Enabled = mMode
        CmdSearchCustomer.Enabled = mMode
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

    Private Sub ReportOnFinalInsp(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mTitle As String

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        mTitle = "INSPECTION REPORT OF FINAL INSPECTION"
        SqlStr = "SELECT QAL_FINAL_HDR.*,QAL_FINAL_DET.*, " & vbCrLf & " INV_ITEM_MST.*,FIN_SUPP_CUST_MST.*, " & vbCrLf & " PAY_EMPLOYEE_MST.EMP_NAME " & vbCrLf & " FROM QAL_FINAL_HDR,QAL_FINAL_DET,  " & vbCrLf & " INV_ITEM_MST,FIN_SUPP_CUST_MST,PAY_EMPLOYEE_MST " & vbCrLf & " WHERE QAL_FINAL_HDR.AUTO_KEY_FINAL=QAL_FINAL_DET.AUTO_KEY_FINAL " & vbCrLf & " AND QAL_FINAL_HDR.COMPANY_CODE=INV_ITEM_MST.COMPANY_CODE(+) " & vbCrLf & " AND QAL_FINAL_HDR.ITEM_CODE=INV_ITEM_MST.ITEM_CODE(+) " & vbCrLf & " AND QAL_FINAL_HDR.COMPANY_CODE=FIN_SUPP_CUST_MST.COMPANY_CODE(+) " & vbCrLf & " AND QAL_FINAL_HDR.SUPP_CUST_CODE=FIN_SUPP_CUST_MST.SUPP_CUST_CODE(+) " & vbCrLf & " AND QAL_FINAL_HDR.COMPANY_CODE=PAY_EMPLOYEE_MST.COMPANY_CODE (+) " & vbCrLf & " AND QAL_FINAL_HDR.INSPECTED_BY=PAY_EMPLOYEE_MST.EMP_CODE (+) " & vbCrLf & " AND QAL_FINAL_HDR.AUTO_KEY_FINAL=" & Val(lblMkey.Text) & " ORDER BY SERIAL_NO "

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\InspecRepFinal.rpt"

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
        Call ReportOnFinalInsp(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnFinalInsp(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
End Class
