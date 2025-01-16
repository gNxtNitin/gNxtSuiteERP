Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility
Friend Class frmListOfMstSample
    Inherits System.Windows.Forms.Form
    Dim RsListOfMstSample As ADODB.Recordset
    'Private PvtDBCn As ADODB.Connection

    Dim NewCode As Short
    Dim ADDMode As Boolean
    Dim MODIFYMode As Boolean
    Dim XRIGHT As String
    Dim FormActive As Boolean


    Private Sub cmdAdd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAdd.Click
        On Error GoTo AddErr
        If CmdAdd.Text = ConCmdAddCaption Then
            ADDMode = True
            MODIFYMode = False
            Clear1()
            txtNumber.Enabled = False
            cmdSearchNo.Enabled = False
        Else
            ADDMode = False
            MODIFYMode = False
            If RsListOfMstSample.EOF = False Then RsListOfMstSample.MoveFirst()
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

        If txtNumber.Text = "" Then MsgInformation("Nothing to Delete") : Exit Sub
        If Not RsListOfMstSample.EOF Then
            If MsgQuestion("Want to Delete ? ") = CStr(MsgBoxResult.Yes) Then ' User chose Yes.
                PubDBCn.Errors.Clear()
                PubDBCn.BeginTrans()
                If InsertIntoDelAudit(PubDBCn, "QAL_MASTER_SAMPLE_TRN", (txtNumber.Text), RsListOfMstSample) = False Then GoTo DelErrPart
                PubDBCn.Execute("DELETE FROM QAL_MASTER_SAMPLE_TRN WHERE AUTO_KEY_LOMS=" & Val(lblMkey.Text) & "")
                PubDBCn.CommitTrans()
                RsListOfMstSample.Requery()
                Clear1()
            End If
        End If
        Exit Sub
DelErrPart:
        PubDBCn.RollbackTrans()
        '    PubDBCn.Errors.Clear
        RsListOfMstSample.Requery()
        MsgBox(Err.Description)
    End Sub

    Private Sub CmdModify_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdModify.Click

        On Error GoTo ModifyErr


        If CmdModify.Text = ConcmdmodifyCaption Then
            ADDMode = False
            MODIFYMode = True
            MainClass.ButtonStatus(Me, XRIGHT, RsListOfMstSample, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
            txtNumber.Enabled = False
            cmdSearchNo.Enabled = False
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
        If IsRecordExist = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Update1 = True Then
            ADDMode = False
            MODIFYMode = False
            txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False))
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
    Private Function IsRecordExist() As Boolean

        On Error GoTo IsRecordExistERR
        Dim RsTemp As ADODB.Recordset
        Dim SqlStr As String

        IsRecordExist = False
        If MODIFYMode = True Then Exit Function
        SqlStr = " SELECT AUTO_KEY_LOMS " & vbCrLf _
                & " FROM QAL_MASTER_SAMPLE_TRN " & vbCrLf _
                & " WHERE COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                & " AND UPPER(LTRIM(RTRIM(ITEM_CODE))) ='" & MainClass.AllowSingleQuote(UCase(txtProduct.Text)) & "'  " & vbCrLf _
                & " AND UPPER(LTRIM(RTRIM(SUPP_CUST_CODE))) = '" & MainClass.AllowSingleQuote(UCase(txtCustomer.Text)) & "'  "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With RsTemp
            If Not .EOF Then
                MsgBox("This entry already exist in Number : " & .Fields("AUTO_KEY_LOMS").Value)
                IsRecordExist = True
            End If
        End With
        Exit Function
IsRecordExistERR:
        IsRecordExist = True
        MsgBox(Err.Description)
    End Function

    Private Function Update1() As Boolean
        On Error GoTo ErrPart
        Dim SqlStr As String
        Dim mSlipNo As Double

        PubDBCn.Errors.Clear()
        PubDBCn.BeginTrans()


        SqlStr = ""
        mSlipNo = Val(txtNumber.Text)
        If Val(txtNumber.Text) = 0 Then
            mSlipNo = AutoGenKeyNo()
        End If
        txtNumber.Text = CStr(mSlipNo)
        If ADDMode = True Then
            lblMkey.Text = CStr(mSlipNo)
            SqlStr = " INSERT INTO QAL_MASTER_SAMPLE_TRN " & vbCrLf _
                            & " (AUTO_KEY_LOMS,COMPANY_CODE," & vbCrLf _
                            & " SUPP_CUST_CODE,ITEM_CODE,PROCESSED_ON_1," & vbCrLf _
                            & " PROCESSED_ON_2,PROCESSED_ON_3,PROCESSED_ON_4,PROCESSED_ON_5, " & vbCrLf _
                            & " PRE_EMP_CODE,ADDUSER, ADDDATE, MODUSER, MODDATE) " & vbCrLf _
                            & " VALUES ( " & vbCrLf _
                            & " " & mSlipNo & "," & RsCompany.Fields("COMPANY_CODE").Value & ", " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtCustomer.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtProduct.Text) & "'," & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtProcessed1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtProcessed2.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtProcessed3.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtProcessed4.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                            & " TO_DATE('" & vb6.Format(txtProcessed5.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY')," & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(txtMaintainedBy.Text) & "', " & vbCrLf _
                            & " '" & MainClass.AllowSingleQuote(PubUserID) & "',TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY'),'','')"
        ElseIf MODIFYMode = True Then
            SqlStr = " UPDATE QAL_MASTER_SAMPLE_TRN SET " & vbCrLf _
                    & " AUTO_KEY_LOMS=" & mSlipNo & ",COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & ", " & vbCrLf _
                    & " SUPP_CUST_CODE='" & MainClass.AllowSingleQuote(txtCustomer.Text) & "', " & vbCrLf _
                    & " ITEM_CODE='" & MainClass.AllowSingleQuote(txtProduct.Text) & "', " & vbCrLf _
                    & " PROCESSED_ON_1=TO_DATE('" & vb6.Format(txtProcessed1.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " PROCESSED_ON_2=TO_DATE('" & vb6.Format(txtProcessed2.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " PROCESSED_ON_3=TO_DATE('" & vb6.Format(txtProcessed3.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " PROCESSED_ON_4=TO_DATE('" & vb6.Format(txtProcessed4.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " PROCESSED_ON_5=TO_DATE('" & vb6.Format(txtProcessed5.Text, "DD-MMM-YYYY") & "','DD-MON-YYYY'), " & vbCrLf _
                    & " PRE_EMP_CODE='" & MainClass.AllowSingleQuote(txtMaintainedBy.Text) & "', " & vbCrLf _
                    & " MODUSER='" & MainClass.AllowSingleQuote(PubUserID) & "', " & vbCrLf _
                    & " MODDATE=TO_DATE('" & vb6.Format(PubCurrDate, "DD-MMM-YYYY") & "','DD-MON-YYYY')" & vbCrLf _
                    & " WHERE COMPANY_CODE=" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                    & " AND AUTO_KEY_LOMS =" & Val(lblMkey.text) & ""
        End If

        PubDBCn.Execute(SqlStr)
        Update1 = True
        PubDBCn.CommitTrans()
        txtNumber.Text = CStr(mSlipNo)
        Exit Function
ErrPart:
        Update1 = False
        PubDBCn.RollbackTrans()
        RsListOfMstSample.Requery()
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
        SqlStr = "SELECT Max(AUTO_KEY_LOMS)  " & vbCrLf & " FROM QAL_MASTER_SAMPLE_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_LOMS,LENGTH(AUTO_KEY_LOMS)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""

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

    Private Sub cmdSearchMainBy_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearchMainBy.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND EMP_LEAVE_DATE IS NULL "
        If MainClass.SearchGridMaster("", "PAY_EMPLOYEE_MST", "EMP_NAME", "EMP_CODE", , , SqlStr) = True Then
            txtMaintainedBy.Text = AcName1
            lblMaintainedBy.text = AcName
        End If
    End Sub

    Private Sub CmdSearchCustomer_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearchCustomer.Click
        Dim SqlStr As String
        SqlStr = "SELECT DISTINCT A.SUPP_CUST_CODE, A.SUPP_CUST_NAME " & vbCrLf & " FROM FIN_SUPP_CUST_MST A, FIN_SUPP_CUST_DET B " & vbCrLf & " WHERE A.COMPANY_CODE = B.COMPANY_CODE " & vbCrLf & " AND A.SUPP_CUST_CODE = B.SUPP_CUST_CODE " & vbCrLf & " AND A.COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " ORDER BY A.SUPP_CUST_CODE "
        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtCustomer.Text = AcName
            lblCustomer.text = AcName1
            txtCustomer_Validating(txtCustomer, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
        End If

    End Sub

    Private Sub cmdSearchNo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearchNo.Click
        Dim SqlStr As String
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_LOMS,LENGTH(AUTO_KEY_LOMS)-5,4)=" & RsCompany.Fields("FYEAR").Value & ""
        If MainClass.SearchGridMaster(txtNumber.Text, "QAL_MASTER_SAMPLE_TRN", "AUTO_KEY_LOMS", "SUPP_CUST_CODE", "ITEM_CODE", "PRE_EMP_CODE", SqlStr) = True Then
            txtNumber.Text = AcName
            Call txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False)) ''__Validating(XXXX, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
        End If
    End Sub

    Private Sub CmdSearchProduct_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdSearchProduct.Click
        Dim SqlStr As String
        SqlStr = "SELECT A.ITEM_CODE, B.ITEM_SHORT_DESC, B.CUSTOMER_PART_NO " & vbCrLf _
                & " FROM FIN_SUPP_CUST_DET A ,INV_ITEM_MST B  " & vbCrLf _
                & " WHERE A.COMPANY_CODE =B.COMPANY_CODE " & vbCrLf _
                & " AND A.ITEM_CODE =  B.ITEM_CODE " & vbCrLf _
                & " AND A.COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                & " AND A.SUPP_CUST_CODE ='" & MainClass.AllowSingleQuote(txtCustomer.Text) & "' " & vbCrLf _
                & " ORDER BY A.ITEM_CODE   "
        If MainClass.SearchGridMasterBySQL2("", SqlStr) = True Then
            txtProduct.Text = AcName
            lblProduct.text = AcName1
            txtProduct_Validating(txtProduct, New System.ComponentModel.CancelEventArgs(False)) ''_Validate(False)
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
        MainClass.ButtonStatus(Me, XRIGHT, RsListOfMstSample, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub frmListOfMstSample_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        On Error GoTo ERR1
        Dim SqlStr As String

        If FormActive = True Then Exit Sub
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Me.Text = "List Of Master Samples"

        SqlStr = "Select * From QAL_MASTER_SAMPLE_TRN WHERE 1<>1"
        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsListOfMstSample, ADODB.LockTypeEnum.adLockReadOnly)


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

        SqlStr = " SELECT " & vbCrLf & " AUTO_KEY_LOMS AS COMPLAINT_NUMBER,SUPP_CUST_CODE,ITEM_CODE, " & vbCrLf & " TO_CHAR(PROCESSED_ON_1,'DD/MM/YYYY') AS PROCESSED_ON_1,TO_CHAR(PROCESSED_ON_2,'DD/MM/YYYY') AS PROCESSED_ON_2,  " & vbCrLf & " TO_CHAR(PROCESSED_ON_3,'DD/MM/YYYY') AS PROCESSED_ON_3,TO_CHAR(PROCESSED_ON_4,'DD/MM/YYYY') AS PROCESSED_ON_4,  " & vbCrLf & " TO_CHAR(PROCESSED_ON_5,'DD/MM/YYYY') AS PROCESSED_ON_5,  " & vbCrLf & " PRE_EMP_CODE " & vbCrLf & " FROM QAL_MASTER_SAMPLE_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_LOMS,LENGTH(AUTO_KEY_LOMS)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " ORDER BY AUTO_KEY_LOMS"
        MainClass.AssignDataInSprd8(SqlStr, SprdView, StrConn, IIf(mRefresh = True, "Y", "N"))
        FormatSprdView()
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
    End Sub

    Private Sub frmListOfMstSample_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
        If KeyAscii = System.Windows.Forms.Keys.Return Then System.Windows.Forms.SendKeys.Send("{Tab}")
        eventArgs.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub

    Private Sub frmListOfMstSample_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        On Error GoTo ErrPart
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'Set PvtDBCn = New ADODB.Connection
        'PvtDBCn.Open StrConn
        XRIGHT = MainClass.STRMenuRight(PubUserID, CurrModuleID, myMenu, PubDBCn)
        MainClass.RightsToButton(Me, XRIGHT)
        MainClass.SetControlsColor(Me)
        Me.Left = 0
        Me.Top = 0
        'Me.Height = VB6.TwipsToPixelsY(4140)
        'Me.Width = VB6.TwipsToPixelsX(9285)
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
        txtNumber.Text = ""
        txtCustomer.Text = ""
        lblCustomer.Text = ""
        txtProduct.Text = ""
        lblProduct.Text = ""
        lblPartNo.Text = ""
        txtProcessed1.Text = ""
        txtProcessed2.Text = ""
        txtProcessed3.Text = ""
        txtProcessed4.Text = ""
        txtProcessed5.Text = ""
        txtMaintainedBy.Text = ""
        lblMaintainedBy.Text = ""
        Call MakeEnableDesableField(True)
        MainClass.ButtonStatus(Me, XRIGHT, RsListOfMstSample, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ClearErr:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
    End Sub
    Private Sub FormatSprdView()

        With SprdView
            .Row = -1
            .set_RowHeight(0, 600)
            .set_ColWidth(0, 500 * 2)
            .set_ColWidth(1, 500 * 4)
            .set_ColWidth(2, 500 * 4)
            .set_ColWidth(3, 500 * 4)
            .set_ColWidth(4, 500 * 4)
            .set_ColWidth(5, 500 * 4)
            .set_ColWidth(6, 500 * 4)
            .set_ColWidth(7, 500 * 4)
            .set_ColWidth(8, 500 * 4)
            .set_ColWidth(9, 500 * 4)
            .ColsFrozen = 2
            MainClass.ProtectCell(SprdView, 1, .MaxRows, 1, .MaxCols)
            MainClass.SetSpreadColor(SprdView, -1)
            .OperationMode = FPSpreadADO.OperationModeConstants.OperationModeSingle
            MainClass.CellColor(SprdView, 1, .MaxRows, 1, .MaxCols)
        End With
    End Sub
    Private Sub SetTextLengths()
        On Error GoTo ERR1

        txtNumber.Maxlength = RsListOfMstSample.Fields("AUTO_KEY_LOMS").Precision
        txtCustomer.Maxlength = RsListOfMstSample.Fields("SUPP_CUST_CODE").DefinedSize
        txtProduct.Maxlength = RsListOfMstSample.Fields("ITEM_CODE").DefinedSize
        txtProcessed1.Maxlength = RsListOfMstSample.Fields("PROCESSED_ON_1").DefinedSize - 6
        txtProcessed2.Maxlength = RsListOfMstSample.Fields("PROCESSED_ON_2").DefinedSize - 6
        txtProcessed3.Maxlength = RsListOfMstSample.Fields("PROCESSED_ON_3").DefinedSize - 6
        txtProcessed4.Maxlength = RsListOfMstSample.Fields("PROCESSED_ON_4").DefinedSize - 6
        txtProcessed5.Maxlength = RsListOfMstSample.Fields("PROCESSED_ON_5").DefinedSize - 6
        txtMaintainedBy.Maxlength = RsListOfMstSample.Fields("PRE_EMP_CODE").DefinedSize
        Exit Sub
ERR1:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        'Resume
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
        If MODIFYMode = True And RsListOfMstSample.EOF = True Then Exit Function

        If Trim(txtCustomer.Text) = "" Then
            MsgInformation("Customer is empty, So unable to save.")
            txtCustomer.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtProduct.Text) = "" Then
            MsgInformation("Product Code is empty, So unable to save.")
            txtProduct.Focus()
            FieldsVarification = False
            Exit Function
        End If
        If Trim(txtProcessed1.Text) = "" Then
            MsgInformation("Process On 1 Date is empty, So unable to save.")
            txtProcessed1.Focus()
            FieldsVarification = False
            Exit Function
        End If

        If Trim(txtMaintainedBy.Text) = "" Then
            MsgInformation("Maintained By is empty, So unable to save.")
            txtMaintainedBy.Focus()
            FieldsVarification = False
            Exit Function
        End If

        Exit Function
err_Renamed:
        MsgBox(Err.Description)
        ''Resume
    End Function

    Private Sub frmListOfMstSample_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        RsListOfMstSample.Close()
        RsListOfMstSample = Nothing
        'PvtDBCn.Close
        'Set PvtDBCn = Nothing
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub SprdView_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles SprdView.DblClick
        SprdView.Col = 1
        SprdView.Row = SprdView.ActiveRow
        txtNumber.Text = SprdView.Text
        txtNumber_Validating(txtNumber, New System.ComponentModel.CancelEventArgs(False))
        Call CmdView_Click(CmdView, New System.EventArgs())
    End Sub
    Private Sub SprdView_KeyPressEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpreadADO._DSpreadEvents_KeyPressEvent) Handles SprdView.KeyPressEvent
        If eventArgs.KeyAscii = System.Windows.Forms.Keys.Return Then SprdView_DblClick(SprdView, New AxFPSpreadADO._DSpreadEvents_DblClickEvent(SprdView.ActiveCol, SprdView.ActiveRow))
    End Sub

    Private Sub txtMaintainedBy_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMaintainedBy.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtMaintainedBy_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtMaintainedBy.DoubleClick
        Call cmdSearchMainBy_Click(cmdSearchMainBy, New System.EventArgs())
    End Sub

    Private Sub txtMaintainedBy_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtMaintainedBy.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchMainBy_Click(cmdSearchMainBy, New System.EventArgs())
    End Sub

    Private Sub txtMaintainedBy_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtMaintainedBy.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim SqlStr As String
        If Trim(txtMaintainedBy.Text) = "" Then GoTo EventExitSub
        SqlStr = "COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & "  AND EMP_LEAVE_DATE IS NULL "
        If MainClass.ValidateWithMasterTable(txtMaintainedBy.Text, "EMP_CODE", "EMP_NAME", "PAY_EMPLOYEE_MST", PubDBCn, MasterNo, , SqlStr) = False Then
            MsgBox("EMPLOYEE Does Not Exist In Master.")
            Cancel = True
        Else
            lblMaintainedBy.text = MasterNo
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtCustomer_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustomer.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtCustomer_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustomer.DoubleClick
        Call CmdSearchCustomer_Click(CmdSearchCustomer, New System.EventArgs())
    End Sub

    Private Sub txtCustomer_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCustomer.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call CmdSearchCustomer_Click(CmdSearchCustomer, New System.EventArgs())
    End Sub

    Private Sub txtCustomer_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCustomer.Leave
        If Trim(txtCustomer.Text) = "" Then Exit Sub
        txtProduct.Focus()
    End Sub

    Private Sub txtCustomer_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCustomer.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim SqlStr As String
        Dim mRsTemp As ADODB.Recordset
        If Trim(txtCustomer.Text) = "" Then GoTo EventExitSub

        SqlStr = " SELECT DISTINCT A.SUPP_CUST_NAME,A.SUPP_CUST_CODE " & vbCrLf _
                    & " FROM FIN_SUPP_CUST_MST A, FIN_SUPP_CUST_DET B " & vbCrLf _
                    & " WHERE A.COMPANY_CODE = B.COMPANY_CODE " & vbCrLf _
                    & " AND A.SUPP_CUST_CODE = B.SUPP_CUST_CODE " & vbCrLf _
                    & " AND A.COMPANY_CODE = " & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                    & " AND A.SUPP_CUST_CODE ='" & MainClass.AllowSingleQuote(txtCustomer.Text) & "'  "

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With mRsTemp
            If Not .EOF Then
                txtCustomer.Text = IIf(IsDbNull(mRsTemp.Fields("SUPP_CUST_CODE").Value), "", .Fields("SUPP_CUST_CODE").Value)
                lblCustomer.Text = IIf(IsDbNull(mRsTemp.Fields("SUPP_CUST_NAME").Value), "", .Fields("SUPP_CUST_NAME").Value)
            Else
                MsgBox("Not a valid Customer")
                txtCustomer.Text = ""
                lblCustomer.Text = ""
                Cancel = True
            End If
        End With
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub


    Private Sub txtNumber_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNumber.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub
    Private Sub Show1()

        On Error GoTo ShowErrPart

        If Not RsListOfMstSample.EOF Then
            lblMkey.Text = IIf(IsDbNull(RsListOfMstSample.Fields("AUTO_KEY_LOMS").Value), "", RsListOfMstSample.Fields("AUTO_KEY_LOMS").Value)
            txtNumber.Text = IIf(IsDbNull(RsListOfMstSample.Fields("AUTO_KEY_LOMS").Value), "", RsListOfMstSample.Fields("AUTO_KEY_LOMS").Value)
            txtCustomer.Text = IIf(IsDbNull(RsListOfMstSample.Fields("SUPP_CUST_CODE").Value), "", RsListOfMstSample.Fields("SUPP_CUST_CODE").Value)
            txtCustomer_Validating(txtCustomer, New System.ComponentModel.CancelEventArgs(False))
            txtProduct.Text = IIf(IsDbNull(RsListOfMstSample.Fields("ITEM_CODE").Value), "", RsListOfMstSample.Fields("ITEM_CODE").Value)
            txtProduct_Validating(txtProduct, New System.ComponentModel.CancelEventArgs(False))
            txtProcessed1.Text = IIf(IsDbNull(RsListOfMstSample.Fields("PROCESSED_ON_1").Value), "", RsListOfMstSample.Fields("PROCESSED_ON_1").Value)
            txtProcessed2.Text = IIf(IsDbNull(RsListOfMstSample.Fields("PROCESSED_ON_2").Value), "", RsListOfMstSample.Fields("PROCESSED_ON_2").Value)
            txtProcessed3.Text = IIf(IsDbNull(RsListOfMstSample.Fields("PROCESSED_ON_3").Value), "", RsListOfMstSample.Fields("PROCESSED_ON_3").Value)
            txtProcessed4.Text = IIf(IsDbNull(RsListOfMstSample.Fields("PROCESSED_ON_4").Value), "", RsListOfMstSample.Fields("PROCESSED_ON_4").Value)
            txtProcessed5.Text = IIf(IsDbNull(RsListOfMstSample.Fields("PROCESSED_ON_5").Value), "", RsListOfMstSample.Fields("PROCESSED_ON_5").Value)
            txtMaintainedBy.Text = IIf(IsDbNull(RsListOfMstSample.Fields("PRE_EMP_CODE").Value), "", RsListOfMstSample.Fields("PRE_EMP_CODE").Value)
            txtMaintainedBy_Validating(txtMaintainedBy, New System.ComponentModel.CancelEventArgs(False))
            Call MakeEnableDesableField(False)
        End If
        ADDMode = False
        MODIFYMode = False
        txtNumber.Enabled = True
        cmdSearchNo.Enabled = True
        MainClass.ButtonStatus(Me, XRIGHT, RsListOfMstSample, ADDMode, MODIFYMode, CmdAdd, CmdModify, CmdClose, CmdSave, CmdDelete, cmdSavePrint, cmdSavePrint, cmdPrint, CmdPreview, cmdSavePrint, CmdView, True)
        Exit Sub
ShowErrPart:
        ErrorMsg(Err.Description, CStr(Err.Number), MsgBoxStyle.Critical)
        '    Resume
    End Sub
    Private Sub txtNumber_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtNumber.DoubleClick
        Call cmdSearchNo_Click(cmdSearchNo, New System.EventArgs())
    End Sub

    Private Sub txtNumber_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtNumber.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call cmdSearchNo_Click(cmdSearchNo, New System.EventArgs())
    End Sub
    Private Sub txtNumber_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtNumber.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        On Error GoTo ERR1
        Dim xMKey As Double
        Dim mSlipNo As Double
        Dim SqlStr As String


        If Trim(txtNumber.Text) = "" Then GoTo EventExitSub
        mSlipNo = Val(txtNumber.Text)

        If MODIFYMode = True And RsListOfMstSample.BOF = False Then xMKey = RsListOfMstSample.Fields("AUTO_KEY_LOMS").Value

        SqlStr = "SELECT * FROM QAL_MASTER_SAMPLE_TRN " & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_LOMS,LENGTH(AUTO_KEY_LOMS)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_LOMS=" & mSlipNo & ""

        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsListOfMstSample, ADODB.LockTypeEnum.adLockReadOnly)
        If RsListOfMstSample.EOF = False Then
            Clear1()
            Show1()
        Else
            If ADDMode = False And MODIFYMode = False Then
                MsgBox("No Such Number. Click, Add for New", MsgBoxStyle.Information)
                Cancel = True
            ElseIf MODIFYMode = True Then
                SqlStr = "SELECT * FROM QAL_MASTER_SAMPLE_TRN " & vbCrLf & " WHERE COMPANY_CODE=" & RsCompany.Fields("COMPANY_CODE").Value & " " & vbCrLf & " AND SUBSTR(AUTO_KEY_LOMS,LENGTH(AUTO_KEY_LOMS)-5,4)=" & RsCompany.Fields("FYEAR").Value & "" & vbCrLf & " AND AUTO_KEY_LOMS=" & Val(CStr(xMKey)) & " "
                MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, RsListOfMstSample, ADODB.LockTypeEnum.adLockReadOnly)
            End If
        End If
        GoTo EventExitSub
ERR1:
        MsgInformation(Err.Description)
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
    Private Sub MakeEnableDesableField(ByRef mMode As Boolean)
        txtCustomer.Enabled = mMode
        cmdSearchCustomer.Enabled = mMode
        txtProduct.Enabled = mMode
        CmdSearchProduct.Enabled = mMode
        txtMaintainedBy.Enabled = mMode
        CmdSearchMainBy.Enabled = mMode
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
    Private Sub ReportOnListOfMastSample(ByRef Mode As Crystal.DestinationConstants)

    End Sub
    Private Sub cmdPreview_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdPreview.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnListOfMastSample(Crystal.DestinationConstants.crptToWindow)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Call ReportOnListOfMastSample(Crystal.DestinationConstants.crptToPrinter)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub


    Private Sub txtProcessed1_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProcessed1.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtProcessed1_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtProcessed1.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtProcessed1.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtProcessed1.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtProcessed2_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProcessed2.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtProcessed2_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtProcessed2.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtProcessed2.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtProcessed2.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtProcessed3_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProcessed3.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtProcessed3_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtProcessed3.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtProcessed3.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtProcessed3.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtProcessed4_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProcessed4.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtProcessed4_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtProcessed4.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtProcessed4.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtProcessed4.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtProcessed5_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProcessed5.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtProcessed5_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtProcessed5.Validating
        Dim Cancel As Boolean = eventArgs.Cancel
        If Trim(txtProcessed5.Text) = "" Then GoTo EventExitSub
        If Not IsDate(txtProcessed5.Text) Then
            MsgBox("Not a valid date.")
            Cancel = True
        End If
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtProduct_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProduct.TextChanged

        MainClass.SaveStatus(Me.cmdsave, ADDMode, MODIFYMode)
    End Sub

    Private Sub txtProduct_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtProduct.DoubleClick
        Call CmdSearchProduct_Click(CmdSearchProduct, New System.EventArgs())
    End Sub

    Private Sub txtProduct_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtProduct.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If KeyCode = System.Windows.Forms.Keys.F1 Then Call CmdSearchProduct_Click(CmdSearchProduct, New System.EventArgs())
    End Sub

    Private Sub txtProduct_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtProduct.Validating
        Dim Cancel As Boolean = eventArgs.Cancel

        Dim mRsTemp As ADODB.Recordset
        Dim SqlStr As String
        If Trim(txtProduct.Text) = "" Then GoTo EventExitSub
        SqlStr = " SELECT A.ITEM_CODE,B.ITEM_SHORT_DESC,B.CUSTOMER_PART_NO " & vbCrLf _
                & " FROM FIN_SUPP_CUST_DET A,INV_ITEM_MST B  " & vbCrLf _
                & " WHERE A.COMPANY_CODE = B.COMPANY_CODE " & vbCrLf _
                & " AND A.ITEM_CODE = B.ITEM_CODE " & vbCrLf _
                & " AND A.COMPANY_CODE =" & RsCompany.fields("COMPANY_CODE").value & " " & vbCrLf _
                & " AND A.ITEM_CODE ='" & MainClass.AllowSingleQuote(txtProduct.Text) & "' " & vbCrLf _
                & " AND A.SUPP_CUST_CODE ='" & MainClass.AllowSingleQuote(txtCustomer.Text) & "' "


        MainClass.UOpenRecordSet(SqlStr, PubDBCn, ADODB.CursorTypeEnum.adOpenStatic, mRsTemp, ADODB.LockTypeEnum.adLockReadOnly)
        With mRsTemp
            If Not .EOF Then
                txtProduct.Text = IIf(IsDbNull(mRsTemp.Fields("ITEM_CODE").Value), "", .Fields("ITEM_CODE").Value)
                lblProduct.Text = IIf(IsDbNull(mRsTemp.Fields("Item_Short_Desc").Value), "", .Fields("Item_Short_Desc").Value)
                lblPartNo.Text = IIf(IsDbNull(mRsTemp.Fields("CUSTOMER_PART_NO").Value), "", .Fields("CUSTOMER_PART_NO").Value)
            Else
                MsgBox("Not a valid Customer's Product.")
                txtProduct.Text = ""
                lblProduct.Text = ""
                lblPartNo.Text = ""
                Cancel = True
            End If
        End With
EventExitSub:
        eventArgs.Cancel = Cancel
    End Sub
End Class
