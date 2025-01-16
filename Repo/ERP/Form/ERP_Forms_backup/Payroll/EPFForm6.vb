Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmEPFForm6
    Inherits System.Windows.Forms.Form

    Dim SqlStr As String = ""
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection

    Dim FormActive As Boolean
    Private Const ConCmdFCaption As String = "&Front Page"
    Private Const ConCmdBCaption As String = "&Back Page"

    Private Const ConRowHeight As Short = 12
    Private Const ColSNo As Short = 0
    Private Const ColCodeNo As Short = 1
    Private Const ColAcctNo As Short = 2
    Private Const ColName As Short = 3
    Private Const ColFName As Short = 4
    Private Const ColWages As Short = 5
    Private Const ColEmpCont As Short = 6
    Private Const ColEPF As Short = 7
    Private Const ColPFund As Short = 8
    Private Const ColRefund As Short = 9
    Private Const ColRate As Short = 10
    Private Const ColRemarks As Short = 11
    Private Const ColCompanyCode As Short = 12


    Private Const ColMonth As Short = 1
    Private Const ColEPFAc As Short = 2
    Private Const ColPF As Short = 3
    Private Const ColDLI As Short = 4
    Private Const ColADM As Short = 5
    Private Const ColEDLI As Short = 6
    Private Const ColPFTotal As Short = 7

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer


    Private Sub FillHeading()

        MainClass.ClearGrid(sprdAttn)
        MainClass.ClearGrid(sprdBack)

        With sprdAttn
            .MaxCols = ColCompanyCode

            .Row = 0

            .Col = ColSNo
            .Text = "S. No."

            .Col = ColCodeNo
            .Text = "Code No"

            .Col = ColAcctNo
            .Text = "Account Number"


            .Col = ColName
            .Text = "Name of Person " & vbNewLine & "(In Block Letters)"

            .Col = ColFName
            .Text = "Father's / Husband's Name "

            .Col = ColWages
            .Text = "Wages, Retaining Allowance (If any) & DA including Cash Value of Food Concession Paid During the Currency Period"

            .Col = ColEmpCont
            .Text = "Amount of Worker's Contribution Deducted From the Wages"

            .Col = ColEPF
            .Text = "EPF Difference Between 12% & 8.33%"

            .Col = ColPFund
            .Text = "Pension Fund 8.33%"

            .Col = ColRefund
            .Text = "Refund of Advance"

            .Col = ColRate
            .Text = "Rate of Higher Voluntary Contribution (If any)"

            .Col = ColRemarks
            .Text = "Remarks"

            .Col = ColCompanyCode
            .Text = "Company Code"

            .set_RowHeight(0, .get_MaxTextRowHeight(0))

            MainClass.ProtectCell(sprdAttn, 0, .MaxRows, 0, .MaxCols)
            sprdAttn.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle
        End With

        With sprdBack
            .MaxCols = ColPFTotal

            .Row = 0

            .Col = ColSNo
            .Text = "S. No."

            .Col = ColMonth
            .Text = "Month"

            .Col = ColEPFAc
            .Text = "EPF Contributions Including Refund of Advance A/c No. 1"

            .Col = ColPF
            .Text = "Pension Fund Contribution A/c No. 10"

            .Col = ColDLI
            .Text = "DLI Contribution A/c No. 21"

            .Col = ColADM
            .Text = "AD. Charges A/c No. 2"

            .Col = ColEDLI
            .Text = "EDLI Charges 0.01% A/c No. 22"

            .Col = ColPFTotal
            .Text = "Cols 5,6,7 Rs. Aggregate() Contribution()"

            .set_RowHeight(0, .get_MaxTextRowHeight(0))

            MainClass.ProtectCell(sprdBack, 0, .MaxRows, 0, .MaxCols)
            sprdBack.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' OperationModeSingle
        End With
    End Sub

    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        If chkAll.CheckState = System.Windows.Forms.CheckState.Checked Then
            cboEmployee.Enabled = False
        Else
            cboEmployee.Enabled = True
        End If
    End Sub

    Private Sub cmdCDOld_Click()

        On Error GoTo ErrPart
        Dim I As Integer
        Dim FoxStrConn As String
        Dim FoxPvtDBCn As ADODB.Connection
        Dim rsFox As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim mPFAcCode As String
        Dim MTempPFAcCode As String
        Dim mEmpName As String
        Dim mFromDate As String
        Dim mToDate As String
        Dim mCompanyCode As Integer
        Dim mEST_CODEStr As String
        Dim mEST_CODE As Double
        Dim mEST_EXTN As String
        Dim mEMP_NO As Double
        Dim mEMP_NAME As String
        Dim mVOL_PEN As String
        Dim mTotWages As Double
        Dim mPFT As Double
        Dim mBST As Double
        Dim mWAGE1 As Double
        Dim mWAGE2 As Double
        Dim mWAGE3 As Double
        Dim mWAGE4 As Double
        Dim mWAGE5 As Double
        Dim mWAGE6 As Double
        Dim mWAGE7 As Double
        Dim mWAGE8 As Double
        Dim mWAGE9 As Double
        Dim mWAGE10 As Double
        Dim mWAGE11 As Double
        Dim mWAGE12 As Double
        Dim mWAGE13 As Double
        Dim mPF1 As Double
        Dim mPF2 As Double
        Dim mPF3 As Double
        Dim mPF4 As Double
        Dim mPF5 As Double
        Dim mPF6 As Double
        Dim mPF7 As Double
        Dim mPF8 As Double
        Dim mPF9 As Double
        Dim mPF10 As Double
        Dim mPF11 As Double
        Dim mPF12 As Double
        Dim mPF13 As Double
        Dim mBS1 As Double
        Dim mBS2 As Double
        Dim mBS3 As Double
        Dim mBS4 As Double
        Dim mBS5 As Double
        Dim mBS6 As Double
        Dim mBS7 As Double
        Dim mBS8 As Double
        Dim mBS9 As Double
        Dim mBS10 As Double
        Dim mBS11 As Double
        Dim mBS12 As Double
        Dim mBS13 As Double
        Dim mTOTEE As Double
        Dim mTOTER As Double
        Dim mTOTREF As Double
        Dim mJANEE As Double
        Dim mFEBEE As Double
        Dim mMAREE As Double
        Dim mAPREE As Double
        Dim mMAYEE As Double
        Dim mJUNEE As Double
        Dim mJULEE As Double
        Dim mAUGEE As Double
        Dim mSEPEE As Double
        Dim mOCTEE As Double
        Dim mNOVEE As Double
        Dim mDECEE As Double
        Dim mOTHEE As Double
        Dim mJANER As Double
        Dim mFEBER As Double
        Dim mMARER As Double
        Dim mAPRER As Double
        Dim mMAYER As Double
        Dim mJUNER As Double
        Dim mJULER As Double
        Dim mAUGER As Double
        Dim mSEPER As Double
        Dim mOCTER As Double
        Dim mNOVER As Double
        Dim mDECER As Double
        Dim mOTHER As Double
        Dim mREFUND1 As Double
        Dim mREFUND2 As Double
        Dim mREFUND3 As Double
        Dim mREFUND4 As Double
        Dim mREFUND5 As Double
        Dim mREFUND6 As Double
        Dim mREFUND7 As Double
        Dim mREFUND8 As Double
        Dim mREFUND9 As Double
        Dim mREFUND10 As Double
        Dim mREFUND11 As Double
        Dim mREFUND12 As Double
        Dim mREFUND13 As Double


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor


        FoxStrConn = "DSN=PAYROLL"
        FoxPvtDBCn = New ADODB.Connection
        FoxPvtDBCn.Open(FoxStrConn)

        FoxPvtDBCn.BeginTrans()
        FoxPvtDBCn.Execute("DELETE FROM D4024.DBF")
        '    FoxPvtDBCn.Execute "PACK"


        For I = 1 To sprdAttn.MaxRows - 2
            sprdAttn.Row = I

            sprdAttn.Col = ColAcctNo
            mPFAcCode = sprdAttn.Text
            MTempPFAcCode = mPFAcCode

            sprdAttn.Col = ColName
            mEmpName = VB.Left(Trim(sprdAttn.Text), 40)

            sprdAttn.Col = ColCompanyCode
            mCompanyCode = Val(sprdAttn.Text)

            If RsCompany.Fields("COMPANY_CODE").Value = 6 Then
                mEST_CODEStr = Mid(RsCompany.Fields("PFEST").Value, 7, Len(RsCompany.Fields("PFEST").Value))
                mEST_CODEStr = Mid(mEST_CODEStr, 1, InStr(1, mEST_CODEStr, "/") - 1)
                mEST_CODE = Val(mEST_CODEStr) ''5415
            ElseIf RsCompany.Fields("COMPANY_CODE").Value = 16 Then
                mEST_CODE = 31507
            Else
                mEST_CODEStr = Mid(RsCompany.Fields("PFEST").Value, 4, Len(RsCompany.Fields("PFEST").Value))
                mEST_CODEStr = Mid(mEST_CODEStr, 1, InStr(1, mEST_CODEStr, "/") - 1)
                mEST_CODE = Val(mEST_CODEStr) ''5415
            End If

            mEST_EXTN = ""
            Do While Not InStr(1, MTempPFAcCode, "/", CompareMethod.Binary) = 0
                MTempPFAcCode = Mid(MTempPFAcCode, InStr(1, MTempPFAcCode, "/", CompareMethod.Binary) + 1)
            Loop
            mEMP_NO = IIf(MTempPFAcCode = "", 0, MTempPFAcCode)
            mEMP_NAME = MainClass.AllowSingleQuote(mEmpName)
            mVOL_PEN = ""
            Call GetFieldValSumm(mPFAcCode, mTotWages, mPFT, mTOTEE, mTOTER, mCompanyCode)
            mBST = 0
            mTOTREF = 0

            mFromDate = "01-" & VB6.Format(txtFrom.Text, "MM-YYYY")
            mToDate = "01-" & VB6.Format(txtTo.Text, "MM-YYYY")

            Do While Not CDate(mFromDate) > CDate(mToDate)
                Select Case Month(CDate(mFromDate))
                    Case 1
                        Call GetFieldValMonth(mPFAcCode, mWAGE1, mPF1, mBS1, mJANEE, mJANER, mFromDate, mCompanyCode)
                        '                    mBS1 = 0
                        mREFUND1 = 0
                    Case 2
                        Call GetFieldValMonth(mPFAcCode, mWAGE2, mPF2, mBS2, mFEBEE, mFEBER, mFromDate, mCompanyCode)
                        '                    mBS2 = 0
                        mREFUND2 = 0
                    Case 3
                        Call GetFieldValMonth(mPFAcCode, mWAGE3, mPF3, mBS3, mMAREE, mMARER, mFromDate, mCompanyCode)
                        '                    mBS3 = 0
                        mREFUND3 = 0
                    Case 4
                        Call GetFieldValMonth(mPFAcCode, mWAGE4, mPF4, mBS4, mAPREE, mAPRER, mFromDate, mCompanyCode)
                        '                    mBS4 = 0
                        mREFUND4 = 0
                    Case 5
                        Call GetFieldValMonth(mPFAcCode, mWAGE5, mPF5, mBS5, mMAYEE, mMAYER, mFromDate, mCompanyCode)
                        '                    mBS5 = 0
                        mREFUND5 = 0
                    Case 6
                        Call GetFieldValMonth(mPFAcCode, mWAGE6, mPF6, mBS6, mJUNEE, mJUNER, mFromDate, mCompanyCode)
                        '                    mBS6 = 0
                        mREFUND6 = 0
                    Case 7
                        Call GetFieldValMonth(mPFAcCode, mWAGE7, mPF7, mBS7, mJULEE, mJULER, mFromDate, mCompanyCode)
                        '                    mBS7 = 0
                        mREFUND7 = 0
                    Case 8
                        Call GetFieldValMonth(mPFAcCode, mWAGE8, mPF8, mBS8, mAUGEE, mAUGER, mFromDate, mCompanyCode)
                        '                    mBS8 = 0
                        mREFUND8 = 0
                    Case 9
                        Call GetFieldValMonth(mPFAcCode, mWAGE9, mPF9, mBS9, mSEPEE, mSEPER, mFromDate, mCompanyCode)
                        '                    mBS9 = 0
                        mREFUND9 = 0
                    Case 10
                        Call GetFieldValMonth(mPFAcCode, mWAGE10, mPF10, mBS10, mOCTEE, mOCTER, mFromDate, mCompanyCode)
                        '                    mBS10 = 0
                        mREFUND10 = 0
                    Case 11
                        Call GetFieldValMonth(mPFAcCode, mWAGE11, mPF11, mBS11, mNOVEE, mNOVER, mFromDate, mCompanyCode)
                        '                    mBS11 = 0
                        mREFUND11 = 0
                    Case 12
                        Call GetFieldValMonth(mPFAcCode, mWAGE12, mPF12, mBS12, mDECEE, mDECER, mFromDate, mCompanyCode)
                        '                    mBS12 = 0
                        mREFUND12 = 0
                End Select
                mFromDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, CDate(mFromDate)))
            Loop
            mWAGE13 = 0
            mPF13 = 0
            mBS13 = 0
            mOTHEE = 0
            mOTHER = 0
            mREFUND13 = 0

            SqlStr = "INSERT INTO D4024.DBF" & vbCrLf & " (EST_CODE,EST_EXTN,EMP_NO,EMP_NAME,VOL_PEN,TOTWAGES,PFT,BST," & vbCrLf & " WAGE1,WAGE2,WAGE3,WAGE4,WAGE5,WAGE6,WAGE7,WAGE8,WAGE9,WAGE10,WAGE11,WAGE12,WAGE13," & vbCrLf & " PF1,PF2,PF3,PF4,PF5,PF6,PF7,PF8,PF9,PF10,PF11,PF12,PF13," & vbCrLf & " BS1,BS2,BS3,BS4,BS5,BS6,BS7,BS8,BS9,BS10,BS11,BS12,BS13," & vbCrLf & " TOTEE,TOTER,TOTREF, " & vbCrLf & " JANEE,FEBEE,MAREE,APREE,MAYEE,JUNEE,JULEE,AUGEE,SEPEE,OCTEE,NOVEE,DECEE,OTHEE," & vbCrLf & " JANER,FEBER,MARER,APRER,MAYER,JUNER,JULER,AUGER,SEPER,OCTER,NOVER,DECER,OTHER," & vbCrLf & " REFUND1,REFUND2,REFUND3,REFUND4,REFUND5,REFUND6,REFUND7,REFUND8,REFUND9,REFUND10,REFUND11,REFUND12,REFUND13) " & vbCrLf & " VALUES(" & mEST_CODE & ",'" & mEST_EXTN & "'," & mEMP_NO & ",'" & mEMP_NAME & "','" & mVOL_PEN & "'," & mTotWages & "," & mPFT & "," & mBST & "," & vbCrLf & " " & mWAGE1 & "," & mWAGE2 & "," & mWAGE3 & "," & mWAGE4 & "," & mWAGE5 & "," & mWAGE6 & "," & mWAGE7 & "," & mWAGE8 & "," & mWAGE9 & "," & mWAGE10 & "," & mWAGE11 & "," & mWAGE12 & "," & mWAGE13 & "," & vbCrLf & " " & mPF1 & "," & mPF2 & "," & mPF3 & "," & mPF4 & "," & mPF5 & "," & mPF6 & "," & mPF7 & "," & mPF8 & "," & mPF9 & "," & mPF10 & "," & mPF11 & "," & mPF12 & "," & mPF13 & "," & vbCrLf & " " & mBS1 & "," & mBS2 & "," & mBS3 & "," & mBS4 & "," & mBS5 & "," & mBS6 & "," & mBS7 & "," & mBS8 & "," & mBS9 & "," & mBS10 & "," & mBS11 & "," & mBS12 & "," & mBS13 & "," & vbCrLf & " " & mTOTEE & "," & mTOTER & "," & mTOTREF & ", " & vbCrLf & " " & mJANEE & "," & mFEBEE & "," & mMAREE & "," & mAPREE & "," & mMAYEE & "," & mJUNEE & "," & mJULEE & "," & mAUGEE & "," & mSEPEE & "," & mOCTEE & "," & mNOVEE & "," & mDECEE & "," & mOTHEE & "," & vbCrLf & " " & mJANER & "," & mFEBER & "," & mMARER & "," & mAPRER & "," & mMAYER & "," & mJUNER & "," & mJULER & "," & mAUGER & "," & mSEPER & "," & mOCTER & "," & mNOVER & "," & mDECER & "," & mOTHER & "," & vbCrLf & " " & mREFUND1 & "," & mREFUND2 & "," & mREFUND3 & "," & mREFUND4 & "," & mREFUND5 & "," & mREFUND6 & "," & mREFUND7 & "," & mREFUND8 & "," & mREFUND9 & "," & mREFUND10 & "," & mREFUND11 & "," & mREFUND12 & "," & mREFUND13 & ") " & vbCrLf
            FoxPvtDBCn.Execute(SqlStr)
        Next
        FoxPvtDBCn.CommitTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FoxPvtDBCn.Cancel()
        FoxPvtDBCn.Close()
        FoxPvtDBCn = Nothing
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '    If FoxPvtDBCn.State = adStateClosed Then
        FoxPvtDBCn.RollbackTrans()
        '    End If
        '    Resume
    End Sub
    Private Sub cmdCD_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCD.Click

        On Error GoTo ErrPart
        Dim I As Integer
        Dim SqlStr As String = ""
        Dim mPFAcCode As String
        Dim MTempPFAcCode As String
        Dim mEmpName As String
        Dim mFromDate As String
        Dim mToDate As String
        Dim mCompanyCode As Integer
        Dim mEST_CODEStr As String
        Dim mEST_CODE As Double
        Dim mEST_EXTN As String
        Dim mEMP_NO As Double
        Dim mEMP_NAME As String
        Dim mVOL_PEN As String
        Dim mTotWages As Double
        Dim mPFT As Double
        Dim mBST As Double
        Dim mWAGE1 As Double
        Dim mWAGE2 As Double
        Dim mWAGE3 As Double
        Dim mWAGE4 As Double
        Dim mWAGE5 As Double
        Dim mWAGE6 As Double
        Dim mWAGE7 As Double
        Dim mWAGE8 As Double
        Dim mWAGE9 As Double
        Dim mWAGE10 As Double
        Dim mWAGE11 As Double
        Dim mWAGE12 As Double
        Dim mWAGE13 As Double
        Dim mPF1 As Double
        Dim mPF2 As Double
        Dim mPF3 As Double
        Dim mPF4 As Double
        Dim mPF5 As Double
        Dim mPF6 As Double
        Dim mPF7 As Double
        Dim mPF8 As Double
        Dim mPF9 As Double
        Dim mPF10 As Double
        Dim mPF11 As Double
        Dim mPF12 As Double
        Dim mPF13 As Double
        Dim mBS1 As Double
        Dim mBS2 As Double
        Dim mBS3 As Double
        Dim mBS4 As Double
        Dim mBS5 As Double
        Dim mBS6 As Double
        Dim mBS7 As Double
        Dim mBS8 As Double
        Dim mBS9 As Double
        Dim mBS10 As Double
        Dim mBS11 As Double
        Dim mBS12 As Double
        Dim mBS13 As Double
        Dim mTOTEE As Double
        Dim mTOTER As Double
        Dim mTOTREF As Double
        Dim mJANEE As Double
        Dim mFEBEE As Double
        Dim mMAREE As Double
        Dim mAPREE As Double
        Dim mMAYEE As Double
        Dim mJUNEE As Double
        Dim mJULEE As Double
        Dim mAUGEE As Double
        Dim mSEPEE As Double
        Dim mOCTEE As Double
        Dim mNOVEE As Double
        Dim mDECEE As Double
        Dim mOTHEE As Double
        Dim mJANER As Double
        Dim mFEBER As Double
        Dim mMARER As Double
        Dim mAPRER As Double
        Dim mMAYER As Double
        Dim mJUNER As Double
        Dim mJULER As Double
        Dim mAUGER As Double
        Dim mSEPER As Double
        Dim mOCTER As Double
        Dim mNOVER As Double
        Dim mDECER As Double
        Dim mOTHER As Double
        Dim mREFUND1 As Double
        Dim mREFUND2 As Double
        Dim mREFUND3 As Double
        Dim mREFUND4 As Double
        Dim mREFUND5 As Double
        Dim mREFUND6 As Double
        Dim mREFUND7 As Double
        Dim mREFUND8 As Double
        Dim mREFUND9 As Double
        Dim mREFUND10 As Double
        Dim mREFUND11 As Double
        Dim mREFUND12 As Double
        Dim mREFUND13 As Double

        Dim mLineCount As Integer
        Dim mDelimited As String

        Dim mMemberNo As Integer
        Dim mYear As Double
        'Dim mTOTWages As Double
        Dim mTOTContEE As Double
        Dim mTOTContER As Double
        Dim mTOTContP As Double
        Dim mTOTNCP As Double
        Dim mTOTArrear As Double
        Dim pFileName As String
        Dim mMainString As String

        mDelimited = "#~#"
        pFileName = mLocalPath & "\Form6A.txt"


        Call ShellAndContinue("ATTRIB +A -R " & pFileName)
        FileOpen(1, pFileName, OpenMode.Output)
        mLineCount = 1

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If RsCompany.Fields("COMPANY_CODE").Value = 6 Then
            mEST_CODEStr = Mid(RsCompany.Fields("PFEST").Value, 7, Len(RsCompany.Fields("PFEST").Value))
            mEST_CODEStr = Mid(mEST_CODEStr, 1, InStr(1, mEST_CODEStr, "/") - 1)
            mEST_CODE = Val(mEST_CODEStr) ''5415
        ElseIf RsCompany.Fields("COMPANY_CODE").Value = 16 Then
            mEST_CODE = 31507
        Else
            mEST_CODEStr = Mid(RsCompany.Fields("PFEST").Value, 4, Len(RsCompany.Fields("PFEST").Value))
            mEST_CODEStr = Mid(mEST_CODEStr, 1, InStr(1, mEST_CODEStr, "/") - 1)
            mEST_CODE = Val(mEST_CODEStr) ''5415
        End If

        sprdAttn.Row = sprdAttn.MaxRows


        mMemberNo = Val(CStr(sprdAttn.MaxRows - 2))
        mYear = Year(CDate(txtFrom.Text))

        sprdAttn.Col = ColWages
        mTotWages = Val(sprdAttn.Text)

        sprdAttn.Col = ColEmpCont
        mTOTContEE = Val(sprdAttn.Text)

        sprdAttn.Col = ColEPF
        mTOTContER = Val(sprdAttn.Text)

        sprdAttn.Col = ColPFund
        mTOTContP = Val(sprdAttn.Text)

        sprdAttn.Col = ColRemarks
        mTOTNCP = Val(sprdAttn.Text)

        sprdAttn.Col = ColPFund
        mTOTArrear = Val(sprdAttn.Text)


        mMainString = mEST_CODEStr
        mMainString = mMainString & mDelimited & mMemberNo
        mMainString = mMainString & mDelimited & mYear
        mMainString = mMainString & mDelimited & mTotWages
        mMainString = mMainString & mDelimited & mTOTContEE
        mMainString = mMainString & mDelimited & mTOTContER
        mMainString = mMainString & mDelimited & mTOTContP
        mMainString = mMainString & mDelimited & mTOTNCP
        mMainString = mMainString & mDelimited & "0"
        mMainString = mMainString & mDelimited & mTOTArrear

        PrintLine(1, TAB(0), mMainString)

        For I = 1 To sprdAttn.MaxRows - 2
            sprdAttn.Row = I

            sprdAttn.Col = ColAcctNo
            mPFAcCode = sprdAttn.Text
            MTempPFAcCode = mPFAcCode

            sprdAttn.Col = ColName
            mEmpName = UCase(Trim(sprdAttn.Text))

            sprdAttn.Col = ColCompanyCode
            mCompanyCode = Val(sprdAttn.Text)



            mEST_EXTN = ""
            Do While Not InStr(1, MTempPFAcCode, "/", CompareMethod.Binary) = 0
                MTempPFAcCode = Mid(MTempPFAcCode, InStr(1, MTempPFAcCode, "/", CompareMethod.Binary) + 1)
            Loop
            mEMP_NO = IIf(MTempPFAcCode = "", 0, MTempPFAcCode)
            mEMP_NAME = MainClass.AllowSingleQuote(mEmpName)
            mVOL_PEN = ""
            Call GetFieldValSumm(mPFAcCode, mTotWages, mPFT, mTOTEE, mTOTER, mCompanyCode)
            mBST = 0
            mTOTREF = 0

            mFromDate = "01-" & VB6.Format(txtFrom.Text, "MM-YYYY")
            mToDate = "01-" & VB6.Format(txtTo.Text, "MM-YYYY")

            Do While Not CDate(mFromDate) > CDate(mToDate)
                Select Case Month(CDate(mFromDate))
                    Case 1
                        Call GetFieldValMonth(mPFAcCode, mWAGE1, mPF1, mBS1, mJANEE, mJANER, mFromDate, mCompanyCode)
                        '                    mBS1 = 0
                        mREFUND1 = 0
                    Case 2
                        Call GetFieldValMonth(mPFAcCode, mWAGE2, mPF2, mBS2, mFEBEE, mFEBER, mFromDate, mCompanyCode)
                        '                    mBS2 = 0
                        mREFUND2 = 0
                    Case 3
                        Call GetFieldValMonth(mPFAcCode, mWAGE3, mPF3, mBS3, mMAREE, mMARER, mFromDate, mCompanyCode)
                        '                    mBS3 = 0
                        mREFUND3 = 0
                    Case 4
                        Call GetFieldValMonth(mPFAcCode, mWAGE4, mPF4, mBS4, mAPREE, mAPRER, mFromDate, mCompanyCode)
                        '                    mBS4 = 0
                        mREFUND4 = 0
                    Case 5
                        Call GetFieldValMonth(mPFAcCode, mWAGE5, mPF5, mBS5, mMAYEE, mMAYER, mFromDate, mCompanyCode)
                        '                    mBS5 = 0
                        mREFUND5 = 0
                    Case 6
                        Call GetFieldValMonth(mPFAcCode, mWAGE6, mPF6, mBS6, mJUNEE, mJUNER, mFromDate, mCompanyCode)
                        '                    mBS6 = 0
                        mREFUND6 = 0
                    Case 7
                        Call GetFieldValMonth(mPFAcCode, mWAGE7, mPF7, mBS7, mJULEE, mJULER, mFromDate, mCompanyCode)
                        '                    mBS7 = 0
                        mREFUND7 = 0
                    Case 8
                        Call GetFieldValMonth(mPFAcCode, mWAGE8, mPF8, mBS8, mAUGEE, mAUGER, mFromDate, mCompanyCode)
                        '                    mBS8 = 0
                        mREFUND8 = 0
                    Case 9
                        Call GetFieldValMonth(mPFAcCode, mWAGE9, mPF9, mBS9, mSEPEE, mSEPER, mFromDate, mCompanyCode)
                        '                    mBS9 = 0
                        mREFUND9 = 0
                    Case 10
                        Call GetFieldValMonth(mPFAcCode, mWAGE10, mPF10, mBS10, mOCTEE, mOCTER, mFromDate, mCompanyCode)
                        '                    mBS10 = 0
                        mREFUND10 = 0
                    Case 11
                        Call GetFieldValMonth(mPFAcCode, mWAGE11, mPF11, mBS11, mNOVEE, mNOVER, mFromDate, mCompanyCode)
                        '                    mBS11 = 0
                        mREFUND11 = 0
                    Case 12
                        Call GetFieldValMonth(mPFAcCode, mWAGE12, mPF12, mBS12, mDECEE, mDECER, mFromDate, mCompanyCode)
                        '                    mBS12 = 0
                        mREFUND12 = 0
                End Select
                mFromDate = CStr(DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, CDate(mFromDate)))
            Loop
            mWAGE13 = 0
            mPF13 = 0
            mBS13 = 0
            mOTHEE = 0
            mOTHER = 0
            mREFUND13 = 0

            ''Jan
            mMainString = mEST_CODEStr & mDelimited & VB6.Format(mFromDate, "MMYYYY") & mDelimited & "0" & mDelimited & "0" & mDelimited & "" & mDelimited & System.Math.Round(mWAGE1) & mDelimited & mPF1 & mDelimited & mBS1
            mMainString = mMainString & mDelimited & "0" & mDelimited & mJANEE & mDelimited & mJANER & mDelimited & mPF1 & mDelimited & "" & mDelimited & ""
            mMainString = mMainString & mDelimited & "" & mDelimited & "N" & mDelimited & "N" & mDelimited & "" & mDelimited & "12" & mDelimited & ""
            mMainString = mMainString & mDelimited & "1" & mDelimited & mEmpName & mDelimited & System.Math.Round(mWAGE1 + mWAGE2 + mWAGE3 + mWAGE4 + mWAGE5 + mWAGE6 + mWAGE7 + mWAGE8 + mWAGE9 + mWAGE10 + mWAGE11 + mWAGE12)
            mMainString = mMainString & mDelimited & (mJANEE + mFEBEE + mMAREE + mAPREE + mMAYEE + mJUNEE + mJULEE + mAUGEE + mSEPEE + mOCTEE + mNOVEE + mDECEE)
            mMainString = mMainString & mDelimited & (mJANER + mFEBER + mMARER + mAPRER + mMAYER + mJUNER + mJULER + mAUGER + mSEPER + mOCTER + mNOVER + mDECER)
            mMainString = mMainString & mDelimited & (mPF1 + mPF2 + mPF3 + mPF4 + mPF5 + mPF6 + mPF7 + mPF8 + mPF9 + mPF10 + mPF11 + mPF12)
            mMainString = mMainString & mDelimited & (mBS1 + mBS2 + mBS3 + mBS4 + mBS5 + mBS6 + mBS7 + mBS8 + mBS9 + mBS10 + mBS11 + mBS12)
            mMainString = mMainString & mDelimited & (mREFUND1 + mREFUND2 + mREFUND3 + mREFUND4 + mREFUND5 + mREFUND6 + mREFUND7 + mREFUND8 + mREFUND9 + mREFUND10 + mREFUND11 + mREFUND12)
            PrintLine(1, TAB(0), mMainString)

            ''feb
            mMainString = mEST_CODEStr & mDelimited & VB6.Format(mFromDate, "MMYYYY") & mDelimited & "0" & mDelimited & "0" & mDelimited & "" & mDelimited & System.Math.Round(mWAGE1) & mDelimited & mPF1 & mDelimited & mBS1
            mMainString = mMainString & mDelimited & "0" & mDelimited & mJANEE & mDelimited & mJANER & mDelimited & mPF1 & mDelimited & "" & mDelimited & ""
            mMainString = mMainString & mDelimited & "" & mDelimited & "N" & mDelimited & "N" & mDelimited & "" & mDelimited & "12" & mDelimited & ""
            mMainString = mMainString & mDelimited & "1" & mDelimited & mEmpName & mDelimited & System.Math.Round(mWAGE1 + mWAGE2 + mWAGE3 + mWAGE4 + mWAGE5 + mWAGE6 + mWAGE7 + mWAGE8 + mWAGE9 + mWAGE10 + mWAGE11 + mWAGE12)
            mMainString = mMainString & mDelimited & (mJANEE + mFEBEE + mMAREE + mAPREE + mMAYEE + mJUNEE + mJULEE + mAUGEE + mSEPEE + mOCTEE + mNOVEE + mDECEE)
            mMainString = mMainString & mDelimited & (mJANER + mFEBER + mMARER + mAPRER + mMAYER + mJUNER + mJULER + mAUGER + mSEPER + mOCTER + mNOVER + mDECER)
            mMainString = mMainString & mDelimited & (mPF1 + mPF2 + mPF3 + mPF4 + mPF5 + mPF6 + mPF7 + mPF8 + mPF9 + mPF10 + mPF11 + mPF12)
            mMainString = mMainString & mDelimited & (mBS1 + mBS2 + mBS3 + mBS4 + mBS5 + mBS6 + mBS7 + mBS8 + mBS9 + mBS10 + mBS11 + mBS12)
            mMainString = mMainString & mDelimited & (mREFUND1 + mREFUND2 + mREFUND3 + mREFUND4 + mREFUND5 + mREFUND6 + mREFUND7 + mREFUND8 + mREFUND9 + mREFUND10 + mREFUND11 + mREFUND12)
            PrintLine(1, TAB(0), mMainString)

            ''mar
            mMainString = mEST_CODEStr & mDelimited & VB6.Format(mFromDate, "MMYYYY") & mDelimited & "0" & mDelimited & "0" & mDelimited & "" & mDelimited & System.Math.Round(mWAGE1) & mDelimited & mPF1 & mDelimited & mBS1
            mMainString = mMainString & mDelimited & "0" & mDelimited & mJANEE & mDelimited & mJANER & mDelimited & mPF1 & mDelimited & "" & mDelimited & ""
            mMainString = mMainString & mDelimited & "" & mDelimited & "N" & mDelimited & "N" & mDelimited & "" & mDelimited & "12" & mDelimited & ""
            mMainString = mMainString & mDelimited & "1" & mDelimited & mEmpName & mDelimited & System.Math.Round(mWAGE1 + mWAGE2 + mWAGE3 + mWAGE4 + mWAGE5 + mWAGE6 + mWAGE7 + mWAGE8 + mWAGE9 + mWAGE10 + mWAGE11 + mWAGE12)
            mMainString = mMainString & mDelimited & (mJANEE + mFEBEE + mMAREE + mAPREE + mMAYEE + mJUNEE + mJULEE + mAUGEE + mSEPEE + mOCTEE + mNOVEE + mDECEE)
            mMainString = mMainString & mDelimited & (mJANER + mFEBER + mMARER + mAPRER + mMAYER + mJUNER + mJULER + mAUGER + mSEPER + mOCTER + mNOVER + mDECER)
            mMainString = mMainString & mDelimited & (mPF1 + mPF2 + mPF3 + mPF4 + mPF5 + mPF6 + mPF7 + mPF8 + mPF9 + mPF10 + mPF11 + mPF12)
            mMainString = mMainString & mDelimited & (mBS1 + mBS2 + mBS3 + mBS4 + mBS5 + mBS6 + mBS7 + mBS8 + mBS9 + mBS10 + mBS11 + mBS12)
            mMainString = mMainString & mDelimited & (mREFUND1 + mREFUND2 + mREFUND3 + mREFUND4 + mREFUND5 + mREFUND6 + mREFUND7 + mREFUND8 + mREFUND9 + mREFUND10 + mREFUND11 + mREFUND12)
            PrintLine(1, TAB(0), mMainString)

            ''Apr
            mMainString = mEST_CODEStr & mDelimited & VB6.Format(mFromDate, "MMYYYY") & mDelimited & "0" & mDelimited & "0" & mDelimited & "" & mDelimited & System.Math.Round(mWAGE1) & mDelimited & mPF1 & mDelimited & mBS1
            mMainString = mMainString & mDelimited & "0" & mDelimited & mJANEE & mDelimited & mJANER & mDelimited & mPF1 & mDelimited & "" & mDelimited & ""
            mMainString = mMainString & mDelimited & "" & mDelimited & "N" & mDelimited & "N" & mDelimited & "" & mDelimited & "12" & mDelimited & ""
            mMainString = mMainString & mDelimited & "1" & mDelimited & mEmpName & mDelimited & System.Math.Round(mWAGE1 + mWAGE2 + mWAGE3 + mWAGE4 + mWAGE5 + mWAGE6 + mWAGE7 + mWAGE8 + mWAGE9 + mWAGE10 + mWAGE11 + mWAGE12)
            mMainString = mMainString & mDelimited & (mJANEE + mFEBEE + mMAREE + mAPREE + mMAYEE + mJUNEE + mJULEE + mAUGEE + mSEPEE + mOCTEE + mNOVEE + mDECEE)
            mMainString = mMainString & mDelimited & (mJANER + mFEBER + mMARER + mAPRER + mMAYER + mJUNER + mJULER + mAUGER + mSEPER + mOCTER + mNOVER + mDECER)
            mMainString = mMainString & mDelimited & (mPF1 + mPF2 + mPF3 + mPF4 + mPF5 + mPF6 + mPF7 + mPF8 + mPF9 + mPF10 + mPF11 + mPF12)
            mMainString = mMainString & mDelimited & (mBS1 + mBS2 + mBS3 + mBS4 + mBS5 + mBS6 + mBS7 + mBS8 + mBS9 + mBS10 + mBS11 + mBS12)
            mMainString = mMainString & mDelimited & (mREFUND1 + mREFUND2 + mREFUND3 + mREFUND4 + mREFUND5 + mREFUND6 + mREFUND7 + mREFUND8 + mREFUND9 + mREFUND10 + mREFUND11 + mREFUND12)
            PrintLine(1, TAB(0), mMainString)

        Next


        FileClose(1)

        Shell("ATTRIB +R -A " & pFileName)
        Shell("NOTEPAD.EXE " & pFileName, AppWinStyle.MaximizedFocus)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '    If FoxPvtDBCn.State = adStateClosed Then
        '    End If
        '    Resume
        FileClose(1)
    End Sub
    Private Function GetFieldValSumm(ByRef pPFAcCode As String, ByRef pTotWages As Double, ByRef pPFT As Double, ByRef pTOTEE As Double, ByRef pTOTER As Double, ByRef pCompanyCode As Integer) As Boolean

        On Error GoTo GetERR
        Dim RsGetSumm As ADODB.Recordset


        SqlStr = "SELECT SUM(TOT_WAGES) AS TotWages,SUM(EPF_833) AS PFT, " & vbCrLf & " SUM(EPF_AMT+VPFAMT) AS TOTEE,SUM(EPF_367) AS TOTER " & vbCrLf & " FROM PAY_CONTSALARY_TRN " & vbCrLf & " WHERE COMPANY_CODE =" & pCompanyCode & " " & vbCrLf & " AND EDATE BETWEEN TO_DATE('" & VB6.Format(txtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf & " AND PFAC_CODE='" & pPFAcCode & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGetSumm, ADODB.LockTypeEnum.adLockReadOnly)
        If Not RsGetSumm.EOF Then
            pTotWages = Val(IIf(IsDBNull(RsGetSumm.Fields("TOTWAGES").Value), 0, RsGetSumm.Fields("TOTWAGES").Value))
            pPFT = Val(IIf(IsDBNull(RsGetSumm.Fields("PFT").Value), 0, RsGetSumm.Fields("PFT").Value))
            pTOTEE = Val(IIf(IsDBNull(RsGetSumm.Fields("TOTEE").Value), 0, RsGetSumm.Fields("TOTEE").Value))
            pTOTER = Val(IIf(IsDBNull(RsGetSumm.Fields("TOTER").Value), 0, RsGetSumm.Fields("TOTER").Value))
        Else
            pTotWages = 0
            pPFT = 0
            pTOTEE = 0
            pTOTER = 0
        End If
        GetFieldValSumm = True
        RsGetSumm.Close()
        Exit Function
GetERR:
        pTotWages = 0
        pPFT = 0
        pTOTEE = 0
        pTOTER = 0
        GetFieldValSumm = False
        MsgBox(Err.Description)
    End Function
    Private Function GetFieldValMonth(ByRef pPFAcCode As String, ByRef pWages As Double, ByRef pPF As Double, ByRef pBS As Double, ByRef pEE As Double, ByRef pER As Double, ByRef pDate As String, ByRef pCompanyCode As Integer) As Boolean

        On Error GoTo GetERR
        Dim RsGetSumm As ADODB.Recordset


        SqlStr = "SELECT SUM(TOT_WAGES) AS Wages,SUM(EPF_833) AS PF, " & vbCrLf & " SUM(EPF_AMT+VPFAMT) AS EE,SUM(EPF_367) AS ER, SUM(WDAYS) AS WDAYS " & vbCrLf & " FROM PAY_CONTSALARY_TRN " & vbCrLf & " WHERE COMPANY_CODE =" & pCompanyCode & " " & vbCrLf & " AND TO_CHAR(EDATE,'MON-YYYY') ='" & UCase(VB6.Format(pDate, "MMM-YYYY")) & "'" & vbCrLf & " AND PFAC_CODE='" & pPFAcCode & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsGetSumm, ADODB.LockTypeEnum.adLockReadOnly)
        If Not RsGetSumm.EOF Then
            pWages = Val(IIf(IsDBNull(RsGetSumm.Fields("Wages").Value), 0, RsGetSumm.Fields("Wages").Value))
            pPF = Val(IIf(IsDBNull(RsGetSumm.Fields("PF").Value), 0, RsGetSumm.Fields("PF").Value))
            pEE = Val(IIf(IsDBNull(RsGetSumm.Fields("EE").Value), 0, RsGetSumm.Fields("EE").Value))
            pER = Val(IIf(IsDBNull(RsGetSumm.Fields("ER").Value), 0, RsGetSumm.Fields("ER").Value))
            pBS = Val(IIf(IsDBNull(RsGetSumm.Fields("WDAYS").Value), 0, RsGetSumm.Fields("WDAYS").Value))
        Else
            pWages = 0
            pPF = 0
            pEE = 0
            pER = 0
            pBS = 0
        End If
        GetFieldValMonth = True
        RsGetSumm.Close()
        Exit Function
GetERR:
        pWages = 0
        pPF = 0
        pEE = 0
        pER = 0
        pBS = 0
        GetFieldValMonth = False
        MsgBox(Err.Description)
    End Function

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
    End Sub

    Private Sub cmdPage_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPage.Click
        If cmdPage.Text = ConCmdFCaption Then
            cmdPage.Text = ConCmdBCaption
            sprdAttn.Visible = True
            If lblBookType.Text = "E" Then
                Me.Text = "FORM 6A [ Staff ]"
            ElseIf lblBookType.Text = "C" Then
                Me.Text = "FORM 6A [ All ]"
            End If
            sprdBack.Visible = False
        Else
            cmdPage.Text = ConCmdFCaption
            sprdAttn.Visible = False
            sprdBack.Visible = True
            Me.Text = "Reconcilation of Remittances"
        End If
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPrint(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportForPrint(ByRef Mode As Crystal.DestinationConstants)

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mRPTName As String
        PubDBCn.Errors.Clear()


        'Insert Data from Grid to PrintDummyData Table...

        If sprdAttn.Visible = True Then
            If FillPrintDummyData(sprdAttn, 1, sprdAttn.MaxRows, 0, sprdAttn.MaxCols, PubDBCn) = False Then GoTo ERR1
            mTitle = "FORM 6A"
            mRPTName = "EPFFORM6.Rpt"
        Else
            If FillPrintDummyData(sprdBack, 1, sprdBack.MaxRows, 0, sprdBack.MaxCols, PubDBCn) = False Then GoTo ERR1
            mTitle = "Reconcilation of Remittances"
            mRPTName = "EPFFORM6Back.Rpt"
        End If

        'Select Record for print...

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        mSubTitle = "From " & MonthName(Month(CDate(txtFrom.Text))) & ", " & Year(CDate(txtFrom.Text)) & " To : " & MonthName(Month(CDate(txtTo.Text))) & ", " & Year(CDate(txtTo.Text))


        Call ShowReport(SqlStr, mRPTName, Mode, mTitle, mSubTitle)

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        If Err.Number = 32755 Or Err.Number = 20507 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        'Resume
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)

        Dim mCompanyPF As String

        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        mCompanyPF = IIf(IsDBNull(RsCompany.Fields("PFEST").Value), "", RsCompany.Fields("PFEST").Value)
        MainClass.AssignCRptFormulas(Report1, "CompanyPF=""" & mCompanyPF & """")

        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportForPrint(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdRefresh_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRefresh.Click
        RefreshScreen()
        RefreshScreenBack()
    End Sub
    Private Sub frmEPFForm6_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        If lblBookType.Text = "E" Then
            Me.Text = "FORM 6A [ Staff ]"
        ElseIf lblBookType.Text = "C" Then
            Me.Text = "FORM 6A [ All ]"
        End If
        'RefreshScreen
    End Sub
    Private Sub frmEPFForm6_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False

        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)
        optCardNo.Checked = True
        txtFrom.Text = VB6.Format(RsCompany.Fields("START_DATE").Value, "DD/MM/YYYY") ''DateAdd("m", -1, RsCompany!START_DATE)
        txtTo.Text = VB6.Format(RsCompany.Fields("END_DATE").Value, "DD/MM/YYYY") '' DateAdd("m", -1, RsCompany!END_DATE)

        Call FillContCombo()
        cboEmployee.Enabled = False

        chkAll.CheckState = System.Windows.Forms.CheckState.Checked

        sprdBack.Visible = True
        FormatSprd(-1)
        FillHeading()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        '    Resume
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub
    Private Sub FillContCombo()

        Dim RsDept As ADODB.Recordset = Nothing
        SqlStr = "Select DISTINCT CONT_NAME from PAY_CONTSALARY_TRN WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " Order by CONT_NAME"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDept, ADODB.LockTypeEnum.adLockOptimistic)

        If RsDept.EOF = False Then
            Do While Not RsDept.EOF
                cboEmployee.Items.Add(IIf(IsDBNull(RsDept.Fields("CONT_NAME").Value), "-", RsDept.Fields("CONT_NAME").Value))
                RsDept.MoveNext()
            Loop
            cboEmployee.SelectedIndex = 0
        End If

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub
    Private Sub RefreshScreen()

        On Error GoTo ErrPart
        Dim RsAttn As ADODB.Recordset = Nothing
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mYM As String

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
            If cboEmployee.Text = "" Then
                MsgInformation("Please select the Department Name.")
                cboEmployee.Focus()
                Exit Sub
            End If
        End If

        MainClass.ClearGrid(sprdAttn)

        If lblBookType.Text = "E" Then
            mYM = "SAL_DATE BETWEEN TO_DATE('" & VB6.Format(txtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            SqlStr = " SELECT EMP.EMP_CODE, SUM(PFABLEAMT), SUM(PFAMT+VPFAMT), " & vbCrLf & " SUM(EPFAMT), SUM(PENSIONFUND), " & vbCrLf & " EMP.EMP_NAME, EMP.EMP_FNAME, EMP.EMP_CODE, " & vbCrLf & " EMP.EMP_PF_ACNO, PFESITRN.PFRATE,EMP.COMPANY_CODE, MAX(VPFRATE) AS VPFRATE " & vbCrLf & " FROM PAY_PFESI_TRN PFESITRN,PAY_EMPLOYEE_MST EMP WHERE" & vbCrLf & " PFESITRN.COMPANY_CODE =EMP.COMPANY_CODE AND  " & vbCrLf & " PFESITRN.EMP_CODE =EMP.EMP_CODE AND  "


            If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                SqlStr = SqlStr & vbCrLf & " PFESITRN.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " "
            Else
                SqlStr = SqlStr & vbCrLf & " PFESITRN.COMPANY_CODE  IN (" & RsCompany.Fields("COMPANY_CODE").Value & ", 4,11,15,25) "
            End If


            SqlStr = SqlStr & vbCrLf & " AND " & mYM & "" ' AND ESIRATE>0"

            '        If chkAll.Value = vbUnchecked Then
            '            SqlStr = SqlStr & vbCrLf & "AND PFESITRN.CONT_NAME='" & MainClass.AllowSingleQuote(Trim(cboEmployee)) & "' "
            '        End If



            SqlStr = SqlStr & vbCrLf & " GROUP BY EMP.COMPANY_CODE,EMP.EMP_CODE,EMP.EMP_NAME,EMP.EMP_PF_ACNO,PFESITRN.PFRATE, EMP.EMP_FNAME "

            SqlStr = SqlStr & vbCrLf & " HAVING SUM(PFAMT+VPFAMT)>0"

            If OptName.Checked = True Then
                SqlStr = SqlStr & vbCrLf & " Order by EMP.EMP_NAME"
            Else
                SqlStr = SqlStr & vbCrLf & " Order by EMP.EMP_PF_ACNO"
            End If
        Else
            mYM = "EDATE BETWEEN TO_DATE('" & VB6.Format(txtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            ''
            ''SUM(EPF_AMT*3.67/100), SUM(EPF_AMT-(EPF_AMT*3.67/100))

            ''EPF_AMT-EPF_833

            SqlStr = " SELECT '' AS EMP_CODE, SUM(TOT_WAGES), SUM(EPF_AMT+VPFAMT), " & vbCrLf & " SUM(EPF_367), SUM(EPF_833), " & vbCrLf & " EMP.EMP_NAME, EMP.EMP_FNAME, '' AS EMP_CODE, " & vbCrLf & " EMP.PFAC_CODE AS EMP_PF_ACNO, 12 AS PFRATE,EMP.COMPANY_CODE, MAX(VPFRATE) AS VPFRATE " & vbCrLf & " FROM PAY_CONTSALARY_TRN EMP WHERE "

            If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                SqlStr = SqlStr & vbCrLf & " EMP.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " "
            Else
                SqlStr = SqlStr & vbCrLf & " EMP.COMPANY_CODE  IN (" & RsCompany.Fields("COMPANY_CODE").Value & ", 4,11,15,25) "
            End If


            SqlStr = SqlStr & vbCrLf & " AND " & mYM & "" ' AND ESIRATE>0"

            If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                SqlStr = SqlStr & vbCrLf & "AND EMP.CONT_NAME='" & MainClass.AllowSingleQuote(Trim(cboEmployee.Text)) & "' "
            End If

            SqlStr = SqlStr & vbCrLf & " GROUP BY EMP.COMPANY_CODE, EMP.EMP_NAME,EMP.PFAC_CODE, EMP.EMP_FNAME "

            SqlStr = SqlStr & vbCrLf & " HAVING SUM(TOT_WAGES)>0"

            If OptName.Checked = True Then
                SqlStr = SqlStr & vbCrLf & " Order by EMP.EMP_NAME"
            Else
                SqlStr = SqlStr & vbCrLf & " Order by EMP.PFAC_CODE"
            End If
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAttn, ADODB.LockTypeEnum.adLockOptimistic)

        If RsAttn.EOF = False Then
            With sprdAttn
                cntRow = 1
                Do While Not RsAttn.EOF
                    .Row = cntRow

                    .Col = 0
                    .Text = CStr(cntRow)

                    .Col = ColCodeNo
                    .Text = CStr(IIf(IsDBNull(RsAttn.Fields("EMP_Code").Value), "", RsAttn.Fields("EMP_Code").Value))

                    .Col = ColAcctNo
                    .Text = IIf(IsDBNull(RsAttn.Fields("EMP_PF_ACNO").Value), "", RsAttn.Fields("EMP_PF_ACNO").Value)

                    .Col = ColName
                    .Text = IIf(IsDBNull(RsAttn.Fields("EMP_NAME").Value), "", RsAttn.Fields("EMP_NAME").Value)

                    .Col = ColFName
                    .Text = IIf(IsDBNull(RsAttn.Fields("EMP_FNAME").Value), "", RsAttn.Fields("EMP_FNAME").Value)

                    .Col = ColWages
                    .Text = MainClass.FormatRupees(RsAttn.Fields(1))

                    .Col = ColEmpCont
                    .Text = MainClass.FormatRupees(RsAttn.Fields(2))

                    .Col = ColEPF
                    .Text = MainClass.FormatRupees(RsAttn.Fields(3))

                    .Col = ColPFund
                    .Text = MainClass.FormatRupees(RsAttn.Fields(4))

                    .Col = ColRate
                    .Text = VB6.Format(IIf(IsDBNull(RsAttn.Fields("VPFRATE").Value), 0, RsAttn.Fields("VPFRATE").Value)) '' Str(GetVPFRate(IIf(IsNull(RsAttn!PFRATE), 0, RsAttn!PFRATE)))

                    .Col = ColCompanyCode
                    .Text = Str(IIf(IsDBNull(RsAttn.Fields("COMPANY_CODE").Value), "", RsAttn.Fields("COMPANY_CODE").Value))

                    RsAttn.MoveNext()
                    If Not RsAttn.EOF Then
                        cntRow = cntRow + 1
                        .MaxRows = cntRow
                    End If
                Loop

                ColTotal(sprdAttn, ColWages, ColPFund)
                .Col = ColName
                .Row = .MaxRows
                .Text = "TOTAL :"


                MainClass.ProtectCell(sprdAttn, 0, .MaxRows, 0, .MaxCols)

            End With
        End If
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub
    Private Sub RefreshScreenBackOld()

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mYM As String
        Dim mPFTotal As Double
        Dim mDLITotal As Double
        Dim mADMTotal As Double
        Dim mEDLITotal As Double
        MainClass.ClearGrid(sprdBack)

        If lblBookType.Text = "E" Then
            mYM = "SAL_DATE BETWEEN TO_DATE('" & VB6.Format(txtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            SqlStr = " SELECT TO_CHAR(PFESITRN.SAL_DATE,'YYYYMM') AS SAL_DATE,SUM(PFABLEAMT),SUM(PFAMT+VPFAMT), " & vbCrLf & " SUM(EPFAMT),SUM(PENSIONFUND) FROM " & vbCrLf & " PAY_PFESI_TRN PFESITRN,PAY_EMPLOYEE_MST EMP WHERE" & vbCrLf & " PFESITRN.COMPANY_CODE =EMP.COMPANY_CODE AND  " & vbCrLf & " PFESITRN.EMP_CODE =EMP.EMP_CODE AND  "

            If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                SqlStr = SqlStr & vbCrLf & " PFESITRN.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " "
            Else
                SqlStr = SqlStr & vbCrLf & " PFESITRN.COMPANY_CODE  IN (" & RsCompany.Fields("COMPANY_CODE").Value & ", 4,11,15,25) "
            End If



            SqlStr = SqlStr & vbCrLf & " AND " & mYM & "" ' AND ESIRATE>0"

            '                & " PFESITRN.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " AND  " & vbCrLf _
            ''                & " " & mYM & ""    ' AND ESIRATE>0"

            SqlStr = SqlStr & vbCrLf & " GROUP BY TO_CHAR(PFESITRN.SAL_DATE,'YYYYMM')"

            SqlStr = SqlStr & vbCrLf & " HAVING SUM(PFAMT+VPFAMT)>0"

            SqlStr = SqlStr & vbCrLf & " Order by TO_CHAR(PFESITRN.SAL_DATE,'YYYYMM')"

        Else
            mYM = "EDATE BETWEEN TO_DATE('" & VB6.Format(txtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            SqlStr = " SELECT TO_CHAR(EMP.EDATE,'YYYYMM') AS SAL_DATE,SUM(TOT_WAGES),SUM(EPF_833+VPFAMT), " & vbCrLf & " SUM(EPF_AMT),SUM(EPF_367) FROM " & vbCrLf & " PAY_CONTSALARY_TRN EMP WHERE"

            If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                SqlStr = SqlStr & vbCrLf & " EMP.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " "
            Else
                SqlStr = SqlStr & vbCrLf & " EMP.COMPANY_CODE  IN (" & RsCompany.Fields("COMPANY_CODE").Value & ", 4,11,15,25) "
            End If


            SqlStr = SqlStr & vbCrLf & " AND " & mYM & "" ' AND ESIRATE>0"
            '
            '                & " EMP.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " AND  " & vbCrLf _
            ''                & " " & mYM & ""    ' AND ESIRATE>0"

            SqlStr = SqlStr & vbCrLf & " GROUP BY TO_CHAR(EMP.EDATE,'YYYYMM')"

            SqlStr = SqlStr & vbCrLf & " HAVING SUM(EPF_AMT+VPFAMT)>0"

            SqlStr = SqlStr & vbCrLf & " Order by TO_CHAR(EMP.EDATE,'YYYYMM')"
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            With sprdBack
                cntRow = 1
                Do While Not RsTemp.EOF
                    .Row = cntRow

                    .Col = 0
                    .Text = CStr(cntRow)

                    .Col = ColMonth
                    '                If Month(RsTemp!SAL_DATE) = 4 Then
                    '                    .Text = "March Paid in April"
                    '                ElseIf Month(RsTemp!SAL_DATE) = 3 Then
                    '                    .Text = "Feb. Paid in March"
                    '                Else
                    '                    .Text = MonthName(Month(DateAdd("m", -1, RsTemp!SAL_DATE)))
                    '                End If

                    If Val(VB.Right(RsTemp.Fields("SAL_DATE").Value, 2)) = 4 Then
                        .Text = "March Paid in April"
                    ElseIf Val(VB.Right(RsTemp.Fields("SAL_DATE").Value, 2)) = 3 Then
                        .Text = "Feb. Paid in March"
                    Else
                        '                    .Text = MonthName(Month(DateAdd("m", -1, RsTemp!SAL_DATE)))
                        .Text = MonthName(IIf(Val(VB.Right(RsTemp.Fields("SAL_DATE").Value, 2)) = 1, 12, Val(VB.Right(RsTemp.Fields("SAL_DATE").Value, 2)) - 1))
                    End If

                    .Col = ColEPFAc
                    .Text = MainClass.FormatRupees(System.Math.Round(RsTemp.Fields(2).Value + RsTemp.Fields(3).Value, 0))


                    'TO_CHAR(PFESITRN.SAL_DATE,'YYYYMM') AS SAL_DATE,SUM(PFABLEAMT),SUM(PFAMT+VPFAMT),SUM(EPFAMT),SUM(PENSIONFUND)

                    .Col = ColPF
                    .Text = MainClass.FormatRupees(System.Math.Round(RsTemp.Fields(4).Value, 0))

                    .Col = ColDLI
                    mDLITotal = System.Math.Round(IIf(IsDBNull(RsTemp.Fields(1).Value), 0, RsTemp.Fields(1).Value) * 0.5 / 100, 0)
                    .Text = MainClass.FormatRupees(mDLITotal)

                    .Col = ColADM
                    mADMTotal = System.Math.Round(IIf(IsDBNull(RsTemp.Fields(1).Value), 0, RsTemp.Fields(1).Value) * 1.1 / 100, 0)
                    .Text = MainClass.FormatRupees(mADMTotal)

                    .Col = ColEDLI
                    mEDLITotal = System.Math.Round(IIf(IsDBNull(RsTemp.Fields(1).Value), 0, RsTemp.Fields(1).Value) * 0.01 / 100, 0)
                    .Text = MainClass.FormatRupees(mEDLITotal)

                    .Col = ColPFTotal
                    mPFTotal = System.Math.Round(mDLITotal + mADMTotal + mEDLITotal, 0)
                    .Text = MainClass.FormatRupees(mPFTotal)

                    RsTemp.MoveNext()
                    If Not RsTemp.EOF Then
                        cntRow = cntRow + 1
                        .MaxRows = cntRow
                    End If
                Loop

                ColTotal(sprdBack, ColEPFAc, ColPFTotal)
                .Col = ColMonth
                .Row = .MaxRows
                .Text = "TOTAL :"


                MainClass.ProtectCell(sprdBack, 0, .MaxRows, 0, .MaxCols)

            End With
        End If
    End Sub

    Private Sub RefreshScreenBack()

        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntRow As Integer
        Dim cntCol As Integer
        Dim mYM As String
        Dim mPFTotal As Double
        Dim mDLITotal As Double
        Dim mADMTotal As Double
        Dim mEDLITotal As Double
        MainClass.ClearGrid(sprdBack)

        If lblBookType.Text = "E" Then
            mYM = "SAL_DATE BETWEEN TO_DATE('" & VB6.Format(txtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            SqlStr = " SELECT TO_CHAR(PFESITRN.SAL_DATE,'YYYYMM') AS SAL_DATE, SUM(PFABLEAMT), SUM(PFAMT+VPFAMT), " & vbCrLf & " SUM(EPFAMT), SUM(PENSIONFUND) " & vbCrLf & " FROM PAY_PFESI_TRN PFESITRN,PAY_EMPLOYEE_MST EMP WHERE" & vbCrLf & " PFESITRN.COMPANY_CODE =EMP.COMPANY_CODE AND  " & vbCrLf & " PFESITRN.EMP_CODE =EMP.EMP_CODE AND  "


            If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                SqlStr = SqlStr & vbCrLf & " PFESITRN.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " "
            Else
                SqlStr = SqlStr & vbCrLf & " PFESITRN.COMPANY_CODE  IN (" & RsCompany.Fields("COMPANY_CODE").Value & ", 4,11,15,25) "
            End If


            SqlStr = SqlStr & vbCrLf & " AND " & mYM & "" ' AND ESIRATE>0"

            '        If chkAll.Value = vbUnchecked Then
            '            SqlStr = SqlStr & vbCrLf & "AND PFESITRN.CONT_NAME='" & MainClass.AllowSingleQuote(Trim(cboEmployee)) & "' "
            '        End If



            SqlStr = SqlStr & vbCrLf & " GROUP BY TO_CHAR(PFESITRN.SAL_DATE,'YYYYMM')"

            SqlStr = SqlStr & vbCrLf & " HAVING SUM(PFAMT+VPFAMT)>0"

            SqlStr = SqlStr & vbCrLf & " Order by TO_CHAR(PFESITRN.SAL_DATE,'YYYYMM')"

            '        If OptName.Value = True Then
            '            SqlStr = SqlStr & vbCrLf & " Order by EMP.EMP_NAME"
            '        Else
            '            SqlStr = SqlStr & vbCrLf & " Order by EMP.EMP_PF_ACNO"
            '        End If
        Else
            mYM = "EDATE BETWEEN TO_DATE('" & VB6.Format(txtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " AND TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            ''
            ''SUM(EPF_AMT*3.67/100), SUM(EPF_AMT-(EPF_AMT*3.67/100))

            ''EPF_AMT-EPF_833

            SqlStr = " SELECT TO_CHAR(EMP.EDATE,'YYYYMM') AS SAL_DATE, SUM(TOT_WAGES), SUM(EPF_AMT+VPFAMT), " & vbCrLf & " SUM(EPF_367), SUM(EPF_833)" & vbCrLf & " FROM PAY_CONTSALARY_TRN EMP WHERE "

            If chkConsolidated.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                SqlStr = SqlStr & vbCrLf & " EMP.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " "
            Else
                SqlStr = SqlStr & vbCrLf & " EMP.COMPANY_CODE  IN (" & RsCompany.Fields("COMPANY_CODE").Value & ", 4,11,15,25) "
            End If


            SqlStr = SqlStr & vbCrLf & " AND " & mYM & "" ' AND ESIRATE>0"

            If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked Then
                SqlStr = SqlStr & vbCrLf & "AND EMP.CONT_NAME='" & MainClass.AllowSingleQuote(Trim(cboEmployee.Text)) & "' "
            End If

            SqlStr = SqlStr & vbCrLf & " GROUP BY TO_CHAR(EMP.EDATE,'YYYYMM')"

            SqlStr = SqlStr & vbCrLf & " HAVING SUM(TOT_WAGES)>0"

            SqlStr = SqlStr & vbCrLf & " Order by TO_CHAR(EMP.EDATE,'YYYYMM')"

            '        If OptName.Value = True Then
            '            SqlStr = SqlStr & vbCrLf & " Order by EMP.EMP_NAME"
            '        Else
            '            SqlStr = SqlStr & vbCrLf & " Order by EMP.PFAC_CODE"
            '        End If
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            With sprdBack
                cntRow = 1
                Do While Not RsTemp.EOF
                    .Row = cntRow

                    .Col = 0
                    .Text = CStr(cntRow)

                    .Col = ColMonth
                    '                If Month(RsTemp!SAL_DATE) = 4 Then
                    '                    .Text = "March Paid in April"
                    '                ElseIf Month(RsTemp!SAL_DATE) = 3 Then
                    '                    .Text = "Feb. Paid in March"
                    '                Else
                    '                    .Text = MonthName(Month(DateAdd("m", -1, RsTemp!SAL_DATE)))
                    '                End If

                    If Val(VB.Right(RsTemp.Fields("SAL_DATE").Value, 2)) = 4 Then
                        .Text = "March Paid in April"
                    ElseIf Val(VB.Right(RsTemp.Fields("SAL_DATE").Value, 2)) = 3 Then
                        .Text = "Feb. Paid in March"
                    Else
                        '                    .Text = MonthName(Month(DateAdd("m", -1, RsTemp!SAL_DATE)))
                        .Text = MonthName(IIf(Val(VB.Right(RsTemp.Fields("SAL_DATE").Value, 2)) = 1, 12, Val(VB.Right(RsTemp.Fields("SAL_DATE").Value, 2)) - 1))
                    End If

                    .Col = ColEPFAc
                    .Text = MainClass.FormatRupees(System.Math.Round(RsTemp.Fields(2).Value + RsTemp.Fields(3).Value, 0))


                    'TO_CHAR(PFESITRN.SAL_DATE,'YYYYMM') AS SAL_DATE,SUM(PFABLEAMT),SUM(PFAMT+VPFAMT),SUM(EPFAMT),SUM(PENSIONFUND)

                    .Col = ColPF
                    .Text = MainClass.FormatRupees(System.Math.Round(RsTemp.Fields(4).Value, 0))

                    .Col = ColDLI
                    mDLITotal = System.Math.Round(IIf(IsDBNull(RsTemp.Fields(1).Value), 0, RsTemp.Fields(1).Value) * 0.5 / 100, 0)
                    .Text = MainClass.FormatRupees(mDLITotal)

                    .Col = ColADM
                    mADMTotal = System.Math.Round(IIf(IsDBNull(RsTemp.Fields(1).Value), 0, RsTemp.Fields(1).Value) * 1.1 / 100, 0)
                    .Text = MainClass.FormatRupees(mADMTotal)

                    .Col = ColEDLI
                    mEDLITotal = System.Math.Round(IIf(IsDBNull(RsTemp.Fields(1).Value), 0, RsTemp.Fields(1).Value) * 0.01 / 100, 0)
                    .Text = MainClass.FormatRupees(mEDLITotal)

                    .Col = ColPFTotal
                    mPFTotal = System.Math.Round(mDLITotal + mADMTotal + mEDLITotal, 0)
                    .Text = MainClass.FormatRupees(mPFTotal)

                    RsTemp.MoveNext()
                    If Not RsTemp.EOF Then
                        cntRow = cntRow + 1
                        .MaxRows = cntRow
                    End If
                Loop

                ColTotal(sprdBack, ColEPFAc, ColPFTotal)
                .Col = ColMonth
                .Row = .MaxRows
                .Text = "TOTAL :"


                MainClass.ProtectCell(sprdBack, 0, .MaxRows, 0, .MaxCols)

            End With
        End If
    End Sub

    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1

        With sprdAttn

            .MaxCols = ColCompanyCode
            .Row = mRow
            .Col = ColCodeNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColCodeNo, 5)
            .ColHidden = True

            .Col = ColAcctNo
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColAcctNo, 10)

            .Col = ColName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColName, 22)
            .ColsFrozen = ColName

            .Col = ColFName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColFName, 22)

            .Col = ColWages
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColWages, 15)

            .Col = ColEmpCont
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColEmpCont, 11)

            .Col = ColEPF
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColEPF, 11)

            .Col = ColPFund
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColPFund, 11)

            .Col = ColRefund
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColRefund, 11)

            .Col = ColRate
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColRate, 11)

            .Col = ColRemarks
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColRemarks, 11)

            .Col = ColCompanyCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColCompanyCode, 11)

        End With

        MainClass.ProtectCell(sprdAttn, 1, sprdAttn.MaxRows, 1, sprdAttn.MaxCols)
        '    MainClass.SetSpreadColor sprdAttn, mRow
        '    sprdAttn.OperationMode = OperationModeNormal '' = OperationModeRead
        sprdAttn.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' OperationModeSingle
        sprdAttn.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
        MainClass.SetSpreadColor(sprdAttn, mRow)

        With sprdBack
            .MaxCols = ColPFTotal
            .Row = mRow
            .Col = ColMonth
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColMonth, 14)
            .ColsFrozen = ColMonth

            .Col = ColEPFAc
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColEPFAc, 15)

            .Col = ColPF
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColPF, 11)

            .Col = ColDLI
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColDLI, 11)

            .Col = ColADM
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColADM, 11)

            .Col = ColEDLI
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColEDLI, 11)

            .Col = ColPFTotal
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
            .set_ColWidth(ColPFTotal, 11)
        End With

        MainClass.ProtectCell(sprdAttn, 1, sprdBack.MaxRows, 1, sprdBack.MaxCols)
        '    MainClass.SetSpreadColor sprdBack, mRow
        '    sprdBack.OperationMode = OperationModeNormal    '' = OperationModeRead
        sprdBack.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' OperationModeSingle
        sprdBack.SetOddEvenRowColor(&HC0FFFF, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black), &HFFFFC0, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
        MainClass.SetSpreadColor(sprdBack, mRow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub

    Private Sub frmEPFForm6_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        On Error GoTo ErrPart
        Dim mReFormWidth As Integer

        mReFormWidth = VB6.PixelsToTwipsX(Me.Width)

        sprdAttn.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 180, mReFormWidth - 180, mReFormWidth))

        Frame1.Width = VB6.TwipsToPixelsX(IIf(mReFormWidth > 120, mReFormWidth, mReFormWidth))
        CurrFormWidth = mReFormWidth

        '    MainClass.SetSpreadColor SprdMain, -1
        '    MainClass.SetSpreadColor SprdOption, -1
        Exit Sub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub txtFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Not IsDate(txtFrom.Text) Then
            MsgInformation("Please enter the vaild date.")
            Cancel = True
            GoTo EventExitSub
        End If
        txtFrom.Text = VB6.Format(txtFrom.Text, "dd/mm/yyyy")
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Not IsDate(txtTo.Text) Then
            MsgInformation("Please enter the vaild date.")
            Cancel = True
            GoTo EventExitSub
        End If
        txtTo.Text = VB6.Format(txtTo.Text, "dd/mm/yyyy")
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Function GetVPFRate(ByRef mTPFRate As Double) As Object

        Dim RsCeiling As ADODB.Recordset
        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mSqlStr As String
        mSqlStr = " SELECT MAX(WEF) FROM PAY_PFESICeiling_MST WHERE " & vbCrLf & " COMPANY_CODE= " & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf

        mSqlStr = mSqlStr & " CODE=" & ConPF & " AND WEF<=TO_DATE('" & VB6.Format(txtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        SqlStr = " SELECT RATE FROM PAY_PFESICeiling_MST WHERE " & vbCrLf & " COMPANY_CODE= " & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " CODE=" & ConPF & " AND WEF=(" & mSqlStr & ") "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsCeiling, ADODB.LockTypeEnum.adLockOptimistic)
        If RsCeiling.EOF = False Then
            GetVPFRate = mTPFRate - IIf(IsDbNull(RsCeiling.Fields("Rate").Value), 0, RsCeiling.Fields("Rate").Value)
        Else
            GetVPFRate = 0
        End If
        Exit Function
ERR1:
        MsgBox(Err.Description)
    End Function
End Class
