Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmParamMachList
    Inherits System.Windows.Forms.Form
    Dim XRIGHT As String
    Private Const RowHeight As Short = 20
    Dim xMyMenu As String
    Private Const ColMachName As Short = 1
    Private Const ColSpecification As Short = 2
    Private Const ColMachNo As Short = 3
    Private Const ColLocation As Short = 4
    Private Const ColMachKey As Short = 5

    Dim CurrFormWidth As Integer
    Dim CurrFormHeight As Integer
    Dim mActiveRow As Integer
    Dim FormActive As Boolean
    Private Sub PrintStatus(ByRef pPrintEnable As Object)
        CmdPreview.Enabled = pPrintEnable
        cmdPrint.Enabled = pPrintEnable
    End Sub

    Private Sub chkAll_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkAll.CheckStateChanged
        TxtMachNo.Enabled = IIf(chkAll.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
        cmdSearch.Enabled = IIf(chkAll.CheckState = System.Windows.Forms.CheckState.Checked, False, True)
    End Sub

    Private Sub CmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdClose.Click
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If UCase(lblRepType.Text) = "MACLIST" Then
            ReportOnMacList(Crystal.DestinationConstants.crptToWindow)
        ElseIf UCase(lblRepType.Text) = "MACHIST" Then
            ReportOnMacHist(Crystal.DestinationConstants.crptToWindow)
        ElseIf UCase(lblRepType.Text) = "FAULTLIST" Then
            ReportOnFaultList(Crystal.DestinationConstants.crptToWindow)
        ElseIf UCase(lblRepType.Text) = "IPT" Then
            ReportOnIPT(Crystal.DestinationConstants.crptToWindow)
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If UCase(lblRepType.Text) = "MACLIST" Then
            ReportOnMacList(Crystal.DestinationConstants.crptToPrinter)
        ElseIf UCase(lblRepType.Text) = "MACHIST" Then
            ReportOnMacHist(Crystal.DestinationConstants.crptToPrinter)
        ElseIf UCase(lblRepType.Text) = "FAULTLIST" Then
            ReportOnFaultList(Crystal.DestinationConstants.crptToPrinter)
        ElseIf UCase(lblRepType.Text) = "IPT" Then
            ReportOnIPT(Crystal.DestinationConstants.crptToPrinter)
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ReportOnMacList(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String

        Report1.Reset()

        mTitle = "List of Machines"
        If optKey(0).Checked = True Then 'YES
            mSubTitle = mSubTitle & " [KEY MACHINE]"
        ElseIf optKey(1).Checked = True Then  'NO
            mSubTitle = mSubTitle & " [NON KEY MACHINE]"
        ElseIf optKey(2).Checked = True Then  'BOTH
            mSubTitle = mSubTitle & " [ALL]"
        End If
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\MachList.RPT"
        SqlStr = MakeSqlForList
        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub

    Private Sub ReportOnIPT(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String

        Report1.Reset()

        mTitle = "List of Initial Part Tag"
        If optIPT(0).Checked = True Then 'BOTH
            mSubTitle = mSubTitle & " [BOTH]"
        ElseIf optIPT(1).Checked = True Then  'COMPLETED
            mSubTitle = mSubTitle & " [COMPLETED]"
        ElseIf optIPT(2).Checked = True Then  'PENDING
            mSubTitle = mSubTitle & " [PENDING]"
        End If
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\IPTList.RPT"
        SqlStr = MakeSqlForIPTList
        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub

    Private Sub ReportOnMacHist(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String

        Report1.Reset()

        mTitle = "Machine History Card"
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\MachHistCard.RPT"

        SqlStr = MakeSqlForHist
        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub
    Private Sub ReportOnFaultList(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ReportErr
        Dim SqlStr As String
        Dim mTitle As String
        Dim mSubTitle As String

        Report1.Reset()

        mTitle = "BreakDown Problems Master"
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\Reports\FaultList.RPT"

        SqlStr = MakeSqlForFault
        Call ShowReport(SqlStr, Mode, mTitle, mSubTitle)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Exit Sub
ReportErr:
        MsgBox(Err.Description)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        'Resume
    End Sub

    Private Function MakeSqlForHist() As String

        On Error GoTo ERR1

        MakeSqlForHist = " SELECT  'B',MAN_BREAKDOWN_HDR.AUTO_KEY_BDSLIP,MAN_BREAKDOWN_HDR.MACHINE_NO, " & vbCrLf & " MAN_BREAKDOWN_HDR.SLIP_DATE, " & vbCrLf & " MAN_BREAKDOWN_HDR.PROBLEM_FACED,MAN_BREAKDOWN_HDR.DEPU_EMP_CODE, " & vbCrLf & " MAN_BDPROBLEMS_MST.PROB_DESC, PAY_EMPLOYEE_MST.EMP_NAME, " & vbCrLf & " MAN_BREAKDOWN_DET.ITEM_CODE,MAN_BREAKDOWN_DET.ITEM_UOM,MAN_BREAKDOWN_DET.ITEM_QTY, " & vbCrLf & " DECODE(MAN_BREAKDOWN_DET.ITEM_CODE, NULL, NULL,MAN_BREAKDOWN_DET.ITEM_CODE || ' - '||INV_ITEM_MST.ITEM_SHORT_DESC) ITEM_DESC, " & vbCrLf & " MAN_MACHINE_MST.MAKE, " & vbCrLf & " MAN_MACHINE_MST.LOCATION , MAN_MACHINE_MST.MACHINE_DESC " & vbCrLf & " FROM MAN_BREAKDOWN_HDR , MAN_BREAKDOWN_DET , INV_ITEM_MST , " & vbCrLf & " MAN_MACHINE_MST , MAN_BDPROBLEMS_MST, PAY_EMPLOYEE_MST " & vbCrLf & " WHERE MAN_BREAKDOWN_HDR.AUTO_KEY_BDSLIP=MAN_BREAKDOWN_DET.AUTO_KEY_BDSLIP(+) " & vbCrLf & " AND MAN_BREAKDOWN_DET.ITEM_CODE=INV_ITEM_MST.ITEM_CODE (+)  " & vbCrLf & " AND INV_ITEM_MST.COMPANY_CODE (+)=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND MAN_BREAKDOWN_HDR.COMPANY_CODE=MAN_MACHINE_MST.COMPANY_CODE (+) " & vbCrLf & " AND MAN_BREAKDOWN_HDR.MACHINE_NO =MAN_MACHINE_MST.MACHINE_NO (+) " & vbCrLf & " AND MAN_BREAKDOWN_HDR.COMPANY_CODE=MAN_BDPROBLEMS_MST.COMPANY_CODE (+)  " & vbCrLf & " AND MAN_BREAKDOWN_HDR.PROBLEM_FACED=MAN_BDPROBLEMS_MST.PROB_CODE (+) " & vbCrLf & " AND MAN_BREAKDOWN_HDR.COMPANY_CODE=PAY_EMPLOYEE_MST.COMPANY_CODE(+)  " & vbCrLf & " AND MAN_BREAKDOWN_HDR.DEPU_EMP_CODE=PAY_EMPLOYEE_MST.EMP_CODE (+)  " & vbCrLf & " AND MAN_BREAKDOWN_HDR.COMPANY_CODE =" & RsCompany.Fields("COMPANY_CODE").Value & " "

        If chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked And Trim(TxtMachNo.Text) <> "" Then
            MakeSqlForHist = MakeSqlForHist & vbCrLf & " AND MAN_BREAKDOWN_HDR.MACHINE_NO ='" & MainClass.AllowSingleQuote(TxtMachNo.Text) & "'  "
        End If
        MakeSqlForHist = MakeSqlForHist & vbCrLf & " ORDER BY   MAN_BREAKDOWN_HDR.MACHINE_NO,MAN_BREAKDOWN_HDR.SLIP_DATE"
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function MakeSqlForFault() As String
        On Error GoTo ERR1
        MakeSqlForFault = " SELECT *  " & vbCrLf & " FROM MAN_BDPROBLEMS_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " ORDER BY PROB_CODE"
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Sub ShowReport(ByRef mSqlStr As String, ByRef mMode As Crystal.DestinationConstants, ByRef mTitle As String, ByRef mSubTitle As String)
        Report1.SQLQuery = mSqlStr
        SetCrpt(Report1, mMode, 1, mTitle, mSubTitle, True, xMyMenu)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
    End Sub

    Private Sub cmdSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdsearch.Click
        On Error GoTo ERR1
        Dim SqlStr As String

        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.SearchGridMaster(TxtMachNo.Text, "MAN_MACHINE_MST", "MACHINE_NO", "MACHINE_DESC", "MACHINE_ITEM_CODE", , SqlStr) = True Then
            TxtMachNo.Text = AcName
            lblMachNo.Text = AcName1
        End If
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamMachList_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If UCase(lblRepType.Text) = "MACLIST" Then
            Me.Text = "List of Machines"
            FraKey.Visible = True
            FraMac.Visible = False
            fraIPT.Visible = False
        ElseIf UCase(lblRepType.Text) = "MACHIST" Then
            Me.Text = "Machine History Card"
            FraKey.Visible = False
            FraMac.Visible = True
            fraIPT.Visible = False
        ElseIf UCase(lblRepType.Text) = "FAULTLIST" Then
            Me.Text = "Fault Master Listing"
            FraKey.Visible = False
            FraMac.Visible = False
            fraIPT.Visible = False
        ElseIf UCase(lblRepType.Text) = "IPT" Then
            Me.Text = "List Of Initial Part Tag"
            FraKey.Visible = False
            FraMac.Visible = False
            fraIPT.Visible = True
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        FormActive = True
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParamMachList_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmParamMachList_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo BSLError
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        xMyMenu = myMenu
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        CurrFormHeight = 7245
        CurrFormWidth = 11355

        Me.Top = 0
        Me.Left = 0
        Me.Height = VB6.TwipsToPixelsY(2010)
        Me.Width = VB6.TwipsToPixelsX(5550)


        Call PrintStatus(True)

        optKey(2).Checked = True
        optIPT(0).Checked = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
BSLError:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgInformation(Err.Description)
    End Sub
    Private Sub frmParamMachList_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormActive = False
        Me.Hide()
        Me.Close()
    End Sub

    Private Function MakeSqlForList() As String
        On Error GoTo ERR1

        MakeSqlForList = " SELECT MACHINE_DESC,MACHINE_SPEC,MACHINE_NO,LOCATION,KEY_MACHINE" & vbCrLf & " FROM  MAN_MACHINE_MST "

        If optKey(0).Checked = True Then 'YES
            MakeSqlForList = MakeSqlForList & vbCrLf & "WHERE KEY_MACHINE='Y'"
        ElseIf optKey(1).Checked = True Then  'NO
            MakeSqlForList = MakeSqlForList & vbCrLf & "WHERE KEY_MACHINE='N'"
        ElseIf optKey(2).Checked = True Then  'BOTH
            'BOTH
        End If

        MakeSqlForList = MakeSqlForList & vbCrLf & "ORDER BY MACHINE_NO"

        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function
    Private Function MakeSqlForIPTList() As String
        On Error GoTo ERR1

        MakeSqlForIPTList = "SELECT QAL_IPT_HDR.*,QAL_IPT_DET.*, " & vbCrLf & " FIN_SUPP_CUST_MST.*,PAY_EMPLOYEE_MST.*,PAY_DEPT_MST.*,EMP2.*, " & vbCrLf & " DEPT2.*,DEPT3.* " & vbCrLf & " FROM QAL_IPT_HDR,QAL_IPT_DET,  " & vbCrLf & " FIN_SUPP_CUST_MST,PAY_EMPLOYEE_MST ,PAY_DEPT_MST , " & vbCrLf & " PAY_EMPLOYEE_MST EMP2,PAY_DEPT_MST DEPT2,PAY_DEPT_MST DEPT3 " & vbCrLf & " WHERE QAL_IPT_HDR.AUTO_KEY_IPT=QAL_IPT_DET.AUTO_KEY_IPT " & vbCrLf & " AND QAL_IPT_HDR.COMPANY_CODE=FIN_SUPP_CUST_MST.COMPANY_CODE " & vbCrLf & " AND QAL_IPT_HDR.SUPP_CUST_CODE=FIN_SUPP_CUST_MST.SUPP_CUST_CODE " & vbCrLf & " AND QAL_IPT_HDR.COMPANY_CODE=PAY_EMPLOYEE_MST.COMPANY_CODE (+) " & vbCrLf & " AND QAL_IPT_HDR.INITBY=PAY_EMPLOYEE_MST.EMP_CODE (+) " & vbCrLf & " AND QAL_IPT_HDR.COMPANY_CODE=PAY_DEPT_MST.COMPANY_CODE (+) " & vbCrLf & " AND QAL_IPT_HDR.CLOSEATDEPT=PAY_DEPT_MST.DEPT_CODE (+) " & vbCrLf & " AND QAL_IPT_HDR.COMPANY_CODE=EMP2.COMPANY_CODE (+) " & vbCrLf & " AND QAL_IPT_HDR.CLOSEBY=EMP2.EMP_CODE (+) " & vbCrLf & " AND DEPT2.COMPANY_CODE(+)=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND QAL_IPT_DET.DEPTCODE=DEPT2.DEPT_CODE (+) " & vbCrLf & " AND DEPT3.COMPANY_CODE(+)=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND QAL_IPT_DET.MOVETODEPT=DEPT3.DEPT_CODE (+) " & vbCrLf & " AND QAL_IPT_HDR.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND QAL_IPT_HDR.FYEAR=" & RsCompany.Fields("FYEAR").Value & " "

        If optIPT(0).Checked = True Then 'BOTH
            'BOTH
        ElseIf optIPT(1).Checked = True Then  'COMPLETED
            MakeSqlForIPTList = MakeSqlForIPTList & vbCrLf & "AND QAL_IPT_HDR.CLOSEATDEPT IS NOT NULL "
        ElseIf optIPT(2).Checked = True Then  'PENDING
            MakeSqlForIPTList = MakeSqlForIPTList & vbCrLf & "AND QAL_IPT_HDR.CLOSEATDEPT IS NULL " & vbCrLf & "AND QAL_IPT_DET.ACTIONTAKEN='N'"
        End If
        MakeSqlForIPTList = MakeSqlForIPTList & vbCrLf & "ORDER BY QAL_IPT_HDR.AUTO_KEY_IPT,QAL_IPT_HDR.IPLDATE,QAL_IPT_DET.SERIAL_NO      "
        Exit Function
ERR1:
        MsgInformation(Err.Description)
    End Function

    Private Sub TxtMachNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtMachNo.DoubleClick
        Call cmdSearch_Click(cmdSearch, New System.EventArgs())
    End Sub

    Private Sub TxtMachNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles TxtMachNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)

        KeyAscii = MainClass.UpperCase(KeyAscii, TxtMachNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub TxtMachNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles TxtMachNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearch_Click(cmdSearch, New System.EventArgs())
    End Sub

    Private Sub TxtMachNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles TxtMachNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1
        Dim SqlStr As String
        If Trim(TxtMachNo.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & ""
        If MainClass.ValidateWithMasterTable(TxtMachNo.Text, "MACHINE_NO", "MACHINE_DESC", "MAN_MACHINE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            lblMachNo.text = ""
            MsgInformation("No Such Machine in Machine Master")
            Cancel = True
        Else
            lblMachNo.text = MasterNo
        End If
        GoTo EventExitSub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
