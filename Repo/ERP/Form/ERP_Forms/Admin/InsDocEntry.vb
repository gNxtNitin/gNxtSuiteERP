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

Friend Class frmInsDocEntry
    Inherits System.Windows.Forms.Form
    Dim RsInsMain As ADODB.Recordset ''ADODB.Recordset					
    Dim RsInsDetail As ADODB.Recordset ''ADODB.Recordset					
    '''Private PvtDBCn As ADODB.Connection					

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean
    Dim mAccountCode As String


    Private Const ConRowHeight As Short = 18

    Private Const colSupplier As Short = 1
    Private Const ColBillNo As Short = 2
    Private Const ColBillDate As Short = 3
    Private Const ColBillAmt As Short = 4

    Private Sub cboInsType_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboInsType.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboInsType_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboInsType.SelectedIndexChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub chkStatus_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkStatus.CheckStateChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
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
            If RsInsMain.EOF = False Then RsInsMain.MoveFirst()
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

        If chkStatus.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Closed Document Cann't be Deleted")
            Exit Sub
        End If

        If txtVNo.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If Not RsInsMain.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.					
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "DOC_INS_CLAIM_HDR", (txtVNo.Text), RsInsMain) = False Then GoTo DelErrPart
                If InsertIntoDeleteTrn(PubDBCn, "DOC_INS_CLAIM_HDR", "MKEY", (lblMkey.Text)) = False Then GoTo DelErrPart

                PubDBCn.Execute("DELETE FROM DOC_INS_CLAIM_DET WHERE MKEY=" & Val(lblMkey.Text) & "")
                PubDBCn.Execute("DELETE FROM DOC_INS_CLAIM_HDR WHERE MKEY=" & Val(lblMkey.Text) & "")
                PubDBCn.CommitTrans()
                RsInsMain.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        PubDBCn.Errors.Clear()
        RsInsMain.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr

        If chkStatus.CheckState = System.Windows.Forms.CheckState.Checked Then
            MsgInformation("Closed Document Cann't be Modified")
            Exit Sub
        End If

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsInsMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
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

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\InsClaimLetter.RPT"

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


        MakeSQL = ""
        ''''SELECT CLAUSE...					

        MakeSQL = " SELECT REF_DATE, MACH_NAME, SURVEYOR_NAME, " & vbCrLf _
            & " OUR_REF_NO, BILL_NO, " & vbCrLf _
            & " TO_CHAR(BILL_DATE,'DD/MM/YYYY'), " & vbCrLf _
            & " SUPPLIER, BILL_AMT, " & vbCrLf _
            & " DECODE(CHQ_NO,NULL,'',CHQ_NO || ' & ' || TO_CHAR(CHQ_DATE,'DD/MM/YYYY')), " & vbCrLf _
            & " TO_CHAR(SETTLED_AMOUNT) AS SETTLED_AMOUNT, " & vbCrLf _
            & " TO_CHAR(CLAIM_AMOUNT-SETTLED_AMOUNT) AS SETTLED_AMOUNT, " & vbCrLf _
            & " IH.MKEY"


        ''''FROM CLAUSE...					
        MakeSQL = MakeSQL & vbCrLf & " FROM " & vbCrLf _
            & " DOC_INS_CLAIM_HDR IH, DOC_INS_CLAIM_DET ID"

        ''''WHERE CLAUSE...					
        MakeSQL = MakeSQL & vbCrLf _
            & " WHERE " & vbCrLf _
            & " IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
            & " AND IH.MKEY=ID.MKEY(+)"

        MakeSQL = MakeSQL & vbCrLf _
            & " AND IH.MKEY='" & MainClass.AllowSingleQuote(lblMkey.Text) & "'"
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
        Call CalcTots()
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
        Dim nMkey As String
        Dim mVNo As Double
        Dim mClosedFlag As String
        Dim mInsType As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mClosedFlag = IIf(chkStatus.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")
        mInsType = VB.Left(cboInsType.Text, 1)

        SqlStr = ""
        If Trim(txtVNo.Text) = "" Then
            mVNo = CDbl(AutoGenSeqRefNo("REF_NO"))
        Else
            mVNo = Val(txtVNo.Text)
        End If

        txtVNo.Text = VB6.Format(Val(CStr(mVNo)), "00000")


        If ADDMode = True Then
            mCurRowNo = MainClass.AutoGenRowNo("DOC_INS_CLAIM", "RowNo", PubDBCn)
            nMkey = RsCompany.Fields("COMPANY_CODE").Value & RsCompany.Fields("FYEAR").Value & mCurRowNo

            lblMkey.Text = nMkey

            SqlStr = " INSERT INTO DOC_INS_CLAIM_HDR ( " & vbCrLf _
                & " MKEY, COMPANY_CODE , FYEAR, ROWNO," & vbCrLf _
                & " REF_NO, REF_DATE," & vbCrLf _
                & " MACH_NAME, SURVEYOR_NAME," & vbCrLf _
                & " OUR_REF_NO, OUR_REF_DATE," & vbCrLf _
                & " CHQ_NO, CHQ_DATE," & vbCrLf _
                & " CLAIM_AMOUNT, SETTLED_AMOUNT," & vbCrLf _
                & " COVERNOTE_NO,STATUS, INS_TYPE, " & vbCrLf _
                & " ADDUSER, ADDDATE," & vbCrLf _
                & " MODUSER,MODDATE,POLICY_NO, INS_COMP_NAME,BDMNO,REMARKS, EstimatedAmount) "


            SqlStr = SqlStr & vbCrLf _
                & " VALUES ( " & vbCrLf _
                & " '" & nMkey & "', " & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & "," & vbCrLf _
                & " " & mCurRowNo & ", " & Val(txtVNo.Text) & ", " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtBreakDown.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtSurveyor.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(TxtRefNo.Text) & "', " & vbCrLf _
                & " TO_DATE('" & VB6.Format(txtRefDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtChqNo.Text) & "', " & vbCrLf _
                & " TO_DATE('" & VB6.Format(Trim(txtChqDate.Text), "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " " & Val(lblTotAmount.Text) & ", " & vbCrLf _
                & " " & Val(txtAmount.Text) & ", " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtCoverNoteNo.Text) & "', '" & mClosedFlag & "', '" & mInsType & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                & " TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'',''," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtPolicyNo.Text) & "','" & MainClass.AllowSingleQuote(txtInsCompanyName.Text) & "'," & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtBDMNo.Text) & "','" & MainClass.AllowSingleQuote(txtRemarks.Text) & "'," & Val(txtEstimatedAmount.Text) & ")"
        End If

        If MODIFYMode = True Then
            SqlStr = " UPDATE DOC_INS_CLAIM_HDR SET " & vbCrLf _
                & " REF_NO=" & Val(txtVNo.Text) & ", " & vbCrLf _
                & " REF_DATE=TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " MACH_NAME='" & MainClass.AllowSingleQuote(txtBreakDown.Text) & "', " & vbCrLf _
                & " SURVEYOR_NAME='" & MainClass.AllowSingleQuote(txtSurveyor.Text) & "', " & vbCrLf _
                & " OUR_REF_NO='" & MainClass.AllowSingleQuote(TxtRefNo.Text) & "', " & vbCrLf _
                & " OUR_REF_DATE=TO_DATE('" & VB6.Format(txtRefDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " CHQ_NO='" & MainClass.AllowSingleQuote(txtChqNo.Text) & "', " & vbCrLf _
                & " CHQ_DATE=TO_DATE('" & VB6.Format(Trim(txtChqDate.Text), "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " CLAIM_AMOUNT=" & Val(lblTotAmount.Text) & ", " & vbCrLf _
                & " SETTLED_AMOUNT=" & Val(txtAmount.Text) & ", " & vbCrLf _
                & " STATUS='" & mClosedFlag & "', INS_TYPE='" & mInsType & "'," & vbCrLf _
                & " COVERNOTE_NO='" & MainClass.AllowSingleQuote(txtCoverNoteNo.Text) & "', EstimatedAmount=" & Val(txtEstimatedAmount.Text) & "," & vbCrLf _
                & " POLICY_NO='" & MainClass.AllowSingleQuote(txtPolicyNo.Text) & "', INS_COMP_NAME='" & MainClass.AllowSingleQuote(txtInsCompanyName.Text) & "'," & vbCrLf _
                & " BDMNO='" & MainClass.AllowSingleQuote(txtBDMNo.Text) & "',REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "'," & vbCrLf _
                & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND MKEY =" & Val(lblMkey.Text) & ""
        End If

        PubDBCn.Execute(SqlStr)
        If UpdateDetail1() = False Then GoTo ErrPart
        txtVNo.Text = VB6.Format(Val(CStr(mVNo)), "00000")

        Update1 = True
        PubDBCn.CommitTrans()

        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsInsMain.Requery()
        RsInsDetail.Requery()
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

        SqlStr = "SELECT Max(" & mFieldName & ")  FROM DOC_INS_CLAIM_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("Company_Code").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " "
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
    Private Function UpdateDetail1() As Boolean

        On Error GoTo UpdateDetail1
        Dim SqlStr As String
        Dim I As Integer
        Dim mSupplier As String
        Dim mBillNo As String
        Dim mBillDate As String
        Dim mBillAmount As Double

        SqlStr = "Delete From  DOC_INS_CLAIM_DET " & vbCrLf & " Where " & vbCrLf & " MKEY=" & Val(lblMkey.Text) & ""

        PubDBCn.Execute(SqlStr)

        With SprdMain
            For I = 1 To .MaxRows - 1
                .Row = I

                .Col = colSupplier
                mSupplier = MainClass.AllowSingleQuote(.Text)

                .Col = ColBillNo
                mBillNo = MainClass.AllowSingleQuote(.Text)

                .Col = ColBillDate
                mBillDate = MainClass.AllowSingleQuote(.Text)

                .Col = ColBillAmt
                mBillAmount = Val(.Text)


                SqlStr = ""

                If mBillNo <> "" And mBillAmount > 0 Then
                    SqlStr = " INSERT INTO DOC_INS_CLAIM_DET ( " & vbCrLf _
                        & " MKEY, SUBROWNO, SUPPLIER, BILL_NO, " & vbCrLf _
                        & " BILL_DATE, BILL_AMT) "

                    SqlStr = SqlStr & vbCrLf _
                        & " VALUES ( " & vbCrLf _
                        & " " & Val(lblMkey.Text) & "," & I & "," & vbCrLf _
                        & " '" & MainClass.AllowSingleQuote(mSupplier) & "', " & vbCrLf _
                        & " '" & mBillNo & "',TO_DATE('" & VB6.Format(mBillDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                        & " " & mBillAmount & ") "

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
        MainClass.ButtonStatus(Me, XRIGHT, RsInsMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub CalcTots()
        On Error GoTo ERR1
        ''Dim RsMisc As ADODB.Recordset					

        Dim mBillAmount As Double
        Dim mTotBillAmount As Double
        Dim I As Integer
        Dim j As Integer

        mBillAmount = 0
        mTotBillAmount = 0

        With SprdMain
            j = .MaxRows
            For I = 1 To j
                .Row = I

                .Col = ColBillAmt
                mBillAmount = Val(.Text)
                .Text = CStr(mBillAmount)

                mTotBillAmount = mTotBillAmount + mBillAmount
            Next I
        End With

        lblTotAmount.Text = VB6.Format(mTotBillAmount, "#0.00")

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume					
    End Sub
    Public Sub frmInsDocEntry_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "Insurance Claim Entry"

        SqlStr = "Select * From DOC_INS_CLAIM_HDR WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsInsMain, ADODB.LockTypeEnum.adLockReadOnly)

        SqlStr = "Select * From DOC_INS_CLAIM_DET WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsInsDetail, ADODB.LockTypeEnum.adLockReadOnly)

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
            & " REF_NO, REF_DATE, MACH_NAME, SURVEYOR_NAME, " & vbCrLf _
            & " OUR_REF_NO, OUR_REF_DATE, CLAIM_AMOUNT," & vbCrLf _
            & " CHQ_NO, CHQ_DATE,SETTLED_AMOUNT " & vbCrLf _
            & " FROM DOC_INS_CLAIM_HDR " & vbCrLf _
            & " WHERE " & vbCrLf _
            & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

        SqlStr = SqlStr & " ORDER BY REF_NO,REF_DATE"

        'MainClass.AssignDataInSprd(SqlStr, ADataGrid, StrConn, IIf(mRefresh = True, "Y", "N"))
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub frmInsDocEntry_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''Set PvtDBCn = New ADODB.Connection					
        ''PvtDBCn.Open StrConn					
        Call SetMainFormCordinate(Me)
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        cboInsType.Items.Clear()
        cboInsType.Items.Add("BreakDown")
        cboInsType.Items.Add("Accident")
        cboInsType.SelectedIndex = 0

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
        chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked
        txtBreakDown.Text = ""
        txtSurveyor.Text = ""
        TxtRefNo.Text = ""
        txtRefDate.Text = ""
        txtChqNo.Text = ""
        txtChqDate.Text = ""
        txtAmount.Text = ""
        txtPolicyNo.Text = ""
        txtInsCompanyName.Text = ""
        txtBDMNo.Text = ""
        txtRemarks.Text = ""
        txtEstimatedAmount.Text = ""
        cboInsType.SelectedIndex = 0
        lblTotAmount.Text = "0.00"
        txtCoverNoteNo.Text = ""
        txtVDate.Enabled = True
        SprdMain.Enabled = True
        MainClass.ClearGrid(SprdMain, ConRowHeight)
        FormatSprdMain(-1)

        MainClass.ButtonStatus(Me, XRIGHT, RsInsMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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

            .Col = colSupplier
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsInsDetail.Fields("SUPPLIER").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 50)

            .Col = ColBillNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditLen = RsInsDetail.Fields("BILL_NO").DefinedSize
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .set_ColWidth(.Col, 15)

            .Col = ColBillDate
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeEditCharSet = SS_CELL_DATE_FORMAT_DDMMYY
            .TypeDateFormat = FPSpreadADO.TypeDateFormatConstants.TypeDateFormatDDMMYY
            .set_ColWidth(.Col, 15)
            .TypeEditMultiLine = True

            .Col = ColBillAmt
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalPlaces = 2
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("999999999.99")
            .TypeFloatMin = CDbl("-999999999.99")
            .TypeEditLen = RsInsDetail.Fields("BILL_AMT").Precision
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(ColBillAmt, 15)

            MainClass.UnProtectCell(SprdMain, 1, SprdMain.MaxRows, colSupplier, ColBillAmt)
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

    Private Sub txtPolicyNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPolicyNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPolicyNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPolicyNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtPolicyNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtInsCompanyName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInsCompanyName.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtInsCompanyName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtInsCompanyName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtInsCompanyName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtBDMNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBDMNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBDMNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBDMNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtBDMNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
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

    Private Sub txtEstimatedAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEstimatedAmount.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtEstimatedAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEstimatedAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub SetTextLengths()
        On Error GoTo ERR1

        txtVNo.MaxLength = RsInsMain.Fields("REF_NO").Precision
        txtVDate.MaxLength = RsInsMain.Fields("REF_DATE").DefinedSize - 6

        TxtRefNo.MaxLength = RsInsMain.Fields("REF_NO").DefinedSize
        txtRefDate.MaxLength = RsInsMain.Fields("REF_DATE").DefinedSize

        txtBreakDown.MaxLength = RsInsMain.Fields("MACH_NAME").DefinedSize
        txtSurveyor.MaxLength = RsInsMain.Fields("SURVEYOR_NAME").DefinedSize
        TxtRefNo.MaxLength = RsInsMain.Fields("OUR_REF_NO").DefinedSize
        txtRefDate.MaxLength = RsInsMain.Fields("OUR_REF_DATE").DefinedSize
        txtChqNo.MaxLength = RsInsMain.Fields("CHQ_NO").DefinedSize
        txtChqDate.MaxLength = RsInsMain.Fields("CHQ_DATE").DefinedSize
        txtAmount.MaxLength = RsInsMain.Fields("CLAIM_AMOUNT").Precision
        txtCoverNoteNo.MaxLength = RsInsMain.Fields("COVERNOTE_NO").DefinedSize

        txtPolicyNo.MaxLength = RsInsMain.Fields("POLICY_NO").DefinedSize
        txtInsCompanyName.MaxLength = RsInsMain.Fields("INS_COMP_NAME").DefinedSize
        txtBDMNo.MaxLength = RsInsMain.Fields("BDMNO").DefinedSize
        txtRemarks.MaxLength = RsInsMain.Fields("REMARKS").DefinedSize
        txtEstimatedAmount.MaxLength = RsInsMain.Fields("EstimatedAmount").Precision


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
        If MODIFYMode = True And RsInsMain.EOF = True Then Exit Function

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

        If Trim(TxtRefNo.Text) = "" Then
            MsgInformation("Our Ref No. is Blank")
            FieldsVarification = False
            TxtRefNo.Focus()
            Exit Function
        End If


        If Trim(txtRefDate.Text) = "" Then
            MsgInformation("Our Ref Date is empty. Cannot Save")
            txtRefDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtRefDate.Text) <> "" Then
            If IsDate(txtRefDate.Text) = False Then
                MsgInformation(" Invalid Our Ref Date. Cannot Save")
                txtRefDate.Focus()
                FieldsVarification = False
                Exit Function
            End If
        End If

        If Trim(txtBreakDown.Text) = "" Then
            MsgInformation("Machine Name is Blank")
            FieldsVarification = False
            txtBreakDown.Focus()
            Exit Function
        End If

        If Trim(cboInsType.Text) = "" Then
            MsgInformation("Insurance Type is Blank.")
            FieldsVarification = False
            cboInsType.Focus()
            Exit Function
        End If

        If Trim(txtPolicyNo.Text) = "" Then
            MsgInformation("Cover Note is Blank")
            FieldsVarification = False
            txtPolicyNo.Focus()
            Exit Function
        End If

        If Trim(txtSurveyor.Text) = "" Then
            MsgInformation("Surveyor Name is Blank")
            FieldsVarification = False
            txtSurveyor.Focus()
            Exit Function
        End If

        If Trim(txtChqNo.Text) <> "" Then
            If Trim(txtChqDate.Text) = "" Then
                MsgInformation("Cheque Date is empty. Cannot Save")
                txtChqDate.Focus()
                FieldsVarification = False
                Exit Function
            End If

            If Trim(txtChqDate.Text) <> "" Then
                If IsDate(txtChqDate.Text) = False Then
                    MsgInformation(" Invalid Cheque Date. Cannot Save")
                    txtChqDate.Focus()
                    FieldsVarification = False
                    Exit Function
                End If
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

    Private Sub frmInsDocEntry_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        Me.Hide()
        Me.Dispose()
        Me.Close()
        RsInsMain.Close()
        'RsOpOuts.Close					
    End Sub

    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub SprdMain_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles SprdMain.Leave
        '    With SprdMain					
        '        SprdMain_LeaveCell .ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False					
        '    End With					
    End Sub
    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim SqlStr As String
        Dim mSupplier As String

        If eventArgs.row = 0 And eventArgs.col = colSupplier Then
            With SprdMain
                .Row = .ActiveRow
                .Col = colSupplier
                If MainClass.SearchGridMaster(.Text, "FIN_SUPP_CUST_MST", "SUPP_CUST_NAME", "SUPP_CUST_CODE",  , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND SUPP_CUST_TYPE IN ('S','C') ") = True Then
                    .Row = .ActiveRow
                    .Col = colSupplier
                    .Text = AcName
                    mSupplier = Trim(.Text)

                    MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, colSupplier)
                End If

            End With
        End If

        If eventArgs.col = 0 And eventArgs.row > 0 And (ADDMode = True Or MODIFYMode = True) Then
            MainClass.DeleteSprdRow(SprdMain, eventArgs.row, colSupplier)
            MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
        End If
    End Sub
    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim xBillNo As String
        Dim xSupplier As String
        Dim mCol2 As Long
        Dim mRow2 As Long

        If eventArgs.newRow = -1 Then Exit Sub

        Select Case eventArgs.col
            Case colSupplier
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = colSupplier
                xSupplier = SprdMain.Text
                If xSupplier = "" Then Exit Sub

                mCol2 = colSupplier
                mRow2 = SprdMain.ActiveRow

                If CheckSupplier(xSupplier, mCol2, mRow2) = False Then
                    FormatSprdMain(eventArgs.row)
                    'MainClass.SetFocusToCell(SprdMain, eventArgs.row, colSupplier)
                End If

            Case ColBillNo
                SprdMain.Row = SprdMain.ActiveRow

                SprdMain.Col = colSupplier
                xSupplier = SprdMain.Text

                SprdMain.Col = ColBillNo
                xBillNo = SprdMain.Text
                If xBillNo = "" Then Exit Sub

                If CheckDuplicateBillNo(xBillNo) = True Then
                    FormatSprdMain(eventArgs.row)
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColBillNo)
                Else
                    MainClass.AddBlankSprdRow(SprdMain, colSupplier, ConRowHeight)
                    FormatSprdMain(-1)
                End If

            Case ColBillAmt
                SprdMain.Row = SprdMain.ActiveRow
                SprdMain.Col = ColBillNo
                xBillNo = SprdMain.Text
                If xBillNo = "" Then Exit Sub

                If CheckBillAmt((SprdMain.ActiveRow)) = True Then
                    '                MainClass.AddBlankSprdRow SprdMain, ColBillNo, ConRowHeight					
                    '                FormatSprdMain -1					
                End If
        End Select
        Call CalcTots()
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Function CheckSupplier(ByRef pAccountName As String, ByRef col2 As Integer, ByRef Row2 As Integer) As Boolean
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RS As ADODB.Recordset = Nothing '' ADODB.Recordset										
        CheckSupplier = False
        If pAccountName = "" Then
            CheckSupplier = True
            Exit Function
        End If

        SqlStr = " SELECT SUPP_CUST_CODE FROM FIN_SUPP_CUST_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND SUPP_CUST_NAME='" & MainClass.AllowSingleQuote(Trim(pAccountName)) & "'"
        If ADDMode = True Then
            SqlStr = SqlStr & vbCrLf & " AND STATUS='O' "
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RS, ADODB.LockTypeEnum.adLockReadOnly)
        If RS.EOF = True Then
            MainClass.SetFocusToCell(SprdMain, Row2, col2, "Invalid Account.")
            Exit Function
        End If

        CheckSupplier = True
        RS.Close()
        RS = Nothing
        Exit Function
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        CheckSupplier = False
        RS.Close()
        RS = Nothing
    End Function
    Private Function CheckDuplicateBillNo(ByRef mBillNo As String) As Boolean

        On Error GoTo ERR1
        Dim I As Integer
        Dim mCheckCode As String
        Dim mItemRept As Integer


        If Trim(mBillNo) = "" Then CheckDuplicateBillNo = True : Exit Function
        With SprdMain
            For I = 1 To .MaxRows
                .Row = I
                .Col = ColBillNo
                mCheckCode = UCase(Trim(.Text))

                If mCheckCode = UCase(Trim(mBillNo)) Then
                    mItemRept = mItemRept + 1
                    If mItemRept > 1 Then
                        CheckDuplicateBillNo = True
                        MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColBillNo)
                        Exit Function
                    End If
                End If
            Next
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function


    Private Function CheckBillAmt(ByRef pRow As Integer) As Boolean

        On Error GoTo ERR1
        CheckBillAmt = True
        With SprdMain
            .Row = pRow
            .Col = ColBillAmt
            If Val(.Text) = 0 Then
                CheckBillAmt = False
                MsgInformation("Please Check Bill Amount.")
                MainClass.SetFocusToCell(SprdMain, .ActiveRow, ColBillAmt)
            End If
        End With
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
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
    Private Sub txtAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAmount.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtBreakDown_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBreakDown.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBreakDown_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBreakDown.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtBreakDown.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtChqDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtChqDate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtChqDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtChqDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtChqDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtChqDate.Text) Then
            MsgBox("Invalid Cheque Date.", MsgBoxStyle.Information)
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtChqNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtChqNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtChqNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtChqNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCoverNoteNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCoverNoteNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCoverNoteNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCoverNoteNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtCoverNoteNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtRefDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtRefDate.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub txtRefDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtRefDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtRefDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtRefDate.Text) Then
            MsgBox("Invalid Our Ref Date.", MsgBoxStyle.Information)
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtRefNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtRefNo.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtRefNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtRefNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtRefNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtSurveyor_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSurveyor.TextChanged

        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSurveyor_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSurveyor.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtSurveyor.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
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

        Clear1()
        If Not RsInsMain.EOF Then
            With RsInsMain
                lblMkey.Text = IIf(IsDBNull(.Fields("mKey").Value), "", .Fields("mKey").Value)
                txtVNo.Text = IIf(IsDBNull(.Fields("REF_NO").Value), "", .Fields("REF_NO").Value)
                txtVDate.Text = VB6.Format(IIf(IsDBNull(.Fields("REF_DATE").Value), "", .Fields("REF_DATE").Value), "DD/MM/YYYY")

                txtBreakDown.Text = IIf(IsDBNull(.Fields("MACH_NAME").Value), "", .Fields("MACH_NAME").Value)
                txtSurveyor.Text = IIf(IsDBNull(.Fields("SURVEYOR_NAME").Value), "", .Fields("SURVEYOR_NAME").Value)
                TxtRefNo.Text = IIf(IsDBNull(.Fields("OUR_REF_NO").Value), "", .Fields("OUR_REF_NO").Value)
                txtRefDate.Text = VB6.Format(IIf(IsDBNull(.Fields("OUR_REF_DATE").Value), "", .Fields("OUR_REF_DATE").Value), "DD/MM/YYYY")
                txtChqNo.Text = IIf(IsDBNull(.Fields("CHQ_NO").Value), "", .Fields("CHQ_NO").Value)
                txtChqDate.Text = VB6.Format(IIf(IsDBNull(.Fields("CHQ_DATE").Value), "", .Fields("CHQ_DATE").Value), "DD/MM/YYYY")
                txtAmount.Text = VB6.Format(IIf(IsDBNull(.Fields("SETTLED_AMOUNT").Value), 0, .Fields("SETTLED_AMOUNT").Value), "0.00")
                lblTotAmount.Text = VB6.Format(IIf(IsDBNull(.Fields("CLAIM_AMOUNT").Value), 0, .Fields("CLAIM_AMOUNT").Value), "0.00")
                txtCoverNoteNo.Text = IIf(IsDBNull(.Fields("COVERNOTE_NO").Value), "", .Fields("COVERNOTE_NO").Value)

                txtPolicyNo.Text = IIf(IsDBNull(.Fields("POLICY_NO").Value), "", .Fields("POLICY_NO").Value)
                txtInsCompanyName.Text = IIf(IsDBNull(.Fields("INS_COMP_NAME").Value), "", .Fields("INS_COMP_NAME").Value)
                txtBDMNo.Text = IIf(IsDBNull(.Fields("BDMNO").Value), "", .Fields("BDMNO").Value)
                txtRemarks.Text = IIf(IsDBNull(.Fields("REMARKS").Value), "", .Fields("REMARKS").Value)
                txtEstimatedAmount.Text = VB6.Format(IIf(IsDBNull(.Fields("EstimatedAmount").Value), 0, .Fields("EstimatedAmount").Value), "0.00")

                chkStatus.CheckState = IIf(RsInsMain.Fields("Status").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
                chkStatus.Enabled = IIf(RsInsMain.Fields("Status").Value = "Y", False, True)

                If RsInsMain.Fields("INS_TYPE").Value = "B" Then
                    cboInsType.SelectedIndex = 0
                ElseIf RsInsMain.Fields("INS_TYPE").Value = "A" Then
                    cboInsType.SelectedIndex = 1
                End If

                '            txtVNo.Enabled = False					

            End With
            Call ShowDetail1()
            Call CalcTots()
        End If
        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = False
        MainClass.ButtonStatus(Me, XRIGHT, RsInsMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
        SqlStr = " SELECT * " & vbCrLf & " FROM DOC_INS_CLAIM_DET " & vbCrLf & " Where MKEY=" & Val(lblMkey.Text) & "" & vbCrLf & " ORDER BY SUBROWNO"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsInsDetail, ADODB.LockTypeEnum.adLockReadOnly)

        With RsInsDetail
            If .EOF = True Then Exit Sub
            '        FormatSprdMain -1					
            I = 1
            '        .MoveFirst					

            Do While Not .EOF

                SprdMain.Row = I

                SprdMain.Col = colSupplier
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("SUPPLIER").Value), "", .Fields("SUPPLIER").Value))

                SprdMain.Col = ColBillNo
                SprdMain.Text = Trim(IIf(IsDBNull(.Fields("BILL_NO").Value), "", .Fields("BILL_NO").Value))

                SprdMain.Col = ColBillDate
                SprdMain.Text = VB6.Format(IIf(IsDBNull(.Fields("BILL_DATE").Value), "", .Fields("BILL_DATE").Value), "DD/MM/YYYY")

                SprdMain.Col = ColBillAmt
                SprdMain.Text = CStr(Val(IIf(IsDBNull(.Fields("BILL_AMT").Value), 0, .Fields("BILL_AMT").Value)))

                .MoveNext()

                I = I + 1
                SprdMain.MaxRows = I
            Loop
        End With

        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        ''Resume					
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


        If MODIFYMode = True And RsInsMain.BOF = False Then xMKey = RsInsMain.Fields("mKey").Value

        SqlStr = "SELECT * FROM DOC_INS_CLAIM_HDR " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND REF_NO=" & Val(mVNo) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsInsMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsInsMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such REf No. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM DOC_INS_CLAIM_HDR WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND MKEY=" & Val(xMKey) & ""

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsInsMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtBDMNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBDMNo.DoubleClick
        searchBDMNo()
    End Sub

    Private Sub txtBDMNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtBDMNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then searchBDMNo()
    End Sub



    Private Sub SearchBDMNo()
        On Error GoTo ErrPart
        Dim SqlStr As String = ""

        SqlStr = " SELECT MACHINE_NO, MACHINE_DESC, LOCATION ,MAKE, MACHINE_SPEC, MACH_ASSET_NO " & vbCrLf _
            & " from MAN_MACHINE_MST" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        If MainClass.SearchGridMasterBySQL2(txtBDMNo.Text, SqlStr) = True Then
            txtBDMNo.Text = AcName
            txtBDMNo_Validating(txtBDMNo, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub txtBDMNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBDMNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        If Trim(txtBDMNo.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""


        'SqlStr = " SELECT MACHINE_NO, MACHINE_DESC, LOCATION ,MAKE, MACHINE_SPEC, MACH_ASSET_NO " & vbCrLf _
        '            & " from MAN_MACHINE_MST" & vbCrLf _
        '            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""


        If MainClass.ValidateWithMasterTable(txtBDMNo.Text, "MACHINE_NO", "MACHINE_NO", "MAN_MACHINE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgInformation("Invalid Machine No.")
            Cancel = True
        End If

        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, Err.Number, MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
