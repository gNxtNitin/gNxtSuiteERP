Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmITChallan
    Inherits System.Windows.Forms.Form
    Dim RsChallanMain As ADODB.Recordset
    Dim RsChallanDetail As ADODB.Recordset

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection

    Dim Shw As Boolean
    Dim SqlStr As String = ""
    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12

    Private Const ColEmpCode As Short = 1
    Private Const ColEmpName As Short = 2
    Private Const ColAmtPaid As Short = 3
    Private Const ColAmt As Short = 4
    Private Const ColCessAmt As Short = 5
    Private Const ColSurcharge As Short = 6
    Private Const ColTDSAmount As Short = 7

    Private Sub ViewGrid()

        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            MainClass.ClearGrid(SprdView)
            AssignGrid(True)
            '        ADataMain.Refresh
            FormatSprdView()
            SprdView.Refresh()

            SprdView.Focus()
            SprdView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            SprdView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsChallanMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub Clear1()

        txtVNo.Text = ""
        txtLastVNo.Text = ""
        txtVDate.Text = VB6.Format(RunDate, "DD/MM/YYYY")
        txtChallanNo.Text = ""
        txtChallanDate.Text = ""
        '    txtBankName.Text = ""
        '    txtBankCode.Text = ""

        If RsCompany.Fields("COMPANY_CODE").Value = 16 Then
            txtBankName.Text = "AXIS BANK LTD, SECTOR -14 NEAR HUDA OFFICE, GURGAON"
            txtBankCode.Text = "6360057"
        Else
            txtBankName.Text = "AXIS BANK LTD, GARIA BRANCH, KOLKATA"
            txtBankCode.Text = "6360218"
        End If

        txtChqNo.Text = ""
        txtChqDate.Text = ""
        SprdMain.Text = ""
        txtTDSAmount.Text = ""
        txtSurcharge.Text = ""
        txtCess.Text = ""
        txtInterest.Text = ""
        txtOthers.Text = ""
        txtNetAmount.Text = ""
        lblMKey.Text = ""
        SprdMain.Enabled = True
        MainClass.ClearGrid(SprdMain)

        cmdShow.Enabled = True
        If lblBookType.Text = "C" Then
            txtLastVNo.Enabled = True
            txtLastVNo.Visible = True
        Else
            txtLastVNo.Enabled = False
            txtLastVNo.Visible = True
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsChallanMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        cmdSavePrint.Enabled = True
    End Sub
    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdModify.Click

        If cmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsChallanMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            SprdMain.Enabled = True
            txtVNo.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            Call Show1()
            txtVNo.Enabled = True
        End If
    End Sub



    Private Sub cmdReset_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdReset.Click
        CalcTotal()
    End Sub

    Private Sub cmdResetAmountPaid_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdResetAmountPaid.Click

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntRow As Integer
        Dim mEmpCode As String
        Dim mAmountPaid As Double
        Dim mDOJ As String
        Dim mDOL As String
        Dim mMonthCount As Integer
        Dim mTotChallanPaidAmount As Double

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColEmpCode
                mEmpCode = Trim(.Text)
                If mEmpCode <> "" Then
                    mAmountPaid = 0
                    SqlStr = " SELECT IH.TOTALAMOUNT, EMP.EMP_DOJ, EMP.EMP_LEAVE_DATE" & vbCrLf & " FROM PAY_ITCOMP_TRN IH, PAY_EMPLOYEE_MST EMP " & vbCrLf & " WHERE IH.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR = " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND IH.COMPANY_CODE = EMP.COMPANY_CODE " & vbCrLf & " AND IH.EMP_CODE = EMP.EMP_CODE "

                    If RsCompany.Fields("FYEAR").Value >= 2018 Then
                        SqlStr = SqlStr & vbCrLf & " AND IH.SUBROWNO=67"
                    Else
                        SqlStr = SqlStr & vbCrLf & " AND IH.SUBROWNO=65"
                    End If

                    SqlStr = SqlStr & vbCrLf & " AND IH.EMP_CODE='" & mEmpCode & "'"

                    SqlStr = SqlStr & vbCrLf & " AND IH.EMP_CODE IN ( " & vbCrLf & " SELECT EMP_CODE" & vbCrLf & " FROM PAY_ITCOMP_TRN" & vbCrLf & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR = " & RsCompany.Fields("FYEAR").Value & " AND EMP_CODE='" & mEmpCode & "'" & vbCrLf & " AND SUBROWNO=CASE WHEN FYEAR >=2018 THEN 75 ELSE 71 END " & vbCrLf & " AND TOTALAMOUNT>0)"

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
                    If RsTemp.EOF = False Then

                        mAmountPaid = CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("TotalAmount").Value), 0, RsTemp.Fields("TotalAmount").Value), "0.00"))

                        If VB6.Format(txtVDate.Text, "MM") = "03" Then
                            mTotChallanPaidAmount = GetNetChallanPaidAmount(mEmpCode)
                            mAmountPaid = mAmountPaid - mTotChallanPaidAmount
                            mAmountPaid = IIf(mAmountPaid < 0, 0, mAmountPaid)
                        Else
                            mDOL = IIf(IsDbNull(RsTemp.Fields("EMP_LEAVE_DATE").Value) Or RsTemp.Fields("EMP_LEAVE_DATE").Value = "", DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, RsCompany.Fields("END_DATE").Value), RsTemp.Fields("EMP_LEAVE_DATE").Value)
                            mDOJ = IIf(IsDbNull(RsTemp.Fields("EMP_DOJ").Value), RsCompany.Fields("START_DATE").Value, RsTemp.Fields("EMP_DOJ").Value)

                            If CDate(mDOJ) < CDate(RsCompany.Fields("START_DATE").Value) And CDate(mDOL) > CDate(RsCompany.Fields("END_DATE").Value) Then
                                mMonthCount = 12
                            ElseIf CDate(mDOJ) < CDate(RsCompany.Fields("START_DATE").Value) And CDate(mDOL) <= CDate(RsCompany.Fields("END_DATE").Value) Then
                                mMonthCount = DateDiff(Microsoft.VisualBasic.DateInterval.Month, RsCompany.Fields("START_DATE").Value, CDate(mDOL))
                            ElseIf CDate(mDOJ) >= CDate(RsCompany.Fields("START_DATE").Value) And CDate(mDOL) <= CDate(RsCompany.Fields("END_DATE").Value) Then
                                mMonthCount = DateDiff(Microsoft.VisualBasic.DateInterval.Month, CDate(mDOJ), CDate(mDOL))
                            ElseIf CDate(mDOJ) >= CDate(RsCompany.Fields("START_DATE").Value) And CDate(mDOL) > CDate(RsCompany.Fields("END_DATE").Value) Then
                                mMonthCount = DateDiff(Microsoft.VisualBasic.DateInterval.Month, CDate(mDOJ), RsCompany.Fields("END_DATE").Value)
                            End If


                            If mMonthCount > 0 Then
                                mAmountPaid = mAmountPaid / mMonthCount
                            End If
                        End If

                        mAmountPaid = System.Math.Round(mAmountPaid, 0)
                        .Row = cntRow
                        .Col = ColAmtPaid
                        .Text = VB6.Format(mAmountPaid, "0.00")
                    End If
                End If
            Next
        End With

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
LedgError:
        'Resume
        MsgInformation(Err.Description)
    End Sub

    Private Function GetNetChallanPaidAmount(ByRef mEmpCode As String) As Double

        On Error GoTo refreshErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""
        Dim mDate As String

        GetNetChallanPaidAmount = 0
        mDate = "01/03/" & VB6.Format(RsCompany.Fields("END_DATE").Value, "YYYY")

        SqlStr = " SELECT  SUM(AMOUNT_PAID) AS AMT_PAID " & vbCrLf & " From PAY_ITChallan_HDR IH, PAY_ITChallan_DET ID" & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND IH.AUTO_KEY_REFNO=ID.AUTO_KEY_REFNO " & vbCrLf & " AND ID.EMP_CODE='" & MainClass.AllowSingleQuote(Trim(mEmpCode)) & "'" & vbCrLf & " AND IH.VDATE<TO_DATE('" & VB6.Format(mDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


        If RsTemp.EOF = False Then
            GetNetChallanPaidAmount = IIf(IsDbNull(RsTemp.Fields("AMT_PAID").Value), 0, RsTemp.Fields("AMT_PAID").Value)
        End If
        Exit Function
refreshErrPart:
        'Resume
        MsgBox(Err.Description)
    End Function
    Private Sub cmdShow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdShow.Click

        MainClass.ClearGrid(SprdMain)
        ShowFromSalary()
        '    FormatSprdMain
        '    Call ReFormatSprdMain
        '    SprdMain.SetFocus
        '    MainClass.SetFocusToCell SprdMain, 1, 4
    End Sub

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdAdd.Click
        If cmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            If txtVNo.Enabled = True Then txtVNo.Focus()
            txtVNo.Enabled = False
            SprdMain.Enabled = True
        Else
            ADDMode = False
            MODIFYMode = False
            If RsChallanMain.EOF = False Then RsChallanMain.MoveFirst()
            Show1()
            txtVNo.Enabled = True
        End If
    End Sub
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.hide()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDelete.Click
        On Error GoTo DelErrPart
        If txtVNo.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub
        If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then
            If Delete1 = False Then GoTo DelErrPart
        End If

        Exit Sub
DelErrPart:
        MsgBox("Record Not Deleted")
    End Sub
    Private Sub frmITChallan_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub
    Private Sub SprdMain_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles SprdMain.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdMain_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles SprdMain.ClickEvent

        Dim xEmpCode As String
        Dim SqlStr As String = ""


        If eventArgs.row = 0 And eventArgs.col = ColEmpCode Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColEmpCode
                If MainClass.SearchGridMaster(.Text, "PAY_EMPLOYEE_MST", "EMP_CODE", "EMP_NAME", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow
                    .Col = ColEmpCode
                    .Text = AcName

                    .Col = ColEmpName
                    .Text = AcName1

                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColEmpCode)
            End With
        End If

        If eventArgs.row = 0 And eventArgs.col = ColEmpName Then
            With SprdMain
                .Row = .ActiveRow
                .Col = ColEmpCode
                If MainClass.SearchGridMaster(.Text, "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    .Row = .ActiveRow
                    .Col = ColEmpName
                    .Text = AcName
                    .Col = ColEmpCode
                    .Text = AcName1
                End If
                MainClass.SetFocusToCell(SprdMain, SprdMain.ActiveRow, ColEmpCode)
            End With
        End If

        If eventArgs.col = 0 And eventArgs.row > 0 Then
            SprdMain.Row = eventArgs.row
            SprdMain.Col = ColEmpCode
            If eventArgs.row < SprdMain.MaxRows Then
                MainClass.DeleteSprdRow(SprdMain, eventArgs.row, ColEmpCode)
                MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
                FormatSprd(eventArgs.row)
            End If
        End If

        Call CalcTotal()
    End Sub
    Private Sub frmITChallan_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        SqlStr = ""
        If FormActive = True Then Exit Sub

        SqlStr = "Select * From PAY_ITChallan_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsChallanMain, ADODB.LockTypeEnum.adLockOptimistic)

        SqlStr = "Select * From PAY_ITChallan_DET Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsChallanDetail, ADODB.LockTypeEnum.adLockOptimistic)

        AssignGrid(False)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        settextlength()
        Clear1()
        FormActive = True
        If cmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '' Resume
    End Sub
    Private Sub frmITChallan_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Height = VB6.TwipsToPixelsY(6720)
        Me.Width = VB6.TwipsToPixelsX(9300)
        Me.Left = 0
        Me.Top = 0

        FormatSprd(-1)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub
    Private Sub frmITChallan_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        'PvtDBCn.Cancel
        'PvtDBCn.Close
        RsChallanMain = Nothing
        'Set PvtDBCn = Nothing
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim cntRow As Integer

        MainClass.ClearGrid(SprdMain)

        If RsChallanMain.EOF = False Then
            With RsChallanMain

                txtVNo.Text = IIf(IsDbNull(RsChallanMain.Fields("AUTO_KEY_REFNO").Value), "", RsChallanMain.Fields("AUTO_KEY_REFNO").Value)
                txtVDate.Text = VB6.Format(IIf(IsDbNull(RsChallanMain.Fields("VDATE").Value), "", RsChallanMain.Fields("VDATE").Value), "DD/MM/YYYY")
                txtChallanNo.Text = IIf(IsDbNull(RsChallanMain.Fields("CHALLANNO").Value), "", RsChallanMain.Fields("CHALLANNO").Value)
                txtChallanDate.Text = VB6.Format(IIf(IsDbNull(RsChallanMain.Fields("CHALLANDATE").Value), "", RsChallanMain.Fields("CHALLANDATE").Value), "DD/MM/YYYY")
                txtBankName.Text = IIf(IsDbNull(RsChallanMain.Fields("BANKNAME").Value), "", RsChallanMain.Fields("BANKNAME").Value)
                txtBankCode.Text = IIf(IsDbNull(RsChallanMain.Fields("BSRCODE").Value), "", RsChallanMain.Fields("BSRCODE").Value)
                txtChqNo.Text = IIf(IsDbNull(RsChallanMain.Fields("CHQ_NO").Value), "", RsChallanMain.Fields("CHQ_NO").Value)
                txtChqDate.Text = VB6.Format(IIf(IsDbNull(RsChallanMain.Fields("CHQ_DATE").Value), "", RsChallanMain.Fields("CHQ_DATE").Value), "DD/MM/YYYY")
                txtTDSAmount.Text = VB6.Format(IIf(IsDbNull(RsChallanMain.Fields("TDS_AMOUNT").Value), 0, RsChallanMain.Fields("TDS_AMOUNT").Value), "0.00")
                txtSurcharge.Text = VB6.Format(IIf(IsDbNull(RsChallanMain.Fields("SURCHARGE").Value), 0, RsChallanMain.Fields("SURCHARGE").Value), "0.00")
                txtCess.Text = VB6.Format(IIf(IsDbNull(RsChallanMain.Fields("EDU_CESS").Value), 0, RsChallanMain.Fields("EDU_CESS").Value), "0.00")
                txtInterest.Text = VB6.Format(IIf(IsDbNull(RsChallanMain.Fields("INTEREST_AMOUNT").Value), 0, RsChallanMain.Fields("INTEREST_AMOUNT").Value), "0.00")
                txtOthers.Text = VB6.Format(IIf(IsDbNull(RsChallanMain.Fields("OTHER_AMOUNT").Value), 0, RsChallanMain.Fields("OTHER_AMOUNT").Value), "0.00")
                txtNetAmount.Text = VB6.Format(IIf(IsDbNull(RsChallanMain.Fields("NETAMOUNT").Value), 0, RsChallanMain.Fields("NETAMOUNT").Value), "0.00")
                lblMKey.Text = IIf(IsDbNull(RsChallanMain.Fields("AUTO_KEY_REFNO").Value), "", RsChallanMain.Fields("AUTO_KEY_REFNO").Value)
                txtLastVNo.Text = IIf(IsDbNull(RsChallanMain.Fields("LAST_VNO").Value), "", RsChallanMain.Fields("LAST_VNO").Value)
                Call ShowDetail1(CDbl(lblMKey.Text))
                cmdShow.Enabled = False
            End With
        End If
        Shw = True

        Shw = False
        ADDMode = False
        MODIFYMode = False
        SprdMain.Enabled = True    '' False Sandeep 15/05/2022
        '    CalcTotal
        MainClass.ButtonStatus(Me, XRIGHT, RsChallanMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
        'Resume
    End Sub
    Private Sub ShowFromSalary()

        On Error GoTo LedgError
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim cntRow As Integer
        Dim mPayableAmount As Double
        Dim mAmountPaid As Double
        Dim mDOJ As String
        Dim mDOL As String
        Dim mMonthCount As Integer
        Dim mEmpCode As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        cntRow = 1
        SqlStr = MakeSQL()
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        With RsTemp
            If RsTemp.EOF = False Then
                Do While RsTemp.EOF = False

                    mDOL = IIf(IsDbNull(RsTemp.Fields("EMP_LEAVE_DATE").Value) Or RsTemp.Fields("EMP_LEAVE_DATE").Value = "", DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, RsCompany.Fields("END_DATE").Value), RsTemp.Fields("EMP_LEAVE_DATE").Value)
                    mDOJ = IIf(IsDbNull(RsTemp.Fields("EMP_DOJ").Value), RsCompany.Fields("START_DATE").Value, RsTemp.Fields("EMP_DOJ").Value)
                    mEmpCode = IIf(IsDbNull(.Fields("EMP_CODE").Value), "", .Fields("EMP_CODE").Value)

                    If CDate(mDOJ) < CDate(RsCompany.Fields("START_DATE").Value) And CDate(mDOL) > CDate(RsCompany.Fields("END_DATE").Value) Then
                        mMonthCount = 12
                    ElseIf CDate(mDOJ) < CDate(RsCompany.Fields("START_DATE").Value) And CDate(mDOL) <= CDate(RsCompany.Fields("END_DATE").Value) Then
                        mMonthCount = DateDiff(Microsoft.VisualBasic.DateInterval.Month, RsCompany.Fields("START_DATE").Value, CDate(mDOL))
                    ElseIf CDate(mDOJ) >= CDate(RsCompany.Fields("START_DATE").Value) And CDate(mDOL) <= CDate(RsCompany.Fields("END_DATE").Value) Then
                        mMonthCount = DateDiff(Microsoft.VisualBasic.DateInterval.Month, CDate(mDOJ), CDate(mDOL))
                    ElseIf CDate(mDOJ) >= CDate(RsCompany.Fields("START_DATE").Value) And CDate(mDOL) > CDate(RsCompany.Fields("END_DATE").Value) Then
                        mMonthCount = DateDiff(Microsoft.VisualBasic.DateInterval.Month, CDate(mDOJ), RsCompany.Fields("END_DATE").Value)
                    End If

                    mAmountPaid = GetAmountPaid(mEmpCode) '' Format(IIf(IsNull(!TotalAmount), 0, !TotalAmount), "0.00")
                    If mMonthCount > 0 Then
                        mAmountPaid = mAmountPaid / mMonthCount
                    End If

                    mAmountPaid = System.Math.Round(mAmountPaid, 0)

                    SprdMain.Row = cntRow
                    SprdMain.Col = ColEmpCode
                    SprdMain.Text = IIf(IsDbNull(.Fields("EMP_CODE").Value), "", .Fields("EMP_CODE").Value)

                    SprdMain.Col = ColEmpName
                    SprdMain.Text = IIf(IsDbNull(.Fields("EMP_NAME").Value), "", .Fields("EMP_NAME").Value)

                    SprdMain.Col = ColAmtPaid
                    SprdMain.Text = VB6.Format(mAmountPaid, "0.00")

                    mPayableAmount = GetTaxPayableAmount(.Fields("EMP_CODE").Value)

                    SprdMain.Col = ColAmt
                    SprdMain.Text = VB6.Format(mPayableAmount, "0.00")

                    SprdMain.MaxRows = SprdMain.MaxRows + 1
                    cntRow = cntRow + 1

                    RsTemp.MoveNext()
                Loop
            End If
        End With

        '    Call FormatSprdMain
        CalcTotal()

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
LedgError:
        MsgInformation(Err.Description)

    End Sub
    Private Function GetTaxPayableAmount(ByRef mEmpCode As String) As Double

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        GetTaxPayableAmount = 0
        SqlStr = " SELECT IH.PAYABLEAMOUNT" & vbCrLf & " FROM PAY_SAL_TRN IH, PAY_SALARYHEAD_MST SMST " & vbCrLf & " WHERE IH.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE = SMST.COMPANY_CODE " & vbCrLf & " AND IH.SALHEADCODE = SMST.CODE " & vbCrLf & " AND IH.EMP_CODE = '" & MainClass.AllowSingleQuote(mEmpCode) & "'"

        SqlStr = SqlStr & vbCrLf & " AND SMST.TYPE=" & ConIncomeTax & ""

        SqlStr = SqlStr & vbCrLf & " AND IH.PAYABLEAMOUNT>0"
        SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(IH.SAL_DATE,'YYYYMM')='" & VB6.Format(txtVDate.Text, "YYYYMM") & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)


        If RsTemp.EOF = False Then
            GetTaxPayableAmount = IIf(IsDbNull(RsTemp.Fields("PayableAmount").Value), 0, RsTemp.Fields("PayableAmount").Value)
        End If

        Exit Function
ERR1:
        MsgInformation(Err.Description)

    End Function
    Private Function GetAmountPaid(ByRef mEmpCode As String) As Double

        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        GetAmountPaid = 0
        SqlStr = " SELECT IH.TOTALAMOUNT" & vbCrLf & " FROM PAY_ITCOMP_TRN IH " & vbCrLf & " WHERE IH.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.FYEAR = " & RsCompany.Fields("FYEAR").Value & ""

        SqlStr = SqlStr & vbCrLf & " AND IH.EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'"
        SqlStr = SqlStr & vbCrLf & " AND IH.SUBROWNO=65"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            GetAmountPaid = System.Math.Round(IIf(IsDbNull(RsTemp.Fields("TotalAmount").Value), 0, RsTemp.Fields("TotalAmount").Value), 0)
        End If
        '
        '    SqlStr = SqlStr & vbCrLf & " AND IH.EMP_CODE IN ( " & vbCrLf _
        ''            & " SELECT EMP_CODE" & vbCrLf _
        ''            & " FROM PAY_ITCOMP_TRN" & vbCrLf _
        ''            & " WHERE COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''            & " AND FYEAR = " & RsCompany.Fields("FYEAR").Value & "" & vbCrLf _
        ''            & " AND SUBROWNO=71) " ''& vbCrLf _
        ''            & " AND TOTALAMOUNT>0)"

        '    SqlStr = SqlStr & vbCrLf & " ORDER BY IH.EMP_CODE"
        '
        '    MakeSQL = SqlStr
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        '    MakeSQL = ""
    End Function
    Private Function MakeSQL() As String
        On Error GoTo ERR1
        Dim SqlStr As String = ""

        SqlStr = " SELECT IH.EMP_CODE, EMP.EMP_NAME, EMP.EMP_DOJ, EMP.EMP_LEAVE_DATE" & vbCrLf & " FROM PAY_SAL_TRN IH, PAY_SALARYHEAD_MST SMST, PAY_EMPLOYEE_MST EMP " & vbCrLf & " WHERE IH.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND IH.COMPANY_CODE = SMST.COMPANY_CODE " & vbCrLf & " AND IH.SALHEADCODE = SMST.CODE " & vbCrLf & " AND IH.COMPANY_CODE = EMP.COMPANY_CODE " & vbCrLf & " AND IH.EMP_CODE = EMP.EMP_CODE "

        SqlStr = SqlStr & vbCrLf & " AND SMST.TYPE=" & ConIncomeTax & ""

        SqlStr = SqlStr & vbCrLf & " AND IH.PAYABLEAMOUNT>0"
        SqlStr = SqlStr & vbCrLf & " AND TO_CHAR(IH.SAL_DATE,'YYYYMM')='" & VB6.Format(txtVDate.Text, "YYYYMM") & "' "

        SqlStr = SqlStr & vbCrLf & " ORDER BY IH.EMP_CODE"

        MakeSQL = SqlStr
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        MakeSQL = ""
    End Function

    Private Sub ShowDetail1(ByRef xMKey As Double)

        On Error GoTo ShowErrPart
        Dim cntRow As Integer
        Dim mEmpName As String

        SqlStr = " SELECT * " & vbCrLf & " FROM PAY_ITChallan_DET WHERE " & vbCrLf & " AUTO_KEY_REFNO=" & xMKey & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsChallanDetail, ADODB.LockTypeEnum.adLockReadOnly)

        If RsChallanDetail.EOF = False Then
            With RsChallanDetail
                cntRow = 1
                Do While Not RsChallanDetail.EOF
                    SprdMain.Row = cntRow
                    SprdMain.Col = ColEmpCode
                    SprdMain.Text = IIf(IsDbNull(.Fields("EMP_CODE").Value), "", .Fields("EMP_CODE").Value)

                    If MainClass.ValidateWithMasterTable(.Fields("EMP_CODE"), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                        mEmpName = MasterNo
                    Else
                        mEmpName = ""
                    End If

                    SprdMain.Col = ColEmpName
                    SprdMain.Text = mEmpName

                    SprdMain.Col = ColAmtPaid
                    SprdMain.Text = VB6.Format(IIf(.Fields("AMOUNT_PAID").Value = 0, "", .Fields("AMOUNT_PAID").Value), "0.00")

                    SprdMain.Col = ColAmt
                    SprdMain.Text = VB6.Format(IIf(.Fields("Amount").Value = 0, "", .Fields("Amount").Value), "0.00")

                    SprdMain.Col = ColCessAmt
                    SprdMain.Text = VB6.Format(IIf(.Fields("CESS_AMT").Value = 0, "", .Fields("CESS_AMT").Value), "0.00") '' - 1

                    SprdMain.Col = ColSurcharge
                    SprdMain.Text = VB6.Format(IIf(.Fields("SURCHARGE_AMT").Value = 0, "", .Fields("SURCHARGE_AMT").Value), "0.00")

                    SprdMain.Col = ColTDSAmount
                    SprdMain.Text = VB6.Format(IIf(.Fields("TDS_AMOUNT").Value = 0, "", .Fields("TDS_AMOUNT").Value), "0.00") '' + 1

                    RsChallanDetail.MoveNext()
                    SprdMain.MaxRows = SprdMain.MaxRows + 1
                    cntRow = cntRow + 1

                Loop
            End With
        End If
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
        'Resume
    End Sub

    Private Function CurrentDateChallanExists(ByRef mEmpCode As String) As Boolean

        On Error GoTo ShowErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mVNO As String
        Dim mVnoStr As String

        CurrentDateChallanExists = False
        SqlStr = " SELECT IH.AUTO_KEY_REFNO, IH.VDATE " & vbCrLf & " FROM PAY_ITChallan_HDR IH, PAY_ITChallan_DET ID " & vbCrLf & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND IH.AUTO_KEY_REFNO=ID.AUTO_KEY_REFNO" & vbCrLf & " AND IH.VDATE=TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "

        SqlStr = SqlStr & vbCrLf & " AND ID.EMP_CODE='" & MainClass.AllowSingleQuote(mEmpCode) & "'"

        If Val(txtVNo.Text) > 0 Then
            SqlStr = SqlStr & vbCrLf & " AND IH.AUTO_KEY_REFNO<>" & Val(txtVNo.Text) & ""
        End If

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                mVNO = IIf(IsDbNull(RsTemp.Fields("AUTO_KEY_REFNO").Value), "", RsTemp.Fields("AUTO_KEY_REFNO").Value)

                mVnoStr = mVnoStr & IIf(mVnoStr = "", mVNO, mVnoStr & "," & mVNO)
                RsTemp.MoveNext()
            Loop

            MsgInformation("You already made Current Date Challan, Ref No is (" & mVnoStr & ")")
            CurrentDateChallanExists = True
        End If

        Exit Function
ShowErrPart:
        MsgBox(Err.Description)
        CurrentDateChallanExists = False
        'Resume
    End Function
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        If Update1 = True Then
            TxtVNo_Validating(TxtVNo, New System.ComponentModel.CancelEventArgs(False))
            If cmdAdd.Enabled = True Then cmdAdd.Focus()
        Else
            MsgInformation("Record not saved")
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    Private Function AutoGenRefNoSeq() As Double

        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset = Nothing
        Dim mAutoGen As Integer
        Dim SqlStr As String = ""

        mAutoGen = 1
        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_REFNO)  " & vbCrLf & " FROM PAY_ITChallan_HDR " & vbCrLf & " WHERE Company_Code=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_REFNO,LENGTH(AUTO_KEY_REFNO)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAutoGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsAutoGen
            If .EOF = False Then
                If Not IsDbNull(.Fields(0).Value) Then
                    mAutoGen = CInt(Mid(.Fields(0).Value, 1, Len(.Fields(0).Value) - 6))
                    mAutoGen = mAutoGen + 1
                Else
                    mAutoGen = 1
                End If
            End If
        End With

        AutoGenRefNoSeq = CDbl(mAutoGen & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00"))
        RsAutoGen.Close()
        RsAutoGen = Nothing
        Exit Function
AutogenErr:
        MsgBox(Err.Description)
    End Function

    Private Function Update1() As Boolean
        On Error GoTo UpdateError
        Dim mVNO As Double
        Dim mAYEAR As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        mVNO = Val(txtVNo.Text)
        If Val(txtVNo.Text) = 0 Then
            mVNO = AutoGenRefNoSeq()
        End If
        txtVNo.Text = CStr(mVNO)
        mAYEAR = Year(RsCompany.Fields("END_DATE").Value) & "-" & (Year(RsCompany.Fields("END_DATE").Value) + 1)

        If ADDMode = True Then
            lblMKey.Text = CStr(mVNO)

            SqlStr = "INSERT INTO PAY_ITChallan_HDR ( " & vbCrLf & " AUTO_KEY_REFNO, COMPANY_CODE, FYEAR, " & vbCrLf & " VDATE, BOOKTYPE, " & vbCrLf & " AYEAR, CHALLANNO, CHALLANDATE, " & vbCrLf & " CHQ_NO, CHQ_DATE, BANKNAME, " & vbCrLf & " BSRCODE, TDS_AMOUNT, SURCHARGE, " & vbCrLf & " EDU_CESS, INTEREST_AMOUNT, OTHER_AMOUNT, " & vbCrLf & " NETAMOUNT,  LAST_VNO," & vbCrLf & " ADDUSER, ADDDATE, " & vbCrLf & " MODUSER, MODDATE " & vbCrLf & " ) VALUES ( "

            SqlStr = SqlStr & vbCrLf & " " & Val(CStr(mVNO)) & ", " & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & lblBookType.Text & "', " & vbCrLf & " '" & mAYEAR & "', '" & MainClass.AllowSingleQuote((txtChallanNo.Text)) & "', TO_DATE('" & VB6.Format(txtChallanDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtChqNo.Text)) & "', TO_DATE('" & VB6.Format(txtChqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote((txtBankName.Text)) & "', " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtBankCode.Text)) & "', " & Val(txtTDSAmount.Text) & ", " & Val(txtSurcharge.Text) & "," & vbCrLf & " " & Val(txtCess.Text) & ", " & Val(txtInterest.Text) & ", " & Val(txtOthers.Text) & ", " & vbCrLf & " " & Val(txtNetAmount.Text) & ", " & Val(txtLastVNo.Text) & "," & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '','')"
        Else

            SqlStr = "UPDATE PAY_ITChallan_HDR SET " & vbCrLf & " VDATE=TO_DATE('" & VB6.Format(txtVDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " CHALLANNO='" & MainClass.AllowSingleQuote((txtChallanNo.Text)) & "', " & vbCrLf & " CHALLANDATE=TO_DATE('" & VB6.Format(txtChallanDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " CHQ_NO='" & MainClass.AllowSingleQuote((txtChqNo.Text)) & "',   " & vbCrLf & " CHQ_DATE=TO_DATE('" & VB6.Format(txtChqDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),   " & vbCrLf & " BANKNAME='" & MainClass.AllowSingleQuote((txtBankName.Text)) & "', " & vbCrLf & " BSRCODE='" & MainClass.AllowSingleQuote((txtBankCode.Text)) & "', " & vbCrLf & " TDS_AMOUNT=" & Val(txtTDSAmount.Text) & ", " & vbCrLf & " SURCHARGE=" & Val(txtSurcharge.Text) & ", " & vbCrLf & " EDU_CESS=" & Val(txtCess.Text) & ", " & vbCrLf & " INTEREST_AMOUNT=" & Val(txtInterest.Text) & ", " & vbCrLf & " OTHER_AMOUNT=" & Val(txtOthers.Text) & ", " & vbCrLf & " NETAMOUNT=" & Val(txtNetAmount.Text) & ", " & vbCrLf & " LAST_VNO=" & Val(txtLastVNo.Text) & "," & vbCrLf & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " WHERE AUTO_KEY_REFNO= " & Val(lblMKey.Text) & ""

        End If

        PubDBCn.Execute(SqlStr)

        If UpdateDetail(Val(lblMKey.Text)) = False Then GoTo UpdateError
        PubDBCn.CommitTrans()
        RsChallanMain.Requery()
        Update1 = True
        Exit Function
UpdateError:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsChallanMain.Requery()
        If Err.Number = -2147467259 Then
            MsgBox("Can't Modify Transaction Exists Against this Code")
            Exit Function
        End If
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        PubDBCn.Errors.Clear()
        'Resume
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Private Function UpdateDetail(ByRef pMKey As Object) As Boolean

        On Error GoTo UpdateError
        Dim mCode As String
        Dim cntRow As Integer
        Dim mEmpCode As String
        Dim mAmount As Double
        Dim mCESSAmount As Double
        Dim mSurchargeAmount As Double
        Dim mTDSAmount As Double
        Dim mAmountPaid As Double

        SqlStr = "DELETE FROM PAY_ITChallan_DET WHERE " & vbCrLf & " AUTO_KEY_REFNO=" & Val(lblMKey.Text) & ""

        PubDBCn.Execute(SqlStr)

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColEmpCode
                If MainClass.ValidateWithMasterTable(.Text, "EMP_CODE", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    mEmpCode = MainClass.AllowSingleQuote(.Text)
                Else
                    mEmpCode = ""
                End If

                .Col = ColAmtPaid
                mAmountPaid = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColAmt
                mAmount = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColCessAmt
                mCESSAmount = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColSurcharge
                mSurchargeAmount = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColTDSAmount
                mTDSAmount = IIf(IsNumeric(.Text), .Text, 0)


                If mEmpCode <> "" And mAmount <> 0 Then
                    SqlStr = " INSERT INTO PAY_ITChallan_DET ( " & vbCrLf & " AUTO_KEY_REFNO, COMPANY_CODE , " & vbCrLf & " EMP_CODE, AMOUNT_PAID, AMOUNT, CESS_AMT, SURCHARGE_AMT, TDS_AMOUNT )  VALUES ( " & vbCrLf & " " & pMKey & ", " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " '" & Trim(mEmpCode) & "'," & mAmountPaid & ",  " & mAmount & ", " & vbCrLf & " " & mCESSAmount & ", " & mSurchargeAmount & ", " & mTDSAmount & ")"

                    PubDBCn.Execute(SqlStr)
                End If
            Next
        End With

        UpdateDetail = True
        Exit Function
UpdateError:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateDetail = False
    End Function
    Private Function FieldsVarification() As Boolean
        On Error GoTo ERR1
        Dim xAmount As Decimal
        Dim mEmpCode As String
        Dim cntRow As Integer



        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
            Exit Function
        End If
        If MODIFYMode = True And (RsChallanMain.RecordCount = 0 Or RsChallanMain.EOF = True) Then Exit Function

        FieldsVarification = True

        If Not IsDate(txtVDate.Text) Then
            MsgInformation("Invaild Chq Date.")
            txtVDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtChallanNo.Text) = "" Then
            MsgInformation("Challan No is empty. Cannot Save")
            txtChallanNo.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Not IsDate(txtChallanDate.Text) Then
            MsgInformation("Invaild Chq Date.")
            txtChallanDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Not IsDate(txtChqDate.Text) Then
            MsgInformation("Invaild Chq Date.")
            txtChqDate.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtBankName.Text) = "" Then
            MsgInformation("Bank Name is empty. Cannot Save")
            txtBankName.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtBankCode.Text) = "" Then
            MsgInformation("Bank Code is empty. Cannot Save")
            txtBankCode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Len(txtBankCode.Text) <> 7 Then
            MsgInformation("Invalid Bank Code. Cannot Save")
            txtBankCode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        With SprdMain
            For cntRow = 1 To .MaxRows
                .Row = cntRow
                .Col = ColEmpCode
                mEmpCode = Trim(.Text)
                If CurrentDateChallanExists(mEmpCode) = True Then
                    FieldsVarification = False
                    Exit Function
                End If
            Next
        End With


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        FieldsVarification = False
        'Resume
    End Function
    Private Sub settextlength()
        On Error GoTo ERR1

        txtVNo.Maxlength = RsChallanMain.Fields("AUTO_KEY_REFNO").Precision
        txtLastVNo.Maxlength = RsChallanMain.Fields("LAST_VNO").Precision
        txtVDate.Maxlength = 10
        txtChallanNo.Maxlength = RsChallanMain.Fields("CHALLANNO").DefinedSize
        txtChallanDate.Maxlength = 10
        txtBankName.Maxlength = RsChallanMain.Fields("BANKNAME").DefinedSize
        txtBankCode.Maxlength = RsChallanMain.Fields("BSRCODE").DefinedSize
        txtChqNo.Maxlength = RsChallanMain.Fields("CHQ_NO").DefinedSize
        txtChqDate.Maxlength = 10

        txtTDSAmount.Maxlength = RsChallanMain.Fields("TDS_AMOUNT").Precision
        txtSurcharge.Maxlength = RsChallanMain.Fields("SURCHARGE").Precision
        txtCess.Maxlength = RsChallanMain.Fields("EDU_CESS").Precision
        txtInterest.Maxlength = RsChallanMain.Fields("INTEREST_AMOUNT").Precision
        txtOthers.Maxlength = RsChallanMain.Fields("OTHER_AMOUNT").Precision
        txtNetAmount.Maxlength = RsChallanMain.Fields("NETAMOUNT").Precision
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        '    Resume
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)


        SqlStr = " SELECT " & vbCrLf & " TO_CHAR(AUTO_KEY_REFNO) AS VNO, TO_CHAR(VDATE,'DD/MM/YYYY') As VDATE, " & vbCrLf & " CHALLANNO, TO_CHAR(CHALLANDATE,'DD/MM/YYYY') As CDATE, " & vbCrLf & " CHQ_NO, TO_CHAR(CHQ_DATE,'DD/MM/YYYY') As CHQ_DATE, " & vbCrLf & " BANKNAME , NETAMOUNT " & vbCrLf & " FROM PAY_ITChallan_HDR WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND BOOKTYPE='" & lblBookType.Text & "'"

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()

    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 12)
            .set_ColWidth(0, 5)
            .set_ColWidth(1, 8)
            .set_ColWidth(2, 8)
            .set_ColWidth(3, 8)
            .set_ColWidth(4, 8)
            .set_ColWidth(5, 8)
            .set_ColWidth(6, 8)
            .set_ColWidth(7, 8)
            .set_ColWidth(8, 8)
            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr

        SqlStr = ""
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = " Delete from PAY_ITChallan_DET WHERE " & vbCrLf & " AUTO_KEY_REFNO=" & Val(lblMKey.Text) & ""

        PubDBCn.Execute(SqlStr)

        SqlStr = " Delete from PAY_ITChallan_HDR WHERE " & vbCrLf & " AUTO_KEY_REFNO=" & Val(lblMKey.Text) & ""

        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()
        Clear1()
        RsChallanMain.Requery()
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans()
        RsChallanMain.Requery()
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete Transaction Exists Against This Employee.")
            Exit Function
        End If
        MsgBox(Err.Description)
    End Function
    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer

        With SprdMain
            .MaxCols = ColTDSAmount
            .Row = mRow
            .set_RowHeight(mRow, ConRowHeight)

            .Col = ColEmpCode
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColEmpCode, 5)

            .Col = ColEmpName
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .set_ColWidth(ColEmpName, 20)

            For cntCol = ColAmtPaid To ColTDSAmount
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("9999999.99")
                .TypeFloatMin = CDbl("-9999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntCol, 8.5)
            Next

        End With
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColEmpName, ColEmpName)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColCessAmt, ColCessAmt)
        MainClass.ProtectCell(SprdMain, 1, SprdMain.MaxRows, ColTDSAmount, ColTDSAmount)
        MainClass.SetSpreadColor(SprdMain, mRow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
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
        Dim All As Boolean
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        Dim PrintStatus As Boolean
        Dim mReportFileName As String

        PubDBCn.Errors.Clear()

        PrintStatus = True

        SqlStr = "DELETE FROM Temp_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = ""

        'Select Record for print...

        SqlStr = ""

        SqlStr = MainClass.FetchFromTempData(SqlStr, "SUBROW")

        mTitle = "T.D.S. / T.C.S. Challan"
        mSubTitle = ""

        mReportFileName = "TDSChallan.Rpt"

        Call ShowReport(SqlStr, mReportFileName, Mode, mTitle, mSubTitle)

        SqlStr = "DELETE FROM Temp_PrintDummyData NOLOGGING WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)

        Exit Sub
ERR1:
        If Err.Number = 32755 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        Else
            MsgInformation(Err.Description)
        End If

        'Resume
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)

        Dim mAYEAR As String
        Dim mTaxType As String
        Dim mCompanyTan As String
        Dim mCompanyPhone As String
        Dim mCompanyPin As String
        Dim mPaymentCode As String
        Dim mTotalInWords As String
        Dim mAmountStr As String
        Dim CompanyAdd As String

        Dim mAmount As String
        Dim mCroreStr As String
        Dim mLacsStr As String
        Dim mThousandStr As String
        Dim mHundredStr As String
        Dim mTenStr As String
        Dim mUnitStr As String

        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)


        CompanyAdd = IIf(IsDbNull(RsCompany.Fields("REGD_ADDR1").Value), "", RsCompany.Fields("REGD_ADDR1").Value)
        CompanyAdd = CompanyAdd & " " & IIf(IsDbNull(RsCompany.Fields("REGD_ADDR2").Value), "", RsCompany.Fields("REGD_ADDR2").Value)
        CompanyAdd = CompanyAdd & " " & IIf(IsDbNull(RsCompany.Fields("REGD_CITY").Value), "", RsCompany.Fields("REGD_CITY").Value)
        '    CompanyAdd = CompanyAdd & " " & IIf(IsNull(RsCompany!REGD_STATE), "", RsCompany!REGD_STATE)
        MainClass.AssignCRptFormulas(Report1, "CompanyAddress=""" & CompanyAdd & """")

        mAYEAR = Year(RsCompany.Fields("END_DATE").Value) & "-" & VB6.Format(CDbl(VB6.Format(RsCompany.Fields("END_DATE").Value, "YY")) + 1, "00")

        MainClass.AssignCRptFormulas(Report1, "AYear=""" & mAYEAR & """")

        mTaxType = "0020"
        MainClass.AssignCRptFormulas(Report1, "TaxType=""" & mTaxType & """")

        mCompanyTan = IIf(IsDbNull(RsCompany.Fields("TDSACNO").Value), "", RsCompany.Fields("TDSACNO").Value)
        MainClass.AssignCRptFormulas(Report1, "CompanyTan=""" & mCompanyTan & """")

        mCompanyPhone = "" ''IIf(IsNull(RsCompany!REGD_PHONE), "", RsCompany!REGD_PHONE)
        MainClass.AssignCRptFormulas(Report1, "CompanyPhone=""" & mCompanyPhone & """")

        mCompanyPin = IIf(IsDbNull(RsCompany.Fields("REGD_PIN").Value), "", RsCompany.Fields("REGD_PIN").Value)
        MainClass.AssignCRptFormulas(Report1, "CompanyPin=""" & mCompanyPin & """")
        MainClass.AssignCRptFormulas(Report1, "PaymentCode=""" & mPaymentCode & """")

        mAmountStr = New String(" ", 12 - Len(VB6.Format(txtTDSAmount.Text, "0"))) & VB6.Format(txtTDSAmount.Text, "0")
        MainClass.AssignCRptFormulas(Report1, "IncomeTax=""" & mAmountStr & """")

        mAmountStr = New String(" ", 12 - Len(VB6.Format(txtSurcharge.Text, "0"))) & VB6.Format(txtSurcharge.Text, "0")
        MainClass.AssignCRptFormulas(Report1, "Surcharge=""" & mAmountStr & """")

        mAmountStr = New String(" ", 12 - Len(VB6.Format(txtCess.Text, "0"))) & VB6.Format(txtCess.Text, "0")
        MainClass.AssignCRptFormulas(Report1, "EduCess=""" & mAmountStr & """")

        mAmountStr = New String(" ", 12 - Len(VB6.Format(txtInterest.Text, "0"))) & VB6.Format(txtInterest.Text, "0")
        MainClass.AssignCRptFormulas(Report1, "Interest=""" & mAmountStr & """")

        mAmountStr = New String(" ", 12 - Len(VB6.Format(txtOthers.Text, "0"))) & VB6.Format(txtOthers.Text, "0")
        MainClass.AssignCRptFormulas(Report1, "Penalty=""" & mAmountStr & """")

        mAmountStr = New String(" ", 12 - Len(VB6.Format(txtNetAmount.Text, "0"))) & VB6.Format(txtNetAmount.Text, "0")
        MainClass.AssignCRptFormulas(Report1, "Total=""" & mAmountStr & """")

        mTotalInWords = MainClass.RupeesConversion(txtNetAmount.Text)

        MainClass.AssignCRptFormulas(Report1, "TotalInWords=""" & mTotalInWords & """")
        MainClass.AssignCRptFormulas(Report1, "ChequeNo=""" & Trim(txtChqNo.Text) & """")
        MainClass.AssignCRptFormulas(Report1, "ChequeDate=""" & Trim(txtChqDate.Text) & """")
        MainClass.AssignCRptFormulas(Report1, "BankName=""" & Trim(txtBankName.Text) & """")

        mAmount = New String("0", 9 - Len(VB6.Format(txtNetAmount.Text, "0"))) & VB6.Format(txtNetAmount.Text, "0")
        mAmountStr = VB.Left(mAmount, 2)
        mTotalInWords = MainClass.RupeesConversion(Val(mAmountStr))
        If Trim(mTotalInWords) = "" Then
            mTotalInWords = "Zero"
        Else
            mTotalInWords = Trim(Mid(mTotalInWords, 1, Len(mTotalInWords) - 5))
        End If
        mCroreStr = mTotalInWords

        mAmountStr = Mid(mAmount, 3, 2)
        mTotalInWords = MainClass.RupeesConversion(Val(mAmountStr))
        If Trim(mTotalInWords) = "" Then
            mTotalInWords = "Zero"
        Else
            mTotalInWords = Trim(Mid(mTotalInWords, 1, Len(mTotalInWords) - 5))
        End If
        mLacsStr = mTotalInWords


        mAmountStr = Mid(mAmount, 5, 2)
        mTotalInWords = MainClass.RupeesConversion(Val(mAmountStr))
        If Trim(mTotalInWords) = "" Then
            mTotalInWords = "Zero"
        Else
            mTotalInWords = Trim(Mid(mTotalInWords, 1, Len(mTotalInWords) - 5))
        End If
        mThousandStr = mTotalInWords

        mAmountStr = Mid(mAmount, 7, 1)
        mTotalInWords = MainClass.RupeesConversion(Val(mAmountStr))
        If Trim(mTotalInWords) = "" Then
            mTotalInWords = "Zero"
        Else
            mTotalInWords = Trim(Mid(mTotalInWords, 1, Len(mTotalInWords) - 5))
        End If
        mHundredStr = mTotalInWords

        mAmountStr = Mid(mAmount, 8, 1)
        mTotalInWords = MainClass.RupeesConversion(Val(mAmountStr))
        If Trim(mTotalInWords) = "" Then
            mTotalInWords = "Zero"
        Else
            mTotalInWords = Trim(Mid(mTotalInWords, 1, Len(mTotalInWords) - 5))
        End If
        mTenStr = mTotalInWords

        mAmountStr = VB.Right(mAmount, 1)
        mTotalInWords = MainClass.RupeesConversion(Val(mAmountStr))
        If Trim(mTotalInWords) = "" Then
            mTotalInWords = "Zero"
        Else
            mTotalInWords = Trim(Mid(mTotalInWords, 1, Len(mTotalInWords) - 5))
        End If
        mUnitStr = mTotalInWords

        MainClass.AssignCRptFormulas(Report1, "CroreStr=""" & mCroreStr & """")
        MainClass.AssignCRptFormulas(Report1, "LacsStr=""" & mLacsStr & """")
        MainClass.AssignCRptFormulas(Report1, "ThousandStr=""" & mThousandStr & """")
        MainClass.AssignCRptFormulas(Report1, "HundredStr=""" & mHundredStr & """")
        MainClass.AssignCRptFormulas(Report1, "TenStr=""" & mTenStr & """")
        MainClass.AssignCRptFormulas(Report1, "UnitStr=""" & mUnitStr & """")

        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub


    Private Sub SprdMain_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdMain.DblClick
        '    SprdMain_Click ColEmpCode, 0
    End Sub

    Private Sub SprdMain_KeyUpEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles SprdMain.KeyUpEvent
        Dim mCol As Short


        mCol = SprdMain.ActiveCol

        If eventArgs.KeyCode = System.Windows.Forms.Keys.F1 And mCol = ColEmpCode Then SprdMain_ClickEvent(SprdMain, New AxFPSpreadADO._DSpreadEvents_ClickEvent(ColEmpCode, 0))

    End Sub

    Private Sub SprdMain_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles SprdMain.LeaveCell

        On Error GoTo ErrPart
        Dim xEmpName As String
        Dim mCESSAmount As Double

        If eventArgs.NewRow = -1 Then Exit Sub

        SprdMain.Row = SprdMain.ActiveRow

        Select Case eventArgs.col
            Case ColEmpCode
                SprdMain.Col = ColEmpCode
                If Trim(SprdMain.Text) = "" Then Exit Sub
                SprdMain.Text = VB6.Format(SprdMain.Text, "000000")
                If MainClass.ValidateWithMasterTable(SprdMain.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    If DuplicateEmpCode() = False Then
                        SprdMain.Row = SprdMain.ActiveRow
                        xEmpName = MasterNo
                        SprdMain.Col = ColEmpName
                        SprdMain.Text = xEmpName
                        MainClass.AddBlankSprdRow(SprdMain, ColEmpCode, ConRowHeight)
                        FormatSprd(-1)
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColAmt)
                    Else
                        MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColEmpCode)
                        eventArgs.cancel = True
                    End If
                Else
                    MsgInformation("Invalid Emp Code.")
                    MainClass.SetFocusToCell(SprdMain, eventArgs.row, ColEmpCode)
                    eventArgs.cancel = True
                End If
            Case ColAmt
                '            SprdMain.Col = ColAmt
                '            If Val(SprdMain.Text) <> 0 Then
                '                mCessAmount = Val(SprdMain.Text) * 2 / 102
                '                mCessAmount = Round(mCessAmount, 0)
                '                SprdMain.Col = ColCessAmt
                '                SprdMain.Text = Format(mCessAmount, "0.00")
                '            End If
        End Select
        Call CalcTotal()
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Function DuplicateEmpCode() As Boolean
        Dim cntRow As Integer
        Dim mCount As Byte
        Dim mCheckEmpCode As String
        Dim mEmpCode As String

        With SprdMain
            .Row = .ActiveRow
            .Col = ColEmpCode
            mCheckEmpCode = Trim(UCase(.Text))

            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColEmpCode
                mEmpCode = Trim(UCase(.Text))

                If mEmpCode = mCheckEmpCode Then
                    mCount = mCount + 1
                End If

            Next

            If mCount > 1 Then
                DuplicateEmpCode = True
                MsgInformation("Duplicate Emp Code : " & mEmpCode & " ")
                '            MainClass.SetFocusToCell SprdMain, .ActiveRow, ColEmpCode
                Exit Function
            End If

        End With
    End Function

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        txtVNo.Text = SprdView.Text

        TxtVNo_Validating(TxtVNo, New System.ComponentModel.CancelEventArgs(True))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub
    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Private Sub txtAmountPaid_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAmountPaid.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtAmountPaid_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtAmountPaid.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub


    Private Sub txtAmountPaid_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtAmountPaid.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcNetAmount()
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtBankCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBankCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBankCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBankCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtBankName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBankName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtBankName_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtBankName.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtBankName.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtCess_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCess.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCess_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCess.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub
    Private Sub txtCess_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCess.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcTDSAmount()
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub txtChallanDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtChallanDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtChallanDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtChallanDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtChallanDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtChallanDate.Text) Then
            MsgInformation("Invaild Challan Date.")
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtChallanNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtChallanNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtChallanNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtChallanNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtChallanNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtChqDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtChqDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtChqDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtChqDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtChqDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtChqDate.Text) Then
            MsgInformation("Invaild Chq Date.")
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtChqNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtChqNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtChqNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtChqNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtChqNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtInterest_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtInterest.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtInterest_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtInterest.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtInterest_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtInterest.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcNetAmount()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtLastVNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLastVNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLastVNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLastVNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtLastVNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtLastVNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim SqlStr As String = ""

        If Trim(txtLastVNo.Text) = "" Then GoTo EventExitSub

        If Len(txtLastVNo.Text) < 6 Then
            txtLastVNo.Text = Val(txtLastVNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If

        SqlStr = "SELECT * FROM PAY_ITChallan_HDR " & " WHERE AUTO_KEY_REFNO=" & Val(txtLastVNo.Text) & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        If RsTemp.EOF = True Then
            MsgInformation("Invalid Last Vno.")
            Cancel = True
            GoTo EventExitSub
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtNetAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNetAmount.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtNetAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtNetAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtOthers_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOthers.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOthers_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOthers.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtOthers_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtOthers.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcNetAmount()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtSurcharge_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtSurcharge.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtSurcharge_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtSurcharge.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtSurcharge_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtSurcharge.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Call CalcTDSAmount()
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtTDSAmount_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTDSAmount.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtTDSAmount_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTDSAmount.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtVDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVDate.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtVDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtVDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Not IsDate(txtVDate.Text) Then
            MsgInformation("Invaild Ref Date.")
            Cancel = True
            GoTo EventExitSub
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtVno_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtVNo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
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
        Dim xMkey As String = ""
        Dim mVNO As Double
        Dim SqlStr As String = ""

        If Trim(txtVNo.Text) = "" Then GoTo EventExitSub

        If Len(txtVNo.Text) < 6 Then
            txtVNo.Text = Val(txtVNo.Text) & VB6.Format(RsCompany.Fields("FYEAR").Value, "0000") & VB6.Format(RsCompany.Fields("COMPANY_CODE").Value, "00")
        End If


        mVNO = Val(txtVNo.Text)

        If MODIFYMode = True And RsChallanMain.BOF = False Then xMKey = RsChallanMain.Fields("AUTO_KEY_REFNO").Value

        SqlStr = "SELECT * FROM PAY_ITChallan_HDR " & " WHERE AUTO_KEY_REFNO='" & MainClass.AllowSingleQuote(UCase(CStr(mVNO))) & "'" & vbCrLf & " AND COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_REFNO,LENGTH(AUTO_KEY_REFNO)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND BOOKTYPE='" & lblBookType.Text & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsChallanMain, ADODB.LockTypeEnum.adLockReadOnly)
        If RsChallanMain.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Ref No. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM PAY_ITChallan_HDR WHERE AUTO_KEY_REFNO=" & Val(xMKey) & "" & vbCrLf & " AND SUBSTR(AUTO_KEY_REFNO,LENGTH(AUTO_KEY_REFNO)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND BOOKTYPE='" & lblBookType.Text & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsChallanMain, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub CalcTotal()
        Dim mPaidAmount As Double
        Dim mCess As Double
        Dim mSurcharge As Double
        Dim mTDSAmount As Double
        Dim mTotalPaidAmount As Double
        Dim mTotCess As Double
        Dim mTotSurcharge As Double
        Dim mTotTDSAmount As Double
        Dim cntRow As Integer
        Dim mTaxAmount As Double
        Dim mTotalTaxAmount As Double

        For cntRow = 1 To SprdMain.MaxRows
            SprdMain.Row = cntRow

            SprdMain.Col = ColEmpCode

            If Trim(SprdMain.Text) <> "" Then
                SprdMain.Col = ColAmtPaid
                mPaidAmount = Val(SprdMain.Text)
                mTotalPaidAmount = mTotalPaidAmount + mPaidAmount

                SprdMain.Col = ColAmt
                mTaxAmount = Val(SprdMain.Text)
                mTotalTaxAmount = mTotalTaxAmount + mTaxAmount

                SprdMain.Col = ColSurcharge
                mSurcharge = Val(SprdMain.Text)
                mTotSurcharge = mTotSurcharge + mSurcharge

                SprdMain.Col = ColTDSAmount
                If RsCompany.Fields("FYEAR").Value < 2007 Then
                    mTDSAmount = ((mTaxAmount) * 100 / IIf(mSurcharge <= 0, 102, 112.2))
                ElseIf RsCompany.Fields("FYEAR").Value < 2018 Then
                    mTDSAmount = ((mTaxAmount) * 100 / IIf(mSurcharge <= 0, 103, 113.3))
                Else
                    mTDSAmount = ((mTaxAmount) * 100 / IIf(mSurcharge <= 0, 104, 114.4))
                End If

                mTDSAmount = System.Math.Round(mTDSAmount, 0) ''+ 1
                SprdMain.Text = VB6.Format(mTDSAmount, "0.00")
                mTotTDSAmount = mTotTDSAmount + mTDSAmount

                SprdMain.Col = ColCessAmt
                mCess = mTaxAmount - mTDSAmount - mSurcharge
                mCess = System.Math.Round(mCess, 0)
                SprdMain.Text = VB6.Format(mCess, "0.00")
                mTotCess = mTotCess + mCess
            End If
        Next
        txtAmountPaid.Text = VB6.Format(mTotalPaidAmount, "0.00")
        lblTotal.Text = VB6.Format(mTotalTaxAmount, "0.00")
        txtCess.Text = VB6.Format(mTotCess, "0.00")
        txtSurcharge.Text = VB6.Format(mTotSurcharge, "0.00")
        txtTDSAmount.Text = VB6.Format(mTotTDSAmount, "0.00")
        '    CalcTDSAmount
        CalcNetAmount()
    End Sub
    Private Sub CalcTDSAmount()
        On Error GoTo ErrPart

        txtTDSAmount.Text = CStr(Val(lblTotal.Text) - (Val(txtSurcharge.Text) + Val(txtCess.Text)))
        txtTDSAmount.Text = VB6.Format(txtTDSAmount.Text, "0.00")

        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub

    Private Sub CalcNetAmount()
        On Error GoTo ErrPart

        txtNetAmount.Text = CStr(Val(lblTotal.Text) + (Val(txtInterest.Text) + Val(txtOthers.Text)))
        txtNetAmount.Text = VB6.Format(txtNetAmount.Text, "0.00")

        Exit Sub
ErrPart:
        MsgBox(Err.Description)
    End Sub
End Class
