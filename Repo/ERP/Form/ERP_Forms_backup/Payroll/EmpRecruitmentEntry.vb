Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports System.ComponentModel
Imports AxFPSpreadADO

Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Imports System.Data
Imports System.IO
Imports System.Configuration

Friend Class frmEmpRecruitmentEntry
    Inherits System.Windows.Forms.Form
    Dim RsRecruitment As ADODB.Recordset ''ADODB.Recordset					

    '''Private PvtDBCn As ADODB.Connection					

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean
    Dim mAccountCode As String


    Private Const ConRowHeight As Short = 22

    Private Const colApplicant As Short = 1
    Private Const ColCurrOrg As Short = 2
    Private Const ColCurrDept As Short = 3
    Private Const ColCurrDesg As Short = 4
    Private Const ColContactNo As Short = 5
    Private Const ColEmail As Short = 6
    Private Const ColLoc As Short = 7
    Private Const ColExp As Short = 8
    Private Const ColRelExp As Short = 9
    Private Const ColSalary As Short = 10
    Private Const ColExpected As Short = 11
    Private Const ColNotice As Short = 12
    Private Const ColInterested As Short = 13
    Private Const ColRemarks As Short = 14
    Private Const ColScreen As Short = 15


    Private Const ColDOI As Short = 1
    Private Const ColModeInv As Short = 2
    Private Const ColIntBy As Short = 3
    Private Const ColAttned As Short = 4
    Private Const ColFeedBack As Short = 5

    Private Const ColDOJ As Short = 1
    Private Const ColSalaryOff As Short = 2
    Private Const ColHRRemarks As Short = 3

    Private Const ColAppHead As Short = 1
    Private Const ColAppTech As Short = 2
    Private Const ColAppMD As Short = 3


    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            SprdMain.Enabled = True
            txtVNo.Enabled = False
            If txtVDate.Enabled = True Then txtVDate.Focus()
        Else
            ADDMode = False
            MODIFYMode = False
            If RsRecruitment.EOF = False Then RsRecruitment.MoveFirst()
            Show1()
            txtVNo.Enabled = True
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
        'Resume					
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        On Error Resume Next
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        Dim SqlStr As String

        If ValidateBranchLocking((txtVDate.Text)) = True Then
            Exit Sub
        End If

        If txtVNo.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If Not RsRecruitment.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.					
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "PAY_RECRUITMENT_MST", (txtVNo.Text), RsRecruitment) = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "PAY_RECRUITMENT_MST", "REF_NO", (lblMkey.Text)) = False Then GoTo DelErrPart

                PubDBCn.Execute("DELETE FROM PAY_RECRUITMENT_MST WHERE REF_NO=" & Val(lblMkey.Text) & "")
                PubDBCn.CommitTrans()
                RsRecruitment.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
        RsRecruitment.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsRecruitment, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
            SprdFirst.Enabled = True
            SprdSecond.Enabled = True
            SprdFinal.Enabled = True
            SprdApproval.Enabled = True
            txtVNo.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
            txtVNo.Enabled = True
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonST(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportonST(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mSubTitle1 As String
        Dim mHeading As String


        Report1.Reset()
        mTitle = Me.Text
        mSubTitle = ""
        mHeading = ""

        'Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\InsClaimLetter.RPT"

        SqlStr = MakeSQL()

        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle, mHeading)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        'Resume					
    End Sub

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String, ByRef pHeading As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub
    Private Function MakeSQL() As String

        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset
        Dim SqlStr As String
        Dim I As Integer


        'MakeSQL = ""
        '''''SELECT CLAUSE...					

        'MakeSQL = " SELECT REF_DATE, MACH_NAME, SURVEYOR_NAME, " & vbCrLf _
        '    & " OUR_REF_NO, BILL_NO, " & vbCrLf _
        '    & " TO_CHAR(BILL_DATE,'DD/MM/YYYY'), " & vbCrLf _
        '    & " SUPPLIER, BILL_AMT, " & vbCrLf _
        '    & " DECODE(CHQ_NO,NULL,'',CHQ_NO || ' & ' || TO_CHAR(CHQ_DATE,'DD/MM/YYYY')), " & vbCrLf _
        '    & " TO_CHAR(SETTLED_AMOUNT) AS SETTLED_AMOUNT, " & vbCrLf _
        '    & " TO_CHAR(CLAIM_AMOUNT-SETTLED_AMOUNT) AS SETTLED_AMOUNT, " & vbCrLf _
        '    & " IH.MKEY"


        '''''FROM CLAUSE...					
        'MakeSQL = MakeSQL & vbCrLf & " FROM " & vbCrLf _
        '    & " PAY_RECRUITMENT_MST IH, DOC_INS_CLAIM_DET ID"

        '''''WHERE CLAUSE...					
        'MakeSQL = MakeSQL & vbCrLf _
        '    & " WHERE " & vbCrLf _
        '    & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        '    & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
        '    & " AND IH.MKEY=ID.MKEY(+)"

        'MakeSQL = MakeSQL & vbCrLf _
        '    & " AND IH.MKEY='" & MainClass.AllowSingleQuote(lblMkey.Text) & "'"
        '''''ORDER CLAUSE...					
        '					
        '    MakeSQL = MakeSQL & vbCrLf & "ORDER BY IH.REF_NO,IH.REF_DATE"					
        '					
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ReportonST(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler

        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If Update1() = True Then
            ADDMode = False
            MODIFYMode = False
            TxtVNo_Validating(txtVNo, New System.ComponentModel.CancelEventArgs(False))
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
        Dim mCurRowNo As Integer
        Dim mVNo As Double

        Dim mApplicant As String
        Dim mCurrOrg As String
        Dim mCurrDept As String
        Dim mCurrDesg As String
        Dim mContactNo As String
        Dim mEmail As String
        Dim mLoc As String
        Dim mExp As Long
        Dim mRelExp As Long
        Dim mSalary As Double
        Dim mExpected As Double
        Dim mNotice As Long
        Dim mInterested As String
        Dim mRemarks As String
        Dim mScreen As String


        Dim mFLDOI As String
        Dim mFLModeInv As String
        Dim mFLIntBy As String
        Dim mFLAttned As String
        Dim mFLFeedBack As String

        Dim mSLDOI As String
        Dim mSLModeInv As String
        Dim mSLIntBy As String
        Dim mSLAttned As String
        Dim mSLFeedBack As String

        Dim mDOJ As String
        Dim mSalaryOff As Double
        Dim mHRRemarks As String

        Dim mAppHead As String
        Dim mAppTech As String
        Dim mAppMD As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()


        SqlStr = ""
        If Trim(txtVNo.Text) = "" Then
            mVNo = CDbl(AutoGenSeqRefNo("REF_NO"))
        Else
            mVNo = Val(txtVNo.Text)
        End If

        txtVNo.Text = mVNo

        With SprdMain
            .Row = 1
            .Col = colApplicant
            mApplicant = MainClass.AllowSingleQuote(.Text)

            .Col = ColCurrOrg
            mCurrOrg = MainClass.AllowSingleQuote(.Text)

            .Col = ColCurrDept
            mCurrDept = MainClass.AllowSingleQuote(.Text)

            .Col = ColCurrDesg
            mCurrDesg = MainClass.AllowSingleQuote(.Text)

            .Col = ColContactNo
            mContactNo = MainClass.AllowSingleQuote(.Text)

            .Col = ColEmail
            mEmail = MainClass.AllowSingleQuote(.Text)

            .Col = ColLoc
            mLoc = MainClass.AllowSingleQuote(.Text)

            .Col = ColExp
            mExp = Val(.Text)

            .Col = ColRelExp
            mRelExp = Val(.Text)

            .Col = ColSalary
            mSalary = Val(.Text)

            .Col = ColExpected
            mExpected = Val(.Text)

            .Col = ColNotice
            mNotice = MainClass.AllowSingleQuote(.Text)

            .Col = ColInterested
            mInterested = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N") '' MainClass.AllowSingleQuote(.Text)

            .Col = ColRemarks
            mRemarks = MainClass.AllowSingleQuote(.Text)

            .Col = ColScreen
            mScreen = MainClass.AllowSingleQuote(.Text)

        End With

        With SprdFirst
            .Row = 1
            .Col = ColDOI
            mFLDOI = VB6.Format(.Text, "DD-MMM-YYYY")

            .Col = ColModeInv
            mFLModeInv = MainClass.AllowSingleQuote(.Text)

            .Col = ColIntBy
            mFLIntBy = MainClass.AllowSingleQuote(.Text)

            .Col = ColAttned
            mFLAttned = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N") '' MainClass.AllowSingleQuote(.Text)

            .Col = ColFeedBack
            mFLFeedBack = MainClass.AllowSingleQuote(.Text)

        End With


        With SprdSecond
            .Row = 1
            .Col = ColDOI
            mSLDOI = VB6.Format(.Text, "DD-MMM-YYYY")

            .Col = ColModeInv
            mSLModeInv = MainClass.AllowSingleQuote(.Text)

            .Col = ColIntBy
            mSLIntBy = MainClass.AllowSingleQuote(.Text)

            .Col = ColAttned
            mSLAttned = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N") ''MainClass.AllowSingleQuote(.Text)

            .Col = ColFeedBack
            mSLFeedBack = MainClass.AllowSingleQuote(.Text)
        End With

        With SprdFinal
            .Row = 1
            .Col = ColDOJ
            mDOJ = VB6.Format(.Text, "DD-MMM-YYYY")

            .Col = ColSalaryOff
            mSalaryOff = Val(.Text)

            .Col = ColHRRemarks
            mHRRemarks = MainClass.AllowSingleQuote(.Text)
        End With


        With SprdApproval
            .Row = 1
            .Col = ColAppHead
            mAppHead = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N") ''MainClass.AllowSingleQuote(.Text)

            .Col = ColAppTech
            mAppTech = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N") ''MainClass.AllowSingleQuote(.Text)

            .Col = ColAppMD
            mAppMD = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N") ''MainClass.AllowSingleQuote(.Text)
        End With

        If ADDMode = True Then

            lblMkey.Text = mVNo

            SqlStr = " INSERT INTO PAY_RECRUITMENT_MST ( " & vbCrLf _
                & " COMPANY_CODE ," & vbCrLf _
                & " REF_NO, REF_DATE," & vbCrLf _
                & " APPLICANT_NAME, CURRENT_ORG, CURRENT_DEPT," & vbCrLf _
                & " CURRENT_DESG, CONTACT_NO, EMAIL_ID, " & vbCrLf _
                & " CURRENT_LOCATION,TOTAL_EXP, RELEVENT_EXP, " & vbCrLf _
                & " CURRENT_SALARY, EXPECTED_SALARY,NOTICES_PERIOD, " & vbCrLf _
                & " JOINING_INTERESTED, REMARKS, SCREENING," & vbCrLf _
                & " FL_DOI,FL_MOI,FL_INTERVIEW_BY, " & vbCrLf _
                & " FL_ATTENDED,FL_FEEDBACK," & vbCrLf _
                & " SL_DOI,SL_MOI,SL_INTERVIEW_BY, " & vbCrLf _
                & " SL_ATTENDED,SL_FEEDBACK," & vbCrLf _
                & " DOJ, SALARY_OFFERED, HR_REMARKS," & vbCrLf _
                & " HEAD_APP,TECH_APP, MD_APP ," & vbCrLf _
                & " ADDUSER,ADDDATE,MODUSER, MODDATE )"


            SqlStr = SqlStr & vbCrLf _
                & " VALUES ( " & vbCrLf _
                & " " & RsCompany.Fields("COMPANY_CODE").Value & "," & vbCrLf _
                & " " & Val(txtVNo.Text) & ", " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & "'" & mApplicant & "', '" & mCurrOrg & "', '" & mCurrDept & "'," & vbCrLf _
                & "'" & mCurrDesg & "', '" & mContactNo & "', '" & mEmail & "'," & vbCrLf _
                & "'" & mLoc & "', " & mExp & ", " & mRelExp & "," & vbCrLf _
                & "" & mSalary & ", " & mExpected & ", '" & mNotice & "'," & vbCrLf _
                & "'" & mInterested & "', '" & mRemarks & "', '" & mScreen & "'," & vbCrLf _
                & "TO_DATE('" & mFLDOI & "','DD-MON-YYYY'), '" & mFLModeInv & "', '" & mFLIntBy & "'," & vbCrLf _
                & "'" & mFLAttned & "', '" & mFLFeedBack & "', " & vbCrLf _
                & " TO_DATE('" & mSLDOI & "','DD-MON-YYYY'), '" & mSLModeInv & "', '" & mSLIntBy & "'," & vbCrLf _
                & "'" & mSLAttned & "', '" & mSLFeedBack & "'," & vbCrLf _
                & " TO_DATE('" & mDOJ & "','DD-MON-YYYY'), " & mSalaryOff & ", '" & mHRRemarks & "'," & vbCrLf _
                & "'" & mAppHead & "', '" & mAppTech & "', '" & mAppMD & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                & " TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"
        End If

        If MODIFYMode = True Then
            SqlStr = " UPDATE PAY_RECRUITMENT_MST SET " & vbCrLf _
                & " REF_NO=" & Val(txtVNo.Text) & ", " & vbCrLf _
                & " REF_DATE=TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " APPLICANT_NAME='" & mApplicant & "', CURRENT_ORG='" & mCurrOrg & "', CURRENT_DEPT='" & mCurrDept & "'," & vbCrLf _
                & " CURRENT_DESG='" & mCurrDesg & "', CONTACT_NO='" & mContactNo & "', EMAIL_ID='" & mEmail & "'," & vbCrLf _
                & " CURRENT_LOCATION='" & mLoc & "', TOTAL_EXP=" & mExp & ", RELEVENT_EXP=" & mRelExp & "," & vbCrLf _
                & " CURRENT_SALARY=" & mSalary & ", EXPECTED_SALARY=" & mExpected & ", NOTICES_PERIOD='" & mNotice & "'," & vbCrLf _
                & " JOINING_INTERESTED='" & mInterested & "', REMARKS='" & mRemarks & "', SCREENING='" & mScreen & "'," & vbCrLf _
                & " FL_DOI=TO_DATE('" & mFLDOI & "','DD-MON-YYYY'), FL_MOI='" & mFLModeInv & "',FL_INTERVIEW_BY= '" & mFLIntBy & "'," & vbCrLf _
                & " FL_ATTENDED='" & mFLAttned & "',FL_FEEDBACK= '" & mFLFeedBack & "', " & vbCrLf _
                & " SL_DOI=TO_DATE('" & mSLDOI & "','DD-MON-YYYY'),SL_MOI= '" & mSLModeInv & "', SL_INTERVIEW_BY='" & mSLIntBy & "'," & vbCrLf _
                & " SL_ATTENDED='" & mSLAttned & "', SL_FEEDBACK='" & mSLFeedBack & "'," & vbCrLf _
                & " DOJ=TO_DATE('" & mDOJ & "','DD-MON-YYYY'), SALARY_OFFERED=" & mSalaryOff & ", HR_REMARKS='" & mHRRemarks & "'," & vbCrLf _
                & " HEAD_APP='" & mAppHead & "', TECH_APP='" & mAppTech & "', MD_APP='" & mAppMD & "',"

            SqlStr = SqlStr & vbCrLf _
                & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND REF_NO =" & Val(lblMkey.Text) & ""
        End If

        PubDBCn.Execute(SqlStr)



        Update1 = True
        PubDBCn.CommitTrans()

        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsRecruitment.Requery()
        RsRecruitment.Requery()
        MsgBox(Err.Description)
        ''Resume					
    End Function
    Private Function AutoGenSeqRefNo(ByRef mFieldName As String) As String

        On Error GoTo AutoGenSeqBillNoErr
        Dim RsGen As ADODB.Recordset
        Dim mNewSeqNo As Integer
        Dim SqlStr As String

        SqlStr = ""
        mNewSeqNo = 1

        SqlStr = "SELECT Max(" & mFieldName & ")  FROM PAY_RECRUITMENT_MST " & vbCrLf _
            & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGen, ADODB.LockTypeEnum.adLockReadOnly)

        With RsGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mNewSeqNo = .Fields(0).Value + 1
                Else
                    mNewSeqNo = 1
                End If
            End If
        End With
        AutoGenSeqRefNo = CStr(mNewSeqNo)
        Exit Function
AutoGenSeqBillNoErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
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
            '        ADataGrid.Refresh					
            FormatSprdView()
            SprdView.Refresh()
            SprdView.Focus()
            SprdView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            SprdView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsRecruitment, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Public Sub frmEmpRecruitmentEntry_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor


        SqlStr = "Select * From PAY_RECRUITMENT_MST WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRecruitment, ADODB.LockTypeEnum.adLockReadOnly)

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
        Dim SqlStr As String

        SqlStr = ""

        SqlStr = " SELECT " & vbCrLf _
            & " REF_NO, REF_DATE, APPLICANT_NAME, CURRENT_ORG, CURRENT_DEPT, CURRENT_DESG, CONTACT_NO, EMAIL_ID " & vbCrLf _
            & " FROM PAY_RECRUITMENT_MST " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        SqlStr = SqlStr & " ORDER BY REF_NO,REF_DATE"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmEmpRecruitmentEntry_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection					
        ''PvtDBCn.Open StrConn					
        Call SetMainFormCordinate(Me)
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

        mAccountCode = "-1"
        lblMkey.Text = ""
        txtVNo.Text = ""
        txtVDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        txtVDate.Enabled = True
        SprdMain.Enabled = True
        SprdFirst.Enabled = True
        SprdSecond.Enabled = True
        SprdFinal.Enabled = True
        SprdApproval.Enabled = True

        MainClass.ClearGrid(SprdMain, ConRowHeight)
        MainClass.ClearGrid(SprdFirst, ConRowHeight)
        MainClass.ClearGrid(SprdSecond, ConRowHeight)
        MainClass.ClearGrid(SprdFinal, ConRowHeight)
        MainClass.ClearGrid(SprdApproval, ConRowHeight)

        FormatSprdMain(-1)

        MainClass.ButtonStatus(Me, XRIGHT, RsRecruitment, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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

            .Col = colApplicant
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRecruitment.Fields("APPLICANT_NAME").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 22)

            .Col = ColCurrOrg
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRecruitment.Fields("CURRENT_ORG").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 15)

            .Col = ColCurrDept
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRecruitment.Fields("CURRENT_DEPT").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 15)

            .Col = ColCurrDesg
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRecruitment.Fields("CURRENT_DESG").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 15)

            .Col = ColContactNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRecruitment.Fields("CONTACT_NO").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 15)

            .Col = ColEmail
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRecruitment.Fields("EMAIL_ID").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 15)

            .Col = ColLoc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRecruitment.Fields("CURRENT_LOCATION").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 15)

            .Col = ColExp
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeEditLen = RsRecruitment.Fields("TOTAL_EXP").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 8)

            .Col = ColRelExp
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeEditLen = RsRecruitment.Fields("RELEVENT_EXP").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 8)

            .Col = ColSalary
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsRecruitment.Fields("CURRENT_SALARY").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColSalary, 12)

            .Col = ColExpected
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsRecruitment.Fields("EXPECTED_SALARY").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColExpected, 12)

            .Col = ColNotice
            .CellType = SS_CELL_TYPE_INTEGER
            .TypeEditLen = RsRecruitment.Fields("NOTICES_PERIOD").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 8)

            .Col = ColInterested
            '.CellType = SS_CELL_TYPE_EDIT
            '.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            '.TypeEditLen = RsRecruitment.Fields("JOINING_INTERESTED").DefinedSize
            '.TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .Value = CStr(System.Windows.Forms.CheckState.Unchecked)
            .set_ColWidth(.Col, 8)

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRecruitment.Fields("REMARKS").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 8)

            .Col = ColScreen
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRecruitment.Fields("SCREENING").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 8)

            'MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, colSupplier, ColBillAmt)
            MainClass.SetSpreadColor(SprdMain, Arow)

        End With



        With SprdFirst
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = ColDOI
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeEditCharSet = SS_CELL_DATE_FORMAT_DDMMYY
            .TypeDateFormat = FPSpreadADO.TypeDateFormatConstants.TypeDateFormatDDMMYY
            .set_ColWidth(.Col, 15)
            .TypeEditMultiLine = True


            .Col = ColModeInv
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRecruitment.Fields("FL_MOI").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 15)


            .Col = ColIntBy
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRecruitment.Fields("FL_INTERVIEW_BY").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 30)

            .Col = ColAttned
            '.CellType = SS_CELL_TYPE_EDIT
            '.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            '.TypeEditLen = RsRecruitment.Fields("FL_ATTENDED").DefinedSize
            '.TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .Value = CStr(System.Windows.Forms.CheckState.Unchecked)
            .set_ColWidth(.Col, 12)

            .Col = ColFeedBack
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRecruitment.Fields("FL_FEEDBACK").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 30)

            MainClass.SetSpreadColor(SprdFirst, Arow)
        End With

        With SprdSecond
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = ColDOI
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeEditCharSet = SS_CELL_DATE_FORMAT_DDMMYY
            .TypeDateFormat = FPSpreadADO.TypeDateFormatConstants.TypeDateFormatDDMMYY
            .set_ColWidth(.Col, 15)
            .TypeEditMultiLine = True


            .Col = ColModeInv
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRecruitment.Fields("SL_MOI").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 15)


            .Col = ColIntBy
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRecruitment.Fields("SL_INTERVIEW_BY").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 30)

            .Col = ColAttned
            '.CellType = SS_CELL_TYPE_EDIT
            '.TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            '.TypeEditLen = RsRecruitment.Fields("SL_ATTENDED").DefinedSize
            '.TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .Value = CStr(System.Windows.Forms.CheckState.Unchecked)
            .set_ColWidth(.Col, 12)

            .Col = ColFeedBack
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRecruitment.Fields("SL_FEEDBACK").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 30)

            MainClass.SetSpreadColor(SprdSecond, Arow)
        End With

        With SprdFinal
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = ColDOJ
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeEditCharSet = SS_CELL_DATE_FORMAT_DDMMYY
            .TypeDateFormat = FPSpreadADO.TypeDateFormatConstants.TypeDateFormatDDMMYY
            .set_ColWidth(.Col, 15)
            .TypeEditMultiLine = True


            .Col = ColSalaryOff
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsRecruitment.Fields("SALARY_OFFERED").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(.Col, 20)


            .Col = ColHRRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsRecruitment.Fields("HR_REMARKS").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 45)

            MainClass.SetSpreadColor(SprdFinal, Arow)
        End With

        With SprdApproval
            .set_RowHeight(-1, ConRowHeight)
            .Row = Arow

            .Col = ColAppHead
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .Value = CStr(System.Windows.Forms.CheckState.Unchecked)
            .set_ColWidth(.Col, 30)


            .Col = ColAppTech
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .Value = CStr(System.Windows.Forms.CheckState.Unchecked)
            .set_ColWidth(.Col, 30)

            .Col = ColAppMD
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .Value = CStr(System.Windows.Forms.CheckState.Unchecked)
            .set_ColWidth(.Col, 30)

            MainClass.SetSpreadColor(SprdApproval, Arow)
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
            .set_ColWidth(1, 1000)
            .set_ColWidth(2, 1000)
            .ColsFrozen = 2
            .set_ColWidth(3, 3000)
            .set_ColWidth(4, 3000)
            .set_ColWidth(5, 1500)
            .set_ColWidth(6, 1000)
            .set_ColWidth(7, 1500)
            .set_ColWidth(8, 1500)
            .set_ColWidth(9, 1000)
            .set_ColWidth(10, 1500)
            .Col = 7
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT
            .Col = 10
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT

            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '''OperationModeSingle					
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub

    Private Sub SetTextLengths()
        On Error GoTo ERR1

        txtVNo.MaxLength = RsRecruitment.Fields("REF_NO").Precision
        txtVDate.MaxLength = RsRecruitment.Fields("REF_DATE").DefinedSize - 6


        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume					
    End Sub

    Private Function FieldsVarification() As Boolean
        On Error GoTo err_Renamed
        FieldsVarification = True
        If ValidateBranchLocking((txtVDate.Text)) = True Then
            FieldsVarification = False
            Exit Function
        End If


        If ADDMode = False And MODIFYMode = False And FieldsVarification Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And RsRecruitment.EOF = True Then Exit Function

        If MODIFYMode = True And Trim(txtVNo.Text) = "" Then
            MsgInformation("REf No. is Blank")
            FieldsVarification = False
            Exit Function
        End If


        If Trim(txtVDate.Text) = "" Then
            MsgInformation(" Ref Date is empty. Cannot Save")
            txtVDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtVDate.Text) <> "" Then
            If IsDate(txtVDate.Text) = False Then
                MsgInformation(" Invalid Ref Date. Cannot Save")
                txtVDate.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        '    If MainClass.ValidDataInGrid(sprdMain, ColSupplier, "S", "Please Check Supplier.") = False Then FieldsVarification = False: Exit Function					
        '    If MainClass.ValidDataInGrid(sprdMain, ColBillNo, "S", "Please Check Bill No.") = False Then FieldsVarification = False: Exit Function					
        '    If MainClass.ValidDataInGrid(sprdMain, ColBillDate, "N", "Please Check Bill Date.") = False Then FieldsVarification = False: Exit Function					
        '    If MainClass.ValidDataInGrid(sprdMain, ColBillAmt, "N", "Please Check Bill Amount.") = False Then FieldsVarification = False: Exit Function					
        '					
        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        'Resume					
    End Function

    Private Sub frmEmpRecruitmentEntry_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        Me.Hide()
        Me.Dispose()
        Me.Close()
        RsRecruitment.Close()
        'RsOpOuts.Close					
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change, SprdFirst.Change, SprdSecond.Change, SprdFinal.Change, SprdApproval.Change
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        txtVNo.Text = SprdView.Text

        TxtVNo_Validating(txtVNo, New System.ComponentModel.CancelEventArgs(False))
        ViewGrid()
    End Sub
    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdMain.ActiveCol, SprdMain.ActiveRow))
    End Sub
    Private Sub txtVDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtVDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtVDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtVDate.Text) Then
            MsgBox("Invalid REf Date.", MsgBoxStyle.Information)
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim mValue As String

        Clear1()
        If Not RsRecruitment.EOF Then
            With RsRecruitment
                lblMkey.Text = IIf(IsDBNull(.Fields("REF_NO").Value), "", .Fields("REF_NO").Value)  ''IIf(IsDBNull(.Fields("mKey").Value), "", .Fields("mKey").Value)
                txtVNo.Text = IIf(IsDBNull(.Fields("REF_NO").Value), "", .Fields("REF_NO").Value)
                txtVDate.Text = VB6.Format(IIf(IsDBNull(.Fields("REF_DATE").Value), "", .Fields("REF_DATE").Value), "DD/MM/YYYY")

                With SprdMain
                    .Row = 1
                    .Col = colApplicant
                    .Text = IIf(IsDBNull(RsRecruitment.Fields("APPLICANT_NAME").Value), "", RsRecruitment.Fields("APPLICANT_NAME").Value)

                    .Col = ColCurrOrg
                    .Text = IIf(IsDBNull(RsRecruitment.Fields("CURRENT_ORG").Value), "", RsRecruitment.Fields("CURRENT_ORG").Value)

                    .Col = ColCurrDept
                    .Text = IIf(IsDBNull(RsRecruitment.Fields("CURRENT_DEPT").Value), "", RsRecruitment.Fields("CURRENT_DEPT").Value)

                    .Col = ColCurrDesg
                    .Text = IIf(IsDBNull(RsRecruitment.Fields("CURRENT_DESG").Value), "", RsRecruitment.Fields("CURRENT_DESG").Value)

                    .Col = ColContactNo
                    .Text = IIf(IsDBNull(RsRecruitment.Fields("CONTACT_NO").Value), "", RsRecruitment.Fields("CONTACT_NO").Value)

                    .Col = ColEmail
                    .Text = IIf(IsDBNull(RsRecruitment.Fields("EMAIL_ID").Value), "", RsRecruitment.Fields("EMAIL_ID").Value)

                    .Col = ColLoc
                    .Text = IIf(IsDBNull(RsRecruitment.Fields("CURRENT_LOCATION").Value), "", RsRecruitment.Fields("CURRENT_LOCATION").Value)

                    .Col = ColExp
                    .Text = IIf(IsDBNull(RsRecruitment.Fields("TOTAL_EXP").Value), 0, RsRecruitment.Fields("TOTAL_EXP").Value)

                    .Col = ColRelExp
                    .Text = IIf(IsDBNull(RsRecruitment.Fields("RELEVENT_EXP").Value), 0, RsRecruitment.Fields("RELEVENT_EXP").Value)

                    .Col = ColSalary
                    .Text = IIf(IsDBNull(RsRecruitment.Fields("CURRENT_SALARY").Value), 0, RsRecruitment.Fields("CURRENT_SALARY").Value)

                    .Col = ColExpected
                    .Text = IIf(IsDBNull(RsRecruitment.Fields("EXPECTED_SALARY").Value), 0, RsRecruitment.Fields("EXPECTED_SALARY").Value)

                    .Col = ColNotice
                    .Text = IIf(IsDBNull(RsRecruitment.Fields("NOTICES_PERIOD").Value), "", RsRecruitment.Fields("NOTICES_PERIOD").Value)

                    .Col = ColInterested
                    mValue = IIf(IsDBNull(RsRecruitment.Fields("JOINING_INTERESTED").Value), "", RsRecruitment.Fields("JOINING_INTERESTED").Value)
                    .Value = IIf(mValue = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                    .Col = ColRemarks
                    .Text = IIf(IsDBNull(RsRecruitment.Fields("REMARKS").Value), "", RsRecruitment.Fields("REMARKS").Value)

                    .Col = ColScreen
                    .Text = IIf(IsDBNull(RsRecruitment.Fields("SCREENING").Value), "", RsRecruitment.Fields("SCREENING").Value)

                End With

                With SprdFirst
                    .Row = 1
                    .Col = ColDOI
                    .Text = VB6.Format(IIf(IsDBNull(RsRecruitment.Fields("FL_DOI").Value), "", RsRecruitment.Fields("FL_DOI").Value), "DD-MMM-YYYY")

                    .Col = ColModeInv
                    .Text = IIf(IsDBNull(RsRecruitment.Fields("FL_MOI").Value), "", RsRecruitment.Fields("FL_MOI").Value)

                    .Col = ColIntBy
                    .Text = IIf(IsDBNull(RsRecruitment.Fields("FL_INTERVIEW_BY").Value), "", RsRecruitment.Fields("FL_INTERVIEW_BY").Value)

                    .Col = ColAttned
                    mValue = IIf(IsDBNull(RsRecruitment.Fields("FL_ATTENDED").Value), "", RsRecruitment.Fields("FL_ATTENDED").Value)
                    .Value = IIf(mValue = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                    .Col = ColFeedBack
                    .Text = IIf(IsDBNull(RsRecruitment.Fields("FL_FEEDBACK").Value), "", RsRecruitment.Fields("FL_FEEDBACK").Value)

                End With

                With SprdSecond
                    .Row = 1
                    .Col = ColDOI
                    .Text = VB6.Format(IIf(IsDBNull(RsRecruitment.Fields("SL_DOI").Value), "", RsRecruitment.Fields("SL_DOI").Value), "DD-MMM-YYYY")

                    .Col = ColModeInv
                    .Text = IIf(IsDBNull(RsRecruitment.Fields("SL_MOI").Value), "", RsRecruitment.Fields("SL_MOI").Value)

                    .Col = ColIntBy
                    .Text = IIf(IsDBNull(RsRecruitment.Fields("SL_INTERVIEW_BY").Value), "", RsRecruitment.Fields("SL_INTERVIEW_BY").Value)

                    .Col = ColAttned
                    mValue = IIf(IsDBNull(RsRecruitment.Fields("SL_ATTENDED").Value), "", RsRecruitment.Fields("SL_ATTENDED").Value)
                    .Value = IIf(mValue = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                    .Col = ColFeedBack
                    .Text = IIf(IsDBNull(RsRecruitment.Fields("SL_FEEDBACK").Value), "", RsRecruitment.Fields("SL_FEEDBACK").Value)
                End With



                With SprdFinal
                    .Row = 1
                    .Col = ColDOJ
                    .Text = VB6.Format(IIf(IsDBNull(RsRecruitment.Fields("DOJ").Value), "", RsRecruitment.Fields("DOJ").Value), "DD-MMM-YYYY")

                    .Col = ColSalaryOff
                    .Text = IIf(IsDBNull(RsRecruitment.Fields("SALARY_OFFERED").Value), 0, RsRecruitment.Fields("SALARY_OFFERED").Value)

                    .Col = ColHRRemarks
                    .Text = IIf(IsDBNull(RsRecruitment.Fields("HR_REMARKS").Value), "", RsRecruitment.Fields("HR_REMARKS").Value)
                End With

                With SprdApproval
                    .Row = 1
                    .Col = ColAppHead
                    .Text = IIf(IsDBNull(RsRecruitment.Fields("HEAD_APP").Value), "", RsRecruitment.Fields("HEAD_APP").Value)
                    mValue = IIf(mValue = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                    .Col = ColAppTech
                    .Text = IIf(IsDBNull(RsRecruitment.Fields("TECH_APP").Value), "", RsRecruitment.Fields("TECH_APP").Value)
                    mValue = IIf(mValue = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                    .Col = ColAppMD
                    mValue = IIf(IsDBNull(RsRecruitment.Fields("MD_APP").Value), "", RsRecruitment.Fields("MD_APP").Value)
                    .Value = IIf(mValue = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                End With



                '            txtVNo.Enabled = False					

            End With

        End If
        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = False
        MainClass.ButtonStatus(Me, XRIGHT, RsRecruitment, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume					
    End Sub
    Private Sub txtVDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVDate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtVno_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtVNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtVNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Public Sub TxtVNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtVNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMKey As String
        Dim mVNo As String
        Dim SqlStr As String

        If Trim(txtVNo.Text) = "" Then GoTo EventExitSub
        mVNo = CStr(Val(txtVNo.Text))


        If MODIFYMode = True And RsRecruitment.BOF = False Then xMKey = RsRecruitment.Fields("mKey").Value

        SqlStr = "SELECT * FROM PAY_RECRUITMENT_MST " _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND REF_NO=" & Val(mVNo) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRecruitment, ADODB.LockTypeEnum.adLockReadOnly)
        If RsRecruitment.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such REf No. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM PAY_RECRUITMENT_MST WHERE " & vbCrLf _
                    & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                    & " AND REF_NO=" & Val(xMKey) & ""

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsRecruitment, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
