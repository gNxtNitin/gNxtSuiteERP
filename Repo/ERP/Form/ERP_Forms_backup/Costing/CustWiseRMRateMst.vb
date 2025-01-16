Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmCustWiseRMRateMst
    Inherits System.Windows.Forms.Form
    Dim RsVCMain As ADODB.Recordset ''ADODB.Recordset					
    Dim RsVCDetail As ADODB.Recordset ''ADODB.Recordset		
    Dim RsPressDetail As ADODB.Recordset ''ADODB.Recordset	
    Dim RsOPRDetail As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection					

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean
    Dim mAccountCode As String


    Private Const ConRowHeight As Short = 14

    Private Const ColGradeCode As Short = 1
    Private Const ColGradeDesc As Short = 2
    Private Const ColGradeUOM As Short = 3
    Private Const ColGradeType As Short = 4
    Private Const ColDensity As Short = 5
    Private Const ColPrevRate As Short = 6
    Private Const ColCurrRate As Short = 7
    Private Const ColDiff As Short = 8
    Private Const ColDiffPer As Short = 9
    Private Const ColScrapRate As Short = 10
    Private Const ColStatus As Short = 11

    Private Const ColPressMc As Short = 1
    Private Const ColPressTonnage As Short = 2
    Private Const ColPressOPR As Short = 3
    Private Const ColPressCost As Short = 4
    Private Const ColPressRemarks As Short = 5

    'Private Const ColOPRType As Short = 1
    Private Const ColOPRDesc As Short = 1
    Private Const ColOPRUOM As Short = 2
    Private Const ColOPRCost As Short = 3
    Private Const ColOPRRemarks As Short = 4

    Dim mAmendStatus As Boolean

    Private Sub chkCancelled_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCancelled.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        If CmdAdd.Text = ConCmdAddCaption Then
            CmdAdd.Text = ConCmdCancelCaption
            ADDMode = True
            MODIFYMode = False
            Clear1()
            TxtName.Focus()
            SprdMain.Enabled = True
            SprdPress.Enabled = True
            SprdWeld.Enabled = True
            SprdPlating.Enabled = True
            SprdPPC.Enabled = True
        Else
            CmdAdd.Text = ConCmdAddCaption
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
    End Sub

    Private Sub cmdAmend_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAmend.Click

        Dim mCustName As String
        Dim I As Integer

        mCustName = Trim(TxtName.Text)

        If mCustName = "" Then
            MsgInformation("Please Select Customer.")
            Exit Sub
        End If

        Call txtAmendNo_Validating(txtAmendNo, New System.ComponentModel.CancelEventArgs(True)) '' txtPONO_Validate(True)					


        txtAmendNo.Text = CStr(GetMaxAmendNo())

        TxtName.Enabled = False
        cmdSearch.Enabled = False
        txtWEF.Enabled = True
        mAmendStatus = True
        cmdAmend.Enabled = False

        ADDMode = True
        MODIFYMode = False
        SprdMain.Enabled = True
        SprdPress.Enabled = True
        SprdWeld.Enabled = True
        SprdPlating.Enabled = True
        SprdPPC.Enabled = True

        cmdSearchAmend.Enabled = False
        MainClass.ButtonStatus(Me, XRIGHT, RsVCMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        On Error Resume Next
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        Dim SqlStr As String
        Dim mMaxAmendNo As Integer

        If TxtName.Text = "" Then Call ErrorMsg("Nothing to Delete", "", MsgBoxStyle.Critical) : Exit Sub

        mMaxAmendNo = GetMaxAmendNo()
        mMaxAmendNo = mMaxAmendNo + IIf(mMaxAmendNo = 0, 0, -1)
        If Val(txtAmendNo.Text) < mMaxAmendNo Then
            MsgInformation("lastest Amend No is " & mMaxAmendNo & ". So cann't be deleted old Amendment.")
            Exit Sub
        End If
        If Not RsVCMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()

                If InsertIntoDelAudit(PubDBCn, "PRD_RM_GRADE_RATE_HDR", (TxtName.Text), RsVCMain, "SUPP_CUST_Code") = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "PRD_RM_GRADE_RATE_HDR", "SUPP_CUST_CODE || ':' || WEF_DATE", RsVCMain.Fields("SUPP_CUST_CODE").Value & ":" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY")) = False Then GoTo DelErrPart

                SqlStr = " DELETE From PRD_RM_GRADE_RATE_DET WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & RsVCMain.Fields("SUPP_CUST_CODE").Value & "'" & vbCrLf & " AND WEF_DATE=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                PubDBCn.Execute(SqlStr)

                SqlStr = " DELETE From PRD_RM_GRADE_RATE_HDR WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & RsVCMain.Fields("SUPP_CUST_CODE").Value & "'" & vbCrLf & " AND WEF_DATE=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

                PubDBCn.Execute(SqlStr)

                PubDBCn.CommitTrans()
                RsVCMain.Requery() ''.Refresh					
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        ''Resume					
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans() ''					
        RsVCMain.Requery() ''.Refresh					
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            SprdMain.Enabled = True
            SprdPress.Enabled = True
            SprdWeld.Enabled = True
            SprdPlating.Enabled = True
            SprdPPC.Enabled = True
            MainClass.ButtonStatus(Me, XRIGHT, RsVCMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            SprdMain.Enabled = True
            SprdPress.Enabled = True
            SprdWeld.Enabled = True
            SprdPlating.Enabled = True
            SprdPPC.Enabled = True
            Show1()
        End If
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPrint(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPrint(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportForPrint(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim mTitle As String
        Dim mSubTitle As String
        Dim SqlStr As String
        Dim mRptFileName As String


        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        mTitle = "RM Grade Rate - W.E.F. From" & VB6.Format(txtWEF.Text, "DD/MM/YYYY")
        mSubTitle = "Customer Name : " & TxtName.Text

        If MainClass.FillPrintDummyDataFromSprd(SprdMain, 1, SprdMain.MaxRows - 1, 1, ColDiffPer, PubDBCn) = False Then GoTo ERR1
        SqlStr = ""
        SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")

        mRptFileName = "CustRMGradeRate.rpt"

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mRptFileName)

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef mRptFileName As String)
        On Error GoTo ErrPart

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\" & mRptFileName

        Report1.SQLQuery = mSqlStr
        Report1.WindowShowGroupTree = False

        Report1.Action = 1
        Report1.Reset()
        Report1.ReportFileName = ""

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number))
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
            '        txtWEF_Validate False					
            txtAmendNo_Validating(txtAmendNo, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True Then CmdAdd.Focus()
        Else
            MsgInformation("Record not saved")
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        Call ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Function Update1() As Boolean
        On Error GoTo UpdateError

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        If UpdateMain1() = False Then GoTo UpdateError

        PubDBCn.CommitTrans()
        Update1 = True
        Exit Function
UpdateError:
        Call ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        PubDBCn.RollbackTrans() ''					
        Update1 = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function UpdateMain1() As Boolean

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mCancelled As String

        mCancelled = IIf(chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        If ADDMode = True Then
            mAccountCode = MainClass.AllowSingleQuote(txtCode.Text)

            SqlStr = ""
            SqlStr = " INSERT INTO PRD_RM_GRADE_RATE_HDR ( " & vbCrLf & " COMPANY_CODE, SUPP_CUST_CODE, " & vbCrLf & " WEF_DATE, CANCELLED, REMARKS, AMEND_NO, " & vbCrLf & " ADDUSER, ADDDATE, MODUSER, MODDATE) VALUES ( "


            SqlStr = SqlStr & vbCrLf & " " & RsCompany.Fields("Company_Code").Value & ", '" & mAccountCode & "', " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & mCancelled & "', '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & Val(txtAmendNo.Text) & "," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"

        End If

        If MODIFYMode = True Then
            SqlStr = ""
            SqlStr = " UPDATE PRD_RM_GRADE_RATE_HDR SET  " & vbCrLf & " AMEND_NO=" & Val(txtAmendNo.Text) & ", " & vbCrLf & " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf & " CANCELLED='" & mCancelled & "'," & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND SUPP_CUST_CODE = '" & mAccountCode & "'" & vbCrLf & " AND WEF_DATE=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        End If
        PubDBCn.Execute(SqlStr)
        If UpdateDetail1(mAccountCode) = False Then GoTo ErrPart
        If UpdateDetailPress1(mAccountCode) = False Then GoTo ErrPart
        If UpdateOPRDetail1(mAccountCode, "WLD", SprdWeld) = False Then GoTo ErrPart
        If UpdateOPRDetail1(mAccountCode, "PLT", SprdPlating) = False Then GoTo ErrPart
        If UpdateOPRDetail1(mAccountCode, "PPC", SprdPPC) = False Then GoTo ErrPart
        UpdateMain1 = True
        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateMain1 = False
        'Resume					
    End Function
    Private Function UpdateDetail1(ByRef pAccountCode As String) As Boolean

        On Error GoTo UpdateDetail1
        Dim SqlStr As String
        Dim I As Integer
        Dim mGradeCode As String
        Dim mRate As Double
        Dim mStatus As String
        Dim mScrapRate As Double

        SqlStr = "DELETE FROM  PRD_RM_GRADE_RATE_DET " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pAccountCode) & "'" & vbCrLf _
                & " AND WEF_DATE=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"


        PubDBCn.Execute(SqlStr)

        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColGradeCode
                mGradeCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColCurrRate
                mRate = Val(.Text)

                .Col = ColScrapRate
                mScrapRate = Val(.Text)


                .Col = ColStatus
                mStatus = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "C", "O")

                SqlStr = ""

                If mGradeCode <> "" Then
                    SqlStr = " INSERT INTO PRD_RM_GRADE_RATE_DET ( " & vbCrLf & " COMPANY_CODE , SUPP_CUST_CODE, " & vbCrLf & " WEF_DATE, GRADE_CODE, " & vbCrLf & " RATE, SCRAP_RATE, STATUS) "

                    SqlStr = SqlStr & vbCrLf & " VALUES ( " & vbCrLf _
                        & " '" & RsCompany.Fields("COMPANY_CODE").Value & "','" & MainClass.AllowSingleQuote(pAccountCode) & "', " & vbCrLf _
                        & " TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                        & " '" & mGradeCode & "', " & mRate & ", " & mScrapRate & ", '" & mStatus & "' ) "

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
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='" & lblType.Text & "'"

        If MainClass.SearchGridMaster(TxtName.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            TxtName.Text = AcName
            TxtName_Validating(TxtName, New System.ComponentModel.CancelEventArgs(False))
            If txtCode.Enabled = True Then txtCode.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub SearchCode()
        On Error GoTo ErrPart
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='" & lblType.Text & "'"

        If MainClass.SearchGridMaster(txtCode.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_CODE", "SUPP_CUST_NAME", , , SqlStr) = True Then
            txtCode.Text = AcName
            txtCode_Validating(txtCode, New System.ComponentModel.CancelEventArgs(False))
            txtCode.Focus()
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdSearchAmend_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchAmend.Click
        On Error GoTo ErrPart
        Dim SqlStr As String

        If Trim(txtCode.Text) = "" Then
            Exit Sub
        End If

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'"

        If MainClass.SearchGridMaster("", "PRD_RM_GRADE_RATE_HDR", "trim(TO_CHAR(AMEND_NO,'000'))", , , , SqlStr) = True Then
            txtAmendNo.Text = AcName
            txtAmendNo_Validating(txtAmendNo, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdSearchWEF_Click()
        Dim SqlStr As String

        SqlStr = "SELECT " & vbCrLf & " DISTINCT TO_CHAR(WEF_DATE,'DD/MM/YYYY') AS WEF" & vbCrLf & " FROM PRD_RM_GRADE_RATE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & Trim(txtCode.Text) & "'"

        If MainClass.SearchBySQL(SqlStr, "WEF") = True Then
            txtWEF.Text = AcName
            txtWEF.Text = Format(txtWEF.Text, "DD/MM/YYYY")
            txtWEF_Validating(txtWEF, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
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
        MainClass.ButtonStatus(Me, XRIGHT, RsVCMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmCustWiseRMRateMst_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        SqlStr = "Select * From PRD_RM_GRADE_RATE_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsVCMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From PRD_RM_GRADE_RATE_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsVCDetail, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From PRD_SUPP_PRESS_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPressDetail, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From PRD_SUPP_OPR_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOPRDetail, ADODB.LockTypeEnum.adLockReadOnly)

        AssignGrid(False)

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
        Dim SqlStr As String

        SqlStr = ""

        SqlStr = " SELECT IH.SUPP_CUST_CODE AS CODE, CMST.SUPP_CUST_NAME AS NAME, " & vbCrLf _
            & " IH.WEF_DATE, TO_CHAR(AMEND_NO,'000') As AMEND_NO," & vbCrLf _
            & " DECODE(CANCELLED,'Y','YES','NO') AS CANCELLED " & vbCrLf _
            & " FROM PRD_RM_GRADE_RATE_HDR IH, FIN_SUPP_CUST_MST CMST " & vbCrLf _
            & " WHERE IH.COMPANY_CODE=CMST.COMPANY_CODE " & vbCrLf _
            & " AND IH.SUPP_CUST_CODE=CMST.SUPP_CUST_CODE " & vbCrLf _
            & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND CMST.SUPP_CUST_TYPE='" & lblType.Text & "'"

        SqlStr = SqlStr & " ORDER BY CMST.SUPP_CUST_NAME,AMEND_NO"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmCustWiseRMRateMst_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection					
        'PvtDBCn.Open StrConn					
        '    Call SetMainFormCordinate(Me)					
        Me.Left = 0
        Me.Top = 0
        'Me.Width = VB6.TwipsToPixelsX(11355)					
        'Me.Height = VB6.TwipsToPixelsY(7245)					

        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

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

        mAccountCode = CStr(-1)
        TxtName.Text = ""
        txtCode.Text = ""
        txtWEF.Text = ""
        txtRemarks.Text = ""
        chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked


        txtAmendNo.Text = CStr(0)
        txtAmendNo.Enabled = False
        mAmendStatus = False

        txtCode.Enabled = True
        TxtName.Enabled = True
        txtWEF.Enabled = True
        chkCancelled.Enabled = True

        SprdMain.Enabled = True
        SprdPress.Enabled = True
        SprdWeld.Enabled = True
        SprdPlating.Enabled = True
        SprdPPC.Enabled = True


        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)

        MainClass.ClearGrid(SprdPress, ConRowHeight)
        FormatSprdPress(-1)

        MainClass.ClearGrid(SprdWeld, ConRowHeight)
        FormatSprdOpr(-1, SprdWeld)

        MainClass.ClearGrid(SprdPlating, ConRowHeight)
        FormatSprdOpr(-1, SprdPlating)

        MainClass.ClearGrid(SprdPPC, ConRowHeight)
        FormatSprdOpr(-1, SprdPPC)

        TabControl1.SelectedIndex = 0

        MainClass.ButtonStatus(Me, XRIGHT, RsVCMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '''Resume					
    End Sub

    Private Sub FormatSprdMain(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim cntCol As Integer

        With SprdMain
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = ColGradeCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsVCDetail.Fields("GRADE_CODE").DefinedSize ''					
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 6)

            .Col = ColGradeDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = MainClass.SetMaxLength("MTRL_DESC", "PRD_MTRL_MST", PubDBCn)
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 18)

            .Col = ColGradeType
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ALPHANUMERIC
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = MainClass.SetMaxLength("MTRL_TYPE", "PRD_MTRL_MST", PubDBCn)
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 7)

            .Col = ColGradeUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ALPHANUMERIC
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = MainClass.SetMaxLength("GRADE_UOM", "PRD_MTRL_MST", PubDBCn)
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 4)

            '        .Col = ColDensity					
            '        .CellType = SS_CELL_TYPE_EDIT					
            '        .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ALPHANUMERIC					
            '        .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE					
            '        .TypeEditLen = MainClass.SetMaxLength("MTRL_DENSITY", "PRD_MTRL_MST", PubDBCn)					
            '        .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE					
            '        .ColWidth(.Col) = 5					

            For cntCol = ColDensity To ColCurrRate
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 4
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.99")
                .TypeFloatMin = CDbl("-999999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntCol, 8)
            Next

            For cntCol = ColDiff To ColScrapRate
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalPlaces = 4
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("999999999.99")
                .TypeFloatMin = CDbl("-999999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntCol, 6)
            Next

            .Col = ColStatus
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .Value = CStr(System.Windows.Forms.CheckState.Unchecked)
            .set_ColWidth(ColStatus, 7)

            MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, ColGradeCode, ColStatus)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColGradeDesc, ColPrevRate)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColDiff, ColDiffPer)
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
            .set_RowHeight(0, 300)
            .set_ColWidth(0, 500)
            .set_ColWidth(1, 900)
            .set_ColWidth(2, 3500)
            .set_ColWidth(3, 2000)
            .set_ColWidth(4, 2000)
            .ColsFrozen = 2
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub SetTextLengths()

        On Error GoTo ERR1

        TxtName.MaxLength = MainClass.SetMaxLength("SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn)
        txtCode.MaxLength = RsVCMain.Fields("SUPP_CUST_CODE").DefinedSize
        txtWEF.MaxLength = 10
        txtRemarks.MaxLength = RsVCMain.Fields("REMARKS").DefinedSize
        txtAmendNo.MaxLength = RsVCMain.Fields("AMEND_NO").Precision

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Function FieldVarification() As Boolean
        On Error GoTo err_Renamed
        FieldVarification = True
        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldVarification = False
            Exit Function
        End If

        If Trim(txtWEF.Text) = "" Then
            MsgInformation("WEF Date is empty. Cannot Save")
            If txtWEF.Enabled = True Then txtWEF.Focus()
            FieldVarification = False
            Exit Function
        End If

        If Not IsDate(txtWEF.Text) Then
            MsgInformation("Invalid WEF Date.")
            If txtWEF.Enabled = True Then txtWEF.Focus()
            FieldVarification = False
            Exit Function
        End If

        If txtCode.Text = "" Then
            MsgInformation("Account Code is empty. Cannot Save")
            txtCode.Focus()
            FieldVarification = False
            Exit Function
        End If

        If MainClass.ValidateWithMasterTable(txtCode.Text, "SUPP_CUST_CODE", "SUPP_CUST_Name", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='" & lblType.Text & "'") = True Then
            TxtName.Text = MasterNo
        Else
            MsgBox("Invalid Account Code.", vbInformation)
            FieldVarification = False
            Exit Function
        End If

        If TxtName.Text = "" Then
            MsgInformation("Account Name is empty. Cannot Save")
            TxtName.Focus()
            FieldVarification = False
            Exit Function
        End If

        If chkCancelled.CheckState = System.Windows.Forms.CheckState.Checked And chkCancelled.Enabled = False Then
            MsgInformation("Cancelled Costing Cann't Saved")
            FieldVarification = False
            Exit Function
        End If

        If MainClass.ValidDataInGrid(SprdMain, ColGradeCode, "S", "Grade Code is must") = False Then FieldVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColGradeDesc, "S", "Grade Desc is must") = False Then FieldVarification = False : Exit Function
        '    If MainClass.ValidDataInGrid(SprdMain, ColDensity, "S", "Item UOM is must") = False Then FieldVarification = False: Exit Function					
        If MainClass.ValidDataInGrid(SprdMain, ColCurrRate, "N", "Grade Rate is must") = False Then FieldVarification = False : Exit Function
        If MainClass.ValidDataInGrid(SprdMain, ColScrapRate, "N", "Grade Scrap Rate is must") = False Then FieldVarification = False : Exit Function

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
    End Function

    Private Sub frmCustWiseRMRateMst_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        Me.Hide()
        Me.Close()
        Me.Dispose()
        RsVCMain.Close()
        'RsOpOuts.Close					
    End Sub
    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Function CheckDuplicateItem(ByRef mGradeCode As String) As Boolean

        On Error GoTo ERR1
        Dim I As Integer
        Dim mItemRept As Integer

        If mGradeCode = "" Then CheckDuplicateItem = True : Exit Function
        With SprdMain
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColGradeCode
                If UCase(.Text) = UCase(mGradeCode) Then
                    mItemRept = mItemRept + 1
                    If mItemRept > 1 Then
                        CheckDuplicateItem = True
                        MsgInformation("Duplicate Grade Code")
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColGradeCode)
                        Exit Function
                    End If
                End If
            Next
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        With SprdMain
            SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub


    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim SqlStr As String

        If eventArgs.row = 0 And eventArgs.col = ColGradeCode And SprdMain.Enabled = True Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColGradeCode
                SqlStr = GetSearchItem("Y")
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColGradeCode
                    .Text = Trim(AcName)
                    .Col = ColGradeDesc
                    .Text = Trim(AcName1)
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColGradeCode)
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColGradeDesc And SprdMain.Enabled = True Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColGradeDesc
                SqlStr = GetSearchItem("N")
                If MainClass.SearchGridMasterBySQL2(.Text, SqlStr) = True Then
                    .Row = .ActiveRow
                    .Col = ColGradeDesc
                    .Text = Trim(AcName)
                    .Col = ColGradeCode
                    .Text = Trim(AcName1)
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColGradeCode)
            End With
        End If

        If eventArgs.col = 0 And eventArgs.row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColGradeDesc)
            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Function GetSearchItem(ByRef mByCode As String) As String
        On Error GoTo ErrPart
        Dim mSqlStr As String

        If mByCode = "Y" Then
            mSqlStr = "SELECT A.MTRL_CODE,A.MTRL_DESC, A.MTRL_TYPE "
        Else
            mSqlStr = "SELECT A.MTRL_DESC,A.MTRL_CODE, A.MTRL_TYPE "
        End If


        mSqlStr = mSqlStr & vbCrLf & " FROM PRD_MTRL_MST A" & vbCrLf & " WHERE A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If mByCode = "Y" Then
            mSqlStr = mSqlStr & vbCrLf & "ORDER BY A.MTRL_CODE "
        Else
            mSqlStr = mSqlStr & vbCrLf & "ORDER BY A.MTRL_DESC"
        End If

        GetSearchItem = mSqlStr
        Exit Function
ErrPart:
        GetSearchItem = ""

    End Function



    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColGradeCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColGradeCode, 0))
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColGradeDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColGradeDesc, 0))
        SprdMain.Refresh()
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim xICode As String

        If eventArgs.newRow = -1 Then Exit Sub

        Select Case eventArgs.col
            Case ColGradeCode
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColGradeCode
                xICode = SprdMain.Text
                If xICode = "" Then Exit Sub

                If GetValidItem(xICode) = True Then
                    If CheckDuplicateItem(xICode) = False Then
                        If FillGridRow(xICode) = False Then Exit Sub
                        '                    FormatSprdMain Row					
                        '                MainClass.setfocusToCell SprdMain, Row, ColItemRate					
                    End If
                Else
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColGradeCode)
                End If

            Case ColCurrRate
                If CheckItemRate(SprdMain, ColGradeCode, ColCurrRate) = True Then
                    MainClass.AddBlankSprdRow(SprdMain, ColGradeCode, ConRowHeight)
                    FormatSprdMain((SprdMain.MaxRows))
                End If
        End Select

        Call CalcGrid()
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function GetValidItem(ByRef pItemCode As String) As Boolean

        On Error GoTo ErrPart
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset

        mSqlStr = "SELECT MTRL_CODE " & vbCrLf & " FROM PRD_MTRL_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MTRL_CODE='" & pItemCode & "'"
        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetValidItem = True
        Else
            MsgInformation("Please Check RM Grade Code in Master.")
            GetValidItem = False
        End If

        Exit Function
ErrPart:
        GetValidItem = False
    End Function

    Private Function CheckItemRate(ByRef pSprd As AxFPSpreadADO.AxfpSpread, pCheckCol As Long, pRateCol As Long) As Boolean
        On Error GoTo ERR1
        With pSprd
            .Row = .ActiveRow
            .Col = pCheckCol
            If Trim(.Text) = "" Then Exit Function

            .Col = pRateCol
            If Val(.Text) > 0 Then
                CheckItemRate = True
                '        Else					
                '            MsgInformation "Please Enter the Rate."					
                '            MainClass.setfocusToCell SprdMain, .ActiveRow, ColCurrRate					
            End If
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function FillGridRow(ByRef mGradeCode As String) As Boolean

        On Error GoTo ERR1
        Dim RsMisc As ADODB.Recordset
        Dim SqlStr As String

        If mGradeCode = "" Then Exit Function


        SqlStr = ""
        SqlStr = " Select MTRL_CODE,MTRL_DESC,MTRL_DENSITY, MTRL_TYPE, GRADE_UOM" & vbCrLf & " FROM PRD_MTRL_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MTRL_CODE='" & Trim(mGradeCode) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsMisc, ADODB.LockTypeEnum.adLockReadOnly)
        If RsMisc.EOF = False Then
            SprdMain.Row = SprdMain.ActiveRow
            With RsMisc

                SprdMain.Col = ColGradeDesc
                SprdMain.Text = IIf(IsDBNull(.Fields("MTRL_DESC").Value), "", .Fields("MTRL_DESC").Value)

                SprdMain.Col = ColGradeUOM
                SprdMain.Text = IIf(IsDBNull(.Fields("GRADE_UOM").Value), "", .Fields("GRADE_UOM").Value)

                SprdMain.Col = ColGradeType
                SprdMain.Text = IIf(IsDBNull(.Fields("MTRL_TYPE").Value), "", .Fields("MTRL_TYPE").Value)

                SprdMain.Col = ColDensity
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("MTRL_DENSITY").Value), "", .Fields("MTRL_DENSITY").Value), "0.0000")

            End With
            FillGridRow = True
        Else
            MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColGradeCode)
            FillGridRow = False
        End If

        Exit Function
ERR1:
        FillGridRow = False
        MsgBox(Err.Description)
    End Function
    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        If eventArgs.row < 1 Then Exit Sub

        SprdView.Row = eventArgs.row
        SprdView.Col = 1
        txtCode.Text = Trim(SprdView.Text)

        SprdView.Col = 2
        TxtName.Text = Trim(SprdView.Text)
        '    TxtName_Validate True					

        SprdView.Col = 3
        txtWEF.Text = VB6.Format(SprdView.Text, "DD/MM/YYYY")


        SprdView.Col = 4
        txtAmendNo.Text = SprdView.Text
        txtAmendNo_Validating(txtAmendNo, New System.ComponentModel.CancelEventArgs(True))

        CmdView_Click(CmdView, New System.EventArgs())
    End Sub
    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdMain.ActiveCol, SprdMain.ActiveRow))
    End Sub

    Private Sub txtAmendNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAmendNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAmendNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAmendNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtAmendNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAmendNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim SqlStr As String

        If Trim(txtCode.Text) = "" Then
            MsgInformation("Customer Code Is Blank")
            txtCode.Focus()
            GoTo EventExitSub
        End If

        If MODIFYMode = True And RsVCMain.EOF = False Then mAccountCode = RsVCMain.Fields("SUPP_CUST_CODE").Value

        SqlStr = " SELECT * FROM PRD_RM_GRADE_RATE_HDR " & vbCrLf _
        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'"


        SqlStr = SqlStr & vbCrLf & " AND AMEND_NO=" & Val(txtAmendNo.Text) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsVCMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsVCMain.EOF = False Then
            Clear1()
            Call Show1()
            If txtWEF.Enabled = True Then txtWEF.Focus()
        Else
            SqlStr = " SELECT * FROM PRD_RM_GRADE_RATE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'"

            SqlStr = SqlStr & vbCrLf & " AND AMEND_NO = (" & vbCrLf & " SELECT MAX(AMEND_NO) FROM PRD_RM_GRADE_RATE_HDR " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "')"


            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsVCMain, ADODB.LockTypeEnum.adLockReadOnly)

            If RsVCMain.EOF = False Then
                Clear1()
                Call Show1()
                If txtWEF.Enabled = True Then txtWEF.Focus()
            Else
                If ADDMode = False And MODIFYMode = False Then
                    MsgBox("No Such Month, Use add Button to Generate New Costing.", MsgBoxStyle.Information)
                    Cancel = True
                    GoTo EventExitSub
                ElseIf MODIFYMode = True Then
                    SqlStr = "SELECT * FROM PRD_RM_GRADE_RATE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'"
                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsVCMain, ADODB.LockTypeEnum.adLockReadOnly)
                    GoTo EventExitSub
                End If
            End If
        End If

        CalcTots()
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCode.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCode.DoubleClick
        Call SearchCode()
    End Sub
    Private Sub txtCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then SearchCode()
    End Sub
    Private Sub txtCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xWEF As String
        Dim SqlStr As String

        If Trim(txtCode.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(Trim(txtCode.Text), "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='" & lblType.Text & "'") = True Then
            TxtName.Text = MasterNo
        End If

        If Trim(TxtName.Text) = "" Then
            MsgInformation("Customer Name Is Blank")
            TxtName.Focus()
            GoTo EventExitSub
        End If


        If MODIFYMode = True And RsVCMain.EOF = False Then mAccountCode = RsVCMain.Fields("SUPP_CUST_CODE").Value

        SqlStr = " SELECT * FROM PRD_RM_GRADE_RATE_HDR " & vbCrLf _
        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'"

        SqlStr = SqlStr & vbCrLf & " AND AMEND_NO = (" & vbCrLf _
        & " SELECT MAX(AMEND_NO) FROM PRD_RM_GRADE_RATE_HDR " _
        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsVCMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsVCMain.EOF = False Then
            Clear1()
            Call Show1()
        Else

            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Month, Use add Button to Generate New Costing.", MsgBoxStyle.Information)
                Cancel = True
                GoTo EventExitSub
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM PRD_RM_GRADE_RATE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsVCMain, ADODB.LockTypeEnum.adLockReadOnly)
                GoTo EventExitSub
            End If

        End If

        CalcTots()
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub TxtName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub TxtName_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtName.DoubleClick
        cmdsearch_Click(cmdSearch, New System.EventArgs())
    End Sub
    Private Sub TxtName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub TxtName_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtName.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdsearch_Click(cmdSearch, New System.EventArgs())
    End Sub
    Private Sub TxtName_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtName.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xWEF As String
        Dim SqlStr As String

        If Trim(TxtName.Text) = "" Then GoTo EventExitSub

        If MainClass.ValidateWithMasterTable(Trim(TxtName.Text), "SUPP_CUST_NAME", "SUPP_CUST_CODE", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='" & lblType.Text & "'") = True Then
            txtCode.Text = MasterNo
        End If

        If Trim(txtCode.Text) = "" Then
            MsgInformation("Customer Code Is Blank")
            txtCode.Focus()
            GoTo EventExitSub
        End If


        If MODIFYMode = True And RsVCMain.EOF = False Then mAccountCode = RsVCMain.Fields("SUPP_CUST_CODE").Value

        SqlStr = " SELECT * FROM PRD_RM_GRADE_RATE_HDR " & vbCrLf _
        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'"

        SqlStr = SqlStr & vbCrLf & " AND AMEND_NO = (" & vbCrLf _
        & " SELECT MAX(AMEND_NO) FROM PRD_RM_GRADE_RATE_HDR " _
        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "')"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsVCMain, ADODB.LockTypeEnum.adLockReadOnly)

        If RsVCMain.EOF = False Then
            Clear1()
            Call Show1()
        Else

            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Month, Use add Button to Generate New Costing.", MsgBoxStyle.Information)
                Cancel = True
                GoTo EventExitSub
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM PRD_RM_GRADE_RATE_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsVCMain, ADODB.LockTypeEnum.adLockReadOnly)
                GoTo EventExitSub
            End If

        End If

        CalcTots()
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim mAccountName As String

        Clear1()
        If Not RsVCMain.EOF Then

            mAccountCode = IIf(IsDBNull(RsVCMain.Fields("SUPP_CUST_CODE").Value), -1, RsVCMain.Fields("SUPP_CUST_CODE").Value)

            If MainClass.ValidateWithMasterTable(mAccountCode, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE='" & lblType.Text & "'") = True Then
                mAccountName = MasterNo
            End If

            TxtName.Text = mAccountName
            txtCode.Text = Trim(IIf(IsDBNull(RsVCMain.Fields("SUPP_CUST_CODE").Value), "", RsVCMain.Fields("SUPP_CUST_CODE").Value))

            txtWEF.Text = VB6.Format(IIf(IsDBNull(RsVCMain.Fields("WEF_DATE").Value), "", RsVCMain.Fields("WEF_DATE").Value), "DD/MM/YYYY")
            txtAmendNo.Text = IIf(IsDBNull(RsVCMain.Fields("AMEND_NO").Value), 0, RsVCMain.Fields("AMEND_NO").Value)

            chkCancelled.CheckState = IIf(RsVCMain.Fields("CANCELLED").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

            txtCode.Enabled = False
            TxtName.Enabled = False
            txtWEF.Enabled = False
            chkCancelled.Enabled = IIf(RsVCMain.Fields("CANCELLED").Value = "Y", False, True)
            cmdAmend.Enabled = True
            cmdSearchAmend.Enabled = True
            Call ShowDetail1((RsVCMain.Fields("SUPP_CUST_CODE").Value), (RsVCMain.Fields("WEF_DATE").Value))

            Call ShowDetailPress1((RsVCMain.Fields("SUPP_CUST_CODE").Value), (RsVCMain.Fields("WEF_DATE").Value))
            Call ShowOprDetail1((RsVCMain.Fields("SUPP_CUST_CODE").Value), (RsVCMain.Fields("WEF_DATE").Value), "WLD", SprdWeld)
            Call ShowOprDetail1((RsVCMain.Fields("SUPP_CUST_CODE").Value), (RsVCMain.Fields("WEF_DATE").Value), "PLT", SprdPlating)
            Call ShowOprDetail1((RsVCMain.Fields("SUPP_CUST_CODE").Value), (RsVCMain.Fields("WEF_DATE").Value), "PPC", SprdPPC)

        End If
        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = True
        SprdMain.Enabled = True
        SprdPress.Enabled = True
        SprdWeld.Enabled = True
        SprdPlating.Enabled = True
        SprdPPC.Enabled = True
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColGradeDesc, ColDensity)
        MainClass.ButtonStatus(Me, XRIGHT, RsVCMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume					
    End Sub
    Private Function GetMaxAmendNo() As Integer

        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim RsTemp As ADODB.Recordset

        SqlStr = " SELECT MAX(AMEND_NO) AS AMEND_NO" & vbCrLf _
        & " FROM PRD_RM_GRADE_RATE_HDR" & vbCrLf _
        & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            If IsDBNull(RsTemp.Fields("AMEND_NO").Value) Then
                GetMaxAmendNo = 0
            Else
                GetMaxAmendNo = Val(RsTemp.Fields("AMEND_NO").Value) + 1
            End If
        Else
            GetMaxAmendNo = 0
        End If

        Exit Function
ErrPart:
        GetMaxAmendNo = Val(txtAmendNo.Text)
    End Function
    Private Sub ShowDetail1(ByRef mSuppCode As String, ByRef mWef As String)

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String
        Dim mGradeCode As String
        Dim mGradeDesc As String
        Dim mGradeType As String
        Dim mGradeUOM As String
        Dim mDensity As String
        Dim mPrevRate As String
        Dim mCurrRate As String
        Dim mScrapRate As Double
        Dim mDiff As String
        Dim mDiffPer As String

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf _
        & " FROM PRD_RM_GRADE_RATE_DET " & vbCrLf _
        & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSuppCode) & "'" & vbCrLf _
        & " AND WEF_DATE=TO_DATE('" & VB6.Format(mWef, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
        & " Order By GRADE_CODE"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsVCDetail, ADODB.LockTypeEnum.adLockReadOnly)

        With RsVCDetail
            If .EOF = True Then Exit Sub
            '        FormatSprdMain -1					
            I = 1
            '        .MoveFirst					

            Do While Not .EOF

                SprdMain.Row = I
                '            FormatSprdMain I					

                SprdMain.Col = ColGradeCode
                mGradeCode = Trim(IIf(IsDBNull(.Fields("GRADE_CODE").Value), "", .Fields("GRADE_CODE").Value))
                SprdMain.Text = mGradeCode

                SprdMain.Col = ColGradeDesc
                If MainClass.ValidateWithMasterTable(mGradeCode, "MTRL_CODE", "MTRL_DESC", "PRD_MTRL_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mGradeDesc = MasterNo
                Else
                    mGradeDesc = ""
                End If
                SprdMain.Text = mGradeDesc

                SprdMain.Col = ColGradeUOM
                If MainClass.ValidateWithMasterTable(mGradeCode, "MTRL_CODE", "GRADE_UOM", "PRD_MTRL_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mGradeUOM = MasterNo
                Else
                    mGradeUOM = ""
                End If
                SprdMain.Text = mGradeUOM

                SprdMain.Col = ColGradeType
                If MainClass.ValidateWithMasterTable(mGradeCode, "MTRL_CODE", "MTRL_TYPE", "PRD_MTRL_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mGradeType = MasterNo
                Else
                    mGradeType = ""
                End If
                SprdMain.Text = mGradeType

                SprdMain.Col = ColDensity
                If MainClass.ValidateWithMasterTable(mGradeCode, "MTRL_CODE", "MTRL_DENSITY", "PRD_MTRL_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDensity = MasterNo
                Else
                    mDensity = ""
                End If
                SprdMain.Text = mDensity

                SprdMain.Col = ColPrevRate
                mPrevRate = CStr(GetPreviousRate(mGradeCode))
                SprdMain.Text = mPrevRate

                SprdMain.Col = ColCurrRate
                mCurrRate = CStr(Val(IIf(IsDBNull(.Fields("RATE").Value), 0, .Fields("RATE").Value)))
                SprdMain.Text = mCurrRate

                SprdMain.Col = ColDiff
                mDiff = CStr(CDbl(mCurrRate) - CDbl(mPrevRate))
                SprdMain.Text = mDiff

                SprdMain.Col = ColDiffPer
                If CDbl(mPrevRate) = 0 Then
                    mDiffPer = CStr(1)
                Else
                    mDiffPer = CStr(CDbl(mDiff) * 100 / CDbl(mPrevRate))
                End If
                SprdMain.Text = mDiffPer

                SprdMain.Col = ColScrapRate
                mScrapRate = Val(IIf(IsDBNull(.Fields("SCRAP_RATE").Value), 0, .Fields("SCRAP_RATE").Value))
                SprdMain.Text = CStr(mScrapRate)

                SprdMain.Col = ColStatus
                SprdMain.Value = IIf(.Fields("STATUS").Value = "C", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                .MoveNext()

                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With
        FormatSprdMain(-1)
        Call GridProtect()
        Call CalcGrid()


        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume					
    End Sub

    Private Function GetPreviousRate(ByRef mGradeCode As String) As Double

        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset
        Dim SqlStr As String

        GetPreviousRate = 0
        If Trim(txtWEF.Text) = "" Then Exit Function
        SqlStr = ""

        SqlStr = " SELECT RATE " & vbCrLf _
        & " FROM PRD_RM_GRADE_RATE_DET " & vbCrLf _
        & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'" & vbCrLf _
        & " AND GRADE_CODE='" & MainClass.AllowSingleQuote(mGradeCode) & "'" & vbCrLf _
        & " AND WEF_DATE=" & vbCrLf _
        & " (SELECT MAX(WEF_DATE) " & vbCrLf _
        & " FROM PRD_RM_GRADE_RATE_DET " & vbCrLf _
        & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCode.Text) & "'" & vbCrLf _
        & " AND GRADE_CODE='" & MainClass.AllowSingleQuote(mGradeCode) & "'" & vbCrLf _
        & " AND WEF_DATE<TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"



        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetPreviousRate = Val(IIf(IsDBNull(RsTemp.Fields("RATE").Value), 0, RsTemp.Fields("RATE").Value))
        End If

        Exit Function
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume					
    End Function
    Private Sub ShowSupplierDetail(ByRef mSuppCode As String)

        On Error GoTo ERR1
        Dim I As Integer
        Dim RsTemp As ADODB.Recordset
        Dim SqlStr As String
        Dim mGradeCode As String
        Dim mGradeDesc As String
        Dim mGradeType As String
        Dim mGradeUOM As String
        Dim mDensity As String
        Dim mPrevRate As String
        Dim mCurrRate As String
        Dim mDiff As String
        Dim mDiffPer As String

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf _
        & " FROM FIN_SUPP_CUST_DET " & vbCrLf _
        & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSuppCode) & "'" & vbCrLf _
        & " AND TRN_TYPE='P' AND COSTING_REQ='Y'" & vbCrLf _
        & " ORDER BY ITEM_CODE"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        With RsTemp
            If .EOF = True Then Exit Sub
            '        FormatSprdMain -1					
            I = 1
            '        .MoveFirst					

            Do While Not .EOF

                SprdMain.Row = I

                SprdMain.Col = ColGradeCode
                mGradeCode = Trim(IIf(IsDBNull(.Fields("GRADE_CODE").Value), "", .Fields("GRADE_CODE").Value))
                SprdMain.Text = mGradeCode

                SprdMain.Col = ColGradeDesc
                If MainClass.ValidateWithMasterTable(mGradeCode, "MTRL_CODE", "MTRL_DESC", "PRD_MTRL_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mGradeDesc = MasterNo
                Else
                    mGradeDesc = ""
                End If
                SprdMain.Text = mGradeDesc

                SprdMain.Col = ColGradeUOM
                If MainClass.ValidateWithMasterTable(mGradeCode, "GRADE_CODE", "GRADE_UOM", "PRD_MTRL_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mGradeUOM = MasterNo
                Else
                    mGradeUOM = ""
                End If
                SprdMain.Text = mGradeUOM

                SprdMain.Col = ColGradeType
                If MainClass.ValidateWithMasterTable(mGradeCode, "MTRL_CODE", "MTRL_TYPE", "PRD_MTRL_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mGradeType = MasterNo
                Else
                    mGradeType = ""
                End If
                SprdMain.Text = mGradeType

                SprdMain.Col = ColDensity
                If MainClass.ValidateWithMasterTable(mGradeCode, "MTRL_CODE", "MTRL_DENSITY", "PRD_MTRL_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mDensity = MasterNo
                Else
                    mDensity = ""
                End If
                SprdMain.Text = mDensity

                SprdMain.Col = ColPrevRate
                mPrevRate = CStr(GetPreviousRate(mGradeCode))
                SprdMain.Text = mPrevRate

                SprdMain.Col = ColCurrRate
                mCurrRate = mPrevRate
                SprdMain.Text = mCurrRate

                SprdMain.Col = ColDiff
                mDiff = CStr(CDbl(mCurrRate) - CDbl(mPrevRate))
                SprdMain.Text = mDiff

                SprdMain.Col = ColDiffPer
                If CDbl(mPrevRate) = 0 Then
                    mDiffPer = CStr(0)
                Else
                    mDiffPer = CStr(CDbl(mDiff) * 100 / CDbl(mPrevRate))
                End If
                SprdMain.Text = mDiffPer

                SprdMain.Col = ColStatus
                SprdMain.Value = CStr(System.Windows.Forms.CheckState.Unchecked)

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
    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRemarks_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtRemarks.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtRemarks.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtWEF_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWEF.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtWEF_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtWEF.DoubleClick
        Call cmdsearch_Click(cmdSearch, New System.EventArgs())
    End Sub


    Private Sub txtWEF_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtWEF.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            Call cmdSearchWEF_Click()
        End If
    End Sub


    Private Sub txtWEF_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtWEF.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        If Trim(txtWEF.Text) = "" Then GoTo EventExitSub

        If IsDate(txtWEF.Text) = False Then
            MsgInformation(" Invalid Date.")
            Cancel = True
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub CalcTots()
        On Error GoTo CalcERR
        Dim I As Integer
        Dim mGradeCode As String
        Dim mPrevRate As Double
        Dim mCurrRate As Double
        Dim mDiff As Double
        Dim ColDiffPer As Double

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColGradeCode
                mGradeCode = Trim(.Text)
                If Trim(mGradeCode) <> "" Then

                    SprdMain.Col = ColPrevRate
                    mPrevRate = GetPreviousRate(mGradeCode)
                    SprdMain.Text = CStr(mPrevRate)

                    SprdMain.Col = ColCurrRate
                    SprdMain.Text = IIf(Val(SprdMain.Text) = 0, mPrevRate, Val(SprdMain.Text))
                End If
            Next
        End With

        Call CalcGrid()

        Exit Sub
CalcERR:
        MsgBox(Err.Description)
    End Sub
    Private Sub CalcGrid()
        On Error GoTo CalcERR
        Dim I As Integer
        Dim mGradeCode As String
        Dim mPrevRate As Double
        Dim mCurrRate As Double
        Dim mDiff As Double
        Dim mDiffPer As Double

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColGradeCode
                mGradeCode = Trim(.Text)
                If Trim(mGradeCode) <> "" Then

                    SprdMain.Col = ColPrevRate
                    mPrevRate = Val(.Text)

                    SprdMain.Col = ColCurrRate
                    mCurrRate = Val(.Text)

                    SprdMain.Col = ColDiff
                    mDiff = mCurrRate - mPrevRate
                    SprdMain.Text = CStr(mDiff)

                    SprdMain.Col = ColDiffPer
                    If mPrevRate = 0 Then
                        mDiffPer = 0
                    Else
                        mDiffPer = mDiff * 100 / mPrevRate
                    End If
                    SprdMain.Text = CStr(mDiffPer)

                End If
            Next
        End With
        Exit Sub
CalcERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub GridProtect()

        On Error GoTo CalcERR
        Dim I As Integer
        Dim mStatus As String
        Dim mGradeCode As String

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColGradeCode
                mGradeCode = Trim(.Text)

                '            If POMade(mGradeCode) = True Then					
                '                .Col = ColStatus					
                '                .Value = vbChecked					
                '            End If					

                .Col = ColStatus
                mStatus = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                If mStatus = "Y" Then
                    MainClass.UnProtectCell(SprdMain, I, I, ColCurrRate, ColCurrRate)
                    MainClass.UnProtectCell(SprdMain, I, I, ColStatus, ColStatus)
                    MainClass.CellColor(SprdMain, I, I, ColGradeCode, ColStatus)

                    '                .Row = I					
                    '                .Row2 = I					
                    '                .Col = 1					
                    '                .col2 = .MaxCols					
                    '                .BlockMode = True					
                    '                .BackColor = &H8000000F					
                    '                .BlockMode = False					

                Else
                    MainClass.UnProtectCell(SprdMain, I, I, ColCurrRate, ColCurrRate)
                    MainClass.UnProtectCell(SprdMain, I, I, ColStatus, ColStatus)
                End If
            Next
        End With

        Exit Sub
CalcERR:
        MsgBox(Err.Description)
    End Sub
    Private Function UpdateDetailPress1(ByRef pAccountCode As String) As Boolean

        On Error GoTo UpdateDetailPress1
        Dim SqlStr As String
        Dim I As Integer
        Dim mPressMachine As String
        Dim mPressOPR As String
        Dim mPressTonnage As String
        Dim mRate As Double
        Dim mStatus As String
        Dim mScrapRate As Double
        Dim mPressRemarks As String
        Dim mOPRCode As String

        SqlStr = "DELETE FROM  PRD_SUPP_PRESS_DET " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pAccountCode) & "'" & vbCrLf _
                & " AND WEF_DATE=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        PubDBCn.Execute(SqlStr)

        With SprdPress
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColPressMc
                mPressMachine = MainClass.AllowSingleQuote(.Text)

                .Col = ColPressTonnage
                mPressTonnage = Val(.Text)

                .Col = ColPressOPR
                mPressOPR = Trim(.Text)

                mOPRCode = ""
                If MainClass.ValidateWithMasterTable(mPressOPR, "OPR_DESC", "OPR_CODE", "PRD_OPR_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mOPRCode = MasterNo
                End If

                .Col = ColPressCost
                mRate = Val(.Text)

                .Col = ColPressRemarks
                mPressRemarks = Trim(.Text)

                SqlStr = ""

                If mPressMachine <> "" Then
                    SqlStr = " INSERT INTO PRD_SUPP_PRESS_DET ( SERIAL_NO," & vbCrLf _
                        & " COMPANY_CODE , SUPP_CUST_CODE, " & vbCrLf _
                        & " WEF_DATE, MACHINE_DESC, MACHINE_TONN, OPR_CODE," & vbCrLf _
                        & " RATE, REMARKS) "

                    SqlStr = SqlStr & vbCrLf & " VALUES ( " & I & "," & vbCrLf _
                        & " '" & RsCompany.Fields("COMPANY_CODE").Value & "','" & MainClass.AllowSingleQuote(pAccountCode) & "', " & vbCrLf _
                        & " TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                        & " '" & mPressMachine & "', " & mPressTonnage & ", '" & mOPRCode & "', " & mRate & ", '" & mPressRemarks & "' ) "

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        UpdateDetailPress1 = True
        Exit Function
UpdateDetailPress1:
        UpdateDetailPress1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume					
    End Function
    Private Sub FormatSprdPress(ByRef Arow As Integer)

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim cntCol As Integer

        With SprdPress
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = ColPressMc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsPressDetail.Fields("MACHINE_DESC").DefinedSize ''					
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 25)

            .Col = ColPressTonnage
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColPressTonnage, 12)


            .Col = ColPressOPR
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ALPHANUMERIC
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = MainClass.SetMaxLength("OPR_CODE", "PRD_OPR_MST", PubDBCn)
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 25)



            .Col = ColPressCost
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColPressCost, 12)



            .Col = ColPressRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ALPHANUMERIC
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = RsPressDetail.Fields("REMARKS").DefinedSize ''	
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 12)

            'MainClass.UnProtectCell(SprdPress, 1, SprdPress.MaxRows, ColGradeCode, ColStatus)
            'MainClass.ProtectCell(SprdPress, 1, SprdPress.MaxRows, ColGradeDesc, ColPrevRate)
            'MainClass.ProtectCell(SprdPress, 1, SprdPress.MaxRows, ColDiff, ColDiffPer)
            MainClass.SetSpreadColor(SprdPress, Arow)
        End With

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume					
    End Sub
    Private Sub SprdPress_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdPress.Change
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdPress_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdPress.Leave
        With SprdPress
            SprdPress_LeaveCell(SprdPress, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub
    Private Sub SprdPress_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdPress.ClickEvent

        Dim SqlStr As String

        Dim mOperationDesc As String
        If eventArgs.row = 0 And eventArgs.col = ColPressOPR Then
            With SprdPress
                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  "

                .Row = .ActiveRow

                .Col = ColPressOPR
                mOperationDesc = .Text

                .Text = ""
                If MainClass.SearchGridMaster(mOperationDesc, "PRD_OPR_MST", "OPR_DESC", "OPR_CODE", , , SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColPressOPR
                    .Text = AcName

                    Call SprdPress_LeaveCell(SprdPress, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColPressOPR, SprdPress.ActiveRow, ColPressOPR, SprdPress.ActiveRow, False))
                End If
            End With
        End If


        If eventArgs.col = 0 And eventArgs.row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdPress, eventArgs.row, ColPressMc)
            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub
    Private Sub SprdPress_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdPress.KeyUpEvent
        Dim mCol As Short
        mCol = SprdPress.ActiveCol
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColPressOPR Then SprdPress_ClickEvent(SprdPress, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColPressOPR, 0))
        SprdPress.Refresh()
    End Sub
    Private Sub SprdPress_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdPress.LeaveCell

        On Error GoTo ErrPart
        Dim mOPNDesc As String

        If eventArgs.newRow = -1 Then Exit Sub

        Select Case eventArgs.col
            Case ColPressOPR
                SprdPress.Row = SprdPress.ActiveRow


                SprdPress.Col = ColPressOPR
                mOPNDesc = MainClass.AllowSingleQuote(SprdPress.Text)

                If Trim(mOPNDesc) = "" Then Exit Sub

                If MainClass.ValidateWithMasterTable(mOPNDesc, "OPR_DESC", "OPR_CODE", "PRD_OPR_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                    MsgInformation("Invalid Process Name.")
                    MainClass.SetFocusToCell(SprdPress, SprdPress.ActiveRow, ColPressOPR)
                    Exit Sub
                End If

            Case ColPressCost
                If CheckItemRate(SprdPress, ColPressOPR, ColPressCost) = True Then
                    MainClass.AddBlankSprdRow(SprdPress, ColPressOPR, ConRowHeight)
                    FormatSprdPress((SprdPress.MaxRows))
                End If
        End Select
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub ShowDetailPress1(ByRef mSuppCode As String, ByRef mWef As String)

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String
        Dim mOprCode As String
        Dim mOPRDesc As String


        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf _
        & " FROM PRD_SUPP_PRESS_DET " & vbCrLf _
        & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSuppCode) & "'" & vbCrLf _
        & " AND WEF_DATE=TO_DATE('" & VB6.Format(mWef, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
        & " Order By SERIAL_NO"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsPressDetail, ADODB.LockTypeEnum.adLockReadOnly)

        With RsPressDetail
            If .EOF = True Then Exit Sub
            '        FormatSprdPress -1					
            I = 1
            '        .MoveFirst					

            Do While Not .EOF

                SprdPress.Row = I
                '            FormatSprdPress I					


                SprdPress.Col = ColPressMc
                SprdPress.Text = Trim(IIf(IsDBNull(.Fields("MACHINE_DESC").Value), "", .Fields("MACHINE_DESC").Value))

                SprdPress.Col = ColPressTonnage
                SprdPress.Text = Val(IIf(IsDBNull(.Fields("MACHINE_TONN").Value), 0, .Fields("MACHINE_TONN").Value))

                mOprCode = Trim(IIf(IsDBNull(.Fields("OPR_CODE").Value), "", .Fields("OPR_CODE").Value))

                SprdPress.Col = ColPressOPR
                If MainClass.ValidateWithMasterTable(mOprCode, "OPR_CODE", "OPR_DESC", "PRD_OPR_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mOPRDesc = MasterNo
                Else
                    mOPRDesc = ""
                End If
                SprdPress.Text = mOPRDesc


                SprdPress.Col = ColPressRemarks
                SprdPress.Text = Trim(IIf(IsDBNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value))

                SprdPress.Col = ColPressCost
                SprdPress.Text = CStr(Val(IIf(IsDBNull(.Fields("RATE").Value), 0, .Fields("RATE").Value)))

                .MoveNext()

                I = I + 1
                SprdPress.MaxRows = I
            Loop
        End With
        FormatSprdPress(-1)

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume					
    End Sub
    Private Function UpdateOPRDetail1(ByRef pAccountCode As String, ByRef pType As String, ByRef pSprd As AxFPSpreadADO.AxfpSpread) As Boolean

        On Error GoTo UpdateDetailWeld1
        Dim SqlStr As String
        Dim I As Integer
        Dim mOPRDesc As String
        Dim mOPRUOM As String
        Dim mOPRCost As Double
        Dim mOPRRemarks As String
        Dim mOPRCode As String

        SqlStr = "DELETE FROM  PRD_SUPP_OPR_DET " & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(pAccountCode) & "'" & vbCrLf _
                & " AND WEF_DATE=TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND OPR_TYPE='" & pType & "'"

        PubDBCn.Execute(SqlStr)
        With pSprd
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = ColOPRDesc
                mOPRDesc = MainClass.AllowSingleQuote(.Text)

                mOPRCode = ""
                If MainClass.ValidateWithMasterTable(mOPRDesc, "OPR_DESC", "OPR_CODE", "PRD_OPR_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mOPRCode = MasterNo
                End If

                .Col = ColOPRUOM
                mOPRUOM = Trim(.Text)

                .Col = ColOPRRemarks
                mOPRRemarks = Trim(.Text)

                .Col = ColOPRCost
                mOPRCost = Val(.Text)

                SqlStr = ""

                If mOPRDesc <> "" Then
                    SqlStr = " INSERT INTO PRD_SUPP_OPR_DET ( SERIAL_NO, " & vbCrLf _
                        & " COMPANY_CODE , SUPP_CUST_CODE, " & vbCrLf _
                        & " WEF_DATE, OPR_TYPE, OPR_CODE, OPR_UOM," & vbCrLf _
                        & " RATE, REMARKS) "

                    SqlStr = SqlStr & vbCrLf & " VALUES ( " & I & "," & vbCrLf _
                        & " '" & RsCompany.Fields("COMPANY_CODE").Value & "','" & MainClass.AllowSingleQuote(pAccountCode) & "', " & vbCrLf _
                        & " TO_DATE('" & VB6.Format(txtWEF.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                        & " '" & pType & "', '" & mOPRCode & "', '" & mOPRUOM & "',  " & mOPRCost & ", '" & mOPRRemarks & "' ) "

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        UpdateOPRDetail1 = True
        Exit Function
UpdateDetailWeld1:
        UpdateOPRDetail1 = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume					
    End Function
    Private Sub FormatSprdOpr(ByRef Arow As Integer, pSprd As AxFPSpreadADO.AxfpSpread)

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim cntCol As Integer

        With pSprd
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = ColOPRDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ALPHANUMERIC
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = MainClass.SetMaxLength("OPR_CODE", "PRD_OPR_MST", PubDBCn)
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 50)

            .Col = ColOPRUOM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ALPHANUMERIC
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = RsOPRDetail.Fields("OPR_UOM").DefinedSize ''	
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 7)


            .Col = ColOPRCost
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 4
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColOPRCost, 12)

            .Col = ColOPRRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ALPHANUMERIC
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditLen = RsOPRDetail.Fields("REMARKS").DefinedSize ''	
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 15)

            'MainClass.UnProtectCell(SprdWeld, 1, SprdWeld.MaxRows, ColGradeCode, ColStatus)
            'MainClass.ProtectCell(SprdWeld, 1, SprdWeld.MaxRows, ColGradeDesc, ColPrevRate)
            'MainClass.ProtectCell(SprdWeld, 1, SprdWeld.MaxRows, ColDiff, ColDiffPer)
            MainClass.SetSpreadColor(pSprd, Arow)
        End With

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume					
    End Sub
    Private Sub SprdWeld_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdWeld.Change
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdPlating_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdPlating.Change
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdPPC_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdPPC.Change
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdWeld_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdWeld.Leave
        With SprdWeld
            SprdWeld_LeaveCell(SprdWeld, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub
    Private Sub SprdPlating_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdPlating.Leave
        With SprdPlating
            SprdPlating_LeaveCell(SprdWeld, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub
    Private Sub SprdPPC_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdPPC.Leave
        With SprdPPC
            SprdPPC_LeaveCell(SprdWeld, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False))
        End With
    End Sub
    Private Sub SprdWeld_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdWeld.KeyUpEvent
        Dim mCol As Short
        mCol = SprdWeld.ActiveCol
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColOPRDesc Then SprdWeld_ClickEvent(SprdWeld, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColOPRDesc, 0))
        SprdWeld.Refresh()
    End Sub
    Private Sub SprdPlating_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdPlating.KeyUpEvent
        Dim mCol As Short
        mCol = SprdPlating.ActiveCol
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColOPRDesc Then SprdPlating_ClickEvent(SprdWeld, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColOPRDesc, 0))
        SprdPlating.Refresh()
    End Sub
    Private Sub SprdPPC_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdPPC.KeyUpEvent
        Dim mCol As Short
        mCol = SprdPPC.ActiveCol
        If eventArgs.keyCode = System.Windows.Forms.Keys.F1 And mCol = ColOPRDesc Then SprdPPC_ClickEvent(SprdWeld, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColOPRDesc, 0))
        SprdPPC.Refresh()
    End Sub
    Private Sub SprdWeld_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdWeld.ClickEvent

        Dim SqlStr As String
        Dim mOperationDesc As String
        If eventArgs.row = 0 And eventArgs.col = ColOPRDesc Then
            With SprdWeld
                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  "

                .Row = .ActiveRow

                .Col = ColOPRDesc
                mOperationDesc = .Text

                .Text = ""
                If MainClass.SearchGridMaster(mOperationDesc, "PRD_OPR_MST", "OPR_DESC", "OPR_CODE", , , SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColOPRDesc
                    .Text = AcName

                    Call SprdWeld_LeaveCell(SprdWeld, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColOPRDesc, SprdWeld.ActiveRow, ColOPRDesc, SprdWeld.ActiveRow, False))
                End If
            End With
        End If

        If eventArgs.col = 0 And eventArgs.row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdWeld, eventArgs.row, ColOPRDesc)
            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub
    Private Sub SprdPlating_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdPlating.ClickEvent

        Dim SqlStr As String
        Dim mOperationDesc As String

        If eventArgs.row = 0 And eventArgs.col = ColOPRDesc Then
            With SprdPlating
                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  "

                .Row = .ActiveRow

                .Col = ColOPRDesc
                mOperationDesc = .Text

                .Text = ""
                If MainClass.SearchGridMaster(mOperationDesc, "PRD_OPR_MST", "OPR_DESC", "OPR_CODE", , , SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColOPRDesc
                    .Text = AcName

                    Call SprdPlating_LeaveCell(SprdPlating, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColOPRDesc, SprdPlating.ActiveRow, ColOPRDesc, SprdPlating.ActiveRow, False))
                End If
            End With
        End If

        If eventArgs.col = 0 And eventArgs.row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdPlating, eventArgs.row, ColOPRDesc)
            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub
    Private Sub SprdPPC_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdPPC.ClickEvent

        Dim SqlStr As String
        Dim mOperationDesc As String
        If eventArgs.row = 0 And eventArgs.col = ColOPRDesc Then
            With SprdPPC
                SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  "

                .Row = .ActiveRow

                .Col = ColOPRDesc
                mOperationDesc = .Text

                .Text = ""
                If MainClass.SearchGridMaster(mOperationDesc, "PRD_OPR_MST", "OPR_DESC", "OPR_CODE", , , SqlStr) = True Then
                    .Row = .ActiveRow

                    .Col = ColOPRDesc
                    .Text = AcName

                    Call SprdPPC_LeaveCell(SprdPPC, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColOPRDesc, SprdPPC.ActiveRow, ColOPRDesc, SprdPPC.ActiveRow, False))
                End If
            End With
        End If

        If eventArgs.col = 0 And eventArgs.row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdPPC, eventArgs.row, ColOPRDesc)
            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub
    Private Sub SprdWeld_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdWeld.LeaveCell
        On Error GoTo ErrPart
        Dim mOPNDesc As String

        If eventArgs.newRow = -1 Then Exit Sub

        Select Case eventArgs.col
            Case ColOPRDesc
                SprdWeld.Row = SprdWeld.ActiveRow


                SprdWeld.Col = ColOPRDesc
                mOPNDesc = MainClass.AllowSingleQuote(SprdWeld.Text)

                If Trim(mOPNDesc) = "" Then Exit Sub

                If MainClass.ValidateWithMasterTable(mOPNDesc, "OPR_DESC", "OPR_CODE", "PRD_OPR_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                    MsgInformation("Invalid Process Name.")
                    MainClass.SetFocusToCell(SprdWeld, SprdWeld.ActiveRow, ColOPRDesc)
                    Exit Sub
                End If

            Case ColPressCost
                If CheckItemRate(SprdWeld, ColOPRDesc, ColOPRCost) = True Then
                    MainClass.AddBlankSprdRow(SprdWeld, ColOPRDesc, ConRowHeight)
                    FormatSprdOpr(-1, SprdWeld)
                End If
        End Select


        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SprdPPC_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdPPC.LeaveCell
        On Error GoTo ErrPart
        Dim mOPNDesc As String

        If eventArgs.newRow = -1 Then Exit Sub

        Select Case eventArgs.col
            Case ColOPRDesc
                SprdPPC.Row = SprdPPC.ActiveRow


                SprdPPC.Col = ColOPRDesc
                mOPNDesc = MainClass.AllowSingleQuote(SprdPPC.Text)

                If Trim(mOPNDesc) = "" Then Exit Sub

                If MainClass.ValidateWithMasterTable(mOPNDesc, "OPR_DESC", "OPR_CODE", "PRD_OPR_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                    MsgInformation("Invalid Process Name.")
                    MainClass.SetFocusToCell(SprdPPC, SprdPPC.ActiveRow, ColOPRDesc)
                    Exit Sub
                End If

            Case ColPressCost
                If CheckItemRate(SprdPPC, ColOPRDesc, ColOPRCost) = True Then
                    MainClass.AddBlankSprdRow(SprdPPC, ColOPRDesc, ConRowHeight)
                    FormatSprdOpr(-1, SprdPPC)
                End If
        End Select

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub SprdPlating_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdPlating.LeaveCell

        On Error GoTo ErrPart
        Dim mOPNDesc As String

        If eventArgs.newRow = -1 Then Exit Sub

        Select Case eventArgs.col
            Case ColOPRDesc
                SprdPlating.Row = SprdPlating.ActiveRow


                SprdPlating.Col = ColOPRDesc
                mOPNDesc = MainClass.AllowSingleQuote(SprdPlating.Text)

                If Trim(mOPNDesc) = "" Then Exit Sub
                If MainClass.ValidateWithMasterTable(mOPNDesc, "OPR_DESC", "OPR_CODE", "PRD_OPR_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
                    MsgInformation("Invalid Process Name.")
                    MainClass.SetFocusToCell(SprdPlating, SprdPlating.ActiveRow, ColOPRDesc)
                    Exit Sub
                End If

            Case ColPressCost
                If CheckItemRate(SprdPlating, ColOPRDesc, ColOPRCost) = True Then
                    MainClass.AddBlankSprdRow(SprdPlating, ColOPRDesc, ConRowHeight)
                    FormatSprdOpr(-1, SprdPlating)
                End If
        End Select

        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub ShowOprDetail1(ByRef mSuppCode As String, ByRef mWef As String, ByRef pOPRType As String, ByRef pSprd As AxFPSpreadADO.AxfpSpread)

        On Error GoTo ERR1
        Dim I As Integer
        Dim SqlStr As String
        Dim mOprCode As String
        Dim mOPRDesc As String


        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf _
        & " FROM PRD_SUPP_OPR_DET " & vbCrLf _
        & " Where COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        & " AND SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(mSuppCode) & "'" & vbCrLf _
        & " AND WEF_DATE=TO_DATE('" & VB6.Format(mWef, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND OPR_TYPE='" & pOPRType & "'" & vbCrLf _
        & " Order By SERIAL_NO"


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOPRDetail, ADODB.LockTypeEnum.adLockReadOnly)

        With RsOPRDetail
            If .EOF = True Then Exit Sub
            '        FormatpSprd -1					
            I = 1
            '        .MoveFirst					

            Do While Not .EOF

                pSprd.Row = I
                '            FormatpSprd I					

                mOprCode = Trim(IIf(IsDBNull(.Fields("OPR_CODE").Value), "", .Fields("OPR_CODE").Value))

                pSprd.Col = ColOPRDesc
                If MainClass.ValidateWithMasterTable(mOprCode, "OPR_CODE", "OPR_DESC", "PRD_OPR_MST", PubDBCn, MasterNo,  , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mOPRDesc = MasterNo
                Else
                    mOPRDesc = ""
                End If
                pSprd.Text = mOPRDesc

                pSprd.Col = ColOPRUOM
                pSprd.Text = Trim(IIf(IsDBNull(.Fields("OPR_UOM").Value), "", .Fields("OPR_UOM").Value))

                pSprd.Col = ColOPRCost
                pSprd.Text = CStr(Val(IIf(IsDBNull(.Fields("RATE").Value), 0, .Fields("RATE").Value)))

                pSprd.Col = ColOPRRemarks
                pSprd.Text = Trim(IIf(IsDBNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value))


                .MoveNext()

                I = I + 1
                pSprd.MaxRows = I
            Loop
        End With
        FormatSprdOpr(-1, pSprd)

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '   Resume					
    End Sub
End Class
