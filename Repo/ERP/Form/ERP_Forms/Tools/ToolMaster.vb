Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmToolMaster
    Inherits System.Windows.Forms.Form
    Dim RsToolNo As ADODB.Recordset
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    'Dim PvtDBCn As ADODB.Connection				
    Dim Shw As Boolean
    Dim FormActive As Boolean
    Dim SqlStr As String
    Dim pToolNumber As String

    Private Sub ViewGrid()
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        If CmdView.Text = ConCmdGridViewCaption Then
            CmdView.Text = ConCmdViewCaption
            AssignGrid(True)
            '        ADataGrid.Refresh				
            SprdView.Refresh()
            SprdView.BringToFront()
        Else
            CmdView.Text = ConCmdGridViewCaption
            SprdView.SendToBack()
        End If
        MainClass.ButtonStatus(Me, XRIGHT, RsToolNo, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Clear1()
        txtToolNo.Text = ""
        txtToolItemCode.Text = ""
        txtToolItemName.Text = ""
        txtDeptCode.Text = ""
        txtDeptDesc.Text = ""
        txtOprCode.Text = ""
        txtOprDesc.Text = ""
        txtItemCode.Text = ""
        txtItemName.Text = ""
        txtToolManuDate.Text = "__/__/____"
        txtProducedQty.Text = ""
        txtRemarks.Text = ""
        txtDrgNo.Text = ""
        txtLocation.Text = ""
        txtMasterToolNo.Text = ""
        cboToolFreq.SelectedIndex = 0
        chkToolUB.CheckState = System.Windows.Forms.CheckState.Unchecked
        txtToolLoadDate.Text = "__/__/____"
        txtToolLoadTime.Text = "__:__"
        txtToolPreventiveQty.Text = ""
        txtToolPrdQty.Text = ""
        txtToolLife.Text = ""
        cboToolStatus.SelectedIndex = 0


        txtOPProduction.Text = ""
        txtOpAsOnDate.Text = "__/__/____"
        cboUnit.SelectedIndex = -1
        txtPcsNoStroke.Text = ""

        MainClass.ButtonStatus(Me, XRIGHT, RsToolNo, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
    End Sub

    Private Sub cboUnit_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboUnit.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboUnit_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboUnit.SelectedIndexChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click
        On Error GoTo ModifyErr
        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            txtToolNo.Enabled = False
            cmdSearchToolNo.Enabled = False
            MainClass.ButtonStatus(Me, XRIGHT, RsToolNo, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Else
            ADDMode = False
            MODIFYMode = False
            Show1()
        End If
        Exit Sub
ModifyErr:
        MsgBox(Err.Description)
    End Sub

    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPreview.Click
        Call ShowReport(Crystal.DestinationConstants.crptToWindow)
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ShowReport(Crystal.DestinationConstants.crptToPrinter)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdSearchDept_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchDept.Click
        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  "
        If MainClass.SearchGridMaster("", "PAY_DEPT_MST", "DEPT_DESC", "DEPT_CODE", , , SqlStr) = True Then
            txtDeptCode.Text = AcName1
            txtDeptDesc.Text = AcName
            txtDeptCode.Focus()
        End If
    End Sub

    Private Sub cmdSearchItem_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchItem.Click
        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  "
        If MainClass.SearchGridMaster("", "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr) = True Then
            txtItemCode.Text = AcName1
            txtItemName.Text = AcName
            txtItemCode.Focus()
        End If
    End Sub

    Private Sub cmdSearchMasterToolNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchMasterToolNo.Click
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  "
        If MainClass.SearchGridMaster(txtMasterToolNo.Text, "TOL_TOOLINFO_MST", "TOOL_NO", , , , SqlStr) = True Then
            txtMasterToolNo.Text = AcName
            txtMasterToolNo.Focus()
        End If
    End Sub

    Private Sub cmdSearchOPR_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchOPR.Click
        If Trim(txtDeptCode.Text) = "" Then
            MsgInformation("Please Select Dept.")
            txtDeptCode.Focus()
            Exit Sub
        End If

        If Trim(txtItemCode.Text) = "" Then
            MsgInformation("Please Select Product Code.")
            txtItemCode.Focus()
            Exit Sub
        End If

        'SqlStr = OperationQuery(Trim(txtItemCode.Text), Trim(txtDeptCode.Text), "", "", "", "TRN.OPR_CODE", "MST.OPR_DESC", "TO_CHAR(OPR_SNO,'000')")
        'If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
        '    txtOprCode.Text = AcName
        '    txtOprDesc.Text = AcName1
        '    txtOprCode.Focus()
        'End If

        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtDeptCode.Text) & "' "
        If MainClass.SearchGridMaster("", "PRD_OPR_MST", "OPR_DESC", "OPR_CODE", , , SqlStr) = True Then
            txtOprCode.Text = AcName1
            txtOprDesc.Text = AcName
            txtOprCode.Focus()
        End If


    End Sub

    Private Sub cmdSearchToolItem_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchToolItem.Click
        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  " & vbCrLf & " AND ITEM_CLASSIFICATION='T' "
        If MainClass.SearchGridMaster("", "INV_ITEM_MST", "ITEM_SHORT_DESC", "ITEM_CODE", , , SqlStr) = True Then
            txtToolItemCode.Text = AcName1
            txtToolItemName.Text = AcName
            txtToolItemCode.Focus()
        End If
    End Sub

    Private Sub cmdSearchToolNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchToolNo.Click
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  "
        If MainClass.SearchGridMaster(txtToolNo.Text, "TOL_TOOLINFO_MST", "TOOL_NO", , , , SqlStr) = True Then
            txtToolNo.Text = AcName
            txtToolNo_Validating(txtToolNo, New System.ComponentModel.CancelEventArgs(False))
        End If
    End Sub

    Private Sub CmdView_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdView.Click
        Call ViewGrid()
    End Sub

    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
        Else
            ADDMode = False
            MODIFYMode = False
            If RsToolNo.EOF = False Then RsToolNo.MoveFirst()
            Show1()
        End If
        Exit Sub
AddErr:
        MsgBox(Err.Description)
        '    Resume				
    End Sub

    Private Sub cmdClose_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClose.Click
        Me.Hide()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub CmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdDelete.Click
        On Error GoTo DelErrPart
        If txtToolNo.Text = "" Then MsgExclamation("Nothing to delete") : Exit Sub
        If RsToolNo.EOF Then Exit Sub
        If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then
            If Delete1() = False Then GoTo DelErrPart
            If RsToolNo.EOF = True Then
                Clear1()
            Else
                Show1()
            End If
        End If
        Exit Sub
DelErrPart:
        MsgBox("Record Not Deleted")
    End Sub

    Private Function Delete1() As Boolean
        On Error GoTo DeleteErr
        SqlStr = ""
        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()

        If InsertIntoDelAudit(PubDBCn, "TOL_TOOLINFO_MST", (txtToolNo.Text), RsToolNo, "", "D") = False Then GoTo DeleteErr

        If InsertIntoDeleteTrn(PubDBCn, "TOL_TOOLINFO_MST", "TOOL_NO", (txtToolNo.Text)) = False Then GoTo DeleteErr

        SqlStr = " DELETE FROM TOL_TOOLINFO_MST " & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
            & " AND TOOL_NO='" & MainClass.AllowSingleQuote(txtToolNo.Text) & "'"

        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        RsToolNo.Requery()
        Delete1 = True
        Exit Function
DeleteErr:
        Delete1 = False
        PubDBCn.RollbackTrans()
        RsToolNo.Requery()
        If Err.Number = -2147467259 Then
            MsgBox("Can't Delete Transaction Exists Against This NO.")
            Exit Function
        End If
        MsgBox(Err.Description)
    End Function

    Private Sub frmToolMaster_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = eventArgs.row
        txtToolNo.Text = SprdView.Text
        txtToolNo_Validating(txtToolNo, New System.ComponentModel.CancelEventArgs(False))
        CmdView_Click(CmdView, New System.EventArgs())
    End Sub

    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Private Sub frmToolMaster_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        On Error GoTo ERR1
        If FormActive = True Then Exit Sub

        SqlStr = " Select * From TOL_TOOLINFO_MST Where 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsToolNo, ADODB.LockTypeEnum.adLockReadOnly)
        Call SetTextLengths()
        AssignGrid(False)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If CmdAdd.Enabled = True Then cmdAdd_Click(CmdAdd, New System.EventArgs())
        Clear1()
        FormActive = True
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ERR1:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '   Resume				
    End Sub

    Private Sub frmToolMaster_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection				
        'PvtDBCn.Open StrConn				
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)

        ADDMode = False
        MODIFYMode = False
        Me.Left = 0
        Me.Top = 0
        Me.Height = VB6.TwipsToPixelsY(7065)
        Me.Width = VB6.TwipsToPixelsX(8970)
        Call FillComboMst()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Sub
ErrPart:
        MsgBox(Err.Description)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub frmToolMaster_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        On Error Resume Next
        FormActive = False
        RsToolNo.Close()
        RsToolNo = Nothing
        'PvtDBCn.Close				
        'Set PvtDBCn = Nothing				
    End Sub

    Private Sub Show1()
        On Error GoTo ShowErrPart

        Shw = True
        If Not RsToolNo.EOF Then
            txtToolNo.Text = IIf(IsDBNull(RsToolNo.Fields("TOOL_NO").Value), "", RsToolNo.Fields("TOOL_NO").Value)
            txtToolItemCode.Text = Trim(IIf(IsDBNull(RsToolNo.Fields("TOOL_ITEM_CODE").Value), "", RsToolNo.Fields("TOOL_ITEM_CODE").Value))
            txtToolItemCode_Validating(txtToolItemCode, New System.ComponentModel.CancelEventArgs((False)))
            txtDeptCode.Text = Trim(IIf(IsDBNull(RsToolNo.Fields("DEPT_CODE").Value), "", RsToolNo.Fields("DEPT_CODE").Value))
            txtDeptCode_Validating(txtDeptCode, New System.ComponentModel.CancelEventArgs((False)))

            txtItemCode.Text = Trim(IIf(IsDBNull(RsToolNo.Fields("ITEM_CODE").Value), "", RsToolNo.Fields("ITEM_CODE").Value))
            txtItemCode_Validating(txtItemCode, New System.ComponentModel.CancelEventArgs((False)))

            txtOprCode.Text = Trim(IIf(IsDBNull(RsToolNo.Fields("OPR_CODE").Value), "", RsToolNo.Fields("OPR_CODE").Value))
            txtOPRCode_Validating(txtOprCode, New System.ComponentModel.CancelEventArgs((False)))


            txtToolManuDate.Text = VB6.Format(IIf(IsDBNull(RsToolNo.Fields("TOOL_MANU_DATE").Value), "__/__/____", RsToolNo.Fields("TOOL_MANU_DATE").Value), "DD/MM/YYYY")
            txtProducedQty.Text = VB6.Format(CalcProdQty(), "0.000")
            txtRemarks.Text = IIf(IsDBNull(RsToolNo.Fields("REMARKS").Value), "", RsToolNo.Fields("REMARKS").Value)
            txtDrgNo.Text = IIf(IsDBNull(RsToolNo.Fields("DRG_NO").Value), "", RsToolNo.Fields("DRG_NO").Value)
            txtLocation.Text = IIf(IsDBNull(RsToolNo.Fields("LOCATION").Value), "", RsToolNo.Fields("LOCATION").Value)
            txtMasterToolNo.Text = IIf(IsDBNull(RsToolNo.Fields("MASTER_TOOL_NO").Value), "", RsToolNo.Fields("MASTER_TOOL_NO").Value)
            cboToolFreq.Text = IIf(IsDBNull(RsToolNo.Fields("TOOL_FREQ").Value), "", RsToolNo.Fields("TOOL_FREQ").Value)
            chkToolUB.CheckState = IIf(RsToolNo.Fields("TOOL_UB").Value = "Y", System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked)
            txtToolLoadDate.Text = VB6.Format(IIf(IsDBNull(RsToolNo.Fields("TOOL_LOAD_DATE").Value), "__/__/____", RsToolNo.Fields("TOOL_LOAD_DATE").Value), "DD/MM/YYYY")
            txtToolLoadTime.Text = VB6.Format(IIf(IsDBNull(RsToolNo.Fields("TOOL_LOAD_TIME").Value), "__:__", RsToolNo.Fields("TOOL_LOAD_TIME").Value), "HH:MM")
            txtToolPreventiveQty.Text = IIf(IsDBNull(RsToolNo.Fields("TOOL_PREVENTIVE_QTY").Value), 0, RsToolNo.Fields("TOOL_PREVENTIVE_QTY").Value)
            txtToolPrdQty.Text = IIf(IsDBNull(RsToolNo.Fields("TOOL_PRD_QTY").Value), 0, RsToolNo.Fields("TOOL_PRD_QTY").Value)
            txtToolLife.Text = IIf(IsDBNull(RsToolNo.Fields("TOOL_LIFE").Value), "", RsToolNo.Fields("TOOL_LIFE").Value)

            If RsToolNo.Fields("TOOL_STATUS").Value = "O" Then
                cboToolStatus.SelectedIndex = 0
            Else
                cboToolStatus.SelectedIndex = 1
            End If

            txtOPProduction.Text = VB6.Format(IIf(IsDBNull(RsToolNo.Fields("OP_QTY").Value), 0, RsToolNo.Fields("OP_QTY").Value))
            txtOpAsOnDate.Text = VB6.Format(IIf(IsDBNull(RsToolNo.Fields("OP_DATE").Value), "__/__/____", RsToolNo.Fields("OP_DATE").Value), "DD/MM/YYYY")
            cboUnit.SelectedIndex = Val(IIf(IsDBNull(RsToolNo.Fields("LIFE_UNIT").Value), 0, RsToolNo.Fields("LIFE_UNIT").Value)) - 1
            txtPcsNoStroke.Text = VB6.Format(IIf(IsDBNull(RsToolNo.Fields("NOS_PER_STROKE").Value), 0, RsToolNo.Fields("NOS_PER_STROKE").Value))

        End If

        Shw = False
        txtToolNo.Enabled = True
        cmdSearchToolNo.Enabled = True
        ADDMode = False
        MODIFYMode = False
        MainClass.ButtonStatus(Me, XRIGHT, RsToolNo, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, cmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        'Resume				
        MsgBox(Err.Description)
    End Sub

    Private Function CalcProdQty() As Double
        On Error GoTo ERR1
        Dim RsQty As ADODB.Recordset

        SqlStr = ""
        SqlStr = " SELECT SUM(ID.PROD_QTY) AS TOTAL_QTY " & vbCrLf _
            & " FROM PRD_PMEMODEPT_HDR IH, PRD_PMEMODEPT_DET ID" & vbCrLf _
            & " WHERE IH.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND IH.AUTO_KEY_REF = ID.AUTO_KEY_REF " & vbCrLf _
            & " AND ID.TOOL_NO='" & MainClass.AllowSingleQuote(txtToolNo.Text) & "'"

        SqlStr = SqlStr & vbCrLf & " AND IH.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDeptCode.Text) & "'"

        SqlStr = SqlStr & vbCrLf & " AND ID.OPR_CODE='" & MainClass.AllowSingleQuote(txtOprCode.Text) & "'"

        SqlStr = SqlStr & vbCrLf & " AND ID.ITEM_CODE='" & MainClass.AllowSingleQuote(txtItemCode.Text) & "'"

        SqlStr = SqlStr & vbCrLf & " AND IH.PROD_DATE >= TO_DATE('" & VB6.Format(txtToolManuDate.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')"
        SqlStr = SqlStr & vbCrLf & " AND IH.PROD_DATE <= TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')"

        ''PRODUCTION DATE '12-14-2007				

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsQty, ADODB.LockTypeEnum.adLockReadOnly)

        If RsQty.EOF = False Then
            If Not IsDBNull(RsQty.Fields("TOTAL_QTY").Value) Then
                CalcProdQty = RsQty.Fields("TOTAL_QTY").Value
            End If
        Else
            CalcProdQty = 0
        End If
        Exit Function
ERR1:
        CalcProdQty = 0
        MsgBox(Err.Description)
    End Function

    Private Sub FillComboMst()
        cboToolStatus.Items.Clear()
        cboToolStatus.Items.Add("Open")
        cboToolStatus.Items.Add("Closed")
        cboToolStatus.SelectedIndex = 0

        cboToolFreq.Items.Clear()
        cboToolFreq.Items.Add("Monthly")
        cboToolFreq.Items.Add("Quarterly")
        cboToolFreq.Items.Add("Half-Yearly")
        cboToolFreq.Items.Add("Yearly")
        cboToolFreq.SelectedIndex = 0

        cboUnit.Items.Clear()
        cboUnit.Items.Add("1.Stroke")
        cboUnit.Items.Add("2.Thickness")
        cboUnit.SelectedIndex = -1

        Exit Sub
    End Sub

    Private Sub CmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSave.Click
        On Error GoTo ErrorHandler
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If FieldsVarification() = False Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If
        If Update1() = True Then
            txtToolNo_Validating(txtToolNo, New System.ComponentModel.CancelEventArgs(False))
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

    Private Function AutoGenToolNo() As Integer
        On Error GoTo AutogenErr
        Dim RsAutoGen As ADODB.Recordset
        Dim mAutoGen As Integer
        Dim SqlStr As String

        mAutoGen = 1
        SqlStr = ""
        SqlStr = "SELECT Max(AUTO_KEY_NO)  " & vbCrLf & " FROM TOL_TOOLINFO_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsAutoGen, ADODB.LockTypeEnum.adLockReadOnly)
        With RsAutoGen
            If .EOF = False Then
                If Not IsDBNull(.Fields(0).Value) Then
                    mAutoGen = .Fields(0).Value + 1
                Else
                    mAutoGen = 1
                End If
            End If
        End With
        AutoGenToolNo = mAutoGen
        RsAutoGen.Close()
        RsAutoGen = Nothing
        Exit Function
AutogenErr:
        MsgBox(Err.Description)
    End Function

    Private Function Update1() As Boolean
        On Error GoTo UpdateError
        Dim mAutoKey As Integer
        Dim mManuDate As String
        Dim mLoadDate As String
        Dim mLoadTime As String
        Dim mStatus As String
        Dim mOPProdAsOnDate As String

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()
        SqlStr = ""

        mAutoKey = AutoGenToolNo()

        mManuDate = Trim(txtToolManuDate.Text)
        mLoadDate = Trim(txtToolLoadDate.Text)
        mLoadTime = Trim(txtToolLoadTime.Text)
        mOPProdAsOnDate = Trim(txtOpAsOnDate.Text)

        If mManuDate = "__/__/____" Or mManuDate = "/  /" Then mManuDate = ""
        If mLoadDate = "__/__/____" Or mLoadDate = "/  /" Then mLoadDate = ""
        If mOPProdAsOnDate = "__/__/____" Or mOPProdAsOnDate = "/  /" Then mOPProdAsOnDate = ""
        If mLoadTime = "__:__" Or mLoadTime = ":" Then mLoadTime = ""

        mStatus = VB.Left(cboToolStatus.Text, 1)

        If ADDMode = True Then
            SqlStr = " INSERT INTO TOL_TOOLINFO_MST ( " & vbCrLf _
                & " COMPANY_CODE, AUTO_KEY_NO, SUPP_CUST_CODE, MODEL_CODE, " & vbCrLf _
                & " TOOL_NO, TOOL_ITEM_CODE, DEPT_CODE, OPR_CODE, ITEM_CODE, " & vbCrLf _
                & " TOOL_MANU_DATE, REMARKS, DRG_NO, LOCATION, " & vbCrLf _
                & " MASTER_TOOL_NO, TOOL_FREQ, TOOL_UB, " & vbCrLf _
                & " TOOL_LOAD_DATE, TOOL_LOAD_TIME, TOOL_PREVENTIVE_QTY, TOOL_PRD_QTY, " & vbCrLf _
                & " TOOL_LIFE, TOOL_STATUS, " & vbCrLf _
                & " OP_QTY, OP_DATE, LIFE_UNIT, NOS_PER_STROKE, " & vbCrLf _
                & " ADDUSER, ADDDATE, MODUSER, MODDATE) " & vbCrLf _
                & " VALUES ( " & vbCrLf _
                & " " & RsCompany.Fields("COMPANY_CODE").Value & ", " & Val(mAutoKey) & ", '', '', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtToolNo.Text) & "', '" & MainClass.AllowSingleQuote(txtToolItemCode.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtDeptCode.Text) & "', '" & MainClass.AllowSingleQuote(txtOprCode.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtItemCode.Text) & "', " & vbCrLf _
                & " TO_DATE('" & VB6.Format(mManuDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtDrgNo.Text) & "', '" & MainClass.AllowSingleQuote(txtLocation.Text) & "', " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtMasterToolNo.Text) & "', '" & MainClass.AllowSingleQuote(cboToolFreq.Text) & "', " & vbCrLf _
                & " '" & IIf(chkToolUB.Checked = True, "Y", "N") & "', " & vbCrLf _
                & " TO_DATE('" & VB6.Format(mLoadDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), TO_DATE('" & mLoadTime & "', 'HH24:MI'), " & vbCrLf _
                & " " & Val(txtToolPreventiveQty.Text) & ", " & Val(txtToolPrdQty.Text) & ", " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(txtToolLife.Text) & "', '" & mStatus & "', " & vbCrLf _
                & " " & Val(txtOPProduction.Text) & ", TO_DATE('" & VB6.Format(mOPProdAsOnDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), '" & Mid(cboUnit.Text, 1, 1) & "', " & Val(txtPcsNoStroke.Text) & ", " & vbCrLf _
                & " '" & MainClass.AllowSingleQuote(PubUserID) & "', TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " '','') "
        Else
            SqlStr = " UPDATE TOL_TOOLINFO_MST SET " & vbCrLf _
                & " TOOL_NO='" & MainClass.AllowSingleQuote(txtToolNo.Text) & "', " & vbCrLf _
                & " TOOL_ITEM_CODE='" & MainClass.AllowSingleQuote(txtToolItemCode.Text) & "', " & vbCrLf _
                & " DEPT_CODE='" & MainClass.AllowSingleQuote(txtDeptCode.Text) & "', " & vbCrLf _
                & " OPR_CODE='" & MainClass.AllowSingleQuote(txtOprCode.Text) & "', " & vbCrLf _
                & " ITEM_CODE='" & MainClass.AllowSingleQuote(txtItemCode.Text) & "', " & vbCrLf _
                & " TOOL_MANU_DATE=TO_DATE('" & VB6.Format(mManuDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " REMARKS='" & MainClass.AllowSingleQuote(txtRemarks.Text) & "', " & vbCrLf _
                & " DRG_NO='" & MainClass.AllowSingleQuote(txtDrgNo.Text) & "', " & vbCrLf _
                & " LOCATION='" & MainClass.AllowSingleQuote(txtLocation.Text) & "', " & vbCrLf _
                & " MASTER_TOOL_NO='" & MainClass.AllowSingleQuote(txtMasterToolNo.Text) & "', " & vbCrLf _
                & " TOOL_FREQ='" & MainClass.AllowSingleQuote(cboToolFreq.Text) & "', " & vbCrLf _
                & " TOOL_UB='" & IIf(chkToolUB.CheckState = System.Windows.Forms.CheckState.Checked, "Y", "N") & "', " & vbCrLf _
                & " TOOL_LOAD_DATE=TO_DATE('" & VB6.Format(mLoadDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " TOOL_LOAD_TIME=TO_DATE('" & mLoadTime & "', 'HH24:MI'), " & vbCrLf _
                & " TOOL_PREVENTIVE_QTY=" & Val(txtToolPreventiveQty.Text) & ", " & vbCrLf _
                & " TOOL_PRD_QTY=" & Val(txtToolPrdQty.Text) & ", " & vbCrLf _
                & " TOOL_LIFE='" & MainClass.AllowSingleQuote(txtToolLife.Text) & "', " & vbCrLf _
                & " TOOL_STATUS='" & mStatus & "', "

            SqlStr = SqlStr & vbCrLf _
                & " OP_QTY=" & Val(txtOPProduction.Text) & ", " & vbCrLf _
                & " OP_DATE=TO_DATE('" & VB6.Format(mOPProdAsOnDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                & " LIFE_UNIT='" & VB.Left(cboUnit.Text, 1) & "', " & vbCrLf _
                & " NOS_PER_STROKE=" & Val(txtPcsNoStroke.Text) & ", " & vbCrLf _
                & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                & " MODDATE=TO_DATE('" & VB6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf _
                & " AND TOOL_NO='" & MainClass.AllowSingleQuote(txtToolNo.Text) & "' "

        End If

        PubDBCn.Execute(SqlStr)
        PubDBCn.CommitTrans()
        Update1 = True
        Exit Function
UpdateError:
        Update1 = False
        MsgBox(Err.Description & " Error No.: " & Str(Err.Number))
        '    Resume				
        PubDBCn.RollbackTrans()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Private Function FieldsVarification() As Boolean
        On Error GoTo VarificationErr
        FieldsVarification = True

        If Trim(txtToolNo.Text) = "" Then
            MsgBox("Tool No Cann't Be Blank.", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtToolNo.Enabled = True Then txtToolNo.Focus()
            Exit Function
        End If
        If Trim(txtToolItemCode.Text) = "" Then
            MsgBox("Tool Item Cann't Be Blank.", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtToolItemCode.Enabled = True Then txtToolItemCode.Focus()
            Exit Function
        End If
        If Trim(txtDeptCode.Text) = "" Then
            MsgBox("Department Cann't Be Blank.", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtDeptCode.Enabled = True Then txtDeptCode.Focus()
            Exit Function
        End If
        If Trim(txtOprCode.Text) = "" Then
            MsgBox("Operation Cann't Be Blank.", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtOprCode.Enabled = True Then txtOprCode.Focus()
            Exit Function
        End If
        If Trim(txtItemCode.Text) = "" Then
            MsgBox("Production Item Cann't Be Blank.", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtItemCode.Enabled = True Then txtItemCode.Focus()
            Exit Function
        End If
        If Trim(txtToolManuDate.Text) = "" Or Trim(txtToolManuDate.Text) = "__/__/____" Then
            MsgBox("Manufacturing Date is Blank", MsgBoxStyle.Information)
            FieldsVarification = False
            If txtToolManuDate.Enabled = True Then txtToolManuDate.Focus()
            Exit Function
        End If

        If ADDMode = False And MODIFYMode = False Then
            MsgInformation("Click Add Mode Or Modify to add a new Material Details or modify an existing Material")
            FieldsVarification = False
        End If
        If MODIFYMode = True And RsToolNo.EOF = True Then Exit Function
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Exit Function
VarificationErr:
        FieldsVarification = False
        MsgInformation(Err.Description)
    End Function

    Private Sub AssignGrid(ByRef mRefresh As Boolean)
        SqlStr = " SELECT TOOL_NO, TOOL_ITEM_CODE, DEPT_CODE, OPR_CODE, ITEM_CODE, " & vbCrLf & " TOOL_MANU_DATE, REMARKS, DRG_NO, LOCATION, " & vbCrLf & " MASTER_TOOL_NO, TOOL_FREQ, TOOL_UB, " & vbCrLf & " TOOL_LOAD_DATE, TOOL_LOAD_TIME, TOOL_PREVENTIVE_QTY, TOOL_PRD_QTY, " & vbCrLf & " TOOL_LIFE, TOOL_STATUS " & vbCrLf & " FROM TOL_TOOLINFO_MST " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " ORDER BY TOOL_NO "

        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        Call FormatSprdView()
    End Sub

    Private Sub ShowReport(ByRef Mode As Crystal.DestinationConstants)
        On Error GoTo ERR1
        Dim mTitle As String
        mTitle = ""
        Report1.Reset()

        mTitle = "TOOL MASTER"
        Report1.ReportFileName = My.Application.Info.DirectoryPath & "\reports\ToolMst.rpt"

        SetCrpt(Report1, Mode, 1, mTitle)
        Report1.WindowShowGroupTree = False
        Report1.Action = 1
        Exit Sub
ERR1:
        MsgInformation(Err.Description)
    End Sub

    Private Sub SetTextLengths()
        On Error GoTo ERR1
        txtToolNo.MaxLength = RsToolNo.Fields("TOOL_NO").DefinedSize
        txtToolItemCode.MaxLength = RsToolNo.Fields("TOOL_ITEM_CODE").DefinedSize
        txtToolItemName.MaxLength = 255
        txtDeptCode.MaxLength = RsToolNo.Fields("DEPT_CODE").DefinedSize
        txtDeptDesc.MaxLength = 255
        txtOprCode.MaxLength = RsToolNo.Fields("OPR_CODE").DefinedSize
        txtOprDesc.MaxLength = 255
        txtItemCode.MaxLength = RsToolNo.Fields("ITEM_CODE").DefinedSize
        txtItemName.MaxLength = 255
        txtToolManuDate.MaxLength = RsToolNo.Fields("TOOL_MANU_DATE").DefinedSize
        txtRemarks.MaxLength = RsToolNo.Fields("REMARKS").DefinedSize
        txtDrgNo.MaxLength = RsToolNo.Fields("DRG_NO").DefinedSize
        txtLocation.MaxLength = RsToolNo.Fields("LOCATION").DefinedSize
        txtMasterToolNo.MaxLength = RsToolNo.Fields("MASTER_TOOL_NO").DefinedSize
        txtToolLoadDate.MaxLength = RsToolNo.Fields("TOOL_LOAD_DATE").DefinedSize
        txtToolLoadTime.MaxLength = RsToolNo.Fields("TOOL_LOAD_TIME").DefinedSize
        txtToolPreventiveQty.MaxLength = RsToolNo.Fields("TOOL_PREVENTIVE_QTY").DefinedSize
        txtToolPrdQty.MaxLength = RsToolNo.Fields("TOOL_PRD_QTY").DefinedSize
        txtToolLife.MaxLength = RsToolNo.Fields("TOOL_LIFE").DefinedSize

        txtOPProduction.MaxLength = RsToolNo.Fields("OP_QTY").Precision
        txtPcsNoStroke.MaxLength = RsToolNo.Fields("NOS_PER_STROKE").Precision

        Exit Sub
ERR1:
        'Resume				
        MsgInformation(Err.Description)
    End Sub

    Private Sub FormatSprdView()
        With SprdView
            .Row = -1
            .set_ColWidth(0, 0)
            .set_ColWidth(1, 500 * 3)
            .set_ColWidth(2, 500 * 3)
            .set_ColWidth(3, 500 * 4)
            .set_ColWidth(4, 500 * 3)
            .set_ColWidth(5, 500 * 3)
            .set_ColWidth(6, 500 * 3)
            .set_ColWidth(7, 500 * 3)
            .set_ColWidth(8, 500 * 3)
            .set_ColWidth(9, 500 * 3)
            .set_ColWidth(10, 500 * 3)
            .set_ColWidth(11, 500 * 3)
            .set_ColWidth(12, 500 * 3)
            .set_ColWidth(13, 500 * 3)
            .set_ColWidth(14, 500 * 3)

            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub

    Private Sub txtOpAsOnDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOpAsOnDate.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOpAsOnDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtOpAsOnDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtOpAsOnDate.Text) = "__/__/____" Or Trim(txtOpAsOnDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtOpAsOnDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtOPProduction_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOPProduction.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOPProduction_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOPProduction.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtPcsNoStroke_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtPcsNoStroke.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtPcsNoStroke_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtPcsNoStroke.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtToolLoadDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtToolLoadDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtToolLoadDate.Text) = "__/__/____" Or Trim(txtToolLoadDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtToolLoadDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtToolManuDate_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtToolManuDate.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtToolManuDate.Text) = "__/__/____" Or Trim(txtToolManuDate.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtToolManuDate.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtToolNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtToolNo.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtToolNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtToolNo.DoubleClick
        Call cmdSearchToolNo_Click(cmdSearchToolNo, New System.EventArgs())
    End Sub

    Private Sub txtToolNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtToolNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtToolNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtToolNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtToolNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchToolNo_Click(cmdSearchToolNo, New System.EventArgs())
    End Sub

    Private Sub txtToolNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtToolNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ERR1

        If Trim(txtToolNo.Text) = "" Then GoTo EventExitSub

        If MODIFYMode = True And RsToolNo.EOF = False Then pToolNumber = RsToolNo.Fields("TOOL_NO").Value

        SqlStr = ""
        SqlStr = " SELECT * " & vbCrLf _
            & " FROM TOL_TOOLINFO_MST" & vbCrLf _
            & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
            & " AND TOOL_NO='" & MainClass.AllowSingleQuote(txtToolNo.Text) & "' "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsToolNo, ADODB.LockTypeEnum.adLockReadOnly)

        If RsToolNo.EOF = False Then
            ADDMode = False
            MODIFYMode = False
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("Such Tool No Does Not Exist" & vbCrLf & "Click Add To Add for New.", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = " SELECT * " & vbCrLf & " FROM TOL_TOOLINFO_MST" & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf & " AND TOOL_NO='" & MainClass.AllowSingleQuote(pToolNumber) & "' "

                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsToolNo, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If

        GoTo EventExitSub
ERR1:
        '    Resume				
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtToolItemCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtToolItemCode.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtToolItemCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtToolItemCode.DoubleClick
        Call cmdSearchToolItem_Click(cmdSearchToolItem, New System.EventArgs())
    End Sub

    Private Sub txtToolItemCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtToolItemCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtToolItemCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtToolItemCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtToolItemCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchToolItem_Click(cmdSearchToolItem, New System.EventArgs())
    End Sub

    Private Sub txtToolItemCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtToolItemCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        If Trim(txtToolItemCode.Text) = "" Then txtToolItemName.Text = "" : GoTo EventExitSub
        SqlStr = " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  " & vbCrLf & " AND ITEM_CLASSIFICATION='T' "

        If MainClass.ValidateWithMasterTable(txtToolItemCode.Text, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgInformation("Invalid Tool Item")
            Cancel = True
        Else
            txtToolItemName.Text = MasterNo
        End If

        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtDeptCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDeptCode.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDeptCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDeptCode.DoubleClick
        Call cmdSearchDept_Click(cmdSearchDept, New System.EventArgs())
    End Sub

    Private Sub txtDeptCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDeptCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtDeptCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtDeptCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtDeptCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchDept_Click(cmdSearchDept, New System.EventArgs())
    End Sub

    Private Sub txtDeptCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtDeptCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        If Trim(txtDeptCode.Text) = "" Then txtDeptDesc.Text = "" : GoTo EventExitSub
        If MainClass.ValidateWithMasterTable(txtDeptCode.Text, "DEPT_CODE", "DEPT_DESC", "PAY_DEPT_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invalid Depatment")
            Cancel = True
        Else
            txtDeptDesc.Text = MasterNo
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtOPRCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOprCode.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtOPRCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtOprCode.DoubleClick
        Call cmdSearchOPR_Click(cmdSearchOPR, New System.EventArgs())
    End Sub

    Private Sub txtOPRCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtOprCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtOprCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtOPRCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtOprCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchOPR_Click(cmdSearchOPR, New System.EventArgs())
    End Sub

    Private Sub txtOPRCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtOprCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        Dim RsTemp As ADODB.Recordset
        Dim SqlStr As String

        If Trim(txtOprCode.Text) = "" Then txtOprDesc.Text = "" : GoTo EventExitSub

        If Trim(txtDeptCode.Text) = "" Then
            MsgInformation("Please first you select Department.")
            txtDeptCode.Focus()
            txtOprCode.Text = ""
            txtOprDesc.Text = "" : GoTo EventExitSub
        End If

        If Trim(txtItemCode.Text) = "" Then
            MsgInformation("Please first you select Product Code.")
            txtItemCode.Focus()
            txtOprCode.Text = ""
            txtOprDesc.Text = "" : GoTo EventExitSub
        End If

        SqlStr = " SELECT OPR_DESC, OPR_CODE " & vbCrLf _
                & " FROM PRD_OPR_MST MST" & vbCrLf _
                & " WHERE " & vbCrLf _
                & " COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _
                & " AND DEPT_CODE='" & MainClass.AllowSingleQuote(txtDeptCode.Text) & "'"

        '    If Trim(txtDeptCode.Text) <> "" Then				
        '        SqlStr = SqlStr & vbCrLf & " AND MST.OPR_DESC='" & MainClass.AllowSingleQuote(txtDeptCode.Text) & "'"				
        '    End If				

        'If Trim(txtItemCode.Text) <> "" Then
        '    SqlStr = SqlStr & vbCrLf & " AND TRN.PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtItemCode.Text) & "'"
        'End If

        SqlStr = SqlStr & vbCrLf & " AND OPR_CODE='" & MainClass.AllowSingleQuote(txtOprCode.Text) & "'"

        SqlStr = SqlStr & vbCrLf & " ORDER BY OPR_CODE"

        'SqlStr = OperationQuery(Trim(txtItemCode.Text), Trim(txtDeptCode.Text), Trim(txtOprCode.Text), "", "", "MST.OPR_DESC", "TRN.OPR_CODE")

        '    SqlStr = " SELECT MST.OPR_DESC, TRN.OPR_CODE " & vbCrLf _				
        ''            & " FROM PRD_OPR_TRN TRN, PRD_OPR_MST MST" & vbCrLf _				
        ''            & " WHERE " & vbCrLf _				
        ''            & " TRN.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "" & vbCrLf _				
        ''            & " AND TRN.COMPANY_CODE=MST.COMPANY_CODE " & vbCrLf _				
        ''            & " AND TRN.OPR_CODE=MST.OPR_CODE " & vbCrLf _				
        ''            & " AND TRN.DEPT_CODE='" & MainClass.AllowSingleQuote(txtDeptCode.Text) & "'"				
        '				
        ''    If Trim(txtDeptCode.Text) <> "" Then				
        ''        SqlStr = SqlStr & vbCrLf & " AND MST.OPR_DESC='" & MainClass.AllowSingleQuote(txtDeptCode.Text) & "'"				
        ''    End If				
        '				
        '    If Trim(txtItemCode.Text) <> "" Then				
        '        SqlStr = SqlStr & vbCrLf & " AND TRN.PRODUCT_CODE='" & MainClass.AllowSingleQuote(txtItemCode.Text) & "'"				
        '    End If				
        '				
        '    SqlStr = SqlStr & vbCrLf & " AND TRN.OPR_CODE='" & MainClass.AllowSingleQuote(txtOprCode.Text) & "'"				
        '				
        '    SqlStr = SqlStr & vbCrLf & " ORDER BY TRN.OPR_CODE"				

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)

        If RsTemp.EOF = True Then
            MsgInformation("Invalid Operation")
            Cancel = True
        Else
            txtOprDesc.Text = IIf(IsDBNull(RsTemp.Fields("OPR_DESC").Value), "", RsTemp.Fields("OPR_DESC").Value)
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtItemCode_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemCode.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtItemCode_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtItemCode.DoubleClick
        Call cmdSearchItem_Click(cmdSearchItem, New System.EventArgs())
    End Sub

    Private Sub txtItemCode_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtItemCode.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtItemCode.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtItemCode_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtItemCode.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchItem_Click(cmdSearchItem, New System.EventArgs())
    End Sub

    Private Sub txtItemCode_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtItemCode.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        If Trim(txtItemCode.Text) = "" Then txtItemName.Text = "" : GoTo EventExitSub
        If MainClass.ValidateWithMasterTable(txtItemCode.Text, "ITEM_CODE", "ITEM_SHORT_DESC", "INV_ITEM_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invalid Production Item")
            Cancel = True
        Else
            txtItemName.Text = MasterNo
        End If
        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtToolManuDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtToolManuDate.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
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

    Private Sub txtDrgNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDrgNo.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtDrgNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtDrgNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtDrgNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtLocation_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtLocation.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtLocation_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtLocation.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtLocation.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtMasterToolNo_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMasterToolNo.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMasterToolNo_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMasterToolNo.DoubleClick
        Call cmdSearchMasterToolNo_Click(cmdSearchMasterToolNo, New System.EventArgs())
    End Sub

    Private Sub txtMasterToolNo_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtMasterToolNo.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtMasterToolNo.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtMasterToolNo_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtMasterToolNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then cmdSearchMasterToolNo_Click(cmdSearchMasterToolNo, New System.EventArgs())
    End Sub

    Private Sub txtMasterToolNo_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMasterToolNo.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        On Error GoTo ErrPart
        If Trim(txtMasterToolNo.Text) = "" Then GoTo EventExitSub
        If MainClass.ValidateWithMasterTable(txtMasterToolNo.Text, "TOOL_NO", "TOOL_NO", "TOL_TOOLINFO_MST", PubDBCn, MasterNo, , "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "") = False Then
            MsgInformation("Invalid Tool No")
            Cancel = True
        End If

        GoTo EventExitSub
ErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub cboToolFreq_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboToolFreq.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboToolFreq_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboToolFreq.SelectedIndexChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtToolLoadDate_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtToolLoadDate.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtToolLoadTime_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtToolLoadTime.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtToolPreventiveQty_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtToolPreventiveQty.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtToolPreventiveQty_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtToolPreventiveQty.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtToolPrdQty_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtToolPrdQty.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtToolPrdQty_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtToolPrdQty.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.SetNumericField(KeyAscii)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub txtToolLife_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtToolLife.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtToolLife_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtToolLife.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        KeyAscii = MainClass.UpperCase(KeyAscii, txtToolLife.Text)
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub cboToolStatus_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboToolStatus.TextChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub

    Private Sub cboToolStatus_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboToolStatus.SelectedIndexChanged
        MainClass.SaveStatus(Me.CmdSave, ADDMode, MODIFYMode)
    End Sub
End Class
