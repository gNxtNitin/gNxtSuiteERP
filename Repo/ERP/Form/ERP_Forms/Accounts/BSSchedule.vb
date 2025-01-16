Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmBSSchedule
    Inherits System.Windows.Forms.Form
    'Dim PvtDBCn As ADODB.Connection
    Private FormLoaded As Boolean
    Private PrintEnable As Boolean

    Private Const ColDesc As Short = 1
    Private Const ColSchd As Short = 2
    Private Const ColCurrSubTotal As Short = 3
    Private Const ColCurrTotal As Short = 4
    Private Const ColPrevSubTotal As Short = 5
    Private Const ColPrevTotal As Short = 6
    Private Const ColCode As Short = 7
    Private Const ColCategory As Short = 8


    Private Sub CboScheduleNo_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CboScheduleNo.SelectedIndexChanged
        PrintEnable = False
        If CboScheduleNo.Text = "Other" Then
            cboHead.Visible = True
        Else
            cboHead.Visible = False
            FieldVarification()
        End If
        Call Show1()
        PrintCommand()
    End Sub
    Private Sub cmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click
        Me.Hide()
        Me.Dispose()
        Me.Close()
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click

        FraPreview.Visible = True
        FraPreview.BringToFront()
        With SprdView
            .set_ColWidth(ColDesc, 27)
            .set_ColWidth(ColSchd, 4)
            .set_ColWidth(ColCurrSubTotal, 11)
            .set_ColWidth(ColCurrTotal, 11)
            .set_ColWidth(ColPrevSubTotal, 11)
            .set_ColWidth(ColPrevTotal, 11)
        End With

        SprdView.PrintHeader = "/c/fn""Arial""/fz""14""/fb1" & RsCompany.Fields("Company_Name").Value & "/fn""Arial""/fz""10""/fb0/rSchedule " & CboScheduleNo.Text & "/n/fn""Arial""/fz""10""/fb1Schedule Forming Part of Balance Sheet as on " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY") & ""

        Call SpreadSheetPreview(SprdView, SprdPreview, SprdCommand, VB6.PixelsToTwipsX(ClientRectangle.Width) - 200, VB6.PixelsToTwipsY(ClientRectangle.Height) - 200)

    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click

        Dim Font1 As String
        Dim Font2 As String
        Dim Font3 As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ' Set printing options for spreadsheet
        With SprdView
            .set_ColWidth(ColDesc, 27)
            .set_ColWidth(ColSchd, 4)
            .set_ColWidth(ColCurrSubTotal, 11)
            .set_ColWidth(ColCurrTotal, 11)
            .set_ColWidth(ColPrevSubTotal, 11)
            .set_ColWidth(ColPrevTotal, 11)
        End With

        SprdView.PrintJobName = RsCompany.Fields("Company_Name").Value
        Font1 = "/fn""Arial""/fz""14""/fb1"
        Font2 = "/fn""Arial""/fz""10""/fb0"
        Font3 = "/fn""Arial""/fz""10""/fb1"



        SprdView.PrintHeader = "/c/fn""Arial""/fz""14""/fb1" & RsCompany.Fields("Company_Name").Value & "/fn""Arial""/fz""10""/fb0/rSchedule " & CboScheduleNo.Text & "/n/fn""Arial""/fz""10""/fb1Schedule Forming Part of Balance Sheet as on " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY") & ""
        'SprdView.PrintFooter = "/cPrint Footer/rPage #/p/n2nd Line"

        SprdView.PrintColHeaders = True
        SprdView.PrintRowHeaders = False
        SprdView.PrintBorder = True
        SprdView.PrintColor = False
        SprdView.PrintGrid = False
        SprdView.PrintShadows = False
        SprdView.PrintUseDataMax = True

        SprdView.PrintType = SS_PRINT_ALL

        'Print control

        SprdView.PrintMarginTop = 1440
        SprdView.PrintMarginBottom = 1440
        SprdView.PrintMarginLeft = 720
        SprdView.PrintMarginRight = 720

        SprdView.Action = SS_ACTION_PRINT
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub cmdRefresh_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdRefresh.Click
        FieldVarification()
        RefreshScreen()
    End Sub

    Private Sub FieldVarification()

        On Error GoTo ERR1

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        MainClass.ClearGrid(SprdView)
        If Trim(CboScheduleNo.Text) = "" Then
            MsgBox("Please Select Schedule No.", MsgBoxStyle.Critical)
            CboScheduleNo.Focus()
            Me.Cursor = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        If Trim(cboHead.Text) = "" And cboHead.Visible = True Then
            MsgBox("Please Select Head.", MsgBoxStyle.Critical)
            cboHead.Focus()
            Me.Cursor = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        PrintEnable = True
        PrintCommand()

        FillHeading()
        SprdView.Refresh()
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        'Resume
    End Sub
    Sub FormatSprdView()

        With SprdView
            '.RowHeight(-1) = 12
            .Row = -1

            .Col = ColDesc
            .set_ColWidth(ColDesc, 35)
            .CellType = SS_CELL_TYPE_STATIC_TEXT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeMaxEditLen = 400
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT

            .Col = ColSchd
            .set_ColWidth(ColSchd, 4)
            .CellType = SS_CELL_TYPE_STATIC_TEXT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT

            .Col = ColCurrSubTotal
            .set_ColWidth(ColCurrSubTotal, 9.5)
            .CellType = SS_CELL_TYPE_STATIC_TEXT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT

            .Col = ColCurrTotal
            .set_ColWidth(ColCurrTotal, 9.5)
            .CellType = SS_CELL_TYPE_STATIC_TEXT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT

            .Col = ColPrevSubTotal
            .set_ColWidth(ColPrevSubTotal, 9.5)
            .CellType = SS_CELL_TYPE_STATIC_TEXT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT

            .Col = ColPrevTotal
            .set_ColWidth(ColPrevTotal, 9.5)
            .CellType = SS_CELL_TYPE_STATIC_TEXT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_RIGHT

            .Col = ColCode
            .set_ColWidth(ColCode, 5)
            .CellType = SS_CELL_TYPE_STATIC_TEXT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .ColHidden = True

            .Col = ColCategory
            .set_ColWidth(ColCategory, 5)
            .CellType = SS_CELL_TYPE_STATIC_TEXT
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeHAlign = SS_CELL_H_ALIGN_LEFT
            .ColHidden = True

            .UserResize = FPSpreadADO.UserResizeConstants.UserResizeNone
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            SprdView.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal ''' OperationModeSingle
            .GridColor = System.Drawing.ColorTranslator.FromOle(&HC00000)
        End With
    End Sub
    Private Sub RefreshScreen()
        On Error GoTo InsertErr

        Dim Sqlstr As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor


        PubDBCn.BeginTrans()

        If FillIntoTempTRNQry = False Then GoTo RefreshErr

        '    If InsertIntoBS = False Then GoTo RefreshErr

        PubDBCn.CommitTrans()

        Call Show1()

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
RefreshErr:
        'Resume
        PubDBCn.RollbackTrans()
InsertErr:
        MsgInformation(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Show1()

        On Error GoTo InsertErr
        Dim RsBalance As ADODB.Recordset
        Dim RowPos As Integer
        Dim mCurrTotalAmount As Double
        Dim mCurrSubAmount As Double
        Dim mPrevTotalAmount As Double
        Dim mPrevSubAmount As Double

        Dim mCurrAmount As Double
        Dim mPrevAmount As Double
        Dim mAcctType As String

        Dim Sqlstr As String

        PubDBCn.BeginTrans()
        If InsertIntoBS = False Then GoTo RefreshErr
        PubDBCn.CommitTrans()


        Sqlstr = " SELECT * " & vbCrLf & " FROM TEMP_BALANCESHEET " & vbCrLf & " WHERE SCHEDULENO=" & Val(CboScheduleNo.Text) & " AND UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf & " START WITH  CATEGORY='G' AND PARENTCODE='-1' " & vbCrLf & " AND UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf & " CONNECT BY PRIOR CODE= PARENTCODE " & vbCrLf & " AND UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'"


        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsBalance, ADODB.LockTypeEnum.adLockReadOnly)

        RowPos = 1
        If RsBalance.EOF = False Then
            With SprdView
                RsBalance.MoveFirst()
                Do While Not RsBalance.EOF
                    If RsBalance.Fields("Category").Value = "G" Then
                        mAcctType = IIf(IsDbNull(RsBalance.Fields("ACCTTYPE").Value), -1, RsBalance.Fields("ACCTTYPE").Value)
                    End If

                    If RsBalance.Fields("Category").Value = "G" Then
                        If RowPos > 1 Then
                            .Row = RowPos - 1

                            .Col = ColCurrTotal
                            If CDbl(mAcctType) = 6 Or CDbl(mAcctType) = 1 Then
                                .Text = IIf(mCurrAmount <= 0, VB6.Format(System.Math.Abs(mCurrAmount), "##,##,##,##,###"), VB6.Format(System.Math.Abs(mCurrAmount), "(##,##,##,##,###)"))
                            Else
                                .Text = IIf(mCurrAmount >= 0, VB6.Format(System.Math.Abs(mCurrAmount), "##,##,##,##,###"), VB6.Format(System.Math.Abs(mCurrAmount), "(##,##,##,##,###)"))
                            End If

                            .Col = ColPrevTotal
                            If CDbl(mAcctType) = 6 Or CDbl(mAcctType) = 1 Then
                                .Text = IIf(mPrevAmount <= 0, VB6.Format(System.Math.Abs(mPrevAmount), "##,##,##,##,###"), VB6.Format(System.Math.Abs(mPrevAmount), "(##,##,##,##,###)"))
                            Else
                                .Text = IIf(mPrevAmount >= 0, VB6.Format(System.Math.Abs(mPrevAmount), "##,##,##,##,###"), VB6.Format(System.Math.Abs(mPrevAmount), "(##,##,##,##,###)")) 'Format(Abs(mPrevAmount), "##,##,##,##,###")
                            End If

                            mCurrAmount = 0
                            mPrevAmount = 0
                        End If

                        .Row = RowPos
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsBalance.Fields("Name").Value), "", RsBalance.Fields("Name").Value)
                        .Font = VB6.FontChangeBold(.Font, True)


                    Else
                        .Row = RowPos
                        .Col = ColDesc
                        .Text = "     " & IIf(IsDbNull(RsBalance.Fields("Name").Value), "", RsBalance.Fields("Name").Value)
                        .Font = VB6.FontChangeBold(.Font, False)
                    End If

                    .Col = ColSchd
                    If RsBalance.Fields("Category").Value = "G" Then
                        .Text = IIf(IsDbNull(RsBalance.Fields("SCHEDULENO").Value) Or RsBalance.Fields("SCHEDULENO").Value = 0, "", RsBalance.Fields("SCHEDULENO").Value)
                    Else
                        .Text = ""
                    End If

                    .Col = ColCode
                    .Text = CStr(IIf(IsDbNull(RsBalance.Fields("Code").Value), "", RsBalance.Fields("Code").Value))

                    .Col = ColCategory
                    .Text = IIf(IsDbNull(RsBalance.Fields("Category").Value), "", RsBalance.Fields("Category").Value)

                    mCurrSubAmount = IIf(IsDbNull(RsBalance.Fields("CURRENTFYRAMT").Value), 0, RsBalance.Fields("CURRENTFYRAMT").Value)
                    mPrevSubAmount = IIf(IsDbNull(RsBalance.Fields("PREVIOUSFYAMT").Value), 0, RsBalance.Fields("PREVIOUSFYAMT").Value)

                    .Col = ColCurrSubTotal
                    If CDbl(mAcctType) = 6 Or CDbl(mAcctType) = 1 Then
                        .Text = IIf(mCurrSubAmount <= 0, VB6.Format(System.Math.Abs(mCurrSubAmount), "##,##,##,##,###"), VB6.Format(System.Math.Abs(mCurrSubAmount), "(##,##,##,##,###)"))
                    Else
                        .Text = IIf(mCurrSubAmount >= 0, VB6.Format(System.Math.Abs(mCurrSubAmount), "##,##,##,##,###"), VB6.Format(System.Math.Abs(mCurrSubAmount), "(##,##,##,##,###)")) 'Format(Abs(mCurrSubAmount), "##,##,##,##,###")
                    End If

                    .Col = ColCurrTotal
                    .Text = ""
                    '                .Text = Format(Abs(mCurrAmount), "##,##,##,##,###")
                    '
                    .Col = ColPrevSubTotal
                    If CDbl(mAcctType) = 6 Or CDbl(mAcctType) = 1 Then
                        .Text = IIf(mPrevSubAmount <= 0, VB6.Format(System.Math.Abs(mPrevSubAmount), "##,##,##,##,###"), VB6.Format(System.Math.Abs(mPrevSubAmount), "(##,##,##,##,###)")) '
                    Else
                        .Text = IIf(mPrevSubAmount >= 0, VB6.Format(System.Math.Abs(mPrevSubAmount), "##,##,##,##,###"), VB6.Format(System.Math.Abs(mPrevSubAmount), "(##,##,##,##,###)")) ' Format(Abs(mPrevSubAmount), "##,##,##,##,###")
                    End If

                    .Col = ColPrevTotal
                    .Text = ""
                    '                .Text = Format(Abs(mPrevAmount), "##,##,##,##,###")

                    mCurrTotalAmount = mCurrTotalAmount + mCurrSubAmount
                    mPrevTotalAmount = mPrevTotalAmount + mPrevSubAmount

                    mCurrAmount = mCurrAmount + mCurrSubAmount
                    mPrevAmount = mPrevAmount + mPrevSubAmount

                    RsBalance.MoveNext()

                    RowPos = RowPos + 1
                    .MaxRows = RowPos

                Loop

                .Row = RowPos - 1
                .Col = ColCurrTotal
                .Text = VB6.Format(System.Math.Abs(mCurrAmount), "##,##,##,##,###")

                .Col = ColPrevTotal
                .Text = VB6.Format(System.Math.Abs(mPrevAmount), "##,##,##,##,###")

                mCurrAmount = 0
                mPrevAmount = 0

                .Row = .MaxRows
                .Col = ColDesc
                .Text = " "

                .MaxRows = .MaxRows + 1

                .Row = .MaxRows
                .Col = ColDesc
                .Text = "Grand Total :"
                .Font = VB6.FontChangeBold(.Font, True)

                .Col = ColCurrTotal
                .Text = VB6.Format(System.Math.Abs(mCurrTotalAmount), "##,##,##,##,###")

                .Col = ColPrevTotal
                .Text = VB6.Format(System.Math.Abs(mPrevTotalAmount), "##,##,##,##,###")


            End With
        End If
        Exit Sub
RefreshErr:
        PubDBCn.RollbackTrans()
InsertErr:
        MsgInformation(Err.Description)
    End Sub
    Private Function FillIntoTempTRNQry() As Boolean

        On Error GoTo ViewTrialErr
        Dim mSqlStr As String
        Dim Sqlstr As String
        Dim mType As String
        Dim mCurrPnL As Double
        Dim mFinalSheet As Boolean

        ' select distinct GROUP_TYPE from fin_group_mst;


        If CDate(txtDateTo.Text) = CDate(RsCompany.Fields("END_DATE").Value) Then
            mFinalSheet = True
        Else
            mFinalSheet = False
        End If

        mType = "G"
        If MainClass.ValidateWithMasterTable(Val(CboScheduleNo.Text), "GROUP_SCHEDULENO", "GROUP_TYPE", "FIN_GROUP_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mType = MasterNo
        End If

        If mType = "G" Then
            mCurrPnL = GetCurrentProfit
        End If

        Sqlstr = "DELETE FROM TEMP_TRN NOLOGGING " ''& vbCrLf |            & " WHERE UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(Sqlstr)

        mSqlStr = "INSERT INTO TEMP_TRN (" & vbCrLf & " USERID, COMPANY_CODE, CODE, NAME," & vbCrLf & " GROUPCODE, CATEGORY, " & vbCrLf & " PREVIOUSFYAMT, CURRENTFYRAMT)"

        ''********SELECTION..........
        Sqlstr = "SELECT  " & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',   " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf & " '' || ACM.SUPP_CUST_CODE, " & vbCrLf & " ACM.SUPP_CUST_NAME, " & vbCrLf & " CASE WHEN SUM(AMOUNT * DECODE(DC,'D',1,-1))>=0 THEN ACM.GROUPCODE ELSE ACM.GROUPCODECR END AS GCODE, " & vbCrLf & " ACM.SUPP_CUST_TYPE, "

        If mType = "E" Then
            Sqlstr = Sqlstr & vbCrLf & " GETPROFITLOSS(" & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value - 1 & " , ACM.SUPP_CUST_CODE) AS OP, "
        Else
            Sqlstr = Sqlstr & vbCrLf & " SUM(CASE WHEN TRN.FYEAR  = " & RsCompany.Fields("FYEAR").Value & " AND VDATE< TO_DATE('" & VB6.Format(RsCompany.Fields("START_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN AMOUNT * DECODE(DC,'D',1,-1) ELSE 0 END) OP, "
        End If

        Sqlstr = Sqlstr & vbCrLf & " SUM(CASE WHEN TRN.FYEAR  = " & RsCompany.Fields("FYEAR").Value & " THEN AMOUNT * DECODE(DC,'D',1,-1) ELSE 0 END)  + DECODE(ACM.HEADTYPE,'P'," & mCurrPnL & ",0)"

        ''********TABLEs..........
        Sqlstr = Sqlstr & vbCrLf & " FROM FIN_POSTED_TRN TRN, " & vbCrLf & " FIN_SUPP_CUST_MST ACM "


        ''********Joining..........
        Sqlstr = Sqlstr & vbCrLf & " WHERE " & vbCrLf & " TRN.COMPANY_CODE=ACM.COMPANY_CODE " & vbCrLf & " AND TRN.ACCOUNTCODE=ACM.SUPP_CUST_CODE "

        ''********Conditions..........

        Sqlstr = Sqlstr & vbCrLf & " AND TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""

        Sqlstr = Sqlstr & vbCrLf & " AND TRN.FYEAR IN (" & RsCompany.Fields("FYEAR").Value & "," & RsCompany.Fields("FYEAR").Value - 1 & ")" & vbCrLf & " AND TRN.VDate <= TO_DATE('" & VB6.Format(txtDateTo.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY')"

        '    ''********GROUP BY CLAUSE..........
        '    SqlStr = SqlStr & vbCrLf & " GROUP BY " & vbCrLf _
        ''            & " ACM.SUPP_CUST_CODE,ACM.SUPP_CUST_NAME,ACM.GROUPCODE,ACM.GROUPCODECR, " & vbCrLf _
        ''            & " ACM.SUPP_CUST_TYPE "
        '
        '    ''********ORDER BY CLAUSE..........
        '     SqlStr = SqlStr & vbCrLf & " ORDER BY " & vbCrLf _
        ''            & " ACM.SUPP_CUST_CODE "
        '
        '
        '
        '    SqlStr = mSqlStr & vbCrLf & SqlStr
        '    PubDBCn.Execute SqlStr

        Sqlstr = Sqlstr & vbCrLf & " AND TRN.PL_FLAG='N' " '''Expenses Adjust Amount not Consider..

        If mFinalSheet = False Then
            Sqlstr = Sqlstr & vbCrLf & " AND TRN.ACCOUNTCODE NOT IN ( " & vbCrLf & " SELECT DISTINCT ACCOUNTCODE  " & vbCrLf & " FROM FIN_PROFITLOSS_TRN " & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR IN (" & RsCompany.Fields("FYEAR").Value & ", " & RsCompany.Fields("FYEAR").Value - 1 & ") " & vbCrLf & " AND VDate = TO_DATE('" & VB6.Format(txtDateTo.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY')" & vbCrLf & " AND TRN.PL_FLAG='N' )"
        End If

        ''********GROUP BY CLAUSE..........
        Sqlstr = Sqlstr & vbCrLf & " GROUP BY " & vbCrLf & " ACM.SUPP_CUST_CODE,ACM.SUPP_CUST_NAME,ACM.GROUPCODE,ACM.GROUPCODECR, " & vbCrLf & " ACM.SUPP_CUST_TYPE,ACM.HEADTYPE" '', ACM.HEADTYPE

        ''********ORDER BY CLAUSE..........
        Sqlstr = Sqlstr & vbCrLf & " ORDER BY " & vbCrLf & " ACM.SUPP_CUST_CODE "


        Sqlstr = mSqlStr & vbCrLf & Sqlstr
        PubDBCn.Execute(Sqlstr)

        ''From Addition Voucher

        Sqlstr = ""
        If mFinalSheet = False Then
            ''********SELECTION..........
            Sqlstr = "SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ",   " & vbCrLf & " '' || ACM.SUPP_CUST_CODE, " & vbCrLf & " ACM.SUPP_CUST_NAME, " & vbCrLf & " CASE WHEN SUM(AMOUNT * DECODE(DC,'D',1,-1))>=0 THEN ACM.GROUPCODE ELSE ACM.GROUPCODECR END AS GCODE, " & vbCrLf & " ACM.SUPP_CUST_TYPE, "

            If mType = "E" Then
                Sqlstr = Sqlstr & vbCrLf & " GETPROFITLOSS(" & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value - 1 & " , ACM.SUPP_CUST_CODE) AS OP, "
            Else
                Sqlstr = Sqlstr & vbCrLf & " SUM(CASE WHEN TRN.FYEAR  = " & RsCompany.Fields("FYEAR").Value & " AND VDATE< TO_DATE('" & VB6.Format(RsCompany.Fields("START_DATE").Value, "DD-MMM-YYYY") & "','DD-MON-YYYY') THEN AMOUNT * DECODE(DC,'D',1,-1) ELSE 0 END) OP, "
            End If


            '    If chkOpening.Value = vbChecked Then
            Sqlstr = Sqlstr & vbCrLf & " SUM(CASE WHEN TRN.FYEAR  = " & RsCompany.Fields("FYEAR").Value & " THEN AMOUNT * DECODE(DC,'D',1,-1) ELSE 0 END)  AS CURRFY"
            '    Else
            '        SqlStr = SqlStr & vbCrLf _
            ''            & " SUM(CASE WHEN TRN.VDate >= TO_DATE('" & vb6.Format(txtDateFrom.Text, "dd-mmm-yyyy") & "') THEN AMOUNT * DECODE(DC,'D',1,-1) ELSE 0 END)  AS CURRFY"
            '    End If

            ''********TABLEs..........
            Sqlstr = Sqlstr & vbCrLf & " FROM FIN_PROFITLOSS_TRN TRN, " & vbCrLf & " FIN_SUPP_CUST_MST ACM "


            '    ''********Joining..........
            Sqlstr = Sqlstr & vbCrLf & " WHERE " & vbCrLf & " ACM.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.COMPANY_CODE=ACM.COMPANY_CODE " & vbCrLf & " AND TRN.ACCOUNTCODE=ACM.SUPP_CUST_CODE "


            Sqlstr = Sqlstr & vbCrLf & " AND TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.FYEAR IN (" & RsCompany.Fields("FYEAR").Value & ", " & RsCompany.Fields("FYEAR").Value - 1 & ")" & vbCrLf & " AND TRN.VDate = TO_DATE('" & VB6.Format(txtDateTo.Text, "dd-mmm-yyyy") & "','DD-MON-YYYY')"

            Sqlstr = Sqlstr & vbCrLf & " AND TRN.PL_FLAG='N' " '''Expenses Adjust Amount not Consider..


            ''********GROUP BY CLAUSE..........
            Sqlstr = Sqlstr & vbCrLf & " GROUP BY " & vbCrLf & " ACM.SUPP_CUST_CODE,ACM.SUPP_CUST_NAME,ACM.GROUPCODE,ACM.GROUPCODECR, " & vbCrLf & " ACM.SUPP_CUST_TYPE "

            ''********ORDER BY CLAUSE..........
            Sqlstr = Sqlstr & vbCrLf & " ORDER BY " & vbCrLf & " ACM.SUPP_CUST_CODE "
            Sqlstr = mSqlStr & vbCrLf & Sqlstr
            PubDBCn.Execute(Sqlstr)
        End If

        FillIntoTempTRNQry = True
        Exit Function
ViewTrialErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '''Resume

        FillIntoTempTRNQry = False

    End Function

    Private Function GetCurrentProfit() As Double

        On Error GoTo ErrPart
        Dim Sqlstr As String
        Dim RsOPBalSumm As ADODB.Recordset

        Dim mFinalSheet As Boolean

        ' select distinct GROUP_TYPE from fin_group_mst;


        If CDate(txtDateTo.Text) = CDate(RsCompany.Fields("END_DATE").Value) Then
            mFinalSheet = True
        Else
            mFinalSheet = False
        End If

        Sqlstr = " Select SUM(DECODE(DC,'D',1,-1)*Amount) AS BALANCE " & vbCrLf & " FROM FIN_POSTED_TRN TRN, FIN_SUPP_CUST_MST ACM, FIN_GROUP_MST ACMGROUP" & vbCrLf & " WHERE TRN.Company_Code=" & RsCompany.Fields("Company_Code").Value & "" & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND TRN.COMPANY_CODE=ACM.COMPANY_CODE" & vbCrLf & " AND TRN.ACCOUNTCODE=ACM.SUPP_CUST_CODE" & vbCrLf & " AND ACM.COMPANY_CODE=ACMGROUP.COMPANY_CODE" & vbCrLf & " AND ACM.GROUPCODE=ACMGROUP.GROUP_CODE" & vbCrLf & " AND ACMGROUP.GROUP_TYPE='E' AND TRN.PL_FLAG='N'"

        '    If chkOpening.Value = vbChecked Then
        Sqlstr = Sqlstr & vbCrLf & " AND TRN.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
        '    Else
        '        SqlStr = SqlStr & vbCrLf & " AND TRN.VDATE>='" & vb6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "' "
        '        SqlStr = SqlStr & vbCrLf & " AND TRN.VDATE<='" & vb6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "' "
        '    End If

        MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOPBalSumm, ADODB.LockTypeEnum.adLockReadOnly)

        If RsOPBalSumm.EOF = False Then
            GetCurrentProfit = IIf(IsDbNull(RsOPBalSumm.Fields("BALANCE").Value), 0, RsOPBalSumm.Fields("BALANCE").Value)
        End If

        ''From Addition Voucher

        Sqlstr = ""

        If mFinalSheet = False Then
            ''********SELECTION..........
            Sqlstr = "SELECT SUM(DECODE(DC,'D',1,-1)*Amount) AS BALANCE " & vbCrLf & " FROM FIN_PROFITLOSS_TRN TRN, FIN_SUPP_CUST_MST ACM, FIN_GROUP_MST ACMGROUP" & vbCrLf & " WHERE TRN.Company_Code=" & RsCompany.Fields("Company_Code").Value & "" & vbCrLf & " AND TRN.FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND TRN.COMPANY_CODE=ACM.COMPANY_CODE" & vbCrLf & " AND TRN.ACCOUNTCODE=ACM.SUPP_CUST_CODE" & vbCrLf & " AND ACM.COMPANY_CODE=ACMGROUP.COMPANY_CODE" & vbCrLf & " AND ACM.GROUPCODE=ACMGROUP.GROUP_CODE" & vbCrLf & " AND ACMGROUP.GROUP_TYPE='E' AND TRN.PL_FLAG='N'"

            '    If chkOpening.Value = vbChecked Then
            Sqlstr = Sqlstr & vbCrLf & " AND TRN.VDATE<=TO_DATE('" & VB6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY') "
            '    Else
            '        SqlStr = SqlStr & vbCrLf & " AND TRN.VDATE>='" & vb6.Format(txtDateFrom.Text, "DD-MMM-YYYY") & "' "
            '        SqlStr = SqlStr & vbCrLf & " AND TRN.VDATE<='" & vb6.Format(txtDateTo.Text, "DD-MMM-YYYY") & "' "
            '    End If

            MainClass.UOpenRecordSet(Sqlstr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsOPBalSumm, ADODB.LockTypeEnum.adLockReadOnly)

            If RsOPBalSumm.EOF = False Then
                GetCurrentProfit = GetCurrentProfit + IIf(IsDbNull(RsOPBalSumm.Fields("BALANCE").Value), 0, RsOPBalSumm.Fields("BALANCE").Value)
            End If
        End If


        Exit Function
ErrPart:
        GetCurrentProfit = 0
    End Function

    Private Function InsertIntoBS() As Boolean

        On Error GoTo ErrPart
        Dim Sqlstr As String
        Dim SqlStrBS As String
        Dim SqlStrACMG As String
        Dim SqlStrTRN As String
        Dim mSqlStr As String


        Sqlstr = "DELETE FROM TEMP_BALANCESHEET NOLOGGING " & vbCrLf & " WHERE UserID='" & MainClass.AllowSingleQuote(PubUserID) & "'"
        PubDBCn.Execute(Sqlstr)

        Sqlstr = ""
        mSqlStr = "INSERT INTO TEMP_BALANCESHEET (" & vbCrLf & " USERID, CODE, NAME," & vbCrLf & " PARENTCODE, BSCODEDR, BSCODECR," & vbCrLf & " CATEGORY, ACCTTYPE, SCHEDULENO," & vbCrLf & " PREVIOUSFYAMT, CURRENTFYRAMT,SEQ_NO )"

        SqlStrACMG = ACMGROUPQry
        SqlStrTRN = TRNQry

        Sqlstr = ""
        Sqlstr = mSqlStr & vbCrLf & SqlStrACMG
        PubDBCn.Execute(Sqlstr)

        Sqlstr = ""
        Sqlstr = mSqlStr & vbCrLf & SqlStrTRN
        PubDBCn.Execute(Sqlstr)

        InsertIntoBS = True

        Exit Function
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        InsertIntoBS = False
    End Function

    Private Function ACMGROUPQry() As String

        On Error GoTo ViewTrialErr
        Dim Sqlstr As String

        ''********SELECTION..........
        Sqlstr = "SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " ACMGROUP.GROUP_CATEGORY || ACMGROUP.GROUP_CODE, ACMGROUP.GROUP_NAME, " & vbCrLf & " DECODE(ACMGROUP.GROUP_PARENTCODE,-1,'-1', 'G' || ACMGROUP.GROUP_PARENTCODE), " & vbCrLf & " DECODE(ACMGROUP.GROUP_PARENTCODE,-1,'-1','G' || ACMGROUP.GROUP_PARENTCODE), " & vbCrLf & " DECODE(ACMGROUP.GROUP_PARENTCODE,-1,'-1','G' || ACMGROUP.GROUP_PARENTCODE), " & vbCrLf & " ACMGROUP.GROUP_CATEGORY,BSGROUP.BSGROUP_ACCTTYPE,ACMGROUP.GROUP_SCHEDULENO, 0, 0,ACMGROUP.GROUP_SEQNO "

        ''********TABLEs..........
        Sqlstr = Sqlstr & vbCrLf & " FROM FIN_GROUP_MST ACMGROUP, FIN_BSGROUP_MST BSGROUP "

        ''********JOINING..........
        Sqlstr = Sqlstr & vbCrLf & " WHERE ACMGROUP.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND ACMGROUP.COMPANY_CODE=BSGROUP.COMPANY_CODE AND ACMGROUP.GROUP_BSCODEDR=BSGROUP.BSGROUP_CODE"

        '     ''********WHERE CLAUSE..........
        Sqlstr = Sqlstr & vbCrLf & " AND ACMGROUP.GROUP_SCHEDULENO=" & Val(CboScheduleNo.Text) & " "

        ''********ORDER BY CLAUSE..........
        Sqlstr = Sqlstr & vbCrLf & " ORDER BY " & vbCrLf & " ACMGROUP.GROUP_NAME "


        ACMGROUPQry = Sqlstr
        Exit Function
ViewTrialErr:
        ACMGROUPQry = ""
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '''Resume
    End Function
    Private Function TRNQry() As String

        On Error GoTo ViewTrialErr

        Dim Sqlstr As String


        ''********SELECTION..........
        Sqlstr = "SELECT '" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf & " '' || TRN.CODE,  " & vbCrLf & " TRN.NAME, " & vbCrLf & " 'G' || TRN.GROUPCODE, " & vbCrLf & " 'G' || TRN.GROUPCODE, " & vbCrLf & " 'G' || TRN.GROUPCODE, " & vbCrLf & " TRN.CATEGORY, -1, ACMGROUP.GROUP_SCHEDULENO, " & vbCrLf & " PREVIOUSFYAMT, " & vbCrLf & " CURRENTFYRAMT,-1 "


        ''********TABLEs..........
        Sqlstr = Sqlstr & vbCrLf & " FROM TEMP_TRN TRN, " & vbCrLf & " FIN_GROUP_MST ACMGROUP "


        ''********Joining..........
        Sqlstr = Sqlstr & vbCrLf & " WHERE " & vbCrLf & " TRN.USERID='" & MainClass.AllowSingleQuote(PubUserID) & "'" & vbCrLf & " AND ACMGROUP.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TRN.GROUPCODE=ACMGROUP.GROUP_CODE "

        ''********WHERE CLAUSE..........

        Sqlstr = Sqlstr & vbCrLf & " AND ACMGROUP.GROUP_SCHEDULENO='" & CboScheduleNo.Text & "' "

        ''********ORDER BY CLAUSE..........
        Sqlstr = Sqlstr & vbCrLf & " ORDER BY " & vbCrLf & " TRN.NAME "

        TRNQry = Sqlstr
        Exit Function
ViewTrialErr:
        TRNQry = ""
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '''Resume
    End Function

    Private Sub frmBSSchedule_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ERR1
        Dim Sqlstr As String
        ''Set PvtDBCn = New ADODB.Connection
        ''PvtDBCn.Open StrConn

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        FormLoaded = True ''false
        Me.Top = 0 ''495
        Me.Left = 0
        Me.Width = VB6.TwipsToPixelsX(11370)
        Me.Height = VB6.TwipsToPixelsY(7230)
        txtDateTo.Text = VB6.Format(RunDate, "DD/MM/YYYY")

        MainClass.SetControlsColor(Me)
        CallFillCombo()
        FillHeading()
        FormatSprdView()
        PrintCommand()


        SprdView.PrintMarginTop = 0.75 * 1440
        SprdView.PrintMarginBottom = 0.75 * 1440
        SprdView.PrintMarginLeft = 0.5 * 1440
        SprdView.PrintMarginRight = 0.5 * 1440

        'Init then zoom display
        zoomindex = 2 ''8   'page height

        PrintEnable = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
        'Resume
    End Sub


    Private Function CheckAcctType(ByRef mAccountCode As String, ByRef mAmount As Double) As String
        On Error GoTo ErrPart

        If MainClass.ValidateWithMasterTable(mAccountCode, "Code", "AcctType", "ACM", PubDBCn, MasterNo) = True Then
            If MasterNo = ConLiabilities Or MasterNo = ConPnLAcct Or MasterNo = ConIncome Then
                mAmount = mAmount * -1
            Else
                mAmount = mAmount
            End If
        End If
        If mAmount < 0 Then
            CheckAcctType = "(" & VB6.Format(System.Math.Abs(mAmount), "##,##,##,##,###") & ")"
        Else
            CheckAcctType = VB6.Format(mAmount, "##,##,##,##,###")
        End If
        Exit Function
ErrPart:

    End Function


    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        ViewAccountLedger()
    End Sub


    Private Sub SprdView_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles SprdView.KeyDownEvent
        If eventArgs.KeyCode = System.Windows.Forms.Keys.Return Then ViewAccountLedger()
    End Sub

    Private Sub txtDateTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDateTo.TextChanged
        PrintEnable = False
        PrintCommand()
    End Sub
    Private Sub PrintCommand()
        CmdPreview.Enabled = PrintEnable
        cmdPrint.Enabled = PrintEnable
    End Sub
    Private Sub FillHeading()
        With SprdView
            .Row = 0
            .set_RowHeight(0, 20)
            .MaxCols = ColCategory

            .Col = ColDesc
            .Text = "PARTICULAR"

            .Col = ColSchd
            .Text = "Sc. No."

            .Col = ColCurrSubTotal
            .Text = " "

            .Col = ColCurrTotal
            .Text = "AS ON " & VB6.Format(txtDateTo.Text, "DD/MM/YYYY")

            .Col = ColPrevSubTotal
            .Text = " "

            .Col = ColPrevTotal
            .Text = "AS ON " & DateAdd("d", -1, RsCompany.Fields("START_DATE").Value)

        End With

    End Sub



    Private Sub SprdCommand_ButtonClicked(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles SprdCommand.ButtonClicked
        On Error GoTo ERR1
        Dim mFilename As String

        SprdCommand.Col = eventArgs.col
        SprdCommand.Row = eventArgs.row

        If SprdCommand.CellType = FPSpreadADO.CellTypeConstants.CellTypeButton Then
            Select Case eventArgs.col
                Case 2 'Next
                    ShowNextPage(SprdView, SprdPreview, SprdCommand, eventArgs.col)

                Case 4 'Previous
                    ShowPreviousPage(SprdView, SprdPreview, SprdCommand, eventArgs.col)

                Case 6 'Zoom
                    SprdPreview.ZoomState = 3

                Case 8 'Print
                    cmdPrint_Click(cmdPrint, New System.EventArgs())

                Case 10 'Export
                    'mFilename = ExportSprdToExcel(CommonDialog1)

                    If SprdView.ExportToExcel(mFilename, "Schedule", "") = True Then
                        MsgInformation("Export Successfully Complete." & vbCrLf & vbCrLf & "Export File Name is " & mFilename)
                    End If
                Case 16 'Close
                    FraPreview.Visible = False
                    With SprdView
                        .set_ColWidth(ColDesc, 24)
                        .set_ColWidth(ColSchd, 4)
                        .set_ColWidth(ColCurrSubTotal, 9.5)
                        .set_ColWidth(ColCurrTotal, 9.5)
                        .set_ColWidth(ColPrevSubTotal, 9.5)
                        .set_ColWidth(ColPrevTotal, 9.5)
                    End With
            End Select
        End If
        Exit Sub
ERR1:
        If Err.Number = 32755 Then
            Exit Sub
        End If
        MsgInformation(Err.Description)
    End Sub

    Private Sub SprdCommand_TextTipFetch(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_TextTipFetchEvent) Handles SprdCommand.TextTipFetch
        With SprdCommand
            .Col = eventArgs.Col
            .Row = eventArgs.Row
            If .CellType = FPSpreadADO.CellTypeConstants.CellTypeButton And Not .Lock Then
                eventArgs.ShowTip = True
                eventArgs.TipText = .TypeButtonText
            ElseIf .CellType = FPSpreadADO.CellTypeConstants.CellTypeEdit And .Text <> "" Then
                eventArgs.ShowTip = True
                eventArgs.TipText = .Text
            End If
        End With
    End Sub
    Private Sub txtdateTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDateTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If txtDateTo.Text = "" Then
            MsgBox("Date Cannot Be Blank", MsgBoxStyle.Critical)
            Cancel = True
        ElseIf txtDateTo.Text <> "" Then
            If Not IsDate(txtDateTo.Text) Then
                MsgBox("Please enter vaild Date.", MsgBoxStyle.Critical)
                Cancel = True
            ElseIf FYChk(CStr(CDate(txtDateTo.Text))) = False Then
                Cancel = True
            End If
        End If
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub CallFillCombo()

        Dim cntRow As Short
        Dim mSqlStr As String
        Dim RsTemp As ADODB.Recordset

        CboScheduleNo.Items.Clear()

        mSqlStr = "SELECT DISTINCT GROUP_SCHEDULENO FROM FIN_GROUP_MST WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " ORDER BY GROUP_SCHEDULENO"
        MainClass.UOpenRecordSet(mSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = False Then
            Do While RsTemp.EOF = False
                CboScheduleNo.Items.Add(IIf(IsDbNull(RsTemp.Fields("GROUP_SCHEDULENO").Value), -1, RsTemp.Fields("GROUP_SCHEDULENO").Value))
                RsTemp.MoveNext()
            Loop
        End If

        '    For cntRow = 1 To 15
        '        CboScheduleNo.AddItem cntRow
        '    Next
        '    CboScheduleNo.AddItem "Other"

        Call MainClass.FillCombo(cboHead, "FIN_GROUP_MST", "UPPER(GROUP_NAME)", , "GROUP_CATEGORY='G'")
    End Sub


    Private Sub ViewAccountLedger()


        On Error GoTo ErrPart
        If SprdView.ActiveRow <= 0 Then Exit Sub
        SprdView.Row = SprdView.ActiveRow
        SprdView.Col = ColCategory
        If Trim(SprdView.Text) = "H" Or Trim(SprdView.Text) = "G" Or Trim(SprdView.Text) = "" Then Exit Sub

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        frmViewLedger.lblBookType.Text = "LEDG"
        '    frmViewLedger.Show
        SprdView.Row = SprdView.ActiveRow
        SprdView.Col = ColDesc
        frmViewLedger.cboAccount.Text = SprdView.Text
        MainClass.ValidateWithMasterTable(SprdView.Text, "Name", "Code", "ACM", PubDBCn, MasterNo)
        frmViewLedger.lblAcCode.Text = MasterNo

        frmViewLedger.txtDateFrom.Text = RsCompany.Fields("FYDateFrom").Value
        frmViewLedger.txtDateTo.Text = txtDateTo.Text
        frmViewLedger.OptSumDet(2).Checked = True
        'frmViewLedger.cboDivision.Text = cboDivision.Text
        frmViewLedger.MdiParent = Me.MdiParent
        frmViewLedger.Show()
        frmViewLedger.frmViewLedger_Activated(Nothing, New System.EventArgs())
        frmViewLedger.cmdShow_Click(Nothing, New System.EventArgs())

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox(Err.Description)
    End Sub
End Class
