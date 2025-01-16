Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmITForm16New
    Inherits System.Windows.Forms.Form
    Dim RsITMain As ADODB.Recordset
    Dim RsITDetail As ADODB.Recordset

    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection

    Dim mTDSAMT As Double

    Dim Shw As Boolean
    Dim xCode As String
    Dim SqlStr As String = ""
    Dim FormActive As Boolean
    Dim mPFAmt As Double
    Dim mGrossSal As Double
    Private Const ConRowHeight As Short = 12

    Private Const ColSNO As Short = 0
    Private Const ColDesc As Short = 1
    Private Const ColAmt1 As Short = 2
    Private Const ColAmt2 As Short = 3
    Private Const ColAmt3 As Short = 4
    Private Const ColTotal As Short = 5
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

        MainClass.ClearGrid(sprdIT)

        If RsCompany.Fields("FYEAR").Value <= 2006 Then
            FillSprdGrid()
            CellFormat()
        ElseIf RsCompany.Fields("FYEAR").Value < 2010 Then
            FillSprdGrid()
            CellFormatNew()
        Else
            FillSprdGrid2010()
            CellFormat2010()
        End If

        txtEmpCode.Text = ""
        txtName.Text = ""
        txtDesignation.Text = ""
        txtPan.Text = ""
        txtTan.Text = ""
        txtEmpPan.Text = ""
        txtTDS.Text = ""

        txtPan.Text = IIf(IsDbNull(RsCompany.Fields("PAN_NO").Value), "", RsCompany.Fields("PAN_NO").Value)
        txtTan.Text = IIf(IsDbNull(RsCompany.Fields("TDSACNO").Value), "", RsCompany.Fields("TDSACNO").Value)
        txtTDS.Text = IIf(IsDbNull(RsCompany.Fields("TDSCIRCLE").Value), "", RsCompany.Fields("TDSCIRCLE").Value)

        txtPrevChallan.Text = ""
        txtPrevSalary.Text = ""

        MainClass.ButtonStatus(Me, XRIGHT, RsITMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
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
    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
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
            txtName.Text = AcName1
            txtEmpCode_Validating(txtEmpCode, New System.ComponentModel.CancelEventArgs(False))
            If txtDesignation.Enabled = True Then txtDesignation.Focus()
        End If
        Exit Sub

    End Sub
    Private Sub frmITForm16New_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        MainClass.DoFunctionKey(Me, KeyCode)
    End Sub

    Private Sub sprdIT_Change(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles sprdIT.Change

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub
    Private Sub sprdIT_LeaveCell(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles sprdIT.LeaveCell
        On Error GoTo ErrPart


        If eventArgs.NewRow = -1 Then Exit Sub
        sprdIT.Row = eventArgs.row
        If RsCompany.Fields("FYEAR").Value < 2010 Then
            CalcGrid()
        Else
            CalcGrid2010()
        End If
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        'Resume
    End Sub
    Private Sub frmITForm16New_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim mRegdAddress As String

        SqlStr = ""
        If FormActive = True Then Exit Sub
        SqlStr = "Select * From PAY_ITFORM16_HDR Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsITMain, ADODB.LockTypeEnum.adLockOptimistic)

        AssignGrid(False)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        txtFrom.Text = RsCompany.Fields("START_DATE").Value
        txtTo.Text = RsCompany.Fields("END_DATE").Value
        txtAYear.Text = Year(RsCompany.Fields("START_DATE").Value) + 1 & "-" & Year(RsCompany.Fields("END_DATE").Value) + 1
        txtCompanyName.Text = RsCompany.Fields("Company_Name").Value

        mRegdAddress = IIf(IsDbNull(RsCompany.Fields("REGD_ADDR1").Value), "", RsCompany.Fields("REGD_ADDR1").Value)
        mRegdAddress = mRegdAddress & " " & IIf(IsDbNull(RsCompany.Fields("REGD_ADDR2").Value), "", RsCompany.Fields("REGD_ADDR2").Value)
        mRegdAddress = mRegdAddress & " " & IIf(IsDbNull(RsCompany.Fields("REGD_CITY").Value), "", RsCompany.Fields("REGD_CITY").Value)
        mRegdAddress = mRegdAddress & " " & IIf(IsDbNull(RsCompany.Fields("REGD_STATE").Value), "", RsCompany.Fields("REGD_STATE").Value)
        mRegdAddress = mRegdAddress & " - " & IIf(IsDbNull(RsCompany.Fields("REGD_PIN").Value), "", RsCompany.Fields("REGD_PIN").Value)

        txtCompanyAdd.Text = mRegdAddress ''IIf(IsNull(RsCompany!COMPANY_ADDR), "", RsCompany!COMPANY_ADDR) & IIf(IsNull(RsCompany!COMPANY_CITY), "", ", " & RsCompany!COMPANY_CITY)

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
    Private Sub frmITForm16New_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

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

        FormatSprd(-1)
        If RsCompany.Fields("FYEAR").Value <= 2006 Then
            FillSprdGrid()
        ElseIf RsCompany.Fields("FYEAR").Value < 2010 Then
            FillSprdGrid()
        Else
            FillSprdGrid2010()
        End If
        'CellFormat
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub
    Private Sub frmITForm16New_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        'PvtDBCn.Cancel
        'PvtDBCn.Close
        RsITMain = Nothing
        'Set PvtDBCn = Nothing
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart
        Dim cntRow As Integer

        If RsITMain.EOF = False Then
            With RsITMain
                txtAYear.Text = .Fields("AYEAR").Value
                txtCompanyName.Text = RsCompany.Fields("Company_Name").Value
                txtCompanyAdd.Text = IIf(IsDbNull(RsCompany.Fields("COMPANY_ADDR").Value), "", RsCompany.Fields("COMPANY_ADDR").Value) & IIf(IsDbNull(RsCompany.Fields("COMPANY_CITY").Value), "", ", " & RsCompany.Fields("COMPANY_CITY").Value)

                txtPan.Text = IIf(IsDbNull(RsCompany.Fields("PAN_NO").Value), "", RsCompany.Fields("PAN_NO").Value)
                txtTan.Text = IIf(IsDbNull(RsCompany.Fields("TDSACNO").Value), "", RsCompany.Fields("TDSACNO").Value)
                txtTDS.Text = IIf(IsDbNull(RsCompany.Fields("TDSCIRCLE").Value), "", RsCompany.Fields("TDSCIRCLE").Value)

                '            If MainClass.ValidateWithMasterTable(!EMP_CODE, "DESG_CODE", "DESG_DESC", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                '                txtDesignation.Text = IIf(IsNull(MasterNo), "", MasterNo)
                '            End If
                '
                '            txtEmpPan.Text = IIf(IsNull(!EMPPANNO), "", !EMPPANNO)
                txtFrom.Text = .Fields("FROMDATE").Value
                txtTo.Text = .Fields("TODATE").Value
                '            txtTDS.Text = IIf(IsNull(!TDSCIRCLE), "", !TDSCIRCLE)
                txtPrevChallan.Text = IIf(IsDbNull(.Fields("PRECHALLAN").Value), "", .Fields("PRECHALLAN").Value)
                txtPrevSalary.Text = IIf(IsDbNull(.Fields("PRESALARY").Value), "", VB6.Format(.Fields("PRESALARY").Value, "0.00"))




                Call ShowDetail1(.Fields("EMP_CODE").Value)
            End With
        End If
        Shw = True


        Shw = False
        ADDMode = False
        MODIFYMode = False

        MainClass.ButtonStatus(Me, XRIGHT, RsITMain, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
        'Resume
    End Sub

    Private Sub ShowDetail1(ByRef xEmpCode As String)

        On Error GoTo ShowErrPart
        Dim cntRow As Integer

        SqlStr = " SELECT * " & vbCrLf & " FROM PAY_ITFORM16_DET WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND EMP_CODE='" & xEmpCode & "'" & vbCrLf & " ORDER BY SUBROW"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsITDetail, ADODB.LockTypeEnum.adLockReadOnly)

        If RsITDetail.EOF = False Then
            With RsITDetail
                cntRow = 1
                Do While Not RsITDetail.EOF
                    sprdIT.Row = cntRow
                    sprdIT.Col = ColDesc
                    sprdIT.Text = IIf(IsDbNull(.Fields("Description").Value), "", .Fields("Description").Value)
                    sprdIT.Col = ColAmt1
                    sprdIT.Text = CStr(IIf(.Fields("AMOUNT1").Value = 0, "", .Fields("AMOUNT1").Value))
                    sprdIT.Col = ColAmt2
                    sprdIT.Text = CStr(IIf(.Fields("AMOUNT2").Value = 0, "", .Fields("AMOUNT2").Value))
                    sprdIT.Col = ColAmt3
                    sprdIT.Text = CStr(IIf(.Fields("AMOUNT3").Value = 0, "", .Fields("AMOUNT3").Value))
                    sprdIT.Col = ColTotal
                    sprdIT.Text = CStr(IIf(.Fields("TotalAmount").Value = 0, "", .Fields("TotalAmount").Value))
                    cntRow = cntRow + 1
                    RsITDetail.MoveNext()
                Loop
            End With
        End If
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
        'Resume
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
        Dim mTAX_PAYABLE As Double

        If MainClass.ValidateWithMasterTable((txtEmpCode.Text), "EMP_CODE", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCode = MasterNo
        Else
            MsgInformation("Employee Name is not exsits in Master.")
            Update1 = False
            Exit Function
        End If

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        sprdIT.Col = sprdIT.MaxCols
        sprdIT.Row = sprdIT.MaxRows
        mTAX_PAYABLE = Val(sprdIT.Text)

        SqlStr = " DELETE FROM PAY_ITFORM16_DET WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " Emp_Code='" & mCode & "' "
        PubDBCn.Execute(SqlStr)

        If ADDMode = True Then
            SqlStr = "INSERT INTO PAY_ITFORM16_HDR ( " & vbCrLf & " COMPANY_CODE, FYEAR, EMP_CODE,  " & vbCrLf & " EMPPANNO, TDSCIRCLE, FROMDATE, TODATE,  " & vbCrLf & " AYEAR, TAX_PAYABLE, " & vbCrLf & " PRESALARY, PRECHALLAN, " & vbCrLf & " ADDUSER, ADDDATE ) " & vbCrLf & " VALUES ( "

            SqlStr = SqlStr & vbCrLf & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & ", '" & Trim(txtEmpCode.Text) & "',  " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtEmpPan.Text)) & "', '" & MainClass.AllowSingleQuote((txtTDS.Text)) & "'," & vbCrLf & " TO_DATE('" & VB6.Format(txtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " '" & MainClass.AllowSingleQuote((txtAYear.Text)) & "', " & Val(CStr(mTAX_PAYABLE)) & "," & vbCrLf & " " & Val(txtPrevSalary.Text) & ", " & Val(txtPrevChallan.Text) & "," & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'))"

        Else

            SqlStr = "UPDATE PAY_ITFORM16_HDR SET " & vbCrLf & " EMPPANNO='" & MainClass.AllowSingleQuote(txtEmpPan.Text) & "', " & vbCrLf & " TDSCIRCLE='" & MainClass.AllowSingleQuote(txtTDS.Text) & "', " & vbCrLf & " FROMDATE=TO_DATE('" & VB6.Format(txtFrom.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf & " TODATE=TO_DATE('" & VB6.Format(txtTo.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'),  " & vbCrLf & " AYEAR='" & MainClass.AllowSingleQuote(txtAYear.Text) & "', " & vbCrLf & " TAX_PAYABLE=" & Val(CStr(mTAX_PAYABLE)) & ", " & vbCrLf & " PRESALARY=" & Val(txtPrevSalary.Text) & ", " & vbCrLf & " PRECHALLAN=" & Val(txtPrevChallan.Text) & ", " & vbCrLf & " ADDUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', ADDDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') " & vbCrLf & " WHERE COMPANY_CODE= " & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR= " & RsCompany.Fields("FYEAR").Value & " " & vbCrLf & " AND EMP_CODE='" & Trim(txtEmpCode.Text) & "'"

        End If

        PubDBCn.Execute(SqlStr)

        If UpdateDetail = False Then GoTo UpdateError
        PubDBCn.CommitTrans()
        RsITMain.Requery()
        Update1 = True
        Exit Function
UpdateError:
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

    Private Function UpdateDetail() As Boolean

        On Error GoTo UpdateError
        Dim mCode As String
        Dim cntRow As Integer
        Dim mDesc As Object
        Dim mAmount1 As Double
        Dim mAmount2 As Double
        Dim mAmount3 As Double
        Dim TotalAmount As Double

        With sprdIT
            For cntRow = 1 To .MaxRows
                .Row = cntRow

                .Col = ColDesc
                mDesc = MainClass.AllowSingleQuote(.Text)

                .Col = ColAmt1
                mAmount1 = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColAmt2
                mAmount2 = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColAmt3
                mAmount3 = IIf(IsNumeric(.Text), .Text, 0)

                .Col = ColTotal
                TotalAmount = IIf(IsNumeric(.Text), .Text, 0)

                SqlStr = " INSERT INTO PAY_ITFORM16_DET " & vbCrLf & " ( COMPANY_CODE , FYEAR, EMP_CODE, " & vbCrLf & " SUBROW, Description, AMOUNT1, " & vbCrLf & " AMOUNT2 , AMOUNT3, TOTALAMOUNT)  VALUES " & vbCrLf & " (" & RsCompany.Fields("COMPANY_CODE").Value & ", " & RsCompany.Fields("FYEAR").Value & ", " & vbCrLf & " '" & Trim(txtEmpCode.Text) & "', " & vbCrLf & " " & cntRow & ", '" & mDesc & "'," & vbCrLf & " " & mAmount1 & "," & mAmount2 & "," & mAmount3 & ", " & vbCrLf & " " & TotalAmount & ")"

                PubDBCn.Execute(SqlStr)
            Next
        End With

        UpdateDetail = True
        Exit Function
UpdateError:
        '    Resume
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        UpdateDetail = False
    End Function

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
        If MODIFYMode = True And (RsITMain.RecordCount = 0 Or RsITMain.EOF = True) Then Exit Function
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
        txtAYear.Maxlength = RsITMain.Fields("AYear").DefinedSize
        '    txtDesignation.MaxLength = mainclass.SetMaxLength("Name", "EMP", PubDBCn)
        txtFrom.Maxlength = RsITMain.Fields("FROMDATE").DefinedSize
        '    txtPan.MaxLength = RsITMain.Fields("EMPPANNO").DefinedSize
        '    txtTan.MaxLength = RsITMain.Fields("Tan").DefinedSize
        txtTDS.Maxlength = RsITMain.Fields("TDSCIRCLE").DefinedSize
        txtTo.Maxlength = RsITMain.Fields("TODATE").DefinedSize
        txtEmpPan.Maxlength = RsITMain.Fields("EMPPANNO").DefinedSize

        Exit Sub
ERR1:
        MsgBox(Err.Description)
    End Sub
    Private Sub AssignGrid(ByRef mRefresh As Boolean)


        SqlStr = " SELECT ITFORM16.EMP_CODE, EMP_NAME, TAX_PAYABLE " & vbCrLf & " FROM PAY_EMPLOYEE_MST EMP,PAY_ITFORM16_HDR ITFORM16 WHERE " & vbCrLf & " EMP.COMPANY_CODE=ITFORM16.COMPANY_CODE " & vbCrLf & " AND EMP.EMP_CODE=ITFORM16.EMP_CODE " & vbCrLf & " AND ITFORM16.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND ITFORM16.FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

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

            .ColsFrozen = 1
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeNormal '' = OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr

        SqlStr = ""
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        SqlStr = " Delete from PAY_ITForm16_DET WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND Emp_Code='" & xCode & "'"
        PubDBCn.Execute(SqlStr)

        SqlStr = " Delete from PAY_ITForm16_HDR WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND Emp_Code='" & xCode & "'"
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
        With sprdIT
            .Row = mRow
            .MaxCols = ColTotal
            .MaxRows = 1
            .set_RowHeight(mRow, ConRowHeight * 1.5)

            .Col = ColDesc
            .TypeEditCharSet = SS_CELL_EDIT_CHAR_SET_ASCII
            .TypeEditMultiLine = True
            .TypeMaxEditLen = 255
            .set_ColWidth(ColDesc, 35)


            .Col = ColAmt1
            .CellType = SS_CELL_TYPE_EDIT
            .set_ColWidth(ColAmt1, 12)

            .Col = ColAmt2
            .CellType = SS_CELL_TYPE_EDIT
            .set_ColWidth(ColAmt2, 12)

            .Col = ColAmt3
            .CellType = SS_CELL_TYPE_EDIT
            .set_ColWidth(ColAmt3, 12)

            .Col = ColTotal
            .CellType = SS_CELL_TYPE_EDIT
            .set_ColWidth(ColTotal, 12)

        End With
        MainClass.SetSpreadColor(sprdIT, mRow)
        Exit Sub
ERR1:
        If Err.Number = -2147418113 Then Resume Next
        MsgBox(Err.Description, MsgBoxStyle.Information)
    End Sub
    Private Sub FillSprdGrid()

        With sprdIT
            .MaxCols = ColTotal

            .Row = 1

            .Col = ColSNO
            .Text = "1."
            .Col = ColDesc
            .Text = "Gross Salary *"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = "a). Salary as per provisions contained in sec. 17(1)"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = "b). Value of perquisites u/s 17(2) (as per Form No. 12BA, wherever applicable)"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = "c). Profits in lieu of salary under section 17(3) (as per Form No. 12BA, wherever applicable)"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = "d). Total"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColSNO
            .Text = "2."
            .Col = ColDesc
            .Text = "Less : Allowance to the extent exempt under section 10 : " & vbNewLine & "(a) HRA "

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = ""

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = ""

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = ""

            ''15-04-2008
            If RsCompany.Fields("FYEAR").Value >= 2007 Then
                .MaxRows = .Row + 1
                .Row = .Row + 1
                .Col = ColDesc
                .Text = ""

                .MaxRows = .Row + 1
                .Row = .Row + 1
                .Col = ColDesc
                .Text = ""

                .MaxRows = .Row + 1
                .Row = .Row + 1
                .Col = ColDesc
                .Text = ""
            End If

            ''29-03-2007
            '13
            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = ""

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = ""

            '*******

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColSNO
            .Text = "3."
            .Col = ColDesc
            .Text = "Balance (1-2)"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColSNO
            .Text = "4."
            .Col = ColDesc
            .Text = "Deductions :"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColSNO
            .Text = ""
            '        .Col = ColDesc
            '        .Text = "(a) Standard deduction"
            '
            '        .MaxRows = .Row + 1
            '        .Row = .Row + 1
            '        .Col = ColSNO
            '        .Text = ""
            .Col = ColDesc
            .Text = "(a) Entertainment Allowance"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColSNO
            .Text = ""
            .Col = ColDesc
            .Text = "(b) Tax on Employment"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColSNO
            .Text = "5."
            .Col = ColDesc
            .Text = "Aggregate of 4(a to b)"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColSNO
            .Text = "6."
            .Col = ColDesc
            .Text = "Income chargeable under the head 'Salaries' [3-5]"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColSNO
            .Text = "7."
            .Col = ColDesc
            .Text = "Add : Any other income reported by the employee"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = ""

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = ""

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = ""
            '22

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = ""

            '29-03-2007
            '        .MaxRows = .Row + 1
            '        .Row = .Row + 1
            '        .Col = ColDesc
            '        .Text = ""
            '
            '        .MaxRows = .Row + 1
            '        .Row = .Row + 1
            '        .Col = ColDesc
            '        .Text = ""

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColSNO
            .Text = "8."
            .Col = ColDesc
            .Text = "Gross total income (6+7)"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColSNO
            .Text = "9."
            .Col = ColDesc
            .Text = "Deduction under chapter VI-A"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = "(A) Sections 80C, 80CCC and 80CCD"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = "(a) Section 80C"
            .Col = ColAmt1
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            .Text = "Gross Amt"
            .Col = ColAmt2
            '        .TypeEditMultiLine = True
            '        .TypeHAlign = TypeHAlignCenter
            '        .Text = "Qualifying Amt"
            '        .Col = ColAmt3
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            .Text = "Deductible Amt"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = ""

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = ""

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = ""

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = ""

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = ""

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = ""

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = ""

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = ""

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = ""

            '        .MaxRows = .Row + 1
            '        .Row = .Row + 1
            '        .Col = ColDesc
            '        .Text = ""
            '
            '        .MaxRows = .Row + 1
            '        .Row = .Row + 1
            '        .Col = ColDesc
            '        .Text = ""

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = "(b) Section 80CCC"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = "(c) Section 80CCD"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = "(B) Other Sections (for e.g. 80E,80G,80TTA,etc.) under Chapter VIA"
            .Col = ColAmt1
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            .Text = "Gross Amt"
            .Col = ColAmt2
            '        .TypeEditMultiLine = True
            '        .TypeHAlign = TypeHAlignCenter
            '        .Text = "Qualifying Amt"
            '        .Col = ColAmt3
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            .Text = "Deductible Amt"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = "(a)."

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = "(b)."

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = "(c)."

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = "(d)."

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = "(e)."


            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColSNO
            .Text = "10."
            .Col = ColDesc
            .Text = "Aggregate of deductible amounts under Chapter VI-A"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColSNO
            .Text = "11."
            .Col = ColDesc
            .Text = "Total Income (8-10)"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColSNO
            .Text = "12."
            .Col = ColDesc
            .Text = "Tax on total income"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColSNO
            .Text = "13."
            .Col = ColDesc
            .Text = "Surcharge (on tax computed at S. No. 12)"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColSNO
            .Text = "14."
            .Col = ColDesc
            .Text = "Education Cess (on tax at S.No. 12 and surcharge at S. No. 13)"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColSNO
            .Text = "15."
            .Col = ColDesc
            .Text = "Tax payable (12+13+14)"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColSNO
            .Text = "16."
            .Col = ColDesc
            .Text = "Relif Under Section 89" & Chr(13) & "(attach details)"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColSNO
            .Text = "17."
            .Col = ColDesc
            .Text = "Tax payable (15-16)"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColSNO
            .Text = "18."
            .Col = ColDesc
            .Text = "Less : (a). Tax deducted at source u/s 192(1) "

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColSNO
            .Text = ""
            .Col = ColDesc
            .Text = "       (b). Tax paid by the employer on behalf of the employee under section 192(1A) on perquisites under section 17(2)"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColSNO
            .Text = "19."
            .Col = ColDesc
            .Text = "Tax payable / refundable (17-18)"


            MainClass.ProtectCell(sprdIT, 1, 6, ColDesc, ColDesc)
            MainClass.ProtectCell(sprdIT, 10, 16, ColDesc, ColDesc)
            MainClass.ProtectCell(sprdIT, 21, 24, ColDesc, ColDesc)
            MainClass.ProtectCell(sprdIT, 34, 36, ColDesc, ColDesc)
            MainClass.ProtectCell(sprdIT, 42, .MaxRows, ColDesc, ColDesc)

        End With
    End Sub

    Private Sub FillSprdGrid2010()

        With sprdIT
            .MaxCols = ColTotal

            .Row = 1

            .Col = ColSNO
            .Text = "1."
            .Col = ColDesc
            .Text = "Gross Salary *"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = "a). Salary as per provisions contained in sec. 17(1)"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = "b). Value of perquisites u/s 17(2) (as per Form No. 12BA, wherever applicable)"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = "c). Profits in lieu of salary under section 17(3) (as per Form No. 12BA, wherever applicable)"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = "d). Total"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColSNO
            .Text = "2."
            .Col = ColDesc
            .Text = "Less : Allowance to the extent exempt under section 10 : " & vbNewLine & "(a) HRA "

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = ""

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = ""

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = ""

            ''15-04-2008
            If RsCompany.Fields("FYEAR").Value >= 2007 Then
                .MaxRows = .Row + 1
                .Row = .Row + 1
                .Col = ColDesc
                .Text = ""

                .MaxRows = .Row + 1
                .Row = .Row + 1
                .Col = ColDesc
                .Text = ""

                .MaxRows = .Row + 1
                .Row = .Row + 1
                .Col = ColDesc
                .Text = ""
            End If

            ''29-03-2007
            '13
            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = ""

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = ""

            '*******

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColSNO
            .Text = "3."
            .Col = ColDesc
            .Text = "Balance (1-2)"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColSNO
            .Text = "4."
            .Col = ColDesc
            .Text = "Deductions :"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColSNO
            .Text = ""
            '        .Col = ColDesc
            '        .Text = "(a) Standard deduction"
            '
            '        .MaxRows = .Row + 1
            '        .Row = .Row + 1
            '        .Col = ColSNO
            '        .Text = ""
            .Col = ColDesc
            .Text = "(a) Entertainment Allowance"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColSNO
            .Text = ""
            .Col = ColDesc
            .Text = "(b) Tax on Employment"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColSNO
            .Text = "5."
            .Col = ColDesc
            .Text = "Aggregate of 4(a) and 4(b)"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColSNO
            .Text = "6."
            .Col = ColDesc
            .Text = "Income chargeable under the head 'Salaries' [3-5]"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColSNO
            .Text = "7."
            .Col = ColDesc
            .Text = "Add : Any other income reported by the employee"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = ""

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = ""

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = ""
            '22

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = ""

            '29-03-2007
            '        .MaxRows = .Row + 1
            '        .Row = .Row + 1
            '        .Col = ColDesc
            '        .Text = ""
            '
            '        .MaxRows = .Row + 1
            '        .Row = .Row + 1
            '        .Col = ColDesc
            '        .Text = ""

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColSNO
            .Text = "8."
            .Col = ColDesc
            .Text = "Gross total income (6+7)"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColSNO
            .Text = "9."
            .Col = ColDesc
            .Text = "Deduction under chapter VI-A"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = "(A) Sections 80C, 80CCC and 80CCD"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = "(a) Section 80C"
            .Col = ColAmt1
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            .Text = "Gross Amt"
            .Col = ColAmt2
            '        .TypeEditMultiLine = True
            '        .TypeHAlign = TypeHAlignCenter
            '        .Text = "Qualifying Amt"
            '        .Col = ColAmt3
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            .Text = "Deductible Amt"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = ""

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = ""

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = ""

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = ""

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = ""

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = ""

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = ""

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = ""

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = ""

            '        .MaxRows = .Row + 1
            '        .Row = .Row + 1
            '        .Col = ColDesc
            '        .Text = ""
            '
            '        .MaxRows = .Row + 1
            '        .Row = .Row + 1
            '        .Col = ColDesc
            '        .Text = ""

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = "(b) Section 80CCC"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = "(c) Section 80CCD"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = "(B) Other Sections (for e.g. 80E,80G,80TTA,etc.) under Chapter VIA"
            .Col = ColAmt1
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            .Text = "Gross Amt"
            .Col = ColAmt2
            '        .TypeEditMultiLine = True
            '        .TypeHAlign = TypeHAlignCenter
            '        .Text = "Qualifying Amt"
            '        .Col = ColAmt3
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            .Text = "Deductible Amt"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = "(a)."

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = "(b)."

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = "(c)."

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = "(d)."

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColDesc
            .Text = "(e)."


            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColSNO
            .Text = "10."
            .Col = ColDesc
            .Text = "Aggregate of deductible amounts under Chapter VI-A"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColSNO
            .Text = "11."
            .Col = ColDesc
            .Text = "Total Income (8-10)"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColSNO
            .Text = "12."
            .Col = ColDesc
            .Text = "Tax on total income"

            '        .MaxRows = .Row + 1
            '        .Row = .Row + 1
            '        .Col = ColSNo
            '        .Text = "13."
            '        .Col = ColDesc
            '        .Text = "Surcharge (on tax computed at S. No. 12)"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColSNO
            .Text = "13."
            .Col = ColDesc
            .Text = "Education Cess @3%(on tax computed at S.No. 12)"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColSNO
            .Text = "14."
            .Col = ColDesc
            .Text = "Tax payable (12+13)"

            .MaxRows = .Row + 1
            .Row = .Row + 1
            .Col = ColSNO
            .Text = "15."
            .Col = ColDesc
            .Text = "Relif Under Section 89" & Chr(13) & "(attach details)"

            If RsCompany.Fields("FYEAR").Value >= 2013 Then
                .MaxRows = .Row + 1
                .Row = .Row + 1
                .Col = ColSNO
                .Text = "16."
                .Col = ColDesc
                .Text = "Rebate Under Section 87A                                               "


                .MaxRows = .Row + 1
                .Row = .Row + 1
                .Col = ColSNO
                .Text = "17."
                .Col = ColDesc
                .Text = "Tax payable (14-15)"
            Else
                .MaxRows = .Row + 1
                .Row = .Row + 1
                .Col = ColSNO
                .Text = "16."
                .Col = ColDesc
                .Text = "Tax payable (14-15)"

            End If
            '        .MaxRows = .Row + 1
            '        .Row = .Row + 1
            '        .Col = ColSNo
            '        .Text = "18."
            '        .Col = ColDesc
            '        .Text = "Less : (a). Tax deducted at source u/s 192(1) "
            '
            '        .MaxRows = .Row + 1
            '        .Row = .Row + 1
            '        .Col = ColSNo
            '        .Text = ""
            '        .Col = ColDesc
            '        .Text = "       (b). Tax paid by the employer on behalf of the employee under section 192(1A) on perquisites under section 17(2)"

            '        .MaxRows = .Row + 1
            '        .Row = .Row + 1
            '        .Col = ColSNo
            '        .Text = "19."
            '        .Col = ColDesc
            '        .Text = "Tax payable / refundable (17-18)"


            MainClass.ProtectCell(sprdIT, 1, 6, ColDesc, ColDesc)
            MainClass.ProtectCell(sprdIT, 10, 16, ColDesc, ColDesc)
            MainClass.ProtectCell(sprdIT, 21, 24, ColDesc, ColDesc)
            MainClass.ProtectCell(sprdIT, 34, 36, ColDesc, ColDesc)
            MainClass.ProtectCell(sprdIT, 42, .MaxRows, ColDesc, ColDesc)

        End With
    End Sub
    Private Sub CalcGrid()
        Dim cntRow As Integer
        Dim mGSalary As Double
        Dim mRow3, mRow2, mRow4 As Object
        Dim mRow5 As Double
        Dim mRow9, mRow7, mRow6, mRow8, mRow9i As Object
        Dim mRow10 As Double
        Dim mRow14, mRow12, mRow13, mRow15 As Object
        Dim mRow16 As Double
        Dim mRow21, mRow19, mRow17, mRow18, mRow20, mrow21i As Object
        Dim mRow22 As Double
        Dim mRow25i, mRow24ii, mRow24i, mRow24iii, mRow25ii As Object
        Dim mRow25iii As Double
        Dim mRow27i, mRow26ii, mRow26i, mRow26iii, mRow27ii As Object
        Dim mRow27iii As Double
        Dim mRow29, mRow28ii, mRow28i, mRow28iii, mRow30 As Object
        Dim mRow31 As Double
        Dim mRow35ii, mRow34ii, mRow34i, mRow35i, mRow36i As Object
        Dim mRow36ii As Double
        Dim mRow39i, mRow38i, mRow37i, mRow37ii, mRow38ii, mRow39ii As Object
        Dim mRow39iii As Double
        Dim mRow44, mRow42, mRow41i, mRow40ii, mRow40, mRow40i, mRow41, mRow41ii, mRow43, mRow45 As Object
        Dim mRow46 As Double
        Dim mRow50, mRow48, mRow42iii, mRow42i, mRow47, mRow42ii, mRow51T, mRow49, mRow51 As Object
        Dim mRow52 As Double
        Dim mSurcharge As Double
        Dim mRow20i As Double
        Dim mRow31i As Object
        Dim mRow31ii As Double
        Dim mRow32i As Object
        Dim mRow32ii As Double
        Dim mRow33i As Object
        Dim mRow33ii As Double
        Dim mRow35iii, mRow37iii As Object
        Dim mRow43iii As Double
        Dim mRow23, mRow22i, mRow29i As Object
        Dim mRow29ii As Double
        Dim mRow30i As Object
        Dim mRow30ii As Double
        Dim mRow43i As Object
        Dim mRow43ii As Double
        Dim mRow53, mRow54 As Object
        Dim mRow53T As Double
        Dim mRow9iii As Double
        Dim mRow9ii As Double
        Dim mRow9iv As Double
        Dim mRow9v As Double
        Dim mRow9vi As Double
        Dim m80CSlab As Double

        'Dim mRow34iii, mRow35iii, mRow36iii, mRow37iii, mRow38iii, mRow40iii, mRow41iii As Double

        m80CSlab = GetChapterVISlab("80C")
        With sprdIT
            .Col = 3
            .Row = 2
            mRow2 = Val(.Text)

            .Row = 3
            mRow3 = Val(.Text)

            .Row = 4
            mRow4 = Val(.Text)

            .Row = 5
            .Col = 4
            mRow5 = mRow2 + mRow3 + mRow4 + Val(txtPrevSalary.Text)
            .Text = CStr(mRow5) 'Gross Salary


            .Col = 3
            .Row = 6
            mRow6 = Val(.Text)

            .Row = 7
            mRow7 = Val(.Text)

            .Row = 8
            mRow8 = Val(.Text)

            .Row = 9
            mRow9i = Val(.Text)
            .Text = mRow9i

            .Row = 10
            mRow9ii = Val(.Text)
            .Text = CStr(mRow9ii)

            .Row = 11
            mRow9iii = Val(.Text)
            .Text = CStr(mRow9iii)

            .Row = 12
            mRow9iv = Val(.Text)
            .Text = CStr(mRow9iv)

            .Row = 13
            mRow9v = Val(.Text)
            .Text = CStr(mRow9v)

            .Row = 14
            mRow9vi = Val(.Text)
            .Text = CStr(mRow9vi)

            .Row = 14
            .Col = 4
            mRow9 = mRow6 + mRow7 + mRow8 + mRow9i + mRow9ii + mRow9iii + mRow9iv + mRow9v + mRow9vi
            .Text = mRow9

            .Row = 10 + 2 + 3
            mRow10 = mRow5 - mRow9
            .Text = CStr(mRow10)

            .Col = 2
            .Row = 12 + 2 + 3
            mRow12 = Val(.Text)

            .Row = 13 + 2 + 3
            mRow13 = Val(.Text)

            .Row = 14 + 2 + 3
            .Col = 3
            mRow14 = mRow12 + mRow13
            .Text = mRow14 'Dedction Total...

            .Row = 15 + 2 + 3
            .Col = ColTotal
            mRow15 = mRow10 - mRow14
            .Text = mRow15 ''6

            .Col = 2
            .Row = 16 + 2
            mRow16 = Val(.Text)

            .Row = 17 + 2 + 3
            mRow17 = Val(.Text)

            .Row = 18 + 2 + 3
            mRow18 = Val(.Text)

            .Row = 19 + 2 + 3
            mRow19 = Val(.Text)

            .Row = 20 + 2 + 3
            mRow20 = Val(.Text)

            '        .Row = 21
            '        mRow21 = Val(.Text)
            '
            '        .Row = 22
            '        mRow22 = Val(.Text)

            '        .Row = 21
            '        mRow21 = Val(.Text)

            .Row = 22 + 3
            .Col = ColTotal
            mRow22i = mRow16 + mRow17 + mRow18 + mRow19 + mRow20 ''+ mRow21 + mRow22
            .Text = mRow22i '7

            .Row = 23 + 3
            .Col = ColTotal
            mRow23 = mRow15 + mRow22i
            .Text = mRow23 '8

            .Row = 27 + 3
            .Col = 2
            mRow27i = Val(.Text)
            .Col = 3
            mRow27ii = Val(.Text)
            '        .Col = 4
            '        mRow25iii = Val(.Text)

            .Row = 28 + 3
            .Col = 2
            mRow28i = Val(.Text)
            .Col = 3
            mRow28ii = Val(.Text)

            .Row = 29 + 3
            .Col = 2
            mRow29i = Val(.Text)
            .Col = 3
            mRow29ii = Val(.Text)

            .Row = 30 + 3
            .Col = 2
            mRow30i = Val(.Text)
            .Col = 3
            mRow30ii = Val(.Text)

            .Row = 31 + 3
            .Col = 2
            mRow31i = Val(.Text)
            .Col = 3
            mRow31ii = Val(.Text)

            .Row = 32 + 3
            .Col = 2
            mRow32i = Val(.Text)
            .Col = 3
            mRow32ii = Val(.Text)

            .Row = 33 + 3
            .Col = 2
            mRow33i = Val(.Text)
            .Col = 3
            mRow33ii = Val(.Text)

            .Row = 34 + 3
            .Col = 2
            mRow34i = Val(.Text)
            .Col = 3
            mRow34ii = Val(.Text)

            .Row = 35 + 3
            .Col = 2
            mRow35i = Val(.Text)
            .Col = 3
            mRow35ii = Val(.Text)

            .Col = 4
            mRow35iii = mRow27ii + mRow28ii + mRow29ii + mRow30ii + mRow31ii + mRow32ii + mRow33ii + mRow34ii + mRow35ii
            mRow35iii = IIf(mRow35iii > m80CSlab, m80CSlab, mRow35iii)
            .Text = mRow35iii


            .Row = 36 + 3
            .Col = 2
            mRow36i = Val(.Text)
            .Col = 3
            mRow36ii = Val(.Text)

            .Row = 37 + 3
            .Col = 2
            mRow37i = Val(.Text)
            .Col = 3
            mRow37ii = Val(.Text)

            .Col = 4
            mRow37iii = mRow36ii + mRow37ii
            .Text = mRow37iii

            .Row = 39 + 3
            .Col = 2
            mRow39i = Val(.Text)
            .Col = 3
            mRow39ii = Val(.Text)

            .Row = 40 + 3
            .Col = 2
            mRow40i = Val(.Text)
            .Col = 3
            mRow40ii = Val(.Text)

            .Row = 41 + 3
            .Col = 2
            mRow41i = Val(.Text)
            .Col = 3
            mRow41ii = Val(.Text)

            .Row = 42 + 3
            .Col = 2
            mRow42i = Val(.Text)
            .Col = 3
            mRow42ii = Val(.Text)

            .Row = 43 + 3
            .Col = 2
            mRow43i = Val(.Text)
            .Col = 3
            mRow43ii = Val(.Text)

            .Col = 4
            mRow43iii = mRow39ii + mRow40ii + mRow41ii + mRow42ii + mRow43ii
            .Text = CStr(mRow43iii) '10

            .Row = 44 + 3
            .Col = ColTotal
            mRow44 = mRow35iii + mRow37iii + mRow43iii
            .Text = mRow44 '10

            .Row = 45 + 3
            .Col = ColTotal
            mRow45 = mRow23 - mRow44
            .Text = mRow45 '11

            .Row = 46 + 3
            .Col = ColTotal
            mRow46 = Val(.Text)
            .Text = CStr(mRow46)

            .Row = 47 + 3
            .Col = ColTotal
            mRow47 = Val(.Text)
            .Text = mRow47

            .Row = 48 + 3
            .Col = ColTotal
            mRow48 = Val(.Text)
            .Text = mRow48

            .Row = 49 + 3
            .Col = ColTotal
            mRow49 = mRow46 + mRow47 + mRow48
            .Text = mRow49

            .Row = 50 + 3
            .Col = ColTotal
            mRow50 = Val(.Text)
            .Text = mRow50

            .Row = 51 + 3
            .Col = ColTotal
            mRow51 = mRow49 + mRow50
            .Text = mRow51

            '        mSurcharge = GetSurcharge()
            '        .Row = 47
            '        .Col = ColTotal
            '        mRow47 = mRow31 - mRow46 + mSurcharge
            '        mRow47 = Round(mRow47, 0)
            '        .Text = mRow47          ''15

            .Row = 52 + 3
            .Col = ColAmt3
            mRow52 = Val(.Text) '18a

            .Row = 53 + 3
            .Col = ColAmt3
            mRow53 = Val(.Text) '18b

            .Col = ColTotal
            mRow53T = mRow52 + mRow53
            .Text = CStr(mRow53T)

            .Row = 54 + 3
            .Col = ColTotal
            mRow54 = mRow51 - mRow53T
            .Text = mRow54 ''19

        End With
    End Sub
    Private Sub CalcGrid2010()
        Dim cntRow As Integer
        Dim mGSalary As Double
        Dim mRow3, mRow2, mRow4 As Object
        Dim mRow5 As Double
        Dim mRow9, mRow7, mRow6, mRow8, mRow9i As Object
        Dim mRow10 As Double
        Dim mRow14, mRow12, mRow13, mRow15 As Object
        Dim mRow16 As Double
        Dim mRow21, mRow19, mRow17, mRow18, mRow20, mrow21i As Object
        Dim mRow22 As Double
        Dim mRow25i, mRow24ii, mRow24i, mRow24iii, mRow25ii As Object
        Dim mRow25iii As Double
        Dim mRow27i, mRow26ii, mRow26i, mRow26iii, mRow27ii As Object
        Dim mRow27iii As Double
        Dim mRow29, mRow28ii, mRow28i, mRow28iii, mRow30 As Object
        Dim mRow31 As Double
        Dim mRow35ii, mRow34ii, mRow34i, mRow35i, mRow36i As Object
        Dim mRow36ii As Double
        Dim mRow39i, mRow38i, mRow37i, mRow37ii, mRow38ii, mRow39ii As Object
        Dim mRow39iii As Double
        Dim mRow44, mRow42, mRow41i, mRow40ii, mRow40, mRow40i, mRow41, mRow41ii, mRow43, mRow45 As Object
        Dim mRow46 As Double
        Dim mRow50, mRow48, mRow42iii, mRow42i, mRow47, mRow42ii, mRow51T, mRow49, mRow51 As Object
        Dim mRow52 As Double
        Dim mSurcharge As Double
        Dim mRow20i As Double
        Dim mRow31i As Object
        Dim mRow31ii As Double
        Dim mRow32i As Object
        Dim mRow32ii As Double
        Dim mRow33i As Object
        Dim mRow33ii As Double
        Dim mRow35iii, mRow37iii As Object
        Dim mRow43iii As Double
        Dim mRow23, mRow22i, mRow29i As Object
        Dim mRow29ii As Double
        Dim mRow30i As Object
        Dim mRow30ii As Double
        Dim mRow43i As Object
        Dim mRow43ii As Double
        Dim mRow53, mRow54 As Object
        Dim mRow53T As Double
        Dim mRow9iii As Double
        Dim mRow9ii As Double
        Dim mRow9iv As Double
        Dim mRow9v As Double
        Dim mRow9vi As Double
        Dim mRow49A As Double
        Dim m80CSlab As Double
        'Dim mRow34iii, mRow35iii, mRow36iii, mRow37iii, mRow38iii, mRow40iii, mRow41iii As Double

        m80CSlab = GetChapterVISlab("80C")

        With sprdIT
            .Col = 3
            .Row = 2
            mRow2 = Val(.Text)

            .Row = 3
            mRow3 = Val(.Text)

            .Row = 4
            mRow4 = Val(.Text)

            .Row = 5
            .Col = 4
            mRow5 = mRow2 + mRow3 + mRow4 + Val(txtPrevSalary.Text)
            .Text = CStr(mRow5) 'Gross Salary


            .Col = 3
            .Row = 6
            mRow6 = Val(.Text)

            .Row = 7
            mRow7 = Val(.Text)

            .Row = 8
            mRow8 = Val(.Text)

            .Row = 9
            mRow9i = Val(.Text)
            .Text = mRow9i

            .Row = 10
            mRow9ii = Val(.Text)
            .Text = CStr(mRow9ii)

            .Row = 11
            mRow9iii = Val(.Text)
            .Text = CStr(mRow9iii)

            .Row = 12
            mRow9iv = Val(.Text)
            .Text = CStr(mRow9iv)

            .Row = 13
            mRow9v = Val(.Text)
            .Text = CStr(mRow9v)

            .Row = 14
            mRow9vi = Val(.Text)
            .Text = CStr(mRow9vi)

            .Row = 14
            .Col = 4
            mRow9 = mRow6 + mRow7 + mRow8 + mRow9i + mRow9ii + mRow9iii + mRow9iv + mRow9v + mRow9vi
            .Text = mRow9

            .Row = 10 + 2 + 3
            mRow10 = mRow5 - mRow9
            .Text = CStr(mRow10)

            .Col = 2
            .Row = 12 + 2 + 3
            mRow12 = Val(.Text)

            .Row = 13 + 2 + 3
            mRow13 = Val(.Text)

            .Row = 14 + 2 + 3
            .Col = 3
            mRow14 = mRow12 + mRow13
            .Text = mRow14 'Dedction Total...

            .Row = 15 + 2 + 3
            .Col = ColTotal
            mRow15 = mRow10 - mRow14
            .Text = mRow15 ''6

            .Col = 2
            .Row = 16 + 2
            mRow16 = Val(.Text)

            .Row = 17 + 2 + 3
            mRow17 = Val(.Text)

            .Row = 18 + 2 + 3
            mRow18 = Val(.Text)

            .Row = 19 + 2 + 3
            mRow19 = Val(.Text)

            .Row = 20 + 2 + 3
            mRow20 = Val(.Text)

            '        .Row = 21
            '        mRow21 = Val(.Text)
            '
            '        .Row = 22
            '        mRow22 = Val(.Text)

            '        .Row = 21
            '        mRow21 = Val(.Text)

            .Row = 22 + 3
            .Col = ColTotal
            mRow22i = mRow16 + mRow17 + mRow18 + mRow19 + mRow20 ''+ mRow21 + mRow22
            .Text = mRow22i '7

            .Row = 23 + 3
            .Col = ColTotal
            mRow23 = mRow15 + mRow22i
            .Text = mRow23 '8

            .Row = 27 + 3
            .Col = 2
            mRow27i = Val(.Text)
            .Col = 3
            mRow27ii = Val(.Text)
            '        .Col = 4
            '        mRow25iii = Val(.Text)

            .Row = 28 + 3
            .Col = 2
            mRow28i = Val(.Text)
            .Col = 3
            mRow28ii = Val(.Text)

            .Row = 29 + 3
            .Col = 2
            mRow29i = Val(.Text)
            .Col = 3
            mRow29ii = Val(.Text)

            .Row = 30 + 3
            .Col = 2
            mRow30i = Val(.Text)
            .Col = 3
            mRow30ii = Val(.Text)

            .Row = 31 + 3
            .Col = 2
            mRow31i = Val(.Text)
            .Col = 3
            mRow31ii = Val(.Text)

            .Row = 32 + 3
            .Col = 2
            mRow32i = Val(.Text)
            .Col = 3
            mRow32ii = Val(.Text)

            .Row = 33 + 3
            .Col = 2
            mRow33i = Val(.Text)
            .Col = 3
            mRow33ii = Val(.Text)

            .Row = 34 + 3
            .Col = 2
            mRow34i = Val(.Text)
            .Col = 3
            mRow34ii = Val(.Text)

            .Row = 35 + 3
            .Col = 2
            mRow35i = Val(.Text)
            .Col = 3
            mRow35ii = Val(.Text)

            .Col = 4
            mRow35iii = mRow27ii + mRow28ii + mRow29ii + mRow30ii + mRow31ii + mRow32ii + mRow33ii + mRow34ii + mRow35ii
            mRow35iii = IIf(mRow35iii > m80CSlab, m80CSlab, mRow35iii)
            .Text = mRow35iii


            .Row = 36 + 3
            .Col = 2
            mRow36i = Val(.Text)
            .Col = 3
            mRow36ii = Val(.Text)

            .Row = 37 + 3
            .Col = 2
            mRow37i = Val(.Text)
            .Col = 3
            mRow37ii = Val(.Text)

            .Col = 4
            mRow37iii = mRow36ii + mRow37ii
            .Text = mRow37iii

            .Row = 39 + 3
            .Col = 2
            mRow39i = Val(.Text)
            .Col = 3
            mRow39ii = Val(.Text)

            .Row = 40 + 3
            .Col = 2
            mRow40i = Val(.Text)
            .Col = 3
            mRow40ii = Val(.Text)

            .Row = 41 + 3
            .Col = 2
            mRow41i = Val(.Text)
            .Col = 3
            mRow41ii = Val(.Text)

            .Row = 42 + 3
            .Col = 2
            mRow42i = Val(.Text)
            .Col = 3
            mRow42ii = Val(.Text)

            .Row = 43 + 3
            .Col = 2
            mRow43i = Val(.Text)
            .Col = 3
            mRow43ii = Val(.Text)

            .Col = 4
            mRow43iii = mRow39ii + mRow40ii + mRow41ii + mRow42ii + mRow43ii
            .Text = CStr(mRow43iii) '10

            .Row = 44 + 3
            .Col = ColTotal
            mRow44 = mRow35iii + mRow37iii + mRow43iii
            .Text = mRow44 '10

            .Row = 45 + 3
            .Col = ColTotal
            mRow45 = mRow23 - mRow44
            .Text = mRow45 '11

            ''12
            .Row = 46 + 3
            .Col = ColTotal
            mRow46 = Val(.Text)
            .Text = CStr(mRow46)

            ''13
            .Row = 47 + 3
            .Col = ColTotal
            mRow47 = Val(.Text)
            .Text = mRow47

            ''14=12-13
            .Row = 48 + 3
            .Col = ColTotal
            mRow48 = mRow46 + mRow47
            .Text = mRow48

            ''15
            .Row = 49 + 3
            .Col = ColTotal
            mRow49 = Val(.Text)
            .Text = mRow49

            ''16-14-15
            If RsCompany.Fields("FYEAR").Value >= 2013 Then
                .Row = 50 + 3
                .Col = ColTotal
                mRow49A = Val(.Text)
                .Text = CStr(mRow49A)

                .Row = 50 + 4
                .Col = ColTotal
                mRow50 = mRow48 + mRow49 + mRow49A
                .Text = mRow50

            Else
                .Row = 50 + 3
                .Col = ColTotal
                mRow50 = mRow48 + mRow49
                .Text = mRow50
            End If

            '        .Row = 51 + 3
            '        .Col = ColTotal
            '        mRow51 = mRow49 + mRow50
            '        .Text = mRow51
            '
            '
            '        .Row = 52 + 3
            '        .Col = ColAmt3
            '        mRow52 = Val(.Text)     '18a
            '
            '        .Row = 53 + 3
            '        .Col = ColAmt3
            '        mRow53 = Val(.Text)     '18b
            '
            '        .Col = ColTotal
            '        mRow53T = mRow52 + mRow53
            '        .Text = mRow53T
            '
            '        .Row = 54 + 3
            '        .Col = ColTotal
            '        mRow54 = mRow51 - mRow53T
            '        .Text = mRow54          ''19

        End With
    End Sub

    Private Sub CellFormat()

        Dim cntRow As Integer
        With sprdIT
            .Row = 1
            .Row2 = .MaxRows
            .Col = ColAmt1
            .col2 = .MaxCols
            .BlockMode = True
            .CellType = SS_CELL_TYPE_STATIC_TEXT
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0C0C0)
            .BlockMode = False

            For cntRow = 1 To .MaxRows
                .Row = cntRow
                Select Case cntRow
                    Case 12 + 2, 13 + 2, 17 + 2, 18 + 2, 19 + 2 '', 20+ 2, ''21         '', 14
                        .Col = ColAmt1
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
                    Case 2, 3, 4, 6, 7, 8, 9, 10
                        .Col = ColAmt2
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
                    Case 9 + 2
                        .Col = ColAmt2
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)

                        .Col = ColAmt3
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF) '&HFFFFFF
                        MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColAmt3, ColAmt3)
                    Case 22
                        .Col = ColAmt1
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)

                        .Col = ColTotal
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF) '&HFFFFFF
                        MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColTotal, ColTotal)

                    Case 5, 10 + 2
                        .Col = ColAmt3
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF) '&HFFFFFF
                        MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColAmt3, ColAmt3)
                    Case 14 + 2
                        .Col = ColAmt2
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF) '&HFFFFFF
                        MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColAmt2, ColAmt2)
                    Case 15 + 2, 23, 44, 45, 46, 47, 48, 49, 51, 54 '16, 22, 29, 30, 31, 45, 46, 48, 51
                        .Col = ColTotal
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF) ' &HFFFFFF
                        MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColTotal, ColTotal)

                    Case 27, 28, 29, 30, 31, 32, 33, 34, 36
                        .Col = ColAmt1
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
                        .Col = ColAmt2
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
                        '                    .Col = ColAmt3
                        '                    .CellType = SS_CELL_TYPE_FLOAT
                        '                    .BackColor = &HFFFFFF
                    Case 39, 40, 41, 42
                        .Col = ColAmt1
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
                        .Col = ColAmt2
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
                        '                    .Col = ColAmt3
                        '                    .CellType = SS_CELL_TYPE_FLOAT
                        '                    .BackColor = &HC0FFFF
                        MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColAmt3, ColAmt3)
                    Case 35, 37, 43
                        .Col = ColAmt1
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
                        .Col = ColAmt2
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
                        .Col = ColAmt3
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF)
                        MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColAmt3, ColAmt3)
                        '                Case 41
                        '                    .Col = ColAmt1
                        '                    .CellType = SS_CELL_TYPE_FLOAT
                        '                    .BackColor = &HFFFFFF
                        '
                        '                    .Col = ColAmt2
                        '                    .CellType = SS_CELL_TYPE_FLOAT
                        '                    .BackColor = &HFFFFFF
                        '
                        '                    .Col = ColAmt3
                        '                    .CellType = SS_CELL_TYPE_FLOAT
                        '                    .BackColor = &HC0FFFF   '&HFFFFFF
                        '                    MainClass.ProtectCell sprdIT, cntRow, cntRow, ColAmt1, ColAmt3
                        '                Case 42, 43, 44     '43, 44
                        '                    .Col = ColAmt3
                        '                    .CellType = SS_CELL_TYPE_FLOAT
                        '                    .BackColor = &HFFFFFF
                    Case 50 '47
                        .Col = ColTotal
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
                    Case 52, 53
                        .Col = ColAmt3
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)

                        If cntRow = 53 Then '50 Then
                            .Col = ColTotal
                            .CellType = SS_CELL_TYPE_FLOAT
                            .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF) ' &HFFFFFF
                            MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColTotal, ColTotal)
                        End If
                End Select
            Next

            For cntRow = 1 To .MaxRows
                .Row = cntRow
                Select Case cntRow
                    Case 44, 45, 46, 47 '43, 44, 45
                        .Col = ColDesc
                        .CellType = SS_CELL_TYPE_EDIT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
                End Select
            Next
        End With
    End Sub
    Private Sub CellFormatNew()

        Dim cntRow As Integer
        With sprdIT
            .Row = 1
            .Row2 = .MaxRows
            .Col = ColAmt1
            .col2 = .MaxCols
            .BlockMode = True
            .CellType = SS_CELL_TYPE_STATIC_TEXT
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0C0C0)
            .BlockMode = False

            For cntRow = 1 To .MaxRows
                .Row = cntRow
                Select Case cntRow
                    Case 17, 18, 22, 23, 24
                        .Col = ColAmt1
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
                    Case 2, 3, 4, 6, 7, 8, 9, 10, 11, 12, 13
                        .Col = ColAmt2
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
                    Case 14
                        .Col = ColAmt2
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)

                        .Col = ColAmt3
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF) '&HFFFFFF
                        MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColAmt3, ColAmt3)
                    Case 25
                        .Col = ColAmt1
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)

                        .Col = ColTotal
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF) '&HFFFFFF
                        MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColTotal, ColTotal)

                    Case 5, 15
                        .Col = ColAmt3
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF) '&HFFFFFF
                        MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColAmt3, ColAmt3)
                    Case 19
                        .Col = ColAmt2
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF) '&HFFFFFF
                        MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColAmt2, ColAmt2)
                    Case 20, 26, 47, 48, 49, 50, 51, 52, 54, 57
                        .Col = ColTotal
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF) ' &HFFFFFF
                        MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColTotal, ColTotal)

                    Case 30, 31, 32, 33, 34, 35, 36, 37, 39
                        .Col = ColAmt1
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
                        .Col = ColAmt2
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
                        '                    .Col = ColAmt3
                        '                    .CellType = SS_CELL_TYPE_FLOAT
                        '                    .BackColor = &HFFFFFF
                    Case 42, 43, 44, 45
                        .Col = ColAmt1
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
                        .Col = ColAmt2
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
                        '                    .Col = ColAmt3
                        '                    .CellType = SS_CELL_TYPE_FLOAT
                        '                    .BackColor = &HC0FFFF
                        MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColAmt3, ColAmt3)
                    Case 38, 40, 46
                        .Col = ColAmt1
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
                        .Col = ColAmt2
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
                        .Col = ColAmt3
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF)
                        MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColAmt3, ColAmt3)
                    Case 53 '47
                        .Col = ColTotal
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
                    Case 55, 56
                        .Col = ColAmt3
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)

                        If cntRow = 56 Then '50 Then
                            .Col = ColTotal
                            .CellType = SS_CELL_TYPE_FLOAT
                            .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF) ' &HFFFFFF
                            MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColTotal, ColTotal)
                        End If
                End Select
            Next

            For cntRow = 1 To .MaxRows
                .Row = cntRow
                Select Case cntRow
                    Case 47, 48, 49, 50
                        .Col = ColDesc
                        .CellType = SS_CELL_TYPE_EDIT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
                End Select
            Next
        End With
    End Sub
    Private Sub CellFormat2010()

        Dim cntRow As Integer
        With sprdIT
            .Row = 1
            .Row2 = .MaxRows
            .Col = ColAmt1
            .col2 = .MaxCols
            .BlockMode = True
            .CellType = SS_CELL_TYPE_STATIC_TEXT
            .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0C0C0)
            .BlockMode = False

            For cntRow = 1 To .MaxRows
                .Row = cntRow
                Select Case cntRow
                    Case 17, 18, 22, 23, 24
                        .Col = ColAmt1
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
                    Case 2, 3, 4, 6, 7, 8, 9, 10, 11, 12, 13
                        .Col = ColAmt2
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
                    Case 14
                        .Col = ColAmt2
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)

                        .Col = ColAmt3
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF) '&HFFFFFF
                        MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColAmt3, ColAmt3)
                    Case 25
                        .Col = ColAmt1
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)

                        .Col = ColTotal
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF) '&HFFFFFF
                        MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColTotal, ColTotal)

                    Case 5, 15
                        .Col = ColAmt3
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF) '&HFFFFFF
                        MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColAmt3, ColAmt3)
                    Case 19
                        .Col = ColAmt2
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF) '&HFFFFFF
                        MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColAmt2, ColAmt2)
                    Case 20, 26, 47, 48, 49, 50, 51, 52, 53, 54, 55
                        .Col = ColTotal
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF) ' &HFFFFFF
                        MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColTotal, ColTotal)

                    Case 30, 31, 32, 33, 34, 35, 36, 37, 39
                        .Col = ColAmt1
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
                        .Col = ColAmt2
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
                        '                    .Col = ColAmt3
                        '                    .CellType = SS_CELL_TYPE_FLOAT
                        '                    .BackColor = &HFFFFFF
                    Case 42, 43, 44, 45
                        .Col = ColAmt1
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
                        .Col = ColAmt2
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
                        '                    .Col = ColAmt3
                        '                    .CellType = SS_CELL_TYPE_FLOAT
                        '                    .BackColor = &HC0FFFF
                        MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColAmt3, ColAmt3)
                    Case 38, 40, 46
                        .Col = ColAmt1
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
                        .Col = ColAmt2
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
                        .Col = ColAmt3
                        .CellType = SS_CELL_TYPE_FLOAT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF)
                        MainClass.ProtectCell(sprdIT, cntRow, cntRow, ColAmt3, ColAmt3)
                        '                Case 54 '47
                        '                    .Col = ColTotal
                        '                    .CellType = SS_CELL_TYPE_FLOAT
                        '                    .BackColor = &HFFFFFF
                        '                Case 55, 56
                        '                    .Col = ColAmt3
                        '                    .CellType = SS_CELL_TYPE_FLOAT
                        '                    .BackColor = &HFFFFFF
                        '
                        '                    If cntRow = 56 Then '50 Then
                        '                        .Col = ColTotal
                        '                        .CellType = SS_CELL_TYPE_FLOAT
                        '                        .BackColor = &HC0FFFF     ' &HFFFFFF
                        '                        MainClass.ProtectCell sprdIT, cntRow, cntRow, ColTotal, ColTotal
                        '                    End If
                End Select
            Next

            For cntRow = 1 To .MaxRows
                .Row = cntRow
                Select Case cntRow
                    Case 47, 48, 49
                        .Col = ColDesc
                        .CellType = SS_CELL_TYPE_EDIT
                        .BackColor = System.Drawing.ColorTranslator.FromOle(&HFFFFFF)
                End Select
            Next
        End With
    End Sub
    Private Sub txtEmpCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtEmpCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim RsEmp As ADODB.Recordset = Nothing
        Dim RsDesig As ADODB.Recordset
        Dim mName As String

        If Trim(txtEmpCode.Text) = "" Then GoTo EventExitSub
        xCode = VB6.Format(Trim(txtEmpCode.Text), "000000")
        If ADDMode Then
            Clear1()
        End If
        txtEmpCode.Text = xCode

        SqlStr = " SELECT DESG_DESC, EMP.EMP_NAME,EMP_PANNO FROM " & vbCrLf & " PAY_EMPLOYEE_MST EMP, PAY_DESG_MST DESG WHERE " & vbCrLf & " EMP.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " EMP.COMPANY_CODE=DESG.COMPANY_CODE AND " & vbCrLf & " TRIM(EMP.EMP_DESG_CODE)=TRIM(DESG.DESG_CODE) AND " & vbCrLf & " EMP.Emp_Code='" & xCode & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsEmp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsEmp.EOF = False Then
            txtName.Text = IIf(IsDbNull(RsEmp.Fields("EMP_NAME").Value), "", RsEmp.Fields("EMP_NAME").Value)
            txtDesignation.Text = IIf(IsDbNull(RsEmp.Fields("DESG_DESC").Value), "", RsEmp.Fields("DESG_DESC").Value)
            txtEmpPan.Text = IIf(IsDbNull(RsEmp.Fields("EMP_PANNO").Value), "", RsEmp.Fields("EMP_PANNO").Value)

            SqlStr = " SELECT * from PAY_SalaryDef_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & xCode & "'" & vbCrLf & " AND SALARY_EFF_DATE=(SELECT MAX(SALARY_EFF_DATE) From PAY_SalaryDef_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND EMP_CODE='" & xCode & "'" & vbCrLf & " AND SALARY_EFF_DATE<= TO_DATE('" & VB6.Format(RunDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')) "

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsDesig, ADODB.LockTypeEnum.adLockOptimistic)

            If RsDesig.Fields("EMP_DESG_CODE").Value <> "" Then
                If MainClass.ValidateWithMasterTable(Trim(RsDesig.Fields("EMP_DESG_CODE").Value), "DESG_CODE", "DESG_DESC", "PAY_DESG_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
                    txtDesignation.Text = MasterNo
                End If
            End If

            CalcFromComputation((xCode))
            SqlStr = " SELECT * FROM PAY_ITFORM16_HDR WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " Emp_Code='" & xCode & "'"

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
                    SqlStr = " SELECT * FROM PAY_ITFORM16_HDR WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " EmpCode='" & xCode & "'"

                    MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsITMain, ADODB.LockTypeEnum.adLockOptimistic)

                End If
            End If
        Else
            MsgBox("Employee Name Does Not Exist In Master.", MsgBoxStyle.Information)
            Cancel = True
        End If
        Call FillGridPart()
        If RsCompany.Fields("FYEAR").Value < 2010 Then
            CalcGrid()
        Else
            CalcGrid2010()
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtFrom_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFrom.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtFrom_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtFrom.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        If Not IsDate(txtFrom.Text) Then
            MsgInformation("Invalid Date")
            Cancel = True
        End If
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtPan_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPan.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtPrevChallan_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPrevChallan.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPrevChallan_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPrevChallan.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPrevSalary_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPrevSalary.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPrevSalary_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPrevSalary.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPrevSalary_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtPrevSalary.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim mRow2 As Double
        Dim mRow3 As Double
        Dim mRow4 As Double
        Dim mRow5 As Double

        If Trim(txtPrevSalary.Text) = "" Then GoTo EventExitSub
        txtPrevSalary.Text = VB6.Format(Trim(txtPrevSalary.Text), "0.00")

        CalcFromComputation((txtEmpCode.Text))

        With sprdIT
            .Col = 3
            .Row = 2
            mRow2 = Val(.Text) - Val(txtPrevSalary.Text)
            .Text = CStr(mRow2)

            .Row = 3
            mRow3 = Val(.Text)

            .Row = 4
            mRow4 = Val(.Text)

            .Row = 5
            .Col = 4
            mRow5 = mRow2 + mRow3 + mRow4 + Val(txtPrevSalary.Text)
            .Text = CStr(mRow5) 'Gross Salary
        End With

        GoTo EventExitSub
ERR1:
        MsgBox(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtTDS_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTDS.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub



    Private Sub CalcFromComputation(ByRef mEmpCode As String)

        On Error GoTo ShowErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim RsFom12 As ADODB.Recordset
        Dim mSalaryPart As Double
        Dim mPerquisit As Double
        Dim mProfit_Lieu As Double

        SqlStr = " SELECT AMOUNT3 " & vbCrLf & " FROM PAY_ITFORM12BA_DET" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " Emp_Code='" & xCode & "' AND SUBROW=18"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsFom12, ADODB.LockTypeEnum.adLockOptimistic)

        If Not RsFom12.EOF Then
            mPerquisit = IIf(IsDbNull(RsFom12.Fields("AMOUNT3").Value), 0, RsFom12.Fields("AMOUNT3").Value)
        End If

        SqlStr = " SELECT AMOUNT3 " & vbCrLf & " FROM PAY_ITFORM12BA_DET" & vbCrLf & " WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " Emp_Code='" & xCode & "' AND SUBROW=19"

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsFom12, ADODB.LockTypeEnum.adLockOptimistic)

        If Not RsFom12.EOF Then
            mProfit_Lieu = IIf(IsDbNull(RsFom12.Fields("AMOUNT3").Value), 0, RsFom12.Fields("AMOUNT3").Value)
        End If

        SqlStr = " SELECT * FROM PAY_ITCOMP_HDR WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " Emp_Code='" & xCode & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            With sprdIT
                .Row = 2
                .Col = ColAmt2
                mSalaryPart = IIf(IsDbNull(RsTemp.Fields("GSalary_A").Value), 0, RsTemp.Fields("GSalary_A").Value) - (mPerquisit + mProfit_Lieu)
                .Text = VB6.Format(mSalaryPart, "0.00")

                .Row = 3
                .Col = ColAmt2
                .Text = VB6.Format(mPerquisit, "0.00") '' Format(IIf(IsNull(RsTemp!GSalary_B), 0, RsTemp!GSalary_B), "0.00")

                .Row = 4
                .Col = ColAmt2
                .Text = VB6.Format(mProfit_Lieu, "0.00") ''Format(IIf(IsNull(RsTemp!GSalary_C), 0, RsTemp!GSalary_C), "0.00")

                '            .Row = 12
                '            .Col = ColAmt1
                '            .Text = Format(IIf(IsNull(RsTemp!SDeduction), 0, RsTemp!SDeduction), "0.00")

                If RsCompany.Fields("FYEAR").Value < 2010 Then
                    .Row = 52 + 3 '49
                    .Col = ColAmt3
                    .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("TAX_DED").Value), 0, RsTemp.Fields("TAX_DED").Value), "0.00")
                End If

            End With
        End If
        If RsCompany.Fields("FYEAR").Value <= 2006 Then
            Call CalcFromCompDetail(mEmpCode)
        ElseIf RsCompany.Fields("FYEAR").Value < 2010 Then
            Call CalcFromCompDetailNew(mEmpCode)
        Else
            Call CalcFromCompDetail2010(mEmpCode)
        End If
        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
        'Resume
    End Sub
    Private Sub CalcFromCompDetail(ByRef mEmpCode As String)

        On Error GoTo ShowErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mADDROW As Integer

        SqlStr = " SELECT * FROM PAY_ITCOMP_TRN WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " Emp_Code='" & xCode & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            Do While Not RsTemp.EOF
                With sprdIT
                    mADDROW = 0 ''  IIf(RsCompany.Fields("COMPANY_CODE").Value = 2, 7, 0)

                    If RsTemp.Fields("SUBROWNO").Value = 31 + 2 + mADDROW Then
                        .Row = 6
                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT4").Value), 0, RsTemp.Fields("AMOUNT4").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 32 + 2 + mADDROW Then
                        .Row = 7
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT4").Value), 0, RsTemp.Fields("AMOUNT4").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 33 + 2 + mADDROW Then
                        .Row = 8

                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT4").Value), 0, RsTemp.Fields("AMOUNT4").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 34 + 2 + mADDROW Then
                        .Row = 9
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT4").Value), 0, RsTemp.Fields("AMOUNT4").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 35 + 2 + mADDROW Then
                        .Row = 10
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT4").Value), 0, RsTemp.Fields("AMOUNT4").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 36 + 2 + mADDROW Then
                        .Row = 11
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT4").Value), 0, RsTemp.Fields("AMOUNT4").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 38 + 3 + mADDROW Then
                        .Row = 17 + 2
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 39 + 3 + mADDROW Then
                        .Row = 18 + 2
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 40 + 3 + mADDROW Then
                        .Row = 19 + 2
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")
                    End If

                    '                If RsTemp!SUBROWNO = 41 + mADDROW Then
                    '                    .Row = 20 + 2
                    '                    .Col = ColDesc
                    '                    .Text = IIf(IsNull(RsTemp!Description), "", RsTemp!Description)
                    '
                    '                    .Col = ColAmt1
                    '                    .Text = Format(IIf(IsNull(RsTemp!AMOUNT1), 0, RsTemp!AMOUNT1), "0.00")
                    '                End If

                    '                If RsTemp!SUBROWNO = 42 + mADDROW Then
                    '                    .Row = 21
                    '                    .Col = ColDesc
                    '                    .Text = IIf(IsNull(RsTemp!Description), "", RsTemp!Description)
                    '
                    '                    .Col = ColAmt1
                    '                    .Text = Format(IIf(IsNull(RsTemp!AMOUNT1), 0, RsTemp!AMOUNT1), "0.00")
                    '                End If

                    '                If RsTemp!SUBROWNO = 43 + mADDROW Then
                    '                    .Row = 22
                    '                    .Col = ColDesc
                    '                    .Text = IIf(IsNull(RsTemp!Description), "", RsTemp!Description)
                    '
                    '                    .Col = ColAmt1
                    '                    .Text = Format(IIf(IsNull(RsTemp!AMOUNT1), 0, RsTemp!AMOUNT1), "0.00")
                    '
                    '                    .Col = ColTotal
                    '                    .Text = Format(IIf(IsNull(RsTemp!TotalAmount), 0, RsTemp!TotalAmount), "0.00")
                    '                End If

                    '                If RsTemp!SUBROWNO = 44 + mADDROW Then
                    '                    .Row = 23
                    '                    .Col = ColTotal
                    '                    .Text = Format(IIf(IsNull(RsTemp!TotalAmount), 0, RsTemp!TotalAmount), "0.00")
                    '                End If

                    '                If RsTemp!SUBROWNO = 45 + mADDROW Then
                    '                    .Row = 21
                    '                    .Col = ColAmt2
                    '                    .Text = Format(IIf(IsNull(RsTemp!TotalAmount), 0, RsTemp!TotalAmount), "0.00")
                    '                End If

                    If RsTemp.Fields("SUBROWNO").Value = 50 + mADDROW Then
                        .Row = 27
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT2").Value), 0, RsTemp.Fields("AMOUNT2").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 51 + mADDROW Then
                        .Row = 28
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT2").Value), 0, RsTemp.Fields("AMOUNT2").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 52 + mADDROW Then
                        .Row = 29
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT2").Value), 0, RsTemp.Fields("AMOUNT2").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 53 + mADDROW Then
                        .Row = 30
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT2").Value), 0, RsTemp.Fields("AMOUNT2").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 54 + mADDROW Then
                        .Row = 31
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT2").Value), 0, RsTemp.Fields("AMOUNT2").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 55 + mADDROW Then
                        .Row = 32
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT2").Value), 0, RsTemp.Fields("AMOUNT2").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 56 + mADDROW Then
                        .Row = 33
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT2").Value), 0, RsTemp.Fields("AMOUNT2").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 57 + mADDROW Then
                        .Row = 34
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT2").Value), 0, RsTemp.Fields("AMOUNT2").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 58 + mADDROW Then
                        .Row = 35
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT2").Value), 0, RsTemp.Fields("AMOUNT2").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 47 + mADDROW Then
                        .Row = 39
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT2").Value), 0, RsTemp.Fields("AMOUNT2").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 48 + mADDROW Then
                        .Row = 40
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT2").Value), 0, RsTemp.Fields("AMOUNT2").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 70 + mADDROW Then
                        .Row = 46
                        .Col = ColTotal
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("TotalAmount").Value), 0, RsTemp.Fields("TotalAmount").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 71 + mADDROW Then
                        .Row = 47
                        .Col = ColTotal
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("TotalAmount").Value), 0, RsTemp.Fields("TotalAmount").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 73 + mADDROW Then
                        .Row = 48 ''48 ''15-04-2008
                        .Col = ColTotal
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("TotalAmount").Value), 0, RsTemp.Fields("TotalAmount").Value), "0.00")
                    End If

                    ''15-04-2008
                    '                If RsTemp!SUBROWNO = 67 + mADDROW Then
                    '                    .Row = 46
                    '                    .Col = ColTotal
                    '                    .Text = Format(IIf(IsNull(RsTemp!TotalAmount), 0, RsTemp!TotalAmount), "0.00")
                    '                End If
                    '
                    '                If RsTemp!SUBROWNO = 68 + mADDROW Then
                    '                    .Row = 47
                    '                    .Col = ColTotal
                    '                    .Text = Format(IIf(IsNull(RsTemp!TotalAmount), 0, RsTemp!TotalAmount), "0.00")
                    '                End If

                    '                If RsTemp!SUBROWNO = 70 + mADDROW Then
                    '                    .Row = 46           ''48
                    '                    .Col = ColTotal
                    '                    .Text = Format(IIf(IsNull(RsTemp!TotalAmount), 0, RsTemp!TotalAmount), "0.00")
                    '                End If



                End With
                RsTemp.MoveNext()
            Loop
        End If

        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
        'Resume
    End Sub
    Private Sub CalcFromCompDetailNew(ByRef mEmpCode As String)

        On Error GoTo ShowErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mADDROW As Integer

        SqlStr = " SELECT * FROM PAY_ITCOMP_TRN WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " Emp_Code='" & xCode & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            Do While Not RsTemp.EOF
                With sprdIT
                    mADDROW = 0 ''  IIf(RsCompany.Fields("COMPANY_CODE").Value = 2, 7, 0)

                    If RsTemp.Fields("SUBROWNO").Value = 31 + 2 + mADDROW Then
                        .Row = 6
                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT4").Value), 0, RsTemp.Fields("AMOUNT4").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 32 + 2 + mADDROW Then
                        .Row = 7
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT4").Value), 0, RsTemp.Fields("AMOUNT4").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 33 + 2 + mADDROW Then
                        .Row = 8

                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT4").Value), 0, RsTemp.Fields("AMOUNT4").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 34 + 2 + mADDROW Then
                        .Row = 9
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT4").Value), 0, RsTemp.Fields("AMOUNT4").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 35 + 2 + mADDROW Then
                        .Row = 10
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT4").Value), 0, RsTemp.Fields("AMOUNT4").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 36 + 2 + mADDROW Then
                        .Row = 11
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT4").Value), 0, RsTemp.Fields("AMOUNT4").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 37 + 2 + mADDROW Then
                        .Row = 12
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT4").Value), 0, RsTemp.Fields("AMOUNT4").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 38 + 2 + mADDROW Then
                        .Row = 13
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT4").Value), 0, RsTemp.Fields("AMOUNT4").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 39 + 2 + mADDROW Then
                        .Row = 14
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT4").Value), 0, RsTemp.Fields("AMOUNT4").Value), "0.00")
                    End If
                    ''mADDROW = 3
                    If RsTemp.Fields("SUBROWNO").Value = 44 + mADDROW Then
                        .Row = 17 + 5
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 45 + mADDROW Then
                        .Row = 18 + 5
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 46 + mADDROW Then
                        .Row = 19 + 5
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")
                    End If


                    If RsTemp.Fields("SUBROWNO").Value = 53 + mADDROW Then
                        .Row = 29
                        '                    .Col = ColDesc
                        '                    .Text = IIf(IsNull(RsTemp!Description), "", RsTemp!Description)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT2").Value), 0, RsTemp.Fields("AMOUNT2").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 54 + mADDROW Then
                        .Row = 30
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT2").Value), 0, RsTemp.Fields("AMOUNT2").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 55 + mADDROW Then
                        .Row = 31
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT2").Value), 0, RsTemp.Fields("AMOUNT2").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 56 + mADDROW Then
                        .Row = 32
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT2").Value), 0, RsTemp.Fields("AMOUNT2").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 57 + mADDROW Then
                        .Row = 33
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT2").Value), 0, RsTemp.Fields("AMOUNT2").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 58 + mADDROW Then
                        .Row = 34
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT2").Value), 0, RsTemp.Fields("AMOUNT2").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 59 + mADDROW Then
                        .Row = 35
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT2").Value), 0, RsTemp.Fields("AMOUNT2").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 60 + mADDROW Then
                        .Row = 36
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT2").Value), 0, RsTemp.Fields("AMOUNT2").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 61 + mADDROW Then
                        .Row = 37
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT2").Value), 0, RsTemp.Fields("AMOUNT2").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 62 + mADDROW Then
                        .Row = 38
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT2").Value), 0, RsTemp.Fields("AMOUNT2").Value), "0.00")
                    End If
                    If RsTemp.Fields("SUBROWNO").Value = 62 + mADDROW Then
                        .Row = 38

                        .Col = ColAmt3
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT2").Value), 0, RsTemp.Fields("AMOUNT2").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 50 + mADDROW Then
                        .Row = 42
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT2").Value), 0, RsTemp.Fields("AMOUNT2").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 51 + mADDROW Then
                        .Row = 43
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT2").Value), 0, RsTemp.Fields("AMOUNT2").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 52 + mADDROW Then
                        .Row = 44
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT2").Value), 0, RsTemp.Fields("AMOUNT2").Value), "0.00")
                    End If

                    '                If RsTemp!SUBROWNO = 63 + mADDROW Then
                    '                    .Row = 40
                    '                    .Col = ColDesc
                    '                    .Text = IIf(IsNull(RsTemp!Description), "", RsTemp!Description)
                    '
                    '                    .Col = ColAmt1
                    '                    .Text = Format(IIf(IsNull(RsTemp!AMOUNT1), 0, RsTemp!AMOUNT1), "0.00")
                    '
                    '                    .Col = ColAmt2
                    '                    .Text = Format(IIf(IsNull(RsTemp!AMOUNT2), 0, RsTemp!AMOUNT2), "0.00")
                    '                End If

                    '                If RsTemp!SUBROWNO = 70 + mADDROW Then
                    '                    .Row = 46
                    '                    .Col = ColTotal
                    '                    .Text = Format(IIf(IsNull(RsTemp!TotalAmount), 0, RsTemp!TotalAmount), "0.00")
                    '                End If
                    '
                    '                If RsTemp!SUBROWNO = 71 + mADDROW Then
                    '                    .Row = 47
                    '                    .Col = ColTotal
                    '                    .Text = Format(IIf(IsNull(RsTemp!TotalAmount), 0, RsTemp!TotalAmount), "0.00")
                    '                End If
                    '
                    '                If RsTemp!SUBROWNO = 73 + mADDROW Then
                    '                    .Row = 48           ''48 ''15-04-2008
                    '                    .Col = ColTotal
                    '                    .Text = Format(IIf(IsNull(RsTemp!TotalAmount), 0, RsTemp!TotalAmount), "0.00")
                    '                End If

                    ''15-04-2008
                    If RsTemp.Fields("SUBROWNO").Value = 64 + mADDROW Then
                        .Row = 47
                        .Col = ColTotal
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("TotalAmount").Value), 0, RsTemp.Fields("TotalAmount").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 71 + mADDROW Then
                        .Row = 49
                        .Col = ColTotal
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("TotalAmount").Value), 0, RsTemp.Fields("TotalAmount").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 72 + mADDROW Then
                        .Row = 50 ''48
                        .Col = ColTotal
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("TotalAmount").Value), 0, RsTemp.Fields("TotalAmount").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 74 + mADDROW Then
                        .Row = 51 ''48
                        .Col = ColTotal
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("TotalAmount").Value), 0, RsTemp.Fields("TotalAmount").Value), "0.00")
                    End If

                End With
                RsTemp.MoveNext()
            Loop
        End If

        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
        'Resume
    End Sub
    Private Sub CalcFromCompDetail2010(ByRef mEmpCode As String)

        On Error GoTo ShowErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mADDROW As Integer

        SqlStr = " SELECT * FROM PAY_ITCOMP_TRN WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " Emp_Code='" & xCode & "'"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            Do While Not RsTemp.EOF
                With sprdIT
                    mADDROW = 0 ''  IIf(RsCompany.Fields("COMPANY_CODE").Value = 2, 7, 0)

                    If RsTemp.Fields("SUBROWNO").Value = 31 + 2 + mADDROW Then
                        .Row = 6
                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT4").Value), 0, RsTemp.Fields("AMOUNT4").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 32 + 2 + mADDROW Then
                        .Row = 7
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT4").Value), 0, RsTemp.Fields("AMOUNT4").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 33 + 2 + mADDROW Then
                        .Row = 8

                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT4").Value), 0, RsTemp.Fields("AMOUNT4").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 34 + 2 + mADDROW Then
                        .Row = 9
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT4").Value), 0, RsTemp.Fields("AMOUNT4").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 35 + 2 + mADDROW Then
                        .Row = 10
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT4").Value), 0, RsTemp.Fields("AMOUNT4").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 36 + 2 + mADDROW Then
                        .Row = 11
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT4").Value), 0, RsTemp.Fields("AMOUNT4").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 37 + 2 + mADDROW Then
                        .Row = 12
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT4").Value), 0, RsTemp.Fields("AMOUNT4").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 38 + 2 + mADDROW Then
                        .Row = 13
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT4").Value), 0, RsTemp.Fields("AMOUNT4").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 39 + 2 + mADDROW Then
                        .Row = 14
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT4").Value), 0, RsTemp.Fields("AMOUNT4").Value), "0.00")
                    End If
                    ''mADDROW = 3
                    If RsTemp.Fields("SUBROWNO").Value = 44 + mADDROW Then
                        .Row = 17 + 5
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 45 + mADDROW Then
                        .Row = 18 + 5
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 46 + mADDROW Then
                        .Row = 19 + 5
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")
                    End If


                    If RsTemp.Fields("SUBROWNO").Value = 53 + mADDROW Then
                        .Row = 29
                        '                    .Col = ColDesc
                        '                    .Text = IIf(IsNull(RsTemp!Description), "", RsTemp!Description)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT2").Value), 0, RsTemp.Fields("AMOUNT2").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 54 + mADDROW Then
                        .Row = 30
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT2").Value), 0, RsTemp.Fields("AMOUNT2").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 55 + mADDROW Then
                        .Row = 31
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT2").Value), 0, RsTemp.Fields("AMOUNT2").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 56 + mADDROW Then
                        .Row = 32
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT2").Value), 0, RsTemp.Fields("AMOUNT2").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 57 + mADDROW Then
                        .Row = 33
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT2").Value), 0, RsTemp.Fields("AMOUNT2").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 58 + mADDROW Then
                        .Row = 34
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT2").Value), 0, RsTemp.Fields("AMOUNT2").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 59 + mADDROW Then
                        .Row = 35
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT2").Value), 0, RsTemp.Fields("AMOUNT2").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 60 + mADDROW Then
                        .Row = 36
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT2").Value), 0, RsTemp.Fields("AMOUNT2").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 61 + mADDROW Then
                        .Row = 37
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT2").Value), 0, RsTemp.Fields("AMOUNT2").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 62 + mADDROW Then
                        .Row = 38
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT2").Value), 0, RsTemp.Fields("AMOUNT2").Value), "0.00")
                    End If
                    If RsTemp.Fields("SUBROWNO").Value = 62 + mADDROW Then
                        .Row = 38

                        .Col = ColAmt3
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT2").Value), 0, RsTemp.Fields("AMOUNT2").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 50 + mADDROW Then
                        .Row = 42
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT2").Value), 0, RsTemp.Fields("AMOUNT2").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 51 + mADDROW Then
                        .Row = 43
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT2").Value), 0, RsTemp.Fields("AMOUNT2").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 52 + mADDROW Then
                        .Row = 44
                        .Col = ColDesc
                        .Text = IIf(IsDbNull(RsTemp.Fields("Description").Value), "", RsTemp.Fields("Description").Value)

                        .Col = ColAmt1
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")

                        .Col = ColAmt2
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT2").Value), 0, RsTemp.Fields("AMOUNT2").Value), "0.00")
                    End If

                    '                If RsTemp!SUBROWNO = 63 + mADDROW Then
                    '                    .Row = 40
                    '                    .Col = ColDesc
                    '                    .Text = IIf(IsNull(RsTemp!Description), "", RsTemp!Description)
                    '
                    '                    .Col = ColAmt1
                    '                    .Text = Format(IIf(IsNull(RsTemp!AMOUNT1), 0, RsTemp!AMOUNT1), "0.00")
                    '
                    '                    .Col = ColAmt2
                    '                    .Text = Format(IIf(IsNull(RsTemp!AMOUNT2), 0, RsTemp!AMOUNT2), "0.00")
                    '                End If

                    '                If RsTemp!SUBROWNO = 70 + mADDROW Then
                    '                    .Row = 46
                    '                    .Col = ColTotal
                    '                    .Text = Format(IIf(IsNull(RsTemp!TotalAmount), 0, RsTemp!TotalAmount), "0.00")
                    '                End If
                    '
                    '                If RsTemp!SUBROWNO = 71 + mADDROW Then
                    '                    .Row = 47
                    '                    .Col = ColTotal
                    '                    .Text = Format(IIf(IsNull(RsTemp!TotalAmount), 0, RsTemp!TotalAmount), "0.00")
                    '                End If
                    '
                    '                If RsTemp!SUBROWNO = 73 + mADDROW Then
                    '                    .Row = 48           ''48 ''15-04-2008
                    '                    .Col = ColTotal
                    '                    .Text = Format(IIf(IsNull(RsTemp!TotalAmount), 0, RsTemp!TotalAmount), "0.00")
                    '                End If

                    ''15-04-2008
                    If RsTemp.Fields("SUBROWNO").Value = 64 + mADDROW Then
                        .Row = 47
                        .Col = ColTotal
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("TotalAmount").Value), 0, RsTemp.Fields("TotalAmount").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 71 + mADDROW Then
                        .Row = 49
                        .Col = ColTotal
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("TotalAmount").Value), 0, RsTemp.Fields("TotalAmount").Value), "0.00")
                    End If

                    If RsTemp.Fields("SUBROWNO").Value = 74 + mADDROW Then
                        .Row = 50 ''48
                        .Col = ColTotal
                        .Text = VB6.Format(IIf(IsDbNull(RsTemp.Fields("TotalAmount").Value), 0, RsTemp.Fields("TotalAmount").Value), "0.00")
                    End If

                    If RsCompany.Fields("FYEAR").Value >= 2013 Then
                        If RsTemp.Fields("SUBROWNO").Value = 73 + mADDROW Then
                            .Row = 53 ''48
                            .Col = ColTotal
                            .Text = CStr(-1 * CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("AMOUNT1").Value), 0, RsTemp.Fields("AMOUNT1").Value), "0.00")))
                        End If
                    End If

                    '                If RsTemp!SUBROWNO = 74 + mADDROW Then
                    '                    .Row = 51           ''48
                    '                    .Col = ColTotal
                    '                    .Text = Format(IIf(IsNull(RsTemp!TotalAmount), 0, RsTemp!TotalAmount), "0.00")
                    '                End If

                End With
                RsTemp.MoveNext()
            Loop
        End If

        Exit Sub
ShowErrPart:
        MsgBox(Err.Description)
        'Resume
    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPreview.Click
        On Error GoTo ErrPart
        Dim mPrintType As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        frmPrintForm16.ShowDialog()

        If frmPrintForm16.OptPrint(0).Checked = True Then
            mPrintType = "1"
        ElseIf frmPrintForm16.OptPrint(1).Checked = True Then
            mPrintType = "2"
        ElseIf frmPrintForm16.OptPrint(2).Checked = True Then
            mPrintType = "3"
        Else
            mPrintType = "4"
        End If

        If G_PrintLedg = False Then
            Exit Sub
        End If

        Call ReportForPrint(Crystal.DestinationConstants.crptToWindow, mPrintType)

        frmPrintForm16.Close()

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        frmPrintForm16.Close()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        On Error GoTo ErrPart
        Dim mPrintType As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        frmPrintForm16.ShowDialog()

        If frmPrintForm16.OptPrint(0).Checked = True Then
            mPrintType = "1"
        ElseIf frmPrintForm16.OptPrint(1).Checked = True Then
            mPrintType = "2"
        ElseIf frmPrintForm16.OptPrint(2).Checked = True Then
            mPrintType = "3"
        Else
            mPrintType = "4"
        End If

        If G_PrintLedg = False Then
            Exit Sub
        End If

        Call ReportForPrint(Crystal.DestinationConstants.crptToPrinter, mPrintType)

        frmPrintForm16.Close()

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        frmPrintForm16.Close()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub ReportForPrint(ByRef Mode As Crystal.DestinationConstants, ByRef mPrintType As String)


        On Error GoTo ERR1
        Dim SqlStr As String = ""
        Dim mTitle As String
        Dim mSubTitle As String
        Dim mCode As Integer
        Dim mRptFileName As String

        'Insert Data from Grid to PrintDummyData Table...


        If FillPrintDummyData(sprdIT, 1, sprdIT.MaxRows, 0, sprdIT.MaxCols, PubDBCn) = False Then GoTo ERR1

        PubDBCn.BeginTrans()

        If Val(txtPrevSalary.Text) <> 0 Then
            SqlStr = " INSERT INTO TEMP_PRINTDUMMYDATA (USERID, SUBROW, " & vbCrLf & " FIELD1,FIELD2,FIELD4) " & vbCrLf & " VALUES (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',1.1, " & vbCrLf & " '', " & vbCrLf & " '*  Salary Recd from Previous Employer (as per Form 16 summitted by the employee ) : ', " & vbCrLf & " '" & VB6.Format(txtPrevSalary.Text, "0.00") & "') "
            PubDBCn.Execute(SqlStr)
        End If

        SqlStr = " UPDATE TEMP_PrintDummyData set FIELD10='M' WHERE UserID='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'"
        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()

        'Select Record for print...

        SqlStr = ""

        SqlStr = FetchRecordForReport(SqlStr)

        mSubTitle = ""
        mTitle = "Form No. 16"
        If RsCompany.Fields("FYEAR").Value >= 2010 Then
            If FillITChallan("Q1") = False Then GoTo ERR1
            If FillITChallan("Q2") = False Then GoTo ERR1
            If FillITChallan("Q3") = False Then GoTo ERR1
            If FillITChallan("Q4") = False Then GoTo ERR1
            If FillITChallan("S") = False Then GoTo ERR1
            If mPrintType = "1" Then
                mRptFileName = "ITForm16New_2010.Rpt"
            ElseIf mPrintType = "2" Then
                mRptFileName = "ITForm16New_PartA.Rpt"
            ElseIf mPrintType = "3" Then
                mRptFileName = "ITForm16New_PartB.Rpt"
            Else
                mRptFileName = "ITForm16New_AnnxB.Rpt"
            End If
            Call ShowReport(SqlStr, mRptFileName, Mode, mTitle, mSubTitle)
        Else
            If FillITChallan("S") = False Then GoTo ERR1
            Call ShowReport(SqlStr, "ITForm16New.Rpt", Mode, mTitle, mSubTitle)
        End If

        '    If PubCurrDate < CDate("01/04/2009") Then
        '        Call ShowReport(SqlStr, "ITForm16New.Rpt", Mode, mTitle, mSubTitle)
        '    Else
        '        Call ShowReport(SqlStr, "ITForm16_2009.Rpt", Mode, mTitle, mSubTitle)
        '    End If

        Report1.ReportFileName = ""

        Exit Sub
ERR1:
        MsgInformation(Err.Description)
        If Err.Number = 32755 Or Err.Number = 20507 Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        PubDBCn.RollbackTrans()
        'Resume
    End Sub
    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mRPTName As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)

        Dim mCode As Integer
        Dim SqlStrSub As String
        Dim mRegdAddress As String
        Dim mAuthoName As String
        Dim mAuthoDesg As String
        Dim mAuthoFName As String
        Dim mAuthoSign As String
        Dim xSqlStr As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mAckNo1QTR As String
        Dim mAckNo2QTR As String
        Dim mAckNo3QTR As String
        Dim mAckNo4QTR As String

        Dim mAckNo1QTRDate As String
        Dim mAckNo2QTRDate As String
        Dim mAckNo3QTRDate As String
        Dim mAckNo4QTRDate As String
        Dim mQtrNo As Integer
        Dim mCITName As String
        Dim mCITAddress As String
        Dim mCITCity As String
        Dim mCITPincode As String

        Report1.SQLQuery = mSqlStr

        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle)


        xSqlStr = " SELECT * FROM PAY_RTN_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND FYEAR=" & RsCompany.Fields("FYEAR").Value & ""
        MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)

        If RsTemp.EOF = False Then
            mAckNo1QTRDate = VB6.Format(IIf(IsDbNull(RsTemp.Fields("I_QTR_DATE").Value), "", RsTemp.Fields("I_QTR_DATE").Value), "DD/MM/YYYY")
            mAckNo2QTRDate = VB6.Format(IIf(IsDbNull(RsTemp.Fields("II_QTR_DATE").Value), "", RsTemp.Fields("II_QTR_DATE").Value), "DD/MM/YYYY")
            mAckNo3QTRDate = VB6.Format(IIf(IsDbNull(RsTemp.Fields("III_QTR_DATE").Value), "", RsTemp.Fields("III_QTR_DATE").Value), "DD/MM/YYYY")
            mAckNo4QTRDate = VB6.Format(IIf(IsDbNull(RsTemp.Fields("IV_QTR_DATE").Value), "", RsTemp.Fields("IV_QTR_DATE").Value), "DD/MM/YYYY")

            mAckNo1QTR = IIf(IsDbNull(RsTemp.Fields("I_QTR_NO").Value), "", RsTemp.Fields("I_QTR_NO").Value)
            mAckNo2QTR = IIf(IsDbNull(RsTemp.Fields("II_QTR_NO").Value), "", RsTemp.Fields("II_QTR_NO").Value)
            mAckNo3QTR = IIf(IsDbNull(RsTemp.Fields("III_QTR_NO").Value), "", RsTemp.Fields("III_QTR_NO").Value)
            mAckNo4QTR = IIf(IsDbNull(RsTemp.Fields("IV_QTR_NO").Value), "", RsTemp.Fields("IV_QTR_NO").Value)


            If RsCompany.Fields("FYEAR").Value < 2010 Then
                If mAckNo1QTRDate <> "" Then
                    If CDate(txtTo.Text) < CDate(mAckNo1QTRDate) Then
                        mAckNo1QTR = ""
                        mAckNo2QTR = ""
                        mAckNo3QTR = ""
                        mAckNo4QTR = ""
                    End If
                End If

                If mAckNo2QTRDate <> "" Then
                    If CDate(txtTo.Text) < CDate(mAckNo2QTRDate) Then
                        mAckNo2QTR = ""
                        mAckNo3QTR = ""
                        mAckNo4QTR = ""
                    End If
                End If

                If mAckNo3QTRDate <> "" Then
                    If CDate(txtTo.Text) < CDate(mAckNo3QTRDate) Then
                        mAckNo3QTR = ""
                        mAckNo4QTR = ""
                    End If
                End If

                If mAckNo4QTRDate <> "" Then
                    If CDate(txtTo.Text) < CDate(mAckNo4QTRDate) Then
                        mAckNo4QTR = ""
                    End If
                End If
            End If
        End If

        MainClass.AssignCRptFormulas(Report1, "AckNo1QTR='" & mAckNo1QTR & "'")
        MainClass.AssignCRptFormulas(Report1, "AckNo2QTR='" & mAckNo2QTR & "'")
        MainClass.AssignCRptFormulas(Report1, "AckNo3QTR='" & mAckNo3QTR & "'")
        MainClass.AssignCRptFormulas(Report1, "AckNo4QTR='" & mAckNo4QTR & "'")



        MainClass.AssignCRptFormulas(Report1, "Com_Pan='" & txtPan.Text & "'")
        MainClass.AssignCRptFormulas(Report1, "Com_Tan='" & txtTan.Text & "'")
        MainClass.AssignCRptFormulas(Report1, "Name='" & txtName.Text & "'")
        MainClass.AssignCRptFormulas(Report1, "Designation='" & txtDesignation.Text & "'")
        MainClass.AssignCRptFormulas(Report1, "Emp_Pan='" & txtEmpPan.Text & "'")
        MainClass.AssignCRptFormulas(Report1, "TDSCircle='" & txtTDS.Text & "'")
        MainClass.AssignCRptFormulas(Report1, "FromDate='" & txtFrom.Text & "'")
        MainClass.AssignCRptFormulas(Report1, "ToDate='" & txtTo.Text & "'")
        MainClass.AssignCRptFormulas(Report1, "AYear='" & txtAYear.Text & "'")

        mRegdAddress = IIf(IsDbNull(RsCompany.Fields("REGD_ADDR1").Value), "", RsCompany.Fields("REGD_ADDR1").Value)
        mRegdAddress = mRegdAddress & " " & IIf(IsDbNull(RsCompany.Fields("REGD_ADDR2").Value), "", RsCompany.Fields("REGD_ADDR2").Value)
        mRegdAddress = mRegdAddress & " " & IIf(IsDbNull(RsCompany.Fields("REGD_CITY").Value), "", RsCompany.Fields("REGD_CITY").Value)
        mRegdAddress = mRegdAddress & " - " & IIf(IsDbNull(RsCompany.Fields("REGD_PIN").Value), "", RsCompany.Fields("REGD_PIN").Value)

        MainClass.AssignCRptFormulas(Report1, "Companyaddress=""" & mRegdAddress & """")

        mAuthoName = IIf(IsDbNull(RsCompany.Fields("TDSAUTHORIZED").Value), "", RsCompany.Fields("TDSAUTHORIZED").Value)
        mAuthoFName = IIf(IsDbNull(RsCompany.Fields("TDSAUTHORIZED_FNAME").Value), "", RsCompany.Fields("TDSAUTHORIZED_FNAME").Value)
        mAuthoDesg = IIf(IsDbNull(RsCompany.Fields("TDSAUTHORIZED_DESIG").Value), "", RsCompany.Fields("TDSAUTHORIZED_DESIG").Value)
        mAuthoSign = mAuthoName & " (" & mAuthoDesg & ")"


        MainClass.AssignCRptFormulas(Report1, "mAuthoName='" & mAuthoName & "'")
        MainClass.AssignCRptFormulas(Report1, "mAuthoFName='" & mAuthoFName & "'")
        MainClass.AssignCRptFormulas(Report1, "mAuthoDesg='" & mAuthoDesg & "'")
        MainClass.AssignCRptFormulas(Report1, "mAuthoSign='" & mAuthoSign & "'")

        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\" & mRPTName

        ' Report1.CopiesToPrinter = PrintCopies
        Report1.WindowShowGroupTree = False

        If RsCompany.Fields("FYEAR").Value >= 2010 Then
            xSqlStr = " SELECT CIRCLE_NAME,CIRCLE_ADDRESS,CIRCLE_CITY,CIRCLE_PINCODE " & vbCrLf & " FROM FIN_PRINT_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "
            MainClass.UOpenRecordSet(xSqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp)

            If RsTemp.EOF = False Then
                mCITName = IIf(IsDbNull(RsTemp.Fields("CIRCLE_NAME").Value), "", RsTemp.Fields("CIRCLE_NAME").Value)
                mCITAddress = IIf(IsDbNull(RsTemp.Fields("CIRCLE_ADDRESS").Value), "", RsTemp.Fields("CIRCLE_ADDRESS").Value)
                mCITCity = IIf(IsDbNull(RsTemp.Fields("CIRCLE_CITY").Value), "", RsTemp.Fields("CIRCLE_CITY").Value)
                mCITPincode = IIf(IsDbNull(RsTemp.Fields("CIRCLE_PINCODE").Value), "", RsTemp.Fields("CIRCLE_PINCODE").Value)
            End If

            MainClass.AssignCRptFormulas(Report1, "mCITName='" & mCITName & "'")
            MainClass.AssignCRptFormulas(Report1, "mCITAddress='" & mCITAddress & "'")
            MainClass.AssignCRptFormulas(Report1, "mCITCity='" & mCITCity & "'")
            MainClass.AssignCRptFormulas(Report1, "mCITPincode='" & mCITPincode & "'")

            SqlStrSub = " SELECT * FROM TEMP_PRINTDUMMYDATA PRINTDUMMYDATA " & vbCrLf & " WHERE " & vbCrLf & " UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " AND FIELD10 IN ('Q1','Q2','Q3','Q4')" & vbCrLf & " ORDER BY FIELD10"

            Report1.SubreportToChange = ""
            Report1.SubreportToChange = Report1.GetNthSubreportName(0)
            Report1.Connect = STRRptConn
            Report1.SQLQuery = SqlStrSub

            MainClass.AssignCRptFormulas(Report1, "AckNo1QTR='" & mAckNo1QTR & "'")
            MainClass.AssignCRptFormulas(Report1, "AckNo2QTR='" & mAckNo2QTR & "'")
            MainClass.AssignCRptFormulas(Report1, "AckNo3QTR='" & mAckNo3QTR & "'")
            MainClass.AssignCRptFormulas(Report1, "AckNo4QTR='" & mAckNo4QTR & "'")

            Report1.SubreportToChange = ""

            SqlStrSub = " SELECT * FROM TEMP_PRINTDUMMYDATA PRINTDUMMYDATA " & vbCrLf & " WHERE " & vbCrLf & " UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " AND FIELD10 ='S'" & vbCrLf & " ORDER BY SUBROW"

            Report1.SubreportToChange = Report1.GetNthSubreportName(1)
            Report1.Connect = STRRptConn
            Report1.SQLQuery = SqlStrSub

            Report1.SubreportToChange = ""

        Else
            SqlStrSub = " SELECT * FROM TEMP_PRINTDUMMYDATA PRINTDUMMYDATA " & vbCrLf & " WHERE " & vbCrLf & " UPPER(UserID)='" & MainClass.AllowSingleQuote(UCase(PubUserID)) & "'" & vbCrLf & " AND FIELD10 ='S'" & vbCrLf & " ORDER BY SUBROW"

            Report1.SubreportToChange = Report1.GetNthSubreportName(0)
            Report1.Connect = STRRptConn
            Report1.SQLQuery = SqlStrSub

            Report1.SubreportToChange = ""
        End If



        Report1.Action = 1
    End Sub
    Public Function FillITChallan(ByRef pChallanType As String) As Boolean

        ' This procedure fills the Grid Data into PrintDummy table for printing...
        On Error GoTo PrintDummyErr

        Dim RSPrintDummy As ADODB.Recordset
        Dim SqlStr As String = ""
        Dim mCode As String
        Dim RowNum As Integer

        Dim mTotPaidAmount As Double
        Dim mTotPaidAmountStr As String
        Dim mTDSAmount As Double
        Dim mSurchargeAmount As Double
        Dim mCESSAmount As Double
        Dim mTotalAmount As Double
        Dim mChequeNo As String
        Dim mBSRCode As String
        Dim mPAYMENTDATE As String
        Dim mChallanNo As String
        Dim mAuthoName As String
        Dim mAuthoFName As String
        Dim mAuthoDesg As String

        Dim mFromEmpCompany As Integer
        Dim mFromEmpCode As String
        Dim mFromEmpLeaveDate As String
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mIsInsert As Boolean
        Dim mBookType As String
        Dim mToEmpCompany As Integer
        Dim mToEmpCode As String
        Dim mFromDate As String
        Dim mToDate As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If pChallanType = "Q1" Then
            mFromDate = "01/04/" & VB6.Format(RsCompany.Fields("START_DATE").Value, "YYYY")
            mToDate = "30/06/" & VB6.Format(RsCompany.Fields("START_DATE").Value, "YYYY")
        ElseIf pChallanType = "Q2" Then
            mFromDate = "01/07/" & VB6.Format(RsCompany.Fields("START_DATE").Value, "YYYY")
            mToDate = "30/09/" & VB6.Format(RsCompany.Fields("START_DATE").Value, "YYYY")
        ElseIf pChallanType = "Q3" Then
            mFromDate = "01/10/" & VB6.Format(RsCompany.Fields("START_DATE").Value, "YYYY")
            mToDate = "31/12/" & VB6.Format(RsCompany.Fields("START_DATE").Value, "YYYY")
        ElseIf pChallanType = "Q4" Then
            mFromDate = "01/01/" & VB6.Format(RsCompany.Fields("END_DATE").Value, "YYYY")
            mToDate = "31/03/" & VB6.Format(RsCompany.Fields("END_DATE").Value, "YYYY")
        Else
            mFromDate = RsCompany.Fields("START_DATE").Value
            mToDate = RsCompany.Fields("END_DATE").Value
        End If

        mIsInsert = False
        If MainClass.ValidateWithMasterTable((txtEmpCode.Text), "EMP_CODE", "EMP_CODE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = True Then
            mCode = MasterNo
        End If

        mAuthoName = IIf(IsDbNull(RsCompany.Fields("TDSAUTHORIZED").Value), "", RsCompany.Fields("TDSAUTHORIZED").Value)
        mAuthoFName = IIf(IsDbNull(RsCompany.Fields("TDSAUTHORIZED_FNAME").Value), "", RsCompany.Fields("TDSAUTHORIZED_FNAME").Value)
        mAuthoDesg = IIf(IsDbNull(RsCompany.Fields("TDSAUTHORIZED_DESIG").Value), "", RsCompany.Fields("TDSAUTHORIZED_DESIG").Value)

        SqlStr = " SELECT  SUM(ID.AMOUNT) AS TOT_AMOUNT " & vbCrLf & " FROM PAY_ITChallan_HDR IH, PAY_ITChallan_DET ID WHERE " & vbCrLf & " IH.AUTO_KEY_REFNO=ID.AUTO_KEY_REFNO" & vbCrLf & " AND IH.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " IH.FYEAR = " & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " ID.EMP_CODE = '" & mCode & "' "

        SqlStr = SqlStr & vbCrLf & " AND IH.VDATE>=TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND IH.VDATE<=TO_DATE('" & VB6.Format(mToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " ORDER BY IH.VDate "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSPrintDummy, ADODB.LockTypeEnum.adLockReadOnly)
        If RSPrintDummy.EOF = False Then
            mTotPaidAmount = IIf(IsDbNull(RSPrintDummy.Fields("TOT_AMOUNT").Value), 0, RSPrintDummy.Fields("TOT_AMOUNT").Value)
        End If

        'Transfer Emp Data ...........

        '    SqlStr = " SELECT * " & vbCrLf _
        ''            & " FROM PAY_EMP_TRF_MST" & vbCrLf _
        ''            & " WHERE " & vbCrLf _
        ''            & " TO_COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
        ''            & " AND TO_EMP_CODE = '" & mCode & "'"

        mToEmpCompany = RsCompany.Fields("COMPANY_CODE").Value
        mToEmpCode = mCode

SearchRow:
        SqlStr = GetEmpTransferSQL(mToEmpCode, mToEmpCompany)

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            mFromEmpCompany = IIf(IsDbNull(RsTemp.Fields("FROM_COMPANY_CODE").Value), "", RsTemp.Fields("FROM_COMPANY_CODE").Value)
            mFromEmpCode = IIf(IsDbNull(RsTemp.Fields("FROM_EMP_CODE").Value), "", RsTemp.Fields("FROM_EMP_CODE").Value)

            If MainClass.ValidateWithMasterTable(mFromEmpCode, "EMP_CODE", "EMP_LEAVE_DATE", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & mFromEmpCompany & "") = True Then
                mFromEmpLeaveDate = VB6.Format(Trim(MasterNo), "DD/MM/YYYY")
            End If

            SqlStr = " SELECT  SUM(ID.AMOUNT) AS TOT_AMOUNT " & vbCrLf & " FROM PAY_ITChallan_HDR IH, PAY_ITChallan_DET ID WHERE " & vbCrLf & " IH.AUTO_KEY_REFNO=ID.AUTO_KEY_REFNO" & vbCrLf & " AND IH.COMPANY_CODE = " & mFromEmpCompany & " AND " & vbCrLf & " IH.FYEAR = " & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " ID.EMP_CODE = '" & mFromEmpCode & "' "

            SqlStr = SqlStr & vbCrLf & " AND IH.VDATE>=TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND IH.VDATE<=TO_DATE('" & VB6.Format(mToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            SqlStr = SqlStr & vbCrLf & " ORDER BY IH.VDate "

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSPrintDummy, ADODB.LockTypeEnum.adLockReadOnly)
            If RSPrintDummy.EOF = False Then
                mTotPaidAmount = mTotPaidAmount + IIf(IsDbNull(RSPrintDummy.Fields("TOT_AMOUNT").Value), 0, RSPrintDummy.Fields("TOT_AMOUNT").Value)
            End If

            SqlStr = " SELECT  ID.TDS_AMOUNT, ID.SURCHARGE_AMT, ID.CESS_AMT, " & vbCrLf & " ID.AMOUNT, IH.CHQ_NO, IH.BSRCODE, IH.CHALLANDATE, " & vbCrLf & " IH.CHALLANNO, IH.BOOKTYPE " & vbCrLf & " FROM PAY_ITChallan_HDR IH, PAY_ITChallan_DET ID WHERE " & vbCrLf & " IH.AUTO_KEY_REFNO=ID.AUTO_KEY_REFNO" & vbCrLf & " AND IH.COMPANY_CODE = " & mFromEmpCompany & " AND " & vbCrLf & " IH.FYEAR = " & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " ID.EMP_CODE = '" & mFromEmpCode & "' "

            SqlStr = SqlStr & vbCrLf & " AND IH.VDATE>=TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND IH.VDATE<=TO_DATE('" & VB6.Format(mToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

            SqlStr = SqlStr & vbCrLf & " ORDER BY IH.VDate "

            MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSPrintDummy, ADODB.LockTypeEnum.adLockReadOnly)

            If RSPrintDummy.EOF = False Then

                Do While Not RSPrintDummy.EOF
                    mTDSAmount = IIf(IsDbNull(RSPrintDummy.Fields("TDS_AMOUNT").Value), 0, RSPrintDummy.Fields("TDS_AMOUNT").Value)
                    mSurchargeAmount = IIf(IsDbNull(RSPrintDummy.Fields("SURCHARGE_AMT").Value), 0, RSPrintDummy.Fields("SURCHARGE_AMT").Value)
                    mCESSAmount = IIf(IsDbNull(RSPrintDummy.Fields("CESS_AMT").Value), 0, RSPrintDummy.Fields("CESS_AMT").Value)
                    mTotalAmount = IIf(IsDbNull(RSPrintDummy.Fields("Amount").Value), 0, RSPrintDummy.Fields("Amount").Value)

                    mChequeNo = IIf(IsDbNull(RSPrintDummy.Fields("CHQ_NO").Value), "", RSPrintDummy.Fields("CHQ_NO").Value)
                    mBSRCode = IIf(IsDbNull(RSPrintDummy.Fields("BSRCODE").Value), "", RSPrintDummy.Fields("BSRCODE").Value)
                    mPAYMENTDATE = IIf(IsDbNull(RSPrintDummy.Fields("CHALLANDATE").Value), "", RSPrintDummy.Fields("CHALLANDATE").Value)
                    mChallanNo = IIf(IsDbNull(RSPrintDummy.Fields("CHALLANNO").Value), "", RSPrintDummy.Fields("CHALLANNO").Value)
                    mBookType = IIf(IsDbNull(RSPrintDummy.Fields("BOOKTYPE").Value), "", RSPrintDummy.Fields("BOOKTYPE").Value)

                    If Val(CStr(mTotalAmount)) = 0 Then GoTo NextRec2
                    RowNum = RowNum + 100

                    SqlStr = " INSERT INTO TEMP_PRINTDUMMYDATA ( " & vbCrLf & " USERID, SUBROW, " & vbCrLf & " FIELD1, FIELD2, FIELD3, FIELD4," & vbCrLf & " FIELD5, FIELD6, FIELD7, FIELD8," & vbCrLf & " FIELD10,FIELD11,FIELD12,FIELD13,FIELD14, FIELD15) " & vbCrLf & " VALUES (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & RowNum & ", " & vbCrLf & " '" & mTDSAmount & "', '" & mSurchargeAmount & "', '" & mCESSAmount & "', " & vbCrLf & " '" & mTotalAmount & "', '" & mChequeNo & "', '" & mBSRCode & "', " & vbCrLf & " '" & mPAYMENTDATE & "', '" & mChallanNo & "', '" & pChallanType & "', " & vbCrLf & " '" & mAuthoName & "','" & mAuthoFName & "','" & mAuthoDesg & "','" & mTotPaidAmountStr & "','" & mBookType & "') "
                    PubDBCn.Execute(SqlStr)
NextRec2:
                    RSPrintDummy.MoveNext()
                    mIsInsert = True
                Loop

            End If
            mToEmpCompany = mFromEmpCompany
            mToEmpCode = mFromEmpCode
            GoTo SearchRow
        End If

        mTotPaidAmountStr = MainClass.RupeesConversion(CDbl(mTotPaidAmount))

        SqlStr = " SELECT  ID.TDS_AMOUNT, ID.SURCHARGE_AMT, ID.CESS_AMT, " & vbCrLf & " ID.AMOUNT, IH.CHQ_NO, IH.BSRCODE, IH.CHALLANDATE, " & vbCrLf & " IH.CHALLANNO,BOOKTYPE " & vbCrLf & " FROM PAY_ITChallan_HDR IH, PAY_ITChallan_DET ID WHERE " & vbCrLf & " IH.AUTO_KEY_REFNO=ID.AUTO_KEY_REFNO" & vbCrLf & " AND IH.COMPANY_CODE = " & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " IH.FYEAR = " & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " ID.EMP_CODE = '" & mCode & "' "

        SqlStr = SqlStr & vbCrLf & " AND IH.VDATE>=TO_DATE('" & VB6.Format(mFromDate, "DD-MMM-YYYY") & "','DD-MON-YYYY') AND IH.VDATE<=TO_DATE('" & VB6.Format(mToDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        SqlStr = SqlStr & vbCrLf & " ORDER BY IH.VDate "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RSPrintDummy, ADODB.LockTypeEnum.adLockReadOnly)

        If RSPrintDummy.EOF = False Then

            Do While Not RSPrintDummy.EOF
                mTDSAmount = IIf(IsDbNull(RSPrintDummy.Fields("TDS_AMOUNT").Value), 0, RSPrintDummy.Fields("TDS_AMOUNT").Value)
                mSurchargeAmount = IIf(IsDbNull(RSPrintDummy.Fields("SURCHARGE_AMT").Value), 0, RSPrintDummy.Fields("SURCHARGE_AMT").Value)
                mCESSAmount = IIf(IsDbNull(RSPrintDummy.Fields("CESS_AMT").Value), 0, RSPrintDummy.Fields("CESS_AMT").Value)
                mTotalAmount = IIf(IsDbNull(RSPrintDummy.Fields("Amount").Value), 0, RSPrintDummy.Fields("Amount").Value)

                mChequeNo = IIf(IsDbNull(RSPrintDummy.Fields("CHQ_NO").Value), "", RSPrintDummy.Fields("CHQ_NO").Value)
                mBSRCode = IIf(IsDbNull(RSPrintDummy.Fields("BSRCODE").Value), "", RSPrintDummy.Fields("BSRCODE").Value)
                mPAYMENTDATE = IIf(IsDbNull(RSPrintDummy.Fields("CHALLANDATE").Value), "", RSPrintDummy.Fields("CHALLANDATE").Value)
                mChallanNo = IIf(IsDbNull(RSPrintDummy.Fields("CHALLANNO").Value), "", RSPrintDummy.Fields("CHALLANNO").Value)
                mBookType = IIf(IsDbNull(RSPrintDummy.Fields("BOOKTYPE").Value), "", RSPrintDummy.Fields("BOOKTYPE").Value)

                If Val(CStr(mTotalAmount)) = 0 Then GoTo NextRec
                RowNum = RowNum + 100

                SqlStr = " INSERT INTO TEMP_PRINTDUMMYDATA ( " & vbCrLf & " USERID, SUBROW, " & vbCrLf & " FIELD1, FIELD2, FIELD3, FIELD4," & vbCrLf & " FIELD5, FIELD6, FIELD7, FIELD8," & vbCrLf & " FIELD10,FIELD11,FIELD12,FIELD13,FIELD14, FIELD15) " & vbCrLf & " VALUES (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "'," & RowNum & ", " & vbCrLf & " '" & mTDSAmount & "', '" & mSurchargeAmount & "', '" & mCESSAmount & "', " & vbCrLf & " '" & mTotalAmount & "', '" & mChequeNo & "', '" & mBSRCode & "', " & vbCrLf & " '" & mPAYMENTDATE & "', '" & mChallanNo & "', '" & pChallanType & "', " & vbCrLf & " '" & mAuthoName & "','" & mAuthoFName & "','" & mAuthoDesg & "','" & mTotPaidAmountStr & "','" & mBookType & "') "
                PubDBCn.Execute(SqlStr)
NextRec:
                RSPrintDummy.MoveNext()
                mIsInsert = True
            Loop

        End If

        If mIsInsert = False Then

            SqlStr = " INSERT INTO TEMP_PRINTDUMMYDATA ( " & vbCrLf & " USERID, SUBROW, " & vbCrLf & " FIELD1, FIELD2, FIELD3, FIELD4," & vbCrLf & " FIELD5, FIELD6, FIELD7, FIELD8," & vbCrLf & " FIELD10,FIELD11,FIELD12,FIELD13,FIELD14, FIELD15) " & vbCrLf & " VALUES (" & vbCrLf & " '" & MainClass.AllowSingleQuote(PubUserID) & "',1, " & vbCrLf & " '0.00', '0.00', '0.00', " & vbCrLf & " '0.00', '', '', " & vbCrLf & " '', '', '" & pChallanType & "', " & vbCrLf & " '" & mAuthoName & "','" & mAuthoFName & "','" & mAuthoDesg & "','Zero','R') "
            PubDBCn.Execute(SqlStr)
        End If
        PubDBCn.CommitTrans()
        FillITChallan = True
        Exit Function
PrintDummyErr:
        FillITChallan = False
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function


    Private Sub FillGridPart()
        With sprdIT
            .Row = 29
            .Col = ColAmt1
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            .Text = "Gross Amt"
            .Col = ColAmt2
            '        .TypeEditMultiLine = True
            '        .TypeHAlign = TypeHAlignCenter
            '        .Text = "Qualifying Amt"
            '        .Col = ColAmt3
            .TypeEditMultiLine = True
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            .Text = "Deductible Amt"


            .Row = 41
            .Col = ColAmt1
            .Text = "Gross Amt"
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            .Col = ColAmt2
            .Text = "Qualifying Amt"
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            '        .Col = ColAmt3
            '        .TypeHAlign = TypeHAlignCenter
            '        .Text = "Tax rebate/ relief"


        End With
    End Sub


    Private Function GetSurcharge() As Double

        On Error GoTo ShowErrPart
        Dim RsTemp As ADODB.Recordset = Nothing
        Dim mSearchRow As String


        mSearchRow = IIf(RsCompany.Fields("COMPANY_CODE").Value = 2 Or RsCompany.Fields("COMPANY_CODE").Value = 12, "(73,80)", "(66,73)")

        SqlStr = " SELECT SUM(TotalAmount) AS TotalAmount FROM PAY_ITCOMP_TRN WHERE " & vbCrLf & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND " & vbCrLf & " FYEAR=" & RsCompany.Fields("FYEAR").Value & " AND " & vbCrLf & " Emp_Code='" & txtEmpCode.Text & "' AND SUBROWNO In " & mSearchRow & ""
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockOptimistic)

        If RsTemp.EOF = False Then
            GetSurcharge = CDbl(VB6.Format(IIf(IsDbNull(RsTemp.Fields("TotalAmount").Value), 0, RsTemp.Fields("TotalAmount").Value), "0.00"))
        End If
        GetSurcharge = System.Math.Round(GetSurcharge, 0)
        Exit Function
ShowErrPart:
        MsgBox(Err.Description)
        GetSurcharge = 0
    End Function

    Private Sub txtTDS_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtTDS.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, txtTDS.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtTo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtTo.TextChanged

        MainClass.SaveStatus(Me.cmdSave, ADDMode, MODIFYMode)
    End Sub


    Private Sub txtTo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtTo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Not IsDate(txtTo.Text) Then
            MsgInformation("Invalid Date")
            Cancel = True
        End If
        eventArgs.Cancel = Cancel
    End Sub
End Class
