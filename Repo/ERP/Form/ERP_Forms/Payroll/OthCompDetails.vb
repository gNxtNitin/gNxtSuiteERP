Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmOthCompDetails
    Inherits System.Windows.Forms.Form
    Dim RsITMain As ADODB.Recordset

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection

    Dim xCode As String
    Dim SqlStr As String = ""
    Dim FormActive As Boolean
    Private Const ConRowHeight As Short = 12


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
        MainClass.ButtonStatus(Me, XRIGHT, RsITMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub
    Private Sub Clear1()

        MainClass.ClearGrid(sprdRD)
        MainClass.ClearGrid(sprdLD)
        MainClass.ClearGrid(sprdSFD)
        FormatSprd(-1)
        '    FillSprdGrid

        txtEmpCode.Text = ""
        TxtName.Text = ""
        txtDesignation.Text = ""
        chkInstPAid.CheckState = System.Windows.Forms.CheckState.Unchecked
        SSTab1.SelectedIndex = 0
        MainClass.ButtonStatus(Me, XRIGHT, RsITMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub chkInstPAid_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkInstPAid.CheckStateChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsITMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            Call Show1()
        End If
    End Sub
    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        ViewGrid()
    End Sub
    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtEmpCode.Focus()
        Else
            ADDMode = False
            MODIFYMode = False
            If RsITMain.EOF = False Then RsITMain.MoveFirst()
            Show1()
        End If
    End Sub
    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.hide()
    End Sub
    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If txtEmpCode.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub
        If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then
            If Delete1 = False Then GoTo DelErrPart
        End If

        Exit Sub
DelErrPart:
        MsgBox("Record Not Deleted")
    End Sub
    Private Sub cmdsearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        SqlStr = ""

        If MainClass.SearchGridMaster((txtEmpCode.Text), "PAY_EMPLOYEE_MST", "EMP_CODE", "EMP_NAME", , , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            txtEmpCode.Text = AcName
            TxtName.Text = AcName1
            txtEmpCode_Validating(txtEmpCode, New System.ComponentModel.CancelEventArgs(False))
        End If
        Exit Sub

    End Sub
    Private Sub frmOthCompDetails_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub sprdLD_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles sprdLD.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub sprdRD_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles sprdRD.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub sprdRD_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles sprdRD.LeaveCell
        On Error GoTo ErrPart


        If eventArgs.NewRow = -1 Then Exit Sub
        sprdRD.Row = eventArgs.row

        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        'Resume
    End Sub
    Private Sub frmOthCompDetails_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        SqlStr = ""
        If FormActive = True Then Exit Sub
        SqlStr = "Select * From PAY_RENT_TRN Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsITMain, ADODB.LockTypeEnum.adLockOptimistic)

        AssignGrid(False)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        settextlength()
        Clear1()
        FormActive = True
        If CmdAdd.Enabled = True Then cmdAdd_Click(cmdAdd, New System.EventArgs())

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '' Resume
    End Sub
    Private Sub frmOthCompDetails_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        ADDMode = False
        MODIFYMode = False
        Me.Height = VB6.TwipsToPixelsY(7245)
        Me.Width = VB6.TwipsToPixelsX(11355)
        Me.Left = 0
        Me.Top = 0

        'CellFormat
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub
    Private Sub frmOthCompDetails_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        '    'PvtDBCn.Cancel
        '    'PvtDBCn.Close
        RsITMain = Nothing
        '    'Set PvtDBCn = Nothing
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim cntRow As Integer
        Dim mDesigationCode As String
        Dim RsDesig As ADODB.Recordset
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mInstPaid As String
        Dim mIS_SF As String

        If RsITMain.EOF = False Then
            With RsITMain
                txtEmpCode.Text = IIf(IsDbNull(.Fields("EMP_CODE").Value), "", .Fields("EMP_CODE").Value)
                xCode = IIf(IsDbNull(.Fields("EMP_CODE").Value), "", .Fields("EMP_CODE").Value)

                If MainClass.ValidateWithMasterTable((txtEmpCode.Text), "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    TxtName.Text = MasterNo
                End If

                SqlStr = " SELECT GETEMPDESG(" & RsCompany.Fields("COMPANY_CODE").Value & ",'" & Trim(txtEmpCode.Text) & "',TO_DATE('" & VB6.Format(RsCompany.Fields("END_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS EMP_DESG FROM DUAL"
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

                If RsTemp.EOF = False Then
                    txtDesignation.Text = IIf(IsDbNull(RsTemp.Fields("EMP_DESG").Value), "", RsTemp.Fields("EMP_DESG").Value)
                End If

                mInstPaid = IIf(IsDbNull(.Fields("IS_INST_PAID").Value), "N", .Fields("IS_INST_PAID").Value)

                chkInstPAid.CheckState = IIf(mInstPaid = "N", System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked)

                With sprdRD
                    For cntRow = 1 To 4
                        .Row = cntRow
                        If cntRow = 1 Then
                            .Col = 1
                            .Text = VB6.Format(IIf(IsDbNull(RsITMain.Fields("RENT_AMOUNT_1").Value), 0, RsITMain.Fields("RENT_AMOUNT_1").Value), "0.00")

                            .Col = 2
                            .Text = IIf(IsDbNull(RsITMain.Fields("LANDLOAD_PAN_1").Value), "", RsITMain.Fields("LANDLOAD_PAN_1").Value)

                            .Col = 3
                            .Text = IIf(IsDbNull(RsITMain.Fields("LANDLOAD_NAME_1").Value), "", RsITMain.Fields("LANDLOAD_NAME_1").Value)

                        ElseIf cntRow = 2 Then
                            .Col = 1
                            .Text = VB6.Format(IIf(IsDbNull(RsITMain.Fields("RENT_AMOUNT_2").Value), 0, RsITMain.Fields("RENT_AMOUNT_2").Value), "0.00")

                            .Col = 2
                            .Text = IIf(IsDbNull(RsITMain.Fields("LANDLOAD_PAN_2").Value), "", RsITMain.Fields("LANDLOAD_PAN_2").Value)

                            .Col = 3
                            .Text = IIf(IsDbNull(RsITMain.Fields("LANDLOAD_NAME_2").Value), "", RsITMain.Fields("LANDLOAD_NAME_2").Value)
                        ElseIf cntRow = 3 Then
                            .Col = 1
                            .Text = VB6.Format(IIf(IsDbNull(RsITMain.Fields("RENT_AMOUNT_3").Value), 0, RsITMain.Fields("RENT_AMOUNT_3").Value), "0.00")

                            .Col = 2
                            .Text = IIf(IsDbNull(RsITMain.Fields("LANDLOAD_PAN_3").Value), "", RsITMain.Fields("LANDLOAD_PAN_3").Value)

                            .Col = 3
                            .Text = IIf(IsDbNull(RsITMain.Fields("LANDLOAD_NAME_3").Value), "", RsITMain.Fields("LANDLOAD_NAME_3").Value)
                        Else
                            .Col = 1
                            .Text = VB6.Format(IIf(IsDbNull(RsITMain.Fields("RENT_AMOUNT_4").Value), 0, RsITMain.Fields("RENT_AMOUNT_4").Value), "0.00")

                            .Col = 2
                            .Text = IIf(IsDbNull(RsITMain.Fields("LANDLOAD_PAN_4").Value), "", RsITMain.Fields("LANDLOAD_PAN_4").Value)

                            .Col = 3
                            .Text = IIf(IsDbNull(RsITMain.Fields("LANDLOAD_NAME_4").Value), "", RsITMain.Fields("LANDLOAD_NAME_4").Value)
                        End If
                    Next
                End With

                With sprdLD
                    For cntRow = 1 To 4
                        .Row = cntRow
                        If cntRow = 1 Then
                            .Col = 1
                            .Text = IIf(IsDbNull(RsITMain.Fields("LENDER_PAN_1").Value), "", RsITMain.Fields("LENDER_PAN_1").Value)

                            .Col = 2
                            .Text = IIf(IsDbNull(RsITMain.Fields("LENDER_NAME_1").Value), "", RsITMain.Fields("LENDER_NAME_1").Value)

                        ElseIf cntRow = 2 Then
                            .Col = 1
                            .Text = IIf(IsDbNull(RsITMain.Fields("LENDER_PAN_2").Value), "", RsITMain.Fields("LENDER_PAN_2").Value)

                            .Col = 2
                            .Text = IIf(IsDbNull(RsITMain.Fields("LENDER_NAME_2").Value), "", RsITMain.Fields("LENDER_NAME_2").Value)
                        ElseIf cntRow = 3 Then
                            .Col = 1
                            .Text = IIf(IsDbNull(RsITMain.Fields("LENDER_PAN_3").Value), "", RsITMain.Fields("LENDER_PAN_3").Value)

                            .Col = 2
                            .Text = IIf(IsDbNull(RsITMain.Fields("LENDER_NAME_3").Value), "", RsITMain.Fields("LENDER_NAME_3").Value)
                        Else
                            .Col = 1
                            .Text = IIf(IsDbNull(RsITMain.Fields("LENDER_PAN_4").Value), "", RsITMain.Fields("LENDER_PAN_4").Value)

                            .Col = 2
                            .Text = IIf(IsDbNull(RsITMain.Fields("LENDER_NAME_4").Value), "", RsITMain.Fields("LENDER_NAME_4").Value)
                        End If
                    Next
                End With

                With sprdSFD
                    .Row = 1
                    .Col = 1
                    mIS_SF = IIf(IsDbNull(RsITMain.Fields("IS_SUPERANNUATION_FUND").Value), "N", RsITMain.Fields("IS_SUPERANNUATION_FUND").Value)
                    .Value = IIf(mIS_SF = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)

                    .Col = 2
                    .Text = IIf(IsDbNull(RsITMain.Fields("SUPERANNUATION_FUND_NAME").Value), "", RsITMain.Fields("SUPERANNUATION_FUND_NAME").Value)

                    .Col = 3
                    .Text = VB6.Format(IIf(IsDbNull(RsITMain.Fields("S_FUND_FROMDATE").Value), "", RsITMain.Fields("S_FUND_FROMDATE").Value), "DD/MM/YYYY")

                    .Col = 4
                    .Text = VB6.Format(IIf(IsDbNull(RsITMain.Fields("S_FUND_TODATE").Value), "", RsITMain.Fields("S_FUND_TODATE").Value), "DD/MM/YYYY")

                    .Col = 5
                    .Text = VB6.Format(IIf(IsDbNull(RsITMain.Fields("S_FUND_REPAID_AMOUNT").Value), "", RsITMain.Fields("S_FUND_REPAID_AMOUNT").Value), "0.00")

                    .Col = 6
                    .Text = VB6.Format(IIf(IsDbNull(RsITMain.Fields("S_FUND_AVG_AMOUNT").Value), "", RsITMain.Fields("S_FUND_AVG_AMOUNT").Value), "0.00")

                    .Col = 7
                    .Text = VB6.Format(IIf(IsDbNull(RsITMain.Fields("S_FUND_REPAYMENT_AMOUNT").Value), "", RsITMain.Fields("S_FUND_REPAYMENT_AMOUNT").Value), "0.00")

                    .Col = 8
                    .Text = VB6.Format(IIf(IsDbNull(RsITMain.Fields("S_FUND_GROSS_AMOUNT").Value), "", RsITMain.Fields("S_FUND_GROSS_AMOUNT").Value), "0.00")

                End With
            End With
        End If

        ADDMode = False
        MODIFYMode = False

        MainClass.ButtonStatus(Me, XRIGHT, RsITMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
        '    Resume
    End Sub
    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        If FieldsVarification = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        If Update1 = True Then
            txtEmpCode_Validating(txtEmpCode, New System.ComponentModel.CancelEventArgs(False))
            If CmdAdd.Enabled = True Then CmdAdd.Focus()
        Else
            MsgInformation("Record not saved")
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrorHandler:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
    Private Function Update1() As Boolean
        On Error GoTo UpdateError
        Dim mCode As String
        Dim cntRow As Integer
        Dim RsTemp As ADODB.Recordset = Nothing

        Dim mRentAmount1 As Double
        Dim mLL_PAN1 As String
        Dim mLL_Name1 As String
        Dim mRentAmount2 As Double
        Dim mLL_PAN2 As String
        Dim mLL_Name2 As String
        Dim mRentAmount3 As Double
        Dim mLL_PAN3 As String
        Dim mLL_Name3 As String
        Dim mRentAmount4 As Double
        Dim mLL_PAN4 As String
        Dim mLL_Name4 As String
        Dim mIS_INST_PAID As String
        Dim mLander_PAN1 As String
        Dim mLander_Name1 As String
        Dim mLander_PAN2 As String
        Dim mLander_Name2 As String
        Dim mLander_PAN3 As String
        Dim mLander_Name3 As String
        Dim mLander_PAN4 As String
        Dim mLander_Name4 As String

        Dim mISSF_Fund As String
        Dim mSF_Fund_Name As String
        Dim mFromDate As String
        Dim mToDate As String

        Dim mRepaidAmount As Double
        Dim mAvgAmount As Double
        Dim mRepaymentAmount As Double
        Dim mGrossAmount As Double


        If MainClass.ValidateWithMasterTable((txtEmpCode.Text), "EMP_CODE", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCode = MasterNo
        Else
            MsgInformation("Employee Name is not exsits in Master.")
            Update1 = False
            Exit Function
        End If

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()


        SqlStr = " DELETE FROM  PAY_RENT_TRN WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " Emp_Code='" & mCode & "' "
        PubDBCn.Execute(SqlStr)

        With sprdRD
            For cntRow = 1 To 4
                .Row = cntRow
                If cntRow = 1 Then
                    .Col = 1
                    mRentAmount1 = Val(.Text)

                    .Col = 2
                    mLL_PAN1 = Trim(.Text)

                    .Col = 3
                    mLL_Name1 = Trim(.Text)

                ElseIf cntRow = 2 Then
                    .Col = 1
                    mRentAmount2 = Val(.Text)

                    .Col = 2
                    mLL_PAN2 = Trim(.Text)

                    .Col = 3
                    mLL_Name2 = Trim(.Text)
                ElseIf cntRow = 3 Then
                    .Col = 1
                    mRentAmount3 = Val(.Text)

                    .Col = 2
                    mLL_PAN3 = Trim(.Text)

                    .Col = 3
                    mLL_Name3 = Trim(.Text)
                Else
                    .Col = 1
                    mRentAmount4 = Val(.Text)

                    .Col = 2
                    mLL_PAN4 = Trim(.Text)

                    .Col = 3
                    mLL_Name4 = Trim(.Text)
                End If
            Next
        End With

        With sprdLD
            For cntRow = 1 To 4
                .Row = cntRow
                If cntRow = 1 Then

                    .Col = 1
                    mLander_PAN1 = Trim(.Text)

                    .Col = 2
                    mLander_Name1 = Trim(.Text)

                ElseIf cntRow = 2 Then
                    .Col = 1
                    mLander_PAN2 = Trim(.Text)

                    .Col = 2
                    mLander_Name2 = Trim(.Text)
                ElseIf cntRow = 3 Then
                    .Col = 1
                    mLander_PAN3 = Trim(.Text)

                    .Col = 2
                    mLander_Name3 = Trim(.Text)
                Else
                    .Col = 1
                    mLander_PAN4 = Trim(.Text)

                    .Col = 2
                    mLander_Name4 = Trim(.Text)
                End If
            Next
        End With

        With sprdSFD
            .Row = 1

            .Col = 1
            mISSF_Fund = IIf(.Value = CStr(System.Windows.Forms.CheckState.Checked), "Y", "N")

            .Col = 2
            mSF_Fund_Name = Trim(.Text)

            .Col = 3
            mFromDate = VB6.Format(.Text, "DD/MM/YYYY")

            .Col = 4
            mToDate = VB6.Format(.Text, "DD/MM/YYYY")

            .Col = 5
            mRepaidAmount = CDbl(VB6.Format(Val(.Text), "0.00"))

            .Col = 6
            mAvgAmount = CDbl(VB6.Format(Val(.Text), "0.00"))

            .Col = 7
            mRepaymentAmount = CDbl(VB6.Format(Val(.Text), "0.00"))

            .Col = 8
            mGrossAmount = CDbl(VB6.Format(Val(.Text), "0.00"))

        End With

        mIS_INST_PAID = IIf(chkInstPAid.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N")

        SqlStr = "INSERT INTO  PAY_RENT_TRN ( " & vbCrLf & " COMPANY_CODE, FYEAR, EMP_CODE, " & vbCrLf & " RENT_AMOUNT_1 , LANDLOAD_PAN_1  , LANDLOAD_NAME_1  ," & vbCrLf & " RENT_AMOUNT_2 , LANDLOAD_PAN_2  , LANDLOAD_NAME_2  ," & vbCrLf & " RENT_AMOUNT_3 , LANDLOAD_PAN_3  , LANDLOAD_NAME_3  , " & vbCrLf & " RENT_AMOUNT_4 , LANDLOAD_PAN_4  , LANDLOAD_NAME_4  , " & vbCrLf & " IS_INST_PAID  , " & vbCrLf & " LENDER_PAN_1  , LENDER_NAME_1  , " & vbCrLf & " LENDER_PAN_2  , LENDER_NAME_2 , " & vbCrLf & " LENDER_PAN_3  , LENDER_NAME_3  , " & vbCrLf & " LENDER_PAN_4  , LENDER_NAME_4  , " & vbCrLf & " IS_SUPERANNUATION_FUND , SUPERANNUATION_FUND_NAME , " & vbCrLf & " S_FUND_FROMDATE  , S_FUND_TODATE  , " & vbCrLf & " S_FUND_REPAID_AMOUNT , S_FUND_AVG_AMOUNT , " & vbCrLf & " S_FUND_REPAYMENT_AMOUNT , S_FUND_GROSS_AMOUNT, " & vbCrLf & " ADDUSER, ADDDATE) " & vbCrLf & " VALUES ( "


        SqlStr = SqlStr & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & ", '" & Trim(txtEmpCode.Text) & "',  " & vbCrLf & " " & mRentAmount1 & ", '" & Trim(mLL_PAN1) & "', '" & MainClass.AllowSingleQuote(mLL_Name1) & "'," & vbCrLf & " " & mRentAmount2 & ", '" & Trim(mLL_PAN2) & "', '" & MainClass.AllowSingleQuote(mLL_Name2) & "'," & vbCrLf & " " & mRentAmount3 & ", '" & Trim(mLL_PAN3) & "', '" & MainClass.AllowSingleQuote(mLL_Name3) & "'," & vbCrLf & " " & mRentAmount4 & ", '" & Trim(mLL_PAN4) & "', '" & MainClass.AllowSingleQuote(mLL_Name4) & "'," & vbCrLf & " '" & mIS_INST_PAID & "', " & vbCrLf & " '" & Trim(mLander_PAN1) & "', '" & MainClass.AllowSingleQuote(mLander_Name1) & "'," & vbCrLf & " '" & Trim(mLander_PAN2) & "', '" & MainClass.AllowSingleQuote(mLander_Name2) & "'," & vbCrLf & " '" & Trim(mLander_PAN3) & "', '" & MainClass.AllowSingleQuote(mLander_Name3) & "'," & vbCrLf & " '" & Trim(mLander_PAN4) & "', '" & MainClass.AllowSingleQuote(mLander_Name4) & "'," & vbCrLf & " '" & mISSF_Fund & "', '" & MainClass.AllowSingleQuote(mSF_Fund_Name) & "', " & vbCrLf & " TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), TO_DATE('" & VB6.Format(mToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf & " " & Val(CStr(mRepaidAmount)) & ", " & Val(CStr(mAvgAmount)) & ",  " & vbCrLf & " " & Val(CStr(mRepaymentAmount)) & ", " & Val(CStr(mGrossAmount)) & "," & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"





        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()
        RsITMain.Requery()
        Update1 = True
        Exit Function
UpdateError:
        'Resume
        Update1 = False
        PubDBCn.RollbackTrans()
        RsITMain.Requery()
        If Err.Number = -2147467259 Then
            MsgBox("Can't Modify Transaction Exists Against this Code")
            Exit Function
        End If
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        PubDBCn.Errors.Clear()
        'Resume
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Private Sub sprdSFD_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles sprdSFD.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SqlStr = ""
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        txtEmpCode.Text = SprdView.Text

        txtEmpCode_Validating(txtEmpCode, New System.ComponentModel.CancelEventArgs(True))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub
    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub
    Private Sub txtDesignation_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDesignation.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtEmpCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpCode.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtEmpCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtEmpCode.DoubleClick
        cmdsearch_Click(cmdsearch, New System.EventArgs())
    End Sub

    Private Sub txtEmpCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtEmpCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.TitleCase(KeyAscii, txtEmpCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtEmpCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtEmpCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then
            cmdsearch_Click(cmdsearch, New System.EventArgs())
        End If
    End Sub
    Private Function FieldsVarification() As Boolean
        On Error GoTo ERR1
        Dim xAmount As Decimal

        FieldsVarification = True
        If Trim(txtEmpCode.Text) = "" Then
            MsgInformation("Employee Code is empty. Cannot Save")
            txtEmpCode.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Account or modify an existing item")
            FieldsVarification = False
        End If
        If MODIFYMode = True And RsITMain.EOF = True Then Exit Function
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
ERR1:
        MsgInformation(Err.Description)
        FieldsVarification = False
        'Resume
    End Function
    Private Sub settextlength()

        On Error GoTo ERR1

        txtEmpCode.Maxlength = MainClass.SetMaxLength("EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn)

        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)


        SqlStr = " SELECT IH.EMP_CODE, EMP_NAME, " & vbCrLf & " RENT_AMOUNT_1, LANDLOAD_PAN_1, LANDLOAD_NAME_1, " & vbCrLf & " RENT_AMOUNT_2, LANDLOAD_PAN_2, LANDLOAD_NAME_2, " & vbCrLf & " RENT_AMOUNT_3, LANDLOAD_PAN_3, LANDLOAD_NAME_3, " & vbCrLf & " RENT_AMOUNT_4, LANDLOAD_PAN_4, LANDLOAD_NAME_4, " & vbCrLf & " IS_INST_PAID, LENDER_PAN_1, LENDER_NAME_1, " & vbCrLf & " LENDER_PAN_2, LENDER_NAME_2, " & vbCrLf & " LENDER_PAN_3, LENDER_NAME_3, " & vbCrLf & " LENDER_PAN_4, LENDER_NAME_4, " & vbCrLf & " IS_SUPERANNUATION_FUND, SUPERANNUATION_FUND_NAME, " & vbCrLf & " S_FUND_FROMDATE, S_FUND_TODATE, " & vbCrLf & " S_FUND_REPAID_AMOUNT, S_FUND_AVG_AMOUNT, " & vbCrLf & " S_FUND_REPAYMENT_AMOUNT , S_FUND_GROSS_AMOUNT " & vbCrLf & " FROM PAY_EMPLOYEE_MST EMP,PAY_RENT_TRN IH " & vbCrLf & " WHERE " & vbCrLf & " EMP.COMPANY_CODE=IH.COMPANY_CODE " & vbCrLf & " AND EMP.EMP_CODE=IH.EMP_CODE " & vbCrLf & " AND IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND IH.FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()

    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 12)
            .set_ColWidth(0, 5)
            .set_ColWidth(1, 8)
            .set_ColWidth(2, 16)
            .set_ColWidth(3, 12)
            .set_ColWidth(4, 12)

            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr

        SqlStr = ""
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = " Delete from PAY_RENT_TRN WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND Emp_Code='" & MainClass.AllowSingleQuote((txtEmpCode.Text)) & "'"
        PubDBCn.Execute(SqlStr)

        PubDBCn.CommitTrans()
        Clear1()
        RsITMain.Requery()
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans()
        RsITMain.Requery()
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete Transaction Exists Against This Employee.")
            Exit Function
        End If
        MsgBox(Err.Description)
    End Function
    Private Sub FormatSprd(ByRef mRow As Integer)

        On Error GoTo ERR1
        Dim cntCol As Integer

        With sprdRD
            .Row = -1
            .MaxCols = 3
            .MaxRows = 4
            .set_RowHeight(-1, ConRowHeight * 1.5)

            .Col = 1
            .CellType = SS_CELL_TYPE_FLOAT
            .TypeFloatDecimalChar = Asc(".")
            .TypeFloatMax = CDbl("9999999.99")
            .TypeFloatMin = CDbl("-9999999.99")
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
            .set_ColWidth(1, 12)


            .Col = 2
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsITMain.Fields("LANDLOAD_PAN_1").DefinedSize
            .set_ColWidth(2, 12)

            .Col = 3
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsITMain.Fields("LANDLOAD_NAME_1").DefinedSize
            .set_ColWidth(3, 50)
        End With

        With sprdLD
            .Row = -1
            .MaxCols = 2
            .MaxRows = 4
            .set_RowHeight(-1, ConRowHeight * 1.5)

            .Col = 1
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsITMain.Fields("LENDER_PAN_1").DefinedSize
            .set_ColWidth(1, 20)

            .Col = 2
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = False
            .TypeEditLen = RsITMain.Fields("LENDER_NAME_1").DefinedSize
            .set_ColWidth(2, 40)
        End With

        With sprdSFD
            .Row = -1
            .MaxCols = 8
            .MaxRows = 1
            .set_RowHeight(-1, ConRowHeight * 2)

            .Col = 1
            .CellType = SS_CELL_TYPE_CHECKBOX
            .TypeHAlign = SS_CELL_H_ALIGN_CENTER
            .set_ColWidth(1, 12)
            .Value = CStr(System.Windows.Forms.CheckState.Unchecked)

            .Col = 2
            .CellType = SS_CELL_TYPE_EDIT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditCharCase = SS_CELL_EDIT_CASE_UPPER_CASE
            .TypeEditMultiLine = True
            .TypeEditLen = RsITMain.Fields("SUPERANNUATION_FUND_NAME").DefinedSize

            .Col = 3
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeDateFormat = SS_CELL_DATE_FORMAT_DDMMYY
            .set_ColWidth(3, 15)

            .Col = 4
            .CellType = SS_CELL_TYPE_DATE
            .TypeDateCentury = True
            .TypeDateFormat = SS_CELL_DATE_FORMAT_DDMMYY
            .set_ColWidth(4, 15)

            For cntCol = 5 To 8
                .Col = cntCol
                .CellType = SS_CELL_TYPE_FLOAT
                .TypeFloatDecimalChar = Asc(".")
                .TypeFloatMax = CDbl("9999999.99")
                .TypeFloatMin = CDbl("-9999999.99")
                .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_NUMERIC
                .set_ColWidth(cntCol, 15)
            Next

        End With

        MainClass.SetSpreadColor(sprdLD, mRow)
        MainClass.SetSpreadColor(sprdRD, mRow)
        MainClass.SetSpreadColor(sprdSFD, mRow)

        sprdLD.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
        sprdRD.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal
        sprdSFD.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal

        FillSprdGrid()
        Exit Sub
ERR1:
        '    Resume
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub FillSprdGrid()
        With sprdRD
            .MaxCols = 3
            .MaxRows = 4
        End With
    End Sub
    Private Sub txtEmpCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmpCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim RsEmp As ADODB.Recordset = Nothing
        Dim mName As String
        Dim SqlStr As String = ""
        Dim RsTemp As ADODB.Recordset = Nothing

        If Trim(txtEmpCode.Text) = "" Then GoTo EventExitSub
        txtEmpCode.Text = VB6.Format(Trim(txtEmpCode.Text), "000000")

        If MainClass.ValidateWithMasterTable(txtEmpCode.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            TxtName.Text = MasterNo

            SqlStr = " SELECT GETEMPDESG(" & RsCompany.Fields("COMPANY_CODE").Value & ",'" & Trim(txtEmpCode.Text) & "',TO_DATE('" & VB6.Format(RsCompany.Fields("END_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY')) AS EMP_DESG FROM DUAL"
            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

            If RsTemp.EOF = False Then
                txtDesignation.Text = IIf(IsDbNull(RsTemp.Fields("EMP_DESG").Value), "", RsTemp.Fields("EMP_DESG").Value)
            End If
        Else
            MsgInformation("Invalid Emp Code")
            Cancel = True
            GoTo EventExitSub
        End If

        SqlStr = " SELECT * FROM PAY_RENT_TRN WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " Emp_Code='" & txtEmpCode.Text & "'"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsITMain, ADODB.LockTypeEnum.adLockOptimistic)

        If RsITMain.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Call Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Click Add Button.", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = " SELECT * FROM PAY_RENT_TRN WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " EmpCode='" & txtEmpCode.Text & "'"

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsITMain, ADODB.LockTypeEnum.adLockOptimistic)

            End If
        End If


        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
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
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mCode As Integer

        SqlStr = " SELECT " & vbCrLf & " EMP.*, " & vbCrLf & " IH.*, ID.* " & vbCrLf & " FROM " & vbCrLf & " PAY_RENT_TRN IH, " & vbCrLf & " PAY_EMPLOYEE_MST EMP " & vbCrLf & " WHERE IH.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " AND IH.FYEAR =" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND IH.COMPANY_CODE = EMP.COMPANY_CODE " & vbCrLf & " AND IH.EMP_CODE = EMP.EMP_CODE AND IH.EMP_CODE='" & MainClass.AllowSingleQuote((txtEmpCode.Text)) & "'" & vbCrLf & " ORDER BY ID.SUBROW"

        mSubTitle = ""
        mTitle = "Other Computation Details"
        Call ShowReport(SqlStr, "OthCompDetails.Rpt", Mode, mTitle, mSubTitle)

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

        Dim mCode As Integer
        Dim Str_Renamed As String
        Dim mFName As String
        Dim mDesignation As String
        Dim mRegdAddress As String
        Dim mAuthoSign As String
        Dim mAuthoDesg As String
        Dim mAuthoFName As String

        Report1.SQLQuery = mSqlStr

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)

        MainClass.AssignCRptFormulas(Report1, "Name='" & TxtName.Text & "'")
        MainClass.AssignCRptFormulas(Report1, "mTANNo='" & IIf(IsDbNull(RsCompany.Fields("TDSACNO").Value), "", RsCompany.Fields("TDSACNO").Value) & "'")
        MainClass.AssignCRptFormulas(Report1, "mCircle='" & IIf(IsDbNull(RsCompany.Fields("TDSCIRCLE").Value), "", RsCompany.Fields("TDSCIRCLE").Value) & "'")
        '    MainClass.AssignCRptFormulas Report1, "Designation='" & txtDesignation.Text & "'"
        MainClass.AssignCRptFormulas(Report1, "mFYEAR='" & Year(RsCompany.Fields("START_DATE").Value) & "-" & Year(RsCompany.Fields("END_DATE").Value) & "'")

        If InStr(1, Trim(UCase(txtDesignation.Text)), "DIRECTOR", CompareMethod.Text) > 0 Then
            '    If Trim(UCase(txtDesignation.Text)) = "DIRECTOR" Then
            MainClass.AssignCRptFormulas(Report1, "IsDirector='Yes'")
        Else
            MainClass.AssignCRptFormulas(Report1, "IsDirector='No'")
        End If

        mRegdAddress = IIf(IsDbNull(RsCompany.Fields("REGD_ADDR1").Value), "", RsCompany.Fields("REGD_ADDR1").Value)
        mRegdAddress = mRegdAddress & " " & IIf(IsDbNull(RsCompany.Fields("REGD_ADDR2").Value), "", RsCompany.Fields("REGD_ADDR2").Value)
        mRegdAddress = mRegdAddress & " " & IIf(IsDbNull(RsCompany.Fields("REGD_CITY").Value), "", RsCompany.Fields("REGD_CITY").Value)
        '    mRegdAddress = mRegdAddress & " " & IIf(IsNull(RsCompany!REGD_STATE), "", RsCompany!REGD_STATE)
        mRegdAddress = mRegdAddress & " - " & IIf(IsDbNull(RsCompany.Fields("REGD_PIN").Value), "", RsCompany.Fields("REGD_PIN").Value)

        MainClass.AssignCRptFormulas(Report1, "RegdAddress=""" & mRegdAddress & """")


        mAuthoSign = IIf(IsDbNull(RsCompany.Fields("TDSAUTHORIZED").Value), "", RsCompany.Fields("TDSAUTHORIZED").Value)
        mAuthoFName = IIf(IsDbNull(RsCompany.Fields("TDSAUTHORIZED_FNAME").Value), "", RsCompany.Fields("TDSAUTHORIZED_FNAME").Value)
        mAuthoDesg = IIf(IsDbNull(RsCompany.Fields("TDSAUTHORIZED_DESIG").Value), "", RsCompany.Fields("TDSAUTHORIZED_DESIG").Value)

        MainClass.AssignCRptFormulas(Report1, "FullName='" & mAuthoSign & "'")
        MainClass.AssignCRptFormulas(Report1, "AuthDesg='" & mAuthoDesg & "'")
        MainClass.AssignCRptFormulas(Report1, "AuthoFName='" & mAuthoFName & "'")

        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName
        Report1.Action = 1
    End Sub

    Private Sub TxtName_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtName.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
End Class
