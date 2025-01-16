Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmDeviation
    Inherits System.Windows.Forms.Form
    Dim RsDeviationMain As ADODB.Recordset
    Dim RsDeviationDetail As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

    Dim IsShowing As Boolean

    Private Const ConRowHeight As Short = 14

    Private Const ColControlDimen As Short = 1
    Private Const ColActualDimen As Short = 2
    Private Const ColReasonDefect As Short = 3
    Private Const ColCorrective As Short = 4
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
            If RsDeviationMain.EOF = False Then RsDeviationMain.MoveFirst()
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
        If Not RsDeviationMain.EOF Then
            If RsDeviationMain.Fields("MGR_EMP_CODE").Value <> "" Then MsgBox("Number been approved, So cann't be Deleted ") : Exit Sub
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "QAL_DEVIATION_HDR", (txtSlipNo.Text), RsDeviationMain) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM QAL_DEVIATION_DET WHERE AUTO_KEY_DEV=" & Val(lblMkey.Text) & "")
                PubDBCn.Execute("DELETE FROM QAL_DEVIATION_HDR WHERE AUTO_KEY_DEV=" & Val(lblMkey.Text) & "")
                PubDBCn.CommitTrans()
                RsDeviationMain.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
        RsDeviationMain.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr


        If CmdModify.Text = ConcmdmodifyCaption Then
            If RsDeviationMain.Fields("MGR_EMP_CODE").Value <> "" Then MsgBox("Number been approved, So cann't be modified") : Exit Sub
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsDeviationMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
            SqlStr = " INSERT INTO QAL_DEVIATION_HDR " & vbCrLf _
                            & " (AUTO_KEY_DEV,COMPANY_CODE," & vbCrLf _
                            & " DOC_DATE,SUPP_CUST_CODE,DEPT_CODE,ITEM_CODE," & vbCrLf _
                            & " DEV_QTY,OPR_CODE,REQ_DATE,DEV_PARAM,PRE_DET_REF, " & vbCrLf _
                            & " DEF_DESC,DEF_QTY,DEF_DATE,COMMENTS,SUP_EMP_CODE, " & vbCrLf _
                            & " GRANTED_FLAG,HOD_EMP_CODE,MGR_EMP_CODE,NO_OF_SETS, " & vbCrLf _
                            & " ADDUSER, ADDDATE, MODUSER, MODDATE) " & vbCrLf _
                            & " VALUES ( " & vbCrLf _
                            & " " & mSlipNo & "," & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(txtCustomer.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtShop.Text) & "','" & MainClass.AllowSingleQuote(txtComponent.Text) & "', " & vbCrLf _
                            & " " & Val(txtDevQuantity.Text) & ",'" & MainClass.AllowSingleQuote(txtOperation.Text) & "', " & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtRequestDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(txtDevParam.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtPrevDevRef.Text) & "','" & MainClass.AllowSingleQuote(txtDefect.Text) & "', " & vbCrLf _
                            & " " & Val(txtDefQuantity.Text) & ",TO_DATE('" & vb6.Format(txtDefDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtComment.Text) & "','" & MainClass.AllowSingleQuote(txtSuperviser.Text) & "', " & vbCrLf _
                            & " '" & VB.Left(cboGranted.Text, 1) & "','" & MainClass.AllowSingleQuote(txtHOD.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtQCManager.Text) & "','" & MainClass.AllowSingleQuote(txtNoSets.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"
        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE QAL_DEVIATION_HDR SET " & vbCrLf _
                    & " AUTO_KEY_DEV=" & mSlipNo & ",COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf _
                    & " DOC_DATE=TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCustomer.Text) & "', " & vbCrLf _
                    & " DEPT_CODE='" & MainClass.AllowSingleQuote(txtShop.Text) & "',ITEM_CODE='" & MainClass.AllowSingleQuote(txtComponent.Text) & "', " & vbCrLf _
                    & " DEV_QTY=" & Val(txtDevQuantity.Text) & ",OPR_CODE='" & MainClass.AllowSingleQuote(txtOperation.Text) & "', " & vbCrLf _
                    & " REQ_DATE=TO_DATE('" & vb6.Format(txtRequestDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),DEV_PARAM='" & MainClass.AllowSingleQuote(txtDevParam.Text) & "', " & vbCrLf _
                    & " PRE_DET_REF='" & MainClass.AllowSingleQuote(txtPrevDevRef.Text) & "', " & vbCrLf _
                    & " DEF_DESC='" & MainClass.AllowSingleQuote(txtDefect.Text) & "',DEF_QTY=" & Val(txtDefQuantity.Text) & ", " & vbCrLf _
                    & " DEF_DATE=TO_DATE('" & vb6.Format(txtDefDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),COMMENTS='" & MainClass.AllowSingleQuote(txtComment.Text) & "', " & vbCrLf _
                    & " SUP_EMP_CODE='" & MainClass.AllowSingleQuote(txtSuperviser.Text) & "', " & vbCrLf _
                    & " GRANTED_FLAG='" & VB.Left(cboGranted.Text, 1) & "',HOD_EMP_CODE='" & MainClass.AllowSingleQuote(txtHOD.Text) & "', " & vbCrLf _
                    & " MGR_EMP_CODE='" & MainClass.AllowSingleQuote(txtQCManager.Text) & "', " & vbCrLf _
                    & " NO_OF_SETS='" & MainClass.AllowSingleQuote(txtNoSets.Text) & "', " & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                    & " MODDATE=TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                    & " AND AUTO_KEY_DEV =" & Val(lblMkey.Text) & ""
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
        RsDeviationMain.Requery()
        RsDeviationDetail.Requery()
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
        SqlStr = "SELECT Max(AUTO_KEY_DEV)  " & vbCrLf & " FROM QAL_DEVIATION_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_DEV,LENGTH(AUTO_KEY_DEV)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

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
        Dim mControlDimen As String
        Dim mActualDimen As String
        Dim mReasonDefect As String
        Dim mCorrective As String


        PubDBCn.Execute("DELETE FROM QAL_DEVIATION_DET WHERE AUTO_KEY_DEV=" & Val(lblMkey.Text) & "")

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColControlDimen
                mControlDimen = MainClass.AllowSingleQuote(.Text)

                .Col = ColActualDimen
                mActualDimen = MainClass.AllowSingleQuote(.Text)

                .Col = ColReasonDefect
                mReasonDefect = MainClass.AllowSingleQuote(.Text)

                .Col = ColCorrective
                mCorrective = MainClass.AllowSingleQuote(.Text)

                SqlStr = ""

                If mControlDimen <> "" Then
                    SqlStr = " INSERT INTO  QAL_DEVIATION_DET ( " & vbCrLf & " AUTO_KEY_DEV,SERIAL_NO,CONTROL_DIM,ACTUAL_DIM, " & vbCrLf & " DEF_REASON,CORR_ACTION ) " & vbCrLf & " VALUES ( " & vbCrLf & " " & Val(lblMkey.Text) & "," & I & ",'" & mControlDimen & "','" & mActualDimen & "', " & vbCrLf & " '" & mReasonDefect & "','" & mCorrective & "') "
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

    Private Sub cmdSearchComp_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchComp.Click
        On Error GoTo CompERR
        Dim SqlStr As String
        SqlStr = "SELECT A.ITEM_CODE, B.ITEM_SHORT_DESC, B.ITEM_MODEL " & vbCrLf _
                & " FROM FIN_SUPP_CUST_DET A ,INV_ITEM_MST B  " & vbCrLf _
                & " WHERE A.COMPANY_CODE =B.COMPANY_CODE " & vbCrLf _
                & " AND A.ITEM_CODE =  B.ITEM_CODE " & vbCrLf _
                & " AND A.COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                & " AND A.SUPP_CUST_CODE ='" & MainClass.AllowSingleQuote(txtCustomer.Text) & "' " & vbCrLf _
                & " ORDER BY A.ITEM_CODE   "
        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtComponent.Text = AcName
            lblComponent.text = AcName1
            txtComponent_Validating(txtComponent, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
        End If
        Exit Sub
CompERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdSearchCustomer_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearchCustomer.Click
        Dim SqlStr As String
        SqlStr = "SELECT DISTINCT A.SUPP_CUST_NAME, A.SUPP_CUST_CODE " & vbCrLf & " FROM FIN_SUPP_CUST_MST A, FIN_SUPP_CUST_DET B " & vbCrLf & " WHERE A.COMPANY_CODE = B.COMPANY_CODE " & vbCrLf & " AND A.SUPP_CUST_CODE = B.SUPP_CUST_CODE " & vbCrLf & " AND A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " ORDER BY A.SUPP_CUST_CODE "
        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtCustomer.Text = AcName1
            lblCustomer.text = AcName
            txtCustomer_Validating(txtCustomer, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
        End If
    End Sub

    Private Sub cmdSearchHOD_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchHOD.Click
        Call SearchEmp(txtHOD, lblHOD)
    End Sub

    Private Sub cmdSearchOperation_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchOperation.Click
        On Error GoTo SrchERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster("", "PRD_OPR_MST", "OPR_DESC", "OPR_CODE", , , SqlStr) = True Then
            txtOperation.Text = AcName1
            lblOperation.text = AcName
            If txtOperation.Enabled = True Then txtOperation.Focus()
        End If
        Exit Sub
SrchERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchQCMan_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchQCMan.Click
        Call SearchEmp(txtQCManager, lblQCManager)
    End Sub

    Private Sub cmdSearchShop_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchShop.Click
        On Error GoTo SrchERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , SqlStr) = True Then
            txtShop.Text = AcName1
            lblShop.text = AcName
            If txtShop.Enabled = True Then txtShop.Focus()
        End If
        Exit Sub
SrchERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchSuper_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchSuper.Click
        Call SearchEmp(txtSuperviser, lblSuperviser)
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
    Private Sub cmdSearchSlipNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchSlipNo.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_DEV,LENGTH(AUTO_KEY_DEV)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
        If MainClass.SearchGridMaster(txtSlipNo.Text, "QAL_DEVIATION_HDR", "AUTO_KEY_DEV", "DOC_DATE", "ITEM_CODE", "SUPP_CUST_CODE", SqlStr) = True Then
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
        MainClass.ButtonStatus(Me, XRIGHT, RsDeviationMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmDeviation_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Deviation Format"

        SqlStr = "Select * From QAL_DEVIATION_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDeviationMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From QAL_DEVIATION_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDeviationDetail, ADODB.LockTypeEnum.adLockReadOnly)

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

        SqlStr = " SELECT " & vbCrLf & " AUTO_KEY_DEV AS SLIP_NUMBER,TO_CHAR(DOC_DATE,'DD/MM/YYYY') AS DOC_DATE, " & vbCrLf & " SUPP_CUST_CODE,DEPT_CODE,ITEM_CODE,DEV_QTY,  " & vbCrLf & " OPR_CODE,TO_CHAR(REQ_DATE,'DD/MM/YYYY') AS REQ_DATE " & vbCrLf & " FROM QAL_DEVIATION_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_DEV,LENGTH(AUTO_KEY_DEV)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " ORDER BY AUTO_KEY_DEV"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmDeviation_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmDeviation_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Left = 0
        Me.Top = 0
        Me.Height = VB6.TwipsToPixelsY(8010)
        Me.Width = VB6.TwipsToPixelsX(10755)
        cboGranted.Items.Add("Yes")
        cboGranted.Items.Add("No")
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
        txtCustomer.Text = ""
        lblCustomer.Text = ""
        txtShop.Text = ""
        lblShop.Text = ""
        txtComponent.Text = ""
        lblComponent.Text = ""
        lblModel.Text = ""
        txtDevQuantity.Text = ""
        txtOperation.Text = ""
        lblOperation.Text = ""
        txtRequestDate.Text = ""
        txtDevParam.Text = ""
        txtPrevDevRef.Text = ""
        txtDefect.Text = ""
        txtDefQuantity.Text = ""
        txtDefDate.Text = ""
        txtComment.Text = ""
        txtSuperviser.Text = ""
        lblSuperviser.Text = ""
        txtHOD.Text = ""
        lblHOD.Text = ""
        txtQCManager.Text = ""
        lblQCManager.Text = ""
        cboGranted.SelectedIndex = 0
        txtNoSets.Text = ""
        Call MakeEnableDesableField(True)
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsDeviationMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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

            .Col = ColControlDimen
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsDeviationDetail.Fields("CONTROL_DIM").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColActualDimen
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsDeviationDetail.Fields("ACTUAL_DIM").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColReasonDefect
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsDeviationDetail.Fields("DEF_REASON").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True

            .Col = ColCorrective
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsDeviationDetail.Fields("CORR_ACTION").DefinedSize
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
            .set_ColWidth(3, 500 * 2)
            .set_ColWidth(4, 500 * 2)
            .set_ColWidth(5, 500 * 2)
            .set_ColWidth(6, 500 * 2)
            .set_ColWidth(7, 500 * 2)
            .set_ColWidth(8, 500 * 3)
            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1

        txtSlipNo.Maxlength = RsDeviationMain.Fields("AUTO_KEY_DEV").Precision
        txtDate.Maxlength = RsDeviationMain.Fields("DOC_DATE").DefinedSize - 6
        txtCustomer.Maxlength = RsDeviationMain.Fields("SUPP_CUST_CODE").DefinedSize
        txtShop.Maxlength = RsDeviationMain.Fields("DEPT_CODE").DefinedSize
        txtComponent.Maxlength = RsDeviationMain.Fields("ITEM_CODE").DefinedSize
        txtDevQuantity.Maxlength = RsDeviationMain.Fields("DEV_QTY").Precision
        txtOperation.Maxlength = RsDeviationMain.Fields("OPR_CODE").DefinedSize
        txtRequestDate.Maxlength = RsDeviationMain.Fields("REQ_DATE").DefinedSize - 6
        txtDevParam.Maxlength = RsDeviationMain.Fields("DEV_PARAM").DefinedSize
        txtPrevDevRef.Maxlength = RsDeviationMain.Fields("PRE_DET_REF").DefinedSize
        txtDefect.Maxlength = RsDeviationMain.Fields("DEF_DESC").DefinedSize
        txtDefQuantity.Maxlength = RsDeviationMain.Fields("DEF_QTY").Precision
        txtDefDate.Maxlength = RsDeviationMain.Fields("DEF_DATE").DefinedSize - 6
        txtComment.Maxlength = RsDeviationMain.Fields("COMMENTS").DefinedSize
        txtSuperviser.Maxlength = RsDeviationMain.Fields("SUP_EMP_CODE").DefinedSize
        txtHOD.Maxlength = RsDeviationMain.Fields("HOD_EMP_CODE").DefinedSize
        txtQCManager.Maxlength = RsDeviationMain.Fields("MGR_EMP_CODE").DefinedSize
        txtNoSets.Maxlength = RsDeviationMain.Fields("NO_OF_SETS").DefinedSize

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
        If MODIFYMode = True And RsDeviationMain.EOF = True Then Exit Function

        If Trim(txtDate.Text) = "" Then
            MsgInformation("Date is empty, So unable to save.")
            txtDate.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtCustomer.Text) = "" Then
            MsgInformation("Customer is empty, So unable to save.")
            txtCustomer.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtShop.Text) = "" Then
            MsgInformation("Shop Section is empty, So unable to save.")
            txtShop.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtComponent.Text) = "" Then
            MsgInformation("Component is empty, So unable to save.")
            txtComponent.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Val(txtDevQuantity.Text) = 0 Then
            MsgInformation("Deviation Quantity is empty, So unable to save.")
            txtDevQuantity.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtOperation.Text) = "" Then
            MsgInformation("Operation is empty, So unable to save.")
            txtOperation.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtSuperviser.Text) = "" Then
            MsgInformation("Superviser is empty, So unable to save.")
            txtSuperviser.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtHOD.Text) = "" Then
            MsgInformation("H.O.D. is empty, So unable to save.")
            txtHOD.Focus()
            FieldsVarification = False
            Exit Function
        End If

        '    If Trim(txtQCManager.Text) = "" Then
        '        MsgInformation "Q.C. Manager is empty, So unable to save."
        '        txtQCManager.SetFocus
        '        FieldsVarification = False
        '        Exit Function
        '    End If

        If MainClass.ValidDataInGrid(SprdMain, ColControlDimen, "S", "Please Check Control Dimension.") = False Then FieldsVarification = False

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        'Resume
    End Function

    Private Sub frmDeviation_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        RsDeviationMain.Close()
        RsDeviationMain = Nothing
        RsDeviationDetail.Close()
        RsDeviationDetail = Nothing
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
            MainClass.DeleteSprdRow(SprdMain, eventArgs.Row, ColControlDimen)
            MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
        End If
    End Sub
    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim xParamDesc As String

        If eventArgs.NewRow = -1 Then Exit Sub


        SprdMain.Row = SprdMain.ActiveRow
        SprdMain.Col = ColControlDimen
        xParamDesc = Trim(SprdMain.Text)
        If xParamDesc = "" Then Exit Sub

        Select Case eventArgs.col
            Case ColControlDimen
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColControlDimen
                xParamDesc = Trim(SprdMain.Text)
                If xParamDesc = "" Then Exit Sub
                MainClass.AddBlankSprdRow(SprdMain, ColControlDimen, ConRowHeight)
                FormatSprdMain((SprdMain.MaxRows))
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

    Private Sub txtComment_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtComment.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtComponent_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtComponent.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtComponent_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtComponent.DoubleClick
        Call cmdSearchComp_Click(cmdSearchComp, New System.EventArgs())
    End Sub

    Private Sub txtComponent_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtComponent.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchComp_Click(cmdSearchComp, New System.EventArgs())
    End Sub

    Private Sub txtComponent_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtComponent.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim SqlStr As String
        Dim mRsTemp As ADODB.Recordset
        If Trim(txtComponent.Text) = "" Then GoTo EventExitSub
        SqlStr = " SELECT A.ITEM_CODE,B.ITEM_SHORT_DESC,B.ITEM_MODEL " & vbCrLf _
                & " FROM FIN_SUPP_CUST_DET A,INV_ITEM_MST B  " & vbCrLf _
                & " WHERE A.COMPANY_CODE = B.COMPANY_CODE " & vbCrLf _
                & " AND A.ITEM_CODE = B.ITEM_CODE " & vbCrLf _
                & " AND A.COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                & " AND A.ITEM_CODE ='" & MainClass.AllowSingleQuote(txtComponent.Text) & "' " & vbCrLf _
                & " AND A.SUPP_CUST_CODE ='" & MainClass.AllowSingleQuote(txtCustomer.Text) & "' "


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With mRsTemp
            If Not .EOF Then
                txtComponent.Text = IIf(IsDbNull(mRsTemp.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                lblComponent.Text = IIf(IsDbNull(mRsTemp.Fields("Item_Short_Desc").Value), "", .Fields("Item_Short_Desc").Value)
                lblModel.Text = IIf(IsDbNull(mRsTemp.Fields("ITEM_MODEL").Value), "", .Fields("ITEM_MODEL").Value)
            Else
                MsgBox("Not a valid Customer's Product.")
                txtComponent.Text = ""
                lblComponent.Text = ""
                lblModel.Text = ""
                Cancel = True
            End If
        End With
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

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

    Private Sub txtCustomer_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustomer.Leave
        If Trim(txtCustomer.Text) = "" Then Exit Sub
        txtComponent.Focus()
    End Sub

    Private Sub txtCustomer_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCustomer.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim SqlStr As String
        Dim mRsTemp As ADODB.Recordset
        If Trim(txtCustomer.Text) = "" Then GoTo EventExitSub

        SqlStr = " SELECT DISTINCT A.SUPP_CUST_NAME,A.SUPP_CUST_CODE " & vbCrLf _
                    & " FROM FIN_SUPP_CUST_MST A, FIN_SUPP_CUST_DET B " & vbCrLf _
                    & " WHERE A.COMPANY_CODE = B.COMPANY_CODE " & vbCrLf _
                    & " AND A.SUPP_CUST_CODE = B.SUPP_CUST_CODE " & vbCrLf _
                    & " AND A.COMPANY_CODE = " & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                    & " AND A.SUPP_CUST_CODE ='" & MainClass.AllowSingleQuote(txtCustomer.Text) & "'  "


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With mRsTemp
            If Not .EOF Then
                txtCustomer.Text = IIf(IsDbNull(mRsTemp.Fields("SUPP_CUST_CODE").Value), "", .Fields("SUPP_CUST_CODE").Value)
                lblCustomer.Text = IIf(IsDbNull(mRsTemp.Fields("SUPP_CUST_NAME").Value), "", .Fields("SUPP_CUST_NAME").Value)
            Else
                MsgBox("Not a valid Customer")
                txtCustomer.Text = ""
                lblCustomer.Text = ""
                Cancel = True
            End If
        End With
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDefDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDefDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDefDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDefDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtDefDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtDefDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDefect_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDefect.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDefQuantity_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDefQuantity.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDevParam_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDevParam.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDevQuantity_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDevQuantity.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDevQuantity_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDevQuantity.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtHOD_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtHOD.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtHOD_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtHOD.DoubleClick
        Call cmdSearchHOD_Click(cmdSearchHOD, New System.EventArgs())
    End Sub

    Private Sub txtHOD_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtHOD.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchHOD_Click(cmdSearchHOD, New System.EventArgs())
    End Sub

    Private Sub txtHOD_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtHOD.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If ValidateEMP(txtHOD, lblHOD) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtNoSets_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNoSets.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOperation_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOperation.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOperation_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOperation.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtOperation.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtOperation_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtOperation.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchOperation_Click(cmdSearchOperation, New System.EventArgs())
    End Sub

    Private Sub txtOperation_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtOperation.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim SqlStr As String
        If Trim(txtOperation.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(txtOperation.Text, "OPR_CODE", "OPR_DESC", "PRD_OPR_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Operation Does Not Exist In Master.")
            Cancel = True
        Else
            lblOperation.text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPrevDevRef_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPrevDevRef.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtQCManager_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtQCManager.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtQCManager_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtQCManager.DoubleClick
        Call cmdSearchQCMan_Click(cmdSearchQCMan, New System.EventArgs())
    End Sub

    Private Sub txtQCManager_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtQCManager.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchQCMan_Click(cmdSearchQCMan, New System.EventArgs())
    End Sub

    Private Sub txtQCManager_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtQCManager.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If ValidateEMP(txtQCManager, lblQCManager) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtRequestDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRequestDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRequestDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRequestDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtRequestDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtRequestDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtShop_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtShop.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtShop_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtShop.DoubleClick
        Call cmdSearchShop_Click(cmdSearchShop, New System.EventArgs())
    End Sub

    Private Sub txtShop_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtShop.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtShop.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtShop_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtShop.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchShop_Click(cmdSearchShop, New System.EventArgs())
    End Sub

    Private Sub txtShop_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtShop.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValERR
        Dim SqlStr As String
        If Trim(txtShop.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(txtShop.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Shop/Section Does Not Exist In Master.")
            Cancel = True
        Else
            lblShop.text = MasterNo
        End If
        GoTo EventExitSub
ValERR:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSuperviser_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSuperviser.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSuperviser_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSuperviser.DoubleClick
        Call cmdSearchSuper_Click(cmdSearchSuper, New System.EventArgs())
    End Sub

    Private Sub txtSuperviser_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtSuperviser.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchSuper_Click(cmdSearchSuper, New System.EventArgs())
    End Sub

    Private Sub txtSuperviser_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSuperviser.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If ValidateEMP(txtSuperviser, lblSuperviser) = False Then Cancel = True
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

        If Not RsDeviationMain.EOF Then
            IsShowing = True
            lblMkey.Text = IIf(IsDbNull(RsDeviationMain.Fields("AUTO_KEY_DEV").Value), "", RsDeviationMain.Fields("AUTO_KEY_DEV").Value)
            txtSlipNo.Text = IIf(IsDbNull(RsDeviationMain.Fields("AUTO_KEY_DEV").Value), "", RsDeviationMain.Fields("AUTO_KEY_DEV").Value)
            txtDate.Text = IIf(IsDbNull(RsDeviationMain.Fields("DOC_DATE").Value), "", RsDeviationMain.Fields("DOC_DATE").Value)
            txtCustomer.Text = IIf(IsDbNull(RsDeviationMain.Fields("SUPP_CUST_CODE").Value), "", RsDeviationMain.Fields("SUPP_CUST_CODE").Value)
            txtCustomer_Validating(txtCustomer, New System.ComponentModel.CancelEventArgs(False))
            txtShop.Text = IIf(IsDbNull(RsDeviationMain.Fields("DEPT_CODE").Value), "", RsDeviationMain.Fields("DEPT_CODE").Value)
            txtShop_Validating(txtShop, New System.ComponentModel.CancelEventArgs(False))
            txtComponent.Text = IIf(IsDbNull(RsDeviationMain.Fields("ITEM_CODE").Value), "", RsDeviationMain.Fields("ITEM_CODE").Value)
            txtComponent_Validating(txtComponent, New System.ComponentModel.CancelEventArgs(False))
            txtDevQuantity.Text = IIf(IsDbNull(RsDeviationMain.Fields("DEV_QTY").Value), "", RsDeviationMain.Fields("DEV_QTY").Value)
            txtOperation.Text = IIf(IsDbNull(RsDeviationMain.Fields("OPR_CODE").Value), "", RsDeviationMain.Fields("OPR_CODE").Value)
            txtOperation_Validating(txtOperation, New System.ComponentModel.CancelEventArgs(False))
            txtRequestDate.Text = IIf(IsDbNull(RsDeviationMain.Fields("REQ_DATE").Value), "", RsDeviationMain.Fields("REQ_DATE").Value)
            txtDevParam.Text = IIf(IsDbNull(RsDeviationMain.Fields("DEV_PARAM").Value), "", RsDeviationMain.Fields("DEV_PARAM").Value)
            txtPrevDevRef.Text = IIf(IsDbNull(RsDeviationMain.Fields("PRE_DET_REF").Value), "", RsDeviationMain.Fields("PRE_DET_REF").Value)
            txtDefect.Text = IIf(IsDbNull(RsDeviationMain.Fields("DEF_DESC").Value), "", RsDeviationMain.Fields("DEF_DESC").Value)
            txtDefQuantity.Text = IIf(IsDbNull(RsDeviationMain.Fields("DEF_QTY").Value), "", RsDeviationMain.Fields("DEF_QTY").Value)
            txtDefDate.Text = IIf(IsDbNull(RsDeviationMain.Fields("DEF_DATE").Value), "", RsDeviationMain.Fields("DEF_DATE").Value)
            txtComment.Text = IIf(IsDbNull(RsDeviationMain.Fields("Comments").Value), "", RsDeviationMain.Fields("Comments").Value)
            txtSuperviser.Text = IIf(IsDbNull(RsDeviationMain.Fields("SUP_EMP_CODE").Value), "", RsDeviationMain.Fields("SUP_EMP_CODE").Value)
            txtSuperviser_Validating(txtSuperviser, New System.ComponentModel.CancelEventArgs(False))
            cboGranted.Text = IIf(IsDbNull(RsDeviationMain.Fields("GRANTED_FLAG").Value) Or RsDeviationMain.Fields("GRANTED_FLAG").Value = "N", "No", "Yes")
            txtHOD.Text = IIf(IsDbNull(RsDeviationMain.Fields("HOD_EMP_CODE").Value), "", RsDeviationMain.Fields("HOD_EMP_CODE").Value)
            txtHOD_Validating(txtHOD, New System.ComponentModel.CancelEventArgs(False))
            txtQCManager.Text = IIf(IsDbNull(RsDeviationMain.Fields("MGR_EMP_CODE").Value), "", RsDeviationMain.Fields("MGR_EMP_CODE").Value)
            txtQCManager_Validating(txtQCManager, New System.ComponentModel.CancelEventArgs(False))
            txtNoSets.Text = IIf(IsDbNull(RsDeviationMain.Fields("NO_OF_SETS").Value), "", RsDeviationMain.Fields("NO_OF_SETS").Value)
            Call ShowDetail1()
            Call MakeEnableDesableField(False)
            IsShowing = False
        End If
        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = True    '' False Sandeep 15/05/2022
        txtSlipNo.Enabled = True
        cmdSearchSlipNo.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsDeviationMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
        SqlStr = " SELECT * " & vbCrLf & " FROM QAL_DEVIATION_DET " & vbCrLf & " WHERE AUTO_KEY_DEV=" & Val(lblMkey.Text) & "" & vbCrLf & " ORDER BY SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDeviationDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsDeviationDetail
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            I = 1
            Do While Not .EOF
                SprdMain.Row = I

                SprdMain.Col = ColControlDimen
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("CONTROL_DIM").Value), "", .Fields("CONTROL_DIM").Value))

                SprdMain.Col = ColActualDimen
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("ACTUAL_DIM").Value), "", .Fields("ACTUAL_DIM").Value))

                SprdMain.Col = ColReasonDefect
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("DEF_REASON").Value), "", .Fields("DEF_REASON").Value))

                SprdMain.Col = ColCorrective
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("CORR_ACTION").Value), "", .Fields("CORR_ACTION").Value))

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

        If MODIFYMode = True And RsDeviationMain.BOF = False Then xMKey = RsDeviationMain.Fields("AUTO_KEY_DEV").Value

        SqlStr = "SELECT * FROM QAL_DEVIATION_HDR " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_DEV,LENGTH(AUTO_KEY_DEV)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_DEV=" & mSlipNo & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDeviationMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsDeviationMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Number. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM QAL_DEVIATION_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_DEV,LENGTH(AUTO_KEY_DEV)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_DEV=" & Val(CStr(xMKey)) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDeviationMain, ADODB.LockTypeEnum.adLockReadOnly)
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
        txtCustomer.Enabled = mMode
        CmdSearchCustomer.Enabled = mMode
        txtShop.Enabled = mMode
        cmdSearchShop.Enabled = mMode
        txtComponent.Enabled = mMode
        cmdSearchComp.Enabled = mMode
        txtDevQuantity.Enabled = mMode
        txtOperation.Enabled = mMode
        cmdSearchOperation.Enabled = mMode
        txtDefQuantity.Enabled = mMode
        txtSuperviser.Enabled = mMode
        cmdSearchSuper.Enabled = mMode
        txtHOD.Enabled = mMode
        cmdSearchHOD.Enabled = mMode
        txtQCManager.Enabled = mMode
        cmdSearchQCMan.Enabled = mMode
        cboGranted.Enabled = mMode

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
    Private Sub ReportOnDeviation(ByRef Mode As Crystal.DestinationConstants)

    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnDeviation(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnDeviation(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
End Class
