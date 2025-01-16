Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.PowerPacks
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmInitialPartTag
    Inherits System.Windows.Forms.Form
    Dim RsIPTMain As ADODB.Recordset
    Dim RsIPTDetail As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean

    Dim xMenuID As String

    Dim IsShowing As Boolean

    Private Const ConRowHeight As Short = 14

    Private Const ColDeptCode As Short = 1
    Private Const ColDeptDesc As Short = 2
    Private Const ColResponsibility As Short = 3
    Private Const ColInspection As Short = 4
    Private Const ColDate As Short = 5
    Private Const ColObservation As Short = 6
    Private Const ColMoveDeptCode As Short = 7
    Private Const ColMoveDeptDesc As Short = 8
    Private Const ColActionTaken As Short = 9
    Private Const ColActionDate As Short = 10

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
            If RsIPTMain.EOF = False Then RsIPTMain.MoveFirst()
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
        If Not RsIPTMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "QAL_IPT_HDR", (txtSlipNo.Text), RsIPTMain) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM QAL_IPT_DET WHERE AUTO_KEY_IPT=" & Val(lblMkey.Text) & "")
                PubDBCn.Execute("DELETE FROM QAL_IPT_HDR WHERE AUTO_KEY_IPT=" & Val(lblMkey.Text) & "")
                PubDBCn.CommitTrans()
                RsIPTMain.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
        RsIPTMain.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr


        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsIPTMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
        Dim mNEWSUPPLIER As String
        Dim mDESIGNCHANGE As String
        Dim mENGGCHANGE As String
        Dim mMTRLCHANGE As String
        Dim mPROCESSCHANGE As String
        Dim mSUBVENDORCHANGE As String
        Dim mJIGTOOLCHANGE As String
        Dim mDIEMOULDCHANGE As String
        Dim mTRANSPORTMTH As String
        Dim mPAKAGINGCHANGE As String
        Dim mMACHINECHANGE As String
        Dim mINSPGAUGECHANGE As String
        Dim mOTHERDETAIL As String

        Dim mQI As String
        Dim mDisSupp As String
        Dim mManpower As String
        Dim mOffLoad As String
        Dim mShiftNewLoc As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mNEWSUPPLIER = IIf(optNewSupp.Checked = True, "Y", "N")
        mDESIGNCHANGE = IIf(optDsgnChng.Checked = True, "Y", "N")
        mENGGCHANGE = IIf(optEnggChng.Checked = True, "Y", "N")
        mMTRLCHANGE = IIf(optMtrlChng.Checked = True, "Y", "N")
        mPROCESSCHANGE = IIf(optProChng.Checked = True, "Y", "N")
        mSUBVENDORCHANGE = IIf(optSubChng.Checked = True, "Y", "N")
        mJIGTOOLCHANGE = IIf(optJigChng.Checked = True, "Y", "N")
        mDIEMOULDCHANGE = IIf(optDieChng.Checked = True, "Y", "N")
        mTRANSPORTMTH = IIf(optTransMthd.Checked = True, "Y", "N")
        mPAKAGINGCHANGE = IIf(optPakChng.Checked = True, "Y", "N")
        mMACHINECHANGE = IIf(optMacChng.Checked = True, "Y", "N")
        mINSPGAUGECHANGE = IIf(optInspChng.Checked = True, "Y", "N")
        mOTHERDETAIL = IIf(optOther.Checked = True, "Y", "N")

        mQI = IIf(optQI.Checked = True, "Y", "N")
        mDisSupp = IIf(optDisSupp.Checked = True, "Y", "N")
        mManpower = IIf(optManpower.Checked = True, "Y", "N")
        mOffLoad = IIf(optOffLoad.Checked = True, "Y", "N")
        mShiftNewLoc = IIf(optShiftNewLoc.Checked = True, "Y", "N")



        SqlStr = ""
        mSlipNo = Val(txtSlipNo.Text)
        If Val(txtSlipNo.Text) = 0 Then
            mSlipNo = AutoGenKeyNo()
        End If
        txtSlipNo.Text = CStr(mSlipNo)
        If ADDMode = True Then
            lblMkey.Text = CStr(mSlipNo)
            SqlStr = " INSERT INTO QAL_IPT_HDR " & vbCrLf _
                            & " (AUTO_KEY_IPT,COMPANY_CODE,FYEAR," & vbCrLf _
                            & " IPLDATE,PARTNO,PARTDESC,MRRNO,MRRDATE,CHALLANNO,CHALLANDATE," & vbCrLf _
                            & " ECNNO,ECNDATE,SUPP_CUST_CODE,MODEL,QUANTITY,INITBY,INITDATE, " & vbCrLf _
                            & " CLOSEATDEPT,CLOSEBY,CLOSEDATE,NEWSUPPLIER,DESIGNCHANGE,ENGGCHANGE,MTRLCHANGE, " & vbCrLf _
                            & " PROCESSCHANGE,SUBVENDORCHANGE,JIGTOOLCHANGE,DIEMOULDCHANGE,TRANSPORTMTH, " & vbCrLf _
                            & " PAKAGINGCHANGE,MACHINECHANGE,INSPGAUGECHANGE,OTHERDETAIL,REMARKS," & vbCrLf _
                            & " QUALITY_IMPROV, DIS_SUPP, MANPOWER, OFFLOAD, SHIFT_NEW_LOC, " & vbCrLf _
                            & " ADDUSER, ADDDATE, MODUSER, MODDATE) " & vbCrLf _
                            & " VALUES ( " & vbCrLf _
                            & " " & mSlipNo & "," & RsCompany.Fields("COMPANY_CODE").Value & "," & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'" & MainClass.AllowSingleQuote(txtPartNo.Text) & "','" & MainClass.AllowSingleQuote(txtPartDesc.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtMRRNo.Text) & "',TO_DATE('" & vb6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtChallanNo.Text) & "',TO_DATE('" & vb6.Format(txtChallanDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtECNNo.Text) & "',TO_DATE('" & vb6.Format(txtECNDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtCustomer.Text) & "','" & MainClass.AllowSingleQuote(txtModel.Text) & "', " & vbCrLf _
                            & " " & Val(txtQuantity.Text) & ",'" & MainClass.AllowSingleQuote(txtInitiatedBy.Text) & "',TO_DATE('" & vb6.Format(txtInitiatedDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtCloseDept.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtClosedBy.Text) & "',TO_DATE('" & vb6.Format(txtClosedDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                            & " '" & mNEWSUPPLIER & "','" & mDESIGNCHANGE & "','" & mENGGCHANGE & "','" & mMTRLCHANGE & "', " & vbCrLf _
                            & " '" & mPROCESSCHANGE & "','" & mSUBVENDORCHANGE & "','" & mJIGTOOLCHANGE & "','" & mDIEMOULDCHANGE & "', " & vbCrLf _
                            & " '" & mTRANSPORTMTH & "','" & mPAKAGINGCHANGE & "','" & mMACHINECHANGE & "','" & mINSPGAUGECHANGE & "','" & mOTHERDETAIL & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                            & " '" & mQI & "','" & mDisSupp & "','" & mManpower & "','" & mOffLoad & "','" & mShiftNewLoc & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"


        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE QAL_IPT_HDR SET " & vbCrLf _
                    & " AUTO_KEY_IPT=" & mSlipNo & ", " & vbCrLf _
                    & " IPLDATE=TO_DATE('" & vb6.Format(txtDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),PARTNO='" & MainClass.AllowSingleQuote(txtPartNo.Text) & "', " & vbCrLf _
                    & " PARTDESC='" & MainClass.AllowSingleQuote(txtPartDesc.Text) & "',MRRNO='" & MainClass.AllowSingleQuote(txtMRRNo.Text) & "', " & vbCrLf _
                    & " MRRDATE=TO_DATE('" & vb6.Format(txtMRRDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),CHALLANNO='" & MainClass.AllowSingleQuote(txtChallanNo.Text) & "', " & vbCrLf _
                    & " CHALLANDATE=TO_DATE('" & vb6.Format(txtChallanDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " ECNNO='" & MainClass.AllowSingleQuote(txtECNNo.Text) & "',ECNDATE=TO_DATE('" & vb6.Format(txtECNDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCustomer.Text) & "',MODEL='" & MainClass.AllowSingleQuote(txtModel.Text) & "', " & vbCrLf _
                    & " QUANTITY=" & Val(txtQuantity.Text) & ",INITBY='" & MainClass.AllowSingleQuote(txtInitiatedBy.Text) & "', " & vbCrLf _
                    & " INITDATE=TO_DATE('" & vb6.Format(txtInitiatedDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " CLOSEATDEPT='" & MainClass.AllowSingleQuote(txtCloseDept.Text) & "',CLOSEBY='" & MainClass.AllowSingleQuote(txtClosedBy.Text) & "', " & vbCrLf _
                    & " CLOSEDATE=TO_DATE('" & vb6.Format(txtClosedDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),NEWSUPPLIER='" & mNEWSUPPLIER & "',DESIGNCHANGE='" & mDESIGNCHANGE & "', " & vbCrLf _
                    & " ENGGCHANGE='" & mENGGCHANGE & "',MTRLCHANGE='" & mMTRLCHANGE & "', " & vbCrLf _
                    & " PROCESSCHANGE='" & mPROCESSCHANGE & "',SUBVENDORCHANGE='" & mSUBVENDORCHANGE & "', " & vbCrLf _
                    & " JIGTOOLCHANGE='" & mJIGTOOLCHANGE & "',DIEMOULDCHANGE='" & mDIEMOULDCHANGE & "', " & vbCrLf _
                    & " TRANSPORTMTH='" & mTRANSPORTMTH & "', " & vbCrLf _
                    & " PAKAGINGCHANGE='" & mPAKAGINGCHANGE & "',MACHINECHANGE='" & mMACHINECHANGE & "', " & vbCrLf _
                    & " INSPGAUGECHANGE='" & mINSPGAUGECHANGE & "',OTHERDETAIL='" & mOTHERDETAIL & "', " & vbCrLf _
                    & " QUALITY_IMPROV='" & mQI & "',DIS_SUPP='" & mDisSupp & "', " & vbCrLf _
                    & " MANPOWER='" & mManpower & "',OFFLOAD='" & mOffLoad & "', " & vbCrLf _
                    & " SHIFT_NEW_LOC='" & mShiftNewLoc & "', "

            SqlStr = SqlStr & vbCrLf & " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTO_KEY_IPT =" & Val(lblMkey.Text) & ""
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
        RsIPTMain.Requery()
        RsIPTDetail.Requery()
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
        SqlStr = "SELECT Max(AUTO_KEY_IPT)  " & vbCrLf & " FROM QAL_IPT_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""

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
        Dim mDeptCode As String
        Dim mResponsibility As String
        Dim mInspection As String
        Dim mDate As String
        Dim mObservation As String
        Dim mMoveDeptCode As String
        Dim mActionTaken As String
        Dim mActionDate As String



        PubDBCn.Execute("DELETE FROM QAL_IPT_DET WHERE AUTO_KEY_IPT=" & Val(lblMkey.Text) & "")

        With SprdMain
            For I = 1 To .MaxRows
                .Row = I

                .Col = ColDeptCode
                mDeptCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColResponsibility
                mResponsibility = MainClass.AllowSingleQuote(.Text)

                .Col = ColInspection
                mInspection = MainClass.AllowSingleQuote(.Text)

                .Col = ColDate
                mDate = VB6.Format(.Text, "DD/MMM/YYYY")

                .Col = ColObservation
                mObservation = MainClass.AllowSingleQuote(.Text)

                .Col = ColMoveDeptCode
                mMoveDeptCode = MainClass.AllowSingleQuote(.Text)

                .Col = ColActionTaken
                mActionTaken = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

                .Col = ColActionDate
                mActionDate = VB6.Format(.Text, "DD/MMM/YYYY")

                If Trim(mActionDate) = "" Then
                    mActionDate = VB6.Format(IIf(mActionTaken = "Y", PubCurrDate, ""), "DD/MMM/YYYY")
                End If

                SqlStr = ""

                If Trim(mDeptCode) <> "" Then
                    SqlStr = " INSERT INTO  QAL_IPT_DET ( " & vbCrLf & " COMPANY_CODE,AUTO_KEY_IPT,SERIAL_NO,DEPTCODE,RESPONSIBILITY,INSPECTION,INSPDATE, " & vbCrLf & " OBSERVATION,MOVETODEPT,ACTIONTAKEN,ACTIONDATE ) " & vbCrLf & " VALUES ( " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & "," & Val(lblMkey.Text) & "," & I & ",'" & mDeptCode & "','" & mResponsibility & "', " & vbCrLf & " '" & mInspection & "','" & mDate & "','" & mObservation & "','" & mMoveDeptCode & "','" & mActionTaken & "','" & mActionDate & "' )"
                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With
        UpdateDetail = True
        Exit Function
UpdateDetailERR:
        UpdateDetail = False
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Function

    Private Sub cmdSearchInitiated_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchInitiated.Click
        Call SearchEmp(txtInitiatedBy, lblInitiatedBy)
    End Sub

    Private Sub cmdSearchClosed_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchClosed.Click
        Call SearchEmp(txtClosedBy, lblClosedBy)
    End Sub
    Private Sub SearchEmp(ByRef pTextBax As System.Windows.Forms.TextBox, ByRef pLable As System.Windows.Forms.Label)
        On Error GoTo SrchERR
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND EMP_LEAVE_DATE IS NULL "
        If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            pTextBax.Text = AcName1
            pLable.text = AcName
            If pTextBax.Enabled = True Then pTextBax.Focus()
        End If
        Exit Sub
SrchERR:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdSearchCust_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchCust.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
        If MainClass.SearchGridMaster("", "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE", , , SqlStr) = True Then
            txtCustomer.Text = AcName1
            lblCustomer.text = AcName
            If txtCustomer.Enabled = True Then txtCustomer.Focus()
        End If
    End Sub
    Private Sub cmdSearchSlipNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchSlipNo.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
        If MainClass.SearchGridMaster(txtSlipNo.Text, "QAL_IPT_HDR", "AUTO_KEY_IPT", "IPLDATE", "PARTNO", "PARTDESC", SqlStr) = True Then
            txtSlipNo.Text = AcName
            Call txtSlipNo_Validating(txtSlipNo, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
        End If
    End Sub

    Private Sub cmdSearchCloseDept_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchCloseDept.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", "", "", SqlStr) = True Then
            txtCloseDept.Text = AcName1
            lblCloseDept.text = AcName
            If txtCloseDept.Enabled = True Then txtCloseDept.Focus()
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
        MainClass.ButtonStatus(Me, XRIGHT, RsIPTMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmInitialPartTag_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Initial Part Tag"

        SqlStr = "Select * From QAL_IPT_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsIPTMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From QAL_IPT_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsIPTDetail, ADODB.LockTypeEnum.adLockReadOnly)

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

        SqlStr = " SELECT " & vbCrLf & " AUTO_KEY_IPT AS SLIP_NUMBER,TO_CHAR(IPLDATE,'DD/MM/YYYY') AS IPL_DATE, " & vbCrLf & " PARTNO,PARTDESC,SUPP_CUST_CODE,MODEL,QUANTITY  " & vbCrLf & " FROM QAL_IPT_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " ORDER BY AUTO_KEY_IPT"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmInitialPartTag_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmInitialPartTag_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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
        'Me.Height = VB6.TwipsToPixelsY(7245)
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
        txtPartNo.Text = ""
        txtPartDesc.Text = ""
        txtMRRNo.Text = ""
        txtMRRDate.Text = ""
        txtChallanNo.Text = ""
        txtChallanDate.Text = ""
        txtECNNo.Text = ""
        txtECNDate.Text = ""
        txtCustomer.Text = ""
        lblCustomer.Text = ""
        txtModel.Text = ""
        txtQuantity.Text = ""
        txtInitiatedBy.Text = ""
        lblInitiatedBy.Text = ""
        txtInitiatedDate.Text = ""
        txtCloseDept.Text = ""
        lblCloseDept.Text = ""
        txtClosedBy.Text = ""
        lblClosedBy.Text = ""
        txtClosedDate.Text = ""

        optNewSupp.Checked = False
        optDsgnChng.Checked = False
        optEnggChng.Checked = False
        optMtrlChng.Checked = False
        optProChng.Checked = False
        optSubChng.Checked = False
        optJigChng.Checked = False
        optDieChng.Checked = False
        optTransMthd.Checked = False
        optPakChng.Checked = False
        optMacChng.Checked = False
        optInspChng.Checked = False

        optQI.Checked = False
        optDisSupp.Checked = False
        optManpower.Checked = False
        optOffLoad.Checked = False
        optShiftNewLoc.Checked = False

        optOther.Checked = True
        txtRemarks.Text = ""
        SSTab1.SelectedIndex = 0
        Call MakeEnableDesableField(True)
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)
        MainClass.ButtonStatus(Me, XRIGHT, RsIPTMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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

            .Col = ColDeptCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsIPTDetail.Fields("DEPTCODE").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColDeptCode, 6.5)

            .Col = ColDeptDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = 255
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .ColHidden = True

            .Col = ColResponsibility
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsIPTDetail.Fields("RESPONSIBILITY").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColResponsibility, 20)
            .ColHidden = False

            .Col = ColInspection
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsIPTDetail.Fields("INSPECTION").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColInspection, 20)
            .ColHidden = True

            .Col = ColDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeEditCharSet = SS_CELL_DATE_FORMAT_DDMMYY
            .set_ColWidth(ColDate, 7.5)
            .ColHidden = True

            .Col = ColObservation
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsIPTDetail.Fields("OBSERVATION").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColObservation, 20)
            .ColHidden = False

            .Col = ColMoveDeptCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsIPTDetail.Fields("MOVETODEPT").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .set_ColWidth(ColMoveDeptCode, 6.5)
            .ColHidden = True

            .Col = ColMoveDeptDesc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = 255
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .ColHidden = True


            .Col = ColActionTaken
            .CellType = SS_CELL_TYPE_CHECKBOX

            .Col = ColActionDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeEditCharSet = SS_CELL_DATE_FORMAT_DDMMYY
            .set_ColWidth(ColDate, 7.5)
            If IsShowing = True Then
                MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColDeptCode, ColDeptCode)
            Else
                MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, ColDeptCode, ColDeptCode)
            End If
            MainClass.SetSpreadColor(SprdMain, Arow)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColDeptDesc, ColDeptDesc)
            MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColMoveDeptDesc, ColMoveDeptDesc)
            If PubSuperUser = "S" Or PubSuperUser = "A" Then
                '            MainClass.ProtectCell SprdMain, 1, SprdMain.MaxRows, ColActionDate, ColActionDate
            Else
                MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColActionDate, ColActionDate)
            End If

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
            .set_ColWidth(4, 500 * 5)
            .set_ColWidth(5, 500 * 3)
            .set_ColWidth(6, 500 * 4)
            .set_ColWidth(7, 500 * 2)

            .ColsFrozen = 2
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1

        txtSlipNo.Maxlength = RsIPTMain.Fields("AUTO_KEY_IPT").Precision
        txtDate.Maxlength = RsIPTMain.Fields("IPLDATE").DefinedSize - 6
        txtPartNo.Maxlength = RsIPTMain.Fields("PARTNO").DefinedSize
        txtPartDesc.Maxlength = RsIPTMain.Fields("PARTDESC").DefinedSize
        txtMRRNo.Maxlength = RsIPTMain.Fields("MRRNO").DefinedSize
        txtMRRDate.Maxlength = RsIPTMain.Fields("MRRDATE").DefinedSize - 6
        txtChallanNo.Maxlength = RsIPTMain.Fields("CHALLANNO").DefinedSize
        txtChallanDate.Maxlength = RsIPTMain.Fields("CHALLANDATE").DefinedSize - 6
        txtECNNo.Maxlength = RsIPTMain.Fields("ECNNO").DefinedSize
        txtECNDate.Maxlength = RsIPTMain.Fields("ECNDATE").DefinedSize - 6
        txtCustomer.Maxlength = RsIPTMain.Fields("SUPP_CUST_CODE").DefinedSize
        txtModel.Maxlength = RsIPTMain.Fields("MODEL").DefinedSize
        txtQuantity.Maxlength = RsIPTMain.Fields("QUANTITY").Precision - 4
        txtInitiatedBy.Maxlength = RsIPTMain.Fields("INITBY").DefinedSize
        txtInitiatedDate.Maxlength = RsIPTMain.Fields("INITDATE").DefinedSize - 6
        txtCloseDept.Maxlength = RsIPTMain.Fields("CLOSEATDEPT").DefinedSize
        txtClosedBy.Maxlength = RsIPTMain.Fields("CLOSEBY").DefinedSize
        txtClosedDate.Maxlength = RsIPTMain.Fields("CLOSEDATE").DefinedSize - 6
        txtRemarks.Maxlength = RsIPTMain.Fields("REMARKS").DefinedSize
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
        If MODIFYMode = True And RsIPTMain.EOF = True Then Exit Function

        If Trim(txtDate.Text) = "" Then
            MsgInformation("Date is empty, So unable to save.")
            txtDate.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtPartNo.Text) = "" Then
            MsgInformation("Part No is empty, So unable to save.")
            txtPartNo.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtPartDesc.Text) = "" Then
            MsgInformation("Part Description is empty, So unable to save.")
            txtPartDesc.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtCustomer.Text) = "" Then
            MsgInformation("Customer is empty, So unable to save.")
            txtCustomer.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtModel.Text) = "" Then
            MsgInformation("Model is empty, So unable to save.")
            txtModel.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Val(txtQuantity.Text) <= 0 Then
            MsgInformation("Quantity is empty, So unable to save.")
            txtQuantity.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtInitiatedBy.Text) = "" Then
            MsgInformation("Initialted By is empty, So unable to save.")
            txtInitiatedBy.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtInitiatedDate.Text) = "" Then
            MsgInformation("Initialted Date is empty, So unable to save.")
            txtInitiatedDate.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If optOther.Checked = True Then
            If Trim(txtRemarks.Text) = "" Then
                MsgInformation("Details Of Modification is empty, So unable to save.")
                txtRemarks.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If MainClass.ValidDataInGrid(SprdMain, ColDeptCode, "S", "Please Check Deptt Routing.") = False Then FieldsVarification = False : Exit Function

        '    If MainClass.ValidDataInGrid(SprdMain, ColResponsibility, "S", "Please Check Responsibility.") = False Then FieldsVarification = False: Exit Function

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        Resume
    End Function

    Private Sub frmInitialPartTag_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        RsIPTMain.Close()
        RsIPTMain = Nothing
        RsIPTDetail.Close()
        RsIPTDetail = Nothing
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub optDieChng_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optDieChng.CheckedChanged
        If eventSender.Checked Then

            MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub optDisSupp_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optDisSupp.CheckedChanged
        If eventSender.Checked Then

            MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
        End If
    End Sub


    Private Sub optDsgnChng_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optDsgnChng.CheckedChanged
        If eventSender.Checked Then

            MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub optEnggChng_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optEnggChng.CheckedChanged
        If eventSender.Checked Then

            MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
        End If
    End Sub


    Private Sub optInspChng_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optInspChng.CheckedChanged
        If eventSender.Checked Then

            MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub optJigChng_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optJigChng.CheckedChanged
        If eventSender.Checked Then

            MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub optMacChng_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optMacChng.CheckedChanged
        If eventSender.Checked Then

            MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub optManpower_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optManpower.CheckedChanged
        If eventSender.Checked Then

            MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub optMtrlChng_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optMtrlChng.CheckedChanged
        If eventSender.Checked Then

            MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub optNewSupp_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optNewSupp.CheckedChanged
        If eventSender.Checked Then

            MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub optOffLoad_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optOffLoad.CheckedChanged
        If eventSender.Checked Then

            MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub optOther_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optOther.CheckedChanged
        If eventSender.Checked Then

            MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
        End If
    End Sub


    Private Sub optPakChng_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optPakChng.CheckedChanged
        If eventSender.Checked Then

            MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub optProChng_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optProChng.CheckedChanged
        If eventSender.Checked Then

            MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
        End If
    End Sub


    Private Sub optQI_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optQI.CheckedChanged
        If eventSender.Checked Then

            MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub optShiftNewLoc_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optShiftNewLoc.CheckedChanged
        If eventSender.Checked Then

            MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub optSubChng_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optSubChng.CheckedChanged
        If eventSender.Checked Then

            MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub optTransMthd_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optTransMthd.CheckedChanged
        If eventSender.Checked Then

            MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
        End If
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim SqlStr As String

        If eventArgs.Row = 0 And eventArgs.Col = ColDeptCode Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColDeptCode

                If MainClass.SearchGridMaster(.Text, "PAY_DEPT_MST", "DEPT_CODE", "DEPT_DESC", , , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
                    .Row = .ActiveRow

                    .Col = ColDeptCode
                    .Text = Trim(AcName)

                    .Col = ColDeptDesc
                    .Text = Trim(AcName1)
                End If
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColDeptCode, .ActiveRow, ColDeptCode, .ActiveRow, False))
            End With
        End If

        If eventArgs.Row = 0 And eventArgs.Col = ColDeptDesc Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColDeptDesc

                If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
                    .Row = .ActiveRow

                    .Col = ColDeptCode
                    .Text = Trim(AcName1)

                    .Col = ColDeptDesc
                    .Text = Trim(AcName)
                End If
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColDeptCode, .ActiveRow, ColDeptCode, .ActiveRow, False))
            End With
        End If
        If eventArgs.Row = 0 And eventArgs.Col = ColMoveDeptCode Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColMoveDeptCode

                If MainClass.SearchGridMaster(.Text, "PAY_DEPT_MST", "DEPT_CODE", "DEPT_DESC", , , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
                    .Row = .ActiveRow

                    .Col = ColMoveDeptCode
                    .Text = Trim(AcName)

                    .Col = ColMoveDeptDesc
                    .Text = Trim(AcName1)
                End If
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColMoveDeptCode, .ActiveRow, ColMoveDeptCode, .ActiveRow, False))
            End With
        End If

        If eventArgs.Row = 0 And eventArgs.Col = ColMoveDeptDesc Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColMoveDeptDesc

                If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , "COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & "") = True Then
                    .Row = .ActiveRow

                    .Col = ColMoveDeptCode
                    .Text = Trim(AcName1)

                    .Col = ColMoveDeptDesc
                    .Text = Trim(AcName)
                End If
                Call SprdMain_LeaveCell(SprdMain, New AxFPSpreadADO._DSpreadEvents_LeaveCellEvent(ColMoveDeptCode, .ActiveRow, ColMoveDeptCode, .ActiveRow, False))
            End With
        End If

        If eventArgs.Col = 0 And eventArgs.Row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            If MODIFYMode = False Then
                MainClass.DeleteSprdRow(SprdMain, eventArgs.Row, ColDeptCode)
                MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
            End If
        End If
        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdMain_ColWidthChange(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ColWidthChangeEvent) Handles SprdMain.ColWidthChange
        With SprdMain
            MsgBox(.get_ColWidth(eventArgs.Col1))
        End With
    End Sub

    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short
        mCol = SprdMain.ActiveCol
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColDeptCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColDeptCode, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColDeptDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColDeptDesc, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColMoveDeptCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColMoveDeptCode, 0))
        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColMoveDeptDesc Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColMoveDeptDesc, 0))
        SprdMain.Refresh()
    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart

        If eventArgs.NewRow = -1 Then Exit Sub

        SprdMain.Row = SprdMain.ActiveRow
        SprdMain.Col = ColDeptCode
        If Trim(SprdMain.Text) = "" Then Exit Sub

        Select Case eventArgs.col
            Case ColDeptCode
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColDeptCode
                If Trim(SprdMain.Text) = "" Then Exit Sub
                If CheckDeptt(ColDeptCode, (SprdMain.ActiveRow), ColDeptDesc) = True Then
                    MainClass.AddBlankSprdRow(SprdMain, ColDeptCode, ConRowHeight)
                    FormatSprdMain((SprdMain.MaxRows))
                End If
            Case ColMoveDeptCode
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = ColMoveDeptCode
                If Trim(SprdMain.Text) = "" Then Exit Sub
                Call CheckDeptt(ColMoveDeptCode, (SprdMain.ActiveRow), ColMoveDeptDesc)
        End Select
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function CheckDeptt(ByRef pCol As Integer, ByRef pRow As Integer, ByRef pColDeptDesc As Integer) As Boolean

        On Error GoTo CheckDeptERR
        Dim SqlStr As String
        CheckDeptt = True
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        With SprdMain
            .Row = pRow
            .Col = pCol

            If Trim(.Text) = "" Then Exit Function
            If MainClass.ValidateWithMasterTable(.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , SqlStr) = False Then
                CheckDeptt = False
                MainClass.SetFocusToCell(SprdMain, pRow, pCol)
            Else
                .Row = pRow
                .Col = pColDeptDesc
                .Text = MasterNo
            End If
        End With
        Exit Function
CheckDeptERR:
        CheckDeptt = False
        MsgBox(Err.Description)
    End Function

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


    Private Sub txtChallanDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtChallanDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtChallanDate) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtClosedDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtClosedDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtClosedDate) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtECNDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtECNDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtECNDate) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtInitiatedBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInitiatedBy.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtInitiatedBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInitiatedBy.DoubleClick
        Call cmdSearchInitiated_Click(cmdSearchInitiated, New System.EventArgs())
    End Sub

    Private Sub txtInitiatedBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtInitiatedBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchInitiated_Click(cmdSearchInitiated, New System.EventArgs())
    End Sub

    Private Sub txtInitiatedBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtInitiatedBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If ValidateEMP(txtInitiatedBy, lblInitiatedBy) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtClosedBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtClosedBy.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtClosedBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtClosedBy.DoubleClick
        Call cmdSearchClosed_Click(cmdSearchClosed, New System.EventArgs())
    End Sub

    Private Sub txtClosedBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtClosedBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchClosed_Click(cmdSearchClosed, New System.EventArgs())
    End Sub

    Private Sub txtClosedBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtClosedBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If ValidateEMP(txtClosedBy, lblClosedBy) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtInitiatedDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtInitiatedDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtInitiatedDate) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Function CheckDate(ByRef pTxt As System.Windows.Forms.TextBox) As Boolean
        CheckDate = True
        If Trim(pTxt.Text) = "" Then Exit Function
        If Not IsDate(pTxt.Text) Then
            MsgBox("Not a valid date.")
            CheckDate = False
            Exit Function
        End If

        If pTxt.Name = txtDate.Name Then
            If Trim(txtDate.Text) <> "" And Trim(txtInitiatedDate.Text) <> "" Then
                If CDate(txtDate.Text) > CDate(txtInitiatedDate.Text) Then
                    MsgBox("Date cann't be greater than Initialted Date")
                    CheckDate = False
                    Exit Function
                End If
            End If
            If Trim(txtDate.Text) <> "" And Trim(txtClosedDate.Text) <> "" Then
                If CDate(txtDate.Text) > CDate(txtClosedDate.Text) Then
                    MsgBox("Date cann't be greater than Closed Date")
                    CheckDate = False
                    Exit Function
                End If
            End If
        ElseIf pTxt.Name = txtInitiatedDate.Name Then
            If Trim(txtDate.Text) <> "" And Trim(txtInitiatedDate.Text) <> "" Then
                If CDate(txtInitiatedDate.Text) < CDate(txtDate.Text) Then
                    MsgBox("Initiated Date cann't be less than Date")
                    CheckDate = False
                    Exit Function
                End If
            End If
            If Trim(txtClosedDate.Text) <> "" And Trim(txtInitiatedDate.Text) <> "" Then
                If CDate(txtInitiatedDate.Text) > CDate(txtClosedDate.Text) Then
                    MsgBox("Initiated Date cann't be greater than Closed Date")
                    CheckDate = False
                    Exit Function
                End If
            End If
        ElseIf pTxt.Name = txtClosedDate.Name Then
            If Trim(txtDate.Text) <> "" And Trim(txtClosedDate.Text) <> "" Then
                If CDate(txtClosedDate.Text) < CDate(txtDate.Text) Then
                    MsgBox("Closed Date cann't be less than Date")
                    CheckDate = False
                    Exit Function
                End If
            End If
            If Trim(txtClosedDate.Text) <> "" And Trim(txtInitiatedDate.Text) <> "" Then
                If CDate(txtInitiatedDate.Text) > CDate(txtClosedDate.Text) Then
                    MsgBox("Closed Date cann't be less than Initiated Date")
                    CheckDate = False
                    Exit Function
                End If
            End If
        End If

    End Function

    Private Sub txtMRRDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMRRDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtMRRDate) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPartNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPartNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub



    Private Sub txtCustomer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustomer.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCustomer_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustomer.DoubleClick
        Call cmdSearchCust_Click(cmdSearchCust, New System.EventArgs())
    End Sub

    Private Sub txtCustomer_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCustomer.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchCust_Click(cmdSearchCust, New System.EventArgs())
    End Sub

    Private Sub txtCustomer_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCustomer.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim SqlStr As String
        If Trim(txtCustomer.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable(txtCustomer.Text, "SUPP_CUST_CODE", "SUPP_CUST_NAME", "FIN_SUPP_CUST_MST", PubDBCn, MasterNo, , " COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " ") = False Then
            MsgBox("Customer Doesn't Exist In Master")
            Cancel = True
        Else
            lblCustomer.text = MasterNo
        End If

EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDate.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If CheckDate(txtDate) = False Then Cancel = True
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtPartDesc_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPartDesc.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
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


    Private Sub txtRemarks_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRemarks.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtSlipNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSlipNo.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim mItemCode As String
        Dim mSuppCode As String


        If Not RsIPTMain.EOF Then
            IsShowing = True
            lblMkey.Text = IIf(IsDbNull(RsIPTMain.Fields("AUTO_KEY_IPT").Value), "", RsIPTMain.Fields("AUTO_KEY_IPT").Value)
            txtSlipNo.Text = IIf(IsDbNull(RsIPTMain.Fields("AUTO_KEY_IPT").Value), "", RsIPTMain.Fields("AUTO_KEY_IPT").Value)
            txtDate.Text = IIf(IsDbNull(RsIPTMain.Fields("IPLDATE").Value), "", RsIPTMain.Fields("IPLDATE").Value)
            txtPartNo.Text = IIf(IsDbNull(RsIPTMain.Fields("PARTNO").Value), "", RsIPTMain.Fields("PARTNO").Value)
            txtPartDesc.Text = IIf(IsDbNull(RsIPTMain.Fields("PARTDESC").Value), "", RsIPTMain.Fields("PARTDESC").Value)
            txtMRRNo.Text = IIf(IsDbNull(RsIPTMain.Fields("MRRNO").Value), "", RsIPTMain.Fields("MRRNO").Value)
            txtMRRDate.Text = IIf(IsDbNull(RsIPTMain.Fields("MRRDATE").Value), "", RsIPTMain.Fields("MRRDATE").Value)
            txtChallanNo.Text = IIf(IsDbNull(RsIPTMain.Fields("CHALLANNO").Value), "", RsIPTMain.Fields("CHALLANNO").Value)
            txtChallanDate.Text = IIf(IsDbNull(RsIPTMain.Fields("CHALLANDATE").Value), "", RsIPTMain.Fields("CHALLANDATE").Value)
            txtECNNo.Text = IIf(IsDbNull(RsIPTMain.Fields("ECNNO").Value), "", RsIPTMain.Fields("ECNNO").Value)
            txtECNDate.Text = IIf(IsDbNull(RsIPTMain.Fields("ECNDATE").Value), "", RsIPTMain.Fields("ECNDATE").Value)
            txtCustomer.Text = IIf(IsDbNull(RsIPTMain.Fields("SUPP_CUST_CODE").Value), "", RsIPTMain.Fields("SUPP_CUST_CODE").Value)
            txtCustomer_Validating(txtCustomer, New System.ComponentModel.CancelEventArgs(False))
            txtModel.Text = IIf(IsDbNull(RsIPTMain.Fields("Model").Value), "", RsIPTMain.Fields("Model").Value)
            txtQuantity.Text = IIf(IsDbNull(RsIPTMain.Fields("QUANTITY").Value), "", RsIPTMain.Fields("QUANTITY").Value)
            txtInitiatedBy.Text = IIf(IsDbNull(RsIPTMain.Fields("INITBY").Value), "", RsIPTMain.Fields("INITBY").Value)
            txtInitiatedBy_Validating(txtInitiatedBy, New System.ComponentModel.CancelEventArgs(False))
            txtInitiatedDate.Text = IIf(IsDbNull(RsIPTMain.Fields("INITDATE").Value), "", RsIPTMain.Fields("INITDATE").Value)
            txtCloseDept.Text = IIf(IsDbNull(RsIPTMain.Fields("CLOSEATDEPT").Value), "", RsIPTMain.Fields("CLOSEATDEPT").Value)
            txtCloseDept_Validating(txtCloseDept, New System.ComponentModel.CancelEventArgs(False))
            txtClosedBy.Text = IIf(IsDbNull(RsIPTMain.Fields("CLOSEBY").Value), "", RsIPTMain.Fields("CLOSEBY").Value)
            txtClosedBy_Validating(txtClosedBy, New System.ComponentModel.CancelEventArgs(False))
            txtClosedDate.Text = IIf(IsDbNull(RsIPTMain.Fields("CLOSEDATE").Value), "", RsIPTMain.Fields("CLOSEDATE").Value)

            optNewSupp.Checked = IIf(IsDbNull(RsIPTMain.Fields("NEWSUPPLIER").Value) Or RsIPTMain.Fields("NEWSUPPLIER").Value = "N", False, True)
            optDsgnChng.Checked = IIf(IsDbNull(RsIPTMain.Fields("DESIGNCHANGE").Value) Or RsIPTMain.Fields("DESIGNCHANGE").Value = "N", False, True)
            optEnggChng.Checked = IIf(IsDbNull(RsIPTMain.Fields("ENGGCHANGE").Value) Or RsIPTMain.Fields("ENGGCHANGE").Value = "N", False, True)
            optMtrlChng.Checked = IIf(IsDbNull(RsIPTMain.Fields("MTRLCHANGE").Value) Or RsIPTMain.Fields("MTRLCHANGE").Value = "N", False, True)
            optProChng.Checked = IIf(IsDbNull(RsIPTMain.Fields("PROCESSCHANGE").Value) Or RsIPTMain.Fields("PROCESSCHANGE").Value = "N", False, True)
            optSubChng.Checked = IIf(IsDbNull(RsIPTMain.Fields("SUBVENDORCHANGE").Value) Or RsIPTMain.Fields("SUBVENDORCHANGE").Value = "N", False, True)
            optJigChng.Checked = IIf(IsDbNull(RsIPTMain.Fields("JIGTOOLCHANGE").Value) Or RsIPTMain.Fields("JIGTOOLCHANGE").Value = "N", False, True)
            optDieChng.Checked = IIf(IsDbNull(RsIPTMain.Fields("DIEMOULDCHANGE").Value) Or RsIPTMain.Fields("DIEMOULDCHANGE").Value = "N", False, True)
            optTransMthd.Checked = IIf(IsDbNull(RsIPTMain.Fields("TRANSPORTMTH").Value) Or RsIPTMain.Fields("TRANSPORTMTH").Value = "N", False, True)
            optPakChng.Checked = IIf(IsDbNull(RsIPTMain.Fields("PAKAGINGCHANGE").Value) Or RsIPTMain.Fields("PAKAGINGCHANGE").Value = "N", False, True)
            optMacChng.Checked = IIf(IsDbNull(RsIPTMain.Fields("MACHINECHANGE").Value) Or RsIPTMain.Fields("MACHINECHANGE").Value = "N", False, True)
            optInspChng.Checked = IIf(IsDbNull(RsIPTMain.Fields("INSPGAUGECHANGE").Value) Or RsIPTMain.Fields("INSPGAUGECHANGE").Value = "N", False, True)

            optQI.Checked = IIf(IsDbNull(RsIPTMain.Fields("QUALITY_IMPROV").Value) Or RsIPTMain.Fields("QUALITY_IMPROV").Value = "N", False, True)
            optDisSupp.Checked = IIf(IsDbNull(RsIPTMain.Fields("DIS_SUPP").Value) Or RsIPTMain.Fields("DIS_SUPP").Value = "N", False, True)
            optManpower.Checked = IIf(IsDbNull(RsIPTMain.Fields("MANPOWER").Value) Or RsIPTMain.Fields("MANPOWER").Value = "N", False, True)
            optOffLoad.Checked = IIf(IsDbNull(RsIPTMain.Fields("OFFLOAD").Value) Or RsIPTMain.Fields("OFFLOAD").Value = "N", False, True)
            optShiftNewLoc.Checked = IIf(IsDbNull(RsIPTMain.Fields("SHIFT_NEW_LOC").Value) Or RsIPTMain.Fields("SHIFT_NEW_LOC").Value = "N", False, True)

            optOther.Checked = IIf(IsDbNull(RsIPTMain.Fields("OTHERDETAIL").Value) Or RsIPTMain.Fields("OTHERDETAIL").Value = "N", False, True)
            txtRemarks.Text = IIf(IsDbNull(RsIPTMain.Fields("REMARKS").Value), "", RsIPTMain.Fields("REMARKS").Value)
            Call ShowDetail1()
            Call MakeEnableDesableField(False)
            IsShowing = False
        End If
        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = True    '' False Sandeep 15/05/2022
        txtSlipNo.Enabled = True
        cmdSearchSlipNo.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsIPTMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
        SqlStr = " SELECT QAL_IPT_DET.*,DEPT1.DEPT_DESC AS DEPTDESC,DEPT2.DEPT_DESC AS MOVEDEPTDESC " & vbCrLf & " FROM QAL_IPT_DET,PAY_DEPT_MST DEPT1,PAY_DEPT_MST DEPT2 " & vbCrLf & " WHERE QAL_IPT_DET.DEPTCODE=DEPT1.DEPT_CODE (+)" & vbCrLf & " AND QAL_IPT_DET.MOVETODEPT=DEPT2.DEPT_CODE (+)" & vbCrLf & " AND DEPT1.COMPANY_CODE(+)=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND DEPT2.COMPANY_CODE(+)=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND AUTO_KEY_IPT=" & Val(lblMkey.Text) & "" & vbCrLf & " ORDER BY SERIAL_NO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsIPTDetail, ADODB.LockTypeEnum.adLockReadOnly)
        With RsIPTDetail
            If .EOF = True Then Exit Sub
            FormatSprdMain(-1)
            I = 1
            Do While Not .EOF
                SprdMain.Row = I

                SprdMain.Col = ColDeptCode
                SprdMain.Text = IIf(IsDbNull(.Fields("DEPTCODE").Value), "", .Fields("DEPTCODE").Value)

                SprdMain.Col = ColDeptDesc
                SprdMain.Text = IIf(IsDbNull(.Fields("DEPTDESC").Value), "", .Fields("DEPTDESC").Value)

                SprdMain.Col = ColResponsibility
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("RESPONSIBILITY").Value), "", .Fields("RESPONSIBILITY").Value))

                SprdMain.Col = ColInspection
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("INSPECTION").Value), "", .Fields("INSPECTION").Value))

                SprdMain.Col = ColDate
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("INSPDATE").Value), "", .Fields("INSPDATE").Value))

                SprdMain.Col = ColObservation
                SprdMain.Text = Trim(IIf(IsDbNull(.Fields("OBSERVATION").Value), "", .Fields("OBSERVATION").Value))

                SprdMain.Col = ColMoveDeptCode
                SprdMain.Text = IIf(IsDbNull(.Fields("MOVETODEPT").Value), "", .Fields("MOVETODEPT").Value)

                SprdMain.Col = ColMoveDeptDesc
                SprdMain.Text = IIf(IsDbNull(.Fields("MOVEDEPTDESC").Value), "", .Fields("MOVEDEPTDESC").Value)

                SprdMain.Col = ColActionTaken
                SprdMain.Value = IIf(IsDbNull(.Fields("ACTIONTAKEN").Value) Or .Fields("ACTIONTAKEN").Value = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)

                SprdMain.Col = ColActionDate
                SprdMain.Text = IIf(IsDbNull(.Fields("ACTIONDATE").Value), "", .Fields("ACTIONDATE").Value)

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
        Dim xMKey As Double
        Dim mSlipNo As Double
        Dim SqlStr As String


        If Trim(txtSlipNo.Text) = "" Then GoTo EventExitSub
        mSlipNo = Val(txtSlipNo.Text)

        If MODIFYMode = True And RsIPTMain.BOF = False Then xMKey = RsIPTMain.Fields("AUTO_KEY_IPT").Value

        SqlStr = "SELECT * FROM QAL_IPT_HDR " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_IPT=" & mSlipNo & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsIPTMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsIPTMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Number. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM QAL_IPT_HDR " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_IPT=" & Val(CStr(xMKey)) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsIPTMain, ADODB.LockTypeEnum.adLockReadOnly)
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
        txtPartDesc.Enabled = mMode
        txtCustomer.Enabled = mMode
        cmdSearchCust.Enabled = mMode
        txtInitiatedBy.Enabled = mMode
        cmdSearchInitiated.Enabled = mMode
        txtInitiatedDate.Enabled = mMode

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
    Private Sub ReportOnIPT(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String
        Dim mTitle As String

        Report1.Reset()
        MainClass.ClearCRptFormulas(Report1)

        mTitle = "INITIAL PART TAG"
        SqlStr = "SELECT QAL_IPT_HDR.*,QAL_IPT_DET.*, " & vbCrLf & " FIN_SUPP_CUST_MST.*,PAY_EMPLOYEE_MST.*,PAY_DEPT_MST.*,EMP2.*, " & vbCrLf & " DEPT2.*,DEPT3.* " & vbCrLf & " FROM QAL_IPT_HDR,QAL_IPT_DET,  " & vbCrLf & " FIN_SUPP_CUST_MST,PAY_EMPLOYEE_MST ,PAY_DEPT_MST , " & vbCrLf & " PAY_EMPLOYEE_MST EMP2,PAY_DEPT_MST DEPT2,PAY_DEPT_MST DEPT3 " & vbCrLf & " WHERE QAL_IPT_HDR.AUTO_KEY_IPT=QAL_IPT_DET.AUTO_KEY_IPT " & vbCrLf & " AND QAL_IPT_HDR.COMPANY_CODE=FIN_SUPP_CUST_MST.COMPANY_CODE " & vbCrLf & " AND QAL_IPT_HDR.SUPP_CUST_CODE=FIN_SUPP_CUST_MST.SUPP_CUST_CODE " & vbCrLf & " AND QAL_IPT_HDR.COMPANY_CODE=PAY_EMPLOYEE_MST.COMPANY_CODE (+) " & vbCrLf & " AND QAL_IPT_HDR.INITBY=PAY_EMPLOYEE_MST.EMP_CODE (+) " & vbCrLf & " AND QAL_IPT_HDR.COMPANY_CODE=PAY_DEPT_MST.COMPANY_CODE (+) " & vbCrLf & " AND QAL_IPT_HDR.CLOSEATDEPT=PAY_DEPT_MST.DEPT_CODE (+) " & vbCrLf & " AND QAL_IPT_HDR.COMPANY_CODE=EMP2.COMPANY_CODE (+) " & vbCrLf & " AND QAL_IPT_HDR.CLOSEBY=EMP2.EMP_CODE (+) " & vbCrLf & " AND DEPT2.COMPANY_CODE(+)=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND QAL_IPT_DET.DEPTCODE=DEPT2.DEPT_CODE (+) " & vbCrLf & " AND DEPT3.COMPANY_CODE(+)=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND QAL_IPT_DET.MOVETODEPT=DEPT3.DEPT_CODE (+) " & vbCrLf & " AND QAL_IPT_HDR.AUTO_KEY_IPT=" & Val(lblMkey.Text) & ""

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\IPT.rpt"

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
        Call ReportOnIPT(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnIPT(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub txtCloseDept_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCloseDept.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCloseDept_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCloseDept.DoubleClick
        Call cmdSearchCloseDept_Click(cmdSearchCloseDept, New System.EventArgs())
    End Sub

    Private Sub txtCloseDept_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCloseDept.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchCloseDept_Click(cmdSearchCloseDept, New System.EventArgs())
    End Sub

    Private Sub txtCloseDept_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCloseDept.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ValSource
        Dim SqlStr As String
        If Trim(txtCloseDept.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(txtCloseDept.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("Deptt Does Not Exist In Master.")
            Cancel = True
        Else
            lblCloseDept.text = MasterNo
        End If
        GoTo EventExitSub
ValSource:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
